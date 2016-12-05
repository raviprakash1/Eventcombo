using EventCombo.DAL;
using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EventCombo.Service
{
    class GiveAwayNotification : INotification
    {
        private IUnitOfWorkFactory _factory;
        private GiveAwayViewModel _message;
        private string _defaultEmail;

        public GiveAwayNotification(IUnitOfWorkFactory factory, GiveAwayViewModel message, string defaultEmail)
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
            _service.Message.To.Add("editor@eventcombo.com");
            _service.Message.Subject = "10KGiveaway - " + _message.OrgName;
            _service.Message.Body = String.Format(@"<h1 style='text-align:center;'>
                                                      <span style='text-decoration:underline; '>
                                                           <span style='text-align:center;color:#0000ff;text-decoration:underline;'>10KGiveaway</span>
                                                        </span></h1>
                                                      <table style='height: 550px;' border='1' width='450'>
                                                        <tbody>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Full Name</h4></td>
                                                                <td>&nbsp;&nbsp;{0}&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Email</h4></td>
                                                                <td>&nbsp;&nbsp;{1}&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Phone Number&nbsp;</h4></td>
                                                                <td>&nbsp;&nbsp;{2}&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Organization Name</h4></td>
                                                                <td>&nbsp;&nbsp;{3}&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Type Of Event</h4></td>
                                                                <td><p>&nbsp;&nbsp;{4}&nbsp;</p></td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Frequency Of Event</h4></td>
                                                                <td>&nbsp;&nbsp;{5}&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Location Of Event</h4></td>
                                                                <td>&nbsp;&nbsp;{6}&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<h4 style='color: #2e6c80;'>&nbsp;Where did you hear about this contest</h4></td>
                                                                <td>&nbsp;&nbsp;{7}&nbsp;</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>",
                                                    _message.FullName, _message.Email, _message.Phone, _message.OrgName, _message.EventType, _message.EventFrequency,
                                                    _message.EventLocation, _message.WhereHear);
            _service.Message.IsBodyHtml = true;
            _service.Message.From = new MailAddress(_defaultEmail, "Event Combo");
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