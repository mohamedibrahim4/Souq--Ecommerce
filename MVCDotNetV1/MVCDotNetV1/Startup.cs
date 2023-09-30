using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCDotNetV1.Startup))]
namespace MVCDotNetV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }

    }
}
