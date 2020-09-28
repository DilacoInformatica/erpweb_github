<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="erpweb.Stock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Stock de Productos en la Web</title>
    <link href="css/estilos.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style4 {
            width: 365px;
        }
        .auto-style5 {
            width: 92px;
        }
        .auto-style6 {
            width: 107px;
        }
        .nuevoEstilo1 {
            font-family: Arial;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1><img alt="" src="img/vineta.gif" /><span class="nuevoEstilo1">Stock&nbsp; de Productos en Sitio Web</span></h1></div>
        <asp:Label ID="lbl_error" runat="server"></asp:Label>
        <br />
        <table >
            <tr class="BottomTabla">
                <td colspan="3">Producto Seleccionado</td>
            </tr>
            <tr>
                <td>Codigo</td>
                <td>
                    <asp:Label ID="lbl_producto" runat="server"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="lbl_descripcion" runat="server" Text="lbl_descripcion"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Stock</td>
                <td>
                    <asp:Label ID="lbl_stock" runat="server"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="lbl_id_item" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Stock a Ingresar</td>
                <td>
                    <asp:TextBox ID="txt_stock" runat="server" OnTextChanged="txt_stock_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" OnClick="btn_actualizar_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lbl_status" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:GridView ID="Grilla" runat="server" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="Grilla_SelectedIndexChanged">
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
        <asp:Label ID="lbl_mensaje" runat="server"></asp:Label>
    </form>
</body>
</html>
