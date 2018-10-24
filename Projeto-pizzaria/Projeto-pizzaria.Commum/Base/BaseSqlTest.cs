using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Infra.Data.Context;
using System.Data.Entity;

namespace Projeto_pizzaria.Commum.Base
{
    public class BaseSqlTest : DropCreateDatabaseAlways<PizzariaContext>
    {
        protected override void Seed(PizzariaContext contexto)
        {
			Pedido Pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

            Cliente cliente = ObjectMother.ObtemClienteValidoFisico();
            contexto.Clientes.Add(cliente);

            ItemPedido itemPizzaMussarela = ObjectMother.ObterItemPedidoValidoPizzaMussarelaGrande();
			ItemPedido itemPizzaPortuguesa = ObjectMother.ObterItemPedidoValidoPizzaPortuguesaGrande();
            ItemPedido itemPizzaDoisSabores = ObjectMother.ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande();

            Adicional adicionalCatupiry = ObjectMother.adicionalValidoComCatupiry();
            contexto.Produtos.Add(adicionalCatupiry);

            Adicional adicionalCheddar = ObjectMother.adicionalValidoComCheddar();
            contexto.Produtos.Add(adicionalCheddar);

            Adicional adicionalDobroCatupiry = ObjectMother.AdicionalValidoDobroCatupiry();
            contexto.Produtos.Add(adicionalDobroCatupiry);

            var bebida = ObjectMother.ObterBebidaValida();
            bebida.Tamanho = Dominio.Features.Produtos.TamanhoEnum.Unico;
            contexto.Produtos.Add(bebida);

            var bebida_coca = ObjectMother.ObterBebidaValida();
            bebida_coca.Nome = "Coca";
            bebida_coca.ValorProduto = 5;
            bebida_coca.Tamanho = Dominio.Features.Produtos.TamanhoEnum.Unico;
            contexto.Produtos.Add(bebida_coca);

			Pedido.AdicionarItem( itemPizzaPortuguesa, itemPizzaDoisSabores );
            contexto.Pedidos.Add(Pedido);

            var pizza_bacon = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            pizza_bacon.Nome = "Bacon";
            pizza_bacon.Tamanho = Dominio.Features.Produtos.TamanhoEnum.Media;
            pizza_bacon.ValorProduto = 30;
            contexto.Produtos.Add(pizza_bacon);

            var pizza_brocoli = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            pizza_brocoli.Nome = "Brocoli";
            pizza_brocoli.Tamanho = Dominio.Features.Produtos.TamanhoEnum.Media;
            pizza_brocoli.ValorProduto = 30;
            contexto.Produtos.Add(pizza_brocoli);

            var pizza_calabresa = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            pizza_calabresa.Nome = "calabresa";
            pizza_calabresa.Tamanho = Dominio.Features.Produtos.TamanhoEnum.Pequena;
            pizza_calabresa.ValorProduto = 25;
            contexto.Produtos.Add(pizza_calabresa);

            var pizza_queijos = ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande();
            pizza_queijos.Nome = "4 queijos";
            pizza_queijos.Tamanho = Dominio.Features.Produtos.TamanhoEnum.Pequena;
            pizza_queijos.ValorProduto = 25;
            contexto.Produtos.Add(pizza_queijos);

            var calzone = ObjectMother.ObterCalzoneValido();
            calzone.Nome = "Calzone calabresa";
            calzone.Tamanho = Dominio.Features.Produtos.TamanhoEnum.Unico;
            calzone.ValorProduto = 30;
            contexto.Produtos.Add(calzone);
            
            contexto.SaveChanges();

            base.Seed(contexto);
        }
    }
}
