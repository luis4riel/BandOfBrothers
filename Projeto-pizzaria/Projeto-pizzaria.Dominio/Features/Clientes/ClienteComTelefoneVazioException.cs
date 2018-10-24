using Projeto_pizzaria.Dominio.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Dominio.Features.Clientes
{
    public class ClienteComTelefoneVazioException : BusinessException
    {
        public ClienteComTelefoneVazioException():base("O telefone do cliente deve ser informado")
        {

        }
    }
}
