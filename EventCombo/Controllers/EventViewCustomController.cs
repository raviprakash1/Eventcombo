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

namespace EventCombo.Controllers
{
  
    public class EventViewCustomController : Controller
    {
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