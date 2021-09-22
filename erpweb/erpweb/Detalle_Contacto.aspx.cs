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
    public partial class Detalle_Contacto : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        int id_contacto = 0;
        int v_id_contacto = 0;
        string usuario = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            id_contacto = Convert.ToInt32(Request.QueryString["nv"].ToString());
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            Btn_Respuesta.Attributes["Onclick"] = "return confirm('Confirma respuesta a Contacto desde el Sitio Web? Respuesta será enviada vía correo electrónico')";
            if (String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                usuario = "2"; // mi usuarios por default mientras no nos conectemos al servidor
            }
            else
            {
                usuario = Request.QueryString["usuario"].ToString();
            }
            if (!this.IsPostBack)
            {
                lbl_error.ForeColor = Color.Red;
                muestra_info_contacto(id_contacto);
            } 
        }

        void muestra_info_contacto(int id_contacto)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            string queryString = "";

            queryString = "select id_contacto 'Id', "; // 0
            queryString = queryString + "nombre 'Nombre',  ";
            queryString = queryString + "email 'Email', ";
            queryString = queryString + "fono 'Fono', "; // 3
            queryString = queryString + "celular 'Celular', ";
            queryString = queryString + "DATE_FORMAT(Fecha, '%d-%m-%Y') 'Fecha', ";
            queryString = queryString + "texto "; //5
            queryString = queryString + "from tbl_contacto_sitio ";
            queryString = queryString + "where leido_erp = 1 ";
            queryString = queryString + "AND id_contacto = " + id_contacto;

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
                            lbl_numero.Text = dr.GetString(0);
                            lbl_nombre.Text = dr.GetString(1);
                            v_id_contacto = Convert.ToInt32(dr.GetString(0));
                            lbl_fecha.Text = dr.GetString(5);
                            // Clientes
                          
                            lbl_fono.Text = dr.GetString(3);
                            lbl_email.Text = dr.GetString(2);
                            lbl_celular.Text = dr.GetString(4);
                            txt_comentario.Text = dr.GetString(6);
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

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Contacto_Sitio.aspx");
        }

        protected void Btn_Respuesta_Click(object sender, EventArgs e)
        {
            string queryString = "";
            string cuerpo_correo = "";
            Page.Validate();
            if (Page.IsValid)
            {
                if (Txt_Respuesta.Text == "")
                {
                    lbl_error.Text = "Debe indicar Respuesta al contacto solicitado";
                }
                else
                { 
                    queryString = "UPDATE tbl_contacto_sitio ";
                    queryString = queryString + "SET respuesta = '" + Context.Server.HtmlDecode(Txt_Respuesta.Text) +"'" ;
                    queryString = queryString + " ,fecha_respuesta = NOW() ";
                    queryString = queryString + " ,id_usuario = " + usuario;
                    queryString = queryString + " ,leido_erp = 1";
                    queryString = queryString + " WHERE id_contacto = " + lbl_numero.Text;
                    using (MySqlConnection conn = new MySqlConnection(SMysql))
                    {
                        try
                        {
                            conn.Open();
                            MySqlCommand command = new MySqlCommand(queryString, conn);
                            command.ExecuteNonQuery();

                            conn.Close();
                            conn.Dispose();

                            // enviar correo a cliente indicando respuesta
                            cuerpo_correo ="<table class='auto-style20'>";
                            cuerpo_correo = cuerpo_correo + "<tr>";
                            cuerpo_correo = cuerpo_correo + "<tr> ";
                            cuerpo_correo = cuerpo_correo + "<td class='auto-style21'><strong>Importadora Dilaco</strong></td>";
                            cuerpo_correo = cuerpo_correo + "</tr> ";
                            cuerpo_correo = cuerpo_correo + "<tr>";
                            cuerpo_correo = cuerpo_correo + "<td class='auto-style22'>Estimado Cliente " + lbl_nombre.Text + "&nbsp; en refencia a su contacto a través de nuestro sitio web, uno de nuestros representantes de venta realizó el siguiente comentario</td>";
                            cuerpo_correo = cuerpo_correo + "</tr>";
                            cuerpo_correo = cuerpo_correo + "<tr>";
                            cuerpo_correo = cuerpo_correo + "<td>&nbsp;</td>";
                            cuerpo_correo = cuerpo_correo + "</tr>";
                            cuerpo_correo = cuerpo_correo + "<tr>";
                            cuerpo_correo = cuerpo_correo + "<td>"+ Txt_Respuesta.Text +"</td>";
                            cuerpo_correo = cuerpo_correo + "</tr>";
                            cuerpo_correo = cuerpo_correo + "<tr>";
                            cuerpo_correo = cuerpo_correo + "<td class='auto-style22'></td>";
                            cuerpo_correo = cuerpo_correo + "</tr>";
                            cuerpo_correo = cuerpo_correo + "<tr>";
                            cuerpo_correo = cuerpo_correo + "<td>Si desea mas información, envíe correo a<a href= 'mailto:pedidos@dilaco.com' > pedidos@dilaco.com</a>&nbsp; o comuniquese al(56) 224029700<span class='auto-style23'> </span>y un representante de ventas se comunicará un UD.</td>";
                            cuerpo_correo = cuerpo_correo + "</tr>";
                            cuerpo_correo = cuerpo_correo + "<tr>";
                            cuerpo_correo = cuerpo_correo + "<td>Importadora Dilaco - Pérez Valenzuela 1138, Providencia, Santiago</td>";
                            cuerpo_correo = cuerpo_correo + "</tr>";
                            cuerpo_correo = cuerpo_correo + "</table>";

                            utiles.enviar_correo("Contacto Cliente", cuerpo_correo, "info@dilaco.com");

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
        }
    }
}