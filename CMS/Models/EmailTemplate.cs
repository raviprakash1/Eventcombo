using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Models
{
    public class EmailTemplate
    {
       public string Template_Name{get;set;}
        public string To { get; set; }
        public string From { get; set; }
        public string  CC { get; set; }
        public string Bcc { get; set; }
        public string Subject{ get; set; }
        [AllowHtml]
        public string TemplateHtml { get; set; }
        public string Template_Id { get; set; }
        [AllowHtml]
        public string ckeditor1 { get; set; }
        public string emailtag { get; set; }
        
    }
   
}