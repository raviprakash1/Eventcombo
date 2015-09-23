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
        public string UserName { get; set; }
        public string EMail { get; set; }
        public List<Permissions> objPermissions { get; set; }
        
    }



}