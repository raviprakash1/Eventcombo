using System;
using System.Collections.Generic;
using EventCombo.Models;

namespace EventCombo.Service
{
  public interface IDBAccessService
  {
    IEnumerable<Address> GetEventAddresses(long eventId);
    StartEndDateTime GetEventStartEnd(long eventId);
  }
}
