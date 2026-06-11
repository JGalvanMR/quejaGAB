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
    public partial class quejas : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtProductos = new DataTable();
        private DataTable dtProblemas = new DataTable();
        private DataTable dtAreas = new DataTable();
        private DataTable dtVariedades = new DataTable();
        private DataTable dtPedidos = new DataTable();
        private DataTable dtProductos_1 = new DataTable();
        private DataTable dtProblemas_1 = new DataTable();
        private DataTable dtAreas_1 = new DataTable();
        private DataTable dtVariedades_1 = new DataTable();
        private DataTable dtPedidos_1 = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["queja"] != null)
            {
                string str = this.Request.QueryString["queja"];
                DataTable dataTable = this.con.datosresponsable(this.Request.QueryString["resp"]);
                this.Session["clave"] = (object)this.Request.QueryString["resp"];
                this.Session["nombre"] = dataTable.Rows[0]["resp_nombre"];
                this.Session["cedis"] = dataTable.Rows[0]["resp_cedis"];
                this.Session["admin"] = dataTable.Rows[0]["resp_nivel_dos"];
                this.Session["nivel"] = dataTable.Rows[0]["resp_nivel_tres"];
                this.Session["queja"] = (object)this.Request.QueryString["queja"];
                this.Session["accion"] = (object)this.Request.QueryString["accion"];
                this.Session["correo"] = (object)"1";
            }
            if (this.Session["clave"] == null)
                this.Response.Redirect("default.aspx");
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = !(this.Session["admin"].ToString() == "X") && !(this.Session["admin"].ToString() == "ADMINISTRADOR") ? "" : "ADMINISTRADOR";
            if (this.Page.IsPostBack)
                return;
            if (this.Session["nivel"] != null)
            {
                if (this.Session["nivel"].ToString() == "1")
                {
                    this.ddlQuejas.Visible = false;
                    this.upnRegistro.Update();
                }
                else
                {
                    this.ddlQuejas.Visible = true;
                    this.upnRegistro.Update();
                }
            }
            DataTable dataTable1 = this.con.buscaquejas(this.lblClave.Text, this.lblAdmin.Text);

            if (dataTable1.Rows.Count == 0)
            {
                dataTable1 = this.con.buscaquejas2(this.lblClave.Text);
            }
            else
            {
                foreach (DataRow row in (InternalDataCollectionBase)this.con.buscaquejas2(this.lblClave.Text).Rows)
                {
                    string str = row["que_folio"].ToString();
                    bool flag = false;
                    foreach (DataRow dataRow in dataTable1.Select("que_folio = '" + str + "'"))
                        flag = true;
                    if (!flag)
                        dataTable1.Rows.Add(row.ItemArray);
                }
            }
            if (dataTable1.Rows.Count == 0)
            {
                dataTable1 = this.con.buscaquejas3(this.lblClave.Text);
            }
            else
            {
                foreach (DataRow row in (InternalDataCollectionBase)this.con.buscaquejas3(this.lblClave.Text).Rows)
                {
                    string str = row["que_folio"].ToString();
                    bool flag = false;
                    foreach (DataRow dataRow in dataTable1.Select("que_folio = '" + str + "'"))
                        flag = true;
                    if (!flag)
                        dataTable1.Rows.Add(row.ItemArray);
                }
            }
            
            this.Session["quejas_page"] = (object)dataTable1;
            this.dtProductos.Columns.Add("nom_producto", typeof(string));
            this.dtProblemas.Columns.Add("pro_nombre", typeof(string));
            this.dtAreas.Columns.Add("area_nombre", typeof(string));
            this.dtVariedades.Columns.Add("qud_variedad", typeof(string));
            this.dtPedidos.Columns.Add("que_pedidos", typeof(string));
            this.dtProductos_1 = this.dtProductos;
            this.dtProblemas_1 = this.dtProblemas;
            this.dtAreas_1 = this.dtAreas;
            this.dtVariedades_1 = this.dtVariedades;
            this.dtPedidos_1 = this.dtPedidos;
            this.dtProductos.Rows.Add((object)"PRODUCTOS...");
            this.dtProblemas.Rows.Add((object)"PROBLEMAS...");
            this.dtAreas.Rows.Add((object)"AREAS...");
            this.dtVariedades.Rows.Add((object)"VARIEDADES...");
            this.dtPedidos.Rows.Add((object)"PEDIDOS...");
            this.dtProductos.Rows.Add((object)"TODOS");
            this.dtProblemas.Rows.Add((object)"TODOS");
            this.dtAreas.Rows.Add((object)"TODOS");
            this.dtVariedades.Rows.Add((object)"TODOS");
            this.dtPedidos.Rows.Add((object)"TODOS");
            if (dataTable1 != null)
            {
                DataView defaultView = dataTable1.DefaultView;
                defaultView.Sort = "que_folio DESC";
                DataTable table = defaultView.ToTable();
                this.dtProductos_1 = table.DefaultView.ToTable(true, "nom_producto");
                this.dtProblemas_1 = table.DefaultView.ToTable(true, "pro_nombre");
                this.dtAreas_1 = table.DefaultView.ToTable(true, "area_nombre");
                this.dtVariedades_1 = table.DefaultView.ToTable(true, "qud_variedad");
                this.dtPedidos_1 = table.DefaultView.ToTable(true, "que_pedido");
                foreach (DataRow dataRow in this.dtProductos_1.Select("nom_producto <> ''"))
                    this.dtProductos.Rows.Add(dataRow.ItemArray);
                foreach (DataRow dataRow in this.dtProblemas_1.Select("pro_nombre <> ''"))
                    this.dtProblemas.Rows.Add(dataRow.ItemArray);
                foreach (DataRow dataRow in this.dtAreas_1.Select("area_nombre <> ''"))
                    this.dtAreas.Rows.Add(dataRow.ItemArray);
                foreach (DataRow dataRow in this.dtVariedades_1.Select("qud_variedad <> ''"))
                    this.dtVariedades.Rows.Add(dataRow.ItemArray);
                foreach (DataRow dataRow in this.dtPedidos_1.Select("que_pedido <> ''"))
                    this.dtPedidos.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)table;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado"] = (object)table;
            }
            this.cmbProducto.DataSource = (object)this.dtProductos;
            this.cmbProducto.DataTextField = "nom_producto";
            this.cmbProducto.DataValueField = "nom_producto";
            this.cmbProducto.DataBind();
            this.cmbProblema.DataSource = (object)this.dtProblemas;
            this.cmbProblema.DataTextField = "pro_nombre";
            this.cmbProblema.DataValueField = "pro_nombre";
            this.cmbProblema.DataBind();
            this.cmbArea.DataSource = (object)this.dtAreas;
            this.cmbArea.DataTextField = "area_nombre";
            this.cmbArea.DataValueField = "area_nombre";
            this.cmbArea.DataBind();
            this.cmbVariedad.DataSource = (object)this.dtVariedades;
            this.cmbVariedad.DataTextField = "qud_variedad";
            this.cmbVariedad.DataValueField = "qud_variedad";
            this.cmbVariedad.DataBind();
            this.cmbPedido.DataSource = (object)this.dtPedidos;
            this.cmbPedido.DataTextField = "que_pedidos";
            this.cmbPedido.DataValueField = "que_pedidos";
            this.cmbPedido.DataBind();

            lblDevolucion.BackColor = Color.LightBlue;
            lblMerma.BackColor = Color.LightGreen;
            lblBonificacion.BackColor = Color.LightGray;
        }

        protected void ddlQuejas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlQuejas.SelectedValue == "1")
            {
                this.Session["clave"] = (object)this.lblClave.Text;
                this.Session["nombre"] = (object)this.lblNombre.Text;
                this.Session["cedis"] = (object)this.lblCedis.Text;
                this.Session["admin"] = (object)this.lblAdmin.Text;
                //this.Response.Redirect("principalnew.aspx");
                //if (this.Session["nombre"].ToString() == "ANGEL ESCAMILLA")
                    
                //else
                this.Response.Redirect("principalnew.aspx");
            }
            if (this.ddlQuejas.SelectedValue == "2")
            {
                this.Session["clave"] = (object)this.lblClave.Text;
                this.Session["nombre"] = (object)this.lblNombre.Text;
                this.Session["cedis"] = (object)this.lblCedis.Text;
                this.Session["admin"] = (object)this.lblAdmin.Text;
                //this.Response.Redirect("principalexpnew.aspx");
                //if (this.Session["nombre"].ToString() == "ANGEL ESCAMILLA")
                //    this.Response.Redirect("principalexpnew.aspx");
                //else
                this.Response.Redirect("principalexpnew.aspx");
                
            }
            if (this.ddlQuejas.SelectedValue == "3")
            {
                this.Session["clave"] = (object)this.lblClave.Text;
                this.Session["nombre"] = (object)this.lblNombre.Text;
                this.Session["cedis"] = (object)this.lblCedis.Text;
                this.Session["admin"] = (object)this.lblAdmin.Text;
                this.Response.Redirect("quejas_serv.aspx");
            }
            if (!(this.ddlQuejas.SelectedValue == "4"))
                return;
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas_fumigacion.aspx");
        }

        protected void gvwQuejas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consulta")
            {
                GridViewRow row = this.gvwQuejas.Rows[Convert.ToInt32(e.CommandArgument)];
                this.Session["clave"] = (object)this.lblClave.Text;
                this.Session["nombre"] = (object)this.lblNombre.Text;
                this.Session["cedis"] = (object)this.lblCedis.Text;
                this.Session["admin"] = (object)this.lblAdmin.Text;
                this.Session["cliente"] = (object)this.Server.HtmlDecode(row.Cells[4].Text);
                this.Session["queja"] = (object)this.Server.HtmlDecode(row.Cells[1].Text);
                this.Response.Redirect("contenido.aspx");
            }
            if (e.CommandName == "productos")
            {
                GridViewRow row = this.gvwQuejas.Rows[Convert.ToInt32(e.CommandArgument)];
                if (!this.Server.HtmlDecode(row.Cells[18].Text).Contains("PRODUCTOS"))
                    return;
                this.gvwProductos.DataSource = (object)this.con.Detalle_Productos_Queja(this.Server.HtmlDecode(row.Cells[1].Text));
                this.gvwProductos.DataBind();
                this.upnProductos.Update();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
            }
            if (!(e.CommandName == "variedades"))
                return;
            GridViewRow row1 = this.gvwQuejas.Rows[Convert.ToInt32(e.CommandArgument)];
            if (!this.Server.HtmlDecode(row1.Cells[19].Text).Contains("VARIEDADES"))
                return;
            this.gvwProductos.DataSource = (object)this.con.Detalle_Variedades_Queja(this.Server.HtmlDecode(row1.Cells[1].Text));
            this.gvwProductos.DataBind();
            this.upnProductos.Update();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
        }

        protected void gvwQuejas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvwQuejas.PageIndex = e.NewPageIndex;
            DataView defaultView = ((DataTable)this.Session["quejas_page"]).DefaultView;
            defaultView.Sort = "que_folio DESC";
            this.gvwQuejas.DataSource = (object)defaultView.ToTable();
            this.gvwQuejas.DataBind();
        }

        protected void gvwQuejas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            if (Convert.ToInt32(e.Row.Cells[20].Text) == 1)
            {
                e.Row.Cells[2].BackColor = Color.LightBlue;
                e.Row.Cells[3].BackColor = Color.LightBlue;
                e.Row.Cells[4].BackColor = Color.LightBlue;
                e.Row.Cells[5].BackColor = Color.LightBlue;
                e.Row.Cells[6].BackColor = Color.LightBlue;
                e.Row.Cells[7].BackColor = Color.LightBlue;
                e.Row.Cells[9].BackColor = Color.LightBlue;
                e.Row.Cells[10].BackColor = Color.LightBlue;
                e.Row.Cells[12].BackColor = Color.LightBlue;
                //e.Row.Cells[2].ForeColor = Color.Yellow;
            }
            if (Convert.ToInt32(e.Row.Cells[20].Text) == 2)
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
                e.Row.Cells[3].BackColor = Color.LightGreen;
                e.Row.Cells[4].BackColor = Color.LightGreen;
                e.Row.Cells[5].BackColor = Color.LightGreen;
                e.Row.Cells[6].BackColor = Color.LightGreen;
                e.Row.Cells[7].BackColor = Color.LightGreen;
                e.Row.Cells[9].BackColor = Color.LightGreen;
                e.Row.Cells[10].BackColor = Color.LightGreen;
                e.Row.Cells[12].BackColor = Color.LightGreen;
                //e.Row.Cells[2].ForeColor = Color.Yellow;
            }
            if (Convert.ToInt32(e.Row.Cells[20].Text) == 3)
            {
                e.Row.Cells[2].BackColor = Color.LightGray;
                e.Row.Cells[3].BackColor = Color.LightGray;
                e.Row.Cells[4].BackColor = Color.LightGray;
                e.Row.Cells[5].BackColor = Color.LightGray;
                e.Row.Cells[6].BackColor = Color.LightGray;
                e.Row.Cells[7].BackColor = Color.LightGray;
                e.Row.Cells[9].BackColor = Color.LightGray;
                e.Row.Cells[10].BackColor = Color.LightGray;
                e.Row.Cells[12].BackColor = Color.LightGray;
                //e.Row.Cells[2].ForeColor = Color.Yellow;
            }

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
            if (e.Row.Cells[13].Text == "2")
            {
                e.Row.Cells[13].BackColor = Color.Red;
                e.Row.Cells[13].ForeColor = Color.Red;
            }
            if (e.Row.Cells[14].Text == "0")
            {
                e.Row.Cells[14].BackColor = Color.White;
                e.Row.Cells[14].ForeColor = Color.White;
            }
            if (e.Row.Cells[14].Text == "1")
            {
                e.Row.Cells[14].BackColor = Color.Green;
                e.Row.Cells[14].ForeColor = Color.Green;
            }
            if (e.Row.Cells[14].Text == "2")
            {
                e.Row.Cells[14].BackColor = Color.Red;
                e.Row.Cells[14].ForeColor = Color.Red;
            }
            if (e.Row.Cells[15].Text == "0")
            {
                e.Row.Cells[15].BackColor = Color.White;
                e.Row.Cells[15].ForeColor = Color.White;
            }
            if (e.Row.Cells[15].Text == "1")
            {
                e.Row.Cells[15].BackColor = Color.Green;
                e.Row.Cells[15].ForeColor = Color.Green;
            }
            if (e.Row.Cells[15].Text == "2")
            {
                e.Row.Cells[15].BackColor = Color.Red;
                e.Row.Cells[15].ForeColor = Color.Red;
            }
            if (e.Row.Cells[16].Text == "0")
            {
                e.Row.Cells[16].BackColor = Color.White;
                e.Row.Cells[16].ForeColor = Color.White;
            }
            if (e.Row.Cells[16].Text == "1")
            {
                e.Row.Cells[16].BackColor = Color.Green;
                e.Row.Cells[16].ForeColor = Color.Green;
            }
            if (e.Row.Cells[16].Text == "2")
            {
                e.Row.Cells[16].BackColor = Color.Red;
                e.Row.Cells[16].ForeColor = Color.Red;
            }
            if (e.Row.Cells[17].Text == "0")
            {
                e.Row.Cells[17].BackColor = Color.White;
                e.Row.Cells[17].ForeColor = Color.White;
            }
            if (e.Row.Cells[17].Text == "1")
            {
                e.Row.Cells[17].BackColor = Color.Green;
                e.Row.Cells[17].ForeColor = Color.Green;
            }
            if (e.Row.Cells[17].Text == "2")
            {
                e.Row.Cells[17].BackColor = Color.Red;
                e.Row.Cells[17].ForeColor = Color.Red;
            }
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("default.aspx");
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("reportes.aspx");
        }

        protected void btnAyuda_Click(object sender, EventArgs e)
        {
            this.Response.Write("<script>window.open('css/Manual Nuevo Sistema de Quejas.pdf','_blank')</script>");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Session["queja"] = (object)this.Server.HtmlDecode(this.txtQueja.Text);
            this.Session["cliente"] = (object)this.Server.HtmlDecode(this.con.consulta_cliente(this.txtQueja.Text));
            this.Response.Redirect("contenido.aspx");
        }

        protected void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...")
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();

                if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PROBLEMA SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PROBLEMA SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PROBLEMA NO AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PROBLEMA NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//NO PROBLEMA SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" || this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PROBLEMA SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" || this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//NO PROBLEMA NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else
                {
                    this.gvwQuejas.DataSource = (object)dataTable2;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable2;
                }
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();

                if ((this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") && 
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") && 
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PROBLEMA SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PROBLEMA SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PROBLEMA NO AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PROBLEMA NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//NO PROBLEMA SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PROBLEMA SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProblema.SelectedValue.ToString() != "TODOS" || this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//NO PROBLEMA NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.Session["quejas_page"] = (object)dataTable4;
            }
        }

        protected void cmbProblema_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...")
            {

                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                
                if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PRODUCTO SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' " +
                        "AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PRODUCTO SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PRODUCTO NO AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PRODUCTO NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//NO PRODUCTO SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" || this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" && this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PRODUCTO SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" || this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" && this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PRODUCTO NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);

                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else
                {
                    this.gvwQuejas.DataSource = (object)dataTable2;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable2;
                }
                
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();

                if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PRODUCTO NO AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") && 
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PRODUCTO SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + " AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' " +
                        "AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PRODUCTO SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI PRODUCTO NO AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI PRODUCTO NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() +
                        "' AND pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//NO PRODUCTO SI AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() +
                        "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" || this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" && this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PRODUCTO SI AREA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" || this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" && this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PRODUCTO NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() +
                        "' AND qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                    (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS...") &&
                    (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//NO PRODUCTO NO AREA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() +
                        "' AND area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                //foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                //    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.Session["quejas_page"] = (object)dataTable4;
            }
        }

        protected void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS...")
            {

                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();

                if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA SI PRODUCTO SI PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA SI PRODUCTO SI PROBLEMA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA SI PRODUCTO NO PROBLEMA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                //else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                //     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "AREAS...") &&
                //     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA NO PRODUCTO NO PROBLEMA NO VARIEDAD
                //{
                //    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                //        dataTable4.Rows.Add(dataRow.ItemArray);
                //    this.gvwQuejas.DataSource = (object)dataTable4;
                //    this.gvwQuejas.DataBind();
                //    this.Session["quejas_page"] = (object)dataTable4;
                //}
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA NO PRODUCTO NO PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA NO PRODUCTO SI PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA NO PRODUCTO SI PROBLEMA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA SI PRODUCTO NO PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else
                {
                    this.gvwQuejas.DataSource = (object)dataTable2;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable2;
                }

                
            }
            else
            {
                //DataTable dataTable3 = new DataTable();
                //DataTable dataTable4 = dataTable2.Copy();
                //dataTable4.Clear();
                //foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                //    dataTable4.Rows.Add(dataRow.ItemArray);

                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();

                if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA SI PRODUCTO SI PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA SI PRODUCTO SI PROBLEMA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA SI PRODUCTO NO PROBLEMA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA NO PRODUCTO NO PROBLEMA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA NO PRODUCTO NO PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' AND " +
                        "qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA NO PRODUCTO SI PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES..."))//SI AREA NO PRODUCTO SI PROBLEMA NO VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbVariedad.SelectedValue.ToString() != "TODOS" && this.cmbVariedad.SelectedValue.ToString() != "VARIEDADES..."))//SI AREA SI PRODUCTO NO PROBLEMA SI VARIEDAD
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }

                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.Session["quejas_page"] = (object)dataTable4;
            }
        }

        protected void cmbVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbVariedad.SelectedValue.ToString() == "TODOS" || this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES...")
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();

                if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD SI PRODUCTO SI PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD SI PRODUCTO SI PROBLEMA NO AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD SI PRODUCTO NO PROBLEMA NO AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                //else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                //     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                //     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD NO PRODUCTO NO PROBLEMA NO AREA
                //{
                //    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                //        dataTable4.Rows.Add(dataRow.ItemArray);
                //    this.gvwQuejas.DataSource = (object)dataTable4;
                //    this.gvwQuejas.DataBind();
                //    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                //    this.Session["quejas_page"] = (object)dataTable4;
                //}
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD NO PRODUCTO NO PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD NO PRODUCTO SI PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD NO PRODUCTO SI PROBLEMA NO AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD SI PRODUCTO NO PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                    this.gvwQuejas.DataSource = (object)dataTable4;
                    this.gvwQuejas.DataBind();
                    this.Session["Filtrado_Reporte"] = (object)dataTable4;
                    this.Session["quejas_page"] = (object)dataTable4;
                }
                else
                {
                    this.gvwQuejas.DataSource = (object)dataTable2;
                    this.gvwQuejas.DataBind();
                    this.Session["quejas_page"] = (object)dataTable2;
                }
                
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();

                if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD SI PRODUCTO SI PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD SI PRODUCTO SI PROBLEMA NO AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD SI PRODUCTO NO PROBLEMA NO AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD NO PRODUCTO NO PROBLEMA NO AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD NO PRODUCTO NO PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "' AND " +
                        "area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD NO PRODUCTO SI PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "' AND " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "' AND " +
                        "area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() == "TODOS" || this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() != "TODOS" && this.cmbProblema.SelectedValue.ToString() != "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() == "TODOS" || this.cmbArea.SelectedValue.ToString() == "AREAS..."))//SI VARIEDAD NO PRODUCTO SI PROBLEMA NO AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "' " +
                        "pro_nombre = '" + this.cmbProblema.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                else if ((this.cmbProducto.SelectedValue.ToString() != "TODOS" && this.cmbProducto.SelectedValue.ToString() != "PRODUCTOS...") &&
                     (this.cmbProblema.SelectedValue.ToString() == "TODOS" || this.cmbProblema.SelectedValue.ToString() == "PROBLEMAS...") &&
                     (this.cmbArea.SelectedValue.ToString() != "TODOS" && this.cmbArea.SelectedValue.ToString() != "AREAS..."))//SI VARIEDAD SI PRODUCTO NO PROBLEMA SI AREA
                {
                    foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "' AND " +
                        "nom_producto = '" + this.cmbProducto.SelectedValue.ToString() + "' AND " +
                        "area_nombre = '" + this.cmbArea.SelectedValue.ToString() + "'"))
                        dataTable4.Rows.Add(dataRow.ItemArray);
                }
                //foreach (DataRow dataRow in dataTable2.Select("qud_variedad = '" + this.cmbVariedad.SelectedValue.ToString() + "'"))
                //    dataTable4.Rows.Add(dataRow.ItemArray);


                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.Session["quejas_page"] = (object)dataTable4;
            }
        }

        protected void cmbPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable)this.Session["Filtrado"];
            if (this.cmbPedido.SelectedValue.ToString() == "TODOS" || this.cmbPedido.SelectedValue.ToString() == "PEDIDOS...")
            {
                this.gvwQuejas.DataSource = (object)dataTable2;
                this.gvwQuejas.DataBind();
                this.Session["quejas_page"] = (object)dataTable2;
            }
            else
            {
                DataTable dataTable3 = new DataTable();
                DataTable dataTable4 = dataTable2.Copy();
                dataTable4.Clear();
                foreach (DataRow dataRow in dataTable2.Select("que_pedido = '" + this.cmbPedido.SelectedValue.ToString() + "'"))
                    dataTable4.Rows.Add(dataRow.ItemArray);
                this.gvwQuejas.DataSource = (object)dataTable4;
                this.gvwQuejas.DataBind();
                this.Session["Filtrado_Reporte"] = (object)dataTable4;
                this.Session["quejas_page"] = (object)dataTable4;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            bool flag = !(this.cmbProducto.SelectedValue.ToString() == "PRODUCTOS...") && !(this.cmbProducto.SelectedValue.ToString() == "TODOS");
            if (!flag)
            {
                flag = !(this.cmbArea.SelectedValue.ToString() == "AREAS...") && !(this.cmbArea.SelectedValue.ToString() == "TODOS");
                if (!flag)
                {
                    flag = !(this.cmbVariedad.SelectedValue.ToString() == "VARIEDADES...") && !(this.cmbVariedad.SelectedValue.ToString() == "TODOS");
                    if (!flag)
                        flag = !(this.cmbPedido.SelectedValue.ToString() == "PEDIDOS...") && !(this.cmbPedido.SelectedValue.ToString() == "TODOS");
                }
            }
            if (!flag)
                return;
            DataTable dataTable1 = new DataTable();
            DataTable dtFiltro = (DataTable)this.Session["Filtrado_Reporte"];
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            str1 = this.cmbProducto.SelectedValue.ToString();
            str2 = this.cmbProblema.SelectedValue.ToString();
            str3 = this.cmbArea.SelectedValue.ToString();
            str4 = this.cmbVariedad.SelectedValue.ToString();
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("semana", typeof(string));
            dataTable2.Columns.Add("folio", typeof(string));
            dataTable2.Columns.Add("fecha", typeof(string));
            dataTable2.Columns.Add("mes", typeof(string));
            dataTable2.Columns.Add("primario", typeof(string));
            dataTable2.Columns.Add("cliente", typeof(string));
            dataTable2.Columns.Add("sucursal", typeof(string));
            dataTable2.Columns.Add("reporto", typeof(string));
            dataTable2.Columns.Add("recibio", typeof(string));
            dataTable2.Columns.Add("producto", typeof(string));
            dataTable2.Columns.Add("variedad", typeof(string));
            dataTable2.Columns.Add("problema", typeof(string));
            dataTable2.Columns.Add("orden_produccion", typeof(string));
            dataTable2.Columns.Add("lote", typeof(string));
            dataTable2.Columns.Add("area", typeof(string));
            dataTable2.Columns.Add("responsable", typeof(string));
            dataTable2.Columns.Add("caducidad", typeof(string));
            dataTable2.Columns.Add("recibidas", typeof(string));
            dataTable2.Columns.Add("rechazadas", typeof(string));
            dataTable2.Columns.Add("unidad", typeof(string));
            dataTable2.Columns.Add("devolucion", typeof(string));
            dataTable2.Columns.Add("moneda", typeof(string));
            dataTable2.Columns.Add("proveedor", typeof(string));
            dataTable2.Columns.Add("rancho", typeof(string));
            dataTable2.Columns.Add("tabla", typeof(string));
            dataTable2.Columns.Add("tarima", typeof(string));
            dataTable2.Columns.Add("investigador", typeof(string));
            dataTable2.Columns.Add("causa", typeof(string));
            dataTable2.Columns.Add("fecha_entrega", typeof(string));
            dataTable2.Columns.Add("cumplimiento", typeof(string));
            dataTable2.Columns.Add("responsable_acciones", typeof(string));
            dataTable2.Columns.Add("acciones", typeof(string));
            dataTable2.Columns.Add("fecha_termino_acciones", typeof(string));
            dataTable2.Columns.Add("responsable_verificacion", typeof(string));
            dataTable2.Columns.Add("fecha_verificacion", typeof(string));
            dataTable2.Columns.Add("acciones_cumplidas", typeof(string));
            dataTable2.Columns.Add("comen_acciones_cumplidas", typeof(string));
            dataTable2.Columns.Add("acciones_efectivas", typeof(string));
            dataTable2.Columns.Add("comen_acciones_efectivas", typeof(string));
            dataTable2.Columns.Add("pedido", typeof(string));
            dataTable2.Columns.Add("transporte", typeof(string));
            DataTable dataTable3 = new DataTable();
            foreach (DataRow row1 in (InternalDataCollectionBase)this.con.general_mstr_quejas_principal(dtFiltro, this.lblClave.Text, this.lblCedis.Text, this.lblAdmin.Text).Rows)
            {
                DataRow row2 = dataTable2.NewRow();
                row2["semana"] = (object)row1["que_semana"].ToString();
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
                row2["transporte"] = (object)row1["transporte"].ToString();
                dataTable2.Rows.Add(row2);
            }
            DataTable dataTable4 = new DataTable();
            DataTable dataTable5 = this.con.general_det_quejas_principal();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
            {
                foreach (DataRow dataRow1 in dataTable5.Select("que_folio = '" + row["folio"].ToString() + "'"))
                {
                    string str5 = dataRow1["nom_producto"].ToString();
                    if (!row["producto"].ToString().Contains(str5))
                    {
                        DataRow dataRow2;
                        (dataRow2 = row)["producto"] = (object)(dataRow2["producto"].ToString() + str5);
                    }
                    row["variedad"] = (object)dataRow1["qud_variedad"].ToString();
                    row["problema"] = (object)dataRow1["pro_nombre"].ToString();
                    row["orden_produccion"] = (object)dataRow1["qud_ordprod"].ToString();
                    row["lote"] = (object)dataRow1["qud_lote"].ToString();
                    row["responsable"] = (object)dataRow1["qud_responsable"].ToString();
                    row["caducidad"] = (object)dataRow1["fecha_cad"].ToString();
                    row["recibidas"] = (object)dataRow1["qud_cantreci"].ToString();
                    row["rechazadas"] = (object)dataRow1["qud_cantrecha"].ToString();
                    row["unidad"] = (object)dataRow1["qud_unidad"].ToString();
                    row["devolucion"] = dataRow1["qud_devolucion"].ToString() == "1" ? (object)"SI" : (object)"NO";
                    row["moneda"] = (object)dataRow1["qud_moneda"].ToString();
                    row["proveedor"] = (object)dataRow1["prov_nombre"].ToString();
                    row["rancho"] = (object)dataRow1["rch_nombre"].ToString();
                    row["tabla"] = (object)dataRow1["tbl_nombre"].ToString();
                    row["tarima"] = (object)dataRow1["qud_tarima"].ToString();
                }
            }
            DataTable dataTable6 = new DataTable();
            DataTable dataTable7 = this.con.general_det_investigacion_principal();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
            {
                string str5 = "";
                foreach (DataRow dataRow in dataTable7.Select("que_folio = '" + row["folio"].ToString() + "'"))
                    str5 = str5 + dataRow["responsable"].ToString() + ", ";
                string str6 = str5.TrimEnd(' ');
                row["investigador"] = (object)str6.TrimEnd(',');
            }
            DataTable dataTable8 = new DataTable();
            DataTable dataTable9 = this.con.general_mstr_acciones();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
            {
                string str5 = "";
                string str6 = "";
                foreach (DataRow dataRow in dataTable9.Select("que_folio = '" + row["folio"].ToString() + "'"))
                {
                    str5 = str5 + dataRow["acc_causa"].ToString() + ", ";
                    str6 = str6 + dataRow["acc_fecha"].ToString() + ", ";
                }
                string str7 = str5.TrimEnd(' ');
                row["causa"] = (object)str7.TrimEnd(',');
                string str8 = str6.TrimEnd(' ');
                row["fecha_entrega"] = (object)str8.TrimEnd(',');
            }
            DataTable dataTable10 = new DataTable();
            DataTable dataTable11 = this.con.general_det_acciones_principal();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
            {
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";
                string str9 = "";
                string str10 = "";
                string str11 = "";
                string str12 = "";
                foreach (DataRow dataRow in dataTable11.Select("que_folio = '" + row["folio"].ToString() + "'"))
                {
                    str5 = str5 + dataRow["acc_cumpli_ver"].ToString() + ", ";
                    str6 = str6 + dataRow["resp_nombre"].ToString() + ", ";
                    str7 = str7 + dataRow["acc_accion"].ToString() + ", ";
                    str8 = str8 + dataRow["acc_fechatermino"].ToString() + ", ";
                    str10 = str10 + dataRow["acc_fecha_ver"].ToString() + ", ";
                    str11 = str11 + dataRow["acc_cumpli_ver"].ToString() + ", ";
                    str12 = str12 + dataRow["acc_comen_ver"].ToString() + ", ";
                }
                row["cumplimiento"] = (object)str5;
                row["responsable_acciones"] = (object)str6;
                row["acciones"] = (object)str7;
                row["fecha_termino_acciones"] = (object)str8;
                row["responsable_verificacion"] = (object)str9;
                row["fecha_verificacion"] = (object)str10;
                row["acciones_cumplidas"] = (object)str11;
                row["comen_acciones_cumplidas"] = (object)str12;
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=Reporte_Quejas.xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            response.ContentEncoding = Encoding.Unicode;
            response.BinaryWrite(Encoding.Unicode.GetPreamble());
            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                {
                    DataGrid dataGrid = new DataGrid();
                    dataGrid.DataSource = (object)dataTable2;
                    dataGrid.DataBind();
                    dataGrid.RenderControl(writer);
                    response.Write(stringWriter.ToString());
                    dataGrid.Dispose();
                    response.End();
                }
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

        protected void btnTransportes_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas_serv.aspx");
        }
    }
}