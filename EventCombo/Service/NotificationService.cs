using AutoMapper;
using EventCombo.DAL;
using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EventCombo.Service
{
  public class NotificationService: INotificationService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;

    public NotificationService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
    }

    public void SendContactOrganizerMessage(EventNotificationViewModel notification, string userId)
    {
      if (notification == null)
        throw new ArgumentNullException("notification");

      OrganizerMessageViewModel model = _mapper.Map<OrganizerMessageViewModel>(notification);
      IRepository<Event_Orgnizer_Detail> eoRepo = new GenericRepository<Event_Orgnizer_Detail>(_factory.ContextFactory);

      model.Userid = userId;
      var org = eoRepo.Get(filter: (eo => (eo.Orgnizer_Event_Id == model.EventId) && (eo.DefaultOrg == "Y"))).FirstOrDefault();
      model.OrganizerId = org == null ? 0 : org.OrganizerMaster_Id;

      SendContactOrganizerMessage(model);
    }

    public void SendContactOrganizerMessage(OrganizerMessageViewModel model)
    {
      if (model == null)
        throw new ArgumentNullException("model");

      IRepository<Event_OrganizerMessages> messRepo = new GenericRepository<Event_OrganizerMessages>(_factory.ContextFactory);
      Event_OrganizerMessages mess;
      using (var uow = _factory.GetUnitOfWork())
      {
        try
        {
          mess = _mapper.Map<Event_OrganizerMessages>(model);
          messRepo.Insert(mess);
          uow.Context.SaveChanges();
          uow.Commit();
        }
        catch (Exception ex)
        {
          uow.Rollback();
          throw new Exception("Exception in the SaveMessage. Transaction rolled back.", ex);
        }
      }

      if ((mess != null))
      {
        ISendMailService sendService = new SendMailService();
        INotification notification = new OrganizerMessageNotification(_factory, mess, ConfigurationManager.AppSettings.Get("DefaultEmail"));
        notification.SendNotification(sendService);
      }
    }

    public void SendEmailFreindsMessage(FriendNotificationViewModel notification)
    {
      if (notification == null)
        throw new ArgumentNullException("notification");

      ISendMailService sendService = new SendMailService();
      INotification efNotification = new EmailToFriendsNotification(_factory, notification, ConfigurationManager.AppSettings.Get("DefaultEmail"));
      efNotification.SendNotification(sendService);
    }

  }
}