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
        public ActionResult Index(int id,long eventid)
        {
            Organizer_Master mst = (from x in db.Organizer_Master where x.Orgnizer_Id == id select x).FirstOrDefault();

            mst.Imagepath = !string.IsNullOrEmpty(mst.Imagepath) ? mst.Imagepath : "Images/default_org_image.jpg";
            mst.Eventid = eventid;
            return View(mst);
        }
    }
}