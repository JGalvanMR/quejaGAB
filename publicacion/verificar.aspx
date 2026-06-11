<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verificar.aspx.cs" Inherits="queja.verificar" %>

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
    <div class="container">
        <div class="col-md-10 col-md-offset-1">
            <form id="form1" runat="server" class="form-horizontal">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                <div class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Verificar Acci&oacute;n</strong></h3></center>
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
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group">
                                        <label for="txtClave" class="col-sm-2 control-label">Clave</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtClave" MaxLength="300" runat="server" 
                                                CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label for="txtResponsable" class="col-sm-2 control-label">Responsable</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtResponsable" MaxLength="300" runat="server" 
                                                CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label for="txtAccion" class="col-sm-2 control-label">Acción</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtAccion" MaxLength="150" TextMode="MultiLine" runat="server" 
                                                CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>    
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label for="txteFecha" class="col-sm-2 control-label">Termino</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtFecha" MaxLength="150" runat="server" 
                                                CssClass="form-control" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>    
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label for="ddlCumplimiento" class="col-sm-2 control-label">Cumplimiento</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" ID="ddlCumplimiento" CssClass="form-control">
                                                <asp:ListItem Value="0">ELEGIR OPCIÓN...</asp:ListItem>
                                                <asp:ListItem Value="SI">SI</asp:ListItem>
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                                <asp:ListItem Value="PARCIAL">PARCIAL</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRecibio" runat="server" ErrorMessage="Debe elegir una opción" ControlToValidate="ddlCumplimiento" ForeColor="Red" InitialValue="0" ValidationGroup="grupo1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label for="txtComentario" class="col-sm-2 control-label">Comentario</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtComentario" MaxLength="100" TextMode="MultiLine" runat="server" 
                                                CssClass="form-control tamano" Rows="5" Style="text-transform: uppercase" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCausa" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtComentario" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="valInput" ControlToValidate="txtComentario" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="Máximo 100 caracteres" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RegularExpressionValidator>   
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <center>
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
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <center>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <asp:Button runat="server" ID="btnGuardar" Text="Guardar" 
                                                    CssClass="btn btn-primary" ValidationGroup="grupo1" 
                                                    onclick="btnGuardar_Click"  />
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>  
        </div>
    </div>
</body>
</html>
