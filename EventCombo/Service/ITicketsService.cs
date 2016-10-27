using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;
using System.IO;

namespace EventCombo.Service
{
  public interface ITicketsService
  {
    IEnumerable<OrderMainViewModel> GetOrdersList(OrderTypes type, string userId);

    long GetOrdersCount(OrderTypes type, string userId);

    OrderDetailsViewModel GetOrderDetails(string orderId, string userId, long EventId);

    bool SaveOrderDetails(OrderDetailsViewModel model, string userId, string baseUrl, string filePath);

    bool CancelOrder(string orderId, string userId);

    bool SaveMessage(OrganizerMessageViewModel model);

    MemoryStream GetDownloadableTicket(string orderId, string format, string filePath, bool IsManualOrder = false);

    IEnumerable<OrderSummaryViewModel> GetEventOrdersSummaryCalculation(long eventId);
    
    EventSummaryViewModel GetEventSummaryCalculation(long eventId);

  }
}
