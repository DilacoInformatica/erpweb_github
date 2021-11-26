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
    public partial class Item : System.Web.UI.Page
    {
        string v_completa = "";
        string Sserver = "";
        string SMysql = "";
        string modo = "";
        int id_item = 0;
        int usuario = 0;
        string ruta_alterna = "";
        string archivo2 = "";

        ClsFTP ftp = new ClsFTP();
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        // FTP
        
        string server = @"ftp://dev.dilaco.com/";
        string user = "dev@dilaco.com";
        string password = "4ydlrvyKUX8}";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

            id_item = Convert.ToInt32(Request.QueryString["id_item"].ToString());
            usuario = Convert.ToInt32(Request.QueryString["usuario"].ToString());

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
                }

                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "D"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Desarrollo"; }
                else
                { lbl_ambiente.Text = "P"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Producción"; }

                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");

                server = @utiles.obtengo_valor_regla("FTPS", Sserver);
                user = utiles.obtengo_valor_regla("FTPU", Sserver);
                password = utiles.obtengo_valor_regla("FTPC", Sserver);

                
            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }
           
            if (String.IsNullOrEmpty(Request.QueryString["modo"]))
            {
                modo = "W"; // mi usuarios por default mientras no nos conectemos al servidor
            }
            else
            {
                modo = "E";
            }
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            txt_precio_lista.Enabled = false;

            //lbl_ambiente.Text = utiles.retorna_ambiente();

            if (utiles.retorna_ambiente() == "D")
            { lbl_ambiente.Text = "D"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Desarrollo"; }
            else
            { lbl_ambiente.Text = "P"; lbl_ambiente.ToolTip = "Estás conetado al Ambiente de Producción"; }

            ruta_alterna = utiles.retorna_ruta();

            if (modo == "W")
            {
                LnkBtn_Volver.Visible = false;
            }
            else
            {
                LnkBtn_Volver.Visible = true;
            }

            ImgBtnLink.Attributes["Onclick"] = "return abrirficha(" + id_item + ");";
            if (!this.IsPostBack)
            {
                Btn_eliminar.Attributes["Onclick"] = "return confirm('Desea Eliminar Producto desde la Web? Esto afectará futuras ventas asociadas... Producto se desactivará y no podrá modificarlo')";
                ImgBtnAct_item.Attributes["Onclick"] = "return confirm('Desea Activar nuevamente el Producto?')";
                ImgBtnDesAct_item.Attributes["Onclick"] = "return confirm('Desea Desactivar el Producto? Este será eliminado del Sitio Web')";
                LnkBtn_Eliminar.Attributes["Onclick"] = "return confirm('Producto se eliminará en la ficha Web del ERP y del Sitio... Desea Continuar?')";
                //carga_contrl_lista("select 0 id_moneda, 'Seleccione Moneda' Sigla union all select id_moneda, Sigla from tbl_monedas", LstMonedas, "tbl_monedas","id_moneda","Sigla");
                carga_contrl_lista("select id_moneda, Sigla from tbl_monedas", LstMonedas, "tbl_monedas", "id_moneda", "Sigla");
                // carga_contrl_lista("select 0 ID_SubCategoria, 'Seleccione Subcategoría' Nombre union all select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1", LstSubCategorias, "tbl_categorias", "ID_SubCategoria", "Nombre");
                carga_contrl_lista("select ID_Linea_Venta, CONCAT(Cod_Linea_Venta ,' ', nombre) Nombre from tbl_Lineas_Venta where Activo = 1 order by replace(Cod_Linea_Venta, 'GRUPO ', '')", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");
                // carga_contrl_lista("select 0 ID_Linea_Venta, 'Seleccione Línea Venta' Nombre union all select ID_Linea_Venta, CONCAT(Cod_Linea_Venta, ' ', Nombre) Nombre from tbl_Lineas_Venta where Activo = 1", LstLineaVtas, "tbl_Lineas_Venta", "ID_Linea_Venta", "Nombre");

                //carga_contrl_lista("select 0 id_familia, 'Seleccione Familia Productos' Nombre union all select id_familia, nombre from tbl_Familias_Productos where Activo = 1", LstDivision, "tbl_Familias_Productos", "id_familia", "Nombre");
                carga_contrl_lista("select id_familia, nombre from tbl_Familias_Productos where Activo = 1 order by nombre", LstDivision, "tbl_Familias_Productos", "id_familia", "Nombre");
                // Categorias parte inferior
                //carga_contrl_lista("select 0 ID_Categoria, 'Seleccione Categoría' Nombre union all select ID_Categoria, Nombre from tbl_categorias where Activo = 1", LstCategorias1, "tbl_categorias", "ID_Categoria", "Nombre");
                //carga_contrl_lista("select 0 ID_Categoria, 'Seleccione Categoría' Nombre union all select ID_Categoria, Nombre from tbl_categorias where Activo = 1", LstCategorias2, "tbl_categorias", "ID_Categoria", "Nombre");
                //carga_contrl_lista("select 0 ID_Categoria, 'Seleccione Categoría' Nombre union all select ID_Categoria, Nombre from tbl_categorias where Activo = 1", LstCategorias3, "tbl_categorias", "ID_Categoria", "Nombre");

                //carga_contrl_lista("select 0 ID_SubCategoria, 'Seleccione SubCategoría' Nombre union all select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1", LstSubCategorias1, "tbl_categorias", "ID_SubCategoria", "Nombre");
                //carga_contrl_lista("select 0 ID_SubCategoria, 'Seleccione SubCategoría' Nombre union all select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1", LstSubCategorias2, "tbl_categorias", "ID_SubCategoria", "Nombre");
                //carga_contrl_lista("select 0 ID_SubCategoria, 'Seleccione SubCategoría' Nombre union all select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1", LstSubCategorias3, "tbl_categorias", "ID_SubCategoria", "Nombre");



                carga_contrl_lista("select ID_Categoria, CONCAT('(',tbl_Familias_Productos.Nombre,') ',tbl_categorias.Nombre ) Nombre from tbl_categorias inner join tbl_Familias_Productos	  on tbl_Familias_Productos.ID_Familia = tbl_categorias.Id_Familia where tbl_Familias_Productos.Activo = 1 order by tbl_Familias_Productos.Nombre, tbl_Categorias.Nombre", LstCategorias1, "tbl_categorias", "ID_Categoria", "Nombre");
                carga_contrl_lista("select ID_Categoria, CONCAT('(',tbl_Familias_Productos.Nombre,') ',tbl_categorias.Nombre ) Nombre from tbl_categorias inner join tbl_Familias_Productos	  on tbl_Familias_Productos.ID_Familia = tbl_categorias.Id_Familia where tbl_Familias_Productos.Activo = 1 order by tbl_Familias_Productos.Nombre, tbl_Categorias.Nombre", LstCategorias2, "tbl_categorias", "ID_Categoria", "Nombre");
                carga_contrl_lista("select ID_Categoria, CONCAT('(',tbl_Familias_Productos.Nombre,') ',tbl_categorias.Nombre ) Nombre from tbl_categorias inner join tbl_Familias_Productos	  on tbl_Familias_Productos.ID_Familia = tbl_categorias.Id_Familia where tbl_Familias_Productos.Activo = 1 order by tbl_Familias_Productos.Nombre, tbl_Categorias.Nombre", LstCategorias3, "tbl_categorias", "ID_Categoria", "Nombre");

                //carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 order by nombre", LstSubCategorias1, "tbl_categorias", "ID_SubCategoria", "Nombre");
                //carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 order by nombre", LstSubCategorias2, "tbl_categorias", "ID_SubCategoria", "Nombre");
                //carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 order by nombre", LstSubCategorias3, "tbl_categorias", "ID_SubCategoria", "Nombre");

                muestra_info(id_item);
                /*MT_Warning.Visible = false;
                VD_Warning.Visible = false;
                HS_Warning.Visible = false;
                FG_Warning.Visible = false;
                FC_Warning.Visible = false;*/

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

        void muestra_info(int id_item)
        {
            string query = "";
            string ruta_local = "";
            string extension = "";
            string nuevo_nom = "";

            string v_correo_operaciones = "";

            lbl_web.Text = "SI"; 

            lbl_status.Text = "";
            query = "SELECT iw.Id_Item "; // 0
            query = query + ",iw.Codigo "; // 1
            query = query + " ,isnull(iw.habilitado_venta,0) "; // 2
            query = query + " ,isnull(iw.prodpedido,0) "; //3
            query = query + ",isnull(iw.visible,0) "; //4
            query = query + ",isnull(iw.cotizaciones,0) "; //5
            query = query + ",isnull(iw.ventas,0) "; //6
            query = query + ",iw.Texto_Destacado "; //7 
            query = query + ",iw.codigo_maestro "; //8
            query = query + ",iw.Texto_maestro "; //9
            query = query + ",iw.Descripcion_maestro "; //10
            query = query + ",iw.Descripcion "; // 11
            query = query + ",iw.Id_Categoria "; // 12
            query = query + ",iw.Id_SubCategoria "; //13
            query = query + ",iw.Id_Linea_Venta "; //14
            query = query + ",iw.Id_proveedor "; //15
            query = query + ",iw.Marca "; //16
            query = query + ",isnull(iw.Precio, 0) Precio "; // 17
            query = query + ",iw.Id_moneda "; // 18
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
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Accesorio1) Id_Accesorio1 "; //29
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Accesorio2) Id_Accesorio2 "; //30
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Accesorio3) Id_Accesorio3 "; //31
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Repuesto1) Id_Repuesto1 "; //32
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Repuesto2) Id_Repuesto2 "; //33
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Repuesto3) Id_Repuesto3 "; //34
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Alternativa1) Id_Alternativa1 "; //35
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Alternativa2) Id_Alternativa2 "; //36
            query = query + ",(select codigo from tbl_items where id_item = iw.Id_Alternativa3) Id_Alternativa3 "; //37
            query = query + ",iw.Id_Categoria1 "; //38
            query = query + ",iw.Id_Categoria2 "; //39
            query = query + ",iw.Id_Categoria3 "; //40
            query = query + ",iw.Id_SubCategoria1 "; //41
            query = query + ",iw.Id_SubCategoria2 "; //42
            query = query + ",iw.Id_SubCategoria3 "; //43
            query = query + ",iw.Tabla_Tecnica "; //44
            query = query + ",iw.Hoja_de_Seguridad "; //45
            query = query + ",pr.Nombre_Fantasia "; //46
            query = query + ",iw.Precio_lista "; //47
            query = query + ",(select Unidad from tbl_items where tbl_items.ID_Item = iw.Id_Item) unidad "; //48
            query = query + ",iw.Activo "; //49
            query = query + ",fp.nombre division "; //50
            query = query + ",fp.ID_Familia "; //51
            query = query + ",isnull(iw.Multiplo,1) Multiplo "; //52
            query = query + ",isnull(iw.Precio_neto, 0) Precio_neto "; //53
            query = query + "FROM tbl_Items_web iw ";
            query = query + "left outer join tbl_Categorias ct on ct.ID_Categoria = iw.Id_Categoria ";
            query = query + "left outer join tbl_Subcategorias sb on sb.ID_SubCategoria = iw.Id_SubCategoria ";
            query = query + "left outer join tbl_Familias_Productos fp on fp.ID_Familia = ct.ID_Familia ";
            query = query + "left outer join tbl_Proveedores pr on pr.ID_Proveedor = iw.Id_proveedor ";
            query = query + "left outer join tbl_Monedas mn on mn.ID_Moneda = iw.Id_moneda ";
            query = query + "where Id_Item = " + id_item;
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        if (reader.GetBoolean(49))
                        {
                            lbl_activo.Text = "SI";
                            ImgBtnDesAct_item.Visible = true;
                            
                        }
                        else
                        {
                            lbl_activo.Text = "NO";
                            lbl_web.Text = "NO";
                            //lbl_status.Text = "Producto desactivado en el ERP... no se puede realizar ninguna acción sobre él hasta que se active nuevamente";
                            lbl_status.Text = "Producto no publicado en Sitio Web.";
                            lbl_status.ForeColor = Color.Red;
                            ImgBtnAct_item.Visible = true;
                        }


                        txt_codigo.Text = reader[1].ToString();
                        txt_descripcion.Text = reader[11].ToString();
                        // txt_descripcion.Text = reader.GetString(11) ;

                        if (reader.GetBoolean(4))
                        { chck_visible.Checked = true; }
                        else
                        { chck_visible.Checked = false; }
                        if (reader.GetBoolean(3))
                        { chck_prodped.Checked = true; }
                        else
                        { chck_prodped.Checked = false; }
                        if (reader.GetBoolean(6))
                        { chck_venta.Checked = true; }
                        else
                        { chck_venta.Checked = false; }
                        if (reader.GetBoolean(5))
                        { chck_cot.Checked = true; }
                        else
                        { chck_cot.Checked = false; }

                        foreach (ListItem item in LstDivision.Items)
                        {
                            if (item.Value == reader[51].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }


                        if (reader[51].ToString() != "")
                        {

                            carga_contrl_lista("select ID_Categoria, Nombre from tbl_Categorias where Activo = 1 and ID_Familia =" + reader[51].ToString() + "order by nombre", LstCategorias, "tbl_categorias", "ID_Categoria", "Nombre");
                            // LstCategorias.Items.Add(new ListItem("Seelccione Categoría", "0"));
                            // carga_contrl_lista("select ID_Categoria, Nombre from tbl_Categorias where Activo = 1 order by nombre", LstCategorias, "tbl_categorias", "ID_Categoria", "Nombre");
                            //if (LstCategorias.Items.FindByValue("0") != null) // or IndVal.ToString()
                            // {
                            //           LstCategorias.SelectedValue = "0";
                            //  }
                        }
                        //else
                        //{
                        //    carga_contrl_lista("select ID_Categoria, Nombre from tbl_Categorias where Activo = 1 order by nombre", LstCategorias, "tbl_categorias", "ID_Categoria", "Nombre");
                        //    if (LstCategorias.Items.FindByValue("0") != null) // or IndVal.ToString()
                        //    {
                        //        LstCategorias.SelectedValue = "0";
                        //    }
                        //}


                        foreach (ListItem item in LstCategorias.Items)
                        {
                            if (item.Value == reader[12].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        if (reader[12].ToString() != "")
                        {
                            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria = " + reader[12].ToString() + " order by Nombre", LstSubCategorias, "tbl_categorias", "ID_SubCategoria", "Nombre");
                        }

                        foreach (ListItem item in LstSubCategorias.Items)
                        {
                            if (item.Value == reader[13].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        foreach (ListItem item in LstLineaVtas.Items)
                        {
                            if (item.Value == reader[14].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        foreach (ListItem item in LstMonedas.Items)
                        {
                            if (item.Value == reader[18].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        //if (swc == 0)
                        //{
                        //    LstMonedas.Items.Insert(0, "Seleccione Moneda");
                        //    LstMonedas.Items[0].Selected = true;
                        //}

                        foreach (ListItem item in LstCategorias1.Items)
                        {
                            if (item.Value == reader[38].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        if (reader[38].ToString()!= "")
                        {
                            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria = " + reader[38].ToString() + " order by Nombre", LstSubCategorias1, "tbl_Subcategorias", "ID_SubCategoria", "Nombre");
                        }

                        foreach (ListItem item in LstSubCategorias1.Items)
                        {
                            if (item.Value == reader[41].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }


                        foreach (ListItem item in LstCategorias2.Items)
                        {
                           // lbl_error.Text = reader[39].ToString();
                            if (item.Value == reader[39].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        if (reader[39].ToString() != "")
                        {
                            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria = " + reader[39].ToString() + " order by Nombre", LstSubCategorias2, "tbl_Subcategorias", "ID_SubCategoria", "Nombre");
                        }

                        foreach (ListItem item in LstSubCategorias2.Items)
                        {
                            if (item.Value == reader[42].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                      
                        foreach (ListItem item in LstCategorias3.Items)
                        {
                            if (item.Value == reader[40].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        if (reader[40].ToString() != "")
                        {
                            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria = " + reader[40].ToString() + " order by Nombre", LstSubCategorias3, "tbl_Subcategorias", "ID_SubCategoria", "Nombre");
                        }

                        foreach (ListItem item in LstSubCategorias3.Items)
                        {
                            if (item.Value == reader[43].ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        // txt_division.Text = reader[50].ToString();
                        lbl_unidad.Text = reader[48].ToString();
                        txt_proveedor.Text = reader[46].ToString();
                        txt_marca.Text = reader[16].ToString();
                        txt_precio.Text = reader[17].ToString();
                        txt_precio_lista.Text = reader[47].ToString();
                       // txt_precio_neto.Text = reader[53].ToString();
                        //lbl_moneda.Text = reader[11].ToString();
                        txt_unidad.Text = reader[19].ToString();
                        txt_codigoprov.Text = reader[20].ToString();
                        //txt_codigoprov.Enabled = false;

                        lbl_manual_tecnico.Text = reader[22].ToString();
                        txt_proveedor.Text = reader[46].ToString();
                        lbl_fotoc.Text = reader[24].ToString();
                        lbl_fotog.Text = reader[25].ToString();
                        lbl_presentacion.Text = reader[23].ToString();
                        
                        if (lbl_fotog.Text != "")
                        {
                            img_prod.ImageUrl = "~/Catalogo/Productos/Imagenes//" + Path.GetFileName(lbl_fotog.Text);
                        }
                        else
                        {
                            img_prod.ImageUrl = "~/img//prod_img_no_disponible.jpg";
                        }

                        lbl_video.Text = reader[26].ToString();
                        lbl_hoja_seguridad.Text = reader[45].ToString();
                        //  txt_tabla_tecnica.Text = HttpContext.Current.Server.HtmlEncode(reader[44].ToString());
                        //
                        string myEncodedString = HttpUtility.HtmlDecode((reader[21].ToString()));
                        StringWriter myWriter = new StringWriter();
                        HttpUtility.HtmlDecode(myEncodedString, myWriter);
                        string myDecodedString = myWriter.ToString();
                        // txt_caracteristicas.Text = myDecodedString; // HttpContext.Current.Server.HtmlDecode(reader[21].ToString());


                        if (myDecodedString == "")
                        {
                            txt_caracteristicas.Visible = true;
                            lbl_caracteristicas.Visible = false;
                            txt_caracteristicas.Text = myDecodedString;
                            ImgVerCar.Visible = false;
                            // ImgHTmltec.Visible = false;
                            ImgGrabaCar.Visible = true;
                        }
                        else
                        {
                            txt_caracteristicas.Visible = false;
                            lbl_caracteristicas.Visible = true;
                            lbl_caracteristicas.Text = myDecodedString;
                            ImgHTmlCar.Visible = true;
                            ImgGrabaCar.Visible = false;
                            ImgVerCar.Visible = false;
                        }

                        // txt_caracteristicas.Visible = false;
                        // lbl_caracteristicas.Text = myDecodedString;

                        myEncodedString = HttpUtility.HtmlDecode((reader[44].ToString()));
                        StringWriter myWriter2 = new StringWriter();
                        HttpUtility.HtmlDecode(myEncodedString, myWriter2);
                        myDecodedString = myWriter2.ToString();
                        //  txt_tabla_tecnica.Text = myDecodedString;


                        if (myDecodedString == "")
                        {
                            txt_tabla_tecnica.Visible = true;
                            lbl_tabla_tecnica.Visible = false;
                            txt_tabla_tecnica.Text = myDecodedString;
                            ImgGrabaTec.Visible = true;
                            ImgVerTec.Visible = false;
                            ImgHTmltec.Visible = false;
                        }
                        else
                        {
                            txt_tabla_tecnica.Visible = false;
                            lbl_tabla_tecnica.Visible = true;
                            lbl_tabla_tecnica.Text = myDecodedString;
                            ImgHTmltec.Visible = true;
                            ImgGrabaTec.Visible = false;

                            ImgVerTec.Visible = false;
                        }



                        // txt_tabla_tecnica.Text = HttpContext.Current.Server.HtmlDecode(reader[44].ToString());
                        //  string pattern = "<.*?>";
                        // txt_caracteristicas.Text = Regex.Replace(HttpUtility.HtmlDecode(reader[21].ToString()), pattern, string.Empty);
                        //lbl_caracteristicas.Text = reader[21].ToString();
                        // txt_caracteristicas.Text = HttpUtility.HtmlDecode(reader[21].ToString());

                        // HttpUtility.HtmlDecode(reader[44].ToString());
                        //lbl_tabla_tecnica.Text = reader[44].ToString();
                        // txt_caracteristicas.Text = HttpContext.Current.Server.HtmlDecode(reader[21].ToString());
                        txt_acc1.Text = reader[29].ToString();
                        txt_acc2.Text = reader[30].ToString();
                        txt_acc3.Text = reader[31].ToString();
                        txt_rep1.Text = reader[32].ToString();
                        txt_rep2.Text = reader[33].ToString();
                        txt_rep2.Text = reader[34].ToString();
                        txt_alt1.Text = reader[35].ToString();
                        txt_alt2.Text = reader[36].ToString();
                        txt_alt3.Text = reader[37].ToString();
                        txt_multiplo.Text = reader[52].ToString();

                       // lbl_stock.Text = consulta_stock_erp(id_item);

                        lbl_stock.Text = consulta_stock_web(id_item);

                        lbl_aviso_informacion.Text = "";

                        if (lbl_web.Text == "SI") {
                         //   actualiza_stock_web(id_item, Convert.ToDouble(lbl_stock.Text));
                            lbl_stock_critico.Text = consulta_stock_critico_web(id_item);

                            if (Convert.ToInt32(lbl_stock.Text) < Convert.ToInt32(lbl_stock_critico.Text) && chck_venta.Checked)
                            {
                                lbl_stock.CssClass = "label label-primary";
                                lbl_stock_critico.CssClass = "label label-primary";

                                lbl_aviso_informacion.Text = "Stock de producto es menor a su stock crítico en Bodega Web, se envía aviso a Operaciones para correción";

                                v_correo_operaciones = utiles.obtengo_valor_regla("COROP", Sserver);

                                utiles.enviar_correo("Aviso solicita reposición Stock","Estimado: Stock para producto " + txt_codigo.Text.Trim() + " se encuentra bajo Stock crítico, favor reponer ",v_correo_operaciones);

                               // envia_aviso_a_operaciones (txt_codigo.Text,  )

                            }

                            if (Convert.ToInt32(lbl_stock.Text) <= Convert.ToInt32(lbl_stock_critico.Text) && chck_venta.Checked)
                            {
                                lbl_stock.CssClass = "label label-primary";
                                lbl_stock_critico.CssClass = "label label-warning";
                                lbl_status.Text = "Stock de Producto llegando a Niveles Críticos";
                            }

                           /* if (Convert.ToInt32(lbl_stock.Text) < Convert.ToInt32(lbl_stock_critico.Text))
                            {
                                lbl_stock.CssClass = "label label-primary";
                                lbl_stock.CssClass = "label label-danger";
                                lbl_status.Text = "Stock de Producto sobrepasó nivel mínimo";
                            }*/
                        }
                        else
                        {
                            lbl_stock_critico.Text = "0";
                            lbl_stock.CssClass = "label label-info";
                            lbl_stock_critico.CssClass = "label label-info";
                        }

                        if (chck_venta.Checked && Convert.ToInt32(lbl_stock.Text) == 0)
                        {
                            lbl_status.Text = "Producto está marcado para ventas pero no tiene stock definido, favor solicitar carga de stock a bodega Web";
                        }

                        if (lbl_fotog.Text.Trim() != "")
                        {
                            archivo2 = Path.Combine(ruta_alterna, lbl_fotog.Text.Trim());
                            extension = Path.GetExtension(archivo2);
                            if (File.Exists(archivo2))
                            {
                                nuevo_nom = "FG_" + txt_codigo.Text.Replace(" ", "") + extension;
                                ruta_local = Server.MapPath(@"~/Catalogo/Productos/Imagenes/");
                                if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                {
                                    File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                    lbl_fotog.Text = nuevo_nom;
                                }
                                else
                                {
                                    if (!File.Exists(Path.Combine(ruta_alterna, nuevo_nom)))
                                    {
                                        File.Copy(archivo2, Path.Combine(ruta_alterna, nuevo_nom));
                                    }
                                    lbl_fotog.Text = nuevo_nom;
                                }
                            }
                            else
                            {
                                FG_Warning.Visible = true;
                                lbl_fotog.ForeColor = Color.Red;
                            }
                        }

                        if (lbl_fotoc.Text.Trim() != "")
                        {
                            archivo2 = Path.Combine(ruta_alterna, lbl_fotoc.Text.Trim());
                            extension = Path.GetExtension(archivo2);
                            if (File.Exists(archivo2))
                            {
                                nuevo_nom = "FC_" + txt_codigo.Text.Replace(" ", "") + extension;
                                ruta_local = Server.MapPath(@"~/Catalogo/Productos/Imagenes/");
                                if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                {
                                    File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                    lbl_fotoc.Text = nuevo_nom;
                                }
                                else
                                {
                                    if (!File.Exists(Path.Combine(ruta_alterna, nuevo_nom)))
                                    {
                                        File.Copy(archivo2, Path.Combine(ruta_alterna, nuevo_nom));
                                    }
                                    lbl_fotoc.Text = nuevo_nom;
                                }
                            }
                            else
                            {
                                FC_Warning.Visible = true;
                                lbl_fotoc.ForeColor = Color.Red;
                            }
                        }

                        if (lbl_video.Text.Trim() != "")
                        {
                            archivo2 = Path.Combine(ruta_alterna, lbl_video.Text.Trim());
                            extension = Path.GetExtension(archivo2);
                            if (File.Exists(archivo2))
                            {
                                nuevo_nom = "VD_" + txt_codigo.Text.Replace(" ", "") + extension;
                                ruta_local = Server.MapPath(@"~/Catalogo/Productos/Videos/");
                                if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                {
                                    File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                    lbl_video.Text = nuevo_nom;
                                }
                                else
                                {
                                    if (!File.Exists(Path.Combine(ruta_alterna, nuevo_nom)))
                                    {
                                        File.Copy(archivo2, Path.Combine(ruta_alterna, nuevo_nom));
                                    }
                                    lbl_video.Text = nuevo_nom;
                                    lbl_video.ForeColor = Color.Red;
                                }
                            }
                            else
                            {
                                VD_Warning.Visible = true;
                            }
                        }

                        if (lbl_manual_tecnico.Text.Trim() != "")
                        {
                            archivo2 = Path.Combine(ruta_alterna, lbl_manual_tecnico.Text.Trim());
                            extension = Path.GetExtension(archivo2);
                            if (File.Exists(archivo2))
                            {
                                nuevo_nom = "MT_" + txt_codigo.Text.Replace(" ", "") + extension;
                                ruta_local = Server.MapPath(@"~/Catalogo/Productos/Manual_tecnico/");
                                if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                {
                                    File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                    lbl_manual_tecnico.Text = nuevo_nom;
                                }
                                else
                                {
                                    if (!File.Exists(Path.Combine(ruta_alterna, nuevo_nom)))
                                    {
                                        File.Copy(archivo2, Path.Combine(ruta_alterna, nuevo_nom));
                                    }
                                    lbl_manual_tecnico.Text = nuevo_nom;
                                }

                            }
                            else
                            {
                                MT_Warning.Visible = true;
                                lbl_manual_tecnico.ForeColor = Color.Red;
                            }

                        }

                        if (lbl_hoja_seguridad.Text.Trim() != "")
                        {
                            archivo2 = Path.Combine(ruta_alterna, lbl_hoja_seguridad.Text.Trim());
                            extension = Path.GetExtension(archivo2);
                            if (File.Exists(archivo2))
                            {
                                nuevo_nom = "HS_" + txt_codigo.Text.Replace(" ", "") + extension;
                                ruta_local = Server.MapPath(@"~/Catalogo/Productos/HojaS/");
                                if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                {
                                    File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                    lbl_hoja_seguridad.Text = nuevo_nom;
                                }
                                else
                                {
                                    if (!File.Exists(Path.Combine(ruta_alterna, nuevo_nom)))
                                    {
                                        File.Copy(archivo2, Path.Combine(ruta_alterna, nuevo_nom));
                                    }
                                    lbl_hoja_seguridad.Text = nuevo_nom;
                                }
                            }
                            else
                            {
                                HS_Warning.Visible = true;
                                lbl_hoja_seguridad.ForeColor = Color.Red;
                            }

                        }
                        if (lbl_presentacion.Text.Trim() != "")
                        {
                            archivo2 = Path.Combine(ruta_alterna, lbl_presentacion.Text.Trim());
                            extension = Path.GetExtension(archivo2);
                            if (File.Exists(archivo2))
                            {
                                nuevo_nom = "PR_" + txt_codigo.Text.Replace(" ", "") + extension;
                                ruta_local = Server.MapPath(@"~/Catalogo/Productos/Presentacion/");
                                if (!File.Exists(Path.Combine(ruta_local, nuevo_nom)))
                                {
                                    File.Copy(archivo2, Path.Combine(ruta_local, nuevo_nom));
                                    lbl_presentacion.Text = nuevo_nom;
                                }
                                else
                                {
                                    if (!File.Exists(Path.Combine(ruta_alterna, nuevo_nom)))
                                    {
                                        File.Copy(archivo2, Path.Combine(ruta_alterna, nuevo_nom));
                                    }
                                    lbl_presentacion.Text = nuevo_nom;
                                }
                            }
                            else
                            {
                                PR_Warning.Visible = true;
                                lbl_presentacion.ForeColor = Color.Red;
                            }
                        }

                        // Validamos que la familia, categoria y subcategoría fueron cargadas....
                        if (LstDivision.SelectedValue.ToString() != "0")
                        {
                            if (consulta_familia_mysql("F", Convert.ToInt32(LstDivision.SelectedValue.ToString()), Convert.ToInt32(LstCategorias.SelectedValue.ToString()), Convert.ToInt32(LstSubCategorias.SelectedValue.ToString())) == 0)
                            {
                                // Familia no existe... debmos crearala en la web
                                Div_fam.Visible = true;
                            }
                        }

                        // Categoria
                        if (LstCategorias.SelectedValue.ToString() != "0" )
                        {
                            if (consulta_familia_mysql("C", Convert.ToInt32(LstDivision.SelectedValue.ToString()), Convert.ToInt32(LstCategorias.SelectedValue.ToString()), Convert.ToInt32(LstSubCategorias.SelectedValue.ToString())) == 0)
                            {
                                // Familia no existe... debmos crearala en la web
                                Div_Cat.Visible = true;
                            }
                        }

                        // Subcateroría
                        if (LstSubCategorias.SelectedValue.ToString() != "0")
                        {
                            if (consulta_familia_mysql("S", Convert.ToInt32(LstDivision.SelectedValue.ToString()), Convert.ToInt32(LstCategorias.SelectedValue.ToString()), Convert.ToInt32(LstSubCategorias.SelectedValue.ToString())) == 0)
                            {
                                // Familia no existe... debmos crearala en la web
                                Div_Subcat.Visible = true;
                            }
                        }

                        // verificamos si el producto fue eliminado del sitio web y eso signfica que esta desactivado

                    }
                    reader.Close();
                    connection.Close();
                    connection.Dispose();
                    validamysql(id_item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    lbl_error.Text = ex.Message;
                }
                connection.Close();
                connection.Dispose();
            }
        }

        void validamysql(int id_item)
        {
            string query = "";
            query = "select count(1) existe from tbl_items where visible = 1 and Id_Item = " + id_item;

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
                                lbl_web.Text = "NO";

                            }
                            else
                            {
                                Btn_emigrar.Enabled = true;
                                Btn_eliminar.Enabled = true;
                                lbl_web.Text = "SI";
                            }
                        }

                    }
                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    Btn_emigrar.Enabled = false;
                    Btn_eliminar.Enabled = false;
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Items_ppal.aspx");
        }

        protected void Btn_eliminar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (lbl_activo.Text == "SI")
                {
                    elimna_item();
                    desactiva_producto();
                }
                else
                {
                    lbl_error.Text = "Producto está desactivado, no se puede eliminar de la Web";
                }
            }
           
        }


        public string consulta_stock_critico_web (int id_item)
        {
            string query = "";
            string result = "0";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "consulta_stock_critico";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id_item", id_item);
                    command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    //var result = command.ExecuteNonQuery();

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            result = Convert.ToString(dr.GetDouble(0));
                        }
                       
                    }

                    conn.Close();
                    conn.Dispose();

                    return result;
                }
                catch (Exception ex)
                {
                    
                    conn.Close();
                    conn.Dispose();
                    return ex.Message + query;
                }
            }
        }


        public string consulta_stock_web(int id_item)
        {
            string query = "";
            string result = "0";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "consulta_stock";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id_item", id_item);
                    command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    //var result = command.ExecuteNonQuery();

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            result = Convert.ToString(dr.GetDouble(0));
                        }
                    }

                    conn.Close();
                    conn.Dispose();

                    return result;
                }
                catch (Exception ex)
                {

                    conn.Close();
                    conn.Dispose();
                    return ex.Message + query;
                }
            }
        }


        void elimna_item()
        {
            string query = "";
            if (lbl_web.Text == "NO")
            {
                lbl_error.Text = "Código no existe en la Web, imposible ejecutar esta acción";
            }
            else
            {
                // Codigo para elimnar producto
                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    try
                    {
                        conn.Open();
                        query = "elimina_item";
                        MySqlCommand command = new MySqlCommand(query, conn);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@v_id_item", id_item);
                        command.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                        //var result = command.ExecuteNonQuery();

                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                        MySqlDataReader dr = command.ExecuteReader();

                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                lbl_status.Text = dr.GetString(0);
                            }
                        }

                        conn.Close();
                        conn.Dispose();
                        lbl_error.Text = "";

                        marca_producto_publicado(id_item, "E");

                        lbl_status.Text = "Producto eliminado correctamente de la Web... desactivado del ERP";
                    }
                    catch (Exception ex)
                    {
                        lbl_error.Text = ex.Message + query;
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        protected void Btn_emigrar_Click(object sender, EventArgs e)
        {
            if (lbl_activo.Text == "SI")
            {
                actualiza();
            }
            else
            {
                lbl_error.Text = "Producto está desactivado, no se puede actualizar";
            }
        }


        void actualiza()
        {
            string ruta_local = "";
            string ruta_server = "";
            string query = "";
            bool v_swc = true;
            if (LstCategorias.SelectedValue.ToString() == "0" && LstSubCategorias.Items.Count == 1)
            {
                lbl_error.Text = "Debe indicar Categoria para el producto, No es posible publicar";
                v_swc = false;
            }

            if (LstCategorias.SelectedValue.ToString() == "0" && LstSubCategorias.Items.Count > 1)
            {
                lbl_error.Text = "Debe indicar Categoria y Subcategoria para el producto, No es posible publicar";
                v_swc = false;
            }

            if (chck_venta.Checked && verifica_stock_web(txt_codigo.Text.Trim()) == "N")
            {
                lbl_status.Text = "No puede actualizar producto para Ventas si no ha regularizado Stock, consulte con su Administrador";
                chck_venta.Checked = false;
            }

            if (v_swc)
            {

                if (lbl_web.Text == "SI")
                {
                    query = "UPDATE dilacocl_dilacoweb.tbl_items ";
                    query = query + "SET ";
                    query = query + " descripcion = '" + txt_descripcion.Text.Replace(",", ".").Trim() + "'";
                    if (chck_prodped.Checked)
                    { query = query + ", prodpedido = 1"; }
                    else
                    { query = query + ", prodpedido = 0"; }
                    if (chck_visible.Checked)
                    { query = query + ", visible = 1"; }
                    else
                    { query = query + ", visible = 0"; }
                    if (chck_cot.Checked)
                    { query = query + ", cotizaciones = 1"; }
                    else
                    { query = query + ", cotizaciones = 0"; }
                    if (chck_venta.Checked)
                    { query = query + ", ventas = 1"; }
                    else
                    { query = query + ", ventas = 0"; }
                    query = query + ",texto_destacado = '" + txt_texto_destacado.Text.Replace(",", ".").Trim() + "'";
                    query = query + ",Id_Categoria = " + LstCategorias.SelectedValue.ToString();
                    query = query + ",Id_Subcategoria = " + LstSubCategorias.SelectedValue.ToString();
                    query = query + ",Id_Linea_Venta = " + LstLineaVtas.SelectedValue.ToString();
                    query = query + ",proveedor = '" + txt_proveedor.Text + "'";
                    query = query + ",Marca = '" + txt_marca.Text + "'";
                    query = query + ",precio = " + txt_precio.Text;
                    query = query + ",precio_lista = " + txt_precio_lista.Text;
                    query = query + ",id_moneda = " + LstMonedas.SelectedItem.Value.ToString();// LstMonedas.SelectedValue.ToString();
                    query = query + ",Unidad_vta = '" + txt_unidad.Text + "'";
                    query = query + ",Unidad = '" + lbl_unidad.Text + "'";
                    query = query + ",Codigo_prov = '" + txt_codigoprov.Text + "'";
                    // query = query + ",Caracteristicas = '" + txt_caracteristicas.Text.Replace(",", ".").Trim() + "'";
                    query = query + ",Caracteristicas = '" + lbl_caracteristicas.Text.Replace(",", ".").Trim() + "'";
                    query = query + ",Manual_tecnico = '" + lbl_manual_tecnico.Text + "'";
                    query = query + ",Presentacion_producto = '" + lbl_presentacion.Text + "'";
                    query = query + ",Multiplo = " + txt_multiplo.Text.Replace(",", ".");
                    query = query + ",Hoja_de_Seguridad = '" + lbl_hoja_seguridad.Text + "'";
                    query = query + ",Foto = '" + lbl_fotoc.Text + "'";
                    query = query + ",Foto_grande = '" + lbl_fotog.Text + "'";
                    //query = query + ",Foto_Maestra = NULL";
                    query = query + ",Video = '" + lbl_video.Text + "'";
                    query = query + ",Id_Accesorio1 = " + retorna_id_item(txt_acc1.Text);
                    query = query + ",Id_Accesorio2 = " + retorna_id_item(txt_acc2.Text);
                    query = query + ",Id_Accesorio3 = " + retorna_id_item(txt_acc3.Text);
                    query = query + ",Id_Repuesto1 = " + retorna_id_item(txt_rep1.Text);
                    query = query + ",Id_Repuesto2 = " + retorna_id_item(txt_rep2.Text);
                    query = query + ",Id_Repuesto3 = " + retorna_id_item(txt_rep3.Text);
                    query = query + ",Id_Alternativa1 = " + retorna_id_item(txt_alt1.Text);
                    query = query + ",Id_Alternativa2 = " + retorna_id_item(txt_alt2.Text);
                    query = query + ",Id_Alternativa3 = " + retorna_id_item(txt_alt3.Text);
                    query = query + ",Id_categoria1 = " + LstCategorias1.SelectedItem.Value.ToString();
                    query = query + ",Id_categoria2 = " + LstCategorias2.SelectedItem.Value.ToString();
                    query = query + ",Id_categoria3 = " + LstCategorias3.SelectedItem.Value.ToString();
                    query = query + ",Id_subcategoria1 = " + LstSubCategorias1.SelectedItem.Value.ToString();
                    query = query + ",Id_subcategoria2 = " + LstSubCategorias2.SelectedItem.Value.ToString();
                    query = query + ",Id_subcategoria3 = " + LstSubCategorias3.SelectedItem.Value.ToString();
                    // query = query + ",tabla_tecnica ='" + txt_tabla_tecnica.Text.Replace(",", ".").Trim() + "'";
                    query = query + ",tabla_tecnica ='" + lbl_tabla_tecnica.Text.Replace(",", ".").Trim() + "'";

                    query = query + "  WHERE Id_Item = " + id_item;
                }
                else
                {
                    query = "INSERT INTO dilacocl_dilacoweb.tbl_items ";
                    query = query + "(Id_Item, ";
                    query = query + "codigo, ";
                    query = query + "descripcion, ";
                    query = query + "prodpedido, ";
                    // query = query + "codigo_maestro, ";
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
                    query = query + "precio_lista, ";
                    query = query + "id_moneda, ";
                    query = query + "Unidad_vta, ";
                    query = query + "Unidad, ";
                    query = query + "codigo_prov,";
                    query = query + "Caracteristicas, ";
                    query = query + "Manual_tecnico, ";
                    query = query + "Presentacion_producto, ";
                    query = query + "Multiplo, ";
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
                    query = query + "(" + id_item + ",";
                    query = query + "'" + txt_codigo.Text + "',";
                    query = query + "'" + txt_descripcion.Text.Replace(",", ".").Trim() + "',";
                    if (chck_prodped.Checked)
                    { query = query + " 1,"; }
                    else
                    { query = query + " 0,"; }
                    if (chck_visible.Checked)
                    { query = query + " 1,"; }
                    else
                    { query = query + " 0,"; }
                    if (chck_cot.Checked)
                    { query = query + " 1,"; }
                    else
                    { query = query + " 0,"; }
                    if (chck_venta.Checked)
                    { query = query + " 1,"; }
                    else
                    { query = query + " 0,"; }
                    query = query + "'" + txt_texto_destacado.Text + "',";
                    query = query + LstCategorias.SelectedValue.ToString() + ",";
                    query = query + LstSubCategorias.SelectedValue.ToString() + ",";
                    query = query + LstLineaVtas.SelectedValue.ToString() + ",";
                    query = query + "'" + txt_proveedor.Text.Replace(",", ".").Trim() + "',";
                    query = query + "'" + txt_marca.Text.Replace(",", ".").Trim() + "',";
                    query = query + txt_precio.Text.Replace(",", ".") + ",";
                    query = query + txt_precio_lista.Text.Replace(",", ".") + ",";
                    query = query + LstMonedas.SelectedValue.ToString() + ",";
                    query = query + "'" + txt_unidad.Text + "',";
                    query = query + "'" + lbl_unidad.Text + "',";
                    query = query + "'" + txt_codigoprov.Text + "',";
                    // query = query + "'" + txt_caracteristicas.Text.Replace(",", ".").Trim() + "',";
                    query = query + "'" + lbl_caracteristicas.Text.Replace(",", ".").Trim() + "',";
                    query = query + "'" + lbl_manual_tecnico.Text + "',";
                    query = query + "'" + lbl_presentacion.Text + "',";
                    query = query + txt_multiplo.Text.Replace(",", ".") + ",";
                    query = query + "'" + lbl_hoja_seguridad.Text + "',";
                    query = query + "'" + lbl_fotoc.Text + "',";
                    query = query + "'" + lbl_fotog.Text + "',";
                    query = query + "'" + lbl_video.Text + "',";
                    query = query + retorna_id_item(txt_acc1.Text) + ",";
                    query = query + retorna_id_item(txt_acc2.Text) + ",";
                    query = query + retorna_id_item(txt_acc3.Text) + ",";
                    query = query + retorna_id_item(txt_rep1.Text) + ",";
                    query = query + retorna_id_item(txt_rep2.Text) + ",";
                    query = query + retorna_id_item(txt_rep3.Text) + ",";
                    query = query + retorna_id_item(txt_alt1.Text) + ",";
                    query = query + retorna_id_item(txt_alt2.Text) + ",";
                    query = query + retorna_id_item(txt_alt3.Text) + ",";
                    query = query + LstCategorias1.SelectedItem.Value.ToString() + ",";
                    query = query + LstCategorias2.SelectedItem.Value.ToString() + ",";
                    query = query + LstCategorias3.SelectedItem.Value.ToString() + ",";
                    query = query + LstSubCategorias1.SelectedItem.Value.ToString() + ",";
                    query = query + LstSubCategorias1.SelectedItem.Value.ToString() + ",";
                    query = query + LstSubCategorias1.SelectedItem.Value.ToString() + ",";
                    query = query + "'" + lbl_tabla_tecnica.Text.Replace(",", ".").Trim() + "')";
                }

                using (MySqlConnection conn = new MySqlConnection(SMysql))
                {
                    try
                    {
                        conn.Open();

                        MySqlCommand command = new MySqlCommand(query, conn);
                        command.ExecuteNonQuery();
                        conn.Close();
                        conn.Dispose();
                        lbl_status.Text = "Producto grabado correctamente en la Web";

                        marca_producto_publicado(id_item, "I");

                       // actualiza_stock_web(id_item, Convert.ToDouble(lbl_stock.Text));

                        validamysql(id_item);

                        // Si el producto fue grabado correctamente, cargamos los archivos en el servidor
                        // Ficha Técnica
                        if (lbl_manual_tecnico.Text.Trim() != "")
                        {
                            ruta_server = @"/dinamicos/productos/manual_tecnico/";
                            ruta_local = Server.MapPath(@"~/Catalogo/Productos/manual_tecnico/");
                            string result = ftp.Ftp(server, user, password, lbl_manual_tecnico.Text, ruta_local, ruta_server);
                        }
                        // Presentación
                        if (lbl_presentacion.Text.Trim() != "")
                        {
                            ruta_server = @"/dinamicos/productos/presentacion/";
                            ruta_local = Server.MapPath(@"~/Catalogo/Productos/Presentacion/");
                            string result = ftp.Ftp(server, user, password, lbl_presentacion.Text, ruta_local, ruta_server);
                        }
                        // Ficha Técnica
                        if (lbl_hoja_seguridad.Text.Trim() != "")
                        {
                            // debemos verificar que el archivo esta en una ruta antigua y con un nombre distinto... debemos moverlo para copiarlo

                            ruta_server = @"/dinamicos/productos/hds/";
                            ruta_local = Server.MapPath(@"~/Catalogo/Productos/HojaS/");
                            string result = ftp.Ftp(server, user, password, lbl_hoja_seguridad.Text, ruta_local, ruta_server);
                        }
                        // Foto C
                        if (lbl_fotoc.Text.Trim() != "")
                        {
                            ruta_server = @"/dinamicos/productos/img/";
                            ruta_local = Server.MapPath(@"~/Catalogo/Productos/Imagenes/");
                            string result = ftp.Ftp(server, user, password, lbl_fotoc.Text, ruta_local, ruta_server);
                        }
                        // Foto G
                        if (lbl_fotog.Text.Trim() != "")
                        {
                            ruta_server = @"/dinamicos/productos/img/";
                            ruta_local = Server.MapPath(@"~/Catalogo/Productos/Imagenes/");
                            string result = ftp.Ftp(server, user, password, lbl_fotog.Text, ruta_local, ruta_server);
                        }

                        // Video
                        if (lbl_video.Text.Trim() != "")
                        {
                            ruta_server = @"/dinamicos/productos/videos/";
                            ruta_local = Server.MapPath(@"~/Catalogo/Productos/Videos/");
                            string result = ftp.Ftp(server, user, password, lbl_video.Text, ruta_local, ruta_server);
                        }

                    }
                    catch (Exception ex)
                    {
                        lbl_error.Text = ex.Message + query;
                        conn.Close();
                        conn.Dispose();
                    }
                }

                // Una vez cargado o actualizado el producto...revisamos si la familia, categoria y subcategoría estan cargadas al sitio
                if (Chk_crea_data.Checked)
                {
                    Adm_familia_producto(LstDivision.SelectedValue.ToString(), LstCategorias.SelectedValue.ToString(), LstSubCategorias.SelectedValue.ToString());
                }
               
            }
        }


        void Adm_familia_producto( string familia, string categoria, string subcategoria)
        {
            Div_Cat.Visible = false;
            Div_fam.Visible = false;
            Div_Subcat.Visible = false;

            // Validamos cada una de las opciones... si estas no exiten las cargamos en el Sitio
            // Familia
            if (familia != "0")
            {
                if (consulta_familia_mysql("F", Convert.ToInt32(familia), Convert.ToInt32(categoria), Convert.ToInt32(subcategoria)) == 0)
                {
                    // Familia no existe... debmos crearala en la web
                    Carga_familia_mysql("F", Convert.ToInt32(familia), Convert.ToInt32(categoria), Convert.ToInt32(subcategoria));
                }
            }
          
            // Categoria
            if (subcategoria != "'0")
            {
                if (consulta_familia_mysql("C", Convert.ToInt32(familia), Convert.ToInt32(categoria), Convert.ToInt32(subcategoria)) == 0)
                {
                    // Familia no existe... debmos crearala en la web
                    Carga_familia_mysql("C", Convert.ToInt32(familia), Convert.ToInt32(categoria), Convert.ToInt32(subcategoria));
                }
            }
           
            // Subcateroría
            if (subcategoria != " 0")
            {
                if (consulta_familia_mysql("S", Convert.ToInt32(familia), Convert.ToInt32(categoria), Convert.ToInt32(subcategoria)) == 0)
                {
                    // Familia no existe... debmos crearala en la web
                    Carga_familia_mysql("S", Convert.ToInt32(familia), Convert.ToInt32(categoria), Convert.ToInt32(subcategoria));
                }
            }
           
        }

        public string consulta_stock_erp(int id_item)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_consulta_stock_item", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@id_item", id_item);
                    cmd.Parameters["@id_item"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                result = Convert.ToString(rdr.GetDouble(0));
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    return result;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        void actualiza_stock_web (int id_item,double stock, int id_bodega)
        {
            string result = "";
            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("Act_Stock", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_id_item", id_item);
                    cmd.Parameters["@v_id_item"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_stock", stock);
                    cmd.Parameters["@v_stock"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_id_bodega", id_bodega);
                    cmd.Parameters["@v_id_bodega"].Direction = ParameterDirection.Input;

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                result = rdr.GetString(0);
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

        void Carga_familia_mysql(string rama, int familia, int categoria, int subcategoria)
        {
            // rescatamos los valores desde SQLSERVER
            int v_Id_Familia = 0;
            int v_ID_Categoria = 0;
            int v_ID_SubCategoria = 0;
            string v_codigo = "";
            string v_Nombre = "";
            string v_Descripcion = "";
            int v_Tiene_SubCategoria = 0;
            int v_Orden = 0;
            int v_Activo = 0;
            string v_Creado = "";
            int v_Usr_Crea = 0;
            string v_Actualizado = "";
            int v_Usr_Actualiza = 0;

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_carga_info_familia_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_rama", rama);
                    cmd.Parameters["@v_rama"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_fam", familia);
                    cmd.Parameters["@v_fam"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_cat", categoria);
                    cmd.Parameters["@v_cat"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_subcat", subcategoria);
                    cmd.Parameters["@v_subcat"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                v_Id_Familia = Convert.ToInt32(rdr.GetString(0)); 
                                v_ID_Categoria = Convert.ToInt32(rdr.GetString(1)); 
                                v_ID_SubCategoria = Convert.ToInt32(rdr.GetString(2)); 
                                v_codigo = Convert.ToString(rdr.GetString(3)); 
                                v_Nombre = Convert.ToString(rdr.GetString(4)); 
                                v_Descripcion = Convert.ToString(rdr.GetString(5)); 
                                v_Tiene_SubCategoria = Convert.ToInt32(rdr.GetString(6));
                                v_Orden = Convert.ToInt32(rdr.GetString(7));
                                v_Activo = Convert.ToInt32(rdr.GetString(8));
                                v_Creado = Convert.ToString(rdr.GetString(9));
                                v_Usr_Crea = Convert.ToInt32(rdr.GetString(10));
                                v_Actualizado = Convert.ToString(rdr.GetString(11)); 
                                v_Usr_Actualiza = Convert.ToInt32(rdr.GetString(12));

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

            // Unva vez obtenidos los valores del ERP... los enviamos al MYSQL

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Adm_ramas_Sitio", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@v_Id_Familia", v_Id_Familia);
                    cmd.Parameters["@v_Id_Familia"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_ID_Categoria", v_ID_Categoria);
                    cmd.Parameters["@v_ID_Categoria"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_ID_SubCategoria", v_ID_SubCategoria);
                    cmd.Parameters["@v_ID_SubCategoria"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_codigo", v_codigo);
                    cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_Nombre", v_Nombre);
                    cmd.Parameters["@v_Nombre"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_Descripcion", v_Descripcion);
                    cmd.Parameters["@v_Descripcion"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_Tiene_SubCategoria", v_Tiene_SubCategoria);
                    cmd.Parameters["@v_Tiene_SubCategoria"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_Orden", v_Orden);
                    cmd.Parameters["@v_Orden"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_Activo", v_Activo);
                    cmd.Parameters["@v_Activo"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_Usr_Crea", v_Usr_Crea);
                    cmd.Parameters["@v_Usr_Crea"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_Usr_Actualiza", v_Usr_Actualiza);
                    cmd.Parameters["@v_Usr_Actualiza"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                lbl_error.Text = rdr.GetInt32(0).ToString();
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

        public int consulta_familia_mysql (string rama, int familia, int categoria, int subcategoria)
        {
            int retorno = 0;
            using (MySqlConnection connection = new MySqlConnection(SMysql))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("consulta_rama_familia", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter param = new MySqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_rama", rama);
                    cmd.Parameters["@v_rama"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_familia", familia);
                    cmd.Parameters["@v_familia"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_cat", categoria);
                    cmd.Parameters["@v_cat"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@v_subcat", subcategoria);
                    cmd.Parameters["@v_subcat"].Direction = ParameterDirection.Input;
                    
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //lbl_error.Text = rdr.GetInt32(0).ToString();
                                retorno = Convert.ToInt32(rdr.GetString(0));
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return retorno;
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

        void marca_producto_publicado(int id_item, string orden)
        {
            string query = "";
            if (orden == "I")
            {
                query = "update tbl_items_web set publicado_sitio = 1 where Id_Item = " + id_item;
            }
            else
            {
                query = "update tbl_items_web set publicado_sitio = 0 where Id_Item = " + id_item;
            }
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


        void desactiva_producto()
        {
            string query = "";
            query = "update tbl_items_web set activo = 0, visible = 0 where codigo = '" + txt_codigo.Text + "'";

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

            query = "update tbl_items set Publicar_Web = 0 where codigo = '" + txt_codigo.Text + "'";

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

            query = "update dilacocl_dilacoweb.tbl_items set visible = 0 where codigo = '" + txt_codigo.Text + "'";

            // MYSQL
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }

        }


        void subir_archivo(FileUpload file, string achivo, string ruta, Label etiqueta)
        {
            String savePath = ruta;

            // Before attempting to perform operations
            // on the file, verify that the FileUpload 
            // control contains a file.


            if (file.HasFile)
            {
                // Get the name of the file to upload.
                String fileName = file.FileName;

                // Append the name of the file to upload to the path.
                savePath += fileName;
                file.SaveAs(savePath);
                // si lo pudo subir renomabramos el archivo
                etiqueta.Text = fileName;
            }
            else
            {
                // Notify the user that a file was not uploaded.
                lbl_error.Text = "Error al subir Archivo";
            }
        }

        protected void ImgBtnFT_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = txt_codigo.Text;
            if (File_FT.HasFile)
            {
                int tamano = File_FT.PostedFile.ContentLength;
                // verficamos que el archivo no pese mas de 5 MB
                if (tamano <= 5000000)
                {
                    string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".txt", ".xls", ".xlsx", ".xppt", ".xppt" };
                    administra_archivos(File_FT, Server.MapPath(@"~/Catalogo/Productos/Manual_tecnico/"), "MT_" + codigo, lbl_manual_tecnico, allowedExtensions);
                }
                else
                {
                    lbl_error.Text = "Tamaño de archivo Manual Técnico no puede exceder los 5 MB";
                }
            }

            //subir_archivo(File_FT, "FT_" + codigo, Server.MapPath(@"~/Catalogo/Productos/Manual/"), lbl_manual_tecnico);
        }

        protected void ImgBtnFG_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = txt_codigo.Text;
            if (File_FG.HasFile)
            {
                int tamano = File_FG.PostedFile.ContentLength;
                // verficamos que el archivo no pese mas de 5 MB
                if (tamano <= 5000000)
                {
                    string[] allowedExtensions = { ".png", ".gif", ".jpg", ".bpm" };
                    administra_archivos(File_FG, Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), "FG_" + codigo.Trim(), lbl_fotog, allowedExtensions);
                    //subir_archivo(File_FG, "FG_" + codigo, Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), lbl_fotog);
                }
                else
                {
                    lbl_error.Text = "Tamaño de archivo Foto Grande no puede exceder los 5 MB";
                }
                img_prod.ImageUrl = "~/Catalogo/Productos/Imagenes//" + Path.GetFileName(File_FG.FileName);
            }
            //    img_prod.ImageUrl = Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), lbl_fotog.Text);

        }

        protected void ImgBtnFC_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = txt_codigo.Text;
            if (File_FC.HasFile)
            {
                int tamano = File_FC.PostedFile.ContentLength;
                // verficamos que el archivo no pese mas de 5 MB
                if (tamano <= 5000000)
                {
                    string[] allowedExtensions = { ".png", ".gif", ".jpg", ".bpm" };
                    administra_archivos(File_FC, Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), "FC_" + codigo.Trim(), lbl_fotoc, allowedExtensions);
                    //subir_archivo(File_FC, "FC_" + codigo, Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), lbl_fotoc);
                }
                else
                {
                    lbl_error.Text = "Tamaño de archivo Foto Pequeña no puede exceder los 5 MB";
                }
            }
        }

        protected void ImgBtnPRE_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = txt_codigo.Text;
            if (File_PRE.HasFile)
            {
                int tamano = File_PRE.PostedFile.ContentLength;
                // verficamos que el archivo no pese mas de 5 MB
                if (tamano <= 5000000)
                {
                    string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".txt", ".xls", ".xlsx", ".xppt", ".xppt" };
                    administra_archivos(File_PRE, Server.MapPath(@"~/Catalogo/Productos/Presentacion/"), "PR_" + codigo.Trim(), lbl_presentacion, allowedExtensions);
                    //subir_archivo(File_PRE, "PR_" + codigo, Server.MapPath(@"~/Catalogo/Productos/Presentacion/"), lbl_presentacion);
                }
                else
                {
                    lbl_error.Text = "Tamaño de archivo Presentación no puede exceder los 5 MB";
                }
            }
        }

        protected void ImgBtnVID_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = txt_codigo.Text;
            if (File_VID.HasFile)
            {
                int tamano = File_VID.PostedFile.ContentLength;
                // verficamos que el archivo no pese mas de 5 MB
                if (tamano <= 5000000)
                {
                    string[] allowedExtensions = { ".mp4", ".avi", ".m4v", ".mov", ".mpg", ".mpeg", ".wmv" };
                    administra_archivos(File_VID, Server.MapPath(@"~/Catalogo/Productos/Videos/"), "VD_" + codigo.Trim(), lbl_video, allowedExtensions);
                    //subir_archivo(File_VID, "VD_" + codigo, Server.MapPath(@"~/Catalogo/Productos/Videos/"), lbl_video);
                }
                else
                {
                    lbl_error.Text = "Tamaño de archivo Video no puede exceder los 5 MB";
                }
            }
        }


        protected void ImgBtnHS_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = txt_codigo.Text;
            if (File_HS.HasFile)
            {
                int tamano = File_HS.PostedFile.ContentLength;
                // verficamos que el archivo no pese mas de 5 MB
                if (tamano <= 5000000)
                {
                    string[] allowedExtensions = { ".pdf", ".doc", ".xdoc", ".txt", ".xls", ".xlsx", ".xppt", ".xppt" };
                    administra_archivos(File_HS, Server.MapPath(@"~/Catalogo/Productos/HojaS/"), "HS_" + codigo.Trim(), lbl_hoja_seguridad, allowedExtensions);
                    //subir_archivo(File_HS, "HS_" + codigo, Server.MapPath(@"~/Catalogo/Productos/HojaS/"), lbl_hoja_presentacion);
                }
                else
                {
                    lbl_error.Text = "Tamaño de archivo Hoja de Seguridad no puede exceder los 5 MB";
                }
            }
        }

        protected void BtnGrabar_Click(object sender, EventArgs e)
        {
            if (lbl_activo.Text == "SI")
            {
                Grabar();
            }
            else
            {
                lbl_error.Text = "Producto está desactivado, no se puede actualizar";
            }
        }

        void Grabar()
        {
            string query = "";
            lbl_error.Text = "";
            float v_precio = busca_precio_lista(id_item);

            if (txt_precio.Text == "0" && chck_venta.Checked)
            {
                lbl_error.Text = "Precio del producto debe ser mayor a Cero";
            }
            if (chck_venta.Checked && verifica_stock_erp(txt_codigo.Text.Trim()) == "N")
            {
                lbl_status.Text = "No puede actualizar producto para Ventas si no ha regularizado Stock, consulte con su Administrador";
                chck_venta.Checked = false;
            }

            query = "UPDATE dbo.tbl_items_web ";
            query = query + "SET ";
            query = query + " descripcion = '" + txt_descripcion.Text.Replace(",", ".").Trim() + "'";
            if (chck_prodped.Checked)
            { query = query + ", prodpedido = 1"; }
            else
            { query = query + ", prodpedido = 0"; }
            if (chck_visible.Checked)
            { query = query + ", visible = 1"; }
            else
            { query = query + ", visible = 0"; }
            if (chck_cot.Checked)
            { query = query + ", cotizaciones = 1"; }
            else
            { query = query + ", cotizaciones = 0"; }
            if (chck_venta.Checked)
            { query = query + ", ventas = 1"; }
            else
            { query = query + ", ventas = 0"; }
            query = query + ",texto_destacado = '" + txt_texto_destacado.Text.Replace(",", ".").Trim() + "'";
            query = query + ",Id_Categoria = " + LstCategorias.SelectedValue.ToString();
            query = query + ",Id_Subcategoria = " + LstSubCategorias.SelectedValue.ToString();
            query = query + ",Id_Linea_Venta = " + LstLineaVtas.SelectedValue.ToString();
            query = query + ",Marca = '" + txt_marca.Text + "'";
            query = query + ",Codigo_prov = '" + txt_codigoprov.Text + "'";
            query = query + ",precio = " + txt_precio.Text;
            query = query + ",id_moneda = " + LstMonedas.SelectedItem.Value.ToString();// LstMonedas.SelectedValue.ToString();
            query = query + ",Unidad_vta = '" + txt_unidad.Text + "'";
            query = query + ",Caracteristicas = '" + lbl_caracteristicas.Text.Replace(",", ".").Trim() + "'";


            query = query + ",Manual_tecnico = '" + lbl_manual_tecnico.Text + "'";
            query = query + ",Presentacion_producto = '" + lbl_presentacion.Text + "'";
            query = query + ",Multiplo = " + txt_multiplo.Text.Replace(",", ".");
            query = query + ",Hoja_de_Seguridad = '" + lbl_hoja_seguridad.Text.Trim() + "'";
            query = query + ",Foto = '" + lbl_fotoc.Text + "'";
            query = query + ",Foto_grande = '" + lbl_fotog.Text + "'";
            //query = query + ",Foto_Maestra = NULL";
            query = query + ",Video = '" + lbl_video.Text + "'";
            query = query + ",Id_Accesorio1 = " + retorna_id_item(txt_acc1.Text);
            query = query + ",Id_Accesorio2 = " + retorna_id_item(txt_acc2.Text);
            query = query + ",Id_Accesorio3 = " + retorna_id_item(txt_acc3.Text);
            query = query + ",Id_Repuesto1 = " + retorna_id_item(txt_rep1.Text);
            query = query + ",Id_Repuesto2 = " + retorna_id_item(txt_rep2.Text);
            query = query + ",Id_Repuesto3 = " + retorna_id_item(txt_rep3.Text);
            query = query + ",Id_Alternativa1 = " + retorna_id_item(txt_alt1.Text);
            query = query + ",Id_Alternativa2 = " + retorna_id_item(txt_alt2.Text);
            query = query + ",Id_Alternativa3 = " + retorna_id_item(txt_alt3.Text);
            query = query + ",Id_categoria1 = " + LstCategorias1.SelectedItem.Value.ToString();
            query = query + ",Id_categoria2 = " + LstCategorias2.SelectedItem.Value.ToString();
            query = query + ",Id_categoria3 = " + LstCategorias3.SelectedItem.Value.ToString();
            query = query + ",Id_subcategoria1 = " + LstSubCategorias1.SelectedItem.Value.ToString();
            query = query + ",Id_subcategoria2 = " + LstSubCategorias2.SelectedItem.Value.ToString();
            query = query + ",Id_subcategoria3 = " + LstSubCategorias3.SelectedItem.Value.ToString();
            query = query + ",tabla_tecnica ='" + lbl_tabla_tecnica.Text.Replace(",", ".").Trim() + "'";

            query = query + "  WHERE Id_Item = " + id_item;

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();


                    if (lbl_manual_tecnico.Text.Trim() != "")
                    {
                        if (!File.Exists(Path.Combine(ruta_alterna, lbl_manual_tecnico.Text.Trim())))
                        {
                            File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Manual_tecnico/"), lbl_manual_tecnico.Text), Path.Combine(ruta_alterna, lbl_manual_tecnico.Text.Trim()));
                        }
                        else
                        {
                            //File.Delete(Path.Combine(ruta_alterna, lbl_manual_tecnico.Text.Trim()));
                            if (!File.Exists(Path.Combine(ruta_alterna, lbl_manual_tecnico.Text.Trim())))
                            {
                                File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Manual_tecnico/"), lbl_manual_tecnico.Text), Path.Combine(ruta_alterna, lbl_manual_tecnico.Text.Trim()));
                            }
                        }

                    }

                    if (lbl_presentacion.Text.Trim() != "")
                    {
                        if (!File.Exists(Path.Combine(ruta_alterna, lbl_presentacion.Text.Trim())))
                        {
                            File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Presentacion/"), lbl_presentacion.Text), Path.Combine(ruta_alterna, lbl_presentacion.Text.Trim()));
                        }
                        else
                        {
                            // File.Delete(Path.Combine(ruta_alterna, lbl_presentacion.Text.Trim()));
                            if (!File.Exists(Path.Combine(ruta_alterna, lbl_presentacion.Text.Trim())))
                            {
                                File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Presentacion/"), lbl_presentacion.Text), Path.Combine(ruta_alterna, lbl_presentacion.Text.Trim()));
                            }

                        }
                    }

                    if (lbl_hoja_seguridad.Text.Trim() != "")
                    {
                        if (!File.Exists(Path.Combine(ruta_alterna, lbl_hoja_seguridad.Text.Trim())))
                        {
                            File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/HojaS/"), lbl_hoja_seguridad.Text.Trim()), Path.Combine(ruta_alterna, lbl_hoja_seguridad.Text.Trim()));
                        }
                        else
                        {
                            if (!File.Exists(Path.Combine(ruta_alterna, lbl_hoja_seguridad.Text.Trim())))
                            {
                                File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/HojaS/"), lbl_hoja_seguridad.Text.Trim()), Path.Combine(ruta_alterna, lbl_hoja_seguridad.Text.Trim()));
                            }
                            //File.Delete(Path.Combine(ruta_alterna, lbl_hoja_seguridad.Text.Trim()));

                        }

                    }

                    if (lbl_fotoc.Text.Trim() != "")
                    {
                        if (!File.Exists(Path.Combine(ruta_alterna, lbl_fotoc.Text.Trim())))
                        {
                            File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), lbl_fotoc.Text.Trim()), Path.Combine(ruta_alterna, lbl_fotoc.Text.Trim()));
                        }
                        else
                        {
                            // File.Delete(Path.Combine(ruta_alterna, lbl_fotoc.Text.Trim()));
                            if (!File.Exists(Path.Combine(ruta_alterna, lbl_fotoc.Text.Trim())))
                            {
                                File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), lbl_fotoc.Text.Trim()), Path.Combine(ruta_alterna, lbl_fotoc.Text.Trim()));
                            }

                        }
                    }

                    if (lbl_fotog.Text.Trim() != "")
                    {
                        if (!File.Exists(Path.Combine(ruta_alterna, lbl_fotog.Text.Trim())))
                        {
                            File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), lbl_fotog.Text.Trim()), Path.Combine(ruta_alterna, lbl_fotog.Text.Trim()));
                        }
                        else
                        {
                            if (!File.Exists(Path.Combine(ruta_alterna, lbl_fotog.Text.Trim())))
                            {
                                File.Copy(Path.Combine(Server.MapPath(@"~/Catalogo/Productos/Imagenes/"), lbl_fotog.Text.Trim()), Path.Combine(ruta_alterna, lbl_fotog.Text.Trim()));
                            }
                            //File.Delete(Path.Combine(ruta_alterna, lbl_fotog.Text.Trim()));  
                        }

                    }

                    lbl_status.Text = "Actualización realizada correctamente";
                    LstProdDispAc1.Visible = false;
                    LstProdDispAc2.Visible = false;
                    LstProdDispAc3.Visible = false;
                    LstProdDispAl1.Visible = false;
                    LstProdDispAl2.Visible = false;
                    LstProdDispAl3.Visible = false;
                    LstProdDispRe1.Visible = false;
                    LstProdDispRe2.Visible = false;
                    LstProdDispRe3.Visible = false;
                    ImgBtnAddAC1.Visible = false;
                    ImgBtnAddAC2.Visible = false;
                    ImgBtnAddAC3.Visible = false;
                    ImgBtnAddRE1.Visible = false;
                    ImgBtnAddRE2.Visible = false;
                    ImgBtnAddRE3.Visible = false;
                    ImgBtnAddAL1.Visible = false;
                    ImgBtnAddAL2.Visible = false;
                    ImgBtnAddAL3.Visible = false;

                    //if (v_precio != Convert.ToDouble(txt_precio_lista.Text))
                    //{
                    //    // ejecutamos la función que creará el historial de modificaciones
                    //    utiles.actualiza_historial_nv(id_item, usuario, "Precio Item Web cambia de " + Convert.ToString(v_precio) + " a " + Convert.ToString(txt_precio.Text), Sserver, "ITEM");
                    //}

                    
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    connection.Close();
                    connection.Dispose();
                }
            }

            // Actualizamos tabla tbl_items
            query = "update tbl_Items set Id_Categoria = " + LstCategorias.SelectedValue.ToString() + ", Id_Linea_Venta= " + LstLineaVtas.SelectedValue.ToString() + ", Id_SubCategoria = " + LstSubCategorias.SelectedValue.ToString() + " where ID_Item = " + id_item;
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

        public string verifica_stock_web(string codigo)
        {
            
            string query = "";
            string result = "";
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    query = "revisa_stock_prod_vtas";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_codigo", codigo);
                    command.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            result = dr.GetString(0);
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                     return result;
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                    return "N";
                }
            }
        }

        public string verifica_stock_erp(string codigo)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_revisa_stock_prod_vtas", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros

                    cmd.Parameters.AddWithValue("@v_codigo", codigo);
                    cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

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

                    connection.Close();
                    connection.Dispose();

                    return result;

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    return "N";
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public float busca_precio_lista(int id_item)
        {
            string query = "";
            float valor = 0;
            query = "select precio from tbl_items where id_item = " + id_item;

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        valor = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                    connection.Close();
                    connection.Dispose();
                    return valor;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    lbl_error.Text = ex.Message;
                    connection.Close();
                    connection.Dispose();
                    return 0;
                }

            }

        }

        protected void LstCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstCategorias.SelectedValue.ToString();

            LstSubCategorias.Items.Clear();
            LstSubCategorias.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria =" + valor, LstSubCategorias, "tbl_categorias", "ID_SubCategoria", "Nombre");
        }

        protected void LstCategorias1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstCategorias1.SelectedValue.ToString();
            LstSubCategorias1.Items.Clear();
            LstSubCategorias1.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria =" + valor, LstSubCategorias1, "tbl_categorias", "ID_SubCategoria", "Nombre");
          //  seleccion_valores(LstCategorias1, LstSubCategorias1);
        }

        protected void LstCategorias2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstCategorias2.SelectedValue.ToString();
            LstSubCategorias2.Items.Clear();
            LstSubCategorias2.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria =" + valor, LstSubCategorias2, "tbl_categorias", "ID_SubCategoria", "Nombre");
        }

        protected void LstCategorias3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstCategorias3.SelectedValue.ToString();
            LstSubCategorias3.Items.Clear();
            LstSubCategorias3.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria =" + valor, LstSubCategorias3, "tbl_categorias", "ID_SubCategoria", "Nombre");
        }

        void seleccion_valores(DropDownList lista, DropDownList sublista)
        {
            string valor = lista.SelectedValue.ToString();
            carga_contrl_lista("select ID_SubCategoria, Nombre from tbl_Subcategorias where Activo = 1 and id_categoria =" + valor, sublista, "tbl_categorias", "ID_SubCategoria", "Nombre");
        }

        protected void borra_acc1_Click(object sender, ImageClickEventArgs e)
        {
            txt_acc1.Text = "";
        }

        protected void borra_acc2_Click(object sender, ImageClickEventArgs e)
        {
            txt_acc2.Text = "";
        }

        protected void borra_acc3_Click(object sender, ImageClickEventArgs e)
        {
            txt_acc3.Text = "";
        }

        protected void borra_rep1_Click(object sender, ImageClickEventArgs e)
        {
            txt_rep1.Text = "";
        }

        protected void borra_rep2_Click(object sender, ImageClickEventArgs e)
        {
            txt_rep2.Text = "";
        }

        protected void borra_rep3_Click(object sender, ImageClickEventArgs e)
        {
            txt_rep3.Text = "";
        }

        protected void borra_alt1_Click(object sender, ImageClickEventArgs e)
        {
            txt_alt1.Text = "";
        }

        protected void borra_alt2_Click(object sender, ImageClickEventArgs e)
        {
            txt_alt2.Text = "";
        }

        protected void borra_alt3_Click(object sender, ImageClickEventArgs e)
        {
            txt_alt3.Text = "";
        }

        protected void ImgBtnAc1_Click(object sender, ImageClickEventArgs e)
        {
            LstProdDispAc1.Visible = true;
            ImgBtnAddAC1.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where  Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispAc1, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnAc2_Click(object sender, ImageClickEventArgs e)
        {
            LstProdDispAc2.Visible = true;
            ImgBtnAddAC2.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispAc2, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnAc3_Click(object sender, ImageClickEventArgs e)
        {
            LstProdDispAc3.Visible = true;
            ImgBtnAddAC3.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispAc3, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnRe1_Click(object sender, ImageClickEventArgs e)
        {
            ImgBtnAddRE1.Visible = true;
            LstProdDispRe1.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispRe1, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnRe2_Click(object sender, ImageClickEventArgs e)
        {
            ImgBtnAddRE2.Visible = true;
            LstProdDispRe2.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispRe2, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnRe3_Click(object sender, ImageClickEventArgs e)
        {
            ImgBtnAddRE3.Visible = true;
            LstProdDispRe3.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispRe3, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnAl1_Click(object sender, ImageClickEventArgs e)
        {
            ImgBtnAddAL1.Visible = true;
            LstProdDispAl1.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispAl1, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnAl2_Click(object sender, ImageClickEventArgs e)
        {
            ImgBtnAddAL2.Visible = true;
            LstProdDispAl2.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispAl2, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnAl3_Click(object sender, ImageClickEventArgs e)
        {
            ImgBtnAddAL3.Visible = true;
            LstProdDispAl3.Visible = true;
            carga_contrl_lista("select id_item, CONCAT(Codigo,' ', SUBSTRING(descripcion,1,40)) Codigo from tbl_items_web where Codigo <> '" + txt_codigo.Text + "' and id_linea_venta = " + LstLineaVtas.SelectedValue + " order by codigo ", LstProdDispAl3, "tbl_monedas", "id_item", "Codigo");
        }

        protected void ImgBtnAddAC1_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispAc1.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispAc1.Visible = false;
            ImgBtnAddAC1.Visible = false;
            txt_acc1.Text = codigo;
        }

        protected void ImgBtnAddAC2_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispAc2.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispAc2.Visible = false;
            ImgBtnAddAC2.Visible = false;
            txt_acc2.Text = codigo;
        }

        protected void ImgBtnAddAC3_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispAc3.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispAc3.Visible = false;
            ImgBtnAddAC3.Visible = false;
            txt_acc3.Text = codigo;
        }

        protected void ImgBtnAddRE1_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispRe1.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispRe1.Visible = false;
            ImgBtnAddRE1.Visible = false;
            txt_rep1.Text = codigo;
        }

        protected void ImgBtnAddRE2_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispRe2.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispRe2.Visible = false;
            ImgBtnAddRE2.Visible = false;
            txt_rep2.Text = codigo;
        }

        protected void ImgBtnAddRE3_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispRe3.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispRe3.Visible = false;
            ImgBtnAddRE3.Visible = false;
            txt_rep3.Text = codigo;
        }

        protected void ImgBtnAddAL1_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispAl1.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispAl1.Visible = false;
            ImgBtnAddAL1.Visible = false;
            txt_alt1.Text = codigo;
        }

        protected void ImgBtnAddAL2_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispAl2.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispAl2.Visible = false;
            ImgBtnAddAL2.Visible = false;
            txt_alt2.Text = codigo;
        }

        protected void ImgBtnAddAL3_Click(object sender, ImageClickEventArgs e)
        {
            string codigo = LstProdDispAl3.SelectedItem.ToString().Substring(0, 9).Trim();
            LstProdDispAl3.Visible = false;
            ImgBtnAddAL3.Visible = false;
            txt_alt3.Text = codigo;
        }

        public int retorna_id_item(string codigo)
        {
            int valor = 0;
            string query = "select id_item from tbl_items where codigo = '" + codigo + "'";
            if (codigo == null)
            {
                valor = 0;
            }
            else
            {

                using (SqlConnection connection = new SqlConnection(Sserver))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            valor = Convert.ToInt32(reader[0].ToString());
                        }
                        reader.Close();
                        connection.Close();
                        connection.Dispose();
                        validamysql(id_item);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        lbl_error.Text = ex.Message;
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }
            return valor;
        }

        public Boolean administra_archivos(FileUpload archivo, string ruta, string nombre_file, Label etiqueta, string[] tipofiles)
        {
            string nuevo_file = "";
            string extension = System.IO.Path.GetExtension(archivo.FileName);
            // si archivo ya existe... lo renombro pra guardar la versión anterior en caso de error
            if (archivo.HasFile)
            {
                //if (System.IO.File.Exists(@ruta + nombre_file.Trim() + extension))
                //{
                //    System.IO.File.Move(@ruta + nombre_file.Trim() + extension, @ruta + nombre_file.Trim() + ".bkp");
                //}

                for (int i = 0; i < tipofiles.Length; i++)
                {
                    if (extension == tipofiles[i])
                    {
                        try
                        {
                            string s_newfilename = DateTime.Now.Year.ToString() +
                                DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
                                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + extension;
                            nuevo_file = nombre_file.Trim() + extension;
                            archivo.PostedFile.SaveAs(@ruta + nuevo_file);
                            etiqueta.Text = nuevo_file;
                            //imagepath = ImageSavedPath + s_newfilename;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            lbl_error.Text = "Error al cargar Imagen " + ex.Message.ToString();
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void borra_manual_tecnico_Click(object sender, ImageClickEventArgs e)
        {
            lbl_manual_tecnico.Text = "";
        }

        protected void borra_presentacion_Click(object sender, ImageClickEventArgs e)
        {
            lbl_presentacion.Text = "";
        }

        protected void borra_fotog_Click(object sender, ImageClickEventArgs e)
        {
            lbl_fotog.Text = "";
        }

        protected void borra_fotoc_Click(object sender, ImageClickEventArgs e)
        {
            lbl_fotoc.Text = "";
        }

        protected void borra_video_Click(object sender, ImageClickEventArgs e)
        {
            lbl_video.Text = "";
        }

        protected void borra_hoja_seg_Click(object sender, ImageClickEventArgs e)
        {
            lbl_hoja_seguridad.Text = "";
        }

       /* protected void txt_precio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Convert the text to a Double and determine if it is a negative number.
                if (double.Parse(txt_precio.Text) < 0)
                {
                    // If the number is negative, display it in Red.
                    txt_precio.ForeColor = Color.Red;
                }
                else
                {
                    // If the number is not negative, display it in Black.
                    txt_precio.ForeColor = Color.Black;
                }
            }
            catch
            {
                // If there is an error, display the text using the system colors.
                txt_precio.ForeColor = SystemColors.ControlText;
                txt_precio.ForeColor = Color.Red;
                txt_precio.Text = "";
            }
        }*/

        protected void LstDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = LstDivision.SelectedValue.ToString();
            LstSubCategorias.Items.Clear();
            LstSubCategorias.DataBind();
            LstSubCategorias.Items.Clear();

            LstCategorias.Items.Clear();
            LstCategorias.DataBind();

            LstCategorias.Items.Add(new ListItem("Seleccione", "0", true));
            carga_contrl_lista("select ID_Categoria, Nombre from tbl_categorias where Activo = 1 and id_familia =" + valor + " order by Nombre", LstCategorias, "tbl_categorias", "ID_Categoria", "Nombre");

        }

        protected void Btn_act_superior_Click(object sender, EventArgs e)
        {
            actualiza();
           /* if (lbl_activo.Text == "SI")
            {
                actualiza();
            }
            else
            {
                lbl_error.Text = "Producto está desactivado, no se puede actualizar";
            }*/
        }

        protected void LinkAct_item_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (lbl_activo.Text == "NO")
                {
                    activa_item();
                    ImgBtnAct_item.Visible = false;
                    muestra_info(id_item);
                    lbl_status.Text = "Producto activado... Visible en el Sitio Web";
                }
            }
        }

        void activa_item()
        {
            // SQL
            string query = "";
            query = "update tbl_items_web set activo = 1, visible = 1 where codigo = '" + txt_codigo.Text + "'";

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

            query = "update tbl_items set Publicar_Web = 1 where codigo = '" + txt_codigo.Text + "'";

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

            query = "update dilacocl_dilacoweb.tbl_items set visible = 1 where codigo = '" + txt_codigo.Text + "'";

            // MYSQL
            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void LinkDesAct_item_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (lbl_activo.Text == "SI")
                {
                    desactiva_producto();
                    ImgBtnDesAct_item.Visible = false;
                    muestra_info(id_item);
                   // elimna_item();
                    lbl_status.Text = "Código desactivado y deja de estar publicado en el Sitio Web";
                }
            }
        }

        void carga_contrl_lista_congrupo(string sql, DropDownList lista, string tabla, string llave, string Campo)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                connection.Open();
                //SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter reader = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                reader.Fill(ds, tabla);
                ListItem newItem;
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                
                foreach (DataRow drow in dt.Rows)
                {
                    newItem = new ListItem(drow["Nombre"].ToString(), drow["ID"].ToString());
                    newItem.Attributes["data-category"] = drow["Familia"].ToString();
                    lista.Items.Add(newItem);
                }
                //    lista.DataSource = lista;
                lista.DataValueField = llave;
                lista.DataTextField = Campo;
                
                lista.DataBind();
                connection.Close();
                connection.Dispose();
            }
        }

        protected void ImgHTmlCar_Click(object sender, ImageClickEventArgs e)
       {
            lbl_caracteristicas.Visible = false;
            txt_caracteristicas.Text = lbl_caracteristicas.Text;
            txt_caracteristicas.Visible = true;
            ImgGrabaCar.Visible = true;
            ImgVerCar.Visible = true;
            ImgHTmlCar.Visible = false;      
        }


        protected void ImgVerCar_Click(object sender, ImageClickEventArgs e)
        {
            lbl_caracteristicas.Visible = true;
            txt_caracteristicas.Visible = false;
            ImgVerCar.Visible = false;
            ImgHTmlCar.Visible = true;
            ImgGrabaCar.Visible = false;
            /*if (lbl_caracteristicas.Text == "")
            {
                lbl_caracteristicas.Visible = false;
                txt_caracteristicas.Visible = true;
            }*/
        }

        protected void ImgGrabaCar_Click(object sender, ImageClickEventArgs e)
        {
            lbl_caracteristicas.Text = txt_caracteristicas.Text;
            txt_caracteristicas.Text = "";
            txt_caracteristicas.Visible = false;
            lbl_caracteristicas.Visible = true;
            ImgHTmlCar.Visible = true;
            ImgVerCar.Visible = false;
            ImgGrabaCar.Visible = false;

        }

        protected void ImgHTmlTec_Click(object sender, ImageClickEventArgs e)
        {
            lbl_tabla_tecnica.Visible = false;
            txt_tabla_tecnica.Text = lbl_tabla_tecnica.Text;
            txt_tabla_tecnica.Visible = true;
            ImgGrabaTec.Visible = true;
            ImgVerTec.Visible = true;
            ImgHTmltec.Visible = false;
        }

        protected void ImgVerTec_Click(object sender, ImageClickEventArgs e)
        {
            lbl_tabla_tecnica.Visible = true;
            txt_tabla_tecnica.Visible = false;
            ImgVerTec.Visible = false;
            ImgHTmltec.Visible = true;
            ImgGrabaTec.Visible = false;
           /* if (lbl_tabla_tecnica.Text == "")
            {
                lbl_tabla_tecnica.Visible = false;
                txt_tabla_tecnica.Visible = true;
            }*/
        }

        protected void ImgGrabaTec_Click(object sender, ImageClickEventArgs e)
        {
            lbl_tabla_tecnica.Text = txt_tabla_tecnica.Text;
            txt_tabla_tecnica.Text = "";
            txt_tabla_tecnica.Visible = false;
            lbl_tabla_tecnica.Visible = true;
            ImgHTmltec.Visible = true;
            ImgVerTec.Visible = false;
            ImgGrabaTec.Visible = false;
        }

        protected void ImgGrabaCar_Click1(object sender, ImageClickEventArgs e)
        {
            lbl_caracteristicas.Text = txt_caracteristicas.Text;
            txt_caracteristicas.Text = "";
            txt_caracteristicas.Visible = false;
            lbl_caracteristicas.Visible = true;
            ImgHTmlCar.Visible = true;
            ImgVerCar.Visible = false;
            ImgGrabaCar.Visible = false;
        }

        protected void Btn_eliminar_todo_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                elimna_item();
                elimina_producto_ficha_erp();
                if (v_completa == "OK")
                {
                    lbl_error.Text = "Producto Eliminado de Categoría Web";
                }
                else
                {
                    lbl_error.Text = "Producto NO Eliminado de Categoría Web";
                }
            }
        }

        void elimina_producto_ficha_erp()
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("web_elimina_producto_web", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@v_codigo", txt_codigo.Text.Trim());
                    cmd.Parameters["@v_codigo"].Direction = ParameterDirection.Input;

                    
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //lbl_error.Text = rdr.GetInt32(0).ToString();
                                v_completa = rdr.GetString(0);
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

        protected void ImgBtnAct_item_Click(object sender, ImageClickEventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (lbl_activo.Text == "NO")
                {
                    activa_item();
                    ImgBtnAct_item.Visible = false;
                    muestra_info(id_item);
                    lbl_status.Text = "Producto activado... Visible en el Sitio Web";
                }
            }
        }

        protected void ImgBtnDesAct_item_Click(object sender, ImageClickEventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (lbl_activo.Text == "SI")
                {
                    desactiva_producto();
                    ImgBtnDesAct_item.Visible = false;
                    muestra_info(id_item);
                    // elimna_item();
                    lbl_status.Text = "Código desactivado y deja de estar publicado en el Sitio Web";
                }
            }
        }

        protected void Lnk_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Items_ppal.aspx");
        }

        protected void LnkBtn_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Items_ppal.aspx");
        }

        protected void LnkBtn_Actualizar_Click(object sender, EventArgs e)
        {
            if (lbl_activo.Text == "SI")
            {
                Grabar();
            }
            else
            {
                lbl_error.Text = "Producto está desactivado, no se puede actualizar";
            }
        }

        protected void LnkBtn_ActWeb_Click(object sender, EventArgs e)
        {
            actualiza();
        }

        protected void LnkBtn_Eliminar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                elimna_item();
                elimina_producto_ficha_erp();
                if (v_completa == "OK")
                {
                    lbl_error.Text = "Producto Eliminado de Categoría Web";
                }
                else
                {
                    lbl_error.Text = "Producto NO Eliminado de Categoría Web";
                }
            }
        }
    }
 }
