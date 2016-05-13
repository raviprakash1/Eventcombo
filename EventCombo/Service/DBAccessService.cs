using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;
using AutoMapper;

namespace EventCombo.Service
{
  public class DBAccessService: IDBAccessService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;

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

    public AccessLevel GetOrderAccess(string orderId, string userId)
    {
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var ticket = tpdRepo.Get(filter: (t => ((t.TPD_User_Id == userId) && (t.TPD_Order_Id == orderId)))).FirstOrDefault();
      if (ticket != null)
        return AccessLevel.OrderOwner;
      if (ticket.Event.UserID == userId)
        return AccessLevel.EventOwner;
      return AccessLevel.Public;
    }

  }
}