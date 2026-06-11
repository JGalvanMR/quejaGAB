<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="principalnew.aspx.cs" Inherits="queja.principalnew" %>

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
        

    </style>
</head>
<body>
    <div class="container">
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
											<label for="txtOrdenProd" class="col-sm-2 control-label">Orden de prod:</label>
											<div class="col-sm-9">
                                                <input type="text" id="txtOrdenProd" class="form-control" style="text-transform: uppercase" placeholder="ORDEN DE PRODUCCION" />
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-12">
												<center>
                                                    <button type="button" id="btnBuscaOrden" class="btn btn-primary" onclick="CargarOrdenProduccion()">Buscar</button>
												</center>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-sm-offset-0 col-sm-12">
                                                <table id="tablaOrdenProd" class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Clave</th>
                                                            <th>Producto</th>
                                                            <th>Fecha</th>
                                                            <th>Fecha Cad</th>
                                                            <th>Tipo</th>
                                                            <th class="hiddencol">Prov</th>
                                                            <th class="hiddencol">ProvNom</th>
                                                            <th class="hiddencol">Ranch</th>
                                                            <th class="hiddencol">RanchNom</th>
                                                            <th class="hiddencol">Tabla</th>
                                                            <th class="hiddencol">TablaNom</th>
                                                            <th class="hiddencol">Lote</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
												<%--<asp:UpdatePanel ID="upnOrden" runat="server">
													<ContentTemplate>
														<asp:GridView ID="gvwOrden" runat="server" AutoGenerateColumns="false" 
															Width="100%" CssClass="table table-striped table-bordered table-hover" 
															EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true">
															<HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
															<Columns>
																<asp:ButtonField HeaderText="Clave" DataTextField="prod_clave" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="muestradatos" />
																<asp:BoundField HeaderText="Clave_2" DataField="prod_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
																<asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="prod_nombre"/>
																<asp:BoundField HeaderText="Fecha" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="pti_fecha"/>
																<asp:BoundField HeaderText="Cve Prov" DataField="prov_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
																<asp:BoundField HeaderText="Proveedor" DataField="prov_nombre" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
																<asp:BoundField HeaderText="Cve Rch" DataField="rch_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
																<asp:BoundField HeaderText="Rancho" DataField="rch_nombre" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
																<asp:BoundField HeaderText="Cve Tbl" DataField="tbl_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
																<asp:BoundField HeaderText="Tabla" DataField="tbl_nombre" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
																<asp:BoundField HeaderText="Fecha Cad" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="fecha_cad"/>
																<asp:BoundField HeaderText="Tipo" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="tipo"/>
																<asp:BoundField HeaderText="Lote" DataField="lote" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
															</Columns>
														</asp:GridView>
													</ContentTemplate>
													<<Triggers>
														<asp:AsyncPostBackTrigger ControlID="btnBuscaOrden" />
													</Triggers>>
												</asp:UpdatePanel>
												<asp:UpdateProgress ID="UpdateProgress1" runat="server">
													<ProgressTemplate>
														<div class="alert alert-info">
														  <strong>CARGANDO...</strong>Se esta buscando la informaci&oacute;n
														</div>
													</ProgressTemplate>
												</asp:UpdateProgress>--%>
											</div>
										</div>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="txt_clave" class="col-sm-2 control-label">Clave</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_clave" class="form-control" style="text-transform: uppercase" placeholder="CLAVE" readonly="readonly" />
                                                <span id="errorClave" style="color:red; display:none;">Debe ingresar una orden de produccion y un producto del listado</span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_nom" class="col-sm-2 control-label">Producto</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_nom" class="form-control" style="text-transform: uppercase" placeholder="PRODUCTO" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_lote" class="col-sm-2 control-label">Lote</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_lote" class="form-control" style="text-transform: uppercase" placeholder="LOTE" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_fechacad" class="col-sm-2 control-label">Fecha Caducidad</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_fechacad" class="form-control" style="text-transform: uppercase" placeholder="FECHA CADUCIDAD" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_cveprov" class="col-sm-2 control-label">Cve Proveedor</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_cveprov" class="form-control" style="text-transform: uppercase" placeholder="CLAVE PROVEEDOR" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_nomprov" class="col-sm-2 control-label">Nom Proveedor</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_nomprov" class="form-control" style="text-transform:uppercase" placeholder="PROVEEDOR" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_cverch" class="col-sm-2 control-label">Clave Rancho</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_cverch" class="form-control" style="text-transform: uppercase" placeholder="Clave Rancho" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_nomrch" class="col-sm-2 control-label">Nombre Rancho</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_nomrch" class="form-control" style="text-transform:uppercase" placeholder="Rancho" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_cvetbl" class="col-sm-2 control-label">Clave Tabla</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_cvetbl" class="form-control" style="text-transform:uppercase" placeholder="Clave Tabla" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txt_nomtbl" class="col-sm-2 control-label">Nombre Tabla</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txt_nomtbl" class="form-control" style="text-transform:uppercase" placeholder="Nombre Tabla" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtTipo" class="col-sm-2 control-label">Tipo</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtTipo" class="form-control" style="text-transform:uppercase" placeholder="Tipo" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtVariedad" class="col-sm-2 control-label">Variedad</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtVariedad" class="form-control" style="text-transform:uppercase" placeholder="Variedad" readonly="readonly" />
                                            </div>
                                        </div>
										<asp:UpdatePanel ID="upnRegistro" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
											</ContentTemplate>
											<%--<Triggers>
													<asp:AsyncPostBackTrigger ControlID="gvwOrden" EventName="RowCommand" />
											</Triggers>--%>
										</asp:UpdatePanel>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="txtRespLinea" class="col-sm-2 control-label">Resp L&iacute;nea</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtRespLinea" class="form-control" style="text-transform: uppercase" placeholder="Responsable L&iacute;nea" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtArea" class="col-sm-2 control-label">Area</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtArea" class="form-control" style="text-transform: uppercase" placeholder="Area" readonly="readonly" />
                                            </div>
                                        </div>
										<asp:UpdatePanel  ID="upnDatos" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div class="form-group">
													<asp:HiddenField ID="txtRespLinea2" runat="server" />
												</div>
												<div class="form-group">
														<asp:HiddenField ID="txtArea2" runat="server" />
												</div>
											</ContentTemplate>
											<%--<Triggers>
												<asp:AsyncPostBackTrigger ControlID="btnBuscaOrden" />
											</Triggers>--%>
										</asp:UpdatePanel>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="txtCantReci" class="col-sm-2 control-label">Cant Recibida</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtCantReci" class="form-control" style="text-transform:uppercase" placeholder="Cantidad Recibida" onkeyup="VerificaProdRecibidas()" />
                                            </div>
                                        </div>											
											<%--div class="col-sm-9">
												<asp:RequiredFieldValidator ID="rfvCantReci" unat="server" ErrorMessage="Campo requerido"  ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator ID="revCantReci" unat="server" ErrorMessage="Solo números" ValidationExpression="^[0-9]*\.?[0-9]+$"  ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
											</di--%>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="txtCantRech" class="col-sm-2 control-label">Cant Rechazada</label>
                                            <div class="col-sm-9">
                                                <input type="number" id="txtCantRech" class="form-control" style="text-transform:uppercase" placeholder="Cantidad Rechazada" max="1000" min="1" onkeyup="CalcularPorcentaje()"/>
                                            </div>
                                        </div>
<%--										<asp:RequiredFieldValidator ID="rfvCantRech" unat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCantRech" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revCantRech" unat="server" ErrorMessage="Solo números" ValidationExpression="^[0-9]*\.?[0-9]+$" ControlToValidate="txtCantRech" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
--%>									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="txtCajasProducidas" class="col-sm-2 control-label">Cajas Prod.</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtCajasProducidas" class="form-control" style="text-transform: uppercase" placeholder="Cajas Producidas" readonly="readonly" />
                                            </div>
                                        </div>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="txtPorcentaje" class="col-sm-2 control-label">% Rechazo</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtPorcentaje" class="form-control" style="text-transform: uppercase" placeholder="Porcentaje Rechazado" readonly="readonly" />
                                                <span id="errorPorcentaje" style="color:red; display:none;">Debe ingresar las cajas recibidas y rechazadas y el porcentaje debe ser mayor a cero</span>
                                            </div>
                                            <div class="spinner-border" role="status">
												<span class="sr-only">Loading...</span>
											</div>
                                        </div>
									</div>
									<div class="row">
                                        <div class="form-group">
                                            <label for="txtUnidad" class="col-sm-2 control-label">Unidad</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtUnidad" class="form-control" style="text-transform: uppercase" placeholder="Unidada" value="CAJA" readonly="readonly" />
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
                                                <select id="ddlMoneda" class="form-control">
                                                    <option value="">SELECCIONA MONEDA...</option>
                                                    <option value="PESOS">PESOS</option>
                                                    <option value="DOLARES">DOLARES</option>
                                                    <%-- Este se cambia segun si es nacional o exportacion la queja --%>
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
									<div class="row">
                                        <div class="form-group">
                                            <label for="txtPedido" class="col-sm-2 control-label">Pedido</label>
                                            <div class="col-sm-9">
                                                <input type="text" id="txtFactPed" /> 
                                                <input type="text" id="txtPedido" class="form-control" style="text-transform:uppercase" placeholder="Pedido" maxlength="10" /> 
                                                <span id="errorPedido" style="color:red; display:none;">Debe ingresar el pedido</span>
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
											<label for="txtCosto" class="col-sm-2 control-label">Costo</label>
											<div class="col-sm-9">
                                                <input type="number" id="txtCosto" class="form-control" style="text-transform: uppercase" placeholder="Costo" value="0" step="0.01" />
                                               <span id="errorCosto" style="color:red; display:none">Debe ingresar el costo</span>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<label for="chkConsumidor" class="col-sm-2 control-label">¿Consumidor?</label>
											<div class="col-sm-2">
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" id="chkConsumidor"> Si
                                                    </label>
                                                </div>
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
                                                    <button type="button" id="btnGuardar" class="btn btn-primary" onclick="GuardarQueja()">Guardar</button>
												</center>
											</div>
										</div>
									</div>
                                    
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-3">
                                                <label class="col-sm-1 label label-success"><div id="lblVerifica">0</div></label>
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
                console.log(items);
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
            data: JSON.stringify({clave: $("#ddlCliente").val(), cedis: $("#<%=lblCedis.ClientID%>").html()}),
            dataType: 'json',
            success: function (data6) {
                //alert(data5.d);
                //onSuccess5(data5);
                let items = data6.d;
                console.log(items);
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

    function SeleccionaMoneda()
    {
        let selectedTipo = $("#ddlTipo option:selected").val();
        //alert(selectedTipo);
        if(selectedTipo == "N")
            $("#ddlMoneda").val("PESOS");
        else if(selectedTipo == "E")
            $("#ddlMoneda").val("DOLARES")
        else
            $("#ddlMoneda").val('');    
    }

    function CargarOrdenProduccion()
    {
    
        if($("#txtOrdenProd").val() == '')
        {
            alert("Debe ingresar el folio de la orden de produccion");
            return;
        }

        var pageUrl = "procesos.asmx/CargarOrdenProduccion";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: $("#txtOrdenProd").val()}),
            dataType: 'json',
            success: function (data7) {
                //alert(data5.d);
                //onSuccess5(data5);
                let ordenProd = data7.d;
                console.log(ordenProd);
                let tbody = $("#tablaOrdenProd tbody");
                tbody.empty();
                let tip;
                $.each(ordenProd, function(index, orden){
                    tip = orden.Tipo;
                    let row = '<tr>' +
                        '<td><button type="button" class="btnSeleccionar btn btn-primary" ' +
                            'data-clave="' + orden.Clave + '" ' +
                            'data-nombre="' + orden.Nombre + '" ' +
                            'data-fecha="' + orden.Fecha + '" ' +
                            'data-fechacad="' + orden.FechaCad + '" ' +
                            'data-tipo="' + orden.Tipo + '" ' +
                            'data-prov="' + orden.Prov + '" ' +
                            'data-provnom="' + orden.ProvNom + '" ' +
                            'data-ranch="' + orden.Ranch + '" ' +
                            'data-ranchnom="' + orden.RanchNom + '" ' +
                            'data-tabla="' + orden.Tabla + '" ' +
                            'data-tablanom="' + orden.TablaNom + '" ' +
                            'data-lote="' + orden.Lote + '" ' +
                            '>' + orden.Clave + '</button></td>' +
                        '<td>' + orden.Nombre + '</td>' +
                        '<td>' + orden.Fecha + '</td>' +
                        '<td>' + orden.FechaCad + '</td>' +
                        '<td>' + orden.Tipo + '</td>' +
                        '<td class="hiddencol">' + orden.Prov + '</td>' +
                        '<td class="hiddencol">' + orden.ProvNom + '</td>' +
                        '<td class="hiddencol">' + orden.Ranch + '</td>' +
                        '<td class="hiddencol">' + orden.RanchNom + '</td>' +
                        '<td class="hiddencol">' + orden.Tabla + '</td>' +
                        '<td class="hiddencol">' + orden.TablaNom + '</td>' +
                        '<td class="hiddencol">' + orden.Lote + '</td>'
                        '</tr>';
                    tbody.append(row);
                });
                CargarDatosOrden(tip);
                
            },
            error: function (data7, success, error) {
                console.error(data7.responseText);
                alert("Error: " + error);
            }
        });
        return false;
    }

    $(document).on('click', '.btnSeleccionar', function(){
        let clave = $(this).data('clave');
        let producto = $(this).data('nombre');
        let fecha = $(this).data('fecha');
        let fechacad = $(this).data('fechacad');
        let tipo = $(this).data('tipo');
        let prov = $(this).data('prov');
        let provnom = $(this).data('provnom');
        let ranch = $(this).data('ranch');
        let ranchnom = $(this).data('ranchnom');
        let tabla = $(this).data('tabla');
        let tablanom = $(this).data('tablanom');
        let lote = $(this).data('lote');
        $("#txt_clave").val(clave);
        $("#txt_nom").val(producto);
        $("#txt_lote").val(lote);
        $("#txt_fechacad").val(fechacad);
        $("#txt_cveprov").val(prov);
        $("#txt_nomprov").val(provnom);
        $("#txt_cverch").val(ranch);
        $("#txt_nomrch").val(ranchnom);
        $("#txt_cvetbl").val(tabla);
        $("#txt_nomtbl").val(tablanom);
        $("#txtTipo").val(tipo);

        CargarVariedad(tipo);
        CargarCajasProducidas(tipo);
    });
    
    function CargarDatosOrden(tipo) {
        var pageUrl = "procesos.asmx/CargarDatosOrden";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: $("#txtOrdenProd").val(), tipo_recibo: tipo}),
            dataType: 'json',
            success: function (data8) {
                let items = data8.d;
                console.log(items);
                $.each(items, function(index, item){
                    $("#txtRespLinea").val(item.Responsable);
                    $("#txtArea").val(item.Linea);
                });
            },
            error: function (data8, success, error) {
                alert("Error: " + error);
            }
        });
        return false;
    }

    function CargarVariedad(tipo) {
        var pageUrl = "procesos.asmx/CargarVariedad";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: $("#txtOrdenProd").val(), prod: $("#txt_clave").val(),  tipo: tipo}),
            dataType: 'json',
            success: function (data9) {
                let items = data9.d;
                console.log(items);
                $.each(items, function(index, item){
                    $("#txtVariedad").val(item.Variedad);
                });
            },
            error: function (data9, success, error) {
                alert("Error: " + error);
            }
        });
        return false;
    }

    function CargarCajasProducidas(tipo) {
        var pageUrl = "procesos.asmx/CargarCajasProducidas";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: $("#txtOrdenProd").val(), prod: $("#txt_clave").val(),  tar: '', tipo: tipo}),
            dataType: 'json',
            success: function (data10) {
                let items = data10.d;
                console.log(items);
                $.each(items, function(index, item){
                    $("#txtCajasProducidas").val(item.Producidas);
                });
            },
            error: function (data10, success, error) {
                alert("Error: " + error);
            }
        });
        return false;
    }

    function VerificaProdRecibidas()
    {
        
        let cajasprod = parseFloat($("#txtCajasProducidas").val());
        let cajasreci = parseFloat($("#txtCantReci").val());

        //alert(cajasreci);
        if(cajasreci == "")
        {
            alert('Ingresar las cajas recibidas');
            $("#txtCajasRecibidas").val('0');
            return;
        }

        if(cajasreci == 0)
        {
            alert('Ingresar las cajas recibidas');
            return;
        }

        if(cajasreci > cajasprod)
        {
            alert('Las cajas recibidas no puede ser mayores a las producidas');
            $("#txtCantReci").val('0');
            $("#txtCantRech").val('0');
            return;
        }
        //alert("sale");
    }

    function CalcularPorcentaje()
    {
        let cajasprod = parseFloat($("#txtCajasProducidas").val());
        let cajasreci = parseFloat($("#txtCantReci").val());
        let cajasrech = parseFloat($("#txtCantRech").val());

        if(cajasreci == "")
        {
            alert('Ingresar las cajas recibidas');
            $("#txtCajasRecibidas").val('0');
            return;
        }
        if(cajasreci == 0)
        {
            alert('Ingresar las cajas recibidas')
            return;
        }


        if(cajasrech > cajasreci)
        {
            alert('Las cajas rechazadas no puede ser mayor a las recibidas');
            $("#txtCantRech").val('0');
            return;
        }

        porcentaje = (cajasrech * 100) / cajasprod;

        if(isNaN(porcentaje))
            $("#txtPorcentaje").val('0');
        else
            $("#txtPorcentaje").val(porcentaje.toFixed(2));

        
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
                console.log(items);
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

    document.getElementById("txtCosto").addEventListener("input", function(){
        const valor = this.value;
        let valorF = parseFloat(valor);
        if(valor.includes('.')){
            const partes = valor.split('.');
            if(partes[1].length > 2)
                $("#txtCosto").val(valorF.toFixed(2))
        }
        //alert(valor);
//        if(valor == '')
//        {
//            $("#txtCosto").val('0');
//        }
    });

    document.querySelectorAll("input[type=text]").forEach(input => {
        input.addEventListener("keydown", function(e){
            if(e.key === "Enter"){
                e.preventDefault();
            }
        });
    });

    document.querySelectorAll("input[type=number]").forEach(input => {
        input.addEventListener("keydown", function(e){
            if(e.key === "Enter"){
                e.preventDefault();
            }
        });
    });

    

    function GuardarQueja()
    {
        //alert(ValidarControles());
        if(parseFloat(ValidarControles()) > 0)
        {
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
        console.log(fecha);
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
        console.log("subclifolio: ", subclifolio);
        let costo = $("#txtCosto").val(); //que_costo
        let pedido = $("#txtPedido").val(); //que_pedido
        let observaciones = $("#txtObservaciones").val(); //que_observacion
        let consumidor = $("#chkConsumidor").is(":checked");
        console.log("reporto", reporto);
        //alert(consumidor);

        let producto = $("#txt_clave").val();
        let problema = $("#ddlProblema").val();
        let ordprod = $("#txtOrdenProd").val();
        let area = $("#txtArea").val();
        let responsable = $("#txtRespLinea").val();
        let cantrecha = $("#txtCantRech").val();
        let cantreci = $("#txtCantReci").val();
        let unidad = $("#txtUnidad").val();
        let devolucion = "0";
        if($("#ddlLista").val() === "DEV"){
            devolucion = "1";
        }
        let moneda = $("#ddlMoneda").val();
        let cveprov = $("#txt_cveprov").val();
        let cverch = $("#txt_cverch").val();
        let cvetbl = $("#txt_cvetbl").val();
        let variedad = $("#txtVariedad").val();
        let lote = $("#txt_lote").val();
        let nomprod = $("#txt_nom").val();
        let fechacad = $("#txt_fechacad").val();
        console.log(fechacad);
        let ptcptp = $("#txtTipo").val();
        let cjsprod = $("#txtCajasProducidas").val();
        let porcen = $("#txtPorcentaje").val();
        let merma = "0";
        if($("#ddlLista").val() === "MER"){
            merma = "1"
        }
        let bonificacion = "0";
        if($("#ddlLista").val() === "BON"){
            bonificacion = "1";
        }
        let rechazo = $("#txtCantRech").val();
        let noaplica = "0";
        if($("#ddlLista").val() === "NA"){
            noaplica = "1";
        }           
        
        let nombre = $("#<%=lblNombre.ClientID%>").html();
        let problemanom = $("#ddlProblema option:selected").text();

        /*ValidarPedido(pedido, tipo, recibio, producto);

        let val_ped_fac = $("#txtFactPed").val();
        console.log("valores: " + val_ped_fac);

        let factu = "";
        let pedid = "";
        let fchpf = "";

        if(val_ped_fac == "0")
        {
            Swal.fire({
                    title: "Quejas",
                    text: "El folio ingresado en el pedido no existe",
                    icon: "error"
                });
				return;
        }

        let valores = val_ped_fac.split("*");
        factu = valores[1];
        pedid = valores[0];
        fchpf = valores[2];

        console.log("values: "+ valores);

        return;*/


        var pageUrl = "procesos.asmx/GuardarQueja";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({folio: folio, semana: semana, fecha: fecha,  mes: mes, cliprim: cliprim, cliente: cliente, sucursal: sucursal, reporto: reporto, recibio: recibio, cedis: cedis, usuario: usuario, 
                tipo: tipo, subclifolio: subclifolio, pedido: pedido, observaciones: observaciones, costo: costo, consumidor: consumidor, producto: producto, problema: problema, ordprod: ordprod, area: area,
                responsable: responsable, cantrecha: cantrecha, cantreci: cantreci, unidad: unidad, devolucion: devolucion, moneda: moneda, cveprov: cveprov, cverch: cverch, cvetbl: cvetbl, variedad: variedad,
                lote: lote, nomprod: nomprod, fechacad: fechacad, ptcptp: ptcptp, cjsprod: cjsprod, porcen: porcen, merma: merma, bonificacion: bonificacion, rechazo: rechazo, noaplica: noaplica,
                nombre: nombre, problemanom: problemanom }),
            dataType: 'json',
            success: function (data12) {
                console.log(data12.d);

                if(data12.d == "NOPED")
                {
                    $("#txtFolio").val(folio);
                    Swal.fire({
                    title: "Quejas",
                    text: "El folio ingresado en el pedido no existe",
                    icon: "error"
                     });
				    return;
                    
                }
                else { 
                    $("#txtFolio").val(data12.d);
                    //alert("Queja Guardada");
                    SubirArchivos();
                    Swal.fire({
                        title: "Queja",
                        text: "Datos guardados",
                        icon: "success"
                    });
				    $("#btnGuardar").prop("disabled", true);
                }   
                
            },
            error: function (data12, success, error) {
                alert("Error: " + error);
                console.error(data12.d);
            }
        });
        return false;


    }

    function ValidarPedido(pedido, expnal, clave, producto)
    {
        var pageUrl = "procesos.asmx/ValidarIngresoPedido";
        var ret = "";
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: pageUrl,
            data: JSON.stringify({pedido : pedido, expnal : expnal, clave : clave, producto : producto}),
            dataType: 'json',
            success: function (data12) {
                //ret = data12.d;
                //console.log(ret);
                $("#txtFactPed").val(data12.d);
            },
            error: function (data12, success, error) {
                console.error(data12.responseText);
                //alert("Error: " + error);
                return "0";
            }
        });
        return "0";
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

    window.onload = function () {
        FolioQueja();
        Semana();
        Mes();
        Fecha();
        CargarClientes();
        CargarProblemas();
    };

    function ValidarControles()
    {
        let i = 0;
        let Cliente = $("#ddlCliente").val();
		console.log("Cliente: ", Cliente);
        if(Cliente === "" || Cliente === "SELECCIONAR CLIENTE..."){
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
        let Tipo = $("#ddlTipo").val();
        if(Tipo === ""){
            i++;
            $("#errorTipo").show();
            //e.preventDefault();
        } else {
            $("#errorTipo").hide();
        }
        let Clave = $("#txt_clave").val();
        if(Clave === "") {
            i++;
            $("#errorClave").show();
            //e.preventDefault();
        } else {
            $("#errorClave").hide();
        }
        let Porcentaje = $("#txtPorcentaje").val();
        if(Porcentaje === "") {
            i++;
            $("#errorPorentaje").show();
            //e.preventDefault();
        } else {
            $("#errorPorcentaje").hide();
        }
        if(Porcentaje === "") {
            i++;
            $("#errorPorcentaje").show();
            //e.preventDefault();
        } else {
            $("#errorPorcentaje").hide();
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
        let Pedido = $("#txtPedido").val();
        //console.log("Pedido: ", Pedido)
        if($("#ddlLista").val() !== "NA")
        {
            if(Pedido === "") {
                //console.log("Pedido 2: ", Pedido);
                i++;
                $("#errorPedido").show();
                //e.preventDefault();
            } else {
                $("#errorPedido").hide();
            }
        }
        else {
            $("#errorPedido").hide();
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
            console.log(extension);
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



        
            


        /*let Costo = parseFloat($("#txtCosto").val());
        if(Costo === 0) {
            $("#errorCosto").show();
            i++;
            e.preventDefault();
        } else {
            $("#errorCosto").hide();
        }*/
        return i;
    }
    
    
</script>
</html>
