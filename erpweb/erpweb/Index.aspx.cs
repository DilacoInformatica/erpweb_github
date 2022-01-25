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
        string Sserver = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Session.Abandon();

            Sserver = utiles.verifica_ambiente("SSERVER");
            if (!String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                id_usuario = Convert.ToInt32(Request.QueryString["usuario"]);
            }
            else
            {
                id_usuario = 98;
            }

            if (utiles.obtiene_acceso_pagina(utiles.obtiene_nombre_usuario(id_usuario,Sserver), "OPC_009_11", Sserver) == "NO")
            {
                Response.Redirect("ErrorAcceso.html");
            }
          
            // Creamos las sessiones
            Session["id_usuario"]  = id_usuario;
            Response.Redirect("Ppal.aspx");
        }
    }
}