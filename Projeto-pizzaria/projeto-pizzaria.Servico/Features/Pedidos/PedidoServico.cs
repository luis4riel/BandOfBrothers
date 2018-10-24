using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Features.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Infra.Data.Features.Pedidos;

namespace projeto_pizzaria.Servico.Features.Pedidos
{
    public class PedidoServico : IPedidoServico
    {
		IPedidoRepositorio _repositorio;

		public PedidoServico( IPedidoRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public Pedido Salvar( Pedido entidade )
		{
			entidade.Validar();
			return entidade = _repositorio.Salvar( entidade );
		}

		public void Deletar( Pedido entidade )
		{
			if (entidade.Id == 0)
				throw new IdentifierUndefinedException();
			else
				_repositorio.Deletar( entidade );
		}

		public Pedido PegarPorId( long id )
		{
			if (id == 0)
				throw new IdentifierUndefinedException();
			else
				return _repositorio.PegarPorId( id );
		}

        public IEnumerable<Pedido> PegarTodos()
        {
			IEnumerable<Pedido> ListaPedidos = _repositorio.PegarTodos();
			return ListaPedidos;
		}

		public Pedido Atualizar( Pedido entidade )
		{
			entidade.Validar();

			if (entidade.Id == 0)
				throw new IdentifierUndefinedException();
			else
				return _repositorio.Atualizar( entidade );
		}
	}
}
