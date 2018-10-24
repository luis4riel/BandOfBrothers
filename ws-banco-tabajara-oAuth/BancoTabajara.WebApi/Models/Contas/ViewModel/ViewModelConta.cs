using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancoTabajara.WebApi.Models.Contas.ViewModel
{
    public class ViewModelConta
    {
        public int Id { get; set; }
        public string NumeroConta { get; private set; }
        public decimal Saldo { get; private set; }
        public bool Estado { get; set; }
        public decimal Limite { get; set; }
        public decimal SaldoTotal { get => Saldo + Limite; }
        //public virtual Cliente Titular { get; set; }
        //public virtual List<Movimentacao> Movimentacoes { get; set; }
        public virtual int ClienteId { get; set; }
    }
}