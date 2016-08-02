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
    }
}