<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clientes.aspx.cs" Inherits="queja.clientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script>
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
    </style>
</head>
<body>
    <div class="container">
        <div class="col-md-10 col-md-offset-1">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong>Clientes</strong></h3></center>
                    </div>
                    <div class="panel-body">
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
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-offset-1 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a menú" 
                                                CssClass="btn btn-primary"  />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label for="ddlCliente" class=" col-sm-offset-0 col-sm-2 control-label">Clientes</label>
                                                    <div class="col-sm-9">
                                                        <asp:UpdatePanel ID="uplResp1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control"  AutoPostBack="true"
                                                                    onselectedindexchanged="ddlCliente_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <label for="txtSucursal" class=" col-sm-offset-0 col-sm-2 control-label">Sucursales</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtSucursal" MaxLength="300" runat="server" data-provide="datepicker"
                                                            CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                            ValidationGroup="grupo1" CssClass="btn btn-primary btn-lg btn-block" 
                                                            onclick="btnGuardar_Click"/>
                                                    </div>
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
                                                                    EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" >
                                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Clave" DataField="subcli_folio"/>
                                                                        <asp:BoundField HeaderText="Sucursal" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="subcli_nombre"/>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
