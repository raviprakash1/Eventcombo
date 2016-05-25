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
      _maservice = new ManageAttendeesService(uowFactory, mapper, _dbservice);
    }

    [HttpGet]
    public FileStreamResult Ticket(string OrderId, string format)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetOrderAccess(OrderId, userId) == AccessLevel.Public)
        return null;

      MemoryStream mem = _tservice.GetDownloadableTicket(OrderId, format, Server.MapPath(".."));

      return new FileStreamResult(mem, "application/" + format.ToLower()) { FileDownloadName = "Ticket_" + OrderId + "." + format };
    }

    [HttpGet]
    public FileStreamResult OrderList(PaymentStates state, long eventId, string format)
    {
      if (Session["AppId"] == null)
        return null;

      string userId = Session["AppId"].ToString();
      if (_dbservice.GetEventAccess(eventId, userId) == AccessLevel.Public)
        return null;
      
      MemoryStream mem = _maservice.GetDownloadableOrderList(state, eventId, format);
      string appformat = "application/" + (format.ToLower() == "xls" ? "ms-excel" : format.ToLower());
      return new FileStreamResult(mem, appformat) { FileDownloadName = "OrderList_" + eventId.ToString() + "." + format };
    }
  }
}