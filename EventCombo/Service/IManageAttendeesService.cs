﻿using EventCombo.Models;
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

    IEnumerable<EventOrderInfoViewModel> GetOrdersForSaleReport(PaymentStates state, long eventId);

    bool SendConfirmations(string orderId, string baseUrl, string filePath );

    MemoryStream GetDownloadableOrderList(PaymentStates state, long eventId, string format);

    MemoryStream GetDownloadableSaleReport(PaymentStates state, long eventId, string format);

    AddAttandeeOrder PrepareAddAttendeeOrder(long eventId, string userId);

    string CreateManualOrder(AddAttandeeOrder model, string userId);

    IEnumerable<ScheduledEmail> GetScheduledEmailList(bool IsEmailSend);

    ScheduledEmailViewModel GetScheduledEmailDetail(long scheduledEmailId);

    ScheduledEmailViewModel PrepareSendAttendeeMail(long eventId);

    bool SendAttendeeMail(ScheduledEmailViewModel scheduledEmail, string userId, string ticketbearerIds, DateTime scheduledDate);

    bool UpdateAttendeeMail(ScheduledEmailViewModel scheduledEmail);

    bool DeleteAttendeeMail(long scheduledEmailId);

    List<AttendeeViewModel> GetAttendeeList(AttendeeSearchRequestViewModel request);

    IEnumerable<SelectItemModel> GetSendToDropdownList(long eventId);
  }
}
