<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="principalexpnew.aspx.cs" Inherits="queja.principalexpnew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="alertifyjs/css/alertify.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="alertifyjs/alertify.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
		$().ready(function () {
            
            
            $(".ir-arriba").click(function(){
                $("body, html").animate({
                    scrollTop:'0px'
                }, 300);
            });
            $(window).scroll(function(){
                if($(this).scrollTop() > 0){
                    $(".ir-arriba").slideDown(300);
                }else{
                    $(".ir-arriba").slideUp(300);
                }
            });

        });
	
        var iddleTimeout = null;
        function pageLoad() {
            if (iddleTimeout != null)
                clearTimeout(iddleTimeout);

                var millisecTimeout = <%= int.Parse(System.Configuration.ConfigurationManager.AppSettings["SessionTimeout"]) * 60 * 1000 %>; 

                //iddleTimeout = setTimeout("TimeoutPage()", millisecTimeout);
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
        .hiddencol
        {
            display: none;
        }
        .tamano
        {
            resize: none;
        }
        body
        {
            background-image: url(imagenes/fondo_7.png);
            background-position: center center;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: cover;
            background-color: #464646;
        }
        .nover
        {
            display:none;
            }
        .ir-arriba{
          display:none;
          background-repeat:no-repeat;
          font-size:20px;
          color:black;
          cursor:pointer;
          position:fixed;
          bottom:10px;
          right:10px;
          z-index:2;
        }

    </style>
</head>
<body class="width:100%">
	<span class="ir-arriba"><img class="manImg" src="imagenes/arrow_up.png" width="70px" height="70px"></img></span>
    <div class="container col-lg-12">
		<div class="row">
			<div class="col-md-10 col-md-offset-1">
				<div class="panel panel-primary">
					<div class="panel-heading">
                        <center><h3><strong>Descripci&oacute;n Queja</strong></h3></center>
                    </div>
					<div class="panel-body">
						<form runat="server" class="form-horizontal" enctype="multipart/form-data">
							<asp:ScriptManager ID="scmScript" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
							<div class="panel panel-primary">
								<div class="panel-body">
									<div class="form-group">
                                        <div class="col-sm-12">
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
										<div class="form-group">
											<div class="col-sm-offset-2 col-sm-8">
												<asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
													CssClass="btn btn-primary" OnClick="btnVolver_Click" />
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="txtFolio" class="col-sm-2 control-label">Folio:</label>
											<div class="col-sm-9">
                                                <input type="text" id="txtFolio" class="form-control" style="text-transform: uppercase" placeholder="Folio" readonly="readonly" />
											</div>
										</div>
									</div>
                                    <div class="row">
										<div class="form-group">
											<label for="txtSemana" class="col-sm-2 control-label">Semana:</label>
											<div class="col-sm-9">
                                                <input type="text" id="txtSemana" class="form-control" style="text-transform: uppercase" placeholder="Semana" readonly="readonly" />
											</div>
										</div>
                                    </div>
									<div class="row">
										<div class="form-group">
											<label for="txtMes" class="col-sm-2 control-label">Mes:</label>
											<div class="col-sm-9">
                                                <input type="text" id="txtMes" class="form-control" style="text-transform: uppercase" placeholder="Mes" readonly="readonly" />
											</div>
										</div>
                                    </div>
									<div class="row">
										<div class="form-group">
											<label for="txtFecha" class="col-sm-2 control-label">Fecha:</label>
											<div class="col-sm-9">
                                                <input type="text" id="txtFecha" class="form-control" style="text-transform: uppercase" placeholder="Fecha" readonly="readonly" />
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="ddlCliente" class="col-sm-2 control-label">Cliente:</label>
											<div class="col-sm-9">
                                                <select id="ddlCliente" class="form-control" onchange="CargarSucursales()">
                                                </select>
                                                <span id="errorCliente" style="color:red; display:none;">Debe seleccionar un cliente</span>
                                                <input type="hidden" id="txtClienteNom" />
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="ddlSucursales" class="col-sm-2 control-label">Sucursal:</label>
											<div class="col-sm-9">
                                                <select id="ddlSucursales" class="form-control" onchange="SucursalNombre()">
                                                    <%--<option value="0">SELECCIONAR...</option>--%>
                                                </select>
                                                <span id="errorSucursal" style="color:red; display:none;">Debe seleccionar una sucursal</span>
                                                <input type="hidden" id="txtSucursalNom" />
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="txtReporto" class="col-sm-2 control-label">Report&oacute;:</label>
											<div class="col-sm-9">
                                                <input type="text" id="txtReporto" class="form-control" style="text-transform: uppercase" placeholder="Reporto" />
                                                <span id="errorReporto" style="color:red; display:none;">Campo requerido</span>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="ddlTipo" class="col-sm-2 control-label">Tipo:</label>
											<div class="col-sm-9">
                                                <select id="ddlTipo" class="form-control" onchange="SeleccionaMoneda()">
                                                    <option value="">ELEGIR OPCION...</option>
                                                    <option value="N">NACIONAL</option>
                                                    <option value="E">EXPORTACION</option>
                                                </select>
                                                <span id="errorTipo" style="color:red; display:none;">Debe seleccionar un tipo</span>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-12">
												<p class="bg-primary text-center"><strong>Descripci&oacute;n del producto</strong></p>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="txtFechaCarga" class="col-sm-2 control-label">Fecha:</label>
											<div class="col-sm-9">
                                                <input type="date" id="txtFechaCarga" class="form-control" style="text-transform: uppercase" placeholder="Fecha" />
                                                <span id="errorFecha" style="color:red; display:none;">Campo requerido</span>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-12">
												<center>
                                                    <button type="button" id="btnBuscaOrden" class="btn btn-primary" onclick="ComboPlacas()">Buscar</button>
												</center>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="ddlPlacas" class="col-sm-2 control-label">Placa:</label>
											<div class="col-sm-9">
                                                <select id="ddlPlacas" class="form-control" onchange="PlacaNombre()">
                                                </select>
												<input type="text" id="txtPlaca" />
                                                <span id="errorPlacas" style="color:red; display:none;">Debe seleccionar un trasportista</span>
											</div>
										</div>
									</div>
									<%--<div class="row">
										<div class="form-group">
											<div class="col-sm-12">
												<center>
                                                    <button type="button" id="btnTodos" class="btn btn-primary" onclick="CargarOrdenProduccion()">Seleccionar todos</button>
												</center>
											</div>
										</div>
									</div>--%>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-12">
                                                <input type="hidden" id="txtEmbarque" class="form-control" style="text-transform: uppercase" placeholder="Embarque" />
												<input type="hidden" id="txtTransporte" class="form-control" style="text-transform: uppercase" placeholder="Transporte" />
												<input type="hidden" id="txtEmbarques" class="form-control" style="text-transform: uppercase" placeholder="Pedidos" />
												<input type="hidden" id="txtFactura" class="form-control" style="text-transform: uppercase" placeholder="Factura" />
												<input type="hidden" id="txtFechaEmb" class="form-control" style="text-transform: uppercase" placeholder="Fecha Embarque" />
												<input type="hidden" id="txtSemEmb" class="form-control" style="text-transform: uppercase" placeholder="Semana Embarque" />
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-offset-1 col-sm-10">
                                                <table id="tablaOrdenProd" class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Clave</th>
                                                            <th>Producto</th>
                                                            <th>Cajas</th>
                                                            <th>Rechazadas</th>
                                                            <th>Pedido</th>
                                                            <th>CNTE</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
											</div>
										</div>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="ddlLista" class="col-sm-2 control-label">Tipo:</label>
                                            <div class="col-sm-9">
                                                <select id="ddlLista" class="form-control">
                                                    <option value="">ELEGIR OPCION...</option>
                                                    <option value="DEV">DEVOLUCION</option>
                                                    <option value="MER">MERMA</option>
                                                    <option value="BON">BONIFICACION</option>
                                                    <option value="NA">NO APLICA</option>
                                                </select>
                                                <span id="errorLista" style="color:red; display:none;">Debe seleccionar el tipo de queja</span>
                                            </div>
                                        </div>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="ddlMoneda" class="col-sm-2 control-label">Moneda</label>
                                            <div class="col-sm-9">
                                                <select id="ddlMoneda" class="form-control" onchange="SeleccionaMoneda()">
                                                    <option value="">SELECCIONA MONEDA...</option>
                                                    <option value="PESOS">PESOS</option>
                                                    <option value="DOLARES">DOLARES</option>
                                                </select>
                                                <span id="errorMoneda" style="color:red; display:none;">Debe seleccionar la moneda</span>
                                            </div>
                                        </div>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="ddlProblema" class="col-sm-2 control-label">Problema</label>
                                            <div class="col-sm-9">
                                                <select id="ddlProblema" class="form-control">
                                                </select>
                                                <span id="errorProblema" style="color:red; display:none;">Debe seleccionar el problema</span>
                                            </div>
                                        </div>
									</div>
									<div class="row" id="idCosto">
										<div class="form-group">
											<label for="txtCosto" class="col-sm-2 control-label">Costo</label>
											<div class="col-sm-9">
                                                <input type="number" id="txtCosto" class="form-control" style="text-transform: uppercase" placeholder="Costo" value="0" step="0.01" />
                                               <span id="errorCosto" style="color:red; display:none">Debe ingresar el costo</span>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-offset-5 col-sm-2">
                                                <table id="tablaFacturas" class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Factura</th>
                                                            <th>Costo</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
											</div>
										</div>
									</div>
									<div class="row" id="idSufijo">
										<div class="form-group">
											<label for="txtSufijo" class="col-sm-2 control-label">Sufijo</label>
											<div class="col-sm-9">
                                                <input type="text" id="txtSufijo" class="form-control" style="text-transform: uppercase" placeholder="Sufijo" />
                                               <span id="errorSufijo" style="color:red; display:none">Debe ingresar el costo</span>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="txtObservaciones" class="col-sm-2 control-label">Observaciones</label>
											<div class="col-sm-9">
                                                <input  type="text" id="txtObservaciones" class="form-control" style="text-transform: uppercase" placeholder="Observaciones" maxlength="200" />
											</div>
										</div>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="fluArchivo1" class="col-sm-2 control-label text-right">Archivo 1</label>
                                            <div class="col-sm-9">
                                                <input type="file" id="fluArchivo1" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="fluArchivo2" class="col-sm-2 control-label text-right">Archivo 2</label>
                                            <div class="col-sm-9">
                                                <input type="file" id="fluArchivo2" class="form-control"/>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="fluArchivo3" class="col-sm-2 control-label text-right">Archivo 3</label>
                                            <div class="col-sm-9">
                                                <input type="file" id="fluArchivo3" class="form-control"/>
                                                <span id="errorArchivos" style="display:none; color:red;">Debe agregar por lo menos un archivo para poder registrar la queja</span>
                                                <span id="errorExtensiones" style="display:none; color:red;">Solo puede agregar archivos JPG, JPEG, PDF y PNG</span>
                                                <span id="errorSize" style="display:none; color:red;">El tama&ntilde;o de los archivos no puede ser mayor a 4Mb</span>
                                            </div>
                                        </div>
                                    </div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-12">
												<center>
                                                    <button type="button" id="btnGuardar" class="btn btn-primary" onclick="GuardarQuejaExp()">Guardar</button>
												</center>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-offset-0 col-sm-12">
												<span id="errorProductos" style="display:none; color:red;">No ha ingresado las cajas rechazadas y seleccionado el producto</span>
                                                <table id="tablaProductos" class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Pedido</th>
                                                            <th>ProdCve</th>
                                                            <th>Producto</th>
                                                            <th class="hiddencol">Ord Prod</th>
                                                            <th class="hiddencol">Tarima</th>
                                                            <th class="hiddencol">Lote</th>
															<th class="hiddencol">Fecha Cad</th>
                                                            <th class="hiddencol">CveProv</th>
                                                            <th>Proveedor</th>
                                                            <th class="hiddencol">CveRch</th>
                                                            <th>Rancho</th>
                                                            <th class="hiddencol">CveTbl</th>
															<th class="hiddencol">Tabla</th>
                                                            <th class="hiddencol">Responsable</th>
                                                            <th class="hiddencol">Area</th>
                                                            <th>Recibido</th>
                                                            <th>Rechazadas</th>
                                                            <th>Producidas</th>
															<th>Porcentaje</th>
															<th class="hiddencol">Unidad</th>
                                                            <th class="hiddencol">Tipo</th>
                                                            <th class="hiddencol">Variedad</th>
                                                            <th>CNTE</th>
                                                            <th>Borrar</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
											</div>
										</div>
									</div>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
	</div>
</body>
<script type="text/javascript">
	$(document).ready(function () {
		let hoy = new Date();
		let seisMesesAtras = new Date();
		seisMesesAtras.setMonth(hoy.getMonth() - 6);

		// Ajuste por si el mes restado se pasa a un año anterior
		if (hoy.getMonth() < 6) {
			seisMesesAtras.setFullYear(hoy.getFullYear() - 1);
		}

		// Convertir fechas al formato YYYY-MM-DD
		let hoyStr = hoy.toISOString().split("T")[0];
		let seisMesesStr = seisMesesAtras.toISOString().split("T")[0];

		// Establecer min y max en el input
		$("#txtFechaCarga").attr("max", hoyStr);
		$("#txtFechaCarga").attr("min", seisMesesStr);
	});
	function FolioQueja() {
        var pageUrl = "procesos.asmx/FolioQueja";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({}),
            dataType: 'json',
            success: function (data) {
                onSuccess(data);
            },
            error: function (data, success, error) {
                //console.error("Error en la peticion:", data.responseText);
                alert("Error: " + error);
            }
        });
        return false;
    }
    function onSuccess(data) {
        $("#txtFolio").val(data.d);
    }
	
	function Semana() {
        var pageUrl = "procesos.asmx/Semana";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({}),
            dataType: 'json',
            success: function (data2) {
                onSuccess2(data2);
            },
            error: function (data2, success, error) {
                alert("Error: " + error);
            }
        });
        return false;
    }
    function onSuccess2(data2) {
        $("#txtSemana").val(data2.d);
    }

	function Mes() {
        var pageUrl = "procesos.asmx/Mes";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({}),
            dataType: 'json',
            success: function (data3) {
                onSuccess3(data3);
            },
            error: function (data3, success, error) {
                alert("Error: " + error);
            }
        });
        return false;
    }
    function onSuccess3(data3) {
        $("#txtMes").val(data3.d);
    }

    function Fecha() {
        var pageUrl = "procesos.asmx/Fecha";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({}),
            dataType: 'json',
            success: function (data4) {
                onSuccess4(data4);
            },
            error: function (data4, success, error) {
                alert("Error: " + error);
            }
        });
        return false;
    }
    function onSuccess4(data4) {
        $("#txtFecha").val(data4.d);
    }
	
	function CargarClientes() {
        var pageUrl = "procesos.asmx/CargarClientes";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({}),
            dataType: 'json',
            success: function (data5) {
                //alert(data5.d);
                //onSuccess5(data5);
                let items = data5.d;
                //console.log(items);
                let combo = $('#ddlCliente');
                combo.empty();
                //combo.append('<option value="">SELECCIONAR CLIENTE...</option>');
                $.each(items, function(index, item){
                    combo.append($('<option>', {
                        value: item.Id,
                        text: item.Nombre
                    }));
                });
            },
            error: function (data5, success, error) {
                console.error(data5.responseText);
                alert("Error: " + error);
            }
        });
        return false;
    }
	
	function CargarSucursales() {
        var pageUrl = "procesos.asmx/CargarSucursales";
        //alert($("#<%=lblCedis.ClientID%>").html());
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({clave: $("#ddlCliente").val(), cedis: $("#<%=lblCedis.ClientID%>").html()}),//$("#<%=lblCedis.ClientID%>").html()
            dataType: 'json',
            success: function (data6) {
                //alert(data5.d);
                //onSuccess5(data5);
                let items = data6.d;
                //console.log(items);
                let combo = $('#ddlSucursales');
                combo.empty();
                //combo.append('<option value="">SELECCIONAR CLIENTE...</option>');
                $.each(items, function(index, item){
                    combo.append($('<option>', {
                        value: item.Id,
                        text: item.Nombre
                    }));
                });
                let selectedText = $("#ddlCliente option:selected").text();
                $("#txtClienteNom").val(selectedText);
                
            },
            error: function (data6, success, error) {
                console.error(data6.responseText);
                alert("Error: " + error);
            }
        });
        return false;
    }
	
	function SucursalNombre()
    {
        let selectedText = $("#ddlSucursales option:selected").text();
        $("#txtSucursalNom").val(selectedText);
    }
	
	function CargarProblemas() {
        var pageUrl = "procesos.asmx/CargarProblemas";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({}),
            dataType: 'json',
            success: function (data11) {
                //alert(data5.d);
                //onSuccess5(data5);
                let items = data11.d;
                //console.log(items);
                let combo = $('#ddlProblema');
                combo.empty();
                //combo.append('<option value="">SELECCIONAR CLIENTE...</option>');
                $.each(items, function(index, item){
                    combo.append($('<option>', {
                        value: item.Clave,
                        text: item.Nombre
                    }));
                });
            },
            error: function (data11, success, error) {
                console.error(data11.responseText);
                alert("Error: " + error);
            }
        });
        return false;
    }
	
	function ComboPlacas() {
		//alert();
		
		if(parseFloat(ValidacionesBotonBuscar()) > 0){
			return;
		}
		
		let FC = $("#txtFechaCarga").val()
		let FechaCarga = FC.replace(/-/g, "");
        var pageUrl = "procesos.asmx/ComboPlacas";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({fecha: FechaCarga}),
            dataType: 'json',
            success: function (data11) {
                let items = data11.d;
                //console.log(items);
                let combo = $('#ddlPlacas');
                combo.empty();
                $.each(items, function(index, item){
                    combo.append($('<option>', {
                        value: item.Pedido,
                        text: item.Trailer
                    }));
                });
            },
            error: function (data11, success, error) {
                console.error(data11.responseText);
                alert("Error: " + error);
            }
        });
        return false;
    }
	
	function PlacaNombre()
    {
        let selectedText = $("#ddlPlacas option:selected").text();
        $("#txtPlaca").val(selectedText);
		$("#txtEmbarque").val($("#ddlPlacas").val());
		$("#txtTransporte").val(selectedText);
		let fechaOriginal = $("#txtFechaCarga").val();
		let partes = fechaOriginal.split("-");
		let fechaFormateada = partes[2] + "/" + partes[1] + "/" + partes[0];
		$("#txtFechaEmb").val(fechaFormateada);
		let FecEmb = SemanaEmb();
		$("#txtSemEmb").val(FecEmb);
		
		DetalleEmbarqueConcentrado();
    }
	
	function SemanaEmb() {
		let FC = $("#txtFechaCarga").val();
		let FechaCarga = FC.replace(/-/g, "");
        var pageUrl = "procesos.asmx/SemanaEmb";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({fecha: FechaCarga}),
            dataType: 'json',
            success: function (data12) {
				//console.log(data12.d);
				$("#txtSemEmb").val(data12.d);
            },
            error: function (data12, success, error) {
                alert("Error: " + error);
            }
        });
		//console.log(FechaCarga);
        return false;
    }
	
	function DetalleEmbarqueConcentrado()
    {
		let fechaOriginal = $("#txtTransporte").val();
		let partes = fechaOriginal.split("-");
		
		let Emb = $("#ddlPlacas").val();
		let Tra = partes[0];
		
		let FC = $("#txtFechaCarga").val();
		let FCpar = FC.split("-");
		let fechaFormateada = FCpar[2] + "/" + FCpar[1] + "/" + FCpar[0];
		let Fec =  fechaFormateada;
		/*/let Fec = FC.replace(/-/g, "");*/
		let Tip = $("#ddlTipo").val();
		let Cho = partes[2];
		
		
        var pageUrl = "procesos.asmx/DetalleEmbarqueConcentrado";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: Emb, placa: Tra, fecha: Fec, tipo: Tip, chofer: Cho}),
            dataType: 'json',
            success: function (data13) {
                //alert(data5.d);
                //onSuccess5(data5);
                let ordenProd = data13.d;
                //console.log(ordenProd);
                let tbody = $("#tablaOrdenProd tbody");
				let tbodyF = $("#tablaFacturas tbody");
                tbody.empty();
				tbodyF.empty();
                let tip;
				let pedSet = new Set();
				let facSet = new Set();
                $.each(ordenProd, function(index, orden){
                    tip = orden.Tipo;
					
					pedSet.add(orden.Folio);
					facSet.add(orden.Fact);
					
                    let row = '<tr>' +
                        '<td><button type="button" class="btnSeleccionar btn btn-primary" ' +
                            'data-clave="' + orden.Prod + '" ' +
                            'data-nombre="' + orden.ProdNom + '" ' +
                            'data-cajas="' + orden.Cajas + '" ' +
                            'data-folio="' + orden.Folio + '" ' +
                            'data-cnte="' + orden.CNTE + '" ' +                            
                            '>' + orden.Prod + '</button></td>' +
                        '<td>' + orden.ProdNom + '</td>' +
                        '<td>' + orden.Cajas + '</td>' +
						'<td><input type="number" class="form-control"  data-id="' + orden.Prod + '" value="0"/></td>' +
                        '<td>' + orden.Folio + '</td>' +
                        '<td>' + orden.CNTE + '</td>' +
                        '</tr>';
                    tbody.append(row);
                });
				let pedUnicos = Array.from(pedSet);
                $("#txtEmbarques").val(pedUnicos);
				let facUnicos = Array.from(facSet);
                $("#txtFactura").val(facUnicos);
				
				facSet.forEach(function(folio) {
					//console.log("Folio:", folio);
					let rowF = '<tr>' +
						'<td class="folFact">' + folio + '</td>' +
						'<td class="canFact" ><input type="number" class="form-control"  data-id="' + folio + '" value="0"/></td>' +
						'</tr>';
					tbodyF.append(rowF);
				});
				//console.log("facSet:", facSet);
				
                
            },
            error: function (data7, success, error) {
                console.error(data7.responseText);
                alert("Error: " + error);
            }
        });
        return false;
    }
	
	$(document).on('click', '.btnSeleccionar', function(){
		
        let boton = $(this);

        if(boton.data("clicked")) return;
        boton.data("clicked", true);

		let fila = boton.closest('tr');
		let input = fila.find('input[type="number"]');
		
		
		let clave = boton.data('clave');
		let rechazadas = input.val();
		let cajas = boton.data('cajas');
		let pedido = boton.data('folio');
		let cnte = boton.data('cnte');

        let filas = $("#tablaProductos tbody tr");

        //console.log("Pedido: ", pedido);
        //console.log("Clave: ", clave);
        //console.log("Cnte: ", cnte);

        if(filas.length > 0)
        {
            let existe = filas.filter(function(){
                let pedTabla = $(this).find(".pedid").text().trim();
                let idpTabla = $(this).find(".idpro").text().trim();
                let cnteTabla = $(this).find(".cnte").text().trim();
                //console.log("pedTabla: ", pedTabla);
                //console.log("idpTabla: ", idpTabla);
                //console.log("cnteTabla: ", cnteTabla);
                return pedTabla == String(pedido).trim() &&
                       idpTabla == String(clave).trim() &&
                       cnteTabla == String(cnte).trim();
            }).length > 0;

            //console.log("Existe: ", existe);

            if(existe){
                Swal.fire({
                    title: "Queja",
                    text: "El registro ya fue agregado anteriormente.",
                    icon: "warning"
                });
                boton.data("clicked", false);
                return;
            }
        }

        
		
		if(parseFloat(rechazadas) <= 0){
			/*alert("Ingrese la cantidad rechazada");*/
			Swal.fire({
                    title: "Queja",
                    text: "Ingrese la cantidad rechazada",
                    icon: "warning"
                });
            boton.data("clicked", false);
			return;
		}
		
		if(parseFloat(rechazadas) > parseFloat(cajas)){
			/*alert("La cantidad rechazada no puede ser mayor a la cantidad de cajas");*/
			Swal.fire({
                    title: "Queja",
                    text: "La cantidad rechazada no puede ser mayor a la cantidad de cajas",
                    icon: "warning"
                });
            boton.data("clicked", false);
			return;
		}
		
		//console.log("Clave:", clave);
		//console.log("Rechazadas:", rechazadas);
		//console.log("Cajas:", cajas);
		//console.log("Pedido", pedido);
		//console.log("CNTE:", cnte);
		
		var pageUrl = "procesos.asmx/DetalleEmbarqueDetalle";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: pedido, prod: clave, cnte: cnte, rechazo: rechazadas}),
            dataType: 'json',
            success: function (data14) {
                let DetalleProd = data14.d;
                //console.log(DetalleProd);
                let tbody = $("#tablaProductos tbody");
                //tbody.empty();
                $.each(DetalleProd, function(index, orden){			
					if(parseFloat(orden.CantRecha) > 0){
						let row = '<tr data-idprod="' + orden.IdProducto + '">' +
							'<td class="pedid">' + orden.Pedido + '</td>' +
							'<td class="idpro">' + orden.IdProducto + '</td>' +
							'<td class="nompr">' + orden.NomProd + '</td>' +
							'<td class="hiddencol ordenp">' + orden.OrdProd + '</td>' +
							'<td class="hiddencol tarim">' + orden.Tarima + '</td>' +
							'<td class="hiddencol lote">' + orden.Lote + '</td>' +
							'<td class="hiddencol fecha">' + orden.FechaCad + '</td>' +
							'<td class="hiddencol prove">' + orden.ProvCve + '</td>' +
							'<td class="prnom">' + orden.ProvNom + '</td>' +
							'<td class="hiddencol ranch">' + orden.RchCve + '</td>' +
							'<td class="ranom">' + orden.RchNom + '</td>' +
							'<td class="hiddencol tabla">' + orden.TblCve + '</td>' +
							'<td class="hiddencol tanom">' + orden.TblNom + '</td>' +
							'<td class="hiddencol respo">' + orden.Responsable + '</td>' +
							'<td class="hiddencol area">' + orden.Area + '</td>' +
							'<td class="recib">' + orden.CantReci + '</td>' +
							'<td class="recha">' + orden.CantRecha + '</td>' +
							'<td class="produ">' + orden.Producidas + '</td>' +
							'<td class="porce">' + orden.Porcentaje + '</td>' +
							'<td class="hiddencol unida">' + orden.Unidad + '</td>' +
							'<td class="hiddencol tipo">' + orden.Tipo + '</td>' +
							'<td class="hiddencol varie">' + orden.Variedad + '</td>' +
							'<td class="cnte">' + orden.CNTE + '</td>' +
							'<td><button type="button" class="btnBorrar btn btn-primary" data-idprod="' + orden.IdProducto + '">Borrar</button></td>' +
							'</tr>';
						tbody.append(row);
					}
                    
                });
				
                
            },
            error: function (data14, success, error) {
                console.error(data14.responseText);
                alert("Error: " + error);
            },
            complete: function(){
                boton.data("clicked", false);
            }
        });
		
	});
	
	$(document).on('click', '.btnBorrar', function (){
		let boton = $(this);
		let idProd = boton.data('idprod');
		//console.log(idProd);
		$("#tablaProductos tbody tr").filter(function(){
			return $(this).data('idprod') === idProd;
		}).remove();
	});
	
	function SeleccionaMoneda()
    {
        let selectedTipo = $("#ddlTipo option:selected").val();
        //alert(selectedTipo);
		
        if(selectedTipo == "N"){
			$("#ddlMoneda").val("PESOS");
			$("#idCosto").show();
			$("#idSufijo").hide();
		}
        else if(selectedTipo == "E") {
			$("#ddlMoneda").val("DOLARES")
			$("#idCosto").hide();
			$("#idSufijo").show();
		}    
        else{
			$("#ddlMoneda").val('');    
			$("#idCosto").hide();
			$("#idSufijo").hide();
		}
            
    }
	
	function ValidacionesBotonBuscar()
	{
		let i = 0;
		let NalExp = $("#ddlTipo").val();
		let FecCar = $("#txtFechaCarga").val();
		if(NalExp == ""){
			$("#errorTipo").show();
			i++;			
		}
		else{
			$("#errorTipo").hide();
		}
		if(FecCar == ""){
			$("#errorFecha").show();
			i++;			
		}
		else{
			$("#errorFecha").hide();
		}
		return i;
	}
	
	function ValidarControles()
    {
        let i = 0;
        let Cliente = $("#ddlCliente").val();
        if(Cliente === ""){
            i++;
            $("#errorCliente").show();
            //e.preventDefault();
        } else {
            $("#errorCliente").hide();
        }
        let Sucursal = $("#ddlSucursales").val();
        if(Sucursal === ""){
            i++;
            $("#errorSucursal").show();
            //e.preventDefault();
        } else {
            $("#errorSucursal").hide();
        }
        let Reporto = $("#txtReporto").val();
        if(Reporto === ""){
            i++;
            $("#errorReporto").show();
            //e.preventDefault();
        } else {
            $("#errorReporto").hide();
        }
        let Lista = $("#ddlLista").val();
        if(Lista === "") {
            i++;
            $("#errorLista").show();
            //e.preventDefault();
        } else {
            $("#errorLista").hide();
        }
        let Moneda = $("#ddlMoneda").val();
        if(Moneda === "") {
            i++;
            $("#errorMoneda").show();
            //e.preventDefault();
        } else {
            $("#errorMoneda").hide()
        }
        let Problema = $("#ddlProblema").val();
        if(Problema === "") {
            i++;
            $("#errorProblema").show();
            //e.preventDefault();
        } else {
            $("#errorProblema").hide();
        }
		let PlacasTra = $("#ddlPlacas").val();
		//console.log("Validacion Placas:", PlacasTra);
		if(PlacasTra === "0") {
            i++;
            $("#errorPlacas").show();
            //e.preventDefault();
        } else {
            $("#errorPlacas").hide();
        }
		
		
		
        
        let j= 0;
        let archivo1 = $("#fluArchivo1").get(0).files;
        let archivo2 = $("#fluArchivo2").get(0).files;
        let archivo3 = $("#fluArchivo3").get(0).files;
        //VALIDACION SI NINGUNO DE LOS INPUT FILE TIENE CARGADO UN ARCHIVO
        if(archivo1.length === 0 && archivo2.length === 0 && archivo3.length === 0){
            i++;
            $("#errorArchivos").show();
        }
        else
            $("#errorArchivos").hide();

        //VERIFICAR EXTENCIONES PERMITIDAS
        const extPermitidas = ['.jpg', '.jpeg', '.png', '.pdf'];
        let maxSizeBytes = 4 * 1024 * 1024;
        
        if(archivo1.length > 0){
            let nombreFile = archivo1[0].name.toLowerCase();
            let extension = nombreFile.substring(nombreFile.lastIndexOf('.'));
            //console.log(extension);
            if(!extPermitidas.includes(extension)){
                //ERROR DE TIPO DE ARCHIVO NO PERMITIDO
                $("#errorExtensiones").show();
                i++;
            }
            else
                $("#errorExtensiones").hide();

            if(archivo1[0].size > maxSizeBytes){
                $("#errorSize").show();
            }
            else 
                $("#errorSize").hide();

        }
        if(archivo2.length > 0){
            let nombreFile = archivo2[0].name.toLowerCase();
            let extension = nombreFile.substring(nombreFile.lastIndexOf('.'));
            if(!extPermitidas.includes(extension)){
                //ERROR DE TIPO DE ARCHIVO NO PERMITIDO
                $("#errorExtenciones").show();
                i++;
            }
            else {
                $("#errorExtensiones").hide();
            }

            if(archivo2[0].size > maxSizeBytes){
                $("#errorSize").show();
            }
            else 
                $("#errorSize").hide();
        }
        if(archivo3.length > 0){
            let nombreFile = archivo3[0].name.toLowerCase();
            let extension = nombreFile.substring(nombreFile.lastIndexOf('.'));
            if(!extPermitidas.includes(extension)){
                //ERROR DE TIPO DE ARCHIVO NO PERMITIDO
                $("#errorExtensiones").show();
                i++;
            }
            else 
                $("#errorExtensiones").hide();

            if(archivo3[0].size > maxSizeBytes){
                $("#errorSize").show();
            }
            else
                $("#errorSize").hide();
        }
		
		let tieneFilas = $("#tablaProductos tbody tr").length > 0;
		//console.log(tieneFilas);
		if (tieneFilas == false) {
			//console.log("entra");
			$("#errorProductos").show();
		} else {
			$("#errorProductos").hide();
		}
		
		
        return i;
    }
	
	function GuardarQuejaExp(){
	
		
		if(parseFloat(ValidarControles()) > 0){
			Swal.fire({
                    title: "Quejas",
                    text: "Faltan campos requeridos",
                    icon: "error"
                });
			return;
		}
		
		let folio = $("#txtFolio").val();
        let semana = $("#txtSemana").val(); //que_semana
        let fecha = $("#txtFecha").val(); //que_fecha
        //console.log(fecha);
        let mes = $("#txtMes").val(); //que_mes
        let cliprim = $("#ddlCliente").val(); //que_cliprim
        let cliente = $("#txtClienteNom").val(); //que_cliente
        let sucursal = $("#txtSucursalNom").val(); //que_sucursal
        let reporto = $("#txtReporto").val(); //que_reporto
        let recibio = $("#<%=lblClave.ClientID%>").html(); //que_recibio
        let cedis = $("#<%=lblCedis.ClientID%>").html(); //cedis
        let usuario = $("#<%=lblClave.ClientID%>").html(); //resp_usuario
        let tipo = $("#ddlTipo").val(); //que_tipo
        let subclifolio = $("#ddlSucursales").val(); //subcli_folio
        
        let costo = $("#txtCosto").val(); //que_costo
        let pedido = $("#txtEmbarques").val(); //que_pedido
        let observaciones = $("#txtObservaciones").val(); //que_observacion
        let consumidor = $("#chkConsumidor").is(":checked");
        //console.log("reporto", reporto);
		let transporte = $("#txtPlaca").val();
		let factura = $("#txtFactura").val();
		let sufijo = $("#txtSufijo").val();
		
		let levantaQueja = $("#<%=lblNombre.ClientID%>").html(); //quien elaboro la queja
		let problemanom = $("#ddlProblema option:selected").text();
		console.log(problemanom);

        let FechaEm = $("#txtFechaEmb").val();
		
		let product = [];
		let conse = 0;
		$("#tablaProductos tbody tr").each(function(){
			conse++;
			let fila = $(this);
			let pedid = fila.find('.pedid').text().trim();
			let idpro = fila.find('.idpro').text().trim();
			let nompr = fila.find('.nompr').text().trim();
			let orden = fila.find('.ordenp').text().trim();
			let tarim = fila.find('.tarim').text().trim();
			let lote = fila.find('.lote').text().trim();
			let fecha = fila.find('.fecha').text().trim();
			let prove = fila.find('.prove').text().trim();
			let prnom = fila.find('.prnom').text().trim();
			let ranch = fila.find('.ranch').text().trim();
			let ranom = fila.find('.ranom').text().trim();
			let tabla = fila.find('.tabla').text().trim();
			let tanom = fila.find('.tanom').text().trim();
			let respo = fila.find('.respo').text().trim();
			let area = fila.find('.area').text().trim();
			let recib = fila.find('.recib').text().trim();
			let recha = fila.find('.recha').text().trim();
			let produ = fila.find('.produ').text().trim();
			let porce = fila.find('.porce').text().trim();
			let unida = fila.find('.unida').text().trim();
			let tipo = fila.find('.tipo').text().trim();
			let varie = fila.find('.varie').text().trim();
			let cnte = fila.find('.cnte').text().trim();
			
			let foli = folio;
			let prob = $("#ddlProblema").val();
			let devo = 0
			if($("#ddlLista").val() == "DEV")
				devo = 1;
			let merm = 0
			if($("#ddlLista").val() == "MER")
				merm = 1;
			let boni = 0
			if($("#ddlLista").val() == "BON")
				boni = 1;
			let noap = 0
			if($("#ddlLista").val() == "NA")
				noap = 1;
			
			let mone = $("#ddlMoneda").val();
			
			product.push({
				Pedido: pedid,
				ProdCve: idpro,
				Producto: nompr,
				OrdProd: orden,
				Tarima: tarim,
				Lote: lote,
				Fecha: fecha,
				CveProv: prove,
				Proveedor: prnom,
				CveRch: ranch,
				Rancho: ranom,
				CveTbl: tabla,
				Tabla: tanom,
				Responsable: respo,
				Area: area,
				Recibido: recib,
				Rechazadas: recha,
				Producidas: produ,
				Porcentaje: porce,
				Unidad: unida,
				Tipo: tipo,
				Variedad: varie,
				CNTE: cnte,
				Foli: foli,
				Prob: prob,
				Devo: devo,
				Merm: merm,
				Boni: boni,
				Noap: noap,
				Caus: '',
				Mone: mone,
				Cons: conse
			});
		});
		
        if(product.length === 0)
        {
            Swal.fire({
                title: "Queja",
                text: "Detalle de producto vacio, verifique que se hayan cargado y vuelva a dar clic en Guardar",
                icon: "error"
            });
            return;
        }

        let invalidItems = product.filter(p =>
            !p.Pedido || !p.ProdCve || !p.Producto || !p.OrdProd || !p.Tarima || !p.Lote || !p.Fecha || !p.CveProv || !p.Proveedor || !p.CveRch
            || !p.Rancho || !p.CveTbl || !p.Tabla || !p.Responsable || !p.Area || !p.Recibido || !p.Rechazadas || !p.Producidas || !p.Porcentaje
            || !p.Unidad || !p.Tipo || !p.Variedad || !p.CNTE || !p.Foli || !p.Prob || !p.Devo || !p.Merm || !p.Boni || !p.Noap || !p.Caus
            || !p.Mone || !p.Cons
        );
		
        if(invalidItems.length > 0)
        {
            Swal.fire({
                title: "Queja",
                text: "Existen productos con información incompleta. Eliminarlo(s) y volver a cargarlo(s) y vuelva a dar clic en Guardar",
                icon: "error"
            });
            return;
        }

		let facts = [];
		//console.log(tipo);
		if(tipo == "E"){
			console.log("Total filas:", $("#tablaFacturas tbody tr").length);
			$("#tablaFacturas tbody tr").each(function(){
				let fila = $(this);
				let folFact = fila.find('.folFact').text().trim();
				let filaTR = fila.closest('tr');
				let input = fila.find('input[type="number"]');
				let canFact = input.val();				
				facts.push({
					FolFact: folFact,
					CanFact: canFact
				});
			
			});
		}
		
		//console.log("facturas:", facts);
        //alert(consumidor);
		var pageUrl = "procesos.asmx/GuardarQuejaExp";
		//console.log(pageUrl);
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: folio, semana: semana, fecha: fecha,  mes: mes, cliprim: cliprim, cliente: cliente, sucursal: sucursal, reporto: reporto, recibio: recibio, cedis: cedis, usuario: usuario, 
                tipo: tipo, subclifolio: subclifolio, costo: costo, pedido: pedido, transporte: transporte, observaciones: observaciones, fact: factura, sufijo: sufijo, product: product,
				facts: facts, levantaQueja: levantaQueja, problemanom: problemanom, FechaEm: FechaEm }),
			//data: JSON.stringify({product: product}),
            dataType: 'json',
            success: function (data15) {
                $("#txtFolio").val(data15.d);
                //alert("Queja Guardada");
                SubirArchivos();
                Swal.fire({
                    title: "Queja",
                    text: "Datos guardados",
                    icon: "success"
                });
				$("#btnGuardar").prop("disabled", true);
            },
            error: function (xhr, success, error) {
                alert("Error: " + error);
				console.error(xhr.responseText);
                //console.error(data15.d);
            }
        });
        return false;
	}
	
	function SubirArchivos(){
        let archivo1 = $("#fluArchivo1")[0].files[0]; //.get(0).files;
        let archivo2 = $("#fluArchivo2")[0].files[0]; //.get(0).files;
        let archivo3 = $("#fluArchivo3")[0].files[0]; //.get(0).files;
        let formData = new FormData();
        
        //let archivo1new = "1_" + $("#txtFolio").val() + ".pdf";
        
        let archivo1Ext = $("#fluArchivo1").get(0).files;
        let archivo2Ext = $("#fluArchivo2").get(0).files;
        let archivo3Ext = $("#fluArchivo3").get(0).files;

        if(archivo1){
            let archivo1new = "";
            let Name1 = archivo1Ext[0].name.toLowerCase();
            let Ext1 = Name1.substring(Name1.lastIndexOf('.'));
            if(Ext1 === ".png")
                archivo1new = "1_" + $("#txtFolio").val() + ".png";
            else if (Ext1 === ".jpg")
                archivo1new = "1_" + $("#txtFolio").val() + ".jpg";
            else if (Ext1 === ".jpeg")
                archivo1new = "1_" + $("#txtFolio").val() + ".jpeg";
            else
                archivo1new = "1_" + $("#txtFolio").val() + ".pdf";
            formData.append("archivo1", archivo1, archivo1new);
        }
        if(archivo2){
            let archivo2new = "";
            let Name2 = archivo2Ext[0].name.toLowerCase();
            let Ext2 = Name2.substring(Name2.lastIndexOf('.'));
            if(Ext2 === ".png")
                archivo2new = "2_" + $("#txtFolio").val() + ".png";
            else if (Ext2 === ".jpg")
                archivo2new = "2_" + $("#txtFolio").val() + ".jpg";
            else if (Ext2 === ".jpeg")
                archivo2new = "2_" + $("#txtFolio").val() + ".jpeg";
            else
                archivo2new = "2_" + $("#txtFolio").val() + ".pdf";
            formData.append("archivo2", archivo2, archivo2new);
        }
        if(archivo3){
            let archivo3new = "";
            let Name3 = archivo3Ext[0].name.toLowerCase();
            let Ext3 = Name3.substring(Name3.lastIndexOf('.'));
            if(Ext3 === ".png")
                archivo3new = "3_" + $("#txtFolio").val() + ".png";
            else if (Ext3 === ".jpg")
                archivo3new = "3_" + $("#txtFolio").val() + ".jpg";
            else if (Ext3 === ".jpeg")
                archivo3new = "3_" + $("#txtFolio").val() + ".jpeg";
            else
                archivo3new = "3_" + $("#txtFolio").val() + ".pdf";
            formData.append("archivo3", archivo3, archivo3new);
        }
            
        $.ajax({
            url: "procesos.asmx/SubirArchivos",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function(resp){
                /*Swal.fire({
                    title: "Quejas",
                    text: "Archivos subidos",
                    icon: "success"
                });*/
            },
            error: function (xhr, status, error){
                console.error(xhr.responseText);
                alert("Error al subir imagenes");
            }
        })

    }

	document.querySelectorAll("input[type=number]").forEach(input => {
        input.addEventListener("keydown", function(e){
            if(e.key === "Enter"){
                e.preventDefault();
            }
        });
    });
	
	document.querySelectorAll("input[type=text]").forEach(input => {
        input.addEventListener("keydown", function(e){
            if(e.key === "Enter"){
                e.preventDefault();
            }
        });
    });
	
	$(document).on("keydown", "input[type=number]", function(e) {
		if (e.key === "Enter") {
			e.preventDefault();
		}
	});
	
	window.onload = function () {
        FolioQueja();
        Semana();
        Mes();
        Fecha();
        CargarClientes();
        CargarProblemas();
		$("#idCosto").hide();
		$("#idSufijo").hide();
    };
</script>
</html>
