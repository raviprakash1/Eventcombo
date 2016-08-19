using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;
using System.IO;
using AutoMapper;

namespace EventCombo.Service
{
  public class ECImageService : IECImageService
  {
    private static readonly string[] allowedMime = { "image/jpeg", "image/png", "image/gif" };
    private IECImageStorage _storage;
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;

    public ECImageService(IUnitOfWorkFactory factory, IMapper mapper, IECImageStorage storage)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");
      if (storage == null)
        throw new ArgumentNullException("storage");

      _factory = factory;
      _mapper = mapper;
      _storage = storage;
    }

    private bool ValidateFile(HttpPostedFileBase file)
    {
      bool res;
      res = allowedMime.Contains(file.ContentType.ToLower());
      return res;
    }

    public ECImageViewModel SaveTempImage(HttpPostedFileBase file)
    {
      if (file == null)
        throw new ArgumentNullException("file");
      if (!ValidateFile(file))
        throw new ArgumentException("file");

      return _storage.SaveTemp(file);
    }

    private void CheckAccess(long imageId, string userId)
    {
      if (imageId == 0)
        return;
      // TODO: Checking of user access to ECImage
      if (false)
        throw new UnauthorizedAccessException("User " + userId + " tried to access to ECImageId = " + imageId.ToString());
    }

    private void CheckAccess(ECImageViewModel image, string userId)
    {
      CheckAccess(image.ECImageId, userId);
    }

    public void DeleteImage(ECImageViewModel image, string userId)
    {
      if (image == null)
        throw new ArgumentNullException("image");

      if (image.ECImageId == 0)
        _storage.Delete(image);
      else
        DeleteImage(image.ECImageId, userId);
    }

    public void DeleteImage(ECImageViewModel image, string userId, IUnitOfWork uow)
    {
      if (image == null)
        throw new ArgumentNullException("image");
      if (uow == null)
        throw new ArgumentNullException("uow");

      if (image.ECImageId == 0)
        _storage.Delete(image);
      else
        DeleteImage(image.ECImageId, userId, uow);
    }

    public void DeleteImage(long imageId, string userId)
    {
      if (imageId == 0)
        return;

      using (var uow = _factory.GetUnitOfWork())
        try
        {
          DeleteImage(imageId, userId, uow);
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception during DeleteImage.", ex);
        }
    }

    public void DeleteImage(long imageId, string userId, IUnitOfWork uow)
    {
      if (uow == null)
        throw new ArgumentNullException("uow");

      if (imageId == 0)
        return;

      CheckAccess(imageId, userId);

      IRepository<ECImage> ecRepo = new GenericRepository<ECImage>(uow.Context);
      ECImage imgDB = ecRepo.GetByID(imageId);
      if (imgDB != null)
      {
        ecRepo.Delete(imgDB);
        _storage.Delete(imgDB);
        uow.Context.SaveChanges();
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

    public ECImageViewModel SaveToDB(HttpPostedFileBase file, string userId)
    {
      if (file == null)
        throw new ArgumentNullException("file");

      if (!ValidateFile(file))
        throw new ArgumentException("file");

      ECImageViewModel newImage;
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          newImage = SaveToDB(file, userId, uow);
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception during SaveToDB.", ex);
        }
      return newImage;
    }

    public ECImageViewModel SaveToDB(HttpPostedFileBase file, string userId, IUnitOfWork uow)
    {
      if (file == null)
        throw new ArgumentNullException("file");
      if (uow == null)
        throw new ArgumentNullException("uow");

      if (!ValidateFile(file))
        throw new ArgumentException("file");

      ECImageViewModel image = _storage.SavePermanent(file);
      return SaveImage(image, userId, uow);
    }

    public ECImageViewModel SaveToDB(ECImageViewModel image, string userId)
    {
      if (image == null)
        throw new ArgumentNullException("image");

      ECImageViewModel newImage;
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          newImage = SaveToDB(image, userId, uow);
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception during SaveToDB.", ex);
        }
      return newImage;
    }

    public ECImageViewModel SaveToDB(ECImageViewModel image, string userId, IUnitOfWork uow)
    {
      if (image == null)
        throw new ArgumentNullException("image");
      if (uow == null)
        throw new ArgumentNullException("uow");

      CheckAccess(image, userId);
      if (image.ECImageId == 0)
        image = _storage.SavePermanent(image);
      return SaveImage(image, userId, uow);
    }

    public ECImageViewModel GetImageById(long id)
    {
      IRepository<ECImage> ecRepo = new GenericRepository<ECImage>(_factory.ContextFactory);
      var ecImage = ecRepo.GetByID(id);
      if (ecImage == null)
        return null;
      else
        return _storage.GetExisting(ecImage);
    }

    private ECImageViewModel SaveImage(ECImageViewModel image, string userId, IUnitOfWork uow)
    {
      IRepository<ECImage> ecRepo = new GenericRepository<ECImage>(uow.Context);
      ECImage imgDB = ecRepo.GetByID(image.ECImageId);
      if (imgDB == null)
        imgDB = _mapper.Map<ECImage>(image);
      else
        _mapper.Map(image, imgDB);
      if (imgDB.ECImageTypeId == 0)
        imgDB.ECImageTypeId = GetImageTypeId(image.TypeName, uow);
      if (imgDB.ECImageId == 0)
        ecRepo.Insert(imgDB);
      uow.Context.SaveChanges();
      return _storage.GetExisting(imgDB);

    }
  }
}