using FluentAssertions;
using Moq;
using NFe.Aplicacao.Features.Notas_Fiscais;
using NFe.Aplicacao.Features.Notas_Fiscais.Queries;
using NFe.Aplicacao.Testes.Inicializador;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Destinatarios;
using NFe.Dominio.Features.Emitentes;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Dominio.Features.Transportadores;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Aplicacao.Testes.Features.Notas_Fiscais
{
    [TestFixture]
    public class NotaFiscalAplicacaoTeste : BaseServicoTestes
    {
        private INotaFiscalServico _servico;
        private Mock<INotaFiscalRepositorio> _notaFiscalRepositorioFake;
        private Mock<IEmitenteRepositorio> _emitenteRepositorioFake;
        private Mock<ITransportadorRepositorio> _transportadorRepositorioFake;
        private Mock<IDestinatarioRepositorio> _destinatarioRepositorioFake;

        [SetUp]
        public void Initialize()
        {
            _notaFiscalRepositorioFake = new Mock<INotaFiscalRepositorio>();
            _emitenteRepositorioFake = new Mock<IEmitenteRepositorio>();
            _transportadorRepositorioFake = new Mock<ITransportadorRepositorio>();
            _destinatarioRepositorioFake = new Mock<IDestinatarioRepositorio>();
            _servico = new NotaFiscalServico(_notaFiscalRepositorioFake.Object, _emitenteRepositorioFake.Object, _transportadorRepositorioFake.Object, _destinatarioRepositorioFake.Object );
        }

        #region ADD 
        [Test]
        public void NotaFiscal_Servico_Adicionar_DeveSalvar()
        {
            //Arrange
            var notaFiscalCmd = ObjectMother.ObtemNotaFiscalValidoAdicionar();
            var notaFiscal = ObjectMother.ObterNotaEmitidaValida();
            var emitente = ObjectMother.ObtemEmitenteValido();
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjENome();
            var destinatario = ObjectMother.ObtemDestinatarioValidoComCnpjRazaoSocial();

            _notaFiscalRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<NotaFiscal>())).Returns(notaFiscal);
            _emitenteRepositorioFake.Setup( odr => odr.PegarPorId(emitente.Id)).Returns(emitente);
            _transportadorRepositorioFake.Setup(odr => odr.PegarPorId(transportador.Id)).Returns(transportador);
            _destinatarioRepositorioFake.Setup(odr => odr.PegarPorId(destinatario.Id)).Returns(destinatario);

            //Action
            var novoNotaFiscalId = _servico.Adicionar(notaFiscalCmd);

            //Assert
            _notaFiscalRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<NotaFiscal>()), Times.Once);
            novoNotaFiscalId.Should().Be((int)notaFiscal.Id);
        }


        [Test]
        public void NotaFiscal_Servico_Adicionar_DeveRetornarExcessaoEmitenteInvalido()
        {
            //Arrange
            var notaFiscal = ObjectMother.ObterNotaEmitidaValida();
            var notaFiscalCmd = ObjectMother.ObtemNotaFiscalValidoAdicionar();
            _notaFiscalRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<NotaFiscal>())).Throws<NotFoundException>();
            var novoNotaFiscalId = 0;

            //Action
            Action action = () => { novoNotaFiscalId = _servico.Adicionar(notaFiscalCmd); };

            //Assert
            action.Should().Throw<NotFoundException>();
            _notaFiscalRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<NotaFiscal>()), Times.Never);
        }

        #endregion

        #region GET 
        [Test]
        public void NotaFiscal_Servico_PegarTodos_DevePassar()
        {
            //Arrange
            var notaFiscal = ObjectMother.ObterNotaEmitidaValida();
            var repositoryMockValue = new List<NotaFiscal>() { notaFiscal }.AsQueryable();
            _notaFiscalRepositorioFake.Setup(odr => odr.PegarTodos()).Returns(repositoryMockValue);

            //Action
            var resultadoNotaFiscal = _servico.PegarTodos();

            //Assert
            _notaFiscalRepositorioFake.Verify(odr => odr.PegarTodos(), Times.Once);
            resultadoNotaFiscal.Should().NotBeNull();
            resultadoNotaFiscal.Count().Should().Be(repositoryMockValue.Count());
            resultadoNotaFiscal.First().Should().Be(repositoryMockValue.First());
        }

        [Test]
        public void NotaFiscal_Servico_PegarPorId_DevePassar()
        {
            //Arrange
            var notaFiscal = ObjectMother.ObterNotaEmitidaValida();
            _notaFiscalRepositorioFake.Setup(odr => odr.PegarPorId(notaFiscal.Id)).Returns(notaFiscal);

            //Action
            var resultadoNotaFiscal = _servico.PegarPorId((int)notaFiscal.Id);

            //Assert
            _notaFiscalRepositorioFake.Verify(odr => odr.PegarPorId(notaFiscal.Id), Times.Once);
            resultadoNotaFiscal.Should().NotBeNull();
            resultadoNotaFiscal.Should().BeOfType<NotaFiscalQuery>();
            resultadoNotaFiscal.Id.Should().Be((int)notaFiscal.Id);
        }

        [Test]
        public void NotaFiscal_Servico_PegarPorId_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var notaFiscal = ObjectMother.ObterNotaEmitidaValida();
            var exception = new NotFoundException();
            _notaFiscalRepositorioFake.Setup(odr => odr.PegarPorId(notaFiscal.Id)).Throws(exception);

            //Action
            Action action = () => _servico.PegarPorId((int)notaFiscal.Id);

            //Assert
            action.Should().Throw<NotFoundException>();
            _notaFiscalRepositorioFake.Verify(odr => odr.PegarPorId((int)notaFiscal.Id), Times.Once);
        }

        #endregion

        #region DELETE
        [Test]
        public void NotaFiscal_Servico_Deletar_DevePassar()
        {
            //Arrange
            var notaFiscalCmd = ObjectMother.ObtemNotaFiscalValidoRemover();
            var mockIsRemoved = true;
            _notaFiscalRepositorioFake.Setup(odr => odr.Deletar(notaFiscalCmd.Id)).Returns(mockIsRemoved);

            //Action
            var notaFiscalRemovido = _servico.Deletar(notaFiscalCmd);

            //Assert
            _notaFiscalRepositorioFake.Verify(odr => odr.Deletar(notaFiscalCmd.Id), Times.Once);
            notaFiscalRemovido.Should().BeTrue();
        }

        [Test]
        public void NotaFiscal_Servico_Deletar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var detinatarioCmd = ObjectMother.ObtemNotaFiscalValidoRemover();
            var exception = new NotFoundException();
            _notaFiscalRepositorioFake.Setup(odr => odr.Deletar(detinatarioCmd.Id)).Throws(exception);

            //Action
            Action action = () => _servico.Deletar(detinatarioCmd);

            //Assert
            action.Should().Throw<NotFoundException>();
            _notaFiscalRepositorioFake.Verify(odr => odr.Deletar(detinatarioCmd.Id), Times.Once);
        }

        #endregion

        #region UPDATE
        [Test]
        public void NotaFiscal_Servico_Atualizar_DevePassar()
        {
            //Arrange
            var notaFiscal = ObjectMother.ObterNotaEmitidaValida();
            var notaFiscalCmd = ObjectMother.ObtemNotaFiscalValidoAtualizar();
            var emitente = ObjectMother.ObtemEmitenteValido();
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjENome();
            var destinatario = ObjectMother.ObtemDestinatarioValidoComCnpjRazaoSocial();
            var isUpdated = true;
            _emitenteRepositorioFake.Setup(odr => odr.PegarPorId(emitente.Id)).Returns(emitente);
            _transportadorRepositorioFake.Setup(odr => odr.PegarPorId(transportador.Id)).Returns(transportador);
            _destinatarioRepositorioFake.Setup(odr => odr.PegarPorId(destinatario.Id)).Returns(destinatario);
            _notaFiscalRepositorioFake.Setup(odr => odr.PegarPorId(notaFiscalCmd.Id)).Returns(notaFiscal);
            _notaFiscalRepositorioFake.Setup(odr => odr.Atualizar(notaFiscal)).Returns(isUpdated);

            //Action
            var notaFiscalAtualizado = _servico.Atualizar(notaFiscalCmd);

            //Assert
            _notaFiscalRepositorioFake.Verify(odr => odr.PegarPorId(notaFiscalCmd.Id), Times.Once);
            _notaFiscalRepositorioFake.Verify(odr => odr.Atualizar(notaFiscal), Times.Once);
            notaFiscalAtualizado.Should().BeTrue();
        }

        [Test]
        public void NotaFiscal_Servico_Atualizar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var notaFiscal = ObjectMother.ObtemNotaFiscalValidoAtualizar();
            notaFiscal.EmitenteId = 100000;
            _notaFiscalRepositorioFake.Setup(odr => odr.PegarPorId(notaFiscal.Id)).Returns((NotaFiscal)null);

            //Action
            Action action = () => _servico.Atualizar(notaFiscal);

            //Assert
            action.Should().Throw<NotFoundException>();
            _notaFiscalRepositorioFake.Verify(odr => odr.PegarPorId(notaFiscal.Id), Times.Once);
            _notaFiscalRepositorioFake.Verify(odr => odr.Atualizar(It.IsAny<NotaFiscal>()), Times.Never);
        }

        #endregion

    }
}
