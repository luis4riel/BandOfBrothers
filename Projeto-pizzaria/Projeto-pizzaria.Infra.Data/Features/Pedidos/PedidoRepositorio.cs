using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Features.Pedidos;
using Projeto_pizzaria.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Projeto_pizzaria.Infra.Data.Features.Pedidos
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private PizzariaContext _contexto;

        public PedidoRepositorio( PizzariaContext contexto )
        {
            _contexto = contexto;
        }

        public Pedido Atualizar(Pedido entidade)
        {   
            DbEntityEntry dbEntityEntry = _contexto.Entry(entidade);

            if (dbEntityEntry.State == EntityState.Detached)
                _contexto.Pedidos.Attach(entidade);

            _contexto.SaveChanges();

            return entidade;
        }

        public void Deletar(Pedido entidade)
        {
            DbEntityEntry dbEntityEntry = _contexto.Entry(entidade);

            if (dbEntityEntry.State == EntityState.Detached)
                _contexto.Pedidos.Attach(entidade);

            _contexto.Pedidos.Remove(entidade);

            _contexto.SaveChanges();
        }

        public Pedido PegarPorId(long id)
        {
            return _contexto.Pedidos.Find(id);
        }

        public IEnumerable<Pedido> PegarTodos()
        {
            return _contexto.Pedidos.OrderBy( c => c.StatusPedido ).ToList();
		}

        public Pedido Salvar(Pedido entidade)
        {
            _contexto.Pedidos.Add(entidade);
            _contexto.SaveChanges();

            return entidade;
        }
    }
}
