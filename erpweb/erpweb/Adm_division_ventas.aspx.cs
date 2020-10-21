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
            sql = sql + "from tbl_Familias_Productos where Activo = 1 ";
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
            lbl_status.Text = row.Cells[2].Text;
            carga_subcategorias(Convert.ToInt32(row.Cells[2].Text));
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
    }
}