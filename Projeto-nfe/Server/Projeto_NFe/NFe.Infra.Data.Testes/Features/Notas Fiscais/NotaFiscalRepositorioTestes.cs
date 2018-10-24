using Effort;
using FluentAssertions;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Infra.Data.Features.Notas_Fiscais;
using NFe.Infra.Data.Testes.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Infra.Data.Testes.Features.Notas_Fiscais
{
    [TestFixture]
    public class NotaFiscalRepositorioTestes
    {
        private FakeDbContext _context;
        INotaFiscalRepositorio repositorio;
        NotaFiscal notaFiscal;
        NotaFiscal notaSeed;

        [SetUp]
        public void SetUp()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);

            repositorio = new NotaFIscalRepositorio(_context);
            notaFiscal = ObjectMother.ObterNotaValida();
            notaFiscal.Destinatario = _context.Destinatarios.Add(ObjectMother.ObtemDestinatarioValido());
            notaFiscal.Emitente = _context.Emitentes.Add(ObjectMother.ObtemEmitenteValido());
            notaFiscal.Transportador = _context.Transportadores.Add(ObjectMother.ObterTransportadorValidoComCpfENome());
            notaSeed = ObjectMother.ObterNotaValida();
            var result = _context.NotasFiscais.Add(notaFiscal);

            _context.SaveChanges();
        }

        [Test]
        public void NotaFiscal_InfraData_Salvar_DeveInserirOK()
        {


            notaFiscal.GerarChaveAcesso();

            NotaFiscal notaInserida = repositorio.Salvar(notaFiscal);

            var idEsperado = 1;
            notaInserida.Id.Should().Be(idEsperado);
        }

        [Test]
        public void NotaFiscal_InfraData_Atualizar_DeveAtualizarOk()
        {

            var novoNome = "COMPRA";
            notaFiscal.NaturezaOperacao = novoNome;

            var notaAtualizado = repositorio.Atualizar(notaFiscal);

            notaAtualizado.Should().BeTrue();
        }

        [Test]
        public void NotaFiscal_InfraData_PegarTodos_DevePegarTodos()
        {

            notaFiscal.GerarChaveAcesso();
            notaFiscal = repositorio.Salvar(notaFiscal);

            IEnumerable<NotaFiscal> notas = repositorio.PegarTodos();

            notas.Count().Should().BeGreaterThan(0);
            notas.Last().Id.Should().Be(notaFiscal.Id);
        }

        [Test]
        public void NotaFiscal_InfraData_PegarPorId_DevePegarPorId()
        {
            IEnumerable<NotaFiscal> notas = repositorio.PegarTodos();

            NotaFiscal _notaEncontrada = repositorio.PegarPorId(notas.Last().Id);

            _notaEncontrada.Should().NotBeNull();
            _notaEncontrada.Id.Should().Be(notas.Last().Id);
        }

        [Test]
        public void NotaFiscal_InfraData_Deletar_DeveDeletar()
        {
            int id = 1;

            notaFiscal = repositorio.Salvar(notaFiscal);

            repositorio.Deletar(id);

            NotaFiscal _notaEncontrada = repositorio.PegarPorId(id);
            _notaEncontrada.Should().BeNull();
        }
        [Test]
        public void NotaFiscal_InfraData_DeletarInexistente_DeveRetornarExcecao()
        {
            int id = 2;
            Action action = () => repositorio.Deletar(id);

            action.Should().Throw<NotFoundException>();
        }

    }
}
