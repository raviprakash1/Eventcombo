using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;
using System.Web.Mvc;

namespace EventCombo.Service
{
  public interface IEventService
  {
    EventViewModel CreateEvent(string userId);
    void SaveEvent(EventViewModel ev, Func<string, string> mapPath);
    EventViewModel GetEventById(long id);
    bool ValidateEvent(EventViewModel ev);
    void PublishEvent(long id, string userId);
    IEnumerable<EventSearchViewModel> Search(string searchStr);
    EventInfoViewModel GetEventInfo(long eventId, string userId, UrlHelper url);
    EventInfoViewModel GetBasicEventInfo(long eventId, UrlHelper url);
    EventInfoViewModel GetPrivateEventInfo(long eventId, string userId, UrlHelper url);
    void UpdateEventInfo(EventInfoViewModel ev, string userId, UrlHelper url, bool loadPrivate);
    void ValidateEventInfo(EventInfoViewModel evi);
    IncrementResultViewModel AddFavorite(long eventId, string userId);
    IncrementResultViewModel VoteEvent(long eventId, string userId);
    IEnumerable<ShortEventInfoViewModel> GetEventListByCoords(decimal lat, decimal lng, string userId);
    HomepageInfoViewModel GetHomepageInfo();
    EventViewModel GetEventBySubDomain(string subDomain);
    void CheckEventAccess(PrivateEventRequest req);
    string GetTicketPrice(long eventId);
    string GetEventFavLikes(long eventId, string strUserId);
  }
}
