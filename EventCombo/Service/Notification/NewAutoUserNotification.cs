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
  public class NewAutoUserNotification : INotification
  {

    private IUnitOfWorkFactory _factory;
    private string _receiver;
    private string _userId;
    private string _password;

    public NewAutoUserNotification(IUnitOfWorkFactory factory, string userId, string password)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      _factory = factory;
      _userId = userId;
      _password = password;
    }

    public string ReceiverName
    {
      get { return _receiver; }
      set { _receiver = value; }
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

      string userEmail = string.Empty;
      string userFirstName = string.Empty;
      string userLastName = string.Empty;

      IRepository<Email_Template> etRepo = new GenericRepository<Email_Template>(_factory.ContextFactory);
      IRepository<Profile> pRepo = new GenericRepository<Profile>(_factory.ContextFactory);

      var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "email_auto_register")).SingleOrDefault();
      if (eTemplate == null)
        throw new Exception("Email template 'Auto_Registration_Notification' not found.");

      var profile = pRepo.Get(p => p.UserID == _userId).FirstOrDefault();
      if ((profile != null) && (!String.IsNullOrWhiteSpace(profile.Email)))
      {
        userEmail = profile.Email;
        userFirstName = profile.FirstName ?? "";
        userLastName = profile.LastName ?? "";
      }
      var tagList = LoadTagList();
      tagList["UserEmailID"] = userEmail;
      tagList["UserFirstNameID"] = userFirstName;
      tagList["UserLastNameID"] = userLastName;
      tagList["UserPassword"] = _password;
      tagList["ClickHere"] = HtmlProcessing.ResolveServerUrl("/Home/Index", false);

      _service.Message.To.Clear();
      if (String.IsNullOrEmpty(eTemplate.To))
        _service.Message.To.Add(new MailAddress(userEmail, userFirstName + " " + userLastName));
      else
        _service.Message.To.Add(ReplaceTags(eTemplate.To, tagList));

      string fromAddress = String.IsNullOrEmpty(eTemplate.From) ? System.Configuration.ConfigurationManager.AppSettings.Get("DefaultEmail") : ReplaceTags(eTemplate.From, tagList);
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
  }
}