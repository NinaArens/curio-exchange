using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CurioExchange.Startup))]
namespace CurioExchange
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
