using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EventCombo.Models
{
    [Table("Event")]
    public class EventCreation
    {
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
        public string IsMultipleEvent { get; set; }
        public string TimeZone { get; set; }
        public string FBUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string AddressStatus { get; set; }
        public Nullable<long> LastLocationAddress { get; set; }
        public string EnableFBDiscussion { get; set; }
        public string Ticket_DAdress { get; set; }
        public string Ticket_showremain { get; set; }
        public string Ticket_showvariable { get; set; }
        public string Ticket_variabledesc { get; set; }
        public string Ticket_variabletype { get; set; }
        public Address[] AddressDetail { get; set; }
        public EventVenue[] EventVenue { get; set; }
        public MultipleEvent[] MultipleEvents { get; set; }
        public Organizer_Master[] Orgnizer { get; set; }
        public Ticket[] Ticket { get; set; }
        public EventImage[] EventImage { get; set; }
        public Event_VariableDesc[] EventVariable { get; set; }

        public string ShowMap { get; set; }

        public string DuplicateEvent { get; set; }

        public string ModifyDate { get; set; }
        public string Cancelevent { get; set; }
        public string Isadmin { get; set; }

        public long? Parent_EventID { get; set; }

    }

    public partial class Organizer_Master
    {
        public string DefaultOrg { get; set; }
        public string EditOrg { get; set; }

        public long Eventid { get; set; }
    }
    public class ManageEvent
    {
        public string Eventstatus { get; set; }
        public string Eventtitle { get; set; }
        public string EventAddress { get; set; }
        public string Eventdate { get; set; }
        public string EventExpired { get; set; }

        public long Eventid { get; set; }
        public string Eventprivacy { get; set; }
        public string Eventtransaction { get; set; }

        public List<OrderAttendees> Order{ get; set; }
        public List<OrderAttendees> Attendess { get; set; }
        public string Eventcancel { get; set; }
        public string url { get; internal set; }
        public string Descritption { get; internal set; }

        public long EventHits { get; set; }

    }

    public class OrderAttendees
    {
        public string Qty { get; set; }
        public string Amount { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
}