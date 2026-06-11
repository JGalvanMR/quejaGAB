<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="soporte.aspx.cs" Inherits="queja.soporte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
        var iddleTimeout = null;
        function pageLoad() {
            if (iddleTimeout != null)
                clearTimeout(iddleTimeout);

                var millisecTimeout = <%= int.Parse(System.Configuration.ConfigurationManager.AppSettings["SessionTimeout"]) * 60 * 1000 %>; 

                iddleTimeout = setTimeout("TimeoutPage()", millisecTimeout);
            }

        function TimeoutPage(){
            var str = getAbsolutePath();
            if(str.indexOf("localhost") != -1)
                location.href = str + "default.aspx";
            if(str.indexOf("gabira1") != -1)
                location.href = str;
            if(str.indexOf("189.206.160.206") != -1)
                location.href = str;
        }

        function getAbsolutePath() {
            var loc = window.location;
            var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
            return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
        }

        document.addEventListener("DOMContentLoaded", function() {
        // Obtener el control de carga de archivos usando ClientID
        var fileUpload = document.getElementById("<%= fupSoporte.ClientID %>");
        if (fileUpload) {
            // Vincular el evento onchange
            fileUpload.addEventListener("change", function() {
                const fileNameDisplay = document.getElementById("file-name-display");
                const fileName = fileUpload.files.length > 0 ? fileUpload.files[0].name : "Ningún archivo seleccionado";
                fileNameDisplay.textContent = fileName;
            });
        }
    });
    </script>
    <style type="text/css">
    body
        {
            background-image: url(imagenes/fondo_7.png);
            background-position: center center;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: cover;
            background-color: #464646;
            
            }
    .file-upload-wrapper {
        display: inline-block;
        position: relative;
        overflow: hidden;
        cursor: pointer;
    }

    .file-upload-wrapper input[type="file"] {
        position: absolute;
        left: 0;
        top: 0;
        opacity: 0;
        width: 100%;
        height: 100%;
        cursor: pointer;
    }

    .file-upload-btn {
        background-color: #4CAF50;
        color: #fff;
        padding: 10px 20px;
        border-radius: 5px;
        cursor: pointer;
        display: inline-block;
    }
    .file-name-display
    {
        margin-top: 10px;
        font-style: italic;
        color: #333;
        }
    </style>
    <title></title>
</head>
<body>
    <div class="container">
        <div class="col-md-12">
            <form id="frmPricipal" runat="server" enctype="multipart/form-data">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                </asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong>Soporte por Merma o Donaci&oacute;n</strong></h3></center>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <div class="col-sm-8">
                                <asp:Button runat="server" ID="btnVolver" Text="Regresar a queja" 
                                    CssClass="btn btn-primary" OnClick="btnVolver_Click"  />
                            </div>  
                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:UpdatePanel ID="updatePanel1" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <div class="file-upload-wrapper" CssClass="file-upload-input">
                                            <asp:FileUpload ID="fupSoporte" runat="server" class="file-name-display"  />
                                            <span class="file-upload-btn">Seleccionar archivo...</span>
                                        </div>
                                        <h3><span id="file-name-display">Ningun archivo seleccionado</span></h3>
                                        <center>
                                            <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                ValidationGroup="grupo1" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnGuardar" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
