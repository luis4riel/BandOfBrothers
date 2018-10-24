using System;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Infra.Features.Cnpj
{
    [ExcludeFromCodeCoverage]
    public class CnpjInvalidoException : Exception
    {
        public CnpjInvalidoException() : base("O Cnpj informado é inválido")
        {

        }
    }
}
