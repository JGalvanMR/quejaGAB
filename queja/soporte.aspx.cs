using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace queja
{
    public partial class soporte : System.Web.UI.Page
    {
        private conectasql con = new conectasql();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.Session["cliente"] = this.Session["cliente"];
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.fupSoporte.HasFile && this.fupSoporte.PostedFile.ContentLength > 4194304)
            {
                this.Response.Write("<script>alert('El tamaño del archivo es superior a 4Mb favor de verificarlo')</script>");
                return;
            }

            string[] strArray = new string[1]
            {
              "pdf",
            };

            if (this.fupSoporte.HasFile)
            {
                bool flag = true;
                string fileName = this.fupSoporte.FileName;
                string lower = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                foreach (string str in strArray)
                {
                    if (str != lower)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.Response.Write("<script>alert('El elemento seleccionado no es un archivo PDF')</script>");
                    return;
                }

                string str1 = "";
                str1 = "~/soportes/";
                str1 = Server.MapPath(str1);
                var existe = Directory.Exists(str1);
                if (!Directory.Exists(str1))
                {
                    Directory.CreateDirectory(str1);
                }

                if (this.fupSoporte.HasFile)
                {
                    this.fupSoporte.SaveAs(str1 + "Soporte_" + this.lblQueja.Text + ".pdf");
                    if (con.queja_soporte_devolucion_merma(lblQueja.Text, str1 + "Soporte_" + this.lblQueja.Text + ".pdf") == "1")
                    {
                        this.Response.Write("<script>alert('Imagenes cargadas correctamente')</script>");
                    }
                }
            }

            
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Session["clave"] = (object)this.lblClave.Text;
            this.Session["nombre"] = (object)this.lblNombre.Text;
            this.Session["cedis"] = (object)this.lblCedis.Text;
            this.Session["queja"] = (object)this.lblQueja.Text;
            this.Session["cliente"] = this.Session["cliente"];
            this.Session["admin"] = (object)this.lblAdmin.Text;
            this.Response.Redirect("contenido.aspx");
        }
    }
}