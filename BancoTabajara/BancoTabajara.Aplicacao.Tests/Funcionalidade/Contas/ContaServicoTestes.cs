using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoTabajara.Aplicacao.Tests.Funcionalidade.Contas
{
	[TestFixture]
	public class ContasServicoTestes
	{
		IContaServico _servico;
		Mock<IContaRepositorio> _repositorio;
		Mock<IClienteRepositorio> _repositorioCliente;

		[SetUp]
		public void SetUp()
		{
			_repositorio = new Mock<IContaRepositorio>();
			_repositorioCliente = new Mock<IClienteRepositorio>();
			_servico = new ContaServico( _repositorio.Object, _repositorioCliente.Object );
		}

		#region inserir
		[Test]
		public void Conta_servico_inserir_deve_funcionar()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.Inserir( It.IsAny<Conta>() ) ).Returns( conta );

			var novaContaId = _servico.Inserir( conta );

			_repositorio.Verify( pr => pr.Inserir( It.IsAny<Conta>() ), Times.Once );
			novaContaId.Should().Be( conta.Id );
		}
		#endregion

		#region pegar
		[Test]
		public void Conta_servico_pegarTodos_deve_trazer_todos()
		{
			var limite = 1;
			var conta = ObjectMother.ObtemContaValida();
			var repositoryMockValue = new List<Conta>() { conta }.AsQueryable();
			_repositorio.Setup( pr => pr.PegarTodos( limite ) ).Returns( repositoryMockValue );

			var listaContas = _servico.PegarTodos( limite );

			_repositorio.Verify( pr => pr.PegarTodos( limite ), Times.Once );
			listaContas.Should().NotBeNull();
			listaContas.Count().Should().Be( repositoryMockValue.Count() );

			listaContas.First().Should().Be( repositoryMockValue.First() );
		}

		[Test]
		public void Conta_servico_pegarPorId_deve_trazer_conta()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.PegarPorId( conta.Id ) ).Returns( conta );

			var contaSelecionada = _servico.PegarPorId( conta.Id );

			_repositorio.Verify( pr => pr.PegarPorId( conta.Id ), Times.Once );
			contaSelecionada.Should().NotBeNull();
			contaSelecionada.Id.Should().Be( conta.Id );
		}

		[Test]
		public void Conta_servico_pegarPorId_deve_falhar_por_id_nao_encontrado()
		{
			var conta = ObjectMother.ObtemContaValida();
			var exception = new NaoEncontradoException();
			_repositorio.Setup( pr => pr.PegarPorId( conta.Id ) ).Throws( exception );

			Action action = () => _servico.PegarPorId( conta.Id );

			action.Should().Throw<NaoEncontradoException>();

			_repositorio.Verify( pr => pr.PegarPorId( conta.Id ), Times.Once );
		}
		#endregion

		#region deletar
		[Test]
		public void Conta_servico_deletar_deve_funcionar()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.Deletar( conta.Id ) ).Returns( true );

			var contaRemovida = _servico.Deletar( conta );

			_repositorio.Verify( pr => pr.Deletar( conta.Id ), Times.Once );
			contaRemovida.Should().BeTrue();
		}

		[Test]
		public void Conta_servico_deletar_id_nao_encontrado_deve_falhar()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.Deletar( conta.Id ) ).Throws<NaoEncontradoException>();

			Action action = () => _servico.Deletar( conta );

			action.Should().Throw<NaoEncontradoException>();

			_repositorio.Verify( pr => pr.Deletar( conta.Id ), Times.Once );
		}
		#endregion

		#region atualizar
		[Test]
		public void Conta_servico_atualizar_deve_funcionar()
		{
			var conta = ObjectMother.ObtemContaValida();
			conta.Estado = true;
			_repositorio.Setup( pr => pr.PegarPorId( conta.Id ) ).Returns( conta );
			_repositorio.Setup( pr => pr.Atualizar( conta ) ).Returns( true );

			var contaAtualizada = _servico.Atualizar( conta );

			_repositorio.Verify( pr => pr.PegarPorId( conta.Id ), Times.Once );
			_repositorio.Verify( pr => pr.Atualizar( conta ), Times.Once );
			contaAtualizada.Should().BeTrue();
		}

		[Test]
		public void Conta_servico_atualizar_deve_falhar_por_id_nao_encontrado()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.PegarPorId( conta.Id ) ).Returns( (Conta) null );

			Action action = () => _servico.Atualizar( conta );

			action.Should().Throw<NaoEncontradoException>();

			_repositorio.Verify( pr => pr.PegarPorId( conta.Id ), Times.Once );
			_repositorio.Verify( pr => pr.Atualizar( It.IsAny<Conta>() ), Times.Never );
		}
		#endregion
	}
}