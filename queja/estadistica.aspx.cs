using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace queja
{
    public partial class estadistica : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtProductos = new DataTable();
        private DataTable dtProblemas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();

            if (this.Page.IsPostBack)
                return;

            Int32 anio = DateTime.Now.Year;
            dtProductos = this.con.productos_graficas(anio);
            
            
            lstProductos.DataSource = dtProductos;
            lstProductos.DataTextField = "nom_producto";
            lstProductos.DataValueField = "id_producto";
            lstProductos.DataBind();
            udpProductos.Update();
            //cargarproblemas
            dtProblemas = this.con.cargarproblemas();
            DataRow row = dtProblemas.NewRow();
            row["pro_clave"] = "";
            row["pro_nombre"] = "SELECCIONA PROBLEMA...";
            dtProblemas.Rows.InsertAt(row, 0);
            ddlProblemas.DataSource = dtProblemas;
            ddlProblemas.DataTextField = "pro_nombre";
            ddlProblemas.DataValueField = "pro_clave";
            ddlProblemas.DataBind();
            udpProblemas.Update();

            
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            lstProductos.ClearSelection();
            lstProductos.DataBind();
            udpProductos.Update();

            lstMeses.ClearSelection();
            lstMeses.DataBind();
            udpMeses.Update();

            ddlProblemas.SelectedValue = "";
            udpProblemas.Update();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Int32 anio = DateTime.Now.Year;
            DataTable dtProductos = new DataTable();
            dtProductos.Columns.Add("clave", typeof(string));
            dtProductos.Columns.Add("producto", typeof(string));
            dtProductos.Columns.Add((anio - 1).ToString(), typeof(string));
            dtProductos.Columns.Add(anio.ToString(), typeof(string));

            string mes_inicial = "";
            string mes_final = "";

            DataTable dtMeses = new DataTable();
            dtMeses.Columns.Add("mes", typeof(string));
            foreach (ListItem valor in lstMeses.Items)
            {
                if (valor.Selected == true)
                {
                    dtMeses.Rows.Add(valor.Value.ToString());
                }
            }
            DataView dv = dtMeses.DefaultView;
            dv.Sort = "mes ASC";
            dtMeses = dv.Table;

            string prod_clave = "";
            foreach (ListItem valor in lstProductos.Items)
            {
                if (valor.Selected == true)
                {
                    DataTable dtRows = this.con.productos_comparativo(anio, valor.Value.ToString(), dtMeses, valor.Text, ddlProblemas.SelectedValue.ToString());
                    dtProductos.Rows.Add(dtRows.Rows[0].ItemArray);
                }
            }

            string cadena = "<table border='1' id='datatable' class='table table-striped table-bordered table-hover table-responsive'>";
            string cadena2 = "<table border='1' id='datatable2' class='table table-striped table-bordered table-hover table-responsive'>";
            cadena += "<thead><th bgcolor='#337ab7'><font color='#fff'>CLAVE</font></th><th bgcolor='#337ab7'><font color='#fff'>PRODUCTO</font></th><th bgcolor='#337ab7'><font color='#fff'>" + dtProductos.Columns[2].ColumnName.ToString() + "</font></th><th bgcolor='#337ab7'><font color='#fff'>" + dtProductos.Columns[3].ColumnName.ToString() + "</font></th></thead>";
            cadena += "<tbody>";
            cadena2 += "<thead><th bgcolor='#337ab7'><font color='#fff'></font></th><th bgcolor='#337ab7'><font color='#fff'>" + dtProductos.Columns[2].ColumnName.ToString() + "</font></th><th bgcolor='#337ab7'><font color='#fff'>" + dtProductos.Columns[3].ColumnName.ToString() + "</font></th></thead>";
            cadena2 += "<tbody>";
            foreach(DataRow ru in dtProductos.Rows)
            {
                cadena += "<tr><td>" + ru["clave"].ToString() + "</td><td>" + ru["producto"].ToString() + "</td><td>" + ru[dtProductos.Columns[2].ColumnName.ToString()].ToString() + "</td><td>" + ru[dtProductos.Columns[3].ColumnName.ToString()].ToString() + "</td></tr>";
                cadena2 += "<tr><td>" + ru["producto"].ToString() + "</td><td>" + ru[dtProductos.Columns[2].ColumnName.ToString()].ToString() + "</td><td>" + ru[dtProductos.Columns[3].ColumnName.ToString()].ToString() + "</td></tr>";
            }
            cadena += "</tbody></table>";
            cadena2 += "</tbody></table>";

            this.datosqueja.InnerHtml = cadena;

            this.datosqueja1.InnerHtml = cadena2;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("reportes.aspx");
        }
    }
}