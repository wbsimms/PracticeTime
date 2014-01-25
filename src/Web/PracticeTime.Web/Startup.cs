using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracticeTime.Web.Startup))]
namespace PracticeTime.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
