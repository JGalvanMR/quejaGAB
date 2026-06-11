<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quejas.aspx.cs" Inherits="queja.quejas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    
   
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

        function ShowPopup() {
            //$("#btnShowPopup").click();
            $("#myModal").modal({ show: true });
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
     .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td {
            display: inline;
        }

        .pagination-ys table > tbody > tr > td > a,
        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #dd4814;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }

        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;    
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }

        .pagination-ys table > tbody > tr > td:first-child > a,
        .pagination-ys table > tbody > tr > td:first-child > span {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td:last-child > a,
        .pagination-ys table > tbody > tr > td:last-child > span {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td > a:hover,
        .pagination-ys table > tbody > tr > td > span:hover,
        .pagination-ys table > tbody > tr > td > a:focus,
        .pagination-ys table > tbody > tr > td > span:focus {
            color: #97310e;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
        .hiddencol
        {
            display: none;
        }
        
        
        
    </style>
</head>
<body>
    <div class="container col-lg-12">
        <div class="row">
            <div class="col-md-12 col-md-offset-0">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h1><strong class="">Quejas Clientes</strong></h1></center>
                    </div>
                    <div class="panel-body">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                        <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                        <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                        <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="lblRol" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    </div>
                                    <div class="form-group form-inline">
                                        <div class="col-sm-8">
                                            <asp:Button runat="server" ID="btnInicio" Text="Inicio" 
                                                CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-2" 
                                                onclick="btnInicio_Click"  />
                                            <asp:UpdatePanel ID="upnRegistro" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlQuejas" runat="server" 
                                                        CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                        AutoPostBack="true" onselectedindexchanged="ddlQuejas_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Elegir tipo de queja...</asp:ListItem>
                                                        <asp:ListItem Value="1">Producto</asp:ListItem>
                                                        <asp:ListItem Value="2">Pedido</asp:ListItem>
                                                        <asp:ListItem Value="3">Servicio</asp:ListItem>
                                                        <asp:ListItem Value="4">Fumigacion</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:Button runat="server" ID="btnReportes" Text="Reportes" 
                                                CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-4" 
                                                onclick="btnReportes_Click"  />
                                            <asp:Button runat="server" ID="btnAyuda" Text="Ayuda" 
                                                CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-4" 
                                                onclick="btnAyuda_Click"   />
                                        </div>
                                    </div>
                                    <br /><br /><br />
                                    <div class="form-group form-inline">
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtQueja" CssClass="form-control" Style="text-transform: uppercase" placeholder="QUEJA" onkeydown = "return (event.keyCode!=13)" />
                                            <asp:Button runat="server" ID="btnBuscar" Text="BUSCAR" 
                                                CssClass="btn btn-primary" onclick="btnBuscar_Click" />
                                        </div>
                                        <div class="col-sm-8">
                                            <label for="cmbProducto" class="col-sm-1 control-label hidden-sm hidden-xs">Producto</label>
                                            <asp:DropDownList ID="cmbProducto" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" 
                                                onselectedindexchanged="cmbProducto_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <label for="cmbProblema" class="col-sm-1 control-label hidden-sm hidden-xs">Problema</label>
                                            <asp:DropDownList ID="cmbProblema" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" 
                                                onselectedindexchanged="cmbProblema_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <label for="cmbArea" class="col-sm-1 control-label hidden-sm hidden-xs">Area</label>
                                            <asp:DropDownList ID="cmbArea" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" onselectedindexchanged="cmbArea_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="form-group form-inline">
                                        <div class="col-sm-8 col-sm-offset-4">
                                            <label for="cmbVariedad" class="col-sm-1 control-label hidden-sm hidden-xs">Variedad</label>
                                            <asp:DropDownList ID="cmbVariedad" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" 
                                                onselectedindexchanged="cmbVariedad_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <label for="cmbPedido" class="col-sm-1 control-label hidden-sm hidden-xs">Pedido</label>
                                            <asp:DropDownList ID="cmbPedido" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" 
                                                onselectedindexchanged="cmbPedido_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <label for="cmbPedido" class="col-sm-1 control-label hidden-sm hidden-xs"></label>
                                            <asp:Button runat="server" ID="btnExportar" Text="Exportar Excel" 
                                                CssClass="btn btn-primary" Width="200" onclick="btnExportar_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group form-inline">
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblDevolucion" runat="server" Text="Devolución" CssClass="form-control"></asp:Label>
                                            <asp:Label ID="lblMerma" runat="server" Text="Merma" CssClass="form-control"></asp:Label>
                                            <asp:Label ID="lblBonificacion" runat="server" Text="Bonificación" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row" style="overflow-x:auto;width:auto">
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="upnQuejas" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwQuejas" runat="server" AutoGenerateColumns="False" 
                                                        Width="100%" CssClass="table table-bordered table-hover table-responsive" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="True" EmptyDataRowStyle-HorizontalAlign="Center"
                                                        AllowPaging="True" PageSize="30"  
                                                        onpageindexchanging="gvwQuejas_PageIndexChanging" 
                                                        onrowcommand="gvwQuejas_RowCommand" 
                                                        onrowdatabound="gvwQuejas_RowDataBound"  >
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:ButtonField HeaderText="Folio" DataTextField="que_folio" 
                                                                ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" 
                                                                CommandName="consulta" >
                                                            <ItemStyle CssClass="form-control btn btn-primary FrozenCell" ForeColor="White" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField HeaderText="Folio" DataField="que_folio" 
                                                                HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                            <HeaderStyle CssClass="hiddencol" />
                                                            <ItemStyle CssClass="hiddencol" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Sem" 
                                                                HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" 
                                                                ItemStyle-CssClass="visible-lg hidden-md hidden-xs" DataField="que_semana">
                                                            <HeaderStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs" />
                                                            <ItemStyle CssClass="visible-lg hidden-md hidden-xs" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Fecha" 
                                                                HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" 
                                                                ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" 
                                                                DataField="que_fecha">
                                                            <HeaderStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs" />
                                                            <ItemStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Cliente" 
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                DataField="que_cliente">
                                                            <HeaderStyle CssClass="visible-lg visible-md visible-sm visible-xs" Width="200" />
                                                            <ItemStyle CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" Width="200"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Sucursal" 
                                                                HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" 
                                                                ItemStyle-CssClass="visible-lg  hidden-md hidden-sm hidden-xs" 
                                                                DataField="que_sucursal">
                                                            <HeaderStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs" Width="200"/>
                                                            <ItemStyle CssClass="visible-lg  hidden-md hidden-sm hidden-xs" Width="200"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Recibió" 
                                                                HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                DataField="que_recibio">
                                                            <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                            <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Producto" 
                                                                HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                DataField="nom_producto">
                                                            <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                            <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                            </asp:BoundField>
                                                            <asp:ButtonField HeaderText="Ver" DataTextField="ver" 
                                                                HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                ItemStyle-CssClass="form-control btn btn-primary visible-lg hidden-sm hidden-xs hidden-md"
                                                                ItemStyle-ForeColor="White" 
                                                                CommandName="productos">
                                                                <ItemStyle CssClass="form-control btn btn-primary visible-lg hidden-sm hidden-xs hidden-md" ForeColor="White" Width="30" />
                                                                <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="30" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField HeaderText="Problema" 
                                                                HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                DataField="pro_nombre">
                                                            <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                            <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Area" 
                                                                HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                DataField="area_nombre" ItemStyle-Width="50">
                                                            <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" />
                                                            <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" />
                                                            </asp:BoundField>
                                                            <asp:ButtonField HeaderText="Variedad" DataTextField="qud_variedad" 
                                                                HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md"
                                                                ItemStyle-CssClass="form-control btn btn-primary" 
                                                                ItemStyle-ForeColor="White" CommandName="variedades">
                                                                <ItemStyle CssClass="form-control btn btn-primary  visible-lg hidden-sm hidden-xs hidden-md" ForeColor="White" Width="120" />
                                                                <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="120" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField HeaderText="Pedido" 
                                                                HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                                DataField="que_pedido" ItemStyle-Width="50">
                                                            <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" />
                                                            <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="1" 
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ItemStyle-ForeColor="White" DataField="que_etapa1">
                                                            <HeaderStyle CssClass="visible-lg visible-md visible-sm visible-xs" />
                                                            <ItemStyle CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="2" 
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ItemStyle-ForeColor="White" DataField="que_etapa2">
                                                            <HeaderStyle CssClass="visible-lg visible-md visible-sm visible-xs" />
                                                            <ItemStyle CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="3" 
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ItemStyle-ForeColor="White" DataField="que_etapa3">
                                                            <HeaderStyle CssClass="visible-lg visible-md visible-sm visible-xs" />
                                                            <ItemStyle CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="4" 
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ItemStyle-ForeColor="White" DataField="que_etapa4">
                                                            <HeaderStyle CssClass="visible-lg visible-md visible-sm visible-xs" />
                                                            <ItemStyle CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="5" 
                                                                HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ItemStyle-ForeColor="White" DataField="que_etapa5">
                                                            <HeaderStyle CssClass="visible-lg visible-md visible-sm visible-xs" />
                                                            <ItemStyle CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                                ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="hiddencol" 
                                                                ItemStyle-CssClass="hiddencol" DataField="nom_producto">
                                                            <HeaderStyle CssClass="hiddencol" />
                                                            <ItemStyle CssClass="hiddencol" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Variedades" HeaderStyle-CssClass="hiddencol" 
                                                                ItemStyle-CssClass="hiddencol" DataField="qud_variedad">
                                                            <HeaderStyle CssClass="hiddencol" />
                                                            <ItemStyle CssClass="hiddencol" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Causas" HeaderStyle-CssClass="hiddencol" 
                                                                ItemStyle-CssClass="hiddencol" DataField="causas">
                                                            <HeaderStyle CssClass="hiddencol" />
                                                            <ItemStyle CssClass="hiddencol" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <PagerSettings 
                                                            Position="Bottom" 
                                                            Mode="NextPreviousFirstLast" 
                                                            FirstPageText="Primero" 
                                                            LastPageText="Ultimo" 
                                                            NextPageText="Siguiente" 
                                                            PreviousPageText="Anterior"
                                                            PageButtonCount="4"
                                                             />
                                                            <PagerStyle CssClass="pagination-ys" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="modal fade" tabindex="-1" role="dialog" id="myModal">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Detalle de Productos o Variedades</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <asp:UpdatePanel ID="upnProductos" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwProductos" runat="server" AutoGenerateColumns="false"
                                                        Width="100%" CssClass="table table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center"
                                                        AllowPaging="true" PageSize="30">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Clave" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="id_producto"/>
                                                            <asp:BoundField HeaderText="Producto/Variedad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="nom_producto"/>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                                data-toggle="modal" data-target="#myModal">
                                Launch demo modal
                            </button>
                        </form>                    
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</body>
</html>
