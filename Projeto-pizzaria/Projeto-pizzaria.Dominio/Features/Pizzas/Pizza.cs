using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace Projeto_pizzaria.Dominio.Features.Pizzas
{
	public class Pizza : Produto
	{

		public Pizza( string nome, double preco ) : base( nome )
		{
			ValorProduto = preco;
            Tipo = TipoProdutoEnum.Pizza;
		}

		[ExcludeFromCodeCoverage]
        public Pizza() : base()
        {

        }
	}
}
