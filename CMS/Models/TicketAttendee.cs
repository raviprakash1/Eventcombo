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
    
    public partial class TicketAttendee
    {
        public long TicketAttendeeId { get; set; }
        public long TicketBearerId { get; set; }
        public long PurchasedTicketId { get; set; }
        public long Quantity { get; set; }
    
        public virtual Ticket_Purchased_Detail Ticket_Purchased_Detail { get; set; }
        public virtual TicketBearer TicketBearer { get; set; }
    }
}
