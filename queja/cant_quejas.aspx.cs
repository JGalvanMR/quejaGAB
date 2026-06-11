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
    public partial class cant_quejas : System.Web.UI.Page
    {
        private DataTable dtQuejas = new DataTable();
        private DataTable dtExcel = new DataTable();
        private DataTable dtQuejas2 = new DataTable();
        private DataTable dtCedis = new DataTable();

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
            dataTable.Columns.Add("Enero", typeof(string));
            dataTable.Columns.Add("Febrero", typeof(string));
            dataTable.Columns.Add("Marzo", typeof(string));
            dataTable.Columns.Add("Abril", typeof(string));
            dataTable.Columns.Add("Mayo", typeof(string));
            dataTable.Columns.Add("Junio", typeof(string));
            dataTable.Columns.Add("Julio", typeof(string));
            dataTable.Columns.Add("Agosto", typeof(string));
            dataTable.Columns.Add("Septiembre", typeof(string));
            dataTable.Columns.Add("Octubre", typeof(string));
            dataTable.Columns.Add("Noviembre", typeof(string));
            dataTable.Columns.Add("Diciembre", typeof(string));
            dataTable.Columns.Add("Total", typeof(string));
            this.dtExcel.Columns.Add("Problema", typeof(string));
            this.dtExcel.Columns.Add("Enero", typeof(string));
            this.dtExcel.Columns.Add("Febrero", typeof(string));
            this.dtExcel.Columns.Add("Marzo", typeof(string));
            this.dtExcel.Columns.Add("Abril", typeof(string));
            this.dtExcel.Columns.Add("Mayo", typeof(string));
            this.dtExcel.Columns.Add("Junio", typeof(string));
            this.dtExcel.Columns.Add("Julio", typeof(string));
            this.dtExcel.Columns.Add("Agosto", typeof(string));
            this.dtExcel.Columns.Add("Septiembre", typeof(string));
            this.dtExcel.Columns.Add("Octubre", typeof(string));
            this.dtExcel.Columns.Add("Noviembre", typeof(string));
            this.dtExcel.Columns.Add("Diciembre", typeof(string));
            this.dtExcel.Columns.Add("Total", typeof(string));
            this.dtCedis.Columns.Add("Problema", typeof(string));
            this.dtCedis.Columns.Add("Enero", typeof(string));
            this.dtCedis.Columns.Add("Febrero", typeof(string));
            this.dtCedis.Columns.Add("Marzo", typeof(string));
            this.dtCedis.Columns.Add("Abril", typeof(string));
            this.dtCedis.Columns.Add("Mayo", typeof(string));
            this.dtCedis.Columns.Add("Junio", typeof(string));
            this.dtCedis.Columns.Add("Julio", typeof(string));
            this.dtCedis.Columns.Add("Agosto", typeof(string));
            this.dtCedis.Columns.Add("Septiembre", typeof(string));
            this.dtCedis.Columns.Add("Octubre", typeof(string));
            this.dtCedis.Columns.Add("Noviembre", typeof(string));
            this.dtCedis.Columns.Add("Diciembre", typeof(string));
            this.dtCedis.Columns.Add("Total", typeof(string));
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
                    row["Enero"] = (object)"0";
                    row["Febrero"] = (object)"0";
                    row["Marzo"] = (object)"0";
                    row["Abril"] = (object)"0";
                    row["Mayo"] = (object)"0";
                    row["Junio"] = (object)"0";
                    row["Julio"] = (object)"0";
                    row["Agosto"] = (object)"0";
                    row["Septiembre"] = (object)"0";
                    row["Octubre"] = (object)"0";
                    row["Noviembre"] = (object)"0";
                    row["Diciembre"] = (object)"0";
                    row["Total"] = (object)"0";
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
            command2.CommandText = "select a.que_folio, a.que_fechaemb AS que_fecha, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.cedis = '" + this.lblCedis.Text + "' and a.que_status in ('A', 'T') order by a.que_fechaemb";
            if (this.lblCedis.Text == "AGUILARES")
                command2.CommandText = "select a.que_folio, a.que_fechaemb AS que_fecha, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c, tb_cat_areas_queja d WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.cedis = 'COMERCIALIZADORA GAB' and a.que_status in ('A', 'T') AND a.area_queja = d.id_area and d.area_nombre = 'EMPAQUE_AGUILARES' order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(4).ToString().Trim());
                    foreach (DataRow dataRow in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        string str4 = Convert.ToDateTime(sqlDataReader2.GetValue(1).ToString().Trim()).Month.ToString();
                        if (str4 == "1")
                        {
                            dataRow["Enero"] = (object)(Convert.ToInt32(dataRow["Enero"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "2")
                        {
                            dataRow["Febrero"] = (object)(Convert.ToInt32(dataRow["Febrero"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "3")
                        {
                            dataRow["Marzo"] = (object)(Convert.ToInt32(dataRow["Marzo"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "4")
                        {
                            dataRow["Abril"] = (object)(Convert.ToInt32(dataRow["Abril"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "5")
                        {
                            dataRow["Mayo"] = (object)(Convert.ToInt32(dataRow["Mayo"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "6")
                        {
                            dataRow["Junio"] = (object)(Convert.ToInt32(dataRow["Junio"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "7")
                        {
                            dataRow["Julio"] = (object)(Convert.ToInt32(dataRow["Julio"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "8")
                        {
                            dataRow["Agosto"] = (object)(Convert.ToInt32(dataRow["Agosto"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "9")
                        {
                            dataRow["Septiembre"] = (object)(Convert.ToInt32(dataRow["Septiembre"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "10")
                        {
                            dataRow["Octubre"] = (object)(Convert.ToInt32(dataRow["Octubre"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "11")
                        {
                            dataRow["Noviembre"] = (object)(Convert.ToInt32(dataRow["Noviembre"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
                        }
                        if (str4 == "12")
                        {
                            dataRow["Diciembre"] = (object)(Convert.ToInt32(dataRow["Diciembre"].ToString()) + int32).ToString();
                            dataRow["Total"] = (object)(Convert.ToInt32(dataRow["Total"].ToString()) + int32).ToString();
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
            string str5 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>" + "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Enero</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Febrero</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Marzo</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Abril</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Mayo</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Junio</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Julio</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Agosto</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Septiembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Octubre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Noviembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Diciembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Total</font></th>" + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row["Enero"].ToString());
                num2 += Convert.ToInt32(row["Febrero"].ToString());
                num3 += Convert.ToInt32(row["Marzo"].ToString());
                num4 += Convert.ToInt32(row["Abril"].ToString());
                num5 += Convert.ToInt32(row["Mayo"].ToString());
                num6 += Convert.ToInt32(row["Junio"].ToString());
                num7 += Convert.ToInt32(row["Julio"].ToString());
                num8 += Convert.ToInt32(row["Agosto"].ToString());
                num9 += Convert.ToInt32(row["Septiembre"].ToString());
                num10 += Convert.ToInt32(row["Octubre"].ToString());
                num11 += Convert.ToInt32(row["Noviembre"].ToString());
                num12 += Convert.ToInt32(row["Diciembre"].ToString());
            }
            Decimal num13 = (Decimal)(num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10 + num11 + num12);
            string str6 = str5 + "<tr>" + "<td align='right'><b>TOTALES</b></td>" + "<td>" + num1.ToString() + "</td>" + "<td>" + num2.ToString() + "</td>" + "<td>" + num3.ToString() + "</td>" + "<td>" + num4.ToString() + "</td>" + "<td>" + num5.ToString() + "</td>" + "<td>" + num6.ToString() + "</td>" + "<td>" + num7.ToString() + "</td>" + "<td>" + num8.ToString() + "</td>" + "<td>" + num9.ToString() + "</td>" + "<td>" + num10.ToString() + "</td>" + "<td>" + num11.ToString() + "</td>" + "<td>" + num12.ToString() + "</td>" + "<td>" + num13.ToString() + "</td>" + "</tr>";
            this.dtExcel.Clear();
            this.dtExcel.Rows.Add((object)"TOTALES", (object)num1.ToString(), (object)num2.ToString(), (object)num3.ToString(), (object)num4.ToString(), (object)num5.ToString(), (object)num6.ToString(), (object)num7.ToString(), (object)num8.ToString(), (object)num9.ToString(), (object)num10.ToString(), (object)num11.ToString(), (object)num12.ToString(), (object)num13.ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this.dtExcel.Rows.Add((object)row["Problema"].ToString(), (object)row["Enero"].ToString(), (object)row["Febrero"].ToString(), (object)row["Marzo"].ToString(), (object)row["Abril"].ToString(), (object)row["Mayo"].ToString(), (object)row["Junio"].ToString(), (object)row["Julio"].ToString(), (object)row["Agosto"].ToString(), (object)row["Septiembre"].ToString(), (object)row["Octubre"].ToString(), (object)row["Noviembre"].ToString(), (object)row["Diciembre"].ToString(), (object)row["Total"].ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                str6 += "<tr>";
                str6 = str6 + "<td>" + row["Problema"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Enero"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Febrero"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Marzo"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Abril"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Mayo"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Junio"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Julio"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Agosto"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Septiembre"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Octubre"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Noviembre"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Diciembre"].ToString() + "</td>";
                str6 = str6 + "<td>" + row["Total"].ToString() + "</td>";
                str6 += "</tr>";
            }
            this.datosqueja.InnerHtml = str6 + "</tbody>" + "</table>";
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
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Clave", typeof(string));
            dataTable.Columns.Add("Problema", typeof(string));
            dataTable.Columns.Add("Enero", typeof(string));
            dataTable.Columns.Add("Febrero", typeof(string));
            dataTable.Columns.Add("Marzo", typeof(string));
            dataTable.Columns.Add("Abril", typeof(string));
            dataTable.Columns.Add("Mayo", typeof(string));
            dataTable.Columns.Add("Junio", typeof(string));
            dataTable.Columns.Add("Julio", typeof(string));
            dataTable.Columns.Add("Agosto", typeof(string));
            dataTable.Columns.Add("Septiembre", typeof(string));
            dataTable.Columns.Add("Octubre", typeof(string));
            dataTable.Columns.Add("Noviembre", typeof(string));
            dataTable.Columns.Add("Diciembre", typeof(string));
            dataTable.Columns.Add("Total", typeof(string));
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
                    row["Enero"] = (object)"0";
                    row["Febrero"] = (object)"0";
                    row["Marzo"] = (object)"0";
                    row["Abril"] = (object)"0";
                    row["Mayo"] = (object)"0";
                    row["Junio"] = (object)"0";
                    row["Julio"] = (object)"0";
                    row["Agosto"] = (object)"0";
                    row["Septiembre"] = (object)"0";
                    row["Octubre"] = (object)"0";
                    row["Noviembre"] = (object)"0";
                    row["Diciembre"] = (object)"0";
                    row["Total"] = (object)"0";
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
            command2.CommandText = "select a.que_folio, a.que_fechaemb AS que_fecha, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.que_status in ('A', 'T') order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(4).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        int num = Convert.ToDateTime(sqlDataReader2.GetValue(1).ToString().Trim()).Month;
                        string str4 = num.ToString();
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Enero"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Enero"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Febrero"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Febrero"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Marzo"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Marzo"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Abril"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Abril"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Mayo"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Mayo"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Junio"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Junio"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Julio"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Julio"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Agosto"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Agosto"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Septiembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Septiembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Octubre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Octubre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Noviembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Noviembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Diciembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Diciembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
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
            string str7 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>" + "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Enero</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Febrero</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Marzo</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Abril</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Mayo</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Junio</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Julio</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Agosto</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Septiembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Octubre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Noviembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Diciembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Total</font></th>" + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row["Enero"].ToString());
                num2 += Convert.ToInt32(row["Febrero"].ToString());
                num3 += Convert.ToInt32(row["Marzo"].ToString());
                num4 += Convert.ToInt32(row["Abril"].ToString());
                num5 += Convert.ToInt32(row["Mayo"].ToString());
                num6 += Convert.ToInt32(row["Junio"].ToString());
                num7 += Convert.ToInt32(row["Julio"].ToString());
                num8 += Convert.ToInt32(row["Agosto"].ToString());
                num9 += Convert.ToInt32(row["Septiembre"].ToString());
                num10 += Convert.ToInt32(row["Octubre"].ToString());
                num11 += Convert.ToInt32(row["Noviembre"].ToString());
                num12 += Convert.ToInt32(row["Diciembre"].ToString());
            }
            Decimal num13 = (Decimal)(num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10 + num11 + num12);
            string str8 = str7 + "<tr>" + "<td align='right'><b>TOTALES</b></td>" + "<td>" + (object)num1 + "</td>" + "<td>" + (object)num2 + "</td>" + "<td>" + (object)num3 + "</td>" + "<td>" + (object)num4 + "</td>" + "<td>" + (object)num5 + "</td>" + "<td>" + (object)num6 + "</td>" + "<td>" + (object)num7 + "</td>" + "<td>" + (object)num8 + "</td>" + "<td>" + (object)num9 + "</td>" + "<td>" + (object)num10 + "</td>" + "<td>" + (object)num11 + "</td>" + "<td>" + (object)num12 + "</td>" + "<td>" + (object)num13 + "</td>" + "</tr>";
            this.dtExcel.Clear();
            this.dtExcel.Rows.Add((object)"TOTALES", (object)num1.ToString(), (object)num2.ToString(), (object)num3.ToString(), (object)num4.ToString(), (object)num5.ToString(), (object)num6.ToString(), (object)num7.ToString(), (object)num8.ToString(), (object)num9.ToString(), (object)num10.ToString(), (object)num11.ToString(), (object)num12.ToString(), (object)num13.ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this.dtExcel.Rows.Add((object)row["Problema"].ToString(), (object)row["Enero"].ToString(), (object)row["Febrero"].ToString(), (object)row["Marzo"].ToString(), (object)row["Abril"].ToString(), (object)row["Mayo"].ToString(), (object)row["Junio"].ToString(), (object)row["Julio"].ToString(), (object)row["Agosto"].ToString(), (object)row["Septiembre"].ToString(), (object)row["Octubre"].ToString(), (object)row["Noviembre"].ToString(), (object)row["Diciembre"].ToString(), (object)row["Total"].ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                str8 += "<tr>";
                str8 = str8 + "<td>" + row["Problema"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Enero"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Febrero"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Marzo"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Abril"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Mayo"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Junio"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Julio"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Agosto"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Septiembre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Octubre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Noviembre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Diciembre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Total"].ToString() + "</td>";
                str8 += "</tr>";
            }
            this.datosqueja.InnerHtml = str8 + "</tbody>" + "</table>";
        }

        protected void btnCedis_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Clave", typeof(string));
            dataTable.Columns.Add("Problema", typeof(string));
            dataTable.Columns.Add("Enero", typeof(string));
            dataTable.Columns.Add("Febrero", typeof(string));
            dataTable.Columns.Add("Marzo", typeof(string));
            dataTable.Columns.Add("Abril", typeof(string));
            dataTable.Columns.Add("Mayo", typeof(string));
            dataTable.Columns.Add("Junio", typeof(string));
            dataTable.Columns.Add("Julio", typeof(string));
            dataTable.Columns.Add("Agosto", typeof(string));
            dataTable.Columns.Add("Septiembre", typeof(string));
            dataTable.Columns.Add("Octubre", typeof(string));
            dataTable.Columns.Add("Noviembre", typeof(string));
            dataTable.Columns.Add("Diciembre", typeof(string));
            dataTable.Columns.Add("Total", typeof(string));
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
                    row["Enero"] = (object)"0";
                    row["Febrero"] = (object)"0";
                    row["Marzo"] = (object)"0";
                    row["Abril"] = (object)"0";
                    row["Mayo"] = (object)"0";
                    row["Junio"] = (object)"0";
                    row["Julio"] = (object)"0";
                    row["Agosto"] = (object)"0";
                    row["Septiembre"] = (object)"0";
                    row["Octubre"] = (object)"0";
                    row["Noviembre"] = (object)"0";
                    row["Diciembre"] = (object)"0";
                    row["Total"] = (object)"0";
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
            command2.CommandText = "select a.que_folio, a.que_fechaemb AS que_fecha, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.cedis = '" + this.lblCedis.Text + "' and a.que_status in ('A', 'T') order by a.que_fechaemb";
            if (this.lblCedis.Text == "AGUILARES")
                command2.CommandText = "select a.que_folio, a.que_fechaemb AS que_fecha, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c, tb_cat_areas_queja d WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.cedis = 'COMERCIALIZADORA GAB' and a.que_status in ('A', 'T') AND a.area_queja = d.id_area and d.area_nombre = 'EMPAQUE_AGUILARES' order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(4).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        int num = Convert.ToDateTime(sqlDataReader2.GetValue(1).ToString().Trim()).Month;
                        string str4 = num.ToString();
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Enero"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Enero"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Febrero"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Febrero"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Marzo"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Marzo"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Abril"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Abril"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Mayo"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Mayo"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Junio"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Junio"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Julio"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Julio"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Agosto"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Agosto"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Septiembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Septiembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Octubre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Octubre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Noviembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Noviembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Diciembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Diciembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
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
            string str7 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>" + "<th bgcolor='#337ab7'><font color='#fff'>Problema</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Enero</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Febrero</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Marzo</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Abril</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Mayo</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Junio</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Julio</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Agosto</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Septiembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Octubre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Noviembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Diciembre</font></th>" + "<th bgcolor='#337ab7'><font color='#fff'>Total</font></th>" + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row["Enero"].ToString());
                num2 += Convert.ToInt32(row["Febrero"].ToString());
                num3 += Convert.ToInt32(row["Marzo"].ToString());
                num4 += Convert.ToInt32(row["Abril"].ToString());
                num5 += Convert.ToInt32(row["Mayo"].ToString());
                num6 += Convert.ToInt32(row["Junio"].ToString());
                num7 += Convert.ToInt32(row["Julio"].ToString());
                num8 += Convert.ToInt32(row["Agosto"].ToString());
                num9 += Convert.ToInt32(row["Septiembre"].ToString());
                num10 += Convert.ToInt32(row["Octubre"].ToString());
                num11 += Convert.ToInt32(row["Noviembre"].ToString());
                num12 += Convert.ToInt32(row["Diciembre"].ToString());
            }
            Decimal num13 = (Decimal)(num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10 + num11 + num12);
            string str8 = str7 + "<tr>" + "<td align='right'><b>TOTALES</b></td>" + "<td>" + (object)num1 + "</td>" + "<td>" + (object)num2 + "</td>" + "<td>" + (object)num3 + "</td>" + "<td>" + (object)num4 + "</td>" + "<td>" + (object)num5 + "</td>" + "<td>" + (object)num6 + "</td>" + "<td>" + (object)num7 + "</td>" + "<td>" + (object)num8 + "</td>" + "<td>" + (object)num9 + "</td>" + "<td>" + (object)num10 + "</td>" + "<td>" + (object)num11 + "</td>" + "<td>" + (object)num12 + "</td>" + "<td>" + (object)num13 + "</td>" + "</tr>";
            this.dtExcel.Clear();
            this.dtExcel.Rows.Add((object)"TOTALES", (object)num1.ToString(), (object)num2.ToString(), (object)num3.ToString(), (object)num4.ToString(), (object)num5.ToString(), (object)num6.ToString(), (object)num7.ToString(), (object)num8.ToString(), (object)num9.ToString(), (object)num10.ToString(), (object)num11.ToString(), (object)num12.ToString(), (object)num13.ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this.dtExcel.Rows.Add((object)row["Problema"].ToString(), (object)row["Enero"].ToString(), (object)row["Febrero"].ToString(), (object)row["Marzo"].ToString(), (object)row["Abril"].ToString(), (object)row["Mayo"].ToString(), (object)row["Junio"].ToString(), (object)row["Julio"].ToString(), (object)row["Agosto"].ToString(), (object)row["Septiembre"].ToString(), (object)row["Octubre"].ToString(), (object)row["Noviembre"].ToString(), (object)row["Diciembre"].ToString(), (object)row["Total"].ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                str8 += "<tr>";
                str8 = str8 + "<td>" + row["Problema"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Enero"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Febrero"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Marzo"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Abril"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Mayo"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Junio"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Julio"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Agosto"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Septiembre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Octubre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Noviembre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Diciembre"].ToString() + "</td>";
                str8 = str8 + "<td>" + row["Total"].ToString() + "</td>";
                str8 += "</tr>";
            }
            this.datosqueja.InnerHtml = str8 + "</tbody>" + "</table>"; 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=CajasMes.xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            response.ContentEncoding = Encoding.Unicode;
            response.BinaryWrite(Encoding.Unicode.GetPreamble());
            foreach (DataRow row in (InternalDataCollectionBase)this.dtExcel.Rows)
            {
                if (row["Enero"].ToString() == "0")
                    row["Enero"] = (object)"";
                if (row["Febrero"].ToString() == "0")
                    row["Febrero"] = (object)"";
                if (row["Marzo"].ToString() == "0")
                    row["Marzo"] = (object)"";
                if (row["Abril"].ToString() == "0")
                    row["Abril"] = (object)"";
                if (row["Mayo"].ToString() == "0")
                    row["Mayo"] = (object)"";
                if (row["Junio"].ToString() == "0")
                    row["Junio"] = (object)"";
                if (row["Julio"].ToString() == "0")
                    row["Julio"] = (object)"";
                if (row["Agosto"].ToString() == "0")
                    row["Agosto"] = (object)"";
                if (row["Septiembre"].ToString() == "0")
                    row["Septiembre"] = (object)"";
                if (row["Octubre"].ToString() == "0")
                    row["Octubre"] = (object)"";
                if (row["Noviembre"].ToString() == "0")
                    row["Noviembre"] = (object)"";
                if (row["Diciembre"].ToString() == "0")
                    row["Diciembre"] = (object)"";
                if (row["Total"].ToString() == "0")
                    row["Total"] = (object)"";
            }
            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                {
                    DataGrid dataGrid = new DataGrid();
                    dataGrid.DataSource = (object)this.dtExcel;
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
            dataTable.Columns.Add("Enero", typeof(string));
            dataTable.Columns.Add("Febrero", typeof(string));
            dataTable.Columns.Add("Marzo", typeof(string));
            dataTable.Columns.Add("Abril", typeof(string));
            dataTable.Columns.Add("Mayo", typeof(string));
            dataTable.Columns.Add("Junio", typeof(string));
            dataTable.Columns.Add("Julio", typeof(string));
            dataTable.Columns.Add("Agosto", typeof(string));
            dataTable.Columns.Add("Septiembre", typeof(string));
            dataTable.Columns.Add("Octubre", typeof(string));
            dataTable.Columns.Add("Noviembre", typeof(string));
            dataTable.Columns.Add("Diciembre", typeof(string));
            dataTable.Columns.Add("Total", typeof(string));
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
                    row["Enero"] = (object)"0";
                    row["Febrero"] = (object)"0";
                    row["Marzo"] = (object)"0";
                    row["Abril"] = (object)"0";
                    row["Mayo"] = (object)"0";
                    row["Junio"] = (object)"0";
                    row["Julio"] = (object)"0";
                    row["Agosto"] = (object)"0";
                    row["Septiembre"] = (object)"0";
                    row["Octubre"] = (object)"0";
                    row["Noviembre"] = (object)"0";
                    row["Diciembre"] = (object)"0";
                    row["Total"] = (object)"0";
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
            command2.CommandText = "select a.que_folio, a.que_fechaemb AS que_fecha, b.qud_problema, c.pro_nombre, b.qud_cantreci from tb_mstr_quejas a, tb_det_quejas b, tb_cat_problemas c WHERE a.que_folio = b.que_folio and b.qud_problema = c.pro_clave and a.que_fechaemb >= '" + str2 + "' and a.que_fechaemb <= '" + str3 + "' and a.que_status in ('A', 'T') order by a.que_fechaemb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    int int32 = Convert.ToInt32(sqlDataReader2.GetValue(4).ToString().Trim());
                    foreach (DataRow dataRow1 in dataTable.Select("Clave = '" + sqlDataReader2.GetValue(2).ToString().Trim() + "'"))
                    {
                        int num = Convert.ToDateTime(sqlDataReader2.GetValue(1).ToString().Trim()).Month;
                        string str4 = num.ToString();
                        if (str4 == "1")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Enero"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Enero"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "2")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Febrero"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Febrero"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "3")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Marzo"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Marzo"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "4")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Abril"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Abril"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "5")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Mayo"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Mayo"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "6")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Junio"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Junio"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "7")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Julio"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Julio"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "8")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Agosto"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Agosto"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "9")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Septiembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Septiembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "10")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Octubre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Octubre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "11")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Noviembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Noviembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
                        }
                        if (str4 == "12")
                        {
                            DataRow dataRow2 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Diciembre"].ToString()) + int32;
                            string str5 = num.ToString();
                            dataRow2["Diciembre"] = (object)str5;
                            DataRow dataRow3 = dataRow1;
                            num = Convert.ToInt32(dataRow1["Total"].ToString()) + int32;
                            string str6 = num.ToString();
                            dataRow3["Total"] = (object)str6;
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
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                num1 += Convert.ToInt32(row["Enero"].ToString());
                num2 += Convert.ToInt32(row["Febrero"].ToString());
                num3 += Convert.ToInt32(row["Marzo"].ToString());
                num4 += Convert.ToInt32(row["Abril"].ToString());
                num5 += Convert.ToInt32(row["Mayo"].ToString());
                num6 += Convert.ToInt32(row["Junio"].ToString());
                num7 += Convert.ToInt32(row["Julio"].ToString());
                num8 += Convert.ToInt32(row["Agosto"].ToString());
                num9 += Convert.ToInt32(row["Septiembre"].ToString());
                num10 += Convert.ToInt32(row["Octubre"].ToString());
                num11 += Convert.ToInt32(row["Noviembre"].ToString());
                num12 += Convert.ToInt32(row["Diciembre"].ToString());
            }
            Decimal num13 = (Decimal)(num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10 + num11 + num12);
            this.dtCedis.Clear();
            this.dtCedis.Rows.Add((object)"TOTALES", (object)num1.ToString(), (object)num2.ToString(), (object)num3.ToString(), (object)num4.ToString(), (object)num5.ToString(), (object)num6.ToString(), (object)num7.ToString(), (object)num8.ToString(), (object)num9.ToString(), (object)num10.ToString(), (object)num11.ToString(), (object)num12.ToString(), (object)num13.ToString());
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this.dtCedis.Rows.Add((object)row["Problema"].ToString(), (object)row["Enero"].ToString(), (object)row["Febrero"].ToString(), (object)row["Marzo"].ToString(), (object)row["Abril"].ToString(), (object)row["Mayo"].ToString(), (object)row["Junio"].ToString(), (object)row["Julio"].ToString(), (object)row["Agosto"].ToString(), (object)row["Septiembre"].ToString(), (object)row["Octubre"].ToString(), (object)row["Noviembre"].ToString(), (object)row["Diciembre"].ToString(), (object)row["Total"].ToString());
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=CajasMes.xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            response.ContentEncoding = Encoding.Unicode;
            response.BinaryWrite(Encoding.Unicode.GetPreamble());
            foreach (DataRow row in (InternalDataCollectionBase)this.dtCedis.Rows)
            {
                if (row["Enero"].ToString() == "0")
                    row["Enero"] = (object)"";
                if (row["Febrero"].ToString() == "0")
                    row["Febrero"] = (object)"";
                if (row["Marzo"].ToString() == "0")
                    row["Marzo"] = (object)"";
                if (row["Abril"].ToString() == "0")
                    row["Abril"] = (object)"";
                if (row["Mayo"].ToString() == "0")
                    row["Mayo"] = (object)"";
                if (row["Junio"].ToString() == "0")
                    row["Junio"] = (object)"";
                if (row["Julio"].ToString() == "0")
                    row["Julio"] = (object)"";
                if (row["Agosto"].ToString() == "0")
                    row["Agosto"] = (object)"";
                if (row["Septiembre"].ToString() == "0")
                    row["Septiembre"] = (object)"";
                if (row["Octubre"].ToString() == "0")
                    row["Octubre"] = (object)"";
                if (row["Noviembre"].ToString() == "0")
                    row["Noviembre"] = (object)"";
                if (row["Diciembre"].ToString() == "0")
                    row["Diciembre"] = (object)"";
                if (row["Total"].ToString() == "0")
                    row["Total"] = (object)"";
            }
            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter))
                {
                    DataGrid dataGrid = new DataGrid();
                    dataGrid.DataSource = (object)this.dtCedis;
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