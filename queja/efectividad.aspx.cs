using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace queja
{
    public partial class efectividad : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private string fecha_reg = "";
        private string datos = "";
        private DataTable dtInvestigador = new DataTable();
        private DataTable dtAcciones = new DataTable();
        private DataTable dtAccion_Correctivas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.lblClave.Text = this.Session["clave"].ToString();
            //this.lblNombre.Text = this.Session["nombre"].ToString();
            //this.lblCedis.Text = this.Session["cedis"].ToString();
            //this.lblQueja.Text = this.Session["queja"].ToString();
            //this.Session["cliente"] = this.Session["cliente"];
            //if (!this.Page.IsPostBack)
            //{
            //    this.fecha_reg = this.con.cargadatosqueja(this.lblQueja.Text);
            //    this.datos = this.con.datosqueja(this.lblQueja.Text);
            //    string[] strArray = this.datos.Split('-');
            //    this.txtProducto.Text = strArray[0];
            //    this.txtProblema.Text = strArray[1];
            //    this.txtFecha.Text = Convert.ToDateTime(this.fecha_reg).ToString("dd/MM/yyyy");
            //    this.dtInvestigador = this.con.verificacion_investigadores_acciones(this.lblQueja.Text);
            //    this.gvwResponsables.DataSource = (object)this.dtInvestigador;
            //    this.gvwResponsables.DataBind();
            //    this.upnResponsables.Update();
            //}
            //string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_ant/"));
            //List<ListItem> listItemList = new List<ListItem>();
            //foreach (string path in files)
            //{
            //    if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + ".pdf"))
            //        listItemList.Add(new ListItem(Path.GetFileName(path), path));
            //}
            //int num = 1;
            //foreach (ListItem listItem in listItemList)
            //{
            //    if (num == 1)
            //        this.btnFile1.Text = listItem.ToString();
            //    if (num == 2)
            //        this.btnFile2.Text = listItem.ToString();
            //    if (num == 3)
            //        this.btnFile3.Text = listItem.ToString();
            //    ++num;
            //}
            //string str = "fotos_ant/1_" + this.lblQueja.Text + ".jpg";
            //this.img1.ImageUrl = str;
            //this.img2.ImageUrl = "fotos_ant/2_" + this.lblQueja.Text + ".jpg";
            //this.img3.ImageUrl = "fotos_ant/3_" + this.lblQueja.Text + ".jpg";
            //this.Image1.ImageUrl = str;
            //this.Image2.ImageUrl = "fotos_ant/2_" + this.lblQueja.Text + ".jpg";
            //this.Image3.ImageUrl = "fotos_ant/3_" + this.lblQueja.Text + ".jpg";
            //this.ddlCumplimiento.Enabled = false;
            //this.txtComentario.Enabled = false;
        }

        protected void gvwResponsables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (!(e.CommandName == "consulta"))
            //    return;
            //this.dtAcciones = this.con.verificacion_responsables_acciones(this.Server.HtmlDecode(this.gvwResponsables.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text), this.lblQueja.Text);
            //this.gvwRespAcciones.DataSource = (object)this.dtAcciones;
            //this.gvwRespAcciones.DataBind();
            //this.upnRespAccion.Update();
            //this.gvwAccionesCorrectivas.DataSource = (object)"";
            //this.gvwAccionesCorrectivas.DataBind();
            //this.upnAccionesCorrectivas.Update();
            //this.lblAccionCorrectiva.Text = "-";
            //this.upnAccioncorrectiva.Update();
            //this.lblFolioRespAcc.Text = "-";
            //this.lblClaveRespAcc.Text = "-";
            //this.upnRespAcciones.Update();
            //this.File1.Text = "File";
            //this.File2.Text = "File";
            //this.File3.Text = "File";
            //this.upnBotones.Update();
            //this.imagen1.ImageUrl = "";
            //this.imagen2.ImageUrl = "";
            //this.imagen3.ImageUrl = "";
            //this.uplImagenes.Update();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            //this.Session["clave"] = (object)this.lblClave.Text;
            //this.Session["nombre"] = (object)this.lblNombre.Text;
            //this.Session["cedis"] = (object)this.lblCedis.Text;
            //this.Session["queja"] = (object)this.lblQueja.Text;
            //this.Session["cliente"] = this.Session["cliente"];
            //this.Response.Redirect("contenido.aspx");
        }

        protected void gvwRespAcciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (!(e.CommandName == "consulta_acciones"))
            //    return;
            //GridViewRow row = this.gvwRespAcciones.Rows[Convert.ToInt32(e.CommandArgument)];
            //string folio = this.Server.HtmlDecode(row.Cells[0].Text);
            //string resp = this.Server.HtmlDecode(row.Cells[1].Text);
            //string str = this.Server.HtmlDecode(row.Cells[2].Text);
            //this.dtAccion_Correctivas = this.con.efectividad_acciones_correctivas(folio, resp);
            //this.gvwAccionesCorrectivas.DataSource = (object)this.dtAccion_Correctivas;
            //this.gvwAccionesCorrectivas.DataBind();
            //this.upnAccionesCorrectivas.Update();
            //this.lblFolioRespAcc.Text = folio;
            //this.lblClaveRespAcc.Text = resp;
            //this.lblNombreRespAcc.Text = str;
            //this.upnRespAcciones.Update();
            //this.lblAccionCorrectiva.Text = "-";
            //this.lblAccionCorrectivaDesc.Text = "-";
            //this.upnAccioncorrectiva.Update();
            //this.btnCancelar.Visible = false;
            //this.upnCancelar.Update();
            //this.imagen1.ImageUrl = "";
            //this.imagen2.ImageUrl = "";
            //this.imagen3.ImageUrl = "";
            //this.uplImagenes.Update();
            //this.File1.Text = "File";
            //this.File2.Text = "File";
            //this.File3.Text = "File";
            //this.upnBotones.Update();
            //this.lblOperacion.Text = "-";
            //this.upnOperacion.Update();
        }

        protected void gvwAccionesCorrectivas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (!(e.CommandName == "verificar_accion"))
            //    return;
            //GridViewRow row = this.gvwAccionesCorrectivas.Rows[Convert.ToInt32(e.CommandArgument)];
            //string str1 = this.Server.HtmlDecode(row.Cells[0].Text);
            //string str2 = this.Server.HtmlDecode(row.Cells[1].Text);
            //string str3 = this.Server.HtmlDecode(row.Cells[6].Text).Trim();
            //string str4 = this.Server.HtmlDecode(row.Cells[7].Text);
            //string str5 = this.Server.HtmlDecode(row.Cells[8].Text);
            //this.lblAccionCorrectiva.Text = str1;
            //this.lblAccionCorrectivaDesc.Text = str2;
            //this.upnAccioncorrectiva.Update();
            //this.ddlCumplimiento.Enabled = true;
            //this.txtComentario.Enabled = true;
            //string fech_ver = DateTime.Now.ToString("MM/dd/yyyy");
            //this.txtFechaVer.Text = Convert.ToDateTime(fech_ver).ToString("dd/MM/yyyy");
            //this.updateVerificacion.Update();
            //this.imagen1.ImageUrl = "fotos_des/1_" + this.lblQueja.Text + "_" + this.lblAccionCorrectiva.Text + ".jpg";
            //this.imagen2.ImageUrl = "fotos_des/2_" + this.lblQueja.Text + "_" + this.lblAccionCorrectiva.Text + ".jpg";
            //this.imagen3.ImageUrl = "fotos_des/3_" + this.lblQueja.Text + "_" + this.lblAccionCorrectiva.Text + ".jpg";
            //this.uplImagenes.Update();
            //string[] files = Directory.GetFiles(this.Server.MapPath("~/fotos_des/"));
            //List<ListItem> listItemList = new List<ListItem>();
            //foreach (string path in files)
            //{
            //    if (Path.GetFileName(path).Contains("_" + this.lblQueja.Text + "_" + this.lblAccionCorrectiva.Text + ".pdf"))
            //        listItemList.Add(new ListItem(Path.GetFileName(path), path));
            //}
            //int num = 1;
            //foreach (ListItem listItem in listItemList)
            //{
            //    if (num == 1)
            //        this.File1.Text = listItem.ToString();
            //    if (num == 2)
            //        this.File2.Text = listItem.ToString();
            //    if (num == 3)
            //        this.File3.Text = listItem.ToString();
            //    ++num;
            //}
            //this.upnBotones.Update();
            //if (str3 != "")
            //{
            //    this.txtFechaVer.Text = str3;
            //    this.ddlCumplimiento.SelectedValue = str4;
            //    this.txtComentario.Text = str5;
            //    this.lblOperacion.Text = "CONSULTA";
            //    this.upnOperacion.Update();
            //    this.btnCancelar.Visible = true;
            //    this.upnCancelar.Update();
            //}
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //if (this.con.insertaefectividad(this.lblAccionCorrectiva.Text, this.txtFechaVer.Text, this.ddlCumplimiento.SelectedValue.ToString().ToUpper(), this.txtComentario.Text.ToUpper()) == "1")
            //{
            //    this.lblSuccess.Visible = true;
            //    this.dtAccion_Correctivas = this.con.efectividad_acciones_correctivas(this.lblFolioRespAcc.Text, this.lblClaveRespAcc.Text);
            //    this.gvwAccionesCorrectivas.DataSource = (object)this.dtAccion_Correctivas;
            //    this.gvwAccionesCorrectivas.DataBind();
            //    this.upnAccionesCorrectivas.Update();
            //    this.Session["accion"] = (object)this.lblAccionCorrectiva.Text;
            //    this.correo(this.lblQueja.Text, this.txtProducto.Text, this.txtProblema.Text, this.lblAccionCorrectivaDesc.Text, this.lblNombreRespAcc.Text, this.ddlCumplimiento.SelectedValue.ToString().ToUpper(), this.txtComentario.Text.ToUpper(), this.txtFechaVer.Text);
            //    this.lblAccionCorrectiva.Text = "-";
            //    this.lblAccionCorrectivaDesc.Text = "-";
            //    this.upnAccioncorrectiva.Update();
            //    this.con.etapa_efectividad(this.lblQueja.Text);
            //    this.ddlCumplimiento.SelectedValue = "";
            //    this.txtComentario.Text = "";
            //    this.updateVerificacion.Update();
            //}
            //else
            //    this.lblWarning.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //this.lblAccionCorrectiva.Text = "-";
            //this.lblAccionCorrectivaDesc.Text = "-";
            //this.upnAccioncorrectiva.Update();
            //this.txtFechaVer.Text = "";
            //this.ddlCumplimiento.SelectedValue = "";
            //this.txtComentario.Text = "";
            //this.ddlCumplimiento.Enabled = false;
            //this.txtComentario.Enabled = false;
            //this.updateVerificacion.Update();
            //this.btnCancelar.Visible = false;
            //this.upnCancelar.Update();
            //this.imagen1.ImageUrl = "";
            //this.imagen2.ImageUrl = "";
            //this.imagen3.ImageUrl = "";
            //this.uplImagenes.Update();
            //this.lblOperacion.Text = "-";
            //this.upnOperacion.Update();
            //this.File1.Text = "File";
            //this.File2.Text = "File";
            //this.File3.Text = "File";
            //this.upnBotones.Update();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //this.lblSuccess.Visible = false;
            //this.lblSuccessInv.Visible = false;
            //this.lblWarning.Visible = false;
            //this.lblWarningInv.Visible = false;
        }

        protected void btnFile1_Click(object sender, EventArgs e)
        {
            //if (!(this.btnFile1.Text != "File"))
            //    return;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "<script>window.open('fotos_ant/" + this.btnFile1.Text + "', '_blank');</script>", false);
        }

        protected void btnFile2_Click(object sender, EventArgs e)
        {
            //if (!(this.btnFile1.Text != "File"))
            //    return;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "<script>window.open('fotos_ant/" + this.btnFile2.Text + "', '_blank');</script>", false);
        }

        protected void btnFile3_Click(object sender, EventArgs e)
        {
            //if (!(this.btnFile1.Text != "File"))
            //    return;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "<script>window.open('fotos_ant/" + this.btnFile3.Text + "', '_blank');</script>", false);
        }

        protected void File1_Click(object sender, EventArgs e)
        {
            //if (!(this.File1.Text != "File"))
            //    return;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "<script>window.open('fotos_des/" + this.File1.Text + "', '_blank');</script>", false);
        }

        protected void File2_Click(object sender, EventArgs e)
        {
            //if (!(this.File2.Text != "File"))
            //    return;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "<script>window.open('fotos_des/" + this.File2.Text + "', '_blank');</script>", false);
        }

        protected void File3_Click(object sender, EventArgs e)
        {
            //if (!(this.File3.Text != "File"))
            //    return;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "<script>window.open('fotos_des/" + this.File3.Text + "', '_blank');</script>", false);
        }

        protected void correo(
          string queja,
          string producto,
          string problema,
          string accion,
          string responsable,
          string cumplimiento,
          string comentario,
          string fecha)
        {
            //string str1 = "http://189.206.160.206:81/quejas/";
            //string str2 = "http://gabira1:81/quejas/";
            //string str3 = "<table border='2'><tr><td align='center'><h2>Verificaci&oacute;n de Efectividad de Acci&oacute;n Correctiva</h2></td></tr><tr><td>Producto: " + this.txtProducto.Text + "</td></tr><tr><td>Queja no.: " + this.lblQueja.Text + "</td></tr><tr><td>Problema: " + problema + "</td></tr><tr><td>Acci&oacute;n: " + accion + "</td></tr><tr><td>Responsable: " + responsable + "</td></tr><tr><td>Cumplimiento: " + cumplimiento + "</td></tr><tr><td>Comentario: " + comentario + "</td></tr><tr><td>Fecha de verificaci&oacute;n: " + fecha + "</td></tr></table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            //MailMessage message = new MailMessage();
            //message.To.Add("aescamilla@mrlucky.com.mx"); //message.To.Add("msamano@mrlucky.com.mx");
            //message.CC.Add("aescamilla@mrlucky.com.mx");
            //message.Subject = "Queja no.: " + queja;
            //message.SubjectEncoding = Encoding.UTF8;
            //message.Body = str3;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true;
            //message.From = new MailAddress("sistemas@mrlucky.com.mx");
            //SmtpClient smtpClient = new SmtpClient();
            //smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential("sistemas", "sisgab");
            //smtpClient.Port = 587;
            //smtpClient.EnableSsl = true;
            //smtpClient.Host = "mail1.mrlucky.com.mx";
            //try
            //{
            //    smtpClient.Send(message);
            //}
            //catch (Exception ex)
            //{
            //    this.Response.Write("<script>alert('No fue enviado el correo electronico')</script>");
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.correo(this.lblQueja.Text, this.txtProducto.Text, this.txtProblema.Text, this.lblAccionCorrectivaDesc.Text, this.lblNombreRespAcc.Text, this.ddlCumplimiento.SelectedValue.ToString().ToUpper(), this.txtComentario.Text.ToUpper(), this.txtFechaVer.Text);
        }
    }
}