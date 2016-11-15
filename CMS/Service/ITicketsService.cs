using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;
using System.IO;
using static CMS.Service.TicketService;

namespace CMS.Service
{
  public interface ITicketsService
  {
    IEnumerable<OrderMainViewModel> GetOrdersList(OrderTypes type, string userId, string searchStr);

    long GetOrdersCount(OrderTypes type, string userId);

    OrderDetailsViewModel GetOrderDetails(string orderId, long eventId);

    bool SaveOrderDetails(OrderDetailsViewModel model, string baseUrl, string filePath);

    bool CancelOrder(string orderId);

    bool SaveMessage(OrganizerMessageViewModel model);

    MemoryStream GetDownloadableTicket(string orderId, string format, string filePath);

    TicketSaleViewModel GetEventTicketSale(long eventId, FilterByOrderType filter);

    MemoryStream GetDownloadableEventTicketSale(FilterByOrderType filter, long eventId, string format);
  }
}
