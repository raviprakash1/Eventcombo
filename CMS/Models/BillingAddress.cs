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
    
    public partial class BillingAddress
    {
        public long BillingId { get; set; }
        public string UserId { get; set; }
        public string OrderId { get; set; }
        public string Guid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phone_Number { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string PaymentType { get; set; }
        public string CardId { get; set; }
        public string card_type { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }
    }
}
