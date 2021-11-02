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
using Microsoft.VisualBasic;
using System.IO;

namespace erpweb
{
    public partial class Inf_Cotizaciones : System.Web.UI.Page
    {
        string Sserver = "";
        string SMysql = "";
        string usuario = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            try
            {
                if (Session["Usuario"].ToString() == "" || Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("Ppal.aspx");
                }
                else
                {
                    if (utiles.obtiene_acceso_pagina(Session["Usuario"].ToString(), "OPC_008_03", Sserver) == "NO")
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


            }
            catch
            {
                Response.Redirect("Ppal.aspx");
            }
            //Btn_cargar.Attributes["Onclick"] = "return confirm('Crear o Actualizar cliente con precios especiales?')";
            if (!this.IsPostBack)
            {
               // muestra_seleccion(1);
            }
        }

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void LnkBtn_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ppal.aspx");
        }

        protected void LnkBtn_Descargar_Click(object sender, EventArgs e)
        {
            if (Lista_cotizacion.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    Lista_cotizacion.AllowPaging = false;
                   // this.BindGrid();

                    Lista_cotizacion.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in Lista_cotizacion.HeaderRow.Cells)
                    {
                        cell.BackColor = Lista_cotizacion.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in Lista_cotizacion.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = Lista_cotizacion.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = Lista_cotizacion.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    Lista_cotizacion.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
           
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void muestra_seleccion(int v_tipo)
        {
            string queryString = "";
            lbl_mensaje.Text = "";
            queryString = "informe_cotizaciones";


            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@v_tipo", v_tipo);
                    command.Parameters["@v_tipo"].Direction = ParameterDirection.Input;

                    DataSet ds = new DataSet();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        lbl_mensaje.Text = "Informe no entregó resultados";
                        Lista_cotizacion.DataSource = null;
                        Lista_cotizacion.DataBind();
                    }
                    else
                    {
                        Lista_cotizacion.DataSource = dr;
                        Lista_cotizacion.DataBind();

                        lbl_cantidad.Text = "Cantidad de Registros: " + Convert.ToString(Lista_cotizacion.Rows.Count);
                    }

                    //Productos.DataMember = "tbl_items";

                    conn.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        protected void BtnMostrar_Click(object sender, EventArgs e)
        {
            if (RadBtnTot.Checked)
            {
                muestra_seleccion(1);
            }
            if (RadBtnDet.Checked)
            {
                muestra_seleccion(2);
            }

        }

        protected void Lista_cotizacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_num_cot_erp = e.Row.FindControl("lbl_num_cot_erp") as Label;

                lbl_num_cot_erp.Text = utiles.busca_numero_doc_erp(Convert.ToInt32(e.Row.Cells[1].Text), "CO", Sserver);

            }
        }
    }
}