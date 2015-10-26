using System;
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
            EventId = "70";
            EventConfirmation cms = new EventConfirmation();
           var Image= db.EventImages.Where(x=>x.EventID.ToString()== EventId.ToString()).FirstOrDefault();
            var Tittle = db.Events.Where(x => x.EventID.ToString() == EventId.ToString()).Select(x => x.EventTitle).First();
            cms.Imageurl = "/Images/events/event_flyers/imagepath/" + Image.EventImageUrl;
            cms.Title = Tittle.ToString ();
            return View(cms);
        }
    }
}