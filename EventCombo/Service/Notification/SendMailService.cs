using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EventCombo.Service
{
  public class SendMailService: ISendMailService
  {
    MailMessage _message = new MailMessage();

    public void SendMail()
    {
      SmtpClient smtp = new SmtpClient();
      smtp.Host = ConfigurationManager.AppSettings["Host"];
      smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
      System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
      string s = ConfigurationManager.AppSettings["UserName"];
      if (!String.IsNullOrEmpty(s))
      {
          NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
          NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
      }
      smtp.UseDefaultCredentials = true;
      smtp.Credentials = NetworkCred;
      smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
      smtp.Send(Message);
    }

    public MailMessage Message
    {
      get { return _message; }
    }
  }
}