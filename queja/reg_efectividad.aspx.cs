using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class reg_efectividad : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtMstrQueja = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        private DataTable dtInvestigador = new DataTable();
        private DataTable dtAcciones = new DataTable();
        private DataTable dtVerificacion = new DataTable();
        private DataTable dtCausas = new DataTable();
        private DataTable dtCorreos = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            string str1 = "";
            string str2 = "";
            this.dtMstrQueja = this.con.consultaqueja(this.Session["queja"].ToString());
            string str3 = this.dtMstrQueja.Rows[0]["que_folio"].ToString();
            string shortDateString = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
            string str4 = this.dtMstrQueja.Rows[0]["que_cliente"].ToString();
            string str5 = this.dtMstrQueja.Rows[0]["recibio"].ToString();
            string str6 = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
            string str7 = this.dtMstrQueja.Rows[0]["que_sucursal"].ToString();
            this.dtDetQueja = this.con.consultadetqueja_pedido(this.Session["queja"].ToString());
            foreach (DataRow row in (InternalDataCollectionBase)this.dtDetQueja.Rows)
                str1 = str1 + "<tr><td>" + row["nom_producto"].ToString() + "</td><td>" + row["problema"].ToString() + "</td><td>" + row["qud_ordprod"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["fecha_cad"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["qud_cantreci"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["qud_unidad"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + (row["qud_devolucion"].ToString() == "1" ? "SI" : "NO") + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["qud_moneda"].ToString() + "</td></tr>";
            this.dtInvestigador = this.con.reporte_investigadores(this.Session["queja"].ToString());
            foreach (DataRow row1 in (InternalDataCollectionBase)this.dtInvestigador.Rows)
            {
                this.dtCausas = this.con.reporte_causas(this.Session["queja"].ToString(), row1["inv_folio"].ToString());
                str2 = str2 + "<tr><td><table class='table table-bordered' width='100%'><tr><th>RESPONSABLE</th><th>COMENTARIO</th></tr><tr><td><h4>&#8226;" + row1["resp_nombre"].ToString() + "</h4></td><td><h4>" + row1["inv_comentario"].ToString() + "</h4></td></tr>";
                str2 += "<tr><td colspan='2' align='center'><table border='1'>";
                foreach (DataRow row2 in (InternalDataCollectionBase)this.dtCausas.Rows)
                {
                    str2 += "<tr><td><table class='table table-bordered'><tr><td colspan='2' align='center' class='color'><b>RESPONSABLE ACCIONES CORRECTIVAS</b></td></tr><tr><td><b>NOMBRE</b></td><td><b>CAUSA</b></td></tr>";
                    str2 = str2 + "<tr><td><h5>" + row2["resp_nombre"].ToString() + "</h5></td><td><h5>" + row2["acc_causa"].ToString() + "</h5></td></tr><tr><td colspan='2' align='center'> <table class='table table-bordered'><tr>";
                    this.dtAcciones = this.con.reporte_acciones(this.Session["queja"].ToString(), row2["acc_responsable"].ToString(), row2["acc_folio"].ToString());
                    foreach (DataRow row3 in (InternalDataCollectionBase)this.dtAcciones.Rows)
                    {
                        str2 += "<td colspan='5' align='center'><b>ACCIONES CORRECTIVAS</b></td>";
                        str2 += "<tr><td><b><u>ACCION</u></b></td><td><b><u>FECHA TERMINO</b></td><td><b><u>VERIFICACION</u></b></td><td><b><u>COMENTARIO</u></b></td><td><b><u>FECHA</u></b></td></tr>";
                        str2 = str2 + "<tr><td>" + row3["acc_accion"] + "</td><td>" + row3["acc_fechatermino"] + "</td><td>" + row3["acc_cumpli_ver"] + "</td><td>" + row3["acc_comen_ver"] + "</td><td>" + row3["acc_fecha_ver"] + "</td></tr><tr><td colspan='5' align='center'><img src='fotos_des/1_" + this.Session["queja"].ToString() + "_" + row3["acc_clave"] + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'><img src='fotos_des/2_" + this.Session["queja"].ToString() + "_" + row3["acc_clave"] + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'><img src='fotos_des/3_" + this.Session["queja"].ToString() + "_" + row3["acc_clave"] + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></td></tr><tr><td colspan='5' align='center'>";
                        string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_des/"));
                        List<ListItem> listItemList = new List<ListItem>();
                        foreach (string path in files)
                        {
                            if (Path.GetFileName(path).Contains("_" + this.Session["queja"].ToString() + "_" + row3["acc_clave"] + ".pdf"))
                                listItemList.Add(new ListItem(Path.GetFileName(path), path));
                        }
                        foreach (ListItem listItem in listItemList)
                            str2 = str2 + "<a href='fotos_des/" + listItem.ToString() + "' class='btn btn-primary' role='button' target='_blank'>" + listItem.ToString() + "</a>";
                        str2 += "</td></tr>";
                    }
                    str2 += "</tr></table></td></tr></table></td></tr>";
                }
                str2 += "</table></td></tr>";
                str2 += "</table>";
            }
            string str8 = str2 + "</tr>";
            string str9 = "<table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td align='center' colspan='4'><b>DESCRIPCIÓN DE LA QUEJA</b></td></tr><tr><td>Folio:</td><td>" + str3 + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>Fecha de la queja:</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + shortDateString + "</td></tr><tr><td>Cliente:</td><td>" + str4 + "</td></tr><tr><td>Queja recibida por:</td><td>" + str5 + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>Tipo de cliente:</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + str7 + "</td></tr><tr><td>Queja reportada por:</td><td>" + str6 + "</td></tr></table><br /><table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='8' align='center'><b>DESCRIPCI&Oacute;N DEL PROBLEMA</b></td></tr><tr><th>PRODUCTO</th><th>PROBLEMA</th><th>ORDEN PROD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>FECHA CAD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>CANTIDAD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>UNIDAD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>DEVOLUCI&Oacute;N</th><th class='visible-lg visible-md hidden-sm hidden-xs'>MONEDA</th></tr><tr>" + str1 + "</tr></table><br /> <table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='3' align='center'><b>IMAGENES</b></td></tr><tr><td align='center'><img src='fotos_ant/1_" + this.Session["queja"].ToString() + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></td><td align='center'><img src='fotos_ant/2_" + this.Session["queja"].ToString() + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></td><td align='center'><img src='fotos_ant/3_" + this.Session["queja"].ToString() + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></td></tr><tr><td colspan='3' align='center'>";
            this.Image1.ImageUrl = "fotos_ant/2_" + this.Session["queja"].ToString() + ".jpg";
            this.Image2.ImageUrl = "fotos_ant/2_" + this.Session["queja"].ToString() + ".jpg";
            this.Image3.ImageUrl = "fotos_ant/3_" + this.Session["queja"].ToString() + ".jpg";
            string str10 = "";
            string[] files1 = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"));
            List<ListItem> listItemList1 = new List<ListItem>();
            foreach (string path in files1)
            {
                if (Path.GetFileName(path).Contains("_" + this.Session["queja"].ToString() + ".pdf"))
                    listItemList1.Add(new ListItem(Path.GetFileName(path), path));
            }
            foreach (ListItem listItem in listItemList1)
                str10 = str10 + "<a href='fotos_ant/" + listItem.ToString() + "' class='btn btn-primary' role='button' target='_blank'>" + listItem.ToString() + "</a>";
            this.datosqueja.InnerHtml = str9 + str10 + "</td></tr></table><br /><table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='2' align='center'><b>INVESTIGACI&Oacute;N DE LAS CAUSAS</b></td></tr>" + str8 + "</table>";
            string fechaefe = DateTime.Now.ToShortDateString();
            this.txtFechaEfe.Text = Convert.ToDateTime(fechaefe).ToString("dd/MM/yyyy");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = this.Session["cliente"];
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("contenido.aspx");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.ddlCumplimiento.SelectedValue = "";
            this.txtComentario.Text = "";
            this.ddlCumplimiento.Enabled = false;
            this.txtComentario.Enabled = false;
            this.updEfectividad.Update();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.con.insertaefectividad(this.lblQueja.Text, this.txtFechaEfe.Text, this.ddlCumplimiento.SelectedValue.ToString().ToUpper(), this.txtComentario.Text.ToUpper()) == "1")
            {
                this.lblSuccess.Visible = true;
                if (this.con.cierre_queja_verificacion(this.lblQueja.Text) == "1")
                {
                    this.con.cierre_queja2(this.lblQueja.Text);
                    this.dtCorreos = this.con.correos_todos(this.lblQueja.Text);
                    this.correo(this.lblQueja.Text, this.dtCorreos);
                }
                this.con.etapa_efectividad(this.lblQueja.Text);
                this.ddlCumplimiento.SelectedValue = "";
                this.txtComentario.Text = "";
                this.updEfectividad.Update();
            }
            else
                this.lblWarning.Visible = true;
        }

        protected void correo(string queja, DataTable correos)
        {
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string d_a_t_e = DateTime.Now.ToString();
            string d_a_t_e_2 = Convert.ToDateTime(d_a_t_e).ToString("dd/MM/yyyy");
            string str3 = "<table border='2'><tr><td align='center'><h2>Cierre de Queja</h2></td></tr><tr><td>Queja no.: " + queja + "</td></tr><tr><td>Fecha de cierre: " + d_a_t_e_2 + "</td></tr></table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            MailMessage message = new MailMessage();
            message.To.Add("msamano@mrlucky.com.mx");
            foreach (DataRow row in (InternalDataCollectionBase)this.dtCorreos.Rows)
                message.CC.Add(row["correo"].ToString());
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
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
    }
}