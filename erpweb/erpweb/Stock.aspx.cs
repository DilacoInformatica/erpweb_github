using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace erpweb
{
    public partial class Stock : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        ClsFTP ftp = new ClsFTP();
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
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_009_10", Sserver) == "NO")
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


          if (!this.IsPostBack)
                {
                    carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                    carga_contrl_lista("select ID_Bodega, Nombre_Bodega from tbl_Bodegas where Activo = 1", ListBodSalida, "tbl_Bodegas", "ID_Bodega", "Nombre_Bodega");
                    // btn_actualizar.Attributes["Onclick"] = "return confirm('Está a punto de actualizar el Stock para estos productos, Desea Continuar?')";
                    muestra_productos();
                }
         }

        void carga_contrl_lista(string sql, DropDownList lista, string tabla, string llave, string Campo)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                connection.Open();
                //SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                DataSet dr = new DataSet();
                reader.Fill(dr, tabla);
                lista.DataSource = dr;
                lista.DataValueField = llave;
                lista.DataTextField = Campo;
                lista.DataBind();

                connection.Close();
                connection.Dispose();
            }
        }

        void muestra_productos()
        {
            String queryString = "Lista_Prod_Stock";
            //  Chk_desactiva_cods.Checked = false;
            Chk_desactiva_cods.Visible = false;
            lbl_error.Text = "";
            //lbl_status.Text = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    if (txt_codigo.Text == "")
                    {
                        command.Parameters.AddWithValue("@v_codigo", DBNull.Value);
                        command.Parameters["@v_codigo"].Direction = ParameterDirection.Input;
                    }

                    else
                    {
                        command.Parameters.AddWithValue("@v_codigo", txt_codigo.Text);
                        command.Parameters["@v_codigo"].Direction = ParameterDirection.Input;
                    }

                    if (LstLineaVtas.SelectedItem.Value.ToString() == "0")
                    {
                        command.Parameters.AddWithValue("@v_id_linea_vta", DBNull.Value);
                        command.Parameters["@v_id_linea_vta"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_id_linea_vta", LstLineaVtas.SelectedItem.Value.ToString());
                        command.Parameters["@v_id_linea_vta"].Direction = ParameterDirection.Input;
                    }


                    //var result = command.ExecuteNonQuery();

                    DataSet ds = new DataSet();
                    DataTable table = new DataTable();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);

                    ///MySqlDataReader dr = command.ExecuteReader();
                    mysqlDAdp.Fill(table);

                    if (table.Rows.Count == 0)
                    {
                        lbl_mensaje.Text = "Sin Resultados";
                    }
                    else
                    {
                        Grilla.DataSource = table;
                        Grilla.DataBind();
                    }

                    lbl_mensaje.Text = "Cantidad de Registros" + Grilla.Rows.Count.ToString();
                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    lbl_error.Text = ex.Message ;
                }
            }
        }

        protected void Grilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Grilla.SelectedRow;
            lbl_id.Text = row.Cells[1].Text;
            lbl_codigo.Text = row.Cells[2].Text;
            lbl_fecha.Text = DateTime.Now.ToString();
            lbl_stock_erp.Text = consulta_stock_erp(Convert.ToInt32(lbl_id.Text));
        }

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
             Response.Redirect("Stock_Masivo.aspx");
        }

        public string consulta_stock_erp(int id_item)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_stock_item", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@id_item", id_item);
                    cmd.Parameters["@id_item"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                result = Convert.ToString(rdr.GetDouble(0));
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    return result;
                }
                finally
                {
                    connection.Close();
                }

            }
        }


        void desmarca_prod_vta(int id_item)
        {
            string result = "";
            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("Desmarca_prod", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_item", id_item);
                    cmd.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                result = rdr.GetString(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    //lbl_status.Text = "Producto(s) actualizado(s) y desmarcado()";

                    muestra_productos();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        void actualiza_stock_web(int id_item, double stock, int id_bodega)
        {
            string result = "";
            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("Act_Stock", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_item", id_item);
                    cmd.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_stock", stock);
                    cmd.Parameters["@v_stock"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_bodega", stock);
                    cmd.Parameters["@v_id_bodega"].Direction = ParameterDirection.Input;


                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                result = rdr.GetString(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    lbl_status.Text = "Producto(s) actualizado(s)";

                    muestra_productos();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            Grilla.DataSource = "";
            muestra_productos();
        }

        protected void Grilla_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label stock = e.Row.FindControl("lbl_stock") as Label;
                Label lbl_stock_erp = e.Row.FindControl("lbl_stock_erp") as Label;

                if (Convert.ToDouble(stock.Text) <= 0 )
                {
                    stock.CssClass = "badge badge-danger";
                }

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void ListBodEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void btn_genera_mov_stock_Click(object sender, EventArgs e)
        {
            string result = "";

            if (lbl_codigo.Text == "")
            {
                lbl_error.Text = "Debe indicar Código de Producto";
            }
            else
            {

                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("web_administra_inventario_web", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter param = new SqlParameter();
                        // Parámetros
                        cmd.Parameters.AddWithValue("@v_codigo", lbl_codigo.Text);
                        cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@v_cantidad", Convert.ToDouble(txt_cantidad.Text));
                        cmd.Parameters["@v_cantidad"].Direction = ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@v_id_bodega_in", ListBodEntrada.SelectedValue);
                        cmd.Parameters["@v_id_bodega_in"].Direction = ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@v_id_bodega_out", ListBodSalida.SelectedValue);
                        cmd.Parameters["@v_id_bodega_out"].Direction = ParameterDirection.Input;

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (!rdr.IsDBNull(0))
                                {
                                    //rescatamos los valores segun lo que utilizaremos
                                    lbl_status.Text = Convert.ToString(rdr.GetString(0));
                                }
                            }
                        }

                        connection.Close();
                        connection.Dispose();

                        string mensaje = lbl_status.Text;

                        // actualizamos el Stock en el Sitio Web
                        if (mensaje.IndexOf("N° Interno", 0) > 0)
                        {
                            actualiza_stock_web(Convert.ToInt32(lbl_id.Text), Convert.ToDouble(txt_cantidad.Text), Convert.ToInt32(ListBodSalida.SelectedValue));
                        }

                    }
                    catch (Exception ex)
                    {
                        lbl_status.Text = ex.Message;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        protected void ListBodSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valor = ListBodSalida.SelectedIndex;
            carga_contrl_lista("select ID_Bodega, Nombre_Bodega from tbl_Bodegas where Activo = 1 and id_bodega not in (" + valor + ")", ListBodEntrada, "tbl_Bodegas", "ID_Bodega", "Nombre_Bodega");
        }
    }
}
