using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public partial class EventCategory
    {
        public List<EventSubCategory> ESubCat { get; set; }
        public bool CategoryPresent { get; set; }

    }
    public partial class EventSubCategory
    {
        public bool EvntSubCatPresent { get; set; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public enum OrderSortBy { Name, Order, Date, Qty, Total, FullName, Email, Address, Status, OrderDate, EventId, DateDiff };
    public enum OrderTypes { Upcoming, Past };
   
    public class OrderSearch
    {
        public DateTime? OrderDate { get; set; }
        public string Order { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Event { get; set; }
    }
   
    public class OrderListRequestViewModel
    {
      public OrderListRequestViewModel()
      {
        SortBy = OrderSortBy.OrderDate;
        OrderType = OrderTypes.Upcoming;
        PerPage = 25;
        Page = 0;
        SortDesc = true;
        Search = "";
      }
      public OrderSortBy SortBy { get; set; }
      public OrderTypes OrderType { get; set; }
      public int PerPage { get; set; }
      public int Page { get; set; }
      public bool SortDesc { get; set; }
      public string Search { get; set; }
    }

    public class OrderMainViewModel
    {
      public long OId { get; set; }
      public string Name { get; set; }
      public DateTime? EventStartDate { get; set; }
      public DateTime? EventEndDate { get; set; }
      public long? Quantity { get; set; }
      public decimal? TotalPaid { get; set; }
      public string OrderId { get; set; }
      public bool EventCancelled { get; set; }
      public bool Favorite { get; set; }
      public byte? OrderStateId { get; set; }
      public long? EventId { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public string City { get; set; }
      public string State { get; set; }
      public DateTime? OrderDateTime { get; set; }
      public string OrderStateName { get; set; }
      public string UserId { get; set; }
    }

    public class OrderListMainViewModel
    {
      public OrderTypes OrderType { get; set; }

      private List<OrderMainViewModel> _orders = new List<OrderMainViewModel>();
      public List<OrderMainViewModel> Orders
      {
        get { return _orders; }
        private set { _orders = value; }
      }

    }

    public class OrderCommonInfoViewModel
    {
      public OrderTypes OrderType { get; set; }
      public long TotalCount { get; set; }
    }

    public class OrderViewModel
    {
      private List<OrderCommonInfoViewModel> _OrderTypeInfo = new List<OrderCommonInfoViewModel>();
      public List<OrderCommonInfoViewModel> OrderTypeInfo
      {
        get { return _OrderTypeInfo; }
        private set { _OrderTypeInfo = value; }
      }

      private OrderListRequestViewModel _state = new OrderListRequestViewModel();
      public OrderListRequestViewModel State
      {
        get { return _state; }
        private set { _state = value; }
      }

    }

    public class AttendeeViewModel
    {
      public long TicketbearerId { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
    }

    public class OrderDetailsViewModel
    {
      public long OId { get; set; }
      public string OrderId { get; set; }
      public string Payment { get; set; }
      public string Email { get; set; }
      public string EventDate { get; set; }
      public string EventLocation { get; set; }
      public bool SendEmail { get; set; }

      private List<AttendeeViewModel> _attendees = new List<AttendeeViewModel>();
      public List<AttendeeViewModel> Attendees
      {
        get { return _attendees; }
        private set { _attendees = value; }
      }
    }

    public class StartEndDateTime
    {
      public DateTime Start { get; set; }
      public DateTime End { get; set; }
    }

    public class OrganizerMessageViewModel
    {
      public long MessageId { get; set; }
      public string OrderId { get; set; }
      public long? EventId { get; set; }
      public long? OrganizerId { get; set; }
      public string Userid { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
      public string Message { get; set; }
    }

}
