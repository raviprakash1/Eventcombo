using EventCombo.Services;
using Hangfire;
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



            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
