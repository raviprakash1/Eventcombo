using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.DAL;
using EventCombo.Service;
using EventCombo.Models;
using AutoMapper;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EventCombo.ViewModels;
using System.Text.RegularExpressions;
using NLog;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace EventCombo.Controllers
{
  public class EventManagementController : BaseController
  {
    private IEventService _eService;
    private ILogger _logger;

    public EventManagementController()
      : base()
    {
      _eService = new EventService(_factory, _mapper);
      _logger = LogManager.GetCurrentClassLogger();
    }

    private ActionResult DefaultAction(string returnUrl = "")
    {
      if (!String.IsNullOrWhiteSpace(returnUrl))
        Session["ReturnUrl"] = returnUrl;
      return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult CreateEvent()
    {

      if (Session["AppId"] == null)
      {
        return DefaultAction(Url.Action("CreateEvent", "EventManagement"));
      }
      else
      {
        if (CommanClasses.UserOrganizerStatus(Session["AppId"].ToString()) == false)
        {
          return RedirectToAction("Index", "Home", new { lat = CookieStore.GetCookie("Lat"), lng = CookieStore.GetCookie("Long"), page = "1", strParm = "Y" });
        }

      }
      string userId = Session["AppId"].ToString();

      Session["logo"] = "events";
      Session["Fromname"] = "events";
      var url = Url.Action("CreateEvent", "EventManagement");
      Session["ReturnUrl"] = "CreateEvent~" + url;

      EventViewModel ev = _eService.CreateEvent(userId);
      PopulateBaseViewModel(ev, "Create Event | Eventcombo");

      ev.IsAdmin = _dbservice.IsEventAdmin(userId);

      return View(ev);
    }

    [HttpGet]
    public ActionResult GetEvent(int eventId)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();

      bool isAdmin = _dbservice.IsEventAdmin(userId);

      if ((eventId > 0) && !isAdmin && (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner))
        return new EmptyResult();

      EventViewModel ev;
      if (eventId == 0)
        ev = _eService.CreateEvent(userId);
      else
        ev = _eService.GetEventById(eventId);

      PopulateBaseViewModel(ev, "Create Event | Eventcombo");

      ev.IsAdmin = isAdmin;

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
      res.SerializerSettings.Converters.Add(new StringEnumConverter());
      res.Data = ev;

      return res;
    }

    [HttpPost]
    public ActionResult SaveEvent(string json)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();

      EventViewModel ev = JsonConvert.DeserializeObject<EventViewModel>(json);
      ev.ErrorEvent = false;
      ev.ErrorMessages.Clear();

      if (_eService.ValidateEvent(ev))
      {
        try
        {
          bool isAdmin = _dbservice.IsEventAdmin(userId);
          if ((ev.EventID == 0) || isAdmin || (_dbservice.GetEventAccess(ev.EventID, userId) == AccessLevel.EventOwner))
          {
            ev.IsAdmin = isAdmin;
            _eService.SaveEvent(ev, Server.MapPath);
            ev = _eService.GetEventById(ev.EventID);
            ev.IsAdmin = isAdmin;
          }
          else
            throw new UnauthorizedAccessException(String.Format("User {0} have not access to edit EventId = {1}", userId, ev.EventID));
        }
        catch (Exception ex)
        {
          _logger.Error(ex, "Error during SaveEvent.", null);
          ev.ErrorEvent = true;
          if (ev.ErrorMessages.Count == 0)
            ev.ErrorMessages.Add("Something went wrong. Please try again later.");
        }
      }

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
      res.SerializerSettings.Converters.Add(new StringEnumConverter());
      res.Data = ev;

      return res;
    }

    [HttpGet]
    public ActionResult ViewEvent(string strEventDs, string strEventId, string InviteId)
    {
      long eventId;
      if (!Int64.TryParse(strEventId, out eventId))
        return RedirectToAction("Index", "Home");

      PrivateEventRequest req = new PrivateEventRequest() { EventId = eventId, InviteCode = InviteId };
      EventInfoViewModel ev;

      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();

      Session["Fromname"] = "events";
      Session["logo"] = "events";

      LoadPrivateState(req);
      _eService.CheckEventAccess(req);
      SavePrivateState(req);
      ViewData["PrivateRequest"] = req;

      if (req.InviteValid && req.PasswordValid)
      {
        ev = _eService.GetPrivateEventInfo(req.EventId, userId, Url);
        PopulateBaseViewModel(ev, String.Format("{0} | Eventcombo", ev.EventTitle));
        var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(ev.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ev.EventId.ToString() });
        Session["ReturnUrl"] = "ViewEvent~" + url;
        return View(ev);
      }
      else 
      {
        ev = _eService.GetBasicEventInfo(req.EventId, Url);
        PopulateBaseViewModel(ev, String.Format("{0} | Eventcombo", ev.EventTitle));
        var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(ev.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ev.EventId.ToString() });
        Session["ReturnUrl"] = "ViewEvent~" + url;
        return View(ev);
      }
    }

    [HttpPost]
    public ActionResult CheckPrivateEventAccess(string json)
    {
      ActionResultViewModel res = new ActionResultViewModel() { Success = true, ErrorCode = 0, ErrorMessage = "" };
      PrivateEventRequest req = JsonConvert.DeserializeObject<PrivateEventRequest>(json);
      LoadPrivateState(req);
      _eService.CheckEventAccess(req);
      SavePrivateState(req);
      if (!req.PasswordValid)
      {
        res.Success = false;
        res.ErrorCode = 1;
        res.ErrorMessage = "Password doesn't match";
      }

      JsonNetResult result = new JsonNetResult();
      result.Data = res;
      return result;
    }

    private void LoadPrivateState(PrivateEventRequest req)
    {
      if ((Session["PrivateEventId"] != null) && ((long)Session["PrivateEventId"]) == req.EventId)
      {
        req.PasswordValid = (Session["PrivatePasswordValid"] != null) && (bool)Session["PrivatePasswordValid"];
        req.InviteValid = (Session["PrivateInviteValid"] != null) && (bool)Session["PrivateInviteValid"];
      }
      else
      {
        req.PasswordValid = false;
        req.InviteValid = false;
      }
    }

    private void SavePrivateState(PrivateEventRequest req)
    {
      Session["PrivateEventId"] = req.EventId;
      Session["PrivatePasswordValid"] = req.PasswordValid;
      Session["PrivateInviteValid"] = req.InviteValid;
    }

    [HttpPost]
    public ActionResult StartPurchase(string ev)
    {
      Ticket_Locked_Detail td = JsonConvert.DeserializeObject<Ticket_Locked_Detail>(ev);
      var ceController = new CreateEventController();
      ceController.ControllerContext = new ControllerContext(this.Request.RequestContext, ceController);
      return Content(ceController.LockTickets(td));
    }

    [HttpGet]
    public ActionResult GetEventInfo(long eventId)
    {
      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();

      PrivateEventRequest req = new PrivateEventRequest { EventId = eventId };
      LoadPrivateState(req);

      EventInfoViewModel ev;
      if (req.InviteValid && req.PasswordValid)
        ev = _eService.GetPrivateEventInfo(eventId, userId, Url);
      else
        ev = _eService.GetEventInfo(eventId, userId, Url);

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
      res.SerializerSettings.Converters.Add(new StringEnumConverter());
      res.Data = ev;

      return res;
    }

    [HttpPost]
    [Authorize]
    public ActionResult AddFavorite(long eventId)
    {
      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();

      IncrementResultViewModel actionResult = _eService.AddFavorite(eventId, userId);

      JsonNetResult res = new JsonNetResult();
      res.Data = actionResult;

      return res;
    }

    [HttpPost]
    [Authorize]
    public ActionResult VoteEvent(long eventId)
    {
      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();

      IncrementResultViewModel actionResult = _eService.VoteEvent(eventId, userId);

      JsonNetResult res = new JsonNetResult();
      res.Data = actionResult;

      return res;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> CMSEditEvent(long eventId)
    {
      if (User.Identity.IsAuthenticated)
      {
        ApplicationUserManager um = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        var user = await um.FindByNameAsync(User.Identity.Name);
        Session["AppId"] = user.Id;
      }
      else
        throw new UnauthorizedAccessException("Unauthorized access from CMS.");

      ViewBag.CMSCall = true;

      return EditEvent(eventId);
    }

    [HttpGet]
    [Authorize]
    public ActionResult EditEvent(long eventId)
    {
      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();
      else
        return RedirectToAction("Index", "Home");

      bool isAdmin = _dbservice.IsEventAdmin(userId);

      if (!isAdmin && (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner))
        return RedirectToAction("Index", "Home");

      Session["logo"] = "events";
      Session["Fromname"] = "events";
      var url = Url.Action("EditEvent", "EventManagement");
      Session["ReturnUrl"] = "EditEvent~" + url;

      EventViewModel ev = _eService.GetEventById(eventId);
      PopulateBaseViewModel(ev, String.Format("Edit {0} | Eventcombo", ev.EventTitle));

      ev.IsAdmin = isAdmin;

      return View("CreateEvent", ev);
    }
  }
}