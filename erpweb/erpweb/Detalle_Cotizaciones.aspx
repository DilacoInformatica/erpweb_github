<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_Cotizaciones.aspx.cs" Inherits="erpweb.Detalle_Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/estilos.css" rel="stylesheet" />
    <title>Detalle Cotización</title>
    <style type="text/css">
        .auto-style1 {
            width: 66%;
        }
        .auto-style8 {
            height: 15px;
        }
        .auto-style6 {
            width: 243px;
            height: 15px;
        }
        .auto-style34 {
            height: 15px;
            text-align: right;
        }
        .auto-style35 {
            height: 21px;
        }
        .auto-style37 {
            margin-bottom: 0px;
        }
        .auto-style9 {
            width: 162px;
            height: 14px;
        }
        .auto-style12 {
            height: 14px;
        }
        .auto-style31 {
            width: 115px;
        }
        .auto-style32 {
            width: 608px;
        }
        .auto-style33 {
            text-align: right;
        }
        .auto-style23 {
            height: 15px;
            width: 2246px;
        }
        .auto-style27 {
            width: 588px;
            height: 15px;
        }
        .auto-style29 {
            width: 588px;
            height: 15px;
            text-align: right;
            background-color: #FFFFCC;
        }
        .auto-style24 {
            width: 2246px;
        }
        .auto-style26 {
            width: 588px;
        }
        .auto-style28 {
            width: 588px;
            text-align: right;
            background-color: #FFFFCC;
        }
        .auto-style38 {
            height: 15px;
            width: 50px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div>
            <table class="titNoticia">
                <tr>
                    <td>
                        <h1>
                            <img alt="" src="img/vineta.gif" /><span class="nuevoEstilo2">Detalle Cotización de Venta WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label>
                            &nbsp;<asp:Button ID="Btn_volver" runat="server" OnClick="Btn_volver_Click" Text="Volver" style="height: 26px" />
                            </span></h1>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbl_status" runat="server"></asp:Label>
            <asp:Label ID="lbl_error" runat="server"></asp:Label>
            <asp:Label ID="lbl_id_cot" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
            <br />
        </div>
    
    </div>
        <table class="auto-style1">
            <tr class="BottomTabla">
                <td class="auto-style8" colspan="7"><h4><strong>Cabecera Cotización</strong></h4></td>
            </tr>
            <tr>
                <td>Numero Cotización ERP</td>
                <td class="auto-style6">
                    <span class="Estilo_titulo"><strong>
                    <asp:Label ID="lbl_numero_erp" runat="server"></asp:Label>
                    </strong> </span>
                </td>
                <td class="auto-style38">Fecha</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_fecha" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    N° OC</td>
                <td class="auto-style34">
                    <asp:Label ID="lbl_n_oc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="BottomTabla">
                <td class="auto-style35" colspan="7"><h4 class="auto-style37"><strong>Información Cliente:</strong></h4></td>
            </tr>
            <tr>
                <td class="auto-style8">Rut</td>
                <td class="auto-style8" colspan="2">
                    <asp:Label ID="lbl_rut" runat="server" BorderStyle="Groove"></asp:Label>
                    <asp:Label ID="lbl_rut_exit" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style8" colspan="2">
                    Existe en ERP</td>
                <td class="auto-style8" colspan="2">
                    <asp:Label ID="lbl_existe" runat="server" BorderStyle="Groove"></asp:Label>
                    <asp:Label ID="lbl_id_cliente" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Empresa</td>
                <td class="auto-style8" colspan="6">
                    <asp:Label ID="lbl_empresa" runat="server" BorderStyle="Double" Width="791px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">Nombre</td>
                <td class="auto-style6" colspan ="2">
                    <asp:Label ID="lbl_nombre" runat="server" BorderStyle="Groove" Width="324px"></asp:Label>
                </td>
              <td  class="auto-style8" colspan="2">Apellidos</td>
                <td colspan="2" class="auto-style8">
                    <asp:Label ID="lbl_apellidos" runat="server" BorderStyle="Groove" Width="367px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Teléfono</td>
                <td colspan="2" class="auto-style12">
                    <asp:Label ID="lbl_fono" runat="server" BorderStyle="Groove" Width="157px"></asp:Label>
                </td>
                <td class="auto-style12" colspan="2">Móvil</td>
                <td class="auto-style12" colspan="2">
                    <asp:Label ID="lbl_movil" runat="server" BorderStyle="Groove" Width="157px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Email</td>
                <td colspan="2" class="auto-style12">
                    <asp:Label ID="lbl_email" runat="server" BorderStyle="Groove" Width="252px"></asp:Label>
                </td>
                <td class="auto-style12" colspan="2"></td>
                <td class="auto-style12" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Dirección</td>
                <td class="auto-style8" colspan="6">
                    <asp:Label ID="lbl_direccion" runat="server" BorderStyle="Groove" Height="47px" Width="781px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Comuna</td>
                <td class="auto-style6" colspan="2">
                    <asp:TextBox ID="txt_comuna" runat="server" BackColor="#FFFFCC" Width="252px"></asp:TextBox>
                </td>
                <td colspan="2">Ciudad</td>
                <td class="auto-style8" colspan="2">
                    <asp:Label ID="lbl_ciudad" runat="server" BorderStyle="Groove" Width="256px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Región</td>
                <td class="auto-style8" colspan="6">
                    <asp:Label ID="lbl_region" runat="server"></asp:Label>
                    <asp:DropDownList ID="Lst_Region" runat="server" AppendDataBoundItems="True" Visible="False">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            </table>
        <asp:GridView ID="lista_detalles" runat="server" Caption="Detalle de la Cotización" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1388px" ShowFooter="True" OnRowDataBound="lista_detalles_RowDataBound">
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
<table class="auto-style1">
            <tr>
                <td class="auto-style31">Vendedor/Emisor</td>
                <td class="auto-style32">
                    <asp:DropDownList ID="Lista_Vendedores" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style33">
                    <asp:Button ID="Btn_crearCot" runat="server" OnClick="Btn_crearCot_Click" Text="Crear Cotización en ERP" />
                </td>
            </tr>
        </table>
        <br />
&nbsp;<table class="auto-style1">
            <tr>
                <td class="auto-style23">
                    &nbsp;</td>
                <td class="auto-style27"><strong>Moneda</strong></td>
                <td class="auto-style29">
                    <asp:Label ID="lbl_moneda" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style23">
                    &nbsp;</td>
                <td class="auto-style27"><strong>Neto</strong></td>
                <td class="auto-style29">
                    <asp:Label ID="lbl_neto" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style23">
                    </td>
                <td class="auto-style27"><strong>Iva</strong></td>
                <td class="auto-style29">
                    <asp:Label ID="lbl_tax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style24">
                    &nbsp;</td>
                <td class="auto-style26"><strong>Total (*)</strong></td>
                <td class="auto-style28">
                    <asp:Label ID="lbl_total" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        * Valores de referencia</form>
</body>
</html>
