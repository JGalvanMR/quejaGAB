<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="grafica_rechazo.aspx.cs" Inherits="queja.grafica_rechazo" %>

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

    <script src="Scripts/tableExport.js" type="text/javascript"></script>
    <script src="Scripts/jquery.base64.js" type="text/javascript"></script>

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>

    <script src="Scripts/jquery.tabletojson.js" type="text/javascript"></script>

    

    <script>

        var anterior = "";
        var actual = "";
        var siguiente = "";

        var anterior_prod = "";
        var actual_prod = "";
        var siguiente_prod = "";

        var productos = "";

        var prods_ant = "";
        var prods_act = "";
        var prods_sig = "";

        $(function () {


            anterior = $("#<%= txtAnterior.ClientID %>").val();
            actual = $("#<%= txtActual.ClientID %>").val();
            siguiente = $("#<%= txtSiguiente.ClientID %>").val();

            //alert(anterior);

            anterior_prod = $("#<%= txtAnteriorProd.ClientID %>").val();
            actual_prod = $("#<%= txtActualProd.ClientID %>").val();
            siguiente_prod = $("#<%= txtSiguienteProd.ClientID %>").val();

            //alert(anterior_prod);

            var anterior_rech = $("#<%= txtAnteriorRech.ClientID %>").val();
            var actual_rech = $("#<%= txtActualRech.ClientID %>").val();
            var siguiente_rech = $("#<%= txtSiguienteRech.ClientID %>").val();

            var anterior_reci = $("#<%= txtAnteriorReci.ClientID %>").val();
            var actual_reci = $("#<%= txtActualReci.ClientID %>").val();
            var siguiente_reci = $("#<%= txtSiguienteReci.ClientID %>").val();

            productos = $("#<%= txtBarNames.ClientID %>").val();
            prods_ant = $("#<%= txtBarAnterior.ClientID %>").val();
            prods_act = $("#<%= txtBarActual.ClientID %>").val();
            prods_sig = $("#<%= txtBarSiguiente.ClientID %>").val();

//            prods_vfant = $("#= txtBarVentasAct.ClientID %>").val();
//            prods_vfact = $("#= txtBarVentasAnt.ClientID %>").val();
//            prods_vfsig = $("#= txtBarVentasSig.ClientID %>").val();
            

            var meses = [anterior, actual, siguiente]; 

            var datos_prod =  "[" + anterior_prod + ", " + actual_prod + ", " + siguiente_prod + "]";
            var datos_rech =  "[" + anterior_rech + ", " + actual_rech + ", " + siguiente_rech + "]";
            var datos_reci =  "[" + anterior_reci + ", " + actual_reci + ", " + siguiente_reci + "]";

            var prods = productos.toString()
            var prodss = prods.split(',');
            var BarProductos = [];
            for (var i = 0; i < prodss.length; i++) {
                BarProductos.push([prodss[i]]);
            }
            //var BarProductos = "[" + productos + "]";
            //alert(BarProductos.length);

            //var prods_todos = ["{ name: '" + anterior + "', data: [" + prods_ant + "]}, { name: '" + actual + "', data: [" + prods_act + "]},{ name: '" + siguiente + "', data: [" + prods_sig + "]}"]; 

            //alert(prods_todos);
            /*[{
                    name: 'John',
                    data: [5, 3, 4, 7, 2, 10, 34, 12]
                }, {
                    name: 'Jane',
                    data: [2, 2, 3, 2, 1, 15, 23, 32]
                }, {
                    name: 'Joe',
                    data: [3, 4, 4, 2, 5, 2, 5, 7]
                }]*/

            /*var myserie_prod = [];
            for (var i = 0; i < datos_prod.length; i++) {
                myserie_prod.push([datos_prod[i]]);
                i++
            }*/
            //alert(myserie_prod);
            //alert(datos_rech);

            Highcharts.chart('container', {

                chart: {
                    type: 'column'
                },

                title: {
                    text: 'Total de Cajas Producidas en Planta y Campo'
                },

                xAxis: {
                    categories: meses
                },

                yAxis: {
                    allowDecimals: false,
                    min: 0,
                    title: {
                        text: 'Numero de cajas'
                    }
                },

                tooltip: {
                    formatter: function () {
                        
                        var n = this.y.toString();
                        var thousands = n.split('').reverse().join('').match(/.{1,3}/g).join(',');
                        var answer = thousands.split('').reverse().join('');

                        return '<b>' + this.x + '</b><br/>' +
                            this.series.name + ': ' + answer;
                    }
                },

                plotOptions: {
                    column: {
                        stacking: 'normal'
                    }
                },
                colors:[
                    '#00ff00'
                ],
                series: [{
                    name: 'Producidas',
                    data: JSON.parse(datos_prod)
                }/*, {
                    name: 'Rechazadas',
                    data: JSON.parse(datos_rech)
                }*/]
            });

            Highcharts.chart('container2', {

                chart: {
                    type: 'column'
                },

                title: {
                    text: 'Total de Cajas Recibidas y Rechazadas por Quejas'
                },

                xAxis: {
                    categories: meses
                },

                yAxis: {
                    allowDecimals: false,
                    min: 0,
                    title: {
                        text: 'Numero de cajas'
                    }
                },

                tooltip: {
                    formatter: function () {
                        var regreso = porcentaje_rechazo(this.x, this.y);
                        var valores = regreso.split("-");

                        var n = this.y.toString();
                        var thousands = n.split('').reverse().join('').match(/.{1,3}/g).join(',');
                        var answer = thousands.split('').reverse().join('');

                        //alert(regreso);
                        return '<b>' + this.x + '</b><br/>Producidas: ' + valores[1] + '<br/>' + this.series.name + ': ' + answer + '<br/>Porcentaje: ' + valores[0];
                    }
                },

                plotOptions: {
                    column: {
                        stacking: 'normal'
                    }
                },

                series: [{
                    name: 'Recibidas',
                    data: JSON.parse(datos_reci)
                }, {
                    name: 'Rechazadas',
                    data: JSON.parse(datos_rech)
                }]
            });

//            var facturadas_sig = JSON.parse("[" + prods_vfsig + "]");
//            var facturadas_act = JSON.parse("[" + prods_vfact + "]");
//            var facturadas_ant = JSON.parse("[" + prods_vfant + "]");

            Highcharts.chart('container3', {
                chart: {
                    type: 'bar',
                    height: (9 / 16 * 300) + '%'
                },
                title: {
                    text: 'Cajas Rechazadas por Mes'
                },
                xAxis: {
                    categories: BarProductos
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Cajas Rechazadas'
                    }
                },
                legend: {
                    reversed: true
                },
                plotOptions: {
                    series: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                tooltip:{
                    /*formatter: function () {
                        var ms = this.series[0].name;
                        console.log(ms);
                        //var array1 = facturadas;
                        //console.log(array1);
                        //var found = array1.indexOf(10);
                        //console.log(found);
                        //return found;
                        
                        return 'The value for <b>' + this.x +
                            '</b> is <b>' + this.y + '</b>';
                    }*/
                    pointFormatter: function() {
                        
                        if(this.series.name == siguiente)
                        {
                            //prod_siguiente
//                            var array1 = prods_sig;
//                            console.log(array1);
//                            var found1 = array1.indexOf(this.y);
//                            console.log(found1);
//                            var array1_a = facturadas_sig;
//                            console.log(array1_a);
//                            var fac = 0;
//                            for(i = 0; i < array1_a.length; i++)
//                            {
//                                if(i == found1)
//                                {
//                                    fac = array1_a[i];
//                                }
//                            }
//                            console.log(fac);
                            var cat = this.category;
                            var buscar = "'";
                            var cat2 = cat.toString().replace(new RegExp(buscar, "g"), "*");
                            console.log(cat2);
                            var cualquiera = "";
                            var pageUrl = "graphics.aspx/facturadas_siguiente";
                            $.ajax({
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                url: pageUrl,
                                data: "{ nombre: '" + cat2 + "', mes: '" + this.series.name + "', rech: '" + this.y + "'}", //
                                dataType: "json",
                                async: false,
                                success: function (data) {
                                    $("#<%= txtFacturadas.ClientID %>").val(data.d);
                                    cualquiera = data.d;
                                    console.log(cualquiera);
                                },
                                error: function (data, success, error) {
                                    alert("Error: " + error);
                                }

                                
                            });

                            return '<span style="color:{point.color}">\u25CF</span> '
                                + this.series.name + ': <b>' + $("#<%= txtFacturadas.ClientID %>").val() + '</b><br/>';//this.y this.category    
                        }
                        else if(this.series.name == actual)
                        {
                            var cat = this.category;
                            var buscar = "'";
                            var cat2 = cat.toString().replace(new RegExp(buscar, "g"), "*");
                            console.log(cat2);
                            var cualquiera = "";
                            var pageUrl = "graphics.aspx/facturadas_actual";
                            $.ajax({
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                url: pageUrl,
                                data: "{ nombre: '" + cat2 + "', mes: '" + this.series.name + "', rech: '" + this.y + "'}", //
                                dataType: "json",
                                async: false,
                                success: function (data) {
                                    $("#<%= txtFacturadas.ClientID %>").val(data.d);
                                    cualquiera = data.d;
                                    console.log(cualquiera);
                                },
                                error: function (data, success, error) {
                                    alert("Error: " + error);
                                }

                                
                            });

                            return '<span style="color:{point.color}">\u25CF</span> '
                                + this.series.name + ': <b>' + $("#<%= txtFacturadas.ClientID %>").val() + '</b><br/>';//this.y this.category 

//                            //prod_actual
//                            var array2 = prods_act;
//                            var found2 = array2.indexOf(this.y);
//                            console.log(found2);
//                            var array2_a = facturadas_act;
//                            console.log(array2_a);
//                            var fac1 = 0;
//                            for(i = 0; i < array2_a.length; i++)
//                            {
//                                if(i == found2)
//                                {
//                                    fac1 = array2_a[i];
//                                }
//                            }
//                            console.log(fac1);

//                            return '<span style="color:{point.color}">\u25CF</span> '
//                                + this.series.name + ': <b>' + this.y + ' entra2' + fac1 + '</b><br/>';
                        }
                        else if(this.series.name == anterior)
                        {
                            var cat = this.category;
                            var buscar = "'";
                            var cat2 = cat.toString().replace(new RegExp(buscar, "g"), "*");
                            console.log(cat2);
                            var cualquiera = "";
                            var pageUrl = "graphics.aspx/facturadas_anterior";
                            $.ajax({
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                url: pageUrl,
                                data: "{ nombre: '" + cat2 + "', mes: '" + this.series.name + "', rech: '" + this.y + "'}", //
                                dataType: "json",
                                async: false,
                                success: function (data) {
                                    $("#<%= txtFacturadas.ClientID %>").val(data.d);
                                    cualquiera = data.d;
                                    console.log(cualquiera);
                                },
                                error: function (data, success, error) {
                                    alert("Error: " + error);
                                }
                            });

                            return '<span style="color:{point.color}">\u25CF</span> '
                                + this.series.name + ': <b>' + $("#<%= txtFacturadas.ClientID %>").val() + '</b><br/>';//this.y this.category 
//                            //prod_anterior
//                            var array3 = prods_ant;
//                            var found3 = array3.indexOf(this.y);
//                            console.log(found3);
//                            var array3_a = facturadas_ant;
//                            console.log(array3_a);
//                            var fac2 = 0;
//                            for(i = 0; i < array3_a.length; i++)
//                            {
//                                if(i == found3)
//                                {
//                                    fac2 = array3_a[i];
//                                }
//                            }
//                            console.log(fac2);
//                            return '<span style="color:{point.color}">\u25CF</span> '
//                                + this.series.name + ': <b>' + this.y + ' entra3' + fac2 + '</b><br/>';
                        }
                        /*var seriesNameConverter = {
                            'Abril': 'Series One Name',
                            'Mayo': 'Series Two Name',
                            'Junio': 'Series Three Name'
                        };seriesNameConverter[]*/

//                        return '<span style="color:{point.color}">\u25CF</span> '
//                                    + this.series.name + ': <b> ' + this.category + ' entra' + $("#<%= txtFacturadas.ClientID %>").val() + '</b><br/>';//this.y 
                         
                    }
                    
                    //headerFormat: '<b>{series.name}</b><br>',
                    //pointFormat: '{point.x} cm, {point.y} kg',
                },

                series: [{
                    name: siguiente,
                    data: JSON.parse("[" + prods_sig + "]")
                }, {
                    name: actual,
                    data: JSON.parse("[" + prods_act + "]")
                }, {
                    name: anterior,
                    data: JSON.parse("[" + prods_ant + "]")
                }]

                
            });


        });

//        function onSuccess(data) {
//            //alert(val1 + ' ' + val2);
//            $("#<%= txtFacturadas.ClientID %>").val(data.d);
//            //return '<span style="color:{point.color}">\u25CF</span> '
//                                    //+ val1 + ': <b>' + val2 + ' entra' + $("#<%= txtFacturadas.ClientID %>").val() + '</b><br/>';//fac
//        }

        function porcentaje_rechazo(produ, recha)
        {
            var produccion = 0;
            if(anterior == produ)
            {
                produccion = anterior_prod;
            }
            if(actual == produ)
            {
                produccion = actual_prod;
            }
            if(siguiente == produ) 
            {
                produccion = siguiente_prod;
            }

            var n = produccion.toString();
            var thousands = n.split('').reverse().join('').match(/.{1,3}/g).join(',');
            var answer = thousands.split('').reverse().join('');

            var porcentaje = ((recha * 100) / produccion).toFixed(2) + "%-" + answer;

            return porcentaje;
        }

        function exportarexcel() {
            $('#datatable1').tableExport({ type: 'excel', escape: 'false' });
        }

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
    <style>
        body
        {
            /*background-image: url(imagenes/fondo_7.png);*/
            background-color: #FFFFFF;
            background-position: center center;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: cover;
            /*background-color: #464646;*/
            font-size: 12px;
            }
    </style>
</head>
<body>
    <div class="container col-lg-12">
        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" >
                </asp:ScriptManager>
                <div  class="panel panel-primary">
                    <div class="panel panel-heading">
                        <center><h3><strong>Cajas Rechazadas</strong></h3></center>
                    </div>
                    <div class="panel panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label3" runat="server" Text="Clave: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                <asp:Label ID="lblClave" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text="Nombre: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                <asp:Label ID="lblNombre" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="Cedis: " CssClass="cols-sm-3 label label-primary"></asp:Label>
                                <asp:Label ID="lblCedis" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                                <asp:Label ID="lblAdmin" runat="server" Text="" CssClass="cols-sm-3 label label-success"></asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-offset-0 col-sm-8">
                                    <asp:Button runat="server" ID="btnVolver" Text="Regresar a reportes" 
                                        CssClass="btn btn-primary" onclick="btnVolver_Click"   />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group">
                                <label for="ddlMes" class="col-sm-2 control-label text-right">Mes</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control" 
                                        AutoPostBack="false" onselectedindexchanged="ddlMes_SelectedIndexChanged" >
                                            <asp:ListItem Value="">SELECCIONE MES...</asp:ListItem>
                                            <asp:ListItem Value="1">ENERO</asp:ListItem>
                                            <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                                            <asp:ListItem Value="3">MARZO</asp:ListItem>
                                            <asp:ListItem Value="4">ABRIL</asp:ListItem>
                                            <asp:ListItem Value="5">MAYO</asp:ListItem>
                                            <asp:ListItem Value="6">JUNIO</asp:ListItem>
                                            <asp:ListItem Value="7">JULIO</asp:ListItem>
                                            <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                                            <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                                            <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                                            <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                                            <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:UpdatePanel ID="udpMeses" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="txtAnterior" runat="server" Value="" /> 
                                        <asp:HiddenField ID="txtActual" runat="server" Value="" />
                                        <asp:HiddenField ID="txtSiguiente" runat="server" Value="" />
                                        <asp:HiddenField ID="txtAnteriorProd" runat="server" Value="" />
                                        <asp:HiddenField ID="txtActualProd" runat="server" Value="" />
                                        <asp:HiddenField ID="txtSiguienteProd" runat="server" Value="" />
                                        <asp:HiddenField ID="txtAnteriorRech" runat="server" Value="" />
                                        <asp:HiddenField ID="txtActualRech" runat="server" Value="" />
                                        <asp:HiddenField ID="txtSiguienteRech" runat="server" Value="" />
                                        <asp:HiddenField ID="txtAnteriorReci" runat="server" Value="" />
                                        <asp:HiddenField ID="txtActualReci" runat="server" Value="" />
                                        <asp:HiddenField ID="txtSiguienteReci" runat="server" Value="" />
                                        <asp:HiddenField ID="txtBarNames" runat="server" Value="" />
                                        <asp:HiddenField ID="txtBarAnterior" runat="server" Value="" />
                                        <asp:HiddenField ID="txtBarActual" runat="server" Value="" />
                                        <asp:HiddenField ID="txtBarSiguiente" runat="server" Value="" />
                                        <asp:HiddenField ID="txtFacturadas" runat="server" Value="" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <%--<br />--%>
                        <div class="row">
                            <div class="form-group">
                                <%--<label for="ddlCliente" class="col-sm-2 control-label text-right">Cliente</label>--%>
                                <div class="col-sm-8">
                                    <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" 
                                            AutoPostBack="true" 
                                        onselectedindexchanged="ddlCliente_SelectedIndexChanged" Visible="false" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%--<br />--%>
                        <div class="row">
                            <div class="form-group">
                                <%--<label for="ddlSucursales" class="col-sm-2 control-label text-right">Sucursal</label>--%>
                                <div class="col-sm-8">
                                    <asp:UpdatePanel ID="upnSucursales" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlSucursales" runat="server" CssClass="form-control"  Visible="false">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:TextBox runat="server" ID="txtSucursal" Visible="false" CssClass="form-control" Style="text-transform: uppercase"  placeholder="Sucursal" onkeydown = "return (event.keyCode!=13)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <%--<br />--%>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <center>
                                        <asp:Button runat="server" ID="btnBuscaOrden" Text="Buscar" 
                                            CssClass="btn btn-primary" onclick="btnBuscaOrden_Click" />
                                    </center>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <center>    
                                <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                <div id="container2" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                <div id="container3" ></div>
                            </center>
                        </div>
                    </div>
                    <!--input type="button" id="convert-table" value="Convertir" /-->
                </div>
            </form>
        </div>
    </div>
</body>
</html>
