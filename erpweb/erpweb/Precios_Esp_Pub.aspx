<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Precios_Esp_Pub.aspx.cs" Inherits="erpweb.Precios_Esp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Publicar precios especiales a Clientes en el Sitio Web</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            display: block;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-clip: padding-box;
            border-radius: 0.25rem;
            transition: none;
            border: 1px solid #ced4da;
            background-color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-11">
                <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;"/>Publicar precios especiales a Clientes ERP en Sitio WEB</h1>
            </div>
            <div class="col-md-1 float-right">
                 <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_volver_Click">Volver</asp:LinkButton>
            </div>
            <p></p>
          </div>
     </div>
    <br />
        <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-4">
                 <h5><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span></h5>
            </div>
            <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h5>
            </div>
            <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger text-dark" Height="16px"></asp:Label></span></h5>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <h4><span class="badge badge-primary">Búsqueda de Información</span></h4>
        <div class="row">
            <div class="col-md-1">
                <h4><span class="badge badge-info">id</span></h4>
            </div>
            <div class="col-md-1">
                <h5><asp:TextBox ID="txt_idw" runat="server" BackColor="#FFFFCC" Width="127px" CssClass="form-control"></asp:TextBox></h5>
            </div>
                <div class="col-md-1">
                <h5><span class="badge badge-info">Rut</span></h5>
            </div>
                <div class="col-md-1">
                <h5><asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="128px"></asp:TextBox></h5>
            </div>
            <div class="col-md-1">
                <h4><span class="badge badge-info">Razón Social</span></h4>
            </div>
            <div class="col-md-2">
                <h5><asp:TextBox ID="txt_razonw" runat="server" Width="240px"  CssClass="auto-style1" BackColor="#FFFFCC"></asp:TextBox></h5>
            </div>
            <div class="col-md-3">
                <h5><asp:Button ID="Btn_buscarw" runat="server" Text="Buscar" CssClass="btn btn-md btn-primary active btn-block" OnClick="Btn_buscarw_Click" Width="91px"/></h5>
            </div>
          </div>
     </div>
     <br />
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
             <div class="col-md-12">
                <asp:GridView ID="lista" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" Width="1768px" AutoGenerateSelectButton="True" OnSelectedIndexChanged="lista_SelectedIndexChanged" ShowFooter="True" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id_cliente" HeaderText="ID" />
                        <asp:BoundField DataField="rut" HeaderText="Rut" />
                        <asp:BoundField DataField="dv_rut" HeaderText="DV" />
                        <asp:BoundField DataField="razon_social" HeaderText="Razón Social" />
                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="telefono2" HeaderText="Teléfono2" />
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="comuna" HeaderText="Comuna" />
                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="id_region" HeaderText="Región" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                    </Columns>
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
                <div class="col-md-1">
                    <h6><span class="badge badge-info">ID</span></h6>
                    <h6><span><asp:Label ID="lbl_id" CssClass="form-check-label" runat="server" Width="136px"></asp:Label></span></h6>
                </div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-info">Rut</span></h6>
                     <h6><span><asp:Label ID="lbl_rut" runat="server"></asp:Label>
                        <asp:Label ID="lbl_dv" runat="server" CssClass="form-check-label" Visible="False"></asp:Label></span></h6>
                </div>
                <div class="col-md-6">
                     <h6><span class="badge badge-info">Razón Social</span></h6>
                     <h6><span><asp:Label ID="lbl_razon"  CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                <div class="col-md-1">
                     <h6><span><asp:Label ID="lbl_fono"  CssClass="form-check-label" runat="server" Visible="false"></asp:Label></span></h6>
                     <h6><span><asp:Label ID="lbl_fono2" CssClass="form-check-label" runat="server" Visible="false"></asp:Label></span></h6>
                     <h6><span><asp:Label ID="lbl_direccion" CssClass="form-check-label" runat="server" Visible="false"></asp:Label></span></h6>
                     <h6><span><asp:Label ID="lbl_ciudad" CssClass="form-check-label" runat="server" Visible="false"></asp:Label></span></h6>
                     <h6><span><asp:Label ID="lbl_comuna" CssClass="form-check-label" runat="server" Visible="false"></asp:Label></span></h6>
                     <h6><span><asp:Label ID="lbl_región" CssClass="form-check-label" runat="server" Visible="false"></asp:Label></span></h6>
                     <h6><span><asp:Label ID="lbl_email" CssClass="form-check-label" runat="server" Visible="false"></asp:Label></span></h6>
                </div>
            </div>
        </div>
        <br />
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-6">
                    <asp:Button ID="Btn_cargar" runat="server" OnClick="Btn_cargar_Click" CssClass="btn btn-md btn-success active" Text="Cargar Cliente y Productos Seleccionado(s) al Sitio Web" Width="445px" Height="31px" />
                </div>
                <div class="col-md-2">
                    <h4><span class="badge badge-info">Producto(s) que no se puedan seleccionar, es porque ya están publicados en el Sitio Web</span></h4>
                </div>
            </div>
            <p></p>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="List_ProdEsp" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1205px" ShowFooter="True" AutoGenerateColumns="False" OnRowDataBound="List_ProdEsp_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_selecciona" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="id" HeaderText="Id" />
                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="prodpedido" HeaderText="Prod a Pedido" />
                            <asp:BoundField DataField="visible" HeaderText="Visible" />
                            <asp:BoundField DataField="cotizaciones" HeaderText="Cotizaciones" />
                            <asp:BoundField DataField="ventas" HeaderText="Ventas" />
                            <asp:BoundField DataField="moneda" HeaderText="Moneda" />
                            <asp:BoundField DataField="precio_lista" HeaderText="Precio Lista" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                        </Columns>
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
    



            


 
        


     
    </form>
</body>
</html>
