using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Funcionalidades.Contas
{
	public interface IContaRepositorio 
	{
		IQueryable<Conta> PegarTodos( int? limite = null );

		Conta Inserir( Conta cliente );

		bool Atualizar( Conta cliente );

		Conta PegarPorId( int clienteId );

        bool Deletar( int clienteId );
	}
}
