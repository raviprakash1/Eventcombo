//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventCombo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organizer_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organizer_Master()
        {
            this.Event_Orgnizer_Detail = new HashSet<Event_Orgnizer_Detail>();
            this.Event_OrganizerMessages = new HashSet<Event_OrganizerMessages>();
        }
    
        public long Orgnizer_Id { get; set; }
        public string Orgnizer_Name { get; set; }
        public string Organizer_Desc { get; set; }
        public string Organizer_FBLink { get; set; }
        public string Organizer_Twitter { get; set; }
        public string Organizer_Linkedin { get; set; }
        public string UserId { get; set; }
        public string Organizer_Image { get; set; }
        public string Organizer_Address1 { get; set; }
        public string Organizer_Address2 { get; set; }
        public string Organizer_City { get; set; }
        public string Organizer_State { get; set; }
        public string Organizer_Zipcode { get; set; }
        public Nullable<byte> Organizer_CountryId { get; set; }
        public string Organizer_Email { get; set; }
        public string Organizer_Phoneno { get; set; }
        public string Organizer_Websiteurl { get; set; }
        public string Organizer_Status { get; set; }
        public string contenttype { get; set; }
        public string Imagepath { get; set; }
        public Nullable<long> ECImageId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_Orgnizer_Detail> Event_Orgnizer_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_OrganizerMessages> Event_OrganizerMessages { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
