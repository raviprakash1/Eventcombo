using EventCombo.DAL;
using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCombo.Service
{
  class OrganizerMessageNotification : INotification
  {
    private IUnitOfWorkFactory _factory;
    private Event_OrganizerMessages _message;

    public OrganizerMessageNotification(IUnitOfWorkFactory factory, Event_OrganizerMessages message)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      _factory = factory;
      _message = message;
    }

    public void SendNotification(ISendMailService _service)
    {
      _service.Message.Subject = String.Format("Message about event {0} from {1}", _message.Event.EventTitle, _message.Name);
      _service.Message.Body = String.Format("Hi {0}, you received the message from {1}<{2}>:\r\n\r\n{3}", new string[] 
      {
        _message.Organizer_Master.Orgnizer_Name,
        _message.Name,
        _message.Email,
        _message.Message
      });
      _service.SendMail();
    }
  }
}
