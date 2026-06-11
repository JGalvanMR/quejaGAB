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
using System.Web.Services;

namespace queja
{
    public partial class principal : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtProblemas = new DataTable();
        private DataTable dtOrdenes = new DataTable();
        private DataTable dtDatos = new DataTable();
        private DataTable dtClientes = new DataTable();
        private DataTable dtResponsables = new DataTable();
        private DataTable dtSucursales = new DataTable();
        private DataTable dtAreas = new DataTable();
        private string resp = "";
        private string area = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            if (this.Page.IsPostBack)
                return;
            this.dtClientes = this.con.cargarclientes();
            this.ddlCliente.DataSource = (object)this.dtClientes;
            this.ddlCliente.DataTextField = "CliRSocial";
            this.ddlCliente.DataValueField = "CliCod";
            this.ddlCliente.DataBind();
            this.dtProblemas = this.con.cargarproblemas();
            this.ddlProblema.DataSource = (object)this.dtProblemas;
            this.ddlProblema.DataTextField = "pro_nombre";
            this.ddlProblema.DataValueField = "pro_clave";
            this.ddlProblema.DataBind();
            this.txtFolio.Text = this.con.cargafolio().ToString();
            TextBox txtSemana = this.txtSemana;
            int num = this.con.cargasemana();
            string str1 = num.ToString();
            txtSemana.Text = str1;
            TextBox txtMes = this.txtMes;
            num = DateTime.Now.Month;
            string str2 = num.ToString().PadLeft(2, '0');
            txtMes.Text = str2;
            this.txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.lblVerifica.Text = "0";
            this.btnGuardar.Enabled = false;
            this.upnGuardar.Update();

            txtPedido.Attributes.Add("onKeyUp", "CallScript(this)");
        }

        protected void btnBuscaOrden_Click(object sender, EventArgs e)
        {
            this.dtOrdenes = this.con.cargarordenprod(this.txtOrdenProd.Text);
            this.gvwOrden.DataSource = (object)this.dtOrdenes;
            this.gvwOrden.DataBind();

            string tipo_recibo = dtOrdenes.Rows[0]["tipo"].ToString();
            this.dtDatos = this.con.cargadatosorden(this.txtOrdenProd.Text, tipo_recibo);
            foreach (DataRow row in (InternalDataCollectionBase)this.dtDatos.Rows)
            {
                this.txtRespLinea.Text = row["ordp_responsable"].ToString();
                this.txtArea.Text = row["ordp_linea"].ToString();
                this.txtRespLinea2.Value = row["ordp_responsable"].ToString();
                this.txtArea2.Value = row["ordp_linea"].ToString();
            }
        }

        protected void gvwOrden_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "muestradatos"))
                return;
            GridViewRow row = this.gvwOrden.Rows[Convert.ToInt32(e.CommandArgument)];
            string str1 = this.Server.HtmlDecode(row.Cells[1].Text);
            string str2 = this.Server.HtmlDecode(row.Cells[2].Text);
            this.Server.HtmlDecode(row.Cells[3].Text);
            string str3 = this.Server.HtmlDecode(row.Cells[4].Text);
            string str4 = this.Server.HtmlDecode(row.Cells[5].Text);
            string str5 = this.Server.HtmlDecode(row.Cells[6].Text);
            string str6 = this.Server.HtmlDecode(row.Cells[7].Text);
            string str7 = this.Server.HtmlDecode(row.Cells[8].Text);
            string str8 = this.Server.HtmlDecode(row.Cells[9].Text);
            string str9 = this.Server.HtmlDecode(row.Cells[10].Text);
            string str10 = this.Server.HtmlDecode(row.Cells[11].Text);
            string str11 = this.Server.HtmlDecode(row.Cells[12].Text);
            this.txt_clave.Text = str1;
            this.txt_nom.Text = str2;
            this.txt_lote.Text = str11;
            this.txt_fechacad.Text = str9;
            this.txt_cveprov.Text = str3;
            this.txt_nomprov.Text = str4;
            this.txt_cverch.Text = str5;
            this.txt_nomrch.Text = str6;
            this.txt_cvetbl.Text = str7;
            this.txt_nomtbl.Text = str8;
            this.txtTipo.Text = str10;
            string text = this.txtTipo.Text;
            if (text == "PTC")
            {
                this.txtVariedad.Text = this.con.variedad_recepcion_pt(this.txtOrdenProd.Text);
                this.txtCajasProducidas.Text = this.con.cajas_producidas_folio(this.txtOrdenProd.Text, this.txt_clave.Text);
                this.upnCajasProducidas.Update();
            }
            if (text == "PTP")
            {
                string rec1 = this.con.variedad_busca_eti_final(this.txtOrdenProd.Text, this.txt_clave.Text);
                this.txtCajasProducidas.Text = this.con.cajas_producidas_folio2(this.txtOrdenProd.Text, this.txt_clave.Text, "");
                this.upnCajasProducidas.Update();
                if (!(rec1 == "83896")) { }
                string str12 = this.con.variedad_busca_prod_odp(this.txtOrdenProd.Text, rec1);
                if (str12 == "MP")
                    this.txtVariedad.Text = this.con.variedad_recepcion_mp(rec1);
                if (str12 == "REM")
                {
                    string[] strArray = this.con.variedad_prod_ode_tipo(rec1).Split('-');
                    if (strArray[1] == "PTP")
                    {
                        string rec2 = this.con.variedad_busca_eti_final(strArray[0], strArray[2]);
                        string str13 = this.con.variedad_busca_prod_odp(strArray[0], rec2);
                        if (str13 == "MP" || str13 == "PTP")
                            this.txtVariedad.Text = this.con.variedad_recepcion_mp(rec2);
                    }
                }
                if (str12 == "ESP")
                    this.txtVariedad.Text = this.con.variedad_recepcion_esparrago(rec1);
            }
            if (str10 == "PTP")
            {
                this.txtRespLinea.Text = this.txtRespLinea2.Value;
                this.txtArea.Text = this.txtArea2.Value;
                this.upnDatos.Update();
            }
            //if (str10 == "PTC")
            //{
            //    if (this.txtRespLinea.Text == "" && this.txtArea.Text == "")
            //    {
            //        this.txtRespLinea.Text = "";
            //        this.txtArea.Text = "";
            //        this.upnDatos.Update();
            //    }
                
            //}
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTipo.SelectedValue == "N")
                this.ddlMoneda.SelectedValue = "PESOS";
            if (this.ddlTipo.SelectedValue == "E")
                this.ddlMoneda.SelectedValue = "DOLARES";
            this.uplMoneda.Update();
        }

        protected void btnValidarFotos_Click(object sender, EventArgs e)
        {
            ////verificar que el usuario haya cargado las evidencias
            //bool foto1 = false;
            //if (this.fluArchivo1.HasFile)
            //    foto1 = true;
            //bool foto2 = false;
            //if (this.fluArchivo2.HasFile)
            //    foto2 = true;
            //bool foto3 = false;
            //if (this.fluArchivo3.HasFile)
            //    foto3 = true;

            //if (foto1 == false || foto2 == false || foto3 == false)
            //{
            //    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Cargue las evidencias de la queja (PDF o Imagen), favor de verificar');", true);
            //    return;
            //}
            //if (foto1 == true && foto2 == true && foto3 == true)
            //{
            //    if (this.fluArchivo1.PostedFile.ContentLength > 4194304)
            //    {
            //        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño del Archivo 1 es superior a 4Mb favor de verificarlo');", true);
            //        return;
            //    }
            //    if (this.fluArchivo2.PostedFile.ContentLength > 4194304)
            //    {
            //        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño del Archivo 2 es superior a 4Mb favor de verificarlo');", true);
            //        return;
            //    }
            //    if (this.fluArchivo3.PostedFile.ContentLength > 4194304)
            //    {
            //        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño del Archivo 3 es superior a 4Mb favor de verificarlo');", true);
            //        return;
            //    }
            //}
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.lblVerifica.Text == "1")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('La queja ya fue registrada');", true);
            }
            else
            {
                

                string text1 = this.txtFolio.Text; 
                string text2 = this.txtSemana.Text; //mstr que_semana
                string text3 = this.txtFecha.Text; //mstr que_fecha
                string text4 = this.txtMes.Text; //mstr que_mes
                string selectedValue1 = this.ddlCliente.SelectedValue; //mstr que_cliprim
                string text5 = this.ddlCliente.SelectedItem.Text; //mstr que_cliente
                string selectedValue2 = this.ddlSucursales.SelectedValue; //mstr subcli_folio
                string text6 = this.ddlSucursales.SelectedItem.Text; //mstr que_sucursal
                string upper1 = this.txtReporto.Text.ToUpper().Replace("'", ""); //mstr que_reporto
                string text7 = this.lblClave.Text; //mstr que_recibio
                string text8 = this.lblCedis.Text; //mstr cedis
                string text9 = this.txt_clave.Text; //det
                string problema = this.ddlProblema.SelectedItem.Value; //det
                string text10 = this.txtOrdenProd.Text; //det
                string text11 = this.txtArea.Text; //det
                string text12 = this.txtRespLinea.Text; //det
                string text13 = this.txtCantRech.Text; //det
                string text14 = this.txtCantReci.Text; //det
                string upper2 = this.txtUnidad.Text.ToUpper(); //det
                string devolucion = this.chkDevolucion.Checked ? "1" : "0"; //deet
                string selectedValue3 = this.ddlMoneda.SelectedValue; //det
                string text15 = this.txt_cveprov.Text; //det
                string text16 = this.txt_cverch.Text; //det
                string text17 = this.txt_cvetbl.Text; //det
                string text18 = this.txt_lote.Text; //det
                string text19 = this.txt_nom.Text; //det
                string text20 = this.txt_fechacad.Text; //det
                string causa = ""; 
                string selectedValue4 = this.ddlTipo.SelectedValue; //mstr 29
                string area_queja = ""; //mstr
                string text21 = this.txtTipo.Text; //det
                string text22 = this.txtVariedad.Text; //det
                string text23 = this.txtCajasProducidas.Text; //
                string text24 = this.txtPorcentaje.Text;
                string text25 = this.txtObservaciones.Text.Replace("'", ""); //mstr
                string text26 = this.lblClave.Text;
                string text27 = this.txtPedido.Text; //mstr
                string text28 = this.lblNombre.Text; 
                string text29 = this.txtCosto.Text; //mstr 40
                string text30 = this.chkMerma.Checked ? "1" : "0";
                string text31 = this.chkConsumidor.Checked ? "1" : "0";

                string textDev = (rbtList.SelectedValue == "DEV") ? "1" : "0";
                string textMer = (rbtList.SelectedValue == "MER") ? "1" : "0";
                string textBon = (rbtList.SelectedValue == "BON") ? "1" : "0";
                string textNA = (rbtList.SelectedValue == "NA") ? "1" : "0";

                devolucion = textDev;
                text30 = textMer;


                if (selectedValue4 == "N")//Validacion para saber si el tipo es Nacional
                {
                    if (selectedValue3 == "DOLARES")//Validacion de la moneda por si se seleccionó otro
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No coincide la moneda en dolares con el tipo Nacional de la queja, favor de verificar');", true); 
                        return;
                    }
                       
                }
                else //Validacion para saber si el tipo es Nacional
                {
                    if (selectedValue3 == "PESOS")//Validacion de la moneda por si se seleccionó otro
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No coincide la moneda en pesos con el tipo Exportacion de la queja, favor de verificar');", true); 
                        return;
                    }
                }

                if (text31 == "0")
                {
                    if (text27 == "")
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No ha ingresado el pedido');", true);
                        return;
                    }

                    if (this.con.validar_ingreso_pedido(text27, selectedValue4, lblClave.Text, txt_clave.Text) == "0")
                    {
                        //con.correo_error("validacion de pedido");
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El pedido ingresado no existe, corrigir e intente nuevamente');", true);
                        
                    }
                    else
                    {
                        string cnte = con.busca_cliente_pedido(text27, selectedValue4, "", lblClave.Text);

                        string fec_emb = con.fecha_embarque(text27, selectedValue4, lblClave.Text);
                        string sem_emb = con.cargasemana_embarque(Convert.ToDateTime(fec_emb).ToString("dd/MM/yyyy")).ToString();


                        if (cnte == "")
                        {
                            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El pedido ingresado no existe, verifique pedido');", true);
                            return;
                        }


                        //validar seleccion de dato de devolucion, merma y bonificacion
                        if (textDev == "0" && textMer == "0" && textBon == "0" && textNA == "0")
                        {
                            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione si es Devolucion, Bonificacion, Merma o No aplica');", true);
                            return;
                        }

                        //con.correo_error(txtFecha.Text);
                        string f_1 = DateTime.Now.ToString();
                        string shortDateString = Convert.ToDateTime(f_1).AddDays(1).ToString("dd/MM/yyyy");

                        //dateTime = dateTime.AddDays(1);
                        //con.correo_error(dateTime.ToString());
                        //string shortDateString = dateTime.ToString();
                        //con.correo_error("fecha mas un día:" + shortDateString);
                        if (text9 == "")
                        {
                            con.correo_error("alert('Se detecto un error, seleccione nuevamente el producto del listado');");
                            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Se detecto un error, seleccione nuevamente el producto del listado');", true);
                        }
                        else
                        {
                            //con.correo_error("validacion de pedido");
                            this.btnGuardar.Enabled = false;
                            this.upnGuardar.Update();
                            if (Convert.ToInt32(text13) <= 0)
                            {
                                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('La cantidad rechazada debe ser mayor a cero cajas');", true);
                            }
                            else
                            {
                                string[] strArray = this.con.insertQuejas(text1, text2, text3, text4, selectedValue1, text5, text6, upper1, text7, text9, problema, text10, text11, text12, text13, text14, upper2, devolucion, selectedValue3, text15, text16, text17, text18, text19, text20, causa, text8, text26, selectedValue4, shortDateString, area_queja, selectedValue2, text21, text22, text27, text25, text28, text23, text24, text29, text30, cnte, text31, textBon, textNA, sem_emb, Convert.ToDateTime(fec_emb).ToString("dd/MM/yyyy")).Split('-');
                                if (strArray[0] == "1")
                                {
                                    this.enviarcorreo("aescamilla@mrlucky.com.mx", text26, strArray[1], "ERROR INSERCION AL DETALLE DE QUEJA POR PEDIDO");//this.con.enviarcorreo_error(this.lblNombre.Text, text1, "ERROR INSERCION AL DETALLE DE QUEJA POR PEDIDO");
                                    this.lblDanger.Visible = true;
                                }
                                else if (strArray[0] == "2")
                                {
                                    this.lblWarning.Visible = true;
                                }
                                else
                                {
                                    this.lblSuccess.Visible = true;
                                    this.btnSubir.Enabled = true;
                                    this.btnTarima.Enabled = true;
                                    this.upnBotones.Update();
                                    string str1 = "http://189.206.160.206:81/quejas/";
                                    string str2 = "http://gabira1:81/quejas/";
                                    string cuerpo = "<table border='2'><tr><td align='center'><h2>Registro de Queja</h2></td></tr><tr><td>Registro de queja realizado por: " + this.lblNombre.Text + "</td></tr><tr><td>CEDIS: " + this.lblCedis.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja: No. " + strArray[1] + "</td></tr><tr><td>Clave y nombre producto: " + this.txt_clave.Text + " " + this.txt_nom.Text + "</td></tr><tr><td>Problema: " + this.ddlProblema.SelectedItem.ToString() + "</td></tr><tr><td>Cliente: " + this.ddlCliente.SelectedItem.ToString() + "</td></tr><tr><td>Pedido: " + this.txtPedido.Text + "</td></tr><tr><td>Costo: " + this.txtCosto.Text + "</td></tr></table><br /><p><h4>Ir a la seccion de Editar Queja para asignaci&oacute; de &Aacute;rea</h4></p><br /><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
                                    this.enviarcorreo("msamano@mrlucky.com.mx", text26, strArray[1], cuerpo);
                                    //this.enviarcorreo("aescamilla@mrlucky.com.mx", text26, strArray[1], cuerpo);
                                    if (strArray[1] != "" && this.txtFolio.Text != strArray[1])
                                    {
                                        this.lblCambio.Visible = true;
                                        this.txtFolio.Text = strArray[1];
                                        this.uplFolio.Update();
                                    }
                                    this.lblVerifica.Text = "1";
                                    this.upnVerifica.Update();
                                }
                            }
                        }

                    }

                }
                else
                {
                    //con.correo_error(txtFecha.Text);
                    string f_1 = DateTime.Now.ToString();
                    string shortDateString = Convert.ToDateTime(f_1).AddDays(1).ToString("dd/MM/yyyy");

                    //dateTime = dateTime.AddDays(1);
                    //con.correo_error(dateTime.ToString());
                    //string shortDateString = dateTime.ToString();
                    //con.correo_error("fecha mas un día:" + shortDateString);
                    if (text9 == "")
                    {
                        con.correo_error("alert('Se detecto un error, seleccione nuevamente el producto del listado');");
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Se detecto un error, seleccione nuevamente el producto del listado');", true);
                    }
                    else
                    {
                        //con.correo_error("validacion de pedido");
                        this.btnGuardar.Enabled = false;
                        this.upnGuardar.Update();
                        if (Convert.ToInt32(text13) <= 0)
                        {
                            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('La cantidad rechazada debe ser mayor a cero cajas');", true);
                        }
                        else
                        {
                            string[] strArray = this.con.insertQuejas(text1, text2, text3, text4, selectedValue1, text5, text6, upper1, text7, text9, problema, text10, text11, text12, text13, text14, upper2, devolucion, selectedValue3, text15, text16, text17, text18, text19, text20, causa, text8, text26, selectedValue4, shortDateString, area_queja, selectedValue2, text21, text22, text27, text25, text28, text23, text24, text29, text30, "", text31, textBon, textNA, "", "").Split('-');
                            if (strArray[0] == "1")
                            {
                                this.enviarcorreo("aescamilla@mrlucky.com.mx", text26, strArray[1], "ERROR INSERCION AL DETALLE DE QUEJA POR PEDIDO");//this.con.enviarcorreo_error(this.lblNombre.Text, text1, "ERROR INSERCION AL DETALLE DE QUEJA POR PEDIDO");
                                this.lblDanger.Visible = true;
                            }
                            else if (strArray[0] == "2")
                            {
                                this.lblWarning.Visible = true;
                            }
                            else
                            {
                                this.lblSuccess.Visible = true;
                                this.btnSubir.Enabled = true;
                                this.btnTarima.Enabled = true;
                                this.upnBotones.Update();
                                string str1 = "http://189.206.160.206:81/quejas/";
                                string str2 = "http://gabira1:81/quejas/";
                                string cuerpo = "<table border='2'><tr><td align='center'><h2>Registro de Queja</h2></td></tr><tr><td>Registro de queja realizado por: " + this.lblNombre.Text + "</td></tr><tr><td>CEDIS: " + this.lblCedis.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja: No. " + strArray[1] + "</td></tr><tr><td>Clave y nombre producto: " + this.txt_clave.Text + " " + this.txt_nom.Text + "</td></tr><tr><td>Problema: " + this.ddlProblema.SelectedItem.ToString() + "</td></tr><tr><td>Cliente: " + this.ddlCliente.SelectedItem.ToString() + "</td></tr><tr><td>Pedido: " + this.txtPedido.Text + "</td></tr><tr><td>Costo: " + this.txtCosto.Text + "</td></tr></table><br /><p><h4>Ir a la seccion de Editar Queja para asignaci&oacute; de &Aacute;rea</h4></p><br /><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
                                this.enviarcorreo("msamano@mrlucky.com.mx", text26, strArray[1], cuerpo);
                                //this.enviarcorreo("aescamilla@mrlucky.com.mx", text26, strArray[1], cuerpo);
                                if (strArray[1] != "" && this.txtFolio.Text != strArray[1])
                                {
                                    this.lblCambio.Visible = true;
                                    this.txtFolio.Text = strArray[1];
                                    this.uplFolio.Update();
                                }
                                this.lblVerifica.Text = "1";
                                this.upnVerifica.Update();
                            }
                        }
                    }
                }
                
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.txtFolio.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Iframe1.Attributes["src"] = "upload.aspx";
            this.Iframe1.Attributes["style"] = "border:solid 1px;";
            this.Iframe1.Visible = true;
            this.UpdatePanel1.Update();
        }

        public void enviarcorreo(string correo, string responsable, string cve_queja, string cuerpo)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add(correo);
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + cve_queja;
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

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //this.lblSuccess.Visible = false;
            //this.lblDanger.Visible = false;
            //this.lblWarning.Visible = false;
            //this.lblCambio.Visible = false;
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dtSucursales.Clear();
            this.dtSucursales = this.con.cargarsucursales(this.ddlCliente.SelectedValue.ToString(), this.lblCedis.Text);
            this.ddlSucursales.DataSource = (object)this.dtSucursales;
            this.ddlSucursales.DataTextField = "subcli_nombre";
            this.ddlSucursales.DataValueField = "subcli_folio";
            this.ddlSucursales.DataBind();
            this.upnSucursales.Update();
        }

        protected void btnTarima_Click(object sender, EventArgs e)
        {
            if (!(this.con.validacion_tarima(this.txtFolio.Text) == "0"))
                return;
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.txtFolio.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Iframe1.Attributes["src"] = "tarimas.aspx";
            this.Iframe1.Attributes["style"] = "border:solid 1px;";
            this.Iframe1.Visible = true;
            this.UpdatePanel1.Update();
        }

        protected void txtCantRech_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCantRech.Text == "")
                return;
            if (this.txtCantReci.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Ingrese la cantidad recibida');", true);
            }
            else
            {
                Decimal num1 = Convert.ToDecimal(this.txtCantReci.Text);
                Decimal num2 = Convert.ToDecimal(this.txtCantRech.Text);
                if (num2 > num1)
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Las cajas rechazadas no deben ser mayor a las recibidas');", true);
                    this.txtCantRech.Text = "";
                    this.upnRecha.Update();
                    this.btnGuardar.Enabled = false;
                    this.upnGuardar.Update();
                }
                else
                {
                    this.txtPorcentaje.Text = (num2 * new Decimal(100) / Convert.ToDecimal(this.txtCajasProducidas.Text)).ToString("0.00");
                    this.upnPorcentaje.Update();
                    this.btnGuardar.Enabled = true;
                    this.upnGuardar.Update();
                }
            }
        }

        protected void txtCantReci_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCantReci.Text == "" || !(Convert.ToDecimal(this.txtCantReci.Text) > Convert.ToDecimal(this.txtCajasProducidas.Text)))
                return;
            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Las cajas recibidas no pueden ser mayor a las producidas');", true);
            this.txtCantReci.Text = "";
            this.upnReci.Update();
        }

        
    }
}