using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

namespace queja
{
    public partial class editar_queja : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtMstrQueja = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        private DataTable dtClientes = new DataTable();
        private DataTable dtProblemas = new DataTable();
        private DataTable dtAreas = new DataTable();
        private DataTable dtOrdenes = new DataTable();
        private DataTable dtDatos = new DataTable();
        private DataTable dtSucursales = new DataTable();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
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
            this.dtAreas = this.con.areas_queja();
            this.ddlAreaQueja.DataSource = (object)this.dtAreas;
            this.ddlAreaQueja.DataTextField = "area_nombre";
            this.ddlAreaQueja.DataValueField = "id_area";
            this.ddlAreaQueja.DataBind();
            this.txtFolio.Text = this.Session["queja"].ToString();
            string valor = this.Session["queja"].ToString();
            this.dtMstrQueja = this.con.consultaqueja(valor);
            this.txtFolio.Text = this.dtMstrQueja.Rows[0]["que_folio"].ToString();
            this.txtSemana.Text = this.dtMstrQueja.Rows[0]["que_semana"].ToString();
            this.txtMes.Text = this.dtMstrQueja.Rows[0]["que_mes"].ToString();
            this.txtFecha.Text = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
            this.ddlCliente.SelectedValue = this.dtMstrQueja.Rows[0]["que_cliprim"].ToString();
            this.txtSucursal.Text = this.dtMstrQueja.Rows[0]["que_sucursal"].ToString();
            this.ddlCliente_SelectedIndexChanged(sender, e);
            this.ddlSucursales.SelectedValue = this.dtMstrQueja.Rows[0]["subcli_folio"].ToString();
            this.txtReporto.Text = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
            this.txtReporto.Text = this.dtMstrQueja.Rows[0]["recibio"].ToString();
            this.ddlTipo.SelectedValue = this.dtMstrQueja.Rows[0]["que_tipo"].ToString() == "N" ? "N" : "E";
            this.ddlAreaQueja.SelectedValue = this.dtMstrQueja.Rows[0]["area_queja"].ToString();
            this.txtPedido.Text = this.dtMstrQueja.Rows[0]["que_pedido"].ToString();
            this.txtObservaciones.Text = this.dtMstrQueja.Rows[0]["que_observacion"].ToString();
            this.txtCosto.Text = this.dtMstrQueja.Rows[0]["que_costo"].ToString() == "" ? "0" : this.dtMstrQueja.Rows[0]["que_costo"].ToString();
            this.dtDetQueja = this.con.consultadetqueja(valor);
            this.txt_orden_prod.Text = this.dtDetQueja.Rows[0]["qud_ordprod"].ToString();
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
            
            this.chkDevolucion.Checked = (this.dtDetQueja.Rows[0]["qud_devolucion"].ToString() == "1") ? true : false ;
            
            this.ddlMoneda.SelectedValue = this.dtDetQueja.Rows[0]["qud_moneda"].ToString() == "PESOS" ? "PESOS" : "DOLARES";
            this.ddlProblema.SelectedValue = this.dtDetQueja.Rows[0]["qud_problema"].ToString();
            this.txtCajasProducidas.Text = this.dtDetQueja.Rows[0]["qud_cjsprod"].ToString();
            this.txtPorcentaje.Text = this.dtDetQueja.Rows[0]["qud_porcen"].ToString();
            
            this.chkMerma.Checked = (this.dtDetQueja.Rows[0]["qud_merma"].ToString() == "1") ? true : false;

            string Dev = this.dtDetQueja.Rows[0]["qud_devolucion"].ToString();
            string Mer = this.dtDetQueja.Rows[0]["qud_merma"].ToString();
            string Bon = this.dtDetQueja.Rows[0]["qud_bonificacion"].ToString();

            this.Session["ptcptp"] = this.dtDetQueja.Rows[0]["qud_ptcptp"].ToString(); 

            if (Dev == "1")
            {
                rbtList.SelectedValue = "DEV";
            }
            if (Mer == "1")
            {
                rbtList.SelectedValue = "MER";
            }
            if (Bon == "1")
            {
                rbtList.SelectedValue = "BON";
            }
            
            string carpeta = "~/fotos_ant/";
            carpeta = Server.MapPath(carpeta);
            var existe = Directory.Exists(carpeta);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
            string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"));
            List<ListItem> listItemList = new List<ListItem>();
            List<ListItem> listItemListImage = new List<ListItem>();

            foreach (string path in files)
            {
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".pdf"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));

                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".jpg"))
                    listItemListImage.Add(new ListItem(Path.GetFileName(path), path));
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".jpeg"))
                    listItemListImage.Add(new ListItem(Path.GetFileName(path), path));
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".png"))
                    listItemListImage.Add(new ListItem(Path.GetFileName(path), path));
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

            foreach (ListItem listItem in listItemListImage)
            {
                if (listItem.ToString().Contains("1_"))
                {
                    if (listItem.ToString().Contains(".jpg"))
                    {
                        string str2 = "fotos_ant/1_" + valor + ".jpg";
                        this.img1.ImageUrl = str2;
                    }
                    else if (listItem.ToString().Contains(".jpeg"))
                    {
                        string str2 = "fotos_ant/1_" + valor + ".jpeg";
                        this.img1.ImageUrl = str2;
                    }
                    else
                    {
                        string str2 = "fotos_ant/1_" + valor + ".png";
                        this.img1.ImageUrl = str2;
                    }
                }
                if (listItem.ToString().Contains("2_"))
                {
                    if (listItem.ToString().Contains(".jpg"))
                    {
                        string str2 = "fotos_ant/2_" + valor + ".jpg";
                        this.img1.ImageUrl = str2;
                    }
                    else if (listItem.ToString().Contains(".jpeg"))
                    {
                        string str2 = "fotos_ant/2_" + valor + ".jpeg";
                        this.img1.ImageUrl = str2;
                    }
                    else
                    {
                        string str2 = "fotos_ant/2_" + valor + ".png";
                        this.img1.ImageUrl = str2;
                    }
                }
                if (listItem.ToString().Contains("3_"))
                {
                    if (listItem.ToString().Contains(".jpg"))
                    {
                        string str2 = "fotos_ant/3_" + valor + ".jpg";
                        this.img1.ImageUrl = str2;
                    }
                    if (listItem.ToString().Contains(".jpeg"))
                    {
                        string str2 = "fotos_ant/3_" + valor + ".jpeg";
                        this.img1.ImageUrl = str2;
                    }
                    else
                    {
                        string str2 = "fotos_ant/3_" + valor + ".png";
                        this.img1.ImageUrl = str2;
                    }
                }
            }

            //string str = "fotos_ant/1_" + valor + ".jpg";
            //this.img1.ImageUrl = str;
            //this.img2.ImageUrl = "fotos_ant/2_" + valor + ".jpg";
            //this.img3.ImageUrl = "fotos_ant/3_" + valor + ".jpg";
            //this.Image1.ImageUrl = str;
            //this.Image2.ImageUrl = "fotos_ant/2_" + valor + ".jpg";
            //this.Image3.ImageUrl = "fotos_ant/3_" + valor + ".jpg";
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.ddlCliente.Enabled = true;
            this.txtSucursal.Enabled = true;
            this.ddlTipo.Enabled = true;
            this.chkDevolucion.Enabled = true;
            this.ddlProblema.Enabled = true;
            if (this.lblClave.Text == "10")
                this.ddlAreaQueja.Enabled = true;
            this.btnGuardar.Enabled = true;
            this.upnGuardar.Update();
            this.btnSubir.Enabled = true;
            this.btnTarima.Enabled = true;
            this.upnBotones.Update();
            this.ddlAreaQueja.Enabled = true;
            //this.txtPedido.Enabled = true;
            this.txtObservaciones.Enabled = true;
            this.txtCosto.Enabled = true;
            this.chkMerma.Enabled = true;
            this.txtCantRech.Enabled = true;

            this.rbtList.Enabled = true;

            this.upnEdicion.Update();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string text1 = this.txtFolio.Text;
            string text2 = this.txtSemana.Text;
            string text3 = this.txtFecha.Text;
            string text4 = this.txtMes.Text;
            string selectedValue1 = this.ddlCliente.SelectedValue;
            string text5 = this.ddlCliente.SelectedItem.Text;
            string upper1 = this.txtSucursal.Text.ToUpper();
            this.txtReporto.Text.ToUpper();
            string text6 = this.lblClave.Text;
            string text7 = this.lblCedis.Text;
            string text8 = this.txt_clave.Text;
            string problema = this.ddlProblema.SelectedItem.Value;
            string text9 = this.txt_orden_prod.Text;
            string text10 = this.txtArea.Text;
            string text11 = this.txtRespLinea.Text;
            string text12 = this.txtCantRech.Text;
            string text13 = this.txtCantReci.Text;
            string upper2 = this.txtUnidad.Text.ToUpper();
            string devolucion = this.chkDevolucion.Checked ? "1" : "0";
            string selectedValue2 = this.ddlMoneda.SelectedValue;
            string text14 = this.txt_cveprov.Text;
            string text15 = this.txt_cverch.Text;
            string text16 = this.txt_cvetbl.Text;
            string text17 = this.txt_lote.Text;
            string text18 = this.txt_nom.Text;
            string text19 = this.txt_fechacad.Text;
            string selectedValue3 = this.ddlTipo.SelectedValue;
            string area = this.ddlAreaQueja.SelectedItem.Value;
            string text20 = this.txtPedido.Text;
            string text21 = this.txtObservaciones.Text;
            string text22 = this.txtCosto.Text;
            string merma = this.chkDevolucion.Checked ? "1" : "0";

            string porce = this.txtPorcentaje.Text;

            //mandar text12 y porce
            string textDev = (rbtList.SelectedValue == "DEV") ? "1" : "0";
            string textMer = (rbtList.SelectedValue == "MER") ? "1" : "0";
            string textBon = (rbtList.SelectedValue == "BON") ? "1" : "0";

            devolucion = textDev;
            merma = textMer;

            if (this.con.modificarmaestro(text1, selectedValue1, text5, upper1, selectedValue3, area, text20, text21, text22) == "1")
            {
                if (this.con.modificadetalle(text1, text8, problema, text9, text10, text11, text12, text13, upper2, devolucion, selectedValue2, text14, text15, text16, "", text17, text18, text19, merma, porce, textBon) == "2")
                {
                    this.lblSuccess.Visible = true;
                    this.upnMensajes.Update();
                }
                else
                {
                    this.lblWarning.Visible = true;
                    this.upnMensajes.Update();
                }
            }
            else
            {
                this.lblWarning.Visible = true;
                this.upnMensajes.Update();
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

        protected void file1_Click(object sender, EventArgs e)
        {
            if (!(this.file1.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_ant/" + this.file1.Text + "','_blank')</script>");
        }

        protected void file2_Click(object sender, EventArgs e)
        {
            if (!(this.file2.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_ant/" + this.file2.Text + "','_blank')</script>");
        }

        protected void file3_Click(object sender, EventArgs e)
        {
            if (!(this.file3.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_ant/" + this.file3.Text + "','_blank')</script>");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //this.lblDanger.Visible = false;
            //this.lblSuccess.Visible = false;
            //this.lblWarning.Visible = false;
            //this.lblCambio.Visible = false;
        }

        protected void btnTarima_Click(object sender, EventArgs e)
        {
            if (!(this.con.validacion_tarima(this.lblQueja.Text) == "0"))
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

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dtSucursales.Clear();
            this.dtSucursales = this.con.cargarsucursales(this.ddlCliente.SelectedValue.ToString(), this.con.trer_cedis(this.lblQueja.Text));
            this.ddlSucursales.DataSource = (object)this.dtSucursales;
            this.ddlSucursales.DataTextField = "subcli_nombre";
            this.ddlSucursales.DataValueField = "subcli_folio";
            this.ddlSucursales.DataBind();
            this.upnSucursales.Update();
        }

        protected void txtCantReci_TextChanged(object sender, EventArgs e)
        {
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

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            //var doc1 = new iTextSharp.text.Document();
            //iTextSharp.text.pdf.PdfWriter.GetInstance(doc1, new FileStream("C://Reportes/Doc1.pdf", FileMode.Create));
            //doc1.Open();
            //doc1.Add(new iTextSharp.text.Paragraph());
            //doc1.AddTitle("Queja " + txtFolio.Text);
            //doc1.Close();

            //FileStream fs = new FileStream("C://Reportes/Doc1.pdf", FileMode.Create);
            //iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 25, 25, 30, 30);
            //iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
            //document.Open();
            //document.AddTitle("Queja " + txtFolio.Text);
            //document.Add(new iTextSharp.text.Paragraph(" ")); 
            //document.Close();
            //writer.Close();
            //fs.Close();

            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 10, 10, 10, 10);
            //iTextSharp.text.pdf.PdfWriter write = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(@"c:\Reportes\" + txtFolio.Text + ".pdf", FileMode.Create));
            iTextSharp.text.pdf.PdfWriter write = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/rep/" + txtFolio.Text + ".pdf"), FileMode.Create));
            doc.Open();
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\logo.png");//, iTextSharp.text.BaseColor.WHITE
            jpg.ScaleToFit(80f, 60f);
            jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\Logo grupo U.png");//, iTextSharp.text.BaseColor.WHITE
            jpg2.ScaleToFit(80f, 60f);
            jpg2.SetAbsolutePosition(540, 720);
            jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            doc.Add(jpg);
            doc.Add(jpg2);

            iTextSharp.text.pdf.PdfContentByte cb = write.DirectContent;
            iTextSharp.text.pdf.BaseFont bfTimes = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, false);
            iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(bfTimes, 20);

            iTextSharp.text.pdf.BaseFont bfTimes2 = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, false);

            cb.BeginText();
            cb.SetFontAndSize(bfTimes, 30);
            cb.SetTextMatrix(230, 750);
            cb.ShowText("Queja " + txtFolio.Text);

            //SUBS
            cb.SetFontAndSize(bfTimes, 12);
            cb.SetTextMatrix(25, 700);
            cb.ShowText("Fecha: ");

            cb.SetTextMatrix(25, 680);
            cb.ShowText("Cliente: ");

            cb.SetTextMatrix(350, 680);
            cb.ShowText("Sucursal: ");

            cb.SetTextMatrix(25, 660);
            cb.ShowText("Reportó: ");

            cb.SetTextMatrix(250, 660);
            cb.ShowText("Tipo: ");

            cb.SetTextMatrix(25, 630);
            cb.ShowText("Ord Prod: ");

            cb.SetTextMatrix(135, 630);
            cb.ShowText("Clave: ");

            cb.SetTextMatrix(255, 630);
            cb.ShowText("Producto: ");

            cb.SetTextMatrix(25, 610);
            cb.ShowText("Lote: ");

            cb.SetTextMatrix(100, 610);
            cb.ShowText("Fecha Caducidad: ");

            if (this.Session["ptcptp"].ToString() == "PTP")
            {
                cb.SetTextMatrix(320, 610);
                cb.ShowText("Fecha Orden Producción: ");
            }
            else
            {
                cb.SetTextMatrix(320, 610);
                cb.ShowText("Fecha Recepción: ");
            }


            cb.SetTextMatrix(25, 590);
            cb.ShowText("Proveedor: ");

            cb.SetTextMatrix(270, 590);
            cb.ShowText("Rancho: ");

            cb.SetTextMatrix(420, 590);
            cb.ShowText("Tabla: ");

            cb.SetTextMatrix(25, 570);
            cb.ShowText("Responsable: ");

            cb.SetTextMatrix(25, 550);
            cb.ShowText("Línea: ");

            cb.SetTextMatrix(25, 530);
            cb.ShowText("Cant Recibida: ");

            cb.SetTextMatrix(150, 530);
            cb.ShowText("Cant Rechazada: ");

            cb.SetTextMatrix(280, 530);
            cb.ShowText("Cajas Producidas: ");

            cb.SetTextMatrix(430, 530);
            cb.ShowText("Rechazado: ");

            cb.SetTextMatrix(25, 510);
            cb.ShowText("Tipo de Rechazo: ");

            cb.SetTextMatrix(25, 490);
            cb.ShowText("Moneda: ");

            cb.SetTextMatrix(150, 490);
            cb.ShowText("Problema: ");

            cb.SetTextMatrix(25, 470);
            cb.ShowText("Pedido: ");

            cb.SetTextMatrix(150, 470);
            cb.ShowText("Costo: ");

            cb.SetTextMatrix(25, 450);
            cb.ShowText("Observaciones: ");

            //FIN SUBS

            cb.SetFontAndSize(bfTimes2, 12);
            cb.SetTextMatrix(65, 700);
            cb.ShowText(txtFecha.Text);

            cb.SetTextMatrix(70, 680);
            cb.ShowText(this.ddlCliente.SelectedItem.Text);

            cb.SetTextMatrix(410, 680);
            cb.ShowText(this.txtSucursal.Text.ToUpper());

            cb.SetTextMatrix(75, 660);
            cb.ShowText(this.txtReporto.Text.ToUpper());

            cb.SetTextMatrix(280, 660);
            cb.ShowText(this.ddlTipo.SelectedItem.Text);

            cb.SetTextMatrix(85, 630);
            cb.ShowText(this.txt_orden_prod.Text);

            cb.SetTextMatrix(174, 630);
            cb.ShowText(this.txt_clave.Text);

            cb.SetTextMatrix(313, 630);
            cb.ShowText(this.txt_nom.Text);

            cb.SetTextMatrix(55, 610);
            cb.ShowText(this.txt_lote.Text);

            cb.SetTextMatrix(205, 610);
            cb.ShowText(this.txt_fechacad.Text);

            if (this.Session["ptcptp"].ToString() == "PTP")
            {
                string fecha_ordp = Convert.ToDateTime(con.fecha_orden_produccion(txt_orden_prod.Text, this.Session["ptcptp"].ToString())).ToString("dd/MM/yyyy");
                cb.SetTextMatrix(470, 610);
                cb.ShowText(fecha_ordp);
            }
            else
            {
                string fecha_ordp = Convert.ToDateTime(con.fecha_orden_produccion(txt_orden_prod.Text, this.Session["ptcptp"].ToString())).ToString("dd/MM/yyyy");
                cb.SetTextMatrix(460, 610);
                cb.ShowText(fecha_ordp);
            }

            cb.SetTextMatrix(90, 590);
            cb.ShowText(this.txt_nomprov.Text);

            cb.SetTextMatrix(320, 590);
            cb.ShowText(this.txt_nomrch.Text);

            cb.SetTextMatrix(457, 590);
            cb.ShowText(this.txt_nomtbl.Text);

            cb.SetTextMatrix(105, 570);
            cb.ShowText(this.txtRespLinea.Text);

            cb.SetTextMatrix(58, 550);
            cb.ShowText(this.txtArea.Text);

            cb.SetTextMatrix(110, 530);
            cb.ShowText(this.txtCantReci.Text);

            cb.SetTextMatrix(250, 530);
            cb.ShowText(this.txtCantRech.Text);

            cb.SetTextMatrix(385, 530);
            cb.ShowText(this.txtCajasProducidas.Text);

            cb.SetTextMatrix(500, 530);
            cb.ShowText(this.txtPorcentaje.Text + "%");

            cb.SetTextMatrix(125, 510);
            if (rbtList.SelectedValue == "DEV")
                cb.ShowText("DEVOLUCION");
            else if (rbtList.SelectedValue == "MER")
                cb.ShowText("MERMA");
            else if (rbtList.SelectedValue == "BON")
                cb.ShowText("BONIFICACION");
            else
                cb.ShowText("NO APLICA");

            cb.SetTextMatrix(80, 490);
            cb.ShowText(this.ddlMoneda.SelectedItem.Text);

            cb.SetTextMatrix(210, 490);
            cb.ShowText(this.ddlProblema.SelectedItem.Text);

            cb.SetTextMatrix(75, 470);
            cb.ShowText(this.txtPedido.Text);

            cb.SetTextMatrix(195, 470);
            cb.ShowText("$" + this.txtCosto.Text);

            string cadena = txtObservaciones.Text.ToUpper();//108 txtObservaciones.Text.Replace("\n", " ");
            string cad1 = "";
            string cad2 = "";
            string cad3 = "";
            string cad4 = "";

            if (cadena.Length <= 60)
            {
                cad1 = cadena;
                cb.SetTextMatrix(120, 450);
                cb.ShowText(cad1);
            }
            else if (cadena.Length > 60 && cadena.Length <= 120)
            {
                cad1 = cadena.Substring(0, 50);
                cb.SetTextMatrix(120, 450);
                cb.ShowText(cad1);
                cad2 = cadena.Substring(50, (cadena.Length - cad1.Length));
                cb.SetTextMatrix(25, 430);
                cb.ShowText(cad2);
            }
            else if (cadena.Length > 120 && cadena.Length <= 180)
            {
                cad1 = cadena.Substring(0, 60);
                int cad_2 = cadena.Length - cad1.Length;
                int cad_3 = 0;
                if (cad_2 <= 60)
                    cad2 = cadena.Substring(60, cad_2);
                else
                {
                    cad2 = cadena.Substring(60, 60);
                    cad_3 = cadena.Length - (cad1.Length + cad2.Length);
                    cad3 = cadena.Substring(120, cad_3);
                }
                cb.SetTextMatrix(120, 450);
                cb.ShowText(cad1);
                cb.SetTextMatrix(120, 430);
                cb.ShowText(cad2);
                if (cad3 != "")
                {
                    cb.SetTextMatrix(120, 410);
                    cb.ShowText(cad3);
                }

            }
            else if (cadena.Length > 180)
            {
                cad1 = cadena.Substring(0, 60);
                cad2 = cadena.Substring(60, 60);
                cad3 = cadena.Substring(120, 60);
                cad4 = cadena.Substring(180, cadena.Length - 180);
                cb.SetTextMatrix(120, 450);
                cb.ShowText(cad1.TrimStart());
                cb.SetTextMatrix(120, 430);
                cb.ShowText(cad2.TrimStart());
                cb.SetTextMatrix(120, 410);
                cb.ShowText(cad3.TrimStart());
                cb.SetTextMatrix(120, 390);
                cb.ShowText(cad4.TrimStart());
            }

            string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"));
            List<ListItem> listItemList = new List<ListItem>();
            foreach (string path in files)
            {
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".jpg"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));
                else if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".jpeg"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));
                else if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".png"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));
            }
            int AxisX = 25;
            int i = 1;
            foreach (ListItem listItem in listItemList)
            {
                if (i == 1)
                {
                    string file_name = listItem.ToString();
                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
                    jpg_1.ScaleToFit(230f, 210f);
                    jpg_1.SetAbsolutePosition(AxisX, 100);
                    doc.Add(jpg_1);
                    AxisX = AxisX + 190;
                }
                if (i == 2)
                {
                    string file_name = listItem.ToString();
                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
                    jpg_2.ScaleToFit(230f, 210f);
                    jpg_2.SetAbsolutePosition(AxisX, 100);
                    doc.Add(jpg_2);
                    AxisX = AxisX + 190;
                }
                if (i == 3)
                {
                    string file_name = listItem.ToString();
                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
                    jpg_3.ScaleToFit(230f, 210f);
                    jpg_3.SetAbsolutePosition(AxisX, 100);
                    doc.Add(jpg_3);
                }

                i++;
            }
            cb.EndText();

            doc.Close();

            this.Response.Write("<script>window.open('rep/" + txtFolio.Text + ".pdf','_blank')</script>");
        }
    }
}