<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportes.aspx.cs" Inherits="queja.reportes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="jqueryui/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>

    <script src="jqueryui/jquery-ui.js" type="text/javascript"></script>
    <script src="jqueryui/datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaInicio.ClientID %>").datepicker({
                /*minDate: 0,
                maxDate: 20*/
            });
            $("#<%= txtFechaFin.ClientID %>").datepicker({
                /*minDate: 0,
                maxDate: 20*/
            });
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
        <div class="col-md-12">
            <form id="frmPrincipal" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                </asp:ScriptManager>
                
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Detalles Queja</strong></h3></center>
                    </div>
                    <div  class="panel-body">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                        <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                        <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                        <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-offset-0 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1478220216_analytics.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte de quejas por semana</h3>
                                                <p>Detalle de las quejas recibidas por CEDIS</p>
                                                <p>
                                                    <asp:Button ID="btnCedis" runat="server" CssClass="btn btn-primary" 
                                                        Text="Entrar" onclick="btnCedis_Click" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/Analytics-128.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte de quejas por semana</h3>
                                                <p>Detalle de las quejas recibidas por área</p>
                                                <p>
                                                    <asp:Button ID="btnArea" runat="server" CssClass="btn btn-primary" 
                                                        Text="Entrar" onclick="btnArea_Click" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1478220457_graphs.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte de cajas por mes</h3>
                                                <p>Detalle de la cantidad de cajas rechazadas por mes</p>
                                                <p>
                                                    <asp:Button ID="btnMes" runat="server" CssClass="btn btn-primary" 
                                                        Text="Entrar" onclick="btnMes_Click" />    
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1478220457_graphs.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte de cajas por semana</h3>
                                                <p>Detalle de la cantidad de cajas rechazadas por semana</p>
                                                <p>
                                                    <asp:Button ID="btnSemana" runat="server" CssClass="btn btn-primary" 
                                                        Text="Entrar" onclick="btnSemana_Click"  />    
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-12">
                                        <div class="thumbnail">
                                            <img src="imagenes/Excel.png" alt="Reporte" height="150" width="150"/>
                                            <div class="caption">
                                                <h3>Reporte General Excel</h3>
                                                <p>Reporte detallado de quejas</p>
                                                <p>
                                                    <asp:Button ID="btnGeneral" runat="server" CssClass="btn btn-primary" 
                                                        Text="Generar Reporte" onclick="btnGeneral_Click"  />    
                                                        <asp:Button runat="server" ID="Button1" Text="Report" 
                                                            CssClass="btn btn-primary" onclick="Button1_Click1"  Visible="False" />
                                                </p>
                                                <p class="form-inline">
                                                    Fechas:
                                                    <asp:TextBox ID="txtFechaInicio" MaxLength="10" runat="server" data-provide="datepicker" 
                                                        CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    De:
                                                    <asp:TextBox ID="txtFechaFin" MaxLength="10" runat="server" data-provide="datepicker" 
                                                        CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/Analytics-128.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte de rechazo</h3>
                                                <p>Detalle de cajas rechazadas</p>
                                                <p>
                                                    <asp:Button ID="btnGraficas" runat="server" CssClass="btn btn-primary" 
                                                        Text="Entrar" onclick="btnGraficas_Click"  />    
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/pngwing.com.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte de Cajas Producidas</h3>
                                                <p>Detalle de cajas producidas vs rechazadas Producto Terminado</p>
                                                <p>
                                                    <asp:Button ID="btnCajasProducidas" runat="server" CssClass="btn btn-primary" 
                                                        Text="Entrar" onclick="btnCajasProducidas_Click"  />    
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-12">
                                        <div class="thumbnail">
                                            <img src="imagenes/rejected.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte por Producto</h3>
                                                <p>Detalle de cajas rechazadas por producto</p>
                                                <p>
                                                    <asp:Button ID="btnProducto" runat="server" CssClass="btn btn-primary" 
                                                        Text="Entrar" onclick="btnProducto_Click"   />    
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
        
    </div>
</body>
</html>

