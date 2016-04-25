using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
{
    
    public class PaymentInfoController : Controller
    {
        // GET: PaymentInfo
        public ActionResult PaymentInfo(long EventId)
        {
            //long EventId = 1;

            if (Session["AppId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (Session["logo"] != null)
            {
                ValidationMessageController vmc = new ValidationMessageController();
                EventId = vmc.GetLatestEventId(EventId);
                ViewBag.EventId = EventId;
                Payment_Info objPI = new Payment_Info();

                if (CommanClasses.CompareCurrentUser(EventId, Session["AppId"].ToString().Trim()) == false) return RedirectToAction("Index", "Home");


                EventComboEntities db = new EventComboEntities();
                objPI = db.Payment_Info.Where(x => x.PI_EventId == EventId).SingleOrDefault();
                string defaultCountry = "";

                string Country = "";
                if (objPI != null)
                {
                    Country = Convert.ToString(objPI.PI_Country);
                }

                var countryQuery = (from c in db.Countries
                                    orderby c.Country1 ascending
                                    select c).Distinct();

                List<SelectListItem> countryList = new List<SelectListItem>();
                if (!string.IsNullOrEmpty(Country))
                {
                    defaultCountry = Country;
                }
                else
                {
                    defaultCountry = "United States";
                }
                foreach (var item in countryQuery)
                {
                    countryList.Add(new SelectListItem()
                    {
                        Text = item.Country1,
                        Value = item.CountryID.ToString(),
                        Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                    });
                }


                ViewBag.CountryID = countryList;
                return View(objPI);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult SavePaymentInfo()
        {
            EventComboEntities db = new EventComboEntities();
            long Eventid = 0;
            Payment_Info objPI = new Payment_Info();
            try
            {
                Eventid = Convert.ToInt32(Request["EventId"].ToString());
                objPI.PI_EventId = Convert.ToInt32(Request["EventId"].ToString());
                objPI.PaymentInfo_Id = Convert.ToInt32(Request["PaymentInfoId"].ToString());

            }
            catch { }

            objPI.PI_PaymentMethod = Request["PaymentMethod"].ToString();
            if (objPI.PI_PaymentMethod == "D")
            {
                objPI.PI_AccountHolderType = Request["AccHoldInfo"].ToString();
                if (!string.IsNullOrEmpty(Request["CompanyName"].ToString()))
                {
                    objPI.PI_AccCompName = Request["CompanyName"].ToString();
                }
                else if (!string.IsNullOrEmpty(Request["Name"].ToString()))
                {
                    objPI.PI_AccCompName = Request["Name"].ToString();
                }
                else
                {
                    objPI.PI_AccCompName = string.Empty;
                }

                objPI.PI_Address = Request["Address1"].ToString();
                objPI.PI_Address2 = Request["Address2"].ToString();
                objPI.PI_City = Request["City1"].ToString();
                objPI.PI_State = Request["State1"].ToString();
                objPI.PI_Country = Convert.ToInt16(Request["Country1"].ToString());
                objPI.PI_PostalCode = Request["PostalCode1"].ToString();


                objPI.PI_BankAccInfo = Request["AccInfo"].ToString();
                objPI.PI_BankName = Request["BankName"].ToString();
                objPI.PI_RoutingNumber = Request["RoutingNumber"].ToString();
                objPI.PI_AccountNumber = Request["AcNumber"].ToString();
                objPI.PI_ReAccountNumber = Request["ReEnterAcNumber"].ToString();


            }
            else
            {
                objPI.PI_AccountHolderType = string.Empty;
                objPI.PI_AccCompName = string.Empty;
                objPI.PI_Address = string.Empty;
                objPI.PI_City = string.Empty;
                objPI.PI_State = string.Empty;
                objPI.PI_Country = 0;
                objPI.PI_PostalCode = string.Empty;


                objPI.PI_BankAccInfo = string.Empty;
                objPI.PI_BankName = string.Empty;
                objPI.PI_RoutingNumber = string.Empty;
                objPI.PI_AccountNumber = string.Empty;
                objPI.PI_ReAccountNumber = string.Empty;



                objPI.PI_PayTo = Request["PayeeName"].ToString();
                objPI.PI_Address = Request["MailingAddress1"].ToString();
                objPI.PI_Address2 = Request["MailingAddress2"].ToString();
                objPI.PI_City = Request["City2"].ToString();
                objPI.PI_State = Request["State2"].ToString();
                objPI.PI_Country = Convert.ToInt16(Request["Country2"].ToString());
                objPI.PI_PostalCode = Request["PostalCode2"].ToString();
            }
            //db.Payment_Info.Add(objPI);
            //db.SaveChanges();

            if (string.IsNullOrEmpty(objPI.PaymentInfo_Id.ToString()) || objPI.PaymentInfo_Id == 0)
            {
                db.Payment_Info.Add(objPI);
                db.SaveChanges();
            }
            else
            {
                var PaymentInfoToUpdate = db.Payment_Info.Find(objPI.PaymentInfo_Id);
                if (PaymentInfoToUpdate != null)
                {
                    db.Entry(PaymentInfoToUpdate).CurrentValues.SetValues(objPI);
                    db.SaveChanges();
                }

            }

            string defaultCountry = "";
            string Country = "";
            Country = Convert.ToString(objPI.PI_Country);

            var countryQuery = (from c in db.Countries
                                orderby c.Country1 ascending
                                select c).Distinct();
            List<SelectListItem> countryList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Country))
            {
                defaultCountry = Country;
            }
            else
            {
                defaultCountry = "United States";
            }


            foreach (var item in countryQuery)
            {
                countryList.Add(new SelectListItem()
                {
                    Text = item.Country1,
                    Value = item.CountryID.ToString(),
                    Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                });
            }
            ViewBag.CountryID = countryList;

            return RedirectToAction("Index", "ManageEvent", new { Eventlid = Eventid, type="N" });
        }


    }
}