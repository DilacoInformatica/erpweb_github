<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="erpweb.Configuracion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Configuración</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-10">
                    <h1 class="text-center text-primary">
                        <img alt="" src="img/vineta.gif" style="width: 31px; height: 33px;" />Parámetros Sitio Web</h1>
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
                    <h6><span>
                        <asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span>
                        <asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h6>
                </div>
                <div class="col-md-4">
                    <h6><span>
                        <asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h6>
                </div>
                <div class="col-md-4">
                    <h6><span>
                        <asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h6>
                </div>
            </div>
        </div>
        <br />
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            Ingreso de reglas
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <span class="badge bg-info">Sigla</span>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txt_sigla" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-md-3">
                                    <span class="badge bg-info">Descripción</span>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txt_descrip" runat="server" Width="256px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <span class="badge bg-info">Valor</span>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txt_valor" runat="server" Width="186px"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Button ID="Btn_grabars" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClick="Btn_grabars_Click" />
                        </div>


                        <div class="card-footer">
                            Dilaco
                        </div>
                    </div>

                </div>
                <div class="col-md-6">
                    <asp:GridView ID="Lst_Info" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="Lst_Info_RowDataBound" OnRowCommand="Lst_Info_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="Id" />
                            <asp:BoundField DataField="sigla" HeaderText="Sigla" />
                            <asp:TemplateField HeaderText="Parámetro">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_paramatro" runat="server" Width="297px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_valor" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_activo" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="grabar" Text="Grabar" ControlStyle-CssClass="btn btn-success text-white" />
                            <asp:ButtonField CommandName="eliminar" Text="Eliminar" ControlStyle-CssClass="btn btn-danger text-white" />
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
