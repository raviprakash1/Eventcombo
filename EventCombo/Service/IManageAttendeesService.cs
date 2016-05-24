using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCombo.Service
{
  public interface IManageAttendeesService
  {
    ManageAttendeesOrdersViewModel GetEventOrdersSummary(long eventId);

    IEnumerable<EventOrderInfoViewModel> GetOrdersForEvent(PaymentStates state, long eventId);

    EventOrderDetailViewModel GetEventOrderDetail(string orderId);

    EventOrderDetailViewModel GetOrderDetails(string orderId);

    bool SendConfirmations(string orderId, string baseUrl);

    MemoryStream GetDownloadableOrderList(PaymentStates state, long eventId, string format);

    AddAttandeeOrder PrepareAddAttendeeOrder(long eventId);
  }
}
