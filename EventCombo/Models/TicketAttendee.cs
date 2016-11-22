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
    
    public partial class TicketAttendee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TicketAttendee()
        {
            this.TicketOrderDetails = new HashSet<TicketOrderDetail>();
        }
    
        public long TicketAttendeeId { get; set; }
        public long TicketBearerId { get; set; }
        public long PurchasedTicketId { get; set; }
        public long Quantity { get; set; }
    
        public virtual Ticket_Purchased_Detail Ticket_Purchased_Detail { get; set; }
        public virtual TicketBearer TicketBearer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketOrderDetail> TicketOrderDetails { get; set; }
    }
}
