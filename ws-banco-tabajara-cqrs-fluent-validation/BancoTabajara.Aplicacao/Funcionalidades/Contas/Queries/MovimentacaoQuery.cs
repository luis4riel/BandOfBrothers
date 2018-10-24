using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas.Queries
{
    public class MovimentacaoQuery
    {
        public decimal Valor { get; private set; }
        public TipoMovimentacao Tipo { get; private set; }
        public DateTime? Data { get; private set; }
    }
}
