using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Infra.Contexto;

namespace BancoTabajara.Infra.ORM.Funcionalidade.Contas
{
	public class ContaRepositorio : IContaRepositorio
	{

		private BancoTabajaraDbContext _contexto;

		public ContaRepositorio( BancoTabajaraDbContext contexto )
		{
			_contexto = contexto;
		}

		public bool Atualizar( Conta conta )
		{
			_contexto.Entry( conta ).State = EntityState.Modified;
			return _contexto.SaveChanges() > 0;
		}

		public bool Deletar( int contaId )
		{
			var conta = _contexto.Contas.Where( o => o.Id == contaId ).FirstOrDefault();
			if (conta == null)
				throw new NaoEncontradoException();

			_contexto.Entry( conta ).State = EntityState.Deleted;
			return _contexto.SaveChanges() > 0;
		}

		public Conta Inserir( Conta conta )
		{
			_contexto.Clientes.Attach( conta.Titular );
			var newConta = _contexto.Contas.Add( conta );
			_contexto.SaveChanges();
			return newConta;
		}

		public Conta PegarPorId( int contaId )
		{
			return _contexto.Contas.Include("Movimentacoes").Include("Titular").FirstOrDefault(c => c.Id == contaId);
        }

		public IQueryable<Conta> PegarTodos(int? limite = null)
		{
            if (limite != null)
                return _contexto.Contas.Include("Titular").Take(Convert.ToInt32(limite)).OrderBy(c => c.Estado);
            else
                return _contexto.Contas.OrderBy(c => c.Estado).Include("Titular");
		}
	}
}
