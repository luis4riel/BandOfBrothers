using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas.Queries
{
    public class ContaQuery
    {
        public virtual int Id { get; set; }
        public string NumeroConta { get; private set; }
        public decimal Saldo { get; private set; }
        public bool Estado { get; set; }
        public decimal Limite { get; set; }
        public virtual Cliente Titular { get; set; }
        public List<MovimentacaoQuery> Movimentacoes { get; set; }
    }
}
