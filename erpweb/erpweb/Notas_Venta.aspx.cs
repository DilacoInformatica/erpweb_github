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
    public partial class Notas_Venta : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        string usuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            ImgBtn_Cerrar.Attributes["Onclick"] = "return salir();";
            if (String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                usuario = "2"; // mi usuarios por default mientras no nos conectemos al servidor
            }
            else
            {
                usuario = Request.QueryString["usuario"].ToString();
            }
            if (!this.IsPostBack)
            {
                Btn_buscar.Attributes["Onclick"] = "return valida()";
                // Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
                carga_nv();
            }
        }

        void carga_nv()
        {
            string queryString = "";
            lbl_mensaje.Text = "";
            queryString = "lista_nv_web ";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    if (txt_nv.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_Nota_vta", txt_nv.Text);
                        command.Parameters["@v_Nota_vta"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_Nota_vta", null);
                        command.Parameters["@v_Nota_vta"].Direction = ParameterDirection.Input;
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
                        Lista_notas.DataSource = dr;
                        Lista_notas.DataBind();

                        lbl_cantidad.Text = "Cantidad de Registros: " + Convert.ToString(Lista_notas.Rows.Count);
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

        protected void Lista_notas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            }
        }


        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                carga_nv();
            }
        }

        protected void Lista_notas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Lista_notas.SelectedRow;
            Response.Redirect("Detalle_NV.aspx?nv=" + row.Cells[1].Text + "&usuario=" + usuario);
        }
    }
}