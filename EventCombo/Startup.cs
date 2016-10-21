using EventCombo.Service;
using EventCombo.Services;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using System.Diagnostics;

[assembly: OwinStartup("EventComboStartup", typeof(EventCombo.Startup))]
//[assembly: OwinStartupAttribute(typeof(EventCombo.Startup))]
namespace EventCombo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var options = new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(300) // Default value
            };

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("MyConnection");

            EventStatusUpdate eventStatus = new EventStatusUpdate();
            RecurringJob.AddOrUpdate(() => eventStatus.Update(), "*/4 * * * *");

            TicketEmailer ticketEmailer = new TicketEmailer();
            RecurringJob.AddOrUpdate(() => ticketEmailer.send(), "*/5 * * * *");

            DeleteLockTickets locktickets = new DeleteLockTickets();
            RecurringJob.AddOrUpdate(() => locktickets.Delete(), "*/1 * * * *");


            LastLoginStatus loginStatus = new LastLoginStatus();
            RecurringJob.AddOrUpdate(() => loginStatus.changeloginstatus(), "*/5 * * * *");

            RecurringJob.AddOrUpdate<AttendeeMailNotification>(amn => amn.Send(), "*/5 * * * *");

            RecurringJob.AddOrUpdate<PurchasingService>(ps => ps.DeleteExpiredLocks(10), "*/1 * * * *");

            var hfOptions = new DashboardOptions
            {
              AuthorizationFilters = new[]
              {
                new LocalRequestsOnlyAuthorizationFilter()
              }
            };

            app.UseHangfireDashboard("/hangfire", hfOptions);
            NLogConfig.Configure();            
        }
    }
}
