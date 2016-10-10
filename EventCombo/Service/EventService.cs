using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.DAL;
using AutoMapper;
using System.Data.SqlClient;
using EventCombo.Utils;
using System.Web.Mvc;
using NLog;
using System.IO;
using System.Web.Hosting;
using System.Configuration;
using System.Net.Mail;
using System.Data;


namespace EventCombo.Service
{
  public class EventService : IEventService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private IECImageService _iservice;
    private ILogger _logger;
    private UrlHelper _urlHelper;

    public EventService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
      _iservice = new ECImageService(factory, mapper, new ECImageStorage(mapper));
      _logger = LogManager.GetCurrentClassLogger();
      try
      {
        _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
      }
      catch (Exception ex)
      {
        _urlHelper = null;
        _logger.Error(ex, "Error during creation of UrlHelper(HttpContext.Current.Request.RequestContext).", null);
      }

    }

    private void LoadEventDictionaries(EventViewModel ev)
    {
      IRepository<EventType> etRepo = new GenericRepository<EventType>(_factory.ContextFactory);
      IRepository<EventCategory> ecRepo = new GenericRepository<EventCategory>(_factory.ContextFactory);
      IRepository<Organizer_Master> orgRepo = new GenericRepository<Organizer_Master>(_factory.ContextFactory);
      IRepository<TimeZoneDetail> tzRepo = new GenericRepository<TimeZoneDetail>(_factory.ContextFactory);
      IRepository<Fee_Structure> feeRepo = new GenericRepository<Fee_Structure>(_factory.ContextFactory);

      ev.EventTypeList.Clear();
      foreach (var etype in etRepo.Get(orderBy: (query => query.OrderBy(et => et.EventType1))))
        ev.EventTypeList.Add(_mapper.Map<EventTypeViewModel>(etype));

      ev.EventCategoryList.Clear();
      foreach (var ecat in ecRepo.Get(orderBy: (query => query.OrderBy(ec => ec.EventCategory1))))
      {
        var ecatVM = _mapper.Map<EventCategoryViewModel>(ecat);
        foreach (var esub in ecat.EventSubCategories)
          ecatVM.SubCategories.Add(_mapper.Map<EventSubCategoryViewModel>(esub));
        ev.EventCategoryList.Add(ecatVM);
      }

      ev.OrganizerList.Clear();
      int i = 1;
      foreach (var orgId in orgRepo.Get(filter: (o => o.UserId == ev.UserID)).GroupBy(o => new { o.Organizer_Email, o.Orgnizer_Name }, (key, x) => x.Max(o => o.Orgnizer_Id)))
      {
        var org = orgRepo.Get(filter: (o => o.Orgnizer_Id == orgId)).FirstOrDefault();
        if (org != null)
        {
          OrganizerViewModel orgVM = _mapper.Map<OrganizerViewModel>(org);
          orgVM.InternalId = i++;
          orgVM.IncludeSocialLinks = !String.IsNullOrWhiteSpace(orgVM.Organizer_FBLink)
            || !String.IsNullOrWhiteSpace(orgVM.Organizer_Linkedin)
            || !String.IsNullOrWhiteSpace(orgVM.Organizer_Twitter);
          if ((org.ECImageId ?? 0) > 0)
            orgVM.Image = _iservice.GetImageById((org.ECImageId ?? 0));
          ev.OrganizerList.Add(orgVM);
        }
      }

      ev.TimeZones.Clear();
      foreach (var timezone in tzRepo.Get(orderBy: (query => query.OrderBy(tz => tz.Timezone_order))))
      {
        var tz = _mapper.Map<TimeZoneViewModel>(timezone);
        string sign = tz.TimeZoneName.Substring(5, 1);
        if ((sign == "+") || (sign == "-"))
        {
          int hours;
          int minutes;
          Int32.TryParse(tz.TimeZoneName.Substring(6, 2), out hours);
          Int32.TryParse(tz.TimeZoneName.Substring(9, 2), out minutes);
          tz.Offset = hours * 60 + minutes;
        }
        else
          tz.Offset = 0;
        ev.TimeZones.Add(tz);
      }

      var fee = feeRepo.Get(orderBy: (query => query.OrderByDescending(f => f.FS_Id))).FirstOrDefault();
      if (fee != null)
        _mapper.Map(fee, ev.FeeStruct);

      ev.DateInfo.TimeList.Clear();
      for (int min = 0; min < 24 * 60; min += 30)
      {
        PredefinedTimeViewModel time = new PredefinedTimeViewModel()
        {
          Minutes = min,
          TimeString = ((min / 60 % 12) == 0 ? "12" : (min / 60 % 12).ToString()) + ":" + (min % 60 == 0 ? "00" : "30") + (min / 60 < 12 ? " AM" : " PM")
        };
        ev.DateInfo.TimeList.Add(time);
      }
    }

    public EventViewModel CreateEvent(string userId)
    {
      EventViewModel ev = new EventViewModel() { UserID = userId };
      ev.DateInfo.IsNewDate = true;
      ev.DateInfo.Frequency = ScheduleFrequency.Single;
      ev.DateInfo.StartDateTime = DateTime.Today.AddDays(1).AddHours(18);
      ev.DateInfo.EndDateTime = ev.DateInfo.StartDateTime;
      ev.EventPrivacy = "Public";
      ev.DisplayEndTime = "Y";
      ev.DisplayStartTime = "Y";
      ev.DisplayTimeZone = "Y";
      ev.Private_ShareOnFB = "N";
      ev.Private_GuestOnly = "N";
      ev.PublishOnFB = "N";
      ev.Parent_EventID = 0;
      ev.AddressStatus = "Single";
      ev.LastLocationAddress = 0;
      ev.EnableFBDiscussion = "N";
      ev.Ticket_DAdress = "N";
      ev.Ticket_showremain = "N";
      ev.Ticket_showvariable = "N";
      ev.Ticket_variabletype = "O";
      ev.EventCancel = "N";
      LoadEventDictionaries(ev);
      if (ev.OrganizerList.Count > 0)
      {
        ev.OrganizerId = ev.OrganizerList[0].OrgnizerId;
        ev.InternalOrganizerId = ev.OrganizerList[0].InternalId;
      }
      return ev;
    }

    public bool ValidateEvent(EventViewModel ev)
    {
      ev.ErrorMessages.Clear();
      if (IsEventSubDomainExists(ev.EventUrl, ev.EventID))
        ev.ErrorMessages.Add("Sorry "+ ev.EventUrl + ".Eventcombo.com already exists, try another URL");
      if (String.IsNullOrWhiteSpace(ev.EventTitle))
        ev.ErrorMessages.Add("Event must have title.");
      if (!ev.OnlineEvent && (String.IsNullOrWhiteSpace(ev.Address)))
        ev.ErrorMessages.Add("Event must have address or must be Online Event.");
      if (String.IsNullOrWhiteSpace(ev.TimeZone))
        ev.ErrorMessages.Add("You need to select timezone of event.");
      if ((ev.DateInfo.Frequency == ScheduleFrequency.Single)
        && ((ev.DateInfo.StartDateTime < DateTime.Now) || (ev.DateInfo.EndDateTime < DateTime.Now)))
        ev.ErrorMessages.Add("Event need to be in the future.");
      if (ev.DateInfo.IsNewDate)
        ev.ErrorMessages.Add("You need to select date for event.");
      if (ev.TicketList.Count == 0)
        ev.ErrorMessages.Add("You need to add at least one ticket.");
      if (ev.EventTypeID == 0)
        ev.ErrorMessages.Add("You need to select event type.");
      if (ev.EventCategoryID == 0)
        ev.ErrorMessages.Add("You need to select event category.");

      bool haveTitle = true;
      bool haveQuantity = true;
      foreach (var ticket in ev.TicketList)
      {
        if (String.IsNullOrWhiteSpace(ticket.T_name))
          haveTitle = false;
        if (ticket.Qty_Available <= 0)
          haveQuantity = false;
      }
      if (!haveTitle)
        ev.ErrorMessages.Add("All tickets must have title.");
      if (!haveQuantity)
        ev.ErrorMessages.Add("You need to set quantity > 0 for all tickets (or remove tickets that you don't need).");

      ev.ErrorEvent = ev.ErrorMessages.Count > 0;
      return !ev.ErrorEvent;
    }

    private bool SaveAddress(EventViewModel ev, IUnitOfWork uow)
    {
      bool res = false;
      IRepository<Address> aRepo = new GenericRepository<Address>(_factory.ContextFactory);
      var address = aRepo.Get(filter: (a => a.EventId == ev.EventID)).FirstOrDefault();
      if (address == null)
        address = new Address();
      address.EventId = ev.EventID;
      res = res || (address.ConsolidateAddress != ev.Address);
      address.ConsolidateAddress = ev.Address;
      address.UserId = ev.UserID;
      res = res || (address.VenueName != ev.VenueName);
      address.VenueName = ev.VenueName ?? "";
      try
      {
        var vc = new ViewEvent();
        var geocode = vc.Geocode(ev.Address);
        address.Latitude = geocode.Latitude.ToString();
        address.Longitude = geocode.Longitude.ToString();
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error during getting coordinates of event Id = {0}", ev.EventID);
        address.Latitude = "";
        address.Longitude = "";
      }
      address.Address1 = "";
      address.Address2 = "";
      address.City = "";
      address.Name = "";
      address.State = "";
      address.Zip = "";
      if (address.AddressID == 0)
        aRepo.Insert(address);
      uow.Context.SaveChanges();
      ev.AddressId = address.AddressID;
      return res;
    }

    private bool SaveMultiplyDates(EventViewModel ev, IUnitOfWork uow)
    {
      bool res = false;
      IRepository<MultipleEvent> multiRepo = new GenericRepository<MultipleEvent>(_factory.ContextFactory);
      IRepository<EventVenue> vRepo = new GenericRepository<EventVenue>(_factory.ContextFactory);

      var single = vRepo.Get(filter: (e => e.EventID == ev.EventID)).ToList();
      res = single.Count > 0;
      foreach (var se in single)
        vRepo.Delete(se);

      MultipleEvent me = multiRepo.Get(filter: (m => m.EventID == ev.EventID)).FirstOrDefault();
      res = res || (me == null);
      if (me == null)
        me = new MultipleEvent();
      me.EventID = ev.EventID;
      res = res || (me.Frequency != ev.DateInfo.Frequency.ToString());
      me.Frequency = ev.DateInfo.Frequency.ToString();

      res = res || (me.StartingFrom != ev.DateInfo.StartDateTime.ToString("MM/dd/yyyy"));
      me.StartingFrom = ev.DateInfo.StartDateTime.ToString("MM/dd/yyyy");
      res = res || (me.StartTime != ev.DateInfo.StartDateTime.ToString("hh:mm tt"));
      me.StartTime = ev.DateInfo.StartDateTime.ToString("hh:mm tt");
      res = res || (me.StartingTo != ev.DateInfo.EndDateTime.ToString("MM/dd/yyyy"));
      me.StartingTo = ev.DateInfo.EndDateTime.ToString("MM/dd/yyyy");
      res = res || (me.EndTime != ev.DateInfo.EndDateTime.ToString("hh:mm tt"));
      me.EndTime = ev.DateInfo.EndDateTime.ToString("hh:mm tt");

      TimeZoneInfo userTimeZone = GetTimeZoneInfo(ev.TimeZone);
      if (userTimeZone != null)
      {
        ev.DateInfo.StartDateTime = ConvertTimeToUtc(ev.DateInfo.StartDateTime, userTimeZone);
        ev.DateInfo.EndDateTime = ConvertTimeToUtc(ev.DateInfo.EndDateTime, userTimeZone); ;
      }

      res = res || (me.M_Startfrom != ev.DateInfo.StartDateTime);
      me.M_Startfrom = ev.DateInfo.StartDateTime;
      res = res || (me.M_StartTo != ev.DateInfo.EndDateTime);
      me.M_StartTo = ev.DateInfo.EndDateTime;
      if (ev.DateInfo.Frequency == ScheduleFrequency.Weekly)
      {
        List<string> weekdays = new List<string>();
        foreach (var day in ev.DateInfo.Weekdays)
          weekdays.Add(((int)day).ToString());
        res = res || (me.WeeklyDay != String.Join(",", weekdays));
        me.WeeklyDay = String.Join(",", weekdays);
      }
      if (me.MultipleEventID == 0)
        multiRepo.Insert(me);
      uow.Context.SaveChanges();
      return res;
    }

    private bool SaveSingleDate(EventViewModel ev, long addressId, IUnitOfWork uow)
    {
      bool res = false;
      IRepository<MultipleEvent> multiRepo = new GenericRepository<MultipleEvent>(_factory.ContextFactory);
      IRepository<EventVenue> vRepo = new GenericRepository<EventVenue>(_factory.ContextFactory);

      var multi = multiRepo.Get(filter: (m => m.EventID == ev.EventID)).ToList();
      res = multi.Count() > 0;
      foreach (var me in multi)
        multiRepo.Delete(me);

      EventVenue se = vRepo.Get(filter: (e => e.EventID == ev.EventID)).FirstOrDefault();
      res = res || (se == null);
      if (se == null)
        se = new EventVenue();
      se.EventID = ev.EventID;

      se.EventStartDate = ev.DateInfo.StartDateTime.ToString("MM/dd/yyyy");
      se.EventStartTime = ev.DateInfo.StartDateTime.ToString("hh:mm tt");
      se.EventEndDate = ev.DateInfo.EndDateTime.ToString("MM/dd/yyyy");
      se.EventEndTime = ev.DateInfo.EndDateTime.ToString("hh:mm tt");

      TimeZoneInfo userTimeZone = GetTimeZoneInfo(ev.TimeZone);
      if (userTimeZone != null)
      {
        ev.DateInfo.StartDateTime = ConvertTimeToUtc(ev.DateInfo.StartDateTime, userTimeZone);
        ev.DateInfo.EndDateTime = ConvertTimeToUtc(ev.DateInfo.EndDateTime, userTimeZone); ;
      }

      if ((se.E_Startdate != ev.DateInfo.StartDateTime) || (se.E_Enddate != ev.DateInfo.EndDateTime) || (se.AddressId != addressId))
        res = true;
      se.E_Startdate = ev.DateInfo.StartDateTime;
      se.E_Enddate = ev.DateInfo.EndDateTime;
      se.AddressId = addressId;
      if (se.EventVenueID == 0)
        vRepo.Insert(se);
      uow.Context.SaveChanges();
      return res;
    }

    private void SaveTickets(EventViewModel ev, IUnitOfWork uow)
    {
      IRepository<Ticket> tRepo = new GenericRepository<Ticket>(_factory.ContextFactory);

      foreach (var tDB in tRepo.Get(filter: (t => t.E_Id == ev.EventID)))
        if (!ev.TicketList.Any(tvm => tvm.T_Id == tDB.T_Id))
          tRepo.Delete(tDB);

      foreach (var ticket in ev.TicketList)
      {
        Ticket tDB = null;
        if (ticket.T_Id != 0)
          tDB = tRepo.GetByID(ticket.T_Id);
        if (tDB == null)
          tDB = new Ticket();

        long purchasedQuantity = ticket.T_Id == 0 ? 0 : GetPurchasedTicketQuantity(ticket.T_Id);

        ticket.E_Id = ev.EventID;
        if (purchasedQuantity != 0)
        {
          ticket.Price = tDB.Price;
          ticket.Qty_Available = ticket.Qty_Available < purchasedQuantity ? purchasedQuantity : ticket.Qty_Available;
          ticket.Fees_Type = tDB.Fees_Type;
        }

        UpdatePrices(ticket, tDB, ev.IsAdmin);

        _mapper.Map(ticket, tDB);
        DateTime? saleStart = ticket.Sale_Start_Date;
        DateTime? saleEnd = ticket.Sale_End_Date;
        DateTime? hideUntil = ticket.Hide_Untill_Date;
        DateTime? hideAfter = ticket.Hide_After_Date;

        tDB.Sale_Start_Date = saleStart == null ? null : (DateTime?)saleStart.Value.Date;
        tDB.Sale_Start_Time = saleStart == null ? null : saleStart.Value.ToString("hh:mm tt");
        tDB.Sale_End_Date = saleEnd == null ? null : (DateTime?)saleEnd.Value.Date;
        tDB.Sale_End_Time = saleEnd == null ? null : saleEnd.Value.ToString("hh:mm tt");
        tDB.Hide_Untill_Date = hideUntil == null ? null : (DateTime?)hideUntil.Value.Date;
        tDB.Hide_Untill_Time = hideUntil == null ? null : hideUntil.Value.ToString("hh:mm tt");
        tDB.Hide_After_Date = hideAfter == null ? null : (DateTime?)hideAfter.Value.Date;
        tDB.Hide_After_Time = hideAfter == null ? null : hideAfter.Value.ToString("hh:mm tt");

        if (tDB.T_Id == 0)
          tRepo.Insert(tDB);
        uow.Context.SaveChanges();
        ticket.T_Id = tDB.T_Id;
      }
    }

    private void UpdatePrices(TicketViewModel ticket, Ticket ticketDB, bool isAdmin)
    {
      if (ticket.TicketTypeID != 2)
        ticket.Price = 0;
      else
        ticket.Price = Math.Round(ticket.Price ?? 0, 2);
      ticket.T_Discount = Math.Round(ticket.T_Discount ?? 0, 2);
      if (!isAdmin)
      {
        if (ticketDB.T_Id == 0)
        {
          IRepository<Fee_Structure> fRepo = new GenericRepository<Fee_Structure>(_factory.ContextFactory);
          var fee = fRepo.Get(filter: (f => f.FS_Apply == "A")).FirstOrDefault();
          if (fee != null)
          {
            ticket.T_Ecpercent = ticket.TicketTypeID == 2 ? fee.FS_Percentage ?? 0 : 0;
            ticket.T_EcAmount = ticket.TicketTypeID != 1 ? fee.FS_Amount ?? 0 : 0;
          }
          else
          {
            ticket.T_Ecpercent = 0;
            ticket.T_EcAmount = 0;
          }
          ticket.Customer_Fee = 0;
        }
        else
        {
          ticket.T_Ecpercent = ticketDB.T_Ecpercent;
          ticket.T_EcAmount = ticketDB.T_EcAmount;
          ticket.Customer_Fee = ticketDB.Customer_Fee;
        }
      }
      ticket.EC_Fee = Math.Round((ticket.Price ?? 0) * ((ticket.T_Ecpercent ?? 0) / 100) + (ticket.T_EcAmount ?? 0), 2);
      ticket.TotalPrice = (ticket.Price ?? 0) - (ticket.T_Discount ?? 0) + (ticket.Customer_Fee ?? 0);
      if (String.IsNullOrEmpty(ticket.Fees_Type) || (ticket.Fees_Type == "0"))
        ticket.TotalPrice += ticket.EC_Fee;
    }

    private void SaveVarCharges(EventViewModel ev, IUnitOfWork uow)
    {
      IRepository<Event_VariableDesc> vcRepo = new GenericRepository<Event_VariableDesc>(_factory.ContextFactory);

      if (String.IsNullOrEmpty(ev.Ticket_showvariable) || (ev.Ticket_showvariable.ToUpper() != "Y"))
        ev.VariableChargesList.Clear();

      foreach (var vcDB in vcRepo.Get(filter: (vc => vc.Event_Id == ev.EventID)))
        if (!ev.VariableChargesList.Any(vcVM => vcVM.VariableId == vcDB.Variable_Id))
          vcRepo.Delete(vcDB);

      foreach (var varCharge in ev.VariableChargesList)
      {
        Event_VariableDesc vc = null;
        if (varCharge.VariableId != 0)
          vc = vcRepo.GetByID(varCharge.VariableId);
        if (vc == null)
          vc = new Event_VariableDesc();
        varCharge.EventId = ev.EventID;
        _mapper.Map(varCharge, vc);
        if (vc.Variable_Id == 0)
          vcRepo.Insert(vc);
        uow.Context.SaveChanges();
        varCharge.VariableId = vc.Variable_Id;
      }
    }

    private void SaveOrganizers(EventViewModel ev, IUnitOfWork uow, Func<string, string> mapPath)
    {
      IRepository<Organizer_Master> orgRepo = new GenericRepository<Organizer_Master>(_factory.ContextFactory);
      IRepository<Event_Orgnizer_Detail> evoRepo = new GenericRepository<Event_Orgnizer_Detail>(_factory.ContextFactory);

      foreach (var org in ev.OrganizerList)
      {
        Organizer_Master oDB = null;
        if (org.OrgnizerId != 0)
          oDB = orgRepo.GetByID(org.OrgnizerId);
        if (oDB == null)
        {
          oDB = orgRepo.Get(filter: (o => ((o.Orgnizer_Name == org.Orgnizer_Name) && (o.Organizer_Email == org.Organizer_Email) && (o.UserId == ev.UserID)))).FirstOrDefault();
          if (oDB == null)
            oDB = new Organizer_Master();
        }

        _mapper.Map(org, oDB);

        if ((org.Image != null) && (org.Image.ECImageId != 0))
        {
          org.Image = _iservice.SaveToDB(org.Image, ev.UserID, uow);
          oDB.ECImageId = org.Image.ECImageId;
          org.ECImageId = org.Image.ECImageId;
          org.Organizer_Image = org.Image.ImagePath;
          org.Imagepath = org.Image.ImagePath;
        }

        if (((oDB.ECImageId ?? 0) != 0) && (org.Image != null))
        {
          _iservice.DeleteImage(org.Image, ev.UserID, uow);
          oDB.ECImageId = 0;
        }

        if (oDB.Orgnizer_Id == 0)
          orgRepo.Insert(oDB);
        uow.Context.SaveChanges();

        org.OrgnizerId = oDB.Orgnizer_Id;
        org.ECImageId = oDB.ECImageId;
        if (org.InternalId == ev.InternalOrganizerId)
          ev.OrganizerId = oDB.Orgnizer_Id;
      }

      Event_Orgnizer_Detail eodDB = evoRepo.Get(filter: (d => d.Orgnizer_Event_Id == ev.EventID)).FirstOrDefault();
      if (eodDB == null)
        eodDB = new Event_Orgnizer_Detail();
      eodDB.Orgnizer_Event_Id = ev.EventID;
      eodDB.UserId = ev.UserID;
      eodDB.OrganizerMaster_Id = ev.OrganizerId;
      eodDB.DefaultOrg = "Y";
      if (eodDB.Orgnizer_Id == 0)
        evoRepo.Insert(eodDB);
      uow.Context.SaveChanges();
    }

    private void SaveImages(EventViewModel ev, IUnitOfWork uow, Func<string, string> mapPath)
    {
      IRepository<EventImage> eiRepo = new GenericRepository<EventImage>(_factory.ContextFactory);
      IRepository<EventECImage> ecRepo = new GenericRepository<EventECImage>(_factory.ContextFactory);

      var dbECImages = ecRepo.Get(filter: (ei => ei.EventId == ev.EventID));
      foreach (var ecDBLink in dbECImages)
        if (!ev.EventImages.Any(ei => ei.ECImageId == ecDBLink.ECImageId))
        {
          var imageId = ecDBLink.ECImageId;
          ecRepo.Delete(ecDBLink);
          _iservice.DeleteImage(imageId, ev.UserID, uow);
        }

      foreach (var ecImage in ev.EventImages)
      {
        ECImageViewModel newImage;
        if (ecImage.ECImageId == 0)
          newImage = _iservice.SaveToDB(ecImage, ev.UserID, uow);
        else
          newImage = ecImage;
        var ecDBLink = ecRepo.Get(filter: (el => (el.ECImageId == newImage.ECImageId) && (el.EventId == ev.EventID))).FirstOrDefault();
        if (ecDBLink == null)
          ecRepo.Insert(new EventECImage()
          {
            ECImageId = newImage.ECImageId,
            EventId = ev.EventID
          });
      }
      uow.Context.SaveChanges();
    }


    public void SaveEvent(EventViewModel ev, Func<string, string> mapPath)
    {
      bool sendNotification = false;
      bool EventChanged = false;
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);

          Event evDB = null;

          if (ev.EventID != 0)
          {
            evDB = eRepo.GetByID(ev.EventID);
            ev.ModifyDate = DateTime.Now;
            sendNotification = ((ev.EventStatus ?? "").ToUpper() == "LIVE") && ((evDB.EventStatus ?? "").ToUpper() != "LIVE");
          }
          if (evDB == null)
          {
            sendNotification = true;
            evDB = new Event();
            ev.CreateDate = DateTime.Now;
            ev.ModifyDate = null;
          }

          if (!String.IsNullOrEmpty(evDB.EventStatus) && (evDB.EventStatus.ToUpper() == "LIVE"))
            ev.EventStatus = "Live";

          _mapper.Map(ev, evDB);
          string status;
          if (ev.OnlineEvent)
            status = "Online";
          else
            status = "Single";
          EventChanged = evDB.AddressStatus != status;
          evDB.AddressStatus = status;
          if (evDB.EventID == 0)
            eRepo.Insert(evDB);
          uow.Context.SaveChanges();
          ev.EventID = evDB.EventID;

          if ((ev.BGImage != null) && (ev.BGImage.ECImageId == 0))
          {
            if ((evDB.ECBackgroundId ?? 0) != 0)
              _iservice.DeleteImage((evDB.ECBackgroundId ?? 0), ev.UserID, uow);
            var ecImage = _iservice.SaveToDB(ev.BGImage, ev.UserID, uow);
            evDB.ECBackgroundId = ecImage.ECImageId;
            ev.BGImage = ecImage;
          }
          if (((evDB.ECBackgroundId ?? 0) != 0) && (ev.BGImage == null))
          {
            _iservice.DeleteImage(evDB.ECBackgroundId ?? 0, ev.UserID, uow);
            evDB.ECBackgroundId = 0;
          }
          ev.ECBackgroundId = evDB.ECBackgroundId;

          if ((ev.ECImage != null) && (ev.ECImage.ECImageId == 0))
          {
            if ((evDB.ECImageId ?? 0) != 0)
              _iservice.DeleteImage(evDB.ECImageId ?? 0, ev.UserID, uow);
            var ecImage = _iservice.SaveToDB(ev.ECImage, ev.UserID, uow);
            evDB.ECImageId = ecImage.ECImageId;
            ev.ECImage = ecImage;
          }
          if ((ev.ECImage == null) && ((evDB.ECImageId ?? 0) != 0))
          {
            _iservice.DeleteImage(evDB.ECImageId ?? 0, ev.UserID, uow);
            evDB.ECImageId = 0;
          }
          ev.ECImageId = evDB.ECImageId;

          SaveImages(ev, uow, mapPath);

          EventChanged = SaveAddress(ev, uow) || EventChanged;

          if (ev.DateInfo.Frequency == ScheduleFrequency.Single)
            EventChanged = SaveSingleDate(ev, ev.AddressId, uow) || EventChanged;
          else
            EventChanged = SaveMultiplyDates(ev, uow) || EventChanged;

          SaveOrganizers(ev, uow, mapPath);
          SaveTickets(ev, uow);
          SaveVarCharges(ev, uow);
          uow.Context.SaveChanges();

          if (ev.EventStatus == "Live")
          {
            var param1 = new SqlParameter("@EventId", ev.EventID);
            var param2 = new SqlParameter("@UserId", ev.UserID);
            var res = uow.Context.Database.ExecuteSqlCommand("EXEC PublishEvent @EventId, @UserId", param1, param2);
          }

          uow.Context.SaveChanges();

          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception during SaveEvent.", ex);
        }
      }

      try
      {
        if (sendNotification)
        {
          var sendEvent = GetEventById(ev.EventID);
          sendEvent.EventPath = ResolveServerUrl(VirtualPathUtility.ToAbsolute(sendEvent.EventPath), false);
          INotification notification = new NewEventNotification(_factory, sendEvent, ConfigurationManager.AppSettings.Get("DefaultEmail"));
          notification.SendNotification(new SendMailService());
        }

      } 
      catch (Exception ex)
      {
        _logger.Error(ex, "Error during send notification about new event.");
      }

      try
      {
        IRepository<Ticket_Purchased_Detail> tRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
        if (EventChanged && ((tRepo.Get(filter: (t => t.TPD_Event_Id == ev.EventID)).Sum(t => t.TPD_Purchased_Qty) ?? 0) > 0))
        {
          var eventSend = GetEventById(ev.EventID);
          eventSend.EventPath = ResolveServerUrl(VirtualPathUtility.ToAbsolute(eventSend.EventPath), false);
          INotification notifyBuyers = new EventChangeNotification(_factory, eventSend, ConfigurationManager.AppSettings.Get("DefaultEmail"));
          INotificationSender sender = new NotificationSender(notifyBuyers, new SendMailService());
          IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
          IRepository<TicketBearer> attRepo = new GenericRepository<TicketBearer>(_factory.ContextFactory);
          List<MailAddress> addresses = new List<MailAddress>();
          foreach (var ticket in tpdRepo.Get(filter: (tpd => tpd.TPD_Event_Id == ev.EventID)))
          {
            var profile = ticket.AspNetUser.Profiles.FirstOrDefault();
            if ((profile != null) && (!addresses.Any(a => a.Address == profile.Email)))
              addresses.Add(new MailAddress(profile.Email, String.Format("{0} {1}", profile.FirstName, profile.LastName)));
            foreach (var attendee in attRepo.Get(filter: (a => a.OrderId == ticket.TPD_Order_Id)))
              if (!String.IsNullOrEmpty(attendee.Email) && (!addresses.Any(a => a.Address == attendee.Email)))
                addresses.Add(new MailAddress(attendee.Email, attendee.Name));
          }
          sender.SendSeparately(addresses);
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error during send notification about event change.");
      }
    }

    public void PublishEvent(long id, string userId)
    {
      var param1 = new SqlParameter("@EventId", id);
      var param2 = new SqlParameter("@UserId", userId);
      var res = _factory.ContextFactory.GetContext().Database.ExecuteSqlCommand("EXEC PublishEvent @EventId, @UserId", param1, param2);
    }

    public IEnumerable<EventSearchViewModel> Search(string searchStr)
    {
      DateTime now = DateTime.UtcNow;
      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      IRepository<EventType> etRepo = new GenericRepository<EventType>(_factory.ContextFactory);
      IRepository<EventCategory> ecRepo = new GenericRepository<EventCategory>(_factory.ContextFactory);
      List<EventSearchViewModel> evList = eRepo.Get(filter: (e => e.EventVenues.Any(ev => (ev.E_Enddate ?? now) >= now) && e.EventTitle.Contains(searchStr)))
        .Select(e => new EventSearchViewModel()
        {
          EventId = e.EventID,
          RecordTypeId = 0,
          EventTitle = e.EventTitle,
          Latitude = e.Addresses.Count > 0 ? e.Addresses.FirstOrDefault().Latitude : null,
          Longitude = e.Addresses.Count > 0 ? e.Addresses.FirstOrDefault().Longitude : null
        }).ToList();

      evList.AddRange(etRepo.Get(filter: (et => et.EventType1.Contains(searchStr))).Select(et => new EventSearchViewModel()
        {
          EventId = et.EventTypeID,
          RecordTypeId = 1,
          EventTitle = et.EventType1 + " (Event Type)"
        }));

      evList.AddRange(ecRepo.Get(filter: (ec => ec.EventCategory1.Contains(searchStr))).Select(ec => new EventSearchViewModel()
      {
        EventId = ec.EventCategoryID,
        RecordTypeId = 2,
        EventTitle = ec.EventCategory1 + " (Event Category)"
      }));

      return evList.OrderBy(e => e.EventTitle);
    }


    public EventViewModel GetEventById(long id)
    {
      EventViewModel ev = new EventViewModel() { EventID = id };
      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      IRepository<EventImage> iRepo = new GenericRepository<EventImage>(_factory.ContextFactory);
      IRepository<EventECImage> eciRepo = new GenericRepository<EventECImage>(_factory.ContextFactory);
      IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);

      Event evDB = eRepo.Get(filter: (e => e.EventID == ev.EventID)).SingleOrDefault();
      if (ev == null)
        throw new ArgumentException(String.Format("Event {0} not found.", ev.EventID));

      _mapper.Map(evDB, ev);
      if (String.IsNullOrEmpty(ev.Ticket_variabletype))
        ev.Ticket_variabletype = "O";
      LoadEventDictionaries(ev);

      ev.EventPath = GetEventUrl(ev.EventID, ev.EventTitle, new UrlHelper(HttpContext.Current.Request.RequestContext));

      ev.OnlineEvent = ev.AddressStatus == "Online";
      var orgn = evDB.Event_Orgnizer_Detail.Where(od => od.DefaultOrg == "Y").FirstOrDefault();
      ev.OrganizerId = orgn == null ? 0 : orgn.OrganizerMaster_Id;
      ev.InternalOrganizerId = ev.OrganizerId == 0 ? 0 : ev.CurrentOrganizer.InternalId;

      if (evDB.ECBackgroundId != null)
        ev.BGImage = _iservice.GetImageById(evDB.ECBackgroundId ?? 0);
      if (evDB.ECImageId != null)
        ev.ECImage = _iservice.GetImageById(evDB.ECImageId ?? 0);

      var dbImages = iRepo.Get(filter: (im => im.EventID == ev.EventID));
      var ecImages = eciRepo.Get(filter: (eci => eci.EventId == ev.EventID), orderBy: (query => query.OrderBy(im => im.ECImageId)));
      var images = new List<string>();
      if ((dbImages.Count() > 0) && (ecImages.Count() == 0))
        using (var uow = _factory.GetUnitOfWork())
          try
          {
            foreach (var eventImage in dbImages)
            {
              var ecImage = ProcessOldEventImage(eventImage, ev.UserID, uow);
              if (ecImage != null)
              {
                if ((evDB.ECImageId ?? 0) == 0)
                {
                  evDB.ECImageId = ecImage.ECImageId;
                  ev.ECImage = _iservice.GetImageById(evDB.ECImageId ?? 0);
                }
                else
                {
                  ev.EventImages.Add(_iservice.GetImageById(ecImage.ECImageId));
                  eciRepo.Insert(new EventECImage()
                    {
                      ECImageId = ecImage.ECImageId,
                      EventId = ev.EventID
                    });
                }
              }
            }
            uow.Context.SaveChanges();
            uow.Commit();
          }
          catch (Exception ex)
          {
            uow.Rollback();
            _logger.Error(ex, "Error during transfer images to EventECImage.", null);
          }
      else
        foreach (var ecImage in ecImages)
          ev.EventImages.Add(_iservice.GetImageById(ecImage.ECImageId));

      IRepository<Address> aRepo = new GenericRepository<Address>(_factory.ContextFactory);
      var address = aRepo.Get(filter: (a => a.EventId == ev.EventID)).FirstOrDefault();
      if (address == null)
      {
        ev.AddressId = 0;
        ev.Address = "";
        ev.VenueName = "";
      }
      else
      {
        ev.AddressId = address.AddressID;
        ev.Address = address.ConsolidateAddress;
        ev.VenueName = address.VenueName;
      }

      TimeZoneInfo tz = GetTimeZoneInfo(ev.TimeZone);
      if (tz == null)
        tz = TimeZoneInfo.Utc;

      var multiDate = evDB.MultipleEvents.FirstOrDefault();
      var singleDate = evDB.EventVenues.SingleOrDefault();
      ev.DateInfo.Frequency = multiDate == null ? ScheduleFrequency.Single : (ScheduleFrequency)Enum.Parse(typeof(ScheduleFrequency), multiDate.Frequency, true);
      ev.DateInfo.IsNewDate = false;
      ev.DateInfo.StartDateTime = (multiDate == null ? singleDate.E_Startdate : multiDate.M_Startfrom) ?? DateTime.MinValue;
      ev.DateInfo.EndDateTime = (multiDate == null ? singleDate.E_Enddate : multiDate.M_StartTo) ?? DateTime.MinValue;
      if (ev.DateInfo.Frequency == ScheduleFrequency.Weekly)
        ev.DateInfo.Weekdays.AddRange(multiDate.WeeklyDay.Split(',').Select(s => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), s.Trim())).ToList());
      if ((ev.DateInfo.StartDateTime > DateTime.MinValue) && (tz != null))
        ev.DateInfo.StartDateTime = ConvertTimeFromUtc(ev.DateInfo.StartDateTime, tz);
      if ((ev.DateInfo.EndDateTime > DateTime.MinValue) && (tz != null))
        ev.DateInfo.EndDateTime = ConvertTimeFromUtc(ev.DateInfo.EndDateTime, tz);

      foreach (var t in evDB.Tickets.OrderBy(t => t.T_order ?? 0))
      {
        TicketViewModel tvm = _mapper.Map<TicketViewModel>(t);

        DateTime saleStartDate;
        DateTime saleEndDate;
        DateTime hideStart;
        DateTime hideEnd;

        if (tvm.Sale_Start_Date != null)
        {
          DateTime.TryParse((tvm.Sale_Start_Date ?? default(DateTime)).ToShortDateString() + " " + tvm.Sale_Start_Time, out saleStartDate);
          tvm.Sale_Start_Date = saleStartDate;
        }
        if (tvm.Sale_End_Date != null)
        {
          DateTime.TryParse((tvm.Sale_End_Date ?? default(DateTime)).ToShortDateString() + " " + tvm.Sale_End_Time, out saleEndDate);
          tvm.Sale_End_Date = saleEndDate;
        }
        if (tvm.Hide_Untill_Date != null)
        {
          DateTime.TryParse((tvm.Hide_Untill_Date ?? default(DateTime)).ToShortDateString() + " " + tvm.Hide_Untill_Time, out hideStart);
          tvm.Hide_Untill_Date = hideStart;
        }
        if (tvm.Hide_After_Date != null)
        {
          DateTime.TryParse((tvm.Hide_After_Date ?? default(DateTime)).ToShortDateString() + " " + tvm.Hide_After_Time, out hideEnd);
          tvm.Hide_After_Date = hideEnd;
        }

        tvm.TicketType.TicketTypeId = Convert.ToByte(t.TicketTypeID ?? 1);
        tvm.TicketType.TicketType = tvm.TicketType.TicketTypeId == 1 ? "Free" : tvm.TicketType.TicketTypeId == 2 ? "Paid" : "Donation";
        tvm.PurchasedQuantity = GetPurchasedTicketQuantity(tvm.T_Id);
        ev.TicketList.Add(tvm);
      }

      foreach (var vc in evDB.Event_VariableDesc)
      {
        VariableChargesViewModel vcvm = _mapper.Map<VariableChargesViewModel>(vc);
        ev.VariableChargesList.Add(vcvm);
      }

      return ev;
    }

    private long GetPurchasedTicketQuantity(long ticketId)
    {
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var q = tpdRepo.Get(filter: (tpd => tpd.Ticket_Quantity_Detail.TQD_Ticket_Id == ticketId)).Sum(tpd => tpd.TPD_Purchased_Qty);
      return q ?? 0;
    }

    private ECImageViewModel ProcessOldEventImage(EventImage image, string userId, IUnitOfWork uow)
    {
      try
      {
        string filePath = HostingEnvironment.MapPath("/Images/events/event_flyers/imagepath/" + image.EventImageUrl);
        ECImageViewModel ecImage = new ECImageViewModel()
        {
          ECImageId = 0,
          ECImageTypeId = 0,
          Filename = image.EventImageUrl,
          FilePath = filePath,
          TypeName = image.ImageType
        };
        return _iservice.SaveToDB(ecImage, userId, uow);
      }
      catch (Exception ex)
      {
        _logger.Error(ex, String.Format("Error while move image '{0}'to ECImage (EventImageId = {1}). ", image.EventImageUrl, image.EventImageID));
        return null;
      }

    }

    public EventInfoViewModel GetEventInfo(long eventId, string userId, UrlHelper url)
    {
      EventInfoViewModel evi = new EventInfoViewModel();
      evi.EventId = eventId;

      UpdateEventInfo(evi, userId, url);

      return evi;
    }

    private string GetECImageUrl(long ImageId)
    {
      var ecImage = _iservice.GetImageById(ImageId);
      if (ecImage != null)
        return ecImage.ImagePath;
      else
        return "";
    }

    public void UpdateEventInfo(EventInfoViewModel evi, string userId, UrlHelper url)
    {
      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      IRepository<EventSubCategory> escRepo = new GenericRepository<EventSubCategory>(_factory.ContextFactory);
      IRepository<EventImage> iRepo = new GenericRepository<EventImage>(_factory.ContextFactory);
      IRepository<EventECImage> eciRepo = new GenericRepository<EventECImage>(_factory.ContextFactory);
      IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);
      IRepository<EventFavourite> favRepo = new GenericRepository<EventFavourite>(_factory.ContextFactory);
      IRepository<EventVote> voteRepo = new GenericRepository<EventVote>(_factory.ContextFactory);

      Event ev = eRepo.Get(filter: (e => e.EventID == evi.EventId)).SingleOrDefault();
      if (ev == null)
        throw new ArgumentException(String.Format("Event {0} not found.", evi.EventId));

      evi.ErrorEvent = false;
      evi.ErrorMessages = new List<string>();
      evi.EventTitle = ev.EventTitle;
      evi.EventDescription = ev.EventDescription;
      evi.EventShortDesc = HtmlProcessing.GetShortString(HtmlProcessing.StripTagsRegex(HttpUtility.HtmlDecode(String.IsNullOrEmpty(evi.EventDescription) ? "" : evi.EventDescription)), 90, 150, ".");
      evi.OnlineEvent = ev.AddressStatus == "Online";
      evi.EventTypeId = ev.EventTypeID;
      evi.EventType = ev.EventType.EventType1;
      evi.EventCategoryId = ev.EventCategoryID;
      evi.EventCategory = ev.EventCategory.EventCategory1;
      evi.DisplayStartTime = ev.DisplayStartTime == "Y";
      evi.DisplayEndTime = ev.DisplayEndTime == "Y";
      evi.BackgroundColor = ev.BackgroundColor;
      evi.EventUrl = GetEventUrl(evi.EventId, evi.EventTitle, url);
      evi.EventSubCategoryId = ev.EventSubCategoryID;
      var esc = escRepo.Get(filter: (esub => esub.EventSubCategoryID == ev.EventSubCategoryID)).SingleOrDefault();
      if (esc != null)
        evi.EventSubCategory = esc.EventSubCategory1;
      var address = ev.Addresses.FirstOrDefault();
      if (address != null)
      {
        evi.Address = address.ConsolidateAddress;
        evi.VenueName = address.VenueName;
        evi.Longitude = address.Longitude;
        evi.Latitude = address.Latitude;
      }
      else if (evi.OnlineEvent)
        evi.Address = "Online";

      evi.ShowRemainingTickets = ev.Ticket_showremain == "1";

      evi.FavoriteCount = favRepo.Get(filter: (fav => fav.eventId == evi.EventId)).Count();
      if (String.IsNullOrWhiteSpace(userId))
        evi.UserFavorite = false;
      else
        evi.UserFavorite = favRepo.Get(filter: (fav => (fav.eventId == evi.EventId) && (fav.UserID == userId))).Any();

      evi.VoteCount = voteRepo.Get(filter: (v => v.eventId == evi.EventId)).Count();
      if (String.IsNullOrWhiteSpace(userId))
        evi.UserVote = false;
      else
        evi.UserVote = voteRepo.Get(filter: (v => (v.eventId == evi.EventId) && (v.UserID == userId))).Any();

      TimeZoneInfo tz = GetTimeZoneInfo(ev.TimeZone);
      if (tz == null)
        tz = TimeZoneInfo.Utc;
      if ((ev.DisplayTimeZone == "Y") && (tz != null))
        evi.TimeZone = tz.DisplayName;

      var multiDate = ev.MultipleEvents.FirstOrDefault();
      var singleDate = ev.EventVenues.SingleOrDefault();
      evi.DateInfo = new EventDateViewModel()
      {
        Frequency = multiDate == null ? ScheduleFrequency.Single : (ScheduleFrequency)Enum.Parse(typeof(ScheduleFrequency), multiDate.Frequency, true),
        IsNewDate = false,
        StartDateTime = (multiDate == null ? singleDate.E_Startdate : multiDate.M_Startfrom) ?? DateTime.MinValue,
        EndDateTime = (multiDate == null ? singleDate.E_Enddate : multiDate.M_StartTo) ?? DateTime.MinValue,
      };
      if (evi.DateInfo.Frequency == ScheduleFrequency.Weekly)
        evi.DateInfo.Weekdays.AddRange(multiDate.WeeklyDay.Split(',').Select(s => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), s.Trim())).ToList());
      if ((evi.DateInfo.StartDateTime > DateTime.MinValue) && (tz != null))
        evi.DateInfo.StartDateTime = ConvertTimeFromUtc(evi.DateInfo.StartDateTime, tz);
      if ((evi.DateInfo.EndDateTime > DateTime.MinValue) && (tz != null))
        evi.DateInfo.EndDateTime = ConvertTimeFromUtc(evi.DateInfo.EndDateTime, tz);

      if ((ev.ECBackgroundId ?? 0) > 0)
        evi.BackgroundUrl = GetECImageUrl(ev.ECBackgroundId ?? 0);

      if ((ev.ECImageId ?? 0) > 0)
        evi.ImageUrl = GetECImageUrl(ev.ECImageId ?? 0);

      var dbImages = iRepo.Get(filter: (im => im.EventID == evi.EventId));
      var ecImages = eciRepo.Get(filter: (eci => eci.EventId == evi.EventId), orderBy: (query => query.OrderBy(im => im.ECImageId)));
      var images = new List<string>();
      if ((dbImages.Count() > 0) && (ecImages.Count() == 0))
        using (var uow = _factory.GetUnitOfWork())
          try
          {
            foreach (var eventImage in dbImages)
            {
              var ecImage = ProcessOldEventImage(eventImage, ev.UserID, uow);
              if (ecImage != null)
              {
                if ((ev.ECImageId ?? 0) == 0)
                {
                  ev.ECImageId = ecImage.ECImageId;
                  evi.ImageUrl = ecImage.ImagePath;
                }
                else
                {
                  images.Add(ecImage.ImagePath);
                  eciRepo.Insert(new EventECImage()
                    {
                      ECImageId = ecImage.ECImageId,
                      EventId = ev.EventID
                    });
                }
              }
            }
            uow.Context.SaveChanges();
            uow.Commit();
          }
          catch (Exception ex)
          {
            uow.Rollback();
            _logger.Error(ex, "Error during transfer images to EventECImage.", null);
          }
      else
        foreach (var ecImage in ecImages)
          images.Add(GetECImageUrl(ecImage.ECImageId));
      evi.ImagesUrl = images;

      evi.Organizer = new OrganizerInfoViewModel();
      var org = ev.Event_Orgnizer_Detail.FirstOrDefault();
      if ((org != null) && (org.Organizer_Master != null))
      {
        evi.Organizer.OrganizerId = org.Organizer_Master.Orgnizer_Id;
        evi.Organizer.OrganizerName = org.Organizer_Master.Orgnizer_Name;
        evi.Organizer.FBLink = org.Organizer_Master.Organizer_FBLink;
        evi.Organizer.LinkdeInLink = org.Organizer_Master.Organizer_Linkedin;
        evi.Organizer.TwitterLink = org.Organizer_Master.Organizer_Twitter;
        evi.Organizer.WebsiteUrl = org.Organizer_Master.Organizer_Websiteurl;
        evi.Organizer.ImageUrl = GetECImageUrl(org.Organizer_Master.ECImageId ?? 0);
      }

      var tickets = new List<TicketInfoViewModel>();
      var tqDB = tqdRepo.Get(filter: (tqd => (tqd.TQD_Event_Id == evi.EventId)), orderBy: (q => q.OrderBy(t => t.Ticket.T_order ?? 0)));
      bool allSoldOut = true;
      bool allUnavailable = true;
      decimal minTicketPrice = decimal.MaxValue;
      decimal maxTicketPrice = decimal.MinValue;
      int allType = 0;

      DateTime eventNow = DateTime.UtcNow;
      if (tz != null)
        eventNow = ConvertTimeFromUtc(eventNow, tz);

      evi.RemainingTickets = 0;
      foreach (var tq in tqDB)
      {
        DateTime ticketDate;
        DateTime saleStartDate;
        DateTime saleEndDate;
        DateTime.TryParse(tq.TQD_StartDate + " " + tq.TQD_StartTime, out ticketDate);
        DateTime.TryParse((tq.Ticket.Sale_Start_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Sale_Start_Time, out saleStartDate);
        DateTime.TryParse((tq.Ticket.Sale_End_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Sale_End_Time, out saleEndDate);

        bool hideTicket = ticketDate == null;
        if ((tq.Ticket.Hide_Ticket == "1") && !hideTicket)
        {
          DateTime hideStart;
          DateTime hideEnd;
          if (tq.Ticket.T_AutoSechduleType == "1")
          {
            DateTime.TryParse((tq.Ticket.Hide_Untill_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Hide_Untill_Time, out hideStart);
            DateTime.TryParse((tq.Ticket.Hide_After_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Hide_After_Time, out hideEnd);
          }
          else
          {
            hideStart = saleStartDate;
            hideEnd = saleEndDate;
          }
          hideTicket = (hideStart >= eventNow) || ((hideEnd != default(DateTime)) && (hideEnd < eventNow));
        }
        if (!hideTicket)
        {
          var tiVM = new TicketInfoViewModel()
          {
            TQDId = tq.TQD_Id,
            TicketId = tq.TQD_Ticket_Id ?? 0,
            TicketTypeId = tq.Ticket.TicketTypeID ?? 0,
            TicketName = tq.Ticket.T_name,
            TicketDescription = tq.Ticket.Show_T_Desc == "1" ? tq.Ticket.T_Desc : "",
            Minimum = Decimal.ToInt64(tq.Ticket.Min_T_Qty ?? 1),
            Maximum = (tq.Ticket.Max_T_Qty ?? 0) == 0 ? (tq.TQD_Remaining_Quantity ?? 0) : Decimal.ToInt64(tq.Ticket.Max_T_Qty ?? 0),
            Price = (tq.Ticket.Price ?? 0) - (tq.Ticket.T_Discount ?? 0),
            TotalPrice = tq.Ticket.TicketTypeID == 2 ? tq.Ticket.TotalPrice ?? 0 : 0,
            StartDate = ticketDate,
            VenueName = evi.OnlineEvent ? "Online" : tq.Address == null ? "Unknown" : tq.Address.VenueName,
            /*Removed until EC1-414 will be implemented
            * ShowFee = tq.Ticket.Fees_Type != "1",
            * Fee = tq.Ticket.Fees_Type != "1" ? (tq.Ticket.TotalPrice ?? 0) - (tq.Ticket.Price ?? 0) + (tq.Ticket.T_Discount ?? 0) : 0,
            */
            ShowFee = false,
            Fee = 0,
            ShowRemaining = tq.Ticket.T_Displayremaining == "1",
            RemainingQuantity = tq.Ticket.T_Displayremaining == "1" ? tq.TQD_Remaining_Quantity ?? 0 : 0,
            SoldOut = (tq.Ticket.T_Mark_SoldOut == "1") || ((tq.TQD_Remaining_Quantity ?? 0) <= 0)
          };
          if (tiVM.Maximum > (tq.TQD_Remaining_Quantity ?? 0))
            tiVM.Maximum = (tq.TQD_Remaining_Quantity ?? 0);

          allSoldOut = allSoldOut && tiVM.SoldOut;
          allUnavailable = allUnavailable &&
                            ((saleStartDate >= eventNow) ||
                            ((saleEndDate != default(DateTime)) && (saleEndDate < eventNow)) ||
                            tiVM.SoldOut);
          if (tiVM.TicketTypeId != 3)
          {
            minTicketPrice = minTicketPrice > tiVM.Price ? tiVM.Price : minTicketPrice;
            maxTicketPrice = maxTicketPrice < tiVM.Price ? tiVM.Price : maxTicketPrice;
          }
          switch (tiVM.TicketTypeId)
          {
            case 1:
              allType = allType == 0 ? 1 : allType == 3 ? 4 : allType;
              break;
            case 3:
              allType = allType == 0 ? 3 : allType == 1 ? 4 : allType;
              break;
            default:
              allType = 2;
              break;
          };
          if (evi.ShowRemainingTickets)
            evi.RemainingTickets += tiVM.SoldOut ? 0 : tq.TQD_Remaining_Quantity ?? 0;
          tickets.Add(tiVM);
        }
      }
      evi.Tickets = tickets;
      allSoldOut = allSoldOut && (tickets.Count() > 0);
      if (allSoldOut)
      {
        evi.ButtonText = "Sold Out";
        evi.PriceRange = evi.ButtonText;
        evi.CheckoutText = evi.ButtonText;
      }
      else if (allUnavailable)
      {
        evi.ButtonText = "Registration Closed";
        evi.PriceRange = evi.ButtonText;
        evi.CheckoutText = evi.ButtonText;
      }
      else if (allType == 1)
      {
        evi.ButtonText = "Register";
        evi.PriceRange = "Free";
        evi.CheckoutText = "Checkout";
      }
      else if (allType == 3)
      {
        evi.ButtonText = "Donate";
        evi.PriceRange = "Donate";
        evi.CheckoutText = "Checkout";
      }
      else if (allType == 4)
      {
        evi.ButtonText = "Get Tickets";
        evi.PriceRange = "Free, Donate";
        evi.CheckoutText = "Checkout";
      }
      else
      {
        evi.ButtonText = "Get Tickets";
        evi.PriceRange = minTicketPrice == maxTicketPrice ? String.Format("${0:N2}", minTicketPrice) : String.Format("${0:N2} - ${1:N2}", minTicketPrice, maxTicketPrice);
        evi.CheckoutText = "Checkout";
      }

      PopulateOGPData(evi);
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

    private void PopulateOGPData(EventInfoViewModel evi)
    {

      evi.OGPDescription = evi.EventDescription;
      evi.OGPImage = String.IsNullOrEmpty(evi.ImageUrl) ? "" : ResolveServerUrl(VirtualPathUtility.ToAbsolute(evi.ImageUrl), false);
      evi.OGPTitle = evi.EventTitle + " | Eventcombo";
      evi.OGPType = "article";
      evi.OGPUrl = ResolveServerUrl(VirtualPathUtility.ToAbsolute(evi.EventUrl), false);
    }

    public void ValidateEventInfo(EventInfoViewModel ev)
    {
      throw new NotImplementedException();
    }

    public string GetEventUrl(long eventId, string eventTitle, UrlHelper url)
    {
      return url.Action("ViewEvent", "EventManagement", new
      {
        strEventDs = System.Text.RegularExpressions.Regex.Replace(eventTitle.Trim().ToLower().Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""),
        strEventId = eventId.ToString()
      });
    }


    public IncrementResultViewModel AddFavorite(long eventId, string userId)
    {
      IRepository<EventFavourite> fRepo = new GenericRepository<EventFavourite>(_factory.ContextFactory);
      IncrementResultViewModel res = new IncrementResultViewModel()
      {
        Count = 0,
        Processed = false,
        AlreadyProcessed = false
      };
      IRepository<AspNetUser> uRepo = new GenericRepository<AspNetUser>(_factory.ContextFactory);
      var user = uRepo.GetByID(userId);
      if (user != null)
      {
        var fav = fRepo.Get(filter: (f => (f.eventId == eventId) && (f.UserID == userId))).FirstOrDefault();
        res.AlreadyProcessed = fav != null;
        if (!res.AlreadyProcessed)
        {
          fav = new EventFavourite()
          {
            eventId = eventId,
            UserID = userId,
            FavId = 0
          };
          fRepo.Insert(fav);
          _factory.ContextFactory.GetContext().SaveChanges();
          res.Processed = true;
        }
      }
      res.Count = fRepo.Get(filter: (f => f.eventId == eventId)).Count();
      return res;
    }

    public IncrementResultViewModel VoteEvent(long eventId, string userId)
    {
      IRepository<EventVote> vRepo = new GenericRepository<EventVote>(_factory.ContextFactory);
      long cnt = vRepo.Get(filter: (f => f.eventId == eventId)).Count();

      IncrementResultViewModel res = new IncrementResultViewModel()
      {
        Count = cnt,
        Processed = false
      };
      IRepository<AspNetUser> uRepo = new GenericRepository<AspNetUser>(_factory.ContextFactory);
      var user = uRepo.GetByID(userId);
      if (user != null)
      {
        var fav = vRepo.Get(filter: (f => (f.eventId == eventId) && (f.UserID == userId))).FirstOrDefault();
        if (fav == null)
        {
          fav = new EventVote()
          {
            eventId = eventId,
            UserID = userId,
            VoteId = 0
          };
          vRepo.Insert(fav);
          _factory.ContextFactory.GetContext().SaveChanges();
          res.Processed = true;
          res.Count = vRepo.Get(filter: (f => f.eventId == eventId)).Count();
        }
      }
      return res;
    }

    public IEnumerable<ShortEventInfoViewModel> GetEventListByCoords(decimal lat, decimal lng, string userId)
    {
      List<ShortEventInfoViewModel> eList = new List<ShortEventInfoViewModel>();
      IRepository<GetNearestEvents_Result> nRepo = new GenericRepository<GetNearestEvents_Result>(_factory.ContextFactory);
      IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);
      IRepository<EventFavourite> favRepo = new GenericRepository<EventFavourite>(_factory.ContextFactory);

      var dbList = nRepo.SQLQuery("GetNearestEvents @longitude, @latitude, 250, 0",
        new SqlParameter("@longitude", SqlDbType.Float) { Value = lng },
        new SqlParameter("@latitude", SqlDbType.Float) { Value = lat });

      foreach (var evDB in dbList)
      {
        ShortEventInfoViewModel ev = _mapper.Map<ShortEventInfoViewModel>(evDB);
        ECImageViewModel image = _iservice.GetImageById(evDB.ECImageId ?? 0);
        if ((image != null) && (File.Exists(image.FilePath)))
          ev.ImageUrl = image.ImagePath;
        else
          ev.ImageUrl = "/Images/default_event_image.jpg";
        ev.ImageAlt = ev.EventTitle;
        ev.EventShortDesc = HtmlProcessing.GetShortString(HtmlProcessing.StripTagsRegex(evDB.EventDescription), 80, 150, ".");
        ev.EventPath = GetEventUrl(evDB.EventID, evDB.EventTitle, new UrlHelper(HttpContext.Current.Request.RequestContext));
        ev.EventPath = ResolveServerUrl(VirtualPathUtility.ToAbsolute(ev.EventPath), false);

        TimeZoneInfo tz = GetTimeZoneInfo(evDB.TimeZone);
        if (evDB.E_Startdate != null)
        {
          if (tz == null)
            ev.EventDates = evDB.E_Startdate.Value.ToString("f");
          else
            ev.EventDates = ConvertTimeFromUtc(evDB.E_Startdate.Value, tz).ToString("f");
        }
        else if ((evDB.M_Startfrom != null) && (evDB.M_StartTo != null))
        {
          if (tz == null)
            ev.EventDates = String.Format("{0} - {1}", evDB.M_Startfrom.Value.ToString("f"), evDB.M_StartTo.Value.ToString("f"));
          else
            ev.EventDates = String.Format("{0} - {1}", ConvertTimeFromUtc(evDB.M_Startfrom.Value, tz).ToString("f"), ConvertTimeFromUtc(evDB.M_StartTo.Value, tz).ToString("f"));
        }
        else
          ev.EventDates = "";

        var tickets = tqdRepo.Get(filter: (t => t.TQD_Event_Id == ev.EventId));
        DateTime eventNow = DateTime.UtcNow;
        if (tz != null)
          eventNow = ConvertTimeFromUtc(eventNow, tz);
        EventTicketSummary tsum = GetTicketsSummary(tickets, eventNow);

        ev.PriceRange = tsum.State == EventTicketState.RegistrationClosed ? "Registration Closed" :
          tsum.State == EventTicketState.SoldOut ? "Sold Out" :
          tsum.State == EventTicketState.NotAvailable ? "Tickets Unavailable" :
                        tsum.Paid ? ((tsum.MinPrice <= 0) && (tsum.MaxPrice <= 0) ? "FREE" :
                                      tsum.MinPrice == tsum.MaxPrice ? String.Format("${0:N2}", tsum.MinPrice) :
                                      String.Format("${0:N2} - ${1:N2}", tsum.MinPrice, tsum.MaxPrice)) :
                        tsum.Free ? "FREE" : "DONATE";
        ev.UserFavorite = !String.IsNullOrEmpty(userId) && (favRepo.Get(filter: (f => (f.eventId == ev.EventId) && (f.UserID == userId)))).Any();
        eList.Add(ev);
      }
      return eList;
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

    private DateTime ConvertTimeToUtc(DateTime time, TimeZoneInfo tz)
    {
      return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(time, DateTimeKind.Unspecified), tz);
    }

    private EventTicketSummary GetTicketsSummary(IEnumerable<Ticket_Quantity_Detail> tickets, DateTime now)
    {
      EventTicketSummary res = new EventTicketSummary()
      {
        Paid = false,
        Free = false,
        Donate = false,
        MinPrice = 0,
        MaxPrice = 0,
        State = EventTicketState.RegistrationClosed
      };

      foreach(var tq in tickets)
      {
        DateTime ticketDate;
        DateTime startDate;
        DateTime endDate;
        DateTime.TryParse(tq.TQD_StartDate + " " + tq.TQD_StartTime, out ticketDate);
        DateTime.TryParse((tq.Ticket.Sale_Start_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Sale_Start_Time, out startDate);
        DateTime.TryParse((tq.Ticket.Sale_End_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Sale_End_Time, out endDate);

        bool hideTicket = ticketDate == null;
        if ((tq.Ticket.Hide_Ticket == "1") && !hideTicket)
        {
          DateTime hideStart = startDate;
          DateTime hideEnd = endDate;
          if (tq.Ticket.T_AutoSechduleType == "1")
          {
            DateTime.TryParse((tq.Ticket.Hide_Untill_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Hide_Untill_Time, out hideStart);
            DateTime.TryParse((tq.Ticket.Hide_After_Date ?? default(DateTime)).ToShortDateString() + " " + tq.Ticket.Hide_After_Time, out hideEnd);
            if (hideStart > startDate)
              startDate = hideStart;
            if ((hideEnd != default(DateTime)) && (hideEnd < endDate))
              endDate = hideEnd;
          }
        }

        bool soldOut = (tq.Ticket.T_Mark_SoldOut == "1") || ((tq.TQD_Remaining_Quantity ?? 0) <= 0);

        if ((now >= startDate) && ((now <= endDate) || (endDate == default(DateTime))))
          if (soldOut)
            res.State = res.State == EventTicketState.RegistrationClosed ? EventTicketState.SoldOut : res.State;
          else
            res.State = EventTicketState.Price;
        else if ((res.State != EventTicketState.Price) && (now < startDate))
          res.State = EventTicketState.NotAvailable;

        res.Paid = res.Paid || tq.Ticket.TicketTypeID == 2;
        res.Free = res.Free || tq.Ticket.TicketTypeID == 1;
        res.Donate = res.Donate || tq.Ticket.TicketTypeID == 3;
      }

      if (res.Paid)
      {
        res.MinPrice = tickets.Min(t => ((t.Ticket.Price ?? 0) - (t.Ticket.T_Discount ?? 0)));
        res.MaxPrice = tickets.Max(t => ((t.Ticket.Price ?? 0) - (t.Ticket.T_Discount ?? 0)));
      }

      return res;
    }

    public HomepageInfoViewModel GetHomepageInfo()
    {
      HomepageInfoViewModel res = new HomepageInfoViewModel();
      IRepository<City> cRepo = new GenericRepository<City>(_factory.ContextFactory);
      IRepository<EventType> etRepo = new GenericRepository<EventType>(_factory.ContextFactory);
      IRepository<HomepageWord> wRepo = new GenericRepository<HomepageWord>(_factory.ContextFactory);

      var fList = Directory.GetFiles(HostingEnvironment.MapPath("/Images/Video"), "*.gif");
      if ((fList != null) && fList.Any())
      {
        var rnd = new Random();
        int fileNum = rnd.Next(fList.Count());
        res.ImageUrl = "/Images/Video/" + Path.GetFileName(fList[fileNum]);
        string jpegFile = Path.ChangeExtension(fList[fileNum], ".jpg");
        if (File.Exists(jpegFile))
          res.StartImageUrl = "/Images/Video/" + Path.GetFileName(jpegFile);
        else
          res.StartImageUrl = "";
      }
      else
        res.ImageUrl = "/Images/AMaterial/RecordBackgroundFinal_1000.gif";

      var cities = cRepo.Get(filter: (c => c.IsOnHomepage), orderBy: (query => query.OrderBy(ct => ct.Position)));
      List<CityViewModel> resCities = new List<CityViewModel>();
      foreach (var city in cities)
        resCities.Add(_mapper.Map<CityViewModel>(city));
      res.Cities = resCities;

      var eTypes = etRepo.Get(filter: (et => et.IsOnHomepage), orderBy: (query => query.OrderBy(t => t.Position)));
      List<EventTypeViewModel> resETypes = new List<EventTypeViewModel>();
      foreach (var eType in eTypes)
        resETypes.Add(_mapper.Map<EventTypeViewModel>(eType));
      res.EventTypes = resETypes;

      res.KeyWords = wRepo.Get().Select(w => w.Word);

      return res;
    }

    public EventViewModel GetEventBySubDomain(string subDomain)
    {
        EventViewModel ev = new EventViewModel();
        IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);

        Event evDB = eRepo.Get(filter: (e => e.EventUrl == subDomain)).FirstOrDefault();

        _mapper.Map(evDB, ev);
        return ev;
    }

    private bool IsEventSubDomainExists(string subDomain, long eventId = 0)
    {
        if (String.IsNullOrWhiteSpace(subDomain))
          return false;

        EventViewModel ev = new EventViewModel();
        IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);

        var evDB = eRepo.Get(filter: (e => e.EventUrl == subDomain && (eventId == 0 ? true : e.EventID != eventId)));

        return evDB.Count() > 0;
    }
  }
}
