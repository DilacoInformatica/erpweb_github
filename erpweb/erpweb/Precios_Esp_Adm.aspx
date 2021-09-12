<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Precios_Esp_Adm.aspx.cs" Inherits="erpweb.Precios_Esp_Adm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administración Clientes y precios especiales en sitio web</title>
         <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server" class="auto-style4">
    <div class="">
        <div class="row">
            <div class="col-md-10">
                <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;"/>Precios Especieales Publicados</h1>
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
            <div class="col-md-4">
                 <h6><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h6>
            </div>
            <div class="col-md-4">
                <h6><span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h6>
            </div>
            <div class="col-md-4">
                <h6><span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h6>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <h6><span class="badge bg-primary">Búsqueda de Información</span></h6>
        <div class="row">
            <div class="col-md-2">
                <h5><span class="badge bg-info">id</span>
                <asp:TextBox ID="txt_idw" runat="server" CssClass="form-control" BackColor="#FFFFCC" Width="127px"></asp:TextBox></h5>
            </div>
             <div class="col-md-2">
                 <h5><span class="badge bg-info">Rut</span>
                 <asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="128px"></asp:TextBox></h5>
            </div>
            <div class="col-md-3">
                <h5><span class="badge bg-info">Razón Social</span>
                 <asp:TextBox ID="txt_razonw" runat="server" CssClass="form-control" Width="266px" BackColor="#FFFFCC"></asp:TextBox></h5>
            </div>
           <div class="col-md-1">
               <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="170px" CssClass="btn btn-primary btn-responsive btninter" OnClick="Btn_buscar_Click" />
            </div>
            <div class="auto-style1">
                <asp:Button ID="Btn_Nuevo" runat="server" Text="Nuevo Precio Especial" Width="172px" OnClick="Btn_Nuevo_Click" CssClass="btn btn-success btn-responsive btninter" />
            </div>
        </div>
    </div>
     <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="Btn_Eliminar" runat="server" Text="Eliminar Información Cliente-Precio del Sitio Web" CssClass="btn btn-md btn-danger active" OnClick="Btn_Eliminar_Click" Width="399px" />
            </div>
        </div>
        <p></p>
        <div class="row">
            <div class="col-md-12">
                 <asp:GridView ID="Grilla" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1388px" ShowFooter="True" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_selecciona" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id_cliente" HeaderText="Id" />
                    <asp:BoundField DataField="rut" HeaderText="Rut" />
                    <asp:BoundField DataField="razon_social" HeaderText="Razón Social" />
                    <asp:BoundField DataField="ID_Item" HeaderText="Id Item" />
                    <asp:BoundField DataField="codigo" HeaderText="Código" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="moneda" HeaderText="Moneda" />
                    <asp:BoundField DataField="precio_lista" HeaderText="Precio Lista" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" />
                    <asp:BoundField DataField="fecha_vigencia" HeaderText="Fecha Hasta" />
                    <asp:BoundField DataField="Vigente" HeaderText="Vigente" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataTemplate>
                    <asp:CheckBox ID="Chk_elimina" runat="server" />
                </EmptyDataTemplate>
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
        <div class="row">
            <div class="col-md-6">
                <h5><asp:Label ID="lbl_mensaje" CssClass="badge bg-success" runat="server"></asp:Label></h5>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
