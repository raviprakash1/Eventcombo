using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models {
    public class BusinessPageMetaData {
        [Display(Name ="Page Name")]
        public string PageName { get; set; }
        [Display(Name = "Page Name Url")]
        public string PageNameUrl { get; set; }
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
    public partial class BusinessPage {

    }
}