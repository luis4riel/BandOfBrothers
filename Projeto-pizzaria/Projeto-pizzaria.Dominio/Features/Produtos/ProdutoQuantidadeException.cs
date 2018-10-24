using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Produtos
{
    [ExcludeFromCodeCoverage]
    public class ProdutoQuantidadeException : BusinessException
    {
        public ProdutoQuantidadeException() : base("Quantidade do produto não pode ser menor que 0")
        {
        }
    }
}