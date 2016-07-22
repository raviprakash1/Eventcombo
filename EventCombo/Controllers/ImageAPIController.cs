using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Service;
using EventCombo.Models;
using EventCombo.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace EventCombo.Controllers
{
  public class ImageAPIController : Controller
  {
    IImageService _iservice;
    public ImageAPIController()
    {
      _iservice = new ImageService();
    }

    public ActionResult UploadImages(List<HttpPostedFileBase> files)
    {
      if (Session["AppId"] == null)
        return null;

      if (files == null)
        return null;

      List<ImageViewModel> images = new List<ImageViewModel>();
      foreach (HttpPostedFileBase file in files)
      {
        ImageViewModel image = _iservice.SaveImage(file, Server.MapPath, Url.Content);
        if (image != null)
          images.Add(image);
      }

      JsonNetResult res = new JsonNetResult();
      res.Data = images;
      return res;
    }

    public ActionResult DeleteImage(string json)
    {
      if (Session["AppId"] == null)
        return null;

      ImageViewModel image = JsonConvert.DeserializeObject<ImageViewModel>(json);

      if (image.ImageType > 0) // not temporary image
        return null;

      image.MapPath = Server.MapPath;
      _iservice.DeleteImage(image);
      JsonNetResult res = new JsonNetResult();
      res.Data = image;
      return res;
    }
  }
}