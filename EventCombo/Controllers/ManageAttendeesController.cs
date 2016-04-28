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

    public ManageAttendeesController()
    {
      if (_maservice == null)
      {
        IUnitOfWorkFactory uowFactory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory());
        AutoMapper.IMapper mapper = AutomapperConfig.Config.CreateMapper();
        _dbservice = new DBAccessService(uowFactory, mapper);
        _maservice = new ManageAttendeesService(uowFactory, mapper, _dbservice);
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

      return View(commonInfo);
    }

    [HttpGet]
    public ActionResult Add()
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();
      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Add", "ManageAttendees");
      return View();
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
    public ActionResult Guests()
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();
      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Guests", "ManageAttendees");
      return View();
    }

    [HttpGet]
    public ActionResult Badges()
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();
      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Badges", "ManageAttendees");
      return View();
    }

    [HttpGet]
    public ActionResult Checkin()
    {
      if ((Session["AppId"] == null))
        return DefaultAction();

      string userId = Session["AppId"].ToString();
      Session["logo"] = "events";
      Session["Fromname"] = "ManageAttendees";
      Session["ReturnUrl"] = Url.Action("Checkin", "ManageAttendees");
      return View();
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
        orders = orders.Where(o => (o.Date >= startDate));
      else
        orders = orders.Where(o => ((o.Date >= startDate) && ((o.OrderId.Contains(req.Search.Trim())) || (o.BuyerName.Contains(req.Search.Trim())) || (o.BuyerEmail.Contains(req.Search.Trim())))));
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

      if (_maservice.SendConfirmations(orderId, Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~/")))
        return 1;
      else
        return 0;
    }
  }
}