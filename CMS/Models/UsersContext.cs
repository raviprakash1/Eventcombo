using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace CMS.Models
{
    public class UsersContext: DbContext
    {
        public DbSet<UsersTemplate> UserTemplates { get; set; }
    }
}