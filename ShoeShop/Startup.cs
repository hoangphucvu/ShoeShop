using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoeShop.Startup))]
namespace ShoeShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
