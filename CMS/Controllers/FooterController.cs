using CMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if ((Session["UserID"] != null))
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

                var businessPages = db.BusinessPages.OrderBy(x => x.PageOrder).ToList();
                if (businessPages != null)
                {
                    footer.BusinessPages = businessPages;
                }
                var eventTypes = db.EventTypes.OrderBy(x => x.EventType1).ToList();
                if (eventTypes != null)
                {
                    footer.EventTypes = eventTypes;
                }
                var cities = db.Cities.OrderBy(x => x.CityName).ToList();
                if (cities != null)
                {
                    footer.Cities = cities;
                }
                return View(footer);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnableDisableCity(int id)
        {
            City City = db.Cities.Find(id);
            City.IsOnFooter = (City.IsOnFooter ? false : true);
            db.SaveChanges();
            return Content("<i class=\"fa fa-eye" + (City.IsOnFooter ? "" : "-slash") + "\"></i>");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnableDisableEventType(int id)
        {
            EventType EventType = db.EventTypes.Find(id);
            EventType.IsOnFooter = (EventType.IsOnFooter ? false : true);
            db.SaveChanges();
            return Content("<i class=\"fa fa-eye" + (EventType.IsOnFooter ? "" : "-slash") + "\"></i>");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnableDisableBusinessPage(int id)
        {
            BusinessPage BusinessPage = db.BusinessPages.Find(id);
            BusinessPage.IsOnFooter = (BusinessPage.IsOnFooter ? false : true);
            db.SaveChanges();
            return Content("<i class=\"fa fa-eye" + (BusinessPage.IsOnFooter ? "" : "-slash") + "\"></i>");
        }
        [HttpPost]
        public ActionResult SortBusinessPage(List<string> order)
        {
            foreach (var item in db.BusinessPages)
            {
                item.PageOrder = order.IndexOf(item.BusinessPageID.ToString());
            }           
            db.SaveChanges();
            return Content("");
        }
    }
}
