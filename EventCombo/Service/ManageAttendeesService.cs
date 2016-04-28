using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.DAL;
using AutoMapper;
using System.Net.Mail;

namespace EventCombo.Service
{
  public class ManageAttendeesService : IManageAttendeesService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private IDBAccessService _dbservice;

    public ManageAttendeesService(IUnitOfWorkFactory factory, IMapper mapper, IDBAccessService dbService)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      if (mapper == null)
        throw new ArgumentNullException("mapper");

      if (dbService == null)
        throw new ArgumentNullException("dbService");

      _factory = factory;
      _mapper = mapper;
      _dbservice = dbService;
    }

    public ManageAttendeesOrdersViewModel GetEventOrdersSummary(long eventId)
    {
      ManageAttendeesOrdersViewModel res = new ManageAttendeesOrdersViewModel();
      res.EventId = eventId;

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var tickets = tpdRepo.Get(filter: (t => t.TPD_Event_Id == eventId));

      EventOrdersSummuryViewModel ordersTotal = new EventOrdersSummuryViewModel()
      {
        PaymentState = PaymentStates.Total,
        TicketsSold = tickets.Sum(t => t.TPD_Purchased_Qty) ?? 0,
        Amount = tickets.Sum(t => t.TPD_Amount) ?? 0,
        TicketsTotal = tickets.Sum(t => t.Event.Tickets.Sum(tt => tt.Ticket_Quantity_Detail.Sum(q => q.TQD_Quantity))) ?? 0,
        Count = tickets.Select(t => t.TPD_Order_Id).Distinct().Count()
      };
      EventOrdersSummuryViewModel ordersCompleted = _mapper.Map<EventOrdersSummuryViewModel>(ordersTotal);
      ordersCompleted.PaymentState = PaymentStates.Completed;
      EventOrdersSummuryViewModel ordersPending = new EventOrdersSummuryViewModel()
      {
        PaymentState = PaymentStates.Pending,
        TicketsSold = 0,
        Amount = 0,
        TicketsTotal = 0,
        Count = 0
      };
      res.OrdersSummary.Add(ordersTotal);
      res.OrdersSummary.Add(ordersCompleted);
      res.OrdersSummary.Add(ordersPending);

      return res;
    }


    public IEnumerable<EventOrderInfoViewModel> GetOrdersForEvent(PaymentStates state, long eventId)
    {
      if (state == PaymentStates.Pending)
        return new List<EventOrderInfoViewModel>();

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      IEnumerable<EventOrderInfoViewModel> res = tpdRepo.Get(filter: (t => t.TPD_Event_Id == eventId))
        .GroupBy(tt => tt.TPD_Order_Id)
        .Select(ticket => new EventOrderInfoViewModel()
        {
          OrderId = ticket.Key,
          PaymentState = state,
          Price = ticket.Sum(t => t.TPD_Amount) ?? 0,
          BuyerName = ticket.FirstOrDefault().AspNetUser.Profiles.Select(p => p.FirstName + p.LastName).FirstOrDefault(),
          BuyerEmail = ticket.FirstOrDefault().AspNetUser.Profiles.Select(p => p.Email).FirstOrDefault(),
          Quantity = ticket.Sum(t => t.TPD_Purchased_Qty) ?? 0
        }).ToList();
      foreach (var order in res)
      {
        var orderDB = orderRepo.Get(filter: (o => o.O_Order_Id == order.OrderId)).FirstOrDefault();
        if (orderDB != null)
          order.Date = orderDB.O_OrderDateTime ?? DateTime.Today;
      }

      return res;
    }


    public EventOrderDetailViewModel GetEventOrderDetail(string orderId)
    {
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      IRepository<TicketBearer> tbRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);

      EventOrderDetailViewModel res = new EventOrderDetailViewModel()
      {
        OrderId = orderId,
        Payment = _dbservice.GetPaymentInfo(orderId),
        Email = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault().AspNetUser.Profiles.FirstOrDefault().Email
      };

      foreach(var attendee in tbRepo.Get(filter: (a => a.OrderId == orderId)))
        res.Attendees.Add(_mapper.Map<AttendeeViewModel>(attendee));

      return res;
    }

    public EventOrderDetailViewModel GetOrderDetails(string orderId)
    {
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var order = orderRepo.Get(filter: (o => (o.O_Order_Id == orderId))).FirstOrDefault();
      if (order == null)
        return null;

      EventOrderDetailViewModel details = new EventOrderDetailViewModel() { OrderId = orderId };
      details.Payment = _dbservice.GetPaymentInfo(orderId);

      IRepository<EventCombo.Models.Profile> userRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
      var user = userRepo.Get(filter: (u => u.UserID == order.O_User_Id)).First();
      details.Email = user.Email;

      IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
      foreach (var att in attRepo.Get(filter: (tb => ((tb.OrderId == orderId) && (tb.UserId == order.O_User_Id)))))
        details.Attendees.Add(_mapper.Map<AttendeeViewModel>(att));

      return details;
    }

    public bool SendConfirmations(string orderId, string baseUrl)
    {
      if (String.IsNullOrWhiteSpace(orderId))
        throw new ArgumentNullException("orderId");

      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();
      if (order == null)
        return false;

      List<MailAddress> addresses = new List<MailAddress>();
      var profile = order.AspNetUser.Profiles.FirstOrDefault();
      if ((profile != null) && (!String.IsNullOrWhiteSpace(profile.Email)))
        addresses.Add(new MailAddress(profile.Email, profile.FirstName + " " + profile.LastName));

      IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
      foreach(var attendee in attRepo.Get(filter: (a => a.OrderId == orderId)))
        if (!String.IsNullOrWhiteSpace(attendee.Email))
          addresses.Add(new MailAddress(attendee.Email, attendee.Name));

      if (addresses.Count <= 0)
        return false;
      else
      {
        INotification notification = new OrderNotification(_factory, _dbservice, orderId, baseUrl);
        ISendMailService sendService = new SendMailService();
        INotificationSender sender = new NotificationSender(notification, sendService);
        sender.SendSeparately(addresses);
        return true;
      }

    }
  }
}