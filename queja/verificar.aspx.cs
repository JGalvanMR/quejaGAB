using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

namespace queja
{
    public partial class verificar : System.Web.UI.Page
    {
        private conectasql con = new conectasql();
        private string respo = "";
        private string val1 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.txtClave.Text = this.Session["clave_acc"].ToString();
            this.val1 = this.Session["resp"].ToString();
            this.txtResponsable.Text = this.Session["nom_resp"].ToString();
            this.txtAccion.Text = this.Session["accion"].ToString();
            this.txtFecha.Text = this.Session["termino"].ToString();
            if (this.Page.IsPostBack)
                return;
            this.respo = this.con.cargadatosrespaccion(this.lblQueja.Text, this.val1);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.img1.HasFile && this.img1.PostedFile.ContentLength > 4194304)
                this.Response.Write("<script>alert('El tamaño de la imagen 1 es superior a 4Mb favor de verificarlo')</script>");
            else if (this.img2.HasFile && this.img2.PostedFile.ContentLength > 4194304)
                this.Response.Write("<script>alert('El tamaño de la imagen 2 es superior a 4Mb favor de verificarlo')</script>");
            else if (this.img3.HasFile && this.img3.PostedFile.ContentLength > 4194304)
            {
                this.Response.Write("<script>alert('El tamaño de la imagen 3 es superior a 4Mb favor de verificarlo')</script>");
            }
            else
            {
                string[] strArray = new string[5]
        {
          "jpg",
          "jpeg",
          "bmp",
          "png",
          "gif"
        };
                if (this.img1.HasFile)
                {
                    bool flag = true;
                    string fileName = this.img1.FileName;
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
                        this.Response.Write("<script>alert('El elemento 1 no es una imagen')</script>");
                        return;
                    }
                }
                if (this.img2.HasFile)
                {
                    bool flag = true;
                    string fileName = this.img2.FileName;
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
                        this.Response.Write("<script>alert('El elemento 2 no es una imagen')</script>");
                        return;
                    }
                }
                if (this.img3.HasFile)
                {
                    bool flag = true;
                    string fileName = this.img1.FileName;
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
                        this.Response.Write("<script>alert('El elemento 3 no es una imagen')</script>");
                        return;
                    }
                }
                string str1 = "\\\\gabira1\\fotos_des\\";
                this.respo = this.con.cargadatosrespaccion(this.lblQueja.Text, this.txtClave.Text);
                if ("" == "1")
                {
                    if (Convert.ToDateTime(this.txtFecha.Text) > DateTime.Now)
                        this.con.actualizaetapa4(this.lblQueja.Text, "1");
                    else
                        this.con.actualizaetapa4(this.lblQueja.Text, "2");
                    if (this.img1.HasFile)
                        verificar.ScaleImage((System.Drawing.Image)new Bitmap(this.img1.PostedFile.InputStream), 405).Save(str1 + "1_" + this.lblQueja.Text + "_" + this.txtClave.Text + ".jpg", ImageFormat.Jpeg);
                    if (this.img2.HasFile)
                        verificar.ScaleImage((System.Drawing.Image)new Bitmap(this.img2.PostedFile.InputStream), 405).Save(str1 + "2_" + this.lblQueja.Text + "_" + this.txtClave.Text + ".jpg", ImageFormat.Jpeg);
                    if (this.img3.HasFile)
                        verificar.ScaleImage((System.Drawing.Image)new Bitmap(this.img3.PostedFile.InputStream), 405).Save(str1 + "3_" + this.lblQueja.Text + "_" + this.txtClave.Text + ".jpg", ImageFormat.Jpeg);
                    this.Response.Write("<script>alert('Datos Guardados Correctamente')</script>");
                }
                else
                    this.Response.Write("<script>alert('Datos no Guardados')</script>");
            }
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            double num = (double)maxHeight / (double)image.Height;
            int width = (int)((double)image.Width * num);
            int height = (int)((double)image.Height * num);
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage((System.Drawing.Image)bitmap))
                graphics.DrawImage(image, 0, 0, width, height);
            return (System.Drawing.Image)bitmap;
        }

        protected bool verificararchivo(object source, ServerValidateEventArgs args)
        {
            string str = args.Value;
            args.IsValid = false;
            bool flag;
            if ((double)this.img1.FileContent.Length > 4096.0)
            {
                args.IsValid = false;
                flag = false;
            }
            else
            {
                args.IsValid = true;
                flag = true;
            }
            return flag;
        }
    }
}