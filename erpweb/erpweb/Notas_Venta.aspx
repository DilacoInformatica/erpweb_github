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
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid rounded border border-secondary bg-light">
                <div class="row">
                    <div class="col-md-11">
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
                    <div class="col-md-12">
                        <h4><span class="badge badge-primary">Búsqueda de Información</span></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-1">
                        <h4><span class="badge badge-info">Nota de Venta</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txt_nv" runat="server" BackColor="#FFFFCC" Width="127px"></asp:TextBox>
                    </div>
                    <div class="col-1">
                        <h4><span class="badge badge-info">Rut Cliente</span> </h4>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txt_rut" runat="server" Width="121px" BackColor="#FFFFCC"></asp:TextBox>
                    </div>
                     <div class="col-2">
                         <asp:Button ID="Btn_buscar" runat="server" Text="Buscar" Width="91px" OnClick="Btn_buscar_Click" CssClass="btn btn-md btn-primary active btn-block"/>
                    </div>
                </div>
        </div>
           <br />
    <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-12">
                <h4><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span>
                </h4>
                <h4><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h4>
                <h4><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger"></asp:Label></span></h4>
            </div>
       </div>
    </div>
    <br />
        <br />        <div class="container-fluid bg-light">
                <div class="row">
                    <div class="col-md-6">
                       <h4><asp:Label ID="lbl_cantidad" runat="server" CssClass="badge badge-primary"></asp:Label></h4>
                    </div>
                </div>
                
        </div>
        
        <br />
        <div class="container-fluid rounded border border-secondary bg-light">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="Lista_notas" CssClass="table table-responsive-md" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Lista_notas_SelectedIndexChanged" ShowFooter="True" HorizontalAlign="Justify" OnRowDataBound="Lista_notas_RowDataBound" Width="1900px">
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

        <div class="container-fluid rounded border border-secondary bg-light">
                <div class="row">
                    <div class="col-md-12">
                        <h4><asp:Label ID="lbl_mensaje" runat="server" CssClass="badge badge-warning"></asp:Label></h4>
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
