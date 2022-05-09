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
    public partial class Contacto_Sitio : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("SSERVER"));
            SMysql = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("MYSQL"));
            ImgBtn_Cerrar.Attributes["Onclick"] = "return salir();";
            if (!this.IsPostBack)
            {
                Btn_buscar.Attributes["Onclick"] = "return valida()";
                // Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
                carga_contacto_sitio();
            }
        }

        void carga_contacto_sitio()
        {
            string queryString = "";
            lbl_mensaje.Text = "";
            queryString = "lista_contactos_sitio ";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    if (txt_numero.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_numero", txt_numero.Text);
                        command.Parameters["@v_numero"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_numero", null);
                        command.Parameters["@v_numero"].Direction = ParameterDirection.Input;
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
                        Lista_contacto.DataSource = dr;
                        Lista_contacto.DataBind();

                        lbl_cantidad.Text = "Cantidad de Registros: " + Convert.ToString(Lista_contacto.Rows.Count);
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
            Page.Validate();
            if (Page.IsValid)
            {
                carga_contacto_sitio();
            }
        }



        protected void Lista_contacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Lista_contacto.SelectedRow;
            Response.Redirect("Detalle_Contacto.aspx?nv=" + row.Cells[1].Text);
        }

        protected void Lista_contacto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }
}