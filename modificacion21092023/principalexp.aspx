<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="principalexp.aspx.cs" Inherits="queja.principalexp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="jqueryui/jquery-ui.css" rel="stylesheet" type="text/css" />
    
    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>

    <script src="jqueryui/jquery-ui.js" type="text/javascript"></script>
    <script src="jqueryui/datepicker-es.js" type="text/javascript"></script>
    <script>
        $().ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaCarga.ClientID %>").datepicker({
                minDate: -60,
                maxDate: 20
            });
            
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
            <div class="col-md-12 col-md-offset-0">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong>Descripción Queja</strong></h3></center>
                    </div>
                    <div class="panel-body">
                        <form id="form1" runat="server" class="form-horizontal" enctype="multipart/form-data">
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
                                    <div class="form-group">
                                        <label for="txtSemana" class="col-sm-3 control-label">Semana</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtSemana" CssClass="form-control" 
                                                Style="text-transform: uppercase" placeholder="Semana" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtMes" class="col-sm-3 control-label">Mes</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtMes" CssClass="form-control" 
                                                Style="text-transform: uppercase" placeholder="Mes" ReadOnly="True"></asp:TextBox>
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
                                            <asp:TextBox runat="server" ID="txtReporto" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Reportó" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReporto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtReporto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlTipo" class="col-sm-3 control-label">Tipo</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" 
                                                AutoPostBack="True" onselectedindexchanged="ddlTipo_SelectedIndexChanged">
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
                                        <label for="txtFecha" class="col-sm-3 control-label">Fecha</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtFechaCarga" CssClass="form-control" Style="text-transform: uppercase" placeholder="Fecha" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    
                                            <asp:UpdatePanel ID="udpPlacas" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <center>
                                                                <asp:Button runat="server" ID="btnBuscaOrden" Text="Buscar" 
                                                                    CssClass="btn btn-primary" onclick="btnBuscaOrden_Click" />
                                                            </center>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="ddlPlacas" class="col-sm-3 control-label">Placa</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlPlacas" runat="server" CssClass="form-control" 
                                                                onselectedindexchanged="ddlPlacas_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscaOrden" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:TextBox runat="server" ID="txtPlaca" Visible="false" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Placa" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                        
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <center>
                                                <asp:Button runat="server" ID="btnTodos" Text="Seleccionar Todos" 
                                                    CssClass="btn btn-primary" onclick="btnTodos_Click" />
                                            </center>
                                        </div>
                                    </div>
                                    <br />
                                      
                                    <br />
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="upnQuejas" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtEmbarque" CssClass="form-control" placeholder="Embarque" ></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtTransporte" CssClass="form-control" 
                                                    placeholder="Transporte" ></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtEmbarques" CssClass="form-control" placeholder="Pedidos" ></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFactura" CssClass="form-control" placeholder="Factura" ></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFechaEmb" CssClass="form-control" placeholder="Fecha Embarque"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtSemEmb" CssClass="form-control" placeholder="SemanaEmbarque"></asp:TextBox>
                                                <br /><br />
                                                <asp:GridView ID="gvwQuejas" runat="server" AutoGenerateColumns="false" 
                                                    Width="100%" CssClass="table table-bordered table-hover" 
                                                    EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center"
                                                    onrowcommand="gvwQuejas_RowCommand" >
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <Columns>
                                                        <asp:ButtonField HeaderText="Clave" DataTextField="prod_clave" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="consulta"/>
                                                        <asp:BoundField HeaderText="Clave" DataField="prod_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                        <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="prod_nombre"/>
                                                        <asp:BoundField HeaderText="Cajas" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-HorizontalAlign="Right" DataField="cajas"/>
                                                        <asp:TemplateField HeaderText="Rechazadas" HeaderStyle-CssClass="visible-lg visible-md visible-sm hiddvisibleen-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" HeaderStyle-Width="100" ItemStyle-Width="100">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRechazadas2" runat="server" Width="100" Text="0" style="text-align: right" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Pedido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="pdn_folio"/>
                                                        <asp:BoundField HeaderText="CNTE" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="cnte"/>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="upnValidaProducto" runat="server" UpdateMode="Conditional">
                                        </asp:UpdatePanel>
                                    </div>
                                    
                                    <br />  
                                    <div class="row col-sm-12" >
                                        <div class="form-group" >
                                            <asp:UpdatePanel ID="uplDetalle" runat="server" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:GridView ID="grvDetalle" runat="server" AutoGenerateColumns="false" 
                                                        Width="100%" CssClass="table table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        EmptyDataRowStyle-HorizontalAlign="Center" 
                                                        onrowcommand="grvDetalle_RowCommand"
                                                          >
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Pedido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="pedido"/>
                                                            <asp:BoundField HeaderText="ProdCve" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="id_producto"/>
                                                            <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="nom_producto"/>
                                                            <asp:BoundField HeaderText="Ord Prod" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_ordprod"/>
                                                            <asp:BoundField HeaderText="Tarima" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_tarima"/>
                                                            <asp:BoundField HeaderText="Lote" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_lote"/>
                                                            <asp:BoundField HeaderText="Fecha Cad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="fecha_cad"/>
                                                            <asp:BoundField HeaderText="CveProv" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="prov_clave"/>
                                                            <asp:BoundField HeaderText="Proveedor" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="prov_nombre"/>
                                                            <asp:BoundField HeaderText="CveRch" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="rch_clave"/>
                                                            <asp:BoundField HeaderText="Rancho" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="rch_nombre"/>
                                                            <asp:BoundField HeaderText="CveTbl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="tbl_clave"/>
                                                            <asp:BoundField HeaderText="Tabla" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="tbl_nombre"/>
                                                            <asp:BoundField HeaderText="Responsable" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_responsable"/>
                                                            <asp:BoundField HeaderText="Area" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_area"/>
                                                            <asp:BoundField HeaderText="Recibido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantreci"/>
                                                            <asp:TemplateField HeaderText="Rechazadas" HeaderStyle-CssClass="visible-lg visible-md visible-sm hiddvisibleen-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" HeaderStyle-Width="100" ItemStyle-Width="100" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRechazadas" runat="server" Width="100" Text='<%# Bind("qud_cantrecha") %>' AutoPostBack="true" style="text-align: right" OnTextChanged="txtRechazadas_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Producidas" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="producidas"/>
                                                            <asp:BoundField HeaderText="Porcentaje" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="porcentaje"/>
                                                            <asp:BoundField HeaderText="Unidad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_unidad"/>
                                                            <asp:BoundField HeaderText="Tipo" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="tipo"/>
                                                            <asp:BoundField HeaderText="Variedad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="vari"/>
                                                            <asp:BoundField HeaderText="Pedido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="pedido"/>
                                                            <asp:ButtonField HeaderText="Borrar" Text="Borrar" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="borrar"/>
                                                            <asp:BoundField HeaderText="CNTE" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="cnte"/>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>  
                                    <div class="form-group nover">
                                        <label for="chkDevolucion" class="col-sm-3 control-label">¿Devolución?</label>
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
                                    <br />
                                    <div class="form-group">
                                        <label for="ddlMoneda" class="col-sm-3 control-label">Moneda</label>
                                        <div class="col-sm-8">
                                            <asp:UpdatePanel ID="uplMoneda" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlMoneda" CssClass="form-control">
                                                        <asp:ListItem Value="PESOS">PESOS</asp:ListItem>
                                                        <asp:ListItem Value="DOLARES">DOLARES</asp:ListItem>
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
                                        <asp:UpdatePanel ID="uplCosto" runat="server" UpdateMode="Conditional" >
                                            <ContentTemplate>
                                                <!--label for="txtCosto" class="col-sm-3 control-label" >Costo</label-->
                                                <asp:Label AssociatedControlID="txtCosto" CssClass="col-sm-3 control-label" ID="lblCosto" runat="server" Text="Costo"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" AutoPostBack="false" ID="txtCosto" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Costo" onkeydown = "return (event.keyCode!=13)" Text="0"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCosto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revCosto" runat="server" ErrorMessage="Error de ingreso de número" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1" ValidationExpression="\d{1,10}(.\d{1,2})?"></asp:RegularExpressionValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>
                                    <br />
                                    <div class="row col-sm-12" >
                                        <div class="form-group" >
                                            <center>
                                                <asp:UpdatePanel ID="uplFacturas" runat="server" UpdateMode="Conditional" >
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grvFacturas" runat="server" AutoGenerateColumns="false" 
                                                            Width="10%" CssClass="table table-bordered table-hover" 
                                                            EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                            EmptyDataRowStyle-HorizontalAlign="Center">
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Factura" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="fact"/>
                                                                <asp:TemplateField HeaderText="Costo" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" HeaderStyle-Width="100" ItemStyle-Width="100" >
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCostoFact" runat="server" Width="100" Text="0" style="text-align: right"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </center>
                                        </div>
                                    </div> 

                                    <div class="form-group">
                                    <asp:UpdatePanel ID="uplSufijo" runat="server" UpdateMode="Conditional" >
                                        <ContentTemplate>
                                            <!--label for="txtSufijo" class="col-sm-3 control-label">Sufijo</label-->
                                            <asp:Label AssociatedControlID="txtSufijo" CssClass="col-sm-3 control-label" ID="lblSufijo" runat="server" Text="Sufijo"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" AutoPostBack="false" ID="txtSufijo" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Sufijo" 
                                                        onkeydown = "return (event.keyCode!=13)" MaxLength="5"></asp:TextBox>
                                                </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                        
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
                                        <div class="col-sm-12">
                                            <center>
                                                <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                    ValidationGroup="grupo1" CssClass="btn btn-primary" onclick="btnGuardar_Click" 
                                                      />
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
                                                        <asp:Button runat="server" ID="btnSubir" Text="Subir imagenes" Enabled="false"
                                                            CssClass="btn btn-primary" onclick="btnSubir_Click"  />
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

