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
    public partial class Precios_Esp_Adm : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("SSERVER"));
            SMysql = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("MYSQL"));
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            // ocultamos el botón de agregar nuevos codigos para que sea el ERP el encargado de subir ítems
            Btn_Nuevo.Visible = false;
            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_008_04", Sserver) == "NO")
                    {
                        Response.Redirect("ErrorAcceso.html");
                    }
                    lbl_conectado.Text = Session["Usuario"].ToString();
                }

                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "Ambiente Desarrollo"; }
                else
                { lbl_ambiente.Text = "Ambiente Producción"; }
                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "Ambiente Desarrollo"; }
                else
                { lbl_ambiente.Text = "Ambiente Producción"; }

                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");


            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }

            if (!this.IsPostBack)
            {
                Btn_Eliminar.Attributes["Onclick"] = "return confirm('Desea eliminar infromación de Precios Especiales para cliente?')";
                carga_clientes();
            }
        }

        void carga_clientes()
        {
            String queryString = "";
            lbl_mensaje.Text = "";
            queryString = "select cl.id_cliente, cl.rut + '-' + cl.Dv_Rut Rut, SUBSTRING(cl.razon_social,1,30) razon_social , pe.id_item ID_Item,  pe.codigo, SUBSTRING(pe.descripcion,1,30) descripcion, mn.Sigla Moneda, pe.precio_lista , pe.precio, ";
            queryString = queryString + "IFNULL(pe.fecha_vigencia,IFNULL(pe.fecha_vigencia,DATE_SUB(NOW(),INTERVAL 24 HOUR))) fecha_vigencia, ";
            queryString = queryString + "IF(DATEDIFF(IFNULL(date(pe.fecha_vigencia),date(DATE_SUB(NOW(),INTERVAL 24 HOUR))),date(NOW())) > 0, 'S','N') Vigente ";
            queryString = queryString + "from tbl_clientes cl, tbl_precios_especiales pe, tbl_Monedas mn ";
            queryString = queryString + "where cl.ID_Cliente = pe.ID_Cliente ";
            queryString = queryString + "and pe.id_moneda = mn.ID_Moneda ";

            if (txt_idw.Text != "")
            {
                queryString = queryString + "and cl.id_cliente = " + txt_idw.Text;
            }
            if (txt_rutw.Text != "")
            {
                queryString = queryString + "and cl.rut = " + txt_rutw.Text;
            }
            if (txt_razonw.Text != "")
            {
                queryString = queryString + "and cl.Razon_Social like '%" + txt_razonw.Text + "%'";
            }

            queryString = queryString + " order by cl.ID_Cliente ";

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

        protected void Btn_Nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Precios_Esp_Pub.aspx");
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            lbl_status.Text = "";
            carga_clientes();
        }

        protected void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            string query;
            Page.Validate();
            if (Page.IsValid)
            {

                foreach (GridViewRow row in Grilla.Rows)
                {
                    CheckBox check = row.FindControl("Chk_selecciona") as CheckBox;

                    if (check.Checked)
                    {
                        using (MySqlConnection conn = new MySqlConnection(SMysql))
                        {
                            try
                            {
                                conn.Open();
                                query = "Del_PreciosEspeciales";
                                MySqlCommand command = new MySqlCommand(query, conn);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@v_id_item", row.Cells[4].Text);
                                command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_id_cliente", row.Cells[1].Text);
                                command.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;

                                command.Parameters.AddWithValue("@v_comando", "E");
                                command.Parameters["@v_comando"].Direction = ParameterDirection.Input;

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

                                // Eliminamosel item del ERP
                                elimina_item_pe_erp(Convert.ToInt32(row.Cells[4].Text), Convert.ToInt32(row.Cells[1].Text));
                                lbl_status.Text = "Producto(s) eliminado(s) correctamente de la Web y desde el ERP";
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
                carga_clientes();
            }
        }

        public string elimina_item_pe_erp(int id_item, int id_cliente)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_elimima_item_pe_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_item", id_item);
                    cmd.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_cliente", id_cliente);
                    cmd.Parameters["@v_id_cliente"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                result = rdr.GetString(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    return result;
                }
                finally
                {
                    connection.Close();
                }

            }
        }


        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }
    }
}