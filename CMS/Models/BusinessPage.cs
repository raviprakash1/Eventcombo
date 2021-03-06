//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BusinessPage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessPage()
        {
            this.BusinessPageECImages = new HashSet<BusinessPageECImage>();
        }
    
        public long BusinessPageID { get; set; }
        public string PageName { get; set; }
        public string PageNameUrl { get; set; }
        public string PageContent { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int PageOrder { get; set; }
        public bool IsOnFooter { get; set; }
        public Nullable<int> ResponseCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessPageECImage> BusinessPageECImages { get; set; }
    }
}
