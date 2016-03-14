using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;

namespace EventCombo.Controllers
{
  public class OrderOptionsController : Controller
  {
    private ActionResult DefaultAction()
    {
      return RedirectToAction("Index", "Home");
    }

    private static DateTime startDateTime = DateTime.ParseExact("01-01-2000", "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);

    private void SetDefaultViewBag(EventComboEntities db, AutoMapper.IMapper mapper, string Message, bool isError, long eventId)
    {
      if (db == null)
        throw new ArgumentNullException("db");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      //Prepare Tickets
      List<TicketViewModel> tickets = new List<TicketViewModel>();
      foreach (var tt in db.Tickets.Where(t => t.E_Id == eventId))
        tickets.Add(mapper.Map<TicketViewModel>(tt));
      ViewBag.Tickets = tickets;

      //Prepare OrderTemplateTypes
      List<OrderTemplateTypeViewModel> templateTypes = new List<OrderTemplateTypeViewModel>();
      foreach (var ott in db.OrderTemplateTypes)
        templateTypes.Add(mapper.Map<OrderTemplateTypeViewModel>(ott));
      ViewBag.OrderTemplateTypes = templateTypes;

      //Prepare OrderTemplateEventTypes
      List<OrderTemplateEventTypeViewModel> eventTypes = new List<OrderTemplateEventTypeViewModel>();
      foreach (var et in db.OrderTemplateEventTypes)
        eventTypes.Add(mapper.Map<OrderTemplateEventTypeViewModel>(et));
      ViewBag.OrderTemplateEventTypes = eventTypes;

      //Prepare SelectedItems for Confirmation View
      List<SelectListItem> SendSettings = new List<SelectListItem>();
      SendSettings.Add(new SelectListItem()
        {
          Text = "Same settings for all ticket types",
          Value = false.ToString(),
          Selected = true
        });
      SendSettings.Add(new SelectListItem()
        {
          Text = "Custom settings for each ticket type",
          Value = true.ToString(),
          Selected = false
        });

      ViewBag.StartDateTime = startDateTime;
      ViewBag.SendSettings = SendSettings;
      ViewBag.StateMessage = Message;
      ViewBag.IsError = isError;
      ViewBag.EventId = eventId;
    }

    private OrderTemplate CreateOrderTemplate(OrderTemplateViewModel ot, EventComboEntities db, AutoMapper.IMapper mapper)
    {
      OrderTemplate otDB = new OrderTemplate();
      mapper.Map(ot, otDB);
      foreach (var rcv in ot.ReceiveByTypes)
        otDB.OrderTemplateReceiveByTypes.Add(mapper.Map<OrderTemplateReceiveByType>(rcv));
      foreach (var ct in ot.CollectTickets)
        otDB.OrderTemplateTickets.Add(mapper.Map<OrderTemplateTicket>(ct));
      foreach (var q in ot.Questions)
        otDB.OrderTemplateQuestions.Add(mapper.Map<OrderTemplateQuestion>(q));
      otDB.OrderTemplateWaitlists.Add(mapper.Map<OrderTemplateWaitlist>(ot.Waitlist));

      db.OrderTemplates.Add(otDB);
      db.SaveChanges();
      return otDB;
    }

    private void LoadOrderTemplateViewModel(OrderTemplateViewModel ot, EventComboEntities db, AutoMapper.IMapper mapper)
    {
      ot.OrderTemplateTypes.Clear();
      foreach (var otType in db.OrderTemplateTypes)
        ot.OrderTemplateTypes.Add(mapper.Map<OrderTemplateTypeViewModel>(otType));

      ot.CollectTickets.Clear();
      var otTickets = db.OrderTemplateTickets.Where(tt => tt.OrderTemplateId == ot.OrderTemplateId);
      foreach (var otTicket in otTickets)
      {
        var ttVM = mapper.Map<OrderTemplateTicketViewModel>(otTicket);
        mapper.Map(db.Tickets.Where(tt => tt.T_Id == otTicket.TicketId).SingleOrDefault(), ttVM.CollectTicket);
        ot.CollectTickets.Add(ttVM);
      }

      ot.ReceiveByTypes.Clear();
      var rcvTickets = db.OrderTemplateReceiveByTypes.Where(rt => rt.OrderTemplateId == ot.OrderTemplateId);
      foreach (var receive in rcvTickets)
      {
        var trcv = mapper.Map<OrderTemplateReceiveByTypeViewModel>(receive);
        mapper.Map(db.Tickets.Where(tt => tt.T_Id == trcv.TicketId).SingleOrDefault(), trcv.ReceiveTicket);
        ot.ReceiveByTypes.Add(trcv);
      }

      ot.Questions.Clear();
      var otQuestions = db.OrderTemplateQuestions
        .Where(q => q.OrderTemplateId == ot.OrderTemplateId)
        .OrderBy(q1 => q1.QuestionType.QuestionTypeId);
      foreach (var otQuestion in otQuestions)
      {
        var qvm = mapper.Map<OrderTemplateQuestionViewModel>(otQuestion);
        mapper.Map(otQuestion.QuestionType, qvm.Question);
        mapper.Map(otQuestion.QuestionType.QuestionTypeGroup, qvm.Question.Group);
        ot.Questions.Add(qvm);
      }

      var waitlist = db.OrderTemplateWaitlists.Where(wl => wl.OrderTemplateId == ot.OrderTemplateId).OrderBy(w => w.TicketId).FirstOrDefault();
      if (waitlist != null)
      {
        ot.Waitlist = mapper.Map<OrderTemplateWaitlistViewModel>(waitlist);
        ot.Waitlist.WaitlistTicket = mapper.Map<TicketViewModel>(waitlist.Ticket);
      }
    }

    private OrderTemplateViewModel GetOrderTemplate(long eventId)
    {
      using (EventComboEntities objEnt = new EventComboEntities())
      {
        var ev = objEnt.Events.Where(e => e.EventID == eventId).SingleOrDefault();
        if (ev == null)
          return null;

        var mapper = AutomapperConfig.Config.CreateMapper();

        SetDefaultViewBag(objEnt, mapper, "", false, eventId);

        OrderTemplateViewModel ot = new OrderTemplateViewModel();
        ot.UserID = ev.UserID;
        OrderTemplate otDB = objEnt.OrderTemplates.Where(t => t.EventID == eventId).SingleOrDefault();
        if (otDB == null)
        {
          ot.EventId = eventId;
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
          ot.OrderTemplateTypeId = ViewBag.OrderTemplateTypes[0].OrderTemplateTypeId;
          ot.OrderTemplateEventTypeId = ViewBag.OrderTemplateEventTypes[0].OrderTemplateEventTypeId;
          ot.ReplyEmail = objEnt.AspNetUsers.Where(u => u.Id == ot.UserID).FirstOrDefault().Email;
          ot.TicketMessage = "";
          ot.EnableWaitlist = false;

          var tickets = objEnt.Tickets.Where(t => t.E_Id == eventId).OrderBy(k => k.T_Id);
          foreach (var ticket in tickets)
          {
            var otTicket = new OrderTemplateTicketViewModel() { TicketId = ticket.T_Id, CollectInformation = false, OrderTemplateTicketId = 0 };
            mapper.Map(ticket, otTicket.CollectTicket);
            ot.CollectTickets.Add(otTicket);
            var rcvTicket = new OrderTemplateReceiveByTypeViewModel() { TicketId = ticket.T_Id, Receive = false, OrderTemplateReceiveByTypeId = 0 };
            mapper.Map(ticket, rcvTicket.ReceiveTicket);
            ot.ReceiveByTypes.Add(rcvTicket);
          }

          var questions = objEnt.QuestionTypes.Where(qt => qt.Visible == true).OrderBy(q1 => q1.QuestionTypeId);
          foreach (var question in questions)
          {
            var otQuestion = new OrderTemplateQuestionViewModel() { QuestionTypeId = question.QuestionTypeId, Include = question.Include, Require = question.Require };
            mapper.Map(question, otQuestion.Question);
            mapper.Map(question.QuestionTypeGroup, otQuestion.Question.Group);
            ot.Questions.Add(otQuestion);
          }

          ot.Waitlist.NameRequired = true;
          ot.Waitlist.EmailRequired = true;
          ot.Waitlist.PhoneRequired = false;
          ot.Waitlist.RespondTime = startDateTime.AddDays(1);
          ot.Waitlist.ResponseMessage = "If a ticket becomes available, you will be contacted automatically with further instructions on how to purchase your ticket. No further action is required.";
          ot.Waitlist.ReleaseMessage = "Congratulations! A spot has opened up for you at this event. You have a limited time to sign up. Please follow the links below to register for the event. If you have any questions, please contact the organizer: " + ot.ReplyEmail;

          otDB = CreateOrderTemplate(ot, objEnt, mapper);

          if (otDB == null)
            DefaultAction();
        }

        mapper.Map(otDB, ot);

        LoadOrderTemplateViewModel(ot, objEnt, mapper);

        return ot;
      }
    }

    [Authorize]
    [HttpGet]
    public ActionResult Form(long eventId)
    {

      if ((Session["AppId"] != null))
      {
        ValidationMessageController vmc = new ValidationMessageController();
        long lastEventId = vmc.GetLatestEventId(eventId);

        OrderTemplateViewModel orderTemplate = GetOrderTemplate(lastEventId);

        if (orderTemplate == null)
          return DefaultAction();

        if (Session["AppId"].ToString().Trim() != orderTemplate.UserID.Trim())
          return DefaultAction();

        HomeController hmc = new HomeController();
        hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
        string usernme = hmc.getusername();
        if (string.IsNullOrEmpty(usernme))
        {
          return DefaultAction();
        }
        Session["logo"] = "events";
        Session["Fromname"] = "ManageEvent";
        var url = Url.Action("Form", "OrderOptions");
        Session["ReturnUrl"] = "OrderOptions~" + url;

        ViewBag.EventId = lastEventId;
        return View(orderTemplate);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpPost]
    public ActionResult Form(OrderTemplateViewModel orderTemplate)
    {
      using (EventComboEntities db = new EventComboEntities())
      {
        var mapper = AutomapperConfig.Config.CreateMapper();
        OrderTemplate otDB = db.OrderTemplates.Where(ot => ot.OrderTemplateId == orderTemplate.OrderTemplateId).FirstOrDefault();
        if (otDB != null)
        {
          otDB.OrderTemplateTypeId = orderTemplate.OrderTemplateTypeId;
          otDB.Title = orderTemplate.Title;
          otDB.Instruction = orderTemplate.Instruction;
          otDB.TimeLimit = orderTemplate.TimeLimit;
          otDB.AfterMessage = orderTemplate.AfterMessage;
          otDB.AllowCallPickup = orderTemplate.AllowCallPickup;
          otDB.AllowEdit = orderTemplate.AllowEdit;
          otDB.AcceptRefund = orderTemplate.AcceptRefund;

          foreach (var ct in orderTemplate.CollectTickets)
            otDB.OrderTemplateTickets.Where(t => t.OrderTemplateTicketId == ct.OrderTemplateTicketId).SingleOrDefault().CollectInformation = ct.CollectInformation;

          foreach (var q in orderTemplate.Questions)
          {
            var qDB = otDB.OrderTemplateQuestions.Where(otq => otq.OrderTemplateQuestionId == q.OrderTemplateQuestionId).SingleOrDefault();
            qDB.Include = qDB.QuestionType.MustInclude || q.Include;
            qDB.Require = qDB.QuestionType.MustRequire || q.Require;
          }

          db.SaveChanges();

          foreach (var ct in otDB.OrderTemplateTickets)
          {
            var otct = orderTemplate.CollectTickets.Where(t => t.OrderTemplateTicketId == ct.OrderTemplateTicketId).SingleOrDefault();
            if (otct != null)
            {
              mapper.Map(ct, otct);
              otct.CollectTicket = mapper.Map<TicketViewModel>(ct.Ticket);
            }
          }

          foreach (var otQDB in otDB.OrderTemplateQuestions)
          {
            var otQ = orderTemplate.Questions.Where(q => q.OrderTemplateQuestionId == otQDB.OrderTemplateQuestionId).SingleOrDefault();
            if (otQ != null)
            {
              mapper.Map(otQDB, otQ);
              otQ.Question = mapper.Map<QuestionTypeViewModel>(otQDB.QuestionType);
              otQ.Question.Group = mapper.Map<QuestionTypeGroupViewModel>(otQDB.QuestionType.QuestionTypeGroup);
            }
          }
          
          SetDefaultViewBag(db, mapper, "Order options saved.", false, orderTemplate.EventId);
        }
        else
          SetDefaultViewBag(db, mapper, "Order options didn't saved.", true, orderTemplate.EventId);
      }
      ViewBag.EventId = orderTemplate.EventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpGet]
    public ActionResult Confirmation(long eventId)
    {
      ValidationMessageController vmc = new ValidationMessageController();
      long lastEventId = vmc.GetLatestEventId(eventId);

      OrderTemplateViewModel orderTemplate = GetOrderTemplate(lastEventId);
      if (orderTemplate == null)
        return DefaultAction();

      ViewBag.EventId = eventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Confirmation(OrderTemplateViewModel orderTemplate)
    {
      using (EventComboEntities db = new EventComboEntities())
      {
        var mapper = AutomapperConfig.Config.CreateMapper();
        OrderTemplate otDB = db.OrderTemplates.Where(ot => ot.OrderTemplateId == orderTemplate.OrderTemplateId).FirstOrDefault();
        if (otDB != null)
        {
          otDB.CustomIncludeSettings = orderTemplate.CustomIncludeSettings;
          otDB.ConfirmationMessage = orderTemplate.ConfirmationMessage;
          otDB.ReplyEmail = orderTemplate.ReplyEmail;
          otDB.TicketMessage = orderTemplate.TicketMessage;

          foreach (var rt in orderTemplate.ReceiveByTypes)
            otDB.OrderTemplateReceiveByTypes.Where(t => t.OrderTemplateReceiveByTypeId == rt.OrderTemplateReceiveByTypeId).SingleOrDefault().Receive = rt.Receive;

          db.SaveChanges();
          foreach (var rt in otDB.OrderTemplateReceiveByTypes)
          {
            var otrt = orderTemplate.ReceiveByTypes.Where(t => t.OrderTemplateReceiveByTypeId == rt.OrderTemplateReceiveByTypeId).SingleOrDefault();
            if (otrt != null)
            {
              mapper.Map(rt, otrt);
              otrt.ReceiveTicket = mapper.Map<TicketViewModel>(rt.Ticket);
            }
          }
          SetDefaultViewBag(db, mapper, "Confirmation saved.", false, orderTemplate.EventId);
        }
        else
          SetDefaultViewBag(db, mapper, "Confirmation didn't saved.", true, orderTemplate.EventId);
      }
      ViewBag.EventId = orderTemplate.EventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpGet]
    public ActionResult EventType(long eventId)
    {
      ValidationMessageController vmc = new ValidationMessageController();
      long lastEventId = vmc.GetLatestEventId(eventId);

      OrderTemplateViewModel orderTemplate = GetOrderTemplate(lastEventId);
      if (orderTemplate == null)
        return DefaultAction();

      ViewBag.EventId = eventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpPost]
    public ActionResult EventType(OrderTemplateViewModel orderTemplate)
    {
      using (EventComboEntities db = new EventComboEntities())
      {
        var mapper = AutomapperConfig.Config.CreateMapper();
        OrderTemplate otDB = db.OrderTemplates.Where(ot => ot.OrderTemplateId == orderTemplate.OrderTemplateId).FirstOrDefault();
        if (otDB != null)
        {
          otDB.OrderTemplateEventTypeID = orderTemplate.OrderTemplateEventTypeId;
          db.SaveChanges();
          SetDefaultViewBag(db, mapper, "Event type saved.", false, orderTemplate.EventId);
        }
        else
          SetDefaultViewBag(db, mapper, "Event type didn't saved.", true, orderTemplate.EventId);
      }
      ViewBag.EventId = orderTemplate.EventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpGet]
    public ActionResult Waitlist(long eventId)
    {
      ValidationMessageController vmc = new ValidationMessageController();
      long lastEventId = vmc.GetLatestEventId(eventId);

      OrderTemplateViewModel orderTemplate = GetOrderTemplate(lastEventId);
      if (orderTemplate == null)
        return DefaultAction();

      orderTemplate.Waitlist.RespondDays = (orderTemplate.Waitlist.RespondTime - startDateTime).Days;
      orderTemplate.Waitlist.RespondHours = (orderTemplate.Waitlist.RespondTime.AddDays(orderTemplate.Waitlist.RespondDays) - startDateTime).Hours;
      orderTemplate.Waitlist.RespondMinutes = (orderTemplate.Waitlist.RespondTime.AddDays(orderTemplate.Waitlist.RespondDays).AddHours(orderTemplate.Waitlist.RespondHours) - startDateTime).Minutes;

      ViewBag.EventId = eventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Waitlist(OrderTemplateViewModel orderTemplate)
    {
      using (EventComboEntities db = new EventComboEntities())
      {
        var mapper = AutomapperConfig.Config.CreateMapper();
        OrderTemplate otDB = db.OrderTemplates.Where(ot => ot.OrderTemplateId == orderTemplate.OrderTemplateId).FirstOrDefault();
        if (otDB != null)
        {
          orderTemplate.Waitlist.RespondTime = startDateTime.AddDays(orderTemplate.Waitlist.RespondDays).AddHours(orderTemplate.Waitlist.RespondHours).AddMinutes(orderTemplate.Waitlist.RespondMinutes);
          orderTemplate.Waitlist.NameRequired = true;
          orderTemplate.Waitlist.EmailRequired = true;
          otDB.EnableWaitlist = orderTemplate.EnableWaitlist;
          foreach (var wlDB in otDB.OrderTemplateWaitlists)
          {
            orderTemplate.Waitlist.OrderTemplateWaitlistId = wlDB.OrderTemplateWaitlistId;
            mapper.Map(orderTemplate.Waitlist, wlDB);
          }

          db.SaveChanges();
          SetDefaultViewBag(db, mapper, "Waitlist saved.", false, orderTemplate.EventId);
        }
        else
          SetDefaultViewBag(db, mapper, "Waitlist didn't saved.", true, orderTemplate.EventId);
      }
      ViewBag.EventId = orderTemplate.EventId;
      return View(orderTemplate);
    }
  }
}