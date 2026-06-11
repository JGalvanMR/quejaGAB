using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;

namespace queja
{
    public partial class investigadores : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtResponsables = new DataTable();
        private DataTable dtInvestigadores = new DataTable();
        private DataTable dtMstrQueja = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        private string folio = "";
        private string causa = "";
        private string fecha = "";
        private string fecha_carga_investigador = "";
        private DataTable dtDetQuejaPed = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.fecha_carga_investigador = this.Session["fecha_carga_investigador"].ToString();
            DataTable dataTable = new DataTable();
            this.lblFecha_Carga_Inv.Text = this.fecha_carga_investigador;
            if (!Page.IsPostBack)
            {
                this.folio = this.lblQueja.Text;
                this.dtResponsables = this.con.cargaresponsables();
                this.ddlResp1.DataSource = (object)this.dtResponsables;
                this.ddlResp1.DataTextField = "resp_nombre";
                this.ddlResp1.DataValueField = "resp_clave";
                this.ddlResp1.DataBind();
                this.dtInvestigadores = this.con.consultainvestigadores(this.folio);
                this.gvwResponsables.DataSource = (object)this.dtInvestigadores;
                this.gvwResponsables.DataBind();
                this.dtMstrQueja = this.con.consultaqueja(this.folio);
                this.txtFolio.Text = this.dtMstrQueja.Rows[0]["que_folio"].ToString();
                this.txtSemana.Text = this.dtMstrQueja.Rows[0]["que_semana"].ToString();
                this.txtMes.Text = this.dtMstrQueja.Rows[0]["que_mes"].ToString();
                this.txtFecha.Text = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
                this.txtCliente.Text = this.dtMstrQueja.Rows[0]["que_cliente"].ToString();
                this.txtSucursal.Text = this.dtMstrQueja.Rows[0]["que_sucursal"].ToString();
                this.txtReporto.Text = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
                this.txtRecibio.Text = this.dtMstrQueja.Rows[0]["recibio"].ToString();
                this.txtTipo.Text = this.dtMstrQueja.Rows[0]["recibio"].ToString() == "N" ? "NACIONAL" : "EXPORTACION";
                this.txtAreaGral.Text = this.dtMstrQueja.Rows[0]["area_nombre"].ToString();
                this.dtDetQueja = this.con.consultadetqueja(this.folio);
                this.txtOrdenProd.Text = this.dtDetQueja.Rows[0]["qud_ordprod"].ToString();
                this.txt_clave.Text = this.dtDetQueja.Rows[0]["id_producto"].ToString();
                this.txt_nom.Text = this.dtDetQueja.Rows[0]["nom_producto"].ToString();
                this.txt_lote.Text = this.dtDetQueja.Rows[0]["qud_lote"].ToString();
                this.txt_fechacad.Text = this.dtDetQueja.Rows[0]["fecha_cad"].ToString();
                this.txt_cveprov.Text = this.dtDetQueja.Rows[0]["prov_clave"].ToString();
                this.txt_nomprov.Text = this.dtDetQueja.Rows[0]["prov_nombre"].ToString();
                this.txt_cverch.Text = this.dtDetQueja.Rows[0]["rch_clave"].ToString();
                this.txt_nomrch.Text = this.dtDetQueja.Rows[0]["rch_nombre"].ToString();
                this.txt_cvetbl.Text = this.dtDetQueja.Rows[0]["tbl_clave"].ToString();
                this.txt_nomtbl.Text = this.dtDetQueja.Rows[0]["tbl_nombre"].ToString();
                this.txtRespLinea.Text = this.dtDetQueja.Rows[0]["qud_responsable"].ToString();
                this.txtArea.Text = this.dtDetQueja.Rows[0]["qud_area"].ToString();
                this.txtCantRech.Text = this.dtDetQueja.Rows[0]["qud_cantrecha"].ToString();
                this.txtCantReci.Text = this.dtDetQueja.Rows[0]["qud_cantreci"].ToString();
                this.txtUnidad.Text = this.dtDetQueja.Rows[0]["qud_unidad"].ToString();
                this.chkDevolucion.Checked = this.dtDetQueja.Rows[0]["qud_devolucion"].ToString() == "1";
                this.txtMoneda.Text = this.dtDetQueja.Rows[0]["qud_moneda"].ToString();
                this.txtProblema.Text = this.dtDetQueja.Rows[0]["problema"].ToString();
                this.txtTarima.Text = this.dtDetQueja.Rows[0]["qud_tarima"].ToString();
                this.dtDetQuejaPed = this.con.consulta_productos_exp(this.folio);
                this.grvDetalle.DataSource = (object)this.dtDetQuejaPed;
                this.grvDetalle.DataBind();
                string str = "fotos_ant/1_" + this.folio + ".jpg";
                this.img1.ImageUrl = str;
                this.img2.ImageUrl = "fotos_ant/2_" + this.folio + ".jpg";
                this.img3.ImageUrl = "fotos_ant/3_" + this.folio + ".jpg";
                this.Image1.ImageUrl = str;
                this.Image2.ImageUrl = "fotos_ant/2_" + this.folio + ".jpg";
                this.Image3.ImageUrl = "fotos_ant/3_" + this.folio + ".jpg";
            }
            TextBox txtFecha = this.txtFecha;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(2);
            string str1 = dateTime.ToString("dd/MM/yyyy");
            txtFecha.Text = str1;

            //<!-- asp:Timer ID="Timer1" runat="server" Interval="3000" ontick="Timer1_Tick"></asp:Timer -->
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!(this.ddlResp1.SelectedValue != "0"))
                return;
            if (this.lblOperacion.Text != "-")
            {
                if (this.con.modificainvestigador(this.lblOperacion.Text, this.ddlResp1.SelectedValue.ToString(), this.txtFecha.Text, this.txtCausa.Text.ToUpper()) == "1")
                {
                    this.lblSuccess.Visible = true;
                    this.folio = this.lblQueja.Text;
                    this.ddlResp1.SelectedValue = "0";
                    this.ddlResp1.Enabled = true;
                    this.uplResp1.Update();
                    this.txtCausa.Text = "";
                    this.uplCausa.Update();
                    this.dtInvestigadores = this.con.consultainvestigadores(this.folio);
                    this.gvwResponsables.DataSource = (object)this.dtInvestigadores;
                    this.gvwResponsables.DataBind();
                    this.upnResponsables.Update();
                    this.lblOperacion.Text = "-";
                    this.upnOperacion.Update();
                }
                else
                    this.lblWarning.Visible = true;
            }
            else
            {
                conectasql con = this.con;
                string text1 = this.lblQueja.Text;
                string responsable = this.ddlResp1.SelectedValue.ToString();
                string now = DateTime.Now.ToString();
                //DateTime now = DateTime.Now;
                //con.correo_error("llega 1");
                string shortDateString = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
                string text2 = this.txtFecha.Text;
                string upper = this.txtCausa.Text.ToUpper();
                if (con.insertinvestigacion(text1, responsable, shortDateString, text2, upper) == "1")
                {
                    this.enviarcorreo(this.con.consultacorreo(this.ddlResp1.SelectedValue), this.ddlResp1.SelectedValue, this.lblQueja.Text);
                    this.lblSuccess.Visible = true;
                    //now = DateTime.Now;

                    //con.correo_error("llega 2 " + shortDateString + " " + lblFecha_Carga_Inv.Text);
                    string f_1 = shortDateString;
                    //con.correo_error("llega 3 " + f_1);
                    string f_2 = lblFecha_Carga_Inv.Text;
                    //con.correo_error("llega 4 " + f_2);
                    //int result = DateTime.Compare(Convert.ToDateTime(f_1), Convert.ToDateTime(f_2));
                    //con.correo_error("llega resultado " + result.ToString());
                    //if (result <= 0)
                    //    this.con.actualizaetapa1(this.lblQueja.Text, "1");
                    //else
                    //    this.con.actualizaetapa1(this.lblQueja.Text, "2");

                    //try
                    //{
                        //DateTime d_1 = DateTime.ParseExact(f_1, "MM/dd/yyyy", null);
                        //DateTime d_2 = DateTime.ParseExact(f_2, "MM/dd/yyyy", null);
                        //if (Convert.ToDateTime(f_1) > Convert.ToDateTime(f_2))
                        //    this.con.actualizaetapa1(this.lblQueja.Text, "2");
                        //else
                    this.con.actualizaetapa1(this.lblQueja.Text, "1");
                    //}
                    //catch (Exception ex)
                    //{

                    //    con.correo_error(ex.ToString());
                    //}
                    

                    this.folio = this.lblQueja.Text;
                    this.ddlResp1.SelectedValue = "0";
                    this.uplResp1.Update();
                    this.txtCausa.Text = "";
                    this.uplCausa.Update();
                    
                    this.dtInvestigadores = this.con.consultainvestigadores(this.folio);
                    //con.correo_error("pasa");
                    this.gvwResponsables.DataSource = (object)this.dtInvestigadores;
                    this.gvwResponsables.DataBind();
                    this.upnResponsables.Update();
                }
                else
                    this.lblWarning.Visible = true;
            }
        }

        public void enviarcorreo(string correo, string responsable, string cve_queja)
        {
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            MailMessage message = new MailMessage();
            message.To.Add(correo);
            //message.CC.Add("aescamilla@mrlucky.com.mx");
            message.CC.Add("msamano@mrlucky.com.mx");
            message.Subject = "Queja no.: " + this.lblQueja.Text;
            message.SubjectEncoding = Encoding.UTF8;
            string str3 = "<table border='2'><tr><td align='center'><h2>Asignaci&oacute;n de investigaci&oacute;n</h2></td></tr><tr><td>Responsable: " + this.ddlResp1.SelectedItem.ToString() + "</td></tr><tr><td>Fecha registro de causas: " + this.txtFecha.Text + "</td></tr><tr><td>Queja: No. " + this.lblQueja.Text + "</td></tr><tr><td>Clave y nombre producto: " + this.txt_clave.Text + " " + this.txt_nom.Text + "</td></tr><tr><td>Problema: " + this.txtProblema.Text + "</td></tr><tr><td>Comentario: " + this.txtCausa.Text.ToUpper() + "</td></tr></table><br /><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
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
            this.Session["cliente"] = this.Session["cliente"];
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("contenido.aspx");
        }

        protected void gvwResponsables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "muestradatos")
            {
                GridViewRow row = this.gvwResponsables.Rows[Convert.ToInt32(e.CommandArgument)];
                string str1 = this.Server.HtmlDecode(row.Cells[0].Text);
                string str2 = this.Server.HtmlDecode(row.Cells[1].Text);
                string str3 = this.Server.HtmlDecode(row.Cells[3].Text);
                string str4 = this.Server.HtmlDecode(row.Cells[4].Text);
                this.ddlResp1.SelectedValue = str2;
                this.ddlResp1.Enabled = false;
                uplResp1.Update();
                this.txtFecha.Text = str4;
                this.txtCausa.Text = str3;
                this.upnDatos.Update();
                this.lblOperacion.Text = str1;
                this.upnOperacion.Update();
                this.btnCancelar.Visible = true;
                this.upnCancelar.Update();
            }
        }

        protected void gvwResponsables_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string text = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[6].Controls.OfType<Button>())
            {
                if (button.CommandName == "Delete")
                    button.Attributes["onclick"] = "if(!confirm('Quieres borrar el investigador')){ return false;};";
            }

        }

        protected void gvwResponsables_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //VALIDAR QUE NO HAYA SIDO CARGADA LA ACCION CORRECTIVA PARA MSTR_ACCIONES 
            string viema = this.con.verifica_investigador_en_mstr_acciones(this.lblQueja.Text, this.Server.HtmlDecode(this.gvwResponsables.Rows[Convert.ToInt32(e.RowIndex)].Cells[0].Text));
            if (viema != "0")
            {
                if (viema == "X")
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Se detecto un error al verificar si ya existe accion para el registro, intente nuevamente');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No se puede eliminar porque ya tiene registro de accion o acciones correctivas');", true);
                    return;
                }
                
            }
            else
            {
                if (this.con.borrainvestigador(this.Server.HtmlDecode(this.gvwResponsables.Rows[Convert.ToInt32(e.RowIndex)].Cells[0].Text)) == "1")
                {
                    this.lblSuccessInv.Visible = true;
                    this.dtInvestigadores = this.con.consultainvestigadores(this.lblQueja.Text);
                    con.correo_error("renglones: " + dtInvestigadores.Rows.Count.ToString());
                    foreach (DataRow rt in dtInvestigadores.Rows)
                    {
                        con.correo_error(rt[0].ToString() + " " + rt[1].ToString() + " " + rt[2].ToString() + " " + rt[3].ToString());
                    }
                    //gvwResponsables.DeleteRow(e.RowIndex);
                    //Page_Load(sender, e);
                    Response.Redirect(Request.RawUrl, true);
                    //this.gvwResponsables.DataSource = dtInvestigadores;
                    //this.gvwResponsables.DataBind();
                    //this.upnResponsables.Update();


                }
                else
                    this.lblWarningInv.Visible = true;
            }

            
        }




        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.ddlResp1.SelectedValue = "0";
            this.ddlResp1.Enabled = true;
            this.uplResp1.Update();
            this.txtCausa.Text = "";
            this.uplCausa.Update();
            this.lblOperacion.Text = "-";
            this.upnOperacion.Update();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.lblSuccess.Visible = false;
            this.lblSuccessInv.Visible = false;
            this.lblWarning.Visible = false;
            this.lblWarningInv.Visible = false;
        }
    }
}