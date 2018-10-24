using System;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }

        public BusinessException(string message) : base(message)
        {

        }
    }
}
