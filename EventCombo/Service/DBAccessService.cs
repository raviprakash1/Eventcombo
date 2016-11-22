using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;
using AutoMapper;
using EventCombo.Utils;

namespace EventCombo.Service
{
  public class DBAccessService: IDBAccessService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;

    public static string UserRoleSA = "1";
    public static string UserRoleAdmin = "2";
    public static string UserRoleUser = "3";
    public static int UserPermissionEvents = 4;

    public DBAccessService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
    }

    public IEnumerable<Address> GetEventAddresses(long eventId)
    {
      IRepository<Event> evRepo = new GenericRepository<Event>(_factory.ContextFactory);

      var ev = evRepo.Get(filter: (e => e.EventID == eventId)).SingleOrDefault();

      if (ev.AddressStatus == "Online")
      {
        return null;
      }
      
      if (ev.AddressStatus == "PastLocation")
      {
        IRepository<Address> aRepo = new GenericRepository<Address>(_factory.ContextFactory);
        return aRepo.Get(filter: (a => a.AddressID == ev.LastLocationAddress));
      }

      return ev.Addresses;
    }


    public StartEndDateTime GetEventStartEnd(long eventId)
    {
      IRepository<Event> evRepo = new GenericRepository<Event>(_factory.ContextFactory);
      var cevent = evRepo.Get(filter: (ev => ev.EventID == eventId)).SingleOrDefault();
      if (cevent == null)
        return null;
      StartEndDateTime se = new StartEndDateTime();
      var svenue = cevent.EventVenues.FirstOrDefault();
      if (svenue != null)
      {
        se.Start = DateTime.Parse(svenue.EventStartDate + " " + svenue.EventStartTime);
        se.End = DateTime.Parse(svenue.EventEndDate + " " + svenue.EventEndTime);
      }
      else
      {
        var mvenue = cevent.MultipleEvents.FirstOrDefault();
        if (mvenue != null)
        {
          se.Start = DateTime.Parse(mvenue.StartingFrom + " " + mvenue.StartTime);
          se.End = DateTime.Parse(mvenue.StartingTo + " " + mvenue.EndTime);
        }
      }
      return se;
    }

    public StartEndDateTime GetEventStartEndUTC(long eventId)
    {
      IRepository<Event> evRepo = new GenericRepository<Event>(_factory.ContextFactory);
      var cevent = evRepo.Get(filter: (ev => ev.EventID == eventId)).SingleOrDefault();
      if (cevent == null)
        return null;
      StartEndDateTime se = new StartEndDateTime();
      var svenue = cevent.EventVenues.FirstOrDefault();
      if (svenue != null)
      {
        se.Start = svenue.E_Startdate ?? DateTime.MinValue;
        se.End = svenue.E_Enddate ?? DateTime.MinValue;
      }
      else
      {
        var mvenue = cevent.MultipleEvents.FirstOrDefault();
        if (mvenue != null)
        {
          se.Start = mvenue.M_Startfrom ?? DateTime.MinValue;
          se.End = mvenue.M_StartTo ?? DateTime.MinValue;
        }
      }
      return se;
    }

    public string GetPaymentInfo(string orderId)
    {
      if (String.IsNullOrWhiteSpace(orderId))
        throw new ArgumentNullException("orderId");

      string res = "";
      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

      var order = orderRepo.Get(filter: (o => o.O_Order_Id == orderId)).FirstOrDefault();
      if (order == null)
        return res;

      if (String.IsNullOrEmpty(order.O_PayPal_TrancId))
      {
        IRepository<BillingAddress> billRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
        var billing = billRepo.Get(filter: (b => b.OrderId == orderId)).FirstOrDefault();
        if (billing != null)
        {
          EncryptDecrypt encryptor = new EncryptDecrypt();
          string cardtype = encryptor.DecryptText(billing.card_type);
          string cardId = encryptor.DecryptText(billing.CardId);
          res = cardtype.First().ToString().ToUpper() + cardtype.Substring(1) + " XXXX-XXXX-XXXX-" + cardId.Substring(cardId.Length - 4);
        }
        else
          res = "No information";
      }
      else
        res = "PayPal ID: XXXXXXXXXXXX" + order.O_PayPal_TrancId.Substring(order.O_PayPal_TrancId.Length - 4);

      return res;
    }


    public AccessLevel GetEventAccess(long eventId, string userId)
    {
      if (String.IsNullOrWhiteSpace(userId))
        return AccessLevel.Public;

      IRepository<Event> evRepo = new GenericRepository<Event>(_factory.ContextFactory);

      Event ev = evRepo.Get(filter: (e => e.EventID == eventId)).SingleOrDefault();
      if (ev == null)
        return AccessLevel.Public;
      if (ev.UserID == userId)
        return AccessLevel.EventOwner;
      if (ev.Ticket_Purchased_Detail.Any(t => t.TPD_User_Id == userId))
        return AccessLevel.OrderOwner;
      return AccessLevel.Public;
    }

    public AccessLevel GetOrderAccess(string orderId, string userId)
    {
      if (String.IsNullOrWhiteSpace(orderId))
        throw new ArgumentNullException("orderId");
      if (String.IsNullOrWhiteSpace(userId))
        throw new ArgumentNullException("userId");

      IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var order = oRepo.Get(o => o.O_Order_Id == orderId).FirstOrDefault();
      if (order == null)
        return AccessLevel.Public;

      if (order.O_User_Id == userId)
        return AccessLevel.OrderOwner;

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var ticket = tpdRepo.Get(filter: (t => t.TPD_Order_Id == orderId)).FirstOrDefault();
      if ((ticket != null) && (ticket.Event.UserID == userId))
        return AccessLevel.EventOwner;

      return AccessLevel.Public;
    }

    public EventCombo.Models.Profile GetUserProfileByEmail(string email)
    {
      IRepository<EventCombo.Models.Profile> pRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
      return pRepo.Get(filter: (pr => pr.Email.ToLower() == email.Trim().ToLower())).FirstOrDefault();
    }

    public EventCombo.Models.Profile GetUserProfileById(string userId)
    {
      IRepository<EventCombo.Models.Profile> pRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
      return pRepo.Get(filter: (pr => pr.UserID == userId)).FirstOrDefault();
    }

    public bool IsEventAdmin(string userId)
    {
      IRepository<AspNetUser> uRepo = new GenericRepository<AspNetUser>(_factory.ContextFactory);
      var user = uRepo.Get(filter: (u => u.Id == userId)).FirstOrDefault();

      if (user == null)
        return false;

      if (user.AspNetRoles.Any(ur => ur.Id == UserRoleSA))
        return true;

      if (user.AspNetRoles.Any(ur => ur.Id == UserRoleAdmin))
      {
        IRepository<User_Permission_Detail> upRepo = new GenericRepository<User_Permission_Detail>(_factory.ContextFactory);
        if (upRepo.Get(filter: (up => (up.UP_User_Id == userId) && (up.UP_Permission_Id == UserPermissionEvents))).Any())
          return true;
      }

      return false;
    }
  }
}
