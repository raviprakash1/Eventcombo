using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Data;
using PagedList;
using System.Web.UI;
using NLog;

namespace EventCombo.Controllers
{

    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    public class EventListController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        string UserId = string.Empty;

        EventComboEntities db = new EventComboEntities();
        // GET: EventList



     
        [HttpGet]
        public ActionResult EventList(string SearchStringEventTitle, string hdLastTab , int page_live = 1, int page_saved = 1, int page_past = 1, int pageSize = 20)
        {
            Session["logo"] = "events";
            Session["Fromname"] = "MyList";
            MyAccount hmc = new MyAccount();
            try {
                if ((Session["AppId"] != null))
                {

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
                pageSize = pageSize > 0 ? pageSize : 20;
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

                TempData["GuestList"] = objGuestEventList.Count;

                if (!string.IsNullOrEmpty(SearchStringEventTitle) && SearchStringEventTitle != null)
                {
                    TempData["LiveEventscnt"] = objLiveEventList.Count;
                    TempData["SavedEventscnt"] = objSavedEventList.Count;
                    TempData["PastEventscnt"] = objPastEventList.Count;

                    TempData["GuestListcnt"] = objGuestEventList.Count;
                    TempData["Allcount"] = 1;

                    if (objLiveEventList.Count > 0 && objSavedEventList.Count > 0 && objPastEventList.Count > 0)
                    {
                        hdLastTab = "L";
                    }
                    if (objLiveEventList.Count <= 0 && objSavedEventList.Count > 0 && objPastEventList.Count > 0)
                    {
                        hdLastTab = "S";
                    }
                    if (objLiveEventList.Count <= 0 && objSavedEventList.Count <= 0 && objPastEventList.Count > 0)
                    {
                        hdLastTab = "P";
                    }
                    if (objLiveEventList.Count <= 0 && objSavedEventList.Count <= 0 && objPastEventList.Count <= 0)
                    {
                        hdLastTab = "";
                    }
                    if (objLiveEventList.Count <= 0 && objSavedEventList.Count > 0 && objPastEventList.Count <= 0)
                    {
                        hdLastTab = "S";
                    }
                    if (objLiveEventList.Count > 0 && objSavedEventList.Count <= 0 && objPastEventList.Count <= 0)
                    {
                        hdLastTab = "L";
                    }


                }
                else
                {
                    TempData["LiveEventscnt"] = 10;
                    TempData["SavedEventscnt"] = 10;
                    TempData["PastEventscnt"] = 10;
                    TempData["GuestListcnt"] = 0;

                    if (objLiveEventList.Count == 0 && objSavedEventList.Count == 0 && objPastEventList.Count == 0 && objGuestEventList.Count == 0)
                    {
                        TempData["Allcount"] = 0;
                    }
                    else
                    {
                        TempData["Allcount"] = 1;
                    }
                }

                TempData["hdLastTab"] = hdLastTab;
                if (objLiveEventList.Count == 0)
                    ViewData["LiveEvntCnt"] = 0;
                if (objSavedEventList.Count == 0)
                    ViewData["SavedEvntCnt"] = 0;
                if (objPastEventList.Count == 0)
                    ViewData["PastEvntCnt"] = 0;
                if (objGuestEventList.Count == 0)
                    ViewData["GuestLstCnt"] = 0;
            }catch(Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }
         
            return View();
        }


        public ActionResult EventList(string e)
        {

            Session["AppId"] = e;
            Session["Fromname"] = "MyList";
            MyAccount hmc = new MyAccount();
            try {
                if ((Session["AppId"] != null))
                {

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

                string hdLastTab = "1"; int page_live = 1; int page_saved = 1; int page_past = 1; int pageSize = 20;
                string SearchStringEventTitle = "";
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

                TempData["GuestList"] = objGuestEventList.Count;

                if (!string.IsNullOrEmpty(SearchStringEventTitle) && SearchStringEventTitle != null)
                {
                    TempData["LiveEventscnt"] = objLiveEventList.Count;
                    TempData["SavedEventscnt"] = objSavedEventList.Count;
                    TempData["PastEventscnt"] = objPastEventList.Count;

                    TempData["GuestListcnt"] = objGuestEventList.Count;
                    TempData["Allcount"] = 1;
                }
                else
                {
                    TempData["LiveEventscnt"] = 10;
                    TempData["SavedEventscnt"] = 10;
                    TempData["PastEventscnt"] = 10;
                    TempData["GuestListcnt"] = 0;

                    if (objLiveEventList.Count == 0 && objSavedEventList.Count == 0 && objPastEventList.Count == 0 && objGuestEventList.Count == 0)
                    {
                        TempData["Allcount"] = 0;
                    }
                    else
                    {
                        TempData["Allcount"] = 1;
                    }
                }

                TempData["hdLastTab"] = hdLastTab;
                if (objLiveEventList.Count == 0)
                    ViewData["LiveEvntCnt"] = 0;
                if (objSavedEventList.Count == 0)
                    ViewData["SavedEvntCnt"] = 0;
                if (objPastEventList.Count == 0)
                    ViewData["PastEvntCnt"] = 0;
                if (objGuestEventList.Count == 0)
                    ViewData["GuestLstCnt"] = 0;
            }
            catch(Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }

            return View();
        }

        public List<GetEventsListByStatus1_Result> GetLiveEvents(string SearchStringEventTitle)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                if (Session["AppId"] != null)
                    UserId = Session["AppId"].ToString();
                List<GetEventsListByStatus1_Result> liveevnt = new List<GetEventsListByStatus1_Result>();
                try {
                    liveevnt = db.GetEventsListByStatus1(SearchStringEventTitle, "Live", UserId).ToList();
                    ViewBag.LiveEvent = liveevnt.ToList();
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
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
                try
                {
                    savedevnt = db.GetEventsListByStatus1(SearchStringEventTitle, "Save", UserId).ToList();
                    ViewBag.SavedEvent = savedevnt.ToList();
                }
                catch (Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
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
                try
                {
                    pastevnt = db.GetEventsListByStatus1(SearchStringEventTitle, "Past", UserId).ToList();
                ViewBag.PastEvent = pastevnt.ToList();
                }
                catch (Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
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
                try
                {
                   
                guestlist = db.GetEventsListByStatus1(SearchStringEventTitle, "Guest", UserId).ToList();
                ViewBag.GuestList = guestlist.ToList();
                }
                catch (Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
                return guestlist.ToList();
            }
        }
    }
}