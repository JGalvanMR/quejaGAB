using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

namespace queja
{
    public partial class scorecard : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtTransportistas = new DataTable();
        private DataTable dtProblemas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();

            if (this.Page.IsPostBack)
                return;

            dtTransportistas = con.score_card_transportistas_combo();
            DataRow row = dtTransportistas.NewRow();
            row["que_transportista"] = "";
            row["prov_nombre"] = "SELECCIONA TRANSPORTISTA...";
            dtTransportistas.Rows.InsertAt(row, 0);
            row = dtTransportistas.NewRow();
            row["que_transportista"] = "0";
            row["prov_nombre"] = "TODOS";
            dtTransportistas.Rows.InsertAt(row, 1);
            ddlTransportistas.DataSource = dtTransportistas;
            ddlTransportistas.DataTextField = "prov_nombre";
            ddlTransportistas.DataValueField = "que_transportista";
            ddlTransportistas.DataBind();
            udpTransportistas.Update();
        }

        protected void btnBuscaOrden_Click(object sender, EventArgs e)
        {
            if (ddlTransportistas.SelectedValue != "")
            {
                string ddlTrans = ddlTransportistas.SelectedValue.ToString();
                string fechaini = txtFecha.Text;
                string fechafin = txtFechaFin.Text;
                string ddlAre = ddlArea.SelectedValue.ToString();
                //style="min-width: 310px; height: 400px; margin: 0 auto"
                //DataTable tbMaestro = con.score_card_transportistas_mstr(ddlTransportistas.SelectedValue.ToString());
                //DataTable tbDetalle = con.score_card_transportistas_det(ddlTransportistas.SelectedValue.ToString());

                ////TOTAL DE PERDIDAS SEGUN FECHAS SELECCIONADAS USD Y MN
                DataTable totalperdidas = con.score_card_total_perdidas(ddlTrans, fechaini, fechafin, ddlAre);
                string perdidas = "<div class='col-md-4'>" +
                                    "<div class='stati bg-belize_hole'>" +
                                        "<i class='glyphicon glyphicon-usd'></i>" +
                                        "<div>" +
                                            "<b>" + Convert.ToDecimal(totalperdidas.Rows[0][0].ToString()).ToString("###,##0.00") + "</b>" +
                                            "<span>Perdida MN</span>" +
                                        "</div>" +
                                    "</div>" +
                                    "</div>" +
                                    "<div class='col-md-4'>" +
                                        "<div class='stati bg-belize_hole'>" +
                                            "<i class='glyphicon glyphicon-usd'></i>" +
                                            "<div>" +
                                                "<b>" + Convert.ToDecimal(totalperdidas.Rows[0][1].ToString()).ToString("###,##0.00") + "</b>" +
                                                "<span>Perdida USD</span>" +
                                            "</div>" +
                                        "</div>" +
                                    "</div>";
                this.perdida.InnerHtml = perdidas;

                //EMBARQUES TOTALES Y QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS NACIONAL
                DataTable tbTotales = con.score_card_embarques_quejas_totales(ddlTrans, fechaini, fechafin, ddlAre);

                //DataTable dtPlacas = con.score_card_embarques_quejas_totales_caja_nacional(ddlTrans, fechaini, fechafin);

                DataView tbView = tbTotales.DefaultView;
                tbView.Sort = "prov_nombre";
                tbTotales = tbView.ToTable();

                tbTotales.Columns.Add("MN", typeof(string));
                tbTotales.Columns.Add("USD", typeof(string));

                //detalle_perdida_transportista_principal
                foreach (DataRow ri in tbTotales.Rows)
                {
                    DataTable dtPerd = detalle_perdida_transportista_principal(ri["prov_clave"].ToString(), fechaini, fechafin, ddlAre);
                    ri["MN"] = Convert.ToDecimal(dtPerd.Rows[0][0].ToString()).ToString("###,###,##0.00");
                    ri["USD"] = Convert.ToDecimal(dtPerd.Rows[0][1].ToString()).ToString("###,###,##0.00");
                }

                string sum_tot_nom = "TOTAL";
                Decimal sum_tot_tot = 0;
                Decimal sum_tot_que = 0;
                Decimal sum_tot_per_mn = 0;
                Decimal sum_tot_per_usd = 0;
                foreach (DataRow ri in tbTotales.Rows)
                {
                    sum_tot_tot = sum_tot_tot + Convert.ToInt32(ri["total"].ToString());
                    sum_tot_que = sum_tot_que + Convert.ToInt32(ri["total_quejas"].ToString());

                    sum_tot_per_mn = sum_tot_per_mn + Convert.ToDecimal(ri["MN"].ToString());
                    sum_tot_per_usd = sum_tot_per_usd + Convert.ToDecimal(ri["USD"].ToString());
                }
                decimal porcentaje = (sum_tot_tot == 0) ? 0 : Math.Round(100 - ((sum_tot_que * 100) / sum_tot_tot), 1);

                DataRow r = tbTotales.NewRow();
                r["prov_clave"] = "";
                r["prov_nombre"] = sum_tot_nom;
                r["total"] = Convert.ToInt32(sum_tot_tot.ToString());
                r["total_quejas"] = Convert.ToInt32(sum_tot_que.ToString());
                r["total_porcen"] = Convert.ToDecimal(porcentaje.ToString());
                r["MN"] = Convert.ToDecimal(sum_tot_per_mn.ToString());
                r["USD"] = Convert.ToDecimal(sum_tot_per_usd.ToString());
                tbTotales.Rows.InsertAt(r, 0);

                this.Session["tbTotales"] = tbTotales;
                DataTable dtTransTotales = new DataTable();
                string tabla = "<table border='1' id='datatable' name='datatable' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th></th><th>Transportista</th><th>Embarques</th><th>Quejas</th><th>Porcentaje</th><th>MN</th><th>USD</th></tr></thead><tbody>";
                int i = 1;
                foreach (DataRow rw in tbTotales.Rows)
                {
                    if (rw["prov_nombre"].ToString() == "TOTAL")
                    {
                        tabla += "<tr><td align='center'></td><td><b>" + rw["prov_nombre"].ToString().Trim() + "</b></td><td align='right'><b>" + rw["total"].ToString().Trim() + "</b></td><td align='right'><b>" + rw["total_quejas"].ToString().Trim() + "</b></td><td align='right'><b>" + rw["total_porcen"].ToString().Trim() + "%</b></td><td align='right'><b>$" + rw["MN"].ToString().Trim() + "</b></td><td align='right'><b>$" + rw["USD"].ToString().Trim() + "</b></td></tr>";
                    }
                    else
                    {
                        if (Convert.ToInt32(rw["total_quejas"].ToString().Trim()) > 0)
                            tabla += "<tr data-toggle='collapse' data-target='#demo" + i.ToString() + "' class='accordion-toggle'><td align='center'><button type='button' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-eye-open'></span></button></td><td>" + rw["prov_nombre"].ToString().Trim() + "</td><td align='right'>" + rw["total"].ToString().Trim() + "</td><td align='right'>" + rw["total_quejas"].ToString().Trim() + "</td><td align='right'>" + rw["total_porcen"].ToString().Trim() + "%</td><td align='right'>$" + rw["MN"].ToString().Trim() + "</td><td align='right'>$" + rw["USD"].ToString().Trim() + "</td></tr><tr><td colspan='7' class='hiddenRow'><center><div id='demo" + i.ToString() + "' class='accordian-body collapse'>" + detalle_perdida_transportista(rw["prov_clave"].ToString().Trim(), fechaini, fechafin, ddlAre) + detalle_placas(rw["prov_clave"].ToString().Trim(), fechaini, fechafin,ddlAre) + "</div></center></td></tr>";
                        else
                            tabla += "<tr data-toggle='collapse' data-target='#demo" + i.ToString() + "' class='accordion-toggle'><td align='center'><button type='button' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-eye-open'></span></button></td><td>" + rw["prov_nombre"].ToString().Trim() + "</td><td align='right'>" + rw["total"].ToString().Trim() + "</td><td align='right'>" + rw["total_quejas"].ToString().Trim() + "</td><td align='right'>100%</td><td align='right'>$" + rw["MN"].ToString().Trim() + "</td><td align='right'>$" + rw["USD"].ToString().Trim() + "</td></tr><tr><td colspan='7' class='hiddenRow'><center><div id='demo" + i.ToString() + "'class='accordian-body collapse'>" + detalle_perdida_transportista(rw["prov_clave"].ToString().Trim(), fechaini, fechafin, ddlAre) + detalle_placas(rw["prov_clave"].ToString().Trim(), fechaini, fechafin, ddlAre) + "</div></center></td></tr>";
                        i++;
                    }
                        
                    
                }
                tabla += "</tbody></table>";
                this.datosqueja.InnerHtml = tabla;

                

                #region no_uso
                ////EMBARQUES TOTALES Y QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS EXPORTACION
                //DataTable tbTotales2 = con.score_card_embarques_quejas_totales_exp(ddlTransportistas.SelectedValue.ToString(), fechaini, fechafin);
                //this.Session["tbTotales2"] = tbTotales2;
                //string tabla2 = "<table border='1' id='datatable2' name='datatable2' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Transportista</th><th>Total Embarques</th><th>Total Quejas</th></tr></thead><tbody>";
                //foreach (DataRow rw in tbTotales2.Rows)
                //{
                //    if (Convert.ToInt32(rw["total_quejas"]) > 0)
                //        tabla2 += "<tr><td>" + rw["prov_nombre"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                //}
                //tabla2 += "</tbody></table>";
                //this.datosqueja2.InnerHtml = tabla2;

                

                //////QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS NACIONAL
                ////DataTable tbTotalesMesAnt = con.score_card_embarques_quejas_totales_mes_anterior_nacional(ddlTransportistas.SelectedValue.ToString(), fechaini);
                ////this.Session["tbTotales3"] = tbTotalesMesAnt;
                ////string tabla3 = "<table border='1' id='datatable3' name='datatable3' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Transportista</th><th>Total Embarques</th><th>Total Quejas</th></tr></thead><tbody>";
                ////foreach (DataRow rw in tbTotalesMesAnt.Rows)
                ////{
                ////    if (Convert.ToInt32(rw["total_quejas"]) > 0)
                ////        tabla3 += "<tr><td>" + rw["prov_nombre"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                ////}
                ////tabla3 += "</tbody></table>";
                ////this.datosqueja3.InnerHtml = tabla3;

                //////QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS NACIONAL
                ////DataTable tbTotalesMesAntE = con.score_card_embarques_quejas_totales_mes_anterior_exportacion(ddlTransportistas.SelectedValue.ToString(), fechaini);
                ////this.Session["tbTotales4"] = tbTotalesMesAntE;
                ////string tabla4 = "<table border='1' id='datatable4' name='datatable4' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Transportista</th><th>Total Embarques</th><th>Total Quejas</th></tr></thead><tbody>";
                ////foreach (DataRow rw in tbTotalesMesAntE.Rows)
                ////{
                ////    if (Convert.ToInt32(rw["total_quejas"]) > 0)
                ////        tabla4 += "<tr><td>" + rw["prov_nombre"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                ////}
                ////tabla4 += "</tbody></table>";
                ////this.datosqueja4.InnerHtml = tabla4;

                //////QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS NACIONAL
                ////DataTable tbTotalesMesAct = con.score_card_embarques_quejas_totales_mes_actual_nacional(ddlTransportistas.SelectedValue.ToString(), fechaini);
                ////this.Session["tbTotales5"] = tbTotalesMesAct;
                ////string tabla5 = "<table border='1' id='datatable5' name='datatable5' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Transportista</th><th>Total Embarques</th><th>Total Quejas</th></tr></thead><tbody>";
                ////foreach (DataRow rw in tbTotalesMesAct.Rows)
                ////{
                ////    if (Convert.ToInt32(rw["total_quejas"]) > 0)
                ////        tabla5 += "<tr><td>" + rw["prov_nombre"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                ////}
                ////tabla5 += "</tbody></table>";
                ////this.datosqueja5.InnerHtml = tabla5;

                //////QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS NACIONAL
                ////DataTable tbTotalesMesActE = con.score_card_embarques_quejas_totales_mes_actual_exportacion(ddlTransportistas.SelectedValue.ToString(), fechaini);
                ////this.Session["tbTotales6"] = tbTotalesMesActE;
                ////string tabla6 = "<table border='1' id='datatable6' name='datatable6' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Transportista</th><th>Total Embarques</th><th>Total Quejas</th></tr></thead><tbody>";
                ////foreach (DataRow rw in tbTotalesMesActE.Rows)
                ////{
                ////    if (Convert.ToInt32(rw["total_quejas"]) > 0)
                ////        tabla6 += "<tr><td>" + rw["prov_nombre"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                ////}
                ////tabla6 += "</tbody></table>";
                ////this.datosqueja6.InnerHtml = tabla6;



                //////PERDIDAS MES ANTERIOR
                ////DataTable totalperdidas_ant = con.score_card_total_perdidas_mes_anterior(ddlTransportistas.SelectedValue.ToString(), fechaini);
                ////string perdidas_ant = "<div class='col-md-3'>" +
                ////                    "<div class='stati bg-belize_hole'>" +
                ////                        "<i class='glyphicon glyphicon-usd'></i>" +
                ////                        "<div>" +
                ////                            "<b>" + Convert.ToDecimal(totalperdidas_ant.Rows[0][0].ToString()).ToString("###,##0.00") + "</b>" +
                ////                            "<span>Perdida MN</span>" +
                ////                        "</div>" +
                ////                    "</div>" +
                ////                    "</div>" +
                ////                    "<div class='col-md-3'>" +
                ////                        "<div class='stati bg-belize_hole'>" +
                ////                            "<i class='glyphicon glyphicon-usd'></i>" +
                ////                            "<div>" +
                ////                                "<b>" + Convert.ToDecimal(totalperdidas_ant.Rows[0][1].ToString()).ToString("###,##0.00") + "</b>" +
                ////                                "<span>Perdida USD</span>" +
                ////                            "</div>" +
                ////                        "</div>" +
                ////                    "</div>";
                ////this.perdida_ant.InnerHtml = perdidas_ant;

                //////PERDIDAS MES ACTUAL
                ////DataTable totalperdidas_act = con.score_card_total_perdidas_mes_actual(ddlTransportistas.SelectedValue.ToString(), fechaini);
                ////string perdidas_act = "<div class='col-md-3'>" +
                ////                    "<div class='stati bg-belize_hole'>" +
                ////                        "<i class='glyphicon glyphicon-usd'></i>" +
                ////                        "<div>" +
                ////                            "<b>" + Convert.ToDecimal(totalperdidas_act.Rows[0][0].ToString()).ToString("###,##0.00") + "</b>" +
                ////                            "<span>Perdida MN</span>" +
                ////                        "</div>" +
                ////                    "</div>" +
                ////                    "</div>" +
                ////                    "<div class='col-md-3'>" +
                ////                        "<div class='stati bg-belize_hole'>" +
                ////                            "<i class='glyphicon glyphicon-usd'></i>" +
                ////                            "<div>" +
                ////                                "<b>" + Convert.ToDecimal(totalperdidas_act.Rows[0][1].ToString()).ToString("###,##0.00") + "</b>" +
                ////                                "<span>Perdida USD</span>" +
                ////                            "</div>" +
                ////                        "</div>" +
                ////                    "</div>";
                ////this.perdida_act.InnerHtml = perdidas_act;

                ////QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS NACIONAL
                //DataTable tbTotalesPlacaNacional = con.score_card_embarques_quejas_totales_caja_nacional(ddlTransportistas.SelectedValue.ToString(), fechaini, fechafin);
                //this.Session["tbTotales7"] = tbTotalesPlacaNacional;
                //string tabla7 = "<table border='1' id='datatable7' name='datatable7' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Transportista</th><th>Total Embarques</th><th>Total Quejas</th></tr></thead><tbody>";
                //if (ddlTransportistas.SelectedValue.ToString() == "0")
                //{
                //    foreach (DataRow rw in tbTotalesPlacaNacional.Rows)
                //    {
                //        if (Convert.ToInt32(rw["total_quejas"]) > 0)
                //            tabla7 += "<tr><td>" + rw["placacaja"].ToString().Trim() + " - " + rw["prov_nombre"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                //    }
                //}
                //else
                //{
                //    foreach (DataRow rw in tbTotalesPlacaNacional.Rows)
                //    {
                //        if (Convert.ToInt32(rw["total_quejas"]) > 0)
                //            tabla7 += "<tr><td>" + rw["placacaja"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                //    }
                //}
                
                //tabla7 += "</tbody></table>";
                //this.datosqueja7.InnerHtml = tabla7;

                ////QUEJAS TOTALES SEGUN FECHAS SELECCIONADAS EXPORTACION
                //DataTable tbTotalesPlacaExportacion = con.score_card_embarques_quejas_totales_caja_exportacion(ddlTransportistas.SelectedValue.ToString(), fechaini, fechafin);
                //this.Session["tbTotales8"] = tbTotalesPlacaExportacion;
                //string tabla8 = "<table border='1' id='datatable8' name='datatable8' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Transportista</th><th>Total Embarques</th><th>Total Quejas</th></tr></thead><tbody>";
                //if (ddlTransportistas.SelectedValue.ToString() == "0")
                //{
                //    foreach (DataRow rw in tbTotalesPlacaExportacion.Rows)
                //    {
                //        if (Convert.ToInt32(rw["total_quejas"]) > 0)
                //            tabla8 += "<tr><td>" + rw["placacaja"].ToString().Trim() + " - " + rw["prov_nombre"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                //    }
                //}
                //else
                //{
                //    foreach (DataRow rw in tbTotalesPlacaExportacion.Rows)
                //    {
                //        if (Convert.ToInt32(rw["total_quejas"]) > 0)
                //            tabla8 += "<tr><td>" + rw["placacaja"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td><td>" + rw["total_quejas"].ToString().Trim() + "</td></tr>";
                //    }
                //}

                //tabla8 += "</tbody></table>";
                //this.datosqueja8.InnerHtml = tabla8;


                ////QUEJAS POR AREA NACIONAL Y EXPRTACION
                ////transportista, nombre, area (clave), total, area (nombre), tipo(nac-exp)
                //DataTable tbTotalesArea = con.score_card_embarques_quejas_area_nacional_exportacion(ddlTransportistas.SelectedValue.ToString(), fechaini, fechafin);
                //this.Session["tbTotales9"] = tbTotalesArea;
                //string tabla9 = "<table border='1' id='datatable9' name='datatable9' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Area</th><th>Total</th></tr></thead><tbody>";
                //DataView view = new DataView(tbTotalesArea);
                //DataTable dValues = view.ToTable(true, "Area");
                //foreach (DataRow rw in dValues.Rows)
                //{
                //    Int32 sum_tot_area = 0;
                //    foreach (DataRow ra in tbTotalesArea.Select("tipo = 'NACIONAL' AND Area = '" + rw["Area"].ToString() + "'"))
                //        sum_tot_area = sum_tot_area + Convert.ToInt32(ra["total"].ToString());
                //    //Int32 sum_tot_area = tbTotalesArea.AsEnumerable().Where(r => r.Field<string>("tipo") == "NACIONAL" && r.Field<string>("Area") == rw["Area"].ToString()).Sum(r => r.Field<int>("total"));
                //    if(sum_tot_area > 0)
                //        tabla9 += "<tr><td>" + rw["area"].ToString().Trim() + "</td><td>" + sum_tot_area.ToString().Trim() + "</td></tr>";
                //}

                ////foreach (DataRow rw in tbTotalesArea.Select("tipo = 'NACIONAL'"))
                ////{
                ////    Int32 sum_tot_area = tbTotalesArea.AsEnumerable().Where(r => r.Field<string>("tipo") == "NACIONAL").Sum(r => r.Field<Int32>("total"));

                ////    tabla9 += "<tr><td>" + rw["area"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td></tr>";
                ////}

                //tabla9 += "</tbody></table>";
                //this.datosqueja9.InnerHtml = tabla9;

                //string tabla10 = "<table border='1' id='datatable10' name='datatable10' class='table table-striped table-bordered table-hover table-responsive'><thead><tr><th>Area</th><th>Total</th></tr></thead><tbody>";
                ////foreach (DataRow rw in tbTotalesArea.Select("tipo = 'EXPORTACION'"))
                ////{
                ////    tabla10 += "<tr><td>" + rw["area"].ToString().Trim() + "</td><td>" + rw["total"].ToString().Trim() + "</td></tr>";
                ////}
                //view = new DataView(tbTotalesArea);
                //dValues = view.ToTable(true, "Area");
                //foreach (DataRow rw in dValues.Rows)
                //{
                //    Int32 sum_tot_area = 0;
                //    foreach (DataRow ra in tbTotalesArea.Select("tipo = 'EXPORTACION' AND Area = '" + rw["Area"].ToString() + "'"))
                //        sum_tot_area = sum_tot_area + Convert.ToInt32(ra["total"].ToString());
                //    //Int32 sum_tot_area = tbTotalesArea.AsEnumerable().Where(r => r.Field<string>("tipo") == "NACIONAL" && r.Field<string>("Area") == rw["Area"].ToString()).Sum(r => r.Field<int>("total"));
                //    if (sum_tot_area > 0)
                //        tabla10 += "<tr><td>" + rw["area"].ToString().Trim() + "</td><td>" + sum_tot_area.ToString().Trim() + "</td></tr>";
                //}
                //tabla10 += "</tbody></table>";
                //this.datosqueja10.InnerHtml = tabla10;

                #endregion

                //detalle(ddlTransportistas.SelectedValue.ToString(), fechaini, fechafin);

            }


        }

        public string detalle_perdida_transportista(string trans, string f1, string f2, string area)
        {
            //DataTable tbTotalesPlaca = con.score_card_embarques_quejas_totales_caja_nacional(trans, f1, f2);
            DataTable tbPerdidas = con.score_card_total_perdidas(trans, f1, f2, area);
            string perdidas = "<div class='col-md-4 col-lg-offset-2'>" +
                                "<div class='stati bg-belize_hole'>" +
                                    "<i class='glyphicon glyphicon-usd'></i>" +
                                    "<div>" +
                                        "<b>" + Convert.ToDecimal(tbPerdidas.Rows[0][0].ToString()).ToString("###,##0.00") + "</b>" +
                                        "<span>Perdida MN</span>" +
                                    "</div>" +
                                "</div>" +
                                "</div>" +
                                "<div class='col-md-4'>" +
                                    "<div class='stati bg-belize_hole'>" +
                                        "<i class='glyphicon glyphicon-usd'></i>" +
                                        "<div>" +
                                            "<b>" + Convert.ToDecimal(tbPerdidas.Rows[0][1].ToString()).ToString("###,##0.00") + "</b>" +
                                            "<span>Perdida USD</span>" +
                                        "</div>" +
                                    "</div>" +
                                "</div>";
            return perdidas;
        }

        public DataTable detalle_perdida_transportista_principal(string trans, string f1, string f2, string area)
        {
            //DataTable tbTotalesPlaca = con.score_card_embarques_quejas_totales_caja_nacional(trans, f1, f2);
            DataTable tbPerdidas = con.score_card_total_perdidas(trans, f1, f2, area);
            return tbPerdidas;
        }

        public string detalle_placas(string trans, string f1, string f2, string area)
        {
            DataTable tbPlacas = con.score_card_embarques_quejas_totales_caja_nacional(trans, f1, f2, area);
            string placas = "<br /><div class='row col-lg-8 col-lg-offset-2'>" +
                "<table border='1' class='table table-striped table-bordered table-hover' width='250px'>" +
                "<thead><th>Placa</th><th>Emb</th><th>Queja_Nac</th><th>Queja_Exp</th><th>Queja_Tot</th><th>Por</th></thead>" +
                "<tbody>";
            foreach (DataRow ro in tbPlacas.Rows)
            {
                if (ro["placacaja"] == "TOTALES")
                    placas += "<tr><td>" + ro["placacaja"] + "</td><td align='right'>" + ro["registros"] + "</td><td align='right'>" + ro["total"] + "</td><td align='right'>" + ro["total_exp"] + "</td><td align='right'>" + ro["total_quejas"] + "</td><td align='right'>" + ro["total_porcen"] + "%</td></tr>";
                else
                {
                    if (Convert.ToInt32(ro["total_quejas"]) > 0)
                        placas += "<tr><td><a data-toggle='modal' data-target='#myModal' onclick='detalle_pedidos(\"" + trans + "\", \"" + f1 + "\", \"" + f2 + "\", \"" + ro["placacaja"] + "\")'>" + ro["placacaja"] + "</a></td><td align='right'>" + ro["registros"] + "</td><td align='right'>" + ro["total"] + "</td><td align='right'>" + ro["total_exp"] + "</td><td align='right'>" + ro["total_quejas"] + "</td><td align='right'>" + ro["total_porcen"] + "%</td></tr>";
                    else
                        placas += "<tr><td>" + ro["placacaja"] + "</td><td align='right'>" + ro["registros"] + "</td><td align='right'>" + ro["total"] + "</td><td align='right'>" + ro["total_exp"] + "</td><td align='right'>" + ro["total_quejas"] + "</td><td align='right'>" + ro["total_porcen"] + "%</td></tr>";
                }
                
            }
            placas += "</tbody></table></div>";
            return placas;
        }

        //public string detalle_pedidos(string trans, string f1, string f2, string placa)
        //{
        //    DataTable tbPlacas = con.score_card_detalle_pedidos(trans, f1, f2, placa);
        //    string detalle = "<table><thead><th>PEDIDO</th><th>FECHA</th><th>CLIENTE</th><th>SUBCLIENTE</th><th>VENDEDOR</th><th>MONTO</th><th>TIPO</th><th>QUEJAS</th></thead><tbody>";
        //    foreach (DataRow rw in tbPlacas.Select("pdn_tipo = 'NAL'"))
        //    {
        //        detalle += "<tr><td>" + rw["pdn_folio"].ToString().Trim() + "</td><td>" + rw["pdn_fecha"].ToString().Trim() + "</td><td>" + rw["cnte_nombre"].ToString().Trim() + "</td>" +
        //            "<td>" + rw["entr_nombre"].ToString().Trim() + "</td><td>" + rw["vendedor"].ToString().Trim() + "</td><td>" + rw["pdn_monto_total"].ToString().Trim() + "</td>" +
        //            "<td>" + rw["pdn_tipo"].ToString().Trim() + "</td><td>" + rw["quejas"].ToString().Trim() + "</td><tr>";
        //    }
        //    foreach (DataRow rw in tbPlacas.Select("pdn_tipo = 'EXP'"))
        //    {
        //        detalle += "<tr><td>" + rw["pdn_folio"].ToString().Trim() + "</td><td>" + rw["pdn_fecha"].ToString().Trim() + "</td><td>" + rw["cnte_nombre"].ToString().Trim() + "</td>" +
        //            "<td>" + rw["entr_nombre"].ToString().Trim() + "</td><td>" + rw["vendedor"].ToString().Trim() + "</td><td>" + rw["pdn_monto_total"].ToString().Trim() + "</td>" +
        //            "<td>" + rw["pdn_tipo"].ToString().Trim() + "</td><td>" + rw["quejas"].ToString().Trim() + "</td><tr>";
        //    }
        //    detalle += "</tbody></table>";
        //}

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas_serv.aspx");
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dtExcel = (DataTable)Session["tbTotales"];
            DataTable dtExcel2 = new DataTable();
            dtExcel2.Columns.Add("Clave", typeof(string));
            dtExcel2.Columns.Add("Transportista", typeof(string));
            dtExcel2.Columns.Add("Total", typeof(string));
            dtExcel2.Columns.Add("Quejas", typeof(string));
            dtExcel2.Columns.Add("Porcentaje", typeof(string));
            dtExcel2.Columns.Add("MN", typeof(string));
            dtExcel2.Columns.Add("USD", typeof(string));
            foreach (DataRow rw in dtExcel.Rows)
            {
                if (rw["total_quejas"].ToString() == "0")
                    rw["total_porcen"] = "100%";
                else
                    rw["total_porcen"] = rw["total_porcen"] + "%";
                
                rw["MN"] = Math.Round(Convert.ToDecimal(rw["MN"].ToString()), 2).ToString("$###,###,##0.00");
                rw["USD"] = Math.Round(Convert.ToDecimal(rw["USD"].ToString()), 2).ToString("$###,###,##0.00");

                dtExcel2.Rows.Add(rw.ItemArray);
            }

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=ReporteServicios" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            response.ContentEncoding = Encoding.Unicode;
            response.BinaryWrite(Encoding.Unicode.GetPreamble());
            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                {
                    DataGrid dataGrid = new DataGrid();
                    dataGrid.DataSource = dtExcel2;
                    dataGrid.DataBind();
                    dataGrid.RenderControl(writer);
                    this.Response.Write(stringWriter.ToString());
                    dataGrid.Dispose();
                    this.Response.End();
                }
            }
        }
    }
}