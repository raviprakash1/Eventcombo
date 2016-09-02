using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Service;
using CMS.DAL;
using CMS.Models;

namespace CMS.Controllers
{
    [CustomAuthorization("13")]
  public class OrdersController : Controller
  {
    TicketService _tservice;
    public OrdersController()
    {
      if (_tservice == null)
      {
        IUnitOfWorkFactory uowFactory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory(new EmsEntities()));
        AutoMapper.IMapper mapper = AutomapperConfig.Config.CreateMapper();
        _tservice = new TicketService(uowFactory, mapper, new DBAccessService(uowFactory, mapper));
      }
    }
    // GET: Orders
    public ActionResult Index()
    {
      if ((Session["UserID"] == null))
        return RedirectToAction("Login", "Home");

      OrderViewModel ordersInfo = new OrderViewModel();
      foreach (var ttype in (OrderTypes[])Enum.GetValues(typeof(OrderTypes)))
      {
        ordersInfo.OrderTypeInfo.Add(new OrderCommonInfoViewModel() { OrderType = ttype, TotalCount = _tservice.GetOrdersCount(ttype, "") });
      }

      ViewBag.CurrentItem = "account";
      return View(ordersInfo);

    }

    [HttpGet]
    public ActionResult PurchasedTicketList(OrderListRequestViewModel model)
    {
      if ((Session["UserID"] == null))
        return RedirectToAction("Login", "Home");

      OrderListMainViewModel olist = new OrderListMainViewModel();
      olist.OrderType = model.OrderType;

      var orders = _tservice.GetOrdersList(model.OrderType, String.Empty, model.Search);
      switch (model.SortBy)
      {
        case OrderSortBy.Date:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.EventStartDate) : orders.OrderBy(t => t.EventStartDate);
          break;
        case OrderSortBy.Name:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.Name) : orders.OrderBy(t => t.Name);
          break;
        case OrderSortBy.Order:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.OrderId) : orders.OrderBy(t => t.OrderId);
          break;
        case OrderSortBy.Qty:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.Quantity) : orders.OrderBy(t => t.Quantity);
          break;
        case OrderSortBy.Address:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.State).ThenByDescending(t => t.City)
            : orders.OrderBy(t => t.State).ThenBy(t => t.City);
          break;
        case OrderSortBy.FullName:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.FirstName).ThenByDescending(t => t.LastName)
            : orders.OrderBy(t => t.FirstName).ThenBy(t => t.LastName);
          break;
        case OrderSortBy.Email:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.Email) : orders.OrderBy(t => t.Email);
          break;
        case OrderSortBy.EventId:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.EventId) : orders.OrderBy(t => t.EventId);
          break;
        case OrderSortBy.OrderDate:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.OrderDateTime) : orders.OrderBy(t => t.OrderDateTime);
          break;
        case OrderSortBy.Status:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.OrderStateName) : orders.OrderBy(t => t.OrderStateName);
          break;
        case OrderSortBy.Total:
          orders = model.SortDesc ? orders.OrderByDescending(t => t.TotalPaid) : orders.OrderBy(t => t.TotalPaid);
          break;
        default:
          orders = model.SortDesc ? orders.OrderByDescending(t => Math.Abs(t.OrderDateTime.Value.Ticks - DateTime.Today.AddDays(1).Ticks))
            : orders.OrderBy(t => Math.Abs(t.OrderDateTime.Value.Ticks - DateTime.Today.AddDays(1).Ticks));
          break;
      }

        orders = orders.Select((element, index) => new OrderMainViewModel
        {
            RowNumber = index + 1,
            OId = element.OId,
            Name = element.Name,
            EventStartDate = element.EventStartDate,
            EventEndDate = element.EventEndDate,
            Quantity = element.Quantity,
            TotalPaid = element.TotalPaid,
            OrderId = element.OrderId,
            EventCancelled = element.EventCancelled,
            Favorite = element.Favorite,
            OrderStateId = element.OrderStateId,
            EventId = element.EventId,
            FirstName = element.FirstName,
            LastName = element.LastName,
            Email = element.Email,
            City = element.City,
            State = element.State,
            OrderDateTime = element.OrderDateTime,
            OrderStateName = element.OrderStateName,
            UserId = element.UserId
        }).ToList();

      if (model.PerPage > 0)
      {
        model.Page = (model.PerPage * model.Page) > orders.Count() ? orders.Count() / model.PerPage : model.Page;
        olist.Orders.AddRange(orders.Skip(model.PerPage * model.Page).Take(model.PerPage));
      }
      else
        olist.Orders.AddRange(orders);

      return PartialView("_PurchasedTicketList", olist);
    }

    [HttpGet]
    public ActionResult PurchasedTicketTotalCount(OrderListRequestViewModel model)
    {
      if ((Session["UserID"] == null))
        return RedirectToAction("Login", "Home");

      var orders = _tservice.GetOrdersList(model.OrderType, String.Empty, model.Search);

      return Json(orders.Count(), JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public ActionResult PurchasedTicketDetail(string OrderId, long eventId)
    {
      if ((Session["UserID"] == null))
        return RedirectToAction("Login", "Home");

      OrderDetailsViewModel orderInfo = _tservice.GetOrderDetails(OrderId, eventId);
      return PartialView("_PurchasedTicketDetail", orderInfo);
    }

    [HttpPost]
    public string SavePurchasedTicketDetail(OrderDetailsViewModel model)
    {
      if ((Session["UserID"] == null))
        return "You can not save changes.";

      if (_tservice.SaveOrderDetails(model, Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~/"), Server.MapPath("..")))
        return "Changes saved.";
      else
        return "Changes not saved.";
    }

    [HttpPost]
    public int CancelOrder(string orderId)
    {
      if ((Session["UserID"] == null))
        return -1;

      if (_tservice.CancelOrder(orderId))
        return 1;
      else
        return 0;
    }

    [HttpPost]
    public int SaveMessage(OrganizerMessageViewModel model)
    {
      if ((Session["UserID"] == null))
        return -1;

      model.Userid = Session["UserID"].ToString();
      if (_tservice.SaveMessage(model))
        return 1;
      else
        return 0;
    }


  }
}