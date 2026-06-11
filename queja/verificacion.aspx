<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verificacion.aspx.cs" Inherits="queja.verificacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>
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


        $(".modal-fullscreen2").on('show.bs.modal', function () {
            setTimeout(function () {
                $(".modal-backdrop").addClass("modal-backdrop-fullscreen");
            }, 0);
        });
        $(".modal-fullscreen2").on('hidden.bs.modal', function () {
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
          
          
          .modal-fullscreen2 {
          background: transparent;
        }
        .modal-fullscreen2 .modal-content2 {
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
        .modal-fullscreen2 .modal-dialog2 {
          margin: 0;
          margin-right: auto;
          margin-left: auto;
          width: 100%;
        }
        .modal-title2
        {
            color: #ffffff;
            }
        @media (min-width: 768px) {
          .modal-fullscreen2 .modal-dialog2 {
            width: 750px;
          }
        }
        @media (min-width: 992px) {
          .modal-fullscreen2 .modal-dialog2 {
            width: 970px;
          }
        }
        @media (min-width: 1200px) {
          .modal-fullscreen2 .modal-dialog2 {
             width: 1170px;
          }
        }
        
    </style>
</head>
<body>
    <div class="container col-lg-12">
        <div class="col-md-10 col-md-offset-1">
            <form id="form1" runat="server" class="form-horizontal">
                
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Verificaci&oacute;n</strong></h3></center>
                    </div>
                    <div class="panel panel-body">
                        <div class="panel panel-primary">
                            <div class="panel panel-body">
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
                                                CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" 
                                                ReadOnly="true" Visible="False"></asp:TextBox>
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
                                        <label for="txtFecha" class="col-sm-2 control-label">Fecha registro</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtFecha" MaxLength="300" runat="server" 
                                                CssClass="form-control" Style="text-transform: uppercase" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">
                                                    Imágenes antes
                                                </a>
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
                                                        <button type="button" id="btnFotosAnt" class="btn btn-primary" data-toggle="modal" data-target="#modal-fullscreen" onclick="cargarfoto()" >
                                                            Ver imagenes
                                                        </button>
                                                    </center>
                                                </div>
                                                <br /><br />
                                                <div class="row">
                                                    <div class="form-group">
                                                        <center>
                                                            <div class="col-sm-offset-2 col-sm-8">
                                                                <asp:Button runat="server" ID="btnFile1" Text="File" 
                                                                    CssClass="btn btn-primary" onclick="btnFile1_Click" />
                                                                <asp:Button runat="server" ID="btnFile2" Text="File" 
                                                                    CssClass="btn btn-primary" onclick="btnFile2_Click" />
                                                                <asp:Button runat="server" ID="btnFile3" Text="File" 
                                                                    CssClass="btn btn-primary" onclick="btnFile3_Click" />
                                                            </div>
                                                        </center>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="upnResponsables" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwResponsables" runat="server" AutoGenerateColumns="false" 
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover" 
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowcommand="gvwResponsables_RowCommand" >
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Folio" DataField="inv_folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="No." DataField="inv_responsable" />
                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="resp_nombre"/>
                                                            <asp:BoundField HeaderText="Comentario" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="inv_comentario"/>
                                                            <asp:ButtonField HeaderText="Responsable" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Responsable" CommandName="consulta"/> 
                                                            <%--<asp:BoundField HeaderText="Causa" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="inv_causa"/>--%>
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
                                            <asp:UpdatePanel ID="upnRespAccion" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwRespAcciones" runat="server" AutoGenerateColumns="false"
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover"
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowcommand="gvwRespAcciones_RowCommand">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="true" ForeColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Folio" DataField="acc_folio" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="No." HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="acc_responsable" />
                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="resp_nombre" />
                                                            <asp:BoundField HeaderText="Causa" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_causa" />
                                                            <asp:ButtonField HeaderText="Acciones" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Acciones" CommandName="consulta_acciones"/> 
                                                        </Columns>
                                                        </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </center>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <asp:UpdatePanel ID="upnRespAcciones" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblFolioRespAcc" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="lblClaveRespAcc" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    <asp:Label ID="lblNombreRespAcc" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="upnAccionesCorrectivas" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwAccionesCorrectivas" runat="server" AutoGenerateColumns="false"
                                                    Width="95%" CssClass="table table-striped table-bordered table-hover"
                                                    EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                        onrowcommand="gvwAccionesCorrectivas_RowCommand">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="true" ForeColor="White" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Folio" DataField="acc_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                        <asp:BoundField HeaderText="Acción" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="acc_accion" />
                                                        <asp:BoundField HeaderText="Termino" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_fechatermino" />
                                                        <asp:BoundField HeaderText="Verificado" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_fecha_ver" />
                                                        <asp:BoundField HeaderText="Cumplimiento" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_cumpli_ver" />
                                                        <asp:BoundField HeaderText="Comentario" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_comen_ver" />
                                                        <asp:ButtonField HeaderText="Verificar" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Verificar" CommandName="verificar_accion"/> 
                                                    </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </center>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:UpdatePanel ID="upnAccioncorrectiva" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblAccionCorrectiva" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                        <asp:Label ID="lblAccionCorrectivaDesc" runat="server" Text="-" CssClass="cols-sm-3 label label-success"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
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
                                <%--<div class="row">
                                    <center>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="uplAcciones" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwAcciones" runat="server" AutoGenerateColumns="false"
                                                        Width="95%" CssClass="table table-striped table-bordered table-hover"
                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true"
                                                        EmptyDataRowStyle-HorizontalAlign="Center" 
                                                        onrowcommand="gvwAcciones_RowCommand">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" /> 
                                                        <Columns>
                                                            <asp:ButtonField HeaderText="Cve. Acc." DataTextField="acc_clave" ItemStyle-CssClass="form-control btn btn-primary" ItemStyle-ForeColor="White" CommandName="consulta"/>
                                                            <asp:BoundField HeaderText="Cve. Acc." DataField="acc_clave" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="Cve. Res." DataField="acc_responsable" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                                            <asp:BoundField HeaderText="Nombre" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="acc_nombre"/>
                                                            <asp:BoundField HeaderText="Accion" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_accion"/>
                                                            <asp:BoundField HeaderText="Fecha Termino" HeaderStyle-CssClass="visible-lg visible-md hidden-sm hidden-xs" ItemStyle-CssClass="hidden-xs visible-md visible-lg hidden-sm" DataField="acc_fechatermino"/>
                                                        </Columns>   
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </center>
                                </div>--%>
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                    Datos Verificación
                                            </h4>
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="updateVerificacion" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtFechaVer" class="col-sm-2 control-label">Fecha</label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtFechaVer" runat="server" CssClass="form-control" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtCumplimiento" class="col-sm-2 control-label">Cumplimiento</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlCumplimiento" runat="server" ViewStateMode="Enabled" CssClass="form-control" ValidationGroup="grupo1">
                                                                    <asp:ListItem Value="">SELECCIONE UNA OPCION...</asp:ListItem>
                                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <asp:RequiredFieldValidator id="rfvCumplimiento" ControlToValidate="ddlCumplimiento" ErrorMessage="Seleccione un elemento" Display="Static"
                                                                InitialValue="" runat="server" ForeColor="Red" ValidationGroup="grupo1" ></asp:RequiredFieldValidator>
                                                            <%--<div class="col-sm-9">
                                                                <asp:TextBox ID="txtCumplimiento" runat="server" CssClass="form-control" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>    
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label for="txtComentario" class="col-sm-2 control-label">Comentario</label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtComentario" MaxLength="150" TextMode="MultiLine" runat="server" 
                                                                    CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ValidationGroup="grupo1"></asp:TextBox> 
                                                                <asp:RequiredFieldValidator ID="rfvComentario" ControlToValidate="txtComentario" ErrorMessage="Ingrese el comentario" runat="server" ForeColor="Red" ValidationGroup="grupo1">
                                                                </asp:RequiredFieldValidator>   
                                                            </div>
                                                        </div>
                                                    </div>    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="row">
                                                <div class="form-group">
                                                    <center>
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <asp:UpdatePanel ID="upnGuardar" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                                    ValidationGroup="grupo1" CssClass="btn btn-primary" 
                                                                    onclick="btnGuardar_Click" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
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
                                                    <center>
                                                        <div class="col-sm-12">
                                                            <asp:UpdatePanel ID="upnMensajes" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="lblSuccess" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                                        Text="Verificación guardada y correo(s) enviado(s)" runat="server" Visible = "false" />
                                                                    <asp:Label ID="lblWarning" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                                        Text="Verificación no guardada, intentelo nuevamente" 
                                                                        runat="server" Visible = "False"/>
                                                                    <asp:Label ID="lblSuccessInv" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                                        Text="Investigacion eliminado" runat="server" Visible = "false" />
                                                                    <asp:Label ID="lblWarningInv" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                                        Text="Investigación no eliminado, intentelo nuevamente" 
                                                                        runat="server" Visible = "False"/>
                                                                    <asp:Timer ID="Timer1" runat="server" Interval="3000" ontick="Timer1_Tick">
                                                                    </asp:Timer>
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
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnImagenes" Text="Subir Imagenes"
                                                        CssClass="btn btn-primary" onclick="btnImagenes_Click" Enabled="False"/>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                    Imágenes después
                                            </h4>
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <center>
                                                        <asp:UpdatePanel ID="uplImagenes" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Image ID="imagen1" runat="server" CssClass="img-thumbnail img-responsive" style="width: 250px; height: 150px;"/>
                                                                <asp:Image ID="imagen2" runat="server" CssClass="img-thumbnail img-responsive" style="width: 250px; height: 150px;"/>
                                                                <asp:Image ID="imagen3" runat="server" CssClass="img-thumbnail img-responsive" style="width: 250px; height: 150px;"/>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>   
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <center>
                                                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-fullscreen2">
                                                            Ver imagenes
                                                        </button>
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <center>
                                                        <div class="col-sm-offset-2 col-sm-8">
                                                            <asp:UpdatePanel ID="upnBotones" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Button runat="server" ID="File1" Text="File"
                                                                        CssClass="btn btn-primary" onclick="File1_Click" />
                                                                    <asp:Button runat="server" ID="File2" Text="File"
                                                                        CssClass="btn btn-primary" onclick="File2_Click"  />
                                                                    <asp:Button runat="server" ID="File3" Text="File"
                                                                        CssClass="btn btn-primary" onclick="File3_Click"  />    

                                                                    <asp:GridView ID="grvArchivos" runat="server" AutoGenerateColumns="false" Visible="false"
                                                                        Width="95%" CssClass="table table-striped table-bordered table-hover"
                                                                        EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                                        onrowcommand="grvArchivos_RowCommand">
                                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="true" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Archivo" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-xs visible-md visible-lg visible-sm" DataField="nombre" />
                                                                            <asp:ButtonField HeaderText="Ver" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="btn btn-primary visible-xs visible-md visible-lg visible-sm" ItemStyle-ForeColor="White" Text="Ver" CommandName="ver_archivo"/> 
                                                                        </Columns>
                                                                        </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </center>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
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

            <!-- Modal fullscreen -->
            <div class="modal modal-fullscreen2 fade" id="modal-fullscreen2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog2">
                <div class="modal-content2">
                    <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title2" id="H1">Vista de imagenes</h4>
                    </div>
                    <div class="modal-body">
                    <center>
                        <div class="container">
                            <br>
                            <div id="myCarousel2" class="carousel slide" data-ride="carousel">
                            <!-- Indicators -->
                            <ol class="carousel-indicators">
                                <li data-target="#myCarousel2" data-slide-to="0" class="active"></li>
                                <li data-target="#myCarousel2" data-slide-to="1"></li>
                                <li data-target="#myCarousel2" data-slide-to="2"></li>
                            </ol>

                            <!-- Wrapper for slides -->
                            <asp:UpdatePanel ID="upnFotosDes" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="carousel-inner" role="listbox">
                                        <div class="item active">
                                        <!-- img id="imagen1" alt="No disponible" width="460" height="345" /-->
                                        <asp:Image ID="Image4" runat="server" CssClass="img-responsive"/>
                            
                                        </div>

                                        <div class="item">
                                        <!-- img id="imagen2" alt="No disponible" width="460" height="345" -->
                                        <asp:Image ID="Image5" runat="server" CssClass="img-responsive"/>
                            
                                        </div>
    
                                        <div class="item">
                                        <!-- img id="imagen3" alt="No disponible" width="460" height="345" -->
                                        <asp:Image ID="Image6" runat="server" CssClass="img-responsive"/>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                                    
                            

                            <!-- Left and right controls -->
                            <a class="left carousel-control" href="#myCarousel2" role="button" data-slide="prev">
                                <span style="width: 90px; height: 90px; background-image: url(imagenes/appbar.chevron.left.png); display:block;" />
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#myCarousel2" role="button" data-slide="next">
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
