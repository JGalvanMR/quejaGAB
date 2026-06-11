<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editar_queja_exp.aspx.cs" Inherits="queja.editar_queja_exp" %>

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
            
        })
        $(".modal-transparent").on('show.bs.modal', function () {
                setTimeout(function () {
                    $(".modal-backdrop").addClass("modal-backdrop-transparent");
                }, 0);
                });
                $(".modal-transparent").on('hidden.bs.modal', function () {
                    $(".modal-backdrop").addClass("modal-backdrop-transparent");
                });

                $(".modal-fullscreen").on('show.bs.modal', function () {
                    setTimeout(function () {
                        $(".modal-backdrop").addClass("modal-backdrop-fullscreen");
                    }, 0);
                });
                $(".modal-fullscreen").on('hidden.bs.modal', function () {
                    $(".modal-backdrop").addClass("modal-backdrop-fullscreen");
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
        .modal-fullscreen {
          background: transparent;
        }
        .modal-fullscreen .modal-content {
          background: transparent;
          border: 0;
          -webkit-box-shadow: none;
          box-shadow: none;
        }
        .modal-backdrop.modal-backdrop-fullscreen {
          background: #ffffff;
        }
        .modal-backdrop.modal-backdrop-fullscreen.in {
          opacity: .97;
          filter: alpha(opacity=97);
        }
        .modal-fullscreen .modal-dialog {
          margin: 0;
          margin-right: auto;
          margin-left: auto;
          width: 100%;
        }
        .modal-title
        {
            color: #ffffff;
            }
        @media (min-width: 768px) {
          .modal-fullscreen .modal-dialog {
            width: 750px;
          }
        }
        @media (min-width: 992px) {
          .modal-fullscreen .modal-dialog {
            width: 970px;
          }
        }
        @media (min-width: 1200px) {
          .modal-fullscreen .modal-dialog {
             width: 1170px;
          }
        }
        
        .carousel-inner > .item > img,
          .carousel-inner > .item > a > img {
              width: 70%;
              margin: auto;
          }
        
        .nover
        {
            display:none;
            }
    </style>
</head>
<body>
    <div class="container col-lg-12">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
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
                                            <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-3 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                                                <asp:Button runat="server" ID="btnReporte" Text="PDF"
                                                CssClass="btn btn-primary" onclick="btnReporte_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:UpdatePanel ID="upnEdicion" runat="server" UpdateMode="Conditional"> 
                                        <ContentTemplate>
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
                                                        Enabled="false" onselectedindexchanged="ddlCliente_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="ddlSucursales" class="col-sm-3 control-label">Sucursal</label>
                                                <div class="col-sm-8">
                                                    <asp:UpdatePanel ID="upnSucursales" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlSucursales" runat="server" CssClass="form-control" Enabled="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSucursales" runat="server" ErrorMessage="Debe elegir una opción" ControlToValidate="ddlSucursales" ForeColor="Red" InitialValue="" ValidationGroup="grupo1">
                                                            </asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:TextBox runat="server" ID="txtSucursal" Visible="false" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Sucursal" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtReporto" class="col-sm-3 control-label">Reportó</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtReporto" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Reportó" onkeydown = "return (event.keyCode!=13)" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvReporto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtReporto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="ddlTipo" class="col-sm-3 control-label">Tipo</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" 
                                                        AutoPostBack="True" Enabled="false" >
                                                        <asp:ListItem Value="">ELEGIR OPCIÓN...</asp:ListItem>
                                                        <asp:ListItem Value="N">NACIONAL</asp:ListItem>
                                                        <asp:ListItem Value="E">EXPORTACION</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtTranporte" class="col-sm-3 control-label">Transporte</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtTransporte" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Transporte" onkeydown = "return (event.keyCode!=13)" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtPedido" class="col-sm-3 control-label">Pedido</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtPedido" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Pedido" onkeydown = "return (event.keyCode!=13)" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <p class="bg-primary text-center"><strong>Descripci&oacute;n del producto</strong></p>
                                                </div>
                                            </div>
                                            <br />
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
                                                                <asp:BoundField HeaderText="Rechazado" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantrecha"/>
                                                                <asp:BoundField HeaderText="Unidad" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_unidad"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <asp:Button runat="server" ID="btnModRechazado" Text="Modificar Cajas Rechazadas" 
                                                        CssClass="btn btn-primary" onclick="btnModRechazado_Click"  />
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="uplControles" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-group nover">
                                                        <label for="chkDevolucion" class="col-sm-3 control-label">¿Devolución?</label>
                                                        <div class="col-sm-2">
                                                            <asp:CheckBox runat="server" ID="chkDevolucion" CssClass="form-control" Text="Si" Enabled="false"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group nover">
                                                        <label for="chkMerma" class="col-sm-3 control-label">¿Merma?</label>
                                                        <div class="col-sm-2">
                                                            <asp:CheckBox runat="server" ID="chkMerma" CssClass="form-control" Text="Si" Enabled="false"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="rbtList" class="col-sm-3 control-label">¿Seleccionar opción?</label>
                                                        <div class="col-sm-8">
                                                            <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" 
                                                                ID="rbtList" runat="server" CssClass="radio" Enabled="False">
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
                                                                    <asp:DropDownList runat="server" ID="ddlMoneda" CssClass="form-control" Enabled="false">
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
                                                            <asp:DropDownList runat="server" ID="ddlProblema" CssClass="form-control" Enabled="false">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txtCosto" class="col-sm-3 control-label">Costo</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txtCosto" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Costo" onkeydown = "return (event.keyCode!=13)" Enabled="false"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCosto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revCosto" runat="server" ErrorMessage="Error de ingreso de número" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1" ValidationExpression="\d{1,10}(.\d{1,2})?"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div> 
                                                    <div class="form-group">
                                                        <label for="txtObservaciones" class="col-sm-3 control-label">Observaciones</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txtObservaciones" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Observaciones" onkeydown = "return (event.keyCode!=13)" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div> 
                                                    <div class="form-group">
                                                        <label for="ddlAreaQueja" class="col-sm-3 control-label">Área Queja</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ID="ddlAreaQueja" CssClass="form-control" 
                                                                Enabled="False">
                                                            </asp:DropDownList>   
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <center>
                                                        <asp:Button runat="server" ID="btnEditar" Text="Editar" 
                                                            CssClass="btn btn-primary" onclick="btnEditar_Click" />
                                                        <asp:UpdatePanel ID="upnGuardar" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Button runat="server" ID="btnGuardar" Text="Guardar" ValidationGroup="grupo1"
                                                                    CssClass="btn btn-primary" Enabled="false" onclick="btnGuardar_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </center>
                                                </div>
                                            </div>   
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <asp:UpdatePanel ID="upnMensajes" runat="server" UpdateMode="Conditional">
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
                                                                    CssClass="btn btn-primary" onclick="btnSubir_Click"   />
                                                            </center>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
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

                                    <div class="row">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    Imágenes
                                                </h4>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <center>
                                                        <asp:Image ID="img1" runat="server" CssClass="img-thumbnail img-responsive" style="width: 250px; height: 150px;"/>
                                                        <asp:Image ID="img2" runat="server" CssClass="img-thumbnail img-responsive" style="width: 250px; height: 150px;"/>
                                                        <asp:Image ID="img3" runat="server" CssClass="img-thumbnail img-responsive" style="width: 250px; height: 150px;"/>
                                                        </center>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <center>
                                                            <asp:Button ID="file1" runat="server" class="btn btn-primary" Text="File" 
                                                                onclick="file1_Click" />
                                                        </center>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <center>
                                                            <asp:Button ID="file2" runat="server" class="btn btn-primary" Text="File" 
                                                                onclick="file2_Click" />
                                                        </center>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <center>
                                                            <asp:Button ID="file3" runat="server" class="btn btn-primary" Text="File" 
                                                                onclick="file3_Click" />
                                                        </center>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <center>
                                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-fullscreen" onclick="cargarfoto()" >
                                                                Ver imagenes
                                                            </button>
                                                        </center>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                  </div>  
                            </div>
                            <!-- Modal fullscreen -->
                            <div class="modal modal-fullscreen fade" id="modal-fullscreen" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                                    <h4 class="modal-title" id="myModalLabel">Vista de imagenes</h4>
                                    </div>
                                    <div class="modal-body">
                                    <center>
                                        <div class="container">
                                            <br>
                                            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                            <!-- Indicators -->
                                            <ol class="carousel-indicators">
                                                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                                <li data-target="#myCarousel" data-slide-to="1"></li>
                                                <li data-target="#myCarousel" data-slide-to="2"></li>
                                            </ol>

                                            <!-- Wrapper for slides -->
                                            <div class="carousel-inner" role="listbox">
                                                <div class="item active">
                                                <!-- img id="imagen1" alt="No disponible" width="460" height="345" /-->
                                                <asp:Image ID="Image1" runat="server" CssClass="img-responsive"/>
                            
                                                </div>

                                                <div class="item">
                                                <!-- img id="imagen2" alt="No disponible" width="460" height="345" -->
                                                <asp:Image ID="Image2" runat="server" CssClass="img-responsive"/>
                            
                                                </div>
    
                                                <div class="item">
                                                <!-- img id="imagen3" alt="No disponible" width="460" height="345" -->
                                                <asp:Image ID="Image3" runat="server" CssClass="img-responsive"/>
                                                </div>
                                            </div>

                                            <!-- Left and right controls -->
                                            <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                                                <span style="width: 90px; height: 90px; background-image: url(imagenes/appbar.chevron.left.png); display:block;" />
                                                <span class="sr-only">Previous</span>
                                            </a>
                                            <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                                                <span style="width: 90px; height: 90px; background-image: url(imagenes/appbar.chevron.right.png); display:block;" />
                                                <span class="sr-only">Next</span>
                                            </a>
                                            </div>
                                        </div>
                                    </center>
                                    </div>
                                    <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                                    </div>
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
