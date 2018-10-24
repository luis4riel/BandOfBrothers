using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Queries;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes;
using BancoTabajara.Dominio.Funcionalidades.Extrato;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas
{
    public class ContaServico : IContaServico
	{
		private readonly IContaRepositorio _repositorioConta;
		private readonly IClienteRepositorio _repositorioCliente;

		public ContaServico( IContaRepositorio repositorioConta, IClienteRepositorio repositorioCliente )
		{
			_repositorioConta = repositorioConta;
			_repositorioCliente = repositorioCliente;
		}

		public bool Atualizar(ContaAtualizarCommand contaCmd)
		{
			Cliente titular = _repositorioCliente.PegarPorId(contaCmd.ClienteId) ?? throw new NaoEncontradoException();
            Conta conta = _repositorioConta.PegarPorId(contaCmd.Id) ?? throw new NaoEncontradoException();

            Mapper.Map(contaCmd, conta);

            return _repositorioConta.Atualizar(conta);
		}

        public bool Deletar(ContaRemoveCommand contaCmd) => _repositorioConta.Deletar(contaCmd.Id);

		public int Inserir(ContaRegistroCommand contaCmd)
		{
            Cliente titular = _repositorioCliente.PegarPorId(contaCmd.ClienteId) ?? throw new NaoEncontradoException();
            var conta = Mapper.Map<ContaRegistroCommand, Conta>(contaCmd);
            conta.Titular = titular;

            var novaConta = _repositorioConta.Inserir(conta);


            return novaConta.Id;
		}

		public ContaQuery PegarPorId( int id )
		{
            var conta = _repositorioConta.PegarPorId(id);

            return Mapper.Map<Conta, ContaQuery>(conta);
        }

		public IQueryable<ContaQuery> PegarTodos( int? limite = null)
        {
            IList<ContaQuery> conta =  Mapper.Map<IList<Conta>, IList<ContaQuery>>(_repositorioConta.PegarTodos(limite).ToList());

            return conta.AsQueryable();
        }

        public bool AtualizarStatus(int id)
        {
            try
            {
                if (id < 1)
                    throw new NaoEncontradoException();

                Conta conta = _repositorioConta.PegarPorId(id) ?? throw new NaoEncontradoException();

                conta.MudarStatus();
                return _repositorioConta.Atualizar(conta);
            }
            catch (Exception)
            {
                throw new AtualizarStatusException();
            }
        }

        public bool Deposito(int id, decimal quantia)
        {
            try
            {
                if (id < 1)
                    throw new NaoEncontradoException();

                Conta conta = _repositorioConta.PegarPorId(id) ?? throw new NaoEncontradoException();
                conta.Deposita(quantia, TipoMovimentacao.Credito );

				return _repositorioConta.Atualizar(conta);
            }
            catch (Exception)
            {
                throw new OperacaoFalhouException();
            }
        }

        public bool Saque(int id, decimal quantia)
        {
            try
            {
                if (id < 1)
                    throw new NaoEncontradoException();

                var conta = _repositorioConta.PegarPorId(id) ?? throw new NaoEncontradoException();
                conta.Saca(quantia, TipoMovimentacao.Debito );

				return _repositorioConta.Atualizar(conta);
            }
            catch (Exception)
            {
                throw new OperacaoFalhouException();
            }
        }

        public bool Transferencia(int idOrigem, int idDestino, decimal quantia)
        {
            try
            {
                if (idOrigem < 1 || idDestino < 1)
                    throw new NaoEncontradoException();

                var contaOrigem = _repositorioConta.PegarPorId(idOrigem) ?? throw new NaoEncontradoException();
                var contaDestino = _repositorioConta.PegarPorId(idDestino) ?? throw new NaoEncontradoException();

                contaOrigem.Transfere(contaDestino, quantia);
				var saque = _repositorioConta.Atualizar(contaOrigem);
                var deposito = _repositorioConta.Atualizar(contaDestino);
                if (saque && deposito)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw new OperacaoFalhouException();
            }
        }

        public Extrato GerarExtrato(int id)
        {
            if (id < 1)
                throw new NaoEncontradoException();

            Conta conta = _repositorioConta.PegarPorId(id) ?? throw new NaoEncontradoException();
            return Extrato.GerarExtrato(conta);
        }
    }
}
