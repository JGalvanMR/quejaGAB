using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace queja
{
    public partial class graphics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DataTable producto()
        {
            DataTable ds = (DataTable)Session["produces"];
            return ds;
        }

        [WebMethod]
        public static string facturadas_siguiente(string nombre, string mes, string rech)
        {

            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            sqlConnection.Open();
            //DataSet ds = new DataSet();
            //SqlDataAdapter adapter1 = new SqlDataAdapter("select RTRIM(prod_clave) AS prod_clave, RTRIM(REPLACE(prod_nombre, '''', '*')) AS prod_nombre from tb_cat_producto where prod_tipo in ('PTC', 'PTP') and prod_nombre <> '' ORDER BY prod_nombre", sqlConnection);
            //adapter1.Fill(ds, "productos");
            graphics dg = new graphics();
            DataTable ds = dg.producto();

            try
            {
                string anio = (mes == "Enero") ? DateTime.Now.AddYears(1).Year.ToString() : DateTime.Now.Year.ToString();
                string mes_num = "";
                string cve_prod = "";
                switch (mes)
                {
                    case "Enero":
                        mes_num = "01";
                        break;
                    case "Febrero":
                        mes_num = "02";
                        break;
                    case "Marzo":
                        mes_num = "03";
                        break;
                    case "Abril":
                        mes_num = "04";
                        break;
                    case "Mayo":
                        mes_num = "05";
                        break;
                    case "Junio":
                        mes_num = "06";
                        break;
                    case "Julio":
                        mes_num = "07";
                        break;
                    case "Agosto":
                        mes_num = "08";
                        break;
                    case "Septiembre":
                        mes_num = "09";
                        break;
                    case "Octubre":
                        mes_num = "10";
                        break;
                    case "Noviembre":
                        mes_num = "11";
                        break;
                    case "Diciembre":
                        mes_num = "12";
                        break;
                }
                string dias_mes = DateTime.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes_num)).ToString();

                string nom = nombre.Trim();
                string cv = "";
                foreach (DataRow rw in ds.Select("prod_nombre = '" + nom + "'"))//.Tables["productos"]
                    cv = rw["prod_clave"].ToString();

                string fecha_inicio = "01/" + mes_num + "/" + anio;//anio + "/" + mes_num + "/01";
                string fecha_final = dias_mes + "/" + mes_num + "/" + anio;//anio + "/" + mes_num + "/" + dias_mes;

                SqlCommand com = new SqlCommand("SELECT ISNULL(sum(A.fcn_num_unidades), 0) as fcn_num_unidades " +
                    "FROM tb_det_facturas A " +
                    "JOIN tb_mstr_facturas_nal B ON A.fcn_folio = B.fcn_folio and A.fcn_tipo = B.fcn_tipo " +
                    "JOIN tb_cat_producto C ON C.prod_clave = A.prod_clave " +
                    "WHERE (B.cnte_clave between '000000' and 'ZZZZZZ') and " +
                    "(B.fcn_fecha between '" + fecha_inicio + "' and '" + fecha_final + "') and " +
                    "B.fcn_estatus <> 'C' " +
                    "AND A.fcn_tipo <> 'TRA' AND A.prod_clave = '" + cv + "' " +
                    "group by A.lin_clave, A.prod_clave, C.prod_nombre " +
                    "order by A.lin_clave, A.prod_clave", sqlConnection);
                SqlDataReader reader = com.ExecuteReader();
                string factds = "0";
                if (reader.HasRows)
                {
                    reader.Read();
                    factds = reader["fcn_num_unidades"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                com.Dispose();
                sqlConnection.Close();

                string cadena = "";

                if (Convert.ToDecimal(factds) > 0)
                {
                    decimal val1 = Convert.ToDecimal(factds);
                    decimal val2 = Convert.ToDecimal(rech);
                    decimal val3 = Math.Round((val2 * Convert.ToDecimal("100")) / val1, 2);

                    cadena = "Facturadas: " + val1.ToString("###,##0") + " Rechazadas: " + val2.ToString("###,##0") + " Porcentaje: " + val3.ToString() + "%";
                }
                else
                {
                    cadena = rech + " (No tuvo ventas este mes)";
                }

                return cadena;

            }
            catch (SqlException ex)
            {
                return "error";
            }
            //cadena = "Facturadas Rechazadas";
            //return cadena;
        }

        [WebMethod]
        public static string facturadas_actual(string nombre, string mes, string rech)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            sqlConnection.Open();

            //DataSet ds = new DataSet();
            //SqlDataAdapter adapter1 = new SqlDataAdapter("select RTRIM(prod_clave) AS prod_clave, RTRIM(REPLACE(prod_nombre, '''', '*')) AS prod_nombre from tb_cat_producto where prod_tipo in ('PTC', 'PTP') and prod_nombre <> '' ORDER BY prod_nombre", sqlConnection);
            //adapter1.Fill(ds, "productos");
            graphics dg = new graphics();
            DataTable ds = dg.producto();

            try
            {
                string anio = DateTime.Now.Year.ToString();
                string mes_num = "";
                string cve_prod = "";
                switch (mes)
                {
                    case "Enero":
                        mes_num = "01";
                        break;
                    case "Febrero":
                        mes_num = "02";
                        break;
                    case "Marzo":
                        mes_num = "03";
                        break;
                    case "Abril":
                        mes_num = "04";
                        break;
                    case "Mayo":
                        mes_num = "05";
                        break;
                    case "Junio":
                        mes_num = "06";
                        break;
                    case "Julio":
                        mes_num = "07";
                        break;
                    case "Agosto":
                        mes_num = "08";
                        break;
                    case "Septiembre":
                        mes_num = "09";
                        break;
                    case "Octubre":
                        mes_num = "10";
                        break;
                    case "Noviembre":
                        mes_num = "11";
                        break;
                    case "Diciembre":
                        mes_num = "12";
                        break;
                }
                string dias_mes = DateTime.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes_num)).ToString();

                string nom = nombre.Trim();
                string cv = "";
                foreach (DataRow rw in ds.Select("prod_nombre = '" + nom.Trim() + "'"))
                    cv = rw["prod_clave"].ToString();

                string fecha_inicio = "01/" + mes_num + "/" + anio; //anio + "/" + mes_num + "/01";
                string fecha_final = dias_mes + "/" + mes_num + "/" + anio;//anio + "/" + mes_num + "/" + dias_mes;

                SqlCommand com = new SqlCommand("SELECT ISNULL(sum(A.fcn_num_unidades), 0) as fcn_num_unidades " +
                    "FROM tb_det_facturas A " +
                    "JOIN tb_mstr_facturas_nal B ON A.fcn_folio = B.fcn_folio and A.fcn_tipo = B.fcn_tipo " +
                    "JOIN tb_cat_producto C ON C.prod_clave = A.prod_clave " +
                    "WHERE (B.cnte_clave between '000000' and 'ZZZZZZ') and " +
                    "(B.fcn_fecha between '" + fecha_inicio + "' and '" + fecha_final + "') and " +
                    "B.fcn_estatus <> 'C' /*AND B.fcn_tipo = 'EXP'*/ " +
                    "AND A.fcn_tipo <> 'TRA' AND A.prod_clave = '" + cv + "' " +
                    "group by A.lin_clave, A.prod_clave, C.prod_nombre " +
                    "order by A.lin_clave, A.prod_clave", sqlConnection);
                SqlDataReader reader = com.ExecuteReader();
                string factds = "0";
                if (reader.HasRows)
                {
                    reader.Read();
                    factds = reader["fcn_num_unidades"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                com.Dispose();
                sqlConnection.Close();

                string cadena = "";

                if (Convert.ToDecimal(factds) > 0)
                {
                    decimal val1 = Convert.ToDecimal(factds);
                    decimal val2 = Convert.ToDecimal(rech);
                    decimal val3 = Math.Round((val2 * Convert.ToDecimal("100")) / val1, 2);

                    cadena = "Facturadas: " + val1.ToString("###,##0") + " Rechazadas: " + val2.ToString("###,##0") + " Porcentaje: " + val3.ToString() + "%";
                }
                else
                {
                    cadena = rech + " (No tuvo ventas este mes)";
                }

                return cadena;

            }
            catch (SqlException ex)
            {
                return "error";
            }
        }

        [WebMethod]
        public static string facturadas_anterior(string nombre, string mes, string rech)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            sqlConnection.Open();

            graphics dg = new graphics();
            DataTable ds = dg.producto();
            //DataSet ds = new DataSet();
            //SqlDataAdapter adapter1 = new SqlDataAdapter("select RTRIM(prod_clave) AS prod_clave, RTRIM(REPLACE(prod_nombre, '''', '*')) AS prod_nombre from tb_cat_producto where prod_tipo in ('PTC', 'PTP') and prod_nombre <> '' ORDER BY prod_nombre", sqlConnection);
            //adapter1.Fill(ds, "productos");

            try
            {
                string anio = (mes == "Diciembre") ? DateTime.Now.AddYears(-1).Year.ToString() : DateTime.Now.Year.ToString();
                string mes_num = "";
                string cve_prod = "";
                switch (mes)
                {
                    case "Enero":
                        mes_num = "01";
                        break;
                    case "Febrero":
                        mes_num = "02";
                        break;
                    case "Marzo":
                        mes_num = "03";
                        break;
                    case "Abril":
                        mes_num = "04";
                        break;
                    case "Mayo":
                        mes_num = "05";
                        break;
                    case "Junio":
                        mes_num = "06";
                        break;
                    case "Julio":
                        mes_num = "07";
                        break;
                    case "Agosto":
                        mes_num = "08";
                        break;
                    case "Septiembre":
                        mes_num = "09";
                        break;
                    case "Octubre":
                        mes_num = "10";
                        break;
                    case "Noviembre":
                        mes_num = "11";
                        break;
                    case "Diciembre":
                        mes_num = "12";
                        break;
                }
                string dias_mes = DateTime.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes_num)).ToString();

                string nom = nombre.Trim();
                string cv = "";
                foreach (DataRow rw in ds.Select("prod_nombre = '" + nom + "'"))
                    cv = rw["prod_clave"].ToString();

                string fecha_inicio = "01/" + mes_num + "/" + anio; //anio + "/" + mes_num + "/01";
                string fecha_final = dias_mes + "/" + mes_num + "/" + anio;//anio + "/" + mes_num + "/" + dias_mes;

                SqlCommand com = new SqlCommand("SELECT A.prod_clave, ISNULL(sum(A.fcn_num_unidades), 0) as fcn_num_unidades, C.prod_nombre " +
                    "FROM tb_det_facturas A " +
                    "JOIN tb_mstr_facturas_nal B ON A.fcn_folio = B.fcn_folio and A.fcn_tipo = B.fcn_tipo " +
                    "JOIN tb_cat_producto C ON C.prod_clave = A.prod_clave " +
                    "JOIN tb_cat_cliente D ON D.cnte_clave = B.cnte_clave " +
                    "join tb_cat_linea E ON E.lin_clave = A.lin_clave " +
                    "WHERE (B.cnte_clave between '000000' and 'ZZZZZZ') and " +
                    "(B.fcn_fecha between '" + fecha_inicio + "' and '" + fecha_final + "') and " +
                    "B.fcn_estatus <> 'C' /*AND B.fcn_tipo = 'EXP'*/ " +
                    "AND A.fcn_tipo <> 'TRA' AND A.prod_clave = '" + cv + "' " +
                    "group by A.lin_clave, A.prod_clave, C.prod_nombre " +
                    "order by A.lin_clave, A.prod_clave", sqlConnection);
                SqlDataReader reader = com.ExecuteReader();
                string factds = "0";
                if (reader.HasRows)
                {
                    reader.Read();
                    factds = reader["fcn_num_unidades"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                com.Dispose();

                string cadena = "";

                if (Convert.ToDecimal(factds) > 0)
                {
                    decimal val1 = Convert.ToDecimal(factds);
                    decimal val2 = Convert.ToDecimal(rech);
                    decimal val3 = Math.Round((val2 * Convert.ToDecimal("100")) / val1, 2);

                    cadena = "Facturadas: " + val1.ToString("###,##0") + " Rechazadas: " + val2.ToString("###,##0") + " Porcentaje: " + val3.ToString() + "%";
                }
                else
                {
                    cadena = rech + " (No tuvo ventas este mes)";
                }

                return cadena;

            }
            catch (SqlException ex)
            {
                return "error";
            }
        }
    }
}