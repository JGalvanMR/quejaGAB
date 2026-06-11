<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cant_quejas.aspx.cs" Inherits="queja.cant_quejas" %>

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

    <script src="Scripts/jquery.tabletojson.js" type="text/javascript"></script>

    

    <script>
        $(function () {

            var table = $('#datatable').tableToJSON(); // Convert the table into a javascript object
            //console.log(table);
            //alert(JSON.stringify(table));
            datosjson = table;
            //document.write(JSON.stringify(datosjson));
            var cadena = "";
            $.each(datosjson, function (i, item) {
                if (datosjson[i].Problema == "TOTALES") {
                    //cadena += "{ name: '" + datosjson[i].Problema + "', data: [" + datosjson[i].Enero + ", " + datosjson[i].Febrero + ", " + datosjson[i].Marzo + ", " + datosjson[i].Abril + ", " + datosjson[i].Mayo + ", " + datosjson[i].Junio + ", " + datosjson[i].Agosto + ", " + datosjson[i].Septiembre + ", " + datosjson[i].Octubre + ", " + datosjson[i].Noviembre + ", " + datosjson[i].Diciembre + "]},";
                    cadena = datosjson[i].Enero + ", " + datosjson[i].Febrero + ", " + datosjson[i].Marzo + ", " + datosjson[i].Abril + ", " + datosjson[i].Mayo + ", " + datosjson[i].Junio + ", " + datosjson[i].Julio + ", " + datosjson[i].Agosto + ", " + datosjson[i].Septiembre + ", " + datosjson[i].Octubre + ", " + datosjson[i].Noviembre + ", " + datosjson[i].Diciembre;
                }
                //document.write("<br>" + i + " - " + datosjson[i].Semana + " - " + datosjson[i].Ajo_1);

            });
            //cadena = cadena.slice(0, -1);
            cadena += "";
            cadena = "[" + cadena + "]";
            //datosjson = cadena;
            //alert(JSON.stringify(cadena));
            //var cad = [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0];


            //document.write(JSON.stringify(cadena));
            Highcharts.chart('container', {
                title: {
                    text: 'Cajas mensuales',
                    x: -20 //center
                },
                subtitle: {
                    text: '',
                    x: -20
                },
                xAxis: {
                    categories: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
                'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre']
                },
                yAxis: {
                    title: {
                        text: 'Cajas'
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
                    name: "CAJAS",
                    data: JSON.parse(cadena)
                }]
            });

//            $("#btnExport").click(function (e) {
//                window.open('data:application/vnd.ms-excel,' + $('#datosqueja').html());
//                e.preventDefault();
//            });
            

            $('#convert-table').click(function () {
                var table = $('#datatable').tableToJSON(); // Convert the table into a javascript object
                //console.log(table);
                alert(JSON.stringify(table));
                datosjson = table;
                //document.write(JSON.stringify(datosjson));
                var cadena = "[";
                $.each(datosjson, function (i, item) {
                    if (datosjson[i].Problema == "TOTALES") {
                        cadena += "{ name: '" + datosjson[i].Problema + "', data: [" + datosjson[i].Enero + ", " + datosjson[i].Febrero + ", " + datosjson[i].Marzo + ", " + datosjson[i].Abril + ", " + datosjson[i].Mayo + ", " + datosjson[i].Junio + ", " + datosjson[i].Julio + ", " + datosjson[i].Agosto + ", " + datosjson[i].Septiembre + ", " + datosjson[i].Octubre + ", " + datosjson[i].Noviembre + ", " + datosjson[i].Diciembre + "]},";
                    }
                    //document.write("<br>" + i + " - " + datosjson[i].Semana + " - " + datosjson[i].Ajo_1);

                });
                cadena = cadena.slice(0, -1);
                cadena += "]";
                //datosjson = cadena;
                alert(JSON.stringify(cadena));
            });
        });

//        function exportarexcel() { 
//                $('#datatable1').tableExport({type:'excel',escape:'false'});
//        }

//        [{
//            name: 'Tokyo',
//            data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
//        }, {
//            name: 'New York',
//            data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
//        }, {
//            name: 'Berlin',
//            data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
//        }, {
//            name: 'London',
//            data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
        //        }]

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
    <div class="container">
        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
            <form id="form1" runat="server">
                <div  class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Cajas por Mes</strong></h3></center>
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
                                        CssClass="btn btn-success" OnClientClick="exportarexcel()" 
                                        onclick="btnExport_Click" />
                                    <asp:Button runat="server" ID="btnTodos" Text="Ver todos los CEDIS" 
                                        CssClass="btn btn-success" onclick="btnTodos_Click" />
                                    <asp:Button runat="server" ID="btnCedis" Text="Ver CEDIS" 
                                        CssClass="btn btn-success" onclick="btnCedis_Click" />
                                    <asp:Button runat="server" ID="btnExcelTodos" Text="Exportar Excel Cedis"
                                        CssClass="btn btn-success" onclick="btnExcelTodos_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <center>    
                                <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                
                                <div id="datosqueja" runat="server"></div>
                            </center>
                        </div>
                    </div>
                    <input type="button" id="convert-table" value="Convertir" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
