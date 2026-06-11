<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="queja._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
        /*var iddleTimeout = null;
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
        }*/

        function getAbsolutePath() {
            var loc = window.location;
            var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
            return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
        }

        function analisis() {
            /*var str = getAbsolutePath();
            //alert(str);
            if (str.indexOf("gabira1") != -1) {
                lan(); 
            }
            if (str.indexOf("189.206.160.206") != -1) {
                web();
            }*/
            window.open("http://189.206.160.206:81/quejas/rechazos/quejas_dashboard.html", "_black"); 
        }

        function lan() {
            //window.location.href = "http://gabira1:81/estadisticas/default.htm";
            window.open("http://gabira1:81/estadisticas/default.htm", "_blank");
        }

        function web() {
            window.open("http://189.206.160.206:81/estadisticas/default.htm", "_blank");
            //window.location.href = "http://189.206.160.206:81/estadisticas/default.htm";
        }
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
    </style>
</head>
<body>
<div class="container">
    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <center><h1><strong class="">Quejas Clientes</strong></h1></center>
                </div>
                <form runat="server" id="frmLogin" class="form-horizontal">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                    <br />
                    <div class="form-group">
                        <center>
                            <img src="imagenes/Logo grupo U.png" height="300" width="250" alt="imagen"/>
                        </center>
                    </div>
                    <div class="form-group form-group-lg">
                        <div class="col-sm-8 col-sm-offset-2">
                            <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" Style="text-transform: uppercase" placeholder="USUARIO"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group form-group-lg">
                        <div class="col-sm-8 col-sm-offset-2">
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" Style="text-transform: uppercase" placeholder="CONTRASEÑA"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group form-group-lg">
                        <div class="col-sm-8 col-sm-offset-2">
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0" Selected="True">PRODUCTO</asp:ListItem>
                                <asp:ListItem Value="1">SERVICIO</asp:ListItem>
                                <asp:ListItem Value="2">FUMIGACION</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ErrorMessage="Debe elegir una opción" ControlToValidate="ddlTipo" ForeColor="Red" InitialValue="" ValidationGroup="grupo1">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group form-group-lg">
                        <div class="col-sm-offset-5 col-sm-10 col-xs-offset-4 col-xs-10">
                          <asp:Button runat="server" ID="btnEntrar" Text="Entrar" 
                                CssClass="btn btn-primary btn-lg" ValidationGroup="grupo1" onclick="btnEntrar_Click" />
                        </div>
                    </div>
                    <div class="form-group form-group-lg">
                        <div class="col-sm-8 col-sm-offset-2">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblDanger" CssClass="alert alert-danger col-sm-12" role="alert" Font-Bold="True" 
                                        Text="Usuario no válido, intentelo nuevamente" 
                                        runat="server" Visible = "False"/>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnEntrar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-md-offset-10">
                            <a onclick="analisis()" style="cursor:pointer"><img width="100"  src="imagenes/Analytics-128.png" class="img-rounded img-responsive" alt="Responsive image" /></a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
</body>
</html>
