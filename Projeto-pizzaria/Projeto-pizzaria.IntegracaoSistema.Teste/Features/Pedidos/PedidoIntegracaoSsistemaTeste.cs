using FluentAssertions;
using NUnit.Framework;
using projeto_pizzaria.Servico.Features.Pedidos;
using Projeto_pizzaria.Commum.Base;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Pedidos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.IntegracaoSistema.Teste.Features.Pedidos
{
	[TestFixture]
	public class PedidoIntegracaoSistemaTeste
	{
		private PizzariaContext _contexto;
		private PedidoRepositorio _repositorio;
		private Pedido pedido;
		private PedidoServico _servico;
		private ItemPedido itemPedido;

		[SetUp]
		public void Setup()
		{
			pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			pedido.AdicionarItem( itemPedido );

			Database.SetInitializer( new BaseSqlTest() );
			_contexto = new PizzariaContext();
			_repositorio = new PedidoRepositorio( _contexto);
			_contexto.Database.Initialize( true );
			_servico = new PedidoServico( _repositorio );
		}

		[Test]
		public void Pedido_IntSistemas_Criar_DeveFuncionar()
		{
			pedido = _servico.Salvar( pedido );

			Pedido result = _servico.PegarPorId( pedido.Id );

			result.Id.Should().Be( pedido.Id );

			result.Should().NotBeNull();
		}

        [Test]
        public void Pedido_IntSistemas_Criar_pedido_pizza_dois_sabores_DeveFuncionar()
        {
            Pedido novoPedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();
            novoPedido.AdicionarItem(itemPedido);
            novoPedido = _servico.Salvar(novoPedido);

            Pedido result = _servico.PegarPorId(novoPedido.Id);

            result.Id.Should().Be(novoPedido.Id);
            result.Quantidade.Should().Be(1);
            result.Should().NotBeNull();
        }

        [Test]
        public void Pedido_IntSistemas_valor_pedido_pizza_dois_sabores_diferentes_DeveFuncionar()
        {
            Produto produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            Pedido novoPedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            ItemPedido novoitemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();
            novoPedido.AdicionarItem(novoitemPedido);
            novoPedido = _servico.Salvar(novoPedido);
            Pedido result = _servico.PegarPorId(novoPedido.Id);

            result.ValorTotal.Should().Be(produto.GetPreco());
            result.Id.Should().Be(novoPedido.Id);
            result.Should().NotBeNull();
        }

        [Test]
        public void Pedido_IntSistemas_valor_pedido_duas_pizzas_dois_sabores_diferentes_em_cada_DeveFuncionar()
        {
            Produto produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            Pedido novoPedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            ItemPedido novoitemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();
            novoPedido.AdicionarItem(novoitemPedido, novoitemPedido);
            novoPedido = _servico.Salvar(novoPedido);
            Pedido result = _servico.PegarPorId(novoPedido.Id);

            result.ValorTotal.Should().Be(produto.GetPreco() + produto.GetPreco());
            result.Id.Should().Be(novoPedido.Id);
            result.Should().NotBeNull();
        }

        [Test]
        public void Pedido_IntSistemas_valor_pedido_pizza_dois_sabores_com_borda_DeveFuncionar()
        {
            Produto produto = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            Adicional adicional = ObjectMother.adicionalValidoComCheddar();
            Pedido novoPedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            ItemPedido novoitemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();
            novoitemPedido.InsereAdicional(adicional);
            novoPedido.AdicionarItem(novoitemPedido);
            novoPedido = _servico.Salvar(novoPedido);

            Pedido result = _servico.PegarPorId(novoPedido.Id);

            result.ValorTotal.Should().Be(produto.GetPreco() + adicional.GetPreco(TamanhoEnum.Grande));
            result.Id.Should().Be(novoPedido.Id);
            result.Should().NotBeNull();
        }

        [Test]
		public void Pedido_IntSistemas_Atualizar_DeveFuncionar()
		{
			pedido.StatusPedido = StatusPedidoEnum.Entregue;

			Pedido result = _servico.Atualizar( pedido );

			result.StatusPedido.Should().Be( pedido.StatusPedido );
			result.Id.Should().Be( pedido.Id );
		}

		[Test]
		public void Pedido_IntSistemas_PegarPorId__DeveFuncionar()
		{
			Pedido result = _servico.PegarPorId( pedido.Id );

			result.Should().NotBeNull();
			result.Id.Should().Be( pedido.Id );
		}

		[Test]
		public void Pedido_IntSistemas_PegarTodos__DeveFuncionar()
		{
			IList<Pedido> result = _servico.PegarTodos().ToList();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterOrEqualTo( 1 );
		}

		[Test]
		public void Pedido_IntSistemas_Deletar_DeveFuncionar()
		{
			Pedido pedidoDelete = ObjectMother.ObterPedidoValidoPessoaFisica();
			pedidoDelete.AdicionarItem( itemPedido );

			pedidoDelete = _servico.Salvar( pedidoDelete );

			_servico.Deletar( pedidoDelete );

			Pedido pedidoEncontrado = _servico.PegarPorId( pedidoDelete.Id );
			pedidoEncontrado.Should().BeNull();
		}

		[Test]
		public void Pedido_IntSistemas_DeletarComIdZerado_NaoDeveFuncionar()
		{
			Pedido pedidoDelete = ObjectMother.ObterPedidoValidoPessoaFisica();
			pedidoDelete.AdicionarItem( itemPedido );
			pedidoDelete = _servico.Salvar( pedidoDelete );
			pedidoDelete.Id = 0;

			Action act = () => { _servico.Deletar( pedidoDelete ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void Pedido_IntSistemas_SalvarSemCliente_NaoDeveFuncionar()
		{
			Pedido pedidoSemCliente = ObjectMother.ObterPedidoSemCliente();

			Action act = () => { _servico.Salvar( pedidoSemCliente ); };

			act.Should().Throw<PedidoClienteVazioException>();
		}

		[Test]
		public void Pedido_IntSistemas_SalvarSemItem_NaoDeveFuncionar()
		{
			Pedido pedidoSemItem = ObjectMother.ObterPedidoValidoPessoaFisica();

			Action act = () => { _servico.Salvar( pedidoSemItem ); };

			act.Should().Throw<PedidoSemItemException>();
		}

		[Test]
		public void Pedido_IntSistemas_Salvar_com_nota_sem_cnpjCpf_NaoDeveFuncionar()
		{
			Pedido pedidoNota = ObjectMother.ObterPedidoValidoPessoaJuridicaComNota();
			pedidoNota.AdicionarItem( itemPedido );

			pedidoNota.Cliente.Cnpj = "";
			pedidoNota.Cliente.Cpf = "";

			Action act = () => { _servico.Salvar( pedidoNota ); };

			act.Should().Throw<PedidoCnpjECpfVazioException>();
		}

		[Test]
		public void Pedido_IntSistemas_Salvar_sem_departamento_NaoDeveFuncionar()
		{
			Pedido pedidoDepartamento = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaSemDepartamento();
			pedidoDepartamento.AdicionarItem( itemPedido );

			Action act = () => { _servico.Salvar( pedidoDepartamento ); };

			act.Should().Throw<PedidoPessoaJuridicaSemDepartamentoException>();
		}

		[Test]
		public void Pedido_IntSistemas_Salvar_sem_responsavel_NaoDeveFuncionar()
		{
			Pedido pedidoResponsavel = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaSemResponsavel();
			pedidoResponsavel.AdicionarItem( itemPedido );

			Action act = () => { _servico.Salvar( pedidoResponsavel ); };

			act.Should().Throw<PedidoPessoaJuridicaSemResponsavelException>();
		}

		[Test]
		public void Pedido_IntSistemas_AtualizarComIdZerado_NaoDeveFuncionar()
		{
			Pedido pedidoValido = ObjectMother.ObterPedidoValidoPessoaFisica();
			pedidoValido.AdicionarItem( itemPedido );

			pedidoValido.Id = 0;

			Action act = () => { _servico.Atualizar( pedidoValido ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}
		[Test]
		public void Pedido_IntSistemas_PegarComIdZerado__NaoDeveFuncionar()
		{
			Pedido pedidoValido = ObjectMother.ObterPedidoValidoPessoaFisica();

			pedidoValido.Id = 0;

			Action act = () => { _servico.PegarPorId( pedidoValido.Id ); };

			act.Should().Throw<IdentifierUndefinedException>();
		}
	}
}
