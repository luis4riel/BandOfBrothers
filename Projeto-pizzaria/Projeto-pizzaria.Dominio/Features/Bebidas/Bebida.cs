using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace Projeto_pizzaria.Dominio.Features.Bebidas
{
	public class Bebida	: Produto
	{
		public Bebida( string nome, double preco ) : base( nome )
		{
			ValorProduto = preco;
            Tipo = TipoProdutoEnum.Bebida;
        }

        private Bebida() : base()
        {

        }

	}
}