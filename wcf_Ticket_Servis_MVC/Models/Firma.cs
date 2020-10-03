using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace wcf_Ticket_Servis_MVC.Models
{
    public class Firma
    {
        public int ID { get; set; }
        public string Ad { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Telefon2 { get; set; }
        public string Mail { get; set; }
        public string VergiNumara { get; set; }
        public string VergiDaire { get; set; }
        public string WebSite { get; set; }
    }
}