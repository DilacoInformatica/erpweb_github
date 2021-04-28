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
    public partial class WebForm1 : System.Web.UI.Page
    {
        int id_usuario = 0;
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            if (utiles.retorna_ambiente() == "D")
            { lbl_ambiente.Text = "Desarrollo"; }
            else
            { lbl_ambiente.Text = "Producción"; }
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            if (!String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                id_usuario = Convert.ToInt32(Request.QueryString["usuario"]);
            }
            else
            {
                id_usuario = 141;
            }
            consulta_usuario_conectado(id_usuario);
            Session.Add("Usuario", utiles.obtiene_nombre_usuario(id_usuario, Sserver));
            GrdProdSinStock.Visible = false;
            consulta_productos_sin_stock();
        }

        void consulta_productos_sin_stock()
        {
            String queryString = "lista_productos_sin_stock";
            lbl_error.Text = "";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        GrdProdSinStock.Visible = true;
                        GrdProdSinStock.DataSource = dr;
                        GrdProdSinStock.DataBind();
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

        void consulta_usuario_conectado (int id_usuario)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_usuario", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_usuario", id_usuario);
                    cmd.Parameters["@v_id_usuario"].Direction = ParameterDirection.Input;


                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
   
                                lbl_nombre.Text = rdr.GetString(0);
                            }
                        }
                     }

                    //   GrdDivERP.DataMember = "tbl_Familias_Productos";


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

        protected void GrdProdSinStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }
}