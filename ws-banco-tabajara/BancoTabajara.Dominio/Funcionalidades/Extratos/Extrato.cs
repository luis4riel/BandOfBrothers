using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using System;
using System.Collections.Generic;

namespace BancoTabajara.Dominio.Funcionalidades.Extrato
{
    public class Extrato
    {
        public string NomeCliente { get; set; }
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }
        public decimal SaldoTotal { get; set; }
        public DateTime Data { get; set; }
        public List<DadosMovimentacao> Movimentacoes { get; set; }

        public Extrato()
        {
            Movimentacoes = new List<DadosMovimentacao>();
        }

        public static Extrato GerarExtrato(Conta conta)
        {
            return new Extrato()
            {
                NomeCliente = conta.Titular.Nome,
                NumeroConta = conta.NumeroConta,
                Saldo = conta.Saldo,
                Limite = conta.Limite,
                SaldoTotal = conta.SaldoTotal,
                Data = DateTime.Now,
                Movimentacoes = DadosDaMovimentacao(conta.Movimentacoes)
            };
        }

        public static List<DadosMovimentacao> DadosDaMovimentacao(List<Movimentacao> Movimentacoes)
        {
            List<DadosMovimentacao> dadosMovimentacao = new List<DadosMovimentacao>();

            foreach (var movimentacao in Movimentacoes)
            {
                dadosMovimentacao.Add(new DadosMovimentacao { Valor = movimentacao.Valor, Data = movimentacao.Data, Tipo = movimentacao.Tipo });
            }

            return dadosMovimentacao;
        }
    }

    public class DadosMovimentacao
    {
        public decimal Valor { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public DateTime? Data { get; set; }
    }
}
