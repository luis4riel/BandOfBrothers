using BancoTabajara.WebApi.Filters;
using BancoTabajara.WebApi.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BancoTabajara.WebApi
{
    [ExcludeFromCodeCoverage]
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapApiRoutes();
            config.ConfigureJsonSerialization();
            config.ConfigureXMLSerialization();
            config.Filters.Add(new ExceptionHandlerAttribute());	   
        }

        private static void MapApiRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new TokenValidationHandler());
            config.Routes.MapHttpRoute(
                name: "BancoTabajara.WebAPI",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    action = RouteParameter.Optional
                }
            );
        }

        private static void ConfigureJsonSerialization(this HttpConfiguration config)
        {
            var jsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSerializerSettings.Formatting = Formatting.None;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        private static void ConfigureXMLSerialization(this HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
