<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Items_detalle.aspx.cs" Inherits="erpweb.Item" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <link href="css/estilos.css" rel="stylesheet" />
    <link href="css/estilos.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title>Detalle de Producto</title>
    
        
    
    
        
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            -ms-flex: 0 0 16.666667%;
            flex: 0 0 16.666667%;
            max-width: 16.666667%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
        .auto-style2 {
            position: relative;
            width: 28%;
            -ms-flex: 0 0 16.666667%;
            flex: 0 0 16.666667%;
            max-width: 16.666667%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
        .auto-style3 {
            display: block;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-clip: padding-box;
            border-radius: 0.25rem;
            transition: none;
            border: 1px solid #ced4da;
            background-color: #fff;
        }
    </style>
    
        
    
    
        
    </head>
<body style="width: 1429px; height: 1593px">
<form id="form1" runat="server" enctype="multipart/form-data" class="auto-style78">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
        <br />
    <div class="container-fluid">
	<div class="row">
		<div class="col-md-4 justify-content-center">
			 <h3><span class="badge badge-none"><img alt=""src="img/vineta.gif" />Edici&oacute;n de Art&iacute;culo (Web)</span></h3>
		</div>
		<div class="col-md-2 justify-content-center">
            <h3><asp:Button ID="BtnGrabar" runat="server" OnClick="BtnGrabar_Click" Text="Grabar" CssClass="btn btn-primary btn-block" /></h3>
		</div>
		<div class="col-md-2 justify-content-center">
            <h3><asp:Button ID="Btn_act_superior" runat="server" OnClick="Btn_act_superior_Click" Text="Subir/Act Código al Sitio Web" CssClass="btn btn-success btn-block" /></h3>
		</div>
        <div class="col-md-2 justify-content-center">
            <h3><asp:Button ID="Btn_eliminar_todo" runat="server" Text="Eliminar"  OnClick="Btn_eliminar_todo_Click" CssClass="btn btn-warning btn-block"/></h3>
		</div>
         <div class="col-md-2 justify-content-center">
            <h3><asp:Button ID="Btn_volver" runat="server" OnClick="Btn_volver_Click" Text="Volver" CssClass="btn btn-danger btn-block" /></h3>
            <asp:ImageButton ID="ImgBtn_Cerrar" runat="server" Height="25px" ImageUrl="~/img/cerrar.png" style="text-align: right"/>
		</div>
	</div>
</div>
<br />
        <div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
			 <span><h4><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></h4></span> 
             <span><h4><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></h4></span> 
             <span><h4><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-warning"></asp:Label></h4></span>
		</div>
	</div>
</div>
<br />
<div class="row">
	<div class="col-md-2">
        <h4><span class="badge badge-primary">Activo</span></h4><span class="badge badge-primary">Activo</span>
	</div>
	<div class="col-md-7">
           <h4><asp:Label ID="lbl_activo" runat="server" CssClass="form-control" Width="93px"></asp:Label></h4>
	</div>
	<div class="col-md-2">
        <asp:LinkButton ID="LinkAct_item" runat="server" OnClick="LinkAct_item_Click" Visible="False" CssClass="form-control">Activar Código</asp:LinkButton>
        <asp:LinkButton ID="LinkDesAct_item" runat="server" OnClick="LinkDesAct_item_Click" Visible="False" CssClass="form-control">Desactivar Código</asp:LinkButton>
	</div>
	</div>

<table  border="0">
<tr>
  <td colspan="2" class="auto-style61"><table>
    <tr>
      <td class="auto-style6">Activo</td>
      <td colspan="6" class="auto-style48" >
                    
                 </td>
      <td class="auto-style11" >Publicado en Sitio</td>
      <td colspan="3" >
                      <asp:Label ID="lbl_web" runat="server"></asp:Label>
                </td>
    </tr>
    <tr>
      <td class="auto-style53">Visible</td>
      <td colspan="6" class="auto-style49" >
          <asp:CheckBox ID="chck_visible" runat="server" TextAlign="Left" />
        </td>
      <td colspan="3">Prod. Pedido</td>
        <td>
                     <asp:CheckBox ID="chck_prodped" runat="server" TextAlign="Left" />
                </td>
    </tr>
      <tr>
          <td class="auto-style53">Código</td>
          <td class="auto-style49" colspan="6">
              <asp:TextBox ID="txt_codigo" runat="server" Enabled="False" Width="95px"></asp:TextBox>
              <asp:ImageButton ID="ImgBtnLink" runat="server" ImageUrl="~/img/ver.gif" />
          </td>
          <td colspan="3" class="auto-style1">Crear Datos faltantes</td>
          <td class="auto-style1">
              <asp:CheckBox ID="Chk_crea_data" runat="server" TextAlign="Left" />
          </td>
      </tr>
    <tr>
      <td class="auto-style73">Venta</td>
      <td colspan="5" class="auto-style74">
                      <asp:CheckBox ID="chck_venta" runat="server" TextAlign="Left" />
                </td>
      <td colspan="3" class="auto-style74">Cotizaciones</td>
      <td colspan="2" class="auto-style74">
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
      <td colspan="10"><asp:TextBox ID="txt_texto_destacado" runat="server" Height="52px" Width="1315px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style53">Descripci&oacute;n<span class="asteriscoObligatorio">*</span></td>
      <td colspan="10" class="auto-style1">
          <asp:TextBox ID="txt_descripcion" runat="server" Height="52px" Width="1315px" TextMode="MultiLine"></asp:TextBox>
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
          <asp:Image ID="Div_fam" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="División aún no se publca en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" Width="16px" />
        </td>
    </tr>
    <tr>
      <td class="auto-style55">Categor&iacute;a</td>
      <td colspan="6" class="auto-style51">
          <asp:DropDownList ID="LstCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
          <asp:Image ID="Div_Cat" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="Categoría aún no se publca en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" Width="16px" />
        </td>
      <td class="auto-style51">
          SubCategor&iacute;a</td>
      <td colspan="3">
          <asp:DropDownList ID="LstSubCategorias" runat="server" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
          <asp:Image ID="Div_Subcat" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="SubCategoría aún no se publca en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" Width="16px" />
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
      <td class="auto-style55">Marca</td>
      <td colspan="6" class="auto-style51">
          <asp:TextBox ID="txt_marca" runat="server" Width="477px"></asp:TextBox>
        </td>
      <td class="auto-style12" >C&oacute;digo Prov.</td>
      <td colspan="3" class="auto-style51">
          <asp:TextBox ID="txt_codigoprov" runat="server" Width="207px"></asp:TextBox>
        </td>
    </tr>
    <tr>
      <td class="auto-style58">Caracter&iacute;sticas</td>
      <td colspan="10" rowspan="2">
                    <asp:TextBox ID="txt_caracteristicas" runat="server" Height="102px" Width="1315px" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lbl_caracteristicas" runat="server" BorderStyle="Groove" Width="1315px" Height="127px"></asp:Label>
                 </td>
    </tr>
      <tr>
          <td class="auto-style58">
              <asp:ImageButton ID="ImgHTmlCar" runat="server" Height="35px" ImageUrl="~/img/html.png" OnClick="ImgHTmlCar_Click" ViewStateMode="Enabled" Visible="False" Width="41px" />
              <asp:ImageButton ID="ImgVerCar" runat="server" Height="35px" ImageUrl="~/img/vista.png" OnClick="ImgVerCar_Click" Visible="False" Width="41px" />
              <asp:ImageButton ID="ImgGrabaCar" runat="server" Height="35px" ImageUrl="~/img/grabar.png" Visible="False" Width="41px" OnClick="ImgGrabaCar_Click1" />
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
                    (Max 5MB)</td>
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

                    (Max 5MB)</td>
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
      <td class="auto-style58">Foto 1</td>
      <td class="auto-style68">
                    <asp:Label ID="lbl_fotog" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>

                    (Max 5MB)</td>
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
      <td class="auto-style79">Foto 2</td>
      <td class="auto-style80">
                 <asp:Label ID="lbl_fotoc" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>

                 (Max 5MB)</td>
        <td class="auto-style81">
            <asp:Image ID="FC_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
        </td>
        <td class="auto-style81">
            <asp:ImageButton ID="borra_fotoc" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_fotoc_Click" Width="16px" />
        </td>
      <td class="auto-style82">

                    <asp:FileUpload ID="File_FC" runat="server" />

                 </td>
      <td class="auto-style82">

                    <asp:ImageButton ID="ImgBtnFC" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFC_Click" Width="14px" />

                 </td>
    </tr>
    <tr>
      <td class="auto-style58">Vídeo</td>
      <td class="auto-style68">
                    <asp:Label ID="lbl_video" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label>
                    (Max 5MB)</td>
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
                    (Max 5MB)</td>
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
      <td  >
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
      <td class="auto-style42"  >
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
      <td >
          <asp:DropDownList ID="LstSubCategorias3" runat="server" AppendDataBoundItems="True">
              <asp:ListItem Value="0">Seleccione</asp:ListItem>
          </asp:DropDownList>
        </td>
    </tr>
    <tr >
      <td class="auto-style43">Tabla T&eacute;cnica
        </td>
      <td colspan="10" class="auto-style44" rowspan="2" >
                    <asp:TextBox ID="txt_tabla_tecnica" runat="server" Height="175px" Width="1315px" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lbl_tabla_tecnica" runat="server" BorderStyle="Groove" Width="1315px" Height="176px"></asp:Label>
                 </td>
    </tr>
      <tr>
          <td class="auto-style76">
              <asp:ImageButton ID="ImgHTmltec" runat="server" Height="35px" ImageUrl="~/img/html.png" OnClick="ImgHTmlTec_Click" Visible="False" Width="41px" />
              <asp:ImageButton ID="ImgVerTec" runat="server" CssClass="auto-style77" Height="35px" ImageUrl="~/img/vista.png" OnClick="ImgVerTec_Click" Visible="False" Width="41px" />
              <asp:ImageButton ID="ImgGrabaTec" runat="server" Height="35px" ImageUrl="~/img/grabar.png" OnClick="ImgGrabaTec_Click" Visible="False" Width="41px" />
          </td>
      </tr>
    <tr >
      <td class="auto-style54" colspan="11">
          <asp:GridView ID="GridView1" runat="server" Caption="Historial de Modificaciones" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="1404px">
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
        </span>
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