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
        public long TPD_Id { get; set; }
        public string TPD_User_Id { get; set; }
        public string TPD_Order_Id { get; set; }
        public Nullable<long> TPD_Purchased_Qty { get; set; }
        public Nullable<long> TPD_TQD_Id { get; set; }
        public Nullable<long> TPD_Event_Id { get; set; }
        public Nullable<decimal> TPD_Amount { get; set; }
        public Nullable<decimal> TPD_Donate { get; set; }
        public string TPD_GUID { get; set; }
    }
}
