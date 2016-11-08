using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.DAL;
using EventCombo.Models;
using AutoMapper;
using NLog;
using System.Data.SqlClient;
using System.Data;
using Hangfire;
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

    public EventPurchaseInfoViewModel GetEventPurchaseInfo(string lockId, string ip)
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
      foreach(var lTicket in lOrder.LockTickets)
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
        if(ticket.TicketTypeId == 1)
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
      if (!String.IsNullOrEmpty(lOrder.UserId))
      {
        IRepository<EventCombo.Models.Profile> pRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
        var profile = pRepo.Get(p => p.UserID == lOrder.UserId).FirstOrDefault();
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

    public string SavePurchaseInfo(PurchasingInfoViewModel model)
    {
      return "";
    }
  }

}