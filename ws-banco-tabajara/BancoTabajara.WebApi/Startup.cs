using Microsoft.Owin;
using BancoTabajara.WebApi;
using Owin;
using System.Web.Http;
using System.Diagnostics.CodeAnalysis;

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
        }
    }
}