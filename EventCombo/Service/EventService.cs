using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.DAL;
using AutoMapper;
using System.Data.SqlClient;
using EventCombo.Utils;


namespace EventCombo.Service
{
  public class EventService : IEventService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private IImageService _iservice;

    public EventService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
      _iservice = new ImageService();
    }

    private void LoadEventDictionaries(EventViewModel ev)
    {
      IRepository<EventType> etRepo = new GenericRepository<EventType>(_factory.ContextFactory);
      IRepository<EventCategory> ecRepo = new GenericRepository<EventCategory>(_factory.ContextFactory);
      IRepository<Organizer_Master> orgRepo = new GenericRepository<Organizer_Master>(_factory.ContextFactory);
      IRepository<TimeZoneDetail> tzRepo = new GenericRepository<TimeZoneDetail>(_factory.ContextFactory);
      IRepository<Fee_Structure> feeRepo = new GenericRepository<Fee_Structure>(_factory.ContextFactory);
      IRepository<ECImage> iRepo = new GenericRepository<ECImage>(_factory.ContextFactory);

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
          {
            var iDb = iRepo.GetByID((org.ECImageId ?? 0));
            if (iDb != null)
              _mapper.Map(iDb, orgVM.Image);
          }
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

    private long SaveAddress(EventViewModel ev, IUnitOfWork uow)
    {
      IRepository<Address> aRepo = new GenericRepository<Address>(_factory.ContextFactory);
      var address = aRepo.Get(filter: (a => a.EventId == ev.EventID)).FirstOrDefault();
      if (address == null)
        address = new Address();
      address.EventId = ev.EventID;
      address.ConsolidateAddress = ev.Address;
      address.UserId = ev.UserID;
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
      return address.AddressID;
    }

    private void SaveMultiplyDates(EventViewModel ev, IUnitOfWork uow)
    {
      IRepository<MultipleEvent> multiRepo = new GenericRepository<MultipleEvent>(_factory.ContextFactory);
      IRepository<EventVenue> vRepo = new GenericRepository<EventVenue>(_factory.ContextFactory);
      IRepository<TimeZoneDetail> tzRepo = new GenericRepository<TimeZoneDetail>(_factory.ContextFactory);

      var single = vRepo.Get(filter: (e => e.EventID == ev.EventID)).ToList();
      foreach (var se in single)
        vRepo.Delete(se);

      MultipleEvent me = multiRepo.Get(filter: (m => m.EventID == ev.EventID)).FirstOrDefault();
      if (me == null)
        me = new MultipleEvent();
      me.EventID = ev.EventID;
      me.Frequency = ev.DateInfo.Frequency.ToString();

      int tzId;
      Int32.TryParse(ev.TimeZone, out tzId);
      TimeZoneDetail tz = tzRepo.GetByID(tzId);
      if (tz != null)
      {
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(tz.TimeZone);
        ev.DateInfo.StartDateTime = TimeZoneInfo.ConvertTimeToUtc(ev.DateInfo.StartDateTime, userTimeZone);
        ev.DateInfo.EndDateTime = TimeZoneInfo.ConvertTimeToUtc(ev.DateInfo.EndDateTime, userTimeZone); ;
      }
      me.M_Startfrom = ev.DateInfo.StartDateTime;
      me.M_StartTo = ev.DateInfo.EndDateTime;
      if (ev.DateInfo.Frequency == ScheduleFrequency.Weekly)
        me.WeeklyDay = String.Join(",", ev.DateInfo.Weekdays);
      me.StartingFrom = ev.DateInfo.StartDateTime.ToString("MM/dd/yyyy"); ;
      me.StartTime = ev.DateInfo.StartDateTime.ToString("hh:mm tt");
      me.StartingTo = ev.DateInfo.EndDateTime.ToString("MM/dd/yyyy");
      me.EndTime = ev.DateInfo.EndDateTime.ToString("hh:mm tt");
      if (me.MultipleEventID == 0)
        multiRepo.Insert(me);
      uow.Context.SaveChanges();
    }

    private void SaveSingleDate(EventViewModel ev, long addressId, IUnitOfWork uow)
    {
      IRepository<MultipleEvent> multiRepo = new GenericRepository<MultipleEvent>(_factory.ContextFactory);
      IRepository<EventVenue> vRepo = new GenericRepository<EventVenue>(_factory.ContextFactory);
      IRepository<TimeZoneDetail> tzRepo = new GenericRepository<TimeZoneDetail>(_factory.ContextFactory);

      var multi = multiRepo.Get(filter: (m => m.EventID == ev.EventID)).ToList();
      foreach (var me in multi)
        multiRepo.Delete(me);

      EventVenue se = vRepo.Get(filter: (e => e.EventID == ev.EventID)).FirstOrDefault();
      if (se == null)
        se = new EventVenue();
      se.EventID = ev.EventID;


      int tzId;
      Int32.TryParse(ev.TimeZone, out tzId);
      TimeZoneDetail tz = tzRepo.GetByID(tzId);
      if (tz != null)
      {
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(tz.TimeZone);
        ev.DateInfo.StartDateTime = TimeZoneInfo.ConvertTimeToUtc(ev.DateInfo.StartDateTime, userTimeZone);
        ev.DateInfo.EndDateTime = TimeZoneInfo.ConvertTimeToUtc(ev.DateInfo.EndDateTime, userTimeZone); ;
      }
      se.E_Startdate = ev.DateInfo.StartDateTime;
      se.E_Enddate = ev.DateInfo.EndDateTime;
      se.EventStartDate = ev.DateInfo.StartDateTime.ToString("MM/dd/yyyy");
      se.EventStartTime = ev.DateInfo.StartDateTime.ToString("hh:mm tt");
      se.EventEndDate = ev.DateInfo.EndDateTime.ToString("MM/dd/yyyy");
      se.EventEndTime = ev.DateInfo.EndDateTime.ToString("hh:mm tt");
      se.AddressId = addressId;
      if (se.EventVenueID == 0)
        vRepo.Insert(se);
      uow.Context.SaveChanges();
    }

    private void SaveTickets(EventViewModel ev, IUnitOfWork uow)
    {
      IRepository<Ticket> tRepo = new GenericRepository<Ticket>(_factory.ContextFactory);
      IRepository<TimeZoneDetail> tzRepo = new GenericRepository<TimeZoneDetail>(_factory.ContextFactory);
      foreach (var ticket in ev.TicketList)
      {
        Ticket tDB = null;
        if (ticket.T_Id != 0)
          tDB = tRepo.GetByID(ticket.T_Id);
        if (tDB == null)
          tDB = new Ticket();

        ticket.E_Id = ev.EventID;
        _mapper.Map(ticket, tDB);

        int tzId;
        Int32.TryParse(ev.TimeZone, out tzId);
        TimeZoneDetail tz = tzRepo.GetByID(tzId);
        DateTime? saleStart = ticket.Sale_Start_Date;
        DateTime? saleEnd = ticket.Sale_End_Date;
        DateTime? hideUntil = ticket.Hide_Untill_Date;
        DateTime? hideAfter = ticket.Hide_After_Date;
        if (tz != null)
        {
          TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(tz.TimeZone);
          saleStart = saleStart == null ? null : (DateTime?)TimeZoneInfo.ConvertTimeToUtc(saleStart.Value, userTimeZone);
          saleEnd = saleEnd == null ? null : (DateTime?)TimeZoneInfo.ConvertTimeToUtc(saleEnd.Value, userTimeZone);
          hideUntil = hideUntil == null ? null : (DateTime?)TimeZoneInfo.ConvertTimeToUtc(hideUntil.Value, userTimeZone);
          hideAfter = hideAfter == null ? null : (DateTime?)TimeZoneInfo.ConvertTimeToUtc(hideAfter.Value, userTimeZone);
        }

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

    private void SaveVarCharges(EventViewModel ev, IUnitOfWork uow)
    {
      IRepository<Event_VariableDesc> vcRepo = new GenericRepository<Event_VariableDesc>(_factory.ContextFactory);

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
          oDB = orgRepo.Get(filter: (o => ((o.Orgnizer_Name == org.Orgnizer_Name) && (o.Organizer_Email == org.Organizer_Email)))).FirstOrDefault();
          if (oDB == null)
            oDB = new Organizer_Master();
        }

        _mapper.Map(org, oDB);

        org.Image.MapPath = mapPath;
        if ((org.Image != null) && (org.Image.ImageType == 0) && (!String.IsNullOrWhiteSpace(org.Image.Filename)))
        {
          ImageViewModel tempImage = _iservice.CopyImage(org.Image, 3);
          org.Organizer_Image = tempImage.Filename;
          org.Imagepath = tempImage.Filename;
        }
        if ((org.Image != null) && (org.Image.ImageType == 0) && (!String.IsNullOrWhiteSpace(org.Image.Filename)))
          oDB.ECImageId = _iservice.UpdateECImage(oDB.ECImageId ?? 0, org.Image, uow);
        if (((oDB.ECImageId ?? 0) != 0) && (org.Image != null) && (String.IsNullOrWhiteSpace(org.Image.Filename)))
        {
          _iservice.DeleteECImage(oDB.ECImageId ?? 0, uow, mapPath);
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

      var dbImages = eiRepo.Get(filter: (img => img.EventID == ev.EventID)).ToList();
      foreach (var image in dbImages)
      {
        ImageViewModel cVM = ev.EventImages.Where(img => img.Id == image.EventImageID).FirstOrDefault();
        if (cVM == null)
          eiRepo.Delete(image);
        else
        {
          if (cVM.ImageType == 0)
          {
            _iservice.DeleteImage(new ImageViewModel() { ImageType = 2, Filename = image.EventImageUrl, MapPath = mapPath });
            cVM.MapPath = mapPath;
            var newVM = _iservice.CopyImage(cVM, 2, true);
            image.EventImageUrl = newVM.Filename;
            image.ImageType = newVM.ContentType;
            cVM.Filename = newVM.Filename;
            cVM.ImageType = 2;
          }
        }
      }

      uow.Context.SaveChanges();
      foreach (var cVM in ev.EventImages.Where(i => ((i.Id == 0) && (i.ImageType == 0) && (!String.IsNullOrWhiteSpace(i.Filename)))))
      {
        cVM.MapPath = mapPath;
        var newVM = _iservice.CopyImage(cVM, 2, true);
        EventImage image = new EventImage() { EventID = ev.EventID, EventImageUrl = newVM.Filename, ImageType = newVM.ContentType };
        eiRepo.Insert(image);
        uow.Context.SaveChanges();
        cVM.Id = image.EventImageID;
        cVM.Filename = newVM.Filename;
        cVM.ImageType = 2;
      }

    }


    public void SaveEvent(EventViewModel ev, Func<string, string> mapPath)
    {
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);

          Event evDB = null;
          if (ev.EventID != 0)
            evDB = eRepo.GetByID(ev.EventID);
          if (evDB == null)
            evDB = new Event();

          if (ev.EventID == 0)
            ev.CreateDate = DateTime.Now;
          else
            ev.ModifyDate = DateTime.Now;

          _mapper.Map(ev, evDB);
          if (ev.OnlineEvent)
            evDB.AddressStatus = "Online";
          else
            evDB.AddressStatus = "Single";
          if (evDB.EventID == 0)
            eRepo.Insert(evDB);
          uow.Context.SaveChanges();
          ev.EventID = evDB.EventID;

          ev.BGImage.MapPath = mapPath;
          if ((ev.BGImage != null) && (ev.BGImage.ImageType == 0) && (!String.IsNullOrWhiteSpace(ev.BGImage.Filename)))
            evDB.ECBackgroundId = _iservice.UpdateECImage(evDB.ECBackgroundId ?? 0, ev.BGImage, uow);
          if (((evDB.ECBackgroundId ?? 0) != 0) && (ev.BGImage != null) && (String.IsNullOrWhiteSpace(ev.BGImage.Filename)))
          {
            _iservice.DeleteECImage(evDB.ECBackgroundId ?? 0, uow, mapPath);
            evDB.ECBackgroundId = 0;
          }
          ev.ECBackgroundId = evDB.ECBackgroundId;

          SaveImages(ev, uow, mapPath);

          var addressID = SaveAddress(ev, uow);

          if (ev.DateInfo.Frequency == ScheduleFrequency.Single)
            SaveSingleDate(ev, addressID, uow);
          else
            SaveMultiplyDates(ev, uow);

          SaveOrganizers(ev, uow, mapPath);
          SaveTickets(ev, uow);
          SaveVarCharges(ev, uow);

          var param1 = new SqlParameter("@EventId", ev.EventID);
          var param2 = new SqlParameter("@UserId", ev.UserID);
          var res = uow.Context.Database.ExecuteSqlCommand("EXEC PublishEvent @EventId, @UserId", param1, param2);
          uow.Context.SaveChanges();

          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception during SaveEvent.", ex);
        }
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
          EventId = 0,
          RecordTypeId = 1,
          EventTitle = et.EventType1
        }));

      evList.AddRange(ecRepo.Get(filter: (ec => ec.EventCategory1.Contains(searchStr))).Select(ec => new EventSearchViewModel()
      {
        EventId = 0,
        RecordTypeId = 2,
        EventTitle = ec.EventCategory1
      }));

      return evList.OrderBy(e => e.EventTitle);
    }


    public EventViewModel GetEventById(int id)
    {
      throw new NotImplementedException();
    }


    public EventInfoViewModel GetEventInfo(long eventId, Func<string, string> UrlFunc)
    {
      EventInfoViewModel evi = new EventInfoViewModel();
      evi.EventId = eventId;

      UpdateEventInfo(evi, UrlFunc);

      return evi;
    }

    private string GetECImageUrl(long ImageId, Func<string, string> UrlFunc)
    {
      IRepository<ECImage> iRepo = new GenericRepository<ECImage>(_factory.ContextFactory);
      var dbImage = iRepo.GetByID(ImageId);
      if (dbImage != null)
      {
        ImageViewModel ecImage = new ImageViewModel()
        {
          Id = dbImage.ECImageId,
          ImageType = 1,
          Filename = dbImage.ImagePath,
          UrlPath = UrlFunc
        };
        return ecImage.ImageUrl;
      }
      else
        return "";
    }

    private string GetEventImageUrl(EventImage dbImage, Func<string, string> UrlFunc)
    {
      if (dbImage != null)
      {
        ImageViewModel ecImage = new ImageViewModel()
        {
          Id = dbImage.EventImageID,
          ImageType = 1,
          Filename = dbImage.EventImageUrl,
          UrlPath = UrlFunc
        };
        return ecImage.ImageUrl;
      }
      else
        return "";
    }

    public void UpdateEventInfo(EventInfoViewModel evi, Func<string, string> UrlFunc)
    {
      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      IRepository<EventSubCategory> escRepo = new GenericRepository<EventSubCategory>(_factory.ContextFactory);
      IRepository<TimeZoneDetail> tzRepo = new GenericRepository<TimeZoneDetail>(_factory.ContextFactory);
      IRepository<EventImage> iRepo = new GenericRepository<EventImage>(_factory.ContextFactory);
      IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);

      Event ev = eRepo.Get(filter: (e => e.EventID == evi.EventId)).SingleOrDefault();
      if (ev == null)
        throw new ArgumentException(String.Format("Event {0} not found.", evi.EventId));

      evi.ErrorEvent = false;
      evi.ErrorMessages = new List<string>();
      evi.EventTitle = ev.EventTitle;
      evi.EventDescription = ev.EventDescription;
      evi.OnlineEvent = ev.AddressStatus == "Online";
      evi.EventTypeId = ev.EventTypeID;
      evi.EventType = ev.EventType.EventType1;
      evi.EventCategoryId = ev.EventCategoryID;
      evi.EventCategory = ev.EventCategory.EventCategory1;
      evi.DisplayStartTime = ev.DisplayStartTime == "Y";
      evi.DisplayEndTime = ev.DisplayEndTime == "Y";
      evi.BackgroundColor = ev.BackgroundColor;
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
      evi.ShowRemainingTickets = ev.Ticket_showremain == "1";

      TimeZoneInfo tz = null;
      long tzId;
      Int64.TryParse(ev.TimeZone, out tzId);
      TimeZoneDetail tzDet = tzRepo.GetByID(tzId);
      if (tzDet != null)
        tz = TimeZoneInfo.FindSystemTimeZoneById(tzDet.TimeZone);
      else
        tz = TimeZoneInfo.Utc;
      if ((ev.DisplayTimeZone == "Y") && (tzDet != null))
        evi.TimeZone = tzDet.TimeZone_Name;

      var multiDate = ev.MultipleEvents.FirstOrDefault();
      var singleDate = ev.EventVenues.SingleOrDefault();
      evi.DateInfo = new EventDateViewModel()
      {
        Frequency = multiDate == null ? ScheduleFrequency.Single : (ScheduleFrequency)Enum.Parse(typeof(ScheduleFrequency), multiDate.Frequency, true),
        IsNewDate = false,
        StartDateTime = (multiDate == null ? singleDate.E_Startdate : multiDate.M_Startfrom) ?? DateTime.MinValue,
        EndDateTime = (multiDate == null ? singleDate.E_Startdate : multiDate.M_Startfrom) ?? DateTime.MinValue,
      };
      if (evi.DateInfo.Frequency == ScheduleFrequency.Weekly)
        evi.DateInfo.Weekdays.AddRange(multiDate.WeeklyDay.Split(',').Select(s => s.Trim()).ToList());
      if ((evi.DateInfo.StartDateTime > DateTime.MinValue) && (tz != null))
        evi.DateInfo.StartDateTime = TimeZoneInfo.ConvertTimeFromUtc(evi.DateInfo.StartDateTime, tz);
      if ((evi.DateInfo.EndDateTime > DateTime.MinValue) && (tz != null))
        evi.DateInfo.EndDateTime = TimeZoneInfo.ConvertTimeFromUtc(evi.DateInfo.EndDateTime, tz);

      if ((ev.ECBackgroundId ?? 0) > 0)
        evi.BackgroundUrl = GetECImageUrl(ev.ECBackgroundId ?? 0, UrlFunc);

      var dbImages = iRepo.Get(filter: (im => im.EventID == evi.EventId), orderBy: (query => query.OrderBy(im => im.EventImageID)));
      var images = new List<string>();
      if (dbImages.Count() > 0)
      {
        evi.ImageUrl = GetEventImageUrl(dbImages.First(), UrlFunc);
        foreach (var eventImage in dbImages.Skip(1))
          images.Add(GetEventImageUrl(eventImage, UrlFunc));
      }
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
        evi.Organizer.ImageUrl = GetECImageUrl(org.Organizer_Master.ECImageId ?? 0, UrlFunc);
      }

      var tickets = new List<TicketInfoViewModel>();
      var tqDB = tqdRepo.Get(filter: (tqd => (tqd.TQD_Event_Id == evi.EventId)));
      evi.ShowMinMax = false;
      evi.RemainingTickets = 0;
      foreach (var tq in tqDB)
      {
        DateTime ticketDate;
        DateTime.TryParse(tq.TQD_StartDate + " " + tq.TQD_StartTime, out ticketDate);
        if (ticketDate != null)
          ticketDate = TimeZoneInfo.ConvertTimeFromUtc(ticketDate, tz);
        DateTime utcNow = DateTime.UtcNow;
        bool hideTicket = ticketDate == null;
        if ((tq.Ticket.Hide_Ticket == "1") && !hideTicket)
        {
          DateTime hideStart;
          DateTime hideEnd;
          if (tq.Ticket.T_AutoSechduleType == "1")
          {
            DateTime.TryParse((tq.Ticket.Hide_Untill_Date ?? DateTime.UtcNow).ToShortDateString() + " " + tq.Ticket.Hide_Untill_Time, out hideStart);
            DateTime.TryParse((tq.Ticket.Hide_After_Date ?? DateTime.UtcNow).ToShortDateString() + " " + tq.Ticket.Hide_After_Time, out hideEnd);
          }
          else
          {
            DateTime.TryParse((tq.Ticket.Sale_Start_Date ?? DateTime.UtcNow).ToShortDateString() + " " + tq.Ticket.Sale_Start_Time, out hideStart);
            DateTime.TryParse((tq.Ticket.Sale_End_Date ?? DateTime.UtcNow).ToShortDateString() + " " + tq.Ticket.Sale_End_Time, out hideEnd);
          }
          hideTicket = ((hideStart != null) && (hideStart >= utcNow)) || ((hideEnd != null) && (hideEnd < utcNow));
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
            VenueName = tq.Address.VenueName,
            ShowFee = tq.Ticket.Fees_Type != "1",
            Fee = tq.Ticket.Fees_Type != "1" ? (tq.Ticket.TotalPrice ?? 0) - (tq.Ticket.Price ?? 0) + (tq.Ticket.T_Discount ?? 0) : 0,
            ShowRemaining = tq.Ticket.T_Displayremaining == "1",
            RemainingQuantity = tq.Ticket.T_Displayremaining == "1" ? tq.TQD_Remaining_Quantity ?? 0 : 0
          };
          if (tiVM.TicketTypeId != 3)
          {
            if (!evi.ShowMinMax)
            {
              evi.ShowMinMax = true;
              evi.MinTicketPrice = tiVM.TotalPrice;
              evi.MaxTicketPrice = tiVM.TotalPrice;
            }
            else
            {
              evi.MinTicketPrice = evi.MinTicketPrice > tiVM.TotalPrice ? tiVM.TotalPrice : evi.MinTicketPrice;
              evi.MaxTicketPrice = evi.MaxTicketPrice < tiVM.TotalPrice ? tiVM.TotalPrice : evi.MaxTicketPrice;
            }
          }
          if (evi.ShowRemainingTickets)
            evi.RemainingTickets += tq.TQD_Remaining_Quantity ?? 0;
          tickets.Add(tiVM);
        }
      }
      evi.Tickets = tickets;
    }

    public void ValidateEventInfo(EventInfoViewModel ev)
    {
      throw new NotImplementedException();
    }
  }
}