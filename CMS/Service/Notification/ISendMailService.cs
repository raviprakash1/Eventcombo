using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
  public interface ISendMailService
  {
    MailMessage Message { get; }
    void SendMail();
  }
}
