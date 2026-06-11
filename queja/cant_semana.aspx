<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cant_semana.aspx.cs" Inherits="queja.cant_semana" %>

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
                    cadena = datosjson[i].Sem1 + ", " + datosjson[i].Sem2 + ", " + datosjson[i].Sem3 + ", " + datosjson[i].Sem4 + ", " + datosjson[i].Sem5 + ", " + datosjson[i].Sem6 + ", " + datosjson[i].Sem7 + ", " + datosjson[i].Sem8 + ", " + datosjson[i].Sem9 + ", " + datosjson[i].Sem10 + ", " + datosjson[i].Sem11 + ", " + datosjson[i].Sem12 + ", " + datosjson[i].Sem13 + ", " + datosjson[i].Sem14 + ", " + datosjson[i].Sem15 + ", " + datosjson[i].Sem16 + ", " + datosjson[i].Sem17 + ", " + datosjson[i].Sem18 + ", " + datosjson[i].Sem19 + ", " + datosjson[i].Sem20 + ", " + datosjson[i].Sem21 + ", " + datosjson[i].Sem22 + ", " + datosjson[i].Sem23 + ", " + datosjson[i].Sem24 + ", " + datosjson[i].Sem25 + ", " + datosjson[i].Sem26 + ", " + datosjson[i].Sem27 + ", " + datosjson[i].Sem28 + ", " + datosjson[i].Sem29 + ", " + datosjson[i].Sem30 + ", " + datosjson[i].Sem31 + ", " + datosjson[i].Sem32 + ", " + datosjson[i].Sem33 + ", " + datosjson[i].Sem34 + ", " + datosjson[i].Sem35 + ", " + datosjson[i].Sem36 + ", " + datosjson[i].Sem37 + ", " + datosjson[i].Sem38 + ", " + datosjson[i].Sem39 + ", " + datosjson[i].Sem40 + ", " + datosjson[i].Sem41 + ", " + datosjson[i].Sem42 + ", " + datosjson[i].Sem43 + ", " + datosjson[i].Sem44 + ", " + datosjson[i].Sem45 + ", " + datosjson[i].Sem46 + ", " + datosjson[i].Sem47 + ", " + datosjson[i].Sem48 + ", " + datosjson[i].Sem49 + ", " + datosjson[i].Sem50 + ", " + datosjson[i].Sem51 + ", " + datosjson[i].Sem52;
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
            //alert('graficando');
            Highcharts.chart('container', {
                title: {
                    text: 'Cajas semanales',
                    x: -20 //center
                },
                subtitle: {
                    text: '',
                    x: -20
                },
                xAxis: {
                    categories: ['Sem1', 'Sem2', 'Sem3', 'Sem4', 'Sem5', 'Sem6', 'Sem7', 'Sem8', 'Sem9', 'Sem10',
                     'Sem11', 'Sem12', 'Sem13', 'Sem14', 'Sem15', 'Sem16', 'Sem17', 'Sem18', 'Sem19', 'Sem20',
                     'Sem21', 'Sem22', 'Sem23', 'Sem24', 'Sem25', 'Sem26', 'Sem27', 'Sem28', 'Sem29', 'Sem30',
                     'Sem31', 'Sem32', 'Sem33', 'Sem34', 'Sem35', 'Sem36', 'Sem37', 'Sem38', 'Sem39', 'Sem40',
                     'Sem41', 'Sem42', 'Sem43', 'Sem44', 'Sem45', 'Sem46', 'Sem47', 'Sem48', 'Sem49', 'Sem50',
                     'Sem51', 'Sem52']
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
            //alert('graficado');
            //            $("#btnExport").click(function (e) {
            //                window.open('data:application/vnd.ms-excel,' + $('#datosqueja').html());
            //                e.preventDefault();
            //            });


            //            $('#convert-table').click(function () {
            //                var table = $('#datatable').tableToJSON(); // Convert the table into a javascript object
            //                //console.log(table);
            //                alert(JSON.stringify(table));
            //                datosjson = table;
            //                //document.write(JSON.stringify(datosjson));
            //                var cadena = "[";
            //                $.each(datosjson, function (i, item) {
            //                    if (datosjson[i].Problema == "TOTALES") {
            //                        cadena += "{ name: '" + datosjson[i].Problema + "', data: [" + datosjson[i].Sem1 + ", " + datosjson[i].Sem2 + ", " + datosjson[i].Sem3 + ", " + datosjson[i].Sem4 + ", " + datosjson[i].Sem5 + ", " + datosjson[i].Sem6 + ", " + datosjson[i].Sem7 + ", " + datosjson[i].Sem8 + ", " + datosjson[i].Sem9 + ", " + datosjson[i].Sem10 + ", " +
            //                        datosjson[i].Sem11 + ", " + datosjson[i].Sem12 + ", " + datosjson[i].Sem13 + ", " + datosjson[i].Sem14 + ", " + datosjson[i].Sem15 + ", " + datosjson[i].Sem16 + ", " + datosjson[i].Sem17 + ", " + datosjson[i].Sem18 + ", " + datosjson[i].Sem19 + ", " + datosjson[i].Sem20 + ", " +
            //                        datosjson[i].Sem21 + ", " + datosjson[i].Sem22 + ", " + datosjson[i].Sem23 + ", " + datosjson[i].Sem24 + ", " + datosjson[i].Sem25 + ", " + datosjson[i].Sem26 + ", " + datosjson[i].Sem27 + ", " + datosjson[i].Sem28 + ", " + datosjson[i].Sem29 + ", " + datosjson[i].Sem30 + ", " +
            //                        datosjson[i].Sem31 + ", " + datosjson[i].Sem32 + ", " + datosjson[i].Sem33 + ", " + datosjson[i].Sem34 + ", " + datosjson[i].Sem35 + ", " + datosjson[i].Sem36 + ", " + datosjson[i].Sem37 + ", " + datosjson[i].Sem38 + ", " + datosjson[i].Sem39 + ", " + datosjson[i].Sem40 + ", " +
            //                        datosjson[i].Sem41 + ", " + datosjson[i].Sem42 + ", " + datosjson[i].Sem43 + ", " + datosjson[i].Sem44 + ", " + datosjson[i].Sem45 + ", " + datosjson[i].Sem46 + ", " + datosjson[i].Sem47 + ", " + datosjson[i].Sem48 + ", " + datosjson[i].Sem49 + ", " + datosjson[i].Sem50 + ", " +
            //                        datosjson[i].Sem51 + ", " + datosjson[i].Sem52 + "]},";
            //                    }
            //                    //document.write("<br>" + i + " - " + datosjson[i].Semana + " - " + datosjson[i].Ajo_1);

            //                });
            //                cadena = cadena.slice(0, -1);
            //                cadena += "]";
            //                //datosjson = cadena;
            //                alert(JSON.stringify(cadena));
            //            });
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
    </script>
    <style>
        body
        {
            /*background-image: url(imagenes/fondo_7.png);*/
            background-color: #FFFFFF;
            background-position: center center;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: cover;
            /*background-color: #464646;*/
            font-size: 12px;
            }
    </style>
</head>
<body>
    <div class="container col-lg-12">
        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
            <form id="form1" runat="server">
                <div  class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Cajas por Semana</strong></h3></center>
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
                                        CssClass="btn btn-success" onclick="btnTodos_Click"  />
                                    <asp:Button runat="server" ID="btnCedis" Text="Ver CEDIS" 
                                        CssClass="btn btn-success" onclick="btnCedis_Click"  />
                                    <asp:Button runat="server" ID="btnExcelTodos" Text="Exportar CEDIS Excel" 
                                        CssClass="btn btn-success" onclick="btnExcelTodos_Click"   />
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
