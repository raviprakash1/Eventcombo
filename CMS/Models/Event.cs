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
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Addresses = new HashSet<Address>();
            this.Event_VariableDesc = new HashSet<Event_VariableDesc>();
            this.EventImages = new HashSet<EventImage>();
            this.EventVenues = new HashSet<EventVenue>();
            this.MultipleEvents = new HashSet<MultipleEvent>();
            this.Tickets = new HashSet<Ticket>();
            this.Event_Orgnizer_Detail = new HashSet<Event_Orgnizer_Detail>();
            this.Event_OrganizerMessages = new HashSet<Event_OrganizerMessages>();
            this.Ticket_Purchased_Detail = new HashSet<Ticket_Purchased_Detail>();
            this.EventFavourites = new HashSet<EventFavourite>();
        }
    
        public long EventID { get; set; }
        public long EventTypeID { get; set; }
        public long EventCategoryID { get; set; }
        public long EventSubCategoryID { get; set; }
        public string UserID { get; set; }
        public string EventTitle { get; set; }
        public string DisplayStartTime { get; set; }
        public string DisplayEndTime { get; set; }
        public string DisplayTimeZone { get; set; }
        public string EventDescription { get; set; }
        public string EventPrivacy { get; set; }
        public string Private_ShareOnFB { get; set; }
        public string Private_GuestOnly { get; set; }
        public string Private_Password { get; set; }
        public string EventUrl { get; set; }
        public string PublishOnFB { get; set; }
        public string EventStatus { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> UpdateOn { get; set; }
        public string IsMultipleEvent { get; set; }
        public string TimeZone { get; set; }
        public string FBUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string AddressStatus { get; set; }
        public Nullable<long> LastLocationAddress { get; set; }
        public string EnableFBDiscussion { get; set; }
        public Nullable<int> Feature { get; set; }
        public string Ticket_DAdress { get; set; }
        public string Ticket_showremain { get; set; }
        public string Ticket_showvariable { get; set; }
        public string Ticket_variabledesc { get; set; }
        public string Ticket_variabletype { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ShowMap { get; set; }
        public Nullable<long> Parent_EventID { get; set; }
        public string EventCancel { get; set; }
        public Nullable<System.DateTime> FeatureUpdateDate { get; set; }
        public Nullable<long> ECBackgroundId { get; set; }
        public string BackgroundColor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual EventType EventType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_VariableDesc> Event_VariableDesc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventImage> EventImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventVenue> EventVenues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MultipleEvent> MultipleEvents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_Orgnizer_Detail> Event_Orgnizer_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_OrganizerMessages> Event_OrganizerMessages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket_Purchased_Detail> Ticket_Purchased_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventFavourite> EventFavourites { get; set; }
    }
}
