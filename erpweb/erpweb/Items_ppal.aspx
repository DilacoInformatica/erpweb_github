<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Items_ppal.aspx.cs" Inherits="erpweb.Ppal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Productos</title>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
</head>
<body>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <form id="form1" runat="server">
                <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
                    <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" href="#">
                        <label>
                            <span data-feather="user-check"></span>
                            <asp:Label ID="lbl_conectado" runat="server"></asp:Label>
                            <span data-feather="message-circle"></span>
                            <asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
                        </label>
                    </a>
                    <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <label class="text-light text-center fs-4 fw-bold">
                        Productos Web
                    </label>
                    <div class="navbar-nav">
                        <div class="nav-item text-nowrap">
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="nav-link px-3" OnClick="LinkButton2_Click1">Volver</asp:LinkButton>
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
                                        <a class="nav-link" href="Ppal.aspx">
                                            <span data-feather="log-out"></span>
                                            Volver
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
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <p class="divider"></p>
                            <div class="container-fluid rounded border border-secondary bg-light">
                                <h4><span class="badge bg-primary">Búsqueda de Información</span></h4>
                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Por palabra clave</span>
                                        <asp:TextBox ID="txt_palabra_clave" runat="server" BackColor="#FFFFCC" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Código</span>
                                        <asp:TextBox ID="txt_codigo" runat="server" BackColor="#FFFFCC" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Código Prov.</span>
                                        <asp:TextBox ID="txt_codprov" runat="server" BackColor="#FFFFCC" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Letra</span>
                                        <asp:DropDownList ID="LstLetras" runat="server" AppendDataBoundItems="True" CssClass="form-select">
                                            <asp:ListItem Selected="True" Value="0">Letra</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="badge bg-info">División</span>
                                        <asp:DropDownList ID="LstDivision" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivision_SelectedIndexChanged" CssClass="form-select">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Categoría</span>
                                        <asp:DropDownList ID="LstCategorias" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged" CssClass="form-select">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Subcategoría</span>
                                        <asp:DropDownList ID="LstSubCategorias" runat="server" AppendDataBoundItems="True" CssClass="form-select">
                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Línea de Venta</span>
                                        <asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True" CssClass="form-select">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <span class="badge bg-info">Proveedor</span>
                                        <asp:DropDownList ID="LstProveedores" runat="server" AppendDataBoundItems="True" CssClass="form-select">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <span class="badge bg-info">Sin Categoría</span>
                                        <asp:CheckBox ID="chk_sin_cat" runat="server" CssClass="form-check" />
                                    </div>
                                    <div class="col-md-2">
                                        <span class="badge bg-info">Publicados</span>
                                         <asp:CheckBox ID="chk_publicados" runat="server" CssClass="form-check" />
                                    </div>
                                    <div class="col-md-2">
                                        <span class="badge bg-info">Sin Imagénes</span>
                                        <asp:CheckBox ID="chk_sin_imagenes" runat="server" CssClass="form-check" />
                                    </div>
                                    <div class="col-md-2">
                                        <span class="badge bg-info">Sólo Cotización</span>
                                        <asp:CheckBox ID="chk_cotizac" runat="server" CssClass="form-check" />
                                    </div>
                                    <div class="col-md-2">
                                         <span class="badge bg-info">Sólo Ventas</span>
                                         <asp:CheckBox ID="chk_ventas" runat="server" CssClass="form-check" />
                                    </div>
                                    <div class="col-md-2">
                                        <span class="badge bg-info">NO Publicados</span>
                                        <asp:CheckBox ID="chk_no_publicados" runat="server" CssClass="form-check" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Button ID="Btn_buscar" runat="server" OnClick="Btn_buscar_Click" Text="Buscar" CssClass="btn btn-primary btn-responsive btninter" />
                                    </div>
                                </div>
                                <p class="divider"></p>
                            </div>
                            <p class="divider"></p>
                            <div class="container-fluid rounded border border-secondary">
                                <div class="row">
                                    <div class="col-md-2">
                                        <span class="badge bg-info">Cantidad Productos</span>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lbl_cantidad" runat="server" CssClass="form-control text-info"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="badge bg-info">Prod. Publicados</span>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lbl_prod_publicados" runat="server" CssClass="form-control text-info"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:ImageButton ID="Excel" runat="server" ImageUrl="~/img/xls.gif" OnClick="Excel_Click" Width="30px" Height="30px" />
                                    </div>
                                </div>
                            </div>
                            <p class="divider"></p>
                            <asp:GridView ID="Productos" runat="server" CssClass="table table-striped table-hover" AllowSorting="True" ShowFooter="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnRowCommand="Productos_RowCommand" OnRowDataBound="Productos_RowDataBound">
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" CommandName="ver" ImageUrl="~/img/vista.png" >
                                        <ControlStyle Height="20px" Width="20px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="codigo" HeaderText="Código" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="visible" HeaderText="Visible" />
                                    <asp:BoundField DataField="a_pedido" HeaderText="A Pedido" />
                                    <asp:BoundField DataField="venta" HeaderText="Vta" />
                                    <asp:BoundField DataField="cotizacion" HeaderText="Cot" />
                                    <asp:BoundField DataField="m_tecnico" HeaderText="M. Téc" />
                                    <asp:BoundField DataField="presentacion" HeaderText="Pres" />
                                    <asp:BoundField DataField="foto1" HeaderText="Foto1" />
                                    <asp:BoundField DataField="foto2" HeaderText="Foto2" />
                                    <asp:BoundField DataField="video" HeaderText="Vid" />
                                    <asp:BoundField DataField="H_Seg" HeaderText="H. Seg" />
                                    <asp:BoundField DataField="publicado" HeaderText="Pub" />
                                    <asp:BoundField DataField="activo" HeaderText="Act" />
                                </Columns>
                            </asp:GridView>
                        </main>
                    </div>
                </div>
       
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="scripts/bootstrap.bundle.min.js"></script>
    <script src="scripts/feather.min.js"></script>
    <script src="scripts/Chart.min.js"></script>
    <script src="scripts/dashboard.js"></script>
</body>
</html>

<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>
