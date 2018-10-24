using Microsoft.Owin;
using BancoTabajara.WebApi;
using Owin;
using System.Web.Http;
using System.Diagnostics.CodeAnalysis;
using BancoTabajara.WebApi.IoC;

[assembly: OwinStartup(typeof(BancoTabajara.WebApi.Startup))]
namespace BancoTabajara.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            app.UseWebApi(config);
            SimpleInjectorContainer.Inicializador();
        }
    }
}