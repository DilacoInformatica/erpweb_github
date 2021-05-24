<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="erpweb.Stock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Stock de Productos en la Web</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="scripts/bootstrap.js"></script>
    <script src="scripts/jquery-3.5.1.min.js"></script>
    <script src="scripts/popper.js"></script>
</head>

<body>
    <form id="form1" runat="server">
     <asp:scriptmanager runat="server"></asp:scriptmanager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-10">
                <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;"/>Stock de Productos en Sitio Web</h1>
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
                 <h6><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h6>
            </div>
            <div class="col-md-4">
                <h6><span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h6>
            </div>
            <div class="col-md-4">
                <h6><span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h6>
            </div>
        </div>
    </div>
    <br />
     <div class="container-fluid rounded border border-secondary bg-light" >
         <div class="row">
              <div class="col-md-12"><h6><span class="badge bg-primary">Búsqueda de Información</span></h6></div>
             <div class="col-md-1"><h6><span class="badge bg-info">Código</span> </h6></div>
             <div class="col-md-2"><asp:TextBox ID="txt_codigo" runat="server" Width="163px" CssClass="form-control"></asp:TextBox></div>
             <div class="col-md-1"><h6><span class="badge bg-info">Línea de Ventas</span> </h6></div>
             <div class="col-md-3"><h5><asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True" CssClass="form-select"  Width="362px" >
                                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                         </asp:DropDownList></h5></div>
             <div class="col-md-1"> <h6><asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click" CssClass="btn btn-info btn-responsive btninter"/></h6></div>
              <div class ="col-md-2">
                    <h6><asp:Button ID="btn_actualizar" runat="server" CssClass="btn btn-success btn-responsive btninter"  Text="Generar cambio masivo de Stock" OnClick="btn_actualizar_Click" /></h6>
                    <asp:CheckBox ID="Chk_desactiva_cods" runat="server" Text="Desmarcar opción Ventas para productos con Stock en cero?" TextAlign="Left" />
                </div>
         </div>
     </div>
     <br />
            <div class="container-fluid rounded border border-secondary bg-light" id="consola" style="v">
        <div class="row">
            <div class="col-md-12">
                 <h6><span class="badge bg-primary">Actualiza Stock Producto</span></h6>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1"><h6><span class="badge bg-primary">Código:</span></h6></div>
            <div class="col-md-1"><h6><asp:Label ID="lbl_codigo" CssClass="form-control badge bg-success text-whit" runat="server" Width="113px"></asp:Label>
                <asp:Label ID="lbl_id" runat="server" Visible="False"></asp:Label>
               </h6></div>
            <div class="col-md-1"><h6><span class="badge bg-primary">Fecha:</span></h6></div>
            <div class="col-md-1"><h6><asp:Label ID="lbl_fecha" CssClass="form-control badge bg-success text-whit" runat="server" Width="113px"></asp:Label></h6></div>
        </div>
         <div class="row">
            <div class="col-md-1"><h6><span class="badge bg-primary">Bodega Salida:</span></h6></div>
            <div class="col-md-2">
                <asp:DropDownList ID="ListBodSalida" runat="server" AppendDataBoundItems="True" CssClass="form-select" OnSelectedIndexChanged="ListBodSalida_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-1"><h6><span class="badge bg-primary">Bodega Entrada:</span></h6></div>
            <div class="col-md-2">
                <asp:DropDownList ID="ListBodEntrada" runat="server" AppendDataBoundItems="True" CssClass="form-select" OnSelectedIndexChanged="ListBodEntrada_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-1"><h6><span class="badge bg-primary">Stock en ERP:</span></h6></div>
            <div class="col-md-1">
                <span>
                <asp:Label CssClass="form-control" ID="lbl_stock_erp" runat="server" Text="" Width="50px"></asp:Label>
                </span>
             </div>
             <div class="col-md-1">
                 <h6><span class="badge bg-primary">Cantidad:</span></h6>
             </div>
             <div class="col-md-1">
                 <h6><span>
                     <asp:TextBox ID="txt_cantidad" runat="server" CssClass="form-control" Width="42px"></asp:TextBox>
                     </span></h6>
             </div>
            <div class="col-md-1">
                <h6><asp:Button ID="btn_genera_mov_stock" runat="server" Text="Generar" CssClass="btn btn-success btn-responsive btninter" OnClick="btn_genera_mov_stock_Click" Width="169px" /></h6>
             </div>
        </div>
    </div>
     <br />
     <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12 col-center overflow-auto" style="max-width: 2000px; max-height: 500px;">
                      <h5>
                         <asp:GridView ID="Grilla" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" Width="1443px" AutoGenerateColumns="False" OnRowDataBound="Grilla_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Grilla_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:BoundField DataField="ID" HeaderText="ID" />
                                 <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                 <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                 <asp:TemplateField HeaderText="Stock">
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_stock" runat="server" Text='<%# Bind("stock") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="vendidos" HeaderText="Vendidos" />
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
                     </h5>
            </div>
            <div class="col-md-3">
                <span><asp:Label ID="lbl_mensaje" runat="server" CssClass="form-control"></asp:Label></span>
            </div>
        </div>
     </div>
 
 </ContentTemplate>
     </asp:UpdatePanel>
    </form>
   
</body>
</html>
