using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Controllers
{
    public class CreateEventController : Controller
    {
        // GET: CreateEvent
        public ActionResult CreateEvent()
        {
            Session["Fromname"] = "events";
            return View();
        }


        #region Ticketing 





        #endregion

    }
}