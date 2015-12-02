﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;

namespace EventCombo.Controllers
{
    public class EventConfirmationController : Controller
    {
        // GET: EventConfirmation
        EventComboEntities db = new EventComboEntities();
        public ActionResult EventConfirmation(string EventId)
        {
           
            EventConfirmation cms = new EventConfirmation();
           var Image= db.EventImages.Where(x=>x.EventID.ToString()== EventId.ToString()).FirstOrDefault();
            var Eventdetails = db.Events.Where(x => x.EventID.ToString() == EventId.ToString()).FirstOrDefault();
            if (Image != null)
            {
                cms.Imageurl = "/Images/events/event_flyers/imagepath/" + Image.EventImageUrl;
            }
            cms.Title = Eventdetails.EventTitle.ToString ();
            var evid = long.Parse(EventId);
            var evAdress = (from ev in db.Addresses where ev.EventId == evid select ev).FirstOrDefault();
           
            var GetEventDate = db.GetEventDateList(evid).FirstOrDefault();
            cms.Startdate = GetEventDate.Dayofweek + " " + GetEventDate.Datefrom + "," + GetEventDate.Time;
            cms.Address = (evAdress!=null ? evAdress.ConsolidateAddress : "");
            var url = Request.Url;
            var baseurl = url.GetLeftPart(UriPartial.Authority);
            cms.url = baseurl + Url.Action("ViewEvent", "CreateEvent") + "?strUrlData=" + cms.Title.Trim() + "­౼" + EventId + "౼N";
            cms.Descritption = Eventdetails.EventDescription;
            cms.EventId = EventId;
            return View(cms);
        }
    }
}