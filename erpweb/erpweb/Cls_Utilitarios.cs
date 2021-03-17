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
        int ambiente =1; // Indica el ambiente dónde debe conectarse el sistema

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
                //salida = @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
                salida = @"Data Source=LAPTOP-K0J0KFD2;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
                //salida = @"Data Source=PC_SARANDA;Initial Catalog=dilaco;uid=sa; pwd= d|l@c0;Integrated Security=false"; // Conexion Local
            }
            if (servidor == "MYSQL" && ambiente == 1)
            {
                salida = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;CHARSET=utf8;"; // Conexion  Local
                //salida = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
            }
            if (servidor == "SSERVER" && ambiente == 2)
            {
                salida =   @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
                //salida = @"Data Source=PC_SARANDA;Initial Catalog=dilaco;uid=sa; pwd= d|l@c0;Integrated Security=false"; // Conexion Local
            }
           
            if (servidor == "MYSQL" && ambiente == 2)
            {
                salida = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
            }

            if (servidor == "MYSQL2" && ambiente == 2)
            {
                salida = @"server=dev.dilaco.com;database=dilacocl_ecommerce;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
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
                    return "ERROR";
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
            EmailMsgObj.To.Add(receptor);
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


    }
}