using BancoTabajara.WebApi.IoC;
using BancoTabajara.WebApi.Models.Mapeador;
using Microsoft.Owin;
using Owin;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;

[assembly: OwinStartup(typeof(BancoTabajara.WebApi.Startup))]
namespace BancoTabajara.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            HttpConfiguration config = new HttpConfiguration();
            app.UseWebApi(config);
            SimpleInjectorContainer.Inicializador();
            InicializadorAutoMapper.Inicializador();
        }
    }
}