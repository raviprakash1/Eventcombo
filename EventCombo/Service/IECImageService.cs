using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;
using System.Web;
using EventCombo.DAL;

namespace EventCombo.Service
{
  interface IECImageService
  {
    Func<string, string> ProcessFilePathFunc { get; set; }
    Func<string, string> ProcessUrlPathFunc { get; set; }

    ECImageViewModel SaveTempImage(HttpPostedFileBase file);
    void DeleteImage(ECImageViewModel image, string userId);
    void DeleteImage(ECImageViewModel image, string userId, IUnitOfWork uow);
    void DeleteImage(long imageId, string userId);
    void DeleteImage(long imageId, string userId, IUnitOfWork uow);
    ECImageViewModel SaveToDB(ECImageViewModel image, string userId);
    ECImageViewModel SaveToDB(ECImageViewModel image, string userId, IUnitOfWork uow);
  }
}
