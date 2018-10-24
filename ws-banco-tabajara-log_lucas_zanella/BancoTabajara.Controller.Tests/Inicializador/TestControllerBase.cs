﻿using BancoTabajara.WebApi.Models.Mapeador;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using NUnit.Framework;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace BancoTabajara.Controller.Tests.Inicializador
{
	[TestFixture]
	public class TestControllerBase
	{
		[OneTimeSetUp]
		public void InitializeOnceTime()
		{
			InicializadorAutoMapper.Reset();
			InicializadorAutoMapper.Inicializador();
		}

		protected ODataQueryOptions<T> GetOdataQueryOptions<T>(ApiController controller) where T : class
		{
			// Criamos um request para simular uma chamada HTTP para os testes
			HttpRequestMessage request = new HttpRequestMessage();
			request.SetConfiguration( new HttpConfiguration() );
			request.GetConfiguration().AddODataQueryFilter();
			request.GetConfiguration().EnableDependencyInjection();
			request.GetConfiguration().Count().Select().Filter().OrderBy().MaxTop( null );
			request.GetConfiguration().AddODataQueryFilter();
			// Criamos um model do odata para a nossa chamada http
			var modelBuilder = new ODataConventionModelBuilder();
			modelBuilder.EntitySet<T>( typeof( T ).Name );
			var model = modelBuilder.GetEdmModel();
			// Criamos um context do odata conforme nossa chamada http mockada
			ODataQueryContext context = new ODataQueryContext( model, typeof( T ), new ODataPath() );
			controller.Request = request;
			// Retornamos opções do odata mockadas
			return new ODataQueryOptions<T>( context, request );
		}
	}
}
