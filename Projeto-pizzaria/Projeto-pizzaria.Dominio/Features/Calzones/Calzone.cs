using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace Projeto_pizzaria.Dominio.Features.Calzones
{
    public class Calzone : Produto
    {
        public Calzone(string nome, double preco) : base(nome)
        {
            ValorProduto = preco;
            Tipo = TipoProdutoEnum.Calzone;
        }

		[ExcludeFromCodeCoverage]
		private Calzone() : base()
        {

        }
    }
}
