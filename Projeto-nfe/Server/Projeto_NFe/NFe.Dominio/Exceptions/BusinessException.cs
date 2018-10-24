using System;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Dominio.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BusinessException : Exception
    {
        public BusinessException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(string message) : base(message)
        {

        }
        public ErrorCodes ErrorCode { get; }

    }
}
