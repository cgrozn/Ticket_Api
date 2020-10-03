using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wcf_Ticket_Servis.Model
{
         public class UserLoginData

    {

       public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}