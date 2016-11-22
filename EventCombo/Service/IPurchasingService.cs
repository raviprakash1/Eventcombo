using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;
using EventCombo.DAL;

namespace EventCombo.Service
{
  public interface IPurchasingService
  {
    TicketLockResult CanLock(TicketLockRequest req);
    TicketLockResult TryLockTickets(TicketLockRequest req);
    bool DeleteTicketLock(string lockId, string ip);
    bool DeleteTicketLock(string lockId, string ip, IUnitOfWork uow);
    EventPurchaseInfoViewModel GetEventPurchaseInfo(string lockId, string userId, string ip);
    PurchaseResult SavePurchaseInfo(EventPurchaseInfoViewModel model, string userId, string ip);
    void CompletePayPalPayment(string orderId, string paymentId, string tokenId, string payerId);
    OrderConfirmationViewModel GetOrderConfirmationInfo(string orderId, string userId);
    void GenerateTicketDetails(string orderId, IUnitOfWork uow);
    EventDatesInfo GetEventDatesInfo(Event ev);
    PromoCodeResponseViewModel GetPromoCode(long eventId, string promocode);
  }
}
