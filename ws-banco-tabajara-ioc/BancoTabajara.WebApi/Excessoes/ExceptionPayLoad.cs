using BancoTabajara.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancoTabajara.WebApi.Excessoes
{
    public class ExceptionPayload
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public static ExceptionPayload New<T>(T exception) where T : Exception
        {
            int errorCode;
            if (exception is ExcecaoDeNegocio)
                errorCode = (exception as ExcecaoDeNegocio).CodigoErro.GetHashCode();
            else
                errorCode = CodigoErros.Unhandled.GetHashCode();
            return new ExceptionPayload
            {
                ErrorCode = errorCode,
                ErrorMessage = exception.Message,
            };
        }
    }
}