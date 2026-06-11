<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accioncorrectiva.aspx.cs" Inherits="queja.accioncorrectiva" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="es" xml:lang="es">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="jqueryui/jquery-ui.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />--%>

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>
    <script src="jqueryui/jquery-ui.js" type="text/javascript"></script>
    <script src="jqueryui/datepicker-es.js" type="text/javascript"></script>
    <%--<script src="Scripts/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap-datepicker.es.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $().ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaEntrega.ClientID %>").datepicker({
                minDate: 0,
                maxDate: 20
            });
            
            /*$("#<%= txtFechaEntrega.ClientID %>").datepicker({
            autoclose: true,
            format: "dd/mm/yyyy",
            language: "es",
            autoclose: true,
            startDate: "0d"
            }).on("changeDate", function () {
            $("#<%= txtFechaEntrega.ClientID %>").datepicker('hide');
            });*/
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
                °°<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Acciones Correctivas</strong></h3></center>
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
                                        <asp:Label ID="lblFecha_Carga_Accion" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-offset-1 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a menú" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"/>
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
                                        <div id="collapse2" class="panel-collapse collapse">
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
                                                                    <asp:BoundField HeaderText="Tarima" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_tarima"/>
                                                                    <asp:BoundField HeaderText="Lote" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_lote"/>
                                                                    <asp:BoundField HeaderText="Fecha Cad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="fecha_cad"/>
                                                                    <asp:BoundField HeaderText="CveProv" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="prov_clave"/>
                                                                    <asp:BoundField HeaderText="Proveedor" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="prov_nombre"/>
                                                                    <asp:BoundField HeaderText="CveRch" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="rch_clave"/>
                                                                    <asp:BoundField HeaderText="Rancho" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="rch_nombre"/>
                                                                    <asp:BoundField HeaderText="CveTbl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="tbl_clave"/>
                                                                    <asp:BoundField HeaderText="Tabla" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="tbl_nombre"/>
                                                                    <asp:BoundField HeaderText="Responsable" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_responsable"/>
                                                                    <asp:BoundField HeaderText="Area" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_area"/>
                                                                    <asp:BoundField HeaderText="Recibido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantreci"/>
                                                                    <asp:BoundField HeaderText="Rechazado" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="qud_cantrecha"/>
                                                                    <asp:BoundField HeaderText="Unidad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_unidad"/>
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
                                <!-- CARGAR DATOS DE RESPONSABLES DE ACCIONES -->
                                <div class="row">
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="upnResponsables" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwResponsable" runat="server" AutoGenerateColumns="false" 
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowcommand="gvwResponsable_RowCommand" >
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Folio" DataField="acc_folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="Cve. Resp." DataField="acc_responsable"/>
                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="resp_nombre"/>
                                                            <asp:BoundField HeaderText="Causa" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_causa"/>
                                                            <asp:ButtonField HeaderText="Acciones" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Acciones" CommandName="muestradatos"/> 
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
                                                    <asp:Label ID="lblClaveResp" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="lblMensaje" runat="server" Text="Seleccione al responsable de las acciones antes de añadir cualquier accion" CssClass="cols-sm-3 label label-danger"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="upnDatos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="txtCausa" class="col-sm-2 control-label">Causa</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCausa" MaxLength="300" TextMode="MultiLine" runat="server" 
                                                        CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="limpiacontroles" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group">
                                                <label for="txtAccion" class="col-sm-2 control-label">Acción</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtAccion" MaxLength="150" TextMode="MultiLine" runat="server" 
                                                        CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCausa" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtAccion" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="valInput" ControlToValidate="txtAccion" ValidationExpression="^[\s\S]{0,150}$" ErrorMessage="Máximo 150 caracteres" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>   
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="row">
                                    <div class="form-group">
                                        <label for="txtFechaEntrega" class="col-sm-2 control-label">Fecha entrega</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtFechaEntrega" MaxLength="10" runat="server" data-provide="datepicker" 
                                                CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFechaEntrega" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtAccion" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                            <%--onkeydown = "return (event.keyCode!=13)"<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato de fecha invalida. (ej.: dd/mm/yyyy)" 
                                            ValidationExpression="(((0|1)[1-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ValidationGroup="grupo1"
                                            ControlToValidate="txtFechaEntrega" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                    
                                
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:UpdatePanel ID="upnAdd" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnAdd" Text="Añadir" CssClass="btn btn-primary" 
                                                            ValidationGroup="grupo1" onclick="btnAdd_Click"   />
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
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowcommand="gvwAgregar_RowCommand" EmptyDataRowStyle-HorizontalAlign="Center" >
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Acción" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="accion"/>
                                                            <asp:BoundField HeaderText="Fecha" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="fecha"/>
                                                            <asp:ButtonField HeaderText="Quitar" Text="Quitar" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="quitar"/>
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
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:UpdatePanel ID="upnGuardar" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                            CssClass="btn btn-primary" 
                                                            onclick="btnGuardar_Click" Enabled="False"  />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <br /><br />
                                                <asp:UpdatePanel ID="upnCancelar" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnCancelar" Text="Limpiar"
                                                        CssClass="btn btn-primary" Visible="False" onclick="btnCancelar_Click"  />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
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
                                                            Text="Acción guardada y correo(s) enviado(s)" runat="server" Visible = "false" />
                                                        <asp:Label ID="lblWarning" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Acción no guardada, intentelo nuevamente" 
                                                            runat="server" Visible = "False"/>
                                                        <asp:Label ID="lblSuccessAcc" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Accion correctiva eliminada" runat="server" Visible = "false" />
                                                        <asp:Label ID="lblWarningAcc" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Acción correctiva no eliminada, intentelo nuevamente" 
                                                            runat="server" Visible = "False"/>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div class="alert alert-info">
                                                          <strong>PROCESANDO...</strong>Se esta registrando la informaci&oacute;n
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
                                            <asp:UpdatePanel ID="upnClaveAccDet" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblClaveAccDet" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="upnAcciones" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwAcciones" runat="server" AutoGenerateColumns="false" 
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        EmptyDataRowStyle-HorizontalAlign="Center" 
                                                        onrowcommand="gvwAcciones_RowCommand" 
                                                        onrowdatabound="gvwAcciones_RowDataBound" 
                                                        onrowdeleting="gvwAcciones_RowDeleting" >
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Clave" DataField="acc_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                            <asp:BoundField HeaderText="Folio" DataField="acc_folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="Resp." HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="acc_responsable"/>
                                                            <asp:BoundField HeaderText="Accion" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_accion"/>
                                                            <asp:BoundField HeaderText="Finaliza" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_fechatermino"/>
                                                            <asp:ButtonField HeaderText="Editar" Text="Editar" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="editar"/>
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
            </form>
            <script type="text/javascript">
                function recargadatepicker() {
                    $("#<%= txtFechaEntrega.ClientID %>").val('');
                }
            </script>
        </div>
    </div>
</body>
</html>
