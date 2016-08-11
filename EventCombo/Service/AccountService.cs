using AutoMapper;
using EventCombo.DAL;
using EventCombo.Models;
using EventCombo.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EventCombo.Service
{
  public class AccountService : IAccountService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private Logger _logger;

    public AccountService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
      _logger = LogManager.GetCurrentClassLogger();
    }

    public bool CheckUserName(string userName)
    {
      IRepository<AspNetUser> uRepo = new GenericRepository<AspNetUser>(_factory.ContextFactory);
      return uRepo.Get(filter: (u => u.UserName == userName.Trim())).Any();
    }


    public bool RegisterLoginAttempt(string userId, string ipAddress)
    {
      bool result = false;
      GeoAddress geoAddress = null;
      try
      {
        Ip2Geo ip2Geo = new Ip2Geo();
        geoAddress = ip2Geo.GetAddress(ipAddress);
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error while using Ip2Geo.", null);
        geoAddress = new GeoAddress()
        {
          cityName = "",
          countryName = "",
          regionName = ""
        };
      }

      using (var uow = _factory.GetUnitOfWork())
        try
        {
          IRepository<EventCombo.Models.Profile> pRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
          var profile = pRepo.Get(filter: (p => p.UserID == userId)).First();

          result = (profile != null) && (String.IsNullOrEmpty(profile.UserStatus) || (profile.UserStatus.ToUpper() == "Y"));
          if (result)
          {

            IRepository<AspNetUser> uRepo = new GenericRepository<AspNetUser>(_factory.ContextFactory);
            var aspuser = uRepo.Get(filter: (u => u.Id == userId)).First();
            aspuser.LoginStatus = "Y";
            aspuser.LastLoginTime = System.DateTime.UtcNow;

            profile.Ipcity = geoAddress.cityName;
            profile.Ipcountry = geoAddress.countryName;
            profile.IpState = geoAddress.regionName;

            uow.Context.SaveChanges();
            uow.Commit();
          }
        }
        catch (Exception ex)
        {
          _logger.Fatal(ex, "Error during RegisterLoginAttempt occurs.", null);
          uow.Rollback();
          result = false;
        }
      return result;
    }

    public void RegisterLogout(string userId)
    {
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          IRepository<AspNetUser> uRepo = new GenericRepository<AspNetUser>(_factory.ContextFactory);
          var aspuser = uRepo.Get(filter: (u => u.Id == userId)).First();
          if (aspuser != null)
          {
            aspuser.LoginStatus = "N";
            uow.Context.SaveChanges();
            uow.Commit();
          }

        }
        catch (Exception ex)
        {
          _logger.Fatal(ex, "Error during RegisterLogout occurs.", null);
          uow.Rollback();
        }
    }

    public bool RegisterNewUser(string userId, RegisterUserRequestViewModel user, string ipAddress)
    {
      bool result;

      using (var uow = _factory.GetUnitOfWork())
        try
        {
          IRepository<User_Permission_Detail> updRepo = new GenericRepository<User_Permission_Detail>(_factory.ContextFactory);
          IRepository<EventCombo.Models.Profile> pRepo = new GenericRepository<EventCombo.Models.Profile>(_factory.ContextFactory);
          for (var i = 1; i < 3; i++)
          {
            User_Permission_Detail upd = new User_Permission_Detail()
            {
              UP_User_Id = userId,
              UP_Permission_Id = i
            };
            updRepo.Insert(upd);
          }

          EventCombo.Models.Profile profile = new EventCombo.Models.Profile();
          profile.UserID = userId;
          profile.Email = user.Email;
          profile.FirstName = user.FirstName;
          profile.LastName = user.LastName;
          profile.UserStatus = "Y";
          profile.Organiser = "Y";
          pRepo.Insert(profile);

          uow.Context.SaveChanges();
          uow.Commit();
          result = true;
        }
        catch (Exception ex)
        {
          _logger.Fatal(ex, "Error during RegisterNewUser occurs.", null);
          uow.Rollback();
          result = false;
        }

      result = result || RegisterLoginAttempt(userId, ipAddress);

      try
      {
        NewUserWelcomeMessage mess = new NewUserWelcomeMessage(_factory, user.Email, ConfigurationManager.AppSettings.Get("DefaultEmail"));
        mess.SendNotification(new SendMailService());
      }
      catch (Exception ex)
      {
        _logger.Fatal(ex, "Exception during sending welcome message occurs.");
      }

      return result;
    }
  }
}