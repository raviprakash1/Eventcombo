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
    
    public partial class LockOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LockOrder()
        {
            this.LockTickets = new HashSet<LockTicket>();
        }
    
        public System.Guid LockOrderId { get; set; }
        public long EventId { get; set; }
        public System.DateTime Locktime { get; set; }
        public string UserId { get; set; }
        public string IP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LockTicket> LockTickets { get; set; }
    }
}
