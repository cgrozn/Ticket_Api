using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;


namespace wcf_Ticket_Servis_MVC.Models
{
    public class FirmaServisClient
    {

            
        public List<Firma> FirmaListeSirket(string token)
        {
            try
            {
                var webClinet = new WebClient();
                webClinet.Headers["Content-type"] = "application/json";
                webClinet.Headers["Token"] = token;
                var json = webClinet.DownloadString(Glob.Base_URL + "FirmaSirket");

                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Firma>>(json);
            }
            catch
            {
                return null;
            }

        }
        public List<Firma> FirmaListeMusteri(string token)
        {
            try
            {
                var webClinet = new WebClient();
                webClinet.Headers["Content-type"] = "application/json";
                webClinet.Headers["Token"] = token;
                var json = webClinet.DownloadString(Glob.Base_URL+ "FirmaMusteri");
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Firma>>(json);
            }
            catch
            {
                return null;
            }

        }

        public bool FirmaEkle(Firma firma, string token) 
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Firma));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, firma);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";
                webClient.Headers["Token"] = token; ;
                webClient.Encoding = Encoding.UTF8;

                webClient.UploadString(Glob.Base_URL+"FirmaEkle", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }

        }
        

        public bool FirmaDelete(Firma firma, string token)
        {
            try
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Firma));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, firma);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Headers["Token"] = token; ;

                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL + "FirmaDelete", "DELETE", data);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public Firma FirmaBul(string id)
        {
            try
            {
                var webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;


                string url = string.Format(Glob.Base_URL + "FirmaBul/{0}", id);

                var json = webClient.DownloadString(url);

                var js = new JavaScriptSerializer();

                return js.Deserialize<Firma>(json);





             
            }
            catch
            {
                return null;
            }

        }

        public bool FirmaUpdate(Firma firma, string token)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Firma));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, firma);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";
                webClient.Headers["Token"] = token; ;

                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL + "FirmaUpdate", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}