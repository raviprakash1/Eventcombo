using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EventCombo.Service;
using EventCombo.DAL;
using EventCombo.Utils;

namespace EventCombo.Controllers
{
  public class TicketPurchaseController : BaseController
  {
    private IPurchasingService _pService;

    public TicketPurchaseController()
      : base()
    {
      _pService = new PurchasingService(_factory, _mapper);
    }

    [HttpPost]
    public ActionResult LockTickets(string json)
    {
      TicketLockRequest req = JsonConvert.DeserializeObject<TicketLockRequest>(json);
      req.IP = ClientIPAddress.GetLanIPAddress(Request);

      if (Session["AppId"] != null)
        req.UserId = Session["AppId"].ToString();
      else
        req.UserId = "";
      

      TicketLockResult tres = _pService.TryLockTickets(req);

      JsonNetResult res = new JsonNetResult();
      res.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
      res.SerializerSettings.Converters.Add(new StringEnumConverter());
      res.Data = tres;
      return res;
    }

    [HttpGet]
    public ActionResult Checkout(string lockId)
    {
      IBaseViewModel model = new BaseViewModel();
      PopulateBaseViewModel(model, String.Format("{0} | Eventcombo", "Event name must be here"));
      return View(model);
    }
  }
}