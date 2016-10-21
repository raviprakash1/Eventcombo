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
    EventPurchaseInfoViewModel GetEventPurchaseInfo(string lockId, string ip); 
  }
}
