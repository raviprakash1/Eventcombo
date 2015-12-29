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
        public ActionResult EventCategory(string msg)
        {
            EventCategory EC = new Models.EventCategory();
            EmsEntities objEntity = new EmsEntities();           
                var modelPerm = (from EventCategory in objEntity.EventCategories 
                                 select EventCategory).OrderBy(x=>x.EventCategory1).ToList();                
                foreach (var item in modelPerm)
                {
                    item.ESubCat = GetSubCategories(item.EventCategoryID);

                } 
                
                if(msg== "SA")
            {
                TempData["SuccessMessage"] = "Event subcategory added successfully";

            }
            if (msg == "SX")
            {
                TempData["SuccessMessage"] = "Event subcategory already exist";

            }
            if (msg == "DS")
            {
                TempData["SuccessMessage"] = "Subcategory deleted successfully";

            }
            if (msg == "DC")
            {
                TempData["SuccessMessage"] = "Category deleted successfully";

            }
            if (msg == "ES")
            {
                TempData["SuccessMessage"] = "Subcategory updated  successfully";

            }
            if (msg == "EC")
            {
                TempData["SuccessMessage"] = "Category updated  successfully";

            }
            if (msg == "CX")
            {
                TempData["SuccessMessage"] = "Category already exist";

            }
            if (msg == "CA")
            {
                TempData["SuccessMessage"] = "Category saved successfully ";

            }
            return View(modelPerm);
            
            
        }
        public string AddCategories(string txtcat)
        {
            string STRRESULT = "";
            bool IsEventCatExists = false;
            EmsEntities objEntity = new EmsEntities();
            EventSubCategory objESC = new EventSubCategory();
            try
            {
                var query1 = (from EventCategory in objEntity.EventCategories
                             where EventCategory.EventCategory1 == txtcat
                             select EventCategory).Any();
                //foreach (EventCategory evntcat in query1)
                //{
                //    IsEventCatExists = true;
                //    STRRESULT = "CX";
                //}
                if (query1 != true)
                {
                    EventCategory ec = new EventCategory();
                    ec.EventCategory1 = txtcat;
                    objEntity.EventCategories.Add(ec);
                    objEntity.SaveChanges();
                    STRRESULT = "CA";
                }
                else
                {
                    STRRESULT = "CX";
                }
            }
            catch (Exception ex)
            {
                STRRESULT = "Err";

            }

            return STRRESULT;

        }
        //[HttpPost]
        //public ActionResult EventCategory(EventCategory ec, EventSubCategory esc,string submit, string txtEventCategory, string txtEventSubCategory, string EventCategoryListBox, string EventSubCategoryListBox)
        //{
        //    EmsEntities objEntity = new EmsEntities();
        //    EventSubCategory objESC = new EventSubCategory();            
        //    switch (submit)
        //    {
        //        case "Add":                                        
        //                bool IsEventCatExists = false;
        //                try
        //                {                            
        //                    var query1 = from EventCategory in objEntity.EventCategories
        //                                where EventCategory.EventCategory1 == txtEventCategory
        //                                select EventCategory;
        //                    foreach (EventCategory evntcat in query1)
        //                    {
        //                        IsEventCatExists = true;
        //                        TempData["SuccessMessage"] = "Event Category Already Exists.";
        //                    }
        //                    if (IsEventCatExists != true)
        //                    {
        //                        ec.EventCategory1 = txtEventCategory;
        //                        objEntity.EventCategories.Add(ec);                             
        //                        objEntity.SaveChanges();
        //                        TempData["SuccessMessage"] = "Event Category Created.";
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    string message = ex.Message;

        //                }                                                                                                            
        //            break;                                    
        //        default:
        //            throw new Exception();
        //    }
        //    var modelPerm = (from EventCategory in objEntity.EventCategories
        //                     orderby EventCategory.EventCategory1
        //                     select EventCategory).ToList();
        //    foreach (var item in modelPerm)
        //    {
        //        item.ESubCat = GetSubCategories(item.EventCategoryID);
        //    }
        //    return View(modelPerm);
        //}
       public List<EventSubCategory> GetSubCategories(long iEvntCtgryId)
        {
            using (EmsEntities objEntity = new EmsEntities())
            {

                var query = (from EventSubCategory in objEntity.EventSubCategories
                              where EventSubCategory.EventCategoryID == iEvntCtgryId
                              select EventSubCategory).OrderBy (x=>x.EventSubCategory1).ToList();            
                return query;
            }            
        }


        public string EditCategories(long iEvntCtgryId,string strNewCatName)
        {
            string strResult = string.Empty;
            try
            {

                using (EmsEntities objEntity = new EmsEntities())
                {
                    var query1 = (from EventCategory in objEntity.EventCategories
                                  where EventCategory.EventCategory1 == strNewCatName
                                  select EventCategory).Any();
                    if (query1 != true)
                    {
                        EventCategory objESC = (from obj in objEntity.EventCategories where obj.EventCategoryID == iEvntCtgryId select obj).FirstOrDefault();
                        if (objESC != null)
                        {
                            objESC.EventCategory1 = strNewCatName;
                            objEntity.SaveChanges();

                        }
                        strResult = "EC";
                    }
                    else
                    {
                        strResult = "CX";
                    }

                }
              

            }
            catch (Exception ex)
            {
                strResult = "Err";
            }

            return strResult;
        }
        public string EditSubCategories(long iEvntCtgryId, long iEvntSubCtgryId, string subcatname)
        {
            string strResult = string.Empty;
            try
            {

                using (EmsEntities objEntity = new EmsEntities())
                {
                    var query1 = (from EventSubCategory in objEntity.EventSubCategories
                                  where EventSubCategory.EventSubCategory1 == subcatname && EventSubCategory.EventCategoryID == iEvntCtgryId
                                  select EventSubCategory).Any();

                    if (query1 != true)
                    {

                        EventSubCategory objESC = (from obj in objEntity.EventSubCategories where obj.EventSubCategoryID == iEvntSubCtgryId select obj).FirstOrDefault();
                        if (objESC != null)
                        {
                            objESC.EventSubCategory1 = subcatname;
                            objEntity.SaveChanges();

                        }
                        strResult = "ES";
                    }
                    else
                    {
                        strResult = "SX";
                    }

                }
                    //EventSubCategory objESC = new EventSubCategory();
                    //objESC.EventCategoryID = iEvntCtgryId;
                    //objESC.EventSubCategory1 = strSubCatName;
                    //objEntity.EventSubCategories.Add(objESC);

                    //objEntity.SaveChanges();
                    //TempData["SuccessMessage"] = "Event Sub Category Created.";
                    
            }
            catch (Exception ex)
            {
                strResult = "Err";
            }

            return strResult;
        }
        public string  AddSubCategories(long iEvntCtgryId,string strSubCatName)
        {
            string strResult = string.Empty;
            try {
                EmsEntities objEntity = new EmsEntities();
                var query1 = (from EventSubCategory in objEntity.EventSubCategories
                              where EventSubCategory.EventSubCategory1 == strSubCatName && EventSubCategory.EventCategoryID== iEvntCtgryId
                              select EventSubCategory).Any();


                if (query1 != true)
                {

                    EventSubCategory objESC = new EventSubCategory();
                    objESC.EventCategoryID = iEvntCtgryId;
                    objESC.EventSubCategory1 = strSubCatName;
                    objEntity.EventSubCategories.Add(objESC);

                    objEntity.SaveChanges();

                    strResult = "SA";
                }
                else
                {
                    strResult = "SX";
                }
            }
            catch(Exception ex)
            {
                strResult = "Err";
            }

            return strResult ;
        }
        public string DeleteSubCategories(long iEvntCtgryId, long iEvntSubCtgryId)
        {
            string strResult = string.Empty;
            try
            {
                EmsEntities objEntity = new EmsEntities();
                objEntity.Database.ExecuteSqlCommand("Delete from EventSubCategory where EventCategoryID='" + iEvntCtgryId + "' and EventSubCategoryID='"+ iEvntSubCtgryId + "'");                
               // TempData["SuccessMessage"] = "Event Sub Category Deleted.";
                strResult = "DS";
            }
            catch (Exception ex)
            {
                strResult = "Err";
            }

            return strResult;
        }
        public string DeleteCategories(long iEvntCtgryId)
        {
            string strResult = string.Empty;
            try
            {
                EmsEntities objEntity = new EmsEntities();
                objEntity.Database.ExecuteSqlCommand("Delete from EventSubCategory where EventCategoryID='" + iEvntCtgryId + "'");
                objEntity.Database.ExecuteSqlCommand("Delete from EventCategory where EventCategoryID='" + iEvntCtgryId + "'");
               // TempData["SuccessMessage"] = "Event Category Deleted.";
                strResult = "DC";
            }
            catch (Exception ex)
            {
                strResult = "Err";
            }

            return strResult;
        }
    }
}