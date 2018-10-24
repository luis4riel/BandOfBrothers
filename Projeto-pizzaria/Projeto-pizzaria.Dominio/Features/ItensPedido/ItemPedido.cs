using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Projeto_pizzaria.Dominio.Base;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace Projeto_pizzaria.Dominio.Features.ItensPedido
{
	public class ItemPedido : Entidade
	{
		public virtual Produto Produto { get; set; }
		public TamanhoEnum Tamanho { get; set; }
		public string Nome { get; set; }

        public double ValorParcial { get; set; }

        public ItemPedido() { }

		public ItemPedido( Produto produto )
		{
			Produto = produto;
			Nome = produto.GetNome();
			Tamanho = produto.Tamanho;
            ValorParcial = produto.GetPreco();
        }

		public ItemPedido( Pizza pizza1, Pizza pizza2 )
		{
			double valorMaiorPizza = MaiorValor( pizza1.GetPreco(), pizza2.GetPreco() );

			if (valorMaiorPizza == pizza1.GetPreco())
			{
				Produto = pizza1;
				Tamanho = pizza1.Tamanho;
				Nome = pizza1.GetNome() + " + " + pizza2.GetNome();
                ValorParcial = Produto.GetPreco();
            }
            else
			{
				Produto = pizza2;
				Tamanho = pizza2.Tamanho;
				Nome = pizza2.GetNome() + " + " + pizza1.GetNome();
                ValorParcial = Produto.GetPreco();
            }
        }

        public void InsereAdicional(Adicional adicional)
        {
            Nome = Nome + " com borda de " + adicional.Nome;
            ValorParcial += adicional.GetPreco(Produto.Tamanho);
        }

		public double MaiorValor( double valor1, double valor2 )
		{
			if (valor1 > valor2)
				return valor1;
			else
				return valor2;
		}

		public override void Validar()
		{
			Produto.Validar();
		}

		[ExcludeFromCodeCoverage]
		public override string ToString()
		{
			return Produto.VerificarTipo(Produto.Tipo) + ": " + Nome + " - Preço: " + ValorParcial;
		}
	}
}