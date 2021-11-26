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
        int v_id_cliente = 0;
        int v_id_contacto = 0;
        string Sserver = ""; // Conexion Servidor
        string SMysql = ""; // Conexion Server
        string SMysql2 = "";

        Cls_Utilitarios utiles = new Cls_Utilitarios();
        string usuario = "";
        string iniciales_user = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            SMysql2 = utiles.verifica_ambiente("MYSQL2"); // enlace a BBDD Ecommerce
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_008_05", Sserver) == "NO")
                    {
                        Response.Redirect("ErrorAcceso.html");
                    }
                    lbl_conectado.Text = Session["Usuario"].ToString();
                }

                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "D"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Desarrollo"; }
                else
                { lbl_ambiente.Text = "P"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Producción"; }
            

                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");
                lbl_transportista.Text = utiles.obtengo_valor_regla("TRANS", Sserver);

            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }

            //ImgBtn_Cerrar.Attributes["Onclick"] = "return salir();";
            //Btn_autorizar.Attributes["Onclick"] = "return confirm('Al autorizar Clientes en el ERP, permitirá que puedan crear Cotizaciones y NV... Desea Proceder?')";
           // Btn_cargarCliERP.Attributes["Onclick"] = "return confirm('Desea Cargar estos clientes al Sitio Web... Desea Proceder?')";
  

           // lbl_usuario.Text = iniciales_user;

            if (!Page.IsPostBack)
            {
                consulta_clientes_web(1);
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
            queryString = queryString + ",CAST(isnull(EnviarDTE,0) AS INT)  "; // 15
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
 
        protected void lista_clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = lista_clientes.SelectedRow;
            Response.Redirect("Item.aspx?id_item=" + row.Cells[1].Text);
        }

     
        protected void Btn_buscarw_Click(object sender, EventArgs e)
        {
          //  consulta_clientes_web(1);
            consulta_clientes_web(1);
        }

        void consulta_clientes_web (int busca)
        {
            String queryString = "lista_clientes_web";
            lbl_error.Text = "";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    

                    if (txt_idw.Text == "")
                    {
                        command.Parameters.AddWithValue("@v_id_cliente", DBNull.Value);
                        command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_id_cliente", txt_idw.Text);
                        command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;
                    } 

                    if (txt_rutw.Text == "")
                    {
                        command.Parameters.AddWithValue("@v_rut", DBNull.Value);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_rut", txt_rutw.Text);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }

                    if (txt_razonw.Text ==  "")
                    {
                        command.Parameters.AddWithValue("@v_razon_social", DBNull.Value);
                        command.Parameters["@v_razon_social"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_razon_social", txt_razonw.Text);
                        command.Parameters["@v_razon_social"].Direction = ParameterDirection.Input;
                    }


                    command.Parameters.AddWithValue("@v_tipo_busca", busca);
                    command.Parameters["@v_tipo_busca"].Direction = ParameterDirection.Input;
                    
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    lbl_status.Text = "";
                    if (!dr.HasRows)
                    {
                        lbl_status  .Visible = true;
                        lbl_status.Text = "Sin Resultados";
                    }
                    else
                    {
                       lista_clientes.DataSource = dr;
                       lista_clientes.DataBind();

                        if (busca == 1)
                        {
                            lbl_cantidad.Text = "Cantidad de Clientes: " + Convert.ToString(lista_clientes.Rows.Count);
                        }
                        else
                        {
                            lbl_cantidad.Text = "Cantidad de Clientes: " + Convert.ToString(lista_clientes.Rows.Count);
                        }
                        
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

        void insert_cliente_en_ERP(int rut, string dv, string razon_social, string telefonos, string telefonos2, string direccion, string ciudad, string comuna, int Id_region, string giro, string url, string email)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_inserta_cliente_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_rut", rut);
                    cmd.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_dv", dv);
                    cmd.Parameters["@v_dv"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_razon_social", razon_social);
                    cmd.Parameters["@v_razon_social"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_telefonos", telefonos);
                    cmd.Parameters["@v_telefonos"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_telefonos2", telefonos2); // Pendiente creacion Cliente
                    cmd.Parameters["@v_telefonos2"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_direccion", direccion); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_direccion"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_comuna", comuna); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_comuna"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_ciudad", ciudad); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_ciudad"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_region", Id_region); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_id_region"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_giro", giro); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_giro"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_url", url); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_url"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_email", email); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_email"].Direction = ParameterDirection.Input;
                    

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //lbl_error.Text = rdr.GetInt32(0).ToString();
                                v_id_cliente = Convert.ToInt32(rdr.GetInt32(0));
                            }
                        }
                    }
                    

                    if (v_id_cliente != 0)
                    {
                        inserta_contacto_web(rut);
                    }
                    connection.Close();
                    connection.Dispose();

                    actualiza_cliente_sitio_a_erp(rut, v_id_cliente, Convert.ToInt32(Session["id_usuario"].ToString()));
                    //activa_cliente_web(rut + '-' +dv, email, razon_social);
                }
                catch (Exception ex)
                {
                    lbl_error.Text = "Error Proc insert_cliente_en_ERP " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        void activa_cliente_web(string rut, string email, string cliente)
        {

            // Actualizamos los valores
            string query = "";

            query = "update dilacocl_dilacoweb.clients ";
            query = query + " set active = 1 ";
            query = query + " where replace(rut,'.','') = " + rut; 

            using (MySqlConnection conn = new MySqlConnection(SMysql2))
            {
                try
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }

            string cuerpo_correo = "";


            cuerpo_correo = "<p><strong> BIENVENIDO / A <a href = 'www.dilaco.cl' >DILACO.CL </a></strong></p>";
            cuerpo_correo = cuerpo_correo + "< p > Hola Cliente , "+ cliente + "< br />";
            cuerpo_correo = cuerpo_correo + "Felicitaciones!tu cuenta ha sido creada, desde ahora <br/> ";
            cuerpo_correo = cuerpo_correo + "puede acceder a revisar nuestro catalogo de ventas y <br/>";
            cuerpo_correo = cuerpo_correo + "realizar  pedidos, cotizaciones y consultas <br/>";
            cuerpo_correo = cuerpo_correo + "Importadora Dilaco S.A < br /></p>";

                                // Si la actualización funciona correctamente... enviamos un correo al cliente dando aviso
           utiles.enviar_correo("Bienvenido a Dilaco.cl", cuerpo_correo, email);
        }

        void actualiza_cliente_sitio_a_erp(int v_rut, int v_id_cliente,  int v_usuario)
        {
            string query = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "Actualiza_cliente_web_to_erp";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_rut", v_rut);
                    command.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_id_cliente", v_id_cliente);
                    command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_usuario", v_usuario);
                    command.Parameters["@v_usuario"].Direction = ParameterDirection.Input;

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

        protected void inserta_contacto_web(int rut)
        {
            // creamos el cliente en el ERP para analisis de información
            String queryString = "";
            queryString = "SELECT Rut_cliente, "; // 0
            queryString = queryString + "IFNULL(Nombre,''), ";
            queryString = queryString + "IFNULL(Apellido,''), ";
            queryString = queryString + "IFNULL(Telefono,''), ";
            queryString = queryString + "IFNULL(Telefono2, ''), ";
            queryString = queryString + "IFNULL(Celular,''), "; // 5
            queryString = queryString + "IFNULL(Email,''), ";
            queryString = queryString + "IFNULL(cargo,''), ";
            queryString = queryString + " IFNULL(observaciones,'') "; // 8
            queryString = queryString + "FROM tbl_contactos_clientes where Rut_cliente = " + rut;
          
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.ExecuteNonQuery();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {

                            insert_contacto_desde_web_cliente(Convert.ToInt32(dr.GetString(0)), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7), dr.GetString(8));
                        }
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

        void insert_contacto_desde_web_cliente(int rut, string Nombre, string apellido, string telefono, string telefono2, string Celular, string Email, string cargo, string observaciones)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_inserta_contacto_cliente_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_rut", rut);
                    cmd.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_nombre", Nombre);
                    cmd.Parameters["@v_nombre"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_apellido", apellido);
                    cmd.Parameters["@v_apellido"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_telefonos", telefono);
                    cmd.Parameters["@v_telefonos"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_telefonos2", telefono2); // Pendiente creacion Cliente
                    cmd.Parameters["@v_telefonos2"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_celular", Celular); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_celular"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_email", Email); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_email"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_cargo", cargo); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_cargo"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_obs", observaciones); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_obs"].Direction = ParameterDirection.Input;


                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                               // lbl_error.Text = rdr.GetInt32(0).ToString();
                                v_id_contacto = Convert.ToInt32(rdr.GetInt32(0));
                            }
                        }
                    }


                    if (v_id_contacto != 0)
                    {
                        actualiza_data_web(v_id_cliente, v_id_contacto, rut);
                    }
                    connection.Close();
                    connection.Dispose();

                   // actualiza_cliente_sitio_a_erp(rut, Convert.ToInt32(usuario));
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

        void actualiza_data_web(int id_cliente, int id_contacto, int rut)
        {

            // Actualizamos los valores
            string query = "";

            query = "update dilacocl_dilacoweb.tbl_clientes ";
            query = query + " set id_cliente = " + id_cliente;
            query = query + " , leido_erp = 1, cliente_erp = 1 ";
            query = query + " where rut = " + rut;

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    lbl_status.Text = "Producto grabado correctamente en la Web";

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }

            query = "update tbl_contactos_clientes ";
            query = query + " set id_cliente = " + id_cliente;
            query = query + " ,id_contacto= " + id_contacto;
            query = query + ", leido_erp = 1, Activo = 1 ";
            query = query + " where rut_cliente  =" + rut;

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    lbl_status.Text = "Cliente autorizado";

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("PPal.aspx");
        }

        protected void lista_clientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string envio_correo = "";
            if (e.CommandName == "detalle")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = lista_clientes.Rows[index];

                Response.Redirect("Adm_Clientes_Det.aspx?rut_cliente=" + (Convert.ToInt32(row.Cells[1].Text)) + "&id_cliente="+ (Convert.ToInt32(row.Cells[0].Text)));
            }

            if (e.CommandName == "apruebo")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = lista_clientes.Rows[index];
                if (Convert.ToInt32(row.Cells[0].Text) > 0)
                {
                    lbl_error.Text = "Cliente " + Context.Server.HtmlDecode(row.Cells[3].Text)  + " ya fue aprobado";
                }
                else
                {
                    lbl_error.Text = "";
                    // Procedimiento de creación de cliente
                    inserta_cliente_en_ERP(Convert.ToInt32(row.Cells[0].Text),  // id
                                            Convert.ToInt32(row.Cells[1].Text),  // rut
                                            Context.Server.HtmlDecode(row.Cells[2].Text),  // dv
                                            Context.Server.HtmlDecode(row.Cells[3].Text), // razon
                                            Context.Server.HtmlDecode(row.Cells[4].Text), //
                                            Context.Server.HtmlDecode(row.Cells[5].Text), 
                                            Context.Server.HtmlDecode(row.Cells[6].Text), 
                                            Context.Server.HtmlDecode(row.Cells[7].Text), 
                                            Context.Server.HtmlDecode(row.Cells[8].Text), 
                                            Convert.ToInt32(row.Cells[9].Text),
                                            Context.Server.HtmlDecode(row.Cells[10].Text),
                                            Context.Server.HtmlDecode(row.Cells[11].Text),
                                            Context.Server.HtmlDecode(row.Cells[12].Text)
                                            );
                    // Si la inserción de hizo correctamente... generaremos el correo de aviso de cliente con Precio Especial
                    if (valida_cliente_precio_especial(Convert.ToInt32(row.Cells[1].Text)) == "S")
                    {
                        envio_correo = utiles.obtengo_valor_regla("CORCP", Sserver);
                        if (envio_correo  != "" && envio_correo.Contains("@"))
                        {
                            utiles.enviar_correo("Cliente ERP con Precios Especiales", "Cliente " + Context.Server.HtmlDecode(row.Cells[3].Text) + " , Rut "+ row.Cells[1].Text + "-"+ Context.Server.HtmlDecode(row.Cells[2].Text)  + " ya existe en el ERP como cliente Precio Especial, asigne los valores si es necesario", envio_correo);
                        }
                    }

                    consulta_clientes_web(1);
                }
            }

            if (e.CommandName == "eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = lista_clientes.Rows[index];
                if (Convert.ToInt32(row.Cells[0].Text) > 0)
                {
                    lbl_error.Text = "No puede eliminar cliente " + Context.Server.HtmlDecode(row.Cells[3].Text) + ", ya fue aprobado en el ERP";
                }
                else
                {
                    lbl_error.Text = "";
                    elimina_cliente(row.Cells[1].Text, Context.Server.HtmlDecode(row.Cells[3].Text));
                }
               
            }
           // consulta_clientes_web(1);
        }

        protected void inserta_cliente_en_ERP(int id, int rut, string dv, string razon_social, string telefono, string telefono2, string direccion, string ciudad, string comuna, int id_region, string pais, string email, string giro)
        {
            // Revisamos la información antes de grabarla.. si encontramos errores los grabamos en una grilla especial
            Boolean swc = true;


            if (rut == 0)
            { 
                lbl_error.Text = "Debe informar Rut para insertar";
                swc = false;
            }
            if (dv == "")
            {
                lbl_error.Text = "Debe informar DV para insertar";
                swc = false;
            }
            if (telefono == "" && telefono2 == "" && email == "")
            {
                lbl_error.Text = "Debe informar algun medio de conmunicación";
                swc = false;
            }
            if (pais.ToUpper() != "CHILE")
            {
                lbl_error.Text = "Clientes Extranjeros no se crean en el ERP";
                swc = false;
            }

            if (ciudad == "")
            {
                lbl_error.Text = "No se informa Ciudad";
                swc = false;
            }

            if (comuna == "")
            {
                lbl_error.Text = "No se informa Comuna";
                swc = false;
            }
            if (id_region == 0)
            {
                lbl_error.Text = "No se informa Región del cliente";
                swc = false;
            }

            if (swc)
            {
                insert_cliente_en_ERP(rut, dv, razon_social, telefono, telefono2, direccion, ciudad, comuna, id_region, giro, "", email);
            }

        }


        void elimina_cliente(string rut, string razon)
        {
            string query = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {

                    conn.Open();
                    query = "elimina_cliente";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_rut", Convert.ToInt32(rut));
                    command.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_nombre", razon);
                    command.Parameters["@v_nombre"].Direction = ParameterDirection.Input;

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

                    lbl_status.Text = "Cliente "+ razon +" eliminado(s) correctamente desde Sitio Web";
                    lista_clientes.DataSource = null;
                    lista_clientes.DataBind();
                    
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void lista_clientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
             if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;


                Label lbl_cli_precio_esp = e.Row.FindControl("lbl_cli_precio_esp") as Label;

                int rut = Convert.ToInt32(e.Row.Cells[1].Text);

                if (valida_cliente_precio_especial(rut) == "S")
                {
                    lbl_cli_precio_esp.Text = "SI";

                }

                else
                {
                    lbl_cli_precio_esp.Text = "NO";
                }
            }
        }


        public string valida_cliente_precio_especial(int rut)
        {
            string result = "N";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_valida_cli_pre_esp_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_rut", rut);
                    cmd.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                               // lbl_error.Text = rdr.GetString(0);
                                result = rdr.GetString(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return result;

                    // actualiza_cliente_sitio_a_erp(rut, Convert.ToInt32(usuario));
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    return "N";
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}