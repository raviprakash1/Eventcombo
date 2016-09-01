using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public class EventNotificationViewModel
  {
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public long EventId { get; set; }
  }
}