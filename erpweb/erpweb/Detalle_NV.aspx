<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_NV.aspx.cs" Inherits="erpweb.Detalle_NV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle Nota de Venta</title>
    <link href="css/estilos.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 66%;
        }
        .auto-style2 {
            width: 162px;
        }
        .auto-style3 {
            width: 194px;
        }
        .auto-style4 {
            width: 132px;
        }
        .auto-style5 {
            width: 162px;
            height: 15px;
        }
        .auto-style6 {
            width: 194px;
            height: 15px;
        }
        .auto-style7 {
            width: 132px;
            height: 15px;
        }
        .auto-style8 {
            height: 15px;
        }
        .auto-style9 {
            width: 162px;
            height: 14px;
        }
        .auto-style12 {
            height: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <table  class="titNoticia">
            <tr>
                <td><h1><img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Detalle Nota de Venta </span></h1>
                 </td>
            </tr>
        </table>
          <asp:Label ID="lbl_status" runat="server"></asp:Label>
          <asp:Label ID="lbl_error" runat="server"></asp:Label>
       <br />
    </div>
        <table class="auto-style1">
            <tr class="BottomTabla">
                <td class="auto-style8" colspan="4">Cabecera Nota de Venta</td>
            </tr>
            <tr>
                <td class="auto-style5">Numero</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_numero" runat="server"></asp:Label>
                </td>
                <td class="auto-style7">Fecha</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_fecha" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Cliente</td>
                <td class="auto-style8" colspan="3">
                    <asp:Label ID="lbl_cliente" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Rut</td>
                <td class="auto-style3">
                    <asp:Label ID="lbl_rut" runat="server"></asp:Label>
                </td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">Teléfono</td>
                <td class="auto-style12" colspan="2">
                    <asp:Label ID="lbl_fono" runat="server"></asp:Label>
                </td>
                <td class="auto-style12"></td>
            </tr>
            <tr>
                <td class="auto-style9">Email</td>
                <td class="auto-style12" colspan="2">
                    <asp:Label ID="lbl_email" runat="server"></asp:Label>
                </td>
                <td class="auto-style12"></td>
            </tr>
            <tr>
                <td class="auto-style5">Dirección</td>
                <td class="auto-style8" colspan="3">
                    <asp:Label ID="lbl_direccion" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Comuna</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_comuna" runat="server"></asp:Label>
                </td>
                <td class="auto-style7">Ciudad</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_ciudad" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Región</td>
                <td class="auto-style8" colspan="3">
                    <asp:Label ID="lbl_region" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" Caption="Detalle de la Nota de Venta" CellPadding="4" ForeColor="#333333" GridLines="None">
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
        <br />

    </form>
</body>
</html>
