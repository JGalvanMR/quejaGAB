using System.IO;
using System.Web;

namespace queja
{
    public static class FotosQueja
    {
        // El alta por lote conserva la extensión original (.jpg, .jpeg, .png); el alta por pedido siempre guarda .jpg
        private static readonly string[] Extensiones = { ".jpg", ".jpeg", ".png" };

        // Devuelve la URL relativa de la foto {numero}_{folio}.ext dentro de la carpeta indicada (fotos_ant por defecto)
        public static string Url(HttpServerUtility server, int numero, string folio, string carpeta = "fotos_ant")
        {
            string ruta = server.MapPath("~/" + carpeta + "/");
            foreach (string ext in Extensiones)
            {
                if (File.Exists(Path.Combine(ruta, numero + "_" + folio + ext)))
                    return carpeta + "/" + numero + "_" + folio + ext;
            }
            return carpeta + "/" + numero + "_" + folio + ".jpg";
        }
    }
}
