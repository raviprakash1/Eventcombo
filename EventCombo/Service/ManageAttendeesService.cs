using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.DAL;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Net.Mime;
using NPOI.HSSF.Util;
using NPOI.XSSF.UserModel;
using EventCombo.Utils;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.Text;
using EventCombo.Controllers;
using System.Web.Mvc;

namespace EventCombo.Service
{
  public class ManageAttendeesService : IManageAttendeesService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private IDBAccessService _dbservice;
    private ITicketsService _tservice;

    public ManageAttendeesService(IUnitOfWorkFactory factory, IMapper mapper, IDBAccessService dbService, ITicketsService tService)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      if (mapper == null)
        throw new ArgumentNullException("mapper");

      if (dbService == null)
        throw new ArgumentNullException("dbService");

      if (tService == null)
        throw new ArgumentNullException("tService");

      _factory = factory;
      _mapper = mapper;
      _dbservice = dbService;
      _tservice = tService;
    }

    public ManageAttendeesOrdersViewModel GetEventOrdersSummary(long eventId)
    {
      ManageAttendeesOrdersViewModel res = new ManageAttendeesOrdersViewModel();
      res.EventId = eventId;

      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      var ev = eRepo.Get(filter: (e => e.EventID == eventId)).FirstOrDefault();

      var eInfo = _tservice.GetEventSummaryCalculation(eventId, FilterByOrderType.Regular);

      EventOrdersSummuryViewModel ordersTotal = new EventOrdersSummuryViewModel()
      {
        PaymentState = PaymentStates.Total,
        TicketsSold = (eInfo != null ? eInfo.TicketQuantity : 0),
        Amount = (eInfo != null ? eInfo.Price : 0),
        TicketsTotal = ev.Tickets.Sum(tt => tt.Ticket_Quantity_Detail.Sum(q => q.TQD_Quantity)) ?? 0,
        Count = (eInfo != null ? eInfo.OrderQuantity : 0)
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
      res.EventTitle = ev.EventTitle;

      return res;
    }


    public IEnumerable<EventOrderInfoViewModel> GetOrdersForEvent(PaymentStates state, long eventId)
    {
      if (state == PaymentStates.Pending)
        return new List<EventOrderInfoViewModel>();

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      IRepository<BillingAddress> billRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
      IRepository<Ticket> ticketRepo = new GenericRepository<Ticket>(_factory.ContextFactory);

      var orderList = orderRepo.Get(filter: o => o.IsManualOrder == false);
      var OrderIds = orderList.Select(oo => oo.O_Order_Id);
      var tickets = ticketRepo.Get(filter: (t => t.E_Id == eventId));

      IEnumerable<EventOrderInfoViewModel> res = tpdRepo.Get(filter: (t => t.TPD_Event_Id == eventId && OrderIds.Contains(t.TPD_Order_Id)))
        .GroupBy(tt => tt.TPD_Order_Id)
        .Select(ticket => new EventOrderInfoViewModel()
        {
          OrderId = ticket.Key,
          PaymentState = PaymentStates.Completed,
          TicketName = String.Join(", ", tickets.Where(t => ticket.Select(tt => tt.Ticket_Quantity_Detail.TQD_Ticket_Id).Contains(t.T_Id)).Select(t => t.T_name).ToArray()),
          Fee = ticket.Sum(t => t.TPD_EC_Fee * t.TPD_Purchased_Qty) ?? 0,
          PricePaid = ticket.Sum(t=>t.TPD_Amount) ?? 0,
          BuyerName = ticket.FirstOrDefault().AspNetUser.Profiles.Select(p => p.FirstName + " " + p.LastName).FirstOrDefault(),
          BuyerEmail = ticket.FirstOrDefault().AspNetUser.Profiles.Select(p => p.Email).FirstOrDefault(),
          Quantity = ticket.Sum(t => t.TPD_Purchased_Qty) ?? 0,
          Address = ""
        }).ToList();
      foreach (var order in res)
      {
        var orderDB = orderRepo.Get(filter: (o => o.O_Order_Id == order.OrderId)).FirstOrDefault();
        var billingAddressDB = billRepo.Get(filter: (b => b.OrderId == order.OrderId)).FirstOrDefault();

        if (orderDB != null)
        {
            order.OId = orderDB.O_Id;
            order.Date = orderDB.O_OrderDateTime ?? DateTime.Today;
            order.Price = orderDB.O_TotalAmount ?? 0;
            order.PricePaid = order.PricePaid + (orderDB.O_VariableAmount ?? 0);
            order.PriceNet = order.PricePaid - order.Fee;
            order.CustomerEmail = orderDB.O_Email;
            order.Refunded = ((orderDB.OrderStateId ?? 0) == 3 ? order.PricePaid - order.Fee : 0);
            order.Cancelled = ((orderDB.OrderStateId ?? 0) == 2 ? order.PricePaid - order.Fee : 0);
            order.PricePaid = order.PricePaid - order.Refunded - order.Cancelled;
            order.PriceNet = order.PriceNet - order.Refunded - order.Cancelled;
            if (billingAddressDB != null)
            {
                order.Address = billingAddressDB.Address1 +
                    "" + (string.IsNullOrEmpty(billingAddressDB.Address2) ? "" : " " + billingAddressDB.Address2) +
                    ", " + billingAddressDB.City +
                    ", " + billingAddressDB.State +
                    " " + billingAddressDB.Zip;
            }
        }
      }
      res = res.OrderByDescending(oo => oo.Date.Date).ThenBy(oo => oo.OId);
      return res;
    }

    public IEnumerable<EventOrderInfoViewModel> GetOrdersForSaleReport(PaymentStates state, long eventId)
    {
        if (state == PaymentStates.Pending)
            return new List<EventOrderInfoViewModel>();

        IRepository<EventTicket_View> EventTicketRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
        IRepository<BillingAddress> billRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
        IRepository<ShippingAddress> shipRepo = new GenericRepository<ShippingAddress>(_factory.ContextFactory);
        IRepository<TicketBearer_View> tbRepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
        IRepository<Event_VariableDesc> evdRepo = new GenericRepository<Event_VariableDesc>(_factory.ContextFactory);

        var vairableChages = evdRepo.Get(filter: (t => t.Event_Id == eventId));

        IEnumerable<EventOrderInfoViewModel> res = EventTicketRepo.Get(filter: (t => t.EventID == eventId && t.IsManualOrder == false))
        .Select(ticket => new EventOrderInfoViewModel()
        {
            OId = ticket.OId,
            OrderId = ticket.OrderId,
            PaymentState = PaymentStates.Completed,
            TicketName = ticket.TicketName,
            BuyerName = string.Join(", ", tbRepo.Get(a => a.OrderId.Trim() == ticket.OrderId.Trim()).Select(a => a.Name.Trim()).ToArray()),
            BuyerEmail = string.Join(", ", tbRepo.Get(a => a.OrderId.Trim() == ticket.OrderId.Trim()).Select(a => a.Email.Trim()).ToArray()),
            PhoneNumber = string.Join(", ", tbRepo.Get(a => a.OrderId.Trim() == ticket.OrderId.Trim() && a.PhoneNumber != null && a.PhoneNumber != "").Select(a => a.PhoneNumber).ToArray()),
            Quantity = ticket.PurchasedQuantity ?? 0,
            Price = ticket.OrderAmount ?? 0,
            PricePaid = ticket.PaidAmount ?? 0,
            PriceNet = (ticket.PaidAmount ?? 0) - (ticket.ECFeePerTicket * (ticket.PurchasedQuantity ?? 0)) - (ticket.MerchantFeePerTicket * (ticket.PurchasedQuantity ?? 0)),
            Fee = ticket.ECFeePerTicket * (ticket.PurchasedQuantity ?? 0),
            MerchantFee = ticket.MerchantFeePerTicket * (ticket.PurchasedQuantity ?? 0),
            Refunded = ((ticket.OrderStateId ?? 0) == 3 ? (ticket.PaidAmount ?? 0) : 0),
            Cancelled = ((ticket.OrderStateId ?? 0) == 2 ? (ticket.PaidAmount ?? 0) : 0),
            Date = ticket.O_OrderDateTime ?? DateTime.Today,
            PromoCode = ticket.PromoCode ?? "",
            Address = "",
            MailTickets = "N",
            VariableChages = vairableChages.ToList().Select((element) => new Event_VariableDesc
            {
                Variable_Id = element.Variable_Id,
                Price = (ticket.VariableIds.Split(',').Select(Int64.Parse).Contains(element.Variable_Id) ? element.Price : 0),
                Event_Id = element.Event_Id,
                VariableDesc = element.VariableDesc
            }).ToList(),
            VariableIds = ticket.VariableIds
        }).ToList();

        foreach (var order in res)
        {
            var billingAddressDB = billRepo.Get(filter: (b => b.OrderId == order.OrderId)).FirstOrDefault();
            var shippingAddressDB = shipRepo.Get(filter: (b => b.OrderId == order.OrderId)).FirstOrDefault();
            if (shippingAddressDB != null)
            {
                order.MailTickets = shippingAddressDB.Address1 +
                "" + (string.IsNullOrEmpty(shippingAddressDB.Address2) ? "" : " " + shippingAddressDB.Address2) +
                ", " + shippingAddressDB.City +
                ", " + shippingAddressDB.State +
                " " + shippingAddressDB.Zip;
            }
            if (billingAddressDB != null)
            {
                order.Address = billingAddressDB.Address1 +
                "" + (string.IsNullOrEmpty(billingAddressDB.Address2) ? "" : " " + billingAddressDB.Address2) +
                ", " + billingAddressDB.City +
                ", " + billingAddressDB.State +
                " " + billingAddressDB.Zip ;
            }
            order.PricePaid = order.PricePaid - order.Refunded - order.Cancelled;
            order.PriceNet = order.PriceNet - order.Refunded - order.Cancelled;
        }
        res = res.OrderByDescending(oo => oo.Date.Date).ThenBy(oo => oo.OId);
        return res;
    }

    public IEnumerable<EventOrderInfoViewModel> GetManualOrdersForEvent(PaymentStates state, long eventId)
    {
        if (state == PaymentStates.Pending)
            return new List<EventOrderInfoViewModel>();

        IRepository<EventTicket_View> EventTicketRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
        IRepository<TicketAttendee_View> tbRepo = new GenericRepository<TicketAttendee_View>(_factory.ContextFactory);
        var attendees = tbRepo.Get();
        var eventTickets = EventTicketRepo.Get(filter: (t => t.EventID == eventId && t.IsManualOrder == true));
        List<EventOrderInfoViewModel> eventOrderInfoViewModelList = new List<EventOrderInfoViewModel>();
        EventOrderInfoViewModel eventOrderInfoViewModel;
        foreach (var eventTicket in eventTickets)
        {
            var ticketAttendees = tbRepo.Get(a => (a.OrderId == eventTicket.OrderId && a.PurchasedTicketId == eventTicket.TPD_Id && a.TicketBearerId != 0));
            foreach (var ticketAttendee in ticketAttendees)
            {
                eventOrderInfoViewModel = new EventOrderInfoViewModel();
                eventOrderInfoViewModel.OrderId = eventTicket.OrderId;
                eventOrderInfoViewModel.PaymentState = PaymentStates.Completed;
                eventOrderInfoViewModel.TicketName = eventTicket.TicketName;
                eventOrderInfoViewModel.BuyerName = ticketAttendee.Name.Trim();
                eventOrderInfoViewModel.BuyerEmail = ticketAttendee.Email.Trim();
                eventOrderInfoViewModel.PhoneNumber = ticketAttendee.PhoneNumber;
                eventOrderInfoViewModel.Quantity = ticketAttendee.Quantity;
                if(eventTicket.TicketTypeID == 3)
                {
                    eventOrderInfoViewModel.PricePaid = ticketAttendee.Quantity * (ticketAttendee.TPD_Donate ?? 0) / (ticketAttendee.TPD_Purchased_Qty ?? 0);
                }
                else
                {
                    eventOrderInfoViewModel.PricePaid = ticketAttendee.Quantity * (ticketAttendee.TPD_Amount ?? 0) / (ticketAttendee.TPD_Purchased_Qty ?? 0);
                }
                eventOrderInfoViewModel.PricePaid = decimal.Truncate(eventOrderInfoViewModel.PricePaid * 100) / 100;
                eventOrderInfoViewModel.Date = eventTicket.O_OrderDateTime ?? DateTime.Today;
                eventOrderInfoViewModelList.Add(eventOrderInfoViewModel);
            }
        }
        return eventOrderInfoViewModelList;
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

      foreach (var attendee in tbRepo.Get(filter: (a => a.OrderId == orderId)))
        res.Attendees.Add(_mapper.Map<AttendeeViewModel>(attendee));

      return res;
    }

    public EventOrderDetailViewModel GetOrderDetails(string orderId, long eventId)
    {
        IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
        IRepository<EventTicket_View> EventTicketRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);

        var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();
        if (order == null)
            throw new NullReferenceException(String.Format("Order {0} not found.", orderId));

        var TicketNames = EventTicketRepo.Get(filter: (t => t.EventID == eventId && t.OrderId == orderId)).Select(t => t.TicketName);

        EventOrderDetailViewModel details = new EventOrderDetailViewModel() { OrderId = orderId };
        details.Payment = _dbservice.GetPaymentInfo(orderId);

        IRepository<Models.Profile> userRepo = new GenericRepository<Models.Profile>(_factory.ContextFactory);
        var user = userRepo.Get(filter: (u => u.UserID == order.O_User_Id)).First();
        details.Name = user.FirstName + (string.IsNullOrEmpty(user.LastName) ? "" : " " + user.LastName);
        details.Email = user.Email;
        details.PhoneNumber = user.MainPhone;
        details.Address = user.StreetAddressLine1 +
                        "" + (string.IsNullOrEmpty(user.StreetAddressLine2) ? "" : " " + user.StreetAddressLine1) +
                        "" + (string.IsNullOrEmpty(user.City) ? "" : ", " + user.City) +
                        "" + (string.IsNullOrEmpty(user.State) ? "" : ", " + user.State) +
                        " " + user.Zip;

        IRepository<Ticket_Purchased_Detail> ptRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
        var tickets = ptRepo.Get(filter: (t => ((t.TPD_Order_Id == orderId) && (t.TPD_User_Id == order.O_User_Id))));
        details.EventLocation = String.Join("; ", tickets.Select(t => t.Ticket_Quantity_Detail.Address.ConsolidateAddress).Distinct().ToArray());
        details.EventDate = String.Join("; ", tickets.Select(t => t.Ticket_Quantity_Detail.Publish_Event_Detail.PE_MultipleVenue_id > 0 ?
                t.Ticket_Quantity_Detail.Publish_Event_Detail.MultipleEvent.StartingFrom :
                t.Ticket_Quantity_Detail.Publish_Event_Detail.EventVenue.EventStartDate).Distinct().ToArray());

        IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
        foreach (var att in attRepo.Get(filter: (tb => ((tb.OrderId == orderId) && (tb.UserId == order.O_User_Id)))))
            details.Attendees.Add(_mapper.Map<AttendeeViewModel>(att));

        details.TicketNames = (TicketNames == null ? "" : string.Join(", ", TicketNames.ToArray()));

        IRepository<TicketAttendee_View> tbRepo = new GenericRepository<TicketAttendee_View>(_factory.ContextFactory);
        IRepository<Promo_Code> pcRepo = new GenericRepository<Promo_Code>(_factory.ContextFactory);

        List<TicketAttendeeViewModel> ticketAttendeeList = new List<TicketAttendeeViewModel>();
        TicketAttendeeViewModel ticketAttendeeViewModel;
        if (order.IsManualOrder)
        {
            var ticketAttendees = tbRepo.Get(a => (a.OrderId == orderId && a.TicketBearerId != 0));
            var ticketAll = tickets.Select(tp => new
            {
                AddressId = tp.Ticket_Quantity_Detail.TQD_AddressId,
                ConsolidateAddress = tp.Ticket_Quantity_Detail.TQD_AddressId == 0 ? "" : tp.Ticket_Quantity_Detail.Address.ConsolidateAddress,
                StartDateStr = tp.Ticket_Quantity_Detail.TQD_StartDate,
                StartDate = DateTime.Parse(tp.Ticket_Quantity_Detail.TQD_StartDate)
            }).Distinct().OrderBy(s => s.StartDate);

            foreach (var ticket in ticketAll)
            {
                foreach (var item in ticketAttendees.Where(ta => tickets.Where(t => ((t.Ticket_Quantity_Detail.TQD_AddressId == ticket.AddressId) && (t.Ticket_Quantity_Detail.TQD_StartDate == ticket.StartDateStr)))
                                                    .OrderBy(o => o.Ticket_Quantity_Detail.TQD_Ticket_Id).Select(tt => tt.TPD_Id).Contains(ta.PurchasedTicketId)))
                {
                    ticketAttendeeViewModel = new TicketAttendeeViewModel();
                    ticketAttendeeViewModel.Address = ticket.ConsolidateAddress;
                    ticketAttendeeViewModel.StartDate = ticket.StartDate;
                    ticketAttendeeViewModel.TicketId = item.PurchasedTicketId;
                    ticketAttendeeViewModel.TicketName = item.T_name;
                    ticketAttendeeViewModel.AttendeeName = item.Name;
                    ticketAttendeeViewModel.AttendeeEmail = item.Email;
                    ticketAttendeeViewModel.AttendeePhone = item.PhoneNumber;
                    ticketAttendeeViewModel.Quantity = item.Quantity;
                    ticketAttendeeViewModel.Price = (item.TPD_Amount > 0 ? decimal.Truncate((item.Quantity * (item.TPD_Amount ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : item.TPD_Donate > 0 ? decimal.Truncate((item.Quantity * (item.TPD_Donate ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : 0);
                    ticketAttendeeViewModel.PriceStr = (item.TPD_Amount > 0 ? "$" + decimal.Truncate((item.Quantity * (item.TPD_Amount ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : item.TPD_Donate > 0 ? "$" + decimal.Truncate((item.Quantity * (item.TPD_Donate ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : "Free");
                    var PromoCode = pcRepo.Get(filter: p => p.PC_id == item.TPD_PromoCodeID && p.PC_Eventid == item.TPD_Event_Id).Select(pc => pc.PC_Code).FirstOrDefault();
                    ticketAttendeeViewModel.PromoCode = (string.IsNullOrEmpty(PromoCode) ? "" : PromoCode);
                    ticketAttendeeViewModel.PromoCodePrice = item.TPD_PromoCodeAmount ?? 0;
                    ticketAttendeeList.Add(ticketAttendeeViewModel);
                }
            }
        }
        else
        {
            var ticketAll = tickets.Select(tp => new
            {
                AddressId = tp.Ticket_Quantity_Detail.TQD_AddressId,
                ConsolidateAddress = tp.Ticket_Quantity_Detail.TQD_AddressId == 0 ? "" : tp.Ticket_Quantity_Detail.Address.ConsolidateAddress,
                StartDateStr = tp.Ticket_Quantity_Detail.TQD_StartDate,
                StartDate = DateTime.Parse(tp.Ticket_Quantity_Detail.TQD_StartDate)
            }).Distinct().OrderBy(s => s.StartDate);

            foreach (var ticket in ticketAll)
            {
                foreach (var item in tickets.Where(t => ((t.Ticket_Quantity_Detail.TQD_AddressId == ticket.AddressId) && (t.Ticket_Quantity_Detail.TQD_StartDate == ticket.StartDateStr)))
                                            .OrderBy(o => o.Ticket_Quantity_Detail.TQD_Ticket_Id))
                {
                    ticketAttendeeViewModel = new TicketAttendeeViewModel();
                    ticketAttendeeViewModel.Address = ticket.ConsolidateAddress;
                    ticketAttendeeViewModel.StartDate = ticket.StartDate;
                    ticketAttendeeViewModel.TicketId = item.Ticket_Quantity_Detail.Ticket.T_Id;
                    ticketAttendeeViewModel.TicketName = item.Ticket_Quantity_Detail.Ticket.T_name;
                    ticketAttendeeViewModel.AttendeeName = item.AspNetUser.Profiles.FirstOrDefault().FirstName;
                    ticketAttendeeViewModel.AttendeeEmail = item.AspNetUser.Profiles.FirstOrDefault().Email;
                    ticketAttendeeViewModel.AttendeePhone = item.AspNetUser.Profiles.FirstOrDefault().MainPhone;
                    ticketAttendeeViewModel.Quantity = item.TPD_Purchased_Qty ?? 0;
                    ticketAttendeeViewModel.Price = (item.TPD_Amount > 0 ? decimal.Truncate(((item.TPD_Purchased_Qty ?? 0) * (item.TPD_Amount ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : item.TPD_Donate > 0 ? decimal.Truncate(((item.TPD_Purchased_Qty ?? 0) * (item.TPD_Donate ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : 0);
                    ticketAttendeeViewModel.PriceStr = (item.TPD_Amount > 0 ? "$" + decimal.Truncate(((item.TPD_Purchased_Qty ?? 0) * (item.TPD_Amount ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : item.TPD_Donate > 0 ? "$" + decimal.Truncate(((item.TPD_Purchased_Qty ?? 0) * (item.TPD_Donate ?? 0) / (item.TPD_Purchased_Qty ?? 0)) * 100) / 100 : "Free");
                    var PromoCode = pcRepo.Get(filter: p => p.PC_id == item.TPD_PromoCodeID && p.PC_Eventid == item.TPD_Event_Id).Select(pc => pc.PC_Code).FirstOrDefault();
                    ticketAttendeeViewModel.PromoCode = (string.IsNullOrEmpty(PromoCode) ? "" : PromoCode);
                    ticketAttendeeViewModel.PromoCodePrice = item.TPD_PromoCodeAmount ?? 0;
                    ticketAttendeeList.Add(ticketAttendeeViewModel);
                }
            }
        }
        details.TicketAttendees = ticketAttendeeList;
        details.OrderTotalAmount = order.O_TotalAmount ?? 0;
        IRepository<Event_VariableDesc> evdRepo = new GenericRepository<Event_VariableDesc>(_factory.ContextFactory);

        var variableChages = evdRepo.Get(filter: (t => t.Event_Id == eventId));

        details.VariableChages = variableChages.Where(x => order.O_VariableId.Split(',').Select(Int64.Parse).Contains(x.Variable_Id)).ToList();

        return details;
    }

    public bool SendConfirmations(string orderId, string baseUrl, string filePath, bool IsManualOrder = false)
    {
      if (String.IsNullOrWhiteSpace(orderId))
        throw new ArgumentNullException("orderId");

      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();
      if (order == null)
        return false;

      List<MailAddress> addresses = new List<MailAddress>();
      IRepository<TicketBearer_View> attRepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
      foreach (var attendee in attRepo.Get(filter: (a => a.OrderId == orderId && a.TicketbearerId != 0)))
        if (!String.IsNullOrWhiteSpace(attendee.Email))
          if (!addresses.Any(a => a.Address == attendee.Email))
            addresses.Add(new MailAddress(attendee.Email, attendee.Name));

      if (addresses.Count <= 0)
        return false;
      else
      {
        var mem = _tservice.GetDownloadableTicket(orderId, "pdf", filePath, IsManualOrder);
        Attachment attach = new Attachment(mem, new ContentType(MediaTypeNames.Application.Pdf));
        attach.ContentDisposition.FileName = "Ticket_EventCombo.pdf";
        INotification notification = new OrderNotification(_factory, _dbservice, orderId, baseUrl, attach, _tservice);
        ISendMailService sendService = new SendMailService();
        INotificationSender sender = new NotificationSender(notification, sendService);
        sender.SendSeparately(addresses);
        INotification organizerNotification = new OrderOrganizerNotification(_factory, _dbservice, orderId, baseUrl, attach, IsManualOrder);
        INotificationSender organizerSender = new NotificationSender(organizerNotification, sendService);
        organizerNotification.SendNotification(new SendMailService());
        return true;
      }

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

    public MemoryStream GetDownloadableOrderList(PaymentStates state, long eventId, string format)
    {
      format = format.Trim().ToLower();
      if ((format != "csv") && (format != "txt") && (format != "xls"))
        return null;
      IEnumerable<EventOrderInfoViewModel> orders = GetOrdersForEvent(state, eventId);
      if (format == "csv")
        return OrderListToCSV(orders, ",");
      if (format == "xls")
        return OrderListToXLS(orders);
      return OrderListToTXT(orders);
    }

    public MemoryStream GetDownloadableSaleReport(PaymentStates state, long eventId, string format)
    {
        format = format.Trim().ToLower();
        if ((format != "csv") && (format != "txt") && (format != "xls"))
            return null;
        IEnumerable<EventOrderInfoViewModel> orders = GetOrdersForSaleReport(state, eventId);
        IRepository<Event> EventRepo = new GenericRepository<Event>(_factory.ContextFactory);

        var eventTitle = EventRepo.Get(filter: (e => e.EventID == eventId)).Select(x => new { x.EventTitle }).FirstOrDefault();
        var SaleReportTitle = (eventTitle == null ? "" : "Ticket Sales for " + eventTitle.EventTitle);
        if (format == "csv")
            return SaleReportToCSV(orders, ",", SaleReportTitle);
        if (format == "xls")
            return SaleReportToXLS(orders, SaleReportTitle);
        return SaleReportToTXT(orders, SaleReportTitle);
    }

    private ICell AddStyledCell(IRow row, int cellnum, ICellStyle style, CellType ctype = CellType.String)
    {
      ICell cell = row.CreateCell(cellnum, ctype);
      cell.CellStyle = style;
      return cell;
    }

    private MemoryStream OrderListToXLS(IEnumerable<EventOrderInfoViewModel> orders)
    {
      MemoryStream res = new MemoryStream();
      IWorkbook wb = new HSSFWorkbook();
      ICellStyle style = wb.CreateCellStyle();
      style.BorderBottom = BorderStyle.Thin;
      style.BorderTop = BorderStyle.Thin;
      style.BorderLeft = BorderStyle.Thin;
      style.BorderRight = BorderStyle.Thin;
      ICellStyle hstyle = wb.CreateCellStyle();
      hstyle.BorderBottom = BorderStyle.Thin;
      hstyle.BorderTop = BorderStyle.Thin;
      hstyle.BorderLeft = BorderStyle.Thin;
      hstyle.BorderRight = BorderStyle.Thin;
      hstyle.Alignment = HorizontalAlignment.Center;
      IFont bfont = wb.CreateFont();
      bfont.Boldweight = (short)FontBoldWeight.Bold;
      hstyle.SetFont(bfont);
      ICellStyle datestyle = wb.CreateCellStyle();
      datestyle.BorderBottom = BorderStyle.Thin;
      datestyle.BorderTop = BorderStyle.Thin;
      datestyle.BorderLeft = BorderStyle.Thin;
      datestyle.BorderRight = BorderStyle.Thin;
      datestyle.DataFormat = wb.CreateDataFormat().GetFormat("MMMM dd, yyyy");
      ICellStyle currencyStyle = wb.CreateCellStyle();
      currencyStyle.BorderBottom = BorderStyle.Thin;
      currencyStyle.BorderTop = BorderStyle.Thin;
      currencyStyle.BorderLeft = BorderStyle.Thin;
      currencyStyle.BorderRight = BorderStyle.Thin;
      currencyStyle.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");


      ISheet sheet = wb.CreateSheet("Orders");
      IRow row = sheet.CreateRow(0);
      AddStyledCell(row, 0, hstyle).SetCellValue("ORDER #");
      AddStyledCell(row, 1, hstyle).SetCellValue("TICKET BUYER");
      AddStyledCell(row, 2, hstyle).SetCellValue("TICKET NAME");
      AddStyledCell(row, 3, hstyle).SetCellValue("QUANTITY");
      AddStyledCell(row, 4, hstyle).SetCellValue("PRICE");
      AddStyledCell(row, 5, hstyle).SetCellValue("PRICE PAID");
      AddStyledCell(row, 6, hstyle).SetCellValue("NET PRICE");
      AddStyledCell(row, 7, hstyle).SetCellValue("CUSTOMER EMAIL");
      AddStyledCell(row, 8, hstyle).SetCellValue("ADDRESS");
      AddStyledCell(row, 9, hstyle).SetCellValue("DATE");
      AddStyledCell(row, 10, hstyle).SetCellValue("PAYMENT");
      var i = 1;
      foreach (var order in orders)
      {
        row = sheet.CreateRow(i++);
        AddStyledCell(row, 0, style).SetCellValue(order.OrderId);
        AddStyledCell(row, 1, style).SetCellValue(order.BuyerName);
        AddStyledCell(row, 2, style).SetCellValue(order.TicketName);
        AddStyledCell(row, 3, style).SetCellValue(order.Quantity);
        AddStyledCell(row, 4, currencyStyle).SetCellValue((double)order.Price);
        AddStyledCell(row, 5, currencyStyle).SetCellValue((double)order.PricePaid);
        AddStyledCell(row, 6, currencyStyle).SetCellValue((double)order.PriceNet);
        AddStyledCell(row, 7, datestyle).SetCellValue(order.CustomerEmail);
        AddStyledCell(row, 8, datestyle).SetCellValue(order.Address);
        AddStyledCell(row, 9, datestyle).SetCellValue(order.Date.ToString("MMM dd, yyyy hh:mm:ss tt"));
        AddStyledCell(row, 10, style).SetCellValue(order.Cancelled > 0 ? "Cancelled" : order.Refunded > 0 ? "Refunded" : order.PaymentState.ToString());
      }
      for (i = 0; i <= 10; i++)
      {
        sheet.AutoSizeColumn(i);
        sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
      }
      wb.Write(res);
      res.Position = 0;
      return res;
    }

    private MemoryStream OrderListToTXT(IEnumerable<EventOrderInfoViewModel> orders)
    {
      MemoryStream res = new MemoryStream();
      StreamWriter rw = new StreamWriter(res);

      rw.Write("  ORDER #  |        TICKET BUYER        |        TICKET NAME         | QUANTITY |  PRICE  |PRICE PAID|NET PRICE|       CUSTOMER EMAIL       |        ADDRESS             |   DATE   |  PAYMENT  ");
      rw.WriteLine();
      string str = "";
      foreach (var order in orders)
      {
        rw.Write(order.OrderId + new String(' ', 11 - order.OrderId.Length) + "|");
        str = order.BuyerName;
        while (str.Length > 28)
        {
          rw.Write(str.Substring(0, 28) + "|                            |          |         |          |         |                            |                            |          |           ");
          rw.WriteLine();
          rw.Write("           |");
          str = str.Substring(28, str.Length - 28);
        }
        rw.Write(str + new String(' ', 28 - str.Length) + "|");
        str = order.TicketName;
        while (str.Length > 28)
        {
            rw.Write(str.Substring(0, 28) + "|          |         |          |         |                            |                            |          |           ");
            rw.WriteLine();
            rw.Write("           |                            |");
            str = str.Substring(28, str.Length - 28);
        }
        rw.Write(str + new String(' ', 28 - str.Length) + "|");
        str = order.Quantity.ToString();
        rw.Write(str + new String(' ', 10 - str.Length) + "|");
        str = "$" + order.Price.ToString("N2");
        rw.Write(str + new String(' ', 9 - str.Length) + "|");
        str = "$" + order.PricePaid.ToString("N2");
        rw.Write(str + new String(' ', 10 - str.Length) + "|");
        str = "$" + order.PriceNet.ToString("N2");
        rw.Write(str + new String(' ', 9 - str.Length) + "|");
        str = order.CustomerEmail;
        while (str.Length > 28)
        {
            rw.Write(str.Substring(0, 28) + "|                            |          |           ");
            rw.WriteLine();
            rw.Write("           |                            |                            |          |         |          |         |");
            str = str.Substring(28, str.Length - 28);
        }
        rw.Write(str + new String(' ', 28 - str.Length) + "|");
        str = order.Address;
        while (str.Length > 28)
        {
            rw.Write(str.Substring(0, 28) + "|          |           ");
            rw.WriteLine();
            rw.Write("           |                            |                            |          |         |          |         |                            |");
            str = str.Substring(28, str.Length - 28);
        }
        rw.Write(str + new String(' ', 28 - str.Length) + "|");
        str = order.Date.ToString("MMM dd, yyyy hh:mm:ss tt");
        rw.Write(str + new String(' ', 10 - str.Length) + "|");
        str = order.Cancelled > 0 ? "Cancelled" : order.Refunded > 0 ? "Refunded" : order.PaymentState.ToString();
        rw.Write(str);
        rw.WriteLine();
      }

      rw.Flush();

      res.Position = 0;
      return res;
    }

    private MemoryStream OrderListToCSV(IEnumerable<EventOrderInfoViewModel> orders, string delimiter)
    {
      MemoryStream res = new MemoryStream();
      StreamWriter rw = new StreamWriter(res);

      rw.Write("ORDER #" + delimiter);
      rw.Write("TICKET BUYER" + delimiter);
      rw.Write("TICKET NAME" + delimiter);
      rw.Write("QUANTITY" + delimiter);
      rw.Write("PRICE" + delimiter);
      rw.Write("PRICE PAID" + delimiter);
      rw.Write("NET PRICE" + delimiter);
      rw.Write("CUSTOMER EMAIL" + delimiter);
      rw.Write("ADDRESS" + delimiter);
      rw.Write("DATE" + delimiter);
      rw.Write("PAYMENT");
      rw.WriteLine();

      foreach (var order in orders)
      {
        rw.Write(order.OrderId + delimiter);
        rw.Write("\"" + order.BuyerName + "\"" + delimiter);
        rw.Write("\"" + order.TicketName + "\"" + delimiter);
        rw.Write(order.Quantity.ToString() + delimiter);
        rw.Write("$" + order.Price.ToString("N2") + delimiter);
        rw.Write("$" + order.PricePaid.ToString("N2") + delimiter);
        rw.Write("$" + order.PriceNet.ToString("N2") + delimiter);
        rw.Write("\"" + order.CustomerEmail + "\"" + delimiter);
        rw.Write("\"" + order.Address + "\"" + delimiter);
        rw.Write("\"" + order.Date.ToString("MMM dd, yyyy hh:mm:ss tt") + "\"" + delimiter);
        rw.Write(order.Cancelled > 0 ? "Cancelled" : order.Refunded > 0 ? "Refunded" : order.PaymentState.ToString());
        rw.WriteLine();
      }

      rw.Flush();

      res.Position = 0;
      return res;
    }

    public AddAttandeeOrder PrepareAddAttendeeOrder(long eventId, string userId)
    {
      AddAttandeeOrder aa = new AddAttandeeOrder();
      IRepository<Ticket> tRepo = new GenericRepository<Ticket>(_factory.ContextFactory);
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<PaymentType> ptRepo = new GenericRepository<PaymentType>(_factory.ContextFactory);
      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);

      aa.EventId = eventId;

      foreach (var ticket in tRepo.Get(filter: (t => t.E_Id == eventId), orderBy: (query => query.OrderBy(t => t.TicketTypeID))))
      {
        TicketInfoAddViewModel ti = new TicketInfoAddViewModel();
        ti.Ticket = _mapper.Map<TicketViewModel>(ticket);
        ti.TicketSold = tpdRepo.Get(filter: (tpd => ((tpd.TPD_Event_Id == eventId) && (tpd.Ticket_Quantity_Detail.Ticket.T_Id == ticket.T_Id)))).Sum(tpd => tpd.TPD_Purchased_Qty);
        ti.TicketSold = ti.TicketSold ?? 0;
        aa.Tickets.Add(ti);
      }

      foreach (var pt in ptRepo.Get(filter: (p => p.Active), orderBy: (query => query.OrderBy(p => p.PaymentTypeId))))
      {
        if (aa.PaymentType == null)
          aa.PaymentType = _mapper.Map<PaymentTypeViewModel>(pt);
        aa.AvailablePT.Add(_mapper.Map<PaymentTypeViewModel>(pt));
      }

      var profile = _dbservice.GetUserProfileById(userId);
      if (profile != null)
      {
        aa.Email = profile.Email;
        aa.FirstName = profile.FirstName;
        aa.LastName = profile.LastName;
      }

      var ev = eRepo.Get(filter: (e => e.EventID == eventId)).FirstOrDefault();
      if (ev != null)
      {
        aa.Title = ev.EventTitle;
        //aa.ImageUrl = ev.
      }
      return aa;
    }

    public string CreateManualOrder(AddAttandeeOrder model, string userId)
    {
      string newOrderId;
      using(var uow = _factory.GetUnitOfWork())
      try
      {
        IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
        IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
        IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);
        IRepository<TicketBearer> tbRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
        IRepository<TicketAttendee> taRepo = new GenericRepository<TicketAttendee>(_factory.ContextFactory);
        Guid guidId = Guid.NewGuid();
        Order_Detail_T order = new Order_Detail_T()
        {
          O_User_Id = userId,
          O_Email = model.Email,
          O_First_Name = model.FirstName,
          O_Last_Name = model.LastName,
          O_OrderDateTime = DateTime.Now,
          O_TotalAmount = model.Tickets.Sum(t => t.Paid),
          O_OrderAmount = model.Tickets.Sum(t => t.Paid),
          OrderStateId = 1,
          PaymentTypeId = model.PaymentType.PaymentTypeId,
          Note = model.Note,
          O_VariableId = "0",
          O_VariableAmount = 0,
          O_PromoCodeId = 0,
          IsManualOrder = true,
        };
        oRepo.Insert(order);
        uow.Context.SaveChanges();
        //((IObjectContextAdapter)uow.Context).ObjectContext.ObjectStateManager.ChangeObjectState(order, System.Data.Entity.EntityState.Modified);
        //uow.RefreshEntities();
        oRepo.Reload(order); // Need to update values after trigger
        _factory.ContextFactory.GetContext().Entry(order).Reference(o => o.AspNetUser).Load();
        _factory.ContextFactory.GetContext().Entry(order).Reference(o => o.PaymentType).Load();
        _factory.ContextFactory.GetContext().Entry(order).Reference(o => o.OrderState).Load();
        newOrderId = order.O_Order_Id;
        if (String.IsNullOrWhiteSpace(newOrderId))
          throw new Exception("Order Id is empty.");
        foreach(var ticket in model.Tickets)
        {
          var tq = tqdRepo.Get(filter: (tqd => (tqd.TQD_Ticket_Id == ticket.Ticket.T_Id))).FirstOrDefault();
          Ticket_Purchased_Detail tpd = new Ticket_Purchased_Detail()
          {
            TPD_Event_Id = model.EventId,
            TPD_Order_Id = newOrderId,
            TPD_Amount = tq.Ticket.TicketTypeID == 3 ? 0 : ticket.Paid,
            TPD_Donate = tq.Ticket.TicketTypeID == 3 ? ticket.Paid : 0,
            TPD_Purchased_Qty = ticket.Quantity,
            TPD_TQD_Id = tq.TQD_Id,            
            TPD_GUID = guidId.ToString(),
            TPD_User_Id = userId,
            TPD_EC_Fee = 0
          };
          tpdRepo.Insert(tpd);
        }
        uow.Context.SaveChanges();
        foreach (var attendee in model.Attendees)
        {
          if (attendee.Quantity <= 0)
            continue;
          long ticketBearerId;
          TicketBearer tb = new TicketBearer()
          {
              OrderId = newOrderId,
              Name = attendee.Name,
              Email = attendee.Email,
              PhoneNumber = (string.IsNullOrEmpty(attendee.PhoneNumber) ? "" : attendee.PhoneNumber),
              Guid = guidId.ToString(),
              UserId = userId
          };
          tbRepo.Insert(tb);
          uow.Context.SaveChanges();
          ticketBearerId = tb.TicketbearerId;
          var tQ = tqdRepo.Get(filter: (tqd => (tqd.TQD_Ticket_Id == attendee.TicketId))).FirstOrDefault();
          long purchasedTicketId = 0;
          if (tQ != null)
          {
              var tPD = tpdRepo.Get(filter: (tqd => (tqd.TPD_TQD_Id == tQ.TQD_Id && tqd.TPD_GUID == guidId.ToString()))).FirstOrDefault();
              if (tPD != null)
              {
                  purchasedTicketId = tPD.TPD_Id;
              }
          }

          TicketAttendee ta = new TicketAttendee()
          {
              PurchasedTicketId = purchasedTicketId,
              TicketBearerId = ticketBearerId,
              Quantity = attendee.Quantity
          };
          taRepo.Insert(ta);
        }
        uow.Context.SaveChanges();
        uow.Commit();
      }
      catch (Exception ex)
      {
        uow.Rollback();
        throw new Exception("Error during CreateManualOrder", ex);
      }
      return newOrderId;
    }
    private MemoryStream SaleReportToXLS(IEnumerable<EventOrderInfoViewModel> orders, string ReportTitle)
    {
        MemoryStream res = new MemoryStream();
        IWorkbook wb = new HSSFWorkbook();
        ICellStyle style = wb.CreateCellStyle();
        style.BorderBottom = BorderStyle.Thin;
        style.BorderTop = BorderStyle.Thin;
        style.BorderLeft = BorderStyle.Thin;
        style.BorderRight = BorderStyle.Thin;

        ICellStyle hstyle = wb.CreateCellStyle();
        hstyle.BorderBottom = BorderStyle.Thin;
        hstyle.BorderTop = BorderStyle.Thin;
        hstyle.BorderLeft = BorderStyle.Thin;
        hstyle.BorderRight = BorderStyle.Thin;
        hstyle.Alignment = HorizontalAlignment.Center;
        IFont bfont = wb.CreateFont();
        bfont.Boldweight = (short)FontBoldWeight.Bold;
        hstyle.SetFont(bfont);

        ICellStyle datestyle = wb.CreateCellStyle();
        datestyle.BorderBottom = BorderStyle.Thin;
        datestyle.BorderTop = BorderStyle.Thin;
        datestyle.BorderLeft = BorderStyle.Thin;
        datestyle.BorderRight = BorderStyle.Thin;
        datestyle.DataFormat = wb.CreateDataFormat().GetFormat("MMMM dd, yyyy");

        ICellStyle currencyStyle = wb.CreateCellStyle();
        currencyStyle.BorderBottom = BorderStyle.Thin;
        currencyStyle.BorderTop = BorderStyle.Thin;
        currencyStyle.BorderLeft = BorderStyle.Thin;
        currencyStyle.BorderRight = BorderStyle.Thin;
        currencyStyle.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");

        ICellStyle Titlestyle = wb.CreateCellStyle();
        Titlestyle.BorderBottom = BorderStyle.Thin;
        Titlestyle.BorderTop = BorderStyle.Thin;
        Titlestyle.BorderLeft = BorderStyle.Thin;
        Titlestyle.BorderRight = BorderStyle.Thin;
        Titlestyle.Alignment = HorizontalAlignment.Center;
        Titlestyle.SetFont(bfont);

        ISheet sheet = wb.CreateSheet("Orders");
        IRow row = sheet.CreateRow(0);
        AddStyledCell(row, 0, Titlestyle).SetCellValue(ReportTitle);
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 13));

        row = sheet.CreateRow(1);
        AddStyledCell(row, 0, hstyle).SetCellValue("ORDER #");
        AddStyledCell(row, 1, hstyle).SetCellValue("DATE");
        AddStyledCell(row, 2, hstyle).SetCellValue("ATTENDEE");
        AddStyledCell(row, 3, hstyle).SetCellValue("TICKET NAME");
        AddStyledCell(row, 4, hstyle).SetCellValue("QUANTITY");
        AddStyledCell(row, 5, hstyle).SetCellValue("GROSS PAID");
        AddStyledCell(row, 6, hstyle).SetCellValue("NET EVENT REVENUE");
        AddStyledCell(row, 7, hstyle).SetCellValue("PROMO CODE");
        AddStyledCell(row, 8, hstyle).SetCellValue("REFUNDED");
        AddStyledCell(row, 9, hstyle).SetCellValue("CANCELLED");
        AddStyledCell(row, 10, hstyle).SetCellValue("ATTENDEE NUMBER");
        AddStyledCell(row, 11, hstyle).SetCellValue("ATTENDEE EMAIL");
        AddStyledCell(row, 12, hstyle).SetCellValue("BILLING ADDRESS");
        AddStyledCell(row, 13, hstyle).SetCellValue("MAIL TICKETS");
        var variableCount = 0;
        if (orders.FirstOrDefault() != null)
        {
            foreach (var variable in orders.FirstOrDefault().VariableChages)
            {
                variableCount += 1;
                AddStyledCell(row, 13 + variableCount, hstyle).SetCellValue(variable.VariableDesc);
            }
        }
        var i = 2;
        string tempOrderId = "";
        foreach (var order in orders)
        {
            row = sheet.CreateRow(i++);
            AddStyledCell(row, 0, style).SetCellValue(order.OrderId);
            AddStyledCell(row, 1, datestyle).SetCellValue(order.Date.ToString("MMM dd, yyyy hh:mm:ss tt"));
            AddStyledCell(row, 2, style).SetCellValue(order.BuyerName);
            AddStyledCell(row, 3, style).SetCellValue(order.TicketName);
            AddStyledCell(row, 4, style).SetCellValue(order.Quantity);
            AddStyledCell(row, 5, currencyStyle).SetCellValue((double)order.PricePaid);
            AddStyledCell(row, 6, currencyStyle).SetCellValue((double)order.PriceNet);
            AddStyledCell(row, 7, style).SetCellValue(order.PromoCode);
            AddStyledCell(row, 8, style).SetCellValue((order.Refunded > 0 ? "Yes" : ""));
            AddStyledCell(row, 9, style).SetCellValue((order.Cancelled > 0 ? "Yes" : ""));
            AddStyledCell(row, 10, style).SetCellValue(order.PhoneNumber);
            AddStyledCell(row, 11, style).SetCellValue(order.BuyerEmail);
            AddStyledCell(row, 12, style).SetCellValue(order.Address);
            AddStyledCell(row, 13, style).SetCellValue(order.MailTickets.ToString());
            variableCount = 0;
            if (tempOrderId != order.OrderId)
            {
                foreach (var variable in order.VariableChages)
                {
                    variableCount += 1;
                    AddStyledCell(row, 13 + variableCount, currencyStyle).SetCellValue((double)(variable.Price ?? 0));
                }
                tempOrderId = order.OrderId;
            }
            else
            {
                foreach (var variable in order.VariableChages)
                {
                    variableCount += 1;
                    AddStyledCell(row, 13 + variableCount, currencyStyle).SetCellValue(0.0);
                }
            }
        }
        for (i = 0; i <= 13 + variableCount; i++)
        {
            sheet.AutoSizeColumn(i);
            sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
        }
        wb.Write(res);
        res.Position = 0;
        return res;
    }

    private MemoryStream SaleReportToTXT(IEnumerable<EventOrderInfoViewModel> orders, string ReportTitle)
    {
        MemoryStream res = new MemoryStream();
        StreamWriter rw = new StreamWriter(res);

        rw.Write(ReportTitle);
        rw.WriteLine();
        var variableCount = 0;
        var variableStr = "";
        if (orders.FirstOrDefault() != null)
        {
            foreach (var variable in orders.FirstOrDefault().VariableChages)
            {
                variableCount += 1;
                variableStr += " | " + variable.VariableDesc + (variable.VariableDesc.Length < (variable.Price ?? 0).ToString("N2").Length + 1 ? new String(' ', (variable.Price ?? 0).ToString("N2").Length + 1 - variable.VariableDesc.Length) : "");
            }
        }
        rw.Write("  ORDER #  |    DATE     |          ATTENDEE          |        TICKET NAME         | QUANTITY |  GROSS PAID  |NET EVENT REVENUE|EVENTCOMBO FEE|EVENTCOMBO MERCHANT FEE|PROMO CODE|REFUNDED|CANCELLED|ATTENDEE NUMBER|       ATTENDEE EMAIL       |          ADDRESS           |          MAIL TICKETS      " + variableStr);
        rw.WriteLine();
        string str = "";
        string tempOrderId = "";
        foreach (var order in orders)
        {
            rw.Write(order.OrderId + new String(' ', 11 - order.OrderId.Length) + "|");
            str = order.Date.ToString("MMM dd, yyyy hh:mm:ss tt");
            rw.Write(str + new String(' ', 26 - str.Length) + "|");
            str = order.BuyerName;
            while (str.Length > 28)
            {
                rw.Write(str.Substring(0, 28) + "|                            |          |              |                 |         |        |        |               |                            |                            |                           ");
                rw.WriteLine();
                rw.Write("           |             |");
                str = str.Substring(28, str.Length - 28);
            }
            rw.Write(str + new String(' ', 28 - str.Length) + "|");
            str = order.TicketName;
            while (str.Length > 28)
            {
                rw.Write(str.Substring(0, 28) + "|          |              |                 |         |        |        |               |                            |                            |                           ");
                rw.WriteLine();
                rw.Write("           |             |                            |");
                str = str.Substring(28, str.Length - 28);
            }
            rw.Write(str + new String(' ', 28 - str.Length) + "|");
            str = order.Quantity.ToString();
            rw.Write(str + new String(' ', 10 - str.Length) + "|");
            str = "$" + order.PricePaid.ToString("N2");
            rw.Write(str + new String(' ', 14 - str.Length) + "|");
            str = "$" + order.PriceNet.ToString("N2");
            rw.Write(str + new String(' ', 16 - str.Length) + "|");
            str = order.PromoCode.ToString();
            rw.Write(str + new String(' ', 10 - str.Length) + "|");
            str = (order.Refunded > 0 ? "Yes" : "");
            rw.Write(str + new String(' ', 8 - str.Length) + "|");
            str = (order.Cancelled > 0 ? "Yes" : "");
            rw.Write(str + new String(' ', 9 - str.Length) + "|");
            str = order.PhoneNumber;
            rw.Write(str + new String(' ', 15 - str.Length) + "|");
            str = order.BuyerEmail.ToString();
            rw.Write(str + new String(' ', 28 - (str.Length > 28 ? 28 : str.Length)) + "|");
            str = order.Address;
            while (str.Length > 28)
            {
                rw.Write(str.Substring(0, 28) + "|                            ");
                rw.WriteLine();
                rw.Write("           |             |                            |                            |          |              |                 |         |        |        |               |                            |");
                str = str.Substring(28, str.Length - 28);
            }
            rw.Write(str + new String(' ', 28 - str.Length) + "|");
            str = order.MailTickets;
            while (str.Length > 28)
            {
                rw.Write(str.Substring(0, 28) + "|");
                rw.WriteLine();
                rw.Write("           |             |                            |                            |          |              |                 |         |        |        |               |                            |                            |");
                str = str.Substring(28, str.Length - 28);
            }
            rw.Write(str + new String(' ', 28 - str.Length) + "");
            variableCount = 0;
            if (tempOrderId != order.OrderId)
            {
                foreach (var variable in order.VariableChages)
                {
                    variableCount += 1;
                    str = "$" + (variable.Price ?? 0).ToString("N2");
                    rw.Write(str + new String(' ', (variable.VariableDesc.Length > str.Length ? variable.VariableDesc.Length - str.Length : 2)) + "|");
                }
                tempOrderId = order.OrderId;
            }
            else
            {
                foreach (var variable in order.VariableChages)
                {
                    variableCount += 1;
                    str = "";
                    rw.Write(str + new String(' ', (variable.VariableDesc.Length > str.Length ? variable.VariableDesc.Length - str.Length : 2)) + "|");
                }
            }
            rw.WriteLine();
        }

        rw.Flush();

        res.Position = 0;
        return res;
    }

    private MemoryStream SaleReportToCSV(IEnumerable<EventOrderInfoViewModel> orders, string delimiter, string ReportTitle)
    {
        MemoryStream res = new MemoryStream();
        StreamWriter rw = new StreamWriter(res);

        rw.Write(ReportTitle);
        rw.WriteLine();
        rw.Write("ORDER #" + delimiter);
        rw.Write("DATE" + delimiter);
        rw.Write("ATTENDEE" + delimiter);
        rw.Write("TICKET NAME" + delimiter);
        rw.Write("QUANTITY" + delimiter);
        rw.Write("GROSS PAID" + delimiter);
        rw.Write("NET EVENT REVENUE" + delimiter);
        rw.Write("PROMO CODE" + delimiter);
        rw.Write("REFUNDED" + delimiter);
        rw.Write("CANCELLED" + delimiter);
        rw.Write("ATTENDEE NUMBER" + delimiter);
        rw.Write("ATTENDEE EMAIL" + delimiter);
        rw.Write("BILLING ADDRESS" + delimiter);
        rw.Write("MAIL TICKETS" + delimiter);
        var variableCount = 0;
        var variableTotalCount = 0;
        if (orders.FirstOrDefault() != null)
        {
            variableTotalCount = orders.FirstOrDefault().VariableChages.Count();
            foreach (var variable in orders.FirstOrDefault().VariableChages)
            {
                variableCount += 1;
                rw.Write(variable.VariableDesc + (variableCount < variableTotalCount ? delimiter : ""));
            }
        }
        rw.WriteLine();

        string tempOrderId = "";
        foreach (var order in orders)
        {
            rw.Write(order.OrderId + delimiter);
            rw.Write("\"" + order.Date.ToString("MMM dd, yyyy hh:mm:ss tt") + "\"" + delimiter);
            rw.Write("\"" + order.BuyerName + "\"" + delimiter);
            rw.Write("\"" + order.TicketName + "\"" + delimiter);
            rw.Write(order.Quantity.ToString() + delimiter);
            rw.Write("$" + order.PricePaid.ToString("N2") + delimiter);
            rw.Write("$" + order.PriceNet.ToString("N2") + delimiter);
            rw.Write(order.PromoCode + delimiter);
            rw.Write((order.Refunded > 0 ? "Yes" : "") + delimiter);
            rw.Write((order.Cancelled > 0 ? "Yes" : "") + delimiter);
            rw.Write(order.PhoneNumber + delimiter);
            rw.Write("\"" + order.BuyerEmail + "\"" + delimiter);
            rw.Write("\"" + order.Address + "\"" + delimiter);
            rw.Write("\"" + order.MailTickets.ToString() + "\"" + delimiter);
            variableCount = 0;
            if (tempOrderId != order.OrderId)
            {
                foreach (var variable in order.VariableChages)
                {
                    variableCount += 1;
                    rw.Write("$" + (variable.Price ?? 0).ToString("N2") + (variableCount < variableTotalCount ? delimiter : ""));
                }
                tempOrderId = order.OrderId;
            }
            else
            {
                foreach (var variable in order.VariableChages)
                {
                    variableCount += 1;
                    rw.Write("" + (variableCount < variableTotalCount ? delimiter : ""));
                }
            }
            rw.WriteLine();
        }

        rw.Flush();

        res.Position = 0;
        return res;
    }
    public ScheduledEmailViewModel PrepareSendAttendeeMail(long eventId)
    {
        IRepository<EventVenue> eRepo = new GenericRepository<EventVenue>(_factory.ContextFactory);
        var ev = eRepo.Get(filter: (e => e.EventID == eventId)).FirstOrDefault();
        ScheduledEmailViewModel scheduledEmail = new ScheduledEmailViewModel();
        scheduledEmail.ScheduledDate = ev.E_Startdate ?? DateTime.UtcNow;
        scheduledEmail.SendFrom= System.Configuration.ConfigurationManager.AppSettings.Get("UserName");       
        scheduledEmail.SendTos = GetSendToDropdownList(eventId);
        scheduledEmail.RegisteredDate = DateTime.UtcNow;
        return scheduledEmail;
    }
    public IEnumerable<SelectItemModel> GetSendToDropdownList(long eventId)
    {
        IRepository<TicketBearer_View> etBRepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
        IRepository<EventTicket_View> eRVTRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);

        var orderIds = eRVTRepo.Get(filter: (t => t.EventID == eventId)).Select(o => o.OrderId);
        var ticketBearers = etBRepo.Get(filter: t => orderIds.Contains(t.OrderId)).ToList();
        List<SelectItemModel> selectItems = new List<SelectItemModel>();

        var PaidAmount = eRVTRepo.Get(filter: (t => t.EventID == eventId &&
                        (t.PaymentTypeId == 1 ||
                        t.PaymentTypeId == 2 ||
                        t.PaymentTypeId == 5 ||
                        t.PaymentTypeId == 6 ||
                        t.PaymentTypeId == 7 ||
                        t.PaymentTypeId == 10))).Sum(o => o.PaidAmount);
        var OrderAmount = eRVTRepo.Get(filter: (t => t.EventID == eventId &&
                        (t.PaymentTypeId == 1 ||
                        t.PaymentTypeId == 2 ||
                        t.PaymentTypeId == 5 ||
                        t.PaymentTypeId == 6 ||
                        t.PaymentTypeId == 7 ||
                        t.PaymentTypeId == 10))).Sum(o => o.OrderAmount);

        selectItems.Add(new SelectItemModel
        {
            Name = "All Attendees (" + ticketBearers.Count() + ")",
            Value = "CONFIRMED_ATTENDEES"
        });
        selectItems.Add(new SelectItemModel
        {
            Name = "All Attendees Registered after Date",
            Value = "ALL_ATTENDEES_DATE"
        });
        selectItems.Add(new SelectItemModel
        {
            Name = "Specific Attendees",
            Value = "ATTENDEES"
        });
        selectItems.Add(new SelectItemModel
        {
            Name = "Attendees by Ticket Type",
            Value = "TICKET_ATTENDEES"
        });
        selectItems.Add(new SelectItemModel
        {
            Name = "Offline Payment Not Received (" + (OrderAmount - PaidAmount) + ")",
            Value = "PAYMENT_NOT_RECEIVED"
        });
        selectItems.Add(new SelectItemModel
        {
            Name = "Offline Payment Received (" + PaidAmount + ")",
            Value = "PAYMENT_RECEIVED"
        });

        return selectItems;
    }
    public IEnumerable<ScheduledEmail> GetScheduledEmailList(long eventId, bool IsEmailSend)
    {
        IRepository<ScheduledEmail> sERepo = new GenericRepository<ScheduledEmail>(_factory.ContextFactory);
        IRepository<AttendeeEmail> aERepo = new GenericRepository<AttendeeEmail>(_factory.ContextFactory);

        var AttendeeEmails = aERepo.Get(filter: s => s.EventID == eventId).Select(a=>a.ScheduledEmailId);
        var ScheduledEmails = sERepo.Get(filter: s => AttendeeEmails.Contains(s.ScheduledEmailId) && s.EmailTypeId == 1 && s.IsEmailSend == IsEmailSend);
        var id = DateTimeWithZone.Timezonedetail(eventId);
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
        foreach (var scheduledEmail in ScheduledEmails)
        {
            var dateTimeWithZone = new DateTimeWithZone(scheduledEmail.ScheduledDate, userTimeZone, true);
            scheduledEmail.ScheduledDate = dateTimeWithZone.LocalTime;
        }
        return ScheduledEmails;
    }
    public ScheduledEmailViewModel GetScheduledEmailDetail(long scheduledEmailId)
    {
        IRepository<ScheduledEmail> sERepo = new GenericRepository<ScheduledEmail>(_factory.ContextFactory);
        var ScheduledEmail = sERepo.Get(filter: s => s.ScheduledEmailId == scheduledEmailId).ToList().FirstOrDefault();
        return _mapper.Map<ScheduledEmailViewModel>(ScheduledEmail);
    }
    public bool SendAttendeeMail(long eventId, ScheduledEmailViewModel scheduledEmail, string userId, string ticketbearerIds, DateTime scheduledDate)
    {
        string defaultEmail;
        defaultEmail = System.Configuration.ConfigurationManager.AppSettings.Get("DefaultEmail");
        var ticketbearerId = (string.IsNullOrEmpty(ticketbearerIds) ? new List<string>() : ticketbearerIds.Split(',').ToList());
        IRepository<TicketBearer_View> etBRepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
        IRepository<EventTicket_View> eRVTRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
  
        List <TicketBearer_View> ticketBearers=new List<TicketBearer_View>();

        if (scheduledEmail.SendTo == "CONFIRMED_ATTENDEES")
        {
            var orderIds = eRVTRepo.Get(filter: (t => t.EventID == eventId)).Select(o => o.OrderId);
            ticketBearers = etBRepo.Get(filter: (t => orderIds.Contains(t.OrderId))).ToList();
        }
        else if (scheduledEmail.SendTo == "ALL_ATTENDEES_DATE")
        {
            var orderIds = eRVTRepo.Get(filter: (t => t.EventID == eventId && System.Data.Entity.DbFunctions.TruncateTime(t.O_OrderDateTime) >= scheduledEmail.RegisteredDate)).Select(o => o.OrderId);
            ticketBearers = etBRepo.Get(filter: (t => orderIds.Contains(t.OrderId))).ToList();
        }
        else if (scheduledEmail.SendTo == "ATTENDEES")
        {
            ticketBearers = etBRepo.Get(filter: r => ticketbearerId.Contains(r.OrderId + ":" + r.TicketbearerId)).ToList();
        }
        else if (scheduledEmail.SendTo == "TICKET_ATTENDEES")
        {
            var orderIds = eRVTRepo.Get(filter: (t => t.EventID == eventId && ticketbearerId.Contains(t.TicketTypeID.ToString()))).Select(o => o.OrderId);
            ticketBearers = etBRepo.Get(filter: (t => orderIds.Contains(t.OrderId))).ToList();
        }
        else if (scheduledEmail.SendTo == "PAYMENT_NOT_RECEIVED")
        {
            var orderIds = eRVTRepo.Get(filter: (t => t.EventID == eventId &&
                            (t.PaymentTypeId == 1 ||
                            t.PaymentTypeId == 2 ||
                            t.PaymentTypeId == 5 ||
                            t.PaymentTypeId == 6 ||
                            t.PaymentTypeId == 7 ||
                            t.PaymentTypeId == 10) &&
                            t.PaidAmount <= 0)).Select(o => o.OrderId);
            ticketBearers = etBRepo.Get(filter: r => orderIds.Contains(r.OrderId)).ToList();
        }
        else if (scheduledEmail.SendTo == "PAYMENT_RECEIVED")
        {
            var orderIds = eRVTRepo.Get(filter: (t => t.EventID == eventId &&
                                (t.PaymentTypeId == 1 ||
                                t.PaymentTypeId == 2 ||
                                t.PaymentTypeId == 5 ||
                                t.PaymentTypeId == 6 ||
                                t.PaymentTypeId == 7 ||
                                t.PaymentTypeId == 10) &&
                                t.PaidAmount > 0)).Select(o => o.OrderId);
            ticketBearers = etBRepo.Get(filter: r => orderIds.Contains(r.OrderId)).ToList();
        }

        AttendeeMailNotification attendeeMailNotification = new AttendeeMailNotification(_factory, defaultEmail, scheduledEmail, ticketBearers);
        attendeeMailNotification.SendNotification(new SendAttendeeMailService(_factory, eventId, userId, ticketBearers, scheduledDate));
        return true;
    }
    public bool UpdateAttendeeMail(ScheduledEmailViewModel scheduledEmail)
    {
        using (var uow = _factory.GetUnitOfWork())
            try
            {
                IRepository<ScheduledEmail> etBRepo = new GenericRepository<ScheduledEmail>(_factory.ContextFactory);
                var scheduledEmaile = etBRepo.Get(filter: s => s.ScheduledEmailId == scheduledEmail.ScheduledEmailId).FirstOrDefault();
                scheduledEmaile.Body = scheduledEmail.Body;
                etBRepo.Update(scheduledEmaile);
                uow.Context.SaveChanges();
                uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                uow.Rollback();
                return false;
            }
    }
    public bool DeleteAttendeeMail(long scheduledEmailId)
    {
        using (var uow = _factory.GetUnitOfWork())
            try
            {
                IRepository<ScheduledEmail> sERepo = new GenericRepository<ScheduledEmail>(_factory.ContextFactory);
                IRepository<AttendeeEmail> aERepo = new GenericRepository<AttendeeEmail>(_factory.ContextFactory);
                var ScheduledEmail = sERepo.GetByID(scheduledEmailId);
                var AttendeeEmails = aERepo.Get(filter: a => a.ScheduledEmailId == ScheduledEmail.ScheduledEmailId);
                foreach (var item in AttendeeEmails)
                {
                    aERepo.Delete(item.AttendeeEmailId);
                }
                sERepo.Delete(ScheduledEmail);
                uow.Context.SaveChanges();
                uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                uow.Rollback();
                return false;
            }
    }
    public List<AttendeeViewModel> GetAttendeeList(AttendeeSearchRequestViewModel request)
    {
        IRepository<EventTicket_View> eRVTRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
        IRepository<TicketBearer_View> sERepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);

        var orderIds = eRVTRepo.Get(filter: (t => t.EventID == request.EventId)).Select(o => o.OrderId);
        var attendees = sERepo.Get(filter: a => (string.IsNullOrEmpty(request.Name) ? true : a.Name.Contains(request.Name))
        && (string.IsNullOrEmpty(request.Email) ? true : a.Email.Contains(request.Email)) && orderIds.Contains(a.OrderId))
                .Select((element) => new AttendeeViewModel
                {
                    Email = element.Email,
                    Name = element.Name,
                    TicketbearerId = element.TicketbearerId,
                    OrderId = element.OrderId
                }).ToList();
        return attendees;
    }
    public List<CheckinViewModel> GetAttendeeCheckinList(AttendeeSearchRequestViewModel request)
    {
        IRepository<EventTicket_View> eRVTRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
        IRepository<TicketBearer_View> sERepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);

        var orderIds = eRVTRepo.Get(filter: (t => t.EventID == request.EventId)).Select(o => o.OrderId);
        var attendees = sERepo.Get(filter: a => (string.IsNullOrEmpty(request.Name) ? true : a.Name.Contains(request.Name))
        && (string.IsNullOrEmpty(request.Email) ? true : a.Email.Contains(request.Email)) && orderIds.Contains(a.OrderId))
                .Select((element) => new CheckinViewModel
                {
                    Email = element.Email,
                    Name = element.Name,
                    TicketbearerId = element.TicketbearerId,
                    OrderId = element.OrderId
                }).ToList();

        foreach (var attendee in attendees)
        {
                attendee.TicketType = "";
                attendee.CheckinStatus = false;
        }
        return attendees;
    }
    public List<AttendeeTicketTypeViewModel> GetAttendeeTicketTypeList(long eventId)
    {
        IRepository<EventTicket_View> eRVTRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
        IRepository<TicketBearer_View> sERepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
        IRepository<TicketType> tTRepo = new GenericRepository<TicketType>(_factory.ContextFactory);

        var attendeeTicketTypes = tTRepo.Get();
        List<AttendeeTicketTypeViewModel> attendeeTicketTypeViewModels = new List<AttendeeTicketTypeViewModel>();
        foreach (var ticketType in attendeeTicketTypes)
        {
            var eventTicketView = eRVTRepo.Get(filter: (t => t.EventID == eventId && t.TicketTypeID == ticketType.TicketTypeID));
            var orderIds = eventTicketView.Select(o => o.OrderId);
            var attendees = sERepo.Get(filter: a => orderIds.Contains(a.OrderId));
            if (attendees.Count() > 0)
            {
                attendeeTicketTypeViewModels.Add(new AttendeeTicketTypeViewModel
                {
                    TicketTypeId = ticketType.TicketTypeID,
                    TicketType = ticketType.TicketType1,
                    Price = eventTicketView.Sum(e => e.OrderAmount) ?? 0,
                    Sold = eventTicketView.Sum(e => e.PaidAmount) ?? 0,
                    AttendeeCount = attendees.Count()
                });
            }
        }
        
        return attendeeTicketTypeViewModels;
    }
    public MemoryStream GetDownloadableGuestList(string sortBy, string ticketTypeIds, string barcode, long eventId, string format)
    {
        format = format.Trim().ToLower();
        if ((format != "pdf"))
            return null;

        return GuestListToPDF(sortBy, ticketTypeIds, barcode, eventId);
    }

    public MemoryStream GuestListToPDF(string sortBy, string ticketTypeIds, string barcode, long eventId)
    {
        IRepository<EventTicket_View> eTVRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
        IRepository<Event> EventRepo = new GenericRepository<Event>(_factory.ContextFactory);
        IRepository<TicketBearer_View> tBVRepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
        IRepository<PaymentType> pTRepo = new GenericRepository<PaymentType>(_factory.ContextFactory);
        IRepository<Address> addressRepo = new GenericRepository<Address>(_factory.ContextFactory);

        var tTypeIds = (!string.IsNullOrEmpty(ticketTypeIds) ? ticketTypeIds.Split(',').Select(Int64.Parse).ToList() : null);
        var eventTickets = eTVRepo.Get(filter: (e => e.EventID == eventId));
        if (sortBy == "TicketType")
        {
            eventTickets = eventTickets.OrderBy(e => e.TicketTypeName);
        }
        if (tTypeIds != null)
        {
            eventTickets = eventTickets.Where(e => tTypeIds.Contains(e.TicketTypeID));
        }
        var eventTitle = EventRepo.Get(filter: (e => e.EventID == eventId)).FirstOrDefault();
        string title = "";
        string subTitle = "";
        string subTitle1 = "";

        if (eventTitle != null)
        {
            title = eventTitle.EventTitle;
            var addressId = eventTitle.EventVenues.FirstOrDefault().AddressId;
            var eventAddressDB = addressRepo.Get(filter: (b => b.AddressID == addressId)).FirstOrDefault();
            DateTime eStartDateTime = eventTitle.EventVenues.FirstOrDefault().E_Startdate ?? DateTime.UtcNow;
            DateTime eEndDateTime = eventTitle.EventVenues.FirstOrDefault().E_Enddate ?? DateTime.UtcNow;
            subTitle = eStartDateTime.ToString("ddd, MMMM dd, yyyy") + " at " + eStartDateTime.ToString("hh:mm tt") + " - " + eEndDateTime.ToString("ddd, MMMM dd, yyyy") + " at " + eEndDateTime.ToString("hh:mm tt");
            if (eventAddressDB != null)
            {
                subTitle1 = eventAddressDB.ConsolidateAddress;
            }
        }
        PdfPTable tableHeader = new PdfPTable(1);
        PdfPCell cell = new PdfPCell(new Phrase(title));
        cell.Border =Rectangle.NO_BORDER;
        cell.HorizontalAlignment = 1;
        tableHeader.AddCell(cell);
        cell = new PdfPCell(new Phrase(subTitle));
        cell.Border = Rectangle.NO_BORDER;
        cell.HorizontalAlignment = 1;
        tableHeader.AddCell(cell);
        cell = new PdfPCell(new Phrase(subTitle1));
        cell.Border = Rectangle.NO_BORDER;
        cell.HorizontalAlignment = 1;
        tableHeader.AddCell(cell);

        PdfPTable table = new PdfPTable(4);
        float[] widths = new float[] { 3f, 1f, 3f, 5f };
        table.SetWidths(widths);

        BaseColor bgColor = new BaseColor(192, 192, 192);
        cell = new PdfPCell(new Phrase("Name"));
        cell.BackgroundColor = bgColor;
        cell.BorderColor = bgColor;
        table.AddCell(cell);
        cell = new PdfPCell(new Phrase("Qty"));
        cell.BackgroundColor = bgColor;
        cell.BorderColor = bgColor;
        table.AddCell(cell);
        cell = new PdfPCell(new Phrase("Ticket Type"));
        cell.BackgroundColor = bgColor;
        cell.BorderColor = bgColor;
        table.AddCell(cell);
        cell = new PdfPCell(new Phrase("Payment Status"));
        cell.BackgroundColor = bgColor;
        cell.BorderColor = bgColor;
        table.AddCell(cell);


        foreach (var eventTicket in eventTickets)
        {
            var ticketBearers = tBVRepo.Get(filter: (t => t.OrderId == eventTicket.OrderId));
            var paymentType = pTRepo.Get(filter: (p => p.PaymentTypeId == eventTicket.PaymentTypeId)).FirstOrDefault();
            var purchasedQuantity = eventTicket.PurchasedQuantity;
            var attendeeQuantity = ticketBearers.Count(t => t.TicketbearerId == 0);
            if (sortBy == "Name")
            {
                ticketBearers = ticketBearers.OrderBy(e => e.Name);
            }
            foreach (var ticketBearer in ticketBearers)
            {
                cell = new PdfPCell(new Phrase(ticketBearer.Name));
                cell.Rowspan = 2;
                cell.BorderColor = bgColor;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase((ticketBearer.TicketbearerId == 0 ? purchasedQuantity - attendeeQuantity : 1).ToString()));
                cell.Rowspan = 2;
                cell.BorderColor = bgColor;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(eventTicket.TicketTypeName));
                cell.Rowspan = 2;
                cell.BorderColor = bgColor;
                table.AddCell(cell);

                if (barcode == null)
                {
                    cell = new PdfPCell(new Phrase((paymentType != null ? paymentType.PaymentTypeName : "")));
                    cell.BorderColor = bgColor;
                    cell.Border = Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Order " + eventId + " - " + eventTicket.OrderId));
                    cell.BorderColor = bgColor;
                    cell.Border = Rectangle.RIGHT_BORDER;
                    cell.Border = Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);
                }
                else if (barcode == "on")
                {
                    cell = new PdfPCell(new Phrase((paymentType != null ? paymentType.PaymentTypeName : "")));
                    cell.BorderColor = bgColor;
                    cell.Border = Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);
                    iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(GenerateBarCode(eventTicket.OrderId));                      
                    cell = new PdfPCell(myImage);
                    cell.FixedHeight = 75;
                    cell.BorderColor = bgColor;
                    cell.PaddingLeft = 20;
                    cell.PaddingTop = 3;
                    cell.Border = Rectangle.RIGHT_BORDER;
                    cell.Border = Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);
                }
            }
        }

        Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        document.Open();
        document.Add(tableHeader);
        document.Add(table);
        document.Close();
        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();
        return new MemoryStream(bytes);
    }

    public byte[] GenerateBarCode(string barCodeData)
    {
        System.Net.WebClient wc = new System.Net.WebClient();
        string url = "https://www.barcodesinc.com/generator/image.php?code=" + barCodeData + "&style=196&type=C128B&width=140&height=70&xres=1&font=3";
        byte[] barImage = wc.DownloadData(url);
        return barImage;
    }

    public IEnumerable<SelectItemModel> GetSelectAttendeeDropdownList(long eventId)
    {
        IRepository<TicketBearer_View> etBRepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
        IRepository<EventTicket_View> eRVTRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);

        var orderIds = eRVTRepo.Get(filter: (t => t.EventID == eventId)).Select(o => o.OrderId);
        var ticketBearers = etBRepo.Get(filter: t => orderIds.Contains(t.OrderId)).ToList();
        List<SelectItemModel> selectItems = new List<SelectItemModel>();

        selectItems.Add(new SelectItemModel
        {
            Name = "All Unique Attendees (" + ticketBearers.Count() + ")",
            Value = "CONFIRMED_ATTENDEES"
        });
        selectItems.Add(new SelectItemModel
        {
            Name = "Specific Attendees",
            Value = "ATTENDEES"
        });
        selectItems.Add(new SelectItemModel
        {
            Name = "Attendees by Ticket Type",
            Value = "TICKET_ATTENDEES"
        });

        return selectItems;
    }

    public MemoryStream GetBadgesPreview(long eventId, string format, string UserID)
    {
        string pdfPath;
        format = format.Trim().ToLower();
        if ((format != "pdf"))
            return null;
        MemoryStream memoryStream = new MemoryStream();

        pdfPath = "/TempDoc/NameBadges/preview_" + eventId + "_" + UserID + ".pdf";

        if (File.Exists(HttpContext.Current.Server.MapPath(pdfPath)))
        {
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(pdfPath), FileMode.Open);

            file.CopyTo(memoryStream);
            file.Close();
        }
        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();
        return new MemoryStream(bytes);
    }

    public string GetBadgesPreviewPath(BadgesViewModel badgesViewModel, string format, string UserID)
    {
        string pdfPath;
        format = format.Trim().ToLower();
        if ((format != "pdf"))
            return null;

        pdfPath = "/TempDoc/NameBadges";
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(pdfPath)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(pdfPath));
        }
        MemoryStream memoryStream = BadgesListPreviewToPDF(badgesViewModel);
        pdfPath += "/preview_" + badgesViewModel.EventId + "_" + UserID + ".pdf";
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath(pdfPath), FileMode.Create, FileAccess.Write);
        memoryStream.WriteTo(file);
        file.Close();
        memoryStream.Close();
        return "~" + pdfPath;
    }

    public MemoryStream GetBadgesList(long eventId, string format, string UserID)
    {
        string pdfPath;
        format = format.Trim().ToLower();
        if ((format != "pdf"))
            return null;
        MemoryStream memoryStream = new MemoryStream();

        pdfPath = "/TempDoc/NameBadges/badges_" + eventId + "_" + UserID + ".pdf";

        if (File.Exists(HttpContext.Current.Server.MapPath(pdfPath)))
        {
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(pdfPath), FileMode.Open);

            file.CopyTo(memoryStream);
            file.Close();
        }
        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();
        return new MemoryStream(bytes);
    }

    public string GetBadgesListPath(BadgesViewModel badgesViewModel, string format, string UserID)
    {
        string pdfPath;
        format = format.Trim().ToLower();
        if ((format != "pdf"))
            return null;

        pdfPath = "/TempDoc/NameBadges";
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(pdfPath)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(pdfPath));
        }
        MemoryStream memoryStream = BadgesListToPDF(badgesViewModel);
        pdfPath += "/badges_" + badgesViewModel.EventId + "_" + UserID + ".pdf";
        FileStream file = new FileStream(HttpContext.Current.Server.MapPath(pdfPath), FileMode.Create, FileAccess.Write);
        memoryStream.WriteTo(file);
        file.Close();
        memoryStream.Close();
        return "~" + pdfPath;
    }

    private MemoryStream BadgesListPreviewToPDF(BadgesViewModel badgesViewModel)
    {

        PdfPTable table = new PdfPTable(1);
        PdfPTable tableChild = new PdfPTable(1);
        float inchUnit = 72.00f;
        float badgeWidth = inchUnit * 4;
        float badgeHeight = inchUnit * 3;

        if (badgesViewModel.BadgeStyle == "5361")
        {
            badgeWidth = inchUnit * 3.25f;
            badgeHeight = inchUnit * 2;
        }
        else if(badgesViewModel.BadgeStyle == "5384" || 
                badgesViewModel.BadgeStyle == "74459" || 
                badgesViewModel.BadgeStyle == "74536" || 
                badgesViewModel.BadgeStyle == "74540" || 
                badgesViewModel.BadgeStyle == "74540" || 
                badgesViewModel.BadgeStyle == "74541")
        {
            badgeWidth = inchUnit * 4f;
            badgeHeight = inchUnit * 3;
        }
        else if (badgesViewModel.BadgeStyle == "5390")
        {
            badgeWidth = inchUnit * 3.50f;
            badgeHeight = inchUnit * 2.25f;
        }
        else if (badgesViewModel.BadgeStyle == "8395")
        {
            badgeWidth = inchUnit * 3.38f;
            badgeHeight = inchUnit * 2.33f;
        }
        else if (badgesViewModel.BadgeStyle == "L7418")
        {
            badgeWidth = inchUnit * 3.39f;
            badgeHeight = inchUnit * 2.17f;
        }

        foreach (BadgesLayout badgesLayout in badgesViewModel.BadgesLayouts.OrderBy(x => x.LineNumber))
        {
            var font = FontFactory.GetFont(badgesLayout.Font, badgesLayout.FontSize, BaseColor.BLACK);
            if (string.IsNullOrEmpty(badgesLayout.LineText))
                font = FontFactory.GetFont(badgesLayout.Font, badgesLayout.FontSize, BaseColor.WHITE);
            var text = string.IsNullOrEmpty(badgesLayout.LineText) ? "." : badgesLayout.LineText;

            if (badgesLayout.LineText == "name")
            {
                text = "Name Name";
            }else if (badgesLayout.LineText == "event_name")
            {
                text = "Event Name";
            }
            else if (badgesLayout.LineText == "ticket_name")
            {
                text = "Ticket Name";
            }
            else if (badgesLayout.LineText == "email_address")
            {
                text = "Email Address";
            }
            else if (badgesLayout.LineText == "bill")
            {
                text = "Billing Address";
            }
                
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            if (badgesLayout.Align.ToLower() == "right")
                cell.HorizontalAlignment = 2;
            else if (badgesLayout.Align.ToLower() == "center")
                cell.HorizontalAlignment = 1;
            else
                cell.HorizontalAlignment = 0;
            tableChild.AddCell(cell);
        }

        PdfPCell cellChild = new PdfPCell(tableChild);
        cellChild.HorizontalAlignment = Element.ALIGN_CENTER;
        cellChild.Border = Rectangle.NO_BORDER;
        cellChild.FixedHeight = badgeHeight;            
        cellChild.VerticalAlignment = Element.ALIGN_MIDDLE;
        table.AddCell(cellChild);
        table.SetWidths(new float[] { badgeWidth });

        Document document = new Document(new Rectangle(badgeWidth, badgeHeight), 0.00f, 0.00f, 0.00f, 0.00f);
        MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        document.Open();
        document.Add(table);
        document.Close();
        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();
        return new MemoryStream(bytes);
    }

    private MemoryStream BadgesListToPDF(BadgesViewModel badgesViewModel)
    {
        IRepository<Event> EventRepo = new GenericRepository<Event>(_factory.ContextFactory);
        IRepository<PaymentType> pTRepo = new GenericRepository<PaymentType>(_factory.ContextFactory);
        IRepository<Address> addressRepo = new GenericRepository<Address>(_factory.ContextFactory);

        var ticketbearerId = (string.IsNullOrEmpty(badgesViewModel.TicketbearerIds) ? new List<string>() : badgesViewModel.TicketbearerIds.Split(',').ToList());
        IRepository<TicketBearer_View> etBRepo = new GenericRepository<TicketBearer_View>(_factory.ContextFactory);
        IRepository<EventTicket_View> eRVTRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);

        List<TicketBearer_View> ticketBearers = new List<TicketBearer_View>();

        if (badgesViewModel.AttendeeSelect == "CONFIRMED_ATTENDEES")
        {
            var orderIds = eRVTRepo.Get(filter: (t => t.EventID == badgesViewModel.EventId)).Select(o => o.OrderId);
            ticketBearers = etBRepo.Get(filter: (t => orderIds.Contains(t.OrderId))).ToList();
        }
        else if (badgesViewModel.AttendeeSelect == "ATTENDEES")
        {
            ticketBearers = etBRepo.Get(filter: r => ticketbearerId.Contains(r.OrderId + ":" + r.TicketbearerId)).ToList();
        }
        else if (badgesViewModel.AttendeeSelect == "TICKET_ATTENDEES")
        {
            var orderIds = eRVTRepo.Get(filter: (t => t.EventID == badgesViewModel.EventId && ticketbearerId.Contains(t.TicketTypeID.ToString()))).Select(o => o.OrderId);
            ticketBearers = etBRepo.Get(filter: (t => orderIds.Contains(t.OrderId))).ToList();
        }

        var eventTickets = eRVTRepo.Get(filter: (e => e.EventID == badgesViewModel.EventId));
        if (badgesViewModel.SortBy == "TicketType")
        {
            eventTickets = eventTickets.OrderBy(e => e.TicketTypeName);
        }
        var eventTitle = EventRepo.Get(filter: (e => e.EventID == badgesViewModel.EventId)).FirstOrDefault();
        string title = "";
        string subTitle = "";
        string subTitle1 = "";

        if (eventTitle != null)
        {
            title = eventTitle.EventTitle;
            var addressId = eventTitle.EventVenues.FirstOrDefault().AddressId;
            var eventAddressDB = addressRepo.Get(filter: (b => b.AddressID == addressId)).FirstOrDefault();
            DateTime eStartDateTime = eventTitle.EventVenues.FirstOrDefault().E_Startdate ?? DateTime.UtcNow;
            DateTime eEndDateTime = eventTitle.EventVenues.FirstOrDefault().E_Enddate ?? DateTime.UtcNow;
            subTitle = eStartDateTime.ToString("ddd, MMMM dd, yyyy") + " at " + eStartDateTime.ToString("hh:mm tt") + " - " + eEndDateTime.ToString("ddd, MMMM dd, yyyy") + " at " + eEndDateTime.ToString("hh:mm tt");
            if (eventAddressDB != null)
            {
                subTitle1 = eventAddressDB.ConsolidateAddress;
            }
        }

        Document document = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
        if (badgesViewModel.BadgeStyle == "L7418")
        {
            document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        }
        MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        document.Open();

        PdfPTable table = new PdfPTable(2);
        PdfPTable tableChild = new PdfPTable(1);
        float inchUnit = 72.00f;
        float badgeWidth = inchUnit * 4;
        float badgeHeight = inchUnit * 3;

        if (badgesViewModel.BadgeStyle == "5361")
        {
            badgeWidth = inchUnit * 3.25f;
            badgeHeight = inchUnit * 2;
        }
        else if (badgesViewModel.BadgeStyle == "5384" ||
                badgesViewModel.BadgeStyle == "74459" ||
                badgesViewModel.BadgeStyle == "74536" ||
                badgesViewModel.BadgeStyle == "74540" ||
                badgesViewModel.BadgeStyle == "74540" ||
                badgesViewModel.BadgeStyle == "74541")
        {
            badgeWidth = inchUnit * 4f;
            badgeHeight = inchUnit * 3;
        }
        else if (badgesViewModel.BadgeStyle == "5390")
        {
            badgeWidth = inchUnit * 3.50f;
            badgeHeight = inchUnit * 2.25f;
        }
        else if (badgesViewModel.BadgeStyle == "8395")
        {
            badgeWidth = inchUnit * 3.38f;
            badgeHeight = inchUnit * 2.33f;
        }
        else if (badgesViewModel.BadgeStyle == "L7418")
        {
            badgeWidth = inchUnit * 3.39f;
            badgeHeight = inchUnit * 2.17f;
        }

        foreach (var eventTicket in eventTickets.Take(1))
        {
            if (badgesViewModel.SortBy == "Name")
            {
                ticketBearers = ticketBearers.OrderBy(e => e.Name).ToList();
            }
            table = new PdfPTable(2);
            table.HorizontalAlignment = Element.ALIGN_CENTER;

            foreach (var ticketBearer in ticketBearers)
            {
                PdfPCell cellChild = new PdfPCell();
                cellChild.Border = Rectangle.NO_BORDER;
                tableChild = new PdfPTable(1);              

                foreach (BadgesLayout badgesLayout in badgesViewModel.BadgesLayouts.OrderBy(x => x.LineNumber))
                {
                    var font = FontFactory.GetFont(badgesLayout.Font, badgesLayout.FontSize, BaseColor.BLACK);
                    if (string.IsNullOrEmpty(badgesLayout.LineText))
                        font = FontFactory.GetFont(badgesLayout.Font, badgesLayout.FontSize, BaseColor.WHITE);
                    var text = string.IsNullOrEmpty(badgesLayout.LineText) ? "." : badgesLayout.LineText;

                    if (badgesLayout.LineText == "name")
                    {
                        text = ticketBearer.Name;
                    }
                    else if (badgesLayout.LineText == "event_name")
                    {
                        text = eventTitle.EventTitle;
                    }
                    else if (badgesLayout.LineText == "ticket_name")
                    {
                        text = eventTicket.TicketName;
                    }
                    else if (badgesLayout.LineText == "email_address")
                    {
                        text = ticketBearer.Email;
                    }
                    else if (badgesLayout.LineText == "bill")
                    {
                        text = subTitle1;
                    }

                    PdfPCell cell = new PdfPCell(new Phrase(text, font));
                    cell.Border = Rectangle.NO_BORDER;
                    if (badgesLayout.Align.ToLower() == "right")
                        cell.HorizontalAlignment = 2;
                    else if (badgesLayout.Align.ToLower() == "center")
                        cell.HorizontalAlignment = 1;
                    else
                        cell.HorizontalAlignment = 0;
                    tableChild.AddCell(cell);
                }
                cellChild = new PdfPCell(tableChild);
                cellChild.HorizontalAlignment = Element.ALIGN_CENTER;
                cellChild.Border = Rectangle.NO_BORDER;
                cellChild.FixedHeight = badgeHeight;
                cellChild.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cellChild);
                document.Add(table);
            }
        }
        if (document.IsOpen())
            document.Close();
        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();
        return new MemoryStream(bytes);
    }

    public MemoryStream GetDownloadableManualOrderList(PaymentStates state, long eventId, string format)
    {
      format = format.Trim().ToLower();
      if (format != "xls")
        return null;
      IEnumerable<EventOrderInfoViewModel> orders = GetManualOrdersForEvent(state, eventId);
      IRepository<Event> EventRepo = new GenericRepository<Event>(_factory.ContextFactory);
      var eventTitle = EventRepo.Get(filter: (e => e.EventID == eventId)).Select(x => new { x.EventTitle }).FirstOrDefault();
      var ReportTitle = (eventTitle == null ? "" : "Manual Order List for " + eventTitle.EventTitle);
      return ManualOrderListToXLS(orders, ReportTitle);
    }

    private MemoryStream ManualOrderListToXLS(IEnumerable<EventOrderInfoViewModel> orders, string ReportTitle)
    {
        MemoryStream res = new MemoryStream();
        IWorkbook wb = new HSSFWorkbook();
        ICellStyle style = wb.CreateCellStyle();
        style.BorderBottom = BorderStyle.Thin;
        style.BorderTop = BorderStyle.Thin;
        style.BorderLeft = BorderStyle.Thin;
        style.BorderRight = BorderStyle.Thin;

        ICellStyle hstyle = wb.CreateCellStyle();
        hstyle.BorderBottom = BorderStyle.Thin;
        hstyle.BorderTop = BorderStyle.Thin;
        hstyle.BorderLeft = BorderStyle.Thin;
        hstyle.BorderRight = BorderStyle.Thin;
        hstyle.Alignment = HorizontalAlignment.Center;
        IFont bfont = wb.CreateFont();
        bfont.Boldweight = (short)FontBoldWeight.Bold;
        hstyle.SetFont(bfont);

        ICellStyle datestyle = wb.CreateCellStyle();
        datestyle.BorderBottom = BorderStyle.Thin;
        datestyle.BorderTop = BorderStyle.Thin;
        datestyle.BorderLeft = BorderStyle.Thin;
        datestyle.BorderRight = BorderStyle.Thin;
        datestyle.DataFormat = wb.CreateDataFormat().GetFormat("MMMM dd, yyyy");

        ICellStyle Titlestyle = wb.CreateCellStyle();
        Titlestyle.BorderBottom = BorderStyle.Thin;
        Titlestyle.BorderTop = BorderStyle.Thin;
        Titlestyle.BorderLeft = BorderStyle.Thin;
        Titlestyle.BorderRight = BorderStyle.Thin;
        Titlestyle.Alignment = HorizontalAlignment.Center;
        Titlestyle.SetFont(bfont);

        ISheet sheet = wb.CreateSheet("Orders");
        IRow row = sheet.CreateRow(0);
        AddStyledCell(row, 0, Titlestyle).SetCellValue(ReportTitle);
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 15));

        row = sheet.CreateRow(1);
        AddStyledCell(row, 0, hstyle).SetCellValue("Order Number");
        AddStyledCell(row, 1, hstyle).SetCellValue("Order Date & Time");
        AddStyledCell(row, 2, hstyle).SetCellValue("Attendee Name");
        AddStyledCell(row, 3, hstyle).SetCellValue("Attendee Email");
        AddStyledCell(row, 4, hstyle).SetCellValue("Attendee Phone");
        AddStyledCell(row, 5, hstyle).SetCellValue("Ticket Name");
        AddStyledCell(row, 6, hstyle).SetCellValue("Quantity");
        AddStyledCell(row, 7, hstyle).SetCellValue("Manual Price Paid");

        var i = 2;
        string tempOrderId = "";
        foreach (var order in orders)
        {
            row = sheet.CreateRow(i++);
            if (tempOrderId != order.OrderId)
            {
                AddStyledCell(row, 0, style).SetCellValue(order.OrderId);
                AddStyledCell(row, 1, datestyle).SetCellValue(order.Date.ToString("MMM dd, yyyy hh:mm:ss tt"));
                tempOrderId = order.OrderId;
            }
            else
            {
                AddStyledCell(row, 0, style).SetCellValue("");
                AddStyledCell(row, 1, datestyle).SetCellValue("");
            }
            AddStyledCell(row, 2, style).SetCellValue(order.BuyerName);
            AddStyledCell(row, 3, style).SetCellValue(order.BuyerEmail);
            AddStyledCell(row, 4, style).SetCellValue(order.PhoneNumber);
            AddStyledCell(row, 5, style).SetCellValue(order.TicketName);
            AddStyledCell(row, 6, style).SetCellValue(order.Quantity);
            AddStyledCell(row, 7, style).SetCellValue("$" + order.PricePaid.ToString("N2"));
        }
        for (i = 0; i <= 7; i++)
        {
            sheet.AutoSizeColumn(i);
            sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
        }
        wb.Write(res);
        res.Position = 0;
        return res;
    }

    public string GetEventTitle(long eventId)
    {
        IRepository<Event> EventRepo = new GenericRepository<Event>(_factory.ContextFactory);
        var eventTitle = EventRepo.Get(filter: (e => e.EventID == eventId)).Select(x => new { x.EventTitle }).FirstOrDefault();
        return (eventTitle == null ? "" : eventTitle.EventTitle);
    }

    public bool SaveOrderDetails(EventOrderDetailViewModel model, string userId, string baseUrl, string filePath)
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
                  && ((attVM.Email ?? "").Trim() != (attendee.Email ?? "").Trim())
                || ((!String.IsNullOrWhiteSpace(attVM.PhoneNumber) || !String.IsNullOrWhiteSpace(attendee.PhoneNumber)))
                  && ((attVM.PhoneNumber ?? "").Trim() != (attendee.PhoneNumber ?? "").Trim()))
              {
                attVM.PhoneNumber = (string.IsNullOrEmpty(attVM.PhoneNumber) ? "" : attVM.PhoneNumber);
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
            OrderNotification notification = new OrderNotification(_factory, _dbservice, model.OrderId, baseUrl, attach, _tservice);
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

    private ISendMailService CreateSendMailService()
    {
      ISendMailService service = new SendMailService();
      service.Message.From = new MailAddress("noreply@eventcombo.com");
      service.Message.ReplyToList.Add(new MailAddress("noreply@eventcombo.com"));
      return service;
    }

  }
}
