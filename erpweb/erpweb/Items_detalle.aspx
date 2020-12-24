<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Items_detalle.aspx.cs" Inherits="erpweb.Item" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <link href="css/estilos.css" rel="stylesheet" />
    <title>Detalle de Producto</title>
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
        .auto-style4 {
            margin-left: 43px;
        }
        .auto-style6 {
            height: 19px;
            width: 149px;
        }
        .auto-style11 {
            height: 19px;
            width: 16%;
        }
        .auto-style12 {
            width: 16%;
        }
        .auto-style41 {
            width: 149px;
            height: 28px;
        }
        .auto-style42 {
            height: 28px;
        }
        .auto-style43 {
            width: 149px;
            height: 132px;
        }
        .auto-style44 {
            height: 132px;
        }
        .auto-style45 {
            height: 21px;
        }
        .auto-style47 {
            width: 852px;
            height: 21px;
        }
        .auto-style48 {
            height: 19px;
            width: 371px;
        }
        .auto-style49 {
            height: 25px;
            width: 371px;
        }
        .auto-style50 {
            width: 371px;
        }
        .auto-style51 {
            height: 24px;
        }
        .auto-style53 {
            height: 25px;
            width: 149px;
        }
        .auto-style54 {
        }
        .auto-style55 {
            height: 24px;
            width: 149px;
        }
        .auto-style56 {
            height: 17px;
            width: 149px;
        }
        .auto-style57 {
            height: 18px;
            width: 149px;
        }
        .auto-style58 {
            width: 149px;
        }
              
        }
        .auto-style59 {
            width: 268435456px;
        }
        .auto-style60 {
            height: 28px;
            width: 268435456px;
        }
        .auto-style61 {
            width: 1132px;
        }
        .auto-style62 {
            height: 18px;
            text-align: right;
        }
        .auto-style63 {
            height: 17px;
            text-align: right;
        }
        .auto-style64 {
            text-align: right;
        }
        .auto-style65 {
            height: 25px;
            text-align: right;
        }
        .auto-style66 {
            text-align: right;
            height: 19px;
        }
        .auto-style67 {
            height: 19px;
        }
        .auto-style68 {
            text-align: left;
        }
        .auto-style69 {
            text-align: left;
            height: 19px;
        }
        .auto-style70 {
            height: 18px;
            text-align: left;
        }
        .auto-style71 {
            height: 25px;
            text-align: left;
        }
        .auto-style72 {
            height: 17px;
            text-align: left;
        }
        </style>
    </head>
<body style="width: 1429px; height: 1517px">
<form id="form1" runat="server" enctype="multipart/form-data">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table  border="0">
<tr>
  <td colspan="2" class="auto-style61"><table>
    <tr>
      <td colspan="11"><table width="100%" border="0"   class="titNoticia">
        <tr class="BottomTabla">
          <td class="auto-style47">
        <img alt=""src="img/vineta.gif" />Edici&oacute;n de Art&iacute;culo (Web)</td>
          <td class="auto-style45" >
                    <asp:Button ID="BtnGrabar" runat="server" OnClick="BtnGrabar_Click" Text="Grabar" CssClass="auto-style4" Width="85px" />
                </td>
                      <td class="auto-style45" >
                    <asp:Button ID="Btn_volver" runat="server" OnClick="Btn_volver_Click" Text="Volver" />
                        <asp:ImageButton ID="ImgBtn_Cerrar" runat="server" Height="25px" ImageUrl="~/img/cerrar.png" style="text-align: right"/>
                </td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td class="auto-style6">
          <asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
        </td>
      <td colspan="6" class="auto-style48" >
        <asp:Label ID="lbl_status" runat="server" BackColor="White"></asp:Label>
        <asp:Label ID="lbl_error" runat="server" BackColor="Red"></asp:Label>
                 </td>
      <td class="auto-style11" >&nbsp;</td>
      <td colspan="3" >
                      &nbsp;</td>
    </tr>
    <tr>
      <td class="auto-style6">Registrado en Sitio</td>
      <td colspan="6" class="auto-style48" >
                    <asp:Label ID="lbl_web" runat="server"></asp:Label>
                 </td>
      <td class="auto-style11" >Visible</td>
      <td colspan="3" >
                      <asp:CheckBox ID="chck_visible" runat="server" TextAlign="Left" />
                </td>
    </tr>
    <tr>
      <td class="auto-style53">C&oacute;digo</td>
      <td colspan="6" class="auto-style49" >
          <asp:TextBox ID="txt_codigo" runat="server" Enabled="False" Width="95px"></asp:TextBox>
          <asp:ImageButton ID="ImgBtnLink" runat="server" ImageUrl="~/img/ver.gif" />
        </td>
      <td colspan="3">Venta</td>
        <td class="auto-style59">
                     <asp:CheckBox ID="chck_venta" runat="server" TextAlign="Left" />
                </td>
    </tr>
    <tr>
      <td class="auto-style58">Prod. a Pedido</td>
      <td colspan="5">
                      <asp:CheckBox ID="chck_prodped" runat="server" TextAlign="Left" />
                </td>
      <td colspan="3">Cotizaciones</td>
      <td colspan="2">
                    <asp:CheckBox ID="chck_cot" runat="server" TextAlign="Left" />
                </td>
    </tr>
    <tr>
      <td class="auto-style6" >Moneda</td>
      <td colspan="5">
          <asp:DropDownList ID="LstMonedas" runat="server" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
      <td colspan="3">Unidad Venta</td>
      <td colspan="2">
                    <asp:Label ID="lbl_unidad" runat="server"></asp:Label>
                 </td>
    </tr>
    <tr>
      <td class="auto-style6" >Precio Lista</td>
      <td colspan="5">
          <asp:TextBox ID="txt_precio_lista" runat="server"></asp:TextBox>
        </td>
      <td colspan="3">Precio</td>
      <td colspan="2">
          <asp:TextBox ID="txt_precio" runat="server" AutoPostBack="True" OnTextChanged="txt_precio_TextChanged"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Texto Destacado</td>
      <td colspan="10"><asp:TextBox ID="txt_texto_destacado" runat="server" Height="52px" Width="1272px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style53">Descripci&oacute;n<span class="asteriscoObligatorio">*</span></td>
      <td colspan="10" class="auto-style1">
          <asp:TextBox ID="txt_descripcion" runat="server" Height="52px" Width="940px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style55">Proveedor</td>
      <td colspan="10" class="auto-style51">
          <asp:TextBox ID="txt_proveedor" runat="server" Enabled="False" Width="927px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style55">División</td>
      <td colspan="10" class="auto-style61">
          <asp:DropDownList ID="LstDivision" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstDivision_SelectedIndexChanged" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
    </tr>
    <tr>
      <td class="auto-style55">Categor&iacute;a</td>
      <td colspan="6" class="auto-style51">
          <asp:DropDownList ID="LstCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
      <td class="auto-style51">
          SubCategor&iacute;a</td>
      <td colspan="3">
          <asp:DropDownList ID="LstSubCategorias" runat="server" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">L&iacute;nea de Venta</td>
      <td colspan="6">
          <asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True">
          </asp:DropDownList>
        </td>
      <td >Presentación: </td>
      <td colspan="3">
          <asp:TextBox ID="txt_unidad" runat="server" Width="185px"></asp:TextBox>
          <asp:TextBox ID="txt_multiplo" runat="server" Width="27px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Marca</td>
      <td colspan="6">
          <asp:TextBox ID="txt_marca" runat="server" Width="477px"></asp:TextBox>
        </td>
      <td class="auto-style12" >C&oacute;digo Prov.</td>
      <td colspan="3">
          <asp:TextBox ID="txt_codigoprov" runat="server" Width="207px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Caracter&iacute;sticas</td>
      <td colspan="10">
                    <asp:TextBox ID="txt_caracteristicas" runat="server" Height="102px" Width="1277px" TextMode="MultiLine"></asp:TextBox>
                 </td>
    </tr>
    <tr>

      <td colspan="11" class="BottomTabla">
                 <strong>Archivos Adjuntos</strong></td>
    </tr>
    <tr>
      <td class="auto-style6">Manual Técnico</td>
      <td class="auto-style69">
                    <asp:Label ID="lbl_manual_tecnico" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>
                 </td>
        <td class="auto-style66">
            <asp:Image ID="MT_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
        </td>
        <td class="auto-style66">
            <asp:ImageButton ID="borra_manual_tecnico" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_manual_tecnico_Click" Width="16px" />
        </td>
      <td class="auto-style67">

                    <asp:FileUpload ID="File_FT" runat="server" />

                 </td>
      <td class="auto-style67">

                    <asp:ImageButton ID="ImgBtnFT" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFT_Click" Height="16px" />

                 </td>
      <td colspan="5" rowspan="6" style="text-align: right">
                 <asp:Image ID="img_prod" runat="server" Height ="217px" Width="300px" />
                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Presentación Producto</td>
      <td class="auto-style68">
                    <asp:Label ID="lbl_presentacion" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>

                 </td>
        <td class="auto-style64">
            <asp:Image ID="PR_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="borra_presentacion" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_presentacion_Click" Width="16px" />
        </td>
      <td>

                    <asp:FileUpload ID="File_PRE" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnPRE" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnPRE_Click" Width="16px" ToolTip="Extensión" Height="16px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Foto Grande</td>
      <td class="auto-style68">
                    <asp:Label ID="lbl_fotog" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>

                 </td>
        <td class="auto-style64">
            <asp:Image ID="FG_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="borra_fotog" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_fotog_Click" Width="16px" />
        </td>
      <td>

                    <asp:FileUpload ID="File_FG" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnFG" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFG_Click" Width="14px" Height="16px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Foto Chica</td>
      <td class="auto-style68">
                 <asp:Label ID="lbl_fotoc" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>

                 </td>
        <td class="auto-style64">
            <asp:Image ID="FC_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="borra_fotoc" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_fotoc_Click" Width="16px" />
        </td>
      <td>

                    <asp:FileUpload ID="File_FC" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnFC" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFC_Click" Width="14px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Vídeo</td>
      <td class="auto-style68">
                    <asp:Label ID="lbl_video" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>
                 </td>
        <td class="auto-style64">
            <asp:Image ID="VD_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="borra_video" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_video_Click" Width="16px" />
        </td>
      <td>

                    <asp:FileUpload ID="File_VID" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnVID" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnVID_Click" Width="14px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style6">Hoja de Seguridad</td>
      <td class="auto-style68">
                    <asp:Label ID="lbl_hoja_seguridad" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>
                 </td>
        <td class="auto-style64">
            <asp:Image ID="HS_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="borra_hoja_seg" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_hoja_seg_Click" Width="16px" />
        </td>
      <td>

                    <asp:FileUpload ID="File_HS" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnHS" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnHS_Click" Width="14px" />

                 </td>
    </tr>
    <tr>
      <td colspan="11" class="BottomTabla"><strong>Accesorios y Repuestos</strong></td>
    </tr>
    <tr>
      <td class="auto-style56">Accesorio 1:</td>
      <td colspan="1" class="auto-style72" >
          <asp:TextBox ID="txt_acc1" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
        <td class="auto-style63">
            <asp:ImageButton ID="borra_acc4" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc1_Click" />
        </td>
        <td class="auto-style63">
            <asp:ImageButton ID="ImgBtnAc4" runat="server" Height="16px" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc1_Click" />
        </td>
      <td colspan="7" class="auto-style63">
          <asp:DropDownList ID="LstProdDispAc1" runat="server" Visible="False" Height="21px">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAC1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC1_Click" Width="14px" Visible="False" />

        </td>
    </tr>
    <tr>
      <td class="auto-style58">Accesorio 2:</td>
      <td colspan="1" class="auto-style71" >
          <asp:TextBox ID="txt_acc2" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style65">
             <asp:ImageButton ID="borra_acc2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc2_Click" style="height: 16px" />
        </td>
        <td class="auto-style65">
            <asp:ImageButton ID="ImgBtnAc5" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc2_Click" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispAc2" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAC2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC2_Click" Width="14px" Visible="False" style="height: 13px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style53">Accesorio 3:</td>
      <td colspan="1" class="auto-style71">
          <asp:TextBox ID="txt_acc3" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style65">
             <asp:ImageButton ID="borra_acc3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc3_Click" />
        </td>
        <td class="auto-style65">
            <asp:ImageButton ID="ImgBtnAc3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc3_Click" style="width: 13px" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispAc3" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAC3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC3_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style57">Repuesto 1:</td>
      <td colspan="1" class="auto-style70">
          <asp:TextBox ID="txt_rep1" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style62">
             <asp:ImageButton ID="borra_rep1" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep1_Click" style="width: 16px" />
        </td>
        <td class="auto-style62">
            <asp:ImageButton ID="ImgBtnRe1" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe1_Click" style="width: 13px" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispRe1" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddRE1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE1_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style57">Repuesto 2:</td>
      <td colspan="1" class="auto-style70" >
          <asp:TextBox ID="txt_rep2" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style62">
             <asp:ImageButton ID="borra_rep2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep2_Click" style="height: 16px" />
        </td>
        <td class="auto-style62">
            <asp:ImageButton ID="ImgBtnRe2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe2_Click" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispRe2" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddRE2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE2_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Repuesto 3:</td>
      <td colspan="1" class="auto-style68" >
          <asp:TextBox ID="txt_rep3" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style64">
             <asp:ImageButton ID="borra_rep3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep3_Click" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="ImgBtnRe3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe3_Click" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispRe3" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddRE3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE3_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Alternativa 1:</td>
      <td colspan="1" class="auto-style68" >
          <asp:TextBox ID="txt_alt1" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style64">
             <asp:ImageButton ID="borra_alt1" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt1_Click" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="ImgBtnAl1" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl1_Click" style="width: 13px" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispAl1" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAL1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL1_Click" Width="14px" Visible="False" style="height: 13px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Alternativa 2:</td>
      <td colspan="1" class="auto-style68" >
          <asp:TextBox ID="txt_alt2" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style64">
             <asp:ImageButton ID="borra_alt2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt2_Click" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="ImgBtnAl2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl2_Click" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispAl2" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAL2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL2_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Alternativa 3:</td>
      <td colspan="1" class="auto-style68">
          <asp:TextBox ID="txt_alt3" runat="server" Enabled="False"></asp:TextBox>
          &nbsp;
          </td>
         <td class="auto-style64">
             <asp:ImageButton ID="borra_alt3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt3_Click" />
        </td>
        <td class="auto-style64">
            <asp:ImageButton ID="ImgBtnAl3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl3_Click" />
        </td>
         <td colspan="7" class="auto-style62">
          <asp:DropDownList ID="LstProdDispAl3" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAL3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL3_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td colspan="11" class="BottomTabla"><strong>Categorías y Subcategorías</strong></td>
    </tr>
    <tr>
      <td class="auto-style58">Categoria1 </td>
      <td colspan="4" >
          <asp:DropDownList ID="LstCategorias1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias1_SelectedIndexChanged" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
      <td  colspan="5" >SubCategoria1</td>
      <td class="auto-style59"  >
          <asp:DropDownList ID="LstSubCategorias1" runat="server" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
    </tr>
    <tr>
      <td class="auto-style41">Categoria 2</td>
      <td colspan="4" class="auto-style42" >
          <asp:DropDownList ID="LstCategorias2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias2_SelectedIndexChanged" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
      <td  colspan="5" class="auto-style42" >SubCategoria2</td>
      <td class="auto-style60"  >
          <asp:DropDownList ID="LstSubCategorias2" runat="server" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Categoria 3</td>
      <td colspan="4" >
          <asp:DropDownList ID="LstCategorias3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias3_SelectedIndexChanged" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
      <td colspan="5" >SubCategoria3</td>
      <td class="auto-style59" >
          <asp:DropDownList ID="LstSubCategorias3" runat="server" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
    </tr>
    <tr >
      <td class="auto-style43"><p>Tabla T&eacute;cnica</p>
        </td>
      <td colspan="10" class="auto-style44" >
                    <asp:TextBox ID="txt_tabla_tecnica" runat="server" Height="111px" Width="1309px"></asp:TextBox>
                 </td>
    </tr>
    <tr >
      <td class="auto-style54" colspan="11">
          <asp:GridView ID="GridView1" runat="server" Caption="Historial de Modificaciones" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="1098px">
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
        </td>
    </tr>
    <tr >
      <td class="auto-style58">&nbsp;</td>
      <td colspan="6" class="auto-style50" >
                    <asp:Button ID="Btn_emigrar" runat="server" Text="Subir/Actualizar Código al Sitio Web" OnClick="Btn_emigrar_Click" />
                </td>
      <td colspan="4" >
                                      <asp:Button ID="Btn_eliminar" runat="server" Text="Eliminar Código del Sitio Web" OnClick="Btn_eliminar_Click" BackColor="#FF3300" />
                </td>
    </tr>
    <tr id="filaboton">
      <td colspan="11"  class="BottomTabla"></td>
    </tr>
  </table></td>
</tr>
</table>
        </ContentTemplate>
    <Triggers>
       <asp:PostBackTrigger ControlID="ImgBtnFT" />
        <asp:PostBackTrigger ControlID="ImgBtnPRE" />
        <asp:PostBackTrigger ControlID="ImgBtnFG" />
        <asp:PostBackTrigger ControlID="ImgBtnFC" />
        <asp:PostBackTrigger ControlID="ImgBtnVID" />
        <asp:PostBackTrigger ControlID="ImgBtnHS" />
       </Triggers>
    </asp:UpdatePanel>
  </form>
    </body>
</html>
<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }

    function abrirficha(id_item) {
        //window.open('C:\Intranet\edita_Item.asp?which=' & id_item, id_item, 900, 900, yes, yes);
        window.open("\\edita_Item.asp?which=" + id_item, "MsgWindow", "width=900,height=900");
    }
</script>