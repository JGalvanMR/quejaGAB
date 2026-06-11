using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class quejas_fumigacion : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtQuejas = new DataTable();
        private DataTable dtTransportistas = new DataTable();
        private DataTable dtTransportistas_1 = new DataTable();
        private DataTable dtProductos = new DataTable();
        private DataTable dtProductos_1 = new DataTable();
        private DataTable dtInsectos = new DataTable();
        private DataTable dtInsectos_1 = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblAdmin.Text = !(this.Session["admin"].ToString() == "X") && !(this.Session["admin"].ToString() == "ADMINISTRADOR") ? "" : "ADMINISTRADOR";
            if (this.Page.IsPostBack)
                return;
            this.dtQuejas.Columns.Add("que_folio", typeof(int));
            this.dtQuejas.Columns.Add("que_fecha", typeof(string));
            this.dtQuejas.Columns.Add("que_pedido", typeof(string));
            this.dtQuejas.Columns.Add("que_tipo", typeof(string));
            this.dtQuejas.Columns.Add("que_aduana", typeof(string));
            this.dtQuejas.Columns.Add("que_transporte", typeof(string));
            this.dtQuejas.Columns.Add("que_caja", typeof(string));
            this.dtQuejas.Columns.Add("que_hold", typeof(string));
            this.dtQuejas.Columns.Add("que_released", typeof(string));
            this.dtQuejas.Columns.Add("producto", typeof(string));
            this.dtQuejas.Columns.Add("insecto", typeof(string));
            this.dtQuejas.Columns.Add("costo", typeof(string));
            this.dtQuejas.Columns.Add("accion", typeof(string));
            DataView defaultView = this.con.quejas_fumigacion(this.lblClave.Text, this.lblAdmin.Text).DefaultView;
            defaultView.Sort = "que_folio DESC";
            DataTable table = defaultView.ToTable();
            this.gvwQuejas.DataSource = (object)table;
            this.gvwQuejas.DataBind();
            this.Session["Filtrado"] = (object)table;
            this.dtTransportistas.Columns.Add("que_transportista", typeof(string));
            this.dtTransportistas_1 = this.dtTransportistas;
            this.dtTransportistas_1 = table.DefaultView.ToTable(true, "que_transporte");
            this.dtTransportistas.Rows.Add((object)"TRANSPORTISTAS...");
            this.dtTransportistas.Rows.Add((object)"TODOS");
            foreach (DataRow dataRow in this.dtTransportistas_1.Select("que_transporte <> ''"))
                this.dtTransportistas.Rows.Add(dataRow.ItemArray);
            this.cmbTransportista.DataSource = (object)this.dtTransportistas;
            this.cmbTransportista.DataTextField = "que_transportista";
            this.cmbTransportista.DataValueField = "que_transportista";
            this.cmbTransportista.DataBind();
            this.dtProductos.Columns.Add("producto", typeof(string));
            this.dtProductos.Rows.Add((object)"PRODUCTOS...");
            this.dtProductos.Rows.Add((object)"TODOS");
            this.dtProductos_1 = this.con.quejas_fumigacion_cmbProducto(this.lblClave.Text, this.lblAdmin.Text);
            foreach (DataRow row in (InternalDataCollectionBase)this.dtProductos_1.Rows)
                this.dtProductos.Rows.Add(row["producto"]);
            this.cmbProducto.DataSource = (object)this.dtProductos;
            this.cmbProducto.DataTextField = "producto";
            this.cmbProducto.DataValueField = "producto";
            this.cmbProducto.DataBind();
            this.dtInsectos.Columns.Add("insecto", typeof(string));
            this.dtInsectos.Rows.Add((object)"INSECTOS...");
            this.dtInsectos.Rows.Add((object)"TODOS");
            this.dtInsectos_1 = this.con.quejas_fumigacion_cmbInsectos(this.lblClave.Text, this.lblAdmin.Text);
            foreach (DataRow row in (InternalDataCollectionBase)this.dtInsectos_1.Rows)
                this.dtInsectos.Rows.Add(row["insecto"]);
            this.cmbInsecto.DataSource = (object)this.dtInsectos;
            this.cmbInsecto.DataTextField = "insecto";
            this.cmbInsecto.DataValueField = "insecto";
            this.cmbInsecto.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Session["queja"] = (object)"";
            this.Session["tarea"] = (object)"";
            this.Response.Redirect("fumigaciones.aspx");
        }

        protected void gvwQuejas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "consulta"))
                return;
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Session["tarea"] = (object)"CONSULTA";
            this.Session["queja"] = (object)this.Server.HtmlDecode(this.gvwQuejas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
            this.Response.Redirect("fumigaciones.aspx");
        }

        protected void gvwQuejas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvwQuejas.PageIndex = e.NewPageIndex;
            this.dtQuejas.Columns.Add("que_folio", typeof(string));
            this.dtQuejas.Columns.Add("que_fecha", typeof(string));
            this.dtQuejas.Columns.Add("que_pedido", typeof(string));
            this.dtQuejas.Columns.Add("que_tipo", typeof(string));
            this.dtQuejas.Columns.Add("que_aduana", typeof(string));
            this.dtQuejas.Columns.Add("que_transporte", typeof(string));
            this.dtQuejas.Columns.Add("que_caja", typeof(string));
            this.dtQuejas.Columns.Add("que_hold", typeof(string));
            this.dtQuejas.Columns.Add("que_released", typeof(string));
            this.dtQuejas.Columns.Add("producto", typeof(string));
            this.dtQuejas.Columns.Add("insecto", typeof(string));
            DataView defaultView = this.con.quejas_fumigacion(this.lblClave.Text, this.lblAdmin.Text).DefaultView;
            defaultView.Sort = "que_folio DESC";
            this.gvwQuejas.DataSource = (object)defaultView.ToTable();
            this.gvwQuejas.DataBind();
        }

        protected void cmbTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbTransportista.SelectedValue.ToString() == "TODOS" || this.cmbTransportista.SelectedValue.ToString() == "TRANSPORTISTAS...")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable2;
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("que_transporte = '" + this.cmbTransportista.SelectedValue.ToString() + "'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.upnQuejas.Update();
            }
        }

        protected void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable2;
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("producto LIKE '%" + this.cmbProducto.SelectedItem.ToString() + "%'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.upnQuejas.Update();
            }
        }

        protected void cmbInsecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbInsecto.SelectedValue.ToString() == "TODOS" || this.cmbInsecto.SelectedValue.ToString() == "INSECTOS...")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable2;
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("insecto LIKE '%" + this.cmbInsecto.SelectedItem.ToString() + "%'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.upnQuejas.Update();
            }
        }

        protected void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbTipo.SelectedValue.ToString() == "TODOS" || this.cmbTipo.SelectedValue.ToString() == "")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable2;
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("que_tipo = '" + this.cmbTipo.SelectedValue.ToString() + "'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.upnQuejas.Update();
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            int num = !(this.cmbTipo.SelectedValue == "") && !(this.cmbTipo.SelectedValue == "TODOS") ? 1 : 0;
            if (num == 0)
            {
                num = !(this.cmbTransportista.SelectedValue == "TRANSPORTISTAS...") && !(this.cmbTransportista.SelectedValue == "TODOS") ? 2 : 0;
                if (num == 0)
                {
                    num = !(this.cmbProducto.SelectedValue == "PRODUCTOS...") && !(this.cmbProducto.SelectedValue == "TODOS") ? 3 : 0;
                    if (num == 0)
                        num = !(this.cmbInsecto.SelectedValue == "INSECTOS...") && !(this.cmbInsecto.SelectedValue == "TODOS") ? 4 : 0;
                }
            }
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("QUEJA", typeof(int));
            dataTable1.Columns.Add("FECHA", typeof(string));
            dataTable1.Columns.Add("DIA", typeof(string));
            dataTable1.Columns.Add("SEMANA", typeof(string));
            dataTable1.Columns.Add("FACTURA", typeof(string));
            dataTable1.Columns.Add("MATERIA PRIMA", typeof(string));
            dataTable1.Columns.Add("PRODUCTO", typeof(string));
            dataTable1.Columns.Add("ADUANA", typeof(string));
            dataTable1.Columns.Add("TRANSPORTISTA", typeof(string));
            dataTable1.Columns.Add("CAJA", typeof(string));
            dataTable1.Columns.Add("INSECTO", typeof(string));
            dataTable1.Columns.Add("FDA/USDA HOLD", typeof(string));
            dataTable1.Columns.Add("FDA/USDA RELEASED", typeof(string));
            dataTable1.Columns.Add("RANCHO", typeof(string));
            dataTable1.Columns.Add("TABLA", typeof(string));
            dataTable1.Columns.Add("TIPO", typeof(string));
            dataTable1.Columns.Add("COSTO", typeof(string));
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            str1 = this.cmbTipo.SelectedValue.ToString();
            str2 = this.cmbTransportista.SelectedValue.ToString();
            str3 = this.cmbProducto.SelectedValue.ToString();
            str4 = this.cmbInsecto.SelectedValue.ToString();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = (DataTable)this.Session["Filtrado"];
            DataTable dataTable4 = new DataTable();
            switch (num)
            {
                case 0:
                    dataTable4 = dataTable3.Copy();
                    break;
                case 1:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("que_tipo = '" + this.cmbTipo.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
                case 2:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("que_transporte = '" + this.cmbTransportista.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
                case 3:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("producto like '%" + this.cmbProducto.SelectedValue.ToString() + "%'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
                case 4:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("insecto like '%" + this.cmbInsecto.SelectedValue.ToString() + "%'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable4.Rows)
            {
                DataTable dataTable5 = this.con.quejas_fumigacion_excel(row["que_folio"].ToString());
                string str5 = dataTable5.Rows[0]["Fecha"].ToString();
                string str6 = dataTable5.Rows[0]["Dia"].ToString();
                string str7 = dataTable5.Rows[0]["Semana"].ToString();
                string str8 = dataTable5.Rows[0]["Factura"].ToString();
                string str9 = dataTable5.Rows[0]["Materia Prima"].ToString();
                string str10 = dataTable5.Rows[0]["Producto"].ToString();
                string str11 = dataTable5.Rows[0]["Aduana"].ToString();
                string str12 = dataTable5.Rows[0]["Transportista"].ToString();
                string str13 = dataTable5.Rows[0]["Caja"].ToString();
                string str14 = dataTable5.Rows[0]["Insecto"].ToString();
                string str15 = dataTable5.Rows[0]["FDA/USDA Hold"].ToString();
                string str16 = dataTable5.Rows[0]["FDA/USDA Released"].ToString();
                string str17 = dataTable5.Rows[0]["Rancho"].ToString();
                string str18 = dataTable5.Rows[0]["Tabla"].ToString();
                string str19 = dataTable5.Rows[0]["Tipo"].ToString();
                string str20 = row["que_folio"].ToString();
                string str21 = dataTable5.Rows[0]["Costo"].ToString();
                dataTable1.Rows.Add((object)Convert.ToInt32(str20), (object)str5, (object)str6, (object)str7, (object)str8, (object)str9, (object)str10, (object)str11, (object)str12, (object)str13, (object)str14, (object)str15, (object)str16, (object)str17, (object)str18, (object)str19, (object)str21);
            }
            DataView defaultView = dataTable1.DefaultView;
            defaultView.Sort = "Fecha DESC";
            DataTable table = defaultView.ToTable();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=FUMIGACIONES COMERCIALIZADORA GAB.xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            response.ContentEncoding = Encoding.Unicode;
            response.BinaryWrite(Encoding.Unicode.GetPreamble());
            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                {
                    DataGrid dataGrid = new DataGrid();
                    dataGrid.DataSource = (object)table;
                    dataGrid.DataBind();
                    dataGrid.RenderControl(writer);
                    response.Write(stringWriter.ToString());
                    dataGrid.Dispose();
                    response.End();
                }
            }
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("default.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Session["tarea"] = (object)"CONSULTA";
            this.Session["queja"] = (object)this.txtQueja.Text;
            this.Response.Redirect("fumigaciones.aspx");
        }

        protected void gvwQuejas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            if (e.Row.Cells[13].Text == "0")
            {
                e.Row.Cells[13].BackColor = Color.White;
                e.Row.Cells[13].ForeColor = Color.White;
            }
            if (e.Row.Cells[13].Text == "1")
            {
                e.Row.Cells[13].BackColor = Color.Green;
                e.Row.Cells[13].ForeColor = Color.Green;
            }
        }
    }
}