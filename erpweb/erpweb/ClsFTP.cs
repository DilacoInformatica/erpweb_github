using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace erpweb
{
    public class ClsFTP
    {
        public string Ftp(string Servidor, string Usuario, string Contrasena, string archivo, string ruta_local, string ruta_server)
        {
            string ruta_final = Servidor + ruta_server + archivo;

            try
            { // Get the object used to communicate with the server.
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(Usuario, Contrasena);
                    client.UploadFile(@ruta_final, "STOR", @ruta_local + archivo);
                    client.Dispose();
                }
                return ("OK");
            }
            catch (Exception e)
            {
                return (e.Message.ToString());
            }
        }
    }
}

//try
//{
//    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@ruta_final);
//    request.Method = WebRequestMethods.Ftp.UploadFile;

//    // This example assumes the FTP site uses anonymous logon.
//    request.Credentials = new NetworkCredential(Usuario, Contrasena);

//    // Copy the contents of the file to the request stream.
//    byte[] fileContents;
//    using (StreamReader sourceStream = new StreamReader(@ruta_local + archivo))
//    {
//        fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
//    }

//    request.ContentLength = fileContents.Length;

//    using (Stream requestStream = request.GetRequestStream())
//    {
//        requestStream.Write(fileContents,0, fileContents.Length);
//    }

//    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
//    {
//        return ("OK");
//    }
//}
//catch (Exception e)
//{
//    return (e.Message.ToString());
//}