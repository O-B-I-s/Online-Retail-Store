using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_Retail_Store.Startup))]
namespace Online_Retail_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
