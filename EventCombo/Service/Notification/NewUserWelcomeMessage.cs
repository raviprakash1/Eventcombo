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
  public class NewUserWelcomeMessage : INotification
  {
    private IUnitOfWorkFactory _factory;
    private string _userEmail;
    private string _defaultEmail;
    private string _receiver;
    public string ReceiverName
    {
      get { return _receiver; }
      set { _receiver = value; }
    }

    public NewUserWelcomeMessage(IUnitOfWorkFactory factory, string userEmail, string defaultEmail)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (String.IsNullOrEmpty(userEmail))
        throw new ArgumentNullException("userEmail");
      if (String.IsNullOrEmpty(defaultEmail))
        throw new ArgumentNullException("defaultEmail");

      _factory = factory;
      _userEmail = userEmail;
      _defaultEmail = defaultEmail;
    }

    private string ReplaceTags(string src, List<KeyValuePair<string, string>> tagList)
    {
      StringBuilder sbRes = new StringBuilder(src);
      if (!string.IsNullOrEmpty(src))
      {
        foreach (var tag in tagList)
          sbRes.Replace("¶¶" + tag.Key + "¶¶", tag.Value);
      }
      return sbRes.ToString();
    }

    public void SendNotification(ISendMailService _service)
    {
      IRepository<Email_Template> etRepo = new GenericRepository<Email_Template>(_factory.ContextFactory);
      var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "email_welcome")).SingleOrDefault();
      if (eTemplate == null)
        throw new Exception("Email template 'email_welcome' not found.");

      List<KeyValuePair<string, string>> tagList = new List<KeyValuePair<string, string>>();
      tagList.Add(new KeyValuePair<string, string>("UserEmailID", _userEmail));

      _service.Message.To.Clear();
      _service.Message.To.Add(new MailAddress(String.IsNullOrEmpty(eTemplate.To) ? _userEmail : ReplaceTags(eTemplate.To, tagList)));
      string fromAddress = String.IsNullOrEmpty(eTemplate.From) ? _defaultEmail : ReplaceTags(eTemplate.From, tagList);
      _service.Message.From = new MailAddress(fromAddress, String.IsNullOrEmpty(eTemplate.From_Name) ? fromAddress : eTemplate.From_Name);
      _service.Message.CC.Clear();
      if (!String.IsNullOrEmpty(eTemplate.CC))
        _service.Message.CC.Add(new MailAddress(ReplaceTags(eTemplate.CC, tagList)));
      _service.Message.Bcc.Clear();
      if (!String.IsNullOrEmpty(eTemplate.Bcc))
        _service.Message.Bcc.Add(new MailAddress(ReplaceTags(eTemplate.Bcc, tagList)));
      _service.Message.Subject = eTemplate.Subject;
      _service.Message.IsBodyHtml = true;
      _service.Message.Body = ReplaceTags(new MvcHtmlString(HttpUtility.HtmlDecode(eTemplate.TemplateHtml)).ToHtmlString(), tagList);
      _service.SendMail();
    }
  }
}