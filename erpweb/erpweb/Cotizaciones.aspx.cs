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
    public partial class Cotizaciones : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            //Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
            if (!this.IsPostBack)
            {
                carga_cotizaciones();
            }
        }

        void carga_cotizaciones()
        {
            string queryString = "";
            lbl_mensaje.Text = "";
            queryString = "lista_cot_web ";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    if (txt_cotizacion.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_Cotizac_Num", txt_cotizacion.Text);
                        command.Parameters["@v_Cotizac_Num"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_Cotizac_Num", null);
                        command.Parameters["@v_Cotizac_Num"].Direction = ParameterDirection.Input;
                    }
                    if (txt_rut.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_rut", txt_rut.Text);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_rut", null);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        lbl_mensaje.Text = "Sin Resultados";
                    }
                    else
                    {
                        Lista_cotizacion.DataSource = dr;
                        Lista_cotizacion.DataBind();

                        lbl_cantidad.Text = "Cantidad de Registros: " + Convert.ToString(Lista_cotizacion.Rows.Count);
                    }

                    //Productos.DataMember = "tbl_items";

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

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            carga_cotizaciones();
        }

        protected void Lista_cotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Lista_cotizacion.SelectedRow;
            Response.Redirect("Detalle_Cotizaciones.aspx?cot=" + row.Cells[2].Text);
        }
    }
}