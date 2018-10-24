using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Dominio.Funcionalidades.Contas;

namespace BancoTabajara.Common.Tests.Funcionalidades
{
    public static partial class ObjectMother
    {
        public static Conta ObtemContaValida() => new Conta(ClienteValido())
        {
            Id = 1,
            Estado = true,
            Limite = 500
        };

        public static Conta ObtemContaValidaParaTransferencia() => new Conta(ClienteValido())
        {
            Id = 2,
            Estado = true,
            Limite = 500
        };

        public static Conta ContaInvalidaSemCliente() => new Conta()
        {
            Id = 1,
            Estado = false,
            Limite = 500
        };

        public static Conta ContaInvalida() => new Conta(ClienteValido())
        {
            Id = 1,
            Estado = false,
            Limite = 500
        };
    }
}
