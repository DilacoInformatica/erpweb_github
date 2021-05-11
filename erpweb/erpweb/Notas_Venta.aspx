<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notas_Venta.aspx.cs" Inherits="erpweb.Notas_Venta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Administración Notas de Venta generadas en el Sitio Web</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script>
        function valida()
        {
            if (document.getElementById("txt_nv").value == '' &&  document.getElementById("txt_rut").value == '')
            {
                alert('Ingrese criterio de búsqueda');
                document.getElementById("txt_id").focus();
                return false;
            }
        }
    </script>
   <style type="text/css">
        .ColumnaOculta {display:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid rounded border border-secondary bg-light">
                <div class="row">
                    <div class="col-md-10">
                        <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;" />Notas de Venta generadas en Sitio Web</h1>
                    </div>
                    <div class="col-md-1 float-right">

                        <asp:LinkButton ID="Btn_Volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_Volver_Click">Volver</asp:LinkButton>
                    </div>
                    <p></p>
                </div>
        </div>
        <br />
         <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span>
                </h5>
            </div>
            <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h5>
            </div>
            <div class="col-md-4">
                <h5<span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger text-dark"></asp:Label></span></h5>
            </div>
       </div>
    </div>
    <br />
        <div class="container-fluid rounded border border-secondary bg-light">
                <div class="row">
                    <div class="col-md-12">
                        <h6><span class="badge badge-primary">Búsqueda de Información</span></h6>
                    </div>
                </div>
                <div class="row">
                    <div class="col-1">
                        <h6><span class="badge badge-info">Nota de Venta</span> </h6>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txt_nv" runat="server" CssClass="form-control" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                    </div>
                    <div class="col-1">
                        <h6><span class="badge badge-info">Rut Cliente</span> </h6>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txt_rut" runat="server" CssClass="form-control" Width="121px" BackColor="#FFFFCC"></asp:TextBox>
                    </div>
                    <div class="auto-style1">
                        <h6><span class="badge badge-info">Estado NV</span> </h6>
                    </div>
                     <div class="col-md-2">
                        <h6><asp:DropDownList ID="LstEstadoNV" Width="296px" CssClass="form-control" runat="server"></asp:DropDownList></h6>
                    </div>
                     <div class="col-2">
                         <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click" CssClass="btn btn-md btn-primary active btn-block"/>
                    </div>
                </div>
        </div>
        
        <br />        <div class="container-fluid bg-light">
                <div class="row">
                    <div class="col-md-6">
                       <h6><asp:Label ID="lbl_cantidad" runat="server" CssClass="badge badge-primary"></asp:Label></h6>
                    </div>
                </div>
                
        </div>
        
        <br />
        <div class="container-fluid rounded border border-secondary bg-light">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="Lista_notas" CssClass="table table-responsive-md" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_notas_SelectedIndexChanged" ShowFooter="True" HorizontalAlign="Justify" OnRowDataBound="Lista_notas_RowDataBound" Width="1467px" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Nta_vta_num" HeaderText="Nota Venta" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="Id_cliente" HeaderText="Id Cliente" />
                                <asp:BoundField DataField="rut" HeaderText="Rut" />
                                <asp:BoundField DataField="Razon_Social" HeaderText="Cliente" />
                                <asp:BoundField DataField="neto" HeaderText="Neto" />
                                <asp:BoundField DataField="Tax_venta" HeaderText="IVA" />
                                <asp:BoundField DataField="Suma_total" HeaderText="Total" />
                                <asp:BoundField DataField="No_transaccion_web" HeaderText="N° Transac. Webpay" />
                                <asp:BoundField DataField="Status_SitioWeb" HeaderText="Estado" ItemStyle-CssClass="ColumnaOculta"  HeaderStyle-CssClass="ColumnaOculta" />
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Image ID="img_estado" runat="server" Height="36px" Width="36px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="N° NV ERP">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_num_nv_erp" runat="server"></asp:Label>
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

        <div class="container-fluid rounded border border-secondary bg-light">
                <div class="row">
                    <div class="col-md-12">
                        <h6><asp:Label ID="lbl_mensaje" runat="server" CssClass="badge badge-warning"></asp:Label></h6>
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
