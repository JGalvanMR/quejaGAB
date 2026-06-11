using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace queja
{
    public partial class notascredito : System.Web.UI.Page
    {
        private conectasql con = new conectasql();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();

            if (!IsPostBack)
            {
                carga_datos();
            }
        }

        public void carga_datos()
        {
            DataTable dt = con.carga_datos();
            grvDetalle.DataSource = dt;
            grvDetalle.DataBind();
        }
    }
}