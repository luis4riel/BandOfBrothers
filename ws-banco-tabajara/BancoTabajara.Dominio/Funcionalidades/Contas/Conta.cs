using BancoTabajara.Dominio.Base;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using System;
using System.Collections.Generic;

namespace BancoTabajara.Dominio.Funcionalidades.Contas
{
    public class Conta : Entidade
    {
        public string NumeroConta { get; private set; }
        public decimal Saldo { get; private set; }
        public bool Estado { get; set; }
        public decimal Limite { get; set; }
        public decimal SaldoTotal { get => Saldo + Limite; }
        public virtual Cliente Titular { get; set; }
        public virtual List<Movimentacao> Movimentacoes { get; set; }

        public Conta() { }

        public Conta(Cliente titular)
        {
            Titular = titular;
            Movimentacoes = new List<Movimentacao>();
            GerarNumeroConta();
        }

        public bool Saca(decimal quantia, TipoMovimentacao tipoMovimentacao)
        {
            if (Estado)
            {
                if (Saldo + Limite < quantia)
                    return false;
                else
                {
                    decimal novoSaldo = Saldo - quantia;
                    Saldo = novoSaldo;
					RegistraMovimentacao( tipoMovimentacao, quantia, Id);

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
                bool realizouSaque = Saca(quantia, TipoMovimentacao.DebitoTransferencia );

                if (realizouSaque)
                {
                    contaDestino.Deposita(quantia, TipoMovimentacao.CreditoTransferencia);
                    return true;
                }
                else
                    return false;
            }
            else
                throw new ContaInativaException();
        }

        public void Deposita(decimal quantia, TipoMovimentacao tipoMovimentacao )
        {
            if (Estado)
            {
                Saldo += quantia;
				RegistraMovimentacao(tipoMovimentacao, quantia, Id);
            }
            else
                throw new ContaInativaException();
        }

        private void GerarNumeroConta()
        {
            Random rdn = new Random();
            NumeroConta = rdn.Next(0, 999999).ToString("D6");
        }

        public override void Validar()
        {
            if (Titular is null)
                throw new ContaTitularVazioException();

			Titular.Validar();
        }

        private void RegistraMovimentacao(TipoMovimentacao tipo, decimal quantia, int id)
        {
            Movimentacao movimentacao = new Movimentacao(tipo, quantia, id);
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
