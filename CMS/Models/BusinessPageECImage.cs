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
    
    public partial class BusinessPageECImage
    {
        public long BusinessPageECImageId { get; set; }
        public long BusinessPageId { get; set; }
        public long ECImageId { get; set; }
    
        public virtual BusinessPage BusinessPage { get; set; }
        public virtual ECImage ECImage { get; set; }
    }
}