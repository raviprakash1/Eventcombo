using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class GetEventData
    {
   //Single Date Event
        public long EventVenueID { get; set; }
        public long EventID { get; set; }
        public long AddressId { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndTime { get; set; }
        public string AddressStatus { get; set; }

        // Multiple venue
        public long MultipleEventID { get; set; }
        public string Frequency { get; set; }
        public string StartingFrom { get; set; }
        public string StartingTo { get; set; }
        public string WeeklyDay { get; set; }
        public Nullable<int> MonthlyDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MonthlyWeek { get; set; }
        public string MonthlyWeekDays { get; set; }
        public string MultipleSchTime { get; set; }

        public string Addresses { get; set; }
        public string FirstAddress { get; set; }

        public string EventDescription { get; set; }
    }
}