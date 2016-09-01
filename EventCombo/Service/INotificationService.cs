using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCombo.Service
{
  interface INotificationService
  {
    void SendContactOrganizerMessage(EventNotificationViewModel notification, string userId);
    void SendContactOrganizerMessage(OrganizerMessageViewModel notification);
    void SendEmailFreindsMessage(EventNotificationViewModel notification);
  }
}
