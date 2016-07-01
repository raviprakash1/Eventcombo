using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;
using System.IO;

namespace EventCombo.Service
{
  public class ImageService : IImageService
  {
    public ImageViewModel SaveImage(HttpPostedFileBase file, Func<string, string> mapPath, Func<string, string> urlPath)
    {
      ImageViewModel image = new ImageViewModel() 
      { 
        Id = 0, 
        ImageType = 0, 
        MapPath = mapPath, 
        Filename = "", 
        ContentType = file.ContentType.ToLower(),
        UrlPath = urlPath
      };
      Directory.CreateDirectory(image.FilePath);
      while (image.Filename == "")
      {
        image.Filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        if (File.Exists(image.FilePath))
          image.Filename = "";
      }
      file.SaveAs(image.FilePath);
      return image;
    }

    public void DeleteImage(ImageViewModel image)
    {
      if (File.Exists(image.FilePath))
        try
        {
          File.Delete(image.FilePath);
        }
        catch (Exception ex)
        {
          // suppress exception...
        }
    }

    private byte GetImageTypeId(string contentType, IUnitOfWork uow)
    {
      IRepository<ECImageType> itRepo = new GenericRepository<ECImageType>(uow.Context);
      contentType = contentType ?? "";
      var imageType = itRepo.Get(filter: (it => it.TypeName == contentType.ToLower())).SingleOrDefault();
      if (imageType == null)
      {
        imageType = new ECImageType() { TypeName = contentType.ToLower() };
        itRepo.Insert(imageType);
        uow.Context.SaveChanges();
      }
      return imageType.ECImageTypeId;
    }

    public long UpdateECImage(long ecImageId, ImageViewModel image, IUnitOfWork uow)
    {
      IRepository<ECImage> ecRepo = new GenericRepository<ECImage>(uow.Context);
      ImageViewModel newImage;

      ECImage ecimage = ecRepo.GetByID(ecImageId);
      if (ecimage == null)
        ecimage = new ECImage();

      ImageViewModel oldImage = new ImageViewModel() { ImageType = 1, Filename = ecimage.ImagePath, MapPath = image.MapPath };

      if (image.ImageType == 0)
        newImage = CopyImage(image, 1, true);
      else
        newImage = CopyImage(image, 1);

      if (oldImage.FilePath != newImage.FilePath)
        DeleteImage(oldImage);

      ecimage.ImagePath = newImage.Filename;
      ecimage.ECImageTypeId = GetImageTypeId(image.ContentType, uow);
      if (ecimage.ECImageId == 0)
        ecRepo.Insert(ecimage);
      uow.Context.SaveChanges();

      return ecimage.ECImageId;
    }

    public ImageViewModel CopyImage(ImageViewModel image, int newType, bool move = false)
    {
      var res = new ImageViewModel { ContentType = image.ContentType, Filename = image.Filename, ImageType = newType, MapPath = image.MapPath, UrlPath = image.UrlPath, Id = image.Id };
      while (File.Exists(res.FilePath))
        res.Filename = Guid.NewGuid().ToString() + Path.GetExtension(image.Filename);
      if (move)
        File.Move(image.FilePath, res.FilePath);
      else
        File.Copy(image.FilePath, res.FilePath);
      return res;
    }

    public void DeleteECImage(long ecImageId, IUnitOfWork uow, Func<string, string> mapPath)
    {
      IRepository<ECImage> ecRepo = new GenericRepository<ECImage>(uow.Context);
      ECImage image = ecRepo.GetByID(ecImageId);
      if (image != null)
      {
        DeleteImage(new ImageViewModel() { ImageType = 1, Filename = image.ImagePath, MapPath = mapPath });
        ecRepo.Delete(image);
      }
      uow.Context.SaveChanges();
    }
  }
}