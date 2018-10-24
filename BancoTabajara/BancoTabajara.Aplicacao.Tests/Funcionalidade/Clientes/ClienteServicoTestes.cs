using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Tests.Funcionalidade.Clientes
{
    [TestFixture]
    public class ClienteServicoTestes
    {
        private IClienteServico _servico;
        private Mock<IClienteRepositorio> _clienteRepositorioFake;

        [SetUp]
        public void Initialize()
        {
            _clienteRepositorioFake = new Mock<IClienteRepositorio>();
            _servico = new ClienteServico(_clienteRepositorioFake.Object);
        }

        [Test]
        public void Cliente_servico_inserir_deve_funcionar()
        {
            var cliente = ObjectMother.ClienteValido();
            _clienteRepositorioFake.Setup(
                pr => pr.Inserir(It.IsAny<Cliente>()))
                .Returns(cliente);

            var novoClienteId = _servico.Inserir(cliente);

            _clienteRepositorioFake.Verify(
                pr => pr.Inserir(It.IsAny<Cliente>()), Times.Once
                );
            novoClienteId.Should().Be(cliente.Id);
        }

        [Test]
        public void Cliente_servico_pegar_todos_deve_trazer_todos()
        {
            var limite = 1;
            var cliente = ObjectMother.ClienteValido();
            var repositoryMockValue = new List<Cliente>() { cliente }.AsQueryable();
            _clienteRepositorioFake.Setup(pr => pr.PegarTodos(limite)).Returns(repositoryMockValue);

            var listaClientes = _servico.PegarTodos(limite);

            _clienteRepositorioFake.Verify(pr => pr.PegarTodos(limite), Times.Once);
            listaClientes.Should().NotBeNull();
            listaClientes.Count().Should().Be(repositoryMockValue.Count());

            listaClientes.First().Should().Be(repositoryMockValue.First());
        }

        [Test]
        public void Cliente_servico_pegar_por_id_deve_trazer_cliente()
        {
            var cliente = ObjectMother.ClienteValido();
            _clienteRepositorioFake.Setup( pr => pr.PegarPorId(cliente.Id) ).Returns(cliente);

            var clientePego = _servico.PegarPorId(cliente.Id);

            _clienteRepositorioFake.Verify(pr => pr.PegarPorId(cliente.Id), Times.Once);
            clientePego.Should().NotBeNull();
            clientePego.Id.Should().Be(cliente.Id);
        }

        [Test]
        public void Cliente_servico_pegar_por_id_deve_falhar_por_id_nao_encontrado()
        {
            var cliente = ObjectMother.ClienteValido();
            var exception = new NaoEncontradoException();
            _clienteRepositorioFake.Setup(pr => pr.PegarPorId(cliente.Id)).Throws(exception);

            Action action = () => _servico.PegarPorId(cliente.Id);

            action.Should().Throw<NaoEncontradoException>();

            _clienteRepositorioFake.Verify(pr => pr.PegarPorId(cliente.Id), Times.Once);
        }

        [Test]
        public void Cliente_servico_deletar_deve_funcionar()
        {
            var cliente = ObjectMother.ClienteValido();
            var retorno = true;
            _clienteRepositorioFake.Setup(pr => pr.Deletar(cliente.Id)).Returns(retorno);

            var isProductRemoved = _servico.Deletar(cliente);

            _clienteRepositorioFake.Verify(pr => pr.Deletar(cliente.Id), Times.Once);
            isProductRemoved.Should().BeTrue();
        }

        [Test]
        public void Cliente_servico_deletar_id_nao_encontrado_deve_falhar()
        {
            var cliente = ObjectMother.ClienteValido();
            _clienteRepositorioFake.Setup(pr => pr.Deletar(cliente.Id)).Throws<NaoEncontradoException>();

            Action action = () => _servico.Deletar(cliente);

            action.Should().Throw<NaoEncontradoException>();

            _clienteRepositorioFake.Verify(pr => pr.Deletar(cliente.Id), Times.Once);
        }

        [Test]
        public void Cliente_servico_atualizar_deve_funcionar()
        {
            var cliente = ObjectMother.ClienteValido();
            cliente.Nome = "zé";
            var isUpdated = true;
            _clienteRepositorioFake.Setup(pr => pr.PegarPorId(cliente.Id)).Returns(cliente);
            _clienteRepositorioFake.Setup(pr => pr.Atualizar(cliente)).Returns(isUpdated);

            var isProductRemoved = _servico.Atualizar(cliente);

            _clienteRepositorioFake.Verify(pr => pr.PegarPorId(cliente.Id), Times.Once);
            _clienteRepositorioFake.Verify(pr => pr.Atualizar(cliente), Times.Once);
            isProductRemoved.Should().BeTrue();
        }

        [Test]
        public void Cliente_servico_atualizar_deve_falhar_por_id_nao_encontrado()
        {
            var cliente = ObjectMother.ClienteValido();
            _clienteRepositorioFake.Setup(pr => pr.PegarPorId(cliente.Id)).Returns((Cliente)null);

            Action clienteAction = () => _servico.Atualizar(cliente);

            clienteAction.Should().Throw<NaoEncontradoException>();

            _clienteRepositorioFake.Verify(pr => pr.PegarPorId(cliente.Id), Times.Once);
            _clienteRepositorioFake.Verify(pr => pr.Atualizar(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
