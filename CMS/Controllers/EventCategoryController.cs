using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class EventCategoryController : Controller
    {
        // GET: EventCategory
        public ActionResult EventCategory()
        {
            EventCategory EC = new Models.EventCategory();
            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelPerm = (from EventCategory in objEntity.EventCategories
                                 select EventCategory).ToList();

                List<string> EventCategoryList = new List<string>();
                foreach (var item in modelPerm)
                {
                    EventCategoryList.Add(item.EventCategory1);
                }
                ViewBag.EventCategory = EventCategoryList;
            }
            return View();
        }

        [HttpPost]
        public ActionResult EventCategory(EventCategory ec, string hdESCat, string submit, string EventCategory1,string EventCategoryListBox)
        {
            EventSubCategory objESC = new EventSubCategory();
            switch (submit)
            {
                case "Create":
                    using (EmsEntities objEntity = new EmsEntities())
                    {

                        bool IsEventCatExists = false;
                        try
                        {
                            string[] strAry = hdESCat.Split(',');

                            var query = from EventCategory in objEntity.EventCategories
                                        where EventCategory.EventCategory1 == ec.EventCategory1
                                        select EventCategory;

                            foreach (EventCategory evntcat in query)
                            {
                                IsEventCatExists = true;
                                TempData["SuccessMessage"] = "Event Category Already Exists.";

                            }
                            if (IsEventCatExists != true)
                            {
                                objEntity.EventCategories.Add(ec);
                                foreach (string str in strAry)
                                {
                                    objESC = new EventSubCategory();
                                    objESC.EventCategoryID = ec.EventCategoryID;
                                    objESC.EventSubCategory1 = str;
                                    objEntity.EventSubCategories.Add(objESC);
                                }
                                objEntity.SaveChanges();
                                TempData["SuccessMessage"] = "Event Category Created.";
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;

                        }
                        var modelPerm = (from EventCategory in objEntity.EventCategories
                                         select EventCategory).ToList();

                        List<string> EventCategoryList = new List<string>();
                        foreach (var item in modelPerm)
                        {
                            EventCategoryList.Add(item.EventCategory1);
                        }
                        ViewBag.EventCategory = EventCategoryList;
                    }
                    break;
                case "Edit":
                    using (EmsEntities objEntity = new EmsEntities())
                    {
                        var query = from EventCategory in objEntity.EventCategories
                                    where EventCategory.EventCategory1 == EventCategoryListBox.Trim()
                                    select EventCategory;

                        foreach (EventCategory evntcat in query)
                        {
                            evntcat.EventCategory1 = ec.EventCategory1;
                        }

                        try
                        {
                            objEntity.SaveChanges();
                            TempData["SuccessMessage"] = "Event Category Edited.";
                        }

                        catch (Exception ex)
                        {
                            string message = ex.Message;

                        }
                        var modelPerm = (from EventCategory in objEntity.EventCategories
                                         select EventCategory).ToList();

                        List<string> EventCatList = new List<string>();
                        foreach (var item in modelPerm)
                        {
                            EventCatList.Add(item.EventCategory1);
                        }
                        ViewBag.EventCategory = EventCatList;                        
                    }
                    break;
                default:
                    throw new Exception();
            }
            return View(ec);
        }
        public string GetSubCategories(string EvntCtgry)
        {
            string returnval = string.Empty;
            EventCategory objEC = new EventCategory();
            using (EmsEntities objEntity = new EmsEntities())
            {
                var query1 = from EventCategory in objEntity.EventCategories
                             where EventCategory.EventCategory1 == EvntCtgry
                             select EventCategory;
                List<string> EventSubCategoryList = new List<string>();
                foreach (var item in query1)
                {
                    var query2 = from EventSubCategory in objEntity.EventSubCategories
                                 where EventSubCategory.EventCategoryID == item.EventCategoryID
                                 select EventSubCategory;
                    foreach (var item1 in query2)
                    {
                        EventSubCategoryList.Add(item1.EventSubCategory1);

                        returnval = item1.EventSubCategory1 + "," + returnval;
                    }
                }
                ViewBag.EventSubCategory = EventSubCategoryList;
            }
            return returnval;
        }    
    }
}