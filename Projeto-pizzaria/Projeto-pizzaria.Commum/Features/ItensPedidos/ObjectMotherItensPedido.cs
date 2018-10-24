using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pizzas;

namespace Projeto_pizzaria.Commum.Features
{
	public static partial class ObjectMother
	{
		public static ItemPedido ObterItemPedidoValidoPizzaMussarelaGrande()
		{
			return new ItemPedido(ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande())
			{
				Id = 1				
			};
		}



		public static ItemPedido ObterItemPedidoValidoBebida()
		{
			return new ItemPedido( ObjectMother.ObterBebidaValida() )
			{
				Id = 2
			};
		}

        public static ItemPedido ObterItemPedidoValidoPizzaPortuguesaGrande()
        {
            return new ItemPedido(ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande(), ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande())
            {
                Id = 1
            };
        }

        public static ItemPedido ObterItemPedidoValidoPizzaMussarelaMaisPortuguesaGrande()
        {
            return new ItemPedido(ObjectMother.ObterPizzaValidaPortuguesaTamanhoGrande(), ObjectMother.ObterPizzaValidaMussarelaTamanhoGrande())
            {
                Id = 1
            };
        }

        public static List<ItemPedido> ObterListaItemPedidoValidoPizzas()
		{
			List<ItemPedido> listaItens = new List<ItemPedido>();

			listaItens.Add(ObterItemPedidoValidoPizzaPortuguesaGrande());
			listaItens.Add(ObterItemPedidoValidoPizzaMussarelaGrande());

			return listaItens;
		}
	}
}
