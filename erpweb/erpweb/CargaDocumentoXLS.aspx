<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaDocumentoXLS.aspx.cs" Inherits="erpweb.CargaDocumentoXLS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="icon" href="img/favicon.ico" type="image/png" />
    <meta name="theme-color" content="#7952b3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="img/favicon.ico" type="image/png" />
    <title>Carga Documentos Pago Proveedores</title>
    <script src="scripts/bootstrap.min.js"></script>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/dashboard.css" rel="stylesheet" />
    </head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid rounded border border-secondary bg-ligh">
            <div class="row">
                <div class="col-md-1">
                    <h6> Subir archivo Proveedores (CSV)</h6>
                </div>
                <div class="col-md-1">
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="30px" ImageUrl="~/img/cerrar.png" Width="43px" />
                </div>
            </div>
        </div>
        <p class="divider"></p>
    <div class="container-fluid rounded border border-secondary bg-ligh">
       
        <div class="col-md-6">
            <h3>Seleccione Archivo<asp:Label ID="lbl_cadena" runat="server"></asp:Label></h3>
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
            <asp:label runat="server" ID="lbl_status" ForeColor="Red"></asp:label>
            <asp:label runat="server" ID="lbl_file" Visible="False"></asp:label>
            <br />
            <span><strong>Extensiones permitidas (*.csv). Separado por punto y coma (;)</strong></span>
            <asp:Label ID="lbl_nombre_file" runat="server"></asp:Label>
            <br />
        </div>
        <br />
        <p class="divider">
            <asp:Label ID="lbl_registros" runat="server"></asp:Label>
        </p>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="Btn_Subir" CssClass="btn btn-primary form-control " runat="server" Text="Subir Archivo" OnClick="Btn_Subir_Click" />

                </div>
                <div class="col-md-3">
                    <asp:Button ID="Btn_Procesar" CssClass="btn btn-success form-control" runat="server" Text="Procesar" OnClick="Btn_Procesar_Click" />
                    <asp:Label ID="lbl_id_file" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </div>
            
        </div>
        <p class="divider"></p>
    </div>
        <p class="divider"></p>
        <div class="container-fluid rounded border border-secondary bg-ligh">
            <div class="col-md-9">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>
            </div>
        </div>
    </form>
    
</body>
</html>

