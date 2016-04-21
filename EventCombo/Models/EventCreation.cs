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
        EventComboEntities db = new EventComboEntities();
        public long EventID { get; set; }
        public long EventTypeID { get; set; }
        public long EventCategoryID { get; set; }
        public long EventSubCategoryID { get; set; }
        public string UserID { get; set; }
        public string EventTitle { get; set; }
        public string DisplayStartTime { get; set; }
        public string DisplayEndTime { get; set; }
        public string DisplayTimeZone { get; set; }
        [AllowHtml]
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



        public Event GetEventdetail(long eventid)
        {
            return ((from ev in db.Events where ev.EventID == eventid select ev).FirstOrDefault());
        }
        public List<string> GetImages(long EventId)
        {
            using (EventComboEntities db = new EventComboEntities())
            {

                return (from myRow in db.EventImages

                        where myRow.EventID == EventId
                        orderby myRow.EventImageID
                        select "/Images/events/event_flyers/imagepath/" + myRow.EventImageUrl).ToList();



            }


        }

        public long GetLatestEventId(long lEvntId)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var lParentID = (from myEvt in db.Events
                                 where myEvt.EventID == lEvntId
                                 select myEvt.Parent_EventID).FirstOrDefault();
                if (lParentID > 0)
                {
                    lParentID = (from myEvt in db.Events
                                 where myEvt.Parent_EventID == lParentID
                                 select myEvt.EventID).Max();
                }
                else
                {
                    int lCnt = (from myEvt in db.Events
                                where myEvt.Parent_EventID == lEvntId
                                select myEvt.EventID).Count();

                    if (lCnt > 0)
                    {
                        lParentID = (from myEvt in db.Events
                                     where myEvt.Parent_EventID == lEvntId
                                     select myEvt.EventID).Max();
                    }
                }
                if (lParentID == null || lParentID <= 0) lParentID = lEvntId;

                return (long)lParentID;
            }
        }

        public Event GetSelectedEventDetail(string strGuid)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var EventId = (from MyE in db.Ticket_Locked_Detail where MyE.TLD_GUID == strGuid select MyE.TLD_Event_Id).FirstOrDefault();
                var MyEvent = (from MyEv in db.Events
                               where MyEv.EventID == EventId
                               select MyEv).FirstOrDefault();
                return MyEvent;
            }
        }
    }

    public partial class Organizer_Master
    {
        public string DefaultOrg { get; set; }
        public string EditOrg { get; set; }

        public long Eventid { get; set; }

        public List<Organiserevent> presentevent { get; set; }
        public List<Organiserevent> pastevent { get; set; }
        public int pasteventcount { get; set; }
        public int presentevtcount { get; set; }
     
        public int maxsetcount { get; set; }
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
        public int DiscountCode { get; set; }

    }

    public class OrderAttendees
    {
        public string Qty { get; set; }
        public string Amount { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }

    public class Organiserevent
    {
        public string FirstImage { get; set; }
        public string Eventtitle { get; set; }
        public string Dateofevent { get; set; }
        public DateTime Dateofeventsort { get; set; }
        public string Venue { get; set; }
        public string eventpath { get; set; }
    }
}