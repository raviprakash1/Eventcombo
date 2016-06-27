using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    [Authorize]
    public class BusinessPagesController : Controller
    {
        private EmsEntities db = new EmsEntities();

        // GET: BusinessPages
        public ActionResult Index()
        {
            return View(db.BusinessPages.ToList());
        }

        // GET: BusinessPages/Details/5
        public ActionResult Details(long? id)
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

        // GET: BusinessPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageName,PageNameUrl,PageContent")] BusinessPage businessPage)
        {
            if (ModelState.IsValid)
            {
                businessPage.CreatedDate = DateTime.Now;
                businessPage.UpdateDate = DateTime.Now;
                db.BusinessPages.Add(businessPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessPage);
        }

        // GET: BusinessPages/Edit/5
        public ActionResult Edit(long? id)
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

        // POST: BusinessPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessPageID,PageName,PageNameUrl,PageContent")] BusinessPage businessPage)
        {
            if (ModelState.IsValid)
            {
                //var currentBusinessPage = db.BusinessPages.FirstOrDefault(p => p.BusinessPageID == businessPage.BusinessPageID);
                //if (currentBusinessPage == null)
                //    return HttpNotFound();
                businessPage.UpdateDate = DateTime.Now;
                businessPage.CreatedDate = DateTime.Now;// currentBusinessPage.CreatedDate;
                db.Entry(businessPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businessPage);
        }

        // GET: BusinessPages/Delete/5
        public ActionResult Delete(long? id)
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

        // POST: BusinessPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BusinessPage businessPage = db.BusinessPages.Find(id);
            db.BusinessPages.Remove(businessPage);
            db.SaveChanges();
            return RedirectToAction("Index");
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
