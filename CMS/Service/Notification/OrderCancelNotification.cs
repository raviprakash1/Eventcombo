using CMS.DAL;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Service
{
  public class OrderCancelNotification : INotification
  {
    private IUnitOfWorkFactory _factory;
    private string _orderId;
    private string _userId;

    public OrderCancelNotification(IUnitOfWorkFactory factory, string orderId, string userId)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      _factory = factory;
      _orderId = orderId;
      _userId = userId;
    }

    public void SendNotification(ISendMailService _service)
    {
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);

      var ticket = tpdRepo.Get(filter: (t => t.TPD_Order_Id == _orderId && t.TPD_User_Id == _userId)).FirstOrDefault();

      if (ticket == null)
        return;

      _service.Message.Subject = String.Format("Order {0} has been cancelled", _orderId);
      _service.Message.Body = String.Format("Order {0} for event {1} has been cancelled by user {2}", new string[] { _orderId, ticket.Event.EventTitle, ticket.AspNetUser.UserName });
      _service.SendMail();
    }

    private string _receiver;
    public string ReceiverName
    {
      get { return _receiver; }
      set { _receiver = value; }
    }
  }
}