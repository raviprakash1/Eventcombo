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
    
    public partial class LockTicket
    {
        public long LockTicketId { get; set; }
        public System.Guid LockOrderId { get; set; }
        public long TicketQuantityDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Donate { get; set; }
        public decimal ECFeeAmount { get; set; }
        public decimal ECFeePercent { get; set; }
        public int ECFeeType { get; set; }
        public decimal CustomerFee { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    
        public virtual LockOrder LockOrder { get; set; }
    }
}
