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
        public ActionResult PaymentInfo()
        {
            Payment_Info objPI = new Payment_Info();

            return View(objPI);
        }
    }
}