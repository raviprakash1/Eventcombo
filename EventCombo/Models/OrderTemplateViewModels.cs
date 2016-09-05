using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public class OrderTemplateTypeViewModel
  {
    public long OrderTemplateTypeId { get; set; }
    public string OrderTemplateTypeName { get; set; }
  }

  public class TicketTypeViewModel
  {
    public byte TicketTypeId { get; set; }
    public string TicketType { get; set; }
  }

  public class TicketViewModel
  {
    public TicketViewModel()
    {
      TicketType = new TicketTypeViewModel();
    }

    public long T_Id { get; set; }
    public Nullable<long> E_Id { get; set; }
    public string T_name { get; set; }
    public long Qty_Available { get; set; }
    public Nullable<decimal> Price { get; set; }
    public Nullable<long> TicketTypeID { get; set; }
    public string T_Sold { get; set; }
    public string Registration_Recorded { get; set; }
    public string T_Desc { get; set; }
    public string Show_T_Desc { get; set; }
    public string Fees_Type { get; set; }
    public Nullable<System.DateTime> Sale_Start_Date { get; set; }
    public string Sale_Start_Time { get; set; }
    public Nullable<System.DateTime> Sale_End_Date { get; set; }
    public string Sale_End_Time { get; set; }
    public string Hide_Ticket { get; set; }
    public string Auto_Hide_Sche { get; set; }
    public Nullable<System.DateTime> Hide_Untill_Date { get; set; }
    public string Hide_Untill_Time { get; set; }
    public Nullable<System.DateTime> Hide_After_Date { get; set; }
    public string Hide_After_Time { get; set; }
    public Nullable<decimal> Min_T_Qty { get; set; }
    public Nullable<decimal> Max_T_Qty { get; set; }
    public string T_Disable { get; set; }
    public string T_Mark_SoldOut { get; set; }
    public Nullable<long> T_Sold_Qty { get; set; }
    public Nullable<byte> T_order { get; set; }
    public Nullable<decimal> EC_Fee { get; set; }
    public Nullable<decimal> Customer_Fee { get; set; }
    public string T_Displayremaining { get; set; }
    public string T_AutoSechduleType { get; set; }
    public Nullable<decimal> Additional_Fee { get; set; }
    public Nullable<decimal> T_Discount { get; set; }
    public Nullable<decimal> TotalPrice { get; set; }
    public string T_Customize { get; set; }
    public Nullable<decimal> T_Ecpercent { get; set; }
    public Nullable<decimal> T_EcAmount { get; set; }
    public long InternalId { get; set; }
    public long PurchasedQuantity { get; set; }

    public TicketTypeViewModel TicketType { get; private set; }
  }

  public class OrderTemplateTicketViewModel
  {
    public long OrderTemplateTicketId { get; set; }

    private TicketViewModel _collectTicket = new TicketViewModel();
    public TicketViewModel CollectTicket
    {
      get { return _collectTicket; }
      set { _collectTicket = value; }
    }
    public long TicketId { get; set; }

    public bool CollectInformation { get; set; }
  }

  public class QuestionTypeGroupViewModel
  {
    public long QuestionTypeGroupId { get; set; }
    public string GroupName { get; set; }
    public long SortNum { get; set; }
  }

  public class QuestionTypeViewModel
  {
    public long QuestionTypeId { get; set; }

    private QuestionTypeGroupViewModel _group = new QuestionTypeGroupViewModel() { QuestionTypeGroupId = 0, GroupName = "", SortNum = 0 };
    public QuestionTypeGroupViewModel Group
    {
      get { return _group; }
      internal set { _group = value; }
    }
    public long QuestionTypeGroupId { get; set; }

    public string Title { get; set; }
    public bool Visible { get; set; }
    public bool Include { get; set; }
    public bool Require { get; set; }
    public bool MustInclude { get; set; }
    public bool MustRequire { get; set; }
    public long SortNum { get; set; }
  }

  public partial class OrderTemplateQuestionTicketViewModel
  {
    public long OrderTemplateQuestionTicketId { get; set; }
    public bool CollectInformation { get; set; }

    private TicketViewModel _collectTicket = new TicketViewModel();
    public TicketViewModel CollectTicket
    {
      get { return _collectTicket; }
      set { _collectTicket = value; }
    }
    public long TicketId { get; set; }
  }

  public partial class OrderTemplateQuestionVariantViewModel
  {
    public long OrderTemplateQuestionVariantId { get; set; }
    public string VariantText { get; set; }
    public long Quantity { get; set; }

    private List<OrderTemplateQuestionViewModel> _subquestions = new List<OrderTemplateQuestionViewModel>();
    public List<OrderTemplateQuestionViewModel> Subquestions
    {
      get { return _subquestions; }
      private set { _subquestions = value; }
    }
  }

  public partial class ControlTypeViewModel
  {
    public long ControlTypeId { get; set; }
    public string ControlTypeName { get; set; }
    public int SortNum { get; set; }
    public string JSFunction { get; set; }
  }

  public class OrderTemplateQuestionViewModel
  {
    public long OrderTemplateQuestionId { get; set; }

    private QuestionTypeViewModel _question = new QuestionTypeViewModel();
    public QuestionTypeViewModel Question
    {
      get { return _question; }
      internal set { _question = value; }
    }
    public long QuestionTypeId { get; set; }

    public bool Include { get; set; }
    public bool Require { get; set; }
    public string QuestionText { get; set; }
    public int SortNum { get; set; }
    public bool ShowForTickets { get; set; }
    public bool LimitQuantity { get; set; }
    public bool ShowAnswer { get; set; }
    public bool EnableSubquestions { get; set; }
    public long ParentId { get; set; }

    private ControlTypeViewModel _control = new ControlTypeViewModel();
    public ControlTypeViewModel Control
    {
      get { return _control; }
      private set { _control = value; }
    }
    public long ControlTypeId { get; set; }

    private List<OrderTemplateQuestionTicketViewModel> _questionTickets = new List<OrderTemplateQuestionTicketViewModel>();
    public List<OrderTemplateQuestionTicketViewModel> QuestionTickets
    {
      get { return _questionTickets; }
      private set { _questionTickets = value; }
    }

    private List<OrderTemplateQuestionVariantViewModel> _questionVariants = new List<OrderTemplateQuestionVariantViewModel>();
    public List<OrderTemplateQuestionVariantViewModel> QuestionVariants
    {
      get { return _questionVariants; }
      private set { _questionVariants = value; }
    }
  }

  public class OrderTemplateEventTypeViewModel
  {
    public long OrderTemplateEventTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }

  public class OrderTemplateReceiveByTypeViewModel
  {
    public long OrderTemplateReceiveByTypeId { get; set; }
    public bool Receive { get; set; }

    private TicketViewModel _receiveTicket = new TicketViewModel();
    public TicketViewModel ReceiveTicket
    {
      get { return _receiveTicket; }
      internal set { _receiveTicket = value; }
    }
    public long TicketId { get; set; }
  }

  public class OrderTemplateWaitlistViewModel
  {
    public long OrderTemplateWaitlistId { get; set; }
    public long MaxSize { get; set; }
    public bool NameRequired { get; set; }
    public bool EmailRequired { get; set; }
    public bool PhoneRequired { get; set; }
    public DateTime RespondTime { get; set; }
    public string ResponseMessage { get; set; }
    public string ReleaseMessage { get; set; }

    private TicketViewModel _waitlistTicket = new TicketViewModel();
    public TicketViewModel WaitlistTicket
    {
      get { return _waitlistTicket; }
      internal set { _waitlistTicket = value; }
    }
    public long TicketId { get; set; }

    public int RespondDays { get; set; }
    public int RespondHours { get; set; }
    public int RespondMinutes { get; set; }
  }

  public partial class LanguageViewModel
  {
    public long LanguageId { get; set; }
    public string LanguageName { get; set; }
  }

  public partial class OrderTemplateGroupTicketViewModel
  {
    public long OrderTemplateGroupTicketId { get; set; }
    public bool EnableRegistration { get; set; }
    public bool GroupOnly { get; set; }
    public bool DontDisplay { get; set; }

    private TicketViewModel _groupTicket = new TicketViewModel();
    public TicketViewModel GroupTicket
    {
      get { return _groupTicket; }
      private set { _groupTicket = value; }
    }
    public long TicketId { get; set; }
  }

  public partial class OrderTemplateGroupTypeViewModel
  {
    public long OrderTemplateGroupTypeId { get; set; }
    public string GroupName { get; set; }
  }

  public class OrderTemplateViewModel
  {
    public long OrderTemplateId { get; set; }
    public long EventId { get; set; }
    public long OrderTemplateTypeId { get; set; }
    public string Title { get; set; }
    public string Instruction { get; set; }
    public string UserID { get; set; }
    public int TimeLimit { get; set; }
    public string AfterMessage { get; set; }
    public bool AllowCallPickup { get; set; }
    public bool AllowEdit { get; set; }
    public bool AcceptRefund { get; set; }
    public string ConfirmationMessage { get; set; }
    public string ReplyEmail { get; set; }
    public string TicketMessage { get; set; }
    public bool CustomIncludeSettings { get; set; }
    public bool IncludePrintableTickets { get; set; }
    public int LanguageId { get; set; }
    public bool EnableWaitlist { get; set; }
    public long OrderTemplateEventTypeId { get; set; }
    public long OrderTemplateGroupTypeId { get; set; }
    public long GroupMaxAttendees { get; set; }
    public string GroupPageHeadline { get; set; }
    public string GroupPageDescription { get; set; }
    public bool GroupAllowPassword { get; set; }
    public bool GroupRequirePassword { get; set; }
    public bool GroupAllowSetTime { get; set; }
    public bool GroupRequireSetTime { get; set; }
    public bool GroupAskIndividualTime { get; set; }
    public Nullable<System.DateTime> GroupStartTime { get; set; }
    public Nullable<System.DateTime> GroupEndTime { get; set; }
    public int GroupMinutesBetween { get; set; }

    private List<OrderTemplateTicketViewModel> _collectTickets = new List<OrderTemplateTicketViewModel>();
    public List<OrderTemplateTicketViewModel> CollectTickets
    {
      get { return _collectTickets; }
      internal set { _collectTickets = value; }
    }

    private List<OrderTemplateQuestionViewModel> _questions = new List<OrderTemplateQuestionViewModel>();
    public List<OrderTemplateQuestionViewModel> Questions
    {
      get { return _questions; }
      internal set { _questions = value; }
    }

    private List<OrderTemplateQuestionViewModel> _userQuestions = new List<OrderTemplateQuestionViewModel>();
    public List<OrderTemplateQuestionViewModel> UserQuestions
    {
      get { return _userQuestions; }
      internal set { _userQuestions = value; }
    }

    private List<OrderTemplateReceiveByTypeViewModel> _receiveByTypes = new List<OrderTemplateReceiveByTypeViewModel>();
    public List<OrderTemplateReceiveByTypeViewModel> ReceiveByTypes
    {
      get { return _receiveByTypes; }
      internal set { _receiveByTypes = value; }
    }

    private OrderTemplateWaitlistViewModel _waitlist = new OrderTemplateWaitlistViewModel();
    public OrderTemplateWaitlistViewModel Waitlist
    {
      get { return _waitlist; }
      internal set { _waitlist = value; }
    }

    private List<OrderTemplateGroupTicketViewModel> _groupTickets = new List<OrderTemplateGroupTicketViewModel>();
    public List<OrderTemplateGroupTicketViewModel> GroupTickets
    {
      get { return _groupTickets; }
      set { _groupTickets = value; }
    }
  }
}