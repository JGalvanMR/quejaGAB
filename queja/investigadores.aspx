<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="investigadores.aspx.cs" Inherits="queja.investigadores" %>

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
    
    <%--<script src="Scripts/bootstrap-datepicker.es.min.js" type="text/javascript"></script>--%>
    
    <%--<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/css/datepicker.min.css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/css/datepicker3.min.css" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.min.js"></script>--%>

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

        //alert("pasa");
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

//        function Confirm() {
//            var confirm_value = document.createElement("INPUT");
//            confirm_value.type = "visible";
//            confirm_value.name = "confirm_value";
//            if (confirm("¿Desea borrar al investigador?")) {
//                confirm_value.value = "SI";
//            }
//            else {
//                confirm_value.value = "NO";
//            }
//            document.forms[0].appendChild(confirm_value);
//        }
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
    </style>
</head>
<body>
    <div class="container col-lg-12">
        <div class="col-md-10 col-md-offset-1">
            <form id="frmDatos" runat="server" class="form-horizontal">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Investigadores</strong></h3></center>
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
                                        <asp:Label ID="lblFecha_Carga_Inv" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-offset-1 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a menú" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">
                                                    Datos generales
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapse1" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtFolio" class="col-sm-2 control-label">Folio</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtFolio" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Folio" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtSemana" class="col-sm-2 control-label">Semana</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtSemana" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Semana" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtMes" class="col-sm-2 control-label">Mes</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtMes" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Mes" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtFecha" class="col-sm-2 control-label">Fecha</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Fecha" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtSucursal" class="col-sm-2 control-label">Cliente</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtCliente" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Cliente" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtSucursal" class="col-sm-2 control-label">Subcliente</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtSucursal" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Subcliente" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtReporto" class="col-sm-2 control-label">Reportó</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtReporto" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Reporto" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtRecibio" class="col-sm-2 control-label">Recibió</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtRecibio" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Recibio" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtTipo" class="col-sm-2 control-label">Tipo</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtTipo" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Tipo" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">
                                                    Datos Orden
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collap" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtOrdenProd" class="col-sm-2 control-label">Orden Prod</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtOrdenProd" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Orden Produccion" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_clave" class="col-sm-2 control-label">Clave</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_clave" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_nom" class="col-sm-2 control-label">Producto</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_nom" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Producto" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_lote" class="col-sm-2 control-label">Lote</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_lote" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Lote" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_fechacad" class="col-sm-2 control-label">Caducidad</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_fechacad" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Fecha caducidad" 
                                                                ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_cveprov" class="col-sm-2 control-label">Proveedor:</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_cveprov" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave Proveedor" 
                                                                ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_nomprov" class="col-sm-2 control-label">Nombre:</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_nomprov" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Proveedor" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_cverch" class="col-sm-2 control-label">Rancho:</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_cverch" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave Rancho" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_nomrch" class="col-sm-2 control-label">Nombre:</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_nomrch" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Rancho" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_cvetbl" class="col-sm-2 control-label">Tabla:</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_cvetbl" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Clave Tabla" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txt_nomtbl" class="col-sm-2 control-label">Nombre:</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txt_nomtbl" CssClass="form-control" 
                                                                Style="text-transform: uppercase"  placeholder="Tabla" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtRespLinea" class="col-sm-2 control-label">Responsable</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtRespLinea" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Responsable Línea" 
                                                                ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtArea" class="col-sm-2 control-label">Area</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtArea" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Area" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtCantReci" class="col-sm-2 control-label">Recibido</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtCantReci" CssClass="form-control" Style="text-transform: uppercase" placeholder="Cantidad recibida" ReadOnly="true">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtCantRech" class="col-sm-2 control-label">Rechazado</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtCantRech" CssClass="form-control" Style="text-transform: uppercase" placeholder="Cantidad rechazada" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtUnidad" class="col-sm-2 control-label">Unidad</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtUnidad" CssClass="form-control" Style="text-transform: uppercase" placeholder="Unidad" onkeydown = "return (event.keyCode!=13)" ReadOnly="true" Text="">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="chkDevolucion" class="col-sm-2 control-label">¿Devolución?</label>
                                                        <div class="col-sm-2">
                                                            <asp:CheckBox runat="server" ID="chkDevolucion" CssClass="form-control" Text="Si"/>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtMoneda" class="col-sm-2 control-label">Moneda</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtMoneda" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Moneda" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtProblema" class="col-sm-2 control-label">Problema</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtProblema" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Problema" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtArea" class="col-sm-2 control-label">Area Gral</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtAreaGral" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Area" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtTarima" class="col-sm-2 control-label">Tarima</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" ID="txtTarima" CssClass="form-control" 
                                                                Style="text-transform: uppercase" placeholder="Tarima" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse4">
                                                    Imágenes
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapse3" class="panel-collapse collapse">
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
                                
                                
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel panel-heading">
                                            <h4 class="panel-title">
                                                Investigadores
                                            </h4>
                                        </div>
                                        <asp:UpdatePanel ID="upnDatos" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="panel panel-body">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtResponsable" class=" col-sm-offset-0 col-sm-2 control-label">Responsable</label>
                                                            <div class="col-sm-9">
                                                                <asp:UpdatePanel ID="uplResp1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlResp1" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtFecha" class="col-sm-2 control-label">Fecha entrega</label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtFecha" MaxLength="300" runat="server" data-provide="datepicker"
                                                                    CssClass="form-control" Style="text-transform: uppercase" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtCausa" class="col-sm-2 control-label">Comentario</label>
                                                            <div class="col-sm-9">
                                                                <asp:UpdatePanel ID="uplCausa" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtCausa" MaxLength="300" TextMode="MultiLine" runat="server" 
                                                                            CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCausa" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtCausa" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator runat="server" ID="valInput" ControlToValidate="txtCausa" ValidationExpression="^[\s\S]{0,200}$" ErrorMessage="Máximo 100 caracteres" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>       
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <center>
                                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                                        ValidationGroup="grupo1" CssClass="btn btn-primary" 
                                                                        onclick="btnGuardar_Click"/>
                                                                    <br /><br />
                                                                    <asp:UpdatePanel ID="upnCancelar" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Button runat="server" ID="btnCancelar" Text="Limpiar"
                                                                            CssClass="btn btn-primary" Visible="False" onclick="btnCancelar_Click" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </center>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                    <asp:UpdatePanel ID="upnOperacion" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Label ID="lblOperacion" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
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
                                                                                Text="Investigacion eliminado" runat="server" Visible = "false" />
                                                                            <asp:Label ID="lblWarningInv" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                                                Text="Investigación no eliminado, intentelo nuevamente" 
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
                                                    <center>
                                                        <div class="form-group">
                                                            <asp:UpdatePanel ID="upnResponsables" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="gvwResponsables" runat="server" AutoGenerateColumns="false" 
                                                                        Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                        onrowcommand="gvwResponsables_RowCommand" 
                                                                        onrowdatabound="gvwResponsables_RowDataBound" 
                                                                        onrowdeleting="gvwResponsables_RowDeleting">
                                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Folio" DataField="inv_folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                                            <asp:BoundField HeaderText="No." DataField="inv_responsable"/>
                                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="emp_nombre"/>
                                                                            <asp:BoundField HeaderText="Comentario" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="inv_comentario"/>
                                                                            <asp:BoundField HeaderText="Entrega" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="inv_fechaentrega"/>
                                                                            <asp:ButtonField HeaderText="Editar" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Editar" CommandName="muestradatos"/> 
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
</body>
</html>
