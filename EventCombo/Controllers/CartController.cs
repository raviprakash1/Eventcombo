using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pay()
        {
            string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            using (var objContent = new EventComboEntities())
            {
               // var EventOrderDetail = (from Order in objContent.Ticket_Purchased_Detail where Order.TPD_GUID == strGUID select Order).FirstOrDefault();
               // var OrderDetail = (from Orderd in objContent.Order_Detail_T where Orderd.O_Order_Id == EventOrderDetail.TPD_Order_Id select Orderd).FirstOrDefault();
                PayPalOrder objPay = new PayPalOrder();
                objPay.Amount = 100;
                objPay.OrderId ="OT" ;

                //objPay.Amount = OrderDetail.O_OrderAmount;
                //objPay.OrderId = OrderDetail.O_Order_Id;
                objPay.ReturnUrl = "http://eventcombo.kiwireader.com//" + Url.Action("PayPalReturn", "PayPalReturn");
                objPay.CancelUrl = "http://eventcombo.kiwireader.com//" + Url.Action("PayPalReturn", "PayPalReturn");

                //objPay.ReturnUrl = "http://eventcombo.kiwireader.com//" + Url.Action("PaymentConfirmation", "TicketPayment");
                //objPay.CancelUrl = "http://eventcombo.kiwireader.com//" + Url.Action("PaymentConfirmation", "TicketPayment");
                PayPalRedirect redirect = PayPal.ExpressCheckout(objPay);
                //Session["token"] = redirect.Token;
                return new RedirectResult(redirect.Url);
            }
        }
    }
}