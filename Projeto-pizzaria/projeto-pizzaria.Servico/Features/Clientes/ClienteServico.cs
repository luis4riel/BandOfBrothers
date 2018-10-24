using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Features.Clientes;

namespace projeto_pizzaria.Servico.Features.Clientes
{
    public class ClienteServico : IClienteServico
    {
		IClienteRepositorio _repositorio;

		public ClienteServico( IClienteRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public void Deletar( Cliente entidade )
		{
			if (entidade.Id == 0)
				throw new IdentifierUndefinedException();
			else
				_repositorio.Deletar( entidade );
		}

		public Cliente PegarPorId( long id )
		{
			if (id == 0)
				throw new IdentifierUndefinedException();
			else
				return _repositorio.PegarPorId( id );
		}

		public IEnumerable<Cliente> PegarTodos()
		{
			IEnumerable<Cliente> ListaCliente = _repositorio.PegarTodos();
			return ListaCliente;
		}

		public Cliente Atualizar( Cliente entidade )
		{
			entidade.Validar();

			if (entidade.Id == 0)
				throw new IdentifierUndefinedException();
			else
				return _repositorio.Atualizar( entidade );
		}

		public Cliente Salvar( Cliente entidade )
		{
			entidade.Validar();
			return entidade = _repositorio.Salvar( entidade );
		}
	}
}