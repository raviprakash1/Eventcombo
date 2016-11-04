using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public enum PaymentStates { Total, Completed, Pending }
  public enum EventOrderSortBy { Date, Price, PricePaid, PriceNet, Buyer, TicketName, CustomerEmail, Order, Quantity, PaymentState, Address, DateOrder }


  public class ManageAttendeesOrdersViewModel
  {
    public long EventId { get; set; }
    public string EventTitle { get; set; }

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
    public long OId { get; set; }
    public string OrderId { get; set; }
    public string BuyerName { get; set; }
    public string TicketName { get; set; }
    public string BuyerEmail { get; set; }        
    public string CustomerEmail { get; set; }
    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PricePaid { get; set; }
    public decimal PriceNet { get; set; }
    public decimal Fee { get; set; }
    public decimal MerchantFee { get; set; }
    public decimal Refunded { get; set; }
    public decimal Cancelled { get; set; }
    public DateTime Date { get; set; }
    public PaymentStates PaymentState { get; set; }
    public string Address { get; set; }
    public string PromoCode { get; set; }
    public string MailTickets { get; set; }   
    private List<Event_VariableDesc> _variableChages = new List<Event_VariableDesc>();     
    public List<Event_VariableDesc> VariableChages
    {
        get { return _variableChages; }
        set { _variableChages = value; }
    }
    public string VariableIds { get; set; }
    public string PhoneNumber { get; set; }
  }

  public class OrderSummaryViewModel
  {
    public long OId { get; set; }
    public string OrderId { get; set; }
    public long EventId { get; set; }
    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PriceNet { get; set; }
    public decimal Discount { get; set; }
    public decimal Fee { get; set; }
    public decimal EventComboFee { get; set; }
    public decimal MerchantFee { get; set; }
    public decimal CustomerFee { get; set; }
    public decimal VarChargesAmount { get; set; }
    public decimal Refunded { get; set; }
    public decimal Cancelled { get; set; }
    public decimal PromoCodeAmount { get; set; }
    public DateTime Date { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsRefunded { get; set; }
    public bool IsManualOrder { get; set; }
  }

  public class EventSummaryViewModel
  {
    public long EventId { get; set; }
    public long OrderQuantity { get; set; }
    public long TicketQuantity { get; set; }
    public decimal Price { get; set; }
    public decimal PriceNet { get; set; }
    public decimal Fee { get; set; }
    public decimal CustomerFee { get; set; }
    public decimal VarChargesAmount { get; set; }
    public decimal Refunded { get; set; }
    public decimal Cancelled { get; set; }
  }

  public class EventOrderDetailViewModel
  {
    public long OId { get; set; }
    public string OrderId { get; set; }
    public string Payment { get; set; }
    public string Email { get; set; }
    public string EventDate { get; set; }
    public string EventLocation { get; set; }
    public string TicketNames { get; set; }
    public bool SendEmail { get; set; }
    public decimal OrderTotalAmount { get; set; }

    private List<AttendeeViewModel> _attendees = new List<AttendeeViewModel>();
    public List<AttendeeViewModel> Attendees
    {
      get { return _attendees; }
      private set { _attendees = value; }
    }

    private List<TicketAttendeeViewModel> _ticketAttendees = new List<TicketAttendeeViewModel>();
    public List<TicketAttendeeViewModel> TicketAttendees
    {
        get { return _ticketAttendees; }
        set { _ticketAttendees = value; }
    }

    private List<Event_VariableDesc> _variableChages = new List<Event_VariableDesc>();     
    public List<Event_VariableDesc> VariableChages
    {
        get { return _variableChages; }
        set { _variableChages = value; }
    }
  }

  public class TicketAttendeeViewModel
  {
      public long TicketId { get; set; }
      public string TicketName { get; set; }
      public DateTime StartDate { get; set; }
      public long TicketTypeId { get; set; }
      public string TicketTypeName { get; set; }
      public string AttendeeName { get; set; }
      public string AttendeeEmail { get; set; }
      public string AttendeePhone { get; set; }
      public string Address { get; set; }
      public long Quantity { get; set; }
      public decimal Price { get; set; }
      public string PriceStr { get; set; }
      public string PromoCode { get; set; }
      public decimal PromoCodePrice { get; set; }
  }

  public class EventOrdersListRequestViewModel
  {
    public EventOrdersListRequestViewModel()
    {
      EventId = 0;
      SortBy = EventOrderSortBy.DateOrder;
      PaymentState = PaymentStates.Total;
      PerPage = 20;
      Page = 0;
      SortDesc = true;
      DateTime now = DateTime.Today;
      now = now.AddYears(-1);
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
    public string EventTitle { get; set; }
  }

  public class PaymentTypeViewModel
  {
    public byte PaymentTypeId { get; set; }
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

    private List<AttendeeViewModel> _attendees = new List<AttendeeViewModel>();
    public List<AttendeeViewModel> Attendees
    {
      get { return _attendees; }
      private set { _attendees = value; }
    }

    public PaymentTypeViewModel PaymentType { get; set; }
    public string Note { get; set; }
    public long EventId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
  }
    public class AttendeeSearchRequestViewModel
    {
        public AttendeeSearchRequestViewModel()
        {
            EventId = 0;
            Name = "";
            Email = "";
        }
        public long EventId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class ScheduledEmailViewModel
    {
        public ScheduledEmailViewModel()
        {
            this.AttendeeEmails = new HashSet<AttendeeEmail>();
        }

        public long ScheduledEmailId { get; set; }
        public string UserId { get; set; }
        public byte EmailTypeId { get; set; }
        [Display(Name = "From")]
        public string SendFrom { get; set; }
        [Required(ErrorMessage ="Select atleast one attendee")]
        [Display(Name = "To")]
        public string SendTo { get; set; }
        public IEnumerable<SelectItemModel> SendTos { get; set; }
        [Required(ErrorMessage = "Reply To can not be blank")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Please enter valid email")]
        [Display(Name = "Reply To")]
        public string ReplyTo { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        [Required(ErrorMessage ="Subject can not be blank")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage ="Message can not be blank")]
        [DataType(DataType.MultilineText)]
        [System.Web.Mvc.AllowHtml]
        [Display(Name = "Message")]
        public string Body { get; set; }
        [Required(ErrorMessage = "Scheduled Date can not be blank")]
        public System.DateTime ScheduledDate { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public bool IsEmailSend { get; set; }
        public string TicketbearerIds { get; set; }
        [Display(Name = "Select a Date")]
        public System.DateTime RegisteredDate { get; set; }
        public string BeforeEvent_Days { get; set; }
        public string BeforeEvent_Hours { get; set; }
        public string BeforeEvent_Minutes { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<AttendeeEmail> AttendeeEmails { get; set; }
        public virtual EmailType EmailType { get; set; }
    }

    public class SelectItemModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class AttendeeTicketTypeViewModel
    {
        public AttendeeTicketTypeViewModel()
        {
            TicketTypeId = 0;
            TicketType = "";
            Price = 0;
            Sold = 0;
            AttendeeCount = 0;
        }
        public long TicketTypeId { get; set; }
        public string TicketType { get; set; }
        public decimal Price { get; set; }
        public decimal Sold { get; set; }
        public int AttendeeCount { get; set; }
    }

    public class BadgesViewModel
    {
        public long EventId { get; set; }
        public string AttendeeSelect { get; set; }
        public string TicketbearerIds { get; set; }
        public string SortBy { get; set; }
        public string BadgeStyle { get; set; }
        private List<BadgesLayout> _BadgesLayouts = new List<BadgesLayout>();
        public List<BadgesLayout> BadgesLayouts
        {
            get { return _BadgesLayouts; }
            set { _BadgesLayouts = value; }
        }
    }

    public class BadgesLayout
    {
        public int LineNumber { get; set; }
        public string LineText { get; set; }
        public string Font { get; set; }
        public int FontSize { get; set; }
        public string Align { get; set; }
    }

    public class CheckinViewModel
    {
        public long TicketbearerId { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TicketType { get; set; }
        public bool CheckinStatus { get; set; }
    }

    public class TicketAttendeeWithSingleQty
    {
        public string TicketAttendeeAutoId { get; set; }
        public long TicketAttendeeId { get; set; }
        public long TicketBearerId { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long PurchasedTicketId { get; set; }
    }
}