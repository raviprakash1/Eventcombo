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
    public class AttendeeMailNotification : INotification
    {
        private IUnitOfWorkFactory _factory;
        private string _receiver;
        private string _defaultEmail;
        private ScheduledEmailViewModel _scheduledEmail;
        private List<TicketBearer_View> _ticketBearers;

        public string ReceiverName
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public AttendeeMailNotification() : 
            this(new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory()), 
                System.Configuration.ConfigurationManager.AppSettings.Get("DefaultEmail"), 
                null, 
                null)
        {

        }

        public AttendeeMailNotification(IUnitOfWorkFactory factory, string defaultEmail, ScheduledEmailViewModel scheduledEmail, List<TicketBearer_View> ticketBearers)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (String.IsNullOrEmpty(defaultEmail))
                throw new ArgumentNullException("defaultEmail");

            _factory = factory;
            _defaultEmail = defaultEmail;
            _scheduledEmail = scheduledEmail;
            _ticketBearers = ticketBearers;
        }

        private Dictionary<string, string> LoadTagList()
        {
            IRepository<Email_Tag> etRepo = new GenericRepository<Email_Tag>(_factory.ContextFactory);
            return etRepo.Get().ToDictionary(et => et.Tag_Name, et => "");
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

        public void SendNotification(ISendMailService _service)
        {
            IRepository<Email_Template> etRepo = new GenericRepository<Email_Template>(_factory.ContextFactory);
            var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "attendee_email")).SingleOrDefault();
            if (eTemplate == null)
                throw new Exception("Email template 'attendee_email' not found.");

            var tagList = LoadTagList();
            tagList["UserEmailID"] = _defaultEmail;
            tagList["UserFirstNameID"] = _scheduledEmail.UserId;
            tagList["MessageBody"] = _scheduledEmail.Body;

            _service.Message.To.Clear();
            foreach (var email in _ticketBearers.Select(a => a.Email))
            {
                _service.Message.To.Add(new MailAddress(email));
            }
            string fromAddress = String.IsNullOrEmpty(eTemplate.From) ? _defaultEmail : ReplaceTags(eTemplate.From, tagList);
            _service.Message.From = new MailAddress(fromAddress, String.IsNullOrEmpty(eTemplate.From_Name) ? fromAddress : ReplaceTags(eTemplate.From_Name, tagList));

            if (!String.IsNullOrEmpty(_scheduledEmail.CC))
                _service.Message.CC.Add(new MailAddress(ReplaceTags(_scheduledEmail.CC, tagList)));
            _service.Message.Bcc.Clear();
            if (!String.IsNullOrEmpty(_scheduledEmail.BCC))
                _service.Message.Bcc.Add(new MailAddress(ReplaceTags(_scheduledEmail.BCC, tagList)));
            _service.Message.ReplyToList.Clear();
            _service.Message.ReplyToList.Add(new MailAddress(_scheduledEmail.ReplyTo));
            _service.Message.Subject = _scheduledEmail.Subject;
            _service.Message.IsBodyHtml = true;
            _service.Message.Body = ReplaceTags(new MvcHtmlString(HttpUtility.HtmlDecode(eTemplate.TemplateHtml)).ToHtmlString(), tagList);
            _service.SendMail();
        }

        public void Send()
        {
            IUnitOfWorkFactory uowFactory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory());
            IRepository<ScheduledEmail> sERepo = new GenericRepository<ScheduledEmail>(uowFactory.ContextFactory);
            var ScheduledEmails = sERepo.Get(filter: s => s.ScheduledDate < DateTime.UtcNow && s.SendDate == null);
            foreach (var scheduledEmail in ScheduledEmails)
            {
                using (var uow = uowFactory.GetUnitOfWork())

                    try
                    {
                        SendMailService _service = new SendMailService();
                        _service.Message.To.Clear();
                        foreach (var email in scheduledEmail.SendTo.Split(';').ToList())
                        {
                            _service.Message.To.Add(new MailAddress(email));
                        }
                        string fromAddress = scheduledEmail.SendFrom;
                        _service.Message.From = new MailAddress(scheduledEmail.SendFrom, "");
                        _service.Message.CC.Clear();
                        if (!String.IsNullOrEmpty(scheduledEmail.CC))
                            _service.Message.CC.Add(new MailAddress(scheduledEmail.CC));
                        _service.Message.Bcc.Clear();
                        if (!String.IsNullOrEmpty(scheduledEmail.BCC))
                            _service.Message.Bcc.Add(new MailAddress(scheduledEmail.BCC));
                        _service.Message.ReplyToList.Clear();
                        if (!String.IsNullOrEmpty(scheduledEmail.ReplyTo))
                            _service.Message.ReplyToList.Add(new MailAddress(scheduledEmail.ReplyTo));
                        _service.Message.Subject = scheduledEmail.Subject;
                        _service.Message.IsBodyHtml = true;
                        _service.Message.Body = scheduledEmail.Body;
                        _service.SendMail();

                        scheduledEmail.SendDate = DateTime.UtcNow;
                        scheduledEmail.IsEmailSend = true;
                        sERepo.Update(scheduledEmail);
                        uow.Context.SaveChanges();
                        uow.Commit();
                    }
                    catch (Exception ex)
                    {
                        uow.Rollback();
                    }
            }

        }

        public void SendTestEmail(string to, string replyTo, string subject, string body)
        {
            try
            {
                SendMailService _service = new SendMailService();
                _service.Message.To.Clear();
                foreach (var email in to.Split(';').ToList())
                {
                    _service.Message.To.Add(new MailAddress(email));
                }
                _service.Message.From = new MailAddress(_defaultEmail, "");
                _service.Message.CC.Clear();
                _service.Message.ReplyToList.Clear();
                if (!String.IsNullOrEmpty(replyTo))
                    _service.Message.ReplyToList.Add(new MailAddress(replyTo));
                _service.Message.Subject = subject;
                _service.Message.IsBodyHtml = true;
                _service.Message.Body = body;
                _service.SendMail();
            }
            catch (Exception ex)
            {

            }
        }
    }
}