using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CMS.Models;
using System.Text;
using System.Data.SqlClient;
using PagedList;

namespace CMS.Controllers
{
    public class ManageEventController : Controller
    {
        EmsEntities db = new EmsEntities();
        // GET: ManageEvent
        //[HttpPost]
        //public ActionResult Index(string SearchStringEventTitle, string EventType, string ddlEventCategory, string ddlEventSubCategory, string Features, int PageF, string Events, string Tickets)
        //{
        //    if ((Session["UserID"] == null))
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    List<EventCreation> objlst = GetAllEvents(SearchStringEventTitle, EventType, ddlEventCategory, ddlEventSubCategory, Features, Events, Tickets);
        //    if (objlst.Count == 0)
        //        ViewData["SearchedUser"] = 0;
        //    int iCount = (PageF != null ? Convert.ToInt32(PageF) : 0);
        //    ViewData["Eventscount"] = objlst.Count;
        //    List<SelectListItem> PageFilter = new List<SelectListItem>();
        //    int i = 0; int z = 0; int iUcount = objlst.Count; int iGapValue = 25;
        //    string strText = "";
        //    if (iUcount > iGapValue)
        //    {
        //        for (i = 0; i < iUcount; i++)
        //        {
        //            if (strText != (z + 1).ToString() + " - " + (z + iGapValue).ToString())
        //            {
        //                strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
        //                PageFilter.Add(new SelectListItem()
        //                {
        //                    Text = strText,
        //                    Value = (i).ToString(),
        //                    Selected = (iCount == z ? true : false)
        //                });
        //            }
        //            z = z + iGapValue;
        //            iUcount = iUcount - iGapValue;
        //            if (iUcount < iGapValue && iUcount > 0)
        //            {
        //                strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
        //                PageFilter.Add(new SelectListItem()
        //                {
        //                    Text = strText,
        //                    Value = (i + 1).ToString(),
        //                    Selected = (iCount == z ? true : false)
        //                });
        //            }
        //        }
        //        if (iCount > 0)
        //        {

        //        }
        //    }
        //    else
        //    {
        //        PageFilter.Add(new SelectListItem()
        //        {
        //            Text = "1 - 25",
        //            Value = "0",
        //            Selected = (iCount == 25 ? true : false)
        //        });

        //    }

        //    ViewBag.PageF = PageFilter;

        //    List<EventCreation> objlst1 = GetAllEvents("", "", "", "", "", "", "");

        //    ViewData["Eventscount"] = objlst1.Count;
        //    int pageSize = 25;
        //    PageF = PageF > 0 ? PageF : 1;
        //    pageSize = pageSize > 0 ? pageSize : 25;
        //    var eventlist = objlst.ToPagedList(PageF, pageSize);
        //    return View(eventlist);

        //}


        public ActionResult EventPaymentInfo(long lEventId)
        {
            using (EmsEntities objEntity = new EmsEntities())
            {
                var vPInfo = (from myRow in objEntity.Payment_Info
                              where myRow.PI_EventId == lEventId
                              select myRow).FirstOrDefault();
                Payment_Info objPI = new Payment_Info();
                if (vPInfo != null)
                {
                    objPI = vPInfo;
                    var vCountry = (from myRow in objEntity.Countries where myRow.CountryID == objPI.PI_Country select myRow.Country1).FirstOrDefault();
                    TempData["Country"] = vCountry;
                }
                return View(objPI);
            }

        }
        public ActionResult EventsList(string SearchStringEventTitle, string EventType, string ddlEventCategory, string ddlEventSubCategory, string Features, string Events, string Tickets, int PageF = 0)
        {


            if ((Session["UserID"] != null))
            {
                int page_live = 1;

                int pageSize = 25;
                PageF = PageF > 0 ? PageF : 1;

                pageSize = pageSize > 0 ? pageSize : 50;

                List<EventCreation> objlst = GetAllEvents("", "", "", "", "", "", "");


               
                var users = objlst.ToPagedList(PageF, pageSize);

                ViewData["Userscount"] = objlst.Count();
                if (objlst.Count == 0)
                    ViewData["SearchedUser"] = 0;
              
                List<SelectListItem> PageFilter = new List<SelectListItem>();

                int iCount = 0;
                int i = 0; int z = 0; int iUcount = objlst.Count; int iGapValue = 25;
                string strText = "";
                if (iUcount > iGapValue)
                {
                    for (i = 0; i < iUcount; i++)
                    {
                        strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (i).ToString(),
                            Selected = (iCount == z ? true : false)
                        });
                        z = z + iGapValue;
                        iUcount = iUcount - iGapValue;
                        if (iUcount < iGapValue)
                        {
                            strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                            PageFilter.Add(new SelectListItem()
                            {
                                Text = strText,
                                Value = (i + 1).ToString(),
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
                            //objuser = objuser.GetRange(iCount - iGapValue, ((iCount - (objuser.Count + 1))));
                            int iGap = (iCount - iGapValue);
                            objlst = objlst.GetRange(iGap, (objlst.Count - iGap));
                            //objlst = objlst.GetRange(iGap, (objlst.Count - iGap));
                        }
                    }
                }
                else
                {
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = "1 - 50",
                        Value = "0",
                        Selected = (iCount == 50 ? true : false)
                    });



                }

                TempData["SearchStringEventTitle"] = SearchStringEventTitle;
                TempData["EventType"] = EventType;
                TempData["ddlEventCategory"] = ddlEventCategory;
                TempData["ddlEventSubCategory"] = ddlEventSubCategory;
                TempData["Features"] = Features;
                TempData["Events"] = Events;
                TempData["Tickets"] = Tickets;


                //PageFilter.Add(new SelectListItem()
                //{
                //    Text = "1 - 5",
                //    Value = "5",
                //    Selected = (iCount == 5 ? true : false)
                //});
                //PageFilter.Add(new SelectListItem()
                //{
                //    Text = "5 - 10",
                //    Value = "10",
                //    Selected = (iCount == 10 ? true : false)
                //});

                ViewBag.ddlPageF = PageFilter;
                var userid = Session["UserID"].ToString();
                TempData["Pagesize"] = pageSize;
                TempData["PageNo"] = PageF;


                // List<Permissions> objPerm = GetPermission("APP");
                // UsersTemplate objU = new UsersTemplate();
                //  objU.objPermissions = GetPermission("APP");
                // objuser.Add(objU);
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }
        }
        public ActionResult Index(int ? page)
        {
            if ((Session["UserID"] == null))
            {
                return RedirectToAction("Login", "Home");
            }
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<EventCreation> objlst = GetAllEvents("", "", "", "", "", "", "");
           

          
           
            var eventlist = objlst.ToPagedList(pageIndex, pageSize);

            int iCount = 0;
            List<SelectListItem> PageFilter = new List<SelectListItem>();
            ViewData["Eventscount"] = objlst.Count;
            int i = 0; int z = 0; int iUcount = objlst.Count; int iGapValue = 25;
            string strText = "";
            if (iUcount > iGapValue)
            {
                for (i = 0; i < iUcount; i++)
                {
                    strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = strText,
                        Value = (i).ToString(),
                        Selected = (iCount == z ? true : false)
                    });
                    z = z + iGapValue;
                    iUcount = iUcount - iGapValue;
                    if (iUcount > 0)
                    {
                        if (iUcount < iGapValue)
                        {
                            strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                            PageFilter.Add(new SelectListItem()
                            {
                                Text = strText,
                                Value = (i + 1).ToString(),
                                Selected = (iCount == z ? true : false)
                            });
                            iUcount = 0;
                        }
                    }
                }
            }
            else
            {
                PageFilter.Add(new SelectListItem()
                {
                    Text = "0 - 25",
                    Value = "0",
                    Selected = (iCount == 25 ? true : false)
                });

            }

            ViewBag.ddlPageF = PageFilter;
           
            ViewBag.OnePageOfProducts = eventlist;
            return View(eventlist);
        }
        public List<EventCreation> GetAllEvents(string SearchStringEventTitle, string iEventType, string iEventCategory, string iEventSubCategory, string strFeature, string Events, string tickets)
        {
            string user = (Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);


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
                List<SelectListItem> Features = new List<SelectListItem>();
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
                objEv = objEntity.GetEventListing(SearchStringEventTitle, iEventType, iEventCategory, iEventSubCategory, strFeature, Events, tickets).ToList();
                //objEv = objEv.OrderBy(m => m.EventTiming);



                return objEv;
            }

        }
        public ActionResult Deleteevent(string Eventid)
        {
            try
            {
                string sql = "";
                List<Object> sqlParamsList = new List<object>();
                var eventID = new SqlParameter("@EventID", Eventid);
                 sql = @"delete from  Event_Orgnizer_Detail  where Orgnizer_Event_Id={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                 sql = @"delete from  Event_VariableDesc  where Event_Id={0}";
                 sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                sql = @"delete from  Ticket_Quantity_Detail  where TQD_Event_Id={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                sql = @"delete from  Ticket  where E_Id={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                 sql = @"delete from  Address  where EventId={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                 sql = @"delete from  MultipleEvent  where EventID={0}";
                 sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                  sql = @"delete from  EventVenue  where EventID={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                sql = @"delete from  Event_OrganizerMessages  where EventId={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                sql = @"delete from  EventFavourite  where eventId={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                sql = @"delete from  EventImage  where EventID={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                sql = @"delete from  EventVote  where eventId={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                sql = @"delete from  Publish_Event_Detail  where PE_Event_Id={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
               
                sql = @"delete from  Event  where EventID={0}";
                sqlParamsList = new List<object>();
                sqlParamsList.Add(Eventid);
                db.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());

               

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
                var eventID = new SqlParameter("@EventID", lEventId);
                var featureID = new SqlParameter("@FeatureID", iFid);
                db.Database.ExecuteSqlCommand("UPDATE Event SET FeatureUpdateDate=getdate(), Feature = @FeatureID  WHERE EventID = @EventID", featureID, eventID);
                strResult = "Y";
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