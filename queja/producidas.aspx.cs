using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Drawing;

namespace queja
{
    public partial class producidas : System.Web.UI.Page
    {
        private DataTable dtVariedades = new DataTable();
        private DataTable dtPrincipal = new DataTable();
        private DataTable dtPrincipal2 = new DataTable();
        private DataTable dtPrincipal3 = new DataTable();
        private DataTable dtRecibos = new DataTable();

        conectasql clase = new conectasql();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();

            dtVariedades.Columns.Add("variedad", typeof(string));

            dtRecibos.Columns.Add("recibos", typeof(string));

            dtPrincipal.Columns.Add("variedad", typeof(string));
            dtPrincipal.Columns.Add("rancho", typeof(string));
            dtPrincipal.Columns.Add("tabla", typeof(string));
            dtPrincipal.Columns.Add("producido", typeof(decimal));
            dtPrincipal.Columns.Add("problema", typeof(string));
            dtPrincipal.Columns.Add("rechazado", typeof(decimal));
            dtPrincipal.Columns.Add("porcentaje", typeof(string));

            dtPrincipal3.Columns.Add("variedad", typeof(string));
            dtPrincipal3.Columns.Add("rancho", typeof(string));
            dtPrincipal3.Columns.Add("tabla", typeof(string));
            dtPrincipal3.Columns.Add("producido", typeof(string));
            dtPrincipal3.Columns.Add("problema", typeof(string));
            dtPrincipal3.Columns.Add("rechazado", typeof(string));
            dtPrincipal3.Columns.Add("porcentaje", typeof(string));
            dtPrincipal3.Columns.Add("porc_var", typeof(string));

            dtPrincipal2.Columns.Add("variedad", typeof(string));
            dtPrincipal2.Columns.Add("rancho", typeof(string));
            dtPrincipal2.Columns.Add("tabla", typeof(string));
            dtPrincipal2.Columns.Add("producido", typeof(decimal));
            dtPrincipal2.Columns.Add("problema", typeof(string));
            dtPrincipal2.Columns.Add("rechazado", typeof(decimal));

            ListItem concepto;

            if (!Page.IsPostBack)
            {
                SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
                conn.Open();
                SqlCommand cmnd;
                string qry = "select lin_clave, lin_nombre FROM tb_cat_linea where lin_clave >= '01' AND lin_clave <= '23' ORDER BY lin_nombre";
                cmnd = conn.CreateCommand();
                cmnd.CommandText = qry;
                SqlDataReader reader = cmnd.ExecuteReader();
                concepto = new ListItem("SELECCIONAR LINEA...", "0");
                ddlQuejas.Items.Add(concepto);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        concepto = new ListItem(reader["lin_nombre"].ToString().Trim(), reader["lin_clave"].ToString().Trim());
                        ddlQuejas.Items.Add(concepto);
                    }
                }
                conn.Close();


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            reporte_tablas_2();
            //reporte_tablas_3();
            ////ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loadMe", "$('#loadMe').modal();", true);

            //SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            //SqlDataAdapter adapter;
            //DataSet dsDatos = new DataSet();

            //string query = "";
            //conn.Open();

            //string fi = "";
            //string ff = "";

            //fi = txtFechaInicio.Text;
            //ff = txtFechaFin.Text;

            //string LINEA = ddlQuejas.SelectedValue.ToString();

            //if (fi == "" || ff == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Falta ingresar una fecha');", true);
            //    return;
            //}

            //////query = "SELECT a.prod_clave, a.hrp_recibo, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            //////    "FROM tb_hist_recepcion a " +
            //////    "join tb_det_recepcion_pt d on d.rpt_recibo = a.hrp_recibo and d.lin_clave = a.lin_clave and d.prod_clave = a.prod_clave " +
            //////    "left join tb_cat_variedad e on e.vari_clave = d.variedad and e.lin_clave = d.lin_clave " +
            //////    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            //////    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' " +
            //////    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave in ('16') " +
            //////    "group by a.prod_clave, a.hrp_recibo, e.vari_nombre";
            ////query = "SELECT a.hrp_recibo, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            ////    "FROM tb_hist_recepcion a " +
            ////    "join tb_mstr_recepcion_pt F ON F.RPT_RECIBO = A.HRP_RECIBO AND F.LIN_CLAVE = A.LIN_CLAVE " +
            ////    "left join tb_cat_variedad e on e.vari_clave = f.vari_clave and e.lin_clave = F.lin_clave " +
            ////    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            ////    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' /*AND e.vari_nombre = 'TLALOC'*/ " +
            ////    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave = '" + LINEA + "' " +
            ////    "group by a.prod_clave, a.hrp_recibo, e.vari_nombre";
            //query = "SELECT a.hrp_recibo, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            //    "FROM tb_hist_recepcion a " +
            //    "join tb_mstr_recepcion_pt F ON F.RPT_RECIBO = A.HRP_RECIBO AND F.LIN_CLAVE = A.LIN_CLAVE " +
            //    "left join tb_cat_variedad e on e.vari_clave = f.vari_clave and e.lin_clave = F.lin_clave " +
            //    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            //    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' /*AND e.vari_nombre = 'TLALOC'*/ " +
            //    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave = '" + LINEA + "' " +
            //    "group by a.prod_clave, a.hrp_recibo, e.vari_nombre";
            //////"WHERE a.hrp_fecha >= '27/04/2020' AND a.hrp_fecha <= '15/10/2020' " +
            //adapter = new SqlDataAdapter(query, conn);
            //adapter.Fill(dsDatos, "historico");

            ////////query = "SELECT a.prod_clave, RTRIM(b.prod_nombre) AS prod_nombre, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            ////////    "FROM tb_hist_recepcion a " +
            ////////    "join tb_det_recepcion_pt d on d.rpt_recibo = a.hrp_recibo and d.lin_clave = a.lin_clave and d.prod_clave = a.prod_clave " +
            ////////    "left join tb_cat_variedad e on e.vari_clave = d.variedad and e.lin_clave = d.lin_clave " +
            ////////    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            ////////    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' " +
            ////////    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave in ('02') " +
            ////////    "group by a.prod_clave, prod_nombre, e.vari_nombre";
            ////////"WHERE a.hrp_fecha >= '27/04/2020' AND a.hrp_fecha <= '15/10/2020' " +
            //////query = "SELECT a.prod_clave, RTRIM(b.prod_nombre) AS prod_nombre, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            //////    "FROM tb_hist_recepcion a " +
            //////    "join tb_mstr_recepcion_pt F ON F.RPT_RECIBO = A.HRP_RECIBO AND F.LIN_CLAVE = A.LIN_CLAVE " +
            //////    "left join tb_cat_variedad e on e.vari_clave = f.vari_clave and e.lin_clave = F.lin_clave " +
            //////    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            //////    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' /*AND e.vari_nombre = 'TLALOC'*/ " +
            //////    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave = '" + LINEA + "' " +
            //////    "group by a.prod_clave, prod_nombre, e.vari_nombre";
            //////adapter = new SqlDataAdapter(query, conn);
            //////adapter.Fill(dsDatos, "historico2");

            //////query = "select a.id_producto, a.nom_producto, c.pro_nombre, (CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS vari_nombre, " +
            //////    "SUM(a.qud_cantrecha) AS qud_cantrecha " +
            //////    "FROM tb_det_quejas a " +
            //////    "JOIN tb_mstr_quejas d ON d.que_folio = a.que_folio " +
            //////    "join tb_mstr_recepcion_pt b on a.qud_ordprod = b.rpt_recibo " +
            //////    "JOIN tb_cat_problemas c ON c.pro_clave = a.qud_problema " +
            //////    "where b.rpt_fecha >= '" + fi + "' and b.rpt_fecha <= '" + ff + "' /*AND A.qud_variedad = 'TLALOC'*/ and b.rpt_estatus <> 'F' and d.que_status in ('A', 'T') " +
            //////    "GROUP BY a.id_producto, a.nom_producto, c.pro_nombre, a.qud_variedad " +
            //////    "order by a.nom_producto";
            //////adapter = new SqlDataAdapter(query, conn);
            //////adapter.Fill(dsDatos, "historico3");

            //////clase.correo_error(query);


            //Session["historico"] = dsDatos.Tables["historico"];
            ////Session["historico2"] = dsDatos.Tables["historico2"];
            ////Session["historico3"] = dsDatos.Tables["historico3"];

            //var x = (from r in dsDatos.Tables["historico"].AsEnumerable() select r["vari_nombre"]).Distinct().ToList();

            //DataTable uniqueRows = dsDatos.Tables["historico"].DefaultView.ToTable(true, "hrp_recibo", "vari_nombre");

            //DataRow rw;

            ////Conncentrado de variedades
            //foreach (var vari in x)
            //{
            //    rw = dtVariedades.NewRow();
            //    rw["variedad"] = vari;
            //    dtVariedades.Rows.Add(rw);
            //}

            ////foreach (var vari in y)
            ////{
            ////    rw = dtRecibos.NewRow();
            ////    rw["recibos"] = vari;
            ////    dtRecibos.Rows.Add(rw);
            ////}


            //foreach (var vari in x)
            //{
            //    rw = dtPrincipal.NewRow();
            //    rw["variedad"] = vari;
            //    rw["producido"] = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
            //    dtPrincipal.Rows.Add(rw);

            //    //foreach (DataRow ri in dsDatos.Tables["historico"].Select("vari_nombre = '" + vari.ToString() + "'"))
            //    foreach (DataRow ri in uniqueRows.Select("vari_nombre = '" + vari.ToString() + "'"))
            //    {
            //        if (ri["hrp_recibo"].ToString() == "266317" || ri["hrp_recibo"].ToString() == "266775")
            //        {
            //        }
            //        if (vari.ToString() == "SIN VARIEDAD")
            //        {
            //            query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
            //                "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
            //                "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
            //                "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
            //                "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
            //                "AND C.que_fecha >= '" + txtFechaInicio.Text + "' " +
            //                "GROUP BY B.pro_nombre, A.qud_variedad " +
            //                "ORDER BY A.qud_variedad";
            //            adapter = new SqlDataAdapter(query, conn);

            //            //Limpiar datatable
            //            foreach (DataTable dt in dsDatos.Tables)
            //            {
            //                if (dt.TableName == "quejas")
            //                {
            //                    dsDatos.Tables["quejas"].Clear();
            //                }
            //            }

            //            //llenar datatable
            //            adapter.Fill(dsDatos, "quejas");

            //            //recorrer datatable filtrar por variedad y problema para sumar el rechazo
            //            foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
            //            {
            //                bool fnd = false;
            //                foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
            //                {
            //                    //si encontro sumar valor de rechazo
            //                    //clase.correo_error(query);
            //                    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
            //                    fnd = true;
            //                }
            //                if (fnd == false)
            //                {
            //                    //si no encuentra agrega el renglon con el nuevo problema
            //                    rw = dtPrincipal.NewRow();
            //                    rw["variedad"] = vari.ToString();
            //                    rw["problema"] = re["pro_nombre"].ToString();
            //                    rw["rechazado"] = re["qud_cantrecha"].ToString();
            //                    dtPrincipal.Rows.Add(rw);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
            //                "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
            //                "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
            //                "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
            //                "WHERE A.qud_variedad = '" + vari.ToString() + "' AND A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
            //                "AND C.que_fecha >= '" + txtFechaInicio.Text + "' " +
            //                "GROUP BY B.pro_nombre, A.qud_variedad " +
            //                "ORDER BY A.qud_variedad";
            //            adapter = new SqlDataAdapter(query, conn);

            //            //Limpiar datatable
            //            foreach (DataTable dt in dsDatos.Tables)
            //            {
            //                if (dt.TableName == "quejas")
            //                {
            //                    dsDatos.Tables["quejas"].Clear();
            //                }
            //            }

            //            //llenar datatable
            //            adapter.Fill(dsDatos, "quejas");

            //            //recorrer datatable filtrar por variedad y problema para sumar el rechazo
            //            foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
            //            {
            //                //conectasql clase = new conectasql();
            //                //clase.correo_error(query);
            //                bool fnd = false;
            //                foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
            //                {
            //                    //si encontro sumar valor de rechazo
            //                    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
            //                    fnd = true;
            //                }
            //                if (fnd == false)
            //                {
            //                    //si no encuentra agrega el renglon con el nuevo problema
            //                    rw = dtPrincipal.NewRow();
            //                    rw["variedad"] = vari.ToString();
            //                    rw["problema"] = re["pro_nombre"].ToString();
            //                    rw["rechazado"] = re["qud_cantrecha"].ToString();
            //                    dtPrincipal.Rows.Add(rw);
            //                }
            //            }
            //        }
            //    }
            //}
            //conn.Close();

            //foreach (DataRow rt in dtPrincipal.Select("variedad <> '' AND producido > 0"))
            //{
            //    rt["rechazado"] = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") == rt["variedad"].ToString() && r.Field<string>("problema") != "").Sum(r => r.Field<decimal?>("rechazado"))).ToString();
            //    rt["porcentaje"] = ((Convert.ToDecimal(rt["rechazado"]) * Convert.ToDecimal("100")) / Convert.ToDecimal(rt["producido"])).ToString("##.00");
            //}

            //string html = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive w-auto small'>" +
            //    "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>Variedad</font></th><th bgcolor='#337ab7'><font color='#fff'>Recibido</font></th>" +
            //    "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th><th bgcolor='#337ab7'><font color='#fff'>Rechazado</font></th>" +
            //    "<th bgcolor='#337ab7'><font color='#fff'>Porcentaje</font></th></tr></thead><tbody>";

            ////Totales generales
            //string total_producido = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && r.Field<decimal?>("producido") > 0).Sum(r => r.Field<decimal?>("producido"))).ToString();
            //string total_rechazado = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && r.Field<decimal?>("producido") > 0).Sum(r => r.Field<decimal?>("rechazado"))).ToString();
            ////(dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && r.Field<string>("problema") != "").Sum(r => r.Field<decimal?>("rechazado"))).ToString();
            //string total_porcentaje = "";
            //if (Convert.ToDecimal(total_producido) == 0)
            //{
            //    total_porcentaje = Convert.ToDecimal(total_producido).ToString("###.00");
            //}
            //else
            //{
            //    total_porcentaje = ((Convert.ToDecimal(total_rechazado) * Convert.ToDecimal("100")) / Convert.ToDecimal(total_producido)).ToString("###.00");
            //}

            //html += "<tr><td><b>Totales Generales</b></td><td align='right'><b>" + Convert.ToDecimal(total_producido).ToString("###,###,##0") + "</b></td><td></td><td align='right'><b>" + Convert.ToDecimal(total_rechazado).ToString("###,###,##0") + "</b></td><td align='right'><b>" + total_porcentaje + " %</b></td></tr>";

            //string var_act = "";
            //string var_ant = "";
            //foreach (DataRow ra in dtPrincipal.Rows)
            //{
            //    var_act = ra["variedad"].ToString();
            //    if (var_act != var_ant)
            //        html += "<tr><td><b><a onclick='recibido_variedad(\"" + ra["variedad"] + "\")' style='color:black'>" + ra["variedad"] + "</a></b></td><td align='right'><b>" + Convert.ToDecimal(ra["producido"].ToString()).ToString("###,###,##0") + "</b></td><td></td><td align='right'><b>" + Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") + "</b></td><td align='right'><b>" + Convert.ToDecimal(ra["porcentaje"].ToString()).ToString("##0.00") + " %</b></td></tr>";
            //    else
            //        html += "<tr><td></td><td></td><td><a onclick='rechazado_problema(\"" + ra["variedad"] + "\", \"" + ra["problema"] + "\")' style='color:black'>" + ra["problema"] + "</a></td><td align='right'>" + Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") + "</td><td></td></tr>";
            //    var_ant = var_act;
            //}

            //html += "</tbody></table>";

            //this.datosqueja.InnerHtml = html;
        }

        public void reporte_tablas()
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loadMe", "$('#loadMe').modal();", true);

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            SqlDataAdapter adapter;
            DataSet dsDatos = new DataSet();

            string query = "";
            conn.Open();

            string fi = "";
            string ff = "";

            fi = txtFechaInicio.Text;
            ff = txtFechaFin.Text;

            string LINEA = ddlQuejas.SelectedValue.ToString();

            if (fi == "" || ff == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Falta ingresar una fecha');", true);
                return;
            }

            ////query = "SELECT a.prod_clave, a.hrp_recibo, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            ////    "FROM tb_hist_recepcion a " +
            ////    "join tb_det_recepcion_pt d on d.rpt_recibo = a.hrp_recibo and d.lin_clave = a.lin_clave and d.prod_clave = a.prod_clave " +
            ////    "left join tb_cat_variedad e on e.vari_clave = d.variedad and e.lin_clave = d.lin_clave " +
            ////    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            ////    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' " +
            ////    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave in ('16') " +
            ////    "group by a.prod_clave, a.hrp_recibo, e.vari_nombre";
            //query = "SELECT a.hrp_recibo, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            //    "FROM tb_hist_recepcion a " +
            //    "join tb_mstr_recepcion_pt F ON F.RPT_RECIBO = A.HRP_RECIBO AND F.LIN_CLAVE = A.LIN_CLAVE " +
            //    "left join tb_cat_variedad e on e.vari_clave = f.vari_clave and e.lin_clave = F.lin_clave " +
            //    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            //    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' /*AND e.vari_nombre = 'TLALOC'*/ " +
            //    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave = '" + LINEA + "' " +
            //    "group by a.prod_clave, a.hrp_recibo, e.vari_nombre";
            query = "SELECT a.hrp_recibo, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas, RTRIM(G.prov_nombre) AS prov_nombre, " +
                "RTRIM(H.rch_nombre) AS rch_nombre, ISNULL(RTRIM(I.tbl_nombre), 'SIN TABLA') AS tbl_nombre, RTRIM(F.prov_clave) AS prov_clave, RTRIM(F.rch_clave) AS rch_clave, RTRIM(F.tbl_clave) AS tbl_clave " +
                //", (SELECT COUNT(I.que_folio)  FROM tb_det_quejas I WHERE qud_ordprod = a.hrp_recibo) AS existe " +
                "FROM tb_hist_recepcion a " +
                "join tb_mstr_recepcion_pt F ON F.RPT_RECIBO = A.HRP_RECIBO AND F.LIN_CLAVE = A.LIN_CLAVE " +
                "left join tb_cat_variedad e on e.vari_clave = f.vari_clave and e.lin_clave = F.lin_clave " +
                //"JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
                "JOIN tb_cat_proveedor G ON F.prov_clave = G.prov_clave " +
                "left JOIN tb_cat_ranchos H ON F.prov_clave = H.prov_clave and F.rch_clave = H.rch_clave " +
                "left JOIN tb_cat_tablas I ON F.prov_clave = I.prov_clave and F.rch_clave = I.rch_clave AND F.tbl_clave = I.tbl_clave " +
                "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' /*AND e.vari_nombre = 'TLALOC'*/ " +
                "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave = '" + LINEA + "' " +
                "group by e.vari_nombre, G.prov_nombre, H.rch_nombre, I.tbl_nombre, F.prov_clave, F.rch_clave, F.tbl_clave, a.hrp_recibo/*a.prod_clave, a.hrp_recibo, e.vari_nombre, G.prov_nombre, H.rch_nombre, I.tbl_nombre*/";
            ////"WHERE a.hrp_fecha >= '27/04/2020' AND a.hrp_fecha <= '15/10/2020' " +
            adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dsDatos, "historico");


            //////query = "SELECT a.prod_clave, RTRIM(b.prod_nombre) AS prod_nombre, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            //////    "FROM tb_hist_recepcion a " +
            //////    "join tb_det_recepcion_pt d on d.rpt_recibo = a.hrp_recibo and d.lin_clave = a.lin_clave and d.prod_clave = a.prod_clave " +
            //////    "left join tb_cat_variedad e on e.vari_clave = d.variedad and e.lin_clave = d.lin_clave " +
            //////    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            //////    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' " +
            //////    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave in ('02') " +
            //////    "group by a.prod_clave, prod_nombre, e.vari_nombre";
            //////"WHERE a.hrp_fecha >= '27/04/2020' AND a.hrp_fecha <= '15/10/2020' " +
            ////query = "SELECT a.prod_clave, RTRIM(b.prod_nombre) AS prod_nombre, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas " +
            ////    "FROM tb_hist_recepcion a " +
            ////    "join tb_mstr_recepcion_pt F ON F.RPT_RECIBO = A.HRP_RECIBO AND F.LIN_CLAVE = A.LIN_CLAVE " +
            ////    "left join tb_cat_variedad e on e.vari_clave = f.vari_clave and e.lin_clave = F.lin_clave " +
            ////    "JOIN tb_cat_producto b ON a.prod_clave = b.prod_clave " +
            ////    "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' /*AND e.vari_nombre = 'TLALOC'*/ " +
            ////    "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave = '" + LINEA + "' " +
            ////    "group by a.prod_clave, prod_nombre, e.vari_nombre";
            ////adapter = new SqlDataAdapter(query, conn);
            ////adapter.Fill(dsDatos, "historico2");

            ////query = "select a.id_producto, a.nom_producto, c.pro_nombre, (CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS vari_nombre, " +
            ////    "SUM(a.qud_cantrecha) AS qud_cantrecha " +
            ////    "FROM tb_det_quejas a " +
            ////    "JOIN tb_mstr_quejas d ON d.que_folio = a.que_folio " +
            ////    "join tb_mstr_recepcion_pt b on a.qud_ordprod = b.rpt_recibo " +
            ////    "JOIN tb_cat_problemas c ON c.pro_clave = a.qud_problema " +
            ////    "where b.rpt_fecha >= '" + fi + "' and b.rpt_fecha <= '" + ff + "' /*AND A.qud_variedad = 'TLALOC'*/ and b.rpt_estatus <> 'F' and d.que_status in ('A', 'T') " +
            ////    "GROUP BY a.id_producto, a.nom_producto, c.pro_nombre, a.qud_variedad " +
            ////    "order by a.nom_producto";
            ////adapter = new SqlDataAdapter(query, conn);
            ////adapter.Fill(dsDatos, "historico3");

            ////clase.correo_error(query);


            Session["historico"] = dsDatos.Tables["historico"];
            //Session["historico2"] = dsDatos.Tables["historico2"];
            //Session["historico3"] = dsDatos.Tables["historico3"];

            var x = (from r in dsDatos.Tables["historico"].AsEnumerable() select r["vari_nombre"]).Distinct().ToList();

            DataTable uniqueRows = dsDatos.Tables["historico"].DefaultView.ToTable(true, "hrp_recibo", "vari_nombre", "rch_nombre", "tbl_nombre", "rch_clave", "tbl_clave", "prov_clave");

            DataRow rw;

            //Conncentrado de variedades
            foreach (var vari in x)
            {
                //if (vari.ToString() != "BIG STAR")
                //    continue;
                rw = dtVariedades.NewRow();
                rw["variedad"] = vari;
                dtVariedades.Rows.Add(rw);
            }

            //foreach (var vari in y)
            //{
            //    rw = dtRecibos.NewRow();
            //    rw["recibos"] = vari;
            //    dtRecibos.Rows.Add(rw);
            //}


            foreach (var vari in x)
            {
                rw = dtPrincipal.NewRow();
                rw["variedad"] = vari;
                rw["producido"] = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
                dtPrincipal.Rows.Add(rw);



                //foreach (DataRow ri in dsDatos.Tables["historico"].Select("vari_nombre = '" + vari.ToString() + "'"))
                string tabla_act = "";
                string tabla_ant = "";
                string rancho_act = "";
                string rancho_ant = "";
                string proveedor_act = "";
                string proveedor_ant = "";

                foreach (DataRow ri in uniqueRows.Select("vari_nombre = '" + vari.ToString() + "'"))//"vari_nombre = '" + vari.ToString() + "'"
                {
                    rancho_act = ri["rch_clave"].ToString();
                    tabla_act = ri["tbl_clave"].ToString();
                    proveedor_act = ri["prov_clave"].ToString();
                    if (rancho_act != rancho_ant)
                    {
                        //rw = dtPrincipal.NewRow();
                        //rw["rancho"] = ri["rch_nombre"];
                        //dtPrincipal.Rows.Add(rw);

                        if (tabla_act != tabla_ant)
                        {
                            string suma_tbl_rch = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()
                                && r.Field<string>("rch_clave") == rancho_act.ToString()
                                && r.Field<string>("tbl_clave") == tabla_act.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
                            rw = dtPrincipal.NewRow();
                            rw["rancho"] = ri["rch_nombre"];
                            rw["tabla"] = ri["tbl_nombre"];
                            rw["producido"] = suma_tbl_rch;
                            dtPrincipal.Rows.Add(rw);

                            //BUSCAR PROBLEMAS
                            if (ri["tbl_nombre"].ToString() == "SIN TABLA")
                            {
                                //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                //    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                //    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                //    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                //    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                //    "ORDER BY A.qud_variedad";

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                foreach (DataRow ri2 in uniqueRows.Select("vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = ''"))//"vari_nombre = '" + vari.ToString() + "'"
                                {
                                    //if (ri2["existe"].ToString() == "0")
                                    //{
                                    //    foreach (DataTable dt in dsDatos.Tables)
                                    //    {
                                    //        if (dt.TableName == "quejas")
                                    //        {
                                    //            dsDatos.Tables["quejas"].Clear();
                                    //        }
                                    //    }
                                    //    //dtReg.Rows.Clear();
                                    //    continue;
                                    //}

                                    query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                        //"WHERE A.qud_variedad = '" + vari + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    "WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND C.que_fecha <= '" + txtFechaFin.Text + "' " +//AND A.prov_clave = '" + ri["prov_clave"]
                                    "AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = ''" +//" + ri["tbl_clave"] + "
                                    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                    "ORDER BY A.qud_variedad";

                                    adapter = new SqlDataAdapter(query, conn);

                                    foreach (DataTable dt in dsDatos.Tables)
                                    {
                                        if (dt.TableName == "quejas")
                                        {
                                            dsDatos.Tables["quejas"].Clear();
                                        }
                                    }

                                    adapter.Fill(dsDatos, "quejas");

                                    if (dtReg.Rows.Count == 0)
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            string p_r_o = re["pro_nombre"].ToString();
                                            bool encontrado = false;
                                            foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                            {
                                                re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                                encontrado = true;
                                            }
                                            if (encontrado == false)
                                            {
                                                dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                            }
                                        }

                                    }

                                    //foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //{
                                    //    //bool fnd = false;
                                    //    //foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                                    //    //{
                                    //    //    //si encontro sumar valor de rechazo
                                    //    //    //clase.correo_error(query);
                                    //    //    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                                    //    //    fnd = true;
                                    //    //}
                                    //    //if (fnd == false)
                                    //    //{
                                    //    //si no encuentra agrega el renglon con el nuevo problema
                                    //    rw = dtPrincipal.NewRow();
                                    //    rw["variedad"] = "";// vari.ToString();
                                    //    rw["problema"] = re["pro_nombre"].ToString();
                                    //    rw["rechazado"] = re["qud_cantrecha"].ToString();
                                    //    //rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    //    dtPrincipal.Rows.Add(rw);
                                    //    //}
                                    //}

                                }
                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                    rw = dtPrincipal.NewRow();
                                    rw["variedad"] = "";// vari.ToString();
                                    rw["problema"] = re2["problem"].ToString();
                                    rw["rechazado"] = re2["rejected"].ToString();
                                    rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    dtPrincipal.Rows.Add(rw);
                                }

                                //string suma_tbl_rch_2 = (dsDatos.Tables["quejas"].AsEnumerable().Sum(r => r.Field<decimal>("qud_cantrecha"))).ToString();



                                
                                

                                
                            }
                            else
                            {
                                //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                //    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                //    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                //    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                //    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                //    "ORDER BY A.qud_variedad";

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                string cadena_busqueda = "vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '" + ri["tbl_clave"] + "'";
                                //if (cadena_busqueda.Contains("tbl_clave ='18'"))
                                //{
                                //}

                                foreach (DataRow ri2 in uniqueRows.Select(cadena_busqueda))//"vari_nombre = '" + vari.ToString() + "'"
                                {
                                    //if (ri2["existe"].ToString() == "0")
                                    //{
                                    //    foreach (DataTable dt in dsDatos.Tables)
                                    //    {
                                    //        if (dt.TableName == "quejas")
                                    //        {
                                    //            dsDatos.Tables["quejas"].Clear();
                                    //        }
                                    //    }
                                    //    dtReg.Rows.Clear();
                                    //    continue;
                                    //}
                                    query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                        //"WHERE A.qud_variedad = '" + vari + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    "WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND C.que_fecha <= '" + txtFechaFin.Text + "' " +//AND A.prov_clave = '" + ri["prov_clave"]
                                    "AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                    "ORDER BY A.qud_variedad";

                                    adapter = new SqlDataAdapter(query, conn);

                                    foreach (DataTable dt in dsDatos.Tables)
                                    {
                                        if (dt.TableName == "quejas")
                                        {
                                            dsDatos.Tables["quejas"].Clear();
                                        }
                                    }

                                    adapter.Fill(dsDatos, "quejas");

                                    if (dtReg.Rows.Count == 0)
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            string p_r_o = re["pro_nombre"].ToString();
                                            bool encontrado = false;
                                            foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                            {
                                                re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                                encontrado = true;
                                            }
                                            if (encontrado == false)
                                            {
                                                dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                            }
                                        }
                                        
                                    }
                                    


                                    //foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //{
                                    //    //bool fnd = false;
                                    //    //foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                                    //    //{
                                    //    //    //si encontro sumar valor de rechazo
                                    //    //    //clase.correo_error(query);
                                    //    //    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                                    //    //    fnd = true;
                                    //    //}
                                    //    //if (fnd == false)
                                    //    //{
                                    //    //si no encuentra agrega el renglon con el nuevo problema
                                    //    rw = dtPrincipal.NewRow();
                                    //    rw["variedad"] = "";// vari.ToString();
                                    //    rw["problema"] = re["pro_nombre"].ToString();
                                    //    rw["rechazado"] = re["qud_cantrecha"].ToString();
                                    //    dtPrincipal.Rows.Add(rw);
                                    //    //}
                                    //}

                                }

                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                        rw = dtPrincipal.NewRow();
                                        rw["variedad"] = "";// vari.ToString();
                                        rw["problema"] = re2["problem"].ToString();
                                        rw["rechazado"] = re2["rejected"].ToString();
                                        rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                        dtPrincipal.Rows.Add(rw);
                                }

                                //string suma_tbl_rch_2 = (dsDatos.Tables["quejas"].AsEnumerable().Sum(r => r.Field<decimal>("qud_cantrecha"))).ToString();



                                
                                ////BUSCAR PROBLEMAS
                                //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                //    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                //    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                //    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                //    //"WHERE A.qud_variedad = '" + vari + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                //    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                //    "ORDER BY A.qud_variedad";

                                //adapter = new SqlDataAdapter(query, conn);

                                //foreach (DataTable dt in dsDatos.Tables)
                                //{
                                //    if (dt.TableName == "quejas")
                                //    {
                                //        dsDatos.Tables["quejas"].Clear();
                                //    }
                                //}

                                //adapter.Fill(dsDatos, "quejas");

                                //foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                //{
                                //    //bool fnd = false;
                                //    //foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                                //    //{
                                //    //    //si encontro sumar valor de rechazo
                                //    //    //clase.correo_error(query);
                                //    //    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                                //    //    fnd = true;
                                //    //}
                                //    //if (fnd == false)
                                //    //{
                                //    //si no encuentra agrega el renglon con el nuevo problema
                                //    rw = dtPrincipal.NewRow();
                                //    rw["variedad"] = vari.ToString();
                                //    rw["problema"] = re["pro_nombre"].ToString();
                                //    rw["rechazado"] = re["qud_cantrecha"].ToString();
                                //    dtPrincipal.Rows.Add(rw);
                                //    //}
                                //}
                            }
                            
                        }
                    }
                    else
                    {
                        if (tabla_act != tabla_ant)
                        {
                            string suma_tbl_rch = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()
                                && r.Field<string>("rch_clave") == rancho_act.ToString()
                                && r.Field<string>("tbl_clave") == tabla_act.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
                            rw = dtPrincipal.NewRow();
                            rw["rancho"] = ri["rch_nombre"];
                            rw["tabla"] = ri["tbl_nombre"];
                            rw["producido"] = suma_tbl_rch;
                            dtPrincipal.Rows.Add(rw);

                            //BUSCAR PROBLEMAS
                            if (ri["tbl_nombre"].ToString() == "SIN TABLA")
                            {
                                //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                //    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                //    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                //    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                //    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                //    "ORDER BY A.qud_variedad";

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                foreach (DataRow ri2 in uniqueRows.Select("vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = ''"))//"vari_nombre = '" + vari.ToString() + "'"
                                {
                                    //if (ri2["existe"].ToString() == "0")
                                    //{
                                    //    foreach (DataTable dt in dsDatos.Tables)
                                    //    {
                                    //        if (dt.TableName == "quejas")
                                    //        {
                                    //            dsDatos.Tables["quejas"].Clear();
                                    //        }
                                    //    }
                                    //    dtReg.Rows.Clear();
                                    //    continue;
                                    //}

                                    query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                        //"WHERE A.qud_variedad = '" + vari + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    "WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND C.que_fecha <= '" + txtFechaFin.Text + "' " +//AND A.prov_clave = '" + ri["prov_clave"]
                                    "AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = ''" +//" + ri["tbl_clave"] + "
                                    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                    "ORDER BY A.qud_variedad";

                                    adapter = new SqlDataAdapter(query, conn);

                                    foreach (DataTable dt in dsDatos.Tables)
                                    {
                                        if (dt.TableName == "quejas")
                                        {
                                            dsDatos.Tables["quejas"].Clear();
                                        }
                                    }

                                    adapter.Fill(dsDatos, "quejas");

                                    if (dtReg.Rows.Count == 0)
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            string p_r_o = re["pro_nombre"].ToString();
                                            foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                            {
                                                re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                            }
                                        }

                                    }

                                    //foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //{
                                    //    //bool fnd = false;
                                    //    //foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                                    //    //{
                                    //    //    //si encontro sumar valor de rechazo
                                    //    //    //clase.correo_error(query);
                                    //    //    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                                    //    //    fnd = true;
                                    //    //}
                                    //    //if (fnd == false)
                                    //    //{
                                    //    //si no encuentra agrega el renglon con el nuevo problema
                                    //    rw = dtPrincipal.NewRow();
                                    //    rw["variedad"] = "";// vari.ToString();
                                    //    rw["problema"] = re["pro_nombre"].ToString();
                                    //    rw["rechazado"] = re["qud_cantrecha"].ToString();
                                    //    dtPrincipal.Rows.Add(rw);
                                    //    //}
                                    //}

                                }

                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                    rw = dtPrincipal.NewRow();
                                    rw["variedad"] = "";// vari.ToString();
                                    rw["problema"] = re2["problem"].ToString();
                                    rw["rechazado"] = re2["rejected"].ToString();
                                    //rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    dtPrincipal.Rows.Add(rw);
                                }

                                //string suma_tbl_rch_2 = (dsDatos.Tables["quejas"].AsEnumerable().Sum(r => r.Field<decimal>("qud_cantrecha"))).ToString();



                                
                                ////query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                ////    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                ////    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                ////    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                ////    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                ////    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                ////    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                ////    "ORDER BY A.qud_variedad";
                                //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                //    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                //    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                //    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                //    //"WHERE A.qud_variedad = '" + vari + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                //    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                //    "ORDER BY A.qud_variedad";

                                //adapter = new SqlDataAdapter(query, conn);

                                //foreach (DataTable dt in dsDatos.Tables)
                                //{
                                //    if (dt.TableName == "quejas")
                                //    {
                                //        dsDatos.Tables["quejas"].Clear();
                                //    }
                                //}

                                //adapter.Fill(dsDatos, "quejas");

                                //foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                //{
                                //    //bool fnd = false;
                                //    //foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                                //    //{
                                //    //    //si encontro sumar valor de rechazo
                                //    //    //clase.correo_error(query);
                                //    //    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                                //    //    fnd = true;
                                //    //}
                                //    //if (fnd == false)
                                //    //{
                                //    //si no encuentra agrega el renglon con el nuevo problema
                                //    rw = dtPrincipal.NewRow();
                                //    rw["variedad"] = vari.ToString();
                                //    rw["problema"] = re["pro_nombre"].ToString();
                                //    rw["rechazado"] = re["qud_cantrecha"].ToString();
                                //    dtPrincipal.Rows.Add(rw);
                                //    //}
                                //}
                            }
                            else
                            {
                                //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                //    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                //    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                //    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                //    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                //    "ORDER BY A.qud_variedad";

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                string cadena_busqueda = "vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '" + ri["tbl_clave"] + "'";

                                foreach (DataRow ri2 in uniqueRows.Select(cadena_busqueda))//"vari_nombre = '" + vari.ToString() + "'"
                                {
                                    //if (ri2["existe"].ToString() == "0")
                                    //{
                                    //    foreach (DataTable dt in dsDatos.Tables)
                                    //    {
                                    //        if (dt.TableName == "quejas")
                                    //        {
                                    //            dsDatos.Tables["quejas"].Clear();
                                    //        }
                                    //    }
                                    //    dtReg.Rows.Clear();
                                    //    continue;
                                    //}

                                    query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                        //"WHERE A.qud_variedad = '" + vari + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    "WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND C.que_fecha <= '" + txtFechaFin.Text + "' " +//AND A.prov_clave = '" + ri["prov_clave"]
                                    "AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                    "ORDER BY A.qud_variedad";

                                    adapter = new SqlDataAdapter(query, conn);

                                    foreach (DataTable dt in dsDatos.Tables)
                                    {
                                        if (dt.TableName == "quejas")
                                        {
                                            dsDatos.Tables["quejas"].Clear();
                                        }
                                    }

                                    adapter.Fill(dsDatos, "quejas");

                                    if (dtReg.Rows.Count == 0)
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                        {
                                            string p_r_o = re["pro_nombre"].ToString();
                                            bool encontrado = false;
                                            foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                            {
                                                re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                                encontrado = true;
                                            }
                                            if (encontrado == false)
                                            {
                                                dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                            }
                                        }

                                    }

                                    //foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //{
                                    //    //bool fnd = false;
                                    //    //foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                                    //    //{
                                    //    //    //si encontro sumar valor de rechazo
                                    //    //    //clase.correo_error(query);
                                    //    //    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                                    //    //    fnd = true;
                                    //    //}
                                    //    //if (fnd == false)
                                    //    //{
                                    //    //si no encuentra agrega el renglon con el nuevo problema
                                    //    rw = dtPrincipal.NewRow();
                                    //    rw["variedad"] = "";// vari.ToString();
                                    //    rw["problema"] = re["pro_nombre"].ToString();
                                    //    rw["rechazado"] = re["qud_cantrecha"].ToString();
                                    //    dtPrincipal.Rows.Add(rw);
                                    //    //}
                                    //}

                                }

                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                    rw = dtPrincipal.NewRow();
                                    rw["variedad"] = "";// vari.ToString();
                                    rw["problema"] = re2["problem"].ToString();
                                    rw["rechazado"] = re2["rejected"].ToString();
                                    rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    dtPrincipal.Rows.Add(rw);
                                }

                                //string suma_tbl_rch_2 = (dsDatos.Tables["quejas"].AsEnumerable().Sum(r => r.Field<decimal>("qud_cantrecha"))).ToString();



                                
                                ////BUSCAR PROBLEMAS
                                ////query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                ////    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                ////    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                ////    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                ////    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                ////    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                ////    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                ////    "ORDER BY A.qud_variedad";
                                //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                //    "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                //    "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                //    "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                //    //"WHERE A.qud_variedad = '" + vari + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                //    "AND C.que_fecha >= '" + txtFechaInicio.Text + "' AND A.prov_clave = '" + ri["prov_clave"] + "' AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                //    "GROUP BY B.pro_nombre, A.qud_variedad " +
                                //    "ORDER BY A.qud_variedad";

                                //adapter = new SqlDataAdapter(query, conn);

                                //foreach (DataTable dt in dsDatos.Tables)
                                //{
                                //    if (dt.TableName == "quejas")
                                //    {
                                //        dsDatos.Tables["quejas"].Clear();
                                //    }
                                //}

                                //adapter.Fill(dsDatos, "quejas");

                                //foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                //{
                                //    //bool fnd = false;
                                //    //foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                                //    //{
                                //    //    //si encontro sumar valor de rechazo
                                //    //    //clase.correo_error(query);
                                //    //    re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                                //    //    fnd = true;
                                //    //}
                                //    //if (fnd == false)
                                //    //{
                                //    //si no encuentra agrega el renglon con el nuevo problema
                                //    rw = dtPrincipal.NewRow();
                                //    rw["variedad"] = vari.ToString();
                                //    rw["problema"] = re["pro_nombre"].ToString();
                                //    rw["rechazado"] = re["qud_cantrecha"].ToString();
                                //    dtPrincipal.Rows.Add(rw);
                                //    //}
                                //}
                            }
                        }
                    }

                    rancho_ant = rancho_act;
                    tabla_ant = tabla_act;
                    //if (vari.ToString() == "SIN VARIEDAD")
                    //{
                    //    query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                    //        "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                    //        "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    //        "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                    //        "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                    //        "AND C.que_fecha >= '" + txtFechaInicio.Text + "' " +
                    //        "GROUP BY B.pro_nombre, A.qud_variedad " +
                    //        "ORDER BY A.qud_variedad";
                    //    adapter = new SqlDataAdapter(query, conn);

                    //    //Limpiar datatable
                    //    foreach (DataTable dt in dsDatos.Tables)
                    //    {
                    //        if (dt.TableName == "quejas")
                    //        {
                    //            dsDatos.Tables["quejas"].Clear();
                    //        }
                    //    }

                    //    //llenar datatable
                    //    adapter.Fill(dsDatos, "quejas");

                    //    //recorrer datatable filtrar por variedad y problema para sumar el rechazo
                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                    //    {
                    //        bool fnd = false;
                    //        foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                    //        {
                    //            //si encontro sumar valor de rechazo
                    //            //clase.correo_error(query);
                    //            re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                    //            fnd = true;
                    //        }
                    //        if (fnd == false)
                    //        {
                    //            //si no encuentra agrega el renglon con el nuevo problema
                    //            rw = dtPrincipal.NewRow();
                    //            rw["variedad"] = vari.ToString();
                    //            rw["problema"] = re["pro_nombre"].ToString();
                    //            rw["rechazado"] = re["qud_cantrecha"].ToString();
                    //            dtPrincipal.Rows.Add(rw);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                    //        "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                    //        "FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    //        "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                    //        "WHERE A.qud_variedad = '" + vari.ToString() + "' AND A.qud_ordprod = '" + ri["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                    //        "AND C.que_fecha >= '" + txtFechaInicio.Text + "' " +
                    //        "GROUP BY B.pro_nombre, A.qud_variedad " +
                    //        "ORDER BY A.qud_variedad";
                    //    adapter = new SqlDataAdapter(query, conn);

                    //    //Limpiar datatable
                    //    foreach (DataTable dt in dsDatos.Tables)
                    //    {
                    //        if (dt.TableName == "quejas")
                    //        {
                    //            dsDatos.Tables["quejas"].Clear();
                    //        }
                    //    }

                    //    //llenar datatable
                    //    adapter.Fill(dsDatos, "quejas");

                    //    //recorrer datatable filtrar por variedad y problema para sumar el rechazo
                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                    //    {
                    //        //conectasql clase = new conectasql();
                    //        //clase.correo_error(query);
                    //        bool fnd = false;
                    //        foreach (DataRow re2 in dtPrincipal.Select("variedad = '" + vari.ToString() + "' AND problema = '" + re["pro_nombre"].ToString() + "'"))
                    //        {
                    //            //si encontro sumar valor de rechazo
                    //            re2["rechazado"] = Convert.ToDecimal(re2["rechazado"].ToString()) + Convert.ToDecimal(re["qud_cantrecha"].ToString());
                    //            fnd = true;
                    //        }
                    //        if (fnd == false)
                    //        {
                    //            //si no encuentra agrega el renglon con el nuevo problema
                    //            rw = dtPrincipal.NewRow();
                    //            rw["variedad"] = vari.ToString();
                    //            rw["problema"] = re["pro_nombre"].ToString();
                    //            rw["rechazado"] = re["qud_cantrecha"].ToString();
                    //            dtPrincipal.Rows.Add(rw);
                    //        }
                    //    }
                    //}
                }
            }
            conn.Close();

            foreach (DataRow rt in dtPrincipal.Select("variedad <> '' AND producido > 0"))
            {
                rt["rechazado"] = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") == rt["variedad"].ToString() && r.Field<string>("problema") != "").Sum(r => r.Field<decimal?>("rechazado"))).ToString();
                rt["porcentaje"] = ((Convert.ToDecimal(rt["rechazado"]) * Convert.ToDecimal("100")) / Convert.ToDecimal(rt["producido"])).ToString("#0.00");
            }

            string html = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive w-auto small'>" +
                "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>Variedad</font></th><th bgcolor='#337ab7'><font color='#fff'>Rancho</font></th><th bgcolor='#337ab7'><font color='#fff'>Tabla</font></th><th bgcolor='#337ab7'><font color='#fff'>Recibido</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th><th bgcolor='#337ab7'><font color='#fff'>Rechazado</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Porcentaje</font></th></tr></thead><tbody>";

            //Totales generales
            //string total_producido = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && r.Field<decimal?>("producido") > 0).Sum(r => r.Field<decimal?>("producido"))).ToString();
            //string total_producido = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && (r.Field<string>("rancho") == "" && r.Field<string>("tabla") == "")).Sum(r => r.Field<decimal?>("producido"))).ToString();
            //string total_rechazado = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") == "" && r.Field<decimal?>("producido") == 0).Sum(r => r.Field<decimal?>("rechazado"))).ToString();
            //(dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && r.Field<string>("problema") != "").Sum(r => r.Field<decimal?>("rechazado"))).ToString();
            decimal total_producido = 0;
            decimal total_rechazado = 0;

            foreach (DataRow rz in dtPrincipal.Select("variedad <> ''"))
                total_producido = total_producido + Convert.ToDecimal(rz["producido"]);
            foreach (DataRow rz in dtPrincipal.Select("problema <> ''"))
                total_rechazado = total_rechazado + Convert.ToDecimal(rz["rechazado"]);

            string total_porcentaje = "";
            if (Convert.ToDecimal(total_producido) == 0)
            {
                total_porcentaje = Convert.ToDecimal(total_producido).ToString("##0.00");
            }
            else
            {
                total_porcentaje = ((Convert.ToDecimal(total_rechazado) * Convert.ToDecimal("100")) / Convert.ToDecimal(total_producido)).ToString("##0.00");
            }

            html += "<tr><td><b>Totales Generales</b></td><td></td><td></td><td align='right'><b>" + Convert.ToDecimal(total_producido).ToString("###,###,##0") + "</b></td><td></td><td align='right'><b>" + Convert.ToDecimal(total_rechazado).ToString("###,###,##0") + "</b></td><td align='right'><b>" + total_porcentaje + " %</b></td></tr>";

            string var_act = "";
            string var_ant = "";
            foreach (DataRow ra in dtPrincipal.Rows)
            {
                var_act = ra["variedad"].ToString();
                //if (var_act != var_ant)
                //    html += "<tr><td><b><a onclick='recibido_variedad(\"" + ra["variedad"] + "\")' style='color:black'>" + ra["variedad"] + "</a></b></td><td align='right'><b>" + ra["rancho"].ToString() + "</b></td><td align='right'><b>" + ra["tabla"].ToString() + "</b></td><td align='right'><b>" + Convert.ToDecimal(ra["producido"].ToString()).ToString("###,###,##0") + "</b></td><td></td><td align='right'><b>" + ((ra["rechazado"].ToString() != "") ? Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") : "0") + "</b></td><td align='right'><b>" + ((ra["porcentaje"].ToString() != "") ? Convert.ToDecimal(ra["porcentaje"].ToString()).ToString("##0.00") : "0") + " %</b></td></tr>";
                //else
                //    html += "<tr><td></td><td></td><td></td><td></td><td><a onclick='rechazado_problema(\"" + ra["variedad"] + "\", \"" + ra["problema"] + "\")' style='color:black'>" + ra["problema"] + "</a></td><td align='right'>" + ((ra["rechazado"].ToString() != "") ? Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") : "0") + "</td><td></td></tr>";
                if(var_act != var_ant)
                    html += "<tr><td align='right'><b>" + ra["variedad"].ToString() + "</b></td><td align='right'><b>" + ra["rancho"].ToString() + "</b></td><td align='right'><b>" + ra["tabla"].ToString() + "</b></td><td align='right'><b>" + Convert.ToDecimal(ra["producido"].ToString()).ToString("###,###,##0") + "</b></td><td align='right'><b>" + ra["problema"].ToString() + "</b></td><td></td><td></td></tr>";//<td align='right'><b>" + ra["rechazado"].ToString() + "</b></td><td align='right'><b>" + ra["porcentaje"].ToString() + "</b></td>
                else
                    html += "<tr><td></td><td align='right'><b>" + ra["rancho"].ToString() + "</b></td><td align='right'><b>" + ra["tabla"].ToString() + "</b></td><td align='right'><b>" + ((ra["producido"].ToString() != "") ? Convert.ToDecimal(ra["producido"].ToString()).ToString("###,###,##0") : "") + "</b></td><td align='right'><b>" + ra["problema"].ToString() + "</b></td><td align='right'><b>" + ((ra["rechazado"].ToString() != "") ? Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") : "") + "</b></td><td align='right'><b>" + ra["porcentaje"].ToString() + "</b></td></tr>";
                var_ant = var_act;
                
            }

            html += "</tbody></table>";

            this.datosqueja.InnerHtml = html;
        }


        public void reporte_tablas_2()
        {
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            SqlDataAdapter adapter;
            DataSet dsDatos = new DataSet();

            string query = "";
            conn.Open();

            string fi = "";
            string ff = "";

            fi = txtFechaInicio.Text;
            ff = txtFechaFin.Text;

            string LINEA = ddlQuejas.SelectedValue.ToString();

            if (fi == "" || ff == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Falta ingresar una fecha');", true);
                return;
            }

            query = "SELECT a.hrp_recibo, ISNULL(RTRIM(e.vari_nombre), 'SIN VARIEDAD') as vari_nombre, sum(a.hrp_num_unidades) AS num_cajas, RTRIM(G.prov_nombre) AS prov_nombre, " +
                "RTRIM(H.rch_nombre) AS rch_nombre, ISNULL(RTRIM(I.tbl_nombre), 'SIN TABLA') AS tbl_nombre, RTRIM(F.prov_clave) AS prov_clave, RTRIM(F.rch_clave) AS rch_clave, RTRIM(F.tbl_clave) AS tbl_clave " +
                "FROM tb_hist_recepcion a " +
                "join tb_mstr_recepcion_pt F ON F.RPT_RECIBO = A.HRP_RECIBO AND F.LIN_CLAVE = A.LIN_CLAVE " +
                "left join tb_cat_variedad e on e.vari_clave = f.vari_clave and e.lin_clave = F.lin_clave " +
                "JOIN tb_cat_proveedor G ON F.prov_clave = G.prov_clave " +
                "left JOIN tb_cat_ranchos H ON F.prov_clave = H.prov_clave and F.rch_clave = H.rch_clave " +
                "left JOIN tb_cat_tablas I ON F.prov_clave = I.prov_clave and F.rch_clave = I.rch_clave AND F.tbl_clave = I.tbl_clave " +
                "WHERE a.hrp_fecha >= '" + fi + "' AND a.hrp_fecha <= '" + ff + "' /*AND e.vari_nombre = 'TLALOC'*/ " +
                "and a.hrp_estatus <> 'C' and a.hrp_tipo_recepcion = 'PTC' and a.hrp_situacion = 'CM' and a.lin_clave = '" + LINEA + "' " +
                "group by e.vari_nombre, G.prov_nombre, H.rch_nombre, I.tbl_nombre, F.prov_clave, F.rch_clave, F.tbl_clave, a.hrp_recibo/*a.prod_clave, a.hrp_recibo, e.vari_nombre, G.prov_nombre, H.rch_nombre, I.tbl_nombre*/";
            adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dsDatos, "historico");

            query = "SELECT A.qud_ordprod, SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                "(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad, " +
                "A.rch_clave, A.tbl_clave " + 
                "FROM tb_det_quejas A " + 
                "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " + 
                "JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " + 
                "WHERE C.que_status in ('A', 'T') " + 
                "AND C.que_fecha >= '" + fi + "' " +
                "GROUP BY A.rch_clave, A.tbl_clave, A.qud_variedad, B.pro_nombre, A.qud_ordprod ORDER BY A.qud_variedad";
            adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dsDatos, "det_quejas");

            //dsDatos.Tables["historico"].DefaultView.Sort = "vari_nombre DESC";
            //DataView dw = dsDatos.Tables["historico"].DefaultView;
            //dw.Sort = "vari_nombre DESC";
            //dsDatos.Tables["historico"].t = dw.ToTable();

            Session["historico"] = dsDatos.Tables["historico"];

            var x = (from r in dsDatos.Tables["historico"].AsEnumerable() select r["vari_nombre"]).Distinct().ToList();

            DataTable uniqueRows = dsDatos.Tables["historico"].DefaultView.ToTable(true, "vari_nombre", "hrp_recibo", "rch_nombre", "tbl_nombre", "rch_clave", "tbl_clave", "prov_clave");
            DataView dv = uniqueRows.DefaultView;
            dv.Sort = "vari_nombre, rch_clave, tbl_clave";
            uniqueRows = dv.ToTable();
            

            DataRow rw;
            DataRow rw2;

            //Conncentrado de variedades
            foreach (var vari in x)
            {
                rw = dtVariedades.NewRow();
                rw["variedad"] = vari;
                dtVariedades.Rows.Add(rw);
            }

            

            foreach (var vari in x)
            {
                //string suma_variedad = (dsDatos.Tables["det_quejas"].AsEnumerable().Where(r => r.Field<string>("qud_variedad") == vari.ToString())
                //                .Sum(r => r.Field<Int32>("qud_cantrecha"))).ToString();
                rw = dtPrincipal.NewRow();
                rw["variedad"] = vari;
                rw["producido"] = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
                //rw["rechazado"] = suma_variedad;
                dtPrincipal.Rows.Add(rw);

                //rw2 = dtPrincipal2.NewRow();
                //rw2["variedad"] = vari;
                //rw2["producido"] = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
                //dtPrincipal2.Rows.Add(rw2);

                //foreach (DataRow ri in dsDatos.Tables["historico"].Select("vari_nombre = '" + vari.ToString() + "'"))
                string tabla_act = "";
                string tabla_ant = "";
                string rancho_act = "";
                string rancho_ant = "";
                string proveedor_act = "";
                string proveedor_ant = "";

                string tbl_nom_act = "";
                string tbl_nom_ant = "";

                foreach (DataRow ri in uniqueRows.Select("vari_nombre = '" + vari.ToString() + "'"))//"vari_nombre = '" + vari.ToString() + "'"
                {
                    rancho_act = ri["rch_clave"].ToString();
                    tabla_act = ri["tbl_clave"].ToString();
                    proveedor_act = ri["prov_clave"].ToString();
                    tbl_nom_act = ri["tbl_nombre"].ToString();
                    if (rancho_act != rancho_ant)
                    {
                        //rw = dtPrincipal.NewRow();
                        //rw["rancho"] = ri["rch_nombre"];
                        //dtPrincipal.Rows.Add(rw);

                        if (tabla_act != tabla_ant)
                        {
                            string suma_tbl_rch = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()
                                && r.Field<string>("rch_clave") == rancho_act.ToString()
                                && r.Field<string>("tbl_clave") == tabla_act.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
                            
                            rw = dtPrincipal.NewRow();
                            rw["rancho"] = ri["rch_nombre"];
                            rw["tabla"] = ri["tbl_nombre"];
                            rw["producido"] = suma_tbl_rch;
                            dtPrincipal.Rows.Add(rw);


                            //BUSCAR PROBLEMAS
                            if (ri["tbl_nombre"].ToString() == "SIN TABLA")
                            {

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                foreach (DataRow ri2 in uniqueRows.Select("vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = ''"))//"vari_nombre = '" + vari.ToString() + "'"
                                {
                                    foreach (DataRow rg in dsDatos.Tables["det_quejas"].Select("rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '' AND qud_ordprod = '" + ri2["hrp_recibo"] + "'"))
                                    {
                                        string p_r_o = rg["pro_nombre"].ToString();
                                        bool encontrado = false;
                                        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                        {
                                            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(rg["qud_cantrecha"]);
                                            encontrado = true;
                                        }
                                        if (encontrado == false)
                                        {
                                            dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                        }
                                        //dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                    }
                                    //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    //"(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    //"FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    //"JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                    //"WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = ''" +
                                    //"GROUP BY B.pro_nombre, A.qud_variedad " +
                                    //"ORDER BY A.qud_variedad";

                                    //adapter = new SqlDataAdapter(query, conn);

                                    //foreach (DataTable dt in dsDatos.Tables)
                                    //{
                                    //    if (dt.TableName == "quejas")
                                    //    {
                                    //        dsDatos.Tables["quejas"].Clear();
                                    //    }
                                    //}

                                    //adapter.Fill(dsDatos, "quejas");

                                    //if (dtReg.Rows.Count == 0)
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        string p_r_o = re["pro_nombre"].ToString();
                                    //        bool encontrado = false;
                                    //        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                    //        {
                                    //            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                    //            encontrado = true;
                                    //        }
                                    //        if (encontrado == false)
                                    //        {
                                    //            dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                    //        }
                                    //    }

                                    //}

                                }

                                int z = 0;
                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                    rw = dtPrincipal.NewRow();
                                    rw["variedad"] = vari.ToString();
                                    rw["problema"] = re2["problem"].ToString();
                                    rw["rechazado"] = re2["rejected"].ToString();
                                    rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    dtPrincipal.Rows.Add(rw);

                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = re2["problem"].ToString();
                                    rw2["rechazado"] = re2["rejected"].ToString();
                                    dtPrincipal2.Rows.Add(rw2);
                                }
                                if (dtReg.Rows.Count == 0)
                                {
                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = "SIN_RECHAZO";
                                    rw2["rechazado"] = "0";
                                    dtPrincipal2.Rows.Add(rw2);
                                }
                            }
                            else
                            {

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                string cadena_busqueda = "vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '" + ri["tbl_clave"] + "'";

                                foreach (DataRow ri2 in uniqueRows.Select(cadena_busqueda))
                                {
                                    foreach (DataRow rg in dsDatos.Tables["det_quejas"].Select("rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '" + ri["tbl_clave"] + "' AND qud_ordprod = '" + ri2["hrp_recibo"] + "'"))
                                    {
                                        string p_r_o = rg["pro_nombre"].ToString();
                                        bool encontrado = false;
                                        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                        {
                                            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(rg["qud_cantrecha"]);
                                            encontrado = true;
                                        }
                                        if (encontrado == false)
                                        {
                                            dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                        }
                                        //dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                    }
                                    //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    //"(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    //"FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    //"JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                    //"WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                    //"GROUP BY B.pro_nombre, A.qud_variedad " +
                                    //"ORDER BY A.qud_variedad";

                                    //adapter = new SqlDataAdapter(query, conn);

                                    //foreach (DataTable dt in dsDatos.Tables)
                                    //{
                                    //    if (dt.TableName == "quejas")
                                    //    {
                                    //        dsDatos.Tables["quejas"].Clear();
                                    //    }
                                    //}

                                    //adapter.Fill(dsDatos, "quejas");

                                    //if (dtReg.Rows.Count == 0)
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        string p_r_o = re["pro_nombre"].ToString();
                                    //        bool encontrado = false;
                                    //        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                    //        {
                                    //            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                    //            encontrado = true;
                                    //        }
                                    //        if (encontrado == false)
                                    //        {
                                    //            dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                    //        }
                                    //    }

                                    //}


                                }

                                int z = 0;
                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                    rw = dtPrincipal.NewRow();
                                    rw["variedad"] = vari.ToString();

                                    rw["rancho"] = ri["rch_nombre"];
                                    rw["tabla"] = ri["tbl_nombre"];

                                    rw["problema"] = re2["problem"].ToString();
                                    rw["rechazado"] = re2["rejected"].ToString();
                                    rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    dtPrincipal.Rows.Add(rw);

                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = re2["problem"].ToString();
                                    rw2["rechazado"] = re2["rejected"].ToString();
                                    dtPrincipal2.Rows.Add(rw2);
                                }

                                if (dtReg.Rows.Count == 0)
                                {
                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = "SIN_RECHAZO";
                                    rw2["rechazado"] = "0";
                                    dtPrincipal2.Rows.Add(rw2);
                                }
                            }

                        }
                    }
                    else
                    {
                        if (tabla_act != tabla_ant)
                        {
                            string suma_tbl_rch = (dsDatos.Tables["historico"].AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString()
                                && r.Field<string>("rch_clave") == rancho_act.ToString()
                                && r.Field<string>("tbl_clave") == tabla_act.ToString()).Sum(r => r.Field<decimal>("num_cajas"))).ToString();
                            rw = dtPrincipal.NewRow();
                            rw["rancho"] = ri["rch_nombre"];
                            rw["tabla"] = ri["tbl_nombre"];
                            rw["producido"] = suma_tbl_rch;
                            dtPrincipal.Rows.Add(rw);

                            

                            //BUSCAR PROBLEMAS
                            if (ri["tbl_nombre"].ToString() == "SIN TABLA")
                            {

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                foreach (DataRow ri2 in uniqueRows.Select("vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = ''"))//"vari_nombre = '" + vari.ToString() + "'"
                                {
                                    foreach (DataRow rg in dsDatos.Tables["det_quejas"].Select("rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '' AND qud_ordprod = '" + ri2["hrp_recibo"] + "'"))
                                    {
                                        string p_r_o = rg["pro_nombre"].ToString();
                                        bool encontrado = false;
                                        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                        {
                                            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(rg["qud_cantrecha"]);
                                            encontrado = true;
                                        }
                                        if (encontrado == false)
                                        {
                                            dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                        }
                                        //dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                    }
                                    //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    //"(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    //"FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    //"JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                    //"WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = ''" +
                                    //"GROUP BY B.pro_nombre, A.qud_variedad " +
                                    //"ORDER BY A.qud_variedad";

                                    //adapter = new SqlDataAdapter(query, conn);

                                    //foreach (DataTable dt in dsDatos.Tables)
                                    //{
                                    //    if (dt.TableName == "quejas")
                                    //    {
                                    //        dsDatos.Tables["quejas"].Clear();
                                    //    }
                                    //}

                                    //adapter.Fill(dsDatos, "quejas");

                                    //if (dtReg.Rows.Count == 0)
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        string p_r_o = re["pro_nombre"].ToString();
                                    //        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                    //        {
                                    //            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                    //        }
                                    //    }

                                    //}

                                }

                                int z = 0;
                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                    rw = dtPrincipal.NewRow();
                                    rw["variedad"] = vari.ToString();

                                    rw["rancho"] = ri["rch_nombre"];
                                    rw["tabla"] = ri["tbl_nombre"];

                                    rw["problema"] = re2["problem"].ToString();
                                    rw["rechazado"] = re2["rejected"].ToString();
                                    rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    dtPrincipal.Rows.Add(rw);

                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = re2["problem"].ToString();
                                    rw2["rechazado"] = re2["rejected"].ToString();
                                    dtPrincipal2.Rows.Add(rw2);
                                }
                                if (dtReg.Rows.Count == 0)
                                {
                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = "SIN_RECHAZO";
                                    rw2["rechazado"] = "0";
                                    dtPrincipal2.Rows.Add(rw2);
                                }
                                
                            }
                            else
                            {

                                DataTable dtReg = new DataTable();
                                dtReg.Columns.Add("problem", typeof(string));
                                dtReg.Columns.Add("rejected", typeof(string));

                                string cadena_busqueda = "vari_nombre = '" + vari.ToString() + "' AND rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '" + ri["tbl_clave"] + "'";

                                foreach (DataRow ri2 in uniqueRows.Select(cadena_busqueda))//"vari_nombre = '" + vari.ToString() + "'"
                                {
                                    foreach (DataRow rg in dsDatos.Tables["det_quejas"].Select("rch_clave = '" + ri["rch_clave"] + "' AND tbl_clave = '" + ri["tbl_clave"] + "' AND qud_ordprod = '" + ri2["hrp_recibo"] + "'"))
                                    {
                                        string p_r_o = rg["pro_nombre"].ToString();
                                        bool encontrado = false;
                                        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                        {
                                            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(rg["qud_cantrecha"]);
                                            encontrado = true;
                                        }
                                        if (encontrado == false)
                                        {
                                            dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                        }
                                        //dtReg.Rows.Add(rg["pro_nombre"].ToString(), rg["qud_cantrecha"].ToString());
                                    }
                                    //query = "SELECT SUM(A.qud_cantrecha) AS qud_cantrecha, RTRIM(B.pro_nombre) AS pro_nombre, " +
                                    //"(CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS qud_variedad " +
                                    //"FROM tb_det_quejas A JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                                    //"JOIN tb_cat_problemas B ON B.pro_clave = A.qud_problema " +
                                    //"WHERE A.qud_ordprod = '" + ri2["hrp_recibo"] + "' and C.que_status in ('A', 'T') /*AND A.id_producto = ' + ri[prod_clave].ToString() + '*/ " +
                                    //"AND A.rch_clave = '" + ri["rch_clave"] + "' AND A.tbl_clave = '" + ri["tbl_clave"] + "'" +
                                    //"GROUP BY B.pro_nombre, A.qud_variedad " +
                                    //"ORDER BY A.qud_variedad";

                                    //adapter = new SqlDataAdapter(query, conn);

                                    //foreach (DataTable dt in dsDatos.Tables)
                                    //{
                                    //    if (dt.TableName == "quejas")
                                    //    {
                                    //        dsDatos.Tables["quejas"].Clear();
                                    //    }
                                    //}

                                    //adapter.Fill(dsDatos, "quejas");

                                    //if (dtReg.Rows.Count == 0)
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    foreach (DataRow re in dsDatos.Tables["quejas"].Rows)
                                    //    {
                                    //        string p_r_o = re["pro_nombre"].ToString();
                                    //        bool encontrado = false;
                                    //        foreach (DataRow re2 in dtReg.Select("problem = '" + p_r_o + "'"))
                                    //        {
                                    //            re2["rejected"] = Convert.ToDecimal(re2["rejected"]) + Convert.ToDecimal(re["qud_cantrecha"]);
                                    //            encontrado = true;
                                    //        }
                                    //        if (encontrado == false)
                                    //        {
                                    //            dtReg.Rows.Add(re["pro_nombre"].ToString(), re["qud_cantrecha"].ToString());
                                    //        }
                                    //    }

                                    //}

                                }

                                int z = 0;
                                foreach (DataRow re2 in dtReg.Rows)
                                {
                                    rw = dtPrincipal.NewRow();
                                    rw["variedad"] = vari.ToString();

                                    rw["rancho"] = ri["rch_nombre"];
                                    rw["tabla"] = ri["tbl_nombre"];
                                    
                                    rw["problema"] = re2["problem"].ToString();
                                    rw["rechazado"] = re2["rejected"].ToString();
                                    rw["porcentaje"] = Math.Round((Convert.ToDecimal(re2["rejected"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(suma_tbl_rch), 2);
                                    dtPrincipal.Rows.Add(rw);

                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = re2["problem"].ToString();
                                    rw2["rechazado"] = re2["rejected"].ToString();
                                    dtPrincipal2.Rows.Add(rw2);
                                    
                                }

                                if (dtReg.Rows.Count == 0)
                                {
                                    rw2 = dtPrincipal2.NewRow();
                                    rw2["variedad"] = vari;
                                    rw2["rancho"] = ri["rch_nombre"];
                                    rw2["tabla"] = ri["tbl_nombre"];
                                    rw2["producido"] = suma_tbl_rch;
                                    rw2["problema"] = "SIN_RECHAZO";
                                    rw2["rechazado"] = "0";
                                    dtPrincipal2.Rows.Add(rw2);
                                }
                                
                            }
                        }
                    }

                    rancho_ant = rancho_act;
                    tabla_ant = tabla_act;
                    tbl_nom_ant = tbl_nom_act;
                    
                }
            }
            conn.Close();

            

            foreach (DataRow rt in dtPrincipal.Select("variedad <> '' AND producido > 0"))
            {
                rt["rechazado"] = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") == rt["variedad"].ToString() && r.Field<string>("problema") != "").Sum(r => r.Field<decimal?>("rechazado"))).ToString();
                rt["porcentaje"] = ((Convert.ToDecimal(rt["rechazado"]) * Convert.ToDecimal("100")) / Convert.ToDecimal(rt["producido"])).ToString("#0.00");
            }

            foreach (DataRow rt in dtPrincipal.Rows)
            {
                dtPrincipal3.Rows.Add(rt.ItemArray);
            }

            Session["principal2"] = dtPrincipal3;

            //string v_a_r_act = "";
            //string v_a_r_ant = "";
            //foreach (DataRow rt in dtPrincipal.Select("variedad <> '' AND producido > 0"))
            //{
            //    v_a_r_ant = rt["variedad"].ToString();
            //    foreach (DataRow rt2 in dtPrincipal.Select("variedad <> '' AND producido > 0"))
            //    {
            //        if()
            //    }
            //    v_a_r_ant = v_a_r_act;
            //}

            string html = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive w-auto small'>" +
                "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>Variedad</font></th><th bgcolor='#337ab7'><font color='#fff'>Rancho</font></th><th bgcolor='#337ab7'><font color='#fff'>Tabla</font></th><th bgcolor='#337ab7'><font color='#fff'>Recibido</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th><th bgcolor='#337ab7'><font color='#fff'>Rechazado</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Porcentaje</font></th></tr></thead><tbody>";

            //Totales generales
            //string total_producido = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && r.Field<decimal?>("producido") > 0).Sum(r => r.Field<decimal?>("producido"))).ToString();
            //string total_producido = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && (r.Field<string>("rancho") == "" && r.Field<string>("tabla") == "")).Sum(r => r.Field<decimal?>("producido"))).ToString();
            //string total_rechazado = (dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") == "" && r.Field<decimal?>("producido") == 0).Sum(r => r.Field<decimal?>("rechazado"))).ToString();
            //(dtPrincipal.AsEnumerable().Where(r => r.Field<string>("variedad") != "" && r.Field<string>("problema") != "").Sum(r => r.Field<decimal?>("rechazado"))).ToString();
            decimal total_producido = 0;
            decimal total_rechazado = 0;

            foreach (DataRow rz in dtPrincipal.Select("variedad <> '' AND producido <> 0"))
                total_producido = total_producido + Convert.ToDecimal(rz["producido"]);
            foreach (DataRow rz in dtPrincipal.Select("problema <> ''"))
                total_rechazado = total_rechazado + Convert.ToDecimal(rz["rechazado"]);

            string total_porcentaje = "";
            if (Convert.ToDecimal(total_producido) == 0)
            {
                total_porcentaje = Convert.ToDecimal(total_producido).ToString("##0.00");
            }
            else
            {
                total_porcentaje = ((Convert.ToDecimal(total_rechazado) * Convert.ToDecimal("100")) / Convert.ToDecimal(total_producido)).ToString("##0.00");
            }

            html += "<tr><td><b>Totales Generales</b></td><td></td><td></td><td align='right'><b>" + Convert.ToDecimal(total_producido).ToString("###,###,##0") + "</b></td><td></td><td align='right'><b>" + Convert.ToDecimal(total_rechazado).ToString("###,###,##0") + "</b></td><td align='right'><b>" + total_porcentaje + " %</b></td></tr>";

            string var_act = "";
            string var_ant = "";
            foreach (DataRow ra in dtPrincipal.Rows)
            {
                var_act = ra["variedad"].ToString();
                //if (var_act != var_ant)
                //    html += "<tr><td><b><a onclick='recibido_variedad(\"" + ra["variedad"] + "\")' style='color:black'>" + ra["variedad"] + "</a></b></td><td align='right'><b>" + ra["rancho"].ToString() + "</b></td><td align='right'><b>" + ra["tabla"].ToString() + "</b></td><td align='right'><b>" + Convert.ToDecimal(ra["producido"].ToString()).ToString("###,###,##0") + "</b></td><td></td><td align='right'><b>" + ((ra["rechazado"].ToString() != "") ? Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") : "0") + "</b></td><td align='right'><b>" + ((ra["porcentaje"].ToString() != "") ? Convert.ToDecimal(ra["porcentaje"].ToString()).ToString("##0.00") : "0") + " %</b></td></tr>";
                //else
                //    html += "<tr><td></td><td></td><td></td><td></td><td><a onclick='rechazado_problema(\"" + ra["variedad"] + "\", \"" + ra["problema"] + "\")' style='color:black'>" + ra["problema"] + "</a></td><td align='right'>" + ((ra["rechazado"].ToString() != "") ? Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") : "0") + "</td><td></td></tr>";
                if (var_act != var_ant && ra["problema"].ToString() == "")
                {
                    string total_porcentaje2 = "0";
                    if (ra["rechazado"].ToString() != "")
                    {
                        total_porcentaje2 = ((Convert.ToDecimal(ra["rechazado"].ToString()) * Convert.ToDecimal("100")) / Convert.ToDecimal(ra["producido"].ToString())).ToString("##0.00");
                    }
                    html += "<tr><td align='right'><b>" + ra["variedad"].ToString() + "</b></td><td align='right'><b>" + ra["rancho"].ToString() + "</b></td><td align='right'><b>" + ra["tabla"].ToString() + "</b></td><td align='right'><b>" + ((ra["producido"].ToString() != "") ? Convert.ToDecimal(ra["producido"].ToString()).ToString("###,###,##0") : "") + "</b></td><td align='right'><b>" + ra["problema"].ToString() + "</b></td><td align='right'><b>" + ((ra["rechazado"].ToString() != "") ? Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") : "") + "</b></td><td align='right'><b>" + ((ra["porcentaje"].ToString() != "") ? ra["porcentaje"].ToString() + "%" : "") + "</b></td></tr>";//<td align='right'><b>" + ra["rechazado"].ToString() + "</b></td><td align='right'><b>" + ra["porcentaje"].ToString() + "</b></td>
                }
                    
                else
                    html += "<tr><td></td><td align='right'><b>" + ra["rancho"].ToString() + "</b></td><td align='right'><b>" + ra["tabla"].ToString() + "</b></td><td align='right'><b>" + ((ra["producido"].ToString() != "") ? Convert.ToDecimal(ra["producido"].ToString()).ToString("###,###,##0") : "") + "</b></td><td align='right'><b><i>" + ra["problema"].ToString() + "</i></b></td><td align='right'><b><i>" + ((ra["rechazado"].ToString() != "") ? Convert.ToDecimal(ra["rechazado"].ToString()).ToString("###,###,##0") : "") + "</i></b></td><td align='right'><b><i>" + ((ra["porcentaje"].ToString() != "") ? ra["porcentaje"].ToString() + "%" : "") + "</i></b></td></tr>";
                var_ant = var_act;

            }

            html += "</tbody></table>";

            this.datosqueja.InnerHtml = html;
        }

        //public void reporte_tablas_3()
        //{
        //    DataTable dtDinamica = new DataTable();
        //    dtDinamica = (DataTable)Session["principal2"];

        //    string html = "<table border='1' id='datatable2' class='table table-striped table-bordered table-hover table-responsive w-auto small'>" +
        //        "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>Variedad</font></th><th bgcolor='#337ab7'><font color='#fff'>Rancho</font></th><th bgcolor='#337ab7'><font color='#fff'>Tabla</font></th><th bgcolor='#337ab7'><font color='#fff'>Recibido</font></th>" +
        //        "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th><th bgcolor='#337ab7'><font color='#fff'>Rechazado</font></th></tr></thead><tbody>";
        //    foreach (DataRow ra in dtDinamica.Rows)
        //    {
        //        html += "<tr><td>" + ra["variedad"] + "</td><td>" + ra["rancho"] + "</td><td>" + ra["tabla"] + "</td><td align='right'>" + Convert.ToDecimal(ra["producido"]).ToString("###,###,##0") + "</td><td>" + ra["problema"] + "</td><td align='right'>" + Convert.ToDecimal(ra["rechazado"]).ToString("###,###,##0") + "</td></tr>";
        //    }
        //    html += "</tbody></table>";

        //    this.datosqueja2.InnerHtml = html;
        //}

        [WebMethod]
        public static string funcion()
        {
            return "hola";
        }

        [WebMethod]
        public static string recibido_variedad(string vari)
        {
            producidas data = new producidas();
            DataTable dtRecibidas = data.recibido_variedad_datos(vari);

            string total_producido = (dtRecibidas.AsEnumerable().Sum(r => r.Field<decimal?>("num_cajas"))).ToString();

            string html = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive w-auto small'>" +
                "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>Clave</font></th><th bgcolor='#337ab7'><font color='#fff'>Producto</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Variedad</font></th><th bgcolor='#337ab7'><font color='#fff'>Cajas</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Rechazadas</font></th></tr></thead><tbody>";

            foreach (DataRow rw in dtRecibidas.Rows)
            {
                html += "<tr><td>" + rw["prod_clave"] + "</td><td>" + rw["prod_nombre"] + "</td><td>" + rw["vari_nombre"] + "</td><td align='right'>" + Convert.ToDecimal(rw["num_cajas"]).ToString("###,##0") + "</td><td align='right'>" + Convert.ToDecimal(rw["rechazadas"]).ToString("###,##0") + "</td></tr>";
            }

            html += "<tr><td></td><td></td><td><b>Total</b></td><td align='right'><b>" + Convert.ToDecimal(total_producido).ToString("###,##0") + "</b></td></tr>";

            html += "</tbody></table>";
            return html;
        }

        public DataTable recibido_variedad_datos(string vari)
        {
            DataTable dtRecibidas = (DataTable)Session["historico2"];
            DataTable dtRechazadas = (DataTable)Session["historico3"];
            DataView dw = dtRecibidas.DefaultView;
            dw.RowFilter = "vari_nombre = '" + vari + "'";
            dtRecibidas = dw.ToTable();

            //agregar columna
            dtRecibidas.Columns.Add("rechazadas", typeof(string));

            foreach (DataRow rt in dtRecibidas.Rows)
            {
                string prod = rt["prod_clave"].ToString();
                string rech = "0";
                foreach (DataRow rt1 in dtRechazadas.Select("vari_nombre = '" + vari.ToString() + "' AND id_producto = '" + prod + "'"))
                {
                    rech = rt1["qud_cantrecha"].ToString();
                }
                rt["rechazadas"] = rech;
                //(dtRechazadas.AsEnumerable().Where(r => r.Field<string>("vari_nombre") == vari.ToString() && r.Field<string>("id_producto") == prod).Select(v => v.Field<string>("qud_cantrecha")));
            }

            return dtRecibidas;
        }

        [WebMethod]
        public static string rechazado_problema(string vari, string prob, string fecha1)
        {
            producidas data = new producidas();
            DataTable dtRecibidas = data.rechazo_variedad_datos(vari, prob, fecha1);

            string html = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive w-auto small'>" +
                "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>Clave</font></th><th bgcolor='#337ab7'><font color='#fff'>Producto</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th><th bgcolor='#337ab7'><font color='#fff'>Variedad</font></th>" +
                "<th bgcolor='#337ab7'><font color='#fff'>Tabla</font></th><th bgcolor='#337ab7'><font color='#fff'>Rechazado</font></th>" +
                "</tr></thead><tbody>";

            //string total_producido = (dtRecibidas.AsEnumerable().Sum(r => r.Field<decimal>("qud_cantrecha"))).ToString();

            decimal total_producido = 0;
            foreach (DataRow rw in dtRecibidas.Rows)
            {
                html += "<tr><td>" + rw["id_producto"] + "</td><td>" + rw["nom_producto"] + "</td><td>" + rw["pro_nombre"] + "</td><td>" + rw["vari_nombre"] + "</td>" +
                    "<td>" + rw["tbl_nombre"] + "</td><td align='right'>" + Convert.ToDecimal(rw["qud_cantrecha"]).ToString("###,##0") + "</td></tr>";
                total_producido = total_producido + Convert.ToDecimal(rw["qud_cantrecha"]);
            }

            html += "<tr><td></td><td></td><td></td><td></td><td><b>Total</b></td><td align='right'><b>" + Convert.ToDecimal(total_producido).ToString("###,##0") + "</b></td></tr>";
            html += "</tbody></table>";
            return html;
        }

        public DataTable rechazo_variedad_datos(string vari, string prob, string fecha1)
        {
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            SqlDataAdapter adapter;
            DataSet dsDatos = new DataSet();

            DataTable dtRecibidas = (DataTable)Session["historico"];

            DataTable uniqueRows = dtRecibidas.DefaultView.ToTable(true, "hrp_recibo", "vari_nombre");

            //DataView dw = dtRecibidas.DefaultView;
            //dw.RowFilter = "vari_nombre = '" + vari + "'";
            //dtRecibidas = dw.ToTable();

            DataView dw = uniqueRows.DefaultView;
            dw.RowFilter = "vari_nombre = '" + vari + "'";
            dtRecibidas = dw.ToTable();

            string query = "";
            DataTable PROBLEMA;
            DataView dw2 = new DataView();

            string f1 = fecha1;

            //CONSULTAR RECIBOS
            foreach (DataRow ri in dtRecibidas.Select("vari_nombre = '" + vari.ToString() + "'"))
            {
                if (ri["vari_nombre"].ToString() == "SIN VARIEDAD")
                {
                    query = "SELECT a.id_producto, a.nom_producto, c.pro_nombre, (CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS vari_nombre," +
                        "SUM(a.qud_cantrecha) AS qud_cantrecha, /*b.tbl_nombre*/" +
                        "(SELECT TOP 1 b.tbl_nombre FROM tb_cat_tablas b where b.tbl_clave = a.tbl_clave AND b.rch_clave = a.rch_clave AND b.prov_clave = a.prov_clave) AS tbl_nombre " +
                        "FROM tb_det_quejas a JOIN tb_mstr_quejas d ON d.que_folio = a.que_folio " +
                        "JOIN tb_cat_problemas c ON c.pro_clave = a.qud_problema " +
                        "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' AND d.que_status in ('A', 'T') AND d.que_fecha >= '" + f1 + "' " +
                        "GROUP BY a.id_producto, a.nom_producto, c.pro_nombre, a.qud_variedad, a.prov_clave, a.rch_clave, a.tbl_clave ORDER BY A.qud_variedad";
                    //query = "SELECT a.id_producto, a.nom_producto, c.pro_nombre, (CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS vari_nombre," +
                    //    "SUM(a.qud_cantrecha) AS qud_cantrecha, b.tbl_nombre FROM tb_det_quejas a JOIN tb_mstr_quejas d ON d.que_folio = a.que_folio" +
                    //    "JOIN tb_cat_problemas c ON c.pro_clave = a.qud_problema " +
                    //    "JOIN tb_cat_tablas b ON b.tbl_clave = a.tbl_clave AND b.rch_clave = a.rch_clave AND a.prov_clave = b.prov_clave " +
                    //    "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' AND d.que_status in ('A', 'T') GROUP BY a.id_producto, a.nom_producto, c.pro_nombre, a.qud_variedad, b.tbl_nombre ORDER BY A.qud_variedad";
                    adapter = new SqlDataAdapter(query, conn);

                    bool fnd = false;
                    foreach (DataTable dt in dsDatos.Tables)
                    {
                        if (dt.TableName == "sinvariedad")
                        {
                            fnd = true;
                            //dsDatos.Tables["sinvariedad"].Clear();
                        }
                    }
                    if (fnd == true)
                    {
                        bool encontrado = false;
                        adapter.Fill(dsDatos, "sinvariedad2");
                        foreach (DataRow rv2 in dsDatos.Tables["sinvariedad2"].Rows)
                        {
                            foreach (DataRow rv3 in dsDatos.Tables["sinvariedad"].Select("id_producto = '" + rv2["id_producto"] + "' AND nom_producto = '" + rv2["nom_producto"] + "' AND pro_nombre = '" + rv2["pro_nombre"] + "' AND vari_nombre = '" + rv2["vari_nombre"] + "' AND tbl_nombre = '" + rv2["tbl_nombre"] + "'"))
                            {
                                //clase.correo_error(query);
                                encontrado = true;
                                rv3["qud_cantrecha"] = (Convert.ToDecimal(rv3["qud_cantrecha"]) + Convert.ToDecimal(rv2["qud_cantrecha"])).ToString();
                            }
                        }
                        dsDatos.Tables["sinvariedad2"].Clear();

                        if(encontrado == false)
                            adapter.Fill(dsDatos, "sinvariedad"); //clase.correo_error(query);
                    }
                    else
                    {
                        //clase.correo_error(query);
                        adapter.Fill(dsDatos, "sinvariedad");
                    }
                    
                    //dw2 = dsDatos.Tables["sin_variedad"].DefaultView;
                    //dw2.RowFilter = "qud_variedad = '" + vari + "' AND pro_nombre = '" + prob + "'";
                    //PROBLEMA = dw2.ToTable();
                }
                else
                {
                    query = "SELECT a.id_producto, a.nom_producto, c.pro_nombre, (CASE WHEN RTRIM(A.qud_variedad) IN ('', NULL) THEN 'SIN VARIEDAD' ELSE RTRIM(A.qud_variedad) END) AS vari_nombre," +
                        "SUM(a.qud_cantrecha) AS qud_cantrecha, /*b.tbl_nombre*/" +
                        "(SELECT TOP 1 b.tbl_nombre FROM tb_cat_tablas b where b.tbl_clave = a.tbl_clave AND b.rch_clave = a.rch_clave AND b.prov_clave = a.prov_clave) AS tbl_nombre " +
                        "FROM tb_det_quejas a JOIN tb_mstr_quejas d ON d.que_folio = a.que_folio " +
                        "JOIN tb_cat_problemas c ON c.pro_clave = a.qud_problema " +
                        "WHERE A.qud_ordprod = '" + ri["hrp_recibo"] + "' AND A.qud_variedad = '" + vari + "' AND d.que_status in ('A', 'T') AND d.que_fecha >= '" + f1 + "' " +
                        "GROUP BY a.id_producto, a.nom_producto, c.pro_nombre, a.qud_variedad, a.prov_clave, a.rch_clave, a.tbl_clave ORDER BY A.qud_variedad";
                    adapter = new SqlDataAdapter(query, conn);
                    //adapter.Fill(dsDatos, "sinvariedad");

                    bool fnd = false;
                    foreach (DataTable dt in dsDatos.Tables)
                    {
                        if (dt.TableName == "sinvariedad")
                        {
                            fnd = true;
                            //dsDatos.Tables["sinvariedad"].Clear();
                        }
                    }
                    if (fnd == true)
                    {
                        bool encontrado = false;
                        adapter.Fill(dsDatos, "sinvariedad2");
                        foreach (DataRow rv2 in dsDatos.Tables["sinvariedad2"].Rows)
                        {
                            foreach (DataRow rv3 in dsDatos.Tables["sinvariedad"].Select("id_producto = '" + rv2["id_producto"] + "' AND nom_producto = '" + rv2["nom_producto"] + "' AND pro_nombre = '" + rv2["pro_nombre"] + "' AND vari_nombre = '" + rv2["vari_nombre"] + "' AND tbl_nombre = '" + rv2["tbl_nombre"] + "'"))
                            {
                                encontrado = true;
                                rv3["qud_cantrecha"] = (Convert.ToDecimal(rv3["qud_cantrecha"]) + Convert.ToDecimal(rv2["qud_cantrecha"])).ToString();
                            }
                        }
                        dsDatos.Tables["sinvariedad2"].Clear();

                        if (encontrado == false)
                            adapter.Fill(dsDatos, "sinvariedad");
                    }
                    else
                    {
                        adapter.Fill(dsDatos, "sinvariedad");
                    }

                    //conn.Open();
                    //SqlCommand cmnd = conn.CreateCommand();
                    //cmnd.CommandText = query;
                    //SqlDataReader reader1 = cmnd.ExecuteReader();
                    //if (reader1.HasRows)
                    //{
                    //    clase.correo_error(query);
                    //}
                    //reader1.Close();
                    //reader1.Dispose();
                    //cmnd.Dispose();
                    //conn.Close();

                    //dw2 = dsDatos.Tables["sin_variedad"].DefaultView;
                    //PROBLEMA = dw2.ToTable();
                }

                
                
            }

            if (vari == "SIN VARIEDAD")
            {
                dw2 = dsDatos.Tables["sinvariedad"].DefaultView;
                dw2.RowFilter = "vari_nombre = '" + vari + "' AND pro_nombre = '" + prob + "'";
                PROBLEMA = dw2.ToTable();
            }
            else
            {
                dw2 = dsDatos.Tables["sinvariedad"].DefaultView;
                dw2.RowFilter = "vari_nombre = '" + vari + "' AND pro_nombre = '" + prob + "'";
                PROBLEMA = dw2.ToTable();
            }

            return PROBLEMA;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("reportes.aspx");
        }

        protected void btnTabla_Click(object sender, EventArgs e)
        {
            
            
            DataTable dataTable = new DataTable();
            dataTable = (DataTable)Session["principal2"];



            string lin = "Reporte Producido contra Rechazado de la linea : " + ddlQuejas.SelectedItem.ToString();
            string f_1 = "Periodo del reporte del " + txtFechaInicio.Text + " al " + txtFechaFin.Text;

            decimal total_producido = 0;
            decimal total_rechazado = 0;

            foreach (DataRow rz in dataTable.Select("variedad <> '' AND producido <> ''"))
                total_producido = total_producido + Convert.ToDecimal(rz["producido"]);
            foreach (DataRow rz in dataTable.Select("problema <> ''"))
                total_rechazado = total_rechazado + Convert.ToDecimal(rz["rechazado"]);
            string total_porcentaje = "";
            if (Convert.ToDecimal(total_producido) == 0)
            {
                total_porcentaje = Convert.ToDecimal(total_producido).ToString("##0.00");
            }
            else
            {
                total_porcentaje = ((Convert.ToDecimal(total_rechazado) * Convert.ToDecimal("100")) / Convert.ToDecimal(total_producido)).ToString("##0.00");
            }


            DataRow rw_cero;
            rw_cero = dataTable.NewRow();
            rw_cero["variedad"] = lin;
            dataTable.Rows.InsertAt(rw_cero, 0);
            rw_cero = dataTable.NewRow();
            rw_cero["variedad"] = f_1;
            dataTable.Rows.InsertAt(rw_cero, 1);
            rw_cero = dataTable.NewRow();
            rw_cero["variedad"] = "VARIEDAD";
            rw_cero["rancho"] = "RANCHO";
            rw_cero["tabla"] = "TABLA";
            rw_cero["producido"] = "PRODUCIDO";
            rw_cero["problema"] = "PROBLEMA";
            rw_cero["rechazado"] = "RECHAZADO";
            rw_cero["porcentaje"] = "PORCENTAJE";
            rw_cero["porc_var"] = "% VARIEDAD";
            dataTable.Rows.InsertAt(rw_cero, 2);

            

            rw_cero = dataTable.NewRow();
            rw_cero["variedad"] = "Totales Generales";
            rw_cero["producido"] = total_producido.ToString("###,###,##0");
            rw_cero["rechazado"] = total_rechazado.ToString("###,###,##0");
            rw_cero["porcentaje"] = Convert.ToDecimal(total_porcentaje).ToString("##0.00") + "%";
            dataTable.Rows.InsertAt(rw_cero, 3);

            

            
            int l = 0;
            foreach (DataRow ri in dataTable.Rows)
            {
                if (l <= 3)
                {
                    l++;
                    continue;
                }
                    
                if (ri["variedad"].ToString() != "" && ri["problema"].ToString() != "")
                {
                    ri["variedad"] = "";
                }

                if (ri["porcentaje"].ToString() != "" && ri["variedad"].ToString() != "")
                {
                    ri["porc_var"] = ri["porcentaje"] + "%";
                    ri["porcentaje"] = "";
                }

                if (ri["porcentaje"].ToString() != "")
                {
                    ri["porcentaje"] = ri["porcentaje"] + "%";
                }

                

                if (ri["producido"].ToString() != "")
                {
                    ri["producido"] = Convert.ToDecimal(ri["producido"].ToString()).ToString("###,###,##0");
                }

                if (ri["rechazado"].ToString() != "")
                {
                    ri["rechazado"] = Convert.ToDecimal(ri["rechazado"].ToString()).ToString("###,###,##0");
                }
                l++;
            }

            int r = 0;
            string nome = "";
            string nome_ant = "";
            foreach (DataRow ry in dataTable.Rows)
            {
                if (r < 4)
                {
                    r++;
                    continue;
                }
                nome = ry["variedad"].ToString();
                if (nome == "")
                {
                    ry["variedad"] = nome_ant;
                }
                else
                {
                    nome_ant = nome;
                }
                r++;
            }
            

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=ProducidasVsRechazadasTablaDinamica.xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            response.ContentEncoding = Encoding.Unicode;
            response.BinaryWrite(Encoding.Unicode.GetPreamble());


            //using (StringWriter stringWriter = new StringWriter())
            //{
            //    using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
            //    {
            //        DataGrid dataGrid = new DataGrid();
            //        dataGrid.DataSource = (object)dataTable;
            //        dataGrid.DataBind();
            //        dataGrid.ShowHeader = false;
            //        dataGrid.RenderControl(writer);
            //        this.Response.Write(stringWriter.ToString());
            //        dataGrid.Dispose();
            //        this.Response.End();
            //    }
            //}

            //Set Fonts
            //Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            Response.Write("<BR><BR><BR>");

            //Sets the table border, cell spacing, border color, font of the text, background,
            //foreground, font height
            Response.Write("<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> ");//<TR>

            // Check not to increase number of records more than 65k according to excel,03
            if (dataTable.Rows.Count <= 65536)
            {

                //// Get DataTable Column's Header
                //foreach (DataColumn column in dataTable.Columns)
                //{
                //    //Write in new column
                //    Response.Write("<Td>");

                //    //Get column headers  and make it as bold in excel columns
                //    Response.Write("<B>");
                //    Response.Write(column);
                //    Response.Write("</B>");
                //    Response.Write("</Td>");
                //}

                //Response.Write("</TR>");

                // Get DataTable Column's Row
                foreach (DataRow rxx in dataTable.Rows)
                {
                    //Write in new row
                    Response.Write("<tr>");

                    for (int i = 0; i <= dataTable.Columns.Count - 1; i++)
                    {
                        if (rxx[0].ToString().Contains("Reporte Producido contra Rechazado de la linea"))
                        {
                            Response.Write("<td><b>");
                            Response.Write(rxx[i].ToString());
                            Response.Write("</b></td>");
                        }
                        else if (rxx[0].ToString().Contains("Periodo del reporte"))
                        {
                            Response.Write("<td><b>");
                            Response.Write(rxx[i].ToString());
                            Response.Write("</b></td>");
                        }
                        else if (rxx[0].ToString().Contains("VARIEDAD"))
                        {
                            Response.Write("<td><b>");
                            Response.Write(rxx[i].ToString());
                            Response.Write("</b></td>");
                        }
                        else if (rxx[0].ToString().Contains("Totales Generales"))
                        {
                            Response.Write("<td><b>");
                            Response.Write(rxx[i].ToString());
                            Response.Write("</b></td>");
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write(rxx[i].ToString());
                            Response.Write("</td>");
                        }
                        
                    }

                    Response.Write("</tr>");
                }
            }

            Response.Write("</table>");
            //Response.Write("</font>");
            Response.Flush();
            Response.End();
        }


    }
}