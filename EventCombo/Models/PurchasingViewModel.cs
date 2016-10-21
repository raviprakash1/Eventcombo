using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public class TicketLockViewModel
  {
    public long LockTicketId { get; set; }
    public System.Guid LockOrderId { get; set; }
    public long TicketQuantityDetailId { get; set; }
    public int Quantity { get; set; }
    public decimal Donate { get; set; }
    public decimal Amount { get; set; }
    public decimal ECFeeAmount { get; set; }
    public decimal ECFeePercent { get; set; }
    public int ECFeeType { get; set; }
    public decimal CustomerFee { get; set; }
  }

  public class TicketLockRequest
  {
    public long EventId { get; set; }
    public string UserId { get; set; }
    public string IP { get; set; }
    public IEnumerable<TicketLockViewModel> Tickets { get; set; }
  }

  public class TicketLockResult
  {
    public string LockId { get; set; }
    public int LockCount { get; set; }
    public bool TicketsAvailable { get; set; }
    public string Message { get; set; }
  }

  public class TicketPurchaseInfoViewModel
  {
    public string Name { get; set; }
    public string VenueName { get; set; }
    public string DateString { get; set; }
    public decimal Price { get; set; }
    public decimal SourcePrice { get; set; }
    public decimal TotalFee { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
  }

  public class EventPurchaseInfoViewModel
  {
    public long EventId { get; set; }
    public string LockId { get; set; }
    public string EventTitle { get; set; }
    public string VenueName { get; set; }
    public string Address { get; set; }
    public string StartDate { get; set; }
  }

}