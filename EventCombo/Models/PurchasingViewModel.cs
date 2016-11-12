using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EventCombo.Models
{
  public enum PaymentMethod { Card = 0, PayPal = 1}

  public class TicketLockViewModel
  {
    public long LockTicketId { get; set; }
    public System.Guid LockOrderId { get; set; }
    public long TicketQuantityDetailId { get; set; }
    public int Quantity { get; set; }
    public decimal Donate { get; set; }
    public decimal Amount { get; set; }
    public decimal ECFeeAmount { get; set; }
    public decimal ECFeePercent { get; set; }
    public int ECFeeType { get; set; }
    public decimal CustomerFee { get; set; }
  }

  public class TicketLockRequest
  {
    public long EventId { get; set; }
    public string UserId { get; set; }
    public string IP { get; set; }
    public IEnumerable<TicketLockViewModel> Tickets { get; set; }
  }

  public class TicketLockResult
  {
    public string LockId { get; set; }
    public int LockCount { get; set; }
    public bool TicketsAvailable { get; set; }
    public string Message { get; set; }
  }

  public class TicketPurchaseInfoViewModel
  {
    public TicketPurchaseInfoViewModel()
    {
      Attendees = new List<AttendeeViewModel>();
    }

    public long LockTicketId { get; set; }
    public long TicketTypeId { get; set; }
    public string Name { get; set; }
    public string VenueName { get; set; }
    public string DateString { get; set; }
    public decimal Price { get; set; }
    public decimal SourcePrice { get; set; }
    public decimal TotalFee { get; set; }
    public long Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public byte Sort { get; set; }

    public IEnumerable<AttendeeViewModel> Attendees { get; set; }
  }

  public class VariableChargePurchaseInfoViewModel
  {
    public long VariableId { get; set; }
    public string VariableDesc { get; set; }
    public decimal Price { get; set; }
    public bool Checked { get; set; }
  }

  public class VariableChargeGroup
  {
    public VariableChargeGroup()
    {
      VariableCharges = new List<VariableChargePurchaseInfoViewModel>();
    }
    public string Title { get; set; }
    public int GroupType { get; set; }

    public IEnumerable<VariableChargePurchaseInfoViewModel> VariableCharges { get; set; }
  }

  public class PaymentAddressViewModel
  {
    public string Country { get; set; }
    public int CountryId { get; set; }
    public string State { get; set; }
    public int StateId { get; set; }
    public string City { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
  }

  public class PurchasingInfoViewModel
  {
    public PurchasingInfoViewModel()
    {
      VariableChargeGroups = new List<VariableChargeGroup>();
      BillingAddress = new PaymentAddressViewModel();
      ShippingAddress = new PaymentAddressViewModel();
    }

    public string LockId { get; set; }
    public PaymentMethod Method { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CardNumber { get; set; }
    public string CardName { get; set; }
    public string CardType { get; set; }
    public string ExpirationDate { get; set; }
    public string SecurityCode { get; set; }
    public bool ShippingSame { get; set; }
    public string PromoCode { get; set; }
    public decimal TotalPromo { get; set; }
    public decimal TotalOrder { get; set; }


    public IEnumerable<VariableChargeGroup> VariableChargeGroups { get; set; }
    public PaymentAddressViewModel BillingAddress { get; set; }
    public PaymentAddressViewModel ShippingAddress { get; set; }
  }

  public class CardTypeInfo
  {
    public string TypeName { get; set; }
    public string ImageUrl { get; set; }
  }
  
  public class StateViewModel
  {
    public int StateId { get; set; }
    public string StateName { get; set; }
    public string StateShortName { get; set; }
    public string StateShowName { get { return StateName + ", " + CountryName; } }
    public byte CountryId { get; set; }
    public string CountryName { get; set; }
  }

  public class EventPurchaseInfoViewModel: IBaseViewModel
  {
    public string BaseTitle { get; set; }
    public string BaseUserId { get; set; }
    public string BaseUserName { get; set; }
    public string BaseUserEmail { get; set; }

    public EventPurchaseInfoViewModel()
    {
      Tickets = new List<TicketPurchaseInfoViewModel>();
      PurchaseInfo = new PurchasingInfoViewModel();
      StateList = new List<StateViewModel>();
      var cards = new List<CardTypeInfo>();
      cards.Add(new CardTypeInfo() { TypeName = "mastercard", ImageUrl = "/Images/AMaterial/master-card-icon.jpg" });
      cards.Add(new CardTypeInfo() { TypeName = "visa", ImageUrl = "/Images/AMaterial/visa-icon.jpg" });
      cards.Add(new CardTypeInfo() { TypeName = "discover", ImageUrl = "/Images/AMaterial/discover-icon.jpg" });
      cards.Add(new CardTypeInfo() { TypeName = "amex", ImageUrl = "/Images/AMaterial/amex-icon.jpg" });
      AcceptedCards = cards;
    }
    public long EventId { get; set; }
    public string EventUrl { get; set; }
    public long SecondsRemains { get; set; }
    public string BGImageUrl { get; set; }
    public string BGColor { get; set; }
    public string EventTitle { get; set; }
    public string VenueName { get; set; }
    public string Address { get; set; }
    public string StartDate { get; set; }
    public decimal TotalAmount { get; set; } 

    public IEnumerable<TicketPurchaseInfoViewModel> Tickets { get; set; }
    public PurchasingInfoViewModel PurchaseInfo { get; set; }
    public IEnumerable<CardTypeInfo> AcceptedCards { get; set; }
    public IEnumerable<StateViewModel> StateList { get; set; }
  }

  public class PurchaseResult
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public string OrderId { get; set; }
    public string Url { get; set; }
    [JsonIgnore]
    public string PayPalId { get; set; }
  }

  public class OrderConfirmationViewModel : IBaseViewModel
  {
    public string BaseTitle { get; set; }
    public string BaseUserId { get; set; }
    public string BaseUserName { get; set; }
    public string BaseUserEmail { get; set; }
    public OrderConfirmationViewModel()
    {
      Tickets = new List<TicketPurchaseInfoViewModel>();
    }
    public string OrderId { get; set; }
    public long EventId { get; set; }
    public string EventUrl { get; set; }
    public string BGImageUrl { get; set; }
    public string BGColor { get; set; }
    public string EventTitle { get; set; }
    public string VenueName { get; set; }
    public string Address { get; set; }
    public string StartDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Email { get; set; }
    public long OrganizerId { get; set; }

    public IEnumerable<TicketPurchaseInfoViewModel> Tickets { get; set; }
  }

}