<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accion.aspx.cs" Inherits="queja.accion" %>

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
                            <asp:UpdatePanel ID="udpGeneral" runat="server" UpdateMode="Conditional">
                               <ContentTemplate>
                                    <div class="panel panel-primary">
                                        <div class="panel-body">
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
                                                            <label for="txtRegistro" class="col-sm-2 control-label text-right">Registado por:</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtRegistro" CssClass="form-control" 
                                                                    Style="text-transform: uppercase" placeholder="REGISTRO" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtCedis" class="col-sm-2 control-label text-right">Cedis</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtCedis" CssClass="form-control" 
                                                                    Style="text-transform: uppercase" placeholder="CEDIS" ReadOnly="True"></asp:TextBox>
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
                                                            <label for="txtTransportista" class="col-sm-2 control-label text-right">Transportista</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtTransportista" CssClass="form-control" 
                                                                    Style="text-transform: uppercase" placeholder="TRANSPORTISTA" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtCaja" class="col-sm-2 control-label text-right">Caja</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtCaja" CssClass="form-control" 
                                                                    Style="text-transform: uppercase" placeholder="CAJA" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtPedido" class="col-sm-2 control-label text-right">Pedido</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtPedido" CssClass="form-control" 
                                                                    Style="text-transform: uppercase" placeholder="PEDIDO" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtTipo" class="col-sm-2 control-label text-right">Tipo</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtTipo" CssClass="form-control" 
                                                                    Style="text-transform: uppercase" placeholder="TIPO" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-sm-8 col-sm-offset-2">
                                                            <asp:GridView ID="gvwQuejas" runat="server" AutoGenerateColumns="false" 
                                                                Width="100%" CssClass="table table-bordered table-hover" 
                                                                EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                EmptyDataRowStyle-HorizontalAlign="Center" >
                                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Insecto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="insecto"/>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtAccCorrectiva" class="col-sm-2 control-label text-right">Acci&oacute;n</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtAccCorrectiva" MaxLength="300" TextMode="MultiLine" runat="server" 
                                                                    CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true" ValidationGroup="group1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <p class="bg-primary text-center"><strong>Descripci&oacute;n de la acci&oacute;n correctiva</strong></p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label for="txtAccion" class="col-sm-2 control-label text-right">Acci&oacute;n</label>
                                                    <div class="col-sm-8">
                                                        <asp:UpdatePanel ID="upnAccion" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtAccion" MaxLength="300" TextMode="MultiLine" runat="server" 
                                                                    CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ValidationGroup="group1"></asp:TextBox>
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
                                                            <asp:UpdatePanel ID="udpGuardar" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" 
                                                                        Text="Guardar" ValidationGroup="group1" onclick="btnGuardar_Click" />
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
                                                                        Text="Accion guardada y correo(s) enviado(s)" Visible="false" />
                                                                    <asp:Label ID="lblWarning" runat="server" 
                                                                        CssClass="alert alert-warning col-sm-12" Font-Bold="True" role="alert" 
                                                                        Text="Accion no guardada, intentelo nuevamente" Visible="False" />
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
                                        </div>
                                    </div>
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
