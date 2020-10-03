using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wcf_Ticket_Servis.Model
{
    public class ResponseUserData
    {
        public int UserID { get; set; }
        public bool Authenticated { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public int FirmaID { get; set; }
        public bool IsAdmin { get; set; }
        public int TipNo { get; set; }

        //TokenUser
        //ID
        //UserName
        //FirmaID
        //IsAdmin
    }
}