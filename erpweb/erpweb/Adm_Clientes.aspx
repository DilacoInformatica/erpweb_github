﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_Clientes.aspx.cs" Inherits="erpweb.Adm_Clientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <title>Administración Clientes</title>
    <script>
        function valida()
        {
            if (document.getElementById("txt_id").value == '' &&  document.getElementById("txt_rut").value == '' && document.getElementById("txt_razon").value == '')
            {
                alert('Ingrese criterio de búsqueda');
                document.getElementById("txt_id").focus();
                return false;
            }
        }
    </script>

    </head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-10">
                            <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;" />Administración de Clientes Sitio Web</h1> 
                        </div>
                        <div class="col-md-1 float-right">
                            <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" OnClick="LinkButton2_Click" Width="133px">Volver</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-3">
                 <h6><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h6>
            </div>
             <div class="col-md-3">
                <h6><span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h6>
            </div>
             <div class="col-md-3">
                <h6><span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h6>
            </div>
          </div>
     </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <h6><span class="badge bg-primary">Búsqueda de Información Sitio Web</span></h6>
        <div class="row">
            <div class="col-md-1"><h6><span class="badge bg-info">ID</span></h6>
                <asp:TextBox ID="txt_idw" runat="server" BackColor="#FFFFCC" Width="89px" CssClass="form-control"></asp:TextBox>
           </div>
            <div class="col-md-1"><h6><span class="badge bg-info">Rut</span></h6>
                <h6><asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC"  Width="145px" CssClass="form-control"></asp:TextBox></h6>
            </div>
            <div class="col-md-3"><h6><span class="badge bg-info">Razón Social</span></h6>
                <h6><asp:TextBox ID="txt_razonw" runat="server" Width="340px" BackColor="#FFFFCC" CssClass="form-control"></asp:TextBox></h6>
            </div>
            <div class="col-md-2"><h6><asp:Button ID="Btn_buscarw" runat="server" CssClass="btn btn-primary" Text="Buscar Cliente(s)" OnClick="Btn_buscarw_Click" Width="184px"/></h6></div>
            <div class="col-md-2"><h6><asp:Button ID="Btn_autorizar" runat="server" CssClass="btn btn-success" OnClick="Btn_autorizar_Click" Text="Autorizar Cliente(s)" Width="186px" /></h6></div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">
                <span>Clientes</span>
                <asp:GridView ID="lista_clientes" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1417px" ShowFooter="True" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSelected" runat="server" CssClass="form-control" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="Rut" HeaderText="Rut" />
                        <asp:BoundField DataField="Dv_rut" HeaderText="DV" />
                        <asp:BoundField DataField="Razon_Social" HeaderText="Razón Social" />
                        <asp:BoundField DataField="Telefonos" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Telefonos2" HeaderText="Teléfono2" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="Comuna" HeaderText="Comuna" />
                        <asp:BoundField DataField="Id_region" HeaderText="Región" />
                        <asp:BoundField DataField="Pais" HeaderText="País" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Giro" HeaderText="Giro" />
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
                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 121pt" width="161">
                    <colgroup>
                        <col style="mso-width-source: userset; mso-width-alt: 5888; width: 121pt" width="161" />
                    </colgroup>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h6><span><asp:Label ID="lbl_cantidad" CssClass="badge bg-success" runat="server" Width="747px"></asp:Label></span></h6>
            </div>
        </div>
    </div>
    <br />
      <div class="container-fluid rounded border border-secondary bg-light">
          <div class="row">
              <div class="col-md-12">
                  <asp:GridView ID="GrdErrores" runat="server" Visible ="False" CellPadding="4" ForeColor="#333333" GridLines="None">
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
        <h6><span class="badge bg-primary">Eliminar Clientes de Sitio Web</span></h6>
        <div class="row">
           <div class="col-md-2"><h6><span class="badge bg-info">Rut</span></h6>
               <h6><asp:TextBox ID="txt_rut" runat="server" CssClass="form-control" BackColor="#FFFFCC"  Width="145px"></asp:TextBox></h6></div>
           <div class="col-md-3"><h6><span class="badge bg-info">Razón Social</span></h6>
               <h6><asp:TextBox ID="txt_razon" runat="server" CssClass="form-control" Width="340px" BackColor="#FFFFCC"></asp:TextBox></h6></div>
            <div class="col-md-3"><h6><asp:Button ID="Btn_buscar" runat="server" CssClass="btn btn-primary" Text="Buscar Cliente(s)" OnClick="Btn_buscar_Click" Width="136px" /></h6></div>
            <div class="col-md-3"><h6><asp:Button ID="Btn_eliminaCLIWEB" runat="server" CssClass="btn btn-danger" OnClick="Btn_eliminaCLIWEB_Click" Text="Eliminar Cliente(s) Sitio Web" Width="185px" /></h6></div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">
               <asp:GridView ID="ClientesERP" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" Width="1700px" ShowFooter="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="check_selcli" runat="server" />
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
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lbl_resultados" CssClass="badge bg-success" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
