using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace queja
{
    public partial class reportes : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private SqlConnection conex;
        private SqlCommand comand;
        private SqlDataAdapter adapter;
        private SqlDataReader reader;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.conex = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
        }

        protected void btnCedis_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("clientes_sem.aspx");
        }

        protected void btnArea_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("areas_sem.aspx");
        }

        protected void btnMes_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("cant_quejas.aspx");
        }

        protected void btnSemana_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("cant_semana.aspx");
        }

        protected void btnGeneral_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("semana_queja", typeof(string));
            dataTable1.Columns.Add("folio", typeof(string));
            dataTable1.Columns.Add("fecha", typeof(string));
            dataTable1.Columns.Add("mes", typeof(string));
            dataTable1.Columns.Add("primario", typeof(string));
            dataTable1.Columns.Add("cliente", typeof(string));
            dataTable1.Columns.Add("sucursal", typeof(string));
            dataTable1.Columns.Add("reporto", typeof(string));
            dataTable1.Columns.Add("recibio", typeof(string));
            dataTable1.Columns.Add("producto", typeof(string));
            dataTable1.Columns.Add("variedad", typeof(string));
            dataTable1.Columns.Add("problema", typeof(string));
            dataTable1.Columns.Add("orden_produccion", typeof(string));
            dataTable1.Columns.Add("orden_produccion_fecha", typeof(string));
            dataTable1.Columns.Add("lote", typeof(string));
            dataTable1.Columns.Add("area", typeof(string));
            dataTable1.Columns.Add("responsable", typeof(string));
            dataTable1.Columns.Add("caducidad", typeof(string));
            dataTable1.Columns.Add("recibidas", typeof(string));
            dataTable1.Columns.Add("rechazadas", typeof(string));
            dataTable1.Columns.Add("producidas", typeof(string));
            dataTable1.Columns.Add("porcentaje", typeof(string));
            dataTable1.Columns.Add("unidad", typeof(string));
            dataTable1.Columns.Add("devolucion", typeof(string));
            dataTable1.Columns.Add("moneda", typeof(string));
            dataTable1.Columns.Add("costo", typeof(string));
            dataTable1.Columns.Add("proveedor", typeof(string));
            dataTable1.Columns.Add("rancho", typeof(string));
            dataTable1.Columns.Add("tabla", typeof(string));
            dataTable1.Columns.Add("tarima", typeof(string));
            dataTable1.Columns.Add("investigador", typeof(string));
            dataTable1.Columns.Add("causa", typeof(string));
            dataTable1.Columns.Add("fecha_entrega", typeof(string));
            dataTable1.Columns.Add("cumplimiento", typeof(string));
            dataTable1.Columns.Add("responsable_acciones", typeof(string));
            dataTable1.Columns.Add("acciones", typeof(string));
            dataTable1.Columns.Add("fecha_termino_acciones", typeof(string));
            dataTable1.Columns.Add("responsable_verificacion", typeof(string));
            dataTable1.Columns.Add("fecha_verificacion", typeof(string));
            dataTable1.Columns.Add("acciones_cumplidas", typeof(string));
            dataTable1.Columns.Add("comen_acciones_cumplidas", typeof(string));
            dataTable1.Columns.Add("acciones_efectivas", typeof(string));
            dataTable1.Columns.Add("comen_acciones_efectivas", typeof(string));
            dataTable1.Columns.Add("pedido", typeof(string));
            dataTable1.Columns.Add("pedido_fecha", typeof(string));
            dataTable1.Columns.Add("semana_pedido", typeof(string));
            dataTable1.Columns.Add("transporte", typeof(string));
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = this.con.general_mstr_quejas(this.txtFechaInicio.Text, this.txtFechaFin.Text, this.lblClave.Text, this.lblCedis.Text, this.lblAdmin.Text);
            DataTable dataTable4 = new DataTable();
            DataTable dataTable5 = this.con.general_det_quejas(this.txtFechaInicio.Text, this.txtFechaFin.Text);
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable3.Rows)
            {
                Decimal num1 = new Decimal(0);
                foreach (DataRow dataRow in dataTable5.Select("que_folio = '" + row1["que_folio"].ToString() + "'"))
                    num1 += Convert.ToDecimal(dataRow["qud_cantrecha"].ToString());
                foreach (DataRow dataRow in dataTable5.Select("que_folio = '" + row1["que_folio"].ToString() + "'"))
                {
                    DataRow row2 = dataTable1.NewRow();
                    row2["semana_queja"] = (object)row1["que_semana"].ToString();
                    row2["folio"] = (object)row1["que_folio"].ToString();
                    row2["fecha"] = (object)Convert.ToDateTime(row1["que_fecha"].ToString()).ToString("dd/MM/yyyy");
                    row2["mes"] = (object)this.mes(row1["que_mes"].ToString());
                    row2["primario"] = (object)row1["cedis"].ToString();
                    row2["cliente"] = (object)row1["que_cliente"].ToString();
                    row2["sucursal"] = (object)row1["que_sucursal"].ToString();
                    row2["reporto"] = (object)row1["que_reporto"].ToString();
                    row2["recibio"] = (object)(row1["resp_nombre"].ToString() + " " + row1["resp_paterno"].ToString());
                    row2["area"] = (object)row1["area_nombre"].ToString();
                    row2["acciones_efectivas"] = (object)row1["que_cumpli_efe"].ToString();
                    row2["comen_acciones_efectivas"] = (object)row1["que_comen_efe"].ToString();
                    row2["pedido"] = (object)row1["pedido"].ToString();
                    row2["pedido_fecha"] = (row1["que_fechaemb"].ToString() == "") ? "" : (object)Convert.ToDateTime(row1["que_fechaemb"].ToString()).ToString("dd/MM/yyyy");
                    row2["semana_pedido"] = (object)row1["que_sememb"].ToString();
                    row2["transporte"] = (object)row1["transporte"].ToString();
                    Decimal num2 = new Decimal(0);
                    Decimal num3 = new Decimal(0);
                    Decimal num4;
                    if (row1["costo"].ToString() != "")
                    {
                        Decimal num5 = Convert.ToDecimal(dataRow["qud_cantrecha"].ToString());
                        num4 = !(num5 == new Decimal(0)) || !(num1 == new Decimal(0)) ? (!(num5 > new Decimal(0)) || !(num1 == new Decimal(0)) ? num5 * Convert.ToDecimal(100) / num1 / Convert.ToDecimal(100) * Convert.ToDecimal(row1["costo"].ToString()) : new Decimal(0)) : new Decimal(0);
                    }
                    else
                        num4 = new Decimal(0);
                    row2["costo"] = (object)num4.ToString("###,###,##0.00");
                    row2["producto"] = (object)dataRow["nom_producto"].ToString();
                    row2["variedad"] = (object)dataRow["qud_variedad"].ToString();
                    row2["problema"] = (object)dataRow["pro_nombre"].ToString();
                    row2["orden_produccion"] = (object)dataRow["qud_ordprod"].ToString();
                    row2["orden_produccion_fecha"] = (object)Convert.ToDateTime(dataRow["ordp_fecha"].ToString()).ToString("dd/MM/yyyy");
                    row2["lote"] = (object)dataRow["qud_lote"].ToString();
                    row2["responsable"] = (object)dataRow["qud_responsable"].ToString();
                    row2["caducidad"] = (object)dataRow["fecha_cad"].ToString();
                    row2["recibidas"] = (object)dataRow["qud_cantreci"].ToString();
                    row2["rechazadas"] = (object)dataRow["qud_cantrecha"].ToString();
                    row2["producidas"] = (object)dataRow["producidas"].ToString();
                    row2["porcentaje"] = (object)dataRow["porcentaje"].ToString();
                    row2["unidad"] = (object)dataRow["qud_unidad"].ToString();
                    row2["devolucion"] = dataRow["qud_devolucion"].ToString() == "1" ? (object)"SI" : (object)"NO";
                    row2["moneda"] = (object)dataRow["qud_moneda"].ToString();
                    row2["proveedor"] = (object)dataRow["prov_nombre"].ToString();
                    row2["rancho"] = (object)dataRow["rch_nombre"].ToString();
                    row2["tabla"] = (object)dataRow["tbl_nombre"].ToString();
                    row2["tarima"] = (object)dataRow["qud_tarima"].ToString();
                    dataTable1.Rows.Add(row2);
                }
            }

            foreach (DataRow row in dataTable1.Select("variedad = ''"))
            {
                string a_1 = row["folio"].ToString();
                string b_1 = this.con.mp_variedades(a_1);
                row["variedad"] = b_1;
            }
            DataTable dataTable6 = new DataTable();
            DataTable dataTable7 = this.con.general_det_investigacion();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
            {
                string str1 = "";
                string str2 = "";
                string str3 = "";
                foreach (DataRow dataRow in dataTable7.Select("que_folio = '" + row["folio"].ToString() + "'"))
                {
                    str1 = dataRow["responsable"].ToString();
                    str3 = "(" + str1.TrimEnd() + ")" + dataRow["inv_comentario"].ToString() + ", ";
                }
                str2 = str1.TrimEnd(' ');
                string str4 = str3.TrimEnd(' ');
                row["investigador"] = (object)str4.TrimEnd(',');
            }
            DataTable dataTable8 = new DataTable();
            DataTable dataTable9 = this.con.general_mstr_acciones();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
            {
                string str1 = "";
                string str2 = "";
                foreach (DataRow dataRow in dataTable9.Select("que_folio = '" + row["folio"].ToString() + "'"))
                {
                    str1 = dataRow["resp_causa"].ToString();
                    str2 = dataRow["acc_fecha"].ToString();
                }
                string str3 = str1.TrimEnd(' ');
                row["causa"] = (object)str3.TrimEnd(',');
                string str4 = str2.TrimEnd(' ');
                row["fecha_entrega"] = (object)str4.TrimEnd(',');
            }
            DataTable dataTable10 = new DataTable();
            DataTable dataTable11 = this.con.general_det_acciones();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
            {
                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";
                string str9 = "";
                foreach (DataRow dataRow in dataTable11.Select("que_folio = '" + row["folio"].ToString() + "'"))
                {
                    str1 = dataRow["acc_cumpli_ver"].ToString();
                    str2 = dataRow["resp_nombre"].ToString();
                    str3 = dataRow["acc_accion"].ToString();
                    str4 = dataRow["acc_fechatermino"].ToString();
                    str6 = dataRow["acc_fecha_ver"].ToString();
                    str7 = dataRow["acc_cumpli_ver"].ToString();
                    str8 = dataRow["acc_comen_ver"].ToString();
                    str9 += dataRow["acciones"].ToString();
                }
                row["cumplimiento"] = (object)str1;
                row["responsable_acciones"] = (object)str2;
                row["acciones"] = (object)str9;
                row["fecha_termino_acciones"] = (object)str4;
                row["responsable_verificacion"] = (object)str5;
                row["fecha_verificacion"] = (object)str6;
                row["acciones_cumplidas"] = (object)str7;
                row["comen_acciones_cumplidas"] = (object)str8;
            }

            if (dataTable1.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No se encontraron datos para generar el reporte');", true);
            }
            else
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearHeaders();
                response.ClearContent();
                response.AddHeader("content-disposition", "attachment; filename=Reporte_Quejas.xls");
                response.AddHeader("Content-Type", "application/Excel");
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";//"application/vnd.xlsx";
                response.ContentEncoding = Encoding.Unicode;
                response.Buffer = true;
                response.BinaryWrite(Encoding.Unicode.GetPreamble());
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                    {
                        DataGrid dataGrid = new DataGrid();
                        dataGrid.DataSource = (object)dataTable1;
                        dataGrid.DataBind();
                        dataGrid.RenderControl(writer);
                        response.Write(stringWriter.ToString());
                        dataGrid.Dispose();
                        response.End();
                    }
                }

                //using (XLWorkbook wb = new XLWorkbook())
                //{
                //    wb.Worksheets.Add(dataTable1, "Reporte");
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.Charset = "";
                //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //    Response.AddHeader("content-disposition", "attachment;filename=Reporte_Quejas.xlsx");
                //    using (MemoryStream MyMemoryStream = new MemoryStream())
                //    {
                //        wb.SaveAs(MyMemoryStream);
                //        MyMemoryStream.WriteTo(Response.OutputStream);
                //        Response.Flush();
                //        Response.End();
                //    }
                //    //using (StringWriter stringWriter = new StringWriter())
                //    //{
                //    //    using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                //    //    {
                //    //        DataGrid dataGrid = new DataGrid();
                //    //        dataGrid.DataSource = (object)dataTable1;
                //    //        dataGrid.DataBind();
                //    //        dataGrid.RenderControl(writer);
                //    //        Response.Write(stringWriter.ToString());
                //    //        dataGrid.Dispose();
                //    //        Response.End();
                //    //    }
                //    //}

                //}
            }
            
        }

        public string mes(string m)
        {
            string str = "";
            switch (m)
            {
                case "01":
                    str = "ENERO";
                    break;
                case "02":
                    str = "FEBRERO";
                    break;
                case "03":
                    str = "MARZO";
                    break;
                case "04":
                    str = "ABRIL";
                    break;
                case "05":
                    str = "MAYO";
                    break;
                case "06":
                    str = "JUNIO";
                    break;
                case "07":
                    str = "JULIO";
                    break;
                case "08":
                    str = "AGOSTO";
                    break;
                case "09":
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

        protected void btnGraficas_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("grafica_rechazo.aspx");
        }

        protected void btnCajasProducidas_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("producidas.aspx");
        }


        protected void Button1_Click1(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("clave", typeof(string));
            //dt.Columns.Add("nombre", typeof(string));

            //dt.Rows.Add("1", "egy");
            //dt.Rows.Add("2", "kettő");
            //dt.Rows.Add("3", "három");
            //dt.Rows.Add("4", "négy");
            //dt.Rows.Add("5", "öt");


            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(dt, "számok");
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    Response.AddHeader("content-disposition", "attachment;filename=Reporte_Quejas.xlsx");
            //    using (MemoryStream MyMemoryStream = new MemoryStream())
            //    {
            //        wb.SaveAs(MyMemoryStream);
            //        MyMemoryStream.WriteTo(Response.OutputStream);
            //        Response.Flush();
            //        Response.End();
            //    }

            //}
        }

        protected void btnProducto_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("estadistica.aspx");
        }
    }
}