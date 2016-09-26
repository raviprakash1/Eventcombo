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
  public class EmailToFriendsNotification : INotification
  {
    private IUnitOfWorkFactory _factory;
    private FriendNotificationViewModel _message;
    private string _defaultEmail;

    public EmailToFriendsNotification(IUnitOfWorkFactory factory, FriendNotificationViewModel message, string defaultEmail)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (message == null)
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
        IRepository<Event> evRepo = new GenericRepository<Event>(_factory.ContextFactory);
        IRepository<Article> articleRepo = new GenericRepository<Article>(_factory.ContextFactory);

        var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "email_friend")).SingleOrDefault();
        if (eTemplate == null)
            throw new Exception("Email template 'email_friend' not found.");

        string Title = "";
        string url = "";
        Event ev = null;
        Article ar = null;

        if (_message.Type.ToLower() == "event")
        {
            ev = evRepo.GetByID(_message.Id);
            Title = ev.EventTitle;

            var baseurl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            url = baseurl + urlHelper.Action("ViewEvent", "EventManagement", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(Title.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ev.EventID.ToString() });
        }
        else
        {
            ar = articleRepo.GetByID(_message.Id);
            Title = ar.Title;
            url = ArticleService.GetArticleUrl(ar.ArticleId, ar.Title);
        }
        if (ev == null && ar == null)
            throw new Exception("Event not found for ID = " + _message.Id.ToString());

        var tagList = LoadTagList();
        tagList["UserFirstNameID"] = _message.Name;
        tagList["UserPhone"] = _message.Phone;
        tagList["FriendsEmail"] = string.Join(";", _message.To);
        tagList["EventTitleId"] = Title;
        tagList["MessageBody"] = _message.Message;
        tagList["DiscoverEventurl"] = url;


        _service.Message.To.Clear();
        if (_message.To.Count() > 0)
        {
            foreach (var to in _message.To)
            {
                _service.Message.To.Add(to);
            }
        }
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

        _service.Message.Subject = (string.IsNullOrEmpty(_message.Subject) ? ReplaceTags(eTemplate.Subject, tagList) : _message.Subject);
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