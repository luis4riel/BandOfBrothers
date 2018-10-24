using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Enderecos
{
    [ExcludeFromCodeCoverage]
    public class EnderecoEmptyLogradouroException : BusinessException
    {
        public EnderecoEmptyLogradouroException() : base("Endereco com logradouro vazio")
        {
        }
    }
}