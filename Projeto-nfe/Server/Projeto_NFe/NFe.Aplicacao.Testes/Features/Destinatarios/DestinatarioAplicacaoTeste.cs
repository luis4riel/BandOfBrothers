using FluentAssertions;
using Moq;
using NFe.Aplicacao.Features.Destinatarios;
using NFe.Aplicacao.Features.Destinatarios.Queries;
using NFe.Aplicacao.Testes.Inicializador;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Destinatarios;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Aplicacao.Testes.Features.Destinatarios
{
    [TestFixture]
    public class DestinatarioAplicacaoTeste : BaseServicoTestes
    {
        private IDestinatarioServico _servico;
        private Mock<IDestinatarioRepositorio> _destinatarioRepositorioFake;

        [SetUp]
        public void Initialize()
        {
            _destinatarioRepositorioFake = new Mock<IDestinatarioRepositorio>();
            _servico = new DestinatarioServico(_destinatarioRepositorioFake.Object);
        }

        #region ADD 
        [Test]
        public void Destinatario_Servico_Adicionar_DeveSalvar()
        {
            //Arrange
            var destinatarioCmd = ObjectMother.ObtemDestinatarioValidoAdicionar();
            var destinatario = ObjectMother.ObtemDestinatarioValido();
            _destinatarioRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Destinatario>())).Returns(destinatario);
            
            //Action
            var novoDestinatarioId = _servico.Adicionar(destinatarioCmd);
            
            //Assert
            _destinatarioRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Destinatario>()), Times.Once);
            novoDestinatarioId.Should().Be((int)destinatario.Id);
        }

        [Test]
        public void Destinatario_Servico_Adicionar_DeveRetornarExcessao()
        {
            //Arrange
            var destinatario = ObjectMother.ObtemDestinatarioValido();
            var destinatarioCmd = ObjectMother.ObtemDestinatarioValidoAdicionar();
            _destinatarioRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Destinatario>())).Throws<Exception>();
            var novoDestinatarioId = 0;
            
            //Action
            Action action = () => { novoDestinatarioId = _servico.Adicionar(destinatarioCmd); };
            
            //Assert
            action.Should().Throw<Exception>();
            _destinatarioRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Destinatario>()), Times.Once);
        }

        #endregion

        #region GET 
        [Test]
        public void Destinatario_Servico_PegarTodos_DevePassar()
        {
            //Arrange
            var destinatario = ObjectMother.ObtemDestinatarioValido();
            var repositoryMockValue = new List<Destinatario>() { destinatario }.AsQueryable();
            _destinatarioRepositorioFake.Setup(odr => odr.PegarTodos()).Returns(repositoryMockValue);

            //Action
            var resultadoDestinatario = _servico.PegarTodos();

            //Assert
            _destinatarioRepositorioFake.Verify(odr => odr.PegarTodos(), Times.Once);
            resultadoDestinatario.Should().NotBeNull();
            resultadoDestinatario.Count().Should().Be(repositoryMockValue.Count());
            resultadoDestinatario.First().Should().Be(repositoryMockValue.First());
        }

        [Test]
        public void Destinatario_Servico_PegarPorId_DevePassar()
        {
            //Arrange
            var destinatario = ObjectMother.ObtemDestinatarioValido();
            _destinatarioRepositorioFake.Setup(odr => odr.PegarPorId(destinatario.Id)).Returns(destinatario);
            
            //Action
            var resultadoDestinatario = _servico.PegarPorId((int)destinatario.Id);
            
            //Assert
            _destinatarioRepositorioFake.Verify(odr => odr.PegarPorId(destinatario.Id), Times.Once);
            resultadoDestinatario.Should().NotBeNull();
            resultadoDestinatario.Should().BeOfType<DestinatarioQuery>();
            resultadoDestinatario.Id.Should().Be((int)destinatario.Id);
        }

        [Test]
        public void Destinatario_Servico_PegarPorId_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var destinatario = ObjectMother.ObtemDestinatarioValido();
            var exception = new NotFoundException();
            _destinatarioRepositorioFake.Setup(odr => odr.PegarPorId(destinatario.Id)).Throws(exception);
            
            //Action
            Action action = () => _servico.PegarPorId((int)destinatario.Id);
            
            //Assert
            action.Should().Throw<NotFoundException>();
            _destinatarioRepositorioFake.Verify(odr => odr.PegarPorId((int)destinatario.Id), Times.Once);
        }

        #endregion

        #region DELETE
        [Test]
        public void Destinatario_Servico_Deletar_DevePassar()
        {
            //Arrange
            var destinatarioCmd = ObjectMother.ObtemDestinatarioValidoRemover();
            var mockIsRemoved = true;
            _destinatarioRepositorioFake.Setup(odr => odr.Deletar(destinatarioCmd.Id)).Returns(mockIsRemoved);
            
            //Action
            var destinatarioRemovido = _servico.Deletar(destinatarioCmd);
            
            //Assert
            _destinatarioRepositorioFake.Verify(odr => odr.Deletar(destinatarioCmd.Id), Times.Once);
            destinatarioRemovido.Should().BeTrue();
        }

        [Test]
        public void Destinatario_Servico_Deletar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var detinatarioCmd = ObjectMother.ObtemDestinatarioValidoRemover();
            var exception = new NotFoundException();
            _destinatarioRepositorioFake.Setup(odr => odr.Deletar(detinatarioCmd.Id)).Throws(exception);
            
            //Action
            Action action = () => _servico.Deletar(detinatarioCmd);
            
            //Assert
            action.Should().Throw<NotFoundException>();
            _destinatarioRepositorioFake.Verify(odr => odr.Deletar(detinatarioCmd.Id), Times.Once);
        }

        #endregion

        #region UPDATE
        [Test]
        public void Destinatario_Servico_Atualizar_DevePassar()
        {
            //Arrange
            var destinatario = ObjectMother.ObtemDestinatarioValido();
            var destinatarioCmd = ObjectMother.ObtemDestinatarioValidoAtualizar();
            var isUpdated = true;
            _destinatarioRepositorioFake.Setup(odr => odr.PegarPorId(destinatarioCmd.Id)).Returns(destinatario);
            _destinatarioRepositorioFake.Setup(odr => odr.Atualizar(destinatario)).Returns(isUpdated);
            
            //Action
            var destinatarioAtualizado = _servico.Atualizar(destinatarioCmd);
            
            //Assert
            _destinatarioRepositorioFake.Verify(odr => odr.PegarPorId(destinatarioCmd.Id), Times.Once);
            _destinatarioRepositorioFake.Verify(odr => odr.Atualizar(destinatario), Times.Once);
            destinatarioAtualizado.Should().BeTrue();
        }

        [Test]
        public void Destinatario_Servico_Atualizar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var destinatario = ObjectMother.ObtemDestinatarioValidoAtualizar();
            _destinatarioRepositorioFake.Setup(odr => odr.PegarPorId(destinatario.Id)).Returns((Destinatario)null);
            
            //Action
            Action action = () => _servico.Atualizar(destinatario);
            
            //Assert
            action.Should().Throw<NotFoundException>();
            _destinatarioRepositorioFake.Verify(odr => odr.PegarPorId(destinatario.Id), Times.Once);
            _destinatarioRepositorioFake.Verify(odr => odr.Atualizar(It.IsAny<Destinatario>()), Times.Never);
        }


        #endregion

    }
}
