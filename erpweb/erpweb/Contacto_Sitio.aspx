<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contacto_Sitio.aspx.cs" Inherits="erpweb.Contacto_Sitio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Contactos Sitio Web</title>
    <link href="css/estilos.css" rel="stylesheet" />
    <script>
        function valida()
        {
            if (document.getElementById("txt_nv").value == '' &&  document.getElementById("txt_rut").value == '')
            {
                alert('Ingrese criterio de búsqueda');
                document.getElementById("txt_id").focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
      <table  class="titNoticia">
            <tr>
                <td class="auto-style35"><h1><img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Contactos Sitio Web</span></h1>
                 </td>
            </tr>
        </table>
    
    </div>
                <table class="auto-style9">
            <tr class="BottomTabla">
                <td colspan="4"><strong>Búsqueda</strong></td>
            </tr>
            <tr>
                <td class="auto-style34">N° Consulta</td>
                <td>
                    <asp:TextBox ID="txt_numero" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                </td>
                <td class="auto-style32">
                    <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click"/>
                </td>
                <td class="auto-style32">
                    <asp:ImageButton ID="ImgBtn_Cerrar" runat="server" Height="25px" ImageUrl="~/img/cerrar.png" Width="25px" />
                </td>
            </tr>
            
        </table>
        <asp:Label ID="lbl_cantidad" runat="server"></asp:Label>
        <asp:Label ID="lbl_error" runat="server"></asp:Label>
        <asp:GridView ID="Lista_contacto" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_contacto_SelectedIndexChanged" ShowFooter="True" HorizontalAlign="Left" OnRowDataBound="Lista_contacto_RowDataBound">
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
<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>