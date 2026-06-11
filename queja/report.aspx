<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="queja.report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>


    </script>
    <script type="text/javascript">
        var iddleTimeout = null;
        function pageLoad() {
            if (iddleTimeout != null)
                clearTimeout(iddleTimeout);

            var millisecTimeout = <%= int.Parse(System.Configuration.ConfigurationManager.AppSettings["SessionTimeout"]) * 60 * 1000 %>;

            iddleTimeout = setTimeout("TimeoutPage()", millisecTimeout);
        }

        function TimeoutPage() {
            var str = getAbsolutePath();
            if (str.indexOf("localhost") != -1)
                location.href = str + "default.aspx";
            if (str.indexOf("gabira1") != -1)
                location.href = str;
            if (str.indexOf("189.206.160.206") != -1)
                location.href = str;
        }

        function getAbsolutePath() {
            var loc = window.location;
            var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
            return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
        }

    </script>
    <style type="text/css">
        body {
            /*background-image: url(imagenes/fondo_7.png);
            background-position: center center;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: cover;
            background-color: #464646;
            font-size: 11px;*/
        }

        .tamano {
            resize: none;
        }

        .hiddencol {
            display: none;
        }
        /*@media print and (color){
        .img {
              display: none;
        }
        * 
        {
            -webkit-print-color-adjust: exact;
            print-color-adjust: exact;
            }  
            
    }*/
        .color {
            background-color: #4CAF50; /*VERDE*/
            color: white;
        }

        .color2 {
            background-color: #337AB7; /*AZUL*/
            color: white;
        }
        /*javascript:window.print();*/
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="jumbotron text-center">
            <h1>Reporte de Quejas</h1>
        </div>
        <div class="container col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <h5>
                        <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                        <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                        <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                        <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Text="Queja: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                        <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                        <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                    </h5>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <div class="col-lg-12">
                        <%--<asp:Button ruat="server" ID="btnVolver" Text="Regresar a quejas" 
									CssClass="btn btn-primary" onclick="btnVolver_Click"  />--%>
                    </div>
                </div>
            </div>
            <div class="row">
                <center>
                    <div id="contenidoPDF">
                        <div id="datosqueja" runat="server">
                        </div>
                    </div>
                </center>
            </div>
        </div>
        <br />
        <div class="row">
            <center>
                <div>
                    <asp:ImageButton ID="btnImprimir" CssClass="img" ImageUrl="~/imagenes/printer-128.png" runat="server" OnClick="btnImprimir_Click" />
                </div>
            </center>
        </div>
    </form>

</body>

</html>


