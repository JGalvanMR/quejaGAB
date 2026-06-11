using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class quejas_serv : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtQuejas = new DataTable();
        private DataTable dtTransportistas = new DataTable();
        private DataTable dtTransportistas_1 = new DataTable();
        private DataTable dtProblema = new DataTable();
        private DataTable dtProblema_1 = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblAdmin.Text = !(this.Session["admin"].ToString() == "X") && !(this.Session["admin"].ToString() == "ADMINISTRADOR") ? "" : "ADMINISTRADOR";
            if (this.Page.IsPostBack)
                return;
            this.dtQuejas.Columns.Add("que_folio", typeof(Int32));
            this.dtQuejas.Columns.Add("que_fecha", typeof(string));
            this.dtQuejas.Columns.Add("que_pedido", typeof(string));
            this.dtQuejas.Columns.Add("que_transportista", typeof(string));
            this.dtQuejas.Columns.Add("que_destino", typeof(string));
            this.dtQuejas.Columns.Add("que_caja", typeof(string));
            this.dtQuejas.Columns.Add("que_perdida_mn", typeof(string));
            this.dtQuejas.Columns.Add("que_perdida_usd", typeof(string));
            this.dtQuejas.Columns.Add("que_tipo", typeof(string));
            this.dtQuejas.Columns.Add("que_nombre", typeof(string));
            this.dtQuejas.Columns.Add("embtracli", typeof(string));
            DataTable dataTable1 = this.con.quejas_servicio(this.lblClave.Text, this.lblAdmin.Text);
            string str1 = "";
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable1.Rows)
            {
                string str2 = row1["que_folio"].ToString();
                if (str2 != str1)
                {
                    DataRow row2 = this.dtQuejas.NewRow();
                    row2["que_folio"] = (object)row1["que_folio"].ToString().Trim();
                    row2["que_fecha"] = row1["que_fecha"].ToString().Trim();
                    row2["que_pedido"] = (object)row1["que_pedido"].ToString().Trim();
                    row2["que_transportista"] = (object)row1["que_transportista"].ToString().Trim();
                    row2["que_destino"] = (object)row1["que_destino"].ToString().Trim();
                    row2["que_caja"] = (object)row1["que_caja"].ToString().Trim();
                    row2["que_perdida_mn"] = row1["que_perdida_mn"].ToString().Trim();
                    row2["que_perdida_usd"] = row1["que_perdida_usd"].ToString().Trim();
                    row2["que_tipo"] = (object)row1["que_tipo"].ToString().Trim();
                    row2["que_nombre"] = (object)row1["que_nombre"].ToString().Trim();
                    row2["embtracli"] = (row1["embtracli"].ToString().Trim() == "0") ? "SIN ESPECIFICAR" : (row1["embtracli"].ToString().Trim() == "1") ? "EMBARQUES" : (row1["embtracli"].ToString().Trim() == "2") ? "TRANSPORTISTA" : (row1["embtracli"].ToString().Trim() == "3") ? "CLIENTE" : "ADUANA";
                    this.dtQuejas.Rows.Add(row2);
                }
                else
                {
                    string str3 = row1["que_nombre"].ToString().Trim();
                    foreach (DataRow dataRow in this.dtQuejas.Select("que_folio = '" + str2 + "'"))
                        dataRow["que_nombre"] = (object)(dataRow["que_nombre"].ToString() + ", " + str3);
                }
                str1 = str2;
            }
            DataView defaultView = this.dtQuejas.DefaultView;
            defaultView.Sort = "que_folio DESC";
            this.dtQuejas = defaultView.ToTable();
            this.gvwQuejas.DataSource = (object)this.dtQuejas;
            this.gvwQuejas.DataBind();
            this.Session["Filtrado"] = (object)this.dtQuejas;
            this.dtTransportistas.Columns.Add("que_transportista", typeof(string));
            this.dtTransportistas_1 = this.dtTransportistas;
            this.dtTransportistas_1 = this.dtQuejas.DefaultView.ToTable(true, "que_transportista");
            this.dtTransportistas.Rows.Add((object)"TRANSPORTISTAS...");
            this.dtTransportistas.Rows.Add((object)"TODOS");
            foreach (DataRow dataRow in this.dtTransportistas_1.Select("que_transportista <> ''"))
                this.dtTransportistas.Rows.Add(dataRow.ItemArray);
            this.cmbTransportista.DataSource = (object)this.dtTransportistas;
            this.cmbTransportista.DataTextField = "que_transportista";
            this.cmbTransportista.DataValueField = "que_transportista";
            this.cmbTransportista.DataBind();
            DataTable dataTable2 = this.con.combo_problemas(this.dtQuejas);
            this.dtProblema.Columns.Add("que_nombre", typeof(string));
            this.dtProblema_1 = this.dtProblema;
            this.dtProblema_1 = dataTable2.DefaultView.ToTable(true, "que_nombre");
            this.dtProblema.Rows.Add((object)"QUEJA/PROBLEMA...");
            this.dtProblema.Rows.Add((object)"TODOS");
            foreach (DataRow dataRow in this.dtProblema_1.Select("que_nombre <> ''"))
                this.dtProblema.Rows.Add(dataRow.ItemArray);
            this.cmbQueja.DataSource = (object)this.dtProblema;
            this.cmbQueja.DataTextField = "que_nombre";
            this.cmbQueja.DataValueField = "que_nombre";
            this.cmbQueja.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Session["queja"] = (object)"";
            this.Session["tarea"] = (object)"";
            if (this.lblNombre.Text == "ANGEL ESCAMILLA")
                this.Response.Redirect("serviciosnew.aspx");
            else
                this.Response.Redirect("servicios.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
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
            this.Response.Redirect("servicios.aspx");
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            int num = !(this.cmbTipo.SelectedValue == "") && !(this.cmbTipo.SelectedValue == "TODOS") ? 1 : 0;
            if (num == 0)
            {
                num = !(this.cmbTransportista.SelectedValue == "TRANSPORTISTAS...") && !(this.cmbTransportista.SelectedValue == "TODOS") ? 2 : 0;
                if (num == 0)
                    num = !(this.cmbQueja.SelectedValue == "QUEJA/PROBLEMA...") && !(this.cmbQueja.SelectedValue == "TODOS") ? 3 : 0;
                    if(num == 0)
                        num = !(this.cmbQueja.SelectedValue == "") && !(this.cmbQueja.SelectedValue == "TODOS") ? 4 : 0;
            }
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("Queja", typeof(int));
            dataTable1.Columns.Add("Fecha", typeof(string));
            dataTable1.Columns.Add("Fecha_Embarque", typeof(string));
            dataTable1.Columns.Add("Mes", typeof(string));
            dataTable1.Columns.Add("Nacional/Exportacion", typeof(string));
            dataTable1.Columns.Add("Orden_Venta", typeof(string));
            dataTable1.Columns.Add("Transportista", typeof(string));
            dataTable1.Columns.Add("Destino", typeof(string));
            dataTable1.Columns.Add("Responsable", typeof(string));
            dataTable1.Columns.Add("Tipo_Problema", typeof(string));
            dataTable1.Columns.Add("Comentarios", typeof(string));
            dataTable1.Columns.Add("Perdida_MN", typeof(string));
            dataTable1.Columns.Add("Perdida_USD", typeof(string));
            dataTable1.Columns.Add("Area", typeof(string));
            string str1 = "";
            string str2 = "";
            string str3 = "";
            str1 = this.cmbTipo.SelectedValue.ToString();
            str2 = this.cmbTransportista.SelectedValue.ToString();
            str3 = this.cmbQueja.SelectedValue.ToString();
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
                    foreach (DataRow dataRow in dataTable3.Select("que_transportista = '" + this.cmbTransportista.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
                case 3:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("que_nombre like '%" + this.cmbQueja.SelectedValue.ToString() + "%'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
                case 4:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("embtracli like '%" + this.ddlArea.SelectedValue.ToString() + "%'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable4.Rows)
            {
                DataTable dataTable5 = this.con.quejas_serv_excel(row["que_folio"].ToString());
                string str4 = dataTable5.Rows[0]["Fecha_Embarque"].ToString();
                string str5 = dataTable5.Rows[0]["Mes"].ToString();
                string str6 = dataTable5.Rows[0]["Nacional/Exportacion"].ToString();
                string str7 = dataTable5.Rows[0]["Orden_Venta"].ToString();
                string str8 = dataTable5.Rows[0]["Transportista"].ToString();
                string str9 = dataTable5.Rows[0]["Destino"].ToString();
                string str10 = dataTable5.Rows[0]["Responsable"].ToString();
                string str11 = dataTable5.Rows[0]["Tipo_Problema"].ToString();
                string str12 = dataTable5.Rows[0]["Comentarios"].ToString();
                string str13 = dataTable5.Rows[0]["Perdida_MN"].ToString();
                string str14 = dataTable5.Rows[0]["Perdida_USD"].ToString();
                string str15 = dataTable5.Rows[0]["Queja"].ToString();
                string shortDateString = dataTable5.Rows[0]["Fecha"].ToString();
                string str16 = dataTable5.Rows[0]["Area"].ToString();
                dataTable1.Rows.Add((object)Convert.ToInt32(str15), shortDateString, (object)str4, (object)str5, (object)str6, (object)str7, (object)str8, (object)str9, (object)str10, (object)str11, (object)str12, (object)str13, (object)str14, (object)str16);
            }
            DataView defaultView = dataTable1.DefaultView;
            defaultView.Sort = "Queja DESC";
            DataTable table = defaultView.ToTable();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=Reporte_Servicios_Transportistas.xls");
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

        protected void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbTipo.SelectedValue.ToString() == "TODOS" || this.cmbTipo.SelectedValue.ToString() == "")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
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

        protected void cmbTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbTransportista.SelectedValue.ToString() == "TODOS" || this.cmbTransportista.SelectedValue.ToString() == "TRANSPORTISTAS...")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("que_transportista = '" + this.cmbTransportista.SelectedValue.ToString() + "'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.upnQuejas.Update();
            }
        }

        protected void cmbQueja_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbQueja.SelectedValue.ToString() == "TODOS" || this.cmbQueja.SelectedValue.ToString() == "QUEJA/PROBLEMA...")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("que_nombre like '%" + this.cmbQueja.SelectedValue.ToString() + "%'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.upnQuejas.Update();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Session["tarea"] = (object)"CONSULTA";
            this.Session["queja"] = (object)this.txtQueja.Text;
            this.Response.Redirect("servicios.aspx");
        }

        protected void gvwQuejas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvwQuejas.PageIndex = e.NewPageIndex;
            this.dtQuejas.Columns.Add("que_folio", typeof(string));
            this.dtQuejas.Columns.Add("que_fecha", typeof(string));
            this.dtQuejas.Columns.Add("que_pedido", typeof(string));
            this.dtQuejas.Columns.Add("que_transportista", typeof(string));
            this.dtQuejas.Columns.Add("que_destino", typeof(string));
            this.dtQuejas.Columns.Add("que_caja", typeof(string));
            this.dtQuejas.Columns.Add("que_perdida_mn", typeof(string));
            this.dtQuejas.Columns.Add("que_perdida_usd", typeof(string));
            this.dtQuejas.Columns.Add("que_tipo", typeof(string));
            this.dtQuejas.Columns.Add("que_nombre", typeof(string));
            this.dtQuejas.Columns.Add("embtracli", typeof(string));
            DataTable dataTable = this.con.quejas_servicio(this.lblClave.Text, this.lblAdmin.Text);
            string str1 = "";
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable.Rows)
            {
                string str2 = row1["que_folio"].ToString();
                if (str2 != str1)
                {
                    DataRow row2 = this.dtQuejas.NewRow();
                    row2["que_folio"] = (object)row1["que_folio"].ToString().Trim();
                    row2["que_fecha"] = (object)Convert.ToDateTime(row1["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row2["que_pedido"] = (object)row1["que_pedido"].ToString().Trim();
                    row2["que_transportista"] = (object)row1["que_transportista"].ToString().Trim();
                    row2["que_destino"] = (object)row1["que_destino"].ToString().Trim();
                    row2["que_caja"] = (object)row1["que_caja"].ToString().Trim();
                    row2["que_perdida_mn"] = row1["que_perdida_mn"].ToString().Trim();
                    row2["que_perdida_usd"] = row1["que_perdida_usd"].ToString().Trim();
                    row2["que_tipo"] = (object)row1["que_tipo"].ToString().Trim();
                    row2["que_nombre"] = (object)row1["que_nombre"].ToString().Trim();
                    row2["embtracli"] = (row1["embtracli"].ToString().Trim() == "0") ? "SIN ESPECIFICAR" : (row1["embtracli"].ToString().Trim() == "1") ? "EMBARQUES" : (row1["embtracli"].ToString().Trim() == "2") ? "TRANSPORTISTA" : (row1["embtracli"].ToString().Trim() == "3") ? "CLIENTE" : "ADUANA";
                    this.dtQuejas.Rows.Add(row2);
                }
                else
                {
                    string str3 = row1["que_nombre"].ToString().Trim();
                    foreach (DataRow dataRow in this.dtQuejas.Select("que_folio = '" + str2 + "'"))
                        dataRow["que_nombre"] = (object)(dataRow["que_nombre"].ToString() + ", " + str3);
                }
                str1 = str2;
            }
            DataView defaultView = this.dtQuejas.DefaultView;
            defaultView.Sort = "que_folio DESC";
            this.dtQuejas = defaultView.ToTable();
            this.gvwQuejas.DataSource = (object)this.dtQuejas;
            this.gvwQuejas.DataBind();
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("default.aspx");
        }

        protected void btnExcelFecha_Click(object sender, EventArgs e)
        {
            int num = !(this.cmbTipo.SelectedValue == "") && !(this.cmbTipo.SelectedValue == "TODOS") ? 1 : 0;
            if (num == 0)
            {
                num = !(this.cmbTransportista.SelectedValue == "TRANSPORTISTAS...") && !(this.cmbTransportista.SelectedValue == "TODOS") ? 2 : 0;
                if (num == 0)
                {
                    num = !(this.cmbQueja.SelectedValue == "QUEJA/PROBLEMA...") && !(this.cmbQueja.SelectedValue == "TODOS") ? 3 : 0;
                    if (num == 0)
                        num = !(this.cmbQueja.SelectedValue == "") && !(this.cmbQueja.SelectedValue == "TODOS") ? 4 : 0;
                }
            }
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("Queja", typeof(int));
            dataTable1.Columns.Add("Fecha", typeof(string));
            dataTable1.Columns.Add("Fecha_Embarque", typeof(string));
            dataTable1.Columns.Add("Mes", typeof(string));
            dataTable1.Columns.Add("Nacional/Exportacion", typeof(string));
            dataTable1.Columns.Add("Orden_Venta", typeof(string));
            dataTable1.Columns.Add("Transportista", typeof(string));
            dataTable1.Columns.Add("Caja", typeof(string));
            dataTable1.Columns.Add("Destino", typeof(string));
            dataTable1.Columns.Add("Responsable", typeof(string));
            dataTable1.Columns.Add("Tipo_Problema", typeof(string));
            dataTable1.Columns.Add("Comentarios", typeof(string));
            dataTable1.Columns.Add("Perdida_MN", typeof(string));
            dataTable1.Columns.Add("Perdida_USD", typeof(string));
            dataTable1.Columns.Add("Area", typeof(string));
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string strfi = txtFecha.Text;
            string strff = txtFechaFin.Text;
            str1 = this.cmbTipo.SelectedValue.ToString();
            str2 = this.cmbTransportista.SelectedValue.ToString();
            str3 = this.cmbQueja.SelectedValue.ToString();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = con.quejas_servicio_rep_excel_fechas(this.lblClave.Text, this.lblAdmin.Text, strfi, strff);//(DataTable)this.Session["Filtrado"];
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
                    foreach (DataRow dataRow in dataTable3.Select("que_transportista = '" + this.cmbTransportista.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
                case 3:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("que_nombre like '%" + this.cmbQueja.SelectedValue.ToString() + "%'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
                case 4:
                    dataTable4 = dataTable3.Copy();
                    dataTable4.Clear();
                    foreach (DataRow dataRow in dataTable3.Select("embtracli like '%" + this.ddlArea.SelectedValue.ToString() + "%'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    break;
            }

            string que_act = "";
            string que_ant = "";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable4.Rows)
            {
                que_act = row["que_folio"].ToString();
                if (que_act != que_ant)
                {
                    DataTable dataTable5 = this.con.quejas_serv_excel(row["que_folio"].ToString());
                    string str4 = dataTable5.Rows[0]["Fecha_Embarque"].ToString();
                    string str5 = dataTable5.Rows[0]["Mes"].ToString();
                    string str6 = dataTable5.Rows[0]["Nacional/Exportacion"].ToString();
                    string str7 = dataTable5.Rows[0]["Orden_Venta"].ToString();
                    string str8 = dataTable5.Rows[0]["Transportista"].ToString();
                    string strCaja = dataTable5.Rows[0]["Caja"].ToString();
                    string str9 = dataTable5.Rows[0]["Destino"].ToString();
                    string str10 = dataTable5.Rows[0]["Responsable"].ToString();
                    string str11 = dataTable5.Rows[0]["Tipo_Problema"].ToString();
                    string str12 = dataTable5.Rows[0]["Comentarios"].ToString();
                    string str13 = dataTable5.Rows[0]["Perdida_MN"].ToString();
                    string str14 = dataTable5.Rows[0]["Perdida_USD"].ToString();
                    string str15 = dataTable5.Rows[0]["Queja"].ToString();
                    string shortDateString = dataTable5.Rows[0]["Fecha"].ToString();
                    string str16 = dataTable5.Rows[0]["Area"].ToString();
                    dataTable1.Rows.Add((object)Convert.ToInt32(str15), shortDateString, (object)str4, (object)str5, (object)str6, (object)str7, (object)str8, (object)strCaja, (object)str9, (object)str10, (object)str11, (object)str12, (object)str13, (object)str14, (object)str16);
                }
                que_ant = que_act;
            }
            DataView defaultView = dataTable1.DefaultView;
            defaultView.Sort = "Queja DESC";
            DataTable table = defaultView.ToTable();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=Reporte_Servicios_Transportistas_" + DateTime.Now.ToString("HHmmss") + ".xls");
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

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.ddlArea.SelectedValue.ToString() == "" || this.ddlArea.SelectedValue.ToString() == "TODOS")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("embtracli = '" + this.ddlArea.SelectedValue.ToString() + "'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.upnQuejas.Update();
            }
        }

        protected void btnRepServicios_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            Response.Redirect("scorecard.aspx");
        }
    }
}