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
    
    public partial class Ticket_Purchased_Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket_Purchased_Detail()
        {
            this.TicketAttendees = new HashSet<TicketAttendee>();
        }
    
        public long TPD_Id { get; set; }
        public string TPD_User_Id { get; set; }
        public string TPD_Order_Id { get; set; }
        public Nullable<long> TPD_Purchased_Qty { get; set; }
        public Nullable<long> TPD_TQD_Id { get; set; }
        public Nullable<long> TPD_Event_Id { get; set; }
        public Nullable<decimal> TPD_Amount { get; set; }
        public Nullable<decimal> TPD_Donate { get; set; }
        public string TPD_GUID { get; set; }
        public Nullable<decimal> TPD_EC_Fee { get; set; }
        public Nullable<int> TPD_PromoCodeID { get; set; }
        public Nullable<decimal> TPD_PromoCodeAmount { get; set; }
        public decimal Customer_Fee { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Ticket_Quantity_Detail Ticket_Quantity_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketAttendee> TicketAttendees { get; set; }
    }
}
