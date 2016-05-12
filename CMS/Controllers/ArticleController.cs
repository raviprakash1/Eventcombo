using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using CMS.DAL;
using CMS.Service;
using AutoMapper;

namespace CMS.Controllers
{
  [Authorize]
  public class ArticleController : Controller
  {
    private IArticleService _service;

    public ArticleController()
    {
      var cfactory = new EventComboContextFactory(new CMS.Models.EmsEntities());
      _service = new ArticleService(new EntityFrameworkUnitOfWorkFactory(cfactory), AutomapperConfig.Config.CreateMapper());
    }

    [HttpGet]
    public ActionResult Index()
    {
      if ((Session["UserID"] != null))
      {
        var articles = _service.GetList(Url.Content("~/Images/ECImages/"));
        return View("Index", articles);
      }
      else
      {
        return RedirectToAction("Login", "Home");
      }
    }

    [HttpGet]
    public ActionResult Edit(long ArticleId)
    {
      if ((Session["UserID"] != null))
      {
        ArticleFullViewModel res;
        if (ArticleId == 0)
          res = _service.GetNewArticle();
        else
          res = _service.GetArticleByID(ArticleId, Url.Content("~/Images/ECImages/"));
        return View(res);
      }
      else
      {
        return RedirectToAction("Login", "Home");
      }
    }

    [HttpPost]
    public ActionResult Save(ArticleFullViewModel model)
    {
      if ((Session["UserID"] != null))
      {
        _service.SaveArticle(model, Server.MapPath("~/Images/ECImages/"));
        return RedirectToAction("Index", "Article");
      }
      else
      {
        return RedirectToAction("Login", "Home");
      }
    }

    [HttpGet]
    public ActionResult Delete(long articleId)
    {
      if ((Session["UserID"] != null))
      {
        _service.DeleteArticle(articleId, Server.MapPath("~/Images/ECImages/"));
        return RedirectToAction("Index", "Article");
      }
      else
      {
        return RedirectToAction("Login", "Home");
      }
    }

    [HttpGet]
    public ActionResult ShowArticle(long articleId)
    {
      if ((Session["UserID"] == null))
        return RedirectToAction("Login", "Home");

      ArticleFullViewModel article = _service.GetArticleByID(articleId, Url.Content("~/Images/ECImages/"));
      return View(article);
    }
  }
}