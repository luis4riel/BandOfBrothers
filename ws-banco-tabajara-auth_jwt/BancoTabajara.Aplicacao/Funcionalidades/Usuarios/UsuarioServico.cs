using System.Linq;
using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Usuarios.Command;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Usuarios;

namespace BancoTabajara.Aplicacao.Funcionalidades.Usuarios
{
	public class UsuarioServico : IUsuarioServico
	{
		private readonly IUsuarioRepositorio _repositorioUsuario;

		public UsuarioServico( IUsuarioRepositorio repositorioUsuario )
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public bool Atualizar(CommandAtualizarUsuario usuario)
		{
            var _usuario = Mapper.Map<CommandAtualizarUsuario, Usuario>(usuario);

            Usuario usuarioDb = _repositorioUsuario.PegarPorId( _usuario.Id) ?? throw new NaoEncontradoException();
 
			usuarioDb.Password = _usuario.Password;
			usuarioDb.Username = _usuario.Username;
            
			return _repositorioUsuario.Atualizar(usuarioDb);
		}

        public bool Deletar(CommandDeletarUsuario usuario)
        {
            var _usuario = Mapper.Map<CommandDeletarUsuario, Usuario>(usuario);

            if(_usuario.Id < 1)
                throw new NaoEncontradoException();

            return _repositorioUsuario.Deletar(_usuario.Id);
        }

		public int Inserir( CommandRegistrarUsuario usuario )
		{
            var _usuario = Mapper.Map<CommandRegistrarUsuario, Usuario>(usuario);
			var novaUsuario = _repositorioUsuario.Inserir( _usuario );

			return novaUsuario.Id;
		}

		public Usuario PegarPorId( int id )
		{
            if (id < 1)
                throw new NaoEncontradoException();

			return _repositorioUsuario.PegarPorId( id) ?? throw new NaoEncontradoException();
        }

		public Usuario PegarPorNomeESenha(string nome, string senha)
		{
			if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(senha))
				throw new NaoEncontradoException();

			return _repositorioUsuario.PegarPorNomeESenha( nome, senha ) ?? throw new NaoEncontradoException();
		}

		public IQueryable<Usuario> PegarTodos( int? limite = null ) => _repositorioUsuario.PegarTodos(limite);   
    }
}
