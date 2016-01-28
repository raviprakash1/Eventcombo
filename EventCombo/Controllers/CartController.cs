﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public JsonResult PayPaltoken()
        {
            string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            using (var objContent = new EventComboEntities())
            {
                // var EventOrderDetail = (from Order in objContent.Ticket_Purchased_Detail where Order.TPD_GUID == strGUID select Order).FirstOrDefault();
                // var OrderDetail = (from Orderd in objContent.Order_Detail_T where Orderd.O_Order_Id == EventOrderDetail.TPD_Order_Id select Orderd).FirstOrDefault();
                TicketPayment TicketPayment = new TicketPayment();
                TicketPayment = (TicketPayment)Session["TicketDatamodel"];
                PayPalOrder objPay = new PayPalOrder();
                objPay.Amount = Convert.ToDecimal(TicketPayment.strGrandTotal);
                objPay.OrderId = "";

                PayPalRedirect redirect = PayPal.ExpressCheckout(objPay);

                return Json(redirect.Token, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckoutReview(string token, string PayerID)
        {
            string retMsg = "";
            string PayerID1 = "";
            TicketPayment TicketPayment = new TicketPayment();
            TicketPayment = (TicketPayment)Session["TicketDatamodel"];
            PayPal.GetCheckoutDetails(token, ref PayerID1, ref retMsg);
            ViewData["ReturnMessage"] = retMsg;
            ViewData["token"] = token;
            ViewData["PayerID"] = PayerID1;
            ViewData["Amount"] = TicketPayment.strGrandTotal;
            return View();
        }
        public ActionResult DoCheckoutPayment(string token, string PayerID)
        {
            string retMsg = "";
            TicketPayment TicketPayment = new TicketPayment();
            TicketPayment = (TicketPayment)Session["TicketDatamodel"];

            if (PayPal.DoCheckoutPayment(TicketPayment.strGrandTotal, token, PayerID, ref retMsg))
            {
                ViewData["ReturnMessage"] = "";

                ViewData["token"] = token;
                ViewData["PayerID"] = PayerID;
                ViewData["TRANSACTIONID"] = retMsg;
                ViewData["Amount"] = TicketPayment.strGrandTotal;
            }
            else
            {
                ViewData["ReturnMessage"] = retMsg;
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
            catch (Exception)
            {
                strResult = "N";
            }
            return strResult;
        }
    }
}