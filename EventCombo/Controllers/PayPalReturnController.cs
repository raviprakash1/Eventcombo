using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
{
    public class PayPalReturnController : Controller
    {
        // GET: PayPalReturn
        public async System.Threading.Tasks.Task<ActionResult> PayPalReturn(string token, string PayerID)
        {
            // will also save token and payer id in database;
            Session["token"] = token;
            Session["PayerID"] = PayerID;
            TicketPayment objTP = (Session["TicketDatamodel"] != null ? (TicketPayment)Session["TicketDatamodel"] : null);
            if (objTP != null)
            {
                TicketPaymentController objTc = new TicketPaymentController();
                string strResult = await objTc.SaveDetails(objTP, objTP.strOrderTotal, objTP.strGrandTotal, objTP.strPromId, objTP.strVarChanges, objTP.strVarId, objTP.strPaymentType);
            }
            return View();
        }
    }
}