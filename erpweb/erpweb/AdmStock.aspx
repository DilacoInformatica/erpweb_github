<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmStock.aspx.cs" Inherits="erpweb.AdmStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <title>Administración de Stock</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container-fluid rounded border border-secondary bg-light">
                    <div class="row">
                        <div class="col-md-10">
                            <h1 class="text-center text-primary">
                                <img alt="" src="img/vineta.gif" style="width: 31px; height: 33px;" />Administración Stock Productos Web</h1>
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
                            <h6><span>
                                <asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                   
                        <span>
                            <asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h6>
                        </div>
                        <div class="col-md-4">
                            <h6><span>
                                <asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h6>
                        </div>
                        <div class="col-md-4">
                            <h6><span>
                                <asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h6>
                        </div>
                    </div>
                </div>
                <br />
                <div class="container-fluid rounded border border-secondary bg-light">
                    <div class="row">
                        <div class="col-md-12">
                            <h4><span class="badge bg-primary">Búsqueda de Información</span></h4>
                        </div>
                        <div class="col-md-1">
                            <h6><span class="badge bg-info">Código</span> </h6>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txt_codigo" runat="server" Width="163px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <h6><span class="badge bg-info">Línea de Ventas</span> </h6>
                        </div>
                        <div class="col-md-3">
                            <h5>
                                <asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True" CssClass="form-select" Width="362px">
                                    <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList></h5>
                        </div>
                        <div class="col-md-1">
                            <h6>
                                <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" CssClass="btn btn-info btn-responsive btninter" OnClick="Btn_buscar_Click" />
                            </h6>
                        </div>
                    </div>
                </div>
                <br />
                <div class="container-fluid rounded border border-secondary bg-light">
                    <div class="col-md-3">
                       <h6><asp:Button ID="Btn_procesar" runat="server" Text="Aplicar regla de Stock" CssClass="btn btn-primary btn-responsive btninter" OnClick="Btn_procesar_Click" /></h6>
                    </div>
                    <div class="col-md-3">
                       <h6><asp:Button ID="btn_genera_mov_stock" runat="server" Text="Crear Mov. Entre Bodegas" CssClass="btn btn-success btn-responsive btninter" OnClick="btn_genera_mov_stock_Click" Width="169px" /></h6>
                    </div>
                </div>
                <br />
                <div class="container-fluid rounded border border-secondary bg-light" id="consola">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <label>
                                        <h4>
                                            <asp:Label ID="lbl_mensaje" runat="server" CssClass="badge bg-info"></asp:Label></h4>
                                    </label>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        <h4>
                                            <asp:Label ID="lbl_regla" runat="server" CssClass="badge bg-info"></asp:Label></h4>
                                    </label>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-12">

                            <asp:GridView ID="Grilla" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="Grilla_RowDataBound" ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="Chk_todos" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_todos_CheckedChanged" />
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
                                    <asp:BoundField HeaderText="Stock" DataField="stock" />
                                    <asp:BoundField DataField="stock_critico" HeaderText="Stock Crítico" />
                                    <asp:TemplateField HeaderText="Stock Bodega salida">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_stock_bod_out_grid" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nivel a Aplicar">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_nivel_aplicar_grid" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Stock a Mover">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Por_Stock" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Stock Critico">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Por_Stock_Critico" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cant Stock a Mover">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_stock_a_mover_grid" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Critico">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_stock_critico" runat="server"></asp:Label>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
