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
  public class OrderNotification: INotification
  {
    private IUnitOfWorkFactory _factory;
    private IDBAccessService _dbservice;
    private string _receiver;
    private string _body;
    private string _subject;
    private string _orderId;
    private string _baseUrl;
    private Attachment _attachment;

    public OrderNotification(IUnitOfWorkFactory factory, IDBAccessService dbService, string orderId, string baseUrl, Attachment attachment)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      if (dbService == null)
        throw new ArgumentNullException("dbService");

      _factory = factory;
      _dbservice = dbService;
      _orderId = orderId;
      _baseUrl = baseUrl;
      _attachment = attachment;
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
      _service.Message.Body = _body.Replace("¶¶UserFirstNameID¶¶", ReceiverName);
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
      _body = new MvcHtmlString(HttpUtility.HtmlDecode(eTemplate.TemplateHtml)).ToHtmlString();
      PrepareBody();
    }

    private void PrepareBody()
    {
      StringBuilder strHTML = new StringBuilder();
      strHTML.Append("<table style='width: 100 %; '><tr><h1 style='font-size:22px;margin-bottom:5px;margin-top:0px;font-weight:normal;'> Order Summary </h1>");
      strHTML.Append("<p style='margin:0px;padding:0px;margin-bottom:5px;font-size:15px;'>Order#:" + _orderId + "</p></tr>");

      IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
      var cEvent = eRepo.Get(filter: (e => e.Ticket_Purchased_Detail.Where(tpd => tpd.TPD_Order_Id == _orderId).Any())).FirstOrDefault();

      var startend = _dbservice.GetEventStartEnd(cEvent.EventID);

      string eventname;
      var eAddresses = _dbservice.GetEventAddresses(cEvent.EventID);
      if (eAddresses == null)
        eventname = "Online";
      else
        eventname = eAddresses.Select(a => a.ConsolidateAddress).FirstOrDefault();

      IRepository<Ticket_Purchased_Detail> tpdRepo = new GenericRepository<Ticket_Purchased_Detail>(_factory.ContextFactory);
      var tickets = tpdRepo.Get(filter: (t => t.TPD_Order_Id == _orderId));
      foreach (var ticket in tickets.Select(tp => new
      {
        AddressId = tp.Ticket_Quantity_Detail.TQD_AddressId,
        ConsolidateAddress = tp.Ticket_Quantity_Detail.TQD_AddressId == 0 ? "" : tp.Ticket_Quantity_Detail.Address.ConsolidateAddress,
        StartDateStr = tp.Ticket_Quantity_Detail.TQD_StartDate,
        StartDate = DateTime.Parse(tp.Ticket_Quantity_Detail.TQD_StartDate)
      })
        .Distinct()
        .OrderBy(s => s.StartDate))
      {
        strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + ticket.ConsolidateAddress + "<span style='float:right;'>" + ticket.StartDate.ToLongDateString() + " </span></p ></tr > ");
        strHTML.Append("<tr align='left' style='color:#696564;'> ");
        strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;' > Type </th >");
        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </ th >");
        strHTML.Append("</tr>");
        foreach (var item in tickets.Where(t => ((t.Ticket_Quantity_Detail.TQD_AddressId == ticket.AddressId) && (t.Ticket_Quantity_Detail.TQD_StartDate == ticket.StartDateStr)))
          .OrderBy(o => o.Ticket_Quantity_Detail.TQD_Ticket_Id))
        {
          strHTML.Append("<tr align='left' style='color:#696564;'> ");
          strHTML.Append("<td style='font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + item.AspNetUser.Profiles.FirstOrDefault().FirstName + "</td>");
          strHTML.Append("<td style='font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + item.Ticket_Quantity_Detail.Ticket.T_name + "</td>");
          strHTML.Append("<td style='font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + item.TPD_Purchased_Qty + "</td>");
          strHTML.Append("<td style='font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (item.TPD_Amount > 0 ? "$ " + item.TPD_Amount.ToString() : item.TPD_Donate > 0 ? "$ " + item.TPD_Donate.ToString() : "Free") + "</td>");
          strHTML.Append("</tr>");
        }
      }

      IRepository<Order_Detail_T> orderRepo = new GenericRepository<Order_Detail_T>(_factory.ContextFactory);
      var order = orderRepo.Get(filter: (o => o.O_Order_Id == _orderId)).SingleOrDefault();
      var variabledescid = order.O_VariableId;
      if (!string.IsNullOrWhiteSpace(variabledescid))
      {
        IRepository<Event_VariableDesc> varRepo = new GenericRepository<Event_VariableDesc>(_factory.ContextFactory);
        foreach (string word in variabledescid.Split(','))
        {
          var vardesc = varRepo.Get(filter: (p => ((p.Event_Id == cEvent.EventID) && (p.Variable_Id.ToString() == word)))).FirstOrDefault();
          if (vardesc != null)
          {
            strHTML.Append("<tr align='right'> ");
            strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
            strHTML.Append("</tr> ");
          }
        }
      }

      strHTML.Append("<tr align='right'> ");
      strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>Total :" + order.O_TotalAmount + " </td></tr>");

      var cardtext = "";
      if (order.O_TotalAmount > 0)
      {
        IRepository<BillingAddress> baRepo = new GenericRepository<BillingAddress>(_factory.ContextFactory);
        IRepository<PaymentType> paymentTypeRepo = new GenericRepository<PaymentType>(_factory.ContextFactory);
        var myBillingdeatils = baRepo.Get(filter: (ba => ba.OrderId == _orderId)).FirstOrDefault();
        var paymentType = paymentTypeRepo.Get(filter: (p => p.PaymentTypeId == order.PaymentTypeId)).FirstOrDefault();
        if (myBillingdeatils != null)
        {
          EncryptDecrypt encryptor = new EncryptDecrypt();
          string cardtype = encryptor.DecryptText(myBillingdeatils.card_type);
          string cardId = encryptor.DecryptText(myBillingdeatils.CardId);
          cardtext = " - "+cardtype.First().ToString().ToUpper() + cardtype.Substring(1) + " XXXX-XXXX-XXXX-" + cardId.Substring(cardId.Length - 4);
        }
        if (paymentType != null)
        {
            cardtext = "Charge to : " + paymentType.PaymentTypeName + "" + cardtext;
        }
      }

      strHTML.Append("<tr align='center'> ");
      strHTML.Append("<td colspan='4' style='font-size:15px; padding:10px 5px;'>" + cardtext + " </td></tr>");
      strHTML.Append("<tr align='center'><td colspan='4' style='font-size:15px;'>");
      strHTML.Append("<p style='background:#fff9cf; padding:10px 15px; display:inline-block; margin:0px;'>This charge will appear on your card statement as Eventcombo * { " + cEvent.EventTitle + "}</p>");
      strHTML.Append("<p style='color:#696564;' >This order is subject to Eventcombo '");
      strHTML.Append("<a href='#' style='color:#0f90ba;'>Terms of Service </a> , <a style='color:#0f90ba;' href='#'>Privacy Policy </a> and <a style='color:#0f90ba;' href='#'>Cookie Policy </a></p>");
      strHTML.Append("</td></tr></table > ");

      string Imagecode = "<img style = 'width:200px;height:200px;' src ='http://maps.google.com/maps/api/staticmap?center=" + eventname + "&zoom=14&size=400x400&maptype=roadmap&markers=color:red|color:red|label:C|" + eventname + "&sensor=false' alt = 'Map Image' />";
      StringBuilder sbBody = new StringBuilder(_body);
      if (!string.IsNullOrEmpty(_body))
      {
        List<KeyValuePair<string, string>> tagList = new List<KeyValuePair<string, string>>();

        tagList.Add(new KeyValuePair<string, string>("EventTitleId", cEvent.EventTitle));
        tagList.Add(new KeyValuePair<string, string>("EventOrganiserName", cEvent.Event_Orgnizer_Detail.FirstOrDefault().Organizer_Master.Orgnizer_Name));
        tagList.Add(new KeyValuePair<string, string>("EventOrganiserEmail", cEvent.Event_Orgnizer_Detail.FirstOrDefault().Organizer_Master.Organizer_Email));
        tagList.Add(new KeyValuePair<string, string>("EventDynamicTable", strHTML.ToString()));
        tagList.Add(new KeyValuePair<string, string>("CreateEventurl", _baseUrl + "/createevent/createevent"));
        tagList.Add(new KeyValuePair<string, string>("DiscoverEventurl", _baseUrl));
        tagList.Add(new KeyValuePair<string, string>("EventLogin", _baseUrl));
        tagList.Add(new KeyValuePair<string, string>("Eventtype", cEvent.AddressStatus == "Multiple" ? "* This event has multiple venues " : ""));
        tagList.Add(new KeyValuePair<string, string>("EventStartDateID", startend.Start.ToString()));
        tagList.Add(new KeyValuePair<string, string>("EventEndDateID", startend.End.ToString()));
        tagList.Add(new KeyValuePair<string, string>("EventVenueID", eventname));
        tagList.Add(new KeyValuePair<string, string>("EventMapImage", Imagecode));
        tagList.Add(new KeyValuePair<string, string>("Downloadurl", _baseUrl + "/download/ticket?OrderId=" + _orderId + "&format=pdf"));

        foreach (var tag in tagList)
          sbBody.Replace("¶¶" + tag.Key + "¶¶", tag.Value);
      }
      _body = sbBody.ToString();
    }

  }
}
