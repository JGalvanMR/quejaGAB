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
    public partial class areas_sem : System.Web.UI.Page
    {
        private DataTable dtQuejas = new DataTable();
        private DataTable dtExcel2 = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            dataTable.Rows.Add((object)"TOTALES");
            for (int index = 1; index < dataTable.Columns.Count; ++index)
                dataTable.Rows[0][index] = (object)"0";
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            sqlConnection.Open();
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B  WHERE A.area_queja = B.id_area AND a.cedis = '" + this.Session["cedis"].ToString() + "' AND A.que_status in ('A','T') group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B  WHERE A.area_queja = B.id_area AND a.cedis = 'COMERCIALIZADORA GAB' AND A.que_status in ('A','T') AND B.area_nombre = 'EMPAQUE_AGUILARES' group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)sqlDataReader2.GetValue(1).ToString().Trim();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            Decimal num2 = new Decimal(0);
            for (int index1 = 1; index1 < dataTable.Columns.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Rows.Count; ++index2)
                    num2 += Convert.ToDecimal(dataTable.Rows[index2][index1].ToString());
                dataTable.Rows[0][index1] = (object)num2.ToString();
                num2 = new Decimal(0);
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num3 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num3 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num3.ToString();
            }
            for (int index1 = 1; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "0")
                        dataTable.Rows[index1][index2] = (object)"";
                }
            }
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>";
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                str1 = str1 + "<th bgcolor='#337ab7'><font color='#fff'>" + column.ColumnName + "</font></th>";
            string str2 = str1 + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                int count = dataTable.Columns.Count;
                str2 = !(row["semana"].ToString() == "0") ? str2 + "<tr><td><b>" + row["semana"].ToString() + "</b></td>" : str2 + "<tr><td><b>Totales</b></td>";
                for (int index = 1; index < count; ++index)
                    str2 = str2 + "<td>" + row[index].ToString() + "</td>";
                str2 += "</tr>";
            }
            this.datosqueja.InnerHtml = str2 + "</tbody>" + "</table>";
            this.carga_totales();
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
            dataTable.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            dataTable.Rows.Add((object)"TOTALES");
            for (int index = 1; index < dataTable.Columns.Count; ++index)
                dataTable.Rows[0][index] = (object)"0";
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            sqlConnection.Open();
            command.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE A.area_queja = B.id_area AND A.que_status in ('A','T') group by A.que_sememb, B.area_nombre ORDER BY B.area_nombre, A.que_sememb";
            SqlDataReader sqlDataReader2 = command.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)sqlDataReader2.GetValue(1).ToString().Trim();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command.Dispose();
            sqlConnection.Close();
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            Decimal num2 = new Decimal(0);
            for (int index1 = 1; index1 < dataTable.Columns.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Rows.Count; ++index2)
                    num2 += Convert.ToDecimal(dataTable.Rows[index2][index1].ToString());
                dataTable.Rows[0][index1] = (object)num2.ToString();
                num2 = new Decimal(0);
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num3 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num3 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num3.ToString();
            }
            for (int index1 = 1; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "0")
                        dataTable.Rows[index1][index2] = (object)"";
                }
            }
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>";
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                str1 = str1 + "<th bgcolor='#337ab7'><font color='#fff'>" + column.ColumnName + "</font></th>";
            string str2 = str1 + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                int count = dataTable.Columns.Count;
                str2 = !(row["semana"].ToString() == "0") ? str2 + "<tr><td><b>" + row["semana"].ToString() + "</b></td>" : str2 + "<tr><td><b>Totales</b></td>";
                for (int index = 1; index < count; ++index)
                    str2 = str2 + "<td>" + row[index].ToString() + "</td>";
                str2 += "</tr>";
            }
            string str3 = str2 + "</tbody>" + "</table>";
            this.datosqueja.InnerHtml = str3;
            this.datosqueja1.InnerHtml = str3;
        }

        protected void btnCedis_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            dataTable.Rows.Add((object)"TOTALES");
            for (int index = 1; index < dataTable.Columns.Count; ++index)
                dataTable.Rows[0][index] = (object)"0";
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            sqlConnection.Open();
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B  WHERE A.area_queja = B.id_area AND A.cedis = '" + this.Session["cedis"].ToString() + "' AND A.que_status in ('A','T') group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)sqlDataReader2.GetValue(1).ToString().Trim();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            Decimal num2 = new Decimal(0);
            for (int index1 = 1; index1 < dataTable.Columns.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Rows.Count; ++index2)
                    num2 += Convert.ToDecimal(dataTable.Rows[index2][index1].ToString());
                dataTable.Rows[0][index1] = (object)num2.ToString();
                num2 = new Decimal(0);
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num3 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num3 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num3.ToString();
            }
            for (int index1 = 1; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "0")
                        dataTable.Rows[index1][index2] = (object)"";
                }
            }
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>";
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                str1 = str1 + "<th bgcolor='#337ab7'><font color='#fff'>" + column.ColumnName + "</font></th>";
            string str2 = str1 + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                int count = dataTable.Columns.Count;
                str2 = !(row["semana"].ToString() == "0") ? str2 + "<tr><td><b>" + row["semana"].ToString() + "</b></td>" : str2 + "<tr><td><b>Totales</b></td>";
                for (int index = 1; index < count; ++index)
                    str2 = str2 + "<td>" + row[index].ToString() + "</td>";
                str2 += "</tr>";
            }
            this.datosqueja.InnerHtml = str2 + "</tbody>" + "</table>";
            this.carga_totales();
        }

        public void carga_totales()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            dataTable.Rows.Add((object)"TOTALES");
            for (int index = 1; index < dataTable.Columns.Count; ++index)
                dataTable.Rows[0][index] = (object)"0";
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            sqlConnection.Open();
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE cedis = '" + this.Session["cedis"].ToString() + "' AND A.area_queja = B.id_area AND A.que_status in ('A','T') group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE cedis = 'COMERCIALIZADORA GAB' AND A.area_queja = B.id_area AND A.que_status in ('A','T') AND B.area_nombre = 'EMPAQUE_AGUILARES' group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)sqlDataReader2.GetValue(1).ToString().Trim();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            Decimal num2 = new Decimal(0);
            for (int index1 = 1; index1 < dataTable.Columns.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Rows.Count; ++index2)
                    num2 += Convert.ToDecimal(dataTable.Rows[index2][index1].ToString());
                dataTable.Rows[0][index1] = (object)num2.ToString();
                num2 = new Decimal(0);
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num3 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num3 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num3.ToString();
            }
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "0")
                        dataTable.Rows[index1][index2] = (object)"";
                }
            }
            string str1 = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>";
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                str1 = str1 + "<th bgcolor='#337ab7'><font color='#fff'>" + column.ColumnName + "</font></th>";
            string str2 = str1 + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                int count = dataTable.Columns.Count;
                str2 = !(row["semana"].ToString() == "0") ? str2 + "<tr><td><b>" + row["semana"].ToString() + "</b></td>" : str2 + "<tr><td><b>Totales</b></td>";
                for (int index = 1; index < count; ++index)
                    str2 = str2 + "<td>" + row[index].ToString() + "</td>";
                str2 += "</tr>";
            }
            this.datosqueja1.InnerHtml = str2 + "</tbody>" + "</table>";
        }

        public void carga_totales_todos()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Semana", typeof(string));
            this.dtExcel2.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                {
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
                    this.dtExcel2.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
                }
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            this.dtExcel2.Columns.Add("TOTAL", typeof(string));
            int num1 = 0;
            for (int index = 0; index < 54; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            sqlConnection.Open();
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE A.area_queja = B.id_area AND A.que_status in ('A','T') group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)(Convert.ToDecimal(dataRow[sqlDataReader2.GetValue(2).ToString().Trim()]) + Convert.ToDecimal(sqlDataReader2.GetValue(1).ToString().Trim())).ToString();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num2 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num2 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num2.ToString();
            }
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
            {
                if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                {
                    Decimal num2 = new Decimal(0);
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                        num2 += Convert.ToDecimal(row[column.ColumnName].ToString());
                    dataTable.Rows[0][column.ColumnName] = (object)num2;
                }
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this.dtExcel2.Rows.Add(row.ItemArray);
            string str1 = "<table border='1' id='datatable1' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>";
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                str1 = str1 + "<th bgcolor='#337ab7'><font color='#fff'>" + column.ColumnName + "</font></th>";
            string str2 = str1 + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                int count = dataTable.Columns.Count;
                str2 = !(row["semana"].ToString() == "0") ? str2 + "<tr><td><b>Sem_" + row["semana"].ToString() + "</b></td>" : str2 + "<tr><td><b>Totales</b></td>";
                for (int index = 1; index < count; ++index)
                    str2 = str2 + "<td>" + row[index].ToString() + "</td>";
                str2 += "</tr>";
            }
            this.datosqueja1.InnerHtml = str2 + "</tbody>" + "</table>";
        }

        protected void btnExportarTodos_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            dataTable.Rows.Add((object)"TOTALES");
            for (int index = 1; index < dataTable.Columns.Count; ++index)
                dataTable.Rows[0][index] = (object)"0";
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            sqlConnection.Open();
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE A.area_queja = B.id_area AND A.que_status in ('A','T') group by A.que_sememb, B.area_nombre ORDER BY A.que_sememb";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)sqlDataReader2.GetValue(1).ToString().Trim();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            Decimal num2 = new Decimal(0);
            for (int index1 = 1; index1 < dataTable.Columns.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Rows.Count; ++index2)
                    num2 += Convert.ToDecimal(dataTable.Rows[index2][index1].ToString());
                dataTable.Rows[0][index1] = (object)num2.ToString();
                num2 = new Decimal(0);
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num3 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num3 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num3.ToString();
            }
            for (int index1 = 1; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "0")
                        dataTable.Rows[index1][index2] = (object)"";
                }
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=QuejasPorArea.xls");
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

        public void carga_valores_iniciales()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            int num1 = 0;
            for (int index = 0; index < 54; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            sqlConnection.Open();
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE cedis = '" + this.Session["cedis"].ToString() + "' AND A.area_queja = B.id_area AND A.que_status in ('A','T') group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE cedis = 'COMERCIALIZADORA GAB' AND A.area_queja = B.id_area AND A.que_status in ('A','T') AND B.area_nombre = 'EMPAQUE_AGUILARES' group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)sqlDataReader2.GetValue(1).ToString().Trim();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num2 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num2 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num2.ToString();
            }
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "0")
                        dataTable.Rows[index1][index2] = (object)"";
                }
            }
            string str1 = "<table border='1' id='datatable1' class='table table-striped table-bordered table-hover table-responsive'>" + "<thead><tr>";
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                str1 = str1 + "<th bgcolor='#337ab7'><font color='#fff'>" + column.ColumnName + "</font></th>";
            string str2 = str1 + "</tr></thead>" + "<tbody>";
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                int count = dataTable.Columns.Count;
                str2 = !(row["semana"].ToString() == "0") ? str2 + "<tr><td><b>Sem_" + row["semana"].ToString() + "</b></td>" : str2 + "<tr><td><b>Totales</b></td>";
                for (int index = 1; index < count; ++index)
                    str2 = str2 + "<td>" + row[index].ToString() + "</td>";
                str2 += "</tr>";
            }
            this.datosqueja.InnerHtml = str2 + "</tbody>" + "</table>";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$");
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Semana", typeof(string));
            sqlConnection.Open();
            SqlCommand command1 = sqlConnection.CreateCommand();
            command1.CommandText = "SELECT id_area, area_nombre FROM tb_cat_areas_queja ORDER BY area_nombre";
            SqlDataReader sqlDataReader1 = command1.ExecuteReader();
            if (sqlDataReader1.HasRows)
            {
                while (sqlDataReader1.Read())
                    dataTable.Columns.Add(sqlDataReader1.GetValue(1).ToString().Trim(), typeof(string));
            }
            sqlDataReader1.Close();
            sqlDataReader1.Dispose();
            command1.Dispose();
            sqlConnection.Close();
            dataTable.Columns.Add("TOTAL", typeof(string));
            dataTable.Rows.Add((object)"TOTALES");
            for (int index = 1; index < dataTable.Columns.Count; ++index)
                dataTable.Rows[0][index] = (object)"0";
            int num1 = 1;
            for (int index = 0; index < 53; ++index)
            {
                dataTable.Rows.Add((object)num1);
                ++num1;
            }
            sqlConnection.Open();
            SqlCommand command2 = sqlConnection.CreateCommand();
            command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) AS cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A LEFT JOIN tb_cat_areas_queja B ON A.area_queja = B.id_area WHERE cedis = '" + this.Session["cedis"].ToString() + "' AND A.que_status in ('A','T') group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            if (this.Session["cedis"].ToString() == "AGUILARES")
                command2.CommandText = "SELECT A.que_sememb AS que_semana, count(A.que_folio) AS cantidad_quejas, B.area_nombre FROM tb_mstr_quejas A, tb_cat_areas_queja B WHERE cedis = 'COMERCIALIZADORA GAB' AND A.area_queja = B.id_area AND A.que_status in ('A','T') AND B.area_nombre = 'EMPAQUE_AGUILARES' group by A.cedis, A.que_sememb, B.area_nombre ORDER BY A.que_sememb, A.cedis";
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    foreach (DataRow dataRow in dataTable.Select("semana = '" + sqlDataReader2.GetValue(0).ToString().Trim() + "'"))
                        dataRow[sqlDataReader2.GetValue(2).ToString().Trim()] = (object)sqlDataReader2.GetValue(1).ToString().Trim();
                }
            }
            sqlDataReader2.Close();
            sqlDataReader2.Dispose();
            command2.Dispose();
            sqlConnection.Close();
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "")
                        dataTable.Rows[index1][index2] = (object)"0";
                }
            }
            Decimal num2 = new Decimal(0);
            for (int index1 = 1; index1 < dataTable.Columns.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Rows.Count; ++index2)
                    num2 += Convert.ToDecimal(dataTable.Rows[index2][index1].ToString());
                dataTable.Rows[0][index1] = (object)num2.ToString();
                num2 = new Decimal(0);
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                Decimal num3 = new Decimal(0);
                foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (!(column.ColumnName == "Semana") && !(column.ColumnName == "TOTAL"))
                        num3 += Convert.ToDecimal(row[column.ColumnName].ToString());
                }
                row["TOTAL"] = (object)num3.ToString();
            }
            for (int index1 = 1; index1 < dataTable.Rows.Count; ++index1)
            {
                for (int index2 = 1; index2 < dataTable.Columns.Count; ++index2)
                {
                    if (dataTable.Rows[index1][index2].ToString() == "0")
                        dataTable.Rows[index1][index2] = (object)"";
                }
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.AddHeader("content-disposition", "attachment; filename=QuejasPorArea.xls");
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