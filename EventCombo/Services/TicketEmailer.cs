using EventCombo.Models;
using EventCombo.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EventCombo.Services
{
    public class TicketEmailer
    {
        public void send()
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                DbRawSqlQuery<TicketEmailTemplate> template = null;
                string query = "SELECT I_SenderName as SenderName, I_SubjectLine as Subject, I_EmailContent as EmailBody, I_ScheduleDate as ScheduledDate, " +
                                "L_EmailId as EmailId FROM Event_Email_Invitation i inner join Event_Email_List l on i.I_Id = l.L_I_Id " +
                                "where I_ScheduleDate between getutcdate() and DATEADD(MINUTE, 5, getutcdate()) and I_mode = 'S'";

                template = db.Database.SqlQuery<TicketEmailTemplate>(query);
                foreach (var item in template)
                {
                    Email.send("Sch: " + item.Subject, item.EmailBody, "", "", item.EmailId, "", item.SenderName);
                    //Debug.WriteLine("Sending " + item.EmailId);
                }
            }
            //Debug.WriteLine("Kannan Testing Scheduler");
        }
    }

    public class TicketEmailTemplate
    {
        public string Subject { get; set; }
        public string SenderName { get; set; }
        public string EmailBody { get; set; }
        public string EmailId { get; set; }
        public DateTime ScheduledDate { get; set; }

    }

}