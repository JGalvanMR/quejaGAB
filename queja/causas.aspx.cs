using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class causas : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtInvestigador = new DataTable();
        private DataTable dtAgregar = new DataTable();
        private DataTable dtResponsables = new DataTable();
        private DataTable dtInvestigadores = new DataTable();
        private DataTable dtAccionesCausa = new DataTable();
        private DataTable dtOrdenes = new DataTable();
        private DataTable dtDatos = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        
        private DataTable dtMatPrima = new DataTable();
        private DataTable dtMatPrimaSelected = new DataTable();

        private string clave_producto = "";
        private string nombre_producto = "";
        private string orden_produccion = "";
        private string numero_tarima = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.dtAgregar.Columns.Add("clv", typeof(string));
            this.dtAgregar.Columns.Add("nombre", typeof(string));
            this.dtAgregar.Columns.Add("causa", typeof(string));
            if (this.Page.IsPostBack)
                return;
            this.gvwAgregar.DataSource = (object)this.dtAgregar;
            this.gvwAgregar.DataBind();
            this.Session["datos"] = (object)this.dtAgregar;
            this.dtResponsables = this.con.cargaresponsables2();
            this.ddlResp1.DataSource = (object)this.dtResponsables;
            this.ddlResp1.DataTextField = "resp_nombre";
            this.ddlResp1.DataValueField = "resp_clave";
            this.ddlResp1.DataBind();
            string[] strArray = this.con.datosqueja(this.lblQueja.Text).Split('-');
            this.txtProducto.Text = strArray[0];
            this.txtProblema.Text = strArray[1];
            this.dtInvestigadores = this.con.consultainvestigadores_causas(this.lblQueja.Text);
            this.gvwResponsables.DataSource = (object)this.dtInvestigadores;
            this.gvwResponsables.DataBind();
            this.dtDetQueja = this.con.consulta_productos_exp2(this.lblQueja.Text);
            this.grvDetalle.DataSource = (object)this.dtDetQueja;
            this.grvDetalle.DataBind();
            if (this.con.validacion_tarima(this.lblQueja.Text) == "0")
            {
                this.btnGuardar.Enabled = false;
                this.upnValidaTarima.Update();
                this.Session["clave"] = (object)this.lblClave.Text;
                this.Session["nombre"] = (object)this.lblNombre.Text;
                this.Session["cedis"] = (object)this.lblCedis.Text;
                this.Session["queja"] = (object)this.lblQueja.Text;
                this.Session["admin"] = (object)this.lblAdmin.Text;
                this.ClientScript.RegisterStartupScript(this.GetType(), "popup", "openChild();", true);
            }
            else
            {
                this.btnGuardar.Enabled = true;
                this.upnValidaTarima.Update();
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.lblOperacion.Text == "CONSULTA")
            {
                DateTime dateTime = DateTime.Now;
                dateTime = dateTime.AddDays(2.0);
                dateTime.ToString("dd/MM/yyyy");
                if (this.con.updateRespAccion(this.lblClaveAccion.Text, this.ddlResp1.SelectedValue.ToString(), this.txtCausa.Text.ToUpper()) == "1")
                {
                    this.lblSuccessMod.Visible = true;
                    this.dtAccionesCausa = this.con.causas_responsables_acciones(this.lblQueja.Text, this.lblClaveInve.Text);
                    this.gvwResponsable.DataSource = (object)this.dtAccionesCausa;
                    this.gvwResponsable.DataBind();
                    this.upnResponsables.Update();
                    this.gvwAgregar.DataSource = (object)"";
                    this.gvwAgregar.DataBind();
                    this.uplAgregar.Update();
                    this.txtCausa.Text = "";
                    this.ddlResp1.SelectedValue = "0";
                    this.upnControles.Update();
                    this.btnAdd.Enabled = true;
                    this.upnAdd.Update();
                    this.lblClaveAccion.Text = "-";
                    this.lblOperacion.Text = "-";
                    this.upnClaveAccion.Update();
                }
                else
                    this.lblWarning.Visible = false;
            }
            else
            {
                
                DataTable tabla = (DataTable)this.Session["datos"];
                if (tabla.Rows.Count == 0)
                    return;
                DateTime dateTime1 = DateTime.Now;
                dateTime1 = dateTime1.AddDays(2.0);

                string d_1 = dateTime1.Day.ToString();
                string m_1 = dateTime1.Month.ToString().PadLeft(2, '0');
                string a_1 = dateTime1.Year.ToString();

                string f_1 = d_1 + "/" + m_1 + "/" + a_1;

                if (this.con.insertcausa(this.lblQueja.Text, this.lblClave.Text, "CAPTURADO", f_1) == "1")
                {
                    foreach (DataRow row in (InternalDataCollectionBase)tabla.Rows)
                    {
                        conectasql con = this.con;
                        string text1 = this.lblQueja.Text;
                        string text2 = this.lblClave.Text;
                        string resp = row["clv"].ToString();
                        string causa = row["causa"].ToString();
                        string text3 = this.lblClaveInve.Text;
                        DateTime dateTime2 = DateTime.Now;
                        dateTime2 = dateTime2.AddDays(2);

                        string d_2 = dateTime2.Day.ToString();
                        string m_2 = dateTime2.Month.ToString().PadLeft(2, '0');
                        string a_2 = dateTime2.Year.ToString();

                        string f_2 = d_2 + "/" + m_2 + "/" + a_2;

                        string shortDateString = f_2;//dateTime2.ToString("dd/MM/yyyy");
                        con.insertRespAccion(text1, text2, resp, causa, text3, shortDateString);
                    }
                    //this.lblSuccess.Visible = true;
                    //upnMensajes.Update();
                    this.enviarcorreo("msamano@mrlucky.com.mx", this.lblClave.Text, this.lblQueja.Text, tabla);
                    //this.enviarcorreo("aescamilla@mrlucky.com.mx", this.lblClave.Text, this.lblQueja.Text, tabla);
                    foreach (DataRow row in (InternalDataCollectionBase)tabla.Rows)
                        this.enviarcorreo_accion(this.con.consultacorreo(row["clv"].ToString()), row["clv"].ToString(), this.lblQueja.Text, row["nombre"].ToString(), row["causa"].ToString());
                    
                    string str = this.con.validacion_de_causas_ingresadas(this.lblQueja.Text);
                    if (str != "")
                    {
                        if (str == "2")
                            this.con.actualizaetapa2(this.lblQueja.Text, "2");
                        else
                            this.con.actualizaetapa2(this.lblQueja.Text, "1");
                    }
                    this.dtAccionesCausa = this.con.causas_responsables_acciones(this.lblQueja.Text, this.lblClaveInve.Text);
                    this.gvwResponsable.DataSource = this.dtAccionesCausa;
                    this.gvwResponsable.DataBind();
                    this.upnResponsables.Update();
                    this.gvwAgregar.DataSource = "";
                    this.gvwAgregar.DataBind();
                    this.uplAgregar.Update();
                    tabla.Clear();
                    //IPHostEntry ipHostEntry = new IPHostEntry();
                    //IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    //Convert.ToString((object)hostEntry.AddressList[hostEntry.AddressList.Length - 2]);
                    //string fch = DateTime.Now.ToString();
                    //string fch2 = Convert.ToDateTime(fch).ToString("dd/MM/yyyy HH:mm:ss");
                    //this.con.registro_movimiento(fch2, Convert.ToString(hostEntry.HostName), this.lblClave.Text, "A", "", this.lblQueja.Text, "INSERCION DE CAUSAS", "QUEJAS");
                }
                else
                    this.lblWarning.Visible = false;
            }
        }

        public void enviarcorreo(string correo, string responsable, string cve_queja, DataTable tabla)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string str3 = "";
            foreach (DataRow row in (InternalDataCollectionBase)tabla.Rows)
                str3 = str3 + "<tr><td>" + row["nombre"].ToString() + "</td></tr><tr><td>" + row["causa"].ToString() + "</td></tr>";
            string str4 = "<table border='2'><tr><td align='center'><h2>Registro de Causas</h2></td></tr><tr><td>Registrado por: " + this.lblNombre.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja no.: " + this.lblQueja.Text + "</td></tr><tr><td>Producto: " + this.txtProducto.Text + "</td></tr><tr><td>Problema: " + this.txtProblema.Text + "</td></tr><tr><td>Comentario: " + this.txtComentario.Text + "</td></tr><tr><td>Causas y Responsables de acciones correctivas:</td></tr>" + str3 + "</table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            MailMessage message = new MailMessage();
            message.To.Add(correo);
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

        public void enviarcorreo_accion(
      string correo,
      string responsable,
      string cve_queja,
      string nombre,
      string caus)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string str3 = "<table border='2'><tr><td align='center'><h2>Asignaci&oacute;n para Acci&oacute;n Correctiva</h2></td></tr><tr><td>Registrado por: " + this.lblNombre.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja no.: " + this.lblQueja.Text + "</td></tr><tr><td>Producto: " + this.txtProducto.Text + "</td></tr><tr><td>Problema: " + this.txtProblema.Text + "</td></tr><tr><td>Comentario: " + this.txtComentario.Text + "</td></tr><tr><td>Causa: " + caus + "</td></tr><tr><td>Fecha l&iacute;mite para registrar acciones: " + this.txtFechaEntrega.Text + "</td></tr></table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            MailMessage message = new MailMessage();
            message.To.Add(correo);
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + cve_queja;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //if (this.lblClaveInve.Text == "-")
            //{
            //    //ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Click en el botón del responsable de dar de alta las acciones correctivas');", true);
            //this.Response.Write("<script>alert('Click en el botón del responsable de dar de alta las acciones correctivas')</script>");
            //    return;
            //}

            DataTable dataTable = (DataTable)this.Session["datos"];
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["clv"].ToString() == this.ddlResp1.SelectedValue.ToString())
                    return;
            }
            DataRow row1 = dataTable.NewRow();
            row1["clv"] = (object)this.ddlResp1.SelectedValue.ToString();
            row1["nombre"] = (object)this.ddlResp1.SelectedItem.ToString();
            row1["causa"] = (object)this.txtCausa.Text.ToUpper();
            dataTable.Rows.Add(row1);
            this.gvwAgregar.DataSource = (object)dataTable;
            this.gvwAgregar.DataBind();
            this.Session["datos"] = (object)dataTable;
            this.uplAgregar.Update();
            this.txtCausa.Text = "";
            this.ddlResp1.SelectedValue = "0";
            this.upnControles.Update();

            this.dtAccionesCausa = this.con.causas_responsables_acciones(this.lblQueja.Text, this.lblClaveInve.Text);
            this.gvwResponsable.DataSource = (object)this.dtAccionesCausa;
            this.gvwResponsable.DataBind();
            this.upnResponsables.Update();
        }

        protected void gvwResponsables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "muestradatos"))
                return;
            GridViewRow row = this.gvwResponsables.Rows[Convert.ToInt32(e.CommandArgument)];
            string str1 = this.Server.HtmlDecode(row.Cells[0].Text);
            string str2 = this.Server.HtmlDecode(row.Cells[3].Text);
            string str3 = this.Server.HtmlDecode(row.Cells[4].Text);
            string str4 = this.Server.HtmlDecode(row.Cells[5].Text);
            this.txtComentario.Text = str2;
            this.txtFecha.Text = str4;
            this.txtFechaEntrega.Text = str3;
            this.upnDatos.Update();
            this.lblClaveInve.Text = str1;
            this.upnClaveInv.Update();
            this.dtAccionesCausa = this.con.causas_responsables_acciones(this.lblQueja.Text, this.lblClaveInve.Text);
            this.gvwResponsable.DataSource = (object)this.dtAccionesCausa;
            this.gvwResponsable.DataBind();
            this.upnResponsables.Update();
        }

        protected void gvwAgregar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string text = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[3].Controls.OfType<Button>())
            {
                if (button.CommandName == "Delete")
                    button.Attributes["onclick"] = "if(!confirm('¿Desea borrar al investigador?')){ return false;};";
            }
        }

        protected void gvwAgregar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int int32 = Convert.ToInt32(e.RowIndex);
            DataTable dataTable = (DataTable)this.Session["datos"];
            dataTable.Rows.RemoveAt(int32);
            this.gvwAgregar.DataSource = (object)dataTable;
            this.gvwAgregar.DataBind();
            this.Session["datos"] = (object)dataTable;
            this.uplAgregar.Update();
        }

        protected void gvwResponsable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string text = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
            {
                if (button.CommandName == "Delete")
                    button.Attributes["onclick"] = "if(!confirm('¿Desea borrar al investigador?')){return false;};";
            }
        }

        protected void gvwResponsable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.con.borrar_responsable_accion(this.Server.HtmlDecode(this.gvwResponsable.Rows[Convert.ToInt32(e.RowIndex)].Cells[0].Text)) == "1")
            {
                this.lblSuccessInv.Visible = true;
                this.dtAccionesCausa = this.con.causas_responsables_acciones(this.lblQueja.Text, this.lblClaveInve.Text);
                this.gvwResponsable.DataSource = (object)this.dtAccionesCausa;
                this.gvwResponsable.DataBind();
                this.upnResponsables.Update();
            }
            else
                this.lblWarningInv.Visible = true;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.lblSuccess.Visible = false;
            this.lblSuccessInv.Visible = false;
            this.lblWarning.Visible = false;
            this.lblWarningInv.Visible = false;
            this.lblSuccessMod.Visible = false;
            this.lblWarningMod.Visible = false;
        }

        protected void gvwResponsable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "verdatos"))
                return;
            GridViewRow row = this.gvwResponsable.Rows[Convert.ToInt32(e.CommandArgument)];
            this.lblClaveAccion.Text = this.Server.HtmlDecode(row.Cells[0].Text);
            this.lblOperacion.Text = "CONSULTA";
            this.upnClaveAccion.Update();
            string str = this.Server.HtmlDecode(row.Cells[1].Text);
            this.txtCausa.Text = this.Server.HtmlDecode(row.Cells[3].Text);
            this.ddlResp1.SelectedValue = str;
            this.upnControles.Update();
            this.btnAdd.Enabled = false;
            this.upnAdd.Update();
        }

        protected void txtTarima_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.txtTarima.Text) <= 0)
                return;
            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Se capturo la tarima');", true);
            this.btnGuardar.Enabled = true;
            this.upnValidaTarima.Update();
        }

        protected void btnTarima_Click(object sender, EventArgs e)
        {
            this.btnGuardar.Enabled = false;
            this.upnValidaTarima.Update();
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.ClientScript.RegisterStartupScript(this.GetType(), "popup", "openChild();", true);
        }
        
        protected void grvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = this.grvDetalle.Rows[Convert.ToInt32(e.CommandArgument)];
            string prod_clave = this.Server.HtmlDecode(row.Cells[0].Text);
            string ord_prod = this.Server.HtmlDecode(row.Cells[2].Text);
            string ptc_ptp = this.Server.HtmlDecode(row.Cells[3].Text);
            if (ptc_ptp == "PTC")
                return;
            dtMatPrima = this.con.productos_MP(prod_clave, ord_prod);

            if (dtMatPrima.Rows.Count < 2)
                return;

            dtMatPrimaSelected = this.con.productos_MP_detalle(lblQueja.Text, ord_prod);
            //foreach (DataRow rw in dtMatPrima.Rows)
            //{
            //    foreach (DataRow rw2 in dtMatPrimaSelected.Select("prod_clave = '" + rw["compp_clave"] + "'"))
            //    {
            //        rw["estado"] = true;
            //    }
            //}

            this.grvMatPrima.DataSource = (object)this.dtMatPrima;
            this.grvMatPrima.DataBind();
            this.upnMatPrima.Update();

            if (e.CommandName == "matprima")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "idmodal", "$('#idmodal').modal();", true);
            }

            check_checkboxes();
        }

        protected void chkMP_CheckedChanged(Object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox cb1 = (CheckBox)grvMatPrima.Rows[index].FindControl("chkMP");
            string prod_clave = this.Server.HtmlDecode(row.Cells[1].Text);
            string prod_nombr = this.Server.HtmlDecode(row.Cells[2].Text);
            string orden_prod = this.Server.HtmlDecode(row.Cells[3].Text);
            if (cb1.Checked == true)
            {
                //AGREGAR REGISTRO A TABLA tb_det_quejas_det
                this.con.agrega_detalle_mp(lblQueja.Text, prod_clave, prod_nombr, orden_prod);
            }
            else
            {
                //BUSCAR REGISTRO Y CANCELARLO
                this.con.cancela_detalle_mp(lblQueja.Text, prod_clave, orden_prod);
            }
            
            //string yourvalue = cb1.Text;
            
        }

        public void check_checkboxes()
        {
            foreach (DataRow rw in dtMatPrimaSelected.Rows)
            {
                string cve = rw["prod_clave"].ToString();
                foreach (GridViewRow rx in grvMatPrima.Rows)
                {
                    if (rx.Cells[1].Text == cve)
                    {
                        CheckBox cb1 = (CheckBox)grvMatPrima.Rows[rx.RowIndex].FindControl("chkMP");
                        cb1.Checked = true;
                    }
                }
            }
        }


        /*
         * <!--asp:BoundField HeaderText="Tarima" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_tarima"/-->
                                                                    <!--asp:BoundField HeaderText="Lote" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_lote"/-->
                                                                    <!--asp:BoundField HeaderText="Fecha Cad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="fecha_cad"/-->
                                                                    <!--asp:BoundField HeaderText="CveProv" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="prov_clave"/-->
                                                                    <!--asp:BoundField HeaderText="Proveedor" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="prov_nombre"/-->
                                                                    <!--asp:BoundField HeaderText="CveRch" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="rch_clave"/-->
                                                                    <!--asp:BoundField HeaderText="Rancho" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="rch_nombre"/-->
                                                                    <!--asp:BoundField HeaderText="CveTbl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="tbl_clave"/-->
                                                                    <!--asp:BoundField HeaderText="Tabla" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="tbl_nombre"/-->
                                                                    <!--asp:BoundField HeaderText="Responsable" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_responsable"/-->
                                                                    <!--asp:BoundField HeaderText="Area" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_area"/-->
         * 
         * <!--ap:BoundField HeaderText="Rechazado" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_cantrecha"/-->
                                                                    <!--ap:BoundField HeaderText="Unidad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_unidad"/-->
         * 
         * <!--asp:BoundField HeaderText="Recibido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantreci"/-->
         */
    }
}