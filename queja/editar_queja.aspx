<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editar_queja.aspx.cs" Inherits="queja.editar_queja" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />
    <%--<link href="css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />--%>
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>
    <%--<script src="Scripts/bootstrap-datepicker.js" type="text/javascript"></script--%>
    <script src="Scripts/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap-datepicker.es.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    $().ready(function () {
        $("#<%= txtFecha.ClientID %>").datepicker({
                autoclose: true,
                format: "dd/mm/yyyy",
                language: "es"
            }).on("changeDate", function () {
                $("#<%= txtFecha.ClientID %>").datepicker('hide');
            });
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
        //alert("pasa");
        

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
            background-color: #464646:
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
                        <center><h3><strong class="">Descripción Queja</strong></h3></center>
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
                                            <asp:Label ID="Label4" runat="server" Text="Queja: " CssClass="cols-sm-3 label label-primary"></asp:Label>
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
                                    
                                    <!-- HABILITACION DE CONTROLES -->
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
                                                        Enabled="False" AutoPostBack="true" 
                                                        onselectedindexchanged="ddlCliente_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtSucursal" class="col-sm-3 control-label">Sucursal</label>
                                                <div class="col-sm-8">
                                                    <asp:UpdatePanel ID="upnSucursales" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlSucursales" runat="server" CssClass="form-control" Enabled="False">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSucursales" runat="server" ErrorMessage="Debe elegir una opción" ControlToValidate="ddlSucursales" ForeColor="Red" InitialValue="0" ValidationGroup="grupo1">
                                                            </asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:TextBox runat="server" ID="txtSucursal" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Sucursal" 
                                                        onkeydown = "return (event.keyCode!=13)" Enabled="False" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtReporto" class="col-sm-3 control-label">Reportó</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtReporto" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Reportó" 
                                                        onkeydown = "return (event.keyCode!=13)" Enabled="False"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvReporto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtReporto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="ddlTipo" class="col-sm-3 control-label">Tipo</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" 
                                                        AutoPostBack="True" Enabled="False" >
                                                        <asp:ListItem Value="">ELEGIR OPCIÓN...</asp:ListItem>
                                                        <asp:ListItem Value="N">NACIONAL</asp:ListItem>
                                                        <asp:ListItem Value="E">EXPORTACION</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <p class="bg-primary text-center"><strong>Descripci&oacute;n del producto</strong></p>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtOrdenProd" class="col-sm-3 control-label">Orden de producción</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtOrdenProd" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Orden de producción" 
                                                        onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <center>
                                                        <asp:Button runat="server" ID="btnBuscaOrden" Text="Buscar" 
                                                            CssClass="btn btn-primary" onclick="btnBuscaOrden_Click"  />
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-offset-0 col-sm-12">
                                                    <asp:UpdatePanel ID="upnOrden" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvwOrden" runat="server" AutoGenerateColumns="false" 
                                                                Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                                                EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                 >
                                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                <Columns>
                                                                    <asp:ButtonField HeaderText="Clave" DataTextField="prod_clave" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="muestradatos" />
                                                                    <asp:BoundField HeaderText="Clave_2" DataField="prod_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                    <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="prod_nombre"/>
                                                                    <asp:BoundField HeaderText="Fecha" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="pti_fecha"/>
                                                                    <asp:BoundField HeaderText="Cve Prov" DataField="prov_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                    <asp:BoundField HeaderText="Proveedor" DataField="prov_nombre" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                    <asp:BoundField HeaderText="Cve Rch" DataField="rch_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                    <asp:BoundField HeaderText="Rancho" DataField="rch_nombre" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                    <asp:BoundField HeaderText="Cve Tbl" DataField="tbl_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                    <asp:BoundField HeaderText="Tabla" DataField="tbl_nombre" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                    <asp:BoundField HeaderText="Fecha Cad" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="fecha_cad"/>
                                                                    <asp:BoundField HeaderText="Tipo" HeaderStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" ItemStyle-CssClass="visible-lg hidden-md hidden-sm hidden-xs" DataField="tipo"/>
                                                                    <asp:BoundField HeaderText="Lote" DataField="lote" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnBuscaOrden" />
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
                                            <asp:UpdatePanel ID="upnRegistro" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="txt_clave" class="col-sm-3 control-label">Orden Prod</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_orden_prod" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_clave" class="col-sm-3 control-label">Clave</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_clave" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_nom" class="col-sm-3 control-label">Producto</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_nom" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Producto" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_lote" class="col-sm-3 control-label">Lote</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_lote" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Lote" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_fechacad" class="col-sm-3 control-label">Fecha Caducidad</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_fechacad" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Fecha caducidad" 
                                                                ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_cveprov" class="col-sm-3 control-label">Clave Proveedor:</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_cveprov" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave Proveedor" 
                                                                ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_nomprov" class="col-sm-3 control-label">Nombre Proveedor:</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_nomprov" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Proveedor" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_cverch" class="col-sm-3 control-label">Clave Rancho:</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_cverch" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave Rancho" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_nomrch" class="col-sm-3 control-label">Nombre Rancho:</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_nomrch" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Rancho" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_cvetbl" class="col-sm-3 control-label">Clave Tabla:</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_cvetbl" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave Tabla" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txt_nomtbl" class="col-sm-3 control-label">Nombre Tabla:</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txt_nomtbl" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Tabla" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="gvwOrden" EventName="RowCommand" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel  ID="upnDatos" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="txtRespLinea" class="col-sm-3 control-label">Responsable Línea</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txtRespLinea" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Responsable Línea" 
                                                                ReadOnly="True"></asp:TextBox>
                                                            <asp:HiddenField ID="txtRespLinea2" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="txtArea" class="col-sm-3 control-label">Area</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txtArea" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Area" ReadOnly="True"></asp:TextBox>
                                                            <asp:HiddenField ID="txtArea2" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscaOrden" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <label for="txtCantReci" class="col-sm-3 control-label">Cant Recibida</label>
                                                <div class="col-sm-8">
                                                    <asp:UpdatePanel ID="upnReci" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" ID="txtCantReci" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Cantidad recibida" 
                                                                onkeydown = "return (event.keyCode!=13)" Enabled="False" 
                                                                ontextchanged="txtCantReci_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:RequiredFieldValidator ID="rfvCantReci" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCantReci" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revCantReci" runat="server" ErrorMessage="Solo números" ValidationExpression="^[0-9]*\.?[0-9]+$" ControlToValidate="txtCantReci" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtCantRech" class="col-sm-3 control-label">Cant Rechazada</label>
                                                <div class="col-sm-8">
                                                    <asp:UpdatePanel ID="upnRecha" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" ID="txtCantRech" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Cantidad rechazada" 
                                                                onkeydown = "return (event.keyCode!=13)" Enabled="False" 
                                                                ontextchanged="txtCantRech_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:RequiredFieldValidator ID="rfvCantRech" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCantRech" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revCantRech" runat="server" ErrorMessage="Solo números" ValidationExpression="^[0-9]*\.?[0-9]+$" ControlToValidate="txtCantRech" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:UpdatePanel  ID="upnCajasRechazadas" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <label for="txtCantRech" class="col-sm-3 control-label">Cajas Prod.</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txtCajasProducidas" CssClass="form-control" Style="text-transform: uppercase" placeholder="Cajas Producidas" onkeydown = "return (event.keyCode!=13)" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group">
                                                <asp:UpdatePanel  ID="upnPorcentaje" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <label for="txtPorcentaje" class="col-sm-3 control-label">% Rechazo</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txtPorcentaje" CssClass="form-control" Style="text-transform: uppercase" placeholder="Porcentaje Rechazado" onkeydown = "return (event.keyCode!=13)" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtUnidad" class="col-sm-3 control-label">Unidad</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtUnidad" CssClass="form-control" Style="text-transform: uppercase" placeholder="Unidad" onkeydown = "return (event.keyCode!=13)" ReadOnly="true" Text="CAJA"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvUnidad" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtUnidad" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group nover">
                                                <label for="chkDevolucion" class="col-sm-3 control-label">¿Devolución?</label>
                                                <div class="col-sm-2">
                                                    <asp:CheckBox runat="server" ID="chkDevolucion" CssClass="form-control" 
                                                        Text="Si" Enabled="False"/>
                                                </div>
                                            </div>
                                            <div class="form-group nover">
                                                <label for="chkMerma" class="col-sm-3 control-label">¿Merma?</label>
                                                <div class="col-sm-2">
                                                    <asp:CheckBox runat="server" ID="chkMerma" CssClass="form-control" 
                                                        Text="Si" Enabled="False"/>
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
                                            <div class="form-group">
                                                <label for="ddlMoneda" class="col-sm-3 control-label">Moneda</label>
                                                <div class="col-sm-8">
                                                    <asp:UpdatePanel ID="uplMoneda" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlMoneda" CssClass="form-control" 
                                                                Enabled="False">
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
                                                    <asp:DropDownList runat="server" ID="ddlProblema" CssClass="form-control" 
                                                        Enabled="False">
                                                    </asp:DropDownList>
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
                                            <div class="form-group">
                                                <label for="txtPedido" class="col-sm-3 control-label">Pedido</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtPedido" CssClass="form-control" 
                                                        Style="text-transform: uppercase" placeholder="Pedido" 
                                                        onkeydown = "return (event.keyCode!=13)" Text="" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtObservaciones" class="col-sm-3 control-label">Observaciones</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtObservaciones" CssClass="form-control" 
                                                        Style="text-transform: uppercase"  placeholder="Observaciones" 
                                                        onkeydown = "return (event.keyCode!=13)" MaxLength="200" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                        <label for="txtCosto" class="col-sm-3 control-label">Costo</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtCosto" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Costo" onkeydown = "return (event.keyCode!=13)" Text="0" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCosto" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCosto" runat="server" ErrorMessage="Error de ingreso de número" ControlToValidate="txtCosto" ForeColor="Red" ValidationGroup="grupo1" ValidationExpression="\d{1,10}(.\d{1,2})?"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="form-group form-inline">
                                        <div class="col-sm-12 col-sm-offset-4">
                                            <center>
                                                <asp:Button runat="server" ID="btnEditar" Text="Editar" 
                                                    CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-2" 
                                                    onclick="btnEditar_Click"  />
                                                <asp:UpdatePanel ID="upnGuardar" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" ValidationGroup="grupo1"
                                                            CssClass="btn btn-primary col-xs-12 col-lg-2 col-sm-2" Enabled="false" 
                                                            onclick="btnGuardar_Click"/>
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
                                                        <asp:Button runat="server" ID="btnBorrar" Text="Borrar imagenes" 
                                                            CssClass="btn btn-primary" Enabled="False"  />
                                                        <asp:Button runat="server" ID="btnSubir" Text="Subir imagenes" 
                                                            CssClass="btn btn-primary" Enabled="False" onclick="btnSubir_Click"  />
                                                        <asp:Button runat="server" ID="btnTarima" Text="Seleccionar Tarima" 
                                                            CssClass="btn btn-primary" Enabled="False" onclick="btnTarima_Click"  />
                                                    </center>
                                                </div>
                                            </div>
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
                                                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-fullscreen" >
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
