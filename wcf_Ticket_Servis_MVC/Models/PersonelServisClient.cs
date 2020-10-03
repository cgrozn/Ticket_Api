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
    public class PersonelServisClient
    {



        public List<Personel> PersonelListeSirket(string token)
        {
            try
            {
                var webClinet = new WebClient();
                webClinet.Encoding = Encoding.UTF8;

                webClinet.Headers["Content-type"] = "application/json";
                webClinet.Headers["Token"] = token;

                var json = webClinet.DownloadString(Glob.Base_URL + "PersonelListeSirket");
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Personel>>(json);
            }
            catch
            {
                return null;
            }

        }


        public Personel PersonelBul(string id)
        {

            try
            {
                var webClinet = new WebClient();

                string url = string.Format(Glob.Base_URL + "PersonelBul/{0}",id);

                webClinet.Encoding = Encoding.UTF8;
                var json = webClinet.DownloadString(url);
                var js = new JavaScriptSerializer();

                return js.Deserialize<Personel>(json);

               
            }
            catch
            {
                return null;
            }
        }

        public bool PersonelEkle(Personel personel,string token)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Personel));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, personel);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";
                webClient.Headers["Token"] = token;

                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL + "PersonelEkle", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        
    }

        public bool PersonelEkle2(Personel personel)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Personel));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, personel);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";

                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL + "KayitOl", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool PersonelUpdate(Personel personel,string token)
        {

            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Personel));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, personel);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Token"] = token;
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL + "PersonelUpdate", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool PersonelDelete(Personel personel,string token)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Personel));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, personel);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";
                webClient.Headers["Token"] = token;
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Glob.Base_URL + "PersonelDelete", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}