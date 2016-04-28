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

            TicketEmailer ticketEmailer = new TicketEmailer();

            var options = new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(300) // Default value
            };

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("MyConnection");

            RecurringJob.AddOrUpdate(() => ticketEmailer.send(), "*/5 * * * *");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
