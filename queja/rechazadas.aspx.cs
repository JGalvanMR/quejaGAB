using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace queja
{
    public partial class rechazadas : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtDetQueja = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            string str1 = this.Session["queja"].ToString();

            if (!Page.IsPostBack)
            {
                this.dtDetQueja = this.con.consulta_productos_exp_update_rechazo(str1);
                this.grvDetalle.DataSource = (object)this.dtDetQueja;
                this.grvDetalle.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string conse = "";
            string prod_clave = "";
            string orden_prod = "";
            string tarima_pro = "";
            string prov_clave = "";
            string rancho_cve = "";
            string tabla_clve = "";
            string rechazadas = "";
            string producidas = "";
            string porcentaje = "";
            string recibidas = "";
            //VALIDAR QUE NO HAYA RECHAZOS NUEVOS MAYORES AL RECIBIDO
            bool fnd = false; 
            foreach (GridViewRow row in grvDetalle.Rows)
            {
                recibidas = this.Server.HtmlDecode(row.Cells[10].Text);
                TextBox control = (TextBox)row.Cells[12].FindControl("txtRechazadas2");
                rechazadas = control.Text;

                if (Convert.ToDecimal(recibidas) < Convert.ToDecimal(rechazadas))
                {
                    fnd = true;
                    break;
                }
            }
            if (fnd == true)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Se detectaron rechazos mayor a lo recibido, favor de verificar');", true);
            }
            else
            {
                SqlConnection connection = new SqlConnection("Data Source=192.168.123.6,1433;Initial Catalog=GAB_Irapuato;Persist Security Info=True;User ID=sa;Password=Gabira2026$; Connect Timeout = 240");
                connection.Open();
                SqlCommand sqlCommand;
                foreach (GridViewRow row in grvDetalle.Rows)
                {
                    //GridViewRow row1 = this.grvDetalle.Rows[row.RowIndex];
                    conse = this.Server.HtmlDecode(row.Cells[0].Text);
                    prod_clave = this.Server.HtmlDecode(row.Cells[1].Text);
                    orden_prod = this.Server.HtmlDecode(row.Cells[3].Text);
                    tarima_pro = this.Server.HtmlDecode(row.Cells[4].Text);
                    prov_clave = this.Server.HtmlDecode(row.Cells[5].Text);
                    rancho_cve = this.Server.HtmlDecode(row.Cells[7].Text);
                    tabla_clve = this.Server.HtmlDecode(row.Cells[8].Text);
                    TextBox control = (TextBox)row.Cells[13].FindControl("txtRechazadas2");
                    rechazadas = control.Text;
                    producidas = this.Server.HtmlDecode(row.Cells[12].Text);

                    porcentaje = Math.Round(((Convert.ToDecimal(rechazadas) * Convert.ToDecimal("100")) / Convert.ToDecimal(producidas)), 2).ToString();

                    if (rechazadas != "0")
                    {
                        //string cadena = "UPDATE tb_det_quejas SET qud_cantrecha = '" + rechazadas + "', qud_porcen = '" + porcentaje + "' WHERE id_producto = '" + prod_clave + "' " +
                        //"AND qud_ordprod = '" + orden_prod + "' AND prov_clave = '" + prov_clave + "' AND rch_clave = '" + rancho_cve + "' AND tbl_clave = '" + tabla_clve + "' " +
                        //"AND qud_tarima = '" + tarima_pro + "' AND que_folio = '" + lblQueja.Text + "' AND conse = '" + conse + "'";
                        string cadena = "UPDATE tb_det_quejas SET qud_cantrecha = '" + rechazadas + "', qud_porcen = '" + porcentaje + "' WHERE id_producto = '" + prod_clave + "' " +
                        "AND que_folio = '" + lblQueja.Text + "' AND conse = '" + conse + "'";
                        sqlCommand = new SqlCommand(cadena, connection);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Dispose();
                    }
                    

                }
                connection.Close();

                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Modificacion Realizada');", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = this.Session["cliente"];
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("editar_queja_exp.aspx");
        }

    }
}