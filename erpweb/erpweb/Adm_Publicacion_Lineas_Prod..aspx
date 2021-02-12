<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_Publicacion_Lineas_Prod..aspx.cs" Inherits="erpweb.Adm_Publicacion_Lineas_Prod__" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administrador Líneas de Venta en Sitio Web</title>
        <link href="css/estilos.css" rel="stylesheet" />
    <style type="text/css">
        .nuevoEstilo1 {
            vertical-align:top;
        }
        .scroll {
            width: 1600px; 
            height: 298px; 
            overflow-y: scroll;
        }
         .scrolltddiv {
            width: 490px; 
            height: 281px; 
            overflow-y: scroll;
        }
        .scrolltdcat {
            width: 620px;
            height: 281px;
            overflow-y: scroll;
        }
        .scrolltdsubcat {
            width: 670px;
            height: 281px;
            overflow-y: scroll;
        }
        .auto-style23 {
            width: 574px;
        }
        .auto-style24 {
            width: 1273px;
        }
        .auto-style25 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 13px;
            font-weight: bold;
            color: #000033;
            width: 1476px;
            height: 16px;
        }
        .auto-style26 {
            width: 35%;
        }
        .auto-style27 {
            width: 65px;
        }
        .auto-style28 {
            height: 23px;
        }
        .auto-style29 {
            margin-left: 0px;
        }
        .auto-style30 {
            width: 81px;
        }
        .auto-style31 {
            width: 68px;
        }
        .auto-style45 {
            width: 97%;
        }
        .auto-style47 {
            text-align: center;
            height: 24px;
        }
        .auto-style50 {
            height: 24px;
            width: 828px;
            text-align: center;
        }
        .auto-style51 {
            width: 684px;
        }
        .auto-style52 {
            text-align: center;
        }
        .auto-style53 {
            height: 212px;
            vertical-align: top;
        }
        .auto-style55 {
            width: 684px;
            text-align: center;
        }
        .auto-style56 {
            text-align: left;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <table class="auto-style25">
            <tr>
                <td class="auto-style23"><h1 class="auto-style24"><img alt="" src="img/vineta.gif" />Administración Líneas de Ventas en Sitio Web</h1>
                 </td>
            </tr>
        </table>
    </div>
        <asp:Label ID="lbl_error" runat="server" ForeColor="#FF3300"></asp:Label>
        <asp:Label ID="lbl_mensaje" runat="server"></asp:Label>
        <br />
        <table class="auto-style26">
            <tr class="BottomTabla">
                <td colspan="7" class="auto-style28">
        <img alt=""src="img/vineta.gif" /> <strong>Busqueda De Información</strong></td>
            </tr>
            <tr>
                <td class="auto-style27">División</td>
                <td>
                    <asp:DropDownList ID="LstDivision" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivivion_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style31">Categoría</td>
                <td>
                    <asp:DropDownList ID="LstCategoria" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="auto-style29" OnSelectedIndexChanged="LstCategoria_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">Subcategoría</td>
                <td>
                    <asp:DropDownList ID="LstSubCategoria" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Btn_Buscar" runat="server" OnClick="Btn_Buscar_Click" Text="Buscar" />
                </td>
            </tr>
            </table>
        <br />
        <br />
        <div>
            <table class="auto-style45">
                <tr class="BottomTabla">
                    <td class="auto-style50"><strong>ERP</strong></td>
                    <td class="auto-style47"><strong>SITIO WEB</strong></td>
                </tr>
                <tr>
                    <td class="auto-style55">
                        <div>
                        <asp:GridView ID="GrdDivERP" runat="server" AutoGenerateColumns="False" Caption="Detalle Familia ERP" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GrdDivERP_RowDataBound" ShowFooter="True" Width="619px" Height="189px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Id_Familia" HeaderText="Id" />
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:TemplateField HeaderText="Nombre">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_nombre" runat="server" Height="16px" Width="481px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Orden" HeaderText="Orden" />
                                <asp:TemplateField HeaderText="Activo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Activo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publicado">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_FamPublicado" runat="server" />
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
                    </td>
                    <td class="auto-style51">
                        <div>
                        <asp:GridView ID="GridDivWeb" runat="server" AutoGenerateColumns="False" Caption="Detalle Familia Sitio Web" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridDivWeb_RowDataBound" ShowFooter="True" Width="619px" Height="189px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="id_familia" HeaderText="ID" />
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="orden" HeaderText="Orden" />
                                <asp:TemplateField HeaderText="Activo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_activo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visible">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_visible" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Etiqueta">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_etiqueta" runat="server" Height="18px" Width="544px"></asp:TextBox>
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
                    </td>
                </tr>
                <tr>
                    <td class="auto-style53"><div class="auto-style56">
                        <asp:GridView ID="GrdCategoriasERP" runat="server" Caption="Cateorías ERP" CellPadding="4" ForeColor="#333333" GridLines="None" Height="189px" ShowFooter="True" Width="841px" AutoGenerateColumns="False" OnRowDataBound="GrdCategoriasERP_RowDataBound" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GrdCategoriasERP_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID_Categoria" HeaderText="ID" />
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Orden" HeaderText="Orden" />
                                <asp:TemplateField HeaderText="Activo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Activo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publicado">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_CatPublicada" runat="server" />
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

                        </div></td>
                    <td class="auto-style53"><div class="auto-style56">

            <asp:GridView ID="GrdCategoriasWEB" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Caption="Categorias WEB" OnRowDataBound="GrdCategorias_RowDataBound" ShowFooter="True" Width="841px" Height="189px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:ButtonField Text="Ver" />
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Cod" HeaderText="Código" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Activo">
                        <EditItemTemplate>
                            <asp:DynamicControl ID="DynamicControl1" runat="server" DataField="" Mode="Edit" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_activo" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Visible">
                        <EditItemTemplate>
                            <asp:DynamicControl ID="DynamicControl2" runat="server" DataField="" Mode="Edit" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_visible" runat="server" />
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

                        </div></td>
                </tr>
                 <tr>
                    <td class="auto-style53"><div class="auto-style56">
                        <asp:GridView ID="GrdSubCatERP" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="841px" AutoGenerateColumns="False" OnRowDataBound="GrdSubCatERP_RowDataBound" Caption="SubCategorías ERP" ShowFooter="True">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID_SubCategoria" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Orden" HeaderText="Orden" />
                                <asp:TemplateField HeaderText="Activo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_activo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publicado">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_PubSubCat" runat="server" />
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
                        </div></td>
                    <td class="auto-style53"><div>
                        <asp:GridView ID="GrdSubCatWEB" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="841px" AutoGenerateColumns="False" OnRowDataBound="GrdSubCatWEB_RowDataBound" Caption="SubCategorías WEB" ShowFooter="True">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                <asp:BoundField DataField="SubCategoria" HeaderText="SubCategoria" />
                                <asp:TemplateField HeaderText="Activo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkActivoSC" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visible">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkVisibleSC" runat="server" />
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
                        </div></td>
                </tr>
            </table>
        </div>
        <br />
                        <asp:Label ID="lbl_cat" runat="server" Visible="False"></asp:Label>

    </form>
</body>
</html>
