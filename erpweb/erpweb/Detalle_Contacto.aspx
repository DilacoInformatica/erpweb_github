<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_Contacto.aspx.cs" Inherits="erpweb.Detalle_Contacto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle Contacto Clientes</title>
    <link href="css/estilos.css" rel="stylesheet" />
    <style type="text/css">

        .auto-style15 {
            height: 24px;
        }
        .auto-style9 {
            width: 117px;
            height: 15px;
        }
        .auto-style16 {
            height: 28px;
        }
        .auto-style17 {
            width: 117px;
        }
        .auto-style18 {
            width: 1359px;
        }
        .auto-style19 {
            width: 117px;
            height: 28px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>d<img alt="" src="img/vineta.gif" /><span class="Estilo_titulo">Detalle Contacto WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label>
                    &nbsp;<asp:Button ID="Btn_volver" runat="server" OnClick="Btn_volver_Click" Text="Volver" />
                    </span></h1>
    
    </div>
        <table class="auto-style18">
            <tr class="BottomTabla">
                <td class="auto-style15" colspan="8"><h4 class="auto-style37"><strong>Información Contacto:</strong></h4></td>
            </tr>
            <tr>
                <td class="auto-style19">Nombre</td>
                <td class="auto-style16" colspan="7">
                    <strong>
                    <asp:Label ID="lbl_nombre" runat="server"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Teléfono</td>
                <td>
                    <strong>
                    <asp:Label ID="lbl_fono" runat="server"></asp:Label>
                    </strong>
                </td>
                <td>
                    Email</td>
                <td>
                    <strong>
                    <asp:Label ID="lbl_email" runat="server"></asp:Label>
                    </strong>
                </td>
                <td>
                    Celular</td>
                <td>
                    <strong>
                    <asp:Label ID="lbl_celular" runat="server"></asp:Label>
                    </strong>
                </td>
                <td>
                    Fecha</td>
                <td class="auto-style15">
                    <strong>
                    <asp:Label ID="lbl_fecha" runat="server"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="auto-style17">Comentario</td>
                <td class="auto-style16" colspan="7">
                    <asp:TextBox ID="txt_comentario" runat="server" Height="220px" Width="1172px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr class="BottomTabla">
                <td class="auto-style15" colspan="8"><h4 class="auto-style37"><strong>Respuesta Dilaco</strong></h4></td>
            </tr>
            <tr>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16" colspan="7">
                    <asp:TextBox ID="Txt_Respuesta" runat="server" Height="220px" Width="1172px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16" colspan="7">
                    <asp:Button ID="Btn_Respuesta" runat="server" Text="Enviar respuesta" OnClick="Btn_Respuesta_Click" />
                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>

</body>
</html>
