using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace queja
{
    public partial class serviciosnew : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtPlacas = new DataTable();
        private DataTable dtQuejasServ = new DataTable();
        private DataTable dtAgregar = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.ToString() == "")
            {
                this.lblClave.Text = this.Session["clave"].ToString();
                this.lblNombre.Text = this.Session["nombre"].ToString();
                this.lblCedis.Text = this.Session["cedis"].ToString();
                this.lblAdmin.Text = this.Session["admin"].ToString();
                this.lblQueja.Text = this.Session["queja"].ToString();
                this.lblTarea.Text = this.Session["tarea"].ToString();
            }
            else
            {
                this.lblClave.Text = Request.QueryString["clave"];
                this.lblNombre.Text = Request.QueryString["nombre"];
                this.lblCedis.Text = Request.QueryString["cedis"];
                this.lblAdmin.Text = Request.QueryString["admin"];
                this.lblQueja.Text = Request.QueryString["queja"];
                this.lblTarea.Text = Request.QueryString["tarea"];

                this.Session["clave"] = this.lblClave.Text;
                this.Session["nombre"] = this.lblNombre.Text;
                this.Session["cedis"] = this.lblCedis.Text;
                this.Session["admin"] = this.lblAdmin.Text;
                this.Session["queja"] = this.lblQueja.Text;
                this.Session["tarea"] = this.lblTarea.Text;
            }
            string str = this.Session["tarea"].ToString();
            this.dtAgregar.Columns.Add("ser_folio", typeof(string));
            this.dtAgregar.Columns.Add("ser_nombre", typeof(string));
            if (!Page.IsPostBack)
            {
                this.txtFolio.Text = this.con.cargafolio_serv().ToString();
                this.txtSemana.Text = this.con.cargasemana().ToString();
                string fech = DateTime.Now.ToString();
                string dia = DateTime.Now.Day.ToString().PadLeft(2, '0');
                string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
                string anio = DateTime.Now.Year.ToString();
                this.txtFecha.Text = dia + "/" + mes + "/" + anio;//Convert.ToDateTime(fech).ToString("dd/MM/yyyy");
                this.txtResponsable.Text = this.lblNombre.Text;
                this.lblTipo.Text = "T";
                this.dtQuejasServ = this.con.combo_qujas_serv();
                this.ddlQueja.DataSource = (object)this.dtQuejasServ;
                this.ddlQueja.DataTextField = "ser_nombre";
                this.ddlQueja.DataValueField = "ser_folio";
                this.ddlQueja.DataBind();
                this.udpPlacas.Update();
                this.gvwQuejas.DataSource = (object)this.dtAgregar;
                this.gvwQuejas.DataBind();
                this.Session["datos"] = (object)this.dtAgregar;
                this.udpQuejas.Update();

                ddlArea.Enabled = false;
                upnArea.Update();

                if (str == "CONSULTA")
                {
                    this.lblTarea.Text = str;
                    this.btnCancelar.Enabled = false;
                    this.udpTipo.Update();
                    this.txtFechaEmb.ReadOnly = true;
                    this.ddlTipo.Enabled = false;
                    this.ddlTransporte.Enabled = false;
                    this.ddlQueja.Enabled = false;
                    this.btnAgregar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                    foreach (DataRow row in (InternalDataCollectionBase)this.con.quejas_servicio_mstr(this.lblQueja.Text).Rows)
                    {
                        this.txtFolio.Text = row["que_folio"].ToString();
                        this.txtFecha.Text = row["que_fecha"].ToString();
                        this.txtSemana.Text = row["que_semana"].ToString();
                        string[] f1 = row["que_fechaemb"].ToString().Split(' ');
                        this.txtFechaEmb.Text = f1[0].ToString();//Convert.ToDateTime().ToString("dd/MM/yyyy");
                        this.ddlTipo.SelectedValue = row["que_tipo"].ToString();
                        this.txtPedido.Text = row["que_pedido"].ToString();
                        this.txtMes.Text = row["que_mes"].ToString();
                        this.txtTransportista.Text = row["que_transportista"].ToString();
                        this.txtDestino.Text = row["que_destino"].ToString();
                        this.txtCaja.Text = row["que_caja"].ToString();
                        this.txtResponsable.Text = row["que_responsable"].ToString();
                        this.txtAduana.Text = row["que_aduana"].ToString();
                        this.txtComentario.Text = row["que_comentario"].ToString();
                        this.txtPerdidaMn.Text = row["que_perdida_mn"].ToString();
                        this.txtPerdidaUsd.Text = row["que_perdida_usd"].ToString();
                        chkSinRegistro.Checked = (row["sin_reg"].ToString() == "1") ? true : false;
                        ddlArea.SelectedValue = row["embtracli"].ToString();
                    }
                    DataTable dataTable1 = this.con.quejas_servicio_det(this.lblQueja.Text);
                    DataTable dataTable2 = (DataTable)this.Session["datos"];
                    foreach (DataRow row1 in (InternalDataCollectionBase)dataTable1.Rows)
                    {
                        DataRow row2 = dataTable2.NewRow();
                        row2["ser_folio"] = (object)row1["que_clave"].ToString();
                        row2["ser_nombre"] = (object)row1["que_nombre"].ToString();
                        dataTable2.Rows.Add(row2);
                    }
                    this.gvwQuejas.DataSource = (object)dataTable2;
                    this.gvwQuejas.DataBind();
                    this.Session["datos"] = (object)dataTable2;
                    //this.udpGeneral.Update();

                    string carpeta = "~/fotos_serv/";
                    carpeta = Server.MapPath(carpeta);
                    var existe = Directory.Exists(carpeta);
                    if (!Directory.Exists(carpeta))
                    {
                        Directory.CreateDirectory(carpeta);
                    }
                    string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_serv/"));
                    List<ListItem> listItemList = new List<ListItem>();
                    foreach (string path in files)
                    {
                        if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".pdf"))
                            listItemList.Add(new ListItem(Path.GetFileName(path), path));
                    }
                    int num = 1;
                    foreach (ListItem listItem in listItemList)
                    {
                        if (num == 1)
                            this.file1.Text = listItem.ToString();
                        if (num == 2)
                            this.file2.Text = listItem.ToString();
                        if (num == 3)
                            this.file3.Text = listItem.ToString();
                        ++num;
                    }
                    string str2 = "fotos_serv/1_" + lblQueja.Text + ".jpg";
                    this.img1.ImageUrl = str2;
                    this.img2.ImageUrl = "fotos_serv/2_" + lblQueja.Text + ".jpg";
                    this.img3.ImageUrl = "fotos_serv/3_" + lblQueja.Text + ".jpg";
                    this.Image1.ImageUrl = str2;
                    this.Image2.ImageUrl = "fotos_serv/2_" + lblQueja.Text + ".jpg";
                    this.Image3.ImageUrl = "fotos_serv/3_" + lblQueja.Text + ".jpg";

                    btnSubir.Enabled = true;
                    udpGuardar.Update();

                    //ddlArea.Enabled = true;
                    //upnArea.Update();

                    chkSinRegistro.Enabled = false;
                    upnCheck.Update();

                    //btnPDF.Enabled = true;
                    //udpPDF.Update();
                    if (this.lblAdmin.Text == "ADMINISTRADOR")
                    {
                        ddlArea.Enabled = true;
                        upnArea.Update();
                    }
                    else
                    {
                        ddlArea.Enabled = false;
                        upnArea.Update();
                    }
                }
                else
                {
                    if (this.lblAdmin.Text == "ADMINISTRADOR")
                    {
                        ddlArea.Enabled = true;
                        upnArea.Update();
                    }
                    else
                    {
                        ddlArea.Enabled = false;
                        upnArea.Update();
                    }
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (chkSinRegistro.Checked == true)
                return;
            if (txtFechaEmb.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione la fecha del pedido');", true);
                return;
            }
            this.dtPlacas.Clear();
            this.dtPlacas = con.comboplacas_serv(this.txtFechaEmb.Text);
            this.ddlTransporte.DataSource = (object)this.dtPlacas;
            this.ddlTransporte.DataTextField = "no_trailer";
            this.ddlTransporte.DataValueField = "pdn_folio";
            this.ddlTransporte.DataBind();


            this.udpPlacas.Update();
        }

        protected void ddlTransporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTipo.SelectedValue == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No se ha seleccionado el tipo de pedido');", true);
                this.ddlTransporte.SelectedValue = "0";
                this.udpPlacas.Update();
            }
            else
            {
                if (chkSinRegistro.Checked == false)
                {
                    //ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Entra proceso');", true);
                    //con.correo_error("ENTRA");
                    string[] strArray = this.ddlTransporte.SelectedItem.ToString().Split('-');

                    //dividir fecha
                    string[] s_plit = txtFechaEmb.Text.Split('/');
                    string a_1 = s_plit[0];
                    string a_2 = s_plit[1];
                    string a_3 = s_plit[2];
                    string f_1 = a_2 + "/" + a_1 + "/" + a_3;

                    string m = Convert.ToDateTime(f_1).Month.ToString();
                    DataTable dataTable = this.con.detalle_embarque_servicios(strArray[0].ToString().TrimEnd(), this.txtFechaEmb.Text);
                    string str = "";
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                        str = str + row["pdn_folio"].ToString() + ", ";
                    //ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Ahí va proceso');", true);
                    this.txtPedido.Text = str.TrimEnd(' ').TrimEnd(',');
                    this.upnPed.Update();
                    this.txtMes.Text = this.mes_letra(m);
                    this.upnMes.Update();
                    this.txtTransportista.Text = strArray[3].ToString().TrimStart(' ');
                    this.upnTrans.Update();
                    this.txtDestino.Text = strArray[1].ToString().TrimStart(' ');
                    this.upnDest.Update();
                    this.txtCaja.Text = strArray[0].ToString().TrimStart(' ');
                    this.upnCaja.Update();
                    if (!(this.ddlTipo.SelectedValue == "EXPORTACION"))
                        return;
                    if (this.con.tipo_pedido_servicios(this.txtPedido.Text.ToString().Split(',')[0]) == "EXP")
                    {
                        this.txtAduana.Text = strArray[1].ToString().TrimStart(' ');
                        this.upnAdu.Update();
                    }
                    //ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Pasa proceso');", true);
                }
                else
                {
                    this.txtTransportista.Text = ddlTransporte.SelectedValue.ToString() + "_" + ddlTransporte.SelectedItem.ToString();
                    this.upnTrans.Update();
                }


            }
        }

        public string mes_letra(string m)
        {
            string str = "";
            switch (m)
            {
                case "1":
                    str = "ENERO";
                    break;
                case "2":
                    str = "FEBRERO";
                    break;
                case "3":
                    str = "MARZO";
                    break;
                case "4":
                    str = "ABRIL";
                    break;
                case "5":
                    str = "MAYO";
                    break;
                case "6":
                    str = "JUNIO";
                    break;
                case "7":
                    str = "JULIO";
                    break;
                case "8":
                    str = "AGOSTO";
                    break;
                case "9":
                    str = "SEPTIEMBRE";
                    break;
                case "10":
                    str = "OCTUBRE";
                    break;
                case "11":
                    str = "NOVIEMBRE";
                    break;
                case "12":
                    str = "DICIEMBRE";
                    break;
            }
            return str;
        }

        public void habilita_fumigaciones()
        {
            this.btnBuscar.Enabled = true;
            this.ddlTipo.Enabled = true;
            this.ddlTransporte.Enabled = true;
            this.udpGral1.Update();
            this.udpPlacas.Update();
            //this.udpgral2.Update();
            this.btnGuardar.Enabled = true;
            this.udpGuardar.Update();
            this.txtFechaEmb.Enabled = true;
            this.udpFechaEmb.Update();
            ScriptManager.RegisterStartupScript((Page)this, this.GetType(), "temp", "refrescar();", true);
        }

        public void habilita_transportista()
        {
            this.btnBuscar.Enabled = true;
            this.ddlTipo.Enabled = true;
            this.ddlTransporte.Enabled = true;
            this.txtComentario.ReadOnly = false;
            this.txtPerdidaMn.ReadOnly = false;
            this.txtPerdidaUsd.ReadOnly = false;
            this.btnGuardar.Enabled = true;
            this.udpGuardar.Update();
            this.udpGral1.Update();
            this.udpPlacas.Update();
            //this.udpgral2.Update();
            this.txtFechaEmb.Enabled = true;
            this.udpFechaEmb.Update();
            ScriptManager.RegisterStartupScript((Page)this, this.GetType(), "temp", "refrescar();", true);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //this.lblSuccess.Visible = false;
            //this.lblWarning.Visible = false;
            //this.lblParcial.Visible = false;
        }

        public void deshabilita_controles()
        {
            this.txtFolio.Text = this.con.cargafolio().ToString();
            this.txtSemana.Text = this.con.cargasemana().ToString();
            string f1 = DateTime.Now.ToString();
            this.txtFecha.Text = Convert.ToDateTime(f1).ToString("dd/MM/yyyy");
            this.btnBuscar.Enabled = false;
            this.ddlTipo.SelectedValue = "";
            this.ddlTipo.Enabled = false;
            this.ddlTransporte.DataSource = (object)"";
            this.ddlTransporte.DataTextField = "no_trailer";
            this.ddlTransporte.DataValueField = "pdn_folio";
            this.ddlTransporte.DataBind();
            this.ddlTransporte.Enabled = false;
            this.udpPlacas.Update();
            this.txtPedido.Text = "";
            this.txtMes.Text = "";
            this.txtTransportista.Text = "";
            this.txtDestino.Text = "";
            this.txtCaja.Text = "";
            this.txtAduana.Text = "";
            this.txtComentario.Text = "";
            this.txtComentario.ReadOnly = true;
            this.txtPerdidaMn.Text = "0";
            this.txtPerdidaMn.ReadOnly = true;
            this.txtPerdidaUsd.Text = "0";
            this.txtPerdidaUsd.ReadOnly = true;
            this.udpGral1.Update();
            this.udpPlacas.Update();
            //this.udpgral2.Update();
            this.txtFechaEmb.Text = "";
            this.txtFechaEmb.Enabled = false;
            this.udpFechaEmb.Update();
            this.btnGuardar.Enabled = false;
            this.udpGuardar.Update();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.deshabilita_controles();
        }

        //<asp:UpdateProgress ID="UpdateProgress" runat="server">
        //    <ProgressTemplate>
        //        <div class="alert alert-info">
        //            <strong>PROCESANDO...</strong>Se esta registrando la informaci&oacute;n
        //        </div>
        //    </ProgressTemplate>
        //</asp:UpdateProgress>

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.lblTarea.Text != "CONSULTA")
            {
                if (chkSinRegistro.Checked == false)
                {
                    if (!(this.lblTipo.Text == "T"))
                        return;
                    if (this.txtFechaEmb.Text == "")
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar la fecha del embarque');", true);
                    else if (this.ddlTipo.SelectedValue == "")
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar el tipo (Nacional o Exportacion)');", true);
                    else if (this.ddlTransporte.SelectedValue == "")
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar el transporte');", true);
                    }
                    //else if (this.fluArchivo1.HasFile)// && this.fluArchivo1.PostedFile.ContentLength > 4194304
                    //{
                    //    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar por lo menos un archivo');", true);
                    //}
                    else
                    {
                        //if (this.fluArchivo1.HasFile)
                        //{
                        //    if (this.fluArchivo1.PostedFile.ContentLength > 4194304)
                        //    {
                        //        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño de la imagen 1 es superior a 4Mb favor de verificarlo');", true);
                        //        return;
                        //    }
                        //}
                        //if (this.fluArchivo2.HasFile)
                        //{
                        //    if (this.fluArchivo2.PostedFile.ContentLength > 4194304)
                        //    {
                        //        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño de la imagen 2 es superior a 4Mb favor de verificarlo');", true);
                        //        return;
                        //    }
                        //}
                        //if (this.fluArchivo3.HasFile)
                        //{
                        //    if (this.fluArchivo3.PostedFile.ContentLength > 4194304)
                        //    {
                        //        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño de la imagen 3 es superior a 4Mb favor de verificarlo');", true);
                        //        return;
                        //    }
                        //}

                        DataTable dtAdd = (DataTable)this.Session["datos"];
                        if (dtAdd.Rows.Count == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar por lo menos una queja');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loading", "abre_spiner()", true);
                            string text1 = this.txtFecha.Text;
                            string text2 = this.txtSemana.Text;
                            string text3 = this.txtFechaEmb.Text;
                            string tipo = this.ddlTipo.SelectedValue.ToString();
                            string text4 = this.txtPedido.Text;
                            string text5 = this.txtMes.Text.ToUpper();
                            string text6 = this.txtTransportista.Text;
                            string text7 = this.txtDestino.Text;
                            string text8 = this.txtCaja.Text;
                            string text9 = this.lblClave.Text;
                            string text10 = this.txtAduana.Text;
                            string text11 = this.lblTipo.Text;
                            string upper = this.txtComentario.Text.ToUpper();
                            string text12 = (this.txtPerdidaMn.Text == "") ? "0" : Convert.ToDecimal(this.txtPerdidaMn.Text).ToString();
                            string text13 = (this.txtPerdidaUsd.Text == "") ? "0" : Convert.ToDecimal(this.txtPerdidaUsd.Text).ToString();
                            string text14 = this.lblNombre.Text;
                            //string source = this.con.guarda_servicio(dtAdd, text1, text2, text3, tipo, text4, text5, text6, text7, text8, text9, text10, upper, text12, text13, text11, text14, "0");
                            //if (source.Contains<char>('.'))
                            //{
                            //    string[] strArray = source.Split('.');
                            //    this.lblSuccess.Visible = true;
                            //    string str1 = "http://189.206.160.206:81/quejas/";
                            //    string str2 = "http://gabira1:81/quejas/";
                            //    string cuerpo = "<table border='2'><tr><td align='center'><h2>Registro de Queja de Servicio</h2></td></tr><tr><td>Registro de queja realizado por: " + this.lblNombre.Text + "</td></tr><tr><td>CEDIS: " + this.lblCedis.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja: No. " + strArray[0].ToString() + "</td></tr><tr><td>Transportista: " + text6 + "</td></tr><tr><td>Caja: " + text8 + "</td></tr><tr><td>Pedido: " + text4.ToString() + "</td></tr></table><br /><p><h4>Ir a la seccion de Editar Queja para asignaci&oacute; de &Aacute;rea</h4></p><br /><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
                            //    this.enviarcorreo("msamano@mrlucky.com.mx", strArray[0].ToString(), cuerpo);
                            //}
                            //else if (!source.Contains<char>('.'))
                            //    this.lblParcial.Visible = true;
                            //else
                            //    this.lblWarning.Visible = true;

                            string str_file = "";
                            str_file = "~/fotos_serv/";
                            if (this.fluArchivo1.HasFile)
                            {
                                if (this.fluArchivo1.FileName.Contains(".pdf"))
                                    this.fluArchivo1.SaveAs(str_file + "1_" + this.lblQueja.Text + ".pdf");
                                else
                                {
                                    using (Bitmap bmb = new Bitmap(upload_ver.ScaleImage((System.Drawing.Image)new Bitmap(this.fluArchivo1.PostedFile.InputStream), 405)))
                                    {
                                        
                                        bmb.Save(str_file + "1_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                                    }
                                }
                                    //upload_ver.ScaleImage((System.Drawing.Image)new Bitmap(this.fluArchivo1.PostedFile.InputStream), 405).Save(str_file + "1_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                            }
                            //if (this.fluArchivo2.HasFile)
                            //{
                            //    if (this.fluArchivo2.FileName.Contains(".pdf"))
                            //        this.fluArchivo2.SaveAs(str_file + "2_" + this.lblQueja.Text + ".pdf");
                            //    else
                            //        upload_ver.ScaleImage((System.Drawing.Image)new Bitmap(this.fluArchivo2.PostedFile.InputStream), 405).Save(str_file + "2_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                            //}
                            //if (this.fluArchivo3.HasFile)
                            //{
                            //    if (this.fluArchivo3.FileName.Contains(".pdf"))
                            //        this.fluArchivo3.SaveAs(str_file + "3_" + this.lblQueja.Text + ".pdf");
                            //    else
                            //        upload_ver.ScaleImage((System.Drawing.Image)new Bitmap(this.fluArchivo3.PostedFile.InputStream), 405).Save(str_file + "3_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                            //}

                            btnSubir.Enabled = true;
                            btnGuardar.Enabled = false;
                            udpGuardar.Update();
                        }
                    }
                }
                else
                {
                    if (this.ddlTipo.SelectedValue == "")
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar el tipo (Nacional o Exportacion)');", true);
                    else if (this.ddlTransporte.SelectedValue == "")
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar el transporte');", true);
                    }
                    else if (this.fluArchivo1.HasFile)// && this.fluArchivo1.PostedFile.ContentLength > 4194304
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar por lo menos un archivo');", true);
                    }
                    else
                    {
                        if (this.fluArchivo1.HasFile)
                        {
                            if (this.fluArchivo1.PostedFile.ContentLength > 4194304)
                            {
                                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño de la imagen 1 es superior a 4Mb favor de verificarlo');", true);
                                return;
                            }
                        }
                        if (this.fluArchivo2.HasFile)
                        {
                            if (this.fluArchivo2.PostedFile.ContentLength > 4194304)
                            {
                                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño de la imagen 2 es superior a 4Mb favor de verificarlo');", true);
                                return;
                            }
                        }
                        if (this.fluArchivo3.HasFile)
                        {
                            if (this.fluArchivo3.PostedFile.ContentLength > 4194304)
                            {
                                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El tamaño de la imagen 3 es superior a 4Mb favor de verificarlo');", true);
                                return;
                            }
                        }

                        DataTable dtAdd = (DataTable)this.Session["datos"];
                        if (dtAdd.Rows.Count == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar por lo menos una queja');", true);
                        }
                        else
                        {
                            string text1 = this.txtFecha.Text;
                            string text2 = this.txtSemana.Text;
                            string text3 = this.txtFecha.Text;
                            string tipo = this.ddlTipo.SelectedValue.ToString();
                            string text4 = "";
                            string text5 = this.txtMes.Text.ToUpper();
                            string text6 = this.txtTransportista.Text;
                            string text7 = "";
                            string text8 = "";
                            string text9 = this.lblClave.Text;
                            string text10 = "";
                            string text11 = this.lblTipo.Text;
                            string upper = this.txtComentario.Text.ToUpper();
                            string text12 = (this.txtPerdidaMn.Text == "") ? "0" : Convert.ToDecimal(this.txtPerdidaMn.Text).ToString();
                            string text13 = (this.txtPerdidaUsd.Text == "") ? "0" : Convert.ToDecimal(this.txtPerdidaUsd.Text).ToString();
                            string text14 = this.lblNombre.Text;
                            string sin_reg = (this.chkSinRegistro.Checked == true) ? "1" : "0";
                            string embtracli = this.ddlArea.SelectedValue.ToString();
                            string source = this.con.guarda_servicio(dtAdd, text1, text2, text3, tipo, text4, text5, text6, text7, text8, text9, text10, upper, text12, text13, text11, text14, sin_reg, embtracli);
                            if (source.Contains<char>('.'))
                            {
                                string[] strArray = source.Split('.');
                                this.lblSuccess.Visible = true;
                                string str1 = "http://189.206.160.206:81/quejas/";
                                string str2 = "http://gabira1:81/quejas/";
                                string cuerpo = "<table border='2'><tr><td align='center'><h2>Registro de Queja de Servicio</h2></td></tr><tr><td>Registro de queja realizado por: " + this.lblNombre.Text + "</td></tr><tr><td>CEDIS: " + this.lblCedis.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja: No. " + strArray[0].ToString() + "</td></tr><tr><td>Transportista: " + text6 + "</td></tr><tr><td>Caja: " + text8 + "</td></tr><tr><td>Pedido: " + text4.ToString() + "</td></tr></table><br /><p><h4>Ir a la seccion de Editar Queja para asignaci&oacute; de &Aacute;rea</h4></p><br /><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
                                this.enviarcorreo("msamano@mrlucky.com.mx", strArray[0].ToString(), cuerpo);
                            }
                            else if (!source.Contains<char>('.'))
                                this.lblParcial.Visible = true;
                            else
                                this.lblWarning.Visible = true;

                            string str_file = "";
                            str_file = "~/fotos_serv/";
                            if (this.fluArchivo1.HasFile)
                            {
                                if (this.fluArchivo1.FileName.Contains(".pdf"))
                                    this.fluArchivo1.SaveAs(str_file + "1_" + this.lblQueja.Text + ".pdf");
                                else
                                    upload_ver.ScaleImage((System.Drawing.Image)new Bitmap(this.fluArchivo1.PostedFile.InputStream), 405).Save(str_file + "1_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                            }
                            if (this.fluArchivo2.HasFile)
                            {
                                if (this.fluArchivo2.FileName.Contains(".pdf"))
                                    this.fluArchivo2.SaveAs(str_file + "2_" + this.lblQueja.Text + ".pdf");
                                else
                                    upload_ver.ScaleImage((System.Drawing.Image)new Bitmap(this.fluArchivo2.PostedFile.InputStream), 405).Save(str_file + "2_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                            }
                            if (this.fluArchivo3.HasFile)
                            {
                                if (this.fluArchivo3.FileName.Contains(".pdf"))
                                    this.fluArchivo3.SaveAs(str_file + "3_" + this.lblQueja.Text + ".pdf");
                                else
                                    upload_ver.ScaleImage((System.Drawing.Image)new Bitmap(this.fluArchivo3.PostedFile.InputStream), 405).Save(str_file + "3_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                            }

                            btnSubir.Enabled = true;
                            btnGuardar.Enabled = false;
                            udpGuardar.Update();

                        }
                    }
                }

            }
            else if (this.con.modifica_servicio(this.lblQueja.Text, this.txtComentario.Text.ToUpper(), this.txtPerdidaMn.Text, this.txtPerdidaUsd.Text, this.ddlArea.SelectedValue.ToString()) == "1")
                this.lblSuccess.Visible = true;
            else
                this.lblWarning.Visible = true;
        }

        public void enviarcorreo(string correo, string cve_queja, string cuerpo)
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (this.ddlQueja.SelectedValue == "")
                return;
            DataTable dataTable = (DataTable)this.Session["datos"];
            DataRow[] dataRowArray = dataTable.Select("ser_folio = '" + this.ddlQueja.SelectedValue.ToString() + "'");
            int index = 0;
            if (index < dataRowArray.Length)
            {
                DataRow dataRow = dataRowArray[index];
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('La queja ya fue ingresada');", true);
            }
            else
            {
                DataRow row = dataTable.NewRow();
                row["ser_folio"] = (object)this.ddlQueja.SelectedValue.ToString();
                row["ser_nombre"] = (object)this.ddlQueja.SelectedItem.ToString();
                dataTable.Rows.Add(row);
                this.gvwQuejas.DataSource = (object)dataTable;
                this.gvwQuejas.DataBind();
                this.Session["datos"] = (object)dataTable;
                this.udpQuejas.Update();
            }
        }

        protected void gvwQuejas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(this.lblTarea.Text != "CONSULTA") || !(e.CommandName == "quitar"))
                return;
            int int32 = Convert.ToInt32(e.CommandArgument);
            DataTable dataTable = (DataTable)this.Session["datos"];
            dataTable.Rows.RemoveAt(int32);
            this.gvwQuejas.DataSource = (object)dataTable;
            this.gvwQuejas.DataBind();
            this.Session["datos"] = (object)dataTable;
            this.udpQuejas.Update();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas_serv.aspx");
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.txtFolio.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Iframe1.Attributes["src"] = "upload_serv.aspx";
            this.Iframe1.Attributes["style"] = "border:solid 1px;";
            this.Iframe1.Visible = true;
            this.UpdatePanel1.Update();
        }

        protected void file1_Click(object sender, EventArgs e)
        {
            if (!(this.file1.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_serv/" + this.file1.Text + "','_blank')</script>");
        }

        protected void file2_Click(object sender, EventArgs e)
        {
            if (!(this.file2.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_serv/" + this.file2.Text + "','_blank')</script>");
        }

        protected void file3_Click(object sender, EventArgs e)
        {
            if (!(this.file3.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_serv/" + this.file3.Text + "','_blank')</script>");
        }

        protected void chkSinRegistro_CheckedChanged(object sender, EventArgs e)
        {
            /*btnBuscar.Enabled = false;
            udpPlacas.DataBind();*/
            //BUSCAR TRANSPORTISTAS
            if (chkSinRegistro.Checked == true)
            {
                this.dtPlacas.Clear();
                this.dtPlacas = con.combo_transportistas_serv();
                this.ddlTransporte.DataSource = (object)this.dtPlacas;
                this.ddlTransporte.DataTextField = "no_trailer";
                this.ddlTransporte.DataValueField = "pdn_folio";
                this.ddlTransporte.DataBind();
                this.udpPlacas.Update();

                DateTime fch = Convert.ToDateTime(txtFecha.Text);
                string month = fch.ToString("MMMM");
                txtMes.Text = month;
                txtMes.DataBind();
                upnMes.Update();

            }
            else
            {
                this.dtPlacas.Clear();
                this.ddlTransporte.DataSource = "";
                this.ddlTransporte.DataBind();
                this.udpPlacas.Update();
            }

        }

        //protected void btnPDF_Click(object sender, EventArgs e)
        //{
        //    DataTable dtDetQ3 = (DataTable)this.Session["datos"];

        //    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 10, 10, 10, 10);
        //    //iTextSharp.text.pdf.PdfWriter write = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(@"c:\Reportes\" + txtFolio.Text + ".pdf", FileMode.Create));
        //    iTextSharp.text.pdf.PdfWriter write = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/repserv/" + txtFolio.Text + ".pdf"), FileMode.Create));
        //    doc.Open();
        //    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\logo.png");//, iTextSharp.text.BaseColor.WHITE
        //    jpg.ScaleToFit(80f, 60f);
        //    jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
        //    iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\Logo grupo U.png");//, iTextSharp.text.BaseColor.WHITE
        //    jpg2.ScaleToFit(80f, 60f);
        //    jpg2.SetAbsolutePosition(540, 720);
        //    jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
        //    doc.Add(jpg);
        //    doc.Add(jpg2);

        //    iTextSharp.text.pdf.PdfContentByte cb = write.DirectContent;
        //    iTextSharp.text.pdf.BaseFont bfTimes = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, false);
        //    iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(bfTimes, 20);

        //    iTextSharp.text.pdf.BaseFont bfTimes2 = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, false);

        //    cb.BeginText();
        //    cb.SetFontAndSize(bfTimes, 30);
        //    cb.SetTextMatrix(230, 750);
        //    cb.ShowText("Queja " + txtFolio.Text);

        //    //SUBS
        //    cb.SetFontAndSize(bfTimes, 12);
        //    cb.SetTextMatrix(25, 700);
        //    cb.ShowText("Fecha: ");

        //    cb.SetTextMatrix(200, 700);
        //    cb.ShowText("Fecha embarque: ");

        //    cb.SetTextMatrix(25, 680);
        //    cb.ShowText("Transportista: ");

        //    cb.SetTextMatrix(350, 680);
        //    cb.ShowText("Destino: ");

        //    //cb.SetTextMatrix(25, 660);
        //    //cb.ShowText("Responsable: ");

        //    cb.SetTextMatrix(25, 660);//250, 660
        //    cb.ShowText("Tipo: ");

        //    cb.SetTextMatrix(25, 640);
        //    cb.ShowText("Pedido: ");

        //    cb.SetTextMatrix(250, 620);
        //    cb.ShowText("Caja: ");

        //    cb.SetTextMatrix(25, 620);
        //    cb.ShowText("Area: ");

        //    cb.SetTextMatrix(25, 600);
        //    cb.ShowText("Perdida MN: ");

        //    cb.SetTextMatrix(250, 600);
        //    cb.ShowText("Perdida USD: ");

        //    cb.SetTextMatrix(25, 580);
        //    cb.ShowText("Comentario: ");





        //    //FIN SUBS

        //    cb.SetFontAndSize(bfTimes2, 12);
        //    cb.SetTextMatrix(65, 700);
        //    cb.ShowText(txtFecha.Text);

        //    cb.SetTextMatrix(300, 700);
        //    cb.ShowText(txtFechaEmb.Text);

        //    cb.SetTextMatrix(110, 680);
        //    cb.ShowText(this.txtTransportista.Text.ToUpper());

        //    cb.SetTextMatrix(400, 680);
        //    cb.ShowText(this.txtDestino.Text.ToUpper());

        //    //cb.SetTextMatrix(105, 660);
        //    //cb.ShowText(this.txtResponsable.Text.ToUpper());

        //    cb.SetTextMatrix(105, 660);//280, 660
        //    cb.ShowText(this.ddlTipo.SelectedItem.Text);

        //    cb.SetTextMatrix(75, 640);
        //    cb.ShowText(this.txtPedido.Text);

        //    cb.SetTextMatrix(60, 620);
        //    cb.ShowText(this.ddlArea.SelectedItem.Text);

        //    cb.SetTextMatrix(280, 620);
        //    cb.ShowText(this.txtCaja.Text);

        //    cb.SetFontAndSize(bfTimes2, 12);
        //    cb.SetTextMatrix(105, 600);
        //    cb.ShowText(this.txtPerdidaMn.Text);

        //    cb.SetTextMatrix(330, 600);
        //    cb.ShowText(this.txtPerdidaUsd.Text);

        //    string cad = this.txtComentario.Text.Replace("\n", " ");
        //    string cad1 = "", cad2 = "", cad3 = "", cad4 = "", cad5 = "", cad6 = "";
        //    if (cad.Length > 500)
        //    {
        //        cad1 = cad.Substring(0, 100);
        //        cad2 = cad.Substring(100, 100);
        //        cad3 = cad.Substring(200, 100);
        //        cad4 = cad.Substring(300, 100);
        //        cad5 = cad.Substring(400, 100);
        //        cad6 = cad.Substring(500, (cad.Length - 500));
        //    }
        //    if (cad.Length > 400 && cad.Length < 501)
        //    {
        //        cad1 = cad.Substring(0, 100);
        //        cad2 = cad.Substring(100, 100);
        //        cad3 = cad.Substring(200, 100);
        //        cad4 = cad.Substring(300, 100);
        //        cad5 = cad.Substring(400, (cad.Length - 400));
        //    }
        //    if (cad.Length > 300 && cad.Length < 401)
        //    {
        //        cad1 = cad.Substring(0, 100);
        //        cad2 = cad.Substring(100, 100);
        //        cad3 = cad.Substring(200, 100);
        //        cad4 = cad.Substring(300, (cad.Length - 300));
        //    }
        //    if (cad.Length > 200 && cad.Length < 301)
        //    {
        //        cad1 = cad.Substring(0, 100);
        //        cad2 = cad.Substring(100, 100);
        //        cad3 = cad.Substring(200, (cad.Length - 200));
        //    }
        //    if (cad.Length > 100 && cad.Length < 201)
        //    {
        //        cad1 = cad.Substring(0, 100);
        //        cad2 = cad.Substring(100, (cad.Length - 100));
        //    }
        //    if (cad.Length < 101)
        //    {
        //        cad1 = cad;
        //    }

        //    cb.SetFontAndSize(bfTimes2, 8);
        //    cb.SetTextMatrix(105, 580);
        //    cb.ShowText(cad1);

        //    if (cad2 != "")
        //    {
        //        cb.SetFontAndSize(bfTimes2, 8);
        //        cb.SetTextMatrix(105, 570);
        //        cb.ShowText(cad2);
        //    }
        //    if (cad3 != "")
        //    {
        //        cb.SetFontAndSize(bfTimes2, 8);
        //        cb.SetTextMatrix(105, 560);
        //        cb.ShowText(cad3);
        //    }
        //    if (cad4 != "")
        //    {
        //        cb.SetFontAndSize(bfTimes2, 8);
        //        cb.SetTextMatrix(105, 550);
        //        cb.ShowText(cad4);
        //    }
        //    if (cad5 != "")
        //    {
        //        cb.SetFontAndSize(bfTimes2, 8);
        //        cb.SetTextMatrix(105, 540);
        //        cb.ShowText(cad5);
        //    }
        //    if (cad6 != "")
        //    {
        //        cb.SetFontAndSize(bfTimes2, 8);
        //        cb.SetTextMatrix(105, 530);
        //        cb.ShowText(cad6);
        //    }






        //    //Creacion de la tabla de las quejas registradas
        //    iTextSharp.text.pdf.PdfPTable table2 = crear_tabla();
        //    //table.TotalWidth = 560f
        //    table2.SetWidths(new float[] { 570f });
        //    //table2.TotalWidth = 560f;
        //    cb.SetFontAndSize(bfTimes2, 10);
        //    iTextSharp.text.pdf.PdfPCell cell;
        //    iTextSharp.text.Font fuente = new iTextSharp.text.Font(bfTimes2, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
        //    foreach (DataRow rt in dtDetQ3.Rows)
        //    {
        //        cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(rt["ser_nombre"].ToString(), fuente));
        //        //cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //        table2.AddCell(cell);
        //    }
        //    table2.WriteSelectedRows(0, -1, 25, 520, cb);

        //    string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_serv/"));
        //    List<ListItem> listItemList = new List<ListItem>();
        //    foreach (string path in files)
        //    {
        //        if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".jpg"))
        //            listItemList.Add(new ListItem(Path.GetFileName(path), path));
        //    }

        //    if (listItemList.Count > 0)
        //    {
        //        int AxisX = 25;
        //        int i = 1;
        //        foreach (ListItem listItem in listItemList)
        //        {
        //            if (i == 1)
        //            {
        //                string file_name = listItem.ToString();
        //                iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_serv\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
        //                jpg_1.ScaleToFit(230f, 210f);
        //                jpg_1.SetAbsolutePosition(AxisX, 100);
        //                doc.Add(jpg_1);
        //                AxisX = AxisX + 190;
        //            }
        //            if (i == 2)
        //            {
        //                string file_name = listItem.ToString();
        //                iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_serv\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
        //                jpg_2.ScaleToFit(230f, 210f);
        //                jpg_2.SetAbsolutePosition(AxisX, 100);
        //                doc.Add(jpg_2);
        //                AxisX = AxisX + 190;
        //            }
        //            if (i == 3)
        //            {
        //                string file_name = listItem.ToString();
        //                iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_serv\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
        //                jpg_3.ScaleToFit(230f, 210f);
        //                jpg_3.SetAbsolutePosition(AxisX, 100);
        //                doc.Add(jpg_3);
        //            }

        //            i++;
        //        }
        //    }

        //    cb.EndText();

        //    doc.Close();

        //    //this.Response.Write("<script>window.open('repserv/" + txtFolio.Text + ".pdf','_blank')</script>");
        //    //this.Response.Write("<script>window.open('rep/" + txtFolio.Text + ".pdf','_blank')</script>");
        //    ScriptManager.RegisterStartupScript(this, GetType(), "openpdf", "abre_pdf();", true);
        //}

        //public iTextSharp.text.pdf.PdfPTable crear_tabla()
        //{
        //    iTextSharp.text.pdf.BaseFont bfTimes3 = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, false);
        //    iTextSharp.text.Font fuente = new iTextSharp.text.Font(bfTimes3, 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
        //    iTextSharp.text.pdf.PdfPTable tbl = new iTextSharp.text.pdf.PdfPTable(1);
        //    tbl.TotalWidth = 570;
        //    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Detalle de quejas", fuente));
        //    //cell.Colspan = 6;
        //    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
        //    tbl.AddCell(cell);

        //    //cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Descripción", fuente));
        //    //tbl.AddCell(cell);

        //    return tbl;
        //}

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            //double num = (double)maxHeight / (double)image.Height;
            //int width = (int)((double)image.Width * num);
            //int height = (int)((double)image.Height * num);
            //Bitmap bitmap = new Bitmap(width, height);
            //using (Graphics graphics = Graphics.FromImage((System.Drawing.Image)bitmap)) {
            //    graphics.DrawImage(image, 0, 0, width, height);
            //    graphics.Dispose();
            //}
            //return (System.Drawing.Image)bitmap;

            double num = (double)maxHeight / (double)image.Height;
            int width = (int)((double)image.Width * num);
            int height = (int)((double)image.Height * num);
            Bitmap bitmap = new Bitmap(image);
            Bitmap bitmap2 = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage((System.Drawing.Image)bitmap2))
            {
                graphics.DrawImage(bitmap, 0, 0, width, height);
                graphics.Dispose();
            }
            bitmap.Dispose();
            return (System.Drawing.Image)bitmap2;
        }
    }
}