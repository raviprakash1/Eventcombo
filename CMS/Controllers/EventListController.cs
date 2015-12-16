using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using PagedList;
namespace CMS.Controllers
{
    public class EventListController : Controller
    {
        // GET: EventList
        EmsEntities db = new EmsEntities();
         string UserId = string.Empty;
        public ActionResult Index(string UserId)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListEvent(string UserId, string SearchStringEventTitle, string hdLastTab, int page_live = 1, int page_saved = 1, int page_past = 1, int pageSize = 20)
        {

            UserId = UserId;
            TempData["User"] = UserId;
            page_live = page_live > 0 ? page_live : 1;
            page_saved = page_saved > 0 ? page_saved : 1;
            page_past = page_past > 0 ? page_past : 1;
            pageSize = pageSize > 0 ? pageSize : 2;
            if (string.IsNullOrEmpty(SearchStringEventTitle))
                SearchStringEventTitle = "";
            List<GetEventsListByStatus_Result> objLiveEventList = GetLiveEvents(UserId,SearchStringEventTitle);
            List<GetEventsListByStatus_Result> objSavedEventList = GetSavedEvents(UserId,SearchStringEventTitle);
            List<GetEventsListByStatus_Result> objPastEventList = GetPastEvents(UserId,SearchStringEventTitle);
            List<GetEventsListByStatus_Result> objGuestEventList = GetGuestsList(UserId,SearchStringEventTitle);

            ViewBag.LiveEvent = objLiveEventList;
            ViewBag.SavedEvent = objSavedEventList;
            ViewBag.PastEvent = objPastEventList;

            TempData["LiveEvents"] = objLiveEventList.Count;
            TempData["SavedEvents"] = objSavedEventList.Count;
            TempData["PastEvents"] = objPastEventList.Count;
            TempData["hdLastTab"] = hdLastTab;
            TempData["UserId"] = UserId;
            TempData["GuestList"] = objGuestEventList.Count;

            if (objLiveEventList.Count == 0)
                ViewData["LiveEvntCnt"] = 0;
            if (objSavedEventList.Count == 0)
                ViewData["SavedEvntCnt"] = 0;
            if (objPastEventList.Count == 0)
                ViewData["PastEvntCnt"] = 0;
            if (objGuestEventList.Count == 0)
                ViewData["GuestLstCnt"] = 0;
            return View();
        }
        public List<GetEventsListByStatus_Result> GetLiveEvents(string user,string SearchStringEventTitle)
        {
            using (EmsEntities db = new EmsEntities())
            {
                
                    UserId = user.ToString();
                List<GetEventsListByStatus_Result> liveevnt = new List<GetEventsListByStatus_Result>();
                liveevnt = db.GetEventsListByStatus(SearchStringEventTitle, "Live", UserId).ToList();
                ViewBag.LiveEvent = liveevnt.ToList();
                return liveevnt.ToList();
            }
        }
        public List<GetEventsListByStatus_Result> GetSavedEvents(string user, string SearchStringEventTitle)
        {
            using (EmsEntities db = new EmsEntities())
            {
               
                    UserId = user.ToString();
                List<GetEventsListByStatus_Result> savedevnt = new List<GetEventsListByStatus_Result>();
                savedevnt = db.GetEventsListByStatus(SearchStringEventTitle, "Save", UserId).ToList();
                ViewBag.SavedEvent = savedevnt.ToList();
                return savedevnt.ToList();
            }
        }
        public List<GetEventsListByStatus_Result> GetPastEvents(string user,string SearchStringEventTitle)
        {
            using (EmsEntities db = new EmsEntities())
            {
               
                    UserId = user.ToString();
                List<GetEventsListByStatus_Result> pastevnt = new List<GetEventsListByStatus_Result>();
                pastevnt = db.GetEventsListByStatus(SearchStringEventTitle, "Past", UserId).ToList();
                ViewBag.PastEvent = pastevnt.ToList();
                return pastevnt.ToList();
            }
        }
        public List<GetEventsListByStatus_Result> GetGuestsList(string user,string SearchStringEventTitle)
        {
            using (EmsEntities db = new EmsEntities())
            {
                
                    UserId = user.ToString();
                List<GetEventsListByStatus_Result> guestlist = new List<GetEventsListByStatus_Result>();
                guestlist = db.GetEventsListByStatus(SearchStringEventTitle, "Guest", UserId).ToList();
                ViewBag.GuestList = guestlist.ToList();
                return guestlist.ToList();
            }
        }

    }
}