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
        .auto-style36 {
            height: 19px;
            width: 273px;
        }
        .auto-style37 {
            width: 273px;
        }
        .auto-style40 {
            height: 18px;
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
        .auto-style52 {
            height: 17px;
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
        </style>
    </head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">

<table  border="0">
<tr>
  <td colspan="2"><table>
    <tr>
      <td colspan="9"><table width="100%" border="0"   class="titNoticia">
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
      <td class="auto-style6">&nbsp;</td>
      <td colspan="4" class="auto-style48" >
        <asp:Label ID="lbl_status" runat="server" BackColor="White"></asp:Label>
        <asp:Label ID="lbl_error" runat="server" BackColor="Red"></asp:Label>
                 </td>
      <td class="auto-style11" >&nbsp;</td>
      <td colspan="3" >
                      &nbsp;</td>
    </tr>
    <tr>
      <td class="auto-style6">Registrado en Sitio</td>
      <td colspan="4" class="auto-style48" >
                    <asp:Label ID="lbl_web" runat="server"></asp:Label>
                 </td>
      <td class="auto-style11" >Visible</td>
      <td colspan="3" >
                      <asp:CheckBox ID="chck_visible" runat="server" TextAlign="Left" />
                </td>
    </tr>
    <tr>
      <td class="auto-style53">C&oacute;digo</td>
      <td colspan="4" class="auto-style49" >
          <asp:TextBox ID="txt_codigo" runat="server" Enabled="False" Width="95px"></asp:TextBox>
        </td>
      <td colspan="3">Venta</td>
        <td>
                     <asp:CheckBox ID="chck_venta" runat="server" TextAlign="Left" />
                </td>
    </tr>
    <tr>
      <td class="auto-style58">Prod. a Pedido</td>
      <td colspan="3">
                      <asp:CheckBox ID="chck_prodped" runat="server" TextAlign="Left" />
                </td>
      <td colspan="3">Cotizaciones</td>
      <td colspan="2">
                    <asp:CheckBox ID="chck_cot" runat="server" TextAlign="Left" />
                </td>
    </tr>
    <tr>
      <td class="auto-style6" >Moneda</td>
      <td colspan="3">
          <asp:DropDownList ID="LstMonedas" runat="server">
              <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
      <td colspan="3">Unidad Venta</td>
      <td colspan="2">
                    <asp:Label ID="lbl_unidad" runat="server"></asp:Label>
                 </td>
    </tr>
    <tr>
      <td class="auto-style6" >Precio Lista</td>
      <td colspan="3">
          <asp:TextBox ID="txt_precio_lista" runat="server"></asp:TextBox>
        </td>
      <td colspan="3">Precio</td>
      <td colspan="2">
          <asp:TextBox ID="txt_precio" runat="server" AutoPostBack="True" OnTextChanged="txt_precio_TextChanged"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Texto Destacado</td>
      <td colspan="8"><asp:TextBox ID="txt_texto_destacado" runat="server" Height="52px" Width="939px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style53">Descripci&oacute;n<span class="asteriscoObligatorio">*</span></td>
      <td colspan="8" class="auto-style1">
          <asp:TextBox ID="txt_descripcion" runat="server" Height="52px" Width="940px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Proveedor</td>
      <td colspan="8">
          <asp:TextBox ID="txt_proveedor" runat="server" Enabled="False" Width="927px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style55">División</td>
      <td colspan="8" class="auto-style61">
          <asp:TextBox ID="txt_division" runat="server" Enabled="False" Width="500px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style55">Categor&iacute;a</td>
      <td colspan="4" class="auto-style51">
          <asp:DropDownList ID="LstCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged">
          </asp:DropDownList>
        </td>
      <td class="auto-style51">
          SubCategor&iacute;a</td>
      <td colspan="3">
          <asp:DropDownList ID="LstSubCategorias" runat="server">
          </asp:DropDownList>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">L&iacute;nea de Venta</td>
      <td colspan="4">
          <asp:DropDownList ID="LstLineaVtas" runat="server">
          </asp:DropDownList>
        </td>
      <td >Presentación: </td>
      <td colspan="3">
          <asp:TextBox ID="txt_unidad" runat="server" Width="207px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Marca</td>
      <td colspan="4">
          <asp:TextBox ID="txt_marca" runat="server" Width="477px"></asp:TextBox>
        </td>
      <td class="auto-style12" >C&oacute;digo Prov.</td>
      <td colspan="3">
          <asp:TextBox ID="txt_codigoprov" runat="server" Width="207px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Caracter&iacute;sticas</td>
      <td colspan="8">
                    <asp:TextBox ID="txt_caracteristicas" runat="server" Height="102px" Width="945px" TextMode="MultiLine"></asp:TextBox>
                 </td>
    </tr>
    <tr>

      <td colspan="9" class="BottomTabla">
                 <strong>Archivos Adjuntos</strong></td>
    </tr>
    <tr>
      <td class="auto-style6">Manual Técnico</td>
      <td class="auto-style36">
                    <asp:Label ID="lbl_manual_tecnico" runat="server" BackColor="#CCCCCC" Width="234px" Height="20px"></asp:Label>
<asp:ImageButton ID="borra_manual_tecnico" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_manual_tecnico_Click" Width="16px" />
                    <asp:Image ID="MT_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Width="16px" />
                 </td>
      <td>

                    <asp:FileUpload ID="File_FT" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnFT" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFT_Click" Height="16px" />

                 </td>
      <td colspan="5" rowspan="6">
                 <asp:Image ID="img_prod" runat="server" Height ="217px" Width="300px" />
                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Presentación Producto</td>
      <td class="auto-style37">
                    <asp:Label ID="lbl_presentacion" runat="server" BackColor="#CCCCCC" Width="234px" Height="20px"></asp:Label>

<asp:ImageButton ID="borra_presentacion" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_presentacion_Click" Width="16px" />

                    <asp:Image ID="PR_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Width="16px" />

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
      <td class="auto-style37">
                    <asp:Label ID="lbl_fotog" runat="server" BackColor="#CCCCCC" Width="234px" Height="20px"></asp:Label>

<asp:ImageButton ID="borra_fotog" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_fotog_Click" Width="16px" />

                    <asp:Image ID="FG_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Width="16px" />

                 </td>
      <td>

                    <asp:FileUpload ID="File_FG" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnFG" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFG_Click" Width="14px" style="height: 13px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Foto Chica</td>
      <td class="auto-style37">
                 <asp:Label ID="lbl_fotoc" runat="server" BackColor="#CCCCCC" Width="234px" Height="20px"></asp:Label>

<asp:ImageButton ID="borra_fotoc" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_fotoc_Click" Width="16px" />

                 <asp:Image ID="FC_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Width="16px" />

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
      <td>
                    <asp:Label ID="lbl_video" runat="server" BackColor="#CCCCCC" Width="234px" Height="20px"></asp:Label>
<asp:ImageButton ID="borra_video" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_video_Click" Width="16px" />
                    <asp:Image ID="VD_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Width="16px" />
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
      <td>
                    <asp:Label ID="lbl_hoja_seguridad" runat="server" BackColor="#CCCCCC" Width="234px" Height="20px"></asp:Label>
<asp:ImageButton ID="borra_hoja_seg" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_hoja_seg_Click" Width="16px" />
                    <asp:Image ID="HS_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Width="16px" />
                 </td>
      <td>

                    <asp:FileUpload ID="File_HS" runat="server" />

                 </td>
      <td>

                    <asp:ImageButton ID="ImgBtnHS" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnHS_Click" Width="14px" />

                 </td>
    </tr>
    <tr>
      <td colspan="9" class="BottomTabla"><strong>Accesorios y Repuestos</strong></td>
    </tr>
    <tr>
      <td class="auto-style56">Accesorio 1:</td>
      <td colspan="1" class="auto-style52" >
          <asp:TextBox ID="txt_acc1" runat="server" Enabled="False"></asp:TextBox>
<asp:ImageButton ID="borra_acc1" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc1_Click" />&nbsp;
          <asp:ImageButton ID="ImgBtnAc1" runat="server" ImageUrl="~/img/ver.gif" style="height: 15px;" OnClick="ImgBtnAc1_Click" />
</td>
      <td colspan="7" class="auto-style52">
          <asp:DropDownList ID="LstProdDispAc1" runat="server" Visible="False" Height="21px">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAC1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC1_Click" Width="14px" Visible="False" />

        </td>
    </tr>
    <tr>
      <td class="auto-style58">Accesorio 2:</td>
      <td colspan="1" class="auto-style1" >
          <asp:TextBox ID="txt_acc2" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_acc2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc2_Click" style="height: 16px" />&nbsp;
          <asp:ImageButton ID="ImgBtnAc2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc2_Click" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispAc2" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAC2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC2_Click" Width="14px" Visible="False" style="height: 13px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style53">Accesorio 3:</td>
      <td colspan="1" class="auto-style1">
          <asp:TextBox ID="txt_acc3" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_acc3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc3_Click" />&nbsp;
          <asp:ImageButton ID="ImgBtnAc3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc3_Click" style="width: 13px" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispAc3" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAC3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC3_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style57">Repuesto 1:</td>
      <td colspan="1" class="auto-style40">
          <asp:TextBox ID="txt_rep1" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_rep1" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep1_Click" style="width: 16px" />&nbsp;
          <asp:ImageButton ID="ImgBtnRe1" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe1_Click" style="width: 13px" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispRe1" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddRE1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE1_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Repuesto 2:</td>
      <td colspan="1" >
          <asp:TextBox ID="txt_rep2" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_rep2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep2_Click" style="height: 16px" />&nbsp;
          <asp:ImageButton ID="ImgBtnRe2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe2_Click" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispRe2" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddRE2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE2_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Repuesto 3:</td>
      <td colspan="1" >
          <asp:TextBox ID="txt_rep3" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_rep3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep3_Click" />&nbsp;
          <asp:ImageButton ID="ImgBtnRe3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe3_Click" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispRe3" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddRE3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE3_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Alternativa 1:</td>
      <td colspan="1" >
          <asp:TextBox ID="txt_alt1" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_alt1" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt1_Click" />&nbsp;
          <asp:ImageButton ID="ImgBtnAl1" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl1_Click" style="width: 13px" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispAl1" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAL1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL1_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Alternativa 2:</td>
      <td colspan="1" >
          <asp:TextBox ID="txt_alt2" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_alt2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt2_Click" />&nbsp;
          <asp:ImageButton ID="ImgBtnAl2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl2_Click" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispAl2" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAL2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL2_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Alternativa 3:</td>
      <td colspan="1">
          <asp:TextBox ID="txt_alt3" runat="server" Enabled="False"></asp:TextBox>
          <asp:ImageButton ID="borra_alt3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt3_Click" />&nbsp;
          <asp:ImageButton ID="ImgBtnAl3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl3_Click" />
        </td>
         <td colspan="7" class="auto-style40">
          <asp:DropDownList ID="LstProdDispAl3" runat="server" Visible="False">
          </asp:DropDownList>

                    <asp:ImageButton ID="ImgBtnAddAL3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL3_Click" Width="14px" Visible="False" />

                 </td>
    </tr>
    <tr>
      <td colspan="9" class="BottomTabla"><strong>Categorías y Subcategorías</strong></td>
    </tr>
    <tr>
      <td class="auto-style58">Categoria1 </td>
      <td colspan="2" >
          <asp:DropDownList ID="LstCategorias1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias1_SelectedIndexChanged">
          </asp:DropDownList>
        </td>
      <td  colspan="5" >SubCategoria1</td>
      <td  >
          <asp:DropDownList ID="LstSubCategorias1" runat="server">
          </asp:DropDownList>
        </td>
    </tr>
    <tr>
      <td class="auto-style41">Categoria 2</td>
      <td colspan="2" class="auto-style42" >
          <asp:DropDownList ID="LstCategorias2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias2_SelectedIndexChanged">
          </asp:DropDownList>
        </td>
      <td  colspan="5" class="auto-style42" >SubCategoria2</td>
      <td class="auto-style42"  >
          <asp:DropDownList ID="LstSubCategorias2" runat="server">
          </asp:DropDownList>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Categoria 3</td>
      <td colspan="2" >
          <asp:DropDownList ID="LstCategorias3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias3_SelectedIndexChanged">
          </asp:DropDownList>
        </td>
      <td colspan="5" >SubCategoria3</td>
      <td >
          <asp:DropDownList ID="LstSubCategorias3" runat="server">
          </asp:DropDownList>
        </td>
    </tr>
    <tr >
      <td class="auto-style43"><p>Tabla T&eacute;cnica</p>
        </td>
      <td colspan="8" class="auto-style44" >
                    <asp:TextBox ID="txt_tabla_tecnica" runat="server" Height="111px" Width="945px"></asp:TextBox>
                 </td>
    </tr>
    <tr >
      <td class="auto-style54" colspan="9">
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
      <td colspan="4" class="auto-style50" >
                    <asp:Button ID="Btn_emigrar" runat="server" Text="Subir/Actualizar Código al Sitio Web" OnClick="Btn_emigrar_Click" />
                </td>
      <td colspan="4" >
                                      <asp:Button ID="Btn_eliminar" runat="server" Text="Eliminar Código del Sitio Web" OnClick="Btn_eliminar_Click" BackColor="#FF3300" />
                </td>
    </tr>
    <tr id="filaboton">
      <td colspan="9"  class="BottomTabla"></td>
    </tr>
  </table></td>
</tr>
</table>
  </form>
 
    </body>
</html>
<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>