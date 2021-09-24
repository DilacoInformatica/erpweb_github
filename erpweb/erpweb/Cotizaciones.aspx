<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="erpweb.Cotizaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cotizaciones</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-10">
                    <h1 class="text-center text-primary">
                        <img alt="" src="img/vineta.gif" style="width: 31px; height: 33px;" />Cotizaciones generadas en Sitio Web</h1>
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
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <h6><span>
                        <asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span>
                        <asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h6>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <h6><span>
                        <asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h6>
                </div>
                <div class="ccol-md-12 col-xs-12 col-sm-12">
                    <h6><span>
                        <asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h6>
                </div>
            </div>
        </div>
        <br />
        <div class="container-fluid rounded border border-secondary bg-ligh">
            <div class="row">
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <h4><span class="badge bg-primary">Búsqueda de Información</span></h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-xs-4 col-sm-4">
                    <span class="badge bg-info">Cotización</span>
                    <asp:TextBox ID="txt_cotizacion" runat="server" CssClass="form-control" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                </div>
                <div class="col-md-2 col-xs-4 col-sm-4">
                    <span class="badge bg-info">Rut Cliente</span>
                    <asp:TextBox ID="txt_rut" runat="server" Width="121px" CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox>
                </div>
                <div class="col-md-2 col-xs-4 col-sm-4">
                    <span class="badge bg-info">Estado Cotización</span>
                    <asp:DropDownList ID="LstEstados" runat="server" CssClass="form-select" Width="296px" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click" CssClass="btn btn-primary btn-responsive btninter" />
                </div>
                
            </div>
        </div>
        <br />
        <div class="container-fluid bg-light">
            <div class="row">
                <div class="col-md-6">
                    <h6>
                        <asp:Label ID="lbl_cantidad" runat="server"></asp:Label></h6>
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
                    <asp:GridView ID="Lista_cotizacion" CssClass="table table-responsive-md" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_cotizacion_SelectedIndexChanged" Width="1350px" AutoGenerateColumns="False" OnRowDataBound="Lista_cotizacion_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Rut" HeaderText="Rut" />
                            <asp:BoundField DataField="Dv" HeaderText="Dv" Visible="False" />
                            <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellidos" />
                            <asp:BoundField DataField="Id_cotizacion" HeaderText="Id" Visible="False" />
                            <asp:BoundField DataField="Cotizac_Num" HeaderText="Cotización" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="ERP" HeaderText="Cliente ERP" />
                            <asp:BoundField DataField="Pais" HeaderText="País" />
                            <asp:TemplateField HeaderText="N° Cot ERP">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_num_cot_erp" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="estado" Visible="true" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" HeaderText="Cliente ERP">
                                <HeaderStyle CssClass="ColumnaOculta"></HeaderStyle>

                                <ItemStyle CssClass="ColumnaOculta"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_cot" HeaderText="Tipo" />
                            <asp:TemplateField HeaderText="Estado">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Estado") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="img_estado" runat="server" CssClass="form-control" Height="40px" Width="40px" />
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

        <div class="container-fluid rounded">
            <div class="row">
                <div class="col-md-12 col-xs-4 col-sm-4">
                    
                        <asp:Label ID="lbl_mensaje" runat="server" CssClass="badge bg-warning"></asp:Label>
                </div>
            </div>
        </div>
        <div>
        </div>

    </form>
</body>
</html>
