using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wcf_Ticket_Servis.Model
{
    public class Bilet
    {

        public int ID { get; set; }
        public int PersonelID { get; set; }
        public string Baslik { get; set; }
        public string Konu { get; set; }
        public string Notlar { get; set; }

       // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      //  [DataType(DataType.Date)]
       // public DateTime OlusTarih { get; set; }
        public int TipNo { get; set; }
      //  public bool IsSilindi {get;set;}
    }

}
