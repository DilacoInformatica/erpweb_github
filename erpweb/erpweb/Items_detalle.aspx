<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Items_detalle.aspx.cs" Inherits="erpweb.Item" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <title>Detalle Ítems</title>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server" enctype="multipart/form-data">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">


            <ContentTemplate>
                <asp:ScriptManager ID="llave" runat="server" EnablePartialRendering="True">
                    <Scripts>
                        <asp:ScriptReference Path="scripts/bootstrap.bundle.min.js" />
                        <asp:ScriptReference Path="scripts/feather.min.js" />
                        <asp:ScriptReference Path="scripts/Chart.min.js" />
                        <asp:ScriptReference Path="scripts/dashboard.js" />
                    </Scripts>
                </asp:ScriptManager>
                <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
                    <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" href="#">
                        <label>
                            <span data-feather="user-check"></span>
                            <asp:Label ID="lbl_conectado" runat="server"></asp:Label>
                            <span data-feather="message-circle"></span>
                            <asp:Label ID="lbl_ambiente" runat="server"></asp:Label>
                        </label>
                    </a>
                    <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <label class="text-light text-center fs-4 fw-bold">
                        Detalle Producto
                    </label>
                    <div class="navbar-nav">
                        <div class="nav-item text-nowrap">
                            <asp:LinkButton ID="Lnk_volver" runat="server" CssClass="nav-link px-3" Width="133px" OnClick="Lnk_volver_Click">Volver</asp:LinkButton>
                        </div>
                    </div>
                </header>
                <div class="container-fluid">
                    <div class="row">
                        <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
                            <div class="position-sticky pt-3">
                                <ul class="nav flex-column">
                                    <li class="nav-item">
                                        <a class="nav-link active" href="Ppal.aspx">
                                            <span data-feather="home"></span>
                                            Dashboard
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <asp:LinkButton ID="LnkBtn_Actualizar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Actualizar_Click"><span data-feather="upload"></span>Actualizar en ERP</asp:LinkButton>
                                    </li>

                                    <li class="nav-item">
                                        <asp:LinkButton ID="LnkBtn_ActWeb" CssClass="nav-link" runat="server" OnClick="LnkBtn_ActWeb_Click"><span data-feather="upload-cloud"></span>Actualizar en Web</asp:LinkButton>
                                    </li>
                                    <li class="nav-item">
                                        <asp:LinkButton ID="LnkBtn_Eliminar" CssClass="nav-link" runat="server" OnClick="LnkBtn_Eliminar_Click"><span data-feather="trash-2"></span>Eliminar</asp:LinkButton>
                                    </li>
                                    <li class="nav-item">
                                        <asp:LinkButton ID="LnkBtn_Volver" CssClass="nav-link" runat="server" OnClick="LnkBtn_Volver_Click"><span data-feather="log-out"></span>Volver</asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </nav>

                        <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
                            <p class="divider"></p>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h4><span data-feather="alert-triangle"></span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 ">
                                        <h4><span data-feather="x-cirle"></span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <h4><span data-feather="info"></span><asp:Label ID="lbl_aviso_informacion" runat="server" CssClass="badge bg-warning"></asp:Label></h4>
                                    </div>
                                </div>
                                <p class="divider"></p>
                            </div>
                            <p class="divider"></p>
                            <div class="container-fluid rounded border border-secondary bg-light">
                                <h4><span class="badge bg-primary">Información Producto</span></h4>
                                <div class="row">
                                   <div class="col-md-4">
                                       <div class="card mb-3">
                                           <asp:Image ID="img_prod" runat="server"  ToolTip="Imagen Producto" CssClass="form-control" />
                                          <div class="card-body">
                                            <p class="card-text">Imagen del Producto en la Web.</p>
                                           <p class="card-text"><small class="text-muted">Verifique visualización en el Sitio Web</small></p>
                                        </div>
                                        </div>
                                   </div>
                                   <div class="col-md-8">
                                       
                                       <div class="row">
                                           <div class="col-md-3">
                                               <span class="badge bg-info"><span data-feather="edit"></span>Código</span>
                                               <asp:ImageButton ID="ImgBtnLink" runat="server" ImageUrl="~/img/ver.gif" />
                                               <asp:TextBox ID="txt_codigo" runat="server" Enabled="False" CssClass="form-control card-link"></asp:TextBox>
                                           </div>
                                           <div class="col-md-9">
                                               <span class="badge bg-info"><span data-feather="edit"></span>Descripción</span>
                                                <asp:TextBox ID="txt_descripcion"  runat="server" CssClass="form-control"></asp:TextBox>
                                           </div>
                                       </div>
                                       <div class="row">
                                            <div class="col-md-3">
                                                <span class="badge bg-info">Moneda</span>
                                                <asp:DropDownList ID="LstMonedas" runat="server" AppendDataBoundItems="True" CssClass="form-select">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <span class="badge bg-info">Precio Lista ERP</span>
                                                <div class="text-end">
                                                    <asp:TextBox ID="txt_precio_lista" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                           <div class="col-md-3">
                                                <span class="badge bg-info">Precio</span>
                                                <div class="text-end">
                                                    <asp:TextBox ID="txt_precio" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                           <div class="col-md-3">
                                                <span class="badge bg-info text-left">Multiplo</span>
                                                <asp:TextBox ID="txt_multiplo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                       </div>
                                       <div class="row">
                                            <div class="col-md-3">
                                                <span class="badge bg-info">Unidad de Venta</span>
                                                <asp:Label ID="lbl_unidad" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                            
                                           <div class="col-md-9">
                                                <span class="badge bg-info text-left">Presentación</span>
                                                <asp:TextBox ID="txt_unidad" runat="server" CssClass="form-select"></asp:TextBox>
                                            </div>
                                       </div>
                                       <div class="row">
                                            <div class="col-md-4">
                                                <span class="badge bg-info">División</span>
                                                <asp:DropDownList ID="LstDivision" CssClass="form-select" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivision_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Image ID="Div_fam" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="División aún no se publica en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" />
                                            </div>
                                            <div class="col-md-4">
                                                <span class="badge bg-info">Categoría</span>
                                                <asp:DropDownList ID="LstCategorias" CssClass="form-select" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Image ID="Div_Cat" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="Categoría aún no se publca en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" />
                                            </div>
                                            <div class="col-md-4">
                                                <span class="badge bg-info">SubCategoria</span>
                                                <asp:DropDownList ID="LstSubCategorias" CssClass="form-select" runat="server" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Image ID="Div_Subcat" runat="server" Height="16px" ImageUrl="~/img/warning.png" ToolTip="SubCategoría aún no se publica en el Sitio... se cargará una vez se suba el producto al Sitio" Visible="False" Width="16px" />
                                            </div>
                                       </div>
                                       <div class="row">
                                           <div class="col-md-12">
                                            <span class="badge bg-info">Línea de Ventas</span>
                                            <asp:DropDownList ID="LstLineaVtas" CssClass="form-select" runat="server" AppendDataBoundItems="True"></asp:DropDownList>
                                           </div>
                                       </div>
                                       <div class="row">
                                           <div class="col-md-3">
                                                <span class="badge bg-info">Marca</span>
                                                <asp:TextBox ID="txt_marca" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <span class="badge bg-info">Cod. Prov</span>
                                                <asp:TextBox ID="txt_codigoprov" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                           <div class="col-md-6">
                                                <span class="badge bg-info input-group-prepend">Proveedor</span>
                                                <asp:TextBox ID="txt_proveedor" runat="server" Enabled="False" CssClass="form-control text-left"></asp:TextBox>
                                            </div>
                                       </div>
                                       <div class="row">
                                           <div class="col-md-3">
                                               <div class="col-md-3">
                                                    <span class="badge bg-info">Stock</span><br />
                                                    <asp:Label ID="lbl_stock" runat="server" CssClass="form-label"></asp:Label>
                                              </div>
                                           </div>
                                           <div class="col-md-3">
                                               <span class="badge bg-info">Stock_critico</span><br />
                                                <asp:Label ID="lbl_stock_critico" runat="server" CssClass="form-label"></asp:Label>
                                           </div>
                                           <div class="col-md-3">
                                               <span class="badge bg-info">Visible</span><br />
                                               <asp:CheckBox ID="chck_visible" runat="server" TextAlign="Left" />
                                           </div>
                                           <div class="col-md-3">
                                                <span class="badge bg-info">Crear Datos faltantes</span><br />
                                                <asp:CheckBox ID="Chk_crea_data" runat="server" TextAlign="Left" />
                                           </div>
                                       </div>
                                       <div class="row">
                                           <div class="col-md-3">
                                               <span class="badge bg-info">Activo</span><br />
                                                <asp:ImageButton ID="ImgBtnAct_item" runat="server" AlternateText="Activar Código" Height="30px" ImageUrl="~/img/on.png" Width="54px" OnClick="ImgBtnAct_item_Click" ToolTip="Activar Producto" Visible="False" />
                                                <asp:ImageButton ID="ImgBtnDesAct_item" runat="server" Height="30px" ImageUrl="~/img/off.png" Width="54px" OnClick="ImgBtnDesAct_item_Click" ToolTip="Desactivar Producto" Visible="False" />
                                                <asp:Label ID="lbl_activo" runat="server" CssClass="form-control" Width="40px"></asp:Label>
                                           </div>
                                           <div class="col-md-3">
                                               <span class="badge bg-info">Prod. a Pedido</span><br />
                                               <asp:CheckBox ID="chck_prodped" runat="server" TextAlign="Left" />
                                           </div>
                                           <div class="col-md-3">
                                               <span class="badge bg-info">Venta</span><br />
                                               <asp:CheckBox ID="chck_venta" runat="server" TextAlign="Left" />
                                           </div>
                                           <div class="col-md-3">
                                               <span class="badge bg-info">Cotizaciones</span><br />
                                            <asp:CheckBox ID="chck_cot" runat="server" TextAlign="Left" />
                                           </div>
                                       </div>
                                       <div class="row">
                                           <div class="col-md-3">
                                                <div class="col-md-3">
                                               <span class="badge bg-info">Pub. Sitio</span><br />
                                                <asp:Label ID="lbl_web" runat="server" CssClass="form-control"></asp:Label>
                                           </div>
                                           </div>
                                       </div>
                                   
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <span class="badge bg-info input-group-prepend"><span data-feather="edit"></span>Texto Destacado</span>
                                         <asp:TextBox ID="txt_texto_destacado" runat="server" TextMode="MultiLine" Columns="6" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                    <p class="divider"></p>
                                 <div class="row">
                                    <div class="col-md-2">
                                        <span class="badge bg-info"><span data-feather="edit"></span>Características</span>
                                            <asp:ImageButton ID="ImgHTmlCar" CssClass="form-control" runat="server" Height="35px" ImageUrl="~/img/html.png" OnClick="ImgHTmlCar_Click" ViewStateMode="Enabled" Visible="False" Width="41px" />
                                        <asp:ImageButton ID="ImgVerCar" CssClass="form-control" runat="server" Height="35px" ImageUrl="~/img/vista.png" OnClick="ImgVerCar_Click" Visible="False" Width="41px" />
                                        <asp:ImageButton ID="ImgGrabaCar" CssClass="form-control" runat="server" Height="35px" ImageUrl="~/img/grabar.png" Visible="False" Width="41px" OnClick="ImgGrabaCar_Click1" />
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txt_caracteristicas" runat="server" TextMode="MultiLine" Columns="6" CssClass="form-control input-lg"></asp:TextBox>
                                        <asp:Label ID="lbl_caracteristicas" runat="server" BorderStyle="Groove" CssClass="form-control input-lg"></asp:Label>
                                    </div>
                                     <p class="divider"></p>
                                </div>
                             
                                    <%-- Separación de Tabs --%>

                                </div>
                            </div>
                            <p class="divider"></p>
                            <div class="container-fluid rounded border border-secondary bg-light">
                                <h4><span class="badge bg-primary">Archivos Adjuntos</span></h4>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Manual Técnico </strong><span class="badge bg-warning">(Máx 5MB)</span>
                                                <asp:Image ID="MT_Warning" runat="server" ToolTip="Archivo no se encuentra" Height="14px" ImageUrl="~/img/warning.png" Visible="False" />
                                                <asp:ImageButton ID="borra_manual_tecnico" ToolTip="Borrar" runat="server" Height="14px" ImageUrl="~/img/cancela.gif" OnClick="borra_manual_tecnico_Click" />
                                                <asp:ImageButton ID="ImgBtnFT" runat="server" ToolTip="Agregar" Height="14px" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFT_Click" />
                                            </div>
                                            <div class="card-body">
                                                <asp:Label ID="lbl_manual_tecnico" CssClass="form-control" runat="server" BackColor="#CCCCCC"></asp:Label>
                                                <asp:FileUpload ID="File_FT" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Presentación Producto </strong><span class="badge bg-warning">(Máx 5MB)</span>
                                                <asp:Image ID="PR_Warning" runat="server" Height="16px" ToolTip="Archivo no se encuentra" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                                <asp:ImageButton ID="borra_presentacion" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_presentacion_Click" Width="16px" />
                                                <asp:ImageButton ID="ImgBtnPRE" runat="server" Height="14px" ToolTip="Agregar" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnPRE_Click" Width="16px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:Label ID="lbl_presentacion" CssClass="form-control" runat="server" BackColor="#CCCCCC"></asp:Label>
                                                <asp:FileUpload ID="File_PRE" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Foto1 </strong><span class="badge bg-warning">(Máx 5MB)</span>
                                                <asp:Image ID="FG_Warning" runat="server" Height="16px" ToolTip="Archivo no se encuentra" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                                <asp:ImageButton ID="borra_fotog" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_fotog_Click" Width="16px" />
                                                <asp:ImageButton ID="ImgBtnFG" runat="server" ToolTip="Agregar" Height="14px" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFG_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:Label ID="lbl_fotog" runat="server" BackColor="#CCCCCC" CssClass="form-control"></asp:Label>
                                                <asp:FileUpload ID="File_FG" runat="server" CssClass="form-control" />                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Foto2 </strong><span class="badge bg-warning">(Máx 5MB)</span>
                                                <asp:Image ID="FC_Warning" runat="server" ToolTip="Archivo no se encuentra" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                                <asp:ImageButton ID="borra_fotoc" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_fotoc_Click" Width="16px" />
                                                <asp:ImageButton ID="ImgBtnFC" runat="server" ToolTip="Agregar" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnFC_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:Label ID="lbl_fotoc" runat="server" BackColor="#CCCCCC" CssClass="form-control"></asp:Label>
                                                <asp:FileUpload ID="File_FC" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Video </strong><span class="badge bg-warning">(Máx 5MB)</span>
                                                <asp:Image ID="VD_Warning" runat="server" ToolTip="Archivo no se encuentra" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                                <asp:ImageButton ID="borra_video" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_video_Click" Width="16px" />
                                                <asp:ImageButton ID="ImgBtnVID" runat="server" ToolTip="Agregar" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnVID_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:Label ID="lbl_video" runat="server" BackColor="#CCCCCC" CssClass="form-control"></asp:Label>
                                                <asp:FileUpload ID="File_VID" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Hoja de Seguridad </strong><span class="badge bg-warning">(Máx 5MB)</span>
                                                <asp:Image ID="HS_Warning" runat="server" ToolTip="Archivo no se encuentra" Height="16px" ImageUrl="~/img/warning.png" Visible="False" Width="16px" />
                                                <asp:ImageButton ID="borra_hoja_seg" ToolTip="Borrar" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_hoja_seg_Click" Width="16px" />
                                                <asp:ImageButton ID="ImgBtnHS" runat="server" ToolTip="Agregar" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnHS_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:Label ID="lbl_hoja_seguridad" runat="server" BackColor="#CCCCCC" CssClass="form-control"></asp:Label>
                                                <asp:FileUpload ID="File_HS" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <p class="divider"></p>
                            </div>
                            <p class="divider"></p>
                            <div class="container-fluid rounded border border-secondary bg-light">
                                <h4><span class="badge bg-primary">Accesorios y Repuestos</span></h4>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Accesorio1 </strong>
                                                <asp:ImageButton ID="borra_acc1" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_acc1_Click" Width="14px" />
                                                <asp:ImageButton ID="ImgBtnAc1" runat="server" ToolTip="Buscar Código" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc1_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_acc1" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispAc1" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddAC1" runat="server" ToolTip="Agregar" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC1_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Accesorio2 </strong>
                                                <asp:ImageButton ID="borra_acc2" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_acc2_Click" Width="14px" />
                                                <asp:ImageButton ID="ImgBtnAc2" runat="server" ToolTip="Buscar Código" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc2_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_acc2" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispAc2" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddAC2" runat="server" ToolTip="Agregar" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC2_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Accesorio3 </strong>
                                                <asp:ImageButton ID="borra_acc3" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_acc3_Click" Width="14px" />
                                                <asp:ImageButton ID="ImgBtnAc3" runat="server" ToolTip="Buscar Código" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAc3_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_acc3" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispAc3" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddAC3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAC3_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <p class="divider"></p>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Repuesto1 </strong>
                                                <asp:ImageButton ID="borra_rep1" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_rep1_Click" Width="14px" />
                                                <asp:ImageButton ID="ImgBtnRe1" runat="server" ToolTip="Buscar Código" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe1_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_rep1" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispRe1" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddRE1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE1_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Repuesto2 </strong>
                                                <asp:ImageButton ID="borra_rep2" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_rep2_Click" Style="height: 14px" />
                                                <asp:ImageButton ID="ImgBtnRe2" runat="server" ToolTip="Buscar Código" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe2_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_rep2" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispRe2" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddRE2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE2_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Repuesto3 </strong>
                                                <asp:ImageButton ID="borra_rep3" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_rep3_Click" />
                                                <asp:ImageButton ID="ImgBtnRe3" runat="server" ToolTip="Buscar Código" ImageUrl="~/img/ver.gif" OnClick="ImgBtnRe3_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_rep3" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispRe3" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddRE3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddRE3_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <p class="divider"></p>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"> 
                                                <strong>Alternativa1 </strong>
                                                <asp:ImageButton ID="borra_alt1" runat="server" ToolTip="Borrar" ImageUrl="~/img/cancela.gif" OnClick="borra_alt1_Click" />
                                                <asp:ImageButton ID="ImgBtnAl1" runat="server" ToolTip="Buscar Código" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl1_Click" Style="width: 14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_alt1" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispAl1" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddAL1" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL1_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Alternativa2 </strong>
                                                <asp:ImageButton ID="borra_alt2" ToolTip="Borrar" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt2_Click" Width="14px" />
                                                <asp:ImageButton ID="ImgBtnAl2" ToolTip="Buscar Código" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl2_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_alt2" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispAl2" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddAL2" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL2_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <strong>Alternativa3 </strong>
                                                <asp:ImageButton ID="borra_alt3" ToolTip="Borrar" runat="server" ImageUrl="~/img/cancela.gif" OnClick="borra_alt3_Click" />
                                                <asp:ImageButton ID="ImgBtnAl3" ToolTip="Buscar Código" runat="server" ImageUrl="~/img/ver.gif" OnClick="ImgBtnAl3_Click" Width="14px" />
                                            </div>
                                            <div class="card-body">
                                                <asp:TextBox ID="txt_alt3" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="LstProdDispAl3" runat="server" Visible="False" CssClass="form-select"></asp:DropDownList>
                                                <asp:ImageButton ID="ImgBtnAddAL3" runat="server" ImageUrl="~/img/mas.jpg" OnClick="ImgBtnAddAL3_Click" Width="14px" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <p class="divider"></p>
                            </div>
                            <p class="divider"></p>
                            <div class="container-fluid rounded border border-secondary">
                                <h4><span class="badge bg-primary">Categorías y Subcategorías</span></h4>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Categoría1 </strong></div>
                                            <div class="card-body">
                                                <asp:DropDownList ID="LstCategorias1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias1_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="badge bg-info">SubCategoría 1</span>
                                                <asp:DropDownList ID="LstSubCategorias1" runat="server" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Categoría2 </strong></div>
                                            <div class="card-body">
                                                <asp:DropDownList ID="LstCategorias2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias2_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="badge bg-info">SubCategoría 2</span>
                                                <asp:DropDownList ID="LstSubCategorias2" runat="server" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="card">
                                            <div class="card-header"><strong>Categoría3 </strong></div>
                                            <div class="card-body">
                                                <asp:DropDownList ID="LstCategorias3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias3_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="badge bg-info">SubCategoría 3</span>
                                                <asp:DropDownList ID="LstSubCategorias3" runat="server" AppendDataBoundItems="True" CssClass="form-select" Width="284px">
                                                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <p class="divider"></p>
                                <div class="row">
                                    <div class="col-md-2">
                                        <span class="badge bg-info"><span data-feather="edit"></span>Tabla Técnica</span>
                                        <asp:ImageButton ID="ImgHTmltec" runat="server" Height="35px" ImageUrl="~/img/html.png" OnClick="ImgHTmlTec_Click" Visible="False" Width="41px" CssClass="form-control" />
                                        <asp:ImageButton ID="ImgVerTec" runat="server" Height="35px" ImageUrl="~/img/vista.png" OnClick="ImgVerTec_Click" Visible="False" Width="41px" CssClass="form-control" />
                                        <asp:ImageButton ID="ImgGrabaTec" runat="server" Height="35px" ImageUrl="~/img/grabar.png" OnClick="ImgGrabaTec_Click" Visible="False" Width="41px" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txt_tabla_tecnica" runat="server" TextMode="MultiLine" CssClass="form-control input-lg"></asp:TextBox>
                                        <asp:Label ID="lbl_tabla_tecnica" runat="server" BorderStyle="Groove" CssClass="form-control input-lg"></asp:Label>
                                    </div>
                                </div>
                                <p class="divider"></p>
                            </div>
                            <p class="divider"></p>
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
                        </main>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgBtnFT" />
                <asp:PostBackTrigger ControlID="ImgBtnPRE" />
                <asp:PostBackTrigger ControlID="ImgBtnFG" />
                <asp:PostBackTrigger ControlID="ImgBtnFC" />
                <asp:PostBackTrigger ControlID="ImgBtnVID" />
                <asp:PostBackTrigger ControlID="ImgBtnHS" />
                <asp:PostBackTrigger ControlID="ImgHTmlCar" />
                <asp:PostBackTrigger ControlID="ImgVerCar" />
                <asp:PostBackTrigger ControlID="ImgGrabaCar" />
                <asp:PostBackTrigger ControlID="borra_alt1" />
                <asp:PostBackTrigger ControlID="ImgBtnAl1" />
                <asp:PostBackTrigger ControlID="borra_alt2" />
                <asp:PostBackTrigger ControlID="ImgBtnAl2" />
                <asp:PostBackTrigger ControlID="borra_alt3" />
                <asp:PostBackTrigger ControlID="LstCategorias1" />
                <asp:PostBackTrigger ControlID="LstCategorias2" />
                <asp:PostBackTrigger ControlID="LstCategorias3" />
                
                <asp:PostBackTrigger ControlID="borra_alt1" />
                <asp:PostBackTrigger ControlID="borra_alt2" />
                <asp:PostBackTrigger ControlID="borra_alt3" />
                <asp:PostBackTrigger ControlID="ImgBtnAl1" />
                <asp:PostBackTrigger ControlID="ImgBtnAl2" />
                <asp:PostBackTrigger ControlID="ImgBtnAl3" />
                <asp:PostBackTrigger ControlID="borra_rep1" />
                <asp:PostBackTrigger ControlID="borra_rep2" />
                <asp:PostBackTrigger ControlID="borra_rep3" />
                <asp:PostBackTrigger ControlID="ImgBtnRe1" />
                <asp:PostBackTrigger ControlID="ImgBtnRe2" />
                <asp:PostBackTrigger ControlID="ImgBtnRe3" />
                <asp:PostBackTrigger ControlID="borra_acc1" />
                <asp:PostBackTrigger ControlID="borra_acc2" />
                <asp:PostBackTrigger ControlID="borra_acc3" />
                <asp:PostBackTrigger ControlID="ImgBtnAc1" />
                <asp:PostBackTrigger ControlID="ImgBtnAc2" />
                <asp:PostBackTrigger ControlID="ImgBtnAc3" />
                <asp:PostBackTrigger ControlID="ImgBtnAddAL1" />
                <asp:PostBackTrigger ControlID="ImgBtnAddAL2" />
                <asp:PostBackTrigger ControlID="ImgBtnAddAL3" />
                <asp:PostBackTrigger ControlID="ImgBtnAddRE1" />
                <asp:PostBackTrigger ControlID="ImgBtnAddRE2" />
                <asp:PostBackTrigger ControlID="ImgBtnAddRE3" />
                <asp:PostBackTrigger ControlID="ImgBtnAddAC1" />
                <asp:PostBackTrigger ControlID="ImgBtnAddAC2" />
                <asp:PostBackTrigger ControlID="ImgBtnAddAC3" />

                <asp:PostBackTrigger ControlID="LnkBtn_Actualizar" />
                <asp:PostBackTrigger ControlID="LnkBtn_ActWeb" />
                <asp:PostBackTrigger ControlID="LnkBtn_Eliminar" />
                <asp:PostBackTrigger ControlID="ImgBtnAct_item" />
                <asp:PostBackTrigger ControlID="ImgBtnDesAct_item" />
                

            </Triggers>

        </asp:UpdatePanel>
        <script src="scripts/bootstrap.bundle.min.js"></script>
        <script src="scripts/feather.min.js"></script>
        <script src="scripts/Chart.min.js"></script>
        <script src="scripts/dashboard.js"></script>
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
