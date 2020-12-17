<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lista_exportacion_items_web.aspx.cs" Inherits="erpweb.Lista_exportacion_items_web" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/estilos.css" rel="stylesheet" />
    <title>Lista Importación Web</title>
    <style type="text/css">
        .auto-style1 {
            width: 66%;
        }
        .auto-style3 {
            height: 15px;
        }
        .auto-style5 {
            width: 196px;
            height: 12px;
        }
        .auto-style6 {
            height: 12px;
        }
        .auto-style7 {
            height: 15px;
        }
        .auto-style8 {
            height: 12px;
            width: 156px;
        }
        .auto-style10 {
            width: 196px;
            height: 24px;
        }
        .auto-style11 {
            width: 156px;
            height: 24px;
        }
        .auto-style12 {
            height: 24px;
        }
        .auto-style13 {
            width: 397px;
            height: 12px;
        }
        .auto-style15 {
            width: 397px;
            height: 24px;
        }
        .auto-style16 {
            width: 196px;
            height: 22px;
        }
        .auto-style17 {
            width: 397px;
            height: 22px;
        }
        .auto-style18 {
            width: 268px;
            height: 22px;
        }
        .auto-style19 {
            height: 22px;
        }
        .nuevoEstilo1 {
            font-family: Arial;
        }
        .auto-style20 {
            width: 156px;
            height: 22px;
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
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td>&nbsp;<h1><img alt=""  src="img/vineta.gif" />Lista Export<span class="nuevoEstilo2">ación Web <span class="auto-style18">(usuario conectado:</span><asp:Label ID="lbl_usuario" runat="server" CssClass="auto-style18"></asp:Label>
            )</span></h1></td>
            </tr>
            </table>
    <br />

    </div>
        <table class="auto-style33">
            <tr>
                <td class="auto-style7" colspan="4"><strong><span class="nuevoEstilo1"><img alt="" src="img/vineta.gif" /></span>Búsqueda de Información</strong></td>
            </tr>
            <tr>
                <td class="auto-style3">Por palabra Clave</td>
                <td class="auto-style3" colspan="3">
                    <asp:TextBox ID="Txt_Claves" runat="server" Width="379px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Por línea de Venta</td>
                <td class="auto-style13">
                    <asp:DropDownList ID="Lst_LV" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10">Por Letra</td>
                <td class="auto-style15">
                    <asp:DropDownList ID="Lst_Letra" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style11">Por División</td>
                <td class="auto-style12">
                    <asp:DropDownList ID="Lst_division" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="Lst_division_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">Por Categoría</td>
                <td class="auto-style15">
                    <asp:DropDownList ID="Lst_Cat" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="Lst_Cat_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style11">Por SubCategría</td>
                <td class="auto-style12">
                    <asp:DropDownList ID="Lst_SubCat" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">Por Código Dilaco</td>
                <td class="auto-style15">
                    <asp:TextBox ID="Txt_Codigo" runat="server" Width="158px"></asp:TextBox>
                </td>
                <td class="auto-style11">Por Proveedor</td>
                <td class="auto-style12">
                    <asp:DropDownList ID="Lst_Prov" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">Por Código Prov.</td>
                <td class="auto-style15">
                    <asp:TextBox ID="Txt_CodigoProv" runat="server" Width="158px"></asp:TextBox>
                </td>
                <td class="auto-style11">Artículos SIN Categoría</td>
                <td class="auto-style12">
                    <asp:CheckBox ID="Chk_sin_cat" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style16">Artículos Publicados</td>
                <td class="auto-style17">
                    <asp:CheckBox ID="Chk_publicados" runat="server" />
                </td>
                <td class="auto-style20"></td>
                <td class="auto-style19"></td>
            </tr>
            <tr>
                <td class="auto-style34">
                    <asp:Button ID="Buscar" class="boton" runat="server" Text="Buscar" Width="154px" OnClick="Buscar_Click" />
                </td>
                <td colspan="3" class="auto-style34">
                    </td>
            </tr>
        </table>
    </form>
</body>
</html>
