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
        public decimal ECFeePerTicket { get; set; }
        public decimal MerchantFeePerTicket { get; set; }
        public decimal Customer_Fee { get; set; }
        public Nullable<int> PromoCodeID { get; set; }
        public string PromoCode { get; set; }
        public Nullable<decimal> PromoCodeAmount { get; set; }
        public Nullable<System.DateTime> O_OrderDateTime { get; set; }
        public Nullable<byte> OrderStateId { get; set; }
        public string OrderStateName { get; set; }
        public long TPD_Id { get; set; }
        public long TicketTypeID { get; set; }
        public string TicketTypeName { get; set; }
        public string VariableIds { get; set; }
        public Nullable<byte> PaymentTypeId { get; set; }
        public bool IsManualOrder { get; set; }
        public long TicketId { get; set; }
        public byte TicketFeeType { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal TicketDiscount { get; set; }
        public Nullable<decimal> Donation { get; set; }
    }
}
