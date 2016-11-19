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
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace EventCombo.Service
{
  public class PurchasingService : IPurchasingService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private ILogger _logger;
    private IECImageService _iService;
    private IDBAccessService _dbService;
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
      _dbService = new DBAccessService(_factory, _mapper);
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
          lTicket.Price = Math.Round(tDB.Ticket.Price ?? 0, 2);
          lTicket.CustomerFee = Math.Round(tDB.Ticket.Customer_Fee ?? 0, 2);
          lTicket.ECFeeAmount = tDB.Ticket.TicketTypeID == 1 ? 0 : Math.Round(tDB.Ticket.T_EcAmount ?? 0, 2);
          lTicket.ECFeePercent = Math.Round((tDB.Ticket.Price ?? 0) * (tDB.Ticket.T_Ecpercent ?? 0) / 100, 2);
          lTicket.Discount = Math.Round(tDB.Ticket.T_Discount ?? 0, 2);
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
      res.ShowShippingAddress = (ev.Ticket_DAdress ?? "").ToUpper() == "Y";
      res.EventUrl = EventService.GetEventUrl(res.EventId, res.EventTitle, new UrlHelper(HttpContext.Current.Request.RequestContext));
      var address = ev.Addresses.FirstOrDefault();
      if (address != null)
      {
        res.Address = address.ConsolidateAddress;
        res.VenueName = address.VenueName;
      }
      else if (ev.AddressStatus == "Online")
        res.Address = "Online";

      var dates = GetEventDatesInfo(ev);
      res.StartDate = dates.Summary;

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
          TicketId = tq.TQD_Ticket_Id ?? 0,
          Name = tq.Ticket.T_name,
          VenueName = ev.AddressStatus == "Online" ? "Online" : tq.Address == null ? "Unknown" : tq.Address.VenueName,
          DateString = ticketDate == DateTime.MinValue ? "" : ticketDate.ToString("f"),
          Price = lTicket.Price - lTicket.Discount,
          SourcePrice = lTicket.Price,
          Quantity = lTicket.Quantity,
          TotalFee = (lTicket.ECFeeType == 0 ? lTicket.ECFeeAmount + lTicket.ECFeePercent : 0) + lTicket.CustomerFee,
          TotalPrice = lTicket.Quantity * (lTicket.Price - lTicket.Discount +
                      (lTicket.ECFeeType == 0 ? lTicket.ECFeeAmount + lTicket.ECFeePercent : 0) + lTicket.CustomerFee),
          Sort = tq.Ticket.T_order ?? 0,
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
      res.Tickets = ticketList.OrderBy(t => t.Sort);
      res.TotalAmount = Math.Round(res.TotalAmount, 2);

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
          Price = Math.Round(vcharge.Price ?? 0, 2),
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

    private EventDatesInfo GetEventDatesInfo(Event ev)
    {
      if (ev == null)
        throw new ArgumentNullException("ev");

      EventDatesInfo result = new EventDatesInfo();
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
      result.StartDate = StartDateTime;
      result.Summary = StartDateTime.ToString("f");
      result.EndDate = EndDateTime;
      if (EndDateTime != StartDateTime)
        result.Summary += " to " + EndDateTime.ToString("f");
      if (multiDate != null)
      {
        var Frequency = multiDate == null ? ScheduleFrequency.Single : (ScheduleFrequency)Enum.Parse(typeof(ScheduleFrequency), multiDate.Frequency, true);
        result.Summary = Frequency.ToString() + " " + result;
        if (Frequency == ScheduleFrequency.Weekly)
          result.Summary += " (" + multiDate.WeeklyDay + ")";
      }
      result.Summary += tzStr;
      return result;
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
          IRepository<Promo_Code> promoRepo = new GenericRepository<Promo_Code>(_factory.ContextFactory);
          // Create Order
          var lGuid = new Guid(model.PurchaseInfo.LockId);
          var lOrder = loRepo.GetByID(lGuid);
          if ((lOrder == null) || (lOrder.IP != ip))
          {
            pres.Message = "Order not found. Please try again";
            pres.Url = EventService.GetEventUrl(model.EventId, model.EventTitle, new UrlHelper(HttpContext.Current.Request.RequestContext));
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

          // Get promocode
          var promo = GetPromoCode(model.EventId, model.PurchaseInfo.PromoCode);
          decimal promoDiscount = 0;

          // Process tickets
          Ticket_Purchased_Detail tpd;
          Ticket_Quantity_Detail tqd;
          TicketBearer tb;
          List<TicketBearer> tbList = new List<TicketBearer>();
          TicketAttendee ta;
          PromoTicketsInfoViewModel promoticket;
          long quantity;
          decimal totalAmount = 0;
          foreach (var lTicket in lOrder.LockTickets)
          {
            tqd = tqdRepo.GetByID(lTicket.TicketQuantityDetailId);
            promoticket = (tqd.Ticket.TicketTypeID ?? 1) == 2 ? promo.Tickets.Where(t => t.TicketId == tqd.TQD_Ticket_Id).FirstOrDefault() : null;
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
              TicketFeeType = (byte)lTicket.ECFeeType, 
              TPD_PromoCodeAmount = promoticket == null ? 0 : lTicket.Quantity * Math.Round(promoticket.Amount + lTicket.Price * promoticket.Percents, 2),
              TPD_PromoCodeID = promo.Success ? promo.PromoCodeId : 0
            };
            tpdRepo.Insert(tpd);
            tqd.TQD_Remaining_Quantity -= tpd.TPD_Purchased_Qty;
            uow.Context.SaveChanges();
            promoDiscount += tpd.TPD_PromoCodeAmount ?? 0;
            totalAmount += (tpd.TPD_Amount ?? 0) + (tpd.TPD_Donate ?? 0);
            quantity = 0;
            var cTicket = model.Tickets.Where(t => t.LockTicketId == lTicket.LockTicketId).FirstOrDefault();
            if (cTicket != null)
            {
              //For future use in PayPal payment
              cTicket.Name = tqd.Ticket.T_name;
              cTicket.TotalPrice = (tpd.TPD_Amount ?? 0) + (tpd.TPD_Donate ?? 0);
              cTicket.Price = CalcTicketAmount(tqd.Ticket.TicketTypeID ?? 1, lTicket.ECFeeType, lTicket.Price, lTicket.ECFeePercent, lTicket.ECFeeAmount, lTicket.CustomerFee, lTicket.Discount) + (tpd.TPD_Donate ?? 0);
              //Save Attendees
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
            }
            if ((tpd.TPD_Purchased_Qty ?? 0) > quantity) // need to add Buyer to attendee
            {
              tb = tbList.Where(b => (b.Email == ord.O_Email) && (b.Name == (ord.O_First_Name + " " + ord.O_Last_Name))).FirstOrDefault();
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
                vcAmount += Math.Round(vc.Price, 2);
                vcList.Add(vc.VariableId.ToString());
              }
            }
            else
            {
              vcAmount += Math.Round(vg.VariableCharges.Where(c => c.Checked).Sum(c1 => c1.Price), 2);
              vcList.AddRange(vg.VariableCharges.Where(c => c.Checked).Select(c1 => c1.VariableId.ToString()));
            }
          }

          if (promo.Success)
          {
            promoDiscount += promo.Amount + Math.Round((totalAmount + vcAmount) * promo.Percents, 2);
            if (promoDiscount <= (totalAmount + vcAmount))
            {
              ord.O_PromoCodeId = promo.PromoCodeId;
              var p = promoRepo.GetByID(promo.PromoCodeId);
              int uses;
              if ((p != null) && Int32.TryParse(p.PC_Uses, out uses))
              {
                uses--;
                p.PC_Uses = uses.ToString();
                uow.Context.SaveChanges();
              }
            }
            else
              promoDiscount = 0;
          }
          // Order summary
          ord.O_OrderAmount = totalAmount;
          ord.O_TotalAmount = totalAmount + vcAmount - promoDiscount;
          ord.O_VariableAmount = vcAmount;
          ord.O_VariableId = String.Join(",", vcList);

          uow.Context.SaveChanges();

          GenerateTicketDetails(ord.O_Order_Id, uow);

          // Save shipping address if necessary
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

          // Payment processing
          StringBuilder bilder = new StringBuilder();
          foreach (var t in lOrder.LockTickets)
          {
            tqd = tqdRepo.GetByID(t.TicketQuantityDetailId);
            bilder.AppendFormat("{0}{1} x {2}", bilder.Length > 0 ? ", " : "", tqd.Ticket.T_name, t.Quantity);
          }
          string desc = bilder.Length > 0 ? "Purchased tickets: " + bilder.ToString() : "";
          if ((model.PurchaseInfo.Method == PaymentMethod.Card) && ((ord.O_TotalAmount ?? 0) > 0))
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

            uow.Context.SaveChanges();

            cardtransaction response;
            try
            {
              response = PayByCreditCard(model.PurchaseInfo, ord.O_TotalAmount ?? 0, ord.O_Order_Id, desc);
            }
            catch (Exception ex)
            {
              throw new Exception(String.Format("Error during processing of payment by credit card {0}", "XXXXXXXXXXXX", model.PurchaseInfo.CardNumber.Substring(model.PurchaseInfo.CardNumber.Length - 4)), ex);
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
              pres.Message = response.UserMessage;
              throw new Exception(String.Format("Payment for card {0} has not been committed. {1}", model.PurchaseInfo.CardNumber.Substring(model.PurchaseInfo.CardNumber.Length - 4), response.message));
            }
          }
          else if ((ord.O_TotalAmount ?? 0) > 0)
          {
            ord.OrderStateId = 4;  // Pending for payment completion
            ord.PaymentTypeId = 3; // Paid by PayPal
            Tuple<string, string> paypalRes;
            try
            {
              paypalRes = StartPayPalPayment(model.Tickets, ord.O_TotalAmount ?? 0, promoDiscount, ord.O_Order_Id, desc);
              if (paypalRes != null)
              {
                pres.Success = true;
                pres.Url = paypalRes.Item1;
                pres.PayPalId = paypalRes.Item2;
              }
              else
              {
                pres.Message = "Error during creation of PayPal payment.";
                throw new Exception("Unknown error during creation of PayPal payment.");
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
            pres.Message = "Error during processing. Please try again later";
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

      if (response.transactionResponse.responseCode == "1") // Approved
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
          StringBuilder ub = new StringBuilder();
          foreach (var err in response.transactionResponse.errors)
          {
            b.AppendLine(String.Format("Code: {0}, Message: {1}", err.errorCode, err.errorText));
            ub.AppendLine(err.errorText);
          }
          carddet.message = b.ToString();
          carddet.UserMessage = String.Join(", ", ub);
        }
      }

      return carddet;
    }

    private Tuple<string, string> StartPayPalPayment(IEnumerable<TicketPurchaseInfoViewModel> tickets, decimal pAmount, decimal promoDiscount, string orderId, string description)
    {
      var itemList = new ItemList() { items = new List<Item>() };
      int i = 1;
      decimal total = 0;
      foreach (var t in tickets)
      {
        Item item = new Item()
        {
          name = String.Format("{0} x {1}", t.Name, t.Quantity),
          currency = "USD",
          price = t.Price.ToString("N2"),
          quantity = t.Quantity.ToString(),
          sku = "Ticket#" + i++
        };
        total += t.Price * t.Quantity; 
        itemList.items.Add(item);
      }
      if (promoDiscount != 0)
      {
        Item promo = new Item()
        {
          name = "Promo Code",
          currency = "USD",
          price = (-promoDiscount).ToString("N2"),
          quantity = "1",
          sku = "Promo"
        };
        itemList.items.Add(promo);
        total -= promoDiscount;
      }
      if (pAmount != total)
      {
        Item remain = new Item()
        {
          name = "Variable charges",
          currency = "USD",
          price = (pAmount - total).ToString("N2"),
          quantity = "1",
          sku = "VarCharge"
        };
        itemList.items.Add(remain);
      }

      var payer = new Payer() { payment_method = "paypal" };
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
        return new Tuple<string, string>(lnk.href, createdPayment.id);
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
      try
      {
        APIContext apiContext = GetAPIContext();
        var payment = Payment.Get(apiContext, paymentId);
        if (payment != null)
        {
          IRepository<BillingAddress> baRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
          IRepository<EventCombo.Models.ShippingAddress> saRepo = new GenericRepository<EventCombo.Models.ShippingAddress>(_factory.ContextFactory);

          EventCombo.Models.ShippingAddress saDB = null;
          BillingAddress baDB = null;
          PayPal.Api.ShippingAddress sa = null;

          var tr = payment.transactions.FirstOrDefault();
          if (tr != null)
          {
            sa = tr.item_list.shipping_address;
            saDB = saRepo.Get(a => a.OrderId == orderId).FirstOrDefault();
            if ((sa != null) && (saDB == null))
            {
              saDB = new Models.ShippingAddress()
              {
                OrderId = orderId,
                Country = sa.country_code,
                City = sa.city,
                State = sa.state,
                Address1 = sa.line1,
                Address2 = sa.line2,
                Phone_Number = sa.phone,
                Zip = sa.postal_code,
                Fname = payment.payer.payer_info.first_name,
                Lname = payment.payer.payer_info.last_name
              };
              saRepo.Insert(saDB);
              _factory.ContextFactory.GetContext().SaveChanges();
            }
          }

          var ba = payment.payer.payer_info.billing_address;
          if (ba != null)
            baDB = new BillingAddress()
            {
              OrderId = orderId,
              Country = ba.country_code,
              City = ba.city,
              State = ba.state,
              Address1 = ba.line1,
              Address2 = ba.line2,
              Phone_Number = ba.phone,
              Zip = ba.postal_code,
              Fname = payment.payer.payer_info.first_name,
              Lname = payment.payer.payer_info.last_name,
              PaymentType = "P"
            };
          else if (sa != null)
            baDB = new BillingAddress()
            {
              OrderId = orderId,
              Country = sa.country_code,
              City = sa.city,
              State = sa.state,
              Address1 = sa.line1,
              Address2 = sa.line2,
              Phone_Number = sa.phone,
              Zip = sa.postal_code,
              Fname = payment.payer.payer_info.first_name,
              Lname = payment.payer.payer_info.last_name,
              PaymentType = "P"
            };
          else if (saDB != null)
            baDB = new BillingAddress()
            {
              OrderId = orderId,
              Country = saDB.Country,
              City = saDB.City,
              State = saDB.State,
              Address1 = saDB.Address1,
              Address2 = saDB.Address2,
              Phone_Number = saDB.Phone_Number,
              Zip = saDB.Zip,
              Fname = saDB.Fname,
              Lname = saDB.Lname,
              PaymentType = "P"
            };
          if (baDB != null)
          {
            baRepo.Insert(baDB);
            _factory.ContextFactory.GetContext().SaveChanges();
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex, String.Format("Exception during getting information from PayPal. Order ID: {0}, Payment ID: {1}, Payer ID: {2}", orderId, paymentId, payerId));
      }
    }

    public OrderConfirmationViewModel GetOrderConfirmationInfo(string orderId, string userId)
    {
      if (String.IsNullOrEmpty(orderId))
        throw new ArgumentNullException("orderId");

      IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var order = oRepo.Get(o => o.O_Order_Id == orderId).FirstOrDefault();
      if (order == null)
        throw new Exception(String.Format("Order #{0} not found.", orderId));
      if (order.O_User_Id != userId)
        throw new Exception(String.Format("Order #{0} information is not available for user {1}.", orderId, userId));

      OrderConfirmationViewModel res = new OrderConfirmationViewModel();

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var tpdTickets = tpdRepo.Get(filter: (t => t.TPD_Order_Id == orderId), orderBy: (q => q.OrderBy(t1 => t1.Ticket_Quantity_Detail.Ticket.T_order)));
      if ((tpdTickets == null) || (tpdTickets.Count() <= 0))
        throw new Exception("There is no tickets for order #" + orderId);

      var ev = tpdTickets.First().Event;
      if (ev == null)
        throw new Exception(String.Format("Event for order #{0} not found.", orderId));

      if ((ev.ECBackgroundId ?? 0) > 0)
      {
        var img = _iService.GetImageById(ev.ECBackgroundId ?? 0);
        if (img != null)
          res.BGImageUrl = img.ImagePath;
      }
      res.BGColor = ev.BackgroundColor;

      res.EventId = ev.EventID;
      res.EventTitle = ev.EventTitle;
      res.EventDescription = ev.EventDescription;
      res.Email = order.O_Email;
      res.OrderId = orderId;
      res.EventUrl = EventService.GetEventUrl(res.EventId, res.EventTitle, new UrlHelper(HttpContext.Current.Request.RequestContext));
      res.EventUrl = ResolveServerUrl(VirtualPathUtility.ToAbsolute(res.EventUrl), false);

      var org = ev.Event_Orgnizer_Detail.FirstOrDefault();
      if (org != null)
      {
        res.OrganizerId = org.OrganizerMaster_Id;
        res.OrganizerName = org.Organizer_Master.Orgnizer_Name;
      }

      var address = ev.Addresses.FirstOrDefault();
      if (address != null)
      {
        res.Address = address.ConsolidateAddress;
        res.VenueName = address.VenueName;
      }
      else if (ev.AddressStatus == "Online")
        res.Address = "Online";

      res.Dates = GetEventDatesInfo(ev);

      List<TicketPurchaseInfoViewModel> ticketList = new List<TicketPurchaseInfoViewModel>();
      foreach (var tpdTicket in tpdTickets)
      {
        DateTime ticketDate;
        if (!DateTime.TryParse(tpdTicket.Ticket_Quantity_Detail.TQD_StartDate + " " + tpdTicket.Ticket_Quantity_Detail.TQD_StartTime, out ticketDate))
          ticketDate = DateTime.MinValue;

        var ticket = new TicketPurchaseInfoViewModel()
        {
          TicketTypeId = tpdTicket.Ticket_Quantity_Detail.Ticket.TicketTypeID ?? 0,
          Name = tpdTicket.Ticket_Quantity_Detail.Ticket.T_name,
          VenueName = ev.AddressStatus == "Online" ? "Online" : tpdTicket.Ticket_Quantity_Detail.Address == null ? "Unknown" : tpdTicket.Ticket_Quantity_Detail.Address.VenueName,
          DateString = ticketDate == DateTime.MinValue ? "" : ticketDate.ToString("f"),
          Price = tpdTicket.TicketPrice - tpdTicket.TicketDiscount,
          SourcePrice = tpdTicket.TicketPrice,
          Quantity = tpdTicket.TPD_Purchased_Qty ?? 0,
          TotalFee = (tpdTicket.TicketFeeType == 0 ? tpdTicket.TPD_EC_Fee ?? 0 : 0) + tpdTicket.Customer_Fee,
          TotalPrice = (tpdTicket.TPD_Amount ?? 0) + (tpdTicket.TPD_Donate ?? 0)
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
          ticket.Price = (tpdTicket.TPD_Donate ?? 0);
          ticket.SourcePrice = ticket.Price;
          ticket.TotalFee = 0;
          ticket.TotalPrice = ticket.Price;
        }
        res.TotalAmount += ticket.TotalPrice;
        ticketList.Add(ticket);
      }
      res.Tickets = ticketList;

      return res;
    }

    private string ResolveServerUrl(string serverUrl, bool forceHttps)
    {
      if (serverUrl.IndexOf("://") > -1)
        return serverUrl;

      string newUrl = serverUrl;
      Uri originalUri = System.Web.HttpContext.Current.Request.Url;
      newUrl = (forceHttps ? "https" : originalUri.Scheme) +
          "://" + originalUri.Authority + newUrl;
      return newUrl;
    }

    public void GenerateTicketDetails(string orderId, IUnitOfWork uow)
    {
      IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      IRepository<TicketAttendee> taRepo = new GenericRepository<TicketAttendee>(_factory.ContextFactory);
      IRepository<TicketBearer> tbRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
      IRepository<TicketOrderDetail> todRepo = new GenericRepository<TicketOrderDetail>(_factory.ContextFactory);

      var order = oRepo.Get(o => o.O_Order_Id == orderId).FirstOrDefault();
      if (order == null)
        throw new ArgumentException(String.Format("Order #{0} not found.", orderId));

      var tpdList = tpdRepo.Get(t => t.TPD_Order_Id == orderId).ToList();
      foreach (var tpd in tpdList)
      {
        var q = taRepo.Get(ta => ta.PurchasedTicketId == tpd.TPD_Id).Sum(t => t.Quantity);
        if (q < tpd.TPD_Purchased_Qty)
        {
          var tb = tbRepo.Get(t => (t.OrderId == orderId) && (t.Email == order.O_Email) && (t.Name == (order.O_First_Name + " " + order.O_Last_Name))).FirstOrDefault();
          if (tb == null)
          {
            tb = new TicketBearer()
            {
              Email = order.O_Email,
              Guid = tpd.TPD_GUID,
              Name = order.O_First_Name + " " + order.O_Last_Name,
              OrderId = order.O_Order_Id,
              PhoneNumber = "",
              TicketbearerId = 0,
              UserId = order.O_User_Id
            };
            tbRepo.Insert(tb);
            uow.Context.SaveChanges();
          }
          var ta = tb.TicketAttendees.Where(a => a.PurchasedTicketId == tpd.TPD_Id).FirstOrDefault(); ;
          if (ta == null)
          {
            ta = new TicketAttendee()
            {
              PurchasedTicketId = tpd.TPD_Id,
              Quantity = (tpd.TPD_Purchased_Qty ?? 0) - q,
              TicketBearerId = tb.TicketbearerId
            };
            tb.TicketAttendees.Add(ta);
          }
          else
            ta.Quantity = (tpd.TPD_Purchased_Qty ?? 0) - q;
          uow.Context.SaveChanges();
        }

        var todList = todRepo.Get(tod => (tod.T_TQD_Id == tpd.TPD_TQD_Id) && (tod.T_Order_Id == orderId)).ToList();
        foreach (var ta in taRepo.Get(t => t.PurchasedTicketId == tpd.TPD_Id).ToList())
        {
          var tq = todList.Where(tod => tod.TicketAttendeeId == ta.TicketAttendeeId).Count();
          while (tq++ < ta.Quantity)
          {
            var tod = todList.Where(t => (t.TicketAttendeeId ?? 0) == 0).FirstOrDefault();
            if (tod == null)
            {
              string todId = GetRandomString(12);
              while (todRepo.GetByID(todId) != null)
                todId = GetRandomString(12);
              tod = new TicketOrderDetail()
              {
                T_Id = todId,
                T_Order_Id = orderId,
                T_TQD_Id = tpd.TPD_TQD_Id,
                T_Guid = tpd.TPD_GUID,
                TicketAttendeeId = ta.TicketAttendeeId
              };
              todRepo.Insert(tod);
            }
            else
              tod.TicketAttendeeId = ta.TicketAttendeeId;
            uow.Context.SaveChanges();
          }
        }
      }
    }

    private string GetRandomString(int length)
    {
      var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      var random = new Random();
      StringBuilder builder = new StringBuilder();
      char ch;
      for (int i = 0; i < length; i++)
      {
        ch = chars[random.Next(chars.Length)];
        builder.Append(ch);
      }
      return builder.ToString();
    }

    private DateTime CalcRelativeDateTime(DateTime date, string offset)
    {
      var vals = offset.Split(' ');
      long minutes = 0;
      if (vals.Count() > 0)
        minutes = Int32.Parse(vals[0]);
      minutes *= 24;
      if (vals.Count() > 2)
        minutes += Int32.Parse(vals[2]);
      minutes *= 60;
      if (vals.Count() > 4)
        minutes += Int32.Parse(vals[4]);
      return date.AddMinutes(-minutes);
    }

    public PromoCodeResponseViewModel GetPromoCode(long eventId, string promocode)
    {
      PromoCodeResponseViewModel res = new PromoCodeResponseViewModel() { Success = false };
      if (String.IsNullOrEmpty(promocode))
        return res;
      IRepository<Promo_Code> pRepo = new GenericRepository<Promo_Code>(_factory.ContextFactory);
      var promo = pRepo.Get(p => (p.PC_Eventid == eventId) && (p.PC_Code.ToLower() == promocode.ToLower())).FirstOrDefault();
      if (promo == null)
      {
        res.Message = "Promo Code not found";
        return res;
      }
      DateTime startDT;
      DateTime endDT;
      DateTime now = DateTime.UtcNow;
      var dates = _dbService.GetEventStartEndUTC(eventId);

      if (promo.PC_Startdatetype == "1")
        startDT = CalcRelativeDateTime(dates.Start, promo.PC_Start);
      else
        startDT = promo.P_Startdate ?? DateTime.MinValue;
      if (promo.Pc_Enddatetype == "1")
        endDT = CalcRelativeDateTime(dates.End, promo.PC_End);
      else
        endDT = promo.P_Enddate ?? DateTime.MinValue;
      if ((now > endDT) || (now < startDT) || (promo.PC_Uses == "0"))
      {
        res.Message = "Promo Code not available.";
        return res;
      }

      res.Success = true;
      res.EventId = eventId;
      res.PromoCode = promocode;
      res.PromoCodeId = promo.PC_id;
      if (!String.IsNullOrEmpty(promo.PC_Apply) && (promo.PC_Apply.ToUpper() != "A"))
      {
        var tickets = promo.PC_Apply.Split(',');
        foreach(var tstr in tickets)
        {
          long tid = 0;
          if (Int64.TryParse(tstr, out tid))
            res.Tickets.Add(new PromoTicketsInfoViewModel() { TicketId = tid, Amount = promo.PC_Amount ?? 0, Percents = (promo.PC_Percentage ?? 0) / 100 });
        }
      }
      if (res.Tickets.Count > 0)
      {
        res.Amount = 0;
        res.Percents = 0;
      }
      else
      {
        res.Amount = promo.PC_Amount ?? 0;
        res.Percents = (promo.PC_Percentage ?? 0) / 100;
      }

      return res;
    }

  }

}