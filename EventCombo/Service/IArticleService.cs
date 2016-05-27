using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCombo.Models;

namespace EventCombo.Service
{
  public interface IArticleService
  {
    string ImagePath { get; }

    IEnumerable<ArticleShortViewModel> GetList(string urlPath);

    ArticleFullViewModel GetNewArticle();

    ArticleFullViewModel GetArticleByID(long ArticleId, string urlPath);

    void SaveArticle(ArticleFullViewModel model, string basePath);

    void DeleteArticle(long articleId, string basePath);

    IEnumerable<ArticleShortViewModel> GetLastArticles(string urlPath, int count);

    IEnumerable<ArticleShortViewModel> GetPopularArticles(string urlPath, int count, bool addLatest);
  }
}
