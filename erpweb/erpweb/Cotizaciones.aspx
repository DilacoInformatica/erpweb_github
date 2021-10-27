<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="erpweb.Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cotizaciones</title>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
            <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" href="#"><label>
                                <span data-feather="user-check"></span>
                                <asp:Label ID="lbl_conectado" runat="server"></asp:Label>
                                <span data-feather="message-circle"></span>
                                <asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
                            </label></a>
            <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <label class="text-light text-center fs-4 fw-bold">
                Cotizaciones generadas en Sitio Web
            </label>
            <div class="navbar-nav">
                <div class="nav-item text-nowrap">
                    <asp:LinkButton ID="Btn_volver" runat="server" CssClass="nav-link px-3" Width="133px" OnClick="LinkButton2_Click">Volver</asp:LinkButton>
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
                            <li class="nav-item">
                                <a class="nav-link" href="#">
                                    <span data-feather="bar-chart-2"></span>
                                    Reportes
                                </a>
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
                     <p class="divider"></p>
                     <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <p class="divider"></p>
                      <div class="container-fluid rounded border border-secondary bg-ligh">
                        <div class="row">
                            <h4><span class="badge bg-primary">Búsqueda de Información</span></h4>
                            <div class="col-md-2">
                                <span class="badge bg-info">Cotización</span>
                                <asp:TextBox ID="txt_cotizacion" runat="server" CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <span class="badge bg-info">Rut Cliente</span>
                                <asp:TextBox ID="txt_rut" runat="server" CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">Estado Cotización</span>
                                <asp:DropDownList ID="LstEstados" runat="server" CssClass="form-select" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" OnClick="Btn_buscar_Click" CssClass="btn btn-primary" />
                            </div>
                            <p class="divider"></p>
                        </div>
                    </div>
                   

                    <h6>
                        <asp:Label ID="lbl_cantidad" runat="server"></asp:Label></h6>

                    <asp:GridView ID="Lista_cotizacion" CssClass="table table-striped table-hover" runat="server" ShowFooter="True" OnSelectedIndexChanged="Lista_cotizacion_SelectedIndexChanged" AutoGenerateColumns="False" OnRowDataBound="Lista_cotizacion_RowDataBound" OnRowCommand="Lista_cotizacion_RowCommand">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="Ver" ImageUrl="~/img/vista.png" Text="Ver">
                            <ControlStyle Height="20px" Width="20px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="Rut" HeaderText="Rut" />
                            <asp:BoundField DataField="Dv" HeaderText="Dv" Visible="False" />
                            <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Id_cotizacion" HeaderText="Id" Visible="False" />
                            <asp:BoundField DataField="Cotizac_Num" HeaderText="Cotización" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="Pais" HeaderText="País" />
                            <asp:BoundField DataField="ERP" HeaderText="Cliente ERP" Visible="False"  />
                            <asp:TemplateField HeaderText="N° Cot ERP">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_num_cot_erp" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="estado" Visible="False" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" HeaderText="Cliente ERP">
                                <HeaderStyle CssClass="ColumnaOculta"></HeaderStyle>

                                <ItemStyle CssClass="ColumnaOculta"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_cot" HeaderText="Tipo" />
                            <asp:BoundField DataField="estado" HeaderText="estado" Visible="False" />
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Estado") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="img_estado" runat="server" CssClass="form-control" Height="40px" Width="40px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="badge bg-warning"></asp:Label>
                </main>
            </div>
        </div>
    </form>
    <script src="scripts/bootstrap.bundle.min.js"></script>
    <script src="scripts/feather.min.js"></script>
    <script src="scripts/Chart.min.js"></script>
    <script src="scripts/dashboard.js"></script>
</body>
</html>
