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
            EmsEntities objEntity = new EmsEntities();           
                var modelPerm = (from EventCategory in objEntity.EventCategories orderby EventCategory.EventCategory1
                                 select EventCategory).ToList();                
                foreach (var item in modelPerm)
                {
                    item.ESubCat = GetSubCategories(item.EventCategoryID);

                }                
                return View(modelPerm);
            
            
        }

        [HttpPost]
        public ActionResult EventCategory(EventCategory ec, EventSubCategory esc,string submit, string txtEventCategory, string txtEventSubCategory, string EventCategoryListBox, string EventSubCategoryListBox)
        {
            EmsEntities objEntity = new EmsEntities();
            EventSubCategory objESC = new EventSubCategory();            
            switch (submit)
            {
                case "Add":                                        
                        bool IsEventCatExists = false;
                        try
                        {                            
                            var query1 = from EventCategory in objEntity.EventCategories
                                        where EventCategory.EventCategory1 == txtEventCategory
                                        select EventCategory;
                            foreach (EventCategory evntcat in query1)
                            {
                                IsEventCatExists = true;
                                TempData["SuccessMessage"] = "Event Category Already Exists.";
                            }
                            if (IsEventCatExists != true)
                            {
                                ec.EventCategory1 = txtEventCategory;
                                objEntity.EventCategories.Add(ec);                             
                                objEntity.SaveChanges();
                                TempData["SuccessMessage"] = "Event Category Created.";
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;

                        }                                                                                                            
                    break;                                    
                default:
                    throw new Exception();
            }
            var modelPerm = (from EventCategory in objEntity.EventCategories
                             orderby EventCategory.EventCategory1
                             select EventCategory).ToList();
            foreach (var item in modelPerm)
            {
                item.ESubCat = GetSubCategories(item.EventCategoryID);
            }
            return View(modelPerm);
        }
        public List<EventSubCategory> GetSubCategories(long iEvntCtgryId)
        {
            using (EmsEntities objEntity = new EmsEntities())
            {

                var query = (from EventSubCategory in objEntity.EventSubCategories
                              where EventSubCategory.EventCategoryID == iEvntCtgryId
                              select EventSubCategory).ToList();            
                return query;
            }            
        }
        public string  AddSubCategories(long iEvntCtgryId,string strSubCatName)
        {
            string strResult = string.Empty;
            try {
                EmsEntities objEntity = new EmsEntities();
                EventSubCategory objESC = new EventSubCategory();
                objESC.EventCategoryID = iEvntCtgryId;
                objESC.EventSubCategory1 = strSubCatName;
                objEntity.EventSubCategories.Add(objESC);

                objEntity.SaveChanges();
                TempData["SuccessMessage"] = "Event Sub Category Created.";
                strResult="Y";
            }
            catch(Exception ex)
            {
                strResult = "N";
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
                TempData["SuccessMessage"] = "Event Sub Category Deleted.";
                strResult = "Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
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
                TempData["SuccessMessage"] = "Event Category Deleted.";
                strResult = "Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
            }

            return strResult;
        }
    }
}