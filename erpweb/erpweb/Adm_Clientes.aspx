<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_Clientes.aspx.cs" Inherits="erpweb.Adm_Clientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <meta name="theme-color" content="#7952b3" />

    <title>Administración Clientes</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
    <script src="scripts/jquery-3.6.0.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

     <script>
        function valida() {
            if (document.getElementById("txt_id").value == '' && document.getElementById("txt_rut").value == '' && document.getElementById("txt_razon").value == '') {
                alert('Ingrese criterio de búsqueda');
                document.getElementById("txt_id").focus();
                return false;
            }
        }
    </script>
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
                Administración de Clientes Sitio Web
            </label>
            <div class="navbar-nav">
                <div class="nav-item text-nowrap">
                    <asp:LinkButton ID="Btn_volver" runat="server" CssClass="nav-link px-3" OnClick="LinkButton2_Click" Width="133px">Volver</asp:LinkButton>
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
                    <div class="container-fluid rounded border border-secondary alert alert-warning">
                        <div class="row">
                            <div class="col-md-12">
                                <label class="text-primary">
                                    <strong>Atención:</strong> Todos los clientes ingresarán al ERP con el Transportista: <strong>
                                        <asp:Label ID="lbl_transportista" runat="server" Text=""></asp:Label></strong>
                                    &nbsp;<strong><span class="text-danger">(Por Pagar)</span></strong>. Antes de aprobar, verifique disponibilidad del transportista en dirección indicada. En caso contrario, seleccione un transportista acorde en el detalle del cliente
                                </label>
                            </div>
                        </div>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                               <h4><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"><span data-feather="alert-triangle"></span></asp:Label></h4>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h4><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"><span data-feather="x-cirle"></span></asp:Label></h4>
                            </div>
                        </div>
                        <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                    <div class="container-fluid rounded border border-secondary bg-light">
                        <div class="row">
                            <h4><span class="badge bg-primary">Búsqueda de Información</span></h4>
                            <div class="col-md-3">
                                <span class="badge bg-info">Id Cliente</span>
                                <asp:TextBox ID="txt_idw" runat="server" BackColor="#FFFFCC"  CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">Rut</span>
                                <asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC"  CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <span class="badge bg-info">Razón Social</span>
                                <asp:TextBox ID="txt_razonw" runat="server" BackColor="#FFFFCC" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <span></span>
                                <asp:Button ID="Btn_buscarw" runat="server" CssClass="btn btn-primary" Text="Buscar Cliente(s)" OnClick="Btn_buscarw_Click" />
                            </div>
                        </div>
                         <p class="divider"></p>
                    </div>
                    <p class="divider"></p>
                   
                    <asp:GridView ID="lista_clientes" CssClass="table table-striped table-hover" runat="server"  ShowFooter="True" AutoGenerateColumns="False" OnRowCommand="lista_clientes_RowCommand" OnRowDataBound="lista_clientes_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Rut" HeaderText="Rut" />
                            <asp:BoundField DataField="Dv_rut" HeaderText="DV" />
                            <asp:BoundField DataField="Razon_Social" HeaderText="Razón Social" />
                            <asp:BoundField DataField="Telefonos" HeaderText="Teléfono" Visible="False" />
                            <asp:BoundField DataField="Telefonos2" HeaderText="Teléfono2" Visible="False" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                            <asp:BoundField DataField="Comuna" HeaderText="Comuna" />
                            <asp:BoundField DataField="Id_region" HeaderText="Región" Visible="False" />
                            <asp:BoundField DataField="Pais" HeaderText="País" Visible="False" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Giro" HeaderText="Giro" Visible="False" />
                            <asp:TemplateField HeaderText="Cliente Precio Esp.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_cli_precio_esp" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField Text="Detalle" ControlStyle-CssClass="btn btn-success" CommandName="detalle">
                                <ControlStyle CssClass="btn btn-success" ForeColor="White"></ControlStyle>
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="lbl_cantidad" CssClass="badge bg-success" runat="server"></asp:Label>
 <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>

                    


                </main>
            </div>
        </div>
    </form>
      <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Modal Header</h4>
        </div>
        <div class="modal-body">
          <p>Some text in the modal.</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
     <script src="scripts/bootstrap.bundle.min.js"></script>
    <script src="scripts/feather.min.js"></script>
    <script src="scripts/Chart.min.js"></script>
    <script src="scripts/dashboard.js"></script>


</body>
</html>


