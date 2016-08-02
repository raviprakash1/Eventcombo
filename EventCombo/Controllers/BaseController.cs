using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.DAL;
using EventCombo.Models;
using EventCombo.Service;
using AutoMapper;

namespace EventCombo.Controllers
{
    public class BaseController : Controller
    {
      protected IDBAccessService _dbservice;
      protected IMapper _mapper;
      protected IUnitOfWorkFactory _factory;

      public BaseController()
      {
        _factory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory(new EventComboEntities()));
        _mapper = AutomapperConfig.Config.CreateMapper();
        _dbservice = new DBAccessService(_factory, _mapper);
      }
      protected void PopulateBaseViewModel(IBaseViewModel model, string title = "")
      {
        if (model == null)
          throw new ArgumentNullException("model");

        if (String.IsNullOrWhiteSpace(title))
          model.BaseTitle = "Eventcombo";
        else
          model.BaseTitle = title;

        if (Session["AppId"] != null)
        {
          var profile = _dbservice.GetUserProfileById(Session["AppId"].ToString());
          if (profile != null)
          {
            model.BaseUserId = profile.UserID;
            model.BaseUserName = profile.FirstName + " " + profile.LastName;
            if (String.IsNullOrWhiteSpace(model.BaseUserName))
              model.BaseUserName = profile.Email;
            model.BaseUserEmail = profile.Email;
          }
        }
        else 
        {
          model.BaseUserId = null;
          model.BaseUserName = "Unknown";
          model.BaseUserEmail = null;
        }
      }
    }
}