//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Promo_Code
    {
        public int PC_id { get; set; }
        public string PC_Type { get; set; }
        public string PC_Code { get; set; }
        public Nullable<decimal> PC_Amount { get; set; }
        public Nullable<decimal> PC_Percentage { get; set; }
        public string PC_Uses { get; set; }
        public string PC_Start { get; set; }
        public string PC_End { get; set; }
        public string PC_Apply { get; set; }
        public string PC_URL { get; set; }
        public long PC_Eventid { get; set; }
        public Nullable<System.DateTime> SavedDate { get; set; }
        public string PC_Startdatetype { get; set; }
        public string Pc_Enddatetype { get; set; }
        public Nullable<System.DateTime> P_Startdate { get; set; }
        public Nullable<System.DateTime> P_Enddate { get; set; }
    }
}
