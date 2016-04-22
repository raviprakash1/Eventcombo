using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Utils
{
    public class DateTimeWithZone
    {
        private readonly DateTime utcDateTime;
        private readonly TimeZoneInfo timeZone;
        private readonly bool isUTC;
        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
        {
            utcDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), timeZone);
            this.timeZone = timeZone;
        }

        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone, bool isUTC)
        {
            this.isUTC = isUTC;
            if (!isUTC)
            {
                utcDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), TimeZoneInfo.Local);
            }
            else
            {
                utcDateTime = dateTime; // TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), TimeZoneInfo.Local);
            }
            this.timeZone = timeZone;
        }

 

        public DateTime UniversalTime { get { return utcDateTime; } }

        public TimeZoneInfo TimeZone { get { return timeZone; } }

        public DateTime LocalTime
        {
            get
            {
                if (!this.isUTC)
                {
                    return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
                }
                else
                {
                    return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
                }
            }
        }
    }
}