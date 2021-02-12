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
        int v_categoria = 0;
        int v_subcategoria = 0;
        int v_famila = 0;

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
            string categoria = "";
            string subcategoria = "";
            string famila = "";



            lbl_error.Text = "";
          if (LstDivision.SelectedValue.ToString() == "0")
            {
                lbl_error.Text = "Debe seleccionar una División para hacer una revisión";
            }
          else
            {
                famila = LstDivision.SelectedValue.ToString();
                categoria = LstCategoria.SelectedValue.ToString();
                subcategoria = LstSubCategoria.SelectedValue.ToString();

                if (famila == "") {v_famila = 0; } else {v_famila = Convert.ToInt32(LstDivision.SelectedValue.ToString());}
                if (categoria == "") {v_categoria = 0; } else {v_categoria = Convert.ToInt32(LstCategoria.SelectedValue.ToString());}
                if (subcategoria == "") {v_subcategoria = 0; } else {v_subcategoria = Convert.ToInt32(LstSubCategoria.SelectedValue.ToString());}

                // FAMILIAS
                Lista_division_erp("F", v_famila, v_categoria, v_subcategoria, GrdDivERP);
                lista_division_web("F", v_famila, v_categoria, v_subcategoria, GridDivWeb);

                // CATEGORIAS

                Lista_division_erp("C", v_famila, v_categoria, v_subcategoria, GrdCategoriasERP);
                lista_division_web("C", v_famila, v_categoria, v_subcategoria, GrdCategoriasWEB);

                //lista_info_categorias_web(v_famila, v_categoria);
            }
        }


        void Lista_division_erp(string rama, int id_familia, int categoria, int subcategoria, GridView grilla)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_muestra_info_ramas_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_rama", rama);
                    cmd.Parameters["@v_rama"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_fam", id_familia);
                    cmd.Parameters["@v_fam"].Direction = ParameterDirection.Input;

                    if (categoria == 0)
                    {
                        cmd.Parameters.AddWithValue("@v_cat", DBNull.Value);
                        cmd.Parameters["@v_cat"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@v_cat", categoria);
                        cmd.Parameters["@v_cat"].Direction = ParameterDirection.Input;
                    }


                    if (subcategoria == 0)
                    {
                        cmd.Parameters.AddWithValue("@v_subcat", DBNull.Value);
                        cmd.Parameters["@v_subcat"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@v_subcat", LstSubCategoria.SelectedValue.ToString());
                        cmd.Parameters["@v_subcat"].Direction = ParameterDirection.Input;
                    }

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    grilla.DataSource = ds;
                    grilla.DataBind();

                    SqlDataAdapter reader = new SqlDataAdapter();

                 //   GrdDivERP.DataMember = "tbl_Familias_Productos";


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


        void lista_division_web(string rama, int id_familia, int categoria, int subcategoria, GridView Grilla)
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
                    MySqlCommand cmd = new MySqlCommand("Lista_ramas_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@v_rama", rama);
                    cmd.Parameters["@v_rama"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_division", id_familia);
                    cmd.Parameters["@v_division"].Direction = ParameterDirection.Input;

                    if (categoria == 0)
                    {
                        cmd.Parameters.AddWithValue("@v_categoria", DBNull.Value);
                        cmd.Parameters["@v_categoria"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@v_categoria", categoria);
                        cmd.Parameters["@v_categoria"].Direction = ParameterDirection.Input;
                    }


                    if (subcategoria == 0)
                    {
                        cmd.Parameters.AddWithValue("@v_subcategoria", DBNull.Value);
                        cmd.Parameters["@v_subcategoria"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@v_subcategoria", subcategoria);
                        cmd.Parameters["@v_subcategoria"].Direction = ParameterDirection.Input;
                    }

                   
                    DataSet ds = new DataSet();
                    DataTable table = new DataTable();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(cmd);

                    ///MySqlDataReader dr = command.ExecuteReader();
                    mysqlDAdp.Fill(table);
                    Grilla.DataSource = table;
                    Grilla.DataBind();

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
                Chk_activo.Enabled = false;

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

        protected void GrdDivERP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                CheckBox Chk_activo = e.Row.FindControl("Chk_Activo") as CheckBox;

                // lbl_error.Text = drv["Activo"].ToString();

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_activo.Checked = true;
                }
                else
                {
                    Chk_activo.Checked = false;
                }


                CheckBox publicado = e.Row.FindControl("Chk_FamPublicado") as CheckBox;

                if (consulta_estado_publicacion("C", v_famila, 0, 0) == 0)
                {
                    publicado.Checked = false;
                }
                else
                {
                    publicado.Checked = true;
                }

                TextBox etiqueta = e.Row.FindControl("txt_nombre") as TextBox;

                etiqueta.Text = drv["Nombre"].ToString();
                etiqueta.Enabled = false;

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void GridDivWeb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                CheckBox Chk_activo = e.Row.FindControl("Chk_Activo") as CheckBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_activo.Checked = true;
                }
                else
                {
                    Chk_activo.Checked = false;
                }

                Chk_activo.Enabled = false;

                CheckBox Chk_visible = e.Row.FindControl("Chk_visible") as CheckBox;

                if (Convert.ToBoolean(drv["Visible"]))
                {
                    Chk_visible.Checked = true;
                }
                else
                {
                    Chk_visible.Checked = false;
                }

                TextBox etiqueta = e.Row.FindControl("txt_etiqueta") as TextBox;

                etiqueta.Text = drv["AliasNombre"].ToString();


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void GrdCategoriasERP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                CheckBox Chk_visible = e.Row.FindControl("Chk_Activo") as CheckBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_visible.Checked = true;
                }
                else
                {
                    Chk_visible.Checked = false;
                }

                CheckBox publicado = e.Row.FindControl("Chk_CatPublicada") as CheckBox;
                
                if (consulta_estado_publicacion("C",v_famila, Convert.ToInt32(drv["ID_Categoria"]),0) == 0)
                {
                    publicado.Checked = false;
                }
                else
                {
                    publicado.Checked = true;
                }


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        public int consulta_estado_publicacion (string rama, int familia, int categoria, int subcategoria)
        {
            int resultado = 0;
 
             using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("consulta_ramas_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@v_rama", rama);
                    cmd.Parameters["@v_rama"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_division", familia);
                    cmd.Parameters["@v_division"].Direction = ParameterDirection.Input;

                    if (categoria == 0)
                    {
                        cmd.Parameters.AddWithValue("@v_categoria", DBNull.Value);
                        cmd.Parameters["@v_categoria"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@v_categoria", categoria);
                        cmd.Parameters["@v_categoria"].Direction = ParameterDirection.Input;
                    }


                    if (subcategoria == 0)
                    {
                        cmd.Parameters.AddWithValue("@v_subcategoria", DBNull.Value);
                        cmd.Parameters["@v_subcategoria"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@v_subcategoria", subcategoria);
                        cmd.Parameters["@v_subcategoria"].Direction = ParameterDirection.Input;
                    }


                    MySqlDataReader myReader;
                    myReader = cmd.ExecuteReader();  //stop here
                    try
                    {
                        while (myReader.Read())
                        {
                            resultado = myReader.GetInt32(0);
                        }
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }

                    return resultado;

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
            
        }

        protected void GrdCategoriasERP_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdCategoriasERP.SelectedRow;
            lbl_cat.Text = "";
            lbl_cat.Text = row.Cells[1].Text;
            Lista_division_erp("S", Convert.ToInt32(LstDivision.SelectedValue.ToString()), Convert.ToInt32(row.Cells[1].Text),0, GrdSubCatERP);
            
            lista_division_web("S", Convert.ToInt32(LstDivision.SelectedValue.ToString()), Convert.ToInt32(row.Cells[1].Text), 0, GrdSubCatWEB);
            //  
        }

        protected void GrdSubCatERP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                CheckBox Chk_visible = e.Row.FindControl("Chk_Activo") as CheckBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_visible.Checked = true;
                }
                else
                {
                    Chk_visible.Checked = false;
                }

                CheckBox publicado = e.Row.FindControl("Chk_PubSubCat") as CheckBox;

                if (consulta_estado_publicacion("S", v_famila, Convert.ToInt32(lbl_cat.Text),  Convert.ToInt32(drv["ID_SubCategoria"])) == 0)
                {
                    publicado.Checked = false;
                }
                else
                {
                    publicado.Checked = true;
                }


                 e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                 e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                 e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                 e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                 e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void GrdSubCatWEB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                CheckBox Chk_visible = e.Row.FindControl("ChkActivoSC") as CheckBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_visible.Checked = true;
                }
                else
                {
                    Chk_visible.Checked = false;
                }

                Chk_visible.Enabled = false;

                CheckBox publicado = e.Row.FindControl("ChkVisibleSC") as CheckBox;

                if (Convert.ToBoolean(drv["Visible"]))
                {
                    publicado.Checked = true;
                }
                else
                {
                    publicado.Checked = false;
                }


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
}