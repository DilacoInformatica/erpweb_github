<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="erpweb.Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cotizaciones</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-11">
                        <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;" />Cotizaciones generadas en Sitio Web</h1>
                    </div>
                    <div class="col-md-1 float-right">
                        <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" OnClick="LinkButton2_Click" Width="133px">Volver</asp:LinkButton>
                    </div>
                    <p></p>
            </div>
    </div>     
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
          <div class="row">
            <div class="col-md-12">
                   <h6><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span></h6>
            </div>
            <div class="col-md-12">
                <h6><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h6>
            </div>
            <div class="col-md-12">
                <h6><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h6>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-ligh">
            <div class="row">
                <div class="col-md-12">
                    <h3><span class="badge badge-primary">Búsqueda de Información</span></h3>
                </div>
            </div>
            <div class="row">
                    <div class="col-md-1">
                        <h6><span class="badge badge-info">Cotización</span> </h6>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txt_cotizacion" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <h6><span class="badge badge-info">Rut Cliente</span> </h6>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txt_rut" runat="server" Width="121px" BackColor="#FFFFCC"></asp:TextBox>
                    </div>
                     <div class="col-md-2">
                         <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click" CssClass="btn btn-md btn-primary active btn-block"/>
                    </div>
            </div>
    </div>
        <br />
   <div class="container-fluid bg-light">
            <div class="row">
                    <div class="col-md-6">
                       <h6><asp:Label ID="lbl_cantidad" runat="server"></asp:Label></h6>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-6">
                    </div>
            </div>
   </div>
    <br />
      <div class="container-fluid border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="Lista_cotizacion" CssClass="table table-responsive-md" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_cotizacion_SelectedIndexChanged" Width="1776px">
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

      <div class="container-fluid rounded">
            <div class="row">
                <div class="col-md-12">
                    <h6><asp:Label ID="lbl_mensaje" runat="server" CssClass="badge badge-warning"></asp:Label></h6>
                </div>
            </div>
        </div>
    <div>

    </div>

    </form>
</body>
</html>
