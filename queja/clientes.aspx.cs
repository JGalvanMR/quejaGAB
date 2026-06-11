using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class clientes : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtClientes = new DataTable();
        private DataTable dtSucursales = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;
            this.dtClientes = this.con.cargarclientes();
            this.ddlCliente.DataSource = (object)this.dtClientes;
            this.ddlCliente.DataTextField = "CliRSocial";
            this.ddlCliente.DataValueField = "CliCod";
            this.ddlCliente.DataBind();
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dtSucursales.Clear();
            this.dtSucursales = this.con.cargarsucursales(this.ddlCliente.SelectedValue.ToString(), this.lblCedis.Text);
            if (this.dtSucursales.Rows.Count > 0)
                this.dtSucursales.Rows.RemoveAt(0);
            this.gvwOrden.DataSource = (object)this.dtSucursales;
            this.gvwOrden.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.txtSucursal.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe ingresar un nombre de Sucursal');", true);
            }
            else
            {
                string selectedValue = this.ddlCliente.SelectedValue;
                string upper = this.txtSucursal.Text.ToUpper();
                if (!(this.con.insertsucursal(selectedValue, upper) == "1"))
                    return;
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Sucursal Guardada');", true);
                this.dtSucursales.Clear();
                this.dtSucursales = this.con.cargarsucursales(selectedValue, this.lblCedis.Text);
                this.dtSucursales.Rows.RemoveAt(0);
                this.gvwOrden.DataSource = (object)this.dtSucursales;
                this.gvwOrden.DataBind();
            }
        }
    }
}