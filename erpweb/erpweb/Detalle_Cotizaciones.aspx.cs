using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Drawing;

namespace erpweb
{
    public partial class Detalle_Cotizaciones : System.Web.UI.Page
    {
        int num_cotizacion = 0;
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        int usuario = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            num_cotizacion = Convert.ToInt32(Request.QueryString["cot"].ToString());
           // usuario = Convert.ToInt32(Request.QueryString["usuario"].ToString());

            if (String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                usuario = 2; // mi usuarios por default mientras no nos conectemos al servidor
            }
            else
            {
                usuario = Convert.ToInt32(Request.QueryString["usuario"].ToString());
            }

            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");

            Btn_crearCot.Attributes["Onclick"] = "return confirm('Ud está a punto de Crear esta Cotización Web en el ERP, desea proceder?')";
            if (!this.IsPostBack)
            {
                carga_vendedores();
                if (VerificaexistenciaCotERP(num_cotizacion) == "SI")
                {
                    Btn_crearCot.Enabled = false;
                    lbl_error.Text = "Cotización N° " + num_cotizacion + " ya fue creado en el ERP, consulte con su Administrador";
                    lbl_error.ForeColor = Color.Red;
                }
                muestra_info_cotizacion(num_cotizacion);
            }
        }

        public string VerificaexistenciaCotERP(int num_cotizacion)
        {
            string sql = "select COUNT(1) from tbl_Cotizaciones where Cotizac_Num_Web = " + num_cotizacion;
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

        void muestra_info_cotizacion(int num_cotizacion)
        {
            int v_id_cotizacion = 0;
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            string queryString = "";

            queryString = "select ct.ID_Cotizacion, "; //0 
            queryString = queryString + " ct.Cotizac_Num, "; // 1
            queryString = queryString + " ct.Nombre_Cotizacion, "; // 2
            queryString = queryString + " ct.Id_Tipo_Cotizacion, "; // 3
            queryString = queryString + " cl.Rut, "; //4
            queryString = queryString + " cl.Dv_Rut, "; //5
            queryString = queryString + " cl.Razon_Social, "; //6
            queryString = queryString + " cl.direccion, "; //7
            queryString = queryString + " cl.Comuna, "; //8
            queryString = queryString + " cl.Ciudad, "; //9
            queryString = queryString + " cl.Telefonos, "; //10
            queryString = queryString + " cl.Email, "; //11
            queryString = queryString + " DATE_FORMAT(ct.Fecha_Cotizac, '%d-%m-%Y') fecha, "; // 12
            queryString = queryString + " cl.id_region, "; //13
            queryString = queryString + " (select replace(nombre_region,'Región','') from tbl_regiones where id_region = cl.Id_region) region, "; //14
            queryString = queryString + " IFNULL(ct.Neto_Venta,0),  IFNULL(ct.Tax_venta,0), IFNULL(ct.TOTAL,0),  "; // 15, 16, 17, 18
            queryString = queryString + " IFNULL((select sigla from tbl_Monedas where ID_Moneda = ct.id_moneda),1) Moneda "; //19
            queryString = queryString + "  from tbl_cotizaciones ct ";
            queryString = queryString + "  inner join tbl_clientes cl on cl.id_cliente = ct.id_cliente ";
            queryString = queryString + "  where ct.Cotizac_Num = " + num_cotizacion;

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
                            v_id_cotizacion = Convert.ToInt32(dr.GetString(0));
                            lbl_id_cot.Text = dr.GetString(0);
                            lbl_existe.Text = detecta_cliente(Convert.ToInt32(dr.GetString(4)));
                            //v_id_nta_vta = Convert.ToInt32(dr.GetString(0));
                            lbl_numero.Text = dr.GetString(1);
                            lbl_fecha.Text = dr.GetString(12);
                            // Clientes
                            lbl_cliente.Text = dr.GetString(6);
                            lbl_rut.Text = dr.GetString(4) + '-' + dr.GetString(5);
                            lbl_rut_exit.Text = dr.GetString(4);
                            lbl_fono.Text = dr.GetString(10);
                            lbl_email.Text = dr.GetString(11);
                            lbl_direccion.Text = dr.GetString(7);
                            lbl_comuna.Text = dr.GetString(8);
                            lbl_ciudad.Text = dr.GetString(9);
                            lbl_region.Text = dr.GetString(14);
                            // Despacho

                            double v_neto = Convert.ToDouble(dr.GetString(15));
                            double v_tax = Convert.ToDouble(dr.GetString(16));
                            double v_total = Convert.ToDouble(dr.GetString(17));
                            //lbl_neto.Text = v_neto.ToString("C3", CultureInfo.CurrentCulture);

                            lbl_moneda.Text = dr.GetString(18);
                            lbl_neto.Text = lbl_moneda.Text + ' ' + v_neto.ToString("N2");
                            lbl_tax.Text = lbl_moneda.Text + ' ' + v_tax.ToString("N");
                            lbl_total.Text = lbl_moneda.Text + ' ' + v_total.ToString("N");
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                    detalle_cotizacion(v_id_cotizacion);
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        void detalle_cotizacion(int v_id_cotizacion)
        {
            string queryString = "";

            queryString = "select dn.item Item, it.codigo 'Codigo', it.descripcion 'Descripcion' , dn.cantidad 'Cantidad', IFNULL(dn.Valor_Unitario,1) 'Precio Unitario' ";
            queryString = queryString + "from tbl_items_cotizacion dn inner ";
            queryString = queryString + "join tbl_items it on it.id_item = dn.id_item ";
            queryString = queryString + "where dn.ID_Cotizacion = " + v_id_cotizacion;

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

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cotizaciones.aspx");
        }

        protected void Btn_crearCot_Click(object sender, EventArgs e)
        {
            int v_id_contacto = 0;
            int v_id_cliente = 0;
            int v_id_cotizacion = 0;
            int v_id_item_cotizacion = 0;
            string v_email = "";
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
                    // revisamos la creación del cliente
                    if (lbl_existe.Text == "NO")
                    {
                        //inserta_cliente_en_ERP(Convert.ToInt32(lbl_rut_exit.Text));
                        lbl_error.Text = "Cliente no existe en el ERP, para generar Cotizaciones primero el Cliente debe ser autorizado y creado";
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
                                SqlCommand cmd = new SqlCommand("web_carga_cot_cab_web", connection);
                                cmd.CommandType = CommandType.StoredProcedure;
                                SqlParameter param = new SqlParameter();
                                // Parámetros
                                cmd.Parameters.AddWithValue("@v_id_cot", lbl_id_cot.Text);
                                cmd.Parameters["@v_id_cot"].Direction = ParameterDirection.Input;

                                cmd.Parameters.AddWithValue("@v_ncot", lbl_numero.Text);
                                cmd.Parameters["@v_ncot"].Direction = ParameterDirection.Input;

                                cmd.Parameters.AddWithValue("@v_fecha", lbl_fecha.Text);
                                cmd.Parameters["@v_fecha"].Direction = ParameterDirection.Input;

                                cmd.Parameters.AddWithValue("@v_obs", "Cotización Web");
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

                                cmd.Parameters.AddWithValue("@v_id_vendedor", Lista_Vendedores.SelectedItem.Value.ToString()); // Pendiente creacion contacto Cliente
                                cmd.Parameters["@v_id_vendedor"].Direction = ParameterDirection.Input;

                                using (SqlDataReader rdr = cmd.ExecuteReader())
                                {
                                    while (rdr.Read())
                                    {
                                        if (!rdr.IsDBNull(0))
                                        {
                                            //lbl_error.Text = rdr.GetInt32(0).ToString();
                                            v_id_cotizacion = Convert.ToInt32(rdr.GetInt32(0));
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
                        if (v_id_cotizacion > 0)
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
                                            SqlCommand cmd = new SqlCommand("web_carga_cot_det_web", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter param = new SqlParameter();
                                            // Parámetros
                                            cmd.Parameters.AddWithValue("@v_id_cot", v_id_cotizacion);
                                            cmd.Parameters["@v_id_cot"].Direction = ParameterDirection.Input;

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
                                                        v_id_item_cotizacion = rdr.GetInt32(0);
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
                        if (v_id_cotizacion > 0 && v_id_item_cotizacion > 0 && lbl_error.Text == "")
                        {
                            actualiza_Cotizacion(Convert.ToInt32(lbl_numero.Text));
                            entrega_num_cot_erp(v_id_cotizacion);
                            lbl_status.Text = "Cotización creada correctamente en el ERP, revise el Home";
                            lbl_status.ForeColor = Color.Red;
                            v_email = utiles.obtiene_email_usuario(Convert.ToInt32(Lista_Vendedores.SelectedItem.Value.ToString()), Sserver);
                            utiles.actualiza_historial_nv(v_id_cotizacion, usuario, "Se crea Cotización Web", Sserver, "COT");
                            utiles.enviar_correo("Cotización Web asignada", "Nv Web " + lbl_numero.Text + " fue creada en el ERP con el numero " + lbl_numero_erp.Text + ", esta fue asignada a Ud, revisela en el Home", v_email);
                        }
                        // una vez insertada la NV en el ERP... actualizó la NV para que no aparezca más en el listado de pendientes
                    }
                }
            }
        }

        void actualiza_Cotizacion(int numero)
        {
            String queryString = "";

            queryString = "UPDATE tbl_cotizaciones SET Leido_ERP = 1, status = 1 WHERE Cotizac_Num =  " + numero;
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

        void entrega_num_cot_erp(int v_id_cotizacion)
        {
            string sql = ""; // "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all select ID_usuario, CONCAT(Apellido_Usu,' ', Nombre_Usu) vendedor from tbl_Usuarios where Activo = 1 order by Apellido_Usu";
            sql = " select Cotizac_Num from tbl_Cotizaciones where ID_Cotizacion = " + v_id_cotizacion;

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

        public int busca_info_cliente(int rut, string busco)
        {
            string sql = "select ID_Contacto_cliente from tbl_Contactos_Cliente where id_cliente = (select id_cliente from tbl_clientes where rut =  " + rut + ") and Contacto_Principal = 1";

            if (busco == "C")
            {
                sql = "select ID_Contacto_cliente from tbl_Contactos_Cliente where id_cliente = (select id_cliente from tbl_clientes where rut =  " + rut + ") and Contacto_Principal = 1";
            }
            if (busco == "I")
            {
                sql = "select id_cliente from tbl_clientes where rut = " + rut;
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
    }
}