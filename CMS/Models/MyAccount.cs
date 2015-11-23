using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class MyAccount
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string HomeNumber { get; set; }
        public string WorkNumber { get; set; }
        public string Website { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
        public string Picturename { get; set; }
        public string Pictureurl { get; set; }
        public string PreviousEmail { get; set; }
        public string ConfirmEmail { get; set; }



        public string MainPhone { get; set; }
        public string SecondPhone { get; set; }
        public string WebsiteURL { get; set; }
        public string UserProfileImage { get; set; }


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

          public string ConfirmPassword { get; set; }
        public string Designation { get; set; }
    
        public string Password { get; set; }
    }
}