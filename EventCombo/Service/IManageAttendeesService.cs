using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EventCombo.Service
{
  public interface IManageAttendeesService
  {
    ManageAttendeesOrdersViewModel GetEventOrdersSummary(long eventId);

    IEnumerable<EventOrderInfoViewModel> GetOrdersForEvent(PaymentStates state, long eventId);

    EventOrderDetailViewModel GetEventOrderDetail(string orderId);

    EventOrderDetailViewModel GetOrderDetails(string orderId, long eventId);

    IEnumerable<EventOrderInfoViewModel> GetOrdersForSaleReport(PaymentStates state, long eventId);

    bool SendConfirmations(string orderId, string baseUrl, string filePath, ControllerContext context, bool IsManualOrder = false);

    MemoryStream GetDownloadableOrderList(PaymentStates state, long eventId, string format);

    MemoryStream GetDownloadableSaleReport(PaymentStates state, long eventId, string format);

    AddAttandeeOrder PrepareAddAttendeeOrder(long eventId, string userId);

    string CreateManualOrder(AddAttandeeOrder model, string userId);

    IEnumerable<ScheduledEmail> GetScheduledEmailList(long eventId, bool IsEmailSend);

    ScheduledEmailViewModel GetScheduledEmailDetail(long scheduledEmailId);

    ScheduledEmailViewModel PrepareSendAttendeeMail(long eventId);

    bool SendAttendeeMail(long eventId, ScheduledEmailViewModel scheduledEmail, string userId, string ticketbearerIds, DateTime scheduledDate);

    bool UpdateAttendeeMail(ScheduledEmailViewModel scheduledEmail);

    bool DeleteAttendeeMail(long scheduledEmailId);

    List<AttendeeViewModel> GetAttendeeList(AttendeeSearchRequestViewModel request);

    List<CheckinViewModel> GetAttendeeCheckinList(AttendeeSearchRequestViewModel request);

    List<AttendeeTicketTypeViewModel> GetAttendeeTicketTypeList(long eventId);

    IEnumerable<SelectItemModel> GetSelectAttendeeDropdownList(long eventId);

    IEnumerable<SelectItemModel> GetSendToDropdownList(long eventId);

    MemoryStream GetDownloadableGuestList(string sortBy, string ticketTypeIds, string barcode, long eventId, string format);

    MemoryStream GetBadgesPreview(long eventId, string format, string UserID);

    string GetBadgesPreviewPath(BadgesViewModel badgesViewModel, string format, string UserID);

    string GetBadgesListPath(BadgesViewModel badgesViewModel, string format, string UserID);

    MemoryStream GetBadgesList(long eventId, string format, string UserID);

    MemoryStream GetDownloadableManualOrderList(PaymentStates state, long eventId, string format);

    IEnumerable<EventOrderInfoViewModel> GetManualOrdersForEvent(PaymentStates state, long eventId);

    string GetEventTitle(long eventId);

    bool SaveOrderDetails(EventOrderDetailViewModel model, string userId, string baseUrl, string filePath);
  }
}
