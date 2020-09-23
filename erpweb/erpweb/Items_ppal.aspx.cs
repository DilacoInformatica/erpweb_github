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

        // FTP
        string server = @"ftp://dev.dilaco.com/";
        string user = "dev@dilaco.com";
        string password = "4ydlrvyKUX8}";

        DataTable lista_errores = new DataTable();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            ImgBtn_Cerrar.Attributes["Onclick"] = "return salir();";
            Btn_Transpaso_Masivo.Attributes["Onclick"] = "return confirm('Ud está a punto de realizar un transpaso masivo de productos a la página Web, Seguro desea proceder?')";
            if (!this.IsPostBack)
            {
                carga_productos("");
            }

           // carga_productos("");
        }

        void carga_productos(String codigo)
        {
            String queryString = "";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                queryString = " Select id_item 'Id', ";
                queryString = queryString + "codigo 'Código', ";
                queryString = queryString + " substring(descripcion, 0, 30) 'Descripción', ";
                queryString = queryString + "IIF(isnull(visible, 0) = 0, 'N', 'S') 'Visible', ";
                queryString = queryString + "IIF(isnull(prodpedido, 0) = 0, 'N', 'S') 'Prod a Pedido', ";
                queryString = queryString + "IIF(isnull(ventas, 0) = 0, 'N', 'S') 'Venta', ";
                queryString = queryString + "IIF(isnull(cotizaciones, 0) = 0, 'N', 'S') 'Cotizacion', ";
                queryString = queryString + "Marca , ";
                queryString = queryString + "IIF(isnull(Manual_Tecnico,'') = '','NO',Manual_Tecnico) 'Manual técnico' , ";
                queryString = queryString + "IIF(isnull(Presentacion_Producto,'') = '','NO', Presentacion_Producto)'Presentación', ";
                queryString = queryString + "IIF(isnull(Foto,'') = '','NO', Foto) 'Foto', ";
                queryString = queryString + "IIF(isnull(Foto_Grande,'') = '','NO',Foto_Grande) 'Foto Grande', ";
                queryString = queryString + "IIF(isnull(video,'') = '','NO',video) 'Video', ";
                queryString = queryString + "IIF(isnull(Hoja_de_Seguridad,'') = '','NO',Hoja_de_Seguridad) 'Hoja Seguridad' ";
                queryString = queryString + "from tbl_items_web  ";

                if (codigo != "")
                {
                    queryString = queryString + "where codigo =  '" + codigo + "'";
                }

                SqlDataAdapter data = new SqlDataAdapter(queryString, connection);
                DataSet ds = new DataSet();

                connection.Open();

                data.Fill(ds, "tbl_items_web_table");

                Productos.DataSource = ds;
                Productos.DataBind();
                Productos.DataMember = "tbl_items_web_table";
                
                lbl_cantidad.Text ="Cantidad de Registros: " + Convert.ToString(Productos.Rows.Count);
            }
        }

        protected void Btn_buscar_Click(object sender, EventArgs e)
        {
            GridResultados.Visible = false;
            if (txt_codigo.Text == "")
            {
                lbl_error.Text = "Debe indicar código a buscar";
            }
            else
            {
                carga_productos(txt_codigo.Text);
            }
        }

        protected void Productos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Productos.SelectedRow;
            Response.Redirect("Items_detalle.aspx?id_item="+ row.Cells[1].Text);
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
                query = "SELECT iw.Id_Item "; // 0
                query = query + ",iw.Codigo "; // 1
                query = query + ",isnull(iw.habilitado_venta,0) habilitado_venta  "; // 2
                query = query + ",isnull(iw.prodpedido,0) prodpedido "; //3
                query = query + ",isnull(iw.visible,0) visible "; //4
                query = query + ",isnull(iw.cotizaciones,0) cotizaciones "; //5
                query = query + ",isnull(iw.ventas,0) ventas "; //6
                query = query + ",iw.Texto_Destacado "; //7 
                query = query + ",iw.codigo_maestro "; //8
                query = query + ",iw.Texto_maestro "; //9
                query = query + ",iw.Descripcion_maestro "; //10
                query = query + ",iw.Descripcion "; // 11
                query = query + ",isnull(iw.Id_Categoria,0) Id_Categoria "; // 12
                query = query + ",isnull(iw.Id_SubCategoria,0) Id_SubCategoria "; //13
                query = query + ",isnull(iw.Id_Linea_Venta,0) Id_Linea_Venta "; //14
                query = query + ",isnull(iw.Id_proveedor,0) id_proveedor "; //15
                query = query + ",iw.Marca "; //16
                query = query + ",isnull(iw.Precio, 0) Precio "; // 17
                query = query + ",isnull(iw.Id_moneda,0) id_moneda "; // 18
                query = query + ",iw.unidad_vta ";  // 19
                query = query + ",iw.Codigo_prov "; //20
                query = query + ",iw.Caracteristicas "; //21
                query = query + ",iw.Manual_Tecnico "; //22
                query = query + ",iw.Presentacion_Producto  "; //23
                query = query + ",iw.Foto "; //24
                query = query + ",iw.Foto_Grande "; //25
                query = query + ",iw.Video "; //26
                query = query + ",iw.Producto_Nuevo "; //27
                query = query + ",iw.Producto_Oferta "; //28
                query = query + ",isnull(iw.Id_Accesorio1,0) Id_Accesorio1 "; //29
                query = query + ",isnull(iw.Id_Accesorio2,0) Id_Accesorio2 "; //30
                query = query + ",isnull(iw.Id_Accesorio3,0) Id_Accesorio3 "; //31
                query = query + ",isnull(iw.Id_Repuesto1,0) Id_Repuesto1 "; //32
                query = query + ",isnull(iw.Id_Repuesto2,0) Id_Repuesto2 "; //33
                query = query + ",isnull(iw.Id_Repuesto3,0) Id_Repuesto3 "; //34
                query = query + ",isnull(iw.Id_Alternativa1,0) Id_Alternativa "; //35
                query = query + ",isnull(iw.Id_Alternativa2,0) Id_Alternativa2 "; //36
                query = query + ",isnull(iw.Id_Alternativa3,0) Id_Alternativa3 "; //37
                query = query + ",isnull(iw.Id_Categoria1,0) Id_Categoria "; //38
                query = query + ",isnull(iw.Id_Categoria2,0) Id_Categoria2 "; //39
                query = query + ",isnull(iw.Id_Categoria3,0) Id_Categoria3 "; //40
                query = query + ",isnull(iw.Id_SubCategoria1,0) Id_SubCategoria1 "; //41
                query = query + ",isnull(iw.Id_SubCategoria2,0) Id_SubCategoria2 "; //42
                query = query + ",isnull(iw.Id_SubCategoria3,0) Id_SubCategoria3 "; //43
                query = query + ",iw.Tabla_Tecnica "; //44
                query = query + ",iw.Hoja_de_Seguridad "; //45
                query = query + ",pr.Nombre_Fantasia "; //46
                query = query + "FROM tbl_Items_web iw ";
                query = query + "left outer join tbl_Categorias ct on ct.ID_Categoria = iw.Id_Categoria ";
                query = query + "left outer join tbl_Subcategorias sb on sb.ID_SubCategoria = iw.Id_SubCategoria ";
                query = query + "left outer join tbl_Proveedores pr on pr.ID_Proveedor = iw.Id_proveedor ";
                query = query + "left outer join tbl_Monedas mn on mn.ID_Moneda = iw.Id_moneda ";
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
                                    //// Ficha Técnica
                                    if (reader[22].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/manual_tecnico/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/manual_tecnico/");
                                        string result = ftp.Ftp(server, user, password, reader[22].ToString().Trim(), ruta_local, ruta_server);
                                        if (result == "Ok")
                                        {
                                            arc++;
                                        }
                                        else
                                        {
                                            are++;
                                        }
                                    }
                                    // Presentación
                                    if (reader[23].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/presentacion/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/Presentacion/");
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
                                    // Hoja Seguridad
                                    if (reader[45].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/hds/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/HojaS/");
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
                                    // Foto C
                                    if (reader[24].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/img/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/Imagenes/");
                                        string result = ftp.Ftp(server, user, password, reader[24].ToString().Trim(), ruta_local, ruta_server);
                                        if (result == "Ok")
                                        {
                                            arc++;
                                        }
                                        else
                                        {
                                            are++;
                                        }
                                    }
                                    // Foto G
                                    if (reader[25].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/img/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/Imagenes/");
                                        string result = ftp.Ftp(server, user, password, reader[25].ToString().Trim(), ruta_local, ruta_server);
                                        if (result == "Ok")
                                        {
                                            arc++;
                                        }
                                        else
                                        {
                                            are++;
                                        }
                                    }

                                    // Video
                                    if (reader[26].ToString().Trim() != "")
                                    {
                                        ruta_server = @"/dinamicos/productos/videos/";
                                        ruta_local = Server.MapPath(@"~/Catalogo/Productos/Videos/");
                                        string result = ftp.Ftp(server, user, password, reader[26].ToString().Trim(), ruta_local, ruta_server);
                                        if (result == "Ok")
                                        {
                                            arc++;
                                        }
                                        else
                                        {
                                            are++;
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

            foreach (GridViewRow gvr in Productos.Rows)
            {
                dt.Rows.Add(gvr.Cells[1].Text, gvr.Cells[2].Text,
                gvr.Cells[3].Text, gvr.Cells[4].Text, gvr.Cells[5].Text,
                gvr.Cells[6].Text, gvr.Cells[7].Text, gvr.Cells[8].Text,
                gvr.Cells[9].Text, gvr.Cells[10].Text, gvr.Cells[11].Text,
                gvr.Cells[12].Text, gvr.Cells[13].Text);
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