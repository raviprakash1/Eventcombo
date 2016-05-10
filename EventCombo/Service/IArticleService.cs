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
    IEnumerable<ArticleShortViewModel> GetList();
  }
}
