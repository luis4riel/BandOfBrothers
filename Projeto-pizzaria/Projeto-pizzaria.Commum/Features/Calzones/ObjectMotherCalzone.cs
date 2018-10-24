using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Features.Calzones;

namespace Projeto_pizzaria.Commum.Features
{
	public static partial class ObjectMother
	{
		public static Calzone ObterCalzoneValido()
		{
			return new Calzone( "Cheedar com Carne", 9.0)
			{
				Id = 1
			};
		}

		public static Calzone ObterCalzonePrecoInvalido()
		{
			return new Calzone( "cheedar com camarão", -9.0 )
			{
				Id = 1
			};
		}
	}
}
