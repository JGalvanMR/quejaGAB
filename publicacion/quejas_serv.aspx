<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quejas_serv.aspx.cs" Inherits="queja.quejas_serv" %>

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
                        <center><h1><strong class="">Quejas Servicio</strong></h1></center>
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
                                    </div>
                                    <div class="form-group form-inline">
                                        <div class="col-sm-8">
                                            <asp:Button runat="server" ID="btnInicio" Text="Inicio" 
                                                CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-2" 
                                                onclick="btnInicio_Click" />
                                            <asp:Button runat="server" ID="btnNuevo" Text="Nuevo" 
                                                CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-4" 
                                                onclick="btnNuevo_Click" />
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
                                                CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-4" 
                                                onclick="btnVolver_Click" Visible="False" />
                                        </div>
                                    </div>
                                    <br /><br /><br />
                                    <div class="form-group form-inline">
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtQueja" CssClass="form-control" Style="text-transform: uppercase" placeholder="QUEJA" onkeydown = "return (event.keyCode!=13)" />
                                            <asp:Button runat="server" ID="btnBuscar" Text="BUSCAR" 
                                                CssClass="btn btn-primary" onclick="btnBuscar_Click" />
                                            <asp:Button runat="server" ID="btnRepServicios" Text="REPORTE TRANSPORTISTAS" 
                                                CssClass="btn btn-primary" OnClick="btnRepServicios_Click" />
                                        </div>
                                        <div class="col-sm-8">
                                            <label for="cmbTipo" class="col-sm-1 control-label hidden-sm hidden-xs">Tipo</label>
                                            <asp:DropDownList ID="cmbTipo" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" onselectedindexchanged="cmbTipo_SelectedIndexChanged">
                                                <asp:ListItem Value="">SELECCIONA OPCION...</asp:ListItem>
                                                <asp:ListItem Value="NACIONAL">NACIONAL</asp:ListItem>
                                                <asp:ListItem Value="EXPORTACION">EXPORTACION</asp:ListItem>
                                                <asp:ListItem Value="TODOS">TODOS</asp:ListItem>
                                            </asp:DropDownList>
                                            <label for="cmbTransportista" class="col-sm-1 control-label hidden-sm hidden-xs">Transportista</label>
                                            <asp:DropDownList ID="cmbTransportista" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" 
                                                onselectedindexchanged="cmbTransportista_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <label for="cmbQueja" class="col-sm-1 control-label hidden-sm hidden-xs">Queja</label>
                                            <asp:DropDownList ID="cmbQueja" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" 
                                                AutoPostBack="true" onselectedindexchanged="cmbQueja_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="form-group form-inline">
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" placeholder="Fecha"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtFechaFin" CssClass="form-control" placeholder="Fecha Fin"></asp:TextBox>
                                            <asp:Button runat="server" ID="btnExcelFecha" Text="Exportar" CssClass="btn btn-primary" OnClick="btnExcelFecha_Click" />
                                        </div>
                                        <div class="col-sm-8">
                                            <label for="btnExportar" class="col-sm-1 control-label hidden-sm hidden-xs"></label>
                                            <asp:Button runat="server" ID="btnExportar" Text="Exportar Excel" 
                                                CssClass="btn btn-primary col-xs-12 col-lg-4 col-sm-4" Width="200" onclick="btnExportar_Click" />

                                            <label for="ddlArea" class="col-sm-1 control-label hidden-sm hidden-xs">Area</label>
                                            <asp:DropDownList ID="ddlArea" runat="server" width="200px"
                                                CssClass="form-control btn btn-primary col-xs-12 col-lg-4 col-sm-4" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                <asp:ListItem Text="SELECCIONE OPCION..." Value=""></asp:ListItem>
                                                <asp:ListItem Text="TODOS" Value="TODOS"></asp:ListItem>
                                                <asp:ListItem Text="EMBARQUES" Value="EMBARQUES"></asp:ListItem>
                                                <asp:ListItem Text="TRANSPORTISTA" Value="TRANSPORTISTA"></asp:ListItem>
                                                <asp:ListItem Text="CLIENTE" Value="CLIENTE"></asp:ListItem>
                                                <asp:ListItem Text="ADUANA" Value="ADUANA"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        
                                    </div>
                                    <div class="form-group form-inline">
                                        <div class="col-sm-8">
                                            
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="upnQuejas" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvwQuejas" runat="server" AutoGenerateColumns="False" 
                                                    Width="100%" CssClass="table table-bordered table-hover" 
                                                    EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="True" EmptyDataRowStyle-HorizontalAlign="Center"
                                                    AllowPaging="True" PageSize="30" onrowcommand="gvwQuejas_RowCommand" 
                                                    onpageindexchanging="gvwQuejas_PageIndexChanging">
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <Columns>
                                                        <asp:ButtonField HeaderText="Folio" DataTextField="que_folio" 
                                                            ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" 
                                                            CommandName="consulta" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemStyle CssClass="form-control btn btn-primary" ForeColor="White" />
                                                        <HeaderStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs" Width="30" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField HeaderText="Folio" DataField="que_folio" 
                                                            HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemStyle CssClass="hiddencol" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Fecha" 
                                                            HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" 
                                                            ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" 
                                                            DataField="que_fecha">
                                                        <HeaderStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs" Width="30"/>
                                                        <ItemStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Pedido" 
                                                            HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                            ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" 
                                                            DataField="que_pedido">
                                                        <HeaderStyle CssClass="visible-lg visible-md visible-sm visible-xs" Width="100" />
                                                        <ItemStyle CssClass="visible-xs visible-md visible-lg visible-sm visible-xs" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Transportista" 
                                                            HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" 
                                                            ItemStyle-CssClass="visible-lg  hidden-md hidden-sm hidden-xs" 
                                                            DataField="que_transportista">
                                                        <HeaderStyle CssClass="visible-lg hidden-md hidden-sm hidden-xs" Width="100"/>
                                                        <ItemStyle CssClass="visible-lg  hidden-md hidden-sm hidden-xs" Width="100"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Destino" 
                                                            HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            DataField="que_destino">
                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="100"/>
                                                        <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="100"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Caja" 
                                                            HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            DataField="que_caja">
                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="20"/>
                                                        <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="20"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Perdida MN" 
                                                            HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            DataField="que_perdida_mn" ItemStyle-HorizontalAlign="Right">
                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Perdida USD" 
                                                            HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            DataField="que_perdida_usd" ItemStyle-HorizontalAlign="Right">
                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Queja" 
                                                            HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            DataField="que_nombre" ItemStyle-Width="50">
                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                        <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="200"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tipo" 
                                                            HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            DataField="que_tipo" ItemStyle-Width="50">
                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Area" 
                                                            HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            ItemStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md" 
                                                            DataField="embtracli" ItemStyle-Width="50">
                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        <ItemStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="50"/>
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerSettings 
                                                        Position="Bottom" 
                                                        Mode="NextPreviousFirstLast" 
                                                        FirstPageText="Primero" 
                                                        LastPageText="Ultimo" 
                                                        NextPageText="Siguiente" 
                                                        PreviousPageText="Anterior"
                                                        PageButtonCount="4"/>
                                                        <PagerStyle CssClass="pagination-ys" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
</html>

