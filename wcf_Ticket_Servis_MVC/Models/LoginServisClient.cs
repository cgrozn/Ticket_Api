using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace wcf_Ticket_Servis_MVC.Models
{
    public class LoginServisClient
    {
        // private string Base_URL = "http://localhost:51442/IslemService.svc/";

        public bool Login(Login user)
        {

            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Login));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, user);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();

                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;

                webClient.UploadString(Glob.Base_URL + "Login", "POST", data);

                

                return true;
            }
            catch
            {


                return false;
            }
        }

        public bool KayitOl(Personel user)
        {

            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Personel));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, user);
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








        }
}