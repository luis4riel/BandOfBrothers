using FluentAssertions;
using Moq;
using NFe.Aplicacao.Features.Produtos;
using NFe.Aplicacao.Features.Produtos.Queries;
using NFe.Aplicacao.Testes.Inicializador;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Produtos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Aplicacao.Testes.Features.Produtos
{
    [TestFixture]
    public class ProdutoAplicacaoTeste : BaseServicoTestes
    {
        private IProdutoServico _servico;
        private Mock<IProdutoRepositorio> _produtoRepositorioFake;

        [SetUp]
        public void Initialize()
        {
            _produtoRepositorioFake = new Mock<IProdutoRepositorio>();
            _servico = new ProdutoServico(_produtoRepositorioFake.Object);
        }

        #region ADD 
        [Test]
        public void Produto_Servico_Adicionar_DeveSalvar()
        {
            //Arrange
            var produtoCmd = ObjectMother.ObtemProdutoValidoAdicionar();
            var produto = ObjectMother.ObtemProdutoValido();
            _produtoRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Produto>())).Returns(produto);

            //Action
            var novoProdutoId = _servico.Adicionar(produtoCmd);

            //Assert
            _produtoRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Produto>()), Times.Once);
            novoProdutoId.Should().Be((int)produto.Id);
        }


        [Test]
        public void Produto_Servico_Adicionar_DeveRetornarExcessao()
        {
            //Arrange
            var produto = ObjectMother.ObtemProdutoValido();
            var produtoCmd = ObjectMother.ObtemProdutoValidoAdicionar();
            _produtoRepositorioFake.Setup(odr => odr.Salvar(It.IsAny<Produto>())).Throws<Exception>();
            var novoProdutoId = 0;

            //Action
            Action action = () => { novoProdutoId = _servico.Adicionar(produtoCmd); };

            //Assert
            action.Should().Throw<Exception>();
            _produtoRepositorioFake.Verify(odr => odr.Salvar(It.IsAny<Produto>()), Times.Once);
        }

        #endregion

        #region GET 
        [Test]
        public void Produto_Servico_PegarTodos_DevePassar()
        {
            //Arrange
            var produto = ObjectMother.ObtemProdutoValido();
            var repositoryMockValue = new List<Produto>() { produto }.AsQueryable();
            _produtoRepositorioFake.Setup(odr => odr.PegarTodos()).Returns(repositoryMockValue);

            //Action
            var resultadoProduto = _servico.PegarTodos();

            //Assert
            _produtoRepositorioFake.Verify(odr => odr.PegarTodos(), Times.Once);
            resultadoProduto.Should().NotBeNull();
            resultadoProduto.Count().Should().Be(repositoryMockValue.Count());
            resultadoProduto.First().Should().Be(repositoryMockValue.First());
        }

        [Test]
        public void Produto_Servico_PegarPorId_DevePassar()
        {
            //Arrange
            var produto = ObjectMother.ObtemProdutoValido();
            _produtoRepositorioFake.Setup(odr => odr.PegarPorId(produto.Id)).Returns(produto);

            //Action
            var resultadoProduto = _servico.PegarPorId((int)produto.Id);

            //Assert
            _produtoRepositorioFake.Verify(odr => odr.PegarPorId(produto.Id), Times.Once);
            resultadoProduto.Should().NotBeNull();
            resultadoProduto.Should().BeOfType<ProdutoQuery>();
            resultadoProduto.Id.Should().Be((int)produto.Id);
        }

        [Test]
        public void Produto_Servico_PegarPorId_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var produto = ObjectMother.ObtemProdutoValido();
            var exception = new NotFoundException();
            _produtoRepositorioFake.Setup(odr => odr.PegarPorId(produto.Id)).Throws(exception);

            //Action
            Action action = () => _servico.PegarPorId((int)produto.Id);

            //Assert
            action.Should().Throw<NotFoundException>();
            _produtoRepositorioFake.Verify(odr => odr.PegarPorId((int)produto.Id), Times.Once);
        }

        #endregion

        #region DELETE
        [Test]
        public void Produto_Servico_Deletar_DevePassar()
        {
            //Arrange
            var produtoCmd = ObjectMother.ObtemProdutoValidoRemover();
            var mockIsRemoved = true;
            _produtoRepositorioFake.Setup(odr => odr.Deletar(produtoCmd.Id)).Returns(mockIsRemoved);

            //Action
            var produtoRemovido = _servico.Deletar(produtoCmd);

            //Assert
            _produtoRepositorioFake.Verify(odr => odr.Deletar(produtoCmd.Id), Times.Once);
            produtoRemovido.Should().BeTrue();
        }

        [Test]
        public void Produto_Servico_Deletar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var detinatarioCmd = ObjectMother.ObtemProdutoValidoRemover();
            var exception = new NotFoundException();
            _produtoRepositorioFake.Setup(odr => odr.Deletar(detinatarioCmd.Id)).Throws(exception);

            //Action
            Action action = () => _servico.Deletar(detinatarioCmd);

            //Assert
            action.Should().Throw<NotFoundException>();
            _produtoRepositorioFake.Verify(odr => odr.Deletar(detinatarioCmd.Id), Times.Once);
        }

        #endregion

        #region UPDATE
        [Test]
        public void Produto_Servico_Atualizar_DevePassar()
        {
            //Arrange
            var produto = ObjectMother.ObtemProdutoValido();
            var produtoCmd = ObjectMother.ObtemProdutoValidoAtualizar();
            var isUpdated = true;
            _produtoRepositorioFake.Setup(odr => odr.PegarPorId(produtoCmd.Id)).Returns(produto);
            _produtoRepositorioFake.Setup(odr => odr.Atualizar(produto)).Returns(isUpdated);

            //Action
            var produtoAtualizado = _servico.Atualizar(produtoCmd);

            //Assert
            _produtoRepositorioFake.Verify(odr => odr.PegarPorId(produtoCmd.Id), Times.Once);
            _produtoRepositorioFake.Verify(odr => odr.Atualizar(produto), Times.Once);
            produtoAtualizado.Should().BeTrue();
        }

        [Test]
        public void Produto_Servico_Atualizar_DeveEmitirNaoEncontradoException()
        {
            //Arrange
            var produto = ObjectMother.ObtemProdutoValidoAtualizar();
            _produtoRepositorioFake.Setup(odr => odr.PegarPorId(produto.Id)).Returns((Produto)null);

            //Action
            Action action = () => _servico.Atualizar(produto);

            //Assert
            action.Should().Throw<NotFoundException>();
            _produtoRepositorioFake.Verify(odr => odr.PegarPorId(produto.Id), Times.Once);
            _produtoRepositorioFake.Verify(odr => odr.Atualizar(It.IsAny<Produto>()), Times.Never);
        }

        #endregion

    }
}
