<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Precios_Esp_Adm.aspx.cs" Inherits="erpweb.Precios_Esp_Adm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administración clientes y precios especiales en sitio web</title>
    <link href="css/estilos.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 93px;
        }
        .auto-style2 {
            height: 24px;
        }

        .auto-style3 {
            width: 100%;
            height: 777px;
            overflow: scroll;
        }

        .auto-style4 {
            height: 825px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style4">
    <div>
    
       <h1>
           <img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Precios especiales de Clientes publicados en Sitio WEB</span></h1>
        <table class="auto-style9">
            <tr class="BottomTabla">
                <td colspan="8" class="auto-style2"><strong>Búsqueda de Clientes con Precios Especiales</strong></td>
            </tr>
            <tr>
                <td>Id</td>
                <td class="auto-style7">
                    <asp:TextBox ID="txt_idw" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                </td>
                <td>
                    Rut</td>
                <td class="auto-style31">
                    <asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC" Height="17px" Width="128px"></asp:TextBox>
                </td>
                <td class="auto-style32">
                    Razón Social</td>
                <td>
                    <asp:TextBox ID="txt_razonw" runat="server" Width="753px" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="89px" OnClick="Btn_buscar_Click" />
                </td>
                
                <td class="auto-style1">
                    <asp:Button ID="Btn_Nuevo" runat="server" Text="Nuevo" Width="89px" OnClick="Btn_Nuevo_Click" />
                </td>
                
            </tr>
            
        </table>
        <br />
        <asp:Label ID="lbl_status" runat="server"></asp:Label>
        <br />
        <br />
        <div class="auto-style3">

            <asp:GridView ID="Grilla" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Seleccionar">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_selecciona" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataTemplate>
                    <asp:CheckBox ID="Chk_elimina" runat="server" />
                </EmptyDataTemplate>
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

        </div>
        </div>
        <asp:Button ID="Btn_Eliminar" runat="server" Text="Eliminar Información Cliente-Precio del Sitio Web" OnClick="Btn_Eliminar_Click" />
        <asp:Label ID="lbl_error" runat="server"></asp:Label>
        <br />
    </form>
</body>
</html>
