using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;
using System.Web;

namespace EventCombo.Service
{
  public interface IECImageStorage
  {
    ECImageViewModel SaveTemp(HttpPostedFileBase file);
    ECImageViewModel SavePermanent(HttpPostedFileBase file);
    ECImageViewModel SavePermanent(ECImageViewModel src);
    ECImageViewModel GetExisting(ECImage src);
    void Delete(ECImageViewModel src);
    void Delete(ECImage src);
  }
}
