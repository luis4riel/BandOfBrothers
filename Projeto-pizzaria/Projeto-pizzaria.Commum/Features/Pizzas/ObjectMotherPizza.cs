using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace Projeto_pizzaria.Commum.Features
{
	public static partial class ObjectMother
	{
		public static Pizza ObterPizzaValidaMussarelaTamanhoGrande()
		{
			return new Pizza( "Mussarela", 40.0 )
			{
				Id = 1,
				Tamanho = TamanhoEnum.Grande
			};
		}

		public static Pizza ObterPizzaValidaPortuguesaTamanhoGrande()
		{
			return new Pizza( "Portuguesa", 38.0 )
			{
				Id = 2,
				Tamanho = TamanhoEnum.Grande
			};
		}

		public static Pizza ObterPizzaPrecoInvalido()
		{
			return new Pizza( "Portuguesa", -38.0 )
			{
				Id = 1,
				Tamanho = TamanhoEnum.Grande
			};
		}
	}
}
