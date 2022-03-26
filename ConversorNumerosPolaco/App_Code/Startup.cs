using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConversorNumerosPolaco.Startup))]
namespace ConversorNumerosPolaco
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
