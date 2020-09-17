using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace erpweb
{
    public class Cls_Utilitarios
    {
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

        public string verifica_ambiente(string servidor, int ambiente)
        {
            string salida = "";
            if (servidor == "SSERVER" && ambiente == 1)
            {
                salida =  @"Data Source=LAPTOP-NM5HA1B3;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Local
            }
            if (servidor == "SSERVER" && ambiente == 2)
            {
                salida =   @"Data Source=172.16.10.13\DILACO;Initial Catalog=dilaco;uid=sa; pwd= d|l@c02016;Integrated Security=false"; // Conexion Servidor
            }
            if (servidor == "MYSQL" && ambiente == 1)
            {
                salida = @"Server=localhost;database=dilacocl_dilacoweb;uid=root;pwd=d|l@c0;CHARSET=utf8;"; // Conexion  Local
            }
            if (servidor == "MYSQL" && ambiente == 2)
            {
                salida =  @"server=dev.dilaco.com;database=dilacocl_dilacoweb;uid=dilacocl_dilaco;pwd=d|l@c02019;"; // Conexion Server
            }

            return salida;

        }
    }
}