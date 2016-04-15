using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCombo.Service
{
  public interface IManageEventService
  {
    int GetPromoCodeCount(long eventId);
  }
}
