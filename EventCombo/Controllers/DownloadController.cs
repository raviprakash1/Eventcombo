using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Service;
using EventCombo.DAL;
using System.IO;
using EventCombo.Models;

namespace EventCombo.Controllers
{
  [Authorize]
  public class DownloadController : Controller
  {
    IDBAccessService _dbservice;
    ITicketsService _tservice;
    IManageAttendeesService _maservice;

    public DownloadController()
    {
      IUnitOfWorkFactory uowFactory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory());
      AutoMapper.IMapper mapper = AutomapperConfig.Config.CreateMapper();
      _dbservice = new DBAccessService(uowFactory, mapper);
      _tservice = new TicketService(uowFactory, mapper, _dbservice);
      _maservice = new ManageAttendeesService(uowFactory, mapper, _dbservice, _tservice);
    }

    [HttpGet]
    public FileStreamResult Ticket(string OrderId, string format)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetOrderAccess(OrderId, userId) == AccessLevel.Public)
        return null;

      MemoryStream mem = _tservice.GetDownloadableTicket(OrderId, format, Server.MapPath(".."), ControllerContext);

      return new FileStreamResult(mem, "application/" + format.ToLower()) { FileDownloadName = "Ticket_" + OrderId + "." + format };
    }

    [HttpGet]
    public ActionResult OrderList(PaymentStates state, long eventId, string format)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
        return null;
      if (format.ToLower() == "html")
      {
        EventOrderInfoListViewModel model = new EventOrderInfoListViewModel();
        model.EventId = eventId;
        model.PaymentState = state;
        model.EventTitle = _maservice.GetEventTitle(eventId);
        var orders = _maservice.GetOrdersForEvent(state, eventId);
        model.Orders.AddRange(orders);
        return View("_OrderList", model);
      }
      MemoryStream mem = _maservice.GetDownloadableOrderList(state, eventId, format);
      string appformat = "application/" + (format.ToLower() == "xls" ? "ms-excel" : format.ToLower());
      return new FileStreamResult(mem, appformat) { FileDownloadName = "OrderList_" + eventId.ToString() + "." + format };
    }

    [HttpGet]
    public FileStreamResult SaleReport(PaymentStates state, long eventId, string format)
    {
        if (Session["AppId"] == null)
            return null;

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
            return null;

        MemoryStream mem = _maservice.GetDownloadableSaleReport(state, eventId, format);
        string appformat = "application/" + (format.ToLower() == "xls" ? "ms-excel" : format.ToLower());
        return new FileStreamResult(mem, appformat) { FileDownloadName = "SaleReport_" + eventId.ToString() + "." + format };
    }

    [HttpGet]
    public FileStreamResult GuestList(string sortBy, string ticketTypeIds, string barcode, long eventId, string format)
    {
        if (Session["AppId"] == null)
            return null;

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
            return null;

        MemoryStream mem = _maservice.GetDownloadableGuestList(sortBy, ticketTypeIds, barcode, eventId, format);
        string appformat = "application/" + (format.ToLower() == "xls" ? "ms-excel" : format.ToLower());
        return new FileStreamResult(mem, appformat) { FileDownloadName = "attendees_" + eventId.ToString() + "." + format };
    }

    public ActionResult AttendeeNameBadgesPreview(long eventId, string format)
    {
        if (Session["AppId"] == null)
            return null;

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
            return null;

        MemoryStream mem = _maservice.GetBadgesPreview(eventId, format, userId);
        string appformat = "application/" + (format.ToLower() == "xls" ? "ms-excel" : format.ToLower());
        return File(mem, appformat);
    }

    public ActionResult AttendeeNameBadgesList(long eventId, string format)
    {
        if (Session["AppId"] == null)
            return null;

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
            return null;

        MemoryStream mem = _maservice.GetBadgesList(eventId, format, userId);
        string appformat = "application/" + (format.ToLower() == "xls" ? "ms-excel" : format.ToLower());
        return new FileStreamResult(mem, appformat) { FileDownloadName = "attendeesbadges_" + eventId.ToString() + "." + format };
    }

    [HttpGet]
    public ActionResult ManualOrderList(PaymentStates state, long eventId, string format)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
        return null;
      if (format.ToLower() == "html")
      {
        EventOrderInfoListViewModel model = new EventOrderInfoListViewModel();
        model.EventId = eventId;
        model.PaymentState = state;
        model.EventTitle = _maservice.GetEventTitle(eventId);
        var orders = _maservice.GetManualOrdersForEvent(state, eventId);
        model.Orders.AddRange(orders);
        return View("_ManualOrderList", model);
      }
      MemoryStream mem = _maservice.GetDownloadableManualOrderList(state, eventId, format);
      string appformat = "application/" + (format.ToLower() == "xls" ? "ms-excel" : format.ToLower());
      return new FileStreamResult(mem, appformat) { FileDownloadName = "ManualOrderList_" + eventId.ToString() + "." + format };
    }

    [HttpGet]
    public FileStreamResult EventTicketTotalSale(long eventId, string format)
    {
        if (Session["AppId"] == null)
            return null;

        string userId = Session["AppId"].ToString();
        if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
            return null;

        MemoryStream mem = _tservice.GetDownloadableEventTicketSale(FilterByOrderType.Regular, eventId, format);

        return new FileStreamResult(mem, "application/" + format.ToLower()) { FileDownloadName = "Ticket_Breakdown_" + eventId + "." + format };
    }
  }
}