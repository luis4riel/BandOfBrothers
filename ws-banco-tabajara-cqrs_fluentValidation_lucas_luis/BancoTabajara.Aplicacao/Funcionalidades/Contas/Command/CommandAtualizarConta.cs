using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas.Command
{
    public class CommandAtualizarConta
    {
        public virtual int Id { get; set; }
        public virtual bool Estado { get; set; }
        public decimal Limite { get; set; }
        public virtual int ClienteId { get; set; }
    }
}
