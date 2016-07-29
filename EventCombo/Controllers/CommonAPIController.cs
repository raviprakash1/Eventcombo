using AutoMapper;
using EventCombo.DAL;
using EventCombo.Models;
using EventCombo.Service;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Controllers
{
  public class CommonAPIController : Controller
  {
    private IEventService _eService;
    private IMapper _mapper;
    private IUnitOfWorkFactory _factory;

    public CommonAPIController()
      : base()
    {
      _factory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory(new EventComboEntities()));
      _mapper = AutomapperConfig.Config.CreateMapper();
      _eService = new EventService(_factory, _mapper);
    }

    public ActionResult FilterEventsByTitle(string title)
    {
      IEnumerable<EventTitleSearchViewModel> ev = _eService.Search(title);

      JsonNetResult res = new JsonNetResult();
      res.Data = ev;

      return res;
    }
  }
}