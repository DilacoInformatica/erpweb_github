<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ppal.aspx.cs" Inherits="erpweb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Menú Principal</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <ul class="nav">
                        <li class="nav-item">
                            <h4><a class="nav-link active" href="Ppal.aspx">Inicio</a></h4>
                        </li>
                        <li class="nav-item">
                            <h4><a class="nav-link" href="Items_ppal.aspx">Productos</a></h4>
                        </li>
                        <li class="nav-item">
                            <h4><a class="nav-link" href="Items_ppal.aspx">Clientes</a></h4>
                        </li>
                        <li class="nav-item">
                            <h4><a class="nav-link" href="stock.aspx">Existencias</a></h4>
                        </li>
                        <li class="nav-item">
                            <h4><a class="nav-link" href="Adm_Publicacion_Lineas_Prod.aspx">Líneas de Venta</a>
                        </li>
                        <li class="nav-item">
                            <h4><a class="nav-link" href="Notas_Venta.aspx">Notas de Venta</a></h4>
                        </li>
                        <li class="nav-item">
                            <h4><a class="nav-link" href="Cotizaciones.aspx">Cotizaciones</a></h4>
                        </li>
                        <li>
                            <h4><span class="nav-link"><asp:LinkButton ID="Btn_Salir" CssClass="btn btn-outline-success" runat="server">Salir</asp:LinkButton></span></h4>
                        </li>
                    </ul>
                    <br />
                    <br />
                    <h1 class="text-center text-primary">Menú Administración Erp-Sitio Web</h1>
                    <div class="jumbotron">
                        <h2>Bienvenido!
                        </h2>
                        <p>
                            Estás en el ambiente de&nbsp;<asp:Label ID="lbl_ambiente" runat="server" Text=""></asp:Label>
                           <h4>Hola , 
                               <asp:Label ID="lbl_nombre" runat="server" Text=""></asp:Label>
&nbsp;estás en el menú principal del Administrador de Información de ERP y Sitio Web de Dilaco. Revisa tus accesos en la parte superior del Navegador, si no tienes acceso solicitalo con el área de Informática.</h4></p>
                        
                        <h4><p><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></p></h4>
                    </div>
                    <p>Departamento de TI</p>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
<script>
    function salir() {
        if (confirm('Cerrar página?'))
        { window.close(); }
    }
</script>