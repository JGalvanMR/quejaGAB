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

namespace queja
{
    public partial class fumigaciones : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtPlacas = new DataTable();
        private DataTable dtQuejasServ = new DataTable();
        private DataTable dtAgregar = new DataTable();
        private DataTable dtInsectos = new DataTable();
        private DataTable dtTabla = new DataTable();
        private DataTable dtEmbarques = new DataTable();
        private DataTable dtRanchos = new DataTable();
        private DataSet dtDatosConsulta = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblTarea.Text = this.Session["tarea"].ToString();
            string str = this.Session["tarea"].ToString();
            if (this.Page.IsPostBack)
                return;
            if (str == "CONSULTA")
            {
                this.txtFolio.Text = this.lblQueja.Text;
                this.deshabilita_controles();
                this.carga_datos_consulta(this.lblQueja.Text, this.lblClave.Text);
                this.lblTipo.Text = "F";
            }
            else
            {
                this.dtTabla.Columns.AddRange(new DataColumn[22]
                {
                  new DataColumn("pedido", typeof (string)),
                  new DataColumn("id_producto", typeof (string)),
                  new DataColumn("qud_ordprod", typeof (string)),
                  new DataColumn("qud_area", typeof (string)),
                  new DataColumn("qud_responsable", typeof (string)),
                  new DataColumn("qud_cantrecha", typeof (string)),
                  new DataColumn("qud_cantreci", typeof (string)),
                  new DataColumn("qud_unidad", typeof (string)),
                  new DataColumn("qud_devolucion", typeof (string)),
                  new DataColumn("qud_moneda", typeof (string)),
                  new DataColumn("prov_clave", typeof (string)),
                  new DataColumn("rch_clave", typeof (string)),
                  new DataColumn("tbl_clave", typeof (string)),
                  new DataColumn("qud_lote", typeof (string)),
                  new DataColumn("nom_producto", typeof (string)),
                  new DataColumn("fecha_cad", typeof (string)),
                  new DataColumn("qud_tarima", typeof (string)),
                  new DataColumn("prov_nombre", typeof (string)),
                  new DataColumn("rch_nombre", typeof (string)),
                  new DataColumn("tbl_nombre", typeof (string)),
                  new DataColumn("tipo", typeof (string)),
                  new DataColumn("vari", typeof (string))
                });
                this.Session["DETALLE"] = (object)this.dtTabla;
                this.dtRanchos.Columns.Add("producto", typeof(string));
                this.dtRanchos.Columns.Add("descripcion", typeof(string));
                this.dtRanchos.Columns.Add("rancho", typeof(string));
                this.dtRanchos.Columns.Add("tabla", typeof(string));
                this.dtRanchos.Columns.Add("prod", typeof(string));
                this.dtRanchos.Columns.Add("pedi", typeof(string));
                this.dtRanchos.Columns.Add("nomb", typeof(string));
                this.Session["Ranchos"] = (object)this.dtRanchos;
                this.dtInsectos.Columns.Add("ins_folio", typeof(string));
                this.dtInsectos.Columns.Add("ins_nombre", typeof(string));
                this.dtInsectos.Columns.Add("ins_nota", typeof(string));
                this.Session["Insectos"] = (object)this.dtInsectos;
                this.txtFolio.Text = this.con.cargafolio_serv().ToString();
                this.txtSemana.Text = this.con.cargasemana().ToString();
                this.txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtResponsable.Text = this.lblNombre.Text;
                this.lblTipo.Text = "F";
                this.dtInsectos = this.con.combo_insectos_fumig();
                this.ddlInsecto.DataSource = (object)this.dtInsectos;
                this.ddlInsecto.DataTextField = "ins_nombre";
                this.ddlInsecto.DataValueField = "ins_folio";
                this.ddlInsecto.DataBind();
                this.upnInsecti.Update();
            }
        }

        public void habilita_controles()
        {
            this.txtFechaEmb.Enabled = true;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (this.txtFechaEmb.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione la fecha del pedido');", true);
            }
            else
            {
                this.dtPlacas.Clear();
                this.dtPlacas = this.con.comboplacas(this.txtFechaEmb.Text);
                this.ddlTransporte.DataSource = (object)this.dtPlacas;
                this.ddlTransporte.DataTextField = "no_trailer";
                this.ddlTransporte.DataValueField = "pdn_folio";
                this.ddlTransporte.DataBind();
                this.udpPlacas.Update();
            }
        }

        protected void ddlTransporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTipo.SelectedValue == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No se ha seleccionado el tipo de pedido');", true);
                this.ddlTransporte.SelectedValue = "0";
                this.udpPlacas.Update();
            }
            else
            {
                string[] strArray = this.ddlTransporte.SelectedItem.ToString().Split('-');
                string[] m1 = this.txtFechaEmb.Text.Split('/');
                //string m = Convert.ToDateTime(this.txtFechaEmb.Text).Month.ToString();
                string m = m1[1].ToString();

                DataTable dataTable = this.con.detalle_embarque_servicios(strArray[0].ToString().TrimEnd(), this.txtFechaEmb.Text);
                string str = "";
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                    str = str + row["pdn_folio"].ToString() + ", ";
                this.txtPedido.Text = str.TrimEnd(' ').TrimEnd(',');
                this.upnPed.Update();
                this.txtMes.Text = this.mes_letra(m);
                this.upnMes.Update();
                this.txtTransportista.Text = strArray[3].ToString().TrimStart(' ');
                this.upnTrans.Update();
                this.txtCaja.Text = strArray[0].ToString().TrimStart(' ');
                this.upnCaja.Update();
                if (this.ddlTipo.SelectedValue == "EXPORTACION")
                {
                    if (this.con.tipo_pedido_servicios(this.txtPedido.Text.ToString().Split(',')[0]) == "EXP")
                    {
                        this.txtAduana.Text = strArray[1].ToString().TrimStart(' ');
                        this.upnAdu.Update();
                    }
                }
                this.dtTabla = (DataTable)this.Session["DETALLE"];
                this.dtTabla.Clear();
                this.Session["DETALLE"] = (object)"";
                this.Session["DETALLE"] = (object)this.dtTabla;
                this.txtTransporte.Text.Split('-');
                this.dtEmbarques = this.con.detalle_embarque_concentrado(this.txtEmbarque.Text, this.txtCaja.Text, this.txtFechaEmb.Text, this.ddlTipo.SelectedValue.ToString(), "");
                this.gvwQuejas.DataSource = (object)this.dtEmbarques;
                this.gvwQuejas.DataBind();
                this.upnQuejas.Update();
            }
        }

        public string mes_letra(string m)
        {
            string str = "";
            switch (m)
            {
                case "1":
                    str = "ENERO";
                    break;
                case "2":
                    str = "FEBRERO";
                    break;
                case "3":
                    str = "MARZO";
                    break;
                case "4":
                    str = "ABRIL";
                    break;
                case "5":
                    str = "MAYO";
                    break;
                case "6":
                    str = "JUNIO";
                    break;
                case "7":
                    str = "JULIO";
                    break;
                case "8":
                    str = "AGOSTO";
                    break;
                case "9":
                    str = "SEPTIEMBRE";
                    break;
                case "10":
                    str = "OCTUBRE";
                    break;
                case "11":
                    str = "NOVIEMBRE";
                    break;
                case "12":
                    str = "DICIEMBRE";
                    break;
            }
            return str;
        }

        protected void gvwQuejas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "consulta"))
                return;
            GridViewRow row1 = this.gvwQuejas.Rows[Convert.ToInt32(e.CommandArgument)];
            string producto = this.Server.HtmlDecode(row1.Cells[1].Text);
            string descripcion = this.Server.HtmlDecode(row1.Cells[2].Text);
            string pedido = this.Server.HtmlDecode(row1.Cells[3].Text);
            this.dtRanchos = (DataTable)this.Session["Ranchos"];
            bool flag = false;
            foreach (DataRow dataRow in this.dtRanchos.Select("producto = '" + producto + "'"))
                flag = true;
            if (flag)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El producto ya fue agrgado');", true);
            }
            else
            {
                foreach (DataRow row2 in (InternalDataCollectionBase)this.con.rancho_tabla(producto, pedido, descripcion).Rows)
                    this.dtRanchos.Rows.Add(row2.ItemArray);
                this.Session["Ranchos"] = (object)this.dtRanchos;
                this.gvwProductos.DataSource = (object)this.dtRanchos;
                this.gvwProductos.DataBind();
                this.upnProductos.Update();
            }
        }

        protected void gvwProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "quitar") || this.lblTarea.Text == "CONSULTA")
                return;
            string str = this.Server.HtmlDecode(this.gvwProductos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
            DataTable dataTable = (DataTable)this.Session["Ranchos"];
            foreach (DataRow dataRow in dataTable.Select("producto = '" + str + "'"))
                dataRow.Delete();
            this.Session["Ranchos"] = (object)dataTable;
            this.gvwProductos.DataSource = (object)(DataTable)this.Session["Ranchos"];
            this.gvwProductos.DataBind();
            this.upnProductos.Update();
        }

        protected void btnAgregaIns_Click(object sender, EventArgs e)
        {
            this.dtInsectos = (DataTable)this.Session["Insectos"];
            string str = this.ddlInsecto.SelectedValue.ToString();
            if (str == "")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione insecto');", true);
            }
            else
            {
                bool flag = false;
                foreach (DataRow dataRow in this.dtInsectos.Select("ins_folio = '" + str + "'"))
                    flag = true;
                if (flag)
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('El insecto ya fue agrgado');", true);
                }
                else
                {
                    this.dtInsectos.Rows.Add((object)str, (object)this.ddlInsecto.SelectedItem.ToString(), (object)this.txtNota.Text.ToUpper());
                    this.Session["Insectos"] = (object)this.dtInsectos;
                    this.gvwInsectos.DataSource = (object)this.dtInsectos;
                    this.gvwInsectos.DataBind();
                    this.udpInsectos.Update();
                    this.ddlInsecto.SelectedValue = "";
                    this.upnInsecti.Update();
                    this.txtNota.Text = "";
                    this.upnNota.Update();
                }
            }
        }

        protected void gvwInsectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "quitar") || this.lblTarea.Text == "CONSULTA")
                return;
            string str = this.Server.HtmlDecode(this.gvwInsectos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
            DataTable dataTable = (DataTable)this.Session["Insectos"];
            foreach (DataRow dataRow in dataTable.Select("ins_folio = '" + str + "'"))
                dataRow.Delete();
            this.Session["Insectos"] = (object)dataTable;
            this.gvwInsectos.DataSource = (object)dataTable;
            this.gvwInsectos.DataBind();
            this.udpInsectos.Update();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.lblTarea.Text == "CONSULTA")
            {
                if (this.con.modifica_fertilizante(this.lblQueja.Text, this.txtHold.Text, this.txtReleased.Text, this.txtCosto.Text) == "1")
                    this.lblSuccess.Visible = true;
                else
                    this.lblWarning.Visible = true;
            }
            else
            {
                DataTable dtP = (DataTable)this.Session["Ranchos"];
                DataTable dtI = (DataTable)this.Session["Insectos"];
                if (dtP.Rows.Count == 0)
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione un producto para cargar los Ranchos correspondientes');", true);
                else if (dtI.Rows.Count == 0)
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione por lo menos un insecto');", true);
                else if (this.txtPedido.Text == "")
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione un transportista');", true);
                else if (this.txtHold.Text == "")
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione la fecha HOLD');", true);
                else if (this.txtReleased.Text == "")
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Seleccione la fecha RELEASED');", true);
                else if (this.txtCosto.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('Ingrese el costo por fumigacón');", true);
                }
                else
                {
                    this.btnGuardar.Enabled = false;
                    this.udpGuardar.Update();
                    string text1 = this.txtFecha.Text;
                    string text2 = this.txtSemana.Text;
                    string text3 = this.txtFechaEmb.Text;
                    string tipo = this.ddlTipo.SelectedValue.ToString();
                    string text4 = this.txtPedido.Text;
                    string text5 = this.txtMes.Text;
                    string text6 = this.txtTransportista.Text;
                    string text7 = this.txtCaja.Text;
                    string text8 = this.lblClave.Text;
                    string text9 = this.txtAduana.Text;
                    string text10 = this.txtHold.Text;
                    string text11 = this.txtReleased.Text;
                    string text12 = this.lblTipo.Text;
                    string text13 = this.txtCosto.Text;
                    string text14 = this.lblNombre.Text;
                    string source = this.con.guarda_servicio_fum(dtP, text1, text2, text3, tipo, text4, text5, text6, text9, text7, text8, text9, text10, text11, text12, dtI, text14, text13);
                    if (source.Contains<char>('.'))
                    {
                        string str1 = "<tr><td><table><tr><td><b>Insecto(s):</b></td></tr>";
                        foreach (DataRow row in (InternalDataCollectionBase)dtI.Rows)
                            str1 = str1 + "<tr><td>" + row["ins_nombre"] + "</td></tr>";
                        string str2 = str1 + "</table></td></tr>";
                        string[] strArray = source.Split('.');
                        this.lblSuccess.Visible = true;
                        string str3 = "http://189.206.160.206:81/quejas/";
                        string str4 = "http://gabira1:81/quejas/";
                        string str5 = "http://189.206.160.206:81/quejas/accion.aspx?key=" + strArray[0].ToString();
                        string str6 = "http://gabira1:81/quejas/accion.aspx?key=" + strArray[0].ToString();
                        string cuerpo = "<table border='2'><tr><td align='center'><h2>Registro de Queja de Fumigacion</h2></td></tr><tr><td>Registro de queja realizado por: " + this.lblNombre.Text + "</td></tr><tr><td>CEDIS: " + this.lblCedis.Text + "</td></tr><tr><td>Fecha: " + this.txtFecha.Text + "</td></tr><tr><td>Queja: No. " + strArray[0].ToString() + "</td></tr><tr><td>Transportista: " + text6 + "</td></tr><tr><td>Caja: " + text7 + "</td></tr><tr><td>Pedido: " + text4.ToString() + "</td></tr><tr><td>Tipo: " + tipo + "</td></tr>" + str2 + "</table><br /><p><h4>Entrar al sistema de quejas<br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str4 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str3 + "</h4></p><p><h4>Registrar accion correctiva<br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str5 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str6 + "</h4></p>";
                        this.enviarcorreo("aescamilla@mrlucky.com.mx", strArray[0].ToString(), cuerpo);
                    }
                    else if (!source.Contains<char>('.'))
                        this.lblParcial.Visible = true;
                    else
                        this.lblWarning.Visible = true;
                }
            }
        }

        public void enviarcorreo(string correo, string cve_queja, string cuerpo)
        {
            Dns.GetHostEntry(Dns.GetHostName());
            MailMessage message = new MailMessage();
            message.To.Add(correo);
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + cve_queja;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = cuerpo;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.From = new MailAddress("sistemas@mrlucky.com.mx");
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential("sistemas", "sisgab");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "mail1.mrlucky.com.mx";
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, typeof(Page), "Queja", "alert('No fue enviado el correo electrónico');", true);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas_fumigacion.aspx");
        }

        public void deshabilita_controles()
        {
            this.txtFechaEmb.ReadOnly = true;
            this.udpFechaEmb.Update();
            this.btnBuscar.Enabled = false;
            this.ddlTipo.Enabled = false;
            this.ddlTransporte.Enabled = false;
            this.udpPlacas.Update();
            this.ddlInsecto.Enabled = false;
            this.upnInsecti.Update();
            this.txtNota.ReadOnly = true;
            this.upnNota.Update();
            this.btnAgregaIns.Enabled = false;
            this.upnAgregaIns.Update();
        }

        public void carga_datos_consulta(string queja, string resp)
        {
            this.dtDatosConsulta = this.con.datos_queja_fumigacion(queja, resp);
            DataTable table1 = this.dtDatosConsulta.Tables["Maestro"];
            DataTable table2 = this.dtDatosConsulta.Tables["Productos"];
            DataTable table3 = this.dtDatosConsulta.Tables["Insectos"];
            foreach (DataRow row in (InternalDataCollectionBase)table1.Rows)
            {
                this.txtFecha.Text = row["que_fecha"].ToString();
                this.txtFechaEmb.Text = row["que_fechaemb"].ToString();
                this.txtSemana.Text = row["que_semana"].ToString();
                this.ddlTipo.SelectedValue = row["que_tipo"].ToString();
                this.txtPedido.Text = row["que_pedido"].ToString();
                this.txtMes.Text = row["que_mes"].ToString();
                this.txtTransportista.Text = row["que_transportista"].ToString();
                this.txtAduana.Text = row["que_aduana"].ToString();
                this.txtCaja.Text = row["que_caja"].ToString();
                this.txtResponsable.Text = row["que_responsable"].ToString();
                this.txtAccCorrectiva.Text = row["descripcion"].ToString();
                this.txtHold.Text = row["que_hold"].ToString() == "" ? "" : Convert.ToDateTime(row["que_hold"].ToString()).ToString("dd/MM/yyyy");
                this.txtReleased.Text = row["que_released"].ToString() == "" ? "" : Convert.ToDateTime(row["que_released"].ToString()).ToString("dd/MM/yyyy");
                this.txtCosto.Text = Convert.ToDecimal(row["costo"].ToString()) > new Decimal(0) ? Convert.ToDecimal(row["costo"].ToString()).ToString() : "0.00";
            }
            this.gvwProductos.DataSource = (object)table2;
            this.gvwProductos.DataBind();
            this.upnProductos.Update();
            this.gvwInsectos.DataSource = (object)table3;
            this.gvwInsectos.DataBind();
            this.udpInsectos.Update();
        }
    }
}