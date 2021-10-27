<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_Clientes_Det.aspx.cs" Inherits="erpweb.Adm_Clientes_Det" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Detalle nuevo cliente</title>
    <script src="scripts/bootstrap.min.js"></script>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
            <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" href="#">Usuarios</a>
            <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <label class="text-light text-center fs-4 fw-bold">
                Detalle Ingreso nuevo Cliente
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
                                <asp:LinkButton ID="LnkBtn_Aprobar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Aprobar_Click"><span data-feather="check"></span>Aprobar</asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton ID="LnkBtn_Rechazar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Rechazar_Click"> <span data-feather="trash"></span>Rechazar</asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton ID="LnkBtn_Volver" CssClass="nav-link" runat="server" OnClick="LnkBtn_Volver_Click"> <span data-feather="log-out"></span>Volver</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </nav>

                <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
                    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                        <div class="btn-toolbar mb-2 mb-md-0">
                            <label>
                                <span data-feather="user-check"></span>
                                <asp:Label ID="lbl_conectado" runat="server"></asp:Label>
                                <span data-feather="message-circle"></span>
                                <asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
                            </label>

                        </div>
                    </div>
                    <br />
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
                    <br />
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="text-success">Id</label>
                                <asp:Label ID="lbl_id" runat="server" CssClass="bg-primary text-white form-control"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <label class="text-success">Rut</label>
                                <asp:Label ID="lbl_rut" runat="server" CssClass="bg-primary text-white form-control"></asp:Label>
                                <asp:Label ID="lbl_dv" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbl_rut_master" runat="server" Visible="false"> </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <label class="text-success">Nombre o Razón Social</label>
                                <asp:Label ID="lbl_nombre" runat="server" CssClass="bg-primary text-white form-control form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="text-success">Giro</label>
                                <asp:TextBox ID="txt_giro" runat="server" CssClass="bg-primary text-white form-control form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="text-success">Dirección</label>
                                <asp:TextBox ID="txt_direccion" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="text-success">Pais</label>
                                <asp:TextBox ID="txt_pais" runat="server" CssClass="bg-primary text-white form-control form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-success">Región</label>
                                <asp:TextBox ID="txt_region" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-success">Comuna</label>
                                <asp:TextBox ID="txt_comuna" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-success">Ciudad</label>
                                <asp:TextBox ID="txt_ciudad" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="text-success">Teléfono (1)</label>
                                <asp:TextBox ID="txt_fono1" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-success">Teléfono (2)</label>
                                <asp:TextBox ID="txt_fono2" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-success">Email</label><span data-feather="mail"></span>
                                <asp:TextBox ID="txt_email" AutoCompleteType="Email" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-success">Transportista</label>
                                <asp:DropDownList ID="Lst_Trasnportistas" runat="server" CssClass="bg-primary text-white form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <p class="divider"></p>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="Btn_Aprobar" runat="server" CssClass="form-control btn btn-success" Text="Aprobar" OnClick="Btn_Aprobar_Click" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="Btn_Rechazar" runat="server" CssClass="form-control btn btn-danger" Text="Rechazar" OnClick="Btn_Rechazar_Click" />
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>

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
