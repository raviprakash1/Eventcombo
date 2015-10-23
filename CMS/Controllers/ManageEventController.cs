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
        [HttpPost]
        public ActionResult Index(string SearchStringEventTitle, string EventType, string ddlEventCategory, string ddlEventSubCategory,string Features)
        {

            return View(GetAllEvents(SearchStringEventTitle, EventType, ddlEventCategory, ddlEventSubCategory, Features));

        }

        


        public ActionResult Index()
        {

            return View(GetAllEvents("", "", "", "",""));
        }
        public List<EventCreation> GetAllEvents(string SearchStringEventTitle, string iEventType, string iEventCategory, string iEventSubCategory,string strFeature)
        {
            string user = (Session["AppId"] != null ? Session["AppId"].ToString() : string.Empty);

            //if (SearchStringEventTitle == null) SearchStringEventTitle = "";
            //if (SearchStringEventType == null) SearchStringEventType = "";
            //if (SearchStringEventCategory == null) SearchStringEventCategory = "";

            using (EmsEntities objEntity = new EmsEntities())
            {


                 var rows = (from myRow in objEntity.EventTypes
                            select myRow).ToList();
                List<SelectListItem> EventType = new List<SelectListItem>();
                EventType.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0",
                    Selected = true
                });
                foreach (var item in rows)
                {
                    EventType.Add(new SelectListItem()
                    {
                        Text = item.EventType1,
                        Value = item.EventTypeID.ToString(),
                    });
                }


                var EventCat = (from myRow in objEntity.EventCategories
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
                                                            
                List <SelectListItem> Features = new List<SelectListItem>();
                Features.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0",
                    Selected = true
                });
                Features.Add(new SelectListItem()
                {
                    Text = "Platinum",
                    Value = "1",
                    Selected = true
                });
                Features.Add(new SelectListItem()
                {
                    Text = "Gold",
                    Value = "2",
                    Selected = true
                });
                Features.Add(new SelectListItem()
                {
                    Text = "Silver",
                    Value = "3",
                    Selected = true
                });
                Features.Add(new SelectListItem()
                {
                    Text = "Bronze",
                    Value = "4",
                    Selected = true
                });


                ViewBag.EventType = EventType;
                ViewBag.ddlEventCategory = EventCategory;
                ViewBag.Features = Features;



                List<EventCreation> objEv = new List<EventCreation>();
                objEv = objEntity.GetEventListing(SearchStringEventTitle, iEventType, iEventCategory, iEventSubCategory,strFeature).ToList();

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
                    strHtml.Append("<option value=0>Select</option>");
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