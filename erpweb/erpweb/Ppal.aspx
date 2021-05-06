<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ppal.aspx.cs" Inherits="erpweb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Menú Principal</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript">
    function salir() {
        if (confirm('Cerrar página?'))
        { window.close(); }
    }
</script>
</head>
<body>
    <header>
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <ul class="nav">
                    <li class="nav-item">
                        <h6><a class="nav-link active" href="Ppal.aspx">Inicio</a></h6>
                    </li>
                    <li class="nav-item">
                        <h6><a class="nav-link" href="Items_ppal.aspx">Productos</a></h6>
                    </li>
                    <li class="nav-item">
                        <h6><a class="nav-link" href="Adm_Clientes.aspx">Clientes</a></h6>
                    </li>
                    <li class="nav-item">
                        <h6><a class="nav-link" href="stock.aspx">Stock</a></h6>
                    </li>
                    <li class="nav-item">
                        <h6><a class="nav-link" href="Adm_Publicacion_Lineas_Prod.aspx">Líneas de Venta</a>
                    </li>
                    <li class="nav-item">
                        <h6><a class="nav-link" href="Notas_Venta.aspx">Notas de Venta</a></h6>
                    </li>
                    <li class="nav-item">
                        <h6><a class="nav-link" href="Cotizaciones.aspx">Cotizaciones</a></h6>
                    </li>
                    <li class="nav-item">
                        <h6><a class="nav-link" href="Precios_Esp_Adm.aspx">Precios Especiales</a></h6>
                    </li>
                    <li>
                        <h6><span class="nav-link"><a class="nav-link" href="#" onclick="return salir();">Salir</a></span></h6>
                    </li>
                    </ul>
               </div>
            </div>
        </div>
    </header>
    <main>
  
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">

                    <br />
                    <br />
                    <h1 class="text-center text-primary">Menú Administración Erp-Sitio Web</h1>
                    <div class="jumbotron">
                        <h2>Bienvenido!
                        </h2>
                        <p>
                            Estás en el ambiente de&nbsp;<asp:Label ID="lbl_ambiente" runat="server" Text=""></asp:Label>
                           <h4>Hola , <asp:Label ID="lbl_nombre" runat="server" Text=""></asp:Label>
&nbsp;estás en el menú principal del Administrador de Información de ERP y Sitio Web de Dilaco. Revisa tus accesos en la parte superior del Navegador, si no tienes acceso solicitalo con el área de Informática.</h4>
                        <h4>&nbsp;<p><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></p></h4>
                    </div>
                    <br />
                    <div class="container">
                                <div class="row">
                                    <div class="col-md-7 col-center">
                                        <asp:GridView ID="GrdProdSinStock" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="1088px" CaptionAlign="Top" AutoGenerateColumns="False" OnRowDataBound="GrdProdSinStock_RowDataBound">
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
</body>
</html>
