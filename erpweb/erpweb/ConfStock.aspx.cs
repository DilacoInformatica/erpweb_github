using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;

namespace erpweb
{
    public partial class ConfStock : System.Web.UI.Page
    {
        string Sserver = ""; // Conexion Servidor
        string SMysql = ""; // Conexion Server
        string SMysql2 = "";

        Cls_Utilitarios utiles = new Cls_Utilitarios();
        string usuario = "";
        string iniciales_user = "";
        DataList listado = new DataList();

        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("SSERVER"));
            SMysql = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("MYSQL"));
            SMysql2 = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("MYSQL2")); // enlace a BBDD Ecommerce
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

            Btn_grabar.Attributes["Onclick"] = "return confirm('Grabar nivel ingresado')";

            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_009_10", Sserver) == "NO")
                    {
                        Response.Redirect("ErrorAcceso.html");
                    }
                    lbl_conectado.Text = Session["Usuario"].ToString();
                }

                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "Ambiente Desarrollo"; }
                else
                { lbl_ambiente.Text = "Ambiente Producción"; }
                if (utiles.retorna_ambiente() == "D")
                { lbl_ambiente.Text = "Ambiente Desarrollo"; }
                else
                { lbl_ambiente.Text = "Ambiente Producción"; }

                Sserver = utiles.verifica_ambiente("SSERVER");
                SMysql = utiles.verifica_ambiente("MYSQL");


            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }


            carga_contrl_lista("select ID_Bodega, Nombre_Bodega from tbl_Bodegas where Activo = 1", Lst_Bodegas, "tbl_Familias_Productos", "ID_Bodega", "Nombre_Bodega");

            // lbl_usuario.Text = iniciales_user;

            if (!Page.IsPostBack)
            {
                ejecuta_crud("L", 0, "", 0, 0, 0, 0,0,0);
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

        void ejecuta_crud(string mov, int id_nivel, string nivel, int v_min, int v_max, int id_bodega, int activo, int stock, int stock_critico)
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter da = new SqlDataAdapter("web_adm_crud_stock_web", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.SelectCommand.Parameters.AddWithValue("@v_mov", mov);
                    da.SelectCommand.Parameters["@v_mov"].Direction = ParameterDirection.Input;

                    if (id_nivel == 0)
                    { da.SelectCommand.Parameters.AddWithValue("@v_id_nivel", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_id_nivel", id_nivel); }

                    da.SelectCommand.Parameters["@v_id_nivel"].Direction = ParameterDirection.Input;

                    if (nivel == "")
                    { da.SelectCommand.Parameters.AddWithValue("@v_nivel", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_nivel", nivel); }

                    da.SelectCommand.Parameters["@v_nivel"].Direction = ParameterDirection.Input;

                    if (v_min == 0)
                    { da.SelectCommand.Parameters.AddWithValue("@v_min", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_min", v_min); }

                    da.SelectCommand.Parameters["@v_min"].Direction = ParameterDirection.Input;

                    if (v_max == 0)
                    { da.SelectCommand.Parameters.AddWithValue("@v_max", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_max", v_max); }

                    da.SelectCommand.Parameters["@v_max"].Direction = ParameterDirection.Input;

                    if (id_bodega == 0)
                    { da.SelectCommand.Parameters.AddWithValue("@v_id_bodega", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_id_bodega", id_bodega); }

                    da.SelectCommand.Parameters["@v_id_bodega"].Direction = ParameterDirection.Input;


                    if (stock == 0)
                    { da.SelectCommand.Parameters.AddWithValue("@v_stock", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_stock", stock); }

                    da.SelectCommand.Parameters["@v_stock"].Direction = ParameterDirection.Input;


                    if (stock_critico == 0)
                    { da.SelectCommand.Parameters.AddWithValue("@v_stock_critico", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_stock_critico", stock_critico); }

                    da.SelectCommand.Parameters["@v_stock_critico"].Direction = ParameterDirection.Input;

                    if (activo == 0)
                    { da.SelectCommand.Parameters.AddWithValue("@v_activo", DBNull.Value); }
                    else
                    { da.SelectCommand.Parameters.AddWithValue("@v_activo", activo); }

                    da.SelectCommand.Parameters["@v_activo"].Direction = ParameterDirection.Input;

                   // if (mov == "L")
                 //   {
                    DataTable dt = new DataTable();


                    da.Fill(dt);
                    Lst_Info.DataSource = dt;

                    Lst_Info.DataBind();
                  //  }
                  /*  else
                    {
                        SqlDataReader dr = da.SelectCommand.ExecuteReader();

                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                lbl_status.Text = dr.GetString(0);
                            }
                        }
                    }*/


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

        protected void Lst_Info_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                TextBox txt_id_nivel_grid = e.Row.FindControl("txt_id_nivel_grid") as TextBox;
                TextBox txt_nivel_grid = e.Row.FindControl("txt_nivel_grid") as TextBox;
                TextBox txt_cant_min_grid = e.Row.FindControl("txt_cant_min_grid") as TextBox;
                TextBox txt_cant_max_grid = e.Row.FindControl("txt_cant_max_grid") as TextBox;
                CheckBox chk_activo_grid = e.Row.FindControl("chk_activo_grid") as CheckBox;
                TextBox txt_stock = e.Row.FindControl("txt_stock") as TextBox;
                TextBox txt_stock_critico = e.Row.FindControl("txt_stock_critico") as TextBox;

                DropDownList cmb_bodega_grid = e.Row.FindControl("cmb_bodega_grid") as DropDownList;

                carga_contrl_lista("select ID_Bodega, Nombre_Bodega from tbl_Bodegas where Activo = 1", cmb_bodega_grid, "tbl_Familias_Productos", "ID_Bodega", "Nombre_Bodega");

                cmb_bodega_grid.SelectedIndex = -1;
                cmb_bodega_grid.Items.FindByText(Convert.ToString(drv["Nombre_Bodega"])).Selected = true;

                chk_activo_grid.Checked = false;

                txt_id_nivel_grid.Text = Convert.ToString(drv["id_nivel"]);
                txt_nivel_grid.Text = Convert.ToString(drv["nivel"]);
                txt_cant_min_grid.Text = Convert.ToString(drv["valor_min"]);
                txt_cant_max_grid.Text = Convert.ToString(drv["valor_max"]);
                txt_stock.Text = Convert.ToString(drv["por_stock"]);
                txt_stock_critico.Text = Convert.ToString(drv["por_stock_critico"]);

                if (Convert.ToBoolean(drv["activo"].ToString()))
                {
                    chk_activo_grid.Checked = true;
                }

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            }
        }


        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void Btn_grabar_Click(object sender, EventArgs e)
        {
            Boolean swc = true;
            Page.Validate();
            if (Page.IsValid)
            {
                // VALIDACIONES

                if (txt_mivel.Text == "")  {lbl_error.Text = "Debe Indicar Nombre del Nivel"; swc = false;}
                if (txt_ValMin.Text == "") {lbl_error.Text = "Debe indicar nivel Mínimo"; swc = false;}
                if (txt_ValMax.Text == "") { lbl_error.Text = "Debe indicar nivel Máximo"; swc = false; }
                if (Lst_Bodegas.SelectedValue == "0") { lbl_error.Text = "Debe indicar Bodega"; swc = false; }

                if (txt_ValMin.Text != "" && txt_ValMax.Text != "")
                { if (Convert.ToInt32(txt_ValMin.Text) > Convert.ToInt32(txt_ValMax.Text)) { lbl_error.Text = "Valor Mínimo no puede ser mayor al Valor Máximo"; swc = false; } }

                if (txt_stock.Text == "") { lbl_error.Text = "Debe inidcar porcentaje de Stock a mover entre Bodegas"; swc = false; }
                if (txt_ValMax.Text == "") { lbl_error.Text = "Debe indicar porcentaje de Stock crítico "; swc = false; }

                if (swc)
                {
                    // Ejecutamos crud de inserción
                    ejecuta_crud("I", 0, txt_mivel.Text.ToUpper().Trim(), Convert.ToInt32(txt_ValMin.Text), Convert.ToInt32(txt_ValMax.Text), Convert.ToInt32(Lst_Bodegas.SelectedValue), 1, Convert.ToInt32(txt_stock.Text), Convert.ToInt32(txt_stock_critico.Text));
                    limpieza();
                }
            }
        }

        protected void limpieza()
        {
            txt_mivel.Text = "";
            txt_stock.Text = "";
            txt_stock_critico.Text = "";
            txt_ValMax.Text = "";
            txt_ValMin.Text = "";
            lbl_error.Text = "";
        }

        protected void Lst_Info_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "grabar")
            {
                int valor = 0;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Lst_Info.Rows[index];
                CheckBox chk_activo_grid = row.FindControl("chk_activo_grid") as CheckBox;
                DropDownList cmb_bodega_grid = row.FindControl("cmb_bodega_grid") as DropDownList;

                TextBox txt_id_nivel_grid = row.FindControl("txt_id_nivel_grid") as TextBox;
                TextBox txt_nivel_grid = row.FindControl("txt_nivel_grid") as TextBox;
                TextBox txt_cant_min_grid = row.FindControl("txt_cant_min_grid") as TextBox;
                TextBox txt_cant_max_grid = row.FindControl("txt_cant_max_grid") as TextBox;

                TextBox txt_stock = row.FindControl("txt_stock") as TextBox;
                TextBox txt_stock_critico = row.FindControl("txt_stock_critico") as TextBox;

                if (chk_activo_grid.Checked)
                {
                    valor = 1;
                }
                ejecuta_crud("U",
                             Convert.ToInt32(txt_id_nivel_grid.Text),
                             txt_nivel_grid.Text,
                             Convert.ToInt32(txt_cant_min_grid.Text),
                             Convert.ToInt32(txt_cant_max_grid.Text),
                             Convert.ToInt32(cmb_bodega_grid.SelectedValue),
                             valor,
                             Convert.ToInt32(txt_stock.Text),
                             Convert.ToInt32(txt_stock_critico.Text));

            }

            if (e.CommandName == "eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Lst_Info.Rows[index];
                TextBox txt_id_nivel_grid = row.FindControl("txt_id_nivel_grid") as TextBox;

                lbl_status.Text = txt_id_nivel_grid.Text;

                ejecuta_crud("E" , Convert.ToInt32(lbl_status.Text),"", 0, 0, 0, 0, 0,0);

            }
        }
    }
  }