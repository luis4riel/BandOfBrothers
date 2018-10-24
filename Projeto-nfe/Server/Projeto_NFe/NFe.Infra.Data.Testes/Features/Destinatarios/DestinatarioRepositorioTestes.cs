using Effort;
using FluentAssertions;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Destinatarios;
using NFe.Infra.Data.Features.Destinatarios;
using NFe.Infra.Data.Testes.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Infra.Data.Testes.Features.Destinatarios
{
    [TestFixture]
    public class DestinatarioRepositorioTestes
    {
        private FakeDbContext _context;

        private IDestinatarioRepositorio _repositorio;
        private Destinatario _destinatario;

        [SetUp]
        public void SetUp()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);

            _repositorio = new DestinatarioRepositorio(_context);
            _destinatario = new Destinatario();
        }

        [Test]
        public void Destinario_InfraData_Salvar_DeveInserirOK()
        {
            var idEsperado = 1;
            _destinatario = ObjectMother.ObtemDestinatarioValido();

            Destinatario destinatarioInserido = _repositorio.Salvar(_destinatario);

            destinatarioInserido.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Destinario_InfraData_Salvar_DeveInserirDestinatarioSemEnderecoSalvo()
        {
            var idEsperado = 1;
            _destinatario = ObjectMother.ObtemDestinatarioValido();

            Destinatario destinatarioInserido = _repositorio.Salvar(_destinatario);

            destinatarioInserido.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Destinario_InfraData_Atualizar_DeveAtualizarOk()
        {
            var _novoNome = "Cia João";
            _destinatario = ObjectMother.ObtemDestinatarioValido();
            _destinatario = _repositorio.Salvar(_destinatario);
            _destinatario.Nome = _novoNome;

            var destinatarioAtualizado = _repositorio.Atualizar(_destinatario);

            destinatarioAtualizado.Should().BeTrue();
        }

        [Test]
        public void Destinario_InfraData_PegarTodos_DevePegarTodos()
        {
            _destinatario = ObjectMother.ObtemDestinatarioValido();
            _destinatario = _repositorio.Salvar(_destinatario);

            IEnumerable<Destinatario> emitentes = _repositorio.PegarTodos();

            emitentes.Count().Should().BeGreaterThan(0);
            emitentes.Last().Id.Should().Be(_destinatario.Id);
        }

        [Test]
        public void Destinario_InfraData_Deletar_DeveDeletarOk()
        {
            _destinatario = ObjectMother.ObtemDestinatarioValido();
            _destinatario = _repositorio.Salvar(_destinatario);
            
            _repositorio.Deletar(Convert.ToInt32(_destinatario.Id));
            
            Destinatario destinatarioEncontrado = _repositorio.PegarPorId(_destinatario.Id);
            destinatarioEncontrado.Should().BeNull();
        }
        [Test]
        public void Destinario_InfraData_DeletarInesistente_DeveRetornarExcecao()
        {
            int id = 2;

            Action action = () => _repositorio.Deletar(id);

            action.Should().Throw<NotFoundException>();

        }

    }
}
