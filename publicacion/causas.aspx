<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="causas.aspx.cs" Inherits="queja.causas" %>

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

    <script type="text/javascript">
        $(function () {
            $("#dialog-form").dialog({
                
            });
        });

        function openChild() {
            childWindow = open('tarima.aspx', 'pagename', 'resizable=no,width=1000,height=600');
        }

        function setValue(myVal) {
            //alert(myVal);
            document.getElementById('<%= txtTarima.ClientID %>').value = myVal;

        }

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
    .ui-dialog-titlebar-close{
            display: none;
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
    <div class="container col-lg-12">
        <div class="col-md-10 col-md-offset-1">
            <form id="frmDatos" runat="server" class="form-horizontal">
                <asp:Textbox ID="txtTarima" runat="server" 
                    ontextchanged="txtTarima_TextChanged">0</asp:Textbox>
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Causas</strong></h3></center>
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
                                        <asp:Label ID="lblFecha_Carga_Causa" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-offset-1 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a menú" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"/>
                                            <asp:Button runat="server" ID="btnTarima" Text="Registrar Tarima" 
                                                CssClass="btn btn-primary" onclick="btnTarima_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">
                                                    Datos producto
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapse2" class="panel-collapse"> <%-- collapse --%>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <asp:UpdatePanel ID="uplDetalle" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grvDetalle" runat="server" AutoGenerateColumns="false" 
                                                                Width="100%" CssClass="table table-bordered table-hover" 
                                                                EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                EmptyDataRowStyle-HorizontalAlign="Center" 
                                                                onrowcommand="grvDetalle_RowCommand" >
                                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="ProdCve" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="id_producto"/>
                                                                    <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="nom_producto"/>
                                                                    <asp:BoundField HeaderText="Ord_Prod/PT" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_ordprod"/>
                                                                    <asp:BoundField HeaderText="Tipo" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_ptcptp"/>
                                                                    <asp:ButtonField HeaderText="Mat Pma"  Text="Seleccionar..."
                                                                        HeaderStyle-CssClass="visible-lg hidden-sm hidden-xs hidden-md"
                                                                        ItemStyle-CssClass="form-control btn btn-primary" 
                                                                        ItemStyle-ForeColor="White" CommandName="matprima">
                                                                        <ItemStyle CssClass="form-control btn btn-primary  visible-lg hidden-sm hidden-xs hidden-md" ForeColor="White" Width="120" />
                                                                        <HeaderStyle CssClass="visible-lg hidden-sm hidden-xs hidden-md" Width="120" />
                                                                    </asp:ButtonField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>            
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtProducto" MaxLength="300" runat="server" 
                                                CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true" Visible="false"></asp:TextBox>
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
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="upnComentarios" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwResponsables" runat="server" AutoGenerateColumns="false" 
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowcommand="gvwResponsables_RowCommand" >
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Folio" DataField="inv_folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="No." DataField="inv_responsable"/>
                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="emp_nombre"/>
                                                            <asp:BoundField HeaderText="Comentario" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="inv_comentario"/>
                                                            <asp:BoundField HeaderText="Entrega" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="inv_fechaentrega"/>
                                                            <asp:BoundField HeaderText="Registro" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="inv_fecharegistro"/>
                                                            <asp:ButtonField HeaderText="Causas" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Causas" CommandName="muestradatos"/> 
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </center>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <asp:UpdatePanel ID="upnClaveInv" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblClaveInve" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="lblMensaje" runat="server" Text="Seleccione al responsable del registro de causas" CssClass="cols-sm-3 label label-danger"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="upnDatos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="txtComentario" class="col-sm-2 control-label">Comentario</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtComentario" MaxLength="300" TextMode="MultiLine" runat="server" 
                                                        CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="txtFecha" class="col-sm-2 control-label">Fecha registro</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtFecha" MaxLength="300" runat="server" 
                                                        CssClass="form-control" Style="text-transform: uppercase" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="txtFechaEntrega" class="col-sm-2 control-label">Fecha entrega</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtFechaEntrega" MaxLength="300" runat="server" 
                                                        CssClass="form-control" Style="text-transform: uppercase" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="upnControles" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="txtCausa" class="col-sm-2 control-label">Causa</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCausa" MaxLength="150" TextMode="MultiLine" runat="server" 
                                                        CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCausa" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCausa" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="valInput" ControlToValidate="txtCausa" ValidationExpression="^[\s\S]{0,150}$" ErrorMessage="Máximo 150 caracteres" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo2"></asp:RegularExpressionValidator>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="ddlResp1" class=" col-sm-offset-0 col-sm-2 control-label">Resp. Acción</label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList runat="server" ID="ddlResp1" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRecibio" runat="server" ErrorMessage="Debe elegir una opción" ControlToValidate="ddlResp1" ForeColor="Red" InitialValue="0" ValidationGroup="grupo2">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="row">
                                            <div class="form-group">
                                                <label for="txtCausa2" class="col-sm-2 control-label">Causa 2</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCausa2" MaxLength="150" TextMode="MultiLine" runat="server"
                                                        CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RegularExpressionValidator runat="server" ID="revCausa2" ControlToValidate="txtCausa2" ValidationExpression="^[\s\S]{0,150}$" ErrorMessage="Máximo 150 caracteres" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo2"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="ddlResp2" class="col-sm-offset-0 col-sm-2 control-label">Resp. Acción 2</label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList runat="server" ID="ddlResp2" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="txtCausa3" class="col-sm-2 control-label">Causa 3</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCausa3" MaxLength="150" TextMode="MultiLine" runat="server"
                                                        CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RegularExpressionValidator runat="server" ID="revCausa3" ControlToValidate="txtCausa2" ValidationExpression="^[\s\S]{0,150}$" ErrorMessage="Máximo 150 caracteres" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo2"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="ddlResp3" class="col-sm-offset-0 col-sm-2 control-label">Resp. Acción 3</label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList runat="server" ID="ddlResp3" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:UpdatePanel ID="upnAdd" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnAdd" Text="Añadir Causa" 
                                                        CssClass="btn btn-primary" ValidationGroup="grupo2"
                                                        onclick="btnAdd_Click"  />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="uplAgregar" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwAgregar" runat="server" AutoGenerateColumns="false" 
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center"
                                                        onrowdatabound="gvwAgregar_RowDataBound" onrowdeleting="gvwAgregar_RowDeleting">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="No. Empl" DataField="clv"/>
                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="nombre"/>
                                                            <asp:BoundField HeaderText="Causa" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="causa"/>
                                                            <asp:CommandField ShowDeleteButton="true" HeaderText="Borrar" ButtonType="Button" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ControlStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White"/>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </center>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <asp:UpdatePanel ID="upnValidaTarima" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar Causas" CssClass="btn btn-primary" 
                                                            onclick="btnGuardar_Click" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel ID="upnMensajes" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblSuccess" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Investigacion guardada y correo(s) enviado(s)" runat="server" Visible = "false" />
                                                        <asp:Label ID="lblWarning" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Investigación no guardada, intentelo nuevamente" 
                                                            runat="server" Visible = "False"/>
                                                        <asp:Label ID="lblSuccessInv" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Responsable de acción eliminado" runat="server" Visible = "false" />
                                                        <asp:Label ID="lblWarningInv" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Responsable de acción no eliminado, intentelo nuevamente" 
                                                            runat="server" Visible = "False"/>
                                                        <asp:Label ID="lblSuccessMod" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Investigacion modificada" runat="server" Visible = "false" />
                                                        <asp:Label ID="lblWarningMod" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Investigación no modificada, intentelo nuevamente" 
                                                            runat="server" Visible = "False"/>
                                                        
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
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
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <asp:UpdatePanel ID="upnClaveAccion" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblClaveAccion" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="lblOperacion" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="upnResponsables" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwResponsable" runat="server" AutoGenerateColumns="false" 
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowdatabound="gvwResponsable_RowDataBound" 
                                                        onrowdeleting="gvwResponsable_RowDeleting" 
                                                        onrowcommand="gvwResponsable_RowCommand" >
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Folio" DataField="acc_folio"  HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="Cve. Resp." DataField="acc_responsable"/>
                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="resp_nombre"/>
                                                            <asp:BoundField HeaderText="Causa" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_causa"/>
                                                            <asp:ButtonField HeaderText="Editar" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Editar" CommandName="verdatos"/> 
                                                            <asp:CommandField ShowDeleteButton="true" HeaderText="Borrar" ButtonType="Button" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ControlStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White"/>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="idmodal" tabindex="-1" role="dialog">
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">Detalle de Materias Primas</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel ID="upnMatPrima" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grvMatPrima" runat="server" AutoGenerateColumns="false" 
                                                            Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                            EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" >
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sel" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkMP" runat="server" AutoPostBack="true" OnCheckedChanged="chkMP_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Clave" DataField="compp_clave"/>
                                                                <asp:BoundField HeaderText="Nombre" DataField="prod_nombre"/>
                                                                <asp:BoundField HeaderText="Ord Prod" DataField="ord_prod"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCierra" runat="server" Text="Cerrar Ventana" class="btn btn-default" data-dismiss="modal"/>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
