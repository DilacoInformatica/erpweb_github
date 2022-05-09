<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmStock.aspx.cs" Inherits="erpweb.AdmStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <meta name="theme-color" content="#7952b3" />

    <title>Administración de Stock</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True">
                    <Scripts>
                        <asp:ScriptReference Path="scripts/bootstrap.bundle.min.js" />
                        <asp:ScriptReference Path="scripts/feather.min.js" />
                        <asp:ScriptReference Path="scripts/Chart.min.js" />
                        <asp:ScriptReference Path="scripts/dashboard.js" />
                    </Scripts>
                </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
                    <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" href="#"><span data-feather="user-check"></span>
                        <asp:Label ID="lbl_conectado" runat="server"></asp:Label>
                        <span data-feather="message-circle"></span>
                        <asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
                        </label></a>
                    <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <label class="text-light text-center fs-4 fw-bold">
                        Administración Stock de Productos
                    </label>
                    <div class="navbar-nav">
                        <div class="nav-item text-nowrap">
                            <asp:LinkButton ID="Lnkvolver" runat="server" CssClass="nav-link px-3" OnClick="Lnkvolver_Click" Width="133px">Volver</asp:LinkButton>
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
                                         <asp:LinkButton ID="LnkBtn_Aprobar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Aprobar_Click"><span data-feather="check"></span>Crear Solicitud</asp:LinkButton>
                                    </li>
                                    <li class="nav-item">
                                        <asp:LinkButton ID="Btn_volver" runat="server" CssClass="nav-link px-3" OnClick="Btn_volveree_Click"><span data-feather="log-out"></span>Volver</asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </nav>

                        <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4><span data-feather="x-cirle"></span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></h4>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 ">
                                            <h4><span data-feather="alert-triangle"></span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></h4>
                                        </div>
                                    </div>
                                </div>
                                <p class="divider"></p>
                            </div>
                            <p class="divider"></p>
                            <div class="container-fluid rounded border border-secondary bg-ligh">
                                <div class="row">
                                    <h4><span class="badge bg-primary">Búsqueda de Información</span></h4>
                                    <div class="col-md-3">
                                        <span class="badge bg-info">Código</span>
                                        <asp:TextBox ID="txt_codigo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <span class="badge bg-info">Línea de Ventas</span>
                                        <asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True" CssClass="form-select">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <br />
                                        <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="Btn_buscar_Click" />

                                    </div>
                                </div>
                                <p class="divider"></p>
                            </div>
                            <p class="divider"></p>
                           
                            <div class="container-fluid rounded border border-secondary bg-ligh">
                                 <div class="row">
                                     <div class="col-md-6">
                                         <h3><span class="badge bg-primary">Generar Solicitud Stock</span></h3>
                                         <asp:Button ID="Btn_crearSolStock" runat="server" CssClass="btn btn-success btn-responsive btninter" OnClick="Btn_crearSolStock_Click" Text="Crear Solicitud" />
                                     </div>
                                 </div>
                                 <p class="divider">
                                 </p>
                            </div>
                            <p class="divider"></p>
                            <asp:GridView ID="Grilla" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" OnRowDataBound="Grilla_RowDataBound" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="Chk_todos" runat="server" OnCheckedChanged="Chk_todos_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk_marcar" runat="server" OnCheckedChanged="Chk_marcar_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Id" DataField="Id" />
                                    <asp:BoundField HeaderText="Código" DataField="codigo" />
                                    <asp:BoundField HeaderText="Descripción" DataField="descripcion" />
                                    <asp:BoundField HeaderText="Precio" DataField="precio" />
                                    <asp:BoundField HeaderText="Precio Lista" DataField="precio_lista" />
                                    <asp:TemplateField HeaderText="Stock ERP">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_stock_erp" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Stock Web" DataField="stock" />
                                    <asp:BoundField DataField="Stock_critico" HeaderText="Stock Crítico" />
                                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                </Columns>
                            </asp:GridView>

                            <asp:Label ID="lbl_mensaje" runat="server" CssClass="badge bg-info"></asp:Label>
                            <asp:Button ID="Btn_procesar" runat="server" Visible="false" Text="Aplicar regla de Stock" CssClass="btn btn-primary btn-responsive btninter" OnClick="Btn_procesar_Click" />
                            <asp:Button ID="btn_genera_mov_stock" runat="server" Visible="false" Text="Crear Mov. Entre Bodegas" CssClass="btn btn-success btn-responsive btninter" OnClick="btn_genera_mov_stock_Click" Width="169px" />
                        </main>
                    </div>
                    <p class="divider"></p>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="scripts/bootstrap.bundle.min.js"></script>
    <script src="scripts/feather.min.js"></script>
    <script src="scripts/Chart.min.js"></script>
    <script src="scripts/dashboard.js"></script>
</body>
</html>
