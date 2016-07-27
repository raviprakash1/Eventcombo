using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Text;
using NLog;
using EventCombo.Utils;
namespace EventCombo.Controllers
{
    public class ContactEventComboesController : Controller
    {
        private EventComboEntities db = new EventComboEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: ContactEventComboes
        public ActionResult Index()
        {

            var EventCat = (from myRow in db.EventCategories
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
            ViewBag.ddlEventCategory = EventCategory;
            return View(db.ContactEventComboes.ToList());
        }


        public string GetSubCat(long lECatId)
        {

            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var EventCat = (from myRow in objEnt.EventSubCategories
                                    where myRow.EventCategoryID == lECatId
                                    orderby myRow.EventSubCategory1
                                    select myRow).ToList();

                    //strHtml.Append("< option value =0 selected=true>Select</ option > ");
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
        public string Temp()
        {
            return "Test";
        }
        public string SaveAndSend(ContactEventCombo model)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                ContactEventCombo msg = new ContactEventCombo();
                msg.Email = model.Email;
                msg.Name = model.Name;
                msg.Question = model.Question;
                msg.PhoneNo = model.PhoneNo;
                msg.Category = model.Category;
                msg.SubCategory = model.SubCategory;
                db.ContactEventComboes.Add(msg);
                try
                {
                    int i = db.SaveChanges();
                    Email.SendToEventCombo(msg.Id);
                }
                catch (Exception ex)
                {
                    logger.Error("Exception during request processing", ex);
                }
                return "saved";
            }
        }
        // GET: ContactEventComboes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEventCombo contactEventCombo = db.ContactEventComboes.Find(id);
            if (contactEventCombo == null)
            {
                return HttpNotFound();
            }
            return View(contactEventCombo);
        }

        // GET: ContactEventComboes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactEventComboes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,PhoneNo,Category,SubCategory")] ContactEventCombo contactEventCombo)
        {
            if (ModelState.IsValid)
            {
                db.ContactEventComboes.Add(contactEventCombo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactEventCombo);
        }

        // GET: ContactEventComboes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEventCombo contactEventCombo = db.ContactEventComboes.Find(id);
            if (contactEventCombo == null)
            {
                return HttpNotFound();
            }
            return View(contactEventCombo);
        }

        // POST: ContactEventComboes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,PhoneNo,Category,SubCategory")] ContactEventCombo contactEventCombo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactEventCombo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactEventCombo);
        }

        // GET: ContactEventComboes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEventCombo contactEventCombo = db.ContactEventComboes.Find(id);
            if (contactEventCombo == null)
            {
                return HttpNotFound();
            }
            return View(contactEventCombo);
        }

        // POST: ContactEventComboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactEventCombo contactEventCombo = db.ContactEventComboes.Find(id);
            db.ContactEventComboes.Remove(contactEventCombo);
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
