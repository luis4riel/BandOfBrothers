using BancoTabajara.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes
{
    public class AtualizarStatusException : ExcecaoDeNegocio
    {
        public AtualizarStatusException() : base("Não foi possível atualizar o status do cliente")
        {
        }
    }
}
