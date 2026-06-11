using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace queja
{
    public partial class WebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string detalle_pedidos(string valor1, string valor2, string valor3, string valor4, string valor5)
        {
            string detalle = ""; //valor1.ToString() + " " + valor2.ToString() + " " + valor3.ToString() + " " + valor4.ToString(); // valor1 + " " + valor2 + " " + valor3 + " " + valor4;
            conectasql con = new conectasql();
            DataTable tbPlacas = con.score_card_detalle_pedidos(valor1.ToString(), valor2.ToString(), valor3.ToString(), valor4.ToString(), valor5.ToString());
            //detalle = "pasa";
            detalle = "<table border='1' class='table table-striped table-bordered table-hover'><thead><th>PEDIDO</th><th>FECHA</th><th>CLIENTE</th><th>DESTINO</th><th>PROBLEMA</th><th>MONTO</th><th>TIPO</th><th>QUEJAS</th></thead><tbody>";
            foreach (DataRow rw in tbPlacas.Select("pdn_tipo = 'NAL'"))
            {
                string[] ligas;
                string links = "";
                if (rw["quejas"].ToString().Trim() != "")
                {
                    if (rw["quejas"].ToString().Trim().Contains(','))
                    {
                        ligas = rw["quejas"].ToString().Trim().Split(',');
                        foreach (string var in ligas)
                        {
                            links += "<a onclick='ir_queja(\"" + var.Trim() + "\")'>" + var.Trim() + "</a>, ";
                        }
                        links = links.TrimEnd(' ').TrimEnd(',').ToString();
                    }
                    else
                    {
                        links += "<a onclick='ir_queja(\"" + rw["quejas"].ToString().Trim() + "\")'>" + rw["quejas"].ToString().Trim() + "</a>";
                    }
                }
                detalle += "<tr><td><small>" + rw["pdn_folio"].ToString().Trim() + "</small></td><td><small>" + Convert.ToDateTime(rw["pdn_fecha"].ToString().Trim()).ToString("dd/MM/yyyy") + "</small></td><td><small>" + rw["cnte_nombre"].ToString().Trim() + "</small></td>" +
                    "<td><small>" + rw["destino"].ToString().Trim() + "</small></td><td><small>" + rw["problema"].ToString().Trim() + "</small></td><td align='right'><small>" + Convert.ToDecimal(rw["pdn_monto_total"].ToString().Trim()).ToString("$###,###,##0.00") + "</small></td>" +
                    "<td><small>" + rw["pdn_tipo"].ToString().Trim() + "</small></td><td align='center'><small>" + links + "</small></td><tr>";
            }
            foreach (DataRow rw in tbPlacas.Select("pdn_tipo = 'EXP'"))
            {
                string[] ligas;
                string links = "";
                if (rw["quejas"].ToString().Trim() != "")
                {
                    if (rw["quejas"].ToString().Trim().Contains(','))
                    {
                        ligas = rw["quejas"].ToString().Trim().Split(',');
                        foreach (string var in ligas)
                        {
                            links += "<a onclick='ir_queja(\"" + var.Trim() + "\")'>" + var.Trim() + "</a>, ";
                        }
                        links = links.TrimEnd(' ').TrimEnd(',').ToString();
                    }
                    else
                    {
                        links += "<a onclick='ir_queja(\"" + rw["quejas"].ToString().Trim() + "\")'>" + rw["quejas"].ToString().Trim() + "</a>";
                    }
                }
                detalle += "<tr><td><small>" + rw["pdn_folio"].ToString().Trim() + "</small></td><td><small>" + Convert.ToDateTime(rw["pdn_fecha"].ToString().Trim()).ToString("dd/MM/yyyy") + "</small></td><td><small>" + rw["cnte_nombre"].ToString().Trim() + "</small></td>" +
                    "<td><small>" + rw["destino"].ToString().Trim() + "</small></td><td><small>" + rw["problema"].ToString().Trim() + "</small></td><td align='right'><small>" + Convert.ToDecimal(rw["pdn_monto_total"].ToString().Trim()).ToString("$###,###,##0.00") + "</small></td>" +
                    "<td><small>" + rw["pdn_tipo"].ToString().Trim() + "</small></td><td align='center'><small>" + links + "</small></td><tr>";
            }
            detalle += "</tbody></table>";

            return detalle;
        }

        //[WebMethod]
        //public static void redireccion_queja(string valor1, string valor2, string valor3, string valor4, string valor5, string valor6)
        //{
        //    string url = "sevicios.aspx?clave=" + valor1.ToString() + "&nombre=" + valor2.ToString() + "&cedis=" + valor3.ToString() + "&admin=" + valor4.ToString() + 
        //        "&queja=" + valor5.ToString() + "&tarea=" + valor6.ToString());
        //}
    }
}