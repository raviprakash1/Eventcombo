using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using System.IO;
using NLog;
using System.Web.Mvc;
using System.Web.Hosting;

namespace EventCombo.Service
{
  public class ECImageStorage : IECImageStorage
  {
    private const string TempPath = "/Images/Temp/";
    private const string PermanentPath = "/Images/ECImages/";

    private IMapper _mapper;
    private UrlHelper _urlHelper;
    private ILogger logger;

    public ECImageStorage(IMapper mapper)
    {
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _mapper = mapper;
      logger = LogManager.GetCurrentClassLogger();
      try
      {
        _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
      }
      catch (Exception ex)
      {
        _urlHelper = null;
        logger.Error(ex, "Error during creation of UrlHelper(HttpContext.Current.Request.RequestContext).", null);
      }
    }

    private string internalProcessFilePath(string p)
    {
      string res;
      try
      {
        res = HostingEnvironment.MapPath(p);
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Error during call of HostingEnvironment.MapPath(srcPath).", null);
        res = p;
      }
      return res;
    }

    private string internalProcessUrlPath(string p)
    {
      if (_urlHelper != null)
        return _urlHelper.Content(p);
      else
        return p;
    }

    private ECImageViewModel SaveFile(HttpPostedFileBase file, string path)
    {
      if (file == null)
        throw new ArgumentNullException("file");

      Directory.CreateDirectory(internalProcessFilePath(path));

      ECImageViewModel image = new ECImageViewModel();
      while (String.IsNullOrEmpty(image.Filename))
      {
        image.Filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        image.FilePath = internalProcessFilePath(path + image.Filename);
        if (File.Exists(image.FilePath))
          image.Filename = "";
      }
      file.SaveAs(image.FilePath);
      image.ImagePath = internalProcessUrlPath(path + image.Filename);
      image.TypeName = file.ContentType;

      return image;
    }

    public ECImageViewModel SaveTemp(HttpPostedFileBase file)
    {
      return SaveFile(file, TempPath);
    }

    public ECImageViewModel SavePermanent(HttpPostedFileBase file)
    {
      return SaveFile(file, PermanentPath);
    }


    public ECImageViewModel SavePermanent(ECImageViewModel src)
    {
      if (src == null)
        throw new ArgumentNullException("src");

      string srcPath;
      if (String.IsNullOrEmpty(src.FilePath))
      {
        if (src.ECImageId == 0)
          srcPath = internalProcessFilePath(TempPath + src.Filename);
        else
          srcPath = internalProcessFilePath(PermanentPath + src.Filename);
      }
      else
        srcPath = src.FilePath;

      if (!File.Exists(srcPath))
        throw new ArgumentException("Image file not found.");

      ECImageViewModel image = _mapper.Map<ECImageViewModel>(src);
      image.FilePath = internalProcessFilePath(PermanentPath + image.Filename);
      while (File.Exists(image.FilePath))
      {
        image.Filename = Guid.NewGuid().ToString() + Path.GetExtension(src.Filename);
        image.FilePath = internalProcessFilePath(PermanentPath + image.Filename);
      }
      File.Copy(srcPath, image.FilePath);

      return image;
    }

    public ECImageViewModel GetExisting(ECImage src)
    {
      if (src == null)
        throw new ArgumentNullException("src");

      ECImageViewModel image = _mapper.Map<ECImageViewModel>(src);
      image.FilePath = internalProcessFilePath(PermanentPath + src.ImagePath);
      image.ImagePath = internalProcessUrlPath(PermanentPath + src.ImagePath);

      return image;
    }

    public void Delete(ECImageViewModel src)
    {
      if (File.Exists(src.FilePath))
        try
        {
          File.Delete(src.FilePath);
        }
        catch (Exception ex)
        {
          logger.Error(ex, "Exception during image delete.", null);
        }
    }
    public void Delete(ECImage src)
    {
      Delete(GetExisting(src));
    }

  }
}