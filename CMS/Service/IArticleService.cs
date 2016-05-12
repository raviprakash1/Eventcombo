using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;

namespace CMS.Service
{
  public interface IArticleService
  {
    IEnumerable<ArticleShortViewModel> GetList(string urlPath);

    ArticleFullViewModel GetNewArticle();

    ArticleFullViewModel GetArticleByID(long ArticleId, string urlPath);

    void SaveArticle(ArticleFullViewModel model, string basePath);

    void DeleteArticle(long articleId, string basePath);
  }
}
