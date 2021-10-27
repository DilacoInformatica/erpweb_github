﻿using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;
using Microsoft.VisualBasic;
namespace erpweb
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        string usuario = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            //Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
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

                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");


            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }
            //Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
            if (!this.IsPostBack)
            {
                carga_contrl_lista("SELECT id_tipo, Estado FROM tbl_status_cot WHERE activo = 1", LstEstados, "tbl_status_cot", "id_tipo", "Estado");
                carga_cotizaciones(0);
            }
        }
        void carga_contrl_lista(string sql, DropDownList lista, string tabla, string llave, string Campo)
        {
            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                connection.Open();
                //SqlCommand command = new SqlCommand(sql, connection);
                MySqlDataAdapter reader = new MySqlDataAdapter(sql, connection);
                DataSet dr = new DataSet();
                reader.Fill(dr, tabla);
                lista.DataSource = dr;
                lista.DataValueField = llave;
                lista.DataTextField = Campo;
                lista.DataBind();

                connection.Close();
                connection.Dispose();
            }
        }

        void carga_cotizaciones(int id_estado)
        {
            string queryString = "";
            lbl_mensaje.Text = "";
            queryString = "lista_cot_web ";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    if (txt_cotizacion.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_Cotizac_Num", txt_cotizacion.Text);
                        command.Parameters["@v_Cotizac_Num"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_Cotizac_Num", DBNull.Value);
                        command.Parameters["@v_Cotizac_Num"].Direction = ParameterDirection.Input;
                    }
                    if (txt_rut.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_rut", txt_rut.Text);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_rut", DBNull.Value);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }

                    if (id_estado != 0)
                    {
                        command.Parameters.AddWithValue("@v_estado", id_estado );
                        command.Parameters["@v_estado"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_estado", 0);
                        command.Parameters["@v_estado"].Direction = ParameterDirection.Input;
                    }

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        lbl_mensaje.Text = "Sin Cotizaciones por Ingresar";
                        Lista_cotizacion.DataSource = null;
                        Lista_cotizacion.DataBind();
                    }
                    else
                    {
                        Lista_cotizacion.DataSource = dr;
                        Lista_cotizacion.DataBind();

                        lbl_cantidad.Text = "Cantidad de Registros: " + Convert.ToString(Lista_cotizacion.Rows.Count);
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

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            lbl_error.Text = "";
            if (LstEstados.SelectedValue == "Seleccione")
            {
                carga_cotizaciones(0);
            }
            else
            {
                carga_cotizaciones(Convert.ToInt32(LstEstados.SelectedValue));
            }
            
        }

        protected void Lista_cotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Lista_cotizacion.SelectedRow;
            string ubicacion = "";

            if (row.Cells[10].Text.ToUpper().Trim() == "CHILE")
            {
                ubicacion = "N";
            }
            else
            {
                ubicacion = "E";

            }
            Response.Redirect("Detalle_Cotizaciones.aspx?cot=" + row.Cells[7].Text + "&ubicacion=" + ubicacion + "&tipo=" + row.Cells[13].Text);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void Lista_cotizacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_num_cot_erp = e.Row.FindControl("lbl_num_cot_erp") as Label;
               // DataRowView drv = (DataRowView)e.Row.DataItem;

                System.Web.UI.WebControls.Image img_estado = e.Row.FindControl("img_estado") as System.Web.UI.WebControls.Image;

                string valor = e.Row.Cells[6].Text;

                string result = busca_estado_cot(Convert.ToInt32(valor) );

                if (result.Substring(0, 8) == "Ingresad")
                {
                    img_estado.ImageUrl = "~/img/nuevo.png";
                    img_estado.ToolTip = result;
                }

                if (result.Substring(0, 8) == "Asignada")
                {
                    img_estado.ImageUrl = "~/img/asignado.png";
                    img_estado.ToolTip = result;
                }

                if  (result.Substring(0, 8) == "Aceptada")
                {
                    img_estado.ImageUrl = "~/img/Apruebo.png";
                    img_estado.ToolTip = result;
                }

                if (result.Substring(0, 8) == "Rechaza ")
                {
                    img_estado.ImageUrl = "~/img/Rechazo.png";
                    img_estado.ToolTip = result;
                }

                lbl_num_cot_erp.Text = busca_numero_doc_erp(Convert.ToInt32(e.Row.Cells[6].Text), "CO");


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
               // e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
               // e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        public string busca_estado_cot(int cotizacion)
        {
            string queryString = "";
            lbl_mensaje.Text = "";
            queryString = "busca_estado_cot ";
            string result = "";

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_Cotizac_Num", cotizacion);
                    command.Parameters["@v_Cotizac_Num"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
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
                
                    //Productos.DataMember = "tbl_items";

                    conn.Close();
                    conn.Dispose();

                    return result ;

                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    conn.Close();
                    conn.Dispose();
                    return result;
                }
            }
        }

        public string busca_numero_doc_erp(int numero, string tipo)
        {
            string v_result = "";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_documento_erp", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_numero", numero);
                    cmd.Parameters["@v_numero"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_tipo", tipo);
                    cmd.Parameters["@v_tipo"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                v_result = Convert.ToString(rdr.GetInt32(0));
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return v_result;
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message.ToString();
                    return "0";
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected void Lista_cotizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Lista_cotizacion.Rows[index];

                string ubicacion = "";

                if (row.Cells[8].Text.ToUpper().Trim() == "CHILE")
                {
                    ubicacion = "N";
                }
                else
                {
                    ubicacion = "E";

                }
                Response.Redirect("Detalle_Cotizaciones.aspx?cot=" + row.Cells[6].Text + "&ubicacion=" + ubicacion + "&tipo=" + row.Cells[12].Text);
            }
        }
    }
}