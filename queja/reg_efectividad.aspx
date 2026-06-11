<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reg_efectividad.aspx.cs" Inherits="queja.reg_efectividad" %>

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
    <script type="text/javascript">
    $(".modal-transparent").on('show.bs.modal', function () {
        setTimeout(function () {
            $(".modal-backdrop").addClass("modal-backdrop-transparent");
        }, 0);
        });
        $(".modal-transparent").on('hidden.bs.modal', function () {
            $(".modal-backdrop").addClass("modal-backdrop-transparent");
        });

        $(".modal-fullscreen").on('show.bs.modal', function () {
            setTimeout(function () {
                $(".modal-backdrop").addClass("modal-backdrop-fullscreen");
            }, 0);
        });
        $(".modal-fullscreen").on('hidden.bs.modal', function () {
            $(".modal-backdrop").addClass("modal-backdrop-fullscreen");

        $(".modal-fullscreen2").on('show.bs.modal', function () {
        setTimeout(function () {
            $(".modal-backdrop").addClass("modal-backdrop-fullscreen");
        }, 0);
        });
        $(".modal-fullscreen2").on('hidden.bs.modal', function () {
            $(".modal-backdrop").addClass("modal-backdrop-fullscreen");
    });
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
    </script>
    <style type="text/css">
    body
        {
            /*background-image: url(imagenes/fondo_7.png);
            background-position: center center;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: cover;
            background-color: #464646;
            font-size: 11px;*/
            
            }
    .tamano
        {
            resize: none;
        }
    .hiddencol
        {
            display: none;
        }
    @media print {
        .img {
              display: none;
        }
    }
    .color {
        background-color: #4CAF50;
        color: white;
    }
    
    .modal-fullscreen {
          background: transparent;
        }
        .modal-fullscreen .modal-content {
          background: transparent;
          border: 0;
          -webkit-box-shadow: none;
          box-shadow: none;
        }
        .modal-backdrop.modal-backdrop-fullscreen {
          background: #ffffff;
        }
        .modal-backdrop.modal-backdrop-fullscreen.in {
          opacity: .97;
          filter: alpha(opacity=97);
        }
        .modal-fullscreen .modal-dialog {
          margin: 0;
          margin-right: auto;
          margin-left: auto;
          width: 100%;
        }
        .modal-title
        {
            color: #ffffff;
            }
        @media (min-width: 768px) {
          .modal-fullscreen .modal-dialog {
            width: 750px;
          }
        }
        @media (min-width: 992px) {
          .modal-fullscreen .modal-dialog {
            width: 970px;
          }
        }
        @media (min-width: 1200px) {
          .modal-fullscreen .modal-dialog {
             width: 1170px;
          }
        }
        
        .carousel-inner > .item > img,
          .carousel-inner > .item > a > img {
              width: 70%;
              margin: auto;
          }
          
         /*modal de fotos de despues*/ 
          .modal-fullscreen2 {
          background: transparent;
        }
        .modal-fullscreen2 .modal-content2 {
          background: transparent;
          border: 0;
          -webkit-box-shadow: none;
          box-shadow: none;
        }
        .modal-backdrop.modal-backdrop-fullscreen {
          background: #ffffff;
        }
        .modal-backdrop.modal-backdrop-fullscreen.in {
          opacity: .97;
          filter: alpha(opacity=97);
        }
        .modal-fullscreen2 .modal-dialog2 {
          margin: 0;
          margin-right: auto;
          margin-left: auto;
          width: 100%;
        }
        .modal-title2
        {
            color: #ffffff;
            }
        @media (min-width: 768px) {
          .modal-fullscreen2 .modal-dialog2 {
            width: 750px;
          }
        }
        @media (min-width: 992px) {
          .modal-fullscreen2 .modal-dialog2 {
            width: 970px;
          }
        }
        @media (min-width: 1200px) {
          .modal-fullscreen2 .modal-dialog2 {
             width: 1170px;
          }
        }
    </style>
</head>
<body>
    <div class="jumbotron text-center">
        <h1>Registro de Efectividad</h1>
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
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <div class="row">
                <div class="form-group">
                    <div class="col-lg-12">
                        <asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
                            CssClass="btn btn-primary" onclick="btnVolver_Click" />
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <center>
                    <div id="datosqueja" runat="server">              
                    </div>
                </center>
            </div>
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h4 class="panel-title">Registro de Efectividad</h4>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="updEfectividad" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group">
                                    <label for="txtFechaEfe" class="col-sm-2 control-label">Fecha</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtFechaEfe" runat="server" CssClass="form-control" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label for="ddlCumplimiento" class="col-sm-2 control-label">Cumplimiento</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlCumplimiento" runat="server" ViewStateMode="Enabled" CssClass="form-control" ValidationGroup="grupo1">
                                            <asp:ListItem Value="">SELECCIONE UNA OPCION...</asp:ListItem>
                                            <asp:ListItem Value="SI">SI</asp:ListItem>
                                            <asp:ListItem Value="NO">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <asp:RequiredFieldValidator id="rfvCumplimiento" ControlToValidate="ddlCumplimiento" ErrorMessage="Seleccione un elemento" Display="Static"
                                        InitialValue="" runat="server" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label for="txtComentario" class="col-sm-2 control-label">Comentario</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtComentario" MaxLength="150" TextMode="MultiLine" runat="server" 
                                            CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ValidationGroup="grupo1"></asp:TextBox> 
                                        <asp:RequiredFieldValidator ID="rfvComentario" ControlToValidate="txtComentario" ErrorMessage="Ingrese el comentario" runat="server" ForeColor="Red" ValidationGroup="grupo1">
                                        </asp:RequiredFieldValidator>   
                                    </div>
                                </div>
                            </div>  
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="form-group">
                            <center>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:UpdatePanel ID="upnGuardar" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                            ValidationGroup="grupo1" CssClass="btn btn-primary" 
                                                onclick="btnGuardar_Click"   />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br /><br />
                                    <asp:UpdatePanel ID="upnCancelar" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnCancelar" Text="Limpiar"
                                            CssClass="btn btn-primary" Visible="False" onclick="btnCancelar_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <center>
                                <div class="col-sm-12">
                                    <asp:UpdatePanel ID="upnMensajes" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lblSuccess" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                Text="Verificación guardada y correo(s) enviado(s)" runat="server" Visible = "false" />
                                            <asp:Label ID="lblWarning" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                Text="Verificación no guardada, intentelo nuevamente" 
                                                runat="server" Visible = "False"/>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                        <ProgressTemplate>
                                            <div class="alert alert-info">
                                                <strong>GUARDANDO...</strong>Se esta registrando la informaci&oacute;n
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal fullscreen -->
            <div class="modal modal-fullscreen fade" id="modal-fullscreen" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Vista de imagenes</h4>
                    </div>
                    <div class="modal-body">
                    <center>
                        <div class="container">
                            <br>
                            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                            <!-- Indicators -->
                            <ol class="carousel-indicators">
                                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                <li data-target="#myCarousel" data-slide-to="1"></li>
                                <li data-target="#myCarousel" data-slide-to="2"></li>
                            </ol>

                            <!-- Wrapper for slides -->
                            <div class="carousel-inner" role="listbox">
                                <div class="item active">
                                <!-- img id="imagen1" alt="No disponible" width="460" height="345" /-->
                                <asp:Image ID="Image1" runat="server" CssClass="img-responsive"/>
                            
                                </div>

                                <div class="item">
                                <!-- img id="imagen2" alt="No disponible" width="460" height="345" -->
                                <asp:Image ID="Image2" runat="server" CssClass="img-responsive"/>
                            
                                </div>
    
                                <div class="item">
                                <!-- img id="imagen3" alt="No disponible" width="460" height="345" -->
                                <asp:Image ID="Image3" runat="server" CssClass="img-responsive"/>
                                </div>
                            </div>

                            <!-- Left and right controls -->
                            <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                                <span style="width: 90px; height: 90px; background-image: url(imagenes/appbar.chevron.left.png); display:block;" />
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                                <span style="width: 90px; height: 90px; background-image: url(imagenes/appbar.chevron.right.png); display:block;" />
                                <span class="sr-only">Next</span>
                            </a>
                            </div>
                        </div>
                    </center>
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
                </div>
            </div>
        </form>
        
    </div>
    
</body>
</html>
