<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm_Clientes_Det.aspx.cs" Inherits="erpweb.Adm_Clientes_Det" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle nuevo cliente</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-10">
                            <h1 class="text-center text-primary"><img alt="" src="img/vineta.gif" style="width:31px;height:33px;" />Detalle Ingreso nuevo Cliente</h1> 
                        </div>
                        <div class="col-md-1 float-right">
                            <asp:LinkButton ID="Btn_volver" runat="server" CssClass="btn btn-outline-success" Width="133px" OnClick="Btn_volver_Click">Volver</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
    </div>
     <br />
    <div class="container-fluid rounded border border-secondary bg-light">
         <div class="row">
            <div class="col-md-3">
                 <h5><span><asp:Label ID="lbl_ambiente" runat="server" CssClass="badge bg-primary"></asp:Label></span>, Usuario:
                    <span><asp:Label ID="lbl_conectado" runat="server" CssClass="badge bg-primary"></asp:Label></span></h5>
            </div>
             <div class="col-md-3">
                <h5><span><asp:Label ID="lbl_status" runat="server" CssClass="badge bg-warning"></asp:Label></span></h5>
            </div>
             <div class="col-md-3">
                <h5><span><asp:Label ID="lbl_error" runat="server" CssClass="badge bg-danger"></asp:Label></span></h5>
            </div>
          </div>
     </div>
    <br />
        <div class="container-fluid rounded border border-secondary bg-light">
            <div class="row">
                <div class="col-md-3">
                    <label class="text-success">Id</label>
                    <asp:Label ID="lbl_id" runat="server" CssClass="bg-primary text-white form-control"></asp:Label>
                </div>
                <div class="col-md-3">
                    <label class="text-success">Rut</label>
                    <asp:Label ID="lbl_rut" runat="server" CssClass="bg-primary text-white form-control"></asp:Label>
                    <asp:Label ID="lbl_dv" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_rut_master" runat="server" Visible="false"> </asp:Label>
                </div>
            </div>
             <div class="row">
                <div class="col-md-6">
                    <label class="text-success">Nombre o Razón Social</label>
                    <asp:Label ID="lbl_nombre" runat="server" CssClass="bg-primary text-white form-control form-control"></asp:Label>
                </div>
            </div>
             <div class="row">
                <div class="col-md-6">
                    <label class="text-success">Giro</label>
                    <asp:TextBox ID="txt_giro" runat="server" CssClass="bg-primary text-white form-control form-control" ></asp:TextBox>
                </div>
                  <div class="col-md-6">
                    <label class="text-success">Pais</label>
                    <asp:TextBox ID="txt_pais" runat="server" CssClass="bg-primary text-white form-control form-control" Width="407px"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label class="text-success">Dirección</label>
                    <asp:TextBox ID="txt_direccion" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label class="text-success">Región</label>
                    <asp:TextBox ID="txt_region" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="text-success">Comuna</label>
                    <asp:TextBox ID="txt_comuna" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                </div>
                 <div class="col-md-3">
                     <label class="text-success">Ciudad</label>
                     <asp:TextBox ID="txt_ciudad" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                </div>
            </div>
             <div class="row">
                <div class="col-md-3">
                    <label class="text-success">Teléfono (1)</label>
                    <asp:TextBox ID="txt_fono1" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                </div>
                 <div class="col-md-3">
                    <label class="text-success">Teléfono (2)</label>
                     <asp:TextBox ID="txt_fono2" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                </div>
                  <div class="col-md-3">
                    <label class="text-success">Email</label>
                     <asp:TextBox ID="txt_email" AutoCompleteType="Email" runat="server" CssClass="bg-primary text-white form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label class="text-success">Transportista</label>
                    <asp:DropDownList ID="Lst_Trasnportistas" runat="server"  CssClass="bg-primary text-white form-control"></asp:DropDownList>
                </div> 
            </div>
            <p class="divider"></p>
             <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="Btn_Aprobar" runat="server" CssClass="form-control btn btn-success" Text="Aprobar" OnClick="Btn_Aprobar_Click" />
                </div> 
                 <div class="col-md-3">
                     <asp:Button ID="Btn_Rechazar" runat="server" CssClass="form-control btn btn-danger" Text="Rechazar" OnClick="Btn_Rechazar_Click" />
                </div> 
            </div>
        </div>
    </form>
</body>
</html>
