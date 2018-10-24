using FluentAssertions;
using NUnit.Framework;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Dominio.Teste.Features.ItemPedidos
{
    [TestFixture]
    public class ItemPedidoDominioTestes
    {
        ItemPedido ItemPedido;

        [SetUp]
        public void PedidoDominioTeste()
        {
            ItemPedido = new ItemPedido();
        }

        [Test]
        public void Pedido_Dominio_Deve_Validar_umItem_com_Sucesso()
        {
            ItemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();

            Action acao = ItemPedido.Validar;

            acao.Should().NotThrow();

        }

        [Test]
        public void Pedido_Dominio_Deve_Validar_duasPizzas_com_Sucesso()
        {
            ItemPedido = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();

            Action acao = ItemPedido.Validar;

            acao.Should().NotThrow();
        }

        [Test]
        public void Pedido_Dominio_Deve_inserir_valor_parcial_uma_pizza_com_sucesso()
        {
            Pizza pizzaMussarela = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();

            ItemPedido Item = new ItemPedido(pizzaMussarela);

            Item.ValorParcial.Should().Be(pizzaMussarela.ValorProduto);
        }

        [Test]
        public void Pedido_Dominio_Deve_inserir_valor_parcial_maior_pizza_com_sucesso()
        {
            Pizza pizzaMussarela = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            Pizza pizzaPortuguesa = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();

            ItemPedido Item = new ItemPedido(pizzaMussarela, pizzaPortuguesa);

            Item.ValorParcial.Should().Be(pizzaMussarela.ValorProduto);
        }

        [Test]
        public void Pedido_Dominio_Deve_inserir_valor_parcial_maior_pizza_e_somar_borda_com_sucesso()
        {
            Pizza pizzaMussarela = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            Pizza pizzaPortuguesa = ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande();
            Adicional adicional = new Adicional("Cheddar");

            ItemPedido Item = new ItemPedido(pizzaMussarela, pizzaPortuguesa);
            Item.InsereAdicional(adicional);

            Item.ValorParcial.Should().Be(pizzaMussarela.ValorProduto + adicional.GetPreco(TamanhoEnum.Grande));
        }
    }
}
