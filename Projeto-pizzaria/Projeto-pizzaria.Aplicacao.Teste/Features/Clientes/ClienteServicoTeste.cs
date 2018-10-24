using FluentAssertions;
using Moq;
using NUnit.Framework;
using projeto_pizzaria.Servico.Features.Clientes;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_pizzaria.Aplicacao.Teste.Features.Clientes
{
	[TestFixture]
	public class ClienteServicoTeste
	{
		ClienteServico _servico;
		Cliente Cliente;

		Mock<IClienteRepositorio> _repositorio;

		[SetUp]
		public void InitializeObjects()
		{
			Cliente = new Cliente();
			_repositorio = new Mock<IClienteRepositorio>();
			_servico = new ClienteServico( _repositorio.Object );
		}

		[Test]
		public void ClienteServico_CriarRepositorio_DeveFuncionar()
		{
			Cliente = ObjectMother.ObtemClienteValidoFisico();

			_repositorio.Setup( m => m.Salvar( Cliente ) ).Returns( new Cliente { Id = 1 } );

			Cliente result = _servico.Salvar( Cliente );

			result.Id.Should().BeGreaterThan( 0 );
			_repositorio.Verify( m => m.Salvar( Cliente ) );
		}

		[Test]
		public void ClienteServico_DeletarRepositorio_DeveFuncionar()
		{

			Cliente cliente = ObjectMother.ObtemClienteValidoJuridico();

			cliente.Id = 1;

			_repositorio.Setup( m => m.Deletar( cliente ) );

			_servico.Deletar( cliente );

			Cliente result = _servico.PegarPorId( cliente.Id );

			result.Should().BeNull();
			_repositorio.Verify( m => m.Deletar( cliente ) );
		}

		[Test]
		public void ClienteServico_AtualizarRepositorio_DeveFuncionar()
		{
			Cliente = ObjectMother.ObtemClienteValidoFisico();

			Cliente.Id = 1;
			Cliente.Endereco.Estado = "Goias";

			_repositorio.Setup( m => m.Atualizar( Cliente ) ).Returns( Cliente );

			Cliente result = _servico.Atualizar( Cliente );

			_repositorio.Verify( m => m.Atualizar( Cliente ) );

			result.Endereco.Estado.Should().Be( "Goias" );
		}

		[Test]
		public void ClienteServico_PegarPorId_Repositorio_DeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteValidoFisico();

			cliente.Id = 1;

			_repositorio.Setup( m => m.PegarPorId( cliente.Id ) ).Returns( cliente );

			Cliente result = _servico.PegarPorId( cliente.Id );

			result.Should().NotBeNull();
			result.Id.Should().Be( cliente.Id );
		}

		[Test]
		public void ClienteServico_PegarTodos_Repositorio_DeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteValidoJuridico();

			List<Cliente> listaClientes = new List<Cliente>();

			listaClientes.Add( cliente );

			_repositorio.Setup( m => m.PegarTodos() ).Returns( listaClientes );

			IList<Cliente> result = _servico.PegarTodos().ToList();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterOrEqualTo( 1 );
		}

		[Test]
		public void ClienteServico_DeletarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Cliente cliente = ObjectMother.ObtemClienteValidoJuridico();

			cliente.Id = 0;

			Action act = () => { _servico.Deletar( cliente ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void ClienteServico_AtualizarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Cliente = ObjectMother.ObtemClienteValidoFisico();

			Cliente.Id = 0;

			Action act = () => { _servico.Atualizar( Cliente ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void ClienteServico_PegarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Cliente = ObjectMother.ObtemClienteValidoFisico();

			Cliente.Id = 0;

			Action act = () => { _servico.PegarPorId( Cliente.Id ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}
	}
}
