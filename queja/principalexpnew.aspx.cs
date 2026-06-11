using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace queja
{
    public partial class principalexpnew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblClave.Text = this.Session["clave"].ToString();
            lblNombre.Text = this.Session["nombre"].ToString();
            lblCedis.Text = this.Session["cedis"].ToString();
            lblAdmin.Text = this.Session["admin"].ToString();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Session["clave"] = (object)lblClave.Text;
            Session["nombre"] = (object)lblNombre.Text;
            Session["cedis"] = (object)lblCedis.Text;
            Session["admin"] = (object)lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
        }
    }
}