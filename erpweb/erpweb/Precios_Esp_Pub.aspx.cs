using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;

namespace erpweb
{
    public partial class Precios_Esp : System.Web.UI.Page
    {

        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_008_04", Sserver) == "NO")
                    {
                        Response.Redirect("ErrorAcceso.html");
                    }
                    lbl_conectado.Text = Session["Usuario"].ToString();
                }

                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "Ambiente Desarrollo"; }
                else
                { lbl_ambiente.Text = "Ambiente Producción"; }
                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "Ambiente Desarrollo"; }
                else
                { lbl_ambiente.Text = "Ambiente Producción"; }

                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");


            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }

            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
            if (utiles.retorna_ambiente() == "D")
            { lbl_ambiente.Text = "Ambiente Desarrollo"; }
            else
            { lbl_ambiente.Text = "Ambiente Producción"; }
            if (!this.IsPostBack)
            {
                carga_clientes();
            }
        }

        void carga_clientes()
        {
            string queryString = "";

            queryString = "Select top 10 rr.id_cliente";
            queryString = queryString + ",rr.rut ";
            queryString = queryString + ",rr.dv_rut ";
            queryString = queryString + ",rr.razon_social ";
            queryString = queryString + ",rr.telefono ";
            queryString = queryString + ",rr.telefono2 ";
            queryString = queryString + ",rr.direccion  ";
            queryString = queryString + ",rr.comuna ";
            queryString = queryString + ",rr.ciudad ";
            queryString = queryString + ",rr.id_region ";
            queryString = queryString + ",rr.Email ";
            queryString = queryString + "FROM( ";
            queryString = queryString + "SELECT distinct cl.id_cliente, Rut, Dv_Rut, Razon_Social, Telefono, Telefono2, sc.Direccion, sc.Comuna, sc.Ciudad, sc.Id_Region, cl.email ";
            queryString = queryString + "FROM dbo.tbl_Clientes cl ";
            queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Sucursales_Clientes sc ON cl.ID_Cliente = sc.Id_Cliente  and sc.Sucursal_Principal = 1 ";
            queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Riesgo ON cl.Id_Riesgo = tbl_Riesgo.Id_Riesgo ";
            queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Vendedor_cliente ON cl.ID_Cliente = tbl_Vendedor_cliente.id_Cliente ";
            queryString = queryString + "LEFT OUTER JOIN dbo.vis_clientes_lv vc on vc.id_cliente = cl.ID_Cliente ";
            queryString = queryString + "LEFT OUTER JOIN tbl_Regiones rr on rr.ID_Region = sc.Id_Region ";
            queryString = queryString + "WHERE(cl.Es_Cliente = 1) ";
            queryString = queryString + "and isnull(cl.Activo, 0) = 1 ";
            queryString = queryString + "and cl.ID_Cliente in (select id_cliente from tbl_Descuentos_Unitarios where Activo = 1) ";
            queryString = queryString + "union all ";
            queryString = queryString + "SELECT distinct cl.id_cliente Id, Rut, Dv_Rut, Razon_Social, Telefono, Telefono2, sc.Direccion, sc.Comuna, sc.Ciudad,sc.Id_Region, cl.email ";
            queryString = queryString + "FROM dbo.tbl_Clientes cl ";
            queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Sucursales_Clientes sc ON cl.ID_Cliente = sc.Id_Cliente ";
            queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Riesgo ON cl.Id_Riesgo = tbl_Riesgo.Id_Riesgo ";
            queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Vendedor_cliente ON cl.ID_Cliente = tbl_Vendedor_cliente.id_Cliente ";
            queryString = queryString + "LEFT OUTER JOIN dbo.vis_clientes_lv vc on vc.id_cliente = cl.ID_Cliente ";
            queryString = queryString + "LEFT OUTER JOIN tbl_Regiones rr on rr.ID_Region = sc.Id_Region ";
            queryString = queryString + "WHERE(cl.Es_Cliente = 1) ";
            queryString = queryString + "and isnull(cl.Activo, 0) = 1 ";
            queryString = queryString + "and cl.ID_Cliente in (SELECT Id_Empresa FROM tbl_Descuentos WHERE isnull(Descuento, 0) > 0)) rr ";
            queryString = queryString + "WHERE 1 = 1 ";

            if (txt_idw.Text != "")
            {
                queryString = queryString + "and rr.id_cliente = " + txt_idw.Text;
            }
            if (txt_rutw.Text != "")
            {
                queryString = queryString + "and rr.rut = " + txt_rutw.Text;
            }
            if (txt_razonw.Text != "")
            {
                queryString = queryString + "and rr.Razon_Social like '%" + txt_razonw.Text + "%'";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    connection.Open();
                    //SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter reader = new SqlDataAdapter(queryString, connection);
                    DataSet dr = new DataSet();
                    reader.Fill(dr, "tbl_Clientes");
                    lista.DataSource = dr;
                    lista.DataBind();
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }


        protected void Btn_cargar_Click(object sender, EventArgs e)
        {
            string query = "";
            int v_visible = 0;
            int v_prodpedido = 0;
            int v_venta = 0;
            int v_cotizaciones = 0;
            Page.Validate();
            if (Page.IsValid)
            {
                // Recorremos la grilla con los clientes del ERP que estén seleccionados
                 insert_cliente(Convert.ToInt32(lbl_id.Text), Convert.ToInt32(lbl_rut.Text), lbl_dv.Text, lbl_razon.Text, lbl_fono.Text, lbl_fono2.Text, lbl_direccion.Text, lbl_comuna.Text, lbl_ciudad.Text, Convert.ToInt32(lbl_región.Text), lbl_email.Text);

                foreach (GridViewRow row in List_ProdEsp.Rows)
                {
                    CheckBox check = row.FindControl("Chk_selecciona") as CheckBox;

                    if (check.Checked)
                    {
                        using (MySqlConnection conn = new MySqlConnection(SMysql))
                        {
                            try
                            {
                                conn.Open();
                                query = "inserta_precios_especiales";
                                MySqlCommand command = new MySqlCommand(query, conn);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@v_id", row.Cells[1].Text);
                                command.Parameters["@v_id"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_id_cliente", lbl_id.Text);
                                command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_codigo", row.Cells[2].Text);
                                command.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_descripcion", Context.Server.HtmlDecode(row.Cells[3].Text));
                                command.Parameters["@v_descripcion"].Direction = ParameterDirection.Input;

                                if (row.Cells[4].Text == "S") { v_visible = 1; } else { v_visible = 0; }
                                command.Parameters.AddWithValue("@v_visible", v_visible);
                                command.Parameters["@v_visible"].Direction = ParameterDirection.Input;

                                if (row.Cells[5].Text == "S") { v_prodpedido = 1; } else { v_prodpedido = 0; }
                                command.Parameters.AddWithValue("@v_prodpedido", v_prodpedido);
                                command.Parameters["@v_prodpedido"].Direction = ParameterDirection.Input;

                                if (row.Cells[6].Text == "S") { v_venta = 1; } else { v_venta = 0; }
                                command.Parameters.AddWithValue("@v_venta", v_venta);
                                command.Parameters["@v_venta"].Direction = ParameterDirection.Input;

                                if (row.Cells[7].Text == "S") { v_cotizaciones = 1; } else { v_cotizaciones = 0; }
                                command.Parameters.AddWithValue("@v_cotizaciones", v_cotizaciones);
                                command.Parameters["@v_cotizaciones"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_sigla", Context.Server.HtmlDecode(row.Cells[8].Text));
                                command.Parameters["@v_sigla"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_precio_lista", row.Cells[9].Text.Replace(",", "."));
                                command.Parameters["@v_precio_lista"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_precio", row.Cells[10].Text.Replace(",","."));
                                command.Parameters["@v_precio"].Direction = ParameterDirection.Input;

                                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                                MySqlDataReader dr = command.ExecuteReader();

                                while (dr.Read())
                                {
                                    if (!dr.IsDBNull(0))
                                    {
                                        lbl_status.Text = dr.GetString(0);
                                    }
                                }

                                conn.Close();
                                conn.Dispose();
                                lbl_error.Text = "";

                                lbl_status.Text = "Clientes insertados correctamente";
                            }
                            catch (Exception ex)
                            {
                                lbl_error.Text = "ERROR "+  ex.Message + query;
                                lbl_error.BackColor = Color.Red;
                                conn.Close();
                                conn.Dispose();
                            }
                        }

                    }
                }
            }
        }

        void insert_cliente(int id, int rut, string dv, string razon, string fono, string fono2, string direccion, string comuna, string ciudad, int region, string email)
        {
            string query = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "inserta_cliente";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id", id);
                    command.Parameters["@v_id"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_rut", rut);
                    command.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_dv", dv);
                    command.Parameters["@v_dv"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_razon", razon.Replace(",", ".").Replace("&nbsp;", ""));
                    command.Parameters["@v_razon"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_fono", fono.Replace("&nbsp;", ""));
                    command.Parameters["@v_fono"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_fono2", fono2.Replace("&nbsp;", ""));
                    command.Parameters["@v_fono2"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_direccion", direccion.Replace(",", ".").Replace("&nbsp;", ""));
                    command.Parameters["@v_direccion"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_comuna", comuna.Replace("&nbsp;", ""));
                    command.Parameters["@v_comuna"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_ciudad", ciudad.Replace("&nbsp;", ""));
                    command.Parameters["@v_ciudad"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_region", region);
                    command.Parameters["@v_region"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_email", email.Replace("&nbsp;", ""));
                    command.Parameters["@v_email"].Direction = ParameterDirection.Input;

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            lbl_status.Text = dr.GetString(0);
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                    lbl_error.Text = "";

                    lbl_status.Text = "Clientes insertados correctamente";
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id_cliente = "";
            GridViewRow row = lista.SelectedRow;
            id_cliente  = row.Cells[1].Text;
            carga_productos_asociados(id_cliente);


            lbl_id.Text = row.Cells[1].Text;
            lbl_rut.Text = row.Cells[2].Text;
            lbl_dv.Text = row.Cells[3].Text;
            lbl_razon.Text = Context.Server.HtmlDecode(row.Cells[4].Text);
            lbl_fono.Text = row.Cells[5].Text;
            lbl_fono2.Text = row.Cells[6].Text;
            lbl_direccion.Text = Context.Server.HtmlDecode(row.Cells[7].Text);
            lbl_comuna.Text = Context.Server.HtmlDecode(row.Cells[8].Text);
            lbl_ciudad.Text = Context.Server.HtmlDecode(row.Cells[9].Text);
            lbl_región.Text = row.Cells[10].Text;
            lbl_email.Text = Context.Server.HtmlDecode(row.Cells[10].Text);
        }

        void carga_productos_asociados(string id_cliente)
        {
            string queryString = "";

            queryString = "Select x.Id_Item Id, x.Codigo, x.Descripcion, x.prodpedido, x.visible, x.cotizaciones, x.ventas, ";
            queryString = queryString + " (select sigla from tbl_monedas where id_moneda = x.id_moneda) moneda , x.precio_lista, x.Precio ";
            queryString = queryString + "FROM( ";
            queryString = queryString + "select tbl_Descuentos_Unitarios.Id_Cliente, ";
            queryString = queryString + "tbl_items.Id_Item, ";
            queryString = queryString + "Codigo, ";
            queryString = queryString + "replace(replace(replace(Descripcion, char(9), ''), char(10), ''), char(13), '') Descripcion, ";
            queryString = queryString + "'S' prodpedido, ";
            queryString = queryString + "'N' visible, ";
            queryString = queryString + "'N' cotizaciones, ";
            queryString = queryString + "'S' ventas, ";
            queryString = queryString + "tbl_Descuentos_Unitarios.Id_Moneda, ";
            queryString = queryString + "tbl_items.Precio precio_lista, ";
            queryString = queryString + "tbl_Descuentos_Unitarios.Valor Precio ";
            queryString = queryString + " from tbl_items with(nolock)";
            queryString = queryString + " inner join tbl_Descuentos_Unitarios on tbl_Descuentos_Unitarios.Id_Item = tbl_items.ID_Item and tbl_Descuentos_Unitarios.Activo = 1 ";
            queryString = queryString + " inner join tbl_Clientes on tbl_Clientes.ID_Cliente = tbl_Descuentos_Unitarios.Id_Cliente and tbl_Clientes.Activo = 1 ";
            queryString = queryString + " where tbl_items.activo = 1 ";
            queryString = queryString + " union all ";
            queryString = queryString + "select tbl_Descuentos.Id_Empresa, ";
            queryString = queryString + "Id_Item, ";
            queryString = queryString + "Codigo, ";
            queryString = queryString + "replace(replace(replace(Descripcion, char(9), ''), char(10), ''), char(13), '') Descripcion, ";
            queryString = queryString + "'S' prodpedido, ";
            queryString = queryString + "'N' visible, ";
            queryString = queryString + "'N' cotizaciones, ";
            queryString = queryString + "'S' ventas, ";
            queryString = queryString + "1 id_moneda, ";
            queryString = queryString + "tbl_items.Precio precio_lista, ";
            queryString = queryString + "precio - ((tbl_Descuentos.Descuento * Precio) / 100) precio ";
            queryString = queryString + "from tbl_items with(nolock) ";
            queryString = queryString + "inner join tbl_Descuentos on tbl_Descuentos.Id_Linea_Venta = tbl_items.Id_Linea_Venta and isnull(tbl_Descuentos.Descuento, 0) > 0 ";
            queryString = queryString + "inner join tbl_clientes on tbl_Clientes.ID_Cliente = tbl_Descuentos.Id_Empresa ";
            queryString = queryString + "where tbl_items.activo = 1 ";
            queryString = queryString + "and tbl_items.ID_Item in (select distinct df.Id_Item from tbl_Facturas fc ";
            queryString = queryString + "inner join tbl_Items_Facturas df on df.Id_Factura = fc.ID_Factura ";
            queryString = queryString + "where year(fc.fecha) >= 2017 and fc.Id_tipo_factura = 12 and fc.Nula = 0)) x ";
            queryString = queryString + "where x.id_cliente = " + id_cliente;

            try
            {
                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    connection.Open();
                    //SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter reader = new SqlDataAdapter(queryString, connection);
                    DataSet dr = new DataSet();
                    reader.Fill(dr, "tbl_items");
                    List_ProdEsp.DataSource = dr;
                    List_ProdEsp.DataBind();
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        protected void Btn_buscarw_Click(object sender, EventArgs e)
        {
            lbl_status.Text = "";
            lbl_error.Text = "";
            carga_clientes();
        }

        protected void Btn_nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Precios_Esp_Pub");
        }

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Precios_Esp_Adm.aspx");
        }

    }
}