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
    public partial class Adm_Clientes : System.Web.UI.Page
    {
        //string Sserver = @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
        //string Sserver = @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
        //string SMysql = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
        // string SMysql = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;"; // Conexion Server Local
        

        string Sserver = ""; // Conexion Servidor
        string SMysql = ""; // Conexion Server
        int validador = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            verifica_ambiente(validador);
            if (!this.IsPostBack)
            {
                Btn_buscar.Attributes["Onclick"] = "return valida()";
                Btn_eliminaCLIWEB.Attributes["Onclick"] = "return confirm('Desea Eliminar Cliente(s) que hoy están registrados en el Sitio Web? Clientes seguirán ingresados en el ERP')";
                ImgBtn_Cerrar.Attributes["Onclick"] = "return salir();";
                lista_clientes_web();
            }
        }

        void verifica_ambiente (int ambiente)
        {
            if (ambiente == 1) // Desarrollo
            {
                Sserver = @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; //Conexion Local
                SMysql = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;"; // Conexion Server Local
            }
            else
            {
                Sserver = @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
                SMysql = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
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
            queryString = queryString + "IFNULL(tbl_clientes.cliente_erp,0) cliente_erp,  ";
            queryString = queryString + "(select count(1) from tbl_contactos_clientes where id_cliente = tbl_clientes.id_cliente) contactos ";
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

        void insert_contactos(int id_cliente)
        {
            string queryString = "";
            queryString = "SELECT ID_Contacto_cliente "; // 0
            queryString = queryString + ",Id_Cliente "; // 1
            queryString = queryString + ",Id_Sucursal "; // 2
            queryString = queryString + ",Nombre "; // 3
            queryString = queryString + ",Apellido "; // 4
            queryString = queryString + ",isnull(Telefono,'') "; //5
            queryString = queryString + ",isnull(Telefono2,'') "; //6
            queryString = queryString + ",isnull(Celular,'') ";
            queryString = queryString + ",isnull(Email,'') ";
            queryString = queryString + ",isnull(Cargo,'') "; // 9
            queryString = queryString + ",isnull(Observaciones,'') "; // 10
            queryString = queryString + ",Activo "; //11
            queryString = queryString + ",Contacto_Principal "; //12
            queryString = queryString + ",Es_Contacto_Cobranza "; //13
            queryString = queryString + ",Id_Departamento"; //14
            queryString = queryString + ",CAST(EnviarDTE AS INT)  "; // 16
            queryString = queryString + "FROM tbl_Contactos_Cliente Where id_cliente = " + id_cliente;


            try
            {
                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

 
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           if (!reader.IsDBNull(0))
                            {

                                int valor0 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(1).ToString()));
                                int valor1 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(0).ToString()));
                                int valor2 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(2).ToString()));

                                string valor3 = Context.Server.HtmlDecode(reader.GetString(3).ToString());
                                string valor4 = Context.Server.HtmlDecode(reader.GetString(4).ToString());
                                string valor5 = Context.Server.HtmlDecode(reader.GetString(5).ToString());
                                string valor6 = Context.Server.HtmlDecode(reader.GetString(6).ToString());
                                string valor7 = Context.Server.HtmlDecode(reader.GetString(7).ToString());
                                string valor8 = Context.Server.HtmlDecode(reader.GetString(8).ToString());
                                string valor9 = Context.Server.HtmlDecode(reader.GetString(9).ToString());
                                string valor10 = Context.Server.HtmlDecode(reader.GetString(10).ToString());
                                int valor11 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(15).ToString()));
                        
                                inserta_contacto_cliente(valor0,
                                                         valor1,
                                                         valor2,
                                                         valor3,
                                                         valor4,
                                                         valor5,
                                                         valor6,
                                                         valor7,
                                                         valor8,
                                                         valor9,
                                                         valor10,
                                                         valor11
                                                                                                                );

                            }
                        }

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

        void insert_sucursales(int id_cliente)
        {
            string queryString = "";

            queryString = "SELECT ID_Sucursal "; //0
            queryString = queryString + ", Id_Cliente "; //1
            queryString = queryString + ", isnull(Nombre_Sucursal,'') "; //2
            queryString = queryString + ", isnull(Razon_Social_Alternativa,'') "; //3
            queryString = queryString + ", isnull(Direccion,'') "; //4
            queryString = queryString + ", isnull(Comuna,'') "; //5
            queryString = queryString + ", isnull(Ciudad,'') "; //6
            queryString = queryString + ", isnull(Pais,'') "; //7
            queryString = queryString + ", isnull(Telefono,'') "; //8
            queryString = queryString + ", isnull(Fax,'') ";
            queryString = queryString + ", isnull(email,'') ";
            queryString = queryString + ", isnull(Id_Transportista,0) "; //11
            queryString = queryString + ", isnull(Activo,1) ";
            queryString = queryString + ", isnull(Obs_Despacho,'') ";
            queryString = queryString + ", isnull(Sucursal_Principal,'') "; //14
            queryString = queryString + ", isnull(Oficina,'') ";
            queryString = queryString + ", isnull(Cta_Cte_Transportista,'0') ";
            queryString = queryString + ", isnull(Telefono2,'') "; //17
            queryString = queryString + ", isnull(Id_Region,13) ";
            queryString = queryString + ", isnull(CAST(Salida_con_Guia as int),0) "; //19
            queryString = queryString + ", isnull(CAST(Salida_con_Factura as int),1) ";
            queryString = queryString + ", isnull(Id_Via_Despacho,0) ";
            queryString = queryString + ", isnull(Telefono_Aviso,'') "; //22
            queryString = queryString + ", isnull(OC_Completa,'') ";
            queryString = queryString + ", isnull(Direccion_Facturacion,'') ";
            queryString = queryString + ", isnull(Comuna_Facturacion,'') ";
            queryString = queryString + ", isnull(Ciudad_Facturacion,'') ";
            queryString = queryString + ", isnull(Fono_Facturacion,'') "; //27
            queryString = queryString + ", isnull(Contacto_Facturacion,'') ";
            queryString = queryString + ", isnull(Id_Salida_Factura,1) ";
            queryString = queryString + ", isnull(Otra_Salida_Factura,1) ";
            queryString = queryString + ", isnull(Observaciones_Facturacion,'') "; //31
            queryString = queryString + ", isnull(Casilla,'') ";
            queryString = queryString + ", isnull(email_facturacion,'') "; //32
            queryString = queryString + "FROM dbo.tbl_Sucursales_Clientes Where id_cliente = " + id_cliente;


            try
            {
                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                int valor0 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(0).ToString())); // sucursal
                                int valor1 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(1).ToString())); // id_cliente
                                string valor3 =Context.Server.HtmlDecode(reader.GetString(2).ToString()); // nombre sucursal
                                string valor4 = Context.Server.HtmlDecode(reader.GetString(4).ToString()); // Direccion
                                string valor5 = Context.Server.HtmlDecode(reader.GetString(5).ToString()); // Comuna
                                string valor6 = Context.Server.HtmlDecode(reader.GetString(6).ToString()); // Ciudad
                                int valor7 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(18).ToString())); // id_region
                                string valor8 = Context.Server.HtmlDecode(reader.GetString(8).ToString()); // telefono
                                string valor9 = Context.Server.HtmlDecode(reader.GetString(10).ToString()); // email
                                int valor10 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(19).ToString())); // salida con guia
                                int valor11 = Convert.ToInt32(Context.Server.HtmlDecode(reader.GetSqlInt32(20).ToString())); // salida con factura
                                string valor12 = Context.Server.HtmlDecode(reader.GetString(13).ToString()); // Obs Despacho
                                string valor13 = Context.Server.HtmlDecode(reader.GetString(15).ToString()); // Oficina



                                inserta_sucursal_cliente(valor0,
                                                         valor1,
                                                         valor3,
                                                         valor4,
                                                         valor5,
                                                         valor6,
                                                         valor7,
                                                         valor8,
                                                         valor9,
                                                         valor10,
                                                         valor11,
                                                         valor12,
                                                         valor13
                                                         );

                            }
                        }

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

        void inserta_sucursal_cliente(int v_id_sucursal, int v_id_cliente, string v_nom_sucursal, string v_direccion, string v_comuna, string v_ciudad, int v_id_region, string v_telefono, string v_Email, int v_sal_guia, int v_sal_fact, string v_Observaciones, string v_oficina)
        {
            string query = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "inserta_sucursal_cliente";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id_sucursal", v_id_sucursal);
                    command.Parameters["@v_id_sucursal"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_id_cliente", v_id_cliente);
                    command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_nom_sucursal", v_nom_sucursal);
                    command.Parameters["@v_nom_sucursal"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_direccion", v_direccion);
                    command.Parameters["@v_direccion"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_comuna", v_comuna);
                    command.Parameters["@v_comuna"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_ciudad", v_ciudad);
                    command.Parameters["@v_ciudad"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_id_region", v_id_region);
                    command.Parameters["@v_id_region"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_telefono", v_telefono);
                    command.Parameters["@v_telefono"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Email", v_Email);
                    command.Parameters["@v_Email"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_sal_guia", v_sal_guia);
                    command.Parameters["@v_sal_guia"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_sal_fact", v_sal_fact);
                    command.Parameters["@v_sal_fact"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Observaciones", v_Observaciones);
                    command.Parameters["@v_Observaciones"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_oficina", v_oficina);
                    command.Parameters["@v_oficina"].Direction = ParameterDirection.Input;

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

                    lbl_status.Text = "Sucursales insertados correctamente";
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        void inserta_contacto_cliente(int v_Id_cliente, int v_Id_contacto, int v_Id_sucursal, string v_Nombre, string v_Apellido, string v_Telefono, string v_Telefono2, string v_Celular, string v_Email, string v_Cargo,string v_Observaciones,int v_EnviarDTE)
        {
            string query = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "inserta_contacto_cliente";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_Id_cliente", v_Id_cliente);
                    command.Parameters["@v_Id_cliente"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Id_contacto", v_Id_contacto);
                    command.Parameters["@v_Id_contacto"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Id_sucursal", v_Id_sucursal);
                    command.Parameters["@v_Id_sucursal"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Nombre", v_Nombre);
                    command.Parameters["@v_Nombre"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Apellido", v_Apellido);
                    command.Parameters["@v_Apellido"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Telefono", v_Telefono);
                    command.Parameters["@v_Telefono"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Telefono2", v_Telefono2);
                    command.Parameters["@v_Telefono2"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Celular", v_Celular);
                    command.Parameters["@v_Celular"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Email", v_Email);
                    command.Parameters["@v_Email"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Cargo", v_Cargo);
                    command.Parameters["@v_Cargo"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_Observaciones", v_Observaciones);
                    command.Parameters["@v_Observaciones"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_EnviarDTE", v_EnviarDTE);
                    command.Parameters["@v_EnviarDTE"].Direction = ParameterDirection.Input;

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
                        insert_contactos(Convert.ToInt32(Context.Server.HtmlDecode(row.Cells[1].Text)));
                        insert_sucursales(Convert.ToInt32(Context.Server.HtmlDecode(row.Cells[1].Text)));
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

                               /* conn.Open();
                                MySqlCommand command = new MySqlCommand(query, conn);
                                command.ExecuteNonQuery();
                                conn.Close();
                                conn.Dispose();
                                lbl_status.Text = "Cliente(s) eliminado(s) correctamente desde Sitio Web";*/
                                

                                conn.Open();
                                query = "elimina_cliente";
                                MySqlCommand command = new MySqlCommand(query, conn);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@v_Id_cliente", Convert.ToInt32(row.Cells[1].Text));
                                command.Parameters["@v_Id_cliente"].Direction = ParameterDirection.Input;
                                
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

                                lbl_status.Text = "Cliente(s) eliminado(s) correctamente desde Sitio Web";
                                lista_clientes.DataSource = null;
                                lista_clientes.DataBind();
                                lista_clientes_web();
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
            queryString = queryString + "tbl_clientes.cliente_erp, ";
            queryString = queryString + "(select count(1) from tbl_contactos_clientes where id_cliente = tbl_clientes.id_cliente) contactos ";
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

        protected void lista_clientes_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id"); //0
            dt.Columns.Add("Rut");
            dt.Columns.Add("Dv_Rut");
            dt.Columns.Add("Razon_Social");
            dt.Columns.Add("Telefonos");
            dt.Columns.Add("Telefonos2");
            dt.Columns.Add("Direccion"); // 6
            dt.Columns.Add("Ciudad");
            dt.Columns.Add("Comuna");
            dt.Columns.Add("Id_region");
            dt.Columns.Add("Giro");
            dt.Columns.Add("URL");
            dt.Columns.Add("Email");
            dt.Columns.Add("leido_erp");
            dt.Columns.Add("cliente_erp");
            // 12

            foreach (GridViewRow gvr in lista_clientes.Rows)
            {
                dt.Rows.Add(gvr.Cells[1].Text, gvr.Cells[2].Text,
                gvr.Cells[3].Text.Replace("&nbsp;", ""), gvr.Cells[4].Text.Replace("&nbsp;", ""), gvr.Cells[5].Text.Replace("&nbsp;", ""),
                gvr.Cells[6].Text.Replace("&nbsp;", ""), gvr.Cells[7].Text.Replace("&nbsp;", ""), gvr.Cells[8].Text.Replace("&nbsp;", ""),
                gvr.Cells[9].Text.Replace("&nbsp;", ""), gvr.Cells[10].Text.Replace("&nbsp;", ""), gvr.Cells[11].Text.Replace("&nbsp;", ""),
                gvr.Cells[12].Text.Replace("&nbsp;", ""), gvr.Cells[13].Text.Replace("&nbsp;", ""), gvr.Cells[14].Text.Replace("&nbsp;", ""));
            }



            if (dt != null)
            {
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                lista_clientes.DataSource = dataView;
                lista_clientes.DataBind();
            }
        }

      

        protected void ClientesERP_Sorting(object sender, GridViewSortEventArgs e)
        {


            /* queryString = "SELECT distinct cl.id_cliente Id, Rut, Dv_Rut 'Dv', Razon_Social 'Razón Social', Telefono 'Teléfono', Telefono2 'Teléfono2', sc.Direccion 'Dirección', sc.Comuna, sc.Ciudad,sc.Id_Region 'Region', cl.email 'Email' ";
                queryString = queryString + "FROM dbo.tbl_Clientes cl ";
                queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Sucursales_Clientes sc ON cl.ID_Cliente = sc.Id_Cliente  and sc.Sucursal_Principal = 1 ";
                queryString = queryString + "left join dbo.tbl_Riesgo ON cl.Id_Riesgo = tbl_Riesgo.Id_Riesgo ";
                queryString = queryString + "LEFT OUTER JOIN dbo.tbl_Vendedor_cliente ON cl.ID_Cliente = tbl_Vendedor_cliente.id_Cliente ";
                queryString = queryString + "LEFT OUTER JOIN dbo.vis_clientes_lv vc on vc.id_cliente = cl.ID_Cliente ";
                queryString = queryString + "LEFT OUTER JOIN tbl_Regiones rr on rr.ID_Region = sc.Id_Region ";
                queryString = queryString + "WHERE(cl.Es_Cliente = 1) ";
                queryString = queryString + "and isnull(cl.Activo, 0) = 1 ";
                queryString = queryString + "and cl.ID_Cliente not in (select id_cliente from tbl_Descuentos_Unitarios where Activo = 1) ";
    ClientesERP */ 

            DataTable dt = new DataTable();
            dt.Columns.Add("Id"); //0
            dt.Columns.Add("Rut");
            dt.Columns.Add("DV");
            dt.Columns.Add("Razón Social");
            dt.Columns.Add("Teléfono");
            dt.Columns.Add("Teléfono2");
            dt.Columns.Add("Dirección"); // 6
            dt.Columns.Add("Comuna");
            dt.Columns.Add("Ciudad");
            dt.Columns.Add("Region");
            dt.Columns.Add("Email");
  
            // 12

            foreach (GridViewRow gvr in ClientesERP.Rows)
            {
                dt.Rows.Add(gvr.Cells[1].Text, gvr.Cells[2].Text,
                gvr.Cells[3].Text.Replace("&nbsp;", ""), gvr.Cells[4].Text.Replace("&nbsp;", ""), gvr.Cells[5].Text.Replace("&nbsp;", ""),
                gvr.Cells[6].Text.Replace("&nbsp;", ""), gvr.Cells[7].Text.Replace("&nbsp;", ""), gvr.Cells[8].Text.Replace("&nbsp;", ""),
                gvr.Cells[9].Text.Replace("&nbsp;", ""), gvr.Cells[10].Text.Replace("&nbsp;", ""));
            }



            if (dt != null)
            {
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                ClientesERP.DataSource = dataView;
                ClientesERP.DataBind();
            }
        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
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