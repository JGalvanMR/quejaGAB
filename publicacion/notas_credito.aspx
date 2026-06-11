<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notas_credito.aspx.cs" Inherits="queja.notas_credito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/sweetalert.min.js" type="text/javascript"></script>
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
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Notas de Cr&eacute;dito</strong></h3></center>
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
                                            <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a menú" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <!--a data-toggle="collapse" data-parent="#accordion" href="#collapse2"-->
                                                        Datos producto
                                                    <!--a-->
                                                </h4>
                                            </div>
                                            <!--div id="collapse2" class="panel-collapse collapse"-->
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel ID="uplDetalle" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grvDetalle" runat="server" AutoGenerateColumns="false" 
                                                                    Width="100%" CssClass="table table-bordered table-hover" 
                                                                    EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" >
                                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="ProdCve" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="id_producto"/>
                                                                        <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="nom_producto"/>
                                                                        <asp:BoundField HeaderText="Ord Prod" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_ordprod"/>
                                                                        <asp:BoundField HeaderText="Recibidas" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantreci"/>
                                                                        <asp:BoundField HeaderText="Rechazadas" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantrecha"/>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>            
                                                <!--/div-->
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="txtProblema" class="col-sm-2 control-label">Problema</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtProblema" MaxLength="300" runat="server" 
                                                    CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="txtProblema" class="col-sm-2 control-label">Tipo queja</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtTipo" MaxLength="300" runat="server" 
                                                    CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="upnCausas" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grvCausas" runat="server" AutoGenerateColumns="false" 
                                                            Width="95%" CssClass="table table-bordered table-hover" 
                                                            EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" >
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Inv Folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="inv_folio"/>
                                                                <asp:BoundField HeaderText="Queja" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="que_folio"/>
                                                                <asp:BoundField HeaderText="Inv Resp" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="inv_responsable"/>
                                                                <asp:BoundField HeaderText="Responsable" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="resp_nombre"/>
                                                                <asp:BoundField HeaderText="Comentario" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="inv_comentario"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="upnPedido" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grvPedido" runat="server" AutoGenerateColumns="false" 
                                                            Width="95%" CssClass="table table-bordered table-hover" 
                                                            EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" >
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Pedido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_pedido"/>
                                                                <asp:BoundField HeaderText="Inv Folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="pdn_elaboro"/>
                                                                <asp:BoundField HeaderText="Realizado" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="usu_nombre"/>
                                                                <asp:BoundField HeaderText="Correo" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="usu_email"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="upnFacturas" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grvFacturas" runat="server" AutoGenerateColumns="false" 
                                                            Width="95%" CssClass="table table-bordered table-hover" 
                                                            EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center">
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Facturas" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-HorizontalAlign="Center" DataField="facturas"/>
                                                                <asp:BoundField HeaderText="Subcliente" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" 
                                                                ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-HorizontalAlign="Center" DataField="subcliente"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <div class="form-group">
                                                <div class="col-sm-offset-2 col-sm-8">
                                                <asp:UpdatePanel ID="upnBotones" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnGuardar" Text="Confirmar Nota de Credito" 
                                                            CssClass="btn btn-primary" onclick="btnGuardar_Click"   />
                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:Button runat="server" ID="btnNoProcede" Text="No procede NC" 
                                                            CssClass="btn btn-primary" onclick="btnNoProcede_Click"   />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </center>
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
