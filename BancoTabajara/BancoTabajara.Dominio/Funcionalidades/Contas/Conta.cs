using BancoTabajara.Dominio.Base;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using System.Collections.Generic;

namespace BancoTabajara.Dominio.Funcionalidades.Contas
{
    public class Conta : Entidade
    {
        public long Numero { get; private set; }
        public decimal Saldo { get; private set; }
        public bool Estado { get; set; }
        public decimal Limite { get; set; }
        public decimal SaldoTotal { get => Saldo + Limite; }
        public virtual Cliente Titular { get; set; }
        public virtual List<Movimentacao> Movimentacoes { get; set; }

        private Movimentacao movimentacao;

        public Conta() { }

        public Conta(Cliente titular)
        {
            this.Titular = titular;
            Movimentacoes = new List<Movimentacao>();
            GerarNumeroConta();
        }

        public bool Saca(decimal quantia)
        {
            if (Estado)
            {
                if (Saldo + Limite < quantia)
                    return false;
                else
                {
                    decimal novoSaldo = Saldo - quantia;
                    this.Saldo = novoSaldo;
                    RegistraMovimentacao(TipoMovimentacao.Debito, quantia, this);
                    return true;
                }
            }
            else
                throw new ContaInativaException();
        }


        public bool Transfere(Conta contaDestino, decimal quantia)
        {
            if (Estado && contaDestino.Estado)
            {
                bool realizouSaque = this.Saca(quantia);
                movimentacao = new Movimentacao(TipoMovimentacao.Debito, quantia, this);

                if (realizouSaque)
                {
                    contaDestino.Deposita(quantia);
                    RegistraMovimentacao(TipoMovimentacao.Debito, quantia, this);
                    return true;
                }
                else
                    return false;
            }
            else
                throw new ContaInativaException();
        }

        public void Deposita(decimal quantia)
        {
            if (Estado)
            {
                this.Saldo += quantia;
                RegistraMovimentacao(TipoMovimentacao.Debito, quantia, this);
            }
            else
                throw new ContaInativaException();
        }

        private void GerarNumeroConta()
        {
            int numeroContaPadrao = 1000;
            this.Numero = Titular.Id + numeroContaPadrao;
        }

        public override void Validar()
        {
            if (Titular is null)
                throw new ContaTitularVazioException();
        }

        private void RegistraMovimentacao(TipoMovimentacao tipo, decimal quantia, Conta conta)
        {
            movimentacao = new Movimentacao(tipo, quantia, conta);
            Movimentacoes.Add(movimentacao);
        }
        public void MudarStatus()
        {
            if (Estado)
                Estado = false;
            else
                Estado = true;
        }
    }
}
