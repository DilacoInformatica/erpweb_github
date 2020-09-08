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
namespace erpweb
{
    public partial class Notas_Venta : System.Web.UI.Page
    {        
        string Sserver = @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
       // String Sserver = @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
       // string SMysql = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Local
        string SMysql = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;"; // Conexion Server Local
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Btn_buscar.Attributes["Onclick"] = "return valida()";
                // Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
                carga_nv();
            }
        }

         void carga_nv()
        {
            String queryString = "";
            lbl_mensaje.Text = "";
            queryString = "lista_nv_web ";
           

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    if (txt_nv.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_Nota_vta", txt_nv.Text);
                        command.Parameters["@v_Nota_vta"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_Nota_vta", null);
                        command.Parameters["@v_Nota_vta"].Direction = ParameterDirection.Input;
                    }
                    if (txt_rut.Text != "")
                    {
                        command.Parameters.AddWithValue("@v_rut", txt_rut.Text);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@v_rut", null);
                        command.Parameters["@v_rut"].Direction = ParameterDirection.Input;
                    }

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();
 
                    if (!dr.HasRows)
                    {
                        lbl_mensaje.Text = "Sin Resultados";
                    }
                    else
                    {
                        Lista_notas.DataSource = dr;
                        Lista_notas.DataBind();
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
            Page.Validate();
            if (Page.IsValid)
            {
                carga_nv();
            }
        }

        protected void Lista_notas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Lista_notas.SelectedRow;
            Response.Redirect("Detalle_NV.aspx?nv=" + row.Cells[1].Text);
        }
    }
}