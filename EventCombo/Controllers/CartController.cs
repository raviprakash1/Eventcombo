using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Text.RegularExpressions;
using EventCombo.Utils;
using System.Globalization;

namespace EventCombo.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public JsonResult PayPaltoken()
        {
            PayPalRedirect redirect = new PayPalRedirect();
            string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            using (var objContent = new EventComboEntities())
            {
                 var EventOrderDetail = (from Order in objContent.Ticket_Purchased_Detail where Order.TPD_GUID == strGUID select Order).FirstOrDefault();
                // var OrderDetail = (from Orderd in  objContent.Order_Detail_T where Orderd.O_Order_Id == EventOrderDetail.TPD_Order_Id select Orderd).FirstOrDefault();
                try
                {
                    Session["Fromname"] = "events";
                    Session["logo"] = "events";
                    TicketPayment TicketPayment = new TicketPayment();
                    TicketPayment = (TicketPayment)Session["TicketDatamodel"];
                    PayPalOrder objPay = new PayPalOrder();
                    objPay.Amount = Convert.ToDecimal(TicketPayment.strGrandTotal);
                    objPay.OrderId = "";
                    var url = Request.Url;
                    var baseurl = url.GetLeftPart(UriPartial.Authority);
                    objPay.CancelUrl = baseurl + Url.Action("TicketPayment", "TicketPayment");

                    redirect = PayPal.ExpressCheckout(objPay);
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendErrorToText(ex);
                }
                return Json(redirect.Token, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult CheckoutReview(string token, string PayerID)
        {
            string retMsg = "";
            string PayerID1 = "";

            try
            {
                EventCreation cs = new EventCreation();


                string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                using (var objContent = new EventComboEntities())
                {
                    var EventOrderDetail = (from Order in objContent.Ticket_Locked_Detail where Order.TLD_GUID == strGUID select Order).FirstOrDefault();
                    var Eventid = EventOrderDetail.TLD_Event_Id??0;
                    var Eventdetails = cs.GetEventdetail(Eventid);

                 var imgurl = (!string.IsNullOrEmpty(cs.GetImages(Eventid).FirstOrDefault()) ? cs.GetImages(Eventid).FirstOrDefault() : "/Images/default_event_image.jpg");
                    var Title = Eventdetails.EventTitle;
                    var addresstemp = objContent.Addresses.FirstOrDefault(i => i.EventId == Eventid);
                    DateTime datetime = new DateTime();
                    DateTimeWithZone dtzstart;
                    string day = "", Sdate = "", time = "";
                    var td = DateTimeWithZone.Timezonedetail(Eventid);
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
                    TempData["address"] = (!string.IsNullOrEmpty(addresstemp.ConsolidateAddress)) ? addresstemp.ConsolidateAddress : "";
                  
                    TempData["ImagUrl"] = imgurl;
                    TempData["Title"] = Title;
                    if (Eventdetails.AddressStatus == "Single")
                    {
                        var ev = objContent.EventVenues.FirstOrDefault(i => i.EventID == Eventid);
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(ev.E_Startdate), userTimeZone, true);
                        datetime = dtzstart.LocalTime;
                        day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datetime).ToString();
                        Sdate = datetime.ToString("MMM dd, yyyy");
                        time = datetime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "");

                    }
                    TempData["eventdatetime"]=day + "," + Sdate + " " + time;

                }
                TicketPayment TicketPayment = new TicketPayment();
                TicketPayment = (TicketPayment)Session["TicketDatamodel"];
                PayPal.GetCheckoutDetails(token, ref PayerID1, ref retMsg);
                ViewData["ReturnMessage"] = retMsg;
                ViewData["token"] = token;
                ViewData["PayerID"] = PayerID1;
                ViewData["Amount"] = TicketPayment.strGrandTotal;
            }
            catch (Exception ex)
            {
                Console.Write("CheckoutReview");
                Console.Write(ex.InnerException.ToString());  
                ExceptionLogging.SendErrorToText(ex);
                ExceptionLogging.SendErrorToText(ex.InnerException);
            }
            return View();
        }
        public ActionResult DoCheckoutPayment(string token, string PayerID)
        {
            string retMsg = "";
            TicketPayment TicketPayment = new TicketPayment();
            try
            {
                Session["Fromname"] = "events";
                Session["logo"] = "events";
                TicketPayment = (TicketPayment)Session["TicketDatamodel"];
                string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                EventCreation cs = new EventCreation();
                using (var objContent = new EventComboEntities())
                {
                    var EventOrderDetail = (from Order in objContent.Ticket_Locked_Detail where Order.TLD_GUID == strGUID select Order).FirstOrDefault();
                    var Eventid = EventOrderDetail.TLD_Event_Id ?? 0;
                    var Eventdetails = cs.GetEventdetail(Eventid);

                    var imgurl = (!string.IsNullOrEmpty(cs.GetImages(Eventid).FirstOrDefault()) ? cs.GetImages(Eventid).FirstOrDefault() : "/Images/default_event_image.jpg");
                    var Title = Eventdetails.EventTitle;
                    var addresstemp = objContent.Addresses.FirstOrDefault(i => i.EventId == Eventid);
                    DateTime datetime = new DateTime();
                    DateTimeWithZone dtzstart;
                    string day = "", Sdate = "", time = "";
                    var td = DateTimeWithZone.Timezonedetail(Eventid);
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
                    TempData["address"] = (!string.IsNullOrEmpty(addresstemp.ConsolidateAddress)) ? addresstemp.ConsolidateAddress : "";

                    TempData["ImagUrl"] = imgurl;
                    TempData["Title"] = Title;
                    if (Eventdetails.AddressStatus == "Single")
                    {
                        var ev = objContent.EventVenues.FirstOrDefault(i => i.EventID == Eventid);
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(ev.E_Startdate), userTimeZone, true);
                        datetime = dtzstart.LocalTime;
                        day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datetime).ToString();
                        Sdate = datetime.ToString("MMM dd, yyyy");
                        time = datetime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "");

                    }
                    TempData["eventdatetime"] = day + "," + Sdate + " " + time;

                }
                if (PayPal.DoCheckoutPayment(TicketPayment.strGrandTotal, token, PayerID, ref retMsg))
                {
                    ViewData["ReturnMessage"] = "";
                    ViewData["token"] = token;
                    ViewData["PayerID"] = PayerID;
                    ViewData["TRANSACTIONID"] = retMsg;
                    ViewData["Amount"] = TicketPayment.strGrandTotal;

                    //TicketPayment objTP = (Session["TicketDatamodel"] != null ? (TicketPayment)Session["TicketDatamodel"] : null);
                    //if (objTP != null)
                    //{
                    //    TicketPaymentController objTc = new TicketPaymentController();
                    //    objTc.ControllerContext = new ControllerContext(this.Request.RequestContext, objTc);
                    //    //string strResult = await objTc.SaveDetails(objTP, objTP.strOrderTotal, objTP.strGrandTotal, objTP.strPromId, objTP.strVarChanges, objTP.strVarId, objTP.strPaymentType);

                    //    //RedirectToAction("PaymentConfirmation", "TicketPayment");
                    //}

                }
                else
                {
                    ViewData["token"] = "";
                    ViewData["PayerID"] = "";
                    ViewData["TRANSACTIONID"] = "";
                    ViewData["Amount"] = "";
                    ViewData["ReturnMessage"] = retMsg;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return View();
        }

        public string SetSessionPaypaldetail(TicketPayment model)
        {
            string strResult = "";
            try
            {
                strResult = "Y";
                Session["TicketDatamodel"] = model;
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                strResult = "N";
            }
            return strResult;
        }
    }
}