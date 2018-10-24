using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Infra.ORM.Funcionalidade.Clientes;
using BancoTabajara.Infra.ORM.Tests.Contexto;
using BancoTabajara.Infra.ORM.Tests.Inicializador;
using Effort;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BancoTabajara.Infra.ORM.Tests.Funcionalidades.Clientes
{
    [TestFixture]
    public class ClienteRepositorioTestes : EffortTestBase
    {
        private FakeDbContext _ctx;
        private ClienteRepositorio _repositorio;
        private Cliente _cliente;
        private Cliente _clienteSeed;
        private Cliente _clienteSemConta;
        private Conta _conta;

        [SetUp]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _ctx = new FakeDbContext(connection);
            _repositorio = new ClienteRepositorio(_ctx);
            _cliente = ObjectMother.ClienteValido();
            _clienteSeed = ObjectMother.ClienteValido();
            _clienteSemConta = ObjectMother.ClienteValido();
            _conta = ObjectMother.ObtemContaValida();
            _conta.Titular =  _clienteSeed;
            _ctx.Clientes.Add(_clienteSeed);
            _ctx.Clientes.Add(_clienteSemConta);
            _ctx.Contas.Add(_conta);

            _ctx.SaveChanges();
        }

        #region ADD
        [Test]
        public void Clientes_repositorio_inserir_deve_funcionar()
        {
            var clienteRegistrado = _repositorio.Inserir(_cliente);
            clienteRegistrado.Should().NotBeNull();
            clienteRegistrado.Id.Should().NotBe(0);
            var expectedCustomer = _ctx.Clientes.Find(clienteRegistrado.Id);
            expectedCustomer.Should().NotBeNull();
            expectedCustomer.Should().Be(clienteRegistrado);
        }

        #endregion

        #region GET

        [Test]
        public void Clientes_repositorio_pegar_todos_comLimite_deve_funcionar()
        {
            var limite = 1;
            var clientes = _repositorio.PegarTodos(limite).ToList();      
            clientes.Should().NotBeNull();
            clientes.Should().HaveCount(limite);
            clientes.First().Should().Be(_clienteSeed);
        }

        [Test]
        public void Clientes_Repositorio_PegarTodos_DeveFuncionar()
        {
            var clientes = _repositorio.PegarTodos().ToList();
            clientes.Should().NotBeNull();
            clientes.Should().HaveCount(_ctx.Clientes.Count());
            clientes.First().Should().Be(_clienteSeed);
        }

        [Test]
        public void Clientes_Repositorio_PegarPorId_DeveFuncionar()
        {
            var clienteResult = _repositorio.PegarPorId(_clienteSeed.Id);
            clienteResult.Should().NotBeNull();
            clienteResult.Should().Be(_clienteSeed);
        }

        [Test]
        public void Clientes_Repositorio_PegarPorId_Deve_Retornar_Excecao()
        {
            var idNaoEncontrado = 10;
            var clienteResult = _repositorio.PegarPorId(idNaoEncontrado);
            clienteResult.Should().BeNull();
        }

        #endregion

        #region DELETE
        [Test]
        public void Clientes_repositorio_deletar_deve_funcionar()
        {
            var ClienteRemovido = _repositorio.Deletar(_clienteSemConta.Id);
            ClienteRemovido.Should().BeTrue();
            _ctx.Clientes.Where(p => p.Id == _clienteSemConta.Id).FirstOrDefault().Should().BeNull();
        }

        [Test]
        public void Clientes_repositorio_deletar_deve_retornar_excecao_clienteId_desconhecido()
        {
            var idNaoEncontrado = 10;
            Action removeAction = () => _repositorio.Deletar(idNaoEncontrado);
            removeAction.Should().Throw<NaoEncontradoException>();
        }
        [Test]
        public void Clientes_repositorio_deletar_deve_retornar_excecao_conta_vinculada()
        {
            Action remover = () => _repositorio.Deletar(_clienteSeed.Id);
            remover.Should().Throw<ClienteVinculadoException>();
        }
        #endregion

        #region UPDATE

        [Test]
        public void Clientes_repositorio_atualizar_deve_funcionar()
        {
            var foiAtualizado = false;
            var novoRG = "5505050500";
            _clienteSeed.Rg = novoRG;
            var actionAtualizar = new Action(() => { foiAtualizado = _repositorio.Atualizar(_clienteSeed); });
            actionAtualizar.Should().NotThrow<Exception>();
            foiAtualizado.Should().BeTrue();
        }

        [Test]
        public void Clientes_repositorio_atualizar_deve_retornar_excecao_clienteId_desconhecido()
        {
            _cliente = ObjectMother.ClienteValido();
            var idDesconhecido = 20;
            _cliente.Id = idDesconhecido;
            Action updatedAction = () => _repositorio.Atualizar(_cliente);
            updatedAction.Should().Throw<DbUpdateConcurrencyException>();
        }
        #endregion
    }
}
