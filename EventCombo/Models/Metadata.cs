using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Models
{
    public class Metadata
    {

       

    }
    public class EventMetadata
    {
        [AllowHtml]
        public string EventDescription { get; set; }
    }

    public class OrganiserMetadata
    {
        [AllowHtml]
        public string Organizer_Desc { get; set; }
    }
}