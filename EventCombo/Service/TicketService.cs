using System;
using System.Collections.Generic;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Net.Mime;
using EventCombo.Controllers;
using EventCombo.Utils;

namespace EventCombo.Service
{
  public class TicketService : ITicketsService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private IDBAccessService _dbservice;

    public TicketService(IUnitOfWorkFactory factory, IMapper mapper, IDBAccessService dbService)
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

    private IEnumerable<OrderMainViewModel> GetOrdersForUser(string userId)
    {
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);

      List<OrderMainViewModel> ordersVM = new List<OrderMainViewModel>();
      OrderMainViewModel oVM = null;
      foreach (var order in orderRepo.Get(filter: (o => o.O_User_Id == userId)))
      {
        var ptickets = tpdRepo.Get(filter: (t => t.TPD_Order_Id == order.O_Order_Id));
        var firstticket = ptickets == null ? null : ptickets.FirstOrDefault();
        if ((ptickets != null) && (firstticket != null))
        {
          oVM = new OrderMainViewModel()
          {
            OId = order.O_Id,
            Name = firstticket.Event.EventTitle,
            Quantity = ptickets.Sum(pt => pt.TPD_Purchased_Qty),
            TotalPaid = ptickets.Sum(pt => pt.TPD_Amount) + order.O_VariableAmount,
            EventStartDate = DateTime.Parse(firstticket.Ticket_Quantity_Detail.Publish_Event_Detail.PE_Scheduled_Date),
            EventEndDate = DateTime.Parse(firstticket.Ticket_Quantity_Detail.Publish_Event_Detail.PE_MultipleVenue_id > 0 ?
              firstticket.Ticket_Quantity_Detail.Publish_Event_Detail.PE_Scheduled_Date :
              firstticket.Ticket_Quantity_Detail.Publish_Event_Detail.EventVenue.EventEndDate),
            OrderId = order.O_Order_Id,
            EventCancelled = (firstticket.Event.EventCancel == "Y"),
            Favorite = firstticket.Event.EventFavourites.Where(ef => ef.UserID == userId).Any(),
            OrderStateId = order.OrderStateId,
            EventId = firstticket.TPD_Event_Id
          };
          ordersVM.Add(oVM);
        };
      }
      return ordersVM;
    }

    private IEnumerable<OrderMainViewModel> GetUpcomingOrdersForUser(string userId)
    {
      IEnumerable<OrderMainViewModel> orders = GetOrdersForUser(userId)
        .Where(t => ((!t.EventCancelled) && (t.EventEndDate >= DateTime.Today))).ToList();
      return orders;
    }

    private IEnumerable<OrderMainViewModel> GetPastOrdersForUser(string userId)
    {
      IEnumerable<OrderMainViewModel> orders = GetOrdersForUser(userId)
        .Where(t => ((t.EventCancelled) || (t.EventEndDate < DateTime.Today))).ToList();
      return orders;
    }

    private IEnumerable<OrderMainViewModel> GetFavoriteOrdersForUser(string userId)
    {
      IEnumerable<OrderMainViewModel> orders = GetOrdersForUser(userId)
      .Where(t => t.Favorite).ToList();
      return orders;
    }

    public IEnumerable<OrderMainViewModel> GetOrdersList(OrderTypes type, string userId)
    {
      IEnumerable<OrderMainViewModel> orders = null;
      switch (type)
      {
        case OrderTypes.Upcoming:
          orders = GetUpcomingOrdersForUser(userId);
          break;
        case OrderTypes.Past:
          orders = GetPastOrdersForUser(userId);
          break;
        case OrderTypes.Favorite:
          orders = GetFavoriteOrdersForUser(userId);
          break;
        default:
          break;
      }
      return orders;
    }


    public long GetOrdersCount(OrderTypes type, string userId)
    {
      return GetOrdersList(type, userId).Count();
    }


    public OrderDetailsViewModel GetOrderDetails(string orderId, string userId)
    {
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      var order = orderRepo.Get(filter: (o => ((o.O_Order_Id == orderId) && (o.O_User_Id == userId)))).FirstOrDefault();
      if (order == null)
        return null;

      OrderDetailsViewModel details = new OrderDetailsViewModel() { OrderId = orderId };
      details.Payment = _dbservice.GetPaymentInfo(orderId);

      IRepository<EventCombo.Models.Profile> userRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
      var user = userRepo.Get(filter: (u => u.UserID == userId)).First();
      details.Email = user.Email;

      IRepository<Ticket_Purchased_Detail> ptRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var tickets = ptRepo.Get(filter: (t => ((t.TPD_Order_Id == orderId) && (t.TPD_User_Id == userId))));
      details.EventLocation = String.Join("; ", tickets.Select(t => t.Ticket_Quantity_Detail.Address.ConsolidateAddress).Distinct().ToArray());
      details.EventDate = String.Join("; ", tickets.Select(t => t.Ticket_Quantity_Detail.Publish_Event_Detail.PE_MultipleVenue_id > 0 ?
              t.Ticket_Quantity_Detail.Publish_Event_Detail.MultipleEvent.StartingFrom :
              t.Ticket_Quantity_Detail.Publish_Event_Detail.EventVenue.EventStartDate).Distinct().ToArray());

      IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
      foreach (var att in attRepo.Get(filter: (tb => ((tb.OrderId == orderId) && (tb.UserId == userId)))))
        details.Attendees.Add(_mapper.Map<AttendeeViewModel>(att));

      return details;
    }


    public bool SaveOrderDetails(OrderDetailsViewModel model, string userId, string baseUrl, string filePath)
    {
      bool res = true;
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
          List<AttendeeViewModel> selected = new List<AttendeeViewModel>();
          foreach (var attendee in attRepo.Get(filter: (a => ((a.OrderId == model.OrderId) && (a.UserId == userId)))))
          {
            var attVM = model.Attendees.Where(a => a.TicketbearerId == attendee.TicketbearerId).SingleOrDefault();
            if (attVM != null)
            {
              if (
                ((!String.IsNullOrWhiteSpace(attVM.Name) || !String.IsNullOrWhiteSpace(attendee.Name))
                  && ((attVM.Name ?? "").Trim() != (attendee.Name ?? "").Trim()))
                || ((!String.IsNullOrWhiteSpace(attVM.Email) || !String.IsNullOrWhiteSpace(attendee.Email)))
                  && ((attVM.Email ?? "").Trim() != (attendee.Email ?? "").Trim()))
              {
                _mapper.Map(attVM, attendee);
                if (model.SendEmail && (!selected.Where(a => a.Email == attVM.Email).Any()))
                  selected.Add(attVM);
              }
            }
            else
              res = false;
          }
          uow.Context.SaveChanges();
          if (selected.Count > 0)
          {
            var mem = GetDownloadableTicket(model.OrderId, "pdf", filePath);
            Attachment attach = new Attachment(mem, new ContentType(MediaTypeNames.Application.Pdf));
            attach.ContentDisposition.FileName = "Ticket_EventCombo.pdf";
            OrderNotification notification = new OrderNotification(_factory, _dbservice, model.OrderId, baseUrl, attach);
            ISendMailService sendService = CreateSendMailService();
            foreach (var att in selected)
              if (!String.IsNullOrWhiteSpace(att.Email))
              {
                notification.ReceiverName = att.Name;
                sendService.Message.To.Clear();
                sendService.Message.To.Add(new MailAddress(att.Email, att.Name));
                notification.SendNotification(sendService);
              }
          }
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception in the SaveOrderDetails. Transaction rolled back.", ex);
        }
      }
      return res;
    }


    public bool CancelOrder(string orderId, string userId)
    {
      bool res = true;
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
          Order_Detail_T order = orderRepo.Get(filter: (o => ((o.O_Order_Id == orderId) && (o.O_User_Id == userId)))).SingleOrDefault();
          if (order != null)
          {
            order.OrderStateId = 2;
            uow.Context.SaveChanges();

            IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
            var ev = tpdRepo.Get(filter: (t => t.TPD_Order_Id == orderId)).FirstOrDefault().Event;

            var org = ev.Event_Orgnizer_Detail.Where(od => od.DefaultOrg == "Y").FirstOrDefault().Organizer_Master;

            string email = org.Organizer_Email ?? org.AspNetUser.Profiles.FirstOrDefault().Email;
            string name = org.Orgnizer_Name ?? org.AspNetUser.Profiles.FirstOrDefault().FirstName;

            if (!String.IsNullOrWhiteSpace(email))
            {
              ISendMailService sendService = CreateSendMailService();
              sendService.Message.To.Add(new MailAddress(email, name));
              INotification notification = new OrderCancelNotification(_factory, orderId, userId);
              notification.SendNotification(sendService);
            }
          }
          else
            res = false;
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception in the CancelOrder. Transaction rolled back.", ex);
        }
      }
      return res;
    }

    private ISendMailService CreateSendMailService()
    {
      ISendMailService service = new SendMailService();
      service.Message.From = new MailAddress("noreply@eventcombo.com");
      service.Message.ReplyToList.Add(new MailAddress("noreply@eventcombo.com"));
      return service;
    }

    public bool SaveMessage(OrganizerMessageViewModel model)
    {
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var ticket = tpdRepo.Get(filter: (t => t.TPD_Order_Id == model.OrderId)).FirstOrDefault();
      if (ticket == null)
        return false;

      var org = ticket.Event.Event_Orgnizer_Detail.Where(od => od.DefaultOrg == "Y").FirstOrDefault();
      model.EventId = ticket.Event.EventID;
      model.OrganizerId = org == null ? null : (long?)org.OrganizerMaster_Id;
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<Event_OrganizerMessages> messRepo = new GenericRepository<Event_OrganizerMessages>(_factory.ContextFactory);
          Event_OrganizerMessages mess = _mapper.Map<Event_OrganizerMessages>(model);
          messRepo.Insert(mess);
          uow.Context.SaveChanges();

          string email = org.Organizer_Master.Organizer_Email ?? org.Organizer_Master.AspNetUser.Profiles.FirstOrDefault().Email;
          string name = org.Organizer_Master.Orgnizer_Name ?? org.Organizer_Master.AspNetUser.Profiles.FirstOrDefault().FirstName;

          if ((org != null) && (email != null))
          {
            ISendMailService sendService = CreateSendMailService();
            sendService.Message.To.Add(new MailAddress(email, name));
            INotification notification = new OrganizerMessageNotification(_factory, mess);
            notification.SendNotification(sendService);
          }

          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception in the SaveMessage. Transaction rolled back.", ex);
        }
      }

      return true;
    }


    public MemoryStream GetDownloadableTicket(string orderId, string format, string filePath)
    {
      TicketPaymentController tpc = new TicketPaymentController();

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      var ticket = tpdRepo.Get(filter: (t => t.TPD_Order_Id == orderId)).FirstOrDefault();
      var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();

      if ((ticket == null) || (order == null))
        return null;

      var user = ticket.AspNetUser.Profiles.FirstOrDefault();
      string name = !String.IsNullOrWhiteSpace(order.O_First_Name) ? order.O_First_Name : (user == null ? "" : user.FirstName);

      return tpc.generateTicketPDF(ticket.TPD_GUID, ticket.TPD_Event_Id ?? 0, null, name, filePath);
    }
  }
}
