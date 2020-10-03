using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace wcf_Ticket_Servis_MVC.Models
{
    public class BiletServicClient
    {
        public List<Bilet> BiletListeSirket(string token)
        {
            try
            {


                var webClinet = new WebClient();
                webClinet.Headers["Content-type"] = "application/json";
                webClinet.Headers["Token"] = token;


                var json = webClinet.DownloadString(Glob.Base_URL + "BiletSirket");
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Bilet>>(json);

               // return new List<Bilet>();

            }
            catch
            {
                //return new List<Bilet>();
                return null;
            }
        }

        public List<Bilet> BiletListeMusteri(string token)
        {

            try
            {
                var webClinet = new WebClient();
                webClinet.Headers["Content-type"] = "application/json";
                //  webClinet.Headers["Token"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6IsOnYcSfcsSxIiwiSUQiOjEsIkZpcm1hSUQiOjEsIklzQWRtaW4iOnRydWUsIlRpcE5vIjo2LCJpYXQiOiIxNTg2OTUwNzk5In0.Cq_BPLeq5aqgt1phMUyr-GJc0eVGpPzHv2k6FAxWNlE";
                webClinet.Headers["Token"] = token;
                 webClinet.Encoding = Encoding.UTF8;
                var json = webClinet.DownloadString(Glob.Base_URL + "BiletMusteri");
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Bilet>>(json);
            }
            catch
            {
                return null;
            }
        }

        public bool BiletEkle(Bilet bilet, string token)
        {
            
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Bilet));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, bilet);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

                WebClient webClient = new WebClient();
                webClient.Headers["Token"] = token;
                webClient.Headers["Content-type"] = "application/json";

                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL+"BiletEkle", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }

            public bool BiletUpdate(Bilet bilet,string token)
            {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Bilet));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, bilet);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";
                webClient.Headers["Token"] = token;
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL + "BiletUpdate", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }

        }


        public bool BiletDelete(Bilet bilet,string token)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Bilet));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, bilet);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";
                webClient.Headers["Token"]= token;

                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL+ "BiletDelete", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }

        }




        public Bilet BiletBul(string id)
        {
            try
            {
                var webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;


                string url = string.Format(Glob.Base_URL + "BiletBul/{0}", id);
               
                var json = webClient.DownloadString(url);

                var ekle = url;
                var js = new JavaScriptSerializer();

                return js.Deserialize<Bilet>(json);
            }
            catch
            {
             
                return null;
            }

        }











    }
}