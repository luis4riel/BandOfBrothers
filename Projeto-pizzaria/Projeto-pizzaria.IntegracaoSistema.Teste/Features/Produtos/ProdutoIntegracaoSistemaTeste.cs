using FluentAssertions;
using NUnit.Framework;
using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Commum.Base;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Projeto_pizzaria.IntegracaoSistema.Teste.Features.Produtos
{
	[TestFixture]
	public class ProdutoIntegracaoSistemaTeste
	{
		private PizzariaContext _contexto;
		private ProdutoRepositorio _repositorio;
		private Produto _produto;
		private ProdutoServico _servico;	 

		[SetUp]
		public void Setup()
		{
			_produto = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();
			Database.SetInitializer( new BaseSqlTest() );
			_contexto = new PizzariaContext();
			_repositorio = new ProdutoRepositorio(_contexto);
			_contexto.Database.Initialize( true );
			_servico = new ProdutoServico( _repositorio );
		}

		[Test]
		public void Produto_IntSistemas_Criar_DeveFuncionar()
		{
			Produto produto = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();

			produto = _servico.Salvar( produto );

			Produto result = _servico.PegarPorId( produto.Id );

			result.Id.Should().Be( produto.Id );

			result.Should().NotBeNull();
		}

        [Test]
		public void Produto_IntSistemas_Atualizar_DeveFuncionar()
		{
			_produto.Nome = "Peperoni";

			Produto result = _servico.Atualizar( _produto );

			result.Nome.Should().Be( _produto.Nome );
		}

		[Test]
		public void Produto_IntSistemas_PegarPorId__DeveFuncionar()
		{
			Produto result = _servico.PegarPorId( _produto.Id );

			result.Should().NotBeNull();
			result.Id.Should().Be( _produto.Id );
		}

		[Test]
		public void Produto_IntSistemas_PegarTodos__DeveFuncionar()
		{
			IList<Produto> result = _servico.PegarTodos().ToList();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterOrEqualTo( 1 );
		}

		[Test]
		public void Produto_IntSistemas_Deletar_DeveFuncionar()
		{
			_produto = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();

			var produtoObtido = _servico.Salvar( _produto );

			Produto produto = _servico.PegarPorId( produtoObtido.Id );

			_servico.Deletar( produto );

			Produto produtoEncontrado = _servico.PegarPorId( produto.Id );
			produtoEncontrado.Should().BeNull();
		}

		[Test]
		public void Produto_IntSistemas_DeletarComIdZerado__NaoDeveFuncionar()
		{
			Produto produto = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();

			produto.Id = 0;

			Action act = () => { _servico.Deletar( produto ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void Produto_IntSistemas_AtualizarComIdZerado__NaoDeveFuncionar()
		{
			Produto produto = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();

			produto.Id = 0;

			Action act = () => { _servico.Atualizar( produto ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void Produto_IntSistemas_PegarComIdZerado__NaoDeveFuncionar()
		{
			Produto produto = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();

			produto.Id = 0;

			Action act = () => { _servico.PegarPorId( produto.Id ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}
	}
}
