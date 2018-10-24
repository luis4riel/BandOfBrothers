using FluentAssertions;
using Moq;
using NUnit.Framework;
using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_pizzaria.Aplicacao.Teste.Features.Produtos
{
	[TestFixture]
	public class ProdutoServicoTeste
	{
		ProdutoServico _servico;
		Produto Produto;

		Mock<IProdutoRepositorio> _repositorio;

		[SetUp]
		public void InitializeObjects()
		{
			Produto = new Pizza("Café", 2);
			_repositorio = new Mock<IProdutoRepositorio>();
			_servico = new ProdutoServico( _repositorio.Object );
		}

		[Test]
		public void ProdutoServico_CriarRepositorio_DeveFuncionar()
		{
			Produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

			_repositorio.Setup( m => m.Salvar( Produto ) ).Returns( new Pizza("Café", 2) { Id = 1 } );

			Produto result = _servico.Salvar( Produto );

			result.Id.Should().BeGreaterThan( 0 );
			_repositorio.Verify( m => m.Salvar( Produto ) );
		}

		[Test]
		public void ProdutoServico_DeletarRepositorio_DeveFuncionar()
		{

			Produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

            Produto.Id = 1;

			_repositorio.Setup( m => m.Deletar(Produto) );

			_servico.Deletar(Produto);

			Produto result = _servico.PegarPorId(Produto.Id );

			result.Should().BeNull();
			_repositorio.Verify( m => m.Deletar(Produto) );
		}

		[Test]
		public void ProdutoServico_AtualizarRepositorio_DeveFuncionar()
		{
			Produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

			Produto.Id = 1;
            Produto.Nome = "teste";

			_repositorio.Setup( m => m.Atualizar( Produto ) ).Returns( Produto );

			Produto result = _servico.Atualizar( Produto );

			_repositorio.Verify( m => m.Atualizar( Produto ) );

			result.Nome.Should().Be("teste");
		}

		[Test]
		public void ProdutoServico_PegarPorId_Repositorio_DeveFuncionar()
		{
			Produto produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

			produto.Id = 1;

			_repositorio.Setup( m => m.PegarPorId( produto.Id ) ).Returns( produto );

			Produto result = _servico.PegarPorId( produto.Id );

			result.Should().NotBeNull();
			result.Id.Should().Be( produto.Id );
		}

		[Test]
		public void ProdutoServico_PegarTodos_Repositorio_DeveFuncionar()
		{
			Produto produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

			List<Produto> listaProdutos = new List<Produto>();

			listaProdutos.Add( produto );

			_repositorio.Setup( m => m.PegarTodos() ).Returns( listaProdutos );

			IList<Produto> result = _servico.PegarTodos().ToList();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterOrEqualTo( 1 );
		}

		[Test]
		public void ProdutoServico_DeletarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Produto produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

			produto.Id = 0;

			Action act = () => { _servico.Deletar( produto ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void ProdutoServico_AtualizarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

			Produto.Id = 0;

			Action act = () => { _servico.Atualizar( Produto ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void ProdutoServico_PegarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

			Produto.Id = 0;

			Action act = () => { _servico.PegarPorId( Produto.Id ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}
	}
}
