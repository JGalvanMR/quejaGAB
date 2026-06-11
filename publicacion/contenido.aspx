<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contenido.aspx.cs" Inherits="queja.contenido" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="css/jasny-bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jasny-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
    $(function () {
        $("#dialog").dialog();
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
    </style>
</head>
<body>
    <div class="container">
        <div class="col-md-12">
            <form id="frmPrincipal" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                </asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Detalles Queja</strong></h3></center>
                    </div>
                    <div  class="panel-body">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                    <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                    <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                    <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                    <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                </div>
                                <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                                    <div class="page-header">
                                        <h1>
                                            Queja: <asp:Label ID="lblQueja" runat="server" Text=""></asp:Label>
                                            <small>
                                                <asp:Label ID="lblCliente" runat="server" Text=""></asp:Label>
                                            </small>
                                        </h1>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-offset-0 col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a quejas" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:UpdatePanel ID="upnMensaje" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblErrorArea" CssClass="alert alert-danger col-lg-12 col-md-12 col-sm-12 col-xs-12" Font-Bold="True" role="alert"
                                                        Text="La queja no tiene asignada un área, favor de editar la queja" 
                                                        runat="server" Visible = "False"/>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:UpdatePanel ID="upnNotaCredito2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblNotaCredito" CssClass="alert alert-info col-lg-12 col-md-12 col-sm-12 col-xs-12" Font-Bold="True" role="alert"
                                                        Text="La queja ya ha sido asignada para nota de credito, no se puede reasignar" 
                                                        runat="server" Visible = "False"/>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-lg-12 col-sm-12 col-sm-12 col-xs-12">
                                            <asp:UpdatePanel ID="upnNotaCreditoVencida" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblNotaCreditoVencida" CssClass="alert alert-info col-lg-12 col-md-12 col-sm-12 col-xs-12" Font-Bold="True" role="alert"
                                                        Text="La Nota de Cr&eacute;dito ya no puede ser confirmada o rechazada porque ya super&oacute; los nueve d&iacute;as para realizar la operaci&oacute;n."
                                                        runat="server" Visible="False" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:UpdatePanel ID="upnNotaMp" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblNotaMp" CssClass="alert alert-warning col-lg-12 col-md-12 col-sm-12 col-xs-12" Font-Bold="True" role="alert"
                                                        Text="Se localizaron productos con más de una materia prima como componente, favor de seleccionar por lo menos una de ellas para seguir con el proceso de la queja" 
                                                        runat="server" Visible = "False"/>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:UpdatePanel ID="upnSoporteAviso" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblSoporteAviso" CssClass="alert alert-info col-lg-12 col-md-12 col-sm-12 col-xs-12" Font-Bold="True" role="alert"
                                                        Text="La queja requiere de un soporte(PDF) por motivo de Devoluci&oacute;n - Merma - Bonificaci&oacute;n" 
                                                        runat="server" Visible = "False"/>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1473881847_magnifier.png" alt="Consulta"/>
                                            <div class="caption">
                                                <h3>Consulta de queja</h3>
                                                <p>Consulta de los detalles de la queja</p>
                                                <p>
                                                <asp:UpdatePanel ID="uplConsulta" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnConsulta" runat="server" CssClass="btn btn-primary" 
                                                            Text="Entrar" onclick="btnConsulta_Click" Visible="False" />
                                                        <asp:Button ID="btnEdicion" runat="server" CssClass="btn btn-primary" 
                                                            Text="Entrar Editar" onclick="btnEdicion_Click" />
                                                        <asp:Button ID="btnEditarPed" runat="server" CssClass="btn btn-primary" 
                                                            Text="Editar Queja Pedido" onclick="btnEditarPed_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1473882341_profle.png" alt="Investigación" />
                                            <div class="caption">
                                                <h3>Asignación de Investigadores</h3>
                                                <p>Asignar responsables de investigar las causas del problema</p>
                                                <p>
                                                    <asp:UpdatePanel ID="uplInvestigacion" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnInvestigacion" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" onclick="btnInvestigacion_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1473882254_compose.png" alt="Causas" />
                                            <div class="caption">
                                                <h3>Registro de Causas</h3>
                                                <p>Detalle de las causas y responsables</p>
                                                <p>
                                                    <asp:UpdatePanel ID="uplCausas" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnCausas" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" onclick="btnCausas_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1473881872_Settings-5.png" alt="Acciones" />
                                            <div class="caption">
                                                <h3>Acciones Correctivas</h3>
                                                <p>Registro de acciones correctivas</p>
                                                <p>
                                                    <asp:UpdatePanel ID="uplAcciones" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAcciones" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" onclick="btnAcciones_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1473881879_Checklist.png" alt="Verificación" />
                                            <div class="caption">
                                                <h3>Verificación de Realización de Acciones</h3>
                                                <p>Verificar las acciones correctivas</p>
                                                <p>
                                                    <asp:UpdatePanel ID="uplVerificar" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnVerificar" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" onclick="btnVerificar_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1473882257_check.png" alt="Consulta" />
                                            <div class="caption">
                                                <h3>Verificación de Efectividad de Acciones</h3>
                                                <p>Conclusión de acciones</p>
                                                <p>
                                                    <asp:UpdatePanel ID="uplConcluir" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnConcluir" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" onclick="btnConcluir_Click" />    
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/1475810288_Report.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Reporte de queja </h3> 
                                                <p>Detalle completo de queja</p>
                                                <p>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnReporte" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" onclick="btnReporte_Click" />    
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-6">
                                        <div class="thumbnail">
                                            <img src="imagenes/credit-note.png" alt="Reporte" />
                                            <div class="caption">
                                                <h3>Notas de cr&eacute;dito</h3>                   
                                                <p>Asignaci&oacute;n de nota de credito</p>
                                                <p>
                                                    <asp:UpdatePanel ID="upnNotasCredito" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnNotaCredito" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" onclick="btnNotaCredito_Click" />    
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12">
                                        <div class="thumbnail">
                                            <img src="imagenes/soporte.png" alt="Reporte" width='130' height='130' />
                                            <div class="caption">
                                                <h3>Acta por Devoluci&oacute;n - Merma - Bonificaci&oacute;n</h3>                   
                                                <p>Adjuntar documento</p>
                                                <p>
                                                    <asp:UpdatePanel ID="upnSoporte" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnSoporte" runat="server" CssClass="btn btn-primary" 
                                                                Text="Entrar" OnClick="btnSoporte_Click" />    
                                                            <asp:Button ID="btnPdf" runat="server" CssClass="btn btn-primary" 
                                                                Text="Ver PDF" OnClick="btnPdf_Click" />
                                                            <asp:HiddenField ID="hflSoporte" runat="server" Value="" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="idmodal" tabindex="-1" role="dialog">
                    <div class="modal-dialog modal-sm" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">Concluir Queja</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel ID="upnMensajes" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblSuccess" CssClass="alert alert-success col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="Queja concluida satisfactoriamente" runat="server" Visible = "false" />
                                                        <asp:Label ID="lblWarning" CssClass="alert alert-danger col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="" 
                                                            runat="server" Visible = "False"/>
                                                        <asp:Label ID="lblError" CssClass="alert alert-warning col-sm-12" role="alert" Font-Bold="True" 
                                                            Text="No se cerró la queja, no se han concluido todas las acciones" runat="server" Visible = "false" />
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
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnGuardar" runat="server" Text="Concluir" 
                                    class="btn btn-primary" ValidationGroup="grupo1" OnClick="btnGuardar_Click" />
                                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar Ventana" class="btn btn-default" data-dismiss="modal"/>
                            </div>
                        </div>
                    </div>
                </div>

            </form>
        </div>
        
    </div>
</body>
</html>
