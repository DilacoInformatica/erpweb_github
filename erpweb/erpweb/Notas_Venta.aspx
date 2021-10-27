<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notas_Venta.aspx.cs" Inherits="erpweb.Notas_Venta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Administración Notas de Venta generadas en el Sitio Web</title>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />

    <script>
        function valida() {
            if (document.getElementById("txt_nv").value == '' && document.getElementById("txt_rut").value == '') {
                alert('Ingrese criterio de búsqueda');
                document.getElementById("txt_id").focus();
                return false;
            }
        }
    </script>
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>
</head>
<body>
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
                Notas de Venta generadas en Sitio Web
            </label>
            <div class="navbar-nav">
                <div class="nav-item text-nowrap">
                    <asp:LinkButton ID="Lnk_volver" runat="server" CssClass="nav-link px-3" Width="133px" OnClick="Lnk_volver_Click">Volver</asp:LinkButton>
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
                    <p class="divider"></p>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger text-white-50"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label>
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">

                        <div class="row">
                            <h4><span class="badge bg-primary">Búsqueda de Información</span></h4>
                            <div class="col-3">
                                <span class="badge bg-info">Nota de Venta</span>
                                <asp:TextBox ID="txt_nv" runat="server" CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox>
                            </div>
                            <div class="col-3">
                                <span class="badge bg-info">Rut Cliente</span>
                                <asp:TextBox ID="txt_rut" runat="server" CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">Estado NV</span>
                                <asp:DropDownList ID="LstEstadoNV" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-3">
                                <br />
                                <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" OnClick="Btn_buscar_Click" CssClass="btn btn-md btn-primary active btn-block" />
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <h6>
                        <asp:Label ID="lbl_cantidad" runat="server"></asp:Label></h6>
                    <asp:GridView ID="Lista_notas" CssClass="table table-striped table-hover" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_notas_SelectedIndexChanged" ShowFooter="True" HorizontalAlign="Justify" OnRowDataBound="Lista_notas_RowDataBound" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Nta_vta_num" HeaderText="Nota Venta" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="Id_cliente" HeaderText="Id Cliente" />
                            <asp:BoundField DataField="rut" HeaderText="Rut" />
                            <asp:BoundField DataField="Razon_Social" HeaderText="Cliente" />
                            <asp:BoundField DataField="neto" HeaderText="Neto" Visible ="false" />
                            <asp:BoundField DataField="Tax_venta" HeaderText="IVA" Visible ="false" />
                            <asp:BoundField DataField="Suma_total" HeaderText="Total" />
                            <asp:BoundField DataField="No_transaccion_web" HeaderText="N° Transac. Webpay" />
                            <asp:BoundField DataField="Status_SitioWeb" HeaderText="Estado" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <HeaderStyle CssClass="ColumnaOculta"></HeaderStyle>

                                <ItemStyle CssClass="ColumnaOculta"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Image ID="img_estado" runat="server" Height="36px" Width="36px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="N° NV ERP">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_num_nv_erp" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12">
                                <h6>
                                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="badge bg-warning"></asp:Label></h6>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>




        <script src="scripts/bootstrap.bundle.min.js"></script>
        <script src="scripts/feather.min.js"></script>
        <script src="scripts/Chart.min.js"></script>
        <script src="scripts/dashboard.js"></script>
    </form>

</body>
</html>
<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>
