<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="erpweb.Clientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/estilos.css" rel="stylesheet" />
    <title>Clientes</title>
    <style type="text/css">
        .nuevoEstilo1 {
            font-family: Arial, Helvetica, sans-serif;
        }
        .auto-style3 {
            width: 65%;
        }
        .auto-style5 {
            width: 100%;
            height: 354px;
            overflow: scroll;
        }
        .auto-style6 {
            width: 100%;
            height: 267px;
            overflow: scroll;
        }
        .nuevoEstilo2 {
            font-family: Arial;
        }
        .auto-style7 {
            width: 160px;
        }
        .auto-style8 {
            width: 63px;
        }
        .auto-style9 {
            width: 61%;
        }
    </style>
    <script>
        function valida()
        {
            if (document.getElementById("txt_id").value == '' &&  document.getElementById("txt_rut").value == '' && document.getElementById("txt_razon").value == '')
            {
                alert('Ingrese criterio de búsqueda');
                document.getElementById("txt_id").focus();
                return false;
            }
        }
    </script>
</head>
<body style="height: 875px">
    <form id="form1" runat="server">
    <div>
        <h1><img alt=""  src="img/vineta.gif" /><span class="nuevoEstilo2">Administración de Clientes Sitio Web</span></h1></div>
        <asp:Label ID="lbl_error" runat="server"></asp:Label>
        <br />
        <table class="auto-style9">
            <tr class="BottomTabla">
                <td colspan="7">Búsqueda de Clientes en Sitio Web</td>
            </tr>
            <tr>
                <td>Id</td>
                <td class="auto-style7">
                    <asp:TextBox ID="txt_idw" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                </td>
                <td>
                    Rut</td>
                <td>
                    <asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC" Height="17px" Width="128px"></asp:TextBox>
                </td>
                <td>
                    Razón Social</td>
                <td>
                    <asp:TextBox ID="txt_razonw" runat="server" Width="375px" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:Button ID="Btn_buscarw" runat="server" Text="Buscar" OnClick="Btn_buscarw_Click"/>
                </td>
            </tr>
            
        </table>
        <br />
        <br />
        <div class="auto-style5">
            <asp:GridView ID="lista_clientes" runat="server" Caption="Clientes Activos en el Sitio Web" CellPadding="4" ForeColor="#333333" GridLines="None" Height="144px" Width="1376px">
                <AlternatingRowStyle BackColor="White" BorderStyle="None" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_elimina" runat="server" />
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
            <br />
            <asp:Label ID="lbl_mensaje" runat="server"></asp:Label>
        </div>
        <asp:Label ID="lbl_status" runat="server"></asp:Label>
        <br />
        <asp:Button ID="Btn_eliminaCLIWEB" runat="server" OnClick="Btn_eliminaCLIWEB_Click" Text="Eliminar Cliente(s) del Sitio Web" />
        <br />
        <br />
        <br />
        <table class="auto-style9">
            <tr class="BottomTabla">
                <td colspan="7">Búsqueda de Clientes en ERP</td>
            </tr>
            <tr>
                <td>Id</td>
                <td class="auto-style7">
                    <asp:TextBox ID="txt_id" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                </td>
                <td>
                    Rut</td>
                <td>
                    <asp:TextBox ID="txt_rut" runat="server" BackColor="#FFFFCC" Height="17px" Width="128px"></asp:TextBox>
                </td>
                <td>
                    Razón Social</td>
                <td>
                    <asp:TextBox ID="txt_razon" runat="server" Width="375px" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" OnClick="Btn_buscar_Click" />
                </td>
            </tr>
            </table>
        <br />
        <div class="auto-style6">
            <asp:GridView ID="ClientesERP" runat="server" Caption="Clientes ERP" CellPadding="4" ForeColor="#333333" GridLines="None" Height="137px" Width="1376px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="check_selcli" runat="server" />
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
            <asp:Label ID="lbl_resultados" runat="server"></asp:Label>
        </div>
        <p>
        </p><asp:Button ID="Btn_cargarCliERP" runat="server" Text="Cargar Cliente(s) al Sitio Web" OnClick="Btn_cargarCliERP_Click" Visible="False" />
     
    </form>
</body>
</html>
