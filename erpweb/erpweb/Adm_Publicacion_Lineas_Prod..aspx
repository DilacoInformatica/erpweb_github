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
        .auto-style32 {
            text-align: right;
        }
        .auto-style33 {
            width: 70%;
        }
        .auto-style34 {
            height: 15px;
        }
        .auto-style35 {
            height: 15px;
            width: 113px;
        }
        .auto-style37 {
            height: 24px;
        }
        .auto-style38 {
            height: 24px;
            width: 457px;
        }
        .auto-style39 {
            height: 15px;
            width: 457px;
        }
        .auto-style41 {
            height: 24px;
            width: 476px;
        }
        .auto-style42 {
            height: 15px;
            width: 476px;
        }
        .auto-style44 {
            height: 15px;
            width: 1805px;
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
                <td colspan="6" class="auto-style28">
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
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style32" colspan="6">
                    <asp:Button ID="Btn_Buscar" runat="server" OnClick="Btn_Buscar_Click" Text="Buscar" />
                </td>
            </tr>
        </table>
        <br />
        <div>

            <table class="auto-style33">
                <tr class="BottomTabla">
                    <td colspan="3" class="auto-style37">Sitio Web</td>
                    <td class="auto-style41"></td>
                    <td class="auto-style38"></td>
                    <td class="auto-style37"></td>
                    <td class="auto-style37">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style35">Nombre</td>
                    <td class="auto-style44">
                        <asp:Label ID="lbl_nombre" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style34">Alias</td>
                    <td class="auto-style42">
                        <asp:TextBox ID="txt_alias" runat="server" BackColor="#FFFFCC" Width="543px"></asp:TextBox>
                    </td>
                    <td class="auto-style39">Visible</td>
                    <td class="auto-style34">
                        <asp:CheckBox ID="ChkDivVisible" runat="server" />
                    </td>
                    <td class="auto-style34">
                        <asp:Button ID="Btn_Ac_Div" runat="server" OnClick="Btn_Ac_Div_Click" Text="Actualizar" />
                    </td>
                </tr>
                </table>

        </div>
        <br />
        <div>

            <asp:GridView ID="GrdCategorias" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AutoGenerateSelectButton="True" Caption="Categorias" OnRowDataBound="GrdCategorias_RowDataBound" ShowFooter="True" Width="925px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Cod" HeaderText="Código" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:TemplateField ConvertEmptyStringToNull="False">
                        <EditItemTemplate>
                            <asp:DynamicControl ID="DynamicControl1" runat="server" DataField="" Mode="Edit" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_activo" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="False">
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

        </div>
    </form>
</body>
</html>
