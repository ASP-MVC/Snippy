using Microsoft.Owin;

using Snippy.App;

[assembly: OwinStartup(typeof(Startup))]

namespace Snippy.App
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}