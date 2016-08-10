﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;
using System.Web.Mvc;

namespace EventCombo.Service
{
  interface IEventService
  {
    EventViewModel CreateEvent(string userId);
    void SaveEvent(EventViewModel ev, Func<string, string> mapPath);
    EventViewModel GetEventById(int id);
    bool ValidateEvent(EventViewModel ev);
    void PublishEvent(long id, string userId);
    IEnumerable<EventSearchViewModel> Search(string searchStr);
    EventInfoViewModel GetEventInfo(long eventId, string userId, UrlHelper url);
    void UpdateEventInfo(EventInfoViewModel ev, string userId, UrlHelper url);
    void ValidateEventInfo(EventInfoViewModel evi);
    string GetEventUrl(long eventId, string eventTitle, UrlHelper url);
  }
}