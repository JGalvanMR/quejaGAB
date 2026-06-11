using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class clientes_sem : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtQuejas = new DataTable();
        private SqlConnection conex = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
        private SqlCommand comand = new SqlCommand();
        private DataTable dtExcel = new DataTable();
        private SqlDataAdapter adapter;
        private SqlDataReader reader;

        string fecha_1 = "";
        string fecha_2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SEMANA", typeof(string));
            dataTable.Columns.Add(this.lblCedis.Text, typeof(string));
            dataTable.Columns.Add("TOTAL", typeof(string));
            this.dtExcel.Columns.Add("SEMANA", typeof(string));
            this.dtExcel.Columns.Add(this.lblCedis.Text, typeof(string));
            this.dtExcel.Columns.Add("TOTAL", typeof(string));
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1, (object)"0", (object)"0");
                ++num1;
            }

            fecha_1 = "01/01/" + DateTime.Now.Year.ToString();
            fecha_2 = "31/12/" + DateTime.Now.Year.ToString();

            Decimal num2 = new Decimal(0);
            this.conex.Open();
            this.comand = this.conex.CreateCommand();
            this.comand.CommandText = "SELECT que_sememb AS que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas WHERE cedis = '" + this.Session["cedis"].ToString() + "' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                this.comand.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, A.cedis FROM tb_mstr_quejas A JOIN tb_cat_areas_queja B ON A.area_queja = B.id_area AND B.area_nombre = 'EMPAQUE_AGUILARES' WHERE cedis = 'COMERCIALIZADORA GAB' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            this.reader = this.comand.ExecuteReader();
            if (this.reader.HasRows)
            {
                while (this.reader.Read())
                {
                    string str = this.reader.GetValue(0).ToString().Trim();
                    foreach (DataRow dataRow in dataTable.Select("Semana = '" + str + "'"))
                    {
                        if (this.reader.GetValue(2).ToString().Trim() == "CANCUN")
                        {
                            dataRow["CANCUN"] = (object)(Convert.ToDecimal(dataRow["CANCUN"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "ABASTOS")
                        {
                            dataRow["ABASTOS"] = (object)(Convert.ToDecimal(dataRow["ABASTOS"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "SMO")
                        {
                            dataRow["SMO"] = (object)(Convert.ToDecimal(dataRow["SMO"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "MONTERREY")
                        {
                            dataRow["MONTERREY"] = (object)(Convert.ToDecimal(dataRow["MONTERREY"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "GUADALAJARA")
                        {
                            dataRow["GUADALAJARA"] = (object)(Convert.ToDecimal(dataRow["GUADALAJARA"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "COMERCIALIZADORA GAB")
                        {
                            if (this.Session["cedis"].ToString() == "AGUILARES")
                            {
                                dataRow["AGUILARES"] = (object)(Convert.ToDecimal(dataRow["AGUILARES"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                                num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                            }
                            else
                            {
                                dataRow["COMERCIALIZADORA GAB"] = (object)(Convert.ToDecimal(dataRow["COMERCIALIZADORA GAB"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                                num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                            }
                        }
                    }
                }
            }
            this.reader.Close();
            this.reader.Dispose();
            this.comand.Dispose();
            this.conex.Close();
            this.dtExcel.Rows.Add((object)"TOTAL", (object)num2, (object)num2);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this.dtExcel.Rows.Add(row.ItemArray);
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>SEMANA</font></th><th bgcolor='#337ab7'><font color='#fff'>" + this.lblCedis.Text + "</font></th></tr></thead>" + "<tbody>";
            string str2 = "<tr><td><b>Sem_1</b></td>";
            string str3 = "<tr><td><b>Sem_2</b></td>";
            string str4 = "<tr><td><b>Sem_3</b></td>";
            string str5 = "<tr><td><b>Sem_4</b></td>";
            string str6 = "<tr><td><b>Sem_5</b></td>";
            string str7 = "<tr><td><b>Sem_6</b></td>";
            string str8 = "<tr><td><b>Sem_7</b></td>";
            string str9 = "<tr><td><b>Sem_8</b></td>";
            string str10 = "<tr><td><b>Sem_9</b></td>";
            string str11 = "<tr><td><b>Sem_10</b></td>";
            string str12 = "<tr><td><b>Sem_11</b></td>";
            string str13 = "<tr><td><b>Sem_12</b></td>";
            string str14 = "<tr><td><b>Sem_13</b></td>";
            string str15 = "<tr><td><b>Sem_14</b></td>";
            string str16 = "<tr><td><b>Sem_15</b></td>";
            string str17 = "<tr><td><b>Sem_16</b></td>";
            string str18 = "<tr><td><b>Sem_17</b></td>";
            string str19 = "<tr><td><b>Sem_18</b></td>";
            string str20 = "<tr><td><b>Sem_19</b></td>";
            string str21 = "<tr><td><b>Sem_20</b></td>";
            string str22 = "<tr><td><b>Sem_21</b></td>";
            string str23 = "<tr><td><b>Sem_22</b></td>";
            string str24 = "<tr><td><b>Sem_23</b></td>";
            string str25 = "<tr><td><b>Sem_24</b></td>";
            string str26 = "<tr><td><b>Sem_25</b></td>";
            string str27 = "<tr><td><b>Sem_26</b></td>";
            string str28 = "<tr><td><b>Sem_27</b></td>";
            string str29 = "<tr><td><b>Sem_28</b></td>";
            string str30 = "<tr><td><b>Sem_29</b></td>";
            string str31 = "<tr><td><b>Sem_30</b></td>";
            string str32 = "<tr><td><b>Sem_31</b></td>";
            string str33 = "<tr><td><b>Sem_32</b></td>";
            string str34 = "<tr><td><b>Sem_33</b></td>";
            string str35 = "<tr><td><b>Sem_34</b></td>";
            string str36 = "<tr><td><b>Sem_35</b></td>";
            string str37 = "<tr><td><b>Sem_36</b></td>";
            string str38 = "<tr><td><b>Sem_37</b></td>";
            string str39 = "<tr><td><b>Sem_38</b></td>";
            string str40 = "<tr><td><b>Sem_39</b></td>";
            string str41 = "<tr><td><b>Sem_40</b></td>";
            string str42 = "<tr><td><b>Sem_41</b></td>";
            string str43 = "<tr><td><b>Sem 42</b></td>";
            string str44 = "<tr><td><b>Sem_43</b></td>";
            string str45 = "<tr><td><b>Sem_44</b></td>";
            string str46 = "<tr><td><b>Sem_45</b></td>";
            string str47 = "<tr><td><b>Sem_46</b></td>";
            string str48 = "<tr><td><b>Sem_47</b></td>";
            string str49 = "<tr><td><b>Sem_48</b></td>";
            string str50 = "<tr><td><b>Sem_49</b></td>";
            string str51 = "<tr><td><b>Sem_50</b></td>";
            string str52 = "<tr><td><b>Sem_51</b></td>";
            string str53 = "<tr><td><b>Sem_52</b></td>";
            string str54 = "<tr><td><b>Sem_53</b></td>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["Semana"].ToString() == "1")
                    str2 = str2 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "2")
                    str3 = str3 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "3")
                    str4 = str4 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "4")
                    str5 = str5 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "5")
                    str6 = str6 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "6")
                    str7 = str7 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "7")
                    str8 = str8 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "8")
                    str9 = str9 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "9")
                    str10 = str10 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "10")
                    str11 = str11 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "11")
                    str12 = str12 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "12")
                    str13 = str13 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "13")
                    str14 = str14 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "14")
                    str15 = str15 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "15")
                    str16 = str16 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "16")
                    str17 = str17 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "17")
                    str18 = str18 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "18")
                    str19 = str19 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "19")
                    str20 = str20 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "20")
                    str21 = str21 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "21")
                    str22 = str22 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "22")
                    str23 = str23 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "23")
                    str24 = str24 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "24")
                    str25 = str25 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "25")
                    str26 = str26 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "26")
                    str27 = str27 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "27")
                    str28 = str28 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "28")
                    str29 = str29 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "29")
                    str30 = str30 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "30")
                    str31 = str31 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "31")
                    str32 = str32 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "32")
                    str33 = str33 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "33")
                    str34 = str34 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "34")
                    str35 = str35 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "35")
                    str36 = str36 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "36")
                    str37 = str37 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "37")
                    str38 = str38 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "38")
                    str39 = str39 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "39")
                    str40 = str40 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "40")
                    str41 = str41 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "41")
                    str42 = str42 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "42")
                    str43 = str43 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "43")
                    str44 = str44 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "44")
                    str45 = str45 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "45")
                    str46 = str46 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "46")
                    str47 = str47 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "47")
                    str48 = str48 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "48")
                    str49 = str49 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "49")
                    str50 = str50 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "50")
                    str51 = str51 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "51")
                    str52 = str52 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "52")
                    str53 = str53 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "53")
                    str54 = str54 + "<td>" + row[1].ToString() + "</td>";
            }
            string str55 = str2 + "</tr>";
            string str56 = str3 + "</tr>";
            string str57 = str4 + "</tr>";
            string str58 = str5 + "</tr>";
            string str59 = str6 + "</tr>";
            string str60 = str7 + "</tr>";
            string str61 = str8 + "</tr>";
            string str62 = str9 + "</tr>";
            string str63 = str10 + "</tr>";
            string str64 = str11 + "</tr>";
            string str65 = str12 + "</tr>";
            string str66 = str13 + "</tr>";
            string str67 = str14 + "</tr>";
            string str68 = str15 + "</tr>";
            string str69 = str16 + "</tr>";
            string str70 = str17 + "</tr>";
            string str71 = str18 + "</tr>";
            string str72 = str19 + "</tr>";
            string str73 = str20 + "</tr>";
            string str74 = str21 + "</tr>";
            string str75 = str22 + "</tr>";
            string str76 = str23 + "</tr>";
            string str77 = str24 + "</tr>";
            string str78 = str25 + "</tr>";
            string str79 = str26 + "</tr>";
            string str80 = str27 + "</tr>";
            string str81 = str28 + "</tr>";
            string str82 = str29 + "</tr>";
            string str83 = str30 + "</tr>";
            string str84 = str31 + "</tr>";
            string str85 = str32 + "</tr>";
            string str86 = str33 + "</tr>";
            string str87 = str34 + "</tr>";
            string str88 = str35 + "</tr>";
            string str89 = str36 + "</tr>";
            string str90 = str37 + "</tr>";
            string str91 = str38 + "</tr>";
            string str92 = str39 + "</tr>";
            string str93 = str40 + "</tr>";
            string str94 = str41 + "</tr>";
            string str95 = str42 + "</tr>";
            string str96 = str43 + "</tr>";
            string str97 = str44 + "</tr>";
            string str98 = str45 + "</tr>";
            string str99 = str46 + "</tr>";
            string str100 = str47 + "</tr>";
            string str101 = str48 + "</tr>";
            string str102 = str49 + "</tr>";
            string str103 = str50 + "</tr>";
            string str104 = str51 + "</tr>";
            string str105 = str52 + "</tr>";
            string str106 = str53 + "</tr>";
            string str107 = str54 + "</tr>";
            this.datosqueja.InnerHtml = str1 + str55 + str56 + str57 + str58 + str59 + str60 + str61 + str62 + str63 + str64 + str65 + str66 + str67 + str68 + str69 + str70 + str71 + str72 + str73 + str74 + str75 + str76 + str77 + str78 + str79 + str80 + str81 + str82 + str83 + str84 + str85 + str86 + str87 + str88 + str89 + str90 + str91 + str92 + str93 + str94 + str95 + str96 + str97 + str98 + str99 + str100 + str101 + str102 + str103 + str104 + str105 + str106 + str107 + "</tbody>" + "</table>";
            this.grafica_totales();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("reportes.aspx");
        }

        protected void btnTodos_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SEMANA", typeof(string));
            dataTable.Columns.Add("CANCUN", typeof(string));
            dataTable.Columns.Add("ABASTOS", typeof(string));
            dataTable.Columns.Add("SMO", typeof(string));
            dataTable.Columns.Add("MONTERREY", typeof(string));
            dataTable.Columns.Add("GUADALAJARA", typeof(string));
            dataTable.Columns.Add("GAB", typeof(string));
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1, (object)"0", (object)"0", (object)"0", (object)"0", (object)"0", (object)"0");
                ++num1;
            }
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = "SELECT que_sememb AS que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas WHERE que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            Decimal num2 = new Decimal(0);
            Decimal num3 = new Decimal(0);
            Decimal num4 = new Decimal(0);
            Decimal num5 = new Decimal(0);
            Decimal num6 = new Decimal(0);
            Decimal num7 = new Decimal(0);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    string str = sqlDataReader.GetValue(0).ToString().Trim();
                    foreach (DataRow dataRow in dataTable.Select("Semana = '" + str + "'"))
                    {
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "CANCUN")
                        {
                            dataRow["CANCUN"] = (object)(Convert.ToDecimal(dataRow["CANCUN"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "ABASTOS")
                        {
                            dataRow["ABASTOS"] = (object)(Convert.ToDecimal(dataRow["ABASTOS"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num3 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "SMO")
                        {
                            dataRow["SMO"] = (object)(Convert.ToDecimal(dataRow["SMO"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num4 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "MONTERREY")
                        {
                            dataRow["MONTERREY"] = (object)(Convert.ToDecimal(dataRow["MONTERREY"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num5 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "GUADALAJARA")
                        {
                            dataRow["GUADALAJARA"] = (object)(Convert.ToDecimal(dataRow["GUADALAJARA"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num6 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "COMERCIALIZADORA GAB")
                        {
                            dataRow["GAB"] = (object)(Convert.ToDecimal(dataRow["GAB"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num7 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                    }
                }
            }
            sqlDataReader.Close();
            sqlDataReader.Dispose();
            command.Dispose();
            sqlConnection.Close();
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>SEMANA</font></th><th bgcolor='#337ab7'><font color='#fff'>CANCUN</font></th><th bgcolor='#337ab7'><font color='#fff'>ABASTOS</font></th><th bgcolor='#337ab7'><font color='#fff'>SMO</font></th><th bgcolor='#337ab7'><font color='#fff'>MONTERREY</font></th><th bgcolor='#337ab7'><font color='#fff'>GUADALAJARA</font></th><th bgcolor='#337ab7'><font color='#fff'>COMERCIALIZADORA GAB</font></th></tr></thead>" + "<tbody>";
            string str2 = "<tr><td><b>Sem_1</b></td>";
            string str3 = "<tr><td><b>Sem_2</b></td>";
            string str4 = "<tr><td><b>Sem_3</b></td>";
            string str5 = "<tr><td><b>Sem_4</b></td>";
            string str6 = "<tr><td><b>Sem_5</b></td>";
            string str7 = "<tr><td><b>Sem_6</b></td>";
            string str8 = "<tr><td><b>Sem_7</b></td>";
            string str9 = "<tr><td><b>Sem_8</b></td>";
            string str10 = "<tr><td><b>Sem_9</b></td>";
            string str11 = "<tr><td><b>Sem_10</b></td>";
            string str12 = "<tr><td><b>Sem_11</b></td>";
            string str13 = "<tr><td><b>Sem_12</b></td>";
            string str14 = "<tr><td><b>Sem_13</b></td>";
            string str15 = "<tr><td><b>Sem_14</b></td>";
            string str16 = "<tr><td><b>Sem_15</b></td>";
            string str17 = "<tr><td><b>Sem_16</b></td>";
            string str18 = "<tr><td><b>Sem_17</b></td>";
            string str19 = "<tr><td><b>Sem_18</b></td>";
            string str20 = "<tr><td><b>Sem_19</b></td>";
            string str21 = "<tr><td><b>Sem_20</b></td>";
            string str22 = "<tr><td><b>Sem_21</b></td>";
            string str23 = "<tr><td><b>Sem_22</b></td>";
            string str24 = "<tr><td><b>Sem_23</b></td>";
            string str25 = "<tr><td><b>Sem_24</b></td>";
            string str26 = "<tr><td><b>Sem_25</b></td>";
            string str27 = "<tr><td><b>Sem_26</b></td>";
            string str28 = "<tr><td><b>Sem_27</b></td>";
            string str29 = "<tr><td><b>Sem_28</b></td>";
            string str30 = "<tr><td><b>Sem_29</b></td>";
            string str31 = "<tr><td><b>Sem_30</b></td>";
            string str32 = "<tr><td><b>Sem_31</b></td>";
            string str33 = "<tr><td><b>Sem_32</b></td>";
            string str34 = "<tr><td><b>Sem_33</b></td>";
            string str35 = "<tr><td><b>Sem_34</b></td>";
            string str36 = "<tr><td><b>Sem_35</b></td>";
            string str37 = "<tr><td><b>Sem_36</b></td>";
            string str38 = "<tr><td><b>Sem_37</b></td>";
            string str39 = "<tr><td><b>Sem_38</b></td>";
            string str40 = "<tr><td><b>Sem_39</b></td>";
            string str41 = "<tr><td><b>Sem_40</b></td>";
            string str42 = "<tr><td><b>Sem_41</b></td>";
            string str43 = "<tr><td><b>Sem 42</b></td>";
            string str44 = "<tr><td><b>Sem_43</b></td>";
            string str45 = "<tr><td><b>Sem_44</b></td>";
            string str46 = "<tr><td><b>Sem_45</b></td>";
            string str47 = "<tr><td><b>Sem_46</b></td>";
            string str48 = "<tr><td><b>Sem_47</b></td>";
            string str49 = "<tr><td><b>Sem_48</b></td>";
            string str50 = "<tr><td><b>Sem_49</b></td>";
            string str51 = "<tr><td><b>Sem_50</b></td>";
            string str52 = "<tr><td><b>Sem_51</b></td>";
            string str53 = "<tr><td><b>Sem_52</b></td>";
            string str54 = "<tr><td><b>Sem_53</b></td>";
            Decimal num8 = new Decimal(0);
            Decimal num9 = new Decimal(0);
            Decimal num10 = new Decimal(0);
            Decimal num11 = new Decimal(0);
            Decimal num12 = new Decimal(0);
            Decimal num13 = new Decimal(0);
            Decimal num14 = new Decimal(0);
            Decimal num15 = new Decimal(0);
            Decimal num16 = new Decimal(0);
            Decimal num17 = new Decimal(0);
            Decimal num18 = new Decimal(0);
            Decimal num19 = new Decimal(0);
            Decimal num20 = new Decimal(0);
            Decimal num21 = new Decimal(0);
            Decimal num22 = new Decimal(0);
            Decimal num23 = new Decimal(0);
            Decimal num24 = new Decimal(0);
            Decimal num25 = new Decimal(0);
            Decimal num26 = new Decimal(0);
            Decimal num27 = new Decimal(0);
            Decimal num28 = new Decimal(0);
            Decimal num29 = new Decimal(0);
            Decimal num30 = new Decimal(0);
            Decimal num31 = new Decimal(0);
            Decimal num32 = new Decimal(0);
            Decimal num33 = new Decimal(0);
            Decimal num34 = new Decimal(0);
            Decimal num35 = new Decimal(0);
            Decimal num36 = new Decimal(0);
            Decimal num37 = new Decimal(0);
            Decimal num38 = new Decimal(0);
            Decimal num39 = new Decimal(0);
            Decimal num40 = new Decimal(0);
            Decimal num41 = new Decimal(0);
            Decimal num42 = new Decimal(0);
            Decimal num43 = new Decimal(0);
            Decimal num44 = new Decimal(0);
            Decimal num45 = new Decimal(0);
            Decimal num46 = new Decimal(0);
            Decimal num47 = new Decimal(0);
            Decimal num48 = new Decimal(0);
            Decimal num49 = new Decimal(0);
            Decimal num50 = new Decimal(0);
            Decimal num51 = new Decimal(0);
            Decimal num52 = new Decimal(0);
            Decimal num53 = new Decimal(0);
            Decimal num54 = new Decimal(0);
            Decimal num55 = new Decimal(0);
            Decimal num56 = new Decimal(0);
            Decimal num57 = new Decimal(0);
            Decimal num58 = new Decimal(0);
            Decimal num59 = new Decimal(0);
            Decimal num60 = new Decimal(0);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["Semana"].ToString() == "1")
                {
                    str2 = str2 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str2 = str2 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str2 = str2 + "<td>" + row["SMO"].ToString() + "</td>";
                    str2 = str2 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str2 = str2 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str2 = str2 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "2")
                {
                    str3 = str3 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str3 = str3 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str3 = str3 + "<td>" + row["SMO"].ToString() + "</td>";
                    str3 = str3 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str3 = str3 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str3 = str3 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "3")
                {
                    str4 = str4 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str4 = str4 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str4 = str4 + "<td>" + row["SMO"].ToString() + "</td>";
                    str4 = str4 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str4 = str4 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str4 = str4 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "4")
                {
                    str5 = str5 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str5 = str5 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str5 = str5 + "<td>" + row["SMO"].ToString() + "</td>";
                    str5 = str5 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str5 = str5 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str5 = str5 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "5")
                {
                    str6 = str6 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str6 = str6 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str6 = str6 + "<td>" + row["SMO"].ToString() + "</td>";
                    str6 = str6 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str6 = str6 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str6 = str6 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "6")
                {
                    str7 = str7 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str7 = str7 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str7 = str7 + "<td>" + row["SMO"].ToString() + "</td>";
                    str7 = str7 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str7 = str7 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str7 = str7 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "7")
                {
                    str8 = str8 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str8 = str8 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str8 = str8 + "<td>" + row["SMO"].ToString() + "</td>";
                    str8 = str8 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str8 = str8 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str8 = str8 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "8")
                {
                    str9 = str9 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str9 = str9 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str9 = str9 + "<td>" + row["SMO"].ToString() + "</td>";
                    str9 = str9 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str9 = str9 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str9 = str9 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "9")
                {
                    str10 = str10 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str10 = str10 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str10 = str10 + "<td>" + row["SMO"].ToString() + "</td>";
                    str10 = str10 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str10 = str10 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str10 = str10 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "10")
                {
                    str11 = str11 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str11 = str11 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str11 = str11 + "<td>" + row["SMO"].ToString() + "</td>";
                    str11 = str11 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str11 = str11 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str11 = str11 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "11")
                {
                    str12 = str12 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str12 = str12 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str12 = str12 + "<td>" + row["SMO"].ToString() + "</td>";
                    str12 = str12 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str12 = str12 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str12 = str12 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "12")
                {
                    str13 = str13 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str13 = str13 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str13 = str13 + "<td>" + row["SMO"].ToString() + "</td>";
                    str13 = str13 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str13 = str13 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str13 = str13 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "13")
                {
                    str14 = str14 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str14 = str14 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str14 = str14 + "<td>" + row["SMO"].ToString() + "</td>";
                    str14 = str14 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str14 = str14 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str14 = str14 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "14")
                {
                    str15 = str15 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str15 = str15 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str15 = str15 + "<td>" + row["SMO"].ToString() + "</td>";
                    str15 = str15 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str15 = str15 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str15 = str15 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "15")
                {
                    str16 = str16 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str16 = str16 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str16 = str16 + "<td>" + row["SMO"].ToString() + "</td>";
                    str16 = str16 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str16 = str16 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str16 = str16 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "16")
                {
                    str17 = str17 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str17 = str17 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str17 = str17 + "<td>" + row["SMO"].ToString() + "</td>";
                    str17 = str17 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str17 = str17 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str17 = str17 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "17")
                {
                    str18 = str18 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str18 = str18 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str18 = str18 + "<td>" + row["SMO"].ToString() + "</td>";
                    str18 = str18 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str18 = str18 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str18 = str18 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "18")
                {
                    str19 = str19 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str19 = str19 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str19 = str19 + "<td>" + row["SMO"].ToString() + "</td>";
                    str19 = str19 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str19 = str19 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str19 = str19 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "19")
                {
                    str20 = str20 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str20 = str20 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str20 = str20 + "<td>" + row["SMO"].ToString() + "</td>";
                    str20 = str20 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str20 = str20 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str20 = str20 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "20")
                {
                    str21 = str21 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str21 = str21 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str21 = str21 + "<td>" + row["SMO"].ToString() + "</td>";
                    str21 = str21 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str21 = str21 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str21 = str21 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "21")
                {
                    str22 = str22 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str22 = str22 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str22 = str22 + "<td>" + row["SMO"].ToString() + "</td>";
                    str22 = str22 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str22 = str22 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str22 = str22 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "22")
                {
                    str23 = str23 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str23 = str23 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str23 = str23 + "<td>" + row["SMO"].ToString() + "</td>";
                    str23 = str23 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str23 = str23 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str23 = str23 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "23")
                {
                    str24 = str24 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str24 = str24 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str24 = str24 + "<td>" + row["SMO"].ToString() + "</td>";
                    str24 = str24 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str24 = str24 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str24 = str24 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "24")
                {
                    str25 = str25 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str25 = str25 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str25 = str25 + "<td>" + row["SMO"].ToString() + "</td>";
                    str25 = str25 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str25 = str25 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str25 = str25 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "25")
                {
                    str26 = str26 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str26 = str26 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str26 = str26 + "<td>" + row["SMO"].ToString() + "</td>";
                    str26 = str26 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str26 = str26 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str26 = str26 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "26")
                {
                    str27 = str27 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str27 = str27 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str27 = str27 + "<td>" + row["SMO"].ToString() + "</td>";
                    str27 = str27 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str27 = str27 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str27 = str27 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "27")
                {
                    str28 = str28 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str28 = str28 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str28 = str28 + "<td>" + row["SMO"].ToString() + "</td>";
                    str28 = str28 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str28 = str28 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str28 = str28 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "28")
                {
                    str29 = str29 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str29 = str29 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str29 = str29 + "<td>" + row["SMO"].ToString() + "</td>";
                    str29 = str29 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str29 = str29 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str29 = str29 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "29")
                {
                    str30 = str30 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str30 = str30 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str30 = str30 + "<td>" + row["SMO"].ToString() + "</td>";
                    str30 = str30 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str30 = str30 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str30 = str30 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "30")
                {
                    str31 = str31 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str31 = str31 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str31 = str31 + "<td>" + row["SMO"].ToString() + "</td>";
                    str31 = str31 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str31 = str31 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str31 = str31 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "31")
                {
                    str32 = str32 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str32 = str32 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str32 = str32 + "<td>" + row["SMO"].ToString() + "</td>";
                    str32 = str32 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str32 = str32 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str32 = str32 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "32")
                {
                    str33 = str33 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str33 = str33 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str33 = str33 + "<td>" + row["SMO"].ToString() + "</td>";
                    str33 = str33 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str33 = str33 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str33 = str33 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "33")
                {
                    str34 = str34 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str34 = str34 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str34 = str34 + "<td>" + row["SMO"].ToString() + "</td>";
                    str34 = str34 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str34 = str34 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str34 = str34 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "34")
                {
                    str35 = str35 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str35 = str35 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str35 = str35 + "<td>" + row["SMO"].ToString() + "</td>";
                    str35 = str35 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str35 = str35 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str35 = str35 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "35")
                {
                    str36 = str36 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str36 = str36 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str36 = str36 + "<td>" + row["SMO"].ToString() + "</td>";
                    str36 = str36 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str36 = str36 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str36 = str36 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "36")
                {
                    str37 = str37 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str37 = str37 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str37 = str37 + "<td>" + row["SMO"].ToString() + "</td>";
                    str37 = str37 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str37 = str37 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str37 = str37 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "37")
                {
                    str38 = str38 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str38 = str38 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str38 = str38 + "<td>" + row["SMO"].ToString() + "</td>";
                    str38 = str38 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str38 = str38 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str38 = str38 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "38")
                {
                    str39 = str39 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str39 = str39 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str39 = str39 + "<td>" + row["SMO"].ToString() + "</td>";
                    str39 = str39 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str39 = str39 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str39 = str39 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "39")
                {
                    str40 = str40 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str40 = str40 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str40 = str40 + "<td>" + row["SMO"].ToString() + "</td>";
                    str40 = str40 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str40 = str40 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str40 = str40 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "40")
                {
                    str41 = str41 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str41 = str41 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str41 = str41 + "<td>" + row["SMO"].ToString() + "</td>";
                    str41 = str41 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str41 = str41 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str41 = str41 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "41")
                {
                    str42 = str42 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str42 = str42 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str42 = str42 + "<td>" + row["SMO"].ToString() + "</td>";
                    str42 = str42 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str42 = str42 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str42 = str42 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "42")
                {
                    str43 = str43 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str43 = str43 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str43 = str43 + "<td>" + row["SMO"].ToString() + "</td>";
                    str43 = str43 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str43 = str43 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str43 = str43 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "43")
                {
                    str44 = str44 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str44 = str44 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str44 = str44 + "<td>" + row["SMO"].ToString() + "</td>";
                    str44 = str44 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str44 = str44 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str44 = str44 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "44")
                {
                    str45 = str45 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str45 = str45 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str45 = str45 + "<td>" + row["SMO"].ToString() + "</td>";
                    str45 = str45 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str45 = str45 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str45 = str45 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "45")
                {
                    str46 = str46 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str46 = str46 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str46 = str46 + "<td>" + row["SMO"].ToString() + "</td>";
                    str46 = str46 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str46 = str46 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str46 = str46 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "46")
                {
                    str47 = str47 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str47 = str47 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str47 = str47 + "<td>" + row["SMO"].ToString() + "</td>";
                    str47 = str47 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str47 = str47 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str47 = str47 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "47")
                {
                    str48 = str48 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str48 = str48 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str48 = str48 + "<td>" + row["SMO"].ToString() + "</td>";
                    str48 = str48 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str48 = str48 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str48 = str48 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "48")
                {
                    str49 = str49 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str49 = str49 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str49 = str49 + "<td>" + row["SMO"].ToString() + "</td>";
                    str49 = str49 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str49 = str49 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str49 = str49 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "49")
                {
                    str50 = str50 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str50 = str50 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str50 = str50 + "<td>" + row["SMO"].ToString() + "</td>";
                    str50 = str50 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str50 = str50 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str50 = str50 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "50")
                {
                    str51 = str51 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str51 = str51 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str51 = str51 + "<td>" + row["SMO"].ToString() + "</td>";
                    str51 = str51 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str51 = str51 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str51 = str51 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "51")
                {
                    str52 = str52 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str52 = str52 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str52 = str52 + "<td>" + row["SMO"].ToString() + "</td>";
                    str52 = str52 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str52 = str52 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str52 = str52 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "52")
                {
                    str53 = str53 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str53 = str53 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str53 = str53 + "<td>" + row["SMO"].ToString() + "</td>";
                    str53 = str53 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str53 = str53 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str53 = str53 + "<td>" + row["GAB"].ToString() + "</td>";
                }
                if (row["Semana"].ToString() == "53")
                {
                    str54 = str54 + "<td>" + row["Cancun"].ToString() + "</td>";
                    str54 = str54 + "<td>" + row["Abastos"].ToString() + "</td>";
                    str54 = str54 + "<td>" + row["SMO"].ToString() + "</td>";
                    str54 = str54 + "<td>" + row["Monterrey"].ToString() + "</td>";
                    str54 = str54 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                    str54 = str54 + "<td>" + row["GAB"].ToString() + "</td>";
                }
            }
            string str55 = str2 + "</tr>";
            string str56 = str3 + "</tr>";
            string str57 = str4 + "</tr>";
            string str58 = str5 + "</tr>";
            string str59 = str6 + "</tr>";
            string str60 = str7 + "</tr>";
            string str61 = str8 + "</tr>";
            string str62 = str9 + "</tr>";
            string str63 = str10 + "</tr>";
            string str64 = str11 + "</tr>";
            string str65 = str12 + "</tr>";
            string str66 = str13 + "</tr>";
            string str67 = str14 + "</tr>";
            string str68 = str15 + "</tr>";
            string str69 = str16 + "</tr>";
            string str70 = str17 + "</tr>";
            string str71 = str18 + "</tr>";
            string str72 = str19 + "</tr>";
            string str73 = str20 + "</tr>";
            string str74 = str21 + "</tr>";
            string str75 = str22 + "</tr>";
            string str76 = str23 + "</tr>";
            string str77 = str24 + "</tr>";
            string str78 = str25 + "</tr>";
            string str79 = str26 + "</tr>";
            string str80 = str27 + "</tr>";
            string str81 = str28 + "</tr>";
            string str82 = str29 + "</tr>";
            string str83 = str30 + "</tr>";
            string str84 = str31 + "</tr>";
            string str85 = str32 + "</tr>";
            string str86 = str33 + "</tr>";
            string str87 = str34 + "</tr>";
            string str88 = str35 + "</tr>";
            string str89 = str36 + "</tr>";
            string str90 = str37 + "</tr>";
            string str91 = str38 + "</tr>";
            string str92 = str39 + "</tr>";
            string str93 = str40 + "</tr>";
            string str94 = str41 + "</tr>";
            string str95 = str42 + "</tr>";
            string str96 = str43 + "</tr>";
            string str97 = str44 + "</tr>";
            string str98 = str45 + "</tr>";
            string str99 = str46 + "</tr>";
            string str100 = str47 + "</tr>";
            string str101 = str48 + "</tr>";
            string str102 = str49 + "</tr>";
            string str103 = str50 + "</tr>";
            string str104 = str51 + "</tr>";
            string str105 = str52 + "</tr>";
            string str106 = str53 + "</tr>";
            string str107 = str54 + "</tr>";
            this.datosqueja.InnerHtml = str1 + str55 + str56 + str57 + str58 + str59 + str60 + str61 + str62 + str63 + str64 + str65 + str66 + str67 + str68 + str69 + str70 + str71 + str72 + str73 + str74 + str75 + str76 + str77 + str78 + str79 + str80 + str81 + str82 + str83 + str84 + str85 + str86 + str87 + str88 + str89 + str90 + str91 + str92 + str93 + str94 + str95 + str96 + str97 + str98 + str99 + str100 + str101 + str102 + str103 + str104 + str105 + str106 + str107 + "</tbody>" + "</table>";
            this.grafica_totales_todos();
        }

        protected void btnCedis_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SEMANA", typeof(string));
            dataTable.Columns.Add(this.lblCedis.Text, typeof(string));
            int num = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num, (object)"0");
                ++num;
            }
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = "SELECT que_sememb AS que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas WHERE cedis = '" + this.Session["cedis"].ToString() + "' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                command.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, A.cedis FROM tb_mstr_quejas A JOIN tb_cat_areas_queja B ON A.area_queja = B.id_area AND B.area_nombre = 'EMPAQUE_AGUILARES' WHERE cedis = 'COMERCIALIZADORA GAB' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    string str = sqlDataReader.GetValue(0).ToString().Trim();
                    foreach (DataRow dataRow in dataTable.Select("Semana = '" + str + "'"))
                    {
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "CANCUN")
                            dataRow["CANCUN"] = (object)(Convert.ToDecimal(dataRow["CANCUN"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "ABASTOS")
                            dataRow["ABASTOS"] = (object)(Convert.ToDecimal(dataRow["ABASTOS"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "SMO")
                            dataRow["SMO"] = (object)(Convert.ToDecimal(dataRow["SMO"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "MONTERREY")
                            dataRow["MONTERREY"] = (object)(Convert.ToDecimal(dataRow["MONTERREY"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "GUADALAJARA")
                            dataRow["GUADALAJARA"] = (object)(Convert.ToDecimal(dataRow["GUADALAJARA"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "COMERCIALIZADORA GAB")
                        {
                            if (this.Session["cedis"].ToString() == "AGUILARES")
                                dataRow["AGUILARES"] = (object)(Convert.ToDecimal(dataRow["AGUILARES"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            else
                                dataRow["COMERCIALIZADORA GAB"] = (object)(Convert.ToDecimal(dataRow["COMERCIALIZADORA GAB"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                        }
                    }
                }
            }
            sqlDataReader.Close();
            sqlDataReader.Dispose();
            command.Dispose();
            sqlConnection.Close();
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>SEMANA</font></th><th bgcolor='#337ab7'><font color='#fff'>" + this.lblCedis.Text + "</font></th></tr></thead>" + "<tbody>";
            string str2 = "<tr><td><b>Sem_1</b></td>";
            string str3 = "<tr><td><b>Sem_2</b></td>";
            string str4 = "<tr><td><b>Sem_3</b></td>";
            string str5 = "<tr><td><b>Sem_4</b></td>";
            string str6 = "<tr><td><b>Sem_5</b></td>";
            string str7 = "<tr><td><b>Sem_6</b></td>";
            string str8 = "<tr><td><b>Sem_7</b></td>";
            string str9 = "<tr><td><b>Sem_8</b></td>";
            string str10 = "<tr><td><b>Sem_9</b></td>";
            string str11 = "<tr><td><b>Sem_10</b></td>";
            string str12 = "<tr><td><b>Sem_11</b></td>";
            string str13 = "<tr><td><b>Sem_12</b></td>";
            string str14 = "<tr><td><b>Sem_13</b></td>";
            string str15 = "<tr><td><b>Sem_14</b></td>";
            string str16 = "<tr><td><b>Sem_15</b></td>";
            string str17 = "<tr><td><b>Sem_16</b></td>";
            string str18 = "<tr><td><b>Sem_17</b></td>";
            string str19 = "<tr><td><b>Sem_18</b></td>";
            string str20 = "<tr><td><b>Sem_19</b></td>";
            string str21 = "<tr><td><b>Sem_20</b></td>";
            string str22 = "<tr><td><b>Sem_21</b></td>";
            string str23 = "<tr><td><b>Sem_22</b></td>";
            string str24 = "<tr><td><b>Sem_23</b></td>";
            string str25 = "<tr><td><b>Sem_24</b></td>";
            string str26 = "<tr><td><b>Sem_25</b></td>";
            string str27 = "<tr><td><b>Sem_26</b></td>";
            string str28 = "<tr><td><b>Sem_27</b></td>";
            string str29 = "<tr><td><b>Sem_28</b></td>";
            string str30 = "<tr><td><b>Sem_29</b></td>";
            string str31 = "<tr><td><b>Sem_30</b></td>";
            string str32 = "<tr><td><b>Sem_31</b></td>";
            string str33 = "<tr><td><b>Sem_32</b></td>";
            string str34 = "<tr><td><b>Sem_33</b></td>";
            string str35 = "<tr><td><b>Sem_34</b></td>";
            string str36 = "<tr><td><b>Sem_35</b></td>";
            string str37 = "<tr><td><b>Sem_36</b></td>";
            string str38 = "<tr><td><b>Sem_37</b></td>";
            string str39 = "<tr><td><b>Sem_38</b></td>";
            string str40 = "<tr><td><b>Sem_39</b></td>";
            string str41 = "<tr><td><b>Sem_40</b></td>";
            string str42 = "<tr><td><b>Sem_41</b></td>";
            string str43 = "<tr><td><b>Sem 42</b></td>";
            string str44 = "<tr><td><b>Sem_43</b></td>";
            string str45 = "<tr><td><b>Sem_44</b></td>";
            string str46 = "<tr><td><b>Sem_45</b></td>";
            string str47 = "<tr><td><b>Sem_46</b></td>";
            string str48 = "<tr><td><b>Sem_47</b></td>";
            string str49 = "<tr><td><b>Sem_48</b></td>";
            string str50 = "<tr><td><b>Sem_49</b></td>";
            string str51 = "<tr><td><b>Sem_50</b></td>";
            string str52 = "<tr><td><b>Sem_51</b></td>";
            string str53 = "<tr><td><b>Sem_52</b></td>";
            string str54 = "<tr><td><b>Sem_53</b></td>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["Semana"].ToString() == "1")
                    str2 = str2 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "2")
                    str3 = str3 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "3")
                    str4 = str4 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "4")
                    str5 = str5 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "5")
                    str6 = str6 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "6")
                    str7 = str7 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "7")
                    str8 = str8 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "8")
                    str9 = str9 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "9")
                    str10 = str10 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "10")
                    str11 = str11 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "11")
                    str12 = str12 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "12")
                    str13 = str13 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "13")
                    str14 = str14 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "14")
                    str15 = str15 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "15")
                    str16 = str16 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "16")
                    str17 = str17 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "17")
                    str18 = str18 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "18")
                    str19 = str19 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "19")
                    str20 = str20 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "20")
                    str21 = str21 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "21")
                    str22 = str22 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "22")
                    str23 = str23 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "23")
                    str24 = str24 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "24")
                    str25 = str25 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "25")
                    str26 = str26 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "26")
                    str27 = str27 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "27")
                    str28 = str28 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "28")
                    str29 = str29 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "29")
                    str30 = str30 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "30")
                    str31 = str31 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "31")
                    str32 = str32 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "32")
                    str33 = str33 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "33")
                    str34 = str34 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "34")
                    str35 = str35 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "35")
                    str36 = str36 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "36")
                    str37 = str37 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "37")
                    str38 = str38 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "38")
                    str39 = str39 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "39")
                    str40 = str40 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "40")
                    str41 = str41 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "41")
                    str42 = str42 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "42")
                    str43 = str43 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "43")
                    str44 = str44 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "44")
                    str45 = str45 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "45")
                    str46 = str46 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "46")
                    str47 = str47 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "47")
                    str48 = str48 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "48")
                    str49 = str49 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "49")
                    str50 = str50 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "50")
                    str51 = str51 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "51")
                    str52 = str52 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "52")
                    str53 = str53 + "<td>" + row[1].ToString() + "</td>";
                if (row["Semana"].ToString() == "53")
                    str54 = str54 + "<td>" + row[1].ToString() + "</td>";
            }
            string str55 = str2 + "</tr>";
            string str56 = str3 + "</tr>";
            string str57 = str4 + "</tr>";
            string str58 = str5 + "</tr>";
            string str59 = str6 + "</tr>";
            string str60 = str7 + "</tr>";
            string str61 = str8 + "</tr>";
            string str62 = str9 + "</tr>";
            string str63 = str10 + "</tr>";
            string str64 = str11 + "</tr>";
            string str65 = str12 + "</tr>";
            string str66 = str13 + "</tr>";
            string str67 = str14 + "</tr>";
            string str68 = str15 + "</tr>";
            string str69 = str16 + "</tr>";
            string str70 = str17 + "</tr>";
            string str71 = str18 + "</tr>";
            string str72 = str19 + "</tr>";
            string str73 = str20 + "</tr>";
            string str74 = str21 + "</tr>";
            string str75 = str22 + "</tr>";
            string str76 = str23 + "</tr>";
            string str77 = str24 + "</tr>";
            string str78 = str25 + "</tr>";
            string str79 = str26 + "</tr>";
            string str80 = str27 + "</tr>";
            string str81 = str28 + "</tr>";
            string str82 = str29 + "</tr>";
            string str83 = str30 + "</tr>";
            string str84 = str31 + "</tr>";
            string str85 = str32 + "</tr>";
            string str86 = str33 + "</tr>";
            string str87 = str34 + "</tr>";
            string str88 = str35 + "</tr>";
            string str89 = str36 + "</tr>";
            string str90 = str37 + "</tr>";
            string str91 = str38 + "</tr>";
            string str92 = str39 + "</tr>";
            string str93 = str40 + "</tr>";
            string str94 = str41 + "</tr>";
            string str95 = str42 + "</tr>";
            string str96 = str43 + "</tr>";
            string str97 = str44 + "</tr>";
            string str98 = str45 + "</tr>";
            string str99 = str46 + "</tr>";
            string str100 = str47 + "</tr>";
            string str101 = str48 + "</tr>";
            string str102 = str49 + "</tr>";
            string str103 = str50 + "</tr>";
            string str104 = str51 + "</tr>";
            string str105 = str52 + "</tr>";
            string str106 = str53 + "</tr>";
            string str107 = str54 + "</tr>";
            this.datosqueja.InnerHtml = str1 + str55 + str56 + str57 + str58 + str59 + str60 + str61 + str62 + str63 + str64 + str65 + str66 + str67 + str68 + str69 + str70 + str71 + str72 + str73 + str74 + str75 + str76 + str77 + str78 + str79 + str80 + str81 + str82 + str83 + str84 + str85 + str86 + str87 + str88 + str89 + str90 + str91 + str92 + str93 + str94 + str95 + str96 + str97 + str98 + str99 + str100 + str101 + str102 + str103 + str104 + str105 + str106 + str107 + "</tbody>" + "</table>";
            this.grafica_totales();
        }

        public void grafica_totales()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SEMANA", typeof(string));
            dataTable.Columns.Add(this.lblCedis.Text, typeof(string));
            int num1 = 1;
            for (int index = 0; index < 54; ++index)
            {
                dataTable.Rows.Add((object)num1, (object)"0");
                ++num1;
            }
            this.conex.Open();
            this.comand = this.conex.CreateCommand();
            this.comand.CommandText = "SELECT que_sememb AS que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas WHERE cedis = '" + this.Session["cedis"].ToString() + "' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                this.comand.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, A.cedis FROM tb_mstr_quejas A JOIN tb_cat_areas_queja B ON A.area_queja = B.id_area AND B.area_nombre = 'EMPAQUE_AGUILARES' WHERE cedis = 'COMERCIALIZADORA GAB' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            Decimal num2 = new Decimal(0);
            this.reader = this.comand.ExecuteReader();
            if (this.reader.HasRows)
            {
                while (this.reader.Read())
                {
                    string str = this.reader.GetValue(0).ToString().Trim();
                    foreach (DataRow dataRow in dataTable.Select("Semana = '" + str + "'"))
                    {
                        if (this.reader.GetValue(2).ToString().Trim() == "CANCUN")
                        {
                            dataRow["CANCUN"] = (object)(Convert.ToDecimal(dataRow["CANCUN"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "ABASTOS")
                        {
                            dataRow["ABASTOS"] = (object)(Convert.ToDecimal(dataRow["ABASTOS"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "SMO")
                        {
                            dataRow["SMO"] = (object)(Convert.ToDecimal(dataRow["SMO"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "MONTERREY")
                        {
                            dataRow["MONTERREY"] = (object)(Convert.ToDecimal(dataRow["MONTERREY"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "GUADALAJARA")
                        {
                            dataRow["GUADALAJARA"] = (object)(Convert.ToDecimal(dataRow["GUADALAJARA"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "COMERCIALIZADORA GAB")
                        {
                            if (this.Session["cedis"].ToString() == "AGUILARES")
                            {
                                dataRow["AGUILARES"] = (object)(Convert.ToDecimal(dataRow["AGUILARES"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                                num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                            }
                            else
                            {
                                dataRow["COMERCIALIZADORA GAB"] = (object)(Convert.ToDecimal(dataRow["COMERCIALIZADORA GAB"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                                num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                            }
                        }
                    }
                }
            }
            this.reader.Close();
            this.reader.Dispose();
            this.comand.Dispose();
            this.conex.Close();
            string str1 = "<table border='1' id='datatable1' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>SEMANA</font></th><th bgcolor='#337ab7'><font color='#fff'>" + this.lblCedis.Text + "</font></th><th bgcolor='#337ab7'><font color='#fff'>TOTAL</font></th></tr></thead>" + "<tbody>";
            string str2 = "<tr><td><b>Sem_1</b></td>";
            string str3 = "<tr><td><b>Sem_2</b></td>";
            string str4 = "<tr><td><b>Sem_3</b></td>";
            string str5 = "<tr><td><b>Sem_4</b></td>";
            string str6 = "<tr><td><b>Sem_5</b></td>";
            string str7 = "<tr><td><b>Sem_6</b></td>";
            string str8 = "<tr><td><b>Sem_7</b></td>";
            string str9 = "<tr><td><b>Sem_8</b></td>";
            string str10 = "<tr><td><b>Sem_9</b></td>";
            string str11 = "<tr><td><b>Sem_10</b></td>";
            string str12 = "<tr><td><b>Sem_11</b></td>";
            string str13 = "<tr><td><b>Sem_12</b></td>";
            string str14 = "<tr><td><b>Sem_13</b></td>";
            string str15 = "<tr><td><b>Sem_14</b></td>";
            string str16 = "<tr><td><b>Sem_15</b></td>";
            string str17 = "<tr><td><b>Sem_16</b></td>";
            string str18 = "<tr><td><b>Sem_17</b></td>";
            string str19 = "<tr><td><b>Sem_18</b></td>";
            string str20 = "<tr><td><b>Sem_19</b></td>";
            string str21 = "<tr><td><b>Sem_20</b></td>";
            string str22 = "<tr><td><b>Sem_21</b></td>";
            string str23 = "<tr><td><b>Sem_22</b></td>";
            string str24 = "<tr><td><b>Sem_23</b></td>";
            string str25 = "<tr><td><b>Sem_24</b></td>";
            string str26 = "<tr><td><b>Sem_25</b></td>";
            string str27 = "<tr><td><b>Sem_26</b></td>";
            string str28 = "<tr><td><b>Sem_27</b></td>";
            string str29 = "<tr><td><b>Sem_28</b></td>";
            string str30 = "<tr><td><b>Sem_29</b></td>";
            string str31 = "<tr><td><b>Sem_30</b></td>";
            string str32 = "<tr><td><b>Sem_31</b></td>";
            string str33 = "<tr><td><b>Sem_32</b></td>";
            string str34 = "<tr><td><b>Sem_33</b></td>";
            string str35 = "<tr><td><b>Sem_34</b></td>";
            string str36 = "<tr><td><b>Sem_35</b></td>";
            string str37 = "<tr><td><b>Sem_36</b></td>";
            string str38 = "<tr><td><b>Sem_37</b></td>";
            string str39 = "<tr><td><b>Sem_38</b></td>";
            string str40 = "<tr><td><b>Sem_39</b></td>";
            string str41 = "<tr><td><b>Sem_40</b></td>";
            string str42 = "<tr><td><b>Sem_41</b></td>";
            string str43 = "<tr><td><b>Sem 42</b></td>";
            string str44 = "<tr><td><b>Sem_43</b></td>";
            string str45 = "<tr><td><b>Sem_44</b></td>";
            string str46 = "<tr><td><b>Sem_45</b></td>";
            string str47 = "<tr><td><b>Sem_46</b></td>";
            string str48 = "<tr><td><b>Sem_47</b></td>";
            string str49 = "<tr><td><b>Sem_48</b></td>";
            string str50 = "<tr><td><b>Sem_49</b></td>";
            string str51 = "<tr><td><b>Sem_50</b></td>";
            string str52 = "<tr><td><b>Sem_51</b></td>";
            string str53 = "<tr><td><b>Sem_52</b></td>";
            string str54 = "<tr><td><b>Sem_53</b></td>";
            string str55 = "<tr><td><b>TOTAL</b></td>" + "<td>" + num2.ToString() + "</td><td>" + num2.ToString() + "</td></tr>";
            string str56 = str1 + str55;
            Decimal num3 = new Decimal(0);
            Decimal num4 = new Decimal(0);
            Decimal num5 = new Decimal(0);
            Decimal num6 = new Decimal(0);
            Decimal num7 = new Decimal(0);
            Decimal num8 = new Decimal(0);
            Decimal num9 = new Decimal(0);
            Decimal num10 = new Decimal(0);
            Decimal num11 = new Decimal(0);
            Decimal num12 = new Decimal(0);
            Decimal num13 = new Decimal(0);
            Decimal num14 = new Decimal(0);
            Decimal num15 = new Decimal(0);
            Decimal num16 = new Decimal(0);
            Decimal num17 = new Decimal(0);
            Decimal num18 = new Decimal(0);
            Decimal num19 = new Decimal(0);
            Decimal num20 = new Decimal(0);
            Decimal num21 = new Decimal(0);
            Decimal num22 = new Decimal(0);
            Decimal num23 = new Decimal(0);
            Decimal num24 = new Decimal(0);
            Decimal num25 = new Decimal(0);
            Decimal num26 = new Decimal(0);
            Decimal num27 = new Decimal(0);
            Decimal num28 = new Decimal(0);
            Decimal num29 = new Decimal(0);
            Decimal num30 = new Decimal(0);
            Decimal num31 = new Decimal(0);
            Decimal num32 = new Decimal(0);
            Decimal num33 = new Decimal(0);
            Decimal num34 = new Decimal(0);
            Decimal num35 = new Decimal(0);
            Decimal num36 = new Decimal(0);
            Decimal num37 = new Decimal(0);
            Decimal num38 = new Decimal(0);
            Decimal num39 = new Decimal(0);
            Decimal num40 = new Decimal(0);
            Decimal num41 = new Decimal(0);
            Decimal num42 = new Decimal(0);
            Decimal num43 = new Decimal(0);
            Decimal num44 = new Decimal(0);
            Decimal num45 = new Decimal(0);
            Decimal num46 = new Decimal(0);
            Decimal num47 = new Decimal(0);
            Decimal num48 = new Decimal(0);
            Decimal num49 = new Decimal(0);
            Decimal num50 = new Decimal(0);
            Decimal num51 = new Decimal(0);
            Decimal num52 = new Decimal(0);
            Decimal num53 = new Decimal(0);
            Decimal num54 = new Decimal(0);
            Decimal num55 = new Decimal(0);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["Semana"].ToString() == "1")
                {
                    num3 += Convert.ToDecimal(row[1].ToString());
                    if (num3 == new Decimal(0))
                        str2 += "<td></td><td></td>";
                    else
                        str2 = str2 + "<td>" + row[1].ToString() + "</td><td>" + (object)num3 + "</td>";
                }
                if (row["Semana"].ToString() == "2")
                {
                    num4 += Convert.ToDecimal(row[1].ToString());
                    if (num4 == new Decimal(0))
                        str3 += "<td></td><td></td>";
                    else
                        str3 = str3 + "<td>" + row[1].ToString() + "</td><td>" + (object)num4 + "</td>";
                }
                if (row["Semana"].ToString() == "3")
                {
                    num5 += Convert.ToDecimal(row[1].ToString());
                    if (num5 == new Decimal(0))
                        str4 += "<td></td><td></td>";
                    else
                        str4 = str4 + "<td>" + row[1].ToString() + "</td><td>" + (object)num5 + "</td>";
                }
                if (row["Semana"].ToString() == "4")
                {
                    num6 += Convert.ToDecimal(row[1].ToString());
                    if (num6 == new Decimal(0))
                        str5 += "<td></td><td></td>";
                    else
                        str5 = str5 + "<td>" + row[1].ToString() + "</td><td>" + (object)num6 + "</td>";
                }
                if (row["Semana"].ToString() == "5")
                {
                    num7 += Convert.ToDecimal(row[1].ToString());
                    if (num7 == new Decimal(0))
                        str6 += "<td></td><td></td>";
                    else
                        str6 = str6 + "<td>" + row[1].ToString() + "</td><td>" + (object)num7 + "</td>";
                }
                if (row["Semana"].ToString() == "6")
                {
                    num8 += Convert.ToDecimal(row[1].ToString());
                    if (num8 == new Decimal(0))
                        str7 += "<td></td><td></td>";
                    else
                        str7 = str7 + "<td>" + row[1].ToString() + "</td><td>" + (object)num8 + "</td>";
                }
                if (row["Semana"].ToString() == "7")
                {
                    num9 += Convert.ToDecimal(row[1].ToString());
                    if (num9 == new Decimal(0))
                        str8 += "<td></td><td></td>";
                    else
                        str8 = str8 + "<td>" + row[1].ToString() + "</td><td>" + (object)num9 + "</td>";
                }
                if (row["Semana"].ToString() == "8")
                {
                    num10 += Convert.ToDecimal(row[1].ToString());
                    if (num10 == new Decimal(0))
                        str9 += "<td></td><td></td>";
                    else
                        str9 = str9 + "<td>" + row[1].ToString() + "</td><td>" + (object)num10 + "</td>";
                }
                if (row["Semana"].ToString() == "9")
                {
                    num11 += Convert.ToDecimal(row[1].ToString());
                    if (num11 == new Decimal(0))
                        str10 += "<td></td><td></td>";
                    else
                        str10 = str10 + "<td>" + row[1].ToString() + "</td><td>" + (object)num11 + "</td>";
                }
                if (row["Semana"].ToString() == "10")
                {
                    num12 += Convert.ToDecimal(row[1].ToString());
                    if (num12 == new Decimal(0))
                        str11 += "<td></td><td></td>";
                    else
                        str11 = str11 + "<td>" + row[1].ToString() + "</td><td>" + (object)num12 + "</td>";
                }
                if (row["Semana"].ToString() == "11")
                {
                    num13 += Convert.ToDecimal(row[1].ToString());
                    if (num13 == new Decimal(0))
                        str12 += "<td></td><td></td>";
                    else
                        str12 = str12 + "<td>" + row[1].ToString() + "</td><td>" + (object)num13 + "</td>";
                }
                if (row["Semana"].ToString() == "12")
                {
                    num14 += Convert.ToDecimal(row[1].ToString());
                    if (num14 == new Decimal(0))
                        str13 += "<td></td><td></td>";
                    else
                        str13 = str13 + "<td>" + row[1].ToString() + "</td><td>" + (object)num14 + "</td>";
                }
                if (row["Semana"].ToString() == "13")
                {
                    num15 += Convert.ToDecimal(row[1].ToString());
                    if (num15 == new Decimal(0))
                        str14 += "<td></td><td></td>";
                    else
                        str14 = str14 + "<td>" + row[1].ToString() + "</td><td>" + (object)num15 + "</td>";
                }
                if (row["Semana"].ToString() == "14")
                {
                    num16 += Convert.ToDecimal(row[1].ToString());
                    if (num16 == new Decimal(0))
                        str15 += "<td></td><td></td>";
                    else
                        str15 = str15 + "<td>" + row[1].ToString() + "</td><td>" + (object)num16 + "</td>";
                }
                if (row["Semana"].ToString() == "15")
                {
                    num17 += Convert.ToDecimal(row[1].ToString());
                    if (num17 == new Decimal(0))
                        str16 += "<td></td><td></td>";
                    else
                        str16 = str16 + "<td>" + row[1].ToString() + "</td><td>" + (object)num17 + "</td>";
                }
                if (row["Semana"].ToString() == "16")
                {
                    num18 += Convert.ToDecimal(row[1].ToString());
                    if (num18 == new Decimal(0))
                        str17 += "<td></td><td></td>";
                    else
                        str17 = str17 + "<td>" + row[1].ToString() + "</td><td>" + (object)num18 + "</td>";
                }
                if (row["Semana"].ToString() == "17")
                {
                    num19 += Convert.ToDecimal(row[1].ToString());
                    if (num19 == new Decimal(0))
                        str18 += "<td></td><td></td>";
                    else
                        str18 = str18 + "<td>" + row[1].ToString() + "</td><td>" + (object)num19 + "</td>";
                }
                if (row["Semana"].ToString() == "18")
                {
                    num20 += Convert.ToDecimal(row[1].ToString());
                    if (num20 == new Decimal(0))
                        str19 += "<td></td><td></td>";
                    else
                        str19 = str19 + "<td>" + row[1].ToString() + "</td><td>" + (object)num20 + "</td>";
                }
                if (row["Semana"].ToString() == "19")
                {
                    num21 += Convert.ToDecimal(row[1].ToString());
                    if (num21 == new Decimal(0))
                        str20 += "<td></td><td></td>";
                    else
                        str20 = str20 + "<td>" + row[1].ToString() + "</td><td>" + (object)num21 + "</td>";
                }
                if (row["Semana"].ToString() == "20")
                {
                    num22 += Convert.ToDecimal(row[1].ToString());
                    if (num22 == new Decimal(0))
                        str21 += "<td></td><td></td>";
                    else
                        str21 = str21 + "<td>" + row[1].ToString() + "</td><td>" + (object)num22 + "</td>";
                }
                if (row["Semana"].ToString() == "21")
                {
                    num23 += Convert.ToDecimal(row[1].ToString());
                    if (num23 == new Decimal(0))
                        str22 += "<td></td><td></td>";
                    else
                        str22 = str22 + "<td>" + row[1].ToString() + "</td><td>" + (object)num23 + "</td>";
                }
                if (row["Semana"].ToString() == "22")
                {
                    num24 += Convert.ToDecimal(row[1].ToString());
                    if (num24 == new Decimal(0))
                        str23 += "<td></td><td></td>";
                    else
                        str23 = str23 + "<td>" + row[1].ToString() + "</td><td>" + (object)num24 + "</td>";
                }
                if (row["Semana"].ToString() == "23")
                {
                    num25 += Convert.ToDecimal(row[1].ToString());
                    if (num25 == new Decimal(0))
                        str24 += "<td></td><td></td>";
                    else
                        str24 = str24 + "<td>" + row[1].ToString() + "</td><td>" + (object)num25 + "</td>";
                }
                if (row["Semana"].ToString() == "24")
                {
                    num26 += Convert.ToDecimal(row[1].ToString());
                    if (num26 == new Decimal(0))
                        str25 += "<td></td><td></td>";
                    else
                        str25 = str25 + "<td>" + row[1].ToString() + "</td><td>" + (object)num26 + "</td>";
                }
                if (row["Semana"].ToString() == "25")
                {
                    num27 += Convert.ToDecimal(row[1].ToString());
                    if (num27 == new Decimal(0))
                        str26 += "<td></td><td></td>";
                    else
                        str26 = str26 + "<td>" + row[1].ToString() + "</td><td>" + (object)num27 + "</td>";
                }
                if (row["Semana"].ToString() == "26")
                {
                    num28 += Convert.ToDecimal(row[1].ToString());
                    if (num28 == new Decimal(0))
                        str27 += "<td></td><td></td>";
                    else
                        str27 = str27 + "<td>" + row[1].ToString() + "</td><td>" + (object)num28 + "</td>";
                }
                if (row["Semana"].ToString() == "27")
                {
                    num29 += Convert.ToDecimal(row[1].ToString());
                    if (num29 == new Decimal(0))
                        str28 += "<td></td><td></td>";
                    else
                        str28 = str28 + "<td>" + row[1].ToString() + "</td><td>" + (object)num29 + "</td>";
                }
                if (row["Semana"].ToString() == "28")
                {
                    num30 += Convert.ToDecimal(row[1].ToString());
                    if (num30 == new Decimal(0))
                        str29 += "<td></td><td></td>";
                    else
                        str29 = str29 + "<td>" + row[1].ToString() + "</td><td>" + (object)num30 + "</td>";
                }
                if (row["Semana"].ToString() == "29")
                {
                    num31 += Convert.ToDecimal(row[1].ToString());
                    if (num31 == new Decimal(0))
                        str30 += "<td></td><td></td>";
                    else
                        str30 = str30 + "<td>" + row[1].ToString() + "</td><td>" + (object)num31 + "</td>";
                }
                if (row["Semana"].ToString() == "30")
                {
                    num32 += Convert.ToDecimal(row[1].ToString());
                    if (num32 == new Decimal(0))
                        str31 += "<td></td><td></td>";
                    else
                        str31 = str31 + "<td>" + row[1].ToString() + "</td><td>" + (object)num32 + "</td>";
                }
                if (row["Semana"].ToString() == "31")
                {
                    num33 += Convert.ToDecimal(row[1].ToString());
                    if (num33 == new Decimal(0))
                        str32 += "<td></td><td></td>";
                    else
                        str32 = str32 + "<td>" + row[1].ToString() + "</td><td>" + (object)num33 + "</td>";
                }
                if (row["Semana"].ToString() == "32")
                {
                    num34 += Convert.ToDecimal(row[1].ToString());
                    if (num34 == new Decimal(0))
                        str33 += "<td></td><td></td>";
                    else
                        str33 = str33 + "<td>" + row[1].ToString() + "</td><td>" + (object)num34 + "</td>";
                }
                if (row["Semana"].ToString() == "33")
                {
                    num35 += Convert.ToDecimal(row[1].ToString());
                    if (num35 == new Decimal(0))
                        str34 += "<td></td><td></td>";
                    else
                        str34 = str34 + "<td>" + row[1].ToString() + "</td><td>" + (object)num35 + "</td>";
                }
                if (row["Semana"].ToString() == "34")
                {
                    num36 += Convert.ToDecimal(row[1].ToString());
                    if (num36 == new Decimal(0))
                        str35 += "<td></td><td></td>";
                    else
                        str35 = str35 + "<td>" + row[1].ToString() + "</td><td>" + (object)num36 + "</td>";
                }
                if (row["Semana"].ToString() == "35")
                {
                    num37 += Convert.ToDecimal(row[1].ToString());
                    if (num37 == new Decimal(0))
                        str36 += "<td></td><td></td>";
                    else
                        str36 = str36 + "<td>" + row[1].ToString() + "</td><td>" + (object)num37 + "</td>";
                }
                if (row["Semana"].ToString() == "36")
                {
                    num38 += Convert.ToDecimal(row[1].ToString());
                    if (num38 == new Decimal(0))
                        str37 += "<td></td><td></td>";
                    else
                        str37 = str37 + "<td>" + row[1].ToString() + "</td><td>" + (object)num38 + "</td>";
                }
                if (row["Semana"].ToString() == "37")
                {
                    num39 += Convert.ToDecimal(row[1].ToString());
                    if (num39 == new Decimal(0))
                        str38 += "<td></td><td></td>";
                    else
                        str38 = str38 + "<td>" + row[1].ToString() + "</td><td>" + (object)num39 + "</td>";
                }
                if (row["Semana"].ToString() == "38")
                {
                    num40 += Convert.ToDecimal(row[1].ToString());
                    if (num40 == new Decimal(0))
                        str39 += "<td></td><td></td>";
                    else
                        str39 = str39 + "<td>" + row[1].ToString() + "</td><td>" + (object)num40 + "</td>";
                }
                if (row["Semana"].ToString() == "39")
                {
                    num41 += Convert.ToDecimal(row[1].ToString());
                    if (num41 == new Decimal(0))
                        str40 += "<td></td><td></td>";
                    else
                        str40 = str40 + "<td>" + row[1].ToString() + "</td><td>" + (object)num41 + "</td>";
                }
                if (row["Semana"].ToString() == "40")
                {
                    num42 += Convert.ToDecimal(row[1].ToString());
                    if (num42 == new Decimal(0))
                        str41 += "<td></td><td></td>";
                    else
                        str41 = str41 + "<td>" + row[1].ToString() + "</td><td>" + (object)num42 + "</td>";
                }
                if (row["Semana"].ToString() == "41")
                {
                    num43 += Convert.ToDecimal(row[1].ToString());
                    if (num43 == new Decimal(0))
                        str42 += "<td></td><td></td>";
                    else
                        str42 = str42 + "<td>" + row[1].ToString() + "</td><td>" + (object)num43 + "</td>";
                }
                if (row["Semana"].ToString() == "42")
                {
                    num44 += Convert.ToDecimal(row[1].ToString());
                    if (num44 == new Decimal(0))
                        str43 += "<td></td><td></td>";
                    else
                        str43 = str43 + "<td>" + row[1].ToString() + "</td><td>" + (object)num44 + "</td>";
                }
                if (row["Semana"].ToString() == "43")
                {
                    num45 += Convert.ToDecimal(row[1].ToString());
                    if (num45 == new Decimal(0))
                        str44 += "<td></td><td></td>";
                    else
                        str44 = str44 + "<td>" + row[1].ToString() + "</td><td>" + (object)num45 + "</td>";
                }
                if (row["Semana"].ToString() == "44")
                {
                    num46 += Convert.ToDecimal(row[1].ToString());
                    if (num46 == new Decimal(0))
                        str45 += "<td></td><td></td>";
                    else
                        str45 = str45 + "<td>" + row[1].ToString() + "</td><td>" + (object)num46 + "</td>";
                }
                if (row["Semana"].ToString() == "45")
                {
                    num47 += Convert.ToDecimal(row[1].ToString());
                    if (num47 == new Decimal(0))
                        str46 += "<td></td><td></td>";
                    else
                        str46 = str46 + "<td>" + row[1].ToString() + "</td><td>" + (object)num47 + "</td>";
                }
                if (row["Semana"].ToString() == "46")
                {
                    num48 += Convert.ToDecimal(row[1].ToString());
                    if (num48 == new Decimal(0))
                        str47 += "<td></td><td></td>";
                    else
                        str47 = str47 + "<td>" + row[1].ToString() + "</td><td>" + (object)num48 + "</td>";
                }
                if (row["Semana"].ToString() == "47")
                {
                    num49 += Convert.ToDecimal(row[1].ToString());
                    if (num49 == new Decimal(0))
                        str48 += "<td></td><td></td>";
                    else
                        str48 = str48 + "<td>" + row[1].ToString() + "</td><td>" + (object)num49 + "</td>";
                }
                if (row["Semana"].ToString() == "48")
                {
                    num50 += Convert.ToDecimal(row[1].ToString());
                    if (num50 == new Decimal(0))
                        str49 += "<td></td><td></td>";
                    else
                        str49 = str49 + "<td>" + row[1].ToString() + "</td><td>" + (object)num50 + "</td>";
                }
                if (row["Semana"].ToString() == "49")
                {
                    num51 += Convert.ToDecimal(row[1].ToString());
                    if (num51 == new Decimal(0))
                        str50 += "<td></td><td></td>";
                    else
                        str50 = str50 + "<td>" + row[1].ToString() + "</td><td>" + (object)num51 + "</td>";
                }
                if (row["Semana"].ToString() == "50")
                {
                    num52 += Convert.ToDecimal(row[1].ToString());
                    if (num52 == new Decimal(0))
                        str51 += "<td></td><td></td>";
                    else
                        str51 = str51 + "<td>" + row[1].ToString() + "</td><td>" + (object)num52 + "</td>";
                }
                if (row["Semana"].ToString() == "51")
                {
                    num53 += Convert.ToDecimal(row[1].ToString());
                    if (num53 == new Decimal(0))
                        str52 += "<td></td><td></td>";
                    else
                        str52 = str52 + "<td>" + row[1].ToString() + "</td><td>" + (object)num53 + "</td>";
                }
                if (row["Semana"].ToString() == "52")
                {
                    num54 += Convert.ToDecimal(row[1].ToString());
                    if (num54 == new Decimal(0))
                        str53 += "<td></td><td></td>";
                    else
                        str53 = str53 + "<td>" + row[1].ToString() + "</td><td>" + (object)num54 + "</td>";
                }
                if (row["Semana"].ToString() == "53")
                {
                    num55 += Convert.ToDecimal(row[1].ToString());
                    if (num55 == new Decimal(0))
                        str54 += "<td></td><td></td>";
                    else
                        str54 = str54 + "<td>" + row[1].ToString() + "</td><td>" + (object)num55 + "</td>";
                }
            }
            string str57 = str2 + "</tr>";
            string str58 = str3 + "</tr>";
            string str59 = str4 + "</tr>";
            string str60 = str5 + "</tr>";
            string str61 = str6 + "</tr>";
            string str62 = str7 + "</tr>";
            string str63 = str8 + "</tr>";
            string str64 = str9 + "</tr>";
            string str65 = str10 + "</tr>";
            string str66 = str11 + "</tr>";
            string str67 = str12 + "</tr>";
            string str68 = str13 + "</tr>";
            string str69 = str14 + "</tr>";
            string str70 = str15 + "</tr>";
            string str71 = str16 + "</tr>";
            string str72 = str17 + "</tr>";
            string str73 = str18 + "</tr>";
            string str74 = str19 + "</tr>";
            string str75 = str20 + "</tr>";
            string str76 = str21 + "</tr>";
            string str77 = str22 + "</tr>";
            string str78 = str23 + "</tr>";
            string str79 = str24 + "</tr>";
            string str80 = str25 + "</tr>";
            string str81 = str26 + "</tr>";
            string str82 = str27 + "</tr>";
            string str83 = str28 + "</tr>";
            string str84 = str29 + "</tr>";
            string str85 = str30 + "</tr>";
            string str86 = str31 + "</tr>";
            string str87 = str32 + "</tr>";
            string str88 = str33 + "</tr>";
            string str89 = str34 + "</tr>";
            string str90 = str35 + "</tr>";
            string str91 = str36 + "</tr>";
            string str92 = str37 + "</tr>";
            string str93 = str38 + "</tr>";
            string str94 = str39 + "</tr>";
            string str95 = str40 + "</tr>";
            string str96 = str41 + "</tr>";
            string str97 = str42 + "</tr>";
            string str98 = str43 + "</tr>";
            string str99 = str44 + "</tr>";
            string str100 = str45 + "</tr>";
            string str101 = str46 + "</tr>";
            string str102 = str47 + "</tr>";
            string str103 = str48 + "</tr>";
            string str104 = str49 + "</tr>";
            string str105 = str50 + "</tr>";
            string str106 = str51 + "</tr>";
            string str107 = str52 + "</tr>";
            string str108 = str53 + "</tr>";
            string str109 = str54 + "</tr>";
            this.datosqueja1.InnerHtml = str56 + str57 + str58 + str59 + str60 + str61 + str62 + str63 + str64 + str65 + str66 + str67 + str68 + str69 + str70 + str71 + str72 + str73 + str74 + str75 + str76 + str77 + str78 + str79 + str80 + str81 + str82 + str83 + str84 + str85 + str86 + str87 + str88 + str89 + str90 + str91 + str92 + str93 + str94 + str95 + str96 + str97 + str98 + str99 + str100 + str101 + str102 + str103 + str104 + str105 + str106 + str107 + str108 + str109 + "</tbody>" + "</table>";
        }

        public void grafica_totales_todos()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SEMANA", typeof(string));
            dataTable.Columns.Add("CANCUN", typeof(string));
            dataTable.Columns.Add("ABASTOS", typeof(string));
            dataTable.Columns.Add("SMO", typeof(string));
            dataTable.Columns.Add("MONTERREY", typeof(string));
            dataTable.Columns.Add("GUADALAJARA", typeof(string));
            dataTable.Columns.Add("GAB", typeof(string));
            int num1 = 1;
            for (int index = 0; index < 54; ++index)
            {
                dataTable.Rows.Add((object)num1, (object)"0", (object)"0", (object)"0", (object)"0", (object)"0", (object)"0");
                ++num1;
            }
            this.conex.Open();
            this.comand = this.conex.CreateCommand();
            this.comand.CommandText = "SELECT que_sememb AS que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas WHERE que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            Decimal num2 = new Decimal(0);
            Decimal num3 = new Decimal(0);
            Decimal num4 = new Decimal(0);
            Decimal num5 = new Decimal(0);
            Decimal num6 = new Decimal(0);
            Decimal num7 = new Decimal(0);
            this.reader = this.comand.ExecuteReader();
            if (this.reader.HasRows)
            {
                while (this.reader.Read())
                {
                    string str = this.reader.GetValue(0).ToString().Trim();
                    foreach (DataRow dataRow in dataTable.Select("Semana = '" + str + "'"))
                    {
                        if (this.reader.GetValue(2).ToString().Trim() == "CANCUN")
                        {
                            dataRow["CANCUN"] = (object)(Convert.ToDecimal(dataRow["CANCUN"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "ABASTOS")
                        {
                            dataRow["ABASTOS"] = (object)(Convert.ToDecimal(dataRow["ABASTOS"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num3 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "SMO")
                        {
                            dataRow["SMO"] = (object)(Convert.ToDecimal(dataRow["SMO"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num4 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "MONTERREY")
                        {
                            dataRow["MONTERREY"] = (object)(Convert.ToDecimal(dataRow["MONTERREY"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num5 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "GUADALAJARA")
                        {
                            dataRow["GUADALAJARA"] = (object)(Convert.ToDecimal(dataRow["GUADALAJARA"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num6 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "COMERCIALIZADORA GAB")
                        {
                            dataRow["GAB"] = (object)(Convert.ToDecimal(dataRow["GAB"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num7 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                    }
                }
            }
            this.reader.Close();
            this.reader.Dispose();
            this.comand.Dispose();
            this.conex.Close();
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr><th bgcolor='#337ab7'><font color='#fff'>SEMANA</font></th><th bgcolor='#337ab7'><font color='#fff'>CANCUN</font></th><th bgcolor='#337ab7'><font color='#fff'>ABASTOS</font></th><th bgcolor='#337ab7'><font color='#fff'>SMO</font></th><th bgcolor='#337ab7'><font color='#fff'>MONTERREY</font></th><th bgcolor='#337ab7'><font color='#fff'>GUADALAJARA</font></th><th bgcolor='#337ab7'><font color='#fff'>COMERCIALIZADORA GAB</font></th><th bgcolor='#337ab7'><font color='#fff'>TOTAL</font></th></tr></thead>" + "<tbody>";
            string str2 = "<tr><td><b>Sem_1</b></td>";
            string str3 = "<tr><td><b>Sem_2</b></td>";
            string str4 = "<tr><td><b>Sem_3</b></td>";
            string str5 = "<tr><td><b>Sem_4</b></td>";
            string str6 = "<tr><td><b>Sem_5</b></td>";
            string str7 = "<tr><td><b>Sem_6</b></td>";
            string str8 = "<tr><td><b>Sem_7</b></td>";
            string str9 = "<tr><td><b>Sem_8</b></td>";
            string str10 = "<tr><td><b>Sem_9</b></td>";
            string str11 = "<tr><td><b>Sem_10</b></td>";
            string str12 = "<tr><td><b>Sem_11</b></td>";
            string str13 = "<tr><td><b>Sem_12</b></td>";
            string str14 = "<tr><td><b>Sem_13</b></td>";
            string str15 = "<tr><td><b>Sem_14</b></td>";
            string str16 = "<tr><td><b>Sem_15</b></td>";
            string str17 = "<tr><td><b>Sem_16</b></td>";
            string str18 = "<tr><td><b>Sem_17</b></td>";
            string str19 = "<tr><td><b>Sem_18</b></td>";
            string str20 = "<tr><td><b>Sem_19</b></td>";
            string str21 = "<tr><td><b>Sem_20</b></td>";
            string str22 = "<tr><td><b>Sem_21</b></td>";
            string str23 = "<tr><td><b>Sem_22</b></td>";
            string str24 = "<tr><td><b>Sem_23</b></td>";
            string str25 = "<tr><td><b>Sem_24</b></td>";
            string str26 = "<tr><td><b>Sem_25</b></td>";
            string str27 = "<tr><td><b>Sem_26</b></td>";
            string str28 = "<tr><td><b>Sem_27</b></td>";
            string str29 = "<tr><td><b>Sem_28</b></td>";
            string str30 = "<tr><td><b>Sem_29</b></td>";
            string str31 = "<tr><td><b>Sem_30</b></td>";
            string str32 = "<tr><td><b>Sem_31</b></td>";
            string str33 = "<tr><td><b>Sem_32</b></td>";
            string str34 = "<tr><td><b>Sem_33</b></td>";
            string str35 = "<tr><td><b>Sem_34</b></td>";
            string str36 = "<tr><td><b>Sem_35</b></td>";
            string str37 = "<tr><td><b>Sem_36</b></td>";
            string str38 = "<tr><td><b>Sem_37</b></td>";
            string str39 = "<tr><td><b>Sem_38</b></td>";
            string str40 = "<tr><td><b>Sem_39</b></td>";
            string str41 = "<tr><td><b>Sem_40</b></td>";
            string str42 = "<tr><td><b>Sem_41</b></td>";
            string str43 = "<tr><td><b>Sem 42</b></td>";
            string str44 = "<tr><td><b>Sem_43</b></td>";
            string str45 = "<tr><td><b>Sem_44</b></td>";
            string str46 = "<tr><td><b>Sem_45</b></td>";
            string str47 = "<tr><td><b>Sem_46</b></td>";
            string str48 = "<tr><td><b>Sem_47</b></td>";
            string str49 = "<tr><td><b>Sem_48</b></td>";
            string str50 = "<tr><td><b>Sem_49</b></td>";
            string str51 = "<tr><td><b>Sem_50</b></td>";
            string str52 = "<tr><td><b>Sem_51</b></td>";
            string str53 = "<tr><td><b>Sem_52</b></td>";
            string str54 = "<tr><td><b>Sem_53</b></td>";
            string str55 = "<tr><td><b>TOTAL</b></td>";
            Decimal num8 = new Decimal(0);
            Decimal num9 = num2 + num3 + num4 + num5 + num6 + num7;
            string str56 = str55 + "<td>" + num2.ToString() + "</td><td>" + num3.ToString() + "</td><td>" + num4.ToString() + "</td><td>" + num5.ToString() + "</td><td>" + num6.ToString() + "</td><td>" + num7.ToString() + "</td><td>" + num9.ToString() + "</td></tr>";
            string str57 = str1 + str56;
            Decimal num10 = new Decimal(0);
            Decimal num11 = new Decimal(0);
            Decimal num12 = new Decimal(0);
            Decimal num13 = new Decimal(0);
            Decimal num14 = new Decimal(0);
            Decimal num15 = new Decimal(0);
            Decimal num16 = new Decimal(0);
            Decimal num17 = new Decimal(0);
            Decimal num18 = new Decimal(0);
            Decimal num19 = new Decimal(0);
            Decimal num20 = new Decimal(0);
            Decimal num21 = new Decimal(0);
            Decimal num22 = new Decimal(0);
            Decimal num23 = new Decimal(0);
            Decimal num24 = new Decimal(0);
            Decimal num25 = new Decimal(0);
            Decimal num26 = new Decimal(0);
            Decimal num27 = new Decimal(0);
            Decimal num28 = new Decimal(0);
            Decimal num29 = new Decimal(0);
            Decimal num30 = new Decimal(0);
            Decimal num31 = new Decimal(0);
            Decimal num32 = new Decimal(0);
            Decimal num33 = new Decimal(0);
            Decimal num34 = new Decimal(0);
            Decimal num35 = new Decimal(0);
            Decimal num36 = new Decimal(0);
            Decimal num37 = new Decimal(0);
            Decimal num38 = new Decimal(0);
            Decimal num39 = new Decimal(0);
            Decimal num40 = new Decimal(0);
            Decimal num41 = new Decimal(0);
            Decimal num42 = new Decimal(0);
            Decimal num43 = new Decimal(0);
            Decimal num44 = new Decimal(0);
            Decimal num45 = new Decimal(0);
            Decimal num46 = new Decimal(0);
            Decimal num47 = new Decimal(0);
            Decimal num48 = new Decimal(0);
            Decimal num49 = new Decimal(0);
            Decimal num50 = new Decimal(0);
            Decimal num51 = new Decimal(0);
            Decimal num52 = new Decimal(0);
            Decimal num53 = new Decimal(0);
            Decimal num54 = new Decimal(0);
            Decimal num55 = new Decimal(0);
            Decimal num56 = new Decimal(0);
            Decimal num57 = new Decimal(0);
            Decimal num58 = new Decimal(0);
            Decimal num59 = new Decimal(0);
            Decimal num60 = new Decimal(0);
            Decimal num61 = new Decimal(0);
            Decimal num62 = new Decimal(0);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["Semana"].ToString() == "1")
                {
                    num10 = num10 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num10 == new Decimal(0))
                    {
                        str2 += "<td></td>";
                        str2 += "<td></td>";
                        str2 += "<td></td>";
                        str2 += "<td></td>";
                        str2 += "<td></td>";
                        str2 += "<td></td><td></td>";
                    }
                    else
                    {
                        str2 = str2 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str2 = str2 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str2 = str2 + "<td>" + row["SMO"].ToString() + "</td>";
                        str2 = str2 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str2 = str2 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str2 = str2 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num10 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "2")
                {
                    num11 = num11 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num11 == new Decimal(0))
                    {
                        str3 += "<td></td>";
                        str3 += "<td></td>";
                        str3 += "<td></td>";
                        str3 += "<td></td>";
                        str3 += "<td></td>";
                        str3 += "<td></td><td></td>";
                    }
                    else
                    {
                        str3 = str3 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str3 = str3 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str3 = str3 + "<td>" + row["SMO"].ToString() + "</td>";
                        str3 = str3 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str3 = str3 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str3 = str3 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num11 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "3")
                {
                    num12 = num12 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num12 == new Decimal(0))
                    {
                        str4 += "<td></td>";
                        str4 += "<td></td>";
                        str4 += "<td></td>";
                        str4 += "<td></td>";
                        str4 += "<td></td>";
                        str4 += "<td></td><td></td>";
                    }
                    else
                    {
                        str4 = str4 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str4 = str4 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str4 = str4 + "<td>" + row["SMO"].ToString() + "</td>";
                        str4 = str4 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str4 = str4 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str4 = str4 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num12 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "4")
                {
                    num13 = num13 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num13 == new Decimal(0))
                    {
                        str5 += "<td></td>";
                        str5 += "<td></td>";
                        str5 += "<td></td>";
                        str5 += "<td></td>";
                        str5 += "<td></td>";
                        str5 += "<td></td><td></td>";
                    }
                    else
                    {
                        str5 = str5 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str5 = str5 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str5 = str5 + "<td>" + row["SMO"].ToString() + "</td>";
                        str5 = str5 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str5 = str5 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str5 = str5 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num12 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "5")
                {
                    num14 = num14 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num14 == new Decimal(0))
                    {
                        str6 += "<td></td>";
                        str6 += "<td></td>";
                        str6 += "<td></td>";
                        str6 += "<td></td>";
                        str6 += "<td></td>";
                        str6 += "<td></td><td></td>";
                    }
                    else
                    {
                        str6 = str6 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str6 = str6 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str6 = str6 + "<td>" + row["SMO"].ToString() + "</td>";
                        str6 = str6 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str6 = str6 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str6 = str6 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num14 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "6")
                {
                    num15 = num15 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num15 == new Decimal(0))
                    {
                        str7 += "<td></td>";
                        str7 += "<td></td>";
                        str7 += "<td></td>";
                        str7 += "<td></td>";
                        str7 += "<td></td>";
                        str7 += "<td></td><td></td>";
                    }
                    else
                    {
                        str7 = str7 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str7 = str7 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str7 = str7 + "<td>" + row["SMO"].ToString() + "</td>";
                        str7 = str7 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str7 = str7 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str7 = str7 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num15 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "7")
                {
                    num16 = num16 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num16 == new Decimal(0))
                    {
                        str8 += "<td></td>";
                        str8 += "<td></td>";
                        str8 += "<td></td>";
                        str8 += "<td></td>";
                        str8 += "<td></td>";
                        str8 += "<td></td><td></td>";
                    }
                    else
                    {
                        str8 = str8 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str8 = str8 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str8 = str8 + "<td>" + row["SMO"].ToString() + "</td>";
                        str8 = str8 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str8 = str8 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str8 = str8 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num16 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "8")
                {
                    num17 = num17 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num17 == new Decimal(0))
                    {
                        str9 += "<td></td>";
                        str9 += "<td></td>";
                        str9 += "<td></td>";
                        str9 += "<td></td>";
                        str9 += "<td></td>";
                        str9 += "<td></td><td></td>";
                    }
                    else
                    {
                        str9 = str9 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str9 = str9 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str9 = str9 + "<td>" + row["SMO"].ToString() + "</td>";
                        str9 = str9 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str9 = str9 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str9 = str9 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num17 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "9")
                {
                    num18 = num18 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num18 == new Decimal(0))
                    {
                        str10 += "<td></td>";
                        str10 += "<td></td>";
                        str10 += "<td></td>";
                        str10 += "<td></td>";
                        str10 += "<td></td>";
                        str10 += "<td></td><td></td>";
                    }
                    else
                    {
                        str10 = str10 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str10 = str10 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str10 = str10 + "<td>" + row["SMO"].ToString() + "</td>";
                        str10 = str10 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str10 = str10 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str10 = str10 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num18 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "10")
                {
                    num19 = num19 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num19 == new Decimal(0))
                    {
                        str11 += "<td></td>";
                        str11 += "<td></td>";
                        str11 += "<td></td>";
                        str11 += "<td></td>";
                        str11 += "<td></td>";
                        str11 += "<td></td><td></td>";
                    }
                    else
                    {
                        str11 = str11 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str11 = str11 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str11 = str11 + "<td>" + row["SMO"].ToString() + "</td>";
                        str11 = str11 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str11 = str11 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str11 = str11 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num19 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "11")
                {
                    num20 = num20 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num20 == new Decimal(0))
                    {
                        str12 += "<td></td>";
                        str12 += "<td></td>";
                        str12 += "<td></td>";
                        str12 += "<td></td>";
                        str12 += "<td></td>";
                        str12 += "<td></td><td></td>";
                    }
                    else
                    {
                        str12 = str12 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str12 = str12 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str12 = str12 + "<td>" + row["SMO"].ToString() + "</td>";
                        str12 = str12 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str12 = str12 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str12 = str12 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num20 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "12")
                {
                    num21 = num21 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num21 == new Decimal(0))
                    {
                        str13 += "<td></td>";
                        str13 += "<td></td>";
                        str13 += "<td></td>";
                        str13 += "<td></td>";
                        str13 += "<td></td>";
                        str13 += "<td></td><td></td>";
                    }
                    else
                    {
                        str13 = str13 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str13 = str13 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str13 = str13 + "<td>" + row["SMO"].ToString() + "</td>";
                        str13 = str13 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str13 = str13 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str13 = str13 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num21 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "13")
                {
                    num22 = num22 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num22 == new Decimal(0))
                    {
                        str14 += "<td></td>";
                        str14 += "<td></td>";
                        str14 += "<td></td>";
                        str14 += "<td></td>";
                        str14 += "<td></td>";
                        str14 += "<td></td><td></td>";
                    }
                    else
                    {
                        str14 = str14 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str14 = str14 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str14 = str14 + "<td>" + row["SMO"].ToString() + "</td>";
                        str14 = str14 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str14 = str14 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str14 = str14 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num22 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "14")
                {
                    num23 = num23 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num23 == new Decimal(0))
                    {
                        str15 += "<td></td>";
                        str15 += "<td></td>";
                        str15 += "<td></td>";
                        str15 += "<td></td>";
                        str15 += "<td></td>";
                        str15 += "<td></td><td></td>";
                    }
                    else
                    {
                        str15 = str15 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str15 = str15 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str15 = str15 + "<td>" + row["SMO"].ToString() + "</td>";
                        str15 = str15 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str15 = str15 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str15 = str15 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num23 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "15")
                {
                    num24 = num24 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num24 == new Decimal(0))
                    {
                        str16 += "<td></td>";
                        str16 += "<td></td>";
                        str16 += "<td></td>";
                        str16 += "<td></td>";
                        str16 += "<td></td>";
                        str16 += "<td></td><td></td>";
                    }
                    else
                    {
                        str16 = str16 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str16 = str16 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str16 = str16 + "<td>" + row["SMO"].ToString() + "</td>";
                        str16 = str16 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str16 = str16 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str16 = str16 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num24 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "16")
                {
                    num25 = num25 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num25 == new Decimal(0))
                    {
                        str17 += "<td></td>";
                        str17 += "<td></td>";
                        str17 += "<td></td>";
                        str17 += "<td></td>";
                        str17 += "<td></td>";
                        str17 += "<td></td><td></td>";
                    }
                    else
                    {
                        str17 = str17 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str17 = str17 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str17 = str17 + "<td>" + row["SMO"].ToString() + "</td>";
                        str17 = str17 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str17 = str17 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str17 = str17 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num25 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "17")
                {
                    num26 = num26 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num26 == new Decimal(0))
                    {
                        str18 += "<td></td>";
                        str18 += "<td></td>";
                        str18 += "<td></td>";
                        str18 += "<td></td>";
                        str18 += "<td></td>";
                        str18 += "<td></td><td></td>";
                    }
                    else
                    {
                        str18 = str18 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str18 = str18 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str18 = str18 + "<td>" + row["SMO"].ToString() + "</td>";
                        str18 = str18 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str18 = str18 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str18 = str18 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num26 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "18")
                {
                    num27 = num27 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num27 == new Decimal(0))
                    {
                        str19 += "<td></td>";
                        str19 += "<td></td>";
                        str19 += "<td></td>";
                        str19 += "<td></td>";
                        str19 += "<td></td>";
                        str19 += "<td></td><td></td>";
                    }
                    else
                    {
                        str19 = str19 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str19 = str19 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str19 = str19 + "<td>" + row["SMO"].ToString() + "</td>";
                        str19 = str19 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str19 = str19 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str19 = str19 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num27 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "19")
                {
                    num28 = num28 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num28 == new Decimal(0))
                    {
                        str20 += "<td></td>";
                        str20 += "<td></td>";
                        str20 += "<td></td>";
                        str20 += "<td></td>";
                        str20 += "<td></td>";
                        str20 += "<td></td><td></td>";
                    }
                    else
                    {
                        str20 = str20 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str20 = str20 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str20 = str20 + "<td>" + row["SMO"].ToString() + "</td>";
                        str20 = str20 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str20 = str20 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str20 = str20 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num28 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "20")
                {
                    num29 = num29 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num29 == new Decimal(0))
                    {
                        str21 += "<td></td>";
                        str21 += "<td></td>";
                        str21 += "<td></td>";
                        str21 += "<td></td>";
                        str21 += "<td></td>";
                        str21 += "<td></td><td></td>";
                    }
                    else
                    {
                        str21 = str21 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str21 = str21 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str21 = str21 + "<td>" + row["SMO"].ToString() + "</td>";
                        str21 = str21 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str21 = str21 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str21 = str21 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num29 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "21")
                {
                    num30 = num30 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num30 == new Decimal(0))
                    {
                        str22 += "<td></td>";
                        str22 += "<td></td>";
                        str22 += "<td></td>";
                        str22 += "<td></td>";
                        str22 += "<td></td>";
                        str22 += "<td></td><td></td>";
                    }
                    else
                    {
                        str22 = str22 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str22 = str22 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str22 = str22 + "<td>" + row["SMO"].ToString() + "</td>";
                        str22 = str22 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str22 = str22 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str22 = str22 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num30 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "22")
                {
                    num31 = num31 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num31 == new Decimal(0))
                    {
                        str23 += "<td></td>";
                        str23 += "<td></td>";
                        str23 += "<td></td>";
                        str23 += "<td></td>";
                        str23 += "<td></td>";
                        str23 += "<td></td><td></td>";
                    }
                    else
                    {
                        str23 = str23 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str23 = str23 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str23 = str23 + "<td>" + row["SMO"].ToString() + "</td>";
                        str23 = str23 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str23 = str23 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str23 = str23 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num31 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "23")
                {
                    num32 = num32 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num32 == new Decimal(0))
                    {
                        str24 += "<td></td>";
                        str24 += "<td></td>";
                        str24 += "<td></td>";
                        str24 += "<td></td>";
                        str24 += "<td></td>";
                        str24 += "<td></td><td></td>";
                    }
                    else
                    {
                        str24 = str24 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str24 = str24 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str24 = str24 + "<td>" + row["SMO"].ToString() + "</td>";
                        str24 = str24 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str24 = str24 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str24 = str24 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num32 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "24")
                {
                    num33 = num33 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num33 == new Decimal(0))
                    {
                        str25 += "<td></td>";
                        str25 += "<td></td>";
                        str25 += "<td></td>";
                        str25 += "<td></td>";
                        str25 += "<td></td>";
                        str25 += "<td></td><td></td>";
                    }
                    else
                    {
                        str25 = str25 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str25 = str25 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str25 = str25 + "<td>" + row["SMO"].ToString() + "</td>";
                        str25 = str25 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str25 = str25 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str25 = str25 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num33 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "25")
                {
                    num34 = num34 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num34 == new Decimal(0))
                    {
                        str26 += "<td></td>";
                        str26 += "<td></td>";
                        str26 += "<td></td>";
                        str26 += "<td></td>";
                        str26 += "<td></td>";
                        str26 += "<td></td><td></td>";
                    }
                    else
                    {
                        str26 = str26 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str26 = str26 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str26 = str26 + "<td>" + row["SMO"].ToString() + "</td>";
                        str26 = str26 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str26 = str26 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str26 = str26 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num34 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "26")
                {
                    num35 = num35 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num35 == new Decimal(0))
                    {
                        str27 += "<td></td>";
                        str27 += "<td></td>";
                        str27 += "<td></td>";
                        str27 += "<td></td>";
                        str27 += "<td></td>";
                        str27 += "<td></td><td></td>";
                    }
                    else
                    {
                        str27 = str27 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str27 = str27 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str27 = str27 + "<td>" + row["SMO"].ToString() + "</td>";
                        str27 = str27 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str27 = str27 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str27 = str27 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num35 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "27")
                {
                    num36 = num36 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num36 == new Decimal(0))
                    {
                        str28 += "<td></td>";
                        str28 += "<td></td>";
                        str28 += "<td></td>";
                        str28 += "<td></td>";
                        str28 += "<td></td>";
                        str28 += "<td></td><td></td>";
                    }
                    else
                    {
                        str28 = str28 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str28 = str28 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str28 = str28 + "<td>" + row["SMO"].ToString() + "</td>";
                        str28 = str28 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str28 = str28 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str28 = str28 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num36 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "28")
                {
                    num37 = num37 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num37 == new Decimal(0))
                    {
                        str29 += "<td></td>";
                        str29 += "<td></td>";
                        str29 += "<td></td>";
                        str29 += "<td></td>";
                        str29 += "<td></td>";
                        str29 += "<td></td><td></td>";
                    }
                    else
                    {
                        str29 = str29 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str29 = str29 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str29 = str29 + "<td>" + row["SMO"].ToString() + "</td>";
                        str29 = str29 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str29 = str29 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str29 = str29 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num37 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "29")
                {
                    num38 = num38 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num38 == new Decimal(0))
                    {
                        str30 += "<td></td>";
                        str30 += "<td></td>";
                        str30 += "<td></td>";
                        str30 += "<td></td>";
                        str30 += "<td></td>";
                        str30 += "<td></td><td></td>";
                    }
                    else
                    {
                        str30 = str30 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str30 = str30 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str30 = str30 + "<td>" + row["SMO"].ToString() + "</td>";
                        str30 = str30 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str30 = str30 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str30 = str30 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num38 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "30")
                {
                    num39 = num39 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num39 == new Decimal(0))
                    {
                        str31 += "<td></td>";
                        str31 += "<td></td>";
                        str31 += "<td></td>";
                        str31 += "<td></td>";
                        str31 += "<td></td>";
                        str31 += "<td></td><td></td>";
                    }
                    else
                    {
                        str31 = str31 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str31 = str31 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str31 = str31 + "<td>" + row["SMO"].ToString() + "</td>";
                        str31 = str31 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str31 = str31 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str31 = str31 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num39 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "31")
                {
                    num40 = num40 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num40 == new Decimal(0))
                    {
                        str32 += "<td></td>";
                        str32 += "<td></td>";
                        str32 += "<td></td>";
                        str32 += "<td></td>";
                        str32 += "<td></td>";
                        str32 += "<td></td><td></td>";
                    }
                    else
                    {
                        str32 = str32 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str32 = str32 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str32 = str32 + "<td>" + row["SMO"].ToString() + "</td>";
                        str32 = str32 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str32 = str32 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str32 = str32 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num40 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "32")
                {
                    num41 = num41 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num41 == new Decimal(0))
                    {
                        str33 += "<td></td>";
                        str33 += "<td></td>";
                        str33 += "<td></td>";
                        str33 += "<td></td>";
                        str33 += "<td></td>";
                        str33 += "<td></td><td></td>";
                    }
                    else
                    {
                        str33 = str33 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str33 = str33 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str33 = str33 + "<td>" + row["SMO"].ToString() + "</td>";
                        str33 = str33 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str33 = str33 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str33 = str33 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num41 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "33")
                {
                    num42 = num42 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num42 == new Decimal(0))
                    {
                        str34 += "<td></td>";
                        str34 += "<td></td>";
                        str34 += "<td></td>";
                        str34 += "<td></td>";
                        str34 += "<td></td>";
                        str34 += "<td></td><td></td>";
                    }
                    else
                    {
                        str34 = str34 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str34 = str34 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str34 = str34 + "<td>" + row["SMO"].ToString() + "</td>";
                        str34 = str34 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str34 = str34 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str34 = str34 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num42 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "34")
                {
                    num43 = num43 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num43 == new Decimal(0))
                    {
                        str35 += "<td></td>";
                        str35 += "<td></td>";
                        str35 += "<td></td>";
                        str35 += "<td></td>";
                        str35 += "<td></td>";
                        str35 += "<td></td><td></td>";
                    }
                    else
                    {
                        str35 = str35 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str35 = str35 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str35 = str35 + "<td>" + row["SMO"].ToString() + "</td>";
                        str35 = str35 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str35 = str35 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str35 = str35 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num43 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "35")
                {
                    num44 = num44 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num44 == new Decimal(0))
                    {
                        str36 += "<td></td>";
                        str36 += "<td></td>";
                        str36 += "<td></td>";
                        str36 += "<td></td>";
                        str36 += "<td></td>";
                        str36 += "<td></td><td></td>";
                    }
                    else
                    {
                        str36 = str36 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str36 = str36 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str36 = str36 + "<td>" + row["SMO"].ToString() + "</td>";
                        str36 = str36 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str36 = str36 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str36 = str36 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num44 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "36")
                {
                    num45 = num45 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num45 == new Decimal(0))
                    {
                        str37 += "<td></td>";
                        str37 += "<td></td>";
                        str37 += "<td></td>";
                        str37 += "<td></td>";
                        str37 += "<td></td>";
                        str37 += "<td></td><td></td>";
                    }
                    else
                    {
                        str37 = str37 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str37 = str37 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str37 = str37 + "<td>" + row["SMO"].ToString() + "</td>";
                        str37 = str37 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str37 = str37 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str37 = str37 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num45 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "37")
                {
                    num46 = num46 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num46 == new Decimal(0))
                    {
                        str38 += "<td></td>";
                        str38 += "<td></td>";
                        str38 += "<td></td>";
                        str38 += "<td></td>";
                        str38 += "<td></td>";
                        str38 += "<td></td><td></td>";
                    }
                    else
                    {
                        str38 = str38 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str38 = str38 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str38 = str38 + "<td>" + row["SMO"].ToString() + "</td>";
                        str38 = str38 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str38 = str38 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str38 = str38 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num46 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "38")
                {
                    num47 = num47 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num47 == new Decimal(0))
                    {
                        str39 += "<td></td>";
                        str39 += "<td></td>";
                        str39 += "<td></td>";
                        str39 += "<td></td>";
                        str39 += "<td></td>";
                        str39 += "<td></td><td></td>";
                    }
                    else
                    {
                        str39 = str39 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str39 = str39 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str39 = str39 + "<td>" + row["SMO"].ToString() + "</td>";
                        str39 = str39 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str39 = str39 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str39 = str39 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num47 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "39")
                {
                    num48 = num48 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num48 == new Decimal(0))
                    {
                        str40 += "<td></td>";
                        str40 += "<td></td>";
                        str40 += "<td></td>";
                        str40 += "<td></td>";
                        str40 += "<td></td>";
                        str40 += "<td></td><td></td>";
                    }
                    else
                    {
                        str40 = str40 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str40 = str40 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str40 = str40 + "<td>" + row["SMO"].ToString() + "</td>";
                        str40 = str40 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str40 = str40 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str40 = str40 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num48 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "40")
                {
                    num49 = num49 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num49 == new Decimal(0))
                    {
                        str41 += "<td></td>";
                        str41 += "<td></td>";
                        str41 += "<td></td>";
                        str41 += "<td></td>";
                        str41 += "<td></td>";
                        str41 += "<td></td><td></td>";
                    }
                    else
                    {
                        str41 = str41 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str41 = str41 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str41 = str41 + "<td>" + row["SMO"].ToString() + "</td>";
                        str41 = str41 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str41 = str41 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str41 = str41 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num49 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "41")
                {
                    num50 = num50 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num50 == new Decimal(0))
                    {
                        str42 += "<td></td>";
                        str42 += "<td></td>";
                        str42 += "<td></td>";
                        str42 += "<td></td>";
                        str42 += "<td></td>";
                        str42 += "<td></td><td></td>";
                    }
                    else
                    {
                        str42 = str42 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str42 = str42 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str42 = str42 + "<td>" + row["SMO"].ToString() + "</td>";
                        str42 = str42 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str42 = str42 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str42 = str42 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num50 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "42")
                {
                    num51 = num51 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num51 == new Decimal(0))
                    {
                        str43 += "<td></td>";
                        str43 += "<td></td>";
                        str43 += "<td></td>";
                        str43 += "<td></td>";
                        str43 += "<td></td>";
                        str43 += "<td></td><td></td>";
                    }
                    else
                    {
                        str43 = str43 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str43 = str43 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str43 = str43 + "<td>" + row["SMO"].ToString() + "</td>";
                        str43 = str43 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str43 = str43 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str43 = str43 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num51 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "43")
                {
                    num52 = num52 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num52 == new Decimal(0))
                    {
                        str44 += "<td></td>";
                        str44 += "<td></td>";
                        str44 += "<td></td>";
                        str44 += "<td></td>";
                        str44 += "<td></td>";
                        str44 += "<td></td><td></td>";
                    }
                    else
                    {
                        str44 = str44 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str44 = str44 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str44 = str44 + "<td>" + row["SMO"].ToString() + "</td>";
                        str44 = str44 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str44 = str44 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str44 = str44 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num52 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "44")
                {
                    num53 = num53 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num53 == new Decimal(0))
                    {
                        str45 += "<td></td>";
                        str45 += "<td></td>";
                        str45 += "<td></td>";
                        str45 += "<td></td>";
                        str45 += "<td></td>";
                        str45 += "<td></td><td></td>";
                    }
                    else
                    {
                        str45 = str45 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str45 = str45 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str45 = str45 + "<td>" + row["SMO"].ToString() + "</td>";
                        str45 = str45 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str45 = str45 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str45 = str45 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num53 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "45")
                {
                    num54 = num54 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num54 == new Decimal(0))
                    {
                        str46 += "<td></td>";
                        str46 += "<td></td>";
                        str46 += "<td></td>";
                        str46 += "<td></td>";
                        str46 += "<td></td>";
                        str46 += "<td></td><td></td>";
                    }
                    else
                    {
                        str46 = str46 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str46 = str46 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str46 = str46 + "<td>" + row["SMO"].ToString() + "</td>";
                        str46 = str46 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str46 = str46 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str46 = str46 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num54 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "46")
                {
                    num55 = num55 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num55 == new Decimal(0))
                    {
                        str47 += "<td></td>";
                        str47 += "<td></td>";
                        str47 += "<td></td>";
                        str47 += "<td></td>";
                        str47 += "<td></td>";
                        str47 += "<td></td><td></td>";
                    }
                    else
                    {
                        str47 = str47 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str47 = str47 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str47 = str47 + "<td>" + row["SMO"].ToString() + "</td>";
                        str47 = str47 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str47 = str47 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str47 = str47 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num55 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "47")
                {
                    num56 = num56 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num56 == new Decimal(0))
                    {
                        str48 += "<td></td>";
                        str48 += "<td></td>";
                        str48 += "<td></td>";
                        str48 += "<td></td>";
                        str48 += "<td></td>";
                        str48 += "<td></td><td></td>";
                    }
                    else
                    {
                        str48 = str48 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str48 = str48 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str48 = str48 + "<td>" + row["SMO"].ToString() + "</td>";
                        str48 = str48 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str48 = str48 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str48 = str48 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num56 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "48")
                {
                    num57 = num57 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num57 == new Decimal(0))
                    {
                        str49 += "<td></td>";
                        str49 += "<td></td>";
                        str49 += "<td></td>";
                        str49 += "<td></td>";
                        str49 += "<td></td>";
                        str49 += "<td></td><td></td>";
                    }
                    else
                    {
                        str49 = str49 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str49 = str49 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str49 = str49 + "<td>" + row["SMO"].ToString() + "</td>";
                        str49 = str49 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str49 = str49 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str49 = str49 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num57 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "49")
                {
                    num58 = num58 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num58 == new Decimal(0))
                    {
                        str50 += "<td></td>";
                        str50 += "<td></td>";
                        str50 += "<td></td>";
                        str50 += "<td></td>";
                        str50 += "<td></td>";
                        str50 += "<td></td><td></td>";
                    }
                    else
                    {
                        str50 = str50 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str50 = str50 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str50 = str50 + "<td>" + row["SMO"].ToString() + "</td>";
                        str50 = str50 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str50 = str50 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str50 = str50 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num58 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "50")
                {
                    num59 = num59 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num59 == new Decimal(0))
                    {
                        str51 += "<td></td>";
                        str51 += "<td></td>";
                        str51 += "<td></td>";
                        str51 += "<td></td>";
                        str51 += "<td></td>";
                        str51 += "<td></td><td></td>";
                    }
                    else
                    {
                        str51 = str51 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str51 = str51 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str51 = str51 + "<td>" + row["SMO"].ToString() + "</td>";
                        str51 = str51 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str51 = str51 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str51 = str51 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num59 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "51")
                {
                    num60 = num60 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num60 == new Decimal(0))
                    {
                        str52 += "<td></td>";
                        str52 += "<td></td>";
                        str52 += "<td></td>";
                        str52 += "<td></td>";
                        str52 += "<td></td>";
                        str52 += "<td></td><td></td>";
                    }
                    else
                    {
                        str52 = str52 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str52 = str52 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str52 = str52 + "<td>" + row["SMO"].ToString() + "</td>";
                        str52 = str52 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str52 = str52 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str52 = str52 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num60 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "52")
                {
                    num61 = num61 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num61 == new Decimal(0))
                    {
                        str53 += "<td></td>";
                        str53 += "<td></td>";
                        str53 += "<td></td>";
                        str53 += "<td></td>";
                        str53 += "<td></td>";
                        str53 += "<td></td><td></td>";
                    }
                    else
                    {
                        str53 = str53 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str53 = str53 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str53 = str53 + "<td>" + row["SMO"].ToString() + "</td>";
                        str53 = str53 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str53 = str53 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str53 = str53 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num61 + "</td>";
                    }
                }
                if (row["Semana"].ToString() == "53")
                {
                    num62 = num62 + Convert.ToDecimal(row["Cancun"].ToString()) + Convert.ToDecimal(row["Abastos"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["Monterrey"].ToString()) + Convert.ToDecimal(row["Guadalajara"].ToString()) + Convert.ToDecimal(row["GAB"].ToString());
                    if (num62 == new Decimal(0))
                    {
                        str54 += "<td></td>";
                        str54 += "<td></td>";
                        str54 += "<td></td>";
                        str54 += "<td></td>";
                        str54 += "<td></td>";
                        str54 += "<td></td><td></td>";
                    }
                    else
                    {
                        str54 = str54 + "<td>" + row["Cancun"].ToString() + "</td>";
                        str54 = str54 + "<td>" + row["Abastos"].ToString() + "</td>";
                        str54 = str54 + "<td>" + row["SMO"].ToString() + "</td>";
                        str54 = str54 + "<td>" + row["Monterrey"].ToString() + "</td>";
                        str54 = str54 + "<td>" + row["Guadalajara"].ToString() + "</td>";
                        str54 = str54 + "<td>" + row["GAB"].ToString() + "</td><td>" + (object)num62 + "</td>";
                    }
                }
            }
            string str58 = str2 + "</tr>";
            string str59 = str3 + "</tr>";
            string str60 = str4 + "</tr>";
            string str61 = str5 + "</tr>";
            string str62 = str6 + "</tr>";
            string str63 = str7 + "</tr>";
            string str64 = str8 + "</tr>";
            string str65 = str9 + "</tr>";
            string str66 = str10 + "</tr>";
            string str67 = str11 + "</tr>";
            string str68 = str12 + "</tr>";
            string str69 = str13 + "</tr>";
            string str70 = str14 + "</tr>";
            string str71 = str15 + "</tr>";
            string str72 = str16 + "</tr>";
            string str73 = str17 + "</tr>";
            string str74 = str18 + "</tr>";
            string str75 = str19 + "</tr>";
            string str76 = str20 + "</tr>";
            string str77 = str21 + "</tr>";
            string str78 = str22 + "</tr>";
            string str79 = str23 + "</tr>";
            string str80 = str24 + "</tr>";
            string str81 = str25 + "</tr>";
            string str82 = str26 + "</tr>";
            string str83 = str27 + "</tr>";
            string str84 = str28 + "</tr>";
            string str85 = str29 + "</tr>";
            string str86 = str30 + "</tr>";
            string str87 = str31 + "</tr>";
            string str88 = str32 + "</tr>";
            string str89 = str33 + "</tr>";
            string str90 = str34 + "</tr>";
            string str91 = str35 + "</tr>";
            string str92 = str36 + "</tr>";
            string str93 = str37 + "</tr>";
            string str94 = str38 + "</tr>";
            string str95 = str39 + "</tr>";
            string str96 = str40 + "</tr>";
            string str97 = str41 + "</tr>";
            string str98 = str42 + "</tr>";
            string str99 = str43 + "</tr>";
            string str100 = str44 + "</tr>";
            string str101 = str45 + "</tr>";
            string str102 = str46 + "</tr>";
            string str103 = str47 + "</tr>";
            string str104 = str48 + "</tr>";
            string str105 = str49 + "</tr>";
            string str106 = str50 + "</tr>";
            string str107 = str51 + "</tr>";
            string str108 = str52 + "</tr>";
            string str109 = str53 + "</tr>";
            string str110 = str54 + "</tr>";
            this.datosqueja1.InnerHtml = str57 + str58 + str59 + str60 + str61 + str62 + str63 + str64 + str65 + str66 + str67 + str68 + str69 + str70 + str71 + str72 + str73 + str74 + str75 + str76 + str77 + str78 + str79 + str80 + str81 + str82 + str83 + str84 + str85 + str86 + str87 + str88 + str89 + str90 + str91 + str92 + str93 + str94 + str95 + str96 + str97 + str98 + str99 + str100 + str101 + str102 + str103 + str104 + str105 + str106 + str107 + str108 + str109 + str110 + "</tbody>" + "</table>";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SEMANA", typeof(string));
            dataTable.Columns.Add(this.lblCedis.Text, typeof(string));
            dataTable.Columns.Add("TOTAL", typeof(string));
            dataTable.Rows.Add((object)"TOTAL", (object)"0", (object)"0");
            int num1 = 1;
            for (int index = 0; index < 54; ++index)
            {
                dataTable.Rows.Add((object)num1, (object)"0", (object)"0");
                ++num1;
            }
            this.conex.Open();
            this.comand = this.conex.CreateCommand();
            this.comand.CommandText = "SELECT que_sememb AS que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas WHERE cedis = '" + this.Session["cedis"].ToString() + "' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                this.comand.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, A.cedis FROM tb_mstr_quejas A JOIN tb_cat_areas_queja B ON A.area_queja = B.id_area AND B.area_nombre = 'EMPAQUE_AGUILARES' WHERE cedis = 'COMERCIALIZADORA GAB' AND que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            Decimal num2 = new Decimal(0);
            this.reader = this.comand.ExecuteReader();
            if (this.reader.HasRows)
            {
                while (this.reader.Read())
                {
                    string str = this.reader.GetValue(0).ToString().Trim();
                    foreach (DataRow dataRow in dataTable.Select("Semana = '" + str + "'"))
                    {
                        if (this.reader.GetValue(2).ToString().Trim() == "CANCUN")
                        {
                            dataRow["CANCUN"] = (object)(Convert.ToDecimal(dataRow["CANCUN"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "ABASTOS")
                        {
                            dataRow["ABASTOS"] = (object)(Convert.ToDecimal(dataRow["ABASTOS"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "SMO")
                        {
                            dataRow["SMO"] = (object)(Convert.ToDecimal(dataRow["SMO"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "MONTERREY")
                        {
                            dataRow["MONTERREY"] = (object)(Convert.ToDecimal(dataRow["MONTERREY"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "GUADALAJARA")
                        {
                            dataRow["GUADALAJARA"] = (object)(Convert.ToDecimal(dataRow["GUADALAJARA"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                        }
                        if (this.reader.GetValue(2).ToString().Trim() == "COMERCIALIZADORA GAB")
                        {
                            if (this.Session["cedis"].ToString() == "AGUILARES")
                            {
                                dataRow["AGUILARES"] = (object)(Convert.ToDecimal(dataRow["AGUILARES"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                                num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                            }
                            else
                            {
                                dataRow["COMERCIALIZADORA GAB"] = (object)(Convert.ToDecimal(dataRow["COMERCIALIZADORA GAB"].ToString()) + Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim()));
                                num2 += Convert.ToDecimal(this.reader.GetValue(1).ToString().Trim());
                            }
                        }
                    }
                }
            }
            this.reader.Close();
            this.reader.Dispose();
            this.comand.Dispose();
            this.conex.Close();
            Decimal num3 = new Decimal(0);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num3 += Convert.ToDecimal(row[1]);
                row[2] = row[1];
                if (row[1].ToString() == "0")
                    row[1] = (object)"";
                if (row[2].ToString() == "0")
                    row[2] = (object)"";
            }
            dataTable.Rows[0][1] = (object)num3.ToString();
            dataTable.Rows[0][2] = (object)num3.ToString();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=QuejasPorMes.xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            response.ContentEncoding = Encoding.Unicode;
            response.BinaryWrite(Encoding.Unicode.GetPreamble());
            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                {
                    DataGrid dataGrid = new DataGrid();
                    dataGrid.DataSource = (object)dataTable;
                    dataGrid.DataBind();
                    dataGrid.RenderControl(writer);
                    this.Response.Write(stringWriter.ToString());
                    dataGrid.Dispose();
                    this.Response.End();
                }
            }
        }

        protected void btnExcelTodos_Click(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("SEMANA", typeof(string));
            dataTable1.Columns.Add("CANCUN", typeof(string));
            dataTable1.Columns.Add("ABASTOS", typeof(string));
            dataTable1.Columns.Add("SMO", typeof(string));
            dataTable1.Columns.Add("MONTERREY", typeof(string));
            dataTable1.Columns.Add("GUADALAJARA", typeof(string));
            dataTable1.Columns.Add("GAB", typeof(string));
            dataTable1.Columns.Add("TOTAL", typeof(string));
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable1.Rows.Add((object)num1, (object)"0", (object)"0", (object)"0", (object)"0", (object)"0", (object)"0", (object)"0");
                ++num1;
            }
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = "SELECT que_sememb AS que_semana, count(que_folio) cantidad_quejas, cedis FROM tb_mstr_quejas WHERE que_status in ('A','T') AND que_fechaemb >= '" + fecha_1 + "' AND que_fechaemb <= '" + fecha_2 + "' group by cedis, que_sememb ORDER BY que_sememb, cedis";
            Decimal num2 = new Decimal(0);
            Decimal num3 = new Decimal(0);
            Decimal num4 = new Decimal(0);
            Decimal num5 = new Decimal(0);
            Decimal num6 = new Decimal(0);
            Decimal num7 = new Decimal(0);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    string str = sqlDataReader.GetValue(0).ToString().Trim();
                    foreach (DataRow dataRow in dataTable1.Select("Semana = '" + str + "'"))
                    {
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "CANCUN")
                        {
                            dataRow["CANCUN"] = (object)(Convert.ToDecimal(dataRow["CANCUN"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num2 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "ABASTOS")
                        {
                            dataRow["ABASTOS"] = (object)(Convert.ToDecimal(dataRow["ABASTOS"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num3 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "SMO")
                        {
                            dataRow["SMO"] = (object)(Convert.ToDecimal(dataRow["SMO"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num4 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "MONTERREY")
                        {
                            dataRow["MONTERREY"] = (object)(Convert.ToDecimal(dataRow["MONTERREY"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num5 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "GUADALAJARA")
                        {
                            dataRow["GUADALAJARA"] = (object)(Convert.ToDecimal(dataRow["GUADALAJARA"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num6 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                        if (sqlDataReader.GetValue(2).ToString().Trim() == "COMERCIALIZADORA GAB")
                        {
                            dataRow["GAB"] = (object)(Convert.ToDecimal(dataRow["GAB"].ToString()) + Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim()));
                            num7 += Convert.ToDecimal(sqlDataReader.GetValue(1).ToString().Trim());
                        }
                    }
                }
            }
            sqlDataReader.Close();
            sqlDataReader.Dispose();
            command.Dispose();
            sqlConnection.Close();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
                row["TOTAL"] = (object)(Convert.ToDecimal(row["CANCUN"].ToString()) + Convert.ToDecimal(row["ABASTOS"].ToString()) + Convert.ToDecimal(row["SMO"].ToString()) + Convert.ToDecimal(row["MONTERREY"].ToString()) + Convert.ToDecimal(row["GUADALAJARA"].ToString()) + Convert.ToDecimal(row["GAB"].ToString()));
            Decimal num8 = num2 + num3 + num4 + num5 + num6 + num7;
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("SEMANA", typeof(string));
            dataTable2.Columns.Add("CANCUN", typeof(string));
            dataTable2.Columns.Add("ABASTOS", typeof(string));
            dataTable2.Columns.Add("SMO", typeof(string));
            dataTable2.Columns.Add("MONTERREY", typeof(string));
            dataTable2.Columns.Add("GUADALAJARA", typeof(string));
            dataTable2.Columns.Add("GAB", typeof(string));
            dataTable2.Columns.Add("TOTAL", typeof(string));
            dataTable2.Rows.Add((object)"TOTAL", (object)num2.ToString(), (object)num3.ToString(), (object)num4.ToString(), (object)num5.ToString(), (object)num6.ToString(), (object)num7.ToString(), (object)num8.ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
                dataTable2.Rows.Add(row.ItemArray);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
            {
                if (row["CANCUN"].ToString() == "0")
                    row["CANCUN"] = (object)"";
                if (row["ABASTOS"].ToString() == "0")
                    row["ABASTOS"] = (object)"";
                if (row["SMO"].ToString() == "0")
                    row["SMO"] = (object)"";
                if (row["MONTERREY"].ToString() == "0")
                    row["MONTERREY"] = (object)"";
                if (row["GUADALAJARA"].ToString() == "0")
                    row["GUADALAJARA"] = (object)"";
                if (row["GAB"].ToString() == "0")
                    row["GAB"] = (object)"";
                if (row["TOTAL"].ToString() == "0")
                    row["TOTAL"] = (object)"";
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=QuejasSemanalesCedis.xls");
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
                    this.Response.Write(stringWriter.ToString());
                    dataGrid.Dispose();
                    this.Response.End();
                }
            }
        }
    }
}