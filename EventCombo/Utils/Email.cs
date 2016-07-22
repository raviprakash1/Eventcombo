using EventCombo.Models;
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
        public static void SendToOrganizer(long lMessId)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var vOrgMes = (from Org in db.Event_OrganizerMessages where Org.MessageId == lMessId select Org).FirstOrDefault();
                var vOrgDetail = (from Org in db.Event_Orgnizer_Detail where Org.Orgnizer_Event_Id == vOrgMes.OrganizerId select Org).FirstOrDefault();
                var vOrgMailId = (from UserEmail in db.Profiles where UserEmail.UserID == vOrgDetail.UserId select UserEmail.Email).SingleOrDefault();
                

                List<Email_Tag> EmailTag = new List<Email_Tag>();
                MyAccount ac = new MyAccount();
                EmailTag = ac.getTag();
                var Emailtemplate = ac.getEmail("Contact_Event_Organizer");
                string to = "", from = "", cc = "", bcc = "", emailname = "", subjectn = "", bodyn = "";
                if (Emailtemplate != null)
                {
                    if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                    {
                        from = Emailtemplate.From;
                    }
                    else
                    {
                        from = ConfigurationManager.AppSettings.Get("UserName");
                    }

                    if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                    {
                        cc = Emailtemplate.CC;
                    }
                    if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                    {
                        bcc = Emailtemplate.Bcc;
                    }
                    if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                    {
                        emailname = Emailtemplate.From_Name;
                    }
                    else
                    {
                        emailname = from;
                    }

                    if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                    {
                        subjectn = Emailtemplate.Subject;
                        for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                        {
                            if (subjectn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                            {
                                if (EmailTag[i].Tag_Name == "EventTitleId")
                                {
                                    subjectn = subjectn.Replace("¶¶EventTitleId¶¶", strEventTitle);
                                }


                            }
                        }
                    }

                    ac.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, "", emailname);
                }
            }
        }
    }
}