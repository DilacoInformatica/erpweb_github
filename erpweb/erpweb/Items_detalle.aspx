﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Items_detalle.aspx.cs" Inherits="erpweb.Item" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <title>Detalle de Producto</title>
    </head>
<body>
<form id="form1" runat="server" enctype="multipart/form-data" class="auto-style78">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
        <br />
   
<%-- Maqueta Boostrap --%>
 <div class="container-fluid rounded border border-secondary bg-light">
       <div class="row">
            <div class="col-md-11">
                <div class="row">
                    <div class="col-md-4">
                        <h3 class="text-primary">Edición de Artículo (Web)</h3>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="BtnGrabar" CssClass="form-control btn btn-block btn-primary" runat="server" Text="Actualizar Código ERP" OnClick="BtnGrabar_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="Btn_act_superior" CssClass="form-control btn btn-block btn-success" runat="server" Text="Subir/Actualizar Sitio Web" OnClick="Btn_act_superior_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="Btn_eliminar_todo" CssClass="form-control btn btn-block btn-danger" runat="server" Text="Eliminar" OnClick="Btn_eliminar_todo_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="Btn_volver" CssClass="form-control btn btn-block btn-primary" runat="server" Text="Volver" OnClick="Btn_volver_Click" />
                        <asp:Image ID="ImgBtn_Cerrar" runat="server" Height="16px" ImageUrl="~/img/cerrar.png" ToolTip="Cerrar" Visible="False" Width="16px" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h5>
            </div>
            <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h5>
            </div>
             <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h5>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-2">
                <div class="row">
                    <div class="col-md-4">
                        <h5><span class="badge bg-info">Activo</span> </h5>
                        <h5><asp:Label ID="lbl_activo" runat="server" CssClass="form-control" Width="40px"></asp:Label></h5>
                    </div>
                    <div class="col-md-8">
                        <h5>
                            <asp:ImageButton ID="ImgBtnAct_item" runat="server" AlternateText="Activar Código" Height="30px" ImageUrl="~/img/on.png" Width="54px" OnClick="ImgBtnAct_item_Click" ToolTip="Activar Producto" Visible="False" />
                        </h5>
                        <h5>
                            <asp:ImageButton ID="ImgBtnDesAct_item" runat="server" Height="30px" ImageUrl="~/img/off.png" Width="54px" OnClick="ImgBtnDesAct_item_Click" ToolTip="Desactivar Producto" Visible="False" />
                        </h5>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <h5><span class="badge bg-info">Publicado en Sitio</span> </h5>
                <h5><asp:Label ID="lbl_web" runat="server" CssClass="form-control" Width="40px"></asp:Label></h5>
            </div>
            <div class="col-md-2">
                <h5><span class="badge bg-info">Visible</span></h5>
                <h5><asp:CheckBox ID="chck_visible" runat="server" TextAlign="Left" Width="150px" /></h5>
            </div>
             <div class="col-md-2">
                <h5><span class="badge bg-info">Prod. a Pedido</span></h5>
                <h5><asp:CheckBox ID="chck_prodped" runat="server" TextAlign="Left" /></h5>
		    </div>
            <div class="col-md-2">
                <h5><span class="badge bg-info">Crear Datos faltantes</span></h5>
                <h5><asp:CheckBox ID="Chk_crea_data" runat="server" TextAlign="Left"  Width="182px" /></h5>
		    </div>
        </div>
        <%-- Separación de Tabs --%>
        <div class="row">
        <div class="col-md-2">
            <div class="row">
                <div class="col-md-3">
                     <h5><span class="badge bg-info">Código</span></h5>
                     <h5><asp:TextBox ID="txt_codigo" runat="server" Enabled="False" Width="104px" CssClass="form-control"></asp:TextBox></h5>
                </div>
                <div class="col-md-1">
                    <h5><asp:ImageButton ID="ImgBtnLink" runat="server" ImageUrl="~/img/ver.gif" /></h5>
                </div>
                </div>
         </div>
        <div class="col-md-6">
            <h5><span class="badge bg-info input-group-prepend">Descripción</span></h5>
            <h5>
                <asp:TextBox ID="txt_descripcion" runat="server" Width="705px" CssClass="form-control"></asp:TextBox></h5>
        </div>
        <div class="col-md-2">
            <h5><span class="badge bg-info">Unidad de Venta</span></h5>
            <h5><asp:Label ID="lbl_unidad" runat="server" CssClass="form-control" Width="131px"></asp:Label></h5>
       </div>
    </div>
        <%-- Separación de Tabs --%>
        <div class="row">
		    <div class="col-md-2">
                <h5><span class="badge bg-info">Venta</span></h5>
                <h5><asp:CheckBox ID="chck_venta" runat="server" TextAlign="Left" Width="139px"  /></h5>
		    </div>
		    <div class="col-md-2">
                <h5><span class="badge bg-info">Cotizaciones</span></h5>
                <h5><asp:CheckBox ID="chck_cot" runat="server" TextAlign="Left" Width="139px" /></h5>
		    </div>
		    <div class="col-md-2">
                <h5><span class="badge bg-info">Moneda</span></h5>
                <h5><asp:DropDownList ID="LstMonedas" runat="server" AppendDataBoundItems="True" CssClass="form-select"  Width="140px">
                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList></h5>
            </div>
           
            <div class="col-md-2">
                <h5><span class="badge bg-info">Precio Lista</span></h5>
                <h5><asp:TextBox ID="txt_precio_lista" runat="server" CssClass="form-control"></asp:TextBox></h5>
            </div>
            <div class="col-md-2">
                <h5><span class="badge bg-info">Precio</span></h5>
                <h5><asp:TextBox ID="txt_precio" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txt_precio_TextChanged"></asp:TextBox></h5>
            </div>
		</div>
         <%-- Separación de Tabs --%>
        <div class="row">
            <div class="col-md-2">
                <div class="row">
                    <div class="col-md-2">
                         <h5>
                             <span class="badge bg-info">División</span>
                             <asp:DropDownList ID="LstDivision" CssClass="form-select" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivision_SelectedIndexChanged" Width="200px">
                                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList></h5>
                    </div>
                    <div class="col-md1">
                        <h5><asp:Image ID="Div_fam" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="División aún no se publica en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" Width="16px" /></h5>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="row">
                    <div class="col-md-2">
                         <h5>
                             <span class="badge bg-info">Categoría</span>
                             <asp:DropDownList ID="LstCategorias" CssClass="form-select" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged" Width="240px">
                                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            </asp:DropDownList></h5>
                    </div>
                    <div class="col-md1">
                        <h5><asp:Image ID="Div_Cat" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="Categoría aún no se publca en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" Width="16px" /></h5>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="row">
                    <div class="col-md-3">
                         <h5>
                             <span class="badge bg-info">SubCategoria</span>
                             <asp:DropDownList ID="LstSubCategorias" CssClass="form-select" runat="server" AppendDataBoundItems="True" Width="240px">
                                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            </asp:DropDownList></h5>
                    </div>
                    <div class="col-md1">
                        <h5><asp:Image ID="Div_Subcat" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="SubCategoría aún no se publica en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" Width="16px" /></h5>
                    </div>
                </div>
            </div>
             <div class="col-md-2">
                <h5><span class="badge bg-info text-left">Presentación</span>
                <asp:TextBox ID="txt_unidad" runat="server" Width="193px" CssClass="form-select"></asp:TextBox></h5>
            </div>
             <div class="col-md-2">
                <h5><span class="badge bg-info">Línea de Ventas</span>
                <asp:DropDownList ID="LstLineaVtas" CssClass="form-select" runat="server" AppendDataBoundItems="True" Width="300px"></asp:DropDownList></h5>
            </div>
             
        </div>
         <%-- Separación de Tabs --%>
        <div class="row">
          
            <div class="col-md-2">
                <h5><span class="badge bg-info text-left">Multiplo</span></h5>
                <h5><asp:TextBox ID="txt_multiplo" runat="server" Width="50px" CssClass="form-control"></asp:TextBox></h5>
            </div>
            <div class="col-md-2">
                <h5><span class="badge bg-info">Marca</span> </h5>
                <h5><asp:TextBox ID="txt_marca" runat="server" Width="204px" CssClass="form-control"></asp:TextBox></h5>
            </div>
              <div class="col-md-2">
                <h5><span class="badge bg-info">Cod. Prov</span></h5>
                <h5><asp:TextBox ID="txt_codigoprov" runat="server" Width="104px" CssClass="form-control"></asp:TextBox></h5>
            </div>
            <div class="col-md-2">
                <h5><span class="badge bg-info">Stock</span> </h5>
                <h5><asp:Label ID="lbl_stock" runat="server" CssClass="form-label" Width="131px"></asp:Label></h5>
            </div>
            <div class="col-md-2">
                <h5><span class="badge bg-info">Stock_critico</span></h5>
                <h5><asp:Label ID="lbl_stock_critico" runat="server" CssClass="form-label" Width="131px"></asp:Label></h5>
            </div>
        </div>
         <%-- Separación de Tabs --%>
        <div class="row">
            <div class="col-md-10">
                <h5><span class="badge bg-info input-group-prepend">Proveedor</span></h5>
                <h5><asp:TextBox ID="txt_proveedor" runat="server" Enabled="False" Width="800px" CssClass="form-control text-left"></asp:TextBox></h5>
            </div>
        </div>
        <%-- Separación de Tabs --%>
        <div class="row">
            <div class="col-md-10">
                <h5><span class="badge bg-info input-group-prepend">Texto Destacado</span></h5>
                <h5><asp:TextBox ID="txt_texto_destacado" runat="server" TextMode="MultiLine" Columns="2" Width="1285px" CssClass="form-control"></asp:TextBox></h5>
            </div>
        </div>
         <%-- Separación de Tabs --%>
        <div class="row">
            <div class="col-md-10">
                <h5><span class="badge bg-info">Características</span> </h5>
                <h5><asp:TextBox ID="txt_caracteristicas" runat="server"  TextMode="MultiLine" Columns="2" Width="1285px" CssClass="form-control input-lg" Height="94px"></asp:TextBox></h5>
                <h5><asp:Label ID="lbl_caracteristicas" runat="server" BorderStyle="Groove"  Width="1285px" CssClass="form-control input-lg"></asp:Label></h5>
            </div>
            <div class ="col-md-2">
                <asp:ImageButton ID="ImgHTmlCar" CssClass="form-control" runat="server" Height="35px" ImageUrl="~/img/html.png" OnClick="ImgHTmlCar_Click" ViewStateMode="Enabled" Visible="False" Width="41px" />
                <asp:ImageButton ID="ImgVerCar" CssClass="form-control"  runat="server" Height="35px" ImageUrl="~/img/vista.png" OnClick="ImgVerCar_Click" Visible="False" Width="41px" />
                <asp:ImageButton ID="ImgGrabaCar" CssClass="form-control"  runat="server" Height="35px" ImageUrl="~/img/grabar.png" Visible="False" Width="41px" OnClick="ImgGrabaCar_Click1" />
           </div>
       </div>
    <br />
     <%-- Div Maestro Boostrap --%>
    </div>
    <p></p>
    <div class="container-fluid rounded border border-secondary bg-light">
           <div class="row">
            <div class="col-md-12">
                <h3><span class="badge bg-primary">Archivos Adjuntos</span></h3>
                <div class="row">
                    <div class="col-md-9">
                        <div class="row">
                            <div class="col-md-3">
                                <h4><span class="badge bg-info">Manual Técnico</span> </h4>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5><asp:Label ID="lbl_manual_tecnico" CssClass="form-control" runat="server" BackColor="#CCCCCC" Height="20px" Width="189px"></asp:Label></h5>
                                    </div>
                                    <div class="col-md-2">
                                        <div class ="row">
                                            <div class="col-md-2">
                                                <h6><span class="badge bg-warning">(Máx 5MB)</span> </h6>
                                            </div>
                                             <div class="col-md-1">
                                                 <asp:Image ID="MT_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <h5><asp:ImageButton ID="borra_manual_tecnico" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_manual_tecnico_Click" Width="16px" /></h5>
                            </div>
                            <div class="col-md-4">
                                 <div class="row">
                                    <div class="col-md-10">
                                        <asp:FileUpload ID="File_FT" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:ImageButton ID="ImgBtnFT"  runat="server" Height="14px" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFT_Click"  />
                                    </div>
                                </div>
                            </div>
                        </div>

                         <div class="row">
                            <div class="col-md-3">
                                <h4><span class="badge bg-info">Presentación Producto</span> </h4>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5><asp:Label ID="lbl_presentacion" CssClass="form-control" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px"></asp:Label></h5>
                                    </div>
                                    <div class="col-md-2">
                                        <div class ="row">
                                            <div class="col-md-2">
                                                <h6><span class="badge bg-warning">(Máx 5MB)</span></h6>
                                            </div>
                                             <div class="col-md-1">
                                                 <asp:Image ID="PR_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="borra_presentacion" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_presentacion_Click" Width="16px" />
                            </div>
                            <div class="col-md-4">
                                 <div class="row">
                                    <div class="col-md-10">
                                        <asp:FileUpload ID="File_PRE" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-1">
                                            <asp:ImageButton ID="ImgBtnPRE" runat="server" Height="14px" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnPRE_Click" ToolTip="Extensión" Width="16px" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <h4><span class="badge bg-info">Foto 1</span> </h4>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5><asp:Label ID="lbl_fotog" runat="server" BackColor="#CCCCCC"  Height="20px" Width="189px" CssClass="form-control"></asp:Label></h5>
                                    </div>
                                    <div class="col-md-2">
                                        <div class ="row">
                                            <div class="col-md-2">
                                                <h6><span class="badge bg-warning">(Máx 5MB)</span> </h6>
                                            </div>
                                             <div class="col-md-1">
                                                 <asp:Image ID="FG_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="borra_fotog" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_fotog_Click" Width="16px" />
                                
                            </div>
                            <div class="col-md-4">
                                 <div class="row">
                                    <div class="col-md-10">
                                        <asp:FileUpload ID="File_FG" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-1">
                                            <asp:ImageButton ID="ImgBtnFG"  runat="server" Height="14px" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFG_Click" Width="14px" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <h4><span class="badge bg-info">Foto2</span> </h4>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5><asp:Label ID="lbl_fotoc" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px" CssClass="form-control"></asp:Label></h5>
                                    </div>
                                    <div class="col-md-2">
                                        <div class ="row">
                                            <div class="col-md-2">
                                                <h6><span class="badge bg-warning">(Máx 5MB)</span> </h6>
                                            </div>
                                             <div class="col-md-1">
                                                 <asp:Image ID="FC_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="borra_fotoc" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_fotoc_Click" Width="16px" />
                            </div>
                            <div class="col-md-4">
                                 <div class="row">
                                    <div class="col-md-10">
                                        <h4><asp:FileUpload ID="File_FC" runat="server" CssClass="form-control" /> </h4>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:ImageButton ID="ImgBtnFC"  runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFC_Click" Width="14px" />          
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <h4><span class="badge bg-info">Vídeo</span> </h4>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5><asp:Label ID="lbl_video" runat="server" BackColor="#CCCCCC" Height="20px" Width="189px" CssClass="form-control"></asp:Label></h5>
                                    </div>
                                    <div class="col-md-2">
                                        <div class ="row">
                                            <div class="col-md-2">
                                                <h6><span class="badge bg-warning">(Máx 5MB)</span> </h6>
                                            </div>
                                             <div class="col-md-1">
                                                 <asp:Image ID="VD_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />  
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="borra_video" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_video_Click" Width="16px" />
                            </div>
                            <div class="col-md-4">
                                 <div class="row">
                                    <div class="col-md-10">
                                        <asp:FileUpload ID="File_VID" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-1">
                                         <asp:ImageButton ID="ImgBtnVID" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnVID_Click" Width="14px" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <h4><span class="badge bg-info">Hoja de Seguridad</span> </h4>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5><asp:Label ID="lbl_hoja_seguridad" runat="server" BackColor="#CCCCCC" Width="189px" Height="20px" CssClass="form-control"></asp:Label></h5>
                                    </div>
                                    <div class="col-md-2">
                                        <div class ="row">
                                            <div class="col-md-2">
                                                <h6><span class="badge bg-warning">(Máx 5MB)</span> </h6>
                                            </div>
                                             <div class="col-md-1">
                                                 <asp:Image ID="HS_Warning" runat="server" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="borra_hoja_seg" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_hoja_seg_Click" Width="16px"/>
                            </div>
                            <div class="col-md-4">
                                 <div class="row">
                                    <div class="col-md-10">
                                        <asp:FileUpload ID="File_HS" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-1">
                                         <asp:ImageButton ID="ImgBtnHS" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnHS_Click" Width="14px"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                       
                    </div>
                    <div class="col-md-3 align-content-center">
                        <asp:Image ID="img_prod" runat="server" Height="217px" Width="300px" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <p></p>
    <div class="container-fluid rounded border border-secondary bg-light">
          <div class="row">
            <div class="col-md-9">
                <h3><span class="badge bg-primary">Accesorios y Repuestos</span></h3>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Accesorio 1</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_acc1" runat="server" Enabled="False" CssClass="form-control" Width="123px"></asp:TextBox></h4>   
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="borra_acc1" runat="server" ImageUrl="~/img/cancela.gif"  OnClick="borra_acc1_Click" Width="14px"  />
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="ImgBtnAc1" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc1_Click"  Width="14px"/>
                    </div>
                    <div class="col-md-4">
                        <h6><asp:DropDownList ID="LstProdDispAc1" runat="server" Visible="False" CssClass="form-select">
                         </asp:DropDownList></h6>
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="ImgBtnAddAC1" runat="server" ImageUrl="~/img/mas.jpg"  OnClick="ImgBtnAddAC1_Click" Width="14px" Visible="False" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Accesorio 2</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_acc2" runat="server" Enabled="False" Width="123px" CssClass="form-control"></asp:TextBox></h4>  
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="borra_acc2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc2_Click" Width="14px" />
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="ImgBtnAc2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc2_Click" Width="14px" />
                    </div>
                     <div class="col-md-4">
                       <h5><asp:DropDownList ID="LstProdDispAc2" runat="server" Visible="False" CssClass="form-select">         
                         </asp:DropDownList></h5>
                     </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="ImgBtnAddAC2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC2_Click" Width="14px" Visible="False"/>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Accesorio 3</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_acc3" runat="server" Enabled="False" CssClass="form-control" Width="123px"></asp:TextBox></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="borra_acc3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_acc3_Click" Width="14px" /></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAc3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc3_Click" Width="14px" /></h4>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:DropDownList ID="LstProdDispAc3" runat="server" Visible="False" CssClass="form-select">
                            </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAddAC3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC3_Click" Width="14px" Visible="False" /></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Repuesto 1</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_rep1" runat="server" Enabled="False" CssClass="form-control" Width="123px"></asp:TextBox></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="borra_rep1" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep1_Click" Width="14px" /></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnRe1" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe1_Click" Width="14px"/></h4>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:DropDownList ID="LstProdDispRe1" runat="server" Visible="False" CssClass="form-select">
                            </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAddRE1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE1_Click" Width="14px" Visible="False" /></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Repuesto 2</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_rep2" runat="server" Enabled="False" CssClass="form-control" Width="123px"></asp:TextBox></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="borra_rep2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep2_Click" style="height: 14px" /></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnRe2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe2_Click" Width="14px" /></h4>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:DropDownList ID="LstProdDispRe2" runat="server" Visible="False" CssClass="form-select">
                        </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAddRE2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE2_Click" Width="14px" Visible="False" /></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Repuesto 3</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_rep3" runat="server" Enabled="False" CssClass="form-control" Width="123px"></asp:TextBox></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="borra_rep3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_rep3_Click" /></h4>
                    </div>
                    <div class="col-md-1">
                         <asp:ImageButton ID="ImgBtnRe3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe3_Click" Width="14px"/>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:DropDownList ID="LstProdDispRe3" runat="server" Visible="False" CssClass="form-select">
                        </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAddRE3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE3_Click" Width="14px" Visible="False" /></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Alternativa 1</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_alt1" runat="server" Enabled="False"  Width="123px" CssClass="form-control"></asp:TextBox></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="borra_alt1" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt1_Click"/></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAl1" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl1_Click" style="width: 14px" /></h4>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:DropDownList ID="LstProdDispAl1" runat="server" Visible="False" CssClass="form-select">
                            </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAddAL1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL1_Click" Width="14px" Visible="False" /></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Alternativa 2</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_alt2" runat="server" Enabled="False" CssClass="form-control" Width="123px"></asp:TextBox></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="borra_alt2" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt2_Click" Width="14px" /></h4>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAl2" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl2_Click" Width="14px" /></h4>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:DropDownList ID="LstProdDispAl2" runat="server" Visible="False" CssClass="form-select">
                            </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAddAL2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL2_Click" Width="14px" Visible="False" /></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Alternativa 3</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <h4><asp:TextBox ID="txt_alt3" runat="server" Enabled="False" CssClass="form-control" Width="123px"></asp:TextBox></h4>
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="borra_alt3" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt3_Click" />
                    </div>
                    <div class="col-md-1">
                        <h4><asp:ImageButton ID="ImgBtnAl3" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl3_Click" Width="14px" /></h4>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:DropDownList ID="LstProdDispAl3" runat="server" Visible="False" CssClass="form-select">
                        </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="ImgBtnAddAL3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL3_Click" Width="14px" Visible="False"  />
                    </div>
                </div>
            </div>
        </div>

    </div>
    <br />
    <div class="container-fluid rounded border border-secondary">
            <div class="row">
            <div class="col-md-12">
                <h3><span class="badge bg-primary">Categorías y Subcategorías</span></h3>
                <div class="row">
                    <div class="col-md-3">
                        <h4><span class="badge bg-info">Categoría 1</span></h4>
                        <h5><asp:DropDownList ID="LstCategorias1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias1_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                              <asp:ListItem Value="0">Seleccione</asp:ListItem>
                          </asp:DropDownList></h5>
                    </div>
                    <div class="col-md-3">
                        <h4><span class="badge bg-info">Categoría 2</span> </h4>
                        <h4>
                            <asp:DropDownList ID="LstCategorias2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias2_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                  <asp:ListItem Value="0">Seleccione</asp:ListItem>
                             </asp:DropDownList>
                        </h4>
                    </div>
                    <div class="col-md-3">
                        <h4><span class="badge bg-info">Categoría 3</span> </h4>
                        <h5>
                            <asp:DropDownList ID="LstCategorias3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias3_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                  <asp:ListItem Value="0">Seleccione</asp:ListItem>
                              </asp:DropDownList>
                        </h5>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-md-3">
                        <h4><span class="badge bg-info">SubCategoría 1</span> </h4>
                        <h5><asp:DropDownList ID="LstSubCategorias1" runat="server" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                  <asp:ListItem Value="0">Seleccione</asp:ListItem>
                              </asp:DropDownList>
                        </h5>
                    </div>
                    <div class="col-md-3">
                        <h4><span class="badge bg-info">SubCategoría 2</span> </h4>
                        <h5>
                           <asp:DropDownList ID="LstSubCategorias2" runat="server" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                              <asp:ListItem Value="0">Seleccione</asp:ListItem>
                          </asp:DropDownList>
                        </h5>
                    </div>
                     <div class="col-md-3">
                        <h4><span class="badge bg-info">SubCategoría 3</span> </h4>
                        <h5>
                            <asp:DropDownList ID="LstSubCategorias3" runat="server" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                              <asp:ListItem Value="0">Seleccione</asp:ListItem>
                          </asp:DropDownList>
                        </h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h4><span class="badge bg-info">Tabla Técnica</span> </h4>
                        <asp:ImageButton ID="ImgHTmltec" runat="server" Height="35px" ImageUrl="~/img/html.png" OnClick="ImgHTmlTec_Click" Visible="False" Width="41px" CssClass="form-control" />
                        <asp:ImageButton ID="ImgVerTec" runat="server"  Height="35px" ImageUrl="~/img/vista.png" OnClick="ImgVerTec_Click" Visible="False" Width="41px" CssClass="form-control"  />
                        <asp:ImageButton ID="ImgGrabaTec" runat="server" Height="35px" ImageUrl="~/img/grabar.png" OnClick="ImgGrabaTec_Click" Visible="False" Width="41px" CssClass="form-control" />
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txt_tabla_tecnica" runat="server" Height="175px" TextMode="MultiLine" Width="1177px" CssClass="form-control input-lg"></asp:TextBox>
          <asp:Label ID="lbl_tabla_tecnica" runat="server" BorderStyle="Groove" Height="176px" Width="1180px" CssClass="form-control input-lg"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
       <div class="row">
		<div class="col-md-12">
            <h4>
                <asp:GridView ID="GridView1" CssClass="form-control" runat="server" Caption="Historial de Modificaciones" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="1404px">
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
           </h4>
		</div>
       </div>
    </div>
    <div class="container-fluid rounded border border-secondary">
            <div class="row">
		        <div class="col-md-12">
			        <div class="row align-content-center">
				        <div class="col-md-6 align-content-center">
                        <asp:Button ID="Btn_emigrar" CssClass="form-control  btn-success" runat="server" Text="Subir/Actualizar Código al Sitio Web" OnClick="Btn_emigrar_Click" Width="308px" />
				    </div>
				    <div class="col-md-6 center">
                        <asp:Button ID="Btn_eliminar" CssClass="form-control  btn-danger" runat="server" Text="Eliminar Código del Sitio Web" OnClick="Btn_eliminar_Click" Width="308px" />
				    </div>
			    </div>
		    </div>
      </div>
</div>

<%-- Fin Maqueta Boostrap --%>

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