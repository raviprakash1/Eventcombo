using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.Configuration;
using CMS.Service;
using CMS.DAL;

namespace CMS.Controllers
{
    [CustomAuthorization("14")]
    public class BusinessPagesController : Controller
    {
        private EmsEntities db = new EmsEntities();
        IECImageService _iservice;
        public BusinessPagesController()
        {
            var factory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory(new EmsEntities()));
            var mapper = AutomapperConfig.Config.CreateMapper();
            _iservice = new ECImageService(factory, mapper, new ECImageStorage(mapper));
        }

        public ActionResult Index()
        {
            if ((Session["UserID"] != null))
            {
                ViewData["EventComboClientDomain"] = ConfigurationManager.AppSettings["EventComboClientDomain"]+ "/ec";
                var model = db.BusinessPages.AsEnumerable().Select((element, index) => new BusinessPage
                {
                    RowNumber = index + 1,
                    BusinessPageID=element.BusinessPageID,
                    PageName=element.PageName,
                    PageNameUrl=element.PageNameUrl,
                    PageContent=element.PageContent,
                    CreatedDate=element.CreatedDate,
                    UpdateDate=element.UpdateDate
                }).ToList();
                return View(model);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Details(long? id)
        {
            if ((Session["UserID"] != null))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BusinessPage businessPage = db.BusinessPages.Find(id);
                if (businessPage == null)
                {
                    return HttpNotFound();
                }
                return View(businessPage);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Create()
        {
            if ((Session["UserID"] != null))
            {
                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageName,PageNameUrl,PageContent")] BusinessPage businessPage)
        {
            if ((Session["UserID"] != null))
            {
                if (ModelState.IsValid)
                {
                    if (!db.BusinessPages.Any(x => x.PageNameUrl == businessPage.PageNameUrl))
                    {
                        businessPage.CreatedDate = DateTime.Now;
                        businessPage.UpdateDate = DateTime.Now;
                        db.BusinessPages.Add(businessPage);
                        foreach (var file in Request.Files)
                        {
                            HttpPostedFileBase fileBase = Request.Files[file.ToString()];
                            if (fileBase.ContentLength != 0)
                            {
                                ECImageViewModel eCImageViewModel = _iservice.SaveToDB(fileBase, Session["UserID"].ToString());
                                BusinessPageECImage businessPageECImage = new BusinessPageECImage();
                                businessPageECImage.ECImageId = eCImageViewModel.ECImageId;
                                businessPageECImage.BusinessPageId = businessPage.BusinessPageID;
                                db.BusinessPageECImages.Add(businessPageECImage);
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else {
                        ModelState.AddModelError("PageNameUrl", "PageNameUrl Already Exist.");
                        return View(businessPage);
                    }
                }

                return View(businessPage);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Edit(long? id)
        {
            if ((Session["UserID"] != null))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BusinessPage businessPage = db.BusinessPages.Find(id);
                foreach (var item in businessPage.BusinessPageECImages)
                {
                    item.ECImage = new ECImage { ImagePath = GetECImageUrl(item.ECImageId) };
                }
                if (businessPage == null)
                {
                    return HttpNotFound();
                }
                return View(businessPage);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessPageID,PageName,PageNameUrl,PageContent,CreatedDate,UpdateDate,IsOnFooter,PageOrder,ResponseCode")] BusinessPage businessPage)
        {
            if ((Session["UserID"] != null))
            {
                if (ModelState.IsValid)
                {
                    if (!db.BusinessPages.Any(x => x.PageNameUrl == businessPage.PageNameUrl && x.BusinessPageID != businessPage.BusinessPageID))
                    {
                        businessPage.UpdateDate = DateTime.Now;
                        db.Entry(businessPage).State = EntityState.Modified;
                        foreach (var file in Request.Files)
                        {
                            HttpPostedFileBase fileBase = Request.Files[file.ToString()];
                            if (fileBase.ContentLength != 0)
                            {
                                ECImageViewModel eCImageViewModel = _iservice.SaveToDB(fileBase, Session["UserID"].ToString());
                                BusinessPageECImage businessPageECImage = new BusinessPageECImage();
                                businessPageECImage.ECImageId = eCImageViewModel.ECImageId;
                                businessPageECImage.BusinessPageId = businessPage.BusinessPageID;
                                db.BusinessPageECImages.Add(businessPageECImage);
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else {
                        ModelState.AddModelError("PageNameUrl", "PageNameUrl Already Exist.");
                        return View(businessPage);
                    }
                }
                return View(businessPage);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Delete(long? id)
        {
            if ((Session["UserID"] != null))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BusinessPage businessPage = db.BusinessPages.Find(id);
                if (businessPage == null)
                {
                    return HttpNotFound();
                }
                return View(businessPage);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if ((Session["UserID"] != null))
            {
                BusinessPage businessPage = db.BusinessPages.Find(id);
                db.BusinessPageECImages.RemoveRange(businessPage.BusinessPageECImages);
                db.BusinessPages.Remove(businessPage);
                db.SaveChanges();
                foreach (var item in businessPage.BusinessPageECImages)
                {
                    _iservice.DeleteImage(item.ECImageId, Session["UserID"].ToString());
                }
                return RedirectToAction("Index");
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        private string GetECImageUrl(long ImageId)
        {
            var ecImage = _iservice.GetImageById(ImageId);
            if (ecImage != null)
                return ecImage.ImagePath;
            else
                return "";
        }

        public ActionResult DeleteImage(long businessPageECImageId)
        {
            if ((Session["UserID"] != null))
            {
                string userId = Session["UserID"].ToString();
                BusinessPageECImage businessPageECImage = db.BusinessPageECImages.Find(businessPageECImageId);
                db.BusinessPageECImages.Remove(businessPageECImage);
                db.SaveChanges();
                _iservice.DeleteImage(businessPageECImage.ECImageId, userId);
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
