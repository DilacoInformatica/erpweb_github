<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock_Masivo.aspx.cs" Inherits="erpweb.Stock_Masivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Actualización Masiva de Items (Stock)</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="scripts/bootstrap.js"></script>
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-11">
                <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;"/>Actualización de Stock Productos Web</h1>
            </div>
            <div class="col-md-1 float-right">
                 <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_volver_Click">Volver</asp:LinkButton>
            </div>
          </div>
     </div>
     <br />
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-12">
                <div class="jumbotron">
                    En esta pantalla Ud podrá generar actualizaciones masivas de Stock de productos que están publicados en la Web, seleccione Línea de Ventas o genere un sólo proceso, esto generará un movimiento interno
                    en el ERP... Consulte con el numero Interno que le entregará el proceso una vez finalizado el procedimiento.
                </div>
            </div>
         </div>
     </div>
      <br />
     <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-12">
               <h2><span class="badge badge-pill badge-primary">Búsqueda de Información</span></h2>
            </div>
         </div>
         <div class="row">
             <div class="col-md-1">
                 <label class="badge badge-pill badge-success">Línea de Ventas:</label>
             </div>
             <div class="col-md-3">
                 <asp:DropDownList ID="LstLineasVenta" runat="server" AppendDataBoundItems="True">
                     <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                 </asp:DropDownList>
             </div>
              <div class="col-md-3">
                  <asp:Button CssClass="btn btn-md btn-primary active" ID="btn_buscar" runat="server" Text="Buscar" />
             </div>
         </div>
     </div>
     <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
            <div class="col-md-12">

                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1483px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">

                 <asp:Button ID="btn_procesar" CssClass="btn btn-md btn-success active" runat="server" Text="Procesar Cambios" />

            </div>
        </div>
    </div>
    </form>
</body>
</html>
