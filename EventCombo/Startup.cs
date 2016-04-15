using EventCombo.Services;
using Hangfire;
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
            GlobalConfiguration.Configuration.UseSqlServerStorage("MyConnection");

            RecurringJob.AddOrUpdate(() => ticketEmailer.send(), Cron.Minutely);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
