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
    IECImageService _iservice;
    public ImageAPIController()
    {
      var factory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory(new EventComboEntities()));
      var mapper = AutomapperConfig.Config.CreateMapper();
      _iservice = new ECImageService(factory, mapper, new ECImageStorage(mapper));
    }

    public ActionResult UploadImages(List<HttpPostedFileBase> files)
    {
      if (Session["AppId"] == null)
        return null;

      if (files == null)
        return null;

      List<ECImageViewModel> images = new List<ECImageViewModel>();
      foreach (HttpPostedFileBase file in files)
      {
        ECImageViewModel image = _iservice.SaveTempImage(file);
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

      string userId = Session["AppId"].ToString();
      ECImageViewModel image = JsonConvert.DeserializeObject<ECImageViewModel>(json);

      if (image.ECImageId == 0) // delete only temporary images
        _iservice.DeleteImage(image, userId);

      JsonNetResult res = new JsonNetResult();
      return res;
    }
  }
}