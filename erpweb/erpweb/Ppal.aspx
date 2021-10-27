<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ppal.aspx.cs" Inherits="erpweb.Maqueta" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <meta name="theme-color" content="#7952b3" />

    <title>Menu Principal</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
    <script type="text/javascript">
    function salir() {
        if (confirm('Cerrar página?'))
        { window.close(); }
        }
    </script>
</head>
<body>

    <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
        <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" href="#"> <label><span data-feather="user-check"></span><asp:Label ID="lbl_conectado" runat="server"></asp:Label>
                            <span data-feather="message-circle"></span><asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
                        </label></a>
        <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <input class="form-control form-control-dark w-100" type="text" placeholder="Search" aria-label="Search"/>
        <div class="navbar-nav">
            <div class="nav-item text-nowrap">
                <a class="nav-link px-3" href="#" onclick="return salir();">Cerrar</a>
            </div>
        </div>
    </header>

    <div class="container-fluid">
        <div class="row">
            <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
                <div class="position-sticky pt-3">
                    <ul class="nav flex-column">
                         <li class="nav-item">
                            <a class="nav-link active" href="Ppal.aspx">
                                <span data-feather="home"></span>
                                Dashboard
                            </a>
                        </li>
                         <li class="nav-item">
                            <a class="nav-link" href="Adm_Clientes.aspx">
                                <span data-feather="users"></span>
                                Clientes
                            </a>
                        </li>
                       
                         <li class="nav-item">
                            <a class="nav-link" href="Cotizaciones.aspx">
                                <span data-feather="file"></span>
                                Cotizaciones
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Notas_Venta.aspx">
                                <span data-feather="file"></span>
                                Notas de Venta
                            </a>
                        </li>
                        
                        <li class="nav-item">
                            <a class="nav-link" href="Items_ppal.aspx">
                                <span data-feather="shopping-cart"></span>
                                Productos
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="AdmStock.aspx">
                                <span data-feather="layers"></span>
                                Stock
                            </a>
                        </li>
                       <li class="nav-item">
                            <a class="nav-link" href="Precios_Esp_Adm.aspx">
                                <span data-feather="dollar-sign"></span>
                                Precios Especiales
                            </a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle text-primary" href="#" id="navbarDropdownRep" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                <span data-feather="bar-chart-2"></span>Reportes</a>
                             <ul class="dropdown-menu" aria-labelledby="navbarDropdownRep">
                                    <li><a class="dropdown-item" href="Inf_Cotizaciones.aspx"><span data-feather="file-text"></span>Cotizaciones</a></li>
                                    <li><a class="dropdown-item" href="Inf_NV.aspx"><span data-feather="file-text"></span>Notas de Venta</a></li>
                                    <li><a class="dropdown-item" href="Inf_Clientes.aspx"><span data-feather="file-text"></span>Clientes</a></li>
                                </ul>
                        </li>
                        <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle text-primary" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <span data-feather="tool"></span>Administración</a>
                                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <li><a class="dropdown-item" href="Adm_Publicacion_Lineas_Prod.aspx"><span data-feather="menu"></span>Categorías y Subcategorías</a></li>
                                    <li><a class="dropdown-item" href="Configuracion.aspx"><span data-feather="tool"></span>Parámetros</a></li>
                                    <li><a class="dropdown-item" href="ConfStock.aspx"><span data-feather="layers"></span>Niveles Stock</a></li>
                                </ul>
                            </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#" onclick="return salir();">
                                <span data-feather="log-out"></span>
                                Salir
                            </a>
                        </li>
                    </ul>
                </div>
            </nav>

            <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
                <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <p class="divider"></p>

                <asp:Chart ID="Grafico1" runat="server">
                    <Series>
                        <asp:Series Name="Series" ChartType="Line" Legend="Legend1"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea"></asp:ChartArea>
                    </ChartAreas>
                    <Titles>
                        <asp:Title Name="Title1" Text="Cotizaciones durante la semana">
                        </asp:Title>
                    </Titles>
                </asp:Chart>

                <asp:Chart ID="Grafico2" runat="server">
                    <Series>
                        <asp:Series Name="Series" Legend="Legend1"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea"></asp:ChartArea>
                    </ChartAreas>
                    <Titles>
                        <asp:Title Name="Title1" Text="Productos más Cotizados">
                        </asp:Title>
                    </Titles>
                </asp:Chart>

                <h2>Ultimos Ingresos</h2>
                <form id="form1" runat="server">
                    <asp:GridView ID="Lst_Movimientos" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
                </form>
                
            </main>
        </div>
    </div>
    <script src="scripts/bootstrap.bundle.min.js"></script>
    <script src="scripts/feather.min.js"></script>
    <script src="scripts/Chart.min.js"></script>
    <script src="scripts/dashboard.js"></script>
</body>
</html>
