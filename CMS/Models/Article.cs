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
    
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            this.ArticleImages = new HashSet<ArticleImage>();
        }
    
        public long ArticleId { get; set; }
        public long ArticleAuthorId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Nullable<long> ECImageId { get; set; }
        public string SubHeading { get; set; }
        public bool EnableFBComments { get; set; }
        public bool HomepageFlag { get; set; }
        public bool PremiumFlag { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime EditDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleImage> ArticleImages { get; set; }
        public virtual ECImage ECImage { get; set; }
        public virtual ArticleAuthor ArticleAuthor { get; set; }
    }
}
