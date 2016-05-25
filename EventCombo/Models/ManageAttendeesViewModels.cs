using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public enum PaymentStates { Total, Completed, Pending }
  public enum EventOrderSortBy { Date, Price, Buyer, Order, Quantity, PaymentState }


  public class ManageAttendeesOrdersViewModel
  {
    public long EventId { get; set; }

    private List<EventOrdersSummuryViewModel> _ordersSummary = new List<EventOrdersSummuryViewModel>();
    public List<EventOrdersSummuryViewModel> OrdersSummary
    {
      get { return _ordersSummary; }
      private set { _ordersSummary = value; }
    }

    private EventOrdersListRequestViewModel _defaultState = new EventOrdersListRequestViewModel();
    public EventOrdersListRequestViewModel DefaultState
    {
      get { return _defaultState; }
      private set { _defaultState = value; }
    }
  }

  public class EventOrdersSummuryViewModel
  {
    public PaymentStates PaymentState { get; set; }
    public long TicketsTotal { get; set; }
    public long TicketsSold { get; set; }
    public decimal Amount { get; set; }
    public long Count { get; set; }
  }

  public class EventOrderInfoViewModel
  {
    public string OrderId { get; set; }
    public string BuyerName { get; set; }
    public string BuyerEmail { get; set; }
    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public PaymentStates PaymentState { get; set; }
  }

  public class EventOrderDetailViewModel
  {
    public string OrderId { get; set; }

    private List<AttendeeViewModel> _attendees = new List<AttendeeViewModel>();
    public List<AttendeeViewModel> Attendees
    {
      get { return _attendees; }
      private set { _attendees = value; }
    }

    public string Payment { get; set; }
    public string Email { get; set; }
  }

  public class EventOrdersListRequestViewModel
  {
    public EventOrdersListRequestViewModel()
    {
      EventId = 0;
      SortBy = EventOrderSortBy.Date;
      PaymentState = PaymentStates.Total;
      PerPage = 20;
      Page = 0;
      SortDesc = false;
      DateTime now = DateTime.Today;
      now.AddDays(DayOfWeek.Monday - now.DayOfWeek);
      DateFrom = now.Month.ToString() + "/" + now.Day.ToString() + "/" + now.Year.ToString();
    }
    public long EventId { get; set; }
    public EventOrderSortBy SortBy { get; set; }
    public PaymentStates PaymentState { get; set; }
    public int PerPage { get; set; }
    public int Page { get; set; }
    public bool SortDesc { get; set; }
    public string DateFrom { get; set; }
    public string Search { get; set; }
  }

  public class EventOrderInfoListViewModel
  {
    public long EventId { get; set; }
    public int Total { get; set; }
    public PaymentStates PaymentState { get; set; }

    private List<EventOrderInfoViewModel> _orders = new List<EventOrderInfoViewModel>();
    public List<EventOrderInfoViewModel> Orders
    {
      get { return _orders; }
      private set { _orders = value; }
    }
  }

  public class PaymentTypeViewModel
  {
    public short PaymentTypeId { get; set; }
    public string PaymentTypeName { get; set; }
    public bool Active { get; set; }
  }

  public class TicketInfoAddViewModel
  {
    public TicketViewModel Ticket { get; set; }
    public long? TicketSold { get; set; }
    public int Quantity { get; set; }
    public decimal Paid { get; set; }
  }

  public class AddAttandeeOrder
  {
    private List<TicketInfoAddViewModel> _tickets = new List<TicketInfoAddViewModel>();
    public List<TicketInfoAddViewModel> Tickets
    {
      get { return _tickets; }
      private set { _tickets = value; }
    }

    private List<PaymentTypeViewModel> _availablePT = new List<PaymentTypeViewModel>();
    public List<PaymentTypeViewModel> AvailablePT
    {
      get { return _availablePT; }
      private set { _availablePT = value; }
    }

    public PaymentTypeViewModel PaymentType { get; set; }

    public string Note { get; set; }

    public long EventId { get; set; }
  }

}