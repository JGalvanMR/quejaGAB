<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tarimas.aspx.cs" Inherits="queja.tarimas" %>

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
        function updateParent() {
            var oVal = document.getElementById('<%= txtTarima.ClientID %>').value; //"0";
            window.opener.setValue(oVal);
            window.close();
            return false;
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
            background-color: #464646:
        }
    </style>
</head>
<body>
    <%--<form id="form1" unat="server">
    <div>
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    </div>
    </form>--%>
    <div class="container col-lg-12">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong>Selección de Tarima</strong></h3></center>
                    </div>
                    <div class="panel-body">
                        <form id="Form1" runat="server" class="form-horizontal" enctype="multipart/form-data">
                            <asp:ScriptManager ID="scriptmanager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                            <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                            <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                            <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text="Queja: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                            <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="lblFecha_Carga_Causa" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-offset-0 col-sm-12">
                                                <asp:UpdatePanel ID="upnOrden" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvwOrden" runat="server" AutoGenerateColumns="false" 
                                                            Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                                            EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                            onrowcommand="gvwOrden_RowCommand">
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                            <Columns>
                                                                <asp:ButtonField HeaderText="Clave" DataTextField="prod_clave" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="muestradatos" />
                                                                <asp:BoundField HeaderText="Clave_2" DataField="prod_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="prod_nombre"/>
                                                                <asp:BoundField HeaderText="Fecha" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="pti_fecha"/>
                                                                <asp:BoundField HeaderText="Cve Prov" DataField="prov_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                <asp:BoundField HeaderText="Proveedor" DataField="prov_nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"/>
                                                                <asp:BoundField HeaderText="Cve Rch" DataField="rch_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                <asp:BoundField HeaderText="Rancho" DataField="rch_nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"/>
                                                                <asp:BoundField HeaderText="Cve Tbl" DataField="tbl_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                <asp:BoundField HeaderText="Tabla" DataField="tbl_nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"/>
                                                                <asp:BoundField HeaderText="Fecha Cad" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="fecha_cad"/>
                                                                <asp:BoundField HeaderText="Tipo" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="tipo"/>
                                                                <asp:BoundField HeaderText="Tarima" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="tarima" ItemStyle-HorizontalAlign="Center"/>
                                                                <asp:BoundField HeaderText="Lote" DataField="lote" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    
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
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <center>
                                                    <label for="txtTarima" class="control-label">Tarima:</label>
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <center>
                                                <div class="col-sm-4 col-sm-offset-4">
                                                    <asp:UpdatePanel ID="upnTarima" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Textbox ID="txtTarima" runat="server" CssClass="form-control" ReadOnly="true"></asp:Textbox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </center>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <center>
                                                    <asp:Button runat="server" ID="btnBuscaOrden" Text="Guardar" 
                                                        CssClass="btn btn-primary" onclick="btnBuscaOrden_Click" />
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="upnDatosActualizar" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Textbox ID="txtProveedor" runat="server" ReadOnly="true"></asp:Textbox>
                                    <asp:Textbox ID="txtRancho" runat="server" ReadOnly="true"></asp:Textbox>
                                    <asp:Textbox ID="txtTabla" runat="server" ReadOnly="true"></asp:Textbox>
                                    <asp:Textbox ID="txtLote" runat="server" ReadOnly="true"></asp:Textbox>
                                    <asp:Textbox ID="txtCaducidad" runat="server" ReadOnly="true"></asp:Textbox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

