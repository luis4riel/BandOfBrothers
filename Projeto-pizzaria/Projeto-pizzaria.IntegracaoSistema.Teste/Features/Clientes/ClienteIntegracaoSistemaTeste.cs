using FluentAssertions;
using NUnit.Framework;
using projeto_pizzaria.Servico.Features.Clientes;
using Projeto_pizzaria.Commum.Base;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Projeto_pizzaria.IntegracaoSistema.Teste.Features.Clientes
{
	[TestFixture]
	public class ClienteIntegracaoSistemaTeste
	{
		private PizzariaContext _contexto;
		private ClienteRepositorio _repositorio;
		private Cliente _cliente;
		private ClienteServico _servico;	 

		[SetUp]
		public void Setup()
		{
			_cliente = ObjectMother.ObtemClienteValidoFisico();
			Database.SetInitializer( new BaseSqlTest() );
			_contexto = new PizzariaContext();
			_repositorio = new ClienteRepositorio(_contexto);
			_contexto.Database.Initialize( true );
			_servico = new ClienteServico( _repositorio );
		}

		[Test]
		public void Cliente_IntSistemas_Criar_DeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteValidoFisico();

			cliente = _servico.Salvar( cliente );

			Cliente result = _servico.PegarPorId( cliente.Id );

			result.Id.Should().Be( cliente.Id );

			result.Should().NotBeNull();
		}

		[Test]
		public void Cliente_IntSistemas_Atualizar_DeveFuncionar()
		{
			_cliente.Nome = "Zé da silva";

			Cliente result = _servico.Atualizar( _cliente );

			result.Nome.Should().Be( _cliente.Nome );
		}

		[Test]
		public void Cliente_IntSistemas_PegarPorId__DeveFuncionar()
		{
			Cliente result = _servico.PegarPorId( _cliente.Id );

			result.Should().NotBeNull();
			result.Id.Should().Be( _cliente.Id );
		}

		[Test]
		public void Cliente_IntSistemas_PegarTodos__DeveFuncionar()
		{
			IList<Cliente> result = _servico.PegarTodos().ToList();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterOrEqualTo( 1 );
		}

		[Test]
		public void Cliente_IntSistemas_Deletar_DeveFuncionar()
		{
			_cliente = ObjectMother.ObtemClienteValidoJuridico();

			var clienteObtido = _servico.Salvar( _cliente );

			Cliente cliente = _servico.PegarPorId( clienteObtido.Id );

			_servico.Deletar( cliente );

			Cliente clienteEncontrado = _servico.PegarPorId( cliente.Id );
			clienteEncontrado.Should().BeNull();
		}

		[Test]
		public void Cliente_IntSistemas_SalvarSemNome__NaoDeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteNomeVazio();

			Action act = () => { _servico.Salvar( cliente ); };

			act.Should().Throw<ClienteComNomeVazioException>();
		}

		[Test]
		public void Cliente_IntSistemas_DeletarComIdZerado__NaoDeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteValidoJuridico();

			cliente.Id = 0;

			Action act = () => { _servico.Deletar( cliente ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void Cliente_IntSistemas_AtualizarComIdZerado__NaoDeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteValidoFisico();

			cliente.Id = 0;

			Action act = () => { _servico.Atualizar( cliente ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void Cliente_IntSistemas_PegarComIdZerado__NaoDeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteValidoJuridico();

			cliente.Id = 0;

			Action act = () => { _servico.PegarPorId( cliente.Id ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}
	}
}
