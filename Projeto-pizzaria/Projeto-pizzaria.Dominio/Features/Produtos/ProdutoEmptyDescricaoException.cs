using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Produtos
{
    [ExcludeFromCodeCoverage]
    public class ProdutoEmptyDescricaoException : BusinessException
    {
        public ProdutoEmptyDescricaoException() : base("Descrição do produto não pode ser vazio")
        {
        }
    }
}