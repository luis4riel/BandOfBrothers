using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using BancoTabajara.Controller.Tests.Inicializador;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.WebApi.Excessoes;
using BancoTabajara.WebApi.Models;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace BancoTabajara.Controller.Tests.Comum
{
	[TestFixture]
	public class ApiControllerBaseTests : TestControllerBase
	{
		private ApiControllerBaseFake _apiControllerBase;
		private Mock<ApiControllerBaseDummy> _dummy;	

		[SetUp]
		public void Initialize()
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.SetConfiguration( new HttpConfiguration() );
			_apiControllerBase = new ApiControllerBaseFake()
			{
				Request = request
			};
			_dummy = new Mock<ApiControllerBaseDummy>();
		}

		#region HandleCallback

		[Test]
		public void Base_Controller_HandleCallback_ShouldHandleBusinessException()
		{
			var message = "message error test";
			var exception = new ExcecaoDeNegocio( CodigoErros.AlreadyExists, message );
			var callback = _apiControllerBase.HandleCallback<ApiControllerBaseDummy>( () => throw exception );
			var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
			httpResponse.StatusCode.Should().Be( (int) HttpStatusCode.BadRequest );
			httpResponse.Content.ErrorCode.Should().Be( (int) CodigoErros.AlreadyExists );
			httpResponse.Content.ErrorMessage.Should().Be( message );
		}

		[Test]
		public void Base_Controller_HandleCallback_ShouldHandleRuntimeException()
		{
			var message = "message error test";
			var exception = new Exception( message );
			var callback = _apiControllerBase.HandleCallback<ApiControllerBaseDummy>( () => throw exception );
			var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
			httpResponse.StatusCode.Should().Be( (int) HttpStatusCode.InternalServerError );
			httpResponse.Content.ErrorCode.Should().Be( (int) CodigoErros.Unhandled );
			httpResponse.Content.ErrorMessage.Should().Be( message );
		}

		#endregion

		#region HandleQuery

		[Test]
		public void Base_Controller_HandleQuery_ShouldBeOk()
		{
			var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();
			var callback = _apiControllerBase.HandleQuery( query );
			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ApiControllerBaseDummy>>>().Subject;
			httpResponse.Content.Should().NotBeNull();
		}

		[Test]
		public async Task Base_Controller_HandleQuery_ShouldHandleCSVExportAsync()
		{
			var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();
			_apiControllerBase.Request.Headers.Accept.Add( MediaTypeWithQualityHeaderValue.Parse( MediaTypes.Csv ) );
			var callback = _apiControllerBase.HandleQuery( query );
			var httpResponse = callback.Should().BeOfType<ResponseMessageResult>().Subject.Response;
			var data = await httpResponse.Content.ReadAsStringAsync();
			data.Should().NotBeNull();
		}

		[Test]
		public async Task Base_Controller_HandleQuery_ShouldHandleCSVExportAsyncWithoutData()
		{
			var query = new List<ApiControllerBaseDummy>().AsQueryable();
			_apiControllerBase.Request.Headers.Accept.Add( MediaTypeWithQualityHeaderValue.Parse( MediaTypes.Csv ) );
			var callback = _apiControllerBase.HandleQuery( query );
			var httpResponse = callback.Should().BeOfType<ResponseMessageResult>().Subject.Response;
			var data = await httpResponse.Content.ReadAsStringAsync();
			data.Trim().Should().Be( String.Empty );
			httpResponse.Content.Headers.ContentDisposition.DispositionType.Should().Be( "attachment" );
			httpResponse.Content.Headers.ContentType.MediaType.Should().Be( MediaTypes.OctetStream );
		}

		#endregion

		#region HandleQueryable
		[Test]
		public void Base_Controller_HandleQueryable_ShouldBeOk()
		{
			var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();	
			var callback = _apiControllerBase.HandleQueryable<ApiControllerBaseDummy>( query );
			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ApiControllerBaseDummy>>>().Subject;
			httpResponse.Content.Should().NotBeNull();
		}

		[Test]
		public async Task Base_Controller_HandleQueryable_ShouldHandleCSVExportAsync()
		{
			var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();
			_apiControllerBase.Request.Headers.Accept.Add( MediaTypeWithQualityHeaderValue.Parse( MediaTypes.Csv ) );
			var callback = _apiControllerBase.HandleQueryable<ApiControllerBaseDummy>( query );
			var httpResponse = callback.Should().BeOfType<ResponseMessageResult>().Subject.Response;
			var data = await httpResponse.Content.ReadAsStringAsync();
			data.Should().NotBeNull();
		}

		#endregion

		#region HandleValidationFailure

		[Test]
		public void Base_Controller_HandleValidationFailure_ShouldBeHandleValidationErrors()
		{
			var validationFailure = new ValidationFailure( "", ( (int) CodigoErros.Unhandled ).ToString() );
			IList<ValidationFailure> errors = new List<ValidationFailure>() { validationFailure };
			var callback = _apiControllerBase.HandleValidationFailure( errors );			
			var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
			httpResponse.Content.FirstOrDefault().Should().Be( validationFailure );
		}

		#endregion
	}
}
