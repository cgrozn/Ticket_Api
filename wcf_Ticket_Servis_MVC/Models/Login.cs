using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wcf_Ticket_Servis_MVC.Models
{
    public class Login
    {

        public int UserID { get; set; }
        public string Ad { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int FirmaID { get; set; }
        public bool IsAdmin { get; set; }
        public int TipNo { get; set; }
    }
}