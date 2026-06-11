<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="descargar.aspx.cs" Inherits="queja.descargar" %>

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
        <div class="col-md-10 col-md-offset-1">
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                </asp:ScriptManager>
                <div>
                    <div class="row">
                        <div class="col-sm-6 col-md-4">
                            <div class="thumbnail">
                                <img src="imagenes/1473881847_magnifier.png" alt="Consulta"/>
                                <div class="caption">
                                    <h3>Carpeta 1</h3>
                                    <p><asp:Button ID="btnConsulta" runat="server" CssClass="btn btn-primary" 
                                            Text="Entrar" /></p>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-4">
                            <div class="thumbnail">
                                <img src="imagenes/1473882341_profle.png" alt="Investigación" />
                                <div class="caption">
                                    <h3>Carpeta 2</h3>
                                    <p>
                                        <asp:Button ID="btnInvestigacion" runat="server" CssClass="btn btn-primary" 
                                            Text="Entrar" />
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-4">
                            <div class="thumbnail">
                                <img src="imagenes/1473882254_compose.png" alt="Causas" />
                                <div class="caption">
                                    <h3>Carpeta 3</h3>
                                    <p>
                                        <asp:Button ID="btnCausas" runat="server" CssClass="btn btn-primary" 
                                            Text="Entrar" />  
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <div class="form-group">
                                <asp:UpdatePanel ID="upnOrden" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvwOrden" runat="server" AutoGenerateColumns="false" 
                                            Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                            EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="true" 
                                            onrowcommand="gvwOrden_RowCommand" >
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Archivo" HeaderStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" DataField="archivo"/>
                                                <asp:ButtonField HeaderText="Descargar" Text="Descargar" ItemStyle-CssClass="form-control btn btn-success" ItemStyle-ForeColor="White" CommandName="descargar" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </center>
                    </div>
                </div>
                <div id="desc" runat="server"></div>
            </form>
        </div>
    </div>
    
</body>
</html>

