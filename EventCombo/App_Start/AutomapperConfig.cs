using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EventCombo.Models;

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
      CreateMap<OrderTemplateReceiveByType, OrderTemplateReceiveByTypeViewModel>();
      CreateMap<OrderTemplateQuestion, OrderTemplateQuestionViewModel>();
      CreateMap<QuestionType, QuestionTypeViewModel>();
      CreateMap<QuestionTypeGroup, QuestionTypeGroupViewModel>();

      //backward maps
      CreateMap<OrderTemplateViewModel, OrderTemplate>();
      CreateMap<OrderTemplateReceiveByTypeViewModel, OrderTemplateReceiveByType>();
      CreateMap<OrderTemplateTicketViewModel, OrderTemplateTicket>();
      CreateMap<OrderTemplateQuestionViewModel, OrderTemplateQuestion>();
      CreateMap<QuestionTypeViewModel, QuestionType>();
      CreateMap<QuestionTypeGroupViewModel, QuestionTypeGroup>();
    }
  }
}