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
    public partial class editar_queja_exp : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtPlacas = new DataTable();
        private DataTable dtEmbarques = new DataTable();
        private DataTable dtTrazabilidad = new DataTable();
        private DataTable dtTabla = new DataTable();
        private DataTable dtProblemas = new DataTable();
        private DataTable dtOrdenes = new DataTable();
        private DataTable dtDatos = new DataTable();
        private DataTable dtClientes = new DataTable();
        private DataTable dtResponsables = new DataTable();
        private DataTable dtSucursales = new DataTable();
        private DataTable dtAreas = new DataTable();
        private DataTable dtMstrQueja = new DataTable();
        private DataTable dtDetQueja = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            string str1 = this.Session["queja"].ToString();

            this.txtFolio.Text = this.Session["queja"].ToString();
            if (!Page.IsPostBack)
            {
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
                
                
                this.dtMstrQueja = this.con.consultaqueja(str1);
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
                this.ddlMoneda.SelectedValue = this.dtMstrQueja.Rows[0]["que_tipo"].ToString() == "N" ? "PESOS" : "DOLARES";
                this.ddlAreaQueja.SelectedValue = this.dtMstrQueja.Rows[0]["area_queja"].ToString();
                this.txtCosto.Text = this.dtMstrQueja.Rows[0]["que_costo"].ToString() != "" ? this.dtMstrQueja.Rows[0]["que_costo"].ToString() : "0";
                this.txtTransporte.Text = this.dtMstrQueja.Rows[0]["que_transporte"].ToString();
                this.txtPedido.Text = this.dtMstrQueja.Rows[0]["que_pedido"].ToString();
                this.txtObservaciones.Text = this.dtMstrQueja.Rows[0]["que_observacion"].ToString();
                this.dtDetQueja = this.con.consulta_productos_exp(str1);
                this.grvDetalle.DataSource = (object)this.dtDetQueja;
                this.grvDetalle.DataBind();
                this.ddlProblema.SelectedValue = this.dtDetQueja.Rows[0]["qud_problema"].ToString();
                Session["dtDetQ"] = this.dtDetQueja;
                //this.chkDevolucion.Checked = this.con.consulta_devolucion_exp(str1) == "1";
                //this.chkMerma.Checked = this.con.consulta_merma_exp(str1) == "1";

                string consulta_tipo_rechazo = this.con.consulta_devolucion_exp(str1);
                if (consulta_tipo_rechazo != "")
                {
                    string[] corta = consulta_tipo_rechazo.Split('-');
                    string cortado = corta[0];
                    string cortado2 = corta[1];
                    //string Dev = this.dtDetQueja.Rows[0]["qud_devolucion"].ToString();
                    //string Mer = this.dtDetQueja.Rows[0]["qud_merma"].ToString();
                    //string Bon = this.dtDetQueja.Rows[0]["qud_bonificacion"].ToString();

                    if (cortado == "DEV" && cortado2 == "1")
                    {
                        rbtList.SelectedValue = "DEV";
                    }
                    if (cortado == "MER" && cortado2 == "1")
                    {
                        rbtList.SelectedValue = "MER";
                    }
                    if (cortado == "BON" && cortado2 == "1")
                    {
                        rbtList.SelectedValue = "BON";
                    }
                    if (cortado == "NA" && cortado2 == "1")
                    {
                        rbtList.SelectedValue = "NA";
                    }
                }
                
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
                        string str2 = "fotos_ant/1_" + str1 + ".jpg";
                        this.img1.ImageUrl = str2;
                    }
                    else if (listItem.ToString().Contains(".jpeg"))
                    {
                        string str2 = "fotos_ant/1_" + str1 + ".jpeg";
                        this.img1.ImageUrl = str2;
                    }
                    else
                    {
                        string str2 = "fotos_ant/1_" + str1 + ".png";
                        this.img1.ImageUrl = str2;
                    }
                }
                if (listItem.ToString().Contains("2_"))
                {
                    if (listItem.ToString().Contains(".jpg"))
                    {
                        string str2 = "fotos_ant/2_" + str1 + ".jpg";
                        this.img1.ImageUrl = str2;
                    }
                    else if (listItem.ToString().Contains(".jpeg"))
                    {
                        string str2 = "fotos_ant/2_" + str1 + ".jpeg";
                        this.img1.ImageUrl = str2;
                    }
                    else
                    {
                        string str2 = "fotos_ant/2_" + str1 + ".png";
                        this.img1.ImageUrl = str2;
                    }
                }
                if (listItem.ToString().Contains("3_"))
                {
                    if (listItem.ToString().Contains(".jpg"))
                    {
                        string str2 = "fotos_ant/3_" + str1 + ".jpg";
                        this.img1.ImageUrl = str2;
                    }
                    if (listItem.ToString().Contains(".jpeg"))
                    {
                        string str2 = "fotos_ant/3_" + str1 + ".jpeg";
                        this.img1.ImageUrl = str2;
                    }
                    else
                    {
                        string str2 = "fotos_ant/3_" + str1 + ".png";
                        this.img1.ImageUrl = str2;
                    }
                }
                //    this.file1.Text = listItem.ToString();
                //if (num == 2)
                //    this.file2.Text = listItem.ToString();
                //if (num == 3)
                //    this.file3.Text = listItem.ToString();
                //++num;
            }

            //string str2 = "fotos_ant/1_" + str1 + ".jpg";
            //this.img1.ImageUrl = str2;
            //this.img2.ImageUrl = "fotos_ant/2_" + str1 + ".jpg";
            //this.img3.ImageUrl = "fotos_ant/3_" + str1 + ".jpg";

            //this.Image1.ImageUrl = str2;
            //this.Image2.ImageUrl = "fotos_ant/2_" + str1 + ".jpg";
            //this.Image3.ImageUrl = "fotos_ant/3_" + str1 + ".jpg";
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

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //this.lblDanger.Visible = false;
            //this.lblSuccess.Visible = false;
            //this.lblWarning.Visible = false;
            //this.lblCambio.Visible = false;
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.chkDevolucion.Enabled = true;
            this.ddlMoneda.Enabled = true;
            this.txtCosto.Enabled = true;
            this.ddlAreaQueja.Enabled = true;
            this.ddlProblema.Enabled = true;
            this.txtObservaciones.Enabled = true;
            this.uplControles.Update();
            this.btnGuardar.Enabled = true;
            this.upnGuardar.Update();
            this.btnSubir.Enabled = true;
            this.chkMerma.Enabled = true;

            this.rbtList.Enabled = true;

            this.upnBotones.Update();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string problema = this.ddlProblema.SelectedItem.Value;
            string devolucion = this.chkDevolucion.Checked ? "1" : "0";
            string selectedValue = this.ddlMoneda.SelectedValue;
            string area_queja = this.ddlAreaQueja.SelectedItem.Value;
            string text1 = this.txtCosto.Text;
            string text2 = this.txtObservaciones.Text;
            string merma = this.chkMerma.Checked ? "1" : "0";

            string textDev = (rbtList.SelectedValue == "DEV") ? "1" : "0";
            string textMer = (rbtList.SelectedValue == "MER") ? "1" : "0";
            string textBon = (rbtList.SelectedValue == "BON") ? "1" : "0";

            devolucion = textDev;
            merma = textMer;

            if (this.con.update_queja_exp(this.txtFolio.Text, devolucion, selectedValue, problema, text1, area_queja, text2, merma, textBon) == "1")
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

        protected void btnModRechazado_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = this.Session["cliente"];
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("rechazadas.aspx");
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 10, 10, 10, 10);
            //iTextSharp.text.pdf.PdfWriter write = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream("c:\\Reportes\\" + txtFolio.Text + ".pdf", FileMode.Create));
            //iTextSharp.text.pdf.PdfWriter write = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath(".") + "\\rep\\" + txtFolio.Text + ".pdf", FileMode.Create));
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

            cb.SetTextMatrix(25, 640);
            cb.ShowText("Tipo de Rechazo: ");

            cb.SetTextMatrix(25, 620);
            cb.ShowText("Moneda: ");

            cb.SetTextMatrix(150, 620);
            cb.ShowText("Problema: ");

            cb.SetTextMatrix(25, 600);
            cb.ShowText("Pedido: ");

            cb.SetTextMatrix(250, 600);
            cb.ShowText("Costo: ");

            cb.SetTextMatrix(25, 580);
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

            cb.SetTextMatrix(125, 640);
            if (rbtList.SelectedValue == "DEV")
                cb.ShowText("DEVOLUCION");
            else if (rbtList.SelectedValue == "MER")
                cb.ShowText("MERMA");
            else if (rbtList.SelectedValue == "BON")
                cb.ShowText("BONIFICACION");
            else
                cb.ShowText("NO APLICA");

            cb.SetTextMatrix(80, 620);
            cb.ShowText(this.ddlMoneda.SelectedItem.Text);

            cb.SetTextMatrix(210, 620);
            cb.ShowText(this.ddlProblema.SelectedItem.Text);

            cb.SetTextMatrix(75, 600);
            cb.ShowText(this.txtPedido.Text);

            cb.SetTextMatrix(295, 600);
            cb.ShowText("$" + Convert.ToDecimal(this.txtCosto.Text).ToString("###,###,##0.00"));

            string cadena = txtObservaciones.Text.ToUpper();//108 txtObservaciones.Text.Replace("\n", " ");
            string cad1 = "";
            string cad2 = "";
            string cad3 = "";
            string cad4 = "";

            if (cadena.Length <= 60)
            {
                cad1 = cadena;
                cb.SetTextMatrix(120, 580);
                cb.ShowText(cad1);
            }
            else if (cadena.Length > 60 && cadena.Length <= 120)
            {
                cad1 = cadena.Substring(0, 50);
                cb.SetTextMatrix(120, 580);
                cb.ShowText(cad1);
                cad2 = cadena.Substring(50, (cadena.Length - cad1.Length));
                cb.SetTextMatrix(120, 560);
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
                cb.SetTextMatrix(120, 580);
                cb.ShowText(cad1);
                cb.SetTextMatrix(120, 560);
                cb.ShowText(cad2);
                if (cad3 != "")
                {
                    cb.SetTextMatrix(120, 540);
                    cb.ShowText(cad3);
                }

            }
            else if (cadena.Length > 180)
            {
                cad1 = cadena.Substring(0, 60);
                cad2 = cadena.Substring(60, 60);
                cad3 = cadena.Substring(120, 60);
                cad4 = cadena.Substring(180, cadena.Length - 180);
                cb.SetTextMatrix(120, 580);
                cb.ShowText(cad1.TrimStart());
                cb.SetTextMatrix(120, 560);
                cb.ShowText(cad2.TrimStart());
                cb.SetTextMatrix(120, 540);
                cb.ShowText(cad3.TrimStart());
                cb.SetTextMatrix(120, 520);
                cb.ShowText(cad4.TrimStart());
            }

            int AxisX = 25;
            int AxisY = 500;

            //detalle de productos
            DataTable dtDetQ = (DataTable)Session["dtDetQ"];
            DataView dv = dtDetQ.DefaultView;
            dv.RowFilter = "qud_cantrecha <> '0'";
            DataTable dtDetQ2 = dv.ToTable();
            DataTable dtDetQ3 = new DataTable();
            dtDetQ3.Columns.Add("id_producto", typeof(string));
            dtDetQ3.Columns.Add("nom_producto", typeof(string));
            dtDetQ3.Columns.Add("qud_lote", typeof(string));
            dtDetQ3.Columns.Add("qud_cantreci", typeof(string));
            dtDetQ3.Columns.Add("qud_cantrecha", typeof(string));
            dtDetQ3.Columns.Add("qud_cjsprod", typeof(string));
            dtDetQ3.Columns.Add("qud_porcen", typeof(string));

            int p = 0;
            foreach (DataRow rw in dtDetQ2.Rows)
            {
                string id_prod = rw["id_producto"].ToString();
                string nom_pro = rw["nom_producto"].ToString();
                string qud_lot = rw["qud_lote"].ToString();

                string qud_reci = rw["qud_cantreci"].ToString();
                string qud_rech = rw["qud_cantrecha"].ToString();
                string qud_caja = rw["qud_cjsprod"].ToString();

                if (p == 0)
                {
                    dtDetQ3.Rows.Add(id_prod, nom_pro, qud_lot, qud_reci, qud_rech, qud_caja, "0");
                    p++;
                }
                else
                {
                    bool fnd = false;
                    foreach (DataRow rw2 in dtDetQ3.Select("id_producto = '" + id_prod + "' AND qud_lote = '" + qud_lot + "'"))
                    {
                        rw2["qud_cantreci"] = Convert.ToInt32(rw2["qud_cantreci"].ToString()) + Convert.ToInt32(qud_reci);
                        rw2["qud_cantrecha"] = Convert.ToInt32(rw2["qud_cantrecha"].ToString()) + Convert.ToInt32(qud_rech);
                        rw2["qud_cjsprod"] = Convert.ToInt32(rw2["qud_cjsprod"].ToString()) + Convert.ToInt32(qud_caja);
                        fnd = true;
                    }
                    if (fnd == false)
                    {
                        dtDetQ3.Rows.Add(id_prod, nom_pro, qud_lot, qud_reci, qud_rech, qud_caja, "0");
                    }
                }

            }



            string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"));
            List<ListItem> listItemList = new List<ListItem>();
            foreach (string path in files)
            {
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".jpg"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".jpeg"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".png"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));
            }
            bool tiene_fotos = false;
            if (listItemList.Count > 0)
                tiene_fotos = true;

            //table.WriteSelectedRows(0, -1, 25, 300, cb);

            iTextSharp.text.pdf.PdfPTable table2 = crear_tabla();
            //table.TotalWidth = 560f
            table2.SetWidths(new float[] { 390f, 40f, 25f, 28f, 25f, 35f });
            cb.SetFontAndSize(bfTimes2, 10);
            iTextSharp.text.pdf.PdfPCell cell;
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(bfTimes2, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            int y = 0;
            foreach (DataRow rt in dtDetQ3.Rows)
            {
                if (tiene_fotos == true)
                {
                    if (y == 19)
                        break;
                }
                else
                {
                    if (y == 35)
                    {
                        break;
                    }
                }
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(rt["nom_producto"].ToString(), fuente));
                table2.AddCell(cell);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(rt["qud_lote"].ToString(), fuente));
                table2.AddCell(cell);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(rt["qud_cantreci"].ToString(), fuente));
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                table2.AddCell(cell);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(rt["qud_cantrecha"].ToString(), fuente));
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                table2.AddCell(cell);
                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(rt["qud_cjsprod"].ToString(), fuente));
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                table2.AddCell(cell);

                decimal rechazado = Convert.ToDecimal(rt["qud_cantrecha"].ToString());
                decimal producido = Convert.ToDecimal(rt["qud_cjsprod"].ToString());
                decimal porcentaj = Math.Round((Convert.ToDecimal(rt["qud_cantrecha"].ToString()) * 100) / Convert.ToDecimal(rt["qud_cjsprod"].ToString()), 1);

                cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(porcentaj + "%", fuente));
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                table2.AddCell(cell);
                //table2.AddCell();
                //table2.AddCell(rt["qud_lote"].ToString());
                //table2.AddCell(rt["qud_cantreci"].ToString());
                //table2.AddCell(rt["qud_cantrecha"].ToString());
                //table2.AddCell(rt["qud_cjsprod"].ToString());
                //table2.AddCell(rt["qud_porcen"].ToString());
                y++;
            }
            table2.WriteSelectedRows(0, -1, 25, 500, cb);

            if (tiene_fotos == true)
            {
                int Axis = 25;
                int l = 1;
                foreach (ListItem listItem in listItemList)
                {
                    if (l == 1)
                    {
                        string file_name = listItem.ToString();
                        iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
                        jpg_1.ScaleToFit(230f, 210f);
                        jpg_1.SetAbsolutePosition(Axis, 25);
                        doc.Add(jpg_1);
                        Axis = Axis + 190;
                    }
                    if (l == 2)
                    {
                        string file_name = listItem.ToString();
                        iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
                        jpg_2.ScaleToFit(230f, 210f);
                        jpg_2.SetAbsolutePosition(AxisX, 25);
                        doc.Add(jpg_2);
                        Axis = Axis + 190;
                    }
                    if (l == 3)
                    {
                        string file_name = listItem.ToString();
                        iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
                        jpg_3.ScaleToFit(230f, 210f);
                        jpg_3.SetAbsolutePosition(Axis, 25);
                        doc.Add(jpg_3);
                    }
                }
            }

            #region sin usar

            //int i = 0;
            //int j = 0;
            //foreach (DataRow rt in dtDetQ2.Rows)
            //{
            //    if (i == 8)
            //    {
            //        cb.EndText();
            //        doc.NewPage();

            //        //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\logo.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg.ScaleToFit(80f, 60f);
            //        jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //        //iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\Logo grupo U.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg2.ScaleToFit(80f, 60f);
            //        jpg2.SetAbsolutePosition(540, 720);
            //        jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //        doc.Add(jpg);
            //        doc.Add(jpg2);

            //        cb.BeginText();

            //        cb.SetFontAndSize(bfTimes, 30);
            //        cb.SetTextMatrix(230, 750);
            //        cb.ShowText("Queja " + txtFolio.Text);

            //        AxisX = 25;
            //        AxisY = 700;
            //    }

            //    if (j == 20)
            //    {
            //        cb.EndText();
            //        doc.NewPage();

            //        //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\logo.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg.ScaleToFit(80f, 60f);
            //        jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //        //iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\Logo grupo U.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg2.ScaleToFit(80f, 60f);
            //        jpg2.SetAbsolutePosition(540, 720);
            //        jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //        doc.Add(jpg);
            //        doc.Add(jpg2);

            //        cb.BeginText();

            //        cb.SetFontAndSize(bfTimes, 30);
            //        cb.SetTextMatrix(230, 750);
            //        cb.ShowText("Queja " + txtFolio.Text);

            //        AxisX = 25;
            //        AxisY = 700;
            //    }

            //    if (j == 32)
            //    {
            //        cb.EndText();
            //        doc.NewPage();

            //        //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\logo.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg.ScaleToFit(80f, 60f);
            //        jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //        //iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\Logo grupo U.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg2.ScaleToFit(80f, 60f);
            //        jpg2.SetAbsolutePosition(540, 720);
            //        jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //        doc.Add(jpg);
            //        doc.Add(jpg2);

            //        cb.BeginText();

            //        cb.SetFontAndSize(bfTimes, 30);
            //        cb.SetTextMatrix(230, 750);
            //        cb.ShowText("Queja " + txtFolio.Text);

            //        AxisX = 25;
            //        AxisY = 700;
            //    }
            //    if (j == 44)
            //    {
            //        cb.EndText();
            //        doc.NewPage();

            //        //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\logo.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg.ScaleToFit(80f, 60f);
            //        jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //        //iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\Logo grupo U.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg2.ScaleToFit(80f, 60f);
            //        jpg2.SetAbsolutePosition(540, 720);
            //        jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //        doc.Add(jpg);
            //        doc.Add(jpg2);

            //        cb.BeginText();

            //        cb.SetFontAndSize(bfTimes, 30);
            //        cb.SetTextMatrix(230, 750);
            //        cb.ShowText("Queja " + txtFolio.Text);

            //        AxisX = 25;
            //        AxisY = 700;
            //    }
            //    if (j == 56)
            //    {
            //        cb.EndText();
            //        doc.NewPage();

            //        //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\logo.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg.ScaleToFit(80f, 60f);
            //        jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //        //iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\imagenes\\Logo grupo U.png");//, iTextSharp.text.BaseColor.WHITE
            //        jpg2.ScaleToFit(80f, 60f);
            //        jpg2.SetAbsolutePosition(540, 720);
            //        jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //        doc.Add(jpg);
            //        doc.Add(jpg2);

            //        cb.BeginText();

            //        cb.SetFontAndSize(bfTimes, 30);
            //        cb.SetTextMatrix(230, 750);
            //        cb.ShowText("Queja " + txtFolio.Text);

            //        AxisX = 25;
            //        AxisY = 700;
            //    }

            //    cb.SetFontAndSize(bfTimes, 8);

            //    cb.SetTextMatrix(AxisX, AxisY);
            //    cb.ShowText("OP: ");
            //    cb.SetTextMatrix(AxisX + 50, AxisY);
            //    cb.ShowText("Nom: ");
            //    cb.SetTextMatrix(AxisX + 320, AxisY);
            //    cb.ShowText("Lote: ");
            //    cb.SetTextMatrix(AxisX + 375, AxisY);
            //    cb.ShowText("F Cad: ");

            //    cb.SetTextMatrix(AxisX, AxisY - 15);
            //    cb.ShowText("FOP: ");
            //    cb.SetTextMatrix(AxisX + 70, AxisY - 15);
            //    cb.ShowText("Tbl: ");
            //    cb.SetTextMatrix(AxisX + 210, AxisY - 15);
            //    cb.ShowText("Resp: ");
            //    cb.SetTextMatrix(AxisX + 375, AxisY - 15);
            //    cb.ShowText("Area: ");

            //    cb.SetTextMatrix(AxisX, AxisY - 30);
            //    cb.ShowText("Rec: ");
            //    cb.SetTextMatrix(AxisX + 50, AxisY - 30);
            //    cb.ShowText("Rech: ");
            //    cb.SetTextMatrix(AxisX + 100, AxisY - 30);
            //    cb.ShowText("Prod: ");
            //    cb.SetTextMatrix(AxisX + 150, AxisY - 30);
            //    cb.ShowText("Porc: ");

            //    string fecha_ordp = Convert.ToDateTime(con.fecha_orden_produccion(rt["qud_ordprod"].ToString(), rt["qud_ptcptp"].ToString())).ToString("dd/MM/yyyy");


            //    AxisX = AxisX + 15;
            //    cb.SetFontAndSize(bfTimes2, 8);
            //    cb.SetTextMatrix(AxisX, AxisY);
            //    cb.ShowText(rt["qud_ordprod"].ToString());//
            //    cb.SetTextMatrix(AxisX + 60, AxisY);
            //    cb.ShowText(rt["nom_producto"].ToString());
            //    cb.SetTextMatrix(AxisX + 325, AxisY);
            //    cb.ShowText(rt["qud_lote"].ToString());
            //    cb.SetTextMatrix(AxisX + 385, AxisY);
            //    cb.ShowText(rt["fecha_cad"].ToString());


            //    cb.SetTextMatrix(AxisX + 6, AxisY - 15);
            //    cb.ShowText(fecha_ordp);
            //    cb.SetTextMatrix(AxisX + 72, AxisY - 15);
            //    cb.ShowText(rt["tbl_nombre"].ToString());
            //    cb.SetTextMatrix(AxisX + 220, AxisY - 15);
            //    cb.ShowText(rt["qud_responsable"].ToString());
            //    cb.SetTextMatrix(AxisX + 390, AxisY - 15);
            //    cb.ShowText(rt["qud_area"].ToString());


            //    cb.SetTextMatrix(AxisX + 5, AxisY - 30);
            //    cb.ShowText(rt["qud_cantreci"].ToString());
            //    cb.SetTextMatrix(AxisX + 60, AxisY - 30);
            //    cb.ShowText(rt["qud_cantrecha"].ToString());
            //    cb.SetTextMatrix(AxisX + 110, AxisY - 30);
            //    cb.ShowText(rt["qud_cjsprod"].ToString());
            //    cb.SetTextMatrix(AxisX + 160, AxisY - 30);
            //    cb.ShowText(rt["qud_porcen"].ToString() + "%");

            //    AxisY = AxisY - 60;
            //    AxisX = AxisX - 15;
            //    i++;
            //    j++;

            //    if (tiene_fotos == true)
            //    {
            //        if (i == dtDetQ2.Rows.Count && i <= 4)
            //        {

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 25);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 4 && i <= 8))
            //        {
            //            cb.EndText();
            //            doc.NewPage();

            //            jpg.ScaleToFit(80f, 60f);
            //            jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //            jpg2.ScaleToFit(80f, 60f);
            //            jpg2.SetAbsolutePosition(540, 720);
            //            jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //            doc.Add(jpg);
            //            doc.Add(jpg2);

            //            cb.BeginText();

            //            cb.SetFontAndSize(bfTimes, 30);
            //            cb.SetTextMatrix(230, 750);
            //            cb.ShowText("Queja " + txtFolio.Text);

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 500);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 8 && i <= 15))
            //        {
            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 50);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 50);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 50);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 15 && i <= 20))
            //        {
            //            cb.EndText();
            //            doc.NewPage();

            //            jpg.ScaleToFit(80f, 60f);
            //            jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //            jpg2.ScaleToFit(80f, 60f);
            //            jpg2.SetAbsolutePosition(540, 720);
            //            jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //            doc.Add(jpg);
            //            doc.Add(jpg2);

            //            cb.BeginText();

            //            cb.SetFontAndSize(bfTimes, 30);
            //            cb.SetTextMatrix(230, 750);
            //            cb.ShowText("Queja " + txtFolio.Text);

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 500);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 20 && i <= 28))
            //        {

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 25);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 28 && i <= 32))
            //        {
            //            cb.EndText();
            //            doc.NewPage();

            //            jpg.ScaleToFit(80f, 60f);
            //            jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //            jpg2.ScaleToFit(80f, 60f);
            //            jpg2.SetAbsolutePosition(540, 720);
            //            jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //            doc.Add(jpg);
            //            doc.Add(jpg2);

            //            cb.BeginText();

            //            cb.SetFontAndSize(bfTimes, 30);
            //            cb.SetTextMatrix(230, 750);
            //            cb.ShowText("Queja " + txtFolio.Text);

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 500);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 32 && i <= 40))
            //        {

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 25);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 40 && i <= 44))
            //        {
            //            cb.EndText();
            //            doc.NewPage();

            //            jpg.ScaleToFit(80f, 60f);
            //            jpg.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            //            jpg2.ScaleToFit(80f, 60f);
            //            jpg2.SetAbsolutePosition(540, 720);
            //            jpg2.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //            doc.Add(jpg);
            //            doc.Add(jpg2);

            //            cb.BeginText();

            //            cb.SetFontAndSize(bfTimes, 30);
            //            cb.SetTextMatrix(230, 750);
            //            cb.ShowText("Queja " + txtFolio.Text);

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 500);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 500);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //        else if (i == dtDetQ2.Rows.Count && (i > 44 && i <= 52))
            //        {

            //            int Axis = 25;
            //            int l = 1;
            //            foreach (ListItem listItem in listItemList)
            //            {
            //                if (l == 1)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_1 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_1.ScaleToFit(230f, 210f);
            //                    jpg_1.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_1);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 2)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_2 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_2.ScaleToFit(230f, 210f);
            //                    jpg_2.SetAbsolutePosition(AxisX, 25);
            //                    doc.Add(jpg_2);
            //                    Axis = Axis + 190;
            //                }
            //                if (l == 3)
            //                {
            //                    string file_name = listItem.ToString();
            //                    iTextSharp.text.Image jpg_3 = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + "\\fotos_ant\\" + file_name);//, iTextSharp.text.BaseColor.WHITE
            //                    jpg_3.ScaleToFit(230f, 210f);
            //                    jpg_3.SetAbsolutePosition(Axis, 25);
            //                    doc.Add(jpg_3);
            //                }
            //            }
            //        }
            //    }
            //}  
            #endregion

            cb.EndText();

            doc.Close();

            this.Response.Write("<script>window.open('rep/" + txtFolio.Text + ".pdf','_blank')</script>");
        }

        public iTextSharp.text.pdf.PdfPTable crear_tabla()
        {
            iTextSharp.text.pdf.BaseFont bfTimes3 = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, false);
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(bfTimes3, 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.pdf.PdfPTable tbl = new iTextSharp.text.pdf.PdfPTable(6);
            tbl.TotalWidth = 570;
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Detalle de productos", fuente));
            cell.Colspan = 6;
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tbl.AddCell(cell);

            //cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("OP", fuente));
            //tbl.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Descripción", fuente));
            tbl.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Lot", fuente));
            tbl.AddCell(cell);
            //cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("FCad", fuente));
            //tbl.AddCell(cell);
            //cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("FOP", fuente));
            //tbl.AddCell(cell);
            //cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Tbl", fuente));
            //tbl.AddCell(cell);
            //cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Resp", fuente));
            //tbl.AddCell(cell);
            //cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Area", fuente));
            //tbl.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Rec", fuente));
            tbl.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Rech", fuente));
            tbl.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Prod", fuente));
            tbl.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Porc", fuente));
            tbl.AddCell(cell);

            return tbl;
        }
    }
}