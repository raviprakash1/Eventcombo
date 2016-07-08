using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class FooterController : Controller
    {
        private EmsEntities db = new EmsEntities();
        public ActionResult Footer()
        {
            Footer footer = new Footer();
            string User = "";
            string UserLatitude = "";
            string UserLongitude = "";

            if (Session["AppId"] != null)
            {
                User = Session["AppId"].ToString();
            }
            var aspuser = db.Profiles.FirstOrDefault(i => i.UserID == User);
            if (aspuser != null)
            {
                var city = db.Cities.FirstOrDefault(c => c.CityName == aspuser.City);
                if (city != null)
                {
                    UserLatitude = city.Latitude;
                    UserLongitude = city.Longitude;
                }
            }
            ViewData["UserLatitude"] = UserLatitude;
            ViewData["UserLongitude"] = UserLongitude;

            var businessPages = db.BusinessPages.Where(x => x.IsOnFooter == true).OrderBy(x => x.PageOrder).ToList();
            if (businessPages != null)
            {
                footer.BusinessPages = businessPages;
            }
            var eventTypes = db.EventTypes.Where(x => x.IsOnFooter == true).OrderBy(x => x.EventType1).ToList();
            if (eventTypes != null)
            {
                footer.EventTypes = eventTypes;
            }
            var cities = db.Cities.Where(x => x.IsOnFooter == true).OrderBy(x => x.CityName).ToList();
            if (cities != null)
            {
                footer.Cities = cities;
            }
            return View(footer);
        }
    }
}
