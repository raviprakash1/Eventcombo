using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;

namespace EventCombo.Service
{
  interface IEventService
  {
    EventViewModel CreateEvent(string userId);
    void SaveEvent(EventViewModel ev, Func<string, string> mapPath);
    EventViewModel GetEventById(int id);
    bool ValidateEvent(EventViewModel ev);
    void PublishEvent(long id, string userId);
    IEnumerable<EventTitleSearchViewModel> Search(string searchStr);
  }
}
