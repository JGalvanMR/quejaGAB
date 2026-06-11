using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace queja
{
    public partial class grafica_rechazo : System.Web.UI.Page
    {
        private DataTable dtAnterior = new DataTable();
        private DataTable dtActual = new DataTable();
        private DataTable dtSiguiente = new DataTable();
        private DataTable dtProdAnterior = new DataTable();
        private DataTable dtProdActual = new DataTable();
        private DataTable dtProdSiguiente = new DataTable();
        private DataTable dtProductos = new DataTable();

        private DataTable dtClientes = new DataTable();
        private DataTable dtSucursales = new DataTable();

        private conectasql con = new conectasql();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            
            this.dtProdAnterior.Columns.Add("id_producto", typeof(string));
            this.dtProdAnterior.Columns.Add("ptcptp", typeof(string));
            this.dtProdAnterior.Columns.Add("producidas", typeof(string));
            this.dtProdAnterior.Columns.Add("rechazadas", typeof(string));
            this.dtProdAnterior.Columns.Add("recibidas", typeof(string));
            //this.dtProdAnterior.Columns.Add("facturadas", typeof(string));
            //this.dtProdAnterior.Columns.Add("porcentaje", typeof(string));

            this.dtProdActual.Columns.Add("id_producto", typeof(string));
            this.dtProdActual.Columns.Add("ptcptp", typeof(string));
            this.dtProdActual.Columns.Add("producidas", typeof(string));
            this.dtProdActual.Columns.Add("rechazadas", typeof(string));
            this.dtProdActual.Columns.Add("recibidas", typeof(string));
            //this.dtProdActual.Columns.Add("facturadas", typeof(string));

            this.dtProdSiguiente.Columns.Add("id_producto", typeof(string));
            this.dtProdSiguiente.Columns.Add("ptcptp", typeof(string));
            this.dtProdSiguiente.Columns.Add("producidas", typeof(string));
            this.dtProdSiguiente.Columns.Add("rechazadas", typeof(string));
            this.dtProdSiguiente.Columns.Add("recibidas", typeof(string));
            //this.dtProdSiguiente.Columns.Add("facturadas", typeof(string));

            this.dtProductos.Columns.Add("id_producto", typeof(string));
            this.dtProductos.Columns.Add("producto", typeof(string));
            this.dtProductos.Columns.Add("mes_anterior", typeof(string));
            this.dtProductos.Columns.Add("mes_actual", typeof(string));
            this.dtProductos.Columns.Add("mes_siguiente", typeof(string));
            //this.dtProductos.Columns.Add("fact_ant", typeof(string));
            //this.dtProductos.Columns.Add("fact_act", typeof(string));
            //this.dtProductos.Columns.Add("fact_sig", typeof(string));

            if (!Page.IsPostBack)
            {
                SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
                sqlConnection.Open();
                SqlDataAdapter adapter1 = new SqlDataAdapter("select RTRIM(prod_clave) AS prod_clave, RTRIM(REPLACE(prod_nombre, '''', '*')) AS prod_nombre from tb_cat_producto where prod_tipo in ('PTC', 'PTP') and prod_nombre <> '' ORDER BY prod_nombre", sqlConnection);
                DataSet ds = new DataSet();
                adapter1.Fill(ds, "productos");
                Session["produces"] = ds.Tables["productos"];
                sqlConnection.Close();

                this.dtClientes = this.con.cargarclientes();
                this.ddlCliente.DataSource = (object)this.dtClientes;
                this.ddlCliente.DataTextField = "CliRSocial";
                this.ddlCliente.DataValueField = "CliCod";
                this.ddlCliente.DataBind();
            }
            
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public string trae_mes(int mes)
        {
            string str = "";
            switch (mes)
            {
                case 1:
                    str = "Enero";
                    break;
                case 2:
                    str = "Febrero";
                    break;
                case 3:
                    str = "Marzo";
                    break;
                case 4:
                    str = "Abril";
                    break;
                case 5:
                    str = "Mayo";
                    break;
                case 6:
                    str = "Junio";
                    break;
                case 7:
                    str = "Julio";
                    break;
                case 8:
                    str = "Agosto";
                    break;
                case 9:
                    str = "Septiembre";
                    break;
                case 10:
                    str = "Octubre";
                    break;
                case 11:
                    str = "Noviembre";
                    break;
                case 12:
                    str = "Diciembre";
                    break;
            }
            return str;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("reportes.aspx");
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*this.dtSucursales.Clear();
            this.dtSucursales = this.con.cargarsucursales(this.ddlCliente.SelectedValue.ToString(), this.lblCedis.Text);
            this.ddlSucursales.DataSource = (object)this.dtSucursales;
            this.ddlSucursales.DataTextField = "subcli_nombre";
            this.ddlSucursales.DataValueField = "subcli_folio";
            this.ddlSucursales.DataBind();
            this.upnSucursales.Update();*/
        }

        protected void btnBuscaOrden_Click(object sender, EventArgs e)
        {
            /*string selectedValue1 = this.ddlCliente.SelectedValue;
            string text5 = this.ddlCliente.SelectedItem.Text;
            string selectedValue2 = "";
            string text6 = "";
            if (selectedValue1 != "")
            {
                selectedValue2 = this.ddlSucursales.SelectedValue;
                text6 = this.ddlSucursales.SelectedItem.Text;
            }*/

            string selectedValue1 = "";
            string selectedValue2 = "";
            
            datos(selectedValue1, selectedValue2);
        }

        public void datos(string cliente, string subcliente)
        {
            //string str1 = this.ddlMes.SelectedValue.ToString();
            ////MES ANTERIOR
            //string str2 = Convert.ToInt32(str1) != 1 ? this.trae_mes(Convert.ToInt32(str1) - 1) : "Diciembre";
            string str2_a = "";
            ////FIN MES ANTERIOR
            //string str3 = Convert.ToInt32(str1) != 12 ? this.trae_mes(Convert.ToInt32(str1) + 1) : "Enero";
            //string str4 = this.trae_mes(Convert.ToInt32(str1));
            //this.txtAnterior.Value = str2;
            //this.txtActual.Value = str4;
            //this.txtSiguiente.Value = str3;
            //this.udpMeses.Update();
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            SqlCommand sqlCommand = new SqlCommand();
            //int num1;
            //string str5;
            //if (!(str2 == "Diciembre"))
            //{
            //    num1 = DateTime.Now.Year;
            //    str5 = num1.ToString();
            //    str2_a = str1.PadLeft(2, '0');
            //}
            //else
            //{
            //    num1 = DateTime.Now.Year - 1;
            //    str5 = num1.ToString();
            //    str2_a = "12";
            //}
            //string str6 = str5;
            //num1 = DateTime.Now.Year;
            //string str7 = num1.ToString();
            //string str8;
            //if (!(str3 == "Enero"))
            //{
            //    num1 = DateTime.Now.Year;
            //    str8 = num1.ToString();
            //}
            //else
            //{
            //    num1 = DateTime.Now.Year + 1;
            //    str8 = num1.ToString();
            //}
            //string str9 = str8;

            string str1 = this.ddlMes.SelectedValue.ToString();
            string num1 = DateTime.Now.Year.ToString();

            string str6 = "";

            //mes anterior
            if (str1 == "1")
            {
                str2_a = "12";
                str6 = DateTime.Now.AddYears(-1).Year.ToString();
            }
            else
            {
                str2_a = (Convert.ToInt32(str1) - 1).ToString().PadLeft(2, '0');
                str6 = DateTime.Now.Year.ToString();
            }

            //mes anterior
            string str10 = "01/" + str2_a + "/" + str6;

            Int32 dias_mes = DateTime.DaysInMonth(Convert.ToInt32(str6), Convert.ToInt32(str2_a));

            //string dateTime = Convert.ToDateTime(str10).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
            //string shortDateString1 = dateTime;
            string str11 = dias_mes.ToString() + "/" + str2_a + "/" + str6;
            //fin mes anterior

            //mes seleccionado
            string str12 = "01/" + str1.PadLeft(2, '0') + "/" + num1;

            Int32 dias_mes_act = DateTime.DaysInMonth(Convert.ToInt32(num1), Convert.ToInt32(str1));

            //string dateTime = Convert.ToDateTime(str10).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
            //string shortDateString1 = dateTime;
            string str13 = dias_mes_act.ToString() + "/" + str1.PadLeft(2, '0') + "/" + num1;
            //fin mes seleccionado

            //mes siguiente
            Int32 mes_sig = Convert.ToInt32(str1) + 1;
            if (Convert.ToInt32(str1) == 12)
            {
                mes_sig = 1;
                num1 = (Convert.ToDecimal(num1) + 1).ToString();
            }
            else
            {
                mes_sig = Convert.ToInt32(str1) + 1;
            }

            string str14 = "01/" + mes_sig.ToString().PadLeft(2, '0') + "/" + num1.ToString();

            Int32 dias_mes_sig = DateTime.DaysInMonth(Convert.ToInt32(num1), Convert.ToInt32(mes_sig));

            //string dateTime = Convert.ToDateTime(str10).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
            //string shortDateString1 = dateTime;
            string str15 = dias_mes_sig.ToString() + "/" + mes_sig.ToString().PadLeft(2, '0') + "/" + num1.ToString();

            //con.correo_error("Fecha inicial: " + str14 + " Fecha fin: " + str15);
            //fin mes siguiente

            this.txtAnterior.Value = trae_mes(Convert.ToInt32(str2_a));
            this.txtActual.Value = trae_mes(Convert.ToInt32(str1));
            this.txtSiguiente.Value = trae_mes(Convert.ToInt32(mes_sig));
            this.udpMeses.Update();

            //dateTime = Convert.ToDateTime(str11).ToString("dd/MM/yyyy");
            //dateTime = Convert.ToDateTime(dateTime).AddMonths(1).ToString("dd/MM/yyyy");
            //dateTime = Convert.ToDateTime(dateTime).AddDays(-1).ToString("dd/MM/yyyy");
            //string shortDateString2 = Convert.ToDateTime(dateTime).ToString("dd/MM/yyyy");
            //string str12 = "01/" + str3 + "/" + str9;
            //dateTime = Convert.ToDateTime(str12).ToString("dd/MM/yyyy");
            //dateTime = Convert.ToDateTime(dateTime).AddMonths(1).ToString("dd/MM/yyyy");
            //dateTime = Convert.ToDateTime(dateTime).AddDays(-1).ToString("dd/MM/yyyy");
            //string shortDateString3 = Convert.ToDateTime(dateTime).ToString("dd/MM/yyyy");
            sqlConnection.Open();

            //16/07/2021
            //sacar cantidad de cajas de ventas facturadas mes anterior
            //SqlCommand command3vfmant = sqlConnection.CreateCommand();
            //string qryvfmant = "SELECT A.prod_clave, ISNULL(sum(A.fcn_num_unidades), 0) as fcn_num_unidades, C.prod_nombre " +
            //    "FROM tb_det_facturas A " +
            //    "JOIN tb_mstr_facturas_nal B ON A.fcn_folio = B.fcn_folio and A.fcn_tipo = B.fcn_tipo " +
            //    "JOIN tb_cat_producto C ON C.prod_clave = A.prod_clave " +
            //    "JOIN tb_cat_cliente D ON D.cnte_clave = B.cnte_clave " +
            //    "join tb_cat_linea E ON E.lin_clave = A.lin_clave " +
            //    "WHERE (B.cnte_clave between '000000' and 'ZZZZZZ') and " +
            //    "(B.fcn_fecha between '" + str10 + "' and '" + str11 + "') and " +
            //    "B.fcn_estatus <> 'C' /*AND B.fcn_tipo = 'EXP'*/ " +
            //    "AND A.fcn_tipo <> 'TRA' " +
            //    "group by A.lin_clave, A.prod_clave, C.prod_nombre " +
            //    "order by A.lin_clave, A.prod_clave";
            //SqlDataAdapter adaptervfmant = new SqlDataAdapter(qryvfmant, sqlConnection);
            //DataSet dsvfmant = new DataSet();
            //adaptervfmant.Fill(dsvfmant, "vfmesanterior");

            //fin de sacar cantidad de cajas de ventas facturadas mes anterior

            SqlCommand command1 = sqlConnection.CreateCommand();

            //CALCULO MES ANTERIOR
            string qry = "";
            if (cliente == "" && subcliente == "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str10 + "' AND B.que_fechaemb <= '" + str11 + "' AND B.que_status = 'A' GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            else if (cliente != "" && subcliente == "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str10 + "' AND B.que_fechaemb <= '" + str11 + "' AND B.que_status in ('A', 'T') " +
                    "AND B.que_cliprim = '" + cliente + "' " + 
                    "GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            else if (cliente != "" && subcliente != "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str10 + "' AND B.que_fechaemb <= '" + str11 + "' AND B.que_status in ('A', 'T') " +
                    "AND B.que_cliprim = '" + cliente + "' AND B.subcli_folio = '" + subcliente + "' " +
                    "GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            command1.CommandText = qry;
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    DataRow row = this.dtProdAnterior.NewRow();
                    row["id_producto"] = (object)sqlDataReader1["id_producto"].ToString().Trim();
                    row["ptcptp"] = (object)sqlDataReader1["qud_ptcptp"].ToString().Trim();
                    row["rechazadas"] = (object)sqlDataReader1["rechazadas"].ToString().Trim();
                    row["recibidas"] = (object)sqlDataReader1["recibidas"].ToString().Trim();

                    //foreach (DataRow rwvfmant in dsvfmant.Tables["vfmesanterior"].Select("prod_clave = '" + (object)sqlDataReader1["id_producto"].ToString().Trim() + "'"))
                    //{
                    //    row["facturadas"] = rwvfmant["fcn_num_unidades"].ToString();
                    //}

                    this.dtProdAnterior.Rows.Add(row);
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();



            string _reci_anterior = "";
            //CALCULO DE CAJAS RECIBIDAS POR MES ACTUAL
            command1 = sqlConnection.CreateCommand();
            qry = "SELECT SUM(A.cajas) AS recibidas FROM tb_det_embarque A JOIN tb_mstr_embarque B ON A.emb_folio = B.emb_folio AND A.emb_tipo = B.emb_tipo " +
                "WHERE B.fecha_cap >= '" + str10 + "' AND B.fecha_cap <= '" + str11 + "' AND A.Estatus = 'A'";
            command1.CommandText = qry;
            sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    _reci_anterior = sqlDataReader1["recibidas"].ToString();
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();

            //con.correo_error(qry);

            ////16/07/2021
            ////sacar cantidad de cajas de ventas facturadas mes actual
            //SqlCommand command3vfmact = sqlConnection.CreateCommand();
            //string qryvfmact = "SELECT A.prod_clave, ISNULL(sum(A.fcn_num_unidades), 0) as fcn_num_unidades, C.prod_nombre " +
            //    "FROM tb_det_facturas A " +
            //    "JOIN tb_mstr_facturas_nal B ON A.fcn_folio = B.fcn_folio and A.fcn_tipo = B.fcn_tipo " +
            //    "JOIN tb_cat_producto C ON C.prod_clave = A.prod_clave " +
            //    "JOIN tb_cat_cliente D ON D.cnte_clave = B.cnte_clave " +
            //    "join tb_cat_linea E ON E.lin_clave = A.lin_clave " +
            //    "WHERE (B.cnte_clave between '000000' and 'ZZZZZZ') and " +
            //    "(B.fcn_fecha between '" + str12 + "' and '" + str13 + "') and " +
            //    "B.fcn_estatus <> 'C' /*AND B.fcn_tipo = 'EXP'*/ " +
            //    "AND A.fcn_tipo <> 'TRA' " +
            //    "group by A.lin_clave, A.prod_clave, C.prod_nombre " +
            //    "order by A.lin_clave, A.prod_clave";
            //SqlDataAdapter adaptervfmact = new SqlDataAdapter(qryvfmact, sqlConnection);
            //DataSet dsvfmact = new DataSet();
            //adaptervfmact.Fill(dsvfmact, "vfmesactual");

            //fin de sacar cantidad de cajas de ventas facturadas mes actual

            command1.Dispose();
            SqlCommand command2 = sqlConnection.CreateCommand();
            if (cliente == "" && subcliente == "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str12 + "' AND B.que_fechaemb <= '" + str13 + "' AND B.que_status in ('A', 'T') GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            else if (cliente != "" && subcliente == "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str12 + "' AND B.que_fechaemb <= '" + str13 + "' AND B.que_status in ('A', 'T') " +
                    "AND B.que_cliprim = '" + cliente + "' " +
                    "GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            else if (cliente != "" && subcliente != "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str12 + "' AND B.que_fechaemb <= '" + str13 + "' AND B.que_status in ('A', 'T') " +
                    "AND B.que_cliprim = '" + cliente + "' AND B.subcli_folio = '" + subcliente + "' " +
                    "GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            
            command2.CommandText = qry;
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    DataRow row = this.dtProdActual.NewRow();
                    row["id_producto"] = (object)sqlDataReader2["id_producto"].ToString().Trim();
                    row["ptcptp"] = (object)sqlDataReader2["qud_ptcptp"].ToString().Trim();
                    row["rechazadas"] = (object)sqlDataReader2["rechazadas"].ToString().Trim();
                    row["recibidas"] = (object)sqlDataReader2["recibidas"].ToString().Trim();

                    //foreach (DataRow rwvfmact in dsvfmact.Tables["vfmesactual"].Select("prod_clave = '" + (object)sqlDataReader2["id_producto"].ToString().Trim() + "'"))
                    //{
                    //    row["facturadas"] = rwvfmact["fcn_num_unidades"].ToString();
                    //}

                    this.dtProdActual.Rows.Add(row);
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();



            string _reci_actual = "";
            //CALCULO DE CAJAS RECIBIDAS POR MES ACTUAL
            command1 = sqlConnection.CreateCommand();
            qry = "SELECT SUM(A.cajas) AS recibidas FROM tb_det_embarque A JOIN tb_mstr_embarque B ON A.emb_folio = B.emb_folio AND A.emb_tipo = B.emb_tipo " +
                "WHERE B.fecha_cap >= '" + str12 + "' AND B.fecha_cap <= '" + str13 + "' AND A.Estatus = 'A'";
            command1.CommandText = qry;
            sqlDataReader2 = command1.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    _reci_actual = sqlDataReader2["recibidas"].ToString();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();

            //con.correo_error(qry);

            ////16/07/2021
            ////sacar cantidad de cajas de ventas facturadas mes siguiente
            //SqlCommand command3vfmsig = sqlConnection.CreateCommand();
            //string qryvfmsig = "SELECT A.prod_clave, ISNULL(sum(A.fcn_num_unidades), 0) as fcn_num_unidades, C.prod_nombre " +
            //    "FROM tb_det_facturas A " +
            //    "JOIN tb_mstr_facturas_nal B ON A.fcn_folio = B.fcn_folio and A.fcn_tipo = B.fcn_tipo " +
            //    "JOIN tb_cat_producto C ON C.prod_clave = A.prod_clave " +
            //    "JOIN tb_cat_cliente D ON D.cnte_clave = B.cnte_clave " +
            //    "join tb_cat_linea E ON E.lin_clave = A.lin_clave " +
            //    "WHERE (B.cnte_clave between '000000' and 'ZZZZZZ') and " +
            //    "(B.fcn_fecha between '" + str14 + "' and '" + str15 + "') and " +
            //    "B.fcn_estatus <> 'C' /*AND B.fcn_tipo = 'EXP'*/ " +
            //    "AND A.fcn_tipo <> 'TRA' " +
            //    "group by A.lin_clave, A.prod_clave, C.prod_nombre " +
            //    "order by A.lin_clave, A.prod_clave";
            //SqlDataAdapter adaptervfmsig = new SqlDataAdapter(qryvfmsig, sqlConnection);
            //DataSet dsvfmsig = new DataSet();
            //adaptervfmsig.Fill(dsvfmsig, "vfmessiguiente");

            //fin de sacar cantidad de cajas de ventas facturadas mes actual

            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            SqlCommand command3 = sqlConnection.CreateCommand();
            
            if (cliente == "" && subcliente == "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str14 + "' AND B.que_fechaemb <= '" + str15 + "' AND B.que_status in ('A', 'T') GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            else if (cliente != "" && subcliente == "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str14 + "' AND B.que_fechaemb <= '" + str15 + "' AND B.que_status in ('A', 'T') " +
                    "AND B.que_cliprim = '" + cliente + "' " +
                    "GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            else if (cliente != "" && subcliente != "")
            {
                qry = "SELECT A.id_producto as id_producto, A.qud_ptcptp, SUM(qud_cantrecha) AS rechazadas, SUM(qud_cantreci) AS recibidas FROM tb_det_quejas A " +
                    "JOIN tb_mstr_quejas B ON A.que_folio = B.que_folio WHERE B.que_fechaemb >= '" + str14 + "' AND B.que_fechaemb <= '" + str15 + "' AND B.que_status in ('A', 'T') " +
                    "AND B.que_cliprim = '" + cliente + "' AND B.subcli_folio = '" + subcliente + "' " +
                    "GROUP BY A.id_producto, A.nom_producto, A.qud_ptcptp ORDER BY A.nom_producto";
            }
            command3.CommandText = qry;
            SqlDataReader sqlDataReader3 = command3.ExecuteReader();
            if (sqlDataReader3.HasRows)
            {
                while (sqlDataReader3.Read())
                {
                    DataRow row = this.dtProdSiguiente.NewRow();
                    row["id_producto"] = (object)sqlDataReader3["id_producto"].ToString().Trim();
                    row["ptcptp"] = (object)sqlDataReader3["qud_ptcptp"].ToString().Trim();
                    row["rechazadas"] = (object)sqlDataReader3["rechazadas"].ToString().Trim();
                    row["recibidas"] = (object)sqlDataReader3["recibidas"].ToString().Trim();

                    //foreach (DataRow rwvfmsig in dsvfmsig.Tables["vfmessiguiente"].Select("prod_clave = '" + (object)sqlDataReader3["id_producto"].ToString().Trim() + "'"))
                    //{
                    //    row["facturadas"] = rwvfmsig["fcn_num_unidades"].ToString();
                    //}

                    this.dtProdSiguiente.Rows.Add(row);
                }
            }
            sqlDataReader3.Close();
            sqlDataReader3.Dispose();
            command3.Dispose();

            string _reci_siguiente = "0";
            //CALCULO DE CAJAS RECIBIDAS POR MES ACTUAL
            command1 = sqlConnection.CreateCommand();
            qry = "SELECT ISNULL(SUM(A.cajas), 0) AS recibidas FROM tb_det_embarque A JOIN tb_mstr_embarque B ON A.emb_folio = B.emb_folio AND A.emb_tipo = B.emb_tipo " +
                "WHERE B.fecha_cap >= '" + str14 + "' AND B.fecha_cap <= '" + str15 + "' AND A.Estatus = 'A'";
            command1.CommandText = qry;
            sqlDataReader3 = command1.ExecuteReader();
            if (sqlDataReader3.HasRows)
            {
                while (sqlDataReader3.Read())
                {
                    _reci_siguiente = sqlDataReader3["recibidas"].ToString();
                }
            }
            sqlDataReader3.Close();
            sqlDataReader3.Dispose();

            //con.correo_error(qry);

            foreach (DataRow row in (InternalDataCollectionBase)this.dtProdAnterior.Rows)
            {
                SqlCommand command4 = sqlConnection.CreateCommand();
                if (row["ptcptp"].ToString() == "PTP")
                    command4.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE cve_prod = '" + row["id_producto"] + "' AND fecha >= '" + str10 + "' AND fecha <= '" + str11 + "'";
                if (row["ptcptp"].ToString() == "PTC")
                    command4.CommandText = "SELECT SUM(A.rptd_cantidad) AS num_cajas FROM tb_det_recepcion_pt A JOIN tb_mstr_recepcion_pt B ON A.rpt_recibo = B.rpt_recibo WHERE B.rpt_fecha >= '" + str10 + "' AND B.rpt_fecha <= '" + str11 + "' AND A.prod_clave = '" + row["id_producto"] + "' AND B.rpt_estatus not in ('F', 'C') and B.rpt_tipo = 'CM'";
                SqlDataReader sqlDataReader4 = command4.ExecuteReader();
                if (sqlDataReader4.HasRows)
                {
                    while (sqlDataReader4.Read())
                        row["producidas"] = sqlDataReader4["num_cajas"].ToString().Trim() == "" ? (object)"0" : (object)sqlDataReader4["num_cajas"].ToString().Trim();
                }
                sqlDataReader4.Close();
                sqlDataReader4.Dispose();
                command4.Dispose();
            }

            //CALCULO DE PRODUCIDAS MES ANTERIOR
            string _prod_anterior = "";
            SqlCommand command4_a = sqlConnection.CreateCommand();
            //command4_a.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE fecha >= '" + str10 + "' AND fecha <= '" + str11 + "'";
            command4_a.CommandText = "SELECT SUM(hrp_num_unidades) AS num_cajas FROM tb_hist_recepcion WHERE hrp_fecha >= '" + str10 + "' AND hrp_fecha <= '" + str11 + "' " +
                "and hrp_estatus <> 'C' and hrp_tipo_recepcion = 'PTC' and hrp_situacion = 'CM'";
            SqlDataReader sqlDataReader4_a = command4_a.ExecuteReader();
            if (sqlDataReader4_a.HasRows)
            {
                while (sqlDataReader4_a.Read())
                    _prod_anterior = sqlDataReader4_a["num_cajas"].ToString().Trim() == "" ? "0" : sqlDataReader4_a["num_cajas"].ToString().Trim();
            }
            sqlDataReader4_a.Close();
            sqlDataReader4_a.Dispose();

            command4_a = sqlConnection.CreateCommand();
            //command4_a.CommandText = "SELECT SUM(A.rptd_cantidad) AS num_cajas FROM tb_det_recepcion_pt A JOIN tb_mstr_recepcion_pt B ON A.rpt_recibo = B.rpt_recibo WHERE B.rpt_fecha >= '" + str10 + "' AND B.rpt_fecha <= '" + str11 + "' AND B.rpt_estatus not in ('F', 'C') and B.rpt_tipo = 'CM'";
            command4_a.CommandText = "Select SUM(E.fodp_unidades) as num_cajas " +
                "FROM TB_MSTR_ORDENES_PROD A, tb_det_final_odp E " +
                "WHERE A.ordp_fecha >= '" + str10 + "' and A.ordp_fecha <= '" + str11 + "' " +
                "and A.ordp_estatus <> 'C' AND A.ORDP_FOLIO = E.ORDP_FOLIO";
            sqlDataReader4_a = command4_a.ExecuteReader();
            if (sqlDataReader4_a.HasRows)
            {
                string val_1 = "";
                while (sqlDataReader4_a.Read())
                    val_1 = (sqlDataReader4_a["num_cajas"].ToString().Trim() == "") ? "0" : sqlDataReader4_a["num_cajas"].ToString().Trim();
                _prod_anterior = Math.Abs(Convert.ToDecimal(_prod_anterior) + Convert.ToDecimal(val_1)).ToString();
            }
            sqlDataReader4_a.Close();
            sqlDataReader4_a.Dispose();
            command4_a.Dispose();

            foreach (DataRow row in (InternalDataCollectionBase)this.dtProdActual.Rows)
            {
                SqlCommand command4 = sqlConnection.CreateCommand();
                if (row["ptcptp"].ToString() == "PTP")
                    command4.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE cve_prod = '" + row["id_producto"] + "' AND fecha >= '" + str12 + "' AND fecha <= '" + str13 + "'";
                if (row["ptcptp"].ToString() == "PTC")
                    command4.CommandText = "SELECT SUM(A.rptd_cantidad) AS num_cajas FROM tb_det_recepcion_pt A JOIN tb_mstr_recepcion_pt B ON A.rpt_recibo = B.rpt_recibo WHERE B.rpt_fecha >= '" + str12 + "' AND B.rpt_fecha <= '" + str13 + "' AND A.prod_clave = '" + row["id_producto"] + "' AND B.rpt_estatus not in ('F', 'C') and B.rpt_tipo = 'CM'";
                SqlDataReader sqlDataReader4 = command4.ExecuteReader();
                if (sqlDataReader4.HasRows)
                {
                    while (sqlDataReader4.Read())
                        row["producidas"] = sqlDataReader4["num_cajas"].ToString().Trim() == "" ? (object)"0" : (object)sqlDataReader4["num_cajas"].ToString().Trim();
                }
                sqlDataReader4.Close();
                sqlDataReader4.Dispose();
                command4.Dispose();
            }

            //CALCULO DE PRODUCIDAS MES ACTUAL
            string _prod_actual = "";
            SqlCommand command4_b = sqlConnection.CreateCommand();
            //command4_b.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE fecha >= '" + str12 + "' AND fecha <= '" + str13 + "'";
            command4_b.CommandText = "SELECT SUM(hrp_num_unidades) AS num_cajas FROM tb_hist_recepcion WHERE hrp_fecha >= '" + str12 + "' AND hrp_fecha <= '" + str13 + "' " +
                "and hrp_estatus <> 'C' and hrp_tipo_recepcion = 'PTC' and hrp_situacion = 'CM'";
            SqlDataReader sqlDataReader4_b = command4_b.ExecuteReader();
            if (sqlDataReader4_b.HasRows)
            {
                while (sqlDataReader4_b.Read())
                    _prod_actual = sqlDataReader4_b["num_cajas"].ToString().Trim() == "" ? "0" : sqlDataReader4_b["num_cajas"].ToString().Trim();
            }
            sqlDataReader4_b.Close();
            sqlDataReader4_b.Dispose();

            command4_b = sqlConnection.CreateCommand();
            //command4_b.CommandText = "SELECT SUM(A.rptd_cantidad) AS num_cajas FROM tb_det_recepcion_pt A JOIN tb_mstr_recepcion_pt B ON A.rpt_recibo = B.rpt_recibo WHERE B.rpt_fecha >= '" + str12 + "' AND B.rpt_fecha <= '" + str13 + "' AND B.rpt_estatus not in ('F', 'C') and B.rpt_tipo = 'CM'";
            command4_b.CommandText = "Select SUM(E.fodp_unidades) as num_cajas " +
                "FROM TB_MSTR_ORDENES_PROD A, tb_det_final_odp E " +
                "WHERE A.ordp_fecha >= '" + str12 + "' and A.ordp_fecha <= '" + str13 + "' " +
                "and A.ordp_estatus <> 'C' AND A.ORDP_FOLIO = E.ORDP_FOLIO";
            sqlDataReader4_b = command4_b.ExecuteReader();
            if (sqlDataReader4_b.HasRows)
            {
                string val_1 = "";
                while (sqlDataReader4_b.Read())
                    val_1 = (sqlDataReader4_b["num_cajas"].ToString().Trim() == "") ? "0" : sqlDataReader4_b["num_cajas"].ToString().Trim();
                _prod_actual = Math.Abs(Convert.ToDecimal(_prod_actual) + Convert.ToDecimal(val_1)).ToString();
            }
            sqlDataReader4_b.Close();
            sqlDataReader4_b.Dispose();
            command4_b.Dispose();

            foreach (DataRow row in (InternalDataCollectionBase)this.dtProdSiguiente.Rows)
            {
                SqlCommand command4 = sqlConnection.CreateCommand();
                if (row["ptcptp"].ToString() == "PTP")
                    command4.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE cve_prod = '" + row["id_producto"] + "' AND fecha >= '" + str14 + "' AND fecha <= '" + str15 + "'";
                if (row["ptcptp"].ToString() == "PTC")
                    command4.CommandText = "SELECT SUM(A.rptd_cantidad) AS num_cajas FROM tb_det_recepcion_pt A JOIN tb_mstr_recepcion_pt B ON A.rpt_recibo = B.rpt_recibo WHERE B.rpt_fecha >= '" + str14 + "' AND B.rpt_fecha <= '" + str15 + "' AND A.prod_clave = '" + row["id_producto"] + "' AND B.rpt_estatus not in ('F', 'C') and B.rpt_tipo = 'CM'";
                SqlDataReader sqlDataReader4 = command4.ExecuteReader();
                if (sqlDataReader4.HasRows)
                {
                    while (sqlDataReader4.Read())
                        row["producidas"] = sqlDataReader4["num_cajas"].ToString().Trim() == "" ? (object)"0" : (object)sqlDataReader4["num_cajas"].ToString().Trim();
                }
                sqlDataReader4.Close();
                sqlDataReader4.Dispose();
                command4.Dispose();
            }

            //CALCULO DE PRODUCIDAS MES SIGUIENTE
            string _prod_siguiente = "";
            SqlCommand command4_c = sqlConnection.CreateCommand();
            //command4_c.CommandText = "SELECT SUM(num_cajas) AS num_cajas FROM tb_det_eti_final WHERE fecha >= '" + str14 + "' AND fecha <= '" + str15 + "'";
            command4_c.CommandText = "SELECT SUM(hrp_num_unidades) AS num_cajas FROM tb_hist_recepcion WHERE hrp_fecha >= '" + str14 + "' AND hrp_fecha <= '" + str15 + "' " +
                "and hrp_estatus <> 'C' and hrp_tipo_recepcion = 'PTC' and hrp_situacion = 'CM'";
            SqlDataReader sqlDataReader4_c = command4_c.ExecuteReader();
            if (sqlDataReader4_c.HasRows)
            {
                while (sqlDataReader4_c.Read())
                    _prod_siguiente = sqlDataReader4_c["num_cajas"].ToString().Trim() == "" ? "0" : sqlDataReader4_c["num_cajas"].ToString().Trim();
            }
            sqlDataReader4_c.Close();
            sqlDataReader4_c.Dispose();

            command4_c = sqlConnection.CreateCommand();
            //command4_c.CommandText = "SELECT SUM(A.rptd_cantidad) AS num_cajas FROM tb_det_recepcion_pt A JOIN tb_mstr_recepcion_pt B ON A.rpt_recibo = B.rpt_recibo WHERE B.rpt_fecha >= '" + str14 + "' AND B.rpt_fecha <= '" + str15 + "' AND B.rpt_estatus not in ('F', 'C') and B.rpt_tipo = 'CM'";
            command4_c.CommandText = "Select SUM(E.fodp_unidades) as num_cajas " +
                "FROM TB_MSTR_ORDENES_PROD A, tb_det_final_odp E " +
                "WHERE A.ordp_fecha >= '" + str14 + "' and A.ordp_fecha <= '" + str15 + "' " +
                "and A.ordp_estatus <> 'C' AND A.ORDP_FOLIO = E.ORDP_FOLIO";
            sqlDataReader4_c = command4_c.ExecuteReader();
            if (sqlDataReader4_c.HasRows)
            {
                string val_1 = "";
                while (sqlDataReader4_c.Read())
                    val_1 = (sqlDataReader4_c["num_cajas"].ToString().Trim() == "") ? "0" : sqlDataReader4_c["num_cajas"].ToString().Trim();
                _prod_siguiente = Math.Abs(Convert.ToDecimal(_prod_siguiente) + Convert.ToDecimal(val_1)).ToString();
            }
            sqlDataReader4_c.Close();
            sqlDataReader4_c.Dispose();
            command4_c.Dispose();

            sqlConnection.Close();
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            foreach (DataRow row in (InternalDataCollectionBase)this.dtProdAnterior.Rows)
            {
                num2 += Convert.ToInt32(row["producidas"].ToString());
                num3 += Convert.ToInt32(row["rechazadas"].ToString());
                num4 += Convert.ToInt32(row["recibidas"].ToString());
            }
            foreach (DataRow row in (InternalDataCollectionBase)this.dtProdActual.Rows)
            {
                num5 += Convert.ToInt32(row["producidas"].ToString());
                num6 += Convert.ToInt32(row["rechazadas"].ToString());
                num7 += Convert.ToInt32(row["recibidas"].ToString());
            }
            foreach (DataRow row in (InternalDataCollectionBase)this.dtProdSiguiente.Rows)
            {
                num8 += Convert.ToInt32(row["producidas"].ToString());
                num9 += Convert.ToInt32(row["rechazadas"].ToString());
                num10 += Convert.ToInt32(row["recibidas"].ToString());
            }
            this.txtAnteriorProd.Value = _prod_anterior;//num2.ToString();
            this.txtAnteriorRech.Value = num3.ToString();
            this.txtAnteriorReci.Value = _reci_anterior;// num4.ToString();
            this.txtActualProd.Value = _prod_actual;//num5.ToString();
            this.txtActualRech.Value = num6.ToString();
            this.txtActualReci.Value = _reci_actual;//num7.ToString();
            this.txtSiguienteProd.Value = _prod_siguiente;//num8.ToString();
            this.txtSiguienteRech.Value = num9.ToString();
            this.txtSiguienteReci.Value = _reci_siguiente;//num10.ToString();
            this.udpMeses.Update();
            foreach (DataRow row1 in (InternalDataCollectionBase)this.dtProdAnterior.Rows)
            {
                DataRow row2 = this.dtProductos.NewRow();
                row2["id_producto"] = row1["id_producto"];
                row2["mes_anterior"] = row1["rechazadas"].ToString() == "" ? (object)"0" : row1["rechazadas"];
                row2["mes_actual"] = (object)"0";
                row2["mes_siguiente"] = (object)"0";
                //row2["fact_ant"] = row1["facturadas"].ToString() == "" ? (object)"0" : row1["facturadas"];
                //row2["fact_act"] = (object)"0";
                //row2["fact_sig"] = (object)"0";
                this.dtProductos.Rows.Add(row2);
            }
            foreach (DataRow row1 in (InternalDataCollectionBase)this.dtProdActual.Rows)
            {
                bool flag = false;
                foreach (DataRow dataRow in this.dtProductos.Select("id_producto = '" + row1["id_producto"] + "'"))
                {
                    flag = true;
                    dataRow["mes_actual"] = row1["rechazadas"].ToString() == "" ? (object)"0" : row1["rechazadas"];
                    //dataRow["fact_act"] = row1["facturadas"].ToString() == "" ? (object)"0" : row1["facturadas"];
                }
                if (!flag)
                {
                    DataRow row2 = this.dtProductos.NewRow();
                    row2["id_producto"] = row1["id_producto"];
                    row2["mes_anterior"] = (object)"0";
                    row2["mes_actual"] = row1["rechazadas"].ToString() == "" ? (object)"0" : row1["rechazadas"];
                    row2["mes_siguiente"] = (object)"0";

                    //row2["fact_ant"] = (object)"0";
                    //row2["fact_act"] = row1["facturadas"].ToString() == "" ? (object)"0" : row1["facturadas"]; 
                    //row2["fact_sig"] = (object)"0";

                    this.dtProductos.Rows.Add(row2);
                }
            }
            foreach (DataRow row1 in (InternalDataCollectionBase)this.dtProdSiguiente.Rows)
            {
                bool flag = false;
                foreach (DataRow dataRow in this.dtProductos.Select("id_producto = '" + row1["id_producto"] + "'"))
                {
                    flag = true;
                    dataRow["mes_siguiente"] = row1["rechazadas"].ToString() == "" ? (object)"0" : row1["rechazadas"];
                    //dataRow["fact_sig"] = row1["facturadas"].ToString() == "" ? (object)"0" : row1["facturadas"];
                }
                if (!flag)
                {
                    DataRow row2 = this.dtProductos.NewRow();
                    row2["id_producto"] = row1["id_producto"];
                    row2["mes_anterior"] = (object)"0";
                    row2["mes_actual"] = (object)"0";
                    row2["mes_siguiente"] = row1["rechazadas"].ToString() == "" ? (object)"0" : row1["rechazadas"];

                    //row2["fact_ant"] = (object)"0";
                    //row2["fact_act"] = (object)"0";
                    //row2["fact_sig"] = row1["facturadas"].ToString() == "" ? (object)"0" : row1["facturadas"]; 

                    this.dtProductos.Rows.Add(row2);
                }
            }
            sqlConnection.Open();
            foreach (DataRow row in (InternalDataCollectionBase)this.dtProductos.Rows)
            {
                SqlCommand command4 = sqlConnection.CreateCommand();
                command4.CommandText = "SELECT prod_nombre FROM tb_cat_producto WHERE prod_clave = '" + row["id_producto"] + "'";
                SqlDataReader sqlDataReader4 = command4.ExecuteReader();
                if (sqlDataReader4.HasRows)
                {
                    sqlDataReader4.Read();
                    row["producto"] = (object)sqlDataReader4["prod_nombre"].ToString().Trim();
                }
                sqlDataReader4.Close();
                sqlDataReader4.Dispose();
                command4.Dispose();
            }
            sqlConnection.Close();
            DataView defaultView = this.dtProductos.DefaultView;
            defaultView.Sort = "producto";
            this.dtProductos = defaultView.ToTable();
            string str16 = "";
            string str17 = "";
            string str18 = "";
            string str19 = "";

            string str20 = "";
            string str21 = "";
            string str22 = "";

            foreach (DataRow row in (InternalDataCollectionBase)this.dtProductos.Rows)
            {
                if (Convert.ToInt32(row["mes_anterior"]) > 24 || Convert.ToInt32(row["mes_actual"]) > 24 || Convert.ToInt32(row["mes_siguiente"]) > 24)
                {
                    str16 = str16 + row["producto"] + ", ";
                    str17 = str17 + row["mes_anterior"] + ", ";
                    str18 = str18 + row["mes_actual"] + ", ";
                    str19 = str19 + row["mes_siguiente"] + ", ";
                    //str20 = str20 + row["fact_ant"] + ", ";
                    //str21 = str21 + row["fact_act"] + ", ";
                    //str22 = str22 + row["fact_sig"] + ", ";
                }
            }
            this.txtBarNames.Value = str16.TrimEnd(' ').TrimEnd(',').ToString();
            this.txtBarAnterior.Value = str17.TrimEnd(' ').TrimEnd(',').ToString();
            this.txtBarActual.Value = str18.TrimEnd(' ').TrimEnd(',').ToString();
            this.txtBarSiguiente.Value = str19.TrimEnd(' ').TrimEnd(',').ToString();
            //this.txtBarVentasAnt.Value = str20.TrimEnd(' ').TrimEnd(',').ToString();
            //this.txtBarVentasAct.Value = str21.TrimEnd(' ').TrimEnd(',').ToString();
            //this.txtBarVentasSig.Value = str22.TrimEnd(' ').TrimEnd(',').ToString();
            this.udpMeses.Update();
        }

    }
}