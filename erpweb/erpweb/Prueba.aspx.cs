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

namespace erpweb
{
    public partial class Prueba : System.Web.UI.Page
    {
        string Sserver = @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
        string SMysql = @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                carga_productos();
            }
        }

        void carga_productos()
        {
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    lbl_mensaje.Text = "Conectado como loco";


                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_mensaje.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}