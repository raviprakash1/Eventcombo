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
    
    public partial class Order_Detail_T
    {
        public long O_Id { get; set; }
        public string O_Order_Id { get; set; }
        public string O_User_Id { get; set; }
        public Nullable<decimal> O_TotalAmount { get; set; }
        public Nullable<decimal> O_OrderAmount { get; set; }
        public string O_VariableId { get; set; }
        public Nullable<decimal> O_VariableAmount { get; set; }
        public Nullable<long> O_PromoCodeId { get; set; }
        public Nullable<System.DateTime> O_OrderDateTime { get; set; }
        public string O_PayPal_TokenId { get; set; }
        public string O_PayPal_PayerId { get; set; }
        public string O_PayPal_TrancId { get; set; }
        public string O_First_Name { get; set; }
        public string O_Last_Name { get; set; }
        public string O_Email { get; set; }
        public string O_Card_TransHash { get; set; }
        public string O_Card_TransId { get; set; }
        public Nullable<byte> OrderStateId { get; set; }
    }
}
