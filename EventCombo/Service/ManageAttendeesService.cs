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

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var tickets = tpdRepo.Get(filter: (t => t.TPD_Event_Id == eventId));
      var ev = eRepo.Get(filter: (e => e.EventID == eventId)).FirstOrDefault();
      var order = orderRepo.Get();

      EventOrdersSummuryViewModel ordersTotal = new EventOrdersSummuryViewModel()
      {
        PaymentState = PaymentStates.Total,
        TicketsSold = tickets.Sum(t => t.TPD_Purchased_Qty) ?? 0,
        Amount = (tickets.Sum(t=>t.TPD_Amount) ?? 0) + (order.Where(o=>(tickets.Select(t=>t.TPD_Order_Id).Contains(o.O_Order_Id))).Sum(os=>os.O_VariableAmount) ?? 0),
        TicketsTotal = ev.Tickets.Sum(tt => tt.Ticket_Quantity_Detail.Sum(q => q.TQD_Quantity)) ?? 0,
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
      IRepository<BillingAddress> billRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
      IRepository<Ticket> ticketRepo = new GenericRepository<Ticket>(_factory.ContextFactory);
      IRepository<Country> countryRepo = new GenericRepository<Country>(_factory.ContextFactory);

      var tickets = ticketRepo.Get(filter: (t => t.E_Id == eventId));

      IEnumerable<EventOrderInfoViewModel> res = tpdRepo.Get(filter: (t => t.TPD_Event_Id == eventId))
        .GroupBy(tt => tt.TPD_Order_Id)
        .Select(ticket => new EventOrderInfoViewModel()
        {
          OrderId = ticket.Key,
          PaymentState = PaymentStates.Completed,
          TicketName = String.Join(", ", tickets.Where(t => t.T_Id == (ticket.FirstOrDefault().Ticket_Quantity_Detail.TQD_Ticket_Id ?? 0)).Select(t => t.T_name).ToArray()),
          Fee = ticket.Sum(t => t.TPD_EC_Fee) ?? 0,
          PricePaid = ticket.Sum(t=>t.TPD_Amount) ?? 0,
          BuyerName = ticket.FirstOrDefault().AspNetUser.Profiles.Select(p => p.FirstName + " " + p.LastName).FirstOrDefault(),
          BuyerEmail = ticket.FirstOrDefault().AspNetUser.Profiles.Select(p => p.Email).FirstOrDefault(),
          Quantity = ticket.Sum(t => t.TPD_Purchased_Qty) ?? 0
        }).ToList();
      foreach (var order in res)
      {
        var orderDB = orderRepo.Get(filter: (o => o.O_Order_Id == order.OrderId)).FirstOrDefault();
        var billingAddressDB = billRepo.Get(filter: (b => b.OrderId == order.OrderId)).FirstOrDefault();

        if (orderDB != null)
        {
            order.Date = orderDB.O_OrderDateTime ?? DateTime.Today;
            order.Price = orderDB.O_TotalAmount ?? 0;
            order.PricePaid = order.PricePaid + (orderDB.O_VariableAmount ?? 0);
            order.PriceNet = order.PricePaid - order.Fee;
            order.CustomerEmail = orderDB.O_Email;
            if (billingAddressDB != null)
            {
                var countryDB = countryRepo.Get(filter: (c => c.CountryID.ToString() == billingAddressDB.Country));

                order.Address = billingAddressDB.Address1 +
                    " " + billingAddressDB.Address2 +
                    ", " + billingAddressDB.City +
                    "-" + billingAddressDB.Zip +
                    " " + billingAddressDB.State +
                    " (" + (countryDB.FirstOrDefault().Country1) + ")";
            }
        }
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

      foreach (var attendee in tbRepo.Get(filter: (a => a.OrderId == orderId)))
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

    public bool SendConfirmations(string orderId, string baseUrl, string filePath)
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
      foreach (var attendee in attRepo.Get(filter: (a => a.OrderId == orderId)))
        if (!String.IsNullOrWhiteSpace(attendee.Email))
          if (!addresses.Any(a => a.Address == attendee.Email))
            addresses.Add(new MailAddress(attendee.Email, attendee.Name));

      if (addresses.Count <= 0)
        return false;
      else
      {
        var mem = _tservice.GetDownloadableTicket(orderId, "pdf", filePath);
        Attachment attach = new Attachment(mem, new ContentType(MediaTypeNames.Application.Pdf));
        attach.ContentDisposition.FileName = "Ticket_EventCombo.pdf";
        INotification notification = new OrderNotification(_factory, _dbservice, orderId, baseUrl, attach);
        ISendMailService sendService = new SendMailService();
        INotificationSender sender = new NotificationSender(notification, sendService);
        sender.SendSeparately(addresses);
        return true;
      }

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
        AddStyledCell(row, 4, style).SetCellValue((double)order.Price);
        AddStyledCell(row, 5, style).SetCellValue((double)order.PricePaid);
        AddStyledCell(row, 6, style).SetCellValue((double)order.PriceNet);
        AddStyledCell(row, 7, datestyle).SetCellValue(order.CustomerEmail);
        AddStyledCell(row, 8, datestyle).SetCellValue(order.Address);
        AddStyledCell(row, 9, datestyle).SetCellValue(order.Date);
        AddStyledCell(row, 10, style).SetCellValue(order.PaymentState.ToString());
      }
      for (i = 0; i <= 5; i++)
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
        str = order.Price.ToString("N2");
        rw.Write(str + new String(' ', 9 - str.Length) + "|");
        str = order.PricePaid.ToString("N2");
        rw.Write(str + new String(' ', 10 - str.Length) + "|");
        str = order.PriceNet.ToString("N2");
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
        str = order.Date.ToShortDateString();
        rw.Write(str + new String(' ', 10 - str.Length) + "|");
        str = order.PaymentState.ToString();
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
        rw.Write(order.BuyerName + delimiter);
        rw.Write(order.TicketName + delimiter);
        rw.Write(order.Quantity.ToString() + delimiter);
        rw.Write("$" + order.Price.ToString("N2") + delimiter);
        rw.Write("$" + order.PricePaid.ToString("N2") + delimiter);
        rw.Write("$" + order.PriceNet.ToString("N2") + delimiter);
        rw.Write(order.CustomerEmail + delimiter);
        rw.Write( order.Address + delimiter);
        rw.Write(order.Date.ToShortDateString() + delimiter);
        rw.Write(order.PaymentState.ToString());
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

        foreach(var attendee in model.Attendees)
        {
          TicketBearer tb = new TicketBearer()
          {
            OrderId = newOrderId,
            Name = attendee.Name,
            Email = attendee.Email,
            Guid = guidId.ToString(),
            UserId = userId
          };
          tbRepo.Insert(tb);
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
  }
}