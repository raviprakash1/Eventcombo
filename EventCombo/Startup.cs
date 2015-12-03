using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("EventComboStartup", typeof(EventCombo.Startup))]
//[assembly: OwinStartupAttribute(typeof(EventCombo.Startup))]
namespace EventCombo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
