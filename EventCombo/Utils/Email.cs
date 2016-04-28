using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EventCombo.Utils
{
    public static class Email
    {

        public static bool send(string subject, string body, string cc, string bcc, string to, string from, string fromName)
        {
            bool sendStatus = false;

            using (MailMessage mailMessage = new MailMessage())
            {
                try
                {
                    if (string.IsNullOrEmpty(from))
                    {
                        from = ConfigurationManager.AppSettings["UserName"];
                    }

                    if (string.IsNullOrEmpty(fromName))
                    {
                        fromName = ConfigurationManager.AppSettings["UserName"];
                    }

                    mailMessage.From = new MailAddress(from, fromName);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    if (!string.IsNullOrEmpty(cc))
                    {
                        mailMessage.CC.Add(cc);
                    }
                    if (!string.IsNullOrEmpty(bcc))
                    {
                        mailMessage.Bcc.Add(bcc);
                    }
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(to));
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    smtp.Send(mailMessage);
                    sendStatus = true;
                }
                catch (Exception)
                {
                    sendStatus = false;
                    throw;
                }

            }
            return sendStatus;
        }

    }
}