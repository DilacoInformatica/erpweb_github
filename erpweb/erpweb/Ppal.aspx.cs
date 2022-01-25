using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace erpweb
{
    public partial class Maqueta : System.Web.UI.Page
    {
        int id_usuario = 0;
        string Sserver = "";
        string SMysql = "";
        int cont = 0;

        int[] barras = new int[7];
        string[] nombres = new string[7];

        int[] barras2 = new int[5];
        string[] nombres2 = new string[5];

        int[] barras3 = new int[5];
        string[] nombres3 = new string[5];

        int[] barras4 = new int[5];
        string[] nombres4 = new string[5];

        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");


            if (Convert.ToString(Session["id_usuario"]) != "")
            {
                id_usuario = Convert.ToInt32(Convert.ToString(Session["id_usuario"]));
                lbl_conectado.Text = utiles.obtiene_nombre_usuario(id_usuario, Sserver);
                lbl_conectado.ToolTip = "Usuario conectado";
            }
            else
            {
                //lbl_ambiente.Text = Convert.ToString(Session["id_usuario"]);
                Response.Redirect("ErrorAcceso.html");
            }

            if (utiles.retorna_ambiente() == "D")
            { lbl_ambiente.Text = "D"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Desarrollo"; }
            else
            { lbl_ambiente.Text = "P"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Producción"; }

            Session["Usuario"] = utiles.obtiene_nombre_usuario(id_usuario, Sserver);

            // Función que regulariza precios con diferencias
           // if (utiles.obtiene_acceso_pagina(utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_009_14", Sserver) == "SI")


             if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_009_14", Sserver) == "SI")
            {
                RevRegPrecios();
            }

            if (!this.IsPostBack)
            {
                llena_movimientos_grafico("C", Grafico1, barras, nombres);
                cont = 0;
                llena_movimientos_grafico("P", Grafico2, barras2, nombres2);
                cont = 0;
                llena_movimientos_grafico("V", Grafico3, barras3, nombres3);
                cont = 0;
                llena_movimientos_grafico("X", Grafico4, barras4, nombres3);
                cont = 0;
                consulta_ingresos_semanales();

            }
        }

        void RevRegPrecios()
        {
            int v_id_item = 0;
            double v_precio_erp = 0;
            string v_codigo = "";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_retorna_precios_descuadrados", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                 
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                v_id_item = rdr.GetInt32(0);
                                
                                v_codigo = rdr.GetString(1);
                                v_precio_erp = rdr.GetDouble(3);
                                // si tengo información... voy a la función que corrige montos en el sitio web
                                if (actualiza_precios_erp(v_id_item, v_precio_erp) == "OK")
                                {
                                   if (actualiza_precios_web(v_id_item, v_precio_erp) != "OK")
                                    {
                                        lbl_error.Text = "Error al actualizar precio " + v_codigo + " Valores con diferencias entre plataformas, consulte con su administrador ";
                                    }
                                }
                                
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

        public string actualiza_precios_erp(int id_item, double precio_erp)
        {
           
            string result = "";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_actualiza_precios_descuadrados", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_item", id_item);
                    cmd.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_precio", precio_erp);
                    cmd.Parameters["@v_precio"].Direction = ParameterDirection.Input;


                    using (SqlDataReader rdr = cmd.ExecuteReader())
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

                    return result;

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    return ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public string actualiza_precios_web (int id_item, double precio_erp)
        {
            String queryString = "Actualiza_precio_item";
            string result = "";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    command.Parameters.AddWithValue("@v_id_item", id_item);
                    command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_nuevo_precio", precio_erp);
                    command.Parameters["@v_nuevo_precio"].Direction = ParameterDirection.Input;

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result = dr.GetString(0).ToString();
                        }
                    }


                    conn.Close();
                    conn.Dispose();

                    return result;
                
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    return ex.Message;
                }
            }
        }

        void consulta_ingresos_semanales()
        {
            String queryString = "consulta_ingresos_semanales";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                 
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        Lst_Movimientos.DataSource = dr;
                        Lst_Movimientos.DataBind();
                    }
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        private void llena_movimientos_grafico(string tipo, System.Web.UI.DataVisualization.Charting.Chart grafico, int[] barras, string[] nombres )
        {
            String queryString = "consulta_movimentos_semanales";

           
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    command.Parameters.AddWithValue("@v_tipo", tipo);
                    command.Parameters["@v_tipo"].Direction = ParameterDirection.Input;

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            barras[cont] = Convert.ToInt32(dr.GetString(0));
                            nombres[cont] = dr.GetString(1);
                                //)

                            cont++;
                        }
                    }

                    conn.Close();
                    conn.Dispose();

                    // LLenamos el Grafico
                    grafico.Series["Series"].Points.DataBindXY(nombres, barras);

                    grafico.ChartAreas["ChartArea"].AxisX.MajorGrid.Enabled = false;
                    grafico.ChartAreas["ChartArea"].AxisY.MajorGrid.Enabled = false;

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

    }
}