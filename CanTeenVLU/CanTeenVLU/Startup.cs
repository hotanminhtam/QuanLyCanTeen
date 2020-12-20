using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CanTeenVLU.StartupOwin))]

namespace CanTeenVLU
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
