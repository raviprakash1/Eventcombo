using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;
using System.IO;

namespace CMS.Service
{
  public interface ITicketsService
  {
    IEnumerable<OrderMainViewModel> GetOrdersList(OrderTypes type, string userId, string searchStr, OrderSearch orderSearch);

    long GetOrdersCount(OrderTypes type, string userId);

    OrderDetailsViewModel GetOrderDetails(string orderId);

    bool SaveOrderDetails(OrderDetailsViewModel model, string baseUrl, string filePath);

    bool CancelOrder(string orderId);

    bool SaveMessage(OrganizerMessageViewModel model);

    MemoryStream GetDownloadableTicket(string orderId, string format, string filePath);
  }
}
