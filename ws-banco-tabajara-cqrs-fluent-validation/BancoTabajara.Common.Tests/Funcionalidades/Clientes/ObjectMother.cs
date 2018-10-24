using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Common.Tests.Funcionalidades
{
    public static partial class ObjectMother
    {
        public static Cliente ClienteValido() => new Cliente
        {
            Nome = "Zé das batatas",
            Cpf = "089.886.799-11",
            DataNascimento = DateTime.Now.AddYears(-20),
            Rg = "5821703"
        };
        public static Cliente ClienteRgEmBranco() => new Cliente
        {
            Rg = String.Empty,
            Nome = "Zé das batatas",
            Cpf = "089.886.799-11",
            DataNascimento = DateTime.Now.AddYears(-20)
        };

        public static Cliente ClienteNomeEmBranco() => new Cliente
        {
            Nome = String.Empty,
            Cpf = "089.886.799-11",
            DataNascimento = DateTime.Now.AddYears(-20),
            Rg = "5821703"
        };

        public static Cliente ClienteCpfEmBranco() => new Cliente
        {
            Nome = "Zé das batatas",
            Cpf = String.Empty,
            DataNascimento = DateTime.Now.AddYears(-20),
            Rg = "5821703"
        };

    }
}
