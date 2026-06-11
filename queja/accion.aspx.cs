using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace queja
{
    public partial class accion : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtDatos = new DataTable();
        private DataTable dtInsec = new DataTable();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;
            string str = Convert.ToString(this.Request.QueryString["key"]);
            this.dtDatos = this.con.accion_correctiva_fumigacion(str);
            this.dtInsec = this.con.accion_correctiva_fumigacion_insectos(str);
            if (this.con.validar_accion_correctiva(str) == 1)
            {
                this.txtAccion.Enabled = false;
                this.upnAccion.Update();
                this.btnGuardar.Enabled = false;
                this.udpGuardar.Update();
            }
            if (this.dtDatos.Rows.Count > 0)
            {
                this.txtFolio.Text = str;
                this.txtCedis.Text = this.dtDatos.Rows[0]["cedis"].ToString();
                this.txtRegistro.Text = this.dtDatos.Rows[0]["realizado"].ToString();
                this.txtFecha.Text = this.dtDatos.Rows[0]["fecha"].ToString();
                this.txtTransportista.Text = this.dtDatos.Rows[0]["transportista"].ToString();
                this.txtCaja.Text = this.dtDatos.Rows[0]["caja"].ToString();
                this.txtPedido.Text = this.dtDatos.Rows[0]["pedido"].ToString();
                this.txtTipo.Text = this.dtDatos.Rows[0]["tipo"].ToString();
                this.txtAccCorrectiva.Text = "";//this.dtDatos.Rows[0][nameof(accion)].ToString();
                this.gvwQuejas.DataSource = (object)this.dtInsec;
                this.gvwQuejas.DataBind();
                this.udpGeneral.Update();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.txtAccion.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Agregar accion correctiva');", true);
            }
            else
            {
                this.btnGuardar.Enabled = false;
                this.udpGuardar.Update();
                string text = this.txtAccion.Text;
                this.txtAccion.Enabled = false;
                this.upnAccion.Update();
                if (this.con.accion_correctiva_ingreso(this.txtFolio.Text, text) == "0")
                {
                    this.lblWarning.Visible = true;
                }
                else
                {
                    this.lblSuccess.Visible = true;
                    DataTable dataTable = this.con.accion_correctiva_fumigacion_insectos(this.txtFolio.Text);
                    string str1 = "<tr><td><table><tr><td><b>Insecto(s):</b></td></tr>";
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                        str1 = str1 + "<tr><td>" + row["insecto"] + "</td></tr>";
                    string str2 = str1 + "</table></td></tr>";
                    string str3 = "http://189.206.160.206:81/quejas/";
                    string str4 = "http://gabira1:81/quejas/";
                    this.enviarcorreo("msamano@mrlucky.com.mx", this.txtFolio.Text, "<table border='2'><tr><td align='center'><h2>Registro de Queja de Fumigacion (Accion Correctiva)</h2></td></tr><tr><td>Fecha: " + DateTime.Now.ToLongDateString() + "</td></tr><tr><td>Queja No.: " + this.txtFolio.Text + "</td></tr><tr><td>CEDIS: " + this.txtCedis.Text + "</td></tr><tr><td>Transportista: " + this.txtTransportista.Text + "</td></tr><tr><td>Caja: " + this.txtCaja.Text + "</td></tr><tr><td>Pedido: " + this.txtPedido.Text + "</td></tr><tr><td>Tipo: " + this.txtTipo.Text + "</td></tr>" + str2 + "<tr><td>Accion correctiva: " + text.ToUpper() + "</td></tr></table><p>Entrar al sistema de quejas</p><br /><h3>Enlace dentro de instalaciónes de Comercializadora GAB: " + str4 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str3 + "</h3>");
                }
            }
        }

        public void enviarcorreo(string correo, string cve_queja, string cuerpo)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add(correo);
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja de fumigacion no.: " + cve_queja;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = cuerpo;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.From = new MailAddress("sistemas@mrlucky.com.mx");
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential("sistemas", "sisgab");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "mail1.mrlucky.com.mx";
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No fue enviado el correo electrónico');", true);
            }
        }
    }
}