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
            TempData["LiveEvents"] = objLiveEventList.Count;
            TempData["SavedEvents"] = objSavedEventList.Count;
            TempData["PastEvents"] = objPastEventList.Count;
            
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