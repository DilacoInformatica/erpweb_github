<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_Cotizaciones.aspx.cs" Inherits="erpweb.Detalle_Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <title>Detalle Cotización</title>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
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
                Detalle Cotización de Venta WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label>
            </label>
            <div class="navbar-nav">
                <div class="nav-item text-nowrap">
                    <asp:LinkButton ID="Btn_volver" runat="server" CssClass="nav-link px-3" OnClick="Btn_volver_Click1">Volver</asp:LinkButton>
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
                                <asp:LinkButton ID="LnkBtn_Aprobar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Aprobar_Click"><span data-feather="file-plus"></span>Crear</asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton ID="LnkBtn_Rechazar" CssClass="nav-link" OnClick="LnkBtn_Rechazar_Click" runat="server"> <span data-feather="trash"></span>Rechazar</asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton ID="LnkBtn_Volver" CssClass="nav-link" runat="server" OnClick="LnkBtn_Volver_Click"> <span data-feather="log-out"></span>Volver</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </nav>

                <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <h4><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <h4><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></h4>
                            </div>
                        </div>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12 bg-primary text-white">
                                <h5>
                                    <label>Cabecera Cotización</label></h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <span class="badge bg-info">N° Cotización</span>
                                <asp:Label ID="lbl_numero_erp" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">Fecha</span>
                                <asp:Label ID="lbl_fecha" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">N° OC</span>
                                <asp:Label ID="lbl_n_oc" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">Existe en Erp</span>
                                <asp:Label ID="lbl_existe" runat="server" CssClass="form-control"></asp:Label>
                                <asp:Label ID="lbl_id_cliente" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <span class="badge bg-info">Observaciones</span>
                                <asp:Label ID="lbl_observaciones" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <span class="badge bg-info">¿Usar Info ERP?</span><br />
                                <asp:CheckBox ID="Chk_data_existente" runat="server" AutoPostBack="True" Enabled="False" OnCheckedChanged="Chk_data_existente_CheckedChanged" />
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">Cliente Particular</span><br />
                                <asp:CheckBox ID="Chk_Cli_Particular" runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Cli_Particular_CheckedChanged" />
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12 bg-primary text-white">
                                <h5>
                                    <label>Información Cliente</label></h5>
                            </div>
                        </div>
                        <p class="divider"></p>
                        <div class="row">
                            <div class="col-md-3">
                                <span class="badge bg-info">Rut</span>
                                <asp:Label ID="lbl_rut" CssClass="form-control" runat="server"></asp:Label>
                                <asp:Label ID="lbl_rut_exit" runat="server" Visible="False"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <span class="badge bg-info">Empresa</span>
                                <asp:Label ID="lbl_empresa" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <span class="badge bg-info">Nombres</span>
                                <asp:Label ID="lbl_nombre" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <span class="badge bg-info">Apellidos</span>
                                <asp:Label ID="lbl_apellidos" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <span class="badge bg-info">Fono</span>
                                <asp:Label ID="lbl_fono" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <span class="badge bg-info col-form-label">Móvil</span>
                                <asp:Label ID="lbl_movil" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <span class="badge bg-info col-form-label">Email</span>
                                <asp:Label ID="lbl_email" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <span class="badge bg-info">Dirección</span>
                                <asp:Label ID="lbl_direccion" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <span class="badge bg-info">Región</span>
                                <asp:DropDownList ID="Lst_Region" CssClass="form-select" runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lbl_region" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <span class="badge bg-info">Comuna</span>
                                <asp:TextBox ID="txt_comuna" runat="server" BackColor="#FFFFCC" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <span class="badge bg-info">Ciudad</span>
                                <asp:Label ID="lbl_ciudad" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <p class="divider"></p>
                        </div>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-6">
                                <span class="badge bg-info">Asistente de Ventas</span>
                                <asp:DropDownList ID="Lista_Vendedores" CssClass="form-select" runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-6">
                                <br />
                                <asp:Button ID="Btn_RechazarCot" CssClass="btn btn-md btn-danger active btn-block" runat="server" OnClick="Btn_RechazarCot_Click" Text="Rechazar Cotización" />
                                <asp:Button ID="Btn_crearCot" CssClass="btn btn-md btn-primary active btn-block" runat="server" OnClick="Btn_crearCot_Click" Text="Crear Cotización en ERP" />
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                    <asp:GridView ID="lista_detalles" runat="server" CssClass="table table-striped table-hover" OnRowDataBound="lista_detalles_RowDataBound">
                    </asp:GridView>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_moneda" CssClass="form-control" Visible="false" runat="server" Width="150px"></asp:Label>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right">
                                        <span class="badge bg-info">Neto</span>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lbl_neto" CssClass="form-control" runat="server" Width="150px"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right">
                                        <span class="badge bg-info">Iva</span>
                                    </div>
                                    <div class="col-md-1 float-right">
                                        <asp:Label ID="lbl_tax" runat="server" CssClass="form-control" Width="150px"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right">
                                        <span class="badge bg-info">Total</span>
                                    </div>
                                    <div class="col-md-1 float-right">
                                        <asp:Label ID="lbl_total" runat="server" CssClass="form-control" Width="150px"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right"></div>
                                    <div class="col-md-1 float-right">
                                        <span class="badge bg-danger">Valores de Referencia</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>

        <%-- final boostrap --%>
        <asp:Label ID="lbl_id_cli" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbl_id_con" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbl_respaldo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbl_id_cot" runat="server" Visible="False"></asp:Label>
    </form>
    <script src="scripts/bootstrap.bundle.min.js"></script>
    <script src="scripts/feather.min.js"></script>
    <script src="scripts/Chart.min.js"></script>
    <script src="scripts/dashboard.js"></script>
</body>
</html>
