using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Enderecos
{
    [ExcludeFromCodeCoverage]
    public class EnderecoEmptyComplementoException : BusinessException
    {
        public EnderecoEmptyComplementoException() : base("Endereco com Complemento vazio")
        {
        }
    }
}