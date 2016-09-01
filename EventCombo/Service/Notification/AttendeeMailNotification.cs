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
        private List<TicketBearer> _ticketBearers;

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

        public AttendeeMailNotification(IUnitOfWorkFactory factory, string defaultEmail, ScheduledEmailViewModel scheduledEmail, List<TicketBearer> ticketBearers)
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
            var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "attendee_email")).SingleOrDefault();
            if (eTemplate == null)
            {
                List<KeyValuePair<string, string>> tagList = new List<KeyValuePair<string, string>>();
                //todo:tag
                _service.Message.To.Clear();
                foreach (var email in _ticketBearers.Select(a => a.Email))
                {
                    _service.Message.To.Add(new MailAddress(email));
                }
                string fromAddress = String.IsNullOrEmpty(_scheduledEmail.SendFrom) ? _defaultEmail : ReplaceTags(_scheduledEmail.SendFrom, tagList);
                _service.Message.From = new MailAddress(fromAddress, "");
                _service.Message.CC.Clear();
                if (!String.IsNullOrEmpty(_scheduledEmail.CC))
                    _service.Message.CC.Add(new MailAddress(ReplaceTags(_scheduledEmail.CC, tagList)));
                _service.Message.Bcc.Clear();
                if (!String.IsNullOrEmpty(_scheduledEmail.BCC))
                    _service.Message.Bcc.Add(new MailAddress(ReplaceTags(_scheduledEmail.BCC, tagList)));
                _service.Message.ReplyToList.Clear();
                _service.Message.ReplyToList.Add(new MailAddress(_scheduledEmail.ReplyTo));
                _service.Message.Subject = _scheduledEmail.Subject;
                _service.Message.IsBodyHtml = true;
                _service.Message.Body = ReplaceTags(new MvcHtmlString(HttpUtility.HtmlDecode(_scheduledEmail.Body)).ToHtmlString(), tagList);
                _service.SendMail();
            }
            else
            {

                List<KeyValuePair<string, string>> tagList = new List<KeyValuePair<string, string>>();

                _service.Message.To.Clear();
                _service.Message.To.Add(new MailAddress(ReplaceTags(eTemplate.To, tagList)));
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
    }
}