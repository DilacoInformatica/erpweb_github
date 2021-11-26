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
                lbl_conectado.Text =  utiles.obtiene_nombre_usuario(id_usuario, Sserver);
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