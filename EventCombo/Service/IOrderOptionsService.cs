using System;
using System.Collections.Generic;
using EventCombo.Models;

namespace EventCombo.Service
{
  public interface IOrderOptionsService
  {
    OrderTemplateViewModel GetOrderTemplateViewModel(long eventId);
    bool SaveOrderTemplateForm(OrderTemplateViewModel otVM);
    bool SaveOrderTemplateConfirmation(OrderTemplateViewModel otVM);
    bool SaveOrderTemplateEventType(OrderTemplateViewModel otVM);
    bool SaveOrderTemplateWaitlist(OrderTemplateViewModel otVM);
    IEnumerable<TicketViewModel> GetTicketsViewModel(long eventId);
    IEnumerable<OrderTemplateTypeViewModel> GetOrderTemplateTypesViewModel();
    IEnumerable<OrderTemplateEventTypeViewModel> GetOrderTemplateEventTypesViewModel();
    IEnumerable<LanguageViewModel> GetLanguagesViewModel();
    IEnumerable<ControlTypeViewModel> GetControlTypesViewModel();
    IEnumerable<QuestionTypeGroupViewModel> GetQuestionTypeGroups();
    IEnumerable<OrderTemplateGroupTypeViewModel> GetOrderTemplateGroupTypes();
    string GetEventTitle(long eventId);
  }
}
