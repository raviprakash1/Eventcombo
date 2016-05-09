using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using EventCombo.Service;
using EventCombo.DAL;

namespace EventCombo.Controllers
{
  public class OrderOptionsController : Controller
  {
    private IOrderOptionsService _service;
    private IManageEventService _manageService;

    public OrderOptionsController()
    {
      _service = new OrderOptionsService(new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory()), AutomapperConfig.Config.CreateMapper());
      _manageService = new ManageEventService(new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory()));
    }

    private ActionResult DefaultAction()
    {
      return RedirectToAction("Index", "Home");
    }

    private void SetDefaultViewBag(string Message, bool isError, long eventId)
    {
      ViewBag.Tickets = _service.GetTicketsViewModel(eventId);

      ViewBag.OrderTemplateTypes = _service.GetOrderTemplateTypesViewModel();

      ViewBag.OrderTemplateEventTypes = _service.GetOrderTemplateEventTypesViewModel();

      ViewBag.Languages = _service.GetLanguagesViewModel();

      ViewBag.ControlType = _service.GetControlTypesViewModel();

      ViewBag.QuestionGroups = _service.GetQuestionTypeGroups();

      ViewBag.GroupTypes = _service.GetOrderTemplateGroupTypes();

      //Prepare SelectedItems for Confirmation View
      List<SelectListItem> SendSettings = new List<SelectListItem>();
      SendSettings.Add(new SelectListItem()
        {
          Text = "Same settings for all ticket types",
          Value = false.ToString(),
          Selected = true
        });
      SendSettings.Add(new SelectListItem()
        {
          Text = "Custom settings for each ticket type",
          Value = true.ToString(),
          Selected = false
        });

      ViewBag.EventTitle = _service.GetEventTitle(eventId);
      ViewBag.SendSettings = SendSettings;
      ViewBag.StateMessage = Message;
      ViewBag.IsError = isError;
      ViewBag.EventId = eventId;
      ViewBag.DiscountCode = _manageService.GetPromoCodeCount(eventId);
    }

    private bool CheckSecurity(string UserId)
    {
      HomeController hmc = new HomeController();
      hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
      string usernme = hmc.getusername();
      return ((Session["AppId"].ToString().Trim() == UserId.Trim()) && !string.IsNullOrEmpty(usernme));
    }

    [Authorize]
    [HttpGet]
    public ActionResult Form(long eventId)
    {

      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("Form", "OrderOptions", new { eventId = eventId });

        ValidationMessageController vmc = new ValidationMessageController();
        long lastEventId = vmc.GetLatestEventId(eventId);

        OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);

        if (orderTemplate == null)
          return DefaultAction();
        if (!CheckSecurity(orderTemplate.UserID))
          return DefaultAction();

        SetDefaultViewBag("", false, lastEventId);
        return View(orderTemplate);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpPost]
    public ActionResult Form(OrderTemplateViewModel orderTemplate)
    {
      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("Form", "OrderOptions", new { eventId = orderTemplate.EventId });

        try
        {
          if (_service.SaveOrderTemplateForm(orderTemplate))
            SetDefaultViewBag("Order options saved.", false, orderTemplate.EventId);
          else
            SetDefaultViewBag("Order options is not saved.", true, orderTemplate.EventId);
        }
        catch (Exception ex)
        {
          SetDefaultViewBag("Error. Order options is not saved." + ex.Message, true, orderTemplate.EventId);
        }

        OrderTemplateViewModel ot = _service.GetOrderTemplateViewModel(orderTemplate.EventId);
        return View(ot);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpGet]
    public ActionResult Confirmation(long eventId)
    {
      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("Confirmation", "OrderOptions", new { eventId = eventId });

        ValidationMessageController vmc = new ValidationMessageController();
        long lastEventId = vmc.GetLatestEventId(eventId);

        OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);

        if (orderTemplate == null)
          return DefaultAction();
        if (!CheckSecurity(orderTemplate.UserID))
          return DefaultAction();

        SetDefaultViewBag("", false, lastEventId);
        return View(orderTemplate);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpPost]
    public ActionResult Confirmation(OrderTemplateViewModel orderTemplate)
    {
      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("Confirmation", "OrderOptions", new { eventId = orderTemplate.EventId });

        try
        {
          if (_service.SaveOrderTemplateConfirmation(orderTemplate))
            SetDefaultViewBag("Confirmation saved.", false, orderTemplate.EventId);
          else
            SetDefaultViewBag("Confirmation is not saved.", true, orderTemplate.EventId);
        }
        catch (Exception ex)
        {
          SetDefaultViewBag("Error. Confirmation is not saved." + ex.Message, true, orderTemplate.EventId);
        }

        OrderTemplateViewModel ot = _service.GetOrderTemplateViewModel(orderTemplate.EventId);
        return View(ot);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpGet]
    public ActionResult EventType(long eventId)
    {
      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("EventType", "OrderOptions", new { eventId = eventId });

        ValidationMessageController vmc = new ValidationMessageController();
        long lastEventId = vmc.GetLatestEventId(eventId);

        OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);

        if (orderTemplate == null)
          return DefaultAction();
        if (!CheckSecurity(orderTemplate.UserID))
          return DefaultAction();

        SetDefaultViewBag("", false, lastEventId);
        return View(orderTemplate);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpPost]
    public ActionResult EventType(OrderTemplateViewModel orderTemplate)
    {
      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("EventType", "OrderOptions", new { eventId = orderTemplate.EventId });

        try
        {
          if (_service.SaveOrderTemplateEventType(orderTemplate))
            SetDefaultViewBag("Event type saved.", false, orderTemplate.EventId);
          else
            SetDefaultViewBag("Event type didn't saved.", true, orderTemplate.EventId);
        }
        catch (Exception ex)
        {
          SetDefaultViewBag("Error. Event type is not saved." + ex.Message, true, orderTemplate.EventId);
        }

        OrderTemplateViewModel ot = _service.GetOrderTemplateViewModel(orderTemplate.EventId);
        return View(ot);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpGet]
    public ActionResult Waitlist(long eventId)
    {
      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("Waitlist", "OrderOptions", new { eventId = eventId });

        ValidationMessageController vmc = new ValidationMessageController();
        long lastEventId = vmc.GetLatestEventId(eventId);

        OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);

        if (orderTemplate == null)
          return DefaultAction();
        if (!CheckSecurity(orderTemplate.UserID))
          return DefaultAction();

        SetDefaultViewBag("", false, lastEventId);
        return View(orderTemplate);
      }
      else
      {
        return DefaultAction();
      }
    }

    [Authorize]
    [HttpPost]
    public ActionResult Waitlist(OrderTemplateViewModel orderTemplate)
    {
      if ((Session["AppId"] != null))
      {
        Session["logo"] = "events";
        Session["Fromname"] = "OrderOptions";
        Session["ReturnUrl"] = Url.Action("Waitlist", "OrderOptions", new { eventId = orderTemplate.EventId });

        try
        {
          if (_service.SaveOrderTemplateWaitlist(orderTemplate))
            SetDefaultViewBag("Waitlist saved.", false, orderTemplate.EventId);
          else
            SetDefaultViewBag("Waitlist is not saved.", true, orderTemplate.EventId);
        }
        catch (Exception ex)
        {
          SetDefaultViewBag("Error. Waitlist is not saved." + ex.Message, true, orderTemplate.EventId);
        }

        OrderTemplateViewModel ot = _service.GetOrderTemplateViewModel(orderTemplate.EventId);
        return View(ot);
      }
      else
      {
        return DefaultAction();
      }
    }
  }
}