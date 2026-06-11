<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="principal.aspx.cs" Inherits="queja.principal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="alertifyjs/css/alertify.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="alertifyjs/alertify.js" type="text/javascript"></script>
    <script src="Scripts/sweetalert.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
        /*$("<%=btnGuardar.ClientID%>").disabled = true;
        function deshabilita() {
            $("<%=btnGuardar.ClientID%>").disabled = true;
        }*/
        
        /*function exito() {
            //alertify.alert('Datos Guardados!', function () { alertify.success('OK'); });
            swal("Queja Guardada!", "", "success");
        }
        function peligro() {
            swal("Queja no guardada", "", "error");
        }

        function advertencia() {
            swal("Queja no guardada", "", "warning");
        }

        function informacion() {
            swal("El folio de la queja cambio por movimientos en la red", "", "info");
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
                        <center><h3><strong class="">Descripción Queja</strong></h3></center>
                    </div>
                    <div class="panel-body">
                        <form id="Form1" runat="server" class="form-horizontal" enctype="multipart/form-data">
                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                            </asp:ScriptManager>
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
                                    <div class="form-group">
                                        <div class="col-sm-offset-3 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label for="txtFolio" class="col-sm-3 control-label">Folio</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="uplFolio" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtFolio" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Folio" ReadOnly="True"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                    <div class="form-group">
                                        <label for="txtSemana" class="col-sm-3 control-label">Semana</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtSemana" CssClass="form-control" 
                                                Style="text-transform: uppercase" placeholder="Semana" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                    <div class="form-group">
                                        <label for="txtMes" class="col-sm-3 control-label">Mes</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtMes" CssClass="form-control" 
                                                Style="text-transform: uppercase" placeholder="Mes" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtFecha" class="col-sm-3 control-label">Fecha</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" 
                                                Style="text-transform: uppercase" placeholder="Fecha" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlCliente" class="col-sm-3 control-label">Cliente</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" 
                                                onselectedindexchanged="ddlCliente_SelectedIndexChanged" AutoPostBack="true" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlSucursales" class="col-sm-3 control-label">Sucursal</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="upnSucursales" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlSucursales" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSucursales" runat="server" ErrorMessage="Debe elegir una opción" ControlToValidate="ddlSucursales" ForeColor="Red" InitialValue="" ValidationGroup="grupo1">
                                                    </asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:TextBox runat="server" ID="txtSucursal" Visible="false" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Sucursal" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtReporto" class="col-sm-3 control-label">Report&oacute;</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtReporto" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Report&oacute;" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReporto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtReporto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlTipo" class="col-sm-3 control-label">Tipo</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" 
                                                AutoPostBack="True" onselectedindexchanged="ddlTipo_SelectedIndexChanged" >
                                                <asp:ListItem Value="">ELEGIR OPCIÓN...</asp:ListItem>
                                                <asp:ListItem Value="N">NACIONAL</asp:ListItem>
                                                <asp:ListItem Value="E">EXPORTACION</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <p class="bg-primary text-center"><strong>Descripci&oacute;n del producto</strong></p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtOrdenProd" class="col-sm-3 control-label">Orden de producci&oacute;n</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtOrdenProd" CssClass="form-control" Style="text-transform: uppercase" placeholder="Orden de producci&oacute;n" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <center>
                                                <asp:Button runat="server" ID="btnBuscaOrden" Text="Buscar" 
                                                    CssClass="btn btn-primary" onclick="btnBuscaOrden_Click" />
                                            </center>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-0 col-sm-12">
                                            <asp:UpdatePanel ID="upnOrden" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwOrden" runat="server" AutoGenerateColumns="false" 
                                                        Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowcommand="gvwOrden_RowCommand"  >
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
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscaOrden" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <div class="alert alert-info">
                                                      <strong>CARGANDO...</strong>Se esta buscando la informaci&oacute;n
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="upnRegistro" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <label for="txt_clave" class="col-sm-3 control-label">Clave</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_clave" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Clave" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_nom" class="col-sm-3 control-label">Producto</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_nom" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Producto" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_lote" class="col-sm-3 control-label">Lote</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_lote" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Lote" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_fechacad" class="col-sm-3 control-label">Fecha Caducidad</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_fechacad" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Fecha caducidad" 
                                                        ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_cveprov" class="col-sm-3 control-label">Clave Proveedor:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_cveprov" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Clave Proveedor" 
                                                        ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_nomprov" class="col-sm-3 control-label">Nombre Proveedor:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_nomprov" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Proveedor" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_cverch" class="col-sm-3 control-label">Clave Rancho:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_cverch" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Clave Rancho" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_nomrch" class="col-sm-3 control-label">Nombre Rancho:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_nomrch" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Rancho" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_cvetbl" class="col-sm-3 control-label">Clave Tabla:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_cvetbl" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Clave Tabla" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txt_nomtbl" class="col-sm-3 control-label">Nombre Tabla:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txt_nomtbl" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Tabla" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtTipo" class="col-sm-3 control-label">Tipo:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtTipo" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Tipo" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtVariedad" class="col-sm-3 control-label">Variedad:</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtVariedad" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Tipo" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </ContentTemplate>
                                        <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gvwOrden" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel  ID="upnDatos" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <label for="txtRespLinea" class="col-sm-3 control-label">Responsable L&iacute;nea</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtRespLinea" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Responsable L&iacute;nea" 
                                                        ReadOnly="True"></asp:TextBox>
                                                    <asp:HiddenField ID="txtRespLinea2" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtArea" class="col-sm-3 control-label">Area</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtArea" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Area" ReadOnly="True"></asp:TextBox>
                                                    <asp:HiddenField ID="txtArea2" runat="server" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnBuscaOrden" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group">
                                        
                                        <label for="txtCantReci" class="col-sm-3 control-label">Cant Recibida</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="upnReci" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtCantReci" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Cantidad recibida" 
                                                        onkeydown = "return (event.keyCode!=13)" 
                                                        ontextchanged="txtCantReci_TextChanged"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                            <asp:RequiredFieldValidator ID="rfvCantReci" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCantReci" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCantReci" runat="server" ErrorMessage="Solo números" ValidationExpression="^[0-9]*\.?[0-9]+$" ControlToValidate="txtCantReci" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtCantRech" class="col-sm-3 control-label">Cant Rechazada</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="upnRecha" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtCantRech" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Cantidad rechazada" 
                                                        onkeydown = "return (event.keyCode!=13)" 
                                                        ontextchanged="txtCantRech_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:RequiredFieldValidator ID="rfvCantRech" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCantRech" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCantRech" runat="server" ErrorMessage="Solo números" ValidationExpression="^[0-9]*\.?[0-9]+$" ControlToValidate="txtCantRech" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                    <asp:UpdatePanel  ID="upnCajasProducidas" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <label for="txtCantRech" class="col-sm-3 control-label">Cajas Prod.</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="txtCajasProducidas" CssClass="form-control" Style="text-transform: uppercase" placeholder="Cajas Producidas" onkeydown = "return (event.keyCode!=13)" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group">
                                        <asp:UpdatePanel  ID="upnPorcentaje" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <label for="txtCantRech" class="col-sm-3 control-label">% Rechazo</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtPorcentaje" CssClass="form-control" Style="text-transform: uppercase" placeholder="Porcentaje Rechazado" onkeydown = "return (event.keyCode!=13)" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="spinner-border" role="status">
                                            <span class="sr-only">Loading...</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtUnidad" class="col-sm-3 control-label">Unidad</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtUnidad" CssClass="form-control" Style="text-transform: uppercase" placeholder="Unidad" onkeydown = "return (event.keyCode!=13)" ReadOnly="true" Text="CAJA"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUnidad" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtUnidad" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group nover">
                                        <label for="chkDevolucion" class="col-sm-3 control-label">¿Devoluci&oacute;n?</label>
                                        <div class="col-sm-2">
                                            <asp:CheckBox runat="server" ID="chkDevolucion" CssClass="form-control" Text="Si"/>
                                        </div>
                                    </div>
                                    <div class="form-group nover">
                                        <label for="chkMerma" class="col-sm-3 control-label">¿Merma?</label>
                                        <div class="col-sm-2">
                                            <asp:CheckBox runat="server" ID="chkMerma" CssClass="form-control" Text="Si"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="rbtList" class="col-sm-3 control-label">¿Seleccionar opción?</label>
                                        <div class="col-sm-8">
                                            <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="rbtList" runat="server" CssClass="radio">
                                                <asp:ListItem Value="DEV" class="radio-inline" Text="Devolución"></asp:ListItem>
                                                <asp:ListItem Value="MER" class="radio-inline" Text="Merma"></asp:ListItem>
                                                <asp:ListItem Value="BON" class="radio-inline" Text="Bonificación"></asp:ListItem>
                                                <asp:ListItem Value="NA" class="radio-inline" Text="No Aplica"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlMoneda" class="col-sm-3 control-label">Moneda</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="uplMoneda" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlMoneda" CssClass="form-control">
                                                        <asp:ListItem  Value="PESOS">PESOS</asp:ListItem>
                                                        <asp:ListItem  Value="DOLARES">DOLARES</asp:ListItem>
                                                    </asp:DropDownList>    
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlTipo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlProblema" class="col-sm-3 control-label">Problema</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="ddlProblema" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtPedido" class="col-sm-3 control-label">Pedido</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="uplPedido" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtPedido" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Pedido"
                                                        onkeydown="return (event.keyCode!=13)" Text="" MaxLength="10" 
                                                          AutoPostBack="true" ></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvPedido" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtPedido" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtObservaciones" class="col-sm-3 control-label">Observaciones</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtObservaciones" CssClass="form-control" 
                                                Style="text-transform: uppercase"  placeholder="Observaciones" 
                                                onkeydown = "return (event.keyCode!=13)" MaxLength="200"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtCosto" class="col-sm-3 control-label">Costo</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtCosto" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Costo" onkeydown = "return (event.keyCode!=13)" Text="0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCosto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCosto" runat="server" ErrorMessage="Error de ingreso de número" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1" ValidationExpression="\d{1,10}(.\d{1,2})?"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="chkConsumidor" class="col-sm-3 control-label">¿Queja de consumidor?</label>
                                        <div class="col-sm-2">
                                            <asp:CheckBox runat="server" ID="chkConsumidor" CssClass="form-control" Text="Si"/>
                                        </div>
                                    </div>
                                    <br />

                                    <%--div class="row">
                                        <div class="form-group">
                                            <label for="fluArchivo1" class="col-sm-3 control-label text-right">Archivo 1</label>
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="fluArchivo1" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="fluArchivo2" class="col-sm-3 control-label text-right">Archivo 2</label>
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="fluArchivo2" runat="server" class="form-control"/>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="fluArchivo3" class="col-sm-3 control-label text-right">Archivo 3</label>
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="fluArchivo3" runat="server" class="form-control"/>
                                            </div>
                                        </div>
                                    </div--%>
                                    
                                    <%--<div class="form-group">
                                        <label for="ddlAreaQueja" class="col-sm-3 control-label">Área Queja</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="ddlAreaQueja" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <center>
                                                <asp:UpdatePanel ID="upnGuardar" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                            ValidationGroup="grupo1" CssClass="btn btn-primary" 
                                                            onclick="btnGuardar_Click" 
                                                            OnClientClick="document.getElementById('<%= btnValidarFotos.ClientID %>').click(); return false;" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:Button runat="server" ID="btnValidaFotos" Text="Valida Fotos" CssClass="btn btn-primary" OnClick="btnValidarFotos_Click" Visible="false" />
                                            </center>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:UpdatePanel ID="upnMensajes" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblSuccess" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                        Text="Queja guardada" runat="server" Visible = "false" />
                                                    <asp:Label ID="lblDanger" CssClass="alert alert-danger col-sm-12" Font-Bold="True" role="alert"
                                                        Text="Queja no guardada" runat="server" Visible = "false"/>
                                                    <asp:Label ID="lblWarning" CssClass="alert alert-warning col-sm-12" Font-Bold="True" role="alert"
                                                        Text="Registro de queja incompleto, reporte al administrador del sistema" 
                                                        runat="server" Visible = "False"/>
                                                    <asp:Label ID="lblCambio" CssClass="alert alert-info col-sm-12" Font-Bold="True" role="alert"
                                                        Text="El folio de la queja cambio por movimientos en la red" 
                                                        runat="server" Visible = "False"/>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                <ProgressTemplate>
                                                    <div class="alert alert-info">
                                                      <strong>GUARDANDO...</strong>Se esta registrando la informaci&oacute;n
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                    
                                    

                                    <asp:UpdatePanel ID="upnBotones" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <center>
                                                        <asp:Button runat="server" ID="btnSubir" Text="Subir imagenes"
                                                            CssClass="btn btn-primary" onclick="btnSubir_Click"  Enabled="False" />
                                                    </center>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <center>
                                                        <asp:Button runat="server" ID="btnTarima" Text="Seleccionar Tarima"
                                                            CssClass="btn btn-primary" onclick="btnTarima_Click" Enabled="False"/>
                                                    </center>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="upnVerifica" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <asp:Label ID="lblVerifica" runat="server" Text="0" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <iframe id="Iframe1" runat="server" name="Iframe1" width="100%" height="500px" visible="false"></iframe>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

