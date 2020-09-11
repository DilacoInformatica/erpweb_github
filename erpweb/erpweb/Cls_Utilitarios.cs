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
    }
}