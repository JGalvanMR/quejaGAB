using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Script.Serialization;

namespace queja
{
    /// <summary>
    /// Envía correos mediante Microsoft Graph (POST /users/{buzón}/sendMail) con credenciales de aplicación
    /// de Microsoft Entra ID. Acepta un MailMessage para que migrar desde SmtpClient sea cambiar solo el Send.
    /// Usa HttpWebRequest y JavaScriptSerializer porque el proyecto es .NET Framework 4.0 (sin MSAL ni HttpClient).
    /// </summary>
    public static class CorreoGraph
    {
        private static readonly object _lock = new object();
        private static string _token;
        private static DateTime _tokenExpira = DateTime.MinValue;

        public static void Enviar(MailMessage mensaje)
        {
            try
            {
                EnviarInterno(mensaje);
            }
            catch (Exception ex)
            {
                RegistrarError(mensaje, ex);
                throw;
            }
        }

        // Los errores quedan en Graph.LogRuta, o en App_Data/correo_graph.log (ASP.NET no sirve App_Data por HTTP).
        // Nunca se registra el secreto ni el cuerpo del mensaje. Si no se puede escribir, se intenta en la carpeta temporal.
        private static void RegistrarError(MailMessage mensaje, Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Error al enviar correo por Graph");
            if (mensaje != null)
            {
                sb.AppendLine("  De: " + (mensaje.From != null ? mensaje.From.Address : "(sin remitente)"));
                sb.AppendLine("  Para: " + string.Join("; ", Direcciones(mensaje.To)));
                sb.AppendLine("  Asunto: " + mensaje.Subject);
            }
            for (Exception e = ex; e != null; e = e.InnerException)
                sb.AppendLine("  " + e.GetType().Name + ": " + e.Message);
            sb.AppendLine();

            string ruta = Config("Graph.LogRuta");
            if (string.IsNullOrEmpty(ruta))
                ruta = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"), "correo_graph.log");

            lock (_lock)
            {
                foreach (string destino in new[] { ruta, Path.Combine(Path.GetTempPath(), "correo_graph.log") })
                {
                    try
                    {
                        string dir = Path.GetDirectoryName(destino);
                        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
                        var fi = new FileInfo(destino);
                        if (fi.Exists && fi.Length > 5 * 1024 * 1024)
                        {
                            File.Delete(destino + ".old");
                            File.Move(destino, destino + ".old");
                        }
                        File.AppendAllText(destino, sb.ToString(), Encoding.UTF8);
                        return;
                    }
                    catch
                    {
                        // Probar el siguiente destino; registrar el error nunca debe romper el flujo.
                    }
                }
            }
        }

        private static IEnumerable<string> Direcciones(MailAddressCollection lista)
        {
            foreach (MailAddress d in lista) yield return d.Address;
        }

        private static void EnviarInterno(MailMessage mensaje)
        {
            if (mensaje == null) throw new ArgumentNullException("mensaje");

            // Graph exige TLS 1.2 (3072); en .NET 4.0 el valor existe solo con 4.5+ instalado en el servidor.
            ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072;

            // El remitente es siempre el From del MailMessage (el mismo que usaba SMTP); Graph.Buzon solo aplica si no trae From.
            string buzon = mensaje.From != null ? mensaje.From.Address : Config("Graph.Buzon");
            if (string.IsNullOrEmpty(buzon))
                throw new InvalidOperationException("El mensaje no tiene remitente (From) ni Graph.Buzon configurado.");

            var msg = new Dictionary<string, object>();
            msg["subject"] = mensaje.Subject ?? "";
            msg["body"] = new Dictionary<string, object>
            {
                { "contentType", mensaje.IsBodyHtml ? "HTML" : "Text" },
                { "content", mensaje.Body ?? "" }
            };
            msg["toRecipients"] = Destinatarios(mensaje.To);
            if (mensaje.CC.Count > 0) msg["ccRecipients"] = Destinatarios(mensaje.CC);
            if (mensaje.Bcc.Count > 0) msg["bccRecipients"] = Destinatarios(mensaje.Bcc);

            if (mensaje.Attachments.Count > 0)
            {
                var adjuntos = new List<object>();
                foreach (Attachment a in mensaje.Attachments)
                {
                    a.ContentStream.Position = 0;
                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        a.ContentStream.CopyTo(ms);
                        bytes = ms.ToArray();
                    }
                    adjuntos.Add(new Dictionary<string, object>
                    {
                        { "@odata.type", "#microsoft.graph.fileAttachment" },
                        { "name", a.Name },
                        { "contentBytes", Convert.ToBase64String(bytes) }
                    });
                }
                msg["attachments"] = adjuntos;
            }

            var cuerpo = new Dictionary<string, object>
            {
                { "message", msg },
                { "saveToSentItems", true }
            };
            string json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(cuerpo);

            string url = "https://graph.microsoft.com/v1.0/users/" + Uri.EscapeDataString(buzon) + "/sendMail";
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json; charset=utf-8";
            req.Headers["Authorization"] = "Bearer " + ObtenerToken();
            req.Timeout = 60000;
            byte[] datos = Encoding.UTF8.GetBytes(json);
            using (var s = req.GetRequestStream()) s.Write(datos, 0, datos.Length);

            // sendMail responde 202 Accepted sin contenido.
            using (Responder(req)) { }
        }

        private static List<object> Destinatarios(MailAddressCollection lista)
        {
            var r = new List<object>();
            foreach (MailAddress d in lista)
                r.Add(new Dictionary<string, object>
                {
                    { "emailAddress", new Dictionary<string, object> { { "address", d.Address } } }
                });
            return r;
        }

        private static string ObtenerToken()
        {
            lock (_lock)
            {
                if (_token != null && DateTime.UtcNow < _tokenExpira) return _token;

                string tenant = Requerida("Graph.TenantId");
                string form = "client_id=" + Uri.EscapeDataString(Requerida("Graph.ClientId"))
                    + "&client_secret=" + Uri.EscapeDataString(Requerida("Graph.Secreto"))
                    + "&scope=" + Uri.EscapeDataString("https://graph.microsoft.com/.default")
                    + "&grant_type=client_credentials";

                var req = (HttpWebRequest)WebRequest.Create(
                    "https://login.microsoftonline.com/" + tenant + "/oauth2/v2.0/token");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Timeout = 30000;
                byte[] datos = Encoding.UTF8.GetBytes(form);
                using (var s = req.GetRequestStream()) s.Write(datos, 0, datos.Length);

                string respuesta;
                using (var resp = Responder(req))
                using (var rd = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                    respuesta = rd.ReadToEnd();

                var d = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(respuesta);
                _token = Convert.ToString(d["access_token"]);
                int segundos = Convert.ToInt32(d["expires_in"]);
                _tokenExpira = DateTime.UtcNow.AddSeconds(segundos - 300);
                return _token;
            }
        }

        private static HttpWebResponse Responder(HttpWebRequest req)
        {
            try
            {
                return (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                string detalle = ex.Message;
                var resp = ex.Response as HttpWebResponse;
                if (resp != null)
                    using (var rd = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                        detalle = (int)resp.StatusCode + ": " + rd.ReadToEnd();
                throw new InvalidOperationException("Microsoft respondió " + detalle, ex);
            }
        }

        // El secreto se lee primero de la variable de entorno (equivalente a la app de escritorio) y luego del Web.config.
        private static string Config(string clave)
        {
            if (clave == "Graph.Secreto")
            {
                string env = Environment.GetEnvironmentVariable("MRLUCKY_GRAPH_SECRET");
                if (!string.IsNullOrEmpty(env)) return env;
            }
            return ConfigurationManager.AppSettings[clave];
        }

        private static string Requerida(string clave)
        {
            string v = Config(clave);
            if (string.IsNullOrEmpty(v))
                throw new InvalidOperationException("Falta la configuración " + clave + ".");
            return v;
        }
    }
}
