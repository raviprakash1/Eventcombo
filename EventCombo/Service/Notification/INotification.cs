using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCombo.Service
{
  public interface INotification
  {
    string ReceiverName { get; set; }
    void SendNotification(ISendMailService _service);
  }
}
