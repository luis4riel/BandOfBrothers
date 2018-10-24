using Projeto_pizzaria.Dominio.Base;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Produtos
{
	public abstract class Produto : Entidade
	{
		public double ValorProduto { get; set; }
		public string Nome { get; set; } 
		public TamanhoEnum Tamanho { get; set; }
		public TipoProdutoEnum Tipo { get; set; }

		public Produto( string nome )
		{
			Nome = nome;
		}

		protected Produto()	{ }

		public double GetPreco() { return ValorProduto; }
		public string GetNome() { return Nome; }

		public override void Validar()
		{
			if (string.IsNullOrEmpty( Nome ))
				throw new ProdutoEmptyDescricaoException();
			if (ValorProduto <= 0)
				throw new ProdutoValorUnitarioException();
		}

		[ExcludeFromCodeCoverage]
		public static string VerificarTipo( TipoProdutoEnum tipo )
		{
			switch (tipo)
			{
				case TipoProdutoEnum.Adicional:
					return "Adicional";
				case TipoProdutoEnum.Bebida:
					return "Bebida";
				case TipoProdutoEnum.Calzone:
					return "Calzone";
				case TipoProdutoEnum.Pizza:
					return "Pizza";
				default:
					return "Não identificado";
			}
		}

		[ExcludeFromCodeCoverage]
		public override string ToString()
		{
			return "Produto: " + Nome + " - Preço: " + ValorProduto;
		}
	}
}
