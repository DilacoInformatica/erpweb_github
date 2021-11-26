<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfStock.aspx.cs" Inherits="erpweb.ConfStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <title>Niveles Stock</title>
    <style type="text/css">
        .auto-style1 {
            display: block;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-clip: padding-box;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            border-radius: .25rem;
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
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-10">
                            <h1 class="text-center text-primary">
                                <img alt="" src="img/vineta.gif" style="width: 31px; height: 33px;" />Niveles de Stock</h1>
                        </div>
                        <div class="col-md-1 float-right">
                            <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_volver_Click">Volver</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-3">
                    <h6><span>
                        <asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span>
                        <asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h6>
                </div>
                <div class="col-md-3">
                    <h4><span>
                        <asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h4>
                </div>
                <div class="col-md-3">
                    <h4><span>
                        <asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h4>
                </div>
            </div>
        </div>
        <br />
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-header">
                            Ingreso de reglas
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <h5><label class="badge bg-info">Nivel</label></h5>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txt_mivel" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <h5><label class="badge bg-info">Bodega Salida</label></h5>
                                </div>
                                 <div class="col-md-3">
                                    <asp:DropDownList ID="Lst_Bodegas" runat="server" CssClass="auto-style1"  Width="340px" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccione Bodega</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <h5><label class="badge bg-info">Valor Mínimo</label></h5>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txt_ValMin" runat="server" Width="70px" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <h5><label class="badge bg-info">Valor Máximo</label></h5>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txt_ValMax" runat="server" Width="70px" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <h5><label class="badge bg-info">% Stock a Transpasar</label></h5>
                                </div>
                                 <div class="col-md-2">
                                    <asp:TextBox ID="txt_stock" runat="server" Width="70px" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <h5><label class="badge bg-info">% Stock Crítico</label></h5>
                                </div>
                                 <div class="col-md-2">
                                    <asp:TextBox ID="txt_stock_critico" runat="server" Width="70px" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <hr class="style4"/>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Button ID="Btn_grabar" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClick="Btn_grabar_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            Dilaco
                        </div>
                    </div>
                </div>
                <div class="col-md-8">
                    <asp:GridView ID="Lst_Info" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="Lst_Info_RowDataBound" ShowFooter="True" OnRowCommand="Lst_Info_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_id_nivel_grid" runat="server" Enabled="False" Width="82px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nivel">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_nivel_grid" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bodega Salida">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmb_bodega_grid" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cant. Mínima">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_cant_min_grid" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cant. Máxima">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_cant_max_grid" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% Stock a cargar">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_stock" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% Stock Crítico">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_stock_critico" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_activo_grid" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="grabar" Text="Grabar" ControlStyle-CssClass="btn btn-success text-white">
                                <ControlStyle CssClass="btn btn-success text-white"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="eliminar" Text="Eliminar" ControlStyle-CssClass="btn btn-danger text-white">
                                <ControlStyle CssClass="btn btn-danger text-white"></ControlStyle>
                            </asp:ButtonField>
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
