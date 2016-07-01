using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Models
{
    public class BusinessPageMetaData
    {
        [Required]
        [Display(Name = "Page Name")]
        public string PageName { get; set; }
        [Required]
        [Display(Name = "Page Name Url")]
        [RegularExpression("^[a-z0-9-_]+$", ErrorMessage = "Use [a-z,0-9] or '-' or '_'")]
        public string PageNameUrl { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [System.Web.Mvc.AllowHtml]
        [Display(Name = "Page Content")]
        public string PageContent { get; set; }
        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public System.DateTime UpdateDate { get; set; }
    }
    [MetadataType(typeof(BusinessPageMetaData))]
    public partial class BusinessPage
    {

    }
}