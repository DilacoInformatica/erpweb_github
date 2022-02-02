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
    public partial class Adm_Clientes_Det : System.Web.UI.Page
    {
        string Sserver = ""; // Conexion Servidor
        string SMysql = ""; // Conexion Server
        string SMysql2 = "";
        int v_id_cliente = 0;
        int v_id_contacto = 0;

        Cls_Utilitarios utiles = new Cls_Utilitarios();
        int rut_cliente = 0;
        int id_cliente = 0;
        int id_transportista = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_009_10", Sserver) == "NO")
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

                rut_cliente = Convert.ToInt32(Request.QueryString["rut_cliente"].ToString());
                id_cliente = Convert.ToInt32(Request.QueryString["id_cliente"].ToString());

                if (valida_info_cliente(rut_cliente) == "Existe")
                {
                    lbl_status.Text = "Rut Cliente ya está ingresado y validado cómo cliente, no puede ingresarlo nuevamente";
                    LnkBtn_Aprobar.Visible = false;
                    LnkBtn_Rechazar.Visible = false;
                    Btn_Aprobar.Enabled = false;
                    Btn_Rechazar.Enabled = false;
                    Btn_Correo.Enabled = false; 
                }

                if (id_cliente > 0)
                {
                    lbl_status.Text = "Cliente ya fue aprobado en el ERP, no es posible reaprobar o eliminar";
                    Btn_Aprobar.Enabled = false;
                    Btn_Rechazar.Enabled = false;
                    LnkBtn_Aprobar.Visible = false;
                    LnkBtn_Rechazar.Visible = false;
                    Btn_Correo.Enabled = false;
                    LnkBtnCorreo.Visible = false;
                }
            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }

            Btn_Aprobar.Attributes["Onclick"] = "return confirm('¿Desea aprobar cliente? Recuede que los cambios pueden ser factor de error en futuras compras')";
            Btn_Rechazar.Attributes["Onclick"] = "return confirm('¿Confirmar rechazo cliente?')";

            LnkBtn_Aprobar.Attributes["Onclick"] = "return confirm('¿Desea aprobar cliente? Recuede que los cambios pueden ser factor de error en futuras compras')";
            LnkBtn_Rechazar.Attributes["Onclick"] = "return confirm('¿Confirmar rechazo cliente?')";


            if (!this.IsPostBack)
            {
                //carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                carga_contrl_lista("select ID_Transportista, Nombre_transportista from tbl_Transportista where Activo = 1 order by Nombre_transportista", Lst_Trasnportistas, "tbl_Transportista", "ID_Transportista", "Nombre_transportista");
                id_transportista = Convert.ToInt32(utiles.obtengo_valor_regla("IDTRAN", Sserver));
                Lst_Trasnportistas.SelectedValue = id_transportista.ToString();
                muestra_info_cliente();
            }
        }


        public string valida_info_cliente(int rut_cliente)
        {
            String queryString = "valida_cliente_web";
            string status = "";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@v_rut", rut_cliente);
                    command.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();


                    while (dr.Read())
                    {
                        status = dr[0].ToString();
                    }
                    dr.Dispose();

                    conn.Close();
                    conn.Dispose();

                    return status;

                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();

                    return ex.Message;
                }
            }
        }

        void carga_contrl_lista(string sql, DropDownList lista, string tabla, string llave, string Campo)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                connection.Open();
                //SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                DataSet dr = new DataSet();
                reader.Fill(dr, tabla);
                //lista.AppendDataBoundItems = true;
                // lista.Items.Add("Seleccione...");
                lista.DataSource = dr;
                lista.DataValueField = llave;
                lista.DataTextField = Campo;
                lista.DataBind();

                connection.Close();
                connection.Dispose();
            }
        }

        void muestra_info_cliente()
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


                    command.Parameters.AddWithValue("@v_id_cliente", DBNull.Value);
                    command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_rut", rut_cliente);
                    command.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_razon_social", DBNull.Value);
                    command.Parameters["@v_razon_social"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_tipo_busca", 1);
                    command.Parameters["@v_tipo_busca"].Direction = ParameterDirection.Input;

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();


                    while (dr.Read())
                    {
                        lbl_id.Text = dr["id"].ToString();
                        lbl_rut.Text = dr["rut"].ToString() + "-" + dr["Dv_rut"].ToString();
                        lbl_rut_master.Text = dr["rut"].ToString();
                        lbl_dv.Text = dr["Dv_rut"].ToString();
                        lbl_nombre.Text = dr["Razon_Social"].ToString();
                        txt_giro.Text = dr["giro"].ToString();
                        txt_fono1.Text = dr["telefonos"].ToString();
                        txt_fono2.Text = dr["telefonos2"].ToString();
                        txt_ciudad.Text = dr["ciudad"].ToString().Trim();
                        txt_comuna.Text = dr["comuna"].ToString().Trim();
                        txt_region.Text = dr["id_region"].ToString().Trim();
                        txt_direccion.Text = dr["direccion"].ToString().Trim();
                        txt_email.Text = dr["email"].ToString().Trim();
                        txt_pais.Text = dr["pais"].ToString().ToUpper().Trim();

                    }
                    dr.Dispose();

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

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Adm_clientes.aspx");
        }

        protected void Btn_Aprobar_Click(object sender, EventArgs e)
        {
            string envio_correo = "";
            Page.Validate();
            if (Page.IsValid)
            {
                inserta_cliente_en_ERP(Convert.ToInt32(lbl_id.Text),  // id
                                          Convert.ToInt32(lbl_rut_master.Text),  // rut
                                          Context.Server.HtmlDecode(lbl_dv.Text),  // dv
                                          Context.Server.HtmlDecode(lbl_nombre.Text), // razon
                                          Context.Server.HtmlDecode(txt_fono1.Text), //
                                          Context.Server.HtmlDecode(txt_fono2.Text),
                                          Context.Server.HtmlDecode(txt_direccion.Text),
                                          Context.Server.HtmlDecode(txt_ciudad.Text),
                                          Context.Server.HtmlDecode(txt_comuna.Text),
                                          Convert.ToInt32(txt_region.Text),
                                          Context.Server.HtmlDecode(txt_pais.Text),
                                          Context.Server.HtmlDecode(txt_email.Text),
                                          Context.Server.HtmlDecode(txt_giro.Text),
                                          Convert.ToInt32(Lst_Trasnportistas.SelectedValue)
                                          );
                // Si la inserción de hizo correctamente... generaremos el correo de aviso de cliente con Precio Especial
                if (valida_cliente_precio_especial( Convert.ToInt32(lbl_rut_master.Text)) == "S")
                {
                    envio_correo = utiles.obtengo_valor_regla("CORCP", Sserver);
                    if (envio_correo != "" && envio_correo.Contains("@"))
                    {
                        utiles.enviar_correo("Cliente ERP con Precios Especiales", "Cliente " + Context.Server.HtmlDecode(lbl_nombre.Text) + " , Rut " + lbl_rut.Text + " ya existe en el ERP como cliente Precio Especial, asigne los valores si es necesario", envio_correo);
                    }
                }
            }
        }

        protected void inserta_cliente_en_ERP(int id, int rut, string dv, string razon_social, string telefono, string telefono2, string direccion, string ciudad, string comuna, int id_region, string pais, string email, string giro, int transportista)
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
                lbl_error.Text = "Debe informar algun medio de comunicación";
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
            if (transportista == 0)
            {
                lbl_error.Text = "No se informa Transportista";
                swc = false;
            }

            if (swc)
            {
                insert_cliente_en_ERP(rut, dv, razon_social, telefono, telefono2, direccion, ciudad, comuna, id_region, giro, "", email, transportista);
            }

        }

        void insert_cliente_en_ERP(int rut, string dv, string razon_social, string telefonos, string telefonos2, string direccion, string ciudad, string comuna, int Id_region, string giro, string url, string email, int transportista)
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

                    cmd.Parameters.AddWithValue("@v_transportista", transportista); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_transportista"].Direction = ParameterDirection.Input;

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

        void actualiza_cliente_sitio_a_erp(int v_rut, int v_id_cliente, int v_usuario)
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
                            
                    Btn_Aprobar.Enabled = false;
                    Btn_Rechazar.Enabled = false;
                    LnkBtn_Rechazar.Visible = false;
                    LnkBtn_Aprobar.Visible = false;
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

        protected void Btn_Rechazar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                elimina_cliente(lbl_rut_master.Text,  Context.Server.HtmlDecode(lbl_nombre.Text));
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

                    lbl_status.Text = "Cliente " + razon + " eliminado(s) correctamente desde Sitio Web";

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void LnkBtn_Rechazar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                elimina_cliente(lbl_rut_master.Text, Context.Server.HtmlDecode(lbl_nombre.Text));
            }
        }

        protected void LnkBtn_Aprobar_Click(object sender, EventArgs e)
        {
            string envio_correo = "";
            Page.Validate();
            if (Page.IsValid)
            {
                inserta_cliente_en_ERP(Convert.ToInt32(lbl_id.Text),  // id
                                          Convert.ToInt32(lbl_rut_master.Text),  // rut
                                          Context.Server.HtmlDecode(lbl_dv.Text),  // dv
                                          Context.Server.HtmlDecode(lbl_nombre.Text), // razon
                                          Context.Server.HtmlDecode(txt_fono1.Text), //
                                          Context.Server.HtmlDecode(txt_fono2.Text),
                                          Context.Server.HtmlDecode(txt_direccion.Text),
                                          Context.Server.HtmlDecode(txt_ciudad.Text),
                                          Context.Server.HtmlDecode(txt_comuna.Text),
                                          Convert.ToInt32(txt_region.Text),
                                          Context.Server.HtmlDecode(txt_pais.Text),
                                          Context.Server.HtmlDecode(txt_email.Text),
                                          Context.Server.HtmlDecode(txt_giro.Text),
                                          Convert.ToInt32(Lst_Trasnportistas.SelectedValue)
                                          );
                // Si la inserción de hizo correctamente... generaremos el correo de aviso de cliente con Precio Especial
                if (valida_cliente_precio_especial(Convert.ToInt32(lbl_rut_master.Text)) == "S")
                {
                    envio_correo = utiles.obtengo_valor_regla("CORCP", Sserver);
                    if (envio_correo != "" && envio_correo.Contains("@"))
                    {
                        utiles.enviar_correo("Cliente ERP con Precios Especiales", "Cliente " + Context.Server.HtmlDecode(lbl_nombre.Text) + " , Rut " + lbl_rut.Text + " ya existe en el ERP como cliente Precio Especial, asigne los valores si es necesario", envio_correo);
                    }
                }
            }
        }

        protected void LnkBtn_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Adm_clientes.aspx");
        }

        protected void LnkBtnCorreo_Click(object sender, EventArgs e)
        {
            Btn_Enviar.Enabled = true;
            lbl_correo_de.Text = "Informatica@dilaco.com";
            lbl_correo_a.Text = txt_email.Text;
        }

        
        protected void Btn_Enviar_Click(object sender, EventArgs e)
        {
            Btn_Enviar.Enabled = true;
            utiles.enviar_correo("Solicitud de Información", txt_cuerpo.Text, "saranda@dilaco.com");
        }
    }
}