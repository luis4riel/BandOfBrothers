using System;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Infra.Features.Cpf
{
    [ExcludeFromCodeCoverage]
    public class CpfInvalidoException : Exception
    {
        public CpfInvalidoException() : base("O Cpf informado é inválido")
        {

        }
    }
}
