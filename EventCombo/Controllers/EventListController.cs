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
        [HttpGet]
        public ActionResult EventList(string SearchStringEventTitle, string hdLastTab , int page_live = 1, int page_saved = 1, int page_past = 1, int pageSize = 20)
        {
           
            if ((Session["AppId"] != null))
            {
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            page_live = page_live > 0 ? page_live : 1;
            page_saved = page_saved > 0 ? page_saved : 1;
            page_past = page_past > 0 ? page_past : 1;
            pageSize = pageSize > 0 ? pageSize : 2;
            if (string.IsNullOrEmpty(SearchStringEventTitle))
                SearchStringEventTitle = "";
            List<GetEventsListByStatus1_Result> objLiveEventList = GetLiveEvents(SearchStringEventTitle);
            List<GetEventsListByStatus1_Result> objSavedEventList = GetSavedEvents(SearchStringEventTitle);
            List<GetEventsListByStatus1_Result> objPastEventList = GetPastEvents(SearchStringEventTitle);
            List<GetEventsListByStatus1_Result> objGuestEventList = GetGuestsList(SearchStringEventTitle);

            ViewBag.LiveEvent = objLiveEventList.ToPagedList(page_live, pageSize);
            ViewBag.SavedEvent = objSavedEventList.ToPagedList(page_saved, pageSize);
            ViewBag.PastEvent = objPastEventList.ToPagedList(page_past, pageSize);

            TempData["LiveEvents"] = objLiveEventList.Count;
            TempData["SavedEvents"] = objSavedEventList.Count;
            TempData["PastEvents"] = objPastEventList.Count;
            TempData["hdLastTab"] = hdLastTab;
            


            if (objLiveEventList.Count == 0)
                ViewData["LiveEvntCnt"] = 0;
            if (objSavedEventList.Count == 0)
                ViewData["SavedEvntCnt"] = 0;
            if (objPastEventList.Count == 0)
                ViewData["PastEvntCnt"] = 0;

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
                ViewBag.LiveEvent = liveevnt.ToList();
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