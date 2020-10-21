<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_division_ventas.aspx.cs" Inherits="erpweb.Adm_division_ventas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administración División Ventas</title>
    <link href="css/estilos.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            width: 414px;
            vertical-align:top;
        }
        .nuevoEstilo1 {
            vertical-align:top;
        }
        .auto-style6 {
            vertical-align: top;
            width: 1322px;
        }
        .auto-style7 {
            width: 479px;
            vertical-align: top;
        }
        .auto-style17 {
            width: 479px;
            vertical-align: top;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <table  class="titNoticia">
            <tr>
                <td class="auto-style35"><h1><img alt="" src="img/vineta.gif" />Administración división Ventas</h1>
                 </td>
            </tr>
        </table>
    </div>
        <table class="auto-style6">
            <tr>
                <td class="auto-style2">
                    <asp:GridView ID="Lst_division" runat="server" AutoGenerateSelectButton="True" Caption="División" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="Lst_division_SelectedIndexChanged" ShowFooter="True" Width="462px">
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
                </td>
                <td class="auto-style7">
                    <asp:GridView ID="LstCategorias" runat="server" Caption="Categorías" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="547px" AutoGenerateSelectButton="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_SelCat" runat="server" />
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
                </td>
                <td class="auto-style17">
                    <asp:GridView ID="LstSubCategorias" runat="server" Caption="Sub Categorias" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="566px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_selSubCot" runat="server" />
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
                </td>
            </tr>
        </table>
        <asp:Label ID="lbl_error" runat="server"></asp:Label>
        <asp:Label ID="lbl_status" runat="server"></asp:Label>
    </form>
</body>
</html>
