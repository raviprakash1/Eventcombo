using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Models;
using CMS.DAL;
using AutoMapper;
using System.IO;

namespace CMS.Service
{
  public class ArticleService : IArticleService
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

    public IEnumerable<Models.ArticleShortViewModel> GetList(string urlPath)
    {
      IRepository<Article> articleRepo = new GenericRepository<Article>(_factory.ContextFactory);
      var articles = articleRepo.Get().OrderByDescending(a => a.EditDate);
      List<ArticleShortViewModel> asVM = new List<ArticleShortViewModel>();
      foreach (var article in articles)
      {
        ArticleShortViewModel articleVM = _mapper.Map<ArticleShortViewModel>(article);
        if (!String.IsNullOrWhiteSpace(articleVM.AuthorImage))
          articleVM.AuthorImage = urlPath + articleVM.AuthorImage;
        asVM.Add(articleVM);
      }
      return asVM;
    }


    public ArticleFullViewModel GetNewArticle()
    {
      ArticleFullViewModel res = new ArticleFullViewModel()
      {
        ArticleId = 0,
        ArticleAuthorId = 0,
        ArticleImageUrl = "",
        AuthorImage = "",
        AuthorName = "",
        AuthorTwitterUrl = "",
        Body = "",
        EnableFBComments = true,
        HomepageFlag = true,
        PremiumFlag = true,
        SubHeading = "",
        Title = ""
      };
      return res;
    }

    public ArticleFullViewModel GetArticleByID(long ArticleId, string urlPath)
    {
      IRepository<Article> articleRepo = new GenericRepository<Article>(_factory.ContextFactory);
      var article = articleRepo.Get(filter: (a => a.ArticleId == ArticleId)).SingleOrDefault();
      if (article == null)
        return GetNewArticle();
      ArticleFullViewModel artVM = _mapper.Map<ArticleFullViewModel>(article);
      if (!String.IsNullOrWhiteSpace(artVM.ArticleImageUrl))
        artVM.ArticleImageUrl = urlPath + artVM.ArticleImageUrl;
      if (!String.IsNullOrWhiteSpace(artVM.AuthorImage))
        artVM.AuthorImage = urlPath + artVM.AuthorImage;
      foreach (var file in article.ArticleImages)
        if (!String.IsNullOrWhiteSpace(file.ECImage.ImagePath))
          artVM.Images.Add(urlPath + file.ECImage.ImagePath);

      return artVM;
    }


    public void SaveArticle(ArticleFullViewModel model, string basePath)
    {
      using (IUnitOfWork uow = _factory.GetUnitOfWork())
      {
        try
        {
          IRepository<ECImage> iRepo = new GenericRepository<ECImage>(_factory.ContextFactory);
          IRepository<Article> aRepo = new GenericRepository<Article>(_factory.ContextFactory);
          Article article = null;
          if (model.ArticleId != 0)
            article = aRepo.GetByID(model.ArticleId);
          if (article == null)
          {
            article = _mapper.Map<Article>(model);
            article.ArticleAuthor = new ArticleAuthor() { Name = model.AuthorName, TwitterLink = model.AuthorTwitterUrl };
            article.CreateDate = DateTime.Now;
            aRepo.Insert(article);
          }
          else
          {
            _mapper.Map(model, article);
            article.ArticleAuthor.Name = model.AuthorName;
            article.ArticleAuthor.TwitterLink = model.AuthorTwitterUrl;
          }

          if (model.ArticleImageFile != null)
          {
            if (article.ECImageId != null)
            {
              DeleteImage(article.ECImageId, basePath);
              article.ECImageId = null;
            }
            article.ECImage = CreateECImage(model.ArticleImageFile, basePath);
          }

          if (model.AuthorImageFile != null)
          {
            if (article.ArticleAuthor.ECImageId != null)
            {
              DeleteImage(article.ArticleAuthor.ECImageId, basePath);
              article.ArticleAuthor.ECImageId = null;
            }
            article.ArticleAuthor.ECImage = CreateECImage(model.AuthorImageFile, basePath);
          }

          IRepository<ArticleImage> aiRepo = new GenericRepository<ArticleImage>(_factory.ContextFactory);
          if (model.ImageFiles.Count() > 0)
            foreach (var aimage in article.ArticleImages.ToList())
            {
              DeleteImage(aimage.ECImageId, basePath);
              aiRepo.Delete(aimage);
            }
          foreach (var file in model.ImageFiles)
            if (file != null)
              article.ArticleImages.Add(new ArticleImage() { ECImage = CreateECImage(file, basePath), Sort = 0 });

          article.EditDate = DateTime.Now;
          uow.Context.SaveChanges();
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception in the SaveArticle. Transaction rolled back.", ex);
        }
      }
    }

    private ECImage CreateECImage(HttpPostedFileBase file, string basePath)
    {
      IRepository<ECImageType> itRepo = new GenericRepository<ECImageType>(_factory.ContextFactory);
      var res = new ECImage() { ImagePath = SaveImage(file, basePath), ECImageTypeId = 0 };
      var imageType = itRepo.Get(filter: (it => it.TypeName == file.ContentType.ToLower())).SingleOrDefault();
      if (imageType == null)
        res.ECImageType = new ECImageType() { TypeName = file.ContentType.ToLower() };
      else
        res.ECImageTypeId = imageType.ECImageTypeId;
      return res;
    }

    private void DeleteImage(long? imageId, string basePath)
    {
      if (imageId == null)
        return;
      IRepository<ECImage> iRepo = new GenericRepository<ECImage>(_factory.ContextFactory);
      var image = iRepo.Get(filter: (i => i.ECImageId == imageId)).SingleOrDefault();
      if (image != null)
      {
        File.Delete(basePath + image.ImagePath);
        iRepo.Delete(image);
      }
    }

    private string SaveImage(HttpPostedFileBase file, string basePath)
    {
      var newName = "";
      while (newName == "")
      {
        newName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
        if (File.Exists(basePath + newName))
          newName = "";
      }
      Directory.CreateDirectory(basePath);
      file.SaveAs(basePath + newName);
      return newName;
    }
  }
}