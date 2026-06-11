using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace queja
{
    public partial class principalexp : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            if (!Page.IsPostBack)
            {
                this.dtTrazabilidad.Columns.AddRange(new DataColumn[21]
              {
                new DataColumn("pedido", typeof (string)),
                new DataColumn("id_producto", typeof (string)),
                new DataColumn("qud_ordprod", typeof (string)),
                new DataColumn("qud_area", typeof (string)),
                new DataColumn("qud_responsable", typeof (string)),
                new DataColumn("qud_cantrecha", typeof (string)),
                new DataColumn("qud_cantreci", typeof (string)),
                new DataColumn("qud_unidad", typeof (string)),
                new DataColumn("qud_devolucion", typeof (string)),
                new DataColumn("qud_moneda", typeof (string)),
                new DataColumn("prov_clave", typeof (string)),
                new DataColumn("rch_clave", typeof (string)),
                new DataColumn("tbl_clave", typeof (string)),
                new DataColumn("qud_lote", typeof (string)),
                new DataColumn("nom_producto", typeof (string)),
                new DataColumn("fecha_cad", typeof (string)),
                new DataColumn("qud_tarima", typeof (string)),
                new DataColumn("prov_nombre", typeof (string)),
                new DataColumn("rch_nombre", typeof (string)),
                new DataColumn("tbl_nombre", typeof (string)),
                new DataColumn("tipo", typeof (string))
              });
                this.dtTabla.Columns.AddRange(new DataColumn[27]
              {
                new DataColumn("pedido", typeof (string)),
                new DataColumn("id_producto", typeof (string)),
                new DataColumn("qud_ordprod", typeof (string)),
                new DataColumn("qud_area", typeof (string)),
                new DataColumn("qud_responsable", typeof (string)),
                new DataColumn("qud_cantrecha", typeof (string)),
                new DataColumn("qud_cantreci", typeof (string)),
                new DataColumn("qud_unidad", typeof (string)),
                new DataColumn("qud_devolucion", typeof (string)),
                new DataColumn("qud_moneda", typeof (string)),
                new DataColumn("prov_clave", typeof (string)),
                new DataColumn("rch_clave", typeof (string)),
                new DataColumn("tbl_clave", typeof (string)),
                new DataColumn("qud_lote", typeof (string)),
                new DataColumn("nom_producto", typeof (string)),
                new DataColumn("fecha_cad", typeof (string)),
                new DataColumn("qud_tarima", typeof (string)),
                new DataColumn("prov_nombre", typeof (string)),
                new DataColumn("rch_nombre", typeof (string)),
                new DataColumn("tbl_nombre", typeof (string)),
                new DataColumn("tipo", typeof (string)),
                new DataColumn("vari", typeof (string)),
                new DataColumn("producidas", typeof (string)),
                new DataColumn("porcentaje", typeof (string)),
                new DataColumn("rech_gral", typeof (string)),
                new DataColumn("qud_merma",typeof(string)),
                new DataColumn("cnte",typeof(string))
              });
                this.Session["DETALLE"] = (object)this.dtTabla;
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
                TextBox txtFolio = this.txtFolio;
                int num = this.con.cargafolio();
                string str1 = num.ToString();
                txtFolio.Text = str1;
                TextBox txtSemana = this.txtSemana;
                num = this.con.cargasemana();
                string str2 = num.ToString();
                txtSemana.Text = str2;
                TextBox txtMes = this.txtMes;
                num = DateTime.Now.Month;
                string str3 = num.ToString().PadLeft(2, '0');
                txtMes.Text = str3;
                string f_c_H = DateTime.Now.ToShortDateString();
                this.txtFecha.Text = Convert.ToDateTime(f_c_H).ToString("dd/MM/yyyy");
                this.lblVerifica.Text = "0";

                lblCosto.Visible = false;
                txtCosto.Visible = false;
                uplCosto.Update();

                lblSufijo.Visible = false;
                txtSufijo.Visible = false;
                uplSufijo.Update();

                grvFacturas.Visible = false;
                uplFacturas.Update();
            }

            
            
        }

        protected void btnBuscaOrden_Click(object sender, EventArgs e)
        {
            this.dtPlacas.Clear();
            this.dtPlacas = this.con.comboplacas(this.txtFechaCarga.Text);
            this.ddlPlacas.DataSource = (object)this.dtPlacas;
            this.Session["DTPLACAS"] = (object)this.dtPlacas;
            this.ddlPlacas.DataTextField = "no_trailer";
            this.ddlPlacas.DataValueField = "pdn_folio";
            this.ddlPlacas.DataBind();
            this.udpPlacas.Update();
        }

        protected void ddlPlacas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string selected_index = ddlPlacas.SelectedIndex.ToString();
            //DropDownList ddl = (DropDownList)sender;
            //Debug.WriteLine(ddlPlacas.SelectedItem.Text);

            this.dtTabla = (DataTable)this.Session["DETALLE"];
            this.dtTabla.Clear();
            this.grvDetalle.DataSource = (object)this.dtTabla;
            this.grvDetalle.DataBind();
            this.uplDetalle.Update();
            this.Session["DETALLE"] = (object)"";
            this.Session["DETALLE"] = (object)this.dtTabla;
            this.txtEmbarque.Text = this.ddlPlacas.SelectedValue.ToString();
            this.txtTransporte.Text = this.ddlPlacas.SelectedItem.ToString();

            this.dtEmbarques = this.con.detalle_embarque_concentrado(this.txtEmbarque.Text, this.txtTransporte.Text.Split('-')[0].ToString().Trim(), this.txtFechaCarga.Text, this.ddlTipo.SelectedValue.ToString(), this.txtTransporte.Text.Split('-')[2].ToString().Trim());
            DataTable dataTable = new DataTable();
            this.dtTabla = this.dtEmbarques.Copy();
            DataTable table = new DataView(this.dtTabla).ToTable(true, "pdn_folio");
            DataTable table2 = new DataView(this.dtTabla).ToTable(true, "fact");
            this.Session["FACTURAS"] = table2;
            string str = "";
            string str2 = "";
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                str = str + row["pdn_folio"].ToString() + ", ";
            this.txtEmbarques.Text = str.TrimEnd().ToString().Trim(',').ToString();
            foreach (DataRow row in (InternalDataCollectionBase)table2.Rows)
                str2 = str2 + row["fact"].ToString() + ", ";
            string fact = str2;
            txtFactura.Text = fact.TrimEnd().ToString().Trim(',').ToString();
            if (this.txtEmbarque.Text == "000000")
                this.txtEmbarque.Text = this.dtEmbarques.Rows[0][0].ToString();
            txtFechaEmb.Text = txtFechaCarga.Text;
            txtSemEmb.Text = con.cargasemana_embarque(txtFechaEmb.Text).ToString();
            this.gvwQuejas.DataSource = (object)this.dtEmbarques;
            this.gvwQuejas.DataBind();
            this.upnQuejas.Update();

            this.grvFacturas.DataSource = table2;
            this.grvFacturas.DataBind();
            this.uplFacturas.Update();
        }

        protected void gvwQuejas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "consulta"))
                return;
            GridViewRow row1 = this.gvwQuejas.Rows[Convert.ToInt32(e.CommandArgument)];
            string prod_clave = this.Server.HtmlDecode(row1.Cells[1].Text);
            string folio = this.Server.HtmlDecode(row1.Cells[5].Text);
            TextBox control = (TextBox)row1.Cells[4].FindControl("txtRechazadas2");
            string str1 = control.Text;
            string str2 = this.Server.HtmlDecode(row1.Cells[3].Text);
            string strPedido = this.Server.HtmlDecode(row1.Cells[5].Text);
            string strCnte = this.Server.HtmlDecode(row1.Cells[6].Text);
            if (control.Text == "0")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Ingrese la cantidad rechazada');", true);
            }
            else
            {
                if (Convert.ToInt32(str1) > Convert.ToInt32(str2))
                {
                    str1 = str2;
                    control.Text = str2.ToString();
                }
                DataRow dataRow1;
                DataTable dataTable1;
                if (Convert.ToInt32(str1) == 0)
                {
                    DataTable dataTable2 = (DataTable)this.Session["DETALLE"];
                    bool flag = false;
                    DataRow[] dataRowArray = dataTable2.Select("id_producto = '" + prod_clave + "'");
                    int index = 0;
                    if (index < dataRowArray.Length)
                    {
                        dataRow1 = dataRowArray[index];
                        flag = true;
                    }
                    if (flag)
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Producto anteriormente seleccionado');", true);
                    }
                    else
                    {
                        dataTable1 = new DataTable();
                        DataTable dataTable3 = this.con.detalle_embarque_detalle(folio, prod_clave);
                        if (dataTable2.Rows.Count > 0)
                        {
                            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable3.Rows)
                            {
                                DataRow row3 = dataTable2.NewRow();
                                row3["pedido"] = (object)row2["pedido"].ToString();
                                row3["id_producto"] = (object)row2["prod_clave"].ToString();
                                row3["qud_ordprod"] = (object)row2["recibo"].ToString();
                                row3["qud_area"] = (object)row2["qud_area"].ToString();
                                row3["qud_responsable"] = (object)row2["qud_responsable"].ToString();
                                row3["qud_cantrecha"] = (object)"0";
                                row3["qud_cantreci"] = (object)row2["cajas"].ToString();
                                row3["qud_unidad"] = (object)row2["qud_unidad"].ToString();
                                row3["qud_devolucion"] = (object)"";
                                row3["qud_moneda"] = (object)"";
                                row3["prov_clave"] = (object)row2["prov_clave"].ToString();
                                row3["rch_clave"] = (object)row2["rch_clave"].ToString();
                                row3["tbl_clave"] = (object)row2["tbl_clave"].ToString();
                                row3["qud_lote"] = (object)row2["qud_lote"].ToString();
                                row3["nom_producto"] = (object)row2["prod_nombre"].ToString();
                                row3["fecha_cad"] = (object)row2["fec_cad"].ToString();
                                row3["qud_tarima"] = (object)row2["tarima"].ToString();
                                row3["prov_nombre"] = (object)row2["prov_nombre"].ToString();
                                row3["rch_nombre"] = (object)row2["rch_nombre"].ToString();
                                row3["tbl_nombre"] = (object)row2["tbl_nombre"].ToString();
                                row3["tipo"] = (object)row2["tipo_rec"].ToString();
                                row3["vari"] = (object)row2["vari_nombre"].ToString();
                                row3["cnte"] = (object)strCnte;
                                dataTable2.Rows.Add(row3);
                            }
                        }
                        else
                        {
                            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable3.Rows)
                            {
                                DataRow row3 = dataTable2.NewRow();
                                row3["pedido"] = (object)row2["pedido"].ToString();
                                row3["id_producto"] = (object)row2["prod_clave"].ToString();
                                row3["qud_ordprod"] = (object)row2["recibo"].ToString();
                                row3["qud_area"] = (object)row2["qud_area"].ToString();
                                row3["qud_responsable"] = (object)row2["qud_responsable"].ToString();
                                row3["qud_cantrecha"] = (object)"0";
                                row3["qud_cantreci"] = (object)row2["cajas"].ToString();
                                row3["qud_unidad"] = (object)row2["qud_unidad"].ToString();
                                row3["qud_devolucion"] = (object)"";
                                row3["qud_moneda"] = (object)"";
                                row3["prov_clave"] = (object)row2["prov_clave"].ToString();
                                row3["rch_clave"] = (object)row2["rch_clave"].ToString();
                                row3["tbl_clave"] = (object)row2["tbl_clave"].ToString();
                                row3["qud_lote"] = (object)row2["qud_lote"].ToString();
                                row3["nom_producto"] = (object)row2["prod_nombre"].ToString();
                                row3["fecha_cad"] = (object)row2["fec_cad"].ToString();
                                row3["qud_tarima"] = (object)row2["tarima"].ToString();
                                row3["prov_nombre"] = (object)row2["prov_nombre"].ToString();
                                row3["rch_nombre"] = (object)row2["rch_nombre"].ToString();
                                row3["tbl_nombre"] = (object)row2["tbl_nombre"].ToString();
                                row3["tipo"] = (object)row2["tipo_rec"].ToString();
                                row3["vari"] = (object)row2["vari_nombre"].ToString();
                                row3["cnte"] = (object)strCnte;
                                dataTable2.Rows.Add(row3);
                            }
                        }
                        this.Session["DETALLE"] = (object)dataTable2;
                        this.grvDetalle.DataSource = (object)(DataTable)this.Session["DETALLE"];
                        this.grvDetalle.DataBind();
                        this.uplDetalle.Update();
                    }
                }
                else
                {
                    DataTable dataTable2 = (DataTable)this.Session["DETALLE"];
                    bool flag = false;
                    DataRow[] dataRowArray = dataTable2.Select("id_producto = '" + prod_clave + "' AND pedido = '" + folio + "'");
                    int index = 0;
                    if (index < dataRowArray.Length)
                    {
                        dataRow1 = dataRowArray[index];
                        flag = true;
                    }
                    if (flag)
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Producto anteriormente seleccionado');", true);
                    }
                    else
                    {
                        dataTable1 = new DataTable();
                        DataTable dataTable3 = this.con.detalle_embarque_detalle(folio, prod_clave);
                        int int32 = Convert.ToInt32(str1);
                        Convert.ToInt32(str2);
                        if (dataTable2.Rows.Count > 0)
                        {
                            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable3.Rows)
                            {
                                DataRow row3 = dataTable2.NewRow();
                                row3["pedido"] = (object)row2["pedido"].ToString();
                                row3["id_producto"] = (object)row2["prod_clave"].ToString();
                                row3["qud_ordprod"] = (object)row2["recibo"].ToString();
                                row3["qud_area"] = (object)row2["qud_area"].ToString();
                                row3["qud_responsable"] = (object)row2["qud_responsable"].ToString();
                                row3["qud_cantrecha"] = (object)"0";
                                row3["qud_cantreci"] = (object)row2["cajas"].ToString();
                                row3["qud_unidad"] = (object)row2["qud_unidad"].ToString();
                                row3["qud_devolucion"] = (object)"";
                                row3["qud_moneda"] = (object)"";
                                row3["prov_clave"] = (object)row2["prov_clave"].ToString();
                                row3["rch_clave"] = (object)row2["rch_clave"].ToString();
                                row3["tbl_clave"] = (object)row2["tbl_clave"].ToString();
                                row3["qud_lote"] = (object)row2["qud_lote"].ToString();
                                row3["nom_producto"] = (object)row2["prod_nombre"].ToString();
                                row3["fecha_cad"] = (object)row2["fec_cad"].ToString();
                                row3["qud_tarima"] = (object)row2["tarima"].ToString();
                                row3["prov_nombre"] = (object)row2["prov_nombre"].ToString();
                                row3["rch_nombre"] = (object)row2["rch_nombre"].ToString();
                                row3["tbl_nombre"] = (object)row2["tbl_nombre"].ToString();
                                row3["tipo"] = (object)row2["tipo_rec"].ToString();
                                row3["vari"] = (object)row2["vari_nombre"].ToString();
                                row3["rech_gral"] = (object)str1;
                                row3["cnte"] = (object)strCnte;
                                dataTable2.Rows.Add(row3);
                            }
                        }
                        else
                        {
                            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable3.Rows)
                            {
                                DataRow row3 = dataTable2.NewRow();
                                row3["pedido"] = (object)row2["pedido"].ToString();
                                row3["id_producto"] = (object)row2["prod_clave"].ToString();
                                row3["qud_ordprod"] = (object)row2["recibo"].ToString();
                                row3["qud_area"] = (object)row2["qud_area"].ToString();
                                row3["qud_responsable"] = (object)row2["qud_responsable"].ToString();
                                row3["qud_cantrecha"] = (object)"0";
                                row3["qud_cantreci"] = (object)row2["cajas"].ToString();
                                row3["qud_unidad"] = (object)row2["qud_unidad"].ToString();
                                row3["qud_devolucion"] = (object)"";
                                row3["qud_moneda"] = (object)"";
                                row3["prov_clave"] = (object)row2["prov_clave"].ToString();
                                row3["rch_clave"] = (object)row2["rch_clave"].ToString();
                                row3["tbl_clave"] = (object)row2["tbl_clave"].ToString();
                                row3["qud_lote"] = (object)row2["qud_lote"].ToString();
                                row3["nom_producto"] = (object)row2["prod_nombre"].ToString();
                                row3["fecha_cad"] = (object)row2["fec_cad"].ToString();
                                row3["qud_tarima"] = (object)row2["tarima"].ToString();
                                row3["prov_nombre"] = (object)row2["prov_nombre"].ToString();
                                row3["rch_nombre"] = (object)row2["rch_nombre"].ToString();
                                row3["tbl_nombre"] = (object)row2["tbl_nombre"].ToString();
                                row3["tipo"] = (object)row2["tipo_rec"].ToString();
                                row3["vari"] = (object)row2["vari_nombre"].ToString();
                                row3["rech_gral"] = (object)str1;
                                row3["cnte"] = (object)strCnte;
                                dataTable2.Rows.Add(row3);
                            }
                        }
                        int num1 = int32;
                        int num2 = 0;
                        int num3 = 0;
                        DataTable dataTable4 = dataTable2;
                        string filterExpression = "id_producto = '" + prod_clave + "' AND pedido = '" + folio.PadLeft(6, '0').ToString() + "'";
                        foreach (DataRow dataRow2 in dataTable4.Select(filterExpression))
                        {
                            num2 += Convert.ToInt32(dataRow2["qud_cantreci"].ToString());
                            int num4 = num1 - Convert.ToInt32(dataRow2["qud_cantreci"].ToString());
                            if (num4 <= 0)
                                ++num3;
                            if (num4 > 0)
                            {
                                dataRow2["qud_cantrecha"] = (object)dataRow2["qud_cantreci"].ToString();
                                num1 = num4;
                            }
                            else if (num3 == 1)
                            {
                                int num5 = num4 + Convert.ToInt32(dataRow2["qud_cantreci"].ToString());
                                dataRow2["qud_cantrecha"] = (object)num5;
                                num1 = num5 - Convert.ToInt32(dataRow2["qud_cantreci"].ToString());
                            }
                            else
                                dataRow2["qud_cantrecha"] = (object)"0";
                        }
                        foreach (DataRow row2 in (InternalDataCollectionBase)dataTable2.Rows)
                        {
                            Decimal num4;
                            if (row2["tipo"].ToString() == "PTP")
                            {
                                string str3 = row2["qud_cantrecha"].ToString();
                                row2["producidas"] = (object)this.con.cajas_producidas_folio2(row2["qud_ordprod"].ToString(), row2["id_producto"].ToString(), row2["qud_tarima"].ToString());
                                DataRow dataRow2 = row2;
                                num4 = Convert.ToDecimal(str3) * new Decimal(100) / Convert.ToDecimal(row2["producidas"].ToString());
                                string str4 = num4.ToString("0.00");
                                dataRow2["porcentaje"] = (object)str4;
                            }
                            if (row2["tipo"].ToString() == "PTC")
                            {
                                string str3 = row2["qud_cantrecha"].ToString();
                                row2["producidas"] = (object)this.con.cajas_producidas_folio(row2["qud_ordprod"].ToString(), row2["id_producto"].ToString());
                                DataRow dataRow2 = row2;
                                num4 = Convert.ToDecimal(str3) * new Decimal(100) / Convert.ToDecimal(row2["producidas"].ToString());
                                string str4 = num4.ToString("0.00");
                                dataRow2["porcentaje"] = (object)str4;
                            }
                        }
                        this.Session["DETALLE"] = (object)dataTable2;
                        this.grvDetalle.DataSource = (object)(DataTable)this.Session["DETALLE"];
                        this.grvDetalle.DataBind();
                        this.uplDetalle.Update();
                    }
                }
            }
        }

        protected void txtRechazadas_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string id = textBox.ID;
            int rowIndex = ((GridViewRow)textBox.Parent.Parent).RowIndex;
            DataTable dataTable = (DataTable)this.Session["DETALLE"];
            if (dataTable.Rows.Count <= 0)
                return;
            decimal validar_valor = 0;
            try
            {
                validar_valor = Convert.ToDecimal(textBox.Text);
                validar_valor = 0;
            }
            catch
            {
                textBox.Text = "0";
            }
            if (textBox.Text == "")
                textBox.Text = "0";
            Decimal num1 = Convert.ToDecimal(dataTable.Rows[rowIndex]["qud_cantreci"].ToString());
            if (Convert.ToDecimal(textBox.Text) > num1)
                textBox.Text = "0";
            Decimal num2 = Convert.ToDecimal(dataTable.Rows[rowIndex]["producidas"].ToString());
            Decimal num3 = Math.Round(Convert.ToDecimal(textBox.Text) * Convert.ToDecimal("100") / num2, 2);
            dataTable.Rows[rowIndex]["porcentaje"] = (object)num3.ToString();
            dataTable.Rows[rowIndex]["qud_cantrecha"] = (object)textBox.Text;
            this.Session["DETALLE"] = (object)dataTable;
            this.grvDetalle.DataSource = (object)(DataTable)this.Session["DETALLE"];
            this.grvDetalle.DataBind();
            this.uplDetalle.Update();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTipo.SelectedValue == "N")
            {
                this.ddlMoneda.SelectedValue = "PESOS";
            }
            
            if (this.ddlTipo.SelectedValue == "E")
            {
                this.ddlMoneda.SelectedValue = "DOLARES";
            }
            this.uplMoneda.Update();

            if (this.ddlTipo.SelectedValue == "N")
            {
                lblCosto.Visible = true;
                txtCosto.Visible = true;
                uplCosto.Update();
                lblSufijo.Visible = false;
                txtSufijo.Visible = false;
                uplSufijo.Update();
                grvFacturas.Visible = false;
                uplFacturas.Update();
            }

            if (this.ddlTipo.SelectedValue == "E")
            {
                lblCosto.Visible = false;
                txtCosto.Visible = false;
                uplCosto.Update();
                lblSufijo.Visible = true;
                txtSufijo.Visible = true;
                uplSufijo.Update();
                grvFacturas.Visible = true;
                uplFacturas.Update();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //this.lblSuccess.Visible = false;
            //this.lblDanger.Visible = false;
            //this.lblWarning.Visible = false;
            //this.lblCambio.Visible = false;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.lblVerifica.Text == "1")
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('La queja ya fue registrada');", true);
            else if (this.ddlPlacas.SelectedIndex <= 0)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No se ha seleccionado el transporte');", true);
            }
            else
            {
                

                this.enviarcorreo("aescamilla@mrlucky.com.mx", lblNombre.Text, txtFolio.Text, "El usuario oprimio el boton guardar");

                string text1 = this.txtFolio.Text;
                string text2 = this.txtSemana.Text;
                string text3 = this.txtFecha.Text;
                string text4 = this.txtMes.Text;
                string selectedValue1 = this.ddlCliente.SelectedValue;
                string text5 = this.ddlCliente.SelectedItem.Text;
                string selectedValue2 = this.ddlSucursales.SelectedValue;
                string text6 = this.ddlSucursales.SelectedItem.Text;
                string upper = this.txtReporto.Text.ToUpper().Replace("'", " ");
                string text7 = this.lblClave.Text;
                string text8 = this.lblCedis.Text;
                string text9 = this.lblClave.Text;
                string selectedValue3 = this.ddlTipo.SelectedValue;

                string textFact = this.txtFactura.Text;
                string textSufijo = this.txtSufijo.Text.ToUpper();

                //DateTime dateTime = Convert.ToDateTime(this.txtFecha.Text);
                //dateTime = dateTime.AddDays(1);
                //string shortDateString = dateTime.ToString("dd/MM/yyyy");

                string f_1 = DateTime.Now.ToString();
                string shortDateString = Convert.ToDateTime(f_1).AddDays(1).ToString("dd/MM/yyyy");

                string str1 = this.ddlProblema.SelectedItem.Value;
                string str2 = this.chkDevolucion.Checked ? "1" : "0";
                string selectedValue4 = this.ddlMoneda.SelectedValue;
                string text10 = Convert.ToDecimal(this.txtCosto.Text).ToString();
                string text11 = this.txtObservaciones.Text;
                string text12 = this.txtEmbarques.Text;
                string text13 = this.txtTransporte.Text;

                DataTable dataTable = (DataTable)this.Session["DETALLE"];
                string text14 = this.lblNombre.Text;

                string strM = this.chkMerma.Checked ? "1" : "0";

                string textDev = (rbtList.SelectedValue == "DEV") ? "1" : "0";
                string textMer = (rbtList.SelectedValue == "MER") ? "1" : "0";
                string textBon = (rbtList.SelectedValue == "BON") ? "1" : "0";
                string textNA = (rbtList.SelectedValue == "NA") ? "1" : "0";

                str2 = textDev;
                strM = textMer;

                string textFechaEmb = txtFechaEmb.Text;
                string textSemEmb = txtSemEmb.Text;

                //validar seleccion de dato de devolucion, merma y bonificacion
                if (textDev == "0" && textMer == "0" && textBon == "0" && textNA == "0")
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione si es Devolucion, Bonificacion, Merma o si no aplica');", true);
                    return;
                }

                if (selectedValue1 == "GAB2")
                {
                    //if (txtSufijo.Text == "")
                    //{
                    //    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe ingresar el sufijo para la(s) factura(s)');", true);
                    //    return;
                    //}
                }

                if (dataTable.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No se han seleccionado productos');", true);
                    return;
                }
                else
                {
                    string cve_queja = this.con.insert_queja_exportacion(text1, text2, text3, text4, selectedValue1, text5, text6, upper, text7, text8, text9, selectedValue3, shortDateString, "", 
                        selectedValue2, text10, text12, text13, text11, text14, textFact, textSufijo, textFechaEmb, textSemEmb);
                    SqlConnection connection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
                    connection.Open();
                    string str3 = "0";
                    string str4 = "<table border='1'>";
                    bool flag = false;
                    int i = 0;
                    string str5 = "INSERT INTO tb_det_quejas(que_folio, id_producto, qud_problema, qud_ordprod, qud_area, qud_responsable, qud_cantrecha, qud_cantreci, qud_unidad, qud_devolucion, qud_moneda, prov_clave, rch_clave, tbl_clave, qud_variedad, qud_lote, nom_producto, fecha_cad, qud_causa, qud_tarima, qud_ptcptp, qud_cjsprod, qud_porcen, qud_merma, qud_cnte, qud_pedido, qud_bonificacion, qud_rechazado, qud_noaplica, conse) VALUES ";
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        if (Convert.ToInt32(row["qud_cantrecha"].ToString()) != 0)
                        {
                            i++;
                            row["id_producto"].ToString();
                            row["qud_ordprod"].ToString();
                            row["qud_area"].ToString();
                            row["qud_responsable"].ToString();
                            row["qud_cantrecha"].ToString();
                            row["qud_cantreci"].ToString();
                            row["qud_unidad"].ToString();
                            row["prov_clave"].ToString();
                            row["rch_clave"].ToString();
                            row["tbl_clave"].ToString();
                            row["qud_lote"].ToString();
                            row["nom_producto"].ToString();
                            row["fecha_cad"].ToString();
                            row["qud_tarima"].ToString();
                            row["vari"].ToString();
                            row["tipo"].ToString();
                            row["pedido"].ToString();
                            row["cnte"].ToString();
                            string str6 = row["producidas"].ToString();
                            string str7 = row["porcentaje"].ToString();
                            string str8p = row["pedido"].ToString();
                            string str9c = row["cnte"].ToString();
                            this.Session["id_producto"] = (object)row["id_producto"].ToString();
                            this.Session["qud_ordprod"] = (object)row["qud_ordprod"].ToString();
                            this.Session["qud_area"] = (object)row["qud_area"].ToString();
                            this.Session["qud_responsable"] = (object)row["qud_responsable"].ToString();
                            this.Session["qud_cantrecha"] = (object)row["qud_cantrecha"].ToString();
                            this.Session["qud_cantreci"] = (object)row["qud_cantreci"].ToString();
                            this.Session["qud_unidad"] = (object)row["qud_unidad"].ToString();
                            this.Session["prov_clave"] = (object)row["prov_clave"].ToString();
                            this.Session["rch_clave"] = (object)row["rch_clave"].ToString();
                            this.Session["tbl_clave"] = (object)row["tbl_clave"].ToString();
                            this.Session["qud_lote"] = (object)row["qud_lote"].ToString();
                            this.Session["nom_producto"] = (object)row["nom_producto"].ToString();
                            this.Session["fecha_cad"] = (object)row["fecha_cad"].ToString();
                            this.Session["qud_tarima"] = (object)row["qud_tarima"].ToString();
                            this.Session["vari"] = (object)row["vari"].ToString();
                            this.Session["tipo_1"] = (object)row["tipo"].ToString();
                            this.Session["problema"] = (object)str1;
                            this.Session["producidas"] = (object)str6;
                            this.Session["porcentaje"] = (object)str7;
                            this.Session["pedido"] = (object)str8p;
                            this.Session["cnte"] = (object)str9c;
                            str5 = str5 + "('" + cve_queja + "', '" + this.Session["id_producto"] + "', '" + this.Session["problema"] + "', '" + this.Session["qud_ordprod"] + "', '" + this.Session["qud_area"] + "', " +
                                "'" + this.Session["qud_responsable"] + "', '" + this.Session["qud_cantrecha"] + "', '" + this.Session["qud_cantreci"] + "', '" + this.Session["qud_unidad"] + "', " +
                                "'" + str2 + "', '" + selectedValue4 + "', '" + this.Session["prov_clave"] + "', '" + this.Session["rch_clave"] + "', '" + this.Session["tbl_clave"] + "', " +
                                "'" + this.Session["vari"] + "', '" + this.Session["qud_lote"] + "', '" + this.Session["nom_producto"].ToString().Replace("'", " ").ToString() + "', " +
                                "'" + this.Session["fecha_cad"] + "', '', '" + this.Session["qud_tarima"] + "', '" + this.Session["tipo_1"] + "', '" + this.Session["producidas"] + "', " +
                                "'" + this.Session["porcentaje"] + "', '" + strM + "', '" + this.Session["cnte"] + "', '" + this.Session["pedido"] + "', '" + textBon + "', " +
                                "'" + this.Session["qud_cantrecha"] + "', '" + textNA + "', '" + i + "') ,";
                            str4 = str4 + "<tr><td>" + this.Session["id_producto"] + "</td><td>" + this.Session["qud_ordprod"] + "</td><td>" + this.Session["qud_area"] + "</td>" +
                                "<td>" + this.Session["qud_responsable"] + "</td><td>" + this.Session["qud_cantrecha"] + "</td>" +
                                "<td>" + this.Session["qud_cantreci"] + "</td><td>" + this.Session["qud_unidad"] + "</td><td>" + this.Session["prov_clave"] + "</td>" +
                                "<td>" + this.Session["rch_clave"] + "</td><td>" + this.Session["tbl_clave"] + "</td><td>" + this.Session["qud_lote"] + "</td>" +
                                "<td>" + this.Session["nom_producto"] + "</td><td>" + this.Session["fecha_cad"] + "</td><td>" + this.Session["qud_tarima"] + "</td>" +
                                "<td>" + this.Session["vari"] + "</td><td>" + this.Session["tipo_1"] + "</td><td>" + this.Session["producidas"] + "</td>" +
                                "<td>" + this.Session["porcentaje"] + "</td><td>" + strM + "</td><td>" + this.Session["cnte"] + "</td>" +
                                "<td>" + this.Session["pedido"] + "</td></tr>";
                        }
                    }

                    //GUARDADO DE GRID DE FACTURAS
                    if (selectedValue3 == "E")
                    {
                        foreach (GridViewRow gr in grvFacturas.Rows)
                        {
                            string val1 = gr.Cells[0].Text;
                            TextBox control = (TextBox)gr.Cells[1].FindControl("txtCostoFact");
                            string val2 = control.Text;
                            string valor = "";
                            if (Convert.ToDecimal(val2) > 0)
                                valor = con.agrega_detalle_costo_factura(cve_queja, val1, Convert.ToDecimal(val2).ToString());
                        }
                    }
                    
                    string str8;
                    try
                    {
                        flag = true;
                        string str6 = str5.TrimEnd(' ').TrimEnd(',');
                        string str7 = "SELECT DEST.[text] AS [full statement code] FROM sys.[dm_exec_requests] SDER CROSS APPLY sys.[dm_exec_sql_text](SDER.[sql_handle]) DEST WHERE SDER.session_id > 50 ORDER BY SDER.[session_id], SDER.[request_id]";
                        SqlCommand sqlCommand = new SqlCommand(str6 + " " + str7, connection);
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                                str7 = str7 + "/CONSULTA: " + sqlDataReader["full statement code"].ToString() + "/";
                        }
                        sqlDataReader.Close();
                        sqlDataReader.Dispose();
                        string str9 = str7.TrimEnd('/');
                        sqlCommand.Dispose();
                        str8 = str6 + "*/**" + str9;
                    }
                    catch (SqlException ex)
                    {
                        str8 = str3 + " - NO SE EJECUTO: " + ex.Message.ToString();
                    }
                    string str10 = str4 + "<tr><td colspan='18'>" + str8 + "</td></tr>";
                    connection.Close();
                    string cuerpo1 = str10 + "</table>";

                    


                    if (!flag)
                        return;
                    this.lblSuccess.Visible = true;
                    this.btnSubir.Enabled = true;
                    this.upnBotones.Update();
                    string str11 = "http://189.206.160.206:81/quejas/";
                    string str12 = "http://gabira1:81/quejas/";
                    string cuerpo2 = "<table border='2'><tr><td align='center'><h2>Registro de Queja</h2></td></tr><tr><td>Registro de queja realizado por: " + this.lblNombre.Text + "</td></tr><tr><td>CEDIS: " + this.lblCedis.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja: No. " + cve_queja + "</td></tr><tr><td>Embarque: " + this.txtEmbarque.Text + "</td></tr><tr><td>Problema: " + this.ddlProblema.SelectedItem.ToString() + "</td></tr><tr><td>Cliente: " + this.ddlCliente.SelectedItem.ToString() + "</td></tr></table><br /><p><h4>Ir a la seccion de Editar Queja para asignaci&oacute; de &Aacute;rea</h4></p><br /><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str12 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str11;
                    this.enviarcorreo("msamano@mrlucky.com.mx", text9, cve_queja, cuerpo2);
                    //this.enviarcorreo("aescamilla@mrlucky.com.mx", text9, cve_queja, cuerpo2);
                    this.enviarcorreo_error("aescamilla@mrlucky.com.mx", cve_queja, cuerpo1);
                    if (cve_queja != "" && this.txtFolio.Text != cve_queja)
                    {
                        this.lblCambio.Visible = true;
                        this.txtFolio.Text = cve_queja;
                        this.uplFolio.Update();
                    }
                    this.lblVerifica.Text = "1";
                    this.upnVerifica.Update();
                    this.Session["id_producto"] = (object)"";
                    this.Session["qud_ordprod"] = (object)"";
                    this.Session["qud_area"] = (object)"";
                    this.Session["qud_responsable"] = (object)"";
                    this.Session["qud_cantrecha"] = (object)"";
                    this.Session["qud_cantreci"] = (object)"";
                    this.Session["qud_unidad"] = (object)"";
                    this.Session["prov_clave"] = (object)"";
                    this.Session["rch_clave"] = (object)"";
                    this.Session["tbl_clave"] = (object)"";
                    this.Session["qud_lote"] = (object)"";
                    this.Session["nom_producto"] = (object)"";
                    this.Session["fecha_cad"] = (object)"";
                    this.Session["qud_tarima"] = (object)"";
                    this.Session["vari"] = (object)"";
                    this.Session["tipo_1"] = (object)"";
                    this.Session["problema"] = (object)"";
                    this.Session["producidas"] = (object)"";
                    this.Session["porcentaje"] = (object)"";
                    this.Session["pedido"] = (object)"";
                    this.Session["cnte"] = (object)"";
                }
            }
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

        protected void grvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "borrar"))
                return;
            GridViewRow row = this.grvDetalle.Rows[Convert.ToInt32(e.CommandArgument)];
            string str1 = this.Server.HtmlDecode(row.Cells[1].Text);
            string str2 = this.Server.HtmlDecode(row.Cells[0].Text);
            DataTable dataTable1 = (DataTable)this.Session["DETALLE"];
            DataTable dataTable2 = dataTable1;
            string filterExpression = "pedido = '" + str2 + "' AND id_producto = '" + str1 + "'";
            foreach (DataRow dataRow in dataTable2.Select(filterExpression))
                dataRow.Delete();
            this.Session["DETALLE"] = (object)dataTable1;
            this.grvDetalle.DataSource = (object)(DataTable)this.Session["DETALLE"];
            this.grvDetalle.DataBind();
            this.uplDetalle.Update();
        }

        protected void btnTodos_Click(object sender, EventArgs e)
        {
            DataTable dataTable1 = (DataTable)this.Session["DETALLE"];
            this.txtEmbarque.Text = this.ddlPlacas.SelectedValue.ToString();
            this.txtTransporte.Text = this.ddlPlacas.SelectedItem.ToString();
            this.dtEmbarques = this.con.detalle_embarque_concentrado(this.txtEmbarque.Text, this.txtTransporte.Text.Split('-')[0].ToString().Trim(), this.txtFechaCarga.Text, this.ddlTipo.SelectedValue.ToString(), this.txtTransporte.Text.Split('-')[2].ToString().Trim());
            int count1 = this.gvwQuejas.Rows.Count;
            int count2 = this.gvwQuejas.Rows.Count;
            foreach (GridViewRow row in this.gvwQuejas.Rows)
            {
                string text1 = row.Cells[4].Text;
                string text2 = row.Cells[3].Text;
                if (row.Cells[3].Text == "0" || row.Cells[3].Text == "")
                    --count2;
                TextBox control = (TextBox)this.gvwQuejas.Rows[row.RowIndex].Cells[3].FindControl("txtRechazadas2");
                //if (control == null)
                //{
                //    --count2;
                //}
                //else
                //{
                    if (control.Text == "0" || control.Text == "")
                        --count2;
                //}
                    
                
            }
            if (count2 < count1)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Para cargar todos, ingrese la cantidad rechazada a cada uno de los productos');", true);
            }
            else
            {
                foreach (DataRow row1 in (InternalDataCollectionBase)this.dtEmbarques.Rows)
                {
                    DataTable dataTable2 = new DataTable();
                    foreach (DataRow row2 in (InternalDataCollectionBase)this.con.detalle_embarque_detalle(row1["pdn_folio"].ToString(), row1["prod_clave"].ToString()).Rows)
                    {
                        DataRow row3 = dataTable1.NewRow();
                        row3["pedido"] = (object)row2["pedido"].ToString();
                        row3["id_producto"] = (object)row2["prod_clave"].ToString();
                        row3["qud_ordprod"] = (object)row2["recibo"].ToString();
                        row3["qud_area"] = (object)row2["qud_area"].ToString();
                        row3["qud_responsable"] = (object)row2["qud_responsable"].ToString();
                        row3["qud_cantrecha"] = (object)row2["cajas"].ToString();
                        row3["qud_cantreci"] = (object)row2["cajas"].ToString();
                        row3["qud_unidad"] = (object)row2["qud_unidad"].ToString();
                        row3["qud_devolucion"] = (object)"";
                        row3["qud_moneda"] = (object)"";
                        row3["prov_clave"] = (object)row2["prov_clave"].ToString();
                        row3["rch_clave"] = (object)row2["rch_clave"].ToString();
                        row3["tbl_clave"] = (object)row2["tbl_clave"].ToString();
                        row3["qud_lote"] = (object)row2["qud_lote"].ToString();
                        row3["nom_producto"] = (object)row2["prod_nombre"].ToString();
                        row3["fecha_cad"] = (object)row2["fec_cad"].ToString();
                        row3["qud_tarima"] = (object)row2["tarima"].ToString();
                        row3["prov_nombre"] = (object)row2["prov_nombre"].ToString();
                        row3["rch_nombre"] = (object)row2["rch_nombre"].ToString();
                        row3["tbl_nombre"] = (object)row2["tbl_nombre"].ToString();
                        row3["tipo"] = (object)row2["tipo_rec"].ToString();
                        row3["cnte"] = (object)row1["cnte"].ToString();
                        dataTable1.Rows.Add(row3);
                    }
                }
                foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
                {
                    Decimal num;
                    if (row["tipo"].ToString() == "PTP")
                    {
                        string str1 = row["qud_cantrecha"].ToString();
                        row["producidas"] = (object)this.con.cajas_producidas_folio2(row["qud_ordprod"].ToString(), row["id_producto"].ToString(), row["qud_tarima"].ToString());
                        DataRow dataRow = row;
                        num = Convert.ToDecimal(str1) * new Decimal(100) / Convert.ToDecimal(row["producidas"].ToString());
                        string str2 = num.ToString("0.00");
                        dataRow["porcentaje"] = (object)str2;
                    }
                    if (row["tipo"].ToString() == "PTC")
                    {
                        string str1 = row["qud_cantrecha"].ToString();
                        row["producidas"] = (object)this.con.cajas_producidas_folio(row["qud_ordprod"].ToString(), row["id_producto"].ToString());
                        DataRow dataRow = row;
                        num = Convert.ToDecimal(str1) * new Decimal(100) / Convert.ToDecimal(row["producidas"].ToString());
                        string str2 = num.ToString("0.00");
                        dataRow["porcentaje"] = (object)str2;
                    }
                }
                this.Session["DETALLE"] = (object)dataTable1;
                this.grvDetalle.DataSource = (object)(DataTable)this.Session["DETALLE"];
                this.grvDetalle.DataBind();
                this.uplDetalle.Update();
            }
        }

        protected void TxtId_TextChanged(object sender, EventArgs e)
        {
            e.ToString();
            GridViewRow row = this.grvDetalle.Rows[((GridViewRow)((Control)sender).Parent.Parent).RowIndex];
            TextBox control = (TextBox)row.Cells[16].FindControl("txtRechazadas");
            string str1 = this.Server.HtmlDecode(row.Cells[0].Text);
            string str2 = this.Server.HtmlDecode(row.Cells[1].Text);
            string str3 = this.Server.HtmlDecode(row.Cells[3].Text);
            string str4 = this.Server.HtmlDecode(row.Cells[4].Text);
            string str5 = this.Server.HtmlDecode(row.Cells[15].Text);
            string str6 = this.Server.HtmlDecode(control.Text);
            if (Convert.ToInt32(str6) > Convert.ToInt32(str5))
                str6 = str5;
            DataTable dataTable1 = (DataTable)this.Session["DETALLE"];
            DataTable dataTable2 = dataTable1;
            string filterExpression = "pedido = '" + str1 + "' AND id_producto = '" + str2 + "' AND qud_tarima = '" + str4 + "' AND qud_ordprod = '" + str3 + "'";
            foreach (DataRow dataRow in dataTable2.Select(filterExpression))
                dataRow["qud_cantrecha"] = (object)str6;
            this.Session["DETALLE"] = (object)dataTable1;
            this.grvDetalle.DataSource = (object)(DataTable)this.Session["DETALLE"];
            this.grvDetalle.DataBind();
            this.uplDetalle.Update();
        }

        public void enviarcorreo_error(string correo, string cve_queja, string cuerpo)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add(correo);
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

        //<div class="form-group">
        //                                <div class="col-sm-12">
        //                                    <asp:UpdatePanel ID="updatePanelNC" runat="server">
        //                                        <ContentTemplate>
        //                                            <asp:Label ID="Label4" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
        //                                                Text="Nota de Credito Pendiente" runat="server" Visible = "false" />
        //                                        </ContentTemplate>
        //                                    </asp:UpdatePanel>
        //                                </div>
        //                            </div>
    }
}