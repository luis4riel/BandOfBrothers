using BancoTabajara.Aplicacao.Funcionalidades.Usuarios.Command;
using BancoTabajara.Dominio.Funcionalidades.Usuarios;
using System.Linq;

namespace BancoTabajara.Aplicacao.Funcionalidades.Usuarios
{
	public interface IUsuarioServico
	{
        int Inserir(CommandRegistrarUsuario entidade);

        bool Atualizar(CommandAtualizarUsuario entidade);

        bool Deletar(CommandDeletarUsuario entidade);

        IQueryable<Usuario> PegarTodos(int? limite = null);
        
        Usuario PegarPorId(int id);

		Usuario Login(string email, string password);
	}
}
