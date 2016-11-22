using EventCombo.DAL;
using EventCombo.Models;
using EventCombo.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Service
{
  public class OrderNotification : INotification
  {
    private IUnitOfWorkFactory _factory;
    private IDBAccessService _dbservice;
    private string _receiver;
    private string _body;
    private string _subject;
    private string _orderId;
    private string _baseUrl;
    private Attachment _attachment;
    private ITicketsService _tService;
    //private bool _isManualOrder;

    public OrderNotification(IUnitOfWorkFactory factory, IDBAccessService dbService, string orderId, string baseUrl, Attachment attachment, ITicketsService tService)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (dbService == null)
        throw new ArgumentNullException("dbService");
      if (tService == null)
        throw new ArgumentNullException("tService");

      _factory = factory;
      _dbservice = dbService;
      _orderId = orderId;
      _baseUrl = baseUrl;
      _attachment = attachment;
      _tService = tService;
    }

    public string ReceiverName
    {
      get { return _receiver; }
      set { _receiver = value; }
    }

    public void SendNotification(ISendMailService _service)
    {
      if (String.IsNullOrEmpty(_body))
        PrepareNotification();
      _service.Message.Subject = _subject;
      _service.Message.IsBodyHtml = true;
      _service.Message.Body = _body.Replace("¶¶UserFirstNameID¶¶", ReceiverName.Trim());
      var to = _service.Message.To.FirstOrDefault();
      if (to != null)
        _service.Message.Body = _service.Message.Body.Replace("¶¶UserEmailID¶¶", to.Address);
      _service.Message.Attachments.Clear();
      if (_attachment != null)
        _service.Message.Attachments.Add(_attachment);
      _service.Message.From = new MailAddress("no-reply@eventcombo.com");
      _service.SendMail();
    }

    public void PrepareNotification()
    {
      IRepository<Email_Template> etempRepo = new GenericRepository<Email_Template>(_factory.ContextFactory);
      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);

      var eTemplate = etempRepo.Get(filter: (e => e.Template_Tag == "eticket")).FirstOrDefault();
      var ticket = tpdRepo.Get(filter: (t => t.TPD_Order_Id == _orderId)).FirstOrDefault();
      if ((eTemplate == null) || (ticket == null))
        return;

      _subject = eTemplate.Subject.Replace("¶¶EventTitleId¶¶", ticket.Event.EventTitle).Replace("¶¶EventOrderNO¶¶", _orderId);
      _body = _tService.GetTicketsHtml(_orderId, "~/Views/Download/_TicketNotification.cshtml");
    }
  }
}
