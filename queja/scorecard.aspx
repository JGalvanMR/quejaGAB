<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scorecard.aspx.cs" Inherits="queja.scorecard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="jqueryui/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui.js" type="text/javascript"></script>
    <script src="jqueryui/datepicker-es.js" type="text/javascript"></script>

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/accessibility.js"></script>
    <script src="https://code.highcharts.com/modules/no-data-to-display.js"></script>

    <script src="Scripts/jquery.tabletojson.js" type="text/javascript"></script>

    <!-- Font Awesome -->


    <script type="text/javascript">

    $().ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFecha.ClientID %>").datepicker({
                minDate: -365,
                maxDate: 20
            });
            $("#<%= txtFechaFin.ClientID %>").datepicker({
                minDate: -365,
                maxDate: 20
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

        function ShowPopup() {
            //$("#btnShowPopup").click();
            $("#myModal").modal({ show: true });
        }

        /*Highcharts.chart('container', {
            data: {
                table: 'datatable'
            },
            chart: {
                type: 'column'
            },
            title: {
                text: 'Total de embarques y quejas'
            },
            xAxis: {
                type: 'category'
            },
            yAxis: {
                allowDecimals: false,
                title: {
                    text: 'Amount'
                }
            }
        });*/
//        $(function () {
//        var chart = Highcharts.chart('container', {
//                data: {
//                    table: 'datatable'
//                },
//                chart: {
//                    type: 'column'
//                },
//                title: {
//                    text: 'Embarques Nacionales'
//                },
//                yAxis: {
//                    allowDecimals: false,
//                    title: {
//                        text: 'Embarques / Quejas'
//                    }
//                },
//                lang:{
//                    noData: 'No se encontraron datos'
//                },
//                noData: {
//                    style:{
//                        fontWeight: 'bold',
//                        fontSize: '15px',
//                        color: '#303030'
//                    }
//                }/*,,
//                tooltip: {
//                    formatter: function () {
//                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
//                            ' Rechazadas: ' + this.point.y;
//                    }
//                }
//                plotOptions: {
//                    series: {
//                        dataLabels: {
//                            enabled: true,
//                            formatter: function () {
//                                return humanizeNumber(this.y)
//                            }
//                        }
//                    }
//                }*/
//            });
//        //$("#datatable2").hide();
//    });

//    $(function () {
//        var chart = Highcharts.chart('container2', {
//                data: {
//                    table: 'datatable2'
//                },
//                chart: {
//                    type: 'column'
//                },
//                title: {
//                    text: 'Embarques Exportacion'
//                },
//                yAxis: {
//                    allowDecimals: false,
//                    title: {
//                        text: 'Embarques / Quejas'
//                    }
//                },
//                lang:{
//                    noData: 'No se encontraron datos'
//                },
//                noData: {
//                    style:{
//                        fontWeight: 'bold',
//                        fontSize: '15px',
//                        color: '#303030'
//                    }
//                }/*,,
//                tooltip: {
//                    formatter: function () {
//                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
//                            ' Rechazadas: ' + this.point.y;
//                    }
//                }
//                plotOptions: {
//                    series: {
//                        dataLabels: {
//                            enabled: true,
//                            formatter: function () {
//                                return humanizeNumber(this.y)
//                            }
//                        }
//                    }
//                }*/
//            });
//        //$("#datatable2").hide();
//    });

    $(function () {
        var chart = Highcharts.chart('container3', {
                data: {
                    table: 'datatable3'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Embarques Nacional Mes Anterior'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Embarques / Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();
    });

    $(function () {
        var chart = Highcharts.chart('container4', {
                data: {
                    table: 'datatable4'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Embarques Exportacion Mes Anterior'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Embarques / Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();
    });

    $(function () {
        var chart = Highcharts.chart('container5', {
                data: {
                    table: 'datatable5'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Embarques Nacional Mes Actual'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Embarques / Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();
    });

    $(function () {
        var chart = Highcharts.chart('container6', {
                data: {
                    table: 'datatable6'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Embarques Exportacion Mes Actual'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Embarques / Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();
    });
    $(function () {
        var chart = Highcharts.chart('container7', {
                data: {
                    table: 'datatable7'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Embarques y Quejas Nacional (Placa)'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Embarques / Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();
    });

    $(function () {
        var chart = Highcharts.chart('container8', {
                data: {
                    table: 'datatable8'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Embarques y Quejas Exportacion (Placa)'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Embarques / Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();
    });

    $(function () {
        var chart = Highcharts.chart('container9', {
                data: {
                    table: 'datatable9'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Quejas por area - Nacional'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos o no se ha asignado area'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();
    });

    $(function () {
        var chart = Highcharts.chart('container10', {
                data: {
                    table: 'datatable10'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Quejas por area - Exportacion'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Quejas'
                    }
                },
                lang:{
                    noData: 'No se encontraron datos o no se ha asignado area'
                },
                noData: {
                    style:{
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                }/*,,
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name.toUpperCase() + '</b><br/>' +
                            ' Rechazadas: ' + this.point.y;
                    }
                }
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
        //$("#datatable2").hide();



    });

    function detalle_pedidos(valor1, valor2, valor3, valor4)
    {
        //alert(valor1 + valor2 + valor3 + valor4);
        var pageUrl = "WebService.aspx/detalle_pedidos";
        var valor5 = document.getElementById("<%= ddlArea.ClientID %>").value; //$("<%= lblAdmin.ClientID %>").text;
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: "{ valor1: '" + valor1 + "', valor2: '" + valor2 + "', valor3: '" + valor3 + "', valor4: '" + valor4 + "', valor5: '" + valor5 + "'}",
            success: function (data2)
            {
                onSuccess(data2);
            },
            error: function (data2, success, error){
                alert("Error: " + data2 + error);
            }
        });
        return false;
    }

    function onSuccess(data2)
    {
        $("#informacion").html(data2.d);
    }
    
    function ir_queja(valor1)
    {
        //alert("redireccion a la queja");
        var clave = document.getElementById("<%= lblClave.ClientID %>").innerHTML; //$("#<%= lblClave.ClientID %>").text;
        var nombre = document.getElementById("<%= lblNombre.ClientID %>").innerHTML; //$("#<%= lblNombre.ClientID %>").text;
        var cedis = document.getElementById("<%= lblCedis.ClientID %>").innerHTML; //$("#<%= lblCedis.ClientID %>").text;
        var admin = document.getElementById("<%= lblAdmin.ClientID %>").innerHTML; //$("<%= lblAdmin.ClientID %>").text;
        var queja = valor1;
        var tarea = "CONSULTA";
        window.open("servicios.aspx?clave="+clave+"&nombre="+nombre+"&cedis="+cedis+"&admin="+admin+"&queja="+queja+"&tarea="+tarea, "_blank");
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
        
        .stati{
  background: #fff;
  height: 6em;
  padding:1em;
  margin:1em 0; 
    -webkit-transition: margin 0.5s ease,box-shadow 0.5s ease; /* Safari */
    transition: margin 0.5s ease,box-shadow 0.5s ease; 
  -moz-box-shadow:0px 0.2em 0.4em rgb(0, 0, 0,0.8);
-webkit-box-shadow:0px 0.2em 0.4em rgb(0, 0, 0,0.8);
box-shadow:0px 0.2em 0.4em rgb(0, 0, 0,0.8);
}
.stati:hover{ 
  margin-top:0.5em;  
  -moz-box-shadow:0px 0.4em 0.5em rgb(0, 0, 0,0.8); 
-webkit-box-shadow:0px 0.4em 0.5em rgb(0, 0, 0,0.8); 
box-shadow:0px 0.4em 0.5em rgb(0, 0, 0,0.8); 
}
.stati i{
  font-size:3.5em; 
} 
.stati div{
  width: calc(100% - 3.5em);
  display: block;
  float:right;
  text-align:right;
}
.stati div b {
  font-size:2.2em;
  width: 100%;
  padding-top:0px;
  margin-top:-0.2em;
  margin-bottom:-0.2em;
  display: block;
}
.stati div span {
  font-size:1em;
  width: 100%;
  color: rgb(0, 0, 0,0.8); !important;
  display: block;
}

.stati.left div{ 
  float:left;
  text-align:left;
}

.stati.bg-belize_hole { background: rgb(41, 128, 185); color:white;} 
.stati.belize_hole { color: rgb(41, 128, 185);}

hr {
  border: 5px solid green;
  border-radius: 5px;
}

.esconde
{
    display: none;
    }
    
.hiddenRow {
    padding: 0 !important;
}
    </style>
</head>
<body>
    <div class="container col-lg-10 col-lg-offset-1">
        <div class="row">
            <div class="col-md-12 col-md-offset-0">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h1><strong class="">Reporte Servicios por Transportista</strong></h1></center>
                    </div>
                    <div class="panel-body">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server" >
                            </asp:ScriptManager>
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                            <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                            <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                            <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>                                     
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group form-inline">
                                            <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="btn btn-primary" onclick="btnRegresar_Click" />
                                        </div>
                                    </div>
                                    </br>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="ddlTransportistas" class="col-sm-2 control-label text-right">Transportistas</label>
                                            <div class="col-sm-8">
                                                <asp:UpdatePanel ID="udpTransportistas" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlTransportistas" runat="server" AutoPostBack="false" CssClass="form-control form">  
                                                        </asp:DropDownList> 
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    </br>
                                    <div class="row">
                                        <div class="form-group form-inline">
                                            <label for="txtFecha" class="col-sm-2 control-label text-right">Fechas</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" placeholder="Fecha"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="form-control" placeholder="FechaFin"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    </br>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="ddlArea" class="col-sm-2 control-label text-right">Area</label>
                                            <div class="col-sm-4">
                                                <asp:UpdatePanel ID="udpArea" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="false" CssClass="form-control form">
                                                            <asp:ListItem Value="1">EMBARQUES</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">TRANSPORTISTA</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    </br>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <center>
                                                    <asp:Button runat="server" ID="btnBuscaOrden" Text="Buscar" 
                                                        CssClass="btn btn-primary" onclick="btnBuscaOrden_Click" />
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                    </br>
                                    </br>
                                    <div class="row">
                                        <span class="label label-success col-lg-12 col-md-12 col-sm-12"><h5><b>Embarques y quejas por fechas</b></h5></span>
                                    </div>
                                    <br />
                                    <div class="row col-md-offset-3">
                                        <div id="perdida" runat="server"></div>
                                    </div>
                                    </br>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <center>
                                                    <asp:Button runat="server" ID="btnExcel" Text="Exportar a Excel" 
                                                        CssClass="btn btn-success" OnClick="btnExcel_Click" />
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <center>
                                            <div id="datosqueja" runat="server"></div>
                                        </center>
                                    </div>
                                    
                                    
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            Ver 1.5
        </div>
        <div class="modal fade" tabindex="-1" role="dialog" id="myModal">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Detalle de Placa</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="informacion"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
