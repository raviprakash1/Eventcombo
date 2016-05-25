﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.DAL;
using EventCombo.Service;
using EventCombo.Models;
using AutoMapper;

namespace EventCombo.Controllers
{
  public class ArticleController : Controller
  {
    private IArticleService _service;
    public ArticleController()
    {
      var cfactory = new EventComboContextFactory(new EventComboEntities());
      _service = new ArticleService(new EntityFrameworkUnitOfWorkFactory(cfactory), AutomapperConfig.Config.CreateMapper());
    }

    [HttpGet]
    public ActionResult ShowArticle(long? articleId, string articleTitle)
    {
      ArticleFullViewModel article = _service.GetArticleByID(articleId ?? 0, Url.Content(_service.ImagePath));
      string curUrl = ArticleService.GetArticleUrl(articleId ?? 0, articleTitle);
      string realUrl = ArticleService.GetArticleUrl(article.ArticleId, article.Title);
      if (curUrl != realUrl)
        return RedirectPermanent(realUrl);
      return View(article);
    }

    [HttpGet]
    public ActionResult ShowLastArticles()
    {
      IEnumerable<ArticleShortViewModel> articles = _service.GetLastArticles(Url.Content(_service.ImagePath));
      return View(articles);
    }

    [HttpGet]
    public ActionResult ShowPopularArticles()
    {
      IEnumerable<ArticleShortViewModel> articles = _service.GetPopularArticles(Url.Content(_service.ImagePath));
      return View(articles);
    }
  }
}