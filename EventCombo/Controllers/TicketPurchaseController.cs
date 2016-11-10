using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EventCombo.Service;
using EventCombo.DAL;
using EventCombo.Utils;
using NLog;

namespace EventCombo.Controllers
{
  public class TicketPurchaseController : BaseController
  {
    private IPurchasingService _pService;
    private ILogger _logger;

    public TicketPurchaseController()
      : base()
    {
      _pService = new PurchasingService(_factory, _mapper);
      _logger = LogManager.GetCurrentClassLogger();
    }

    [HttpPost]
    public ActionResult LockTickets(string json)
    {
      TicketLockRequest req = JsonConvert.DeserializeObject<TicketLockRequest>(json);
      req.IP = ClientIPAddress.GetLanIPAddress(Request);

      if (Session["AppId"] != null)
        req.UserId = Session["AppId"].ToString();
      else
        req.UserId = "";


      TicketLockResult tres = _pService.TryLockTickets(req);

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
      res.SerializerSettings.Converters.Add(new StringEnumConverter());
      res.Data = tres;
      return res;
    }

    [HttpGet]
    public ActionResult Checkout(string lockId)
    {
      string IP = ClientIPAddress.GetLanIPAddress(Request);
      string userId;
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();
      else
        userId = "";

      EventPurchaseInfoViewModel model = _pService.GetEventPurchaseInfo(lockId, userId, IP);
      if (model.EventId == 0)
        return RedirectToAction("Index", "Home");
      PopulateBaseViewModel(model, String.Format("{0} | Eventcombo", model.EventTitle));
      return View(model);
    }

    [HttpGet]
    public ActionResult GetPurchaseInfo(string lockId)
    {
      string IP = ClientIPAddress.GetLanIPAddress(Request);
      string userId;
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();
      else
        userId = "";

      EventPurchaseInfoViewModel model = _pService.GetEventPurchaseInfo(lockId, userId, IP);

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
      res.Data = model;
      return res;
    }

    [HttpPost]
    public ActionResult ReleaseTickets(string lockId)
    {
      var IP = ClientIPAddress.GetLanIPAddress(Request);
      var result = _pService.DeleteTicketLock(lockId, IP);
      JsonNetResult res = new JsonNetResult();
      res.Data = result;
      return res;
    }

    [HttpGet]
    public ActionResult Confirmation(string orderId)
    {
      return RedirectToAction("PurchasedTicket", "Account");
    }

    [HttpPost]
    public ActionResult SavePurchaseInfo(string json)
    {

      string userId;
      var IP = ClientIPAddress.GetLanIPAddress(Request);
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();
      else
        userId = "";
      PurchaseResult pres = new PurchaseResult() { Success = false, Message = "", OrderId = "" };
      if (String.IsNullOrEmpty(userId))
      {
        pres.Message = "Login or register, please.";
      }
      else
        try
        {
          EventPurchaseInfoViewModel model = JsonConvert.DeserializeObject<EventPurchaseInfoViewModel>(json);
          pres = _pService.SavePurchaseInfo(model, userId, IP);
          if (pres.Success && !String.IsNullOrEmpty(pres.PayPalId))
          {
            Session.Add("PP" + pres.OrderId, pres.PayPalId);
            pres.PayPalId = "";
          }
        }
        catch (Exception ex)
        {
          _logger.Error(ex, "Error during SavePurchaseInfo processing.");
          pres.Message = "Something went wrong. Please try again later";
          pres.Success = false;
        }

      JsonNetResult res = new JsonNetResult();
      res.Data = pres;
      return res;
    }

    [HttpGet]
    public ActionResult CompletePayment(string orderId, string paymentId, string token, string PayerId)
    {
      if ((Session["PP" + orderId] != null) && (Session["PP" + orderId].ToString() == paymentId))
      {
        _pService.CompletePayPalPayment(orderId, paymentId, token, PayerId);
        return RedirectToAction("Confirmation");
      }
      else
        return RedirectToAction("Index", "Home");
    }

  }
}