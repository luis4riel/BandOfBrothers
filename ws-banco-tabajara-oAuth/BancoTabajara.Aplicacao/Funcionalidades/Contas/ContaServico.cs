using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BancoTabajara.Aplicacao.Base;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Command;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes;
using BancoTabajara.Dominio.Funcionalidades.Extrato;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using BancoTabajara.Infra.ORM.Funcionalidade.Contas;

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

		public bool Atualizar(CommandAtualizarConta conta)
		{
            var _conta = Mapper.Map<CommandAtualizarConta, Conta>(conta);

			//Cliente titular = _repositorioCliente.PegarPorId(_conta.Titular.Id) ?? throw new NaoEncontradoException();
            Cliente titular = _repositorioCliente.PegarPorId(_conta.ClienteId) ?? throw new NaoEncontradoException();
            Conta contaDb = _repositorioConta.PegarPorId( _conta.Id) ?? throw new NaoEncontradoException();
 
			contaDb.Estado = _conta.Estado;
			contaDb.Limite = _conta.Limite;
			contaDb.Titular = titular;
            
			return _repositorioConta.Atualizar(contaDb);
		}

        public bool Deletar(CommandDeletarConta conta)
        {
            var _conta = Mapper.Map<CommandDeletarConta, Conta>(conta);

            if(_conta.Id < 1)
                throw new NaoEncontradoException();

            return _repositorioConta.Deletar(_conta.Id);
        }

		public int Inserir( CommandRegistrarConta conta )
		{
            var _conta = Mapper.Map<CommandRegistrarConta, Conta>(conta);

            Cliente titular = _repositorioCliente.PegarPorId(_conta.ClienteId) ?? throw new NaoEncontradoException();

            _conta.Titular = titular;
			_conta.GerarNumeroConta();

			var novaConta = _repositorioConta.Inserir( _conta );

			return novaConta.Id;
		}

		public Conta PegarPorId( int id )
		{
            if (id < 1)
                throw new NaoEncontradoException();

			return _repositorioConta.PegarPorId( id) ?? throw new NaoEncontradoException();
        }

		public IQueryable<Conta> PegarTodos( int? limite = null ) => _repositorioConta.PegarTodos(limite);

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
