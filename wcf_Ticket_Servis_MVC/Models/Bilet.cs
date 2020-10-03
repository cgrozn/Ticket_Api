using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace wcf_Ticket_Servis_MVC.Models
{
    public class Bilet
    {

        public int ID { get; set; }
        public int PersonelID { get; set; }
        public string Baslik { get; set; }
        public string Konu { get; set; }
        public string Notlar { get; set; }
        public int TipNo { get; set; }
        public bool IsSilindi { get; set; }
       public string OlusTarih { get; set; }


    }
}