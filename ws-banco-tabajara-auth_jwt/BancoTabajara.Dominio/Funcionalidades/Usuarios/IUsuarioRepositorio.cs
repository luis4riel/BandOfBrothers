using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoTabajara.Dominio.Funcionalidades.Usuarios
{
	public interface IUsuarioRepositorio 
	{
		IQueryable<Usuario> PegarTodos( int? limite = null );

		Usuario Inserir( Usuario cliente );

		bool Atualizar( Usuario cliente );

		Usuario PegarPorId( int clienteId );

		Usuario PegarPorNomeESenha(string nome, string senha);

		bool Deletar( int clienteId );
	}
}
