<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notas_Venta.aspx.cs" Inherits="erpweb.Notas_Venta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administración Notas de Venta generadas en el Sitio Web</title>
    <link href="css/estilos.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style32 {
            width: 90px;
        }
        .auto-style8 {
            width: 61px;
        }
        .auto-style34 {
            width: 141px;
        }
        </style>
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
                <td><h1><img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Notas de Venta generadas en Sitio Web</span></h1>
                 </td>
            </tr>
        </table>
       <br />
                <table class="auto-style9">
            <tr class="BottomTabla">
                <td colspan="5"><strong>Búsqueda de Clientes con Precios Especiales</strong></td>
            </tr>
            <tr>
                <td class="auto-style34">Nota de Venta</td>
                <td>
                    <asp:TextBox ID="txt_nv" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                </td>
                <td class="auto-style32">
                    Rut Cliente</td>
                <td>
                    <asp:TextBox ID="txt_rut" runat="server" Width="121px" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click"/>
                </td>
            </tr>
            
        </table>
         <p>
             <asp:Label ID="lbl_error" runat="server"></asp:Label>
        </p>
    <p>
        <asp:GridView ID="Lista_notas" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_notas_SelectedIndexChanged" ShowFooter="True" HorizontalAlign="Justify" OnRowDataBound="Lista_notas_RowDataBound">
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
        </p>
    </div>
    </form>
   
</body>
</html>
