using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Command;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Controller.Tests.Inicializador;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.WebApi.Controllers;
using BancoTabajara.WebApi.Excessoes;
using BancoTabajara.WebApi.Models.Clientes.ViewModel;
using BancoTabajara.WebApi.Models.Mapeador;
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
        //private Mock<CommandRegistrarCliente> _clienteRegistarCmd;
        //private Mock<CommandAtualizarCliente> _clienteAtualizarCmd;
        //private Mock<CommandDeletarCliente> _clienteDeletarCmd;

        private CommandRegistrarCliente _clienteRegistarCmd;
        private CommandAtualizarCliente _clienteAtualizarCmd;
        private CommandDeletarCliente _clienteDeletarCmd;


        [SetUp]
        public void SetUp()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _clienteServico = new Mock<IClienteServico>();
            _clienteController = new ClientesController(_clienteServico.Object)
            {
                Request = request,
                _clienteServico = _clienteServico.Object
            };
            _cliente = new Mock<Cliente>();
            //_clienteRegistarCmd = new Mock<CommandRegistrarCliente>();
            //_clienteAtualizarCmd = new Mock<CommandAtualizarCliente>();
            //_clienteDeletarCmd = new Mock<CommandDeletarCliente>();

            _clienteRegistarCmd = new CommandRegistrarCliente();
            _clienteAtualizarCmd = new CommandAtualizarCliente();
            _clienteDeletarCmd = new CommandDeletarCliente();

            InicializadorAutoMapper.Reset();
            InicializadorAutoMapper.Inicializador();
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
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ViewModelCliente>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _clienteServico.Verify(s => s.PegarPorId(id), Times.Once);
            _cliente.Verify(p => p.Id, Times.Once);
        }

        #endregion

        #region POST
        //TODO: ver como fazer o mock do validation
        [Test]
        public void Cliente_Controller_Post_DeveFuncionar()
        {
            var cliente = ObjectMother.ClienteValido();
            _clienteRegistarCmd.Cpf = cliente.Cpf;
            _clienteRegistarCmd.DataNascimento = cliente.DataNascimento;
            _clienteRegistarCmd.Nome = cliente.Nome;
            _clienteRegistarCmd.Rg = cliente.Rg;

            var id = 1;
            _clienteServico.Setup(c => c.Inserir(_clienteRegistarCmd)).Returns(id);

            IHttpActionResult callback = _clienteController.Post(_clienteRegistarCmd);
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _clienteServico.Verify(s => s.Inserir(_clienteRegistarCmd), Times.Once);
        }

        #endregion

        #region PUT

        [Test]
        public void Cliente_Controller_Put_DeveFuncionar()
        {
            var cliente = ObjectMother.ClienteValidoComId();
            _clienteAtualizarCmd.Cpf = cliente.Cpf;
            _clienteAtualizarCmd.DataNascimento = cliente.DataNascimento;
            _clienteAtualizarCmd.Nome = cliente.Nome;
            _clienteAtualizarCmd.Rg = cliente.Rg;
            _clienteAtualizarCmd.Id = cliente.Id;

            var isUpdated = true;
            _clienteServico.Setup(c => c.Atualizar(_clienteAtualizarCmd)).Returns(isUpdated);
            
            IHttpActionResult callback = _clienteController.Update(_clienteAtualizarCmd);
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _clienteServico.Verify(s => s.Atualizar(_clienteAtualizarCmd), Times.Once);
        }

        [Test]
        public void Cliente_Controller_Put_Id_nao_encontrado()
        {
            var Id = 9999;
            var cliente = ObjectMother.ClienteValidoComId();
            _clienteAtualizarCmd.Cpf = cliente.Cpf;
            _clienteAtualizarCmd.DataNascimento = cliente.DataNascimento;
            _clienteAtualizarCmd.Nome = cliente.Nome;
            _clienteAtualizarCmd.Rg = cliente.Rg;
            _clienteAtualizarCmd.Id = Id;

            _clienteServico.Setup(c => c.Atualizar(_clienteAtualizarCmd)).Throws<NaoEncontradoException>();
            
            IHttpActionResult callback = _clienteController.Update(_clienteAtualizarCmd);
            
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigoErros.NotFound);

			_clienteServico.Verify(s => s.Atualizar(_clienteAtualizarCmd), Times.Once);
        }

        #endregion

        #region DELETE
        //TODO: ver como fazer o mock do validation
        [Test]
        public void Cliente_Controller_Delete_DeveFuncionar()
        {
            _clienteDeletarCmd.Id = 1;
            var isUpdated = true;
            _clienteServico.Setup(c => c.Deletar(_clienteDeletarCmd)).Returns(isUpdated);
            
            IHttpActionResult callback = _clienteController.Delete(_clienteDeletarCmd);
            
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _clienteServico.Verify(s => s.Deletar(_clienteDeletarCmd), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion
    }
}
