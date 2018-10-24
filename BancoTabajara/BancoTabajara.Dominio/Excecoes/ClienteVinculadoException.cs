using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Excecoes
{
    public  class ClienteVinculadoException : ExcecaoDeNegocio
    {
        public ClienteVinculadoException() : base("Você não pode deletar um cliente com vinculo")
        {
        }
    }
}
