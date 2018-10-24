using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using NFe.Aplicacao.Mapping;
using NUnit.Framework;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace NFe.Controller.Testes.Initializer
{
    [TestFixture]
    public class TestControllerBase
    {
        [OneTimeSetUp]
        public void InitializeOnceTime()
        {
            AutoMapperInitializer.Reset();
            AutoMapperInitializer.Initialize();
        }

        protected ODataQueryOptions<T> GetOdataQueryOptions<T>(ApiController controller) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            request.GetConfiguration().AddODataQueryFilter();
            request.GetConfiguration().EnableDependencyInjection();
            request.GetConfiguration().Count().Select().Filter().OrderBy().MaxTop(null);
            request.GetConfiguration().AddODataQueryFilter();
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<T>(typeof(T).Name);
            var model = modelBuilder.GetEdmModel();
            ODataQueryContext context = new ODataQueryContext(model, typeof(T), new ODataPath());
            controller.Request = request;
            return new ODataQueryOptions<T>(context, request);
        }
    }
}
