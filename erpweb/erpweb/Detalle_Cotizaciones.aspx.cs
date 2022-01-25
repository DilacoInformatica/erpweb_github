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
        int v_id_cliente = 0;
        int v_id_contacto = 0;

        double v_neto = 0;
        double v_tax = 0;
        double v_total = 0;
        int num_cotizacion = 0;
        string ubicacion = "";
        string Sserver = "";
        string SMysql = "";
        double total = 0;
        string tipo = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        int usuario = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            num_cotizacion = Convert.ToInt32(Request.QueryString["cot"].ToString());
            ubicacion = Request.QueryString["ubicacion"].ToString();
            tipo = Request.QueryString["tipo"].ToString();

            LnkBtn_Aprobar.Visible = true;
            LnkBtn_Rechazar.Visible = true;

            lbl_neto.Style.Add("text-align", "right");
            lbl_tax.Style.Add("text-align", "right");
            lbl_total.Style.Add("text-align", "right");
            // usuario = Convert.ToInt32(Request.QueryString["usuario"].ToString());
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");

            lbl_numero_erp.Text = "0";

            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_008_03", Sserver) == "NO")
                    {
                        Response.Redirect("ErrorAcceso.html");
                    }
                    lbl_conectado.Text = Session["Usuario"].ToString();
                }

                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "D"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Desarrollo"; }
                else
                { lbl_ambiente.Text = "P"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Producción"; }
            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }


            Btn_crearCot.Attributes["Onclick"] = "return confirm('Ud está a punto de Crear esta Cotización Web en el ERP, desea proceder?')";
            LnkBtn_Aprobar.Attributes["Onclick"] = "return confirm('Ud está a punto de Crear esta Cotización Web en el ERP, desea proceder?')";
            Btn_RechazarCot.Attributes["Onclick"] = "return confirm('Ud está a punto de RECHAZAR esta Cotización Web en el ERP, desea proceder?')";
            LnkBtn_Rechazar.Attributes["Onclick"] = "return confirm('Ud está a punto de RECHAZAR esta Cotización Web en el ERP, desea proceder?')";
            if (!this.IsPostBack)
            {
                carga_vendedores();
                carga_contrl_lista("select id_region, concat(Codigo, ' ', nombre_corto) region from tbl_Regiones where activo = 1", Lst_Region, "tbl_Regiones", "id_region", "region");
                if (VerificaexistenciaCotERP(num_cotizacion) == "SI")
                {
                    lbl_error.Text = "Cotización N° " + num_cotizacion + " ya fue creado en el ERP, consulte con su Administrador";
                    Btn_crearCot.Enabled = false;
                    LnkBtn_Aprobar.Visible = false;
                    Btn_RechazarCot.Enabled = false;
                    LnkBtn_Rechazar.Visible = false;
                    // lbl_error.ForeColor = Color.Red;
                }
                muestra_info_cotizacion(num_cotizacion, ubicacion, tipo);
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
            sql = sql + "SELECT TOP 100 PERCENT dbo.tbl_Usuarios.ID_usuario, CONCAT(dbo.tbl_Usuarios.Apellido_Usu, ' ', dbo.tbl_Usuarios.Nombre_Usu) Vendedor ";
            sql = sql + "FROM dbo.tbl_Areas_Empresa RIGHT OUTER JOIN ";
            sql = sql + "dbo.tbl_Cargo ON ";
            sql = sql + "dbo.tbl_Areas_Empresa.ID_Area = dbo.tbl_Cargo.Id_Area RIGHT OUTER JOIN ";
            sql = sql + "dbo.tbl_Usuarios ON ";
            sql = sql + "dbo.tbl_Cargo.ID_Cargo = dbo.tbl_Usuarios.Id_Cargo and dbo.tbl_Usuarios.Id_Perfil = 6 ";
            sql = sql + "WHERE(dbo.tbl_Areas_Empresa.Puede_Vender = 1)  order by dbo.tbl_Usuarios.Apellido_Usu";

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

        void muestra_info_cotizacion(int num_cotizacion, string ubicacion, string tipo)
        {
            int v_id_cotizacion = 0;
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            string queryString = "lista_cot_web_detalle";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_Cotizac_Num", num_cotizacion);
                    command.Parameters["@v_Cotizac_Num"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@tipo", Convert.ToInt32(tipo));
                    command.Parameters["@tipo"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        lbl_status.Text = "Sin Resultados";
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                // NV
                                v_id_cotizacion = Convert.ToInt32(dr.GetString(0));
                                lbl_id_cot.Text = dr.GetString(0);
                                if (ubicacion != "E")
                                {
                                    lbl_existe.Text = detecta_cliente(Convert.ToInt32(dr.GetString(4)));
                                    //Si cliente existe habilitamos check que permite usar la info ya creada en el ERP
                                    Chk_data_existente.Enabled = true;
                                }
                                else
                                {
                                    lbl_existe.Text = "NO";
                                }
                                lbl_numero_erp.Text =Convert.ToString(detecta_num_cotizac(dr.GetInt32(1)));
                                //v_id_nta_vta = Convert.ToInt32(dr.GetString(0));
                                lbl_numero.Text = dr.GetString(1);
                                lbl_fecha.Text = dr.GetString(12);
                                // Clientes
                                lbl_nombre.Text = dr.GetString(6);
                                lbl_rut.Text = dr.GetString(4) + '-' + dr.GetString(5);
                                lbl_rut_exit.Text = dr.GetString(4);
                                lbl_fono.Text = dr.GetString(10);
                                lbl_email.Text = dr.GetString(11);
                                lbl_direccion.Text = dr.GetString(7);
                                txt_comuna.Text = dr.GetString(8);
                                lbl_ciudad.Text = dr.GetString(9);
                                lbl_region.Text = dr.GetString(13);
                                lbl_empresa.Text = dr.GetString(19);
                                lbl_movil.Text = dr.GetString(20);
                                lbl_nombre.Text = dr.GetString(21);
                                lbl_apellidos.Text = dr.GetString(22);
                                lbl_observaciones.Text = dr.GetString(23);
                                lbl_region.Visible = false;
                                lbl_n_oc.Text = "0";

                                foreach (ListItem item in Lst_Region.Items)
                                {
                                    item.Selected = false;
                                    if (item.Value == lbl_region.Text)
                                    {
                                        item.Selected = true;
                                        break;
                                    }
                                }

                                // Lst_Region.Visible = true;

                                if (lbl_fono.Text == "")
                                {
                                    lbl_fono.Text = "0";
                                }

                                if (lbl_movil.Text == "")
                                {
                                    lbl_movil.Text = "0";
                                }

                                /*  if (lbl_region.Text == "")
                                  {
                                      lbl_region.Visible = false;
                                      Lst_Region.Visible = true;
                                  }*/
                                // Despacho

                                v_neto = Convert.ToDouble(dr.GetString(15));
                                v_tax = Convert.ToDouble(dr.GetString(16));
                                v_total = Convert.ToDouble(dr.GetString(17));


                                //lbl_neto.Text = v_neto.ToString("C3", CultureInfo.CurrentCulture);

                                /*lbl_moneda.Text = "$";
                                lbl_neto.Text = lbl_moneda.Text + ' ' + v_neto.ToString("N2");
                                lbl_tax.Text = lbl_moneda.Text + ' ' + v_tax.ToString("N");
                                lbl_total.Text = lbl_moneda.Text + ' ' + v_total.ToString("N");*/

                                if (ubicacion == "E")
                                {
                                    lbl_error.Text = "Cotización corresponde a Extranjeros... No es posible ingresarlo al ERP ";
                                    lbl_email.ForeColor = Color.Red;
                                    Btn_crearCot.Enabled = false;
                                    LnkBtn_Aprobar.Visible = false;
                                    Btn_RechazarCot.Visible = true;
                                   // LnkBtn_Rechazar.Visible = false;
                                }

                            }
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

            queryString = "select dn.item Item, it.codigo 'Codigo', it.descripcion 'Descripcion' , dn.cantidad 'Cantidad', IFNULL(it.Precio,1) 'Precio Unitario' ";
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

                    if (v_neto != total)
                    {

                        v_neto = total;
                        v_tax = total * 0.19;
                        v_tax = (int)Math.Round(v_tax);
                        v_total = total + (total * 0.19);
                        v_total = (int)Math.Round(v_total);
                    }


                    lbl_neto.Text = v_neto.ToString("C3", CultureInfo.CurrentCulture);

                    lbl_moneda.Text = "$";
                    lbl_neto.Text = lbl_moneda.Text + ' ' + v_neto.ToString("N2");
                    lbl_tax.Text = lbl_moneda.Text + ' ' + v_tax.ToString("N");
                    lbl_total.Text = lbl_moneda.Text + ' ' + v_total.ToString("N");

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public int detecta_num_cotizac(int v_num_cot_web)
        {
            string sql = "select Cotizac_Num from tbl_Cotizaciones where Cotizac_Num_Web = " + v_num_cot_web;
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
                                result = rdr.GetInt32(0);
                              
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
            string swc = "0";
            Page.Validate();

            if (Page.IsValid)
            {
                if (Lst_Region.SelectedItem.Value.ToString() == "0")
                {
                    lbl_status.Text = "Completar Información (Región)";
                    lbl_status.BackColor = Color.Red;
                    swc = "1";
                }
                if (txt_comuna.Text == "")
                {
                    lbl_status.Text = "Completar Información (C0muna)";
                    lbl_status.BackColor = Color.Red;
                    swc = "1";
                }
                if (Lista_Vendedores.SelectedItem.Value.ToString() == "0")
                {
                    lbl_status.Text = "Debe indicar Vendedor o Representante de Ventas";
                    lbl_status.BackColor = Color.Red;
                    swc = "1";
                }

                if (swc == "0")
                {
                    crea_cotizacion();
                }

            }
        }

        void crea_cotizacion()
        {
            int v_id_cotizacion = 0;
            int v_id_item_cotizacion = 0;
            string v_email = "";
            // revisamos la creación del cliente
           // if (lbl_existe.Text == "NO")
          //  {
          if (!Chk_data_existente.Checked)
            {
                inserta_cliente_prospecto_en_ERP(lbl_rut.Text, lbl_empresa.Text, lbl_nombre.Text, lbl_apellidos.Text, lbl_direccion.Text, lbl_ciudad.Text, txt_comuna.Text, Lst_Region.SelectedItem.Value.ToString(), lbl_fono.Text, lbl_movil.Text, lbl_email.Text);
            }
           // }
           // else
           // {
          //      v_id_contacto = busca_info_cliente(Convert.ToInt32(lbl_rut_exit.Text), "C");
           //     v_id_cliente = busca_info_cliente(Convert.ToInt32(lbl_rut_exit.Text), "I");
          //  }
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
                                   // lbl_error.Text = rdr.GetString(0);
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
                                    double v_cantidad = Convert.ToDouble(fila.Cells[3].Text);
                                    double v_precio_unitario = Convert.ToInt32(fila.Cells[4].Text);
                                    //float v_multiplo = obtiene_multiplo_producto(v_id_item);
                                    connection.Open();
                                    SqlCommand cmd = new SqlCommand("web_carga_cot_det_web", connection);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    SqlParameter param = new SqlParameter();
                                    // Parámetros
                                    cmd.Parameters.AddWithValue("@v_id_cot", v_id_cotizacion);
                                    cmd.Parameters["@v_id_cot"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_item", v_id_item);
                                    cmd.Parameters["@v_item"].Direction = ParameterDirection.Input;

                                    cmd.Parameters.AddWithValue("@v_codigo", v_codigo.Trim());
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
                    lbl_status.Text = "Cotización creada correctamente en el ERP, aparecerá en el Home del usuario a quien fue asignada";
                    v_email = utiles.obtiene_email_usuario(Convert.ToInt32(Lista_Vendedores.SelectedItem.Value.ToString()), Sserver);
                    utiles.actualiza_historial_nv(v_id_cotizacion, usuario, "Se crea Cotización Web", Sserver, "COT");
                    utiles.enviar_correo("Cotización Web asignada", "N° Cotización " + lbl_numero.Text + " fue creada en el ERP con el numero " + lbl_numero_erp.Text + ", esta fue asignada a Ud, revisela en el Home", v_email);
                   // utiles.enviar_correo("Cotización Web asignada", "N° Cotización " + lbl_numero.Text + " fue creada en el ERP con el numero " + lbl_numero_erp.Text + ", esta fue asignada a Ud, revisela en el Home", "saranda@dilaco.com");
                    Btn_crearCot.Enabled = false;
                    LnkBtn_Aprobar.Visible = false;
                    LnkBtn_Rechazar.Visible = false;
                    Btn_RechazarCot.Enabled = false;

                }
                // una vez insertada la NV en el ERP... actualizó la NV para que no aparezca más en el listado de pendientes
        }

        public float obtiene_multiplo_producto (int id_item)
        {
            string queryString = "";
            queryString = "Consulta_multiplo_producto ";
            float result = 0;

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id_item", id_item);
                    command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        lbl_status.Text = "Sin Resultados";
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                // NV
                                result = dr.GetFloat(0);
                            }
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                    return result;

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                    return 0;
                }
              
            }
        }
        void actualiza_Cotizacion(int numero)
        {
            string queryString = "";
            queryString = "Actualiza_estado_Documento ";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                string result = "";
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_doc", numero);
                    command.Parameters["@v_doc"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_tipo", "CO");
                    command.Parameters["@v_tipo"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_status", 1);
                    command.Parameters["@v_status"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        lbl_status.Text = "Sin Resultados";
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                // NV
                                result = Convert.ToString(dr.GetString(0));
                            }
                        }
                    }

                    if (result == "")
                    {
                        lbl_error.Text = "ERROR AL ACTUALIZAR COTIZACION EN EL SITIO WEB";
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
                /*String queryString = "";

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
                }*/
            }
        }

        void entrega_num_cot_erp(int v_id_cotizacion)
        {
            string sql = ""; // "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all select ID_usuario, CONCAT(Apellido_Usu,' ', Nombre_Usu) vendedor from tbl_Usuarios where Activo = 1 order by Apellido_Usu";
            sql = " select isnull(Cotizac_Num,0) Cotizac_Num  from tbl_Cotizaciones where ID_Cotizacion = " + v_id_cotizacion;
            lbl_numero_erp.Text = "0";

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

        void entrega_nunmeros_cliente(int rut, string razon, string nombre, string apellido)
        {
            string sql = ""; // "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all select ID_usuario, CONCAT(Apellido_Usu,' ', Nombre_Usu) vendedor from tbl_Usuarios where Activo = 1 order by Apellido_Usu";


            sql = "select tbl_clientes.id_cliente, tbl_Contactos_Cliente.ID_Contacto_cliente from tbl_clientes ";
            sql = sql + "inner join tbl_Contactos_Cliente on tbl_Contactos_Cliente.Id_Cliente = tbl_clientes.ID_Cliente ";
            sql = sql + "where rut = " + rut + " and Razon_Social = '" + razon + "' and nombre = '"+ nombre + "' and Apellido = '"+ apellido + "'";
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
                                v_id_cliente = Convert.ToInt32(rdr.GetInt32(0));
                                v_id_contacto = Convert.ToInt32(rdr.GetInt32(1));
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

        void inserta_cliente_prospecto_en_ERP (string rut_cliente, string empresa, string nombre, string apellido, string direccion, string ciudad, string comuna, string region, string fono, string movil, string email)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_inserta_cliente_prospecto_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_rut", rut_cliente);
                    cmd.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_empresa", empresa);
                    cmd.Parameters["@v_empresa"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_nomnbre", nombre);
                    cmd.Parameters["@v_nomnbre"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_apellido", apellido);
                    cmd.Parameters["@v_apellido"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_direccion", direccion); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_direccion"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_comuna", comuna); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_comuna"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_ciudad", ciudad); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_ciudad"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_region", region); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_id_region"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_telefonos", fono); // Pendiente creacion Cliente
                    cmd.Parameters["@v_telefonos"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_movil", movil); // Pendiente creacion Cliente
                    cmd.Parameters["@v_movil"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_email", email); // Pendiente creacion contacto Cliente
                    cmd.Parameters["@v_email"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                               // lbl_error.Text = rdr.GetString(0);
                                v_id_cliente = Convert.ToInt32(rdr.GetInt32(0));
                                v_id_contacto= Convert.ToInt32(rdr.GetInt32(1));
                                Actualiza_cliente_web(lbl_rut_exit.Text, v_id_cliente, v_id_contacto);
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

        void Actualiza_cliente_web (string rut, int id_cliente, int id_contacto)
        {

            // Actualizamos los valores

            
               // v_rut int, v_id_cliente int, v_id_contacto int, v_empresa varchar(100), v_nombre nvarchar(100), v_apellido nvarchar(100), v_fono nvarchar(100), v_movil nvarchar(100), v_email nvarchar(100)

             string queryString = "";
             queryString = "Actualiza_cliente_web ";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                string result = "";
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_rut", rut);
                    command.Parameters["@v_rut"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_id_cliente", id_cliente);
                    command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_id_contacto", id_contacto);
                    command.Parameters["@v_id_contacto"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_empresa", lbl_empresa.Text);
                    command.Parameters["@v_empresa"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_nombre", lbl_nombre.Text.ToUpper());
                    command.Parameters["@v_nombre"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_apellido", lbl_apellidos.Text.ToUpper());
                    command.Parameters["@v_apellido"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_fono", lbl_fono.Text);
                    command.Parameters["@v_fono"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_movil", lbl_movil.Text);
                    command.Parameters["@v_movil"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_email", lbl_email.Text);
                    command.Parameters["@v_email"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_cotizacion", Convert.ToInt32(lbl_numero.Text));
                    command.Parameters["@v_cotizacion"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        lbl_status.Text = "Sin Resultados";
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                // NV
                                result = Convert.ToString(dr.GetString(0));
                            }
                        }
                    }

                   if (result != "OK")
                    {
                        lbl_error.Text = "ERROR AL ACTUALIZAR CLIENTE EN EL SITIO WEB " + result;
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

            /* string query = "";

             query = "update tbl_clientes set id_cliente = " + id_cliente  + ", leido_erp = 1, cliente_erp = 1";
             query = query + " where rut = " + rut;
             query = query + " and leido_erp = 0 ";
             query = query + " and upper(empresa) = '" + empresa + "'";
             query = query + " and upper(nombre) = '" + nombre + "'";
             query = query + " and upper(apellido) = '" + apellido + "'";

             using (MySqlConnection conn = new MySqlConnection(SMysql))
             {
                 try
                 {
                     conn.Open();

                     MySqlCommand command = new MySqlCommand(query, conn);
                     command.ExecuteNonQuery();
                     conn.Close();
                     conn.Dispose();
                     lbl_id_cliente.Text ="Id Creado para Cliente Potencial " + Convert.ToString(id_cliente);
                     lbl_id_cliente.Visible = true;
                 }
                 catch (Exception ex)
                 {
                     lbl_error.Text = ex.Message + query;
                     conn.Close();
                     conn.Dispose();
                 }
             }*/
        }



        protected void lista_detalles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Assumes the Price column is at index 4
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                total = total + Convert.ToDouble(e.Row.Cells[4].Text);
            }
                
        }

        protected void Chk_Cli_Particular_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Chk_Cli_Particular.Checked == true)
            {
                lbl_respaldo.Text = lbl_empresa.Text;
                lbl_empresa.Text = "";
            } // if
            else
            {
                lbl_empresa.Text = lbl_respaldo.Text;
            } // else
        }

        protected void Chk_data_existente_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Chk_data_existente.Checked == true)
            {
                // reemplazamos la información que viene por la que ya existe en el ERP...  la información que llegara se ingresará como Contacto del Cliente existe
                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("web_muestra_info_cliente_existente", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter param = new SqlParameter();
                        // Parámetros
                        cmd.Parameters.AddWithValue("@v_rut",lbl_rut.Text);
                        cmd.Parameters["@v_rut"].Direction = ParameterDirection.Input;

  
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (!rdr.IsDBNull(0))
                                {

                                   //lbl_error.Text = rdr.GetInt32(0).ToString();
                                    v_id_cliente = Convert.ToInt32(rdr.GetInt32(1));
                                    v_id_contacto = Convert.ToInt32(rdr.GetInt32(2));
                                    lbl_empresa.Text = rdr.GetString(3);
                                    lbl_ciudad.Text = rdr.GetString(6);
                                    txt_comuna.Text = rdr.GetString(7);
                                    lbl_direccion.Text = rdr.GetString(8);
                                    lbl_region.Text = Convert.ToString(rdr.GetInt32(9));
                                    lbl_email.Text = rdr.GetString(10);
                                    lbl_nombre.Text = rdr.GetString(11);
                                    lbl_apellidos.Text = rdr.GetString(12);

                                    foreach (ListItem item in Lst_Region.Items)
                                    {
                                        item.Selected = false;
                                        if (item.Value == lbl_region.Text)
                                        {
                                            item.Selected = true;
                                            break;
                                        }
                                    }




                                    // Actualiza_cliente_web(lbl_rut_exit.Text, v_id_cliente, v_id_contacto);
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
            else
            {
                Lst_Region.Items.Clear();
                carga_contrl_lista("select id_region, concat(Codigo, ' ', nombre_corto) region from tbl_Regiones where activo = 1", Lst_Region, "tbl_Regiones", "id_region", "region");
                muestra_info_cotizacion(num_cotizacion,ubicacion,tipo);
            }
        }

        protected void Btn_RechazarCot_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                string queryString = "";
                queryString = "Rechaza_Documento ";


                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    string result = "";
                    try
                    {
                        conn.Open();
                        MySqlCommand command = new MySqlCommand(queryString, conn);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@v_doc", lbl_numero.Text);
                        command.Parameters["@v_doc"].Direction = ParameterDirection.Input;

                        command.Parameters.AddWithValue("@v_tipo", "CO");
                        command.Parameters["@v_tipo"].Direction = ParameterDirection.Input;

                        DataSet ds = new DataSet();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                        MySqlDataReader dr = command.ExecuteReader();

                        if (!dr.HasRows)
                        {
                            lbl_status.Text = "Sin Resultados";
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                if (!dr.IsDBNull(0))
                                {
                                    // NV
                                    result = Convert.ToString(dr.GetString(0));
                                }
                            }
                        }

                        if (result != "")
                        {
                            lbl_error.Text = "ERROR AL RECHAZAR COTIZACION ";
                        }
                        else
                        {
                            lbl_status.Text = "Cotización N° " + lbl_numero.Text + " fue Rechazada exitosamente";
                            Btn_crearCot.Enabled = false;
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
        }

        protected void Btn_volver_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Cotizaciones.aspx");
        }

        protected void LnkBtn_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cotizaciones.aspx");
        }

        protected void LnkBtn_Rechazar_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                string queryString = "";
                queryString = "Rechaza_Documento ";


                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    string result = "";
                    try
                    {
                        conn.Open();
                        MySqlCommand command = new MySqlCommand(queryString, conn);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@v_doc", lbl_numero.Text);
                        command.Parameters["@v_doc"].Direction = ParameterDirection.Input;

                        command.Parameters.AddWithValue("@v_tipo", "CO");
                        command.Parameters["@v_tipo"].Direction = ParameterDirection.Input;

                        DataSet ds = new DataSet();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                        MySqlDataReader dr = command.ExecuteReader();

                        if (!dr.HasRows)
                        {
                            lbl_status.Text = "Sin Resultados";
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                if (!dr.IsDBNull(0))
                                {
                                    // NV
                                    result = Convert.ToString(dr.GetString(0));
                                }
                            }
                        }

                        if (result != "OK")
                        {
                            lbl_error.Text = "ERROR AL RECHAZAR COTIZACION ";
                        }
                        else
                        {
                            lbl_status.Text = "Cotización N° " + lbl_numero.Text + " fue Rechazada exitosamente";
                            Btn_crearCot.Enabled = false;
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
        }

        protected void LnkBtn_Aprobar_Click(object sender, EventArgs e)
        {
            string swc = "0";
            Page.Validate();

            if (Page.IsValid)
            {
                if (Lst_Region.SelectedItem.Value.ToString() == "0")
                {
                    lbl_status.Text = "Completar Información (Región)";
                    lbl_status.BackColor = Color.Red;
                    swc = "1";
                }
                if (txt_comuna.Text == "")
                {
                    lbl_status.Text = "Completar Información (C0muna)";
                    lbl_status.BackColor = Color.Red;
                    swc = "1";
                }
                if (Lista_Vendedores.SelectedItem.Value.ToString() == "0")
                {
                    lbl_status.Text = "Debe indicar Vendedor o Representante de Ventas";
                    lbl_status.BackColor = Color.Red;
                    swc = "1";
                }

                if (swc == "0")
                {
                    crea_cotizacion();
                }

            }
        }
    }
}