using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Features.Bebidas;

namespace Projeto_pizzaria.Commum.Features
{
	public static partial class  ObjectMother
	{
		public static Bebida ObterBebidaValida()
		{
			return new Bebida( "Smoothie 'sem noção'", 10.0 )
			{
				Id = 1
			};
		}

		public static Bebida ObterBebidaPrecoInvalido()
		{
			return new Bebida( "Coca-Cola", -11.0 )
			{
				Id = 1
			};
		}
	}
}
