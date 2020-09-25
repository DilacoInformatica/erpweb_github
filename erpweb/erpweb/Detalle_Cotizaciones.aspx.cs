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
using System.Globalization;

namespace erpweb
{
    public partial class Detalle_Cotizaciones : System.Web.UI.Page
    {
        int num_cotizacion = 0;
        string Sserver = "";
        string SMysql = "";
        Cls_Utilitarios utiles = new Cls_Utilitarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            num_cotizacion = Convert.ToInt32(Request.QueryString["cot"].ToString());
            Sserver = utiles.verifica_ambiente("SSERVER");
            SMysql = utiles.verifica_ambiente("MYSQL");
            if (!this.IsPostBack)
            {
                muestra_info_cotizacion(num_cotizacion);
            }
        }

        void muestra_info_cotizacion(int num_cotizacion)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            string queryString = "";

            queryString = "select ct.ID_Cotizacion, ";
            queryString = queryString + " ct.Cotizac_Num, ";
            queryString = queryString + " ct.Nombre_Cotizacion, ";
            queryString = queryString + " ct.Id_Tipo_Cotizacion, ";
            queryString = queryString + " cl.Rut, ";
            queryString = queryString + " cl.Dv_Rut, ";
            queryString = queryString + " cl.Razon_Social, ";
            queryString = queryString + " cl.direccion, ";
            queryString = queryString + " cl.Comuna, ";
            queryString = queryString + " cl.Ciudad, ";
            queryString = queryString + " cl.Telefonos, ";
            queryString = queryString + " cl.Email ";
            queryString = queryString + "  from tbl_cotizaciones ct ";
            queryString = queryString + "  inner join tbl_clientes cl on cl.id_cliente = ct.id_cliente ";
            queryString = queryString + "  where ct.Cotizac_Num = " + num_cotizacion;

            using (MySqlConnection conn = new MySqlConnection(SMysql))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    command.ExecuteNonQuery();
                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(command);
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            // NV
                            lbl_id_cot.Text = dr.GetString(0);
                            lbl_existe.Text = detecta_cliente(Convert.ToInt32(dr.GetString(4)));
                            //v_id_nta_vta = Convert.ToInt32(dr.GetString(0));
                            lbl_numero.Text = dr.GetString(1);
                            lbl_fecha.Text = dr.GetString(2);
                            // Clientes
                            lbl_cliente.Text = dr.GetString(10);
                            lbl_rut.Text = dr.GetString(6) + '-' + dr.GetString(7);
                            lbl_rut_exit.Text = dr.GetString(6);
                            lbl_fono.Text = dr.GetString(8);
                            lbl_email.Text = dr.GetString(9);
                            lbl_direccion.Text = dr.GetString(16);
                            lbl_comuna.Text = dr.GetString(17);
                            lbl_ciudad.Text = dr.GetString(18);
                            lbl_region.Text = dr.GetString(16);
                            // Despacho
                           
                            double v_neto = Convert.ToDouble(dr.GetString(12));
                            double v_tax = Convert.ToDouble(dr.GetString(13));
                            double v_total = Convert.ToDouble(dr.GetString(11));
                            //lbl_neto.Text = v_neto.ToString("C3", CultureInfo.CurrentCulture);


                            /*lbl_moneda.Text = dr.GetString(30);
                            lbl_neto.Text = lbl_moneda.Text + ' ' + v_neto.ToString("N2");
                            lbl_tax.Text = lbl_moneda.Text + ' ' + v_tax.ToString("N");
                            lbl_total.Text = lbl_moneda.Text + ' ' + v_total.ToString("N");
                            lbl_n_oc.Text = dr.GetString(31);*/
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                   // detalle_nv(v_id_nta_vta);
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public string detecta_cliente(int v_rut)
        {
            string sql = "select count(1) from tbl_clientes where rut = " + v_rut;
            string result = "N";
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                if (rdr.GetInt32(0).ToString() == "0")
                                {
                                    result = "NO";
                                }
                                else
                                {
                                    result = "SI";
                                }
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        protected void Btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cotizaciones.aspx");
        }
    }
}