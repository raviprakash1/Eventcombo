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

        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
        {
            utcDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), timeZone);
            this.timeZone = timeZone;
        }

        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone, bool isUTC)
        {
            if (!isUTC)
            {
                utcDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), timeZone);
            }
            else
            {
                utcDateTime = dateTime;
            }
            this.timeZone = timeZone;
        }

        public DateTime UniversalTime { get { return utcDateTime; } }

        public TimeZoneInfo TimeZone { get { return timeZone; } }

        public DateTime LocalTime
        {
            get
            {
                return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
            }
        }
    }
}