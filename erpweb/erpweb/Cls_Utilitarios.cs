using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;

namespace erpweb
{
    public class Cls_Utilitarios
    {
        int ambiente =2; // Indica el ambiente dónde debe conectarse el sistema

        string correo_envia = "informatica@dilaco.com";
        string correo_recibe = "sebastian.aranda.o@gmail.com";

        public string selecciona_todos(CheckBox cabecera, string ejecutor, GridView grilla, string buscador)
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
            return "OK";
        }

        public string verifica_ambiente(string servidor)
        {
            string salida = "";
            if (servidor == "SSERVER" && ambiente == 1)
            {
                salida = @"RABhAHQAYQAgAFMAbwB1AHIAYwBlAD0AUABDAF8AUwBBAFIAQQBOAEQAQQA7AEkAbgBpAHQAaQBhAGwAIABDAGEAdABhAGwAbwBnAD0AZABpAGwAYQBjAG8AOwB1AGkAZAA9AHMAYQA7ACAAcAB3AGQAPQAgAGQAfABsAEAAYwAwADsASQBuAHQAZQBnAHIAYQB0AGUAZAAgAFMAZQBjAHUAcgBpAHQAeQA9AGYAYQBsAHMAZQA=";
            }
            if (servidor == "MYSQL" && ambiente == 1)
            {
                salida = @"UwBlAHIAdgBlAHIAPQBsAG8AYwBhAGwAaABvAHMAdAA7AGQAYQB0AGEAYgBhAHMAZQA9AGQAaQBsAGEAYwBvAGMAbABfAGQAaQBsAGEAYwBvAHcAZQBiADsAdQBpAGQAPQByAG8AbwB0ADsAcAB3AGQAPQBkAHwAbABAAGMAMAA7AEMASABBAFIAUwBFAFQAPQB1AHQAZgA4ADsA";
            }
            if (servidor == "SSERVER" && ambiente == 2)
            {
                salida = @"RABhAHQAYQAgAFMAbwB1AHIAYwBlAD0AMQA3ADIALgAxADYALgAxADAALgAxADMAXABEAEkATABBAEMATwA7AEkAbgBpAHQAaQBhAGwAIABDAGEAdABhAGwAbwBnAD0AZABpAGwAYQBjAG8AOwB1AGkAZAA9AHMAYQA7ACAAcAB3AGQAPQAgAGQAfABsAEAAYwAwADIAMAAxADYAOwBJAG4AdABlAGcAcgBhAHQAZQBkACAAUwBlAGMAdQByAGkAdAB5AD0AZgBhAGwAcwBlAA==";
            }
           
            if (servidor == "MYSQL" && ambiente == 2)
            {
                salida = @"cwBlAHIAdgBlAHIAPQBkAGUAdgAuAGQAaQBsAGEAYwBvAC4AYwBvAG0AOwBkAGEAdABhAGIAYQBzAGUAPQBkAGkAbABhAGMAbwBjAGwAXwBkAGkAbABhAGMAbwB3AGUAYgA7AHUAaQBkAD0AZABpAGwAYQBjAG8AYwBsAF8AZABpAGwAYQBjAG8AOwBwAHcAZAA9AGQAfABsAEAAYwAwADIAMAAxADkAOwA=";
            }

            if (servidor == "MYSQL2" && ambiente == 2)
            {
                salida = @"cwBlAHIAdgBlAHIAPQBkAGUAdgAuAGQAaQBsAGEAYwBvAC4AYwBvAG0AOwBkAGEAdABhAGIAYQBzAGUAPQBkAGkAbABhAGMAbwBjAGwAXwBlAGMAbwBtAG0AZQByAGMAZQA7AHUAaQBkAD0AZABpAGwAYQBjAG8AYwBsAF8AZABpAGwAYQBjAG8AOwBwAHcAZAA9AGQAfABsAEAAYwAwADIAMAAxADkAOwA="; // Conexion Server
            }

            return salida;

        }

        public string retorna_ambiente ()
        {
            if(ambiente == 1 ) // Local
            {
                return "D";
            }
            else // Servidor
            {
                return "P";
            }
        }

        public string busca_numero_doc_erp(int numero, string tipo, string Servidor)
        {
            string v_result = "";
            using (SqlConnection connection = new SqlConnection(Servidor))
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
                    return "0";
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public string retorna_ruta()
        {
            if (ambiente == 1) // Local
            {
                return @"C:\intranet\documentos\Biblioteca";
            }
            else // Servidor
            {
                return @"E:\intranet\documentos\Biblioteca";
            }
        }

        public string actualiza_historial_nv(int valor, int usuario, string comentario,string servidor, string codigo )
        {
            string sql = "";
            sql = "INSERT INTO tbl_Seguimiento (COD_DOC, Fecha_Seg, Id_Tipo_Accion, Fecha_Vencimiento, Id_Usuario_Resp, ";
            sql = sql + "Observaciones_Seg,Id_Documento, Creado, Usr_Id, Tarea_Hecha,Documento_Adjunto) ";
            sql = sql + "VALUES ('"+ codigo +  "',getdate(),";
            sql = sql + "5,null,null,";
            sql = sql + "'"+ comentario + "',";
            sql = sql + valor + ",getdate()," + usuario + ",null,null)";

            using (SqlConnection connection = new SqlConnection(servidor))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return "OK";

                }
                catch (Exception ex)
                {
                    return "ERROR " + ex.Message;
                }
            }
        }



        public string enviar_correo(string cabecera, string cuerpo, string receptor)
        {

            string htmlmensaje = "";
            SmtpClient SMTPClientService = new System.Net.Mail.SmtpClient();

            htmlmensaje =


            htmlmensaje =  "<table width='650' align='enter' border=1 cellspacing=0 cellpadding=7 bordercolor='#C0D2E4'>";
            htmlmensaje = htmlmensaje + "  <tr>";
            htmlmensaje = htmlmensaje + "    <td align='right' style='background-color:#C0D2E4'><span style='font-family:Arial, Helvetica, sans-serif; color:#4C8AEF; font-size:25px; font-weight:bold; font-style:italic'>DILACO</span><span style='font-family:Arial, Helvetica, sans-serif; color:#445159; font-size:25px; font-weight:bold; font-style:italic'>&nbsp;informa</span><br />";
            htmlmensaje = htmlmensaje + "      <span style='font-family:Arial, Helvetica, sans-serif; color:#586974; font-size:13px; font-style:italic'>Sitio Web</span>";
            htmlmensaje = htmlmensaje + "    </td>";
            htmlmensaje = htmlmensaje + "  </tr>";
            htmlmensaje = htmlmensaje + "  <tr>";
            htmlmensaje = htmlmensaje + "    <td height=300 valign='top' style='background-color:#F8FAFC'><span style='font-family:'Trebuchet MS', Arial, Helvetica, sans-serif; color:#000; font-size:13px'>{BODY}</span></td>";
            htmlmensaje = htmlmensaje + "  </tr>";
            htmlmensaje = htmlmensaje + "  <tr>";
            htmlmensaje = htmlmensaje + "    <td height=20 valign='top' style='font-size:11px;font-family:'Trebuchet MS', Arial, Helvetica, sans-serif;border-top-color:#F8FAFC;border-top-width:0px;border-top-style:none'>Nota: Este email es enviado automaticamente. No responda a su remitente ya que no será leído.</td>";
            htmlmensaje = htmlmensaje + "  </tr>";
            htmlmensaje = htmlmensaje + "</table>";


            SMTPClientService.Host = "mail.dilaco.com";
            SMTPClientService.Port = 25;
            SMTPClientService.EnableSsl = false;
            SMTPClientService.Credentials = new System.Net.NetworkCredential(correo_envia, "informatica#2021");

            System.Net.Mail.MailMessage EmailMsgObj = new System.Net.Mail.MailMessage();
            EmailMsgObj.IsBodyHtml = true;
            EmailMsgObj.To.Add(receptor);
           // EmailMsgObj.To.Add("saranda@dilaco.com");
          //  EmailMsgObj.To.Add(receptor);
            EmailMsgObj.From = new System.Net.Mail.MailAddress(correo_envia);

           // EmailMsgObj.ReplyToList.Add("saranda@dilaco.com");

            EmailMsgObj.Subject = cabecera;

            //EmailMsgObj.Body = cuerpo.ToString();
            EmailMsgObj.Body = htmlmensaje.Replace("{BODY}", cuerpo).ToString();
            EmailMsgObj.IsBodyHtml = true;

            try
            {
                SMTPClientService.Send(EmailMsgObj);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                SMTPClientService.Dispose();
            }
        }

        public string obtiene_email_usuario(int v_id_usuario, string conexion)
        {
            string sql = "";
            string email = "";
            sql = "select email from tbl_Usuarios where ID_usuario = " + v_id_usuario;

            using (SqlConnection connection = new SqlConnection(conexion))
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
                                email = rdr.GetString(0);
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                    return email;
                }
                catch (Exception ex)
                {

                    connection.Close();
                    connection.Dispose();
                    return ex.Message.ToString();
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }

            }
        }

        public string obtiene_acceso_pagina(string usuario, string opcion, string conexion)
        {
            string sql = "";
            string salida = "";

            sql = "select[dbo].[acceso_web]('" + usuario + "', '" + opcion + "')";

            using (SqlConnection connection = new SqlConnection(conexion))
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
                                salida = rdr.GetString(0);
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                    return salida;
                }
                catch (Exception ex)
                {

                    connection.Close();
                    connection.Dispose();
                    return ex.Message.ToString();
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }

            }
        }

        public string obtiene_nombre_usuario(int v_id_usuario, string conexion)
        {
            string sql = "";
            string email = "";
            sql = "select Iniciales from tbl_Usuarios where ID_usuario = " + v_id_usuario;

            using (SqlConnection connection = new SqlConnection(conexion))
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
                                email = rdr.GetString(0);
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                    return email;
                }
                catch (Exception ex)
                {

                    connection.Close();
                    connection.Dispose();
                    return ex.Message.ToString();
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }

            }
        }

        public string rectfica_stock(string conexion, string conexionMysql)
        {
            // Esta función revisará el correcto manejo del Stock de los productos
            int id_item = 0;
            double stock = 0;
            string salida = "";
            double stock_critico = 0;

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_stock_erp_lista", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@id_producto", DBNull.Value);
                    cmd.Parameters["@id_producto"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                id_item = rdr.GetInt32(0);
                                stock = rdr.GetDouble(1);
                                // Con la información Obtenida... actualizamos el producto en la web
                                stock_critico = valida_stock_critico(id_item, conexion);
                                salida = actualiza_stock_web(id_item, stock, stock_critico, conexionMysql);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return "Proceso Finalizado";

                }
                catch (Exception ex)
                {
                    return "Error Procedimiento rectifica_stcok: " + ex.Message;

                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public int crea_solicitud(string conexion)
        {
            // Esta función revisará el correcto manejo del Stock de los productos
            int id_solicitud = 0;
           

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_stock_erp_lista", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                id_solicitud= rdr.GetInt32(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return id_solicitud;

                }
                catch (Exception ex)
                {
                    return 0;

                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int crea_solicitud_detalle(string conexion, int id_solicitud, int id_item)
        {
            // Esta función revisará el correcto manejo del Stock de los productos
            int id_detalle_solicitud = 0;


            using (SqlConnection connection = new SqlConnection(conexion))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_stock_erp_lista", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                id_detalle_solicitud = rdr.GetInt32(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return id_detalle_solicitud;

                }
                catch (Exception ex)
                {
                    return 0;

                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public double valida_stock_critico(int id_item, string conexion)
        {
            double resultado = 0;

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_stock_critico", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@id_producto", id_item);
                    cmd.Parameters["@id_producto"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@salida", "SC");
                    cmd.Parameters["@salida"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                resultado = rdr.GetDouble(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return resultado;

                }
                catch (Exception ex)
                {
                   // lbl_error.Text = "Error procedimiento web_consulta_stock_critico " + ex.Message;
                    return 0;
                }
                finally
                {
                    connection.Close();

                }
            }
        }

        public string actualiza_stock_web(int id_item, double stock, double stock_critico, string conexion)
        {
            string result = "";
            String queryString = "crud_stock";
            //lbl_status.Text = "";
            using (MySqlConnection conn = new MySqlConnection(conexion))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id_item", id_item);
                    command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_stock", stock);
                    command.Parameters["@v_stock"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_stock_critico", stock_critico);
                    command.Parameters["@v_stock_critico"].Direction = ParameterDirection.Input;

                    //var result = command.ExecuteNonQuery();
                    using (MySqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                result = rdr.GetString(0);
                            }
                        }
                    }

                    conn.Close();
                    conn.Dispose();

                    return result;

                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    return ex.Message;
                }
            }
        }



        public string obtengo_valor_regla(string valor, string conexion)
        {
            string result = "";

 
                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("web_consulta_parametros_web", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter param = new SqlParameter();
                        // Parámetros
                        cmd.Parameters.AddWithValue("@v_valor", valor);
                        cmd.Parameters["@v_valor"].Direction = ParameterDirection.Input;

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (!rdr.IsDBNull(0))
                                {
                                    //rescatamos los valores segun lo que utilizaremos
                                    valor = Convert.ToString(rdr.GetString(0));
                                }
                            }
                        }

                        connection.Close();
                        connection.Dispose();

                      return valor;

                    }
                    catch (Exception ex)
                    {
                        return "Error " + ex.Message;
                    }
                    finally
                    {
                        connection.Close();
                        
                    }
                }
        }

    }
}