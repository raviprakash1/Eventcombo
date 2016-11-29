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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EventCombo.Controllers
{
  public class TicketPurchaseController : BaseController
  {
    private ApplicationUserManager _userManager;
    private IPurchasingService _pService;
    private IAccountService _aService;
    private ILogger _logger;
    private ITicketsService _tService;
    private IManageAttendeesService _maService;

    public TicketPurchaseController()
      : base()
    {
      _pService = new PurchasingService(_factory, _mapper);
      _aService = new AccountService(_factory, _mapper);
      _tService = new TicketService(_factory, _mapper, _dbservice, this);
      _maService = new ManageAttendeesService(_factory, _mapper, _dbservice, _tService);
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
      string userId;
      if (Session["buyerId"] != null)
        userId = Session["buyerId"].ToString();
      else
        userId = "";
      if ((Session["AppId"] != null) && String.IsNullOrEmpty(userId))
        userId = Session["AppId"].ToString();

      _maService.SendConfirmations(orderId, Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~/"), Server.MapPath(".."));
      OrderConfirmationViewModel oinfo = _pService.GetOrderConfirmationInfo(orderId, userId);
      if ((oinfo == null) || String.IsNullOrEmpty(oinfo.OrderId))
        return RedirectToAction("Index", "Home");

      PopulateBaseViewModel(oinfo, "Confirmation for Order #" + oinfo.OrderId + " | Eventcombo");
      Session["Fromname"] = "confirmation";
      return View(oinfo);
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
      try
      {
        EventPurchaseInfoViewModel model = JsonConvert.DeserializeObject<EventPurchaseInfoViewModel>(json);

        if (String.IsNullOrEmpty(userId))
          userId = GetOrCreateUser(model.PurchaseInfo.Email, model.PurchaseInfo.FirstName, model.PurchaseInfo.LastName);

        pres = _pService.SavePurchaseInfo(model, userId, IP);
        if (pres.Success)
          Session.Add("buyerId", userId);
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

    private string GetOrCreateUser(string email, string firstName, string lastName)
    {
      RegisterUserRequestViewModel model = new RegisterUserRequestViewModel() 
      { 
        Email = email, FirstName = firstName, LastName = lastName, Password = System.Web.Security.Membership.GeneratePassword(12, 0)
      };

      var user = UserManager.FindByEmail(model.Email);
      if (user == null)
      {
        var aUser = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var resultCreate = UserManager.Create(aUser, model.Password);
        user = UserManager.FindByEmail(model.Email);
        if (resultCreate.Succeeded)
        {
          UserManager.AddToRole(user.Id, "Member");
          _aService.RegisterNewUser(user.Id, model, ClientIPAddress.GetLanIPAddress(Request), true);
        }
      }
      if (user == null)
        return "";
      else
        return user.Id;
    }


    [HttpGet]
    public ActionResult CompletePayment(string orderId, string paymentId, string token, string PayerId)
    {
      if ((Session["PP" + orderId] != null) && (Session["PP" + orderId].ToString() == paymentId))
      {
        _pService.CompletePayPalPayment(orderId, paymentId, token, PayerId);
        return RedirectToAction("Confirmation", new { orderId = orderId });
      }
      else
        return RedirectToAction("Index", "Home");
    }

    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    [HttpGet]
    public ActionResult CheckPromocode(long eventId, string promocode)
    {
      PromoCodeResponseViewModel promo = _pService.GetPromoCode(eventId, promocode);

      JsonNetResult res = new JsonNetResult();
      res.Data = promo;
      return res;
    }
  }
}