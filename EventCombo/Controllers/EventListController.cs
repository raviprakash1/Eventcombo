using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Data;

namespace EventCombo.Controllers
{
    public class EventListController : Controller
    {
        EventComboEntities db = new EventComboEntities();
        // GET: EventList
        public ActionResult EventList()
        {
            List<EventListTemplate> objLiveEventList = GetLiveEvents();
            List<EventListTemplate> objSavedEventList = GetSavedEvents();
            List <EventListTemplate> objPastEventList = GetPastEvents();
            foreach (var item in objLiveEventList)
            {
                item.TicketSold = "0";
                item.TotalTicket = "0";
                var liveevtcount = db.Database.SqlQuery<Int32>("SELECT  COUNT(*) FROM Event ev LEFT OUTER JOIN EventVenue evn ON ev.EventID=evn.EventID LEFT OUTER JOIN MultipleEvent mvn ON ev.EventID=mvn.EventID WHERE UPPER(ev.EventStatus)='LIVE' ").FirstOrDefault();                
                TempData["LiveEvents"] = liveevtcount;
            }
            foreach (var item in objSavedEventList)
            {
                item.TicketSold = "0";
                item.TotalTicket = "0";
                var savedevtcount = db.Database.SqlQuery<Int32>("SELECT  COUNT(*) FROM Event ev LEFT OUTER JOIN EventVenue evn ON ev.EventID=evn.EventID LEFT OUTER JOIN MultipleEvent mvn ON ev.EventID=mvn.EventID WHERE UPPER(ev.EventStatus)='SAVE' ").FirstOrDefault();                
                TempData["SavedEvents"] = savedevtcount;
            }
            foreach (var item in objPastEventList)
            {
                item.TicketSold = "0";
                item.TotalTicket = "0";
                var pastevtcount = db.Database.SqlQuery<Int32>("select  count(*) from Event ev left outer join EventVenue evn on ev.EventID=evn.EventID where evn.EventStartDate<GETDATE()  ").FirstOrDefault();                
                TempData["PastEvents"] = pastevtcount;
            }
            return View();
        }
        public List<EventListTemplate> GetLiveEvents()
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var savedevnt = db.Database.SqlQuery<EventListTemplate>("SELECT  * FROM Event ev LEFT OUTER JOIN EventVenue evn ON ev.EventID=evn.EventID LEFT OUTER JOIN MultipleEvent mvn ON ev.EventID=mvn.EventID WHERE UPPER(ev.EventStatus)='LIVE' ").ToList();
                //var modelEventTemp = (from Ev in db.Events
                //                     join Even in db.EventVenues on Ev.EventID equals Even.EventID                                      
                //                     where Ev.EventStatus =="Live"
                //                     select new EventListTemplate
                //                     {
                //                        EventTitle = Ev.EventTitle,
                //                        EventDate = Even.EventStartDate,
                //                        EventTime = Even.EventStartTime                                         
                //                     }
                //                    );
                ViewBag.LiveEvent= savedevnt.ToList();
                return savedevnt.ToList();
            }            
        }
        public List<EventListTemplate> GetSavedEvents()
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var savedevnt = db.Database.SqlQuery<EventListTemplate>("SELECT  * FROM Event ev LEFT OUTER JOIN EventVenue evn ON ev.EventID=evn.EventID LEFT OUTER JOIN MultipleEvent mvn ON ev.EventID=mvn.EventID WHERE UPPER(ev.EventStatus)='SAVE' ").ToList();
                //var modelEventTemp = (from Ev in db.Events
                //                      join Even in db.EventVenues on Ev.EventID equals Even.EventID
                //                      where Ev.EventStatus == "Save"
                //                      select new EventListTemplate
                //                      {
                //                          EventTitle = Ev.EventTitle,
                //                          EventDate = Even.EventStartDate,
                //                          EventTime = Even.EventStartTime
                //                      }
                //                    );
                ViewBag.SavedEvent = savedevnt.ToList();
                return savedevnt.ToList();
            }
        }
        public List<EventListTemplate> GetPastEvents()
        {
            using (EventComboEntities db = new EventComboEntities())
            {                
                var pastevnt = db.Database.SqlQuery<EventListTemplate>("select  * from Event ev left outer join EventVenue evn on ev.EventID=evn.EventID where evn.EventStartDate<GETDATE() ").ToList();             
                ViewBag.PastEvent = pastevnt.ToList();
                return pastevnt.ToList();
            }
        }
    }
}