using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Service;
using EventCombo.DAL;
using EventCombo.Models;
using AutoMapper;
using System.Globalization;

namespace EventCombo.Controllers
{
  [Authorize]
  public class ManageAttendeesController : Controller
  {
    private IManageAttendeesService _maservice;
    private IDBAccessService _dbservice;
    private ITicketsService _tservice;


    public ManageAttendeesController()
    {
      if (_maservice == null)
      {
        IUnitOfWorkFactory uowFactory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory());
        AutoMapper.IMapper mapper = AutomapperConfig.Config.CreateMapper();
        _dbservice = new DBAccessService(uowFactory, mapper);
        _tservice = new TicketService(uowFactory, mapper, _dbservice);
        _maservice = new ManageAttendeesService(uowFactory, mapper, _dbservice, _tservice);
      }

    }
    private ActionResult DefaultAction()
    {
      return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult Orders(long eventId)
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();

      if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
        return DefaultAction();

      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Orders", "ManageAttendees");

      ManageAttendeesOrdersViewModel commonInfo = _maservice.GetEventOrdersSummary(eventId);

      ViewBag.Title = "Manage Attendees for Event-" + commonInfo.EventTitle + " | Eventcombo";

      return View(commonInfo);
    }

    [HttpGet]
    public ActionResult Add(long eventId)
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();

      if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
        return DefaultAction();

      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Add", "ManageAttendees");

      var addAttendee = _maservice.PrepareAddAttendeeOrder(eventId, userId);

      return View(addAttendee);
    }

    [HttpGet]
    public ActionResult RedirectAddPage(long eventId, string successMessage)
    {
        TempData["SuccessMessage"] = successMessage;
        return RedirectToAction("Add", new { eventId = eventId });
    }

    [HttpPost]
    public ActionResult ProcessAddAttendee(AddAttandeeOrder model)
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();

      if (_dbservice.GetEventAccess(model.EventId, userId) != AccessLevel.EventOwner)
        return DefaultAction();

      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Add", "ManageAttendees");

      return View(model);
    }

    [HttpPost]
    public int SaveManualOrder(AddAttandeeOrder model)
    {
      if ((Session["AppId"] == null))
        return -1;

      string userId = Session["AppId"].ToString();

      if (_dbservice.GetEventAccess(model.EventId, userId) != AccessLevel.EventOwner)
        return -1;

      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Add", "ManageAttendees");

      string orderId;
      try
      {
        orderId = _maservice.CreateManualOrder(model, userId);
      }
      catch (Exception ex)
      {
        return -1;
      }

      try
      {
        if (String.IsNullOrWhiteSpace(orderId))
          return -1;
        if (_maservice.SendConfirmations(orderId, Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~/"), Server.MapPath("..")))
          return 1;
        else
          return 0;
      }
      catch(Exception ex)
      {
        return 0;
      }
    }

    [HttpGet]
    public ActionResult Email()
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();
      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Email", "ManageAttendees");
      return View();
    }

    [HttpGet]
    public ActionResult Guests(long eventId)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return new EmptyResult();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Guests", "ManageAttendees");

        ViewBag.EventId = eventId;
        ViewBag.Title = "Guest List | Eventcombo";

        var ticketTypes = _maservice.GetAttendeeTicketTypeList(eventId);

        return View(ticketTypes);
    }

    [HttpGet]
    public ActionResult Badges(long eventId)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return new EmptyResult();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Badges", "ManageAttendees");

        ViewBag.EventId = eventId;
        ViewBag.Title = "Name Badges | Eventcombo";
        var model = _maservice.GetSelectAttendeeDropdownList(eventId);

        return View(model);
    }

    [HttpGet]
    public ActionResult Checkin(long eventId)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return new EmptyResult();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Checkin", "ManageAttendees");

        ViewBag.EventId = eventId;
        ViewBag.Title = "Checkin | Eventcombo";

        AttendeeSearchRequestViewModel attendeeSearchRequest = new AttendeeSearchRequestViewModel();
        attendeeSearchRequest.EventId = eventId;
        var attendees = _maservice.GetAttendeeCheckinList(attendeeSearchRequest);

        return View(attendees);
    }

    [HttpGet]
    public ActionResult GetOrderList(EventOrdersListRequestViewModel req)
    {
      if ((Session["AppId"] == null))
        return new EmptyResult();

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetEventAccess(req.EventId, userId) != AccessLevel.EventOwner)
        return new EmptyResult();

      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Orders", "ManageAttendees");

      EventOrderInfoListViewModel res = new EventOrderInfoListViewModel();
      res.EventId = req.EventId;
      res.PaymentState = req.PaymentState;

      var orders = _maservice.GetOrdersForEvent(req.PaymentState, req.EventId);
      DateTime startDate;
      if (!DateTime.TryParseExact(req.DateFrom, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
      {
        startDate = DateTime.Today;
        startDate = new DateTime(startDate.Year, startDate.Month, 1);
      }
      if (String.IsNullOrWhiteSpace(req.Search))
      {
        orders = orders.Where(o => (o.Date >= startDate));
      }
      else
      {
        orders = orders.Where(o => ((o.Date >= startDate) && ((o.OrderId.ToLower().Contains(req.Search.ToLower().Trim())) || (o.BuyerName.ToLower().Contains(req.Search.ToLower().Trim())) || (o.BuyerEmail.ToLower().Contains(req.Search.ToLower().Trim())))));
      }
      res.Total = orders.Count();

      switch (req.SortBy)
      {
        case EventOrderSortBy.Buyer:
          orders = req.SortDesc ? orders.OrderByDescending(o => o.BuyerName) : orders.OrderBy(o => o.BuyerName);
          break;
        case EventOrderSortBy.Date:
          orders = req.SortDesc ? orders.OrderByDescending(o => o.Date) : orders.OrderBy(o => o.Date);
          break;
        case EventOrderSortBy.Order:
          orders = req.SortDesc ? orders.OrderByDescending(o => o.OrderId) : orders.OrderBy(o => o.OrderId);
          break;
        case EventOrderSortBy.Price:
          orders = req.SortDesc ? orders.OrderByDescending(o => o.Price) : orders.OrderBy(o => o.Price);
          break;
        case EventOrderSortBy.PaymentState:
          orders = req.SortDesc ? orders.OrderByDescending(o => o.PaymentState) : orders.OrderBy(o => o.PaymentState);
          break;
        default:
          orders = req.SortDesc ? orders.OrderByDescending(o => o.Quantity) : orders.OrderBy(o => o.Quantity);
          break;
      };

      if (req.PerPage > 0)
      {
        req.Page = (req.Page * req.PerPage) > res.Total ? res.Total / req.PerPage : req.Page;
        res.Orders.AddRange(orders.Skip(req.PerPage * req.Page).Take(req.PerPage));
      }
      else
        res.Orders.AddRange(orders);

      return PartialView("_OrderList", res);
    }

    [HttpGet]
    public ActionResult GetOrderDetail(string orderId)
    {
      if ((Session["AppId"] == null))
        return new EmptyResult();

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetOrderAccess(orderId, userId) == AccessLevel.Public)
        return new EmptyResult();

      EventOrderDetailViewModel order = _maservice.GetOrderDetails(orderId);

      return PartialView("_OrderDetail", order);
    }

    [HttpPost]
    public int ResendConfirmations(string orderId)
    {
      if ((Session["AppId"] == null))
        return -1;

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetOrderAccess(orderId, userId) == AccessLevel.Public)
        return -1;

      if (_maservice.SendConfirmations(orderId, Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~/"), Server.MapPath("..")))
        return 1;
      else
        return 0;
    }

    [HttpGet]
    public ActionResult SaleReport(int EventId)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();

        if (_dbservice.GetEventAccess(EventId, userId) != AccessLevel.EventOwner)
            return DefaultAction();

        ViewData["EventID"] = EventId;
        IEnumerable<EventOrderInfoViewModel> orders = _maservice.GetOrdersForSaleReport(PaymentStates.Completed, EventId);
        return PartialView("_SaleReport", orders);
    }

    [HttpGet]
    public ActionResult AttendeeEmail(long eventId)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();

        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return DefaultAction();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Orders", "ManageAttendees");
        ViewBag.EventId = eventId;
        ViewBag.Title = "Email to Attendees | Eventcombo";

        return View();
    }
    [HttpGet]
    public ActionResult AttendeeEmailList(long eventId, bool isEmailSend)
    {
        if ((Session["AppId"] == null))
            return new EmptyResult();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return new EmptyResult();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Orders", "ManageAttendees");

        var attendeeEmails = _maservice.GetScheduledEmailList(eventId, isEmailSend);

        return PartialView("_AttendeeEmailList", attendeeEmails);
    }

    [HttpGet]
    public ActionResult AttendeeEmailDetail(long eventId, long scheduledEmailId)
    {
        if ((Session["AppId"] == null))
            return new EmptyResult();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
            return new EmptyResult();

        var attendeeEmail = _maservice.GetScheduledEmailDetail(scheduledEmailId);
        return PartialView("_AttendeeEmailDetail", attendeeEmail);
     }

    public ActionResult SendEmail(long eventId)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return new EmptyResult();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Email", "ManageAttendees");
        ViewBag.EventId = eventId;
        ScheduledEmailViewModel scheduledEmail = _maservice.PrepareSendAttendeeMail(eventId);
        return View(scheduledEmail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult SendEmail(long eventId, [Bind(Include = "SendFrom, SendTo, ReplyTo, CC, BCC, Subject, Body, ScheduledDate, TicketbearerIds, SendTo, RegisteredDate, BeforeEvent_Days, BeforeEvent_Hours, BeforeEvent_Minutes")] ScheduledEmailViewModel scheduledEmail, string SendEmail, string txtOnDateTime, string txtBeforeEventTime)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();
        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Email", "ManageAttendees");
        ViewBag.EventId = eventId;
        scheduledEmail.SendTos = _maservice.GetSendToDropdownList(eventId);

        if (ModelState.IsValid)
        {
            if (scheduledEmail.SendTo == "ALL_ATTENDEES_DATE") {
                if (scheduledEmail.RegisteredDate == null)
                {
                    ModelState.AddModelError("SendTo", "Please select registered date");
                    scheduledEmail = _maservice.PrepareSendAttendeeMail(eventId);
                    return View(scheduledEmail);
                    }
                    else
                    {
                        var id = Utils.DateTimeWithZone.Timezonedetail(eventId);
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
                        var dateTimeWithZone = new Utils.DateTimeWithZone(scheduledEmail.RegisteredDate, userTimeZone, false);
                        scheduledEmail.RegisteredDate = dateTimeWithZone.UniversalTime;
                    }
            }
            else if (scheduledEmail.SendTo == "ATTENDEES")
            {
                if (string.IsNullOrEmpty(scheduledEmail.TicketbearerIds))
                {
                    ModelState.AddModelError("SendTo", "Please select atleast one attendee");
                    scheduledEmail = _maservice.PrepareSendAttendeeMail(eventId);
                    return View(scheduledEmail);
                }
            }
            else if (scheduledEmail.SendTo == "TICKET_ATTENDEES")
            {
                if (string.IsNullOrEmpty(scheduledEmail.TicketbearerIds))
                {
                    ModelState.AddModelError("SendTo", "Please select ticket type");
                    scheduledEmail = _maservice.PrepareSendAttendeeMail(eventId);
                    return View(scheduledEmail);
                }
            }

            if (SendEmail == "Now")
                scheduledEmail.ScheduledDate = DateTime.UtcNow;
            if (SendEmail == "OnDate")
            {
                if (txtOnDateTime != "")
                {
                        var id = Utils.DateTimeWithZone.Timezonedetail(eventId);
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
                        var dateTimeWithZone = new Utils.DateTimeWithZone(DateTime.Parse(txtOnDateTime), userTimeZone, false);
                        scheduledEmail.ScheduledDate = dateTimeWithZone.UniversalTime;
                }
                else
                {
                    ModelState.AddModelError("SendDate", "Date or Time can not be blank.");
                    scheduledEmail = _maservice.PrepareSendAttendeeMail(eventId);
                    return View(scheduledEmail);
                }
            }
            if (SendEmail == "BeforeEvent")
            {
                if (!string.IsNullOrEmpty(scheduledEmail.BeforeEvent_Days) && !string.IsNullOrEmpty(scheduledEmail.BeforeEvent_Hours) && !string.IsNullOrEmpty(scheduledEmail.BeforeEvent_Minutes))
                {
                    TimeSpan timeBeforeTimeSpan = TimeSpan.Parse(scheduledEmail.BeforeEvent_Days + "." + scheduledEmail.BeforeEvent_Hours + ":" + scheduledEmail.BeforeEvent_Minutes + ":00");
                    scheduledEmail.ScheduledDate = scheduledEmail.ScheduledDate.Add(-timeBeforeTimeSpan);
                }
                else
                {
                    ModelState.AddModelError("SendDate", "Date or Time can not be blank.");
                    scheduledEmail = _maservice.PrepareSendAttendeeMail(eventId);
                    return View(scheduledEmail);
                }
            }
            _maservice.SendAttendeeMail(eventId, scheduledEmail, userId, scheduledEmail.TicketbearerIds, scheduledEmail.ScheduledDate);

            return RedirectToAction("AttendeeEmail", new { eventId = eventId });
        }
        return View(scheduledEmail);
    }

    [HttpPost]
    public ActionResult DeleteEmail(long scheduledEmailId)
    {
        var IsDeleted = _maservice.DeleteAttendeeMail(scheduledEmailId);
        return Json(IsDeleted, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public ActionResult GetAttendeeList(AttendeeSearchRequestViewModel request)
    {
        var attendees = _maservice.GetAttendeeList(request);
        return PartialView("_AttendeeSearchList", attendees);
    }

    [HttpPost]
    public ActionResult SendTestEmail(ScheduledEmailViewModel emailViewModel)
    {
        AttendeeMailNotification AttendeeMailNotification = new AttendeeMailNotification();
        AttendeeMailNotification.SendTestEmail(emailViewModel.SendTo, emailViewModel.ReplyTo, emailViewModel.Subject, emailViewModel.Body);
        return Json("true", JsonRequestBehavior.AllowGet);
    }

    public ActionResult EditEmail(long eventId, long scheduledEmailId)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return new EmptyResult();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Email", "ManageAttendees");
        ViewBag.EventId = eventId;
        ScheduledEmailViewModel scheduledEmail = _maservice.GetScheduledEmailDetail(scheduledEmailId);
        return View(scheduledEmail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditEmail(long eventId, [Bind(Include = "ScheduledEmailId,Body")] ScheduledEmailViewModel scheduledEmail)
    {
        if ((Session["AppId"] == null))
            return DefaultAction();

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) != AccessLevel.EventOwner)
            return new EmptyResult();

        Session["logo"] = "events";
        Session["Fromname"] = "ManageAttendees";
        Session["ReturnUrl"] = Url.Action("Email", "ManageAttendees");
        ViewBag.EventId = eventId;
        _maservice.UpdateAttendeeMail(scheduledEmail);
        return Content("");
    }

    [HttpGet]
    public ActionResult GetAttendeeTicketTypeList(long eventId)
    {
        var attendees = _maservice.GetAttendeeTicketTypeList(eventId);
        return PartialView("_AttendeeTicketTypeList", attendees);
    }

    [HttpPost]
    public ActionResult AttendeeNameBadgesPreview(BadgesViewModel badgesViewModel, string format)
    {
        if (Session["AppId"] == null)
            return null;

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(badgesViewModel.EventId, userId) == AccessLevel.Public)
            return null;

        string path = _maservice.GetBadgesPreviewPath(badgesViewModel, format, userId);
        return Content(path);
    }

    [HttpPost]
    public ActionResult AttendeeNameBadgesList(BadgesViewModel badgesViewModel, string format)
    {
        if (Session["AppId"] == null)
            return null;

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(badgesViewModel.EventId, userId) == AccessLevel.Public)
            return null;

        string path = _maservice.GetBadgesListPath(badgesViewModel, format, userId);
        return Content(path);
    }
  }
}