using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace queja
{
    public partial class report : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtMstrQueja = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        private DataTable dtInvestigador = new DataTable();
        private DataTable dtAcciones = new DataTable();
        private DataTable dtVerificacion = new DataTable();
        private DataTable dtCausas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string carpeta = "~/fotos_des/";
            carpeta = Server.MapPath(carpeta);
            var existe = Directory.Exists(carpeta);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            string carpeta2 = "~/fotos_ant/";
            carpeta2 = Server.MapPath(carpeta2);
            existe = Directory.Exists(carpeta2);
            if (!Directory.Exists(carpeta2))
            {
                Directory.CreateDirectory(carpeta2);
            }

            
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            string str1 = "";
            string str2 = "";
            this.dtMstrQueja = this.con.consultaqueja(this.Session["queja"].ToString());
            this.Session["dtMstrQueja"] = this.dtMstrQueja;
            string str3 = this.dtMstrQueja.Rows[0]["que_folio"].ToString();
            string shortDateString = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
            string str4 = this.dtMstrQueja.Rows[0]["que_cliente"].ToString();
            string str5 = this.dtMstrQueja.Rows[0]["recibio"].ToString();
            string str6 = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
            string str7 = this.dtMstrQueja.Rows[0]["que_sucursal"].ToString();
            string str8 = this.dtMstrQueja.Rows[0]["que_fecha_efe"].ToString();
            string str9 = this.dtMstrQueja.Rows[0]["que_cumpli_efe"].ToString();
            string str10 = this.dtMstrQueja.Rows[0]["que_comen_efe"].ToString();
            this.dtDetQueja = this.con.consultadetqueja_pedido(this.Session["queja"].ToString());
            this.Session["dtDetQueja"] = this.dtDetQueja;
            foreach (DataRow row in (InternalDataCollectionBase)this.dtDetQueja.Rows)
                str1 = str1 + "<tr><td>" + row["nom_producto"].ToString() + "</td><td>" + row["problema"].ToString() + "</td><td>" + row["qud_ordprod"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["fecha_cad"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["qud_cantreci"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["qud_cantrecha"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["qud_unidad"].ToString() + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + (row["qud_devolucion"].ToString() == "1" ? "SI" : "NO") + "</td><td class='visible-lg visible-md visible-sm visible-xs'>" + row["qud_moneda"].ToString() + "</td></tr>";
            this.dtInvestigador = this.con.reporte_investigadores(this.Session["queja"].ToString());
            this.Session["dtInvestigador"] = this.dtInvestigador;
            foreach (DataRow row1 in (InternalDataCollectionBase)this.dtInvestigador.Rows)
            {
                this.dtCausas = this.con.reporte_causas(this.Session["queja"].ToString(), row1["inv_folio"].ToString());
                str2 = str2 + "<tr><td><table class='table table-bordered' width='100%'><tr><th>RESPONSABLE</th><th>COMENTARIO</th></tr><tr><td><h4>&#8226;" + row1["resp_nombre"].ToString() + "</h4></td><td><h4>" + row1["inv_comentario"].ToString() + "</h4></td></tr>";
                str2 += "<tr><td colspan='2' align='center'><table border='1' width='100%'>";
                foreach (DataRow row2 in (InternalDataCollectionBase)this.dtCausas.Rows)
                {
                    string acc_folio = row2["acc_folio"].ToString().Trim();
                    str2 += "<tr><td><table class='table table-bordered' width='100%'><tr><td colspan='2' align='center' class='color2'><b>RESPONSABLE ACCIONES CORRECTIVAS</b></td></tr><tr><td><b>NOMBRE</b></td><td><b>CAUSA</b></td></tr>";
                    str2 = str2 + "<tr><td><h5>" + row2["resp_nombre"].ToString() + "</h5></td><td><h5>" + row2["acc_causa"].ToString() + "</h5></td></tr><tr><td colspan='2' align='center'> <table class='table table-bordered' width='80%'><tr>";
                    this.dtAcciones = this.con.reporte_acciones(this.Session["queja"].ToString(), row2["acc_responsable"].ToString(), acc_folio);
                    foreach (DataRow row3 in (InternalDataCollectionBase)this.dtAcciones.Rows)
                    {
                        str2 += "<td colspan='5' align='center' class='color'><b>ACCIONES CORRECTIVAS</b></td>";
                        str2 += "<tr><td><b><u>ACCION</u></b></td><td><b><u>FECHA TERMINO</b></td><td><b><u>VERIFICACION</u></b></td><td><b><u>COMENTARIO</u></b></td><td><b><u>FECHA</u></b></td></tr>";
                        str2 = str2 + "<tr><td>" + row3["acc_accion"] + "</td><td>" + row3["acc_fechatermino"] + "</td><td>" + row3["acc_cumpli_ver"] + "</td><td>" + row3["acc_comen_ver"] + "</td><td>" + row3["acc_fecha_ver"] + "</td></tr><tr><td colspan='5' align='center'><div class='col-xs-12 col-sm-12 col-md-12 col-lg-12'><center><img src='fotos_des/1_" + this.Session["queja"].ToString() + "_" + row3["acc_clave"] + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'><img src='fotos_des/2_" + this.Session["queja"].ToString() + "_" + row3["acc_clave"] + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'><img src='fotos_des/3_" + this.Session["queja"].ToString() + "_" + row3["acc_clave"] + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></center></div></td></tr><tr><td colspan='5' align='center'>";
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
            string str11 = str2 + "</tr>";
            string str12 = "<table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td align='center' colspan='4' class='color2'><b>DESCRIPCIÓN DE LA QUEJA</b></td></tr><tr><td>Folio:</td><td>" + str3 + "</td><td class='visible-lg visible-md visible-sm visible-xs'>Fecha de la queja:</td><td class='visible-lg visible-md visible-sm visible-xs'>" + shortDateString + "</td></tr><tr><td>Cliente:</td><td>" + str4 + "</td></tr><tr><td>Queja recibida por:</td><td>" + str5 + "</td><td class='visible-lg visible-md visible-sm visible-xs'>Tipo de cliente:</td><td class='visible-lg visible-md visible-sm visible-xs'>" + str7 + "</td></tr><tr><td>Queja reportada por:</td><td>" + str6 + "</td></tr></table><br /><table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='9' align='center' class='color2'><b>DESCRIPCI&Oacute;N DEL PROBLEMA</b></td></tr><tr><th>PRODUCTO</th><th>PROBLEMA</th><th>ORDEN PROD</th><th class='visible-lg visible-md visible-sm visible-xs'>FECHA CAD</th><th class='visible-lg visible-md visible-sm visible-xs'>RECIBIDAS</th><th class='visible-lg visible-md visible-sm visible-xs'>RECHAZADAS</th><th class='visible-lg visible-md visible-sm visible-xs'>UNIDAD</th><th class='visible-lg visible-md visible-sm visible-xs'>DEVOLUCI&Oacute;N</th><th class='visible-lg visible-md visible-sm visible-xs'>MONEDA</th></tr>" + str1 + "</table><br /> <table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='3' align='center' class='color2'><b>IMAGENES</b></td></tr><tr><td align='center'><img src='fotos_ant/1_" + this.Session["queja"].ToString() + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></td><td align='center'><img src='fotos_ant/2_" + this.Session["queja"].ToString() + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></td><td align='center'><img src='fotos_ant/3_" + this.Session["queja"].ToString() + ".jpg' class='img-thumbnail img-responsive' style='width: 250px; height: 150px;'></td></tr><tr><td colspan='3' align='center'>";
            string str13 = "";
            string[] files1 = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"));
            List<ListItem> listItemList1 = new List<ListItem>();
            foreach (string path in files1)
            {
                if (Path.GetFileName(path).Contains("_" + this.Session["queja"].ToString() + ".pdf"))
                    listItemList1.Add(new ListItem(Path.GetFileName(path), path));
            }
            foreach (ListItem listItem in listItemList1)
                str13 = str13 + "<a href='fotos_ant/" + listItem.ToString() + "' class='btn btn-primary' role='button' target='_blank'>" + listItem.ToString() + "</a>";
            this.datosqueja.InnerHtml = str12 + str13 + "</td></tr></table><br /><table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='2' align='center' class='color2'><b>INVESTIGACI&Oacute;N DE LAS CAUSAS</b></td></tr>" + str11 + "</table>" + "<br/><table border='0' width='100%' class='table table-striped table-bordered table-hover table-responsive'><tr><td align='center' colspan='3' class='color2'><b>EFECTIVIDAD DE LAS ACCIONES CORRECTIVAS</b></td></tr><tr><th>FECHA</th><th>CUMPLIMIENTO</th><th class='visible-lg visible-md visible-sm visible-xs'>COMENTARIO</th></tr><tr><td>" + str8 + "</td><td>" + str9 + "</td><td>" + str10 + "</td></tr></table>";
            
            
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

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            //
            //PdfDocument pdfDoc = new PdfDocument();
            //pdfDoc.Info.Title = "Comercializadora GAB";
            //PdfPage page = pdfDoc.AddPage();
            //page.Size = PdfSharp.PageSize.Letter;

            //XGraphics gfx = XGraphics.FromPdfPage(page);
            //double pageWidth = page.Width.Point;
            //double pageHeight = page.Height.Point;

            //XImage imgIzq = XImage.FromFile(Server.MapPath("~/imagenes/GrupoU.png"));
            //XImage imgDer = XImage.FromFile(Server.MapPath("~/imagenes/logo.png"));

            //// Definir tamaño de las imágenes (50x50 pts)
            //double imgWidth = 50;
            //double imgHeight = 50;

            //// Posiciones
            //double margin = 40;
            //double yPosition = margin;

            //// Dibujar imagen izquierda
            //gfx.DrawImage(imgIzq, margin, yPosition, imgWidth, imgHeight);

            //// Dibujar imagen derecha
            //gfx.DrawImage(imgDer, pageWidth - margin - imgWidth, yPosition, imgWidth, imgHeight);

            //// Dibujar título centrado
            //XFont fontTitulo = new XFont("Helvetica", 24, XFontStyle.Bold);
            //XFont fontEncabezadoTabla = new XFont("Helvetica", 12, XFontStyle.Bold);
            //XFont fontContenido = new XFont("Helvetica", 10, XFontStyle.Regular);
            //XFont fontContenidoB = new XFont("Helvetica", 10, XFontStyle.Bold);
            //string titulo = "Reporte de Quejas No: " + this.lblQueja.Text;

            

            //// Medir ancho del texto para centrarlo
            //XSize sizeTitulo = gfx.MeasureString(titulo, fontTitulo);
            //double xTitulo = (pageWidth - sizeTitulo.Width) / 2;
            //double yTitulo = yPosition + (imgHeight - sizeTitulo.Height) / 2;

            //gfx.DrawString(titulo, fontTitulo, XBrushes.DarkBlue, new XPoint(xTitulo, yTitulo));

            //// Definir posición inicial de la tabla
            //double startX = 50;
            //double startY = 100;
            //double rowHeight = 20;

            //DataTable dtMstrQ = (DataTable)this.Session["tbMstrQueja"];
            //string shortDateString = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
            //string str4 = this.dtMstrQueja.Rows[0]["que_cliente"].ToString();
            //string str5 = this.dtMstrQueja.Rows[0]["recibio"].ToString();
            //string str6 = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
            //string str7 = this.dtMstrQueja.Rows[0]["que_sucursal"].ToString();

            //gfx.DrawRectangle(XPens.Black, XBrushes.Blue, startX, startY, 500, rowHeight);
            //gfx.DrawString("DESCRIPCION DE LA QUEJA", fontEncabezadoTabla, XBrushes.White, new XRect(startX, startY, 500, rowHeight), XStringFormats.Center);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX, startY + 20, 125, 15);
            //gfx.DrawString("Fecha:", fontContenidoB, XBrushes.Black, new XRect(startX + 2, startY + 22, 125, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 125, startY + 20, 125, 15);
            //gfx.DrawString(shortDateString, fontContenido, XBrushes.Black, new XRect(startX + 127, startY + 22, 125, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 250, startY + 20, 125, 15);
            //gfx.DrawString("Recibio:", fontContenidoB, XBrushes.Black, new XRect(startX + 252, startY + 22, 125, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 375, startY + 20, 125, 15);
            //gfx.DrawString(str5, fontContenido, XBrushes.Black, new XRect(startX + 377, startY + 22, 125, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX, startY + 35, 125, 15);
            //gfx.DrawString("Cliente:", fontContenidoB, XBrushes.Black, new XRect(startX + 2, startY + 37, 125, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 125, startY + 35, 375, 15);
            //gfx.DrawString(str4, fontContenido, XBrushes.Black, new XRect(startX + 127, startY + 37, 375, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX, startY + 50, 125, 15);
            //gfx.DrawString("Subcliente:", fontContenidoB, XBrushes.Black, new XRect(startX + 2, startY + 52, 125, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 125, startY + 50, 375, 15);
            //gfx.DrawString(str7, fontContenido, XBrushes.Black, new XRect(startX + 127, startY + 52, 375, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX, startY + 65, 125, 15);
            //gfx.DrawString("Reportó:", fontContenidoB, XBrushes.Black, new XRect(startX + 2, startY + 67, 125, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 125, startY + 65, 375, 15);
            //gfx.DrawString(str6, fontContenido, XBrushes.Black, new XRect(startX + 127, startY + 67, 375, 15), XStringFormats.TopLeft);

            //startX = 50;
            //startY = 190;

            //gfx.DrawRectangle(XPens.Black, XBrushes.Blue, startX, startY, 500, rowHeight);
            //gfx.DrawString("DESCRIPCION DEL PROBLEMA", fontEncabezadoTabla, XBrushes.White, new XRect(startX, startY, 500, rowHeight), XStringFormats.Center);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX, startY + 20, 55.55, 15);
            //gfx.DrawString("Producto", fontContenidoB, XBrushes.Black, new XRect(startX + 2, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 55.55, startY + 20, 55.55, 15);
            //gfx.DrawString("Problema", fontContenidoB, XBrushes.Black, new XRect(startX + 57.55, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 111.1, startY + 20, 55.55, 15);
            //gfx.DrawString("OrdenProd", fontContenidoB, XBrushes.Black, new XRect(startX + 113.1, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 166.65, startY + 20, 55.55, 15);
            //gfx.DrawString("FechaCad", fontContenidoB, XBrushes.Black, new XRect(startX + 168.65, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 222.22, startY + 20, 55.55, 15);
            //gfx.DrawString("Recibido", fontContenidoB, XBrushes.Black, new XRect(startX + 224.22, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 277.75, startY + 20, 55.55, 15);
            //gfx.DrawString("Rechazado", fontContenidoB, XBrushes.Black, new XRect(startX + 279.75, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 333.3, startY + 20, 55.55, 15);
            //gfx.DrawString("Cajas", fontContenidoB, XBrushes.Black, new XRect(startX + 335.3, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 388.85, startY + 20, 55.55, 15);
            //gfx.DrawString("Devoluc", fontContenidoB, XBrushes.Black, new XRect(startX + 390.85, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 444.4, startY + 20, 55.55, 15);
            //gfx.DrawString("Moneda", fontContenidoB, XBrushes.Black, new XRect(startX + 446.4, startY + 22, 55.55, 15), XStringFormats.TopLeft);

            //DataTable dtDetQ = (DataTable)this.Session["dtDetQueja"];
            //startX = 50;
            //startY = 205;
            //int RowIndex = 20;
            //XTextFormatter tf = new XTextFormatter(gfx);
            //foreach (DataRow row in dtDetQ.Rows) 
            //{
            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX, startY + RowIndex, 55.55, 70);
            //    //gfx.DrawString(row["nom_producto"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 2, startY + RowIndex + 2, 55.55, 50), XStringFormats.TopLeft);
            //    tf.DrawString(row["nom_producto"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 2, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 55.55, startY + RowIndex, 55.55, 70);
            //    //gfx.DrawString(row["problema"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 57.55, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);
            //    tf.DrawString(row["problema"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 57.55, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 111.1, startY + RowIndex, 55.55, 70);
            //    gfx.DrawString(row["qud_ordprod"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 113.1, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 166.65, startY + RowIndex, 55.55, 70);
            //    gfx.DrawString(row["fecha_cad"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 168.65, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 222.22, startY + RowIndex, 55.55, 70);
            //    gfx.DrawString(row["qud_cantreci"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 224.22, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 277.75, startY + RowIndex, 55.55, 70);
            //    gfx.DrawString(row["qud_cantrecha"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 279.75, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 333.3, startY + RowIndex, 55.55, 70);
            //    gfx.DrawString(row["qud_unidad"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 335.3, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 388.85, startY + RowIndex, 55.55, 70);
            //    gfx.DrawString((row["qud_devolucion"].ToString() == "1" ? "SI" : "NO"), fontContenido, XBrushes.Black, new XRect(startX + 390.85, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    gfx.DrawRectangle(XPens.Black, XBrushes.White, startX + 444.4, startY + RowIndex, 55.55, 70);
            //    gfx.DrawString(row["qud_moneda"].ToString(), fontContenido, XBrushes.Black, new XRect(startX + 446.4, startY + RowIndex + 2, 55.55, 70), XStringFormats.TopLeft);

            //    RowIndex = RowIndex + 70;
            //}


            
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    pdfDoc.Save(ms, false); // false para no cerrar el stream
            //    byte[] bytes = ms.ToArray();

            //    // Enviar PDF al cliente
            //    HttpContext.Current.Response.Clear();
            //    HttpContext.Current.Response.ContentType = "application/pdf";
            //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ReporteQueja2.pdf");
            //    HttpContext.Current.Response.Buffer = true;
            //    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    HttpContext.Current.Response.BinaryWrite(bytes);
            //    HttpContext.Current.Response.End();
            //}

            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 50f, 50f, 50f, 50f);

            string Helvetica = Server.MapPath("~/fuentes/Helvetica.ttf");
            string HelveticaBold = Server.MapPath("~/fuentes/Helvetica-Bold.ttf");
            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(Helvetica, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);
            iTextSharp.text.pdf.BaseFont bfBold = iTextSharp.text.pdf.BaseFont.CreateFont(HelveticaBold, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

            using (MemoryStream ms = new MemoryStream())
            {
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();

                // Fuente básica
                var fontTitle = iTextSharp.text.FontFactory.GetFont(bfBold.ToString(), 24, iTextSharp.text.BaseColor.BLACK);
                var fontSubTitle = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.BLACK);
                var fontSubTitleW = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.BaseColor.WHITE);
                var fontLabel = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 10);
                iTextSharp.text.Font fuenteContenido = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 10, iTextSharp.text.BaseColor.BLACK);
                iTextSharp.text.Font fuenteTitulos = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 10, iTextSharp.text.BaseColor.BLACK);
                iTextSharp.text.Font fuenteTitulosW = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 10, iTextSharp.text.BaseColor.WHITE);

                string rutaImagen1 = Server.MapPath("~/imagenes/GrupoU.png");
                string rutaImagen2 = Server.MapPath("~/imagenes/logo.png");
                //iTextSharp.text.Image imgIzq = iTextSharp.text.Image.GetInstance(rutaImagen1);
                //iTextSharp.text.Image imgDer = iTextSharp.text.Image.GetInstance(rutaImagen2);
                //imgIzq.ScaleAbsolute(60f, 60f);
                //imgDer.ScaleAbsolute(60f, 60f);
                //imgIzq.SetAbsolutePosition(40f, pdfDoc.PageSize.Height - 80f); // Izquierda arriba
                //imgDer.SetAbsolutePosition(pdfDoc.PageSize.Width - 100f, pdfDoc.PageSize.Height - 80f); // Derecha arriba
                //pdfDoc.Add(imgIzq);
                //pdfDoc.Add(imgDer);


                // Título
                //iTextSharp.text.Paragraph titulo = new iTextSharp.text.Paragraph("Reporte de Queja No: " + this.lblQueja.Text, fontTitle);
                //titulo.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                //pdfDoc.Add(titulo);
                iTextSharp.text.pdf.PdfPTable TablaTitle = new iTextSharp.text.pdf.PdfPTable(3);
                TablaTitle.WidthPercentage = 100;
                TablaTitle.SetWidths(new float[] { 1f, 3f, 1f });

                iTextSharp.text.Image imgLogo1 = iTextSharp.text.Image.GetInstance(rutaImagen1);
                imgLogo1.ScaleAbsolute(30f, 30f);
                iTextSharp.text.pdf.PdfPCell celdaImgTit = new iTextSharp.text.pdf.PdfPCell(imgLogo1, true);
                celdaImgTit.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celdaImgTit.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                celdaImgTit.Border = iTextSharp.text.Rectangle.NO_BORDER;
                celdaImgTit.BackgroundColor = new iTextSharp.text.BaseColor(211, 211, 211);
                celdaImgTit.FixedHeight = 50f;
                TablaTitle.AddCell(celdaImgTit);

                iTextSharp.text.pdf.PdfPCell titulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Reporte de Queja No: " + this.lblQueja.Text, fontTitle));
                titulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                titulo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                titulo.BackgroundColor = new iTextSharp.text.BaseColor(211, 211, 211);
                titulo.PaddingBottom = 10f;
                TablaTitle.AddCell(titulo);

                imgLogo1 = iTextSharp.text.Image.GetInstance(rutaImagen2);
                imgLogo1.ScaleAbsolute(30f, 30f);
                celdaImgTit = new iTextSharp.text.pdf.PdfPCell(imgLogo1, true);
                celdaImgTit.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celdaImgTit.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                celdaImgTit.Border = iTextSharp.text.Rectangle.NO_BORDER;
                celdaImgTit.BackgroundColor = new iTextSharp.text.BaseColor(211, 211, 211);
                celdaImgTit.FixedHeight = 50f;
                TablaTitle.AddCell(celdaImgTit);

                pdfDoc.Add(TablaTitle);

                pdfDoc.Add(new iTextSharp.text.Paragraph(" ")); // espacio

                iTextSharp.text.pdf.PdfPTable tabla = new iTextSharp.text.pdf.PdfPTable(4);
                tabla.WidthPercentage = 100;

                iTextSharp.text.pdf.PdfPCell celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("DESCRIPCION DE LA QUEJA", fuenteTitulosW));
                celdaTitulo.Colspan = 4;
                celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                celdaTitulo.PaddingBottom = 5f;
                tabla.AddCell(celdaTitulo);

                DataTable dtMstrQ = (DataTable)this.Session["tbMstrQueja"];
                DataTable dtDetQ = (DataTable)this.Session["dtDetQueja"];
                DataTable dtInves = (DataTable)this.Session["dtInvestigador"];

                string shortDateString = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
                string str4 = this.dtMstrQueja.Rows[0]["que_cliente"].ToString();
                string str5 = this.dtMstrQueja.Rows[0]["recibio"].ToString();
                string str6 = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
                string str7 = this.dtMstrQueja.Rows[0]["que_sucursal"].ToString();

                string str8 = this.dtMstrQueja.Rows[0]["que_fecha_efe"].ToString();
                string str9 = this.dtMstrQueja.Rows[0]["que_cumpli_efe"].ToString();
                string str10 = this.dtMstrQueja.Rows[0]["que_comen_efe"].ToString();

                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Fecha:", fuenteTitulos)));
                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(shortDateString, fuenteContenido)));
                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Recibio:", fuenteTitulos)));
                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(str5, fuenteContenido)));

                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Cliente:", fuenteTitulos)));
                iTextSharp.text.pdf.PdfPCell celdaCliente = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(str4, fontLabel));
                celdaCliente.Colspan = 3;
                celdaCliente.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celdaCliente.Border = iTextSharp.text.Rectangle.BOX;
                celdaCliente.PaddingBottom = 5f;
                tabla.AddCell(celdaCliente);

                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Sucursal:", fuenteTitulos)));
                celdaCliente = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(str7, fontLabel));
                celdaCliente.Colspan = 3;
                celdaCliente.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celdaCliente.Border = iTextSharp.text.Rectangle.BOX;
                celdaCliente.PaddingBottom = 5f;
                tabla.AddCell(celdaCliente);

                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Reporto:", fuenteTitulos)));
                tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(str6, fuenteContenido)));
                tabla.AddCell("");
                tabla.AddCell("");

                pdfDoc.Add(tabla);
                pdfDoc.Add(new iTextSharp.text.Paragraph(" ")); // espacio

                iTextSharp.text.pdf.PdfPTable tabla2 = new iTextSharp.text.pdf.PdfPTable(9);
                tabla2.WidthPercentage = 100;
                celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("DESCRIPCION DEL PROBLEMA", fuenteTitulosW));
                celdaTitulo.Colspan = 9;
                celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                celdaTitulo.PaddingBottom = 5f;
                tabla2.AddCell(celdaTitulo);

                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Producto", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Problema", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("OrdenProd", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("FechaCad", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Recibido", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Rechazado", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Cajas", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Devol", fuenteTitulos)));
                tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Moneda", fuenteTitulos)));


                foreach (DataRow row in dtDetQ.Rows)
                {
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["nom_producto"].ToString(), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["problema"].ToString(), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["qud_ordprod"].ToString(), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["fecha_cad"].ToString(), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["qud_cantreci"].ToString(), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["qud_cantrecha"].ToString(), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["qud_unidad"].ToString(), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase((row["qud_devolucion"].ToString() == "1" ? "SI" : "NO"), fuenteContenido)));
                    tabla2.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["qud_moneda"].ToString(), fuenteContenido)));
                }

                pdfDoc.Add(tabla2);
                pdfDoc.Add(new iTextSharp.text.Paragraph(" ")); // espacio

                if (dtDetQ.Rows.Count > 5)
                    pdfDoc.NewPage();

                //string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"), "*_" + this.lblQueja.Text + ".*");
                string[] extensionesPermitidas = { ".jpg", ".png" };
                string id = this.lblQueja.Text;
                string[] files = Directory
                    .GetFiles(this.Server.MapPath("~/fotos_ant/"), "*_" + id + ".*")
                    .Where(f => extensionesPermitidas.Contains(
                        Path.GetExtension(f).ToLower()
                    ))
                    .ToArray();

                iTextSharp.text.pdf.PdfPTable tabla3 = null;
                if (files.Length == 1)
                {
                    tabla3 = new iTextSharp.text.pdf.PdfPTable(1);
                    tabla3.WidthPercentage = 100;
                    celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("IMAGENES", fuenteTitulosW));
                    celdaTitulo.Colspan = 1;
                    celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                    celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                    celdaTitulo.PaddingBottom = 5f;
                    tabla3.AddCell(celdaTitulo);

                    string rutaArchivo1 = "";
                    foreach (string arc in files)
                    {
                        //string[] cad = arc.Split('\\');
                        string nombreArchivo = Path.GetFileName(arc);
                        rutaArchivo1 = this.Server.MapPath("fotos_ant/" + nombreArchivo);//rutaArchivo1 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                    }


                    iTextSharp.text.Image imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo1);
                    imgPersona.ScaleAbsolute(60f, 60f);
                    iTextSharp.text.pdf.PdfPCell celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                    celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    celdaImg.FixedHeight = 150f;
                    tabla3.AddCell(celdaImg);

                }
                if (files.Length == 2)
                {
                    tabla3 = new iTextSharp.text.pdf.PdfPTable(2);
                    tabla3.WidthPercentage = 100;
                    celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("IMAGENES", fuenteTitulosW));
                    celdaTitulo.Colspan = 2;
                    celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                    celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                    celdaTitulo.PaddingBottom = 10f;
                    tabla3.AddCell(celdaTitulo);

                    //string rutaArchivo1 = this.Server.MapPath("~/fotos_ant/1_11561.jpg");
                    //string rutaArchivo2 = this.Server.MapPath("~/fotos_ant/2_11561.jpg");

                    string rutaArchivo1 = "";
                    string rutaArchivo2 = "";
                    int index = 0;
                    foreach (string arc in files)
                    {
                        string nombreArchivo = Path.GetFileName(arc);
                        
                        if (index == 0)
                        {
                            rutaArchivo1 = this.Server.MapPath("fotos_ant/" + nombreArchivo);
                        }
                        else
                        {
                            rutaArchivo2 = this.Server.MapPath("fotos_ant/" + nombreArchivo);
                        }
                        index++;

                        //string[] cad = arc.Split('\\');
                        //if (index == 0)
                        //{
                        //    if (arc.Contains(".png"))
                        //        rutaArchivo1 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //    if (arc.Contains(".jpg"))
                        //        rutaArchivo1 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //}
                        //else
                        //{
                        //    if (arc.Contains(".png"))
                        //        rutaArchivo2 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //    if (arc.Contains(".jpg"))
                        //        rutaArchivo2 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //}
                        //index++;
                    }

                    iTextSharp.text.Image imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo1);
                    imgPersona.ScaleAbsolute(60f, 60f);
                    iTextSharp.text.pdf.PdfPCell celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                    celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    celdaImg.FixedHeight = 150f;
                    tabla3.AddCell(celdaImg);

                    imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo2);
                    imgPersona.ScaleAbsolute(60f, 60f);
                    celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                    celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    celdaImg.FixedHeight = 150f;
                    tabla3.AddCell(celdaImg);
                }
                if (files.Length == 3)
                {
                    tabla3 = new iTextSharp.text.pdf.PdfPTable(3);
                    tabla3.WidthPercentage = 100;
                    celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("IMAGENES", fuenteTitulosW));
                    celdaTitulo.Colspan = 3;
                    celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                    celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                    celdaTitulo.PaddingBottom = 5f;
                    tabla3.AddCell(celdaTitulo);

                    //string rutaArchivo1 = this.Server.MapPath("~/fotos_ant/1_11561.jpg");
                    //string rutaArchivo2 = this.Server.MapPath("~/fotos_ant/2_11561.jpg");
                    //string rutaArchivo3 = this.Server.MapPath("~/fotos_ant/3_11561.jpg");

                    string rutaArchivo1 = "";
                    string rutaArchivo2 = "";
                    string rutaArchivo3 = "";
                    int index = 0;
                    foreach (string arc in files)
                    {
                        string nombreArchivo = Path.GetFileName(arc);
                        if (index == 0)
                        {
                            rutaArchivo1 = this.Server.MapPath("fotos_ant/" + nombreArchivo);
                        }
                        else if (index == 1)
                        {
                            rutaArchivo2 = this.Server.MapPath("fotos_ant/" + nombreArchivo);
                        }
                        else if (index == 2)
                        {
                            rutaArchivo3 = this.Server.MapPath("fotos_ant/" + nombreArchivo);
                        }
                        index++;
                        //string[] cad = arc.Split('\\');
                        //if (index == 0)
                        //{
                        //    if (arc.Contains(".png"))
                        //        rutaArchivo1 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //    if (arc.Contains(".jpg"))
                        //        rutaArchivo1 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //}
                        //else if (index == 1)
                        //{
                        //    if (arc.Contains(".png"))
                        //        rutaArchivo2 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //    if (arc.Contains(".jpg"))
                        //        rutaArchivo2 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //}
                        //else if (index == 2)
                        //{
                        //    if (arc.Contains(".png"))
                        //        rutaArchivo3 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //    if (arc.Contains(".jpg"))
                        //        rutaArchivo3 = this.Server.MapPath("fotos_ant/" + cad[cad.Length - 1]);
                        //}
                        //index++;
                    }

                    iTextSharp.text.Image imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo1);
                    imgPersona.ScaleAbsolute(60f, 60f);
                    iTextSharp.text.pdf.PdfPCell celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                    celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    celdaImg.FixedHeight = 150f;
                    tabla3.AddCell(celdaImg);
                    imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo2);
                    imgPersona.ScaleAbsolute(60f, 60f);
                    celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                    celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    celdaImg.FixedHeight = 150f;
                    tabla3.AddCell(celdaImg);
                    imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo3);
                    imgPersona.ScaleAbsolute(60f, 60f);
                    celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                    celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    celdaImg.FixedHeight = 150f;
                    tabla3.AddCell(celdaImg);
                }

                if (tabla3 != null)
                    pdfDoc.Add(tabla3);

                pdfDoc.Add(new iTextSharp.text.Paragraph(" ")); // espacio

                iTextSharp.text.pdf.PdfPTable tabla4 = new iTextSharp.text.pdf.PdfPTable(2);
                tabla4.WidthPercentage = 100;
                celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("INVESTIGACION DE LAS CAUSAS", fuenteTitulosW));
                celdaTitulo.Colspan = 2;
                celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                celdaTitulo.PaddingBottom = 5f;
                tabla4.AddCell(celdaTitulo);
                pdfDoc.Add(tabla4);





                DataTable dtCausasRep = new DataTable();
                DataTable dtAccionRep = new DataTable();
                foreach (DataRow row in dtInves.Rows)
                {
                    pdfDoc.Add(new iTextSharp.text.Paragraph(" ")); // espacio
                    iTextSharp.text.pdf.PdfPTable tabla5 = new iTextSharp.text.pdf.PdfPTable(2);
                    tabla5.WidthPercentage = 100;
                    tabla5.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("RESPONSABLE", fuenteTitulos)));
                    tabla5.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("COMENTARIO", fuenteTitulos)));
                    pdfDoc.Add(tabla5);

                    dtCausasRep = this.con.reporte_causas(this.Session["queja"].ToString(), row["inv_folio"].ToString());

                    iTextSharp.text.pdf.PdfPTable tabla6 = new iTextSharp.text.pdf.PdfPTable(2);
                    tabla6.WidthPercentage = 100;
                    tabla6.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["resp_nombre"].ToString(), fuenteContenido)));
                    tabla6.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row["inv_comentario"].ToString(), fuenteContenido)));
                    pdfDoc.Add(tabla6);

                    iTextSharp.text.pdf.PdfPTable tabla7 = new iTextSharp.text.pdf.PdfPTable(1);
                    tabla7.WidthPercentage = 100;
                    celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("RESPONSABLE ACCIONES CORRECTIVAS", fuenteTitulosW));
                    //celdaTitulo.Colspan = 1;
                    celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                    celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                    celdaTitulo.PaddingBottom = 5f;
                    tabla7.AddCell(celdaTitulo);
                    //tabla7.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("RESPONSABLE ACCIONES CORRECTIVAS", fuenteTitulos)));
                    celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);

                    pdfDoc.Add(tabla7);

                    foreach (DataRow row2 in dtCausasRep.Rows)
                    {
                        string acc_folio = row2["acc_folio"].ToString().Trim();
                        iTextSharp.text.pdf.PdfPTable tabla8 = new iTextSharp.text.pdf.PdfPTable(2);
                        tabla8.WidthPercentage = 100;
                        tabla8.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("NOMBRE", fuenteTitulos)));
                        tabla8.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("CAUSA", fuenteTitulos)));
                        tabla8.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row2["resp_nombre"].ToString(), fuenteContenido)));
                        tabla8.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row2["acc_causa"].ToString(), fuenteContenido)));
                        pdfDoc.Add(tabla8);

                        dtAccionRep = this.con.reporte_acciones(this.Session["queja"].ToString(), row2["acc_responsable"].ToString(), acc_folio);
                        iTextSharp.text.pdf.PdfPTable tabla9 = new iTextSharp.text.pdf.PdfPTable(5);
                        tabla9.WidthPercentage = 100;

                        celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("ACCIONES CORRECTIVAS", fuenteTitulosW));
                        celdaTitulo.Colspan = 5;
                        celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                        celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(25, 135, 84);
                        celdaTitulo.PaddingBottom = 5f;
                        tabla9.AddCell(celdaTitulo);

                        tabla9.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("ACCION", fuenteTitulos)));
                        tabla9.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("FEC_TERMINO", fuenteTitulos)));
                        tabla9.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("VERIF", fuenteTitulos)));
                        tabla9.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("COMENTARIO", fuenteTitulos)));
                        tabla9.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("FECHA", fuenteTitulos)));
                        pdfDoc.Add(tabla9);
                        foreach (DataRow row3 in dtAccionRep.Rows)
                        {
                            iTextSharp.text.pdf.PdfPTable tabla9A = new iTextSharp.text.pdf.PdfPTable(5);
                            tabla9A.WidthPercentage = 100;
                            tabla9A.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row3["acc_accion"].ToString(), fuenteContenido)));
                            tabla9A.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row3["acc_fechatermino"].ToString(), fuenteContenido)));
                            tabla9A.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row3["acc_cumpli_ver"].ToString(), fuenteContenido)));
                            tabla9A.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row3["acc_comen_ver"].ToString(), fuenteContenido)));
                            tabla9A.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(row3["acc_fecha_ver"].ToString(), fuenteContenido)));
                            pdfDoc.Add(tabla9A);

                            string acc_clave = row3["acc_clave"].ToString().Trim();

                            string[] extensionesPermitidas2 = { ".jpg", ".png" };
                            string id2 = this.lblQueja.Text;
                            string[] files2 = Directory
                                .GetFiles(this.Server.MapPath("~/fotos_des/"), "*_" + id + "_" + acc_clave + ".*")
                                .Where(f => extensionesPermitidas2.Contains(
                                    Path.GetExtension(f).ToLower()
                                ))
                                .ToArray();

                            iTextSharp.text.pdf.PdfPTable tabla10 = null;
                            if (files2.Length == 1)
                            {
                                tabla10 = new iTextSharp.text.pdf.PdfPTable(1);
                                tabla10.WidthPercentage = 100;

                                string rutaArchivo1 = "";
                                foreach (string arc in files2)
                                {
                                    string nombreArchivo = Path.GetFileName(arc);
                                    rutaArchivo1 = this.Server.MapPath("fotos_des/" + nombreArchivo);
                                }
                                
                                //foreach (string arc in files2)
                                //{
                                //    string[] cad = arc.Split('\\');
                                //    if (arc.Contains(".png"))
                                //        rutaArchivo1 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //    if (arc.Contains(".jpg"))
                                //        rutaArchivo1 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //}


                                iTextSharp.text.Image imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo1);
                                imgPersona.ScaleAbsolute(60f, 60f);
                                iTextSharp.text.pdf.PdfPCell celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                                celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                celdaImg.FixedHeight = 150f;
                                tabla10.AddCell(celdaImg);

                            }
                            if (files2.Length == 2)
                            {
                                tabla10 = new iTextSharp.text.pdf.PdfPTable(2);
                                tabla10.WidthPercentage = 100;

                                //string rutaArchivo1 = this.Server.MapPath("~/fotos_ant/1_11561.jpg");
                                //string rutaArchivo2 = this.Server.MapPath("~/fotos_ant/2_11561.jpg");

                                string rutaArchivo1 = "";
                                string rutaArchivo2 = "";
                                int index = 0;
                                foreach (string arc in files2)
                                {
                                    string nombreArchivo = Path.GetFileName(arc);
                                    if (index == 0) 
                                    {
                                        rutaArchivo1 = this.Server.MapPath("fotos_des/" + nombreArchivo);
                                    }
                                    else
                                    {
                                        rutaArchivo2 = this.Server.MapPath("fotos_des/" + nombreArchivo);
                                    }
                                    index++;
                                }

                                //foreach (string arc in files2)
                                //{
                                //    string[] cad = arc.Split('\\');
                                //    if (index == 0)
                                //    {
                                //        if (arc.Contains(".png"))
                                //            rutaArchivo1 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //        if (arc.Contains(".jpg"))
                                //            rutaArchivo1 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //    }
                                //    else
                                //    {
                                //        if (arc.Contains(".png"))
                                //            rutaArchivo2 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //        if (arc.Contains(".jpg"))
                                //            rutaArchivo2 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //    }
                                //    index++;
                                //}

                                iTextSharp.text.Image imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo1);
                                imgPersona.ScaleAbsolute(60f, 60f);
                                iTextSharp.text.pdf.PdfPCell celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                                celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                celdaImg.FixedHeight = 150f;
                                tabla10.AddCell(celdaImg);

                                imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo2);
                                imgPersona.ScaleAbsolute(60f, 60f);
                                celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                                celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                celdaImg.FixedHeight = 150f;
                                tabla10.AddCell(celdaImg);
                            }
                            if (files2.Length == 3)
                            {
                                tabla10 = new iTextSharp.text.pdf.PdfPTable(3);
                                tabla10.WidthPercentage = 100;

                                //string rutaArchivo1 = this.Server.MapPath("~/fotos_ant/1_11561.jpg");
                                //string rutaArchivo2 = this.Server.MapPath("~/fotos_ant/2_11561.jpg");
                                //string rutaArchivo3 = this.Server.MapPath("~/fotos_ant/3_11561.jpg");

                                string rutaArchivo1 = "";
                                string rutaArchivo2 = "";
                                string rutaArchivo3 = "";
                                int index = 0;

                                foreach (string arc in files2)
                                {
                                    string nombreArchivo = Path.GetFileName(arc);
                                    if (index == 0)
                                    {
                                        rutaArchivo1 = this.Server.MapPath("fotos_des/" + nombreArchivo);
                                    }
                                    else if (index == 1)
                                    {
                                        rutaArchivo2 = this.Server.MapPath("fotos_des/" + nombreArchivo);
                                    }
                                    else
                                    {
                                        rutaArchivo3 = this.Server.MapPath("fotos_des/" + nombreArchivo);
                                    }
                                    index++;
                                }
                                //foreach (string arc in files2)
                                //{
                                //    string[] cad = arc.Split('\\');
                                //    if (index == 0)
                                //    {
                                //        if (arc.Contains(".png"))
                                //            rutaArchivo1 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //        if (arc.Contains(".jpg"))
                                //            rutaArchivo1 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //    }
                                //    else if (index == 1)
                                //    {
                                //        if (arc.Contains(".png"))
                                //            rutaArchivo2 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //        if (arc.Contains(".jpg"))
                                //            rutaArchivo2 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //    }
                                //    else if (index == 2)
                                //    {
                                //        if (arc.Contains(".png"))
                                //            rutaArchivo3 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //        if (arc.Contains(".jpg"))
                                //            rutaArchivo3 = this.Server.MapPath("fotos_des/" + cad[cad.Length - 1]);
                                //    }
                                //    index++;
                                //}

                                iTextSharp.text.Image imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo1);
                                imgPersona.ScaleAbsolute(60f, 60f);
                                iTextSharp.text.pdf.PdfPCell celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                                celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                celdaImg.FixedHeight = 150f;
                                tabla10.AddCell(celdaImg);
                                imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo2);
                                imgPersona.ScaleAbsolute(60f, 60f);
                                celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                                celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                celdaImg.FixedHeight = 150f;
                                tabla10.AddCell(celdaImg);
                                imgPersona = iTextSharp.text.Image.GetInstance(rutaArchivo3);
                                imgPersona.ScaleAbsolute(60f, 60f);
                                celdaImg = new iTextSharp.text.pdf.PdfPCell(imgPersona, true);
                                celdaImg.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                celdaImg.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                celdaImg.FixedHeight = 150f;

                                tabla10.AddCell(celdaImg);
                            }

                            if (tabla10 != null)
                                pdfDoc.Add(tabla10);
                        }



                    }



                }

                pdfDoc.Add(new iTextSharp.text.Paragraph(" ")); // espacio

                iTextSharp.text.pdf.PdfPTable tabla11 = new iTextSharp.text.pdf.PdfPTable(3);
                tabla11.WidthPercentage = 100;
                celdaTitulo = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("EFECTIVIDAD DE LAS ACCIONES CORRECTIVAS", fuenteTitulosW));
                celdaTitulo.Colspan = 3;
                celdaTitulo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celdaTitulo.BackgroundColor = new iTextSharp.text.BaseColor(13, 110, 253);
                celdaTitulo.Border = iTextSharp.text.Rectangle.BOX;
                celdaTitulo.PaddingBottom = 5f;
                tabla11.AddCell(celdaTitulo);

                tabla11.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("FECHA", fuenteTitulos)));
                tabla11.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("CUMPLIMIENTO", fuenteTitulos)));
                tabla11.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("COMENTARIO", fuenteTitulos)));

                tabla11.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(str8, fuenteContenido)));
                tabla11.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(str9, fuenteContenido)));
                tabla11.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(str10, fuenteContenido)));

                pdfDoc.Add(tabla11);



                pdfDoc.Close();

                // Descargar el PDF
                byte[] bytes = ms.ToArray();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=ReporteQueja.pdf");
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
            }


        }

    }
}