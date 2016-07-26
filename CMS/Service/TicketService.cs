using System;
using System.Collections.Generic;
using System.Web;
using CMS.Models;
using CMS.DAL;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Net.Mime;
using CMS.Controllers;
using CMS.Utils;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

namespace CMS.Service
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

    private IEnumerable<OrderMainViewModel> GetOrdersForUser(string userId, string searchStr, OrderSearch orderSearch)
    {
      IRepository<v_OrderList> orderRepo = new GenericRepository<v_OrderList>(_factory.ContextFactory);

      List<OrderMainViewModel> ordersVM = new List<OrderMainViewModel>();
      OrderMainViewModel oVM = null;
      IEnumerable<v_OrderList> orders = null;

      string srch = String.IsNullOrEmpty(searchStr) ? "" : searchStr.Trim();

      if (String.IsNullOrEmpty(userId))
      {
        if (String.IsNullOrEmpty(srch))
          orders = orderRepo.Get();
        else
          orders = orderRepo.Get(filter: (o => o.OrderId.Contains(srch) || o.Email.Contains(srch) || o.FirstName.Contains(srch)
            || o.LastName.Contains(srch) || o.Name.Contains(srch) || (o.EventID.ToString() == srch)));
      }
      else
      {
        if (String.IsNullOrEmpty(srch))
          orders = orderRepo.Get(filter: (o => o.UserId == userId));
        else
          orders = orderRepo.Get(filter: (o => (o.UserId == userId) &&
            (o.OrderId.Contains(srch) || o.Email.Contains(srch) || o.FirstName.Contains(srch)
            || o.LastName.Contains(srch) || o.Name.Contains(srch) || (o.EventID.ToString() == srch))));
      }
      foreach (var order in orders)
      {
        oVM = _mapper.Map<OrderMainViewModel>(order);
        ordersVM.Add(oVM);
      }
      return ordersVM;
    }

    private IEnumerable<OrderMainViewModel> GetUpcomingOrdersForUser(string userId, string searchStr, OrderSearch orderSearch)
    {
      IEnumerable<OrderMainViewModel> orders = GetOrdersForUser(userId, searchStr, orderSearch)
        .Where(t => ((!t.EventCancelled) && (t.EventEndDate >= DateTime.Today))).ToList();
      return orders;
    }

    private IEnumerable<OrderMainViewModel> GetPastOrdersForUser(string userId, string searchStr, OrderSearch orderSearch)
    {
      IEnumerable<OrderMainViewModel> orders = GetOrdersForUser(userId, searchStr, orderSearch)
        .Where(t => ((t.EventCancelled) || (t.EventEndDate < DateTime.Today))).ToList();
      return orders;
    }

    public IEnumerable<OrderMainViewModel> GetOrdersList(OrderTypes type, string userId, string searchStr, OrderSearch orderSearch)
    {
      IEnumerable<OrderMainViewModel> orders = null;
      switch (type)
      {
        case OrderTypes.Upcoming:
          orders = GetUpcomingOrdersForUser(userId, searchStr, orderSearch);
          break;
        case OrderTypes.Past:
          orders = GetPastOrdersForUser(userId, searchStr, orderSearch);
          break;
        default:
          break;
      }
      return orders;
    }


    public long GetOrdersCount(OrderTypes type, string userId)
    {
      long res = 0;
      switch (type)
      {
        case OrderTypes.Upcoming:
          res = GetUpcomingOrdersCountForUser(userId);
          break;
        case OrderTypes.Past:
          res = GetPastOrdersCountForUser(userId);
          break;
        default:
          break;
      }
      return res;
    }

    private long GetPastOrdersCountForUser(string userId)
    {
      IRepository<v_OrderList> orderRepo = new GenericRepository<v_OrderList>(_factory.ContextFactory);
      long result = 0;
      if (String.IsNullOrEmpty(userId))
        result = orderRepo.Get(filter: (o => o.EventEndDate < DateTime.Today)).Count();
      else
        result = orderRepo.Get(filter: (o => (o.EventEndDate < DateTime.Today) && (o.UserId == userId))).Count();
      return result;
    }

    private long GetUpcomingOrdersCountForUser(string userId)
    {
      IRepository<v_OrderList> orderRepo = new GenericRepository<v_OrderList>(_factory.ContextFactory);
      long result = 0;
      if (String.IsNullOrEmpty(userId))
        result = orderRepo.Get(filter: (o => o.EventEndDate >= DateTime.Today)).Count();
      else
        result = orderRepo.Get(filter: (o => (o.EventEndDate >= DateTime.Today) && (o.UserId == userId))).Count();
      return result;
    }


    public OrderDetailsViewModel GetOrderDetails(string orderId)
    {
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      var order = orderRepo.Get(filter: (o => ((o.O_Order_Id == orderId)))).FirstOrDefault();
      if (order == null)
        return null;

      OrderDetailsViewModel details = new OrderDetailsViewModel() { OrderId = orderId };
      details.Payment = _dbservice.GetPaymentInfo(orderId);

      IRepository<CMS.Models.Profile> userRepo = new GenericRepository<CMS.Models.Profile>(_factory.ContextFactory);
      var user = userRepo.Get(filter: (u => u.UserID == order.O_User_Id)).First();
      details.Email = user.Email;

      IRepository<Ticket_Purchased_Detail> ptRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var tickets = ptRepo.Get(filter: (t => ((t.TPD_Order_Id == orderId) && (t.TPD_User_Id == order.O_User_Id))));
      details.EventLocation = String.Join("; ", tickets.Select(t => t.Ticket_Quantity_Detail.Address.ConsolidateAddress).Distinct().ToArray());
      details.EventDate = String.Join("; ", tickets.Select(t => t.Ticket_Quantity_Detail.Publish_Event_Detail.PE_MultipleVenue_id > 0 ?
              t.Ticket_Quantity_Detail.Publish_Event_Detail.MultipleEvent.StartingFrom :
              t.Ticket_Quantity_Detail.Publish_Event_Detail.EventVenue.EventStartDate).Distinct().ToArray());

      IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
      foreach (var att in attRepo.Get(filter: (tb => ((tb.OrderId == orderId) && (tb.UserId == order.O_User_Id)))))
        details.Attendees.Add(_mapper.Map<AttendeeViewModel>(att));

      return details;
    }


    public bool SaveOrderDetails(OrderDetailsViewModel model, string baseUrl, string filePath)
    {
      bool res = true;
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
          List<AttendeeViewModel> selected = new List<AttendeeViewModel>();
          foreach (var attendee in attRepo.Get(filter: (a => ((a.OrderId == model.OrderId)))))
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


    public bool CancelOrder(string orderId)
    {
      bool res = true;
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
          Order_Detail_T order = orderRepo.Get(filter: (o => ((o.O_Order_Id == orderId)))).SingleOrDefault();
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
              INotification notification = new OrderCancelNotification(_factory, orderId, order.O_User_Id);
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

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      var ticket = tpdRepo.Get(filter: (t => t.TPD_Order_Id == orderId)).FirstOrDefault();
      var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();

      if ((ticket == null) || (order == null))
        return null;

      var user = ticket.AspNetUser.Profiles.FirstOrDefault();
      string name = !String.IsNullOrWhiteSpace(order.O_First_Name) ? order.O_First_Name : (user == null ? "" : user.FirstName);

      return generateTicketPDF(ticket.TPD_GUID, ticket.TPD_Event_Id ?? 0, null, name, filePath);
    }

    public MemoryStream generateTicketPDF(string guid, long eventid, List<Email_Tag> emailtag, string fname, string htmlPath)
    {
      WebClient wc = new WebClient();
      MemoryStream mms = new MemoryStream();
      EncryptDecrypt Ecode = new EncryptDecrypt();
      EmsEntities db = new EmsEntities();

      string htmlText = "";
      try
      {
        var TicketDetail = (from Ord in db.TicketOrderDetails
                            where Ord.T_Guid == guid
                            select Ord).ToList();
        if (TicketDetail != null)
        {
          var count = 0;
          var lastcount = TicketDetail.Count();
          foreach (var item in TicketDetail)
          {
            count = count + 1;

            string barImgPath = htmlPath + "/Images/br_" + item.T_Id + ".Png";

            string qrImgPath = htmlPath + "/Images/QR_" + item.T_Id + ".Png";

            // Ticket and event details
            var TQtydetail = (from tQty in db.Ticket_Quantity_Detail
                              where tQty.TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                               where t.T_Id == item.T_Id
                                                               select t.T_TQD_Id).FirstOrDefault().ToString()
                              select tQty
                      ).FirstOrDefault();

            var TPurchasedetail = (from tQty in db.Ticket_Purchased_Detail
                                   where tQty.TPD_TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                                        where t.T_Id == item.T_Id
                                                                        select t.T_TQD_Id).FirstOrDefault().ToString()
                                                                    && tQty.TPD_GUID == guid
                                   select tQty
                                 ).FirstOrDefault();


            var Ticketorderdetail = (from f in db.Order_Detail_T where f.O_Order_Id == item.T_Order_Id select f).FirstOrDefault();
            var userdetail = (from prof in db.Profiles where prof.UserID == TPurchasedetail.TPD_User_Id select prof).FirstOrDefault();
            var Edtails = (from p in db.Events
                           join user in db.Profiles on p.UserID equals user.UserID
                           join org in db.Event_Orgnizer_Detail on p.EventID equals org.Orgnizer_Event_Id
                           join orgprof in db.Profiles on org.UserId equals orgprof.UserID
                           where p.EventID == eventid && org.DefaultOrg == "Y"
                           select new
                           {
                             EventTitle = p.EventTitle,
                             UserName = user.FirstName,
                             Organizername = orgprof.FirstName,
                             OrganiserEmail = orgprof.Email,
                             Addresstatus = p.AddressStatus,
                           }).ToList().Distinct().FirstOrDefault();
            var Organizerdetail = (from p in db.Event_Orgnizer_Detail join k in db.Organizer_Master on p.OrganizerMaster_Id equals k.Orgnizer_Id where p.Orgnizer_Event_Id == eventid && p.DefaultOrg == "Y" select k).FirstOrDefault();

            var usernmae = !string.IsNullOrEmpty(Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name) ? Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name : Edtails.UserName;
            var datetime = DateTime.Parse(TQtydetail.TQD_StartDate);
            var day = datetime.DayOfWeek.ToString();
            var Sdate = datetime.ToString("MMM dd, yyyy");
            string Eventtype = "", Etype = "", add = "";

            var addresslist = (from a in db.Addresses where a.AddressID == TQtydetail.TQD_AddressId select a).FirstOrDefault();
            if ((addresslist) != null)
            {
              add = addresslist.ConsolidateAddress;
            }
            var time = TQtydetail.TQD_StartTime;

            if (Edtails.Addresstatus == "PastLocation")
            {
              Eventtype = "Single";
            }
            else
            {
              Eventtype = Edtails.Addresstatus;
            }
            if (Edtails.Addresstatus == "Multiple")
            {
              Etype = "*This event has multiple venues";
              add = add + "*";
            }
            else
            {
              Etype = "";
            }

            //

            string xel = createxml(item.T_Order_Id, eventid, guid, TQtydetail, TPurchasedetail);

            generateBarCode(item.T_Id, barImgPath);
            generateQR(xel.ToString(), qrImgPath);


            string Qrcode = "<img style = 'width:150px;height:150px' src ='" + qrImgPath + "' alt = 'QRCode' />";
            string barcode = "<img  src ='" + barImgPath + "' alt = 'BarCode' >";
            string Imagelogo = htmlPath + "/Images/logo_vertical.png";
            string logoImage = "<img style='width:57px;height:375px' src ='" + Imagelogo + "' alt = 'Logo' >";
            EventCreation ccEvent = new EventCreation();
            var Images = GetImages(eventid).FirstOrDefault();
            string Imageevent = "";
            if (string.IsNullOrEmpty(Images))
            {
              Imageevent = htmlPath + "/Images/default_event_image.jpg";
            }
            else
            {
              Imageevent = htmlPath + Images;
            }
            string Imagevent = "<img style='width:200px;height:200px;' src ='" + Imageevent + "' alt = 'Image' >";

            //Order Details
            StringBuilder strHTML = new StringBuilder();
            var itemQuery = from TqtId in db.Ticket_Purchased_Detail

                            where TqtId.TPD_GUID == guid && TqtId.TPD_Event_Id == eventid

                            select TqtId.TPD_TQD_Id;
            var myOrderId = (from p in db.Ticket_Purchased_Detail where p.TPD_GUID == guid && p.TPD_Event_Id == eventid select p.TPD_Order_Id).ToList().Distinct().FirstOrDefault();
            var myOrderDetails = (from p in db.Order_Detail_T where p.O_Order_Id == myOrderId select p).FirstOrDefault();

            var myAddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_AddressId).ToList().Distinct();
            var myaddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p).ToList();

            strHTML.Append("<table style='width: 100 %;'>");
            var eventtype = "";
            var eventname = "";
            var startdate = "";
            var enddate = "";
            var Event = (from myEnt in db.Events where myEnt.EventID == eventid select myEnt).FirstOrDefault();

            int timeZoneID = Int32.Parse(Event.TimeZone);
            TimeZoneDetail td = db.TimeZoneDetails.First(x => x.TimeZone_Id == timeZoneID);
            DateTimeWithZone dtzstart, dzend;
            DateTimeWithZone dtzcreated;


            var singledate = (from date in db.EventVenues where date.EventID == eventid select date).FirstOrDefault();
            if (singledate != null)
            {
              if (td != null)
              {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
              }
              else
              {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
              }


            }
            else
            {
              var muldate = (from date in db.MultipleEvents where date.EventID == eventid select date).FirstOrDefault();
              if (td != null)
              {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
              }
              else
              {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
              }


            }
            startdate = dtzstart.LocalTime.ToLongDateString() + " " + dtzstart.LocalTime.ToLongTimeString();
            enddate = dzend.LocalTime.ToLongDateString() + " " + dzend.LocalTime.ToLongTimeString();

            if (Edtails.Addresstatus == "Online")
            {
              eventname = "Online";

            }
            else if (Edtails.Addresstatus == "PastLocation")
            {


              var Address = (from address in db.Addresses
                             where address.AddressID == (from fb in db.Events where fb.EventID == eventid select fb.LastLocationAddress).FirstOrDefault()
                             select address).FirstOrDefault();

              eventname = Address.ConsolidateAddress;
              if (Address != null)
              {
                eventname = Address.ConsolidateAddress;
              }
              else
              {
                eventname = "";
              }

            }
            else
            {
              var Address = (from address in db.Addresses
                             where address.EventId == eventid
                             select address).FirstOrDefault();
              if (Address != null)
              {
                eventname = Address.ConsolidateAddress;
              }
              else
              {
                eventname = "";
              }
            }
            if (myAddress.Count() == 1)
            {

              foreach (var item1 in myAddress)
              {
                if (item1 == 0)
                {


                  var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();

                  foreach (var vdate in myDatescnt)
                  {
                    //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                    strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                    strHTML.Append("</tr>");
                    var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                     join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                     join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                     join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                     where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                     select new
                                     {
                                       eventid = TP.TPD_Event_Id,
                                       Promocode = TP.TPD_PromoCodeID,
                                       Promocodeamt = TP.TPD_PromoCodeAmount,
                                       username = l.FirstName,
                                       Ticketname = t.T_name,
                                       Quantity = TP.TPD_Purchased_Qty,
                                       Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                     }).ToList();


                    foreach (var qty in itemtoadd)
                    {

                      if (qty.Promocode != null && qty.Promocode != 0)
                      {
                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                        strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                        strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                        strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                        strHTML.Append("</tr>");
                        var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                        strHTML.Append("<tr align='left'>");
                        strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                        var promoprice = qty.Promocodeamt * qty.Quantity;
                        strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                        strHTML.Append("</tr>");
                      }
                      else
                      {
                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                        strHTML.Append("</tr>");
                      }



                    }



                  }
                }
                else
                {
                  var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();
                  //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "</p ></tr > ");
                  var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();
                  foreach (var vdate in myDatescnt)
                  {
                    var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                     join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                     join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                     join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                     where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                     select new
                                     {
                                       eventid = TP.TPD_Event_Id,
                                       Promocode = TP.TPD_PromoCodeID,
                                       Promocodeamt = TP.TPD_PromoCodeAmount,
                                       username = l.FirstName,
                                       Ticketname = t.T_name,
                                       Quantity = TP.TPD_Purchased_Qty,
                                       Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                     }).ToList();

                    //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                    strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                    strHTML.Append("</tr>");
                    foreach (var qty in itemtoadd)
                    {

                      if (qty.Promocode != null && qty.Promocode != 0)
                      {
                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                        strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                        strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                        strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                        strHTML.Append("</tr>");
                        var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                        strHTML.Append("<tr align='left'>");
                        strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                        var promoprice = qty.Promocodeamt * qty.Quantity;
                        strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                        strHTML.Append("</tr>");
                      }
                      else
                      {
                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                        strHTML.Append("</tr>");
                      }



                    }



                  }




                }

              }



            }
            else
            {

              foreach (var item1 in myAddress)
              {

                var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                 join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                 join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                 join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                 where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                 select new
                                 {
                                   eventid = TP.TPD_Event_Id,
                                   Promocode = TP.TPD_PromoCodeID,
                                   Promocodeamt = TP.TPD_PromoCodeAmount,
                                   username = l.FirstName,
                                   Ticketname = t.T_name,
                                   Quantity = TP.TPD_Purchased_Qty,
                                   Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                 }).ToList();

                var dateofaddress = (from p in db.Ticket_Quantity_Detail
                                     join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                     where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                     select p.TQD_StartDate).ToList().Distinct().FirstOrDefault();
                var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();

                strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "<span style='float:right;'>" + Convert.ToDateTime(dateofaddress).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;' > Type </th >");
                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </ th >");
                strHTML.Append("</tr>");
                foreach (var qty in itemtoadd)
                {

                  if (qty.Promocode != null && qty.Promocode != 0)
                  {
                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                    strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                    strHTML.Append("</tr>");
                    var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                    strHTML.Append("<tr align='left'>");
                    strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                    var promoprice = qty.Promocodeamt * qty.Quantity;
                    strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                    strHTML.Append("</tr>");
                  }
                  else
                  {
                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                    strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                    strHTML.Append("</tr>");
                  }
                }

              }


            }

            var variabledescid = myOrderDetails.O_VariableId;
            if (!string.IsNullOrWhiteSpace(variabledescid))
            {
              if (variabledescid.Contains(','))
              {
                string[] words = variabledescid.Split(',');
                foreach (string word in words)
                {
                  var vardesc = (from p in db.Event_VariableDesc
                                 where p.Variable_Id.ToString() == word && p.Event_Id == eventid
                                 select p).FirstOrDefault();
                  strHTML.Append("<tr align='right'> ");
                  strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                  strHTML.Append("</tr> ");
                }
              }
              else
              {
                var vardesc = (from p in db.Event_VariableDesc
                               where p.Variable_Id.ToString() == variabledescid && p.Event_Id == eventid
                               select p).FirstOrDefault();

                if (vardesc != null)
                {
                  strHTML.Append("<tr align='right'> ");
                  strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                  strHTML.Append("</tr> ");
                }
              }
            }
            strHTML.Append("<tr align='right'> ");
            strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>Total :$" + myOrderDetails.O_TotalAmount + " </td></tr>");
            if (Edtails.Addresstatus == "Multiple")
            {
              eventtype = "* This event has multiple venues ";
            }
            else
            {
              eventtype = "";
            }


            var myBillingdeatils = (from p in db.BillingAddresses where p.Guid == guid && p.OrderId == myOrderId select p).ToList().Distinct().FirstOrDefault();

            var cardtext = "";


            if (myBillingdeatils != null)
            {
              var Scardnumber = Ecode.DecryptText(myBillingdeatils.CardId).Trim();
              var Icardlength = Scardnumber.Length;
              var WrVS = "";
              int k = 1;
              for (int i = 0; i < Icardlength; i++)
              {

                WrVS += "X";
                if (k == 4)
                {
                  WrVS += "-";
                  k = 0;
                }


                k++;

              }
              if (WrVS.EndsWith("-"))
              {
                WrVS = WrVS.Substring(0, WrVS.LastIndexOf("-"));
              }
              var rvrs = WrVS.Substring(0, WrVS.LastIndexOf("-") + 1);
              var rvrsd = rvrs.Replace("-", "");
              var chrlength = Icardlength - rvrsd.Length;
              var result = Scardnumber.Substring(Icardlength - Math.Min(chrlength, Icardlength));
              var finalstr = rvrs + result;

              var touper = "";

              if (!string.IsNullOrWhiteSpace(myBillingdeatils.card_type))
              {
                var cardtype = Ecode.DecryptText(myBillingdeatils.card_type);
                touper = char.ToUpper(cardtype[0]) + cardtype.Substring(1);
              }
              else
              {
                touper = "";
              }

              cardtext = "Charge to :" + touper + "  " + finalstr;
            }
            else
            {
              cardtext = "Charge to : Paypal";
            }
            if (myOrderDetails.O_TotalAmount <= 0)
            {
              cardtext = "";
            }
            strHTML.Append("<tr align='center'> ");
            strHTML.Append("<td colspan='4' style='font-size:15px; padding:10px 5px;'>" + cardtext + " </td></tr>");
            strHTML.Append("<tr align='center'><td colspan='4' style='font-size:15px;'>");
            strHTML.Append("<p style='background:#fff9cf; padding:10px 15px; display:inline-block; margin:0px;'>This charge will appear on your card statement as Eventcombo * { " + Edtails.EventTitle + "}</p>");
            strHTML.Append("<p style='color:#696564;' >This order is subject to Eventcombo '");
            strHTML.Append("<a href='#' style='color:#0f90ba;'>Terms of Service </a> , <a style='color:#0f90ba;' href='#'>Privacy Policy </a> and <a style='color:#0f90ba;' href='#'>Cookie Policy </a></p>");
            strHTML.Append("</td></tr></table > ");

            //Order Details

            htmlText += wc.DownloadString(htmlPath + "/email.html");

            htmlText = htmlText.Replace("¶¶EventTitleId¶¶", Edtails.EventTitle);
            htmlText = htmlText.Replace("¶¶EventStartDateID¶¶", Sdate);
            htmlText = htmlText.Replace("¶¶EventVenueID¶¶", add);
            htmlText = htmlText.Replace("¶¶EventOrderNO¶¶", item.T_Order_Id);
            htmlText = htmlText.Replace("¶¶UserFirstNameID¶¶", fname);
            htmlText = htmlText.Replace("¶¶TicketOrderDateId¶¶", System.DateTime.Now.ToLongDateString());
            htmlText = htmlText.Replace("¶¶EventStartTimeID¶¶", time);
            htmlText = htmlText.Replace("¶¶EventBarcodeId¶¶", barcode);
            htmlText = htmlText.Replace("¶¶EventQrCode¶¶", Qrcode);
            htmlText = htmlText.Replace("¶¶EventImage¶¶", Imagevent);
            htmlText = htmlText.Replace("¶¶EventdayId¶¶", day);
            htmlText = htmlText.Replace("¶¶EventLogo¶¶", logoImage);
            htmlText = htmlText.Replace("¶¶Eventtype¶¶", Eventtype);
            htmlText = htmlText.Replace("¶¶EventDescription¶¶", "");
            htmlText = htmlText.Replace("¶¶Eventtypedetail¶¶", Etype);
            htmlText = htmlText.Replace("¶¶OrderDetail¶¶", strHTML.ToString());
            htmlText = htmlText.Replace("¶¶EventOrganiserName¶¶", !string.IsNullOrEmpty(Organizerdetail.Orgnizer_Name) ? Organizerdetail.Orgnizer_Name : Edtails.Organizername);
            htmlText = htmlText.Replace("¶¶EventOrganiserEmail¶¶", !string.IsNullOrEmpty(Organizerdetail.Organizer_Email) ? Organizerdetail.Organizer_Email : Edtails.OrganiserEmail);
            if (count == lastcount)
            {
              htmlText = htmlText.Replace("¶¶Linebreak¶¶", "");
            }
            else
            {
              htmlText = htmlText.Replace("¶¶Linebreak¶¶", "<div style='page-break-before: always;width:100% text - align: center></div>");
            }
          }



          var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
          var pdfBytes = htmlToPdf.GeneratePdf(htmlText);
          mms = new MemoryStream(pdfBytes);
        }
      }
      catch (Exception ex)
      {
        //ExceptionLogging.SendErrorToText(ex);
      }
      return mms;

    }

    private string createxml(string Ticketnumber, long eventid, string guid, Ticket_Quantity_Detail TQtydetail, Ticket_Purchased_Detail TPurchasedetail)
    {
      using (EmsEntities db = new EmsEntities())
      {
        string tprice = "", Tdiscount = "", totalprice = "", tickettype = "", addressdetail = "";
        var TicketDetail = (from t in db.Tickets where t.T_Id == TQtydetail.TQD_Ticket_Id select t).FirstOrDefault();
        var eDetails = (from e in db.Events where e.EventID == eventid select e).FirstOrDefault();
        var address = (from a in db.Addresses where a.AddressID == TQtydetail.TQD_AddressId select a).FirstOrDefault();
        var username = (from prof in db.Profiles where prof.UserID == TPurchasedetail.TPD_User_Id select prof.FirstName + " " + prof.LastName).FirstOrDefault();
        if (address != null)
        {
          addressdetail = address.ConsolidateAddress;
        }
        var tickettypeid = TicketDetail.TicketTypeID;
        if (tickettypeid == 1)
        {
          tprice = "$ 0.00";
          Tdiscount = "$ 0.00";
          totalprice = "$ 0.00";
          tickettype = "Free";
        }
        if (tickettypeid == 2)
        {
          var t = TPurchasedetail.TPD_Amount / TPurchasedetail.TPD_Purchased_Qty;
          tprice = "$ " + TicketDetail.TotalPrice;
          Tdiscount = "$ " + TicketDetail.T_Discount;
          totalprice = "$ " + t;
          tickettype = "Paid";
        }
        if (tickettypeid == 3)
        {
          tprice = "$ " + TPurchasedetail.TPD_Donate;
          Tdiscount = "$ 0.00";
          totalprice = "$ " + TPurchasedetail.TPD_Donate;
          tickettype = "Donate";
        }

        StringBuilder strInfo = new StringBuilder();

        strInfo.Append("UniqueOrderNumber:" + Ticketnumber);
        strInfo.Append("TicketTypeName:" + TicketDetail.T_name);
        strInfo.Append("TotalTicketQuantityPerOrder:" + 1);
        strInfo.Append("TicketPrice:" + tprice);
        strInfo.Append("TicketDiscountAmount:" + Tdiscount);
        strInfo.Append("TotalTicketPrice:" + totalprice);
        strInfo.Append("TicketType:" + tickettype);
        strInfo.Append("CustomerName:" + username);
        strInfo.Append("EventName:" + eDetails.EventTitle);
        strInfo.Append("EventStartDate:" + TQtydetail.TQD_StartDate + " " + TQtydetail.TQD_StartTime);
        strInfo.Append("EventVenueName:" + addressdetail);

        return strInfo.ToString();
      }
    }

    private void generateQR(string qrdata, string qrImgPath)
    {
      WebClient wc = new WebClient();
      string url = "http://chart.apis.google.com/chart?cht=qr&chs=150x150&chl=" + qrdata;
      byte[] qrImage = wc.DownloadData(url);
      MemoryStream ms = new MemoryStream(qrImage);
      Image img = Image.FromStream(ms);
      img.Save(qrImgPath, ImageFormat.Png);
      img.Dispose();
      ms.Close();
    }

    private void generateBarCode(string strBarCodeData, string strqrBarImgPath)
    {
      WebClient wc = new WebClient();
      string url = "https://www.barcodesinc.com/generator/image.php?code=" + strBarCodeData + "&style=196&type=C128B&width=250&height=70&xres=1&font=3";
      byte[] barImage = wc.DownloadData(url);
      MemoryStream mms = new MemoryStream(barImage);
      Image img = Image.FromStream(mms);
      img.Save(strqrBarImgPath, ImageFormat.Png);
      mms.Close();
    }

    public List<string> GetImages(long EventId)
    {
      using (EmsEntities db = new EmsEntities())
      {

        return (from myRow in db.EventImages

                where myRow.EventID == EventId
                orderby myRow.EventImageID
                select "/Images/events/event_flyers/imagepath/" + myRow.EventImageUrl).ToList();
      }
    }


  }
}
