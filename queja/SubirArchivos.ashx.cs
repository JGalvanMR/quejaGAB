using System;
using System.Web;
using System.IO;

namespace queja // ¡Cambia esto por el namespace real de tu proyecto!
{
    public class SubirArchivos : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                // 1. Saber a qué folio pertenecen
                string folio = context.Request.Form["folio"];

                if (string.IsNullOrEmpty(folio))
                {
                    context.Response.Write("ERROR: No se recibió el folio.");
                    return;
                }

                // 2. Definir la ruta física donde se guardarán (ej: C:\...\imagenes\quejas\13540\)
                string rutaCarpeta = context.Server.MapPath("~/imagenes/quejas/" + folio + "/");

                // Crear la carpeta si no existe
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }

                // 3. Recuperar los archivos usando el MISMO NAME que pusiste en el formData.append de JS
                HttpPostedFile archivo1 = context.Request.Files["archivo1"];
                HttpPostedFile archivo2 = context.Request.Files["archivo2"];
                HttpPostedFile archivo3 = context.Request.Files["archivo3"];

                int archivosGuardados = 0;

                // 4. Guardar Archivo 1
                if (archivo1 != null && archivo1.ContentLength > 0)
                {
                    // archivo1.FileName YA TIENE el nombre que le pusiste en JS (ej: 1_13540.jpg)
                    string ruta1 = Path.Combine(rutaCarpeta, archivo1.FileName);
                    archivo1.SaveAs(ruta1);
                    archivosGuardados++;
                }

                // 5. Guardar Archivo 2
                if (archivo2 != null && archivo2.ContentLength > 0)
                {
                    string ruta2 = Path.Combine(rutaCarpeta, archivo2.FileName);
                    archivo2.SaveAs(ruta2);
                    archivosGuardados++;
                }

                // 6. Guardar Archivo 3
                if (archivo3 != null && archivo3.ContentLength > 0)
                {
                    string ruta3 = Path.Combine(rutaCarpeta, archivo3.FileName);
                    archivo3.SaveAs(ruta3);
                    archivosGuardados++;
                }

                if (archivosGuardados > 0)
                {
                    context.Response.Write("OK"); // Si regresa esto, en JS entra al "success"
                }
                else
                {
                    context.Response.Write("NO_FILES"); // No seleccionó ninguna imagen
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("ERROR: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}