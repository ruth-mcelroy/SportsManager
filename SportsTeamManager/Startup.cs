using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportsTeamManager.Startup))]
namespace SportsTeamManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
