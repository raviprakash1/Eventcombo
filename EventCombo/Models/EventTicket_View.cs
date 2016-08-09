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
    
    public partial class EventTicket_View
    {
        public Nullable<long> EventID { get; set; }
        public string TicketName { get; set; }
        public long OId { get; set; }
        public string OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> OrderAmount { get; set; }
        public Nullable<decimal> VariableAmount { get; set; }
        public Nullable<long> TotalQuantity { get; set; }
        public Nullable<long> PurchasedQuantity { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> ECFeePerTicket { get; set; }
        public Nullable<decimal> MerchantFeePerTicket { get; set; }
        public Nullable<decimal> Customer_Fee { get; set; }
        public Nullable<int> PromoCodeID { get; set; }
        public string PromoCode { get; set; }
        public Nullable<decimal> PromoCodeAmount { get; set; }
        public Nullable<System.DateTime> O_OrderDateTime { get; set; }
        public Nullable<byte> OrderStateId { get; set; }
        public string OrderStateName { get; set; }
    }
}
