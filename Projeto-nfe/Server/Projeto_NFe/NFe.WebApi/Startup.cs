using Microsoft.Owin;
using NFe.WebApi.App_Start;
using Owin;
using System.Web.Http;
using System.Diagnostics.CodeAnalysis;

[assembly: OwinStartup(typeof(NFe.WebApi.Startup))]
namespace NFe.WebApi
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