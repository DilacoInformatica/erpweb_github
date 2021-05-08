<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_NV.aspx.cs" Inherits="erpweb.Detalle_NV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle Nota de Venta</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    </head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-11">
                    <h1><img alt="" src="img/vineta.gif" style="width:31px;height:33px;" /><span class="text-center text-primary">Detalle Nota de Venta WEB N°<asp:Label ID="lbl_numero" runat="server"></asp:Label></span></h1>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="Btn_volver" runat="server" CssClass="btn btn-md btn-primary active btn-block" OnClick="Btn_volver_Click" Text="Volver" />
                </div>
            </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
           <div class="row">
            <div class="col-md-4">
                 <h5<span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge badge-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge badge-primary"></asp:Label></span>
                </h5>
             </div>
             <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_status" runat="server" CssClass="badge badge-warning"></asp:Label></span></h5>
             </div>
             <div class="col-md-4">
                <h5><span><asp:Label ID="lbl_error" runat="server" CssClass="badge badge-danger text-dark"></asp:Label></span></h5>
            </div>
      </div>
     </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <h3><span class="badge badge-primary">Cabecera Nota de Venta</span></h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">N° NV ERP</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_numero_erp" CssClass="form-control" runat="server" Width="227px"></asp:Label></h5>
                        </div>
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Fecha</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_fecha" runat="server" CssClass="form-control" Width="229px"></asp:Label></h5>
                        </div>
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">N° OC</span></h5>
                        </div>
                        <div class="col-md-2">
                           <h5><asp:Label ID="lbl_n_oc" runat="server" CssClass="form-control" Width="100px"></asp:Label></h5>
                        </div>
                         <div class="col-md-1">
                            <h5><span class="badge badge-info">N° WebPay</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_transac_pago" runat="server" CssClass="form-control" Width="232px"></asp:Label></h5>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="LnkUpdInfoPagoNV" CssClass="form-control" runat="server" OnClick="LnkUpdInfoPagoNV_Click" Visible="False" Width="221px">Actualizar Información</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Documento</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_tipo_facturacion" runat="server"  CssClass="form-control" Width="235px"></asp:Label></h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h3><span class="badge badge-primary">Información Cliente</span></h3>
                        </div>
                    </div>
                    <div class="row">
                         <div class="col-md-1">
                            <h5><span class="badge badge-info">Existe en ERP</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_existe" runat="server" CssClass="form-control" Width="43px"></asp:Label></h5>
                        </div>
                        <div class="col-md-1">
                            <h5></h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Rut</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_rut" runat="server" CssClass="form-control" Width="225px"></asp:Label></h5>
                            <asp:Label ID="lbl_rut_exit" runat="server" CssClass="form-control" Width="181px" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Cliente</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_cliente" runat="server" CssClass="form-control" Width="299px"></asp:Label></h5>
                        </div>
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Teléfono</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_fono" runat="server" CssClass="form-control" Width="225px"></asp:Label></h5>
                        </div>
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Email</span></h5>
                        </div>
                       <div class="col-md-2">
                            <h5><asp:Label ID="lbl_email" runat="server" CssClass="form-control" Width="225px"></asp:Label></h5>
                        </div>
                       
                    </div>
                       
                     <div class="row">
                    </div>
                     <div class="row">
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Dirección</span></h5>
                        </div>
                        <div class="col-md-8">
                            <h5><asp:Label ID="lbl_direccion" runat="server" CssClass="form-control" Width="729px"></asp:Label></h5>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Comuna</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_comuna" runat="server" CssClass="form-control" Width="229px"></asp:Label></h5>
                        </div>
                        <div class="col-md-1">
                            <h5><span class="badge badge-info">Ciudad</span></h5>
                        </div>
                        <div class="col-md-2">
                            <h5><asp:Label ID="lbl_ciudad" runat="server" CssClass="form-control" Width="229px"></asp:Label></h5>
                        </div>
                         <div class="col-md-1">
                            <h5><span class="badge badge-info">Región</span></h5>
                        </div>
                        <div class="col-md-4">
                            <h5><asp:Label ID="lbl_region" runat="server" CssClass="form-control" Width="280px"></asp:Label></h5>
                        </div>
                    </div>

                <div class="row">
                    <div class="col-md-12">
                        <h3><span class="badge badge-primary">Información Despacho</span></h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Contacto</span></h5>
                    </div>
                    <div class="col-md-2">
                        <h5><asp:Label ID="lbl_contacto" runat="server" CssClass="form-control" Width="346px"></asp:Label></h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Teléfono</span></h5>
                    </div>
                    <div class="col-md-2">
                        <h5><asp:Label ID="lbl_fono_despacho" runat="server" CssClass="form-control"></asp:Label></h5>
                    </div>
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Email</span></h5>
                    </div>
                    <div class="col-md-2">
                        <h5><asp:Label ID="lbl_email_contacto" runat="server" CssClass="auto-style1" Width="725px"></asp:Label></h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Dirección</span></h5>
                    </div>
                    <div class="col-md-8">
                        <h5><asp:Label ID="lbl_direccion_despacho" runat="server" CssClass="form-control" Width="729px"></asp:Label></h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Comuna</span></h5>
                    </div>
                    <div class="col-md-2">
                        <h5><asp:Label ID="lbl_comuna_despacho" runat="server" CssClass="auto-style1" Width="722px"></asp:Label></h5>
                    </div>
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Ciudad</span></h5>
                    </div>
                    <div class="col-md-2">
                        <h5><asp:Label ID="lbl_ciudad_despacho" runat="server" CssClass="form-control"></asp:Label></h5>
                    </div>
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Región</span></h5>
                    </div>
                    <div class="col-md-4">
                        <h5><asp:Label ID="lbl_region_despacho" runat="server" CssClass="form-control" Width="280px"></asp:Label></h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <h5><span class="badge badge-info">Observaciones</span></h5>
                    </div>
                    <div class="col-md-8">
                        <h5><asp:Label ID="lbl_obs_despacho" runat="server" CssClass="form-control"></asp:Label></h5>
                    </div>
                </div>
            </div>
      </div>
     </div>
     <br />
    <div class="container-fluid rounded border border-secondary bg-light">
        <div class="row">
             <div class="col-md-12">
                <h6><span class="badge badge-primary">Generar NV de Venta</span></h6>
            </div>
        </div>
        <div class="row">
             <div class="col-md-10">
                 <div class="form-check form-check-inline">
                     <asp:RadioButton ID="RadBtnNV" CssClass="form-check-input" runat="server" Text="" GroupName="N1" Checked="True" />
                     <h5><label class="form-check-label" for="RadBtnNV"><span class="badge badge-info">Sólo Nota de Venta</span></label></h5>
                 </div>
                 <div class="form-check form-check-inline">
                     <asp:RadioButton ID="RadNVDesp" CssClass="form-check-input" runat="server" Text="" GroupName="N1" />
                     <h5><label class="form-check-label" for="RadNVDesp"><span class="badge badge-info">Nota de Venta y Despacho</span></label></h5>
                 </div>         
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-2">
                            <h5><span class="badge badge-info">Vendedor/Emisor</span></h5>
                        </div>
                        <div class="col-md-3">
                                <h5>
                                    <asp:DropDownList ID="Lista_Vendedores" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                    </asp:DropDownList>
                                </h5>
                        </div>
                        <div class="col-md-2">
                            <h6><asp:Button ID="Btn_Rechazar" runat="server" CssClass="btn btn-md btn-danger active btn-block" Enabled="False" OnClick="Btn_Rechazar_Click" Text="Rechazar NV" Width="200px" /></h6>
                        </div>
                        <div class="col-md-2">
                            <h6><asp:Button ID="Btn_crearNV" runat="server" CssClass="btn btn-md btn-primary active btn-block" OnClick="Btn_crearNV_Click" Text="Crear NV" Width="200px"/>
                                
                            </h6>

                        </div>
                    </div>
                </div>
            </div>
    </div>
        <br />
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
        <asp:GridView ID="lista_detalles" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" Width="1503px" ShowFooter="True" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Item" HeaderText="Item" />
                <asp:BoundField DataField="codigo" HeaderText="Código" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Valor_Unitario" HeaderText="Precio Unitario" />
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
     <br />
    <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge badge-info">Moneda</span></h6>
                        </div>
                        <div class="col-md-1 float-right">
                            <h6><span class="text-md-right"> <asp:Label ID="lbl_moneda" CssClass="form-control" runat="server" Width="150px"></asp:Label></span></h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge badge-info">Neto</span></h6>
                        </div>
                        <div class="col-md-1 float-right">
                            <h6><asp:Label ID="lbl_neto" CssClass="form-control" runat="server" Width="150px"></asp:Label></h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge badge-info">Iva</span></h6>
                        </div>
                        <div class="col-md-1 float-right">
                            <h6><asp:Label ID="lbl_tax" runat="server" CssClass="form-control" Width="150px"></asp:Label></h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right">
                            <h6><span class="badge badge-info">Total</span></h6>
                        </div>
                        <div class="col-md-1 float-right">
                            <h6><asp:Label ID="lbl_total" runat="server" CssClass="form-control" Width="150px"></asp:Label></h6>            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9"></div>
                        <div class="col-md-1 float-right"></div>
                    </div>
                </div>
        <asp:Label ID="lbl_id_nv" runat="server" Visible="False"></asp:Label>
       <asp:Label ID="lbl_transac" runat="server" Visible="False"></asp:Label>
     </div>
            <%-- fin plantilla boostrap --%>
      
    </form>
</body>
</html>