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
    public long OrganizerId { get; set; }
  }
  public class FriendNotificationViewModel
  {
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string[] To { get; set; }
    public string Message { get; set; }
    public string Subject { get; set; }
    public long Id { get; set; }
    public string Type { get; set; }
  }
}