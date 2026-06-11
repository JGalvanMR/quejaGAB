<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="servicios.aspx.cs" Inherits="queja.servicios" %>

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

    <script type="text/javascript">

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

        $().ready(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaEmb.ClientID %>").datepicker({
                minDate: -60,
                maxDate: 5
            });
        });

        function refrescar()
        {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#<%= txtFechaEmb.ClientID %>").datepicker({
                minDate: -120,
                maxDate: 5
            });
            //alert("HOLA DESDE JAVASCRIPT VIA ASP.NET");
        }

        function abre_pdf()
        {
            var clave = document.getElementById("<%= lblQueja.ClientID %>").innerHTML;
            window.open("repserv/" + clave + ".pdf","_blank");
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
    <div class="container col-lg-10 col-lg-offset-1">
        <div class="row">
            <div class="col-md-12 col-md-offset-0">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h1><strong class="">Quejas Clientes Servicios</strong></h1></center>
                    </div>
                    <div class="panel-body">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server" >
                            </asp:ScriptManager>
                            <%--  asp:UpdatePanel ID="udpGeneral" runat="server" UpdateMode="Conditional">
                                <ContentTemplate--%>
                                    <div class="panel panel-primary">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                                    <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                                    <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                                    <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>  
                                                    <asp:Label ID="Label4" runat="server" Text="Queja: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                                    <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>                                     
                                                </div>
                                                <asp:UpdatePanel ID="udpTipo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="form-group form-inline">
                                                            <asp:Button runat="server" ID="btnCancelar" Text="Limpiar Controles" 
                                                                CssClass="btn btn-primary" onclick="btnCancelar_Click"/>
                                                            <asp:Button runat="server" ID="btnRegresar" Text="Regresar" 
                                                                CssClass="btn btn-primary" onclick="btnRegresar_Click"/>
                                                            
                                                                    <asp:Button runat="server" ID="btnPDF" Text="Exportar a PDF" 
                                                                        CssClass="btn btn-danger" onclick="btnPDF_Click" />
                                                                
                                                                
                                                        </div>
                                                        <asp:Label ID="lblTipo" runat="server" Text="" CssClass="cols-sm-3 label label-success">-</asp:Label>
                                                        <asp:Label ID="lblTarea" runat="server" Text="" CssClass="cols-sm-3 label label-success">-</asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <br />
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
                                                                <label for="txtSemana" class="col-sm-2 control-label text-right">Semana</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtSemana" CssClass="form-control" 
                                                                        Style="text-transform: uppercase" placeholder="SEMANA" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <br />
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label for="txtFechaEmb" class="col-sm-2 control-label text-right">Fecha Embarque</label>
                                                        <div class="col-sm-8">
                                                            <asp:UpdatePanel ID="udpFechaEmb" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtFechaEmb" MaxLength="10" runat="server" data-provide="datepicker" 
                                                                        CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
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
                                                                <asp:UpdatePanel ID="upnCheck" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:CheckBox ID="chkSinRegistro" runat="server" Text="Trailer no registrado" AutoPostBack="true" OnCheckedChanged="chkSinRegistro_CheckedChanged"/>        
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </center>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                                <br />
                                                <asp:UpdatePanel ID="udpPlacas" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">   
                                                            <div class="form-group">
                                                                <div class="col-sm-12">
                                                                    <center>
                                                                        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
                                                                            CssClass="btn btn-primary" onclick="btnBuscar_Click" />
                                                                    </center>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="ddlTipo" class="col-sm-2 control-label text-right">Tipo</label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" 
                                                                        AutoPostBack="False">
                                                                            <asp:ListItem Value="">SELECCIONE TIPO...</asp:ListItem>
                                                                            <asp:ListItem Value="NACIONAL">NACIONAL</asp:ListItem>
                                                                            <asp:ListItem Value="EXPORTACION">EXPORTACION</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="ddlTransporte" class="col-sm-2 control-label text-right">Transporte</label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList runat="server" ID="ddlTransporte" CssClass="form-control" 
                                                                        AutoPostBack="True" 
                                                                        onselectedindexchanged="ddlTransporte_SelectedIndexChanged" >
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <br />
                                                <%-- asp:UpdatePanel ID="udpgral2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate --%>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtPedido" class="col-sm-2 control-label text-right">Pedido/Factura</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnPed" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtPedido" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="PEDIDO/FACTURA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtMes" class="col-sm-2 control-label text-right">Mes</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnMes" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>   
                                                                            <asp:TextBox runat="server" ID="txtMes" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="MES" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtTransportista" class="col-sm-2 control-label text-right">Transportista</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnTrans" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtTransportista" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="TRANSPORTISTA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtDestino" class="col-sm-2 control-label text-right">Destino</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnDest" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtDestino" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="DESTINO" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtCaja" class="col-sm-2 control-label text-right">Caja</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnCaja" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtCaja" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="CAJA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtResponsable" class="col-sm-2 control-label text-right">Responsable GAB</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtResponsable" CssClass="form-control" 
                                                                        Style="text-transform: uppercase" placeholder="RESPONSABLE GAB" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtAduana" class="col-sm-2 control-label text-right">Aduana</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnAdu" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate> 
                                                                            <asp:TextBox runat="server" ID="txtAduana" CssClass="form-control" 
                                                                                Style="text-transform: uppercase" placeholder="ADUANA" ReadOnly="True"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="ddlQueja" class="col-sm-2 control-label text-right">Queja</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnQuejasServ" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList runat="server" ID="ddlQueja" CssClass="form-control" 
                                                                                AutoPostBack="True"  >
                                                                            </asp:DropDownList>
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
                                                                        <asp:Button runat="server" ID="btnAgregar" Text="Agregar" 
                                                                            CssClass="btn btn-primary" onclick="btnAgregar_Click" />
                                                                    </center>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <center>
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="udpQuejas" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvwQuejas" runat="server" AutoGenerateColumns="false"
                                                                                Width="90%" CssClass="table table-bordered table-hover" 
                                                                                EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center"
                                                                                AllowPaging="true" onrowcommand="gvwQuejas_RowCommand">
                                                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Clave" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="ser_folio"/>
                                                                                    <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" DataField="ser_nombre"/>
                                                                                    <asp:ButtonField HeaderText="Quitar" Text="Quitar" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="quitar"/>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </center>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtComentario" class="col-sm-2 control-label text-right">Comentario</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtComentario" CssClass="form-control tamano" TextMode="MultiLine" Rows="3"
                                                                        Style="text-transform: uppercase" placeholder="COMENTARIO"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br /> 
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtPerdidaMn" class="col-sm-2 control-label text-right">Perdida MN</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtPerdidaMn" CssClass="form-control"
                                                                        Style="text-transform: uppercase" placeholder="PERDIDA MN" Text="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="txtPerdidaUsd" class="col-sm-2 control-label text-right">Perdida USD</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" ID="txtPerdidaUsd" CssClass="form-control"
                                                                        Style="text-transform: uppercase" placeholder="PERDIDA USD" Text="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label for="ddlArea" class="col-sm-2 control-label text-right">Area queja</label>
                                                                <div class="col-sm-8">
                                                                    <asp:UpdatePanel ID="upnArea" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList runat="server" ID="ddlArea" CssClass="form-control">
                                                                                <asp:ListItem Text="SELECCIONAR OPCION..." Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="EMBARQUES" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="TRANSPORTISTA" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="CLIENTE" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="ADUANA" Value="4"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row col-lg-offset-1 col-xs-10 col-sm-10 col-md-10 col-lg-10">
                                                            <div class="panel panel-primary">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        Imágenes capturadas
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
                                                                    <br /><br />
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
                                                    <%--/ContentTemplate>
                                                </asp:UpdatePanel --%>
                                                <br />
                                                <div class="row">   
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <center>
                                                                <asp:UpdatePanel ID="udpGuardar" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                                            CssClass="btn btn-primary" ValidationGroup="group1" 
                                                                            onclick="btnGuardar_Click" />
                                                                        <asp:Button runat="server" ID="btnSubir" Text="Subir imagenes" 
                                                                            CssClass="btn btn-primary" Enabled="False" onclick="btnSubir_Click" />
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
                                                                        <asp:Label ID="lblSuccess" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                                            Text="Queja guardada y correo(s) enviado(s)" runat="server" Visible = "false" />
                                                                        <asp:Label ID="lblWarning" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                                            Text="Queja no guardada, intentelo nuevamente" 
                                                                            runat="server" Visible = "False"/>
                                                                        <asp:Label ID="lblParcial" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                                            Text="La queja no se guardo completamente, verificar con sistemas" 
                                                                            runat="server" Visible = "False"/>
                                                                        
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <asp:UpdateProgress ID="UpdateProgress" runat="server">
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
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <div class="col-sm-12">
                                                                    <iframe id="Iframe1" runat="server" name="Iframe1" width="100%" height="500px" visible="false"></iframe>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>         
                                        </div>
                                    </div>
                                <%-- /ContentTemplate>
                            </asp:UpdatePanel --%>
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

