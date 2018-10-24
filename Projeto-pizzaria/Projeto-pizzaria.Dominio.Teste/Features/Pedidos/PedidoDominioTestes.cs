using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Features.Cnpj;
using Projeto_pizzaria.Infra.Features.Cpf;

namespace Projeto_pizzaria.Dominio.Teste.Features.Pedidos
{
	[TestFixture]
	public class PedidoDominioTestes
	{
		Pedido Pedido;

		[SetUp]
		public void PedidoDominioTeste()
		{
			Pedido = new Pedido();
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_com_Sucesso()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Action acao = Pedido.Validar;

			acao.Should().NotThrow();
		}

		[Test]
		public void Pedido_Dominio_Deve_calcular_valor_pedido_com_1_pizza_grande_com_1_unico_sabor()
		{
			//Cenário
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();

			//ação
			Pedido.AdicionarItem( itemPedido );

			//verificação
			Pedido.ItensPedido.Should().HaveCount( 1 );
			Pedido.ValorTotal.Should().Be( 40 );
		}

		[Test]
		public void Deve_obter_valor_pedido_com_1_pizza_grande_com_2_sabores_de_valores_diferentes()
		{
			//Cenário
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPizzaDoisSabores = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();

			//ação
			Pedido.AdicionarItem( itemPizzaDoisSabores );

			//verificação
			Pedido.ValorTotal.Should().Be( 40 );
			Pedido.Quantidade.Should().Be( 1 );
		}

		[Test]
		public void Deve_obter_valor_pedido_com_2_pizzas_grande_com_1_sabor_cada()
		{
			//Cenário
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPizzaMussarela = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			ItemPedido itemPizzaPortuguesa = ObjectMother.ObterItemPedidoValidoPizzaPortuguesaGrande();

			//ação
			Pedido.AdicionarItem( itemPizzaMussarela, itemPizzaPortuguesa );

			//verificação
			Pedido.ValorTotal.Should().Be( 78 );
			Pedido.Quantidade.Should().Be( 2 );
		}

		[Test]
		public void Deve_obter_valor_pedido_com_2_pizzas_grandes_com_2_sabores_cada()
		{
			//Cenário
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            ItemPedido itemPizzaDoisSabores = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();

            //ação
            Pedido.AdicionarItem(itemPizzaDoisSabores, itemPizzaDoisSabores);

			//verificação
			Pedido.ValorTotal.Should().Be( 80 );
			Pedido.Quantidade.Should().Be( 2 );
		}

		[Test]
		public void Deve_obter_valor_pedido_com_2_pizzas_grandes_com_2_sabores_em_uma_e_um_sabor_em_outra()
		{
			//Cenário
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            ItemPedido itemPizzaDoisSabores = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();
            ItemPedido itemPizzaMussarela = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();

            //ação
            Pedido.AdicionarItem(itemPizzaDoisSabores, itemPizzaMussarela);

			//verificação
			Pedido.ValorTotal.Should().Be( 80 );
			Pedido.Quantidade.Should().Be( 2 );
		}

		[Test]
		public void Deve_obter_valor_pedido_com_1_pizza_pequena_com_1_unico_sabor_com_borda_com_sucesso()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
			ItemPedido itemPizzaMussarela = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCatupiry();

            itemPizzaMussarela.InsereAdicional(bordaCatupiry);

			//ação
			Pedido.AdicionarItem( itemPizzaMussarela );

			//verificação
			Pedido.ValorTotal.Should().Be( 42.5 );
			Pedido.Quantidade.Should().Be( 1 );
		}

		[Test]
		public void Deve_verificar_quantidade_de_itens_no_pedido_com_pizza_dois_sabores()
		{
			//bool doisSabores = true;
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();
            ItemPedido itemPizzaDoisSabores = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();
            ItemPedido itemBebida = ObjectMother.ObterItemPedidoValidoBebida();

			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCatupiry();
			var novoItem = new ItemPedido( bordaCatupiry );

            //ação

            Pedido.AdicionarItem(itemPizzaDoisSabores);
            Pedido.AdicionarItem( novoItem, itemBebida );

			//verificação
			Pedido.ItensPedido.Should().HaveCount( 3 );
			Pedido.Quantidade.Should().Be( 3 );
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_pedido_sem_cliente_com_Sucesso()
		{
			Pedido = ObjectMother.ObterPedidoSemCliente();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Action acao = Pedido.Validar;

			acao.Should().Throw<PedidoClienteVazioException>();
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_pedido_emitirnota_com_cnpj_invalido()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaECnpjInvalido();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Action acao = Pedido.Validar;

			acao.Should().Throw<CnpjInvalidoException>();
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_pedido_emitirnota_com_cpf_invalido()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaECpfInvalido();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Action acao = Pedido.Validar;

			acao.Should().Throw<CpfInvalidoException>();
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_pedido_emitirnota_com_cpf_e_cnpj_invalido()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaECpfeCnpjInvalido();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Action acao = Pedido.Validar;

			acao.Should().Throw<PedidoCnpjECpfVazioException>();
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_pedido_emitirnota_com_departamento_invalido()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaSemDepartamento();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Action acao = Pedido.Validar;

			acao.Should().Throw<PedidoPessoaJuridicaSemDepartamentoException>();
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_pedido_emitirnota_com_responsavel_invalido()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaJuridicaComNotaSemResponsavel();
			ItemPedido itemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			Pedido.AdicionarItem( itemPedido );

			Action acao = Pedido.Validar;

			acao.Should().Throw<PedidoPessoaJuridicaSemResponsavelException>();
		}

		[Test]
		public void Pedido_Dominio_Deve_Validar_pedido_sem_itens()
		{
			Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

			Action acao = Pedido.Validar;

			acao.Should().Throw<PedidoSemItemException>();
		}
	}
}