using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace Projeto_pizzaria.Commum.Features
{
	public static partial class ObjectMother
	{
		public static  Adicional adicionalValidoComCheddar()
		{
            return new Adicional("cheddar")
            {
                Id = 1,
                Nome = "Cheddar",
                Tamanho = TamanhoEnum.Pequena,
                ValorProduto = 5
            };
		}

		public static Adicional adicionalValidoComCatupiry()
		{
            return new Adicional("catupiry")
            {
                Id = 1,
                Nome = "Catupiry",
                Tamanho = TamanhoEnum.Pequena,
                ValorProduto = 5
            };
		}
        public static Adicional AdicionalValidoDobroCatupiry()
        {
            return new Adicional("Dobro Catupiry")
            {
                Id = 1,
                Nome = " Dobro Catupiry",
                Tamanho = TamanhoEnum.Pequena,
                ValorProduto = 5
            };
        }
    }
}
