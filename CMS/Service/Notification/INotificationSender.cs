using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{

  public interface INotificationSender
  {
    void Send(MailAddress address);
    void Send(string Name, string EMail);
    void Send(IEnumerable<MailAddress> addresses);
    void SendSeparately(IEnumerable<MailAddress> addresses, PrepareNotification prepareFunc = null);
  }
}
