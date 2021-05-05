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
    public partial class Stock_Masivo : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        ClsFTP ftp = new ClsFTP();
        Cls_Utilitarios utiles = new Cls_Utilitarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            if (Session["Usuario"].ToString() == "" || Session["Usuario"] == null)
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

            if (!this.IsPostBack)
            {
                carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", LstLineasVenta, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                carga_contrl_lista("select ID_Bodega, Nombre_Bodega from tbl_Bodegas where Activo = 1 and id_bodega = 10 ", ListBodEntrada, "tbl_Bodegas", "ID_Bodega", "Nombre_Bodega");
                carga_contrl_lista("select ID_Bodega, Nombre_Bodega from tbl_Bodegas where Activo = 1 and id_bodega <> 10 ", ListBodSalida, "tbl_Bodegas", "ID_Bodega", "Nombre_Bodega");
                //btn_actualizar.Attributes["Onclick"] = "return confirm('Está a punto de actualizar el Stock para estos productos, Desea Continuar?')";
                // muestra_productos();
                btn_procesar.Attributes["Onclick"] = "return confirm('Ajustar Stock a Productos seleccionados?')";
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

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Stock.aspx");
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_lista_stock_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    if (LstLineasVenta.SelectedValue == "0")
                    {
                        cmd.Parameters.AddWithValue("@id_linea_vta", DBNull.Value);
                        cmd.Parameters["@id_linea_vta"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@id_linea_vta", LstLineasVenta.SelectedValue);
                        cmd.Parameters["@id_linea_vta"].Direction = ParameterDirection.Input;
                    }
                    cmd.Parameters.AddWithValue("@v_id_bodega_in", ListBodEntrada.SelectedValue);
                    cmd.Parameters["@v_id_bodega_in"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_bodega_out", ListBodSalida.SelectedValue);
                    cmd.Parameters["@v_id_bodega_in"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                     da.Fill(ds);

                    Grilla_items.DataSource = ds;
                    Grilla_items.DataBind();

                    connection.Close();
                    connection.Dispose();

                    string mensaje = lbl_status.Text;

                    btn_procesar.Visible = true;

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

        protected void Grilla_items_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;


                TextBox txt_porcentaje = e.Row.FindControl("txt_porcentaje") as TextBox;
                TextBox txt_cantidadd_a_mover = e.Row.FindControl("txt_cantidadd_a_mover") as TextBox;

                txt_porcentaje.Text = drv["por_max_a_mover"].ToString();
                txt_cantidadd_a_mover.Text = drv["unidades_a_mover"].ToString();

                CheckBox Chk_Validar = e.Row.FindControl("Chk_Validar") as CheckBox;

                if (txt_cantidadd_a_mover.Text == "0")
                {
                    Chk_Validar.Checked = false;
                    Chk_Validar.Enabled = false;
                }



                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void Grilla_items_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = Grilla_items.SelectedRow;
            TextBox txt_porcentaje = row.FindControl("txt_porcentaje") as TextBox;

            if (char.IsNumber(Convert.ToChar(txt_porcentaje.Text)))
            {
                txt_porcentaje.Text = "";
            }
           
        }

        protected void btn_procesar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                int v_id_mst = 0;
                int v_swc = 0;
                int v_id_item = 0;

                double v_por_a_mover = 0;
                double v_cantidad = 0;

                if (ListBodEntrada.SelectedValue ==  "0")
                {
                    lbl_status.Text = "Debe seleccionar Bodega entrada";
                    v_swc = 1;
                }

                if (ListBodSalida.SelectedValue == "0")
                {
                    lbl_status.Text = "Debe seleccionar Bodega salida";
                    v_swc = 1;
                }

                if (v_swc == 0)
                {
                    v_id_mst = crea_mov_interbodegas();
                    // creamos el movimiento de cabecera

                    if (v_id_mst > 0)
                    {
                        // Si se crea la cabecera... ingresamos el detalle
                        foreach (GridViewRow row in Grilla_items.Rows)
                        {
                            CheckBox Chk_Validar = row.FindControl("Chk_Validar") as CheckBox;
                            TextBox txt_cantidadd_a_mover = row.FindControl("txt_cantidadd_a_mover") as TextBox;
                            TextBox txt_porcentaje = row.FindControl("txt_porcentaje") as TextBox;

                            if (Chk_Validar.Checked)
                            {
                                v_id_item = 0;
                                v_por_a_mover = Convert.ToDouble(txt_cantidadd_a_mover.Text);
                                v_cantidad = Math.Round((Convert.ToDouble(row.Cells[6].Text) * Convert.ToDouble(txt_porcentaje.Text)) / 100);
                                txt_cantidadd_a_mover.Text = Convert.ToString(v_cantidad);
                                crea_det_mov_interbodegas(row.Cells[0].Text, v_cantidad, v_id_mst);
                                v_id_item = retorna_id_producto(row.Cells[0].Text.Trim());
                                actualiza_stock_web(v_id_item, v_cantidad);
                            }
                        }
                        lbl_status.Text = "Proceso terminado correctamente, puede revisarlo en el ERP con el numero Interno N° " + v_id_mst;
                    }
                    else
                    {
                        lbl_error.Text = "No es posible generar Movimiento Interbodegas, no se crea movimiento ";
                    }

                    // recorremos la grilla
                   
                }
            }
            btn_procesar.Visible = false;
        }

        void actualiza_stock_web(int id_item, double stock)
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

        public string crea_det_mov_interbodegas(string codigo, double cantidad, int id_mst)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_admin_inv_det_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@v_codigo", codigo.Trim());
                    cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_cantidad", cantidad);
                    cmd.Parameters["@v_cantidad"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_mst", id_mst);
                    cmd.Parameters["@v_id_mst"].Direction = ParameterDirection.Input;

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
                                result = Convert.ToString(rdr.GetString(0));
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return result;

                }
                catch (Exception ex)
                {
                    lbl_status.Text = ex.Message;
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public int crea_mov_interbodegas()
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_admin_inv_cab_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

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
                                result = Convert.ToInt32(rdr.GetInt32(0));
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return Convert.ToInt32(result);

                }
                catch (Exception ex)
                {
                    lbl_status.Text = ex.Message;
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int retorna_id_producto(string codigo)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_retorna_id_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@v_codigo", codigo.Trim());
                    cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                result = Convert.ToString(rdr.GetInt32(0));
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return Convert.ToInt32(result);

                }
                catch (Exception ex)
                {
                    lbl_status.Text = ex.Message;
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}