//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wcf_Ticket_Servis.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblDosya
    {
        public int ID { get; set; }
        public Nullable<bool> IsSilindi { get; set; }
        public Nullable<int> DurumNo { get; set; }
        public Nullable<int> TipNo { get; set; }
        public Nullable<System.DateTime> OlusTarih { get; set; }
        public Nullable<int> DosyaID { get; set; }
        public string Ad { get; set; }
        public string ResimBoyut { get; set; }
        public string ResimUzanti { get; set; }
        public string ResimTur { get; set; }
        public string Path { get; set; }
        public Nullable<int> Sira { get; set; }
    }
}