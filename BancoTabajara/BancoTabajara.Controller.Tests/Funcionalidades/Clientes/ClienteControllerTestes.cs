using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Controller.Tests.Inicializador;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.WebApi.Controllers;
using BancoTabajara.WebApi.Excessoes;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BancoTabajara.Controller.Tests.Funcionalidades.Clientes
{
    [TestFixture]
    public class ClienteControllerTestes : TestControllerBase
    {
        private ClientesController _clienteController;
        private Mock<IClienteServico> _clienteServico;
        private Mock<Cliente> _cliente;

        [SetUp]
        public void SetUp()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _clienteServico = new Mock<IClienteServico>();
            _clienteController = new ClientesController()
            {
                Request = request,
                _clienteServico = _clienteServico.Object
            };
            _cliente = new Mock<Cliente>();
        }

        #region GET

        [Test]
        public void Cliente_Controller_PegarTodos_Deve_Funcionar()
        {
            int? limite = null;
            Cliente cliente = ObjectMother.ClienteValido();
            var response = new List<Cliente>() { cliente }.AsQueryable();

            _clienteServico.Setup(s => s.PegarTodos(limite)).Returns(response);

            var result = _clienteController.Get();
            _clienteServico.Verify(x => x.PegarTodos(limite));
            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<List<Cliente>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(cliente.Id);
        }

        [Test]
        public void Cliente_Controller_PegarPorId_DeveFuncionar()
        {
            var id = 1;
            _cliente.Setup(p => p.Id).Returns(id);
            _clienteServico.Setup(c => c.PegarPorId(id)).Returns(_cliente.Object);
            
            IHttpActionResult callback = _clienteController.GetById(id);
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<Cliente>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _clienteServico.Verify(s => s.PegarPorId(id), Times.Once);
            _cliente.Verify(p => p.Id, Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Cliente_Controller_Post_DeveFuncionar()
        {
            var id = 1;
            _clienteServico.Setup(c => c.Inserir(_cliente.Object)).Returns(id);
            
            IHttpActionResult callback = _clienteController.Post(_cliente.Object);
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _clienteServico.Verify(s => s.Inserir(_cliente.Object), Times.Once);
        }

        #endregion

        #region PUT

        [Test]
        public void Cliente_Controller_Put_DeveFuncionar()
        {
            var isUpdated = true;
            _clienteServico.Setup(c => c.Atualizar(_cliente.Object)).Returns(isUpdated);
            
            IHttpActionResult callback = _clienteController.Update(_cliente.Object);
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _clienteServico.Verify(s => s.Atualizar(_cliente.Object), Times.Once);
        }

        [Test]
        public void Conta_Controller_Put_Id_nao_encontrado()
        {
            _clienteServico.Setup(c => c.Atualizar(_cliente.Object)).Throws<NaoEncontradoException>();
            
            IHttpActionResult callback = _clienteController.Update(_cliente.Object);
            
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigoErros.NotFound);

			_clienteServico.Verify(s => s.Atualizar(_cliente.Object), Times.Once);
        }

        #endregion

        #region DELETE

        [Test]
        public void Cliente_Controller_Delete_DeveFuncionar()
        {
            var isUpdated = true;
            _clienteServico.Setup(c => c.Deletar(_cliente.Object)).Returns(isUpdated);
            
            IHttpActionResult callback = _clienteController.Delete(_cliente.Object);
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _clienteServico.Verify(s => s.Deletar(_cliente.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion
    }
}
