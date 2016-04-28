using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EventCombo.Service
{
  public delegate void PrepareNotification(INotification notification, MailAddress address);

  public class NotificationSender : INotificationSender
  {
    INotification _notification;
    ISendMailService _service;

    public NotificationSender(INotification notification, ISendMailService service)
    {
      if (notification == null)
        throw new ArgumentNullException("notification");
      if (service == null)
        throw new ArgumentNullException("service");

      _notification = notification;
      _service = service;
    }
    public void Send(MailAddress address)
    {
      if (address == null)
        throw new ArgumentNullException("address");
      _service.Message.To.Clear();
      _service.Message.To.Add(address);
      _notification.ReceiverName = address.DisplayName;
      _notification.SendNotification(_service);
    }

    public void Send(string name, string eMail)
    {
      if (String.IsNullOrWhiteSpace(eMail))
        throw new ArgumentNullException("eMail");

      _service.Message.To.Clear();
      _service.Message.To.Add(new MailAddress(eMail, name));
      _notification.ReceiverName = name;
      _notification.SendNotification(_service);
    }

    public void Send(IEnumerable<MailAddress> addresses)
    {
      if (addresses == null)
        throw new ArgumentNullException("addresses");

      if (addresses.Count() > 0)
      {
        _service.Message.To.Clear();
        foreach (var address in addresses)
          _service.Message.To.Add(address);
        _notification.ReceiverName = "";
        _notification.SendNotification(_service);
      }
    }

    public void SendSeparately(IEnumerable<MailAddress> addresses, PrepareNotification prepareFunc = null)
    {
      if (addresses == null)
        throw new ArgumentNullException("addresses");

      foreach (var address in addresses)
      {
        if (prepareFunc != null)
          prepareFunc(_notification, address);
        _service.Message.To.Clear();
        _service.Message.To.Add(address);
        _notification.ReceiverName = address.DisplayName;
        _notification.SendNotification(_service);
      }
    }

  }
}