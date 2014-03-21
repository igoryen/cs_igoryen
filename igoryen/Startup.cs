using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(igoryen.Startup))]
namespace igoryen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
