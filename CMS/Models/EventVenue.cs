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
    
    public partial class EventVenue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventVenue()
        {
            this.Publish_Event_Detail = new HashSet<Publish_Event_Detail>();
        }
    
        public long EventVenueID { get; set; }
        public long EventID { get; set; }
        public long AddressId { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndTime { get; set; }
        public Nullable<System.DateTime> E_Startdate { get; set; }
        public Nullable<System.DateTime> E_Enddate { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual EventVenue EventVenue1 { get; set; }
        public virtual EventVenue EventVenue2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publish_Event_Detail> Publish_Event_Detail { get; set; }
    }
}
