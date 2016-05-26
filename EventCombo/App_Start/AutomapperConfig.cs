using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EventCombo.Models;
using EventCombo.Service;

namespace EventCombo
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
      CreateMap<OrderTemplate, OrderTemplateViewModel>();
      CreateMap<OrderTemplateType, OrderTemplateTypeViewModel>();
      CreateMap<OrderTemplateTicket, OrderTemplateTicketViewModel>();
      CreateMap<Ticket, TicketViewModel>();
      CreateMap<OrderTemplateEventType, OrderTemplateEventTypeViewModel>();
      CreateMap<OrderTemplateTicket, OrderTemplateReceiveByTypeViewModel>()
        .ForMember(d => d.OrderTemplateReceiveByTypeId, m => m.MapFrom(s => s.OrderTemplateTicketId))
        .ForMember(d => d.ReceiveTicket, m => m.MapFrom(s => s.Ticket));
      CreateMap<OrderTemplateQuestion, OrderTemplateQuestionViewModel>()
        .ForMember(d => d.Question, m => m.MapFrom(s => s.QuestionType))
        .ForMember(d => d.Control, m => m.MapFrom(s => s.ControlType));
      CreateMap<QuestionType, QuestionTypeViewModel>()
        .ForMember(d => d.Group, m => m.MapFrom(s => s.QuestionTypeGroup));
      CreateMap<QuestionTypeGroup, QuestionTypeGroupViewModel>();
      CreateMap<OrderTemplate, OrderTemplateWaitlistViewModel>()
        .ForMember(d => d.OrderTemplateWaitlistId, m => m.MapFrom(s => s.OrderTemplateId))
        .ForMember(d => d.TicketId, m => m.MapFrom(s => s.WaitlistTicketId))
        .ForMember(d => d.WaitlistTicket, m => m.MapFrom(s => s.Ticket));
      CreateMap<ControlType, ControlTypeViewModel>();
      CreateMap<Language, LanguageViewModel>();
      CreateMap<OrderTemplateQuestionTicket, OrderTemplateQuestionTicketViewModel>()
        .ForMember(d => d.CollectTicket, m => m.MapFrom(s => s.Ticket));
      CreateMap<OrderTemplateQuestionVariant, OrderTemplateQuestionVariantViewModel>();
      CreateMap<OrderTemplateTicket, OrderTemplateGroupTicketViewModel>()
        .ForMember(d => d.OrderTemplateGroupTicketId, m => m.MapFrom(s => s.OrderTemplateTicketId))
        .ForMember(d => d.GroupTicket, m => m.MapFrom(s => s.Ticket));
      CreateMap<OrderTemplateGroupType, OrderTemplateGroupTypeViewModel>();
      CreateMap<Article, ArticleShortViewModel>()
        .ForMember(d => d.AuthorName, m => m.MapFrom(s => s.ArticleAuthor.Name))
        .ForMember(d => d.AuthorImage, m => m.MapFrom(s => s.ArticleAuthor.ECImage.ImagePath))
        .ForMember(d => d.ArticleImage, m => m.MapFrom(s => s.ECImage.ImagePath))
        .ForMember(d => d.ShortBody, m => m.MapFrom(s => HtmlProcessing.GetShortString(HtmlProcessing.StripTagsRegex(s.Body), 160, 220, ".")));
      CreateMap<Article, ArticleFullViewModel>()
        .ForMember(d => d.AuthorImage, m => m.MapFrom(s => s.ArticleAuthor.ECImage.ImagePath))
        .ForMember(d => d.AuthorName, m => m.MapFrom(s => s.ArticleAuthor.Name))
            .ForMember(d => d.AuthorTwitterUrl, m => m.MapFrom(s => s.ArticleAuthor.TwitterLink))
        .ForMember(d => d.ArticleImageUrl, m => m.MapFrom(s => s.ECImage.ImagePath));
      CreateMap<TicketBearer, AttendeeViewModel>()
        .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Trim()))
        .ForMember(d => d.Email, m => m.MapFrom(s => s.Email.Trim()));
      CreateMap<Event_OrganizerMessages, OrganizerMessageViewModel>();
      CreateMap<EventOrdersSummuryViewModel, EventOrdersSummuryViewModel>();
      CreateMap<PaymentType, PaymentTypeViewModel>();

      //backward maps
      CreateMap<OrderTemplateViewModel, OrderTemplate>();
      CreateMap<OrderTemplateReceiveByTypeViewModel, OrderTemplateTicket>();
      CreateMap<OrderTemplateTicketViewModel, OrderTemplateTicket>();
      CreateMap<OrderTemplateQuestionViewModel, OrderTemplateQuestion>()
        .ForMember(d => d.OrderTemplateQuestionId, m => m.Ignore())
        .ForMember(d => d.QuestionTypeId, m => m.Ignore());
      CreateMap<QuestionTypeViewModel, QuestionType>();
      CreateMap<QuestionTypeGroupViewModel, QuestionTypeGroup>();
      CreateMap<OrderTemplateWaitlistViewModel, OrderTemplate>()
        .ForMember(d => d.WaitlistTicketId, m => m.MapFrom(s => s.TicketId));
      CreateMap<ControlTypeViewModel, ControlType>();
      CreateMap<LanguageViewModel, Language>();
      CreateMap<OrderTemplateQuestionTicketViewModel, OrderTemplateQuestionTicket>();
      CreateMap<OrderTemplateQuestionVariantViewModel, OrderTemplateQuestionVariant>();
      CreateMap<OrderTemplateGroupTicketViewModel, OrderTemplateTicket>();
      CreateMap<OrderTemplateGroupTypeViewModel, OrderTemplateGroupType>();
      CreateMap<AttendeeViewModel, TicketBearer>()
        .ForMember(d => d.TicketbearerId, m => m.Ignore());
      CreateMap<OrganizerMessageViewModel, Event_OrganizerMessages>()
        .ForMember(d => d.MessageId, m => m.Ignore());
    }
  }
}
