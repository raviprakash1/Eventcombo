using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class BusinessPageViewModel: IBaseViewModel
    {
        // IBaseViewModel implementation
        public string BaseTitle { get; set; }
        public string BaseUserId { get; set; }
        public string BaseUserName { get; set; }
        public string BaseUserEmail { get; set; }

        // Business Page implementation
        public long BusinessPageID { get; set; }
        public string PageNameUrl { get; set; }
        public string PageName { get; set; }
        public string PageContent { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int PageOrder { get; set; }
        public bool IsOnFooter { get; set; }
        public Nullable<int> ResponseCode { get; set; }
    }
}