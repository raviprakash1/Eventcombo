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

  public class TicketViewModel
  {
    public long T_Id { get; set; }
    public Nullable<long> E_Id { get; set; }
    public string T_name { get; set; }
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

  public class OrderTemplateViewModel
  {
    public long OrderTemplateId { get; set; }

    public long EventId { get; set; }

    public long OrderTemplateTypeId { get; set; }

    public string Title { get; set; }

    public string Instruction { get; set; }

    public string UserID { get; set; }

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

    public long OrderTemplateEventTypeId { get; set; }

    private List<OrderTemplateReceiveByTypeViewModel> _receiveByTypes = new List<OrderTemplateReceiveByTypeViewModel>();
    public List<OrderTemplateReceiveByTypeViewModel> ReceiveByTypes
    {
      get { return _receiveByTypes; }
      internal set { _receiveByTypes = value; }
    }

    public bool EnableWaitlist { get; set; }

    private OrderTemplateWaitlistViewModel _waitlist = new OrderTemplateWaitlistViewModel();
    public OrderTemplateWaitlistViewModel Waitlist
    {
      get { return _waitlist; }
      internal set { _waitlist = value; }
    }

    public int LanguageId { get; set; }

  }
}