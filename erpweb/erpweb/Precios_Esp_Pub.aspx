<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Precios_Esp_Pub.aspx.cs" Inherits="erpweb.Precios_Esp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Publicar precios especiales a Clientes en el Sitio Web</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-11">
                <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;"/>Publicar precios especiales a Clientes ERP en Sitio WEB</h1>
            </div>
            <div class="col-md-1 float-right">
                 <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_volver_Click">Volver</asp:LinkButton>
            </div>
            <p></p>
          </div>
     </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-sm-12">
                <h4><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span></h4>
                <h4><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h4>
                <h4><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h4>
            </div>
       </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <h4><span class="badge badge-primary">Búsqueda de Información</span></h4>
        <div class="row">
            <div class="col-md-1">
                <h4><span class="badge badge-info">id</span></h4>
            </div>
            <div class="col-md-1">
                <h4><asp:TextBox ID="txt_idw" runat="server" BackColor="#FFFFCC" Width="127px" CssClass="form-control"></asp:TextBox></h4>
            </div>
                <div class="col-md-1">
                <h4><span class="badge badge-info">Rut</span></h4>
            </div>
                <div class="col-md-1">
                <h4><asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="128px"></asp:TextBox></h4>
            </div>
            <div class="col-md-1">
                <h4><span class="badge badge-info">Razón Social</span></h4>
            </div>
            <div class="col-md-2">
                <h4><asp:TextBox ID="txt_razonw" runat="server" Width="200px"  CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox></h4>
            </div>
            <div class="col-md-3">
                <h4><asp:Button ID="Btn_buscarw" runat="server" Text="Buscar" CssClass="btn btn-md btn-primary active btn-block" OnClick="Btn_buscarw_Click" Width="91px"/></h4>
            </div>
          </div>
     </div>
     <br />
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
             <div class="col-md-12">
                <asp:GridView ID="lista" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" Width="1900px" AutoGenerateSelectButton="True" OnSelectedIndexChanged="lista_SelectedIndexChanged" ShowFooter="True">
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
             </div>
         </div>
     </div>
    <br />
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-xs-1">
                    <h6><span class="badge badge-info">ID</span></h6>
                    <h6><span><asp:Label ID="lbl_id" CssClass="form-check-label" runat="server" Width="136px"></asp:Label></span></h6>
                </div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-info">Rut</span></h6>
                     <h6><span><asp:Label ID="lbl_rut" runat="server"></asp:Label>
                        <asp:Label ID="lbl_dv" runat="server" CssClass="form-check-label" Visible="False"></asp:Label></span></h6>
                </div>
                <div class="col-md-1">
                     <h6><span class="badge badge-info">Razón Social</span></h6>
                     <h6><span><asp:Label ID="lbl_razon"  CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                <div class="col-md-1">
                     <h6><span class="badge badge-info">Teléfono</span></h6>
                     <h6><span><asp:Label ID="lbl_fono"  CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-info">Teléfono2</span></h6>
                     <h6><span><asp:Label ID="lbl_fono2" CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                <div class="col-md-3">
                     <h6><span class="badge badge-info">Dirección</span></h6>
                     <h6><span><asp:Label ID="lbl_direccion" CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                <div class="col-md-1">
                     <h6><span class="badge badge-info">Ciudad</span></h6>
                     <h6><span><asp:Label ID="lbl_ciudad" CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-info">Comuna</span></h6>
                     <h6><span><asp:Label ID="lbl_comuna" CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-info">Región</span></h6>
                     <h6><span><asp:Label ID="lbl_región" CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
                 <div class="col-md-1">
                     <h6><span class="badge badge-info">Email</span></h6>
                     <h6><span><asp:Label ID="lbl_email" CssClass="form-check-label" runat="server"></asp:Label></span></h6>
                </div>
            </div>
        </div>
        <br />
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-6">
                    <asp:Button ID="Btn_cargar" runat="server" OnClick="Btn_cargar_Click" CssClass="btn btn-md btn-success active" Text="Cargar Cliente y Productos Seleccionado(s) al Sitio Web" Width="445px" />
                </div>
            </div>
            <p></p>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="List_ProdEsp" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1900px" ShowFooter="True">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_selecciona" runat="server" />
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
                </div>
            </div>
        </div>
    



            


 
        


     
    </form>
</body>
</html>
