using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EventCombo.Service
{
  public interface ISendMailService
  {
    MailMessage Message { get; }
    void SendMail();
  }
}
