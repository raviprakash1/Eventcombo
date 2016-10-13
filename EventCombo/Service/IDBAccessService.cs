using System;
using System.Collections.Generic;
using EventCombo.Models;

namespace EventCombo.Service
{
  public enum AccessLevel { EventOwner, OrderOwner, Public }

  public interface IDBAccessService
  {
    IEnumerable<Address> GetEventAddresses(long eventId);
    StartEndDateTime GetEventStartEnd(long eventId);
    string GetPaymentInfo(string orderId);
    AccessLevel GetEventAccess(long eventId, string userId);
    AccessLevel GetOrderAccess(string orderId, string userId);
    Profile GetUserProfileByEmail(string email);
    Profile GetUserProfileById(string userId);
    bool IsEventAdmin(string userId);
  }
}
