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
    
    public partial class EventSubCategory
    {
        public long EventSubCategoryID { get; set; }
        public long EventCategoryID { get; set; }
        public string EventSubCategory1 { get; set; }
    
        public virtual EventCategory EventCategory { get; set; }
    }
}