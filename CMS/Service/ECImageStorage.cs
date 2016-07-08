using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Models;
using System.IO;
using NLog;

namespace CMS.Service
{
  public class ECImageStorage : IECImageStorage
  {
    private const string TempPath = "/Images/Temp/";
    private const string PermanentPath = "/Images/ECImages/";

    private Func<string, string> _processFilePathFunc { get; set; }
    private Func<string, string> _processUrlPathFunc { get; set; }
    private IMapper _mapper;

    private ILogger logger;

    public ECImageStorage(IMapper mapper, Func<string, string> filePathFunc = null, Func<string, string> urlPathFunc = null)
    {
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _mapper = mapper;
      _processFilePathFunc = filePathFunc;
      _processUrlPathFunc = urlPathFunc;
      logger = LogManager.GetCurrentClassLogger();
    }

    private string internalProcessFilePath(string p)
    {
      return _processFilePathFunc == null ? p : _processFilePathFunc(p);
    }

    private string internalProcessUrlPath(string p)
    {
      return _processUrlPathFunc == null ? p : _processUrlPathFunc(p);
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
      if (src.ECImageId != 0)
        throw new ArgumentException("Image is not temporary.");
      if (!File.Exists(src.FilePath))
        throw new ArgumentException("Image file not found.");

      ECImageViewModel image = _mapper.Map<ECImageViewModel>(src);
      image.FilePath = internalProcessFilePath(PermanentPath + image.Filename);
      while (File.Exists(image.FilePath))
      {
        image.Filename = Guid.NewGuid().ToString() + Path.GetExtension(src.Filename);
        image.FilePath = internalProcessFilePath(PermanentPath + image.Filename);
      }
      image.ImagePath = internalProcessUrlPath(PermanentPath + image.Filename);

      File.Move(src.FilePath, image.FilePath);

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