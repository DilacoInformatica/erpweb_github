<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inf_Cotizaciones.aspx.cs" Inherits="erpweb.Inf_Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <title>Informe Cotizaciones</title>
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
                Informe Cotizaciones
            </label>
            <div class="navbar-nav">
                <div class="nav-item text-nowrap">
                    <asp:LinkButton ID="Btn_volver" runat="server" CssClass="nav-link px-3" Width="133px" OnClick="Btn_volver_Click">Volver</asp:LinkButton>
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
                                <asp:LinkButton ID="LnkBtn_Descargar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Descargar_Click"> <span data-feather="download"></span>Descargar</asp:LinkButton>
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
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <h4><span class="badge bg-primary">Seleccione Tipo de Informe</span></h4>
                            <div class="col-md-12">
                                 <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="RadBtnTot" CssClass="form-check-input" runat="server" Text="" GroupName="N1" Checked="True" />
                                    <label class="form-check-label" for="RadBtnNV"><span class="badge bg-info">Global</span></label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="RadBtnDet" CssClass="form-check-input" runat="server" Text="" GroupName="N1" />
                                    <label class="form-check-label" for="RadNVDesp"><span class="badge bg-info">Detalle</span></label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="BtnMostrar" CssClass="btn btn-md btn-primary active btn-block" runat="server" Text="Mostrar" OnClick="BtnMostrar_Click" />
                                </div>
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                      <h6>
                        <asp:Label ID="lbl_cantidad" runat="server"></asp:Label></h6>

                    <asp:GridView ID="Lista_cotizacion" CssClass="table table-striped table-hover" runat="server" ShowFooter="True" OnRowDataBound="Lista_cotizacion_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="N° Cot ERP">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_num_cot_erp" runat="server"></asp:Label>
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
