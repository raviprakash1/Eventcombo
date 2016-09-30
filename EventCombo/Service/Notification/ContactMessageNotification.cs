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
    class ContactMessageNotification : INotification
    {
        private IUnitOfWorkFactory _factory;
        private ContactMessageViewModel _message;
        private string _defaultEmail;

        public ContactMessageNotification(IUnitOfWorkFactory factory, ContactMessageViewModel message, string defaultEmail)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (message == null)
                throw new ArgumentNullException("message");
            if (String.IsNullOrEmpty(defaultEmail))
                throw new ArgumentNullException("defaultEmail");

            _factory = factory;
            _message = message;
            _defaultEmail = defaultEmail;
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

            IRepository<Email_Template> etRepo = new GenericRepository<Email_Template>(_factory.ContextFactory);
            var eTemplate = etRepo.Get(filter: (et => et.Template_Tag == "Contact_EventCombo")).SingleOrDefault();
            if (eTemplate == null)
                throw new Exception("Email template 'Contact_EventCombo' not found.");

            var tagList = LoadTagList();
            tagList["UserEmailID"] = _message.Email;
            tagList["UserFirstNameID"] = _message.Name;
            tagList["CategoryName"] = _message.Category;
            tagList["SubCategoryName"] = _message.SubCategory;
            tagList["UserPhone"] = _message.PhoneNo;
            tagList["MessageBody"] = _message.Message;

            _service.Message.To.Clear();
            if (String.IsNullOrEmpty(eTemplate.To))
                _service.Message.To.Add(new MailAddress(_defaultEmail));
            else
                _service.Message.To.Add(ReplaceTags(eTemplate.To, tagList));

            string fromAddress = String.IsNullOrEmpty(eTemplate.From) ? _defaultEmail : ReplaceTags(eTemplate.From, tagList);
            _service.Message.From = new MailAddress(fromAddress, String.IsNullOrEmpty(eTemplate.From_Name) ? fromAddress : ReplaceTags(eTemplate.From_Name, tagList));

            _service.Message.ReplyToList.Clear();
            if (!String.IsNullOrEmpty(_message.Email))
                _service.Message.ReplyToList.Add(new MailAddress(_message.Email, _message.Name));

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

        private string _receiver;
        public string ReceiverName
        {
            get { return _receiver; }
            set { _receiver = value; }
        }
    }
}
