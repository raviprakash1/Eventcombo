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
                var liveevtcount = db.Database.SqlQuery<Int32>("SELECT COUNT(*) from (SELECT ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime FROM Event ev inner JOIN EventVenue evn ON ev.EventID=evn.EventID  WHERE UPPER(ev.EventStatus)='LIVE' Union SELECT ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime FROM Event ev inner JOIN MultipleEvent mevn ON ev.EventID=mevn.EventID  WHERE UPPER(ev.EventStatus)='LIVE') as LiveEvntCnt ").FirstOrDefault();                
                TempData["LiveEvents"] = liveevtcount;
            }
            foreach (var item in objSavedEventList)
            {
                item.TicketSold = "0";
                item.TotalTicket = "0";
                var savedevtcount = db.Database.SqlQuery<Int32>("SELECT COUNT(*) from (SELECT ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime FROM Event ev inner JOIN EventVenue evn ON ev.EventID=evn.EventID  WHERE UPPER(ev.EventStatus)='SAVE' Union SELECT ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime FROM Event ev inner JOIN MultipleEvent mevn ON ev.EventID=mevn.EventID  WHERE UPPER(ev.EventStatus)='SAVE') as SavedEvntCnt ").FirstOrDefault();                
                TempData["SavedEvents"] = savedevtcount;
            }
            foreach (var item in objPastEventList)
            {
                item.TicketSold = "0";
                item.TotalTicket = "0";
                var pastevtcount = db.Database.SqlQuery<Int32>("Select count(*) from (select  ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime from Event ev left outer join EventVenue evn on ev.EventID=evn.EventID where evn.EventStartDate<GETDATE() union select  ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime from Event ev left outer join MultipleEvent mevn on ev.EventID=mevn.EventID where CONVERT(varchar,mevn.StartingFrom,101)<GETDATE()) as PastEvntCnt  ").FirstOrDefault();                
                TempData["PastEvents"] = pastevtcount;
            }
            return View();
        }
        public List<EventListTemplate> GetLiveEvents()
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var liveevnt = db.Database.SqlQuery<EventListTemplate>("SELECT ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime FROM Event ev inner JOIN EventVenue evn ON ev.EventID=evn.EventID  WHERE UPPER(ev.EventStatus)='LIVE' Union SELECT ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime FROM Event ev inner JOIN MultipleEvent mevn ON ev.EventID=mevn.EventID  WHERE UPPER(ev.EventStatus)='LIVE' ").ToList();
                //var savedevnt = (from Ev in db.Events
                //                 join Even in db.EventVenues on Ev.EventID equals Even.EventID
                //                 where Ev.EventStatus == "Live"
                //                 select new EventListTemplate
                //                 {
                //                     EventID = Ev.EventID,
                //                     EventTitle = Ev.EventTitle,
                //                     EventDate = Even.EventStartDate,
                //                     EventTime = Even.EventStartTime
                //                 }
                //                    );
                ViewBag.LiveEvent= liveevnt.ToList();
                return liveevnt.ToList();
            }            
        }
        public List<EventListTemplate> GetSavedEvents()
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var savedevnt = db.Database.SqlQuery<EventListTemplate>("SELECT ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime FROM Event ev inner JOIN EventVenue evn ON ev.EventID=evn.EventID  WHERE UPPER(ev.EventStatus)='SAVE' Union SELECT ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime FROM Event ev inner JOIN MultipleEvent mevn ON ev.EventID=mevn.EventID  WHERE UPPER(ev.EventStatus)='SAVE' ").ToList();
                //var savedevnt = (from Ev in db.Events
                //                 join Even in db.EventVenues on Ev.EventID equals Even.EventID
                //                 where Ev.EventStatus == "Save"
                //                 select new EventListTemplate
                //                 {
                //                     EventID = Ev.EventID,
                //                     EventTitle = Ev.EventTitle,
                //                     EventDate = Even.EventStartDate,
                //                     EventTime = Even.EventStartTime
                //                 }
                //                );
                ViewBag.SavedEvent = savedevnt.ToList();
                return savedevnt.ToList();
            }
        }
        public List<EventListTemplate> GetPastEvents()
        {
            using (EventComboEntities db = new EventComboEntities())
            {                
                var pastevnt = db.Database.SqlQuery<EventListTemplate>("select  ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime from Event ev left outer join EventVenue evn on ev.EventID=evn.EventID where evn.EventStartDate<GETDATE() union select  ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime from Event ev left outer join MultipleEvent mevn on ev.EventID=mevn.EventID where CONVERT(varchar,mevn.StartingFrom,101)<GETDATE() ").ToList();             
                ViewBag.PastEvent = pastevnt.ToList();
                return pastevnt.ToList();
            }
        }
    }
}