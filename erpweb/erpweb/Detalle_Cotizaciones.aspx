<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_Cotizaciones.aspx.cs" Inherits="erpweb.Detalle_Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/estilos.css" rel="stylesheet" />
    <title>Detalle Cotización</title>
    <style type="text/css">

        .auto-style8 {
            height: 15px;
        }
        .auto-style5 {
            width: 162px;
            height: 15px;
        }
        .auto-style6 {
            width: 243px;
            height: 15px;
        }
        .auto-style36 {
            height: 15px;
            width: 38px;
        }
        .auto-style18 {
            width: 177px;
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
        .auto-style15 {
            height: 16px;
        }
        .auto-style16 {
            width: 64px;
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
                            <img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Detalle Cotización de Venta WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label>
                            &nbsp;<asp:Button ID="Btn_volver" runat="server" OnClick="Btn_volver_Click" Text="Volver" />
                            </span></h1>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbl_status" runat="server"></asp:Label>
            <asp:Label ID="lbl_error" runat="server"></asp:Label>
            <br />
        </div>
    
    </div>
        <table class="auto-style1">
            <tr class="BottomTabla">
                <td class="auto-style8" colspan="7"><h4><strong>Cabecera Cotización</strong></h4></td>
            </tr>
            <tr>
                <td class="auto-style5">Numero Cotización ERP</td>
                <td class="auto-style6">
                    <span class="Estilo_titulo"><strong>
                    <asp:Label ID="lbl_numero_erp" runat="server"></asp:Label>
                    </strong> </span>
                </td>
                <td class="auto-style36">Fecha</td>
                <td class="auto-style8">
                    <asp:Label ID="lbl_fecha" runat="server"></asp:Label>
                </td>
                <td class="auto-style18" colspan="2">
                    N° OC</td>
                <td class="auto-style34">
                    <asp:Label ID="lbl_n_oc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="BottomTabla">
                <td class="auto-style35" colspan="7"><h4 class="auto-style37"><strong>Información Cliente:</strong></h4></td>
            </tr>
            <tr>
                <td class="auto-style5">Cliente</td>
                <td class="auto-style8" colspan="6">
                    <asp:Label ID="lbl_cliente" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Rut</td>
                <td class="auto-style6" colspan ="2">
                    <asp:Label ID="lbl_rut" runat="server"></asp:Label>
                    <asp:Label ID="lbl_rut_exit" runat="server" Visible="False"></asp:Label>
                </td>
              <td  class="auto-style8" colspan="2">Existe en ERP</td>
                <td colspan="2" class="auto-style8">
                    <asp:Label ID="lbl_existe" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Teléfono</td>
                <td colspan="2" class="auto-style12">
                    <asp:Label ID="lbl_fono" runat="server"></asp:Label>
                </td>
                <td class="auto-style12" colspan="2">Email</td>
                <td class="auto-style12" colspan="2">
                    <asp:Label ID="lbl_email" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Dirección</td>
                <td class="auto-style8" colspan="6">
                    <asp:Label ID="lbl_direccion" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Comuna</td>
                <td class="auto-style6" colspan="2">
                    <asp:Label ID="lbl_comuna" runat="server"></asp:Label>
                </td>
                <td colspan="2">Ciudad</td>
                <td class="auto-style8" colspan="2">
                    <asp:Label ID="lbl_ciudad" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Región</td>
                <td class="auto-style8" colspan="6">
                    <asp:Label ID="lbl_region" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="auto-style8">
                    Email</td>
                <td class="auto-style8" colspan="2">
                    <asp:Label ID="lbl_email_contacto" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style16" colspan="2">
                    Ciudad</td>
                <td class="auto-style15" colspan="2">
                    <asp:Label ID="lbl_ciudad_despacho" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
    </form>
</body>
</html>
