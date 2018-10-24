using NFe.WebApi.App_Start;
using NFe.WebApi.IoC;
using NFe.Aplicacao.Mapping;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;

namespace NFe.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SimpleInjectorContainer.Initialize();
            AutoMapperInitializer.Initialize();
        }
    }
}