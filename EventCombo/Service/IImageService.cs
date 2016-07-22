using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;

namespace EventCombo.Service
{
  interface IImageService
  {
    ImageViewModel SaveImage(HttpPostedFileBase file, Func<string, string> mapPath, Func<string, string> urlPath);

    void DeleteImage(ImageViewModel image);

    ImageViewModel CopyImage(ImageViewModel image, int newType, bool move = false);

    long UpdateECImage(long ecImageId, ImageViewModel image, IUnitOfWork uow);

    void DeleteECImage(long ecImageId, IUnitOfWork uow, Func<string, string> mapPath);
  }
}
