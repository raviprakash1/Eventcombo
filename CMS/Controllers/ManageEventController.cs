using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CMS.Models;
using System.Text;

namespace CMS.Controllers
{
    public class ManageEventController : Controller
    {
        // GET: ManageEvent
        public ActionResult Index(string SearchStringEventTitle, string EventType, string ddlEventCategory, string ddlEventSubCategory)
        {

            using (EmsEntities db = new EmsEntities())
            {

                var rows = (from myRow in db.EventTypes
                            select myRow).ToList();
                List<SelectListItem> EventTypes = new List<SelectListItem>();
                EventTypes.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0",
                    Selected = true
                });
                foreach (var item in rows)
                {
                    EventTypes.Add(new SelectListItem()
                    {
                        Text = item.EventType1,
                        Value = item.EventTypeID.ToString(),
                    });
                }


                var EventCat = (from myRow in db.EventCategories
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



                ViewBag.EventType = EventType;
                ViewBag.ddlEventCategory = EventCategory;

            }


            return View(GetAllEvents(SearchStringEventTitle, EventType, ddlEventCategory, ddlEventSubCategory));
        }
        public List<EventCreation> GetAllEvents(string SearchStringEventTitle, string iEventType, string iEventCategory, string iEventSubCategory)
        {
            string user = (Session["AppId"] != null ? Session["AppId"].ToString() : string.Empty);

            //if (SearchStringEventTitle == null) SearchStringEventTitle = "";
            //if (SearchStringEventType == null) SearchStringEventType = "";
            //if (SearchStringEventCategory == null) SearchStringEventCategory = "";

            using (EmsEntities objEntity = new EmsEntities())
            {

                List<EventCreation> objEv = new List<EventCreation>();
                objEv = objEntity.GetEventListing(SearchStringEventTitle, iEventType, iEventCategory, iEventSubCategory).ToList();

                //var modelEvent = (from Ev in objEntity.Events
                //                  join EType in objEntity.EventTypes on Ev.EventTypeID equals EType.EventTypeID
                //                  select new Event
                //                  {
                //                      EventTitle = Ev.EventTitle,
                //                      EventCategoryID = Ev.EventCategoryID,
                //                      EventSubCategoryID = Ev.EventSubCategoryID,
                //                     // EventTypeName = EType.EventType1
                //                  }
                //                    );
                return objEv;
            }
            //if (!SearchStringFirstName.Equals(string.Empty))
            //    return modelUserTemp.Where(us => us.FirstName.ToLower().Contains(SearchStringFirstName.ToLower()) || us.LastName.ToLower().Contains(SearchStringLastName.ToLower())
            //    || us.EMail.ToLower().Contains(SearchStringEmail.ToLower())).ToList();
            //else





        }

        public string UpdateEventFeature(int iFid, long lEventId)
        {
            string strResult = "";
            try
            {
                EmsEntities db = new EmsEntities();
                db.Database.ExecuteSqlCommand("UPDATE Event SET Feature = "  + iFid.ToString() + "  WHERE EventID = " + lEventId.ToString());
                strResult ="Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
            }
            return strResult;
        }




        public string GetSubCat(long lECatId)
        {

            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EmsEntities objEnt = new EmsEntities())
                {
                    var EventCat = (from myRow in objEnt.EventSubCategories
                                    where myRow.EventCategoryID == lECatId
                                    select myRow).ToList();

                    foreach (var item in EventCat)
                        strHtml.Append("<option value=" + item.EventSubCategoryID.ToString() + ">" + item.EventSubCategory1 + "</option>");

                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
                return strHtml.ToString();

            }


        }




    }
}