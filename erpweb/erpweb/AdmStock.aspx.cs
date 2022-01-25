using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace erpweb
{
    public partial class AdmStock : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        int id_bodega_salida = 0;
        string bodega_salida = "";
        int id_bodega_entrada = 0;
        string bodega_entrada = "";
        ClsFTP ftp = new ClsFTP();
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            try
            {
                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");
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
                { lbl_ambiente.Text = "D"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Desarrollo"; }
                else
                { lbl_ambiente.Text = "P"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Producción"; }

            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }

            id_bodega_salida = Convert.ToInt32(utiles.obtengo_valor_regla("BODOU", Sserver));
            id_bodega_entrada = Convert.ToInt32(utiles.obtengo_valor_regla("BODIN", Sserver));
            bodega_entrada = busca_informacion("select Nombre_Bodega from tbl_bodegas where Activo = 1 and ID_Bodega = " + id_bodega_entrada, Sserver, "String");
            bodega_salida = busca_informacion("select Nombre_Bodega from tbl_bodegas where Activo = 1 and ID_Bodega = " + id_bodega_salida, Sserver, "String");

            //lbl_regla.Text = "Regla indica que los productos saldrán desde " + bodega_salida + " hasta " + bodega_entrada;


            btn_genera_mov_stock.Attributes["Onclick"] = "return confirm('Generar Actualización de Stock?')";

            if (!this.IsPostBack)
            {
                carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                // btn_actualizar.Attributes["Onclick"] = "return confirm('Está a punto de actualizar el Stock para estos productos, Desea Continuar?')";
               
               
                muestra_productos();
            }
        }

        public string busca_informacion (string sentencia, string conector, string salida)
        {
            string valor = "";
            double retorno = 0;
            string resultado = "";

            using (SqlConnection connection = new SqlConnection(conector))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sentencia, connection);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                if (salida == "Number")
                                {
                                    retorno = rdr.GetInt32(0);
                                }
                                else
                                {
                                    valor = rdr.GetString(0).Trim();
                                }
                                //rescatamos los valores segun lo que utilizaremos

                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    if (salida == "Number")
                    {
                        resultado = Convert.ToString(retorno);
                    }
                    else
                    {
                        resultado = valor;
                    }

                    return resultado;

                }
                catch (Exception ex)
                {
                    return "Error " + ex.Message + " " + sentencia;
                }
                finally
                {
                    connection.Close();

                }
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
            lbl_error.Text = "";
            //lbl_status.Text = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    if (txt_codigo.Text == "") {command.Parameters.AddWithValue("@v_codigo", DBNull.Value);}
                    else
                    {command.Parameters.AddWithValue("@v_codigo", txt_codigo.Text);}
                    command.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                    if (LstLineaVtas.SelectedItem.Value.ToString() == "0") { command.Parameters.AddWithValue("@v_id_linea_vta", DBNull.Value); }
                    else
                    { command.Parameters.AddWithValue("@v_id_linea_vta", LstLineaVtas.SelectedItem.Value.ToString()); }
                    command.Parameters["@v_id_linea_vta"].Direction = ParameterDirection.Input;

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

                    lbl_mensaje.Text = "Cantidad de Registros: " + Grilla.Rows.Count.ToString();
                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    lbl_error.Text = ex.Message;
                }
            }
        }

        protected void Grilla_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // Stock en cero

                if (Convert.ToDouble(e.Row.Cells[6].Text.Replace(".",",")) <= 0)
                {
                  //  e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.ForeColor = Color.Red;
                }
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
               // e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
               // e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
               //  e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            muestra_productos();
        }

        protected void Btn_procesar_Click(object sender, EventArgs e)
        {

            double stock;
            string nivel = "";
            int por_stock = 0;
            int por_stock_critico = 0;

            lbl_error.Text = "";
            if (Grilla.Rows.Count > 0 )
            {
                foreach (GridViewRow row in Grilla.Rows)
                {
                    if (row.Cells[0].Text == "")
                    {
                        CheckBox Chk_marcar =  row.FindControl("Chk_marcar") as CheckBox;

                        if (Chk_marcar.Checked)
                        {
                            Label lbl_stock_bod_out_grid = row.FindControl("lbl_stock_bod_out_grid") as Label;
                            Label lbl_nivel_aplicar_grid = row.FindControl("lbl_nivel_aplicar_grid") as Label;

                            Label lbl_Por_Stock = row.FindControl("lbl_Por_Stock") as Label;
                            Label lbl_Por_Stock_Critico = row.FindControl("lbl_Por_Stock_Critico") as Label;
                            Label lbl_stock_critico = row.FindControl("lbl_stock_critico") as Label;
                            Label lbl_stock_a_mover_grid = row.FindControl("lbl_stock_a_mover_grid") as Label;


                            lbl_stock_bod_out_grid.Text = "";
                            lbl_nivel_aplicar_grid.Text = "";
                            lbl_Por_Stock.Text = "";
                            lbl_Por_Stock_Critico.Text = "";
                            lbl_stock_critico.Text = "";

                            stock = Convert.ToInt32(busca_informacion("select [dbo].[mostrar_nivel_stock_web] (" + row.Cells[1].Text + "," + id_bodega_salida + ")", Sserver, "Number"));

                            nivel = busca_informacion("select [dbo].[revisa_nivel_stock_web] (" + row.Cells[1].Text + "," + id_bodega_salida + ")", Sserver, "String");
                            lbl_nivel_aplicar_grid.Text = nivel;

                            lbl_stock_bod_out_grid.Text = Convert.ToString(stock);

                            por_stock = Convert.ToInt32(busca_informacion("select [dbo].[indica_porcentaje_stock_web] (" + row.Cells[1].Text + "," + id_bodega_salida + ",'S')", Sserver, "Number"));
                            por_stock_critico = Convert.ToInt32(busca_informacion("select [dbo].[indica_porcentaje_stock_web] (" + row.Cells[1].Text + "," + id_bodega_salida + ",'C')", Sserver, "Number"));

                            lbl_Por_Stock.Text = por_stock.ToString();
                            lbl_Por_Stock_Critico.Text = por_stock_critico.ToString();

                            lbl_stock_a_mover_grid.Text = Convert.ToString(Math.Round(stock * por_stock / 100, 0));
                            lbl_stock_critico.Text = Convert.ToString(Convert.ToInt32(lbl_stock_a_mover_grid.Text) * por_stock_critico / 100);

                        }
                       
                    }
                }
            }
            else
            {
                lbl_error.Text = "Grilla vacía, no se pueden aplicar reglas de Stock";
            }

        /*    If DataGridView1.Rows.Count > 0 Then
    For i As Integer = 0 To DataGridView1.Rows.Count
        If Not DataGridView1.Rows.Item(i) Is Nothing Then
            '//Puedes hacer una validación con el nombre de la columna
            If DataGridView1.Rows.Item(i).Cells("NombreDeUsuario").Value = "Gaby_26" Then
                'Código
            End If

            '//O puedes hacer una validación con el número de la columna
            If DataGridView1.Rows.Item(i).Cells(2).Value = "Duarte Mendieta" Then
                'Código
            End If

            '//O puedes almacenar el valor en una base de datos o mostrarlo en un textbox
            TextBox1.Text = DataGridView1.Rows.Item(i).Cells("NombreDeUsuario").Value
        End If
    Next
End If*/
        }

        protected void Chk_marcar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox Chk_marcar = (CheckBox)sender;
            GridViewRow row = (GridViewRow)Chk_marcar.NamingContainer;
        }

        protected void Chk_todos_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox Chk_todos = (CheckBox)Grilla.HeaderRow.FindControl("Chk_todos");
            foreach(GridViewRow row in Grilla.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("Chk_marcar");
                if (Chk_todos.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
        }

        protected void btn_genera_mov_stock_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                int v_id_mst = 0;
                int v_swc = 0;
                int v_id_item = 0;

                double v_por_a_mover = 0;
                double v_cantidad = 0;

                v_id_mst = crea_mov_interbodegas();
                // creamos el movimiento de cabecera

                if (v_id_mst > 0)
                {
                    // Si se crea la cabecera... ingresamos el detalle
                    foreach (GridViewRow row in Grilla.Rows)
                    {
                        CheckBox Chk_marcar = row.FindControl("Chk_marcar") as CheckBox;
                        Label lbl_stock_a_mover_grid = row.FindControl("lbl_stock_a_mover_grid") as Label;
                        Label lbl_stock_critico = row.FindControl("lbl_stock_critico") as Label;

                        if (Chk_marcar.Checked)
                        {
                            v_id_item = 0;
                            v_por_a_mover = Convert.ToDouble(lbl_stock_a_mover_grid.Text);
                            crea_det_mov_interbodegas(row.Cells[2].Text, v_por_a_mover, v_id_mst);
                            v_id_item = retorna_id_producto(row.Cells[2].Text.Trim());
                            actualiza_stock_web(v_id_item, v_por_a_mover,Convert.ToDouble(lbl_stock_critico.Text),"IN");
                        }
                    }
                    lbl_status.Text = "Proceso terminado correctamente, puede revisarlo en el ERP con el numero Interno N° " + v_id_mst;
                }
                else
                {
                    lbl_error.Text = "No es posible generar Movimiento Interbodegas, no se crea movimiento ";
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

                    cmd.Parameters.AddWithValue("@v_id_bodega_in", id_bodega_entrada);
                    cmd.Parameters["@v_id_bodega_in"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_bodega_out", id_bodega_salida);
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

                    cmd.Parameters.AddWithValue("@v_id_bodega_in", id_bodega_entrada);
                    cmd.Parameters["@v_id_bodega_in"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_bodega_out", id_bodega_salida);
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

        void actualiza_stock_web(int id_item, double stock, double Stock_critico, string mov)
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

                    cmd.Parameters.AddWithValue("@v_Stock_critico", Stock_critico);
                    cmd.Parameters["@v_Stock_critico"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_mov", mov);
                    cmd.Parameters["@v_mov"].Direction = ParameterDirection.Input;

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

        protected void Lnkvolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void Btn_volveree_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }
    }
}