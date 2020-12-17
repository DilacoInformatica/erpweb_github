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
            sql = "SELECT  dbo.tbl_Items.Id_Linea_Venta, dbo.tbl_Items.Id_Categoria, dbo.tbl_Items.Id_SubCategoria, Publicar_Web, Ultimo_Factor, Ultimo_Precio , Fecha_precio, dbo.tbl_Items.Sigla,  dbo.tbl_Items.Precio,   dbo.tbl_Items.ID_Item, dbo.tbl_Items.Codigo, dbo.tbl_Items.Descripcion, dbo.tbl_Categorias.Nombre AS Categoria, ";
            sql = sql + " dbo.tbl_Subcategorias.Nombre AS SubCategoria, dbo.tbl_Proveedores.Nombre_Fantasia AS Proveedor, dbo.tbl_Items.Unidad, dbo.tbl_Items.Activo, ";
            sql = sql + " dbo.tbl_Items.Cod_Barra";
            sql = sql + " FROM dbo.tbl_Items LEFT OUTER  JOIN";
            sql = sql + " dbo.tbl_Categorias ON dbo.tbl_Items.Id_Categoria = dbo.tbl_Categorias.ID_Categoria LEFT OUTER JOIN";
            sql = sql + " dbo.tbl_Subcategorias ON dbo.tbl_Items.Id_SubCategoria = dbo.tbl_Subcategorias.ID_SubCategoria LEFT OUTER JOIN";
            sql = sql + "  dbo.tbl_Proveedores ON dbo.tbl_Items.Id_proveedor = dbo.tbl_Proveedores.ID_Proveedor";
            sql = sql + " where 1 = 1 ";
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
    
 }
}