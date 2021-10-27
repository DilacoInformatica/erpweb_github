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
    public partial class Ppal : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        string ruta_alterna = "";
        string master_queryString = "";
        DataSet ds_master = new DataSet();
        string usuario = "";
        string id_usuario = "";
        HttpContext context = HttpContext.Current;

        DataTable lista_errores = new DataTable();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_008_02", Sserver) == "NO")
                    {
                        Response.Redirect("ErrorAcceso.html");
                    }
                    lbl_conectado.Text = Session["Usuario"].ToString();
                    usuario = Session["Usuario"].ToString();
                    id_usuario = Session["id_usuario"].ToString();
                }

                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "D"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Desarrollo"; }
                else
                { lbl_ambiente.Text = "P"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Producción"; }

                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");
            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }

            ruta_alterna = utiles.retorna_ruta();

            if (!this.IsPostBack)
            {
                carga_contrl_lista("select id_familia, nombre from tbl_Familias_Productos where Activo = 1 order by nombre", LstDivision, "tbl_Familias_Productos", "id_familia", "Nombre");
                //carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta, ' ', Nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by nombre", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                carga_contrl_lista("select ID_Proveedor, substring(Razon_Social,1,50) Razon_Social from tbl_Proveedores where Activo = 1 order by razon_social", LstProveedores, "tbl_Proveedores", "ID_Proveedor", "Razon_Social");
                carga_contrl_lista("select 'A' id_lista, 'A' letra union all select 'B' id_lista, 'B' letra union all select 'C' id_lista, 'C' letra union all select 'D' id_lista, 'D' letra union all select 'E' id_lista, 'E' letra union all select 'F' id_lista, 'F' letra union all select 'G' id_lista, 'G' letra", LstLetras, "tbl_letras", "id_lista", "letra");

                if (!String.IsNullOrEmpty((string)(context.Session["SQL"])))
                {
                    master_queryString = (string)(context.Session["SQL"].ToString());
                }

                carga_productos("");
                productos_publicados();
            }

           // carga_productos("");
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
                lista.DataSource = dr;
                lista.DataValueField = llave;
                lista.DataTextField = Campo;
                lista.DataBind();

                connection.Close();
                connection.Dispose();
            }
        }

        void carga_productos(String codigo)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                if (!String.IsNullOrEmpty(master_queryString))
                {
                    master_queryString = (string)(context.Session["SQL"].ToString());
                }
                else
                {
                   
                    master_queryString = " Select tbl_items_web.id_item Id, ";
                    master_queryString = master_queryString + "tbl_items_web.codigo codigo, ";
                    master_queryString = master_queryString + " substring(tbl_items_web.descripcion, 0, 30) descripcion, ";
                   // master_queryString = master_queryString + "substring(tbl_items_web.Marca,0,20) 'Marca' , ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.visible, 0) = 0, 'N', 'S') visible, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.prodpedido, 0) = 0, 'N', 'S') a_pedido, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.ventas, 0) = 0, 'N', 'S') venta, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.cotizaciones, 0) = 0, 'N', 'S') cotizacion, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Manual_Tecnico,'') = '','N','S') m_tecnico , ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Presentacion_Producto,'') = '','N', 'S') presentacion, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Foto,'') = '','N', 'S') foto1, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Foto_Grande,'') = '','N','S') foto2, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.video,'') = '','N','S') video, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Hoja_de_Seguridad,'') = '','N','S') H_Seg, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.publicado_sitio,0) = 0,'N','S') publicado, ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.activo,0) = 0,'N','S') activo ";
                    master_queryString = master_queryString + "from tbl_items_web with(nolock) inner join tbl_items on tbl_items.ID_Item = tbl_items_web.Id_Item  ";
                  
                    if (codigo != "")
                    {
                        master_queryString = master_queryString + "and tbl_items_web.codigo like  '" + codigo + "%'";
                    }

                    if (txt_codprov.Text != "")
                    {
                        master_queryString = master_queryString + "and tbl_items_web.Codigo_prov like  '" + txt_codprov.Text + "%'";
                    }

                    if (LstCategorias.SelectedItem.Value.ToString() != "0")
                    {
                        master_queryString = master_queryString + "and tbl_items_web.Id_Categoria = " + LstCategorias.SelectedItem.Value.ToString();
                    }

                    if (LstSubCategorias.SelectedItem.Value.ToString() != "0")
                    {
                        master_queryString = master_queryString + "and tbl_items_web.Id_Subcategoria = " + LstSubCategorias.SelectedItem.Value.ToString();
                    }

                    if (LstLetras.SelectedItem.Value.ToString() != "0")
                    {
                        master_queryString = master_queryString + "and tbl_items.Sigla = '" + LstLetras.SelectedItem.Value.ToString() + "'";
                    }

                    if (LstLineaVtas.SelectedItem.Value.ToString() != "0")
                    {
                        master_queryString = master_queryString + "and tbl_items.Id_Linea_Venta = '" + LstLineaVtas.SelectedItem.Value.ToString() + "'";
                    }

                    if (LstDivision.SelectedItem.Value.ToString() != "0")
                    {
                       // master_queryString = master_queryString + "and tbl_Familias_Productos.Id_Familia = '" + LstDivision.SelectedItem.Value.ToString() + "'";

                        master_queryString = master_queryString + "and tbl_items_web.Id_Categoria in (select ID_Categoria from tbl_Familias_Productos fm ";
                        master_queryString = master_queryString + "inner join tbl_Categorias ct on ct.Id_Familia = fm.ID_Familia ";
                        master_queryString = master_queryString + "where fm.ID_Familia = " + LstDivision.SelectedItem.Value.ToString()  + ") ";
                    }

                    if (LstProveedores.SelectedItem.Value.ToString() != "0")
                    {
                        master_queryString = master_queryString + "and tbl_items.Id_proveedor = " + LstProveedores.SelectedItem.Value.ToString();
                    }

                    if (chk_sin_cat.Checked)
                    {
                        master_queryString = master_queryString + "and isnull(tbl_items.Id_Categoria,0) = 0 ";
                    }

                    if (chk_no_publicados.Checked)
                    {
                        master_queryString = master_queryString + "and isnull(tbl_items_web.publicado_sitio,0) = 0 ";
                    }

                    if (chk_publicados.Checked)
                    {
                        master_queryString = master_queryString + "and isnull(tbl_items_web.publicado_sitio,0) = 1 ";
                    }

                    if (chk_sin_imagenes.Checked)
                    {
                       // master_queryString = master_queryString + "and len(isnull(tbl_items_web.Foto, 0)) + len(isnull(tbl_items_web.Foto_Grande, 0)) = 0 ";
                        master_queryString = master_queryString + "and isnull(tbl_items_web.Foto, '') = '' and isnull(tbl_items_web.Foto_Grande,'') = ''";
                    }


                    if (chk_cotizac.Checked)
                    {
                        // master_queryString = master_queryString + "and len(isnull(tbl_items_web.Foto, 0)) + len(isnull(tbl_items_web.Foto_Grande, 0)) = 0 ";
                        master_queryString = master_queryString + "and isnull(tbl_items_web.cotizaciones, 0) = 1";
                    }


                    if (chk_ventas.Checked)
                    {
                        // master_queryString = master_queryString + "and len(isnull(tbl_items_web.Foto, 0)) + len(isnull(tbl_items_web.Foto_Grande, 0)) = 0 ";
                        master_queryString = master_queryString + "and isnull(tbl_items_web.ventas, 0) = 1";
                    }
                }
                connection.Open();
                
                SqlDataAdapter reader = new SqlDataAdapter(master_queryString, connection);
                //DataSet ds = new DataSet();
                reader.Fill(ds_master, "tbl_items_web");

                Productos.DataSource = ds_master;
                Productos.DataBind();
                Productos.DataMember = "tbl_items_web";

                lbl_cantidad.Text = Convert.ToString(Productos.Rows.Count);
            }
        }

        public int validamysql(int id_item)
        {
            string query = "";
            query = "select count(1) existe from tbl_items where Id_Item = " + id_item;

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            if (dr.GetString(0) == "0")
                            {
                                return 0;

                            }
                            else
                            {
                                return 1;
                            }
                        }

                    }
                    conn.Close();
                    conn.Dispose();
                    return 1;
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message.ToString();
                    conn.Close();
                    conn.Dispose();
                    return 0;
                }
            }
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            //GridResultados.Visible = false;
           // LstProductos.Visible = false;
            carga_productos(txt_codigo.Text);
            context.Session["SQL"] = master_queryString;
           
        }

        protected void Productos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Productos.SelectedRow;
            Response.Redirect("Items_detalle.aspx?id_item="+ row.Cells[1].Text + "&usuario="+ id_usuario + "&modo='W'");
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);
            Productos.RenderControl(HtmlTextWriter);

            Response.Write(StringWriter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        void act_nom_archivosserver (string sentencia)
        {
            string query = sentencia;
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    connection.Close();
                    connection.Dispose();
                }
            }
        }



        void act_nom_archivomysql(string sentencia)
        {
            string query = sentencia;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();

                    conn.Close();
                    conn.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
               
            }
        }

        void productos_publicados()
        {
            string query = "";
            query = "SELECT COUNT(1) FROM tbl_items ";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            lbl_prod_publicados.Text = dr.GetString(0);
                        }
                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                lbl_prod_publicados.Text = ex.Message.ToString();

            }
        }

        public Boolean validamysql(string id_item)
        {
            string query = "";
            query = "select count(1) existe from tbl_items where Id_Item = " + id_item.Trim();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            if (dr.GetString(0) == "0")
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    conn.Close();
                    conn.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;

            }
        }

        protected void Btn_cerrar_Click(object sender, EventArgs e)
        {
            HtmlControl control = FindControl("resultado") as HtmlControl;
            control.Attributes["style"] = "none"; 
        }

        protected void LstCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstCategorias.SelectedValue.ToString();
            LstSubCategorias.Items.Clear();
            LstSubCategorias.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria = "+ valor  + " order by nombre ", LstSubCategorias, "tbl_categorias", "ID_SubCategoria", "Nombre");
        }

        protected void LstDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstDivision.SelectedValue.ToString();
            LstCategorias.Items.Clear();
            LstCategorias.Items.Add(new ListItem("Seleccione", "0", true));


            LstSubCategorias.Items.Clear();
            LstSubCategorias.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_Categoria, Nombre from tbl_categorias where Activo = 1 and Id_Familia = " + valor + " order by nombre", LstCategorias, "tbl_categorias", "ID_Categoria", "Nombre");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session.Remove("SQL");
            Response.Redirect("Ppal.aspx");
        }

        protected void LinkButton2_Click1(object sender, EventArgs e)
        {
            Session.Remove("SQL");
            Response.Redirect("Ppal.aspx");
        }

        protected void Productos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ver")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Productos.Rows[index];

                Response.Redirect("Items_detalle.aspx?id_item=" + row.Cells[1].Text + "&usuario=" + id_usuario + "&modo='W'");
            }
        }

        protected void Productos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_num_cot_erp = e.Row.FindControl("lbl_num_cot_erp") as Label;

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        public string consulta_valor (string valor, int id_item)
        {
            string v_result = "";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_info_item", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_item", id_item);
                    cmd.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_valor", valor);
                    cmd.Parameters["@v_valor"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                v_result = rdr.GetString(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return v_result;
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message.ToString();
                    return "0";
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}