using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;

namespace EventCombo.Service
{
  public class ECImageService: IECImageService
  {
    public Func<string, string> ProcessFilePathFunc { get; set; }

    public Func<string, string> ProcessUrlPathFunc { get; set; }

    public ECImageViewModel SaveTempImage(HttpPostedFileBase file)
    {
      throw new NotImplementedException();
    }

    public void DeleteImage(ECImageViewModel image, string userId)
    {
      throw new NotImplementedException();
    }

    public void DeleteImage(ECImageViewModel image, string userId, IUnitOfWork uow)
    {
      throw new NotImplementedException();
    }

    public void DeleteImage(long imageId, string userId)
    {
      throw new NotImplementedException();
    }

    public void DeleteImage(long imageId, string userId, IUnitOfWork uow)
    {
      throw new NotImplementedException();
    }

    public ECImageViewModel SaveToDB(ECImageViewModel image, string userId)
    {
      throw new NotImplementedException();
    }

    public ECImageViewModel SaveToDB(ECImageViewModel image, string userId, IUnitOfWork uow)
    {
      throw new NotImplementedException();
    }
  }
}