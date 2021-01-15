using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

using System.Collections;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace erpweb
{
    public partial class Adm_Publicacion_Lineas_Prod__ : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        string usuario = "";
        int id_familia = 0;
        int id_categoria = 0;
        int id_subcategoria = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
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
               Btn_Ac_Div.Attributes["Onclick"] = "return confirm('Este cambio afectará a toda la división, Desaea Continuar?')";
                // Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
                carga_contrl_lista("select id_familia, nombre from tbl_Familias_Productos where Activo = 1 order by nombre", LstDivision, "tbl_Familias_Productos", "id_familia", "Nombre");
            }
        }

        void carga_contrl_lista(string sql, DropDownList lista, string tabla, string llave, string Campo)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                connection.Open();
                //SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                DataSet dr = new DataSet();
                reader.Fill(dr, tabla);
                //lista.AppendDataBoundItems = true;
                // lista.Items.Add("Seleccione...");
                lista.DataSource = dr;
                lista.DataValueField = llave;
                lista.DataTextField = Campo;
                lista.DataBind();

                connection.Close();
                connection.Dispose();
            }
        }

        protected void LstDivivion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstDivision.SelectedValue.ToString();
            LstSubCategoria.Items.Clear();
            LstSubCategoria.DataBind();
            LstSubCategoria.Items.Clear();

            LstCategoria.Items.Clear();
            LstCategoria.DataBind();

            LstCategoria.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_Categoria, Nombre from tbl_categorias where Activo = 1 and id_familia =" + valor + " order by Nombre", LstCategoria, "tbl_categorias", "ID_Categoria", "Nombre");
        }

        protected void LstCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstCategoria.SelectedValue.ToString();

            LstSubCategoria.Items.Clear();
            LstSubCategoria.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria =" + valor, LstSubCategoria, "tbl_categorias", "ID_SubCategoria", "Nombre");
        }

        protected void Btn_Buscar_Click(object sender, EventArgs e)
        {
            lbl_error.Text = "";
          if (LstDivision.SelectedValue.ToString() == "0")
            {
                lbl_error.Text = "Debe seleccionar una División para hacer una revisión";
            }
          else
            {
                lista_info_division();
                lista_info_categorias(LstDivision.SelectedValue.ToString());
            }
        }


        void lista_info_categorias(string id_familia)
        {
         
            string v_AliasNombre = "";
            string v_nombre = "";

            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("lista_categorias", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros


                    command.Parameters.AddWithValue("@v_division", LstDivision.SelectedValue.ToString());
                    command.Parameters["@v_division"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    DataTable table = new DataTable();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                   
                    ///MySqlDataReader dr = command.ExecuteReader();
                    mysqlDAdp.Fill(table);

                    if (!table.HasErrors)
                    {
                        lbl_mensaje.Text = "Sin Resultados";
                    }
                    else
                    {
                        GrdCategorias.DataSource = table;
                        GrdCategorias.DataBind();

                       // lbl_cantidad.Text = "Cantidad de Registros: " + Convert.ToString(Lista_cotizacion.Rows.Count);
                    }

                    //lbl_cantidad.Text = "Cantidad de Registros: " + Convert.ToString(GrdCategorias.Rows.Count);



                    connection.Close();
                    connection.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        void lista_info_division()
        {
            int v_id_familia = 0;
            string v_codigo = "";
            int v_orden = 0;
            int v_activo = 0;
            int v_visible = 0;
            string v_AliasNombre = "";
            string v_nombre = "";

            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("lista_division", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros


                    cmd.Parameters.AddWithValue("@v_division", LstDivision.SelectedValue.ToString());
                    cmd.Parameters["@v_division"].Direction = ParameterDirection.Input;

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {

                                /*id_familia, codigo, nombre, orden, activo, visible, AliasNombre*/
                                //lbl_error.Text = rdr.GetInt32(0).ToString();
                                v_id_familia = rdr.GetInt32(0);
                                v_codigo = rdr.GetString(1);
                                v_nombre = rdr.GetString(2);
                                v_orden = rdr.GetInt32(3);
                                v_activo = rdr.GetInt32(4);
                                v_visible = rdr.GetInt32(5);
                                v_AliasNombre = rdr.GetString(6);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }

            lbl_nombre.Text = v_nombre;
            txt_alias.Text = v_AliasNombre;
            if (v_visible == 1)
            {
                ChkDivVisible.Checked = true;
            }
            else
            {
                ChkDivVisible.Checked = false;
            }
        }


        protected void Btn_Ac_Div_Click(object sender, EventArgs e)
        {
            Page.Validate();

           

            if (Page.IsValid)
            {
               

            }
        }

        protected void GrdCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
               // DataRowView dr = (DataRowView)e.Row.DataItem;
                /* DropDownList ddlFamilias = e.Row.FindControl("LstFamilias") as DropDownList;

                 Label ID = e.Row.FindControl("lbl_ID") as Label;
                 ID.Text = drv["ID"].ToString();

                 Label Cod = e.Row.FindControl("Lbl_Cod") as Label;
                 Cod.Text = drv["Cod"].ToString();

                 Label Cat = e.Row.FindControl("Lbl_Categoria") as Label;
                 Cat.Text = drv["Categoría"].ToString();*/

                CheckBox Chk_activo = e.Row.FindControl("Chk_activo") as CheckBox;

                if (drv[3].ToString() == "1")
                {
                    Chk_activo.Checked = true;
                }
                else
                {
                    Chk_activo.Checked = false;
                }

                CheckBox Chk_visible = e.Row.FindControl("Chk_visible") as CheckBox;

                if (drv["Visible"].ToString() == "1")
                {
                    Chk_visible.Checked = true;
                }
                else
                {
                    Chk_visible.Checked = false;
                }
            }
        }

    }
}