using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace queja
{
    public partial class _default : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session.Clear();

            //enviarcorreo_accion();
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            bool flag1 = con.palabrasreservadas(txtUsuario.Text);
            bool flag2 = con.palabrasreservadas(txtPassword.Text);
            if (flag1 || flag2)
            {
                this.Response.Write("<script>alert('Hay palabras no validas en los campos');</script>");
            }//"Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240"
            else
            {
                
                string str1 = con.validarusuario(txtUsuario.Text, txtPassword.Text.ToUpper());
                if (str1 == "")
                {
                    this.lblDanger.Visible = true;
                    this.txtUsuario.Text = "";
                    this.txtPassword.Text = "";
                }
                else
                {
                    string[] strArray = str1.Split('-');
                    string usuario = strArray[0];
                    string str2 = strArray[1];
                    string str3 = strArray[2].ToString();
                    string str4 = strArray[3].ToString();
                    string str5 = strArray[4];
                    Session["clave"] = (object)usuario;
                    Session["nombre"] = (object)str2;
                    Session["cedis"] = (object)str3;
                    Session["admin"] = (object)str4;
                    Session["nivel"] = (object)str5;
                    IPHostEntry ipHostEntry = new IPHostEntry();
                    IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    //Convert.ToString((object)hostEntry.AddressList[hostEntry.AddressList.Length - 2]);
                    string fecha_reg = "";
                    string f_reg = "";
                    //try
                    //{
                    fecha_reg = DateTime.Now.ToString();
                    f_reg = Convert.ToDateTime(fecha_reg).ToString("dd/MM/yyyy HH:mm:ss");
                    this.con.registro_movimiento(f_reg, Convert.ToString(hostEntry.HostName), usuario, "A", "", "", "ACCESO SISTEMA", "QUEJAS");
                    //}
                    //catch
                    //{
                    //    fecha_reg = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    //    f_reg = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    //    this.con.registro_movimiento(f_reg, Convert.ToString(hostEntry.HostName), usuario, "A", "", "", "ACCESO SISTEMA", "QUEJAS");
                    //}
                    //enviarcorreo_accion();
                    switch (ddlTipo.SelectedValue.ToString())
                    {
                        case "0":
                            Response.Redirect("quejas.aspx");
                            break;
                        case "1":
                            Response.Redirect("quejas_serv.aspx");
                            break;
                        case "2":
                            Response.Redirect("quejas_fumigacion.aspx");
                            break;
                    }
                }
            }
        }

        public void enviarcorreo_accion()
        {
            string str = "<table border='2'><tr><td align='center'><h2>Prueba de correo</h2></td></tr></table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + "http://gabira1:81/quejas/" + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + "http://189.206.160.206:81/quejas/";
            MailMessage message = new MailMessage();
            message.To.Add("dmunoz@mrlucky.com.mx");
            message.CC.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Prueba";
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = str;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.From = new MailAddress("aescamilla@mrlucky.com.mx");
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential("aescamilla", "atrejo");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "mail1.mrlucky.com.mx";
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                this.Response.Write("<script>alert('No fue enviado el correo electronico')</script>");
            }
        }
    }
}