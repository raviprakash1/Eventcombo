using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using EventCombo.DAL;
using AutoMapper;

namespace EventCombo.Service
{
  public class ArticleService: IArticleService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;

    public ArticleService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
    }

    public IEnumerable<Models.ArticleShortViewModel> GetList()
    {
      IRepository<Article> articleRepo = new GenericRepository<Article>(_factory.ContextFactory);
      var articles = articleRepo.Get().OrderByDescending(a => a.EditDate);
      List<ArticleShortViewModel> asVM = new List<ArticleShortViewModel>();
      foreach (var article in articles)
      {
        ArticleShortViewModel articleVM = _mapper.Map<ArticleShortViewModel>(article);
        _mapper.Map(article.ArticleAuthor, articleVM);
        asVM.Add(articleVM);
      }
      return asVM;
    }
  }
}