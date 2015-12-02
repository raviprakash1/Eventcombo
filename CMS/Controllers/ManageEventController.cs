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
        EmsEntities db = new EmsEntities();
        // GET: ManageEvent
        [HttpPost]
        public ActionResult Index(string SearchStringEventTitle, string EventType, string ddlEventCategory, string ddlEventSubCategory,string Features, string PageF,string Events, string Tickets)
        {
            List<EventCreation> objlst = GetAllEvents(SearchStringEventTitle, EventType, ddlEventCategory, ddlEventSubCategory, Features, Events,Tickets);
            int iCount = (PageF != null ? Convert.ToInt32(PageF) : 0);
            List<SelectListItem> PageFilter = new List<SelectListItem>();
            int i = 0; int z = 0; int iUcount = objlst.Count; int iGapValue = 25;
            string strText = "";
            PageFilter.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "0",
                Selected = (iCount == 0 ? true : false)
            });
            if (iUcount > iGapValue)
            {
                for (i = 0; i < iUcount; i++)
                {
                    strText = z.ToString() + " - " + (z + iGapValue).ToString();
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = strText,
                        Value = (z + iGapValue).ToString(),
                        Selected = (iCount == z ? true : false)
                    });
                    z = z + iGapValue;
                    iUcount = iUcount - iGapValue;
                    if (iUcount < iGapValue)
                    {
                        strText = z.ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (z + iGapValue).ToString(),
                            Selected = (iCount == z ? true : false)
                        });
                    }
                }
                if (iCount > 0)
                {
                    if (iCount < objlst.Count)
                        objlst = objlst.GetRange(iCount - iGapValue, iGapValue);
                    else
                    {
                        int iGap = (iCount - iGapValue);
                        objlst = objlst.GetRange(iGap, (objlst.Count -iGap) );
                    }
                }
            }
            else
            {
                PageFilter.Add(new SelectListItem()
                {
                    Text = "0 - 25",
                    Value = "25",
                    Selected = (iCount == 25 ? true : false)
                });

            }

            ViewBag.PageF = PageFilter;



           

            return View(objlst);

        }

        


        public ActionResult Index()
        {
            List<EventCreation> objlst = GetAllEvents("", "", "", "", "","","");
            int iCount = 0;
            List<SelectListItem> PageFilter = new List<SelectListItem>();
            int i = 0; int z = 0; int iUcount = objlst.Count; int iGapValue = 25;
            string strText = "";
            PageFilter.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "0",
                Selected = (iCount == 0 ? true : false)
            });
            if (iUcount > iGapValue)
            {
                for (i = 0; i < iUcount; i++)
                {
                    strText = z.ToString() + " - " + (z + iGapValue).ToString();
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = strText,
                        Value = (z + iGapValue).ToString(),
                        Selected = (iCount == z ? true : false)
                    });
                    z = z + iGapValue;
                    iUcount = iUcount - iGapValue;
                    if (iUcount < iGapValue)
                    {
                        strText = z.ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (z + iGapValue).ToString(),
                            Selected = (iCount == z ? true : false)
                        });
                        iUcount = 0;
                    }
                }
            }
            else
            {
                PageFilter.Add(new SelectListItem()
                {
                    Text = "0 - 25",
                    Value = "25",
                    Selected = (iCount == 25 ? true : false)
                });

            }

            ViewBag.PageF = PageFilter;
            return View(objlst);
        }
        public List<EventCreation> GetAllEvents(string SearchStringEventTitle, string iEventType, string iEventCategory, string iEventSubCategory,string strFeature,string Events,string tickets)
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
                List<SelectListItem> TagEvents = new List<SelectListItem>();
                TagEvents.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0",
                    Selected = true
                });
                TagEvents.Add(new SelectListItem()
                {
                    Text = "Upcoming Events",
                    Value = "1",
                    Selected = true
                });
                TagEvents.Add(new SelectListItem()
                {
                    Text = "Expired Events",
                    Value = "2",
                    Selected = true
                });

                List<SelectListItem> Tickets = new List<SelectListItem>();
                Tickets.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0",
                    Selected = true
                });
                Tickets.Add(new SelectListItem()
                {
                    Text = "No Sales",
                    Value = "1",
                    Selected = true
                });
                Tickets.Add(new SelectListItem()
                {
                    Text = "Sold Tickets",
                    Value = "2",
                    Selected = true
                });
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
                ViewBag.Tickets = Tickets;
                ViewBag.Events = TagEvents;


                GetEventListing_Result obj = new GetEventListing_Result();
                List<EventCreation> objEv = new List<EventCreation>();
                objEv = objEntity.GetEventListing(SearchStringEventTitle, iEventType, iEventCategory, iEventSubCategory,strFeature, Events,tickets).ToList();

                


                return objEv;
            }
            //if (!SearchStringFirstName.Equals(string.Empty))
            //    return modelUserTemp.Where(us => us.FirstName.ToLower().Contains(SearchStringFirstName.ToLower()) || us.LastName.ToLower().Contains(SearchStringLastName.ToLower())
            //    || us.EMail.ToLower().Contains(SearchStringEmail.ToLower())).ToList();
            //else





        }
        public ActionResult Deleteevent(string Eventid)
        {
            try
            {

                db.Database.ExecuteSqlCommand("Delete from Event_Orgnizer_Detail where Orgnizer_Event_Id='" + Eventid + "'");
                db.Database.ExecuteSqlCommand("Delete from Event_VariableDesc where Event_Id='" + Eventid + "'");
                db.Database.ExecuteSqlCommand("Delete from Ticket where E_Id='" + Eventid + "'");
                db.Database.ExecuteSqlCommand("Delete from Address where EventId='" + Eventid + "'");
                db.Database.ExecuteSqlCommand("Delete from MultipleEvent where EventID='" + Eventid + "'");
                db.Database.ExecuteSqlCommand("Delete from EventVenue where EventID='" + Eventid + "'");
                db.Database.ExecuteSqlCommand("Delete from Event where EventID='" + Eventid + "'");




                return Content("Deleted");

            }
            catch (Exception ex)
            {
                return Content(ex.Message);

            }

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