using FluentAssertions;
using Moq;
using NFe.Aplicacao.Features.Transportadores;
using NFe.Aplicacao.Features.Transportadores.Queries;
using NFe.Aplicacao.Testes.Inicializador;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Enderecos;
using NFe.Dominio.Features.Transportadores;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Aplicacao.Testes.Features.Transportadores
{
    [TestFixture]
    public class TransportadorAplicacaoTeste : BaseServicoTestes
    {
        private ITransportadorServico _servico;
        private Mock<ITransportadorRepositorio> _transportadorRepositorioFake;

        [SetUp]
        public void Initialize()
        {
            _transportadorRepositorioFake = new Mock<ITransportadorRepositorio>();
            _servico = new TransportadorServico(_transportadorRepositorioFake.Object);
        }

        #region ADD 
        [Test]
        public void Transportador_Servico_Adicionar_DeveSalvar()
        {
            //Arrange
            var transportadorCmd = ObjectMother.ObtemTransportadorValidoAdicionar();
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjERazaoSocial();
            _transportadorRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Transportador>())).Returns(transportador);

            //Action
            var novoTransportadorId = _servico.Adicionar(transportadorCmd);

            //Assert
            _transportadorRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Transportador>()), Times.Once);
            novoTransportadorId.Should().Be((int)transportador.Id);
        }


        [Test]
        public void Transportador_Servico_Adicionar_DeveRetornarExcessao()
        {
            //Arrange
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjERazaoSocial();
            var transportadorCmd = ObjectMother.ObtemTransportadorValidoAdicionar();
            _transportadorRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Transportador>())).Throws<Exception>();
            var novoTransportadorId = 0;

            //Action
            Action action = () => { novoTransportadorId = _servico.Adicionar(transportadorCmd); };

            //Assert
            action.Should().Throw<Exception>();
            _transportadorRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Transportador>()), Times.Once);
        }

        #endregion

        #region GET 
        [Test]
        public void Transportador_Servico_PegarTodos_DevePassar()
        {
            //Arrange
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjERazaoSocial();
            var repositoryMockValue = new List<Transportador>() { transportador }.AsQueryable();
            _transportadorRepositorioFake.Setup(odr => odr.PegarTodos()).Returns(repositoryMockValue);

            //Action
            var resultadoTransportador = _servico.PegarTodos();

            //Assert
            _transportadorRepositorioFake.Verify(odr => odr.PegarTodos(), Times.Once);
            resultadoTransportador.Should().NotBeNull();
            resultadoTransportador.Count().Should().Be(repositoryMockValue.Count());
            resultadoTransportador.First().Should().Be(repositoryMockValue.First());
        }

        [Test]
        public void Transportador_Servico_PegarPorId_DevePassar()
        {
            //Arrange
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjERazaoSocial();
            _transportadorRepositorioFake.Setup(odr => odr.PegarPorId(transportador.Id)).Returns(transportador);

            //Action
            var resultadoTransportador = _servico.PegarPorId((int)transportador.Id);

            //Assert
            _transportadorRepositorioFake.Verify(odr => odr.PegarPorId(transportador.Id), Times.Once);
            resultadoTransportador.Should().NotBeNull();
            resultadoTransportador.Should().BeOfType<TransportadorQuery>();
            resultadoTransportador.Id.Should().Be((int)transportador.Id);
        }

        [Test]
        public void Transportador_Servico_PegarPorId_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjERazaoSocial();
            var exception = new NotFoundException();
            _transportadorRepositorioFake.Setup(odr => odr.PegarPorId(transportador.Id)).Throws(exception);

            //Action
            Action action = () => _servico.PegarPorId((int)transportador.Id);

            //Assert
            action.Should().Throw<NotFoundException>();
            _transportadorRepositorioFake.Verify(odr => odr.PegarPorId((int)transportador.Id), Times.Once);
        }

        #endregion

        #region DELETE
        [Test]
        public void Transportador_Servico_Deletar_DevePassar()
        {
            //Arrange
            var transportadorCmd = ObjectMother.ObtemTransportadorValidoRemover();
            var mockIsRemoved = true;
            _transportadorRepositorioFake.Setup(odr => odr.Deletar(transportadorCmd.Id)).Returns(mockIsRemoved);

            //Action
            var transportadorRemovido = _servico.Deletar(transportadorCmd);

            //Assert
            _transportadorRepositorioFake.Verify(odr => odr.Deletar(transportadorCmd.Id), Times.Once);
            transportadorRemovido.Should().BeTrue();
        }

        [Test]
        public void Transportador_Servico_Deletar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var detinatarioCmd = ObjectMother.ObtemTransportadorValidoRemover();
            var exception = new NotFoundException();
            _transportadorRepositorioFake.Setup(odr => odr.Deletar(detinatarioCmd.Id)).Throws(exception);

            //Action
            Action action = () => _servico.Deletar(detinatarioCmd);

            //Assert
            action.Should().Throw<NotFoundException>();
            _transportadorRepositorioFake.Verify(odr => odr.Deletar(detinatarioCmd.Id), Times.Once);
        }

        #endregion

        #region UPDATE
        [Test]
        public void Transportador_Servico_Atualizar_DevePassar()
        {
            //Arrange
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjERazaoSocial();
            var transportadorCmd = ObjectMother.ObtemTransportadorValidoAtualizar();
            var isUpdated = true;
            _transportadorRepositorioFake.Setup(odr => odr.PegarPorId(transportadorCmd.Id)).Returns(transportador);
            _transportadorRepositorioFake.Setup(odr => odr.Atualizar(transportador)).Returns(isUpdated);

            //Action
            var transportadorAtualizado = _servico.Atualizar(transportadorCmd);

            //Assert
            _transportadorRepositorioFake.Verify(odr => odr.PegarPorId(transportadorCmd.Id), Times.Once);
            _transportadorRepositorioFake.Verify(odr => odr.Atualizar(transportador), Times.Once);
            transportadorAtualizado.Should().BeTrue();
        }

        [Test]
        public void Transportador_Servico_Atualizar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var transportador = ObjectMother.ObtemTransportadorValidoAtualizar();
            _transportadorRepositorioFake.Setup(odr => odr.PegarPorId(transportador.Id)).Returns((Transportador)null);

            //Action
            Action action = () => _servico.Atualizar(transportador);

            //Assert
            action.Should().Throw<NotFoundException>();
            _transportadorRepositorioFake.Verify(odr => odr.PegarPorId(transportador.Id), Times.Once);
            _transportadorRepositorioFake.Verify(odr => odr.Atualizar(It.IsAny<Transportador>()), Times.Never);
        }

        #endregion

    }
}
