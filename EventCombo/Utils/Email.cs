using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
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
                        from = ConfigurationManager.AppSettings["DefaultEmail"]; //ConfigurationManager.AppSettings["UserName"];
                    }

                    if (string.IsNullOrEmpty(fromName))
                    {
                        fromName = ConfigurationManager.AppSettings["DefaultEmail"]; //ConfigurationManager.AppSettings["UserName"];
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
                    string s = ConfigurationManager.AppSettings["UserName"];
                    if (!String.IsNullOrEmpty(s))
                    {
                        NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                        NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                    }
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
        public static void SendErrorReport(string TemplateTag, string PrecedingPage, string ErrorPage, string SpecificErrorReason)
        {
            EventComboEntities db = new EventComboEntities();
            string Subject = string.Empty;
            string Body = string.Empty;
            string CC = string.Empty;
            string BCC = string.Empty;
            string To = string.Empty;
            string From = string.Empty;
            string FromName;

            var EmailTag = db.Email_Tag.ToList();
            var Emailtemplate = db.Email_Template.Where(x => x.Template_Tag == TemplateTag).SingleOrDefault();

            if (Emailtemplate != null)
            {
                if (!string.IsNullOrEmpty(Emailtemplate.To))
                {
                    To = Emailtemplate.To;
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                {
                    From = Emailtemplate.From;
                }
                else
                {
                    From = ConfigurationManager.AppSettings.Get("DefaultEmail");
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                {
                    FromName = Emailtemplate.From_Name;
                }
                else
                {
                    FromName = From;
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                {
                    CC = Emailtemplate.CC;
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                {
                    BCC = Emailtemplate.Bcc;
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                {
                    FromName = Emailtemplate.From_Name;
                }
                else
                {
                    FromName = From;
                }
                if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                {
                    Subject = Emailtemplate.Subject;

                }
                if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                {
                    Body = HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml).ToString();
                    foreach (var emailTag in EmailTag)
                    {
                        if (Body.Contains("¶¶" + emailTag.Tag_Name.Trim() + "¶¶"))
                        {
                            if (emailTag.Tag_Name == "PrecedingPage")
                            {
                                Body = Body.Replace("¶¶PrecedingPage¶¶", PrecedingPage);
                            }
                            if (emailTag.Tag_Name == "ErrorPage")
                            {
                                Body = Body.Replace("¶¶ErrorPage¶¶", ErrorPage);
                            }
                            if (emailTag.Tag_Name == "SpecificErrorReason")
                            {
                                Body = Body.Replace("¶¶SpecificErrorReason¶¶", SpecificErrorReason);
                            }
                        }
                    }
                }
                try
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(From, FromName);
                        mailMessage.Subject = Subject;
                        mailMessage.Body = Body;
                        if (!string.IsNullOrEmpty(CC))
                        {
                            mailMessage.CC.Add(CC);
                        }
                        if (!string.IsNullOrEmpty(BCC))
                        {
                            mailMessage.Bcc.Add(BCC);
                        }
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(To));
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
                        smtp.Send(mailMessage);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}