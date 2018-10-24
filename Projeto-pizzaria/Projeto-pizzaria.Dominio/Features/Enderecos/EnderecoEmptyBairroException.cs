using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Enderecos
{
    [ExcludeFromCodeCoverage]
    public class EnderecoEmptyBairroException : BusinessException
    {
        public EnderecoEmptyBairroException() : base("Endereco com bairro vazio")
        {
        }
    }
}