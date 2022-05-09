<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_NV.aspx.cs" Inherits="erpweb.Detalle_NV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <title>Detalle Nota de Venta</title>
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
                Detalle Nota de Venta WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label>
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
                                <asp:LinkButton ID="LnkBtn_Aprobar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Aprobar_Click"><span data-feather="file-plus"></span>Aprobar</asp:LinkButton>
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
                                <h4><span data-feather="x-cirle"></span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></h4>
                            </div>
                        </div>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h5><span class="badge bg-primary">Cabecera Nota de Venta</span></h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="badge bg-info">N° NV ERP</span>
                                        <asp:Label ID="lbl_numero_erp" CssClass="form-control" runat="server" Width="227px"></asp:Label>
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
                                        <span class="badge bg-info">N° WebPay</span>
                                        <asp:Label ID="lbl_transac_pago" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="LnkUpdInfoPagoNV" CssClass="form-control" runat="server" OnClick="LnkUpdInfoPagoNV_Click" Visible="False">Actualizar Información</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Documento</span>
                                        <asp:Label ID="lbl_tipo_facturacion" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <p class="divider"></p>
                                <div class="row">
                                    <div class="col-md-12">
                                        <h5><span class="badge bg-primary">Información Cliente</span></h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Existe en ERP</span>
                                        <asp:Label ID="lbl_existe" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Rut</span>
                                        <h5>
                                            <asp:Label ID="lbl_rut" runat="server" CssClass="form-control" Width="225px"></asp:Label></h5>
                                        <asp:Label ID="lbl_rut_exit" runat="server" CssClass="form-control" Width="181px" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <span class="badge bg-info">Cliente</span>
                                        <asp:Label ID="lbl_cliente" runat="server" CssClass="form-control"></asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Teléfono</span>
                                        <asp:Label ID="lbl_fono" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Email</span>
                                        <asp:Label ID="lbl_email" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <span class="badge bg-info">Dirección</span>
                                        <asp:Label ID="lbl_direccion" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Región</span>
                                        <asp:Label ID="lbl_region" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Ciudad</span>
                                        <asp:Label ID="lbl_ciudad" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Comuna</span>
                                        <asp:Label ID="lbl_comuna" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <p class="divider"></p>
                                <div class="row">
                                    <div class="col-md-12">
                                        <h5><span class="badge bg-primary">Información Despacho</span></h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <span class="badge bg-info">Contacto</span>
                                        <asp:Label ID="lbl_contacto" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <span class="badge bg-info">Teléfono</span>
                                        <asp:Label ID="lbl_fono_despacho" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <span class="badge bg-info">Email</span>
                                        <asp:Label ID="lbl_email_contacto" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <span class="badge bg-info">Dirección</span>
                                        <h5>
                                            <asp:Label ID="lbl_direccion_despacho" runat="server" CssClass="form-control"></asp:Label></h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <span class="badge bg-info">Región</span>
                                        <asp:Label ID="lbl_region_despacho" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <span class="badge bg-info">Ciudad</span>
                                        <asp:Label ID="lbl_ciudad_despacho" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <span class="badge bg-info">Comuna</span>
                                        <asp:Label ID="lbl_comuna_despacho" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <span class="badge bg-info">Observaciones</span>
                                        <asp:Label ID="lbl_obs_despacho" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12">
                                <h5><span class="badge bg-primary">Generar NV de Venta</span></h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="RadBtnNV" CssClass="form-check-input" runat="server" Text="" GroupName="N1" Checked="True" />
                                    <label class="form-check-label" for="RadBtnNV"><span class="badge bg-info">Sólo Nota de Venta</span></label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="RadNVDesp" CssClass="form-check-input" runat="server" Text="" GroupName="N1" />
                                    <label class="form-check-label" for="RadNVDesp"><span class="badge bg-info">Nota de Venta y Despacho</span></label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <span class="badge bg-info">Representante de Ventas</span>

                                        <asp:DropDownList ID="Lista_Vendedores" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <br />
                                        <asp:Button ID="Btn_Rechazar" runat="server" CssClass="btn btn-md btn-danger active btn-block" OnClick="Btn_Rechazar_Click" Text="Rechazar Nota de Venta" />
                                        <asp:Button ID="Btn_crearNV" runat="server" CssClass="btn btn-md btn-primary active btn-block" OnClick="Btn_crearNV_Click" Text="Aprobar Nota de Venta" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                    <asp:GridView ID="lista_detalles" CssClass="table table-striped table-hover" runat="server" ShowFooter="True" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Item" HeaderText="Item" />
                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="Valor_Unitario" HeaderText="Precio Unitario" />
                        </Columns>
                    </asp:GridView>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right">
                                        <h6><span class="badge bg-info">Moneda</span></h6>
                                    </div>
                                    <div class="col-md-1 float-right">
                                        <h6><span class="text-md-right">
                                            <asp:Label ID="lbl_moneda" CssClass="form-control" runat="server" Width="150px"></asp:Label></span></h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right">
                                        <h6><span class="badge bg-info">Neto</span></h6>
                                    </div>
                                    <div class="col-md-1 float-right">
                                        <h6>
                                            <asp:Label ID="lbl_neto" CssClass="form-control" runat="server" Width="150px"></asp:Label></h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right">
                                        <h6><span class="badge bg-info">Iva</span></h6>
                                    </div>
                                    <div class="col-md-1 float-right">
                                        <h6>
                                            <asp:Label ID="lbl_tax" runat="server" CssClass="form-control" Width="150px"></asp:Label></h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right">
                                        <h6><span class="badge bg-info">Total</span></h6>
                                    </div>
                                    <div class="col-md-1 float-right">
                                        <h6>
                                            <asp:Label ID="lbl_total" runat="server" CssClass="form-control" Width="150px"></asp:Label></h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1 float-right"></div>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lbl_id_nv" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lbl_transac" runat="server" Visible="False"></asp:Label>
                    </div>
                </main>
            </div>
        </div>
        <%-- fin plantilla boostrap --%>
        <script src="scripts/bootstrap.bundle.min.js"></script>
        <script src="scripts/feather.min.js"></script>
        <script src="scripts/Chart.min.js"></script>
        <script src="scripts/dashboard.js"></script>
    </form>
</body>
</html>
