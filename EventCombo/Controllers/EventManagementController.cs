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

    public static string SARole = "Super Admin";
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

      ev.IsAdmin = User.IsInRole(SARole);

      return View(ev);
    }

    [HttpGet]
    public ActionResult GetEvent(int eventId)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();

      if ((eventId > 0) && (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner) && !User.IsInRole(SARole))
        return new EmptyResult();
      
      EventViewModel ev;
      if (eventId == 0)
        ev = _eService.CreateEvent(userId);
      else
        ev = _eService.GetEventById(eventId);

      PopulateBaseViewModel(ev, "Create Event | Eventcombo");

      ev.IsAdmin = User.IsInRole(SARole);

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
          if ((ev.EventID == 0) || (_dbservice.GetEventAccess(ev.EventID, userId) == AccessLevel.EventOwner) || User.IsInRole(SARole))
          {
            ev.IsAdmin = User.IsInRole(SARole);
            _eService.SaveEvent(ev, Server.MapPath);
            ev = _eService.GetEventById(ev.EventID);
            ev.IsAdmin = User.IsInRole(SARole);
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
    public ActionResult ViewEvent(string strEventDs, string strEventId)
    {
      long eventId;
      if (!Int64.TryParse(strEventId, out eventId))
        return RedirectToAction("Index", "Home");

      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();

      EventInfoViewModel ev = _eService.GetEventInfo(eventId, userId, Url);
      PopulateBaseViewModel(ev, String.Format("{0} | Eventcombo", ev.EventTitle));

      Session["Fromname"] = "events";
      Session["logo"] = "events";
      var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(ev.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ev.EventId.ToString() });
      Session["ReturnUrl"] = "ViewEvent~" + url;

      return View(ev);
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

      EventInfoViewModel ev = _eService.GetEventInfo(eventId, userId, Url);

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
    public ActionResult EditEvent(long eventId)
    {
      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();
      else
        return RedirectToAction("Index", "Home", new { lat = CookieStore.GetCookie("Lat"), lng = CookieStore.GetCookie("Long"), page = "1", strParm = "Y" });
    
      if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
        return RedirectToAction("Index", "Home", new { lat = CookieStore.GetCookie("Lat"), lng = CookieStore.GetCookie("Long"), page = "1", strParm = "Y" });

      Session["logo"] = "events";
      Session["Fromname"] = "events";
      var url = Url.Action("EditEvent", "EventManagement");
      Session["ReturnUrl"] = "EditEvent~" + url;

      EventViewModel ev = _eService.GetEventById(eventId);
      PopulateBaseViewModel(ev, String.Format("Edit {0} | Eventcombo", ev.EventTitle));

      ev.IsAdmin = User.IsInRole(SARole);

      return View("CreateEvent", ev);
    }
  }
}