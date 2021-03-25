<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Precios_Esp_Adm.aspx.cs" Inherits="erpweb.Precios_Esp_Adm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administración Clientes y precios especiales en sitio web</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style4">
    <div class="">
        <div class="row">
            <div class="col-md-11">
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
            <div class="auto-style5">
                 <h4><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span></h4>
                <h4 class="auto-style2"><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h4>
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
                <asp:TextBox ID="txt_idw" runat="server" CssClass="form-control" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
            </div>
             <div class="col-md-1">
                 <h4><span class="badge badge-info">Rut</span></h4>
            </div>
             <div class="col-md-1">
                 <asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC" CssClass="form-control" Width="128px"></asp:TextBox>
            </div>
            <div class="col-md-1">
                 <h4><span class="badge badge-info">Razón Socials</span></h4>
            </div>
            <div class="col-md-3">
                 <h4><asp:TextBox ID="txt_razonw" runat="server" CssClass="form-control" Width="266px" BackColor="#FFFFCC"></asp:TextBox></h4>
            </div>
           <div class="col-md-2">
               <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="89px" CssClass="btn btn-md btn-primary active" OnClick="Btn_buscar_Click" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="Btn_Nuevo" runat="server" Text="Nuevo" Width="89px" OnClick="Btn_Nuevo_Click" CssClass="btn btn-md btn-success active" />
            </div>
        </div>
    </div>
     <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="Btn_Eliminar" runat="server" Text="Eliminar Información Cliente-Precio del Sitio Web" CssClass="btn btn-md btn-danger active" OnClick="Btn_Eliminar_Click" />
            </div>
        </div>
        <p></p>
        <div class="row">
            <div class="col-md-12">
                 <asp:GridView ID="Grilla" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1800px" ShowFooter="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Seleccionar">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_selecciona" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
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
                <h5><asp:Label ID="lbl_mensaje" CssClass="badge badge-success" runat="server"></asp:Label></h5>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
