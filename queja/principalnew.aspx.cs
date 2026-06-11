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
    public partial class principalnew : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtProblemas = new DataTable();
        private DataTable dtOrdenes = new DataTable();
        private DataTable dtDatos = new DataTable();
        private DataTable dtClientes = new DataTable();
        private DataTable dtResponsables = new DataTable();
        private DataTable dtSucursales = new DataTable();
        private DataTable dtAreas = new DataTable();
        private string resp = "";
        private string area = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            //if (this.Page.IsPostBack)
            //    return;
            //this.dtClientes = this.con.cargarclientes();
            //this.ddlCliente.DataSource = (object)this.dtClientes;
            //this.ddlCliente.DataTextField = "CliRSocial";
            //this.ddlCliente.DataValueField = "CliCod";
            //this.ddlCliente.DataBind();
            //this.dtProblemas = this.con.cargarproblemas();
            //this.ddlProblema.DataSource = (object)this.dtProblemas;
            //this.ddlProblema.DataTextField = "pro_nombre";
            //this.ddlProblema.DataValueField = "pro_clave";
            //this.ddlProblema.DataBind();
            //this.txtFolio.Text = this.con.cargafolio().ToString();
            //TextBox txtSemana = this.txtSemana;
            //int num = this.con.cargasemana();
            //string str1 = num.ToString();
            //txtSemana.Text = str1;
            //TextBox txtMes = this.txtMes;
            //num = DateTime.Now.Month;
            //string str2 = num.ToString().PadLeft(2, '0');
            //txtMes.Text = str2;
            //this.txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //this.lblVerifica.Text = "0";
            //this.btnGuardar.Enabled = false;
            //this.upnGuardar.Update();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
        }
    }
}