using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Service;
using EventCombo.DAL;
using System.IO;

namespace EventCombo.Controllers
{
  [Authorize]
  public class DownloadController : Controller
  {
    IDBAccessService _dbservice;
    ITicketsService _tservice;

    public DownloadController()
    {
      IUnitOfWorkFactory uowFactory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory());
      AutoMapper.IMapper mapper = AutomapperConfig.Config.CreateMapper();
      _dbservice = new DBAccessService(uowFactory, mapper);
      _tservice = new TicketService(uowFactory, mapper, _dbservice);
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
      
      return new FileStreamResult(mem, "application/" + format.ToLower());
    }
  }
}