<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="areas_sem.aspx.cs" Inherits="queja.areas_sem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />


    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>

    <script src="xlsx/xlsx.full.min.js" type="text/javascript"></script>
    <script src="xlsx/FileSaver.min.js" type="text/javascript"></script>

    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>
    
    <script src="jqueryui/jquery-ui.js" type="text/javascript"></script>

    <script src="Scripts/jquery.tabletojson.js" type="text/javascript"></script>

    <script src="Scripts/tableExport.js" type="text/javascript"></script>
    <script src="Scripts/jquery.base64.js" type="text/javascript"></script>

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>


    <script language="javascript">

        

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
    /*body
    {
        background-image: url(imagenes/fondo_7.png);
        background-position: center center;
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-size: cover;
        background-color: #464646;
            
        }*/
</style>
</head>
<body>
    <div class="jumbotron text-center">
        <h1>Reporte Semanal de Quejas por &Aacute;rea</h1>
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
                    <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                </h5>
            </div>
        </div>
        <form id="form1" runat="server">
            <div class="row">
                <div class="form-group">
                    <div class="col-sm-offset-0 col-sm-8">
                        <asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
                            CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                        <asp:Button runat="server" ID="btnExport" Text="Exportar tabla a Excel" 
                            CssClass="btn btn-success"  
                            onclick="btnExport_Click" />
                        <asp:Button runat="server" ID="btnTodos" Text="Ver todos los CEDIS" 
                            CssClass="btn btn-success" onclick="btnTodos_Click" />
                        <asp:Button runat="server" ID="btnCedis" Text="Ver CEDIS" 
                            CssClass="btn btn-success" onclick="btnCedis_Click" />
                        <asp:Button runat="server" ID="btnExportarTodos" Text="Exportar CEDIS Excel" 
                            CssClass="btn btn-success" onclick="btnExportarTodos_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <center>    
                    <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                    <div id="datosqueja" runat="server"></div>
                    <div id="datosqueja1" runat="server"></div>
                </center>
            </div>
            <input type="button" id="convert-table" value="Convertir" />
        </form>
        
    </div>
    <script type="text/javascript">
        

        var datosjson = "";

        $(function () {
            /*$('#container').highcharts({
            data: {
            table: 'datatable'
            },
            chart: {
            type: 'column'
            },
            title: {
            text: 'Cantidad de quejas por semana por Área'
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
            });*/

            var table = $('#datatable').tableToJSON(); // Convert the table into a javascript object
            //console.log(table);
            //alert(JSON.stringify(table));
            datosjson = table;
            //alert(datosjson);
            //document.write(JSON.stringify(datosjson));
            var cadena = "";
            $.each(datosjson, function (i, item) {
                if (datosjson[i].Semana == "TOTALES") {
                    //cadena += "{ name: '" + datosjson[i].Problema + "', data: [" + datosjson[i].Enero + ", " + datosjson[i].Febrero + ", " + datosjson[i].Marzo + ", " + datosjson[i].Abril + ", " + datosjson[i].Mayo + ", " + datosjson[i].Junio + ", " + datosjson[i].Agosto + ", " + datosjson[i].Septiembre + ", " + datosjson[i].Octubre + ", " + datosjson[i].Noviembre + ", " + datosjson[i].Diciembre + "]},";
                    cadena = datosjson[i].AJO + ", " + datosjson[i].ALMACEN_CEDIS + ", " + datosjson[i].CLIENTE + ", " + datosjson[i].COMPRAS_CEDIS + ", " + datosjson[i].COMPRAS_PLANTA + ", " + datosjson[i].EMBARQUES + ", " + datosjson[i].EMPAQUE_AGUILARES + ", " + datosjson[i].EMPAQUE_CAMPO + ", " + datosjson[i].ENSALADAS + ", " + datosjson[i].FRESCO + ", " + datosjson[i].LOGISTICA_CEDIS + ", " + datosjson[i].PRODUCCION + ", " + datosjson[i].TRANSPORTISTAS + ", " + datosjson[i].TUBO + ", " + datosjson[i].VENTAS_CEDIS + ", " + datosjson[i].VENTAS_PLANTA;
                }
                //document.write("<br>" + i + " - " + datosjson[i].Semana + " - " + datosjson[i].Ajo_1);

            });
            //cadena = "1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16";
            //cadena = cadena.slice(0, -1);157
            cadena += "";
            cadena = "[" + cadena + "]";

            //alert(JSON.stringify(cadena));

            Highcharts.chart('container', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Quejas semanales por Área',
                    x: -20 //center
                },
                subtitle: {
                    text: '',
                    x: -20
                },
                xAxis: {
                    categories: ['AJO', 'ALMACEN-CEDIS', 'CLIENTE', 'COMPRAS-CEDIS', 'COMPRAS-PLANTA', 'EMBARQUES',
                'EMPAQUE AGUILARES', 'EMPAQUE CAMPO', 'ENSALADAS', 'FRESCO', 'LOGISTICA-CEDIS', 'PRODUCCION', 'TRANSPORTISTAS', 'TUBO', 'VENTAS-CEDIS', 'VENTAS-PLANTA']
                },
                yAxis: {
                    title: {
                        text: 'Quejas'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: ''
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: "QUEJAS",
                    data: JSON.parse(cadena)
                }]
            });

            $("#datosqueja").hide();


        });

        var wb = XLSX.utils.book_new();
        wb.Props = {
            Title: "SheetJS Tutorial",
            Subject: "Test",
            Author: "Red Stapler",
            CreatedDate: new Date(2017, 12, 19)
        };

        wb.SheetNames.push("Test Sheet");
        var ws_data = $('#datatable').tableToJSON(); //[['hello', 'world']];
        var ws = XLSX.utils.aoa_to_sheet(ws_data);
        wb.Sheets["Test Sheet"] = ws;
        var wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'binary' });
        function s2ab(s) {

            var buf = new ArrayBuffer(s.length);
            var view = new Uint8Array(buf);
            for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;
            return buf;

        }
        $("#convert-table").click(function () {
            saveAs(new Blob([s2ab(wbout)], { type: "application/octet-stream" }), 'test.xlsx');
        });
        </script>
</body>
</html>
