using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using CMS.Models;
namespace CMS.Models
{
    [Table("AspNetUsers")]
    public class UsersTemplate
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public List<Permissions> objPermissions { get; set; }
        public string Organiser { get; set; }
        public string Merchant { get; set; }
        public string UserStatus { get; set; }

        public string OrganiserId { get; set; }
        public string MerchantId { get; set; }
        public string UserStatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Online { get; set; }
        public string Role { get; set; }
        public string RoleType { get; set; }
        public int EventCount { get; set; }
        public string State { get; set; }
        public string Usercount { get; set; }
        public long TicketPurchased { get; set; }
        public string Ipcountry { get; set; }

    }



    public class Eventtype
    {
        public long EventTypeID { get; set; }
        public string EventType1 { get; set; }
        public bool EventContains { get; set; }
        public string EventHide { get; set; }

    }

}