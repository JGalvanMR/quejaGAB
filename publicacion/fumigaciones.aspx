<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fumigaciones.aspx.cs" Inherits="queja.fumigaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="jqueryui/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui.js" type="text/javascript"></script>
    <script src="jqueryui/datepicker-es.js" type="text/javascript"></script>

    <script type="text/javascript">
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

        $().ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaEmb.ClientID %>").datepicker({
                minDate: -150,
                maxDate: 5
            });
            $("#<%= txtHold.ClientID %>").datepicker({
                minDate: -60,
                maxDate: 5
            });
            $("#<%= txtReleased.ClientID %>").datepicker({
                minDate: -60,
                maxDate: 5
            });
        });

        function refrescar()
        {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaEmb.ClientID %>").datepicker({
                minDate: -60,
                maxDate: 5
            });
            $("#<%= txtHold.ClientID %>").datepicker({
                minDate: -60,
                maxDate: 5
            });
            $("#<%= txtReleased.ClientID %>").datepicker({
                minDate: -60,
                maxDate: 5
            });
            //alert("HOLA DESDE JAVASCRIPT VIA ASP.NET");
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
        .tamano
        {
            resize: none;
        }
        .hiddencol
        {
            display: none;
        }
    </style>
</head>
<body>
    <div class="container col-lg-10 col-lg-offset-1">
        <div class="row">
            <div class="col-md-12 col-md-offset-0">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h1><strong class="">Quejas Fumigaciones</strong></h1></center>
                    </div>
                    <div class="panel-body">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server" >
                            </asp:ScriptManager>
                            <%-- asp:UpdatePanel ID="udpGeneral" runat="server" UpdateMode="Conditional">
                                <ContentTemplate --%>
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
                                                    <asp:Label ID="Label4" runat="server" Text="Queja: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                                    <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>                                      
                                                </div>

                                        
                                                <asp:UpdatePanel ID="udpTipo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="form-group form-inline">
                                                            <asp:Button runat="server" ID="btnRegresar" Text="Regresar" 
                                                                CssClass="btn btn-primary" onclick="btnRegresar_Click" />
                                                        </div>
                                                        <asp:Label ID="lblTipo" runat="server" Text="" CssClass="cols-sm-3 label label-success">-</asp:Label>
                                                        <asp:Label ID="lblTarea" runat="server" Text="" CssClass="cols-sm-3 label label-success">-</asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <br />
                                                <asp:UpdatePanel ID="udpGral1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtFolio" class="col-sm-2 control-label text-right">Folio</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtFolio" CssClass="form-control" 
                                                                        Style="text-transform: uppercase" placeholder="FOLIO" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtFecha" class="col-sm-2 control-label text-right">Fecha</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" 
                                                                        Style="text-transform: uppercase" placeholder="FECHA" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtSemana" class="col-sm-2 control-label text-right">Semana</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtSemana" CssClass="form-control" 
                                                                        Style="text-transform: uppercase" placeholder="SEMANA" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                
                                        
                                                <br />
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtFechaEmb" class="col-sm-2 control-label text-right">Fecha Embarque</label>
                                                        <div class="col-sm-8">
                                                            <asp:UpdatePanel ID="udpFechaEmb" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtFechaEmb" MaxLength="10" runat="server" data-provide="datepicker" 
                                                                        CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <asp:UpdatePanel ID="udpPlacas" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">   
                                                            <div class="form-group">
                                                                <div class="col-sm-12">
                                                                    <center>
                                                                        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
                                                                            CssClass="btn btn-primary" onclick="btnBuscar_Click" />
                                                                    </center>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="ddlTipo" class="col-sm-2 control-label text-right">Tipo</label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" 
                                                                        AutoPostBack="False" >
                                                                            <asp:ListItem Value="">SELECCIONE TIPO...</asp:ListItem>
                                                                            <asp:ListItem Value="NACIONAL">NACIONAL</asp:ListItem>
                                                                            <asp:ListItem Value="EXPORTACION">EXPORTACION</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="ddlTransporte" class="col-sm-2 control-label text-right">Trasporte</label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" ID="ddlTransporte" CssClass="form-control" 
                                                                        AutoPostBack="True" 
                                                                        onselectedindexchanged="ddlTransporte_SelectedIndexChanged" >
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <br />
                                                <%-- asp:UpdatePanel ID="udpgral2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate --%>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtPedido" class="col-sm-2 control-label text-right">Pedido/Factura</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnPed" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtPedido" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="PEDIDO/FACTURA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtMes" class="col-sm-2 control-label text-right">Mes</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnMes" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>   
                                                                            <asp:TextBox runat="server" ID="txtMes" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="MES" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtTransportista" class="col-sm-2 control-label text-right">Transportista</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnTrans" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtTransportista" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="TRANSPORTISTA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtAduana" class="col-sm-2 control-label text-right">Aduana</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnAdu" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtAduana" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="ADUANA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtCaja" class="col-sm-2 control-label text-right">Caja</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnCaja" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtCaja" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="CAJA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtResponsable" class="col-sm-2 control-label text-right">Responsable GAB</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtResponsable" CssClass="form-control" 
                                                                        Style="text-transform: uppercase" placeholder="RESPONSABLE GAB" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="form-group">
                                                            <asp:UpdatePanel ID="upnQuejas" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtEmbarque" CssClass="form-control" placeholder="Embarque" Visible="false" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtTransporte" CssClass="form-control" placeholder="Transporte" Visible="false" ></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtEmbarques" CssClass="form-control" placeholder="Pedidos" Visible="false"></asp:TextBox>
                                                                    <asp:GridView ID="gvwQuejas" runat="server" AutoGenerateColumns="false" 
                                                                        Width="100%" CssClass="table table-bordered table-hover" 
                                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                        EmptyDataRowStyle-HorizontalAlign="Center" onrowcommand="gvwQuejas_RowCommand" >
                                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:ButtonField HeaderText="Clave" DataTextField="prod_clave" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="consulta"/>
                                                                            <asp:BoundField HeaderText="Clave" DataField="prod_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                            <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="prod_nombre"/>
                                                                            <asp:BoundField HeaderText="Pedido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="pdn_folio"/>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <asp:UpdatePanel ID="upnValidaProducto" runat="server" UpdateMode="Conditional">
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <center>
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="upnProductos" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvwProductos" runat="server" AutoGenerateColumns="false"
                                                                                Width="90%" CssClass="table table-bordered table-hover" 
                                                                                EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                                EmptyDataRowStyle-HorizontalAlign="Center" 
                                                                                onrowcommand="gvwProductos_RowCommand">
                                                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="producto"/>
                                                                                    <asp:BoundField HeaderText="Descripcion" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="descripcion"/>
                                                                                    <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="prod"/>
                                                                                    <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="nomb"/>
                                                                                    <asp:BoundField HeaderText="Recibo" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="pedi"/>
                                                                                    <asp:BoundField HeaderText="Rancho" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="rancho"/>
                                                                                    <asp:BoundField HeaderText="Tabla" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="tabla"/>
                                                                                    <asp:ButtonField HeaderText="Quitar" Text="Quitar" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="quitar"/>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </center>
                                            
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="ddlInsecto" class="col-sm-2 control-label text-right">Insecto</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnInsecti" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList runat="server" ID="ddlInsecto" CssClass="form-control" 
                                                                                AutoPostBack="True" ValidationGroup="grupo1" >
                                                                            </asp:DropDownList>
                                                                            <!--asp:RequiredFieldValidator ID="rfv1" run="server" ControlToValidate="ddlInsecto" InitialValue="" ErrorMessage="Seleccione insecto" ForeColor="Red" /-->
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtNota" class="col-sm-2 control-label text-right">Nota</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnNota" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtNota" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="NOTA"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">   
                                                            <div class="form-group">
                                                                <div class="col-sm-12">
                                                                    <center>
                                                                        <asp:UpdatePanel ID="upnAgregaIns" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:Button runat="server" ID="btnAgregaIns" Text="Agregar Insecto" 
                                                                                    CssClass="btn btn-primary" onclick="btnAgregaIns_Click" ValidationGroup="grupo1" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </center>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <center>
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="udpInsectos" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvwInsectos" runat="server" AutoGenerateColumns="false"
                                                                                Width="90%" CssClass="table table-bordered table-hover" 
                                                                                EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                                EmptyDataRowStyle-HorizontalAlign="Center" 
                                                                                onrowcommand="gvwInsectos_RowCommand" >
                                                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Clave" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="ins_folio"/>
                                                                                    <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="ins_nombre"/>
                                                                                    <asp:BoundField HeaderText="Nota" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="ins_nota"/>
                                                                                    <asp:ButtonField HeaderText="Quitar" Text="Quitar" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="quitar"/>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregaIns" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </center>
                                                        </div>
                                                    <%--/ContentTemplate>
                                                </asp:UpdatePanel --%>
                                                <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtHold" class="col-sm-2 control-label text-right">FDA/USDA hold</label>
                                                            <div class="col-sm-8">
                                                                <asp:UpdatePanel ID="udpHold" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtHold" MaxLength="10" runat="server" data-provide="datepicker" 
                                                                            CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtReleased" class="col-sm-2 control-label text-right">FDA/USDA released</label>
                                                            <div class="col-sm-8">
                                                                <asp:UpdatePanel ID="udpReleased" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtReleased" MaxLength="10" runat="server" data-provide="datepicker" 
                                                                            CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br>
                                                <br>
                                                <br></br>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label text-right" for="txtCosto">
                                                        Costo</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control" 
                                                                placeholder="COSTO" Style="text-transform: uppercase" Text="0"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label text-right" for="txtAccCorrectiva">
                                                        Acción</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtAccCorrectiva" runat="server" 
                                                                CssClass="form-control tamano" MaxLength="300" ReadOnly="true" Rows="5" 
                                                                Style="text-transform: uppercase" TextMode="MultiLine" ValidationGroup="group1"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <center>
                                                                <asp:UpdatePanel ID="udpGuardar" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" 
                                                                            onclick="btnGuardar_Click" Text="Guardar" ValidationGroup="group1" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </center>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <center>
                                                            <div class="col-sm-12">
                                                                <asp:UpdatePanel ID="upnMensajes" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Label ID="lblSuccess" runat="server" 
                                                                            CssClass="alert alert-success col-sm-12" Font-Bold="True" role="alert" 
                                                                            Text="Queja guardada y correo(s) enviado(s)" Visible="false" />
                                                                        <asp:Label ID="lblWarning" runat="server" 
                                                                            CssClass="alert alert-warning col-sm-12" Font-Bold="True" role="alert" 
                                                                            Text="Queja no guardada, intentelo nuevamente" Visible="False" />
                                                                        <asp:Label ID="lblParcial" runat="server" 
                                                                            CssClass="alert alert-warning col-sm-12" Font-Bold="True" role="alert" 
                                                                            Text="La queja no se guardo completamente, verificar con sistemas" 
                                                                            Visible="False" />
                                                                        <asp:Timer ID="Timer1" runat="server" Interval="5000">
                                                                        </asp:Timer>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                                                                    <ProgressTemplate>
                                                                        <div class="alert alert-info">
                                                                            <strong>PROCESANDO...</strong>Se esta registrando la información
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </div>
                                                        </center>
                                                    </div>
                                                </div>
                                                <br></br>
                                                </br>
                                                    </br>
                                            </div>         
                                        </div>
                                    </div>
                                <%--/ContentTemplate>
                            </asp:UpdatePanel --%>
                        </form>
                     </div>      
                </div>
            </div>
        </div>
    </div>
</body>
</html>
