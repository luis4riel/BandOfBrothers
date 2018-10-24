using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;	
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Controller.Tests.Inicializador;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.WebApi.Controllers;
using BancoTabajara.WebApi.Excessoes; 
using FluentAssertions;	 
using Moq;		  
using NUnit.Framework;

namespace BancoTabajara.Controller.Tests.Funcionalidades.Contas
{
	[TestFixture]
	public class ContasControllerTests:TestControllerBase
	{
		private ContasController _contasController;
		private Mock<IContaServico> _contaServicoMock;
		private Mock<Conta> _conta;

		[SetUp]
		public void Initialize()
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.SetConfiguration( new HttpConfiguration() );
			_contaServicoMock = new Mock<IContaServico>();
			_contasController = new ContasController()
			{
				Request = request,
				_contaServico = _contaServicoMock.Object,
			};
			_conta = new Mock<Conta>();
		}

		#region GET

		[Test]
		public void Conta_Controller_PegarTodos_Deve_Funcionar()
		{
            int? limite = null;
			var conta = ObjectMother.ObtemContaValida();
			var response = new List<Conta>() { conta }.AsQueryable();
			_contaServicoMock.Setup( s => s.PegarTodos(limite) ).Returns( response );

			var callback = _contasController.Get();

			_contaServicoMock.Verify( s => s.PegarTodos(limite), Times.Once );
			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<Conta>>>().Subject;
			httpResponse.Content.Should().NotBeNullOrEmpty();
			httpResponse.Content.First().Id.Should().Be( conta.Id );
		}

		[Test]
		public void Cliente_Controller_PegarPorId_DeveFuncionar()
		{
			var id = 1;
			_conta.Setup( p => p.Id ).Returns( id );
			_contaServicoMock.Setup( c => c.PegarPorId( id ) ).Returns( _conta.Object );

			IHttpActionResult callback = _contasController.GetById( id );

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<Conta>>().Subject;
			httpResponse.Content.Should().NotBeNull();
			httpResponse.Content.Id.Should().Be( id );
			_contaServicoMock.Verify( s => s.PegarPorId( id ), Times.Once );
			_conta.Verify( p => p.Id, Times.Once );
		}

		#endregion

		#region POST

		[Test]
		public void Cliente_Controller_Post_DeveFuncionar()
		{
			var id = 1;

			_conta.Setup( s => s.Titular ).Returns( ObjectMother.ClienteValido() );

			_contaServicoMock.Setup( c => c.Inserir( _conta.Object ) ).Returns( id );

			IHttpActionResult callback = _contasController.Post( _conta.Object );

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
			httpResponse.Content.Should().Be( id );
			_contaServicoMock.Verify( s => s.Inserir( _conta.Object ), Times.Once );
		}

		#endregion

		#region PUT

		[Test]
		public void Conta_Controller_Put_DeveFuncionar()
		{
			var isUpdated = true;
			_contaServicoMock.Setup( c => c.Atualizar( _conta.Object ) ).Returns( isUpdated );

			IHttpActionResult callback = _contasController.Update( _conta.Object );

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
			httpResponse.Content.Should().BeTrue();
			_contaServicoMock.Verify( s => s.Atualizar( _conta.Object ), Times.Once );
		}

		[Test]
		public void Conta_Controller_Put_Id_nao_encontrado()
		{

			_contaServicoMock.Setup( c => c.Atualizar( _conta.Object ) ).Throws<NaoEncontradoException>();

			IHttpActionResult callback = _contasController.Update( _conta.Object );

			var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
			httpResponse.Content.ErrorCode.Should().Be( (int) CodigoErros.NotFound );

			_contaServicoMock.Verify( s => s.Atualizar( _conta.Object ), Times.Once );
		}

		#endregion

		#region DELETE

		[Test]
		public void Conta_Controller_Delete_DeveFuncionar()
		{
			var isUpdated = true;
			_contaServicoMock.Setup( c => c.Deletar( _conta.Object ) ).Returns( isUpdated );

			IHttpActionResult callback = _contasController.Delete( _conta.Object );

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
			_contaServicoMock.Verify( s => s.Deletar( _conta.Object ), Times.Once );
			httpResponse.Content.Should().BeTrue();
		}

		#endregion
	}
}
