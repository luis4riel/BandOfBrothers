using FluentAssertions;
using Moq;
using NFe.Aplicacao.Features.Emitentes;
using NFe.Aplicacao.Features.Emitentes.Queries;
using NFe.Aplicacao.Testes.Inicializador;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Emitentes;
using NFe.Dominio.Features.Enderecos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Aplicacao.Testes.Features.Emitentes
{
    [TestFixture]
    public class EmitenteAplicacaoTeste : BaseServicoTestes
    {
        private IEmitenteServico _servico;
        private Mock<IEmitenteRepositorio> _emitenteRepositorioFake;

        [SetUp]
        public void Initialize()
        {
            _emitenteRepositorioFake = new Mock<IEmitenteRepositorio>();
            _servico = new EmitenteServico(_emitenteRepositorioFake.Object);
        }

        #region ADD 
        [Test]
        public void Emitente_Servico_Adicionar_DeveSalvar()
        {
            //Arrange
            var emitenteCmd = ObjectMother.ObtemEmitenteValidoAdicionar();
            var emitente = ObjectMother.ObtemEmitenteValido();
            _emitenteRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Emitente>())).Returns(emitente);

            //Action
            var novoEmitenteId = _servico.Adicionar(emitenteCmd);

            //Assert
            _emitenteRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Emitente>()), Times.Once);
            novoEmitenteId.Should().Be((int)emitente.Id);
        }


        [Test]
        public void Emitente_Servico_Adicionar_DeveRetornarExcessao()
        {
            //Arrange
            var emitente = ObjectMother.ObtemEmitenteValido();
            var emitenteCmd = ObjectMother.ObtemEmitenteValidoAdicionar();
            _emitenteRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Emitente>())).Throws<Exception>();
            var novoEmitenteId = 0;

            //Action
            Action action = () => { novoEmitenteId = _servico.Adicionar(emitenteCmd); };

            //Assert
            action.Should().Throw<Exception>();
            _emitenteRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Emitente>()), Times.Once);
        }

        #endregion

        #region GET 
        [Test]
        public void Emitente_Servico_PegarTodos_DevePassar()
        {
            //Arrange
            var emitente = ObjectMother.ObtemEmitenteValido();
            var repositoryMockValue = new List<Emitente>() { emitente }.AsQueryable();
            _emitenteRepositorioFake.Setup(odr => odr.PegarTodos()).Returns(repositoryMockValue);

            //Action
            var resultadoEmitente = _servico.PegarTodos();

            //Assert
            _emitenteRepositorioFake.Verify(odr => odr.PegarTodos(), Times.Once);
            resultadoEmitente.Should().NotBeNull();
            resultadoEmitente.Count().Should().Be(repositoryMockValue.Count());
            resultadoEmitente.First().Should().Be(repositoryMockValue.First());
        }

        [Test]
        public void Emitente_Servico_PegarPorId_DevePassar()
        {
            //Arrange
            var emitente = ObjectMother.ObtemEmitenteValido();
            _emitenteRepositorioFake.Setup(odr => odr.PegarPorId(emitente.Id)).Returns(emitente);

            //Action
            var resultadoEmitente = _servico.PegarPorId((int)emitente.Id);

            //Assert
            _emitenteRepositorioFake.Verify(odr => odr.PegarPorId(emitente.Id), Times.Once);
            resultadoEmitente.Should().NotBeNull();
            resultadoEmitente.Should().BeOfType<EmitenteQuery>();
            resultadoEmitente.Id.Should().Be((int)emitente.Id);
        }

        [Test]
        public void Emitente_Servico_PegarPorId_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var emitente = ObjectMother.ObtemEmitenteValido();
            var exception = new NotFoundException();
            _emitenteRepositorioFake.Setup(odr => odr.PegarPorId(emitente.Id)).Throws(exception);

            //Action
            Action action = () => _servico.PegarPorId((int)emitente.Id);

            //Assert
            action.Should().Throw<NotFoundException>();
            _emitenteRepositorioFake.Verify(odr => odr.PegarPorId((int)emitente.Id), Times.Once);
        }

        #endregion

        #region DELETE
        [Test]
        public void Emitente_Servico_Deletar_DevePassar()
        {
            //Arrange
            var emitenteCmd = ObjectMother.ObtemEmitenteValidoRemover();
            var mockIsRemoved = true;
            _emitenteRepositorioFake.Setup(odr => odr.Deletar(emitenteCmd.Id)).Returns(mockIsRemoved);

            //Action
            var emitenteRemovido = _servico.Deletar(emitenteCmd);

            //Assert
            _emitenteRepositorioFake.Verify(odr => odr.Deletar(emitenteCmd.Id), Times.Once);
            emitenteRemovido.Should().BeTrue();
        }

        [Test]
        public void Emitente_Servico_Deletar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var detinatarioCmd = ObjectMother.ObtemEmitenteValidoRemover();
            var exception = new NotFoundException();
            _emitenteRepositorioFake.Setup(odr => odr.Deletar(detinatarioCmd.Id)).Throws(exception);

            //Action
            Action action = () => _servico.Deletar(detinatarioCmd);

            //Assert
            action.Should().Throw<NotFoundException>();
            _emitenteRepositorioFake.Verify(odr => odr.Deletar(detinatarioCmd.Id), Times.Once);
        }

        #endregion

        #region UPDATE
        [Test]
        public void Emitente_Servico_Atualizar_DevePassar()
        {
            //Arrange
            var emitente = ObjectMother.ObtemEmitenteValido();
            var emitenteCmd = ObjectMother.ObtemEmitenteValidoAtualizar();
            var isUpdated = true;
            _emitenteRepositorioFake.Setup(odr => odr.PegarPorId(emitenteCmd.Id)).Returns(emitente);
            _emitenteRepositorioFake.Setup(odr => odr.Atualizar(emitente)).Returns(isUpdated);

            //Action
            var emitenteAtualizado = _servico.Atualizar(emitenteCmd);

            //Assert
            _emitenteRepositorioFake.Verify(odr => odr.PegarPorId(emitenteCmd.Id), Times.Once);
            _emitenteRepositorioFake.Verify(odr => odr.Atualizar(emitente), Times.Once);
            emitenteAtualizado.Should().BeTrue();
        }

        [Test]
        public void Emitente_Servico_Atualizar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var emitente = ObjectMother.ObtemEmitenteValidoAtualizar();
            _emitenteRepositorioFake.Setup(odr => odr.PegarPorId(emitente.Id)).Returns((Emitente)null);

            //Action
            Action action = () => _servico.Atualizar(emitente);

            //Assert
            action.Should().Throw<NotFoundException>();
            _emitenteRepositorioFake.Verify(odr => odr.PegarPorId(emitente.Id), Times.Once);
            _emitenteRepositorioFake.Verify(odr => odr.Atualizar(It.IsAny<Emitente>()), Times.Never);
        }

        #endregion

    }
}
