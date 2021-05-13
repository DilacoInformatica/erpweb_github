<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_Publicacion_Lineas_Prod.aspx.cs" Inherits="erpweb.Adm_Publicacion_Lineas_Prod__" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administrador Líneas de Venta en Sitio Web</title>
     <link href="Content/bootstrap.css" rel="stylesheet" />
     <style type="text/css">
         .auto-style4 {
             position: relative;
             left: 0px;
             top: 0px;
         }
     </style>
     </head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-10">
                    <h1 class="text-center text-primary">
                        <img alt="" src="img/vineta.gif" style="width:31px;height:33px;" />Administración Categorías y Subcategorías</h1>
                </div>
                <div class="col-md-1 float-right">
                    <asp:LinkButton ID="Btn_Volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_Volver_Click">Volver</asp:LinkButton>
                </div>
                <p></p>
            </div>
        </div>
            <br />
    <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-12">
                 <h5><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span></h5>
                <h5><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h5>
                <h5><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h5>
            </div>
       </div>
    </div>
    <br />

    <div class="container-fluid rounded border border-secondary bg-light ">
	<div class="row">
		<div class="col-md-9">
			<h3 class="text-left text-primary">
				Búsqueda de Información
			</h3>
			<div class="row">
				<div class="col-md-2">
					 <h4><span class="badge badge-success">División</span></h4>
                         <div class="dropdown">
                             <h5>
                         <asp:DropDownList ID="LstDivision" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivivion_SelectedIndexChanged">
                         <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                          </asp:DropDownList></h5>
                     </div>
				</div>
				<div class="col-md-2">
					 <h4><span class="badge badge-success">Catergoría</span></h4>
                         <div class="dropdown">
                             <h5>
                        <asp:DropDownList ID="LstCategoria" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="LstCategoria_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList></h5>
                     </div>
                     
				</div>
				<div class="col-md-2">
					 <h4><span class="badge badge-success">Subcategoria</span></h4>
                          <div class="auto-style4">
                             <h4>
                         <asp:DropDownList ID="LstSubCategoria" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                     </h4>
                     </div>
				</div>
                <div class="col-md-2">
                    <h4><span class="badge badge-success">Ver Inactivos</span></h4>
                    <asp:CheckBox ID="Chk_Activos" runat="server" Width="288px" CssClass="form-check"/>
                </div>
                 <div class="col-md-1">
                    <asp:Button ID="Btn_Buscar" runat="server" OnClick="Btn_Buscar_Click" Text="Buscar" CssClass="btn btn-primary" />
                </div>
                 <div class="col-md-1">
                    <asp:Button ID="Btn_Grabar" runat="server"  Text="Grabar" CssClass="btn btn-success" OnClick="Btn_Grabar_Click" />
                </div>
			</div> 
           
                    
		</div>
	</div>
</div>
<br />
    <div class="container-fluid rounded border border-secondary bg-light">
	<div class="row">
		<div class="col-md-6">
			<h3 class="text-center text-info">
				ERP
			</h3>
			<div class="row">
				<div class="col-md-1"> 
                    <h6><span class="badge badge-default">ID:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Id_ERP" runat="server" Width="41px" Enabled="False" CssClass="form-control"></asp:TextBox></span>         
				</div>
                <div class="col-md-1">
                     <h6><span class="badge badge-default">Código:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Cod_ERP" runat="server" Enabled="False" CssClass="form-control"  Width="57px"></asp:TextBox></span>  
				</div>
				<div class="col-md-4">
                     <h6><span class="badge badge-default">Descripción:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Desc_ERP" runat="server" Width="234px" Enabled="False" CssClass="form-control"></asp:TextBox></span>  
				</div>
                <div class="col-md-1">
                     <h6><span class="badge badge-default">Orden:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Orden_ERP" runat="server" Width="57px" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </span>  
				</div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-default">Activo:</span></h6>
                     <div class="row justify-content-center">
                        <span class="badge badge-default"><asp:CheckBox ID="Chk_Activo_ERP" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="Chk_Activo_ERP_CheckedChanged" /></span>
                     </div>       
				</div>
                <div class="col-md-1">
                     <h6><span class="badge badge-default">En Sitio:</span></h6>
                    <div class="row justify-content-center">
                        <span class="badge badge-default"><asp:CheckBox ID="Chk_En_Sitio_ERP" runat="server" CssClass="form-control" /></span> 
                    </div>
				</div>
			</div>
		</div>
		<div class="col-md-6">
			<h3 class="text-center text-info">
				WEB</h3>
			<div class="row">
				<div class="col-md-1"> 
                    <h6><span class="badge badge-default">ID:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Id_WEB" runat="server" Width="50px" Enabled="False" CssClass="form-control"></asp:TextBox></span>         
				</div>
                <div class="col-md-1"> 
                    <h6><span class="badge badge-default">Código:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Cod_WEB" runat="server" Width="57px" Enabled="False" CssClass="form-control"></asp:TextBox></span>         
				</div>
				<div class="col-md-4">
                     <h6><span class="badge badge-default">Descripción:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Desc_WEB" runat="server" Width="200px" Enabled="False" CssClass="form-control"></asp:TextBox></span>  
				</div>
                <div class="col-md-3">
                     <h6><span class="badge badge-default">Etiqueta:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Label_WEB" runat="server" Width="200px" CssClass="form-control"></asp:TextBox></span>  
				</div>
                <div class="col-md-1">
                     <h6><span class="badge badge-default">Orden:</span></h6>
                    <span class="badge badge-default"><asp:TextBox ID="Txt_Orden_WEB" runat="server" Width="53px" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </span>  
				</div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-default">Activo:</span></h6>
                     <div class="row justify-content-center">
                        <span class="badge badge-default"><asp:CheckBox ID="Chk_Activo_WEB" runat="server" CssClass="form-control" Enabled="False" /></span>
                     </div>       
				</div>
                <div class="col-md-1">
                     <h6><span class="badge badge-default">Publicar:</span></h6>
                    <div class="row justify-content-center">
                        <span class="badge badge-default"><asp:CheckBox ID="Chk_Publicado_Web" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="Chk_Publicado_Web_CheckedChanged" /></span> 
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>
<br />
        
<div class="container-fluid rounded border border-secondary bg-light">
	<div class="row">
		<div class="col-md-12 d-flex justify-content-center">
            <h5>
             <asp:GridView ID="GrdCategoriasERP" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="189px" ShowFooter="True" Width="1303px" AutoGenerateColumns="False" OnRowDataBound="GrdCategoriasERP_RowDataBound" OnSelectedIndexChanged="GrdCategoriasERP_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField SelectText="Ver" ShowSelectButton="True" />
                                <asp:BoundField DataField="ID_Categoria" HeaderText="ID"  >
                                <ControlStyle CssClass="form-control" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo" HeaderText="Código" >
                                <ControlStyle CssClass="form-control" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Categría" >
                                <ControlStyle CssClass="form-control" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Orden" HeaderText="Orden" >
                                <ControlStyle CssClass="form-control" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_ActivoCAT" runat="server" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publicado Sitio">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_CatPublicada" runat="server" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Etiqueta Web">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_etiqueta_web" runat="server" Width="700px" CssClass="form-control"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
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
                </h5>
		</div>
		
	</div>
    <div class="row">
        <div class="col-md-12 d-flex justify-content-center">
            <h5>
             <asp:GridView ID="GrdSubCatERP" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1304px" AutoGenerateColumns="False" OnRowDataBound="GrdSubCatERP_RowDataBound" ShowFooter="True" Height="189px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID_SubCategoria" HeaderText="ID" >
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="SubCategoría"  >
                                <ControlStyle CssClass="form-control" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Orden" HeaderText="Orden" >
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_activo" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="form-control" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publicado Sitio">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_PubSubCat" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="form-control" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Etiqueta Web">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_etiqueta_web" runat="server" Width="700px" CssClass="form-control"></asp:TextBox>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="form-control" />
                                </asp:TemplateField>
                            </Columns>
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
            </h5>
        </div>
    </div>
</div>
                <br />
                        <asp:Label ID="lbl_cat" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
