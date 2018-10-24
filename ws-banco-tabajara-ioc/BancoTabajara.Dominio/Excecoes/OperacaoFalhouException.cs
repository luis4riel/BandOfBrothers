using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Excecoes
{
    public class OperacaoFalhouException : ExcecaoDeNegocio
    {
        public OperacaoFalhouException() : base("Não foi possível realizar esta operação")
        {
        }
    }
}
