using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CMS.Models;

namespace CMS
{
  public static class AutomapperConfig
  {
    private static MapperConfiguration _config = null;

    public static MapperConfiguration Config
    {
      get
      {
        if (_config == null)
        {
          _config = new MapperConfiguration(cfg =>
          {
            cfg.AddProfile<EventComboProfile>();
          });

        }
        return _config;
      }
    }

  }

  public class EventComboProfile : AutoMapper.Profile
  {
    protected override void Configure()
    {
      // forward maps
      CreateMap<Article, ArticleShortViewModel>()
        .ForMember(d => d.AuthorName, m => m.MapFrom(s => s.ArticleAuthor.Name))
        .ForMember(d => d.AuthorImage, m => m.MapFrom(s => s.ArticleAuthor.ECImage.ImagePath));
      CreateMap<Article, ArticleFullViewModel>()
        .ForMember(d => d.AuthorImage, m => m.MapFrom(s => s.ArticleAuthor.ECImage.ImagePath))
        .ForMember(d => d.AuthorName, m => m.MapFrom(s => s.ArticleAuthor.Name))
        .ForMember(d => d.AuthorTwitterUrl, m => m.MapFrom(s => s.ArticleAuthor.TwitterLink))
        .ForMember(d => d.ArticleImageUrl, m => m.MapFrom(s => s.ECImage.ImagePath));

      //backward maps
      CreateMap<ArticleFullViewModel, Article>()
        .ForMember(d => d.ArticleId, m => m.Ignore());
    }
  }
}