using AutoMapper;
using EventCombo.DAL;
using EventCombo.Models;
using EventCombo.Service;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Controllers
{
  public class NotificationAPIController : BaseController
  {
    private INotificationService _nService;
    private ILogger _logger;
    public NotificationAPIController()
      : base()
    {
      _logger = LogManager.GetCurrentClassLogger();
      _nService = new NotificationService(_factory, _mapper);
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult SendOrganizer(string json)
    {
      ActionResultViewModel result = new ActionResultViewModel()
      {
        Success = false,
        ErrorCode = 1,
        ErrorMessage = "Error while sending email."
      };

      string userId = Session["AppId"] == null ? "" : Session["AppId"].ToString();

      try
      {
        EventNotificationViewModel en = JsonConvert.DeserializeObject<EventNotificationViewModel>(json);
        _nService.SendContactOrganizerMessage(en, userId);
        result.Success = true;
        result.ErrorCode = 0;
        result.ErrorMessage = "";
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error while processing SendOrganizer action, null");
      }

      JsonNetResult res = new JsonNetResult();
      res.Data = result;
      return res;
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult ShareFriends(string json)
      {
        ActionResultViewModel result = new ActionResultViewModel()
        {
          Success = false,
          ErrorCode = 1,
          ErrorMessage = "Error while sending email."
        };

        try 
        { 
          EventNotificationViewModel en = JsonConvert.DeserializeObject<EventNotificationViewModel>(json);
          _nService.SendEmailFreindsMessage(en);
          result.Success = true;
          result.ErrorCode = 0;
          result.ErrorMessage = "";
        }
        catch (Exception ex)
        {
          _logger.Error(ex, "Error while processing ShareFriends action, null");
        }

        JsonNetResult res = new JsonNetResult();
        res.Data = result;
        return res;
      }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCategory()
        {
            return Json(new string[] { "Event Organizer", "Event Attendee", "General" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetSubCategory(string category)
        {
            string[] subCategorys;

            if (category == "Event Organizer")
            {
                subCategorys = new string[]
                {
                    "Fees (SubCat)",
                    "Payouts & Invoices",
                    "Tickets & Inventory",
                    "Event Listing",
                    "Managing Orders & Attendees",
                    "Attendee Info",
                    "Invitations & Promo Tools",
                    "Reporting & Analytics",
                    "Equipment & Partnerships",
                    "Trust & Safety",
                    "Account Settings"
                };
            }
            else if (category == "Event Attendee")
            {
                subCategorys = new string[]
                {
                    "Tickets and Refunds",
                    "Managing My Order",
                    "Questions About Event",
                    "Buying Tickets or Registering",
                    "Profile & Account",
                    "Trust and Safety",
                    "Unrecognized Charge"
                };
            }
            else
            {
                subCategorys = new string[]
                {
                    "API & Developer Info",
                    "Legal or Security"
                };
            }

            return Json(subCategorys, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendContactEmail(ContactMessageViewModel contactMessageViewModel)
        {
            string defaultEmail;

            defaultEmail = System.Configuration.ConfigurationManager.AppSettings.Get("DefaultEmail");
            ContactMessageNotification notification = new ContactMessageNotification(
                                                        new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory()),
                                                        contactMessageViewModel,
                                                        defaultEmail
                                                        );
            notification.SendNotification(new SendMailService());
            return Json("true", JsonRequestBehavior.AllowGet);
        }

    }
}