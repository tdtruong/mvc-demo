using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineShop2.Startup))]
namespace OnlineShop2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
