﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Models
{

  public class EventTypeViewModel
  {
    public long EventTypeId { get; set; }
    public string EventType { get; set; }
    public string EventHide { get; set; }
  }

  public class EventCategoryViewModel
  {
    public long EventCategoryId { get; set; }
    public string EventCategory { get; set; }

    private List<EventSubCategoryViewModel> _subCategories = new List<EventSubCategoryViewModel>();
    public List<EventSubCategoryViewModel> SubCategories
    {
      get { return _subCategories; }
      set { _subCategories = value; }
    }
  }

  public class EventSubCategoryViewModel
  {
    public long EventSubCategoryId { get; set; }
    public string EventSubCategory { get; set; }
  }

  public class TimeZoneViewModel
  {
    public string TimeZoneName { get; set; }
    public string TimeZone { get; set; }
    public int TimeZoneId { get; set; }
    public int TimeZoneOrder { get; set; }
    public int Offset { get; set; }
  }

  public class PredefinedTimeViewModel
  {
    public string TimeString { get; set; }
    public int Minutes { get; set; }
  }

  public enum ScheduleFrequency { Single, Monthly, Weekly, Daily }

  public class EventDateViewModel
  {
    public EventDateViewModel()
    {
      _timeList = new List<PredefinedTimeViewModel>();
      _weekdays = new List<DayOfWeek>();
    }

    public bool IsNewDate { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ScheduleFrequency Frequency { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    private List<DayOfWeek> _weekdays;
    public List<DayOfWeek> Weekdays
    {
      get { return _weekdays; }
      set { _weekdays = value; }
    }

    private List<PredefinedTimeViewModel> _timeList;
    public List<PredefinedTimeViewModel> TimeList
    {
      get { return _timeList; }
      set { _timeList = value; }
    }
  }

  public class OrganizerViewModel
  {
    public long OrgnizerId { get; set; }
    public string Orgnizer_Name { get; set; }
    public string Organizer_Desc { get; set; }
    public string Organizer_FBLink { get; set; }
    public string Organizer_Twitter { get; set; }
    public string Organizer_Linkedin { get; set; }
    public string UserId { get; set; }
    [JsonIgnore]
    public string Organizer_Image { get; set; }
    [JsonIgnore]
    public string Organizer_Address1 { get; set; }
    [JsonIgnore]
    public string Organizer_Address2 { get; set; }
    [JsonIgnore]
    public string Organizer_City { get; set; }
    [JsonIgnore]
    public string Organizer_State { get; set; }
    [JsonIgnore]
    public string Organizer_Zipcode { get; set; }
    [JsonIgnore]
    public byte? Organizer_CountryId { get; set; }
    public string Organizer_Email { get; set; }
    public string Organizer_Phoneno { get; set; }
    [JsonIgnore]
    public string Organizer_Websiteurl { get; set; }
    public string Organizer_Status { get; set; }
    [JsonIgnore]
    public string contenttype { get; set; }
    public string Imagepath { get; set; }
    public long? ECImageId { get; set; }
    public int InternalId { get; set; }
    public ECImageViewModel Image { get; set; }
    public bool IncludeSocialLinks { get; set; }
  }

  public class VariableChargesViewModel
  {
    public long VariableId { get; set; }
    public long? EventId { get; set; }
    public string VariableDesc { get; set; }
    public decimal? Price { get; set; }
    public bool Optional { get; set; }
  }

  public class FeeStructureViewModel
  {
    public long FS_Id { get; set; }
    public string FS_Type { get; set; }
    public decimal? FS_Amount { get; set; }
    public decimal? FS_Percentage { get; set; }
    public string FS_Apply { get; set; }
  }

  public class EventViewModel : IBaseViewModel
  {
    public EventViewModel()
    {
      _eventTypeList = new List<EventTypeViewModel>();
      _eventCategoryList = new List<EventCategoryViewModel>();
      _timeZones = new List<TimeZoneViewModel>();
      _dateInfo = new EventDateViewModel();
      _organizerList = new List<OrganizerViewModel>();
      _variableChargesList = new List<VariableChargesViewModel>();
      _ticketList = new List<TicketViewModel>();
      _feeStruct = new FeeStructureViewModel();
      _errorMessages = new List<string>();
      _eventImages = new List<ECImageViewModel>();
    }

    // IBaseViewModel interface implementation
    public string BaseTitle { get; set; }
    public string BaseUserId { get; set; }
    public string BaseUserName { get; set; }
    public string BaseUserEmail { get; set; }


    public long EventID { get; set; }
    public long EventTypeID { get; set; }
    public long EventCategoryID { get; set; }
    public long EventSubCategoryID { get; set; }
    public string UserID { get; set; }
    public string EventTitle { get; set; }
    public string DisplayStartTime { get; set; }
    public string DisplayEndTime { get; set; }
    public string DisplayTimeZone { get; set; }
    public string EventDescription { get; set; }
    public string EventPrivacy { get; set; }
    public string Private_ShareOnFB { get; set; }
    public string Private_GuestOnly { get; set; }
    public string Private_Password { get; set; }
    public string EventUrl { get; set; }
    public string PublishOnFB { get; set; }
    public string EventStatus { get; set; }
    public DateTime? AddedOn { get; set; }
    public DateTime? UpdateOn { get; set; }
    public string IsMultipleEvent { get; set; }
    public string TimeZone { get; set; }
    public string FBUrl { get; set; }
    public string TwitterUrl { get; set; }
    public string AddressStatus { get; set; }
    public long? LastLocationAddress { get; set; }
    public string EnableFBDiscussion { get; set; }
    public int? Feature { get; set; }
    public string Ticket_DAdress { get; set; }
    public string Ticket_showremain { get; set; }
    public string Ticket_showvariable { get; set; }
    public string Ticket_variabledesc { get; set; }
    public string Ticket_variabletype { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string ShowMap { get; set; }
    public long? Parent_EventID { get; set; }
    public string EventCancel { get; set; }
    public DateTime? FeatureUpdateDate { get; set; }
    public long? ECBackgroundId { get; set; }
    public string BackgroundColor { get; set; }
    public string VenueName { get; set; }
    public string Address { get; set; }
    public bool OnlineEvent { get; set; }
    public bool ErrorEvent { get; set; }
    public long? ECImageId { get; set; }
    public long AddressId { get; set; }
    public string EventPath { get; set; }
    public bool IsAdmin { get; set; }

    private List<string> _errorMessages;
    public List<string> ErrorMessages
    {
      get { return _errorMessages; }
      set { _errorMessages = value; }
    }

    private List<EventTypeViewModel> _eventTypeList;
    public List<EventTypeViewModel> EventTypeList
    {
      get { return _eventTypeList; }
      set { _eventTypeList = value; }
    }
    public EventTypeViewModel CurrentEventType { get { return EventTypeList.Where(et => et.EventTypeId == EventTypeID).FirstOrDefault(); } }

    private List<EventCategoryViewModel> _eventCategoryList;
    public List<EventCategoryViewModel> EventCategoryList
    {
      get { return _eventCategoryList; }
      set { _eventCategoryList = value; }
    }
    public EventCategoryViewModel CurrentEventCategory { get { return EventCategoryList.Where(ec => ec.EventCategoryId == EventCategoryID).FirstOrDefault(); } }
    public EventSubCategoryViewModel CurrentEventSubCategory
    {
      get
      {
        var cCat = CurrentEventCategory;
        if (cCat != null)
          return cCat.SubCategories.Where(sc => sc.EventSubCategoryId == EventSubCategoryID).FirstOrDefault();
        else
          return null;
      }
    }

    private List<TimeZoneViewModel> _timeZones;
    public List<TimeZoneViewModel> TimeZones
    {
      get { return _timeZones; }
      set { _timeZones = value; }
    }

    private EventDateViewModel _dateInfo;
    public EventDateViewModel DateInfo
    {
      get { return _dateInfo; }
      set { _dateInfo = value; }
    }

    public long OrganizerId { get; set; }
    public int InternalOrganizerId { get; set; }
    private List<OrganizerViewModel> _organizerList;
    public List<OrganizerViewModel> OrganizerList
    {
      get { return _organizerList; }
      set { _organizerList = value; }
    }
    public OrganizerViewModel CurrentOrganizer { get { return OrganizerList.Where(o => o.OrgnizerId == OrganizerId).FirstOrDefault(); } }
    public OrganizerViewModel CurrentInternalOrganizer { get { return OrganizerList.Where(o => o.InternalId == InternalOrganizerId).FirstOrDefault(); } }

    private List<VariableChargesViewModel> _variableChargesList;
    public List<VariableChargesViewModel> VariableChargesList
    {
      get { return _variableChargesList; }
      set { _variableChargesList = value; }
    }

    private List<TicketViewModel> _ticketList;
    public List<TicketViewModel> TicketList
    {
      get { return _ticketList; }
      set { _ticketList = value; }
    }

    private FeeStructureViewModel _feeStruct;
    public FeeStructureViewModel FeeStruct
    {
      get { return _feeStruct; }
      set { _feeStruct = value; }
    }

    public ECImageViewModel BGImage { get; set; }

    public ECImageViewModel ECImage { get; set; }

    private List<ECImageViewModel> _eventImages;
    public List<ECImageViewModel> EventImages
    {
      get { return _eventImages; }
      set { _eventImages = value; }
    }
  }

  public class EventSearchViewModel
  {
    public long EventId { get; set; }
    public byte RecordTypeId { get; set; } // 0 - Title, 1 - Type, 2 - Category, 3 - SubCategory
    public string EventTitle { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
  }

  public class OrganizerInfoViewModel
  {
    public long OrganizerId { get; set; }
    public string OrganizerName { get; set; }
    public string ImageUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public string FBLink { get; set; }
    public string LinkdeInLink { get; set; }
    public string TwitterLink { get; set; }
  }

  public class TicketInfoViewModel
  {
    public long TicketId { get; set; }
    public long TQDId { get; set; }
    public string TicketName { get; set; }
    public string VenueName { get; set; }
    public string TicketDescription { get; set; }
    public DateTime StartDate { get; set; }
    public long TicketTypeId { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public long Minimum { get; set; }
    public long Maximum { get; set; }
    public decimal Fee { get; set; }
    public bool ShowFee { get; set; }
    public decimal Amount { get; set; }
    public bool SoldOut { get; set; }
    public bool ShowRemaining { get; set; }
    public long RemainingQuantity { get; set; }
    public decimal TotalPrice { get; set; }
    public bool Available { get; set; }
    public string DateInfoString1 { get; set; }
    public string DateInfoString2 { get; set; }
  }

  public class ShortEventInfoViewModel
  {
    public long EventId { get; set; }
    public string EventTitle { get; set; }
    public string EventShortDesc { get; set; }
    public string VenueName { get; set; }
    public string Address { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string EventType { get; set; }
    public string EventCategory { get; set; }
    public string EventSubCategory { get; set; }
    public long FavoriteCount { get; set; }
    public bool UserFavorite { get; set; }
    public long VoteCount { get; set; }
    public bool UserVote { get; set; }
    public string ImageUrl { get; set; }
    public string ImageAlt { get; set; }
    public string EventPath { get; set; }
    public string PriceRange { get; set; }
    public string EventDates { get; set; }
  }

  public class EventInfoViewModel : IBaseViewModel, IOpenGraphProtocol
  {
    // IBaseViewModel interface implementation
    public string BaseTitle { get; set; }
    public string BaseUserId { get; set; }
    public string BaseUserName { get; set; }
    public string BaseUserEmail { get; set; }
    // IOpenGraphProtocol interface implementation
    public string OGPTitle { get; set; }
    public string OGPType { get; set; }
    public string OGPUrl { get; set; }
    public string OGPImage { get; set; }
    public string OGPDescription { get; set; }

    public long EventId { get; set; }
    public string EventTitle { get; set; }
    public string EventDescription { get; set; }
    public string EventShortDesc { get; set; }
    public string VenueName { get; set; }
    public string Address { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public bool OnlineEvent { get; set; }
    public string TimeZone { get; set; }
    public long EventTypeId { get; set; }
    public string EventType { get; set; }
    public long EventCategoryId { get; set; }
    public string EventCategory { get; set; }
    public long EventSubCategoryId { get; set; }
    public string EventSubCategory { get; set; }
    public long FavoriteCount { get; set; }
    public bool UserFavorite { get; set; }
    public long VoteCount { get; set; }
    public bool UserVote { get; set; }
    public byte SingleTicketType { get; set; }
    public byte EventStatus { get; set; }
    public string BackgroundUrl { get; set; }
    public string BackgroundColor { get; set; }
    public string ImageUrl { get; set; }
    public string ImageAlt { get; set; }
    public bool ShowRemainingTickets { get; set; }
    public long RemainingTickets { get; set; }
    public string ShowVariables { get; set; }
    public string EventPrivacy { get; set; }
    public bool UsePrivatePassword { get; set; }
    public string EnableFBDiscussion { get; set; }
    public bool DisplayStartTime { get; set; }
    public bool DisplayEndTime { get; set; }
    public string EventUrl { get; set; }
    public string ButtonText { get; set; }
    public string CheckoutText { get; set; }
    public string PriceRange { get; set; }
    public bool AllowPrivateShare { get; set; }


    public OrganizerInfoViewModel Organizer { get; set; }
    public IEnumerable<string> ImagesUrl { get; set; }
    public IEnumerable<TicketInfoViewModel> Tickets { get; set; }
    public EventDateViewModel DateInfo {get; set; }

    public bool ErrorEvent { get; set; }
    private List<string> _errorMessages;
    public List<string> ErrorMessages
    {
      get { return _errorMessages; }
      set { _errorMessages = value; }
    }
  }

  public class IncrementResultViewModel
  {
    public bool Processed { get; set; }
    public bool AlreadyProcessed { get; set; }
    public long Count { get; set; }
  }

  public enum EventTicketState { Price = 0, NotAvailable = 1, SoldOut = 2, RegistrationClosed = 3 }

  public class EventTicketSummary
  {
    public EventTicketState State { get; set; }
    public bool Paid { get; set; }
    public bool Free { get; set; }
    public bool Donate { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
  }

  public class CityViewModel
  {
    public long CityID { get; set; }
    public string CityName { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string ShortName { get; set; }
    public string StateId { get; set; }
  }

  public class HomepageInfoViewModel : IBaseViewModel
  {
    // IBaseViewModel interface implementation
    public string BaseTitle { get; set; }
    public string BaseUserId { get; set; }
    public string BaseUserName { get; set; }
    public string BaseUserEmail { get; set; }


    public string ImageUrl { get; set; }
    public string StartImageUrl { get; set; }
    public IEnumerable<string> KeyWords { get; set; }
    public IEnumerable<CityViewModel> Cities { get; set; }
    public IEnumerable<EventTypeViewModel> EventTypes { get; set; }
  }

  public class PrivateEventRequest
  {
    public long EventId { get; set; }
    public string Password { get; set; }
    public string InviteCode { get; set; }
    public bool PasswordValid { get; set; }
    public bool InviteValid { get; set; }
  }

  public class OrganizerEvnetViewModel
  {
      public long EventId { get; set; }
      public string EventTitle { get; set; }
      public string EventImg { get; set; }
      public string EventPrice { get; set; }
      public string EventDes { get; set; }
      public string EventType { get; set; }
      public string EventCat { get; set; }
      public DateTime EventDate { get; set; }
      private Position _Position = new Position();
      public Position Position
      {
          get
          {
              return _Position;
          }
          set
          {
              _Position = value;
          }
      }
      public string MarkerIcon { get; set; }
      public int MarkerWidth { get; set; }
      public int MarkerHeight { get; set; }
      public string Venue { get; set; }
      public string DateOfEvent { get; set; }
      public string EventPath { get; set; }
      public string EventPrivacy { get; set; }
      public string EventLike { get; set; }
  }

  public class Position
  {
      public decimal lat { get; set; }
      public decimal lng { get; set; }
  }
}
