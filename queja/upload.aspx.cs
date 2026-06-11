using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace queja
{
    public partial class upload : System.Web.UI.Page
    {
        private string accion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblClave.Text = this.Session["clave"].ToString();
            this.lblNombre.Text = this.Session["nombre"].ToString();
            this.lblCedis.Text = this.Session["cedis"].ToString();
            this.lblQueja.Text = this.Session["queja"].ToString();
            this.lblAdmin.Text = this.Session["admin"].ToString();
            //if (!(this.Request.QueryString["accion"] == "1"))
            //    return;
            this.accion = "1";


        }

        protected void btnSubir_Click(object sender, EventArgs e)
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
                string[] strArray = new string[6]
        {
          "jpg",
          "jpeg",
          "bmp",
          "png",
          "gif",
          "pdf"
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
                        this.Response.Write("<script>alert('El elemento 1 no es una imagen o un archivo PDF')</script>");
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
                        this.Response.Write("<script>alert('El elemento 2 no es una imagen o un archivo PDF')</script>");
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
                        this.Response.Write("<script>alert('El elemento 3 no es una imagen o un archivo PDF')</script>");
                        return;
                    }
                }
                //"~/FotoRevisionTrailer/";
                //string str1 = !(this.accion == "1") ? "\\\\192.168.123.6\\fotos_ant\\" : "\\\\192.168.123.6\\fotos_des\\";
                string str1 = "";
                if (this.accion == "1")
                {
                    str1 = "~/fotos_ant/";
                    str1 = Server.MapPath(str1);
                    var existe = Directory.Exists(str1);
                    if (!Directory.Exists(str1))
                    {
                        Directory.CreateDirectory(str1);
                    }
                }
                else
                {
                    str1 = "~/fotos_des/";
                    str1 = Server.MapPath(str1);
                    var existe = Directory.Exists(str1);
                    if (!Directory.Exists(str1))
                    {
                        Directory.CreateDirectory(str1);
                    }
                }
                //string str1 = !(this.accion == "1") ? "~/fotos_ant/" : "~/fotos_des/";
                if (this.img1.HasFile)
                {
                    if (this.img1.FileName.Contains(".pdf"))
                        this.img1.SaveAs(str1 + "1_" + this.lblQueja.Text + ".pdf");
                    else
                        upload.ScaleImage((System.Drawing.Image)new Bitmap(this.img1.PostedFile.InputStream), 405).Save(str1 + "1_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                }

                if (this.img2.HasFile)
                {
                    if (this.img2.FileName.Contains(".pdf"))
                        this.img2.SaveAs(str1 + "2_" + this.lblQueja.Text + ".pdf");
                    else
                        upload.ScaleImage((System.Drawing.Image)new Bitmap(this.img2.PostedFile.InputStream), 405).Save(str1 + "2_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                }

                if (this.img3.HasFile)
                {
                    if (this.img3.FileName.Contains(".pdf"))
                        this.img3.SaveAs(str1 + "3_" + this.lblQueja.Text + ".pdf");
                    else
                        upload.ScaleImage((System.Drawing.Image)new Bitmap(this.img3.PostedFile.InputStream), 405).Save(str1 + "3_" + this.lblQueja.Text + ".jpg", ImageFormat.Jpeg);
                }

                this.Response.Write("<script>alert('Imagenes cargadas correctamente')</script>");
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