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
    
    public partial class Ticket_Locked_Detail
    {
        public long TLD_Id { get; set; }
        public string TLD_User_Id { get; set; }
        public Nullable<long> TLD_Locked_Qty { get; set; }
        public Nullable<long> TLD_Event_Id { get; set; }
        public Nullable<long> TLD_TQD_Id { get; set; }
    
        public virtual Ticket_Locked_Detail Ticket_Locked_Detail1 { get; set; }
        public virtual Ticket_Locked_Detail Ticket_Locked_Detail2 { get; set; }
    }
}