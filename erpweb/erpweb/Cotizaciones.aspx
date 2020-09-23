<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="erpweb.Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">


 .Estilo_titulo {
            font-family: Arial;
        }


    .BottomTabla {
	background-image: url('img/tabla/fdo_header_bot.jpg');
	background-color: #C2DCF5;
	height:24px;
}
        .auto-style34 {
            width: 141px;
        }
        
input[type=text], input[type=password] {
	height: 17px;
}
        .auto-style32 {
            width: 90px;
        }
        input[type=button], input[type=submit] {
	cursor:pointer;
}

        .auto-style8 {
            width: 61px;
            text-align: center;
        }
        .auto-style35 {
            border-collapse: collapse;
        }
        .auto-style36 {
            width: 659px;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table  class="titNoticia">
            <tr>
                <td class="auto-style36"><h1><img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Cotizaciones generadas en Sitio Web&nbsp;</span></h1>
               </td>
            </tr>
        </table>
          <table class="auto-style35">
            <tr class="BottomTabla">
                <td colspan="6"><strong>Búsqueda de Información</strong></td>
            </tr>
            <tr>
                <td class="auto-style34">N° Cotización</td>
                <td>
                    <asp:TextBox ID="txt_cotizacion" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                </td>
                <td class="auto-style32">
                    Rut Cliente</td>
                <td class="auto-style32">
                    <asp:TextBox ID="txt_rut" runat="server" Width="121px" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click"/>
                </td>
                <td class="auto-style8">
                    <asp:ImageButton ID="ImgBtn_Cerrar" runat="server" Height="25px" ImageUrl="~/img/cerrar.png" Width="25px" />
                </td>
            </tr>
            
        </table>



                
    </div>
                        <asp:Label ID="lbl_cantidad" runat="server"></asp:Label>
            <asp:Label ID="lbl_error" runat="server"></asp:Label>
        <asp:GridView ID="Lista_cotizacion" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_cotizacion_SelectedIndexChanged">
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
