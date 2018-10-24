using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Excecoes
{
    public class ExcecaoDeNegocio : Exception
    {
        public CodigoErros CodigoErro { get; }
        public ExcecaoDeNegocio(string message) : base(message)
        {

        }
        public ExcecaoDeNegocio(CodigoErros codigo, string message) : base(message)
        {
            CodigoErro = codigo;
        }

        
    }
}
