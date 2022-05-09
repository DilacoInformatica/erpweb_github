using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web.UI.HtmlControls;
namespace erpweb
{
    public partial class Configuracion : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        ClsFTP ftp = new ClsFTP();
        Cls_Utilitarios utiles = new Cls_Utilitarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            try
            {
                Sserver = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("SSERVER"));
                SMysql = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("MYSQL"));

                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_009_12", Sserver) == "NO")
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

              


            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }

            if (!IsPostBack)
            {
                muestra_info();
            }

        }

        void muestra_info()
        {
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    // SqlCommand cmd = new SqlCommand("web_informa_parametros_web", connection);
                    // cmd.CommandType = CommandType.StoredProcedure;
                    // Parámetros

                    SqlDataAdapter da = new SqlDataAdapter("web_informa_parametros_web", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                   
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Lst_Info.DataSource = dt;

                    Lst_Info.DataBind();


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

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void Lst_Info_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                TextBox txt_paramatro = e.Row.FindControl("txt_paramatro") as TextBox;
                TextBox txt_valor = e.Row.FindControl("txt_valor") as TextBox;
                CheckBox Chk_activo = e.Row.FindControl("Chk_activo") as CheckBox;

                Chk_activo.Checked = false;

                txt_paramatro.Text = Convert.ToString(drv["regla"]);
                txt_valor.Text = Convert.ToString(drv["valor"]);

                if (Convert.ToBoolean(drv["activo"].ToString()))
                {
                    Chk_activo.Checked = true;
                }

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void Lst_Info_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "grabar")
            {
                int valor = 0;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Lst_Info.Rows[index];
                TextBox txt_paramatro = row.FindControl("txt_paramatro") as TextBox;
                TextBox txt_valor = row.FindControl("txt_valor") as TextBox;
                CheckBox Chk_activo = row.FindControl("Chk_activo") as CheckBox;

                if (Chk_activo.Checked)
                {
                    valor = 1;
                }

                procesa_info("G", Convert.ToInt32(row.Cells[0].Text), Context.Server.HtmlDecode(row.Cells[1].Text), txt_paramatro.Text, txt_valor.Text, valor);
                muestra_info();
            }

            if (e.CommandName == "eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Lst_Info.Rows[index];

                procesa_info("E", Convert.ToInt32(row.Cells[0].Text), "", "", "", 0);
                muestra_info();

            }
        }


        void procesa_info(string accion, int id, string sigla, string regla, string valor, int activo)
        {
            string ejecuata = "web_procesa_parametros_web";
            string query = "";

            using (SqlConnection conn = new SqlConnection(Sserver))
            {
                try
                {

                    conn.Open();


                    SqlCommand command = new SqlCommand(ejecuata, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_id", id);
                    command.Parameters["@v_id"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_mod", accion);
                    command.Parameters["@v_mod"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_sigla", sigla);
                    command.Parameters["@v_sigla"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_regla", regla);
                    command.Parameters["@v_regla"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_valor", valor);
                    command.Parameters["@v_valor"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@v_activo", activo);
                    command.Parameters["@v_activo"].Direction = ParameterDirection.Input;

                    SqlDataReader dr = command.ExecuteReader();

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

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message + query;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void Btn_grabars_Click(object sender, EventArgs e)
        {
            procesa_info("I", 0, txt_sigla.Text, txt_descrip.Text, txt_valor.Text, 1);
            muestra_info();
        }
    }
}