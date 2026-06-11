using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace queja
{
    public partial class notas_credito : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtDetQueja = new DataTable();
        private DataTable dtCausas = new DataTable();
        private DataTable dtPedido = new DataTable();
        private DataTable dtFacturas = new DataTable();
        private DataTable dtSucursal = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();

            if (this.Page.IsPostBack)
                return;
            this.dtDetQueja = this.con.notas_credito_productos(this.lblQueja.Text);
            this.grvDetalle.DataSource = (object)this.dtDetQueja;
            this.grvDetalle.DataBind();

            this.dtCausas = this.con.notas_credito_causas(this.lblQueja.Text);
            this.grvCausas.DataSource = (object)this.dtCausas;
            this.grvCausas.DataBind();

            this.dtPedido = this.con.notas_credito_pedido(this.lblQueja.Text, dtDetQueja.Rows[0][6].ToString());
            this.grvPedido.DataSource = (object)this.dtPedido;
            this.grvPedido.DataBind();

            this.dtFacturas = this.con.notas_credito_facturas(this.lblQueja.Text);
            //this.grvFacturas.DataSource = (object)this.dtFacturas;
            //this.grvFacturas.DataBind();

            txtProblema.Text = dtDetQueja.Rows[0][5].ToString();
            txtTipo.Text = ((dtDetQueja.Rows[0][7].ToString() == "1") ? "DEVOLUCION" : (dtDetQueja.Rows[0][8].ToString() == "1") ? "BONIFICACION" : "");

            this.Session["tipo"] = dtDetQueja.Rows[0][6].ToString();

            //verificar si ya se confirmo o no procedio la nc
            string nc_no_procede = this.con.notas_credito_queja_NC_O_NO_PROCEDE(this.lblQueja.Text);
            if (nc_no_procede == "1" || nc_no_procede == "2")
            {
                btnGuardar.Enabled = false;
                btnNoProcede.Enabled = false;
                upnBotones.Update();
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nota_credito = con.notas_credito_queja(lblQueja.Text, lblClave.Text);
            if (nota_credito == "1")//mandar correo electrónicos
            {
                string tipo = (string)Session["tipo"];
                this.dtDetQueja = this.con.notas_credito_productos(this.lblQueja.Text);
                this.dtCausas = this.con.notas_credito_causas(this.lblQueja.Text);
                this.dtPedido = this.con.notas_credito_pedido(this.lblQueja.Text, tipo);
                string CORREO = dtPedido.Rows[0]["usu_email"].ToString();
                this.dtFacturas = this.con.notas_credito_facturas(this.lblQueja.Text);
                string tipo_queja = this.con.notas_credito_tipo_queja(this.lblQueja.Text);


                string tabla = "<table border='2'>" +
                    "<tr><td colspan='5' align='center' bgcolor='#2196F3'><b><h2>Confirmaci&oacute;n de Nota de Cr&eacute;dito " + txtTipo.Text + "</h2></b></td></tr>" +
                    "<tr><td><b><h3>Queja:</h3></b></td><td colspan='4'><b><h3>" + lblQueja.Text + "</h3></b></td></tr>" +
                    "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Clave producto</b></td><td bgcolor='#E0E0E0'><b>Producto</b></td><td bgcolor='#E0E0E0'><b>Orden Prod</b></td><td bgcolor='#E0E0E0'><b>Recibidas</b></td><td bgcolor='#E0E0E0'><b>Rechazado</b></td></tr>";
                foreach (DataRow r1 in dtDetQueja.Rows)
                {
                    tabla += "<tr><td>" + r1["id_producto"].ToString() + "</td><td>" + r1["nom_producto"].ToString() + "</td><td align='center'>" + r1["qud_ordprod"].ToString() + "</td>" +
                        "<td align='center'>" + r1["qud_cantreci"].ToString() + "</td><td align='center'>" + r1["qud_cantrecha"].ToString() + "</td></tr>";
                }
                tabla += "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Problema:</b></td><td colspan='4'>" + txtProblema.Text + "</td></tr>" +
                    "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Responsable</b></td><td colspan='4' bgcolor='#E0E0E0'><b>Comentario / Causa</b></td></tr>";
                foreach (DataRow r1 in dtCausas.Rows)
                {
                    tabla += "<tr><td>" + r1["resp_nombre"].ToString() + "</td><td colspan='4'>" + r1["inv_comentario"].ToString() + "</td></tr>";
                }
                tabla += "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Ped o Fact</b></td><td colspan='4' bgcolor='#E0E0E0'><b>Realiz&oacute;</b></td></tr>";
                foreach (DataRow r1 in dtPedido.Rows)
                {
                    tabla += "<tr><td>" + r1["qud_pedido"].ToString() + "</td><td colspan='4'>" + r1["usu_nombre"].ToString() + "</td></tr>";
                }

                if (tipo_queja == "E")
                {
                    tabla += "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Facturas</b></td><td bgcolor='#E0E0E0' colspan='2'><b>Subcliente</b></td><td bgcolor='#E0E0E0'><b>Costo</b></td><td bgcolor='#E0E0E0'><b>Estatus</b></td></tr>";
                    foreach (DataRow r1 in dtFacturas.Rows)
                    {
                        //if(i == 0)
                        //    tabla += "<tr><td>" + r1["facturas"].ToString() + "</td><td colspan='3' rowspan='" + dtFacturas.Rows.Count.ToString() + "'>" + r1["subcliente"].ToString() + "</td>" +
                        //        "<td rowspan='" + dtFacturas.Rows.Count.ToString() + "'>" + Convert.ToDecimal(r1["costo"].ToString()).ToString("$###,###,##0.00") + "</td></tr>";
                        //else
                        //r1["sufijo"].ToString()

                        string cos_fact = this.con.notas_credito_costo(lblQueja.Text, r1["facturas"].ToString());

                        tabla += "<tr><td>" + r1["facturas"].ToString() + "</td><td colspan='2'>" + r1["subcliente"].ToString() + "</td>" +
                            "<td align='right'>" + Convert.ToDecimal(cos_fact).ToString("$###,###,##0.00") + "</td><td>CONFIRMADO</td></tr>";
                        //i++;

                    }
                }
                
                correo(lblQueja.Text, tabla, CORREO);

                btnGuardar.Enabled = false;
                btnNoProcede.Enabled = false;
                upnBotones.Update();
            }
        }

        protected void correo(string queja, string tabla, string email)
        {
            //correo quien realizo la queja
            string str0 = con.correo_quien_hizo_queja(queja);
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string date = DateTime.Now.ToString();
            string date2 = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            string str3 = tabla;// "<table border='2'><tr><td align='center'><h2>Cierre de Queja</h2></td></tr><tr><td>Queja no.: " + queja + "</td></tr><tr><td>Cliente: " + clien + "</td></tr><tr><td>Fecha de cierre: " + date2 + "</td></tr></table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            MailMessage message = new MailMessage();
            //message.To.Add("aescamilla@mrlucky.com.mx");
            message.To.Add(email);
            message.CC.Add("msamano@mrlucky.com.mx");
            if (str0 != "0")
                message.CC.Add(str0);
            message.CC.Add("auditoria@mrlucky.com.mx");
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Bcc.Add("ricardo.cortes@mrlucky.com.mx");
            message.Subject = "Queja no.: " + queja;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = str3;
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
                this.Response.Write("<script>alert('No fue enviado el correo electronico')</script>");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = this.Session["cliente"].ToString();
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("contenido.aspx");
        }

        protected void btnNoProcede_Click(object sender, EventArgs e)
        {
            string nota_credito = con.notas_credito_queja_NO_PROCEDE(lblQueja.Text, lblClave.Text);
            if (nota_credito == "2")//mandar correo electrónicos
            {
                string tipo = (string)Session["tipo"];
                this.dtDetQueja = this.con.notas_credito_productos(this.lblQueja.Text);
                this.dtCausas = this.con.notas_credito_causas(this.lblQueja.Text);
                this.dtPedido = this.con.notas_credito_pedido(this.lblQueja.Text, tipo);
                string CORREO = dtPedido.Rows[0]["usu_email"].ToString();
                this.dtFacturas = this.con.notas_credito_facturas(this.lblQueja.Text);
                string tipo_queja = this.con.notas_credito_tipo_queja(this.lblQueja.Text);

                string tabla = "<table border='2'>" +
                    "<tr><td colspan='5' align='center' bgcolor='#2196F3'><b><h2>NO PROCEDE NOTA DE CR&Eacute;DITO " + txtTipo.Text + "</h2></b></td></tr>" +
                    "<tr><td><b><h3>Queja:</h3></b></td><td colspan='4'><b><h3>" + lblQueja.Text + "</h3></b></td></tr>" +
                    "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Clave producto</b></td><td bgcolor='#E0E0E0'><b>Producto</b></td><td bgcolor='#E0E0E0'><b>Orden Prod</b></td><td bgcolor='#E0E0E0'><b>Recibidas</b></td><td bgcolor='#E0E0E0'><b>Rechazado</b></td></tr>";
                foreach (DataRow r1 in dtDetQueja.Rows)
                {
                    tabla += "<tr><td>" + r1["id_producto"].ToString() + "</td><td>" + r1["nom_producto"].ToString() + "</td><td align='center'>" + r1["qud_ordprod"].ToString() + "</td>" +
                        "<td align='center'>" + r1["qud_cantreci"].ToString() + "</td><td align='center'>" + r1["qud_cantrecha"].ToString() + "</td></tr>";
                }
                tabla += "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Problema:</b></td><td colspan='4'>" + txtProblema.Text + "</td></tr>" +
                    "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Responsable</b></td><td colspan='4' bgcolor='#E0E0E0'><b>Comentario / Causa</b></td></tr>";
                foreach (DataRow r1 in dtCausas.Rows)
                {
                    tabla += "<tr><td>" + r1["resp_nombre"].ToString() + "</td><td colspan='4'>" + r1["inv_comentario"].ToString() + "</td></tr>";
                }
                tabla += "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Ped o Fact</b></td><td colspan='4' bgcolor='#E0E0E0'><b>Realiz&oacute;</b></td></tr>";
                foreach (DataRow r1 in dtPedido.Rows)
                {
                    tabla += "<tr><td>" + r1["qud_pedido"].ToString() + "</td><td colspan='4'>" + r1["usu_nombre"].ToString() + "</td></tr>";
                }

                if (tipo_queja == "E")
                {
                    tabla += "<tr><td colspan='5' bgcolor='#FFE082'>&nbsp;</td></tr>" +
                    "<tr><td bgcolor='#E0E0E0'><b>Facturas</b></td><td bgcolor='#E0E0E0' colspan='2'><b>Subcliente</b></td><td bgcolor='#E0E0E0'><b>Costo</b></td><td bgcolor='#E0E0E0'><b>Estatus</b></td></tr>";
                    foreach (DataRow r1 in dtFacturas.Rows)
                    {
                        tabla += "<tr><td>" + r1["facturas"].ToString() + "</td><td colspan='2'>" + r1["subcliente"].ToString() + "</td>" +
                            "<td>" + Convert.ToDecimal(r1["costo"].ToString()).ToString("$###,###,##0.00") + "</td><td>NO CONFIRMADO</td></tr>";

                    }
                }
                
                correo(lblQueja.Text, tabla, CORREO);

                //deshabilitar botones
                btnGuardar.Enabled = false;
                btnNoProcede.Enabled = false;
                upnBotones.Update();
            }
        }
    }
}