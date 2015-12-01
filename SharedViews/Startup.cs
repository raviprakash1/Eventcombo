using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SharedViews.Startup))]
namespace SharedViews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
