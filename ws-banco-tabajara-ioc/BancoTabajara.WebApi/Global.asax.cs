using BancoTabajara.WebApi.IoC;
using SimpleInjector.Integration.WebApi;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace BancoTabajara.WebApi
{
    [ExcludeFromCodeCoverage]
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
