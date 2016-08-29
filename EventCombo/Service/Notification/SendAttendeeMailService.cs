using EventCombo.DAL;
using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EventCombo.Service
{
    public class SendAttendeeMailService : ISendMailService
    {
        MailMessage _message = new MailMessage();
        private IUnitOfWorkFactory _factory;
        private string _userId;
        private List<TicketBearer> _ticketBearer;
        private DateTime _scheduledDate;
        private byte _emailTypeId;
        public DateTime ScheduledDate
        {
            get { return _scheduledDate; }
            set { _scheduledDate = value; }
        }

        public SendAttendeeMailService(IUnitOfWorkFactory factory, string userId, List<TicketBearer> ticketBearer, DateTime scheduledDate)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (ticketBearer == null)
                throw new ArgumentNullException("ticketBearer");

            _factory = factory;
            _userId = userId;
            _ticketBearer = ticketBearer;
            _scheduledDate = scheduledDate;
            _emailTypeId = 1;

        }
        public MailMessage Message
        {
            get
            {
                return _message;
            }
        }

        public void SendMail()
        {
            using (var uow = _factory.GetUnitOfWork())
                try
                {
                    IRepository<ScheduledEmail> sERepo = new GenericRepository<ScheduledEmail>(_factory.ContextFactory);
                    IRepository<AttendeeEmail> aERepo = new GenericRepository<AttendeeEmail>(_factory.ContextFactory);

                    ScheduledEmail scheduledEmail = new ScheduledEmail()
                    {
                        UserId = _userId,
                        EmailTypeId = _emailTypeId,
                        SendFrom = Message.From.Address,
                        SendTo = string.Join(";", Message.To),
                        ReplyTo = string.Join(";", Message.ReplyToList),
                        CC = string.Join(";", Message.CC),
                        BCC = string.Join(";", Message.Bcc),
                        Subject = Message.Subject,
                        Body = Message.Body,
                        ScheduledDate = ScheduledDate,
                        IsEmailSend=false,
                        CreateDate=DateTime.UtcNow,
                        SendDate=null
                    };
                    sERepo.Insert(scheduledEmail);
                    uow.Context.SaveChanges();
                    foreach (var item in _ticketBearer)
                    {
                        AttendeeEmail attendeeEmail = new AttendeeEmail()
                        {
                            ScheduledEmailId = scheduledEmail.ScheduledEmailId,
                            TicketbearerId = item.TicketbearerId
                        };
                        aERepo.Insert(attendeeEmail);
                    }
                    uow.Context.SaveChanges();
                    uow.Commit();
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new Exception("Error during SendMail", ex);
                }
        }
    }
}