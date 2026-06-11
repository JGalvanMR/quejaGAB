using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Script.Services;
using System.IO;

namespace queja
{
    /// <summary>
    /// Descripción breve de procesos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class procesos : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string FolioQueja()
        {
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT MAX(que_folio) AS maximo FROM tb_mstr_quejas";
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            int num = 1;
            if (reader.HasRows)
            {
                reader.Read();
                if (reader.GetValue(0).ToString().Trim() != "")
                    num = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();

            return num.ToString();
        }

        [WebMethod]
        public int Semana()
        {
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            cmd = conn.CreateCommand();
            DateTime ahora = DateTime.Now;
            string dia = ahora.Day.ToString().PadLeft(2, '0');
            string mes = ahora.Month.ToString().PadLeft(2, '0');
            string anio = ahora.Year.ToString();
            string f_1 = dia + "/" + mes + "/" + anio;
            string f_2 = "";
            cmd.CommandText = "SELECT semana from tb_cat_semanas WHERE '" + f_1 + "' >= fecha1  and '" + f_1 + "' <= fecha2";
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            int num = 1;
            if (reader.HasRows)
            {
                reader.Read();
                f_2 = reader["semana"].ToString().Trim();
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();

            return Convert.ToInt32(f_2);
        }

        [WebMethod]
        public int SemanaEmb(string fecha)
        {
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            cmd = conn.CreateCommand();
            //DateTime ahora = DateTime.Now;
            //string dia = ahora.Day.ToString().PadLeft(2, '0');
            //string mes = ahora.Month.ToString().PadLeft(2, '0');
            //string anio = ahora.Year.ToString();
            //string f_1 = dia + "/" + mes + "/" + anio;
            string fech = fecha;

            if (fech.Contains(":"))
                fech = Convert.ToDateTime(fecha).ToString("dd/MM/yyyy");

            string f_2 = "";
            cmd.CommandText = "SELECT semana from tb_cat_semanas WHERE '" + fech + "' >= fecha1  and '" + fech + "' <= fecha2";
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            int num = 1;
            if (reader.HasRows)
            {
                reader.Read();
                f_2 = reader["semana"].ToString().Trim();
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();

            return Convert.ToInt32(f_2);
        }

        [WebMethod]
        public string Mes()
        {
            DateTime ahora = DateTime.Now;
            string mes = ahora.Month.ToString().PadLeft(2, '0');
            return mes;
        }

        [WebMethod]
        public string Fecha()
        {
            DateTime ahora = DateTime.Now;
            string fech = ahora.ToString("dd/MM/yyyy");
            return fech;
        }

        [WebMethod]
        public List<ItemCombo> CargarClientes()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CliCod", typeof(string));
            dataTable.Columns.Add("CliRSocial", typeof(string));
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT cli_folio, cli_nombre FROM tb_quejas_clientes ORDER BY cli_nombre";
            reader = cmd.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["CliCod"] = (object)"";
            row1["CliRSocial"] = (object)"SELECCIONAR CLIENTE...";
            dataTable.Rows.Add(row1);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["CliCod"] = (object)reader["cli_folio"].ToString().Trim();
                    row2["CliRSocial"] = (object)reader["cli_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
            
            List<ItemCombo> lista = new List<ItemCombo>();
            foreach(DataRow row in dataTable.Rows)
            {
                lista.Add(new ItemCombo
                {
                    Id = row["CliCod"].ToString(),
                    Nombre = row["CliRSocial"].ToString()
                });
            }

            return lista;
        }

        [WebMethod]
        public List<ItemCombo> CargarSucursales(string clave, string cedis)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(string));
            dataTable.Columns.Add("Nombre", typeof(string));
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT subcli_folio AS Id, subcli_nombre AS Nombre FROM tb_queja_subclientes WHERE cli_folio = '" + clave + "' AND subcli_cedis = '" + cedis + "' AND estatus = '1' ORDER BY subcli_nombre";
            reader = cmd.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["Id"] = (object)"";
            row1["Nombre"] = (object)"SELECCIONAR SUCURSAL...";
            dataTable.Rows.Add(row1);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["Id"] = (object)reader["Id"].ToString().Trim();
                    row2["Nombre"] = (object)reader["Nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();

            List<ItemCombo> lista = new List<ItemCombo>();
            foreach (DataRow row in dataTable.Rows)
            {
                lista.Add(new ItemCombo
                {
                    Id = row["Id"].ToString(),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return lista;
        }

        [WebMethod]
        public List<RowOrdenProduccion> CargarOrdenProduccion(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prod_clave", typeof(string));
            dataTable.Columns.Add("prod_nombre", typeof(string));
            dataTable.Columns.Add("pti_fecha", typeof(string));
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("rch_clave", typeof(string));
            dataTable.Columns.Add("rch_nombre", typeof(string));
            dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("tbl_nombre", typeof(string));
            dataTable.Columns.Add("fecha_cad", typeof(string));
            dataTable.Columns.Add("tipo", typeof(string));
            dataTable.Columns.Add("lote", typeof(string));

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            string[] strArray1 = new string[5]
              {
                "SELECT distinct(A.prod_clave), A.prod_nombre, A.pti_fecha, A.prov_clave, A.prov_nombre, A.rch_clave, A.rch_nombre, A.tbl_clave, A.tbl_nombre, A.fecha_cad, A.tipo, A.lote FROM tb_det_trazabilidad A WHERE A.pti_estatus <> 'C' AND A.recibo = '",
                folio,
                "' AND A.pti_fecha >= '",
                null,
                null
              };
            string[] strArray2 = strArray1;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(-120.0);
            string shortDateString = dateTime.ToString("dd/MM/yyyy");
            strArray2[3] = shortDateString;
            strArray1[4] = "'ORDER BY A.prod_nombre";
            string str = string.Concat(strArray1);
            cmd.CommandText = str;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prod_clave"] = (object)reader.GetValue(0).ToString().Trim();
                    row["prod_nombre"] = (object)reader.GetValue(1).ToString().Trim();
                    row["pti_fecha"] = (object)Convert.ToDateTime(reader.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["prov_clave"] = (object)reader.GetValue(3).ToString().Trim();
                    row["prov_nombre"] = (object)reader.GetValue(4).ToString().Trim();
                    row["rch_clave"] = (object)reader.GetValue(5).ToString().Trim();
                    row["rch_nombre"] = (object)reader.GetValue(6).ToString().Trim();
                    row["tbl_clave"] = (object)reader.GetValue(7).ToString().Trim();
                    row["tbl_nombre"] = (object)reader.GetValue(8).ToString().Trim();
                    row["fecha_cad"] = (object)reader.GetValue(9).ToString().Trim();
                    row["tipo"] = (object)reader.GetValue(10).ToString().Trim();
                    row["lote"] = (object)reader.GetValue(11).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();

            List<RowOrdenProduccion> lista = new List<RowOrdenProduccion>();
            foreach (DataRow row in dataTable.Rows)
            {
                lista.Add(new RowOrdenProduccion
                {
                    Clave = row["prod_clave"].ToString(),
                    Nombre = row["prod_nombre"].ToString(),
                    Fecha = row["pti_fecha"].ToString(),
                    Prov = row["prov_clave"].ToString(),
                    ProvNom = row["prov_nombre"].ToString(),
                    Ranch = row["rch_clave"].ToString(),
                    RanchNom = row["rch_nombre"].ToString(),
                    Tabla = row["tbl_clave"].ToString(),
                    TablaNom = row["tbl_nombre"].ToString(),
                    FechaCad = row["fecha_cad"].ToString(),
                    Tipo = row["tipo"].ToString(),
                    Lote = row["lote"].ToString()
                });
            }

            return lista;
            
        }

        [WebMethod]
        public List<RowDatosOrden> CargarDatosOrden(string folio, string tipo_recibo)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ordp_linea", typeof(string));
            dataTable.Columns.Add("ordp_responsable", typeof(string));

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT inicio_campo FROM Tb_folio_campo";
                reader = cmd.ExecuteReader();
                int fol_campo = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fol_campo = Convert.ToInt32(reader["inicio_campo"].ToString().Trim());
                    }
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();

                if (Convert.ToInt32(folio) > fol_campo) //folio de campo
                {
                    //validar combo de problema
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT rpt_flete FROM tb_mstr_recepcion_pt WHERE rpt_recibo = '" + folio + "'";
                    reader = cmd.ExecuteReader();
                    int flete_campo = 0;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["rpt_flete"].ToString().Trim() == "S/F")
                                flete_campo = 0;
                            else
                                flete_campo = (reader["rpt_flete"].ToString().Trim() == "") ? 0 : Convert.ToInt32(reader["rpt_flete"].ToString().Trim());
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    cmd.Dispose();
                    //buscar flete

                    string responsable_flete = "";
                    if (flete_campo == 0)
                    {
                        DataRow row = dataTable.NewRow();
                        row["ordp_linea"] = "";
                        row["ordp_responsable"] = responsable_flete;
                        dataTable.Rows.Add(row);
                    }
                    else
                    {

                        MySqlConnection conexion = new MySqlConnection();
                        conexion.ConnectionString = "Server=gab.mrlucky.com.mx;Database=campo; Uid=www1166;Pwd=taQ17Zm;";
                        MySqlCommand comando;
                        MySqlDataReader lector;
                        conexion.Open();
                        comando = conexion.CreateCommand();
                        comando.CommandText = "SELECT B.nom_responsable FROM tb_mstr_flete A JOIN tb_cat_responsables B ON A.id_responsable = B.id_responsable WHERE A.id_flete = '" + flete_campo + "'";
                        lector = comando.ExecuteReader();
                        if (lector.HasRows)
                        {
                            lector.Read();
                            responsable_flete = lector["nom_responsable"].ToString().Trim();
                        }
                        lector.Close();
                        lector.Dispose();
                        comando.Dispose();
                        conexion.Close();

                        DataRow row = dataTable.NewRow();
                        row["ordp_linea"] = "CAMPO";
                        row["ordp_responsable"] = responsable_flete;
                        dataTable.Rows.Add(row);
                    }



                }
                else //folio de planta
                {
                    if (tipo_recibo == "PTC")//recibo de aguilares
                    {
                        bool bandera = false;
                        SqlConnection conn_agui = new SqlConnection();
                        //conn_agui.ConnectionString = "Data Source=200.76.124.19\\SQL2014,2359; Initial Catalog=GAB_Empaque; Connect Timeout=130; User ID=SipGab; Password=Empaque1$; MultipleActiveResultSets=True";
                        conn_agui.ConnectionString = "Data Source=38.49.143.54\\SQL2017,2359; Initial Catalog=GAB_Empaque; Connect Timeout=130; User ID=uerp; Password=mocoro1$; MultipleActiveResultSets=True";
                        SqlCommand comm_agui;
                        SqlDataReader read_agui;
                        conn_agui.Open();
                        comm_agui = conn_agui.CreateCommand();
                        comm_agui.CommandText = "SELECT ordp_linea, ordp_responsable FROM tb_mstr_ordenes_prod WHERE ordp_folio = '" + folio + "'";
                        read_agui = comm_agui.ExecuteReader();
                        if (read_agui.HasRows)
                        {
                            bandera = true;
                            while (read_agui.Read())
                            {
                                DataRow row = dataTable.NewRow();
                                row["ordp_linea"] = read_agui.GetValue(0).ToString().Trim();
                                row["ordp_responsable"] = read_agui.GetValue(1).ToString().Trim();
                                dataTable.Rows.Add(row);
                            }
                        }
                        read_agui.Close();
                        read_agui.Dispose();
                        comm_agui.Dispose();
                        conn_agui.Close();

                        if (bandera == false)
                        {
                            cmd = conn.CreateCommand();
                            cmd.CommandText = "SELECT rpt_evaluador FROM tb_mstr_recepcion_pt WHERE rpt_recibo = '" + folio + "'";
                            reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow row = dataTable.NewRow();
                                    row["ordp_linea"] = "";
                                    row["ordp_responsable"] = (object)reader.GetValue(0).ToString().Trim();
                                    dataTable.Rows.Add(row);
                                }
                            }
                            reader.Close();
                            reader.Dispose();
                            cmd.Dispose();
                        }

                    }
                    else
                    {
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "SELECT ordp_linea, ordp_responsable FROM tb_mstr_ordenes_prod WHERE ordp_folio = '" + folio + "'";
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DataRow row = dataTable.NewRow();
                                row["ordp_linea"] = (object)reader.GetValue(0).ToString().Trim();
                                row["ordp_responsable"] = (object)reader.GetValue(1).ToString().Trim();
                                dataTable.Rows.Add(row);
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        cmd.Dispose();
                    }

                }


                conn.Close();
            }
            catch (Exception ex)
            {
                DataRow row = dataTable.NewRow();
                row["ordp_linea"] = "";
                row["ordp_responsable"] = ex.ToString();
                dataTable.Rows.Add(row);
            }

            List<RowDatosOrden> lista = new List<RowDatosOrden>();
            foreach (DataRow row in dataTable.Rows)
            {
                lista.Add(new RowDatosOrden
                {
                    Linea = row["ordp_linea"].ToString(),
                    Responsable = row["ordp_responsable"].ToString()
                });
            }

            return lista;
        }

        [WebMethod]
        public List<RowVariedad> CargarVariedad(string folio, string prod, string tipo)
        {
            string variedad = "";
            if (tipo == "PTC")
            {
                variedad = variedad_recepcion_pt(folio);
            }
            if (tipo == "PTP")
            {
                string val1 = variedad_busca_eti_final(folio, prod);
                string val2 = variedad_busca_prod_odp(folio, val1);
                if (val2 == "MP")
                    variedad = variedad_recepcion_mp(val1);
                if (val2 == "REM")
                {
                    string[] strArray = variedad_prod_ode_tipo(val1).Split('-');
                    if (strArray[1] == "PTP")
                    {
                        string val1a = variedad_busca_eti_final(strArray[0], strArray[2]);
                        string val2a = variedad_busca_prod_odp(strArray[0], val1a);
                        if (val2a == "MP" || val2a == "PTP")
                            variedad = variedad_recepcion_mp(val1a);
                    }
                }
                if (val2 == "ESP")
                    variedad = variedad_recepcion_esparrago(val1);
            }

            List<RowVariedad> lista = new List<RowVariedad>();

            lista.Add(new RowVariedad
            {
                Variedad = variedad
            });
            

            return lista;
        }

        [WebMethod]
        public List<RowProducidas> CargarCajasProducidas(string folio, string prod, string tar, string tipo)
        {
            string cajas_prod = "0";
            if (tipo == "PTC")
            {
                cajas_prod = cajas_producidas_folio(folio, prod);
            }
            if (tipo =="PTP")
            {
                cajas_prod = cajas_producidas_folio2(folio, prod, "");
            }

            List<RowProducidas> lista = new List<RowProducidas>();

            lista.Add(new RowProducidas
            {
                Producidas = cajas_prod
            });
            
            return lista;
        }

        public string variedad_recepcion_pt(string rec)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select b.vari_nombre from tb_mstr_recepcion_pt a, tb_cat_variedad b where a.rpt_recibo = '" + rec + "' and a.vari_clave = b.vari_clave and a.lin_clave = b.lin_clave";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["vari_nombre"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        public string variedad_busca_eti_final(string fol, string prod)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT recibo FROM tb_det_eti_final WHERE folio = '" + fol + "' AND cve_prod = '" + prod + "'";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["recibo"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        public string variedad_busca_prod_odp(string fol, string rec)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select rmp_tipo from tb_det_prod_odp where ordp_folio = '" + fol + "' and rmp_recibo = '" + rec + "'";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["rmp_tipo"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        public string variedad_recepcion_mp(string rec)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select b.vari_nombre from tb_mstr_recepcion_mp a, tb_cat_variedad b where a.rmp_recibo = '" + rec + "' and a.vari_clave = b.vari_clave and a.lin_clave = b.lin_clave";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["vari_nombre"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        public string variedad_prod_ode_tipo(string rec)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select rmp_recibo, rmp_tipo, prod_clave from tb_det_prod_ode where orde_folio = '" + rec + "'";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["rmp_tipo"].ToString().Trim() + "-" + reader["rmp_tipo"].ToString().Trim() + "-" + reader["prod_clave"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        public string variedad_recepcion_esparrago(string rec)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select b.vari_nombre from tb_mstr_recepcion_esparrago a, tb_cat_variedad b where a.rmp_recibo = '" + rec + "' and a.vari_clave = b.vari_clave and a.lin_clave = b.lin_clave";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["vari_nombre"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        public string cajas_producidas_folio(string rec, string pro)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select b.rptd_cantidad from tb_det_recepcion_pt B join tb_mstr_recepcion_pt a on b.rpt_recibo = a.rpt_recibo where a.rpt_recibo = '" + rec + "' and prod_clave = '" + pro + "'";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["rptd_cantidad"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        public string cajas_producidas_folio2(string rec, string pro, string tar)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE folio = '" + rec + "' AND cve_prod = '" + pro + "'";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    str = reader["num_cajas"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                conn.Close();
                str = "0";
            }
            conn.Close();
            return str;
        }

        [WebMethod]
        public List<ItemProblemas> CargarProblemas()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("pro_clave", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT pro_clave, pro_nombre FROM tb_cat_problemas WHERE pro_estatus = 'A' ORDER BY pro_nombre";
            reader = cmd.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["pro_clave"] = (object)"";
            row1["pro_nombre"] = (object)"SELECCIONAR PROBLEMA...";
            dataTable.Rows.Add(row1);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["pro_clave"] = (object)reader.GetValue(0).ToString().Trim();
                    row["pro_nombre"] = (object)reader.GetValue(1).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            conn.Close();

            List<ItemProblemas> lista = new List<ItemProblemas>();
            foreach (DataRow row in dataTable.Rows)
            {
                lista.Add(new ItemProblemas
                {
                    Clave = row["pro_clave"].ToString(),
                    Nombre = row["pro_nombre"].ToString()
                });
            }

            return lista;
        }

        [WebMethod]
        public string GuardarQueja(string folio, string semana, string fecha, string mes, string cliprim, string cliente, string sucursal, string reporto, string recibio, 
            string cedis, string usuario, string tipo, string subclifolio, string pedido, string observaciones, string costo, string consumidor, string producto, string problema, string ordprod, string area,
            string responsable, string cantrecha, string cantreci, string unidad, string devolucion, string moneda, string cveprov, string cverch, string cvetbl, string variedad,
            string lote, string nomprod, string fechacad, string ptcptp, string cjsprod, string porcen, string merma, string bonificacion, string rechazo, string noaplica,
            string nombre, string problemanom)
        {
            string cuerpo = "";
            string str1 = "";

            string fechainv = "";
            string area_queja = "";
            string cnte = "";
            string sememb = ""; 
            string fechaemb = "";

            string consu = (consumidor == "True") ? "1" : "0";



            string valores = "";
            string fchf = "";
            string fact = "";
            if (consu == "0")
            {
                valores = ValidarIngresoPedido(pedido, tipo, recibio, producto);

                if (valores == "0")
                {
                    return "NOPED";
                }

                string[] values = valores.Split('*');
                fchf = values[2];
                fact = values[1];
                pedido = values[0];

                cnte = BuscarClientePedido(pedido, tipo, cedis, usuario);
                //fechaemb = fecha_embarque(pedido, tipo, usuario);
                sememb = SemanaEmb(fchf).ToString();
                
            }

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            try
            {
                conn.Open();
                SqlCommand com;
                com = conn.CreateCommand();
                string f_1 = fecha; // Convert.ToDateTime(fecha).ToString();
                string[] f_1a = f_1.Split('/');
                Fecha2 fch = new procesos.Fecha2(Convert.ToInt32(f_1a[0]), Convert.ToInt32(f_1a[1]), Convert.ToInt32(f_1a[2]));
                string fecha_inv = fch.AgregarUnDia();
                string f_emb = "";

                if (consu == "0")
                {
                    //f_emb = Convert.ToDateTime(fechaemb).ToString("dd/MM/yyyy");

                    cuerpo = "INSERT INTO tb_mstr_quejas(que_semana, que_fecha, que_mes, que_cliprim, que_cliente, que_sucursal, que_reporto, que_recibio, que_status, cedis, resp_usuario, que_tipo, que_fechainv, area_queja, " +
                    "subcli_folio, que_pedido, que_observacion, que_costo, que_cnte, que_consumidor, que_sememb, que_fechaemb, fcn_folio) " +
                    "VALUES('" + semana + "', '" + fecha + "', '" + mes + "', '" + cliprim + "', '" + cliente + "', '" + sucursal + "', '" + reporto.ToUpper() + "', '" + recibio + "', 'A', '" + cedis + "', " +
                    "'" + usuario + "', '" + tipo + "', '" + fecha_inv + "', '" + area_queja + "', '" + subclifolio + "', '" + pedido + "', '" + observaciones.ToUpper() + "', '" + costo + "', '" + cnte + "', " +
                    "'" + consu + "', '" + sememb + "', '" + fchf + "', '" + fact + "') SELECT SCOPE_IDENTITY()";
                }
                else
                {
                    
                    cuerpo = "INSERT INTO tb_mstr_quejas(que_semana, que_fecha, que_mes, que_cliprim, que_cliente, que_sucursal, que_reporto, que_recibio, que_status, cedis, resp_usuario, que_tipo, que_fechainv, area_queja, " +
                    "subcli_folio, que_pedido, que_observacion, que_costo, que_cnte, que_consumidor) " +
                    "VALUES('" + semana + "', '" + fecha + "', '" + mes + "', '" + cliprim + "', '" + cliente + "', '" + sucursal + "', '" + reporto.ToUpper() + "', '" + recibio + "', 'A', '" + cedis + "', " +
                    "'" + usuario + "', '" + tipo + "', '" + fecha_inv + "', '" + area_queja + "', '" + subclifolio + "', '" + pedido + "', '" + observaciones.ToUpper() + "', '" + costo + "', '" + cnte + "', '" + consu + "') SELECT SCOPE_IDENTITY()";
                }
                com.CommandText = cuerpo;
                str1 = "1";
                string str2 = Convert.ToString(com.ExecuteScalar()).Trim();
                com.Dispose();
                if (Convert.ToInt32(str2) > Convert.ToInt32(folio))
                    folio = str2;
                string str3 = "INSERT INTO tb_det_quejas(que_folio, id_producto, qud_problema, qud_ordprod, qud_area, qud_responsable, qud_cantrecha, qud_cantreci, qud_unidad, qud_devolucion, " +
                    "qud_moneda, prov_clave, rch_clave, tbl_clave, qud_variedad, qud_lote, nom_producto, fecha_cad, qud_causa, qud_ptcptp, qud_cjsprod, qud_porcen, qud_merma, qud_cnte, qud_pedido, qud_bonificacion, qud_rechazado, qud_noaplica) " +
                    "VALUES('" + folio + "', '" + producto + "', '" + problema + "', '" + ordprod + "', '" + area + "', '" + responsable + "', '" + cantrecha + "', '" + cantreci + "', '" + unidad + "', " +
                    "'" + devolucion + "', '" + moneda + "', '" + cveprov + "', '" + cverch + "', '" + cvetbl + "', '" + variedad + "', '" + lote + "', '" + nomprod.Replace("'", "").ToString() + "', " +
                    "'" + fechacad + "', '', '" + ptcptp + "', '" + cjsprod + "', '" + porcen + "', '" + merma + "', '" + cnte + "', '" + pedido + "', '" + bonificacion + "', '" + rechazo + "', '" + noaplica + "')";
                cuerpo = cuerpo + " DETALLE:" + str3;
                com = conn.CreateCommand();
                com.CommandText = str3;
                //str1 = "2";
                com.ExecuteNonQuery();
                conn.Close();

                enviarcorreo(nombre, cedis, fecha, folio, producto, nomprod, problemanom, cliente, pedido, costo);

                //str1 = "";
                return folio; //str1 + "-" + folio;
            }
            catch (SqlException ex)
            {
                //this.enviarcorreo_error(nom_realiza, "NUEVO FOLIO: " + folio, cuerpo);
                conn.Close();
                return str1;
            }
        }

        [WebMethod]
        public string GuardarQuejaExp(string folio, string semana, string fecha, string mes, string cliprim, string cliente, string sucursal, string reporto, string recibio,
            string cedis, string usuario, string tipo, string subclifolio, string costo, string pedido, string transporte, string observaciones, string fact, string sufijo,
            List<ListaProductos> product, List<ListaFacturas> facts, string levantaQueja, string problemanom, string FechaEm)
        {
            string cuerpo = "";
            string str1 = "";

            string fechainv = "";
            string area_queja = "";
            string sememb = "";
            string fechaemb = "";

            fechaemb = fecha_embarque(pedido, tipo, usuario);
            sememb = SemanaEmb(FechaEm).ToString();

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            try
            {
                conn.Open();
                SqlCommand com;
                com = conn.CreateCommand();
                string f_1 = fecha; // Convert.ToDateTime(fecha).ToString();
                string[] f_1a = f_1.Split('/');
                Fecha2 fch = new procesos.Fecha2(Convert.ToInt32(f_1a[0]), Convert.ToInt32(f_1a[1]), Convert.ToInt32(f_1a[2]));
                string fecha_inv = fch.AgregarUnDia();
                string f_emb = Convert.ToDateTime(fechaemb).ToString("dd/MM/yyyy");

                cuerpo = "INSERT INTO tb_mstr_quejas(que_semana, que_fecha, que_mes, que_cliprim, que_cliente, que_sucursal, que_reporto, que_recibio, que_status, cedis, " +
                    "resp_usuario, que_tipo, que_fechainv, area_queja, subcli_folio, que_costo, que_pedido, que_transporte, que_observacion, " +
                    "fcn_folio, que_sufijo, que_fechaemb, que_sememb) " +
                    "VALUES('" + semana + "', '" + fecha + "', '" + mes + "', '" + cliprim + "', '" + cliente + "', '" + sucursal + "', '" + reporto.ToUpper() + "', '" + recibio + "', 'A', '" + cedis + "', " +
                    "'" + usuario + "', '" + tipo + "', '" + fecha_inv + "', '" + area_queja + "', '" + subclifolio + "', '" + costo + "', '" + pedido + "', '" + transporte + "', '" + observaciones.ToUpper() + "', " +
                    "'" + fact + "', '" + sufijo + "', '" + f_emb + "', '" + sememb + "') SELECT SCOPE_IDENTITY()";

                com.CommandText = cuerpo;
                str1 = "1";
                string str2 = Convert.ToString(com.ExecuteScalar()).Trim();
                com.Dispose();
                if (Convert.ToInt32(str2) > Convert.ToInt32(folio))
                    folio = str2;
                //string str3 = "INSERT INTO tb_det_quejas(que_folio, id_producto, qud_problema, qud_ordprod, qud_area, qud_responsable, qud_cantrecha, qud_cantreci, qud_unidad, qud_devolucion, " +
                //    "qud_moneda, prov_clave, rch_clave, tbl_clave, qud_variedad, qud_lote, nom_producto, fecha_cad, qud_causa, qud_ptcptp, qud_cjsprod, qud_porcen, qud_merma, qud_cnte, qud_pedido, qud_bonificacion, qud_rechazado, qud_noaplica) " +
                //    "VALUES('" + folio + "', '" + producto + "', '" + problema + "', '" + ordprod + "', '" + area + "', '" + responsable + "', '" + cantrecha + "', '" + cantreci + "', '" + unidad + "', " +
                //    "'" + devolucion + "', '" + moneda + "', '" + cveprov + "', '" + cverch + "', '" + cvetbl + "', '" + variedad + "', '" + lote + "', '" + nomprod.Replace("'", " ").ToString() + "', " +
                //    "'" + fechacad + "', '', '" + ptcptp + "', '" + cjsprod + "', '" + porcen + "', '" + merma + "', '" + cnte + "', '" + pedido + "', '" + bonificacion + "', '" + rechazo + "', '" + noaplica + "')";
                //cuerpo = cuerpo + " DETALLE:" + str3;
                //com = conn.CreateCommand();
                //com.CommandText = str3;
                ////str1 = "2";
                //com.ExecuteNonQuery();

                foreach (var p in product)
                {
                    com = conn.CreateCommand();
                    string str5 = "INSERT INTO tb_det_quejas(" +
                        "que_folio, id_producto, qud_problema, qud_ordprod, qud_area, qud_responsable, qud_cantrecha, qud_cantreci, qud_unidad, qud_devolucion, " +
                        "qud_moneda, prov_clave, rch_clave, tbl_clave, qud_variedad, qud_lote, nom_producto, fecha_cad, qud_causa, qud_tarima, " +
                        "qud_ptcptp, qud_cjsprod, qud_porcen, qud_merma, qud_cnte, qud_pedido, qud_bonificacion, qud_rechazado, qud_noaplica, conse) VALUES ";
                    str5 = str5 + "('" + folio + "', '" + p.ProdCve + "', '" + p.Prob + "', '" + p.OrdProd + "', '" + p.Area + "', '" + p.Responsable + "', '" + p.Rechazadas + "', '" + p.Recibido + "', '" + p.Unidad + "', '" + p.Devo + "', " +
                        "'" + p.Mone + "', '" + p.CveProv + "', '" + p.CveRch + "', '" + p.CveTbl + "', '" + p.Variedad + "', '" + p.Lote + "', '" + p.Producto.Replace("'", "") + "', '" + p.Fecha + "', '" + p.Caus + "', '" + p.Tarima + "', " +
                        "'" + p.Tipo + "', '" + p.Producidas + "', '" + p.Porcentaje + "', '" + p.Merm + "', '" + p.CNTE + "', '" + p.Pedido + "', '" + p.Boni + "', '" + p.Rechazadas + "', '" + p.Noap + "', '" + p.Cons + "')";
                    com.CommandText = str5;
                    com.ExecuteNonQuery();
                    com.Dispose();
                }
                conn.Close();

                if (facts.Count > 0)
                {
                    foreach (var f in facts)
                    {
                        decimal fCan = Convert.ToDecimal(f.CanFact);
                        string valor = "";
                        //if (fCan > 0)
                        valor = AgregaDetalleCostoFactura(folio, f.FolFact, Convert.ToDecimal(fCan).ToString());
                    }
                }

                enviarcorreoexp(levantaQueja, cedis, fecha, folio, problemanom, cliente, pedido);

                //str1 = "";
                return folio; //str1 + "-" + folio;
            }
            catch (SqlException ex)
            {
                //this.enviarcorreo_error(nom_realiza, "NUEVO FOLIO: " + folio, cuerpo);
                conn.Close();
                return str1;
            }
        }

        public string AgregaDetalleCostoFactura(string folio, string factura, string costo)
        {
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand com;
            com = conn.CreateCommand();
            string str;
            try
            {
                com = conn.CreateCommand();
                com.CommandText = "INSERT INTO tb_det_quejas_fact(que_folio, factura, costo) VALUES('" + folio + "', '" + factura + "', '" + costo + "')";
                com.ExecuteNonQuery();
                com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            conn.Close();
            return str;
        }

        /*
         * 
         * string folio, string semana, string fecha, string mes, string cliprim, string cliente, string sucursal, string reporto, string recibio, string producto,
          string problema, string ordprod, string area, string responsable, string cantrecha, string cantreci, string unidad, string devolucion, string moneda, string cveprov,
          string cverch, string cvetbl, string lote, string nomprod, string fechacad, string causa, string cedis, string usuario, string tipo, string fechainv,
          string area_queja, string subcli_folio, string ptc_ptp, string varie, string pedido, string observaciones, string nom_realiza, string cjs_prod, string porcen, string costo,
          string merma, string cnte, string consumidor, string bonificacion, string noaplica, string sememb, string fechaemb
         */

        //[WebMethod]
        public string ValidarIngresoPedido(string pedido, string expnal, string clave, string producto)
        {
            string str = "";

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            string val_tipo = "";
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT resp_tipo FROM tb_cat_responsables WHERE resp_clave = '" + clave + "'";
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                val_tipo = reader["resp_tipo"].ToString().Trim();
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();

            string fecha_ov = "";

            cmd = conn.CreateCommand();
            if (expnal == "N")
                cmd.CommandText = "SELECT A.pdn_folio, FORMAT(A.pdn_fecha, 'dd-MM-yyyy') AS fecha FROM tb_mstr_pedidos_nal A JOIN tb_det_pedidos B " +
                    "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo " +
                    "WHERE A.pdn_folio = '" + pedido + "' AND A.pdn_tipo  = 'NAL' AND B.pdn_tipo = 'NAL' AND B.prod_clave = '" + producto + "'";
            else if (expnal == "E")
                cmd.CommandText = "SELECT A.pdn_folio, FORMAT(A.pdn_fecha, 'dd-MM-yyyy') AS fecha FROM tb_mstr_pedidos_exp A JOIN tb_det_pedidos B " +
                    "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo " +
                    "WHERE A.pdn_folio = '" + pedido + "' AND A.pdn_tipo  = 'EXP' AND B.pdn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";//this.com.CommandText = "SELECT pdn_folio FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + pedido + "'";
            else
                str = "0";
            if (str != "0")
            {
                try
                {
                    bool fnd_ped = false;
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)//cuando lo encuentra dentro de los pedidos
                    {
                        reader.Read();
                        str = reader["pdn_folio"].ToString().Trim();
                        fecha_ov = reader["fecha"].ToString();
                        fnd_ped = true;
                    }
                    else //lo busca dentro de las facturas
                    {
                        reader.Close();
                        reader.Dispose();
                        if (expnal == "N")
                            cmd.CommandText = "SELECT A.fcn_folio, A.pdn_folio, FORMAT(A.fcn_fecha, 'dd-MM-yyyy') As fecha FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
                                "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
                                "WHERE A.fcn_folio = '" + pedido + "' /*AND A.fcn_lugar  = '" + val_tipo + "' AND B.fcn_tipo = '" + val_tipo + "'*/ AND B.prod_clave = '" + producto + "'";
                        else if (expnal == "E")
                            cmd.CommandText = "SELECT A.fcn_folio, A.pdn_folio, FORMAT(A.fcn_fecha, 'dd-MM-yyyy') As fecha FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
                                "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
                                "WHERE A.fcn_folio = '" + pedido + "' AND A.fcn_lugar  = 'EXP' AND B.fcn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)//cuando lo encuentra dentro de los pedidos
                        {
                            reader.Read();
                            str = reader["pdn_folio"].ToString().Trim() + "*" + reader["fcn_folio"].ToString().Trim() + "*" + reader["fecha"].ToString().Trim();
                        }
                        else //lo busca dentro de las facturas
                            str = "0";
                    }
                    reader.Close();
                    reader.Dispose();

                    if (fnd_ped == true)
                    {
                        //cmd.CommandText = "SELECT A.fcn_folio, A.pdn_folio, FORMAT(A.fcn_fecha, 'dd-MM-yyyy') As fecha FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
                        //        "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
                        //        "WHERE A.pdn_folio = '" + pedido + "' AND A.fcn_lugar  = '" + val_tipo + "' AND B.fcn_tipo = '" + val_tipo + "' AND B.prod_clave = '" + producto + "'";
                        if (expnal == "N")
                            cmd.CommandText = "SELECT A.fcn_folio, A.pdn_folio, FORMAT(A.fcn_fecha, 'dd-MM-yyyy') As fecha FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
                                "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
                                "WHERE A.pdn_folio = '" + pedido + "' /*AND A.fcn_lugar <> '" + val_tipo + "' AND B.fcn_tipo = '" + val_tipo + "'*/ AND B.prod_clave = '" + producto + "'";
                            
                        else if (expnal == "E")
                            cmd.CommandText = "SELECT A.fcn_folio, A.pdn_folio, FORMAT(A.fcn_fecha, 'dd-MM-yyyy') As fecha FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
                                "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
                                "WHERE A.pdn_folio = '" + pedido + "' AND A.fcn_lugar  = 'EXP' AND B.fcn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)//cuando lo encuentra dentro de los pedidos
                        {
                            reader.Read();
                            str = reader["pdn_folio"].ToString().Trim() + "*" + reader["fcn_folio"].ToString().Trim() + "*" + reader["fecha"].ToString().Trim();
                        }
                        else //lo busca dentro de las facturas
                            str += "*" + "*" + fecha_ov;
                    }
                }
                catch (SqlException ex)
                {
                    conn.Close();
                    str = "0";
                }
            }
            //}
            conn.Close();
            return str;
        }

        public string BuscarClientePedido(string folio, string tipo, string cedis, string clave)
        {
            //buscar en facturas
            string cnte = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand com;
            SqlDataReader read;

            string val_tipo = "";
            com = conn.CreateCommand();
            com.CommandText = "SELECT resp_tipo FROM tb_cat_responsables WHERE resp_clave = '" + clave + "'";
            read = com.ExecuteReader();
            if (read.HasRows)
            {
                read.Read();
                val_tipo = read["resp_tipo"].ToString().Trim();
            }
            read.Close();
            read.Dispose();
            com.Dispose();

            try
            {
                if (tipo == "E")
                {
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT cnte_clave FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + folio + "' AND pdn_tipo = 'EXP'";
                    read = com.ExecuteReader();
                    if (read.HasRows)
                    {
                        read.Read();
                        cnte = read["cnte_clave"].ToString().Trim();
                    }
                    else
                    {
                        read.Close();
                        read.Dispose();
                        com.Dispose();

                        com = conn.CreateCommand();
                        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = 'EXP'";
                        read = com.ExecuteReader();
                        if (read.HasRows)
                        {
                            read.Read();
                            cnte = read["cnte_clave"].ToString().Trim();
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();

                }
                else
                {
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT cnte_clave FROM tb_mstr_pedidos_nal WHERE pdn_folio = '" + folio + "' AND pdn_tipo = 'NAL'";
                    read = com.ExecuteReader();
                    if (read.HasRows)
                    {
                        read.Read();
                        cnte = read["cnte_clave"].ToString().Trim();
                    }
                    else
                    {
                        read.Close();
                        read.Dispose();
                        com.Dispose();

                        com = conn.CreateCommand();
                        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = '" + val_tipo + "'";
                        read = com.ExecuteReader();
                        if (read.HasRows)
                        {
                            read.Read();
                            cnte = read["cnte_clave"].ToString().Trim();
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();
                }
            }
            catch (SqlException ex)
            {
                conn.Close();
                cnte = "";
            }
            conn.Close();
            return cnte;
        }

        public string fecha_embarque(string folio, string tipo, string clave)
        {
            string str = "";
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand com;
            SqlDataReader read;
            try
            {
                string val_tipo = "";
                com = conn.CreateCommand();
                com.CommandText = "SELECT resp_tipo FROM tb_cat_responsables WHERE resp_clave = '" + clave + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    read.Read();
                    val_tipo = read["resp_tipo"].ToString().Trim();
                }
                read.Close();
                read.Dispose();
                com.Dispose();

                if (val_tipo == "EXP")
                {
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT pdn_folio FROM tb_mstr_facturas_nal WHERE fcn_folio in (" + folio + ") and fcn_tipo = '" + val_tipo + "' AND fcn_fecha >= '" + DateTime.Now.AddDays(60).ToShortDateString() + "'";
                    read = com.ExecuteReader();
                    bool fnd = false;
                    string pdn_fol = "";
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            pdn_fol = read["pdn_folio"].ToString().Trim();
                            fnd = true;
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();

                    if (fnd == true)
                    {
                        com = conn.CreateCommand();
                        com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_exp WHERE pdn_folio in (" + pdn_fol + ") and pdn_tipo = 'EXP'";
                        read = com.ExecuteReader();
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                str = read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        com.Dispose();
                    }
                    else
                    {
                        com = conn.CreateCommand();
                        com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_exp WHERE pdn_folio in (" + folio + ") and pdn_tipo = 'EXP'";
                        read = com.ExecuteReader();
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                str = read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        com.Dispose();
                    }
                }
                else
                {
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT pdn_folio FROM tb_mstr_facturas_nal WHERE fcn_folio in (" + folio + ") and fcn_tipo = '" + val_tipo + "' AND fcn_fecha >= '" + DateTime.Now.AddDays(60).ToShortDateString() + "'";
                    read = com.ExecuteReader();
                    bool fnd = false;
                    string pdn_fol = "";
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            pdn_fol = read["pdn_folio"].ToString().Trim();
                            fnd = true;
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();

                    if (fnd == true)
                    {
                        com = conn.CreateCommand();
                        com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_nal WHERE pdn_folio in (" + pdn_fol + ") and pdn_tipo = 'NAL'";
                        read = com.ExecuteReader();
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                str = read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        com.Dispose();
                    }
                    else
                    {
                        com = conn.CreateCommand();
                        com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_nal WHERE pdn_folio in (" + folio + ") and pdn_tipo = 'NAL'";
                        read = com.ExecuteReader();
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                str = read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        com.Dispose();
                    }
                }
            }
            catch (SqlException ex)
            {
                str = "";
            }
            conn.Close();

            return str;
        }

        public void enviarcorreo(string nombre, string cedis, string fecha, string queja, string cveprod, string nomprod, string problema, string cliente, string pedido, string costo)
        {
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string cuerpo = "<table border='2'><tr><td align='center'><h2>Registro de Queja</h2></td></tr><tr><td>Registro de queja realizado por: " + nombre + 
                "</td></tr><tr><td>CEDIS: " + cedis + "</td></tr><tr><td>Fecha: " + fecha + 
                "</td></tr><tr><td>Queja: No. " + queja + "</td></tr><tr><td>Clave y nombre producto: " + cveprod + " " + nomprod + 
                "</td></tr><tr><td>Problema: " + problema + "</td></tr><tr><td>Cliente: " + cliente + 
                "</td></tr><tr><td>Pedido: " + pedido + "</td></tr><tr><td>Costo: " + costo + "</td></tr></table><br />" +
                "<p><h4>Ir a la seccion de Editar Queja para asignaci&oacute; de &Aacute;rea</h4></p><br /><p>Entrar al sistema de quejas</p>" +
                "<br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add("msamano@mrlucky.com.mx");
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + queja;
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
            }
        }

        public void enviarcorreoexp(string nombre, string cedis, string fecha, string queja, string problema, string cliente, string pedido)
        {
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string cuerpo = "<table border='2'><tr><td align='center'><h2>Registro de Queja</h2></td></tr>" +
                "<tr><td>Registro de queja realizado por: " + nombre + "</td></tr>" +
                "<tr><td>CEDIS: " + cedis + "</td></tr>" +
                "<tr><td>Fecha: " + fecha + "</td></tr>" +
                "<tr><td>Queja: No. " + queja + "</td></tr>" +
                "<tr><td>Problema: " + problema + "</td></tr>" +
                "<tr><td>Cliente: " + cliente + "</td></tr>" +
                "<tr><td>Pedido: " + pedido + "</td></tr></table><br />" +
                "<p><h4>Ir a la seccion de Editar Queja para asignaci&oacute; de &Aacute;rea</h4></p><br /><p>Entrar al sistema de quejas</p>" +
                "<br />Enlace dentro de instalaciones de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciones de Comercializadora GAB: " + str1;
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add("msamano@mrlucky.com.mx");
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + queja;
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
            }
        }

        public void enviarcorreo_error(string responsable, string cve_queja, string cuerpo)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + cve_queja;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = cuerpo;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.From = new MailAddress("sistemas@mrlucky.com.mx");
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential("sistemas", "sipgab");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "mail1.mrlucky.com.mx";
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
            }
        }

        [WebMethod]
        public List<ItemPlacas> ComboPlacas(string fecha)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NoTrailer", typeof(string));
            dataTable.Columns.Add("PdnFolio", typeof(string));
            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand com;
            SqlDataReader read;

            com = conn.CreateCommand();
            com.CommandText = "select DISTINCT(A.no_trailer), B.destino, B.chofer, B.transporte from tb_mstr_embarque A INNER JOIN tb_mstr_trailer B ON A.hora_trailer = B.hora_trailer AND A.no_trailer = B.no_trailer WHERE B.fecha = '" + fecha + "'";
            read = com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["NoTrailer"] = (object)"SELECCIONE TRANSPORTE...";
            row1["PdnFolio"] = (object)"0";
            dataTable.Rows.Add(row1);
            if (read.HasRows)
            {
                int i = 0;
                while (read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["NoTrailer"] = (object)(read["no_trailer"].ToString().Trim() + " - " + read["destino"].ToString().Trim() + " - " + read["chofer"].ToString().Trim() + " - " + read["transporte"].ToString().Trim());
                    if (read["no_trailer"].ToString().Trim() == "PC")
                        row2["PdnFolio"] = (object)read["no_trailer"].ToString().Trim() + i++.ToString();
                    else
                        row2["PdnFolio"] = (object)read["no_trailer"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            read.Close();
            read.Dispose();
            com.Dispose();
            conn.Close();

            List<ItemPlacas> lista = new List<ItemPlacas>();
            foreach (DataRow row in dataTable.Rows)
            {
                lista.Add(new ItemPlacas
                {
                    Trailer = row["NoTrailer"].ToString(),
                    Pedido = row["PdnFolio"].ToString()
                });
            }

            return lista;
        }

        [WebMethod]
        public List<RowProductosTrailer> DetalleEmbarqueConcentrado(string folio, string placa, string fecha, string tipo, string chofer)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("pdn_folio", typeof(string));
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("Folio", typeof(string));//pdn_folio
            dataTable2.Columns.Add("Prod", typeof(string));//prod_clave
            dataTable2.Columns.Add("ProdNom", typeof(string));//prod_nombre
            dataTable2.Columns.Add("Cajas", typeof(string));//cajas
            dataTable2.Columns.Add("CNTE", typeof(string));//cnte
            dataTable2.Columns.Add("Fact", typeof(string));//fact

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand com;
            SqlDataReader read;

            if (placa == "PC")
            {
                string ht = "";
                com = conn.CreateCommand();
                com.CommandText = "SELECT hora_trailer FROM tb_mstr_trailer WHERE fecha = '" + fecha + "' AND no_trailer = '" + placa + "' AND chofer = '" + chofer + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    read.Read();
                    ht = read["hora_trailer"].ToString();
                }
                read.Close();
                read.Dispose();
                com.Dispose();

                com = conn.CreateCommand();
                com.CommandText = "SELECT emb_folio FROM tb_mstr_embarque WHERE no_trailer = '" + placa + "' AND hora_trailer = '" + ht + "'";

            }
            else
            {
                com = conn.CreateCommand();
                com.CommandText = "SELECT emb_folio FROM tb_mstr_embarque WHERE no_trailer = '" + placa + "' AND hora_trailer like '" + fecha + "%'";
            }

            read = com.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    DataRow row = dataTable1.NewRow();
                    row["pdn_folio"] = (object)read["emb_folio"].ToString().Trim();
                    dataTable1.Rows.Add(row);
                }
            }
            read.Close();
            read.Dispose();
            com.Dispose();
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable1.Rows)
            {
                if (tipo == "E" || tipo == "EXPORTACION")
                {
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT A.prod_clave, B.prod_nombre, SUM(A.cajas) AS cajas, C.cnte_clave, D.fcn_folio FROM tb_det_embarque A " +
                        "INNER JOIN tb_cat_producto b ON A.prod_clave = B.prod_clave " +
                        "JOIN tb_mstr_pedidos_exp C ON C.pdn_folio = A.emb_folio AND C.pdn_tipo = A.emb_tipo " +
                        "JOIN tb_mstr_facturas_nal D ON D.pdn_folio = C.pdn_folio AND C.pdn_tipo = D.fcn_tipo " +
                        "WHERE A.emb_folio = '" + row1["pdn_folio"].ToString() + "' AND A.Estatus = 'A' " +
                        "GROUP BY A.prod_clave, B.prod_nombre, C.cnte_clave, D.fcn_folio ORDER BY B.prod_nombre";
                    read = com.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            DataRow row2 = dataTable2.NewRow();
                            row2["Folio"] = (object)row1["pdn_folio"].ToString();
                            row2["Prod"] = (object)read["prod_clave"].ToString().Trim();
                            row2["ProdNom"] = (object)read["prod_nombre"].ToString().Trim();
                            row2["Cajas"] = (object)read["cajas"].ToString().Trim();
                            row2["CNTE"] = (object)read["cnte_clave"].ToString().Trim();
                            row2["Fact"] = (object)read["fcn_folio"].ToString().Trim();
                            dataTable2.Rows.Add(row2);
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();
                }
                else
                {
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT A.prod_clave, B.prod_nombre, SUM(A.cajas) AS cajas, C.cnte_clave, D.fcn_folio FROM tb_det_embarque A " +
                        "INNER JOIN tb_cat_producto b ON A.prod_clave = B.prod_clave " +
                        "JOIN tb_mstr_pedidos_nal C ON C.pdn_folio = A.emb_folio AND C.pdn_tipo = A.emb_tipo " +
                        "JOIN tb_mstr_facturas_nal D ON D.pdn_folio = C.pdn_folio AND C.pdn_serie = D.fcn_tipo " +
                        "WHERE A.emb_folio = '" + row1["pdn_folio"].ToString() + "' AND A.Estatus = 'A' " +
                        "GROUP BY A.prod_clave, B.prod_nombre, C.cnte_clave, D.fcn_folio ORDER BY B.prod_nombre";
                    read = com.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            DataRow row2 = dataTable2.NewRow();
                            row2["Folio"] = (object)row1["pdn_folio"].ToString();
                            row2["Prod"] = (object)read["prod_clave"].ToString().Trim();
                            row2["ProdNom"] = (object)read["prod_nombre"].ToString().Trim();
                            row2["Cajas"] = (object)read["cajas"].ToString().Trim();
                            row2["CNTE"] = (object)read["cnte_clave"].ToString().Trim();
                            row2["Fact"] = (object)read["fcn_folio"].ToString().Trim(); //row2["fact"] = "";
                            dataTable2.Rows.Add(row2);
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();
                }

            }
            conn.Close();

            List<RowProductosTrailer> lista = new List<RowProductosTrailer>();
            foreach (DataRow row in dataTable2.Rows)
            {
                lista.Add(new RowProductosTrailer
                {
                    Folio = row["Folio"].ToString(),
                    Prod = row["Prod"].ToString(),
                    ProdNom = row["ProdNom"].ToString(),
                    Cajas = row["Cajas"].ToString(),
                    CNTE = row["CNTE"].ToString(),
                    Fact = row["Fact"].ToString()
                });
            }

            return lista;
        }

        [WebMethod]
        public List<RowDetalleEmbarque> DetalleEmbarqueDetalle(string folio, string prod, string cnte, string rechazo)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();

            dataTable2 = detalle_embarque_detalle(folio, prod, rechazo);



            List<RowDetalleEmbarque> lista = new List<RowDetalleEmbarque>();
            foreach (DataRow row in dataTable2.Rows)
            {
                lista.Add(new RowDetalleEmbarque
                {
                    Pedido = row["pedido"].ToString(),
                    IdProducto = row["prod_clave"].ToString(),
                    OrdProd = row["recibo"].ToString(),
                    Area = row["qud_area"].ToString(),
                    Responsable = row["qud_responsable"].ToString(),
                    CantRecha = row["rechazadas"].ToString(),
                    CantReci = row["cajas"].ToString(),
                    Unidad = row["qud_unidad"].ToString(),
                    Devolucion = "",
                    Moneda = "",
                    ProvCve = row["prov_clave"].ToString(),
                    RchCve = row["rch_clave"].ToString(),
                    TblCve = row["tbl_clave"].ToString(),
                    Lote = row["qud_lote"].ToString(),
                    NomProd = row["prod_nombre"].ToString(),
                    FechaCad = row["fec_cad"].ToString(),
                    Tarima = row["tarima"].ToString(),
                    ProvNom = row["prov_nombre"].ToString(),
                    RchNom = row["rch_nombre"].ToString(),
                    TblNom = row["tbl_nombre"].ToString(),
                    Tipo = row["tipo_rec"].ToString(),
                    Variedad = row["vari_nombre"].ToString(),
                    CNTE = cnte,
                    Rechazadas = rechazo,
                    Producidas = row["Producidas"].ToString(),
                    Porcentaje = row["Porcentaje"].ToString()
                });
            }

            return lista;



        }

        public DataTable detalle_embarque_detalle(string folio, string prod_clave, string CantRech)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("pedido", typeof(string));
            dataTable.Columns.Add("prod_clave", typeof(string));
            dataTable.Columns.Add("cajas", typeof(string));
            dataTable.Columns.Add("tarima", typeof(string));
            dataTable.Columns.Add("fec_cad", typeof(string));
            dataTable.Columns.Add("recibo", typeof(string));
            dataTable.Columns.Add("prod_nombre", typeof(string));
            dataTable.Columns.Add("qud_area", typeof(string));
            dataTable.Columns.Add("qud_responsable", typeof(string));
            dataTable.Columns.Add("qud_unidad", typeof(string));
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("rch_clave", typeof(string));
            dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("rch_nombre", typeof(string));
            dataTable.Columns.Add("tbl_nombre", typeof(string));
            dataTable.Columns.Add("qud_lote", typeof(string));
            dataTable.Columns.Add("tipo_rec", typeof(string));
            dataTable.Columns.Add("vari_nombre", typeof(string));
            dataTable.Columns.Add("producidas", typeof(string));
            dataTable.Columns.Add("porcentaje", typeof(string));
            dataTable.Columns.Add("rechazadas", typeof(string));

            SqlConnection conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            conn.Open();
            SqlCommand com;
            SqlDataReader read;

            com = conn.CreateCommand();
            folio = folio.PadLeft(6, '0').ToString();
            com.CommandText = "SELECT A.prod_clave, A.cajas, A.tarima, A.fec_cad, A.recibo, B.prod_nombre, A.tipo_rec FROM tb_det_embarque A INNER JOIN tb_cat_producto B ON A.prod_clave = B.prod_clave WHERE A.emb_folio = '" + folio + "' AND A.prod_clave = '" + prod_clave + "' ORDER BY B.prod_nombre";
            read = com.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["pedido"] = (object)folio;
                    row["prod_clave"] = (object)read["prod_clave"].ToString().Trim();
                    row["cajas"] = (object)read["cajas"].ToString().Trim();
                    row["tarima"] = (object)read["tarima"].ToString().Trim();
                    row["fec_cad"] = (object)read["fec_cad"].ToString().Trim();
                    row["recibo"] = (object)read["recibo"].ToString().Trim();
                    row["prod_nombre"] = (object)read["prod_nombre"].ToString().Trim();
                    row["qud_unidad"] = (object)"CAJAS";
                    row["tipo_rec"] = (object)read["tipo_rec"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            read.Close();
            read.Dispose();
            com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                com = conn.CreateCommand();
                com.CommandText = "SELECT prov_clave, prov_nombre, rch_clave, rch_nombre, tbl_clave, tbl_nombre, lote FROM tb_det_trazabilidad WHERE prod_clave = '" + row["prod_clave"].ToString() + "' AND recibo = '" + row["recibo"].ToString() + "' AND tarima = '" + row["tarima"].ToString() + "' AND tipo = '" + row["tipo_rec"].ToString() + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        row["prov_clave"] = (object)read["prov_clave"].ToString().Trim();
                        row["prov_nombre"] = (object)read["prov_nombre"].ToString().Trim();
                        row["rch_clave"] = (object)read["rch_clave"].ToString().Trim();
                        row["rch_nombre"] = (object)read["rch_nombre"].ToString().Trim();
                        row["tbl_clave"] = (object)read["tbl_clave"].ToString().Trim();
                        row["tbl_nombre"] = (object)read["tbl_nombre"].ToString().Trim();
                        row["qud_lote"] = (object)read["lote"].ToString().Trim();
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();

                //folio de produccion o campo
                com = conn.CreateCommand();
                com.CommandText = "SELECT inicio_campo FROM Tb_folio_campo";
                read = com.ExecuteReader();
                int fol_campo = 0;
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        fol_campo = Convert.ToInt32(read["inicio_campo"].ToString().Trim());
                        fol_campo = 0;
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();

                if (Convert.ToInt32(row["recibo"].ToString()) > fol_campo) //folio de campo
                {
                    //validar combo de problema
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT rpt_flete FROM tb_mstr_recepcion_pt WHERE rpt_recibo = '" + row["recibo"].ToString() + "'";
                    read = com.ExecuteReader();
                    int flete_campo = 0;
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            if (read["rpt_flete"].ToString().Trim() != "" && read["rpt_flete"].ToString().Trim() != "S/F" && read["rpt_flete"].ToString().Trim() != "AJUSTE")
                                flete_campo = Convert.ToInt32(read["rpt_flete"].ToString().Trim());
                            else
                                flete_campo = 0;
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();

                    //buscar flete

                    string responsable_flete = "";
                    if (flete_campo > 0)
                    {
                        MySqlConnection conexion = new MySqlConnection();
                        conexion.ConnectionString = "Server=gab.mrlucky.com.mx;Database=campo; Uid=www1166;Pwd=taQ17Zm;";
                        MySqlCommand comando;
                        MySqlDataReader lector;
                        conexion.Open();
                        comando = conexion.CreateCommand();
                        comando.CommandText = "SELECT B.nom_responsable FROM tb_mstr_flete A JOIN tb_cat_responsables B ON A.id_responsable = B.id_responsable WHERE A.id_flete = '" + flete_campo + "'";
                        lector = comando.ExecuteReader();
                        if (lector.HasRows)
                        {
                            lector.Read();
                            responsable_flete = lector["nom_responsable"].ToString().Trim();
                        }
                        lector.Close();
                        lector.Dispose();
                        comando.Dispose();
                        conexion.Close();

                        row["qud_area"] = "CAMPO";
                        row["qud_responsable"] = responsable_flete;
                    }
                    else
                    {
                        row["qud_area"] = "";
                        row["qud_responsable"] = responsable_flete;
                    }


                    //DataRow row = dataTable.NewRow();
                    //row["ordp_linea"] = "CAMPO";
                    //row["ordp_responsable"] = responsable_flete;
                    //dataTable.Rows.Add(row);
                }
                else
                {
                    if (row["tipo_rec"].ToString() == "PTC")
                    {
                        SqlConnection conn_agui = new SqlConnection();//200.76.124.19
                        conn_agui.ConnectionString = "Data Source=38.49.143.54\\SQL2014,2359; Initial Catalog=GAB_Empaque; Connect Timeout=130; User ID=SipGab; Password=Empaque1$; MultipleActiveResultSets=True";
                        SqlCommand comm_agui;
                        SqlDataReader read_agui;
                        conn_agui.Open();
                        comm_agui = conn_agui.CreateCommand();
                        comm_agui.CommandText = "SELECT ordp_linea, ordp_responsable FROM tb_mstr_ordenes_prod WHERE ordp_folio = '" + row["recibo"].ToString() + "'";
                        read_agui = comm_agui.ExecuteReader();
                        if (read_agui.HasRows)
                        {
                            while (read_agui.Read())
                            {
                                row["qud_area"] = read_agui["ordp_linea"].ToString().Trim();
                                row["qud_responsable"] = read_agui["ordp_responsable"].ToString().Trim();
                            }
                        }
                        read_agui.Close();
                        read_agui.Dispose();
                        comm_agui.Dispose();
                        conn_agui.Close();
                    }
                    else
                    {
                        com = conn.CreateCommand();
                        com.CommandText = "SELECT ordp_linea, ordp_responsable FROM tb_mstr_ordenes_prod WHERE ordp_folio = '" + row["recibo"].ToString() + "'";
                        read = com.ExecuteReader();
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                row["qud_area"] = (object)read["ordp_linea"].ToString().Trim();
                                row["qud_responsable"] = (object)read["ordp_responsable"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        com.Dispose();
                    }

                }


            }

            conn.Close();

            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["tipo_rec"].ToString() == "PTC")
                {
                    string str = this.variedad_recepcion_pt(row["recibo"].ToString());
                    row["vari_nombre"] = (object)str;
                }
                if (row["tipo_rec"].ToString() == "PTP")
                {
                    string rec = this.variedad_busca_eti_final(row["recibo"].ToString(), row["prod_clave"].ToString());
                    string str1 = this.variedad_busca_prod_odp(row["recibo"].ToString(), rec);
                    string str2 = "";
                    if (str1 == "MP")
                        str2 = this.variedad_recepcion_mp(rec);
                    if (str1 == "REM")
                    {
                        string[] strArray = this.variedad_prod_ode_tipo(rec).Split('-');
                        if (strArray[1] == "PTP")
                            str2 = this.variedad_recepcion_pt(strArray[0]);
                        if (strArray[1] == "PTC")
                            str2 = this.variedad_recepcion_mp(strArray[0]);
                    }
                    if (str1 == "ESP")
                        str2 = this.variedad_recepcion_esparrago(rec);
                    row["vari_nombre"] = (object)str2;
                }
            }

            int num1 = Convert.ToInt32(CantRech);
            int num2 = 0;
            int num3 = 0;
            //DataTable dataTable4 = dataTable2;
            //string filterExpression = "id_producto = '" + prod_clave + "' AND pedido = '" + folio.PadLeft(6, '0').ToString() + "'";
            //foreach (DataRow dataRow2 in dataTable4.Select(filterExpression))
            foreach (DataRow dataRow2 in dataTable.Rows)
            {
                num2 += Convert.ToInt32(dataRow2["cajas"].ToString());
                int num4 = num1 - Convert.ToInt32(dataRow2["cajas"].ToString());
                if (num4 <= 0)
                    ++num3;
                if (num4 > 0)
                {
                    dataRow2["rechazadas"] = (object)dataRow2["cajas"].ToString();
                    num1 = num4;
                }
                else if (num3 == 1)
                {
                    int num5 = num4 + Convert.ToInt32(dataRow2["cajas"].ToString());
                    dataRow2["rechazadas"] = (object)num5;
                    num1 = num5 - Convert.ToInt32(dataRow2["cajas"].ToString());
                }
                else
                    dataRow2["rechazadas"] = (object)"0";
            }

            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num4;
                if (row2["tipo_rec"].ToString() == "PTP")
                {
                    string str3 = row2["rechazadas"].ToString();
                    row2["producidas"] = this.cajas_producidas_folio2(row2["recibo"].ToString(), row2["prod_clave"].ToString(), row2["tarima"].ToString());
                    DataRow dataRow2 = row2;
                    num4 = Convert.ToDecimal(str3) * new Decimal(100) / Convert.ToDecimal(row2["producidas"].ToString());
                    string str4 = num4.ToString("0.00");
                    dataRow2["porcentaje"] = (object)str4;
                }
                if (row2["tipo_rec"].ToString() == "PTC")
                {
                    string str3 = row2["rechazadas"].ToString();
                    row2["producidas"] = this.cajas_producidas_folio(row2["recibo"].ToString(), row2["prod_clave"].ToString());
                    DataRow dataRow2 = row2;
                    num4 = Convert.ToDecimal(str3) * new Decimal(100) / Convert.ToDecimal(row2["producidas"].ToString());
                    string str4 = num4.ToString("0.00");
                    dataRow2["porcentaje"] = (object)str4;
                }
            }


            return dataTable;
        }
        
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void SubirArchivos()
        {
            HttpContext context = HttpContext.Current;
            HttpFileCollection files = context.Request.Files;

            if (files.Count > 0)
            {
                HttpPostedFile archivo1 = files["archivo1"];
                HttpPostedFile archivo2 = files["archivo2"];
                HttpPostedFile archivo3 = files["archivo3"];
                if (archivo1 != null && archivo1.ContentLength > 0)
                {
                    string ruta = context.Server.MapPath("~/fotos_ant/");
                    string nombreFinal = Path.GetFileName(archivo1.FileName); // o usa otro nombre si lo deseas
                    archivo1.SaveAs(Path.Combine(ruta, nombreFinal));
                }
                if (archivo2 != null && archivo2.ContentLength > 0)
                {
                    string ruta = context.Server.MapPath("~/fotos_ant/");
                    string nombreFinal = Path.GetFileName(archivo2.FileName); // o usa otro nombre si lo deseas
                    archivo2.SaveAs(Path.Combine(ruta, nombreFinal));
                }
                if (archivo3 != null && archivo3.ContentLength > 0)
                {
                    string ruta = context.Server.MapPath("~/fotos_ant/");
                    string nombreFinal = Path.GetFileName(archivo3.FileName); // o usa otro nombre si lo deseas
                    archivo3.SaveAs(Path.Combine(ruta, nombreFinal));
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.Write("{\"success\":true}");

            //HttpPostedFile archivo1 = HttpContext.Current.Request.Files["archivo1"];

            //string ruta = HttpContext.Current.Server.MapPath("~/fotos_ant/");

            //if (archivo1 != null && archivo1.ContentLength > 0)
            //    archivo1.SaveAs(Path.Combine(ruta, Path.GetFileName(archivo1.FileName)));

        }

        public class ItemCombo
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
        }

        public class RowOrdenProduccion
        {
            public string Clave { get; set; }
            public string Nombre { get; set; }
            public string Fecha { get; set; }
            public string Prov { get; set; }
            public string ProvNom { get; set; }
            public string Ranch { get; set; }
            public string RanchNom { get; set; }
            public string Tabla { get; set; }
            public string TablaNom { get; set; }
            public string FechaCad { get; set; }
            public string Tipo { get; set; }
            public string Lote { get; set; }
        }

        public class RowDatosOrden
        {
            public string Linea { get; set; }
            public string Responsable { get; set; }
        }

        public class RowVariedad
        {
            public string Variedad { get; set; }
        }

        public class RowProducidas
        {
            public string Producidas { get; set; }
        }

        public class ItemProblemas
        {
            public string Clave { get; set; }
            public string Nombre { get; set; }
        }

        public class Fecha2
        {
            public int Dia;
            public int Mes;
            public int Anio;

            public Fecha2(int dia, int mes, int anio)
            {
                Dia = dia;
                Mes = mes;
                Anio = anio;
            }

            public string AgregarUnDia()
            {
                int[] diasPorMes = new int[] { 31, EsBisiesto(Anio) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

                int nuevoDia = Dia + 1;
                int nuevoMes = Mes;
                int nuevoAnio = Anio;

                //Dia++;

                if (nuevoDia > diasPorMes[Mes - 1])
                {
                    nuevoDia = 1;
                    nuevoMes++;
                    if (nuevoMes > 12)
                    {
                        nuevoMes = 1;
                        nuevoAnio++;
                    }
                }

                return nuevoDia.ToString("D2") + "/" + nuevoMes.ToString("D2") + "/" + nuevoAnio;
            }

            private bool EsBisiesto(int anio)
            {
                return (anio % 4 == 0 && anio % 100 != 0) || (anio % 400 == 0);
            }

            public override string ToString()
            {
                return Dia.ToString("D2") + "/" + Mes.ToString("D2") + "/" + Anio;
            }
        }

        public class ItemPlacas
        {
            public string Trailer { get; set; }
            public string Pedido { get; set; }
        }

        public class RowProductosTrailer
        {
            public string Folio { get; set; }
            public string Prod { get; set; }
            public string ProdNom { get; set; }
            public string Cajas { get; set; }
            public string CNTE { get; set; }
            public string Fact { get; set; }
        }

        public class RowDetalleEmbarque
        {
            public string Pedido { get; set; }
            public string IdProducto { get; set; }
            public string OrdProd { get; set; }
            public string Area { get; set; }
            public string Responsable { get; set; }
            public string CantRecha { get; set; }
            public string CantReci { get; set; }
            public string Unidad { get; set; }
            public string Devolucion { get; set; }
            public string Moneda { get; set; }
            public string ProvCve { get; set; }
            public string RchCve { get; set; }
            public string TblCve { get; set; }
            public string Lote { get; set; }
            public string NomProd { get; set; }
            public string FechaCad { get; set; }
            public string Tarima { get; set; }
            public string ProvNom { get; set; }
            public string RchNom { get; set; }
            public string TblNom { get; set; }
            public string Tipo { get; set; }
            public string Variedad { get; set; }
            public string CNTE { get; set; }
            public string Rechazadas { get; set; }
            public string Producidas { get; set; }
            public string Porcentaje { get; set; }
        }

        public class ListaProductos
        {
            public string Pedido { get; set; }
            public string ProdCve { get; set; }
            public string Producto { get; set; }
            public string OrdProd { get; set; }
            public string Tarima { get; set; }
            public string Lote { get; set; }
            public string Fecha { get; set; }
            public string CveProv { get; set; }
            public string Proveedor { get; set; }
            public string CveRch { get; set; }
            public string Rancho { get; set; }
            public string CveTbl { get; set; }
            public string Tabla { get; set; }
            public string Responsable { get; set; }
            public string Area { get; set; }
            public string Recibido { get; set; }
            public string Rechazadas { get; set; }
            public string Producidas { get; set; }
            public string Porcentaje { get; set; }
            public string Unidad { get; set; }
            public string Tipo { get; set; }
            public string Variedad { get; set; }
            public string CNTE { get; set; }
            public string Foli { get; set; }
            public string Prob { get; set; }
            public string Devo { get; set; }
            public string Merm { get; set; }
            public string Boni { get; set; }
            public string Noap { get; set; }
            public string Caus { get; set; }
            public string Mone { get; set; }
            public string Cons { get; set; }
        }

        public class ListaFacturas 
        {
            public string FolFact { get; set; }
            public string CanFact { get; set; }
        }

    }
}
