using EventCombo.DAL;
using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Service
{
  class OrganizerMessageNotification : INotification
  {
    private IUnitOfWorkFactory _factory;
    private Event_OrganizerMessages _message;
    private string _defaultEmail;

    public OrganizerMessageNotification(IUnitOfWorkFactory factory, Event_OrganizerMessages message, string defaultEmail)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if(message == null)
        throw new ArgumentNullException("message");
      if (String.IsNullOrEmpty(defaultEmail))
        throw new ArgumentNullException("defaultEmail");

      _factory = factory;
      _message = message;
      _defaultEmail = defaultEmail;
    }

    private string ReplaceTags(string src, Dictionary<string, string> tagList)
    {
      StringBuilder sbRes = new StringBuilder(src);
      if (!string.IsNullOrEmpty(src))
      {
        foreach (var tag in tagList)
          sbRes.Replace("¶¶" + tag.Key + "¶¶", tag.Value);
      }
      return sbRes.ToString();
    }

    private Dictionary<string, string> LoadTagList()
    {
      IRepository<Email_Tag> etRepo = new GenericRepository<Email_Tag>(_factory.ContextFactory);
      return etRepo.Get().ToDictionary(et => et.Tag_Name, et => "");
    }

    public void SendNotification(ISendMailService _service)
    {

      IRepository<Email_Template> etRepo = new GenericRepository<Email_Template>(_factory.ContextFactory);
      IRepository<Organizer_Master> omRepo = new GenericRepository<Organizer_Master>(_factory.ContextFactory);
      IRepository<Event> evRepo = new GenericRepository<Event>(_factory.ContextFactory);
      var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "Contact_Event_Organizer")).SingleOrDefault();
      if (eTemplate == null)
        throw new Exception("Email template 'Contact_Event_Organizer' not found.");

      var om = omRepo.GetByID(_message.OrganizerId);
      if (om == null)
        throw new Exception("Organizer_Master nof found for ID = " + (_message.OrganizerId ?? 0).ToString());
      var ev = evRepo.GetByID(_message.EventId);
      if (ev == null)
        throw new Exception("Event nof found for ID = " + (_message.EventId ?? 0).ToString());

      string userEmail = om.Organizer_Email ?? om.AspNetUser.Profiles.FirstOrDefault().Email;
      string userName = om.Orgnizer_Name ?? om.AspNetUser.Profiles.FirstOrDefault().FirstName;

      var tagList = LoadTagList();
      tagList["EventOrganiserEmail"] = userEmail;
      tagList["EventOrganiserName"] = userName;
      tagList["UserEmailID"] = _message.Email;
      tagList["UserFirstNameID"] = _message.Name;
      tagList["UserPhone"] = _message.PhoneNo;
      tagList["EventTitleId"] = ev.EventTitle;
      tagList["MessageBody"] = _message.Message;

      _service.Message.To.Clear();
      if (String.IsNullOrEmpty(eTemplate.To))
        _service.Message.To.Add(new MailAddress(userEmail, userName));
      else
       _service.Message.To.Add(ReplaceTags(eTemplate.To, tagList));

      string fromAddress = String.IsNullOrEmpty(eTemplate.From) ? _defaultEmail : ReplaceTags(eTemplate.From, tagList);
      _service.Message.From = new MailAddress(fromAddress, String.IsNullOrEmpty(eTemplate.From_Name) ? fromAddress : ReplaceTags(eTemplate.From_Name, tagList));

      _service.Message.CC.Clear();
      if (!String.IsNullOrEmpty(eTemplate.CC))
        _service.Message.CC.Add(new MailAddress(ReplaceTags(eTemplate.CC, tagList)));

      _service.Message.Bcc.Clear();
      if (!String.IsNullOrEmpty(eTemplate.Bcc))
        _service.Message.Bcc.Add(new MailAddress(ReplaceTags(eTemplate.Bcc, tagList)));

      _service.Message.Subject = ReplaceTags(eTemplate.Subject, tagList);
      _service.Message.IsBodyHtml = true;
      _service.Message.Body = ReplaceTags(new MvcHtmlString(HttpUtility.HtmlDecode(eTemplate.TemplateHtml)).ToHtmlString(), tagList);
      _service.SendMail();
    }

    private string _receiver;
    public string ReceiverName
    {
      get { return _receiver; }
      set { _receiver = value; }
    }
  }
}
