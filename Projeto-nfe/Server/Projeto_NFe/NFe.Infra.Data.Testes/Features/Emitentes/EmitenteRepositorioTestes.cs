using Effort;
using FluentAssertions;
using Moq;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Emitentes;
using NFe.Dominio.Features.Enderecos;
using NFe.Infra.Data.Features.Emitentes;
using NFe.Infra.Data.Testes.Context;
using NFe.Infra.Data.Testes.Initializer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Infra.Data.Testes.Features.Emitentes
{
    [TestFixture]
    public class EmitenteRepositorioTestes : EffortTestBase
    {
        private FakeDbContext _context;

        private IEmitenteRepositorio repositorio;
        private Emitente emitente;
        Emitente _emitenteSeed;

        [SetUp]
        public void SetUp()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);

            repositorio = new EmitenteRepositorio(_context);
            emitente = new Emitente();
            _emitenteSeed = ObjectMother.ObtemEmitenteValido();
        }

        [Test]
        public void Emitente_InfraData_Salvar_DeveInserirOK()
        {
            emitente = ObjectMother.ObtemEmitenteValido();

            Emitente _emitenteInserido = repositorio.Salvar(emitente);

            _emitenteInserido.Should().NotBeNull();
            _emitenteInserido.Id.Should().Be(emitente.Id);

        }

        [Test]
        public void Emitente_InfraData_Salvar_DeveInserirEmitenteSemEnderecoSalvo()
        {
            emitente = ObjectMother.ObtemEmitenteValido();

            Emitente emitenteInserido = repositorio.Salvar(emitente);

            emitenteInserido.Id.Should().Be(emitente.Id);
        }

        [Test]
        public void Emitente_InfraData_Atualizar_DeveAtualizarOk()
        {
            emitente = ObjectMother.ObtemEmitenteValido();
            emitente = repositorio.Salvar(emitente);
            var _novoNome = "Cia João";
            emitente.Nome = _novoNome;

            var _emitenteAtualizado = repositorio.Atualizar(emitente);

            _emitenteAtualizado.Should().BeTrue();
        }

        [Test]
        public void Emitente_InfraData_PegarTodos_DevePegarTodos()
        {
            emitente = ObjectMother.ObtemEmitenteValido();
            emitente = repositorio.Salvar(emitente);

            IEnumerable<Emitente> emitentes = repositorio.PegarTodos();

            emitentes.Count().Should().BeGreaterThan(0);
            emitentes.Last().Id.Should().Be(emitente.Id);
        }

        [Test]
        public void Emitente_InfraData_PegarPorId_DevePegarPorIdOk()
        {
            emitente = ObjectMother.ObtemEmitenteValido();
            emitente = repositorio.Salvar(emitente);

            Emitente emitentePego = repositorio.PegarPorId(emitente.Id);

            emitentePego.Should().NotBeNull();
        }

        [Test]
        public void Emitente_InfraData_Deletar_DeveDeletar()
        {
            emitente = ObjectMother.ObtemEmitenteValido();
            emitente = repositorio.Salvar(emitente);

            repositorio.Deletar(emitente.Id);

            Emitente _emitenteEncontrado = repositorio.PegarPorId(emitente.Id);
            _emitenteEncontrado.Should().BeNull();
        }

        [Test]
        public void Emitente_InfraData_Deletar_ID_Invalido_DeveRetornarNotFoundException()
        {
            int id = 2;
            Action action = () => repositorio.Deletar(id);

            action.Should().Throw<NotFoundException>();
        }
    }
}
