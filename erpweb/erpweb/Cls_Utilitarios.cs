using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace erpweb
{
    public class Cls_Utilitarios
    {
        int ambiente = 1; // Indica el ambiente dónde debe conectarse el sistema

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
                salida = @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
                //salida = @"Data Source=PC_SARANDA;Initial Catalog=dilaco;uid=sa; pwd= d|l@c0;Integrated Security=false"; // Conexion Local
            }
            if (servidor == "SSERVER" && ambiente == 2)
            {
                //salida =   @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
                salida = @"Data Source=PC_SARANDA;Initial Catalog=dilaco;uid=sa; pwd= d|l@c0;Integrated Security=false"; // Conexion Local
            }
            if (servidor == "MYSQL" && ambiente == 1)
            {
                salida = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;CHARSET=utf8;"; // Conexion  Local
               // salida = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
            }
            if (servidor == "MYSQL" && ambiente == 2)
            {
                salida =  @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
            }

            return salida;

        }

        public string enviar_correo(string cabecera, string cuerpo)
        {


            SmtpClient SMTPClientService = new System.Net.Mail.SmtpClient();


            SMTPClientService.Host = "mail.dilaco.com";
            SMTPClientService.Port = Convert.ToInt32("110");
            SMTPClientService.Credentials = new System.Net.NetworkCredential(correo_envia,"informatica#2019");

            System.Net.Mail.MailMessage EmailMsgObj = new System.Net.Mail.MailMessage();
            EmailMsgObj.IsBodyHtml = true;
            EmailMsgObj.To.Add(correo_recibe);
            EmailMsgObj.From = new System.Net.Mail.MailAddress(correo_envia);

           EmailMsgObj.ReplyToList.Add("saranda@dilaco.com");


            EmailMsgObj.Subject = cabecera;

            EmailMsgObj.Body = cuerpo.ToString();

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

            //MailMessage mail = new System.Net.Mail.MailMessage();
            //mail.From = new MailAddress(correo_envia);
            //mail.To.Add(correo_recibe);

            //mail.Subject = cabecera;
            //mail.Body = cuerpo;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "mail.dilaco.com";

            //try
            //{
            //    smtp.Send(mail);
            //    return "OK";
            //}

        }
    }
}