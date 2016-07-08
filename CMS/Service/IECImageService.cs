using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;
using System.Web;
using CMS.DAL;

namespace CMS.Service
{
  interface IECImageService
  {
    ECImageViewModel SaveTempImage(HttpPostedFileBase file);
    void DeleteImage(ECImageViewModel image, string userId);
    void DeleteImage(ECImageViewModel image, string userId, IUnitOfWork uow);
    void DeleteImage(long imageId, string userId);
    void DeleteImage(long imageId, string userId, IUnitOfWork uow);
    ECImageViewModel SaveToDB(HttpPostedFileBase file, string userId);
    ECImageViewModel SaveToDB(HttpPostedFileBase file, string userId, IUnitOfWork uow);
    ECImageViewModel SaveToDB(ECImageViewModel image, string userId);
    ECImageViewModel SaveToDB(ECImageViewModel image, string userId, IUnitOfWork uow);
  }
}
