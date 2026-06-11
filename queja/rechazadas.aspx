<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rechazadas.aspx.cs" Inherits="queja.rechazadas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="alertifyjs/css/alertify.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.0.js" type="text/javascript"></script>
    <script src="alertifyjs/alertify.js" type="text/javascript"></script>
    <script src="Scripts/sweetalert.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
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
        .nover
        {
            display:none;
            }
        

    </style>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Modificar Cajas Rechazadas</strong></h3></center>
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
                                            <asp:Label ID="lblQueja" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                            <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-8">
                                            <asp:Button runat="server" ID="btnVolver" Text="Regresar a queja" 
                                                CssClass="btn btn-primary" onclick="btnVolver_Click"  />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="uplDetalle" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="grvDetalle" runat="server" AutoGenerateColumns="false" 
                                                    Width="100%" CssClass="table table-bordered table-hover" 
                                                    EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                                    EmptyDataRowStyle-HorizontalAlign="Center" >
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Conse" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="conse"/>
                                                        <asp:BoundField HeaderText="ProdCve" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="id_producto"/>
                                                        <asp:BoundField HeaderText="Producto" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="nom_producto"/>
                                                        <asp:BoundField HeaderText="Ord Prod" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_ordprod"/>
                                                        <asp:BoundField HeaderText="Tarima" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_tarima"/>
                                                        <asp:BoundField HeaderText="CveProv" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="prov_clave"/>
                                                        <asp:BoundField HeaderText="Proveedor" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="prov_nombre"/>
                                                        <asp:BoundField HeaderText="CveRch" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="rch_clave"/>
                                                        <asp:BoundField HeaderText="CveTbl" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataField="tbl_clave"/>
                                                        <asp:BoundField HeaderText="Area" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_area"/>
                                                        <asp:BoundField HeaderText="Recibido" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantreci"/>
                                                        <asp:BoundField HeaderText="Rechazado" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cantrecha"/>
                                                        <asp:BoundField HeaderText="Producidas" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="qud_cjsprod"/>
                                                        <asp:TemplateField HeaderText="Rechazadas" HeaderStyle-CssClass="visible-lg visible-md visible-sm hiddvisibleen-xs" ItemStyle-CssClass="visible-lg visible-md visible-xs" HeaderStyle-Width="100" ItemStyle-Width="100">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRechazadas2" runat="server" Width="100" Text="0" style="text-align: right" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-8">
                                            <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                CssClass="btn btn-primary" onclick="btnGuardar_Click"  />
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
