using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace queja
{
    public partial class contenido : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private DataTable dtEtapas = new DataTable();
        private string fecha_carga_investigador = "";
        private DataTable dtProductos = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string val_clave = Request.QueryString["clave"];
            string val_nombr = Request.QueryString["nombre"]; //"ANGEL ESCAMILLA";
            string val_cedis = Request.QueryString["cedis"]; //"COMERCIALIZADORA GAB";
            string val_admin = Request.QueryString["admin"]; //"ADMINISTRADOR";
            string val_queja = Request.QueryString["queja"]; //"10373";
            string val_clien = Request.QueryString["clien"]; //"COSTCO DE MEXICO, S.A. DE C.V.  PROV 51925";
            if (val_clave != null)
            {
                this.Session["clave"] = val_clave;
                if (val_nombr != "")
                    this.Session["nombre"] = val_nombr;
                if (val_cedis != "")
                    this.Session["cedis"] = val_cedis;
                if (val_admin != "")
                    this.Session["admin"] = val_admin;
                if (val_queja != "")
                    this.Session["queja"] = val_queja;
                if (val_clien != "")
                    this.Session["cliente"] = val_clien;
            }
                

            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblCliente.Text = this.Session["cliente"].ToString();
            //string corr = this.Session["correo"].ToString();
            this.Session["correo"] = this.Session["correo"];
            if (this.Page.IsPostBack)
                return;

            

            lblNotaCredito.Visible = false;
            upnNotaCredito2.Update();
            this.dtEtapas = this.con.consultaetapas(this.lblQueja.Text);
            this.fecha_carga_investigador = this.con.fechaqueja(this.lblQueja.Text);
            this.Session["fecha_carga_investigador"] = this.fecha_carga_investigador;
            if (this.con.valida_queja_pedido(this.lblQueja.Text))
            {
                this.btnEdicion.Visible = false;
                this.uplConsulta.Update();
            }
            else
            {
                this.btnEditarPed.Visible = false;
                this.uplConsulta.Update();
            }
            if (this.con.queja_finalizada(this.lblQueja.Text) == "1")
            {
                this.btnEdicion.Enabled = false;
                this.uplConsulta.Update();
                this.btnInvestigacion.Enabled = false;
                this.uplInvestigacion.Update();
                this.btnCausas.Enabled = false;
                this.uplCausas.Update();
                this.btnAcciones.Enabled = false;
                this.uplAcciones.Update();
                this.btnVerificar.Enabled = false;
                this.uplVerificar.Update();
                this.btnConcluir.Enabled = false;
                this.uplConcluir.Update();
                //this.btnTerminarQueja.Enabled = false;
                //this.UpdatePanel2.Update();
            }
            else if (this.con.deshabilitar_botones_alta_investigador(this.lblQueja.Text) == "0")
            {
                if (this.lblAdmin.Text == "ADMINISTRADOR")
                {
                    this.btnEdicion.Enabled = false;
                    this.uplConsulta.Update();
                    this.btnInvestigacion.Enabled = true;
                    this.uplInvestigacion.Update();
                }
                else
                {
                    this.btnInvestigacion.Enabled = false;
                    this.uplInvestigacion.Update();
                }
                this.btnCausas.Enabled = false;
                this.uplCausas.Update();
                this.btnAcciones.Enabled = false;
                this.uplAcciones.Update();
                this.btnVerificar.Enabled = false;
                this.uplVerificar.Update();
                this.btnConcluir.Enabled = false;
                this.uplConcluir.Update();
                //this.btnTerminarQueja.Enabled = false;
                //this.UpdatePanel2.Update();
                if (this.con.validar_asignar_area(this.lblQueja.Text) == "0")
                {
                    this.lblErrorArea.Visible = true;
                    this.upnMensaje.Update();
                }
            }
            else if (this.con.validar_asignar_area(this.lblQueja.Text) == "0")
            {
                this.btnInvestigacion.Enabled = false;
                this.uplInvestigacion.Update();
                this.btnCausas.Enabled = false;
                this.uplCausas.Update();
                this.btnAcciones.Enabled = false;
                this.uplAcciones.Update();
                this.btnVerificar.Enabled = false;
                this.uplVerificar.Update();
                this.btnConcluir.Enabled = false;
                this.uplConcluir.Update();
                //this.btnTerminarQueja.Enabled = false;
                //this.UpdatePanel2.Update();
                this.lblErrorArea.Visible = true;
                this.upnMensaje.Update();
            }
            else if (this.con.deshabilitar_edicion_mstr_acciones_con_detalle_acciones(this.lblQueja.Text) == "1")
            {
                this.btnEdicion.Enabled = true;
                this.uplConsulta.Update();
                this.btnCausas.Enabled = true;
                this.uplCausas.Update();
                this.btnAcciones.Enabled = true;
                this.uplAcciones.Update();
                this.btnVerificar.Enabled = true;
                this.uplVerificar.Update();
            }
            else if (this.con.deshabilitar_edicion_causas_acciones(this.lblQueja.Text, this.lblClave.Text) == "1")
            {
                this.btnEdicion.Enabled = true;
                this.uplConsulta.Update();
                this.btnCausas.Enabled = true;
                this.uplCausas.Update();
                this.btnAcciones.Enabled = true;
                this.uplAcciones.Update();
                this.btnVerificar.Enabled = true;
                this.uplVerificar.Update();
            }
            else
            {
                this.btnEdicion.Enabled = false;
                this.uplConsulta.Update();
                this.btnCausas.Enabled = false;
                this.uplCausas.Update();
                this.btnAcciones.Enabled = false;
                this.uplAcciones.Update();
                this.btnVerificar.Enabled = false;
                this.uplVerificar.Update();
                this.btnConcluir.Enabled = true;
                this.uplConcluir.Update();
            }
            if (this.lblAdmin.Text == "ADMINISTRADOR")
            {
                this.btnEdicion.Enabled = true;
                this.uplConsulta.Update();
            }
            else
            {
                this.btnInvestigacion.Enabled = false;
                this.uplInvestigacion.Update();
            }

            if (this.con.notas_credito_deshabilitar_consumidor(this.lblQueja.Text) == "1")
            {
                btnNotaCredito.Enabled = false;
                upnNotasCredito.Update();

                btnSoporte.Visible = false;
                btnPdf.Visible = false;
                upnSoporte.Update();

                lblSoporteAviso.Visible = false;
                upnSoporteAviso.Update();
            }
            else
            {
                //buscar si es devolucion y bonificacion
                string dev_bon_mer = con.notas_credito_devolucion_bonificacion(this.lblQueja.Text);

                if(dev_bon_mer.Contains("NA"))
                {
                    btnNotaCredito.Enabled = false;
                    upnNotasCredito.Update();

                    btnSoporte.Visible = false;
                    btnPdf.Visible = false;
                    upnSoporte.Update();

                    lblSoporteAviso.Visible = false;
                    upnSoporteAviso.Update();
                }
                else
                {
                    if (dev_bon_mer.Contains("MER"))
                    {
                        btnNotaCredito.Enabled = false;
                        upnNotasCredito.Update();

                        //validamos si el soporte ya fue capturado
                        DataTable dtSoporteVer = con.queja_soporte_verificar(lblQueja.Text);
                        if (dtSoporteVer.Rows.Count > 0)
                        {
                            btnSoporte.Visible = false;
                            btnPdf.Visible = true;
                            upnSoporte.Update();

                            lblSoporteAviso.Visible = false;
                            upnSoporteAviso.Update();
                            hflSoporte.Value = dtSoporteVer.Rows[0]["que_archivo"].ToString();
                        }
                        else
                        {
                            btnSoporte.Visible = true;
                            btnPdf.Visible = false;
                            upnSoporte.Update();

                            lblSoporteAviso.Visible = true;
                            upnSoporteAviso.Update();
                        }
                    }
                    else
                    {

                        //VALIDAR ESTATUS DE ASIGNACION A NOTA DE CREDITO
                        string asignacion_nc = con.notas_credito_asignacion(this.lblQueja.Text);

                        //validamos si el soporte ya fue capturado
                        DataTable dtSoporteVer = con.queja_soporte_verificar(lblQueja.Text);
                        if (dtSoporteVer.Rows.Count > 0)
                        {
                            btnSoporte.Visible = false;
                            btnPdf.Visible = true;
                            upnSoporte.Update();

                            lblSoporteAviso.Visible = false;
                            upnSoporteAviso.Update();
                            hflSoporte.Value = dtSoporteVer.Rows[0]["que_archivo"].ToString();
                        }
                        else
                        {
                            btnSoporte.Visible = true;
                            btnPdf.Visible = false;
                            upnSoporte.Update();

                            lblSoporteAviso.Visible = true;
                            upnSoporteAviso.Update();
                        }

                        //btnSoporte.Visible = false;
                        //btnPdf.Visible = false;
                        //upnSoporte.Update();

                        //lblSoporteAviso.Visible = false;
                        //upnSoporteAviso.Update();

                        if (asignacion_nc == "0")
                        {
                            btnNotaCredito.Enabled = true;
                            upnNotasCredito.Update();
                        }
                        else
                        {
                            lblNotaCredito.Visible = true;
                            upnNotaCredito2.Update();
                        }



                    }
                }

                

                //VALIDACION PARA DESHABILITAR CONTROL DESPUES DE NUEVE DIAS SIN ASIGNAR O NO ASIGNAR LA NOTA DE CREDITO POR MOTIVO DE BONIFICACION O DEVOLUCION
                this.Session["NC"] = true;
                if (dev_bon_mer.Contains("BON") || dev_bon_mer.Contains("DEV"))
                {
                    if (dev_bon_mer.Contains("BON"))
                    {
                        DateTime dateTime2 = DateTime.Now;
                        string d_2 = dateTime2.Day.ToString();
                        string m_2 = dateTime2.Month.ToString().PadLeft(2, '0');
                        string a_2 = dateTime2.Year.ToString();

                        string f_2 = d_2 + "/" + m_2 + "/" + a_2;

                        if (con.queja_nota_credito_vencida(f_2, this.lblQueja.Text, "BON") == true)
                        {
                            btnNotaCredito.Enabled = false;
                            upnNotasCredito.Update();

                            lblNotaCreditoVencida.Visible = true;
                            upnNotaCreditoVencida.Update();
                            this.Session["NC"] = false;
                        }
                    }
                    else
                    {
                        DateTime dateTime2 = DateTime.Now;
                        string d_2 = dateTime2.Day.ToString();
                        string m_2 = dateTime2.Month.ToString().PadLeft(2, '0');
                        string a_2 = dateTime2.Year.ToString();
                        string f_2 = d_2 + "/" + m_2 + "/" + a_2;

                        if (con.queja_nota_credito_vencida(f_2, this.lblQueja.Text, "DEV") == true)
                        {
                            btnNotaCredito.Enabled = false;
                            upnNotasCredito.Update();

                            lblNotaCreditoVencida.Visible = true;
                            upnNotaCreditoVencida.Update();
                            this.Session["NC"] = false;
                        }
                    }
                }
                

            }

            bool validacion_mp = false;
            validacion_mp = con.valida_productos_mp(lblQueja.Text);

            if (validacion_mp == false)
            {
                lblNotaMp.Visible = true;
                upnNotaMp.DataBind();

                btnAcciones.Enabled = false;
                uplAcciones.DataBind();
                btnVerificar.Enabled = false;
                uplVerificar.DataBind();
                btnConcluir.Enabled = false;
                uplConcluir.DataBind();

                btnCausas.Enabled = true;
                uplCausas.DataBind();
            }

            btnNotaCredito.Enabled = false;
            upnNotasCredito.Update();

            if (lblClave.Text == "10" || lblClave.Text == "11" || lblClave.Text == "19" || lblClave.Text == "43")
            {
                if (Convert.ToBoolean(this.Session["NC"]) != false)
                {
                    btnNotaCredito.Enabled = true;
                    upnNotasCredito.Update();

                    
                }
                
            }

            

            ////dtProductos = con.productos_queja(this.lblQueja.Text);
            ////bool validacion_mp = false;
            ////foreach (DataRow rw in dtProductos.Rows)
            ////{
            ////    string val1 = rw["id_producto"].ToString();
            ////    string val2 = rw["qud_ordprod"].ToString();

            ////    DataTable str = con.valida_productos_mp(val1, lblQueja.Text, val2);

            ////    foreach (DataRow rz in str.Select("seleccionado = '1'"))
            ////    {
            ////        validacion_mp = true;
            ////        lblNotaMp.Visible = false;
            ////        upnNotaMp.DataBind();
            ////    }

            ////    if (validacion_mp == false)
            ////    {
            ////        lblNotaMp.Visible = true;
            ////        upnNotaMp.DataBind();
            ////    }

            ////    //if (str == "X")
            ////    //{
                
            ////    //}
            ////}

            
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("consulta.aspx");
        }

        protected void btnInvestigacion_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("investigadores.aspx");
        }

        protected void btnCausas_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("causas.aspx");
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("verificacion.aspx");
        }

        protected void btnAcciones_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("accioncorrectiva.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("quejas.aspx");
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("report.aspx");
        }

        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            if (this.con.cierre_queja_verificacion(this.lblQueja.Text) == "1")
            {
                this.Session["clave"] = (object)this.lblClave.Text;
                this.Session["nombre"] = (object)this.lblNombre.Text;
                this.Session["cedis"] = (object)this.lblCedis.Text;
                this.Session["queja"] = (object)this.lblQueja.Text;
                this.Session["cliente"] = (object)this.lblCliente.Text;
                this.Session["admin"] = (object)this.lblAdmin.Text;
                this.Response.Redirect("reg_efectividad.aspx");
            }
            else
                this.Response.Write("<script>alert('Faltan acciones por verificar');</script>");
        }

        protected void btnEdicion_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("editar_queja.aspx");
        }

        protected void correo(string queja, string clien)
        {
            string str1 = "http://189.206.160.206:81/quejas/";
            string str2 = "http://gabira1:81/quejas/";
            string date = DateTime.Now.ToString();
            string date2 = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            string str3 = "<table border='2'><tr><td align='center'><h2>Cierre de Queja</h2></td></tr><tr><td>Queja no.: " + queja + "</td></tr><tr><td>Cliente: " + clien + "</td></tr><tr><td>Fecha de cierre: " + date2 + "</td></tr></table><p>Entrar al sistema de quejas</p><br />Enlace dentro de instalaciónes de Comercializadora GAB: " + str2 + "<br />Enlace fuera de instalaciónes de Comercializadora GAB: " + str1;
            MailMessage message = new MailMessage();
            message.To.Add("msamano@mrlucky.com.mx");
            message.Bcc.Add("aescamilla@mrlucky.com.mx");
            message.Subject = "Queja no.: " + queja;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = str3;
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
                this.Response.Write("<script>alert('No fue enviado el correo electronico')</script>");
            }
        }

        protected void btnEditarPed_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("editar_queja_exp.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.con.cierre_queja_verificacion(this.lblQueja.Text) == "1")
            {
                this.con.cierre_queja2(this.lblQueja.Text);
                this.lblSuccess.Visible = true;
                this.upnMensajes.Update();
                this.correo(this.lblQueja.Text, this.lblCliente.Text);
            }
            else
            {
                this.lblError.Visible = true;
                this.upnMensajes.Update();
            }
        }


        protected void btnNotaCredito_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("notas_credito.aspx");
        }

        protected void btnSoporte_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = (object)this.lblCliente.Text;
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("soporte.aspx");

            //
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            string[] doc = hflSoporte.Value.ToString().Split('/');
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script", "<script>window.open('http://189.206.160.206:81/quejas/soportes/" + doc[doc.Length - 1].ToString() + "', '_blank');</script>", false);
        }
    }
}