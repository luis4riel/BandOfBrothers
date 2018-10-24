using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using projeto_pizzaria.Servico.Features.Pedidos;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Features.Pedidos;

namespace Projeto_pizzaria.Aplicacao.Teste.Features.Pedidos
{
	[TestFixture]
	public class PedidoServicoTeste
	{
		PedidoServico _servico;
		Pedido Pedido;

		Mock<IPedidoRepositorio> _repositorio;

		[SetUp]
		public void InitializeObjects()
		{
			Pedido = new Pedido();
			_repositorio = new Mock<IPedidoRepositorio>();
			_servico = new PedidoServico( _repositorio.Object );
		}

		[Test]
		public void PedidoServico_CriarRepositorio_DeveFuncionar()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			_repositorio.Setup( m => m.Salvar( Pedido ) ).Returns( new Pedido { Id = 1 } );

			Pedido result = _servico.Salvar( Pedido );

			result.Id.Should().BeGreaterThan( 0 );
			_repositorio.Verify( m => m.Salvar( Pedido ) );
		}

		[Test]
		public void PedidoServico_DeletarRepositorio_DeveFuncionar()
		{

			Pedido pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

			pedido.Id = 1;

			_repositorio.Setup( m => m.Deletar( pedido ) );

			_servico.Deletar( pedido );

			Pedido result = _servico.PegarPorId( pedido.Id );

			result.Should().BeNull();
			_repositorio.Verify( m => m.Deletar( pedido ) );
		}

		[Test]
		public void PedidoServico_AtualizarRepositorio_DeveFuncionar()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Pedido.Id = 1;
			Pedido.Endereco.Estado = "Goias";

			_repositorio.Setup( m => m.Atualizar( Pedido ) ).Returns( Pedido );

			Pedido result = _servico.Atualizar( Pedido );

			_repositorio.Verify( m => m.Atualizar( Pedido ) );

			result.Endereco.Estado.Should().Be( "Goias" );
		}

		[Test]
		public void PedidoServico_PegarPorId_Repositorio_DeveFuncionar()
		{
			Pedido pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

			pedido.Id = 1;

			_repositorio.Setup( m => m.PegarPorId( pedido.Id ) ).Returns( pedido );

			Pedido result = _servico.PegarPorId( pedido.Id );

			result.Should().NotBeNull();
			result.Id.Should().Be( pedido.Id );
		}

		[Test]
		public void PedidoServico_PegarTodos_Repositorio_DeveFuncionar()
		{
			Pedido pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

			List<Pedido> listaPedidos = new List<Pedido>();

			listaPedidos.Add( pedido );

			_repositorio.Setup( m => m.PegarTodos() ).Returns( listaPedidos );

			IList<Pedido> result = _servico.PegarTodos().ToList();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterOrEqualTo( 1 );
		}

		[Test]
		public void PedidoServico_SalvarPedidoSemCliente_Repositorio_NaoDeveFuncionar()
		{
			Pedido pedido = ObjectMother.ObterPedidoSemCliente();

			Action act = () => { _servico.Salvar( pedido ); };

			act.Should().Throw<PedidoClienteVazioException>();
		}

		[Test]
		public void Pedido_servico_SalvarSemItem_NaoDeveFuncionar()
		{
			Pedido pedidoSemItem = ObjectMother.ObterPedidoValidoPessoaFisica();

			Action act = () => { _servico.Salvar( pedidoSemItem ); };

			act.Should().Throw<PedidoSemItemException>();
		}

		[Test]
		public void Pedido_servico_Salvar_com_nota_sem_cnpjCpf_NaoDeveFuncionar()
		{
			Pedido pedidoNota = ObjectMother.ObterPedidoValidoPessoaJuridicaComNota();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			pedidoNota.AdicionarItem( itemPedido );

			pedidoNota.Cliente.Cnpj = "";
			pedidoNota.Cliente.Cpf = "";

			Action act = () => { _servico.Salvar( pedidoNota ); };

			act.Should().Throw<PedidoCnpjECpfVazioException>();
		}

		[Test]
		public void Pedido_servico_Salvar_sem_departamento_NaoDeveFuncionar()
		{
			Pedido pedidoDepartamento = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaSemDepartamento();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			pedidoDepartamento.AdicionarItem( itemPedido );

			Action act = () => { _servico.Salvar( pedidoDepartamento ); };

			act.Should().Throw<PedidoPessoaJuridicaSemDepartamentoException>();
		}

		[Test]
		public void Pedido_servico_Salvar_sem_responsavel_NaoDeveFuncionar()
		{
			Pedido pedidoResponsavel = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaSemResponsavel();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			pedidoResponsavel.AdicionarItem( itemPedido );

			Action act = () => { _servico.Salvar( pedidoResponsavel ); };

			act.Should().Throw<PedidoPessoaJuridicaSemResponsavelException>();
		}

		[Test]
		public void PedidoServico_DeletarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Pedido pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

			pedido.Id = 0;

			Action act = () => { _servico.Deletar( pedido ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void PedidoServico_AtualizarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            ItemPedido itemPizzaDoisSabores = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();

            Pedido.AdicionarItem(itemPizzaDoisSabores);
            Pedido.Id = 0;

			Action act = () => { _servico.Atualizar( Pedido ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}


		[Test]
		public void PedidoServico_PegarComIdZerado_Repositorio_NaoDeveFuncionar()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPizzaDoisSabores = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();

			Pedido.AdicionarItem(itemPizzaDoisSabores);
			Pedido.Id = 0;

			Action act = () => { _servico.PegarPorId( Pedido.Id ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

	}
}