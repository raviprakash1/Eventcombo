﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventCombo.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EventComboEntities : DbContext
    {
        public EventComboEntities()
            : base("name=EventComboEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BillingAddress> BillingAddresses { get; set; }
        public virtual DbSet<CardDetail> CardDetails { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<Email_Tag> Email_Tag { get; set; }
        public virtual DbSet<Email_Template> Email_Template { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Event_OrganizerMessages> Event_OrganizerMessages { get; set; }
        public virtual DbSet<Event_VariableDesc> Event_VariableDesc { get; set; }
        public virtual DbSet<EventCategory> EventCategories { get; set; }
        public virtual DbSet<EventFavourite> EventFavourites { get; set; }
        public virtual DbSet<EventImage> EventImages { get; set; }
        public virtual DbSet<Events_Hit> Events_Hit { get; set; }
        public virtual DbSet<EventSubCategory> EventSubCategories { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<EventVenue> EventVenues { get; set; }
        public virtual DbSet<EventVote> EventVotes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MultipleEvent> MultipleEvents { get; set; }
        public virtual DbSet<Order_Detail_T> Order_Detail_T { get; set; }
        public virtual DbSet<Payment_Info> Payment_Info { get; set; }
        public virtual DbSet<Permission_Detail> Permission_Detail { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Publish_Event_Detail> Publish_Event_Detail { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Ticket_Locked_Detail> Ticket_Locked_Detail { get; set; }
        public virtual DbSet<Ticket_Purchased_Detail> Ticket_Purchased_Detail { get; set; }
        public virtual DbSet<Ticket_Quantity_Detail> Ticket_Quantity_Detail { get; set; }
        public virtual DbSet<TicketBearer> TicketBearers { get; set; }
        public virtual DbSet<TicketDeliveryMethod> TicketDeliveryMethods { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }
        public virtual DbSet<TimeZoneDetail> TimeZoneDetails { get; set; }
        public virtual DbSet<User_Permission_Detail> User_Permission_Detail { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<TicketOrderDetail> TicketOrderDetails { get; set; }
        public virtual DbSet<Fee_Structure> Fee_Structure { get; set; }
        public virtual DbSet<Organizer_Master> Organizer_Master { get; set; }
        public virtual DbSet<Event_Orgnizer_Detail> Event_Orgnizer_Detail { get; set; }
        public virtual DbSet<v_RetrieveEventid> v_RetrieveEventid { get; set; }
        public virtual DbSet<OrderTemplate> OrderTemplates { get; set; }
        public virtual DbSet<OrderTemplateQuestion> OrderTemplateQuestions { get; set; }
        public virtual DbSet<OrderTemplateType> OrderTemplateTypes { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<QuestionTypeGroup> QuestionTypeGroups { get; set; }
        public virtual DbSet<OrderTemplateEventType> OrderTemplateEventTypes { get; set; }
        public virtual DbSet<OrderTemplateTicket> OrderTemplateTickets { get; set; }
        public virtual DbSet<ControlType> ControlTypes { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<OrderTemplateQuestionTicket> OrderTemplateQuestionTickets { get; set; }
        public virtual DbSet<OrderTemplateQuestionVariant> OrderTemplateQuestionVariants { get; set; }
        public virtual DbSet<OrderTemplateGroupType> OrderTemplateGroupTypes { get; set; }
        public virtual DbSet<Promo_Code> Promo_Code { get; set; }
        public virtual DbSet<OrderState> OrderStates { get; set; }
    
        [DbFunction("EventComboEntities", "func_Split")]
        public virtual IQueryable<func_Split_Result> func_Split(string delimitedString, string delimiter)
        {
            var delimitedStringParameter = delimitedString != null ?
                new ObjectParameter("DelimitedString", delimitedString) :
                new ObjectParameter("DelimitedString", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<func_Split_Result>("[EventComboEntities].[func_Split](@DelimitedString, @Delimiter)", delimitedStringParameter, delimiterParameter);
        }
    
        public virtual ObjectResult<GetEventDateList_Result> GetEventDateList(Nullable<long> eventId)
        {
            var eventIdParameter = eventId.HasValue ?
                new ObjectParameter("EventId", eventId) :
                new ObjectParameter("EventId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEventDateList_Result>("GetEventDateList", eventIdParameter);
        }
    
        public virtual ObjectResult<GetEventListing_Result> GetEventListing(string eventTitle, string eventType, string eventCat, string eventSubCat, string eventFeature, string eventStartdate, string eventTicket)
        {
            var eventTitleParameter = eventTitle != null ?
                new ObjectParameter("EventTitle", eventTitle) :
                new ObjectParameter("EventTitle", typeof(string));
    
            var eventTypeParameter = eventType != null ?
                new ObjectParameter("EventType", eventType) :
                new ObjectParameter("EventType", typeof(string));
    
            var eventCatParameter = eventCat != null ?
                new ObjectParameter("EventCat", eventCat) :
                new ObjectParameter("EventCat", typeof(string));
    
            var eventSubCatParameter = eventSubCat != null ?
                new ObjectParameter("EventSubCat", eventSubCat) :
                new ObjectParameter("EventSubCat", typeof(string));
    
            var eventFeatureParameter = eventFeature != null ?
                new ObjectParameter("EventFeature", eventFeature) :
                new ObjectParameter("EventFeature", typeof(string));
    
            var eventStartdateParameter = eventStartdate != null ?
                new ObjectParameter("EventStartdate", eventStartdate) :
                new ObjectParameter("EventStartdate", typeof(string));
    
            var eventTicketParameter = eventTicket != null ?
                new ObjectParameter("EventTicket", eventTicket) :
                new ObjectParameter("EventTicket", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEventListing_Result>("GetEventListing", eventTitleParameter, eventTypeParameter, eventCatParameter, eventSubCatParameter, eventFeatureParameter, eventStartdateParameter, eventTicketParameter);
        }
    
        public virtual ObjectResult<GetEventsListByStatus1_Result> GetEventsListByStatus1(string eventTitle, string eventStatus, string userID)
        {
            var eventTitleParameter = eventTitle != null ?
                new ObjectParameter("EventTitle", eventTitle) :
                new ObjectParameter("EventTitle", typeof(string));
    
            var eventStatusParameter = eventStatus != null ?
                new ObjectParameter("EventStatus", eventStatus) :
                new ObjectParameter("EventStatus", typeof(string));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEventsListByStatus1_Result>("GetEventsListByStatus1", eventTitleParameter, eventStatusParameter, userIDParameter);
        }
    
        public virtual ObjectResult<string> GetSelectedTicketListing(string gUID, Nullable<long> eventId)
        {
            var gUIDParameter = gUID != null ?
                new ObjectParameter("GUID", gUID) :
                new ObjectParameter("GUID", typeof(string));
    
            var eventIdParameter = eventId.HasValue ?
                new ObjectParameter("EventId", eventId) :
                new ObjectParameter("EventId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetSelectedTicketListing", gUIDParameter, eventIdParameter);
        }
    
        public virtual ObjectResult<string> GetSetUserRole(string user_Id, string gETSET, string role_Id)
        {
            var user_IdParameter = user_Id != null ?
                new ObjectParameter("user_Id", user_Id) :
                new ObjectParameter("user_Id", typeof(string));
    
            var gETSETParameter = gETSET != null ?
                new ObjectParameter("GETSET", gETSET) :
                new ObjectParameter("GETSET", typeof(string));
    
            var role_IdParameter = role_Id != null ?
                new ObjectParameter("Role_Id", role_Id) :
                new ObjectParameter("Role_Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetSetUserRole", user_IdParameter, gETSETParameter, role_IdParameter);
        }
    
        public virtual ObjectResult<string> GetTicketListing(Nullable<long> eventId)
        {
            var eventIdParameter = eventId.HasValue ?
                new ObjectParameter("EventId", eventId) :
                new ObjectParameter("EventId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetTicketListing", eventIdParameter);
        }
    
        public virtual int PublishEvent(Nullable<long> eventId, string userId)
        {
            var eventIdParameter = eventId.HasValue ?
                new ObjectParameter("EventId", eventId) :
                new ObjectParameter("EventId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PublishEvent", eventIdParameter, userIdParameter);
        }
    
        public virtual ObjectResult<string> GetLantLong(string lat, string @long)
        {
            var latParameter = lat != null ?
                new ObjectParameter("Lat", lat) :
                new ObjectParameter("Lat", typeof(string));
    
            var longParameter = @long != null ?
                new ObjectParameter("Long", @long) :
                new ObjectParameter("Long", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetLantLong", latParameter, longParameter);
        }
    
        public virtual ObjectResult<GetOrganizerEventid_Result> GetOrganizerEventid(Nullable<long> masterid)
        {
            var masteridParameter = masterid.HasValue ?
                new ObjectParameter("Masterid", masterid) :
                new ObjectParameter("Masterid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrganizerEventid_Result>("GetOrganizerEventid", masteridParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> GetLantLongDistance(Nullable<double> lat1, Nullable<double> long1, Nullable<double> lat2, Nullable<double> long2)
        {
            var lat1Parameter = lat1.HasValue ?
                new ObjectParameter("lat1", lat1) :
                new ObjectParameter("lat1", typeof(double));
    
            var long1Parameter = long1.HasValue ?
                new ObjectParameter("long1", long1) :
                new ObjectParameter("long1", typeof(double));
    
            var lat2Parameter = lat2.HasValue ?
                new ObjectParameter("lat2", lat2) :
                new ObjectParameter("lat2", typeof(double));
    
            var long2Parameter = long2.HasValue ?
                new ObjectParameter("long2", long2) :
                new ObjectParameter("long2", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("GetLantLongDistance", lat1Parameter, long1Parameter, lat2Parameter, long2Parameter);
        }
    }
}
