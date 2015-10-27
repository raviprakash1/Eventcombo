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
        public int EventTypeID { get; set; }
        public int EventCategoryID { get; set; }
        public int EventSubCategoryID { get; set; }
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
        public long LastLocationAddress { get; set; }
        public string EnableFBDiscussion { get; set; }
        public string Ticket_DAdress { get; set; }
        public string Ticket_showremain { get; set; }
        public string Ticket_showvariable { get; set; }
        public string Ticket_variabledesc { get; set; }
        public string Ticket_variabletype { get; set; }
        public Address[] AddressDetail { get; set; }
        public EventVenue[] EventVenue { get; set; }
        public MultipleEvent[] MultipleEvents { get; set; }
        public Event_Orgnizer_Detail[] Orgnizer { get; set; }
        public Ticket[] Ticket { get; set; }
        public EventImage[] EventImage { get; set; }
        public Event_VariableDesc[] EventVariable { get; set; }

    }
}