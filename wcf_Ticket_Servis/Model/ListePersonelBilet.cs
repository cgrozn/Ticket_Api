using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wcf_Ticket_Servis.Model
{
    public class ListePersonelBilet
    {

        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Konu { get; set; }
        public string Notlar { get; set; }
        public string Departman { get; set; }
        public string OlusTarihi { get; set; }
        public string BitisTarihi { get; set; }
        public int TipNo { get; set; }
        
     
    }
}