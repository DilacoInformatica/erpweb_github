<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_division_ventas.aspx.cs" Inherits="erpweb.Adm_division_ventas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administración División Ventas</title>
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
        .auto-style18 {
            width: 461px;
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
        .auto-style21 {
            width: 559px;
        }
        .auto-style22 {
            width: 1355px;
            height: 18px;
        }
        .auto-style23 {
            width: 574px;
        }
        .auto-style24 {
            width: 1111px;
            height: 18px;
        }
        .auto-style25 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 13px;
            font-weight: bold;
            color: #000033;
            width: 1587px;
        }
        .auto-style26 {
            height: 1276px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style26">
    <div>
          <table class="titNoticia">
            <tr>
                <td class="auto-style23"><h1><img alt="" src="img/vineta.gif" />Administración división Ventas </h1>
                 </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td class="auto-style18">
                    <div class="scrolltddiv"> 
                    <asp:GridView ID="Lst_division" runat="server" AutoGenerateSelectButton="True" Caption="División" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="Lst_division_SelectedIndexChanged" ShowFooter="True" Width="473px" >
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <PagerTemplate>
                            &nbsp;<asp:Label ID="lbl_total_familias" runat="server"></asp:Label>
                        </PagerTemplate>
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    </div>
                </td>
                <td class="auto-style21">
                    <div class="scrolltdcat">
                    <asp:GridView ID="LstCategorias" runat="server" Caption="Categorías" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="473px" AutoGenerateSelectButton="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged">
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

                    </div>
                </td>
                <td>
                    <div class="scrolltdsubcat">
                    <asp:GridView ID="LstSubCategorias" runat="server" Caption="Sub Categorias" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="636px" AutoGenerateSelectButton="True" Height="126px" OnSelectedIndexChanged="LstSubCategorias_SelectedIndexChanged">
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
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_familia" runat="server"></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lbl_categoria" runat="server"></asp:Label>
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lbl_subcategoria" runat="server"></asp:Label>
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                </td>
            </tr>
        </table>
    </div>
        
        <asp:Label ID="lbl_error" runat="server"></asp:Label>
        <asp:Label ID="lbl_status" runat="server"></asp:Label>
    <table class="auto-style25">
        <tr>
            <td class="BottomTabla" colspan="2"><img alt="" src="img/vineta.gif" />Artículos</td>
        </tr>
        <tr>
            <td class="auto-style24"></td>
            <td class="auto-style22"></td>
        </tr>
    </table>
        <asp:GridView ID="LstItems" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1007px">
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
    </form>
    </body>
</html>
