using CMS.DAL;
using CMS.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CMS.Service.TicketService;

namespace CMS.Controllers
{
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
        public FileStreamResult EventTicketTotalSale(long eventId, string format)
        {
            if ((Session["UserID"] == null))
                return null;

            MemoryStream mem = _tservice.GetDownloadableEventTicketSale(FilterByOrderType.Regular, eventId, format);

            return new FileStreamResult(mem, "application/" + format.ToLower()) { FileDownloadName = "Ticket_Breakdown_" + eventId + "." + format };
        }
    }
}