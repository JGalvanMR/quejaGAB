using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class reporte : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtMstrQueja = new DataTable();
        private DataTable dtDetQueja = new DataTable();
        private DataTable dtInvestigador = new DataTable();
        private DataTable dtAcciones = new DataTable();
        private DataTable dtVerificacion = new DataTable();
        private DataTable dtCausas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            string str1 = "";
            this.dtMstrQueja = this.con.consultaqueja(this.Session["queja"].ToString());
            string str2 = this.dtMstrQueja.Rows[0]["que_folio"].ToString();
            string shortDateString = Convert.ToDateTime(this.dtMstrQueja.Rows[0]["que_fecha"].ToString()).ToString("dd/MM/yyyy");
            string str3 = this.dtMstrQueja.Rows[0]["que_cliente"].ToString();
            string str4 = this.dtMstrQueja.Rows[0]["recibio"].ToString();
            string str5 = this.dtMstrQueja.Rows[0]["que_reporto"].ToString();
            string str6 = "sin dato";
            this.dtDetQueja = this.con.consultadetqueja(this.Session["queja"].ToString());
            string str7 = "<td>" + this.dtDetQueja.Rows[0]["nom_producto"].ToString() + "</td><td>" + this.dtDetQueja.Rows[0]["problema"].ToString() + "</td><td>" + this.dtDetQueja.Rows[0]["qud_ordprod"].ToString() + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + this.dtDetQueja.Rows[0]["fecha_cad"].ToString() + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + this.dtDetQueja.Rows[0]["qud_cantrecha"].ToString() + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + this.dtDetQueja.Rows[0]["qud_unidad"].ToString() + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + (this.dtDetQueja.Rows[0]["qud_devolucion"].ToString() == "1" ? "SI" : "NO") + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + this.dtDetQueja.Rows[0]["qud_moneda"].ToString() + "</td>";
            this.dtInvestigador = this.con.reporte_investigadores(this.Session["queja"].ToString());
            foreach (DataRow row1 in (InternalDataCollectionBase)this.dtInvestigador.Rows)
            {
                str1 = str1 + "<tr><td colspan='2'><h4>&#8226;" + row1["resp_nombre"].ToString() + "</h4></td><td><h4>" + row1["inv_comentario"].ToString() + "</h4></td></tr>";
                this.dtCausas = this.con.reporte_causas(this.Session["queja"].ToString(), row1["inv_clave"].ToString());
                foreach (DataRow row2 in (InternalDataCollectionBase)this.dtCausas.Rows)
                {
                    str1 += "<tr><td colspan='3'><b>RESPONSABLE ACCIONES CORRECTIVAS</b></td></tr>";
                    str1 += "<tr><td  colspan='2'><b>NOMBRE</b></td><td><b>CAUSA</b></td></tr>";
                    str1 = str1 + "<tr><td  colspan='2'><h5>" + row2["resp_nombre"].ToString() + "</h5></td><td><h5>" + row2["acc_causa"].ToString() + "</h5></td></tr>";
                    this.dtAcciones = this.con.reporte_acciones(this.Session["queja"].ToString(), row2["acc_responsable"].ToString(), row2["acc_folio"].ToString());
                    foreach (DataRow row3 in (InternalDataCollectionBase)this.dtAcciones.Rows)
                    {
                        str1 += "<tr><td colspan='3'><b>ACCIONES CORRECTIVAS</b></td></tr>";
                        str1 += "<tr><td  colspan='2'><b><u>ACCION</u></b></td><td><b><u>FECHA TERMINO</b></td></tr>";
                        str1 = str1 + "<tr><td  colspan='2'>" + row3["acc_accion"] + "</td><td>" + row3["acc_fechatermino"] + "</td></tr>";
                        str1 += "<tr><td colspan='3'><b>VERIFICACIÓN DE ACCIONES CORRECTIVAS</b></td></tr>";
                        str1 += "<tr><td><b><u>CUMPLIMIENTO</u></b></td><td><b><u>COMENTARIO</u></b></td><td><b><u>FECHA</u></b></td></tr>";
                        str1 = str1 + "<tr><td>" + row3["acc_cumpli_ver"] + "</td><td>" + row3["acc_comen_ver"] + "</td><td>" + row3["acc_fecha_ver"] + "</td></tr>";
                        str1 += "<tr><td colspan='3'><b>EFECTIVIDAD DE ACCIONES CORRECTIVAS</b></td></tr>";
                        str1 += "<tr><td><b><u>CUMPLIMIENTO</u></b></td><td><b><u>COMENTARIO</u></b></td><td><b><u>FECHA</u></b></td></tr>";
                        str1 = str1 + "<tr><td>" + row3["acc_cumpli_efe"] + "</td><td>" + row3["acc_comen_efe"] + "</td><td>" + row3["acc_fecha_efe"] + "</td></tr>";
                    }
                }
            }
            this.datosqueja.InnerHtml = "<table border='0' width='95%' class='table table-striped table-bordered table-hover table-responsive'><tr><td align='center' colspan='4'><b>DESCRIPCIÓN DE LA QUEJA</b></td></tr><tr><td>Folio:</td><td>" + str2 + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>Fecha de la queja:</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + shortDateString + "</td></tr><tr><td>Cliente:</td><td>" + str3 + "</td></tr><tr><td>Queja recibida por:</td><td>" + str4 + "</td><td class='visible-lg visible-md hidden-sm hidden-xs'>Tipo de cliente:</td><td class='visible-lg visible-md hidden-sm hidden-xs'>" + str6 + "</td></tr><tr><td>Queja reportada por:</td><td>" + str5 + "</td></tr></table><br /><table border='0' width='95%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='8' align='center'><b>DESCRIPCI&Oacute;N DEL PROBLEMA</b></td></tr><tr><th>PRODUCTO</th><th>PROBLEMA</th><th>ORDEN PROD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>FECHA CAD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>CANTIDAD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>UNIDAD</th><th class='visible-lg visible-md hidden-sm hidden-xs'>DEVOLUCI&Oacute;N</th><th class='visible-lg visible-md hidden-sm hidden-xs'>MONEDA</th></tr><tr>" + str7 + "</tr></table><br /><table border='0' width='95%' class='table table-striped table-bordered table-hover table-responsive'><tr><td colspan='3' align='center'><b>INVESTIGACI&Oacute;N DE LAS CAUSAS Y ACCIONES CORRECTIVAS</b></td></tr><tr><th colspan='2'>RESPONSABLE</th><th>COMENTARIO</th></tr>" + str1 + "</table>";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = this.Session["queja"];
            this.Session["cliente"] = this.Session["cliente"];
            this.Response.Redirect("contenido.aspx");
        }
    }
}