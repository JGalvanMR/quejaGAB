using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Data;

namespace queja
{
    public partial class accioncorrectiva : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtAcciones = new DataTable();
        private DataTable dtAgregar = new DataTable();
        private DataTable dtAccionesCausa = new DataTable();
        private DataTable dtAccionesAcc = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        private string folio = "";
        private string fecha_carga_accion = "";
        private string investigador = "";
        private string correo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.RegisterRedirectOnSessionEndScript();
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.Session["correo"] = this.Session["correo"];
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.dtAgregar.Columns.Add("accion", typeof(string));
            this.dtAgregar.Columns.Add("fecha", typeof(string));
            if (this.Page.IsPostBack)
                return;
            this.folio = this.lblQueja.Text;
            this.fecha_carga_accion = this.con.fecha_accion(this.folio, this.lblClave.Text);
            this.lblFecha_Carga_Accion.Text = this.fecha_carga_accion;
            string[] strArray1 = this.con.datosqueja(this.lblQueja.Text).Split('-');
            this.txtProducto.Text = strArray1[0];
            this.txtProblema.Text = strArray1[1];
            if (this.con.claveinvestigador(this.lblQueja.Text, this.lblClave.Text) != "")
                this.Session["correo"] = (object)"1";
            string str1 = "";
            if (this.Session["correo"] != null)
            {
                str1 = this.Session["correo"].ToString();
                if (this.fecha_carga_accion == "")
                {
                    this.fecha_carga_accion = this.con.fechaentregaacciones(this.lblQueja.Text, this.lblClave.Text);
                    this.lblFecha_Carga_Accion.Text = this.fecha_carga_accion;
                }
            }
            string str2 = "";
            if (str1 == "1")
            {
                string[] strArray2 = this.con.responsableinvestigacion(this.lblQueja.Text, this.lblClave.Text).Split('-');
                str2 = strArray2[0].ToString();
                this.investigador = strArray2[1].ToString();
                this.correo = strArray2[2].ToString();
                this.txtCausa.Text = this.con.consultacausa(this.folio, this.lblClave.Text);
            }
            else
                this.txtCausa.Text = this.con.consultacausa(this.folio, this.lblClave.Text);
            this.gvwAgregar.DataSource = (object)this.dtAgregar;
            this.gvwAgregar.DataBind();
            this.Session["datos"] = (object)this.dtAgregar;
            this.dtAccionesCausa = this.con.causas_responsables_acciones_ver(this.lblQueja.Text);
            this.gvwResponsable.DataSource = (object)this.dtAccionesCausa;
            this.gvwResponsable.DataBind();
            this.upnResponsables.Update();
            this.dtDetQueja = this.con.consulta_productos_exp(this.lblQueja.Text);
            this.grvDetalle.DataSource = (object)this.dtDetQueja;
            this.grvDetalle.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.lblClaveAccDet.Text == "-")
            {
                string text = this.lblFecha_Carga_Accion.Text;
                DataTable table = (DataTable)this.Session["datos"];
                string str = "";
                foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                {
                    str = this.con.actualizaacciones(this.lblQueja.Text, this.lblClaveInve.Text, row["fecha"].ToString(), row["accion"].ToString().ToUpper(), this.lblClaveResp.Text, DateTime.Now.ToString("dd/MM/yyyy"));
                    if (str == "0")
                        break;
                }
                if (str == "1")
                {
                    this.lblSuccess.Visible = true;
                    string[] strArray = this.con.correo_investigador_accion(this.lblClaveInve.Text, this.lblClaveResp.Text).Split('-');
                    this.enviarcorreo_accion(strArray[1].ToString(), strArray[0].ToString(), this.lblQueja.Text, table);
                    this.con.actualizaetapa3(this.lblQueja.Text, this.con.validacion_de_acciones_registradas(this.lblQueja.Text));
                    this.dtAccionesAcc = this.con.causas_responsables_acciones_acc(this.lblClaveInve.Text);
                    this.gvwAcciones.DataSource = (object)this.dtAccionesAcc;
                    this.gvwAcciones.DataBind();
                    this.upnAcciones.Update();
                    this.gvwAgregar.DataSource = (object)"";
                    this.gvwAgregar.DataBind();
                    this.Session["datos"] = (object)this.dtAgregar;
                    this.uplAgregar.Update();
                    table.Clear();
                    //IPHostEntry ipHostEntry = new IPHostEntry();
                    //IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    //Convert.ToString((object)hostEntry.AddressList[hostEntry.AddressList.Length - 2]);
                    //this.con.registro_movimiento(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Convert.ToString(hostEntry.HostName), this.lblClave.Text, "A", "", this.lblQueja.Text, "INSERCION DE ACCIONES", "QUEJAS");
                }
                else
                    this.lblWarning.Visible = true;
            }
            else if (this.con.modificaaccioncorrectiva(this.lblClaveAccDet.Text, this.txtAccion.Text, this.txtFechaEntrega.Text) == "1")
            {
                this.lblSuccess.Visible = true;
                this.dtAccionesAcc = this.con.causas_responsables_acciones_acc(this.lblClaveInve.Text);
                this.gvwAcciones.DataSource = (object)this.dtAccionesAcc;
                this.gvwAcciones.DataBind();
                this.upnAcciones.Update();
                this.lblClaveAccDet.Text = "-";
                this.upnClaveAccDet.Update();
                this.txtCausa.Text = "";
                this.upnDatos.Update();
                this.txtAccion.Text = "";
                this.txtFechaEntrega.Text = "";
                this.upnDatos.Update();
            }
            else
                this.lblWarning.Visible = true;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = this.Session["cliente"];
            this.Response.Redirect("contenido.aspx");
        }

        public void enviarcorreo(string correo, string investiga)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string str3 = "<table border='2'><tr><td align='center'><h2>Registro de Acciones Correctivas</h2></td></tr><tr><td>Registrado por: " + this.lblNombre.Text + "</td></tr><tr><td>Fecha:</td></tr><tr><td>Queja no.: " + this.lblQueja.Text + "</td></tr><tr><td>Producto: " + this.txtProducto.Text + "</td></tr><tr><td>Investigador: " + investiga + "</td></tr><tr><td>Problema: " + this.txtProblema.Text + "</td></tr><tr><td>Causa: " + this.txtCausa.Text.ToUpper() + "</td></tr><tr><td>Accion: " + this.txtAccion.Text.ToUpper() + "</td></tr><tr><td>Fecha entrega: " + this.txtFechaEntrega.Text + "</td></tr></table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            MailMessage message = new MailMessage();
            message.To.Add(correo);
            message.Subject = "Queja no.: " + this.lblQueja.Text;
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

        public void enviarcorreo_accion(
          string correo,
          string responsable,
          string cve_queja,
          DataTable table)
        {
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string str3 = "";
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                str3 = str3 + "<tr><td>Acción: " + row["accion"].ToString() + " Fecha Entrega: " + row["fecha"].ToString() + "</td></tr>";
            string str4 = "<table border='2'><tr><td align='center'><h2>Registro de Acciones Correctivas</h2></td></tr><tr><td>Registrado por: " + this.lblNombre.Text + "</td></tr><tr><td>Fecha:</td></tr><tr><td>Queja no.: " + this.lblQueja.Text + "</td></tr><tr><td>Producto: " + this.txtProducto.Text + "</td></tr><tr><td>Problema: " + this.txtProblema.Text + "</td></tr><tr><td>Causa: " + this.txtCausa.Text.ToUpper() + "</td></tr>" + str3 + "</table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            MailMessage message = new MailMessage();
            message.To.Add(correo);
            message.CC.Add("msamano@mrlucky.com.mx");
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + cve_queja;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = str4;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lblClaveResp.Text == "-" && this.lblClaveInve.Text == "-" || (this.txtAccion.Text == "" || this.txtFechaEntrega.Text == ""))
                return;
            DataTable dataTable = (DataTable)this.Session["datos"];
            DataRow row = dataTable.NewRow();
            row["accion"] = (object)this.txtAccion.Text.ToUpper();
            row["fecha"] = (object)this.txtFechaEntrega.Text;
            dataTable.Rows.Add(row);
            this.gvwAgregar.DataSource = (object)dataTable;
            this.gvwAgregar.DataBind();
            this.Session["datos"] = (object)dataTable;
            this.uplAgregar.Update();
            this.txtAccion.Text = "";
            this.limpiacontroles.Update();
        }

        protected void gvwAgregar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "quitar"))
                return;
            int int32 = Convert.ToInt32(e.CommandArgument);
            DataTable dataTable = (DataTable)this.Session["datos"];
            dataTable.Rows.RemoveAt(int32);
            this.gvwAgregar.DataSource = (object)dataTable;
            this.gvwAgregar.DataBind();
            this.Session["datos"] = (object)dataTable;
            this.uplAgregar.Update();
        }

        protected void gvwResponsable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "muestradatos"))
                return;
            GridViewRow row = this.gvwResponsable.Rows[Convert.ToInt32(e.CommandArgument)];
            string folio = this.Server.HtmlDecode(row.Cells[0].Text);
            string str1 = this.Server.HtmlDecode(row.Cells[1].Text);
            string str2 = this.Server.HtmlDecode(row.Cells[3].Text);
            this.lblClaveInve.Text = folio;
            this.lblClaveResp.Text = str1;
            this.upnClaveInv.Update();
            this.txtCausa.Text = str2;
            this.upnDatos.Update();
            this.dtAccionesAcc = this.con.causas_responsables_acciones_acc(folio);
            this.gvwAcciones.DataSource = (object)this.dtAccionesAcc;
            this.gvwAcciones.DataBind();
            this.upnAcciones.Update();

            this.btnGuardar.Enabled = true;
            upnGuardar.Update();
        }

        protected void gvwAcciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "editar"))
                return;
            GridViewRow row = this.gvwAcciones.Rows[Convert.ToInt32(e.CommandArgument)];
            string str1 = this.Server.HtmlDecode(row.Cells[0].Text);//FOLIO DE LA ACCION
            string str2 = this.Server.HtmlDecode(row.Cells[3].Text);
            string str3 = this.Server.HtmlDecode(row.Cells[4].Text);
            this.txtAccion.Text = str2;
            this.txtFechaEntrega.Text = str3;
            this.limpiacontroles.Update();
            this.lblClaveAccDet.Text = str1;
            this.upnClaveAccDet.Update();
            this.btnAdd.Enabled = false;
            this.upnAdd.Update();
            this.btnCancelar.Visible = true;
            this.upnCancelar.Update();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.txtAccion.Text = "";
            this.txtFechaEntrega.Text = "";
            this.limpiacontroles.Update();
            this.lblClaveAccDet.Text = "-";
            this.upnClaveAccDet.Update();
            this.btnAdd.Enabled = true;
            this.upnAdd.Update();
            this.btnCancelar.Visible = false;
            this.upnCancelar.Update();
        }

        protected void gvwAcciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string text = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[6].Controls.OfType<Button>())
            {
                if (button.CommandName == "Delete")
                    button.Attributes["onclick"] = "if(!confirm('¿Desea borrar la accion correctiva?')){return false;};";
            }
        }

        protected void gvwAcciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string vaeda = this.con.verifica_acciones_en_det_acciones(this.lblQueja.Text, this.Server.HtmlDecode(this.gvwAcciones.Rows[Convert.ToInt32(e.RowIndex)].Cells[0].Text)); 
            if (vaeda != "")
            {
                if (vaeda == "X")
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Se detecto un error al buscar la verificacion de la accion, intente nuevamente');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No se puede eliminar porque la accion ya tiene verificacion');", true);
                    return;
                }
            }
            else
            {
                if (this.con.borraraccioncorrectiva(this.Server.HtmlDecode(this.gvwAcciones.Rows[Convert.ToInt32(e.RowIndex)].Cells[0].Text)) == "1")
                {
                    this.lblSuccessAcc.Visible = true;
                    this.dtAccionesAcc = this.con.causas_responsables_acciones_acc(this.lblClaveInve.Text);
                    this.gvwAcciones.DataSource = (object)this.dtAccionesAcc;
                    this.gvwAcciones.DataBind();
                    this.upnAcciones.Update();
                }
                else
                    this.lblWarningAcc.Visible = true;
            }
            
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //this.lblSuccess.Visible = false;
            //this.lblSuccessAcc.Visible = false;
            //this.lblWarning.Visible = false;
            //this.lblWarningAcc.Visible = false;
        }

    }
}