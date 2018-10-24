using Projeto_pizzaria.Dominio.Base;
using Projeto_pizzaria.Dominio.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Dominio.Features.Adicionais
{
    public class Adicional : Produto
    {

        public Adicional( string nome ) : base( nome )
		{
            Nome = nome;
            Tipo = TipoProdutoEnum.Adicional;
        }

        private Adicional() : base()
        {

        }

		public double GetPreco( TamanhoEnum tamanhoPizza )
		{
			if (Nome.ToUpper().Contains( "CATUPIRY" ))
			{
				if (tamanhoPizza == TamanhoEnum.Grande)
					return 2.50;
				else if (tamanhoPizza == TamanhoEnum.Media)
					return 1.75;
				else if (tamanhoPizza == TamanhoEnum.Pequena)
					return 1.25;
			}
			if (Nome.ToUpper().Contains( "CHEDDAR" ))
			{
				if (tamanhoPizza == TamanhoEnum.Grande)
					return 2.00;
				else if (tamanhoPizza == TamanhoEnum.Media)
					return 1.50;
				else if (tamanhoPizza == TamanhoEnum.Pequena)
					return 1.00;
			}

			return 0;
		}
    }
}
