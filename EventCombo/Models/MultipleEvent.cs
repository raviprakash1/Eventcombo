//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventCombo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MultipleEvent
    {
        public long MultipleEventID { get; set; }
        public long EventID { get; set; }
        public int Frequency_Id { get; set; }
        public Nullable<System.DateTime> StartingFrom { get; set; }
        public Nullable<System.DateTime> StartingTo { get; set; }
        public string WeeklyDay { get; set; }
        public Nullable<int> MonthlyDay { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual LookUpEntry LookUpEntry { get; set; }
    }
}
