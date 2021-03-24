<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="erpweb.Stock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Stock de Productos en la Web</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-11">
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
            <div class="col-sm-12">
                <h4><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span></h4>
                <h4><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h4>
                <h4><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h4>
            </div>
        </div>
    </div>
    <br />
     <div class="container-fluid rounded border border-secondary bg-light" >
         <div class="row">
              <div class="col-md-12"><h4><span class="badge badge-primary">Búsqueda de Información</span></h4></div>
             <div class="col-md-1"><h4><span class="badge badge-info">Código</span> </h4></div>
             <div class="col-md-2"><asp:TextBox ID="txt_codigo" runat="server" Width="163px"></asp:TextBox></div>
             <div class="col-md-1"><h4><span class="badge badge-info">Línea de Ventas</span> </h4></div>
             <div class="col-md-3"><h5><asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True"  Width="439px">
                                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                         </asp:DropDownList></h5>
             </div>
         </div>
         <div class="row">
             <div class="col-md-12"><asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click" CssClass="btn btn-md btn-primary active btn-block"/></div>
         </div>
         <p></p>
     </div>
     <br />
        <div class="container-fluid rounded border border-secondary bg-ligh">
            <div class="row">
                <div class ="col-md-3">
                    <asp:Button ID="btn_actualizar" runat="server" CssClass="btn btn-md btn-primary"  Text="Actualizar Stock" OnClick="btn_actualizar_Click" />
                </div>
                 <div class ="col-md-1">
                    <h4><span class="badge badge-warning"><asp:CheckBox ID="Chk_desactiva_cods" runat="server" Text="Desmarcar opción Ventas para productos con Stock en cero?" TextAlign="Left" /></span></h4>
                </div>
            </div>

        </div>
     <br />
     <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">
                 <asp:GridView ID="Grilla" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" Width="1408px" AutoGenerateColumns="False" OnRowDataBound="Grilla_RowDataBound" ShowFooter="True" ShowHeader="true" ShowHeaderWhenEmpty="True">
                <AlternatingRowStyle BackColor="White" />
                     <Columns>
                         <asp:BoundField DataField="ID" HeaderText="ID" />
                         <asp:BoundField DataField="Codigo" HeaderText="Código" />
                         <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                         <asp:BoundField DataField="pedido" HeaderText="A pedido" />
                         <asp:TemplateField HeaderText="Stock">
                             <ItemTemplate>
                                 <asp:Label ID="lbl_stock" runat="server" Text='<%# Bind("stock") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Stock Critico">
                             <ItemTemplate>
                                 <asp:Label ID="lbl_stock_critico" runat="server" Text='<%# Bind("stock_critico") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
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
            <div class="col-md-3">
                <span><asp:Label ID="lbl_mensaje" runat="server" CssClass="form-control"></asp:Label></span>
            </div>
        </div>
     </div>


    <div>
        <h1>&nbsp;</h1></div>
        <br />
        <br />
                    
        <br />
        
        
    </form>
</body>
</html>
