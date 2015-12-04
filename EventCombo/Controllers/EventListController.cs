using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Data;
using PagedList;

namespace EventCombo.Controllers
{
    public class EventListController : Controller
    {
        string UserId = string.Empty;
        EventComboEntities db = new EventComboEntities();
        // GET: EventList
        public ActionResult EventList(string SearchStringEventTitle,int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
                        
            if (string.IsNullOrEmpty(SearchStringEventTitle))
                SearchStringEventTitle = "";            
            List<GetEventsListByStatus1_Result> objLiveEventList = GetLiveEvents(SearchStringEventTitle);
            List<GetEventsListByStatus1_Result> objSavedEventList = GetSavedEvents(SearchStringEventTitle);
            List <GetEventsListByStatus1_Result> objPastEventList = GetPastEvents(SearchStringEventTitle);
            List<GetEventsListByStatus1_Result> objGuestEventList = GetGuestsList(SearchStringEventTitle);
            foreach (var item in objLiveEventList)
            {                
                var liveevtcount = db.Database.SqlQuery<Int32>("SELECT COUNT(*) from (SELECT ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime FROM Event ev inner JOIN EventVenue evn ON ev.EventID=evn.EventID  WHERE ev.UserID='"+UserId+"' AND UPPER(ev.EventStatus)='LIVE' and EventTitle like '%" + SearchStringEventTitle + "%' Union SELECT ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime FROM Event ev inner JOIN MultipleEvent mevn ON ev.EventID=mevn.EventID  WHERE  ev.UserID='"+UserId+"' AND UPPER(ev.EventStatus)='LIVE' and EventTitle like '%" + SearchStringEventTitle + "%')  as LiveEvntCnt ").FirstOrDefault();                
                TempData["LiveEvents"] = liveevtcount;
            }
            foreach (var item in objSavedEventList)
            {                
                var savedevtcount = db.Database.SqlQuery<Int32>("SELECT COUNT(*) from (SELECT ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime FROM Event ev inner JOIN EventVenue evn ON ev.EventID=evn.EventID  WHERE ev.UserID='" + UserId + "' AND UPPER(ev.EventStatus)='SAVE' and EventTitle like '%" + SearchStringEventTitle + "%' Union SELECT ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime FROM Event ev inner JOIN MultipleEvent mevn ON ev.EventID=mevn.EventID  WHERE ev.UserID='" + UserId + "' AND UPPER(ev.EventStatus)='SAVE' and EventTitle like '%" + SearchStringEventTitle + "%') as SavedEvntCnt ").FirstOrDefault();                
                TempData["SavedEvents"] = savedevtcount;
            }
            foreach (var item in objPastEventList)
            {                
                var pastevtcount = db.Database.SqlQuery<Int32>("Select count(*) from (select  ev.EventID,ev.EventTitle, evn.EventStartDate as EventDate,evn.EventStartTime as EventTime from Event ev left outer join EventVenue evn on ev.EventID=evn.EventID where ev.UserID='" + UserId + "' AND evn.EventStartDate<GETDATE() and EventTitle like '%" + SearchStringEventTitle + "%' union select  ev.EventID,ev.EventTitle,mevn.StartingFrom as EventDate,mevn.StartTime as EventTime from Event ev left outer join MultipleEvent mevn on ev.EventID=mevn.EventID where ev.UserID='" + UserId + "' AND CONVERT(varchar,mevn.StartingFrom,101)<GETDATE() and EventTitle like '%" + SearchStringEventTitle+"%') as PastEvntCnt  ").FirstOrDefault();                
                TempData["PastEvents"] = pastevtcount;
            }
            int iCount = 0;
            List<SelectListItem> PageFilter = new List<SelectListItem>();
            int i = 0; int z = 0; int iUcount = objLiveEventList.Count; int iGapValue = 3;
            string strText = "";
            PageFilter.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "0",
                Selected = (iCount == 0 ? true : false)
            });
            if (iUcount > iGapValue)
            {
                for (i = 0; i < iUcount; i++)
                {
                    strText = z.ToString() + " - " + (z + iGapValue).ToString();
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = strText,
                        Value = (z + iGapValue).ToString(),
                        Selected = (iCount == z ? true : false)
                    });
                    z = z + iGapValue;
                    iUcount = iUcount - iGapValue;
                    if (iUcount < iGapValue)
                    {
                        strText = z.ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (z + iGapValue).ToString(),
                            Selected = (iCount == z ? true : false)
                        });
                        iUcount = 0;
                    }
                }
            }
            else
            {
                PageFilter.Add(new SelectListItem()
                {
                    Text = "0 - 3",
                    Value = "3",
                    Selected = (iCount == 10 ? true : false)
                });

            }

            ViewBag.PageF = PageFilter;

            return View();
        }
        public List<GetEventsListByStatus1_Result> GetLiveEvents(string SearchStringEventTitle)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                if (Session["AppId"] != null)
                    UserId = Session["AppId"].ToString();
                List<GetEventsListByStatus1_Result> liveevnt = new List<GetEventsListByStatus1_Result>();                
                liveevnt = db.GetEventsListByStatus1(SearchStringEventTitle, "Live", UserId).ToList();
                ViewBag.LiveEvent= liveevnt.ToList();
                return liveevnt.ToList();
            }            
        }
        public List<GetEventsListByStatus1_Result> GetSavedEvents(string SearchStringEventTitle)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                if (Session["AppId"] != null)
                    UserId = Session["AppId"].ToString();
                List<GetEventsListByStatus1_Result> savedevnt = new List<GetEventsListByStatus1_Result>();
                savedevnt = db.GetEventsListByStatus1(SearchStringEventTitle, "Save", UserId).ToList();
                ViewBag.SavedEvent = savedevnt.ToList();
                return savedevnt.ToList();
            }
        }
        public List<GetEventsListByStatus1_Result> GetPastEvents(string SearchStringEventTitle)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                if (Session["AppId"] != null)
                    UserId = Session["AppId"].ToString();
                List<GetEventsListByStatus1_Result> pastevnt = new List<GetEventsListByStatus1_Result>();
                pastevnt = db.GetEventsListByStatus1(SearchStringEventTitle, "Past", UserId).ToList();                
                ViewBag.PastEvent = pastevnt.ToList();
                return pastevnt.ToList();
            }
        }
        public List<GetEventsListByStatus1_Result> GetGuestsList(string SearchStringEventTitle)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                if (Session["AppId"] != null)
                    UserId = Session["AppId"].ToString();
                List<GetEventsListByStatus1_Result> guestlist = new List<GetEventsListByStatus1_Result>();
                guestlist = db.GetEventsListByStatus1(SearchStringEventTitle, "Guest", UserId).ToList();
                ViewBag.GuestList = guestlist.ToList();
                return guestlist.ToList();
            }
        }
    }
}