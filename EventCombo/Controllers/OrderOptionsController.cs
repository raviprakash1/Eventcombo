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

    public OrderOptionsController()
    {
      _service = new OrderOptionsService(new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory()), AutomapperConfig.Config.CreateMapper());
    }

    private ActionResult DefaultAction()
    {
      return RedirectToAction("Index", "Home");
    }

    private void SetDefaultViewBag(string Message, bool isError, long eventId)
    {
      //Prepare Tickets
      ViewBag.Tickets = _service.GetTicketsViewModel(eventId);

      //Prepare OrderTemplateTypes
      ViewBag.OrderTemplateTypes = _service.GetOrderTemplateTypesViewModel();

      //Prepare OrderTemplateEventTypes
      ViewBag.OrderTemplateEventTypes = _service.GetOrderTemplateEventTypesViewModel();

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

      ViewBag.SendSettings = SendSettings;
      ViewBag.StateMessage = Message;
      ViewBag.IsError = isError;
      ViewBag.EventId = eventId;
    }

    [Authorize]
    [HttpGet]
    public ActionResult Form(long eventId)
    {

      if ((Session["AppId"] != null))
      {
        ValidationMessageController vmc = new ValidationMessageController();
        long lastEventId = vmc.GetLatestEventId(eventId);

        OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);
        SetDefaultViewBag("", false, eventId);

        if (orderTemplate == null)
          return DefaultAction();

        //if (Session["AppId"].ToString().Trim() != orderTemplate.UserID.Trim())
        //  return DefaultAction();

        HomeController hmc = new HomeController();
        hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
        string usernme = hmc.getusername();
        if (string.IsNullOrEmpty(usernme))
        {
          return DefaultAction();
        }
        Session["logo"] = "events";
        Session["Fromname"] = "ManageEvent";
        var url = Url.Action("Form", "OrderOptions");
        Session["ReturnUrl"] = "OrderOptions~" + url;

        ViewBag.EventId = lastEventId;
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
      ViewBag.EventId = orderTemplate.EventId;
      return View(ot);
    }

    [Authorize]
    [HttpGet]
    public ActionResult Confirmation(long eventId)
    {
      ValidationMessageController vmc = new ValidationMessageController();
      long lastEventId = vmc.GetLatestEventId(eventId);

      OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);
      SetDefaultViewBag("", false, eventId);

      if (orderTemplate == null)
        return DefaultAction();

      ViewBag.EventId = eventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Confirmation(OrderTemplateViewModel orderTemplate)
    {
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
      ViewBag.EventId = orderTemplate.EventId;
      return View(ot);
    }

    [Authorize]
    [HttpGet]
    public ActionResult EventType(long eventId)
    {
      ValidationMessageController vmc = new ValidationMessageController();
      long lastEventId = vmc.GetLatestEventId(eventId);

      OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);
      SetDefaultViewBag("", false, eventId);

      if (orderTemplate == null)
        return DefaultAction();

      ViewBag.EventId = eventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpPost]
    public ActionResult EventType(OrderTemplateViewModel orderTemplate)
    {
      try
      {
        if (_service.SaveOrderTemplateEventType(orderTemplate))
          SetDefaultViewBag("Event type saved.", false, orderTemplate.EventId);
        else
          SetDefaultViewBag("Event type didn't saved.", true, orderTemplate.EventId);
      }
      catch (Exception ex)
      {
        SetDefaultViewBag("Error. Confirmation is not saved." + ex.Message, true, orderTemplate.EventId);
      }

      OrderTemplateViewModel ot = _service.GetOrderTemplateViewModel(orderTemplate.EventId);
      ViewBag.EventId = orderTemplate.EventId;
      return View(ot);
    }

    [Authorize]
    [HttpGet]
    public ActionResult Waitlist(long eventId)
    {
      ValidationMessageController vmc = new ValidationMessageController();
      long lastEventId = vmc.GetLatestEventId(eventId);

      OrderTemplateViewModel orderTemplate = _service.GetOrderTemplateViewModel(lastEventId);
      SetDefaultViewBag("", false, eventId);

      if (orderTemplate == null)
        return DefaultAction();

      ViewBag.EventId = eventId;
      return View(orderTemplate);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Waitlist(OrderTemplateViewModel orderTemplate)
    {
      try
      {
        if (_service.SaveOrderTemplateWaitlist(orderTemplate))
          SetDefaultViewBag("Waitlist saved.", false, orderTemplate.EventId);
        else
          SetDefaultViewBag("Waitlist is not saved.", true, orderTemplate.EventId);
      }
      catch (Exception ex)
      {
        SetDefaultViewBag("Error. Confirmation is not saved." + ex.Message, true, orderTemplate.EventId);
      }

      OrderTemplateViewModel ot = _service.GetOrderTemplateViewModel(orderTemplate.EventId);
      ViewBag.EventId = orderTemplate.EventId;
      return View(ot);
    }
  }
}