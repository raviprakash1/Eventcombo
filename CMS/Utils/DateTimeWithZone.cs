using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Models;
namespace CMS.Utils
{
    public class DateTimeWithZone
    {
        private readonly DateTime utcDateTime;
        private readonly TimeZoneInfo timeZone;
        private readonly bool isUTC;
        EmsEntities db = new EmsEntities();
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

        public static string Timezonedetail(long eventid)
         {
            string timezone = "Eastern Standard Time";
            using (EmsEntities obj = new EmsEntities())
            {
                var eventdetail = (from o in obj.Events where o.EventID == eventid select o).FirstOrDefault();

                var Timezonedetail = (from o in obj.TimeZoneDetails where o.TimeZone_Id.ToString() == eventdetail.TimeZone select o).FirstOrDefault();
                if (Timezonedetail != null)
                {
                    timezone = Timezonedetail.TimeZone;
                }
                else
                {
                    timezone = "Eastern Standard Time";
                }
            }
          
            return timezone;
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