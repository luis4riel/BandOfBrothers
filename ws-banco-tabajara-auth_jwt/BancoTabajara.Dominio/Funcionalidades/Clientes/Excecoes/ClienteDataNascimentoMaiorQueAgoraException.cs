using BancoTabajara.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Funcionalidades.Clientes.Excecoes
{
    public class ClienteDataNascimentoMaiorQueAgoraException : ExcecaoDeNegocio
    {
        public ClienteDataNascimentoMaiorQueAgoraException() : base("Data de nascimento deve ser menor que hoje")
        {

        }
    }
}
