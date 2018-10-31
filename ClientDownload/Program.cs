using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (CookiesAwareWebClient client = new CookiesAwareWebClient())
            //{

                string URI = "https://sescampre.jccm.es/sitrap/controlerAcceso";
                string myParameters = @"opera=entrar&cod_usuario=uu01&clave=O@";
                string htmlResult = "";
                using (CookiesAwareWebClient wc = new CookiesAwareWebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    ///string HtmlResult = wc.UploadString(URI, myParameters);
                    htmlResult = wc.UploadString(URI, myParameters);
                    //Console.WriteLine(htmlResult);
               
                    string NextURI = "https://sescampre.jccm.es/sitrap/Controlador";
                    string NextmyParameters = @"opera=generaDescarga&Descargados=TODOS";
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Encoding = Encoding.UTF8;
                    htmlResult = wc.UploadString(NextURI, NextmyParameters);
                }
            if (htmlResult.StartsWith("\r\n<html>"))
                Console.WriteLine("No HAY");
            else
                Console.WriteLine(htmlResult);
            var aca = File.CreateText("test.txt");
            aca.Write(htmlResult);
            aca.Close();


            Console.ReadLine();

            //https://sescampre.jccm.es/sitrap/Controlador?opera=generaDescarga&altaHospitalaria=false&Descargados=TODOS


            //System.Collections.Specialized.NameValueCollection values = new System.Collections.Specialized.NameValueCollection();
            //values.Add("opera", "boton");
            //values.Add("cod_usuario", "uu01");
            //values.Add("clave", "Osoft2010@");

            ////client.

            //// We authenticate first
            //client.UploadValues("https://sescampre.jccm.es/sitrap/controlerAcceso", values);

            //// Now we can download

            //string downloadUri = "https://sescampre.jccm.es/sitrap/Controlador?opera=enlaceDescarga&altaHospitalaria=true&Descargados=NO";


            //string filename = System.IO.Path.GetFileName(downloadUri);

            ////string filepathname = System.IO.Path.Combine(destination, filename);

            //client.DownloadFile(downloadUri, "Text.txt");
            //}

        }
    }


    public class CookiesAwareWebClient : WebClient
    {
        public CookieContainer CookieContainer;
        public CookieContainer MyProperty
        {
            get { return CookieContainer; }
            set { CookieContainer = value; }
        }
        public CookiesAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            ((HttpWebRequest)request).CookieContainer = CookieContainer;
            return request;
        }
    }
}
