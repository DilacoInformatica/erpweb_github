using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;

namespace erpweb
{
    public partial class Lista_exportacion_items_web : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        string usuario = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
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
            //Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
            if (!this.IsPostBack)
            {
                carga_valores_de_lista();
            }
        }

        void carga_valores_de_lista()
        {
            carga_contrl_lista("select id_familia, nombre from tbl_Familias_Productos where Activo = 1 order by nombre", Lst_division, "tbl_Familias_Productos", "id_familia", "Nombre");
            carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", Lst_LV, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
            carga_contrl_lista("select ID_Proveedor, substring(Razon_Social,1,50) Razon_Social from tbl_Proveedores where Activo = 1 order by razon_social", Lst_Prov, "tbl_Proveedores", "ID_Proveedor", "Razon_Social");
            carga_contrl_lista("select 'A' id_lista, 'A' letra union all select 'B' id_lista, 'B' letra union all select 'C' id_lista, 'C' letra union all select 'D' id_lista, 'D' letra union all select 'E' id_lista, 'E' letra union all select 'F' id_lista, 'F' letra union all select 'G' id_lista, 'G' letra", Lst_Letra, "tbl_letras", "id_lista", "letra");
            // carga_contrl_lista("select ID_Categoria, Nombre from tbl_categorias where Activo = 1  order by nombre", Lst_Cat, "tbl_categorias", "ID_Categoria", "Nombre");
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

        protected void Buscar_Click(object sender, EventArgs e)
        {
            string sql = "";

            sql = "SELECT tbl_Items.id_item ID, tbl_Items.codigo Codigo, dbo.PadreHijo(id_item) ph, isnull(tbl_Items.sigla, '') Letra, tbl_items.descripcion, tbl_items.Unidad, ";
            sql = sql + "isnull(tbl_Familias_Productos.ID_Familia,0) ID_Familia, isnull(tbl_Categorias.ID_Categoria,0) ID_Categoria, isnull(tbl_Subcategorias.ID_SubCategoria,0) ID_SubCategoria, ";
            sql = sql + " isnull((select 1 from tbl_items_web where Id_Item = dbo.tbl_Items.ID_Item),0) item_web ";
           /* sql = "SELECT tbl_Items.id_item, tbl_Items.codigo,dbo.PadreHijo(id_item) ph,  dbo.tbl_Items.Id_Linea_Venta, dbo.tbl_Items.Id_Categoria, dbo.tbl_Items.Id_SubCategoria, Publicar_Web, Ultimo_Factor, Ultimo_Precio , Fecha_precio, dbo.tbl_Items.Sigla,  dbo.tbl_Items.Precio,   dbo.tbl_Items.ID_Item, dbo.tbl_Items.Codigo, dbo.tbl_Items.Descripcion, dbo.tbl_Categorias.Nombre AS Categoria, ";
            sql = sql + " dbo.tbl_Subcategorias.Nombre AS SubCategoria, dbo.tbl_Proveedores.Nombre_Fantasia AS Proveedor, dbo.tbl_Items.Unidad, dbo.tbl_Items.Activo, ";
            sql = sql + " dbo.tbl_Items.Cod_Barra";*/
            sql = sql + " FROM dbo.tbl_Items LEFT OUTER  JOIN";
            sql = sql + " dbo.tbl_Categorias ON dbo.tbl_Items.Id_Categoria = dbo.tbl_Categorias.ID_Categoria LEFT OUTER JOIN";
            sql = sql + " dbo.tbl_Familias_Productos ON tbl_Familias_Productos.ID_Familia = dbo.tbl_Categorias.Id_Familia LEFT OUTER JOIN ";
            sql = sql + " dbo.tbl_Subcategorias ON dbo.tbl_Items.Id_SubCategoria = dbo.tbl_Subcategorias.ID_SubCategoria LEFT OUTER JOIN";
            sql = sql + "  dbo.tbl_Proveedores ON dbo.tbl_Items.Id_proveedor = dbo.tbl_Proveedores.ID_Proveedor";
            sql = sql + " where tbl_Items.Activo = 1 ";
            if (Txt_Codigo.Text != "")
            {
                sql = sql + " and tbl_items_web.codigo like  '" + Txt_Codigo.Text + "%'";
            }

            if (Txt_CodigoProv.Text != "")
            {
                sql = sql + "and tbl_items_web.Codigo_prov like  '" + Txt_CodigoProv.Text + "%'";
            }

            if (Lst_Cat.SelectedItem.Value.ToString() != "0")
            {
                sql = sql + "and tbl_items.Id_Categoria = " + Lst_Cat.SelectedItem.Value.ToString();
            }

            if (Lst_SubCat.SelectedItem.Value.ToString() != "0")
            {
                sql = sql + "and tbl_items.Id_Subcategoria = " + Lst_SubCat.SelectedItem.Value.ToString();
            }

            if (Lst_Letra.SelectedItem.Value.ToString() != "0")
            {
                sql = sql + "and tbl_items.Sigla = '" + Lst_Letra.SelectedItem.Value.ToString() + "'";
            }

            if (Lst_LV.SelectedItem.Value.ToString() != "0")
            {
                sql = sql + "and tbl_items.Id_Linea_Venta = '" + Lst_LV.SelectedItem.Value.ToString() + "'";
            }

            if (Lst_division.SelectedItem.Value.ToString() != "0")
            {
                sql = sql + "and tbl_Familias_Productos.Id_Familia = '" + Lst_division.SelectedItem.Value.ToString() + "'";
            }

            if (Lst_Prov.SelectedItem.Value.ToString() != "0")
            {
                sql = sql + "and tbl_items.Id_proveedor = " + Lst_Prov.SelectedItem.Value.ToString();
            }

            if (Chk_sin_cat.Checked)
            {
                sql = sql + "and isnull(tbl_items.Id_Categoria,0) = 0 ";
            }

            if (Chk_publicados.Checked)
            {
                sql = sql + " and tbl_Items.Publicar_Web = 1";
            }

            if (Chk_sin_cat.Checked)
            {
                sql = sql + " (tbl_Items.ID_Categoria = 0 or tbl_Items.ID_Categoria IS Null)";
            }

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                    DataSet dr = new DataSet();
                    reader.Fill(dr, "tbl_clientes");
                    LstItems.DataSource = dr;
                    LstItems.DataBind();


                    //SqlCommand command = new SqlCommand(sql, connection);
                    //SqlDataReader reader = command.ExecuteReader();

                    //DataSet ds = new DataSet();

                    //DataTable table = new DataTable("items");
                    //table.Columns.Add(new DataColumn("Id", typeof(int)));
                    //table.Columns.Add(new DataColumn("Código", typeof(string)));
                    //table.Columns.Add(new DataColumn("PH", typeof(string)));
                    //table.Columns.Add(new DataColumn("Sigla", typeof(string)));
                    //table.Columns.Add(new DataColumn("Descripción", typeof(string)));
                    //table.Columns.Add(new DataColumn("Unidad", typeof(string)));
                    //table.Columns.Add(new DataColumn("ID_Familia", typeof(int)));
                    //table.Columns.Add(new DataColumn("ID_Categoria", typeof(int)));
                    //table.Columns.Add(new DataColumn("ID_SubCategoria", typeof(int)));
                    //table.Columns.Add(new DataColumn("item_web", typeof(int)));



                    //int v_id = 0;
                    //string v_codigo = "";
                    //string v_ph = "";
                    //string v_sigla = "";
                    //string v_descripcion = "";
                    //string v_unidad = "";
                    //int v_id_fam = 0;
                    //int v_id_cat = 0;
                    //int v_id_subcat = 0;
                    //int v_web = 0;


                    //while (reader.Read())
                    //{

                   
                    //    v_id = reader.GetInt32(0);
                    //    v_codigo = reader.GetString(1);
                    //    v_ph = reader.GetString(2);
                    //    v_sigla = reader.GetString(3);
                    //    v_descripcion = reader.GetString(4);
                    //    v_unidad = reader.GetString(5);
                    //    v_id_fam = reader.GetInt32(6);
                    //    v_id_cat = reader.GetInt32(7);
                    //    v_id_subcat = reader.GetInt32(8);
                    //    v_web = reader.GetInt32(9);

                    //    table.Rows.Add(v_id,
                    //                   v_codigo,
                    //                   v_ph,
                    //                   v_sigla,
                    //                   v_descripcion,
                    //                   v_unidad,
                    //                   v_id_fam,
                    //                   v_id_cat,
                    //                   v_id_subcat,
                    //                   v_web);
                    //}
                    
                   // LstItems.DataSource = table;
                    //LstItems.DataBind();

                    connection.Close();
                    connection.Dispose();
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    connection.Close();
                    connection.Dispose();
                }
            }

        }

        protected void Lst_Cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = Lst_Cat.SelectedValue.ToString();
            Lst_SubCat.Items.Clear();
            Lst_SubCat.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria = " + valor + " order by nombre ", Lst_SubCat, "tbl_Subcategorias", "ID_SubCategoria", "Nombre");
        }

        protected void Lst_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = Lst_division.SelectedValue.ToString();
            Lst_Cat.Items.Clear();
            Lst_Cat.Items.Add(new ListItem("Seleccione", "0", true));


            Lst_SubCat.Items.Clear();
            Lst_SubCat.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_Categoria, Nombre from tbl_categorias where Activo = 1 and Id_Familia = " + valor + " order by nombre", Lst_Cat, "tbl_categorias", "ID_Categoria", "Nombre");
        }

        protected void LstItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                DropDownList ddlCategories = e.Row.FindControl("LstUnidadVenta") as DropDownList;
                if (ddlCategories != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    
                    carga_contrl_lista("select ID_Unidad_Venta, Nombre from tbl_Unidad_Venta where Activo = 1 ", ddlCategories, "tbl_Unidad_Venta", "ID_Unidad_Venta", "Nombre");
                   // lbl_error.Text = drv["Unidad"].ToString();

                    foreach (ListItem item in ddlCategories.Items)
                    {
                        if (item.Text == drv["Unidad"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                   // ddlCategories.SelectedValue = drv["Unidad"].ToString();
                }

                DropDownList ddlFamilias = e.Row.FindControl("LstFamilias") as DropDownList;
                if (ddlFamilias != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    lbl_error.Text = drv["Id_SubCategoria"].ToString();
                    carga_contrl_lista("select id_familia, nombre from tbl_Familias_Productos where Activo = 1 order by nombre", ddlFamilias, "tbl_Familias_Productos", "id_familia", "Nombre");

                    foreach (ListItem item in ddlFamilias.Items)
                    {
                        if (item.Value == drv["Id_Familia"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    // ddlCategories.SelectedValue = drv["Unidad"].ToString();
                }


                DropDownList ddlCategorias = e.Row.FindControl("LstCategorias") as DropDownList;
                if (ddlCategorias != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    
                    carga_contrl_lista("select ID_Categoria, Nombre from tbl_categorias where Activo = 1 and Id_Familia = " + drv["Id_Familia"].ToString() + " order by nombre", ddlCategorias, "tbl_categorias", "ID_Categoria", "Nombre");
                    //lbl_error.Text = drv["Id_Categoria"].ToString();

                    foreach (ListItem item in ddlCategorias.Items)
                    {
                        if (item.Value == drv["Id_Categoria"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    // ddlCategories.SelectedValue = drv["Unidad"].ToString();
                }


                DropDownList ddlSubCategorias = e.Row.FindControl("LstSubCategorias") as DropDownList;
                if (ddlSubCategorias != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria = " + drv["ID_Categoria"].ToString() + " order by nombre ", ddlSubCategorias, "tbl_Subcategorias", "ID_SubCategoria", "Nombre");

                    foreach (ListItem item in ddlSubCategorias.Items)
                    {
                        if (item.Value == drv["Id_SubCategoria"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    // ddlCategories.SelectedValue = drv["Unidad"].ToString();
                }

                DropDownList ddlLetras = e.Row.FindControl("LstLetras") as DropDownList;
                if (ddlLetras != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    carga_contrl_lista("select 'A' id_lista, 'A' letra union all select 'B' id_lista, 'B' letra union all select 'C' id_lista, 'C' letra union all select 'D' id_lista, 'D' letra union all select 'E' id_lista, 'E' letra union all select 'F' id_lista, 'F' letra union all select 'G' id_lista, 'G' letra", ddlLetras, "tbl_letras", "id_lista", "letra");
                    lbl_error.Text = drv["Letra"].ToString();

                    foreach (ListItem item in ddlLetras.Items)
                    {
                        if (item.Value == drv["Letra"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    // ddlCategories.SelectedValue = drv["Unidad"].ToString();
                }

                CheckBox ddpublicar = e.Row.FindControl("Chk_publicar") as CheckBox;
                if (ddpublicar != null)
                {
                    //Get the data from DB and bind the dropdownlist
                   
                    lbl_error.Text = drv["item_web"].ToString();

                    if  (drv["item_web"].ToString() == "0")
                    {
                        ddpublicar.Checked = false;
                    }
                    else
                    {
                        ddpublicar.Checked = true;
                    }
                }




                
            }
        }

    
    }
}