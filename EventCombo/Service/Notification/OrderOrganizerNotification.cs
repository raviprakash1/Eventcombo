using EventCombo.DAL;
using EventCombo.Models;
using EventCombo.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Service
{
    public class OrderOrganizerNotification : INotification
    {
        private IUnitOfWorkFactory _factory;
        private IDBAccessService _dbservice;
        private string _receiver;
        private string _orderId;
        private string _baseUrl;
        private Attachment _attachment;
        private bool _isManualOrder;

        public OrderOrganizerNotification(IUnitOfWorkFactory factory, IDBAccessService dbService, string orderId, string baseUrl, Attachment attachment, bool isManualOrder = false)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            if (dbService == null)
                throw new ArgumentNullException("dbService");

            _factory = factory;
            _dbservice = dbService;
            _orderId = orderId;
            _baseUrl = baseUrl;
            _attachment = attachment;
            _isManualOrder = isManualOrder;
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
            string userName = string.Empty;

            IRepository<Email_Template> etRepo = new GenericRepository<Email_Template>(_factory.ContextFactory);
            IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
            IRepository<Order_Detail_T> oRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);

            var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "email_ticket_purchase")).SingleOrDefault();
            if (eTemplate == null)
                throw new Exception("Email template 'Contact_Event_Organizer' not found.");

            var ticketPurchasedDetail = tpdRepo.Get(filter: (t => t.TPD_Order_Id == _orderId));
            var orderDetail = oRepo.Get(filter: (t => t.O_Order_Id == _orderId)).FirstOrDefault();
            var profile = ticketPurchasedDetail.FirstOrDefault().AspNetUser.Profiles.FirstOrDefault();
            if ((profile != null) && (!String.IsNullOrWhiteSpace(profile.Email)))
            {
                userEmail = profile.Email;
                userName = profile.FirstName;
            }
            var emailOrder = orderDetail.O_Email != null ? orderDetail.O_Email : "";
            var tagList = LoadTagList();
            tagList["EventOrganiserEmail"] = userEmail;
            tagList["EventOrganiserName"] = userName;
            tagList["UserEmailID"] = emailOrder;
            tagList["UserFirstNameID"] = userName;
            tagList["TicketQty"] = (ticketPurchasedDetail.Sum(s => s.TPD_Purchased_Qty) ?? 0).ToString();
            tagList["TicketPrice"] = ((ticketPurchasedDetail.Sum(s => s.TPD_Amount) ?? 0) + (ticketPurchasedDetail.Sum(s => s.TPD_Donate) ?? 0)).ToString();
            tagList["EventOrderNO"] = _orderId;
            tagList["EventTitleId"] = ticketPurchasedDetail.FirstOrDefault().Event.EventTitle;
            tagList["ClickHere"] = _baseUrl + "/Home/Index";

            _service.Message.To.Clear();
            if (String.IsNullOrEmpty(eTemplate.To))
                _service.Message.To.Add(new MailAddress(userEmail, userName));
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
