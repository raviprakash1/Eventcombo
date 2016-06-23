using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public class ArticleShortViewModel
  {
    public long ArticleId { get; set; }
    public string Title { get; set; }
    public long ArticleAuthorId { get; set; }
    public string AuthorName { get; set; }
    public string AuthorImage { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime EditDate { get; set; }
    public string ShortBody { get; set; }
    public string ArticleImage { get; set; }
  }

  public class ArticleFullViewModel
  {
    public long ArticleId { get; set; }
    public string Title { get; set; }
    public string ArticleImageUrl { get; set; }
    public long ArticleAuthorId { get; set; }
    public string AuthorName { get; set; }
    public string AuthorImage { get; set; }
    public string AuthorTwitterUrl { get; set; }
    [System.Web.Mvc.AllowHtml]
    public string Body { get; set; }
    public string SubHeading { get; set; }
    public bool EnableFBComments { get; set; }
    public bool HomepageFlag { get; set; }
    public bool PremiumFlag { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime EditDate { get; set; }

    private List<string> _images = new List<string>();
    public List<string> Images
    {
      get { return _images; }
      private set { _images = value; }
    }

    public HttpPostedFileBase ArticleImageFile { get; set; }
    public HttpPostedFileBase AuthorImageFile { get; set; }
    public IEnumerable<HttpPostedFileBase> ImageFiles { get; set; }
  }

  public class TwoListsOfSomething<T>
  {
    private List<T> _firstList;
    public List<T> FirstList
    {
      get { return _firstList; }
      set { _firstList = value; }
    }

    private List<T> _secondList;
    public List<T> SecondList
    {
      get { return _secondList; }
      set { _secondList = value; }
    }

  }
}