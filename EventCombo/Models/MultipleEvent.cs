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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MultipleEvent()
        {
            this.Publish_Event_Details = new HashSet<Publish_Event_Detail>();
        }
    
        public long MultipleEventID { get; set; }
        public long EventID { get; set; }
        public string Frequency { get; set; }
        public string StartingFrom { get; set; }
        public string StartingTo { get; set; }
        public string WeeklyDay { get; set; }
        public Nullable<int> MonthlyDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MonthlyWeek { get; set; }
        public string MonthlyWeekDays { get; set; }
    
        public virtual Event Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publish_Event_Detail> Publish_Event_Details { get; set; }
    }
}
