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
    
    public partial class Ticket_Quantity_Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket_Quantity_Detail()
        {
            this.Ticket_Purchased_Detail = new HashSet<Ticket_Purchased_Detail>();
        }
    
        public long TQD_Id { get; set; }
        public Nullable<long> TQD_PE_Id { get; set; }
        public Nullable<long> TQD_Event_Id { get; set; }
        public Nullable<long> TQD_Ticket_Id { get; set; }
        public Nullable<long> TQD_AddressId { get; set; }
        public Nullable<long> TQD_Quantity { get; set; }
        public Nullable<long> TQD_Remaining_Quantity { get; set; }
        public string TQD_StartDate { get; set; }
        public string TQD_StartTime { get; set; }
    
        public virtual Ticket Ticket { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket_Purchased_Detail> Ticket_Purchased_Detail { get; set; }
        public virtual Address Address { get; set; }
        public virtual Publish_Event_Detail Publish_Event_Detail { get; set; }
    }
}
