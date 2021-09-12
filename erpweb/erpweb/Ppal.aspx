<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ppal.aspx.cs" Inherits="erpweb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Menú Principal</title>
    <link href="Content/bootstrap-grid.min.css" rel="stylesheet" />


   
   
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    
    <script type="text/javascript">
    function salir() {
        if (confirm('Cerrar página?'))
        { window.close(); }
    }
</script>
</head>
<body>
    <header>
        <div class="container-fluit ">
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <div class="container-fluid">
                    <a class="navbar-brand" href="#">DILACO</a>
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                            <li class="nav-item">
                                <a class="nav-link text-primary" href="Ppal.aspx">Inicio</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-primary" href="Adm_Clientes.aspx">Clientes</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-primary" href="Cotizaciones.aspx">Cotizaciones</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-primary" href="Notas_Venta.aspx">Notas de Venta</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-primary" href="Items_ppal.aspx">Productos</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-primary" href="AdmStock.aspx">Stock</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-primary" href="Precios_Esp_Adm.aspx">Precios Especiales</a>
                            </li>

                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle text-primary" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Administración
                                </a>
                                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <li><a class="dropdown-item" href="Adm_Publicacion_Lineas_Prod.aspx">Categorías y Subcategorías</a></li>
                                    <li><a class="dropdown-item" href="Configuracion.aspx">Parámetros</a></li>
                                    <li><a class="dropdown-item" href="ConfStock.aspx">Niveles Stock</a></li>
                                </ul>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link text-danger" href="#" onclick="return salir();">Salir</a>
                            </li>
                        </ul>

                    </div>
                </div>
            </nav>
        </div>
    </header>
    <main>
  
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">

                    <br />
                    <br />
                    <h1 class="text-center text-primary">Menú Administración ERP-Sitio Web</h1>
                     <div class="p-5 mb-4 bg-light rounded-3">
                      <div class="container-fluid py-5">
                        <h1 class="display-5 fw-bold">Bienvenido!</h1>
                        <p class="col-md-8 fs-4"> Estás en el ambiente de&nbsp;<asp:Label ID="lbl_ambiente" runat="server" Text=""></asp:Label>
                             <h4>Hola , <asp:Label ID="lbl_nombre" runat="server" Text=""></asp:Label>
&nbsp;estás en el menú principal del Administrador de Información de ERP y Sitio Web de Dilaco. Revisa tus accesos en la parte superior del Navegador, si no tienes acceso solicitalo con el área de Informática.</h4>
                        <h4>&nbsp;<p><asp:Label ID="Label3" runat="server" CssClass="badge bg-danger"></asp:Label></h4></p>
                          <p><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></p>
                      </div>
                    </div>
                    <br />
                    <div class="container">
                                <div class="row">
                                    <div class="col-md-6 col-center">
                                        <asp:GridView ID="GrdProdSinStock" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="543px" CaptionAlign="Top" AutoGenerateColumns="False" OnRowDataBound="GrdProdSinStock_RowDataBound" Caption="Productos sin Stock">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                                <asp:BoundField DataField="stock" HeaderText="Stock" />
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-6 col-center">
                                        <asp:GridView ID="GrdProdSinFVPE" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="540px" CaptionAlign="Top" AutoGenerateColumns="False" OnRowDataBound="GrdProdSinFVPE_RowDataBound" Caption="Productos Precio Esp. Fecha vencimiento no vigente">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                                <asp:BoundField DataField="fecha_vigencia" HeaderText="Fecha Vigencia" />
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>
                                    
                                </div>
                            </div>
                </div>
            </div>
        </div>

    </form>
    </main>
    <footer>
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <span class="label label-primary">Departamento Informática</span>
                </div>
            </div>
        </div>

     </footer>
     <script src="scripts/popper.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
</body>
</html>
