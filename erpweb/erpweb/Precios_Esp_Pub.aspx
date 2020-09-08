<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Precios_Esp_Pub.aspx.cs" Inherits="erpweb.Precios_Esp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="css/estilos.css" rel="stylesheet" />
    <title>Publicar precios especiales a Clientes en el Sitio Web</title>
    <style type="text/css">

        .auto-style2 {
            width: 100%;
            height: 238px;
            overflow: scroll;
        }
        .auto-style3 {
            width: 17px;
            height: 20px;
        }
        .nuevoEstilo1 {
            font-family: Arial;
        }
        .auto-style7 {
            width: 160px;
        }
        .auto-style8 {
            width: 63px;
        }
        .auto-style9 {
            width: 1449px;
        }
        .auto-style10 {
            width: 1449px;
        }
        .auto-style11 {
            width: 136px;
        }
        .auto-style13 {
            width: 136px;
            height: 15px;
        }
        .auto-style14 {
            width: 147px;
            height: 15px;
        }
        .auto-style15 {
            height: 15px;
        }
        .auto-style16 {
            height: 15px;
            width: 279px;
        }
        .auto-style17 {
            width: 279px;
        }
        .auto-style18 {
            height: 15px;
            width: 1280px;
        }
        .auto-style19 {
            width: 1280px;
        }
        .auto-style22 {
            height: 15px;
            width: 462px;
        }
        .auto-style23 {
            width: 462px;
        }
        .auto-style24 {
            height: 15px;
            width: 80px;
        }
        .auto-style26 {
            height: 15px;
            width: 1112px;
        }
        .auto-style27 {
            width: 1112px;
        }
        .auto-style28 {
            width: 80px;
        }
        .auto-style29 {
            width: 100%;
            height: 234px;
            overflow: scroll;
        }
        .auto-style30 {
            width: 147px;
        }
        .auto-style31 {
            width: 152px;
        }
        .auto-style32 {
            width: 90px;
        }
        .auto-style33 {
            height: 783px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style33">
    <div>
    
       <h1>
           <em>
           <img alt="" class="auto-style3" src="img/vineta.gif" /><span class="Estilo_titulo">Publicar precios especiales a Clientes ERP en Sitio WEB</span></h1>
        <table class="auto-style9">
            <tr class="BottomTabla">
                <td colspan="7"><strong>Búsqueda de Clientes con Precios Especiales</strong></td>
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
                <td class="auto-style8">
                    <asp:Button ID="Btn_buscarw" runat="server" Text="Buscar" OnClick="Btn_buscarw_Click" Width="91px"/>
                </td>
            </tr>
            
        </table>
        </div>
        <br />
        <div class="auto-style2">


        <asp:GridView ID="lista" runat="server" Caption="Clientes con Productos con precios especiales en el ERP" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1449px" AutoGenerateSelectButton="True" OnSelectedIndexChanged="lista_SelectedIndexChanged">
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
        <p><strong>Cliente Seleccionado</strong></p>
        <table class="auto-style10">
            <tr class="BottomTabla">
                <td class="auto-style13">
                    Id</td>
                <td class="auto-style14">
                    Rut</td>
                <td class="auto-style26">
                    Razón Social</td>
                <td class="auto-style15">
                    Teléfono</td>
                <td class="auto-style24">
                    Teléfono2</td>
                <td class="auto-style18">
                    <br />
                    Dirección</td>
                <td class="auto-style22">
                    Ciudad</td>
                <td class="auto-style16">
                    Comuna</td>
                <td class="auto-style15">
                    Región</td>
                <td class="auto-style15">
                    Email</td>
            </tr>
            <tr>
                <td class="auto-style11">
                   <strong><asp:Label ID="lbl_id" runat="server"></asp:Label></strong> 
                </td>
                <td class="auto-style30">
                    <strong><asp:Label ID="lbl_rut" runat="server"></asp:Label>
                        <asp:Label ID="lbl_dv" runat="server" Visible="False"></asp:Label>

                    </strong> 
                </td>
                <td class="auto-style27">
                    <strong><asp:Label ID="lbl_razon" runat="server"></asp:Label></strong> 
                </td>
                <td>
                    <strong><asp:Label ID="lbl_fono" runat="server"></asp:Label></strong>
                </td>
                <td class="auto-style28">
                    <strong><asp:Label ID="lbl_fono2" runat="server"></asp:Label></strong>
                </td>
                <td class="auto-style19">
                    <strong><asp:Label ID="lbl_direccion" runat="server"></asp:Label></strong>
                </td>
                <td class="auto-style23">
                    <strong><asp:Label ID="lbl_ciudad" runat="server"></asp:Label></strong>
                </td>
                <td class="auto-style17">
                    <strong><asp:Label ID="lbl_comuna" runat="server"></asp:Label></strong>
                </td>
                <td>
                    <strong><asp:Label ID="lbl_región" runat="server"></asp:Label></strong>
                </td>
                <td>
                    <strong><asp:Label ID="lbl_email" runat="server"></asp:Label></strong>
                </td>
            </tr>
        </table>
        <br />
        <div class="auto-style29">


            <asp:GridView ID="List_ProdEsp" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1449px" Caption="Productos con precios especiales asociados al Cliente">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_selecciona" runat="server" />
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


        </div>
        <asp:Button ID="Btn_cargar" runat="server" OnClick="Btn_cargar_Click" Text="Cargar Cliente y Productos Seleccionado(s) al Sitio Web" />
        <asp:Label ID="lbl_status" runat="server"></asp:Label>
        <asp:Label ID="lbl_error" runat="server"></asp:Label>
        </em>
    </form>
</body>
</html>
