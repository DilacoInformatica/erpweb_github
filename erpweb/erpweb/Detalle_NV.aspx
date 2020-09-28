<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_NV.aspx.cs" Inherits="erpweb.Detalle_NV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle Nota de Venta</title>
    <link href="css/estilos.css" rel="stylesheet" />
    

    <style type="text/css">
        .auto-style1 {
            width: 1254px;
        }
        .auto-style2 {
            width: 105px;
        }
        .auto-style3 {
            width: 331px;
        }
        .auto-style5 {
            height: 28px;
        }
        .auto-style6 {
            width: 331px;
            height: 28px;
        }
        .auto-style8 {
            width: 1254px;
            height: 28px;
        }
    </style>
    

</head>
<body>
    <form id="form1" runat="server" class="auto-style21">
    <div>
          <table  class="titNoticia">
            <tr>
                <td><h1><img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Detalle Nota de Venta WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label>
                    &nbsp;<asp:Button ID="Btn_volver" runat="server" OnClick="Btn_volver_Click" Text="Volver" />
                    </span></h1>
                 </td>
            </tr>
        </table>
          <asp:Label ID="lbl_status" runat="server"></asp:Label>
          <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    <asp:Label ID="lbl_id_nv" runat="server" Visible="False"></asp:Label>
       <br />
    </div>
        <table class="auto-style1">
            <tr class="BottomTabla">
                <td class="auto-style8" colspan="5"><h4><strong>Cabecera Nota de Venta</strong></h4></td>
            </tr>
            <tr>
                <td>Numero NV ERP</td>
                <td class="auto-style3">
                    <span class="Estilo_titulo"><strong>
                    <asp:Label ID="lbl_numero_erp" runat="server"></asp:Label>
                    </strong> </span>
                </td>
                <td class="auto-style3">
                    Fecha</td>
                <td class="auto-style3">
                    <asp:Label ID="lbl_fecha" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>N° OC</td>
                <td class="auto-style3">
                    <asp:Label ID="lbl_n_oc" runat="server"></asp:Label>
                </td>
                <td class="auto-style3">
                    N° Transaccion WebPay</td>
                <td class="auto-style3">
                    <strong>
                    <asp:Label ID="lbl_transac_pago" runat="server"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td>Tipo Facturación</td>
                <td class="auto-style6">
                     <strong>
                     <asp:Label ID="lbl_tipo_facturacion" runat="server"></asp:Label>
                     </strong>
                </td>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style6">
                    &nbsp;</td>
            </tr>
            <tr class="BottomTabla">
                <td class="auto-style35" colspan="5"><h4 class="auto-style37"><strong>Información Cliente:</strong></h4></td>
            </tr>
            <tr>
                <td>Cliente</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_cliente" runat="server"></asp:Label>
                </td>
                <td class="auto-style8">
                    Existe en ERP</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_existe" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Rut</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_rut" runat="server"></asp:Label>
                    <asp:Label ID="lbl_rut_exit" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">Teléfono</td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_fono" runat="server"></asp:Label>
                </td>
                <td class="auto-style12">
                    Email</td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_email" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Dirección</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_direccion" runat="server"></asp:Label>
                </td>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style8">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Comuna</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_comuna" runat="server"></asp:Label>
                </td>
                <td class="auto-style6">
                    Ciudad</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_ciudad" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Región</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_region" runat="server"></asp:Label>
                </td>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style8">
                    &nbsp;</td>
            </tr>
            <tr class="BottomTabla">
                <td class="auto-style13" colspan="5"><h4><strong>Información de Despacho:</strong></h4></td>
            </tr>
            <tr>
                <td class="auto-style2">Contacto</td>
                <td>
                    <asp:Label ID="lbl_contacto" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">Teléfono</td>
                <td class="auto-style18">
                    <asp:Label ID="lbl_fono_despacho" runat="server"></asp:Label>
                </td>
                <td class="auto-style18">
                    Email</td>
                <td class="auto-style18">
                    <asp:Label ID="lbl_email_contacto" runat="server"></asp:Label>
                </td>
                <td class="auto-style18">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Dirección</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_direccion_despacho" runat="server"></asp:Label>
                </td>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style8">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style14">Comuna</td>
                <td class="auto-style15">
                    <asp:Label ID="lbl_comuna_despacho" runat="server"></asp:Label>
                </td>
                <td class="auto-style15">
                    Ciudad</td>
                <td class="auto-style15">
                    <asp:Label ID="lbl_ciudad_despacho" runat="server"></asp:Label>
                </td>
                <td class="auto-style15">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Región</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_region_despacho" runat="server"></asp:Label>
                </td>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style8">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Observaciones</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_obs_despacho" runat="server"></asp:Label>
                </td>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style8">
                    &nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="lista_detalles" runat="server" Caption="Detalle de la Nota de Venta" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1265px" ShowFooter="True">
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
<table class="auto-style1">
            <tr>
                <td class="auto-style31">Vendedor/Emisor</td>
                <td class="auto-style32">
                    <asp:DropDownList ID="Lista_Vendedores" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style33">
                    <asp:Button ID="Btn_crearNV" runat="server" OnClick="Btn_crearNV_Click" Text="Crear NV en ERP" />
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
                <td class="auto-style24">
                    &nbsp;</td>
                <td class="auto-style26"><strong>Iva</strong></td>
                <td class="auto-style28">
                    <asp:Label ID="lbl_tax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style24">
                    &nbsp;</td>
                <td class="auto-style26"><strong>Total</strong></td>
                <td class="auto-style28">
                    <asp:Label ID="lbl_total" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />

    </form>
</body>
</html>
