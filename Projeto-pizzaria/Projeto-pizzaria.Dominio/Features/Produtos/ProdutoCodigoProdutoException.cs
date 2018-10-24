using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Produtos
{
    [ExcludeFromCodeCoverage]
    public class ProdutoCodigoProdutoException : BusinessException
    {
        public ProdutoCodigoProdutoException() : base("O código do produto não deve ser menor que 0")
        {
        }
    }
}