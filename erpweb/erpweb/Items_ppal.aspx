<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Items_ppal.aspx.cs" Inherits="erpweb.Ppal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="css/estilos.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title>Productos</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    <div class="container-fluid .bg-light">
	<div class="row">
		<div class="col-md-12">
            <div class="row">
		   <div class="col-md-11">
               			<h1 class="text-center text-primary">
				<img alt="" src="img/vineta.gif" />
				Productos Web
			</h1> 
		   </div>
		<div class="col-md-1">
            <asp:ImageButton ID="ImgBtn_Cerrar" runat="server" ImageUrl="~/img/cerrar.png" Width="37px" style="text-align: right" CssClass="auto-style1" Height="37px" />
		</div>
	    </div>

            <h4><span class="badge badge-info">Búsqueda de Información</span></h4>
			<div class="row bg-light text-dark">
				<div class="col-md-3">
					 <h5><span class="badge badge-info">Por palabra clave</span></h5>
                     <h5><asp:TextBox ID="txt_palabra_clave" runat="server" BackColor="#FFFFCC" Width="405px" CssClass="form-control"></asp:TextBox></h5>
				</div>
				<div class="col-md-4">
				</div>
			</div>
            <div class="row bg-light text-dark">
				<div class="col-md-3">
					 <h5><span class="badge badge-info">División</span></h5>
                     <h5><asp:DropDownList ID="LstDivision" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivision_SelectedIndexChanged" CssClass="form-control" Width="405px">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList></h5>
				</div>
                <div class="col-md-3">
                    <h5><span class="badge badge-info">Catgoría</span></h5>
                     <asp:DropDownList ID="LstCategorias" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged" CssClass="form-control" Width="405px">
                     <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                     </asp:DropDownList>
                    <h5></h5>
				</div>
				<div class="col-md-3">
                    <h5><span class="badge badge-info">Subcategoría</span></h5>
                    <h5><asp:DropDownList ID="LstSubCategorias" runat="server" AppendDataBoundItems="True" CssClass="form-control" Width="405px">
                       <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                       </asp:DropDownList></h5>
				</div>
			</div>
            <div class="row bg-light text-dark">
		        <div class="col-md-12">
			        <div class="row">
				        <div class="col-md-3">
                            <h5><span class="badge badge-info">Línea de Venta</span></h5>
                            <h5><asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True" CssClass="form-control" Width="405px">
                                <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList></h5>
				        </div>
				    <div class="col-md-3">
                        <h5><span class="badge badge-info">Letra</span></h5>
                        <h5><asp:DropDownList ID="LstLetras" runat="server" AppendDataBoundItems="True"  CssClass="form-control" Width="200px">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList></h5>
				    </div>
			     </div>
		      </div>
		</div>

       <div class="row bg-light text-dark">
		<div class="col-md-12">
			<div class="row">
				<div class="col-md-12">
                    <h5><span class="badge badge-info">Proveedor</span></h5>
                    <h5><asp:DropDownList ID="LstProveedores" runat="server" AppendDataBoundItems="True"  CssClass="form-control" Width="800px">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList></h5>
				</div>
			</div>
	    	</div>
	   </div>
        <div class="row bg-light text-dark">
		     <div class="col-md-12">
			        <div class="row">
				        <div class="col-md-3">
                            <h5><span class="badge badge-info">Código</span></h5>
                            <h5><asp:TextBox ID="txt_codigo" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="200px"></asp:TextBox></h5>
				        </div>
				    <div class="col-md-3">
                        <h5><span class="badge badge-info">Código Proveedor</span></h5>
                        <h5><asp:TextBox ID="txt_codprov" runat="server" BackColor="#FFFFCC" Width="200px" CssClass="form-control"></asp:TextBox></h5>
				    </div>
			     </div>
		      </div>
		</div>
        <div class="row bg-light text-dark">
		    <div class="col-md-2">
                <h4><span class="badge badge-info">Prod. sin Categoría</span></h4>
                <h4><asp:CheckBox ID="chk_sin_cat" runat="server" CssClass  ="form-check" /></h4>
		    </div>
		    <div class="col-md-2">
                <h4><span class="badge badge-info">Prod. Publicados</span></h4>
                <h4><asp:CheckBox ID="chk_publicados" runat="server" CssClass  ="form-controls"/></h4>
		    </div>
		    <div class="col-md-2">
                <h4><span class="badge badge-info">Prod. sin Imagénes</span></h4>
                <h4><asp:CheckBox ID="chk_sin_imagenes" runat="server" CssClass  ="form-controls"/></h4>
		    </div>
	    </div>
            <div class="row bg-light text-dark">
		    <div class="col-md-2">
                <h4><span class="badge badge-info">Prod. sólo Cotización</span></h4>
                <h4><asp:CheckBox ID="chk_cotizac" runat="server" CssClass  ="form-controls" /></h4>
		    </div>
             <div class="col-md-2">
                <h4><span class="badge badge-info">Prod. sólo Ventas</span></h4>
                <h4><asp:CheckBox ID="chk_ventas" runat="server" CssClass  ="form-controls" /></h4>
		    </div>
		    <div class="col-md-2">
                <h4><span class="badge badge-info">Prod. NO Publicados</span></h4>
                <h4><asp:CheckBox ID="chk_no_publicados" runat="server" CssClass  ="form-controls" /></h4>
		    </div>
	    </div>
        <div class ="row bg-light text-dark">
            <div class="col-md-3">
                <asp:Button ID="Btn_buscar" runat="server" OnClick="Btn_buscar_Click" Text="Buscar" Width="140px" CssClass="btn btn-md btn-primary active btn-block" />
            </div>
        </div>
	</div>
</div>
<br />
<br />
<div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
            <h3><asp:Label ID="lbl_error" runat="server" CssClass="form-control text-danger" Width="1599px"></asp:Label></h3>
		</div>
	</div>
	<div class="row">
		<div class="col-md-4">
            <h4><span class="badge badge-info">Cantidad Productos</span></h4>
            <h4><asp:Label ID="lbl_cantidad" runat="server" CssClass="form-control text-info" Width="146px"></asp:Label></h4>
		</div>
		<div class="col-md-4">
            <h4><span class="badge badge-info">Prod. Publicados</span></h4>
            <h4><asp:Label ID="lbl_prod_publicados" runat="server" CssClass="form-control text-info" Width="146px"></asp:Label></h4>
		</div>
		<div class="col-md-4">
             <asp:ImageButton ID="Excel" runat="server" ImageUrl="~/img/xls.gif" OnClick="Excel_Click" Width="59px"  CssClass="auto-style1" Height="59px" />
		</div>
	</div>
</div>
<br />

<div class="container-fluid ">
	<div class="row">
		<div class="col-md-12 bg-secondary">
            <h4>
              <asp:GridView ID="LstProductos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="772px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Ver"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Código" />
                <asp:BoundField HeaderText="Descripción" />
                <asp:BoundField HeaderText="Letra" />
                <asp:CheckBoxField HeaderText="A Pedido" />
                <asp:CheckBoxField HeaderText="Venta" />
                <asp:CheckBoxField HeaderText="Cotizacion" />
                <asp:BoundField HeaderText="Marca" />
                <asp:BoundField HeaderText="Pro" />
                <asp:BoundField />
                <asp:CheckBoxField HeaderText="Publicado Sitio" />
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
                </h4>
		</div>
	</div>
</div>








&nbsp;<div id="resultado" style="display:block">
            <asp:GridView ID="GridResultados" runat="server" Caption="Resultados" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False" ShowFooter="True" ShowHeaderWhenEmpty="True" CssClass="form-control">
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
        </div>
        <asp:GridView ID="Productos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="Productos_SelectedIndexChanged" AllowSorting="True" OnSorting="Productos_Sorting" ShowFooter="True" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField SelectText="Ver" ShowSelectButton="True" />
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
      
    </ContentTemplate>
      
    </asp:UpdatePanel>
    </form>
</body>
</html>

<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>
