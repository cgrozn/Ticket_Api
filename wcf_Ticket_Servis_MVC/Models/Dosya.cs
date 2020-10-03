using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wcf_Ticket_Servis_MVC.Models
{
    public class Dosya
    {

       public int ID { get; set; }
       public string OlusTarihi { get; set; }
       public string Ad { get; set; }
       public string Path   { get; set; }
       public string Sira { get; set; }
       public string TipID { get; set; }
    }
}