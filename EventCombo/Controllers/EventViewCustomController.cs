using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text.RegularExpressions;
using EventCombo.Service;
using NLog;

namespace EventCombo.Controllers
{
  
    public class EventViewCustomController : BaseController
    {
        private IEventService _eService;
        private ILogger _logger;
        public EventViewCustomController()
      : base()
        {
            _eService = new EventService(_factory, _mapper);
            _logger = LogManager.GetCurrentClassLogger();
        }
        // GET: EventViewCustom
        public ActionResult Index(string strCustomUrl)
        {
            Event objEv = GetEventByUniqueUrl(strCustomUrl);
            if (objEv != null)
            {
                TempData["EventName"] = Regex.Replace(objEv.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", "");
                TempData["EventId"] = objEv.EventID.ToString();
                return RedirectToAction("ViewEvent", "EventManagement", new { strEventDs = Regex.Replace(objEv.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = objEv.EventID.ToString() });
                //return View();
            }
            else {
                return RedirectToAction("Index", "");
            }
        }
        public ActionResult ViewEvent()
        {
            long eventId;
            var strEventId = RouteData.Values["EventId"];
            var mainDomain= RouteData.Values["Domain"];
            if (strEventId == null)           
                return RedirectToAction("Index", "Home");            
            if (!Int64.TryParse(strEventId.ToString(), out eventId))
                return RedirectToAction("Index", "Home");

            string userId = "";
            if (Session["AppId"] != null)
                userId = Session["AppId"].ToString();

            EventInfoViewModel ev = _eService.GetEventInfo(eventId, userId, Url);
            PopulateBaseViewModel(ev, String.Format("{0} | Eventcombo", ev.EventTitle));

            var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(ev.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ev.EventId.ToString() });
            Session["ReturnUrl"] = "ViewEvent~" + url;
            return Redirect(Request.Url.Scheme + System.Uri.SchemeDelimiter + mainDomain + "/e/"+ Regex.Replace(ev.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", "") + "-"+ ev.EventId.ToString() + "");
        }
        public Event GetEventByUniqueUrl(string strUniqueUrl)
        {
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vEvent = (from myEvent in objEnt.Events where myEvent.EventUrl == strUniqueUrl select myEvent).FirstOrDefault();
                    return vEvent;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}