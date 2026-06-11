<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="producidas.aspx.cs" Inherits="queja.producidas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="jqueryui/jquery-ui.css" rel="stylesheet" type="text/css" />

    <link href="excel/css/tableexport.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>

    <script src="Scripts/bootstrap.js" type="text/javascript"></script>

    <script src="jqueryui/jquery-ui.js" type="text/javascript"></script>
    <script src="jqueryui/datepicker-es.js" type="text/javascript"></script>

    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>

    <script src="Scripts/tableExport.js" type="text/javascript"></script>
    <script src="Scripts/jquery.base64.js" type="text/javascript"></script>

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>

    <script src="Scripts/jquery.tabletojson.js" type="text/javascript"></script>

    <script src="excel/Blob.min.js" type="text/javascript"></script>
    <script src="excel/xls.core.min.js" type="text/javascript"></script>
    <script src="excel/FileSaver.min.js" type="text/javascript"></script>
    <script src="excel/js/tableexport.js" type="text/javascript"></script>

    <script>
    
    $().ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaInicio.ClientID %>").datepicker({
            });
            $("#<%= txtFechaFin.ClientID %>").datepicker({
                minDate: $("#<%= txtFechaInicio.ClientID %>").val()
            });

       //$("#just_load_please").on("click", function(e) {
//       $("#<%= btnBuscar.ClientID %>").on("click", function(e) {
//                e.preventDefault();
//                $("#loadMe").modal({
//                  backdrop: "static", //remove ability to close modal with click
//                  keyboard: false, //remove option to close with keyboard
//                  show: true //Display loader!
//                });
//                setTimeout(function() {
//                  $("#loadMe").modal("hide");
//                }, 3500);
//              });
//            
//            
        });

        function ventana()
        {
            $("#loadMe").modal();
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
        .datatable{
          font-size: 10px;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div  class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Cajas Recibidas vs Rechazadas PT</strong></h3></center>
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
                                    <asp:Button runat="server" ID="btnVolver" Text="Regresar a reportes" 
                                        CssClass="btn btn-primary" onclick="btnVolver_Click"   />
                                    <%--<asp:Button runat="server" ID="btnExport" Text="Exportar tabla a Excel" 
                                        CssClass="btn btn-success" OnClientClick="exportarexcel()" 
                                         />--%>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group form-inline">    
                                <div class="col-sm-12">
                                    <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="form-control" Style="text-transform: uppercase" placeholder="FECHA INICIAL" onkeydown = "return (event.keyCode!=13)" data-provide="datepicker" />
                                    <asp:TextBox runat="server" ID="txtFechaFin" CssClass="form-control" Style="text-transform: uppercase" placeholder="FECHA FINAL" onkeydown = "return (event.keyCode!=13)" data-provide="datepicker"/>
                                    <asp:DropDownList ID="ddlQuejas" runat="server" 
                                        CssClass="form-control btn btn-primary" >
                                    </asp:DropDownList>
                                    <asp:Button runat="server" ID="btnBuscar" Text="BUSCAR" 
                                        CssClass="btn btn-primary" onclick="btnBuscar_Click" OnClientClick="ventana()" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group form-inline">    
                                <div class="col-sm-12">
                                    <asp:Button runat="server" ID="btnTabla" Text="ENVIAR RESULTADOS A TABLA DINAMICA" 
                                        CssClass="btn btn-success" onclick="btnTabla_Click" Visible="true" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <center>    
                                <%--<div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>--%>
                                
                                <div id="datosqueja" runat="server"></div>
                            </center>
                        </div>
                        <div class="row">
                            <center>    
                                <%--<div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>--%>
                                
                                <div id="datosqueja2" runat="server"></div>
                            </center>
                        </div>
                    </div>
                    <%--<input type="button" id="convert-table" value="Convertir" />--%>
                    <input type="button" id="btnPrueba" runat="server" value="Convertir" onclick="funcion()"/>
                    <%--<button type="button" class="btn btn-default btn-lg btn-block" id="just_load_please">
                --%></div>
                <div class="modal fade" tabindex="-1" role="dialog" id="myModal">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Detalle de Variedades</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div id="cargando" class="loader-txt">
                                  <p><h4>Generando Información</h4><br/>
                                  <div class="progress">
                                      <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                        </div>
                                    </div>
                                  </p>
                                </div>
                                <div id="recibido_variedad"></div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="loadMe" tabindex="-1" role="dialog" aria-labelledby="loadMeLabel">
                  <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                      <div class="modal-body text-center">
                        <div class="loader"></div>
                        <div class="loader-txt">
                          <p><h1>Generando Información</h1><br/>
                          <div class="progress">
                              <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                        <span class="sr-only">45% Complete</span>
                                </div>
                            </div>
                          </p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
            </form>
        </div>
    </div>
    <script>
//    $("#datatable").tableExport({
//	    formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
//	    position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
//	    bootstrap: true,//Usar lo estilos de css de bootstrap para los botones (true, false)
//	    fileName: "ReporteProducidasVsRechazadas"    //Nombre del archivo 


//	});

//	function tabla_dinamica() {

//	    $("#datatable2").tableExport({
//	        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
//	        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
//	        bootstrap: true, //Usar lo estilos de css de bootstrap para los botones (true, false)
//	        fileName: "ProducidasVsRechazadasTablaDinamica"    //Nombre del archivo 
//	    });

//    }

	function funcion() {
	    
	    var pageUrl = "producidas.aspx/funcion";
	    //alert(pageUrl);
	    $.ajax({
	        type: 'POST',
	        contentType: 'application/json; charset=utf-8',
	        url: pageUrl,
	        dataType: "json",
	        success: function (data) {
	            onSuccess(data);
	        },
	        error: function (data, success, error) {
	            alert("Error: 2" + error);
	        }
	    });
	    return false;
	}

	function onSuccess(data) {
	    alert(data.d);
	    
	    //$("#cargando").removeClass("progress-bar progress-bar-striped active")
	}

	function recibido_variedad(vari) {
	    $("#recibido_variedad").hide();
        $("#myModal").modal({ show: true });
	    $("#cargando").show();
	    var pageUrl = "producidas.aspx/recibido_variedad";
	    //alert(pageUrl);
	    $.ajax({
	        type: 'POST',
	        contentType: 'application/json; charset=utf-8',
	        url: pageUrl,
	        data: "{ vari: '" + vari + "'}",
	        dataType: "json",
	        success: function (data) {
	            onSuccess(data);
	        },
	        error: function (data, success, error) {
	            alert("Error: 2" + error);
	        }
	    });
	    return false;
	    
	}

	function onSuccess(data) {
	    //alert(data.d);
	    $("#recibido_variedad").html(data.d);
	    $("#recibido_variedad").show();
	    $("#cargando").hide();
	    //$("#myModal").modal({ show: true });
	}

	function rechazado_problema(vari, prob) {
	    var f1 = $("#<%= txtFechaInicio.ClientID %>").val();
	    $("#recibido_variedad").hide();
        $("#myModal").modal({ show: true });
	    $("#cargando").show();
	    var pageUrl = "producidas.aspx/rechazado_problema";
	    //alert(pageUrl);
	    $.ajax({
	        type: 'POST',
	        contentType: 'application/json; charset=utf-8',
	        url: pageUrl,
	        data: "{ vari: '" + vari + "', prob: '" + prob + "', fecha1: '" + f1 + "'}",
	        dataType: "json",
	        success: function (data2) {
	            onSuccess2(data2);
	        },
	        error: function (data2, success, error) {
	            alert("Error: 2" + error);
	        }
	    });
	    return false;

	}

	function onSuccess2(data2) {
	    //alert(data.d);
	    $("#recibido_variedad").html(data2.d);
	    $("#recibido_variedad").show();
	    $("#cargando").hide();
	    
	}

    
    </script>
</body>
</html>
