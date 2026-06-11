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
    public partial class cant_semana : System.Web.UI.Page
    {
        private DataTable dtQuejas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Clave", typeof(string));
            dataTable.Columns.Add("Problema", typeof(string));
            dataTable.Columns.Add("1", typeof(string));
            dataTable.Columns.Add("2", typeof(string));
            dataTable.Columns.Add("3", typeof(string));
            dataTable.Columns.Add("4", typeof(string));
            dataTable.Columns.Add("5", typeof(string));
            dataTable.Columns.Add("6", typeof(string));
            dataTable.Columns.Add("7", typeof(string));
            dataTable.Columns.Add("8", typeof(string));
            dataTable.Columns.Add("9", typeof(string));
            dataTable.Columns.Add("10", typeof(string));
            dataTable.Columns.Add("11", typeof(string));
            dataTable.Columns.Add("12", typeof(string));
            dataTable.Columns.Add("13", typeof(string));
            dataTable.Columns.Add("14", typeof(string));
            dataTable.Columns.Add("15", typeof(string));
            dataTable.Columns.Add("16", typeof(string));
            dataTable.Columns.Add("17", typeof(string));
            dataTable.Columns.Add("18", typeof(string));
            dataTable.Columns.Add("19", typeof(string));
            dataTable.Columns.Add("20", typeof(string));
            dataTable.Columns.Add("21", typeof(string));
            dataTable.Columns.Add("22", typeof(string));
            dataTable.Columns.Add("23", typeof(string));
            dataTable.Columns.Add("24", typeof(string));
            dataTable.Columns.Add("25", typeof(string));
            dataTable.Columns.Add("26", typeof(string));
            dataTable.Columns.Add("27", typeof(string));
            dataTable.Columns.Add("28", typeof(string));
            dataTable.Columns.Add("29", typeof(string));
            dataTable.Columns.Add("30", typeof(string));
            dataTable.Columns.Add("31", typeof(string));
            dataTable.Columns.Add("32", typeof(string));
            dataTable.Columns.Add("33", typeof(string));
            dataTable.Columns.Add("34", typeof(string));
            dataTable.Columns.Add("35", typeof(string));
            dataTable.Columns.Add("36", typeof(string));
            dataTable.Columns.Add("37", typeof(string));
            dataTable.Columns.Add("38", typeof(string));
            dataTable.Columns.Add("39", typeof(string));
            dataTable.Columns.Add("40", typeof(string));
            dataTable.Columns.Add("41", typeof(string));
            dataTable.Columns.Add("42", typeof(string));
            dataTable.Columns.Add("43", typeof(string));
            dataTable.Columns.Add("44", typeof(string));
            dataTable.Columns.Add("45", typeof(string));
            dataTable.Columns.Add("46", typeof(string));
            dataTable.Columns.Add("47", typeof(string));
            dataTable.Columns.Add("48", typeof(string));
            dataTable.Columns.Add("49", typeof(string));
            dataTable.Columns.Add("50", typeof(string));
            dataTable.Columns.Add("51", typeof(string));
            dataTable.Columns.Add("52", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT * FROM tb_cat_problemas ORDER BY pro_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["Clave"] = (object)sqlDataReader1.GetValue(0).ToString().Trim();
                    row["Problema"] = (object)sqlDataReader1.GetValue(1).ToString().Trim();
                    row["1"] = (object)"0";
                    row["2"] = (object)"0";
                    row["3"] = (object)"0";
                    row["4"] = (object)"0";
                    row["5"] = (object)"0";
                    row["6"] = (object)"0";
                    row["7"] = (object)"0";
                    row["8"] = (object)"0";
                    row["9"] = (object)"0";
                    row["10"] = (object)"0";
                    row["11"] = (object)"0";
                    row["12"] = (object)"0";
                    row["13"] = (object)"0";
                    row["14"] = (object)"0";
                    row["15"] = (object)"0";
                    row["16"] = (object)"0";
                    row["17"] = (object)"0";
                    row["18"] = (object)"0";
                    row["19"] = (object)"0";
                    row["20"] = (object)"0";
                    row["21"] = (object)"0";
                    row["22"] = (object)"0";
                    row["23"] = (object)"0";
                    row["24"] = (object)"0";
                    row["25"] = (object)"0";
                    row["26"] = (object)"0";
                    row["27"] = (object)"0";
                    row["28"] = (object)"0";
                    row["29"] = (object)"0";
                    row["30"] = (object)"0";
                    row["31"] = (object)"0";
                    row["32"] = (object)"0";
                    row["33"] = (object)"0";
                    row["34"] = (object)"0";
                    row["35"] = (object)"0";
                    row["36"] = (object)"0";
                    row["37"] = (object)"0";
                    row["38"] = (object)"0";
                    row["39"] = (object)"0";
                    row["40"] = (object)"0";
                    row["41"] = (object)"0";
                    row["42"] = (object)"0";
                    row["43"] = (object)"0";
                    row["44"] = (object)"0";
                    row["45"] = (object)"0";
                    row["46"] = (object)"0";
                    row["47"] = (object)"0";
                    row["48"] = (object)"0";
                    row["49"] = (object)"0";
                    row["50"] = (object)"0";
                    row["51"] = (object)"0";
                    row["52"] = (object)"0";
                    dataTable.Rows.Add(row);
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            string str1 = DateTime.Now.Year.ToString();
            string str2 = "01/01/" + str1;
            string str3 = "31/12/" + str1;
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.cedis = '" + this.lblCedis.Text + "' AND a.que_status in ('A','T') order by a.que_fechaemb";
            if (this.lblCedis.Text == "AGUILARES")
                command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c, tb_cat_areas_queja d WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' AND a.que_status in ('A','T') AND a.cedis = 'COMERCIALIZADORA GAB' AND a.area_queja = d.id_area AND d.area_nombre = 'EMPAQUE_AGUILARES' order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(1).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        string str4 = sqlDataReader2["que_semana"].ToString().Trim();
                        int num;
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["1"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["1"] = (object)str5;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["2"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["2"] = (object)str5;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["3"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["3"] = (object)str5;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["4"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["4"] = (object)str5;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["5"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["5"] = (object)str5;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["6"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["6"] = (object)str5;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["7"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["7"] = (object)str5;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["8"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["8"] = (object)str5;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["9"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["9"] = (object)str5;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["10"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["10"] = (object)str5;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["11"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["11"] = (object)str5;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["12"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["12"] = (object)str5;
                        }
                        if (str4 == "13")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["13"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["13"] = (object)str5;
                        }
                        if (str4 == "14")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["14"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["14"] = (object)str5;
                        }
                        if (str4 == "15")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["15"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["15"] = (object)str5;
                        }
                        if (str4 == "16")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["16"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["16"] = (object)str5;
                        }
                        if (str4 == "17")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["17"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["17"] = (object)str5;
                        }
                        if (str4 == "18")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["18"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["18"] = (object)str5;
                        }
                        if (str4 == "19")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["19"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["19"] = (object)str5;
                        }
                        if (str4 == "20")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["20"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["20"] = (object)str5;
                        }
                        if (str4 == "21")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["21"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["21"] = (object)str5;
                        }
                        if (str4 == "22")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["22"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["22"] = (object)str5;
                        }
                        if (str4 == "23")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["23"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["23"] = (object)str5;
                        }
                        if (str4 == "24")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["24"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["24"] = (object)str5;
                        }
                        if (str4 == "25")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["25"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["25"] = (object)str5;
                        }
                        if (str4 == "26")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["26"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["26"] = (object)str5;
                        }
                        if (str4 == "27")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["27"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["27"] = (object)str5;
                        }
                        if (str4 == "28")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["28"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["28"] = (object)str5;
                        }
                        if (str4 == "29")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["29"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["29"] = (object)str5;
                        }
                        if (str4 == "30")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["30"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["30"] = (object)str5;
                        }
                        if (str4 == "31")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["31"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["31"] = (object)str5;
                        }
                        if (str4 == "32")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["32"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["32"] = (object)str5;
                        }
                        if (str4 == "33")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["33"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["33"] = (object)str5;
                        }
                        if (str4 == "34")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["34"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["34"] = (object)str5;
                        }
                        if (str4 == "35")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["35"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["35"] = (object)str5;
                        }
                        if (str4 == "36")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["36"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["36"] = (object)str5;
                        }
                        if (str4 == "37")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["37"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["37"] = (object)str5;
                        }
                        if (str4 == "38")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["38"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["38"] = (object)str5;
                        }
                        if (str4 == "39")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["39"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["39"] = (object)str5;
                        }
                        if (str4 == "40")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["40"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["40"] = (object)str5;
                        }
                        if (str4 == "41")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["41"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["41"] = (object)str5;
                        }
                        if (str4 == "42")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["42"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["42"] = (object)str5;
                        }
                        if (str4 == "43")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["43"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["43"] = (object)str5;
                        }
                        if (str4 == "44")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["44"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["44"] = (object)str5;
                        }
                        if (str4 == "45")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["45"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["45"] = (object)str5;
                        }
                        if (str4 == "46")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["46"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["46"] = (object)str5;
                        }
                        if (str4 == "47")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["47"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["47"] = (object)str5;
                        }
                        if (str4 == "48")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["48"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["48"] = (object)str5;
                        }
                        if (str4 == "49")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["49"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["49"] = (object)str5;
                        }
                        if (str4 == "50")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["50"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["50"] = (object)str5;
                        }
                        if (str4 == "51'")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["51"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["51"] = (object)str5;
                        }
                        if (str4 == "52")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["52"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["52"] = (object)str5;
                        }
                    }
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = 0;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            int num28 = 0;
            int num29 = 0;
            int num30 = 0;
            int num31 = 0;
            int num32 = 0;
            int num33 = 0;
            int num34 = 0;
            int num35 = 0;
            int num36 = 0;
            int num37 = 0;
            int num38 = 0;
            int num39 = 0;
            int num40 = 0;
            int num41 = 0;
            int num42 = 0;
            int num43 = 0;
            int num44 = 0;
            int num45 = 0;
            int num46 = 0;
            int num47 = 0;
            int num48 = 0;
            int num49 = 0;
            int num50 = 0;
            int num51 = 0;
            int num52 = 0;
            string str6 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>" + "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem1</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem2</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem3</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem4</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem5</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem6</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem7</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem8</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem9</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem10</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem11</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem12</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem13</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem14</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem15</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem16</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem17</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem18</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem19</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem20</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem21</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem22</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem23</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem24</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem25</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem26</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem27</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem28</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem29</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem30</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem31</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem32</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem33</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem34</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem35</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem36</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem37</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem38</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem39</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem40</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem41</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem42</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem43</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem44</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem45</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem46</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem47</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem48</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem49</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem50</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem51</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem52</font></th>" + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row["1"].ToString());
                num2 += Convert.ToInt32(row["2"].ToString());
                num3 += Convert.ToInt32(row["3"].ToString());
                num4 += Convert.ToInt32(row["4"].ToString());
                num5 += Convert.ToInt32(row["5"].ToString());
                num6 += Convert.ToInt32(row["6"].ToString());
                num7 += Convert.ToInt32(row["7"].ToString());
                num8 += Convert.ToInt32(row["8"].ToString());
                num9 += Convert.ToInt32(row["9"].ToString());
                num10 += Convert.ToInt32(row["10"].ToString());
                num11 += Convert.ToInt32(row["11"].ToString());
                num12 += Convert.ToInt32(row["12"].ToString());
                num13 += Convert.ToInt32(row["13"].ToString());
                num14 += Convert.ToInt32(row["14"].ToString());
                num15 += Convert.ToInt32(row["15"].ToString());
                num16 += Convert.ToInt32(row["16"].ToString());
                num17 += Convert.ToInt32(row["17"].ToString());
                num18 += Convert.ToInt32(row["18"].ToString());
                num19 += Convert.ToInt32(row["19"].ToString());
                num20 += Convert.ToInt32(row["20"].ToString());
                num21 += Convert.ToInt32(row["21"].ToString());
                num22 += Convert.ToInt32(row["22"].ToString());
                num23 += Convert.ToInt32(row["23"].ToString());
                num24 += Convert.ToInt32(row["24"].ToString());
                num25 += Convert.ToInt32(row["25"].ToString());
                num26 += Convert.ToInt32(row["26"].ToString());
                num27 += Convert.ToInt32(row["27"].ToString());
                num28 += Convert.ToInt32(row["28"].ToString());
                num29 += Convert.ToInt32(row["29"].ToString());
                num30 += Convert.ToInt32(row["30"].ToString());
                num31 += Convert.ToInt32(row["31"].ToString());
                num32 += Convert.ToInt32(row["32"].ToString());
                num33 += Convert.ToInt32(row["33"].ToString());
                num34 += Convert.ToInt32(row["34"].ToString());
                num35 += Convert.ToInt32(row["35"].ToString());
                num36 += Convert.ToInt32(row["36"].ToString());
                num37 += Convert.ToInt32(row["37"].ToString());
                num38 += Convert.ToInt32(row["38"].ToString());
                num39 += Convert.ToInt32(row["39"].ToString());
                num40 += Convert.ToInt32(row["40"].ToString());
                num41 += Convert.ToInt32(row["41"].ToString());
                num42 += Convert.ToInt32(row["42"].ToString());
                num43 += Convert.ToInt32(row["43"].ToString());
                num44 += Convert.ToInt32(row["44"].ToString());
                num45 += Convert.ToInt32(row["45"].ToString());
                num46 += Convert.ToInt32(row["46"].ToString());
                num47 += Convert.ToInt32(row["47"].ToString());
                num48 += Convert.ToInt32(row["48"].ToString());
                num49 += Convert.ToInt32(row["49"].ToString());
                num50 += Convert.ToInt32(row["50"].ToString());
                num51 += Convert.ToInt32(row["51"].ToString());
                num52 += Convert.ToInt32(row["52"].ToString());
            }
            string str7 = str6 + "<tr>" + "<td align='right'><b>TOTALES</b></td>" + "<td>" + (object)num1 + "</td>" + "<td>" + (object)num2 + "</td>" + "<td>" + (object)num3 + "</td>" + "<td>" + (object)num4 + "</td>" + "<td>" + (object)num5 + "</td>" + "<td>" + (object)num6 + "</td>" + "<td>" + (object)num7 + "</td>" + "<td>" + (object)num8 + "</td>" + "<td>" + (object)num9 + "</td>" + "<td>" + (object)num10 + "</td>" + "<td>" + (object)num11 + "</td>" + "<td>" + (object)num12 + "</td>" + "<td>" + (object)num13 + "</td>" + "<td>" + (object)num14 + "</td>" + "<td>" + (object)num15 + "</td>" + "<td>" + (object)num16 + "</td>" + "<td>" + (object)num17 + "</td>" + "<td>" + (object)num18 + "</td>" + "<td>" + (object)num19 + "</td>" + "<td>" + (object)num20 + "</td>" + "<td>" + (object)num21 + "</td>" + "<td>" + (object)num22 + "</td>" + "<td>" + (object)num23 + "</td>" + "<td>" + (object)num24 + "</td>" + "<td>" + (object)num25 + "</td>" + "<td>" + (object)num26 + "</td>" + "<td>" + (object)num27 + "</td>" + "<td>" + (object)num28 + "</td>" + "<td>" + (object)num29 + "</td>" + "<td>" + (object)num30 + "</td>" + "<td>" + (object)num31 + "</td>" + "<td>" + (object)num32 + "</td>" + "<td>" + (object)num33 + "</td>" + "<td>" + (object)num34 + "</td>" + "<td>" + (object)num35 + "</td>" + "<td>" + (object)num36 + "</td>" + "<td>" + (object)num37 + "</td>" + "<td>" + (object)num38 + "</td>" + "<td>" + (object)num39 + "</td>" + "<td>" + (object)num40 + "</td>" + "<td>" + (object)num41 + "</td>" + "<td>" + (object)num42 + "</td>" + "<td>" + (object)num43 + "</td>" + "<td>" + (object)num44 + "</td>" + "<td>" + (object)num45 + "</td>" + "<td>" + (object)num46 + "</td>" + "<td>" + (object)num47 + "</td>" + "<td>" + (object)num48 + "</td>" + "<td>" + (object)num49 + "</td>" + "<td>" + (object)num50 + "</td>" + "<td>" + (object)num51 + "</td>" + "<td>" + (object)num52 + "</td>" + "</tr>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                str7 += "<tr>";
                str7 = str7 + "<td>" + row["Problema"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["1"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["2"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["3"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["4"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["5"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["6"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["7"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["8"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["9"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["10"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["11"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["12"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["13"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["14"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["15"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["16"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["17"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["18"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["19"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["20"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["21"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["22"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["23"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["24"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["25"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["26"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["27"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["28"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["29"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["30"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["31"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["32"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["33"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["34"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["35"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["36"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["37"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["38"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["39"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["40"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["41"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["42"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["43"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["44"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["45"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["46"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["47"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["48"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["49"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["50"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["51"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["52"].ToString() + "</td>";
                str7 += "</tr>";
            }
            this.datosqueja.InnerHtml = str7 + "</tbody>" + "</table>";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("reportes.aspx");
        }

        protected void btnCedis_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Clave", typeof(string));
            dataTable.Columns.Add("Problema", typeof(string));
            dataTable.Columns.Add("1", typeof(string));
            dataTable.Columns.Add("2", typeof(string));
            dataTable.Columns.Add("3", typeof(string));
            dataTable.Columns.Add("4", typeof(string));
            dataTable.Columns.Add("5", typeof(string));
            dataTable.Columns.Add("6", typeof(string));
            dataTable.Columns.Add("7", typeof(string));
            dataTable.Columns.Add("8", typeof(string));
            dataTable.Columns.Add("9", typeof(string));
            dataTable.Columns.Add("10", typeof(string));
            dataTable.Columns.Add("11", typeof(string));
            dataTable.Columns.Add("12", typeof(string));
            dataTable.Columns.Add("13", typeof(string));
            dataTable.Columns.Add("14", typeof(string));
            dataTable.Columns.Add("15", typeof(string));
            dataTable.Columns.Add("16", typeof(string));
            dataTable.Columns.Add("17", typeof(string));
            dataTable.Columns.Add("18", typeof(string));
            dataTable.Columns.Add("19", typeof(string));
            dataTable.Columns.Add("20", typeof(string));
            dataTable.Columns.Add("21", typeof(string));
            dataTable.Columns.Add("22", typeof(string));
            dataTable.Columns.Add("23", typeof(string));
            dataTable.Columns.Add("24", typeof(string));
            dataTable.Columns.Add("25", typeof(string));
            dataTable.Columns.Add("26", typeof(string));
            dataTable.Columns.Add("27", typeof(string));
            dataTable.Columns.Add("28", typeof(string));
            dataTable.Columns.Add("29", typeof(string));
            dataTable.Columns.Add("30", typeof(string));
            dataTable.Columns.Add("31", typeof(string));
            dataTable.Columns.Add("32", typeof(string));
            dataTable.Columns.Add("33", typeof(string));
            dataTable.Columns.Add("34", typeof(string));
            dataTable.Columns.Add("35", typeof(string));
            dataTable.Columns.Add("36", typeof(string));
            dataTable.Columns.Add("37", typeof(string));
            dataTable.Columns.Add("38", typeof(string));
            dataTable.Columns.Add("39", typeof(string));
            dataTable.Columns.Add("40", typeof(string));
            dataTable.Columns.Add("41", typeof(string));
            dataTable.Columns.Add("42", typeof(string));
            dataTable.Columns.Add("43", typeof(string));
            dataTable.Columns.Add("44", typeof(string));
            dataTable.Columns.Add("45", typeof(string));
            dataTable.Columns.Add("46", typeof(string));
            dataTable.Columns.Add("47", typeof(string));
            dataTable.Columns.Add("48", typeof(string));
            dataTable.Columns.Add("49", typeof(string));
            dataTable.Columns.Add("50", typeof(string));
            dataTable.Columns.Add("51", typeof(string));
            dataTable.Columns.Add("52", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT * FROM tb_cat_problemas ORDER BY pro_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["Clave"] = (object)sqlDataReader1.GetValue(0).ToString().Trim();
                    row["Problema"] = (object)sqlDataReader1.GetValue(1).ToString().Trim();
                    row["1"] = (object)"0";
                    row["2"] = (object)"0";
                    row["3"] = (object)"0";
                    row["4"] = (object)"0";
                    row["5"] = (object)"0";
                    row["6"] = (object)"0";
                    row["7"] = (object)"0";
                    row["8"] = (object)"0";
                    row["9"] = (object)"0";
                    row["10"] = (object)"0";
                    row["11"] = (object)"0";
                    row["12"] = (object)"0";
                    row["13"] = (object)"0";
                    row["14"] = (object)"0";
                    row["15"] = (object)"0";
                    row["16"] = (object)"0";
                    row["17"] = (object)"0";
                    row["18"] = (object)"0";
                    row["19"] = (object)"0";
                    row["20"] = (object)"0";
                    row["21"] = (object)"0";
                    row["22"] = (object)"0";
                    row["23"] = (object)"0";
                    row["24"] = (object)"0";
                    row["25"] = (object)"0";
                    row["26"] = (object)"0";
                    row["27"] = (object)"0";
                    row["28"] = (object)"0";
                    row["29"] = (object)"0";
                    row["30"] = (object)"0";
                    row["31"] = (object)"0";
                    row["32"] = (object)"0";
                    row["33"] = (object)"0";
                    row["34"] = (object)"0";
                    row["35"] = (object)"0";
                    row["36"] = (object)"0";
                    row["37"] = (object)"0";
                    row["38"] = (object)"0";
                    row["39"] = (object)"0";
                    row["40"] = (object)"0";
                    row["41"] = (object)"0";
                    row["42"] = (object)"0";
                    row["43"] = (object)"0";
                    row["44"] = (object)"0";
                    row["45"] = (object)"0";
                    row["46"] = (object)"0";
                    row["47"] = (object)"0";
                    row["48"] = (object)"0";
                    row["49"] = (object)"0";
                    row["50"] = (object)"0";
                    row["51"] = (object)"0";
                    row["52"] = (object)"0";
                    dataTable.Rows.Add(row);
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            string str1 = DateTime.Now.Year.ToString();
            string str2 = "01/01/" + str1;
            string str3 = "31/12/" + str1;
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.cedis = '" + this.lblCedis.Text + "' AND a.que_status in ('A','T') order by a.que_fechaemb";
            if (this.lblCedis.Text == "AGUILARES")
                command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c, tb_cat_areas_queja d WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' AND a.que_status in ('A','T') AND a.cedis = 'COMERCIALIZADORA GAB' AND a.area_queja = d.id_area AND d.area_nombre = 'EMPAQUE_AGUILARES' order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(1).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        string str4 = sqlDataReader2["que_semana"].ToString().Trim();
                        int num;
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["1"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["1"] = (object)str5;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["2"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["2"] = (object)str5;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["3"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["3"] = (object)str5;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["4"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["4"] = (object)str5;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["5"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["5"] = (object)str5;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["6"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["6"] = (object)str5;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["7"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["7"] = (object)str5;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["8"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["8"] = (object)str5;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["9"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["9"] = (object)str5;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["10"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["10"] = (object)str5;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["11"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["11"] = (object)str5;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["12"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["12"] = (object)str5;
                        }
                        if (str4 == "13")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["13"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["13"] = (object)str5;
                        }
                        if (str4 == "14")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["14"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["14"] = (object)str5;
                        }
                        if (str4 == "15")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["15"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["15"] = (object)str5;
                        }
                        if (str4 == "16")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["16"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["16"] = (object)str5;
                        }
                        if (str4 == "17")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["17"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["17"] = (object)str5;
                        }
                        if (str4 == "18")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["18"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["18"] = (object)str5;
                        }
                        if (str4 == "19")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["19"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["19"] = (object)str5;
                        }
                        if (str4 == "20")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["20"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["20"] = (object)str5;
                        }
                        if (str4 == "21")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["21"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["21"] = (object)str5;
                        }
                        if (str4 == "22")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["22"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["22"] = (object)str5;
                        }
                        if (str4 == "23")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["23"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["23"] = (object)str5;
                        }
                        if (str4 == "24")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["24"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["24"] = (object)str5;
                        }
                        if (str4 == "25")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["25"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["25"] = (object)str5;
                        }
                        if (str4 == "26")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["26"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["26"] = (object)str5;
                        }
                        if (str4 == "27")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["27"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["27"] = (object)str5;
                        }
                        if (str4 == "28")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["28"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["28"] = (object)str5;
                        }
                        if (str4 == "29")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["29"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["29"] = (object)str5;
                        }
                        if (str4 == "30")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["30"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["30"] = (object)str5;
                        }
                        if (str4 == "31")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["31"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["31"] = (object)str5;
                        }
                        if (str4 == "32")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["32"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["32"] = (object)str5;
                        }
                        if (str4 == "33")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["33"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["33"] = (object)str5;
                        }
                        if (str4 == "34")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["34"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["34"] = (object)str5;
                        }
                        if (str4 == "35")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["35"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["35"] = (object)str5;
                        }
                        if (str4 == "36")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["36"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["36"] = (object)str5;
                        }
                        if (str4 == "37")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["37"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["37"] = (object)str5;
                        }
                        if (str4 == "38")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["38"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["38"] = (object)str5;
                        }
                        if (str4 == "39")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["39"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["39"] = (object)str5;
                        }
                        if (str4 == "40")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["40"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["40"] = (object)str5;
                        }
                        if (str4 == "41")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["41"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["41"] = (object)str5;
                        }
                        if (str4 == "42")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["42"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["42"] = (object)str5;
                        }
                        if (str4 == "43")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["43"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["43"] = (object)str5;
                        }
                        if (str4 == "44")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["44"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["44"] = (object)str5;
                        }
                        if (str4 == "45")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["45"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["45"] = (object)str5;
                        }
                        if (str4 == "46")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["46"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["46"] = (object)str5;
                        }
                        if (str4 == "47")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["47"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["47"] = (object)str5;
                        }
                        if (str4 == "48")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["48"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["48"] = (object)str5;
                        }
                        if (str4 == "49")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["49"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["49"] = (object)str5;
                        }
                        if (str4 == "50")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["50"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["50"] = (object)str5;
                        }
                        if (str4 == "51'")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["51"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["51"] = (object)str5;
                        }
                        if (str4 == "52")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["52"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["52"] = (object)str5;
                        }
                    }
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = 0;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            int num28 = 0;
            int num29 = 0;
            int num30 = 0;
            int num31 = 0;
            int num32 = 0;
            int num33 = 0;
            int num34 = 0;
            int num35 = 0;
            int num36 = 0;
            int num37 = 0;
            int num38 = 0;
            int num39 = 0;
            int num40 = 0;
            int num41 = 0;
            int num42 = 0;
            int num43 = 0;
            int num44 = 0;
            int num45 = 0;
            int num46 = 0;
            int num47 = 0;
            int num48 = 0;
            int num49 = 0;
            int num50 = 0;
            int num51 = 0;
            int num52 = 0;
            string str6 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>" + "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem1</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem2</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem3</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem4</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem5</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem6</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem7</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem8</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem9</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem10</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem11</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem12</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem13</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem14</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem15</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem16</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem17</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem18</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem19</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem20</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem21</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem22</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem23</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem24</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem25</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem26</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem27</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem28</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem29</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem30</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem31</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem32</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem33</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem34</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem35</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem36</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem37</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem38</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem39</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem40</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem41</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem42</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem43</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem44</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem45</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem46</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem47</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem48</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem49</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem50</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem51</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem52</font></th>" + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row["1"].ToString());
                num2 += Convert.ToInt32(row["2"].ToString());
                num3 += Convert.ToInt32(row["3"].ToString());
                num4 += Convert.ToInt32(row["4"].ToString());
                num5 += Convert.ToInt32(row["5"].ToString());
                num6 += Convert.ToInt32(row["6"].ToString());
                num7 += Convert.ToInt32(row["7"].ToString());
                num8 += Convert.ToInt32(row["8"].ToString());
                num9 += Convert.ToInt32(row["9"].ToString());
                num10 += Convert.ToInt32(row["10"].ToString());
                num11 += Convert.ToInt32(row["11"].ToString());
                num12 += Convert.ToInt32(row["12"].ToString());
                num13 += Convert.ToInt32(row["13"].ToString());
                num14 += Convert.ToInt32(row["14"].ToString());
                num15 += Convert.ToInt32(row["15"].ToString());
                num16 += Convert.ToInt32(row["16"].ToString());
                num17 += Convert.ToInt32(row["17"].ToString());
                num18 += Convert.ToInt32(row["18"].ToString());
                num19 += Convert.ToInt32(row["19"].ToString());
                num20 += Convert.ToInt32(row["20"].ToString());
                num21 += Convert.ToInt32(row["21"].ToString());
                num22 += Convert.ToInt32(row["22"].ToString());
                num23 += Convert.ToInt32(row["23"].ToString());
                num24 += Convert.ToInt32(row["24"].ToString());
                num25 += Convert.ToInt32(row["25"].ToString());
                num26 += Convert.ToInt32(row["26"].ToString());
                num27 += Convert.ToInt32(row["27"].ToString());
                num28 += Convert.ToInt32(row["28"].ToString());
                num29 += Convert.ToInt32(row["29"].ToString());
                num30 += Convert.ToInt32(row["30"].ToString());
                num31 += Convert.ToInt32(row["31"].ToString());
                num32 += Convert.ToInt32(row["32"].ToString());
                num33 += Convert.ToInt32(row["33"].ToString());
                num34 += Convert.ToInt32(row["34"].ToString());
                num35 += Convert.ToInt32(row["35"].ToString());
                num36 += Convert.ToInt32(row["36"].ToString());
                num37 += Convert.ToInt32(row["37"].ToString());
                num38 += Convert.ToInt32(row["38"].ToString());
                num39 += Convert.ToInt32(row["39"].ToString());
                num40 += Convert.ToInt32(row["40"].ToString());
                num41 += Convert.ToInt32(row["41"].ToString());
                num42 += Convert.ToInt32(row["42"].ToString());
                num43 += Convert.ToInt32(row["43"].ToString());
                num44 += Convert.ToInt32(row["44"].ToString());
                num45 += Convert.ToInt32(row["45"].ToString());
                num46 += Convert.ToInt32(row["46"].ToString());
                num47 += Convert.ToInt32(row["47"].ToString());
                num48 += Convert.ToInt32(row["48"].ToString());
                num49 += Convert.ToInt32(row["49"].ToString());
                num50 += Convert.ToInt32(row["50"].ToString());
                num51 += Convert.ToInt32(row["51"].ToString());
                num52 += Convert.ToInt32(row["52"].ToString());
            }
            string str7 = str6 + "<tr>" + "<td align='right'><b>TOTALES</b></td>" + "<td>" + (object)num1 + "</td>" + "<td>" + (object)num2 + "</td>" + "<td>" + (object)num3 + "</td>" + "<td>" + (object)num4 + "</td>" + "<td>" + (object)num5 + "</td>" + "<td>" + (object)num6 + "</td>" + "<td>" + (object)num7 + "</td>" + "<td>" + (object)num8 + "</td>" + "<td>" + (object)num9 + "</td>" + "<td>" + (object)num10 + "</td>" + "<td>" + (object)num11 + "</td>" + "<td>" + (object)num12 + "</td>" + "<td>" + (object)num13 + "</td>" + "<td>" + (object)num14 + "</td>" + "<td>" + (object)num15 + "</td>" + "<td>" + (object)num16 + "</td>" + "<td>" + (object)num17 + "</td>" + "<td>" + (object)num18 + "</td>" + "<td>" + (object)num19 + "</td>" + "<td>" + (object)num20 + "</td>" + "<td>" + (object)num21 + "</td>" + "<td>" + (object)num22 + "</td>" + "<td>" + (object)num23 + "</td>" + "<td>" + (object)num24 + "</td>" + "<td>" + (object)num25 + "</td>" + "<td>" + (object)num26 + "</td>" + "<td>" + (object)num27 + "</td>" + "<td>" + (object)num28 + "</td>" + "<td>" + (object)num29 + "</td>" + "<td>" + (object)num30 + "</td>" + "<td>" + (object)num31 + "</td>" + "<td>" + (object)num32 + "</td>" + "<td>" + (object)num33 + "</td>" + "<td>" + (object)num34 + "</td>" + "<td>" + (object)num35 + "</td>" + "<td>" + (object)num36 + "</td>" + "<td>" + (object)num37 + "</td>" + "<td>" + (object)num38 + "</td>" + "<td>" + (object)num39 + "</td>" + "<td>" + (object)num40 + "</td>" + "<td>" + (object)num41 + "</td>" + "<td>" + (object)num42 + "</td>" + "<td>" + (object)num43 + "</td>" + "<td>" + (object)num44 + "</td>" + "<td>" + (object)num45 + "</td>" + "<td>" + (object)num46 + "</td>" + "<td>" + (object)num47 + "</td>" + "<td>" + (object)num48 + "</td>" + "<td>" + (object)num49 + "</td>" + "<td>" + (object)num50 + "</td>" + "<td>" + (object)num51 + "</td>" + "<td>" + (object)num52 + "</td>" + "</tr>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                str7 += "<tr>";
                str7 = str7 + "<td>" + row["Problema"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["1"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["2"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["3"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["4"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["5"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["6"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["7"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["8"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["9"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["10"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["11"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["12"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["13"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["14"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["15"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["16"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["17"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["18"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["19"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["20"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["21"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["22"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["23"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["24"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["25"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["26"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["27"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["28"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["29"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["30"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["31"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["32"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["33"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["34"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["35"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["36"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["37"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["38"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["39"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["40"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["41"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["42"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["43"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["44"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["45"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["46"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["47"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["48"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["49"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["50"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["51"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["52"].ToString() + "</td>";
                str7 += "</tr>";
            }
            this.datosqueja.InnerHtml = str7 + "</tbody>" + "</table>";
        }

        protected void btnTodos_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Clave", typeof(string));
            dataTable.Columns.Add("Problema", typeof(string));
            dataTable.Columns.Add("1", typeof(string));
            dataTable.Columns.Add("2", typeof(string));
            dataTable.Columns.Add("3", typeof(string));
            dataTable.Columns.Add("4", typeof(string));
            dataTable.Columns.Add("5", typeof(string));
            dataTable.Columns.Add("6", typeof(string));
            dataTable.Columns.Add("7", typeof(string));
            dataTable.Columns.Add("8", typeof(string));
            dataTable.Columns.Add("9", typeof(string));
            dataTable.Columns.Add("10", typeof(string));
            dataTable.Columns.Add("11", typeof(string));
            dataTable.Columns.Add("12", typeof(string));
            dataTable.Columns.Add("13", typeof(string));
            dataTable.Columns.Add("14", typeof(string));
            dataTable.Columns.Add("15", typeof(string));
            dataTable.Columns.Add("16", typeof(string));
            dataTable.Columns.Add("17", typeof(string));
            dataTable.Columns.Add("18", typeof(string));
            dataTable.Columns.Add("19", typeof(string));
            dataTable.Columns.Add("20", typeof(string));
            dataTable.Columns.Add("21", typeof(string));
            dataTable.Columns.Add("22", typeof(string));
            dataTable.Columns.Add("23", typeof(string));
            dataTable.Columns.Add("24", typeof(string));
            dataTable.Columns.Add("25", typeof(string));
            dataTable.Columns.Add("26", typeof(string));
            dataTable.Columns.Add("27", typeof(string));
            dataTable.Columns.Add("28", typeof(string));
            dataTable.Columns.Add("29", typeof(string));
            dataTable.Columns.Add("30", typeof(string));
            dataTable.Columns.Add("31", typeof(string));
            dataTable.Columns.Add("32", typeof(string));
            dataTable.Columns.Add("33", typeof(string));
            dataTable.Columns.Add("34", typeof(string));
            dataTable.Columns.Add("35", typeof(string));
            dataTable.Columns.Add("36", typeof(string));
            dataTable.Columns.Add("37", typeof(string));
            dataTable.Columns.Add("38", typeof(string));
            dataTable.Columns.Add("39", typeof(string));
            dataTable.Columns.Add("40", typeof(string));
            dataTable.Columns.Add("41", typeof(string));
            dataTable.Columns.Add("42", typeof(string));
            dataTable.Columns.Add("43", typeof(string));
            dataTable.Columns.Add("44", typeof(string));
            dataTable.Columns.Add("45", typeof(string));
            dataTable.Columns.Add("46", typeof(string));
            dataTable.Columns.Add("47", typeof(string));
            dataTable.Columns.Add("48", typeof(string));
            dataTable.Columns.Add("49", typeof(string));
            dataTable.Columns.Add("50", typeof(string));
            dataTable.Columns.Add("51", typeof(string));
            dataTable.Columns.Add("52", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT * FROM tb_cat_problemas ORDER BY pro_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["Clave"] = (object)sqlDataReader1.GetValue(0).ToString().Trim();
                    row["Problema"] = (object)sqlDataReader1.GetValue(1).ToString().Trim();
                    row["1"] = (object)"0";
                    row["2"] = (object)"0";
                    row["3"] = (object)"0";
                    row["4"] = (object)"0";
                    row["5"] = (object)"0";
                    row["6"] = (object)"0";
                    row["7"] = (object)"0";
                    row["8"] = (object)"0";
                    row["9"] = (object)"0";
                    row["10"] = (object)"0";
                    row["11"] = (object)"0";
                    row["12"] = (object)"0";
                    row["13"] = (object)"0";
                    row["14"] = (object)"0";
                    row["15"] = (object)"0";
                    row["16"] = (object)"0";
                    row["17"] = (object)"0";
                    row["18"] = (object)"0";
                    row["19"] = (object)"0";
                    row["20"] = (object)"0";
                    row["21"] = (object)"0";
                    row["22"] = (object)"0";
                    row["23"] = (object)"0";
                    row["24"] = (object)"0";
                    row["25"] = (object)"0";
                    row["26"] = (object)"0";
                    row["27"] = (object)"0";
                    row["28"] = (object)"0";
                    row["29"] = (object)"0";
                    row["30"] = (object)"0";
                    row["31"] = (object)"0";
                    row["32"] = (object)"0";
                    row["33"] = (object)"0";
                    row["34"] = (object)"0";
                    row["35"] = (object)"0";
                    row["36"] = (object)"0";
                    row["37"] = (object)"0";
                    row["38"] = (object)"0";
                    row["39"] = (object)"0";
                    row["40"] = (object)"0";
                    row["41"] = (object)"0";
                    row["42"] = (object)"0";
                    row["43"] = (object)"0";
                    row["44"] = (object)"0";
                    row["45"] = (object)"0";
                    row["46"] = (object)"0";
                    row["47"] = (object)"0";
                    row["48"] = (object)"0";
                    row["49"] = (object)"0";
                    row["50"] = (object)"0";
                    row["51"] = (object)"0";
                    row["52"] = (object)"0";
                    dataTable.Rows.Add(row);
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            string str1 = DateTime.Now.Year.ToString();
            string str2 = "01/01/" + str1;
            string str3 = "31/12/" + str1;
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' AND a.que_status in ('A','T') order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(1).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        string str4 = sqlDataReader2["que_semana"].ToString().Trim();
                        int num;
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["1"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["1"] = (object)str5;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["2"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["2"] = (object)str5;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["3"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["3"] = (object)str5;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["4"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["4"] = (object)str5;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["5"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["5"] = (object)str5;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["6"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["6"] = (object)str5;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["7"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["7"] = (object)str5;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["8"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["8"] = (object)str5;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["9"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["9"] = (object)str5;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["10"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["10"] = (object)str5;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["11"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["11"] = (object)str5;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["12"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["12"] = (object)str5;
                        }
                        if (str4 == "13")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["13"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["13"] = (object)str5;
                        }
                        if (str4 == "14")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["14"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["14"] = (object)str5;
                        }
                        if (str4 == "15")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["15"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["15"] = (object)str5;
                        }
                        if (str4 == "16")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["16"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["16"] = (object)str5;
                        }
                        if (str4 == "17")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["17"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["17"] = (object)str5;
                        }
                        if (str4 == "18")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["18"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["18"] = (object)str5;
                        }
                        if (str4 == "19")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["19"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["19"] = (object)str5;
                        }
                        if (str4 == "20")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["20"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["20"] = (object)str5;
                        }
                        if (str4 == "21")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["21"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["21"] = (object)str5;
                        }
                        if (str4 == "22")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["22"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["22"] = (object)str5;
                        }
                        if (str4 == "23")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["23"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["23"] = (object)str5;
                        }
                        if (str4 == "24")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["24"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["24"] = (object)str5;
                        }
                        if (str4 == "25")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["25"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["25"] = (object)str5;
                        }
                        if (str4 == "26")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["26"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["26"] = (object)str5;
                        }
                        if (str4 == "27")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["27"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["27"] = (object)str5;
                        }
                        if (str4 == "28")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["28"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["28"] = (object)str5;
                        }
                        if (str4 == "29")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["29"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["29"] = (object)str5;
                        }
                        if (str4 == "30")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["30"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["30"] = (object)str5;
                        }
                        if (str4 == "31")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["31"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["31"] = (object)str5;
                        }
                        if (str4 == "32")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["32"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["32"] = (object)str5;
                        }
                        if (str4 == "33")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["33"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["33"] = (object)str5;
                        }
                        if (str4 == "34")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["34"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["34"] = (object)str5;
                        }
                        if (str4 == "35")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["35"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["35"] = (object)str5;
                        }
                        if (str4 == "36")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["36"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["36"] = (object)str5;
                        }
                        if (str4 == "37")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["37"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["37"] = (object)str5;
                        }
                        if (str4 == "38")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["38"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["38"] = (object)str5;
                        }
                        if (str4 == "39")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["39"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["39"] = (object)str5;
                        }
                        if (str4 == "40")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["40"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["40"] = (object)str5;
                        }
                        if (str4 == "41")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["41"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["41"] = (object)str5;
                        }
                        if (str4 == "42")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["42"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["42"] = (object)str5;
                        }
                        if (str4 == "43")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["43"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["43"] = (object)str5;
                        }
                        if (str4 == "44")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["44"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["44"] = (object)str5;
                        }
                        if (str4 == "45")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["45"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["45"] = (object)str5;
                        }
                        if (str4 == "46")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["46"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["46"] = (object)str5;
                        }
                        if (str4 == "47")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["47"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["47"] = (object)str5;
                        }
                        if (str4 == "48")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["48"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["48"] = (object)str5;
                        }
                        if (str4 == "49")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["49"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["49"] = (object)str5;
                        }
                        if (str4 == "50")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["50"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["50"] = (object)str5;
                        }
                        if (str4 == "51'")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["51"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["51"] = (object)str5;
                        }
                        if (str4 == "52")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["52"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["52"] = (object)str5;
                        }
                    }
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = 0;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            int num28 = 0;
            int num29 = 0;
            int num30 = 0;
            int num31 = 0;
            int num32 = 0;
            int num33 = 0;
            int num34 = 0;
            int num35 = 0;
            int num36 = 0;
            int num37 = 0;
            int num38 = 0;
            int num39 = 0;
            int num40 = 0;
            int num41 = 0;
            int num42 = 0;
            int num43 = 0;
            int num44 = 0;
            int num45 = 0;
            int num46 = 0;
            int num47 = 0;
            int num48 = 0;
            int num49 = 0;
            int num50 = 0;
            int num51 = 0;
            int num52 = 0;
            string str6 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>" + "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem1</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem2</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem3</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem4</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem5</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem6</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem7</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem8</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem9</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem10</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem11</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem12</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem13</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem14</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem15</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem16</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem17</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem18</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem19</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem20</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem21</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem22</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem23</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem24</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem25</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem26</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem27</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem28</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem29</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem30</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem31</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem32</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem33</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem34</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem35</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem36</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem37</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem38</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem39</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem40</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem41</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem42</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem43</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem44</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem45</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem46</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem47</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem48</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem49</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem50</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem51</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Sem52</font></th>" + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row["1"].ToString());
                num2 += Convert.ToInt32(row["2"].ToString());
                num3 += Convert.ToInt32(row["3"].ToString());
                num4 += Convert.ToInt32(row["4"].ToString());
                num5 += Convert.ToInt32(row["5"].ToString());
                num6 += Convert.ToInt32(row["6"].ToString());
                num7 += Convert.ToInt32(row["7"].ToString());
                num8 += Convert.ToInt32(row["8"].ToString());
                num9 += Convert.ToInt32(row["9"].ToString());
                num10 += Convert.ToInt32(row["10"].ToString());
                num11 += Convert.ToInt32(row["11"].ToString());
                num12 += Convert.ToInt32(row["12"].ToString());
                num13 += Convert.ToInt32(row["13"].ToString());
                num14 += Convert.ToInt32(row["14"].ToString());
                num15 += Convert.ToInt32(row["15"].ToString());
                num16 += Convert.ToInt32(row["16"].ToString());
                num17 += Convert.ToInt32(row["17"].ToString());
                num18 += Convert.ToInt32(row["18"].ToString());
                num19 += Convert.ToInt32(row["19"].ToString());
                num20 += Convert.ToInt32(row["20"].ToString());
                num21 += Convert.ToInt32(row["21"].ToString());
                num22 += Convert.ToInt32(row["22"].ToString());
                num23 += Convert.ToInt32(row["23"].ToString());
                num24 += Convert.ToInt32(row["24"].ToString());
                num25 += Convert.ToInt32(row["25"].ToString());
                num26 += Convert.ToInt32(row["26"].ToString());
                num27 += Convert.ToInt32(row["27"].ToString());
                num28 += Convert.ToInt32(row["28"].ToString());
                num29 += Convert.ToInt32(row["29"].ToString());
                num30 += Convert.ToInt32(row["30"].ToString());
                num31 += Convert.ToInt32(row["31"].ToString());
                num32 += Convert.ToInt32(row["32"].ToString());
                num33 += Convert.ToInt32(row["33"].ToString());
                num34 += Convert.ToInt32(row["34"].ToString());
                num35 += Convert.ToInt32(row["35"].ToString());
                num36 += Convert.ToInt32(row["36"].ToString());
                num37 += Convert.ToInt32(row["37"].ToString());
                num38 += Convert.ToInt32(row["38"].ToString());
                num39 += Convert.ToInt32(row["39"].ToString());
                num40 += Convert.ToInt32(row["40"].ToString());
                num41 += Convert.ToInt32(row["41"].ToString());
                num42 += Convert.ToInt32(row["42"].ToString());
                num43 += Convert.ToInt32(row["43"].ToString());
                num44 += Convert.ToInt32(row["44"].ToString());
                num45 += Convert.ToInt32(row["45"].ToString());
                num46 += Convert.ToInt32(row["46"].ToString());
                num47 += Convert.ToInt32(row["47"].ToString());
                num48 += Convert.ToInt32(row["48"].ToString());
                num49 += Convert.ToInt32(row["49"].ToString());
                num50 += Convert.ToInt32(row["50"].ToString());
                num51 += Convert.ToInt32(row["51"].ToString());
                num52 += Convert.ToInt32(row["52"].ToString());
            }
            string str7 = str6 + "<tr>" + "<td align='right'><b>TOTALES</b></td>" + "<td>" + (object)num1 + "</td>" + "<td>" + (object)num2 + "</td>" + "<td>" + (object)num3 + "</td>" + "<td>" + (object)num4 + "</td>" + "<td>" + (object)num5 + "</td>" + "<td>" + (object)num6 + "</td>" + "<td>" + (object)num7 + "</td>" + "<td>" + (object)num8 + "</td>" + "<td>" + (object)num9 + "</td>" + "<td>" + (object)num10 + "</td>" + "<td>" + (object)num11 + "</td>" + "<td>" + (object)num12 + "</td>" + "<td>" + (object)num13 + "</td>" + "<td>" + (object)num14 + "</td>" + "<td>" + (object)num15 + "</td>" + "<td>" + (object)num16 + "</td>" + "<td>" + (object)num17 + "</td>" + "<td>" + (object)num18 + "</td>" + "<td>" + (object)num19 + "</td>" + "<td>" + (object)num20 + "</td>" + "<td>" + (object)num21 + "</td>" + "<td>" + (object)num22 + "</td>" + "<td>" + (object)num23 + "</td>" + "<td>" + (object)num24 + "</td>" + "<td>" + (object)num25 + "</td>" + "<td>" + (object)num26 + "</td>" + "<td>" + (object)num27 + "</td>" + "<td>" + (object)num28 + "</td>" + "<td>" + (object)num29 + "</td>" + "<td>" + (object)num30 + "</td>" + "<td>" + (object)num31 + "</td>" + "<td>" + (object)num32 + "</td>" + "<td>" + (object)num33 + "</td>" + "<td>" + (object)num34 + "</td>" + "<td>" + (object)num35 + "</td>" + "<td>" + (object)num36 + "</td>" + "<td>" + (object)num37 + "</td>" + "<td>" + (object)num38 + "</td>" + "<td>" + (object)num39 + "</td>" + "<td>" + (object)num40 + "</td>" + "<td>" + (object)num41 + "</td>" + "<td>" + (object)num42 + "</td>" + "<td>" + (object)num43 + "</td>" + "<td>" + (object)num44 + "</td>" + "<td>" + (object)num45 + "</td>" + "<td>" + (object)num46 + "</td>" + "<td>" + (object)num47 + "</td>" + "<td>" + (object)num48 + "</td>" + "<td>" + (object)num49 + "</td>" + "<td>" + (object)num50 + "</td>" + "<td>" + (object)num51 + "</td>" + "<td>" + (object)num52 + "</td>" + "</tr>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                str7 += "<tr>";
                str7 = str7 + "<td>" + row["Problema"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["1"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["2"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["3"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["4"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["5"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["6"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["7"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["8"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["9"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["10"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["11"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["12"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["13"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["14"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["15"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["16"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["17"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["18"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["19"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["20"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["21"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["22"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["23"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["24"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["25"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["26"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["27"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["28"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["29"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["30"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["31"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["32"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["33"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["34"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["35"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["36"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["37"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["38"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["39"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["40"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["41"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["42"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["43"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["44"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["45"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["46"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["47"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["48"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["49"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["50"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["51"].ToString() + "</td>";
                str7 = str7 + "<td>" + row["52"].ToString() + "</td>";
                str7 += "</tr>";
            }
            this.datosqueja.InnerHtml = str7 + "</tbody>" + "</table>";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Clave", typeof(string));
            dataTable.Columns.Add("Problema", typeof(string));
            dataTable.Columns.Add("1", typeof(string));
            dataTable.Columns.Add("2", typeof(string));
            dataTable.Columns.Add("3", typeof(string));
            dataTable.Columns.Add("4", typeof(string));
            dataTable.Columns.Add("5", typeof(string));
            dataTable.Columns.Add("6", typeof(string));
            dataTable.Columns.Add("7", typeof(string));
            dataTable.Columns.Add("8", typeof(string));
            dataTable.Columns.Add("9", typeof(string));
            dataTable.Columns.Add("10", typeof(string));
            dataTable.Columns.Add("11", typeof(string));
            dataTable.Columns.Add("12", typeof(string));
            dataTable.Columns.Add("13", typeof(string));
            dataTable.Columns.Add("14", typeof(string));
            dataTable.Columns.Add("15", typeof(string));
            dataTable.Columns.Add("16", typeof(string));
            dataTable.Columns.Add("17", typeof(string));
            dataTable.Columns.Add("18", typeof(string));
            dataTable.Columns.Add("19", typeof(string));
            dataTable.Columns.Add("20", typeof(string));
            dataTable.Columns.Add("21", typeof(string));
            dataTable.Columns.Add("22", typeof(string));
            dataTable.Columns.Add("23", typeof(string));
            dataTable.Columns.Add("24", typeof(string));
            dataTable.Columns.Add("25", typeof(string));
            dataTable.Columns.Add("26", typeof(string));
            dataTable.Columns.Add("27", typeof(string));
            dataTable.Columns.Add("28", typeof(string));
            dataTable.Columns.Add("29", typeof(string));
            dataTable.Columns.Add("30", typeof(string));
            dataTable.Columns.Add("31", typeof(string));
            dataTable.Columns.Add("32", typeof(string));
            dataTable.Columns.Add("33", typeof(string));
            dataTable.Columns.Add("34", typeof(string));
            dataTable.Columns.Add("35", typeof(string));
            dataTable.Columns.Add("36", typeof(string));
            dataTable.Columns.Add("37", typeof(string));
            dataTable.Columns.Add("38", typeof(string));
            dataTable.Columns.Add("39", typeof(string));
            dataTable.Columns.Add("40", typeof(string));
            dataTable.Columns.Add("41", typeof(string));
            dataTable.Columns.Add("42", typeof(string));
            dataTable.Columns.Add("43", typeof(string));
            dataTable.Columns.Add("44", typeof(string));
            dataTable.Columns.Add("45", typeof(string));
            dataTable.Columns.Add("46", typeof(string));
            dataTable.Columns.Add("47", typeof(string));
            dataTable.Columns.Add("48", typeof(string));
            dataTable.Columns.Add("49", typeof(string));
            dataTable.Columns.Add("50", typeof(string));
            dataTable.Columns.Add("51", typeof(string));
            dataTable.Columns.Add("52", typeof(string));
            DataRow row1 = dataTable.NewRow();
            row1["Clave"] = (object)"";
            row1["Problema"] = (object)"TOTALES";
            row1["1"] = (object)"0";
            row1["2"] = (object)"0";
            row1["3"] = (object)"0";
            row1["4"] = (object)"0";
            row1["5"] = (object)"0";
            row1["6"] = (object)"0";
            row1["7"] = (object)"0";
            row1["8"] = (object)"0";
            row1["9"] = (object)"0";
            row1["10"] = (object)"0";
            row1["11"] = (object)"0";
            row1["12"] = (object)"0";
            row1["13"] = (object)"0";
            row1["14"] = (object)"0";
            row1["15"] = (object)"0";
            row1["16"] = (object)"0";
            row1["17"] = (object)"0";
            row1["18"] = (object)"0";
            row1["19"] = (object)"0";
            row1["20"] = (object)"0";
            row1["21"] = (object)"0";
            row1["22"] = (object)"0";
            row1["23"] = (object)"0";
            row1["24"] = (object)"0";
            row1["25"] = (object)"0";
            row1["26"] = (object)"0";
            row1["27"] = (object)"0";
            row1["28"] = (object)"0";
            row1["29"] = (object)"0";
            row1["30"] = (object)"0";
            row1["31"] = (object)"0";
            row1["32"] = (object)"0";
            row1["33"] = (object)"0";
            row1["34"] = (object)"0";
            row1["35"] = (object)"0";
            row1["36"] = (object)"0";
            row1["37"] = (object)"0";
            row1["38"] = (object)"0";
            row1["39"] = (object)"0";
            row1["40"] = (object)"0";
            row1["41"] = (object)"0";
            row1["42"] = (object)"0";
            row1["43"] = (object)"0";
            row1["44"] = (object)"0";
            row1["45"] = (object)"0";
            row1["46"] = (object)"0";
            row1["47"] = (object)"0";
            row1["48"] = (object)"0";
            row1["49"] = (object)"0";
            row1["50"] = (object)"0";
            row1["51"] = (object)"0";
            row1["52"] = (object)"0";
            dataTable.Rows.Add(row1);
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT * FROM tb_cat_problemas ORDER BY pro_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["Clave"] = (object)sqlDataReader1.GetValue(0).ToString().Trim();
                    row2["Problema"] = (object)sqlDataReader1.GetValue(1).ToString().Trim();
                    row2["1"] = (object)"0";
                    row2["2"] = (object)"0";
                    row2["3"] = (object)"0";
                    row2["4"] = (object)"0";
                    row2["5"] = (object)"0";
                    row2["6"] = (object)"0";
                    row2["7"] = (object)"0";
                    row2["8"] = (object)"0";
                    row2["9"] = (object)"0";
                    row2["10"] = (object)"0";
                    row2["11"] = (object)"0";
                    row2["12"] = (object)"0";
                    row2["13"] = (object)"0";
                    row2["14"] = (object)"0";
                    row2["15"] = (object)"0";
                    row2["16"] = (object)"0";
                    row2["17"] = (object)"0";
                    row2["18"] = (object)"0";
                    row2["19"] = (object)"0";
                    row2["20"] = (object)"0";
                    row2["21"] = (object)"0";
                    row2["22"] = (object)"0";
                    row2["23"] = (object)"0";
                    row2["24"] = (object)"0";
                    row2["25"] = (object)"0";
                    row2["26"] = (object)"0";
                    row2["27"] = (object)"0";
                    row2["28"] = (object)"0";
                    row2["29"] = (object)"0";
                    row2["30"] = (object)"0";
                    row2["31"] = (object)"0";
                    row2["32"] = (object)"0";
                    row2["33"] = (object)"0";
                    row2["34"] = (object)"0";
                    row2["35"] = (object)"0";
                    row2["36"] = (object)"0";
                    row2["37"] = (object)"0";
                    row2["38"] = (object)"0";
                    row2["39"] = (object)"0";
                    row2["40"] = (object)"0";
                    row2["41"] = (object)"0";
                    row2["42"] = (object)"0";
                    row2["43"] = (object)"0";
                    row2["44"] = (object)"0";
                    row2["45"] = (object)"0";
                    row2["46"] = (object)"0";
                    row2["47"] = (object)"0";
                    row2["48"] = (object)"0";
                    row2["49"] = (object)"0";
                    row2["50"] = (object)"0";
                    row2["51"] = (object)"0";
                    row2["52"] = (object)"0";
                    dataTable.Rows.Add(row2);
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = 0;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            int num28 = 0;
            int num29 = 0;
            int num30 = 0;
            int num31 = 0;
            int num32 = 0;
            int num33 = 0;
            int num34 = 0;
            int num35 = 0;
            int num36 = 0;
            int num37 = 0;
            int num38 = 0;
            int num39 = 0;
            int num40 = 0;
            int num41 = 0;
            int num42 = 0;
            int num43 = 0;
            int num44 = 0;
            int num45 = 0;
            int num46 = 0;
            int num47 = 0;
            int num48 = 0;
            int num49 = 0;
            int num50 = 0;
            int num51 = 0;
            int num52 = 0;
            string str1 = DateTime.Now.Year.ToString();
            string str2 = "01/01/" + str1;
            string str3 = "31/12/" + str1;
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.cedis = '" + this.lblCedis.Text + "' AND a.que_status in ('A','T') order by a.que_fechaemb";
            if (this.lblCedis.Text == "AGUILARES")
                command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c, tb_cat_areas_queja d WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' AND a.que_status in ('A','T') AND a.cedis = 'COMERCIALIZADORA GAB' AND a.area_queja = d.id_area AND d.area_nombre = 'EMPAQUE_AGUILARES' order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(1).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        string str4 = sqlDataReader2["que_semana"].ToString().Trim();
                        int num53;
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["1"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["1"] = (object)str5;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["2"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["2"] = (object)str5;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["3"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["3"] = (object)str5;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["4"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["4"] = (object)str5;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["5"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["5"] = (object)str5;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["6"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["6"] = (object)str5;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["7"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["7"] = (object)str5;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["8"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["8"] = (object)str5;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["9"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["9"] = (object)str5;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["10"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["10"] = (object)str5;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["11"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["11"] = (object)str5;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["12"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["12"] = (object)str5;
                        }
                        if (str4 == "13")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["13"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["13"] = (object)str5;
                        }
                        if (str4 == "14")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["14"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["14"] = (object)str5;
                        }
                        if (str4 == "15")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["15"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["15"] = (object)str5;
                        }
                        if (str4 == "16")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["16"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["16"] = (object)str5;
                        }
                        if (str4 == "17")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["17"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["17"] = (object)str5;
                        }
                        if (str4 == "18")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["18"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["18"] = (object)str5;
                        }
                        if (str4 == "19")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["19"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["19"] = (object)str5;
                        }
                        if (str4 == "20")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["20"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["20"] = (object)str5;
                        }
                        if (str4 == "21")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["21"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["21"] = (object)str5;
                        }
                        if (str4 == "22")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["22"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["22"] = (object)str5;
                        }
                        if (str4 == "23")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["23"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["23"] = (object)str5;
                        }
                        if (str4 == "24")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["24"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["24"] = (object)str5;
                        }
                        if (str4 == "25")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["25"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["25"] = (object)str5;
                        }
                        if (str4 == "26")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["26"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["26"] = (object)str5;
                        }
                        if (str4 == "27")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["27"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["27"] = (object)str5;
                        }
                        if (str4 == "28")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["28"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["28"] = (object)str5;
                        }
                        if (str4 == "29")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["29"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["29"] = (object)str5;
                        }
                        if (str4 == "30")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["30"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["30"] = (object)str5;
                        }
                        if (str4 == "31")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["31"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["31"] = (object)str5;
                        }
                        if (str4 == "32")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["32"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["32"] = (object)str5;
                        }
                        if (str4 == "33")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["33"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["33"] = (object)str5;
                        }
                        if (str4 == "34")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["34"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["34"] = (object)str5;
                        }
                        if (str4 == "35")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["35"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["35"] = (object)str5;
                        }
                        if (str4 == "36")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["36"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["36"] = (object)str5;
                        }
                        if (str4 == "37")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["37"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["37"] = (object)str5;
                        }
                        if (str4 == "38")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["38"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["38"] = (object)str5;
                        }
                        if (str4 == "39")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["39"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["39"] = (object)str5;
                        }
                        if (str4 == "40")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["40"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["40"] = (object)str5;
                        }
                        if (str4 == "41")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["41"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["41"] = (object)str5;
                        }
                        if (str4 == "42")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["42"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["42"] = (object)str5;
                        }
                        if (str4 == "43")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["43"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["43"] = (object)str5;
                        }
                        if (str4 == "44")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["44"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["44"] = (object)str5;
                        }
                        if (str4 == "45")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["45"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["45"] = (object)str5;
                        }
                        if (str4 == "46")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["46"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["46"] = (object)str5;
                        }
                        if (str4 == "47")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["47"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["47"] = (object)str5;
                        }
                        if (str4 == "48")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["48"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["48"] = (object)str5;
                        }
                        if (str4 == "49")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["49"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["49"] = (object)str5;
                        }
                        if (str4 == "50")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["50"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["50"] = (object)str5;
                        }
                        if (str4 == "51'")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["51"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["51"] = (object)str5;
                        }
                        if (str4 == "52")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["52"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["52"] = (object)str5;
                        }
                    }
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row2["1"].ToString());
                num2 += Convert.ToInt32(row2["2"].ToString());
                num3 += Convert.ToInt32(row2["3"].ToString());
                num4 += Convert.ToInt32(row2["4"].ToString());
                num5 += Convert.ToInt32(row2["5"].ToString());
                num6 += Convert.ToInt32(row2["6"].ToString());
                num7 += Convert.ToInt32(row2["7"].ToString());
                num8 += Convert.ToInt32(row2["8"].ToString());
                num9 += Convert.ToInt32(row2["9"].ToString());
                num10 += Convert.ToInt32(row2["10"].ToString());
                num11 += Convert.ToInt32(row2["11"].ToString());
                num12 += Convert.ToInt32(row2["12"].ToString());
                num13 += Convert.ToInt32(row2["13"].ToString());
                num14 += Convert.ToInt32(row2["14"].ToString());
                num15 += Convert.ToInt32(row2["15"].ToString());
                num16 += Convert.ToInt32(row2["16"].ToString());
                num17 += Convert.ToInt32(row2["17"].ToString());
                num18 += Convert.ToInt32(row2["18"].ToString());
                num19 += Convert.ToInt32(row2["19"].ToString());
                num20 += Convert.ToInt32(row2["20"].ToString());
                num21 += Convert.ToInt32(row2["21"].ToString());
                num22 += Convert.ToInt32(row2["22"].ToString());
                num23 += Convert.ToInt32(row2["23"].ToString());
                num24 += Convert.ToInt32(row2["24"].ToString());
                num25 += Convert.ToInt32(row2["25"].ToString());
                num26 += Convert.ToInt32(row2["26"].ToString());
                num27 += Convert.ToInt32(row2["27"].ToString());
                num28 += Convert.ToInt32(row2["28"].ToString());
                num29 += Convert.ToInt32(row2["29"].ToString());
                num30 += Convert.ToInt32(row2["30"].ToString());
                num31 += Convert.ToInt32(row2["31"].ToString());
                num32 += Convert.ToInt32(row2["32"].ToString());
                num33 += Convert.ToInt32(row2["33"].ToString());
                num34 += Convert.ToInt32(row2["34"].ToString());
                num35 += Convert.ToInt32(row2["35"].ToString());
                num36 += Convert.ToInt32(row2["36"].ToString());
                num37 += Convert.ToInt32(row2["37"].ToString());
                num38 += Convert.ToInt32(row2["38"].ToString());
                num39 += Convert.ToInt32(row2["39"].ToString());
                num40 += Convert.ToInt32(row2["40"].ToString());
                num41 += Convert.ToInt32(row2["41"].ToString());
                num42 += Convert.ToInt32(row2["42"].ToString());
                num43 += Convert.ToInt32(row2["43"].ToString());
                num44 += Convert.ToInt32(row2["44"].ToString());
                num45 += Convert.ToInt32(row2["45"].ToString());
                num46 += Convert.ToInt32(row2["46"].ToString());
                num47 += Convert.ToInt32(row2["47"].ToString());
                num48 += Convert.ToInt32(row2["48"].ToString());
                num49 += Convert.ToInt32(row2["49"].ToString());
                num50 += Convert.ToInt32(row2["50"].ToString());
                num51 += Convert.ToInt32(row2["51"].ToString());
                num52 += Convert.ToInt32(row2["52"].ToString());
            }
            command2.Dispose();
            foreach (DataRow dataRow in dataTable.Select("Clave = ''"))
            {
                dataRow["1"] = num1 == 0 ? (object)"" : (object)num1.ToString();
                dataRow["2"] = num2 == 0 ? (object)"" : (object)num2.ToString();
                dataRow["3"] = num3 == 0 ? (object)"" : (object)num3.ToString();
                dataRow["4"] = num4 == 0 ? (object)"" : (object)num4.ToString();
                dataRow["5"] = num5 == 0 ? (object)"" : (object)num5.ToString();
                dataRow["6"] = num6 == 0 ? (object)"" : (object)num6.ToString();
                dataRow["7"] = num7 == 0 ? (object)"" : (object)num7.ToString();
                dataRow["8"] = num8 == 0 ? (object)"" : (object)num8.ToString();
                dataRow["9"] = num9 == 0 ? (object)"" : (object)num9.ToString();
                dataRow["10"] = num10 == 0 ? (object)"" : (object)num10.ToString();
                dataRow["11"] = num11 == 0 ? (object)"" : (object)num11.ToString();
                dataRow["12"] = num12 == 0 ? (object)"" : (object)num12.ToString();
                dataRow["13"] = num13 == 0 ? (object)"" : (object)num13.ToString();
                dataRow["14"] = num14 == 0 ? (object)"" : (object)num14.ToString();
                dataRow["15"] = num15 == 0 ? (object)"" : (object)num15.ToString();
                dataRow["16"] = num16 == 0 ? (object)"" : (object)num16.ToString();
                dataRow["17"] = num17 == 0 ? (object)"" : (object)num17.ToString();
                dataRow["18"] = num18 == 0 ? (object)"" : (object)num18.ToString();
                dataRow["19"] = num19 == 0 ? (object)"" : (object)num19.ToString();
                dataRow["20"] = num20 == 0 ? (object)"" : (object)num20.ToString();
                dataRow["21"] = num21 == 0 ? (object)"" : (object)num21.ToString();
                dataRow["22"] = num22 == 0 ? (object)"" : (object)num22.ToString();
                dataRow["23"] = num23 == 0 ? (object)"" : (object)num23.ToString();
                dataRow["24"] = num24 == 0 ? (object)"" : (object)num24.ToString();
                dataRow["25"] = num25 == 0 ? (object)"" : (object)num25.ToString();
                dataRow["26"] = num26 == 0 ? (object)"" : (object)num26.ToString();
                dataRow["27"] = num27 == 0 ? (object)"" : (object)num27.ToString();
                dataRow["28"] = num28 == 0 ? (object)"" : (object)num28.ToString();
                dataRow["29"] = num29 == 0 ? (object)"" : (object)num29.ToString();
                dataRow["30"] = num30 == 0 ? (object)"" : (object)num30.ToString();
                dataRow["31"] = num31 == 0 ? (object)"" : (object)num31.ToString();
                dataRow["32"] = num32 == 0 ? (object)"" : (object)num32.ToString();
                dataRow["33"] = num33 == 0 ? (object)"" : (object)num33.ToString();
                dataRow["34"] = num34 == 0 ? (object)"" : (object)num34.ToString();
                dataRow["35"] = num35 == 0 ? (object)"" : (object)num35.ToString();
                dataRow["36"] = num36 == 0 ? (object)"" : (object)num36.ToString();
                dataRow["37"] = num37 == 0 ? (object)"" : (object)num37.ToString();
                dataRow["38"] = num38 == 0 ? (object)"" : (object)num38.ToString();
                dataRow["39"] = num39 == 0 ? (object)"" : (object)num39.ToString();
                dataRow["40"] = num40 == 0 ? (object)"" : (object)num40.ToString();
                dataRow["41"] = num41 == 0 ? (object)"" : (object)num41.ToString();
                dataRow["42"] = num42 == 0 ? (object)"" : (object)num42.ToString();
                dataRow["43"] = num43 == 0 ? (object)"" : (object)num43.ToString();
                dataRow["44"] = num44 == 0 ? (object)"" : (object)num44.ToString();
                dataRow["45"] = num45 == 0 ? (object)"" : (object)num45.ToString();
                dataRow["46"] = num46 == 0 ? (object)"" : (object)num46.ToString();
                dataRow["47"] = num47 == 0 ? (object)"" : (object)num47.ToString();
                dataRow["48"] = num48 == 0 ? (object)"" : (object)num48.ToString();
                dataRow["49"] = num49 == 0 ? (object)"" : (object)num49.ToString();
                dataRow["50"] = num50 == 0 ? (object)"" : (object)num50.ToString();
                dataRow["51"] = num51 == 0 ? (object)"" : (object)num51.ToString();
                dataRow["52"] = num52 == 0 ? (object)"" : (object)num52.ToString();
            }
            foreach (DataRow dataRow in dataTable.Select())
            {
                dataRow["1"] = dataRow["1"].ToString() == "0" ? (object)"" : (object)dataRow["1"].ToString();
                dataRow["2"] = dataRow["2"].ToString() == "0" ? (object)"" : (object)dataRow["2"].ToString();
                dataRow["3"] = dataRow["3"].ToString() == "0" ? (object)"" : (object)dataRow["3"].ToString();
                dataRow["4"] = dataRow["4"].ToString() == "0" ? (object)"" : (object)dataRow["4"].ToString();
                dataRow["5"] = dataRow["5"].ToString() == "0" ? (object)"" : (object)dataRow["5"].ToString();
                dataRow["6"] = dataRow["6"].ToString() == "0" ? (object)"" : (object)dataRow["6"].ToString();
                dataRow["7"] = dataRow["7"].ToString() == "0" ? (object)"" : (object)dataRow["7"].ToString();
                dataRow["8"] = dataRow["8"].ToString() == "0" ? (object)"" : (object)dataRow["8"].ToString();
                dataRow["9"] = dataRow["9"].ToString() == "0" ? (object)"" : (object)dataRow["9"].ToString();
                dataRow["10"] = dataRow["10"].ToString() == "0" ? (object)"" : (object)dataRow["10"].ToString();
                dataRow["11"] = dataRow["11"].ToString() == "0" ? (object)"" : (object)dataRow["11"].ToString();
                dataRow["12"] = dataRow["12"].ToString() == "0" ? (object)"" : (object)dataRow["12"].ToString();
                dataRow["13"] = dataRow["13"].ToString() == "0" ? (object)"" : (object)dataRow["13"].ToString();
                dataRow["14"] = dataRow["14"].ToString() == "0" ? (object)"" : (object)dataRow["14"].ToString();
                dataRow["15"] = dataRow["15"].ToString() == "0" ? (object)"" : (object)dataRow["15"].ToString();
                dataRow["16"] = dataRow["16"].ToString() == "0" ? (object)"" : (object)dataRow["16"].ToString();
                dataRow["17"] = dataRow["17"].ToString() == "0" ? (object)"" : (object)dataRow["17"].ToString();
                dataRow["18"] = dataRow["18"].ToString() == "0" ? (object)"" : (object)dataRow["18"].ToString();
                dataRow["19"] = dataRow["19"].ToString() == "0" ? (object)"" : (object)dataRow["19"].ToString();
                dataRow["20"] = dataRow["20"].ToString() == "0" ? (object)"" : (object)dataRow["20"].ToString();
                dataRow["21"] = dataRow["21"].ToString() == "0" ? (object)"" : (object)dataRow["21"].ToString();
                dataRow["22"] = dataRow["22"].ToString() == "0" ? (object)"" : (object)dataRow["22"].ToString();
                dataRow["23"] = dataRow["23"].ToString() == "0" ? (object)"" : (object)dataRow["23"].ToString();
                dataRow["24"] = dataRow["24"].ToString() == "0" ? (object)"" : (object)dataRow["24"].ToString();
                dataRow["25"] = dataRow["25"].ToString() == "0" ? (object)"" : (object)dataRow["25"].ToString();
                dataRow["26"] = dataRow["26"].ToString() == "0" ? (object)"" : (object)dataRow["26"].ToString();
                dataRow["27"] = dataRow["27"].ToString() == "0" ? (object)"" : (object)dataRow["27"].ToString();
                dataRow["28"] = dataRow["28"].ToString() == "0" ? (object)"" : (object)dataRow["28"].ToString();
                dataRow["29"] = dataRow["29"].ToString() == "0" ? (object)"" : (object)dataRow["29"].ToString();
                dataRow["30"] = dataRow["30"].ToString() == "0" ? (object)"" : (object)dataRow["30"].ToString();
                dataRow["31"] = dataRow["31"].ToString() == "0" ? (object)"" : (object)dataRow["31"].ToString();
                dataRow["32"] = dataRow["32"].ToString() == "0" ? (object)"" : (object)dataRow["32"].ToString();
                dataRow["33"] = dataRow["33"].ToString() == "0" ? (object)"" : (object)dataRow["33"].ToString();
                dataRow["34"] = dataRow["34"].ToString() == "0" ? (object)"" : (object)dataRow["34"].ToString();
                dataRow["35"] = dataRow["35"].ToString() == "0" ? (object)"" : (object)dataRow["35"].ToString();
                dataRow["36"] = dataRow["36"].ToString() == "0" ? (object)"" : (object)dataRow["36"].ToString();
                dataRow["37"] = dataRow["37"].ToString() == "0" ? (object)"" : (object)dataRow["37"].ToString();
                dataRow["38"] = dataRow["38"].ToString() == "0" ? (object)"" : (object)dataRow["38"].ToString();
                dataRow["39"] = dataRow["39"].ToString() == "0" ? (object)"" : (object)dataRow["39"].ToString();
                dataRow["40"] = dataRow["40"].ToString() == "0" ? (object)"" : (object)dataRow["40"].ToString();
                dataRow["41"] = dataRow["41"].ToString() == "0" ? (object)"" : (object)dataRow["41"].ToString();
                dataRow["42"] = dataRow["42"].ToString() == "0" ? (object)"" : (object)dataRow["42"].ToString();
                dataRow["43"] = dataRow["43"].ToString() == "0" ? (object)"" : (object)dataRow["43"].ToString();
                dataRow["44"] = dataRow["44"].ToString() == "0" ? (object)"" : (object)dataRow["44"].ToString();
                dataRow["45"] = dataRow["45"].ToString() == "0" ? (object)"" : (object)dataRow["45"].ToString();
                dataRow["46"] = dataRow["46"].ToString() == "0" ? (object)"" : (object)dataRow["46"].ToString();
                dataRow["47"] = dataRow["47"].ToString() == "0" ? (object)"" : (object)dataRow["47"].ToString();
                dataRow["48"] = dataRow["48"].ToString() == "0" ? (object)"" : (object)dataRow["48"].ToString();
                dataRow["49"] = dataRow["49"].ToString() == "0" ? (object)"" : (object)dataRow["49"].ToString();
                dataRow["50"] = dataRow["50"].ToString() == "0" ? (object)"" : (object)dataRow["50"].ToString();
                dataRow["51"] = dataRow["51"].ToString() == "0" ? (object)"" : (object)dataRow["51"].ToString();
                dataRow["52"] = dataRow["52"].ToString() == "0" ? (object)"" : (object)dataRow["52"].ToString();
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=CajasSemana.xls");
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
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Clave", typeof(string));
            dataTable.Columns.Add("Problema", typeof(string));
            dataTable.Columns.Add("1", typeof(string));
            dataTable.Columns.Add("2", typeof(string));
            dataTable.Columns.Add("3", typeof(string));
            dataTable.Columns.Add("4", typeof(string));
            dataTable.Columns.Add("5", typeof(string));
            dataTable.Columns.Add("6", typeof(string));
            dataTable.Columns.Add("7", typeof(string));
            dataTable.Columns.Add("8", typeof(string));
            dataTable.Columns.Add("9", typeof(string));
            dataTable.Columns.Add("10", typeof(string));
            dataTable.Columns.Add("11", typeof(string));
            dataTable.Columns.Add("12", typeof(string));
            dataTable.Columns.Add("13", typeof(string));
            dataTable.Columns.Add("14", typeof(string));
            dataTable.Columns.Add("15", typeof(string));
            dataTable.Columns.Add("16", typeof(string));
            dataTable.Columns.Add("17", typeof(string));
            dataTable.Columns.Add("18", typeof(string));
            dataTable.Columns.Add("19", typeof(string));
            dataTable.Columns.Add("20", typeof(string));
            dataTable.Columns.Add("21", typeof(string));
            dataTable.Columns.Add("22", typeof(string));
            dataTable.Columns.Add("23", typeof(string));
            dataTable.Columns.Add("24", typeof(string));
            dataTable.Columns.Add("25", typeof(string));
            dataTable.Columns.Add("26", typeof(string));
            dataTable.Columns.Add("27", typeof(string));
            dataTable.Columns.Add("28", typeof(string));
            dataTable.Columns.Add("29", typeof(string));
            dataTable.Columns.Add("30", typeof(string));
            dataTable.Columns.Add("31", typeof(string));
            dataTable.Columns.Add("32", typeof(string));
            dataTable.Columns.Add("33", typeof(string));
            dataTable.Columns.Add("34", typeof(string));
            dataTable.Columns.Add("35", typeof(string));
            dataTable.Columns.Add("36", typeof(string));
            dataTable.Columns.Add("37", typeof(string));
            dataTable.Columns.Add("38", typeof(string));
            dataTable.Columns.Add("39", typeof(string));
            dataTable.Columns.Add("40", typeof(string));
            dataTable.Columns.Add("41", typeof(string));
            dataTable.Columns.Add("42", typeof(string));
            dataTable.Columns.Add("43", typeof(string));
            dataTable.Columns.Add("44", typeof(string));
            dataTable.Columns.Add("45", typeof(string));
            dataTable.Columns.Add("46", typeof(string));
            dataTable.Columns.Add("47", typeof(string));
            dataTable.Columns.Add("48", typeof(string));
            dataTable.Columns.Add("49", typeof(string));
            dataTable.Columns.Add("50", typeof(string));
            dataTable.Columns.Add("51", typeof(string));
            dataTable.Columns.Add("52", typeof(string));
            DataRow row1 = dataTable.NewRow();
            row1["Clave"] = (object)"";
            row1["Problema"] = (object)"TOTALES";
            row1["1"] = (object)"0";
            row1["2"] = (object)"0";
            row1["3"] = (object)"0";
            row1["4"] = (object)"0";
            row1["5"] = (object)"0";
            row1["6"] = (object)"0";
            row1["7"] = (object)"0";
            row1["8"] = (object)"0";
            row1["9"] = (object)"0";
            row1["10"] = (object)"0";
            row1["11"] = (object)"0";
            row1["12"] = (object)"0";
            row1["13"] = (object)"0";
            row1["14"] = (object)"0";
            row1["15"] = (object)"0";
            row1["16"] = (object)"0";
            row1["17"] = (object)"0";
            row1["18"] = (object)"0";
            row1["19"] = (object)"0";
            row1["20"] = (object)"0";
            row1["21"] = (object)"0";
            row1["22"] = (object)"0";
            row1["23"] = (object)"0";
            row1["24"] = (object)"0";
            row1["25"] = (object)"0";
            row1["26"] = (object)"0";
            row1["27"] = (object)"0";
            row1["28"] = (object)"0";
            row1["29"] = (object)"0";
            row1["30"] = (object)"0";
            row1["31"] = (object)"0";
            row1["32"] = (object)"0";
            row1["33"] = (object)"0";
            row1["34"] = (object)"0";
            row1["35"] = (object)"0";
            row1["36"] = (object)"0";
            row1["37"] = (object)"0";
            row1["38"] = (object)"0";
            row1["39"] = (object)"0";
            row1["40"] = (object)"0";
            row1["41"] = (object)"0";
            row1["42"] = (object)"0";
            row1["43"] = (object)"0";
            row1["44"] = (object)"0";
            row1["45"] = (object)"0";
            row1["46"] = (object)"0";
            row1["47"] = (object)"0";
            row1["48"] = (object)"0";
            row1["49"] = (object)"0";
            row1["50"] = (object)"0";
            row1["51"] = (object)"0";
            row1["52"] = (object)"0";
            dataTable.Rows.Add(row1);
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT * FROM tb_cat_problemas ORDER BY pro_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["Clave"] = (object)sqlDataReader1.GetValue(0).ToString().Trim();
                    row2["Problema"] = (object)sqlDataReader1.GetValue(1).ToString().Trim();
                    row2["1"] = (object)"0";
                    row2["2"] = (object)"0";
                    row2["3"] = (object)"0";
                    row2["4"] = (object)"0";
                    row2["5"] = (object)"0";
                    row2["6"] = (object)"0";
                    row2["7"] = (object)"0";
                    row2["8"] = (object)"0";
                    row2["9"] = (object)"0";
                    row2["10"] = (object)"0";
                    row2["11"] = (object)"0";
                    row2["12"] = (object)"0";
                    row2["13"] = (object)"0";
                    row2["14"] = (object)"0";
                    row2["15"] = (object)"0";
                    row2["16"] = (object)"0";
                    row2["17"] = (object)"0";
                    row2["18"] = (object)"0";
                    row2["19"] = (object)"0";
                    row2["20"] = (object)"0";
                    row2["21"] = (object)"0";
                    row2["22"] = (object)"0";
                    row2["23"] = (object)"0";
                    row2["24"] = (object)"0";
                    row2["25"] = (object)"0";
                    row2["26"] = (object)"0";
                    row2["27"] = (object)"0";
                    row2["28"] = (object)"0";
                    row2["29"] = (object)"0";
                    row2["30"] = (object)"0";
                    row2["31"] = (object)"0";
                    row2["32"] = (object)"0";
                    row2["33"] = (object)"0";
                    row2["34"] = (object)"0";
                    row2["35"] = (object)"0";
                    row2["36"] = (object)"0";
                    row2["37"] = (object)"0";
                    row2["38"] = (object)"0";
                    row2["39"] = (object)"0";
                    row2["40"] = (object)"0";
                    row2["41"] = (object)"0";
                    row2["42"] = (object)"0";
                    row2["43"] = (object)"0";
                    row2["44"] = (object)"0";
                    row2["45"] = (object)"0";
                    row2["46"] = (object)"0";
                    row2["47"] = (object)"0";
                    row2["48"] = (object)"0";
                    row2["49"] = (object)"0";
                    row2["50"] = (object)"0";
                    row2["51"] = (object)"0";
                    row2["52"] = (object)"0";
                    dataTable.Rows.Add(row2);
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = 0;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            int num28 = 0;
            int num29 = 0;
            int num30 = 0;
            int num31 = 0;
            int num32 = 0;
            int num33 = 0;
            int num34 = 0;
            int num35 = 0;
            int num36 = 0;
            int num37 = 0;
            int num38 = 0;
            int num39 = 0;
            int num40 = 0;
            int num41 = 0;
            int num42 = 0;
            int num43 = 0;
            int num44 = 0;
            int num45 = 0;
            int num46 = 0;
            int num47 = 0;
            int num48 = 0;
            int num49 = 0;
            int num50 = 0;
            int num51 = 0;
            int num52 = 0;
            string str1 = DateTime.Now.Year.ToString();
            string str2 = "01/01/" + str1;
            string str3 = "31/12/" + str1;
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "select a.que_folio, a.que_sememb AS que_semana, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' AND a.que_status in ('A','T') order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(1).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        string str4 = sqlDataReader2["que_semana"].ToString().Trim();
                        int num53;
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["1"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["1"] = (object)str5;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["2"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["2"] = (object)str5;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["3"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["3"] = (object)str5;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["4"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["4"] = (object)str5;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["5"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["5"] = (object)str5;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["6"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["6"] = (object)str5;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["7"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["7"] = (object)str5;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["8"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["8"] = (object)str5;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["9"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["9"] = (object)str5;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["10"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["10"] = (object)str5;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["11"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["11"] = (object)str5;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["12"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["12"] = (object)str5;
                        }
                        if (str4 == "13")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["13"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["13"] = (object)str5;
                        }
                        if (str4 == "14")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["14"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["14"] = (object)str5;
                        }
                        if (str4 == "15")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["15"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["15"] = (object)str5;
                        }
                        if (str4 == "16")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["16"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["16"] = (object)str5;
                        }
                        if (str4 == "17")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["17"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["17"] = (object)str5;
                        }
                        if (str4 == "18")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["18"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["18"] = (object)str5;
                        }
                        if (str4 == "19")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["19"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["19"] = (object)str5;
                        }
                        if (str4 == "20")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["20"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["20"] = (object)str5;
                        }
                        if (str4 == "21")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["21"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["21"] = (object)str5;
                        }
                        if (str4 == "22")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["22"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["22"] = (object)str5;
                        }
                        if (str4 == "23")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["23"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["23"] = (object)str5;
                        }
                        if (str4 == "24")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["24"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["24"] = (object)str5;
                        }
                        if (str4 == "25")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["25"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["25"] = (object)str5;
                        }
                        if (str4 == "26")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["26"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["26"] = (object)str5;
                        }
                        if (str4 == "27")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["27"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["27"] = (object)str5;
                        }
                        if (str4 == "28")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["28"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["28"] = (object)str5;
                        }
                        if (str4 == "29")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["29"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["29"] = (object)str5;
                        }
                        if (str4 == "30")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["30"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["30"] = (object)str5;
                        }
                        if (str4 == "31")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["31"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["31"] = (object)str5;
                        }
                        if (str4 == "32")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["32"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["32"] = (object)str5;
                        }
                        if (str4 == "33")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["33"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["33"] = (object)str5;
                        }
                        if (str4 == "34")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["34"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["34"] = (object)str5;
                        }
                        if (str4 == "35")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["35"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["35"] = (object)str5;
                        }
                        if (str4 == "36")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["36"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["36"] = (object)str5;
                        }
                        if (str4 == "37")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["37"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["37"] = (object)str5;
                        }
                        if (str4 == "38")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["38"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["38"] = (object)str5;
                        }
                        if (str4 == "39")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["39"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["39"] = (object)str5;
                        }
                        if (str4 == "40")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["40"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["40"] = (object)str5;
                        }
                        if (str4 == "41")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["41"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["41"] = (object)str5;
                        }
                        if (str4 == "42")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["42"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["42"] = (object)str5;
                        }
                        if (str4 == "43")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["43"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["43"] = (object)str5;
                        }
                        if (str4 == "44")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["44"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["44"] = (object)str5;
                        }
                        if (str4 == "45")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["45"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["45"] = (object)str5;
                        }
                        if (str4 == "46")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["46"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["46"] = (object)str5;
                        }
                        if (str4 == "47")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["47"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["47"] = (object)str5;
                        }
                        if (str4 == "48")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["48"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["48"] = (object)str5;
                        }
                        if (str4 == "49")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["49"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["49"] = (object)str5;
                        }
                        if (str4 == "50")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["50"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["50"] = (object)str5;
                        }
                        if (str4 == "51'")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["51"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["51"] = (object)str5;
                        }
                        if (str4 == "52")
                        {
                            DataRow dataRow2 = dataRow1;
                            num53 = Convert.ToInt32(dataRow1["52"].ToString()) + int32;
                            string str5 = num53.ToString();
                            dataRow2["52"] = (object)str5;
                        }
                    }
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row2["1"].ToString());
                num2 += Convert.ToInt32(row2["2"].ToString());
                num3 += Convert.ToInt32(row2["3"].ToString());
                num4 += Convert.ToInt32(row2["4"].ToString());
                num5 += Convert.ToInt32(row2["5"].ToString());
                num6 += Convert.ToInt32(row2["6"].ToString());
                num7 += Convert.ToInt32(row2["7"].ToString());
                num8 += Convert.ToInt32(row2["8"].ToString());
                num9 += Convert.ToInt32(row2["9"].ToString());
                num10 += Convert.ToInt32(row2["10"].ToString());
                num11 += Convert.ToInt32(row2["11"].ToString());
                num12 += Convert.ToInt32(row2["12"].ToString());
                num13 += Convert.ToInt32(row2["13"].ToString());
                num14 += Convert.ToInt32(row2["14"].ToString());
                num15 += Convert.ToInt32(row2["15"].ToString());
                num16 += Convert.ToInt32(row2["16"].ToString());
                num17 += Convert.ToInt32(row2["17"].ToString());
                num18 += Convert.ToInt32(row2["18"].ToString());
                num19 += Convert.ToInt32(row2["19"].ToString());
                num20 += Convert.ToInt32(row2["20"].ToString());
                num21 += Convert.ToInt32(row2["21"].ToString());
                num22 += Convert.ToInt32(row2["22"].ToString());
                num23 += Convert.ToInt32(row2["23"].ToString());
                num24 += Convert.ToInt32(row2["24"].ToString());
                num25 += Convert.ToInt32(row2["25"].ToString());
                num26 += Convert.ToInt32(row2["26"].ToString());
                num27 += Convert.ToInt32(row2["27"].ToString());
                num28 += Convert.ToInt32(row2["28"].ToString());
                num29 += Convert.ToInt32(row2["29"].ToString());
                num30 += Convert.ToInt32(row2["30"].ToString());
                num31 += Convert.ToInt32(row2["31"].ToString());
                num32 += Convert.ToInt32(row2["32"].ToString());
                num33 += Convert.ToInt32(row2["33"].ToString());
                num34 += Convert.ToInt32(row2["34"].ToString());
                num35 += Convert.ToInt32(row2["35"].ToString());
                num36 += Convert.ToInt32(row2["36"].ToString());
                num37 += Convert.ToInt32(row2["37"].ToString());
                num38 += Convert.ToInt32(row2["38"].ToString());
                num39 += Convert.ToInt32(row2["39"].ToString());
                num40 += Convert.ToInt32(row2["40"].ToString());
                num41 += Convert.ToInt32(row2["41"].ToString());
                num42 += Convert.ToInt32(row2["42"].ToString());
                num43 += Convert.ToInt32(row2["43"].ToString());
                num44 += Convert.ToInt32(row2["44"].ToString());
                num45 += Convert.ToInt32(row2["45"].ToString());
                num46 += Convert.ToInt32(row2["46"].ToString());
                num47 += Convert.ToInt32(row2["47"].ToString());
                num48 += Convert.ToInt32(row2["48"].ToString());
                num49 += Convert.ToInt32(row2["49"].ToString());
                num50 += Convert.ToInt32(row2["50"].ToString());
                num51 += Convert.ToInt32(row2["51"].ToString());
                num52 += Convert.ToInt32(row2["52"].ToString());
            }
            command2.Dispose();
            foreach (DataRow dataRow in dataTable.Select("Clave = ''"))
            {
                dataRow["1"] = num1 == 0 ? (object)"" : (object)num1.ToString();
                dataRow["2"] = num2 == 0 ? (object)"" : (object)num2.ToString();
                dataRow["3"] = num3 == 0 ? (object)"" : (object)num3.ToString();
                dataRow["4"] = num4 == 0 ? (object)"" : (object)num4.ToString();
                dataRow["5"] = num5 == 0 ? (object)"" : (object)num5.ToString();
                dataRow["6"] = num6 == 0 ? (object)"" : (object)num6.ToString();
                dataRow["7"] = num7 == 0 ? (object)"" : (object)num7.ToString();
                dataRow["8"] = num8 == 0 ? (object)"" : (object)num8.ToString();
                dataRow["9"] = num9 == 0 ? (object)"" : (object)num9.ToString();
                dataRow["10"] = num10 == 0 ? (object)"" : (object)num10.ToString();
                dataRow["11"] = num11 == 0 ? (object)"" : (object)num11.ToString();
                dataRow["12"] = num12 == 0 ? (object)"" : (object)num12.ToString();
                dataRow["13"] = num13 == 0 ? (object)"" : (object)num13.ToString();
                dataRow["14"] = num14 == 0 ? (object)"" : (object)num14.ToString();
                dataRow["15"] = num15 == 0 ? (object)"" : (object)num15.ToString();
                dataRow["16"] = num16 == 0 ? (object)"" : (object)num16.ToString();
                dataRow["17"] = num17 == 0 ? (object)"" : (object)num17.ToString();
                dataRow["18"] = num18 == 0 ? (object)"" : (object)num18.ToString();
                dataRow["19"] = num19 == 0 ? (object)"" : (object)num19.ToString();
                dataRow["20"] = num20 == 0 ? (object)"" : (object)num20.ToString();
                dataRow["21"] = num21 == 0 ? (object)"" : (object)num21.ToString();
                dataRow["22"] = num22 == 0 ? (object)"" : (object)num22.ToString();
                dataRow["23"] = num23 == 0 ? (object)"" : (object)num23.ToString();
                dataRow["24"] = num24 == 0 ? (object)"" : (object)num24.ToString();
                dataRow["25"] = num25 == 0 ? (object)"" : (object)num25.ToString();
                dataRow["26"] = num26 == 0 ? (object)"" : (object)num26.ToString();
                dataRow["27"] = num27 == 0 ? (object)"" : (object)num27.ToString();
                dataRow["28"] = num28 == 0 ? (object)"" : (object)num28.ToString();
                dataRow["29"] = num29 == 0 ? (object)"" : (object)num29.ToString();
                dataRow["30"] = num30 == 0 ? (object)"" : (object)num30.ToString();
                dataRow["31"] = num31 == 0 ? (object)"" : (object)num31.ToString();
                dataRow["32"] = num32 == 0 ? (object)"" : (object)num32.ToString();
                dataRow["33"] = num33 == 0 ? (object)"" : (object)num33.ToString();
                dataRow["34"] = num34 == 0 ? (object)"" : (object)num34.ToString();
                dataRow["35"] = num35 == 0 ? (object)"" : (object)num35.ToString();
                dataRow["36"] = num36 == 0 ? (object)"" : (object)num36.ToString();
                dataRow["37"] = num37 == 0 ? (object)"" : (object)num37.ToString();
                dataRow["38"] = num38 == 0 ? (object)"" : (object)num38.ToString();
                dataRow["39"] = num39 == 0 ? (object)"" : (object)num39.ToString();
                dataRow["40"] = num40 == 0 ? (object)"" : (object)num40.ToString();
                dataRow["41"] = num41 == 0 ? (object)"" : (object)num41.ToString();
                dataRow["42"] = num42 == 0 ? (object)"" : (object)num42.ToString();
                dataRow["43"] = num43 == 0 ? (object)"" : (object)num43.ToString();
                dataRow["44"] = num44 == 0 ? (object)"" : (object)num44.ToString();
                dataRow["45"] = num45 == 0 ? (object)"" : (object)num45.ToString();
                dataRow["46"] = num46 == 0 ? (object)"" : (object)num46.ToString();
                dataRow["47"] = num47 == 0 ? (object)"" : (object)num47.ToString();
                dataRow["48"] = num48 == 0 ? (object)"" : (object)num48.ToString();
                dataRow["49"] = num49 == 0 ? (object)"" : (object)num49.ToString();
                dataRow["50"] = num50 == 0 ? (object)"" : (object)num50.ToString();
                dataRow["51"] = num51 == 0 ? (object)"" : (object)num51.ToString();
                dataRow["52"] = num52 == 0 ? (object)"" : (object)num52.ToString();
            }
            foreach (DataRow dataRow in dataTable.Select())
            {
                dataRow["1"] = dataRow["1"].ToString() == "0" ? (object)"" : (object)dataRow["1"].ToString();
                dataRow["2"] = dataRow["2"].ToString() == "0" ? (object)"" : (object)dataRow["2"].ToString();
                dataRow["3"] = dataRow["3"].ToString() == "0" ? (object)"" : (object)dataRow["3"].ToString();
                dataRow["4"] = dataRow["4"].ToString() == "0" ? (object)"" : (object)dataRow["4"].ToString();
                dataRow["5"] = dataRow["5"].ToString() == "0" ? (object)"" : (object)dataRow["5"].ToString();
                dataRow["6"] = dataRow["6"].ToString() == "0" ? (object)"" : (object)dataRow["6"].ToString();
                dataRow["7"] = dataRow["7"].ToString() == "0" ? (object)"" : (object)dataRow["7"].ToString();
                dataRow["8"] = dataRow["8"].ToString() == "0" ? (object)"" : (object)dataRow["8"].ToString();
                dataRow["9"] = dataRow["9"].ToString() == "0" ? (object)"" : (object)dataRow["9"].ToString();
                dataRow["10"] = dataRow["10"].ToString() == "0" ? (object)"" : (object)dataRow["10"].ToString();
                dataRow["11"] = dataRow["11"].ToString() == "0" ? (object)"" : (object)dataRow["11"].ToString();
                dataRow["12"] = dataRow["12"].ToString() == "0" ? (object)"" : (object)dataRow["12"].ToString();
                dataRow["13"] = dataRow["13"].ToString() == "0" ? (object)"" : (object)dataRow["13"].ToString();
                dataRow["14"] = dataRow["14"].ToString() == "0" ? (object)"" : (object)dataRow["14"].ToString();
                dataRow["15"] = dataRow["15"].ToString() == "0" ? (object)"" : (object)dataRow["15"].ToString();
                dataRow["16"] = dataRow["16"].ToString() == "0" ? (object)"" : (object)dataRow["16"].ToString();
                dataRow["17"] = dataRow["17"].ToString() == "0" ? (object)"" : (object)dataRow["17"].ToString();
                dataRow["18"] = dataRow["18"].ToString() == "0" ? (object)"" : (object)dataRow["18"].ToString();
                dataRow["19"] = dataRow["19"].ToString() == "0" ? (object)"" : (object)dataRow["19"].ToString();
                dataRow["20"] = dataRow["20"].ToString() == "0" ? (object)"" : (object)dataRow["20"].ToString();
                dataRow["21"] = dataRow["21"].ToString() == "0" ? (object)"" : (object)dataRow["21"].ToString();
                dataRow["22"] = dataRow["22"].ToString() == "0" ? (object)"" : (object)dataRow["22"].ToString();
                dataRow["23"] = dataRow["23"].ToString() == "0" ? (object)"" : (object)dataRow["23"].ToString();
                dataRow["24"] = dataRow["24"].ToString() == "0" ? (object)"" : (object)dataRow["24"].ToString();
                dataRow["25"] = dataRow["25"].ToString() == "0" ? (object)"" : (object)dataRow["25"].ToString();
                dataRow["26"] = dataRow["26"].ToString() == "0" ? (object)"" : (object)dataRow["26"].ToString();
                dataRow["27"] = dataRow["27"].ToString() == "0" ? (object)"" : (object)dataRow["27"].ToString();
                dataRow["28"] = dataRow["28"].ToString() == "0" ? (object)"" : (object)dataRow["28"].ToString();
                dataRow["29"] = dataRow["29"].ToString() == "0" ? (object)"" : (object)dataRow["29"].ToString();
                dataRow["30"] = dataRow["30"].ToString() == "0" ? (object)"" : (object)dataRow["30"].ToString();
                dataRow["31"] = dataRow["31"].ToString() == "0" ? (object)"" : (object)dataRow["31"].ToString();
                dataRow["32"] = dataRow["32"].ToString() == "0" ? (object)"" : (object)dataRow["32"].ToString();
                dataRow["33"] = dataRow["33"].ToString() == "0" ? (object)"" : (object)dataRow["33"].ToString();
                dataRow["34"] = dataRow["34"].ToString() == "0" ? (object)"" : (object)dataRow["34"].ToString();
                dataRow["35"] = dataRow["35"].ToString() == "0" ? (object)"" : (object)dataRow["35"].ToString();
                dataRow["36"] = dataRow["36"].ToString() == "0" ? (object)"" : (object)dataRow["36"].ToString();
                dataRow["37"] = dataRow["37"].ToString() == "0" ? (object)"" : (object)dataRow["37"].ToString();
                dataRow["38"] = dataRow["38"].ToString() == "0" ? (object)"" : (object)dataRow["38"].ToString();
                dataRow["39"] = dataRow["39"].ToString() == "0" ? (object)"" : (object)dataRow["39"].ToString();
                dataRow["40"] = dataRow["40"].ToString() == "0" ? (object)"" : (object)dataRow["40"].ToString();
                dataRow["41"] = dataRow["41"].ToString() == "0" ? (object)"" : (object)dataRow["41"].ToString();
                dataRow["42"] = dataRow["42"].ToString() == "0" ? (object)"" : (object)dataRow["42"].ToString();
                dataRow["43"] = dataRow["43"].ToString() == "0" ? (object)"" : (object)dataRow["43"].ToString();
                dataRow["44"] = dataRow["44"].ToString() == "0" ? (object)"" : (object)dataRow["44"].ToString();
                dataRow["45"] = dataRow["45"].ToString() == "0" ? (object)"" : (object)dataRow["45"].ToString();
                dataRow["46"] = dataRow["46"].ToString() == "0" ? (object)"" : (object)dataRow["46"].ToString();
                dataRow["47"] = dataRow["47"].ToString() == "0" ? (object)"" : (object)dataRow["47"].ToString();
                dataRow["48"] = dataRow["48"].ToString() == "0" ? (object)"" : (object)dataRow["48"].ToString();
                dataRow["49"] = dataRow["49"].ToString() == "0" ? (object)"" : (object)dataRow["49"].ToString();
                dataRow["50"] = dataRow["50"].ToString() == "0" ? (object)"" : (object)dataRow["50"].ToString();
                dataRow["51"] = dataRow["51"].ToString() == "0" ? (object)"" : (object)dataRow["51"].ToString();
                dataRow["52"] = dataRow["52"].ToString() == "0" ? (object)"" : (object)dataRow["52"].ToString();
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=CajasSemana.xls");
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
    }
}