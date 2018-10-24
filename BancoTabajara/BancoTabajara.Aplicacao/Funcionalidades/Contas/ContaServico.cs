using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Aplicacao.Base;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
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

		public bool Atualizar(Conta conta)
		{
			var titular = _repositorioCliente.PegarPorId(conta.Titular.Id);
			var contaDb = _repositorioConta.PegarPorId( conta.Id );
			if (contaDb == null)
				throw new NaoEncontradoException();
 
			contaDb.Estado = conta.Estado;
			contaDb.Limite = conta.Limite;
			contaDb.Titular = titular;
			
			return _repositorioConta.Atualizar(contaDb);
		}

        public bool AtualizarStatus(Conta conta)
        {
            conta.MudarStatus();
            return _repositorioConta.Atualizar(conta);
        }

        public bool Deletar(Conta conta) => _repositorioConta.Deletar(conta.Id);

		public int Inserir( Conta conta )
		{
			var newconta = _repositorioConta.Inserir( conta );

			return newconta.Id;
		}

		public Conta PegarPorId( int id )
		{
			return _repositorioConta.PegarPorId( id );
		}

		public IQueryable<Conta> PegarTodos( int? limite = null ) => _repositorioConta.PegarTodos(limite);
	}
}
