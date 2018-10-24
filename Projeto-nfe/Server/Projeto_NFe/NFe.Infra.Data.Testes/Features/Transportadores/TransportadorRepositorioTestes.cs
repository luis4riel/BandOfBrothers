using Effort;
using FluentAssertions;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Transportadores;
using NFe.Infra.Data.Features.Transportadores;
using NFe.Infra.Data.Testes.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Infra.Data.Testes.Features.Transportadores
{
    [TestFixture]
    public class TransportadorRepositorioTestes
    {
        private FakeDbContext _context;

        private ITransportadorRepositorio repositorio;
        private Transportador _transportador;
        Transportador _transportadorSeed;



        [SetUp]
        public void InitializeObjects()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);

            repositorio = new TransportadorRepositorio(_context);
            _transportador = new Transportador();
            _transportadorSeed = ObjectMother.ObterTransportadorValidoComCpfENome();

        }

        [Test]
        public void Transportador_InfraData_Salvar_DeveInserirOK()
        {
            _transportador = ObjectMother.ObterTransportadorValidoComCpfENome();

            Transportador _transportadorInserido = repositorio.Salvar(_transportador);

            _transportadorInserido.Should().NotBeNull();
            _transportadorInserido.Id.Should().Be(_transportador.Id);

        }

        [Test]
        public void Transportador_InfraData_Salvar_DeveInserirTransportadorSemEnderecoSalvo()
        {
            _transportador = ObjectMother.ObterTransportadorValidoComCnpjENome();

            Transportador transportadorInserido = repositorio.Salvar(_transportador);

            transportadorInserido.Id.Should().Be(_transportador.Id);
        }

        [Test]
        public void Transportador_InfraData_Atualizar_DeveAtualizarOk()
        {
            var _novoNome = "Cia João";
            _transportador = ObjectMother.ObterTransportadorValidoComCnpjENome();
            _transportador = repositorio.Salvar(_transportador);
            _transportador.Nome = _novoNome;

            var transportadorAtualizado = repositorio.Atualizar(_transportador);

            transportadorAtualizado.Should().BeTrue();
        }

        [Test]
        public void Transportador_InfraData_PegarTodos_DevePegarTodos()
        {
            _transportador = ObjectMother.ObterTransportadorValidoComCnpjENome();
            _transportador = repositorio.Salvar(_transportador);

            IEnumerable<Transportador> emitentes = repositorio.PegarTodos();

            emitentes.Count().Should().BeGreaterThan(0);
            emitentes.Last().Id.Should().Be(_transportador.Id);
        }

        [Test]
        public void Transportador_InfraData_Deletar_DeveDeletarOk()
        {
            _transportador = ObjectMother.ObterTransportadorValidoComCnpjENome();
            _transportador = repositorio.Salvar(_transportador);

            repositorio.Deletar(_transportador.Id);

            Transportador transportadorEncontrado = repositorio.PegarPorId(_transportador.Id);
            transportadorEncontrado.Should().BeNull();
        }

        [Test]
        public void Transportador_InfraData_Deletar_ID_Invalido_DeveRetornarNotFoundException()
        {
            int id = 2;
            Action action = () => repositorio.Deletar(id);

            action.Should().Throw<NotFoundException>();
        }
    }
}
