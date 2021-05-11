<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock_Masivo.aspx.cs" Inherits="erpweb.Stock_Masivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Actualización Masiva de Items (Stock)</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="scripts/bootstrap.js"></script>
  
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-10">
                <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;"/>Actualización de Stock Productos Web</h1>
            </div>
            <div class="col-md-1 float-right">
                 <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_volver_Click">Volver</asp:LinkButton>
            </div>
          </div>
     </div>
     <br />
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-3">
                    <h6><span>
                        <asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span>
                        <asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span></h6>
                </div>
                <div class="col-md-3">
                    <h6><span>
                        <asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h6>
                </div>
                <div class="col-md-3">
                    <h6><span>
                        <asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h6>
                </div>
            </div>
        </div>
    <br />
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-12">
                    <h6>En esta pantalla Ud podrá generar actualizaciones masivas de Stock de productos que están publicados en la Web, seleccione Línea de Ventas o genere un sólo proceso... 
                    Consulte con el numero Interno en el ERP el resultado una vez finalizado el procedimiento.</h6>
            </div>
         </div>
     </div>
      <br />
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <h4><span class="badge badge-pill badge-primary">Búsqueda de Información</span></h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <h6>
                        <label class="badge badge-pill badge-success">Desde:</label></h6>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ListBodSalida" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <h6>
                        <label class="badge badge-pill badge-success">Hasta:</label></h6>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ListBodEntrada" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <h6>
                        <label class="badge badge-pill badge-success">Línea de Ventas:</label></h6>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="LstLineasVenta" CssClass="auto-style2" runat="server" AppendDataBoundItems="True" Width="233px">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Button CssClass="btn btn-info btn-responsive btninter" ID="btn_buscar" runat="server" Text="Buscar" OnClick="btn_buscar_Click" />
                </div>
            </div>
        </div>
     <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">

                <asp:GridView ID="Grilla_items" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1425px" AutoGenerateColumns="False" OnRowDataBound="Grilla_items_RowDataBound" OnSelectedIndexChanged="Grilla_items_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="Código" DataField="codigo" />
                        <asp:BoundField HeaderText="Descripción" DataField="descripcion" />
                        <asp:BoundField HeaderText="Stock Bod Entrada" DataField="Stock_bodega_entrada" />
                        <asp:BoundField HeaderText="Stock Bod Salida" DataField="Stock_bodega_salida" />
                        <asp:TemplateField HeaderText="% de Prod a mover">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_porcentaje" runat="server" Height="16px" Width="65px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Productos a Mover">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_cantidadd_a_mover" runat="server" Height="17px" Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ajustar">
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Validar" runat="server" Checked="True" />
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
        </div>
        <div class="row">
            <div class="col-md-12">

                 <asp:Button ID="btn_procesar" CssClass="btn btn-md btn-success active" runat="server" Text="Generar Movimiento entre Bodegas" OnClick="btn_procesar_Click" Visible="False" />

            </div>
        </div>
    </div>
    </form>
</body>
</html>
