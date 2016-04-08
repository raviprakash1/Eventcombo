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

      ot.Waitlist = _mapper.Map<OrderTemplateWaitlistViewModel>(otDB);
      ot.Waitlist.RespondDays = (ot.Waitlist.RespondTime - startDateTime).Days;
      ot.Waitlist.RespondHours = (ot.Waitlist.RespondTime.AddDays(ot.Waitlist.RespondDays) - startDateTime).Hours;
      ot.Waitlist.RespondMinutes = (ot.Waitlist.RespondTime.AddDays(ot.Waitlist.RespondDays).AddHours(ot.Waitlist.RespondHours) - startDateTime).Minutes;
      ot.UserID = otDB.Event.UserID;

      ot.CollectTickets.Clear();
      foreach (var otTicket in otDB.OrderTemplateTickets)
      {
        var ttVM = _mapper.Map<OrderTemplateTicketViewModel>(otTicket);
        _mapper.Map(otTicket.Ticket, ttVM.CollectTicket);
        ot.CollectTickets.Add(ttVM);
      }

      ot.ReceiveByTypes.Clear();
      ot.GroupTickets.Clear();
      foreach (var otTicket in otDB.OrderTemplateTickets)
      {
        var trcv = _mapper.Map<OrderTemplateReceiveByTypeViewModel>(otTicket);
        ot.ReceiveByTypes.Add(trcv);

        var gtVM = _mapper.Map<OrderTemplateGroupTicketViewModel>(otTicket);
        _mapper.Map(otTicket.Ticket, gtVM.GroupTicket);
        ot.GroupTickets.Add(gtVM);
      }

      ot.Questions.Clear();
      foreach (var otQuestion in otDB.OrderTemplateQuestions.Where(q => q.QuestionType.Visible).OrderBy(q => q.SortNum))
      {
        var qvm = _mapper.Map<OrderTemplateQuestionViewModel>(otQuestion);
        ot.Questions.Add(qvm);
      }

      IRepository<OrderTemplateQuestion> qRepository = new GenericRepository<OrderTemplateQuestion>(_factory.ContextFactory);
      ot.UserQuestions.Clear();
      foreach (var otUserQuestion in otDB.OrderTemplateQuestions.Where(q => !q.QuestionType.Visible).OrderBy(q => q.SortNum))
      {
        var qvm = _mapper.Map<OrderTemplateQuestionViewModel>(otUserQuestion);
        _mapper.Map(otUserQuestion.ControlType, qvm.Control);
        foreach (var qVariant in otUserQuestion.OrderTemplateQuestionVariants)
        {
          var qvVM = _mapper.Map<OrderTemplateQuestionVariantViewModel>(qVariant);
          foreach (var sqDB in qRepository.Get(filter: (q => q.ParentId == qVariant.OrderTemplateQuestionVariantId)).ToList())
          {
            var sqVM = _mapper.Map<OrderTemplateQuestionViewModel>(sqDB);
            foreach (var sqvDB in sqDB.OrderTemplateQuestionVariants)
              sqVM.QuestionVariants.Add(_mapper.Map<OrderTemplateQuestionVariantViewModel>(sqvDB));
            qvVM.Subquestions.Add(sqVM);
          }
          qvm.QuestionVariants.Add(qvVM);
        }
        foreach (var qTicket in otUserQuestion.OrderTemplateQuestionTickets)
          qvm.QuestionTickets.Add(_mapper.Map<OrderTemplateQuestionTicketViewModel>(qTicket));
        ot.UserQuestions.Add(qvm);
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
            IRepository<Language> languageRepository = new GenericRepository<Language>(_factory.ContextFactory);
            IRepository<OrderTemplateGroupType> groupTypeRepository = new GenericRepository<OrderTemplateGroupType>(_factory.ContextFactory);

            Event ev = eventRepository.Get(filter: (e => e.EventID == eventId)).SingleOrDefault();
            if (ev == null)
              return null;
            ot.EventID = eventId;
            ot.Title = "";
            ot.TimeLimit = 10;
            ot.AcceptRefund = true;
            ot.AllowCallPickup = false;
            ot.AllowEdit = true;
            ot.AfterMessage = "";
            ot.ConfirmationMessage = "";
            ot.CustomIncludeSettings = false;
            ot.IncludePrintableTickets = true;
            ot.Instruction = "";
            ot.OrderTemplateTypeId = orderTemplateTypesRepository.Get(orderBy: (query => query.OrderBy(q => q.OrderTemplateTypeId))).First().OrderTemplateTypeId;
            ot.OrderTemplateEventTypeID = orderTemplateEventTypesRepository.Get(orderBy: (query => query.OrderBy(q => q.OrderTemplateEventTypeId))).First().OrderTemplateEventTypeId;
            ot.ReplyEmail = aspNetUsersRepository.Get(filter: (u => u.Id == ev.UserID)).FirstOrDefault().Email;
            ot.TicketMessage = "";
            ot.EnableWaitlist = false;
            ot.LanguageId = languageRepository.Get(orderBy: (query => query.OrderBy(q => q.LanguageId))).First().LanguageId;
            ot.OrderTemplateGroupTypeId = groupTypeRepository.Get(orderBy: (query => query.OrderBy(q => q.OrderTemplateGroupTypeId))).First().OrderTemplateGroupTypeId;
            ot.GroupAllowPassword = false;
            ot.GroupAllowSetTime = false;
            ot.GroupAskIndividualTime = false;
            ot.GroupMaxAttendees = 0;
            ot.GroupMinutesBetween = 0;
            ot.GroupPageDescription = "";
            ot.GroupPageHeadline = "";
            ot.GroupRequirePassword = false;
            ot.GroupRequireSetTime = false;
            ot.GroupStartTime = null;
            ot.GroupEndTime = null;
            ot.NameRequired = true;
            ot.EmailRequired = true;
            ot.PhoneRequired = false;
            ot.RespondTime = startDateTime.AddDays(1);
            ot.ResponseMessage = "If a ticket becomes available, you will be contacted automatically with further instructions on how to purchase your ticket. No further action is required.";
            ot.ReleaseMessage = "Congratulations! A spot has opened up for you at this event. You have a limited time to sign up. Please follow the links below to register for the event. If you have any questions, please contact the organizer: " + ot.ReplyEmail;


            IRepository<Ticket> ticketRepository = new GenericRepository<Ticket>(_factory.ContextFactory);
            var tickets = ticketRepository.Get(filter: (t => t.E_Id == eventId), orderBy: (query => query.OrderBy(f => f.T_Id)));
            foreach (var ticket in tickets)
            {
              ot.OrderTemplateTickets.Add(new OrderTemplateTicket() { TicketId = ticket.T_Id, CollectInformation = false, Receive = false, DontDisplay = false, EnableRegistration = false, GroupOnly = false });
            }

            IRepository<QuestionType> questionTypeRepository = new GenericRepository<QuestionType>(_factory.ContextFactory);
            var questions = questionTypeRepository.Get(filter: (qt => qt.Visible == true), orderBy: (query => query.OrderBy(e => e.SortNum)));
            foreach (var question in questions)
              ot.OrderTemplateQuestions.Add(new OrderTemplateQuestion() { QuestionTypeId = question.QuestionTypeId, Include = question.Include, Require = question.Require });

            otRepository.Insert(ot);
          }

          uow.Context.SaveChanges();
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

    private void DeleteQuestionVariant(OrderTemplateQuestionVariant qVariant, long OrderTemplateId)
    {
      IRepository<OrderTemplateQuestionVariant> qVariantRepository = new GenericRepository<OrderTemplateQuestionVariant>(_factory.ContextFactory);
      IRepository<OrderTemplateQuestion> qRepository = new GenericRepository<OrderTemplateQuestion>(_factory.ContextFactory);
      long parentId = qVariant.OrderTemplateQuestionVariantId;
      qVariantRepository.Delete(qVariant);
      foreach (var subQuestion in qRepository.Get(filter: (q => ((q.OrderTemplateId == OrderTemplateId) && (q.ParentId == parentId)))).ToList())
        DeleteUserQuestion(subQuestion);
    }

    private void DeleteUserQuestion(OrderTemplateQuestion userQuestion)
    {
      IRepository<OrderTemplateQuestion> qRepository = new GenericRepository<OrderTemplateQuestion>(_factory.ContextFactory);
      IRepository<OrderTemplateQuestionTicket> qTicketRepository = new GenericRepository<OrderTemplateQuestionTicket>(_factory.ContextFactory);
      foreach (var qv in userQuestion.OrderTemplateQuestionVariants.ToList())
        DeleteQuestionVariant(qv, userQuestion.OrderTemplateId);
      foreach (var qt in userQuestion.OrderTemplateQuestionTickets.ToList())
        qTicketRepository.Delete(qt);
      qRepository.Delete(userQuestion);
    }

    private OrderTemplateQuestionVariant CreateQuestionVariant(OrderTemplateQuestionVariantViewModel qvVM)
    {
      IRepository<OrderTemplateQuestionVariant> qvRepository = new GenericRepository<OrderTemplateQuestionVariant>(_factory.ContextFactory);
      IRepository<OrderTemplateQuestion> qRepository = new GenericRepository<OrderTemplateQuestion>(_factory.ContextFactory);
      var qvDB = _mapper.Map<OrderTemplateQuestionVariant>(qvVM);
      qvRepository.Insert(qvDB);
      _factory.ContextFactory.GetContext().SaveChanges();
      foreach (var subquestion in qvVM.Subquestions)
      {
        subquestion.ParentId = qvDB.OrderTemplateQuestionVariantId;
        qRepository.Insert(CreateUserQuestion(subquestion));
      }
      return qvDB;
    }

    private OrderTemplateQuestion CreateUserQuestion(OrderTemplateQuestionViewModel qVM)
    {
      IRepository<QuestionType> qtRepository = new GenericRepository<QuestionType>(_factory.ContextFactory);
      OrderTemplateQuestion qDB = _mapper.Map<OrderTemplateQuestion>(qVM);

      qDB.QuestionType = qtRepository.Get(filter: (qt => !qt.Visible), orderBy: (query => query.OrderBy(t => t.QuestionTypeId))).First();

      foreach (var qTicket in qVM.QuestionTickets)
        qDB.OrderTemplateQuestionTickets.Add(new OrderTemplateQuestionTicket() { CollectInformation = qTicket.CollectInformation, TicketId = qTicket.TicketId });

      foreach (var qvVM in qVM.QuestionVariants)
        qDB.OrderTemplateQuestionVariants.Add(CreateQuestionVariant(qvVM));

      return qDB;
    }

    private void UpdateQuestionVariant(OrderTemplateQuestionVariantViewModel qvVM, OrderTemplateQuestionVariant qvDB)
    {
      IRepository<OrderTemplateQuestion> qRepository = new GenericRepository<OrderTemplateQuestion>(_factory.ContextFactory);
      _mapper.Map(qvVM, qvDB);

      var deletedQuestions = qRepository.Get(filter: (q => q.ParentId == qvVM.OrderTemplateQuestionVariantId)).Where(dq => !qvVM.Subquestions.Any(sq => sq.OrderTemplateQuestionId == dq.OrderTemplateQuestionId)).ToList();
      foreach (var dq in deletedQuestions)
        qRepository.Delete(dq);

      foreach (var subqVM in qvVM.Subquestions)
      {
        if (subqVM.OrderTemplateQuestionId == 0)
          qRepository.Insert(CreateUserQuestion(subqVM));
        else
        {
          var subqDB = qRepository.Get(filter: (q => q.OrderTemplateQuestionId == subqVM.OrderTemplateQuestionId)).SingleOrDefault();
          UpdateUserQuestion(subqVM, subqDB);
        }
      }
    }

    private void UpdateUserQuestion(OrderTemplateQuestionViewModel qVM, OrderTemplateQuestion qDB)
    {
      _mapper.Map(qVM, qDB);

      foreach (var qTicketVM in qVM.QuestionTickets)
      {
        var qTicketDB = qDB.OrderTemplateQuestionTickets.Where(qt => qt.OrderTemplateQuestionTicketId == qTicketVM.OrderTemplateQuestionTicketId).SingleOrDefault();
        if (qTicketDB != null)
          _mapper.Map(qTicketVM, qTicketDB);
      }

      //Delete
      foreach (var qVariant in qDB.OrderTemplateQuestionVariants.Where(v => !qVM.QuestionVariants.Any(vv => vv.OrderTemplateQuestionVariantId == v.OrderTemplateQuestionVariantId)))
        DeleteQuestionVariant(qVariant, qDB.OrderTemplateId);
      //Insert and Update
      foreach (var qVariantVM in qVM.QuestionVariants)
      {
        if (qVariantVM.OrderTemplateQuestionVariantId == 0)
          qDB.OrderTemplateQuestionVariants.Add(CreateQuestionVariant(qVariantVM));
        else
        {
          var qVariantDB = qDB.OrderTemplateQuestionVariants.Where(v => v.OrderTemplateQuestionVariantId == qVariantVM.OrderTemplateQuestionVariantId).SingleOrDefault();
          UpdateQuestionVariant(qVariantVM, qVariantDB);
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
            otDB.Title = otVM.Title ?? "";
            otDB.Instruction = otVM.Instruction;
            otDB.TimeLimit = otVM.TimeLimit;
            otDB.AfterMessage = otVM.AfterMessage;
            otDB.AllowCallPickup = otVM.AllowCallPickup;
            otDB.AllowEdit = otVM.AllowEdit;
            otDB.AcceptRefund = otVM.AcceptRefund;
            otDB.GroupAllowPassword = otVM.GroupAllowPassword;
            otDB.GroupAllowSetTime = otVM.GroupAllowSetTime;
            otDB.GroupAskIndividualTime = otVM.GroupAskIndividualTime;
            otDB.GroupMaxAttendees = otVM.GroupMaxAttendees;
            otDB.GroupPageDescription = otVM.GroupPageDescription;
            otDB.GroupPageHeadline = otVM.GroupPageHeadline;
            otDB.GroupRequirePassword = otVM.GroupRequirePassword;
            otDB.OrderTemplateGroupTypeId = otVM.OrderTemplateGroupTypeId;

            foreach (var ct in otVM.CollectTickets)
              otDB.OrderTemplateTickets.Where(t => t.OrderTemplateTicketId == ct.OrderTemplateTicketId).SingleOrDefault().CollectInformation = ct.CollectInformation;

            foreach (var q in otVM.Questions)
            {
              var qDB = otDB.OrderTemplateQuestions.Where(otq => otq.OrderTemplateQuestionId == q.OrderTemplateQuestionId).SingleOrDefault();
              qDB.Include = qDB.QuestionType.MustInclude || q.Include;
              qDB.Require = qDB.QuestionType.MustRequire || q.Require;
              qDB.SortNum = q.SortNum;
            }

            foreach (var gt in otVM.GroupTickets)
            {
              var gtDB = otDB.OrderTemplateTickets.Where(g1 => g1.OrderTemplateTicketId == gt.OrderTemplateGroupTicketId).Single();
              gtDB.EnableRegistration = gt.EnableRegistration;
              gtDB.GroupOnly = gt.GroupOnly;
              gtDB.DontDisplay = gt.DontDisplay;
            }
            uow.Context.SaveChanges();
            //Delete
            var deleteUserQuestions = otDB.OrderTemplateQuestions.Where(q => !q.QuestionType.Visible).Where(q1 => !otVM.UserQuestions.Any(uq => uq.OrderTemplateQuestionId == q1.OrderTemplateQuestionId)).ToList();
            foreach (var userQuestion in deleteUserQuestions)
              DeleteUserQuestion(userQuestion);
            uow.Context.SaveChanges();
            //Update and Insert
            foreach (var userQuestionVM in otVM.UserQuestions)
            {
              if (userQuestionVM.OrderTemplateQuestionId == 0)
                otDB.OrderTemplateQuestions.Add(CreateUserQuestion(userQuestionVM));
              else
              {
                var userQuestionDB = otDB.OrderTemplateQuestions.Where(q => q.OrderTemplateQuestionId == userQuestionVM.OrderTemplateQuestionId).SingleOrDefault();
                UpdateUserQuestion(userQuestionVM, userQuestionDB);
              }
            }

            uow.Context.SaveChanges();
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
            otDB.IncludePrintableTickets = otVM.IncludePrintableTickets;

            foreach (var rtDB in otDB.OrderTemplateTickets)
            {
              var rtVM = otVM.ReceiveByTypes.Where(rt => rt.OrderTemplateReceiveByTypeId == rtDB.OrderTemplateTicketId).SingleOrDefault();
              rtDB.Receive = rtVM != null;
            }

            uow.Context.SaveChanges();
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

            uow.Context.SaveChanges();
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
            _mapper.Map(otVM.Waitlist, otDB);

            uow.Context.SaveChanges();
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


    public IEnumerable<LanguageViewModel> GetLanguagesViewModel()
    {
      IRepository<Language> languageRepository = new GenericRepository<Language>(_factory.ContextFactory);
      return _mapper.Map<IEnumerable<LanguageViewModel>>(languageRepository.Get(orderBy: query => query.OrderBy(l => l.LanguageName)).ToList());
    }

    public IEnumerable<ControlTypeViewModel> GetControlTypesViewModel()
    {
      IRepository<ControlType> controlTypeRepository = new GenericRepository<ControlType>(_factory.ContextFactory);
      return _mapper.Map<IEnumerable<ControlTypeViewModel>>(controlTypeRepository.Get(orderBy: query => query.OrderBy(l => l.SortNum)).ToList());
    }

    public IEnumerable<QuestionTypeGroupViewModel> GetQuestionTypeGroups()
    {
      IRepository<QuestionType> questionTypesRepository = new GenericRepository<QuestionType>(_factory.ContextFactory);
      return _mapper.Map<IEnumerable<QuestionTypeGroupViewModel>>(questionTypesRepository.Get(filter: qt => qt.Visible).Select(g => g.QuestionTypeGroup).Distinct().OrderBy(og => og.SortNum).ToList());
    }

    public IEnumerable<OrderTemplateGroupTypeViewModel> GetOrderTemplateGroupTypes()
    {
      IRepository<OrderTemplateGroupType> groupRepository = new GenericRepository<OrderTemplateGroupType>(_factory.ContextFactory);
      return _mapper.Map<IEnumerable<OrderTemplateGroupTypeViewModel>>(groupRepository.Get(orderBy: query => query.OrderBy(g => g.OrderTemplateGroupTypeId)).ToList());
    }


    public string GetEventTitle(long eventId)
    {
      IRepository<Event> eventRepository = new GenericRepository<Event>(_factory.ContextFactory);
      return eventRepository.Get(filter: (e => e.EventID == eventId)).SingleOrDefault().EventTitle;
    }
  }
}