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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using NLog;
using System.Web.Hosting;

namespace EventCombo.Service
{
  public enum FilterByOrderType { All, Regular, Manual }

  public class TicketService : ITicketsService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private IDBAccessService _dbservice;
    private INotificationService _nService;
    private IECImageService _iService;
    private IPurchasingService _pService;
    private ILogger _logger;
    private ControllerBase _baseController;
    public static readonly string TicketBarcodePath = "/Images/Barcodes/";
    public static string GetBarcodePath(string code)
    {
      return String.Format("{0}BR_{1}.png", TicketBarcodePath, code);
    }
    public static string GetQRPath(string code)
    {
      return String.Format("{0}QR_{1}.png", TicketBarcodePath, code);
    }

    public TicketService(IUnitOfWorkFactory factory, IMapper mapper, IDBAccessService dbService, ControllerBase baseController)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      if (mapper == null)
        throw new ArgumentNullException("mapper");

      if (dbService == null)
        throw new ArgumentNullException("dbService");

      if (baseController == null)
        throw new ArgumentNullException("baseController");

      _factory = factory;
      _mapper = mapper;
      _dbservice = dbService;
      _nService = new NotificationService(_factory, _mapper);
      _iService = new ECImageService(_factory, _mapper, new ECImageStorage(_mapper));
      _pService = new PurchasingService(_factory, _mapper);
      _logger = LogManager.GetCurrentClassLogger();
      _baseController = baseController;

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


    public OrderDetailsViewModel GetOrderDetails(string orderId, string userId, long eventId)
    {
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      IRepository<EventTicket_View> EventTicketRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);

      if (_dbservice.GetOrderAccess(orderId, userId) != AccessLevel.OrderOwner)
        throw new UnauthorizedAccessException(String.Format("Unathorized access to Order {0}. User {1}", orderId, userId));

      var TicketNames = EventTicketRepo.Get(filter: (t => t.EventID == eventId && t.OrderId == orderId)).Select(t => t.TicketName);
      var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();
      if (order == null)
        throw new NullReferenceException(String.Format("Order {0} not found.", orderId));

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

      details.TicketNames = (TicketNames == null ? "" : string.Join(", ", TicketNames.ToArray()));

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
                attVM.OrderId = model.OrderId;
                attVM.PhoneNumber = String.IsNullOrEmpty(attVM.PhoneNumber) ? (attendee.PhoneNumber ?? "") : attVM.PhoneNumber;
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
            OrderNotification notification = new OrderNotification(_factory, _dbservice, model.OrderId, baseUrl, attach, this);
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

      _nService.SendContactOrganizerMessage(model);
      return true;
    }


    public MemoryStream GetDownloadableTicket(string orderId, string format, string filePath, bool IsManualOrder = false)
    {
      TicketPaymentController tpc = new TicketPaymentController();

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      var ticket = tpdRepo.Get(filter: (t => t.TPD_Order_Id == orderId)).FirstOrDefault();
      var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();

      if ((ticket == null) || (order == null))
        return null;

      DownloadController dc = new DownloadController();
      string htmlText = GetTicketsHtml(orderId, "~/Views/Download/_Ticket.cshtml");
      var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
      var pdfBytes = htmlToPdf.GeneratePdf(htmlText);
      return new MemoryStream(pdfBytes);
    }

    public string GetTicketsHtml(string orderId, string view)
    {
      if (_baseController == null)
        throw new ArgumentNullException("ControllerContext wasn't initialized.");
      ControllerContext context = _baseController.ControllerContext;
      if (context == null)
        throw new Exception("Can't get ControllerContext.");
      IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);

      ETicketOrderViewModel model = new ETicketOrderViewModel();
      var order = oRepo.Get(o => o.O_Order_Id == orderId).FirstOrDefault();
      if (order == null)
        return "";
      model.OrderId = orderId;
      model.OrderDate = order.O_OrderDateTime ?? DateTime.MinValue;
      model.TotalPrice = order.O_TotalAmount ?? 0;
      if (order.PaymentTypeId == 8) // credit card
      {
        IRepository<BillingAddress> baRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
        var ba = baRepo.Get(b => b.OrderId == orderId).FirstOrDefault();
        if (ba != null)
        {
          EncryptDecrypt EDcode = new EncryptDecrypt();
          model.PaidBy = EDcode.DecryptText(ba.card_type);
          model.PaidBy = model.PaidBy.First().ToString().ToUpper() + model.PaidBy.Substring(1);
          string card = EDcode.DecryptText(ba.CardId);
          model.PaidBy = "Paid by " + model.PaidBy + " ending in " + card.Substring(card.Length - 4, 4);
        }
        else
          model.PaidBy = "Paid by unknown credit card";
      }
      else
        model.PaidBy = order.PaymentType != null ? order.PaymentType.PaymentTypeName : "Paid by unknown";

      model.PromoAmount = 0;
      model.PromoCode = "";
      if ((order.O_PromoCodeId ?? 0) > 0)
      {
        IRepository<Promo_Code> promoRepo = new GenericRepository<Promo_Code>(_factory.ContextFactory);
        var promo = promoRepo.GetByID(order.O_PromoCodeId ?? 0);
        if (promo != null)
        {
          model.PromoCode = promo.PC_Code;
          model.PromoAmount = (order.O_OrderAmount ?? 0) + (order.O_VariableAmount ?? 0) - (order.O_TotalAmount ?? 0);
        }
      }
      var tpdfirst = tpdRepo.Get(t => t.TPD_Order_Id == orderId).FirstOrDefault();
      Event ev = null;
      if (tpdfirst != null)
        ev = tpdfirst.Event;
      if (ev != null)
      {
        model.EventId = ev.EventID;
        model.EventTitle = ev.EventTitle;
        model.EventDescription = ev.EventDescription;
        ECImageViewModel image = _iService.GetImageById(ev.ECImageId ?? 0);
        if ((image != null) && (File.Exists(image.FilePath)))
          model.ImageUrl = image.ImagePath;
        else
          model.ImageUrl = "/Images/default_event_image.jpg";

        var address = ev.Addresses.FirstOrDefault();
        if (address != null)
        {
          model.Address = address.ConsolidateAddress;
          model.VenueName = address.VenueName;
          model.Latitude = address.Latitude;
          model.Longitude = address.Longitude;
        }
        else if (ev.AddressStatus == "Online")
          model.Address = "Online";

        var dateInfo = _pService.GetEventDatesInfo(ev);
        model.StartDate = dateInfo.StartDate;
        var org = ev.Event_Orgnizer_Detail.FirstOrDefault();
        if (org != null)
        {
          model.OrganizerId = org.OrganizerMaster_Id;
          model.OrganizerName = org.Organizer_Master.Orgnizer_Name;
          model.OrganizerEmail = org.Organizer_Master.Organizer_Email;
        }
      }

      if (!String.IsNullOrEmpty(order.O_VariableId))
      {
        IRepository<Event_VariableDesc> varRepo = new GenericRepository<Event_VariableDesc>(_factory.ContextFactory);
        var varCharges = order.O_VariableId.Split(',');
        if (varCharges != null)
          foreach (var vc in varCharges)
          {
            long vcId;
            if (Int64.TryParse(vc, out vcId))
            {
              var vcDB = varRepo.GetByID(vcId);
              if (vcDB != null)
                model.VariableCharges.Add(new ETicketVariableChargeViewModel()
                {
                  VarChargeId = vcId,
                  VarChargeName = vcDB.VariableDesc,
                  VarChargePrice = vcDB.Price ?? 0
                });
            }
          }
      }
      // process old orders
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          _pService.GenerateTicketDetails(orderId, uow);
          uow.Context.SaveChanges();
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception(String.Format("Exception during generationg tickets for order {0}", orderId), ex);
        }
      // populate attendee and tickets info
      IRepository<TicketBearer> tbRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
      var tbList = tbRepo.Get(tb => tb.OrderId == orderId);
      foreach (var tb in tbList)
      {
        ETicketAttendeeViewModel att = new ETicketAttendeeViewModel()
        {
          TicketBearerId = tb.TicketbearerId,
          AttendeeName = tb.Name,
          AttendeeEmail = tb.Email,
          TotalPrice = 0,
          TotalPromo = 0
        };

        foreach (var t in tb.TicketAttendees.OrderBy(tt => tt.Ticket_Purchased_Detail.Ticket_Quantity_Detail.Ticket.T_order ?? 0).ToList())
        {
          ETicketTicketViewModel et = new ETicketTicketViewModel()
          {
            PurchasedTicketId = t.PurchasedTicketId,
            Quantity = t.Quantity,
            Price = Math.Round((t.Ticket_Purchased_Detail.TPD_Purchased_Qty ?? 1) > 0 ? ((t.Ticket_Purchased_Detail.TPD_Amount ?? 0) + (t.Ticket_Purchased_Detail.TPD_Donate ?? 0)) / (t.Ticket_Purchased_Detail.TPD_Purchased_Qty ?? 1) : 0, 2),
            PromoAmount = Math.Round((t.Ticket_Purchased_Detail.TPD_Purchased_Qty ?? 1) > 0 ? (t.Ticket_Purchased_Detail.TPD_PromoCodeAmount ?? 0) / (t.Ticket_Purchased_Detail.TPD_Purchased_Qty ?? 1) : 0, 2),
            Discount = t.Ticket_Purchased_Detail.TicketDiscount,
            TicketName = t.Ticket_Purchased_Detail.Ticket_Quantity_Detail.Ticket.T_name,
            TicketType = t.Ticket_Purchased_Detail.Ticket_Quantity_Detail.Ticket.TicketTypeID == 2 ? "Paid" : (t.Ticket_Purchased_Detail.Ticket_Quantity_Detail.Ticket.TicketTypeID == 3 ? "Donation" : "Free")
          };
          et.TotalPrice = et.Quantity * et.Price;
          et.TotalPromo = et.Quantity * et.PromoAmount;
          foreach (var tt in t.TicketOrderDetails)
            et.TicketDetails.Add(tt.T_Id);
          att.Tickets.Add(et);
          att.TotalPrice += et.TotalPrice;
          att.TotalPromo += et.TotalPromo;
        }
        model.Attendees.Add(att);
      }

      PrepareTicketImages(model);

      context.Controller.ViewData.Model = model;
      var viewResult = ViewEngines.Engines.FindPartialView(context, view);
      using (var sw = new StringWriter())
      {
        var viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, sw);
        viewResult.View.Render(viewContext, sw);
        viewResult.ViewEngine.ReleaseView(context, viewResult.View);
        return sw.GetStringBuilder().ToString();
      }
    }

    private void PrepareTicketImages(ETicketOrderViewModel model)
    {
      var codes = model.Attendees.SelectMany(a => a.Tickets.SelectMany(t => t.TicketDetails));
      foreach (var code in codes)
      {
        GenerateBarCode(code, HostingEnvironment.MapPath(GetBarcodePath(code)));
        GenerateQR(code, HostingEnvironment.MapPath(GetQRPath(code)));
      }
    }

    public void GenerateQR(string qrdata, string qrImgPath)
    {
      string url = "";
      try
      {
        url = "http://chart.apis.google.com/chart?cht=qr&chs=102x102&chld=H|0&chl=" + qrdata;
        WebClient wc = new WebClient();
        byte[] qrImage = wc.DownloadData(url);
        MemoryStream ms = new MemoryStream(qrImage);
        Image img = Image.FromStream(ms);
        img.Save(qrImgPath, ImageFormat.Png);
        img.Dispose();
        ms.Close();
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error during call Google service: " + url);
      }
    }

    public void GenerateBarCode(string strBarCodeData, string strqrBarImgPath)
    {
      string url = "";
      try
      {
        url = "https://www.barcodesinc.com/generator/image.php?code=" + strBarCodeData + "&style=196&type=C128B&height=100&xres=2&font=2";
        WebClient wc = new WebClient();
        byte[] barImage = wc.DownloadData(url);
        MemoryStream mms = new MemoryStream(barImage);
        Image img = Image.FromStream(mms);
        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
        img.Save(strqrBarImgPath, ImageFormat.Png);
        mms.Close();
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error during call BarcodesInc service: " + url);
      }
    }


    public IEnumerable<OrderSummaryViewModel> GetEventOrdersSummaryCalculation(long eventId)
    {
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      var tpd = tpdRepo.Get(t => (t.TPD_Event_Id ?? 0) == eventId);
      var orderIds = tpd.Select(t => t.TPD_Order_Id).Distinct();
      var orders = oRepo.Get(o => orderIds.Contains(o.O_Order_Id));
      return orders.Select(o => new OrderSummaryViewModel()
      {
        EventId = eventId,
        OId = o.O_Id,
        OrderId = o.O_Order_Id,
        Date = o.O_OrderDateTime ?? default(DateTime),
        Quantity = tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.TPD_Purchased_Qty) ?? 0,
        Price = o.O_TotalAmount ?? 0,
        PriceNet = (o.O_TotalAmount ?? 0) - (tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.TPD_EC_Fee) ?? 0),
        Discount = tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.TicketDiscount),
        Fee = tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.TPD_EC_Fee) ?? 0,
        EventComboFee = tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.TicketECFee),
        MerchantFee = tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.TicketMerchantFee),
        CustomerFee = tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.Customer_Fee),
        Cancelled = (o.OrderStateId == 2) ? (o.O_TotalAmount ?? 0) : 0,
        Refunded = (o.OrderStateId == 3) ? (o.O_TotalAmount ?? 0) : 0,
        VarChargesAmount = o.O_VariableAmount ?? 0,
        PromoCodeAmount = tpd.Where(t => t.TPD_Order_Id == o.O_Order_Id).Sum(tt => tt.TPD_PromoCodeAmount) ?? 0,
        IsCancelled = o.OrderStateId == 2,
        IsRefunded = o.OrderStateId == 3,
        IsManualOrder = o.IsManualOrder
      });

    }

    public EventSummaryViewModel GetEventSummaryCalculation(long eventId, FilterByOrderType filter)
    {
      var orders = GetEventOrdersSummaryCalculation(eventId);
      if (orders == null)
        throw new NullReferenceException(String.Format("Event not found for id = {0}", eventId));
      return orders
              .Where(ord => (filter == FilterByOrderType.All) || ((filter == FilterByOrderType.Manual) && ord.IsManualOrder) || ((filter == FilterByOrderType.Regular) && !ord.IsManualOrder))
              .GroupBy(o => new { o.EventId }).Select(os => new EventSummaryViewModel()
      {
        EventId = eventId,
        OrderQuantity = os.Sum(x => (x.IsCancelled || x.IsRefunded ? 0 : 1)),
        TicketQuantity = os.Sum(x => (x.IsCancelled || x.IsRefunded ? 0 : x.Quantity)),
        Price = os.Sum(x => (x.IsCancelled || x.IsRefunded ? 0 : x.Price)),
        PriceNet = os.Sum(x => (x.IsCancelled || x.IsRefunded ? 0 : x.PriceNet)),
        Fee = os.Sum(x => (x.IsCancelled || x.IsRefunded ? 0 : x.Fee)),
        CustomerFee = os.Sum(x => (x.IsCancelled || x.IsRefunded ? 0 : x.CustomerFee)),
        Cancelled = os.Sum(x => x.Cancelled),
        Refunded = os.Sum(x => x.Refunded),
        VarChargesAmount = os.Sum(x => (x.IsCancelled || x.IsRefunded ? 0 : x.VarChargesAmount))
      }).FirstOrDefault();
    }

    public TicketSaleViewModel GetEventTicketSale(long eventId, FilterByOrderType filter)
    {
      IRepository<EventTicket_View> etvRepo = new GenericRepository<EventTicket_View>(_factory.ContextFactory);
      TicketSaleViewModel ticketSaleViewModel = new TicketSaleViewModel();

      var tickets = etvRepo.Get(t => t.EventID == eventId && t.OrderStateId != 2);
      var orders = GetEventOrdersSummaryCalculation(eventId);
      if (orders == null)
        throw new NullReferenceException(String.Format("Event not found for id = {0}", eventId));

      orders = orders.Where(ord => ((filter == FilterByOrderType.All) ||
                                  ((filter == FilterByOrderType.Manual) && ord.IsManualOrder) ||
                                  ((filter == FilterByOrderType.Regular) && !ord.IsManualOrder)) && ord.IsCancelled == false
                              );
      ticketSaleViewModel.VarChargesAmount = orders.Sum(x => x.VarChargesAmount);
      ticketSaleViewModel.PromoCodeAmount = orders.Sum(x => x.PromoCodeAmount);
      ticketSaleViewModel.EventComboFee = orders.Sum(x => x.EventComboFee);
      ticketSaleViewModel.Discount = orders.Sum(x => x.Discount);
      ticketSaleViewModel.MerchantFee = orders.Sum(x => x.MerchantFee);
      ticketSaleViewModel.Refunded = orders.Sum(x => x.Refunded);
      ticketSaleViewModel.TicketSales = tickets
                                      .Where(ord => (filter == FilterByOrderType.All) ||
                                          ((filter == FilterByOrderType.Manual) && ord.IsManualOrder) ||
                                          ((filter == FilterByOrderType.Regular) && !ord.IsManualOrder))
                                      .Select(ticket => new TicketSales()
                                      {
                                        TicketId = ticket.TicketId,
                                        TicketName = ticket.TicketName,
                                        TicketTypeId = ticket.TicketTypeID,
                                        TicketTypeName = ticket.TicketTypeName,
                                        Quantity = ticket.PurchasedQuantity ?? 0,
                                        PricePerTicket = ticket.TicketPrice,
                                        PricePaid = ticket.TicketPrice * (ticket.PurchasedQuantity ?? 0)
                                                    + ticket.ECFeePerTicket * (ticket.PurchasedQuantity ?? 0)
                                                    + ticket.MerchantFeePerTicket * (ticket.PurchasedQuantity ?? 0)
                                                    + ticket.Customer_Fee * (ticket.PurchasedQuantity ?? 0),
                                        PriceNet = ticket.TicketPrice * (ticket.PurchasedQuantity ?? 0)
                                      }).ToList();

      ticketSaleViewModel.TicketSales = ticketSaleViewModel.TicketSales.GroupBy(g => new { g.TicketId }).Select(x => new TicketSales()
      {
        TicketId = x.FirstOrDefault().TicketId,
        TicketName = x.FirstOrDefault().TicketName,
        TicketTypeId = x.FirstOrDefault().TicketTypeId,
        TicketTypeName = x.FirstOrDefault().TicketTypeName,
        Quantity = x.Sum(t => t.Quantity),
        PricePerTicket = x.FirstOrDefault().PricePerTicket,
        PricePaid = x.Sum(t => t.PricePaid),
        PriceNet = x.Sum(t => t.PriceNet)
      }).OrderBy(oo => oo.TicketTypeId).ToList();

      return ticketSaleViewModel;
    }

    public MemoryStream GetDownloadableEventTicketSale(FilterByOrderType filter, long eventId, string format)
    {
      format = format.Trim().ToLower();
      if (format != "xls")
        return null;
      TicketSaleViewModel ticketSaleViewModel = GetEventTicketSale(eventId, filter);
      return EventTicketSaleToXLS(ticketSaleViewModel);
    }

    private MemoryStream EventTicketSaleToXLS(TicketSaleViewModel ticketSales)
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

      ICellStyle hRightStyle = wb.CreateCellStyle();
      hRightStyle.BorderBottom = BorderStyle.Thin;
      hRightStyle.BorderTop = BorderStyle.Thin;
      hRightStyle.BorderLeft = BorderStyle.Thin;
      hRightStyle.BorderRight = BorderStyle.Thin;
      hRightStyle.Alignment = HorizontalAlignment.Right;
      bfont = wb.CreateFont();
      bfont.Boldweight = (short)FontBoldWeight.Bold;
      hRightStyle.SetFont(bfont);

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

      ICellStyle currencyRedStyle = wb.CreateCellStyle();
      currencyRedStyle.BorderBottom = BorderStyle.Thin;
      currencyRedStyle.BorderTop = BorderStyle.Thin;
      currencyRedStyle.BorderLeft = BorderStyle.Thin;
      currencyRedStyle.BorderRight = BorderStyle.Thin;
      currencyRedStyle.DataFormat = wb.CreateDataFormat().GetFormat("[red]-$#,##0.00");
      bfont = wb.CreateFont();
      bfont.Color = (short)FontColor.Red;
      currencyRedStyle.SetFont(bfont);

      ICellStyle currencyBoldStyle = wb.CreateCellStyle();
      currencyBoldStyle.BorderBottom = BorderStyle.Thin;
      currencyBoldStyle.BorderTop = BorderStyle.Thin;
      currencyBoldStyle.BorderLeft = BorderStyle.Thin;
      currencyBoldStyle.BorderRight = BorderStyle.Thin;
      currencyBoldStyle.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");
      bfont = wb.CreateFont();
      bfont.Boldweight = (short)FontBoldWeight.Bold;
      currencyBoldStyle.SetFont(bfont);

      ICellStyle Titlestyle = wb.CreateCellStyle();
      Titlestyle.BorderBottom = BorderStyle.Thin;
      Titlestyle.BorderTop = BorderStyle.Thin;
      Titlestyle.BorderLeft = BorderStyle.Thin;
      Titlestyle.BorderRight = BorderStyle.Thin;
      Titlestyle.Alignment = HorizontalAlignment.Center;
      Titlestyle.SetFont(bfont);

      ISheet sheet = wb.CreateSheet("TicketSale");

      IRow row = sheet.CreateRow(0);
      AddStyledCell(row, 0, hstyle).SetCellValue("Ticket Type");
      AddStyledCell(row, 1, hstyle).SetCellValue("Ticket Price");
      AddStyledCell(row, 2, hstyle).SetCellValue("Quantity");
      AddStyledCell(row, 3, hstyle).SetCellValue("Total Sales + Fees");
      AddStyledCell(row, 4, hstyle).SetCellValue("Total Net");
      var i = 1;
      foreach (var ticketSale in ticketSales.TicketSales)
      {
        row = sheet.CreateRow(i++);
        AddStyledCell(row, 0, style).SetCellValue(ticketSale.TicketName);
        AddStyledCell(row, 1, currencyStyle).SetCellValue((double)ticketSale.PricePerTicket);
        AddStyledCell(row, 2, style).SetCellValue(ticketSale.Quantity);
        AddStyledCell(row, 3, currencyStyle).SetCellValue((double)ticketSale.PricePaid);
        AddStyledCell(row, 4, currencyStyle).SetCellValue((double)ticketSale.PriceNet);
      }
      i += 1;
      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Variable Charges");
      AddStyledCell(row, 4, currencyStyle).SetCellValue((double)ticketSales.VarChargesAmount);

      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Discounted Tickets");
      AddStyledCell(row, 4, currencyRedStyle).SetCellValue((double)ticketSales.Discount);

      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Promo Codes");
      AddStyledCell(row, 4, currencyRedStyle).SetCellValue((double)ticketSales.PromoCodeAmount);

      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Total Gross");
      AddStyledCell(row, 4, currencyBoldStyle).SetCellValue((double)(ticketSales.TicketSales.Sum(x => x.PricePaid) + ticketSales.VarChargesAmount - ticketSales.Discount - ticketSales.PromoCodeAmount));

      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Refunds");
      AddStyledCell(row, 4, currencyRedStyle).SetCellValue((double)ticketSales.Refunded);

      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Total Gross After Refunds");
      AddStyledCell(row, 4, currencyStyle).SetCellValue((double)(ticketSales.TicketSales.Sum(x => x.PricePaid) + ticketSales.VarChargesAmount - ticketSales.Discount - ticketSales.PromoCodeAmount - ticketSales.Refunded));

      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Total Net Payout");
      AddStyledCell(row, 4, currencyBoldStyle).SetCellValue((double)(ticketSales.TicketSales.Sum(x => x.PriceNet) + ticketSales.VarChargesAmount - ticketSales.Discount - ticketSales.PromoCodeAmount - ticketSales.Refunded));

      i += 4;
      row = sheet.CreateRow(i++);
      AddStyledCell(row, 3, hRightStyle).SetCellValue("Eventcombo Fees");
      AddStyledCell(row, 4, currencyStyle).SetCellValue((double)(ticketSales.EventComboFee));

      for (i = 0; i <= 4; i++)
      {
        sheet.AutoSizeColumn(i);
        sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
      }
      wb.Write(res);
      res.Position = 0;
      return res;
    }

    private ICell AddStyledCell(IRow row, int cellnum, ICellStyle style, CellType ctype = CellType.String)
    {
      ICell cell = row.CreateCell(cellnum, ctype);
      cell.CellStyle = style;
      return cell;
    }

  }
}
