using NFe.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Dominio.Features.Enderecos
{
    [ExcludeFromCodeCoverage]
    public class EnderecoEmptyEstadoException : BusinessException
    {
        public EnderecoEmptyEstadoException() : base("Localizacao com estado vazio")
        {
        }
    }
}