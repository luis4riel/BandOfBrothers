using NFe.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Dominio.Features.Enderecos
{
    [ExcludeFromCodeCoverage]
    public class EnderecoEmptyMunicipioException : BusinessException
    {
        public EnderecoEmptyMunicipioException() : base("Localizacao com municipio vazio")
        {
        }
    }
}