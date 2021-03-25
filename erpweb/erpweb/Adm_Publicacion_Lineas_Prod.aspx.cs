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
        int v_activo_CAT = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            Btn_Grabar.Attributes["Onclick"] = "return confirm('Desea grabar cambios, estos pueden afectar a toda la rama de la familia involucrada?')";

            if (Session["Usuario"].ToString() == "")
            {
                Response.Redirect("Ppal.aspx");
            }
            else
            {
                if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), " 	OPC_009_09", Sserver) == "NO")
                {
                    Response.Redirect("ErrorAcceso.html");
                }
                lbl_conectado.Text = Session["Usuario"].ToString();
            }
            if (utiles.retorna_ambiente() == "D")
            { lbl_ambiente.Text = "Ambiente Desarrollo"; }
            else
            { lbl_ambiente.Text = "Ambiente Producción"; }

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
            lbl_error.Text = "";
            procesar_busqueda();
          
        }

        void procesar_busqueda()
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
                /* GrdDivERP.Columns.Clear();
                 GrdCategoriasERP.Columns.Clear();
                 GrdSubCatERP.Columns.Clear();*/


                GrdCategoriasERP.DataBind();
                GrdSubCatERP.DataBind();

                /*GridDivWeb.Columns.Clear();
                 GrdCategoriasWEB.Columns.Clear();
                 GrdSubCatWEB.Columns.Clear();*/




                famila = LstDivision.SelectedValue.ToString();
                categoria = LstCategoria.SelectedValue.ToString();
                subcategoria = LstSubCategoria.SelectedValue.ToString();

                if (lbl_cat.Text != "")
                {
                    categoria = lbl_cat.Text;
                }

                if (famila == "") { v_famila = 0; } else { v_famila = Convert.ToInt32(LstDivision.SelectedValue.ToString()); }
                if (categoria == "") { v_categoria = 0; } else { v_categoria = Convert.ToInt32(LstCategoria.SelectedValue.ToString()); }
                
                if (subcategoria == "") { v_subcategoria = 0; } else { v_subcategoria = Convert.ToInt32(LstSubCategoria.SelectedValue.ToString()); }

                // FAMILIAS
                Lista_division_erp("F", v_famila, v_categoria, v_subcategoria, null);
                lista_division_web("F", v_famila, v_categoria, v_subcategoria, null);

                // CATEGORIAS

                Lista_division_erp("C", v_famila, v_categoria, v_subcategoria, GrdCategoriasERP);

                if(v_categoria == 0 && lbl_cat.Text != "") { v_categoria = Convert.ToInt32(lbl_cat.Text); }
                Lista_division_erp("S", v_famila, v_categoria, v_subcategoria, GrdSubCatERP);

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

                    if (rama !=  "F")
                    {
                        SqlDataAdapter da = new SqlDataAdapter();

                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        grilla.DataSource = ds;
                        grilla.DataBind();

                    }
                    else
                    {

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (!rdr.IsDBNull(0))
                                {
                                    Txt_Id_ERP.Text = Convert.ToString(rdr.GetInt32(0));
                                    Txt_Cod_ERP.Text = rdr.GetString(1);
                                    Txt_Desc_ERP.Text = rdr.GetString(2);
                                    Txt_Orden_ERP.Text = Convert.ToString(rdr.GetInt32(4));
                                    if (Convert.ToString(rdr.GetBoolean(5)) == "True") { Chk_Activo_ERP.Checked = true; } else { Chk_Activo_ERP.Checked = false; }
                                    if (consulta_estado_publicacion("F", id_familia, 0, 0) == 0) {Chk_En_Sitio_ERP.Checked = false;} else { Chk_En_Sitio_ERP.Checked = true; }
                                }
                            }
                        }
                    }

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

                    if (rama != "F")
                    {
                        DataSet ds = new DataSet();
                        DataTable table = new DataTable();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(cmd);

                        ///MySqlDataReader dr = command.ExecuteReader();
                        mysqlDAdp.Fill(table);
                        Grilla.DataSource = table;
                        Grilla.DataBind();

                    }
                    else
                    {
                        
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (!rdr.IsDBNull(0))
                                {
                                    Txt_Id_WEB.Text = Convert.ToString(rdr.GetInt32(0));
                                    Txt_Cod_WEB.Text = rdr.GetString(1);
                                    Txt_Desc_WEB.Text = rdr.GetString(2);
                                    Txt_Orden_WEB.Text = Convert.ToString(rdr.GetString(3));
                                    Txt_Label_WEB.Text = Convert.ToString(rdr.GetString(6));
                                    if (rdr.GetInt32(4) == 1) { Chk_Activo_WEB.Checked = true; } else { Chk_Activo_WEB.Checked = false; }
                                    if (rdr.GetInt32(5) == 1) { Chk_Publicado_Web.Checked = true; } else { Chk_Publicado_Web.Checked = false; }
 
                                }
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
        }


        protected void Btn_Ac_Div_Click(object sender, EventArgs e)
        {
            Page.Validate();



            if (Page.IsValid)
            {


            }
        }

    
       
        protected void GridDivWeb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                CheckBox Chk_activo = e.Row.FindControl("Chk_Activo1") as CheckBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_activo.Checked = true;
                }
                else
                {
                    Chk_activo.Checked = false;
                }

              //  Chk_activo.Enabled = false;

                CheckBox Chk_visible = e.Row.FindControl("Chk_PubDivWeb") as CheckBox;

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

                CheckBox Chk_visible = e.Row.FindControl("Chk_ActivoCAT") as CheckBox;

                TextBox txt_etiqueta_web = e.Row.FindControl("txt_etiqueta_web") as TextBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_visible.Checked = true;
                    
                }
                else
                {
                    Chk_visible.Checked = false;
                }

                txt_etiqueta_web.Text = consulta_etiqueta_web(Convert.ToInt32(Txt_Id_ERP.Text), Convert.ToInt32(drv["ID_Categoria"]),0,"C");

                CheckBox publicado = e.Row.FindControl("Chk_CatPublicada") as CheckBox;

                if (consulta_estado_publicacion("C", v_famila, Convert.ToInt32(drv["ID_Categoria"]), 0) == 0)
                {
                    publicado.Checked = false;
                }
                else
                {
                    publicado.Checked = true;
                }

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        public string consulta_etiqueta_web(int v_id_familia,  int v_id_categoria, int v_id_subcat, string rama)
        {
            string resultado = "";
            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {

                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("consulta_etiqueta_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@v_rama", rama);
                    cmd.Parameters["@v_rama"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_familia", v_id_familia);
                    cmd.Parameters["@v_id_familia"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_cat", v_id_categoria);
                    cmd.Parameters["@v_id_cat"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_subcat", v_id_subcat);
                    cmd.Parameters["@v_id_subcat"].Direction = ParameterDirection.Input;

                    MySqlDataReader myReader;
                    myReader = cmd.ExecuteReader();  //stop here
                    try
                    {
                        while (myReader.Read())
                        {
                            resultado = myReader.GetString(0);
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
                    lbl_error.Text = "error en Procedimiento consulta_etiqueta_web " + ex.Message;
                    return "";
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int consulta_estado_publicacion(string rama, int familia, int categoria, int subcategoria)
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
                    lbl_error.Text = "error en Procedimiento consulta_estado_publicacion " + ex.Message;
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
            Lista_division_erp("S", Convert.ToInt32(LstDivision.SelectedValue.ToString()), Convert.ToInt32(row.Cells[1].Text), 0, GrdSubCatERP);
            //  
        }

        protected void GrdSubCatERP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                CheckBox Chk_visible = e.Row.FindControl("Chk_activo") as CheckBox;
                TextBox txt_etiqueta_web = e.Row.FindControl("txt_etiqueta_web") as TextBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_visible.Checked = true;
                }
                else
                {
                    Chk_visible.Checked = false;
                }

                CheckBox publicado = e.Row.FindControl("Chk_PubSubCat") as CheckBox;

                 if (consulta_estado_publicacion("S", v_famila, Convert.ToInt32(lbl_cat.Text), Convert.ToInt32(drv["ID_SubCategoria"])) == 0)
                {
                    publicado.Checked = false;
                }
                else
                {
                    publicado.Checked = true;
                }

                txt_etiqueta_web.Text = consulta_etiqueta_web(Convert.ToInt32(Txt_Id_ERP.Text), Convert.ToInt32(drv["ID_Categoria"]), Convert.ToInt32(drv["ID_SubCategoria"]), "S");

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        protected void GrdSubCatWEB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                CheckBox Chk_visible = e.Row.FindControl("Chk_ActivoSC") as CheckBox;

                if (Convert.ToBoolean(drv["Activo"]))
                {
                    Chk_visible.Checked = true;
                }
                else
                {
                    Chk_visible.Checked = false;
                }

         //       Chk_visible.Enabled = false;

                CheckBox publicado = e.Row.FindControl("Chk_PubSC") as CheckBox;

                if (Convert.ToBoolean(drv["Visible"]))
                {
                    publicado.Checked = true;
                }
                else
                {
                    publicado.Checked = false;
                }

                publicado.Enabled = false;

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            }
        }

    
        void actualiza_informacion()
        {
            actualiza_division();
            actualiza_categorias();
            actualiza_subcategorias();
        }


        void actualiza_division()
        {
            // Si está marcado como desactivado... desmarcamos la división, caregorias y subcategorias asociadas
            int publicado = 0;
            int activo = 0;


            if (Chk_Publicado_Web.Checked) 
            {
                publicado = 1;
            }
            else
            {
                publicado = 0;
            }

            if (Chk_Activo_ERP.Checked)
            {
                activo = 1;
            }
            else
            {
                activo = 0;
                publicado = 0;
            }

            revisa_division_prod_ERP(Convert.ToInt32(Txt_Id_ERP.Text), "FA", activo);
            revisa_division_prod_WEB(Convert.ToInt32(Txt_Id_ERP.Text), "FA", activo, publicado, Txt_Label_WEB.Text);

            //if (Chk_Activo_ERP.Checked == false)
            //{
            //    revisa_division_prod_ERP(Convert.ToInt32(Txt_Id_ERP.Text), "FA", 0);
            //    revisa_division_prod_WEB(Convert.ToInt32(Txt_Id_ERP.Text), "FA", 0, publicado, Txt_Label_WEB.Text);
            //}
            //else
            //{
            //    revisa_division_prod_ERP(Convert.ToInt32(Txt_Id_ERP.Text), "FA", 1);
            //    revisa_division_prod_WEB(Convert.ToInt32(Txt_Id_ERP.Text), "FA", 1, publicado, Txt_Label_WEB.Text);
            //}
        }

        void actualiza_categorias()
        {
            int publicado = 0;
            int activo = 0;
            foreach (GridViewRow fila in GrdCategoriasERP.Rows)
            {
                // fila.Cells[1].Text;
                //fila.FindControl
                CheckBox Chk_Publicado = fila.FindControl("Chk_CatPublicada") as CheckBox;
                CheckBox Chk_Activo = fila.FindControl("Chk_ActivoCAT") as CheckBox;
                TextBox txt_etiqueta_web = fila.FindControl("txt_etiqueta_web") as TextBox;

                if (Chk_Publicado_Web.Checked)
                {
                    if (Chk_Publicado.Checked)
                    { publicado = 1; }
                    else
                    { publicado = 0; }
                }
                else
                {
                    publicado = 0;
                }

                if (Chk_Activo.Checked)
                {
                    activo = 1;
                }
                else
                {
                    activo = 0;
                    publicado = 0;
                }

                revisa_division_prod_ERP(Convert.ToInt32(fila.Cells[1].Text), "CA", activo);
                revisa_division_prod_WEB(Convert.ToInt32(fila.Cells[1].Text), "CA", activo, publicado, txt_etiqueta_web.Text);

               // Lista_division_erp("S", Convert.ToInt32(Txt_Id_ERP.Text), Convert.ToInt32(fila.Cells[1].Text), 0, GrdSubCatERP);

                // else
                //  {
                //      foreach (GridViewRow filaz in GrdSubCatERP.Rows)
                //         {
                //          CheckBox Chk_Activoz = filaz.FindControl("Chk_activo") as CheckBox;
                //       Chk_Activoz.Checked = true;
                //       }
                //  }
            }
        }

        void actualiza_subcategorias()
        {
            int publicado = 0;
            int activo = 0;
            foreach (GridViewRow fila in GrdSubCatERP.Rows)
            {
                // fila.Cells[1].Text;
                //fila.FindControl
                CheckBox Chk_Publicado = fila.FindControl("Chk_PubSubCat") as CheckBox;
                CheckBox Chk_Activo = fila.FindControl("Chk_Activo") as CheckBox;
                TextBox txt_etiqueta_web = fila.FindControl("txt_etiqueta_web") as TextBox;

                if (Chk_Publicado_Web.Checked)
                {
                    if (Chk_Publicado.Checked)
                    { publicado = 1; }
                    else
                    { publicado = 0; }
                }
                else
                {
                    publicado = 0;
                }

                if (Chk_Activo.Checked)
                {
                    activo = 1;
                }
                else
                {
                    activo = 0;
                    publicado = 0;
                }

                revisa_division_prod_ERP(Convert.ToInt32(fila.Cells[0].Text), "SC", activo);
                revisa_division_prod_WEB(Convert.ToInt32(fila.Cells[0].Text), "SC", activo, publicado, txt_etiqueta_web.Text);

            }
        }

        void revisa_division_prod_ERP(int id_familia, string familia, int status)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_administra_division_productos", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_master", id_familia);
                    cmd.Parameters["@v_id_master"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_division", familia);
                    cmd.Parameters["@v_division"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_visible", status);
                    cmd.Parameters["@v_visible"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                result = rdr.GetString(0);
                            }
                        }
                    }

                    if (result != "OK")
                    {
                        lbl_error.Text = "Error en Procedimiento " + result;
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
        }

        void revisa_division_prod_WEB(int id_familia, string familia, int activo, int status, string etiqueta)
        {
            string queryString = "";
            string result = "";
            queryString = "Adm_div_productos ";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id_master", id_familia);
                    command.Parameters["@v_id_master"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_division", familia);
                    command.Parameters["@v_division"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_activo", activo);
                    command.Parameters["@v_activo"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_visible", status);
                    command.Parameters["@v_visible"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_etiqueta", etiqueta);
                    command.Parameters["@v_etiqueta"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        result = "Sin Resultados";
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                // NV
                                result = Convert.ToString(dr.GetString(0));
                            }
                        }
                    }

                    if (result != "OK")
                    {
                        lbl_error.Text = "Error Procedimiento " + result;
                        lbl_error.Text = "";
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

        protected void Chk_Activo_ERP_CheckedChanged(object sender, EventArgs e)
        {

            if (Txt_Id_ERP.Text != "")
            {
                if (!Chk_Activo_ERP.Checked)
                {
                    lbl_error.Text = "";
                    Chk_En_Sitio_ERP.Checked = false;
                    Chk_Activo_WEB.Checked = false;
                    Chk_Publicado_Web.Checked = false;

                    // Desactivamos Todos los movimientos en Categorías y Subcategorías

                    foreach (GridViewRow fila in GrdCategoriasERP.Rows)
                    {
                        CheckBox Chk_Publicado = fila.FindControl("Chk_CatPublicada") as CheckBox;
                        CheckBox Chk_Activo = fila.FindControl("Chk_ActivoCAT") as CheckBox;

                        Chk_Publicado.Enabled = false;
                        Chk_Activo.Enabled = false;
                    }

                    foreach (GridViewRow fila in GrdSubCatERP.Rows)
                    {
                        CheckBox Chk_Publicado = fila.FindControl("Chk_PubSubCat") as CheckBox;
                        CheckBox Chk_Activo = fila.FindControl("Chk_activo") as CheckBox;

                        Chk_Publicado.Enabled = false;
                        Chk_Activo.Enabled = false;
                    }

                }
                else
                {
                    Chk_Activo_WEB.Checked = true;
                    if (consulta_estado_publicacion("F", Convert.ToInt32(Txt_Id_ERP.Text), 0, 0) == 0) { Chk_En_Sitio_ERP.Checked = false; Chk_Publicado_Web.Checked = false; } else { Chk_En_Sitio_ERP.Checked = true; Chk_Publicado_Web.Checked = true; }
                    procesar_busqueda();
                }
            }    
        }

        protected void Chk_Publicado_Web_CheckedChanged(object sender, EventArgs e)
        {
            if (Txt_Id_ERP.Text != "")
            {
                if (Chk_Publicado_Web.Checked && !Chk_Activo_WEB.Checked)
                {
                    lbl_error.Text = "No puede  publicar sin que no esté activada la División ";
                    Chk_Publicado_Web.Checked = false;
                }

                if (!Chk_Publicado_Web.Checked)
                {

                  
                    lbl_error.Text = "";

                    // Desactivamos Todos los movimientos en Categorías y Subcategorías

                    foreach (GridViewRow fila in GrdCategoriasERP.Rows)
                    {
                        CheckBox Chk_Publicado = fila.FindControl("Chk_CatPublicada") as CheckBox;
                        Chk_Publicado.Checked = false;
                    }

                    foreach (GridViewRow fila in GrdSubCatERP.Rows)
                    {
                        CheckBox Chk_Publicado = fila.FindControl("Chk_PubSubCat") as CheckBox;
                        Chk_Publicado.Checked = false;
                    }

                }
                else
                {
                    Chk_Activo_WEB.Checked = true;
                    if (consulta_estado_publicacion("F", Convert.ToInt32(Txt_Id_ERP.Text), 0, 0) == 0) { Chk_En_Sitio_ERP.Checked = false; Chk_Publicado_Web.Checked = false; } else { Chk_En_Sitio_ERP.Checked = true; Chk_Publicado_Web.Checked = true; }
                    procesar_busqueda();
                }
            }


        }

        protected void Btn_Grabar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                actualiza_informacion();
                procesar_busqueda();
            }
        }

        protected void Btn_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PPal.aspx");
        }
    }
}