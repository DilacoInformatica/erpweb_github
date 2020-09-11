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
        string Sserver = @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
        //string Sserver = @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
        //string SMysql = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Local
        string SMysql = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;CHARSET=utf8;"; // Conexion Server Local
        int id_nv = 0;
        int v_id_nta_vta = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            id_nv = Convert.ToInt32(Request.QueryString["nv"].ToString()); 
            if (!this.IsPostBack)
            {
                Btn_crearNV.Attributes["Onclick"] = "return confirm('Ud está a punto de Crear esta NV Web en el ERP, desea proceder?')";
                carga_vendedores();
                muestra_info_nv(id_nv);
            }
        }


        void carga_vendedores()
        {
            string sql = "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all select ID_usuario, CONCAT(Apellido_Usu,' ', Nombre_Usu) vendedor from tbl_Usuarios where Activo = 1";
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
                                result = "SI";
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
            queryString = queryString + "a.id_ciudad_despacho, "; //22
            queryString = queryString + "ifnull((select descripcion from tbl_comunas where id_comuna = a.id_comuna_despacho),'') comuna_despacho, "; //23
            queryString = queryString + "a.fono_despacho, "; // 24
            queryString = queryString + "a.email_despacho, "; //25
            queryString = queryString + "a.No_transaccion_web, "; //26
            queryString = queryString + "a.Leido_ERP, "; //27
            queryString = queryString + "1 status_nv ,"; //28
            queryString = queryString + " ifnull(a.contacto_despacho,'') contacto_despacho, "; //29
            queryString = queryString + " (select sigla from tbl_Monedas where ID_Moneda = a.Id_Moneda) Moneda "; //30
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

                            lbl_existe.Text = detecta_cliente(Convert.ToInt32(dr.GetString(6)));
                            v_id_nta_vta = Convert.ToInt32(dr.GetString(0));
                            lbl_numero.Text = dr.GetString(1);
                            lbl_fecha.Text = dr.GetString(2);
                            lbl_transac_pago.Text = dr.GetString(26);
                            // Clientes
                            lbl_cliente.Text = dr.GetString(10);
                            lbl_rut.Text = dr.GetString(6) + '-' + dr.GetString(7);
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

                            double v_neto = Convert.ToDouble(dr.GetString(12));
                            double v_tax = Convert.ToDouble(dr.GetString(13));
                            double v_total = Convert.ToDouble(dr.GetString(11));
                            //lbl_neto.Text = v_neto.ToString("C3", CultureInfo.CurrentCulture);


                            lbl_moneda.Text = dr.GetString(30);
                            lbl_neto.Text = lbl_moneda.Text + ' ' + v_neto.ToString("N2");
                            lbl_tax.Text = lbl_moneda.Text + ' ' + v_tax.ToString("N");
                            lbl_total.Text = lbl_moneda.Text + ' ' + v_total.ToString("N");

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
            String queryString = "";

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

        protected void Btn_crearNV_Click(object sender, EventArgs e)
        {
            int v_id_nta_vta = 0;
            Page.Validate();
            if (Page.IsValid)
            {
                if (Lista_Vendedores.SelectedItem.Value.ToString() == "0")
                {
                    lbl_status.Text = "Debe indicar Vendedor o Representante de Ventas";
                    lbl_status.BackColor = Color.Red;
                }
                else
                {
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
                            cmd.Parameters.AddWithValue("@v_id_nv", v_id_nta_vta);
                            cmd.Parameters["@v_id_nv"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_nv", lbl_numero.Text);
                            cmd.Parameters["@v_nv"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_fecha", lbl_fecha.Text);
                            cmd.Parameters["@v_fecha"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_obs", lbl_obs_despacho.Text);
                            cmd.Parameters["@v_obs"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_Id_cliente", 0); // Pendiente creacion Cliente
                            cmd.Parameters["@v_Id_cliente"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_id_contacto", 0); // Pendiente creacion contacto Cliente
                            cmd.Parameters["@v_id_contacto"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_Suma_total", lbl_total.Text); // Pendiente creacion contacto Cliente
                            cmd.Parameters["@v_Suma_total"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_Neto_venta", lbl_neto.Text); // Pendiente creacion contacto Cliente
                            cmd.Parameters["@v_Neto_venta"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_Tax_venta", lbl_tax.Text); // Pendiente creacion contacto Cliente
                            cmd.Parameters["@v_Tax_venta"].Direction = ParameterDirection.Input;

                            cmd.Parameters.AddWithValue("@v_total", lbl_total.Text); // Pendiente creacion contacto Cliente
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


                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                    {
                                        lbl_status.Text = rdr.GetString(0);
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

                    // Si la inserción de la cabecera resultó correcta (regresa Id de la Nueva NV... insertamos el detalle de la misma
                    if (v_id_nta_vta > 0)
                    {
                        // recoremmos la grilla
                        if (lista_detalles.Rows.Count > 0)
                        {
                            foreach (GridViewRow fila in lista_detalles.Rows)
                            {

                                /*queryString = "select dn.item Item, it.codigo 'Código', it.descripcion 'Descripción' , dn.cantidad 'Cantidad', dn.Valor_Unitario 'Precio Unitario' ";
                                queryString = queryString + "from tbl_items_nv dn inner join tbl_items it on it.id_item = dn.id_item ";
                                queryString = queryString + "where dn.Id_Nta_Vta = " + id_nv;*/

                                using (SqlConnection connection = new SqlConnection(Sserver))
                                {
                                    try
                                    {

                                        connection.Open();
                                        SqlCommand cmd = new SqlCommand("web_carga_nv_det_web", connection);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        SqlParameter param = new SqlParameter();
                                        // Parámetros
                                        cmd.Parameters.AddWithValue("@v_id_nv", v_id_nta_vta);
                                        cmd.Parameters["@v_id_nv"].Direction = ParameterDirection.Input;

                                        cmd.Parameters.AddWithValue("@v_item", fila.Cells[1].ToString());
                                        cmd.Parameters["@v_item"].Direction = ParameterDirection.Input;

                                        cmd.Parameters.AddWithValue("@v_codigo", fila.Cells[2].ToString());
                                        cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                                        cmd.Parameters.AddWithValue("@v_descripcion", fila.Cells[3].ToString());
                                        cmd.Parameters["@v_descripcion"].Direction = ParameterDirection.Input;

                                        cmd.Parameters.AddWithValue("@v_cantidad", fila.Cells[4].ToString()); 
                                        cmd.Parameters["@v_cantidad"].Direction = ParameterDirection.Input;

                                        cmd.Parameters.AddWithValue("@v_precio_unitario", fila.Cells[5].ToString()); 
                                        cmd.Parameters["@v_precio_unitario"].Direction = ParameterDirection.Input;


                                        using (SqlDataReader rdr = cmd.ExecuteReader())
                                        {
                                            while (rdr.Read())
                                            {
                                                if (!rdr.IsDBNull(0))
                                                {
                                                    lbl_status.Text = rdr.GetString(0);
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
                  }
                }
            }
    }
}