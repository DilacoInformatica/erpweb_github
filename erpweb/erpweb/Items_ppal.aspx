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
        .auto-style10 {
            width: 1094px;
        }
        .nuevoEstilo1 {
            font-family: Arial;
        }
        .auto-style11 {
            width: 35px;
            text-align: center;
        }
        .auto-style12 {
            height: 21px;
        }
        .auto-style14 {
            height: 21px;
            width: 254px;
        }
        .auto-style16 {
            width: 254px;
        }
        .auto-style20 {
            height: 28px;
        }
        .auto-style21 {
            width: 254px;
            height: 28px;
        }
        .auto-style28 {
            height: 24px;
        }
        .auto-style29 {
            width: 254px;
            height: 24px;
        }
        .auto-style31 {
            height: 19px;
        }
        .auto-style33 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            background-color: #FFFFF9;
            border: 1px solid #C0C0C0;
            margin: 0px;
            border-collapse: collapse;
            border-spacing: 0;
            padding: 0px;
            height: 264px;
            width: 99%;
        }
        .auto-style34 {
            width: 1198px;
        }
        .auto-style36 {
            height: 21px;
            width: 477px;
        }
        .auto-style37 {
            width: 477px;
        }
        .auto-style38 {
            width: 477px;
            height: 24px;
        }
        .auto-style40 {
            height: 21px;
            width: 2076px;
        }
        .auto-style42 {
            width: 2076px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <div>
        <table  class="titNoticia">
            <tr>
                <td class="auto-style34" ><h1 class="auto-style10" ><img alt="" src="img/vineta.gif" /><span class="nuevoEstilo1">Listado Productos publicados Sitio Web<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    </h1> 
                    
                        
                            <table border="0" cellpadding="2" cellspacing="0" class="auto-style33">
                                <tr class="HeadTabla">
                                    <td class="auto-style31" colspan="3"><strong><img alt="" src="img/vineta.gif" />Búsqueda Artículos </strong></td>
                                    <td class="auto-style31" colspan="2" style="text-align: right">
                                        <asp:ImageButton ID="ImgBtn_Cerrar" runat="server" Height="25px" ImageUrl="~/img/cerrar.png" style="text-align: right" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Palabra Clave</td>
                                    <td colspan="4">&nbsp;<asp:TextBox ID="txt_palabra_clave" runat="server" BackColor="#FFFFCC" Width="401px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">Division</td>
                                    <td class="auto-style12" colspan="4">
                                        <asp:DropDownList ID="LstDivision" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivision_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">Categoría</td>
                                    <td class="auto-style14">
                                        <span class="nuevoEstilo1">
                                        <asp:DropDownList ID="LstCategorias" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                        </span>
                                    </td>
                                    <td  colspan="2" class="auto-style40">SubCategoría</td>
                                    <td class="auto-style36">
                                        <span class="nuevoEstilo1">
                                        <asp:DropDownList ID="LstSubCategorias" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                        </span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">Línea de Venta</td>
                                    <td class="auto-style16">
                                        <span class="nuevoEstilo1">
                                        <asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                        </span></td>
                                    <td class="auto-style42" colspan="2">Letra</td>
                                    <td class="auto-style37">
                                        <span class="nuevoEstilo1">
                                        <asp:DropDownList ID="LstLetras" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                        </span></td>
                                </tr>
                                <tr>
                                    <td width="127">Código Dilaco </td>
                                    <td class="auto-style16">&nbsp;<asp:TextBox ID="txt_codigo" runat="server" BackColor="#FFFFCC"></asp:TextBox>
                                    </td>
                                    <td class="auto-style42" colspan="2">Código Prov. </td>
                                    <td class="auto-style37">
                                        <asp:TextBox ID="txt_codprov" runat="server" BackColor="#FFFFCC" Width="119px"></asp:TextBox>
                                        <span class="nuevoEstilo1">
                                        &nbsp;</span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style20">Proveedor</td>
                                    <td class="auto-style21">
                                        <asp:DropDownList ID="LstProveedores" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2" class="auto-style42">&nbsp;</td>
                                    <td class="auto-style37">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">Artículos SIN Categorías </td>
                                    <td class="auto-style29">
                                        <asp:CheckBox ID="chk_sin_cat" runat="server" />
                                    </td>
                                    <td class="auto-style42" colspan="2">Artículos Publicados</td>
                                    <td class="auto-style38">
                                        <asp:CheckBox ID="chk_publicados" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style28">Artículos sin Imágenes</td>
                                    <td class="auto-style29">
                                        <asp:CheckBox ID="chk_sin_imagenes" runat="server" />
                                    </td>
                                    <td class="auto-style42" colspan="2">Artículos NO Publicados</td>
                                    <td class="auto-style38">
                                        <asp:CheckBox ID="chk_no_publicados" runat="server" />
                                    </td>
                                </tr>
                                <tr class="baseTabla">
                                    <td class="baseTabla">&nbsp;</td>
                                    <td colspan="4">
                                        <asp:Button ID="Btn_buscar" runat="server" OnClick="Btn_buscar_Click" Text="Buscar" Width="140px" />
                                    </td>
                                </tr>
                            </table>
                       
                  </td>
            </tr>
        </table>
    </div>

       <asp:Label ID="lbl_error" runat="server"></asp:Label>
&nbsp;<div>




            <table>
                <tr class="BottomTabla">
                    <td colspan ="7">
                        Información global</td>
                    
                </tr>
                
                <tr>
                    <td class="auto-style4">
                        Productos Web</td>
                    <td class="auto-style3">
                        <asp:Label ID="lbl_cantidad" runat="server"></asp:Label>
                        </td>
                    <td class="auto-style3">
                        Publicados en el Sitio</td>
                    <td class="auto-style3">
                        <asp:Label ID="lbl_prod_publicados" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="Btn_Transpaso_Masivo" runat="server" Text="Transpaso Masivo" OnClick="Btn_Transpaso_Masivo_Click" />
                    </td>
                    <td class="auto-style11">
            <asp:ImageButton ID="Excel" runat="server" ImageUrl="~/img/xls.gif" OnClick="Excel_Click" />
                    </td>
                    <td class="auto-style11">
                        &nbsp;</td>
                </tr>
                </table>
        </div>

        <div id="resultado" style="display:block">
            <asp:GridView ID="GridResultados" runat="server" Caption="Resultados" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False" ShowFooter="True" ShowHeaderWhenEmpty="True">
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
        <asp:GridView ID="Productos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="Productos_SelectedIndexChanged" AllowSorting="True" OnSorting="Productos_Sorting" ShowFooter="True" ShowHeaderWhenEmpty="True">
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
        <asp:GridView ID="LstProductos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="772px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Ver"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Código" />
                <asp:BoundField HeaderText="Descripción" />
                <asp:BoundField HeaderText="Letra" />
                <asp:CheckBoxField HeaderText="A Pedido" />
                <asp:CheckBoxField HeaderText="Venta" />
                <asp:CheckBoxField HeaderText="Cotizacion" />
                <asp:BoundField HeaderText="Marca" />
                <asp:BoundField HeaderText="Pro" />
                <asp:BoundField />
                <asp:CheckBoxField HeaderText="Publicado Sitio" />
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
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>
