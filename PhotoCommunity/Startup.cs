using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoCommunity.Startup))]
namespace PhotoCommunity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }  
    }
}
