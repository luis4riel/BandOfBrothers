using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Enderecos
{
    [ExcludeFromCodeCoverage]
    public class EnderecoEmptyMunicipioException : BusinessException
    {
        public EnderecoEmptyMunicipioException() : base("Endereco com municipio vazio")
        {
        }
    }
}