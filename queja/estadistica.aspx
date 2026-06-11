<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="estadistica.aspx.cs" Inherits="queja.estadistica" %>

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

    <script src="css/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css">

<script language="javascript">
    $(function () {
        var chart = Highcharts.chart('container', {
                data: {
                    table: 'datatable2'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Cajas Rechazadas por Producto y Año'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Cajas'
                    }
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }/*,
                plotOptions: {
                    series: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return humanizeNumber(this.y)
                            }
                        }
                    }
                }*/
            });
        $("#datatable2").hide();
    });
    /*function exportarexcel() {
        $('#datatable1').tableExport({ type: 'excel', escape: 'false' });
    }*/
    
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

        $(document).ready(function() {
            /*$('#multiple-checkboxes').multiselect({
                //includeSelectAllOption: true,
                buttonWidth: '100%'
            });*/
            $('#<%= lstProductos.ClientID %>').multiselect({
                //includeSelectAllOption: true,
                enableFiltering: true,
                buttonWidth: '100%',
                maxHeight: 500,
                dropRight: true,
                buttonClass: 'btn btn-button btn-primary'
            });
            $('#<%= lstMeses.ClientID %>').multiselect({
                buttonWidth: '100%',
                maxHeight: 500,
                dropRight: true,
                buttonClass: 'btn btn-button btn-primary'
            });
        });

        

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
     #container {
        height: 400px;
    }  
    .highcharts-figure, .highcharts-data-table table {
        min-width: 310px;
        max-width: 800px;
        margin: 1em auto;
    }
    
    .esconde
    {
        display: none;
        }
</style>
</head>
<body>
    
    <div class="container col-md-12">
        <div class="col-sm-offset-1 col-md-10 col-lg-10 col-sm-10 col-xs-10">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" >
            </asp:ScriptManager>
                <div  class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Reporte de Comparativo de Cajas Rechadas por Año y Producto</strong></h3></center>
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
                                        CssClass="btn btn-primary" onclick="btnVolver_Click"   />
                                    <%--<asp:Button unat="server" ID="btnExport" Text="Exportar tabla a Excel" 
                                        CssClass="btn btn-success"  />
                                    <asp:Button unat="server" ID="btnTodos" Text="Ver todos los CEDIS" 
                                        CssClass="btn btn-success"  />
                                    <asp:Button unat="server" ID="btnCedis" Text="Ver CEDIS" 
                                        CssClass="btn btn-success"  />
                                    <asp:Button unat="server" ID="btnExcelTodos" Text="Exportar CEDIS Excel" 
                                        CssClass="btn btn-success"  />--%>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group">
                                <label for="lstProductos" class="col-sm-2 control-label text-right">Producto</label>
                                <div class="col-sm-8">
                                    <%--<select id="multiple-checkboxes" multiple="multiple" class="form-control multiselect-container">
                                        <option value="php">PHP</option>
                                        <option value="javascript">JavaScript</option>
                                        <option value="java">Java</option>
                                        <option value="sql">SQL</option>
                                        <option value="jquery">Jquery</option>
                                        <option value=".net">.Net</option>
                                        <asp:ListItem Text="Nikunj Satasiya" Value="1" />
                                        <asp:ListItem Text="Ronak Rabadiya" Value="2" />
                                        <asp:ListItem Text="Hiren Dobariya" Value="3" />
                                        <asp:ListItem Text="Vivek Ghadiya" Value="4" />
                                        <asp:ListItem Text="Pratik Pansuriya" Value="5" />
                                        <asp:ListItem Text="Kishan Patel" Value="6" />
                                    </select>--%>
                                    <asp:UpdatePanel ID="udpProductos" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstProductos" CssClass="dropdown dropdown-menu" runat="server" SelectionMode="Multiple" >
                                            </asp:ListBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        
                        <br />
                        <div class="row">
                            <div class="form-group">
                                <label for="lstMeses" class="col-sm-2 control-label text-right">Mes</label>
                                <div class="col-sm-8">
                                <asp:UpdatePanel ID="udpMeses" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstMeses" CssClass="dropdown dropdown-menu" runat="server" SelectionMode="Multiple" >
                                            <asp:ListItem Text="ENERO" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="FEBRERO" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="MARZO" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="ABRIL" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="MAYO" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="JUNIO" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="JULIO" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="AGOSTO" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="SEPTIEMBRE" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="OCTUBRE" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="NOVIEMBRE" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="DICIEMBRE" Value="12"></asp:ListItem>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group">
                                <label for="lstProductos" class="col-sm-2 control-label text-right">Problemas</label>
                                <div class="col-sm-8">
                                    <asp:UpdatePanel ID="udpProblemas" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlProblemas" runat="server" AutoPostBack="true" CssClass="form-control form">  
                                            </asp:DropDownList> 
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <br />
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-offset-4 col-sm-4">
                                        <asp:Button runat="server" ID="btnAceptar" Text="Generar Reporte" 
                                            CssClass="btn btn-primary" onclick="btnAceptar_Click"   />
                                        <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar seleccion" 
                                            CssClass="btn btn-primary" onclick="btnLimpiar_Click"  />
                                    </div>
                                </div>
                            </div>
                        <br />
                        <div class="row">
                            <center>
                                <div id="container" name="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                <div id="datosqueja" name="datosqueja" runat="server"></div>
                                <div id="datosqueja1" name="datosqueja1" runat="server" class="esconde"></div>
                            </center>
                        </div>
                    </div>
                    
                </div>
            </form>
        </div>
    </div>
</body>
</html>
