using Effort;
using FluentAssertions;
using NFe.Common.Testes.Features;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Produtos;
using NFe.Infra.Data.Features.Produtos;
using NFe.Infra.Data.Testes.Context;
using NFe.Infra.Data.Testes.Initializer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Infra.Data.Testes.Features.Produtos
{
    [TestFixture]
    public class ProdutoRepositorioTestes : EffortTestBase
    {
        private FakeDbContext _context;
        IProdutoRepositorio repositorio;
        Produto produto;
        Produto produtoSeed;

        [SetUp]
        public void SetUp()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);

            repositorio = new ProdutoRepositorio(_context);
            produto = new Produto();

            produtoSeed = ObjectMother.ObtemProdutoValido();
            _context.Produtos.Add(produtoSeed);
            _context.SaveChanges();
        }

        [Test]
        public void Produto_InfraData_Salvar_DeveInserirOK()
        {
            produto = ObjectMother.ObtemProdutoValido();

            Produto _produtoInserido = repositorio.Salvar(produto);

            _produtoInserido.Should().NotBeNull();
            _produtoInserido.Id.Should().Be(produto.Id);

        }

        [Test]
        public void Produto_InfraData_Atualizar_DeveAtualizarOk()
        {
            var removido = false;
            var novoValor = 200;
            produtoSeed.ValorProduto.Unitario = novoValor;
            var action = new Action(() => { removido = repositorio.Atualizar(produtoSeed); });
            action.Should().NotThrow<Exception>();
            removido.Should().BeTrue();
        }

        [Test]
        public void Produto_InfraData_PegarTodos_DevePegarTodos()
        {
            var products = repositorio.PegarTodos().ToList();
            products.Should().NotBeNull();
            products.Should().HaveCount(_context.Produtos.Count());
            products.First().Should().Be(produtoSeed);
        }

        [Test]
        public void Produto_InfraData_PegarPorId_DevePegarProduto()
        {
            var product = repositorio.PegarPorId(produtoSeed.Id);
            product.Should().NotBeNull();
            product.Should().Be(produtoSeed);
        }

        [Test]
        public void Produto_InfraData_Delete_DeveDeletar()
        {
            var removido = repositorio.Deletar(produtoSeed.Id);
            //Verify
            removido.Should().BeTrue();
            _context.Produtos.Where(p => p.Id == produto.Id).ToList().Should().BeEmpty();
        }

        [Test]
        public void Produto_InfraData_Deletar_ID_Invalido_DeveRetornarNotFoundException()
        {
            int id = 2;
            Action action = () => repositorio.Deletar(id);

            action.Should().Throw<NotFoundException>();
        }
    }
}
