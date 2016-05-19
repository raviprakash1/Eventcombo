using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EventCombo.Models
{
    public class AllViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
    public class LoginResultViewModel
    {
        public LoginResultViewModel(bool success, string returnUrl)
        {
            Success = success;
            ReturnUrl = returnUrl;
        }

        public bool Success { get; set; }
        public string ReturnUrl { get; set; }

    }
    public class ForgetPassword
    {
        public string Email { get; set; }
    }
    public class myAccount
    {

        [Required]
        [StringLength(35, ErrorMessage = "The {0} cannot exceed 35 characters")]
        public string  Firstname{ get; set; }
       
        [StringLength(35, ErrorMessage = "The {0} cannot exceed 35 characters")]
        public string Lastname { get; set; }

       
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Password { get; set; }

        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string NewPassword { get; set; }

     
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
       
        public string Email { get; set; }
      

      
        public string PreviousEmail { get; set; }
        [EmailAddress]
      
        public string ConfirmEmail { get; set; }

        [StringLength(256, ErrorMessage = "The {0} cannot exceed 256 characters")]
        public string StreetAddress1 { get; set; }
        [StringLength(256, ErrorMessage = "The {0} cannot exceed 256 characters")]
        public string StreeAddress2 { get; set; }
        public string Id { get; set; }
        [StringLength(256, ErrorMessage = "The {0} cannot exceed 256 characters")]
        public string City { get; set; }
        [StringLength(256, ErrorMessage = "The {0} cannot exceed 256 characters")]
        public string State { get; set; }
        [StringLength(6, ErrorMessage = "The {0} cannot exceed 5 characters")]
        public string Zip { get; set; }

        public string Country { get; set; }
        public string MainPhone { get; set; }
        public string SecondPhone { get; set; }
        public string WebsiteURL { get; set; }
        public string UserProfileImage{ get; set; }
       
      
        public string WorkPhone { get; set; }
        public string WorkAreaCode { get; set; }
        public string WorkPhoneback { get; set; }
        public string MainAreaCode { get; set; }
        public string MainPhnback { get; set; }
        public string cellareacode { get; set; }
        public string cellareaback { get; set; }
        public string contentype { get; set; }
        public string editsave { get; set; }
        public string ImagePath { get; set; }
        public string Gender { get; set; }
        public string Dateofbirth { get; set; }
        public int? day { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public string ImagePresent { get; set; }
       public string userimage { get; set; }

        public string SendLatestdetails { get; set; }



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
        //[Remote("CheckExistingEmail", "Account", ErrorMessage = "Email already exists!")]
        //[Remote("CheckExistingEmail", "Account", ErrorMessage = "Email already exists!")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
       
        [Display(Name = "Password")]
        public string Password { get; set; }

      
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [StringLength(22, ErrorMessage = "City Maximum character should be 22", MinimumLength = 0)]
        public string city { get; set; }
        [StringLength(22,ErrorMessage = "State Maximum character should be 22", MinimumLength =0)]
        public string state { get; set; }
        [DataType(DataType.PostalCode)]
        public string PinCode { get; set; }
       
        public string Country { get; set; }

    }

    public class ResetPasswordViewModel
    {
        

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
     
        [Display(Name = "Password")]
        public string Password { get; set; }

      
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public enum OrderSortBy { Name, Order, Date, Qty, Total};
    public enum OrderTypes { Upcoming, Past, Favorite};

    public class OrderListRequestViewModel
    {
      public OrderListRequestViewModel()
      {
        SortBy = OrderSortBy.Date;
        OrderType = OrderTypes.Upcoming;
        PerPage = 20;
        Page = 0;
        SortDesc = false;
      }
      public OrderSortBy SortBy { get; set; }
      public OrderTypes OrderType { get; set; }
      public int PerPage { get; set; }
      public int Page { get; set; }
      public bool SortDesc { get; set; }
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
