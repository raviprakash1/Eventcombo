using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Controllers
{
    public class BusinessPagesController : Controller
    {
        private EventComboEntities db = new EventComboEntities();

        // GET: BusinessPages
        public ActionResult BusinessPage(string PageNameUrl) {
            var businessPage = db.BusinessPages.FirstOrDefault(x => x.PageNameUrl == PageNameUrl);
            if (businessPage == null)
                throw new HttpException(404, "Page not found");
            return View(businessPage);
        }
    }
}
