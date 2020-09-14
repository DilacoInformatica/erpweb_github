<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Items_ppal.aspx.cs" Inherits="erpweb.Ppal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="css/estilos.css" rel="stylesheet" />
    <title>Productos</title>
    <style type="text/css">
        .auto-style3 {
            width: 177px;
        }
        .auto-style4 {
            width: 91px;
        }
        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            height: 15px;
        }
        .auto-style10 {
            width: 1094px;
        }
        .nuevoEstilo1 {
            font-family: Arial;
        }
        .auto-style11 {
            width: 177px;
            text-align: right;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table  class="titNoticia">
            <tr>
                <td ><h1 class="auto-style10" ><img alt="" src="img/vineta.gif" /><span class="nuevoEstilo1">Listado Productos publicados Sitio Web</span></h1> </td>
            </tr>
        </table>
    </div>
        <div>
            <table>
                <tr class="BottomTabla">
                    <td colspan ="5">
                        <strong>Búsqueda Información:</strong></td>
                    
                </tr>
                
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Label2" runat="server" Text="Producto : "></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txt_codigo" runat="server" BackColor="#FFFFCC"></asp:TextBox>
                    </td>
                    <td class="auto-style11">
                        <asp:Button ID="Btn_buscar" runat="server" OnClick="Btn_buscar_Click" Text="Buscar" Width="140px" />
                    </td>
                    <td class="auto-style11">
                        <asp:Button ID="Btn_Transpaso_Masivo" runat="server" Text="Transpaso Masivo" OnClick="Btn_Transpaso_Masivo_Click" />
                    </td>
                    <td class="auto-style11">
                        <asp:Button ID="Btn_Cerrar" runat="server" Text="Cerrar Pantalla" OnClick="Btn_Cerrar_Click1" />
                    </td>
                </tr>
                
                </table>
        </div>
        <div>


            <table class="auto-style5">
                <tr>
                    <td class="auto-style6"></td>
                </tr>
                </table>


        </div>
        <p>
            <asp:Label ID="lbl_error" runat="server"></asp:Label>
            <asp:ImageButton ID="Excel" runat="server" ImageUrl="~/img/xls.gif" OnClick="Excel_Click" />
        </p>

        <div id="resultado" style="display:block">
            <asp:GridView ID="GridResultados" runat="server" Caption="Resultados" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
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
            <br />
        </div>
        <asp:GridView ID="Productos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="Productos_SelectedIndexChanged" AllowSorting="True" OnSorting="Productos_Sorting">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField SelectText="Ver" ShowSelectButton="True" />
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
    </form>
</body>
</html>

<script>
function myFunction() {
    var x = document.getElementById("resultado");
  if (x.style.display === "none") {
    x.style.display = "block";
  } else {
    x.style.display = "none";
  }
}

function salir()
{
    if (confirm('Cerrar página, Seguro desea proceder?'))
    { window.close();}
}
</script>
