using NFe.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Dominio.Features.Enderecos
{
    [ExcludeFromCodeCoverage]
    public class EnderecoEmptyBairroException : BusinessException
    {
        public EnderecoEmptyBairroException() : base("Localizacao com bairro vazio")
        {
        }
    }
}