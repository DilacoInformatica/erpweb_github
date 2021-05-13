<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Items_ppal.aspx.cs" Inherits="erpweb.Ppal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title>Productos</title>
    </head>
<body>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    <form id="form1" runat="server">
    <%-- Maqueta Boostrap --%> 
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-10">
                            <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;" />Productos Web</h1> 
                        </div>
                        <div class="col-md-1 float-right">
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-outline-success" OnClick="LinkButton2_Click" Width="133px">Volver</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-3">
                <h6><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span>
            </div>
             <div class="col-md-3">
                <h6><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h6>
             </div>
             <div class="col-md-3">
                <h6><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h6>
            </div>
       </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light"">
            <h4><span class="badge badge-primary">Búsqueda de Información</span></h4>
            <div class="row">
                <div class="col-md-4">
                    <h6><span class="badge badge-info">Por palabra clave</span></h6>
                    <h6><asp:TextBox ID="txt_palabra_clave" runat="server" BackColor="#FFFFCC" Width="405px" CssClass="form-control"></asp:TextBox></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Código</span></h6>
                    <h6><asp:TextBox ID="txt_codigo" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="200px"></asp:TextBox></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Código Prov.</span></h6>
                    <h6><asp:TextBox ID="txt_codprov" runat="server" BackColor="#FFFFCC" Width="200px" CssClass="form-control"></asp:TextBox></h6>
                </div>
            </div>
            <div class="row">
                 <div class="col-md-2">
                    <h6><span class="badge badge-info">División</span></h6>
                    <h6><asp:DropDownList ID="LstDivision" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstDivision_SelectedIndexChanged" CssClass="form-control" Width="196px">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Categoría</span></h6>
                    <h6><asp:DropDownList ID="LstCategorias" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="LstCategorias_SelectedIndexChanged" CssClass="form-control" Width="196px">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Subcategoría</span></h6>
                    <h6><asp:DropDownList ID="LstSubCategorias" runat="server" AppendDataBoundItems="True" CssClass="form-control" Width="196px">
                       <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                       </asp:DropDownList>
                    </h6>
                </div>
                <div class="col-md-2">
                     <h6><span class="badge badge-info">Línea de Venta</span></h6>
                    <h6><asp:DropDownList ID="LstLineaVtas" runat="server" AppendDataBoundItems="True" CssClass="form-control" Width="196px">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                         </asp:DropDownList></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Letra</span></h6>
                    <h6><asp:DropDownList ID="LstLetras" runat="server" AppendDataBoundItems="True"  CssClass="form-control" Width="80px">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList></h6>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <h6><span class="badge badge-info">Proveedor</span></h6>
                    <h4><asp:DropDownList ID="LstProveedores" runat="server" AppendDataBoundItems="True"  CssClass="form-control" Width="739px" >
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                        </asp:DropDownList></h4>
                </div>
            </div>
            <div class="row">

            </div>
            <div class="row">
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Sin Categoría</span></h6>
                    <h6><asp:CheckBox ID="chk_sin_cat" runat="server" CssClass  ="form-check" /></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Publicados</span></h6>
                    <h6><asp:CheckBox ID="chk_publicados" runat="server" CssClass  ="form-check"/></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Sin Imagénes</span></h6>
                    <h6><asp:CheckBox ID="chk_sin_imagenes" runat="server" CssClass  ="form-check"/></h6>
                </div>
                <div class="col-md-2">
                     <h6><span class="badge badge-info">Sólo Cotización</span></h6>
                    <h6><asp:CheckBox ID="chk_cotizac" runat="server" CssClass  ="form-check" /></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">Sólo Ventas</span></h6>
                    <h6><asp:CheckBox ID="chk_ventas" runat="server" CssClass  ="form-check" /></h6>
                </div>
                <div class="col-md-2">
                    <h6><span class="badge badge-info">NO Publicados</span></h6>
                    <h6><asp:CheckBox ID="chk_no_publicados" runat="server" CssClass  ="form-check" /></h6>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="Btn_buscar" runat="server" OnClick="Btn_buscar_Click" Text="Buscar" Width="140px" CssClass="btn btn-primary btn-responsive btninter" />
                </div>
            </div>
        <p></p>
        </div>
        
    </div>
<br />

<div class="container-fluid rounded border border-secondary">
        <div class="row">
            <div class="col-md-2">
                <h6><span class="badge badge-info">Cantidad Productos</span></h6>
            </div>
            <div class="col-md-1">
                <h6><asp:Label ID="lbl_cantidad" runat="server" CssClass="form-control text-info" Width="88px"></asp:Label></h6>
            </div>
            <div class="col-md-2">
                <h6><span class="badge badge-info">Prod. Publicados</span></h6>
            </div>
            <div class="col-md-1">
                <h6><asp:Label ID="lbl_prod_publicados" runat="server" CssClass="form-control text-info" Width="88px"></asp:Label></h6>
            </div>
            <div class="col-md-4">
                    <asp:ImageButton ID="Excel" runat="server" ImageUrl="~/img/xls.gif" OnClick="Excel_Click" Width="30px"  Height="30px" />
		    </div>
        </div>
</div>
<br />

<div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <h6>
                        <asp:GridView ID="Productos" runat="server" CellPadding="4"  ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="Productos_SelectedIndexChanged" AllowSorting="True" ShowFooter="True" ShowHeaderWhenEmpty="True">
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
                    </h6>
          </div>
    </div>
</div>

    </form>
      </ContentTemplate>
    </asp:UpdatePanel>               
</body>
</html>

<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>
