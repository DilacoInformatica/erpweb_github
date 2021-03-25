using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
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
            if (Session["Usuario"].ToString() == "")
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
                carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                btn_actualizar.Attributes["Onclick"] = "return confirm('Está a punto de actualizar el Stock para estos productos, Desea Continuar?')";
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
            Chk_desactiva_cods.Checked = false;
            lbl_error.Text = "";
            lbl_status.Text = "";
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
        }

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            string query = "";
            string stock = "";
            int id_item = 0;
            Page.Validate();
            if (Page.IsValid)
            {
                try
                {
                    for (int i = 0; i < Grilla.Rows.Count; i++)
                    {
                        GridViewRow row = Grilla.Rows[i];
                        id_item = Convert.ToInt32(row.Cells[0].Text);
                        stock = consulta_stock_erp(id_item);
                        actualiza_stock_web(id_item, Convert.ToDouble(stock));

                        if (Chk_desactiva_cods.Checked && row.Cells[3].Text == "NO")
                        {
                            desmarca_prod_vta(id_item);
                        }
                        // the rest o your code
                    }
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }


            }
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

                    lbl_status.Text = "Producto(s) actualizado(s) y desmarcado()";

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
                Label stock_critico = e.Row.FindControl("lbl_stock_critico") as Label;

                
                if (Convert.ToDouble(stock.Text) > Convert.ToDouble(stock_critico.Text))
                {
                    stock_critico.CssClass = "badge badge-primary";
                }


                if (Convert.ToDouble(stock.Text)  == Convert.ToDouble(stock_critico.Text))
                {
                    stock_critico.CssClass = "badge badge-warning";
                }

                if (Convert.ToDouble(stock.Text) == Convert.ToDouble(stock_critico.Text) && Convert.ToDouble(stock.Text) <= 0)
                {
                    stock_critico.CssClass = "badge badge-danger";
                }

                if (Convert.ToDouble(stock.Text) < Convert.ToDouble(stock_critico.Text))
                {
                    stock_critico.CssClass = "badge badge-danger";
                }

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }
    }
}
