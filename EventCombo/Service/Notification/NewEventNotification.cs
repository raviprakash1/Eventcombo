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
  class NewEventNotification : INotification
  {
    private IUnitOfWorkFactory _factory;
    private EventViewModel _event;
    private string _defaultEmail;

    public NewEventNotification(IUnitOfWorkFactory factory, EventViewModel ev, string defaultEmail)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (ev == null)
        throw new ArgumentNullException("ev");
      if (String.IsNullOrEmpty(defaultEmail))
        throw new ArgumentNullException("defaultEmail");

      _factory = factory;
      _event = ev;
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
      var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "new_event_notification_email")).SingleOrDefault();
      if (eTemplate == null)
        throw new Exception("Email template 'new_event_notification_email' not found.");

      var tagList = LoadTagList();
      if (_event.CurrentOrganizer != null)
      {
        tagList["EventOrganiserEmail"] = _event.CurrentOrganizer.Organizer_Email;
        tagList["EventOrganiserName"] = _event.CurrentOrganizer.Orgnizer_Name;
        tagList["EventOrganiserNumber"] = _event.CurrentOrganizer.Organizer_Phoneno;
      }
      tagList["EventTitleId"] = _event.EventTitle;
      tagList["EventAddressID"] = _event.Address;
      tagList["DiscoverEventurl"] = _event.EventPath;

      _service.Message.To.Clear();
      if (String.IsNullOrEmpty(eTemplate.To))
        _service.Message.To.Add(new MailAddress("events@eventcombo.com"));
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
