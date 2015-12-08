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
        string UserId = string.Empty;
        EventComboEntities db = new EventComboEntities();
        // GET: EventList
        public ActionResult EventList(string SearchStringEventTitle)
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
            if (string.IsNullOrEmpty(SearchStringEventTitle))
                SearchStringEventTitle = "";            
            List<GetEventsListByStatus1_Result> objLiveEventList = GetLiveEvents(SearchStringEventTitle);
            List<GetEventsListByStatus1_Result> objSavedEventList = GetSavedEvents(SearchStringEventTitle);
            List <GetEventsListByStatus1_Result> objPastEventList = GetPastEvents(SearchStringEventTitle);
            List<GetEventsListByStatus1_Result> objGuestEventList = GetGuestsList(SearchStringEventTitle);
            TempData["LiveEvents"] = objLiveEventList.Count;
            TempData["SavedEvents"] = objSavedEventList.Count;
            TempData["PastEvents"] = objPastEventList.Count;            
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