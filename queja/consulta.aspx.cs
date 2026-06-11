using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class consulta : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtMstrQueja = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        private DataTable dtDetQuejaPed = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.RegisterRedirectOnSessionEndScript();
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            string valor = this.Session["queja"].ToString();
            this.dtMstrQueja = this.con.consultaqueja(valor);
            this.txtFolio.Text = this.dtMstrQueja.Rows[0]["que_folio"].ToString();
            this.txtSemana.Text = this.dtMstrQueja.Rows[0]["que_semana"].ToString();
            this.txtMes.Text = this.dtMstrQueja.Rows[0]["que_mes"].ToString();
            this.txtFecha.Text = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
            this.txtCliente.Text = this.dtMstrQueja.Rows[0]["que_cliente"].ToString();
            this.txtSucursal.Text = this.dtMstrQueja.Rows[0]["que_sucursal"].ToString();
            this.txtReporto.Text = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
            this.txtRecibio.Text = this.dtMstrQueja.Rows[0]["recibio"].ToString();
            this.txtTipo.Text = this.dtMstrQueja.Rows[0]["recibio"].ToString() == "N" ? "NACIONAL" : "EXPORTACION";
            this.dtDetQueja = this.con.consultadetqueja(valor);
            this.txtOrdenProd.Text = this.dtDetQueja.Rows[0]["qud_ordprod"].ToString();
            this.txt_clave.Text = this.dtDetQueja.Rows[0]["id_producto"].ToString();
            this.txt_nom.Text = this.dtDetQueja.Rows[0]["nom_producto"].ToString();
            this.txt_lote.Text = this.dtDetQueja.Rows[0]["id_producto"].ToString();
            this.txt_fechacad.Text = this.dtDetQueja.Rows[0]["fecha_cad"].ToString();
            this.txt_cveprov.Text = this.dtDetQueja.Rows[0]["prov_clave"].ToString();
            this.txt_nomprov.Text = this.dtDetQueja.Rows[0]["prov_nombre"].ToString();
            this.txt_cverch.Text = this.dtDetQueja.Rows[0]["rch_clave"].ToString();
            this.txt_nomrch.Text = this.dtDetQueja.Rows[0]["rch_nombre"].ToString();
            this.txt_cvetbl.Text = this.dtDetQueja.Rows[0]["tbl_clave"].ToString();
            this.txt_nomtbl.Text = this.dtDetQueja.Rows[0]["tbl_nombre"].ToString();
            this.txtRespLinea.Text = this.dtDetQueja.Rows[0]["qud_responsable"].ToString();
            this.txtArea.Text = this.dtDetQueja.Rows[0]["qud_area"].ToString();
            this.txtCantRech.Text = this.dtDetQueja.Rows[0]["qud_cantrecha"].ToString();
            this.txtCantReci.Text = this.dtDetQueja.Rows[0]["qud_cantreci"].ToString();
            this.txtUnidad.Text = this.dtDetQueja.Rows[0]["qud_unidad"].ToString();
            this.chkDevolucion.Checked = this.dtDetQueja.Rows[0]["qud_devolucion"].ToString() == "1";
            this.txtMoneda.Text = this.dtDetQueja.Rows[0]["qud_moneda"].ToString();
            this.txtProblema.Text = this.dtDetQueja.Rows[0]["problema"].ToString();
            this.dtDetQuejaPed = this.con.consulta_productos_exp(this.lblQueja.Text);
            this.grvDetalle.DataSource = (object)this.dtDetQuejaPed;
            this.grvDetalle.DataBind();
            string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"));
            List<ListItem> listItemList = new List<ListItem>();
            foreach (string path in files)
            {
                if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".pdf"))
                    listItemList.Add(new ListItem(Path.GetFileName(path), path));
            }
            int num = 1;
            foreach (ListItem listItem in listItemList)
            {
                if (num == 1)
                    this.file1.Text = listItem.ToString();
                if (num == 2)
                    this.file2.Text = listItem.ToString();
                if (num == 3)
                    this.file3.Text = listItem.ToString();
                ++num;
            }
            string str = "fotos_ant/1_" + valor + ".jpg";
            this.img1.ImageUrl = str;
            this.img2.ImageUrl = "fotos_ant/2_" + valor + ".jpg";
            this.img3.ImageUrl = "fotos_ant/3_" + valor + ".jpg";
            this.Image1.ImageUrl = str;
            this.Image2.ImageUrl = "fotos_ant/2_" + valor + ".jpg";
            this.Image3.ImageUrl = "fotos_ant/3_" + valor + ".jpg";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = this.Session["cliente"];
            this.Response.Redirect("contenido.aspx");
        }

        protected void file1_Click(object sender, EventArgs e)
        {
            if (!(this.file1.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_ant/" + this.file1.Text + "','_blank')</script>");
        }

        protected void file2_Click(object sender, EventArgs e)
        {
            if (!(this.file2.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_ant/" + this.file2.Text + "','_blank')</script>");
        }

        protected void file3_Click(object sender, EventArgs e)
        {
            if (!(this.file3.Text != "File"))
                return;
            this.Response.Write("<script>window.open('fotos_ant/" + this.file3.Text + "','_blank')</script>");
        }
    }
}