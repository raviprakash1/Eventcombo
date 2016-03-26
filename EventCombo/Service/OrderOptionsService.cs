using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;
using AutoMapper;


namespace EventCombo.Service
{
  public class OrderOptionsService : IOrderOptionsService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;

    private static DateTime startDateTime = DateTime.ParseExact("01-01-2000", "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);

    public OrderOptionsService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
    }

    public OrderTemplateViewModel GetOrderTemplateViewModel(long eventId)
    {
      OrderTemplateViewModel ot = new OrderTemplateViewModel();
      OrderTemplate otDB = GetOrderTemplate(eventId);

      if (otDB == null)
        return null;

      LoadOrderTemplateViewModel(otDB, ot);
      return ot;
    }

    private void LoadOrderTemplateViewModel(OrderTemplate otDB, OrderTemplateViewModel ot)
    {
      _mapper.Map(otDB, ot);

      ot.CollectTickets.Clear();
      foreach (var otTicket in otDB.OrderTemplateTickets)
      {
        var ttVM = _mapper.Map<OrderTemplateTicketViewModel>(otTicket);
        _mapper.Map(otTicket.Ticket, ttVM.CollectTicket);
        ot.CollectTickets.Add(ttVM);
      }

      ot.ReceiveByTypes.Clear();
      foreach (var receive in otDB.OrderTemplateReceiveByTypes)
      {
        var trcv = _mapper.Map<OrderTemplateReceiveByTypeViewModel>(receive);
        _mapper.Map(receive.Ticket, trcv.ReceiveTicket);
        ot.ReceiveByTypes.Add(trcv);
      }

      ot.Questions.Clear();
      foreach (var otQuestion in otDB.OrderTemplateQuestions.OrderBy(q => q.QuestionType.SortNum))
      {
        var qvm = _mapper.Map<OrderTemplateQuestionViewModel>(otQuestion);
        _mapper.Map(otQuestion.QuestionType, qvm.Question);
        _mapper.Map(otQuestion.QuestionType.QuestionTypeGroup, qvm.Question.Group);
        ot.Questions.Add(qvm);
      }

      var waitlist = otDB.OrderTemplateWaitlists.OrderBy(w => w.TicketId).FirstOrDefault();
      if (waitlist != null)
      {
        ot.Waitlist = _mapper.Map<OrderTemplateWaitlistViewModel>(waitlist);
        ot.Waitlist.WaitlistTicket = _mapper.Map<TicketViewModel>(waitlist.Ticket);
        ot.Waitlist.RespondDays = (ot.Waitlist.RespondTime - startDateTime).Days;
        ot.Waitlist.RespondHours = (ot.Waitlist.RespondTime.AddDays(ot.Waitlist.RespondDays) - startDateTime).Hours;
        ot.Waitlist.RespondMinutes = (ot.Waitlist.RespondTime.AddDays(ot.Waitlist.RespondDays).AddHours(ot.Waitlist.RespondHours) - startDateTime).Minutes;
      }
    }

    private OrderTemplate GetOrderTemplate(long eventId)
    {
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<OrderTemplate> otRepository = new GenericRepository<OrderTemplate>(_factory.ContextFactory);
          OrderTemplate ot = otRepository.Get(filter: (e => e.EventID == eventId)).SingleOrDefault();
          if (ot == null)
          {
            ot = new OrderTemplate();
            IRepository<Event> eventRepository = new GenericRepository<Event>(_factory.ContextFactory);
            IRepository<OrderTemplateType> orderTemplateTypesRepository = new GenericRepository<OrderTemplateType>(_factory.ContextFactory);
            IRepository<OrderTemplateEventType> orderTemplateEventTypesRepository = new GenericRepository<OrderTemplateEventType>(_factory.ContextFactory);
            IRepository<AspNetUser> aspNetUsersRepository = new GenericRepository<AspNetUser>(_factory.ContextFactory);

            Event ev = eventRepository.Get(filter: (e => e.EventID == eventId)).SingleOrDefault();
            if (ev == null)
              return null;
            ot.EventID = eventId;
            ot.Title = "Registration Information";
            ot.TimeLimit = 10;
            ot.AcceptRefund = true;
            ot.AllowCallPickup = false;
            ot.AllowEdit = true;
            ot.AfterMessage = "";
            ot.ConfirmationMessage = "";
            ot.CustomIncludeSettings = false;
            ot.IncludePrintableTickets = true;
            ot.Instruction = "";
            ot.OrderTemplateTypeId = orderTemplateTypesRepository.Get(orderBy:(query => query.OrderBy(q => q.OrderTemplateTypeId))).First().OrderTemplateTypeId;
            ot.OrderTemplateEventTypeID = orderTemplateEventTypesRepository.Get(orderBy: (query => query.OrderBy(q => q.OrderTemplateEventTypeId))).First().OrderTemplateEventTypeId;
            ot.ReplyEmail = aspNetUsersRepository.Get(filter: (u => u.Id == ev.UserID)).FirstOrDefault().Email;
            ot.TicketMessage = "";
            ot.EnableWaitlist = false;

            IRepository<Ticket> ticketRepository = new GenericRepository<Ticket>(_factory.ContextFactory);
            var tickets = ticketRepository.Get(filter: (t => t.E_Id == eventId), orderBy: (query => query.OrderBy(f => f.T_Id)));
            foreach (var ticket in tickets)
            {
              ot.OrderTemplateTickets.Add(new OrderTemplateTicket() { TicketId = ticket.T_Id, CollectInformation = false, OrderTemplateTicketId = 0 });
              ot.OrderTemplateReceiveByTypes.Add(new OrderTemplateReceiveByType() { TicketId = ticket.T_Id, Receive = false, OrderTemplateReceiveByTypeId = 0 });
            }

            IRepository<QuestionType> questionTypeRepository = new GenericRepository<QuestionType>(_factory.ContextFactory);
            var questions = questionTypeRepository.Get(filter:(qt => qt.Visible == true), orderBy:(query => query.OrderBy(e => e.SortNum)));
            foreach (var question in questions)
              ot.OrderTemplateQuestions.Add(new OrderTemplateQuestion() { QuestionTypeId = question.QuestionTypeId, Include = question.Include, Require = question.Require });

            ot.OrderTemplateWaitlists.Add(new OrderTemplateWaitlist()
              {
                NameRequired = true,
                EmailRequired = true,
                PhoneRequired = false,
                RespondTime = startDateTime.AddDays(1),
                ResponseMessage = "If a ticket becomes available, you will be contacted automatically with further instructions on how to purchase your ticket. No further action is required.",
                ReleaseMessage = "Congratulations! A spot has opened up for you at this event. You have a limited time to sign up. Please follow the links below to register for the event. If you have any questions, please contact the organizer: " + ot.ReplyEmail
              });

            otRepository.Insert(ot);
          }

          uow.Commit();
          return ot;
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception in the CreateNewOrderTemplate. Transaction rolled back.", ex);
        }
      }
    }

    public bool SaveOrderTemplateForm(OrderTemplateViewModel otVM)
    {
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<OrderTemplate> orderTemplateRepository = new GenericRepository<OrderTemplate>(_factory.ContextFactory);
          OrderTemplate otDB = orderTemplateRepository.Get(filter: (ot => ot.OrderTemplateId == otVM.OrderTemplateId)).SingleOrDefault();
          if (otDB != null)
          {
            otDB.OrderTemplateTypeId = otVM.OrderTemplateTypeId;
            otDB.Title = otVM.Title;
            otDB.Instruction = otVM.Instruction;
            otDB.TimeLimit = otVM.TimeLimit;
            otDB.AfterMessage = otVM.AfterMessage;
            otDB.AllowCallPickup = otVM.AllowCallPickup;
            otDB.AllowEdit = otVM.AllowEdit;
            otDB.AcceptRefund = otVM.AcceptRefund;

            foreach (var ct in otVM.CollectTickets)
              otDB.OrderTemplateTickets.Where(t => t.OrderTemplateTicketId == ct.OrderTemplateTicketId).SingleOrDefault().CollectInformation = ct.CollectInformation;

            foreach (var q in otVM.Questions)
            {
              var qDB = otDB.OrderTemplateQuestions.Where(otq => otq.OrderTemplateQuestionId == q.OrderTemplateQuestionId).SingleOrDefault();
              qDB.Include = qDB.QuestionType.MustInclude || q.Include;
              qDB.Require = qDB.QuestionType.MustRequire || q.Require;
            }

            uow.Commit();
            return true;
          }
          else
            return false;
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception while tried to save data. Transaction rolled back.", ex);
        }
      }
    }

    public bool SaveOrderTemplateConfirmation(OrderTemplateViewModel otVM)
    {
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<OrderTemplate> orderTemplateRepository = new GenericRepository<OrderTemplate>(_factory.ContextFactory);
          OrderTemplate otDB = orderTemplateRepository.Get(filter: (ot => ot.OrderTemplateId == otVM.OrderTemplateId)).SingleOrDefault();
          if (otDB != null)
          {
            otDB.CustomIncludeSettings = otVM.CustomIncludeSettings;
            otDB.ConfirmationMessage = otVM.ConfirmationMessage;
            otDB.ReplyEmail = otVM.ReplyEmail;
            otDB.TicketMessage = otVM.TicketMessage;

            foreach (var rt in otVM.ReceiveByTypes)
              otDB.OrderTemplateReceiveByTypes.Where(t => t.OrderTemplateReceiveByTypeId == rt.OrderTemplateReceiveByTypeId).SingleOrDefault().Receive = rt.Receive;

            uow.Commit();
            return true;
          }
          else
            return false;
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception while tried to save data. Transaction rolled back.", ex);
        }
      }
    }

    public bool SaveOrderTemplateEventType(OrderTemplateViewModel otVM)
    {
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<OrderTemplate> orderTemplateRepository = new GenericRepository<OrderTemplate>(_factory.ContextFactory);
          OrderTemplate otDB = orderTemplateRepository.Get(filter: (ot => ot.OrderTemplateId == otVM.OrderTemplateId)).SingleOrDefault();
          if (otDB != null)
          {
            otDB.OrderTemplateEventTypeID = otVM.OrderTemplateEventTypeId;
            otDB.LanguageId = otVM.LanguageId;

            uow.Commit();
            return true;
          }
          else
            return false;
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception while tried to save data. Transaction rolled back.", ex);
        }
      }
    }

    public bool SaveOrderTemplateWaitlist(OrderTemplateViewModel otVM)
    {
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<OrderTemplate> orderTemplateRepository = new GenericRepository<OrderTemplate>(_factory.ContextFactory);
          OrderTemplate otDB = orderTemplateRepository.Get(filter: (ot => ot.OrderTemplateId == otVM.OrderTemplateId)).SingleOrDefault();
          if (otDB != null)
          {
            otVM.Waitlist.RespondTime = startDateTime.AddDays(otVM.Waitlist.RespondDays).AddHours(otVM.Waitlist.RespondHours).AddMinutes(otVM.Waitlist.RespondMinutes);
            otVM.Waitlist.NameRequired = true;
            otVM.Waitlist.EmailRequired = true;
            otDB.EnableWaitlist = otVM.EnableWaitlist;
            foreach (var wlDB in otDB.OrderTemplateWaitlists)
            {
              otVM.Waitlist.OrderTemplateWaitlistId = wlDB.OrderTemplateWaitlistId;
              _mapper.Map(otVM.Waitlist, wlDB);
            }

            uow.Commit();
            return true;
          }
          else
            return false;
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception while tried to save data. Transaction rolled back.", ex);
        }
      }
    }


    public IEnumerable<TicketViewModel> GetTicketsViewModel(long eventId)
    {
      IRepository<Ticket> ticketRepository = new GenericRepository<Ticket>(_factory.ContextFactory);
      return _mapper.Map<IEnumerable<TicketViewModel>>(ticketRepository.Get(filter: (t => t.E_Id == eventId), orderBy: query => query.OrderBy(t => t.T_Id)).ToList());
    }

    public IEnumerable<OrderTemplateTypeViewModel> GetOrderTemplateTypesViewModel()
    {
      IRepository<OrderTemplateType> orderTemplateTypeRepository = new GenericRepository<OrderTemplateType>(_factory.ContextFactory);
      return _mapper.Map<IEnumerable<OrderTemplateTypeViewModel>>(orderTemplateTypeRepository.Get(orderBy: query => query.OrderBy(ott => ott.OrderTemplateTypeId)).ToList());
    }

    public IEnumerable<OrderTemplateEventTypeViewModel> GetOrderTemplateEventTypesViewModel()
    {
      IRepository<OrderTemplateEventType> orderTemplateEventTypeRepository = new GenericRepository<OrderTemplateEventType>(_factory.ContextFactory);
      return _mapper.Map<IEnumerable<OrderTemplateEventTypeViewModel>>(orderTemplateEventTypeRepository.Get(orderBy: query => query.OrderBy(ott => ott.OrderTemplateEventTypeId)).ToList());
    }
  }
}