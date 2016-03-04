using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;

namespace EventCombo.Controllers
{
    public class OrganizerInfoController : Controller
    {
        EventComboEntities db = new EventComboEntities();
        // GET: OrganizerInfo
        public ActionResult Index(int id)
        {
            Organizer_Master mst = (from x in db.Organizer_Master where x.Orgnizer_Id == id select x).FirstOrDefault();


            return View(mst);
        }
    }
}