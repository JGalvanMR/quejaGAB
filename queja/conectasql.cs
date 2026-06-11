using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using MySql.Data.MySqlClient;

namespace queja
{
    public class conectasql
    {
        SqlConnection conn;
        SqlCommand com;
        SqlDataReader read;
        SqlConnection connUERP;
        SqlCommand comUERP;
        SqlDataReader readUERP;

        string cad = "Data Source=192.168.123.6,1433; GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240";

        public void Conexion()
        {
            this.conn = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
        }

        public void Abrir()
        {
            this.Conexion();
            this.conn.Open();
        }

        public void Cerrar()
        {
            this.conn.Close();
        }

        public void ConexionUERP()
        {
            this.connUERP = new SqlConnection("Data Source= GABIRAPUATO\\SQL2014,2353;Initial Catalog=UERP_Irapuato;Connect Timeout=130;User ID=uerp; Password=mocoro1$;  MultipleActiveResultSets=True");
        }

        public void AbrirUERP()
        {
            this.ConexionUERP();
            this.connUERP.Open();
        }

        public void CerrarUERP()
        {
            this.connUERP.Close();
        }

        public string validarusuario(string usu, string pas)
        {
            string str = "";
            try
            {
                Abrir();
                com = conn.CreateCommand();
                com.CommandText = "SELECT resp_clave, resp_nombre, resp_paterno, resp_cedis, resp_nivel_dos, resp_nivel_tres, resp_clave FROM tb_cat_responsables WHERE resp_usuario = '" + usu + "' AND resp_password = '" + pas + "'";
                read = this.com.ExecuteReader();
                if (read.HasRows)
                {
                    read.Read();
                    str = read["resp_clave"].ToString().Trim() + "-" + read["resp_nombre"].ToString().Trim() + " " + read["resp_paterno"].ToString().Trim() + "-" + read["resp_cedis"].ToString().Trim() + "-" + read["resp_nivel_dos"].ToString().Trim() + "-" + read["resp_nivel_tres"].ToString().Trim();
                }
                read.Close();
                read.Dispose();
                com.Dispose();
                conn.Close();
                Cerrar();
            }
            catch (SqlException ex)
            {
                Cerrar();
                return ex.Message;
            }
            return str;
        }

        public bool palabrasreservadas(string palabra)
        {
            bool flag = false;
            string[] strArray = new string[26]
            {
            "SELECT",
            "INSERT",
            "UPDATE",
            "DELETE",
            "1=1",
            "1 = 1",
            "ALTER",
            "CREATE",
            "DROP",
            "AND",
            "BEGIN",
            "EXEC",
            "EXECUTE",
            "IDENTITY",
            "INNER",
            "JOIN",
            "IN",
            "WHERE",
            "IF",
            "IGNORE",
            "LAST",
            "LEFT",
            "ORDER",
            "ROLLBACK",
            "TABLE",
            "UNION"
            };
            foreach (string str in strArray)
            {
                if (palabra.ToUpper() == str)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public DataTable buscaquejas(string empleado, string tipo)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(int));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_cliente", typeof(string));
            dataTable.Columns.Add("que_sucursal", typeof(string));
            dataTable.Columns.Add("que_reporto", typeof(string));
            dataTable.Columns.Add("que_recibio", typeof(string));
            dataTable.Columns.Add("que_status", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            dataTable.Columns.Add("que_etapa1", typeof(string));
            dataTable.Columns.Add("que_etapa2", typeof(string));
            dataTable.Columns.Add("que_etapa3", typeof(string));
            dataTable.Columns.Add("que_etapa4", typeof(string));
            dataTable.Columns.Add("que_etapa5", typeof(string));
            dataTable.Columns.Add("qud_variedad", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("ver", typeof(string));
            dataTable.Columns.Add("causas", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (tipo == "ADMINISTRADOR")
                this.com.CommandText = "SELECT DISTINCT(A.que_folio) AS que_folio, A.que_semana, A.que_fecha, A.que_cliente, A.que_sucursal, A.que_reporto, B.resp_nombre, B.resp_paterno, A.que_status, " +
                    "A.que_etapa1, A.que_etapa2, A.que_etapa3, A.que_etapa4, A.que_etapa5, E.area_nombre, A.que_pedido, " +
                    "(SELECT DISTINCT(D.pro_nombre) FROM tb_det_quejas C, tb_cat_problemas D WHERE C.qud_problema = D.pro_clave AND C.que_folio = A.que_folio) AS pro_nombre, " +
                    "(SELECT (CASE WHEN (SELECT count(DISTINCT(H.nom_producto)) FROM tb_det_quejas H WHERE H.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(I.nom_producto) FROM tb_det_quejas I " +
                    "WHERE I.que_folio = A.que_folio) ELSE '+ PRODUCTOS' END)) AS nom_producto,(SELECT (CASE WHEN (SELECT count(DISTINCT(F.qud_variedad)) FROM tb_det_quejas F " +
                    "WHERE F.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(G.qud_variedad) FROM tb_det_quejas G WHERE G.que_folio = A.que_folio) ELSE '+ VARIEDADES' END)) AS qud_variedad " +
                    "FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja E ON A.area_queja = E.id_area, tb_cat_responsables B WHERE A.que_recibio = B.resp_clave AND A.que_status IN ('A', 'T') " +
                    "AND A.que_fecha >= '" + DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy") + "'  ORDER BY A.que_folio DESC";
            else if (empleado == "100")
            {
                this.com.CommandText = "SELECT DISTINCT(A.que_folio) AS que_folio, A.que_semana, A.que_fecha, A.que_cliente, A.que_sucursal, A.que_reporto, B.resp_nombre, B.resp_paterno, A.que_status, " +
                    "A.que_etapa1, A.que_etapa2, A.que_etapa3, A.que_etapa4, A.que_etapa5, E.area_nombre, A.que_pedido, " +
                    "(SELECT DISTINCT(D.pro_nombre) FROM tb_det_quejas C, tb_cat_problemas D WHERE C.qud_problema = D.pro_clave AND C.que_folio = A.que_folio) AS pro_nombre, " +
                    "(SELECT (CASE WHEN (SELECT count(DISTINCT(H.nom_producto)) FROM tb_det_quejas H WHERE H.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(I.nom_producto) FROM tb_det_quejas I " +
                    "WHERE I.que_folio = A.que_folio) ELSE '+ PRODUCTOS' END)) AS nom_producto,(SELECT (CASE WHEN (SELECT count(DISTINCT(F.qud_variedad)) FROM tb_det_quejas F " +
                    "WHERE F.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(G.qud_variedad) FROM tb_det_quejas G WHERE G.que_folio = A.que_folio) ELSE '+ VARIEDADES' END)) AS qud_variedad " +
                    "FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja E ON A.area_queja = E.id_area, tb_cat_responsables B WHERE A.que_recibio = B.resp_clave AND A.que_status IN ('A', 'T') " +
                    "AND A.que_fecha >= '" + DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy") + "' AND A.cedis = 'AGUILARES' ORDER BY A.que_folio DESC";
            }
            else
                this.com.CommandText = "SELECT DISTINCT(A.que_folio) AS que_folio, A.que_semana, A.que_fecha, A.que_cliente, A.que_sucursal, A.que_reporto, B.resp_nombre, B.resp_paterno, A.que_status, A.que_etapa1, A.que_etapa2, A.que_etapa3, A.que_etapa4, A.que_etapa5, E.area_nombre, A.que_pedido, (SELECT DISTINCT(D.pro_nombre) FROM tb_det_quejas C, tb_cat_problemas D WHERE C.qud_problema = D.pro_clave AND C.que_folio = A.que_folio) AS pro_nombre, (SELECT (CASE WHEN (SELECT count(DISTINCT(H.nom_producto)) FROM tb_det_quejas H WHERE H.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(I.nom_producto) FROM tb_det_quejas I WHERE I.que_folio = A.que_folio) ELSE '+ PRODUCTOS' END)) AS nom_producto,(SELECT (CASE WHEN (SELECT count(DISTINCT(F.qud_variedad)) FROM tb_det_quejas F WHERE F.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(G.qud_variedad) FROM tb_det_quejas G WHERE G.que_folio = A.que_folio) ELSE '+ VARIEDADES' END)) AS qud_variedad FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja E ON A.area_queja = E.id_area, tb_cat_responsables B WHERE A.que_recibio = B.resp_clave AND A.resp_usuario = '" + empleado + "' AND A.que_status IN ('A', 'T') AND A.que_fecha >= '" + DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy") + "' ORDER BY A.que_folio DESC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = this.read["que_folio"];
                    row["que_semana"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_cliente"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["que_sucursal"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["que_reporto"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["que_recibio"] = (object)(this.read.GetValue(6).ToString().Trim() + " " + this.read.GetValue(7).ToString().Trim());
                    row["que_status"] = (object)this.read.GetValue(8).ToString().Trim();
                    row["area_nombre"] = (object)this.read.GetValue(14).ToString().Trim();
                    row["que_etapa1"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["que_etapa2"] = (object)this.read.GetValue(10).ToString().Trim();
                    row["que_etapa3"] = (object)this.read.GetValue(11).ToString().Trim();
                    row["que_etapa4"] = (object)this.read.GetValue(12).ToString().Trim();
                    row["que_etapa5"] = (object)this.read.GetValue(13).ToString().Trim();
                    if (this.read["nom_producto"].ToString().Trim() == "+ PRODUCTOS")
                        row["ver"] = (object)"+";
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                    row["qud_variedad"] = (object)this.read["qud_variedad"].ToString().Trim();
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["causas"] = "";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            foreach (DataRow rw in dataTable.Rows)
            {
                //VALIDAR SI YA FUE REALIZADA LA NOTA DE CREDITO O SI NO PROCEDIO
                string VALIDADO_NC_NO_PRODCEDE = "";
                com = conn.CreateCommand();
                com.CommandText = "SELECT que_notacredito FROM tb_mstr_quejas WHERE que_folio = '" + rw["que_folio"].ToString() + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if (read["que_notacredito"].ToString() == "1" || read["que_notacredito"].ToString() == "2")
                            VALIDADO_NC_NO_PRODCEDE = "0";
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();

                if (VALIDADO_NC_NO_PRODCEDE == "")
                {
                    //SI TIENE CAUSAS SE PINTA
                    com = conn.CreateCommand();
                    com.CommandText = "SELECT COUNT(inv_folio) AS conteo FROM tb_det_investigacion WHERE que_folio = '" + rw["que_folio"].ToString() + "' AND inv_causa <> ''";
                    read = com.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            rw["causas"] = read["conteo"].ToString();
                        }
                    }
                    read.Close();
                    read.Dispose();
                    com.Dispose();
                }
                else
                {
                    rw["causas"] = "0";
                }

                //SI ES POR CONSUMIDOR NO LA PINTA
                com = conn.CreateCommand();
                com.CommandText = "SELECT que_consumidor FROM tb_mstr_quejas WHERE que_folio = '" + rw["que_folio"].ToString() + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if (read["que_consumidor"].ToString() == "1")
                            rw["causas"] = "0";
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();
            }



            this.Cerrar();

            foreach (DataRow rw in dataTable.Rows)
            {
                //SE BUSCA QUE SEA DEVOLUCION O BONIFICACION
                string dev_bon_mer = notas_credito_devolucion_bonificacion(rw["que_folio"].ToString());
                if (dev_bon_mer.Contains("DEV"))
                {
                    rw["causas"] = "1";
                }
                if (dev_bon_mer.Contains("MER"))
                {
                    rw["causas"] = "2";
                }
                if (dev_bon_mer.Contains("BON"))
                {
                    rw["causas"] = "3";
                }
                if (dev_bon_mer.Contains("NA"))
                {
                    rw["causas"] = "4";
                }
            }

            return dataTable;
        }

        public DataTable buscaquejas2(string empleado)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(int));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_cliente", typeof(string));
            dataTable.Columns.Add("que_sucursal", typeof(string));
            dataTable.Columns.Add("que_reporto", typeof(string));
            dataTable.Columns.Add("que_recibio", typeof(string));
            dataTable.Columns.Add("que_status", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            dataTable.Columns.Add("que_etapa1", typeof(string));
            dataTable.Columns.Add("que_etapa2", typeof(string));
            dataTable.Columns.Add("que_etapa3", typeof(string));
            dataTable.Columns.Add("que_etapa4", typeof(string));
            dataTable.Columns.Add("que_etapa5", typeof(string));
            dataTable.Columns.Add("qud_variedad", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("ver", typeof(string));
            dataTable.Columns.Add("causas", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            SqlCommand com = this.com;
            string[] strArray1 = null;
            if (empleado == "100")
            {
                strArray1 = new string[4]
                {
                "SELECT DISTINCT(A.que_folio) AS que_folio, A.que_semana, A.que_fecha, A.que_cliente, A.que_sucursal, A.que_reporto, B.resp_nombre, B.resp_paterno, A.que_status, A.que_etapa1, A.que_etapa2, " +
                    "A.que_etapa3, A.que_etapa4, A.que_etapa5, F.area_nombre, A.que_pedido, (SELECT DISTINCT(G.pro_nombre) FROM tb_det_quejas H, tb_cat_problemas G " +
                    "WHERE H.qud_problema = G.pro_clave AND H.que_folio = A.que_folio) AS pro_nombre, (SELECT (CASE WHEN (SELECT count(DISTINCT(I.nom_producto)) FROM tb_det_quejas I WHERE I.que_folio = A.que_folio) = 1 " +
                    "THEN (SELECT DISTINCT(J.nom_producto) FROM tb_det_quejas J WHERE J.que_folio = A.que_folio) ELSE '+ PRODUCTOS' END)) AS nom_producto, (SELECT (CASE WHEN (SELECT count(DISTINCT(K.qud_variedad)) " +
                    "FROM tb_det_quejas K WHERE K.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(L.qud_variedad) FROM tb_det_quejas L WHERE L.que_folio = A.que_folio) ELSE '+ VARIEDADES' END)) AS qud_variedad " +
                    "FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja F ON A.area_queja = F.id_area, tb_cat_responsables B, tb_det_investigacion C WHERE A.que_status = 'A' " +
                    "AND A.que_recibio = B.resp_clave AND A.que_folio = C.que_folio AND C.inv_responsable in ('81', '82') ",
                " AND A.que_status = 'A'  AND A.que_fecha >= '",
                null,
                null
                };
            }
            else
            {
                strArray1 = new string[5]
                {
                "SELECT DISTINCT(A.que_folio) AS que_folio, A.que_semana, A.que_fecha, A.que_cliente, A.que_sucursal, A.que_reporto, B.resp_nombre, B.resp_paterno, A.que_status, A.que_etapa1, A.que_etapa2, " +
                    "A.que_etapa3, A.que_etapa4, A.que_etapa5, F.area_nombre, A.que_pedido, (SELECT DISTINCT(G.pro_nombre) FROM tb_det_quejas H, tb_cat_problemas G " +
                    "WHERE H.qud_problema = G.pro_clave AND H.que_folio = A.que_folio) AS pro_nombre, (SELECT (CASE WHEN (SELECT count(DISTINCT(I.nom_producto)) FROM tb_det_quejas I WHERE I.que_folio = A.que_folio) = 1 " +
                    "THEN (SELECT DISTINCT(J.nom_producto) FROM tb_det_quejas J WHERE J.que_folio = A.que_folio) ELSE '+ PRODUCTOS' END)) AS nom_producto, (SELECT (CASE WHEN (SELECT count(DISTINCT(K.qud_variedad)) " +
                    "FROM tb_det_quejas K WHERE K.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(L.qud_variedad) FROM tb_det_quejas L WHERE L.que_folio = A.que_folio) ELSE '+ VARIEDADES' END)) AS qud_variedad " +
                    "FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja F ON A.area_queja = F.id_area, tb_cat_responsables B, tb_det_investigacion C WHERE A.que_status = 'A' " +
                    "AND A.que_recibio = B.resp_clave AND A.que_folio = C.que_folio AND C.inv_responsable = '",
                empleado,
                "' AND A.que_status = 'A'  AND A.que_fecha >= '",
                null,
                null
                };
            }

            string[] strArray2 = strArray1;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMonths(-3);
            string shortDateString = dateTime.ToString("dd/MM/yyyy");
            if (strArray2.Length == 5)
            {
                strArray2[3] = shortDateString;
                strArray1[4] = "' ORDER BY A.que_folio DESC";
            }
            else
            {
                strArray2[2] = shortDateString;
                strArray1[3] = "' ORDER BY A.que_folio DESC";
            }

            string str = string.Concat(strArray1);
            com.CommandText = str;
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = this.read["que_folio"];
                    row["que_semana"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_cliente"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["que_sucursal"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["que_reporto"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["que_recibio"] = (object)(this.read.GetValue(6).ToString().Trim() + " " + this.read.GetValue(7).ToString().Trim());
                    row["que_status"] = (object)this.read.GetValue(8).ToString().Trim();
                    row["area_nombre"] = (object)this.read.GetValue(14).ToString().Trim();
                    row["que_etapa1"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["que_etapa2"] = (object)this.read.GetValue(10).ToString().Trim();
                    row["que_etapa3"] = (object)this.read.GetValue(11).ToString().Trim();
                    row["que_etapa4"] = (object)this.read.GetValue(12).ToString().Trim();
                    row["que_etapa5"] = (object)this.read.GetValue(13).ToString().Trim();
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    if (this.read["nom_producto"].ToString().Trim() == "+ PRODUCTOS")
                        row["ver"] = (object)"+";
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                    row["qud_variedad"] = (object)this.read["qud_variedad"].ToString().Trim();
                    row["causas"] = (object)"";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            foreach (DataRow rw in dataTable.Rows)
            {
                com = conn.CreateCommand();
                com.CommandText = "SELECT COUNT(inv_folio) AS conteo FROM tb_det_investigacion WHERE que_folio = '" + rw["que_folio"].ToString() + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        rw["causas"] = read["conteo"].ToString();
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();


                com = conn.CreateCommand();
                com.CommandText = "SELECT que_consumidor FROM tb_mstr_quejas WHERE que_folio = '" + rw["que_folio"].ToString() + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if (read["que_consumidor"].ToString() == "1")
                            rw["causas"] = "0";
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();
            }
            this.Cerrar();

            foreach (DataRow rw in dataTable.Rows)
            {
                //SE BUSCA QUE SEA DEVOLUCION O BONIFICACION
                string dev_bon_mer = notas_credito_devolucion_bonificacion(rw["que_folio"].ToString());
                if (dev_bon_mer.Contains("MER"))
                {
                    rw["causas"] = "0";
                }
            }

            return dataTable;
        }

        public DataTable buscaquejas3(string empleado)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(int));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_cliente", typeof(string));
            dataTable.Columns.Add("que_sucursal", typeof(string));
            dataTable.Columns.Add("que_reporto", typeof(string));
            dataTable.Columns.Add("que_recibio", typeof(string));
            dataTable.Columns.Add("que_status", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            dataTable.Columns.Add("que_etapa1", typeof(string));
            dataTable.Columns.Add("que_etapa2", typeof(string));
            dataTable.Columns.Add("que_etapa3", typeof(string));
            dataTable.Columns.Add("que_etapa4", typeof(string));
            dataTable.Columns.Add("que_etapa5", typeof(string));
            dataTable.Columns.Add("qud_variedad", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("ver", typeof(string));
            dataTable.Columns.Add("causas", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            SqlCommand com = this.com;
            string[] strArray1 = null;
            if (empleado == "100")
            {
                strArray1 = new string[4]
                {
                "SELECT DISTINCT(A.que_folio) AS que_folio, A.que_semana, A.que_fecha, A.que_cliente, A.que_sucursal, A.que_reporto, B.resp_nombre, B.resp_paterno, A.que_status, A.que_etapa1, A.que_etapa2, " +
                "A.que_etapa3, A.que_etapa4, A.que_etapa5, F.area_nombre, A.que_pedido,  (SELECT DISTINCT(G.pro_nombre) FROM tb_det_quejas H, tb_cat_problemas G " +
                "WHERE H.qud_problema = G.pro_clave AND H.que_folio = A.que_folio) AS pro_nombre, (SELECT (CASE WHEN (SELECT count(DISTINCT(I.nom_producto)) FROM tb_det_quejas I WHERE I.que_folio = A.que_folio) = 1 " +
                "THEN (SELECT DISTINCT(J.nom_producto) FROM tb_det_quejas J WHERE J.que_folio = A.que_folio) ELSE '+ PRODUCTOS' END)) AS nom_producto, (SELECT (CASE WHEN (SELECT count(DISTINCT(K.qud_variedad)) " +
                "FROM tb_det_quejas K WHERE K.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(L.qud_variedad) FROM tb_det_quejas L WHERE L.que_folio = A.que_folio) ELSE '+ VARIEDADES' END)) AS qud_variedad " +
                "FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja F ON A.area_queja = F.id_area, tb_cat_responsables B, tb_mstr_acciones C WHERE A.que_status = 'A' AND A.que_recibio = B.resp_clave " +
                "AND A.que_folio = C.que_folio AND C.acc_responsable in ('81', '82') ",
                "AND A.que_fecha >= '",
                null,
                null
                };
            }
            else
            {
                strArray1 = new string[5]
                {
                "SELECT DISTINCT(A.que_folio) AS que_folio, A.que_semana, A.que_fecha, A.que_cliente, A.que_sucursal, A.que_reporto, B.resp_nombre, B.resp_paterno, A.que_status, A.que_etapa1, A.que_etapa2, A.que_etapa3, A.que_etapa4, A.que_etapa5, F.area_nombre, A.que_pedido,  (SELECT DISTINCT(G.pro_nombre) FROM tb_det_quejas H, tb_cat_problemas G WHERE H.qud_problema = G.pro_clave AND H.que_folio = A.que_folio) AS pro_nombre, (SELECT (CASE WHEN (SELECT count(DISTINCT(I.nom_producto)) FROM tb_det_quejas I WHERE I.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(J.nom_producto) FROM tb_det_quejas J WHERE J.que_folio = A.que_folio) ELSE '+ PRODUCTOS' END)) AS nom_producto, (SELECT (CASE WHEN (SELECT count(DISTINCT(K.qud_variedad)) FROM tb_det_quejas K WHERE K.que_folio = A.que_folio) = 1 THEN (SELECT DISTINCT(L.qud_variedad) FROM tb_det_quejas L WHERE L.que_folio = A.que_folio) ELSE '+ VARIEDADES' END)) AS qud_variedad   FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja F ON A.area_queja = F.id_area, tb_cat_responsables B, tb_mstr_acciones C WHERE A.que_status = 'A' AND A.que_recibio = B.resp_clave AND A.que_folio = C.que_folio AND C.acc_responsable = '",
                empleado,
                "'   AND A.que_fecha >= '",
                null,
                null
                };
            }

            string[] strArray2 = strArray1;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMonths(-3);
            //string[] data = dateTime.ToShortDateString().Split('/');
            //string da1 = data[1];
            //string da2 = data[0];
            //string da3 = data[2];
            //string shortDateString = da1 + "/" + da2 + "/" + da3;
            string shortDateString = dateTime.ToString("dd/MM/yyyy");
            if (strArray2.Length == 5)
            {
                strArray2[3] = shortDateString;
                strArray1[4] = "'  ORDER BY A.que_folio DESC";
            }
            else
            {
                strArray2[2] = shortDateString;
                strArray1[3] = "'  ORDER BY A.que_folio DESC";
            }

            string str = string.Concat(strArray1);
            com.CommandText = str;
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = this.read["que_folio"];
                    row["que_semana"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_cliente"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["que_sucursal"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["que_reporto"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["que_recibio"] = (object)(this.read.GetValue(6).ToString().Trim() + " " + this.read.GetValue(7).ToString().Trim());
                    row["que_status"] = (object)this.read.GetValue(8).ToString().Trim();
                    row["nom_producto"] = (object)"";
                    row["pro_nombre"] = (object)"";
                    row["area_nombre"] = (object)this.read.GetValue(14).ToString().Trim();
                    row["que_etapa1"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["que_etapa2"] = (object)this.read.GetValue(10).ToString().Trim();
                    row["que_etapa3"] = (object)this.read.GetValue(11).ToString().Trim();
                    row["que_etapa4"] = (object)this.read.GetValue(12).ToString().Trim();
                    row["que_etapa5"] = (object)this.read.GetValue(13).ToString().Trim();
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["causas"] = (object)"";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(B.pro_nombre) As pro_nombre FROM tb_det_quejas A, tb_cat_problemas B WHERE A.qud_problema = B.pro_clave AND A.que_folio = '" + row["que_folio"].ToString() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(nom_producto) FROM tb_det_quejas WHERE que_folio = '" + row["que_folio"].ToString() + "'";
                int num1 = 0;
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                        ++num1;
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                if (num1 > 1)
                {
                    row["ver"] = (object)" + ";
                    row["nom_producto"] = (object)"+ PRODUCTOS";
                }
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(qud_variedad) FROM tb_det_quejas WHERE que_folio = '" + row["que_folio"].ToString() + "'";
                int num2 = 0;
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        row["qud_variedad"] = (object)this.read["qud_variedad"].ToString().Trim();
                        ++num2;
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                if (num2 > 1)
                    row["qud_variedad"] = (object)"+ VARIEDADES";
            }

            this.Cerrar();


            Abrir();
            foreach (DataRow rw in dataTable.Rows)
            {
                com = conn.CreateCommand();
                com.CommandText = "SELECT COUNT(inv_folio) AS conteo FROM tb_det_investigacion WHERE que_folio = '" + rw["que_folio"].ToString() + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        rw["causas"] = read["conteo"].ToString();
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();


                com = conn.CreateCommand();
                com.CommandText = "SELECT que_consumidor FROM tb_mstr_quejas WHERE que_folio = '" + rw["que_folio"].ToString() + "'";
                read = com.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if (read["que_consumidor"].ToString() == "1")
                            rw["causas"] = "0";
                    }
                }
                read.Close();
                read.Dispose();
                com.Dispose();
            }
            Cerrar();

            foreach (DataRow rw in dataTable.Rows)
            {
                //SE BUSCA QUE SEA DEVOLUCION O BONIFICACION
                string dev_bon_mer = notas_credito_devolucion_bonificacion(rw["que_folio"].ToString());
                if (dev_bon_mer.Contains("MER"))
                {
                    rw["causas"] = "0";
                }
            }

            return dataTable;
        }

        public DataTable cargarproblemas()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("pro_clave", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT pro_clave, pro_nombre FROM tb_cat_problemas WHERE pro_estatus = 'A' ORDER BY pro_nombre";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["pro_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["pro_nombre"] = (object)this.read.GetValue(1).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable cargarordenprod(string folio)
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
            this.Abrir();
            this.com = this.conn.CreateCommand();
            SqlCommand com = this.com;
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
            com.CommandText = str;
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prod_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["prod_nombre"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["pti_fecha"] = (object)Convert.ToDateTime(this.read.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["prov_clave"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["prov_nombre"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["rch_clave"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["rch_nombre"] = (object)this.read.GetValue(6).ToString().Trim();
                    row["tbl_clave"] = (object)this.read.GetValue(7).ToString().Trim();
                    row["tbl_nombre"] = (object)this.read.GetValue(8).ToString().Trim();
                    row["fecha_cad"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["tipo"] = (object)this.read.GetValue(10).ToString().Trim();
                    row["lote"] = (object)this.read.GetValue(11).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable cargadatosorden(string folio, string tipo_recibo)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ordp_linea", typeof(string));
            dataTable.Columns.Add("ordp_responsable", typeof(string));
            try
            {
                this.Abrir();

                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT inicio_campo FROM Tb_folio_campo";
                this.read = this.com.ExecuteReader();
                int fol_campo = 0;
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        fol_campo = Convert.ToInt32(read["inicio_campo"].ToString().Trim());
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                if (Convert.ToInt32(folio) > fol_campo) //folio de campo
                {
                    //validar combo de problema
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT rpt_flete FROM tb_mstr_recepcion_pt WHERE rpt_recibo = '" + folio + "'";
                    this.read = this.com.ExecuteReader();
                    int flete_campo = 0;
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            if (read["rpt_flete"].ToString().Trim() == "S/F")
                                flete_campo = 0;
                            else
                                flete_campo = (read["rpt_flete"].ToString().Trim() == "") ? 0 : Convert.ToInt32(read["rpt_flete"].ToString().Trim());
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
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
                            this.com = this.conn.CreateCommand();
                            this.com.CommandText = "SELECT rpt_evaluador FROM tb_mstr_recepcion_pt WHERE rpt_recibo = '" + folio + "'";
                            this.read = this.com.ExecuteReader();
                            if (this.read.HasRows)
                            {
                                while (this.read.Read())
                                {
                                    DataRow row = dataTable.NewRow();
                                    row["ordp_linea"] = "";
                                    row["ordp_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                                    dataTable.Rows.Add(row);
                                }
                            }
                            this.read.Close();
                            this.read.Dispose();
                            this.com.Dispose();
                        }

                    }
                    else
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT ordp_linea, ordp_responsable FROM tb_mstr_ordenes_prod WHERE ordp_folio = '" + folio + "'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                DataRow row = dataTable.NewRow();
                                row["ordp_linea"] = (object)this.read.GetValue(0).ToString().Trim();
                                row["ordp_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                                dataTable.Rows.Add(row);
                            }
                        }
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();
                    }

                }


                this.Cerrar();
            }
            catch (Exception ex)
            {
                DataRow row = dataTable.NewRow();
                row["ordp_linea"] = "";
                row["ordp_responsable"] = ex.ToString();
                dataTable.Rows.Add(row);
            }


            return dataTable;
        }

        public int cargafolio()
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT MAX(que_folio) AS maximo FROM tb_mstr_quejas";
            this.read = this.com.ExecuteReader();
            int num = 0;
            bool flag = false;
            if (this.read.HasRows)
            {
                this.read.Read();
                num = this.read.GetValue(0).ToString().Trim() == "" ? 1 : Convert.ToInt32(this.read.GetValue(0).ToString().Trim());
                if (this.read.GetValue(0).ToString().Trim() != "")
                    flag = true;
            }
            this.com.Dispose();
            this.Cerrar();
            if (flag)
                ++num;
            return num;
        }

        public int cargafolio_serv()
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT MAX(que_folio) AS maximo FROM tb_mstr_quejas_serv";
            this.read = this.com.ExecuteReader();
            int num = 0;
            bool flag = false;
            if (this.read.HasRows)
            {
                this.read.Read();
                num = this.read.GetValue(0).ToString().Trim() == "" ? 1 : Convert.ToInt32(this.read.GetValue(0).ToString().Trim());
                if (this.read.GetValue(0).ToString().Trim() != "")
                    flag = true;
            }
            this.com.Dispose();
            this.Cerrar();
            if (flag)
                ++num;
            return num;
        }

        public int cargasemana()
        {
            Abrir();
            com = conn.CreateCommand();
            DateTime ahora = DateTime.Now;
            string dia = ahora.Day.ToString().PadLeft(2, '0');
            string mes = ahora.Month.ToString().PadLeft(2, '0');
            string anio = ahora.Year.ToString();
            string f_1 = dia + "/" + mes + "/" + anio;
            string f_2 = "";
            com.CommandText = "SELECT semana from tb_cat_semanas WHERE '" + f_1 + "' >= fecha1  and '" + f_1 + "' <= fecha2";
            read = com.ExecuteReader();
            if (read.HasRows)
            {
                read.Read();
                f_2 = read["semana"].ToString().Trim();
            }
            read.Close();
            read.Dispose();
            com.Dispose();

            //CultureInfo cultureInfo = new CultureInfo("es-MX");
            //string sem = cultureInfo.Calendar.GetWeekOfYear(DateTime.Now, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek).ToString();

            return Convert.ToInt32(f_2);



        }

        public int cargasemana_embarque(string fecha)
        {
            Abrir();
            com = conn.CreateCommand();
            //DateTime ahora = DateTime.Now;
            //string dia = ahora.Day.ToString().PadLeft(2, '0');
            //string mes = ahora.Month.ToString().PadLeft(2, '0');
            //string anio = ahora.Year.ToString();
            //string f_1 = dia + "/" + mes + "/" + anio;
            string f_2 = "";
            com.CommandText = "SELECT semana from tb_cat_semanas WHERE '" + fecha + "' >= fecha1  and '" + fecha + "' <= fecha2";
            read = com.ExecuteReader();
            if (read.HasRows)
            {
                read.Read();
                f_2 = read["semana"].ToString().Trim();
            }
            read.Close();
            read.Dispose();
            com.Dispose();

            //CultureInfo cultureInfo = new CultureInfo("es-MX");
            //string sem = cultureInfo.Calendar.GetWeekOfYear(DateTime.Now, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek).ToString();

            return Convert.ToInt32(f_2);



        }

        public DataTable cargarclientes()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CliCod", typeof(string));
            dataTable.Columns.Add("CliRSocial", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT cli_folio, cli_nombre FROM tb_quejas_clientes ORDER BY cli_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["CliCod"] = (object)"";
            row1["CliRSocial"] = (object)"SELECCIONAR CLIENTE...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["CliCod"] = (object)this.read["cli_folio"].ToString().Trim();
                    row2["CliRSocial"] = (object)this.read["cli_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable cargarsucursales(string folio, string cedis)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("subcli_folio", typeof(string));
            dataTable.Columns.Add("subcli_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT subcli_folio, subcli_nombre FROM tb_queja_subclientes WHERE cli_folio = '" + folio + "' AND subcli_cedis = '" + cedis + "' AND estatus = '1' ORDER BY subcli_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["subcli_folio"] = (object)"";
            row1["subcli_nombre"] = (object)"ELEGIR SUCURSAL";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["subcli_folio"] = (object)this.read["subcli_folio"].ToString().Trim();
                    row2["subcli_nombre"] = (object)this.read["subcli_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            else
                dataTable.Clear();
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertQuejas(
          string folio,
          string semana,
          string fecha,
          string mes,
          string cliprim,
          string cliente,
          string sucursal,
          string reporto,
          string recibio,
          string producto,
          string problema,
          string ordprod,
          string area,
          string responsable,
          string cantrecha,
          string cantreci,
          string unidad,
          string devolucion,
          string moneda,
          string cveprov,
          string cverch,
          string cvetbl,
          string lote,
          string nomprod,
          string fechacad,
          string causa,
          string cedis,
          string usuario,
          string tipo,
          string fechainv,
          string area_queja,
          string subcli_folio,
          string ptc_ptp,
          string varie,
          string pedido,
          string observaciones,
          string nom_realiza,
          string cjs_prod,
          string porcen,
          string costo,
          string merma,
          string cnte,
          string consumidor, string bonificacion, string noaplica, string sememb, string fechaemb)
        {
            string cuerpo = "";
            string str1 = "";
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                if (consumidor == "0")
                {
                    cuerpo = "INSERT INTO tb_mstr_quejas(que_semana, que_fecha, que_mes, que_cliprim, que_cliente, que_sucursal, que_reporto, que_recibio, que_status, cedis, resp_usuario, que_tipo, que_fechainv, area_queja, " +
                    "subcli_folio, que_pedido, que_observacion, que_costo, que_cnte, que_consumidor, que_sememb, que_fechaemb) " +
                    "VALUES('" + semana + "', '" + fecha + "', '" + mes + "', '" + cliprim + "', '" + cliente + "', '" + sucursal + "', '" + reporto + "', '" + recibio + "', 'A', '" + cedis + "', " +
                    "'" + usuario + "', '" + tipo + "', '" + fechainv + "', '" + area_queja + "', '" + subcli_folio + "', '" + pedido + "', '" + observaciones + "', '" + costo + "', '" + cnte + "', '" + consumidor + "', '" + sememb + "', '" + fechaemb + "') SELECT SCOPE_IDENTITY()";
                }
                else
                {
                    cuerpo = "INSERT INTO tb_mstr_quejas(que_semana, que_fecha, que_mes, que_cliprim, que_cliente, que_sucursal, que_reporto, que_recibio, que_status, cedis, resp_usuario, que_tipo, que_fechainv, area_queja, " +
                    "subcli_folio, que_pedido, que_observacion, que_costo, que_cnte, que_consumidor) " +
                    "VALUES('" + semana + "', '" + fecha + "', '" + mes + "', '" + cliprim + "', '" + cliente + "', '" + sucursal + "', '" + reporto + "', '" + recibio + "', 'A', '" + cedis + "', " +
                    "'" + usuario + "', '" + tipo + "', '" + fechainv + "', '" + area_queja + "', '" + subcli_folio + "', '" + pedido + "', '" + observaciones + "', '" + costo + "', '" + cnte + "', '" + consumidor + "') SELECT SCOPE_IDENTITY()";
                }
                this.com.CommandText = cuerpo;
                str1 = "1";
                string str2 = Convert.ToString(this.com.ExecuteScalar()).Trim();
                this.com.Dispose();
                if (Convert.ToInt32(str2) > Convert.ToInt32(folio))
                    folio = str2;
                string str3 = "INSERT INTO tb_det_quejas(que_folio, id_producto, qud_problema, qud_ordprod, qud_area, qud_responsable, qud_cantrecha, qud_cantreci, qud_unidad, qud_devolucion, " +
                    "qud_moneda, prov_clave, rch_clave, tbl_clave, qud_variedad, qud_lote, nom_producto, fecha_cad, qud_causa, qud_ptcptp, qud_cjsprod, qud_porcen, qud_merma, qud_cnte, qud_pedido, qud_bonificacion, qud_rechazado, qud_noaplica) " +
                    "VALUES('" + folio + "', '" + producto + "', '" + problema + "', '" + ordprod + "', '" + area + "', '" + responsable + "', '" + cantrecha + "', '" + cantreci + "', '" + unidad + "', " +
                    "'" + devolucion + "', '" + moneda + "', '" + cveprov + "', '" + cverch + "', '" + cvetbl + "', '" + varie + "', '" + lote + "', '" + nomprod.Replace("'", " ").ToString() + "', " +
                    "'" + fechacad + "', '" + causa + "', '" + ptc_ptp + "', '" + cjs_prod + "', '" + porcen + "', '" + merma + "', '" + cnte + "', '" + pedido + "', '" + bonificacion + "', '" + cantrecha + "', '" + noaplica + "')";
                cuerpo = cuerpo + " DETALLE:" + str3;
                this.com = this.conn.CreateCommand();
                this.com.CommandText = str3;
                str1 = "2";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str1 = "";
                return str1 + "-" + folio;
            }
            catch (SqlException ex)
            {
                this.enviarcorreo_error(nom_realiza, "NUEVO FOLIO: " + folio, cuerpo);
                this.Cerrar();
                return str1;
            }
        }

        public DataTable consultaqueja(string valor)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_mes", typeof(string));
            dataTable.Columns.Add("que_cliprim", typeof(string));
            dataTable.Columns.Add("que_cliente", typeof(string));
            dataTable.Columns.Add("que_sucursal", typeof(string));
            dataTable.Columns.Add("que_reporto", typeof(string));
            dataTable.Columns.Add("que_recibio", typeof(string));
            dataTable.Columns.Add("que_status", typeof(string));
            dataTable.Columns.Add("cedis", typeof(string));
            dataTable.Columns.Add("recibio", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("area_queja", typeof(string));
            dataTable.Columns.Add("subcli_folio", typeof(string));
            dataTable.Columns.Add("que_fecha_efe", typeof(string));
            dataTable.Columns.Add("que_cumpli_efe", typeof(string));
            dataTable.Columns.Add("que_comen_efe", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            dataTable.Columns.Add("que_costo", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("que_transporte", typeof(string));
            dataTable.Columns.Add("que_observacion", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT area_queja FROM tb_mstr_quejas WHERE que_folio = '" + valor + "'";
            this.read = this.com.ExecuteReader();
            this.read.Read();
            string str = this.read["area_queja"].ToString().Trim();
            this.read.Close();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            if (str != "0")
            {
                this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliprim, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.que_status, A.cedis, B.resp_nombre, B.resp_paterno, A.que_tipo, A.area_queja, A.subcli_folio, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, C.area_nombre, A.que_costo, A.que_pedido, A.que_transporte, A.que_observacion FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE A.que_folio = '" + valor + "' AND A.que_recibio = B.resp_clave  AND A.area_queja = C.id_area";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        DataRow row = dataTable.NewRow();
                        row["que_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                        row["que_semana"] = (object)this.read.GetValue(1).ToString().Trim();
                        row["que_fecha"] = (object)this.read.GetValue(2).ToString().Trim();
                        row["que_mes"] = (object)this.read.GetValue(3).ToString().Trim();
                        row["que_cliprim"] = (object)this.read.GetValue(4).ToString().Trim();
                        row["que_cliente"] = (object)this.read.GetValue(5).ToString().Trim();
                        row["que_sucursal"] = (object)this.read.GetValue(6).ToString().Trim();
                        row["que_reporto"] = (object)this.read.GetValue(7).ToString().Trim();
                        row["que_recibio"] = (object)this.read.GetValue(8).ToString().Trim();
                        row["que_status"] = (object)this.read.GetValue(9).ToString().Trim();
                        row["cedis"] = (object)this.read.GetValue(10).ToString().Trim();
                        row["recibio"] = (object)(this.read.GetValue(11).ToString().Trim() + " " + this.read.GetValue(12).ToString().Trim());
                        row["que_tipo"] = (object)this.read.GetValue(13).ToString().Trim();
                        row["area_queja"] = (object)this.read["area_queja"].ToString().Trim();
                        row["subcli_folio"] = (object)this.read["subcli_folio"].ToString().Trim();
                        row["que_fecha_efe"] = this.read["que_fecha_efe"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["que_fecha_efe"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                        row["que_cumpli_efe"] = (object)this.read["que_cumpli_efe"].ToString().Trim();
                        row["que_comen_efe"] = (object)this.read["que_comen_efe"].ToString().Trim();
                        row["area_nombre"] = (object)this.read["area_nombre"].ToString().Trim();
                        row["que_costo"] = (object)this.read["que_costo"].ToString().Trim();
                        row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                        row["que_transporte"] = (object)this.read["que_transporte"].ToString().Trim();
                        row["que_observacion"] = (object)this.read["que_observacion"].ToString().Trim();
                        dataTable.Rows.Add(row);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            else
            {
                this.com.CommandText = "SELECT DISTINCT(A.que_folio), A.que_semana, A.que_fecha, A.que_mes, A.que_cliprim, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.que_status, A.cedis, B.resp_nombre, B.resp_paterno, A.que_tipo, A.area_queja, A.subcli_folio, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, A.que_costo, A.que_pedido, A.que_transporte, A.que_observacion FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE A.que_folio = '" + valor + "' AND A.que_recibio = B.resp_clave";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        DataRow row = dataTable.NewRow();
                        row["que_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                        row["que_semana"] = (object)this.read.GetValue(1).ToString().Trim();
                        row["que_fecha"] = (object)this.read.GetValue(2).ToString().Trim();
                        row["que_mes"] = (object)this.read.GetValue(3).ToString().Trim();
                        row["que_cliprim"] = (object)this.read.GetValue(4).ToString().Trim();
                        row["que_cliente"] = (object)this.read.GetValue(5).ToString().Trim();
                        row["que_sucursal"] = (object)this.read.GetValue(6).ToString().Trim();
                        row["que_reporto"] = (object)this.read.GetValue(7).ToString().Trim();
                        row["que_recibio"] = (object)this.read.GetValue(8).ToString().Trim();
                        row["que_status"] = (object)this.read.GetValue(9).ToString().Trim();
                        row["cedis"] = (object)this.read.GetValue(10).ToString().Trim();
                        row["recibio"] = (object)(this.read.GetValue(11).ToString().Trim() + " " + this.read.GetValue(12).ToString().Trim());
                        row["que_tipo"] = (object)this.read.GetValue(13).ToString().Trim();
                        row["area_queja"] = (object)this.read["area_queja"].ToString().Trim();
                        row["subcli_folio"] = (object)this.read["subcli_folio"].ToString().Trim();
                        row["que_fecha_efe"] = this.read["que_fecha_efe"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["que_fecha_efe"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                        row["que_cumpli_efe"] = (object)this.read["que_cumpli_efe"].ToString().Trim();
                        row["que_comen_efe"] = (object)this.read["que_comen_efe"].ToString().Trim();
                        row["area_nombre"] = (object)"";
                        row["que_costo"] = (object)this.read["que_costo"].ToString().Trim();
                        row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                        row["que_transporte"] = (object)this.read["que_transporte"].ToString().Trim();
                        row["que_observacion"] = (object)this.read["que_observacion"].ToString().Trim();
                        dataTable.Rows.Add(row);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultadetqueja(string valor)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_area", typeof(string));
            dataTable.Columns.Add("qud_responsable", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("qud_unidad", typeof(string));
            dataTable.Columns.Add("qud_devolucion", typeof(string));
            dataTable.Columns.Add("qud_moneda", typeof(string));
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("rch_clave", typeof(string));
            dataTable.Columns.Add("rch_nombre", typeof(string));
            dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("tbl_nombre", typeof(string));
            dataTable.Columns.Add("qud_variedad", typeof(string));
            dataTable.Columns.Add("qud_lote", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("fecha_cad", typeof(string));
            dataTable.Columns.Add("qud_causa", typeof(string));
            dataTable.Columns.Add("problema", typeof(string));
            dataTable.Columns.Add("qud_tarima", typeof(string));
            dataTable.Columns.Add("qud_cjsprod", typeof(string));
            dataTable.Columns.Add("qud_porcen", typeof(string));
            dataTable.Columns.Add("qud_merma", typeof(string));
            dataTable.Columns.Add("qud_bonificacion", typeof(string));
            dataTable.Columns.Add("qud_ptcptp", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.id_producto, A.qud_problema, A.qud_ordprod, A.qud_area, A.qud_responsable, A.qud_cantrecha, A.qud_cantreci, A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.prov_clave, A.rch_clave, A.tbl_clave, A.qud_variedad, A.qud_lote, A.nom_producto, A.fecha_cad, A.qud_causa, B.pro_nombre, A.qud_tarima, A.qud_cjsprod, A.qud_porcen, A.qud_merma, A.qud_bonificacion, A.qud_ptcptp FROM tb_det_quejas A, tb_cat_problemas B WHERE A.que_folio = '" + valor + "' AND A.qud_problema = B.pro_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["id_producto"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["qud_problema"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["qud_ordprod"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["qud_area"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["qud_responsable"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read.GetValue(6).ToString().Trim();
                    row["qud_cantreci"] = (object)this.read.GetValue(7).ToString().Trim();
                    row["qud_unidad"] = (object)this.read.GetValue(8).ToString().Trim();
                    row["qud_devolucion"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["qud_moneda"] = (object)this.read.GetValue(10).ToString().Trim();
                    row["prov_clave"] = (object)this.read.GetValue(11).ToString().Trim();
                    row["rch_clave"] = (object)this.read.GetValue(12).ToString().Trim();
                    row["tbl_clave"] = (object)this.read.GetValue(13).ToString().Trim();
                    row["qud_variedad"] = (object)this.read.GetValue(14).ToString().Trim();
                    row["qud_lote"] = (object)this.read.GetValue(15).ToString().Trim();
                    row["nom_producto"] = (object)this.read.GetValue(16).ToString().Trim();
                    row["fecha_cad"] = (object)this.read.GetValue(17).ToString().Trim();
                    row["qud_causa"] = (object)this.read.GetValue(18).ToString().Trim();
                    row["problema"] = (object)this.read.GetValue(19).ToString().Trim();
                    row["qud_tarima"] = (object)this.read["qud_tarima"].ToString().Trim();
                    row["qud_cjsprod"] = (object)this.read["qud_cjsprod"].ToString().Trim();
                    row["qud_porcen"] = (object)this.read["qud_porcen"].ToString().Trim();
                    row["qud_merma"] = (object)this.read["qud_merma"].ToString().Trim();
                    row["qud_bonificacion"] = (object)this.read["qud_bonificacion"].ToString().Trim();
                    row["qud_ptcptp"] = (object)this.read["qud_ptcptp"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                string str1 = row["prov_clave"].ToString();
                string str2 = row["rch_clave"].ToString();
                string str3 = row["tbl_clave"].ToString();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor WHERE prov_clave = '" + str1 + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["prov_nombre"] = (object)this.read.GetValue(0).ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT rch_nombre FROM tb_cat_ranchos WHERE prov_clave = '" + str1 + "' AND rch_clave = '" + str2 + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["rch_nombre"] = (object)this.read.GetValue(0).ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT tbl_nombre FROM tb_cat_tablas WHERE prov_clave = '" + str1 + "' AND rch_clave = '" + str2 + "' AND tbl_clave = '" + str3 + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["tbl_nombre"] = (object)this.read.GetValue(0).ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return dataTable;
        }

        public string buscaproveedor(string prov)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor WHERE prov_clave = '" + prov + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string buscarancho(string prov, string ranc)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT rch_nombre FROM tb_cat_ranchos WHERE prov_clave = '" + prov + "' AND rch_clave = '" + ranc + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string buscatabla(string prov, string ranc, string tabl)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT tbl_nombre FROM tb_cat_tablas WHERE prov_clave = '" + prov + "' AND rch_clave = '" + ranc + "' AND tbl_clave = '" + tabl + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public bool cancelarqueja(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_status = 'C' WHERE que_folio = '" + folio + "'";
            bool flag1;
            try
            {
                this.com.ExecuteNonQuery();
                flag1 = true;
            }
            catch
            {
                bool flag2 = false;
                this.com.Dispose();
                this.Cerrar();
                return flag2;
            }
            this.com.Dispose();
            this.Cerrar();
            return flag1;
        }

        public int operaciones(string folio)
        {
            int num = 0;
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_status FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                num = !(this.read.GetValue(0).ToString().Trim() == "C") ? 2 : 1;
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            if (num >= 2)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT que_folio FROM tb_det_investigacion WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    num = 3;
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return num;
        }

        public DataTable cargaempleados()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("emp_clave", typeof(string));
            dataTable.Columns.Add("emp_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT emp_clave, emp_nombre, emp_paterno, emp_materno FROM tb_cat_empleados ORDER BY emp_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["emp_clave"] = (object)"0";
            row1["emp_nombre"] = (object)"ELIGE UNA OPCIÓN...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["emp_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row2["emp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable cargaresponsables()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("resp_clave", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_clave, resp_nombre, resp_paterno, resp_materno FROM tb_cat_responsables where resp_estatus = '1' ORDER BY resp_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["resp_clave"] = (object)"0";
            row1["resp_nombre"] = (object)"ELIGE UNA OPCIÓN...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["resp_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row2["resp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable cargaresponsables2()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("resp_clave", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_clave, resp_nombre, resp_paterno, resp_materno FROM tb_cat_responsables WHERE resp_nivel = '2' AND resp_estatus = '1' ORDER BY resp_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["resp_clave"] = (object)"0";
            row1["resp_nombre"] = (object)"ELIGE UNA OPCIÓN...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["resp_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row2["resp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertinvestigacion(
          string folio,
          string responsable,
          string fecharegistro,
          string fechaentrega,
          string comentario)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_det_investigacion (que_folio, inv_responsable, inv_fecharegistro, inv_fechaentrega, inv_comentario, inv_causa, inv_fechaaccion) VALUES ('" + folio + "', '" + responsable + "', '" + fecharegistro + "', '" + fechaentrega + "', '" + comentario + "', '', '" + fechaentrega + "')";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public void deleteinvestigacion(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "DELETE FROM tb_det_investigacion WHERE inv_folio = '" + folio + "'";
            this.com.ExecuteNonQuery();
            this.Cerrar();
        }

        public string causa(string folio)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT qud_causa FROM tb_det_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable resp_accion(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("emp_clave", typeof(string));
            dataTable.Columns.Add("emp_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT B.emp_clave, B.emp_nombre, B.emp_paterno, B.emp_materno FROM tb_mstr_acciones A, tb_cat_empleados B WHERE A.acc_responsable = B.emp_clave AND A.que_folio = '" + folio + "' ORDER BY B.emp_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["emp_clave"] = (object)"0";
            row1["emp_nombre"] = (object)"ELIGE UNA OPCIÓN...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["emp_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row2["emp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertaccion(
          string folio_que,
          string responsable,
          string resp,
          string fecha,
          string accion)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_mstr_acciones (que_folio, acc_responsable, resp_clave, acc_fechatermino, acc_accion, acc_fecha) VALUES ('" + folio_que + "', '" + responsable + "', '" + resp + "', '" + fecha + "', '" + accion + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "')";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string updateaccion(string folio_que, string responsable, string fecha, string accion)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_acciones SET acc_fechatermino = '" + fecha + "', acc_accion = '" + accion + "' WHERE que_folio = '" + folio_que + "' AND acc_responsable = '" + responsable + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable consultaacciones(string folio, string responsable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.que_folio, A.acc_responsable, B.resp_nombre, A.acc_fechatermino, A.acc_accion FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.resp_clave = '" + responsable + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["que_folio"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["resp_nombre"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["acc_fechatermino"] = this.read.GetValue(4).ToString().Trim() == "" ? (object)"" : (object)Convert.ToDateTime(this.read.GetValue(4).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["acc_accion"] = (object)this.read.GetValue(5).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultainvestigacion(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("inv_fechaentrega", typeof(string));
            dataTable.Columns.Add("inv_cumplimiento", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_folio, inv_responsable, inv_fechaentrega, inv_cumplimiento, inv_comentario FROM tb_det_investigacion WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["inv_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["inv_fechaentrega"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["inv_cumplimiento"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["inv_comentario"] = (object)this.read.GetValue(4).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insert_verificacion(
          string folio,
          string responsable,
          string fecha,
          string cumplimiento,
          string comentario)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_det_verificacion(que_folio, acc_responsable, ver_fecha, ver_cumplimiento, ver_comentario)VALUES('" + folio + "', '" + responsable + "', '" + fecha + "', '" + cumplimiento + "', '" + comentario + "')";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string insert_efectividad(
          string folio,
          string responsable,
          string fecha,
          string cumplimiento,
          string comentario)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_acciones SET acc_fecha_efe = '" + fecha + "', acc_cumplimiento_efe = '" + cumplimiento + "', acc_comentario_efe = '" + comentario + "'WHERE que_folio = '" + folio + "' AND acc_responsable = '" + responsable + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable consultaverificacion(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("emp_nombre", typeof(string));
            dataTable.Columns.Add("ver_fecha", typeof(string));
            dataTable.Columns.Add("ver_cumplimiento", typeof(string));
            dataTable.Columns.Add("ver_comentario", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_responsable, B.emp_nombre, B.emp_paterno, A.ver_fecha, A.ver_cumplimiento, A.ver_comentario FROM tb_det_verificacion A, tb_cat_empleados B WHERE A.acc_responsable = B.emp_clave AND A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["emp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim());
                    row["ver_fecha"] = this.read.GetValue(3).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(3).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["ver_cumplimiento"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["ver_comentario"] = (object)this.read.GetValue(5).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultaefectividad(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("emp_nombre", typeof(string));
            dataTable.Columns.Add("acc_fecha_efe", typeof(string));
            dataTable.Columns.Add("acc_cumplimiento_efe", typeof(string));
            dataTable.Columns.Add("acc_comentario_efe", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_responsable, B.emp_nombre, A.acc_fecha_efe, A.acc_cumplimiento_efe, A.acc_comentario_efe FROM tb_mstr_acciones A, tb_cat_empleados B WHERE A.acc_responsable = B.emp_clave AND A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["emp_nombre"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_fecha_efe"] = this.read.GetValue(2).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumplimiento_efe"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["acc_comentario_efe"] = (object)this.read.GetValue(4).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string concluirqueja(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_status = 'T' WHERE que_folio = '" + folio + "'";
            string str;
            try
            {
                this.com.ExecuteNonQuery();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable consultainvestigadores(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("emp_nombre", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            dataTable.Columns.Add("inv_fechaentrega", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.inv_folio, A.inv_responsable, B.resp_nombre, B.resp_paterno, A.inv_comentario, A.inv_fechaentrega FROM tb_det_investigacion A, tb_cat_responsables B WHERE A.inv_responsable = B.resp_clave AND A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_folio"] = (object)this.read["inv_folio"].ToString().Trim();
                    row["inv_responsable"] = (object)this.read["inv_responsable"].ToString().Trim();
                    row["emp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["inv_comentario"] = (object)this.read["inv_comentario"].ToString().Trim();
                    row["inv_fechaentrega"] = (object)Convert.ToDateTime(this.read["inv_fechaentrega"].ToString().Trim()).ToString("dd/MM/yyyy");
                    //row["inv_fechaentrega"] = (object)this.read["inv_fechaentrega"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultainvestigadores_causas(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("emp_nombre", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            dataTable.Columns.Add("inv_fechaentrega", typeof(string));
            dataTable.Columns.Add("inv_fecharegistro", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.inv_folio, A.inv_responsable, B.resp_nombre, B.resp_paterno, A.inv_comentario, A.inv_fechaentrega, A.inv_fecharegistro FROM tb_det_investigacion A, tb_cat_responsables B WHERE A.inv_responsable = B.resp_clave AND A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["inv_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["emp_nombre"] = (object)(this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    row["inv_comentario"] = (object)this.read.GetValue(4).ToString().Trim();
                    DataRow dataRow1 = row;
                    DateTime dateTime = Convert.ToDateTime(this.read.GetValue(5).ToString().Trim());
                    string shortDateString1 = dateTime.ToString("dd/MM/yyyy");
                    dataRow1["inv_fechaentrega"] = (object)shortDateString1;
                    DataRow dataRow2 = row;
                    dateTime = Convert.ToDateTime(this.read.GetValue(6).ToString().Trim());
                    string shortDateString2 = dateTime.ToString("dd/MM/yyyy");
                    dataRow2["inv_fecharegistro"] = (object)shortDateString2;
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string eliminainvestigador(string folio, string resp)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "DELETE FROM tb_det_investigacion WHERE inv_responsable = '" + resp + "' AND que_folio = '" + folio + "'";
            string str;
            try
            {
                this.com.ExecuteNonQuery();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string consultacorreo(string resp)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_correo FROM tb_cat_responsables WHERE resp_clave = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable consultarespaccion(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("emp_nombre", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_responsable, B.emp_nombre, A.acc_accion, A.acc_fechatermino FROM tb_mstr_acciones A, tb_cat_empleados B WHERE A.acc_responsable = B.emp_clave AND A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["emp_nombre"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_accion"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["acc_fechatermino"] = (object)Convert.ToDateTime(this.read.GetValue(3).ToString().Trim()).ToString("dd/MM/yyyy");
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string eliminaaccion(string folio, string resp)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "DELETE FROM tb_mstr_acciones WHERE acc_responsable = '" + resp + "' AND que_folio = '" + folio + "'";
            string str;
            try
            {
                this.com.ExecuteNonQuery();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string eliminaverificacion(string folio, string resp)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "DELETE FROM tb_det_verificacion WHERE acc_responsable = '" + resp + "' AND que_folio = '" + folio + "'";
            string str;
            try
            {
                this.com.ExecuteNonQuery();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable consultaetapas(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_etapa1", typeof(string));
            dataTable.Columns.Add("que_etapa2", typeof(string));
            dataTable.Columns.Add("que_etapa3", typeof(string));
            dataTable.Columns.Add("que_etapa4", typeof(string));
            dataTable.Columns.Add("que_etapa5", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_folio, que_etapa1, que_etapa2, que_etapa3, que_etapa4, que_etapa5 FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["que_etapa1"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["que_etapa2"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["que_etapa3"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["que_etapa4"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["que_etapa5"] = (object)this.read.GetValue(5).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string clientequeja(string folio)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_cliente FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable datosresponsable(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("resp_cedis", typeof(string));
            dataTable.Columns.Add("resp_nivel_dos", typeof(string));
            dataTable.Columns.Add("resp_nivel_tres", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_nombre, resp_paterno, resp_cedis, resp_nivel_dos, resp_nivel_tres FROM tb_cat_responsables WHERE resp_clave = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["resp_nombre"] = (object)(this.read.GetValue(0).ToString().Trim() + " " + this.read.GetValue(1).ToString().Trim());
                    row["resp_cedis"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["resp_nivel_dos"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["resp_nivel_tres"] = (object)this.read.GetValue(3).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultainvestiga(string folio, string responsable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_fecharegistro", typeof(string));
            dataTable.Columns.Add("inv_fechaentrega", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT inv_fecharegistro, inv_fechaentrega, inv_comentario FROM tb_det_investigacion WHERE que_folio = '" + folio + "' AND inv_responsable = '" + responsable + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_fecharegistro"] = (object)Convert.ToDateTime(this.read.GetValue(0).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_fechaentrega"] = (object)Convert.ToDateTime(this.read.GetValue(1).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_comentario"] = (object)this.read.GetValue(2).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertcausa(string folio, string resp, string causa, string fecha_accion)
        {
            string str;
            try
            {
                this.Abrir();
                string f = DateTime.Now.ToString();
                string f_2 = Convert.ToDateTime(f).ToString("dd/MM/yyyy");
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_investigacion SET inv_causa = '" + causa + "', inv_fechaaccion = '" + fecha_accion + "', inv_causa_fecha = '" + f_2 + "' WHERE que_folio = '" + folio + "' AND inv_responsable = '" + resp + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable consultacausas(string folio, string responsable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("inv_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.inv_responsable, B.resp_nombre, B.resp_paterno, A.inv_causa FROM tb_det_investigacion A, tb_cat_responsables B WHERE A.inv_responsable = B.resp_clave AND que_folio = '" + folio + "' AND inv_responsable = '" + responsable + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim());
                    row["inv_causa"] = (object)this.read.GetValue(3).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public void actualizaetapa1(string folio, string valor)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_etapa1 = '" + valor + "' WHERE que_folio = '" + folio + "'";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public void actualizaetapa2(string folio, string valor)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_etapa2 = '" + valor + "' WHERE que_folio = '" + folio + "'";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public void actualizaetapa3(string folio, string valor)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_etapa3 = '" + valor + "' WHERE que_folio = '" + folio + "'";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public void actualizaetapa4(string folio, string valor)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_etapa4 = '" + valor + "' WHERE que_folio = '" + folio + "'";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public void actualizaetapa5(string folio, string valor)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_etapa5 = '" + valor + "' WHERE que_folio = '" + folio + "'";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public string fechaqueja(string folio)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_fechainv FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = Convert.ToDateTime(this.read.GetValue(0).ToString().Trim()).ToString("dd/MM/yyyy");
            }
            this.Cerrar();
            return str;
        }

        public string fecha_causa(string folio, string responsable)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_fechaentrega FROM tb_det_investigacion WHERE que_folio = '" + folio + "' AND inv_responsable = '" + responsable + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = Convert.ToDateTime(this.read.GetValue(0).ToString().Trim()).ToString("dd/MM/yyyy");
            }
            this.Cerrar();
            return str;
        }

        public string fecha_accion(string folio, string responsable)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT inv_fechaaccion FROM tb_det_investigacion WHERE que_folio = '" + folio + "' AND inv_responsable = '" + responsable + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = Convert.ToDateTime(this.read.GetValue(0).ToString().Trim()).ToString("dd/MM/yyyy");
            }
            this.Cerrar();
            return str;
        }

        public string consultacausa(string folio, string responsable)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT acc_causa FROM tb_mstr_acciones WHERE que_folio = '" + folio + "' AND acc_responsable = '" + responsable + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.Cerrar();
            return str;
        }

        public void insertRespAccion(
          string folio,
          string responsable,
          string resp,
          string causa,
          string folio_inv,
          string fecha_captura_acciones)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "INSERT INTO tb_mstr_acciones(que_folio, resp_clave, acc_responsable, acc_fecha, acc_causa, inv_folio) VALUES('" + folio + "', '" + responsable + "', '" + resp + "', '" + fecha_captura_acciones + "', '" + causa + "', '" + folio_inv + "')";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public string updateRespAccion(string folio, string responsable, string causa)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_acciones SET acc_responsable = '" + responsable + "', acc_causa = '" + causa + "' WHERE acc_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                this.Cerrar();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            return str;
        }

        public string correo_responsable(string responsable)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_correo FROM tb_cat_responsables WHERE resp_clave = '" + responsable + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.Cerrar();
            return str;
        }

        public string actualizaacciones(
          string folio,
          string responsable,
          string fecha,
          string accion,
          string cve_acc,
          string fecha_reg)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_det_acciones(acc_folio, acc_responsable, acc_accion, acc_fechatermino, que_folio, acc_fecha_reg) VALUES('" + responsable + "', '" + cve_acc + "', '" + accion + "', '" + fecha + "', '" + folio + "', '" + fecha_reg + "')";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string responsableinvestigacion(string folio, string resp)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.resp_clave, B.resp_nombre, B.resp_correo FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.resp_clave = B.resp_clave AND que_folio = '" + folio + "' AND acc_responsable = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim() + "-" + this.read.GetValue(1).ToString().Trim() + "-" + this.read.GetValue(2).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string fechaentregaacciones(string folio, string resp)
        {
            string str1 = "";
            string str2 = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_clave FROM tb_mstr_acciones WHERE que_folio = '" + folio + "' AND acc_responsable = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str1 = this.read.GetValue(0).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT inv_fechaaccion FROM tb_det_investigacion WHERE que_folio = '" + folio + "' AND inv_responsable = '" + str1 + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str2 = Convert.ToDateTime(this.read.GetValue(0).ToString().Trim()).ToString("dd/MM/yyyy");
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str2;
        }

        public string datosqueja(string folio)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.id_producto, A.nom_producto, B.pro_nombre FROM tb_det_quejas A, tb_cat_problemas B WHERE A.que_folio = '" + folio + "' AND A.qud_problema = B.pro_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim() + " " + this.read.GetValue(1).ToString().Trim() + "-" + this.read.GetValue(2).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string claveinvestigador(string folio, string resp)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_clave FROM tb_mstr_acciones WHERE que_folio = '" + folio + "' AND acc_responsable = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string cargadatosqueja(string folio)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_fecha FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable consultadatosinvestigador(string folio, string responsable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            dataTable.Columns.Add("inv_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.inv_responsable, B.resp_nombre, B.resp_paterno, A.inv_comentario, C.acc_causa FROM tb_det_investigacion A, tb_cat_responsables B, tb_mstr_acciones C WHERE A.inv_responsable = B.resp_clave AND A.que_folio = '" + folio + "' AND A.que_folio = C.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim());
                    row["inv_comentario"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["inv_causa"] = (object)this.read.GetValue(4).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultadatosacciones(string folio, string responsable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("resp_clave", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.resp_clave, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_accion, A.acc_fechatermino FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.acc_responsable = B.resp_clave AND A.resp_clave = '" + responsable + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_clave"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(3).ToString().Trim() + " " + this.read.GetValue(4).ToString().Trim());
                    row["acc_accion"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["acc_fechatermino"] = (object)Convert.ToDateTime(this.read.GetValue(6).ToString().Trim()).ToString("dd/MM/yyyy");
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertaverificacion(
          string folio,
          string fecha,
          string cumplimiento,
          string comentario)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_acciones SET acc_fecha_ver = '" + fecha + "', acc_cumpli_ver = '" + cumplimiento + "', acc_comen_ver = '" + comentario + "' WHERE acc_clave = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string cargadatosrespaccion(string folio, string accion)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT acc_responsable FROM tb_mstr_acciones WHERE que_folio = '" + folio + "' AND acc_folio = '" + accion + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string cargadatosrespaccion2(string folio, string accion)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT acc_responsable FROM tb_det_acciones WHERE acc_folio = '" + folio + "' AND acc_clave = '" + accion + "' AND acc_fecha_ver <> '' AND acc_cumpli_ver <> ''";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable consultadatosacciones_reporte(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("resp_clave", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.resp_clave, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_accion, A.acc_fechatermino FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_clave"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(3).ToString().Trim() + " " + this.read.GetValue(4).ToString().Trim());
                    row["acc_accion"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["acc_fechatermino"] = this.read.GetValue(6).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(6).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultadatosverificacion_reporte(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("resp_clave", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_fecha_ver", typeof(string));
            dataTable.Columns.Add("acc_cumplimiento_ver", typeof(string));
            dataTable.Columns.Add("acc_comentario_ver", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.resp_clave, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_fecha_ver, A.acc_cumplimiento_ver, A.acc_comentario_ver  FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_clave"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(3).ToString().Trim() + " " + this.read.GetValue(4).ToString().Trim());
                    row["acc_fecha_ver"] = this.read.GetValue(5).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(5).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumplimiento_ver"] = (object)this.read.GetValue(6).ToString().Trim();
                    row["acc_comentario_ver"] = (object)this.read.GetValue(7).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable clientes_semana()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("cantidad_quejas", typeof(string));
            dataTable.Columns.Add("cedis", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas group by cedis, que_semana ORDER BY que_semana, cedis";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_semana"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["cantidad_quejas"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["cedis"] = (object)this.read.GetValue(2).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string inserta_acciones(string folio, string resp, string accion, string fecha)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_det_acciones(acc_folio, acc_responsable, acc_accion, acc_fechatermino) VALUES('" + folio + "', '" + resp + "', '" + accion + "', '" + fecha + "')";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public void elimina_acciones(string folio, string resp)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "DELETE FROM tb_det_acciones WHERE acc_folio = '" + folio + "' AND acc_responsable = '" + resp + "'";
            this.com.ExecuteNonQuery();
            this.Cerrar();
        }

        public DataTable consultadatosacciones_MSTR(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_nombre", typeof(string));
            dataTable.Columns.Add("acc_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_causa FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.resp_clave = '" + resp + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_nombre"] = (object)(this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    row["acc_causa"] = (object)this.read.GetValue(4).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultadatosacciones_VER(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_nombre", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_accion, A.acc_fechatermino FROM tb__acciones A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_nombre"] = (object)(this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    row["acc_accion"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["acc_fechatermino"] = (object)Convert.ToDateTime(this.read.GetValue(5).ToString().Trim()).ToString("dd/MM/yyyy");
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable consultadatosacciones_ACC(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_clave", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_nombre", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_clave, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_accion, A.acc_fechatermino FROM tb_det_acciones A, tb_cat_responsables B WHERE A.acc_folio = '" + folio + "' AND A.acc_responsable = '" + resp + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_nombre"] = (object)(this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                    row["acc_accion"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["acc_fechatermino"] = (object)Convert.ToDateTime(this.read.GetValue(5).ToString().Trim()).ToString("dd/MM/yyyy");
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string verificacion_accion(string folio, string resp, string clave)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT acc_fecha_ver, acc_cumpli_ver, acc_comen_ver FROM tb_det_acciones WHERE acc_clave = '" + clave + "' AND acc_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                this.read.Read();
                str = Convert.ToDateTime(this.read.GetValue(0).ToString().Trim()).ToString("dd/MM/yyyy") + "*" + this.read.GetValue(1).ToString().Trim() + "*" + this.read.GetValue(2).ToString().Trim();
                this.Cerrar();
            }
            catch (SqlException ex)
            {
                str = "";
            }
            return str;
        }

        public DataTable consulta_acciones(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_clave", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_nombre", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_cumpli_ver", typeof(string));
            dataTable.Columns.Add("acc_comen_ver", typeof(string));
            dataTable.Columns.Add("acc_fecha_ver", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_clave, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_accion, A.acc_cumpli_ver, A.acc_comen_ver, A.acc_fecha_ver, A.acc_cumpli_efe FROM tb_det_acciones A, tb_cat_responsables B WHERE A.acc_folio = '" + folio + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    if (!(this.read.GetValue(5).ToString().Trim() != "") || !(this.read.GetValue(8).ToString().Trim() != ""))
                    {
                        DataRow row = dataTable.NewRow();
                        row["acc_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                        row["acc_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                        row["acc_nombre"] = (object)(this.read.GetValue(2).ToString().Trim() + " " + this.read.GetValue(3).ToString().Trim());
                        row["acc_accion"] = (object)this.read.GetValue(4).ToString().Trim();
                        row["acc_cumpli_ver"] = (object)this.read.GetValue(5).ToString().Trim();
                        row["acc_comen_ver"] = (object)this.read.GetValue(6).ToString().Trim();
                        row["acc_fecha_ver"] = this.read.GetValue(7).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(7).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                        dataTable.Rows.Add(row);
                    }
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertaefectividad(
          string folio,
          string clave_acc_det,
          string fecha,
          string cumplimiento,
          string comentario)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_acciones SET acc_fecha_efe = '" + fecha + "', acc_cumpli_efe = '" + cumplimiento + "', acc_comen_efe = '" + comentario + "' WHERE acc_clave = '" + clave_acc_det + "' AND acc_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable reporte_investigadores(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_clave", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            dataTable.Columns.Add("inv_fecha", typeof(string));
            dataTable.Columns.Add("inv_folio", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.inv_responsable, B.resp_nombre, B.resp_paterno, A.inv_comentario, A.inv_fecharegistro, A.inv_folio FROM tb_det_investigacion A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.inv_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim());
                    row["inv_comentario"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["inv_fecha"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["inv_folio"] = (object)this.read.GetValue(5).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable reporte_causas(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_causa", typeof(string));
            dataTable.Columns.Add("acc_folio", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_causa, A.acc_folio FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.que_folio = '" + folio + "' AND A.inv_folio = '" + resp + "' AND A.acc_responsable = B.resp_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim());
                    row["acc_causa"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["acc_folio"] = (object)this.read.GetValue(4).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable reporte_acciones(string folio, string resp, string acc_folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            dataTable.Columns.Add("acc_fecha_ver", typeof(string));
            dataTable.Columns.Add("acc_cumpli_ver", typeof(string));
            dataTable.Columns.Add("acc_comen_ver", typeof(string));
            dataTable.Columns.Add("acc_fecha_efe", typeof(string));
            dataTable.Columns.Add("acc_cumpli_efe", typeof(string));
            dataTable.Columns.Add("acc_comen_efe", typeof(string));
            dataTable.Columns.Add("acc_clave", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_accion, A.acc_fechatermino, A.acc_fecha_ver, A.acc_cumpli_ver,  A.acc_comen_ver, A.acc_fecha_efe, A.acc_cumpli_efe, A.acc_comen_efe, A.acc_clave FROM tb_det_acciones A, tb_cat_responsables B, tb_mstr_acciones C WHERE A.que_folio = '" + folio + "' AND A.acc_responsable = '" + resp + "' AND A.acc_responsable = B.resp_clave AND A.acc_folio = '" + acc_folio + "' AND A.acc_folio = C.acc_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_responsable"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_nombre"] = (object)(this.read.GetValue(1).ToString().Trim() + " " + this.read.GetValue(2).ToString().Trim());
                    row["acc_accion"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["acc_fechatermino"] = this.read.GetValue(4).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(4).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_fecha_ver"] = this.read.GetValue(5).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(5).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumpli_ver"] = (object)this.read.GetValue(6).ToString().Trim();
                    row["acc_comen_ver"] = (object)this.read.GetValue(7).ToString().Trim();
                    row["acc_fecha_efe"] = this.read.GetValue(8).ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read.GetValue(8).ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumpli_efe"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["acc_comen_efe"] = (object)this.read.GetValue(10).ToString().Trim();
                    row["acc_clave"] = (object)this.read.GetValue(11).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string existe_det_investigadores(string folio)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT COUNT(que_folio) AS que_folio FROM tb_det_investigacion WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim();
                this.Cerrar();
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable existe_investigacion(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_folio, inv_responsable FROM tb_det_investigacion WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["inv_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable existe_investigador_mstr_acciones(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("resp_clave", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_folio, resp_clave, acc_responsable, acc_causa FROM tb_mstr_acciones WHERE que_folio = '" + folio + "' AND resp_clave = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["resp_clave"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["acc_causa"] = (object)this.read.GetValue(3).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable existe_investigador_det_acciones(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_cumpli_ver", typeof(string));
            dataTable.Columns.Add("acc_cumpli_efe", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT acc_folio, acc_responsable, acc_accion, acc_cumpli_ver, acc_cumpli_efe FROM tb_det_acciones WHERE acc_folio = '" + folio + "' AND acc_responsable = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["acc_responsable"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["acc_accion"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["acc_cumpli_ver"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["acc_cumpli_efe"] = (object)this.read.GetValue(4).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string cierre_queja(string folio)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas SET que_status = 'T' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                str = "1";
                this.Cerrar();
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable dinamica_productos_lista(string anio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prod_clave", typeof(string));
            dataTable.Columns.Add("prod_nombre", typeof(string));
            dataTable.Columns.Add("enero", typeof(string));
            dataTable.Columns.Add("febrero", typeof(string));
            dataTable.Columns.Add("marzo", typeof(string));
            dataTable.Columns.Add("abril", typeof(string));
            dataTable.Columns.Add("mayo", typeof(string));
            dataTable.Columns.Add("junio", typeof(string));
            dataTable.Columns.Add("julio", typeof(string));
            dataTable.Columns.Add("agosto", typeof(string));
            dataTable.Columns.Add("septiembre", typeof(string));
            dataTable.Columns.Add("octubre", typeof(string));
            dataTable.Columns.Add("noviembre", typeof(string));
            dataTable.Columns.Add("diciembre", typeof(string));
            dataTable.Columns.Add("total", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT DISTINCT(A.id_producto), B.prod_nombre FROM tb_det_quejas A, tb_cat_producto B, tb_mstr_quejas C where A.id_producto = B.prod_clave AND C.que_fecha >= '01/01/" + anio + "' AND C.que_fecha <= '31/12/" + anio + "' AND A.que_folio = C.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prod_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["prod_nombre"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["enero"] = (object)"0";
                    row["febrero"] = (object)"0";
                    row["marzo"] = (object)"0";
                    row["abril"] = (object)"0";
                    row["mayo"] = (object)"0";
                    row["junio"] = (object)"0";
                    row["julio"] = (object)"0";
                    row["agosto"] = (object)"0";
                    row["septiembre"] = (object)"0";
                    row["octubre"] = (object)"0";
                    row["noviembre"] = (object)"0";
                    row["diciembre"] = (object)"0";
                    row["total"] = (object)"0";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable dinamica_productos_cajas(string anio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prod_clave", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.id_producto, A.qud_cantreci, C.que_fecha FROM tb_det_quejas A, tb_mstr_quejas C where  C.que_fecha >= '01/01/" + anio + "' AND C.que_fecha <= '31/12/" + anio + "' AND A.que_folio = C.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prod_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["qud_cantreci"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy");
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable areas_queja()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_area", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY id_area";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                DataRow row1 = dataTable.NewRow();
                row1["id_area"] = (object)"";
                row1["area_nombre"] = (object)"SELECCIONAR..";
                dataTable.Rows.Add(row1);
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["id_area"] = (object)this.read["id_area"].ToString().Trim();
                    row2["area_nombre"] = (object)this.read["area_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string modificarmaestro(
          string folio,
          string cliente,
          string nombre,
          string sucursal,
          string tipo,
          string area,
          string pedido,
          string observaciones,
          string costo)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas SET que_cliprim = '" + cliente + "' , que_cliente = '" + nombre + "', que_sucursal = '" + sucursal + "', que_tipo = '" + tipo + "', area_queja = '" + area + "', que_pedido = '" + pedido + "', que_observacion = '" + observaciones + "', que_costo = '" + costo + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string modificadetalle(
          string folio,
          string producto,
          string problema,
          string ordprod,
          string area,
          string responsable,
          string cantrecha,
          string cantreci,
          string unidad,
          string devolucion,
          string moneda,
          string proveedor,
          string rancho,
          string tabla,
          string variedad,
          string lote,
          string nomprod,
          string caducidad,
          string merma, string porce, string bonificacion)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_quejas SET id_producto = '" + producto + "' , qud_problema = '" + problema + "', qud_ordprod = '" + ordprod + "', qud_area = '" + area + "', " +
                    "qud_responsable = '" + responsable + "', qud_cantrecha = '" + cantrecha + "', qud_cantreci = '" + cantreci + "', qud_unidad = '" + unidad + "', qud_devolucion = '" + devolucion + "', " +
                    "qud_moneda = '" + moneda + "', prov_clave = '" + proveedor + "', rch_clave = '" + rancho + "', tbl_clave = '" + tabla + "', qud_variedad = '" + variedad + "', qud_lote = '" + lote + "', " +
                    "nom_producto = '" + nomprod + "', fecha_cad = '" + caducidad + "', qud_merma = '" + merma + "', qud_porcen = '" + porce + "', qud_bonificacion = '" + bonificacion + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "2";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string modificainvestigador(
          string folio,
          string investigador,
          string fecha,
          string comentario)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_investigacion SET inv_comentario = '" + comentario + "', inv_fechaentrega = '" + fecha + "' WHERE inv_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string borrainvestigador(string folio)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "DELETE FROM tb_det_investigacion WHERE inv_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable causas_responsables_acciones(string folio, string folio_inv)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_causa FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.acc_responsable = B.resp_clave AND A.que_folio = '" + folio + "' AND inv_folio = '" + folio_inv + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    row["acc_responsable"] = (object)this.read["acc_responsable"].ToString().Trim();
                    row["resp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["acc_causa"] = (object)this.read["acc_causa"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string borrar_responsable_accion(string folio)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "DELETE FROM tb_mstr_acciones WHERE acc_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable causas_responsables_acciones_ver(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_causa FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.acc_responsable = B.resp_clave AND A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    row["acc_responsable"] = (object)this.read["acc_responsable"].ToString().Trim();
                    row["resp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["acc_causa"] = (object)this.read["acc_causa"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable causas_responsables_acciones_acc(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_clave", typeof(string));
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_clave, A.acc_folio, A.acc_responsable, A.acc_accion, A.acc_fechatermino FROM tb_det_acciones A WHERE A.acc_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_clave"] = (object)this.read["acc_clave"].ToString().Trim();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    row["acc_responsable"] = (object)this.read["acc_responsable"].ToString().Trim();
                    row["acc_accion"] = (object)this.read["acc_accion"].ToString().Trim();
                    row["acc_fechatermino"] = (object)Convert.ToDateTime(this.read["acc_fechatermino"].ToString().Trim()).ToString("dd/MM/yyyy");
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string modificaaccioncorrectiva(string folio, string accion, string fecha)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_acciones SET acc_accion = '" + accion + "', acc_fechatermino = '" + fecha + "' WHERE acc_clave = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string borraraccioncorrectiva(string folio)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "DELETE FROM tb_det_acciones WHERE acc_clave = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public string correo_responsable_accion(string resp)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_nombre, resp_paterno, resp_correo FROM tb_cat_responsables WHERE resp_clave = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str = this.read.GetValue(0).ToString().Trim() + " " + this.read.GetValue(1).ToString().Trim() + "-" + this.read.GetValue(2).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string correo_investigador_accion(string folio, string resp)
        {
            string str1 = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT resp_clave FROM tb_mstr_acciones WHERE acc_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            this.read.Read();
            string str2 = this.read["resp_clave"].ToString();
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.resp_nombre, A.resp_paterno, A.resp_correo FROM tb_cat_responsables A WHERE A.resp_clave = '" + str2 + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                this.read.Read();
                str1 = this.read.GetValue(0).ToString().Trim() + " " + this.read.GetValue(1).ToString().Trim() + "-" + this.read.GetValue(2).ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str1;
        }

        public DataTable verificacion_investigadores_acciones(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.inv_folio, A.inv_responsable, B.resp_nombre, B.resp_paterno, A.inv_comentario FROM tb_det_investigacion A, tb_cat_responsables B WHERE A.inv_responsable = B.resp_clave AND A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_folio"] = (object)this.read["inv_folio"].ToString().Trim();
                    row["inv_responsable"] = (object)this.read["inv_responsable"].ToString().Trim();
                    row["resp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["inv_comentario"] = (object)this.read["inv_comentario"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable verificacion_responsables_acciones(string folio, string queja)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acc_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, A.acc_responsable, B.resp_nombre, B.resp_paterno, A.acc_causa FROM tb_mstr_acciones A, tb_cat_responsables B WHERE A.acc_responsable = B.resp_clave AND A.inv_folio = '" + folio + "' AND A.que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    row["acc_responsable"] = (object)this.read["acc_responsable"].ToString().Trim();
                    row["resp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["acc_causa"] = (object)this.read["acc_causa"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable verificacion_acciones_correctivas(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_clave", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            dataTable.Columns.Add("acc_fecha_ver", typeof(string));
            dataTable.Columns.Add("acc_cumpli_ver", typeof(string));
            dataTable.Columns.Add("acc_comen_ver", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_clave, A.acc_accion, A.acc_fechatermino, A.acc_fecha_ver, A.acc_cumpli_ver, A.acc_comen_ver FROM tb_det_acciones A WHERE A.acc_folio = '" + folio + "' AND A.acc_responsable = '" + resp + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_clave"] = (object)this.read["acc_clave"].ToString().Trim();
                    row["acc_accion"] = (object)this.read["acc_accion"].ToString().Trim();
                    row["acc_fechatermino"] = (object)Convert.ToDateTime(this.read["acc_fechatermino"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["acc_fecha_ver"] = this.read["acc_fecha_ver"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fecha_ver"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumpli_ver"] = (object)this.read["acc_cumpli_ver"].ToString().Trim();
                    row["acc_comen_ver"] = (object)this.read["acc_comen_ver"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public void etapa_verificar(string queja)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_etapa4 = '1' WHERE que_folio = '" + queja + "'";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public DataTable efectividad_acciones_correctivas(string folio, string resp)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_clave", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            dataTable.Columns.Add("acc_fecha_ver", typeof(string));
            dataTable.Columns.Add("acc_cumpli_ver", typeof(string));
            dataTable.Columns.Add("acc_comen_ver", typeof(string));
            dataTable.Columns.Add("acc_fecha_efe", typeof(string));
            dataTable.Columns.Add("acc_cumpli_efe", typeof(string));
            dataTable.Columns.Add("acc_comen_efe", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_clave, A.acc_accion, A.acc_fechatermino, A.acc_fecha_ver, A.acc_cumpli_ver, A.acc_comen_ver, A.acc_fecha_efe, A.acc_cumpli_efe, A.acc_comen_efe FROM tb_det_acciones A WHERE A.acc_folio = '" + folio + "' AND A.acc_responsable = '" + resp + "' and A.acc_cumpli_ver <> ''";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_clave"] = (object)this.read["acc_clave"].ToString().Trim();
                    row["acc_accion"] = (object)this.read["acc_accion"].ToString().Trim();
                    row["acc_fechatermino"] = (object)Convert.ToDateTime(this.read["acc_fechatermino"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["acc_fecha_ver"] = this.read["acc_fecha_ver"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fecha_ver"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumpli_ver"] = (object)this.read["acc_cumpli_ver"].ToString().Trim();
                    row["acc_comen_ver"] = (object)this.read["acc_comen_ver"].ToString().Trim();
                    row["acc_fecha_efe"] = this.read["acc_fecha_efe"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fecha_efe"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumpli_efe"] = (object)this.read["acc_cumpli_efe"].ToString().Trim();
                    row["acc_comen_efe"] = (object)this.read["acc_comen_efe"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertaefectividad(
          string folio,
          string fecha,
          string cumplimiento,
          string comentario)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas SET que_fecha_efe = '" + fecha + "', que_cumpli_efe = '" + cumplimiento + "', que_comen_efe = '" + comentario + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public void etapa_efectividad(string queja)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_etapa5 = '1' WHERE que_folio = '" + queja + "'";
            this.com.ExecuteNonQuery();
            this.com.Dispose();
            this.Cerrar();
        }

        public string cierre_queja_verificacion(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("acc_folio", typeof(string));
            string str = "0";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT acc_folio FROM tb_mstr_acciones WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT acc_clave, acc_fecha_ver FROM tb_det_acciones WHERE que_folio = '" + folio + "' AND acc_folio = '" + row["acc_folio"].ToString() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        if (this.read["acc_fecha_ver"].ToString().Trim() != "")
                        {
                            str = "1";
                        }
                        else
                        {
                            str = "0";
                            break;
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }
                else
                {
                    str = "0";
                    break;
                }
            }
            this.Cerrar();
            return str;
        }

        public void cierre_queja2(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "UPDATE tb_mstr_quejas SET que_status = 'T' WHERE que_folio = '" + folio + "'";
            this.com.ExecuteNonQuery();
            this.Cerrar();
        }

        public string queja_finalizada(string folio)
        {
            string str = "0";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_status FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                    str = !(this.read["que_status"].ToString().Trim() == "T") ? "0" : "1";
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string validacion_de_causas_ingresadas(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("inv_fechaentrega", typeof(string));
            dataTable.Columns.Add("inv_causa", typeof(string));
            dataTable.Columns.Add("inv_causa_fecha", typeof(string));
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT inv_folio, inv_responsable, inv_fechaentrega, inv_causa, inv_causa_fecha FROM tb_det_investigacion WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_folio"] = (object)this.read["inv_folio"].ToString().Trim();
                    row["inv_responsable"] = (object)this.read["inv_responsable"].ToString().Trim();
                    row["inv_fechaentrega"] = (object)this.read["inv_fechaentrega"].ToString().Trim();
                    row["inv_causa"] = (object)this.read["inv_causa"].ToString().Trim();
                    row["inv_causa_fecha"] = (object)this.read["inv_causa_fecha"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            bool flag = false;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT acc_folio FROM tb_mstr_acciones WHERE inv_folio = '" + row["inv_folio"].ToString() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    flag = true;
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }
                else
                {
                    flag = false;
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                    break;
                }
            }
            if (flag)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT inv_fechaentrega, inv_causa_fecha FROM tb_det_investigacion WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        if (Convert.ToDateTime(this.read["inv_causa_fecha"].ToString().Trim()) > Convert.ToDateTime(this.read["inv_fechaentrega"].ToString().Trim()))
                        {
                            str = "2";
                            break;
                        }
                        str = "1";
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return str;
        }

        public string validacion_de_acciones_registradas(string folio)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("acc_folio", typeof(string));
            dataTable1.Columns.Add("que_folio", typeof(string));
            dataTable1.Columns.Add("acc_fecha", typeof(string));
            DataTable dataTable2 = new DataTable()
            {
                Columns = {
              {
                "acc_clave",
                typeof (string)
              },
              {
                "que_folio",
                typeof (string)
              },
              {
                "acc_fecha_reg",
                typeof (string)
              }
            }
            };
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT acc_folio, que_folio, acc_fecha FROM tb_mstr_acciones WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable1.NewRow();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["acc_fecha"] = (object)this.read["acc_fecha"].ToString().Trim();
                    dataTable1.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            string str1 = "";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
            {
                string str2 = row["acc_folio"].ToString();
                string str3 = row["acc_fecha"].ToString();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT acc_clave, que_folio, acc_fecha_reg FROM tb_det_acciones WHERE acc_folio = '" + str2 + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        string str4 = this.read["acc_fecha_reg"].ToString().Trim();
                        if (Convert.ToDateTime(str3) < Convert.ToDateTime(str4))
                        {
                            str1 = "2";
                            break;
                        }
                    }
                    if (str1 == "2")
                    {
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();
                        break;
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            if (str1 != "2")
                str1 = "1";
            this.Cerrar();
            return str1;
        }

        public DataTable dinamica_problema()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("pro_clave", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT pro_clave, pro_nombre FROM tb_cat_problemas ORDER BY pro_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["pro_clave"] = (object)this.read["pro_clave"].ToString().Trim();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable dinamica_quejas(string rango1, string rango2)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prod_clave", typeof(string));
            dataTable.Columns.Add("prod_nombre", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.id_producto, B.prod_nombre, A.qud_problema, A.qud_ordprod, A.qud_cantrecha, C.que_fecha FROM tb_det_quejas A, tb_cat_producto B, tb_mstr_quejas C WHERE A.id_producto = B.prod_clave AND A.que_folio = C.que_folio AND C.que_fecha >= '" + rango1 + "' AND C.que_fecha <= '" + rango2 + "' ORDER BY A.qud_problema";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prod_clave"] = (object)this.read["id_producto"].ToString().Trim();
                    row["prod_nombre"] = (object)this.read["prod_nombre"].ToString().Trim();
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    row["que_fecha"] = (object)this.read["que_fecha"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable dinamica_quejas_sumatoria_causa(string rango1, string rango2)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select B.que_fecha, A.qud_problema, sum(A.qud_cantrecha) AS qud_cantrecha from tb_det_quejas A, tb_mstr_quejas B WHERE A.que_folio = B.que_folio AND B.que_fecha >= '" + rango1 + "' and B.que_fecha <= '" + rango2 + "' GROUP BY A.qud_problema, B.que_fecha ORDER BY B.que_fecha ASC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable tabla_dinamica_causas(string rango1, string rango2)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("pro_clave", typeof(string));
            dataTable1.Columns.Add("pro_nombre", typeof(string));
            int num = 0;
            string str1 = rango1;
            do
            {
                if (num == 0)
                {
                    dataTable1.Columns.Add(Convert.ToDateTime(str1).ToString("dd/MM/yyyy"), typeof(string));
                    ++num;
                }
                else
                {
                    DataColumnCollection columns = dataTable1.Columns;
                    DateTime dateTime = Convert.ToDateTime(str1);
                    dateTime = dateTime.AddDays(1.0);
                    string shortDateString = dateTime.ToString("dd/MM/yyyy");
                    Type type = typeof(string);
                    columns.Add(shortDateString, type);
                    str1 = Convert.ToDateTime(str1).AddDays(1.0).ToString("dd/MM/yyyy");
                    ++num;
                }
            }
            while (str1 != rango2);
            dataTable1.Columns.Add("total", typeof(string));
            this.Abrir();
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("que_folio", typeof(string));
            dataTable2.Columns.Add("que_fecha", typeof(string));
            dataTable2.Columns.Add("id_producto", typeof(string));
            dataTable2.Columns.Add("qud_problema", typeof(string));
            dataTable2.Columns.Add("pro_nombre", typeof(string));
            dataTable2.Columns.Add("qud_ordprod", typeof(string));
            dataTable2.Columns.Add("qud_cantrecha", typeof(string));
            dataTable2.Columns.Add("nom_producto", typeof(string));
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.que_fecha, B.id_producto, B.qud_problema, C.pro_nombre, B.qud_ordprod, B.qud_cantrecha, B.nom_producto FROM tb_mstr_quejas A, tb_det_quejas B, tb_cat_problemas C WHERE A.que_folio = B.que_folio AND A.que_fecha >= '" + rango1 + "' AND A.que_fecha <= '" + rango2 + "' AND B.qud_problema = C.pro_clave ORDER BY B.id_producto ";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable2.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    dataTable2.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            DataTable dataTable3 = new DataTable();
            dataTable3.Columns.Add("pro_clave", typeof(string));
            dataTable3.Columns.Add("pro_nombre", typeof(string));
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT pro_clave, pro_nombre FROM tb_cat_problemas  ORDER BY pro_clave";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable3.NewRow();
                    row["pro_clave"] = (object)this.read["pro_clave"].ToString().Trim();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                    dataTable3.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            DataTable dataTable4 = new DataTable();
            dataTable4.Columns.Add("prod_clave", typeof(string));
            dataTable4.Columns.Add("prod_nombre", typeof(string));
            dataTable4.Columns.Add("prod_fecha", typeof(string));
            dataTable4.Columns.Add("prod_total", typeof(string));
            dataTable4.Columns.Add("prod_problema", typeof(string));
            string str2 = "";
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable2.Rows)
            {
                string str3 = row1["nom_producto"].ToString();
                string str4 = row1["que_fecha"].ToString();
                string str5 = row1["qud_cantrecha"].ToString();
                string str6 = row1["qud_problema"].ToString();
                if (str3 != str2)
                {
                    DataRow row2 = dataTable4.NewRow();
                    row2["prod_clave"] = (object)row1["id_producto"].ToString();
                    row2["prod_nombre"] = (object)row1["nom_producto"].ToString();
                    row2["prod_fecha"] = (object)str4;
                    row2["prod_total"] = (object)str5;
                    row2["prod_problema"] = (object)str6;
                    dataTable4.Rows.Add(row2);
                }
                else
                {
                    bool flag = false;
                    DataTable dataTable5 = dataTable4;
                    string filterExpression = "prod_nombre = '" + str3 + "' AND prod_fecha = '" + str4 + "' AND prod_problema = '" + str6 + "'";
                    foreach (DataRow dataRow in dataTable5.Select(filterExpression))
                    {
                        flag = true;
                        dataRow["prod_total"] = (object)(Convert.ToDecimal(dataRow["prod_total"].ToString()) + Convert.ToDecimal(str5));
                    }
                    if (!flag)
                    {
                        DataRow row2 = dataTable4.NewRow();
                        row2["prod_clave"] = (object)row1["id_producto"].ToString();
                        row2["prod_nombre"] = (object)row1["nom_producto"].ToString();
                        row2["prod_fecha"] = (object)row1["que_fecha"].ToString();
                        row2["prod_total"] = (object)row1["qud_cantrecha"].ToString();
                        row2["prod_problema"] = (object)row1["qud_problema"].ToString();
                        dataTable4.Rows.Add(row2);
                    }
                }
                str2 = str3;
            }
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable3.Rows)
            {
                string str3 = row1["pro_clave"].ToString();
                DataRow row2 = dataTable1.NewRow();
                row2["pro_clave"] = (object)row1["pro_clave"].ToString();
                row2["pro_nombre"] = (object)row1["pro_nombre"].ToString();
                dataTable1.Rows.Add(row2);
                foreach (DataRow dataRow1 in dataTable1.Select("pro_clave = '" + str3 + "'"))
                {
                    foreach (DataRow dataRow2 in dataTable4.Select("prod_problema = '" + str3 + "'"))
                    {
                        foreach (DataColumn column in (InternalDataCollectionBase)dataTable1.Columns)
                        {
                            string columnName = column.ColumnName;
                            if (!(columnName == "pro_clave") && !(columnName == "pro_nombre") && !(columnName == "total") && column.ColumnName == dataRow2["prod_fecha"].ToString())
                                dataRow1[columnName] = (object)dataRow2["prod_total"].ToString();
                        }
                    }
                }
                foreach (DataRow dataRow1 in dataTable4.Select("prod_problema = '" + str3 + "'"))
                {
                    DataRow row3 = dataTable1.NewRow();
                    row3["pro_clave"] = (object)dataRow1["prod_clave"].ToString();
                    row3["pro_nombre"] = (object)dataRow1["prod_nombre"].ToString();
                    foreach (DataColumn column in (InternalDataCollectionBase)dataTable1.Columns)
                    {
                        string columnName = column.ColumnName;
                        if (!(columnName == "pro_clave") && !(columnName == "pro_nombre") && !(columnName == "total") && column.ColumnName == dataRow1["prod_fecha"].ToString())
                            row3[columnName] = (object)dataRow1["prod_total"].ToString();
                    }
                    dataTable1.Rows.Add(row3);
                    DataTable dataTable5 = dataTable2;
                    string filterExpression = "qud_problema = '" + str3 + "' AND nom_producto = '" + dataRow1["prod_nombre"].ToString() + "' AND que_fecha = '" + dataRow1["prod_fecha"].ToString() + "'";
                    foreach (DataRow dataRow2 in dataTable5.Select(filterExpression))
                    {
                        DataRow row4 = dataTable1.NewRow();
                        row4["pro_clave"] = (object)"";
                        row4["pro_nombre"] = (object)dataRow2["qud_ordprod"].ToString();
                        foreach (DataColumn column in (InternalDataCollectionBase)dataTable1.Columns)
                        {
                            string columnName = column.ColumnName;
                            if (!(columnName == "pro_clave") && !(columnName == "pro_nombre") && !(columnName == "total") && column.ColumnName == dataRow2["que_fecha"].ToString())
                                row4[columnName] = (object)dataRow2["qud_cantrecha"].ToString();
                        }
                        dataTable1.Rows.Add(row4);
                    }
                }
            }
            return dataTable1;
        }

        public DataTable correos_todos(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("nombre", typeof(string));
            dataTable.Columns.Add("correo", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.resp_nombre, A.resp_paterno, A.resp_correo FROM tb_cat_responsables A, tb_mstr_quejas B WHERE A.resp_clave = B.resp_usuario AND B.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["correo"] = (object)this.read["resp_correo"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.resp_nombre, A.resp_paterno, A.resp_correo FROM tb_cat_responsables A, tb_det_investigacion B WHERE A.resp_clave = B.inv_responsable AND B.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["correo"] = (object)this.read["resp_correo"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.resp_nombre, A.resp_paterno, A.resp_correo FROM tb_cat_responsables A, tb_mstr_acciones B WHERE A.resp_clave = B.acc_responsable AND B.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["correo"] = (object)this.read["resp_correo"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string validar_asignar_area(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT area_queja FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            this.read.Read();
            string str = this.read["area_queja"].ToString().Trim();
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string validacion_tarima(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT qud_tarima, id_producto, qud_ordprod FROM tb_det_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            this.read.Read();
            string str1 = this.read["qud_tarima"].ToString().Trim();
            string str2 = this.read["id_producto"].ToString().Trim();
            string str3 = this.read["qud_ordprod"].ToString().Trim();
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            SqlCommand com = this.com;
            string[] strArray1 = new string[7]
            {
            "SELECT tipo FROM tb_det_trazabilidad WHERE recibo = '",
            str3,
            "' AND prod_clave = '",
            str2,
            "' AND pti_fecha >= '",
            null,
            null
            };
            string[] strArray2 = strArray1;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(-40.0);
            string uno = dateTime.Day.ToString();
            string dos = dateTime.Month.ToString().PadLeft(2, '0');
            string tres = dateTime.Year.ToString();
            string shortDateString = uno + "/" + dos + "/" + tres;
            strArray2[5] = shortDateString;
            strArray1[6] = "'";
            string str4 = string.Concat(strArray1);
            com.CommandText = str4;
            this.read = this.com.ExecuteReader();
            string str5 = this.read.HasRows ? str1 : "1";
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str5;
        }

        public DataTable buscarregistros_tarima(string folio)
        {
            string str1 = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT qud_tarima, id_producto, qud_ordprod FROM tb_det_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            this.read.Read();
            str1 = this.read["qud_tarima"].ToString().Trim();
            string str2 = this.read["id_producto"].ToString().Trim();
            string str3 = this.read["qud_ordprod"].ToString().Trim();
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
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
            dataTable.Columns.Add("tarima", typeof(string));
            dataTable.Columns.Add("lote", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            SqlCommand com = this.com;
            string[] strArray1 = new string[7]
            {
            "SELECT A.prod_clave, A.prod_nombre, A.pti_fecha, A.prov_clave, A.prov_nombre, A.rch_clave, A.rch_nombre, A.tbl_clave, A.tbl_nombre, A.fecha_cad, A.tipo, A.tarima, A.lote FROM tb_det_trazabilidad A WHERE A.pti_estatus <> 'C' AND A.recibo = '",
            str3,
            "' AND A.prod_clave = '",
            str2,
            "' AND A.pti_fecha >= '",
            null,
            null
            };
            string[] strArray2 = strArray1;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(-100.0);
            string uno = dateTime.Day.ToString();
            string dos = dateTime.Month.ToString().PadLeft(2, '0');
            string tres = dateTime.Year.ToString();
            string shortDateString = uno + "/" + dos + "/" + tres;
            //string shortDateString = dateTime.ToString("dd/MM/yyyy");
            strArray2[5] = shortDateString;
            strArray1[6] = "'ORDER BY A.pti_fecha ASC, A.tarima, A.prod_clave";
            string str4 = string.Concat(strArray1);
            com.CommandText = str4;
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prod_clave"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["prod_nombre"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["pti_fecha"] = (object)Convert.ToDateTime(this.read.GetValue(2).ToString().Trim()).ToString("dd/MM/yyyy");
                    row["prov_clave"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["prov_nombre"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["rch_clave"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["rch_nombre"] = (object)this.read.GetValue(6).ToString().Trim();
                    row["tbl_clave"] = (object)this.read.GetValue(7).ToString().Trim();
                    row["tbl_nombre"] = (object)this.read.GetValue(8).ToString().Trim();
                    row["fecha_cad"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["tipo"] = this.read.GetValue(10).ToString().Trim() == "PTC" ? (object)"CAMPO" : (this.read.GetValue(10).ToString().Trim() == "PTP" ? (object)"PLANTA" : (object)"");
                    row["tarima"] = (object)this.read.GetValue(11).ToString().Trim();
                    row["lote"] = (object)this.read.GetValue(12).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string actualizar_tarima(
          string folio,
          string proveedor,
          string rancho,
          string tabla,
          string lote,
          string caducidad,
          string tarima)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_quejas SET prov_clave = '" + proveedor + "', rch_clave = '" + rancho + "', tbl_clave = '" + tabla + "', qud_lote = '" + lote + "', fecha_cad = '" + caducidad + "', qud_tarima = '" + tarima + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string actualizar_tarima_principal(
          string folio,
          string proveedor,
          string rancho,
          string tabla,
          string lote,
          string caducidad,
          string tarima,
          string nomRecibe)
        {
            string cuerpo = "";
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                cuerpo = "UPDATE tb_det_quejas SET prov_clave = '" + proveedor + "', rch_clave = '" + rancho + "', tbl_clave = '" + tabla + "', qud_lote = '" + lote + "', fecha_cad = '" + caducidad + "', qud_tarima = '" + tarima + "' WHERE que_folio = '" + folio + "'";
                this.com.CommandText = cuerpo;
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (Exception ex)
            {
                this.enviarcorreo_error(nomRecibe, folio, cuerpo);
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string deshabilitar_edicion_causas_acciones(string folio, string clave)
        {
            string str = "1";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select A.inv_responsable, B.acc_cumpli_ver FROM tb_det_investigacion A LEFT JOIN tb_det_acciones B ON A.que_folio = B.que_folio and A.inv_responsable = B.acc_responsable WHERE A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            while (this.read.Read())
            {
                if (this.read["acc_cumpli_ver"].ToString().Trim() == "")
                {
                    str = "1";
                    break;
                }
                str = "0";
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string deshabilitar_edicion_mstr_acciones_con_detalle_acciones(string folio)
        {
            string str = "1";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.acc_folio, B.acc_accion FROM tb_mstr_acciones A LEFT JOIN tb_det_acciones B ON A.acc_folio = B.acc_folio AND A.que_folio = B.que_folio WHERE  A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            while (this.read.Read())
            {
                if (this.read["acc_accion"].ToString().Trim() == "")
                {
                    str = "1";
                    break;
                }
                str = "0";
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string deshabilitar_botones_alta_investigador(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_folio FROM tb_det_investigacion WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            string str = !this.read.HasRows ? "0" : "1";
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable general_mstr_quejas(
          string fecha_inicio,
          string fecha_fin,
          string clave,
          string cedis,
          string admin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_mes", typeof(string));
            dataTable.Columns.Add("que_cliente", typeof(string));
            dataTable.Columns.Add("que_sucursal", typeof(string));
            dataTable.Columns.Add("que_reporto", typeof(string));
            dataTable.Columns.Add("que_recibio", typeof(string));
            dataTable.Columns.Add("cedis", typeof(string));
            dataTable.Columns.Add("area_queja", typeof(string));
            dataTable.Columns.Add("que_fecha_efe", typeof(string));
            dataTable.Columns.Add("que_cumpli_efe", typeof(string));
            dataTable.Columns.Add("que_comen_efe", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("resp_paterno", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            dataTable.Columns.Add("pedido", typeof(string));
            dataTable.Columns.Add("transporte", typeof(string));
            dataTable.Columns.Add("costo", typeof(string));
            dataTable.Columns.Add("tipo", typeof(string));
            dataTable.Columns.Add("que_fechaemb", typeof(string));
            dataTable.Columns.Add("que_sememb", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (admin == "ADMINISTRADOR")
            {
                if (fecha_inicio == "")
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.que_fecha = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' AND que_status in ('A', 'T') ORDER BY A.que_folio";
                if (fecha_fin == "" && fecha_inicio != "")
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.que_fecha >= '" + fecha_inicio + "' AND que_status in ('A', 'T') ORDER BY A.que_folio";
                if (fecha_fin != "" && fecha_inicio != "")
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND (A.que_fecha >= '" + fecha_inicio + "' AND A.que_fecha <= '" + fecha_fin + "') AND que_status in ('A', 'T') ORDER BY A.que_folio";
            }
            else
            {
                if (fecha_inicio == "")
                {
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.cedis = '" + cedis + "' AND A.que_fecha = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' AND que_status in ('A', 'T') ORDER BY A.que_folio";
                    if (cedis == "AGUILARES")
                        this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.cedis = 'COMERCIALIZADORA GAB' AND C.area_nombre = 'EMPAQUE_AGUILARES' AND A.que_fecha = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' AND que_status in ('A', 'T') ORDER BY A.que_folio";
                }
                if (fecha_fin == "" && fecha_inicio != "")
                {
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.que_fecha >= '" + fecha_inicio + "' AND A.cedis = '" + cedis + "' AND que_status in ('A', 'T') ORDER BY A.que_folio";
                    if (cedis == "AGUILARES")
                        this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.cedis = 'COMERCIALIZADORA GAB' AND C.area_nombre = 'EMPAQUE_AGUILARES' AND A.que_fecha >= '" + fecha_inicio + "' AND que_status in ('A', 'T') ORDER BY A.que_folio";
                }
                if (fecha_fin != "" && fecha_inicio != "")
                {
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND (A.que_fecha >= '" + fecha_inicio + "' AND A.que_fecha <= '" + fecha_fin + "') AND A.cedis = '" + cedis + "' AND que_status in ('A', 'T') ORDER BY A.que_folio";
                    if (cedis == "AGUILARES")
                        this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte, A.que_costo, A.que_tipo, A.que_fechaemb, A.que_sememb FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.cedis = 'COMERCIALIZADORA GAB' AND C.area_nombre = 'EMPAQUE_AGUILARES' AND (A.que_fecha >= '" + fecha_inicio + "' AND A.que_fecha <= '" + fecha_fin + "') AND que_status in ('A', 'T') ORDER BY A.que_folio";
                }
            }
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_semana"] = (object)this.read["que_semana"].ToString().Trim();
                    row["que_fecha"] = (object)this.read["que_fecha"].ToString().Trim();
                    row["que_mes"] = (object)this.read["que_mes"].ToString().Trim();
                    row["que_cliente"] = (object)this.read["que_cliente"].ToString().Trim();
                    row["que_sucursal"] = (object)this.read["que_sucursal"].ToString().Trim();
                    row["que_reporto"] = (object)this.read["que_reporto"].ToString().Trim();
                    row["que_recibio"] = (object)this.read["que_recibio"].ToString().Trim();
                    row["cedis"] = (object)this.read["cedis"].ToString().Trim();
                    row["area_queja"] = (object)this.read["area_queja"].ToString().Trim();
                    row["que_fecha_efe"] = (object)this.read["que_fecha_efe"].ToString().Trim();
                    row["que_cumpli_efe"] = (object)this.read["que_cumpli_efe"].ToString().Trim();
                    row["que_comen_efe"] = (object)this.read["que_comen_efe"].ToString().Trim();
                    row["resp_nombre"] = (object)this.read["resp_nombre"].ToString().Trim();
                    row["resp_paterno"] = (object)this.read["resp_paterno"].ToString().Trim();
                    row["area_nombre"] = (object)this.read["area_nombre"].ToString().Trim();
                    row["pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["transporte"] = (object)this.read["que_transporte"].ToString().Trim();
                    row["costo"] = (object)this.read["que_costo"].ToString().Trim();
                    row["tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["que_fechaemb"] = (object)this.read["que_fechaemb"].ToString().Trim();
                    row["que_sememb"] = (object)this.read["que_sememb"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }


            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            foreach (DataRow rt in dataTable.Select("costo = '0.00'"))
            {
                com = conn.CreateCommand();
                com.CommandText = "SELECT ISNULL(SUM(costo), 0) as Costo FROM tb_det_quejas_fact WHERE que_folio = '" + rt["que_folio"] + "'";
                read = com.ExecuteReader();
                if (read.Read())
                {
                    rt["costo"] = Convert.ToDecimal(read["Costo"].ToString()).ToString("###,##,##0.00");
                }
                read.Close();
                read.Dispose();
                com.Dispose();
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable general_det_quejas(string fecha_inicio, string fecha_fin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_area", typeof(string));
            dataTable.Columns.Add("qud_responsable", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("qud_unidad", typeof(string));
            dataTable.Columns.Add("qud_devolucion", typeof(string));
            dataTable.Columns.Add("qud_moneda", typeof(string));
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("rch_clave", typeof(string));
            dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("qud_variedad", typeof(string));
            dataTable.Columns.Add("qud_lote", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("fecha_cad", typeof(string));
            dataTable.Columns.Add("qud_tarima", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("rch_nombre", typeof(string));
            dataTable.Columns.Add("tbl_nombre", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            dataTable.Columns.Add("producidas", typeof(string));
            dataTable.Columns.Add("porcentaje", typeof(string));
            dataTable.Columns.Add("ordp_fecha", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            //this.com.CommandText = "SELECT A.que_folio, A.id_producto, A.qud_problema, A.qud_ordprod, A.qud_area, A.qud_responsable, A.qud_cantrecha, A.qud_cantreci, " +
            //    "A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.prov_clave, A.rch_clave, A.tbl_clave, A.qud_variedad, A.qud_lote, A.nom_producto, A.fecha_cad, A.qud_tarima, " +
            //    "A.qud_cjsprod, A.qud_porcen, C.ordp_fecha FROM tb_det_quejas A JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio JOIN tb_mstr_ordenes_prod C ON A.qud_ordprod = C.ordp_folio " +
            //    "WHERE B.que_fechaemb >= '" + fecha_inicio + "' AND B.que_fechaemb <= '" + fecha_fin + "' ORDER BY que_folio";
            this.com.CommandText = "SELECT A.que_folio, A.id_producto, A.qud_problema, A.qud_ordprod, A.qud_area, A.qud_responsable, A.qud_cantrecha, A.qud_cantreci, " +
                "A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.prov_clave, A.rch_clave, A.tbl_clave, A.qud_variedad, A.qud_lote, A.nom_producto, " +
                "A.fecha_cad, A.qud_tarima, A.qud_cjsprod, A.qud_porcen, " +
                "(case " +
                    "when A.qud_ptcptp = 'PTP' " +
                    "then (SELECT C.ordp_fecha from tb_mstr_ordenes_prod C where A.qud_ordprod = C.ordp_folio) " +
                    "else (SELECT C.rpt_fecha from tb_mstr_recepcion_pt C where A.qud_ordprod = C.rpt_recibo) " +
                    "end) AS ordp_fecha " +
                "FROM tb_det_quejas A JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio " +
                "WHERE B.que_fecha >= '" + fecha_inicio + "' AND B.que_fecha <= '" + fecha_fin + "' ORDER BY que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    row["qud_area"] = (object)this.read["qud_area"].ToString().Trim();
                    row["qud_responsable"] = (object)this.read["qud_responsable"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    row["qud_cantreci"] = (object)this.read["qud_cantreci"].ToString().Trim();
                    row["qud_unidad"] = (object)this.read["qud_unidad"].ToString().Trim();
                    row["qud_devolucion"] = (object)this.read["qud_devolucion"].ToString().Trim();
                    row["qud_moneda"] = (object)this.read["qud_moneda"].ToString().Trim();
                    row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    row["rch_clave"] = (object)this.read["rch_clave"].ToString().Trim();
                    row["tbl_clave"] = (object)this.read["tbl_clave"].ToString().Trim();
                    row["qud_variedad"] = (object)this.read["qud_variedad"].ToString().Trim();
                    row["qud_lote"] = (object)this.read["qud_lote"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["fecha_cad"] = (object)this.read["fecha_cad"].ToString().Trim();
                    row["qud_tarima"] = (object)this.read["qud_tarima"].ToString().Trim();
                    row["producidas"] = (object)this.read["qud_cjsprod"].ToString().Trim();
                    row["porcentaje"] = (object)this.read["qud_porcen"].ToString().Trim();
                    row["ordp_fecha"] = (object)this.read["ordp_fecha"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT pro_nombre FROM tb_cat_problemas WHERE pro_clave = '" + row["qud_problema"] + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor WHERE prov_clave = '" + row["prov_clave"] + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                if (row["rch_clave"].ToString() != "")
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT rch_nombre FROM tb_cat_ranchos WHERE prov_clave = '" + row["prov_clave"] + "' AND rch_clave = '" + row["rch_clave"] + "'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        this.read.Read();
                        row["rch_nombre"] = (object)this.read["rch_nombre"].ToString().Trim();
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                    if (row["tbl_clave"].ToString() != "")
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT tbl_nombre FROM tb_cat_tablas WHERE prov_clave = '" + row["prov_clave"] + "' AND rch_clave = '" + row["rch_clave"] + "' AND tbl_clave = '" + row["tbl_clave"] + "'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            this.read.Read();
                            row["tbl_nombre"] = (object)this.read["tbl_nombre"].ToString().Trim();
                        }
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();
                    }
                }
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable general_det_investigacion()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("inv_fecharegistro", typeof(string));
            dataTable.Columns.Add("inv_fechaentrega", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            dataTable.Columns.Add("inv_causa", typeof(string));
            dataTable.Columns.Add("inv_fechaaccion", typeof(string));
            dataTable.Columns.Add("inv_causa_fecha", typeof(string));
            dataTable.Columns.Add("responsable", typeof(string));
            dataTable.Columns.Add("comen_investigador", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.inv_responsable, A.inv_fecharegistro, A.inv_fechaentrega, A.inv_comentario, A.inv_causa, A.inv_fechaaccion, A.inv_causa_fecha, B.resp_nombre, B.resp_paterno FROM tb_det_investigacion A, tb_cat_responsables B WHERE A.inv_responsable = B.resp_clave ORDER BY A.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["inv_responsable"] = (object)this.read["inv_responsable"].ToString().Trim();
                    row["inv_fecharegistro"] = (object)Convert.ToDateTime(this.read["inv_fecharegistro"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_fechaentrega"] = (object)Convert.ToDateTime(this.read["inv_fechaentrega"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_comentario"] = (object)this.read["inv_comentario"].ToString().Trim();
                    row["inv_causa"] = (object)this.read["inv_causa"].ToString().Trim();
                    row["inv_fechaaccion"] = (this.read["inv_fechaaccion"].ToString().Trim() == "") ? "" : (object)Convert.ToDateTime(this.read["inv_fechaaccion"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_causa_fecha"] = (object)"";
                    row["responsable"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["comen_investigador"] = (object)("(" + this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim() + ") " + this.read["inv_comentario"].ToString().Trim());
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable general_mstr_acciones()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("resp_clave", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_fecha", typeof(string));
            dataTable.Columns.Add("acc_causa", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("resp_paterno", typeof(string));
            dataTable.Columns.Add("resp_causa", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.resp_clave, A.acc_responsable, A.acc_fecha, A.acc_causa, B.resp_nombre, B.resp_paterno FROM tb_mstr_acciones A, tb_cat_responsables B, tb_mstr_quejas C  " +
                "WHERE A.resp_clave = B.resp_clave AND A.que_folio = C.que_folio AND C.que_fecha >= '" + DateTime.Now.AddMonths(-4).ToString("dd/MM/yyyy") + "' AND C.que_status in ('A','T') ORDER BY A.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["resp_clave"] = (object)this.read["resp_clave"].ToString().Trim();
                    row["acc_responsable"] = (object)this.read["acc_responsable"].ToString().Trim();
                    row["acc_fecha"] = this.read["acc_fecha"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fecha"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_causa"] = (object)this.read["acc_causa"].ToString().Trim();
                    row["resp_nombre"] = (object)this.read["resp_nombre"].ToString().Trim();
                    row["resp_paterno"] = (object)this.read["resp_paterno"].ToString().Trim();
                    row["resp_causa"] = (object)("(" + this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim() + ")" + this.read["acc_causa"].ToString().Trim());
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable general_det_acciones()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            dataTable.Columns.Add("acc_fecha_ver", typeof(string));
            dataTable.Columns.Add("acc_cumpli_ver", typeof(string));
            dataTable.Columns.Add("acc_comen_ver", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("acciones", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.acc_folio, A.acc_responsable, A.acc_accion, A.acc_fechatermino, A.acc_fecha_ver, A.acc_cumpli_ver, A.acc_comen_ver, B.resp_nombre, B.resp_paterno FROM tb_det_acciones A, tb_cat_responsables B WHERE A.acc_responsable = B.resp_clave ORDER BY A.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    row["acc_responsable"] = (object)this.read["acc_responsable"].ToString().Trim();
                    row["acc_accion"] = (object)this.read["acc_accion"].ToString().Trim();
                    row["acc_fechatermino"] = this.read["acc_fechatermino"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fechatermino"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_fecha_ver"] = this.read["acc_fecha_ver"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fecha_ver"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumpli_ver"] = (object)this.read["acc_cumpli_ver"].ToString().Trim();
                    row["acc_comen_ver"] = (object)("(" + this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim() + ")" + this.read["acc_comen_ver"].ToString().Trim());
                    row["resp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["acciones"] = (object)("(" + this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim() + ")" + this.read["acc_accion"].ToString().Trim());
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string insertsucursal(string folio_cliente, string nombre_subcliente)
        {
            string str;
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_queja_subclientes (subcli_nombre, cli_folio) VALUES ('" + nombre_subcliente + "', '" + folio_cliente + "')";
                this.com.Dispose();
                this.Cerrar();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            return str;
        }

        public DataTable comboplacas(string fecha)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("no_trailer", typeof(string));
            dataTable.Columns.Add("pdn_folio", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select DISTINCT(A.no_trailer), B.destino, B.chofer, B.transporte from tb_mstr_embarque A INNER JOIN tb_mstr_trailer B ON A.hora_trailer = B.hora_trailer AND A.no_trailer = B.no_trailer WHERE B.fecha = '" + fecha + "'";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["no_trailer"] = (object)"SELECCIONE TRANSPORTE...";
            row1["pdn_folio"] = (object)"0";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                int i = 0;
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["no_trailer"] = (object)(this.read["no_trailer"].ToString().Trim() + " - " + this.read["destino"].ToString().Trim() + " - " + this.read["chofer"].ToString().Trim() + " - " + this.read["transporte"].ToString().Trim());
                    if (this.read["no_trailer"].ToString().Trim() == "PC")
                        row2["pdn_folio"] = (object)this.read["no_trailer"].ToString().Trim() + i++.ToString();
                    else
                        row2["pdn_folio"] = (object)this.read["no_trailer"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable comboplacas_serv(string fecha)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("no_trailer", typeof(string));
            dataTable.Columns.Add("pdn_folio", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select DISTINCT(A.no_trailer), B.destino, B.chofer, B.transporte from tb_mstr_embarque A INNER JOIN tb_mstr_trailer B ON A.hora_trailer = B.hora_trailer AND A.no_trailer = B.no_trailer WHERE B.fecha = '" + fecha + "'";

            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["no_trailer"] = (object)"SELECCIONE TRANSPORTE...";
            row1["pdn_folio"] = (object)"0";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["no_trailer"] = (object)(this.read["no_trailer"].ToString().Trim() + " - " + this.read["destino"].ToString().Trim() + " - " + this.read["chofer"].ToString().Trim() + " - " + this.read["transporte"].ToString().Trim());
                    row2["pdn_folio"] = (object)this.read["no_trailer"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        //20/09/2023
        public DataTable combo_transportistas_serv()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("no_trailer", typeof(string));
            dataTable.Columns.Add("pdn_folio", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT prov_clave, prov_nombre FROM tb_cat_proveedor WHERE prov_clasificacion = 'FL' ORDER BY prov_nombre";

            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["no_trailer"] = (object)"SELECCIONE TRANSPORTE...";
            row1["pdn_folio"] = (object)"0";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["no_trailer"] = (object)this.read["prov_nombre"].ToString().Trim();
                    row2["pdn_folio"] = (object)this.read["prov_clave"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }
        //20/09/2023

        public DataTable detalle_embarque_concentrado(
          string folio,
          string placa,
          string fecha,
          string tipo,
            string chofer)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("pdn_folio", typeof(string));
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("pdn_folio", typeof(string));
            dataTable2.Columns.Add("prod_clave", typeof(string));
            dataTable2.Columns.Add("prod_nombre", typeof(string));
            dataTable2.Columns.Add("cajas", typeof(string));
            dataTable2.Columns.Add("cnte", typeof(string));
            dataTable2.Columns.Add("fact", typeof(string));
            this.Abrir();

            if (placa == "PC")
            {
                string ht = "";
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT hora_trailer FROM tb_mstr_trailer WHERE fecha = '" + fecha + "' AND no_trailer = '" + placa + "' AND chofer = '" + chofer + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    ht = this.read["hora_trailer"].ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT emb_folio FROM tb_mstr_embarque WHERE no_trailer = '" + placa + "' AND hora_trailer = '" + ht + "'";

            }
            //this.com.CommandText = "SELECT emb_folio FROM tb_mstr_embarque WHERE no_trailer = '" + placa + "' AND hora_trailer like '" + fecha + "%' AND chofer = '" + chofer + "'";
            else
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT emb_folio FROM tb_mstr_embarque WHERE no_trailer = '" + placa + "' AND hora_trailer like '" + fecha + "%'";
            }

            //this.com = this.conn.CreateCommand();
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable1.NewRow();
                    row["pdn_folio"] = (object)this.read["emb_folio"].ToString().Trim();
                    dataTable1.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable1.Rows)
            {
                if (tipo == "E" || tipo == "EXPORTACION")
                {
                    this.com = this.conn.CreateCommand();
                    //this.com.CommandText = "SELECT A.prod_clave, B.prod_nombre, SUM(A.cajas) AS cajas, C.cnte_clave FROM tb_det_embarque A " +
                    //    "INNER JOIN tb_cat_producto b ON A.prod_clave = B.prod_clave " +
                    //    "JOIN tb_mstr_pedidos_exp C ON C.pdn_folio = A.emb_folio AND C.pdn_tipo = A.emb_tipo " +
                    //    "WHERE A.emb_folio = '" + row1["pdn_folio"].ToString() + "' AND A.Estatus = 'A' " +
                    //    "GROUP BY A.prod_clave, B.prod_nombre, C.cnte_clave ORDER BY B.prod_nombre";
                    this.com.CommandText = "SELECT A.prod_clave, B.prod_nombre, SUM(A.cajas) AS cajas, C.cnte_clave, D.fcn_folio FROM tb_det_embarque A " +
                        "INNER JOIN tb_cat_producto b ON A.prod_clave = B.prod_clave " +
                        "JOIN tb_mstr_pedidos_exp C ON C.pdn_folio = A.emb_folio AND C.pdn_tipo = A.emb_tipo " +
                        "JOIN tb_mstr_facturas_nal D ON D.pdn_folio = C.pdn_folio AND C.pdn_tipo = D.fcn_tipo " +
                        "WHERE A.emb_folio = '" + row1["pdn_folio"].ToString() + "' AND A.Estatus = 'A' " +
                        "GROUP BY A.prod_clave, B.prod_nombre, C.cnte_clave, D.fcn_folio ORDER BY B.prod_nombre";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            DataRow row2 = dataTable2.NewRow();
                            row2["pdn_folio"] = (object)row1["pdn_folio"].ToString();
                            row2["prod_clave"] = (object)this.read["prod_clave"].ToString().Trim();
                            row2["prod_nombre"] = (object)this.read["prod_nombre"].ToString().Trim();
                            row2["cajas"] = (object)this.read["cajas"].ToString().Trim();
                            row2["cnte"] = (object)this.read["cnte_clave"].ToString().Trim();
                            row2["fact"] = (object)this.read["fcn_folio"].ToString().Trim();
                            dataTable2.Rows.Add(row2);
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }
                else
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT A.prod_clave, B.prod_nombre, SUM(A.cajas) AS cajas, C.cnte_clave, D.fcn_folio FROM tb_det_embarque A " +
                        "INNER JOIN tb_cat_producto b ON A.prod_clave = B.prod_clave " +
                        "JOIN tb_mstr_pedidos_nal C ON C.pdn_folio = A.emb_folio AND C.pdn_tipo = A.emb_tipo " +
                        "JOIN tb_mstr_facturas_nal D ON D.pdn_folio = C.pdn_folio AND C.pdn_serie = D.fcn_tipo " +
                        "WHERE A.emb_folio = '" + row1["pdn_folio"].ToString() + "' AND A.Estatus = 'A' " +
                        "GROUP BY A.prod_clave, B.prod_nombre, C.cnte_clave, D.fcn_folio ORDER BY B.prod_nombre";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            DataRow row2 = dataTable2.NewRow();
                            row2["pdn_folio"] = (object)row1["pdn_folio"].ToString();
                            row2["prod_clave"] = (object)this.read["prod_clave"].ToString().Trim();
                            row2["prod_nombre"] = (object)this.read["prod_nombre"].ToString().Trim();
                            row2["cajas"] = (object)this.read["cajas"].ToString().Trim();
                            row2["cnte"] = (object)this.read["cnte_clave"].ToString().Trim();
                            row2["fact"] = (object)this.read["fcn_folio"].ToString().Trim(); //row2["fact"] = "";
                            dataTable2.Rows.Add(row2);
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }

            }
            this.Cerrar();
            return dataTable2;
        }

        public DataTable detalle_embarque_detalle(string folio, string prod_clave)
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
            this.Abrir();
            this.com = this.conn.CreateCommand();
            folio = folio.PadLeft(6, '0').ToString();
            this.com.CommandText = "SELECT A.prod_clave, A.cajas, A.tarima, A.fec_cad, A.recibo, B.prod_nombre, A.tipo_rec FROM tb_det_embarque A INNER JOIN tb_cat_producto B ON A.prod_clave = B.prod_clave WHERE A.emb_folio = '" + folio + "' AND A.prod_clave = '" + prod_clave + "' ORDER BY B.prod_nombre";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["pedido"] = (object)folio;
                    row["prod_clave"] = (object)this.read["prod_clave"].ToString().Trim();
                    row["cajas"] = (object)this.read["cajas"].ToString().Trim();
                    row["tarima"] = (object)this.read["tarima"].ToString().Trim();
                    row["fec_cad"] = (object)this.read["fec_cad"].ToString().Trim();
                    row["recibo"] = (object)this.read["recibo"].ToString().Trim();
                    row["prod_nombre"] = (object)this.read["prod_nombre"].ToString().Trim();
                    row["qud_unidad"] = (object)"CAJAS";
                    row["tipo_rec"] = (object)this.read["tipo_rec"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT prov_clave, prov_nombre, rch_clave, rch_nombre, tbl_clave, tbl_nombre, lote FROM tb_det_trazabilidad WHERE prod_clave = '" + row["prod_clave"].ToString() + "' AND recibo = '" + row["recibo"].ToString() + "' AND tarima = '" + row["tarima"].ToString() + "' AND tipo = '" + row["tipo_rec"].ToString() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                        row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                        row["rch_clave"] = (object)this.read["rch_clave"].ToString().Trim();
                        row["rch_nombre"] = (object)this.read["rch_nombre"].ToString().Trim();
                        row["tbl_clave"] = (object)this.read["tbl_clave"].ToString().Trim();
                        row["tbl_nombre"] = (object)this.read["tbl_nombre"].ToString().Trim();
                        row["qud_lote"] = (object)this.read["lote"].ToString().Trim();
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                //folio de produccion o campo
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT inicio_campo FROM Tb_folio_campo";
                this.read = this.com.ExecuteReader();
                int fol_campo = 0;
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        fol_campo = Convert.ToInt32(read["inicio_campo"].ToString().Trim());
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                if (Convert.ToInt32(row["recibo"].ToString()) > fol_campo) //folio de campo
                {
                    //validar combo de problema
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT rpt_flete FROM tb_mstr_recepcion_pt WHERE rpt_recibo = '" + row["recibo"].ToString() + "'";
                    this.read = this.com.ExecuteReader();
                    int flete_campo = 0;
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            if (read["rpt_flete"].ToString().Trim() != "" && read["rpt_flete"].ToString().Trim() != "S/F" && read["rpt_flete"].ToString().Trim() != "AJUSTE")
                                flete_campo = Convert.ToInt32(read["rpt_flete"].ToString().Trim());
                            else
                                flete_campo = 0;
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();

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
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT ordp_linea, ordp_responsable FROM tb_mstr_ordenes_prod WHERE ordp_folio = '" + row["recibo"].ToString() + "'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                row["qud_area"] = (object)this.read["ordp_linea"].ToString().Trim();
                                row["qud_responsable"] = (object)this.read["ordp_responsable"].ToString().Trim();
                            }
                        }
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();
                    }

                }


            }
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
            this.Cerrar();
            return dataTable;
        }

        public string insert_queja_exportacion(
          string folio,
          string semana,
          string fecha,
          string mes,
          string cliprim,
          string cliente,
          string sucursal,
          string reporto,
          string recibio,
          string cedis,
          string usuario,
          string tipo,
          string fechainv,
          string area_queja,
          string subcli_folio,
          string costo,
          string pedido,
          string transporte,
          string observaciones,
          string nomRecibe,
            string factura,
            string sufijo,
            string fech_emb,
            string sem_emb)
        {
            string cuerpo = "";
            string str1 = "";
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                cuerpo = "INSERT INTO tb_mstr_quejas(que_semana, que_fecha, que_mes, que_cliprim, que_cliente, que_sucursal, que_reporto, que_recibio, que_status, cedis, resp_usuario, que_tipo, " +
                    "que_fechainv, area_queja, subcli_folio, que_costo, que_pedido, que_transporte, que_observacion, fcn_folio, que_sufijo, que_sememb, que_fechaemb) " +
                    "VALUES('" + semana + "', '" + fecha + "', '" + mes + "', '" + cliprim + "', '" + cliente + "', '" + sucursal + "', '" + reporto + "', '" + recibio + "', 'A', " +
                    "'" + cedis + "', '" + usuario + "', '" + tipo + "', '" + fechainv + "', '" + area_queja + "', '" + subcli_folio + "', '" + costo + "', '" + pedido + "', " +
                    "'" + transporte + "', '" + observaciones + "', '" + factura + "', '" + sufijo + "', '" + sem_emb + "', '" + fech_emb + "') SELECT SCOPE_IDENTITY()";
                this.com.CommandText = cuerpo;
                str1 = "1";
                string str2 = Convert.ToString(this.com.ExecuteScalar()).Trim();
                this.com.Dispose();
                if (Convert.ToInt32(str2) > Convert.ToInt32(folio))
                    folio = str2;
                if (folio == "1")
                {
                    this.com = this.conn.CreateCommand();
                    cuerpo = "INSERT INTO tb_mstr_quejas(que_semana, que_fecha, que_mes, que_cliprim, que_cliente, que_sucursal, que_reporto, que_recibio, que_status, cedis, resp_usuario, que_tipo, " +
                        "que_fechainv, area_queja, subcli_folio, que_costo, que_pedido, que_transporte, que_observacion, fcn_folio, que_sufijo, que_sememb, que_fechaemb) " +
                        "VALUES('" + semana + "', '" + fecha + "', '" + mes + "', '" + cliprim + "', '" + cliente + "', '" + sucursal + "', '" + reporto + "', '" + recibio + "', 'A', " +
                        "'" + cedis + "', '" + usuario + "', '" + tipo + "', '" + fechainv + "', '" + area_queja + "', '" + subcli_folio + "', '" + costo + "', '" + pedido + "', " +
                        "'" + transporte + "', '" + observaciones + "', '" + factura + "', '" + sufijo + "', '" + sem_emb + "', '" + fech_emb + "') SELECT SCOPE_IDENTITY()";
                    this.com.CommandText = cuerpo;
                    str2 = Convert.ToString(this.com.ExecuteScalar()).Trim();
                    folio = str2;
                }

                this.Cerrar();
                return folio;
            }
            catch (SqlException ex)
            {
                this.enviarcorreo_error(nomRecibe, "NUEVO", cuerpo);
                this.Cerrar();
                return str1;
            }
        }

        public string insert_detalle_quejas_exp(
          string folio,
          string producto,
          string problema,
          string ordprod,
          string area,
          string responsable,
          string cantrecha,
          string cantreci,
          string unidad,
          string devolucion,
          string moneda,
          string cveprov,
          string cverch,
          string cvetbl,
          string lote,
          string nomprod,
          string fechacad,
          string causa,
          string tarima,
          string vari,
          string ptc_ptp)
        {
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_det_quejas(que_folio, id_producto, qud_problema, qud_ordprod, qud_area, qud_responsable, qud_cantrecha, qud_cantreci, qud_unidad, qud_devolucion, qud_moneda, prov_clave, rch_clave, tbl_clave, qud_variedad, qud_lote, nom_producto, fecha_cad, qud_causa, qud_tarima, qud_ptcptp) VALUES('" + folio + "', '" + producto + "', '" + problema + "', '" + ordprod + "', '" + area + "', '" + responsable + "', '" + cantrecha + "', '" + cantreci + "', '" + unidad + "', '" + devolucion + "', '" + moneda + "', '" + cveprov + "', '" + cverch + "', '" + cvetbl + "', '" + vari + "', '" + lote + "', '" + nomprod.Replace("'", " ").ToString() + "', '" + fechacad + "', '" + causa + "', '" + tarima + "', '" + ptc_ptp + "')";
                string str = "1";
                this.com.ExecuteNonQuery();
                this.Cerrar();
                return str;
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                return "0";
            }
        }

        public string insert_detalle_quejas_exp_copy(string query)
        {
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = query;
                string str = query;
                this.com.ExecuteNonQuery();
                this.Cerrar();
                return str;
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                return "0";
            }
        }

        public string update_queja_exp(
          string folio,
          string devolucion,
          string moneda,
          string problema,
          string costo,
          string area_queja,
          string observaciones,
          string merma, string bonificacion)
        {
            try
            {
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_quejas SET qud_devolucion = '" + devolucion + "', qud_moneda = '" + moneda + "', qud_problema = '" + problema + "', qud_merma = '" + merma + "', qud_bonificacion = '" + bonificacion + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas SET que_costo = '" + costo + "', area_queja = '" + area_queja + "', que_observacion = '" + observaciones + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                this.Cerrar();
                return "1";
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                return "0";
            }
        }

        public DataTable consulta_productos_exp(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_area", typeof(string));
            dataTable.Columns.Add("qud_responsable", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("qud_unidad", typeof(string));
            dataTable.Columns.Add("qud_devolucion", typeof(string));
            dataTable.Columns.Add("qud_moneda", typeof(string));
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("rch_clave", typeof(string));
            dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("qud_lote", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("fecha_cad", typeof(string));
            dataTable.Columns.Add("qud_tarima", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("rch_nombre", typeof(string));
            dataTable.Columns.Add("tbl_nombre", typeof(string));
            dataTable.Columns.Add("qud_ptcptp", typeof(string));
            dataTable.Columns.Add("qud_cjsprod", typeof(string));
            dataTable.Columns.Add("qud_porcen", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.id_producto, A.qud_problema, A.qud_ordprod, A.qud_area, A.qud_responsable, A.qud_cantrecha, A.qud_cantreci, A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.prov_clave, A.rch_clave, A.tbl_clave, A.qud_variedad, A.qud_lote, A.nom_producto, A.fecha_cad, A.qud_tarima, A.qud_ptcptp, A.qud_cjsprod, A.qud_porcen FROM tb_det_quejas A WHERE A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    row["qud_area"] = (object)this.read["qud_area"].ToString().Trim();
                    row["qud_responsable"] = (object)this.read["qud_responsable"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    row["qud_cantreci"] = (object)this.read["qud_cantreci"].ToString().Trim();
                    row["qud_unidad"] = (object)this.read["qud_unidad"].ToString().Trim();
                    row["qud_devolucion"] = (object)this.read["qud_devolucion"].ToString().Trim();
                    row["qud_moneda"] = (object)this.read["qud_moneda"].ToString().Trim();
                    row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    row["rch_clave"] = (object)this.read["rch_clave"].ToString().Trim();
                    row["tbl_clave"] = (object)this.read["tbl_clave"].ToString().Trim();
                    row["qud_lote"] = (object)this.read["qud_lote"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["fecha_cad"] = (object)this.read["fecha_cad"].ToString().Trim();
                    row["qud_tarima"] = (object)this.read["qud_tarima"].ToString().Trim();
                    row["prov_nombre"] = (object)"";
                    row["rch_nombre"] = (object)"";
                    row["tbl_nombre"] = (object)"";
                    row["qud_ptcptp"] = (object)this.read["qud_ptcptp"].ToString().Trim();
                    row["qud_cjsprod"] = (object)this.read["qud_cjsprod"].ToString().Trim();
                    row["qud_porcen"] = (object)this.read["qud_porcen"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor WHERE prov_clave = '" + row["prov_clave"].ToString() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT rch_nombre FROM tb_cat_ranchos WHERE prov_clave = '" + row["prov_clave"].ToString() + "' AND rch_clave = '" + row["rch_clave"].ToString().Trim() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["rch_nombre"] = (object)this.read["rch_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT tbl_nombre FROM tb_cat_tablas WHERE prov_clave = '" + row["prov_clave"].ToString() + "' AND rch_clave = '" + row["rch_clave"].ToString().Trim() + "' AND tbl_clave = '" + row["tbl_clave"].ToString().Trim() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["tbl_nombre"] = (object)this.read["tbl_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable consulta_productos_exp2(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            //dataTable.Columns.Add("qud_area", typeof(string));
            //dataTable.Columns.Add("qud_responsable", typeof(string));
            //dataTable.Columns.Add("qud_cantrecha", typeof(string));
            //dataTable.Columns.Add("qud_cantreci", typeof(string));
            //dataTable.Columns.Add("qud_unidad", typeof(string));
            //dataTable.Columns.Add("qud_devolucion", typeof(string));
            //dataTable.Columns.Add("qud_moneda", typeof(string));
            //dataTable.Columns.Add("prov_clave", typeof(string));
            //dataTable.Columns.Add("rch_clave", typeof(string));
            //dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("qud_lote", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("qud_ptcptp", typeof(string));
            //dataTable.Columns.Add("fecha_cad", typeof(string));
            dataTable.Columns.Add("qud_tarima", typeof(string));
            //dataTable.Columns.Add("prov_nombre", typeof(string));
            //dataTable.Columns.Add("rch_nombre", typeof(string));
            //dataTable.Columns.Add("tbl_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.id_producto, A.qud_problema, A.qud_ordprod, A.nom_producto, A.qud_ptcptp " +
                "FROM tb_det_quejas A WHERE A.que_folio = '" + folio + "' " +
                "GROUP BY A.id_producto, A.qud_problema, A.qud_ordprod, A.nom_producto, A.qud_ptcptp";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    //row["qud_area"] = (object)this.read["qud_area"].ToString().Trim();
                    //row["qud_responsable"] = (object)this.read["qud_responsable"].ToString().Trim();
                    //row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    //row["qud_cantreci"] = (object)this.read["qud_cantreci"].ToString().Trim();
                    //row["qud_unidad"] = (object)this.read["qud_unidad"].ToString().Trim();
                    //row["qud_devolucion"] = (object)this.read["qud_devolucion"].ToString().Trim();
                    //row["qud_moneda"] = (object)this.read["qud_moneda"].ToString().Trim();
                    //row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    //row["rch_clave"] = (object)this.read["rch_clave"].ToString().Trim();
                    //row["tbl_clave"] = (object)this.read["tbl_clave"].ToString().Trim();
                    //row["qud_lote"] = (object)this.read["qud_lote"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["qud_ptcptp"] = (object)this.read["qud_ptcptp"].ToString().Trim();
                    //row["fecha_cad"] = (object)this.read["fecha_cad"].ToString().Trim();
                    //row["qud_tarima"] = (object)this.read["qud_tarima"].ToString().Trim();
                    //row["prov_nombre"] = (object)"";
                    //row["rch_nombre"] = (object)"";
                    //row["tbl_nombre"] = (object)"";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            //foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            //{
            //    this.com = this.conn.CreateCommand();
            //    this.com.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor WHERE prov_clave = '" + row["prov_clave"].ToString() + "'";
            //    this.read = this.com.ExecuteReader();
            //    if (this.read.HasRows)
            //    {
            //        this.read.Read();
            //        row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
            //    }
            //    this.read.Close();
            //    this.read.Dispose();
            //    this.com.Dispose();
            //    this.com = this.conn.CreateCommand();
            //    this.com.CommandText = "SELECT rch_nombre FROM tb_cat_ranchos WHERE prov_clave = '" + row["prov_clave"].ToString() + "' AND rch_clave = '" + row["rch_clave"].ToString().Trim() + "'";
            //    this.read = this.com.ExecuteReader();
            //    if (this.read.HasRows)
            //    {
            //        this.read.Read();
            //        row["rch_nombre"] = (object)this.read["rch_nombre"].ToString().Trim();
            //    }
            //    this.read.Close();
            //    this.read.Dispose();
            //    this.com.Dispose();
            //    this.com = this.conn.CreateCommand();
            //    this.com.CommandText = "SELECT tbl_nombre FROM tb_cat_tablas WHERE prov_clave = '" + row["prov_clave"].ToString() + "' AND rch_clave = '" + row["rch_clave"].ToString().Trim() + "' AND tbl_clave = '" + row["tbl_clave"].ToString().Trim() + "'";
            //    this.read = this.com.ExecuteReader();
            //    if (this.read.HasRows)
            //    {
            //        this.read.Read();
            //        row["tbl_nombre"] = (object)this.read["tbl_nombre"].ToString().Trim();
            //    }
            //    this.read.Close();
            //    this.read.Dispose();
            //    this.com.Dispose();
            //}
            this.Cerrar();
            return dataTable;
        }

        public DataTable consulta_productos_exp_update_rechazo(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("conse", typeof(string));
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_area", typeof(string));
            dataTable.Columns.Add("qud_responsable", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("qud_unidad", typeof(string));
            dataTable.Columns.Add("qud_devolucion", typeof(string));
            dataTable.Columns.Add("qud_moneda", typeof(string));
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("rch_clave", typeof(string));
            dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("qud_lote", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("fecha_cad", typeof(string));
            dataTable.Columns.Add("qud_tarima", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("rch_nombre", typeof(string));
            dataTable.Columns.Add("tbl_nombre", typeof(string));
            dataTable.Columns.Add("qud_cjsprod", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.conse, A.id_producto, A.qud_problema, A.qud_ordprod, A.qud_area, A.qud_responsable, A.qud_cantrecha, A.qud_cantreci, A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.prov_clave, A.rch_clave, A.tbl_clave, A.qud_variedad, A.qud_lote, A.nom_producto, A.fecha_cad, A.qud_tarima, A.qud_cjsprod FROM tb_det_quejas A WHERE A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["conse"] = (object)this.read["conse"].ToString().Trim();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    row["qud_area"] = (object)this.read["qud_area"].ToString().Trim();
                    row["qud_responsable"] = (object)this.read["qud_responsable"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    row["qud_cantreci"] = (object)this.read["qud_cantreci"].ToString().Trim();
                    row["qud_unidad"] = (object)this.read["qud_unidad"].ToString().Trim();
                    row["qud_devolucion"] = (object)this.read["qud_devolucion"].ToString().Trim();
                    row["qud_moneda"] = (object)this.read["qud_moneda"].ToString().Trim();
                    row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    row["rch_clave"] = (object)this.read["rch_clave"].ToString().Trim();
                    row["tbl_clave"] = (object)this.read["tbl_clave"].ToString().Trim();
                    row["qud_lote"] = (object)this.read["qud_lote"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["fecha_cad"] = (object)this.read["fecha_cad"].ToString().Trim();
                    row["qud_tarima"] = (object)this.read["qud_tarima"].ToString().Trim();
                    row["prov_nombre"] = (object)"";
                    row["rch_nombre"] = (object)"";
                    row["tbl_nombre"] = (object)"";
                    row["qud_cjsprod"] = (object)this.read["qud_cjsprod"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor WHERE prov_clave = '" + row["prov_clave"].ToString() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT rch_nombre FROM tb_cat_ranchos WHERE prov_clave = '" + row["prov_clave"].ToString() + "' AND rch_clave = '" + row["rch_clave"].ToString().Trim() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["rch_nombre"] = (object)this.read["rch_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT tbl_nombre FROM tb_cat_tablas WHERE prov_clave = '" + row["prov_clave"].ToString() + "' AND rch_clave = '" + row["rch_clave"].ToString().Trim() + "' AND tbl_clave = '" + row["tbl_clave"].ToString().Trim() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["tbl_nombre"] = (object)this.read["tbl_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return dataTable;
        }

        public string consulta_devolucion_exp(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT TOP 1 A.qud_devolucion, A.qud_moneda, A.prov_clave, A.qud_merma, A.qud_bonificacion FROM tb_det_quejas A WHERE A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            string str = "";
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    str = (this.read["qud_devolucion"].ToString().Trim() == "1") ? "DEV-" + this.read["qud_devolucion"].ToString().Trim() :
                        (this.read["qud_merma"].ToString().Trim() == "1") ? "MER-" + this.read["qud_merma"].ToString().Trim() : (this.read["qud_bonificacion"].ToString().Trim() == "1") ? "BON-" + this.read["qud_bonificacion"].ToString().Trim() : "";
                }

            }
            else
                str = "0";
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string consulta_merma_exp(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT TOP 1 A.qud_merma FROM tb_det_quejas A WHERE A.que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            string str = "";
            if (this.read.HasRows)
            {
                while (this.read.Read())
                    str = this.read["qud_merma"].ToString().Trim();
            }
            else
                str = "0";
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable consultadetqueja_pedido(string valor)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("qud_unidad", typeof(string));
            dataTable.Columns.Add("qud_devolucion", typeof(string));
            dataTable.Columns.Add("qud_moneda", typeof(string));
            dataTable.Columns.Add("fecha_cad", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.id_producto, A.nom_producto, A.qud_problema, B.pro_nombre, A.qud_ordprod, SUM(A.qud_cantreci) AS qud_cantreci, SUM(A.qud_cantrecha) AS qud_cantrecha, A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.fecha_cad FROM tb_det_quejas A, tb_cat_problemas B, tb_mstr_quejas C WHERE A.que_folio = '" + valor + "' AND A.qud_problema = B.pro_clave and A.que_folio = C.que_folio and C.que_status in ('A','T') GROUP BY A.id_producto, A.nom_producto, A.qud_problema, B.pro_nombre, A.qud_ordprod, A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.fecha_cad ORDER BY A.nom_producto";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["id_producto"] = (object)this.read.GetValue(0).ToString().Trim();
                    row["nom_producto"] = (object)this.read.GetValue(1).ToString().Trim();
                    row["qud_problema"] = (object)this.read.GetValue(2).ToString().Trim();
                    row["problema"] = (object)this.read.GetValue(3).ToString().Trim();
                    row["qud_ordprod"] = (object)this.read.GetValue(4).ToString().Trim();
                    row["qud_cantreci"] = (object)this.read.GetValue(5).ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read.GetValue(6).ToString().Trim();
                    row["qud_unidad"] = (object)this.read.GetValue(7).ToString().Trim();
                    row["qud_devolucion"] = (object)this.read.GetValue(8).ToString().Trim();
                    row["qud_moneda"] = (object)this.read.GetValue(9).ToString().Trim();
                    row["fecha_cad"] = (object)this.read.GetValue(10).ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string trer_cedis(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT cedis FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            string str = "";
            if (this.read.HasRows)
            {
                while (this.read.Read())
                    str = this.read["cedis"].ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string consulta_cliente(string folio)
        {
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT que_cliente FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            string str = "";
            if (this.read.HasRows)
            {
                while (this.read.Read())
                    str = this.read["que_cliente"].ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public string variedad_busca_eti_final(string fol, string prod)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT recibo FROM tb_det_eti_final WHERE folio = '" + fol + "' AND cve_prod = '" + prod + "'";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["recibo"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string variedad_busca_prod_odp(string fol, string rec)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select rmp_tipo from tb_det_prod_odp where ordp_folio = '" + fol + "' and rmp_recibo = '" + rec + "'";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["rmp_tipo"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string variedad_recepcion_mp(string rec)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select b.vari_nombre from tb_mstr_recepcion_mp a, tb_cat_variedad b where a.rmp_recibo = '" + rec + "' and a.vari_clave = b.vari_clave and a.lin_clave = b.lin_clave";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["vari_nombre"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string variedad_prod_ode_tipo(string rec)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select rmp_recibo, rmp_tipo, prod_clave from tb_det_prod_ode where orde_folio = '" + rec + "'";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["rmp_tipo"].ToString().Trim() + "-" + this.read["rmp_tipo"].ToString().Trim() + "-" + this.read["prod_clave"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string variedad_recepcion_pt(string rec)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select b.vari_nombre from tb_mstr_recepcion_pt a, tb_cat_variedad b where a.rpt_recibo = '" + rec + "' and a.vari_clave = b.vari_clave and a.lin_clave = b.lin_clave";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["vari_nombre"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string variedad_recepcion_esparrago(string rec)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select b.vari_nombre from tb_mstr_recepcion_esparrago a, tb_cat_variedad b where a.rmp_recibo = '" + rec + "' and a.vari_clave = b.vari_clave and a.lin_clave = b.lin_clave";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["vari_nombre"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public DataTable filtroAreas()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_area", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select id_area, area_nombre from tb_cat_areas_queja order by area_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["id_area"] = (object)"0";
            row1["area_nombre"] = (object)"SELECCIONAR AREA...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["id_area"] = (object)this.read["id_area"].ToString().Trim();
                    row2["area_nombre"] = (object)this.read["area_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable filtroProductos()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select id_area, area_nombre from tb_cat_areas_queja order by area_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["id_area"] = (object)"0";
            row1["area_nombre"] = (object)"SELECCIONAR AREA...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["id_area"] = (object)this.read["id_area"].ToString().Trim();
                    row2["area_nombre"] = (object)this.read["area_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable filtroProblemas()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select id_area, area_nombre from tb_cat_areas_queja order by area_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["id_area"] = (object)"0";
            row1["area_nombre"] = (object)"SELECCIONAR AREA...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["id_area"] = (object)this.read["id_area"].ToString().Trim();
                    row2["area_nombre"] = (object)this.read["area_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string cajas_producidas_folio(string rec, string pro)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select b.rptd_cantidad from tb_det_recepcion_pt B join tb_mstr_recepcion_pt a on b.rpt_recibo = a.rpt_recibo where a.rpt_recibo = '" + rec + "' and prod_clave = '" + pro + "'";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["rptd_cantidad"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string cajas_producidas_folio2(string rec, string pro, string tar)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE folio = '" + rec + "' AND cve_prod = '" + pro + "'";
            try
            {
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["num_cajas"].ToString().Trim();
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public DataTable Detalle_Productos_Queja(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select DISTINCT(id_producto), nom_producto from tb_det_quejas WHERE que_folio = '" + folio + "' order by nom_producto";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable Detalle_Variedades_Queja(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select DISTINCT(qud_variedad) AS variedad from tb_det_quejas WHERE que_folio = '" + folio + "' order by qud_variedad";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["id_producto"] = (object)"";
                    row["nom_producto"] = (object)this.read["variedad"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string registro_movimiento(
          string fecha,
          string computadora,
          string usuario,
          string movimiento,
          string opcion,
          string folio,
          string detalle,
          string sistema)
        {
            try
            {
                fecha = !fecha.Contains(" p. m.") ? fecha.Replace(" a. m.", "") : fecha.Replace(" p. m.", "");
                this.Abrir();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_registro_movimientos(fecha, nom_compu, nom_usu, tipo_mov, op_clave, folio, detalle, sistema) VALUES ('" + fecha + "', '" + computadora + "', '" + usuario + "', '" + movimiento + "', '', '" + folio + "', '" + detalle + "', '" + sistema + "')";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                this.Cerrar();
                return "1";
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                return "0";
            }
        }

        public DataTable general_mstr_quejas_principal(
          DataTable dtFiltro,
          string clave,
          string cedis,
          string admin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_mes", typeof(string));
            dataTable.Columns.Add("que_cliente", typeof(string));
            dataTable.Columns.Add("que_sucursal", typeof(string));
            dataTable.Columns.Add("que_reporto", typeof(string));
            dataTable.Columns.Add("que_recibio", typeof(string));
            dataTable.Columns.Add("cedis", typeof(string));
            dataTable.Columns.Add("area_queja", typeof(string));
            dataTable.Columns.Add("que_fecha_efe", typeof(string));
            dataTable.Columns.Add("que_cumpli_efe", typeof(string));
            dataTable.Columns.Add("que_comen_efe", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("resp_paterno", typeof(string));
            dataTable.Columns.Add("area_nombre", typeof(string));
            dataTable.Columns.Add("pedido", typeof(string));
            dataTable.Columns.Add("transporte", typeof(string));
            this.Abrir();
            foreach (DataRow row1 in (InternalDataCollectionBase)dtFiltro.Rows)
            {
                this.com = this.conn.CreateCommand();
                if (admin == "ADMINISTRADOR")
                {
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, " +
                        "A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte " +
                        "FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.que_folio = '" + row1["que_folio"].ToString() + "' " +
                        "and A.que_status in ('A','T') AND A.que_fecha >= '" + DateTime.Now.AddMonths(-4).ToString("dd/MM/yyyy") + "' ORDER BY A.que_folio";
                }

                else
                    this.com.CommandText = "SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_mes, A.que_cliente, A.que_sucursal, A.que_reporto, A.que_recibio, A.cedis, A.area_queja, A.que_fecha_efe, " +
                        "A.que_cumpli_efe, A.que_comen_efe, B.resp_nombre, B.resp_paterno, C.area_nombre, A.que_pedido, A.que_transporte " +
                        "FROM tb_mstr_quejas A, tb_cat_responsables B, tb_cat_areas_queja C WHERE B.resp_clave = A.que_recibio AND C.id_area = A.area_queja AND A.cedis = '" + cedis + "' " +
                        "AND A.que_folio = '" + row1["que_folio"].ToString() + "' and A.que_status in ('A','T') AND A.que_fecha >= '" + DateTime.Now.AddMonths(-4).ToString("dd/MM/yyyy") + "' ORDER BY A.que_folio";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        DataRow row2 = dataTable.NewRow();
                        row2["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                        row2["que_semana"] = (object)this.read["que_semana"].ToString().Trim();
                        row2["que_fecha"] = (object)this.read["que_fecha"].ToString().Trim();
                        row2["que_mes"] = (object)this.read["que_mes"].ToString().Trim();
                        row2["que_cliente"] = (object)this.read["que_cliente"].ToString().Trim();
                        row2["que_sucursal"] = (object)this.read["que_sucursal"].ToString().Trim();
                        row2["que_reporto"] = (object)this.read["que_reporto"].ToString().Trim();
                        row2["que_recibio"] = (object)this.read["que_recibio"].ToString().Trim();
                        row2["cedis"] = (object)this.read["cedis"].ToString().Trim();
                        row2["area_queja"] = (object)this.read["area_queja"].ToString().Trim();
                        row2["que_fecha_efe"] = (object)this.read["que_fecha_efe"].ToString().Trim();
                        row2["que_cumpli_efe"] = (object)this.read["que_cumpli_efe"].ToString().Trim();
                        row2["que_comen_efe"] = (object)this.read["que_comen_efe"].ToString().Trim();
                        row2["resp_nombre"] = (object)this.read["resp_nombre"].ToString().Trim();
                        row2["resp_paterno"] = (object)this.read["resp_paterno"].ToString().Trim();
                        row2["area_nombre"] = (object)this.read["area_nombre"].ToString().Trim();
                        row2["pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                        row2["transporte"] = (object)this.read["que_transporte"].ToString().Trim();
                        dataTable.Rows.Add(row2);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable general_det_quejas_principal()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_problema", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_area", typeof(string));
            dataTable.Columns.Add("qud_responsable", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("qud_unidad", typeof(string));
            dataTable.Columns.Add("qud_devolucion", typeof(string));
            dataTable.Columns.Add("qud_moneda", typeof(string));
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("rch_clave", typeof(string));
            dataTable.Columns.Add("tbl_clave", typeof(string));
            dataTable.Columns.Add("qud_variedad", typeof(string));
            dataTable.Columns.Add("qud_lote", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("fecha_cad", typeof(string));
            dataTable.Columns.Add("qud_tarima", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("rch_nombre", typeof(string));
            dataTable.Columns.Add("tbl_nombre", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.id_producto, A.qud_problema, A.qud_ordprod, A.qud_area, A.qud_responsable, A.qud_cantrecha, A.qud_cantreci, A.qud_unidad, A.qud_devolucion, A.qud_moneda, A.prov_clave, " +
                "A.rch_clave, A.tbl_clave, A.qud_variedad, A.qud_lote, A.nom_producto, A.fecha_cad, A.qud_tarima FROM tb_det_quejas A JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio " +
                "WHERE B.que_fecha >= '" + DateTime.Now.AddMonths(-4).ToString("dd/MM/yyyy") + "' AND B.que_status in ('A','T') ORDER BY que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_problema"] = (object)this.read["qud_problema"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    row["qud_area"] = (object)this.read["qud_area"].ToString().Trim();
                    row["qud_responsable"] = (object)this.read["qud_responsable"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    row["qud_cantreci"] = (object)this.read["qud_cantreci"].ToString().Trim();
                    row["qud_unidad"] = (object)this.read["qud_unidad"].ToString().Trim();
                    row["qud_devolucion"] = (object)this.read["qud_devolucion"].ToString().Trim();
                    row["qud_moneda"] = (object)this.read["qud_moneda"].ToString().Trim();
                    row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    row["rch_clave"] = (object)this.read["rch_clave"].ToString().Trim();
                    row["tbl_clave"] = (object)this.read["tbl_clave"].ToString().Trim();
                    row["qud_variedad"] = (object)this.read["qud_variedad"].ToString().Trim();
                    row["qud_lote"] = (object)this.read["qud_lote"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["fecha_cad"] = (object)this.read["fecha_cad"].ToString().Trim();
                    row["qud_tarima"] = (object)this.read["qud_tarima"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT pro_nombre FROM tb_cat_problemas WHERE pro_clave = '" + row["qud_problema"] + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor WHERE prov_clave = '" + row["prov_clave"] + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                if (row["rch_clave"].ToString() != "")
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT rch_nombre FROM tb_cat_ranchos WHERE prov_clave = '" + row["prov_clave"] + "' AND rch_clave = '" + row["rch_clave"] + "'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        this.read.Read();
                        row["rch_nombre"] = (object)this.read["rch_nombre"].ToString().Trim();
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                    if (row["tbl_clave"].ToString() != "")
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT tbl_nombre FROM tb_cat_tablas WHERE prov_clave = '" + row["prov_clave"] + "' AND rch_clave = '" + row["rch_clave"] + "' AND tbl_clave = '" + row["tbl_clave"] + "'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            this.read.Read();
                            row["tbl_nombre"] = (object)this.read["tbl_nombre"].ToString().Trim();
                        }
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();
                    }
                }
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable general_det_investigacion_principal()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("inv_fecharegistro", typeof(string));
            dataTable.Columns.Add("inv_fechaentrega", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));
            dataTable.Columns.Add("inv_causa", typeof(string));
            dataTable.Columns.Add("inv_fechaaccion", typeof(string));
            dataTable.Columns.Add("inv_causa_fecha", typeof(string));
            dataTable.Columns.Add("responsable", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.inv_responsable, A.inv_fecharegistro, A.inv_fechaentrega, A.inv_comentario, A.inv_causa, A.inv_fechaaccion, A.inv_causa_fecha, B.resp_nombre, B.resp_paterno " +
                "FROM tb_det_investigacion A, tb_cat_responsables B, tb_mstr_quejas C WHERE A.inv_responsable = B.resp_clave AND A.que_folio = C.que_folio AND " +
                "C.que_fecha >= '" + DateTime.Now.AddMonths(-4).ToString("dd/MM/yyyy") + "' AND C.que_status in ('A','T') ORDER BY A.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["inv_responsable"] = (object)this.read["inv_responsable"].ToString().Trim();
                    row["inv_fecharegistro"] = (object)Convert.ToDateTime(this.read["inv_fecharegistro"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_fechaentrega"] = (object)Convert.ToDateTime(this.read["inv_fechaentrega"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_comentario"] = (object)this.read["inv_comentario"].ToString().Trim();
                    row["inv_causa"] = (object)this.read["inv_causa"].ToString().Trim();
                    row["inv_fechaaccion"] = (object)Convert.ToDateTime(this.read["inv_fechaaccion"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["inv_causa_fecha"] = (object)"";
                    row["responsable"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable general_det_acciones_principal()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("acc_folio", typeof(string));
            dataTable.Columns.Add("acc_responsable", typeof(string));
            dataTable.Columns.Add("acc_accion", typeof(string));
            dataTable.Columns.Add("acc_fechatermino", typeof(string));
            dataTable.Columns.Add("acc_fecha_ver", typeof(string));
            dataTable.Columns.Add("acc_cumpli_ver", typeof(string));
            dataTable.Columns.Add("acc_comen_ver", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.acc_folio, A.acc_responsable, A.acc_accion, A.acc_fechatermino, A.acc_fecha_ver, A.acc_cumpli_ver, A.acc_comen_ver, B.resp_nombre, B.resp_paterno " +
                "FROM tb_det_acciones A, tb_cat_responsables B, tb_mstr_quejas C WHERE A.acc_responsable = B.resp_clave AND A.que_folio = C.que_folio " +
                "AND C.que_fecha >= '" + DateTime.Now.AddMonths(-4).ToString("dd/MM/yyyy") + "' AND C.que_status in ('A','T') ORDER BY A.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["acc_folio"] = (object)this.read["acc_folio"].ToString().Trim();
                    row["acc_responsable"] = (object)this.read["acc_responsable"].ToString().Trim();
                    row["acc_accion"] = (object)this.read["acc_accion"].ToString().Trim();
                    row["acc_fechatermino"] = this.read["acc_fechatermino"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fechatermino"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_fecha_ver"] = this.read["acc_fecha_ver"].ToString().Trim() != "" ? (object)Convert.ToDateTime(this.read["acc_fecha_ver"].ToString().Trim()).ToString("dd/MM/yyyy") : (object)"";
                    row["acc_cumpli_ver"] = (object)this.read["acc_cumpli_ver"].ToString().Trim();
                    row["acc_comen_ver"] = (object)this.read["acc_comen_ver"].ToString().Trim();
                    row["resp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string validar_ingreso_pedido(string pedido, string expnal, string clave, string producto)
        {
            string str = "";
            this.Abrir();

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

            //if (val_tipo == "")//busqueda en pedido y factura segun corresponda
            //{
            //    this.com = this.conn.CreateCommand();
            //    if (expnal == "N")
            //        this.com.CommandText = "SELECT A.pdn_folio FROM tb_mstr_pedidos_nal JOIN tb_det_pedidos B " +
            //            "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo" +
            //            "WHERE A.pdn_folio = '" + pedido + "' AND AND A.pdn_tipo  = '" + val_tipo + "' AND B.pdn_tipo = '" + val_tipo + "' AND B.prod_clave = '" + producto + "'";
            //    else if (expnal == "E")
            //        this.com.CommandText = "SELECT A.pdn_folio FROM tb_mstr_pedidos_exp JOIN tb_det_pedidos B " +
            //            "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo" +
            //            "WHERE A.pdn_folio = '" + pedido + "' AND AND A.pdn_tipo  = 'EXP' AND B.pdn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";//this.com.CommandText = "SELECT pdn_folio FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + pedido + "'";
            //    else
            //        str = "0";
            //    if (str != "0")
            //    {
            //        try
            //        {
            //            this.read = this.com.ExecuteReader();
            //            if (this.read.HasRows)//cuando lo encuentra dentro de los pedidos
            //            {
            //                this.read.Read();
            //                str = this.read["pdn_folio"].ToString().Trim();
            //            }
            //            else //lo busca dentro de las facturas
            //            {
            //                read.Close();
            //                read.Dispose();
            //                if (expnal == "N")
            //                    this.com.CommandText = "SELECT A.fcn_folio FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
            //                        "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
            //                        "WHERE A.fcn_folio = '" + pedido + "' AND AND A.fcn_lugar  = '" + val_tipo + "' AND B.fcn_tipo = '" + val_tipo + "' AND B.prod_clave = '" + producto + "'";
            //                else if (expnal == "E")
            //                    this.com.CommandText = "SELECT A.fcn_folio FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
            //                        "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
            //                        "WHERE A.fcn_folio = '" + pedido + "' AND AND A.fcn_lugar  = 'EXP' AND B.fcn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";
            //                this.read = this.com.ExecuteReader();
            //                if (this.read.HasRows)//cuando lo encuentra dentro de los pedidos
            //                {
            //                    this.read.Read();
            //                    str = this.read["pdn_folio"].ToString().Trim();
            //                }
            //                else //lo busca dentro de las facturas
            //                    str = "0";
            //            }
            //            read.Close();
            //            read.Dispose();
            //        }
            //        catch (SqlException ex)
            //        {
            //            this.Cerrar();
            //            str = "0";
            //        }
            //    }
            //}
            //else
            //{
            this.com = this.conn.CreateCommand();
            if (expnal == "N")
                this.com.CommandText = "SELECT A.pdn_folio FROM tb_mstr_pedidos_nal A JOIN tb_det_pedidos B " +
                    "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo " +
                    "WHERE A.pdn_folio = '" + pedido + "' AND A.pdn_tipo  = 'NAL' AND B.pdn_tipo = 'NAL' AND B.prod_clave = '" + producto + "'";
            else if (expnal == "E")
                this.com.CommandText = "SELECT A.pdn_folio FROM tb_mstr_pedidos_exp A JOIN tb_det_pedidos B " +
                    "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo " +
                    "WHERE A.pdn_folio = '" + pedido + "' AND A.pdn_tipo  = 'EXP' AND B.pdn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";//this.com.CommandText = "SELECT pdn_folio FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + pedido + "'";
            else
                str = "0";
            if (str != "0")
            {
                try
                {
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)//cuando lo encuentra dentro de los pedidos
                    {
                        this.read.Read();
                        str = this.read["pdn_folio"].ToString().Trim();
                    }
                    else //lo busca dentro de las facturas
                    {
                        read.Close();
                        read.Dispose();
                        if (expnal == "N")
                            this.com.CommandText = "SELECT A.fcn_folio FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
                                "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
                                "WHERE A.fcn_folio = '" + pedido + "' AND AND A.fcn_lugar  = '" + val_tipo + "' AND B.fcn_tipo = '" + val_tipo + "' AND B.prod_clave = '" + producto + "'";
                        else if (expnal == "E")
                            this.com.CommandText = "SELECT A.fcn_folio FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
                                "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
                                "WHERE A.fcn_folio = '" + pedido + "' AND A.fcn_lugar  = 'EXP' AND B.fcn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)//cuando lo encuentra dentro de los pedidos
                        {
                            this.read.Read();
                            str = this.read["pdn_folio"].ToString().Trim();
                        }
                        else //lo busca dentro de las facturas
                            str = "0";
                    }
                    read.Close();
                    read.Dispose();
                }
                catch (SqlException ex)
                {
                    this.Cerrar();
                    str = "0";
                }
            }
            //}
            this.Cerrar();
            return str;
        }

        public DataTable detalle_embarque_servicios(string placa, string fecha)
        {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("pdn_folio", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT emb_folio FROM tb_mstr_embarque WHERE no_trailer = '" + placa + "' AND hora_trailer like '" + fecha + "%'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["pdn_folio"] = (object)this.read["emb_folio"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string tipo_pedido_servicios(string pedido)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT nalexp FROM tb_mstr_embarque WHERE emb_folio = '" + pedido + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                    str = this.read["nalexp"].ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable detalle_emb_servicios(string placa, string fecha)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("pdn_folio", typeof(string));
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("prod_clave", typeof(string));
            dataTable2.Columns.Add("prod_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT emb_folio FROM tb_mstr_embarque WHERE no_trailer = '" + placa + "' AND hora_trailer like '" + fecha + "%'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable1.NewRow();
                    row["pdn_folio"] = (object)this.read["emb_folio"].ToString().Trim();
                    dataTable1.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable1.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT A.prod_clave, B.prod_nombre, A.recibo, A.tarima, A.tipo_rec FROM tb_det_embarque A INNER JOIN tb_cat_producto b ON A.prod_clave = B.prod_clave WHERE A.emb_folio = '" + row1["pdn_folio"].ToString() + "' ORDER BY B.prod_nombre";
                this.read = this.com.ExecuteReader();
                DataRow row2 = dataTable2.NewRow();
                row2["prod_clave"] = (object)"";
                row2["prod_nombre"] = (object)"SELECCIONAR PRODUCTO...";
                dataTable2.Rows.Add(row2);
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        DataRow row3 = dataTable2.NewRow();
                        row3["prod_clave"] = (object)this.read["prod_clave"].ToString().Trim();
                        row3["prod_nombre"] = (object)(this.read["prod_nombre"].ToString().Trim() + " * " + this.read["recibo"].ToString().Trim() + " * " + this.read["tarima"].ToString().Trim() + " * " + this.read["tipo_rec"].ToString().Trim());
                        dataTable2.Rows.Add(row3);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return dataTable2;
        }

        public string rancho_tabla(string pro, string rec, string tar, string tip)
        {
            string str = "";
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT rch_nombre, tbl_nombre FROM tb_det_trazabilidad WHERE prod_clave = '" + pro + "' AND recibo = '" + rec + "' AND tarima = '" + tar + "' AND tipo = '" + tip + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                    str = this.read["rch_nombre"].ToString().Trim() + " - " + this.read["tbl_nombre"].ToString().Trim();
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return str;
        }

        public DataTable combo_insectos()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ins_folio", typeof(string));
            dataTable.Columns.Add("ins_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT ins_folio, ins_nombre FROM tb_cat_insectos_quejas WHERE ins_estatus = 'A' ORDER BY ins_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["ins_folio"] = (object)"";
            row1["ins_nombre"] = (object)"SELECCIONE INSECTO...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["ins_folio"] = (object)this.read["ins_folio"].ToString().Trim();
                    row2["ins_nombre"] = (object)this.read["ins_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable combo_qujas_serv()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ser_folio", typeof(string));
            dataTable.Columns.Add("ser_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT ser_folio, ser_nombre FROM tb_cat_problemas_serv WHERE ser_estatus = 'A' ORDER BY ser_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["ser_folio"] = (object)"";
            row1["ser_nombre"] = (object)"SELECCIONE QUEJA...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["ser_folio"] = (object)this.read["ser_folio"].ToString().Trim();
                    row2["ser_nombre"] = (object)this.read["ser_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string guarda_servicio(
          DataTable dtAdd,
          string fecha,
          string semana,
          string fecha_emb,
          string tipo,
          string pedido,
          string mes,
          string transportista,
          string destino,
          string caja,
          string responsable,
          string aduana,
          string comentario,
          string perdida_mn,
          string perdida_usd,
          string fumi_trans,
          string nomR,
            string sin_reg,
            string embtracli)
        {
            string cuerpo = "";
            string str1 = "";
            this.Abrir();
            string str2;
            try
            {
                this.com = this.conn.CreateCommand();
                cuerpo = "INSERT INTO tb_mstr_quejas_serv(que_fecha, que_semana, que_fechaemb, que_tipo, que_pedido, que_mes, que_transportista, que_destino, que_caja, que_responsable, que_aduana, que_comentario, que_perdida_mn, que_perdida_usd, que_fumi_trans, sin_reg, embtracli) VALUES('" + fecha + "', '" + semana + "', '" + fecha_emb + "', '" + tipo + "', '" + pedido + "', '" + mes + "', '" + transportista + "', '" + destino + "', '" + caja + "', '" + responsable + "', '" + aduana + "', '" + comentario + "', '" + perdida_mn + "', '" + perdida_usd + "', '" + fumi_trans + "', '" + sin_reg + "', '" + embtracli + "') SELECT SCOPE_IDENTITY()";
                this.com.CommandText = cuerpo;
                str1 = Convert.ToString(this.com.ExecuteScalar());
                this.com.Dispose();
                str2 = str1;
            }
            catch (SqlException ex)
            {
                this.enviarcorreo_error(nomR, "NUEVO SERVICIO", cuerpo);
                str2 = "0";
            }
            if (str2 != "0")
            {
                try
                {
                    foreach (DataRow row in (InternalDataCollectionBase)dtAdd.Rows)
                    {
                        cuerpo = "INSERT INTO tb_det_queja_serv(que_folio, que_clave, que_nombre)VALUES('" + str1 + "', '" + row["ser_folio"].ToString() + "', '" + row["ser_nombre"].ToString() + "')";
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = cuerpo;
                        this.com.ExecuteNonQuery();
                        this.com.Dispose();
                    }
                    str2 = str1 + ".2";
                }
                catch (SqlException ex)
                {
                    this.enviarcorreo_error(nomR, "ERROR INSERT DETALLE SERVICIO FOLIO: " + str1, cuerpo);
                    str2 = "0";
                }
            }
            this.Cerrar();
            return str2;
        }

        public DataTable quejas_servicio(string resp, string admin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(Int32));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("que_transportista", typeof(string));
            dataTable.Columns.Add("que_destino", typeof(string));
            dataTable.Columns.Add("que_caja", typeof(string));
            dataTable.Columns.Add("que_perdida_mn", typeof(string));
            dataTable.Columns.Add("que_perdida_usd", typeof(string));
            dataTable.Columns.Add("que_clave", typeof(string));
            dataTable.Columns.Add("que_nombre", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("embtracli", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (admin == "ADMINISTRADOR")
                this.com.CommandText = " SELECT A.que_folio, que_semana, A.que_fechaemb AS que_fecha, A.que_pedido, A.que_transportista, A.que_destino, A.que_caja, A.que_perdida_mn, A.que_perdida_usd, A.que_tipo, B.que_clave, B.que_nombre, A.embtracli FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_queja_serv B ON A.que_folio = B.que_folio WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'T' ORDER BY A.que_folio DESC";
            else
                this.com.CommandText = " SELECT A.que_folio, que_semana, A.que_fechaemb AS que_fecha, A.que_pedido, A.que_transportista, A.que_destino, A.que_caja, A.que_perdida_mn, A.que_perdida_usd, A.que_tipo, B.que_clave, B.que_nombre, A.embtracli FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_queja_serv B ON A.que_folio = B.que_folio WHERE A.que_responsable = '" + resp + "' AND A.que_estatus = 'A' AND A.que_fumi_trans = 'T' ORDER BY A.que_folio DESC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["que_transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["que_destino"] = (object)this.read["que_destino"].ToString().Trim();
                    row["que_caja"] = (object)this.read["que_caja"].ToString().Trim();
                    DataRow dataRow1 = row;
                    Decimal num = Convert.ToDecimal(this.read["que_perdida_mn"].ToString().Trim());
                    string str1 = num.ToString("###,###,##0.00");
                    dataRow1["que_perdida_mn"] = (object)str1;
                    DataRow dataRow2 = row;
                    num = Convert.ToDecimal(this.read["que_perdida_usd"].ToString().Trim());
                    string str2 = num.ToString("###,###,##0.00");
                    dataRow2["que_perdida_usd"] = (object)str2;
                    row["que_clave"] = (object)this.read["que_clave"].ToString().Trim();
                    row["que_nombre"] = (object)this.read["que_nombre"].ToString().Trim();
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["embtracli"] = (object)this.read["embtracli"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }

            DataView dv = dataTable.DefaultView;
            dv.Sort = "que_folio DESC";
            dataTable = dv.ToTable();
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        //21/09/2023
        public DataTable quejas_servicio_rep_excel_fechas(string resp, string admin, string fecha, string fechafin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(Int32));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("que_transportista", typeof(string));
            dataTable.Columns.Add("que_destino", typeof(string));
            dataTable.Columns.Add("que_caja", typeof(string));
            dataTable.Columns.Add("que_perdida_mn", typeof(string));
            dataTable.Columns.Add("que_perdida_usd", typeof(string));
            dataTable.Columns.Add("que_clave", typeof(string));
            dataTable.Columns.Add("que_nombre", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("embtracli", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            //if (admin == "ADMINISTRADOR")
            this.com.CommandText = " SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_pedido, A.que_transportista, A.que_destino, A.que_caja, A.que_perdida_mn, A.que_perdida_usd, A.que_tipo, B.que_clave, B.que_nombre, A.embtracli FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_queja_serv B ON A.que_folio = B.que_folio WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'T' AND A.que_fechaemb >= '" + fecha + "' AND A.que_fechaemb <= '" + fechafin + "' ORDER BY A.que_folio DESC";
            //else
            //    this.com.CommandText = " SELECT A.que_folio, A.que_semana, A.que_fecha, A.que_pedido, A.que_transportista, A.que_destino, A.que_caja, A.que_perdida_mn, A.que_perdida_usd, A.que_tipo, B.que_clave, B.que_nombre, A.embtracli FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_queja_serv B ON A.que_folio = B.que_folio WHERE A.que_responsable = '" + resp + "' AND A.que_estatus = 'A' AND A.que_fumi_trans = 'T' AND A.que_fechaemb >= '" + fecha + "' AND A.que_fechaemb <= '" + fechafin + "' ORDER BY A.que_folio DESC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["que_transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["que_destino"] = (object)this.read["que_destino"].ToString().Trim();
                    row["que_caja"] = (object)this.read["que_caja"].ToString().Trim();
                    DataRow dataRow1 = row;
                    Decimal num = Convert.ToDecimal(this.read["que_perdida_mn"].ToString().Trim());
                    string str1 = num.ToString("###,###,##0.00");
                    dataRow1["que_perdida_mn"] = (object)str1;
                    DataRow dataRow2 = row;
                    num = Convert.ToDecimal(this.read["que_perdida_usd"].ToString().Trim());
                    string str2 = num.ToString("###,###,##0.00");
                    dataRow2["que_perdida_usd"] = (object)str2;
                    row["que_clave"] = (object)this.read["que_clave"].ToString().Trim();
                    row["que_nombre"] = (object)this.read["que_nombre"].ToString().Trim();
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["embtracli"] = (this.read["embtracli"].ToString().Trim() == "0") ? "SIN ESPECIFICAR"
                        : (this.read["embtracli"].ToString().Trim() == "1") ? "EMBARQUES"
                        : (this.read["embtracli"].ToString().Trim() == "2") ? "TRANSPORTISTA" : "CLIENTE";
                    dataTable.Rows.Add(row);
                }
            }

            DataView dv = dataTable.DefaultView;
            dv.Sort = "que_folio DESC";
            dataTable = dv.ToTable();
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }
        //21/09/2023

        public DataTable quejas_servicio_mstr(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fechaemb", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("que_mes", typeof(string));
            dataTable.Columns.Add("que_transportista", typeof(string));
            dataTable.Columns.Add("que_destino", typeof(string));
            dataTable.Columns.Add("que_caja", typeof(string));
            dataTable.Columns.Add("que_aduana", typeof(string));
            dataTable.Columns.Add("que_comentario", typeof(string));
            dataTable.Columns.Add("que_perdida_mn", typeof(string));
            dataTable.Columns.Add("que_perdida_usd", typeof(string));
            dataTable.Columns.Add("que_fumi_trans", typeof(string));
            dataTable.Columns.Add("que_responsable", typeof(string));
            dataTable.Columns.Add("sin_reg", typeof(string));
            dataTable.Columns.Add("embtracli", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = " SELECT A.que_folio, A.que_fecha, A.que_semana, A.que_fechaemb, A.que_tipo, A.que_pedido, A.que_mes, A.que_transportista, A.que_destino, A.que_caja, A.que_aduana, A.que_comentario, " +
                "A.que_perdida_mn, A.que_perdida_usd, A.que_fumi_trans, B.resp_nombre, B.resp_paterno, A.sin_reg, A.embtracli FROM tb_mstr_quejas_serv A LEFT JOIN tb_cat_responsables B ON A.que_responsable = B.resp_clave WHERE A.que_folio = '" + folio + "' AND A.que_estatus = 'A' AND A.que_fumi_trans = 'T'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    //correo_error("entra");
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_semana"] = (object)this.read["que_semana"].ToString().Trim();
                    //correo_error("entra 1");  
                    row["que_fechaemb"] = (object)Convert.ToDateTime(this.read["que_fechaemb"].ToString().Trim()).ToString("dd/MM/yyyy");
                    //correo_error("entra 2");
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["que_mes"] = (object)this.read["que_mes"].ToString().Trim();
                    row["que_transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["que_destino"] = (object)this.read["que_destino"].ToString().Trim();
                    row["que_aduana"] = (object)this.read["que_aduana"].ToString().Trim();
                    row["que_caja"] = (object)this.read["que_caja"].ToString().Trim();
                    row["que_comentario"] = (object)this.read["que_comentario"].ToString().Trim();
                    row["que_perdida_mn"] = (object)this.read["que_perdida_mn"].ToString().Trim();
                    row["que_perdida_usd"] = (object)this.read["que_perdida_usd"].ToString().Trim();
                    row["que_fumi_trans"] = (object)this.read["que_fumi_trans"].ToString().Trim();
                    row["que_responsable"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["sin_reg"] = (object)this.read["sin_reg"].ToString().Trim();
                    row["embtracli"] = (object)this.read["embtracli"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable quejas_servicio_det(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_clave", typeof(string));
            dataTable.Columns.Add("que_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = " SELECT que_folio, que_clave, que_nombre FROM tb_det_queja_serv WHERE que_folio = '" + folio + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_clave"] = (object)this.read["que_clave"].ToString().Trim();
                    row["que_nombre"] = (object)this.read["que_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string modifica_servicio(
          string folio,
          string comentario,
          string perdida_mn,
          string perdida_usd, string area)
        {
            this.Abrir();
            string str;
            try
            {
                string p_mn = "";
                string p_usd = "";
                p_mn = (perdida_mn == "") ? "0" : Convert.ToDecimal(perdida_mn).ToString();
                p_usd = (perdida_usd == "") ? "0" : Convert.ToDecimal(perdida_usd).ToString();
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas_serv  SET que_comentario = '" + comentario + "', que_perdida_mn = '" + p_mn + "', que_perdida_usd = '" + p_usd + "', embtracli = '" + area + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public DataTable quejas_serv_excel(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Fecha_Embarque", typeof(string));
            dataTable.Columns.Add("Mes", typeof(string));
            dataTable.Columns.Add("Nacional/Exportacion", typeof(string));
            dataTable.Columns.Add("Orden_Venta", typeof(string));
            dataTable.Columns.Add("Transportista", typeof(string));
            dataTable.Columns.Add("Caja", typeof(string));
            dataTable.Columns.Add("Destino", typeof(string));
            dataTable.Columns.Add("Responsable", typeof(string));
            dataTable.Columns.Add("Tipo_Problema", typeof(string));
            dataTable.Columns.Add("Comentarios", typeof(string));
            dataTable.Columns.Add("Perdida_MN", typeof(string));
            dataTable.Columns.Add("Perdida_USD", typeof(string));
            dataTable.Columns.Add("Queja", typeof(string));
            dataTable.Columns.Add("Fecha", typeof(string));
            dataTable.Columns.Add("Area", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_fechaemb, A.que_mes, A.que_tipo, A.que_pedido, A.que_transportista, A.que_caja, A.que_destino, A.que_comentario, A.que_perdida_mn, A.que_perdida_usd, " +
                "B.resp_nombre, B.resp_paterno, A.que_folio, A.que_fecha, A.embtracli FROM tb_mstr_quejas_serv A LEFT JOIN tb_cat_responsables B ON A.que_responsable = B.resp_clave " +
                "WHERE A.que_folio = '" + folio + "' AND A.que_estatus = 'A' AND A.que_fumi_trans = 'T' ORDER BY A.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["Fecha_Embarque"] = (object)Convert.ToDateTime(this.read["que_fechaemb"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["Mes"] = (object)this.read["que_mes"].ToString().Trim();
                    row["Nacional/Exportacion"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["Orden_Venta"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["Transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["Caja"] = (object)this.read["que_caja"].ToString().Trim();
                    row["Destino"] = (object)this.read["que_destino"].ToString().Trim();
                    row["Responsable"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["Comentarios"] = (object)this.read["que_comentario"].ToString().Trim();
                    row["Perdida_MN"] = (object)this.read["que_perdida_mn"].ToString().Trim();
                    row["Perdida_USD"] = (object)this.read["que_perdida_usd"].ToString().Trim();
                    row["Queja"] = (object)this.read["que_folio"].ToString().Trim();
                    row["Fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["Area"] = (this.read["embtracli"].ToString().Trim() == "0") ? "SIN ESPECIFICAR" :
                          (this.read["embtracli"].ToString().Trim() == "1") ? "EMBARQUES" :
                          (this.read["embtracli"].ToString().Trim() == "2") ? "TRANSPORTISTA" : "CLIENTE";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT que_nombre FROM tb_det_queja_serv WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                string str = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        str = str + this.read["que_nombre"].ToString().Trim() + ", ";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                row["Tipo_Problema"] = (object)str.Trim().TrimEnd(',');
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable combo_transportistas()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_transportista", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = " SELECT que_transportista FROM tb_mstr_quejas_serv ORDER BY A.que_transportista";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable combo_problemas(DataTable dt)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_nombre", typeof(string));
            this.Abrir();
            foreach (DataRow row1 in (InternalDataCollectionBase)dt.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = " SELECT que_nombre FROM tb_det_queja_serv ORDER BY que_nombre";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        DataRow row2 = dataTable.NewRow();
                        row2["que_nombre"] = (object)this.read["que_nombre"].ToString().Trim();
                        dataTable.Rows.Add(row2);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable combo_insectos_fumig()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ins_folio", typeof(string));
            dataTable.Columns.Add("ins_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = " SELECT ins_folio, ins_nombre FROM tb_cat_insectos_quejas ORDER BY ins_nombre";
            this.read = this.com.ExecuteReader();
            DataRow row1 = dataTable.NewRow();
            row1["ins_folio"] = (object)"";
            row1["ins_nombre"] = (object)"SELECCIONAR INSECTO...";
            dataTable.Rows.Add(row1);
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["ins_folio"] = (object)this.read["ins_folio"].ToString().Trim();
                    row2["ins_nombre"] = (object)this.read["ins_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row2);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable rancho_tabla(string producto, string pedido, string descripcion)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("producto", typeof(string));
            dataTable1.Columns.Add("tarima", typeof(string));
            dataTable1.Columns.Add("recibo", typeof(string));
            dataTable1.Columns.Add("tipo_rec", typeof(string));
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("producto", typeof(string));
            dataTable2.Columns.Add("descripcion", typeof(string));
            dataTable2.Columns.Add("rancho", typeof(string));
            dataTable2.Columns.Add("tabla", typeof(string));
            dataTable2.Columns.Add("prod", typeof(string));
            dataTable2.Columns.Add("pedi", typeof(string));
            dataTable2.Columns.Add("nomb", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT tarima, recibo, tipo_rec FROM tb_det_embarque WHERE emb_folio = '" + pedido + "' AND prod_clave = '" + producto + "' ORDER BY prod_clave, tarima";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable1.NewRow();
                    row["producto"] = (object)producto;
                    row["tarima"] = (object)this.read["tarima"].ToString().Trim();
                    row["recibo"] = (object)this.read["recibo"].ToString().Trim();
                    row["tipo_rec"] = (object)this.read["tipo_rec"].ToString().Trim();
                    dataTable1.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            DataRow dataRow1;
            foreach (DataRow dataRow2 in dataTable1.Select("tipo_rec = 'PTC'"))
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT B.rch_nombre, D.tbl_nombre FROM tb_mstr_recepcion_pt A, tb_cat_ranchos B, tb_cat_tablas D WHERE (A.prov_clave = B.prov_clave AND A.rch_clave = B.rch_clave) AND (A.prov_clave = D.prov_clave AND A.rch_clave = D.rch_clave AND A.tbl_clave = D.tbl_clave) AND A.rpt_recibo = '" + dataRow2["recibo"].ToString() + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        bool flag = false;
                        DataTable dataTable3 = dataTable2;
                        string filterExpression = "rancho = '" + this.read["rch_nombre"].ToString().Trim() + "' AND tabla = '" + this.read["tbl_nombre"].ToString().Trim() + "'";
                        foreach (DataRow dataRow3 in dataTable3.Select(filterExpression))
                        {
                            dataRow1 = dataRow3;
                            flag = true;
                        }
                        if (!flag)
                        {
                            DataRow row = dataTable2.NewRow();
                            row["producto"] = (object)producto;
                            row["descripcion"] = (object)descripcion;
                            row["prod"] = (object)"";
                            row["pedi"] = (object)dataRow2["recibo"].ToString();
                            row["rancho"] = (object)this.read["rch_nombre"].ToString().Trim();
                            row["tabla"] = (object)this.read["tbl_nombre"].ToString().Trim();
                            dataTable2.Rows.Add(row);
                        }
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            DataTable dataTable4 = new DataTable();
            dataTable4.Columns.Add("recibo", typeof(string));
            foreach (DataRow dataRow2 in dataTable1.Select("tipo_rec = 'PTP'"))
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT recibo FROM tb_det_eti_final WHERE folio = '" + dataRow2["recibo"] + "' AND cve_prod = '" + dataRow2["producto"] + "' AND tarima = '" + dataRow2["tarima"] + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        DataRow row = dataTable4.NewRow();
                        row["recibo"] = (object)this.read["recibo"].ToString().Trim();
                        dataTable4.Rows.Add(row);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                DataTable dataTable3 = new DataTable();
                dataTable3.Columns.Add("recibo", typeof(string));
                dataTable3.Columns.Add("tipo", typeof(string));
                foreach (DataRow row1 in (InternalDataCollectionBase)dataTable4.Rows)
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT rmp_recibo, rmp_tipo FROM tb_det_prod_odp WHERE rmp_recibo = '" + row1["recibo"] + "' and ordp_folio = '" + dataRow2["recibo"] + "'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            DataRow row2 = dataTable3.NewRow();
                            row2["recibo"] = (object)this.read["rmp_recibo"].ToString().Trim();
                            row2["tipo"] = (object)this.read["rmp_tipo"].ToString().Trim();
                            dataTable3.Rows.Add(row2);
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }
                foreach (DataRow dataRow3 in dataTable3.Select("tipo = 'MP'"))
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT B.rch_nombre, D.tbl_nombre, A.prod_clave, E.prod_nombre FROM tb_mstr_recepcion_mp A, tb_cat_ranchos B, tb_cat_tablas D, tb_cat_producto E WHERE (A.prov_clave = B.prov_clave AND A.rch_clave = B.rch_clave) AND (A.prov_clave = D.prov_clave AND A.rch_clave = D.rch_clave AND A.tbl_clave = D.tbl_clave) AND (A.prod_clave = E.prod_clave) AND A.rmp_recibo = '" + dataRow3["recibo"].ToString() + "'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            bool flag = false;
                            DataTable dataTable5 = dataTable2;
                            string filterExpression = "rancho = '" + this.read["rch_nombre"].ToString().Trim() + "' AND tabla = '" + this.read["tbl_nombre"].ToString().Trim() + "'";
                            foreach (DataRow dataRow4 in dataTable5.Select(filterExpression))
                            {
                                dataRow1 = dataRow4;
                                flag = true;
                            }
                            if (!flag)
                            {
                                DataRow row = dataTable2.NewRow();
                                row["producto"] = (object)producto;
                                row["descripcion"] = (object)descripcion;
                                row["rancho"] = (object)this.read["rch_nombre"].ToString().Trim();
                                row["tabla"] = (object)this.read["tbl_nombre"].ToString().Trim();
                                row["prod"] = (object)this.read["prod_clave"].ToString().Trim();
                                row["pedi"] = (object)dataRow3["recibo"].ToString();
                                row["nomb"] = (object)this.read["prod_nombre"].ToString().Trim();
                                dataTable2.Rows.Add(row);
                            }
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }
                DataTable dataTable6 = new DataTable();
                dataTable6.Columns.Add("recibo", typeof(string));
                dataTable6.Columns.Add("producto", typeof(string));
                foreach (DataRow dataRow3 in dataTable3.Select("tipo = 'REM'"))
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT rmp_recibo, prod_clave FROM tb_det_prod_ode WHERE orde_folio = '" + dataRow3["recibo"] + "'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            DataRow row = dataTable6.NewRow();
                            row["recibo"] = (object)this.read["rmp_recibo"].ToString().Trim();
                            row["producto"] = (object)this.read["prod_clave"].ToString().Trim();
                            dataTable6.Rows.Add(row);
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                    DataTable dataTable5 = new DataTable();
                    dataTable5.Columns.Add("recibo", typeof(string));
                    foreach (DataRow row1 in (InternalDataCollectionBase)dataTable6.Rows)
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT DISTINCT(recibo) FROM tb_det_eti_final WHERE folio = '" + row1["recibo"] + "' AND cve_prod = '" + row1["producto"] + "'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                DataRow row2 = dataTable5.NewRow();
                                row2["recibo"] = (object)this.read["recibo"].ToString().Trim();
                                dataTable5.Rows.Add(row2);
                            }
                        }
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();
                        DataTable dataTable7 = new DataTable();
                        dataTable7.Columns.Add("recibo", typeof(string));
                        dataTable7.Columns.Add("tipo", typeof(string));
                        foreach (DataRow row2 in (InternalDataCollectionBase)dataTable5.Rows)
                        {
                            this.com = this.conn.CreateCommand();
                            this.com.CommandText = "SELECT rmp_recibo, rmp_tipo FROM tb_det_prod_odp WHERE rmp_recibo = '" + row2["recibo"] + "' AND ordp_folio = '" + row1["recibo"] + "'";
                            this.read = this.com.ExecuteReader();
                            if (this.read.HasRows)
                            {
                                while (this.read.Read())
                                {
                                    DataRow row3 = dataTable7.NewRow();
                                    row3["recibo"] = (object)this.read["rmp_recibo"].ToString().Trim();
                                    row3["tipo"] = (object)this.read["rmp_tipo"].ToString().Trim();
                                    dataTable7.Rows.Add(row3);
                                }
                            }
                            this.read.Close();
                            this.read.Dispose();
                            this.com.Dispose();
                        }
                        foreach (DataRow dataRow4 in dataTable7.Select("tipo = 'MP'"))
                        {
                            this.com = this.conn.CreateCommand();
                            this.com.CommandText = "SELECT B.rch_nombre, D.tbl_nombre, A.prod_clave, E.prod_nombre FROM tb_mstr_recepcion_mp A, tb_cat_ranchos B, tb_cat_tablas D, tb_cat_producto E WHERE (A.prov_clave = B.prov_clave AND A.rch_clave = B.rch_clave) AND (A.prov_clave = D.prov_clave AND A.rch_clave = D.rch_clave AND A.tbl_clave = D.tbl_clave) AND (A.prod_clave = E.prod_clave) AND A.rmp_recibo = '" + dataRow4["recibo"].ToString() + "'";
                            this.read = this.com.ExecuteReader();
                            if (this.read.HasRows)
                            {
                                while (this.read.Read())
                                {
                                    bool flag = false;
                                    DataTable dataTable8 = dataTable2;
                                    string filterExpression = "rancho = '" + this.read["rch_nombre"].ToString().Trim() + "' AND tabla = '" + this.read["tbl_nombre"].ToString().Trim() + "'";
                                    foreach (DataRow dataRow5 in dataTable8.Select(filterExpression))
                                    {
                                        dataRow1 = dataRow5;
                                        flag = true;
                                    }
                                    if (!flag)
                                    {
                                        DataRow row2 = dataTable2.NewRow();
                                        row2["producto"] = (object)producto;
                                        row2["descripcion"] = (object)descripcion;
                                        row2["rancho"] = (object)this.read["rch_nombre"].ToString().Trim();
                                        row2["tabla"] = (object)this.read["tbl_nombre"].ToString().Trim();
                                        row2["prod"] = (object)this.read["prod_clave"].ToString().Trim();
                                        row2["pedi"] = (object)dataRow4["recibo"].ToString();
                                        row2["nomb"] = (object)this.read["prod_nombre"].ToString().Trim();
                                        dataTable2.Rows.Add(row2);
                                    }
                                }
                            }
                            this.read.Close();
                            this.read.Dispose();
                            this.com.Dispose();
                        }
                    }
                }
            }
            this.Cerrar();
            return dataTable2;
        }

        public string guarda_servicio_fum(
          DataTable dtP,
          string fecha,
          string semana,
          string fecha_emb,
          string tipo,
          string pedido,
          string mes,
          string transportista,
          string destino,
          string caja,
          string responsable,
          string aduana,
          string hold,
          string released,
          string fumi_trans,
          DataTable dtI,
          string nomR,
          string costo)
        {
            string str1 = "";
            string cuerpo = "";
            this.Abrir();
            string str2;
            try
            {
                this.com = this.conn.CreateCommand();
                cuerpo = "INSERT INTO tb_mstr_quejas_serv(que_fecha, que_semana, que_fechaemb, que_tipo, que_pedido, que_mes, que_transportista, que_destino, que_caja, que_responsable, que_aduana, que_hold, que_released, que_fumi_trans, que_pesosxdlls) VALUES('" + fecha + "', '" + semana + "', '" + fecha_emb + "', '" + tipo + "', '" + pedido + "', '" + mes + "', '" + transportista + "', '" + destino + "', '" + caja + "', '" + responsable + "', '" + aduana + "', '" + hold + "', '" + released + "', '" + fumi_trans + "', '" + costo + "') SELECT SCOPE_IDENTITY()";
                this.com.CommandText = cuerpo;
                str1 = Convert.ToString(this.com.ExecuteScalar());
                this.com.Dispose();
                str2 = str1;
            }
            catch (SqlException ex)
            {
                this.enviarcorreo_error(nomR, "NUEVA QUEJA DE FUMIGACION", cuerpo);
                str2 = "0";
            }
            if (str2 != "0")
            {
                try
                {
                    foreach (DataRow row in (InternalDataCollectionBase)dtP.Rows)
                    {
                        this.com = this.conn.CreateCommand();
                        string p_r_o_d = row["descripcion"].ToString().Replace("'", "");
                        cuerpo = "INSERT INTO tb_det_quejas_serv(que_folio, prod_clave, prod_nombre, p_clave, p_nombre, recibo, rancho, tabla)VALUES('" + str1 + "', '" + row["producto"].ToString() + "', '" + p_r_o_d + "', '" + row["prod"].ToString() + "', '" + row["nomb"].ToString() + "', '" + row["pedi"].ToString() + "', '" + row["rancho"].ToString() + "', '" + row["tabla"].ToString() + "')";
                        this.com.CommandText = cuerpo;
                        this.com.ExecuteNonQuery();
                        this.com.Dispose();
                    }
                    foreach (DataRow row in (InternalDataCollectionBase)dtI.Rows)
                    {
                        this.com = this.conn.CreateCommand();
                        cuerpo = "INSERT INTO tb_det_quejas_insec(ins_folio, que_folio, ins_nombre, ins_nota)VALUES('" + row["ins_folio"].ToString() + "','" + str1 + "', '" + row["ins_nombre"].ToString() + "', '" + row["ins_nota"].ToString() + "')";
                        this.com.CommandText = cuerpo;
                        this.com.ExecuteNonQuery();
                        this.com.Dispose();
                    }
                    str2 = str1 + ".2";
                }
                catch (SqlException ex)
                {
                    this.enviarcorreo_error(nomR, "ERROR EN INSERCION DE DETALLE FOLIO: " + str1, cuerpo);
                    str2 = "0";
                }
            }
            this.Cerrar();
            return str2;
        }

        public DataTable quejas_fumigacion(string resp, string admin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(int));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("que_aduana", typeof(string));
            dataTable.Columns.Add("que_transporte", typeof(string));
            dataTable.Columns.Add("que_caja", typeof(string));
            dataTable.Columns.Add("que_hold", typeof(string));
            dataTable.Columns.Add("que_released", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("producto", typeof(string));
            dataTable.Columns.Add("insecto", typeof(string));
            dataTable.Columns.Add("costo", typeof(string));
            dataTable.Columns.Add("accion", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (admin == "ADMINISTRADOR")
                this.com.CommandText = " SELECT A.que_folio, A.que_fecha, A.que_pedido, A.que_aduana, A.que_transportista, A.que_destino, A.que_caja, A.que_hold, A.que_released, A.que_tipo, A.que_pesosxdlls, accion FROM tb_mstr_quejas_serv A WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'F' ORDER BY A.que_folio DESC";
            else
                this.com.CommandText = " SELECT A.que_folio, A.que_fecha, A.que_pedido, A.que_aduana, A.que_transportista, A.que_destino, A.que_caja, A.que_hold, A.que_released, A.que_tipo, A.que_pesosxdlls, accion FROM tb_mstr_quejas_serv A WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'F' AND A.que_responsable = '" + resp + "' ORDER BY A.que_folio DESC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)Convert.ToInt32(this.read["que_folio"].ToString().Trim());
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["que_aduana"] = (object)this.read["que_aduana"].ToString().Trim();
                    row["que_transporte"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["que_caja"] = (object)this.read["que_caja"].ToString().Trim();
                    row["que_hold"] = this.read["que_hold"].ToString().Trim() == "" ? (object)"" : (object)Convert.ToDateTime(this.read["que_hold"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_released"] = this.read["que_released"].ToString().Trim() == "" ? (object)"" : (object)Convert.ToDateTime(this.read["que_released"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["costo"] = (object)this.read["que_pesosxdlls"].ToString().Trim();
                    row["accion"] = (object)this.read["accion"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(prod_nombre) FROM tb_det_quejas_serv WHERE que_folio = '" + row["que_folio"].ToString() + "' ORDER BY prod_nombre";
                this.read = this.com.ExecuteReader();
                string str1 = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        str1 = str1 + this.read["prod_nombre"].ToString().Trim() + ", ";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                string str2 = str1.TrimEnd(' ').TrimEnd(',');
                row["producto"] = (object)str2;
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT ins_nombre FROM tb_det_quejas_insec WHERE que_folio = '" + row["que_folio"].ToString() + "' ORDER BY ins_nombre";
                this.read = this.com.ExecuteReader();
                string str3 = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        str3 = str3 + this.read["ins_nombre"].ToString().Trim() + ", ";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                string str4 = str3.TrimEnd(' ').TrimEnd(',');
                row["insecto"] = (object)str4;
            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable quejas_fumigacion_cmbInsectos(string resp, string admin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("insecto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (admin == "ADMINISTRADOR")
                this.com.CommandText = "SELECT DISTINCT(B.ins_nombre) FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_quejas_insec B ON A.que_folio = B.que_folio WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'F' ORDER BY B.ins_nombre DESC";
            else
                this.com.CommandText = "SELECT DISTINCT(B.ins_nombre) FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_quejas_insec B ON A.que_folio = B.que_folio WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'F' AND A.que_responsable = '" + resp + "' ORDER BY B.ins_nombre DESC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["insecto"] = (object)this.read["ins_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable quejas_fumigacion_cmbProducto(string resp, string admin)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("producto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (admin == "ADMINISTRADOR")
                this.com.CommandText = "SELECT DISTINCT(B.prod_nombre) FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_quejas_serv B ON A.que_folio = B.que_folio WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'F' ORDER BY B.prod_nombre DESC";
            else
                this.com.CommandText = "SELECT DISTINCT(B.prod_nombre) FROM tb_mstr_quejas_serv A LEFT JOIN tb_det_quejas_serv B ON A.que_folio = B.que_folio WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'F' AND A.que_responsable = '" + resp + "' ORDER BY B.prod_nombre DESC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["producto"] = (object)this.read["prod_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                ;
            this.Cerrar();
            return dataTable;
        }

        public DataSet datos_queja_fumigacion(string queja, string resp)
        {
            DataSet dataSet = new DataSet("Queja");
            DataTable dataTable1 = dataSet.Tables.Add("Maestro");
            dataTable1.Columns.Add("que_fecha", typeof(string));
            dataTable1.Columns.Add("que_semana", typeof(string));
            dataTable1.Columns.Add("que_fechaemb", typeof(string));
            dataTable1.Columns.Add("que_tipo", typeof(string));
            dataTable1.Columns.Add("que_pedido", typeof(string));
            dataTable1.Columns.Add("que_mes", typeof(string));
            dataTable1.Columns.Add("que_transportista", typeof(string));
            dataTable1.Columns.Add("que_destino", typeof(string));
            dataTable1.Columns.Add("que_caja", typeof(string));
            dataTable1.Columns.Add("que_aduana", typeof(string));
            dataTable1.Columns.Add("que_hold", typeof(string));
            dataTable1.Columns.Add("que_released", typeof(string));
            dataTable1.Columns.Add("que_responsable", typeof(string));
            dataTable1.Columns.Add("descripcion", typeof(string));
            dataTable1.Columns.Add("costo", typeof(string));
            DataTable dataTable2 = dataSet.Tables.Add("Productos");
            dataTable2.Columns.Add("producto", typeof(string));
            dataTable2.Columns.Add("descripcion", typeof(string));
            dataTable2.Columns.Add("prod", typeof(string));
            dataTable2.Columns.Add("nomb", typeof(string));
            dataTable2.Columns.Add("pedi", typeof(string));
            dataTable2.Columns.Add("rancho", typeof(string));
            dataTable2.Columns.Add("tabla", typeof(string));
            DataTable dataTable3 = dataSet.Tables.Add("Insectos");
            dataTable3.Columns.Add("ins_folio", typeof(string));
            dataTable3.Columns.Add("ins_nombre", typeof(string));
            dataTable3.Columns.Add("ins_nota", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_fecha, A.que_semana, A.que_fechaemb, A.que_tipo, A.que_pedido, A.que_mes, A.que_transportista, A.que_destino, A.que_caja, A.que_aduana, A.que_hold, A.que_released, B.resp_nombre, B.resp_paterno, A.descripcion, A.que_pesosxdlls FROM tb_mstr_quejas_serv A LEFT JOIN tb_cat_responsables B ON A.que_responsable = B.resp_clave WHERE que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable1.NewRow();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_semana"] = (object)this.read["que_semana"].ToString().Trim();
                    row["que_fechaemb"] = (object)Convert.ToDateTime(this.read["que_fechaemb"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["que_mes"] = (object)this.read["que_mes"].ToString().Trim();
                    row["que_transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["que_destino"] = (object)this.read["que_destino"].ToString().Trim();
                    row["que_caja"] = (object)this.read["que_caja"].ToString().Trim();
                    row["que_aduana"] = (object)this.read["que_aduana"].ToString().Trim();
                    row["que_hold"] = (object)this.read["que_hold"].ToString().Trim();
                    row["que_released"] = (object)this.read["que_released"].ToString().Trim();
                    row["que_responsable"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["descripcion"] = (object)this.read["descripcion"].ToString().Trim();
                    row["costo"] = (object)this.read["que_pesosxdlls"].ToString().Trim();
                    dataTable1.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT prod_clave, prod_nombre, p_clave, p_nombre, recibo, rancho, tabla FROM tb_det_quejas_serv WHERE que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable2.NewRow();
                    row["producto"] = (object)this.read["prod_clave"].ToString().Trim();
                    row["descripcion"] = (object)this.read["prod_nombre"].ToString().Trim();
                    row["prod"] = (object)this.read["p_clave"].ToString().Trim();
                    row["nomb"] = (object)this.read["p_nombre"].ToString().Trim();
                    row["pedi"] = (object)this.read["recibo"].ToString().Trim();
                    row["rancho"] = (object)this.read["rancho"].ToString().Trim();
                    row["tabla"] = (object)this.read["tabla"].ToString().Trim();
                    dataTable2.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT ins_folio, ins_nombre, ins_nota FROM tb_det_quejas_insec WHERE que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable3.NewRow();
                    row["ins_folio"] = (object)this.read["ins_folio"].ToString().Trim();
                    row["ins_nombre"] = (object)this.read["ins_nombre"].ToString().Trim();
                    row["ins_nota"] = (object)this.read["ins_nota"].ToString().Trim();
                    dataTable3.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataSet;
        }

        public string modifica_fertilizante(string queja, string hold, string released, string costo)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas_serv  SET que_hold = '" + hold + "', que_released = '" + released + "', que_pesosxdlls = '" + costo + "' WHERE que_folio = '" + queja + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public DataTable quejas_fumigacion_excel(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("Fecha", typeof(string));
            dataTable.Columns.Add("Dia", typeof(string));
            dataTable.Columns.Add("Semana", typeof(string));
            dataTable.Columns.Add("Factura", typeof(string));
            dataTable.Columns.Add("Materia Prima", typeof(string));
            dataTable.Columns.Add("Producto", typeof(string));
            dataTable.Columns.Add("Aduana", typeof(string));
            dataTable.Columns.Add("Transportista", typeof(string));
            dataTable.Columns.Add("Caja", typeof(string));
            dataTable.Columns.Add("Insecto", typeof(string));
            dataTable.Columns.Add("FDA/USDA Hold", typeof(string));
            dataTable.Columns.Add("FDA/USDA Released", typeof(string));
            dataTable.Columns.Add("Rancho", typeof(string));
            dataTable.Columns.Add("Tabla", typeof(string));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("Costo", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = " SELECT A.que_folio, A.que_fechaemb, A.que_semana, A.que_pedido, A.que_aduana, A.que_transportista, A.que_caja, A.que_hold, A.que_released, A.que_tipo, A.que_pesosxdlls FROM tb_mstr_quejas_serv A WHERE A.que_estatus = 'A' AND A.que_fumi_trans = 'F' AND A.que_folio = '" + folio + "' ORDER BY A.que_folio DESC";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["Fecha"] = (object)Convert.ToDateTime(this.read["que_fechaemb"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["Semana"] = (object)this.read["que_semana"].ToString().Trim();
                    row["Factura"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["Aduana"] = (object)this.read["que_aduana"].ToString().Trim();
                    row["Transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["Caja"] = (object)this.read["que_caja"].ToString().Trim();
                    row["FDA/USDA Hold"] = this.read["que_hold"].ToString().Trim() == "" ? (object)"" : (object)Convert.ToDateTime(this.read["que_hold"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["FDA/USDA Released"] = this.read["que_released"].ToString().Trim() == "" ? (object)"" : (object)Convert.ToDateTime(this.read["que_released"].ToString().Trim()).ToString("dd/MM/yyyy");
                    row["Tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["Costo"] = (object)this.read["que_pesosxdlls"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(prod_nombre) FROM tb_det_quejas_serv WHERE que_folio = '" + row["que_folio"].ToString() + "' ORDER BY prod_nombre";
                this.read = this.com.ExecuteReader();
                string str1 = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        str1 = str1 + this.read["prod_nombre"].ToString().Trim() + ", ";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                string str2 = str1.TrimEnd(' ').TrimEnd(',');
                row["Producto"] = (object)str2;
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(p_nombre) FROM tb_det_quejas_serv WHERE que_folio = '" + row["que_folio"].ToString() + "' ORDER BY p_nombre";
                this.read = this.com.ExecuteReader();
                string str3 = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        if (this.read["p_nombre"].ToString().Trim() != "")
                            str3 = str3 + this.read["p_nombre"].ToString().Trim() + ", ";
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                string str4 = str3.TrimEnd(' ').TrimEnd(',');
                row["Materia Prima"] = (object)str4;
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(rancho) FROM tb_det_quejas_serv WHERE que_folio = '" + row["que_folio"].ToString() + "' ORDER BY rancho";
                this.read = this.com.ExecuteReader();
                string str5 = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        str5 = str5 + this.read["rancho"].ToString().Trim() + ", ";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                string str6 = str5.TrimEnd(' ').TrimEnd(',');
                row["Rancho"] = (object)str6;
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT DISTINCT(tabla) FROM tb_det_quejas_serv WHERE que_folio = '" + row["que_folio"].ToString() + "' ORDER BY tabla";
                this.read = this.com.ExecuteReader();
                string str7 = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        str7 = str7 + this.read["tabla"].ToString().Trim() + ", ";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                string str8 = str7.TrimEnd(' ').TrimEnd(',');
                row["Tabla"] = (object)str8;
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT ins_nombre FROM tb_det_quejas_insec WHERE que_folio = '" + row["que_folio"].ToString() + "' ORDER BY ins_nombre";
                this.read = this.com.ExecuteReader();
                string str9 = "";
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                        str9 = str9 + this.read["ins_nombre"].ToString().Trim() + ", ";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                string str10 = str9.TrimEnd(' ').TrimEnd(',');
                row["Insecto"] = (object)str10;
            }
            this.Cerrar();
            return dataTable;
        }

        //comboplacas_serv
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

        public void correo_error(string cuerpo)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Error";
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

        public bool valida_queja_pedido(string queja)
        {
            string str = "";
            this.Abrir();
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT que_transporte FROM tb_mstr_quejas WHERE que_folio = '" + queja + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["que_transporte"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            catch (Exception ex)
            {
                return false;
            }
            this.Cerrar();
            return !(str == "") && str != null;
        }

        public DataTable accion_correctiva_fumigacion(string queja)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("fecha", typeof(string));
            dataTable.Columns.Add("cedis", typeof(string));
            dataTable.Columns.Add("realizado", typeof(string));
            dataTable.Columns.Add("transportista", typeof(string));
            dataTable.Columns.Add("caja", typeof(string));
            dataTable.Columns.Add("pedido", typeof(string));
            dataTable.Columns.Add("tipo", typeof(string));
            dataTable.Columns.Add("accion", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_fecha, A.que_tipo, A.que_pedido, A.que_transportista, A.que_caja, B.resp_nombre, B.resp_paterno, B.resp_cedis, A.descripcion FROM tb_mstr_quejas_serv A JOIN tb_cat_responsables B ON A.que_responsable = B.resp_clave WHERE A.que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"]).ToString("dd/MM/yyyy");
                    row["cedis"] = (object)this.read["resp_cedis"].ToString().Trim();
                    row["realizado"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["caja"] = (object)this.read["que_caja"].ToString().Trim();
                    row["pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["accion"] = (object)this.read["descripcion"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable accion_correctiva_fumigacion_insectos(string queja)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("insecto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT ins_nombre FROM tb_det_quejas_insec WHERE que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["insecto"] = (object)this.read["ins_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string accion_correctiva_ingreso(string folio, string comentario)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas_serv  SET descripcion = '" + comentario.ToUpper() + "', accion = '1' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public int validar_accion_correctiva(string folio)
        {
            int num = 0;
            this.Abrir();
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT accion FROM tb_mstr_quejas_serv WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    num = Convert.ToInt32(this.read["accion"].ToString().Trim());
                }
                this.com.Dispose();
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                num = 0;
            }
            this.Cerrar();
            return num;
        }

        public string busca_cliente_pedido(string folio, string tipo, string cedis, string clave)
        {
            //buscar en facturas
            string cnte = "";
            Abrir();

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

                    //com = conn.CreateCommand();
                    //com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = 'EXP' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //read = com.ExecuteReader();
                    //bool fnd = false;
                    //if (read.HasRows)
                    //{
                    //    read.Read();
                    //    cnte = read["cnte_clave"].ToString().Trim();
                    //    fnd = true;
                    //}
                    //read.Close();
                    //read.Dispose();
                    //com.Dispose();

                    //if (fnd == false)
                    //{
                    //    com = conn.CreateCommand();
                    //    com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + folio + "' AND fcn_lugar = 'EXP' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //    read = com.ExecuteReader();
                    //    if (read.HasRows)
                    //    {
                    //        read.Read();
                    //        cnte = read["cnte_clave"].ToString().Trim();
                    //    }
                    //    read.Close();
                    //    read.Dispose();
                    //    com.Dispose();
                    //}

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
                    //com = conn.CreateCommand();
                    //if (cedis == "COMERCIALIZADORA GAB")
                    //{
                    //    bool fnd = false;
                    //    com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = 'FE' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //    read = com.ExecuteReader();
                    //    if (read.HasRows)
                    //    {
                    //        read.Read();
                    //        cnte = read["cnte_clave"].ToString().Trim();
                    //    }
                    //    read.Close();
                    //    read.Dispose();
                    //    com.Dispose();

                    //    if (fnd == false)
                    //    {
                    //        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + folio + "' AND fcn_lugar = 'FE' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //        read = com.ExecuteReader();
                    //        if (read.HasRows)
                    //        {
                    //            read.Read();
                    //            cnte = read["cnte_clave"].ToString().Trim();
                    //        }
                    //        read.Close();
                    //        read.Dispose();
                    //        com.Dispose();
                    //    }
                    //}
                    //if (cedis == "CANCUN")
                    //{
                    //    bool fnd = false;
                    //    com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = 'FC' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //    read = com.ExecuteReader();
                    //    if (read.HasRows)
                    //    {
                    //        read.Read();
                    //        cnte = read["cnte_clave"].ToString().Trim();
                    //    }
                    //    read.Close();
                    //    read.Dispose();
                    //    com.Dispose();

                    //    if (fnd == false)
                    //    {
                    //        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + folio + "' AND fcn_lugar = 'FC' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //        read = com.ExecuteReader();
                    //        if (read.HasRows)
                    //        {
                    //            read.Read();
                    //            cnte = read["cnte_clave"].ToString().Trim();
                    //        }
                    //        read.Close();
                    //        read.Dispose();
                    //        com.Dispose();
                    //    }
                    //}
                    //if (cedis == "ABASTOS")
                    //{
                    //    bool fnd = false;
                    //    com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = 'FD' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //    read = com.ExecuteReader();
                    //    if (read.HasRows)
                    //    {
                    //        read.Read();
                    //        cnte = read["cnte_clave"].ToString().Trim();
                    //    }
                    //    read.Close();
                    //    read.Dispose();
                    //    com.Dispose();

                    //    if (fnd == false)
                    //    {
                    //        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + folio + "' AND fcn_lugar = 'FD' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //        read = com.ExecuteReader();
                    //        if (read.HasRows)
                    //        {
                    //            read.Read();
                    //            cnte = read["cnte_clave"].ToString().Trim();
                    //        }
                    //        read.Close();
                    //        read.Dispose();
                    //        com.Dispose();
                    //    }
                    //}
                    //if (cedis == "GUADALAJARA")
                    //{
                    //    bool fnd = false;
                    //    com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = 'FG' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //    read = com.ExecuteReader();
                    //    if (read.HasRows)
                    //    {
                    //        read.Read();
                    //        cnte = read["cnte_clave"].ToString().Trim();
                    //    }
                    //    read.Close();
                    //    read.Dispose();
                    //    com.Dispose();

                    //    if (fnd == false)
                    //    {
                    //        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + folio + "' AND fcn_lugar = 'FG' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //        read = com.ExecuteReader();
                    //        if (read.HasRows)
                    //        {
                    //            read.Read();
                    //            cnte = read["cnte_clave"].ToString().Trim();
                    //        }
                    //        read.Close();
                    //        read.Dispose();
                    //        com.Dispose();
                    //    }
                    //}
                    //if (cedis == "SMO")
                    //{
                    //    bool fnd = false;
                    //    com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_lugar = 'FM' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //    read = com.ExecuteReader();
                    //    if (read.HasRows)
                    //    {
                    //        read.Read();
                    //        cnte = read["cnte_clave"].ToString().Trim();
                    //    }
                    //    read.Close();
                    //    read.Dispose();
                    //    com.Dispose();

                    //    if (fnd == false)
                    //    {
                    //        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + folio + "' AND fcn_lugar = 'FM' AND fcn_fecha >='" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //        read = com.ExecuteReader();
                    //        if (read.HasRows)
                    //        {
                    //            read.Read();
                    //            cnte = read["cnte_clave"].ToString().Trim();
                    //        }
                    //        read.Close();
                    //        read.Dispose();
                    //        com.Dispose();
                    //    }
                    //}
                    //if (cedis == "AGUILARES" || cedis == "GABINC" || cedis == "INVERNADEROS" || cedis == "MINITA")
                    //{
                    //    bool fnd = false;
                    //    com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' AND fcn_fecha >= '" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //    read = com.ExecuteReader();
                    //    if (read.HasRows)
                    //    {
                    //        read.Read();
                    //        cnte = read["cnte_clave"].ToString().Trim();
                    //    }
                    //    read.Close();
                    //    read.Dispose();
                    //    com.Dispose();

                    //    if (fnd == false)
                    //    {
                    //        com.CommandText = "SELECT cnte_clave FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + folio + "' AND fcn_fecha >= '" + DateTime.Now.AddMonths(-6).ToShortDateString() + "'";
                    //        read = com.ExecuteReader();
                    //        if (read.HasRows)
                    //        {
                    //            read.Read();
                    //            cnte = read["cnte_clave"].ToString().Trim();
                    //        }
                    //        read.Close();
                    //        read.Dispose();
                    //        com.Dispose();
                    //    }
                    //}
                }
            }
            catch (SqlException ex)
            {
                this.Cerrar();
                cnte = "";
            }
            Cerrar();
            return cnte;
        }

        public DataTable carga_datos()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("rejected", typeof(string));
            dataTable.Columns.Add("que_cliente", typeof(string));
            dataTable.Columns.Add("que_recibio", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("qud_pedido", typeof(string));
            dataTable.Columns.Add("resp_tipo", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("vendedor", typeof(string));
            dataTable.Columns.Add("correo", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select A.que_folio, A.id_producto, A.nom_producto, SUM(qud_cantrecha) AS rejected, B.que_cliente, B.que_recibio, C.resp_nombre, C.resp_paterno, A.qud_pedido, C.resp_tipo, " +
                "B.que_tipo " +
                "FROM tb_det_quejas A " +
                "JOIN tb_mstr_quejas B on A.que_folio = B.que_folio " +
                "JOIN tb_cat_responsables C ON B.que_recibio = C.resp_clave " +
                "WHERE B.que_status <> 'C' AND B.que_fecha >= '01/10/2021' AND A.qud_pedido is not null " +
                "GROUP BY A.que_folio, A.id_producto, A.nom_producto, B.que_cliente, B.que_recibio, C.resp_nombre, C.resp_paterno, A.qud_pedido, C.resp_tipo, B.que_tipo " +
                "ORDER BY A.que_folio";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["rejected"] = (object)this.read["rejected"].ToString().Trim();
                    row["que_cliente"] = (object)this.read["que_cliente"].ToString().Trim();
                    row["que_recibio"] = (object)this.read["que_recibio"].ToString().Trim();
                    row["resp_nombre"] = (object)this.read["resp_nombre"].ToString().Trim() + " " + (object)this.read["resp_paterno"].ToString().Trim();
                    row["qud_pedido"] = (object)this.read["qud_pedido"].ToString().Trim();
                    row["resp_tipo"] = (object)this.read["resp_tipo"].ToString().Trim();
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["vendedor"] = "";
                    row["correo"] = "";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            foreach (DataRow rw in dataTable.Rows)
            {
                if (rw["que_tipo"].ToString() == "E")
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT vendedor FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + rw["qud_pedido"].ToString() + "' AND pdn_tipo = 'EXP'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        this.read.Read();
                        rw["vendedor"] = read["vendedor"].ToString().Trim();
                    }
                    else
                    {
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();

                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT fcn_elaboro FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + rw["qud_pedido"].ToString() + "' AND fcn_lugar = 'EXP'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            this.read.Read();
                            rw["vendedor"] = read["fcn_elaboro"].ToString().Trim();
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }
                else
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT vendedor FROM tb_mstr_pedidos_nal WHERE pdn_folio = '" + rw["qud_pedido"].ToString() + "' AND pdn_tipo = 'NAL'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        this.read.Read();
                        rw["vendedor"] = read["vendedor"].ToString().Trim();
                    }
                    else
                    {
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();

                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT fcn_elaboro FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + rw["qud_pedido"].ToString() + "' AND fcn_lugar = '" + rw["resp_tipo"].ToString() + "'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            this.read.Read();
                            rw["vendedor"] = read["fcn_elaboro"].ToString().Trim();
                        }
                    }
                    this.read.Close();
                    this.read.Dispose();
                    this.com.Dispose();
                }


            }
            this.Cerrar();
            return dataTable;
        }

        public DataTable notas_credito_causas(string queja)
        {
            SqlCommand cmnd2 = conn.CreateCommand();
            SqlDataReader reader2;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("inv_folio", typeof(string));
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("inv_responsable", typeof(string));
            dataTable.Columns.Add("resp_nombre", typeof(string));
            dataTable.Columns.Add("inv_comentario", typeof(string));

            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("inv_folio", typeof(string));
            dataTable2.Columns.Add("que_folio", typeof(string));
            dataTable2.Columns.Add("inv_responsable", typeof(string));
            dataTable2.Columns.Add("resp_nombre", typeof(string));
            dataTable2.Columns.Add("inv_comentario", typeof(string));

            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select A.inv_folio, A.que_folio, A.inv_responsable, B.resp_nombre, B.resp_paterno, A.inv_comentario FROM tb_det_investigacion A " +
                "JOIN tb_cat_responsables B ON A.inv_responsable = B.resp_clave " +
                "WHERE A.que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["inv_folio"] = (object)this.read["inv_folio"].ToString().Trim();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["inv_responsable"] = (object)this.read["inv_responsable"].ToString().Trim();
                    row["resp_nombre"] = (object)(this.read["resp_nombre"].ToString().Trim() + " " + this.read["resp_paterno"].ToString().Trim());
                    row["inv_comentario"] = (object)this.read["inv_comentario"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            DataRow ri;
            foreach (DataRow red in dataTable.Rows)
            {
                ri = dataTable2.NewRow();
                ri["inv_folio"] = red["inv_folio"].ToString().Trim();
                ri["que_folio"] = red["que_folio"].ToString().Trim();
                ri["inv_responsable"] = red["inv_responsable"].ToString().Trim();
                ri["resp_nombre"] = red["resp_nombre"].ToString().Trim();
                ri["inv_comentario"] = red["inv_comentario"].ToString().Trim();
                dataTable2.Rows.Add(ri);

                cmnd2 = conn.CreateCommand();
                cmnd2.CommandText = "SELECT acc_causa FROM tb_mstr_acciones WHERE inv_folio = '" + red["inv_folio"].ToString().Trim() + "'";
                reader2 = cmnd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        DataRow row2 = dataTable2.NewRow();
                        row2["inv_folio"] = "";
                        row2["que_folio"] = "";
                        row2["inv_responsable"] = "";
                        row2["resp_nombre"] = "CAUSA:";
                        row2["inv_comentario"] = reader2["acc_causa"].ToString().Trim();
                        dataTable2.Rows.Add(row2);
                    }
                }
                else
                {
                    DataRow row2 = dataTable2.NewRow();
                    row2["inv_folio"] = "";
                    row2["que_folio"] = "";
                    row2["inv_responsable"] = "";
                    row2["resp_nombre"] = "CAUSA:";
                    row2["inv_comentario"] = "SIN CAPTURA";
                    dataTable2.Rows.Add(row2);
                }
                reader2.Close();
                reader2.Dispose();
                cmnd2.Dispose();
            }

            this.Cerrar();
            return dataTable2;
        }

        public DataTable notas_credito_pedido(string queja, string tipo)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("qud_pedido", typeof(string));
            dataTable.Columns.Add("factura", typeof(string));
            dataTable.Columns.Add("pdn_elaboro", typeof(string));
            dataTable.Columns.Add("usu_nombre", typeof(string));
            dataTable.Columns.Add("usu_email", typeof(string));


            this.Abrir();


            this.com = this.conn.CreateCommand();

            bool fnd = false;
            //BUSCAR QUIEN REALIZO CUANDO SE TIENE EL PEDIDO
            if (tipo == "N")
            {
                this.com.CommandText = "select A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email from tb_det_quejas A " +
                "JOIN tb_mstr_pedidos_nal B ON A.qud_pedido = B.pdn_folio " +
                "JOIN tb_cat_usuarios C ON B.pdn_elaboro = C.usu_login " +
                "WHERE A.que_folio = '" + queja + "' " +
                "group by A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email";
                //this.com.CommandText = "select A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email, D.fcn_folio, E.que_sufijo " +
                //    "from tb_det_quejas A " +
                //    "JOIN tb_mstr_pedidos_nal B ON A.qud_pedido = B.pdn_folio " +
                //    "JOIN tb_cat_usuarios C ON B.pdn_elaboro = C.usu_login " +
                //    "JOIN tb_mstr_facturas_nal D ON D.pdn_folio = B.pdn_folio AND B.pdn_serie = D.fcn_tipo AND " +
                //    "JOIN tb_mstr_quejas E ON A.que_folio = E.que_folio " +
                //    "WHERE A.que_folio = '" + queja + "' " +
                //    "group by A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email, D.fcn_folio, E.que_sufijo";
            }
            if (tipo == "E")
            {
                this.com.CommandText = "select A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email from tb_det_quejas A " +
                "JOIN tb_mstr_pedidos_exp B ON A.qud_pedido = B.pdn_folio " +
                "JOIN tb_cat_usuarios C ON B.pdn_elaboro = C.usu_login " +
                "WHERE A.que_folio = '" + queja + "' " +
                "group by A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email";
                //this.com.CommandText = "select A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email, D.fcn_folio, E.que_sufijo " +
                //    "from tb_det_quejas A " +
                //    "JOIN tb_mstr_pedidos_exp B ON A.qud_pedido = B.pdn_folio " +
                //    "JOIN tb_cat_usuarios C ON B.pdn_elaboro = C.usu_login " +
                //    "JOIN tb_mstr_facturas_nal D ON D.pdn_folio = B.pdn_folio AND B.pdn_tipo = D.fcn_tipo AND D.fcn_lugar = 'EXP' " +
                //    "JOIN tb_mstr_quejas E ON A.que_folio = E.que_folio " +
                //    "WHERE A.que_folio = '" + queja + "' " +
                //    "group by A.qud_pedido, B.pdn_elaboro, C.usu_nombre, C.usu_email, D.fcn_folio, E.que_sufijo";
            }
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                fnd = true;
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["qud_pedido"] = (object)this.read["qud_pedido"].ToString().Trim();
                    row["pdn_elaboro"] = (object)this.read["pdn_elaboro"].ToString().Trim();
                    row["usu_nombre"] = (object)this.read["usu_nombre"].ToString().Trim();
                    row["usu_email"] = (object)this.read["usu_email"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();


            if (fnd == false)
            {
                string resp_tipo = "";
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT B.resp_tipo FROM tb_mstr_quejas A JOIN tb_cat_responsables B ON A.que_recibio = B.resp_clave WHERE A.que_folio = '" + queja + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        resp_tipo = this.read["resp_tipo"].ToString().Trim();
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                if (tipo == "N")
                {
                    this.com.CommandText = "select A.qud_pedido, B.fcn_elaboro, C.usu_nombre, C.usu_email from tb_det_quejas A " +
                    "JOIN tb_mstr_facturas_nal B ON A.qud_pedido = B.fcn_folio " +
                    "JOIN tb_cat_usuarios C ON B.fcn_elaboro = C.usu_login " +
                    "WHERE A.que_folio = '" + queja + "' AND B.fcn_lugar = '" + resp_tipo + "' " +
                    "group by A.qud_pedido, B.fcn_elaboro, C.usu_nombre, C.usu_email";
                }
                if (tipo == "E")
                {
                    this.com.CommandText = "select A.qud_pedido, B.fcn_elaboro, C.usu_nombre, C.usu_email from tb_det_quejas A " +
                    "JOIN tb_mstr_facturas_nal B ON A.qud_pedido = B.fcn_folio " +
                    "JOIN tb_cat_usuarios C ON B.fcn_elaboro = C.usu_login " +
                    "WHERE A.que_folio = '" + queja + "' AND B.fcn_lugar = 'EXP' " +
                    "group by A.qud_pedido, B.fcn_elaboro, C.usu_nombre, C.usu_email";
                }
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    fnd = true;
                    while (this.read.Read())
                    {
                        DataRow row = dataTable.NewRow();
                        row["qud_pedido"] = (object)this.read["qud_pedido"].ToString().Trim();
                        row["pdn_elaboro"] = (object)this.read["fcn_elaboro"].ToString().Trim();
                        row["usu_nombre"] = (object)this.read["usu_nombre"].ToString().Trim();
                        row["usu_email"] = (object)this.read["usu_email"].ToString().Trim();
                        dataTable.Rows.Add(row);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }


            this.Cerrar();
            return dataTable;
        }

        public DataTable notas_credito_facturas(string queja)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("facturas", typeof(string));
            dataTable.Columns.Add("sufijo", typeof(string));
            dataTable.Columns.Add("subcliente", typeof(string));
            dataTable.Columns.Add("costo", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();

            this.com.CommandText = "SELECT fcn_folio, que_sufijo, que_sucursal, que_costo FROM tb_mstr_quejas WHERE que_folio = '" + queja + "'";
            this.read = this.com.ExecuteReader();
            List<string> list = new List<string>();
            string fac = "";
            string pre = "";
            string sub = "";
            string cos = "";
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    fac = this.read["fcn_folio"].ToString().Trim();
                    pre = this.read["que_sufijo"].ToString().Trim();
                    sub = this.read["que_sucursal"].ToString().Trim();
                    cos = this.read["que_costo"].ToString().Trim();
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();

            string[] fac2 = fac.Split(',');
            foreach (string var in fac2)
            {
                DataRow row = dataTable.NewRow();
                row["facturas"] = var.Trim();
                row["sufijo"] = pre;
                row["subcliente"] = sub;
                row["costo"] = cos;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public string notas_credito_costo(string queja, string factura)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT costo FROM tb_det_quejas_fact WHERE que_folio = '" + queja + "' AND factura = '" + factura.Trim() + "'";
                this.read = this.com.ExecuteReader();
                while (this.read.Read())
                {
                    str = this.read["costo"].ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string notas_credito_tipo_queja(string queja)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT que_tipo FROM tb_mstr_quejas WHERE que_folio = '" + queja + "'";
                this.read = this.com.ExecuteReader();
                while (this.read.Read())
                {
                    str = this.read["que_tipo"].ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public DataTable notas_credito_productos(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            dataTable.Columns.Add("qud_cantrecha", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            dataTable.Columns.Add("qud_cantreci", typeof(string));
            dataTable.Columns.Add("pro_nombre", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("DEVOLUCION", typeof(string));
            dataTable.Columns.Add("BONIFICACION", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.id_producto, A.qud_ordprod, SUM(A.qud_cantrecha) AS qud_cantreci, SUM(A.qud_cantrecha) AS qud_cantrecha, A.nom_producto, B.pro_nombre, C.que_tipo, " +
                "A.qud_devolucion, A.qud_bonificacion " +
                "FROM tb_det_quejas A " +
                "JOIN tb_cat_problemas B ON A.qud_problema = B.pro_clave " +
                "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                "WHERE A.que_folio = '" + folio + "' " +
                "GROUP BY A.id_producto, A.qud_ordprod, A.nom_producto, B.pro_nombre, C.que_tipo, A.qud_devolucion, A.qud_bonificacion";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    row["qud_cantrecha"] = (object)this.read["qud_cantrecha"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    row["qud_cantreci"] = (object)this.read["qud_cantreci"].ToString().Trim();
                    row["pro_nombre"] = (object)this.read["pro_nombre"].ToString().Trim();
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["DEVOLUCION"] = (object)this.read["qud_devolucion"].ToString().Trim();
                    row["BONIFICACION"] = (object)this.read["qud_bonificacion"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string notas_credito_queja(string folio, string aut_nc)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas SET que_notacredito = '1', que_autnc = '" + aut_nc + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string notas_credito_queja_NO_PROCEDE(string folio, string aut_nc)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_mstr_quejas SET que_notacredito = '2', que_autnc = '" + aut_nc + "' WHERE que_folio = '" + folio + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "2";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string notas_credito_queja_NC_O_NO_PROCEDE(string folio)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT que_notacredito FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["que_notacredito"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string notas_credito_deshabilitar_consumidor(string folio)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT que_consumidor FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["que_consumidor"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                //str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();

            return str;
        }

        public string notas_credito_devolucion_bonificacion(string folio)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT top 1 qud_devolucion, qud_bonificacion, qud_merma, qud_noaplica FROM tb_det_quejas WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = (this.read["qud_devolucion"].ToString().Trim() == "1") ? "DEV-" + this.read["qud_devolucion"].ToString().Trim() :
                        (this.read["qud_bonificacion"].ToString().Trim() == "1") ? "BON-" + this.read["qud_bonificacion"].ToString().Trim() :
                        (this.read["qud_merma"].ToString().Trim() == "1") ? "MER-" + this.read["qud_merma"].ToString().Trim() : "NA-";
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                //str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();

            return str;
        }

        public string notas_credito_asignacion(string folio)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT que_notacredito FROM tb_mstr_quejas WHERE que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["que_notacredito"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                //str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();

            return str;
        }

        public string correo_quien_hizo_queja(string folio)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT A.resp_correo FROM tb_cat_responsables A JOIN tb_mstr_quejas B ON A.resp_clave = B.que_recibio WHERE B.que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["resp_correo"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                //str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();

            return str;
        }

        public string verifica_investigador_en_mstr_acciones(string folio, string inv_folio)
        {
            this.Abrir();
            string str = "0";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT inv_folio FROM tb_mstr_acciones WHERE que_folio = '" + folio + "' AND inv_folio = '" + inv_folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["inv_folio"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                //str = "1";
            }
            catch (SqlException ex)
            {
                str = "X";
            }
            this.Cerrar();

            return str;
        }

        public string verifica_acciones_en_det_acciones(string folio, string acc_folio)
        {
            this.Abrir();
            string str = "";
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT acc_cumpli_ver FROM tb_det_acciones WHERE que_folio = '" + folio + "' AND acc_clave = '" + acc_folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    str = this.read["acc_cumpli_ver"].ToString().Trim();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                //str = "1";
            }
            catch (SqlException ex)
            {
                str = "X";
            }
            this.Cerrar();

            return str;
        }

        //graficas por porducto comparativo anual
        public DataTable productos_graficas(Int32 anio)
        {
            string f1_ant = "01/01/" + (anio - 1).ToString();
            string f2_act = "31/12/" + anio.ToString();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("nom_producto", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto " +
                "FROM tb_det_quejas A " +
                "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_ant + "' AND C.que_fechaemb <= '" + f2_act + "' and C.que_status <> 'C' " +
                "GROUP BY A.id_producto, A.nom_producto ORDER BY A.nom_producto";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["nom_producto"] = (object)this.read["nom_producto"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable productos_comparativo(Int32 anio, string prod, DataTable dtMeses, string nom, string problema)
        {
            string rechazado_act = "0";
            string rechazado_ant = "0";
            string producto = "";
            string clave = "";

            DataTable dtProductos = new DataTable();
            dtProductos.Columns.Add("clave", typeof(string));
            dtProductos.Columns.Add("producto", typeof(string));
            dtProductos.Columns.Add((anio - 1).ToString(), typeof(string));
            dtProductos.Columns.Add(anio.ToString(), typeof(string));

            if (dtMeses.Rows.Count == 0)
            {
                #region anual
                string f1_act = "01/01/" + anio.ToString();
                string f2_act = "31/12/" + anio.ToString();

                string f1_ant = "01/01/" + (anio - 1).ToString();
                string f2_ant = "31/12/" + (anio - 1).ToString();

                DataSet set = new DataSet();
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                this.Abrir();
                this.com = this.conn.CreateCommand();
                if (problema == "")
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_ant + "' AND C.que_fechaemb <= '" + f2_ant + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "GROUP BY A.id_producto, A.nom_producto";
                }
                else
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_ant + "' AND C.que_fechaemb <= '" + f2_ant + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "AND A.qud_problema = '" + problema + "' GROUP BY A.id_producto, A.nom_producto";
                }

                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    rechazado_ant = this.read["rechazadas"].ToString();
                    producto = this.read["nom_producto"].ToString();
                    clave = this.read["id_producto"].ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                this.com = this.conn.CreateCommand();
                if (problema == "")
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_act + "' AND C.que_fechaemb <= '" + f2_act + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "GROUP BY A.id_producto, A.nom_producto";
                }
                else
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_act + "' AND C.que_fechaemb <= '" + f2_act + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "AND A.qud_problema = '" + problema + "' GROUP BY A.id_producto, A.nom_producto";
                }

                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    rechazado_act = this.read["rechazadas"].ToString();
                    producto = this.read["nom_producto"].ToString();
                    clave = this.read["id_producto"].ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.Cerrar();

                if (rechazado_ant == "0" && rechazado_act == "0")
                    dtProductos.Rows.Add(prod, nom, "0", "0");
                else
                    dtProductos.Rows.Add(clave, producto, rechazado_ant, rechazado_act);

                #endregion
            }
            else
            {
                #region meses

                string mes_inicial = dtMeses.Rows[0]["mes"].ToString().PadLeft(2, '0');
                string mes_final = dtMeses.Rows[dtMeses.Rows.Count - 1]["mes"].ToString().PadLeft(2, '0');

                //dias mes del año actual
                int dias_mes_i_act = DateTime.DaysInMonth(anio, Convert.ToInt32(dtMeses.Rows[0]["mes"].ToString()));
                int dias_mes_f_act = DateTime.DaysInMonth(anio, Convert.ToInt32(dtMeses.Rows[dtMeses.Rows.Count - 1]["mes"].ToString()));

                //dias mes del año anterior
                int dias_mes_i_ant = DateTime.DaysInMonth((anio - 1), Convert.ToInt32(dtMeses.Rows[0]["mes"].ToString()));
                int dias_mes_f_ant = DateTime.DaysInMonth((anio - 1), Convert.ToInt32(dtMeses.Rows[dtMeses.Rows.Count - 1]["mes"].ToString()));

                string f1_act = "01/" + mes_inicial + "/" + anio.ToString();
                string f2_act = dias_mes_f_act.ToString() + "/" + mes_final + "/" + anio.ToString();

                string f1_ant = "01/" + mes_inicial + "/" + (anio - 1).ToString();
                string f2_ant = dias_mes_f_ant.ToString() + "/" + mes_final + "/" + (anio - 1).ToString();

                DataSet set = new DataSet();
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                this.Abrir();
                this.com = this.conn.CreateCommand();
                if (problema == "")
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_ant + "' AND C.que_fechaemb <= '" + f2_ant + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "GROUP BY A.id_producto, A.nom_producto";
                }
                else
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_ant + "' AND C.que_fechaemb <= '" + f2_ant + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "AND A.qud_problema = '" + problema + "' GROUP BY A.id_producto, A.nom_producto";
                }

                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    rechazado_ant = this.read["rechazadas"].ToString();
                    producto = this.read["nom_producto"].ToString();
                    clave = this.read["id_producto"].ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                this.com = this.conn.CreateCommand();
                if (problema == "")
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_act + "' AND C.que_fechaemb <= '" + f2_act + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "GROUP BY A.id_producto, A.nom_producto";
                }
                else
                {
                    this.com.CommandText = "SELECT RTRIM(A.id_producto) AS id_producto, RTRIM(A.nom_producto) AS nom_producto, SUM(A.qud_cantrecha) AS rechazadas " +
                    "FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas C ON A.que_folio = C.que_folio " +
                    "WHERE A.id_producto <> '' AND C.que_fechaemb >= '" + f1_act + "' AND C.que_fechaemb <= '" + f2_act + "' AND A.id_producto = '" + prod + "' and C.que_status <> 'C' " +
                    "AND A.qud_problema = '" + problema + "' GROUP BY A.id_producto, A.nom_producto";
                }

                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    this.read.Read();
                    rechazado_act = this.read["rechazadas"].ToString();
                    producto = this.read["nom_producto"].ToString();
                    clave = this.read["id_producto"].ToString();
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();
                this.Cerrar();

                if (rechazado_ant == "0" && rechazado_act == "0")
                    dtProductos.Rows.Add(prod, nom, "0", "0");
                else
                    dtProductos.Rows.Add(clave, producto, rechazado_ant, rechazado_act);

                #endregion
            }


            return dtProductos;
        }

        //ASIGNACION DE MATERIA PRIMA
        public DataTable productos_MP(string clave_prod, string ord_prod)
        {

            DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("estado", typeof(string));
            dataTable.Columns.Add("compp_clave", typeof(string));
            dataTable.Columns.Add("prod_nombre", typeof(string));
            dataTable.Columns.Add("ord_prod", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select A.compp_clave, B.prod_nombre FROM tb_mstr_comp_prod A " +
                "JOIN tb_cat_producto B ON A.compp_clave = B.prod_clave AND B.prod_tipo = 'MP' " +
                "WHERE A.prod_clave = '" + clave_prod + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["compp_clave"] = (object)this.read["compp_clave"].ToString().Trim();
                    row["prod_nombre"] = (object)this.read["prod_nombre"].ToString().Trim();
                    row["ord_prod"] = (object)ord_prod;
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public string agrega_detalle_mp(string folio, string clave_prod, string nombre_prod, string ord_prod)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_det_quejas_det(que_folio, prod_clave, prod_nombre, ord_prod) VALUES('" + folio + "', '" + clave_prod + "', '" + nombre_prod + "', '" + ord_prod + "')";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public string cancela_detalle_mp(string folio, string clave_prod, string ord_prod)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "UPDATE tb_det_quejas_det SET estado = '0' WHERE que_folio = '" + folio + "' AND prod_clave = '" + clave_prod + "' AND ord_prod = '" + ord_prod + "'";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public DataTable productos_MP_detalle(string folio, string ord_prod)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prod_clave", typeof(string));
            dataTable.Columns.Add("estado", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT prod_clave, estado FROM tb_det_quejas_det WHERE estado = '1' AND que_folio = '" + folio + "' AND ord_prod = '" + ord_prod + "'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prod_clave"] = (object)this.read["prod_clave"].ToString().Trim();
                    row["estado"] = (object)this.read["estado"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable productos_queja(string folio)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("id_producto", typeof(string));
            dataTable.Columns.Add("qud_ordprod", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.id_producto, A.qud_ordprod FROM tb_det_quejas A " +
                "WHERE A.que_folio = '" + folio + "' AND " +
                "(select count(B.prod_clave) FROM tb_mstr_comp_prod B WHERE B.prod_clave = A.id_producto) > 1 " +
                "GROUP BY A.que_folio, A.id_producto, A.qud_ordprod";// "SELECT que_folio, id_producto, qud_ordprod FROM tb_det_quejas WHERE que_folio = '" + folio + "' GROUP BY que_folio, id_producto, qud_ordprod";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                    row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public bool valida_productos_mp(string folio)
        {
            this.Abrir();
            bool VAL = false;
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("que_folio", typeof(string));
                dataTable.Columns.Add("id_producto", typeof(string));
                dataTable.Columns.Add("qud_ordprod", typeof(string));
                dataTable.Columns.Add("qud_ptcptp", typeof(string));
                dataTable.Columns.Add("validacion", typeof(string));

                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT A.que_folio, A.id_producto, A.qud_ordprod, A.qud_ptcptp FROM tb_det_quejas A " +
                    "WHERE A.que_folio = '" + folio + "' AND " +
                    "(select count(B.prod_clave) FROM tb_mstr_comp_prod B WHERE B.prod_clave = A.id_producto) > 1 AND A.qud_ptcptp = 'PTP' " +
                    "GROUP BY A.que_folio, A.id_producto, A.qud_ordprod, A.qud_ptcptp";// "SELECT que_folio, id_producto, qud_ordprod FROM tb_det_quejas WHERE que_folio = '" + folio + "' GROUP BY que_folio, id_producto, qud_ordprod";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        DataRow row = dataTable.NewRow();
                        row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                        row["id_producto"] = (object)this.read["id_producto"].ToString().Trim();
                        row["qud_ordprod"] = (object)this.read["qud_ordprod"].ToString().Trim();
                        row["validacion"] = "";
                        dataTable.Rows.Add(row);
                    }
                }
                this.read.Close();
                this.read.Dispose();
                this.com.Dispose();

                if (dataTable.Rows.Count > 0)
                {
                    string str = "";
                    DataTable dtmp = new DataTable();
                    dtmp.Columns.Add("prod_clave", typeof(string));
                    dtmp.Columns.Add("prod_mp", typeof(string));
                    dtmp.Columns.Add("prod_or", typeof(string));
                    dtmp.Columns.Add("seleccionado", typeof(string));

                    foreach (DataRow rt in dataTable.Rows)
                    {
                        string o_produ = rt["qud_ordprod"].ToString();
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT compp_clave FROM tb_mstr_comp_prod WHERE prod_clave = '" + rt["id_producto"].ToString() + "'";
                        this.read = this.com.ExecuteReader();
                        DataRow rw;
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                rw = dtmp.NewRow();
                                rw["prod_clave"] = rt["id_producto"].ToString();
                                rw["prod_or"] = rt["qud_ordprod"].ToString();
                                rw["prod_mp"] = read["compp_clave"].ToString().Trim();
                                rw["seleccionado"] = "";
                                dtmp.Rows.Add(rw);
                            }
                        }
                        this.read.Close();
                        this.read.Dispose();
                        this.com.Dispose();

                        foreach (DataRow ry in dtmp.Select("prod_clave = '" + rt["id_producto"].ToString() + "' AND prod_or = '" + o_produ + "'"))
                        {
                            this.com = this.conn.CreateCommand();
                            this.com.CommandText = "SELECT prod_clave FROM tb_det_quejas_det WHERE que_folio = '" + folio + "' AND prod_clave = '" + ry["prod_mp"] + "' AND ord_prod = '" + o_produ + "' AND estado = '1'";
                            this.read = this.com.ExecuteReader();
                            if (this.read.HasRows)
                            {
                                ry["seleccionado"] = "1";
                            }
                            this.read.Close();
                            this.read.Dispose();
                            this.com.Dispose();
                        }

                        bool fnd = false;
                        foreach (DataRow ry in dtmp.Select("seleccionado = '1' AND prod_clave = '" + rt["id_producto"].ToString() + "' AND prod_or = '" + o_produ + "'"))
                        {
                            rt["validacion"] = "1";
                            fnd = true;
                        }
                        if (fnd == false)
                        {
                            rt["validacion"] = "0";
                        }

                        //if (dtmp.Rows.Count > 1)
                        //{
                        //    foreach (DataRow ry in dtmp.Rows)
                        //    {
                        //        this.com = this.conn.CreateCommand();
                        //        this.com.CommandText = "SELECT prod_clave FROM tb_det_quejas_det WHERE que_folio = '" + folio + "' AND prod_clave = '" + ry["prod_clave"] + "' AND ord_prod = '" + ord_prod + "' AND estado = '1'";
                        //        this.read = this.com.ExecuteReader();
                        //        if (this.read.HasRows)
                        //        {
                        //            ry["seleccionado"] = "1";
                        //        }
                        //        this.read.Close();
                        //        this.read.Dispose();
                        //        this.com.Dispose();
                        //    }
                        //}


                    }
                    this.Cerrar();

                    int i = dataTable.Rows.Count;
                    int j = 0;
                    foreach (DataRow rt in dataTable.Rows)
                    {
                        if (rt["validacion"].ToString() == "1")
                        {
                            j++;
                        }
                    }

                    if (i == j)
                        VAL = true;
                    else
                        VAL = false;
                }
                else
                    VAL = true;
            }
            catch (Exception ex)
            {
                VAL = false;
            }



            return VAL;
        }

        public string mp_variedades(string folio)
        {
            string str = "";
            this.Abrir();
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "SELECT prod_nombre, variedad FROM tb_det_quejas_det WHERE estado = '1' AND que_folio = '" + folio + "'";
                this.read = this.com.ExecuteReader();
                if (this.read.HasRows)
                {
                    while (this.read.Read())
                    {
                        str += read["prod_nombre"].ToString().Trim() + " (" + read["variedad"].ToString().Trim() + "),";
                    }
                }
                this.com.Dispose();
                str = str.TrimEnd(',').ToString();
            }
            catch (SqlException ex)
            {
                str = "";
            }
            this.Cerrar();

            return str;
        }

        //INGRESO DE FACTURAS
        public string agrega_detalle_costo_factura(string folio, string factura, string costo)
        {
            this.Abrir();
            string str;
            try
            {
                this.com = this.conn.CreateCommand();
                this.com.CommandText = "INSERT INTO tb_det_quejas_fact(que_folio, factura, costo) VALUES('" + folio + "', '" + factura + "', '" + costo + "')";
                this.com.ExecuteNonQuery();
                this.com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        //BUSCAR FECHA DE EMBARQUE
        public string fecha_embarque(string folio, string tipo, string clave)
        {
            string str = "";

            this.Abrir();
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
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT pdn_folio FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' and fcn_tipo = '" + val_tipo + "' AND fcn_fecha >= '" + DateTime.Now.AddDays(60).ToShortDateString() + "'";
                    this.read = this.com.ExecuteReader();
                    bool fnd = false;
                    string pdn_fol = "";
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            pdn_fol = this.read["pdn_folio"].ToString().Trim();
                            fnd = true;
                        }
                    }
                    read.Close();
                    read.Dispose();
                    this.com.Dispose();

                    if (fnd == true)
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + pdn_fol + "' and pdn_tipo = 'EXP'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                str = this.read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        this.com.Dispose();
                    }
                    else
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + folio + "' and pdn_tipo = 'EXP'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                str = this.read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        this.com.Dispose();
                    }
                }
                else
                {
                    this.com = this.conn.CreateCommand();
                    this.com.CommandText = "SELECT pdn_folio FROM tb_mstr_facturas_nal WHERE fcn_folio = '" + folio + "' and fcn_tipo = '" + val_tipo + "' AND fcn_fecha >= '" + DateTime.Now.AddDays(60).ToShortDateString() + "'";
                    this.read = this.com.ExecuteReader();
                    bool fnd = false;
                    string pdn_fol = "";
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            pdn_fol = this.read["pdn_folio"].ToString().Trim();
                            fnd = true;
                        }
                    }
                    read.Close();
                    read.Dispose();
                    this.com.Dispose();

                    if (fnd == true)
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_nal WHERE pdn_folio = '" + pdn_fol + "' and pdn_tipo = 'NAL'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                str = this.read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        this.com.Dispose();
                    }
                    else
                    {
                        this.com = this.conn.CreateCommand();
                        this.com.CommandText = "SELECT pdn_fecha FROM tb_mstr_pedidos_nal WHERE pdn_folio = '" + folio + "' and pdn_tipo = 'NAL'";
                        this.read = this.com.ExecuteReader();
                        if (this.read.HasRows)
                        {
                            while (this.read.Read())
                            {
                                str = this.read["pdn_fecha"].ToString().Trim();
                            }
                        }
                        read.Close();
                        read.Dispose();
                        this.com.Dispose();
                    }
                }

            }
            catch (SqlException ex)
            {
                str = "";
            }
            this.Cerrar();

            return str;
        }

        //SCORE CARD
        #region no usables
        public DataTable score_card_transportistas_combo()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_transportista", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select DISTINCT(A.que_transportista), B.prov_nombre " +
                "FROM tb_mstr_quejas_serv A left JOIN tb_cat_proveedor B ON A.que_transportista = B.prov_clave " +
                "where A.que_fecha >= '01/01/2023' and A.que_fumi_trans = 'T' " +
                "AND A.que_transportista <> '' AND B.prov_nombre is not null ORDER BY B.prov_nombre";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable score_card_transportistas_mstr(string trans)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_fecha", typeof(string));
            dataTable.Columns.Add("que_semana", typeof(string));
            dataTable.Columns.Add("que_fechaemb", typeof(string));
            dataTable.Columns.Add("que_tipo", typeof(string));
            dataTable.Columns.Add("que_pedido", typeof(string));
            dataTable.Columns.Add("que_mes", typeof(string));
            dataTable.Columns.Add("que_transportista", typeof(string));
            dataTable.Columns.Add("que_destino", typeof(string));
            dataTable.Columns.Add("que_caja", typeof(string));
            dataTable.Columns.Add("que_comentario", typeof(string));
            dataTable.Columns.Add("que_perdida_mn", typeof(string));
            dataTable.Columns.Add("que_perdida_usd", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "select que_folio, que_fecha, que_semana, que_fechaemb, que_tipo, que_pedido, que_mes, que_transportista, que_destino, que_caja, que_comentario, que_perdida_mn, que_perdida_usd " +
                "from tb_mstr_quejas_serv where que_transportista = '" + trans + "' and que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND que_fumi_trans = 'T'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_fecha"] = (object)Convert.ToDateTime(this.read["que_fecha"].ToString().Trim()).ToShortDateString();
                    row["que_semana"] = (object)this.read["que_semana"].ToString().Trim();
                    row["que_fechaemb"] = (object)Convert.ToDateTime(this.read["que_fechaemb"].ToString().Trim()).ToShortDateString();
                    row["que_tipo"] = (object)this.read["que_tipo"].ToString().Trim();
                    row["que_pedido"] = (object)this.read["que_pedido"].ToString().Trim();
                    row["que_mes"] = (object)this.read["que_mes"].ToString().Trim();
                    row["que_transportista"] = (object)this.read["que_transportista"].ToString().Trim();
                    row["que_destino"] = (object)this.read["que_destino"].ToString().Trim();
                    row["que_caja"] = (object)this.read["que_caja"].ToString().Trim();
                    row["que_comentario"] = (object)this.read["que_comentario"].ToString().Trim();
                    row["que_perdida_mn"] = (object)this.read["que_perdida_mn"].ToString().Trim();
                    row["que_perdida_usd"] = (object)this.read["que_perdida_usd"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable score_card_transportistas_det(string trans)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            dataTable.Columns.Add("que_clave", typeof(string));
            dataTable.Columns.Add("que_nombre", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT A.que_folio, A.que_lave, A.que_nombre from tb_det_queja_serv A join tb_mstr_quejas_serv B ON A.que_folio = B.que_folio where " +
                "B.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND B.que_transportista = '" + trans + "' AND B.que_fumi_trans = 'T'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_folio"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_clave"] = (object)Convert.ToDateTime(this.read["que_clave"].ToString().Trim());
                    row["que_semana"] = (object)this.read["que_semana"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }

        public DataTable score_card_transportistas_cant(string trans)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_clave", typeof(string));
            dataTable.Columns.Add("que_nombre", typeof(string));
            dataTable.Columns.Add("CANTIDAD", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();
            this.com.CommandText = "SELECT distinct(que_clave), que_nombre, " +
                "(SELECT COUNT(que_clave) from tb_det_queja_serv C join tb_mstr_quejas_serv D ON C.que_folio = D.que_folio " +
                "where A.que_clave = C.que_clave and D.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND D.que_transportista = '" + trans + "' AND D.que_fumi_trans = 'T') AS CANTIDAD " +
                "from tb_det_queja_serv A join tb_mstr_quejas_serv B ON A.que_folio = B.que_folio " +
                "where B.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND B.que_transportista = '" + trans + "' AND B.que_fumi_trans = 'T'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["que_clave"] = (object)this.read["que_folio"].ToString().Trim();
                    row["que_nombre"] = (object)this.read["que_clave"].ToString().Trim();
                    row["CANTIDAD"] = (object)this.read["que_semana"].ToString().Trim();
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }
        #endregion

        public DataTable score_card_embarques_quejas_totales(string trans, string f_i, string f_f, string area)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("total", typeof(string));
            dataTable.Columns.Add("total_quejas", typeof(string));
            dataTable.Columns.Add("total_porcen", typeof(string));
            this.Abrir();
            this.com = this.conn.CreateCommand();

            //string f_ia = Convert.ToDateTime(f_i).ToString("dd/MM/yyyy");
            //string f_fa = Convert.ToDateTime(f_f).ToString("dd/MM/yyyy");

            if (trans == "0")
            {
                this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
                    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> '' " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') " +
                    "group by A.prov_clave, C.prov_nombre";
            }
            else
            {
                this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
                    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> '' " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' " +
                    "group by A.prov_clave, C.prov_nombre";
            }

            //this.com.CommandText = "SELECT distinct(que_clave), que_nombre, " +
            //    "(SELECT COUNT(que_clave) from tb_det_queja_serv C join tb_mstr_quejas_serv D ON C.que_folio = D.que_folio " +
            //    "where A.que_clave = C.que_clave and D.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND D.que_transportista = '" + trans + "' AND D.que_fumi_trans = 'T') AS CANTIDAD " +
            //    "from tb_det_queja_serv A join tb_mstr_quejas_serv B ON A.que_folio = B.que_folio " +
            //    "where B.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND B.que_transportista = '" + trans + "' AND B.que_fumi_trans = 'T'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                    row["total"] = (object)this.read["total"].ToString().Trim();
                    row["total_quejas"] = "0";
                    row["total_porcen"] = "0";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            SqlDataAdapter adapter;
            if (trans == "0")
            {
                adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) AS cant_quejas " +
                    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> '' " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') and " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) > 0", this.conn);
            }
            else
            {
                adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) AS cant_quejas " +
                    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> '' " +
                    "AND A.pdn_estatus <> 'C' AND " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) > 0", this.conn);
                //adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
                //    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                //    "AND B.que_fumi_trans = 'T' " +
                //    "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
                //    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                //    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> '' " +
                //    "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND " +
                //    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                //    "AND B.que_fumi_trans = 'T' and B.que_transportista = '" + trans + "' AND B.que_tipo = 'NACIONAL'), 0) > 0", this.conn);
            }
            DataSet set = new DataSet();
            adapter.Fill(set, "quejas_totales");

            foreach (DataRow rw in dataTable.Rows)
            {
                bool fnd = false;
                foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "'"))
                {
                    fnd = true;
                    break;
                }
                if (fnd == true)
                {
                    decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)", "prov_clave = '" + rw["prov_clave"].ToString() + "'"));
                    rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();

                    decimal porcentaje = Math.Round(100 - ((Convert.ToDecimal(rw["total_quejas"].ToString()) * 100) / Convert.ToDecimal(rw["total"].ToString())), 0);
                    rw["total_porcen"] = porcentaje.ToString();

                }
                //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
                //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
                //    .Sum(r => r.Field<Int32>("cant_quejas"));

            }

            //exportacion
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("prov_clave", typeof(string));
            dataTable2.Columns.Add("prov_nombre", typeof(string));
            dataTable2.Columns.Add("total", typeof(string));
            dataTable2.Columns.Add("total_quejas", typeof(string));
            dataTable2.Columns.Add("total_porcen", typeof(string));

            //string f_ia = Convert.ToDateTime(f_i).ToString("dd/MM/yyyy");
            //string f_fa = Convert.ToDateTime(f_f).ToString("dd/MM/yyyy");

            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (trans == "0")
            {
                this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') " +
                    "group by A.prov_clave, C.prov_nombre";
            }
            else
            {
                this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' " +
                    "group by A.prov_clave, C.prov_nombre";
            }

            //this.com.CommandText = "SELECT distinct(que_clave), que_nombre, " +
            //    "(SELECT COUNT(que_clave) from tb_det_queja_serv C join tb_mstr_quejas_serv D ON C.que_folio = D.que_folio " +
            //    "where A.que_clave = C.que_clave and D.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND D.que_transportista = '" + trans + "' AND D.que_fumi_trans = 'T') AS CANTIDAD " +
            //    "from tb_det_queja_serv A join tb_mstr_quejas_serv B ON A.que_folio = B.que_folio " +
            //    "where B.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND B.que_transportista = '" + trans + "' AND B.que_fumi_trans = 'T'";
            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable2.NewRow();
                    row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                    row["total"] = (object)this.read["total"].ToString().Trim();
                    row["total_quejas"] = "0";
                    dataTable2.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            //SqlDataAdapter adapter2;
            if (trans == "0")
            {
                adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) AS cant_quejas " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> '' " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') and " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) > 0", this.conn);
                //adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
                //    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                //    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
                //    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                //    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  AND " +
                //    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                //    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
                //    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY')", this.conn);
            }
            else
            {
                adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) AS cant_quejas " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> '' " +
                    "AND A.pdn_estatus <> 'C' AND " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                    "AND B.que_fechaemb = A.pdn_fecha AND B.que_fechaemb >= '" + f_i + "' AND B.que_fechaemb <= '" + f_f + "' " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) > 0", this.conn);
                //adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
                //    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                //    "AND B.que_fumi_trans = 'T' " +
                //    "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
                //    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                //    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  and " +
                //    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
                //    "AND B.que_fumi_trans = 'T' " +
                //    "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
                //    "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "'", this.conn);
            }
            //DataSet set = new DataSet();
            adapter.Fill(set, "quejas_totales2");

            foreach (DataRow rw in dataTable2.Rows)
            {
                bool fnd = false;
                foreach (DataRow re in set.Tables["quejas_totales2"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "'"))
                {
                    fnd = true;
                    break;
                }
                if (fnd == true)
                {
                    decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales2"].Compute("SUM(cant_quejas)", "prov_clave = '" + rw["prov_clave"].ToString() + "'"));
                    rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();

                    decimal porcentaje = Math.Round(100 - ((Convert.ToDecimal(rw["total_quejas"].ToString()) * 100) / Convert.ToDecimal(rw["total"].ToString())), 0);
                    rw["total_porcen"] = porcentaje.ToString();
                }

                //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
                //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
                //    .Sum(r => r.Field<Int32>("cant_quejas"));

            }

            foreach (DataRow rt in dataTable2.Rows)
            {
                bool fnd = false;
                foreach (DataRow ry in dataTable.Select("prov_clave = '" + rt["prov_clave"].ToString() + "'"))
                {
                    fnd = true;
                    ry["total"] = Convert.ToInt32(ry["total"]) + Convert.ToInt32(rt["total"]);
                    ry["total_quejas"] = Convert.ToInt32(ry["total_quejas"]) + Convert.ToInt32(rt["total_quejas"]);
                    decimal porcentaje = Math.Round(100 - ((Convert.ToDecimal(ry["total_quejas"].ToString()) * 100) / Convert.ToDecimal(ry["total"].ToString())), 1);
                    ry["total_porcen"] = porcentaje.ToString();
                }
                if (fnd == false)
                {
                    dataTable.Rows.Add(rt.ItemArray);
                }
            }


            this.Cerrar();

            //DataView tbView = dataTable.DefaultView;
            //tbView.Sort = "prov_nombre";
            //dataTable = tbView.ToTable();

            //string sum_tot_nom = "TOTAL";
            //Decimal sum_tot_tot = 0;
            //Decimal sum_tot_que = 0;
            //foreach (DataRow ri in dataTable.Rows)
            //{
            //    sum_tot_tot = sum_tot_tot + Convert.ToInt32(ri["total"].ToString());
            //    sum_tot_que = sum_tot_que + Convert.ToInt32(ri["total_quejas"].ToString());
            //}
            //decimal porcentajeT = Math.Round(100 - ((sum_tot_que * 100) / sum_tot_tot), 1);

            //DataRow r = dataTable.NewRow();
            //r["prov_clave"] = "";
            //r["prov_nombre"] = sum_tot_nom;
            //r["total"] = Convert.ToInt32(sum_tot_tot.ToString());
            //r["total_quejas"] = Convert.ToInt32(sum_tot_que.ToString());
            //r["total_porcen"] = Convert.ToDecimal(porcentajeT.ToString());
            //dataTable.Rows.InsertAt(r, 0);

            return dataTable;
        }
        public DataTable score_card_total_perdidas(string trans, string f_i, string f_f, string area)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("mn", typeof(string));
            dataTable.Columns.Add("usd", typeof(string));
            //string f_ia = Convert.ToDateTime(f_i).ToString("dd/MM/yyyy");
            //string f_fa = Convert.ToDateTime(f_f).ToString("dd/MM/yyyy");

            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (trans == "0")
            {
                this.com.CommandText = "SELECT ISNULL(sum(que_perdida_mn), 0) AS MN, ISNULL(sum(que_perdida_usd), 0) as USD FROM tb_mstr_quejas_serv WHERE que_fumi_trans = 'T' and que_estatus = 'A' AND " +
                    "que_fechaemb >= '" + f_i + "' and que_fechaemb <= '" + f_f + "' AND embtracli = '" + area + "'";
            }
            else
            {
                this.com.CommandText = "SELECT ISNULL(sum(que_perdida_mn), 0) AS MN, ISNULL(sum(que_perdida_usd), 0) as USD FROM tb_mstr_quejas_serv WHERE que_fumi_trans = 'T' and que_estatus = 'A' AND " +
                    "que_fechaemb >= '" + f_i + "' and que_fechaemb <= '" + f_f + "' AND que_transportista = '" + trans + "' AND embtracli = '" + area + "'";
            }

            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["mn"] = (object)Convert.ToDecimal(this.read["MN"].ToString().Trim());
                    row["usd"] = (object)Convert.ToDecimal(this.read["USD"].ToString().Trim());
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();
            this.Cerrar();
            return dataTable;
        }
        public DataTable score_card_embarques_quejas_totales_caja_nacional(string trans, string f_i, string f_f, string area)
        {
            SqlDataAdapter adapter;
            DataSet set = new DataSet();
            if (trans == "0")
            {
                adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) AS cant_quejas, '0' AS cant_exp " +
                    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' " +
                    "order by A.prov_clave, A.placacaja", this.conn);
                adapter.Fill(set, "quejas_totales");

                adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
                        "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                        "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION' AND B.embtracli = '" + area + "' AND B.que_transportista = A.prov_clave), 0) AS cant_quejas " +
                        "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
                        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' " +
                        "order by A.prov_clave, A.placacaja", this.conn);
                adapter.Fill(set, "quejas_totales2");
            }
            else
            {
                adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL' and B.que_transportista = '" + trans + "' AND B.embtracli = '" + area + "'), 0) AS cant_quejas, '0' AS cant_exp " +
                    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' and A.prov_clave = '" + trans + "' " +
                    "order by A.prov_clave, A.placacaja", this.conn);

                adapter.Fill(set, "quejas_totales");

                adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
                        "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                        "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION' and B.que_transportista = '" + trans + "' AND B.embtracli = '" + area + "'), 0) AS cant_quejas " +
                        "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
                        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' and A.prov_clave = '" + trans + "' " +
                        "order by A.prov_clave, A.placacaja", this.conn);
                adapter.Fill(set, "quejas_totales2");
            }

            DataView view = new DataView(set.Tables["quejas_totales"]);
            DataTable distinctValues = view.ToTable(true, "placacaja", "prov_clave", "prov_nombre");

            DataView view2 = new DataView(set.Tables["quejas_totales2"]);
            DataTable distinctValues2 = view2.ToTable(true, "placacaja", "prov_clave", "prov_nombre");

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("placacaja", typeof(string));
            dataTable.Columns.Add("registros", typeof(string));
            dataTable.Columns.Add("total", typeof(string)); //total nacional
            dataTable.Columns.Add("total_exp", typeof(string)); //total exportacion
            dataTable.Columns.Add("total_quejas", typeof(string)); //total quejas
            dataTable.Columns.Add("total_porcen", typeof(string)); //total embarques


            foreach (DataRow ry in distinctValues.Rows)
            {
                DataRow rw = dataTable.NewRow();
                rw["placacaja"] = ry["placacaja"].ToString().Trim();
                rw["prov_clave"] = ry["prov_clave"].ToString().Trim();
                rw["prov_nombre"] = ry["prov_nombre"].ToString().Trim();
                Int32 registros = 0;
                decimal total = 0;
                decimal total_exp = 0;
                decimal total_quejas = 0;
                foreach (DataRow rx in set.Tables["quejas_totales"].Select("prov_clave = '" + ry["prov_clave"].ToString().Trim() + "' AND placacaja = '" + ry["placacaja"].ToString().Trim() + "'"))
                {
                    total = total + Convert.ToDecimal(rx["cant_quejas"].ToString());
                    registros++;
                }
                rw["registros"] = registros.ToString();
                rw["total"] = total.ToString();
                rw["total_exp"] = "0";
                rw["total_quejas"] = "0";
                rw["total_porcen"] = "0";
                dataTable.Rows.Add(rw);
            }

            foreach (DataRow ry in distinctValues2.Rows)
            {
                //CONTEO DE REGISTROS
                Int32 registros = 0;
                decimal total_exp = 0;
                decimal total_quejas = 0;
                foreach (DataRow rx in set.Tables["quejas_totales2"].Select("prov_clave = '" + ry["prov_clave"].ToString().Trim() + "' AND placacaja = '" + ry["placacaja"].ToString().Trim() + "'"))
                {
                    total_exp = total_exp + Convert.ToDecimal(rx["cant_quejas"].ToString());
                    registros++;
                }

                bool fnd = false;
                foreach (DataRow rt in dataTable.Select("prov_clave = '" + ry["prov_clave"].ToString().Trim() + "' AND placacaja = '" + ry["placacaja"].ToString().Trim() + "'"))
                {
                    fnd = true;
                    rt["registros"] = (Convert.ToInt32(rt["registros"]) + registros).ToString();
                    rt["total_exp"] = total_exp.ToString();
                }
                if (fnd == false)
                {
                    DataRow rw = dataTable.NewRow();
                    rw["placacaja"] = ry["placacaja"].ToString().Trim();
                    rw["prov_clave"] = ry["prov_clave"].ToString().Trim();
                    rw["prov_nombre"] = ry["prov_nombre"].ToString().Trim();
                    rw["registros"] = registros.ToString();
                    rw["total"] = "0";
                    rw["total_exp"] = total_exp.ToString();
                    rw["total_quejas"] = "0";
                    rw["total_porcen"] = "0";
                    dataTable.Rows.Add(rw);
                }
            }

            decimal sum_emb = 0;
            decimal sum_q_nac = 0;
            decimal sum_q_exp = 0;
            decimal sum_q_tot = 0;
            decimal por_final = 0;
            foreach (DataRow ry in dataTable.Rows)
            {
                ry["total_quejas"] = Convert.ToDecimal(ry["total"]) + Convert.ToDecimal(ry["total_exp"]);
                if ((Convert.ToDecimal(ry["total"]) + Convert.ToDecimal(ry["total_exp"])) == 0)
                {
                    ry["total_porcen"] = "100";
                }
                else
                {
                    decimal por = Math.Round((Convert.ToDecimal(ry["total_quejas"]) * 100) / Convert.ToDecimal(ry["registros"]), 1);
                    decimal por_def = 100 - por;
                    ry["total_porcen"] = por_def.ToString();
                }
                sum_emb = sum_emb + Convert.ToDecimal(ry["registros"]);
                sum_q_nac = sum_q_nac + Convert.ToDecimal(ry["total"]);
                sum_q_exp = sum_q_exp + Convert.ToDecimal(ry["total_exp"]);
                sum_q_tot = sum_q_tot + Convert.ToDecimal(ry["total_quejas"]);
            }

            DataRow rwf = dataTable.NewRow();
            rwf["placacaja"] = "TOTALES";
            rwf["prov_clave"] = "";
            rwf["prov_nombre"] = "";
            rwf["registros"] = sum_emb.ToString();
            rwf["total"] = sum_q_nac.ToString();
            rwf["total_exp"] = sum_q_exp.ToString();
            rwf["total_quejas"] = sum_q_tot.ToString();
            decimal por_f = Math.Round((sum_q_tot * 100) / sum_emb, 1);
            decimal por_def_f = 100 - por_f;
            rwf["total_porcen"] = por_def_f.ToString();
            dataTable.Rows.Add(rwf);

            return dataTable;

            #region comentado
            //string prv_ac = "";
            //string prv_an = "";
            //string pla_ac = "";
            //string pla_an = "";
            //int i = 0;
            //foreach (DataRow rw in set.Tables["quejas_totales"].Rows)
            //{
            //    prv_ac = rw["prov_clave"].ToString().Trim();
            //    pla_ac = rw["placacaja"].ToString().Trim();

            //    if (i == 0)
            //    {
            //        dataTable.Rows.Add(rw["prov_clave"].ToString().Trim(), rw["prov_nombre"].ToString().Trim(), rw["placacaja"].ToString().Trim(), "0", rw["cant_quejas"].ToString().Trim(),
            //            rw["cant_exp"].ToString().Trim(), "0", "0");
            //        i++;
            //    }
            //    else
            //    {
            //        //verificar que haya sido agregado
            //        bool fnd = false;
            //        foreach (DataRow rx in dataTable.Select("prov_clave = '" + prv_ac + "' AND placacaja = '" + pla_ac + "'"))
            //        {
            //            fnd = true;
            //            Int32 tot_reg = 0;
            //            decimal tot_cant_quejas = 0;
            //            foreach (DataRow ry in set.Tables["quejas_totales"].Select("prov_clave = '" + prv_ac + "' AND placacaja = '" + pla_ac + "'"))
            //            {
            //                tot_reg++;//registros
            //                tot_cant_quejas = tot_cant_quejas + Convert.ToInt32(ry["cant_quejas"].ToString().Trim());//total
            //                rx["registros"] = tot_reg.ToString();
            //                rx["total"] = tot_cant_quejas;
            //            }

            //        }
            //        if (fnd == false)
            //        {
            //            dataTable.Rows.Add(rw["prov_clave"].ToString().Trim(), rw["prov_nombre"].ToString().Trim(), rw["placacaja"].ToString().Trim(), "0", rw["cant_quejas"].ToString().Trim(),
            //            rw["cant_exp"].ToString().Trim(), "0", "0");    
            //        }

            //    }

            //    //total de quejas





            //}

            //foreach (DataRow rw in dataTable.Rows)
            //{
            //    bool fnd = false;
            //    decimal total = 0;
            //    decimal total_exp = 0;
            //    foreach (DataRow rx in set.Tables["quejas_totales2"].Select("prov_clave = '" + rw["prov_clave"].ToString().Trim() + "' AND placacaja = '" + rw["placacaja"].ToString().Trim() + "'"))
            //    {
            //        fnd = true;
            //        total_exp = total_exp + Convert.ToDecimal(rx["cant_quejas"].ToString().Trim());
            //        rw["total_exp"] = total_exp.ToString();
            //    }
            //    if (fnd == false)
            //    {
            //        dataTable.Rows.Add(rw["prov_clave"].ToString().Trim(), rw["prov_nombre"].ToString().Trim(), rw["placacaja"].ToString().Trim(), "0",
            //        rw["cant_quejas"].ToString().Trim(), rw["cant_quejas"].ToString().Trim(), "0");
            //    }
            //}







            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("prov_clave", typeof(string));
            //dataTable.Columns.Add("prov_nombre", typeof(string));
            //dataTable.Columns.Add("placacaja", typeof(string));
            //dataTable.Columns.Add("total", typeof(string));
            //dataTable.Columns.Add("total_quejas", typeof(string));
            //dataTable.Columns.Add("total_porcen", typeof(string));

            ////string f_ia = Convert.ToDateTime(f_i).ToString("dd/MM/yyyy");
            ////string f_fa = Convert.ToDateTime(f_f).ToString("dd/MM/yyyy");

            //this.Abrir();
            //this.com = this.conn.CreateCommand();
            //if (trans == "0")
            //{
            //    this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
            //        "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') and A.placacaja <> '' " +
            //        "group by A.prov_clave, C.prov_nombre, A.placacaja " +
            //        "ORDER BY C.prov_nombre, A.placacaja";
            //}
            //else
            //{
            //    this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
            //        "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.prov_clave = '" + trans + "' and A.placacaja <> '' " +
            //        "group by A.prov_clave, C.prov_nombre, A.placacaja " +
            //        "ORDER BY C.prov_nombre, A.placacaja";
            //}

            //this.read = this.com.ExecuteReader();
            //if (this.read.HasRows)
            //{
            //    while (this.read.Read())
            //    {
            //        DataRow row = dataTable.NewRow();
            //        row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
            //        row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
            //        row["placacaja"] = (object)this.read["placacaja"].ToString().Trim();
            //        row["total"] = (object)this.read["total"].ToString().Trim();
            //        row["total_quejas"] = "0";
            //        dataTable.Rows.Add(row);
            //    }
            //}
            //this.read.Close();
            //this.read.Dispose();
            //this.com.Dispose();

            //DataTable dataTable2 = new DataTable();
            //dataTable2.Columns.Add("prov_clave", typeof(string));
            //dataTable2.Columns.Add("prov_nombre", typeof(string));
            //dataTable2.Columns.Add("placacaja", typeof(string));
            //dataTable2.Columns.Add("total", typeof(string));
            //dataTable2.Columns.Add("total_quejas", typeof(string));
            //string prov_clave = "";
            //string placa = "";
            //string total = "";
            //int i = 0;
            //foreach (DataRow rw in dataTable.Rows)
            //{
            //    prov_clave = rw["prov_clave"].ToString();
            //    placa = rw["placacaja"].ToString();
            //    total = rw["total"].ToString();
            //    if (i == 0)
            //    {
            //        i++;
            //        DataRow row = dataTable2.NewRow();
            //        row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
            //        row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
            //        row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
            //        row["total"] = (object)rw["total"].ToString().Trim();
            //        row["total_quejas"] = "0";
            //        dataTable2.Rows.Add(row);
            //    }
            //    else
            //    {
            //        bool fnd = false;
            //        foreach (DataRow rx in dataTable2.Select("placacaja = '" + placa + "' AND prov_clave = '" + prov_clave + "'"))
            //        {
            //            fnd = true;
            //            rx["total"] = Convert.ToDecimal(rx["total"]) + Convert.ToDecimal(total);
            //        }
            //        if (fnd == false)
            //        {
            //            DataRow row = dataTable2.NewRow();
            //            row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
            //            row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
            //            row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
            //            row["total"] = (object)rw["total"].ToString().Trim();
            //            row["total_quejas"] = "0";
            //            dataTable2.Rows.Add(row);
            //        }
            //    }
            //}

            //SqlDataAdapter adapter;
            //if (trans == "0")
            //{
            //    //adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
            //    //    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
            //    //    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
            //    //    "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //    //    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
            //    //    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' " +
            //    //    "and ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
            //    //    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) > 0 " +
            //    //    "order by A.prov_clave, A.placacaja", this.conn);
            //    adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
            //        "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
            //        "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
            //        "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' " +
            //        "order by A.prov_clave, A.placacaja", this.conn);
            //}
            //else
            //{
            //    adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
            //        "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
            //        "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
            //        "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND A.placacaja <> '' " +
            //        "order by A.prov_clave, A.placacaja", this.conn);
            //}
            //DataSet set = new DataSet();
            //adapter.Fill(set, "quejas_totales");

            //foreach (DataRow rw in dataTable2.Rows)
            //{
            //    //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
            //    //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
            //    //    .Sum(r => r.Field<Int32>("cant_quejas"));

            //    bool fnd = false;
            //    foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"))
            //    {
            //        fnd = true;
            //        break;
            //    }

            //    if (fnd == true)
            //    {
            //        decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)",
            //            "prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"));
            //        rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
            //    }

            //}

            ////exportacion
            //DataTable dataTable3 = new DataTable();
            //dataTable3.Columns.Add("prov_clave", typeof(string));
            //dataTable3.Columns.Add("prov_nombre", typeof(string));
            //dataTable3.Columns.Add("placacaja", typeof(string));
            //dataTable3.Columns.Add("total", typeof(string));
            //dataTable3.Columns.Add("total_quejas", typeof(string));

            //this.com = this.conn.CreateCommand();
            //if (trans == "0")
            //{
            //    this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
            //        "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') and A.placacaja <> '' " +
            //        "group by A.prov_clave, C.prov_nombre, A.placacaja " +
            //        "ORDER BY C.prov_nombre, A.placacaja";
            //}
            //else
            //{
            //    this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
            //        "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.prov_clave = '" + trans + "' and A.placacaja <> '' " +
            //        "group by A.prov_clave, C.prov_nombre, A.placacaja " +
            //        "ORDER BY C.prov_nombre, A.placacaja";
            //}

            //this.read = this.com.ExecuteReader();
            //if (this.read.HasRows)
            //{
            //    while (this.read.Read())
            //    {
            //        DataRow row = dataTable3.NewRow();
            //        row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
            //        row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
            //        row["placacaja"] = (object)this.read["placacaja"].ToString().Trim();
            //        row["total"] = (object)this.read["total"].ToString().Trim();
            //        row["total_quejas"] = "0";
            //        dataTable3.Rows.Add(row);
            //    }
            //}
            //this.read.Close();
            //this.read.Dispose();
            //this.com.Dispose();

            //DataTable dataTable4 = new DataTable();
            //dataTable4.Columns.Add("prov_clave", typeof(string));
            //dataTable4.Columns.Add("prov_nombre", typeof(string));
            //dataTable4.Columns.Add("placacaja", typeof(string));
            //dataTable4.Columns.Add("total", typeof(string));
            //dataTable4.Columns.Add("total_quejas", typeof(string));
            //prov_clave = "";
            //placa = "";
            //total = "";
            //i = 0;
            //foreach (DataRow rw in dataTable3.Rows)
            //{
            //    prov_clave = rw["prov_clave"].ToString();
            //    placa = rw["placacaja"].ToString();
            //    total = rw["total"].ToString();
            //    if (i == 0)
            //    {
            //        i++;
            //        DataRow row = dataTable4.NewRow();
            //        row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
            //        row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
            //        row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
            //        row["total"] = (object)rw["total"].ToString().Trim();
            //        row["total_quejas"] = "0";
            //        dataTable4.Rows.Add(row);
            //    }
            //    else
            //    {
            //        bool fnd = false;
            //        foreach (DataRow rx in dataTable4.Select("placacaja = '" + placa + "' AND prov_clave = '" + prov_clave + "'"))
            //        {
            //            fnd = true;
            //            rx["total"] = Convert.ToDecimal(rx["total"]) + Convert.ToDecimal(total);
            //        }
            //        if (fnd == false)
            //        {
            //            DataRow row = dataTable4.NewRow();
            //            row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
            //            row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
            //            row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
            //            row["total"] = (object)rw["total"].ToString().Trim();
            //            row["total_quejas"] = "0";
            //            dataTable4.Rows.Add(row);
            //        }
            //    }
            //}

            //SqlDataAdapter adapter2;
            //if (trans == "0")
            //{
            //    adapter2 = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
            //        "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
            //        "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
            //        "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' " +
            //        "order by A.prov_clave, A.placacaja", this.conn);
            //}
            //else
            //{
            //    adapter2 = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
            //        "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
            //        "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
            //        "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
            //        "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 and A.placacaja <> ''  " +
            //        "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND A.placacaja <> '' " +
            //        "order by A.prov_clave, A.placacaja", this.conn);
            //}
            ////set = new DataSet();
            //adapter.Fill(set, "quejas_totales4");

            //foreach (DataRow rw in dataTable3.Rows)
            //{
            //    //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
            //    //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
            //    //    .Sum(r => r.Field<Int32>("cant_quejas"));

            //    bool fnd = false;
            //    foreach (DataRow re in set.Tables["quejas_totales4"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"))
            //    {
            //        fnd = true;
            //        break;
            //    }

            //    if (fnd == true)
            //    {
            //        decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales4"].Compute("SUM(cant_quejas)",
            //            "prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"));
            //        rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
            //    }

            //}
            //this.Cerrar();
            //return dataTable2;
            #endregion
        }
        public string fecha_orden_produccion(string orden, string tipo)
        {
            string str = "";
            this.Abrir();
            try
            {
                this.com = this.conn.CreateCommand();
                if (tipo == "PTP")
                {
                    this.com.CommandText = "SELECT ordp_fecha FROM tb_mstr_ordenes_prod WHERE ordp_folio = '" + orden + "'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            str += read["ordp_fecha"].ToString().Trim();
                        }
                    }
                }
                if (tipo == "PTC")
                {
                    this.com.CommandText = "SELECT rpt_fecha FROM tb_mstr_recepcion_pt WHERE rpt_recibo = '" + orden + "'";
                    this.read = this.com.ExecuteReader();
                    if (this.read.HasRows)
                    {
                        while (this.read.Read())
                        {
                            str += read["rpt_fecha"].ToString().Trim();
                        }
                    }
                }
                this.com.Dispose();
            }
            catch (SqlException ex)
            {
                str = "";
            }
            this.Cerrar();

            return str;
        }
        public DataTable score_card_detalle_pedidos(string trans, string f_i, string f_f, string placa, string area)
        {
            this.Abrir();
            SqlDataAdapter adapter;
            DataSet set = new DataSet();

            adapter = new SqlDataAdapter(" select A.pdn_folio, A.cnte_clave, A.cve_subcli, A.pdn_fecha, A.vendedor, A.pdn_monto_total, A.pdn_observacion, A.pdn_tipo, A.placacaja, A.pdn_transporte, A.pdn_pedorigen, " +
                     "B.cnte_nombre, B.cnte_ciudad " +
                     "FROM tb_mstr_pedidos_nal A " +
                     "join tb_cat_cliente B ON A.cnte_clave = B.cnte_clave " +
                     "" +
                     "WHERE pdn_fecha >= '" + f_i + "' and pdn_fecha <= '" + f_f + "' and placacaja <> '' " +
                     "and A.prov_clave = '" + trans + "' and A.placacaja = '" + placa + "' AND A.pdn_pedorigen = 0 ORDER BY A.pdn_folio", this.conn);

            adapter.Fill(set, "detalle_nal");

            adapter = new SqlDataAdapter("select A.pdn_folio, A.cnte_clave, A.cve_subcli, A.pdn_fecha, A.vendedor, A.pdn_monto_total, A.pdn_observacion, A.pdn_tipo, A.placacaja, A.pdn_transporte, A.pdn_pedorigen, " +
                 "B.cnte_nombre, B.cnte_ciudad " +
                 "FROM tb_mstr_pedidos_exp A " +
                 "join tb_cat_cliente B ON A.cnte_clave = B.cnte_clave " +
                 "left join tb_cat_entregas C ON A.cnte_clave = C.cnte_clave AND A.cve_subcli = C.entr_clave " +
                 "WHERE A.pdn_fecha >= '" + f_i + "' and A.pdn_fecha <= '" + f_f + "' and A.placacaja <> '' " +
                 "and A.prov_clave = '" + trans + "' and A.placacaja = '" + placa + "' AND A.pdn_pedorigen = 0", this.conn);
            adapter.Fill(set, "detalle_exp");

            this.Cerrar();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("pdn_folio", typeof(string));
            dataTable.Columns.Add("cnte_clave", typeof(string));
            dataTable.Columns.Add("cve_subcli", typeof(string));
            dataTable.Columns.Add("pdn_fecha", typeof(string));
            dataTable.Columns.Add("vendedor", typeof(string));
            dataTable.Columns.Add("pdn_monto_total", typeof(string));
            dataTable.Columns.Add("pdn_observacion", typeof(string));
            dataTable.Columns.Add("pdn_tipo", typeof(string));
            dataTable.Columns.Add("placacaja", typeof(string));
            dataTable.Columns.Add("pdn_transporte", typeof(string));
            dataTable.Columns.Add("pdn_pedorigen", typeof(string));
            dataTable.Columns.Add("cnte_nombre", typeof(string));
            dataTable.Columns.Add("quejas", typeof(string));
            dataTable.Columns.Add("destino", typeof(string));
            dataTable.Columns.Add("problema", typeof(string));

            foreach (DataTable st in set.Tables)
            {
                if (st.TableName == "detalle_nal")
                {
                    foreach (DataRow rw in set.Tables["detalle_nal"].Rows)
                    {
                        DataTable dtQuejas = score_card_detalle_pedidos_quejas(rw["pdn_folio"].ToString().Trim(), area, trans);
                        string quej = "";
                        string prob = "";
                        if (dtQuejas.Rows.Count > 0)
                        {
                            foreach (DataRow rt in dtQuejas.Rows)
                            {
                                quej += quej + rt["que_folio"] + ", ";

                                DataTable dtProblemas = score_card_detalle_pedidos_quejas_problema(rt["que_folio"].ToString());

                                if (dtProblemas.Rows.Count > 0)
                                {
                                    foreach (DataRow rt2 in dtProblemas.Rows)
                                    {
                                        prob += prob + rt2["problema"] + ", ";
                                    }
                                }

                            }

                        }
                        quej = quej.TrimEnd(' ').TrimEnd(',').ToString();
                        prob = prob.TrimEnd(' ').TrimEnd(',').ToString();

                        dataTable.Rows.Add(rw["pdn_folio"].ToString().Trim(),
                            rw["cnte_clave"].ToString().Trim(),
                            rw["cve_subcli"].ToString().Trim(),
                            rw["pdn_fecha"].ToString().Trim(),
                            rw["vendedor"].ToString().Trim(),
                            rw["pdn_monto_total"].ToString().Trim(),
                            rw["pdn_observacion"].ToString().Trim(),
                            rw["pdn_tipo"].ToString().Trim(),
                            rw["placacaja"].ToString().Trim(),
                            rw["pdn_transporte"].ToString().Trim(),
                            rw["pdn_pedorigen"].ToString().Trim(),
                            rw["cnte_nombre"].ToString().Trim(),
                            quej, rw["cnte_ciudad"].ToString().Trim(), prob);
                    }
                }
                if (st.TableName == "detalle_exp")
                {
                    foreach (DataRow rw in set.Tables["detalle_exp"].Rows)
                    {
                        DataTable dtQuejas = score_card_detalle_pedidos_quejas(rw["pdn_folio"].ToString().Trim(), area, trans);
                        string quej = "";
                        string prob = "";
                        if (dtQuejas.Rows.Count > 0)
                        {
                            foreach (DataRow rt in dtQuejas.Rows)
                            {
                                quej += quej + rt["que_folio"] + ", ";

                                DataTable dtProblemas = score_card_detalle_pedidos_quejas_problema(rt["que_folio"].ToString());

                                if (dtProblemas.Rows.Count > 0)
                                {
                                    foreach (DataRow rt2 in dtProblemas.Rows)
                                    {
                                        prob += prob + rt2["problema"] + ", ";
                                    }
                                }

                            }

                        }

                        quej = quej.TrimEnd(' ').TrimEnd(',').ToString();
                        prob = prob.TrimEnd(' ').TrimEnd(',').ToString();
                        dataTable.Rows.Add(rw["pdn_folio"].ToString().Trim(),
                            rw["cnte_clave"].ToString().Trim(),
                            rw["cve_subcli"].ToString().Trim(),
                            rw["pdn_fecha"].ToString().Trim(),
                            rw["vendedor"].ToString().Trim(),
                            rw["pdn_monto_total"].ToString().Trim(),
                            rw["pdn_observacion"].ToString().Trim(),
                            rw["pdn_tipo"].ToString().Trim(),
                            rw["placacaja"].ToString().Trim(),
                            rw["pdn_transporte"].ToString().Trim(),
                            rw["pdn_pedorigen"].ToString().Trim(),
                            rw["cnte_nombre"].ToString().Trim(),
                            quej, rw["cnte_ciudad"].ToString().Trim(), prob);
                    }
                }
            }
            return dataTable;
        }
        public DataTable score_card_detalle_pedidos_quejas(string pedido, string area, string prov)
        {
            this.Abrir();
            SqlDataAdapter adapter;
            DataSet set = new DataSet();
            adapter = new SqlDataAdapter("select que_folio FROM tb_mstr_quejas_serv WHERE que_pedido like '%'+cast(RTRIM('" + pedido + "') as varchar)+'%' AND embtracli = '" + area + "' AND " +
                "que_transportista = '" + prov + "' ORDER BY que_folio", this.conn);
            adapter.Fill(set, "detalle_queja");
            //set.Tables["detalle_queja"].Columns.Add("problema", typeof(string));

            //string val = "";
            //foreach (DataRow rw in set.Tables["detalle_queja"].Rows)
            //{
            //    com = conn.CreateCommand();
            //    com.CommandText = "SELECT que_nombre FROM tb_det_queja_serv WHERE que_folio = '" + rw["que_folio"].ToString().Trim() + "'";
            //    read = com.ExecuteReader();
            //    if (read.HasRows)
            //    {
            //        while (read.Read())
            //        {
            //            val = read["resp_tipo"].ToString().Trim() + ", ";
            //        }

            //    }
            //    read.Close();
            //    read.Dispose();
            //    com.Dispose();

            //}

            this.Cerrar();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_folio", typeof(string));
            foreach (DataRow rw in set.Tables["detalle_queja"].Rows)
            {
                dataTable.Rows.Add(rw["que_folio"].ToString().Trim());
            }
            return dataTable;
        }
        public DataTable score_card_detalle_pedidos_quejas_problema(string folio)
        {
            this.Abrir();
            SqlDataAdapter adapter;
            DataSet set = new DataSet();
            adapter = new SqlDataAdapter("select que_nombre FROM tb_det_queja_serv WHERE que_folio = '" + folio + "'", this.conn);
            adapter.Fill(set, "problemas");
            //set.Tables["detalle_queja"].Columns.Add("problema", typeof(string));

            //string val = "";
            //foreach (DataRow rw in set.Tables["detalle_queja"].Rows)
            //{
            //    com = conn.CreateCommand();
            //    com.CommandText = "SELECT que_nombre FROM tb_det_queja_serv WHERE que_folio = '" + rw["que_folio"].ToString().Trim() + "'";
            //    read = com.ExecuteReader();
            //    if (read.HasRows)
            //    {
            //        while (read.Read())
            //        {
            //            val = read["resp_tipo"].ToString().Trim() + ", ";
            //        }

            //    }
            //    read.Close();
            //    read.Dispose();
            //    com.Dispose();

            //}

            this.Cerrar();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("problema", typeof(string));
            foreach (DataRow rw in set.Tables["problemas"].Rows)
            {
                dataTable.Rows.Add(rw["que_nombre"].ToString().Trim());
            }
            return dataTable;
        }


        //public DataTable score_card_embarques_quejas_totales_exp(string trans, string f_i, string f_f)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("prov_clave", typeof(string));
        //    dataTable.Columns.Add("prov_nombre", typeof(string));
        //    dataTable.Columns.Add("total", typeof(string));
        //    dataTable.Columns.Add("total_quejas", typeof(string));

        //    //string f_ia = Convert.ToDateTime(f_i).ToString("dd/MM/yyyy");
        //    //string f_fa = Convert.ToDateTime(f_f).ToString("dd/MM/yyyy");

        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();
        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }

        //    //this.com.CommandText = "SELECT distinct(que_clave), que_nombre, " +
        //    //    "(SELECT COUNT(que_clave) from tb_det_queja_serv C join tb_mstr_quejas_serv D ON C.que_folio = D.que_folio " +
        //    //    "where A.que_clave = C.que_clave and D.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND D.que_transportista = '" + trans + "' AND D.que_fumi_trans = 'T') AS CANTIDAD " +
        //    //    "from tb_det_queja_serv A join tb_mstr_quejas_serv B ON A.que_folio = B.que_folio " +
        //    //    "where B.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND B.que_transportista = '" + trans + "' AND B.que_fumi_trans = 'T'";
        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
        //            row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
        //            row["total"] = (object)this.read["total"].ToString().Trim();
        //            row["total_quejas"] = "0";
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();

        //    SqlDataAdapter adapter;
        //    if (trans == "0")
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY')", this.conn);
        //    }
        //    else
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' " +
        //            "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 and " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' " +
        //            "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "'", this.conn);
        //    }
        //    DataSet set = new DataSet();
        //    adapter.Fill(set, "quejas_totales");

        //    foreach (DataRow rw in dataTable.Rows)
        //    {
        //        bool fnd = false;
        //        foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "'"))
        //        {
        //            fnd = true;
        //            break;
        //        }
        //        if (fnd == true)
        //        {
        //            decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)", "prov_clave = '" + rw["prov_clave"].ToString() + "'"));
        //            rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
        //        }

        //        //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
        //        //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
        //        //    .Sum(r => r.Field<Int32>("cant_quejas"));

        //    }

        //    this.Cerrar();
        //    return dataTable;
        //}
        //public DataTable score_card_embarques_quejas_totales_mes_anterior_nacional(string trans, string f_i)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("prov_clave", typeof(string));
        //    dataTable.Columns.Add("prov_nombre", typeof(string));
        //    dataTable.Columns.Add("total", typeof(string));
        //    dataTable.Columns.Add("total_quejas", typeof(string));

        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();

        //    string[] fch_i_m_act = f_i.Split('/');
        //    string mes_ant = "";
        //    string ano_ant = "";
        //    if(fch_i_m_act[1] == "01") //si mes es 1 mes anterior igual a 12
        //    {
        //        mes_ant = "12";
        //        ano_ant = (Convert.ToInt32(fch_i_m_act[2].ToString()) - 1).ToString();//Convert.ToDateTime(f_i).AddYears(-1).Year.ToString();
        //    }
        //    else //si mes es cualquier otro seria el mes anterior al actual
        //    {
        //        mes_ant = (Convert.ToInt32(fch_i_m_act[1].ToString()) - 1).ToString().PadLeft(2, '0');
        //        ano_ant = fch_i_m_act[2].ToString();
        //    }
        //    Int32 dias_mes = DateTime.DaysInMonth(Convert.ToInt32(ano_ant), Convert.ToInt32(mes_ant));
        //    string fecha_i = "01/" + mes_ant + "/" + ano_ant;
        //    string fecha_f = dias_mes.ToString().PadLeft(2, '0') + "/" + mes_ant + "/" + ano_ant;

        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }

        //    //this.com.CommandText = "SELECT distinct(que_clave), que_nombre, " +
        //    //    "(SELECT COUNT(que_clave) from tb_det_queja_serv C join tb_mstr_quejas_serv D ON C.que_folio = D.que_folio " +
        //    //    "where A.que_clave = C.que_clave and D.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND D.que_transportista = '" + trans + "' AND D.que_fumi_trans = 'T') AS CANTIDAD " +
        //    //    "from tb_det_queja_serv A join tb_mstr_quejas_serv B ON A.que_folio = B.que_folio " +
        //    //    "where B.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND B.que_transportista = '" + trans + "' AND B.que_fumi_trans = 'T'";
        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
        //            row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
        //            row["total"] = (object)this.read["total"].ToString().Trim();
        //            row["total_quejas"] = "0";
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();

        //    SqlDataAdapter adapter;
        //    if (trans == "0")
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) > 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY')", this.conn);
        //    }
        //    else
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' " +
        //            "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' and B.que_transportista = '" + trans + "' AND B.que_tipo = 'NACIONAL'), 0) > 0", this.conn);
        //    }
        //    DataSet set = new DataSet();
        //    adapter.Fill(set, "quejas_totales");

        //    foreach (DataRow rw in dataTable.Rows)
        //    {
        //        //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
        //        //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
        //        //    .Sum(r => r.Field<Int32>("cant_quejas"));

        //        bool fnd = false;
        //        foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "'"))
        //        {
        //            fnd = true;
        //            break;
        //        }
        //        if (fnd == true)
        //        {
        //            decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)", "prov_clave = '" + rw["prov_clave"].ToString() + "'"));
        //            rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
        //        }


        //    }

        //    this.Cerrar();
        //    return dataTable;
        //}

        //public DataTable score_card_embarques_quejas_totales_mes_anterior_exportacion(string trans, string f_i)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("prov_clave", typeof(string));
        //    dataTable.Columns.Add("prov_nombre", typeof(string));
        //    dataTable.Columns.Add("total", typeof(string));
        //    dataTable.Columns.Add("total_quejas", typeof(string));

        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();

        //    string[] fch_i_m_act = f_i.Split('/');
        //    string mes_ant = "";
        //    string ano_ant = "";
        //    if (fch_i_m_act[1] == "01") //si mes es 1 mes anterior igual a 12
        //    {
        //        mes_ant = "12";
        //        ano_ant = (Convert.ToInt32(fch_i_m_act[2].ToString())-1).ToString();
        //    }
        //    else //si mes es cualquier otro seria el mes anterior al actual
        //    {
        //        mes_ant = (Convert.ToInt32(fch_i_m_act[1].ToString()) - 1).ToString().PadLeft(2, '0');
        //        ano_ant = fch_i_m_act[2].ToString();
        //    }
        //    Int32 dias_mes = DateTime.DaysInMonth(Convert.ToInt32(ano_ant), Convert.ToInt32(mes_ant));
        //    string fecha_i = "01/" + mes_ant + "/" + ano_ant;
        //    string fecha_f = dias_mes.ToString().PadLeft(2, '0') + "/" + mes_ant + "/" + ano_ant;

        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }

        //    //this.com.CommandText = "SELECT distinct(que_clave), que_nombre, " +
        //    //    "(SELECT COUNT(que_clave) from tb_det_queja_serv C join tb_mstr_quejas_serv D ON C.que_folio = D.que_folio " +
        //    //    "where A.que_clave = C.que_clave and D.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND D.que_transportista = '" + trans + "' AND D.que_fumi_trans = 'T') AS CANTIDAD " +
        //    //    "from tb_det_queja_serv A join tb_mstr_quejas_serv B ON A.que_folio = B.que_folio " +
        //    //    "where B.que_fechaemb >= '" + DateTime.Now.AddYears(-1).ToShortDateString() + "' AND B.que_transportista = '" + trans + "' AND B.que_fumi_trans = 'T'";
        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
        //            row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
        //            row["total"] = (object)this.read["total"].ToString().Trim();
        //            row["total_quejas"] = "0";
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();

        //    SqlDataAdapter adapter;
        //    if (trans == "0")
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY')", this.conn);
        //    }
        //    else
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' " +
        //            "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) > 0", this.conn);
        //    }
        //    DataSet set = new DataSet();
        //    adapter.Fill(set, "quejas_totales");

        //    foreach (DataRow rw in dataTable.Rows)
        //    {
        //        //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
        //        //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
        //        //    .Sum(r => r.Field<Int32>("cant_quejas"));
        //        bool fnd = false;
        //        foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "'"))
        //        {
        //            fnd = true;
        //            break;
        //        }
        //        if (fnd == true)
        //        {
        //            decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)", "prov_clave = '" + rw["prov_clave"].ToString() + "'"));
        //            rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
        //        }

        //    }

        //    this.Cerrar();
        //    return dataTable;
        //}

        //public DataTable score_card_embarques_quejas_totales_mes_actual_nacional(string trans, string f_i)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("prov_clave", typeof(string));
        //    dataTable.Columns.Add("prov_nombre", typeof(string));
        //    dataTable.Columns.Add("total", typeof(string));
        //    dataTable.Columns.Add("total_quejas", typeof(string));
        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();

        //    string[] fch_i_m_act = f_i.Split('/');
        //    string mes_act = "";
        //    string ano_act = "";

        //    mes_act = fch_i_m_act[1].ToString().PadLeft(2, '0');
        //    ano_act = fch_i_m_act[2].ToString();

        //    Int32 dias_mes = DateTime.DaysInMonth(Convert.ToInt32(ano_act), Convert.ToInt32(mes_act));
        //    string fecha_i = "01/" + mes_act + "/" + ano_act;
        //    string fecha_f = dias_mes.ToString().PadLeft(2, '0') + "/" + mes_act + "/" + ano_act;

        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }

        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
        //            row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
        //            row["total"] = (object)this.read["total"].ToString().Trim();
        //            row["total_quejas"] = "0";
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();

        //    SqlDataAdapter adapter;
        //    if (trans == "0")
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) > 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY')", this.conn);
        //    }
        //    else
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' " +
        //            "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' and B.que_transportista = '" + trans + "'  AND B.que_tipo = 'NACIONAL'), 0) > 0", this.conn);
        //    }
        //    DataSet set = new DataSet();
        //    adapter.Fill(set, "quejas_totales");

        //    foreach (DataRow rw in dataTable.Rows)
        //    {
        //        bool fnd = false;
        //        foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "'"))
        //        {
        //            fnd = true;
        //            break;
        //        }
        //        if (fnd == true)
        //        {
        //            decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)", "prov_clave = '" + rw["prov_clave"].ToString() + "'"));
        //            rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
        //        }

        //    }

        //    this.Cerrar();
        //    return dataTable;
        //}

        //public DataTable score_card_embarques_quejas_totales_mes_actual_exportacion(string trans, string f_i)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("prov_clave", typeof(string));
        //    dataTable.Columns.Add("prov_nombre", typeof(string));
        //    dataTable.Columns.Add("total", typeof(string));
        //    dataTable.Columns.Add("total_quejas", typeof(string));
        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();

        //    //string fch_i_m_act = Convert.ToDateTime(f_i).Month.ToString();
        //    //string mes_act = "";
        //    //string ano_act = "";

        //    //mes_act = Convert.ToInt32(fch_i_m_act).ToString().PadLeft(2, '0');
        //    //ano_act = Convert.ToDateTime(f_i).Year.ToString();

        //    string[] fch_i_m_act = f_i.Split('/');
        //    string mes_act = "";
        //    string ano_act = "";

        //    mes_act = fch_i_m_act[1].ToString().PadLeft(2, '0');
        //    ano_act = fch_i_m_act[2].ToString();

        //    Int32 dias_mes = DateTime.DaysInMonth(Convert.ToInt32(ano_act), Convert.ToInt32(mes_act));
        //    string fecha_i = "01/" + mes_act + "/" + ano_act;
        //    string fecha_f = dias_mes.ToString().PadLeft(2, '0') + "/" + mes_act + "/" + ano_act;

        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, count(*) as total " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' " +
        //            "group by A.prov_clave, C.prov_nombre";
        //    }

        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
        //            row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
        //            row["total"] = (object)this.read["total"].ToString().Trim();
        //            row["total_quejas"] = "0";
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();

        //    SqlDataAdapter adapter;
        //    if (trans == "0")
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY')", this.conn);
        //    }
        //    else
        //    {
        //        adapter = new SqlDataAdapter("select A.pdn_folio, A.prov_clave, C.prov_nombre, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' " +
        //            "and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + fecha_i + "' AND A.pdn_fecha <= '" + fecha_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_pedido like '%'+cast(A.pdn_folio as varchar)+'%' " +
        //            "AND B.que_fumi_trans = 'T' and B.que_transportista = '" + trans + "' AND B.que_tipo = 'EXPORTACION'), 0) > 0", this.conn);
        //    }
        //    DataSet set = new DataSet();
        //    adapter.Fill(set, "quejas_totales");

        //    foreach (DataRow rw in dataTable.Rows)
        //    {
        //        //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
        //        //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
        //        //    .Sum(r => r.Field<Int32>("cant_quejas"));
        //        bool fnd = false;
        //        foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "'"))
        //        {
        //            fnd = true;
        //            break;
        //        }
        //        if (fnd == true)
        //        {
        //            decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)", "prov_clave = '" + rw["prov_clave"].ToString() + "'"));
        //            rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
        //        }

        //    }

        //    this.Cerrar();
        //    return dataTable;
        //}

        //public DataTable score_card_total_perdidas_mes_anterior(string trans, string f_i)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("mn", typeof(string));
        //    dataTable.Columns.Add("usd", typeof(string));
        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();

        //    //string fch_i_m_act = Convert.ToDateTime(f_i).Month.ToString();
        //    string[] fch_i_m_act = f_i.Split('/');
        //    string mes_ant = "";
        //    string ano_ant = "";
        //    if (fch_i_m_act[1] == "01") //si mes es 1 mes anterior igual a 12
        //    {
        //        mes_ant = "12";
        //        ano_ant = fch_i_m_act[2].ToString();
        //    }
        //    else //si mes es cualquier otro seria el mes anterior al actual
        //    {
        //        mes_ant = (Convert.ToInt32(fch_i_m_act[2].ToString()) - 1).ToString().PadLeft(2, '0');
        //        ano_ant = fch_i_m_act[2].ToString();
        //    }
        //    Int32 dias_mes = DateTime.DaysInMonth(Convert.ToInt32(ano_ant), Convert.ToInt32(mes_ant));
        //    string fecha_i = "01/" + mes_ant + "/" + ano_ant;
        //    string fecha_f = dias_mes.ToString().PadLeft(2, '0') + "/" + mes_ant + "/" + ano_ant;

        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "SELECT ISNULL(sum(que_perdida_mn), 0) AS MN, ISNULL(sum(que_perdida_usd), 0) as USD FROM tb_mstr_quejas_serv WHERE que_fumi_trans = 'T' and que_estatus = 'A' AND " +
        //            "que_fechaemb >= '" + fecha_i + "' and que_fechaemb <= '" + fecha_f + "'";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "SELECT ISNULL(sum(que_perdida_mn), 0) AS MN, ISNULL(sum(que_perdida_usd), 0) as USD FROM tb_mstr_quejas_serv WHERE que_fumi_trans = 'T' and que_estatus = 'A' AND " +
        //            "que_fechaemb >= '" + fecha_i + "' and que_fechaemb <= '" + fecha_f + "' AND que_transportista = '" + trans + "'";
        //    }

        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["mn"] = (object)Convert.ToDecimal(this.read["MN"].ToString().Trim());
        //            row["usd"] = (object)Convert.ToDecimal(this.read["USD"].ToString().Trim());
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();
        //    this.Cerrar();
        //    return dataTable;
        //}

        //public DataTable score_card_total_perdidas_mes_actual(string trans, string f_i)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("mn", typeof(string));
        //    dataTable.Columns.Add("usd", typeof(string));
        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();

        //    string[] fch_i_m_act = f_i.Split('/');
        //    string mes_act = "";
        //    string ano_act = "";

        //    mes_act = Convert.ToInt32(fch_i_m_act[1]).ToString().PadLeft(2, '0');
        //    ano_act = fch_i_m_act[2].ToString();

        //    Int32 dias_mes = DateTime.DaysInMonth(Convert.ToInt32(ano_act), Convert.ToInt32(mes_act));
        //    string fecha_i = "01/" + mes_act + "/" + ano_act;
        //    string fecha_f = dias_mes.ToString().PadLeft(2, '0') + "/" + mes_act + "/" + ano_act;

        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "SELECT ISNULL(sum(que_perdida_mn), 0) AS MN, ISNULL(sum(que_perdida_usd), 0) as USD FROM tb_mstr_quejas_serv WHERE que_fumi_trans = 'T' and que_estatus = 'A' AND " +
        //            "que_fechaemb >= '" + fecha_i + "' and que_fechaemb <= '" + fecha_f + "'";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "SELECT ISNULL(sum(que_perdida_mn), 0) AS MN, ISNULL(sum(que_perdida_usd), 0) as USD FROM tb_mstr_quejas_serv WHERE que_fumi_trans = 'T' and que_estatus = 'A' AND " +
        //            "que_fechaemb >= '" + fecha_i + "' and que_fechaemb <= '" + fecha_f + "' AND que_transportista = '" + trans + "'";
        //    }

        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["mn"] = (object)Convert.ToDecimal(this.read["MN"].ToString().Trim());
        //            row["usd"] = (object)Convert.ToDecimal(this.read["USD"].ToString().Trim());
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();
        //    this.Cerrar();
        //    return dataTable;
        //}

        //public DataTable score_card_embarques_quejas_totales_caja_nacional(string trans, string f_i, string f_f)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("prov_clave", typeof(string));
        //    dataTable.Columns.Add("prov_nombre", typeof(string));
        //    dataTable.Columns.Add("placacaja", typeof(string));
        //    dataTable.Columns.Add("total", typeof(string));
        //    dataTable.Columns.Add("total_quejas", typeof(string));

        //    //string f_ia = Convert.ToDateTime(f_i).ToString("dd/MM/yyyy");
        //    //string f_fa = Convert.ToDateTime(f_f).ToString("dd/MM/yyyy");

        //    this.Abrir();
        //    this.com = this.conn.CreateCommand();
        //    if (trans == "0")
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 " + 
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') and A.placacaja <> '' " +
        //            "group by A.prov_clave, C.prov_nombre, A.placacaja " +
        //            "ORDER BY C.prov_nombre, A.placacaja";
        //    }
        //    else
        //    {
        //        this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.prov_clave = '" + trans + "' and A.placacaja <> '' " +
        //            "group by A.prov_clave, C.prov_nombre, A.placacaja " +
        //            "ORDER BY C.prov_nombre, A.placacaja";
        //    }

        //    this.read = this.com.ExecuteReader();
        //    if (this.read.HasRows)
        //    {
        //        while (this.read.Read())
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
        //            row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
        //            row["placacaja"] = (object)this.read["placacaja"].ToString().Trim();
        //            row["total"] = (object)this.read["total"].ToString().Trim();
        //            row["total_quejas"] = "0";
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    this.read.Close();
        //    this.read.Dispose();
        //    this.com.Dispose();

        //    DataTable dataTable2 = new DataTable();
        //    dataTable2.Columns.Add("prov_clave", typeof(string));
        //    dataTable2.Columns.Add("prov_nombre", typeof(string));
        //    dataTable2.Columns.Add("placacaja", typeof(string));
        //    dataTable2.Columns.Add("total", typeof(string));
        //    dataTable2.Columns.Add("total_quejas", typeof(string));
        //    string prov_clave = "";
        //    string placa = "";
        //    string total = "";
        //    int i = 0;
        //    foreach (DataRow rw in dataTable.Rows)
        //    {
        //        prov_clave = rw["prov_clave"].ToString();
        //        placa = rw["placacaja"].ToString();
        //        total = rw["total"].ToString();
        //        if (i == 0)
        //        {
        //            i++;
        //            DataRow row = dataTable2.NewRow();
        //            row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
        //            row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
        //            row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
        //            row["total"] = (object)rw["total"].ToString().Trim();
        //            row["total_quejas"] = "0";
        //            dataTable2.Rows.Add(row);
        //        }
        //        else
        //        {
        //            bool fnd = false;
        //            foreach (DataRow rx in dataTable2.Select("placacaja = '" + placa + "' AND prov_clave = '" + prov_clave + "'"))
        //            {
        //                fnd = true;
        //                rx["total"] = Convert.ToDecimal(rx["total"]) + Convert.ToDecimal(total);
        //            }
        //            if (fnd == false)
        //            {
        //                DataRow row = dataTable2.NewRow();
        //                row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
        //                row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
        //                row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
        //                row["total"] = (object)rw["total"].ToString().Trim();
        //                row["total_quejas"] = "0";
        //                dataTable2.Rows.Add(row);
        //            }
        //        }
        //    }

        //    SqlDataAdapter adapter;
        //    if (trans == "0")
        //    {
        //        adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " + 
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " + 
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' " +
        //            "and ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) > 0 " +
        //            "order by A.prov_clave, A.placacaja", this.conn);
        //    }
        //    else
        //    {
        //        adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
        //            "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) AS cant_quejas " +
        //            "from tb_mstr_pedidos_nal A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
        //            "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
        //            "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND A.placacaja <> '' " +
        //            "and ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
        //            "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'NACIONAL'), 0) > 0 " +
        //            "order by A.prov_clave, A.placacaja", this.conn);
        //    }
        //    DataSet set = new DataSet();
        //    adapter.Fill(set, "quejas_totales");

        //    foreach (DataRow rw in dataTable2.Rows)
        //    {
        //        //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
        //        //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
        //        //    .Sum(r => r.Field<Int32>("cant_quejas"));

        //        bool fnd = false;
        //        foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"))
        //        {
        //            fnd = true;
        //            break;
        //        }

        //        if (fnd == true)
        //        {
        //            decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)",
        //                "prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"));
        //                rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
        //        }

        //    }
        //    this.Cerrar();
        //    return dataTable2;
        //}

        public DataTable score_card_embarques_quejas_totales_caja_exportacion(string trans, string f_i, string f_f)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("prov_clave", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("placacaja", typeof(string));
            dataTable.Columns.Add("total", typeof(string));
            dataTable.Columns.Add("total_quejas", typeof(string));

            this.Abrir();
            this.com = this.conn.CreateCommand();
            if (trans == "0")
            {
                this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') and A.placacaja <> '' " +
                    "group by A.prov_clave, C.prov_nombre, A.placacaja " +
                    "ORDER BY C.prov_nombre, A.placacaja";
            }
            else
            {
                this.com.CommandText = "select A.prov_clave, C.prov_nombre, A.placacaja, count(*) as total " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "' and A.pdn_pedorigen = 0 " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.prov_clave = '" + trans + "' and A.placacaja <> '' " +
                    "group by A.prov_clave, C.prov_nombre, A.placacaja " +
                    "ORDER BY C.prov_nombre, A.placacaja";
            }

            this.read = this.com.ExecuteReader();
            if (this.read.HasRows)
            {
                while (this.read.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["prov_clave"] = (object)this.read["prov_clave"].ToString().Trim();
                    row["prov_nombre"] = (object)this.read["prov_nombre"].ToString().Trim();
                    row["placacaja"] = (object)this.read["placacaja"].ToString().Trim();
                    row["total"] = (object)this.read["total"].ToString().Trim();
                    row["total_quejas"] = "0";
                    dataTable.Rows.Add(row);
                }
            }
            this.read.Close();
            this.read.Dispose();
            this.com.Dispose();

            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("prov_clave", typeof(string));
            dataTable2.Columns.Add("prov_nombre", typeof(string));
            dataTable2.Columns.Add("placacaja", typeof(string));
            dataTable2.Columns.Add("total", typeof(string));
            dataTable2.Columns.Add("total_quejas", typeof(string));
            string prov_clave = "";
            string placa = "";
            string total = "";
            int i = 0;
            foreach (DataRow rw in dataTable.Rows)
            {
                prov_clave = rw["prov_clave"].ToString();
                placa = rw["placacaja"].ToString();
                total = rw["total"].ToString();
                if (i == 0)
                {
                    i++;
                    DataRow row = dataTable2.NewRow();
                    row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
                    row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
                    row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
                    row["total"] = (object)rw["total"].ToString().Trim();
                    row["total_quejas"] = "0";
                    dataTable2.Rows.Add(row);
                }
                else
                {
                    bool fnd = false;
                    foreach (DataRow rx in dataTable2.Select("placacaja = '" + placa + "' AND prov_clave = '" + prov_clave + "'"))
                    {
                        fnd = true;
                        rx["total"] = Convert.ToDecimal(rx["total"]) + Convert.ToDecimal(total);
                    }
                    if (fnd == false)
                    {
                        DataRow row = dataTable2.NewRow();
                        row["prov_clave"] = (object)rw["prov_clave"].ToString().Trim();
                        row["prov_nombre"] = (object)rw["prov_nombre"].ToString().Trim();
                        row["placacaja"] = (object)rw["placacaja"].ToString().Trim();
                        row["total"] = (object)rw["total"].ToString().Trim();
                        row["total_quejas"] = "0";
                        dataTable2.Rows.Add(row);
                    }
                }
            }

            SqlDataAdapter adapter;
            if (trans == "0")
            {
                adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave NOT IN ('MRLUCKY') AND A.placacaja <> '' " +
                    "and ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
                    "order by A.prov_clave, A.placacaja", this.conn);
            }
            else
            {
                adapter = new SqlDataAdapter("select A.prov_clave, C.prov_nombre, A.placacaja, " +
                    "ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) AS cant_quejas " +
                    "from tb_mstr_pedidos_exp A JOIN tb_cat_proveedor C ON A.prov_clave = C.prov_clave WHERE " +
                    "A.pdn_fecha >= '" + f_i + "' AND A.pdn_fecha <= '" + f_f + "'and A.pdn_pedorigen = 0 " +
                    "AND A.pdn_estatus <> 'C' AND A.prov_clave = '" + trans + "' AND A.placacaja <> '' " +
                    "and ISNULL((select count(B.que_folio) FROM tb_mstr_quejas_serv B WHERE B.que_caja = A.placacaja and B.que_fechaemb = A.pdn_fecha " +
                    "AND B.que_fumi_trans = 'T' AND B.que_tipo = 'EXPORTACION'), 0) > 0 " +
                    "order by A.prov_clave, A.placacaja", this.conn);
            }
            DataSet set = new DataSet();
            adapter.Fill(set, "quejas_totales");

            foreach (DataRow rw in dataTable2.Rows)
            {
                //decimal suma = set.Tables["quejas_totales"].AsEnumerable().
                //    Where(row => row.Field<string>("prov_clave") == rw["prov_clave"].ToString())
                //    .Sum(r => r.Field<Int32>("cant_quejas"));

                bool fnd = false;
                foreach (DataRow re in set.Tables["quejas_totales"].Select("prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"))
                {
                    fnd = true;
                    break;
                }

                if (fnd == true)
                {
                    decimal DecimalTotalPriceFiltered = Convert.ToDecimal(set.Tables["quejas_totales"].Compute("SUM(cant_quejas)",
                        "prov_clave = '" + rw["prov_clave"].ToString() + "' AND placacaja = '" + rw["placacaja"].ToString() + "'"));
                    rw["total_quejas"] = DecimalTotalPriceFiltered.ToString();
                }

            }
            this.Cerrar();
            return dataTable2;
        }

        public DataTable score_card_embarques_quejas_area_nacional_exportacion(string trans, string f_i, string f_f)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("que_transportista", typeof(string));
            dataTable.Columns.Add("prov_nombre", typeof(string));
            dataTable.Columns.Add("embtracli", typeof(string));
            dataTable.Columns.Add("total", typeof(string));
            dataTable.Columns.Add("area", typeof(string));
            dataTable.Columns.Add("tipo", typeof(string));

            //string f_ia = Convert.ToDateTime(f_i).ToString("dd/MM/yyyy");
            //string f_fa = Convert.ToDateTime(f_f).ToString("dd/MM/yyyy");

            this.Abrir();
            SqlDataAdapter adapter;
            if (trans == "0")
            {
                adapter = new SqlDataAdapter("select A.que_transportista, B.prov_nombre, A.embtracli, count(*) as total, " +
                    "case " +
                        "when A.embtracli = '1' then 'EMBARQUE' " +
                        "when A.embtracli = '2' then 'TRANSPORTISTA' " +
                        "when A.embtracli = '3' then 'CLIENTE' " +
                    "END AS Area, 'NACIONAL' AS tipo " +
                    "from tb_mstr_quejas_serv A " +
                    "JOIN tb_cat_proveedor B ON B.prov_clave = A.que_transportista " +
                    "WHERE A.que_fechaemb >= '" + f_i + "' AND que_fechaemb <= '" + f_f + "' AND que_fumi_trans = 'T' AND que_tipo = 'NACIONAL' " +
                    "AND A.embtracli <> 0 " +
                    "group by A.que_transportista, B.prov_nombre, A.embtracli " +
                    "UNION " +
                    "select A.que_transportista, B.prov_nombre, A.embtracli, count(*) as total, " +
                    "case " +
                        "when A.embtracli = '1' then 'EMBARQUE' " +
                        "when A.embtracli = '2' then 'TRANSPORTISTA' " +
                        "when A.embtracli = '3' then 'CLIENTE' " +
                    "END AS Area, 'EXPORTACION' AS tipo " +
                    "from tb_mstr_quejas_serv A " +
                    "JOIN tb_cat_proveedor B ON B.prov_clave = A.que_transportista " +
                    "WHERE A.que_fechaemb >= '" + f_i + "' AND A.que_fechaemb <= '" + f_f + "' AND A.que_fumi_trans = 'T' AND A.que_tipo = 'EXPORTACION' " +
                    "AND A.embtracli <> 0 " +
                    "group by A.que_transportista, B.prov_nombre, A.embtracli", this.conn);
            }
            else
            {
                adapter = new SqlDataAdapter("select A.que_transportista, B.prov_nombre, A.embtracli, count(*) as total, " +
                    "case " +
                        "when A.embtracli = '1' then 'EMBARQUE' " +
                        "when A.embtracli = '2' then 'TRANSPORTISTA' " +
                        "when A.embtracli = '3' then 'CLIENTE' " +
                    "END AS Area, 'NACIONAL' AS tipo " +
                    "from tb_mstr_quejas_serv A " +
                    "JOIN tb_cat_proveedor B ON B.prov_clave = A.que_transportista " +
                    "WHERE A.que_fechaemb >= '" + f_i + "' AND que_fechaemb <= '" + f_f + "' AND que_fumi_trans = 'T' AND que_tipo = 'NACIONAL' " +
                    "AND A.embtracli <> 0 AND A.que_transportista = '" + trans + "' " +
                    "group by A.que_transportista, B.prov_nombre, A.embtracli " +
                    "UNION " +
                    "select A.que_transportista, B.prov_nombre, A.embtracli, count(*) as total, " +
                    "case " +
                        "when A.embtracli = '1' then 'EMBARQUE' " +
                        "when A.embtracli = '2' then 'TRANSPORTISTA' " +
                        "when A.embtracli = '3' then 'CLIENTE' " +
                    "END AS Area, 'EXPORTACION' AS tipo " +
                    "from tb_mstr_quejas_serv A " +
                    "JOIN tb_cat_proveedor B ON B.prov_clave = A.que_transportista " +
                    "WHERE A.que_fechaemb >= '" + f_i + "' AND A.que_fechaemb <= '" + f_f + "' AND A.que_fumi_trans = 'T' AND A.que_tipo = 'EXPORTACION' " +
                    "AND A.embtracli <> 0 AND A.que_transportista = '" + trans + "' " +
                    "group by A.que_transportista, B.prov_nombre, A.embtracli", this.conn);
            }
            DataSet set = new DataSet();
            adapter.Fill(set, "quejas_totales");

            foreach (DataRow rw in set.Tables["quejas_totales"].Rows)
            {
                dataTable.Rows.Add(rw["que_transportista"].ToString().Trim(), rw["prov_nombre"].ToString().Trim(), rw["embtracli"].ToString().Trim(),
                    rw["total"].ToString().Trim(), rw["area"].ToString().Trim(), rw["tipo"].ToString().Trim());
            }

            this.Cerrar();
            return dataTable;
        }

        public bool queja_nota_credito_vencida(string fecha, string folio, string tipo)
        {
            string nalexp = "";
            this.Abrir();
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();

            adapter = new SqlDataAdapter("spSISEMPQuejasConsultaNotasCreditoNalExp", this.conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@folio", folio);
            adapter.Fill(dt);
            nalexp = (dt.Rows[0]["que_tipo"].ToString().Trim() == "N") ? "NAL" : "EXP";

            dt = new DataTable();

            adapter = new SqlDataAdapter("spSISEMPQuejasConsultaNotasCreditoNueveDiasDeshabilita", this.conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@fecha", fecha);
            adapter.SelectCommand.Parameters.AddWithValue("@folio", folio);
            adapter.SelectCommand.Parameters.AddWithValue("@tipo", tipo);
            adapter.SelectCommand.Parameters.AddWithValue("@nalexp", nalexp);
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string queja_soporte_devolucion_merma(string folio, string archivo)
        {
            this.Abrir();
            string str;
            try
            {
                SqlCommand com = new SqlCommand("spSISEMPQuejasSoporteMermaDevolucion", this.conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@folio", folio);
                com.Parameters.AddWithValue("@archivo", archivo);
                com.ExecuteNonQuery();
                com.Dispose();
                str = "1";
            }
            catch (SqlException ex)
            {
                str = "0";
            }
            this.Cerrar();
            return str;
        }

        public DataTable queja_soporte_verificar(string folio)
        {
            this.Abrir();
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();

            adapter = new SqlDataAdapter("spSISEMPQuejasSoporteMermaDevolucionVerificar", this.conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@folio", folio);
            adapter.Fill(dt);
            this.Cerrar();

            return dt;
        }


        //public string validar_ingreso_pedido(string pedido, string expnal, string clave, string producto)
        //{
        //    string str = "";
        //    this.Abrir();

        //    string val_tipo = "";
        //    com = conn.CreateCommand();
        //    com.CommandText = "SELECT resp_tipo FROM tb_cat_responsables WHERE resp_clave = '" + clave + "'";
        //    read = com.ExecuteReader();
        //    if (read.HasRows)
        //    {
        //        read.Read();
        //        val_tipo = read["resp_tipo"].ToString().Trim();
        //    }
        //    read.Close();
        //    read.Dispose();
        //    com.Dispose();

        //    this.com = this.conn.CreateCommand();
        //    if (expnal == "N")
        //        this.com.CommandText = "SELECT A.pdn_fecha FROM tb_mstr_pedidos_nal A JOIN tb_det_pedidos B " +
        //            "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo " +
        //            "WHERE A.pdn_folio = '" + pedido + "' AND A.pdn_tipo  = 'NAL' AND B.pdn_tipo = 'NAL' AND B.prod_clave = '" + producto + "'";
        //    else if (expnal == "E")
        //        this.com.CommandText = "SELECT A.pdn_fecha FROM tb_mstr_pedidos_exp A JOIN tb_det_pedidos B " +
        //            "ON A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo " +
        //            "WHERE A.pdn_folio = '" + pedido + "' AND A.pdn_tipo  = 'EXP' AND B.pdn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";//this.com.CommandText = "SELECT pdn_folio FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + pedido + "'";
        //    else
        //        str = "0";
        //    if (str != "0")
        //    {
        //        try
        //        {
        //            this.read = this.com.ExecuteReader();
        //            if (this.read.HasRows)//cuando lo encuentra dentro de los pedidos
        //            {
        //                this.read.Read();
        //                str = this.read["pdn_folio"].ToString().Trim();
        //            }
        //            else //lo busca dentro de las facturas
        //            {
        //                read.Close();
        //                read.Dispose();
        //                if (expnal == "N")
        //                    this.com.CommandText = "SELECT A.fcn_folio, C.pdn_fecha FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
        //                        "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
        //                        "WHERE A.fcn_folio = '" + pedido + "' AND AND A.fcn_lugar  = '" + val_tipo + "' AND B.fcn_tipo = '" + val_tipo + "' AND B.prod_clave = '" + producto + "'";
        //                else if (expnal == "E")
        //                    this.com.CommandText = "SELECT A.fcn_folio FROM tb_mstr_facturas_nal A JOIN tb_det_facturas B " +
        //                        "ON A.fcn_folio = B.fcn_folio AND A.fcn_lugar = B.fcn_tipo " +
        //                        "WHERE A.fcn_folio = '" + pedido + "' AND AND A.fcn_lugar  = 'EXP' AND B.fcn_tipo = 'EXP' AND B.prod_clave = '" + producto + "'";
        //                this.read = this.com.ExecuteReader();
        //                if (this.read.HasRows)//cuando lo encuentra dentro de los pedidos
        //                {
        //                    this.read.Read();
        //                    str = this.read["pdn_folio"].ToString().Trim();
        //                }
        //                else //lo busca dentro de las facturas
        //                    str = "0";
        //            }
        //            read.Close();
        //            read.Dispose();
        //        }
        //        catch (SqlException ex)
        //        {
        //            this.Cerrar();
        //            str = "0";
        //        }
        //    }
        //    //}
        //    this.Cerrar();
        //    return str;
        //}





    }
}