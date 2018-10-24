using System;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException() : base("Operação não suportada")
        {

        }
    }
}
