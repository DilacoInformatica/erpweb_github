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
        string archivo2 = "";
        string extension = "";
        string nuevo_nom = "";
        string master_queryString = "";
        // FTP
        string server = @"ftp://dev.dilaco.com/";
        string user = "dev@dilaco.com";
        string password = "4ydlrvyKUX8}";
        string usuario = "";
        HttpContext context = HttpContext.Current;

        DataTable lista_errores = new DataTable();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            ImgBtn_Cerrar.Attributes["Onclick"] = "return salir();";
            Btn_Transpaso_Masivo.Attributes["Onclick"] = "return confirm('Ud está a punto de realizar un transpaso masivo de productos a la página Web, Seguro desea proceder?')";
            if (String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                usuario = "2"; // mi usuarios por default mientras no nos conectemos al servidor
            }
            else
            {
                usuario = Request.QueryString["usuario"].ToString();
            }
            ruta_alterna = utiles.retorna_ruta();

            if (!this.IsPostBack)
            {
                carga_contrl_lista("select 0 ID_Categoria, 'Seleccione Categoría' Nombre union all select ID_Categoria, Nombre from tbl_categorias where Activo = 1", LstCategorias, "tbl_categorias", "ID_Categoria", "Nombre");
                carga_contrl_lista("select 0 ID_Linea_Venta, 'Seleccione Línea Venta' Nombre union all select ID_Linea_Venta, CONCAT(Cod_Linea_Venta, ' ', Nombre) Nombre from tbl_Lineas_Venta where Activo = 1", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                carga_contrl_lista("select 0 ID_Proveedor, 'Seleccione Proveedor' Razon_Social union all  select ID_Proveedor, substring(Razon_Social,1,50) Razon_Social from tbl_Proveedores where Activo = 1", LstProveedores, "tbl_Proveedores", "ID_Proveedor", "Razon_Social");
                carga_contrl_lista("select 0 ID_SubCategoria, 'Seleccione Subcategoría' Nombre union all select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1", LstSubCategorias, "tbl_categorias", "ID_SubCategoria", "Nombre");
                carga_contrl_lista("select ' ' id_lista, 'Selecione Letra' letra union all select 'A' id_lista, 'A' letra union all select 'B' id_lista, 'B' letra union all select 'C' id_lista, 'C' letra union all select 'D' id_lista, 'D' letra union all select 'E' id_lista, 'E' letra union all select 'F' id_lista, 'F' letra union all select 'G' id_lista, 'G' letra", LstLetras, "tbl_letras", "id_lista", "letra");

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
                    //master_queryString = " Select tbl_items_web.id_item 'Id', ";
                    //master_queryString = master_queryString + "tbl_items_web.codigo 'Código', ";
                    //master_queryString = master_queryString + " substring(tbl_items_web.descripcion, 0, 30) 'Descripción', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.visible, 0) = 0, 'N', 'S') 'Visible', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.prodpedido, 0) = 0, 'N', 'S') 'Prod a Pedido', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.ventas, 0) = 0, 'N', 'S') 'Venta', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.cotizaciones, 0) = 0, 'N', 'S') 'Cotizacion', ";
                    //master_queryString = master_queryString + "tbl_items_web.Marca , ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Manual_Tecnico,'') = '','N',tbl_items_web.Manual_Tecnico) 'Manual técnico' , ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Presentacion_Producto,'') = '','N', tbl_items_web.Presentacion_Producto)'Presentación', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Foto,'') = '','N', tbl_items_web.Foto) 'Foto', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Foto_Grande,'') = '','N',tbl_items_web.Foto_Grande) 'Foto Grande', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.video,'') = '','N',video) 'Video', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Hoja_de_Seguridad,'') = '','N',tbl_items_web.Hoja_de_Seguridad) 'Hoja Seguridad', ";
                    //master_queryString = master_queryString + "IIF(isnull(tbl_items_web.publicado_sitio,0) = 0,'N','S') 'Publicado' ";
                    // master_queryString = master_queryString + "from tbl_items_web with(nolock) inner join tbl_items on tbl_items.ID_Item = tbl_items_web.Id_Item where 1 = 1  ";

                    master_queryString = " Select tbl_items_web.id_item 'Id', ";
                    master_queryString = master_queryString + "tbl_items_web.codigo 'Código', ";
                    master_queryString = master_queryString + " substring(tbl_items_web.descripcion, 0, 30) 'Descripción', ";
                    master_queryString = master_queryString + "tbl_items_web.Marca , ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.visible, 0) = 0, 'N', 'S') 'Visible', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.prodpedido, 0) = 0, 'N', 'S') 'Prod a Pedido', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.ventas, 0) = 0, 'N', 'S') 'Venta', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.cotizaciones, 0) = 0, 'N', 'S') 'Cotizacion', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Manual_Tecnico,'') = '','N','S') 'Manual técnico' , ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Presentacion_Producto,'') = '','N', 'S')'Presentación', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Foto,'') = '','N', 'S') 'Foto', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Foto_Grande,'') = '','N','S') 'Foto Grande', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.video,'') = '','N','S') 'Video', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.Hoja_de_Seguridad,'') = '','N','S') 'Hoja Seguridad', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.publicado_sitio,0) = 0,'N','S') 'Publicado', ";
                    master_queryString = master_queryString + "IIF(isnull(tbl_items_web.activo,0) = 0,'N','S') 'Activo' ";
                    master_queryString = master_queryString + "from tbl_items_web with(nolock) inner join tbl_items on tbl_items.ID_Item = tbl_items_web.Id_Item where 1 = 1  ";

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
                        master_queryString = master_queryString + "and tbl_items.Id_Categoria = " + LstCategorias.SelectedItem.Value.ToString();
                    }

                    if (LstSubCategorias.SelectedItem.Value.ToString() != "0")
                    {
                        master_queryString = master_queryString + "and tbl_items.Id_Subcategoria = " + LstSubCategorias.SelectedItem.Value.ToString();
                    }

                    if (LstLetras.SelectedItem.Value.ToString() != " ")
                    {
                        master_queryString = master_queryString + "and tbl_items.Sigla = '" + LstLetras.SelectedItem.Value.ToString() + "'";
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
                        master_queryString = master_queryString + "and len(isnull(tbl_items_web.Foto, 0)) + len(isnull(tbl_items_web.Foto_Grande, 0)) = 0 ";
                    }
                }
                connection.Open();
                
                SqlDataAdapter reader = new SqlDataAdapter(master_queryString, connection);
                DataSet ds = new DataSet();
                reader.Fill(ds, "tbl_items_web");

                Productos.DataSource = ds;
                Productos.DataBind();
                Productos.DataMember = "tbl_items_web";

                //SqlCommand command = new SqlCommand(queryString, connection);
                //SqlDataReader reader = command.ExecuteReader();

                //DataSet ds = new DataSet();



                //DataTable table = new DataTable("items");
                //table.Columns.Add(new DataColumn("Id", typeof(int)));
                //table.Columns.Add(new DataColumn("Código", typeof(string)));
                //table.Columns.Add(new DataColumn("Descripción", typeof(string)));
                //table.Columns.Add(new DataColumn("Visible", typeof(string)));
                //table.Columns.Add(new DataColumn("Prod a Pedido", typeof(string)));
                //table.Columns.Add(new DataColumn("Venta", typeof(string)));
                //table.Columns.Add(new DataColumn("Cotizacion", typeof(string)));
                //table.Columns.Add(new DataColumn("Publicado", typeof(string)));


                //int v_id = 0;
                //string v_codigo = "";
                //string v_descripcion = "";
                //string v_visible = "";
                //string v_pedido = "";
                //string v_ventas = "";
                //string v_cotiza = "";
                //string v_publi = "";
                //string v_swc = "";

                //Transpaso a Mysql
                //while (reader.Read())
                //{
                //    publica = validamysql(reader.GetInt32(0));
                //    cont++;

                //    if (publica == 0)
                //    {
                //        v_swc = "N";
                //    }
                //    else
                //    {
                //        v_swc = "S";
                //    }

                //    v_id = reader.GetInt32(0);
                //    v_codigo = reader.GetString(1);
                //    v_descripcion = reader.GetString(1);
                //    v_visible = reader.GetString(3);
                //    v_pedido = reader.GetString(4);
                //    v_ventas = reader.GetString(5);
                //    v_cotiza = reader.GetString(6);
                //    v_publi = v_swc;


                //    table.Rows.Add(v_id,
                //                   v_codigo,
                //                   v_descripcion,
                //                   v_visible,
                //                   v_pedido,
                //                   v_ventas,
                //                   v_cotiza,
                //                   v_publi);
                //}

                //LstProductos.DataSource = table;
                //LstProductos.DataBind();



                lbl_cantidad.Text ="Cantidad de Registros: " + Convert.ToString(Productos.Rows.Count);
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
            GridResultados.Visible = false;
            carga_productos(txt_codigo.Text);
            context.Session["SQL"] = master_queryString;
            //if (txt_codigo.Text == "")
            //{
            //    lbl_error.Text = "Debe indicar código a buscar";
            //}
            //else
            //{
            //    carga_productos(txt_codigo.Text);
            //}
        }

        protected void Productos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Productos.SelectedRow;
            Response.Redirect("Items_detalle.aspx?id_item="+ row.Cells[1].Text + "&usuario="+ usuario +"&modo='W'");
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

        protected void Btn_Transpaso_Masivo_Click(object sender, EventArgs e)
        {
            lista_errores.Columns.Add("Información");
            lista_errores.Columns.Add("Resultado");
            GridResultados.DataSource = lista_errores;
            lista_errores.Clear();
            GridResultados.DataBind();
            GridResultados.Visible = false;
            string query = "";
            string ruta_server = "";
            string ruta_local = "";
            string descripcion = "";
            int ins = 0;
            int upd = 0;
            int err = 0;
            int arc = 0;
            int are = 0;
            ClsFTP ftp = new ClsFTP();
            Page.Validate();
            if (Page.IsValid)
            {
                // Comenzamos con el transpaso masivo de información al Servidor Web
                query = "SELECT tbl_items_web.Id_Item "; // 0
                query = query + ",tbl_items_web.Codigo "; // 1
                query = query + ",isnull(tbl_items_web.habilitado_venta,0) habilitado_venta  "; // 2
                query = query + ",isnull(tbl_items_web.prodpedido,0) prodpedido "; //3
                query = query + ",isnull(tbl_items_web.visible,0) visible "; //4
                query = query + ",isnull(tbl_items_web.cotizaciones,0) cotizaciones "; //5
                query = query + ",isnull(tbl_items_web.ventas,0) ventas "; //6
                query = query + ",tbl_items_web.Texto_Destacado "; //7 
                query = query + ",tbl_items_web.codigo_maestro "; //8
                query = query + ",tbl_items_web.Texto_maestro "; //9
                query = query + ",tbl_items_web.Descripcion_maestro "; //10
                query = query + ",tbl_items_web.Descripcion "; // 11
                query = query + ",isnull(tbl_items_web.Id_Categoria,0) Id_Categoria "; // 12
                query = query + ",isnull(tbl_items_web.Id_SubCategoria,0) Id_SubCategoria "; //13
                query = query + ",isnull(tbl_items_web.Id_Linea_Venta,0) Id_Linea_Venta "; //14
                query = query + ",isnull(tbl_items_web.Id_proveedor,0) id_proveedor "; //15
                query = query + ",tbl_items_web.Marca "; //16
                query = query + ",isnull(tbl_items_web.Precio, 0) Precio "; // 17
                query = query + ",isnull(tbl_items_web.Id_moneda,0) id_moneda "; // 18
                query = query + ",tbl_items_web.unidad_vta ";  // 19
                query = query + ",tbl_items_web.Codigo_prov "; //20
                query = query + ",tbl_items_web.Caracteristicas "; //21
                query = query + ",tbl_items_web.Manual_Tecnico "; //22
                query = query + ",tbl_items_web.Presentacion_Producto  "; //23
                query = query + ",tbl_items_web.Foto "; //24
                query = query + ",tbl_items_web.Foto_Grande "; //25
                query = query + ",tbl_items_web.Video "; //26
                query = query + ",tbl_items_web.Producto_Nuevo "; //27
                query = query + ",tbl_items_web.Producto_Oferta "; //28
                query = query + ",isnull(tbl_items_web.Id_Accesorio1,0) Id_Accesorio1 "; //29
                query = query + ",isnull(tbl_items_web.Id_Accesorio2,0) Id_Accesorio2 "; //30
                query = query + ",isnull(tbl_items_web.Id_Accesorio3,0) Id_Accesorio3 "; //31
                query = query + ",isnull(tbl_items_web.Id_Repuesto1,0) Id_Repuesto1 "; //32
                query = query + ",isnull(tbl_items_web.Id_Repuesto2,0) Id_Repuesto2 "; //33
                query = query + ",isnull(tbl_items_web.Id_Repuesto3,0) Id_Repuesto3 "; //34
                query = query + ",isnull(tbl_items_web.Id_Alternativa1,0) Id_Alternativa "; //35
                query = query + ",isnull(tbl_items_web.Id_Alternativa2,0) Id_Alternativa2 "; //36
                query = query + ",isnull(tbl_items_web.Id_Alternativa3,0) Id_Alternativa3 "; //37
                query = query + ",isnull(tbl_items_web.Id_Categoria1,0) Id_Categoria "; //38
                query = query + ",isnull(tbl_items_web.Id_Categoria2,0) Id_Categoria2 "; //39
                query = query + ",isnull(tbl_items_web.Id_Categoria3,0) Id_Categoria3 "; //40
                query = query + ",isnull(tbl_items_web.Id_SubCategoria1,0) Id_SubCategoria1 "; //41
                query = query + ",isnull(tbl_items_web.Id_SubCategoria2,0) Id_SubCategoria2 "; //42
                query = query + ",isnull(tbl_items_web.Id_SubCategoria3,0) Id_SubCategoria3 "; //43
                query = query + ",tbl_items_web.Tabla_Tecnica "; //44
                query = query + ",tbl_items_web.Hoja_de_Seguridad "; //45
                query = query + ",pr.Nombre_Fantasia "; //46
                query = query + "FROM tbl_Items_web with(nolock) ";
                query = query + "left outer join tbl_Categorias ct on ct.ID_Categoria = tbl_items_web.Id_Categoria ";
                query = query + "left outer join tbl_Subcategorias sb on sb.ID_SubCategoria = tbl_items_web.Id_SubCategoria ";
                query = query + "left outer join tbl_Proveedores pr on pr.ID_Proveedor = tbl_items_web.Id_proveedor ";
                query = query + "left outer join tbl_Monedas mn on mn.ID_Moneda = tbl_items_web.Id_moneda ";
                query = query + "inner join tbl_items on tbl_items.ID_Item = tbl_items_web.Id_Item where 1 = 1 ";

                if (txt_codigo.Text != "")
                {
                    query = query + "and  tbl_items_web.codigo like  '" + txt_codigo.Text + "%'";
                }

                if (txt_codprov.Text != "")
                {
                    query = query + "and tbl_items.Codigo_prov like  '" + txt_codprov.Text + "%'";
                }

                if (LstCategorias.SelectedItem.Value.ToString() != "0")
                {
                    query = query + "and tbl_items.Id_Categoria = " + LstCategorias.SelectedItem.Value.ToString();
                }

                if (LstSubCategorias.SelectedItem.Value.ToString() != "0")
                {
                    query = query + "and tbl_items.Id_Subcategoria = " + LstSubCategorias.SelectedItem.Value.ToString();
                }

               // if (LstLetras.SelectedItem.Value.ToString() != " ")
               // {
               //     master_queryString = master_queryString + "and tbl_items.Sigla = '" + LstLetras.SelectedItem.Value.ToString() + "'";
              //  }


                if (LstProveedores.SelectedItem.Value.ToString() != "0")
                {
                    query = query + "and tbl_items.Id_proveedor = " + LstProveedores.SelectedItem.Value.ToString();
                }

                if (chk_sin_cat.Checked)
                {
                    query = query + "and isnull(tbl_items.Id_Categoria,0) = 0 ";
                }

                if (chk_no_publicados.Checked)
                {
                    query = query + "and isnull(tbl_items.publicado_sitio,0) = 0 ";
                }

                if (chk_publicados.Checked)
                {
                    query = query + "and isnull(tbl_items.publicado_sitio,0) = 1 ";
                }

                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);

                        SqlDataReader reader = command.ExecuteReader();
                        // Transpaso a Mysql
                        while (reader.Read())
                        {
                            // Verficamos que exista el item en el servidor web
                            if (validamysql(reader[0].ToString()))
                            {

                                descripcion = reader[11].ToString();

                                if (descripcion.Length > 101)
                                {
                                    descripcion = descripcion.Substring(0, 100);
                                }
                                upd++;
                                query = "UPDATE dilacocl_dilacoweb.tbl_items ";
                                query = query + "SET ";
                                query = query + " descripcion = '" + Context.Server.HtmlDecode(descripcion) + "'";
                                query = query + ",prodpedido = " + reader[3].ToString();
                                query = query + ",visible = " + reader[4].ToString();
                                query = query + ",cotizaciones = " + reader[5].ToString();
                                query = query + ",ventas = " + reader[6].ToString();
                                query = query + ",texto_destacado = '" + Context.Server.HtmlDecode(reader[7].ToString().Replace(",", ".").Trim() + "'");
                                query = query + ",Id_Categoria = " + reader[12].ToString();
                                query = query + ",Id_Subcategoria = " + reader[13].ToString();
                                query = query + ",Id_Linea_Venta = " + reader[14].ToString();
                                query = query + ",proveedor = '" + reader[46].ToString().Replace(",", ".").Trim() + "'";
                                query = query + ",Marca = '" + Context.Server.HtmlDecode(reader[16].ToString().Replace(",", ".").Trim() + "'");
                                query = query + ",precio = " + reader[17].ToString().Replace(",", ".");
                                query = query + ",id_moneda = " + reader[18].ToString();
                                query = query + ",Unidad_vta = '" + reader[19].ToString() + "'";
                                query = query + ",Codigo_prov = '" + Context.Server.HtmlDecode(reader[20].ToString() + "'");
                                query = query + ",Caracteristicas = '" + Context.Server.HtmlDecode(reader[21].ToString().Replace(",", ".").Trim() + "'");
                                query = query + ",Manual_tecnico = '" + reader[22].ToString() + "'";
                                query = query + ",Presentacion_producto = '" + reader[23].ToString() + "'";
                                query = query + ",Hoja_de_Seguridad = '" + reader[45].ToString() + "'";
                                query = query + ",Foto = '" + reader[24].ToString() + "'";
                                query = query + ",Foto_grande = '" + reader[25].ToString() + "'";
                                query = query + ",Video = '" + reader[26].ToString() + "'";
                                query = query + ",Id_Accesorio1 = " + reader[29].ToString();
                                query = query + ",Id_Accesorio2 = " + reader[30].ToString();
                                query = query + ",Id_Accesorio3 = " + reader[31].ToString();
                                query = query + ",Id_Repuesto1 = " + reader[32].ToString();
                                query = query + ",Id_Repuesto2 = " + reader[33].ToString();
                                query = query + ",Id_Repuesto3 = " + reader[34].ToString();
                                query = query + ",Id_Alternativa1 = " + reader[35].ToString();
                                query = query + ",Id_Alternativa2 = " + reader[36].ToString();
                                query = query + ",Id_Alternativa3 = " + reader[37].ToString();
                                query = query + ",Id_categoria1 = " + reader[38].ToString();
                                query = query + ",Id_categoria2 = " + reader[39].ToString();
                                query = query + ",Id_categoria3 = " + reader[40].ToString();
                                query = query + ",Id_subcategoria1 = " + reader[41].ToString();
                                query = query + ",Id_subcategoria2 = " + reader[42].ToString();
                                query = query + ",Id_subcategoria3 = " + reader[43].ToString();
                                query = query + ",tabla_tecnica ='" + Context.Server.HtmlDecode(reader[44].ToString().Replace(",", ".").Trim() + "'");
                                query = query + "  WHERE Id_Item = " + reader[0].ToString();
                            }
                            else
                            {

                                descripcion = reader[11].ToString();

                                if (descripcion.Length > 101)
                                {
                                    descripcion = descripcion.Substring(0, 100);
                                }
                                ins++;
                                query = "INSERT INTO dilacocl_dilacoweb.tbl_items ";
                                query = query + "(Id_Item, ";
                                query = query + "codigo, ";
                                query = query + "descripcion, ";
                                query = query + "prodpedido, ";
                                query = query + "visible, ";
                                query = query + "cotizaciones, ";
                                query = query + "ventas, ";
                                query = query + "texto_destacado, ";
                                query = query + "Id_Categoria, ";
                                query = query + "Id_Subcategoria, ";
                                query = query + "Id_Linea_Venta, ";
                                query = query + "proveedor, ";
                                query = query + "Marca, ";
                                query = query + "precio, ";
                                query = query + "id_moneda, ";
                                query = query + "Unidad_vta, ";
                                query = query + "codigo_prov,";
                                query = query + "Caracteristicas, ";
                                query = query + "Manual_tecnico, ";
                                query = query + "Presentacion_producto, ";
                                query = query + "Hoja_de_Seguridad, ";
                                query = query + "Foto, ";
                                query = query + "Foto_grande, ";
                                query = query + "Video, ";
                                query = query + "Id_Accesorio1, ";
                                query = query + "Id_Accesorio2, ";
                                query = query + "Id_Accesorio3, ";
                                query = query + "Id_Repuesto1, ";
                                query = query + "Id_Repuesto2, ";
                                query = query + "Id_Repuesto3, ";
                                query = query + "Id_Alternativa1, ";
                                query = query + "Id_Alternativa2, ";
                                query = query + "Id_Alternativa3, ";
                                query = query + "Id_categoria1, ";
                                query = query + "Id_categoria2, ";
                                query = query + "Id_categoria3, ";
                                query = query + "Id_subcategoria1, ";
                                query = query + "Id_subcategoria2, ";
                                query = query + "Id_subcategoria3, ";
                                query = query + "tabla_tecnica) ";
                                query = query + "VALUES ";
                                query = query + "(" + reader[0].ToString() + ",";
                                query = query + "'" + reader[1].ToString().Replace(",", ".").Trim() + "',";
                                query = query + "'" + descripcion.Replace(",", ".").Trim() + "',";
                                query = query + reader[3].ToString() + ",";
                                query = query + reader[4].ToString() + ",";
                                query = query + reader[5].ToString() + ",";
                                query = query + reader[6].ToString() + ",";
                                query = query + "'" + reader[7].ToString() + "',";
                                query = query + reader[12].ToString() + ",";
                                query = query + reader[13].ToString() + ",";
                                query = query + reader[14].ToString() + ",";
                                query = query + "'" + reader[46].ToString().Replace(",", ".").Trim() + "',";
                                query = query + "'" + reader[16].ToString().Replace(",", ".").Trim() + "',";
                                query = query + reader[17].ToString().Replace(",", ".") + ",";
                                query = query + reader[18].ToString() + ",";
                                query = query + "'" + reader[19].ToString().Trim() + "',";
                                query = query + "'" + reader[20].ToString().Trim() + "',";
                                query = query + "'" + reader[21].ToString().Trim() + "',";
                                query = query + "'" + reader[22].ToString().Trim() + "',";
                                query = query + "'" + reader[23].ToString().Trim() + "',";
                                query = query + "'" + reader[45].ToString().Trim() + "',";
                                query = query + "'" + reader[24].ToString().Trim() + "',";
                                query = query + "'" + reader[25].ToString().Trim() + "',";
                                query = query + "'" + reader[26].ToString().Trim() + "',";
                                query = query + reader[29].ToString() + ",";
                                query = query + reader[30].ToString() + ",";
                                query = query + reader[31].ToString() + ",";
                                query = query + reader[32].ToString() + ",";
                                query = query + reader[33].ToString() + ",";
                                query = query + reader[34].ToString() + ",";
                                query = query + reader[35].ToString() + ",";
                                query = query + reader[36].ToString() + ",";
                                query = query + reader[37].ToString() + ",";
                                query = query + reader[38].ToString() + ",";
                                query = query + reader[39].ToString() + ",";
                                query = query + reader[40].ToString() + ",";
                                query = query + reader[41].ToString() + ",";
                                query = query + reader[42].ToString() + ",";
                                query = query + reader[43].ToString().Replace(",",".").Trim() + ",";
                                query = query + "'" + reader[44].ToString() + "')";
                            }

                            // Comenzamos la inserción de registros
                            using (MySqlConnection conn = new MySqlConnection(SMysql))
                            {
                                try
                                {
                                    conn.Open();

                                    MySqlCommand com = new MySqlCommand(query, conn);
                                    com.ExecuteNonQuery();
                                    conn.Close();
                                    conn.Dispose();

                                    //// Si el producto fue grabado correctamente, cargamos los archivos en el servidor
                                    //// Manual Técnico
                                    if (reader[22].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/manual_tecnico/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/manual_tecnico/");
                                        archivo2 = Path.Combine(ruta_alterna, reader[22].ToString().Trim());
                                        extension = Path.GetExtension(archivo2);
                                        if (File.Exists(archivo2))
                                        {
                                            nuevo_nom = "MT_" + reader[1].ToString().Replace(",", ".").Trim() + extension;
                                            if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                            {
                                                File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                                string result = ftp.Ftp(server, user, password, nuevo_nom.ToString().Trim(), ruta_local, ruta_server);
                                                if (result == "OK")
                                                {
                                                    arc++;
                                                    act_nom_archivosserver("update tbl_items_web set Manual_Tecnico = '" + nuevo_nom + "' where id_item = " + reader[0].ToString());
                                                    act_nom_archivomysql("update tbl_items set Manual_Tecnico = '" + nuevo_nom + "' where id_item = '" + reader[0].ToString());
                                                    // copiamos de vuelta el archivo generado a la ficha
                                                    File.Copy(Path.Combine(ruta_local, nuevo_nom), Path.Combine(ruta_alterna, nuevo_nom));
                                                }
                                                else
                                                {
                                                    are++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string result = ftp.Ftp(server, user, password, reader[22].ToString().Trim(), ruta_local, ruta_server);
                                            if (result == "OK")
                                            {
                                                arc++;
                                            }
                                            else
                                            {
                                                are++;
                                            }
                                        }
                                    }
                                    // Presentación
                                    if (reader[23].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/Presentacion/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/Presentacion/");
                                        archivo2 = Path.Combine(ruta_alterna, reader[23].ToString().Trim());
                                        extension = Path.GetExtension(archivo2);
                                        if (File.Exists(archivo2))
                                        {
                                            nuevo_nom = "PR_" + reader[1].ToString().Replace(",", ".").Trim() + extension;
                                            if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                            {
                                                File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                                string result = ftp.Ftp(server, user, password, nuevo_nom.ToString().Trim(), ruta_local, ruta_server);
                                                if (result == "OK")
                                                {
                                                    arc++;
                                                    act_nom_archivosserver("update tbl_items_web set Manual_Tecnico = '" + nuevo_nom + "' where id_item = " + reader[0].ToString());
                                                    act_nom_archivomysql("update tbl_items set Manual_Tecnico = '" + nuevo_nom + "' where id_item = '" + reader[0].ToString());
                                                    File.Copy(Path.Combine(ruta_local, nuevo_nom), Path.Combine(ruta_alterna, nuevo_nom));
                                                }
                                                else
                                                {
                                                    are++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string result = ftp.Ftp(server, user, password, reader[23].ToString().Trim(), ruta_local, ruta_server);
                                            if (result == "OK")
                                            {
                                                arc++;
                                            }
                                            else
                                            {
                                                are++;
                                            }
                                        }
                                    }

                                    // Hoja Seguridad
                                    if (reader[45].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/hds/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/HojaS/");
                                        archivo2 = Path.Combine(ruta_alterna, reader[45].ToString().Trim());
                                        extension = Path.GetExtension(archivo2);
                                        if (File.Exists(archivo2))
                                        {
                                            nuevo_nom = "HS_" + reader[1].ToString().Replace(",", ".").Trim() + extension;
                                            if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                            {
                                                File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
  
                                                string result = ftp.Ftp(server, user, password, nuevo_nom.ToString().Trim(), ruta_local, ruta_server);
                                                if (result == "OK")
                                                {
                                                    arc++;
                                                    act_nom_archivosserver("update tbl_items_web set Manual_Tecnico = '" + nuevo_nom + "' where id_item = " + reader[0].ToString());
                                                    act_nom_archivomysql("update tbl_items set Manual_Tecnico = '" + nuevo_nom + "' where id_item = '" + reader[0].ToString());
                                                    File.Copy(Path.Combine(ruta_local, nuevo_nom), Path.Combine(ruta_alterna, nuevo_nom));
                                                }
                                                else
                                                {
                                                    are++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string result = ftp.Ftp(server, user, password, reader[45].ToString().Trim(), ruta_local, ruta_server);
                                            if (result == "OK")
                                            {
                                                arc++;
                                            }
                                            else
                                            {
                                                are++;
                                            }
                                        }
                                    }
                                    // Foto C
                                    if (reader[24].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/img/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/img/");
                                        archivo2 = Path.Combine(ruta_alterna, reader[24].ToString().Trim());
                                        extension = Path.GetExtension(archivo2);
                                        if (File.Exists(archivo2))
                                        {
                                            nuevo_nom = "FC_" + reader[1].ToString().Replace(",", ".").Trim() + extension;
                                            if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                            {
                                                File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                                string result = ftp.Ftp(server, user, password, nuevo_nom.ToString().Trim(), ruta_local, ruta_server);
                                                if (result == "OK")
                                                {
                                                    arc++;
                                                    act_nom_archivosserver("update tbl_items_web set Manual_Tecnico = '" + nuevo_nom + "' where id_item = " + reader[0].ToString());
                                                    act_nom_archivomysql("update tbl_items set Manual_Tecnico = '" + nuevo_nom + "' where id_item = '" + reader[0].ToString());
                                                    File.Copy(Path.Combine(ruta_local, nuevo_nom), Path.Combine(ruta_alterna, nuevo_nom));
                                                }
                                                else
                                                {
                                                    are++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string result = ftp.Ftp(server, user, password, reader[24].ToString().Trim(), ruta_local, ruta_server);
                                            if (result == "OK")
                                            {
                                                arc++;
                                            }
                                            else
                                            {
                                                are++;
                                            }
                                        }
                                    }
                                    // Foto G
                                    if (reader[25].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/img/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/img/");
                                        archivo2 = Path.Combine(ruta_alterna, reader[25].ToString().Trim());
                                        extension = Path.GetExtension(archivo2);
                                        if (File.Exists(archivo2))
                                        {
                                            nuevo_nom = "FG_" + reader[1].ToString().Replace(",", ".").Trim() + extension;
                                            if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                            {
                                                File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                                string result = ftp.Ftp(server, user, password, nuevo_nom.ToString().Trim(), ruta_local, ruta_server);
                                                if (result == "OK")
                                                {
                                                    arc++;
                                                    act_nom_archivosserver("update tbl_items_web set Manual_Tecnico = '" + nuevo_nom + "' where id_item = " + reader[0].ToString());
                                                    act_nom_archivomysql("update tbl_items set Manual_Tecnico = '" + nuevo_nom + "' where id_item = '" + reader[0].ToString());
                                                    File.Copy(Path.Combine(ruta_local, nuevo_nom), Path.Combine(ruta_alterna, nuevo_nom));
                                                }
                                                else
                                                {
                                                    are++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string result = ftp.Ftp(server, user, password, reader[25].ToString().Trim(), ruta_local, ruta_server);
                                            if (result == "OK")
                                            {
                                                arc++;
                                            }
                                            else
                                            {
                                                are++;
                                            }
                                        }
                                    }

                                    // Video
                                    if (reader[26].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/Videos/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/Videos/");
                                        archivo2 = Path.Combine(ruta_alterna, reader[26].ToString().Trim());
                                        extension = Path.GetExtension(archivo2);
                                        if (File.Exists(archivo2))
                                        {
                                            nuevo_nom = "VD_" + reader[1].ToString().Replace(",", ".").Trim() + extension;
                                            if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                            {
                                                File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                                string result = ftp.Ftp(server, user, password, nuevo_nom.ToString().Trim(), ruta_local, ruta_server);
                                                if (result == "OK")
                                                {
                                                    arc++;
                                                    act_nom_archivosserver("update tbl_items_web set Manual_Tecnico = '" + nuevo_nom + "' where id_item = " + reader[0].ToString());
                                                    act_nom_archivomysql("update tbl_items set Manual_Tecnico = '" + nuevo_nom + "' where id_item = '" + reader[0].ToString());
                                                    File.Copy(Path.Combine(ruta_local, nuevo_nom), Path.Combine(ruta_alterna, nuevo_nom)); 
                                                }
                                                else
                                                {
                                                    are++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string result = ftp.Ftp(server, user, password, reader[26].ToString().Trim(), ruta_local, ruta_server);
                                            if (result == "OK")
                                            {
                                                arc++;
                                            }
                                            else
                                            {
                                                are++;
                                            }
                                        }  
                                    }
                                }
                                catch (Exception ex)
                                {
                                    err++;
                                    lbl_error.Text = ex.Message;
                                    conn.Close();
                                    conn.Dispose();
                                }
                            }
                        }
                        reader.Close();
                        // Final Transpaso
                        lista_errores.Rows.Add("Ítems Nuevos", Convert.ToString(ins));
                        lista_errores.Rows.Add("Ítems Actualizados", Convert.ToString(upd));
                        lista_errores.Rows.Add("Error", Convert.ToString(err));
                        lista_errores.Rows.Add("Archivos Procesados", Convert.ToString(arc));
                        lista_errores.Rows.Add("Archivos con error", Convert.ToString(are));
                        GridResultados.DataBind();
                        GridResultados.Visible = true;
                        connection.Close();
                        connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        lbl_error.Text = ex.Message + query;
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }
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

        protected void Productos_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id"); //0
            dt.Columns.Add("Código");
            dt.Columns.Add("Descripción");
            dt.Columns.Add("Visible");
            dt.Columns.Add("Prod a Pedido");
            dt.Columns.Add("Venta"); // 6
            dt.Columns.Add("Marca");
            dt.Columns.Add("Manual técnico");
            dt.Columns.Add("Presentación");
            dt.Columns.Add("Foto");
            dt.Columns.Add("Foto Grande");
            dt.Columns.Add("Video");
            dt.Columns.Add("Hoja Seguridad"); // 12
            dt.Columns.Add("Publicado");

            foreach (GridViewRow gvr in Productos.Rows)
            {
                dt.Rows.Add(gvr.Cells[1].Text, gvr.Cells[2].Text,
                gvr.Cells[3].Text, gvr.Cells[4].Text, gvr.Cells[5].Text,
                gvr.Cells[6].Text, gvr.Cells[7].Text, gvr.Cells[8].Text,
                gvr.Cells[9].Text, gvr.Cells[10].Text, gvr.Cells[11].Text,
                gvr.Cells[12].Text, gvr.Cells[13].Text, gvr.Cells[14].Text);
            }



            if (dt != null)
            {
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                Productos.DataSource = dataView;
                Productos.DataBind();
            }
        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

    }
}