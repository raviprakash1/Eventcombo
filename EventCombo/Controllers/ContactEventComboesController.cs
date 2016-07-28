using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Text;
using NLog;
using EventCombo.Utils;
namespace EventCombo.Controllers
{
    public class ContactEventComboesController : Controller
    {
        private EventComboEntities db = new EventComboEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {

            var EventCat = (from myRow in db.EventCategories
                            orderby myRow.EventCategory1
                            select myRow).ToList();
            List<SelectListItem> EventCategory = new List<SelectListItem>();
            EventCategory.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "0",
                Selected = true
            });
            foreach (var item in EventCat)
            {
                EventCategory.Add(new SelectListItem()
                {
                    Text = item.EventCategory1,
                    Value = item.EventCategoryID.ToString(),
                });
            }
            ViewBag.ddlEventCategory = EventCategory;
            return View(db.ContactEventComboes.ToList());
        }


        public string GetSubCat(long lECatId)
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var EventCat = (from myRow in objEnt.EventSubCategories
                                    where myRow.EventCategoryID == lECatId
                                    orderby myRow.EventSubCategory1
                                    select myRow).ToList();

                    strHtml.Append("<option value=0>Select</option>");
                    foreach (var item in EventCat)
                        strHtml.Append("<option value=" + item.EventSubCategoryID.ToString() + ">" + item.EventSubCategory1 + "</option>");

                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception during request processing", null);
                return strHtml.ToString();
            }
        }
  
        public string SaveAndSend(ContactEventCombo model)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                ContactEventCombo msg = new ContactEventCombo();
                msg.Email = model.Email;
                msg.Name = model.Name;
                msg.Question = model.Question;
                msg.PhoneNo = model.PhoneNo;
                msg.Category = model.Category;
                msg.SubCategory = model.SubCategory;
                db.ContactEventComboes.Add(msg);
                try
                {
                    int i = db.SaveChanges();
                    Email.SendToEventCombo(msg.ContactEventComboId);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception during request processing", null);
                }
                return "saved";
            }
        }
    }
}
