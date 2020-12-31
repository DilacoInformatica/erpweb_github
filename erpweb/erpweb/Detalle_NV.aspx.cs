using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Globalization;

namespace erpweb
{
    public partial class Detalle_NV : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";

        int id_nv = 0;
        int v_id_nta_vta = 0;
        int v_id_cliente = 0;
        int v_id_contacto = 0;
        int id_tipo_fac = 0;
        int usuario = 0;

        Cls_Utilitarios utiles = new Cls_Utilitarios();

        protected void Page_Load(object sender, EventArgs e)
        {

            id_nv = Convert.ToInt32(Request.QueryString["nv"].ToString());
            usuario = Convert.ToInt32(Request.QueryString["usuario"].ToString());
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            if (!this.IsPostBack)
            {
                Btn_crearNV.Attributes["Onclick"] = "return confirm('Ud está a punto de Crear esta NV Web en el ERP, desea proceder?')";
                carga_vendedores();
                if (VerificaexistenciaNVERP(id_nv) == "SI")
                {
                    Btn_crearNV.Enabled = false;
                    lbl_error.Text = "NV N°"+ id_nv  + " ya fue creado en el ERP, consulte con su Administrador";
                    lbl_error.ForeColor = Color.Red;
                }
                muestra_info_nv(id_nv);
            }
        }

        public string VerificaexistenciaNVERP(int id_nv)
        {
            string sql = "select * from tbl_Nota_Venta where Nta_Vta_Num_Web = " + id_nv;
            string result = "N";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                if (rdr.GetInt32(0).ToString() == "0")
                                {
                                    result = "NO";
                                }
                                else
                                {
                                    result = "SI";
                                }
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        void carga_vendedores()
        {
            string sql = ""; // "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all select ID_usuario, CONCAT(Apellido_Usu,' ', Nombre_Usu) vendedor from tbl_Usuarios where Activo = 1 order by Apellido_Usu";
            sql = "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all ";
            sql = sql + "SELECT TOP 100 PERCENT dbo.tbl_Usuarios.ID_usuario, CONCAT(dbo.tbl_Usuarios.Nombre_Usu, ' ', dbo.tbl_Usuarios.Apellido_Usu) ";
            sql = sql + "FROM dbo.tbl_Areas_Empresa RIGHT OUTER JOIN ";
            sql = sql + "dbo.tbl_Cargo ON ";
            sql = sql + "dbo.tbl_Areas_Empresa.ID_Area = dbo.tbl_Cargo.Id_Area RIGHT OUTER JOIN ";
            sql = sql + "dbo.tbl_Usuarios ON ";
            sql = sql + "dbo.tbl_Cargo.ID_Cargo = dbo.tbl_Usuarios.Id_Cargo ";
            sql = sql + "WHERE(dbo.tbl_Areas_Empresa.Puede_Vender = 1) ";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                connection.Open();
                //SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                DataSet dr = new DataSet();
                reader.Fill(dr, "tbl_clientes");
                Lista_Vendedores.DataSource = dr;
                Lista_Vendedores.DataValueField = "ID_usuario";
                Lista_Vendedores.DataTextField = "Vendedor";
                Lista_Vendedores.DataBind();

                connection.Close();
                connection.Dispose();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public string detecta_cliente(int v_rut) 
        {
            string sql = "select count(1) from tbl_clientes where rut = " + v_rut;
            string result = "N";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                if (rdr.GetInt32(0).ToString() == "0")
                                {
                                    result = "NO";
                                }
                                else
                                {
                                    result = "SI";
                                }
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        void muestra_info_nv(int id_nv)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            string queryString = "";
            
            queryString = "SELECT a.Id_Nota_Vta, "; // 0
            queryString = queryString + "a.Nta_vta_num, "; // 1
            queryString = queryString + "DATE_FORMAT(a.fecha, '%d-%m-%Y') fecha, "; // 2
            queryString = queryString + "a.Observaciones, "; //3 
            queryString = queryString + "a.Id_cliente, "; //4
            queryString = queryString + "d.Id_contacto, "; //5
            queryString = queryString + "c.Rut, "; //6
            queryString = queryString + "c.Dv_Rut, "; //7
            queryString = queryString + "c.Telefonos, "; //8
            queryString = queryString + "c.email, "; //9
            queryString = queryString + "c.Razon_Social, "; //10
            queryString = queryString + "a.Suma_total, "; //11
            queryString = queryString + "a.Neto, "; //12
            queryString = queryString + "a.Tax_venta, "; //13
            queryString = queryString + "a.total, "; //14
            queryString = queryString + "c.Direccion 'Dirección', "; //15
            queryString = queryString + "(select nombre_region from tbl_regiones where id_region = c.id_region) region, "; //16
            queryString = queryString + "c.ciudad, "; //17
            queryString = queryString + "c.comuna, "; //18
            queryString = queryString + "a.Obs_despacho, "; //19
            queryString = queryString + "a.Direccion_despacho, "; //20
            queryString = queryString + "(select nombre_region from tbl_regiones where id_region = a.id_region_despacho) region_despacho , "; //21
            queryString = queryString + "(select nombre from tbl_ciudad where id_ciudad = a.id_ciudad_despacho) ciudad, "; //22
            queryString = queryString + "ifnull((select descripcion from tbl_comunas where id_comuna = a.id_comuna_despacho),'') comuna_despacho, "; //23
            queryString = queryString + "a.fono_despacho, "; // 24
            queryString = queryString + "a.email_despacho, "; //25
            queryString = queryString + "a.No_transaccion_web, "; //26
            queryString = queryString + "a.Leido_ERP, "; //27
            queryString = queryString + "1 status_nv ,"; //28
            queryString = queryString + " ifnull(a.contacto_despacho,'') contacto_despacho, "; //29
            queryString = queryString + " (select sigla from tbl_Monedas where ID_Moneda = a.Id_Moneda) Moneda, "; //30
            queryString = queryString + " ifnull(numero_oc,0) numero_oc, ";
            queryString = queryString + " ifnull(id_tipo_fact,1) id_tipo_fact ";
            queryString = queryString + "FROM tbl_nota_vta a ";
            queryString = queryString + "inner join tbl_clientes c on c.id_cliente = a.Id_cliente ";
            queryString = queryString + "left outer join tbl_contactos_clientes d on d.id_cliente = c.Id_cliente ";
            queryString = queryString + " WHERE a.Nta_vta_num = " + id_nv;

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
                            // NV
                            lbl_id_nv.Text = dr.GetString(0);
                            lbl_existe.Text = detecta_cliente(Convert.ToInt32(dr.GetString(6)));
                            v_id_nta_vta = Convert.ToInt32(dr.GetString(0));
                            lbl_numero.Text = dr.GetString(1);
                            lbl_fecha.Text = dr.GetString(2);
                            lbl_transac_pago.Text = dr.GetString(26);
                            // Clientes
                            lbl_cliente.Text = dr.GetString(10);
                            lbl_rut.Text = dr.GetString(6) + '-' + dr.GetString(7);
                            lbl_rut_exit.Text = dr.GetString(6);
                            lbl_fono.Text = dr.GetString(8);
                            lbl_email.Text = dr.GetString(9);
                            lbl_direccion.Text = dr.GetString(16);
                            lbl_comuna.Text = dr.GetString(17);
                            lbl_ciudad.Text = dr.GetString(18);
                            lbl_region.Text = dr.GetString(16);
                            // Despacho
                            lbl_contacto.Text = dr.GetString(29);
                            lbl_email_contacto.Text = dr.GetString(25);
                            lbl_direccion_despacho.Text = dr.GetString(20);
                            lbl_comuna_despacho.Text = dr.GetString(23);
                            lbl_ciudad_despacho.Text = dr.GetString(22);
                            lbl_obs_despacho.Text = dr.GetString(19);
                            lbl_fono_despacho.Text = dr.GetString(24);
                            lbl_region_despacho.Text = dr.GetString(21);

                            double v_neto = Convert.ToDouble(dr.GetString(12));
                            double v_tax = Convert.ToDouble(dr.GetString(13));
                            double v_total = Convert.ToDouble(dr.GetString(11));
                            //lbl_neto.Text = v_neto.ToString("C3", CultureInfo.CurrentCulture);


                            lbl_moneda.Text = dr.GetString(30);
                            lbl_neto.Text = lbl_moneda.Text + ' ' + v_neto.ToString("N2");
                            lbl_tax.Text = lbl_moneda.Text + ' ' + v_tax.ToString("N");
                            lbl_total.Text = lbl_moneda.Text + ' ' + v_total.ToString("N");
                            lbl_n_oc.Text = dr.GetString(31);

                            if (dr.GetInt32(32) == 1)
                            {
                                id_tipo_fac = 1;
                                lbl_tipo_facturacion.Text = "Factura Electrónica";
                            }
                            else
                            {
                                id_tipo_fac = 2;
                                lbl_tipo_facturacion.Text = "Boleta Electrónica";
                            }
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                    detalle_nv(v_id_nta_vta);
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        void detalle_nv(int id_nv)
        {
            string queryString = "";

            queryString = "select dn.item Item, it.codigo 'Código', it.descripcion 'Descripción' , dn.cantidad 'Cantidad', dn.Valor_Unitario 'Precio Unitario' ";
            queryString = queryString + "from tbl_items_nv dn inner join tbl_items it on it.id_item = dn.id_item ";
            queryString = queryString + "where dn.Id_Nta_Vta = " + id_nv;

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
                        lbl_status.Text = "Sin Resultados";
                    }
                    else
                    {
                        lista_detalles.DataSource = ds;
                        lista_detalles.DataBind();
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

        protected void inserta_cliente_en_ERP(int rut)
        {
            // creamos el cliente en el ERP para analisis de información
            String queryString = "";

            queryString = "SELECT Rut, "; // 0
            queryString = queryString + "Dv_Rut, "; //1
            queryString = queryString + "Razon_Social, "; // 2
            queryString = queryString + "IFNULL(Telefonos,''), "; //3
            queryString = queryString + "IFNULL(Telefonos2,''), "; //4
            queryString = queryString + "Direccion, "; // 5
            queryString = queryString + "Ciudad, "; //6
            queryString = queryString + "Comuna, ";//7
            queryString = queryString + "Id_region, "; //8
            queryString = queryString + "IFNULL(Giro,''), "; //9
            queryString = queryString + "IFNULL(URL,''), "; //10
            queryString = queryString + "Email, "; //11
            queryString = queryString + "IFNULL(leido_erp,0), ";// 12
            queryString = queryString + "IFNULL(cliente_erp,0) "; //13
            queryString = queryString + "FROM tbl_clientes where Rut = " + rut;
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

                           insert_cliente_en_ERP(Convert.ToInt32(dr.GetString(0)), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5),dr.GetString(6), dr.GetString(7),Convert.ToInt32(dr.GetString(8)), dr.GetString(9),dr.GetString(10), dr.GetString(11));
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
                                lbl_error.Text = rdr.GetInt32(0).ToString();
                                v_id_cliente = Convert.ToInt32(rdr.GetInt32(0));
                            }
                        }
                    }

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

        public int busca_info_cliente(int rut, string busco)
        {
            string sql = "select ID_Contacto_cliente from tbl_Contactos_Cliente where id_cliente = (select id_cliente from tbl_clientes where rut =  " + rut + ") and Contacto_Principal = 1";

            if (busco == "C")
            {
                sql = "select ID_Contacto_cliente from tbl_Contactos_Cliente where id_cliente = (select id_cliente from tbl_clientes where rut =  " + rut + ") and Contacto_Principal = 1";
            }
            if (busco == "I")
            {
                sql = "select id_cliente from tbl_clientes where rut = " + rut ;
            }

            int result = 0;
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                result = Convert.ToInt32(rdr.GetInt32(0).ToString());
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    return 0;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }


        protected void Btn_crearNV_Click(object sender, EventArgs e)
        {
            int v_id_nta_vta = 0;
            int v_id_item_nta_vta = 0;
            string v_email = "";
            Page.Validate();
            if (Page.IsValid)
            {
                if (lista_detalles.Rows.Count == 0)
                {
                    lbl_status.Text = "No hay detalle que ingresar en la NV, revise información";
                    lbl_status.BackColor = Color.Red;
                }
                else
                {
                    if (Lista_Vendedores.SelectedItem.Value.ToString() == "0")
                    {
                        lbl_status.Text = "Debe indicar Vendedor o Representante de Ventas";
                        lbl_status.BackColor = Color.Red;
                    }
                    else
                    {

                        // revisamos la creación del cliente
                        if (lbl_existe.Text == "NO")
                        {
                            //inserta_cliente_en_ERP(Convert.ToInt32(lbl_rut_exit.Text));
                            lbl_error.Text = "Cliente no existe en el ERP, para generar NV primero el Cliente debe ser autorizado y creado";
                            lbl_error.ForeColor = Color.Red;
                            // creamos el cliente 
                        }
                        else
                        {

                            v_id_contacto = busca_info_cliente(Convert.ToInt32(lbl_rut_exit.Text), "C");
                            v_id_cliente = busca_info_cliente(Convert.ToInt32(lbl_rut_exit.Text), "I");
                            // Insertamos la Nv en el ERP
                            using (SqlConnection connection = new SqlConnection(Sserver))
                            {
                                try
                                {
                                    connection.Open();
                                    SqlCommand cmd = new SqlCommand("web_carga_nv_cab_web", connection);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    SqlParameter param = new SqlParameter();
                                    // Parámetros
                                    cmd.Parameters.AddWithValue("@v_id_nv", lbl_id_nv.Text);
                                    cmd.Parameters["@v_id_nv"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_nv", lbl_numero.Text);
                                    cmd.Parameters["@v_nv"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_fecha", lbl_fecha.Text);
                                    cmd.Parameters["@v_fecha"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_obs", lbl_obs_despacho.Text);
                                    cmd.Parameters["@v_obs"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_Id_cliente", v_id_cliente); // Pendiente creacion Cliente
                                    cmd.Parameters["@v_Id_cliente"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_id_contacto", v_id_contacto); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_id_contacto"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_Suma_total", Convert.ToDouble(lbl_total.Text.Replace(lbl_moneda.Text, ""))); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_Suma_total"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_Neto_venta", Convert.ToDouble(lbl_neto.Text.Replace(lbl_moneda.Text, ""))); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_Neto_venta"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_Tax_venta", Convert.ToDouble(lbl_tax.Text.Replace(lbl_moneda.Text, ""))); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_Tax_venta"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_total", Convert.ToDouble(lbl_total.Text.Replace(lbl_moneda.Text, ""))); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_total"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_sigla_moneda", lbl_moneda.Text); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_sigla_moneda"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_Direcciondespacho", lbl_direccion_despacho.Text); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_Direcciondespacho"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_ciudaddespacho", lbl_ciudad_despacho.Text); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_ciudaddespacho"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_comunadespacho", lbl_comuna_despacho.Text); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_comunadespacho"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_Folio_transac_web", lbl_fono_despacho.Text); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_Folio_transac_web"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_id_vendedor", Lista_Vendedores.SelectedItem.Value.ToString()); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_id_vendedor"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_id_tipo_fact", id_tipo_fac); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_id_tipo_fact"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_oc", lbl_n_oc.Text); // Pendiente creacion contacto Cliente
                                    cmd.Parameters["@v_oc"].Direction = ParameterDirection.Input;

                                    using (SqlDataReader rdr = cmd.ExecuteReader())
                                    {
                                        while (rdr.Read())
                                        {
                                            if (!rdr.IsDBNull(0))
                                            {
                                                v_id_nta_vta = rdr.GetInt32(0);
                                            }
                                        }
                                    }

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
                            // v_id_nta_vta = Convert.ToInt32(lbl_status.Text);
                            // Si la inserción de la cabecera resultó correcta (regresa Id de la Nueva NV... insertamos el detalle de la misma
                            if (v_id_nta_vta > 0)
                            {
                                // recoremmos la grilla
                                if (lista_detalles.Rows.Count > 0)
                                {
                                    foreach (GridViewRow fila in lista_detalles.Rows)
                                    {
                                        using (SqlConnection connection = new SqlConnection(Sserver))
                                        {
                                            try
                                            {
                                                int v_id_item = Convert.ToInt32(fila.Cells[0].Text);
                                                string v_codigo = fila.Cells[1].Text;
                                                string v_descrip = fila.Cells[2].Text;
                                                double v_cantidad = Convert.ToInt32(fila.Cells[3].Text);
                                                double v_precio_unitario = Convert.ToInt32(fila.Cells[4].Text);
                                                connection.Open();
                                                SqlCommand cmd = new SqlCommand("web_carga_nv_det_web", connection);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                SqlParameter param = new SqlParameter();
                                                // Parámetros
                                                cmd.Parameters.AddWithValue("@v_id_nv", v_id_nta_vta);
                                                cmd.Parameters["@v_id_nv"].Direction = ParameterDirection.Input;

                                                cmd.Parameters.AddWithValue("@v_item", v_id_item);
                                                cmd.Parameters["@v_item"].Direction = ParameterDirection.Input;

                                                cmd.Parameters.AddWithValue("@v_codigo", v_codigo);
                                                cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                                                cmd.Parameters.AddWithValue("@v_descripcion", v_descrip);
                                                cmd.Parameters["@v_descripcion"].Direction = ParameterDirection.Input;

                                                cmd.Parameters.AddWithValue("@v_cantidad", v_cantidad);
                                                cmd.Parameters["@v_cantidad"].Direction = ParameterDirection.Input;

                                                cmd.Parameters.AddWithValue("@v_precio_unitario", v_precio_unitario);
                                                cmd.Parameters["@v_precio_unitario"].Direction = ParameterDirection.Input;


                                                using (SqlDataReader rdr = cmd.ExecuteReader())
                                                {
                                                    while (rdr.Read())
                                                    {
                                                        if (!rdr.IsDBNull(0))
                                                        {
                                                            v_id_item_nta_vta = rdr.GetInt32(0);
                                                        }
                                                    }
                                                }

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
                                }
                            }
                            if (v_id_nta_vta > 0 && v_id_item_nta_vta > 0 && lbl_error.Text == "")
                            {
                                actualiza_NV(Convert.ToInt32(lbl_numero.Text));
                                entrega_num_nv_erp(v_id_nta_vta);
                                lbl_status.Text = "Nota de Venta creada correctamente en el ERP, revise el Home";
                                utiles.actualiza_historial_nv(v_id_nta_vta, usuario, "Se crea NV desde Sitio Web",Sserver,"NV");
                                lbl_status.ForeColor = Color.Red;
                                v_email = utiles.obtiene_email_usuario(Convert.ToInt32(Lista_Vendedores.SelectedItem.Value.ToString()),Sserver);
                                utiles.enviar_correo("Nv Web asignada", "Nv Web " + lbl_numero.Text + " fue creada en el ERP con el numero " + lbl_numero_erp.Text + ", esta fue asignada a Ud, revisela en el Home", v_email);
                            }
                            // una vez insertada la NV en el ERP... actualizó la NV para que no aparezca más en el listado de pendientes
                        }
                    }
                }
            }
          }

        void entrega_num_nv_erp(int v_id_nta_vta)
        {
            string sql = ""; // "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all select ID_usuario, CONCAT(Apellido_Usu,' ', Nombre_Usu) vendedor from tbl_Usuarios where Activo = 1 order by Apellido_Usu";
            sql = "select distinct Nta_Vta_Num from tbl_Nota_Venta where ID_Nta_Vta = " + v_id_nta_vta;
            
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                lbl_numero_erp.Text = Convert.ToString(rdr.GetInt32(0));
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        void actualiza_NV(int numero)
        {
            string queryString = "";

            queryString = "UPDATE tbl_nota_vta SET Leido_ERP = 1, status = 1 WHERE Nta_vta_num =  " + numero;
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.ExecuteNonQuery();
                   
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
            Response.Redirect("Notas_Venta.aspx");
        }
    }
}