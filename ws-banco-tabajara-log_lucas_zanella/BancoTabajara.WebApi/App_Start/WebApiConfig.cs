using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData;
using Microsoft.OData.UriParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BancoTabajara.WebApi.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
            config.EnableOdata();
        }

        private static void MapApiRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
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

        private static void EnableOdata(this HttpConfiguration config)
        {
            config.Count().Select().Filter().OrderBy().MaxTop(null);
            config.AddODataQueryFilter();
            config.EnableDependencyInjection(builder =>
            {
                builder.AddService<ODataUriResolver>(ServiceLifetime.Singleton, sp => new StringAsEnumResolver() { EnableCaseInsensitive = true });
            });
        }
    }
}
