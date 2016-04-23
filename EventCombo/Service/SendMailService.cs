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
    public void SendMail(MailMessage mess)
    {
      SmtpClient smtp = new SmtpClient();
      smtp.Host = ConfigurationManager.AppSettings["Host"];
      smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
      System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
      NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
      NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
      smtp.UseDefaultCredentials = true;
      smtp.Credentials = NetworkCred;
      smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
      smtp.Send(mess);
    }
  }
}