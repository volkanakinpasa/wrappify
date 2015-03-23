using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(wrappify.Web.Startup))]
namespace wrappify.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
