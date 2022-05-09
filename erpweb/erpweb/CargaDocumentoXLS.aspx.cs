using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.IO;
namespace erpweb
{
    public partial class CargaDocumentoXLS : System.Web.UI.Page
    {
        string Sserver = "";
        int id_usuario = 0;
        Cls_Utilitarios utiles = new Cls_Utilitarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            Sserver = Cls_Seguridad.DesEncriptar(utiles.verifica_ambiente("SSERVER"));

            if (!String.IsNullOrEmpty(Request.QueryString["usuario"]))
            {
                id_usuario = Convert.ToInt32(Request.QueryString["usuario"]);
            }
            else
            {
                id_usuario = 141;
            }
            Btn_Procesar.Enabled = false;
        }

        protected void Btn_Subir_Click(object sender, EventArgs e)
        {
            try
            {
                Random rnd1 = new Random();
                int N = rnd1.Next(9000) + 100; // Aquí trae un numero entre 0 y 8999 y le sumamos 100 para darle un valor único al archivo
                string iniciales = retona_inicial_usuario(id_usuario);
                string nuevo_nombre_archivo = "";
                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                
                if (fileExt == ".csv")
                {
                    nuevo_nombre_archivo = iniciales + "_" + Convert.ToString(DateTime.Today.Year) + Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Day) + "_" + Convert.ToString(N) + fileExt;
                    lbl_nombre_file.Text = nuevo_nombre_archivo;
                    FileUpload1.SaveAs(Server.MapPath("~/xls/" + System.IO.Path.GetFileName(nuevo_nombre_archivo)));
                    lbl_status.Text = "Archivo cargado correctamente";
                    lbl_id_file.Text = Convert.ToString(insert_historial_archivo(nuevo_nombre_archivo, FileUpload1.FileName,Convert.ToString(id_usuario)));
                    Btn_Procesar.Enabled = true;
                    lbl_file.Text = Server.MapPath("~/xls/" + System.IO.Path.GetFileName(nuevo_nombre_archivo));
                    import_to_grid();
                }
                else
                {
                    lbl_status.Text = "Extensión no permitida, Sólo se permite archivos con extensión CSV";
                }
            }
            catch (Exception ex)
            {
                lbl_status.Text = "Error: " + ex.ToString();
            }
        }

        string[] Split(string input, char separator)
        {
            return input.Split(separator);
        }

        private void import_to_grid()
        {
            try
            {
                System.Data.DataTable dtDataSource = new System.Data.DataTable();

                string[] fileContent = File.ReadAllLines(lbl_file.Text);

                if (fileContent.Count() > 0)
                {
                    string[] columns = fileContent[0].Split(';');
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        dtDataSource.Columns.Add(columns[i]);
                    }

                    dtDataSource.Columns.Add("Resultado");
                    for (int i = 1; i < fileContent.Count(); i++)
                    {
                        lbl_registros.Text = "Cantidad de Registros: " + i.ToString();
                        string[] rowData = fileContent[i].Split(';');
                        dtDataSource.Rows.Add(rowData);
                    }
                }

                GridView1.DataSource = dtDataSource;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lbl_status.Text = ex.ToString();
            }

           


            //try
            //{
            //    int cont = 0;
            //    System.Data.DataTable Data = new System.Data.DataTable();
            //    DataRow dr;
            //    DataSet ds = new DataSet();
            //    string s;
            //    Data.Columns.Add("Tipo_Compra", Type.GetType("System.String"));
            //    Data.Columns.Add("Rut_Proveedor", Type.GetType("System.String"));
            //    Data.Columns.Add("Proveedor", Type.GetType("System.String"));
            //    Data.Columns.Add("N_factura", Type.GetType("System.String"));
            //    Data.Columns.Add("Fecha_Dcto", Type.GetType("System.String"));
            //    Data.Columns.Add("Monto_Total", Type.GetType("System.String"));
            //    Data.Columns.Add("Resultado", Type.GetType("System.String"));

            //    using (StreamReader sr = File.OpenText(lbl_file.Text))
            //    {
                   
            //        while ((sr.ReadLine()) != null)
            //        {

            //            if (sr.ReadLine() != null)
            //            {
            //                Console.Write(sr.ReadLine());
            //                string[] items = Split(sr.ReadLine(), ';');

            //                if (cont > 0)
            //                {
            //                    dr = Data.NewRow();

            //                    dr[0] = items[0].ToString();
            //                    dr[1] = items[1].ToString();
            //                    dr[2] = items[2].ToString();
            //                    dr[3] = items[3].ToString();
            //                    dr[4] = items[4].ToString();
            //                    if (items[5].ToString() != null)
            //                    {
            //                        dr[5] = items[5].ToString();
            //                    }
            //                    else
            //                    {
            //                        dr[5] = "0";
            //                    }

            //                    if (cont == 92)
            //                    {
            //                        lbl_status.Text = "";
            //                    }
            //                    Data.Rows.Add(dr);
            //                }
                          
            //                cont++;
            //            }
                       
            //        }
            //        lbl_registros.Text = cont.ToString();
            //        ds.Tables.Add(Data);
            //        GridView1.DataSource = ds;
            //        GridView1.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex.Message);
            //    //lbl_status.Text = ex.Message;
            //}

           
            //try
            //{
            //    Application excel = new Microsoft.Office.Interop.Excel.Application { Visible = false };

            //    System.Data.DataTable Data = new System.Data.DataTable();
            //    DataRow dr;
            //    DataSet ds = new DataSet();

            //    Workbook w = excel.Workbooks.Open(lbl_file.Text);
            //    Worksheet Hoja = w.Sheets["Hoja1"];
            //    //int numrow = 6;
            //    int numcol = Hoja.Columns.Count;

            //    Data.Columns.Add("Tipo_Compra", Type.GetType("System.String"));
            //    Data.Columns.Add("Rut_Proveedor", Type.GetType("System.String"));
            //    Data.Columns.Add("Proveedor", Type.GetType("System.String"));
            //    Data.Columns.Add("N_factura", Type.GetType("System.String"));
            //    Data.Columns.Add("Fecha_Dcto", Type.GetType("System.String"));
            //    Data.Columns.Add("Monto_Total", Type.GetType("System.String"));
            //    Data.Columns.Add("Resultado", Type.GetType("System.String"));

            //    double x = 2;

            //    // lbl_status.Text = Hoja.Cells(x, 1).Value.ToString();

            //    while (Hoja.Cells[x, 1].Value != null)
            //    {
            //        dr = Data.NewRow();
            //        //(string)(Hoja.Cells[x, 1] as Excel.Range).Value

            //        dr[0] = ((Range)Hoja.Cells[x, 1]).Value;
            //        dr[1] = ((Range)Hoja.Cells[x, 2]).Value;
            //        dr[2] = ((Range)Hoja.Cells[x, 3]).Value;
            //        dr[3] = ((Range)Hoja.Cells[x, 4]).Value;
            //        dr[4] = ((Range)Hoja.Cells[x, 5]).Value;
            //        if (((Range)Hoja.Cells[x, 6]).Value != null)
            //        {
            //            dr[5] = ((Range)Hoja.Cells[x, 6]).Value;
            //        }
            //        else
            //        {
            //            dr[5] = "0";
            //        }
            //        Data.Rows.Add(dr);
            //        x++;
            //    }

            //    ds.Tables.Add(Data);
            //    w.Close();
            //    excel.Quit();

            //    GridView1.DataSource = ds;
            //    GridView1.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    lbl_status.Text = "Error en funcion import_to_grid " + ex.ToString();
            //}



        }

        public int insert_historial_archivo(string archivo, string original, string id_usuario)
        {
            int id_file = 0;
            
            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_crud_carga_files", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@mov", "I");
                    cmd.Parameters["@mov"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@id_file", DBNull.Value);
                    cmd.Parameters["@id_file"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@nombre_archivo_org", original);
                    cmd.Parameters["@nombre_archivo_org"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@nom_file_guardado", archivo);
                    cmd.Parameters["@nom_file_guardado"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@id_usuario", id_usuario);
                    cmd.Parameters["@id_usuario"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                id_file = rdr.GetInt32(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return id_file;

                }
                catch (Exception ex)
                {
                    lbl_status.Text = "Error insert_historial_archivo: " + ex.Message;
                    return 0;
                }
                finally
                {
                    connection.Close();

                }
            }
        }

        public string retona_inicial_usuario(int id_usuario)
        {
            string sql = "";
            string resultado = "";
            sql = "select Iniciales from tbl_Usuarios where ID_usuario = " + id_usuario;

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
                                resultado = rdr.GetString(0);
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                    return resultado;
                }
                catch (Exception ex)
                {

                    connection.Close();
                    connection.Dispose();
                    return ex.Message.ToString();
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
              //  e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void Btn_Procesar_Click(object sender, EventArgs e)
        {
            string resultado = "";
            foreach(GridViewRow Fila in GridView1.Rows)
            {
                resultado = carga_datagrilla(Fila.Cells[0].Text, Fila.Cells[1].Text, Fila.Cells[2].Text, Fila.Cells[3].Text, Fila.Cells[4].Text, Fila.Cells[5].Text, id_usuario, Convert.ToInt32(lbl_id_file.Text));
                if (resultado == "OK")
                {
                    Fila.Cells[6].Text = resultado;
                    Fila.Cells[6].ForeColor = System.Drawing.Color.Black;

                }
                if (resultado == "EX")
                {
                    Fila.Cells[6].Text = "Documento fue cargado con anterioridad";
                    Fila.Cells[6].ForeColor = System.Drawing.Color.Red;
                }


                lbl_status.Text = "Archivo procesado, verifique estado columna Resultado para ver estado línea por línea";
            }
        }

        public string carga_datagrilla(string tipo_compra ,string rut_prov, string proveedor,string nfactura, string fechadocto, string monto, int id_usuario, int id_file)
        {
            string resultado = "";

            using (SqlConnection connection = new SqlConnection(Sserver))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_crud_carga_fileProv", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    // Parámetros
                    cmd.Parameters.AddWithValue("@mov", "I");
                    cmd.Parameters["@mov"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@id_file", id_file);
                    cmd.Parameters["@id_file"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@tipo_compra", tipo_compra);
                    cmd.Parameters["@tipo_compra"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@rut_proveedor", rut_prov);
                    cmd.Parameters["@rut_proveedor"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@proveedor", proveedor);
                    cmd.Parameters["@proveedor"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@nro_factura", nfactura);
                    cmd.Parameters["@nro_factura"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@fecha_docto", fechadocto.Replace(" 0:00:00", ""));
                    cmd.Parameters["@fecha_docto"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@monto_total", monto.Replace(".",""));
                    cmd.Parameters["@monto_total"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@id_usr_responsable", DBNull.Value);
                    cmd.Parameters["@id_usr_responsable"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@fecha", DBNull.Value);
                    cmd.Parameters["@fecha"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@codigo", DBNull.Value);
                    cmd.Parameters["@codigo"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@conta_nro_folio", DBNull.Value);
                    cmd.Parameters["@conta_nro_folio"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@fecha_pago", DBNull.Value);
                    cmd.Parameters["@fecha_pago"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@comentario", DBNull.Value);
                    cmd.Parameters["@comentario"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@id_usuario_carga", id_usuario);
                    cmd.Parameters["@id_usuario_carga"].Direction = ParameterDirection.Input;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                //rescatamos los valores segun lo que utilizaremos
                                resultado = rdr.GetString(0);
                            }
                        }
                    }

                    connection.Close();
                    connection.Dispose();

                    return resultado;

                }
                catch (Exception ex)
                {
                    lbl_status.Text = "Error insert_historial_archivo: " + ex.Message;
                    return "ERROR carga_datagrilla: " + ex.Message.ToString();
                }
                finally
                {
                    connection.Close();

                }
            }
        }
    }
}