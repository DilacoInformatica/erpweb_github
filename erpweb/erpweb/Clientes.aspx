<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="erpweb.Clientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title>Clientes</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
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
        <div class="container-fluid rounded border border-secondary">
            <div class="row">
                <div class="col-md-11 ">
                    <h1>
                        <img alt="" src="img/vineta.gif" /><span class="nuevoEstilo2">Administración de Clientes Sitio Web</span></h1>
                </div>
                <div class="col-md-1">
                    <asp:ImageButton ID="ImgBtn_Cerrar" runat="server" Height="25px" ImageUrl="~/img/cerrar.png" Width="25px" />
                </div>
            </div>
        </div>
    <br />
    <div class="container-fluid rounded border border-secondary">
                    <div class="row">
            <div class="col-sm-12">
                <h4><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span></h4>
                <h4><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h4>
                <h4><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h4>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary">
            <div class="row">
                <div class="col-md-6">
                    <h4><span class="badge badge-primary">Búsqueda de Clientes en Sitio Web</span></h4>
                </div>
            </div>
            <div class="row">
                <div class ="col-md-1"><h4><span class="badge badge-info">ID</span></h4></div>
                <div class ="col-md-2"><h4><asp:TextBox ID="txt_idw" runat="server" BackColor="#FFFFCC" Width="127px" CssClass="form-control"></asp:TextBox></h4></div>
                <div class ="col-md-1"><h4><span class="badge badge-info">Rut</span></h4></div>
                <div class ="col-md-2"><h4><asp:TextBox ID="txt_rutw" runat="server" BackColor="#FFFFCC"  Width="127px" CssClass="form-control"></asp:TextBox></h4></div>
                <div class ="col-md-1"><h4><span class="badge badge-info">Razón Social</span></h4></div>
                <div class ="col-md-2"><h4><asp:TextBox ID="txt_razonw" runat="server" Width="337px" CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox></h4></div>
                <div class ="col-md-2"><h4><asp:Button ID="Btn_buscarw" runat="server" Text="Buscar" OnClick="Btn_buscarw_Click" Width="80px" CssClass="btn btn-md btn-primary active btn-block float-md-right"/></h4></div>
            </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary">
            <div class="row">
                <div class="col-md-12">
                        <asp:GridView ID="lista_clientes" runat="server"   CellPadding="4" ForeColor="#333333" GridLines="None"  Width="1757px">
                        <AlternatingRowStyle BackColor="White" BorderStyle="None" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_elimina" runat="server" />
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
    <br />
    <div class="container-fluid rounded border border-secondary">
            <div class ="row">
                <div class="col-md-6">
                    <h5><asp:Button ID="Btn_eliminaCLIWEB" runat="server" CssClass="btn btn-md btn-primary active btn-danger" Text="Eliminar Cliente(s) del Sitio Web" Width="376px" OnClick="Btn_eliminaCLIWEB_Click" /></h5>
                </div>
            </div>
    </div>
    <br />
        <div class="container-fluid rounded border border-secondary">
            <div class="row">
                <div class="col-md-6">
                    <h4><span class="badge badge-primary">Búsqueda de Clientes en ERP</span></h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <h4><span class="badge badge-info">ID</span></h4>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txt_id" runat="server" BackColor="#FFFFCC" Width="127px" CssClass="form-control"></asp:TextBox></div>
                <div class="col-md-1">
                    <h4><span class="badge badge-info">Rut</span></h4>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txt_rut" runat="server" BackColor="#FFFFCC" Width="127px" CssClass="form-control"></asp:TextBox></div>
                <div class="col-md-1">
                    <h4><span class="badge badge-info">Razón Social</span></h4>
                </div>
                <div class="col-md-2">
                    <h4>
                        <asp:TextBox ID="txt_razon" runat="server" Width="337px" CssClass="form-control" BackColor="#FFFFCC"></asp:TextBox></h4>
                </div>
                <div class="col-md-2">
                    <h4>
                        <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" OnClick="Btn_buscar_Click" Width="104px" CssClass="btn btn-md btn-primary active btn-block float-md-right" /></h4>
                </div>
            </div>
        </div>
    <br />
    <div class ="container-fluid rounded border border-secondary">
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="ClientesERP" runat="server" CellPadding="4"  ForeColor="#333333" GridLines="None" Width="1757px">
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
    </div>
    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <h4><asp:Label ID="lbl_resultados" CssClass="badge badge-warning" runat="server" Width="314px"></asp:Label></h4>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <h4><asp:Button ID="Btn_cargarCliERP" runat="server" Text="Cargar Cliente(s) al Sitio Web"  CssClass="btn btn-md btn-primary active btn-success" OnClick="Btn_cargarCliERP_Click" Visible="False" /></h4>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
<script>
    function salir() {
        if (confirm('Cerrar página, Seguro desea proceder?'))
        { window.close(); }
    }
</script>
