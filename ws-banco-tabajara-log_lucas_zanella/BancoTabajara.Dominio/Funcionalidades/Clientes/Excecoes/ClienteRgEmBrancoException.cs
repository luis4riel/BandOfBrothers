using BancoTabajara.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Funcionalidades.Clientes.Excecoes
{
    public class ClienteRgEmBrancoException : ExcecaoDeNegocio
    {
        public ClienteRgEmBrancoException() : base("RG não pode estar em branco!")
        {
        }
    }
}
