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
    public partial class Clientes : System.Web.UI.Page
    {
        string Sserver = @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
        string SMysql = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Local
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Btn_buscar.Attributes["Onclick"] = "return valida()";
                Btn_eliminaCLIWEB.Attributes["Onclick"] = "return confirm('Desea Eliminar Cliente(s) que hoy están registrados en el Sitio Web? Clientes seguirán ingresados en el ERP')";
                ImgBtn_Cerrar.Attributes["Onclick"] = "return salir();";
                lista_clientes_web();
            }
        }


        void lista_clientes_web()
        {
            String queryString = "";
            queryString = "SELECT tbl_clientes.ID_Cliente Id, ";
            queryString = queryString + "tbl_clientes.Rut, ";
            queryString = queryString + "tbl_clientes.Dv_Rut, ";
            queryString = queryString + "tbl_clientes.Razon_Social, ";
            queryString = queryString + "tbl_clientes.Telefonos, ";
            queryString = queryString + "IFNULL(tbl_clientes.Telefonos2,'') Telefonos2, ";
            queryString = queryString + "tbl_clientes.Direccion, ";
            queryString = queryString + "tbl_clientes.Ciudad, ";
            queryString = queryString + "tbl_clientes.Comuna, ";
            queryString = queryString + "tbl_clientes.Id_region, ";
            queryString = queryString + "tbl_clientes.Giro, ";
            queryString = queryString + "IFNULL(tbl_clientes.URL,'') URL, ";
            queryString = queryString + "tbl_clientes.Email, ";
            queryString = queryString + "tbl_clientes.leido_erp, ";
            queryString = queryString + "IFNULL(tbl_clientes.cliente_erp,0) cliente_erp  ";
            queryString = queryString + "FROM dilacocl_dilacoweb.tbl_clientes ";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Context.Server.HtmlDecode(queryString), conn);
                    adapter.Fill(ds);



                    lista_clientes.DataSource = ds;
                    lista_clientes.DataBind();
                    //Productos.DataMember = "tbl_items";


                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + queryString;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }


        void insert_cliente(int id, int rut, string dv, string razon, string fono, string fono2, string direccion, string comuna, string ciudad, int region, string email)
        {
            string query = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "inserta_cliente";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id", id);
                    command.Parameters["@v_id"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_rut", rut);
                    command.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_dv", dv);
                    command.Parameters["@v_dv"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_razon", razon.Replace(",", ".").Replace("&nbsp;", "").Replace("&#209;", "Ñ"));
                    command.Parameters["@v_razon"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_fono", fono.Replace("&nbsp;", ""));
                    command.Parameters["@v_fono"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_fono2", fono2.Replace("&nbsp;", ""));
                    command.Parameters["@v_fono2"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_direccion", direccion.Replace(",", ".").Replace("&nbsp;", ""));
                    command.Parameters["@v_direccion"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_comuna", comuna.Replace("&nbsp;", ""));
                    command.Parameters["@v_comuna"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_ciudad", ciudad.Replace("&nbsp;", ""));
                    command.Parameters["@v_ciudad"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_region", region);
                    command.Parameters["@v_region"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_email", email.Replace("&nbsp;", ""));
                    command.Parameters["@v_email"].Direction = ParameterDirection.Input;

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

                    lbl_status.Text = "Clientes insertados correctamente";
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            string queryString = "";
            Page.Validate();
            if (Page.IsValid)
            {
                queryString = "SELECT distinct cl.id_cliente Id, Rut, Dv_Rut 'Dv', Razon_Social 'Razón Social', Telefono 'Teléfono', Telefono2 'Teléfono2', sc.Direccion 'Dirección', sc.Comuna, sc.Ciudad,sc.Id_Region 'Region', cl.email 'Email' ";
                queryString = queryString + "FROM dbo.tbl_Clientes cl ";
                queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Sucursales_Clientes sc ON cl.ID_Cliente = sc.Id_Cliente  and sc.Sucursal_Principal = 1 ";
                queryString = queryString + "left join dbo.tbl_Riesgo ON cl.Id_Riesgo = tbl_Riesgo.Id_Riesgo ";
                queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Vendedor_cliente ON cl.ID_Cliente = tbl_Vendedor_cliente.id_Cliente ";
                queryString = queryString + "LEFT OUTER JOIN dbo.vis_clientes_lv vc on vc.id_cliente = cl.ID_Cliente ";
                queryString = queryString + "LEFT OUTER JOIN tbl_Regiones rr on rr.ID_Region = sc.Id_Region ";
                queryString = queryString + "WHERE(cl.Es_Cliente = 1) ";
                queryString = queryString + "and isnull(cl.Activo, 0) = 1 ";
                queryString = queryString + "and cl.ID_Cliente not in (select id_cliente from tbl_Descuentos_Unitarios where Activo = 1) ";

                if (txt_id.Text != "")
                {
                    queryString = queryString + "and cl.id_cliente = " + txt_id.Text;
                }
                if (txt_rut.Text != "")
                {
                    queryString = queryString + "and cl.rut = " + txt_rut.Text;
                }
                if (txt_razon.Text != "")
                {
                    queryString = queryString + "and cl.Razon_Social like '%" + txt_razon.Text + "%'";
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(Sserver))
                    {
                        connection.Open();
                        //SqlCommand command = new SqlCommand(sql, connection);
                        SqlDataAdapter reader = new SqlDataAdapter(queryString, connection);
                        DataSet dr = new DataSet();
                        reader.Fill(dr, "tbl_Clientes");
                        if (dr.Tables[0].Rows.Count == 0)
                        {
                            lbl_resultados.Text = "No se encuentran Clientes";
                        }
                        else
                        {
                            
                            ClientesERP.DataSource = dr;
                            ClientesERP.DataBind();
                            Btn_cargarCliERP.Visible = true;
                        }

                        connection.Close();
                        connection.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }
            }
        }

        protected void Btn_cargarCliERP_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                // Recorremos la grilla con los clientes del ERP que estén seleccionados
                foreach (GridViewRow row in ClientesERP.Rows)
                {
                    CheckBox check = row.FindControl("check_selcli") as CheckBox;

                    if (check.Checked)
                    {
                        insert_cliente(Convert.ToInt32(Context.Server.HtmlDecode(row.Cells[1].Text)), Convert.ToInt32(row.Cells[2].Text), Context.Server.HtmlDecode(row.Cells[3].Text), Context.Server.HtmlDecode(row.Cells[4].Text), Context.Server.HtmlDecode(row.Cells[5].Text), Context.Server.HtmlDecode(row.Cells[6].Text), Context.Server.HtmlDecode(row.Cells[7].Text), Context.Server.HtmlDecode(row.Cells[8].Text), Context.Server.HtmlDecode(row.Cells[9].Text), Convert.ToInt32(row.Cells[10].Text), Context.Server.HtmlDecode(row.Cells[11].Text));
                    }
                }
                // recargamos los clientes web
                lista_clientes_web();
            }
        }

        protected void lista_clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = lista_clientes.SelectedRow;
            Response.Redirect("Item.aspx?id_item=" + row.Cells[1].Text);
        }

        protected void Btn_eliminaCLIWEB_Click(object sender, EventArgs e)
        {
            string query = "";
            Page.Validate();
            if (Page.IsValid)
            {
                foreach (GridViewRow row in lista_clientes.Rows)
                {
                    CheckBox check = row.FindControl("Chk_elimina") as CheckBox;

                    if (check.Checked)
                    {
                        using (MySqlConnection conn = new MySqlConnection(SMysql))
                        {
                            query = "DELETE FROM dilacocl_dilacoweb.tbl_clientes WHERE id_cliente = " + Convert.ToInt32(row.Cells[1].Text);
                            try
                            {
                                conn.Open();
                                MySqlCommand command = new MySqlCommand(query, conn);
                                command.ExecuteNonQuery();
                                conn.Close();
                                conn.Dispose();
                                lbl_status.Text = "Cliente(s) eliminado(s) correctamente desde Sitio Web";


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
                // recargamos los clientes web
                lista_clientes_web();
            }
        }

        protected void Btn_buscarw_Click(object sender, EventArgs e)
        {
            String queryString = "";
            lbl_error.Text = "";
            queryString = "SELECT tbl_clientes.ID_Cliente Id, ";
            queryString = queryString + "tbl_clientes.Rut, ";
            queryString = queryString + "tbl_clientes.Dv_Rut, ";
            queryString = queryString + "tbl_clientes.Razon_Social, ";
            queryString = queryString + "tbl_clientes.Telefonos, ";
            queryString = queryString + "tbl_clientes.Telefonos2, ";
            queryString = queryString + "tbl_clientes.Direccion, ";
            queryString = queryString + "tbl_clientes.Ciudad, ";
            queryString = queryString + "tbl_clientes.Comuna, ";
            queryString = queryString + "tbl_clientes.Id_region 'Región', ";
            queryString = queryString + "tbl_clientes.Giro, ";
            queryString = queryString + "tbl_clientes.URL, ";
            queryString = queryString + "tbl_clientes.Email, ";
            queryString = queryString + "tbl_clientes.leido_erp, ";
            queryString = queryString + "tbl_clientes.cliente_erp ";
            queryString = queryString + "FROM dilacocl_dilacoweb.tbl_clientes ";
            queryString = queryString + "WHERE 1 = 1 ";

            if (txt_idw.Text != "")
            {
                queryString = queryString + "and id_cliente = " + txt_idw.Text;
            }
            if (txt_rutw.Text != "")
            {
                queryString = queryString + "and rut = " + txt_rutw.Text;
            }
            if (txt_razonw.Text != "")
            {
                queryString = queryString + "and Razon_Social like '%" + txt_razonw.Text + "%'";
            }

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(queryString, conn);
                    adapter.Fill(ds);



                    lista_clientes.DataSource = ds;
                    lista_clientes.DataBind();
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


        protected void CheckAll(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)lista_clientes.HeaderRow.FindControl("Chck_todos");
            //selecciona_todos(chckheader, "Chck_todos", lista_clientes, "Chk_elimina");
        }

        protected void CheckAll2(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)ClientesERP.HeaderRow.FindControl("Chck_todoserp");
            //selecciona_todos(chckheader, "Chck_todoserp", ClientesERP, "check_selcli");
        }

       /* void selecciona_todos (CheckBox cabecera, string ejecutor, GridView grilla, string buscador)
        {
           // cabecera = (CheckBox)ClientesERP.HeaderRow.FindControl(ejecutor);
            foreach (GridViewRow row in grilla.Rows)
            {
                //CheckBox chckrw = (CheckBox)row.FindControl("Chk_elimina");
                CheckBox check = row.FindControl(buscador) as CheckBox;
                if (cabecera.Checked)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }

            }
        }*/

    }
}