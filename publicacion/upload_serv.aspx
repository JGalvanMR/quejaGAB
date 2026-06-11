    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload_serv.aspx.cs" Inherits="queja.upload_serv" %>

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
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <center><h3><strong class="">Subir Imagenes</strong></h3></center>
                    </div>
                    <div class="panel-body">
                        <form id="Form2" runat="server" >
                            
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
                                    <br />
                                    <div class="form-group">
                                        <div class="cols-sm-offset-2 col-sm-12">
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="fileinput-preview thumbnail" data-trigger="fileinput" style="width: 200px; height: 150px;">
                                                </div>
                                                <div>
                                                    <span class="btn btn-primary btn-file">
                                                        <span class="fileinput-new">Seleccionar</span>
                                                        <span class="fileinput-exists">Cambiar</span>
                                                        <asp:FileUpload ID="img1"  runat="server"/>
                                                    </span>
                                                    <a href="#" class="btn btn-danger fileinput-exists" data-dismiss="fileinput">Quitar</a>
                                                </div>
                                            </div>
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="fileinput-preview thumbnail" data-trigger="fileinput" style="width: 200px; height: 150px;">
                                                </div>
                                                <div>
                                                    <span class="btn btn-primary btn-file">
                                                        <span class="fileinput-new">Seleccionar</span>
                                                        <span class="fileinput-exists">Cambiar</span>
                                                        <asp:FileUpload ID="img2"  runat="server"/>
                                                    </span>
                                                    <a href="#" class="btn btn-danger fileinput-exists" data-dismiss="fileinput">Quitar</a>
                                                </div>
                                            </div>
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="fileinput-preview thumbnail" data-trigger="fileinput" style="width: 200px; height: 150px;"></div>
                                                <div>
                                                <span class="btn btn-primary btn-file">
                                                    <span class="fileinput-new">Seleccionar</span>
                                                    <span class="fileinput-exists">Cambiar</span>
                                                    <asp:FileUpload ID="img3"  runat="server"/>
                                                </span>
                                                <a href="#" class="btn btn-danger fileinput-exists" data-dismiss="fileinput">Quitar</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12 col-xs-12">
                                            <asp:Button ID="btnSubir" runat="server" Text="Subir imagenes" 
                                                CssClass="btn btn-primary col-sm-12 col-xs-12" ValidationGroup="grupo1" onclick="btnSubir_Click" />
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
