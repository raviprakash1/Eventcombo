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
    
    public partial class Ticket_Quantity_Detail
    {
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
    }
}
