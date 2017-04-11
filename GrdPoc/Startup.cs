using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GrdPoc.Startup))]
namespace GrdPoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
