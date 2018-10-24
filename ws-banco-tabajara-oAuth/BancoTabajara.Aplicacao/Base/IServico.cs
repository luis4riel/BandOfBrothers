using System.Linq;
using BancoTabajara.Dominio.Base;

namespace BancoTabajara.Aplicacao.Base
{
	public interface IServico<T> where T : Entidade
	{
		int Inserir( T entidade );
		bool Atualizar( T entidade );
		bool Deletar( T entidade );
		IQueryable<T> PegarTodos( int? limite = null );
		T PegarPorId( int id );
	}
}