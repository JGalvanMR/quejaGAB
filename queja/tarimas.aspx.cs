using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class tarimas : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtOrdenes = new DataTable();
        private string lote = "";
        private string prov = "";
        private string nomp = "";
        private string rcho = "";
        private string nomr = "";
        private string tbla = "";
        private string nomt = "";
        private string cadu = "";
        private string tari = "";
        private string nomRecibe = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            if (!this.Page.IsPostBack)
            {
                this.dtOrdenes = this.con.buscarregistros_tarima(this.lblQueja.Text);
                this.gvwOrden.DataSource = (object)this.dtOrdenes;
                this.gvwOrden.DataBind();
            }
        }

        protected void gvwOrden_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "muestradatos"))
                return;
            GridViewRow row = this.gvwOrden.Rows[Convert.ToInt32(e.CommandArgument)];
            this.lote = this.Server.HtmlDecode(row.Cells[13].Text);
            this.prov = this.Server.HtmlDecode(row.Cells[4].Text);
            this.nomp = this.Server.HtmlDecode(row.Cells[5].Text);
            this.rcho = this.Server.HtmlDecode(row.Cells[6].Text);
            this.nomr = this.Server.HtmlDecode(row.Cells[7].Text);
            this.tbla = this.Server.HtmlDecode(row.Cells[8].Text);
            this.nomt = this.Server.HtmlDecode(row.Cells[9].Text);
            this.cadu = this.Server.HtmlDecode(row.Cells[10].Text);
            this.tari = this.Server.HtmlDecode(row.Cells[12].Text);
            this.txtTarima.Text = this.tari;
            this.upnTarima.Update();
            this.txtProveedor.Text = this.prov;
            this.txtRancho.Text = this.rcho;
            this.txtTabla.Text = this.tbla;
            this.txtLote.Text = this.lote;
            this.txtCaducidad.Text = this.cadu;
            this.upnDatosActualizar.Update();
        }

        protected void btnBuscaOrden_Click(object sender, EventArgs e)
        {
            this.prov = this.txtProveedor.Text;
            this.rcho = this.txtRancho.Text;
            this.tbla = this.txtTabla.Text;
            this.lote = this.txtLote.Text;
            this.cadu = this.txtCaducidad.Text;
            this.tari = this.txtTarima.Text;
            this.nomRecibe = this.lblNombre.Text;
            if (this.txtTarima.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Debe seleccionar una tarima del listado');", true);
            }
            else
            {
                if (!(this.con.actualizar_tarima_principal(this.lblQueja.Text, this.prov, this.rcho, this.tbla, this.lote, this.cadu, this.tari, this.nomRecibe) == "1"))
                    return;
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Tarima Asignada');", true);
            }
        }
    }
}