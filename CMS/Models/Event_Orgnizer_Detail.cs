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
    
    public partial class Event_Orgnizer_Detail
    {
        public long Orgnizer_Id { get; set; }
        public Nullable<long> Orgnizer_Event_Id { get; set; }
        public string UserId { get; set; }
        public string DefaultOrg { get; set; }
        public long OrganizerMaster_Id { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual Organizer_Master Organizer_Master { get; set; }
    }
}
