using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
namespace EventCombo.Models
{
    public class MyAccount
    {
        EventComboEntities db = new EventComboEntities();
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string HomeNumber { get; set; }
        public string WorkNumber { get; set; }
        public string Website { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
        public string Picturename { get; set; }
        public string Pictureurl { get; set; }
        public string WorkPhone { get; set; }
        public string userimage { get; set; }


        public myAccount GetLoginDetails(string userid)
        {

            using (EventComboEntities objEntity = new EventComboEntities())
            {
                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.Profiles
                                      on cpd.Id equals pfd.UserID
                                      where cpd.Id == userid
                                      select new myAccount
                                      {
                                          Id = cpd.Id,
                                          Firstname = pfd.FirstName,
                                          Lastname = pfd.LastName,
                                          StreetAddress1 = pfd.StreetAddressLine1,
                                          StreeAddress2 = pfd.StreetAddressLine2,
                                          City = pfd.City,
                                          State = pfd.State,
                                          Zip = pfd.Zip,
                                          Country = pfd.CountryID.ToString(),
                                          MainPhone = pfd.MainPhone,
                                          SecondPhone = pfd.SecondPhone,
                                          WebsiteURL = pfd.WebsiteURL,
                                          UserProfileImage = pfd.UserProfileImage,
                                          Email = cpd.Email,
                                          Password = cpd.PasswordHash,
                                          contentype = pfd.ContentType,
                                          Dateofbirth = pfd.DateofBirth,
                                          Gender = pfd.Gender,
                                          PreviousEmail = cpd.Email,
                                          SendLatestdetails = pfd.SendCur_EventDetail

                                      });
                return modelmyaccount.FirstOrDefault();

            }
        }
        public Email_Template getEmail(string template)
        {

            var userEmail = db.Email_Template.Where(x => x.Template_Tag == template).SingleOrDefault();

            return userEmail;


        }
        public string getusername()
        {
            if ((HttpContext.Current.Session["AppId"] != null))
            {
                string userid = HttpContext.Current.Session["AppId"].ToString();
                var userEmail = db.AspNetUsers.Where(x => x.Id == userid).Select(y => y.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    return userEmail.Substring(0, userEmail.IndexOf("@") + 1);
                }
                else
                {

                    return "";
                }

            }
            else
            {

                return "";

            }
        }
        public List<Email_Tag> getTag()
        {
            var EmailTag = db.Email_Tag.ToList();
            return EmailTag;

        }
        public void SendHtmlFormattedEmail(string To, string from, string subject, string body, string cc, string bcc, MemoryStream attachment, string emailname, string qrimage, string brcode, List<TicketBearer> GuestList)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(from, emailname);


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
            if (attachment != null)
            {
                if (attachment.Length != 0)
                {
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachment, ct);
                    attach.ContentDisposition.FileName = "Ticket_EventCombo.pdf";
                    mailMessage.Attachments.Add(attach);
                }
            }
            mailMessage.IsBodyHtml = true;
            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            //mailMessage.AlternateViews.Add(htmlView);

            //Add Image
            //LinkedResource theEmailImage = new LinkedResource(ImageMapPath);
            //theEmailImage.ContentId = "myeventmapImageID";
            //htmlView.LinkedResources.Add(theEmailImage);


            ////LinkedResource theQrImage = new LinkedResource(qrimage);

            ////theQrImage.ContentId = "myQrcodeImageID";
            ////htmlView.LinkedResources.Add(theQrImage);


            ////LinkedResource thebarImage = new LinkedResource(brcode);

            ////thebarImage.ContentId = "myBarcodeImageID";
            ////htmlView.LinkedResources.Add(thebarImage);

            //LinkedResource theeventImage = new LinkedResource(Imageevent);

            //theeventImage.ContentId = "myeventImageID";
            //htmlView.LinkedResources.Add(theeventImage);


            mailMessage.To.Add(new MailAddress(To));
            if (GuestList != null)
            {
                foreach (var item in GuestList)
                {
                    mailMessage.To.Add(new MailAddress(item.Email, item.Name));
                }
            }
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

        public void SendHtmlFormattedEmail(string To, string from, string subject, string body, string cc, string bcc, string tags, string emailname)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(from, emailname);
                List<Email_Tag> EmailTag = new List<Email_Tag>();
                EmailTag = getTag();
                if (!string.IsNullOrWhiteSpace(tags))
                {
                    string[] arr = tags.Split('¶');
                    int length = arr.Length;



                    if (!string.IsNullOrEmpty(subject) && subject != null)
                    {


                        for (int j = 0; j < length; j++)
                        {
                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {
                                string[] arrtag = arr[j].Split(':');
                                if (arrtag[0] == EmailTag[i].Tag_Name)
                                {
                                    if (subject.Contains(EmailTag[i].Tag_Name))
                                    {
                                        subject = subject.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", arrtag[1]);
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                        {
                            if (subject.Contains(EmailTag[i].Tag_Name))
                            {
                                subject = subject.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", "");
                            }
                        }



                    }
                    if (body != null && !string.IsNullOrEmpty(body))
                    {
                        for (int j = 0; j < length; j++)
                        {
                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {

                                string[] arrtag = arr[j].Split(':');
                                if (arrtag[0] == EmailTag[i].Tag_Name)
                                {
                                    if (body.Contains(EmailTag[i].Tag_Name))
                                    {
                                        body = body.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", arrtag[1]);
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                        {
                            if (body.Contains(EmailTag[i].Tag_Name))
                            {
                                body = body.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", "");
                            }
                        }

                    }
                }
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

        public bool Getprofiledetails(string id)
        {
            using (EventComboEntities objEntity = new EventComboEntities())
            {

                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.Profiles
    on cpd.Id equals pfd.UserID
                                      where cpd.Id == id
                                      select new ExternalLogin
                                      {
                                          userid = cpd.Id,
                                          email = cpd.Email

                                      }).FirstOrDefault();


                if (modelmyaccount == null)
                {

                    return false;
                }
                else
                {
                    return true;

                }
            }
        }
        public bool GetExternalLogindetails(string userid, string loginprovider)
        {
            using (EventComboEntities objEntity = new EventComboEntities())
            {

                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.AspNetUserLogins
    on cpd.Id equals pfd.UserId
                                      where cpd.Id == userid && pfd.LoginProvider == loginprovider
                                      select new ExternalLogin
                                      {
                                          userid = cpd.Id,
                                          email = cpd.Email

                                      }).FirstOrDefault();


                if (modelmyaccount == null)
                {

                    return false;
                }
                else
                {
                    return true;

                }


            }


        }
    }
}