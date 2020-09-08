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
        //string Sserver = @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
        String Sserver = @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
        string SMysql = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Local
       //string SMysql = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;"; // Conexion Server Local
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                btn_actualizar.Attributes["Onclick"] = "return confirm('Actualizar Stock del Producto?')";
                muestra_productos();
            }
        }

        void muestra_productos()
        {
            String queryString = "";

            queryString = "select st.id_item Id, codigo 'Codigo', descripcion 'Descripcion', if(it.prodpedido=0,'NO','SI') 'A pedido', round(st.Stock,4) Stock, if(round(st.Stock,4) <=  '5', 'Crítico','Normal') Estado   from ";
            queryString = queryString + "tbl_items it, tbl_stock st ";
            queryString = queryString + "where it.Id_Item = st.Id_Item ";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(queryString, conn);
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        lbl_mensaje.Text = "Sin Resultados";
                    }
                    else
                    {
                        Grilla.DataSource = ds;
                        Grilla.DataBind();
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

        protected void Grilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Grilla.SelectedRow;
            lbl_id_item.Text = row.Cells[1].Text;
            lbl_producto.Text = row.Cells[2].Text;
            lbl_descripcion.Text = row.Cells[3].Text;
            lbl_stock.Text = row.Cells[5].Text;
        }

        protected void txt_stock_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Convert the text to a Double and determine if it is a negative number.
                if (double.Parse(txt_stock.Text) < 0)
                {
                    // If the number is negative, display it in Red.
                    txt_stock.ForeColor = Color.Red;
                }
                else
                {
                    // If the number is not negative, display it in Black.
                    txt_stock.ForeColor = Color.Black;
                }
            }
            catch
            {
                // If there is an error, display the text using the system colors.
                txt_stock.ForeColor = SystemColors.ControlText;
                txt_stock.ForeColor = Color.Red;
                txt_stock.Text = "";
            }
        }

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            string query = "";
            Page.Validate();
            if (Page.IsValid)
            {
                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    try
                    {
                        conn.Open();
                        query = "Actualiza_Stock";
                        MySqlCommand command = new MySqlCommand(query, conn);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@v_id_item", lbl_id_item.Text);
                        command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@v_stock", txt_stock.Text.Replace(",","."));
                        command.Parameters["@v_stock"].Direction = ParameterDirection.Input;

                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                        MySqlDataReader dr = command.ExecuteReader();

                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                lbl_status.Text = dr.GetString(0);
                            }
                        }

                        conn.Close();
                        conn.Dispose();
                        lbl_error.Text = "";

                        lbl_status.Text = "Producto eliminado correctamente de la Web";
                    }
                    catch (Exception ex)
                    {
                        lbl_error.Text = ex.Message + query;
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
    }
}
