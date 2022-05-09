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
    public partial class Adm_division_ventas : System.Web.UI.Page
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
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            Sserver = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("SSERVER"));
            SMysql = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("MYSQL"));
            if (Session["Usuario"].ToString() == "" || Session["Usuario"] == null)
            {
                Response.Redirect("Ppal.aspx");
            }
            else
            {
                if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_009_10", Sserver) == "NO")
                {
                    Response.Redirect("ErrorAcceso.html");
                }
               /// lbl_conectado.Text = Session["Usuario"].ToString();
            }
            if (!this.IsPostBack)
            {
                // Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
                carga_divisiones();
            }
        }

        void carga_divisiones()
        {
            string sql = ""; // "select 0 ID_Usuario, 'Seleccione Vendedor' vendedor union all select ID_usuario, CONCAT(Apellido_Usu,' ', Nombre_Usu) vendedor from tbl_Usuarios where Activo = 1 order by Apellido_Usu";
            sql = "select ID_Familia ID, CONCAT('(',Codigo,')',' ', Nombre) Division, IIF(activo=1,'SI','NO') Activo from  tbl_Familias_Productos where Activo = 1 ";
            sql = "select ID_Familia ID, CONCAT('(', Codigo, ')', ' ', Nombre) Division, IIF(activo = 1, 'SI', 'NO') Activo, ";
            sql = sql + "(select COUNT(1) from tbl_Categorias where Id_Familia = tbl_Familias_Productos.ID_Familia) Categorias, ";
            sql = sql + "(select COUNT(distinct ct.id_categoria) from tbl_Items_web iw ";
            sql = sql + "inner join tbl_Categorias ct on ct.ID_Categoria = iw.Id_Categoria ";
            sql = sql + "where iw.publicado_sitio = 1  and ct.Id_Familia = tbl_Familias_Productos.ID_Familia) Publicadas ";
            sql = sql + "from tbl_Familias_Productos ";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    //SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                    DataSet dr = new DataSet();
                    reader.Fill(dr, "tbl_clientes");
                    Lst_division.DataSource = dr;
                    Lst_division.DataBind();

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

        protected void Lst_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Lst_division.SelectedRow;
            //lbl_status.Text = row.Cells[1].Text;
            lbl_familia.Text = row.Cells[2].Text;
            id_familia = Convert.ToInt32(row.Cells[1].Text);

            LstCategorias.DataSource = null;
            LstCategorias.DataBind();

            LstSubCategorias.DataSource = null;
            LstSubCategorias.DataBind();
        
            carga_categorias(Convert.ToInt32(row.Cells[1].Text));
        }


        void carga_categorias(int id_familia)
        {
            string sql = "";
            sql = "select ID_Categoria ID, CONCAT('(',Codigo,')',' ', Nombre) Categoria, IIF(activo=1,'SI','NO') Activo from tbl_Categorias where Id_Familia = " + id_familia;

            sql = "select ID_Categoria ID, ";
            sql = sql + "CONCAT('(', Codigo, ')', ' ', Nombre) Categoria, ";
            sql = sql + "IIF(activo = 1, 'SI', 'NO') Activo, ";
            sql = sql + "(select COUNT(1) from tbl_Subcategorias where Id_Categoria = tbl_Categorias.Id_Categoria) SubCategorias, ";
            sql = sql + "(select COUNT(distinct ct.id_categoria) from tbl_Items_web iw ";
            sql = sql + "inner join tbl_Categorias ct on ct.ID_Categoria = iw.Id_Categoria  where iw.publicado_sitio = 1 ";
            sql = sql + "and ct.ID_Categoria = tbl_Categorias.ID_Categoria) Publicadas ";
            sql = sql + "from tbl_Categorias where Id_Familia = " + id_familia;
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    //SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                    DataSet dr = new DataSet();
                    reader.Fill(dr, "tbl_Categorias");
                    LstCategorias.DataSource = dr;
                    LstCategorias.DataBind();

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

        protected void LstCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = LstCategorias.SelectedRow;
            lbl_categoria.Text = row.Cells[2].Text;
            id_categoria = Convert.ToInt32(row.Cells[1].Text);
            carga_subcategorias(Convert.ToInt32(row.Cells[1].Text));
        }

        void carga_subcategorias(int id_categoria)
        {
            string sql = "";
            sql = "select ID_SubCategoria ID, Nombre Subacategoria, IIF(activo=1,'SI','NO') Activo from tbl_SubCategorias where Id_Categoria = " + id_categoria;

            sql = "select ID_SubCategoria ID, Nombre Subacategoria, IIF(activo = 1, 'SI', 'NO') Activo, ";
            sql = sql + "(select COUNT(1) from tbl_Items where Id_Categoria = tbl_SubCategorias.Id_Categoria and Id_SubCategoria = tbl_SubCategorias.Id_SubCategoria) Productos, ";
            sql = sql + "(select COUNT(1) from tbl_Items_web where Id_Categoria = tbl_SubCategorias.Id_Categoria and Id_SubCategoria = tbl_SubCategorias.Id_SubCategoria) Publicados ";
            sql = sql + "from tbl_SubCategorias where Id_Categoria = " + id_categoria;
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    //SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                    DataSet dr = new DataSet();
                    reader.Fill(dr, "tbl_SubCategorias");
                    LstSubCategorias.DataSource = dr;
                    LstSubCategorias.DataBind();

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

        protected void LstSubCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = LstSubCategorias.SelectedRow;
            id_subcategoria = Convert.ToInt32(row.Cells[1].Text);
            lbl_subcategoria.Text = row.Cells[2].Text;

            carga_productos(id_familia, id_categoria, id_subcategoria);
        }

        void carga_productos(int d_familia, int id_categoria, int id_subcategoria)
        {
            string sql = "";
            sql = "SELECT dbo.tbl_Items.Id_Linea_Venta, dbo.tbl_Familias_Productos.ID_Familia,  dbo.tbl_Items.Id_Categoria, dbo.tbl_Items.Id_SubCategoria, Publicar_Web,  ";
            sql = sql + "Ultimo_Factor, Ultimo_Precio , Fecha_precio, dbo.tbl_Items.Sigla,  dbo.tbl_Items.Precio,  dbo.tbl_Items.ID_Item, dbo.tbl_Items.Codigo, dbo.tbl_Items.Descripcion, ";
            sql = sql + "dbo.tbl_Categorias.Nombre AS Categoria, ";
            sql = sql + "dbo.tbl_Subcategorias.Nombre AS SubCategoria, dbo.tbl_Proveedores.Nombre_Fantasia AS Proveedor, dbo.tbl_Items.Unidad, dbo.tbl_Items.Activo, ";
            sql = sql + "dbo.tbl_Items.Cod_Barra ";
            sql = sql + "FROM  dbo.tbl_Items with(nolock) LEFT OUTER JOIN ";
            sql = sql + "dbo.tbl_Categorias ON dbo.tbl_Items.Id_Categoria = dbo.tbl_Categorias.ID_Categoria LEFT OUTER JOIN ";
            sql = sql + "dbo.tbl_Subcategorias ON dbo.tbl_Items.Id_SubCategoria = dbo.tbl_Subcategorias.ID_SubCategoria LEFT OUTER JOIN ";
            sql = sql + "dbo.tbl_Proveedores ON dbo.tbl_Items.Id_proveedor = dbo.tbl_Proveedores.ID_Proveedor LEFT OUTER JOIN ";
            sql = sql + "dbo.tbl_Familias_Productos ON dbo.tbl_Familias_Productos.ID_Familia = dbo.tbl_categorias.Id_Familia ";
            sql = sql + "WHERE dbo.tbl_Familias_Productos.ID_Familia =  " + id_familia;
            sql = sql + "AND dbo.tbl_Items.Id_Categoria = " + id_categoria ;
            sql = sql + "AND dbo.tbl_Items.Id_SubCategoria =  "  + id_subcategoria;
            

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                    DataSet dr = new DataSet();
                    reader.Fill(dr, "tbl_Items");
                    LstItems.DataSource = dr;
                    LstItems.DataBind();

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

        //protected void Lst_division_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        cat_familia++;
        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {

        //        string totalAmtFinanced = ((Label)Lst_division.FooterRow.FindControl("lbl_total_familias")).Text;
        //        Dim lbl As Label = DirectCast(e.Row.FindControl("lblTotal"), Label)
        //        totalAmtFinanced = Convert.ToString(cat_familia);
        //    }
        //}
    }
}