using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using BancoTabajara.Infra.ORM.Funcionalidade.Clientes;
using BancoTabajara.Infra.ORM.Funcionalidade.Contas;
using BancoTabajara.Infra.ORM.Tests.Contexto;
using BancoTabajara.Infra.ORM.Tests.Inicializador;
using Effort;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BancoTabajara.Infra.ORM.Tests.Funcionalidades.Contas
{
    [TestFixture]
    public class ContaRepositorioTestes : EffortTestBase
    {
        FakeDbContext _contexto;
        ContaRepositorio _contaRepositorio;
        ClienteRepositorio _clienteRepositorio;
        Conta _conta;
        Conta _contaSeed;

        [SetUp]
        public void SetUp()
        {
            var conexao = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _contexto = new FakeDbContext(conexao);
            _contaRepositorio = new ContaRepositorio(_contexto);
            _clienteRepositorio = new ClienteRepositorio(_contexto);
            _conta = ObjectMother.ObtemContaValida();
            _contaSeed = ObjectMother.ObtemContaValida();
            _contexto.Contas.Add(_contaSeed);
            _contexto.SaveChanges();
        }

        #region ADD
        [Test]
        public void Conta_Repositorio_Inserir_DeveFuncionar()
        {
            _conta.Titular = _clienteRepositorio.PegarPorId(1);
            var contaRegistrada = _contaRepositorio.Inserir(_conta);
            contaRegistrada.Should().NotBeNull();
            contaRegistrada.Id.Should().NotBe(0);
            var contaEsperada = _contexto.Contas.Find(contaRegistrada.Id);
            contaEsperada.Should().NotBeNull();
            contaEsperada.Should().Be(contaRegistrada);
        }
        [Test]
        public void Conta_Repositorio_Inserir_com_Deposito_DeveFuncionar()
        {
            _conta.Titular = _clienteRepositorio.PegarPorId(1);
            _conta.Deposita(50, TipoMovimentacao.Credito );
            var contaRegistrada = _contaRepositorio.Inserir(_conta);
            contaRegistrada.Should().NotBeNull();
            contaRegistrada.Id.Should().NotBe(0);
            var contaEsperada = _contexto.Contas.Find(contaRegistrada.Id);
            contaEsperada.Should().NotBeNull();
            contaEsperada.Should().Be(contaRegistrada);
        }
        [Test]
        public void Conta_Repositorio_Inserir_com_Saque_DeveFuncionar()
        {
            _conta.Titular = _clienteRepositorio.PegarPorId(1);
            _conta.Saca(50, TipoMovimentacao.Debito );
			var contaRegistrada = _contaRepositorio.Inserir(_conta);
            contaRegistrada.Should().NotBeNull();
            contaRegistrada.Id.Should().NotBe(0);
            var contaEsperada = _contexto.Contas.Find(contaRegistrada.Id);
            contaEsperada.Should().NotBeNull();
            contaEsperada.Should().Be(contaRegistrada);
        }

        #endregion

        #region GET

        [Test]
        public void Conta_repositorio_pegar_todos_com_limite_deve_funcionar()
        {
            var limite = 1;
            var contas = _contaRepositorio.PegarTodos(limite).ToList();
            contas.Should().NotBeNull();
            contas.Should().HaveCount(limite);
            contas.First().Should().Be(_contaSeed);
        }

        [Test]
        public void Conta_repositorio_pegar_todos_deve_funcionar()
        {
            var contas = _contaRepositorio.PegarTodos().ToList();
            contas.Should().NotBeNull();
            contas.Should().HaveCount(_contexto.Contas.Count());
            contas.First().Should().Be(_contaSeed);
        }

        [Test]
        public void Conta_repositorio_pegar_por_id_deve_funcionar()
        {
            var conta = _contaRepositorio.PegarPorId(_contaSeed.Id);
            conta.Should().NotBeNull();
            conta.Should().Be(_contaSeed);
        }

        [Test]
        public void Conta_repositorio_pegar_por_id_deve_retornar_nulo()
        { 
            var idNaoEncontrado = 10;
            var conta = _contaRepositorio.PegarPorId(idNaoEncontrado);
            conta.Should().BeNull();
        }

        #endregion

        #region DELETE
        [Test]
        public void Conta_repositorio_deletar_deve_funcionar()
        {
            var contaRemovida = _contaRepositorio.Deletar(_contaSeed.Id);
            contaRemovida.Should().BeTrue();
            _contexto.Contas.Where(p => p.Id == _contaSeed.Id).FirstOrDefault().Should().BeNull();
        }

        [Test]
        public void Conta_repositorio_deletar_deve_retornar_excecao_clienteId_desconhecido()
        {
            var idNaoEncontrado = 10;
            Action remover = () => _contaRepositorio.Deletar(idNaoEncontrado);
            remover.Should().Throw<NaoEncontradoException>();
        }

        #endregion

        #region UPDATE

        [Test]
        public void Conta_Repositorio_Atualizar_DeveFuncionar()
        {
            var foiAtualizado = false;
            _contaSeed.Estado = false;
            var actionAtualizar = new Action(() => { foiAtualizado = _contaRepositorio.Atualizar(_contaSeed); });
            var conta = _contaRepositorio.PegarPorId(1);
            actionAtualizar.Should().NotThrow<Exception>();
            foiAtualizado.Should().BeTrue();
            conta.Estado.Should().Be(false);
        }

        [Test]
        public void Conta_Repositorio_Atualizar_Deve_Retornar_Excecao_ClienteId_Desconhecido()
        {
            _conta = ObjectMother.ObtemContaValida();
            var idDesconhecido = 20;
            _conta.Id = idDesconhecido;
            Action updatedAction = () => _contaRepositorio.Atualizar(_conta);
            updatedAction.Should().Throw<DbUpdateConcurrencyException>();
        }
        #endregion
    }
}
