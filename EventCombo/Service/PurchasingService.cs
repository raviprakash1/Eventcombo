using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AutoMapper;
using EventCombo.DAL;
using EventCombo.Models;
using EventCombo.Utils;
using Hangfire;
using NLog;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Service
{
  public class PurchasingService : IPurchasingService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private ILogger _logger;
    private IECImageService _iService;
    public static readonly int MaxLockCount = 1;
    public static readonly int LockTimeout = 10;

    public PurchasingService() : this(new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory(new EventComboEntities())), AutomapperConfig.Config.CreateMapper()) { }

    public PurchasingService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
      _logger = LogManager.GetCurrentClassLogger();
      _iService = new ECImageService(_factory, _mapper, new ECImageStorage(_mapper));
    }


    public TicketLockResult TryLockTickets(TicketLockRequest req)
    {
      TicketLockResult res = new TicketLockResult();
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          res = CanLock(req);
          if (res.TicketsAvailable)
          {
            if (res.LockCount > 0)
            {
              DeleteOldestLock(req, uow);
              res.LockCount = GetLockedCount(req.EventId, req.IP);
            }
            if (res.LockCount == 0)
            {
              res.LockId = LockTickets(req, uow).ToString();
              uow.Context.SaveChanges();
              uow.Commit();
            }
            else
              uow.Rollback();
          }
          else
            uow.Rollback();
        }
        catch (Exception ex)
        {
          _logger.Error(ex, "Error during TryLockTickets.");
          uow.Rollback();
        }

      return res;
    }

    private Guid LockTickets(TicketLockRequest req, IUnitOfWork uow)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);
      LockOrder lOrder = new LockOrder()
      {
        LockOrderId = Guid.NewGuid(),
        EventId = req.EventId,
        IP = req.IP,
        Locktime = DateTime.UtcNow,
        UserId = req.UserId
      };
      foreach (var ticket in req.Tickets)
      {
        var tDB = tqdRepo.GetByID(ticket.TicketQuantityDetailId);
        if (tDB != null)
        {
          LockTicket lTicket = _mapper.Map<LockTicket>(ticket);
          int feeType;
          if (Int32.TryParse(tDB.Ticket.Fees_Type, out feeType))
            lTicket.ECFeeType = feeType;
          lTicket.LockOrderId = lOrder.LockOrderId;
          lTicket.Price = tDB.Ticket.Price ?? 0;
          lTicket.CustomerFee = tDB.Ticket.Customer_Fee ?? 0;
          lTicket.ECFeeAmount = tDB.Ticket.T_EcAmount ?? 0;
          lTicket.ECFeePercent = (tDB.Ticket.Price ?? 0) * (tDB.Ticket.T_Ecpercent ?? 0) / 100;
          lTicket.Discount = tDB.Ticket.T_Discount ?? 0;
          if ((tDB.Ticket.TicketTypeID ?? 0) == 3)
            lTicket.Quantity = 1;
          lOrder.LockTickets.Add(lTicket);
        }
      }
      loRepo.Insert(lOrder);
      uow.Context.SaveChanges();
      BackgroundJob.Schedule<PurchasingService>(ps => ps.DeleteTicketLock(lOrder.LockOrderId.ToString(), req.IP), TimeSpan.FromSeconds(60 * LockTimeout + 30));
      return lOrder.LockOrderId;
    }

    private void DeleteOldestLock(TicketLockRequest req, IUnitOfWork uow)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      IRepository<LockTicket> ltRepo = new GenericRepository<LockTicket>(_factory.ContextFactory);
      var lorder = loRepo.Get(filter: (o => (o.EventId == req.EventId) && (o.IP == req.IP)),
                              orderBy: (query => query.OrderBy(o => o.Locktime))).FirstOrDefault();
      if (lorder != null)
      {
        foreach (var ticket in lorder.LockTickets.ToList())
          ltRepo.Delete(ticket);
        loRepo.Delete(lorder);
      }
      uow.Context.SaveChanges();
    }

    public bool DeleteTicketLock(string lockId, string ip)
    {
      bool result = false;
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          result = DeleteTicketLock(lockId, ip, uow);
          if (result)
            uow.Commit();
          else
            uow.Rollback();
        }
        catch (Exception ex)
        {
          _logger.Error(ex, String.Format("Exception during DeleteTicketLock fo lockId = {0}", lockId));
          uow.Rollback();
        }
      return result;
    }

    public bool DeleteTicketLock(string lockId, string ip, IUnitOfWork uow)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      IRepository<LockTicket> ltRepo = new GenericRepository<LockTicket>(_factory.ContextFactory);
      var lGuid = new Guid(lockId);
      var lorder = loRepo.GetByID(lGuid);
      if ((lorder == null) || (lorder.IP != ip))
        return false;

      foreach (var ticket in lorder.LockTickets.ToList())
        ltRepo.Delete(ticket);
      loRepo.Delete(lorder);
      uow.Context.SaveChanges();
      return true;
    }

    private int GetLockedCount(long eventId, string ip)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      return loRepo.Get(s => (s.EventId == eventId) && (s.IP == ip)).Count();
    }

    public TicketLockResult CanLock(TicketLockRequest req)
    {
      TicketLockResult res = new TicketLockResult()
      {
        LockCount = 0,
        LockId = "",
        TicketsAvailable = true,
        Message = ""
      };
      if (GetLockedCount(req.EventId, req.IP) >= MaxLockCount)
      {
        res.LockCount = MaxLockCount;
        res.Message = String.Format("There is {0} tries from your IP. Do you like to clear data about your started orders?", MaxLockCount);
      }
      else
      {
        IRepository<AvailableTicket_View> atRepo = new GenericRepository<AvailableTicket_View>(_factory.ContextFactory);
        IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
        var e = eRepo.GetByID(req.EventId);
        res.TicketsAvailable = (e != null) && !String.IsNullOrEmpty(e.EventStatus) && (e.EventStatus.ToUpper() == "LIVE");
        if (res.TicketsAvailable)
          foreach (var ticket in req.Tickets)
          {
            res.TicketsAvailable = res.TicketsAvailable && atRepo.Get(t => (t.TQD_Id == ticket.TicketQuantityDetailId) && ((t.RemainingQuantity ?? 0) >= ticket.Quantity)).Any();
          }
      }
      return res;
    }

    public void DeleteExpiredLocks(int minutes)
    {
      _factory.ContextFactory.GetContext().Database.ExecuteSqlCommand("EXEC DeleteExpiredLocks @minutes", new SqlParameter("minutes", SqlDbType.Int) { Value = minutes });
    }

    public EventPurchaseInfoViewModel GetEventPurchaseInfo(string lockId, string userId, string ip)
    {
      if (String.IsNullOrEmpty(lockId))
        throw new ArgumentNullException("lockId");
      if (String.IsNullOrEmpty(ip))
        throw new ArgumentNullException("ip");

      EventPurchaseInfoViewModel res = new EventPurchaseInfoViewModel();
      res.PurchaseInfo.LockId = lockId;
      var lockGuid = new Guid(lockId);

      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      var lOrder = loRepo.Get(o => (o.LockOrderId == lockGuid) && (o.IP == ip)).FirstOrDefault();
      if (lOrder == null)
      {
        _logger.Error(String.Format("Order for lockId = {0} and ip = {1} not found.", lockId, ip));
        return res;
      }
      res.SecondsRemains = Convert.ToInt32(Math.Round((lOrder.Locktime.AddMinutes(LockTimeout) - DateTime.UtcNow).TotalSeconds));

      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      var ev = eRepo.GetByID(lOrder.EventId);
      if (ev == null)
        throw new Exception(String.Format("Event {0} not found.", lOrder.EventId));

      if ((ev.ECBackgroundId ?? 0) > 0)
      {
        var img = _iService.GetImageById(ev.ECBackgroundId ?? 0);
        if (img != null)
          res.BGImageUrl = img.ImagePath;
      }
      res.BGColor = ev.BackgroundColor;

      res.EventId = ev.EventID;
      res.EventTitle = ev.EventTitle;
      res.EventUrl = EventService.GetEventUrl(res.EventId, res.EventTitle, new UrlHelper(HttpContext.Current.Request.RequestContext));
      var address = ev.Addresses.FirstOrDefault();
      if (address != null)
      {
        res.Address = address.ConsolidateAddress;
        res.VenueName = address.VenueName;
      }
      else if (ev.AddressStatus == "Online")
        res.Address = "Online";

      res.StartDate = GetEventDatesInfo(ev);

      IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);
      res.TotalAmount = 0;
      List<TicketPurchaseInfoViewModel> ticketList = new List<TicketPurchaseInfoViewModel>();
      foreach (var lTicket in lOrder.LockTickets)
      {
        var tq = tqdRepo.GetByID(lTicket.TicketQuantityDetailId);
        if (tq == null)
          throw new Exception(String.Format("Ticket_Quantity_Detail with Id = {0} not found for LockId = {1}", lTicket.TicketQuantityDetailId, lockId));

        DateTime ticketDate;
        if (!DateTime.TryParse(tq.TQD_StartDate + " " + tq.TQD_StartTime, out ticketDate))
          ticketDate = DateTime.MinValue;
        List<AttendeeViewModel> attendees = Enumerable.Repeat(new AttendeeViewModel(), lTicket.Quantity).ToList();

        var ticket = new TicketPurchaseInfoViewModel()
        {
          LockTicketId = lTicket.LockTicketId,
          TicketTypeId = tq.Ticket.TicketTypeID ?? 0,
          Name = tq.Ticket.T_name,
          VenueName = ev.AddressStatus == "Online" ? "Online" : tq.Address == null ? "Unknown" : tq.Address.VenueName,
          DateString = ticketDate == DateTime.MinValue ? "" : ticketDate.ToString("f"),
          Price = lTicket.Price - lTicket.Discount,
          SourcePrice = lTicket.Price,
          Quantity = lTicket.Quantity,
          TotalFee = (lTicket.ECFeeType == 0 ? lTicket.ECFeeAmount + lTicket.ECFeePercent : 0) + lTicket.CustomerFee,
          TotalPrice = lTicket.Quantity * (lTicket.Price - lTicket.Discount +
                      (lTicket.ECFeeType == 0 ? lTicket.ECFeeAmount + lTicket.ECFeePercent : 0) + lTicket.CustomerFee),
          Attendees = attendees
        };
        if (ticket.TicketTypeId == 1)
        {
          ticket.Price = 0;
          ticket.SourcePrice = 0;
          ticket.TotalFee = 0;
          ticket.TotalPrice = 0;
        }
        else if (ticket.TicketTypeId == 3)
        {
          ticket.Price = lTicket.Donate;
          ticket.SourcePrice = ticket.Price;
          ticket.TotalFee = 0;
          ticket.TotalPrice = ticket.Price;
        }
        res.TotalAmount += ticket.TotalPrice;
        ticketList.Add(ticket);
      }
      res.Tickets = ticketList;

      List<VariableChargeGroup> vcGroupList = new List<VariableChargeGroup>();
      VariableChargeGroup cGroup = new VariableChargeGroup()
        {
          GroupType = ev.Ticket_variabletype == "O" ? 0 : 1,
          Title = ev.Ticket_variabledesc
        };
      IRepository<Event_VariableDesc> varRepo = new GenericRepository<Event_VariableDesc>(_factory.ContextFactory);
      List<VariableChargePurchaseInfoViewModel> vcList = new List<VariableChargePurchaseInfoViewModel>();
      foreach (var vcharge in varRepo.Get(v => v.Event_Id == res.EventId))
      {
        vcList.Add(new VariableChargePurchaseInfoViewModel()
        {
          Checked = (cGroup.GroupType == 1) && !vcList.Any(),
          Price = vcharge.Price ?? 0,
          VariableDesc = vcharge.VariableDesc,
          VariableId = vcharge.Variable_Id
        });
      }
      cGroup.VariableCharges = vcList;
      vcGroupList.Add(cGroup);
      res.PurchaseInfo.VariableChargeGroups = vcGroupList;

      IRepository<GeoState> stRepo = new GenericRepository<GeoState>(_factory.ContextFactory);
      res.PurchaseInfo.ShippingSame = true;
      if (!String.IsNullOrEmpty(userId))
      {
        IRepository<EventCombo.Models.Profile> pRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
        var profile = pRepo.Get(p => p.UserID == userId).FirstOrDefault();
        if (profile != null)
        {
          res.PurchaseInfo.FirstName = profile.FirstName;
          res.PurchaseInfo.LastName = profile.LastName;
          res.PurchaseInfo.Email = profile.Email;
          res.PurchaseInfo.BillingAddress.CountryId = profile.CountryID;
          IRepository<Country> cRepo = new GenericRepository<Country>(_factory.ContextFactory);
          var country = cRepo.GetByID(profile.CountryID);
          res.PurchaseInfo.BillingAddress.Country = country == null ? "" : country.Country1;
          var state = stRepo.Get(s => s.StateName.ToLower() == profile.State.ToLower()).FirstOrDefault();
          res.PurchaseInfo.BillingAddress.StateId = state == null ? 0 : state.StateId;
          res.PurchaseInfo.BillingAddress.State = state == null ? "" : state.StateName;
          res.PurchaseInfo.BillingAddress.City = profile.City;
          res.PurchaseInfo.BillingAddress.Address1 = profile.StreetAddressLine1;
          res.PurchaseInfo.BillingAddress.Address2 = profile.StreetAddressLine2;
          res.PurchaseInfo.BillingAddress.ZipCode = profile.Zip;
          res.PurchaseInfo.BillingAddress.PhoneNumber = profile.MainPhone;
        }
      }

      List<StateViewModel> stList = new List<StateViewModel>();
      foreach (var stDB in stRepo.Get())
        stList.Add(_mapper.Map<StateViewModel>(stDB));
      res.StateList = stList;

      return res;
    }

    private TimeZoneInfo GetTimeZoneInfo(string strId)
    {
      int tzId;
      if (Int32.TryParse(strId, out tzId))
        return GetTimeZoneInfo(tzId);
      else
        return null;
    }

    private TimeZoneInfo GetTimeZoneInfo(int tzId)
    {
      IRepository<TimeZoneDetail> tzRepo = new GenericRepository<TimeZoneDetail>(_factory.ContextFactory);
      TimeZoneDetail tz = tzRepo.GetByID(tzId);
      if (tz != null)
        return TimeZoneInfo.FindSystemTimeZoneById(tz.TimeZone);
      else
        return null;
    }

    private DateTime ConvertTimeFromUtc(DateTime time, TimeZoneInfo tz)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(time, DateTimeKind.Unspecified), tz);
    }

    private string GetEventDatesInfo(Event ev)
    {
      if (ev == null)
        throw new ArgumentNullException("ev");

      string result;
      string tzStr = "";

      TimeZoneInfo tz = GetTimeZoneInfo(ev.TimeZone);
      if (tz == null)
        tz = TimeZoneInfo.Utc;
      if ((ev.DisplayTimeZone == "Y") && (tz != null))
        tzStr = " (" + tz.DisplayName + ")";

      var multiDate = ev.MultipleEvents.FirstOrDefault();
      var singleDate = ev.EventVenues.SingleOrDefault();
      var StartDateTime = (multiDate == null ? singleDate.E_Startdate : multiDate.M_Startfrom) ?? DateTime.MinValue;
      var EndDateTime = (multiDate == null ? singleDate.E_Enddate : multiDate.M_StartTo) ?? DateTime.MinValue;
      if ((StartDateTime > DateTime.MinValue) && (tz != null))
        StartDateTime = ConvertTimeFromUtc(StartDateTime, tz);
      if ((EndDateTime > DateTime.MinValue) && (tz != null))
        EndDateTime = ConvertTimeFromUtc(EndDateTime, tz);
      if (StartDateTime == DateTime.MinValue)
        StartDateTime = EndDateTime;
      result = StartDateTime.ToString("f");
      if (EndDateTime != StartDateTime)
        result = result + " to " + EndDateTime.ToString("f");
      if (multiDate != null)
      {
        var Frequency = multiDate == null ? ScheduleFrequency.Single : (ScheduleFrequency)Enum.Parse(typeof(ScheduleFrequency), multiDate.Frequency, true);
        result = Frequency.ToString() + " " + result;
        if (Frequency == ScheduleFrequency.Weekly)
          result = result + " (" + multiDate.WeeklyDay + ")";
      }
      return result + tzStr;
    }

    public PurchaseResult SavePurchaseInfo(EventPurchaseInfoViewModel model, string userId, string ip)
    {
      PurchaseResult pres = new PurchaseResult() { Success = false, Message = "", OrderId = "", PayPalId = "", Url = "" };
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
          IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
          IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);
          IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
          IRepository<TicketBearer> tbRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
          IRepository<TicketAttendee> taRepo = new GenericRepository<TicketAttendee>(_factory.ContextFactory);
          // Create Order
          var lGuid = new Guid(model.PurchaseInfo.LockId);
          var lOrder = loRepo.GetByID(lGuid);
          if ((lOrder == null) || (lOrder.IP != ip))
          {
            pres.Message = "Order not found. Please try again";
            return pres;
          }
          Order_Detail_T ord = new Order_Detail_T()
          {
            IsManualOrder = false,
            O_User_Id = userId,
            O_First_Name = model.PurchaseInfo.FirstName,
            O_Last_Name = model.PurchaseInfo.LastName,
            O_Email = model.PurchaseInfo.Email,
            O_OrderDateTime = DateTime.UtcNow,
            OrderStateId = 1,
            O_PromoCodeId = 0
          };
          oRepo.Insert(ord);
          uow.Context.SaveChanges();
          oRepo.Reload(ord);

          // Process tickets
          Ticket_Purchased_Detail tpd;
          Ticket_Quantity_Detail tqd;
          TicketBearer tb;
          List<TicketBearer> tbList = new List<TicketBearer>();
          TicketAttendee ta;
          long quantity;
          decimal totalAmount = 0;
          foreach (var lTicket in lOrder.LockTickets)
          {
            tqd = tqdRepo.GetByID(lTicket.TicketQuantityDetailId);
            tpd = new Ticket_Purchased_Detail()
            {
              TPD_Event_Id = lOrder.EventId,
              TPD_Order_Id = ord.O_Order_Id,
              TPD_TQD_Id = lTicket.TicketQuantityDetailId,
              TPD_User_Id = userId,
              TPD_Purchased_Qty = lTicket.Quantity,
              TPD_GUID = model.PurchaseInfo.LockId,
              TPD_Amount = lTicket.Quantity * CalcTicketAmount(tqd.Ticket.TicketTypeID ?? 1, lTicket.ECFeeType, lTicket.Price, lTicket.ECFeePercent, lTicket.ECFeeAmount, lTicket.CustomerFee, lTicket.Discount),
              TPD_Donate = lTicket.Donate,
              Customer_Fee = lTicket.CustomerFee,
              TPD_EC_Fee = lTicket.ECFeeAmount + lTicket.ECFeePercent,
              TicketDiscount = lTicket.Discount,
              TicketECFee = lTicket.ECFeePercent,
              TicketMerchantFee = lTicket.ECFeeAmount,
              TicketPrice = lTicket.Price,
              TicketFeeType = (byte)lTicket.ECFeeType
            };
            tpdRepo.Insert(tpd);
            tqd.TQD_Remaining_Quantity -= tpd.TPD_Purchased_Qty;
            uow.Context.SaveChanges();
            totalAmount += (tpd.TPD_Amount ?? 0) + (tpd.TPD_Donate ?? 0);
            quantity = 0;
            var cTicket = model.Tickets.Where(t => t.LockTicketId == lTicket.LockTicketId).FirstOrDefault();
            if (cTicket != null)
              foreach (var att in cTicket.Attendees)
              {
                if (!String.IsNullOrEmpty(att.Email))
                {
                  tb = tbList.Where(b => (b.Email == att.Email) && (b.Name == att.Name) && (b.PhoneNumber == att.PhoneNumber)).FirstOrDefault();
                  if (tb == null)
                  {
                    tb = new TicketBearer()
                    {
                      Email = att.Email,
                      Guid = model.PurchaseInfo.LockId,
                      Name = att.Name ?? "",
                      OrderId = ord.O_Order_Id,
                      PhoneNumber = att.PhoneNumber ?? "",
                      TicketbearerId = 0,
                      UserId = userId
                    };
                    tbList.Add(tb);
                  }
                  ta = tb.TicketAttendees.Where(t => t.PurchasedTicketId == tpd.TPD_Id).FirstOrDefault();
                  if (ta == null)
                  {
                    ta = new TicketAttendee() { PurchasedTicketId = tpd.TPD_Id, Quantity = 1 };
                    tb.TicketAttendees.Add(ta);
                  }
                  else
                    ta.Quantity++;
                  quantity++;
                }
              }
            if ((tpd.TPD_Purchased_Qty ?? 0) > quantity) // need to add Buyer to attendee
            {
              tb = tbList.Where(b => (b.Email == ord.O_Email) && (b.Name == (ord.O_First_Name + ' ' + ord.O_Last_Name))).FirstOrDefault();
              if (tb == null)
              {
                tb = new TicketBearer()
                {
                  Email = ord.O_Email,
                  Guid = model.PurchaseInfo.LockId,
                  Name = ord.O_First_Name + ' ' + ord.O_Last_Name,
                  OrderId = ord.O_Order_Id,
                  PhoneNumber = model.PurchaseInfo.BillingAddress.PhoneNumber ?? "",
                  TicketbearerId = 0,
                  UserId = userId
                };
                tbList.Add(tb);
              }
              tb.TicketAttendees.Add(new TicketAttendee() { PurchasedTicketId = tpd.TPD_Id, Quantity = (tpd.TPD_Purchased_Qty ?? 0) - quantity });
            }
          }
          foreach (var tbearer in tbList)
            tbRepo.Insert(tbearer);

          // Variable charges calculation
          decimal vcAmount = 0;
          List<string> vcList = new List<string>();
          foreach (var vg in model.PurchaseInfo.VariableChargeGroups)
          {
            if (vg.GroupType == 1)
            {
              var vc = vg.VariableCharges.Where(c => c.Checked).FirstOrDefault();
              if (vc == null)
                vc = vg.VariableCharges.FirstOrDefault();
              if (vc != null)
              {
                vcAmount += vc.Price;
                vcList.Add(vc.VariableId.ToString());
              }
            }
            else
            {
              vcAmount += vg.VariableCharges.Where(c => c.Checked).Sum(c1 => c1.Price);
              vcList.AddRange(vg.VariableCharges.Where(c => c.Checked).Select(c1 => c1.VariableId.ToString()));
            }
          }

          // Order summary
          ord.O_OrderAmount = totalAmount;
          ord.O_TotalAmount = totalAmount + vcAmount;
          ord.O_VariableAmount = vcAmount;
          ord.O_VariableId = String.Join(",", vcList);

          uow.Context.SaveChanges();
          if (model.PurchaseInfo.Method == PaymentMethod.Card)
          {
            // Save billing address
            IRepository<BillingAddress> baRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
            EncryptDecrypt EDcode = new EncryptDecrypt();
            BillingAddress ba = new BillingAddress()
            {
              OrderId = ord.O_Order_Id,
              UserId = userId,
              Guid = model.PurchaseInfo.LockId,
              CardId = EDcode.EncryptText("XXXXXXXXXXXX" + model.PurchaseInfo.CardNumber.Substring(model.PurchaseInfo.CardNumber.Length - 4)),
              card_type = EDcode.EncryptText(model.PurchaseInfo.CardType),
              Cvv = EDcode.EncryptText("Unknown"),
              ExpirationDate = EDcode.EncryptText("Unknown"),
              PaymentType = "C",
              Fname = model.PurchaseInfo.FirstName,
              Lname = model.PurchaseInfo.LastName,
              Zip = model.PurchaseInfo.BillingAddress.ZipCode,
              Country = model.PurchaseInfo.BillingAddress.CountryId.ToString(),
              State = model.PurchaseInfo.BillingAddress.State,
              City = model.PurchaseInfo.BillingAddress.City,
              Address1 = model.PurchaseInfo.BillingAddress.Address1,
              Address2 = model.PurchaseInfo.BillingAddress.Address2,
              Phone_Number = model.PurchaseInfo.BillingAddress.PhoneNumber
            };
            baRepo.Insert(ba);

            // Save shipping address
            if (!model.PurchaseInfo.ShippingSame)
            {
              IRepository<EventCombo.Models.ShippingAddress> saRepo = new GenericRepository<EventCombo.Models.ShippingAddress>(_factory.ContextFactory);
              EventCombo.Models.ShippingAddress sa = new EventCombo.Models.ShippingAddress()
              {
                OrderId = ord.O_Order_Id,
                UserId = userId,
                Guid = model.PurchaseInfo.LockId,
                Fname = model.PurchaseInfo.FirstName,
                Lname = model.PurchaseInfo.LastName,
                Zip = model.PurchaseInfo.ShippingAddress.ZipCode,
                Country = model.PurchaseInfo.ShippingAddress.CountryId.ToString(),
                State = model.PurchaseInfo.ShippingAddress.State,
                City = model.PurchaseInfo.ShippingAddress.City,
                Address1 = model.PurchaseInfo.ShippingAddress.Address1,
                Address2 = model.PurchaseInfo.ShippingAddress.Address2,
                Phone_Number = model.PurchaseInfo.ShippingAddress.PhoneNumber
              };
              saRepo.Insert(sa);
            }
            uow.Context.SaveChanges();
          }
          // Payment processing
          StringBuilder bilder = new StringBuilder();
          foreach (var t in lOrder.LockTickets)
          {
            tqd = tqdRepo.GetByID(t.TicketQuantityDetailId);
            bilder.AppendFormat("{0}{1} x {2}", bilder.Length > 0 ? ", " : "", tqd.Ticket.T_name, t.Quantity);
          }
          string desc = bilder.Length > 0 ? "Purchased tickets: " + bilder.ToString() : "";
          if (model.PurchaseInfo.Method == PaymentMethod.Card)
          {
            cardtransaction response;
            try
            {
              response = PayByCreditCard(model.PurchaseInfo, ord.O_TotalAmount ?? 0, ord.O_Order_Id, desc);
            }
            catch (Exception ex)
            {
              throw new Exception(String.Format("Error during processing payment by credit card {0}", "XXXXXXXXXXXX", model.PurchaseInfo.CardNumber.Substring(model.PurchaseInfo.CardNumber.Length - 4)), ex);
            }
            if (response.Success)
            {
              ord.O_Card_TransHash = response.Transactionhash;
              ord.O_Card_TransId = response.TransactionId;
              ord.PaymentTypeId = 8; // Paid by credit card
              uow.Context.SaveChanges();
            }
            else
            {
              pres.Message = "Payment has not been committed. Please, check your payment information";
              throw new Exception(String.Format("Payment for card {0} has not been committed. {1}", model.PurchaseInfo.CardNumber.Substring(model.PurchaseInfo.CardNumber.Length - 4), response.message));
            }
          }
          else
          {
            ord.OrderStateId = 4; // Pending for payment completion
            Tuple<string, string> paypalRes;
            try
            {
              paypalRes = StartPayPalPayment(model.PurchaseInfo, ord.O_TotalAmount ?? 0, ord.O_Order_Id, desc);
              if (paypalRes != null)
              {
                pres.Success = true;
                pres.Url = paypalRes.Item1;
                pres.PayPalId = paypalRes.Item2;
              }
              else
              {
                pres.Message = "Error during creation of PayPal payment.";
                throw new Exception("");
              }
            }
            catch (Exception ex)
            {
              throw new Exception("Error during PayPal payment", ex);
            }
            uow.Context.SaveChanges();
          }
          uow.Commit();
          pres.Success = true;
          pres.OrderId = ord.O_Order_Id;
          
        }
        catch (Exception ex)
        {
          uow.Rollback();
          if (String.IsNullOrEmpty(pres.Message))
            pres.Message = "Error during processing of purchasing. Please try again later";
          _logger.Fatal(ex, String.Format("Error during processing of purchasing info for LockId = {0}, EventId = {1}, Payment method = {2}.", model.PurchaseInfo.LockId, model.EventId, model.PurchaseInfo.Method));
        }

      if (pres.Success)
        DeleteTicketLock(model.PurchaseInfo.LockId, ip);

      return pres;
    }

    public static decimal CalcTicketAmount(long ticketTypeId, int feeTypeId, decimal Price, decimal ECFee, decimal MerchantFee, decimal CustomerFee, decimal Discount)
    {
      decimal res = 0;
      if (ticketTypeId == 2)
        res = Price + CustomerFee - Discount + (feeTypeId == 0 ? ECFee + MerchantFee : 0);
      else if (ticketTypeId == 3)
        res = Price;
      return res;
    }

    public static decimal CalcTicketFee(long ticketTypeId, decimal ECFee, decimal MerchantFee, decimal CustomerFee)
    {
      decimal res = 0;
      if (ticketTypeId == 2)
        res = ECFee + MerchantFee + CustomerFee;
      else if (ticketTypeId == 3)
        res = MerchantFee + CustomerFee;
      return res;
    }

    private cardtransaction PayByCreditCard(PurchasingInfoViewModel pmodel, decimal pAmount, string orderId, string description)
    {
      string ApiLoginID = ConfigurationManager.AppSettings.Get("AuthorzieNetApiLoginID");
      string ApiTransactionKey = ConfigurationManager.AppSettings.Get("AuthorzieNetApiTransactionKey");
      cardtransaction carddet = new cardtransaction() { Success = false, message = "", Transactionhash = "", TransactionId = "" };

      string strEnv = ConfigurationManager.AppSettings.Get("AuthorzieNetEnvironment");
      if (strEnv.ToLower() == "production")
        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
      else
        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

      // define the merchant information (authentication / transaction id)
      ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
      {
        name = ApiLoginID,
        ItemElementName = ItemChoiceType.transactionKey,
        Item = ApiTransactionKey,
      };

      var creditCard = new creditCardType
      {
        cardNumber = pmodel.CardNumber,
        expirationDate = pmodel.ExpirationDate,
        cardCode = pmodel.SecurityCode
      };

      //standard api call to retrieve response
      var paymentType = new paymentType { Item = creditCard };
      orderType objot = new orderType();
      objot.invoiceNumber = orderId;
      objot.description = description;
      nameAndAddressType objShiping = new nameAndAddressType();
      customerAddressType objCAddType = new customerAddressType();

      objCAddType.address = pmodel.BillingAddress.Address1 + " " + pmodel.BillingAddress.Address2;
      objCAddType.city = pmodel.BillingAddress.City;
      objCAddType.country = pmodel.BillingAddress.Country;
      objCAddType.firstName = pmodel.FirstName;
      objCAddType.lastName = pmodel.LastName;
      objCAddType.phoneNumber = pmodel.BillingAddress.PhoneNumber;
      objCAddType.state = pmodel.BillingAddress.State;
      objCAddType.zip = pmodel.BillingAddress.ZipCode;

      if (pmodel.ShippingSame)
      {
        objShiping.address = objCAddType.address;
        objShiping.city = objCAddType.city;
        objShiping.country = objCAddType.country;
        objShiping.firstName = objCAddType.firstName;
        objShiping.lastName = objCAddType.lastName;
        objShiping.state = objCAddType.state;
        objShiping.zip = objCAddType.zip;
      }
      else
      {
        objShiping.address = pmodel.ShippingAddress.Address1 + " " + pmodel.ShippingAddress.Address2;
        objShiping.city = pmodel.ShippingAddress.City;
        objShiping.country = pmodel.ShippingAddress.Country;
        objShiping.firstName = pmodel.FirstName;
        objShiping.lastName = pmodel.LastName;
        objShiping.state = pmodel.ShippingAddress.State;
        objShiping.zip = pmodel.ShippingAddress.ZipCode;
      }

      customerDataType objCdt = new customerDataType();
      objCdt.id = "";
      objCdt.email = pmodel.Email;

      var transactionRequest = new transactionRequestType
      {
        transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
        amount = pAmount,
        payment = paymentType,
        order = objot,
        billTo = objCAddType,
        shipTo = objShiping,
        customer = objCdt
      };

      var request = new createTransactionRequest { transactionRequest = transactionRequest };

      // instantiate the contoller that will call the service
      var controller = new createTransactionController(request);
      controller.Execute();

      // get the response from the service (errors contained if any)
      var response = controller.GetApiResponse();

      if (response.messages.resultCode == messageTypeEnum.Ok)
      {
        carddet.Success = true;
        if (response.transactionResponse != null)
        {
          var uerfileds = response.transactionResponse.userFields;
          carddet.Transactionhash = response.transactionResponse.transHash;
          carddet.TransactionId = response.transactionResponse.transId;
        }
      }
      else
      {
        carddet.Success = false;
        if (response.transactionResponse != null)
        {
          StringBuilder b = new StringBuilder();
          foreach (var err in response.transactionResponse.errors)
            b.AppendLine(String.Format("Code: {0}, Message: {1}", err.errorCode, err.errorText));
          carddet.message = b.ToString();
        }
      }

      return carddet;
    }

    private Tuple<string, string> StartPayPalPayment(PurchasingInfoViewModel pmodel, decimal pAmount, string orderId, string description)
    {
      var itemList = new ItemList()
      {
        items = new List<Item>() 
        {
          new Item()
          {
            name = "Tickets",
            currency = "USD",
            price = pAmount.ToString(),
            quantity = "1",
            sku = "sku"
          }
        }
      };

      var payer = new Payer() { payment_method = "paypal" };

      //var guid = Convert.ToString((new Random()).Next(100000));
      var redirUrls = new RedirectUrls()
      {
        cancel_url = PayPalSettings.CancelUrl,
        return_url = PayPalSettings.ReturnUrl + "?orderId=" + orderId
      };

      var details = new Details() { tax = "0", shipping = "0", subtotal = pAmount.ToString() };
      var amount = new Amount() { currency = "USD", total = pAmount.ToString(), details = details };

      var transactionList = new List<Transaction>();
      transactionList.Add(new Transaction()
      {
        description = description,
        invoice_number = orderId,
        amount = amount,
        item_list = itemList
      });

      var payment = new Payment() { intent = "sale", payer = payer, transactions = transactionList, redirect_urls = redirUrls };

      APIContext apiContext = GetAPIContext();
      var createdPayment = payment.Create(apiContext);
      var lnk = createdPayment.links.Where(l => l.rel.ToLower().Trim().Equals("approval_url")).FirstOrDefault();
      if (lnk != null)
        return new Tuple<string,string>(lnk.href, createdPayment.id);
      else
        return null;
    }

    private APIContext GetAPIContext()
    {
      var cfg = PayPal.Api.ConfigManager.Instance.GetProperties();
      string accessToken = new OAuthTokenCredential(cfg["clientId"], cfg["clientSecret"], cfg).GetAccessToken();
      APIContext apiContext = new APIContext(accessToken);
      apiContext.Config = cfg;
      return apiContext;
    }

    public void CompletePayPalPayment(string orderId, string paymentId, string tokenId, string payerId)
    {
      IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var order = oRepo.Get(o => o.O_Order_Id == orderId).FirstOrDefault();
      if (order == null)
        throw new Exception(String.Format("Order {0} for saving information about PayPal payment {1} (token = {2}, payer = {3}) not found", orderId, paymentId, tokenId, payerId));
      order.OrderStateId = 1;
      order.O_PayPal_PayerId = payerId;
      order.O_PayPal_TokenId = tokenId;
      order.O_PayPal_TrancId = paymentId;
      _factory.ContextFactory.GetContext().SaveChanges();
    }

  }

}