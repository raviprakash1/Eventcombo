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
    [CustomAuthorization("4")]
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
        public ActionResult EventsList(string SearchStringEventTitle, string EventType, string ddlEventCategory, string ddlEventSubCategory, string Features, string Events, string Tickets,string sortorder, int PageF = 0)
        {


            if ((Session["UserID"] != null))
            {
                int page_live = 1;

                int pageSize = 25;
                PageF = PageF > 0 ? PageF : 1;

                pageSize = pageSize > 0 ? pageSize :25;

                EventType = EventType != null ? EventType : "0";
                ddlEventSubCategory = ddlEventSubCategory != null ? ddlEventSubCategory : "0";
                ddlEventCategory = ddlEventCategory != null ? ddlEventCategory : "0";
                Features = Features != null ? Features : "0";
                Events = Events != null ? Events : "1";
                Tickets = Tickets != null ? Tickets : "0";
                var objlst = GetAllEvents(SearchStringEventTitle, EventType, ddlEventCategory, ddlEventSubCategory, Features, Events, Tickets,"M");
                switch (sortorder)
                {
                    case "sno":
                        objlst = objlst.OrderBy(s => s.Sno).ToList();
                        break;
                    case "snodesc":
                        objlst = objlst.OrderByDescending(s => s.Sno).ToList();
                        break;
                    case "title":
                        objlst = objlst.OrderBy(s => s.EventTitle).ToList();
                        break;
                    case "titledesc":
                        objlst = objlst.OrderByDescending(s => s.EventTitle).ToList();
                        break;
                    case "type":
                        objlst = objlst.OrderBy(s => s.EventType).ToList();
                        break;
                    case "typedesc":
                        objlst = objlst.OrderByDescending(s => s.EventType).ToList();
                        break;
                    case "category":
                        objlst = objlst.OrderBy(s => s.EventCategory).ToList();
                        break;
                    case "categorydesc":
                        objlst = objlst.OrderByDescending(s => s.EventCategory).ToList();
                        break;
                    case "date":
                        objlst = objlst.OrderBy(s => s.StartingFrom).ToList();
                        break;
                    case "datedesc":
                        objlst = objlst.OrderByDescending(s => s.StartingFrom).ToList();
                        break;
                    case "Organiser":
                        objlst = objlst.OrderBy(s => s.Orgnizer_Name).ToList();
                        break;
                    case "Organiserdesc":
                        objlst = objlst.OrderByDescending(s => s.Orgnizer_Name).ToList();
                        break;
                    case "venue":
                        objlst = objlst.OrderBy(s => s.EventAddress).ToList();
                        break;
                    case "venuedesc":
                        objlst = objlst.OrderByDescending(s => s.EventAddress).ToList();
                        break;
                    case "ticket":
                        objlst = objlst.OrderBy(s => s.Purchasedqty).ToList();
                        break;
                    case "ticketdesc":
                        objlst = objlst.OrderByDescending(s => s.Purchasedqty).ToList();
                        break;
                    case "feature":
                        objlst = objlst.OrderBy(s => s.Feature).ToList();
                        break;
                    case "featuredesc":
                        objlst = objlst.OrderByDescending(s => s.Feature).ToList();
                        break;
                    default:
                        //objlst = objlst.OrderByDescending(s => s.E_Startdate).ToList(); 
                        break;
                }

               

                ViewData["Eventscount"] = objlst.Count();
                if (objlst.Count == 0)
                    ViewData["SearchedUser"] = 0;
              
                List<SelectListItem> PageFilter = new List<SelectListItem>();

                int iCount = 0;
                int i = 0; int z = 0; int iUcount = objlst.Count; int iGapValue = 25;
                string strText = "";
                if (iUcount > iGapValue)
                {
                    double dRowCnt = iUcount / iGapValue;
                    if ((dRowCnt - (int)dRowCnt) != 0) dRowCnt = (int)dRowCnt + 1;
                    for (i = 0; i < dRowCnt; i++)
                    {
                        strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (i).ToString(),
                            Selected = (PageF == i ? true : false)
                        });
                        z = z + iGapValue;
                        iUcount = iUcount - iGapValue;

                        //strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        //PageFilter.Add(new SelectListItem()
                        //{
                        //    Text = strText,
                        //    Value = (i).ToString(),
                        //    Selected = (iCount == z ? true : false)
                        //});
                        //z = z + iGapValue;
                        //iUcount = iUcount - iGapValue;
                        //if (iUcount < iGapValue)
                        //{
                        //    strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        //    PageFilter.Add(new SelectListItem()
                        //    {
                        //        Text = strText,
                        //        Value = (i + 1).ToString(),
                        //        Selected = (iCount == z ? true : false)
                        //    });
                        //}
                    }
                    if (iUcount > 0)
                    {
                        if (iUcount < iGapValue)
                        {
                            strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                            PageFilter.Add(new SelectListItem()
                            {
                                Text = strText,
                                Value = (i).ToString(),
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
                            objlst = objlst.GetRange(iGap, (objlst.Count - iGap));
                           
                        }
                    }
                }
                else
                {
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = "1 - 25",
                        Value = "0",
                        Selected = (iCount == 50 ? true : false)
                    });



                }


                PageFilter.Insert(0, new SelectListItem()
                {
                    Text = "Select",
                    Value = "S",
                    //Selected = (iCount == 50 ? true : false)
                });
                var users = objlst.ToPagedList(PageF, pageSize);
                TempData["SearchStringEventTitle"] = SearchStringEventTitle;
                TempData["EventType"] = EventType;
                TempData["ddlEventCategory"] = ddlEventCategory;
                TempData["ddlEventSubCategory"] = ddlEventSubCategory;
                TempData["Features"] = Features;
                TempData["Events"] = Events;
                TempData["Tickets"] = Tickets;
                TempData["sortorder"] = sortorder;


             

                ViewBag.ddlPageF = PageFilter;
                var userid = Session["UserID"].ToString();
                TempData["Pagesize"] = pageSize;
                TempData["PageNo"] = PageF;


            
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }
        }
        public ActionResult ExpiredticketList(string SearchStringEventTitle, string EventType, string ddlEventCategory, string ddlEventSubCategory, string Features, string Events, string Tickets, string sortorder, int PageF = 0)
        {


            if ((Session["UserID"] != null))
            {
                int page_live = 1;

                int pageSize = 25;
                PageF = PageF > 0 ? PageF : 1;

                pageSize = pageSize > 0 ? pageSize : 25;

                EventType = EventType != null ? EventType : "0";
                ddlEventSubCategory = ddlEventSubCategory != null ? ddlEventSubCategory : "0";
                ddlEventCategory = ddlEventCategory != null ? ddlEventCategory : "0";
                Features = Features != null ? Features : "0";
                Events = Events != null ? Events : "2";
               var  TTickets = Tickets != null ? Tickets : "2";
                var objlst = GetAllEvents(SearchStringEventTitle, EventType, ddlEventCategory, ddlEventSubCategory, Features, Events, TTickets, "E");

                switch (sortorder)
                {
                    case "sno":
                        objlst = objlst.OrderBy(s => s.Sno).ToList();
                        break;
                    case "snodesc":
                        objlst = objlst.OrderByDescending(s => s.Sno).ToList();
                        break;
                    case "title":
                        objlst = objlst.OrderBy(s => s.EventTitle).ToList();
                        break;
                    case "titledesc":
                        objlst = objlst.OrderByDescending(s => s.EventTitle).ToList();
                        break;
                    case "type":
                        objlst = objlst.OrderBy(s => s.EventType).ToList();
                        break;
                    case "typedesc":
                        objlst = objlst.OrderByDescending(s => s.EventType).ToList();
                        break;
                    case "category":
                        objlst = objlst.OrderBy(s => s.EventCategory).ToList();
                        break;
                    case "categorydesc":
                        objlst = objlst.OrderByDescending(s => s.EventCategory).ToList();
                        break;
                    case "date":
                        objlst = objlst.OrderBy(s => s.StartingFrom).ToList();
                        break;
                    case "datedesc":
                        objlst = objlst.OrderByDescending(s => s.StartingFrom).ToList();
                        break;
                    case "Organiser":
                        objlst = objlst.OrderBy(s => s.Orgnizer_Name).ToList();
                        break;
                    case "Organiserdesc":
                        objlst = objlst.OrderByDescending(s => s.Orgnizer_Name).ToList();
                        break;
                    case "venue":
                        objlst = objlst.OrderBy(s => s.EventAddress).ToList();
                        break;
                    case "venuedesc":
                        objlst = objlst.OrderByDescending(s => s.EventAddress).ToList();
                        break;
                    case "ticket":
                        objlst = objlst.OrderBy(s => s.Purchasedqty).ToList();
                        break;
                    case "ticketdesc":
                        objlst = objlst.OrderByDescending(s => s.Purchasedqty).ToList();
                        break;
                    case "feature":
                        objlst = objlst.OrderBy(s => s.Feature).ToList();
                        break;
                    case "featuredesc":
                        objlst = objlst.OrderByDescending(s => s.Feature).ToList();
                        break;
                    default:
                        objlst = objlst.OrderBy(s => s.Sno).ToList();
                        break;
                }

                var users = objlst.ToPagedList(PageF, pageSize);

                ViewData["Eventscount"] = objlst.Count();
                if (objlst.Count == 0)
                    ViewData["SearchedUser"] = 0;

                List<SelectListItem> PageFilter = new List<SelectListItem>();

                int iCount = 0;
                int i = 0; int z = 0; int iUcount = objlst.Count; int iGapValue = 25;
                string strText = "";
                if (iUcount > iGapValue)
                {
                    double dRowCnt = iUcount / iGapValue;
                    if ((dRowCnt - (int)dRowCnt) != 0) dRowCnt = (int)dRowCnt + 1;
                    for (i = 0; i < dRowCnt; i++)
                    {
                        strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (i).ToString(),
                            Selected = (PageF == i ? true : false)
                        });
                        z = z + iGapValue;
                        iUcount = iUcount - iGapValue;

                        //strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        //PageFilter.Add(new SelectListItem()
                        //{
                        //    Text = strText,
                        //    Value = (i).ToString(),
                        //    Selected = (iCount == z ? true : false)
                        //});
                        //z = z + iGapValue;
                        //iUcount = iUcount - iGapValue;
                        //if (iUcount < iGapValue)
                        //{
                        //    strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        //    PageFilter.Add(new SelectListItem()
                        //    {
                        //        Text = strText,
                        //        Value = (i + 1).ToString(),
                        //        Selected = (iCount == z ? true : false)
                        //    });
                        //}
                    }
                    if (iUcount > 0)
                    {
                        if (iUcount < iGapValue)
                        {
                            strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                            PageFilter.Add(new SelectListItem()
                            {
                                Text = strText,
                                Value = (i).ToString(),
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
                            objlst = objlst.GetRange(iGap, (objlst.Count - iGap));

                        }
                    }
                }
                else
                {
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = "1 - 25",
                        Value = "0",
                        Selected = (iCount == 50 ? true : false)
                    });



                }

                PageFilter.Insert(0, new SelectListItem()
                {
                    Text = "Select",
                    Value = "S",
                    //Selected = (iCount == 50 ? true : false)
                });

                TempData["SearchStringEventTitle"] = SearchStringEventTitle;
                TempData["EventType"] = EventType;
                TempData["ddlEventCategory"] = ddlEventCategory;
                TempData["ddlEventSubCategory"] = ddlEventSubCategory;
                TempData["Features"] = Features;
             
                TempData["Events"] = Events;
                TempData["Tickets"] = TTickets;
                TempData["sortorder"] = sortorder;




                ViewBag.ddlPageF = PageFilter;
                var userid = Session["UserID"].ToString();
                TempData["Pagesize"] = pageSize;
                TempData["PageNo"] = PageF;



                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }
        }

        public List<V_EventsList> GetAllEvents(string SearchStringEventTitle, string iEventType, string iEventCategory, string iEventSubCategory, string strFeature)
        {
            string user = (Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
            V_EventsList obj = new V_EventsList();
            List<V_EventsList> objEv = new List<V_EventsList>();
            try
            {
                using (EmsEntities objEntity = new EmsEntities())
                {
                    var rows = (from myRow in objEntity.EventTypes
                                orderby myRow.EventType1
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
                    List<SelectListItem> TagEvents = new List<SelectListItem>();
                    TagEvents.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0"
                        
                    });
                    TagEvents.Add(new SelectListItem()
                    {
                        Text = "Upcoming Events",
                        Value = "1"
                    });
                    TagEvents.Add(new SelectListItem()
                    {
                        Text = "Expired Events",
                        Value = "2"
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
                    string strsql = "";



                    if (!string.IsNullOrWhiteSpace(SearchStringEventTitle))
                    {
                        var str = "%" + SearchStringEventTitle + "%";
                        strsql += " And ( rtrim(ltrim(EV.EventTitle)) like rtrim(ltrim('" + str + "')) OR rtrim(ltrim(EV.EventID)) like rtrim(ltrim('" + str + "')))";

                    }
                    if (Convert.ToInt32(iEventType) > 0)
                    {
                        strsql += " And EV.EventTypeID =" + iEventType + "";
                    }
                    if (Convert.ToInt32(iEventCategory) > 0)
                    {
                        strsql += " And EV.EventCategoryID =" + iEventCategory + "";
                    }
                    if (Convert.ToInt32(iEventSubCategory) > 0)
                    {
                        strsql += " And EV.EventSubCategoryID =" + iEventSubCategory + "";
                    }
                    if (Convert.ToInt32(strFeature) > 0)
                    {
                        strsql += " And EV.Feature =" + strFeature + "";
                    }
                 
                    List<Object> sqlParamsList = new List<object>();
                    var eventID = new SqlParameter("@EventID", strsql);
                    string sql = "";

                    if (!string.IsNullOrEmpty(strsql))
                    {
                        objEv = db.V_EventsList.SqlQuery("Select * from V_EventsList EV where  EV.E_Enddate < GETUTCDATE() and ev.TicketDetail<=0 and 1=1  " + strsql + " ").ToList<V_EventsList>();
                    }
                    else
                    {
                        objEv = db.V_EventsList.SqlQuery("Select * from V_EventsList EV where EV.E_Enddate < GETUTCDATE() and ev.TicketDetail<=0 ").ToList<V_EventsList>();
                    }

                }

            }
            catch (Exception ex)
            {

            }

            // objEv = objEntity.GetEventListing(SearchStringEventTitle, iEventType, iEventCategory, iEventSubCategory, strFeature, Events, tickets).ToList();
            //objEv = objEv.OrderBy(m => m.EventTiming);


            return objEv;

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
            List<EventList> objlst = GetAllEvents("", "", "", "", "", "", "","M");
           

          
           
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
        public List<EventList> GetAllEvents(string SearchStringEventTitle, string iEventType, string iEventCategory, string iEventSubCategory, string strFeature, string Events, string tickets ,string type)
        {
            string user = (Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
            EventList obj = new EventList();
            List<V_EventsList> objEv = new List<V_EventsList>();
            List<V_EventsexpiredList> objEv2 = new List<V_EventsexpiredList>();
            List<EventList> objEv3 = new List<EventList>();
            try {
                using (EmsEntities objEntity = new EmsEntities())
                {
                    var rows = (from myRow in objEntity.EventTypes
                                orderby myRow.EventType1
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
                    List<SelectListItem> TagEvents = new List<SelectListItem>();
                    TagEvents.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0"
                        
                    });
                    if (type == "M")
                    {
                        TagEvents.Add(new SelectListItem()
                        {
                            Text = "Upcoming Events",
                            Value = "1",
                            Selected = true

                        });
                    }
                    else
                    {
                        TagEvents.Add(new SelectListItem()
                        {
                            Text = "Upcoming Events",
                            Value = "1"
                        });
                    }
                    if (type == "E")
                    {
                        TagEvents.Add(new SelectListItem()
                        {
                            Text = "Expired Events",
                            Value = "2",
                            Selected = true
                        });
                    }
                    else
                    {
                        TagEvents.Add(new SelectListItem()
                        {
                            Text = "Expired Events",
                            Value = "2"
                           
                        });

                    }

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
                    string strsql = "";


                   
                    if (!string.IsNullOrWhiteSpace(SearchStringEventTitle))
                    {
                        var str = "%" + SearchStringEventTitle + "%";
                        strsql += " And ( rtrim(ltrim(EV.EventTitle)) like rtrim(ltrim('" + str + "')) OR rtrim(ltrim(EV.EventID)) like rtrim(ltrim('" + str + "')))";

                    }
                    if (Convert.ToInt32(iEventType) > 0)
                    {
                        strsql += " And EV.EventTypeID =" + iEventType + "";
                    }
                    if (Convert.ToInt32(iEventCategory) > 0)
                    {
                        strsql += " And EV.EventCategoryID =" + iEventCategory + "";
                    }
                    if (Convert.ToInt32(iEventSubCategory) > 0)
                    {
                        strsql += " And EV.EventSubCategoryID =" + iEventSubCategory + "";
                    }
                    if (Convert.ToInt32(strFeature) > 0)
                    {
                        strsql += " And EV.Feature =" + strFeature + "";
                    }
                    if (Convert.ToInt32(Events) > 0)
                    {
                        if (Convert.ToInt32(Events) == 1)
                        {
                            type = "M";
                        }
                        if (Convert.ToInt32(Events) == 2)
                        {
                            type = "E";
                        }
                    }
                    if (Convert.ToInt32(tickets) > 0)
                    {
                        if (Convert.ToInt32(tickets) == 1)
                        {
                            strsql += " And EV.Purchasedqty <= 0";
                        }
                        if (Convert.ToInt32(tickets) == 2)
                        {
                            strsql += " And EV.Purchasedqty > 0 ";
                        }
                    }
                        List<Object> sqlParamsList = new List<object>();
                        var eventID = new SqlParameter("@EventID", strsql);
                       
                    if (type == "M")
                    {
                        if (Convert.ToInt32(Events) > 0)
                        {
                            if (!string.IsNullOrEmpty(strsql))
                            {
                                objEv = db.V_EventsList.SqlQuery("Select * from V_EventsListUpcoming EV where 1=1  " + strsql + " ").ToList<V_EventsList>();
                            }
                            else
                            {
                                objEv = db.V_EventsList.SqlQuery("Select * from V_EventsListUpcoming ev").ToList<V_EventsList>();
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(strsql))
                            {
                                objEv = db.V_EventsList.SqlQuery("Select * from V_EventsList EV where 1=1  " + strsql + " ").ToList<V_EventsList>();
                            }
                            else
                            {
                                objEv = db.V_EventsList.SqlQuery("Select *  from V_EventsList ev").ToList<V_EventsList>();
                            }
                        }
                    }
                    else 
                    {
                        if (!string.IsNullOrEmpty(strsql))
                        {
                            objEv2 = db.V_EventsexpiredList.SqlQuery("Select * from [V_EventsexpiredList] EV where 1=1  " + strsql + " ").ToList<V_EventsexpiredList>();
                        }
                        else
                        {
                            objEv2 = db.V_EventsexpiredList.SqlQuery("Select * from [V_EventsexpiredList]").ToList<V_EventsexpiredList>();
                        }
                    }

                    }

                if (objEv.Count > 0)
                {
                    objEv3 = (from x in objEv
                              select new EventList
                              {
                                  Sno = x.Sno,
                                  EventTitle = x.EventTitle,
                                  UserID = x.UserID,
                                  EventID = x.EventID,
                                  EventCategoryID = x.EventCategoryID,
                                  EventCategory = x.EventCategory,
                                  EventTypeID = x.EventTypeID,
                                  EventType = x.EventType,
                                  EventSubCategoryID = x.EventSubCategoryID,
                                  EventSubCategory = x.EventSubCategory,
                                  StartingFrom = x.StartingFrom,
                                  EventStartTime = x.EventStartTime,
                                  EventEndDate = x.EventEndDate,
                                  EventEndTime = x.EventEndTime,
                                  E_Enddate = x.E_Enddate,
                                  E_Startdate = x.E_Startdate,
                                  Orgnizer_Name = x.Orgnizer_Name,
                                  Email = x.Email,
                                  Feature = x.Feature,
                                  EventAddress = x.EventAddress,
                                  VenueName = x.VenueName,
                                  TicketDetail = x.TicketDetail,
                                  Purchasedqty = x.Purchasedqty,
                                  EventTiming = x.EventTiming

                              }).ToList();
                }
                else
                {
                    objEv3 = (from x in objEv2
                              select new EventList
                              {
                                  Sno = x.Sno,
                                  EventTitle = x.EventTitle,
                                  UserID = x.UserID,
                                  EventID = x.EventID,
                                  EventCategoryID = x.EventCategoryID,
                                  EventCategory = x.EventCategory,
                                  EventTypeID = x.EventTypeID,
                                  EventType = x.EventType,
                                  EventSubCategoryID = x.EventSubCategoryID,
                                  EventSubCategory = x.EventSubCategory,
                                  StartingFrom = x.StartingFrom,
                                  EventStartTime = x.EventStartTime,
                                  EventEndDate = x.EventEndDate,
                                  EventEndTime = x.EventEndTime,
                                  E_Enddate = x.E_Enddate,
                                  E_Startdate = x.E_Startdate,
                                  Orgnizer_Name = x.Orgnizer_Name,
                                  Email = x.Email,
                                  Feature = x.Feature,
                                  EventAddress = x.EventAddress,
                                  VenueName = x.VenueName,
                                  TicketDetail = x.TicketDetail,
                                  Purchasedqty = x.Purchasedqty,
                                  EventTiming = x.EventTiming

                              }).ToList();
                }
                
            }catch(Exception ex)
            {

            }

            // objEv = objEntity.GetEventListing(SearchStringEventTitle, iEventType, iEventCategory, iEventSubCategory, strFeature, Events, tickets).ToList();
            //objEv = objEv.OrderBy(m => m.EventTiming);


            return objEv3;

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
                                    orderby myRow .EventSubCategory1
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