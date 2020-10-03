using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wcf_Ticket_Servis_MVC.Models;

namespace wcf_Ticket_Servis_MVC.ViewModel
{
    public class BiletViewModel
    {
        public Bilet Bilet { get; set; }
        public IEnumerable<Personel> Personels { get; set; }

    }
}