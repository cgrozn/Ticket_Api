using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wcf_Ticket_Servis_MVC.Models
{
    public class Personel
    {
        public int ID { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSilindi { get; set; }
        public int TipNo { get; set; }
        public int DurumNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Password { get; set; }
        public string Departman { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Telefon2 { get; set; }
        public string Adres { get; set; }
        public string WebSite { get; set; }
        public int FirmaID { get; set; }

    }
}