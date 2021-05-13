using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace erpweb
{
    public partial class Index : System.Web.UI.Page
    {
        int id_usuario = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           // Session.Abandon();
            if (!String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                id_usuario = Convert.ToInt32(Request.QueryString["usuario"]);
            }
            else
            {
                id_usuario =141;
            }
            // Creamos las sessiones
            Session["id_usuario"]  = id_usuario;
            Response.Redirect("Ppal.aspx");
        }
    }
}