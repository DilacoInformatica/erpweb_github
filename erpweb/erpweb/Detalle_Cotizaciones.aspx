<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_Cotizaciones.aspx.cs" Inherits="erpweb.Detalle_Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle Cotización</title>
               <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-11">
                    <h1><img alt="" src="img/vineta.gif" style="width:31px;height:33px;" /><span class="text-center text-primary">Detalle Cotización de Venta WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label></span></h1>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="Btn_volver" runat="server" OnClick="Btn_volver_Click" CssClass="btn btn-md btn-primary active btn-block" Text="Volver"/>
                </div>
            </div>
    </div>
    <br />
   <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-4">
                 <h4><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h4>
            </div>
            <div class="auto-style1">
                <h4><span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h4>
            </div>
            <div class="col-md-4">
                <h4><span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger text-black-50"></asp:Label></span></h4>
            </div>
        </div>
    </div>
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <h2><span class="badge bg-primary">Cabecera Cotización</span></h2>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge bg-info">N° Cotización</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_numero_erp" runat="server" CssClass="form-control" Width="153px"></asp:Label></h6>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info">Fecha</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_fecha" runat="server" CssClass="form-control" Width="153px"></asp:Label></h6>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info">N° OC</span></h6>
                </div>
                <div class ="col-md-2">
                    <h6><asp:Label ID="lbl_n_oc" runat="server" CssClass="form-control" Width="153px"></asp:Label></h6>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge bg-info">Rut</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_rut" CssClass="form-control" runat="server" BorderStyle="Groove" Width="153px"></asp:Label></h6>
                        <asp:Label ID="lbl_rut_exit" runat="server" Visible="False"></asp:Label>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info">Existe en ERP</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_existe" runat="server" CssClass="form-control" ></asp:Label></h6>
                    <asp:Label ID="lbl_id_cliente" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info">Usar Info ERP?</span></h6>
                </div>
                <div class="col-md-1">
                    <h6><asp:CheckBox ID="Chk_data_existente" runat="server" AutoPostBack="True" Enabled="False" OnCheckedChanged="Chk_data_existente_CheckedChanged" /></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge bg-info">Cliente Particular</span></h6>
                </div>
                <div class="col-md-1">
                    <h6><asp:CheckBox ID="Chk_Cli_Particular"  runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Cli_Particular_CheckedChanged" /></h6>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h2><span class="badge bg-primary">Información Cliente</span></h2>
                </div>
            </div>

             <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge bg-info">Empresa</span></h6>
                </div>
                <div class="col-md-10">
                    <h6><asp:Label ID="lbl_empresa" runat="server" CssClass="form-control" Width="1345px" Height="36px"></asp:Label></h6>
                </div>
            </div>
             <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge bg-info">Nombres</span></h6>
                </div>
                <div class="col-md-4">
                    <h6><asp:Label ID="lbl_nombre" runat="server" CssClass="form-control" Width="400px"></asp:Label></h6>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info">Apellidos</span></h6>
                </div>
                <div class="col-md-4">
                    <h6><asp:Label ID="lbl_apellidos" runat="server" CssClass="form-control" Width="400px"></asp:Label></h6>
                </div>
            </div>
     
             <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge bg-info">Fono</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_fono" runat="server" CssClass="form-control" Width="252px"></asp:Label></h6>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info col-form-label">Móvil</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_movil" runat="server" CssClass="form-control" Width="252px"></asp:Label></h6>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info col-form-label">Email</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_email" runat="server" CssClass="form-control" Width="300px"></asp:Label></h6>
                </div>
            </div>
             <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge bg-info">Dirección</span></h6>
                </div>
                <div class="col-md-10">
                    <h6><asp:Label ID="lbl_direccion" runat="server" CssClass="form-control"  Width="933px"></asp:Label></h6>
                </div>
            </div>
             <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge bg-info">Comuna</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:TextBox ID="txt_comuna" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="250px"></asp:TextBox></h6>
                    
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info">Ciudad</span></h6>
                </div>
                <div class="col-md-2">
                    <h6><asp:Label ID="lbl_ciudad" runat="server" CssClass="form-control" Width="250px"></asp:Label></h6>
                </div>
                <div class="col-md-1">
                    <h6><span class="badge bg-info">Región</span></h6>
                </div>
                <div class="col-md-2">
                    <h5><asp:DropDownList ID="Lst_Region" CssClass="form-select" runat="server" AppendDataBoundItems="True"  Width="300px">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList></h5>
                    <asp:Label ID="lbl_region" runat="server"></asp:Label>
                </div>
            </div>
    </div>
    <br />
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-2">
                            <h4><span class="badge bg-info">Asistente de Ventas</span></h4>
                        </div>
                        <div class="col-md-3">
                                <h5><asp:DropDownList ID="Lista_Vendedores" CssClass="form-select"  runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList></h5>
                        </div>
                        <div class="col-md-2">
                            <h6>
                                <asp:Button ID="Btn_RechazarCot" CssClass="btn btn-md btn-danger active btn-block" runat="server" OnClick="Btn_RechazarCot_Click" Text="Rechazar Cotización" Width="261px" /></h6>
                        </div>
                        <div class="col-md-2">
                            <h6>
                                <asp:Button ID="Btn_crearCot" CssClass="btn btn-md btn-primary active btn-block" runat="server" OnClick="Btn_crearCot_Click" Text="Crear Cotización en ERP" Width="261px" /></h6>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">
                    <asp:GridView ID="lista_detalles" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1719px" ShowFooter="True" OnRowDataBound="lista_detalles_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge bg-info">Moneda</span></h6>
                        </div>
                        <div class="col-md-1 float-right">
                            <h6><span class="text-md-right"> <asp:Label ID="lbl_moneda" CssClass="form-control" runat="server" Width="150px"></asp:Label></span></h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge bg-info">Neto</span></h6>
                        </div>
                        <div class="col-md-1">
                            <h6><asp:Label ID="lbl_neto" CssClass="form-control" runat="server" Width="150px"></asp:Label></h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge bg-info">Iva</span></h6>
                        </div>
                        <div class="col-md-1 float-right">
                            <h6><asp:Label ID="lbl_tax" runat="server" CssClass="form-control" Width="150px"></asp:Label></h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge bg-info">Total</span></h6>
                        </div>
                        <div class="col-md-1 float-right">
                            <h6><asp:Label ID="lbl_total" runat="server" CssClass="form-control" Width="150px"></asp:Label></h6>            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right"></div>
                        <div class="col-md-1 float-right">    
                            <h6><span class="badge bg-danger">Valores de Referencia</span></h6>      
                        </div>
                    </div>
                </div>
            </div>
        </div>
         <br />

  <%-- final boostrap --%>
        <asp:Label ID="lbl_id_cli" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbl_id_con" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbl_respaldo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbl_id_cot" runat="server" Visible="False"></asp:Label>
</form>
</body>
</html>
