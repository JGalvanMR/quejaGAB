using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

namespace queja
{
    public partial class descargar : System.Web.UI.Page
    {
        private DataTable dtArchivos = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.dtArchivos.Columns.Add("archivo", typeof(string));
            string[] files = Directory.GetFiles("\\\\gabira1\\fotos_ant\\");
            List<ListItem> listItemList = new List<ListItem>();
            foreach (string path in files)
            {
                listItemList.Add(new ListItem(Path.GetFileName(path), path));
                DataRow row = this.dtArchivos.NewRow();
                row["archivo"] = (object)Path.GetFileName(path);
                this.dtArchivos.Rows.Add(row);
            }
            this.gvwOrden.DataSource = (object)this.dtArchivos;
            this.gvwOrden.DataBind();
        }

        private void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "Attachment; Filename=" + sFileName);
            HttpContext.Current.Response.WriteFile(new FileInfo(sFilePath).FullName);
            HttpContext.Current.Response.End();
        }

        protected void gvwOrden_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "descargar"))
                return;
            string str = "\\\\gabira1\\fotos_ant\\";
            string sFileName = this.Server.HtmlDecode(this.gvwOrden.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
            string sFilePath = str + sFileName;
            this.Download(sFileName, sFilePath);
        }
    }
}