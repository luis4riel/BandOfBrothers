using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Infra.Data.Features.Clientes
{
	public class ClienteRepositorio : IClienteRepositorio
	{
		private PizzariaContext _contexto;

		public ClienteRepositorio( PizzariaContext contexto )
		{
			_contexto = contexto;
		}

		public Cliente Atualizar( Cliente entidade )
		{
			DbEntityEntry dbEntityEntry = _contexto.Entry( entidade );

            if (dbEntityEntry.State == EntityState.Detached)
            {
                _contexto.Clientes.Attach(entidade);
            }
			_contexto.SaveChanges();

			return entidade;
		}

		public void Deletar( Cliente entidade )
		{
			DbEntityEntry dbEntityEntry = _contexto.Entry( entidade );

			if (dbEntityEntry.State == EntityState.Detached)
				_contexto.Clientes.Attach( entidade );

			_contexto.Clientes.Remove( entidade );

			_contexto.SaveChanges();
		}

		public Cliente PegarPorId( long id )
		{
			return _contexto.Clientes.Find( id );
		}

		public IEnumerable<Cliente> PegarTodos()
		{
			return _contexto.Clientes.OrderBy(c => c.Nome).ToList();
		}

		public Cliente Salvar( Cliente entidade )
		{
			_contexto.Clientes.Add( entidade );
			_contexto.SaveChanges();

			return entidade;
		}
	}
}
