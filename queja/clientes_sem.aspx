<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clientes_sem.aspx.cs" Inherits="queja.clientes_sem" %>

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

    <script src="Scripts/tableExport.js" type="text/javascript"></script>
    <script src="Scripts/jquery.base64.js" type="text/javascript"></script>

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
<script language="javascript">
    $(function () {
        $('#container').highcharts({
            data: {
                table: 'datatable'
            },
            chart: {
                type: 'column'
            },
            title: {
                text: 'Cantidad de quejas por semana por CEDIS'
            },
            yAxis: {
                allowDecimals: false,
                title: {
                    text: 'Quejas'
                }
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + ' ' + this.point.name.toLowerCase();
                }
            }

        });
        $("#datatable").hide();
        
    });
    function exportarexcel() {
        $('#datatable1').tableExport({ type: 'excel', escape: 'false' });
    }
    
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
<style>
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
    <div class="container col-md-12">
        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
            <form id="form1" runat="server">
                <div  class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Reporte Semanal de Quejas por CEDIS</strong></h3></center>
                    </div>
                    <div class="panel panel-body">
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
                                    <asp:Button runat="server" ID="btnExport" Text="Exportar tabla a Excel" 
                                        CssClass="btn btn-success" onclick="btnExport_Click" />
                                    <asp:Button runat="server" ID="btnTodos" Text="Ver todos los CEDIS" 
                                        CssClass="btn btn-success" onclick="btnTodos_Click" />
                                    <asp:Button runat="server" ID="btnCedis" Text="Ver CEDIS" 
                                        CssClass="btn btn-success" onclick="btnCedis_Click" />
                                    <asp:Button runat="server" ID="btnExcelTodos" Text="Exportar CEDIS Excel" 
                                        CssClass="btn btn-success" onclick="btnExcelTodos_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <center>    
                                <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                <div id="datosqueja" runat="server"></div>
                                <div id="datosqueja1" runat="server"></div>
                            </center>
                        </div>
                    </div>
                    
                </div>
            </form>
        </div>
    </div>
</body>
</html>
