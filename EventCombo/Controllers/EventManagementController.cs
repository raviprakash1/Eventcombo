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
namespace EventCombo.Controllers
{
  public class EventManagementController : BaseController
  {
    private IEventService _eService;
    public EventManagementController()
      : base()
    {
      _eService = new EventService(_factory, _mapper);
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

      return View(ev);
    }

    [HttpGet]
    public ActionResult GetEvent(int eventId)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();

      EventViewModel ev = _eService.CreateEvent(userId);
      PopulateBaseViewModel(ev, "Create Event | Eventcombo");

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
      res.Data = ev;

      return res;
    }

    [HttpPost]
    public ActionResult SaveEvent(string json)
    {
      if (Session["AppId"] == null)
        return null;

      EventViewModel ev = JsonConvert.DeserializeObject<EventViewModel>(json);

      if (_eService.ValidateEvent(ev))
      {
        _eService.SaveEvent(ev, Server.MapPath);
      }

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
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
      res.Data = ev;

      return res;
    }

    [HttpGet]
    public ActionResult EmptyTemplate()
    {
      string userId = "";
      if (Session["AppId"] != null)
        userId = Session["AppId"].ToString();

      BaseViewModel ev = new BaseViewModel();
      PopulateBaseViewModel(ev, "Empty Template | Eventcombo");

      Session["ReturnUrl"] = Url.Action("EmptyTemplate");

      return View(ev);
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
  }
}